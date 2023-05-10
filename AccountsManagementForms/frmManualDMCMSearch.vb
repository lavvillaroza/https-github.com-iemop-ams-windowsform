'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmManualDMCMSearch
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
Imports System.Runtime.Serialization.Formatters.Binary
'Imports LDAPLib
'Imports LDAPLogin

Public Class frmManualDMCMSearch
    Public Enum EnumManualDMCMSearch
        AccountCode
        SelectWESMBillSummaryPayable
        SelectWESMBillSummaryReceivable
        InsertWESMBillSummary
    End Enum

#Region "Properties"
    Private _LoadType As EnumManualDMCMSearch
    Public Property LoadType() As EnumManualDMCMSearch
        Get
            Return _LoadType
        End Get
        Set(ByVal value As EnumManualDMCMSearch)
            _LoadType = value
        End Set
    End Property

    Private _IDNumber As Long
    Public Property IDNumber() As Long
        Get
            Return _IDNumber
        End Get
        Set(ByVal value As Long)
            _IDNumber = value
        End Set
    End Property

    Private _ChargeType As EnumChargeType
    Public Property ChargeType() As EnumChargeType
        Get
            Return _ChargeType
        End Get
        Set(ByVal value As EnumChargeType)
            _ChargeType = value
        End Set
    End Property


#End Region

    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory

    Private Sub frmManualDMCMSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName
            BFactory = BusinessFactory.GetInstance()

            Select Case Me.LoadType
                Case EnumManualDMCMSearch.AccountCode
                    Me.Text = "Select the chart of accounts"

                    'Get the Accounting Code
                    Dim listAcctCode = WBillHelper.GetAccountingCodes()

                    'Get the accounting code of the selected charge type
                    Dim listSelectedAcct = Me.GetListOfAccountCodes()

                    Dim listItems = From x In listAcctCode Join y In listSelectedAcct _
                                    On x.AccountCode Equals y Select x

                    Dim dt As New DataTable
                    dt.Columns.Add("AccountCode", GetType(String))
                    dt.Columns.Add("Description", GetType(String))
                    dt.AcceptChanges()

                    For Each item In listItems
                        Dim row = dt.NewRow
                        row("AccountCode") = item.AccountCode
                        row("Description") = item.Description
                        dt.Rows.Add(row)
                    Next
                    dt.AcceptChanges()

                    With Me.DGridView
                        .DataSource = dt
                        .Columns(0).Width = 120
                        .Columns(1).Width = 300
                    End With

                    Dim valSize As New System.Drawing.Size
                    valSize.Width = 540
                    valSize.Height = 600
                    Me.Size = valSize
                    Me.gpDMCMDetails.Visible = False

                Case EnumManualDMCMSearch.SelectWESMBillSummaryPayable, EnumManualDMCMSearch.SelectWESMBillSummaryReceivable
                    Me.Text = "Select the oustanding balance to adjust"

                    Dim valSize As New System.Drawing.Size
                    valSize.Width = 750
                    valSize.Height = 600
                    Me.Size = valSize
                    Me.gpDMCMDetails.Visible = False

                    Dim listSummary As New List(Of WESMBillSummary)

                    Dim dt As New DataTable
                    With dt.Columns
                        .Add("WESMBillSummaryNo", GetType(Long))
                        .Add("BillingPeriod", GetType(Integer))
                        .Add("DueDate", GetType(String))
                        .Add("ChargeType", GetType(String))
                        .Add("ReferenceDocument", GetType(String))
                        .Add("IDType", GetType(String))
                        .Add("EndingBalance", GetType(String))
                    End With
                    dt.AcceptChanges()

                    If Me.LoadType = EnumManualDMCMSearch.SelectWESMBillSummaryPayable Then
                        listSummary = WBillHelper.GetWESMBillSummaryPerParticipant(Me.IDNumber, Me.ChargeType, True)
                    Else
                        listSummary = WBillHelper.GetWESMBillSummaryPerParticipant(Me.IDNumber, Me.ChargeType, False)
                    End If

                    If listSummary.Count = 0 Then
                        MsgBox("No outstanding balance for the selected participant to be deducted or added!", MsgBoxStyle.Exclamation, "No data")
                        Me.Close()
                    End If

                    For Each item In listSummary
                        Dim row = dt.NewRow()
                        With item
                            row("WESMBillSummaryNo") = .WESMBillSummaryNo
                            row("BillingPeriod") = .BillPeriod
                            row("DueDate") = .DueDate.ToString("MM/dd/yyyy")
                            row("ReferenceDocument") = .SummaryType.ToString() & "-" & .INVDMCMNo.ToString()
                            row("IDType") = .IDType
                            row("EndingBalance") = FormatNumber(.EndingBalance, 2)

                            Select Case Me.ChargeType
                                Case EnumChargeType.E
                                    row("ChargeType") = "Energy"
                                Case EnumChargeType.EV
                                    row("ChargeType") = "VAT on Energy"
                                Case EnumChargeType.MF
                                    row("ChargeType") = "Market Fees"
                                Case EnumChargeType.MFV
                                    row("ChargeType") = "VAT on Market Fees"
                            End Select
                        End With
                        dt.Rows.Add(row)
                    Next
                    dt.AcceptChanges()

                    With Me.DGridView
                        .DataSource = dt
                        .Columns(0).Visible = False
                        .Columns(4).Width = 130
                        .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
                    End With

                Case EnumManualDMCMSearch.InsertWESMBillSummary
                    Me.Text = "Specify the additional details"

                    'Get the Calendar BP
                    Dim listBP = WBillHelper.GetCalendarBP()
                    Dim listItems = (From x In listBP Select x Order By x.BillingDate Descending).ToList()

                    With Me.ddlBillingPeriod
                        .DisplayMember = "BillingPeriod"
                        .ValueMember = "BillingPeriod"
                        .DataSource = listItems
                        .SelectedIndex = -1
                    End With

                    Dim valSize As New System.Drawing.Size
                    valSize.Width = 307
                    valSize.Height = 182
                    Me.Size = valSize
            End Select
        Catch ex As Exception
            'LDAPModule.LDAP.InsertLog(Me.Name, EnumAMSModules.AMS_ManualDMCMWindow.ToString(), ex.Message, Me.LoadType.ToString(), "Manual DMCM Search Window", EnumColorCode.Red.ToString(), EnumLogType.ErrorInAccessing.ToString(), 'LDAPModule.LDAP.Username)
        End Try
      
    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        If Not Me.ValidateControls() Then
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Function ValidateControls() As Boolean
        Select Case Me.LoadType
            Case EnumManualDMCMSearch.AccountCode


            Case EnumManualDMCMSearch.SelectWESMBillSummaryPayable

            Case EnumManualDMCMSearch.SelectWESMBillSummaryReceivable

            Case EnumManualDMCMSearch.InsertWESMBillSummary
                If Me.ddlBillingPeriod.SelectedIndex = -1 Then
                    MsgBox("Please select the billing period!", MsgBoxStyle.Critical, "Specify the inputs")
                    Return False
                End If
        End Select

        Return True
    End Function

    Private Function GetListOfAccountCodes() As List(Of String)
        Dim result As New List(Of String)

        Select Case Me.ChargeType
            Case EnumChargeType.MF
                result = Split(AMModule.ManualDMCMMarketFees, ",").ToList()

            Case EnumChargeType.MFV
                result = Split(AMModule.ManualDMCMVATonMarketFees, ",").ToList()

            Case EnumChargeType.E
                result = Split(AMModule.ManualDMCMEnergy, ",").ToList()

            Case EnumChargeType.EV
                result = Split(AMModule.ManualDMCMVATonEnergy, ",").ToList()

        End Select

        Return result
    End Function
End Class