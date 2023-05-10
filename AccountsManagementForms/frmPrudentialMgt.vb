'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmCollection
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     April 02, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for the maintenance of Collection
'Arguments/Parameters:  
'Files/Database Tables:  AM_COLLECTION
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   April 02, 2012          Vladimir E. Espiritu                 GUI design and basic functionalities                    
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmPrudentialMgt
    Private BFactory As BusinessFactory
    Private WBillHelper As WESMBillHelper

    Public Enum EnumTransType
        Replenishment
        Interest
        TranferInterestIntoPR
    End Enum

    Private _ListParticipants As List(Of AMParticipants)
    Public Property ListParticipants() As List(Of AMParticipants)
        Get
            Return _ListParticipants
        End Get
        Set(ByVal value As List(Of AMParticipants))
            _ListParticipants = value
        End Set
    End Property

    Private _TransType As EnumTransType
    Public Property TransType() As EnumTransType
        Get
            Return _TransType
        End Get
        Set(ByVal value As EnumTransType)
            _TransType = value
        End Set
    End Property

    Private _ListPrudential As List(Of Prudential)
    Public Property ListPrudential() As List(Of Prudential)
        Get
            Return _ListPrudential
        End Get
        Set(ByVal value As List(Of Prudential))
            _ListPrudential = value
        End Set
    End Property

    Private Sub frmPrudentialMgt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        Me.BFactory = BusinessFactory.GetInstance()

        Select Case Me.TransType
            Case EnumTransType.Replenishment
                Me.DGridMain.Columns("colCheck").Visible = False
                Me.chckAll.Visible = False

                For Each item In Me.ListParticipants
                    Me.DGridMain.Rows.Add(item.IDNumber, item.ParticipantID, "0.00")
                Next

            Case EnumTransType.Interest
                Me.DGridMain.Columns("colCheck").Visible = False
                Me.chckAll.Visible = False

                For Each item In Me.ListParticipants
                    Me.DGridMain.Rows.Add(item.IDNumber, item.ParticipantID, "0.00")
                Next

            Case EnumTransType.TranferInterestIntoPR
                Me.chckAll.CheckState = CheckState.Checked
                Me.chckAll.Visible = True
                Me.DGridMain.Columns("colCheck").Visible = True
                Me.DGridMain.Columns("colAmount").ReadOnly = True

                'Get the Prudentail with interest
                Dim listPRWithInterest = From x In Me.ListPrudential Join y In Me.ListParticipants _
                                         On x.IDNumber Equals y.IDNumber _
                                         Where x.InterestAmount <> 0 _
                                         Select x, y.ParticipantID

                For Each item In listPRWithInterest
                    Me.DGridMain.Rows.Add(item.x.IDNumber, item.ParticipantID, FormatNumber(item.x.InterestAmount, 2), _
                                          CheckState.Checked)
                Next
        End Select

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not Me.FormValidation() Then
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.No
        Me.Close()
    End Sub

    Private Sub txtReplenishmentAmount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtReplenishmentAmount.LostFocus
        If Not IsNumeric(Me.txtReplenishmentAmount.Text) Then
            Exit Sub
        End If

        Me.txtReplenishmentAmount.Text = FormatNumber(Me.txtReplenishmentAmount.Text, 2)
    End Sub

    Private Sub txtInterestAmount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInterestAmount.LostFocus
        If Not IsNumeric(Me.txtInterestAmount.Text) Then
            Exit Sub
        End If

        Me.txtInterestAmount.Text = FormatNumber(Me.txtInterestAmount.Text, 2)
    End Sub

    Private Sub DGridMain_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGridMain.CellEndEdit
        Dim Amount As String = CStr(Me.DGridMain.CurrentRow.Cells("colAmount").Value)

        If Not IsNumeric(Amount) Then
            MsgBox("Entered value must be numeric!", MsgBoxStyle.Exclamation, "Specify the inputs")
            Me.DGridMain.CurrentRow.Cells("colAmount").Value = "0.00"
            Me.DGridMain.Rows(e.RowIndex).Selected = True
            Exit Sub
        ElseIf CDec(Amount) < 0 Then
            MsgBox("Entered value must not be negative!", MsgBoxStyle.Exclamation, "Specify the inputs")
            Me.DGridMain.CurrentRow.Cells("colAmount").Value = "0.00"
            Me.DGridMain.Rows(e.RowIndex).Selected = True
            Exit Sub
        ElseIf Not Me.BFactory.CheckPrecisionAndScale(10, 2, Amount.ToString()) Then
            MsgBox("Amount should have a maximum of 10 whole number and 2 decimal places only!", MsgBoxStyle.Critical, "Specify the inputs")
            Me.DGridMain.CurrentRow.Cells("colAmount").Value = "0.00"
            Me.DGridMain.Rows(e.RowIndex).Selected = True
            Exit Sub
        End If

        Me.DGridMain.CurrentRow.Cells("colAmount").Value = FormatNumber(Amount, 2)
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        For index As Integer = 0 To Me.DGridMain.RowCount - 1
            Me.DGridMain.Rows(index).Cells("colAmount").Value = "0.00"
        Next
    End Sub

    Private Sub chckAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chckAll.CheckedChanged
        For index As Integer = 0 To Me.DGridMain.RowCount - 1
            If Me.chckAll.CheckState = CheckState.Checked Then
                Me.DGridMain.Rows(index).Cells("colCheck").Value = True
            Else
                Me.DGridMain.Rows(index).Cells("colCheck").Value = False
            End If
        Next
    End Sub

#Region "Methods/Functions"
    Private Function FormValidation() As Boolean
        FormValidation = False

        For index As Integer = 0 To Me.DGridMain.RowCount - 1
            Select Case Me.TransType
                Case EnumTransType.Replenishment
                    Dim amount As Decimal = CDec(Me.DGridMain.Rows(index).Cells("colAmount").Value)
                    If amount <> 0 Then
                        FormValidation = True
                        Exit For
                    End If

                Case EnumTransType.Interest
                    Dim amount As Decimal = CDec(Me.DGridMain.Rows(index).Cells("colAmount").Value)
                    If amount <> 0 Then
                        FormValidation = True
                        Exit For
                    End If

                Case EnumTransType.TranferInterestIntoPR
                    FormValidation = CBool(Me.DGridMain.Rows(index).Cells("colCheck").Value)

                    If FormValidation = True Then
                        Exit For
                    End If
            End Select
        Next

        If FormValidation = False Then
            MsgBox("Nothing to save", MsgBoxStyle.Exclamation, "No data")
        End If
    End Function

#End Region


End Class