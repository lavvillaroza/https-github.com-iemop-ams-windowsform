Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmWBSExemption
    Private wbExemptionHlper As WESMBillsExemptionHelper
    Private wBQuery As List(Of WESMBillsExemption)
    Private wBWithFilter As List(Of WESMBillsExemption)
    Private currStartPage As Integer
    Private currEndPage As Integer
    Private totalRecords As Integer

    Private Sub frmWESMBillExemption_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Try            
            wbExemptionHlper = New WESMBillsExemptionHelper
            Me.txtBoxSearchValue.SendToBack()

            AddHandler cb_ColumnName.SelectedIndexChanged, AddressOf cb_ColumnName_SelectedIndexChanged
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadDataComboBox()
        Dim colname As New List(Of String)
        colname.Add("")
        colname.Add("Batch No")
        colname.Add("Billing No")
        colname.Add("Invoice No")
        colname.Add("ID Number")
        colname.Add("Due Date")
        colname.Add("Charge Type")
        colname.Add("Ending Balance")
        cb_ColumnName.DataSource = colname
        cb_ColumnName.SelectedIndex = 0
    End Sub
    Private Sub LoadDataGridView(ByVal wbExemptionListData As List(Of WESMBillsExemption))

        dataGridView.Rows.Clear()

        For Each item In wbExemptionListData
            With dataGridView
                Dim row As String() = New String() {item.WESMBillSummaryNo.ToString, item.WESMBillBatchNo.ToString, item.BillPeriod.ToString, item.InvoiceNo, item.IDNumber,
                                                    item.OrigDueDate.ToShortDateString, item.ChargeType.ToString,
                                                     FormatNumber(item.EndingBalance, UseParensForNegativeNumbers:=TriState.True), item.NoOffset.ToString, item.NoSOA.ToString, item.NoDefaultInterest.ToString}
                .Rows.Add(row)
            End With
        Next
    End Sub


    Private Sub cb_ColumnName_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            ProgressThread.Show("Please wait while preparing data.")
            If cb_ColumnName.SelectedIndex > 0 And cb_ColumnName.Text <> "" Then
                Select Case cb_ColumnName.Text
                    Case "Batch No"
                        txtBoxSearchValue.SendToBack()
                        txtBoxSearchValue.Text = ""
                        cb_SearchValue.Items.Clear()
                        cb_SearchValue.Items.Add("")

                        Dim listOfData As List(Of Integer) = wbExemptionHlper.WESMBillsExemptionList.Select(Function(x) x.WESMBillBatchNo).Distinct.OrderBy(Function(y) y).ToList

                        For Each item In listOfData
                            cb_SearchValue.Items.Add(item)
                        Next
                        cb_SearchValue.SelectedIndex = 0
                    Case "Billing No"
                        txtBoxSearchValue.SendToBack()
                        txtBoxSearchValue.Text = ""
                        cb_SearchValue.Items.Clear()
                        cb_SearchValue.Items.Add("")

                        Dim listOfData As List(Of Integer) = wbExemptionHlper.WESMBillsExemptionList.Select(Function(x) x.BillPeriod).Distinct.OrderBy(Function(y) y).ToList

                        For Each item In listOfData
                            cb_SearchValue.Items.Add(item)
                        Next
                        cb_SearchValue.SelectedIndex = 0
                    Case "Invoice No"
                        cb_SearchValue.Items.Clear()
                        txtBoxSearchValue.BringToFront()                        
                    Case "ID Number"
                        txtBoxSearchValue.SendToBack()
                        txtBoxSearchValue.Text = ""
                        cb_SearchValue.Items.Clear()
                        cb_SearchValue.Items.Add("")

                        Dim listOfData As List(Of String) = wbExemptionHlper.WESMBillsExemptionList.Select(Function(x) x.IDNumber).Distinct.OrderBy(Function(y) y).ToList

                        For Each item In listOfData
                            cb_SearchValue.Items.Add(item)
                        Next
                        cb_SearchValue.SelectedIndex = 0
                    Case "Due Date"
                        txtBoxSearchValue.SendToBack()
                        txtBoxSearchValue.Text = ""
                        cb_SearchValue.Items.Clear()
                        cb_SearchValue.Items.Add("")

                        Dim listOfData As List(Of Date) = wbExemptionHlper.WESMBillsExemptionList.Select(Function(x) x.OrigDueDate).Distinct.OrderBy(Function(y) y).ToList

                        For Each item In listOfData
                            cb_SearchValue.Items.Add(item.ToShortDateString)
                        Next
                        cb_SearchValue.SelectedIndex = 0
                    Case "Charge Type"
                        txtBoxSearchValue.SendToBack()
                        txtBoxSearchValue.Text = ""
                        cb_SearchValue.Items.Clear()
                        cb_SearchValue.Items.Add("")
                        cb_SearchValue.Items.Add("E")
                        cb_SearchValue.Items.Add("EV")
                        cb_SearchValue.SelectedIndex = 0
                End Select
                cb_SearchValue.SelectedIndex = -1                
            End If
            ProgressThread.Close()
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        
    End Sub

    Private Sub dgv_WBSummaryList_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dataGridView.CurrentCellDirtyStateChanged
        Try
            If Not Me.dataGridView.IsCurrentCellDirty Then
                Exit Sub
            End If
            Select Case Me.dataGridView.CurrentCell.ColumnIndex
                Case 8
                    Me.dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
                    With Me.dataGridView.CurrentRow
                        Dim selectedItem As Long = CLng(.Cells(0).Value)
                        Dim updatedValue As Boolean = CBool(.Cells(8).Value)
                        wbExemptionHlper.UpdateWESMBillExemptionForNoOffset(selectedItem, updatedValue)
                    End With
                Case 9
                    Me.dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
                    With Me.dataGridView.CurrentRow
                        Dim selectedItem As Long = CLng(.Cells(0).Value)
                        Dim updatedValue As Boolean = CBool(.Cells(9).Value)
                        wbExemptionHlper.UpdateWESMBillExemptionForNoSOA(selectedItem, updatedValue)
                    End With
                Case 10
                    Me.dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
                    With Me.dataGridView.CurrentRow
                        Dim selectedItem As Long = CLng(.Cells(0).Value)
                        Dim updatedValue As Boolean = CBool(.Cells(10).Value)
                        wbExemptionHlper.UpdateWESMBillExemptionForNoDefInt(selectedItem, updatedValue)
                    End With
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If txtBoxSearchValue.Text.Trim.Length <> 0 Then
                wBWithFilter = wbExemptionHlper.WESMBillsExemptionList.Where(Function(x) x.InvoiceNo.Contains(CStr(txtBoxSearchValue.Text))).OrderBy(Function(x) x.WESMBillBatchNo).ToList
                totalRecords = wBWithFilter.Count
                currStartPage = 0
                currEndPage = 0

                If totalRecords <= 1000 Then
                    Me.Pagination(currStartPage, CInt(totalRecords))
                Else
                    Me.Pagination(currStartPage, 1000)
                End If
                LoadDataGridView(wBQuery)
            ElseIf txtBoxSearchValue.Text.Trim.Length = 0 And cb_SearchValue.Text.Trim.Length = 0 Then
                MessageBox.Show("Please provide inputs to search!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                ProgressThread.Show("Please wait while preparing data.")
                If cb_SearchValue.SelectedIndex > 0 And cb_SearchValue.Text <> "" Then
                    Select Case cb_ColumnName.Text
                        Case "Batch No"
                            wBWithFilter = wbExemptionHlper.WESMBillsExemptionList.Where(Function(x) x.WESMBillBatchNo = CInt(cb_SearchValue.Text)).OrderBy(Function(x) x.WESMBillBatchNo).ToList
                        Case "Billing No"
                            wBWithFilter = wbExemptionHlper.WESMBillsExemptionList.Where(Function(x) x.BillPeriod = CInt(cb_SearchValue.Text)).OrderBy(Function(x) x.WESMBillBatchNo).ToList
                        Case "ID Number"
                            wBWithFilter = wbExemptionHlper.WESMBillsExemptionList.Where(Function(x) x.IDNumber = CStr(cb_SearchValue.Text)).OrderBy(Function(x) x.WESMBillBatchNo).ToList
                        Case "Due Date"
                            wBWithFilter = wbExemptionHlper.WESMBillsExemptionList.Where(Function(x) x.OrigDueDate = CDate(cb_SearchValue.Text)).OrderBy(Function(x) x.WESMBillBatchNo).ToList
                        Case "Charge Type"
                            Dim chargeType As New EnumChargeType
                            If CStr(cb_SearchValue.Text) = "E" Then
                                chargeType = EnumChargeType.E
                            ElseIf CStr(cb_SearchValue.Text) = "EV" Then
                                chargeType = EnumChargeType.EV
                            End If
                            wBWithFilter = wbExemptionHlper.WESMBillsExemptionList.Where(Function(x) x.ChargeType = chargeType).ToList
                    End Select

                    totalRecords = wBWithFilter.Count
                    currStartPage = 0
                    currEndPage = 0

                    If totalRecords <= 1000 Then
                        Me.Pagination(currStartPage, CInt(totalRecords))
                    Else
                        Me.Pagination(currStartPage, 1000)
                    End If
                    LoadDataGridView(wBQuery)
                End If
                ProgressThread.Close()
            End If
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try        
    End Sub

    Private Sub btnViewChanges_Click(sender As Object, e As EventArgs) Handles btnViewChanges.Click
        Try
            Dim getUpdatedList As List(Of WESMBillsExemption) = wbExemptionHlper.WESMBillsExemptionListWithUpdate
            If getUpdatedList.Count = 0 Then
                MessageBox.Show("No changes have been made!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                LoadDataGridView(getUpdatedList)

                wBWithFilter = getUpdatedList
                totalRecords = getUpdatedList.Count
                currStartPage = 0
                currEndPage = 0
                Me.Pagination(0, 1000)
                LoadDataGridView(wBQuery)
                cb_SearchValue.Items.Clear()
                cb_SearchValue.Text = ""

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ProgressThread.Close()
        End Try
    End Sub

    Private Sub btnResetChanges_Click(sender As Object, e As EventArgs) Handles btnResetChanges.Click
        Try
            If wbExemptionHlper.WESMBillsExemptionListWithUpdate.Count = 0 Then
                MessageBox.Show("No available data to reset with!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            Dim msgAns As New MsgBoxResult
            msgAns = MsgBox("Do you really want to remove the changes you made?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "System Message")

            If msgAns = MsgBoxResult.Yes Then
                ProgressThread.Show("Please wait while removing changes.")
                wbExemptionHlper.WESMBillsExemptionListWithUpdate = New List(Of WESMBillsExemption)
                wbExemptionHlper.GetWESMBillsExemptionList()
                wBWithFilter = wbExemptionHlper.WESMBillsExemptionList
                totalRecords = wbExemptionHlper.WESMBillsExemptionList.Count
                currStartPage = 0
                currEndPage = 0
                Me.Pagination(0, 1000)
                LoadDataGridView(wBQuery)
                Me.cb_SearchValue.Items.Clear()
                Me.cb_SearchValue.Text = ""
                Me.cb_ColumnName.Text = ""
            End If
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ProgressThread.Close()
        End Try
    End Sub


    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Try
            ProgressThread.Show("Please wait while fetching.")
            wbExemptionHlper.GetWESMBillsExemptionList()
            wBWithFilter = wbExemptionHlper.WESMBillsExemptionList
            totalRecords = wbExemptionHlper.WESMBillsExemptionList.Count
            currStartPage = 0
            currEndPage = 0
            Me.Pagination(0, 1000)
            LoadDataGridView(wBQuery)
            LoadDataComboBox()
            cb_SearchValue.Items.Clear()
            cb_SearchValue.Text = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ProgressThread.Close()
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim msgAns As New MsgBoxResult
            msgAns = MsgBox("Do you really want to save the changes you made?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "System Message")

            If msgAns = MsgBoxResult.Yes Then
                If wbExemptionHlper.WESMBillsExemptionListWithUpdate.Count <> 0 Then
                    ProgressThread.Show("Please wait while Saving.")
                    wbExemptionHlper.SaveChanges()
                    wbExemptionHlper.WESMBillsExemptionListWithUpdate = New List(Of WESMBillsExemption)
                    ProgressThread.Close()
                    MessageBox.Show("Successfully save!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.dataGridView.Rows.Clear()
                    totalRecords = 0
                    currStartPage = 0
                    currEndPage = 0
                    Me.cb_SearchValue.Items.Clear()
                    Me.cb_SearchValue.Text = ""
                    Me.cb_ColumnName.Text = ""
                Else
                    MessageBox.Show("No available data for update!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ProgressThread.Close()
        End Try
    End Sub

    
    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click        
        Me.Pagination(-1000, -1000)
        LoadDataGridView(wBQuery)
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click        
        Me.Pagination(1000, 1000)
        LoadDataGridView(wBQuery)
    End Sub


    Private Sub Pagination(ByVal movefrom As Integer, ByVal moveto As Integer)
        If currStartPage = 0 Then
            Me.btnPrevious.Enabled = False
        Else
            Me.btnPrevious.Enabled = True
        End If

        If (currEndPage + moveto) >= totalRecords Then
            Me.btnNext.Enabled = False
        Else
            Me.btnNext.Enabled = True
        End If

        If movefrom < 0 And moveto < 0 Then
            currStartPage += movefrom
            If currEndPage = totalRecords Then
                currEndPage -= (currEndPage - (currStartPage + 999))
            Else
                currEndPage += moveto
            End If
            If currStartPage = 1 Then
                Me.btnPrevious.Enabled = False
            Else
                Me.btnPrevious.Enabled = True
            End If
        Else
            If movefrom = 0 And totalRecords > 100 Then
                currStartPage += movefrom + 1
                currEndPage += moveto
            ElseIf movefrom = 0 And totalRecords < 100 Then
                currStartPage += movefrom + 1
                currEndPage += totalRecords
            Else
                currStartPage += movefrom
                If currEndPage = totalRecords And moveto < 0 Then
                    currEndPage += (currEndPage - (currStartPage - 1))
                Else
                    currEndPage += CInt(IIf((currEndPage + moveto) <= totalRecords, moveto, (totalRecords - currEndPage)))
                End If
            End If
        End If

        wBQuery = wBWithFilter.Skip(currStartPage - 1).Take(currEndPage).ToList
        Me.lblPagination.Text = currStartPage.ToString("N0") & " to " & currEndPage.ToString("N0") & " out of " & totalRecords.ToString("N0")
    End Sub

    
End Class