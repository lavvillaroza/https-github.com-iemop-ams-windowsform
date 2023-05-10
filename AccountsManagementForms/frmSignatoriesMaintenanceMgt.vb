'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmSignatoriesMaintenance
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     December 7, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI For updating the Maintenance of Document Signatories
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
Imports WESMLib.Auth.Lib

'Imports LDAPLib
'Imports LDAPLogin

Public Class frmSignatoriesMaintenanceMgt
    Dim WBillHelper As WESMBillHelper
    Dim Admin As Boolean

    Private Sub frmSignatoriesMaintenanceMgt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

    End Sub

    Public Sub UpdateRecord(ByVal DocumentCode As String)
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        LBL_DocumentContainer.Text = DocumentCode
        Dim GetDocumentSignatories = WBillHelper.GetSignatories()
        Dim DocSigUpdate = (From x In GetDocumentSignatories Where x.DocCode = DocumentCode).FirstOrDefault

        With DocSigUpdate
            TXT_description.Text = .DocDescription

            txt_s1.Text = .Signatory_1
            txt_s2.Text = .Signatory_2
            txt_s3.Text = .Signatory_3

            txt_p1.Text = .Position_1
            txt_p2.Text = .Position_2
            txt_p3.Text = .Position_3
        End With
    End Sub

    Private Sub CMD_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMD_Ok.Click
        Try


            Dim ForSaveSignatory As New DocSignatories
            With ForSaveSignatory
                .Signatory_1 = txt_s1.Text
                .Position_1 = txt_p1.Text

                .Signatory_2 = txt_s2.Text
                .Position_2 = txt_p2.Text

                .Signatory_3 = txt_s3.Text
                .Position_3 = txt_p3.Text

                .DocCode = Me.LBL_DocumentContainer.Text
                .DocDescription = Me.TXT_description.Text
            End With
            WBillHelper.SaveSignatories(ForSaveSignatory)
            MsgBox("Successfully updated!", MsgBoxStyle.Information, "Success!")

            'Updated By Lance 08/24/2014

            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibSignatoriesMainteWindow.ToString, "Updated Record " & Me.LBL_DocumentContainer.Text & ".", "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccessfullyEdited.ToString, AMModule.UserName)
            Me.Close()

            Dim GetDocumentSignatories = WBillHelper.GetSignatories()
            frmSignatoriesMaintenance.LoadTables(GetDocumentSignatories)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")            
        End Try
    End Sub

    Private Sub CMD_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMD_Cancel.Click
        Me.Close()
    End Sub

    Private Sub DisableKey(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TXT_description.KeyDown, txt_p1.KeyDown, txt_p2.KeyDown, txt_p3.KeyDown, txt_s1.KeyDown, txt_s2.KeyDown, txt_s3.KeyDown
        'MsgBox(e.KeyValue)
        Select Case e.KeyValue
            Case 222
                e.SuppressKeyPress = True
            Case Else
        End Select
    End Sub


End Class