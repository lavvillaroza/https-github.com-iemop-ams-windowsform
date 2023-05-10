'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmNSSMonitoringSearch
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     February 11, 2013
'Development Group:      Software Development and Support Division
'Description:            GUI for the NSS Monitoring Search
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   February 11, 2013       Vladimir E. Espiritu                 GUI design and basic functionalities                    
'

Option Explicit On
Option Strict On

Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
'Imports LDAPLib
'Imports LDAPLogin

Public Class frmNSSMonitoringSearch
    Private WBillHelper As WESMBillHelper
    Dim _ListBP As List(Of CalendarBillingPeriod)

#Region "Properties"
    Private _ListOfSelectedBillingPeriod As List(Of CalendarBillingPeriod)
    Public ReadOnly Property ListOfSelectedBillingPeriod() As List(Of CalendarBillingPeriod)
        Get
            If Me.ddlBillingPeriodFrom.SelectedIndex = -1 Or Me.ddlBillingPeriodTo.SelectedIndex = -1 Then
                _ListOfSelectedBillingPeriod = New List(Of CalendarBillingPeriod)
                Return _ListOfSelectedBillingPeriod
            End If

            Dim billFrom As Integer = CInt(Me.ddlBillingPeriodFrom.Text)
            Dim billTo As Integer = CInt(Me.ddlBillingPeriodTo.Text)

            _ListOfSelectedBillingPeriod = (From x In Me._ListBP _
                                            Where x.BillingPeriod >= billFrom And x.BillingPeriod <= billTo _
                                            Select x Order By x.BillingPeriod).ToList()

            Return _ListOfSelectedBillingPeriod
        End Get
    End Property

    Private _ListOfNetSettlementSurplusMain As List(Of NetSettlementSurplusMain)
    Public ReadOnly Property ListOfNetSettlementSurplusMain() As List(Of NetSettlementSurplusMain)
        Get
            _ListOfNetSettlementSurplusMain = New List(Of NetSettlementSurplusMain)
            For index As Integer = 0 To Me.DGridView.RowCount - 1
                With Me.DGridView.Rows(index)
                    Dim item As New NetSettlementSurplusMain

                    item.BillingPeriod = New CalendarBillingPeriod(CInt(.Cells("colBillingPeriod").Value), _
                                                                   CDate(.Cells("colStartDate").Value), _
                                                                   CDate(.Cells("colEndDate").Value), _
                                                                   CDate(.Cells("colBillingDate").Value))
                    item.Interest = CDec(.Cells("colInterestRate").Value)
                    item.InterestOnInterest = CDec(.Cells("colInterestRateOnInterest").Value)
                    _ListOfNetSettlementSurplusMain.Add(item)
                End With
            Next
            _ListOfNetSettlementSurplusMain.TrimExcess()

            Return _ListOfNetSettlementSurplusMain
        End Get
    End Property

#End Region

    Private Sub frmNSSMonitoringSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName

            'Get the billing period for all uploaded bill
            Me._ListBP = WBillHelper.GetBillingPeriodsNotYetComputedForNSSInterest()

            If Me._ListBP.Count = 0 Then
                MsgBox("No uploaded WESM Bill", MsgBoxStyle.Exclamation, "No data")
                Exit Sub
            End If

            For Each item In Me._ListBP
                Me.ddlBillingPeriodFrom.Items.Add(item.BillingPeriod)
                Me.ddlBillingPeriodTo.Items.Add(item.BillingPeriod)
            Next
            Me.ddlBillingPeriodFrom.SelectedIndex = -1
            Me.ddlBillingPeriodTo.SelectedIndex = -1

            AddHandler Me.ddlBillingPeriodFrom.SelectedIndexChanged, AddressOf Me.ddlBillingPeriodFrom_SelectedIndexChanged
            AddHandler Me.ddlBillingPeriodTo.SelectedIndexChanged, AddressOf Me.ddlBillingPeriodTo_SelectedIndexChanged
        Catch ex As Exception
            'LDAPModule.LDAP.InsertLog(Me.Name, EnumAMSModules.AMS_NSS_InterestWindow.ToString(), "Error in searching window", ex.Message, "", EnumColorCode.Red.ToString(), EnumLogType.ErrorInRetrieving.ToString(), 'LDAPModule.LDAP.Username)
        End Try
       
    End Sub

    Private Sub ddlBillingPeriodFrom_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.ddlBillingPeriodFrom.SelectedIndex = -1 Then
            Exit Sub
        End If

        Dim selectedItem = CInt(Me.ddlBillingPeriodFrom.Text)
        Dim item = (From x In Me._ListBP _
                    Where x.BillingPeriod = selectedItem _
                    Select x).First()
        Me.txtBillingPeriodFrom.Text = item.StartDate.ToString("MM/dd/yyyy") & " to " & item.EndDate.ToString("MM/dd/yyyy")
        Me.GenerateDatagridView()
    End Sub

    Private Sub ddlBillingPeriodTo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.ddlBillingPeriodTo.SelectedIndex = -1 Then
            Exit Sub
        End If

        Dim selectedItem = CInt(Me.ddlBillingPeriodTo.Text)
        Dim item = (From x In Me._ListBP _
                    Where x.BillingPeriod = selectedItem _
                    Select x).First()
        Me.txtBilligPeriodTo.Text = item.StartDate.ToString("MM/dd/yyyy") & " to " & item.EndDate.ToString("MM/dd/yyyy")
        Me.GenerateDatagridView()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If Not Me.ValidateControls Then
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

#Region "Functions/Methods"
    Private Sub GenerateDatagridView()
        Me.DGridView.Rows.Clear()
        For Each item In Me.ListOfSelectedBillingPeriod
            Me.DGridView.Rows.Add(item.BillingPeriod, item.StartDate, item.EndDate, item.BillingDate, _
                                  item.StartDate.ToString("MM/dd/yyyy") & " to " & _
                                  item.EndDate.ToString("MM/dd/yyyy"), "0.00", "0.00")
        Next
    End Sub

    Private Function ValidateControls() As Boolean
        If Me.ddlBillingPeriodFrom.SelectedIndex = -1 Then
            MsgBox("Please specify the Billing Period for From field!", MsgBoxStyle.Critical, "Specify the inputs")
            Return False
        ElseIf Me.ddlBillingPeriodTo.SelectedIndex = -1 Then
            MsgBox("Please specify the Billing Period for To field!", MsgBoxStyle.Critical, "Specify the inputs")
            Return False
        ElseIf CInt(Me.ddlBillingPeriodFrom.Text) > CInt(Me.ddlBillingPeriodTo.Text) Then
            MsgBox("Invalid Billing Period Range!", MsgBoxStyle.Critical, "Specify the inputs")
            Return False
        End If

        Return True
    End Function

#End Region


End Class