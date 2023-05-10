'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmProgress
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 22, 2011
'Development Group:      Software Development and Support Division
'Description:            Graphical Interface to show that the System is Busy
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description
'   October 10, 2011     Juan Carlo L. Panopio         Property is set to Always on Top
Option Explicit On
Option Strict On
Imports System.Windows.Forms
Public Class frmProgress
    Inherits System.Windows.Forms.Form
    Public message As String
    Public timeRun As Boolean = False

    Private Sub frmProgress_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Timer1.Enabled = False
    End Sub

    Public Sub CloseIt()
        Me.Close()
        Me.Dispose()
    End Sub
    Public Overloads Sub ShowDialog(ByVal msg As String)
        CheckForIllegalCrossThreadCalls = False
        Try
            timeRun = True
            Me.Timer1.Enabled = True
            message = msg
            Me.lbl_Message.Text = msg
            Me.ShowDialog()
        Catch
        End Try
    End Sub

    Public Sub stopForm()
        timeRun = False
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            If timeRun = False Then
                Me.Close()
                Me.Timer1.Enabled = False
                Me.Dispose()
            Else
                Me.lbl_Message.Text = message
            End If
        Catch ex As Exception

        End Try
    End Sub


End Class