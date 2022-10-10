'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmCollectionDetails
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     April 13, 2012
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
'   April 13, 2012          Vladimir E. Espiritu                 GUI design and basic functionalities                    
'


Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmCollectionDetails

#Region "Properties"
    Private _itemWESMBill As WESMBill
    Public Property itemWESMBill() As WESMBill
        Get
            Return _itemWESMBill
        End Get
        Set(ByVal value As WESMBill)
            _itemWESMBill = value
        End Set
    End Property

    Private _listDMCM As List(Of DebitCreditMemo)
    Public Property listDMCM() As List(Of DebitCreditMemo)
        Get
            Return _listDMCM
        End Get
        Set(ByVal value As List(Of DebitCreditMemo))
            _listDMCM = value
        End Set
    End Property

    Private _listParticipants As List(Of AMParticipants)
    Public Property listParticipants() As List(Of AMParticipants)
        Get
            Return _listParticipants
        End Get
        Set(ByVal value As List(Of AMParticipants))
            _listParticipants = value
        End Set
    End Property

    Private _listAccountingCodes As List(Of AccountingCode)
    Public Property listAccountingCodes() As List(Of AccountingCode)
        Get
            Return _listAccountingCodes
        End Get
        Set(ByVal value As List(Of AccountingCode))
            _listAccountingCodes = value
        End Set
    End Property

#End Region
    
    Private Sub frmCollectionDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.itemWESMBill.InvoiceNumber <> "" Then
            Me.LoadWESMBill()
        End If

        If Me.listDMCM.Count <> 0 Then
            Me.LoadDMCM()
            Me.LoadDMCMDetails()
        End If
    End Sub

    Private Sub LoadWESMBill()

        Dim participant = (From x In Me.listParticipants _
                           Where x.IDNumber = CStr(Me.itemWESMBill.IDNumber) _
                           Select x).First()

        With Me.itemWESMBill
            Me.txtParticipantID.Text = participant.ParticipantID
            Me.txtBatchCode.Text = .BatchCode
            Me.txtBillingPeriod.Text = .BillingPeriod.ToString()
            Me.txtStlRun.Text = .SettlementRun
            Me.txtInvoiceNo.Text = .InvoiceNumber.ToString()
            Me.txtInvoiceDate.Text = .InvoiceDate.ToString("MM/dd/yyyy")
            Me.txtAmount.Text = FormatNumber(.Amount, 2)
            Me.txtDueDate.Text = .DueDate.ToString("MM/dd/yyyy")
        End With
    End Sub

    Private Sub LoadDMCM()
        Me.DGridViewMain.Rows.Clear()

        Dim itemsDMCM = From x In Me.listDMCM Join y In Me.listParticipants _
                        On x.IDNumber Equals y.IDNumber _
                        Select x, y.ParticipantID

        For Each item In itemsDMCM
            With item

                Dim total = (From x In item.x.DMCMDetails Select x.Debit).Sum()

                Me.DGridViewMain.Rows.Insert(0, .x.DMCMNumber, .x.JVNumber, .ParticipantID, _
                                             .x.Particulars, FormatNumber(total, 2), .x.PreparedBy, _
                                             .x.CheckedBy, .x.ApprovedBy, .x.UpdatedDate)
            End With
        Next

    End Sub

    Private Sub DGridViewMain_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGridViewMain.CellClick
        If Me.DGridViewMain.Rows.Count = 0 Then
            Exit Sub
        End If

        Me.LoadDMCMDetails()
    End Sub

    Private Sub LoadDMCMDetails()
        Me.DGridViewDetails.Rows.Clear()
        Dim dmcmno = CInt(Me.DGridViewMain.CurrentRow.Cells("colDMCMNo").Value)

        Dim itemDMCM = (From x In Me.listDMCM _
                        Where x.DMCMNumber = dmcmno _
                        Select x).First()

        Dim itemDMCMDetails = From x In itemDMCM.DMCMDetails Join y In Me.listAccountingCodes _
                               On x.AccountCode Equals y.AccountCode _
                               Select x, y.Description

        For Each item In itemDMCMDetails
            With item
                Me.DGridViewDetails.Rows.Add(.x.SummaryType.ToString() & "-" & .x.InvDMCMNo.ToString(), .x.IDNumber.ParticipantID, _
                                             .x.AccountCode, .Description, FormatNumber(.x.Debit, 2), FormatNumber(.x.Credit, 2))
            End With
        Next
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class