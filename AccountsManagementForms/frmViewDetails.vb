'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmViewErrors
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     December 13, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI for viewing of Errors
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   December 13, 2011       Vladimir E. Espiritu            GUI initialization
'


Option Explicit On
Option Strict On

Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmViewDetails
    Private WBillHelper As WESMBillHelper
    Private xlHandler As ExcelHandler
    Private _PaymentGroupCode As String
    Private _DetailsList As List(Of PaymentAllocationAccount)
    Private _HeaderList As List(Of PaymentAllocationParticipant)
    Private _ParticipantsList As List(Of AMParticipants)
    Private _DMCMHeaders As List(Of DebitCreditMemo)
    Private _isFinal As Boolean


    Public Sub ShowParticipantsForEFT(ByVal dt As DataTable)
        Me.Text = "Participants for EFT"
        With Me.dgridView
            .DataSource = dt
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .EditMode = DataGridViewEditMode.EditProgrammatically
            .Columns(0).HeaderText = "RecordNumber"
            .Columns(1).HeaderText = "Payee/BeneficiaryName (max of 35 characters)"
            .Columns(2).HeaderText = "Payee/BeneficiaryAccountNo."
            .Columns(3).HeaderText = "BeneficiaryBankClearingCode"
            .Columns(4).HeaderText = "InvoiceNo"
            .Columns(5).HeaderText = "InvoiceDate"
            .Columns(6).HeaderText = "PaymentDate"
            .Columns(7).HeaderText = "Amount"
            .Columns(8).HeaderText = "PaymentDetailsI (max of 35 characters)"
            .Columns(9).HeaderText = "PaymentDetailsIV (max of 35 characters)"
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(7).DefaultCellStyle.Format = "n2"
        End With

        Me.cmd_ToCSV.Visible = True
    End Sub


    Public Sub ShowUnmappedParticipants(ByVal listParticipants As List(Of String))
        Me.Text = "Unmapped Participants"

        Dim dt As New DataTable
        dt.Columns.Add("ParticipantID", GetType(String))
        dt.AcceptChanges()

        For Each item In listParticipants
            Dim row = dt.NewRow()
            row("ParticipantID") = item
            dt.Rows.Add(row)
        Next
        dt.AcceptChanges()

        With Me.dgridView
            .DataSource = dt
            .Columns(0).HeaderText = "Participant ID"
            .Columns(0).Width = 300
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .EditMode = DataGridViewEditMode.EditProgrammatically
        End With

    End Sub

    Public Sub ShowNotExistingParticipants(ByVal listParticipants As List(Of String))
        Me.Text = "Not Existing Participants/In-active Participants"

        Dim dt As New DataTable
        dt.Columns.Add("IDNumber", GetType(String))
        dt.AcceptChanges()

        For Each item In listParticipants
            Dim row = dt.NewRow()
            row("IDNumber") = item
            dt.Rows.Add(row)
        Next
        dt.AcceptChanges()

        With Me.dgridView
            .DataSource = dt
            .Columns(0).HeaderText = "IDNumber"
            .Columns(0).Width = 300
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .EditMode = DataGridViewEditMode.EditProgrammatically
        End With

    End Sub

    Public Sub ShowListOfErrorsForUploadingInCRSSDB(ByVal listofErrors As List(Of String))
        Me.Text = "Error/s found in CRSS DB"

        Dim dt As New DataTable
        dt.Columns.Add("Remarks", GetType(String))
        dt.AcceptChanges()

        For Each item In listofErrors
            Dim row = dt.NewRow()
            row("Remarks") = item
            dt.Rows.Add(row)
        Next
        dt.AcceptChanges()

        With Me.dgridView
            .DataSource = dt
            .Columns(0).HeaderText = "Remarks"
            .Columns(0).Width = 800
            .RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .EditMode = DataGridViewEditMode.EditProgrammatically
        End With

    End Sub

    Public Sub ShowInActiveParticipants(ByVal listParticipants As List(Of String))
        Me.Text = "In-active Participants"

        Dim dt As New DataTable
        dt.Columns.Add("ParticipantID", GetType(String))
        dt.AcceptChanges()

        For Each item In listParticipants
            Dim row = dt.NewRow()
            row("ParticipantID") = item
            dt.Rows.Add(row)
        Next
        dt.AcceptChanges()

        With Me.dgridView
            .DataSource = dt
            .Columns(0).HeaderText = "Participant ID"
            .Columns(0).Width = 300
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .EditMode = DataGridViewEditMode.EditProgrammatically
        End With

    End Sub


    Public Sub ShowWESMBill(ByVal dt As DataTable)
        Me.Text = "WESM Bill"
        With Me.dgridView
            .DataSource = dt
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .EditMode = DataGridViewEditMode.EditProgrammatically
            .Columns(0).HeaderText = "IDNumber"
            .Columns(1).HeaderText = "ParticipantID"
            .Columns(2).HeaderText = "InvoiceNo"
            .Columns(3).HeaderText = "ChargeType"
            .Columns(4).HeaderText = "InvoiceDate"
            .Columns(5).HeaderText = "Amount"
        End With

    End Sub


    Public Sub ShowCollectionDetails(ByVal dt As DataTable, ByVal tEnergyCollection As Decimal, ByVal tVATCollection As Decimal, ByVal tDefaultCollection As Decimal)
        Me.Text = "Collection Details"
        With Me.dgridView
            .DataSource = dt
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .EditMode = DataGridViewEditMode.EditProgrammatically
            .Columns(0).HeaderText = "Collection Date"
            .Columns(1).HeaderText = "Billing Period"
            .Columns(2).HeaderText = "Invoice/DMCM No"
            .Columns(3).HeaderText = "ORNo."
            .Columns(4).HeaderText = "Collection Category"
            .Columns(5).HeaderText = "Collection Type"
            .Columns(6).HeaderText = "Allocated Amount"
            .Columns(6).DefaultCellStyle.Format = "n2"
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        For x As Integer = 0 To Me.dgridView.Columns.Count - 1
            Me.dgridView.Columns(x).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        Next

        txt_tEnergyCollection.Text = tEnergyCollection.ToString("#,##0.00")
        Me.Label7.Text = "Total VAT Collection"
        txt_tDICollection.Text = tDefaultCollection.ToString("#,##0.00")

        txt_tVATCollection.Text = tVATCollection.ToString("#,##0.00")

        Label1.Visible = True
        txt_tDICollection.Visible = True

        Dim newSize As New System.Drawing.Size
        newSize.Height = 497
        newSize.Width = 703

        Me.dgridView.Size = newSize
    End Sub

    Public Sub ShowPaymentDetails(ByVal PaymentAccount As List(Of PaymentAllocationAccount))
        Dim dt As New DataTable
        With dt.Columns
            .Add("Bill Period")
            .Add("Invoice/DMCM number")
            .Add("Charge Type")
            .Add("Payment Type")
            .Add("Beginning Balance")
            .Add("Payment Amount")
            .Add("Ending Balance")
            .Add("DMCM Number")
        End With

        For Each item In PaymentAccount
            Dim dr As DataRow
            dr = dt.NewRow
            With item
                dr("Bill Period") = .WESMBillSummary.BillPeriod
                dr("Charge Type") = .WESMBillSummary.ChargeType.ToString
                dr("Invoice/DMCM number") = CStr(IIf(.WESMBillSummary.SummaryType = EnumSummaryType.DMCM, "DMCM-", "INV-")) & .WESMBillSummary.INVDMCMNo
                dr("Payment Type") = .PaymentType.ToString
                dr("Beginning Balance") = .BeginningBalance.ToString("#,##0.00")
                dr("Payment Amount") = .PaymentAmount.ToString("#,##0.00")
                dr("Ending Balance") = .EndingBalance.ToString("#,##0.00")
                dr("DMCM Number") = .DMCMNo
            End With
            dt.Rows.Add(dr)
            dt.AcceptChanges()
        Next


        With Me.dgridView
            .DataSource = dt
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .EditMode = DataGridViewEditMode.EditProgrammatically

            .Columns(4).DefaultCellStyle.Format = "n2"
            .Columns(5).DefaultCellStyle.Format = "n2"
            .Columns(6).DefaultCellStyle.Format = "n2"

            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        Me.dgridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        Me.cmd_ToCSV.Visible = True
        'txt_tEnergyCollection.Text = EnergyPayment.ToString("#,##0.00")
        'txt_tVATCollection.Text = VATPayment.ToString("#,##0.00")

        'Label1.Visible = False
        'Label6.Text = "Total Energy Allocated: "
        'Label7.Text = "Total VAT Allocated: "
        'txt_tDICollection.Visible = False

        'If AmountDistributed = True Then
        '    cmd_viewDetails.Visible = True
        '    _DetailsList = ParticipantDetails
        '    _HeaderList = PaymentHeader
        '    _ParticipantsList = Participants
        'End If


        Dim newSize As New System.Drawing.Size
        newSize.Height = 497
        newSize.Width = 661

        Me.dgridView.Size = newSize
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Public Sub ShowSummaryDetails(ByVal dt As DataTable)
        Me.Text = "WESM Bill Summary Details"
        With Me.dgridView
            .DataSource = dt
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .EditMode = DataGridViewEditMode.EditProgrammatically
            .Columns(0).HeaderText = "BillingPeriod"
            .Columns(1).HeaderText = "ParticipantID"
            .Columns(2).HeaderText = "ParticipantName"
            .Columns(3).HeaderText = "InvoiceNumber"
            .Columns(4).HeaderText = "InvoiceDate"
            .Columns(5).HeaderText = "DueDate"
            .Columns(6).HeaderText = "Amount"

            .Columns(5).DefaultCellStyle.Format = "d"

            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(6).DefaultCellStyle.Format = "n2"
        End With
    End Sub

    Public Sub ViewParticipantDetails(ByVal dt As DataTable)
        Me.Text = "Participant Details"
        With Me.dgridView
            .DataSource = dt
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .EditMode = DataGridViewEditMode.EditProgrammatically
            .Columns(0).HeaderText = "BillPeriod"
            .Columns(1).HeaderText = "IdNumber"
            .Columns(2).HeaderText = "ParticipantID"
            .Columns(3).HeaderText = "Remarks"
            .Columns(4).HeaderText = "Status"
        End With
    End Sub

    Public Sub ViewAllocatedAmount(ByVal dt As DataTable)
        Me.Text = "Amount Allocated for Participant"
        With Me.dgridView
            .DataSource = dt
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .EditMode = DataGridViewEditMode.EditProgrammatically
            .Columns(0).HeaderText = "IDNumber"
            .Columns(1).HeaderText = "ParticipantID"
            .Columns(2).HeaderText = "AllocatedAmount"
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(2).DefaultCellStyle.Format = "n2"
        End With
    End Sub

    Private Sub frmViewDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Me.MdiParent = MainForm
    End Sub

    Private Sub cmd_viewDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_viewDetails.Click
        Dim dt As New DataTable
        With dt.Columns
            .Add("hIDNumber", GetType(Integer))
            .Add("hParticipantID", GetType(String))
            .Add("hAllocAmount", GetType(Decimal))
        End With


        For Each item In _DetailsList
            Dim cGroupCode As Long
            Dim IDNo As String
            Dim dr As DataRow

            cGroupCode = item.PaymentGroupCode
            IDNo = CStr((From x In _HeaderList _
                        Where _
                        x.PaymentGroupCode = cGroupCode _
                        Select x.Participant.IDNumber).FirstOrDefault)

            Dim gParticipantID = (From x In _ParticipantsList _
                                  Where x.IDNumber = IDNo _
                                  Select x.ParticipantID).FirstOrDefault


            dr = dt.NewRow
            With item
                dr(0) = IDNo
                dr(1) = gParticipantID
                dr(2) = item.PaymentAmount
            End With
            dt.Rows.Add(dr)
        Next
        dt.AcceptChanges()

        Dim a As New frmViewDetails
        With a
            .ViewAllocatedAmount(dt)
            .ShowDialog()
        End With
    End Sub

    Private Sub cmd_ToCSV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ToCSV.Click
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString

        Dim saveFileDialogBox As New SaveFileDialog

        With saveFileDialogBox
            .Title = "Save File As"
            .Filter = "CSV Files (*.csv)|*.csv"
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                WBillHelper.subExportDGVToCSV(.FileName, Me.dgridView, True)
                MsgBox("Successfully Downloaded!", MsgBoxStyle.Information, "Done")
            End If
        End With

    End Sub

    Public Sub _tmpTestViewDMCM(ByVal ForViewing As List(Of DebitCreditMemo), ByVal AllParticipants As List(Of AMParticipants))
        _DMCMHeaders = ForViewing

        Dim dicParticipantToName As New Dictionary(Of String, String)
        dicParticipantToName.Add("0", "0")

        For Each item In AllParticipants
            dicParticipantToName.Add(item.IDNumber, item.ParticipantID)
        Next


        With Me.dgridView.Columns
            .Add("IDNumber", "IDNumber")
            .Add("ParticipantName", "ParticipantName")
            .Add("BillPeriod", "BillPeriod")
            .Add("DueDate", "DueDate")
            .Add("Debit", "Debit")
            .Add("Credit", "Credit")
        End With

        For Each item In ForViewing
            Dim DebitAmt As Decimal
            Dim CreditAmt As Decimal
            For Each rec In item.DMCMDetails
                If rec.Credit = 0 Then
                    DebitAmt = rec.Debit
                Else
                    CreditAmt = rec.Credit
                End If
            Next
            With item
                Me.dgridView.Rows.Add(.IDNumber, dicParticipantToName(.IDNumber), .BillingPeriod, _
                                      .DueDate, debitamt, creditamt)
            End With

        Next

        Me.dgridView.AllowUserToAddRows = False
        Me.dgridView.AllowUserToDeleteRows = False
        Me.dgridView.EditMode = DataGridViewEditMode.EditProgrammatically

        Me.dgridView.Columns("Debit").DefaultCellStyle.Format = "n2"
        Me.dgridView.Columns("Credit").DefaultCellStyle.Format = "n2"

        Me.dgridView.Columns("Debit").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.dgridView.Columns("Credit").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        Me.dgridView.Columns("DueDate").DefaultCellStyle.Format = "d"

    End Sub

    Public Sub ViewColPaymentSummary(ByVal isFinal As Boolean)
        _isFinal = isFinal
        Me.dgridView.EditMode = DataGridViewEditMode.EditProgrammatically
        Me.dgridView.AllowUserToAddRows = False
        Me.dgridView.AllowUserToDeleteRows = False
        Me.dgridView.AllowUserToOrderColumns = False
        Me.dgridView.Columns(1).Frozen = True
        Me.Show()
    End Sub


    Private Sub cmd_ExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ExportToExcel.Click
        Dim ds As New DataSet
        Dim dt As New DataTable
        Try

            Dim sFilePath As String = ""
            ' xlHandler = ExcelHandler.GetInstance()
            WBillHelper = WESMBillHelper.GetInstance()




            Dim sfDialog As New FolderBrowserDialog
            With sfDialog
                .ShowNewFolderButton = True
                .RootFolder = Environment.SpecialFolder.Desktop
                .ShowDialog()
                If _isFinal = True Then

                    If .SelectedPath.Trim.Length = 0 Then
                        MsgBox("Export Cancelled", MsgBoxStyle.Critical, "Error!")
                        Exit Sub
                    End If

                    Dim Signatories = WBillHelper.GetSignatories("S_COLPAYMENT").FirstOrDefault
                    'Add Signatories

                    dt = CType(Me.dgridView.DataSource, DataTable)
                    ds.Tables.Add(dt)

                    ds.Tables(0).Rows.Add(ds.Tables(0).NewRow)
                    ds.Tables(0).Rows.Add(ds.Tables(0).NewRow)
                    ds.Tables(0).AcceptChanges()

                    Dim dr As DataRow
                    dr = ds.Tables(0).NewRow

                    dr(2) = "Prepared By:"
                    dr(5) = "Checked By:"
                    dr(8) = "Approved By:"

                    ds.Tables(0).Rows.Add(dr)
                    ds.Tables(0).AcceptChanges()

                    dr = ds.Tables(0).NewRow

                    dr(2) = WBillHelper.UserName
                    dr(5) = Signatories.Signatory_1
                    dr(8) = Signatories.Signatory_2

                    ds.Tables(0).Rows.Add(dr)
                    ds.Tables(0).AcceptChanges()

                    dr = ds.Tables(0).NewRow

                    dr(5) = Signatories.Position_1
                    dr(8) = Signatories.Position_2

                    ds.Tables(0).Rows.Add(dr)
                    ds.Tables(0).AcceptChanges()

                    sFilePath = .SelectedPath & "\Summary of Collection and Payments(Final) " & Replace(FormatDateTime(Now, DateFormat.ShortDate), "/", "") & ".csv"
                Else
                    If .SelectedPath.Trim.Length = 0 Then
                        MsgBox("Export Cancelled", MsgBoxStyle.Critical, "Error!")
                        Exit Sub
                    End If

                    dt = CType(Me.dgridView.DataSource, DataTable)
                    ds.Tables.Add(dt)

                    sFilePath = .SelectedPath & "\Summary of Collection and Payments(Initial) " & Replace(FormatDateTime(Now, DateFormat.ShortDate), "/", "") & ".csv"
                End If

            End With

            Me.WBillHelper.DataTable2CSV(Me.WBillHelper.BFactory.RemoveCommaForCSVExport(ds.Tables(0)), sFilePath)
            ds.Clear()
            'If xlHandler.ExportToExcel(sFilePath, ds, "Summary of Collection and Payments", "", ds.Tables(0).Columns.Count) = True Then
            MsgBox("Successfully completed generation of Summary of Collection and Payments", MsgBoxStyle.Information, Me.Name)
            Me.Close()
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error!")
            Exit Sub
        Finally
            ds = New DataSet
            dt = New DataTable
        End Try
    End Sub

End Class