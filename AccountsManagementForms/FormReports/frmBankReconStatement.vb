'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmBankReconStatement
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     February 18, 2013
'Development Group:      Software Development and Support Division
'Description:            GUI for the Viewing and Generation of Bank REcon Statement
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'
'

Option Strict On
Option Explicit On

Imports AccountsManagementObjects
Imports AccountsManagementForms
Imports AccountsManagementLogic
Imports WESMLib.Auth.Lib

'Imports LDAPLib
'Imports LDAPLogin

Public Class frmBankReconStatement
    Private WbillHelper As WESMBillHelper
    Private BFactory As BusinessFactory
    Private genTransactions As New List(Of BankReconMain)
    Private TotalNSS As Decimal
    Private TotalSTL As Decimal
    Private TotalPR As Decimal

    Private Sub frmBankReconStatement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Me.MdiParent = MainForm
            WbillHelper = WESMBillHelper.GetInstance()
            WbillHelper.ConnectionString = AMModule.ConnectionString
            WbillHelper.UserName = AMModule.UserName

            BFactory = BusinessFactory.GetInstance

            Me.cbo_MonthSelected.SelectedIndex = 0
            Me.nud_Year.Value = CInt(Year(SystemDate))
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Encountered")
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Close.Click
        Me.Close()
    End Sub

    Private Sub cmd_Generate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Generate.Click
        Try
            Dim curDate As New Date
            'Dim isSameMonth As Boolean = False
            curDate = SystemDate
            curDate = DateAdd(DateInterval.Day, (curDate.Day - 1) * -1, curDate)

            Me.txt_stlBegBalance.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_stlBookEndBalance.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_stlBookAdjusted.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_stlBankAdjusted.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_stlBookCol.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_stlBookCheck.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_stlBookEFT.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_stlBookNSSPay.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_stlBookMF.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_stlBookPR.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_stlBookInterest.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_stlBankCheck.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_stlBookCharges.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_stlBookNSS.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_stlBookPRRep.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_stlBankEndBalance.Text = FormatNumber(0, 2, TriState.True, TriState.True)

            Me.txt_PRBookCol.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_PRBookDDown.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_PRBookPay.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_PRBookPrudential.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_PRBookInterest.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_PRBankCheck.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_PRBookCharges.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_PRBankEndBalance.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_PRBegBalance.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_PRBookEndBalance.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_PRBookAdjusted.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_PRBankAdjusted.Text = FormatNumber(0, 2, TriState.True, TriState.True)


            Me.txt_NSSBookPay.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_NSSBookCol.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_NSSBookInterest.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_NSSBookNSS.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_NSSBankEndBal.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_NSSBegBalance.Text = FormatNumber(0, 2, TriState.True, TriState.True)
            Me.txt_NSSBookEndBal.Text = FormatNumber(0, 2, TriState.True, TriState.True)

            If chk_Search.Checked Then
                genTransactions = WbillHelper.GetBankRecon(Me.cbo_MonthSelected.SelectedIndex + 1, CInt(Me.nud_Year.Value))

                If genTransactions.Count = 0 Then
                    MsgBox("No Records found for the selected Month and Year.", MsgBoxStyle.Exclamation, "No Records Found")
                    Me.chk_Search.Checked = False
                    Exit Sub
                End If

                Me.Text = "Bank Recon Statement for - " & Me.cbo_MonthSelected.SelectedItem.ToString & "-" & CInt(Me.nud_Year.Value)


                Dim _MaxDate = (From x In genTransactions _
                                Select x.TransactionDate Order By TransactionDate Descending).FirstOrDefault

                genTransactions = (From x In genTransactions _
                                  Where x.TransactionDate = _MaxDate _
                                  Select x).ToList

                'If genTransactions.FirstOrDefault.Status = EnumStatus.InActive Then
                cmd_Save.Enabled = False

                txt_stlBankEndBalance.ReadOnly = True
                txt_stlBookCharges.ReadOnly = True
                txt_stlBegBalance.ReadOnly = True

                txt_PRBankEndBalance.ReadOnly = True
                txt_PRBookCharges.ReadOnly = True
                txt_PRBegBalance.ReadOnly = True

                txt_NSSBankEndBal.ReadOnly = True
                txt_NSSBegBalance.ReadOnly = True
                'Else
                '    cmd_Save.Enabled = True
                '    cmd_Save.Text = "Update"

                '    txt_stlBankEndBalance.ReadOnly = False
                '    txt_stlBookCharges.ReadOnly = False

                '    txt_PRBankEndBalance.ReadOnly = False
                '    txt_PRBookCharges.ReadOnly = False

                '    txt_NSSBankEndBal.ReadOnly = False
                'End If
            Else
                Dim _tmpTransactions As New List(Of BankReconMain)
                Dim _month As String
                Dim _year As Integer

                Me.Text = "Bank Recon Statement for - " & MonthName(SystemDate.Month) & "-" & SystemDate.Year.ToString

                If Month(SystemDate) = 1 Then
                    _tmpTransactions = WbillHelper.GetBankRecon(12, CInt(Year(SystemDate) - 1))
                    _month = "December"
                    _year = CInt(Year(SystemDate) - 1)
                Else
                    _tmpTransactions = WbillHelper.GetBankRecon(Month(SystemDate) - 1, CInt(Year(SystemDate)))
                    _month = Month(SystemDate).ToString
                    _year = CInt(Year(SystemDate))
                End If

                If _tmpTransactions.Count = 0 Then
                    'Check Today's Transaction
                    _tmpTransactions = WbillHelper.GetBankRecon(Month(SystemDate), CInt(Year(SystemDate)))


                    Dim _MaxDate = (From x In _tmpTransactions _
                                    Select x.TransactionDate Order By TransactionDate Descending).FirstOrDefault

                    _tmpTransactions = (From x In _tmpTransactions _
                                      Where x.TransactionDate = _MaxDate _
                                      Select x).ToList

                    'If _tmpTransactions.Count <> 0 Then
                    'isSameMonth = True
                    For Each itmTransaction In _tmpTransactions
                        Select Case itmTransaction.BankReconType
                            Case EnumBankReconType.Settlement
                                Me.txt_stlBegBalance.Text = FormatNumber(itmTransaction.BeginningBalance, 2, TriState.True, TriState.True)
                                Me.txt_stlBankEndBalance.Text = FormatNumber(itmTransaction.BankEndingBalance, 2, TriState.True, TriState.True)
                            Case EnumBankReconType.NSS
                                Me.txt_NSSBegBalance.Text = FormatNumber(itmTransaction.BeginningBalance, 2, TriState.True, TriState.True)
                                Me.txt_NSSBankEndBal.Text = FormatNumber(itmTransaction.BankEndingBalance, 2, TriState.True, TriState.True)
                            Case EnumBankReconType.Prudential
                                Me.txt_PRBegBalance.Text = FormatNumber(itmTransaction.BeginningBalance, 2, TriState.True, TriState.True)
                                Me.txt_PRBankEndBalance.Text = FormatNumber(itmTransaction.BankEndingBalance, 2, TriState.True, TriState.True)
                        End Select
                    Next
                    'Else
                    Me.txt_stlBegBalance.ReadOnly = False
                    Me.txt_stlBookCharges.ReadOnly = False
                    Me.txt_PRBegBalance.ReadOnly = False
                    Me.txt_PRBookCharges.ReadOnly = False
                    Me.txt_NSSBegBalance.ReadOnly = False

                    Me.txt_stlBankEndBalance.ReadOnly = False
                    Me.txt_PRBankEndBalance.ReadOnly = False
                    Me.txt_NSSBankEndBal.ReadOnly = False
                    ' End If


                Else
                    For Each itmTransactions In _tmpTransactions.OrderBy(Function(x) x.TransactionDate)
                        With itmTransactions
                            Select Case .BankReconType
                                Case EnumBankReconType.Settlement
                                    Me.txt_stlBegBalance.Text = FormatNumber(.BookEndingbalance, 2, TriState.True, TriState.True)
                                    Me.txt_stlBankEndBalance.Text = FormatNumber(.BankEndingBalance, 2, TriState.True, TriState.True)
                                Case EnumBankReconType.Prudential
                                    Me.txt_PRBegBalance.Text = FormatNumber(.BookEndingbalance, 2, TriState.True, TriState.True)
                                    Me.txt_PRBankEndBalance.Text = FormatNumber(.BankEndingBalance, 2, TriState.True, TriState.True)
                                Case EnumBankReconType.NSS
                                    Me.txt_NSSBegBalance.Text = FormatNumber(.BookEndingbalance, 2, TriState.True, TriState.True)
                                    Me.txt_NSSBankEndBal.Text = FormatNumber(.BankEndingBalance, 2, TriState.True, TriState.True)
                            End Select
                        End With
                    Next
                End If

                Dim srchBankRecon As New FuncBankReconStatement
                With srchBankRecon
                    .Collection = Me.WbillHelper.GetCollections(curDate, SystemDate, True)
                    '.Collection = (From x In .Collection _
                    '               Where x.Status = EnumCollectionStatus.Allocated _
                    '               Select x).ToList
                    .CollectionMonitoring = Me.WbillHelper.GetCollectionMonitoring(Month(SystemDate), CInt(Year(SystemDate)))
                    .lstChecks = Me.WbillHelper.GetCheck(curDate, SystemDate)
                    .lstFundTransfer = Me.WbillHelper.GetFundTransferForm(curDate, SystemDate)
                    .PaymentAllocation = Me.WbillHelper.GetPaymentAllocation(curDate, SystemDate)
                    .PRHistory = Me.WbillHelper.GetParticipantsPrudentialHistory(curDate, SystemDate)

                    If .Collection.Count = 0 And .CollectionMonitoring.Count = 0 And .lstFundTransfer.Count = 0 _
                    And .lstChecks.Count = 0 And .PaymentAllocation.lstPerBillPeriodAllocation.Count = 0 _
                    And .PRHistory.Count = 0 Then
                    Else
                        genTransactions = .GenerateBankRecon()
                    End If
                End With

                If genTransactions.Count = 0 Then
                    MsgBox("No Transactions found for the selected Month and Year.", MsgBoxStyle.Exclamation, "No Records Found")

                    Exit Sub
                End If

                cmd_Save.Enabled = True
                cmd_Save.Text = "Save"

            End If

            For Each itmBankRecon In genTransactions
                Select Case itmBankRecon.BankReconType
                    Case EnumBankReconType.Settlement

                        Dim TotalAmount As Decimal = 0

                        'Me.txt_stlBookCol.Text = FormatNumber(0, 2, TriState.True, TriState.True)
                        'Me.txt_stlBookCheck.Text = FormatNumber(0, 2, TriState.True, TriState.True)
                        'Me.txt_stlBookEFT.Text = FormatNumber(0, 2, TriState.True, TriState.True)
                        'Me.txt_stlBookNSSPay.Text = FormatNumber(0, 2, TriState.True, TriState.True)
                        'Me.txt_stlBookMF.Text = FormatNumber(0, 2, TriState.True, TriState.True)
                        'Me.txt_stlBookPR.Text = FormatNumber(0, 2, TriState.True, TriState.True)
                        'Me.txt_stlBookInterest.Text = FormatNumber(0, 2, TriState.True, TriState.True)
                        'Me.txt_stlBankCheck.Text = FormatNumber(0, 2, TriState.True, TriState.True)
                        'Me.txt_stlBookCharges.Text = FormatNumber(0, 2, TriState.True, TriState.True)
                        'Me.txt_stlBookNSS.Text = FormatNumber(0, 2, TriState.True, TriState.True)
                        'Me.txt_stlBookPRRep.Text = FormatNumber(0, 2, TriState.True, TriState.True)

                        If itmBankRecon.BeginningBalance <> 0 Then
                            Me.txt_stlBegBalance.Text = FormatNumber(itmBankRecon.BeginningBalance, 2, TriState.True, TriState.True)
                        End If

                        If itmBankRecon.BankEndingBalance <> 0 Then
                            Me.txt_stlBankEndBalance.Text = FormatNumber(itmBankRecon.BankEndingBalance, 2, TriState.True, TriState.True)
                        End If

                        If itmBankRecon.BankCharges <> 0 Then
                            Me.txt_stlBookCharges.Text = FormatNumber(itmBankRecon.BankCharges, 2, TriState.True, TriState.True)
                        End If

                        For Each itmBankReconDetails In itmBankRecon.lstBankReconDetails
                            With itmBankReconDetails

                                'If .Amount = 0 Then
                                '    Continue For
                                'End If

                                Select Case .TransactionType
                                    Case EnumBankReconTransactionType.Collection
                                        Me.txt_stlBookCol.Text = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                                    Case EnumBankReconTransactionType.PaymentCheck
                                        Me.txt_stlBookCheck.Text = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                                    Case EnumBankReconTransactionType.PaymentEFT
                                        Me.txt_stlBookEFT.Text = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                                    Case EnumBankReconTransactionType.NSS
                                        If .Amount > 0 Then
                                            Me.txt_stlBookNSS.Text = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                                        ElseIf .Amount < 0 And .Amount <> 0 Then
                                            Me.txt_stlBookNSSPay.Text = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                                        End If
                                    Case EnumBankReconTransactionType.MarketFees
                                        Me.txt_stlBookMF.Text = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                                    Case EnumBankReconTransactionType.PR
                                        If .Amount > 0 Then
                                            Me.txt_stlBookPRRep.Text = FormatNumber(.Amount * -1D, 2, TriState.True, TriState.True)
                                        ElseIf .Amount < 0 And .Amount <> 0 Then
                                            Me.txt_stlBookPR.Text = FormatNumber(.Amount * -1D, 2, TriState.True, TriState.True)
                                        End If
                                    Case EnumBankReconTransactionType.Interest
                                        Me.txt_stlBookInterest.Text = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                                    Case EnumBankReconTransactionType.OutstandingCheck
                                        Me.txt_stlBankCheck.Text = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                                    Case EnumBankReconTransactionType.BankCharges
                                        Me.txt_stlBookCharges.Text = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                                End Select

                                If .TransactionType <> EnumBankReconTransactionType.OutstandingCheck Then
                                    TotalAmount += .Amount
                                End If
                            End With
                        Next

                        TotalSTL = TotalAmount
                        Me.txt_stlBookEndBalance.Text = FormatNumber(CDec(Me.txt_stlBegBalance.Text) + TotalAmount, 2, TriState.True, TriState.True)
                        Me.txt_stlBookAdjusted.Text = FormatNumber(CDec(Me.txt_stlBegBalance.Text) + TotalAmount - CDec(Me.txt_stlBookCharges.Text), 2, TriState.True, TriState.True)
                        Me.txt_stlBankAdjusted.Text = FormatNumber(CDec(Me.txt_stlBankEndBalance.Text) - CDec(Me.txt_stlBankCheck.Text))
                    Case EnumBankReconType.Prudential

                        Dim TotalAmount As Decimal = 0

                        'Me.txt_PRBookCol.Text = FormatNumber(0, 2, TriState.True, TriState.True)
                        'Me.txt_PRBookDDown.Text = FormatNumber(0, 2, TriState.True, TriState.True)
                        'Me.txt_PRBookPay.Text = FormatNumber(0, 2, TriState.True, TriState.True)
                        'Me.txt_PRBookPrudential.Text = FormatNumber(0, 2, TriState.True, TriState.True)
                        'Me.txt_PRBookInterest.Text = FormatNumber(0, 2, TriState.True, TriState.True)
                        'Me.txt_PRBankCheck.Text = FormatNumber(0, 2, TriState.True, TriState.True)
                        'Me.txt_PRBookCharges.Text = FormatNumber(0, 2, TriState.True, TriState.True)

                        If itmBankRecon.BeginningBalance <> 0 Then
                            Me.txt_PRBegBalance.Text = FormatNumber(itmBankRecon.BeginningBalance, 2, TriState.True, TriState.True)
                        End If

                        If itmBankRecon.BankEndingBalance <> 0 Then
                            Me.txt_PRBankEndBalance.Text = FormatNumber(itmBankRecon.BankEndingBalance, 2, TriState.True, TriState.True)
                        End If

                        If itmBankRecon.BankCharges <> 0 Then
                            Me.txt_PRBookCharges.Text = FormatNumber(itmBankRecon.BankCharges, 2, TriState.True, TriState.True)
                        End If


                        For Each itmBankReconDetails In itmBankRecon.lstBankReconDetails
                            With itmBankReconDetails

                                'If .Amount = 0 Then
                                '    Continue For
                                'End If

                                Select Case .TransactionType
                                    Case EnumBankReconTransactionType.Collection
                                        If .Amount > 0 Then
                                            Me.txt_PRBookCol.Text = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                                        ElseIf .Amount < 0 And .Amount <> 0 Then
                                            Me.txt_PRBookDDown.Text = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                                        End If
                                    Case EnumBankReconTransactionType.PaymentCheck, EnumBankReconTransactionType.PaymentEFT
                                        Me.txt_PRBookPay.Text = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                                    Case EnumBankReconTransactionType.PR
                                        Me.txt_PRBookPrudential.Text = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                                    Case EnumBankReconTransactionType.Interest
                                        Me.txt_PRBookInterest.Text = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                                    Case EnumBankReconTransactionType.OutstandingCheck
                                        Me.txt_PRBankCheck.Text = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                                    Case EnumBankReconTransactionType.BankCharges
                                        Me.txt_PRBookCharges.Text = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                                End Select
                                TotalAmount += .Amount
                            End With
                        Next
                        TotalPR = TotalAmount
                        Me.txt_PRBookEndBalance.Text = FormatNumber(CDec(Me.txt_PRBegBalance.Text) + TotalAmount, 2, TriState.True, TriState.True)
                        Me.txt_PRBookAdjusted.Text = FormatNumber(CDec(Me.txt_PRBegBalance.Text) + TotalAmount - CDec(Me.txt_PRBookCharges.Text), 2, TriState.True, TriState.True)
                        Me.txt_PRBankAdjusted.Text = FormatNumber(CDec(Me.txt_PRBankEndBalance.Text) - CDec(Me.txt_PRBankCheck.Text), 2, TriState.True, TriState.True)
                    Case EnumBankReconType.NSS
                        Dim TotalAmount As Decimal = 0

                        'Me.txt_NSSBookPay.Text = FormatNumber(0, 2, TriState.True, TriState.True)
                        'Me.txt_NSSBookCol.Text = FormatNumber(0, 2, TriState.True, TriState.True)
                        'Me.txt_NSSBookInterest.Text = FormatNumber(0, 2, TriState.True, TriState.True)
                        'Me.txt_NSSBookNSS.Text = FormatNumber(0, 2, TriState.True, TriState.True)


                        If itmBankRecon.BeginningBalance <> 0 Then
                            Me.txt_NSSBegBalance.Text = FormatNumber(itmBankRecon.BeginningBalance, 2, TriState.True, TriState.True)
                        End If

                        If itmBankRecon.BankEndingBalance <> 0 Then
                            Me.txt_NSSBankEndBal.Text = FormatNumber(itmBankRecon.BankEndingBalance, 2, TriState.True, TriState.True)
                        End If

                        For Each itmBankReconDetails In itmBankRecon.lstBankReconDetails
                            With itmBankReconDetails

                                'If .Amount = 0 Then
                                '    Continue For
                                'End If

                                Select Case .TransactionType
                                    Case EnumBankReconTransactionType.PaymentCheck, EnumBankReconTransactionType.PaymentEFT
                                        Me.txt_NSSBookPay.Text = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                                    Case EnumBankReconTransactionType.Collection
                                        Me.txt_NSSBookCol.Text = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                                    Case EnumBankReconTransactionType.Interest
                                        If .Amount > 0 Then
                                            Me.txt_NSSBookInterest.Text = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                                        ElseIf .Amount < 0 And .Amount <> 0 Then
                                            Me.txt_NSSBookNSS.Text = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                                        End If
                                End Select
                                TotalAmount += .Amount
                            End With
                        Next

                        TotalNSS = TotalAmount
                        Me.txt_NSSBookEndBal.Text = FormatNumber(CDec(Me.txt_NSSBegBalance.Text) + TotalAmount, 2, TriState.True, TriState.True)
                End Select
            Next

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Updated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_BankReconStatementWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInGeneratingReport.ToString, AMModule.UserName)
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Save.Click
        Dim ans As New MsgBoxResult
        Try

            If chk_Search.Checked = False Then
                If Me.txt_stlBegBalance.Text = "0.00" Or Me.txt_PRBegBalance.Text = "0.00" Or Me.txt_NSSBegBalance.Text = "0.00" Then
                    MsgBox("Cannot proceed to saving the transactions please check Beginning Balances of all transactions!", MsgBoxStyle.Critical, "Error")                    
                    Exit Sub
                End If

                ans = MsgBox("Do you really want to save the transactions?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Save")

                If ans = MsgBoxResult.Yes Then

                    For Each itmGenTransactions In genTransactions
                        With itmGenTransactions
                            Select Case .BankReconType
                                Case EnumBankReconType.Settlement
                                    .BeginningBalance = CDec(Me.txt_stlBegBalance.Text)
                                    .BankCharges = CDec(Me.txt_stlBookCharges.Text)
                                    .BookEndingbalance = CDec(Me.txt_stlBookEndBalance.Text)
                                    .BankEndingBalance = CDec(Me.txt_stlBankEndBalance.Text)
                                Case EnumBankReconType.Prudential
                                    .BeginningBalance = CDec(Me.txt_PRBegBalance.Text)
                                    .BankCharges = CDec(Me.txt_PRBookCharges.Text)
                                    .BookEndingbalance = CDec(Me.txt_PRBookEndBalance.Text)
                                    .BankEndingBalance = CDec(Me.txt_PRBankEndBalance.Text)
                                Case EnumBankReconType.NSS
                                    .BeginningBalance = CDec(Me.txt_NSSBegBalance.Text)
                                    .BookEndingbalance = CDec(Me.txt_NSSBookEndBal.Text)
                                    .BankEndingBalance = CDec(Me.txt_NSSBankEndBal.Text)
                            End Select
                        End With
                    Next

                    Me.WbillHelper.SaveBankReconStatement(genTransactions)
                    MsgBox("Successfully saved", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, MsgBoxStyle), "Success Save")                    
                End If
            Else

                Select Case tc_MainWindow.SelectedTab.Name
                    Case tp_STL.Name
                        If Me.txt_stlBankEndBalance.Text = "0.00" Then
                            MsgBox("Bank Ending Balance should not be zero", MsgBoxStyle.Critical, "Error")
                            Me.txt_stlBankEndBalance.Focus()                            
                            Exit Sub
                        End If

                        If Me.txt_stlBookCharges.Text = "0.00" Then
                            MsgBox("Bank Charges is amounting to " & FormatNumber(CDec(Me.txt_stlBookCharges.Text)), MsgBoxStyle.Exclamation, "Notification")
                        End If

                        ans = MsgBox("Do you want to save the bank ending balance for SETTLEMENT " & FormatNumber(CDec(Me.txt_stlBankEndBalance.Text), 2, TriState.True, TriState.True) & "?", MsgBoxStyle.YesNo, "Save Bank Ending Balance")
                        If ans = MsgBoxResult.Yes Then
                            Dim _stlTransaction = (From x In genTransactions _
                                                   Where x.BankReconType = EnumBankReconType.Settlement _
                                                   Select x).FirstOrDefault
                            Me.WbillHelper.UpdateBankEndingBalance(CDec(Me.txt_stlBankEndBalance.Text), CDec(Me.txt_stlBookCharges.Text), _stlTransaction)
                            MsgBox("Update complete for Settlement Transaction", MsgBoxStyle.Information, "Success")                            
                        End If
                    Case tp_PR.Name
                        If Me.txt_PRBankEndBalance.Text = "0.00" Then
                            MsgBox("Bank Ending Balance should not be zero", MsgBoxStyle.Critical, "Error")
                            Me.txt_PRBankEndBalance.Focus()                            
                            Exit Sub
                        End If

                        If Me.txt_PRBookCharges.Text = "0.00" Then
                            MsgBox("Bank Charges is amounting to " & FormatNumber(CDec(Me.txt_PRBookCharges.Text)), MsgBoxStyle.Exclamation, "Notification")
                        End If

                        ans = MsgBox("Do you want to save the bank ending balance for PRUDENTIAL " & FormatNumber(CDec(Me.txt_PRBankEndBalance.Text), 2, TriState.True, TriState.True) & "?", MsgBoxStyle.YesNo, "Save Bank Ending Balance")
                        If ans = MsgBoxResult.Yes Then
                            Dim _PRTransaction = (From x In genTransactions _
                                                   Where x.BankReconType = EnumBankReconType.Prudential _
                                                   Select x).FirstOrDefault
                            Me.WbillHelper.UpdateBankEndingBalance(CDec(Me.txt_PRBankEndBalance.Text), CDec(Me.txt_PRBookCharges.Text), _PRTransaction)
                            MsgBox("Update complete for Prudential Transaction", MsgBoxStyle.Information, "Success")                            
                        End If
                    Case tp_NSS.Name
                        If Me.txt_NSSBankEndBal.Text = "0.00" Then
                            MsgBox("Bank Ending Balance should not be zero", MsgBoxStyle.Critical, "Error")
                            Me.txt_NSSBankEndBal.Focus()                            
                            Exit Sub
                        End If

                        ans = MsgBox("Do you want to save the bank ending balance for NSS " & FormatNumber(CDec(Me.txt_NSSBankEndBal.Text), 2, TriState.True, TriState.True) & "?", MsgBoxStyle.YesNo, "Save Bank Ending Balance")
                        If ans = MsgBoxResult.Yes Then
                            Dim _NSSTransaction = (From x In genTransactions _
                                                   Where x.BankReconType = EnumBankReconType.NSS _
                                                   Select x).FirstOrDefault
                            Me.WbillHelper.UpdateBankEndingBalance(CDec(Me.txt_NSSBankEndBal.Text), 0, _NSSTransaction)
                            MsgBox("Update complete for NSS Transaction", MsgBoxStyle.Information, "Success")                            
                        End If
                End Select
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Updated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_BankReconStatementWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInSaving.ToString, AMModule.UserName)
            Exit Sub
        End Try

    End Sub

    Private Sub chk_Search_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_Search.CheckedChanged
        If Me.chk_Search.Checked Then
            Me.cbo_MonthSelected.Enabled = True
            Me.nud_Year.Enabled = True
        Else
            Me.cbo_MonthSelected.Enabled = False
            Me.nud_Year.Enabled = False
        End If
    End Sub

    'Validate KeyPress
    Function CheckNumber(ByVal CharEnter As Char) As Boolean
        If IsNumeric(CharEnter) Then
            Return False
        Else
            Return True
        End If
    End Function

#Region "STL Tab Functions"

    Private Sub txt_stlBankEndBalance_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_stlBankEndBalance.GotFocus
        Me.txt_stlBankEndBalance.Text = Replace(Me.txt_stlBankEndBalance.Text, ",", "")
    End Sub

    Private Sub txt_stlBankEndBalance_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_stlBankEndBalance.KeyPress
        If Me.txt_stlBankEndBalance.Text.Contains(".") = True Then
            If e.KeyChar = "." Then
                e.Handled = True
            End If
        End If

        If e.KeyChar <> ChrW(8) And e.KeyChar <> ChrW(37) And e.KeyChar <> ChrW(39) And e.KeyChar <> ChrW(46) Then
            If e.KeyChar() <> "." Then
                e.Handled = CheckNumber(e.KeyChar)
            End If
        End If

    End Sub

    'On Lost Focus
    Private Sub txt_stlBankEndBalance_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_stlBankEndBalance.LostFocus
        If Me.txt_stlBankEndBalance.Text.Trim.Length = 0 Then
            Me.txt_stlBankEndBalance.Text = "0.00"
        Else
            Me.txt_stlBankEndBalance.Text = FormatNumber(Me.txt_stlBankEndBalance.Text, 2, TriState.True, TriState.True)
            Me.txt_stlBankAdjusted.Text = FormatNumber(CDec(Me.txt_stlBankEndBalance.Text) - CDec(Me.txt_stlBankCheck.Text), 2, TriState.True, TriState.True)
        End If
    End Sub

    Private Sub txt_stlBookCharges_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_stlBookCharges.GotFocus
        Me.txt_stlBookCharges.Text = Replace(Me.txt_stlBookCharges.Text, ",", "")
    End Sub

    Private Sub txt_stlBookCharges_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_stlBookCharges.KeyPress
        If Me.txt_stlBookCharges.Text.Contains(".") = True Then
            If e.KeyChar = "." Then
                e.Handled = True
            End If
        End If

        If e.KeyChar <> ChrW(8) And e.KeyChar <> ChrW(37) And e.KeyChar <> ChrW(39) And e.KeyChar <> ChrW(46) Then
            e.Handled = CheckNumber(e.KeyChar)
        End If
    End Sub

    Private Sub txt_stlBookCharges_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_stlBookCharges.LostFocus
        If Me.txt_stlBookCharges.Text.Trim.Length = 0 Then
            Me.txt_stlBookCharges.Text = "0.00"
        Else
            Me.txt_stlBookCharges.Text = FormatNumber(Me.txt_stlBookCharges.Text, 2, TriState.True, TriState.True)
            Me.txt_stlBookAdjusted.Text = FormatNumber(CDec(Me.txt_stlBookEndBalance.Text) - CDec(Me.txt_stlBookCharges.Text), 2, TriState.True, TriState.True)
            'Me.txt_stlBookEndBalance.Text = FormatNumber(CDec(Me.txt_stlBegBalance.Text) - TotalSTL, 2, TriState.True, TriState.True)
        End If

    End Sub

    Private Sub txt_stlBegBalance_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_stlBegBalance.GotFocus
        Me.txt_stlBegBalance.Text = Replace(Me.txt_stlBegBalance.Text, ",", "")
    End Sub

    Private Sub txt_stlBegBalance_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_stlBegBalance.KeyPress
        If Me.txt_stlBegBalance.Text.Contains(".") = True Then
            If e.KeyChar = "." Then
                e.Handled = True
            End If
        End If

        If e.KeyChar <> ChrW(8) And e.KeyChar <> ChrW(37) And e.KeyChar <> ChrW(39) And e.KeyChar <> ChrW(46) Then
            e.Handled = CheckNumber(e.KeyChar)
        End If
    End Sub

    Private Sub txt_stlBegBalance_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_stlBegBalance.LostFocus
        If Me.txt_stlBegBalance.Text.Trim.Length = 0 Then
            Me.txt_stlBegBalance.Text = "0.00"
        Else
            Me.txt_stlBegBalance.Text = FormatNumber(CDec(Me.txt_stlBegBalance.Text), 2, TriState.True, TriState.True)
            Me.txt_stlBookEndBalance.Text = FormatNumber(CDec(Me.txt_stlBegBalance.Text) + TotalSTL, 2, TriState.True, TriState.True)
            Me.txt_stlBookAdjusted.Text = FormatNumber(CDec(Me.txt_stlBookEndBalance.Text) - CDec(Me.txt_stlBookCharges.Text), 2, TriState.True, TriState.True)
        End If
    End Sub


    'On Key Down
    'Private Sub txt_stlBankEndBalance_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txt_stlBankEndBalance.KeyDown
    '    If e.KeyCode <> 8 And e.KeyCode <> 37 And e.KeyCode <> 39 And e.KeyCode <> 46 Then
    '        e.Handled = CheckNumber(ChrW(e.KeyValue))
    '    End If

    '    If Me.txt_stlBankEndBalance.Text.Trim.Length = 0 Then
    '        Me.txt_stlBankEndBalance.Text = "0.00"
    '    End If
    'End Sub

    'Private Sub txt_stlBankEndBalance_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_stlBankEndBalance.KeyUp
    '    If e.KeyCode <> 8 And e.KeyCode <> 37 And e.KeyCode <> 39 And e.KeyCode <> 46 Then
    '        e.Handled = CheckNumber(ChrW(e.KeyValue))
    '    End If

    '    If Me.txt_stlBankEndBalance.Text.Trim.Length = 0 Then
    '        Me.txt_stlBankEndBalance.Text = "0.00"
    '    End If

    '    Me.txt_stlBankAdjusted.Text = FormatNumber(CDec(Me.txt_stlBankEndBalance.Text) - CDec(Me.txt_stlBankCheck.Text), 2, TriState.True, TriState.True)
    'End Sub

    'Private Sub txt_stlBookCharges_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txt_stlBookCharges.KeyDown
    '    If e.KeyCode <> 8 And e.KeyCode <> 37 And e.KeyCode <> 39 And e.KeyCode <> 46 Then
    '        e.Handled = CheckNumber(ChrW(e.KeyValue))
    '    End If

    '    If Me.txt_stlBookCharges.Text.Trim.Length = 0 Then
    '        Me.txt_stlBookCharges.Text = "0.00"
    '    End If
    'End Sub

    'Private Sub txt_stlBookCharges_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_stlBookCharges.KeyUp
    '    If Me.txt_stlBookCharges.Text.Trim.Length = 0 Then
    '        Me.txt_stlBookCharges.Text = "0.00"
    '    End If
    '    Me.txt_stlBookAdjusted.Text = FormatNumber(CDec(Me.txt_stlBookEndBalance.Text) - CDec(Me.txt_stlBookCharges.Text), 2, TriState.True, TriState.True)
    'End Sub

    'Private Sub txt_stlBegBalance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_stlBegBalance.KeyDown

    '    If Me.txt_stlBegBalance.ReadOnly = False Then
    '        If e.KeyCode <> 8 And e.KeyCode <> 37 And e.KeyCode <> 39 And e.KeyCode <> 46 Then
    '            If IsNumeric(ChrW(e.KeyData)) = False Then
    '                Exit Sub
    '            End If
    '        End If

    '        If Me.txt_stlBegBalance.Text.Trim.Length = 0 Then
    '            Me.txt_stlBegBalance.Text = "0.00"
    '        End If


    '    End If
    'End Sub

    'Private Sub txt_stlBegBalance_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_stlBegBalance.KeyUp
    '    If Me.txt_stlBegBalance.Text.Trim.Length = 0 Then
    '        Me.txt_stlBegBalance.Text = "0.00"
    '    End If

    '    If IsNumeric(ChrW(e.KeyData)) = False Then
    '        Exit Sub
    '    End If

    '    Me.txt_stlBookEndBalance.Text = FormatNumber(CDec(Me.txt_stlBegBalance.Text) - TotalSTL, 2, TriState.True, TriState.True)
    '    Me.txt_stlBookAdjusted.Text = (FormatNumber(CDec(Me.txt_stlBegBalance.Text) - TotalSTL - CDec(Me.txt_stlBookCharges.Text), 2, TriState.True, TriState.True))
    'End Sub

#End Region

#Region "NSS Tab Functions"

    Private Sub txt_NSSBegBalance_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_NSSBegBalance.GotFocus
        Me.txt_NSSBegBalance.Text = Replace(Me.txt_NSSBegBalance.Text, ",", "")
    End Sub

    Private Sub txt_NSSBegBalance_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_NSSBegBalance.KeyPress
        If Me.txt_NSSBegBalance.Text.Contains(".") = True Then
            If e.KeyChar = "." Then
                e.Handled = True
            End If
        End If

        If e.KeyChar <> ChrW(8) And e.KeyChar <> ChrW(37) And e.KeyChar <> ChrW(39) And e.KeyChar <> ChrW(46) Then
            e.Handled = CheckNumber(e.KeyChar)
        End If
    End Sub
    'Formatting on Lost Focus
    Private Sub txt_NSSBegBalance_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_NSSBegBalance.LostFocus
        If Me.txt_NSSBegBalance.Text.Trim.Length = 0 Then
            Me.txt_NSSBegBalance.Text = "0.00"
        Else
            Me.txt_NSSBegBalance.Text = FormatNumber(CDec(Me.txt_NSSBegBalance.Text), 2, TriState.True, TriState.True)
            Me.txt_NSSBookEndBal.Text = FormatNumber(CDec(Me.txt_NSSBegBalance.Text) + TotalNSS, 2, TriState.True, TriState.True)
        End If
    End Sub

    Private Sub txt_NSSBankEndBal_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_NSSBankEndBal.GotFocus
        Me.txt_NSSBankEndBal.Text = Replace(Me.txt_NSSBankEndBal.Text, ",", "")
    End Sub

    Private Sub txt_NSSBankEndBal_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_NSSBankEndBal.KeyPress
        If Me.txt_NSSBankEndBal.Text.Contains(".") = True Then
            If e.KeyChar = "." Then
                e.Handled = True
            End If
        End If

        If e.KeyChar <> ChrW(8) And e.KeyChar <> ChrW(37) And e.KeyChar <> ChrW(39) And e.KeyChar <> ChrW(46) Then
            e.Handled = CheckNumber(e.KeyChar)
        End If
    End Sub

    Private Sub txt_NSSBankEndBal_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_NSSBankEndBal.LostFocus
        If Me.txt_NSSBankEndBal.Text.Trim.Length = 0 Then
            Me.txt_NSSBankEndBal.Text = "0.00"
        Else
            Me.txt_NSSBankEndBal.Text = FormatNumber(Me.txt_NSSBankEndBal.Text, 2, TriState.True, TriState.True)
        End If
    End Sub

    'On Key Press
    'Private Sub txt_NSSBankEndBal_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_NSSBankEndBal.KeyDown
    '    If e.KeyCode <> 8 And e.KeyCode <> 37 And e.KeyCode <> 39 And e.KeyCode <> 46 Then
    '        e.Handled = CheckNumber(ChrW(e.KeyValue))
    '    End If

    '    If Me.txt_NSSBankEndBal.Text.Trim.Length = 0 Then
    '        Me.txt_NSSBankEndBal.Text = "0.00"
    '    End If
    'End Sub

    'Private Sub txt_NSSBegBalance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_NSSBegBalance.KeyDown
    '    If Me.txt_NSSBegBalance.ReadOnly = False Then
    '        If e.KeyCode <> 8 And e.KeyCode <> 37 And e.KeyCode <> 39 And e.KeyCode <> 46 Then
    '            e.Handled = CheckNumber(ChrW(e.KeyValue))
    '        End If

    '        If Me.txt_NSSBegBalance.Text.Trim.Length = 0 Then
    '            Me.txt_NSSBegBalance.Text = "0.00"
    '        End If
    '    End If
    'End Sub

    'Private Sub txt_NSSBegBalance_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_NSSBegBalance.KeyUp
    '    If Me.txt_NSSBegBalance.Text.Trim.Length = 0 Then
    '        Me.txt_NSSBegBalance.Text = "0.00"
    '    End If
    '    Me.txt_NSSBookEndBal.Text = FormatNumber(CDec(Me.txt_NSSBegBalance.Text) - TotalNSS, 2, TriState.True, TriState.True)
    'End Sub
#End Region

#Region "PR Tab Functions"

    Private Sub txt_PRBankEndBalance_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_PRBankEndBalance.GotFocus
        Me.txt_PRBankEndBalance.Text = Replace(Me.txt_PRBankEndBalance.Text, ",", "")
    End Sub

    Private Sub txt_PRBankEndBalance_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_PRBankEndBalance.KeyPress
        If Me.txt_PRBankEndBalance.Text.Contains(".") = True Then
            If e.KeyChar = "." Then
                e.Handled = True
            End If
        End If

        If e.KeyChar <> ChrW(8) And e.KeyChar <> ChrW(37) And e.KeyChar <> ChrW(39) And e.KeyChar <> ChrW(46) Then
            e.Handled = CheckNumber(e.KeyChar)
        End If
    End Sub
    'Formatting on Lost Focus
    Private Sub txt_PRBankEndBalance_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_PRBankEndBalance.LostFocus
        If Me.txt_PRBankEndBalance.Text.Trim.Length = 0 Then
            Me.txt_PRBankEndBalance.Text = "0.00"
        Else
            Me.txt_PRBankEndBalance.Text = FormatNumber(Me.txt_PRBankEndBalance.Text, 2, TriState.True, TriState.True)
        End If
    End Sub

    Private Sub txt_PRBookCharges_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_PRBookCharges.GotFocus
        Me.txt_PRBookCharges.Text = Replace(Me.txt_PRBookCharges.Text, ",", "")
    End Sub

    Private Sub txt_PRBookCharges_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_PRBookCharges.KeyPress
        If Me.txt_PRBookCharges.Text.Contains(".") = True Then
            If e.KeyChar = "." Then
                e.Handled = True
            End If
        End If

        If e.KeyChar <> ChrW(8) And e.KeyChar <> ChrW(37) And e.KeyChar <> ChrW(39) And e.KeyChar <> ChrW(46) Then
            e.Handled = CheckNumber(e.KeyChar)
        End If
    End Sub

    Private Sub txt_PRBookCharges_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_PRBookCharges.LostFocus
        If Me.txt_PRBookCharges.Text.Trim.Length = 0 Then
            Me.txt_PRBookCharges.Text = "0.00"
        Else
            Me.txt_PRBookCharges.Text = FormatNumber(Me.txt_PRBookCharges.Text, 2, TriState.True, TriState.True)
            Me.txt_PRBookAdjusted.Text = FormatNumber(CDec(Me.txt_PRBookEndBalance.Text) - CDec(Me.txt_PRBookCharges.Text), 2, TriState.True, TriState.True)
        End If

    End Sub

    Private Sub txt_PRBegBalance_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_PRBegBalance.GotFocus
        Me.txt_PRBegBalance.Text = Replace(Me.txt_PRBegBalance.Text, ",", "")
    End Sub

    Private Sub txt_PRBegBalance_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_PRBegBalance.KeyPress
        If Me.txt_PRBegBalance.Text.Contains(".") = True Then
            If e.KeyChar = "." Then
                e.Handled = True
            End If
        End If

        If e.KeyChar <> ChrW(8) And e.KeyChar <> ChrW(37) And e.KeyChar <> ChrW(39) And e.KeyChar <> ChrW(46) Then
            e.Handled = CheckNumber(e.KeyChar)
        End If
    End Sub

    Private Sub txt_PRBegBalance_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_PRBegBalance.LostFocus
        If Me.txt_PRBegBalance.Text.Trim.Length = 0 Then
            Me.txt_PRBegBalance.Text = "0.00"
        Else
            Me.txt_PRBegBalance.Text = FormatNumber(Me.txt_PRBegBalance.Text, 2, TriState.True, TriState.True)
            Me.txt_PRBookEndBalance.Text = FormatNumber(CDec(Me.txt_PRBegBalance.Text) + TotalPR, 2, TriState.True, TriState.True)
            Me.txt_PRBookAdjusted.Text = FormatNumber(CDec(Me.txt_PRBegBalance.Text) - CDec(Me.txt_PRBookCharges.Text), 2, TriState.True, TriState.True)
        End If
    End Sub

    'On Key Press
    'Private Sub txt_PRBankEndBalance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_PRBankEndBalance.KeyDown
    '    If e.KeyCode <> 8 And e.KeyCode <> 37 And e.KeyCode <> 39 Then
    '        e.Handled = CheckNumber(ChrW(e.KeyValue))
    '    End If

    '    If Me.txt_PRBankEndBalance.Text.Trim.Length = 0 Then
    '        Me.txt_PRBankEndBalance.Text = "0.00"
    '    End If
    'End Sub

    'Private Sub txt_PRBankEndBalance_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_PRBankEndBalance.KeyUp
    '    If Me.txt_PRBankEndBalance.Text.Trim.Length = 0 Then
    '        Me.txt_PRBankEndBalance.Text = "0.00"
    '    End If

    '    Me.txt_PRBankAdjusted.Text = FormatNumber(CDec(Me.txt_PRBankEndBalance.Text) - CDec(Me.txt_PRBankCheck.Text), 2, TriState.True, TriState.True)
    'End Sub

    'Private Sub txt_PRBookCharges_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_PRBookCharges.KeyDown
    '    If e.KeyCode <> 8 And e.KeyCode <> 37 And e.KeyCode <> 39 Then
    '        e.Handled = CheckNumber(ChrW(e.KeyValue))
    '    End If

    '    If Me.txt_PRBookCharges.Text.Trim.Length = 0 Then
    '        Me.txt_PRBookCharges.Text = "0.00"
    '    End If
    'End Sub

    'Private Sub txt_PRBookCharges_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_PRBookCharges.KeyUp
    '    If Me.txt_PRBankEndBalance.Text.Trim.Length = 0 Then
    '        Me.txt_PRBankEndBalance.Text = "0.00"
    '    End If
    '    Me.txt_PRBookAdjusted.Text = FormatNumber(CDec(Me.txt_PRBookEndBalance.Text) - CDec(Me.txt_PRBookCharges.Text), 2, TriState.True, TriState.True)
    'End Sub

    'Private Sub txt_PRBegBalance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_PRBegBalance.KeyDown
    '    If Me.txt_PRBegBalance.ReadOnly = False Then
    '        If e.KeyCode <> 8 And e.KeyCode <> 37 And e.KeyCode <> 39 Then
    '            e.Handled = CheckNumber(ChrW(e.KeyValue))
    '        End If

    '        If Me.txt_PRBegBalance.Text.Trim.Length = 0 Then
    '            Me.txt_PRBegBalance.Text = "0.00"
    '        End If
    '    End If
    'End Sub

    'Private Sub txt_PRBegBalance_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_PRBegBalance.KeyUp
    '    If Me.txt_PRBegBalance.Text.Trim.Length = 0 Then
    '        Me.txt_PRBegBalance.Text = "0.00"
    '    End If

    '    Me.txt_PRBookEndBalance.Text = FormatNumber(CDec(Me.txt_PRBegBalance.Text) - TotalPR, 2, TriState.True, TriState.True)
    '    Me.txt_PRBookAdjusted.Text = FormatNumber(CDec(Me.txt_PRBegBalance.Text) - TotalPR - CDec(Me.txt_PRBookCharges.Text), 2, TriState.True, TriState.True)
    'End Sub

#End Region

    Private Sub cmd_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_export.Click

        If genTransactions.Count = 0 Then
            MsgBox("No records available for export to CSV.", MsgBoxStyle.Exclamation, "Error")
            'Updated by Lance 04/28/2014 --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            'LDAPModule.LDAP.InsertLog(Me.Name, EnumAMSModules.AMS_BankReconStatementWindow.ToString(), "No records available for export to CSV.", "", "", EnumColorCode.Red.ToString(), EnumLogType.ErrorInExporting.ToString(), 'LDAPModule.LDAP.Username)
            '------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            Exit Sub
        End If

        Dim fnameBuilder As String = ""
        fnameBuilder = ""

        Dim ans As New MsgBoxResult
        ans = MsgBox("Do you really want to export the selected account to CSV file?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Save")

        If ans = MsgBoxResult.No Then
            Exit Sub
        End If

        Dim fldrBrowser As New FolderBrowserDialog
        With fldrBrowser
            .RootFolder = Environment.SpecialFolder.Desktop
            .ShowDialog()

            If .SelectedPath.Length = 0 Then
                Exit Sub
            Else
                fnameBuilder = .SelectedPath
            End If
        End With

        Dim headerBuilder As String = ""

        Select Case tc_MainWindow.SelectedTab.Name
            Case tp_STL.Name
                fnameBuilder &= "\BankReconciliationStatement_STL_"
                Dim _selTransaction = (From x In genTransactions _
                                         Where x.BankReconType = EnumBankReconType.Settlement _
                                         Select x).FirstOrDefault
                fnameBuilder &= MonthName(Month(_selTransaction.TransactionDate), True) & "_" & Year(_selTransaction.TransactionDate)
                headerBuilder = "Settlement Account for " & MonthName(Month(_selTransaction.TransactionDate)) & " " & Year(_selTransaction.TransactionDate)
            Case tp_PR.Name
                fnameBuilder &= "\BankReconciliationStatement_PR_"
                Dim _selTransaction = (From x In genTransactions _
                                         Where x.BankReconType = EnumBankReconType.Prudential _
                                         Select x).FirstOrDefault
                fnameBuilder &= MonthName(Month(_selTransaction.TransactionDate), True) & "_" & Year(_selTransaction.TransactionDate)
                headerBuilder = "Prudential Account for " & MonthName(Month(_selTransaction.TransactionDate)) & " " & Year(_selTransaction.TransactionDate)
            Case tp_NSS.Name
                fnameBuilder &= "\BankReconciliationStatement_NSS_"
                Dim _selTransaction = (From x In genTransactions _
                                         Where x.BankReconType = EnumBankReconType.NSS _
                                         Select x).FirstOrDefault
                fnameBuilder &= MonthName(Month(_selTransaction.TransactionDate), True) & "_" & Year(_selTransaction.TransactionDate)
                headerBuilder = "NSS Account for " & MonthName(Month(_selTransaction.TransactionDate)) & " " & Year(_selTransaction.TransactionDate)
        End Select

        fnameBuilder &= ".csv"

        Dim dt As New DataTable
        dt.Columns.Add("Transaction Type")
        dt.Columns.Add("Book Balance")
        dt.Columns.Add("Bank Balance")

        Dim dr As DataRow
        dr = dt.NewRow
        dr("Transaction Type") = "Philippine Electricity Market Corporation"
        dt.Rows.Add(dr)
        dt.AcceptChanges()

        dr = dt.NewRow
        dr("Transaction Type") = "Bank Reconciliation Statement"
        dt.Rows.Add(dr)
        dt.AcceptChanges()

        dr = dt.NewRow
        dr("Transaction Type") = headerBuilder
        dt.Rows.Add(dr)
        dt.AcceptChanges()

        dt.Rows.Add(dt.NewRow)
        dt.Rows.Add(dt.NewRow)

        dr = dt.NewRow
        dr("Transaction Type") = "Transaction Type"
        dr("Book Balance") = "Book Balance"
        dr("Bank Balance") = "Bank Balance"
        dt.Rows.Add(dr)
        dt.AcceptChanges()

        Select Case tc_MainWindow.SelectedTab.Name
            Case tp_STL.Name
                dr = dt.NewRow()
                dr("Transaction Type") = "Beginning Balance"
                dr("Book Balance") = Me.txt_stlBegBalance.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "Collection"
                dr("Book Balance") = Me.txt_stlBookCol.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "PR - Drawdown"
                dr("Book Balance") = Me.txt_stlBookPR.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "NSS"
                dr("Book Balance") = Me.txt_stlBookNSS.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "Interest Earned"
                dr("Book Balance") = Me.txt_stlBookInterest.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "Payments"
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "   LBC"
                dr("Book Balance") = Me.txt_stlBookCheck.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "   EFT"
                dr("Book Balance") = Me.txt_stlBookEFT.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "   FTF - MF"
                dr("Book Balance") = Me.txt_stlBookMF.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "   FTF - PR REPLENISHMENT"
                dr("Book Balance") = Me.txt_stlBookPRRep.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "   FTF - NSS"
                dr("Book Balance") = Me.txt_stlBookNSSPay.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "Ending Balance"
                dr("Book Balance") = Me.txt_stlBookEndBalance.Text
                dr("Bank Balance") = Me.txt_stlBankEndBalance.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "Outstanding Checks"
                dr("Bank Balance") = Me.txt_stlBankCheck.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "Bank Charges"
                dr("Book Balance") = Me.txt_stlBookCharges.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "Adjusted Balance"
                dr("Book Balance") = Me.txt_stlBookAdjusted.Text
                dr("Bank Balance") = Me.txt_stlBankAdjusted.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

            Case tp_PR.Name

                dr = dt.NewRow()
                dr("Transaction Type") = "Beginning Balance"
                dr("Book Balance") = Me.txt_PRBegBalance.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "Replenishment Collection"
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "   Collection from STL"
                dr("Book Balance") = Me.txt_PRBookCol.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "   Transfer from Payment"
                dr("Book Balance") = Me.txt_PRBookPay.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "   Direct Update"
                dr("Book Balance") = Me.txt_PRBookPrudential.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "   Interest Earned"
                dr("Book Balance") = Me.txt_PRBookInterest.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "   PR Drawdown"
                dr("Book Balance") = Me.txt_PRBookDDown.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "Ending Balance"
                dr("Book Balance") = Me.txt_PRBookEndBalance.Text
                dr("Bank Balance") = Me.txt_PRBankEndBalance.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "Outstanding Checks"
                dr("Bank Balance") = Me.txt_PRBankCheck.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "Bank Charges"
                dr("Book Balance") = Me.txt_PRBookCharges.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "Adjusted Balance"
                dr("Book Balance") = Me.txt_PRBookAdjusted.Text
                dr("Bank Balance") = Me.txt_PRBankAdjusted.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()
            Case tp_NSS.Name

                dr = dt.NewRow()
                dr("Transaction Type") = "Beginning Balance"
                dr("Book Balance") = Me.txt_NSSBegBalance.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "FTF from Settlement"
                dr("Book Balance") = Me.txt_NSSBookCol.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "Interest Earned"
                dr("Book Balance") = Me.txt_NSSBookInterest.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "FTF to Settlement"
                dr("Book Balance") = Me.txt_NSSBookPay.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "Interest to MP"
                dr("Book Balance") = Me.txt_NSSBookNSS.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()

                dr = dt.NewRow()
                dr("Transaction Type") = "Ending Balance"
                dr("Book Balance") = Me.txt_NSSBookEndBal.Text
                dr("Bank Balance") = Me.txt_NSSBankEndBal.Text
                dt.Rows.Add(dr)
                dt.AcceptChanges()
        End Select

        Me.BFactory.RemoveCommaForCSVExport(dt)
        Me.WbillHelper.DataTable2CSV(dt, fnameBuilder)
        MsgBox("Successfully exported data to " & fnameBuilder & ".", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Information, MsgBoxStyle), "Success")

    End Sub

    Private Sub tc_MainWindow_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles tc_MainWindow.DrawItem

        'Firstly we'll define some parameters.
        Dim CurrentTab As TabPage = tc_MainWindow.TabPages(e.Index)
        Dim ItemRect As Rectangle = tc_MainWindow.GetTabRect(e.Index)
        Dim FillBrush As New SolidBrush(Color.White)
        Dim TextBrush As New SolidBrush(System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer)))
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        'If we are currently painting the Selected TabItem we'll 
        'change the brush colors and inflate the rectangle.
        If CBool(e.State And DrawItemState.Selected) Then
            FillBrush.Color = Color.White
            TextBrush.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
            ItemRect.Inflate(2, 2)
        End If

        'Set up rotation for left and right aligned tabs
        If tc_MainWindow.Alignment = TabAlignment.Left Or tc_MainWindow.Alignment = TabAlignment.Right Then
            Dim RotateAngle As Single = 90
            If tc_MainWindow.Alignment = TabAlignment.Left Then RotateAngle = 270
            Dim cp As New PointF(ItemRect.Left + (ItemRect.Width \ 2), ItemRect.Top + (ItemRect.Height \ 2))
            e.Graphics.TranslateTransform(cp.X, cp.Y)
            e.Graphics.RotateTransform(RotateAngle)
            ItemRect = New Rectangle(-(ItemRect.Height \ 2), -(ItemRect.Width \ 2), ItemRect.Height, ItemRect.Width)
        End If

        'Next we'll paint the TabItem with our Fill Brush
        e.Graphics.FillRectangle(FillBrush, ItemRect)

        'Now draw the text.
        e.Graphics.DrawString(CurrentTab.Text, e.Font, TextBrush, RectangleF.op_Implicit(ItemRect), sf)

        'Reset any Graphics rotation
        e.Graphics.ResetTransform()

        'Finally, we should Dispose of our brushes.
        FillBrush.Dispose()
        TextBrush.Dispose()

    End Sub


End Class