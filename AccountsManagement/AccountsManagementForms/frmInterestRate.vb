'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmInterestRate
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     January 12, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for the maintenance of Daily Interest Rate
'Arguments/Parameters:  
'Files/Database Tables:  AM_DAILY_INTEREST_RATE
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   January 12, 2012        Vladimir E. Espiritu                 GUI design and basic functionalities                    
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

'Imports LDAPLib
'Imports LDAPLogin

Public Class frmInterestRate
    Private WBillHelper As WESMBillHelper
    Private _dicInterestRate As Dictionary(Of Date, Decimal)

    Private Sub frmInterestRate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MdiParent = MainForm

        Try
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName

            Me.LoadDailyInterestRate()
            Me.EnableControls(False, False, True, False, False, False)

        Catch ex As Exception
            'LDAPModule.LDAP.InsertLog(Me.Name, EnumAMSModules.AMS_DailyInterestWindow.ToString(), "Error in loading of window", ex.Message, "", EnumColorCode.Red.ToString(), EnumLogType.ErrorInAccessing.ToString(), 'LDAPModule.LDAP.Username)
        End Try
    End Sub

    Public Sub EnableControls(ByVal vDate As Boolean, ByVal vInterestRate As Boolean, ByVal vAdd As Boolean, _
                              ByVal vEdit As Boolean, ByVal vSave As Boolean, ByVal vCancel As Boolean)
        Me.dtDate.Enabled = vDate
        Me.txtInterestRate.Enabled = vInterestRate
        Me.btnNew.Enabled = vAdd
        Me.btnEdit.Enabled = vEdit
        Me.btnSave.Enabled = vSave
        Me.btnCancel.Enabled = vCancel
    End Sub

    Public Sub LoadDailyInterestRate()
        Try
            Me._dicInterestRate = Me.WBillHelper.GetDailyInterestRate()

            Me.DGridView.Rows.Clear()
            For Each item In Me._dicInterestRate
                Dim transDate As Date = item.Key
                Dim interestRate As Decimal = CDec(item.Value)
                Me.DGridView.Rows.Add(transDate, FormatNumber(interestRate * 100D, 4))
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")            
            Me.Close()
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Me.EnableControls(True, True, True, False, True, True)

        Me.txtInterestRate.Text = ""
        If Me._dicInterestRate.Count = 0 Then
            Me.dtDate.Value = Now()
        Else
            Dim dates = (From x In Me._dicInterestRate Select x.Key Order By Key Descending).ToList()
            Me.dtDate.Value = dates(0).AddDays(1)
        End If
    End Sub

    Private Sub DGridView_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGridView.CellClick
        If Me.DGridView.RowCount = 0 Or Me.btnSave.Enabled Or e.RowIndex = -1 Then
            Exit Sub
        End If

        Me.dtDate.Value = CDate(Me.DGridView.Rows(e.RowIndex).Cells(0).Value)
        Me.txtInterestRate.Text = Me.DGridView.Rows(e.RowIndex).Cells(1).Value.ToString()
        Me.EnableControls(False, False, True, True, False, False)
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Me.EnableControls(False, True, True, False, True, True)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            Dim transDate = CDate(FormatDateTime(Me.dtDate.Value, DateFormat.ShortDate))

            If Me.dtDate.Enabled = True Then

                If Me._dicInterestRate.ContainsKey(transDate) Then
                    MsgBox("Duplicate Entry!", MsgBoxStyle.Exclamation, "Warning")
                    Exit Sub
                End If
            End If

            If Me.txtInterestRate.Text.Trim.Length = 0 Then
                MsgBox("Please specify the interest rate!", MsgBoxStyle.Exclamation, "Warning")
                Exit Sub
            ElseIf Not IsNumeric(Me.txtInterestRate.Text) Then
                MsgBox("Interest rate must be numeric!", MsgBoxStyle.Exclamation, "Warning")
                Exit Sub
            ElseIf CDec(Me.txtInterestRate.Text) > 100 Or CDec(Me.txtInterestRate.Text) <= 0 Then
                MsgBox("Interest rate must be less than or equal to 100 but more than 0!", MsgBoxStyle.Exclamation, "Warning")
                Exit Sub
            End If

            Dim ans = MsgBox("Do you really want to save this record?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")

            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            If Me.dtDate.Enabled = True Then
                Me.WBillHelper.SaveDailyInterest(transDate, CDec(Me.txtInterestRate.Text) / 100D, 0)
                MsgBox("Successfully Saved!", MsgBoxStyle.Information, "Save")

                Me.DGridView.Rows.Insert(0, transDate, FormatNumber(Me.txtInterestRate.Text, 4))
                Me._dicInterestRate.Add(transDate, CDec(Me.txtInterestRate.Text) / 100D)

                'Updated By Lance 08/17/2014
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibDailyInterestRateWindow.ToString, transDate.ToString() & " - " & CStr(CDec(Me.txtInterestRate.Text) / 100D), "New record", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccesffullySaved.ToString, AMModule.UserName)
            Else
                Me.WBillHelper.SaveDailyInterest(transDate, CDec(Me.txtInterestRate.Text) / 100D, 1)
                MsgBox("Successfully Updated!", MsgBoxStyle.Information, "Update")

                'Updated By Lance 08/17/2014
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibDailyInterestRateWindow.ToString, transDate.ToString() & " - " & CStr(CDec(Me.txtInterestRate.Text) / 100D), "Update record from " & Me._dicInterestRate(transDate).ToString(), "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccesffullySaved.ToString, AMModule.UserName)

                Me._dicInterestRate(transDate) = CDec(Me.txtInterestRate.Text) / 100D
                Me.LoadDailyInterestRate()
            End If

            Me.EnableControls(False, False, True, True, False, False)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")          
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.EnableControls(False, False, True, False, False, False)
        Me.dtDate.Value = Now()
        Me.txtInterestRate.Text = ""
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub DGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGridView.CellContentClick

    End Sub

    Private Sub dtDate_ValueChanged(sender As Object, e As EventArgs) Handles dtDate.ValueChanged

    End Sub
End Class