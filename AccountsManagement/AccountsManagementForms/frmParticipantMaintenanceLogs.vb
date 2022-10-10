'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmParticipantMaintenance
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     November 25, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI For the Maintenance of Parent - Child Relationship for finance to be used in their Offsetting
'Arguments/Parameters:  
'Files/Database Tables:  BILL_PARTICIPANTS, AM_PARENT_CHILD_MAPPING
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description
'   November 25, 2011   Juan Carlo L. Panopio                Form initialization and design
'   November 28, 2011   Juan Carlo L. Panopio                Finished functionalities for Set as Parent, Set as Child, Transfer Parent

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects


Public Class frmParticipantMaintenanceLogs
    Dim WBillHelper As WESMBillHelper
    Dim Admin As Boolean

    Private Sub frmParticipantMaintenanceLogs_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        'Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        'WBillHelper = WESMBillHelper.GetInstance()
        'WBillHelper.ConnectionString = AMModule.ConnectionString
        'WBillHelper.UserName = AMModule.UserName
    End Sub

    Public Sub LoadLogs(ByVal ParticipantIDNumber As String)
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        LBL_Participant.Text = ParticipantIDNumber.ToString
        Dim GetParticipants = WBillHelper.GetAMParticipantsAll()
        Dim GetParticipantLog = WBillHelper.GetParentChildMappingLogs(ParticipantIDNumber)

        For Each item In GetParticipantLog
            'Get Parent ID
            Dim _item = item
            Dim ParentID = (From x In GetParticipants _
                            Where x.IDNumber = _item.PCNumber _
                            Select x.ParticipantID).FirstOrDefault
            If item.Status <> 2 Then
                DGV_Logs.Rows.Add(item.BillPeriod, ParentID, item.Remarks, item.UpdatedDate, item.UpdatedBy)
            End If
        Next


        Me.Show()
    End Sub


    Private Sub CMD_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMD_Close.Click
        Me.Close()
    End Sub
End Class