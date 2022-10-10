'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmBillParticipants
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     January 18, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for the maintenance of Bill Participants
'Arguments/Parameters:  
'Files/Database Tables:  BILL_PARTICIPANTS
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   January 18, 2012        Vladimir E. Espiritu                 GUI design and basic functionalities    
'   January 25, 2011        Vladimir E. Espiritu                 Added viewing OnDoubleClicked on grid
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmBillParticipants

    Private WBillHelper As WESMBillHelper
    Private listOfParticipants As New List(Of AMParticipants)    

    Private Sub frmBillParticipants_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.MdiParent = MainForm
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName

            listOfParticipants = WBillHelper.GetAMParticipantsAll()
            Me.DisplayOnGrid(listOfParticipants)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DGridView_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGridView.CellFormatting
        If Me.DGridView.Columns(e.ColumnIndex).Name = "colStatus" Then
            If e.Value.ToString() = EnumStatus.InActive.ToString() Then
                Me.DGridView.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Red
            Else
                Me.DGridView.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Black
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim participantid = Me.txtSearch.Text.Trim.ToUpper()

        If participantid.Length = 0 Then
            MsgBox("Specify first the participant id!", MsgBoxStyle.Critical, "Invalid")
            Me.txtSearch.Select()
            Exit Sub
        End If

        For index As Integer = 0 To Me.DGridView.RowCount - 1
            If CStr(Me.DGridView.Rows(index).Cells(1).Value).ToUpper() = participantid Then
                Me.DGridView.Rows(index).Selected = True
                Me.DGridView.CurrentCell = Me.DGridView.Rows(index).Cells(0)
                Me.txtSearch.Text = ""
                Exit Sub
            End If
        Next

        MsgBox("The participant does not exist!", MsgBoxStyle.Information, "No record found")
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim frm As New frmBillParticipantsMgt
        With frm
            .LoadType = frmBillParticipantsMgt.AMParticipantsLoadType.Add
            .ShowDialog()
        End With
        listOfParticipants = WBillHelper.GetAMParticipantsAll()
        Me.DisplayOnGrid(listOfParticipants)
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim item As New AMParticipants
        With Me.DGridView.CurrentRow            
            item = (From x In listOfParticipants Where x.IDNumber = CStr(.Cells("colIDNo").Value) Select x).FirstOrDefault()
        End With

        Dim frm As New frmBillParticipantsMgt
        With frm
            .LoadType = frmBillParticipantsMgt.AMParticipantsLoadType.Edit
            .Participant = item
            .ShowDialog()
        End With
        listOfParticipants = WBillHelper.GetAMParticipantsAll()
        Me.DisplayOnGrid(listOfParticipants)
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        ListRefresh()
    End Sub

    Public Sub ListRefresh()
        Dim Participants = WBillHelper.GetAMParticipantsAll()
        Me.DisplayOnGrid(Participants)
        Me.txtSearch.Text = ""
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim participantid = Me.txtSearch.Text.Trim.ToUpper()

            If participantid.Length = 0 Then
                MsgBox("Specify first the participant id!", MsgBoxStyle.Critical, "Invalid")
                Me.txtSearch.Select()
                Exit Sub
            End If

            For index As Integer = 0 To Me.DGridView.RowCount - 1
                If CStr(Me.DGridView.Rows(index).Cells(1).Value).ToUpper() = participantid Then
                    Me.DGridView.Rows(index).Selected = True
                    Me.DGridView.CurrentCell = Me.DGridView.Rows(index).Cells(0)
                    Me.txtSearch.Text = ""
                    Exit Sub
                End If
            Next

            MsgBox("The participant does not exist!", MsgBoxStyle.Information, "No record found")
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub DGridView_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGridView.CellMouseDoubleClick

        If e.RowIndex = -1 Then
            Exit Sub
        End If

        Dim item As New AMParticipants
         With Me.DGridView.CurrentRow            
            item = (From x In listOfParticipants Where x.IDNumber = CStr(.Cells("colIDNo").Value) Select x).FirstOrDefault()
        End With
        Dim frm As New frmBillParticipantsMgt
        With frm
            .LoadType = frmBillParticipantsMgt.AMParticipantsLoadType.View
            .Participant = item
            .ShowDialog()
        End With
    End Sub

#Region "Methods/Functions"
    Private Sub DisplayOnGrid(ByVal listParticipants As List(Of AMParticipants))
        Try
            Me.DGridView.Rows.Clear()
            For Each item In listParticipants                
                With item
                    Me.DGridView.Rows.Add(.IDNumber, .ParticipantID, .FullName, .Status.ToString)
                End With
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

    Private Sub btn_Import_Click(sender As Object, e As EventArgs) Handles btn_Import.Click
        Dim frm As New frmBillParticiopantsImportFromCRSSDB
        With frm
            .ShowDialog()
        End With
        Me.ListRefresh()

    End Sub
End Class