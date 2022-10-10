Option Explicit On
Option Strict On

Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmBIRATC
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

    Private Sub frmBIRATC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm

        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName
        Me.LoadRecords()

    End Sub

    Private Sub btn_TSearch_Click(sender As Object, e As EventArgs) Handles btn_TSearch.Click
        Dim i = WBillHelper.srchBIRATC("%" & Me.txtSearch.Text & "%")
        Try

            If i.Count <> 0 Then
                Me.GridView.Rows.Clear()
                For Each rec In i
                    Me.GridView.Rows.Add(rec.ATCName, rec.ATCDescription, FormatDateTime(rec.UpdateDate, DateFormat.ShortDate), rec.UpdatedBy)
                Next
            ElseIf i.Count = 0 Then
                MsgBox("No record found!", MsgBoxStyle.Information, "No Record")
                Me.LoadRecords()
                Exit Sub
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibBATCodeWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInSearching.ToString(), AMModule.UserName)
            Exit Sub
        End Try
    End Sub

    Public Sub LoadRecords()
        Try
            Dim BIRATCList = WBillHelper.GetBIRATC()
            Me.GridView.Rows.Clear()
            For Each rec In BIRATCList
                Me.GridView.Rows.Add(rec.ATCName, rec.ATCDescription, rec.ATCRate, FormatDateTime(rec.UpdateDate, DateFormat.ShortDate), rec.UpdatedBy)
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibBATCodeWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInSearching.ToString(), AMModule.UserName)
            Exit Sub
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ans As MsgBoxResult

        Try
            Dim item As New BIRAlphanumericTaxCode
            With item
                .ATCName = GridView.Columns(0).Selected.ToString
                .ATCDescription = GridView.Columns(1).Selected.ToString
            End With

            ans = MsgBox("Do you really want to delete this record?", MsgBoxStyle.YesNo, "Delete")
            WBillHelper.DeleteBIRATC(item)
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

    Private Sub cmd_close_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub btn_New_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_New.Click
        frmBIRATCMgt.State = "ADD"
        frmBIRATCMgt.ShowDialog()
    End Sub

    Private Sub btn_Edit_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Edit.Click
        frmBIRATCMgt.State = "EDIT"
        frmBIRATCMgt.ShowDialog()
    End Sub

    Private Sub btn_refresh_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_refresh.Click
        Me.LoadRecords()
        Me.txtSearch.Text = ""
    End Sub


    Private Sub GridView_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView.CellDoubleClick
        If e.RowIndex = -1 Then
            Exit Sub
        End If
        frmBIRATCMgt.State = "VIEW"
        frmBIRATCMgt.ShowDialog()
    End Sub

End Class