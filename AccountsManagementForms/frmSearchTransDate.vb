

Option Explicit On
Option Strict On

Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Public Class frmSearchTransDate

#Region "Properties"
    Private _YearNow As Integer
    Public Property YearNow() As Integer
        Get
            Return _YearNow
        End Get
        Set(ByVal value As Integer)
            _YearNow = value
        End Set
    End Property

    Private _TransDate As Date
    Public ReadOnly Property TransDate() As Date
        Get
            _TransDate = New Date(CInt(Me.ddlYear.Value), Me.ddlMonth.SelectedIndex + 1, 1)
            Return _TransDate
        End Get
    End Property

    Private _YearSelected As Integer
    Public ReadOnly Property YearSelected() As Integer
        Get
            _YearSelected = CInt(Me.ddlYear.Value)
            Return _YearSelected
        End Get
    End Property

    Private _QuarterSelected As EnumQuarterlyPeriod
    Public ReadOnly Property QuarterSelected() As EnumQuarterlyPeriod
        Get
            _QuarterSelected = CType(System.Enum.Parse(GetType(EnumQuarterlyPeriod), CStr(Me.ddlQuarter.Text)), EnumQuarterlyPeriod)
            Return _QuarterSelected
        End Get
    End Property

#End Region


    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If Me.ddlQuarter.SelectedIndex = -1 Then
            MsgBox("Please specify first the value of quarter dropdown!", MsgBoxStyle.Critical, "Specify the inputs")
            Exit Sub
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmSearchTransDate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ddlYear.Maximum = YearNow
        For index As Integer = 1 To 12
            Me.ddlMonth.Items.Add(New Date(Now.Year, index, 1).ToString("MMMM"))
        Next

        Me.ddlYear.Value = Me.YearNow

        With Me.ddlQuarter.Items
            .Add(EnumQuarterlyPeriod.FirstQuarter)
            .Add(EnumQuarterlyPeriod.SecondQuarter)
            .Add(EnumQuarterlyPeriod.ThirdQuarter)
            .Add(EnumQuarterlyPeriod.FourthQuarter)
        End With

        Me.ddlQuarter.SelectedIndex = -1
    End Sub

End Class