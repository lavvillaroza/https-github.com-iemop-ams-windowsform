'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmDeferredPayment
'Orginal Author:         Joseph B. Gabriel
'File Creation Date:     June 09, 2015
'Development Group:      Software Development and Support Division
'Description:            GUI for the generation of Deferred Payment
'Arguments/Parameters:  
'Files/Database Tables:  frmDeferredPayment
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmDeferredMonitoring
    Private BFactory As BusinessFactory
    Private _ListParticipants As List(Of AMParticipants)
    Private WbillHelper As WESMBillHelper


    Private Sub frmDeferredMonitoring_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try


            'Dim listOfDate As List(Of Date)
            Me.MdiParent = MainForm

            BFactory = BusinessFactory.GetInstance()
            WbillHelper = WESMBillHelper.GetInstance()
            WbillHelper.ConnectionString = AMModule.ConnectionString
            WbillHelper.UserName = AMModule.UserName

            Dim dtTransDate As New List(Of String)
            dtTransDate = WbillHelper.GetDeferredPaymentTransactionDate
            cb_TransactionDate.DataSource = dtTransDate
            cb_TransactionDate.SelectedIndex = -1
            'cb_TransactionDate.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error!")
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_GenSave_Click(sender As Object, e As EventArgs) Handles cmd_GenSave.Click
        Dim dicParticipants As New Dictionary(Of String, AMParticipants)
        Dim dicPreviousDeferred As New Dictionary(Of String, DeferredMonitoring)
        Dim dicCurrentDeferred As New Dictionary(Of String, DeferredMonitoring)
        Dim listOfDeferred As New List(Of DeferredMonitoringReport)
        Dim PreviousDate As Date, CurrentDate As Date
        Dim result As New DataTable        

        Try
            Dim rptView As New frmReportViewer

            If Me.cb_TransactionDate.Items.Count = 0 Then
                MessageBox.Show("No data", "No Transactions Generated!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            ElseIf Me.cb_TransactionDate.SelectedIndex = -1 Then
                MessageBox.Show("Specify the input", "Please select the Allocation Date", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            'Get the \ Participants and Deffered Amount
            'Consider the beginning transaction of payment
            If Me.cb_TransactionDate.Items.Count = 1 Then
                dicParticipants = WbillHelper.GetDeferredParticipant(CDate(Me.cb_TransactionDate.Text))
                dicCurrentDeferred = WbillHelper.GetDeferredPayment(CDate(Me.cb_TransactionDate.Text))

                For Each item In dicParticipants
                    If dicCurrentDeferred.ContainsKey(item.Key) Then
                        Dim itemDeferred As New DeferredMonitoringReport

                        With dicCurrentDeferred(item.Key)
                            itemDeferred.IDNumber = item.Value
                            itemDeferred.CurrentEnergy = .Energy
                            itemDeferred.CurrentVAT = .VAT
                            itemDeferred.TransactionDate = .TransactionDate
                        End With

                        listOfDeferred.Add(itemDeferred)
                    End If
                Next
            Else
                PreviousDate = CDate(Me.cb_TransactionDate.Items(Me.cb_TransactionDate.SelectedIndex - 1))
                CurrentDate = CDate(Me.cb_TransactionDate.Text)
                dicParticipants = WbillHelper.GetDeferredParticipant(PreviousDate, CurrentDate)

                dicPreviousDeferred = WbillHelper.GetDeferredPayment(PreviousDate)
                dicCurrentDeferred = WbillHelper.GetDeferredPayment(CurrentDate)

                For Each item In dicParticipants
                    Dim itemDeferred As New DeferredMonitoringReport

                    If dicPreviousDeferred.ContainsKey(item.Key) Then
                        With dicPreviousDeferred(item.Key)
                            itemDeferred.IDNumber = item.Value
                            itemDeferred.OldEnergy = .Energy
                            itemDeferred.OldVAT = .VAT
                            itemDeferred.TransactionDate = .TransactionDate
                        End With
                    End If

                    If dicCurrentDeferred.ContainsKey(item.Key) Then
                        With dicCurrentDeferred(item.Key)
                            itemDeferred.IDNumber = item.Value
                            itemDeferred.CurrentEnergy = .Energy
                            itemDeferred.CurrentVAT = .VAT
                            itemDeferred.TransactionDate = .TransactionDate
                        End With
                    End If

                    listOfDeferred.Add(itemDeferred)
                Next
            End If

            If listOfDeferred.Count = 0 Then
                MessageBox.Show("No data", "No Transactions Generated!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            result = BFactory.GenerateDeferredMonitoringReport(New DSReport.DeferredMonitoringDataTable, listOfDeferred)

            With rptView
                .LoadDeferredMonitoringReport(result)                
                .ShowDialog()
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepDebitCreditMemoWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInGeneratingReport.ToString, AMModule.UserName)        
        End Try

    End Sub

    Private Sub cmd_close_Click(sender As Object, e As EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub

    Private Sub cmd_Gen_Save_Main_Click(sender As Object, e As EventArgs) Handles cmd_Gen_Save_Main.Click
        'Kevin 10/10/2015 
        'Updated by Lance 11/13/2015
        Dim DtResult As New DataTable
        Dim rptView As New frmReportViewer

        Dim CurrDeferredPaymentMain As List(Of DeferredMain) = WbillHelper.GetCurrentDeferredPayementMain(CDate(cb_TransactionDate.Text))
        Dim GetPaymentNo As Long = (From x In CurrDeferredPaymentMain _
                                    Select x.PaymentNo).Distinct.FirstOrDefault

        Dim PrevDeferredPaymentMain As List(Of DeferredMain) = WbillHelper.GetPrevDeferredPayementMain(CLng(GetPaymentNo - 1))
        Dim AMParticipantsList As List(Of AMParticipants) = WbillHelper.GetAMParticipants
        Dim _DeferredPaymentProc As New DeferredPaymentProcess
        Dim DSDT As New DSReport.DeferredMonitoringDataTable

        Dim DeferredDT As DataTable = _DeferredPaymentProc.GenerateDeferredPaymentsDT(DSDT, _
                                                                                    CDate(cb_TransactionDate.Text), _
                                                                                    CurrDeferredPaymentMain, _
                                                                                    PrevDeferredPaymentMain, _
                                                                                    AMParticipantsList)
        'DtResult = WbillHelper.GetDeferredPaymentMain(cb_TransactionDate.Text)
        With rptView
            .LoadDeferredMonitoringReport(DeferredDT)
            .WindowState = FormWindowState.Maximized
            .ShowDialog()
        End With
    End Sub   
End Class