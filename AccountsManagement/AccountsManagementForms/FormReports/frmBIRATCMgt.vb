
Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmBIRATCMgt
    Dim WBillHelper As WESMBillHelper
    Private _State As String
    Public Property State() As String
        Get
            Return _State
        End Get
        Set(ByVal value As String)
            Me._State = value
        End Set
    End Property
    Private Sub frmBIRATCMgt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName
        If Me.State = "ADD" Then
            Me.ATCName_Txtbox.ReadOnly = False
            Me.ATCDesc_Txtbox.ReadOnly = False
            Me.ATCRate_TxtBox.ReadOnly = False
            Me.ATCName_Txtbox.Select()

            Me.ATCName_Txtbox.Text = ""
            Me.ATCDesc_Txtbox.Text = ""
            Me.ATCRate_TxtBox.Text = ""

            Me.cmdCancel.Text = "Cancel"
            Me.cmdSave.Visible = True

        ElseIf Me.State = "VIEW" Then        
            With frmBIRATC.GridView.Rows(frmBIRATC.GridView.CurrentRow.Index)
                .Selected = True
                Me.ATCName_Txtbox.Text = .Cells(0).Value.ToString()
                Me.ATCName_Txtbox.ReadOnly = True
                Me.ATCDesc_Txtbox.Text = .Cells(1).Value.ToString()
                Me.ATCRate_TxtBox.Text = .Cells(2).Value.ToString()
            End With
            Me.ATCName_Txtbox.ReadOnly = True
            Me.ATCDesc_Txtbox.ReadOnly = True
            Me.ATCRate_TxtBox.ReadOnly = True
            Me.cmdSave.Visible = False
            Me.cmdCancel.Text = "Close"

        ElseIf Me.State = "EDIT" Then            
            With frmBIRATC.GridView.Rows(frmBIRATC.GridView.CurrentRow.Index)
                .Selected = True
                Me.ATCName_Txtbox.Text = .Cells(0).Value.ToString()
                Me.ATCName_Txtbox.ReadOnly = True
                Me.ATCDesc_Txtbox.Text = .Cells(1).Value.ToString()
                Me.ATCRate_TxtBox.Text = .Cells(3).Value.ToString
            End With
            Me.ATCName_Txtbox.ReadOnly = True
            Me.ATCDesc_Txtbox.ReadOnly = False
            Me.ATCRate_TxtBox.ReadOnly = False
            Me.cmdCancel.Text = "Cancel"
            Me.cmdSave.Visible = True
        End If
    End Sub

    Private Sub ATCRate_TxtBox_Leave(sender As Object, e As EventArgs) Handles ATCRate_TxtBox.Leave
        If Not IsNumeric(ATCRate_TxtBox.Text) Then
            MessageBox.Show("You are inputting not numeric value, please input the numeric value.")
            ATCRate_TxtBox.Text = "0.00"
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If Me.ATCName_Txtbox.Text.Trim.Length = 0 Then
            MsgBox("Please specify the Alphanumeric Tax Code!", MsgBoxStyle.Critical, "Specify the inputs")
            Me.ATCName_Txtbox.Select()
            Exit Sub
        ElseIf Me.ATCDesc_Txtbox.Text.Trim.Length = 0 Then
            MsgBox("Please specify the Description of the Alphanumeric Tax Code!", MsgBoxStyle.Critical, "Specify the inputs")
            Me.ATCDesc_Txtbox.Select()
            Exit Sub
        ElseIf Me.ATCRate_TxtBox.Text.Trim.Length = 0 Then
            MsgBox("Please specify the Rate of the Alphanumeric Tax Code!", MsgBoxStyle.Critical, "Specify the inputs")
            Me.ATCRate_TxtBox.Select()
            Exit Sub
        Else
            If Me.State = "ADD" Then
                Try
                    Dim i As List(Of BIRAlphanumericTaxCode) = WBillHelper.VerifyBIRATC(ATCName_Txtbox.Text.Trim)
                    If i.Count > 0 Then
                        MsgBox("Duplicate Alphanumeric Tax Code!", MsgBoxStyle.Critical, "Duplicate entry")
                        Me.ATCName_Txtbox.Select()
                        SendKeys.Send("{HOME}+{END}")
                        Exit Sub
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End Try
            End If
        End If
        Dim item As New BIRAlphanumericTaxCode
        With item
            .ATCName = CStr(Me.ATCName_Txtbox.Text.Trim)
            .ATCDescription = CStr(Me.ATCDesc_Txtbox.Text.Trim)
            .ATCRate = CDec(Me.ATCRate_TxtBox.Text.Trim)
            .UpdatedBy = AMModule.UserName
        End With

        Try
            Dim ans As MsgBoxResult
            Dim strLogs As String = ""
            If Me.State = "ADD" Then
                ans = MsgBox("Do You really want to save?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "Save")
                If ans = MsgBoxResult.Yes Then
                    WBillHelper.AddBIRATC(item)
                    strLogs &= "Added new record "
                    With item
                        strLogs &= "ATC Name: " & .ATCName
                        strLogs &= ", ATC Description: " & .ATCDescription
                        strLogs &= ", ATC Rate: " & .ATCRate
                    End With

                    'Updated By Lance 8/24/2014
                    _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibBATCodeWindow.ToString, strLogs, "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccesffullySaved.ToString(), AMModule.UserName)

                    MsgBox("Record Successfully Added!", MsgBoxStyle.Information, "Added")
                    frmBIRATC.LoadRecords()
                    Me.Close()
                End If
            ElseIf Me.State = "EDIT" Then
                ans = MsgBox("Do you really want to update?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "Update")
                If ans = MsgBoxResult.Yes Then
                    WBillHelper.UPDateBIRATC(item)
                    MsgBox("Record Successfully Updated!", MsgBoxStyle.Information, "Updated")                  
                    strLogs &= "Updated existing record "
                    With item
                        strLogs &= "ATC Name: " & .ATCName
                        strLogs &= ", ATC Description: " & .ATCDescription
                        strLogs &= ", ATC Rate: " & .ATCRate
                    End With

                    Dim BIRATCList As New List(Of BIRAlphanumericTaxCode)
                    If frmAccountingCode.txtSearch.Text.Length <> 0 Then
                        BIRATCList = WBillHelper.srchBIRATC("%" & frmBIRATC.txtSearch.Text & "%")
                    Else
                        BIRATCList = WBillHelper.srchBIRATC("%" & frmBIRATC.txtSearch.Text & "%")
                    End If

                    frmAccountingCode.GridView.Rows.Clear()

                    For Each rec In BIRATCList
                        frmBIRATC.GridView.Rows.Add(rec.ATCName, rec.ATCDescription, rec.ATCRate.ToString, rec.UpdateDate, rec.UpdatedBy)
                    Next

                    'Updated By Lance 8/24/2014
                    _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibBATCodeWindow.ToString, strLogs, "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccessfullyEdited.ToString(), AMModule.UserName)

                    Me.Close()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")

            'Updated By Lance 8/24/2014
            If Me.State = "ADD" Then
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibBATCodeWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInSaving.ToString(), AMModule.UserName)
            ElseIf Me.State = "EDIT" Then
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibBATCodeWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInEditing.ToString(), AMModule.UserName)
            End If

            Exit Sub

        End Try
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

End Class