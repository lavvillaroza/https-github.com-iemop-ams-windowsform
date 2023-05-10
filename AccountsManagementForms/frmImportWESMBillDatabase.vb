'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmImportWESMBillDatabase
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     September 20, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI for viewing of uploaded WESM Bills that were uploaded thru csv file or coming from WBSS database.
'                        It also generates the WESM Bill Report.
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   September 20, 2011      Vladimir E. Espiritu            GUI initialization and uploading of WESM Bill coming from WBSS Database
'   December 05, 2011       Vladimir E. Espiritu            Load billing period and settlement run that are ready for uploading
'   


Option Explicit On
Option Strict On

Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
'Imports LDAPLib
'Imports LDAPLogin

Public Class frmImportWESMBillDatabase

    Dim UtiImporter As ImporterUtility
    Dim WBillHelper As WESMBillHelper
    Dim dicEnergyBPs As Dictionary(Of Integer, List(Of String))
    Dim dicMFBPs As Dictionary(Of Integer, List(Of String))
    Dim dicFinalBPs As Dictionary(Of Integer, List(Of String))
    Dim ListGPPosted As List(Of WESMBillGPPosted)

    Private Sub frmImportWESMBillDatabase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm

        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"

        UtiImporter = ImporterUtility.GetInstance()
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        Me.LoadData()
        AddHandler ddlBillingPeriod.SelectedIndexChanged, AddressOf Me.ddlBillingPeriod_SelectedIndexChanged
        AddHandler rdEnergy.CheckedChanged, AddressOf Me.selectChargeType
        AddHandler rdMF.CheckedChanged, AddressOf Me.selectChargeType
        Me.rdEnergy.Checked = True
    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Dim ans As MsgBoxResult = MsgBoxResult.No
        Dim CountWESMBillExist As Integer = 0
        Dim BillingPeriod As Integer
        Dim SettlementRun As String
        Dim chargeTypeID As EnumChargeType

        Try
            If Me.ddlBillingPeriod.Items.Count = 0 Then
                MsgBox("No existing records", MsgBoxStyle.Information, "No data")
                Exit Sub
            ElseIf Me.ddlBillingPeriod.SelectedIndex = -1 Then
                MsgBox("Please specify first the billing period!", MsgBoxStyle.Critical, "Specify the inputs")
                Exit Sub
            ElseIf Me.ddlSTLRun.SelectedIndex = -1 Then
                MsgBox("Please specify first the settlement run!", MsgBoxStyle.Critical, "Specify the inputs")
                Exit Sub
            End If

            BillingPeriod = CInt(Me.ddlBillingPeriod.Text)
            SettlementRun = Me.ddlSTLRun.Text

            If Me.rdEnergy.Checked Then
                chargeTypeID = EnumChargeType.E
            Else
                chargeTypeID = EnumChargeType.MF
            End If
            'Get the list of WESM Bill
            Dim listWESMBill = WBillHelper.GetWESMBillFromWBSS(BillingPeriod, SettlementRun, chargeTypeID)

            'Check if there are existing data
            If listWESMBill.Count = 0 Then
                MsgBox("No record found!!", MsgBoxStyle.Information, "No data")
                Exit Sub
            End If

            CountWESMBillExist = WBillHelper.GetWESMBillCount(BillingPeriod, SettlementRun, chargeTypeID)
          
            If CountWESMBillExist <> 0 Then
                ans = MsgBox("There are already existing records, " & vbCrLf _
                           & "Replace existing?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")
            Else
                ans = MsgBox("Do you really want to save the records?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Upload")
            End If

            If ans = MsgBoxResult.No Then
                MsgBox("Transaction Cancelled", MsgBoxStyle.Information, "")
                Exit Sub
            End If

            'Save WESM Bills
            'WBillHelper.SaveWESMBill(BillingPeriod, SettlementRun, listWESMBill)

            MsgBox("Successfully Saved!", MsgBoxStyle.Information, "Done Uploading")

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ddlBillingPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.ddlBillingPeriod.SelectedIndex = -1 Then
            Exit Sub
        End If

        Me.ddlSTLRun.DataSource = dicFinalBPs(CInt(Me.ddlBillingPeriod.Text))

        If Me.ddlSTLRun.Items.Count = 0 Then
            Exit Sub
        ElseIf Me.ddlSTLRun.Items.Count = 1 Then
            Me.ddlSTLRun.SelectedIndex = 0
        Else
            Me.ddlSTLRun.SelectedIndex = -1
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub LoadBillingPeriod(ByVal chargeType As EnumChargeType)
        Dim dicBPs As New Dictionary(Of Integer, List(Of String))
        Dim dicBillBPs As Dictionary(Of Integer, List(Of String))

        dicFinalBPs = New Dictionary(Of Integer, List(Of String))

        If chargeType = EnumChargeType.E Or chargeType = EnumChargeType.EV Then
            Dim items = (From x In ListGPPosted _
                         Where (x.Charge = EnumChargeType.E Or x.Charge = EnumChargeType.EV) And x.Posted = 1 _
                         Select x.BillingPeriod, x.SettlementRun Distinct).ToList()

            For Each item In items
                With item
                    If dicBPs.ContainsKey(.BillingPeriod) Then
                        dicBPs(.BillingPeriod).Add(.SettlementRun)
                    Else
                        dicBPs.Add(.BillingPeriod, New List(Of String))
                        dicBPs(.BillingPeriod).Add(.SettlementRun)
                    End If
                End With
            Next

            dicBillBPs = dicEnergyBPs
        Else
            Dim items = (From x In ListGPPosted _
                         Where (x.Charge = EnumChargeType.MF Or x.Charge = EnumChargeType.MFV) And x.Posted = 1 _
                         Select x.BillingPeriod, x.SettlementRun Distinct).ToList()

            For Each item In items
                With item
                    If dicBPs.ContainsKey(.BillingPeriod) Then
                        dicBPs(.BillingPeriod).Add(.SettlementRun)
                    Else
                        dicBPs.Add(.BillingPeriod, New List(Of String))
                        dicBPs(.BillingPeriod).Add(.SettlementRun)
                    End If
                End With
            Next

            dicBillBPs = dicMFBPs
        End If

        For Each item In dicBillBPs
            If dicBPs.ContainsKey(item.Key) Then
                For Each stlrun In item.Value
                    If Not dicBillBPs(item.Key).Contains(stlrun) Then
                        If dicFinalBPs.ContainsKey(item.Key) Then
                            dicFinalBPs(item.Key).Add(stlrun)
                        Else
                            dicFinalBPs.Add(item.Key, New List(Of String))
                            dicFinalBPs(item.Key).Add(stlrun)
                        End If
                    End If
                Next
            Else
                dicFinalBPs.Add(item.Key, item.Value)
            End If
        Next

        Dim listBPs = (From x In dicFinalBPs _
                       Select x.Key Order By Key.ToString() Descending).ToList()

        With Me.ddlBillingPeriod
            .Items.Clear()
            .Items.Add("")
            For Each Item In listBPs
                .Items.Add(Item)
            Next

            If .Items.Count = 2 Then
                .SelectedIndex = 1
            Else
                .SelectedIndex = -1
            End If
        End With
       
    End Sub

    Private Sub selectChargeType(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.ddlSTLRun.DataSource = Nothing
        If rdEnergy.Checked Then
            Me.LoadBillingPeriod(EnumChargeType.E)
        Else
            Me.LoadBillingPeriod(EnumChargeType.MF)
        End If
    End Sub

    Private Sub LoadData()
        dicEnergyBPs = WBillHelper.GetWBSSBillingPeriod(EnumChargeType.E)
        dicMFBPs = WBillHelper.GetWBSSBillingPeriod(EnumChargeType.MF)
        ListGPPosted = WBillHelper.GetWESMBillGPPosted()
    End Sub
End Class