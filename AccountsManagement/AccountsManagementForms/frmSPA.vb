Option Explicit On
Option Strict On

Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports System.Threading
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmSPA
    Dim WBillHelper As WESMBillHelper
    Private Sub frmSPA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Try
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName
            Me.LoadGridView()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)            
        End Try       
    End Sub

    Public Sub LoadGridView()
        Me.dgv_SPAMain.DataSource = Nothing
        Me.dgv_SPAMain.DataSource = Me.WBillHelper.GetSPAList()
        Me.GridViewFormat(Me.dgv_SPAMain)
    End Sub

    Private Sub btn_Add_Click(sender As Object, e As EventArgs) Handles btn_Add.Click
        Dim _frmSPAMgt As New frmSPAMgt
        With _frmSPAMgt
            .objfrmSPA = Me
            .ShowDialog()
        End With
    End Sub

    Private Sub GridViewFormat(ByVal dgv As DataGridView)
        With dgv.Columns(0)            
            .Visible = False
        End With

        With dgv.Columns(4).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(5).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        With dgv.Columns(6).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        With dgv.Columns(7).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(8).DefaultCellStyle
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub

    Private Sub btn_Close_Click(sender As Object, e As EventArgs) Handles btn_Close.Click
        Me.Close()
    End Sub

    Private Sub btn_View_Click(sender As Object, e As EventArgs) Handles btn_View.Click
        Try            
            If dgv_SPAMain.RowCount > 0 Then
                If dgv_SPAMain.CurrentRow Is Nothing Then                    
                    dgv_SPAMain.CurrentCell = dgv_SPAMain.Rows(0).Cells(0)
                End If                
                Dim r As Integer = dgv_SPAMain.CurrentRow.Index
                Dim SPANumber As Long = CLng(dgv_SPAMain.Item(0, r).Value.ToString)

                'Get the datasource for the report  
                Dim _frmSPAView As New frmSPAView
                With _frmSPAView
                    .Text = "Special Payment Agreement - View"
                    .SPANumberSelected = SPANumber
                    .isView = True
                    .ShowDialog()
                End With
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)            
        End Try
    End Sub

End Class