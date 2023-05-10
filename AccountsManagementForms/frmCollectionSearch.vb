'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmCollectionSearch
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     February 09, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for the Collection Search
'Arguments/Parameters:  
'Files/Database Tables:  AM_COLLECTION
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   February 09, 2012       Vladimir E. Espiritu                 GUI design and basic functionalities                    
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmCollectionSearch
    Private WBillHelper As WESMBillHelper

    Public Enum EnumFunctionType
        CollectionSearch
        SelectAllocationDate
        ReplenishmentSearch
        InterestSearch
        TransferOfInterestSearch
        SelectJVDate
        SelectNewDueDate
    End Enum

    Private _LoadType As EnumFunctionType

    Public Property LoadType() As EnumFunctionType
        Get
            Return _LoadType
        End Get
        Set(ByVal value As EnumFunctionType)
            _LoadType = value
        End Set
    End Property


    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If Me.ValidateSearch = False Then
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmCollectionSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        Select Case Me.LoadType
            Case EnumFunctionType.CollectionSearch
                'Get the participants
                Dim participants = WBillHelper.GetAMParticipants()

                With Me.ddlParticipantID
                    .Enabled = False
                    .DisplayMember = "ParticipantID"
                    .ValueMember = "IDNumber"
                    .DataSource = participants
                    .SelectedIndex = -1
                End With

                Me.gpCollection.Visible = False
                Me.Text = "Collection Search"

                Dim valLocation As New System.Drawing.Point
                valLocation.X = 3
                valLocation.Y = 3
                Me.gpSearch.Location = valLocation

            Case EnumFunctionType.SelectAllocationDate
                Me.Text = "Allocation Date"
                Me.lblTransaction.Text = "Allocation Date:"
                Me.gpSearch.Visible = False

                Dim valLocation As New System.Drawing.Point
                valLocation.X = 3
                valLocation.Y = 3
                Me.gpCollection.Location = valLocation

            Case EnumFunctionType.ReplenishmentSearch, EnumFunctionType.InterestSearch, EnumFunctionType.TransferOfInterestSearch
                Me.Text = "Transaction Date"
                Me.lblTransaction.Text = "Transaction Date:"
                Me.gpSearch.Visible = False

                Dim valLocation As New System.Drawing.Point
                valLocation.X = 3
                valLocation.Y = 3
                Me.gpCollection.Location = valLocation

                valLocation = New System.Drawing.Point
                valLocation.X = 6
                valLocation.Y = 23
                Me.lblTransaction.Location = valLocation

            Case EnumFunctionType.SelectJVDate
                Me.Text = "Specify the JV Date"
                Me.lblTransaction.Text = "JV Date:"
                Me.gpSearch.Visible = False

                Dim valLocation As New System.Drawing.Point
                valLocation.X = 3
                valLocation.Y = 3
                Me.gpCollection.Location = valLocation

            Case EnumFunctionType.SelectNewDueDate
                Me.Text = "New DueDate"
                Me.lblTransaction.Text = "New DueDate:"
                Me.gpSearch.Visible = False

                Dim valLocation As New System.Drawing.Point
                valLocation.X = 3
                valLocation.Y = 3
                Me.gpCollection.Location = valLocation
        End Select

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub chckParticipantID_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If Me.chckParticipantID.CheckState = CheckState.Checked Then
            Me.ddlParticipantID.Enabled = True
            Me.ddlParticipantID.SelectedIndex = -1
        Else
            Me.ddlParticipantID.Enabled = False
        End If
    End Sub

    Private Sub chckParticipantID_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chckParticipantID.CheckedChanged
        If Me.chckParticipantID.Checked Then
            Me.ddlParticipantID.Enabled = True
        Else
            Me.ddlParticipantID.SelectedIndex = -1
            Me.ddlParticipantID.Enabled = False
        End If
    End Sub

#Region "Methods/Functions"
    Private Function ValidateSearch() As Boolean
        ValidateSearch = False

        If Me.chckParticipantID.CheckState = CheckState.Checked And Me.ddlParticipantID.SelectedIndex = -1 Then
            MsgBox("Please select first the participant id!", MsgBoxStyle.Exclamation, "Specify the inputs")
            Exit Function

        ElseIf CDate(Me.dtFrom.Value.ToString("MM/dd/yyyy")) > CDate(Me.dtTo.Value.ToString("MM/dd/yyyy")) Then
            MsgBox("Invalid date range!", MsgBoxStyle.Exclamation, "Specify the inputs")
            Exit Function

        ElseIf Me.chckAuto.CheckState = CheckState.Unchecked And Me.chckManual.CheckState = CheckState.Unchecked Then
            MsgBox("Please specify first the type of allocation!", MsgBoxStyle.Exclamation, "Specify the inputs")
            Exit Function

        ElseIf Me.chckAllocated.CheckState = CheckState.Unchecked And Me.chckUnallocated.CheckState = CheckState.Unchecked Then
            MsgBox("Please specify first the status!", MsgBoxStyle.Exclamation, "Specify the inputs")
            Exit Function

        ElseIf Me.chckPosted.CheckState = CheckState.Unchecked And Me.chckNotPost.CheckState = CheckState.Unchecked Then
            MsgBox("Please specify first the isposted!", MsgBoxStyle.Exclamation, "Specify the inputs")
            Exit Function

        Else
            ValidateSearch = True
        End If

    End Function
#End Region

End Class