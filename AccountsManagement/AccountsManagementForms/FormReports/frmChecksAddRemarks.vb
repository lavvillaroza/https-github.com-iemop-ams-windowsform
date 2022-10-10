'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmChecksAddRemarks
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     January 11, 2013
'Development Group:      Software Development and Support Division
'Description:            GUI for Adding Remarks when Cancelling a Check
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description
'
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects


Public Class frmChecksAddRemarks
    Public _CheckForUpdate As New Check



    Private Sub cmd_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Ok.Click
        _CheckForUpdate.Remarks = Me.txt_Remarks.Text
        frmChecks.isCancelled = False
        frmChecks.ForCancelCheck = _CheckForUpdate
        Me.Close()
    End Sub

    Private Sub cmd_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Cancel.Click
        frmChecks.isCancelled = True
        Me.Close()
    End Sub

    Private Sub frmChecksAddRemarks_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '  Me.MdiParent = MainForm
    End Sub
End Class