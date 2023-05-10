'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmSignatoriesMaintenance
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     December 7, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI For the Maintenance of Document Signatories
'Arguments/Parameters:  
'Files/Database Tables:  AM_SIGNATORIES
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description
'   December 7, 2011   Juan Carlo L. Panopio                Form initialization and design

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
'Imports LDAPLib
'Imports LDAPLogin

Public Class frmSignatoriesMaintenance
    Dim WBillHelper As WESMBillHelper

    Private Sub frmSignatoriesMaintenance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        Dim GetDocumentSig = WBillHelper.GetSignatories()
        Me.LoadTables(GetDocumentSig)
    End Sub


    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        frmSignatoriesMaintenanceMgt.UpdateRecord(DGV_ViewRecords.CurrentRow.Cells("Document Code").Value.ToString)
        frmSignatoriesMaintenanceMgt.Show()
    End Sub

    Private Sub cmd_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub

    Public Sub LoadTables(ByVal GetDocumentSig As List(Of DocSignatories))
        Dim dt As New DataTable

        With dt.Columns
            .Add("Document Code")
            .Add("Document Description")
            .Add("Signatory 1")
            .Add("Position 1")
            .Add("Signatory 2")
            .Add("Position 2")
            .Add("Signatory 3")
            .Add("Position 3")
            .Add("Updated By")
            .Add("Updated Date")
        End With

        For Each itmSignatory In GetDocumentSig
            Dim dr As DataRow
            dr = dt.NewRow

            With itmSignatory
                dr("Document Code") = .DocCode
                dr("Document Description") = .DocDescription
                dr("Signatory 1") = .Signatory_1
                dr("Position 1") = .Position_1
                dr("Signatory 2") = .Signatory_2
                dr("Position 2") = .Position_2
                dr("Signatory 3") = .Signatory_3
                dr("Position 3") = .Position_3
                dr("Updated By") = .UpdatedBy
                dr("Updated Date") = FormatDateTime(.UpdatedDate, DateFormat.ShortDate)
            End With

            dt.Rows.Add(dr)
            dt.AcceptChanges()
        Next

        DGV_ViewRecords.DataSource = dt
    End Sub

    Private Sub btn_Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Search.Click
        If txtSearch.Text = "" Or Len(Trim(txtSearch.Text)) = 0 Then
            MsgBox("Invalid Entry found for Search field", MsgBoxStyle.Exclamation, "Invalid")
        Else
            Dim GetList = WBillHelper.GetSignatories("%" & UCase(txtSearch.Text) & "%")
            If GetList.Count = 0 Then
                MsgBox("No records found.", MsgBoxStyle.Exclamation, "No records")                
                Exit Sub
            End If
            Me.LoadTables(GetList)            
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Dim GetDocumentSig = WBillHelper.GetSignatories()
        Me.LoadTables(GetDocumentSig)
        Me.txtSearch.Text = ""
    End Sub
End Class