'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmFTF
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     April 26, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Collection
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   April 26, 2012          Vladimir E. Espiritu            Class initialization
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmFTF

    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory
    Private ListFTF As List(Of FundTransferFormMain)

    Private Sub frmFTF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm

        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName
        BFactory = BusinessFactory.GetInstance()

        With Me.ddlTransType
            .Items.Add("DrawDown")
            .Items.Add("Replenishment")
            .Items.Add("TransferPEMCAccount")
            .Items.Add("TransferSTLToNSS")
            .Items.Add("TransferNSSToSTL")
            .Items.Add("TransferMarketFeesToPEMC")
            .Items.Add(EnumFTFTransType.TransferMarketFeesToSTL.ToString)
        End With
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        If Not Me.Validation Then
            Exit Sub
        End If

        Me.DGridViewMain.Rows.Clear()
        Me.DGridViewParticipant.Rows.Clear()
        Me.DGridViewDetails.Rows.Clear()

        Dim dateFrom As Date = CDate(FormatDateTime(Me.dtDateFrom.Value, DateFormat.ShortDate))
        Dim dateTo As Date = CDate(FormatDateTime(Me.dtDateTo.Value, DateFormat.ShortDate))
        Dim transType As EnumFTFTransType

        Select Case Me.ddlTransType.SelectedIndex
            Case 0
                transType = EnumFTFTransType.DrawDown
            Case 1
                transType = EnumFTFTransType.Replenishment
            Case 2
                transType = EnumFTFTransType.TransferPEMCAccount
            Case 3
                transType = EnumFTFTransType.TransferSTLToNSS
            Case 4
                transType = EnumFTFTransType.TransferNSSToSTL
            Case 5
                transType = EnumFTFTransType.TransferMarketFeesToPEMC
            Case 6
                transType = EnumFTFTransType.TransferMarketFeesToSTL
        End Select

        Me.ListFTF = WBillHelper.GetFundTransferForm(dateFrom, dateTo, transType)

        If Me.ListFTF.Count = 0 Then
            MsgBox("No records found!", MsgBoxStyle.Exclamation, "No data")
            Exit Sub
        End If

        Me.LoadFTFMain()
        Me.LoadFTFParticipantsAndDetails()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        If Me.DGridViewMain.Rows.Count = 0 Then
            MsgBox("Nothing to print!", MsgBoxStyle.Critical, "No data")
            Exit Sub
        End If

        If Me.DGridViewMain.SelectedRows.Count = 0 Then
            MsgBox("Please select first the item to print", MsgBoxStyle.Critical, "Select the record")
            Exit Sub
        End If

        'Signatories
        Dim refNoValue = CLng(Me.DGridViewMain.CurrentRow.Cells("colRefNoMain").Value)

        Dim itemSignatory As New DocSignatories

        Dim itemFTF = WBillHelper.GetFundTransferForm(refNoValue).First()

        If itemFTF.TransType = EnumFTFTransType.TransferMarketFeesToPEMC Or itemFTF.TransType = EnumFTFTransType.TransferPEMCAccount Then
            itemSignatory = WBillHelper.GetSignatories("FTF2").First()
        Else
            itemSignatory = WBillHelper.GetSignatories("FTF").First()
        End If

        Dim dtMain As New DSReport.FTFMainDataTable
        Dim dtParticipant As New DSReport.FTFParticipantDataTable
        Dim dtDetails As New DSReport.FTFDetailsDataTable

        Dim ds = BFactory.GenerateFundTransferFormReport(dtMain, dtParticipant, dtDetails, itemFTF, itemSignatory)

        Dim frmViewer As New frmReportViewer()
        With frmViewer
            .LoadFTF(ds)            
            .ShowDialog()
        End With
    End Sub

    Private Sub DGridViewMain_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGridViewMain.CellClick
        If Me.DGridViewMain.RowCount = 0 Then
            Exit Sub
        End If

        Me.LoadFTFParticipantsAndDetails()
    End Sub

    Private Sub DGridViewMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
                                  Handles DGridViewMain.KeyDown, DGridViewMain.KeyUp

        If Me.DGridViewMain.RowCount = 0 Then
            Exit Sub
        End If

        Me.LoadFTFParticipantsAndDetails()
    End Sub

#Region "Functions/Method"

    Private Function Validation() As Boolean
        If CDate(Me.dtDateFrom.Value.ToString("MM/dd/yyyy")) > CDate(Me.dtDateTo.Value.ToString("MM/dd/yyyy")) Then
            MsgBox("Invalid date range!", MsgBoxStyle.Critical, "Specify the inputs")
            Me.dtDateFrom.Select()
            Exit Function
        ElseIf Me.ddlTransType.SelectedIndex = -1 Then
            MsgBox("Please select the transaction type!", MsgBoxStyle.Critical, "Specify the inputs")
            Me.ddlTransType.Select()
            Exit Function
        End If

        Return True
    End Function

    Private Sub LoadFTFMain()
        Me.DGridViewMain.Rows.Clear()

        For Each item In Me.ListFTF
            With item
                Me.DGridViewMain.Rows.Add(.RefNo, .AllocationDate.ToString("MM/dd/yyyy"), .BatchCode, .DRDate.ToString("MM/dd/yyyy"), _
                                          .CRDate.ToString("MM/dd/yyyy"), .TotalAmount)

            End With
        Next
    End Sub

    Private Sub LoadFTFParticipantsAndDetails()
        If Me.DGridViewMain.Rows.Count = 0 Then
            Exit Sub
        End If

        Me.DGridViewParticipant.Rows.Clear()
        Me.DGridViewDetails.Rows.Clear()

        Dim refNoValue = CLng(Me.DGridViewMain.CurrentRow.Cells("colRefNoMain").Value)

        Dim listFTFParticipant = From x In Me.ListFTF _
                                 Where x.RefNo = refNoValue _
                                 Select x.ListOfFTFParticipants

        Dim listFTFDetails = From x In Me.ListFTF _
                             Where x.RefNo = refNoValue _
                             Select x.ListOfFTFDetails

        If listFTFParticipant.Count > 0 Then
            For Each item In listFTFParticipant.First()
                With item
                    Me.DGridViewParticipant.Rows.Add(.RefNo, .IDNumber.IDNumber, .IDNumber.ParticipantID, .Amount)
                End With
            Next
        End If

        If listFTFDetails.Count > 0 Then
            For Each item In listFTFDetails.First()
                With item
                    Me.DGridViewDetails.Rows.Add(.RefNo, .BankAccountNo, "", "", .Debit, .Credit, "")
                End With
            Next
        End If
    End Sub

#End Region


End Class