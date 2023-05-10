'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmGLPrudentialPerParticipant
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     August 22, 2015
'Development Group:      Software Development and Support Division
'Description:            GUI for the generation of Deferred Payment
'Arguments/Parameters:  
'Files/Database Tables:  frmGLPrudentialPerParticipant
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib
Imports System.Text

Public Class frmSLPrudentialPerParticipant
    Private BFactory As BusinessFactory
    Private WBillHelper As WESMBillHelper
    Private GLedger As GeneralLedger
    Private dicParticipants As Dictionary(Of String, AMParticipants)

    Private Sub frmGLPrudentialPerParticipant_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Try
            BFactory = BusinessFactory.GetInstance()
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            dicParticipants = WBillHelper.GetDicAMParticipantsByParticipantID()

            Me.ddlParticipantID.Items.Clear()

            For Each item In dicParticipants.Keys
                Me.ddlParticipantID.Items.Add(item)
            Next
            Me.ddlParticipantID.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try        
    End Sub


    Private Sub cmd_Generate_Click(sender As Object, e As EventArgs) Handles cmd_Generate.Click
        Dim dt As New DSReport.GeneralLedgerPrudentialPerParticipantDataTable
        Dim result As New DataTable
        Dim listGLPrudentialPerParticipant As New List(Of GeneralLedgerPrudentialPerParticipant)
        Dim startDate As Date, endDate As Date, currentDate As Date

        Try
            If Me.ddlParticipantID.SelectedIndex = -1 Then
                MessageBox.Show("Specify the Participant ID!", "No input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            'Get the current date
            currentDate = CDate(WBillHelper.GetSystemDate().ToString("MM/dd/yyyy"))

            startDate = CDate(Me.dtp_From.Value.ToString("MM/dd/yyyy"))
            endDate = CDate(Me.dtp_To.Value.ToString("MM/dd/yyyy"))

            If endDate > currentDate Then
                MessageBox.Show("End Date is More Than Current Date!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If startDate > endDate Then
                MessageBox.Show("Invalid Date Range!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            'Beginning Balance
            listGLPrudentialPerParticipant.AddRange(WBillHelper.GeneralLedgerPrudentialPerParticipantBeginning(startDate, endDate, Me.txtIDNumber.Text))

            'PR Transactions
            listGLPrudentialPerParticipant.AddRange(WBillHelper.GetGeneralLedgerPrudentialPerParticipant(startDate, endDate, Me.txtIDNumber.Text))

            If listGLPrudentialPerParticipant.Count = 0 Then
                MessageBox.Show("No record found!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            'Generate the Datatable
            result = BFactory.GenerateGeneralLedgerPrudentialPerParticipant(startDate, endDate, listGLPrudentialPerParticipant, dt)

            Dim rptView As New frmReportViewer
            With rptView
                .LoadGLPrudentialPerParticipant(result)                
                .ShowDialog()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)        
        End Try
    End Sub

    Private Sub cmd_Close_Click(sender As Object, e As EventArgs) Handles cmd_Close.Click
        Me.Close()
    End Sub

    Private Sub ddlParticipantID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlParticipantID.SelectedIndexChanged
        Me.txtIDNumber.Text = dicParticipants(Me.ddlParticipantID.Text).IDNumber
        Me.txtName.Text = dicParticipants(Me.ddlParticipantID.Text).FullName
    End Sub

    Private Sub btn_ExportToCSV_Click(sender As Object, e As EventArgs) Handles btn_ExportToCSV.Click
        Dim dt As New DSReport.GeneralLedgerPrudentialPerParticipantDataTable
        Dim result As New DataTable
        Dim listGLPrudentialPerParticipant As New List(Of GeneralLedgerPrudentialPerParticipant)
        Dim startDate As Date, endDate As Date, currentDate As Date

        Try
            If Me.ddlParticipantID.SelectedIndex = -1 Then
                MessageBox.Show("Specify the Participant ID!", "No input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            'Get the current date
            currentDate = CDate(WBillHelper.GetSystemDate().ToString("MM/dd/yyyy"))

            startDate = CDate(Me.dtp_From.Value.ToString("MM/dd/yyyy"))
            endDate = CDate(Me.dtp_To.Value.ToString("MM/dd/yyyy"))

            If endDate > currentDate Then
                MessageBox.Show("End Date is More Than Current Date!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If startDate > endDate Then
                MessageBox.Show("Invalid Date Range!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim ans As New MsgBoxResult
            Dim _fldrSelect As New FolderBrowserDialog
            Dim fPathName As String = ""

            ans = MsgBox("Do you really want to export the records as CSV Format?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Export records")
            If ans <> MsgBoxResult.Yes Then
                Exit Sub
            End If


            With _fldrSelect
                .ShowDialog()
                If .SelectedPath = "" Then
                    Exit Sub
                End If
                fPathName = .SelectedPath & "\SL_Prudential_" & Me.txtIDNumber.Text & Format(startDate, "MMddyyyy") & "-" & Format(endDate, "MMddyyyy") & ".csv"
            End With

            'Beginning Balance
            listGLPrudentialPerParticipant.AddRange(WBillHelper.GeneralLedgerPrudentialPerParticipantBeginning(startDate, endDate, Me.txtIDNumber.Text))

            'PR Transactions
            listGLPrudentialPerParticipant.AddRange(WBillHelper.GetGeneralLedgerPrudentialPerParticipant(startDate, endDate, Me.txtIDNumber.Text))

            If listGLPrudentialPerParticipant.Count = 0 Then
                MessageBox.Show("No record found!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            'Generate the Datatable
            result = BFactory.GenerateGeneralLedgerPrudentialPerParticipant(startDate, endDate, listGLPrudentialPerParticipant, dt)

            If result.Rows.Count = 0 AndAlso dt Is Nothing Then
                ProgressThread.Close()
                MessageBox.Show("Nothing to generate!", "No data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Me.CreateCSV(result, fPathName)

            ProgressThread.Close()
            MessageBox.Show("Successfully exported records to " & fPathName, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CreateCSV(dt As DataTable, filename As String)

        Dim strBldr As New StringBuilder()
        'Header
        strBldr.Append("ID_NUMBER,JOURNAL_NUMBER, REFERENCE_NUMBER,TRANS_DATE,AMOUNT,CURRENT_AMOUNT,AMOUNT31TO60,AMOUNT61TO90,AMOUNT91OVER")
        strBldr.AppendLine()

        'Data
        Dim totalAmount As Decimal = 0

        For Each perIDNumber In (From x In dt.AsEnumerable() Select x).ToList
            strBldr.Append(perIDNumber.Field(Of String)("ID_NUMBER") & Chr(44))
            strBldr.Append(perIDNumber.Field(Of String)("JOURNAL_NUMBER") & Chr(44))
            strBldr.Append(perIDNumber.Field(Of String)("REFERENCE_NUMBER") & Chr(44))
            strBldr.Append(perIDNumber.Field(Of Date)("TRANS_DATE") & Chr(44))
            strBldr.Append(perIDNumber.Field(Of Decimal)("AMOUNT"))
            strBldr.AppendLine()
            totalAmount += perIDNumber.Field(Of Decimal)("AMOUNT")
            
        Next

        strBldr.Append("" & Chr(44))
        strBldr.Append("" & Chr(44))
        strBldr.Append("" & Chr(44))
        strBldr.Append("Ending Balance:" & Chr(44))
        strBldr.Append(totalAmount)        
        strBldr.AppendLine()

        Using fs As FileStream = File.Create(filename)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(strBldr.ToString)
            fs.Write(info, 0, info.Length)
        End Using
    End Sub
End Class