'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmAccountingCodeMgt
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     August 26, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI the maintenance of Accounting codes Viewing and Saving
'Arguments/Parameters:  
'Files/Database Tables:  AM_ACCOUNTING_CODE
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description



Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

'Imports LDAPLib
'Imports LDAPLogin

Public Class frmAccountingCodeMgt
    Dim WBillHelper As WESMBillHelper
    Private _oValue As New AccountingCode
    Private _State As String
    Public Property State() As String
        Get
            Return _State
        End Get
        Set(ByVal value As String)
            Me._State = value
        End Set
    End Property
    Private Sub frmAccountingCodeMgt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName
        If Me.State = "ADD" Then
            Me.txt_acID.ReadOnly = False
            Me.txt_desc.ReadOnly = False
            Me.rb_active.Enabled = True
            Me.rb_inactive.Enabled = True
            Me.txt_acID.Text = ""
            Me.txt_desc.Text = ""
            Me.rb_active.Checked = True
            Me.rb_inactive.Checked = False
            Me.cmdCancel.Text = "Cancel"
            Me.cmdSave.Visible = True
            Me.lbl_addEdit.Text = ""
            Me.lbl_dcomm.Text = ""
            With _oValue
                .AccountCode = "Add New"
                .Description = Me.txt_desc.Text.Trim
                If Me.rb_active.Checked = True Then
                    .Status = 1
                Else
                    .Status = 0
                End If
            End With
        ElseIf Me.State = "VIEW" Then
            Me.txt_acID.ReadOnly = True
            Me.txt_desc.ReadOnly = True
            Me.rb_active.Enabled = True
            Me.rb_inactive.Enabled = True
            Me.cmdSave.Visible = False
            Me.cmdCancel.Text = "Close"
            Me.rb_active.Enabled = False
            Me.rb_inactive.Enabled = False
            With frmAccountingCode.GridView.Rows(frmAccountingCode.GridView.CurrentRow.Index)
                .Selected = True
                Me.txt_acID.Text = .Cells(0).Value.ToString()
                Me.txt_desc.Text = .Cells(1).Value.ToString()
                If .Cells(2).Value.ToString = "Active" Then
                    Me.rb_active.Checked = True
                Else
                    Me.rb_inactive.Checked = True
                End If
                Me.lbl_dcomm.Text = .Cells(3).Value.ToString
                Me.lbl_addEdit.Text = .Cells(4).Value.ToString
            End With
        ElseIf Me.State = "EDIT" Then
            Me.txt_acID.ReadOnly = True
            Me.txt_desc.ReadOnly = False
            Me.rb_active.Enabled = False
            Me.rb_inactive.Enabled = False
            Me.rb_active.Enabled = True
            Me.rb_inactive.Enabled = True
            With frmAccountingCode.GridView.Rows(frmAccountingCode.GridView.CurrentRow.Index)
                .Selected = True
                Me.txt_acID.Text = .Cells(0).Value.ToString()
                Me.txt_acID.ReadOnly = True
                Me.txt_desc.Text = .Cells(1).Value.ToString()
                If .Cells(2).Value.ToString = "Active" Then
                    Me.rb_active.Checked = True
                    Me.rb_inactive.Checked = False
                Else
                    Me.rb_active.Checked = False
                    Me.rb_inactive.Checked = True
                End If
                Me.lbl_dcomm.Text = .Cells(3).Value.ToString
                Me.lbl_addEdit.Text = .Cells(4).Value.ToString
            End With
            Me.cmdCancel.Text = "Cancel"
            Me.cmdSave.Visible = True

            With _oValue
                .AccountCode = Replace(Me.txt_acID.Text, "|", "")
                .Description = Replace(Me.txt_desc.Text.Trim, "|", "")
                If Me.rb_active.Checked = True Then
                    .Status = 1
                Else
                    .Status = 0
                End If
            End With
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If Me.txt_acID.Text.Trim.Length = 0 Then
            MsgBox("Please specify the Account Code!", MsgBoxStyle.Critical, "Specify the inputs")
            Me.txt_acID.Select()
            Exit Sub
        ElseIf Me.txt_desc.Text.Trim.Length = 0 Then
            MsgBox("Please specify the Description of the Account Code!", MsgBoxStyle.Critical, "Specify the inputs")
            Me.txt_desc.Select()
            Exit Sub
        Else
            If Me.State = "ADD" Then
                Try                    
                    Dim i As AccountingCode = WBillHelper.GetAccountingCode(CStr(txt_acID.Text.Trim.ToString))
                    If i.AccountCode.Length <> 0 Then
                        MsgBox("Duplicate Account Code!", MsgBoxStyle.Critical, "Duplicate entry")
                        Me.txt_acID.Select()
                        SendKeys.Send("{HOME}+{END}")
                        Exit Sub
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End Try
            End If

        End If
        Dim item As New AccountingCode
        With item
            .AccountCode = Replace(Me.txt_acID.Text, "|", "")
            .Description = Replace(Me.txt_desc.Text, "|", "")
            If rb_active.Checked = True Then
                .Status = 1
            ElseIf rb_inactive.Checked = True Then
                .Status = 0
            End If
        End With

        Try
            Dim ans As MsgBoxResult
            Dim strLogs As String = ""
            If Me.State = "ADD" Then
                ans = MsgBox("Do You really want to save?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "Save")
                If ans = MsgBoxResult.Yes Then
                    WBillHelper.SaveAccountingCode(item, True)
                    strLogs &= "Added new record "
                    With item
                        strLogs &= "Account Code: " & .AccountCode
                        strLogs &= ", Description: " & .Description
                        strLogs &= ", " & If(.Status = 1, "Active", "In-Active")
                    End With

                    'Updated By Lance 8/24/2014
                    _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibAccountCodeWindow.ToString, strLogs, "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccesffullySaved.ToString(), AMModule.UserName)

                    MsgBox("Record Successfully Added!", MsgBoxStyle.Information, "Added")
                    frmAccountingCode.LoadRecords()
                    Me.Close()
                End If
            ElseIf Me.State = "EDIT" Then
                ans = MsgBox("Do you really want to update?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "Update")
                If ans = MsgBoxResult.Yes Then
                    WBillHelper.SaveAccountingCode(item, False)
                    MsgBox("Record Successfully Updated!", MsgBoxStyle.Information, "Updated")
                    Dim i = WBillHelper.srchAccountingCode("%" & item.AccountCode & "%")

                    strLogs &= "Updated existing record "
                    With item
                        strLogs &= "From Account Code: " & .AccountCode
                        strLogs &= ", Description: " & .Description
                        strLogs &= ", " & If(.Status = 1, "Active", "In-Active")
                    End With

                    Dim AccountCodes As New List(Of AccountingCode)
                    If frmAccountingCode.txtSearch.Text.Length <> 0 Then
                        AccountCodes = WBillHelper.srchAccountingCode("%" & frmAccountingCode.txtSearch.Text & "%")
                    Else
                        AccountCodes = WBillHelper.srchAccountingCode("%" & frmAccountingCode.txtSearch.Text & "%")
                    End If

                    frmAccountingCode.GridView.Rows.Clear()

                    For Each rec In AccountCodes
                        frmAccountingCode.GridView.Rows.Add(rec.AccountCode, rec.Description, If(rec.Status = 1, "Active", "In-Active"), rec.DateCommit, rec.updatedBy)
                    Next

                    'Updated By Lance 8/24/2014
                    _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibAccountCodeWindow.ToString, strLogs, "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccessfullyEdited.ToString(), AMModule.UserName)

                    Me.Close()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub
    Private Sub clickOnce()
        With frmChargeId.dgv_ChargeID.Rows(frmChargeId.dgv_ChargeID.CurrentRow.Index)
            .Selected = True
            Me.txt_acID.Text = .Cells(0).Value.ToString()
            Me.txt_desc.Text = .Cells(1).Value.ToString()
            If .Cells(3).Value.ToString = "Active" Then
                rb_active.Checked = True
            Else
                rb_inactive.Checked = True
            End If
            lbl_dcomm.Text = .Cells(4).Value.ToString
            lbl_addEdit.Text = .Cells(5).Value.ToString
        End With
    End Sub

    Private Sub txt_acID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_acID.KeyDown, txt_desc.KeyDown
        Select Case e.KeyValue
            Case 222
                e.SuppressKeyPress = True
            Case Else
        End Select
    End Sub

End Class