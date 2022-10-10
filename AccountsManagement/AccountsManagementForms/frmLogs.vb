Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmLogs
    Private WBillHelper As WESMBillHelper

    Private Sub frmLogs_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm

        Try
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = _Login.AMSConnectionString
            WBillHelper.UserName = AMModule.UserName

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim listWESMLogs As New List(Of WESMLogsTransaction)
        If Me.dtFrom.Value > Me.dtTo.Value Then
            MsgBox("invalid date range!", MsgBoxStyle.Exclamation, "Invalid")
            Exit Sub
        End If

        Me.DGridView.Rows.Clear()

        listWESMLogs = WBillHelper.GetWESMLogsTransaction(CDate(FormatDateTime(Me.dtFrom.Value, DateFormat.ShortDate)), CDate(FormatDateTime(Me.dtTo.Value.AddDays(1), DateFormat.ShortDate)), _Login.ApplicationName)

        If listWESMLogs.Count = 0 Then
            MsgBox("No record found!", MsgBoxStyle.Information, "No data")
            Exit Sub
        End If
        Dim rowstart As Integer = 0
        For Each item In listWESMLogs
            With item
                Me.DGridView.Rows.Add(.TransactionDate, .ModuleName, .ComputerName, .IPAddress, .Remarks, .LogType, .UpdatedBy)
                If .LogType.ToString.Contains("ErrorIn") = True Then
                    Me.DGridView.Rows(rowstart).DefaultCellStyle.ForeColor = Color.Red                
                End If
                rowstart += 1
            End With
        Next
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class