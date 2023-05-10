'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmDebitCreditMemoSearch
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     July 16, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for the maintenance of Collection
'Arguments/Parameters:  
'Files/Database Tables:  AM_DMCM and AM_DMCM_DETAILS
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   July 16, 2012           Vladimir E. Espiritu                 GUI design and basic functionalities   
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmDebitCreditMemoSearch

    Private WBillHelper As WESMBillHelper
    Private ListBP As Dictionary(Of Integer, List(Of String))
    Private ListParticipants As List(Of AMParticipants)
    Private ListAccountCodes As List(Of AccountingCode)

    Private Sub frmDebitCreditMemoSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        'Get the billing participants
        Me.ListParticipants = WBillHelper.GetAMParticipantsAll()

        'Get the accounting codes
        ListAccountCodes = WBillHelper.GetAccountingCodes()

        With Me.ddlParticipant
            .DataSource = ListParticipants
            .DisplayMember = "ParticipantID"
            .ValueMember = "IDNumber"
            .SelectedIndex = -1
        End With

        'Clear/Disable inputs
        Me.ClearInputs()

        AddHandler Me.ddlBillingPeriod.SelectedIndexChanged, AddressOf Me.ddlBillingPeriod_SelectedIndexChanged
        AddHandler Me.rbOffsetting.CheckedChanged, AddressOf Me.EnableControls
        AddHandler Me.rbCollection.CheckedChanged, AddressOf Me.EnableControls
        AddHandler Me.rbPayment.CheckedChanged, AddressOf Me.EnableControls
        AddHandler Me.rbPrudential.CheckedChanged, AddressOf Me.EnableControls        
        AddHandler Me.rbSettlement.CheckedChanged, AddressOf Me.EnableControls
        AddHandler Me.rbSPA.CheckedChanged, AddressOf Me.EnableControls
        AddHandler Me.rbwthtax.CheckedChanged, AddressOf Me.EnableControls
    End Sub

    Private Sub EnableControls(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.chckTransType.Items.Clear()
        Me.chckTransType.Enabled = True
        Me.dtFrom.Enabled = True
        Me.dtTo.Enabled = True
        Me.ddlBillingPeriod.Enabled = True
        Me.ddlDueDate.Enabled = True
        Me.ddlChargeType.Enabled = True
        Me.chckParticipantID.Enabled = True
        Me.chckParticipantID.Checked = True
        Me.ddlBillingPeriod.SelectedIndex = -1
        Me.ddlDueDate.SelectedIndex = -1
        Me.ddlChargeType.SelectedIndex = -1
        Me.ddlParticipant.SelectedIndex = -1

        If Me.rbOffsetting.Checked Then
            Me.chckTransType.Items.Add("ParentToParent/ChildToChild", True)
            Me.chckTransType.Items.Add("ParentToChild", True)
            Me.dtFrom.Enabled = False
            Me.dtTo.Enabled = False
            Me.chckParticipantID.Checked = False

            'Load the billing period
            Me.LoadBillingPeriod()

        ElseIf Me.rbCollection.Checked Then
            With Me.chckTransType.Items
                .Add("SetupWithholdingTaxAndVATOnMF", True)
                .Add("SetupWithholdingOnDefaultInterest", True)
                .Add("SetupDefaultInterestOnMFAndVAT", True)
                .Add("SetupDefaultInterestOnEnergy", True)
                .Add("Drawdown", True)
            End With
            Me.ddlBillingPeriod.Enabled = False
            Me.ddlDueDate.Enabled = False
            Me.ddlChargeType.Enabled = False
            Me.chckParticipantID.Checked = False

        ElseIf Me.rbPayment.Checked Then
            With Me.chckTransType.Items
                .Add("SetupClearingOnMFAndMFV", True)
                .Add("SetupClearingOnEWTAndWVAT", True)
                .Add("SetupClearingOnMFAndMFVDefaultInterest", True)
                .Add("SetupClearingOnEWTAndWVATDefaultInterest", True)
                .Add("SetupClearingAROnEnergyAndDefaultInterest", True)
                .Add("SetupClearingAROnVATonEnergy", True)
                .Add("SetupOffsettingAROfDefaultInterestOnEnergy", True)
                .Add("SetupClearingAPOfEnergyAndDefaultInterest", True)
                .Add("SetupClearingAPOfVATonEnergy", True)
                .Add("SetupOffsettingAPOfDefaultInterestOnEnergy", True)
                .Add("SetupForMFAPPaidByIEMOPAccount", True)
            End With

            Me.ddlBillingPeriod.Enabled = False
            Me.ddlDueDate.Enabled = False
            Me.ddlChargeType.Enabled = False
            Me.chckParticipantID.Checked = False

        ElseIf Me.rbPrudential.Checked Then
            Me.chckTransType.Items.Add("--- Not Applicable ---")
            Me.chckTransType.Enabled = False
            Me.ddlBillingPeriod.Enabled = False
            Me.ddlDueDate.Enabled = False
            Me.ddlChargeType.Enabled = False
            Me.chckParticipantID.Checked = False

        ElseIf Me.rbSettlement.Checked Then
            Me.chckTransType.Items.Add("--- Not Applicable ---")
            Me.chckTransType.Enabled = False
            Me.ddlBillingPeriod.Enabled = False
            Me.ddlDueDate.Enabled = False
            Me.ddlChargeType.Enabled = False
            Me.chckParticipantID.Checked = False

        ElseIf Me.rbSPA.Checked Then
            With Me.chckTransType.Items
                .Add("ClosingAccountReceivable", True)
                .Add("ClosingAccountPayable", True)
                .Add("SetupAccountReceivable", True)
                .Add("SetupAccountPayable", True)
            End With

            Me.ddlBillingPeriod.Enabled = False
            Me.ddlDueDate.Enabled = False
            Me.ddlChargeType.Enabled = False
            Me.chckParticipantID.Checked = False
        ElseIf Me.rbwthtax.Checked Then
            With Me.chckTransType.Items
                .Add("SetupClearingAPOnEnergy", True)
                .Add("SetupClearingAROnEnergy", True)
                .Add("SetupClearingAPOnMarketFees", True)
                .Add("SetupClearingAROnEnergyAddBack", True)
                .Add("SetupClearingAPOnEnergyAddBack", True)
            End With

            Me.ddlBillingPeriod.Enabled = False
            Me.ddlDueDate.Enabled = False
            Me.ddlChargeType.Enabled = False
            Me.chckParticipantID.Checked = False
        End If

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If Not Me.Validations() Then
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub ddlBillingPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.ddlBillingPeriod.Items.Count = 0 Or Me.ddlBillingPeriod.SelectedIndex = -1 Then
            Me.ddlDueDate.DataSource = Nothing
            Exit Sub
        End If

        'Get the due dates for the selected billing period
        Dim listDueDate = ListBP(CInt(Me.ddlBillingPeriod.Text))

        'Set the datasource
        Me.ddlDueDate.DataSource = listDueDate
        If listDueDate.Count > 1 Then
            Me.ddlDueDate.SelectedIndex = -1
        ElseIf listDueDate.Count = 1 Then
            Me.ddlDueDate.SelectedIndex = 0
        Else
            Exit Sub
        End If
    End Sub

    Private Sub ddlDueDate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlDueDate.SelectedIndexChanged
        If Me.ddlBillingPeriod.SelectedIndex = -1 Or Me.ddlDueDate.SelectedIndex = -1 Then
            Exit Sub
        End If

        Try
            Dim billingperiod As Integer = CInt(Me.ddlBillingPeriod.Text)
            Dim duedate As Date = CDate(Me.ddlDueDate.Text)

            'Get only the existing charge type for the selected billing period
            Dim listCharge = WBillHelper.GetChargeTypeDebitCreditMemoForOffsetting(billingperiod, duedate)
            listCharge = (From x In listCharge Select x Distinct).ToList()
            Me.ddlChargeType.DataSource = listCharge

            If listCharge.Count > 1 Then
                Me.ddlChargeType.SelectedIndex = -1
            ElseIf listCharge.Count = 1 Then
                Me.ddlChargeType.SelectedIndex = 0
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Contact Administrator")
        End Try
    End Sub

    Private Sub chckParticipantID_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chckParticipantID.CheckedChanged
        Me.ddlParticipant.SelectedIndex = -1
        If Me.chckParticipantID.Checked Then
            Me.ddlParticipant.Enabled = True
        Else
            Me.ddlParticipant.Enabled = False
        End If

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.ClearInputs()
    End Sub

#Region "Methods/Functions"
    Private Sub LoadBillingPeriod()
        'Get the existing billing periods from Debit/Credit memo table
        ListBP = WBillHelper.GetBillingPeriodDebitCreditMemoForOffsetting()
        Dim listBPs = (From x In ListBP Select x.Key).ToList()

        'Set the datasource
        Me.ddlBillingPeriod.DataSource = listBPs
        Me.ddlBillingPeriod.SelectedIndex = -1
    End Sub

    Private Function Validations() As Boolean
        If Not Me.rbOffsetting.Checked And Not Me.rbCollection.Checked And Not Me.rbPayment.Checked _
            And Not Me.rbPrudential.Checked And Not Me.rbSPA.Checked And Not Me.rbSettlement.Checked And Not Me.rbwthtax.Checked Then
            MsgBox("Please select the DMCM Type!", MsgBoxStyle.Exclamation, "Specify the inputs")
            Return False
        End If

        If Not Me.rbPrudential.Checked And Not Me.rbSPA.Checked And Not Me.rbwthtax.Checked And Not Me.rbSettlement.Checked Then
            If Me.chckTransType.CheckedItems.Count = 0 Then
                MsgBox("Please select the Transaction Type!", MsgBoxStyle.Exclamation, "Specify the inputs")
                Return False
            End If
        End If

        If Me.dtFrom.Enabled And Me.dtTo.Enabled Then
            If CDate(FormatDateTime(Me.dtFrom.Value, DateFormat.ShortDate)) > CDate(FormatDateTime(Me.dtTo.Value, DateFormat.ShortDate)) Then
                MsgBox("Invalid date range!", MsgBoxStyle.Exclamation, "Specify the inputs")
                Me.dtFrom.Select()

                Return False
            End If
        End If

        If Me.ddlBillingPeriod.Enabled And Me.ddlBillingPeriod.SelectedIndex = -1 Then
            MsgBox("Please select the Billing Period!", MsgBoxStyle.Exclamation, "Specify the inputs")
            Me.ddlBillingPeriod.Select()
            Return False

        ElseIf Me.ddlDueDate.Enabled And Me.ddlDueDate.SelectedIndex = -1 Then
            MsgBox("Please select the Due Date!", MsgBoxStyle.Exclamation, "Specify the inputs")
            Me.ddlDueDate.Select()
            Return False

        ElseIf Me.ddlChargeType.Enabled And Me.ddlChargeType.SelectedIndex = -1 Then
            MsgBox("Please select the Charge Type!", MsgBoxStyle.Exclamation, "Specify the inputs")
            Me.ddlChargeType.Select()
            Return False

        ElseIf Me.ddlParticipant.Enabled And Me.ddlParticipant.SelectedIndex = -1 Then
            MsgBox("Please select the Participant ID!", MsgBoxStyle.Exclamation, "Specify the inputs")
            Me.ddlParticipant.Select()
            Return False
        End If

        If Me.rbPayment.Checked And Me.ddlParticipant.SelectedIndex = -1 Then
            MsgBox("Please select the Participant ID!", MsgBoxStyle.Exclamation, "Specify the inputs")
            Me.ddlParticipant.Select()
            Return False
        End If

        Return True
    End Function

    Private Sub ClearInputs()
        Me.rbOffsetting.Checked = False
        Me.rbCollection.Checked = False
        Me.rbPayment.Checked = False
        Me.rbPrudential.Checked = False
        Me.rbwthtax.Checked = False
        Me.rbSettlement.Checked = False
        Me.rbSPA.Checked = False
        Me.chckTransType.Items.Clear()
        Me.dtFrom.Value = Now()
        Me.dtTo.Value = Now()
        Me.dtFrom.Enabled = False
        Me.dtTo.Enabled = False
        Me.ddlBillingPeriod.SelectedIndex = -1
        Me.ddlDueDate.SelectedIndex = -1
        Me.ddlChargeType.SelectedIndex = -1
        Me.ddlParticipant.SelectedIndex = -1
        Me.ddlParticipant.Enabled = False
        Me.chckParticipantID.Checked = False
        Me.chckTransType.Enabled = False
        Me.ddlBillingPeriod.Enabled = False
        Me.ddlDueDate.Enabled = False
        Me.ddlChargeType.Enabled = False
        Me.chckParticipantID.Enabled = False
    End Sub

#End Region

End Class