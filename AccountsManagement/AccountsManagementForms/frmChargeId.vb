'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmChargeId
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     August 23, 2011
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
'   September 02, 2011      Juan Carlo L. Panopio               Changed loading of records to the For-Loop method
'   September 02, 2011      Juan Carlo L. Panopio               Added temporary logging of actions for Save/Delete/Add
'   September 14, 2011      Juan Carlo L. Panopio               Changed Charge ID Type to ENUMChargeType
'   September 26, 2011      Juan Carlo L. Panopio               Updated Charge ID to allow limited Input Only, Fixed Bugs found due to 
'                                                               Changes that were made in the Charge ID to ENUMChargeType
Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmChargeId
    Dim WBillHelper As WESMBillHelper
    Dim Admin As Boolean
    Private Sub frmChargeId_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName
        Me.Admin = False
        Me.LoadRecord()
        Me.ResizeRedraw = True
    End Sub

    Public Sub LoadRecord()
        Me.ToolStripStatusLabel2.Text = "Loading Record/s"
        Try

            Dim chargeId = WBillHelper.GetChargeIDCodes()
            Me.dgv_ChargeID.Rows.Clear()
            For Each rec In chargeId
                Me.dgv_ChargeID.Rows.Add(rec.ChargeId, rec.Description, If(rec.cIDType = EnumChargeType.E, "Energy", If(rec.cIDType = EnumChargeType.EV, "VAT on Energy", If(rec.cIDType = EnumChargeType.MF, "Market Fees", "VAT on Market Fees"))), If(rec.Status = 1, "Active", "In-Active"), FormatDateTime(rec.updDate), rec.updBy)
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")           
        End Try
        Me.ToolStripStatusLabel2.Text = "Active"
    End Sub
    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_ChargeID.CellClick
        Me.ToolStripStatusLabel2.Text = "View"
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_ChargeID.CellDoubleClick
        If e.RowIndex = -1 Then
            Exit Sub
        End If
        frmChargeIdMgt.State = "VIEW"
        frmChargeIdMgt.ShowDialog()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Me.ToolStripStatusLabel2.Text = "Add New Record"
        frmChargeIdMgt.State = "ADD"
        frmChargeIdMgt.ShowDialog()
        Me.ToolStripStatusLabel2.Text = "Active"
    End Sub

    Private Sub btn_TSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_TSearch.Click
        Me.ToolStripStatusLabel2.Text = "Searching..."
        Try
            Dim i = WBillHelper.srchLikeGetChargeID("%" & Me.txtSearch.Text & "%")
            If i.Count <> 0 Then
                Me.dgv_ChargeID.Rows.Clear()
                For Each rec In i
                    Me.dgv_ChargeID.Rows.Add(rec.ChargeId, rec.Description, If(rec.cIDType = EnumChargeType.E, "Energy", If(rec.cIDType = EnumChargeType.EV, "VAT on Energy", If(rec.cIDType = EnumChargeType.MF, "Market Fees", "VAT on Market Fees"))), If(rec.Status = 1, "Active", "In-Active"), FormatDateTime(rec.updDate, DateFormat.ShortDate), rec.updBy)
                Next

            ElseIf i.Count = 0 Then
                MsgBox("No record found!", MsgBoxStyle.Information, "No Record")
                Me.LoadRecord()
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")           
        End Try
        Me.ToolStripStatusLabel2.Text = "Search Complete!"
    End Sub

    Private Sub btnEdit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Me.ToolStripStatusLabel2.Text = "Update Record"
        frmChargeIdMgt.State = "EDIT"
        frmChargeIdMgt.ShowDialog()
        Me.ToolStripStatusLabel2.Text = "Active"
    End Sub

    Private Sub btnDelete_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim ans As MsgBoxResult
        Me.ToolStripStatusLabel2.Text = "Delete Record"
        If ((UCase(dgv_ChargeID.Rows(dgv_ChargeID.CurrentRow.Index).Cells(3).Value.ToString)) = "IN-ACTIVE") Then
            MsgBox("Cannot set In-Active record to In-Active", MsgBoxStyle.Exclamation, "Set to In-Active")
            Exit Sub
        End If
        ans = MsgBox("Do you want to set the record to In-Active?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "Set to In-Active")
        Try
            If ans = MsgBoxResult.Yes Then
                Dim item As New ChargeId

                With item
                    .ChargeId = dgv_ChargeID.Rows(dgv_ChargeID.CurrentRow.Index).Cells(0).Value.ToString
                    Select Case UCase(dgv_ChargeID.Rows(dgv_ChargeID.CurrentRow.Index).Cells(2).Value.ToString)
                        Case "ENERGY"
                            .cIDType = EnumChargeType.E
                        Case "VAT on Energy"
                            .cIDType = EnumChargeType.EV
                        Case "MARKET FEES"
                            .cIDType = EnumChargeType.MF
                        Case "VAT on Market Fees"
                            .cIDType = EnumChargeType.MFV
                    End Select
                    .Description = (dgv_ChargeID.Rows(dgv_ChargeID.CurrentRow.Index).Cells(1).Value.ToString)
                    .Status = If((UCase(dgv_ChargeID.Rows(dgv_ChargeID.CurrentRow.Index).Cells(3).Value.ToString)) = "ACTIVE", 1, 0)
                End With
                WBillHelper.delChargeID(item)
                Dim strLogs As String = ""
                With item
                    strLogs = "Deleted record " & .ChargeId
                    strLogs &= ", Description: " & .Description
                    strLogs &= ", Charge ID Type: " & If(.cIDType = EnumChargeType.E, "Energy", If(.cIDType = EnumChargeType.EV, "VAT on Energy", If(.cIDType = EnumChargeType.MF, "Market Fees", "VAT on Market Fees")))
                End With

                MsgBox("Record Successfully set to In-Active!", MsgBoxStyle.Information, "Set to In-Active/")
                'Updated by lance 08/24/2014
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibChargeIDWindow.ToString, strLogs, "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccessfullySearched.ToString(), AMModule.UserName)

            Else
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")           
        End Try
        Me.LoadRecord()
        Me.ToolStripStatusLabel2.Text = "Active"
    End Sub
    Private Sub btnRefresh_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        txtSearch.Clear()
        txtSearch.Focus()
        LoadRecord()
    End Sub

    Private Sub cmd_close_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub

End Class