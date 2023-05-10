'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmAccountingCode
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     August 26, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI the maintenance of Accounting codes to be used by the system
'Arguments/Parameters:  
'Files/Database Tables:  AM_ACCOUNTING_CODE
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description
'   September 02, 2011      Juan Carlo L. Panopio               Record loading method set to For-Loop
'   September 02, 2011      Juan Carlo L. Panopio               Added temporary Log procedure in Add/Edit/Delete records
'   September 26, 2011      Juan Carlo L. Panopio               Fixed bugs found in Add/Delete

Option Explicit On
Option Strict On

Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib


Public Class frmAccountingCode

    Dim WBillHelper As WESMBillHelper

    Private Enum EnumFormStatus
        Load
        NewRecord
        Edit
        Save
        Cancel
        Refresh
        Delete
        Search
        View
    End Enum

    Private Sub frmAccountingCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm

        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName
        Me.LoadRecords()
        Me.txtFormStatus.Text = EnumFormStatus.Load.ToString()
        Me.ResizeRedraw = True

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.txtFormStatus.Text = EnumFormStatus.NewRecord.ToString()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.txtFormStatus.Text = EnumFormStatus.Edit.ToString()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.txtFormStatus.Text = EnumFormStatus.Cancel.ToString()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ans As MsgBoxResult

        Try
            Dim item As New AccountingCode
            With item
                .AccountCode = GridView.Columns(0).Selected.ToString
                .Description = GridView.Columns(1).Selected.ToString
                .Status = 0
            End With

            ans = MsgBox("Do you really want to delete this record?", MsgBoxStyle.YesNo, "Delete")
            WBillHelper.SaveAccountingCode(item, False)
            MsgBox("Successfully deleted!", MsgBoxStyle.Information, "Deleted")

            Me.txtFormStatus.Text = EnumFormStatus.Delete.ToString()
            Me.LoadRecords()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.LoadRecords()
    End Sub

#Region "Methods/Functions"
    Public Sub LoadRecords()

        Try
            Dim acctcodes = WBillHelper.GetAccountingCodes()
            Me.GridView.Rows.Clear()
            For Each rec In acctcodes
                Me.GridView.Rows.Add(rec.AccountCode, rec.Description, If(rec.Status = 1, "Active", "In-Active"), FormatDateTime(rec.DateCommit, DateFormat.ShortDate), rec.updatedBy)
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

#End Region

    Private Sub btn_TSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_TSearch.Click
        Dim i = WBillHelper.srchAccountingCode("%" & Me.txtSearch.Text & "%")
        Try

            If i.Count <> 0 Then
                Me.GridView.Rows.Clear()
                For Each rec In i
                    Me.GridView.Rows.Add(rec.AccountCode, rec.Description, If(rec.Status = 1, "Active", "In-Active"), FormatDateTime(rec.DateCommit, DateFormat.ShortDate), rec.updatedBy)
                Next
            ElseIf i.Count = 0 Then
                MessageBox.Show("No record found!", "System Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.LoadRecords()
                Exit Sub
            End If
        Catch ex As Exception            
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_close_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub btn_New_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_New.Click
        frmAccountingCodeMgt.State = "ADD"
        frmAccountingCodeMgt.ShowDialog()
    End Sub

    Private Sub btn_Edit_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Edit.Click
        frmAccountingCodeMgt.State = "EDIT"
        frmAccountingCodeMgt.ShowDialog()
    End Sub

    Private Sub btn_Del_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Del.Click
        Dim ans As MsgBoxResult
        Try


            ans = MsgBox("Do you want to set the record to In-Active?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "Delete")
            If ans = MsgBoxResult.Yes Then
                Dim item As New AccountingCode

                With item
                    .AccountCode = GridView.Rows(GridView.CurrentRow.Index).Cells(0).Value.ToString
                    .Description = (GridView.Rows(GridView.CurrentRow.Index).Cells(1).Value.ToString)
                    .Status = If((UCase(GridView.Rows(GridView.CurrentRow.Index).Cells(2).Value.ToString)) = "ACTIVE", 1, 0)
                End With

                WBillHelper.delAC(item)

                Dim strLogs As String = ""
                strLogs = "Delete Record"
                With item
                    strLogs &= "Account Code: " & .AccountCode
                    strLogs &= ", Description: " & .Description
                End With
                'Updated By Lance 8/24/2014
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibAccountCodeWindow.ToString, strLogs, "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccssfullyDeleted.ToString(), AMModule.UserName)
                MsgBox("Record Successfully set to In-Active!", MsgBoxStyle.Information, "Delete")
            Else
                Exit Sub
            End If
            Me.LoadRecords()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")            
            Exit Sub
        End Try
    End Sub

    Private Sub btn_refresh_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_refresh.Click
        Me.LoadRecords()
        Me.txtSearch.Text = ""
    End Sub


    Private Sub GridView_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView.CellDoubleClick
        If e.RowIndex = -1 Then
            Exit Sub
        End If
        frmAccountingCodeMgt.State = "VIEW"
        frmAccountingCodeMgt.ShowDialog()
    End Sub

End Class