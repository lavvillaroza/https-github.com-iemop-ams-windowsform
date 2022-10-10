'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmDebitCreditMemo
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     October 05, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI for viewing of existing debit/credit memo data. It also generates the Debit/Credit Memo Report.
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   October 05, 2011        Vladimir E. Espiritu            GUI initialization and viewing of existing records in grid.
'   October 06, 2011        Vladimir E. Espiritu            Added functionality to view the Debit/Credit Memo Report.
'


Option Explicit On
Option Strict On

Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmDebitCreditMemo
    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory
    Private dicChargeType As Dictionary(Of String, EnumChargeType)
    Private dicDMCMTransType As Dictionary(Of String, EnumDMCMTransactionType)
    Private ListParticipants As List(Of AMParticipants)
    Private ListDMCM As List(Of DebitCreditMemo)
    Private ListAccountCodes As List(Of AccountingCode)
    Private _Signatories As DocSignatories


    Private Sub frmDebitCreditMemo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm

        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"

        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName
        BFactory = BusinessFactory.GetInstance()

        'Get the billing participants
        Me.ListParticipants = WBillHelper.GetAMParticipantsAll()

        'Get the accounting codes
        ListAccountCodes = WBillHelper.GetAccountingCodes()

        'Add the items for Charge Type
        dicChargeType = New Dictionary(Of String, EnumChargeType)
        With dicChargeType
            .Add("ENERGY", EnumChargeType.E)
            .Add("ENERGY-VAT", EnumChargeType.EV)
            .Add("MARKET FEE", EnumChargeType.MF)
            .Add("MARKET FEE-VAT", EnumChargeType.MFV)
        End With

        'Add the items for DMCM Trans Type
        dicDMCMTransType = New Dictionary(Of String, EnumDMCMTransactionType)        
        With dicDMCMTransType
            .Add("ParentToParent/ChildToChild", EnumDMCMTransactionType.WESMBillP2PC2COffsetting)
            .Add("ParentToChild", EnumDMCMTransactionType.WESMBillP2COffsetting)
            .Add("SetupWithholdingTaxAndVATOnMF", EnumDMCMTransactionType.CollectionSetupWithholding)
            .Add("SetupWithholdingOnDefaultInterest", EnumDMCMTransactionType.CollectionSetupWithholdingDefaultInterest)
            .Add("SetupDefaultInterestOnMFAndVAT", EnumDMCMTransactionType.CollectionSetupMFandMFVatDefaultInterest)
            .Add("SetupDefaultInterestOnEnergy", EnumDMCMTransactionType.CollectionSetupEnergyDefaultInterest)
            .Add("Drawdown", EnumDMCMTransactionType.CollectionDrawdown)
            .Add("SetupClearingOnMFAndMFV", EnumDMCMTransactionType.PaymentSetupClearingOnMFAndMFV)
            .Add("SetupClearingOnEWTAndWVAT", EnumDMCMTransactionType.PaymentSetupClearingOnEWTAndWVAT)
            .Add("SetupClearingOnMFAndMFVDefaultInterest", EnumDMCMTransactionType.PaymentSetupClearingOnMFAndMFVDefaultInterest)
            .Add("SetupClearingOnEWTAndWVATDefaultInterest", EnumDMCMTransactionType.PaymentSetupClearingOnEWTAndWVATDefaultInterest)
            .Add("SetupClearingAROnEnergyAndDefaultInterest", EnumDMCMTransactionType.PaymentSetupClearingAROnEnergyAndDefaultInterest)
            .Add("SetupClearingAROnVATonEnergy", EnumDMCMTransactionType.PaymentSetupClearingAROnVATonEnergy)
            .Add("SetupOffsettingAROfDefaultInterestOnEnergy", EnumDMCMTransactionType.PaymentSetupOffsettingAROfDefaultInterestOnEnergy)
            .Add("SetupClearingAPOfEnergyAndDefaultInterest", EnumDMCMTransactionType.PaymentSetupClearingAPOfEnergyAndDefaultInterest)
            .Add("SetupClearingAPOfVATonEnergy", EnumDMCMTransactionType.PaymentSetupClearingAPOfVATonEnergy)
            .Add("SetupOffsettingAPOfDefaultInterestOnEnergy", EnumDMCMTransactionType.PaymentSetupOffsettingAPOfDefaultInterestOnEnergy)
            .Add("SetupForMFAPPaidByIEMOPAccount", EnumDMCMTransactionType.PaymentSetupForMFAPPaidByIEMOPAccount)
            .Add("ClosingAccountReceivable", EnumDMCMTransactionType.SPAClosingAccountReceivable)
            .Add("ClosingAccountPayable", EnumDMCMTransactionType.SPAClosingAccountPayable)
            .Add("SetupAccountReceivable", EnumDMCMTransactionType.SPASetupAccountReceivable)
            .Add("SetupAccountPayable", EnumDMCMTransactionType.SPASetupAccountPayable)
            .Add("SetupClearingAPOnEnergy", EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAPOnEnergy)
            .Add("SetupClearingAROnEnergy", EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAROnEnergy)
            .Add("SetupClearingAPOnMarketFees", EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAPOnMarketFees)
            .Add("SetupClearingAROnEnergyAddBack", EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAROnEnergyAddBack)
            .Add("SetupClearingAPOnEnergyAddBack", EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAPOnEnergyAddBack)
        End With

    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click        

        Try
            '  Validation
            If Me.DGridViewMain.Rows.Count = 0 Then
                MsgBox("No records to view!", MsgBoxStyle.Information, "No data")
                Exit Sub
            End If

            Dim listDMCMNo As New List(Of Long)

            'Get the DMCM No
            Dim itemDMCMNo = CLng(Me.DGridViewMain.CurrentRow.Cells("DMCM_No2").Value)

            'Get the Accounting Codes
            Dim listAccountCodes = WBillHelper.GetAccountingCodes()

            'Get the Participants
            Dim listParticipants = WBillHelper.GetAMParticipantsAll()

            'Get the Signatory
            Dim signatory = WBillHelper.GetSignatories("DMCM").First()

            'Get the DMCM
            Dim listDMCMMain = WBillHelper.GetDebitCreditMemoMain(itemDMCMNo)

            'Get the WESM Bill Sales and Purchase
            Dim listWESMBillSalesAndPurchased = WBillHelper.GetWESMInvoiceSalesAndPurchasedWithDMCM(CLng(itemDMCMNo))

            'Add the DMCM Number
            listDMCMNo.Add(itemDMCMNo)

            Dim dt = BFactory.GenerateDMCMReport1(listDMCMNo, New DSReport.DebitCreditMemoDataTable, listDMCMMain, _
                                                  listAccountCodes, listParticipants, signatory, listWESMBillSalesAndPurchased)

            'Get the datasource for the report
            Dim frmViewer As New frmReportViewer()
            With frmViewer
                .LoadDebitCreditMemo(dt)
                ProgressThread.Close()
                .ShowDialog()
            End With

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Updated By Lance 08/18/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepDebitCreditMemoWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInGeneratingReport.ToString, AMModule.UserName)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub DGridViewMain_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGridViewMain.CellClick
        If e.RowIndex = -1 Then
            Exit Sub
        End If

        Me.ViewDetails()
    End Sub

    Private Sub btnSearchDMCM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDMCM.Click
        Me.SearchDMCMNo()
    End Sub

    Private Sub txtDMCMNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDMCMNo.GotFocus
        Me.txtDMCMNo.Text = ""
        Me.txtDMCMNo.Font = New System.Drawing.Font("Helvetica", 8.5, FontStyle.Regular)
        Me.txtDMCMNo.ForeColor = Color.Black
    End Sub

    Private Sub txtDMCMNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDMCMNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Me.SearchDMCMNo()
        End If
    End Sub

    Private Sub txtDMCMNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDMCMNo.LostFocus
        If txtDMCMNo.Text.Trim.Length = 0 Then
            Me.txtDMCMNo.Text = "Type DMCM No. here"
            Me.txtDMCMNo.Font = New System.Drawing.Font("Helvetica", 8.5, FontStyle.Italic)
            Me.txtDMCMNo.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub btnAdvanceSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdvanceSearch.Click        
        Try
            Dim frm As New frmDebitCreditMemoSearch

            If frm.ShowDialog <> Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If

            Dim tmpDMCMList As New List(Of DebitCreditMemo)

            With frm
                Dim listTransType As New List(Of EnumDMCMTransactionType)
                Dim dateFrom As Date? = Nothing, dateTo As Date? = Nothing, dueDate As Date? = Nothing
                Dim billingPeriod As Integer? = Nothing, IDNumber As String = ""
                Dim chargeType As EnumChargeType? = Nothing

                If .dtFrom.Enabled And .dtTo.Enabled Then
                    dateFrom = CDate(FormatDateTime(.dtFrom.Value, DateFormat.ShortDate))
                    dateTo = CDate(FormatDateTime(.dtTo.Value, DateFormat.ShortDate))
                End If

                If .ddlBillingPeriod.Enabled Then
                    billingPeriod = CInt(.ddlBillingPeriod.Text)
                End If

                If .ddlDueDate.Enabled Then
                    dueDate = CDate(.ddlDueDate.Text)
                End If

                If .ddlChargeType.Enabled Then
                    chargeType = Me.dicChargeType(.ddlChargeType.Text)
                End If

                If .ddlParticipant.Enabled Then
                    IDNumber = CStr(.ddlParticipant.SelectedValue)
                End If

                If .chckTransType.Enabled Then
                    For index As Integer = 0 To .chckTransType.Items.Count - 1
                        If .chckTransType.GetItemCheckState(index) = CheckState.Checked Then
                            listTransType.Add(dicDMCMTransType(.chckTransType.Items.Item(index).ToString()))
                        End If
                    Next
                Else
                    If frm.rbPrudential.Checked Then
                        listTransType.Add(EnumDMCMTransactionType.PrudentialInterest)
                    End If

                    If frm.rbSettlement.Checked Then
                        listTransType.Add(EnumDMCMTransactionType.STLInterest)
                    End If

                    If frm.rbSPA.Checked Then
                        listTransType.Add(EnumDMCMTransactionType.ManualDMCM)
                    End If
                End If

                'Get the list of Debit/Credit Memo
                    tmpDMCMList = WBillHelper.GetDebitCreditMemoMain(listTransType, dateFrom, dateTo, billingPeriod, dueDate, chargeType, IDNumber)
            End With

            If tmpDMCMList.Count = 0 Then
                MsgBox("No records found!", MsgBoxStyle.Exclamation, "No data")
                Exit Sub
            End If

            Me.ListDMCM = tmpDMCMList

            Me.DGridViewMain.Rows.Clear()
            Me.DGridViewDetails.Rows.Clear()

            'Load the DMCM into list
            Me.LoadDMCMToGrid()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Contact Administrator")
            'Updated By Lance 08/18/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepDebitCreditMemoWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInSearching.ToString, AMModule.UserName)
        End Try
    End Sub

    Private Sub DGridViewMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
                                  Handles DGridViewMain.KeyDown, DGridViewMain.KeyUp

        If Me.DGridViewMain.RowCount = 0 Then
            Exit Sub
        End If

        Me.ViewDetails()
    End Sub

#Region "Functions/Method"

    Private Sub LoadDMCMToGrid()
        Me.DGridViewMain.Rows.Clear()

        'Join the Debit/Credit Memo table into Bill Participants table
        Dim items = (From x In Me.ListDMCM Join y In Me.ListParticipants On x.IDNumber Equals y.IDNumber _
                     Select x.DMCMNumber, x.JVNumber, y.ParticipantID, x.Particulars, x.PreparedBy, _
                            x.CheckedBy, x.ApprovedBy, x.UpdatedDate).ToList()

        'Put the Main Debit Credit Memo in grid
        For Each item In items
            With item
                Me.DGridViewMain.Rows.Add(.DMCMNumber, .JVNumber, AMModule.DMCMNumberPrefix & .DMCMNumber.ToString("0000000"), AMModule.JVNumberPrefix & .JVNumber.ToString("0000000"), .ParticipantID, .Particulars, _
                                          .PreparedBy, .CheckedBy, _
                                          .ApprovedBy, .UpdatedDate)
            End With
        Next

        'Put the details of the first row selected in Debit/Credit Memo Main grid
        If items.Count > 0 Then
            Me.DGridViewDetails.Rows.Clear()
            Dim dmcm As Long = items(0).DMCMNumber
            Dim tmpMain = (From x In ListDMCM Where x.DMCMNumber = dmcm Select x).FirstOrDefault()

            'Dim itemDetails = (From x In tmpMain.DMCMDetails Join y In ListAccountCodes _
            '                   On x.AccountCode Equals y.AccountCode Join z In Me.ListParticipants _
            '                   On x.IDNumber.IDNumber Equals z.IDNumber _
            '                   Select x.IDNumber.ParticipantID, x.AccountCode, x.SummaryType, x.InvDMCMNo, _
            '                   y.Description, x.Debit, x.Credit _
            '                   Order By AccountCode).ToList()

            Dim itemDetails = (From x In tmpMain.DMCMDetails Join y In ListAccountCodes _
                               On x.AccountCode Equals y.AccountCode _
                               Select x.IDNumber.ParticipantID, x.AccountCode, x.SummaryType, x.InvDMCMNo, _
                               y.Description, x.Debit, x.Credit _
                               Order By AccountCode).ToList()

            For Each item In itemDetails
                With item
                    Me.DGridViewDetails.Rows.Add(.ParticipantID, .AccountCode, .SummaryType, .InvDMCMNo, _
                                                 .Description, FormatNumber(.Debit, 2), FormatNumber(.Credit, 2))
                End With
            Next
        End If
    End Sub

    Private Sub SearchDMCMNo()
        Try
            If Not IsNumeric(Me.txtDMCMNo.Text) Or Me.txtDMCMNo.Text.Contains(".") Then
                MsgBox("DMCM Number should be numeric!", MsgBoxStyle.Exclamation, "Specify the inputs")
                Me.txtDMCMNo.Select()
                Me.txtDMCMNo.Text = ""
                Exit Sub
            ElseIf CInt(Me.txtDMCMNo.Text) < 0 Then
                MsgBox("DMCM Number should not be negative!", MsgBoxStyle.Exclamation, "Specify the inputs")
                Me.txtDMCMNo.Text = ""
                Me.txtDMCMNo.Select()
                Exit Sub
            End If

            Dim DMCMNo = CLng(Me.txtDMCMNo.Text.Trim)

            Dim tmpDMCMList = WBillHelper.GetDebitCreditMemoMain(DMCMNo)

            If tmpDMCMList.Count = 0 Then
                MsgBox("The specify DMCM No. does not exist!", MsgBoxStyle.Information, "Not found")
                Me.txtDMCMNo.Text = ""
                Me.txtDMCMNo.Select()
                Exit Sub
            Else
                Me.ListDMCM = tmpDMCMList
                Me.LoadDMCMToGrid()
            End If

            Me.txtDMCMNo.Text = "Type DMCM No. here"
            Me.txtDMCMNo.Font = New System.Drawing.Font("Helvetica", 8.5, FontStyle.Italic)
            Me.txtDMCMNo.ForeColor = Color.Gray
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ViewDetails()
        Me.DGridViewDetails.Rows.Clear()
        'Get the DMCM Number        
        Dim dmcm As Integer = CInt(Me.DGridViewMain.CurrentRow.Cells(0).Value)

        'Get the Main
        Dim itemMain = (From x In Me.ListDMCM Where x.DMCMNumber = dmcm Select x).FirstOrDefault()

        'Get the Details of the main
        'Dim itemDetails = (From x In itemMain.DMCMDetails Join y In ListAccountCodes _
        '                   On x.AccountCode Equals y.AccountCode _
        '                   Join z In Me.ListParticipants On x.IDNumber.IDNumber Equals z.IDNumber _
        '                   Select z.ParticipantID, x.AccountCode, x.SummaryType, x.InvDMCMNo, y.Description, x.Debit, x.Credit _
        '                   Order By AccountCode).ToList()

        Dim itemDetails = (From x In itemMain.DMCMDetails Join y In ListAccountCodes _
                              On x.AccountCode Equals y.AccountCode _
                              Select x.IDNumber.ParticipantID, x.AccountCode, x.SummaryType, x.InvDMCMNo, _
                              y.Description, x.Debit, x.Credit _
                              Order By AccountCode).ToList()

        'Put the items in Grid
        For Each item In itemDetails
            With item
                Me.DGridViewDetails.Rows.Add(.ParticipantID, .AccountCode, .SummaryType, .InvDMCMNo, _
                                             .Description, FormatNumber(.Debit, 2), FormatNumber(.Credit, 2))
            End With
        Next
    End Sub
#End Region


  
End Class