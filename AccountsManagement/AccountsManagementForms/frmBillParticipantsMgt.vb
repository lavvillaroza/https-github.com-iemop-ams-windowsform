'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmBillParticipantsMgt
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     January 19, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for the maintenance of Bill Participants
'Arguments/Parameters:  
'Files/Database Tables:  AMParticipants
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   January 19, 2012        Vladimir E. Espiritu                 GUI design and basic functionalities     
'   February 07, 2012       Vladimir E. Espiritu                 Change Bill Participants in AM Participants
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

'Imports LDAPLib
'Imports LDAPLogin

Public Class frmBillParticipantsMgt
    Private WBillHelper As WESMBillHelper    
    Private BFactory As BusinessFactory

    Public Enum AMParticipantsLoadType
        Add
        Edit
        View
        Save
    End Enum

    Private _LoadType As AMParticipantsLoadType
    Public Property LoadType() As AMParticipantsLoadType
        Get
            Return _LoadType
        End Get
        Set(ByVal value As AMParticipantsLoadType)
            _LoadType = value
        End Set
    End Property

    Private _Participant As AMParticipants
    Public Property Participant() As AMParticipants
        Get
            Return _Participant
        End Get
        Set(ByVal value As AMParticipants)
            _Participant = value
        End Set
    End Property


    Private Sub frmBillParticipantsMgt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        WBillHelper = WESMBillHelper.GetInstance()
        BFactory = BusinessFactory.GetInstance()
        With Me.ddlType
            .Items.Add("GEN")
            .Items.Add("LOAD")
            .Items.Add("DCC")
            .SelectedIndex = -1
        End With

        With Me.ddlPaymentType
            .Items.Add(EnumParticipantPaymentType.Check.ToString())
            .Items.Add(EnumParticipantPaymentType.EFT.ToString())
            .SelectedIndex = -1
        End With

        Me.ddlRegion.Items.Clear()
        With ddlRegion
            .Items.Add("Luzon")
            .Items.Add("Visayas")
            .Items.Add("Mindanao")
            .SelectedIndex = -1
        End With

        Me.ATCType_cmb.Items.Clear()
        Try
            Dim BIRAlphanumericTaxCodes As List(Of BIRAlphanumericTaxCode) = WBillHelper.GetBIRATC()
            For Each item In BIRAlphanumericTaxCodes
                With ATCType_cmb.Items
                    .Add(item.ATCName)
                End With
            Next
            ATCType_cmb.SelectedIndex = 1
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Me.CB_MembershipType.Items.Clear()
        With Me.CB_MembershipType
            .Items.Add("DIRECT")
            .Items.Add("INDIRECT")
        End With

        Select Case Me.LoadType
            Case AMParticipantsLoadType.Add
                Me.txtMFWHTax.Text = "0.0"
                Me.txtMFWHVat.Text = "0.0"                
                Me.txtEnergyWHTax.Text = "0.0"                
                Me.txtEnergyWHVAT.Text = "0.0"                                
                Me.rbZeroRatedMFNo.Checked = True
                Me.rbZeroRatedEnergyNo.Checked = True
                Me.rbActive.Checked = True
            Case AMParticipantsLoadType.Edit
                Me.txtIDNumber.Enabled = False
                Me.LoadParticipant(Me.Participant)
            Case AMParticipantsLoadType.View
                Me.LoadParticipant(Me.Participant)
                Me.gpMain.Enabled = False
                Me.gpAddress.Enabled = False
                Me.gpZeroRatedEnergy.Enabled = False
                Me.gpZeroRatedMF.Enabled = False
                Me.gpBankInfo.Enabled = False
                Me.gpRateInfo.Enabled = False
                Me.gpOtherInfo.Enabled = False
                Me.btnSave.Visible = False
                Me.btnCancel.Text = "Close"
                Me.gpRep.Enabled = False
        End Select
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If Not Me.FormValidation() Then
            Exit Sub
        End If

        Dim ans As MsgBoxResult

        Try            
            If Me.txtIDNumber.Enabled = True Then
                ans = MsgBox("Do you really want to save this record?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")
            Else
                ans = MsgBox("Do you really want to update this record?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Update")
            End If

            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            Dim item As New AMParticipants            

            With item
                .IDNumber = Trim(Replace(Me.txtIDNumber.Text, "'", "")).ToUpper()
                .ParticipantID = Trim(Replace(Me.txtParticipantID.Text.ToUpper(), "'", ""))
                .FullName = Trim(Replace(Me.txtFullName.Text, "'", "")).ToUpper()
                .BusinessStyle = Trim(Replace(Me.txtBusinesStyle.Text, "'", "")).ToUpper()
                .TIN = Trim(Replace(Me.txtTIN.Text, "'", "")).ToUpper()
                .EcoZoneRegCertificateNo = Trim(Replace(Me.txtEcoZoneRegCertNo.Text, "'", "")).ToUpper()
                .EcoZoneEffectiveDate = CDate(IIf(Me.chckEcoZoneEffectivityDate.Checked, _
                                                  Me.dtEcoZoneEffectivityDate.Value.ToString("MM/dd/yyyy"), Nothing))
                If Me.ddlType.Text = "GEN" Then
                    item.GenLoad = EnumGenLoad.G
                ElseIf Me.ddlType.Text = "LOAD" Then
                    item.GenLoad = EnumGenLoad.L
                Else
                    item.GenLoad = EnumGenLoad.DCC
                End If
                .City = Trim(Replace(Me.txtMunicipality.Text, "'", ""))
                .Province = Trim(Replace(Me.txtProvince.Text, "'", ""))
                .ZipCode = Trim(Replace(Me.txtZipCode.Text, "'", ""))
                .Region = Me.ddlRegion.Text
                .ParticipantAddress = Trim(Replace(Me.txtParticipantAddress.Text, "'", ""))
                .BillingAddress = Trim(Replace(Me.txtBillingAddress.Text, "'", ""))

                .Representative = New ParticipantRepresentative(Trim(Replace(Me.txtTitle.Text, "'", "")),
                                                                Trim(Replace(Me.txtRepFName.Text, "'", "")),
                                                                Trim(Replace(Me.txtRepMName.Text, "'", "")),
                                                                Trim(Replace(Me.txtRepLName.Text, "'", "")),
                                                                Trim(Replace(Me.txtRepPosition.Text, "'", "")),
                                                                Trim(Replace(Me.txtRepContactNumbers.Text, "'", "")),
                                                                Trim(Replace(Me.txtRepEmailAddress.Text, "'", "")))

                .ZeroRatedMarketFees = CType(IIf(Me.rbZeroRatedMFYes.Checked, True, False), Boolean)
                .ZeroRatedEnergy = CType(IIf(Me.rbZeroRatedEnergyYes.Checked, True, False), Boolean)

                .Status = CType(IIf(Me.rbActive.Checked, EnumStatus.Active, EnumStatus.InActive), EnumStatus)
                .BankTransactionCode = Trim(Replace(Me.txtBankTransCode.Text, "'", "")).ToUpper()
                .BankAccountNo = Trim(Replace(Me.txtBankAccountNo.Text, "'", "")).ToUpper()
                .Bank = Trim(Replace(Me.txtBank.Text, "'", "")).ToUpper()
                .BankBranch = Trim(Replace(Me.txtBankBranch.Text, "'", "")).ToUpper()
                .CheckPay = Trim(Replace(Me.txtCheckPay.Text, "'", "")).ToUpper()
                .VirtualAccountNo = Trim(Replace(Me.txtVirtualAccountNo.Text, "'", "")).ToUpper()
                .PaymentType = CType(IIf(Me.ddlPaymentType.Text = EnumParticipantPaymentType.Check.ToString(), _
                                    EnumParticipantPaymentType.Check, EnumParticipantPaymentType.EFT), EnumParticipantPaymentType)
                .MarketFeesWHTax = CDec(Me.txtMFWHTax.Text)
                .MarketFeesWHVAT = CDec(Me.txtMFWHVat.Text)
                .EnergyWHTax = CDec(Me.txtEnergyWHTax.Text)
                .EnergyWHVAT = CDec(Me.txtEnergyWHVAT.Text)
                .BIRATCType = CStr(Me.ATCType_cmb.Text)
                .MembershipType = CStr(Me.CB_MembershipType.Text)

            End With

            'Save
            If Me.txtIDNumber.Enabled Then
                WBillHelper.SaveAMParticipant(item, True)
                MsgBox("Successfully Saved!", MsgBoxStyle.Information, "Save")
            Else                
                WBillHelper.SaveAMParticipant(item, False)
                MsgBox("Successfully Updated!", MsgBoxStyle.Information, "Updated")
            End If


            If Me.txtIDNumber.Enabled = True Then
                With item
                    frmBillParticipants.DGridView.Rows.Insert(0, .IDNumber, .ParticipantID, .ParticipantAddress, .Status)
                    'Updated By Lance 08/17/2014
                    _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibParticipantInfoWindow.ToString, "Added New Record with IDNumber: " & .IDNumber & " and ParticipantName: " & .ParticipantID, "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccesffullySaved.ToString, AMModule.UserName)
                End With                            
            End If
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Updated By Lance 08/17/2014            
        End Try

    End Sub

    ReadOnly AllowedKeys As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789-_"
    Private Sub txtIDNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIDNumber.KeyPress
        Select Case e.KeyChar
            Case Convert.ToChar(Keys.Enter) ' Enter is pressed
                Me.txtParticipantID.Select()
            Case Convert.ToChar(Keys.Back) ' Backspace is pressed
                'e.Handled = False ' Delete the character
            Case Convert.ToChar(Keys.Capital Or Keys.RButton) ' CTRL+V is pressed
                ' Paste clipboard content only if contains allowed keys
                e.Handled = Not Clipboard.GetText().All(Function(c) AllowedKeys.Contains(c))
            Case Else ' Other key is pressed
                e.Handled = Not AllowedKeys.Contains(e.KeyChar)
        End Select
    End Sub
    ReadOnly AllowedKeys2 As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789-_"
    Private Sub txtParticipantID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtParticipantID.KeyPress
        Select Case e.KeyChar
            Case Convert.ToChar(Keys.Enter) ' Enter is pressed
                Me.txtFullName.Select()
            Case Convert.ToChar(Keys.Back) ' Backspace is pressed
                'e.Handled = False ' Delete the character
            Case Convert.ToChar(Keys.Capital Or Keys.RButton) ' CTRL+V is pressed
                ' Paste clipboard content only if contains allowed keys
                e.Handled = Not Clipboard.GetText().All(Function(c) AllowedKeys2.Contains(c))
            Case Else ' Other key is pressed
                e.Handled = Not AllowedKeys2.Contains(e.KeyChar)
        End Select
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub chckEcoZoneEffectivityDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chckEcoZoneEffectivityDate.CheckedChanged
        If Me.chckEcoZoneEffectivityDate.CheckState = CheckState.Checked Then
            Me.txtEffectiveDate.Visible = False
        Else
            Me.txtEffectiveDate.Visible = True
        End If
    End Sub

#Region "Method/Functions"

    Private Function FormValidation() As Boolean
        Me.ErrorProvider1.Clear()
        FormValidation = False
        Try
            If Me.txtIDNumber.Text.Trim.Length = 0 Then
                Me.ErrorProvider1.SetError(txtIDNumber, "Please specify the ID Number!")
                Me.txtIDNumber.Select()
                Exit Function
            ElseIf Me.txtParticipantID.Text.Trim.Length = 0 Then
                Me.ErrorProvider1.SetError(txtParticipantID, "Please specify the Participant ID!")
                Me.txtParticipantID.Select()
                Exit Function
            ElseIf Me.txtFullName.Text.Trim.Length = 0 Then
                Me.ErrorProvider1.SetError(txtFullName, "Please specify the Full Name!")
                Me.txtFullName.Select()
                Exit Function
            ElseIf Me.txtBusinesStyle.Text.Trim.Length = 0 Then
                Me.ErrorProvider1.SetError(txtBusinesStyle, "Please specify the Business Style!")
                Me.txtBusinesStyle.Select()
                Exit Function
            ElseIf Not Me.chckEcoZoneEffectivityDate.Checked And Me.txtEcoZoneRegCertNo.Text.Trim.Length <> 0 Then
                Me.ErrorProvider1.SetError(txtEcoZoneRegCertNo, "Please specify the Eco-Zone Effectivity Date if the participant is a member of Eco-zone!")
                Exit Function
            ElseIf Me.chckEcoZoneEffectivityDate.Checked And Me.txtEcoZoneRegCertNo.Text.Trim.Length = 0 Then
                Me.ErrorProvider1.SetError(txtEcoZoneRegCertNo, "Please uncheck the Eco-Zone Effectivity Date if the participant is not a member of Eco-zone!")
                Exit Function
            ElseIf Me.ddlType.SelectedIndex = -1 Then
                Me.ErrorProvider1.SetError(ddlType, "Please select the Type!")
                Me.ddlType.Select()
                Exit Function
            ElseIf Me.ddlPaymentType.SelectedIndex = -1 Then
                Me.ErrorProvider1.SetError(ddlPaymentType, "Please select the Payment Type!")
                Me.ddlPaymentType.Select()
                Exit Function
            ElseIf Not IsNumeric(Me.txtMFWHTax.Text) Then
                Me.ErrorProvider1.SetError(txtMFWHTax, "Market Fees withholding Tax must be numeric!")
                Exit Function
            ElseIf CDec(Me.txtMFWHTax.Text) < 0 Then
                Me.ErrorProvider1.SetError(txtMFWHTax, "Market Fees withholding Tax must be not greater than 1!")                
                Me.txtMFWHTax.Select()
                Exit Function
            ElseIf Not Me.BFactory.CheckPrecisionAndScale(1, 4, Me.txtMFWHTax.Text) Then
                Me.ErrorProvider1.SetError(txtMFWHTax, "The maximum capacity of MF withholding Tax field is 1 whole number and 4 decimal places only!")
                Me.txtMFWHTax.Select()
                Exit Function
            ElseIf Not IsNumeric(Me.txtMFWHVat.Text) Then
                Me.ErrorProvider1.SetError(txtMFWHVat, "Market Fees withholding Vat must be numeric!")
                Me.txtMFWHVat.Select()
                Exit Function
            ElseIf CDec(Me.txtMFWHVat.Text) < 0 Then
                Me.ErrorProvider1.SetError(txtMFWHVat, "Market Fees withholding Vat must be not greater than 1!")
                Me.txtMFWHVat.Select()
                Exit Function
            ElseIf Not Me.BFactory.CheckPrecisionAndScale(1, 4, Me.txtMFWHVat.Text) Then
                Me.ErrorProvider1.SetError(txtMFWHVat, "The maximum capacity of MF withholding Vat field is 1 whole number and 4 decimal places only!")
                Me.txtMFWHVat.Select()
                Exit Function
            ElseIf Not IsNumeric(Me.txtEnergyWHTax.Text) Then
                Me.ErrorProvider1.SetError(txtEnergyWHTax, "Withholding Tax Rate on Energy must be numeric!")
                Me.txtEnergyWHTax.Select()
                Exit Function
            ElseIf CDec(Me.txtEnergyWHTax.Text) < 0 Then
                Me.ErrorProvider1.SetError(txtEnergyWHTax, "Withholding Tax Rate on Energy must not be greater than 1!")
                Me.txtEnergyWHTax.Select()
                Exit Function
            ElseIf Not Me.BFactory.CheckPrecisionAndScale(1, 4, Me.txtEnergyWHTax.Text) Then
                Me.ErrorProvider1.SetError(txtEnergyWHTax, "The maximum capacity of Withholding Tax Rate on Energy field is 1 whole number and 4 decimal places only!")
                Me.txtEnergyWHTax.Select()                
                Exit Function            
            ElseIf Not IsNumeric(Me.txtEnergyWHVAT.Text) Then
                Me.ErrorProvider1.SetError(txtEnergyWHTax, "Withholding Vat Rate on Energy must be numeric!")
                Me.txtEnergyWHVAT.Select()
                Exit Function
            ElseIf CDec(Me.txtEnergyWHVAT.Text) < 0 Then
                Me.ErrorProvider1.SetError(txtEnergyWHVAT, "Withholding Vat Rate on Energy must not be greater than 1!")
                Me.txtEnergyWHTax.Select()
                Exit Function
            ElseIf Not Me.BFactory.CheckPrecisionAndScale(1, 4, Me.txtEnergyWHVAT.Text) Then
                Me.ErrorProvider1.SetError(txtEnergyWHVAT, "The maximum capacity of Withholding Vat Rate on Energy field is 1 whole number and 4 decimal places only!")
                Me.txtEnergyWHTax.Select()
                Exit Function
            End If

            If Me.txtIDNumber.Enabled Then
                For index As Integer = 0 To frmBillParticipants.DGridView.RowCount - 1
                    With frmBillParticipants.DGridView.Rows(index)
                        If Me.txtIDNumber.Text = CStr(.Cells(0).Value) Then
                            MsgBox("Duplicate ID Number!", MsgBoxStyle.Critical, "Duplicate")
                            Me.txtIDNumber.Select()
                            Exit Function
                        End If
                    End With
                Next           
            End If

            'Check if the participant has pending balance when deactivated
            If rbInactive.Checked Then
                Dim listWESMBillSummary = WBillHelper.GetWESMBillSummaryPerParticipant(CStr(Me.txtIDNumber.Text))
                listWESMBillSummary = (From x In listWESMBillSummary _
                                       Where x.EndingBalance <> 0 _
                                       Select x).ToList

                Dim cnt = (From x In listWESMBillSummary _
                           Where x.EndingBalance <> 0 _
                           Select x).Count()

                If cnt <> 0 Then
                    MsgBox("The participant can not be deactivated because it has pending transaction/s!", MsgBoxStyle.Exclamation, "Warning")
                    Exit Function
                End If
            End If

            FormValidation = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Sub LoadParticipant(ByVal item As AMParticipants)
        Try
            With item
                Me.txtIDNumber.Text = .IDNumber
                Me.txtParticipantID.Text = .ParticipantID
                Me.txtFullName.Text = .FullName
                Me.txtBusinesStyle.Text = .BusinessStyle
                Me.txtTIN.Text = .TIN
                Me.txtEcoZoneRegCertNo.Text = .EcoZoneRegCertificateNo

                If .EcoZoneEffectiveDate <> Nothing Then
                    chckEcoZoneEffectivityDate.CheckState = CheckState.Checked
                    Me.txtEffectiveDate.Visible = False
                    dtEcoZoneEffectivityDate.Value = .EcoZoneEffectiveDate
                Else
                    Me.chckEcoZoneEffectivityDate.CheckState = CheckState.Unchecked
                    Me.txtEffectiveDate.Visible = True
                End If

                Me.ddlType.Text = CStr(IIf(.GenLoad = EnumGenLoad.G, "GEN", IIf(.GenLoad = EnumGenLoad.DCC, "DCC", "LOAD")))
                Me.txtParticipantAddress.Text = .ParticipantAddress
                Me.txtBillingAddress.Text = .BillingAddress.ToString
                Me.txtMunicipality.Text = .City
                Me.txtProvince.Text = .Province
                Me.txtZipCode.Text = .ZipCode
                Me.ddlRegion.Text = .Region

                Me.txtTitle.Text = .Representative.Title
                Me.txtRepFName.Text = .Representative.FName
                Me.txtRepMName.Text = .Representative.MName
                Me.txtRepLName.Text = .Representative.LName
                Me.txtVirtualAccountNo.Text = .VirtualAccountNo
                Me.txtRepPosition.Text = .Representative.Position
                Me.txtRepContactNumbers.Text = .Representative.Contact
                Me.txtRepEmailAddress.Text = .Representative.EmailAddress

                If .ZeroRatedMarketFees Then
                    Me.rbZeroRatedMFYes.Checked = True
                Else
                    Me.rbZeroRatedMFNo.Checked = True
                End If

                If .ZeroRatedEnergy Then
                    Me.rbZeroRatedEnergyYes.Checked = True
                Else
                    Me.rbZeroRatedEnergyNo.Checked = True
                End If

                If .Status = EnumStatus.Active Then
                    Me.rbActive.Checked = True
                    Me.rbInactive.Checked = False
                Else
                    Me.rbActive.Checked = False
                    Me.rbInactive.Checked = True
                End If

                Me.txtBankTransCode.Text = .BankTransactionCode
                Me.txtBankAccountNo.Text = .BankAccountNo
                Me.txtBank.Text = .Bank
                Me.txtBankBranch.Text = .BankBranch
                Me.txtCheckPay.Text = .CheckPay
                Me.txtVirtualAccountNo.Text = .VirtualAccountNo
                Me.ddlPaymentType.Text = .PaymentType.ToString()
                Me.txtMFWHTax.Text = .MarketFeesWHTax.ToString()
                Me.txtMFWHVat.Text = .MarketFeesWHVAT.ToString()
                Me.txtEnergyWHTax.Text = .EnergyWHTax.ToString()
                Me.txtEnergyWHVAT.Text = .EnergyWHVAT.ToString()
                If String.IsNullOrEmpty(.BIRATCType) Then
                    Me.ATCType_cmb.SelectedIndex = 0
                Else
                    Me.ATCType_cmb.Text = .BIRATCType.ToString
                End If

                If String.IsNullOrEmpty(.MembershipType) Then
                    Me.CB_MembershipType.SelectedIndex = 0
                Else
                    Me.CB_MembershipType.Text = .MembershipType.ToString
                End If

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region


End Class