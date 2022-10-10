'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmDebitCreditMemo
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     March 04, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for tracking of WESM Bill
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   March 04, 2012          Vladimir E. Espiritu            GUI initialization
'


Option Explicit On
Option Strict On

Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmUploadedWESMBillTracking
    Private listOfAMParticipants As List(Of AMParticipants)
    Private listOfDMCM As List(Of DebitCreditMemo)
    Private listOfAccountCode As List(Of AccountingCode)
    Private WBillHelper As WESMBillHelper

    Private Sub txtSearcho_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.GotFocus
        Me.txtSearch.Text = ""
        Me.txtSearch.Font = New System.Drawing.Font("Helvetica", 8.5, FontStyle.Regular)
        Me.txtSearch.ForeColor = Color.Black
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Me.SearchInvoice()
        End If
    End Sub

    Private Sub txtSearch_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.LostFocus
        If txtSearch.Text.Trim.Length = 0 Then
            Me.txtSearch.Text = "Type WESM Invoice No. here"
            Me.txtSearch.Font = New System.Drawing.Font("Helvetica", 8.5, FontStyle.Italic)
            Me.txtSearch.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub DGridViewDMCMMain_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGridViewDMCMMain.CellClick
        If Me.DGridViewDMCMMain.RowCount = 0 Or e.RowIndex = -1 Then
            Exit Sub
        End If

        If Me.DGridViewDMCMMain.RowCount <> 0 Then
            Me.LoadDMCMDetails(CLng(Me.DGridViewDMCMMain.Rows(e.RowIndex).Cells("colDMCMNo").Value))
        End If
    End Sub

    Private Sub DGridViewDMCMDetails_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGridViewDMCMDetails.CellFormatting
        If Me.DGridViewDMCMDetails.Columns(e.ColumnIndex).Name = "colInvDMCM" Then
            If e.Value.ToString() = Me.txtInvoiceNo.Text And _
                CType(System.Enum.Parse(GetType(EnumSummaryType), CStr(Me.DGridViewDMCMDetails.Rows(e.RowIndex).Cells("colType").Value)), EnumSummaryType) = EnumSummaryType.INV Then

                Me.DGridViewDMCMDetails.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Yellow
                Me.DGridViewDMCMDetails.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Brown
            End If
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.SearchInvoice()
    End Sub

    Private Sub DGridViewCollection_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGridViewCollection.CellClick
        If Me.DGridViewCollection.RowCount = 0 Or e.RowIndex = -1 Then
            Exit Sub
        End If

        Dim colNo = CLng(Me.DGridViewCollection.Rows(e.RowIndex).Cells("colCollectionNo").Value)

        Dim items = WBillHelper.GetCollectionAllocation(colNo)

        Me.LoadCollectionAllocations(items)
    End Sub

    Private Sub DGridViewColAllocation_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGridViewColAllocation.CellFormatting
        If Me.DGridViewColAllocation.Columns(e.ColumnIndex).Name = "colWESMBillSummaryNo" Then
            If e.Value.ToString() = Me.txtWESMBillSummaryNo.Text Then

                Me.DGridViewColAllocation.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Yellow
                Me.DGridViewColAllocation.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Brown
            End If
        End If
    End Sub

    Private Sub TabInvoice_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles TabInvoice.DrawItem
        'Dim g As Graphics = e.Graphics
        'Dim font As Font = TabInvoice.Font
        'Dim brush As New SolidBrush(Color.Black)
        'Dim tabTextArea As RectangleF = RectangleF.op_Implicit(TabInvoice.GetTabRect(e.Index))
        'If TabInvoice.SelectedIndex = e.Index Then
        '    font = New Font(font, FontStyle.Bold)
        '    brush = New SolidBrush(Color.Blue)
        'Else
        '    font = New Font(font, FontStyle.Regular)
        '    brush = New SolidBrush(Color.DarkGreen)
        'End If
        'g.DrawString(TabInvoice.TabPages(e.Index).Text, font, brush, tabTextArea)

        Dim g As Graphics = e.Graphics
        Dim tp As TabPage = TabInvoice.TabPages(e.Index)
        Dim br As Brush
        Dim sf As New StringFormat

        Dim r As New RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height - 2)

        sf.Alignment = StringAlignment.Center

        Dim strTitle As String = tp.Text

        'If the current index is the Selected Index, change the color
        If TabInvoice.SelectedIndex = e.Index Then

            'this is the background color of the tabpage header
            br = New SolidBrush(Color.LightSteelBlue) ' chnge to your choice
            g.FillRectangle(br, e.Bounds)

            'this is the foreground color of the text in the tab header
            br = New SolidBrush(Color.Black) ' change to your choice
            g.DrawString(strTitle, TabInvoice.Font, br, r, sf)

        Else

            'these are the colors for the unselected tab pages
            br = New SolidBrush(Color.LightBlue) ' Change this to your preference
            g.FillRectangle(br, e.Bounds)
            br = New SolidBrush(Color.Black)
            g.DrawString(strTitle, TabInvoice.Font, br, r, sf)

        End If

    End Sub

#Region "Methods/Functions"
    Public Sub LoadInputs()
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"

        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        Me.listOfAMParticipants = WBillHelper.GetAMParticipants()
        Me.listOfAccountCode = WBillHelper.GetAccountingCodes()
    End Sub

    Public Sub LoadWESMInvoice(ByVal InvoiceNumber As Long, ByVal ChargeType As EnumChargeType)
        Dim item = WBillHelper.GetWESMBill(InvoiceNumber.ToString(), ChargeType)

        If item.InvoiceNumber = "" Then
            MsgBox("The WESM Bill does not exist!", MsgBoxStyle.Information, "Warning")
            Exit Sub
        End If

        Me.ClearControls()

        Me.LoadWESMBill(item)
        Me.listOfDMCM = WBillHelper.GetDebitCreditMemoMainInvoice(InvoiceNumber, ChargeType)
        Me.LoadDMCMMain(Me.listOfDMCM)
        Me.LoadWESMBillSummary(WBillHelper.GetWESMBillSummary(item.AMCode))
        'Me.LoadCollections(WBillHelper.GetCollections(CLng(Me.txtWESMBillSummaryNo.Text)))

        If Me.DGridViewDMCMMain.RowCount <> 0 Then
            Me.LoadDMCMDetails(CLng(Me.DGridViewDMCMMain.Rows(0).Cells("colDMCMNo").Value))
        End If

        If Me.DGridViewCollection.RowCount <> 0 Then
            Dim colNo = CLng(Me.DGridViewCollection.Rows(0).Cells("colCollectionNo").Value)
            Dim items = WBillHelper.GetCollectionAllocation(colNo)
            Me.LoadCollectionAllocations(items)
        End If
    End Sub

    Private Sub LoadWESMBill(ByVal item As WESMBill)

        '08/27/2013 Vloody
        Dim participant = (From x In Me.listOfAMParticipants _
                           Where x.IDNumber = CStr(item.IDNumber) _
                           Select x).First()
        With item
            Me.txtInvoiceNo.Text = CStr(.InvoiceNumber)
            Me.txtParticipantID.Text = participant.ParticipantID
            Me.txtBatchCode.Text = .BatchCode
            Me.txtBillingPeriod.Text = CStr(.BillingPeriod)
            Me.txtStlRun.Text = .SettlementRun
            Me.txtInvoiceDate.Text = FormatDateTime(.InvoiceDate, DateFormat.ShortDate)
            Me.txtAmount.Text = .Amount.ToString("#,###.#0")
            Me.txtDueDate.Text = FormatDateTime(.DueDate, DateFormat.ShortDate)
        End With
    End Sub

    Private Sub LoadDMCMMain(ByVal items As List(Of DebitCreditMemo))

        Dim listItems = From x In items Join y In Me.listOfAMParticipants _
                        On x.IDNumber Equals y.IDNumber _
                        Select x, y.ParticipantID

        Me.DGridViewDMCMMain.Rows.Clear()
        For Each itemDMCM In listItems
            Dim amount = (From x In itemDMCM.x.DMCMDetails _
                          Select x.Debit).Sum()
            With itemDMCM
                Me.DGridViewDMCMMain.Rows.Add(.x.DMCMNumber, .x.JVNumber, .ParticipantID, .x.Particulars, _
                                              amount.ToString("#,###.#0"), .x.PreparedBy, .x.CheckedBy, .x.ApprovedBy, .x.UpdatedBy)
            End With
        Next
    End Sub

    Private Sub LoadDMCMDetails(ByVal DMCMNo As Long)
        Dim item = (From x In Me.listOfDMCM _
                    Where x.DMCMNumber = DMCMNo _
                    Select x).First()

        Dim listItems = From x In item.DMCMDetails Join y In Me.listOfAccountCode _
                        On x.AccountCode Equals y.AccountCode _
                        Select x, y.Description

        Me.DGridViewDMCMDetails.Rows.Clear()
        For Each i In listItems
            With i
                Me.DGridViewDMCMDetails.Rows.Add(.x.SummaryType, .x.InvDMCMNo, .x.AccountCode, _
                                                 .Description, .x.Debit.ToString("#,###.#0"), .x.Credit.ToString("#,###.#0"))
            End With
        Next
    End Sub

    Private Sub LoadWESMBillSummary(ByVal items As List(Of WESMBillSummary))
        Dim item As New WESMBillSummary

        Dim cntParent = (From x In items Join y In Me.listOfAMParticipants _
                         On x.IDNumber.IDNumber Equals y.IDNumber _
                         Where y.ParticipantID = Me.txtParticipantID.Text _
                         Select x).Count()
        If cntParent = 0 Then
            item = (From x In items _
                    Where x.IDType = EnumIDType.C.ToString() _
                    Select x).First()
            Me.txtBalance.Text = "0.00"
        Else
            Dim cntInv = (From x In items _
                          Where x.SummaryType = EnumSummaryType.INV And x.INVDMCMNo = CStr(Me.txtInvoiceNo.Text) _
                          Select x).Count()
            If cntInv = 1 Then
                item = (From x In items _
                        Where x.SummaryType = EnumSummaryType.INV And x.INVDMCMNo = CStr(Me.txtInvoiceNo.Text) _
                        Select x).First()
                Me.txtBalance.Text = item.EndingBalance.ToString("#,###.#0")
            Else
                'Loop each WESM Bill Summary
                For Each itemSummary In items
                    If itemSummary.IDType = EnumIDType.P.ToString() Then
                        Dim inv = itemSummary.INVDMCMNo
                        For Each itemDMCMMain In Me.listOfDMCM
                            Dim i = (From x In itemDMCMMain.DMCMDetails _
                                     Where x.SummaryType = EnumSummaryType.INV And x.InvDMCMNo = inv _
                                     Select x).Count()
                            Dim j = (From x In itemDMCMMain.DMCMDetails _
                                     Where x.SummaryType = EnumSummaryType.INV And x.InvDMCMNo = CStr(Me.txtInvoiceNo.Text) _
                                     Select x).Count()

                            If i <> 0 And j <> 0 Then
                                item = itemSummary
                                Me.txtBalance.Text = "0.00"
                                Exit For
                            End If
                        Next
                    End If
                Next
            End If
        End If

        With item
            Me.txtWESMBillSummaryNo.Text = .WESMBillSummaryNo.ToString()
            Me.txtParentID.Text = .IDNumber.ParticipantID
            Me.txtPDueDate.Text = FormatDateTime(.DueDate, DateFormat.ShortDate)
            Me.txtPBeginningBalance.Text = .BeginningBalance.ToString("#,###.#0")
            Me.txtPNewDueDate.Text = FormatDateTime(.NewDueDate, DateFormat.ShortDate)
            Me.txtPEndingBalance.Text = .EndingBalance.ToString("#,###.#0")
        End With
    End Sub

    Private Sub LoadCollections(ByVal items As List(Of Collection))
        Dim listItems = (From x In items Join y In Me.listOfAMParticipants _
                         On x.IDNumber Equals y.IDNumber _
                         Select x, y.ParticipantID).ToList()

        Me.DGridViewCollection.Rows.Clear()

        For Each item In listItems
            With item
                Me.DGridViewCollection.Rows.Add(.x.CollectionNumber, .x.ORNo, .x.CollectionDate.ToString("MM/dd/yyyy"), _
                                                .ParticipantID, FormatNumber(Math.Abs(.x.CollectedAmount), 2), _
                                                FormatNumber(Math.Abs(.x.CollectedPrudential), 2), FormatNumber(Math.Abs(.x.CollectedHeld), 2), _
                                                FormatNumber(Math.Abs(.x.CollectedAmount + .x.CollectedPrudential + .x.CollectedHeld), 2), _
                                                .x.AllocationType)

            End With
        Next
    End Sub

    Private Sub LoadCollectionAllocations(ByVal items As List(Of CollectionAllocation))
        Me.DGridViewColAllocation.Rows.Clear()
        For Each item In items
            With item
                Me.DGridViewColAllocation.Rows.Add(.WESMBillSummaryNo, .BatchCode, .CollectionType.ToString(), _
                                                   FormatNumber(.Amount, 2), .AllocationDate.ToString("MM/dd/yyyy"))
            End With
        Next
    End Sub

    Public Sub SearchInvoice()
        If Not IsNumeric(Me.txtSearch.Text) Then
            MsgBox("WESM Invoice Number must be numeric!", MsgBoxStyle.Information, "Warning")
            Me.txtSearch.Text = ""
            Me.txtSearch.Select()
            Exit Sub
        End If

        Dim frm As New frmChargeTypeSelection
        Dim valSize As New System.Drawing.Size(300, 225)
        With frm
            .Size = valSize
            .rbMF.Visible = True
            .rbVATMF.Visible = True
            .ViewType = 2
            .InvoiceNumber = CLng(Me.txtSearch.Text.Trim)
            .ShowDialog()
        End With

        Me.txtSearch.Text = "Type WESM Invoice No. here"
        Me.txtSearch.Font = New System.Drawing.Font("Helvetica", 8.5, FontStyle.Italic)
        Me.txtSearch.ForeColor = Color.Gray
        Me.btnClose.Select()
    End Sub

    Private Sub ClearControls()
        Me.txtInvoiceNo.Text = ""
        Me.txtParticipantID.Text = ""
        Me.txtBatchCode.Text = ""
        Me.txtBillingPeriod.Text = ""
        Me.txtStlRun.Text = ""
        Me.txtInvoiceDate.Text = ""
        Me.txtAmount.Text = ""
        Me.txtDueDate.Text = ""
        Me.txtBalance.Text = ""
        Me.txtParentID.Text = ""
        Me.txtPBeginningBalance.Text = ""
        Me.txtPDueDate.Text = ""
        Me.txtPNewDueDate.Text = ""
        Me.txtPEndingBalance.Text = ""
        Me.DGridViewDMCMMain.Rows.Clear()
        Me.DGridViewDMCMDetails.Rows.Clear()
        Me.DGridViewCollection.Rows.Clear()
        Me.DGridViewColAllocation.Rows.Clear()
    End Sub

#End Region

    Private Sub frmUploadedWESMBillTracking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class