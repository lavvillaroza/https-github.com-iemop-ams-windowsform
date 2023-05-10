'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmManualDMCM
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     January 18, 2013
'Development Group:      Software Development and Support Division
'Description:            GUI for the maintenance of Collection
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   January 18, 2013        Vladimir E. Espiritu                 GUI design and basic functionalities   
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

Public Class frmManualDMCM
    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory
    Private _ListParticipants As List(Of AMParticipants)

    Private Sub frmManualDMCM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.MdiParent = MainForm
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName
            BFactory = BusinessFactory.GetInstance()

            'Get all the am participants
            Me._ListParticipants = WBillHelper.GetAMParticipantsAll()

            With Me.ddlParticipantID
                .DisplayMember = "ParticipantID"
                .ValueMember = "IDNumber"
                .DataSource = Me._ListParticipants
                .SelectedIndex = -1
            End With

            Me.InitializeDMCMHeader()
            Me.ClearControls()
            Me.EnableControls(False, False, False, True, False, False, False)

            AddHandler Me.ddlParticipantID.SelectedIndexChanged, AddressOf Me.ddlParticipantID_SelectedIndexChanged

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")            
            Exit Sub
        End Try
    End Sub

    Private Sub DGridView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGridView.CellContentClick
        If e.ColumnIndex = 1 Then
            If Not IsNumeric(Me.txtMarketFees.Text) Or Not IsNumeric(Me.txtVATonMarketFees.Text) _
               Or Not IsNumeric(Me.txtEnergy.Text) Or Not IsNumeric(Me.txtVATonEnergy.Text) Then
                Exit Sub

            ElseIf Not Me.rbMarketFees.Checked And Not Me.rbVATonMarketFees.Checked _
                   And Not Me.rbEnergy.Checked And Not Me.rbVATonEnergy.Checked Then

                MsgBox("Please specify first the charge type!", MsgBoxStyle.Critical, "Specify the inputs")
                Exit Sub
            End If

            Dim frmSearch As New frmManualDMCMSearch
            With frmSearch
                .LoadType = frmManualDMCMSearch.EnumManualDMCMSearch.AccountCode
                .ChargeType = Me.GetChargeType()

                If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                    Me.DGridView.CurrentRow.Cells("colAcctCode").Value = .DGridView.CurrentRow.Cells(0).Value
                    Me.DGridView.CurrentRow.Cells("colDescription").Value = .DGridView.CurrentRow.Cells(1).Value
                End If
            End With
        End If

    End Sub

    Private Sub ddlParticipantID_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.ddlParticipantID.SelectedIndex = -1 Then
            Exit Sub
        End If

        Dim IDNumber = CStr(Me.ddlParticipantID.SelectedValue)

        Dim participantID = (From x In Me._ListParticipants _
                             Where x.IDNumber = IDNumber _
                             Select x).First()

        Me.txtParticipantName.Text = participantID.FullName
        Me.txtIDNumber.Text = participantID.IDNumber.ToString()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Me.EnableControls(True, True, True, True, True, False, False)
        Me.ClearControls()

        Me.rbEnergy.Checked = False
        Me.rbVATonEnergy.Checked = False
        Me.rbMarketFees.Checked = False
        Me.rbVATonMarketFees.Checked = False

        Me.InitializeDMCMHeader()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim itemWESMBillSummary As New WESMBillSummary
        Dim flag As Boolean
        Dim DocumentAmount As Decimal

        Try
            If Not ValidateControls() Then
                Exit Sub
            ElseIf Not VaidateGridView() Then
                Exit Sub
            End If

            'Generate DMCM
            Dim itemDMCM = Me.GenerateDebitCreditMemo()

            'Generate the Function Manual DMCM
            Dim itemDMCMFactory = New FuncManualDMCM(itemDMCM)

            'Get the charge type
            Dim chargeType = Me.GetChargeType()

            If Not itemDMCMFactory.IsDebitAndCreditAreEqual Then
                MsgBox("Summation of Debit and Credit must be equal!", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            ElseIf itemDMCM.TotalAmountDue <> itemDMCMFactory.DocumentAmount Then
                MsgBox("Summation of Debit and Credit must be equal to charge type amount!", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            ElseIf itemDMCMFactory.IsAccountsPayable And itemDMCMFactory.IsAccountsRecievable Then
                MsgBox("Accounts Payable and Accounts Receivable must not be specified at the same time!", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            ElseIf itemDMCMFactory.IsDuplicateAccountCode Then
                MsgBox("Duplicate Account Code are not acceptable!", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            If itemDMCMFactory.IsAccountsPayable Then
                DocumentAmount = itemDMCMFactory.DocumentAmount
            Else
                DocumentAmount = itemDMCMFactory.DocumentAmount * -1D
            End If

            If itemDMCMFactory.HasReference Then
                Dim frm As New frmManualDMCMSearch
                With frm
                    If itemDMCMFactory.IsAccountsPayable Then
                        .LoadType = frmManualDMCMSearch.EnumManualDMCMSearch.SelectWESMBillSummaryPayable
                    Else
                        .LoadType = frmManualDMCMSearch.EnumManualDMCMSearch.SelectWESMBillSummaryReceivable
                    End If
                    .IDNumber = CLng(Me.txtIDNumber.Text)
                    .ChargeType = chargeType

                    If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                        Dim listWESMBillNo As New List(Of Long)

                        'Update the WESMBill Summary
                        listWESMBillNo.Add(CLng(.DGridView.CurrentRow.Cells("WESMBillSummaryNo").Value))
                        itemWESMBillSummary = WBillHelper.GetWESMBillSummary(listWESMBillNo, False).First()

                        'Update the DMCM
                        itemDMCM.BillingPeriod = itemWESMBillSummary.BillPeriod
                        itemDMCM.DueDate = itemWESMBillSummary.DueDate
                        itemDMCM.ChargeType = chargeType

                        'Update flag
                        flag = False
                    Else
                        Exit Sub
                    End If
                End With
            Else
                Dim frm As New frmManualDMCMSearch
                frm.LoadType = frmManualDMCMSearch.EnumManualDMCMSearch.InsertWESMBillSummary
                If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then

                    'Update the WESMBill Summary
                    With itemWESMBillSummary
                        .BeginningBalance = DocumentAmount
                        .EndingBalance = DocumentAmount
                        .BillPeriod = CInt(frm.ddlBillingPeriod.SelectedValue)
                        .ChargeType = chargeType
                        .DueDate = CDate(FormatDateTime(frm.dtDueDate.Value, DateFormat.ShortDate))
                        .NewDueDate = CDate(FormatDateTime(frm.dtDueDate.Value, DateFormat.ShortDate))
                        .IDNumber = New AMParticipants(CStr(Me.txtIDNumber.Text))
                        .IDType = "P"
                        .SummaryType = EnumSummaryType.DMCM
                    End With

                    'Update the DMCM
                    itemDMCM.BillingPeriod = CInt(frm.ddlBillingPeriod.SelectedValue)
                    itemDMCM.DueDate = CDate(FormatDateTime(frm.dtDueDate.Value))
                    itemDMCM.ChargeType = chargeType

                    'Update flag
                    flag = True
                Else
                    Exit Sub
                End If
            End If

            'Generate Journal Voucher
            Dim itemJV = BFactory.GenerateManualDMCMJournalVoucher(itemDMCM, WBillHelper.GetSignatories("DMCM").First)

            'Generate GP posted
            Dim itemGP = BFactory.GenerateManualDMCMGPPosted(itemJV, itemDMCM)

            Dim ans = MsgBox("Do you really want to save the DMCM?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Save")

            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            WBillHelper.SaveManualDMCM(itemDMCM, itemWESMBillSummary, itemJV, itemGP, DocumentAmount, flag)

            MsgBox("Successfully Saved!", MsgBoxStyle.Information, "Saved")            

            'Updated By Lance 08/17/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", ("Manual DMCM Window").ToString, itemDMCM.DMCMNumber.ToString(), "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccesffullySaved.ToString, AMModule.UserName)

            Me.txtDMCMNo.Text = itemDMCM.DMCMNumber.ToString()
            Me.txtJVNo.Text = itemDMCM.JVNumber.ToString()

            Me.EnableControls(False, False, False, True, False, True, True)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Found")                       
        End Try
    End Sub

    Private Sub btnPrintDMCM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintDMCM.Click

        Try

            ProgressThread.Show("Please wait while preparing DMCM Report.")
            Dim listDMCMNo As New List(Of Long)
            Dim listDMCM As New List(Of DebitCreditMemo)

            'Get the Accounting Codes
            Dim listAccountCodes = WBillHelper.GetAccountingCodes()

            'Get the Participants
            Dim listParticipants = WBillHelper.GetAMParticipantsAll()

            'Get the Signatory
            Dim signatory = WBillHelper.GetSignatories("DMCM").First()

            'Get the DMCM
            listDMCM = WBillHelper.GetDebitCreditMemoMain(CLng(Me.txtDMCMNo.Text))

            'Add the DMCM Number
            listDMCMNo.Add(listDMCM.First.DMCMNumber)

            Dim dt = BFactory.GenerateDMCMReport(listDMCMNo, New DSReport.DebitCreditMemoDataTable, listDMCM, _
                                                 listAccountCodes, listParticipants, signatory)

            Dim frmViewer As New frmReportViewer()
            With frmViewer
                .LoadDebitCreditMemo(dt)
                ProgressThread.Close()
                .ShowDialog()
            End With
            
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)           
        End Try
    End Sub

    Private Sub btnPrintJV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintJV.Click
        Try
            ProgressThread.Show("Please wait while preparing JV.")
            'Get the Accounting Codes
            Dim listAccountCodes = WBillHelper.GetAccountingCodes()

            'Get the JV
            Dim itemJV = WBillHelper.GetJournalVoucher(CLng(Me.txtJVNo.Text)).First()

            'Insert the particulars
            itemJV.Remarks = Me.txtParticulars.Text.Trim()

            'Generate dataset for Journal voucher
            Dim ds = BFactory.GenerateJournalVoucherReport(listAccountCodes, itemJV, New DSReport.JournalVoucherDataTable, _
                                                           New DSReport.JournalVoucherDetailsDataTable, New DSReport.AccountingCodeDataTable)

            Dim frmViewer As New frmReportViewer()
            With frmViewer
                .LoadJournalVoucher(ds)
                ProgressThread.Close()
                .ShowDialog()
            End With
            
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)            
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtDMCMHeader_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
                                        txtMarketFees.LostFocus, txtVATonMarketFees.LostFocus, txtEnergy.LostFocus, txtVATonEnergy.LostFocus
        Me.FormatDMCMHeader()
    End Sub

    Private Sub DGridView_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGridView.CellEndEdit
        With Me.DGridView.Rows(e.RowIndex)
            Select Case e.ColumnIndex
                Case 3
                    If IsNumeric(.Cells("colDebit").Value) Then
                        .Cells("colDebit").Value = FormatNumber(.Cells("colDebit").Value, 2)
                    End If

                Case 4
                    If IsNumeric(.Cells("colCredit").Value) Then
                        .Cells("colCredit").Value = FormatNumber(.Cells("colCredit").Value, 2)
                    End If
            End Select
        End With
    End Sub

    Private Sub rbChargeType_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbMarketFees.CheckedChanged, _
                                           rbVATonMarketFees.CheckedChanged, rbEnergy.CheckedChanged, rbVATonEnergy.CheckedChanged

        Me.InitializeDMCMHeader()

        If Me.rbMarketFees.Checked Then
            Me.txtMarketFees.Enabled = True
        ElseIf Me.rbVATonMarketFees.Checked Then
            Me.txtVATonMarketFees.Enabled = True
        ElseIf Me.rbEnergy.Checked Then
            Me.txtEnergy.Enabled = True
        ElseIf Me.rbVATonEnergy.Checked Then
            Me.txtVATonEnergy.Enabled = True
        End If

        'Clear the grid
        Me.DGridView.Rows.Clear()
    End Sub

#Region "Methods/Functions"
    Private Sub EnableControls(ByVal gpMain As Boolean, ByVal gpDetails As Boolean, ByVal grid As Boolean, _
                               ByVal vNew As Boolean, ByVal vSave As Boolean, ByVal vPrintDMCM As Boolean, _
                               ByVal vPrintJV As Boolean)
        Me.gpMain.Enabled = gpMain
        Me.gpDetails.Enabled = gpDetails
        Me.DGridView.Enabled = grid
        Me.btnNew.Enabled = vNew
        Me.btnSave.Enabled = vSave
        Me.btnPrintDMCM.Enabled = vPrintDMCM
        Me.btnPrintJV.Enabled = vPrintJV
    End Sub

    Private Function GenerateDebitCreditMemo() As DebitCreditMemo
        Dim result As New DebitCreditMemo
        Dim Signatories = WBillHelper.GetSignatories("DMCM").First()

        With result
            .IDNumber = CStr(Me.txtIDNumber.Text)
            .Particulars = Me.txtParticulars.Text.Trim()
            .TransType = EnumDMCMTransactionType.ManualDMCM
            .EWT = 0
            .EWV = 0
            .Vatable = CDec(Me.txtMarketFees.Text)
            .VAT = CDec(Me.txtVATonMarketFees.Text)
            .VATExempt = CDec(Me.txtEnergy.Text) + CDec(Me.txtVATonEnergy.Text)
            .VatZeroRated = 0
            .TotalAmountDue = CDec(Me.txtMarketFees.Text) + CDec(Me.txtVATonMarketFees.Text) + _
                              CDec(Me.txtEnergy.Text) + CDec(Me.txtVATonEnergy.Text)
            .CheckedBy = Signatories.Signatory_1
            .ApprovedBy = Signatories.Signatory_2
        End With

        Dim listDetails As New List(Of DebitCreditMemoDetails)
        For index As Integer = 0 To Me.DGridView.RowCount - 2
            With Me.DGridView.Rows(index)
                listDetails.Add(New DebitCreditMemoDetails(1, CStr(.Cells("colAcctCode").Value), _
                                                           CDec(.Cells("colDebit").Value), CDec(.Cells("colCredit").Value), _
                                                           "", EnumSummaryType.INV, New AMParticipants(CStr(Me.txtIDNumber.Text))))
            End With
        Next
        listDetails.TrimExcess()
        result.DMCMDetails = listDetails

        Return result
    End Function

    Private Function VaidateGridView() As Boolean

        If Me.DGridView.RowCount = 0 Then
            MsgBox("Please specify the DMCM details in grid!", MsgBoxStyle.Critical, "Speciy the inputs")
            Return False
        End If

        For index As Integer = 0 To Me.DGridView.RowCount - 2
            With Me.DGridView.Rows(index)
                If CStr(.Cells("colAcctCode").Value).Trim.Length = 0 Then
                    MsgBox("Please specify the account code!", MsgBoxStyle.Critical, "Specify the inputs")
                    Return False

                ElseIf Not IsNumeric(.Cells("colDebit").Value) And Not IsNumeric(.Cells("colCredit").Value) Then
                    MsgBox("Debit/Credit must be numeric1", MsgBoxStyle.Critical, "Specify the inputs")
                    Return False
                Else

                End If

                If IsNumeric(Replace(CStr(.Cells("colDebit").Value), ",", "")) Then
                    If Not BFactory.CheckPrecisionAndScale(12, 2, Replace(CStr(.Cells("colDebit").Value), ",", "")) Then
                        MsgBox("Debit amount shall have a maximum of 12 whole number and 2 decimal places only!", MsgBoxStyle.Critical, _
                                                                                                             "Specify the inputs")
                        Return False
                    End If
                End If

                If IsNumeric(Replace(CStr(.Cells("colCredit").Value), ",", "")) Then
                    If Not BFactory.CheckPrecisionAndScale(12, 2, Replace(CStr(.Cells("colCredit").Value), ",", "")) Then
                        MsgBox("Credit amount shall have a maximum of 12 whole number and 2 decimal places only!", MsgBoxStyle.Critical, _
                                                                                                             "Specify the inputs")
                        Return False
                    End If
                End If

                If CDec(.Cells("colDebit").Value) < 0 Or CDec(.Cells("colCredit").Value) < 0 Then
                    MsgBox("Debit/Credit value must not be negative!", MsgBoxStyle.Critical, "Specify the inputs")
                    Return False

                ElseIf CDec(.Cells("colDebit").Value) = 0 And CDec(.Cells("colCredit").Value) = 0 Then
                    MsgBox("Either Debit/Credit must have a value!", MsgBoxStyle.Critical, "Specify the inputs")
                    Return False
                ElseIf CDec(.Cells("colDebit").Value) <> 0 And CDec(.Cells("colCredit").Value) <> 0 Then
                    MsgBox("Either Debit/Credit must be empty or equal to zero!", MsgBoxStyle.Critical, "Specify the inputs")
                    Return False
                End If
            End With
        Next

        Return True
    End Function

    Private Function ValidateControls() As Boolean
        If Me.ddlParticipantID.SelectedIndex = -1 Then
            MsgBox("Please specify the Participant ID!", MsgBoxStyle.Critical, "Specify the inputs")
            Me.ddlParticipantID.Select()
            Return False
        ElseIf Not IsNumeric(Me.txtMarketFees.Text) Then
            MsgBox("Market Fees must be numeric!", MsgBoxStyle.Critical, "Specify the inputs")
            Me.txtMarketFees.Select()
            Return False
        ElseIf Not IsNumeric(Me.txtVATonMarketFees.Text) Then
            MsgBox("VAT on Market Fees must be numeric!", MsgBoxStyle.Critical, "Specify the inputs")
            Me.txtVATonMarketFees.Select()
            Return False
        ElseIf Not IsNumeric(Me.txtEnergy.Text) Then
            MsgBox("Energy must be numeric!", MsgBoxStyle.Critical, "Specify the inputs")
            Me.txtEnergy.Select()
            Return False
        ElseIf Not IsNumeric(Me.txtVATonEnergy.Text) Then
            MsgBox("VAT on Energy must be numeric!", MsgBoxStyle.Critical, "Specify the inputs")
            Me.txtVATonEnergy.Select()
            Return False
        ElseIf CDec(Me.txtMarketFees.Text) < 0 Then
            MsgBox("Market Fees must not be negative!", MsgBoxStyle.Critical, "Specify the inputs")
            Me.txtMarketFees.Select()
            Return False
        ElseIf CDec(Me.txtVATonMarketFees.Text) < 0 Then
            MsgBox("VAT on Market Fees must not be negative!", MsgBoxStyle.Critical, "Specify the inputs")
            Me.txtVATonMarketFees.Select()
            Return False
        ElseIf CDec(Me.txtEnergy.Text) < 0 Then
            MsgBox("Energy must must not be negative!", MsgBoxStyle.Critical, "Specify the inputs")
            Me.txtEnergy.Select()
            Return False
        ElseIf CDec(Me.txtVATonEnergy.Text) < 0 Then
            MsgBox("VAT on Energy must not be negative!", MsgBoxStyle.Critical, "Specify the inputs")
            Me.txtVATonEnergy.Select()
            Return False
        ElseIf Not BFactory.CheckPrecisionAndScale(12, 2, Me.txtMarketFees.Text) Then
            MsgBox("Market Fees amount shall have a maximum of 12 whole number and 2 decimal places only!", MsgBoxStyle.Critical, _
                                                                                                 "Specify the inputs")
            Me.txtMarketFees.Select()
            Return False
        ElseIf Not BFactory.CheckPrecisionAndScale(12, 2, Me.txtVATonMarketFees.Text) Then
            MsgBox("VAT on Market Fees amount shall have a maximum of 12 whole number and 2 decimal places only!", MsgBoxStyle.Critical, _
                                                                                                 "Specify the inputs")
            Me.txtVATonMarketFees.Select()
            Return False
        ElseIf Not BFactory.CheckPrecisionAndScale(12, 2, Me.txtEnergy.Text) Then
            MsgBox("Energy amount shall have a maximum of 12 whole number and 2 decimal places only!", MsgBoxStyle.Critical, _
                                                                                                 "Specify the inputs")
            Me.txtEnergy.Select()
            Return False
        ElseIf Not BFactory.CheckPrecisionAndScale(12, 2, Me.txtVATonEnergy.Text) Then
            MsgBox("VAT on Energy amount shall have a maximum of 12 whole number and 2 decimal places only!", MsgBoxStyle.Critical, _
                                                                                                 "Specify the inputs")
            Me.txtVATonEnergy.Select()
            Return False
        ElseIf Me.DGridView.RowCount < 2 Then
            MsgBox("Please specify the DMCM details!", MsgBoxStyle.Critical, "Specify the inputs")
            Return False
        Else

        End If

        Return True
    End Function

    Private Sub ClearControls()
        Me.ddlParticipantID.SelectedIndex = -1
        Me.txtIDNumber.Text = ""
        Me.txtParticipantName.Text = ""
        Me.txtDMCMNo.Text = ""
        Me.txtJVNo.Text = ""
        Me.txtDate.Text = SystemDate.ToString("MM/dd/yyyy")
        Me.txtParticulars.Text = ""
        Me.DGridView.Rows.Clear()
    End Sub

    Private Sub FormatDMCMHeader()
        Try
            Me.txtMarketFees.Text = FormatNumber(Me.txtMarketFees.Text, 2)
            Me.txtVATonMarketFees.Text = FormatNumber(Me.txtVATonMarketFees.Text, 2)
            Me.txtEnergy.Text = FormatNumber(Me.txtEnergy.Text, 2)
            Me.txtVATonEnergy.Text = FormatNumber(Me.txtVATonEnergy.Text, 2)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub InitializeDMCMHeader()
        Me.txtMarketFees.Text = "0.00"
        Me.txtVATonMarketFees.Text = "0.00"
        Me.txtEnergy.Text = "0.00"
        Me.txtVATonEnergy.Text = "0.00"

        Me.txtMarketFees.Enabled = False
        Me.txtVATonMarketFees.Enabled = False
        Me.txtEnergy.Enabled = False
        Me.txtVATonEnergy.Enabled = False
    End Sub

    Private Function GetChargeType() As EnumChargeType
        Dim result As EnumChargeType

        If Me.rbMarketFees.Checked Then
            result = EnumChargeType.MF
        ElseIf Me.rbVATonMarketFees.Checked Then
            result = EnumChargeType.MFV
        ElseIf Me.rbEnergy.Checked Then
            result = EnumChargeType.E
        ElseIf Me.rbVATonEnergy.Checked Then
            result = EnumChargeType.EV
        End If

        Return result
    End Function

#End Region

End Class