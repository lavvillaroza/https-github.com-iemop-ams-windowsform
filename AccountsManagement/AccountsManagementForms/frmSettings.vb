'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmSettings
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     January 12, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for the maintenance of Settings
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   January 12, 2011        Vladimir E. Espiritu                 GUI design and basic functionalities   
'   December 02, 2012       Vladimir E. Espiritu                 Overall revision
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports System.Configuration
Imports WESMLib.Auth.Lib

'Imports LDAPLib
'Imports LDAPLogin

Public Class frmSettings

    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory
    Private dicSettings As Dictionary(Of String, AdminSettings)

    Private Sub frmSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MdiParent = MainForm
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName
        BFactory = BusinessFactory.GetInstance()

        'Get the settings
        Me.dicSettings = WBillHelper.GetDicAdminSettings()

        'Load the records in the grid
        Me.LoadRecords()

        'Load the first record
        Me.SelectRecord()

        If Me.DGridView.RowCount = 0 Then
            Me.EnableControls(False, False, True, True)
        Else
            Me.EnableControls(True, False, True, True)
        End If
    End Sub

    Private Sub DGridView_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGridView.CellClick
        If Me.DGridView.RowCount = 0 Then
            Exit Sub
        End If

        Me.SelectRecord()
        Me.EnableControls(True, False, True, True)
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If Me.txtCodeName.Text.Trim.Length = 0 Then
            MsgBox("Nothing to edit!", MsgBoxStyle.Exclamation, "No record")
            Exit Sub
        End If

        Me.EnableControls(False, True, True, False)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.txtDescription.Text.Replace("'", "").Trim.Length = 0 Then
                MsgBox("Please specify the description field!", MsgBoxStyle.Critical, "Specify the inputs")
                Exit Sub
            ElseIf Me.txtValue.Text.Replace("'", "").Trim.Length = 0 Then
                MsgBox("Please specify the value field!", MsgBoxStyle.Critical, "Specify the inputs")
                Exit Sub
            End If

            Dim ans = MsgBox("Do you really want to update this record?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Update")
            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            Dim item As New AdminSettings
            Dim strLogs As String = ""
            With item
                .CodeName = Me.txtCodeName.Text.Replace("'", "").Trim()
                .Description = Me.txtDescription.Text.Replace("'", "").Trim()
                .Value = Me.txtValue.Text.Replace("'", "").Trim()
                strLogs &= "Code Name: " & .CodeName
                strLogs &= ", Description: " & .Description
                strLogs &= ", Value: " & .Value
            End With

            'Save
            WBillHelper.SaveAdminSettings(item)
            MsgBox("Successfully Updated!", MsgBoxStyle.Information, "Updated")

            'Updated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.SystemParamWindow.ToString, strLogs, "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccessfullyEdited.ToString(), AMModule.UserName)

            Me.dicSettings = WBillHelper.GetDicAdminSettings()
            Me.LoadRecords()

            'Load the settings in variable
            BFactory.LoadSettingsValues(Me.dicSettings)

            Me.ClearTextBoxes()
            Me.EnableControls(True, False, True, True)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error found")            
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.ClearTextBoxes()
        Me.LoadRecords()
        Me.EnableControls(False, False, True, True)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#Region "Methods/Functions"
    Private Sub LoadRecords()
        Me.DGridView.Rows.Clear()
        For Each item In Me.dicSettings
            With item.Value
                Me.DGridView.Rows.Add(.CodeName, .Description, .Value, .UpdatedBy, .UpdatedDate)
            End With
        Next
    End Sub

    Private Sub SelectRecord()
        If Me.DGridView.RowCount = 0 Then
            Exit Sub
        End If

        With Me.DGridView.CurrentRow
            Me.txtCodeName.Text = CStr(.Cells("colCodeName").Value)
            Me.txtDescription.Text = CStr(.Cells("colDescription").Value)
            Me.txtValue.Text = CStr(.Cells("colValue").Value)
            Me.txtUpdatedBy.Text = CStr(.Cells("colUpdatedBy").Value)
            Me.txtUpdatedDate.Text = CStr(.Cells("colUpdatedDate").Value)
        End With
    End Sub

    Private Sub EnableControls(ByVal vEdit As Boolean, ByVal vSave As Boolean, ByVal vRefresh As Boolean, _
                               ByVal vGrid As Boolean)
        Me.btnEdit.Enabled = vEdit
        Me.btnSave.Enabled = vSave
        Me.btnRefresh.Enabled = vRefresh
        Me.DGridView.Enabled = vGrid

        If vSave = True Then
            Me.txtDescription.ReadOnly = False
            Me.txtValue.ReadOnly = False
            Me.txtDescription.BackColor = Color.White
            Me.txtValue.BackColor = Color.White
        Else
            Me.txtDescription.ReadOnly = True
            Me.txtValue.ReadOnly = True
            Me.txtDescription.BackColor = Color.FromArgb(255, 255, 192)
            Me.txtValue.BackColor = Color.FromArgb(255, 255, 192)
        End If
    End Sub

    Private Sub ClearTextBoxes()
        Me.txtCodeName.Text = ""
        Me.txtDescription.Text = ""
        Me.txtValue.Text = ""
        Me.txtUpdatedBy.Text = ""
        Me.txtUpdatedDate.Text = ""
    End Sub
#End Region


End Class