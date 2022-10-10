'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmPrudentialComputeMarginCallSearch
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     January 13, 2013
'Development Group:      Software Development and Support Division
'Description:            GUI for the generation and searching of margin call
'Arguments/Parameters:  
'Files/Database Tables:  frmPrudentialComputeMarginCallSearch
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   January 13, 2013        Vladimir E. Espiritu                 GUI design and basic functionalities                    
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmPrudentialComputeMarginCallSearch
    Public Enum EnumMarginCallSearch
        SelectMarginCallDate
        SearchMarginCall
    End Enum

#Region "Properties"
    Private _LoadType As EnumMarginCallSearch
    Public Property LoadType() As EnumMarginCallSearch
        Get
            Return _LoadType
        End Get
        Set(ByVal value As EnumMarginCallSearch)
            _LoadType = value
        End Set
    End Property

    Private _MarginCallDate As Date
    Public ReadOnly Property MarginCallDate() As Date
        Get
            MarginCallDate = CDate(FormatDateTime(Me.dtMarginCallDate.Value, DateFormat.ShortDate))
            Return MarginCallDate
        End Get
    End Property

    Private _BillingPeriod As Integer
    Public ReadOnly Property BillingPeriod() As Integer
        Get
            _BillingPeriod = CInt(Me.ddlTransDate.SelectedValue)
            Return _BillingPeriod
        End Get
    End Property

    Private _IDNumber As String
    Public ReadOnly Property IDNumber() As String
        Get
            If Me.chckParticipantID.CheckState = CheckState.Checked Then
                _IDNumber = CStr(Me.ddlParticipantID.SelectedValue)
            Else
                _IDNumber = "0"
            End If

            Return _IDNumber
        End Get
    End Property
#End Region

    Private WBillHelper As WESMBillHelper
    Private Sub frmPrudentialComputeMarginCallSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        Try
            Select Case Me.LoadType
                Case EnumMarginCallSearch.SelectMarginCallDate
                    Me.gpSearch.Visible = False
                    Me.Text = "Specify the Margin Call Date"

                    Dim valLocation As New System.Drawing.Point
                    valLocation.X = 11
                    valLocation.Y = 12
                    Me.gpSearch.Location = valLocation

                    Dim valSize As New Drawing.Size
                    valSize.Height = 135
                    valSize.Width = 218
                    Me.Size = valSize

                Case EnumMarginCallSearch.SearchMarginCall
                    'Get all the margin call transaction dates
                    Dim listTransDate = WBillHelper.GetPrudentialMarginCallTransactionDates()

                    If listTransDate.Count = 0 Then
                        MsgBox("No records to search!", MsgBoxStyle.Information, "No data")
                        Me.Close()
                    End If

                    Me.gpDueDate.Visible = False
                    Me.Text = "Search Margin Call"

                    Dim valLocation As New System.Drawing.Point
                    valLocation.X = 11
                    valLocation.Y = 12
                    Me.gpSearch.Location = valLocation

                    Dim valSize As New Drawing.Size
                    valSize.Height = 183
                    valSize.Width = 301
                    Me.Size = valSize

                    'Get all the am participants
                    Dim listParticipants = WBillHelper.GetAMParticipantsAll()

                    With Me.ddlTransDate
                        .DataSource = listTransDate
                        .SelectedIndex = -1
                    End With

                    With Me.ddlParticipantID
                        .Enabled = False
                        .DisplayMember = "ParticipantID"
                        .ValueMember = "IDNumber"
                        .DataSource = listParticipants
                        .SelectedIndex = -1
                    End With
            End Select

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Found")
        End Try
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If Me.LoadType = EnumMarginCallSearch.SearchMarginCall Then
            If Me.ValidateControls = False Then
                Exit Sub
            End If
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub chckParticipantID_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chckParticipantID.CheckedChanged
        If Me.chckParticipantID.CheckState = CheckState.Checked Then
            Me.ddlParticipantID.Enabled = True
        Else
            Me.ddlParticipantID.Enabled = False
            Me.ddlParticipantID.SelectedIndex = -1
        End If
    End Sub

#Region "Methods/Functions"
    Private Function ValidateControls() As Boolean
        Dim result As Boolean = True

        If Me.ddlTransDate.SelectedIndex = -1 Then
            MsgBox("Please specify the transaction date!", MsgBoxStyle.Exclamation, "Specify the inputs")
            result = False
        ElseIf Me.chckParticipantID.CheckState = CheckState.Checked Then
            If Me.ddlParticipantID.SelectedIndex = -1 Then
                MsgBox("Please specify the participant id!", MsgBoxStyle.Exclamation, "Specify the inputs")
                result = False
            End If
        End If

        Return result
    End Function
#End Region

End Class