'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmCheckNumber
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     March 8, 2013
'Development Group:      Software Development and Support Division
'Description:            GUI for maintenance of Check Number
'Arguments/Parameters:  
'Files/Database Tables:  AM_INIT_TABLE
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
'Imports LDAPLogin
'Imports LDAPLib

Public Class frmCheckNumber
    Private WBillHelper As WESMBillHelper
    Private _currentActive As New InitializationTable
    Private Sub frmCheckNumber_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        _currentActive = Me.WBillHelper.GetInitializationTable("CHECKS")
        With _currentActive
            Me.txt_BatchNo.Text = .BatchNo
            Me.txt_InitCheckNo.Text = FormatNumber(.InitialNo, 0, TriState.True, TriState.True)
            Me.txt_LastCheckNo.Text = FormatNumber(.LastNo, 0, TriState.True, TriState.True)
            If .LastSeqUsed = .InitialNo - 1 Then
                Me.txt_LastSeqUsed.Text = "0"
            Else
                Me.txt_LastSeqUsed.Text = FormatNumber(.LastSeqUsed, 0, TriState.True, TriState.True)
            End If
            Me.txt_RemainingAvailable.Text = FormatNumber(.LastNo - .LastSeqUsed, 0, TriState.True, TriState.True)
        End With

        'Fill Datagrid of historical data
        Dim dt As New DataTable
        Me.dgv_History.DataSource = Me.HistoricalChecks(dt)
    End Sub

    Private Function HistoricalChecks(ByVal dt As DataTable) As DataTable
        Dim _lstInitialization = Me.WBillHelper.GetInitializationTableList()
        _lstInitialization = (From x In _lstInitialization _
                              Where x.DocumentName = "CHECKS" _
                              And x.BatchNo <> _currentActive.BatchNo _
                              Select x).ToList

        With dt.Columns
            .Add("Batch Number")
            .Add("Initial No")
            .Add("Last No")
            .Add("Last Used Sequence")
            .Add("Remaining Available")
        End With

        For Each _itmInit In _lstInitialization
            Dim dr As DataRow
            dr = dt.NewRow
            With _itmInit
                dr("Batch Number") = .BatchNo
                dr("Initial No") = .InitialNo
                dr("Last No") = .LastNo
                dr("Last Used Sequence") = .LastSeqUsed
                dr("Remaining Available") = .LastSeqUsed - .LastSeqUsed
            End With
            dt.Rows.Add(dr)
            dt.AcceptChanges()
        Next

        Return dt
    End Function

    Private Sub cmd_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Add.Click

        Try
            If Me.cmd_Add.Text = "Add" Then
                If _currentActive.LastSeqUsed = _currentActive.LastNo Then
                    Me.txt_BatchNo.Text = ""
                    Me.txt_InitCheckNo.Text = ""
                    Me.txt_LastCheckNo.Text = ""
                    Me.txt_LastSeqUsed.Text = ""
                    Me.txt_RemainingAvailable.Text = ""

                    Me.txt_InitCheckNo.BackColor = Color.White
                    Me.txt_LastCheckNo.BackColor = Color.White

                    Me.txt_InitCheckNo.ReadOnly = False
                    Me.txt_LastCheckNo.ReadOnly = False

                    Me.txt_InitCheckNo.Focus()
                    Me.cmd_Add.Text = "Save"
                Else
                    MsgBox("There are still remaining checks, Cannot add new batch", MsgBoxStyle.Critical, "Error")
                End If
            ElseIf Me.cmd_Add.Text = "Save" Then
                Dim ans As New MsgBoxResult

                Me.txt_InitCheckNo.Text = Trim(Me.txt_InitCheckNo.Text)
                Me.txt_LastCheckNo.Text = Trim(Me.txt_LastCheckNo.Text)


                If IsNumeric(Me.txt_InitCheckNo.Text) = False Or IsNumeric(Me.txt_LastCheckNo.Text) = False Then
                    MsgBox("Invalid input for Initial or Last Check Number", MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End If

                If CInt(Me.txt_InitCheckNo.Text) > CInt(Me.txt_LastCheckNo.Text) Then
                    MsgBox("Invalid input for Initial and Last Check Number", MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End If

                ans = MsgBox("Do you really want to save the new record?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "Add new Record")

                If ans = MsgBoxResult.No Then
                    With _currentActive
                        Me.txt_BatchNo.Text = .BatchNo
                        Me.txt_InitCheckNo.Text = FormatNumber(.InitialNo, 0, TriState.True, TriState.True)
                        Me.txt_LastCheckNo.Text = FormatNumber(.LastNo, 0, TriState.True, TriState.True)
                        Me.txt_LastSeqUsed.Text = FormatNumber(.LastSeqUsed, 0, TriState.True, TriState.True)
                        Me.txt_RemainingAvailable.Text = FormatNumber(.LastNo - .LastSeqUsed, 0, TriState.True, TriState.True)

                        Me.txt_InitCheckNo.BackColor = Me.txt_BatchNo.BackColor
                        Me.txt_LastCheckNo.BackColor = Me.txt_BatchNo.BackColor

                        Me.txt_InitCheckNo.ReadOnly = True
                        Me.txt_LastCheckNo.ReadOnly = True
                    End With
                    Me.cmd_Add.Text = "Add"

                    Exit Sub
                End If

                Dim newRecord As New InitializationTable
                Dim strLogs As String
                With newRecord
                    .DocumentName = "CHECKS"
                    .InitialNo = CLng(Me.txt_InitCheckNo.Text)
                    .LastNo = CLng(Me.txt_LastCheckNo.Text)

                    strLogs = "Initialized Checks "
                    strLogs &= "with initial no. of " & .InitialNo & ", "
                    strLogs &= "and last no. of " & .LastNo
                End With

                WBillHelper.SaveInitializationTable(newRecord)
                MsgBox("Successfully saved to database", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Information, MsgBoxStyle), "Success")
                Me.cmd_Add.Enabled = True

                'LDAPModule.LDAP.InsertLog(Me.Name, EnumAMSModules.AMS_CheckNoInitialization.ToString(), strLogs, "", "", EnumColorCode.Blue.ToString(), EnumLogType.SuccesffullySaved.ToString(), 'LDAPModule.LDAP.Username)

                _currentActive = Me.WBillHelper.GetInitializationTable("CHECKS")
                With _currentActive
                    Me.txt_BatchNo.Text = .BatchNo
                    Me.txt_InitCheckNo.Text = FormatNumber(.InitialNo, 0, TriState.True, TriState.True)
                    Me.txt_LastCheckNo.Text = FormatNumber(.LastNo, 0, TriState.True, TriState.True)
                    Me.txt_LastSeqUsed.Text = FormatNumber(.LastSeqUsed, 0, TriState.True, TriState.True)
                    Me.txt_RemainingAvailable.Text = FormatNumber(.LastNo - .LastSeqUsed, 0, TriState.True, TriState.True)

                    Me.txt_InitCheckNo.BackColor = Me.txt_BatchNo.BackColor
                    Me.txt_LastCheckNo.BackColor = Me.txt_BatchNo.BackColor

                    Me.txt_InitCheckNo.ReadOnly = True
                    Me.txt_LastCheckNo.ReadOnly = True
                End With

                Me.cmd_Add.Text = "Add"
                Dim dt As New DataTable
                Me.dgv_History.DataSource = Me.HistoricalChecks(dt)
            End If

        Catch ex As Exception
            'LDAPModule.LDAP.InsertLog(Me.Name, EnumAMSModules.AMS_CheckNoInitialization.ToString(), ex.Message, "", "", EnumColorCode.Red.ToString(), EnumLogType.ErrorInSaving.ToString(), 'LDAPModule.LDAP.Username)

        End Try
    End Sub

    Private Sub cmd_Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Refresh.Click
        _currentActive = Me.WBillHelper.GetInitializationTable("CHECKS")
        With _currentActive
            Me.txt_BatchNo.Text = .BatchNo
            Me.txt_InitCheckNo.Text = FormatNumber(.InitialNo, 0, TriState.True, TriState.True)
            Me.txt_LastCheckNo.Text = FormatNumber(.LastNo, 0, TriState.True, TriState.True)
            Me.txt_LastSeqUsed.Text = FormatNumber(.LastSeqUsed, 0, TriState.True, TriState.True)
            Me.txt_RemainingAvailable.Text = FormatNumber(.LastNo - .LastSeqUsed, 0, TriState.True, TriState.True)

            Me.txt_InitCheckNo.BackColor = Me.txt_BatchNo.BackColor
            Me.txt_LastCheckNo.BackColor = Me.txt_BatchNo.BackColor

            Me.txt_InitCheckNo.ReadOnly = True
            Me.txt_LastCheckNo.ReadOnly = True
            Me.cmd_Add.Text = "Add"
        End With

        Dim dt As New DataTable
        Me.dgv_History.DataSource = Me.HistoricalChecks(dt)
    End Sub

    Private Sub txt_InitCheckNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_InitCheckNo.KeyPress
        If IsNumeric(e.KeyChar) Then
            e.Handled = False
        End If
    End Sub

    Private Sub txt_LastCheckNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_LastCheckNo.KeyPress
        If IsNumeric(e.KeyChar) Then
            e.Handled = False
        End If
    End Sub

    Private Sub cmd_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Close.Click
        Me.Close()
    End Sub
End Class