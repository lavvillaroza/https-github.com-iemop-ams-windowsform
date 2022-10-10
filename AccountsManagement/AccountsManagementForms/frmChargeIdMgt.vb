'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmChargeIdMgt
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     August 25, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI the maintenance of Charge IDs View/Delete
'Arguments/Parameters:  
'Files/Database Tables:  AM_CHARGE_ID_LIB
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description
'   September 08, 2011      Juan Carlo L. Panopio               Changed Edit Method to not to refresh the whole Grid View after edit
'   September 14, 2011      Juan Carlo L. Panopio               Changed Charge ID type based on EnumChargeType

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
'Imports LDAPLogin
'Imports LDAPLib

Public Class frmChargeIdMgt
    Dim WBillHelper As WESMBillHelper
    Private _oValue As New ChargeId
    Private _State As String
    Public Property State() As String
        Get
            Return _State
        End Get
        Set(ByVal value As String)
            Me._State = value
        End Set
    End Property

    Private Sub frmChargeIdMgt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName
        If Me.State = "ADD" Then
            Me.txt_cId.ReadOnly = False
            Me.txt_desc.ReadOnly = False
            Me.rb_active.Enabled = True
            Me.rb_inactive.Enabled = True
            Me.cbo_Type.Enabled = True
            fillCombo()
            Me.txt_cId.Text = ""
            Me.txt_desc.Text = ""
            Me.rb_active.Checked = True
            Me.rb_inactive.Checked = False
            Me.cbo_Type.SelectedIndex = 0
            Me.cmdCancel.Text = "Cancel"
            Me.cmdSave.Visible = True
            Me.lbl_addEdit.Text = ""
            Me.lbl_dcomm.Text = ""
            With _oValue
                .ChargeId = "ADD NEW"
                .cIDType = CType(Me.cbo_Type.SelectedIndex, EnumChargeType)
                .Description = Me.txt_desc.Text
                If rb_active.Checked = True Then
                    .Status = 1
                ElseIf rb_inactive.Checked = True Then
                    .Status = 0
                End If
            End With
        ElseIf Me.State = "VIEW" Then
            Me.txt_cId.ReadOnly = True
            Me.txt_desc.ReadOnly = True
            Me.rb_active.Enabled = True
            Me.rb_inactive.Enabled = True
            Me.cbo_Type.Enabled = True
            Me.cmdSave.Visible = False
            Me.cmdCancel.Text = "Close"
            Me.rb_active.Enabled = False
            Me.rb_inactive.Enabled = False
            fillCombo()
            With frmChargeId.dgv_ChargeID.Rows(frmChargeId.dgv_ChargeID.CurrentRow.Index)
                .Selected = True
                Me.txt_cId.Text = .Cells(0).Value.ToString()
                Me.txt_desc.Text = .Cells(1).Value.ToString()
                Me.cbo_Type.SelectedItem = .Cells(2).Value.ToString
                If .Cells(3).Value.ToString = "Active" Then
                    Me.rb_active.Checked = True
                Else
                    Me.rb_inactive.Checked = True
                End If
                Me.lbl_dcomm.Text = .Cells(4).Value.ToString
                Me.lbl_addEdit.Text = .Cells(5).Value.ToString
            End With
            Me.cbo_Type.Enabled = False

        ElseIf Me.State = "EDIT" Then
            Me.txt_cId.ReadOnly = True
            Me.txt_desc.ReadOnly = False
            Me.rb_active.Enabled = False
            Me.rb_inactive.Enabled = False
            Me.cbo_Type.Enabled = False
            fillCombo()
            Me.rb_active.Enabled = True
            Me.rb_inactive.Enabled = True
            With frmChargeId.dgv_ChargeID.Rows(frmChargeId.dgv_ChargeID.CurrentRow.Index)
                .Selected = True
                Me.txt_cId.Text = .Cells(0).Value.ToString()
                Me.txt_cId.ReadOnly = True
                Me.cbo_Type.Enabled = True
                Me.txt_desc.Text = .Cells(1).Value.ToString()
                Me.cbo_Type.SelectedItem = .Cells(2).Value.ToString
                If .Cells(3).Value.ToString = "Active" Then
                    Me.rb_active.Checked = True
                    Me.rb_inactive.Checked = False
                Else
                    Me.rb_active.Checked = False
                    Me.rb_inactive.Checked = True
                End If
                Me.lbl_dcomm.Text = .Cells(4).Value.ToString
                Me.lbl_addEdit.Text = .Cells(5).Value.ToString
            End With
            Me.cmdCancel.Text = "Cancel"
            Me.cmdSave.Visible = True
            With _oValue
                .ChargeId = Replace(Me.txt_cId.Text, "|", "")
                .cIDType = CType(Me.cbo_Type.SelectedIndex - 1, EnumChargeType)
                .Description = Replace(Me.txt_desc.Text, "|", "")
                If rb_active.Checked = True Then
                    .Status = 1
                ElseIf rb_inactive.Checked = True Then
                    .Status = 0
                End If
            End With
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If Me.txt_cId.Text.Trim.Length = 0 Then
            MsgBox("Please specify the Charge ID!", MsgBoxStyle.Critical, "Specify the inputs")
            Me.txt_cId.Select()
            Exit Sub
        ElseIf Me.txt_desc.Text.Trim.Length = 0 Then
            MsgBox("Please Specify the Description of the Charge ID!", MsgBoxStyle.Critical, "Specify the inputs")
            Me.txt_desc.Select()
            Exit Sub
        Else
            If Me.State = "ADD" Then
                Try
                    Dim i = WBillHelper.GetChargeId(txt_cId.Text.Trim)
                    If i.ChargeId.Length <> 0 Then
                        MsgBox("Duplicate Charge ID!", MsgBoxStyle.Critical, "Duplicate entry")
                        Me.txt_cId.Select()
                        SendKeys.Send("{HOME}+{END}")
                        Exit Sub
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End Try
            End If

        End If

        Dim item As New ChargeId
        With item
            .ChargeId = Replace(Me.txt_cId.Text, "|", "")
            Dim selCharge = IIf(Me.cbo_Type.SelectedItem.ToString = "Energy", EnumChargeType.E, IIf(Me.cbo_Type.SelectedItem.ToString = "VAT on Energy", EnumChargeType.EV, _
                                                                                             IIf(Me.cbo_Type.SelectedItem.ToString = "Market Fees", EnumChargeType.MF, EnumChargeType.MFV)))
            .cIDType = CType(selCharge, EnumChargeType)
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

                    WBillHelper.SaveChargeIdCode(item, True)
                    MsgBox("Record Successfully Added!", MsgBoxStyle.Information, "Added")

                    strLogs = "Added Record "
                    With item
                        strLogs &= .ChargeId & ", "
                        strLogs &= .cIDType & ", "
                        strLogs &= .Description & "."
                    End With

                    'LDAPModule.LDAP.InsertLog(Me.Name, EnumAMSModules.AMS_ChargeIDWindow.ToString(), strLogs, "", "", EnumColorCode.Blue.ToString(), EnumLogType.SuccesffullySaved.ToString(), 'LDAPModule.LDAP.Username)

                    frmChargeId.LoadRecord()
                    Me.Close()
                End If
            ElseIf Me.State = "EDIT" Then
                ans = MsgBox("Do you really want to update?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "Update")
                If ans = MsgBoxResult.Yes Then
                    WBillHelper.SaveChargeIdCode(item, False)
                    MsgBox("Record Successfully Updated!", MsgBoxStyle.Information, "Updated")
                    Dim i = WBillHelper.srchGetChargeID(item.ChargeId)

                    strLogs = "Updated record to"
                    With item
                        strLogs &= " Charge ID: " & .ChargeId
                        strLogs &= ", Charge ID Type: " & .cIDType
                        strLogs &= ", Description: " & .Description
                        strLogs &= ", Status" & .Status
                    End With

                    With frmChargeId.dgv_ChargeID.Rows(frmChargeId.dgv_ChargeID.CurrentRow.Index)
                        For Each rec In i
                            strLogs &= "From"
                            strLogs &= " Charge ID: " & .Cells(0).Value.ToString
                            strLogs &= ", Description: " & .Cells(1).Value.ToString
                            strLogs &= ", Charge Type: " & .Cells(2).Value.ToString
                            strLogs &= ", Status: " & .Cells(3).Value.ToString

                            .Cells(0).Value = rec.ChargeId
                            .Cells(1).Value = rec.Description
                            .Cells(2).Value = If(rec.cIDType = EnumChargeType.E, "Energy", If(rec.cIDType = EnumChargeType.EV, "VAT on Energy", If(rec.cIDType = EnumChargeType.MF, "Market Fees", "VAT on Market Fees")))
                            .Cells(3).Value = If(rec.Status = 1, "Active", "In-Active")
                            .Cells(4).Value = FormatDateTime(rec.updDate, DateFormat.ShortDate)
                            .Cells(5).Value = rec.updBy
                        Next
                    End With
                    'LDAPModule.LDAP.InsertLog(Me.Name, EnumAMSModules.AMS_ChargeIDWindow.ToString(), strLogs, "", "", EnumColorCode.Blue.ToString(), EnumLogType.SuccesffullySaved.ToString(), 'LDAPModule.LDAP.Username)
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'LDAPModule.LDAP.InsertLog(Me.Name, EnumAMSModules.AMS_ChargeIDWindow.ToString(), ex.Message, "", "", EnumColorCode.Red.ToString(), EnumLogType.ErrorInSaving.ToString(), 'LDAPModule.LDAP.Username)
            Exit Sub
        End Try
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub fillCombo()
        Me.cbo_Type.Items.Clear()
        Me.cbo_Type.Items.Add("Energy")
        Me.cbo_Type.Items.Add("VAT on Energy")
        Me.cbo_Type.Items.Add("Market Fees")
        Me.cbo_Type.Items.Add("VAT on Market Fees")
    End Sub

    Private Sub txt_desc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_desc.KeyDown, txt_cId.KeyDown
        Select Case e.KeyValue
            Case 222
                e.SuppressKeyPress = True
            Case Else
        End Select
    End Sub


End Class