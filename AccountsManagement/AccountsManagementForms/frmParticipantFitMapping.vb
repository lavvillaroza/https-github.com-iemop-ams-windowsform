Imports System
Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmParticipantFitMapping
    Private WBillHelper As WESMBillHelper
    Private DicBillingPeriods As Dictionary(Of Integer, CalendarBillingPeriod)
    Private DicParticipants As Dictionary(Of String, AMParticipants)
    Private ListOfParentFIT As List(Of AMParticipants)


#Region "Events"
    Private Sub frmParticipantFitMapping_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim listParentChildFIT As List(Of AMParticipantsFIT)

        Me.MdiParent = MainForm
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        'Get the Billing Periods
        Me.DicBillingPeriods = WBillHelper.GetDicCalendarBP()

        'Get the AM Participants
        Me.DicParticipants = WBillHelper.GetDicAMParticipantsByParticipantID()

        'Get the Parent FIT
        Me.ListOfParentFIT = WBillHelper.GetParentFIT()

        'Get the List of Parent and Child FIT
        listParentChildFIT = WBillHelper.GetMappedChildFIT()

        Me.DisplayOnGrid(listParentChildFIT)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim frm As New frmParticipantFitMappingMgt
        Try
            With frm
                .LogType = EnumLogType.Saving
                .DicBillingPeriods = Me.DicBillingPeriods
                .DicParticpants = Me.DicParticipants
                .ListOfParentFIT = Me.ListOfParentFIT
                .ShowDialog()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub DGV_ParticipantsFit_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_ParticipantsFit.CellContentClick
        Dim frm As New frmParticipantFitMappingMgt
        Dim ItemParentAndChildFITMapping As New AMParticipantsFIT
        Dim FITID As Long

        Try

            FITID = CLng(Me.DGV_ParticipantsFit.Rows(e.RowIndex).Cells("colFITID").Value)

                Select Case e.ColumnIndex
                Case 6
                    With frm
                        .DicBillingPeriods = Me.DicBillingPeriods
                        .DicParticpants = Me.DicParticipants
                        .ListOfParentFIT = Me.ListOfParentFIT
                        .ItemParentAndChildFITMapping = WBillHelper.GetMappedChildFIT(FITID).First()
                        .LogType = EnumLogType.Viewing
                        .ShowDialog()
                    End With

                Case 7
                    With frm
                        .DicBillingPeriods = Me.DicBillingPeriods
                        .DicParticpants = Me.DicParticipants
                        .ListOfParentFIT = Me.ListOfParentFIT
                        .ItemParentAndChildFITMapping = WBillHelper.GetMappedChildFIT(FITID).First()
                        .LogType = EnumLogType.Editing
                        .ShowDialog()
                    End With

                Case 8
                    Dim ans As MsgBoxResult

                    ItemParentAndChildFITMapping = WBillHelper.GetMappedChildFIT(FITID).First()

                    ans = MsgBox("Do you really want to delete " & ItemParentAndChildFITMapping.ParentIDNumber.ParticipantID & "?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Delete")

                    If ans = MsgBoxResult.Yes Then
                        WBillHelper.DeleteFITMapping(ItemParentAndChildFITMapping.FitID)
                        MessageBox.Show("Delete", "Successfully Deleted!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.DGV_ParticipantsFit.Rows.RemoveAt(e.RowIndex)
                    End If
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DGV_ParticipantsFit_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGV_ParticipantsFit.CellFormatting
        Select Case e.ColumnIndex
            Case 6
                Me.DGV_ParticipantsFit.Rows(e.RowIndex).Cells(e.ColumnIndex).ToolTipText = "View"
            Case 7
                Me.DGV_ParticipantsFit.Rows(e.RowIndex).Cells(e.ColumnIndex).ToolTipText = "Edit"
            Case 8
                Me.DGV_ParticipantsFit.Rows(e.RowIndex).Cells(e.ColumnIndex).ToolTipText = "Delete"
        End Select
    End Sub

#End Region


#Region "Methods / Functions"
    Private Sub DisplayOnGrid(ByVal listParentChildFIT As List(Of AMParticipantsFIT))
        Dim itemBP As CalendarBillingPeriod
        Dim itemParticipant As AMParticipants

        Me.DGV_ParticipantsFit.Rows.Clear()

        For Each item In listParentChildFIT
            With item
                itemParticipant = Me.DicParticipants(.ParentIDNumber.ParticipantID)
                itemBP = Me.DicBillingPeriods(.BillingPeriod)

                Me.DGV_ParticipantsFit.Rows.Insert(0, .FitID.ToString(), itemBP.BillingPeriod, itemBP.BillingPeriod.ToString() & " (" & itemBP.StartDate.ToString("MM/dd/yyyy") & "-" & itemBP.EndDate.ToString("MM/dd/yyyy") & ")", _
                                                itemParticipant.IDNumber, itemParticipant.ParticipantID, itemParticipant.FullName)
            End With
        Next
    End Sub
#End Region


End Class