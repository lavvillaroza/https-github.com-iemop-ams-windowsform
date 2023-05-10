'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmGreatPlainsDetails
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     December 8, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI for Viewing of details of the Journal Voucher
'Arguments/Parameters:   -
'Files/Database Tables:  -
'Return Value:           -
'Error codes/Exceptions: -
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description
'   December 14, 2011       Juan Carlo L. Panopio           Added Download to CSV Button and to export
'                                                           the data in DataGridView to a CSV File.
Option Strict On
Option Explicit On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Public Class frmGreatPlainsDetails
    Dim WBillHelper As WESMBillHelper
    Private _State As String
    Private Property State() As String
        Get
            Return _State
        End Get
        Set(ByVal value As String)
            Me._State = value
        End Set
    End Property
    Public Sub LoadReportUploaded(ByVal WESMBillDetails As List(Of WESMBill))
        'Me.MdiParent = MainForm
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = "JCLP"
        DGV_Details.Rows.Clear()
        DGV_Details.Columns.Clear()

        'Get All necessary Tables
        Dim GetJVHeader = WBillHelper.GetJournalVoucher()
        Dim GetPosting = WBillHelper.GetWESMBillGPPosted()
        Dim AllParticipants = WBillHelper.GetAMParticipantsAll()

        'Get All Details Needed
        Dim BatchNo = (From x In WESMBillDetails Select x.BatchCode).FirstOrDefault.ToString
        Dim GetJVNo As String = (From x In GetJVHeader Where x.BatchCode = BatchNo Select x.JVNumber).FirstOrDefault.ToString
        Dim BillPeriod As String = (From x In WESMBillDetails Select x.BillingPeriod).FirstOrDefault.ToString
        Dim STLRun As String = (From x In WESMBillDetails Select x.SettlementRun).FirstOrDefault.ToString
        Dim GetGPNo As String = (From x In GetPosting Where x.BatchCode = BatchNo Select x.GPRefNo).FirstOrDefault.ToString
        Dim GetCharge As EnumChargeType = (From x In WESMBillDetails Select x.ChargeType).FirstOrDefault

        'Create Viewing List
        Dim ForViewing = (From x In WESMBillDetails Join y In AllParticipants On x.IDNumber Equals y.IDNumber _
                          Select x.IDNumber, y.ParticipantID, x.InvoiceNumber, x.InvoiceDate, _
                          x.DueDate, x.Amount Order By InvoiceNumber Ascending, ParticipantID Ascending).ToList

        Dim CalendarBP = WBillHelper.GetCalendarBP(CInt(BillPeriod))
        Dim CalStart = (From x In CalendarBP Select x.StartDate)
        Dim CalEnd = (From x In CalendarBP Select x.EndDate)

        LBL_Batch.Text = BatchNo
        LBL_BPeriod.Text = BillPeriod & " (" & CalStart.FirstOrDefault.ToString("MM/dd/yyyy") & " - " & CalEnd.FirstOrDefault.ToString("MM/dd/yyyy") & ")"
        If GetCharge = EnumChargeType.E Then
            LBL_Charge.Text = "ENERGY"
        ElseIf GetCharge = EnumChargeType.EV Then
            LBL_Charge.Text = "VAT ON ENERGY"
        ElseIf GetCharge = EnumChargeType.MF Then
            LBL_Charge.Text = "MARKET FEES"
        ElseIf GetCharge = EnumChargeType.MFV Then
            LBL_Charge.Text = "VAT ON MARKET FEES"
        End If

        If Len(Trim(GetGPNo.ToString)) = 0 Then
            LBL_GPRef.Text = "No GP Reference No. set"
        Else
            LBL_GPRef.Text = GetGPNo
        End If

        LBL_JVNo.Text = GetJVNo
        LBL_STLRun.Text = STLRun

        With DGV_Details.Columns
            .Add("IDNo", "ID Number")
            .Add("ParticipantID", "Participant ID")
            .Add("InvNo", "Invoice No")
            .Add("InvDate", "Invoice Date")
            .Add("DueDate", "Due Date")
            .Add("ARAmt", "AR")
            .Add("APAmt", "AP")
        End With

        Dim dicChargeType As New Dictionary(Of EnumChargeType, String)
        dicChargeType.Add(EnumChargeType.E, "ENERGY")
        dicChargeType.Add(EnumChargeType.EV, "VAT ON ENERGY")
        dicChargeType.Add(EnumChargeType.MF, "MARKET FEES")
        dicChargeType.Add(EnumChargeType.MFV, "VAT ON MF")

        DGV_Details.Columns(5).DefaultCellStyle.Format = "n2"
        DGV_Details.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DGV_Details.Columns(6).DefaultCellStyle.Format = "n2"
        DGV_Details.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        For Each item In ForViewing
            With item
                DGV_Details.Rows.Add(.IDNumber, .ParticipantID, .InvoiceNumber, .InvoiceDate.ToString("MM/dd/yyyy"), _
                                     .DueDate.ToString("MM/dd/yyyy"), IIf(.Amount < 0, .Amount, CDec("0.00")), IIf(.Amount > 0, .Amount, CDec("0.00")))
            End With
        Next

        Dim APTotal = (From x In WESMBillDetails Where x.Amount > 0 _
                       Select Math.Abs(x.Amount)).Sum()
        Dim ARTotal = (From x In WESMBillDetails Where x.Amount < 0 _
                       Select Math.Abs(x.Amount)).Sum()

        Dim NSSTotal = Math.Abs(APTotal - ARTotal)
        If APTotal = 0 Or ARTotal = 0 Then
            NSSTotal = 0
        End If

        TXT_AP.Text = APTotal.ToString("#,##0.#0")
        TXT_AR.Text = ARTotal.ToString("#,##0.#0")
        TXT_TotalNSS.Text = NSSTotal.ToString("#,##0.#0")

        Me.Label5.Text = "Total AP"
        Me.Label8.Text = "Total AR"
        Me.Label9.Visible = True
        TXT_TotalNSS.Visible = True

        Me.State = "UPLOADED"
    End Sub

    Public Sub LoadReportOffset(ByVal OffsettingDetails As List(Of JournalVoucherSupport), ByVal BillPeriod As String, _
                                ByVal BatchCode As String, ByVal ChargeType As String)
        ' Me.MdiParent = MainForm
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = "JCLP"

        DGV_Details.Rows.Clear()
        DGV_Details.Columns.Clear()

        Dim GetJVHeader = WBillHelper.GetJournalVoucher(BatchCode).FirstOrDefault
        Dim GetGPDetails = WBillHelper.GetWESMBillGPPosted(BatchCode).FirstOrDefault

        Me.DGV_Details.DataSource = OffsettingDetails
        Me.DGV_Details.Columns(DGV_Details.Columns.Count - 1).DefaultCellStyle.Format = "n2"
        Me.DGV_Details.Columns(DGV_Details.Columns.Count - 1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.DGV_Details.Columns(DGV_Details.Columns.Count - 2).DefaultCellStyle.Format = "n2"
        Me.DGV_Details.Columns(DGV_Details.Columns.Count - 2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.LBL_JVNo.Text = CStr(GetJVHeader.JVNumber)
        Me.LBL_BPeriod.Text = BillPeriod
        Me.LBL_GPRef.Text = GetGPDetails.GPRefNo
        Me.LBL_Batch.Text = BatchCode
        Me.LBL_Charge.Text = ChargeType
        Me.LBL_STLRun.Text = ""

        Me.TXT_AP.Text = (From x In OffsettingDetails _
                          Select x.APAmount).Sum.ToString("#,##0.00")
        Me.TXT_AR.Text = (From x In OffsettingDetails _
                          Select x.ARAmount).Sum.ToString("#,##0.00")

        Me.Label5.Text = "Total AR"
        Me.Label8.Text = "Total AP"
        Me.Label9.Visible = False
        TXT_TotalNSS.Visible = False
        Me.State = "OFFSETTING"
    End Sub

    Public Sub LoadReportAdjustment(ByVal AdjustedBills As List(Of WESMBillSummary), ByVal BillPeriod As Integer, _
                                    ByVal BatchNo As String)
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = "JCLP"

        With DGV_Details
            .Columns.Add("hIDNumber", "IDNumber")
            .Columns.Add("hParticipantID", "ParticipantID")
            .Columns.Add("hChargeType", "ChargeType")
            .Columns.Add("hDuedate", "DueDate")
            .Columns.Add("hBeginningBal", "BeginningBalance")
            .Columns.Add("hEndingBalance", "EndingBalance")

            .Columns("hBeginningBal").DefaultCellStyle.Format = "n2"
            .Columns("hBeginningBal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("hEndingBalance").DefaultCellStyle.Format = "n2"
            .Columns("hEndingBalance").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        Dim GetJVHeader As New List(Of JournalVoucher)
        Dim GetPosting As New List(Of WESMBillGPPosted)

        GetJVHeader = WBillHelper.GetJournalVoucher()
        GetPosting = WBillHelper.GetWESMBillGPPosted()

        Dim CalendarBP = WBillHelper.GetCalendarBP(CInt(BillPeriod))
        Dim CalStart = (From x In CalendarBP Select x.StartDate)
        Dim CalEnd = (From x In CalendarBP Select x.EndDate)
        Dim GetCharge = (From x In AdjustedBills Select x.ChargeType).FirstOrDefault

        Dim GetGPNo = (From x In GetPosting _
                       Where x.BatchCode = BatchNo _
                       Select x.GPRefNo).FirstOrDefault.ToString

        LBL_Batch.Text = BatchNo
        LBL_BPeriod.Text = BillPeriod & " (" & CalStart.FirstOrDefault.ToString("MM/dd/yyyy") & " - " & CalEnd.FirstOrDefault.ToString("MM/dd/yyyy") & ")"
        Dim JVNo = (From x In GetJVHeader _
                        Where x.BatchCode = BatchNo _
                        Select x.JVNumber).FirstOrDefault.ToString

        If GetCharge = EnumChargeType.E Then
            LBL_Charge.Text = "ENERGY"
        ElseIf GetCharge = EnumChargeType.EV Then
            LBL_Charge.Text = "VAT ON ENERGY"
        ElseIf GetCharge = EnumChargeType.MF Then
            LBL_Charge.Text = "MARKET FEES"
        ElseIf GetCharge = EnumChargeType.MFV Then
            LBL_Charge.Text = "VAT ON MARKET FEES"
        End If

        If Len(Trim(GetGPNo.ToString)) = 0 Then
            LBL_GPRef.Text = "No GP Reference No. set"
        Else
            LBL_GPRef.Text = GetGPNo
        End If
        LBL_JVNo.Text = JVNo
        LBL_STLRun.Text = "From Adjustment"

        Dim APTotal = (From x In AdjustedBills Where x.EndingBalance > 0 _
               Select Math.Abs(x.EndingBalance)).Sum()
        Dim ARTotal = (From x In AdjustedBills Where x.EndingBalance < 0 _
                       Select Math.Abs(x.EndingBalance)).Sum()

        Dim NSSTotal = Math.Abs(APTotal - ARTotal)
        If APTotal = 0 Or ARTotal = 0 Then
            NSSTotal = 0
        End If

        TXT_AP.Text = APTotal.ToString("#,##0.#0")
        TXT_AR.Text = ARTotal.ToString("#,##0.#0")
        TXT_TotalNSS.Text = NSSTotal.ToString("#,##0.#0")

        For Each item In AdjustedBills
            With item
                DGV_Details.Rows.Add(.IDNumber.IDNumber, .IDNumber.ParticipantID, .ChargeType, _
                                     .NewDueDate, .BeginningBalance, .EndingBalance)
            End With
        Next

        Me.State = "ADJUSTMENT"
    End Sub

    Public Sub LoadReportCollection(ByVal AdjustedBills As List(Of CollectionAllocation))
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = "JCLP"

        Dim WBillParticipants = WBillHelper.GetAMParticipantsAll()
        Dim WBillSummary = WBillHelper.GetWESMBillSummary()
        Dim WBillGP = WBillHelper.GetWESMBillGPPosted()
        Dim WBillJV = WBillHelper.GetJournalVoucher()
        With DGV_Details
            .Columns.Add("hBillPeriod", "BillingPeriod")
            .Columns.Add("hIDNumber", "IDNumber")
            .Columns.Add("hParticipantID", "ParticipantID")
            .Columns.Add("hAmount", "Amount")
            .Columns.Add("hDueDate", "DueDate")
            .Columns.Add("hAllocationDate", "Allocationdate")
            .Columns.Add("hNewDueDate", "NewDueDate")

            .Columns("hAmount").DefaultCellStyle.Format = "n2"
            .Columns("hAmount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        Dim cBatchCode As String = AdjustedBills.First.BatchCode

        Me.LBL_Batch.Text = cBatchCode

        Dim GPDetails = (From x In WBillGP _
                         Where _
                         x.BatchCode = cBatchCode _
                         Select x).FirstOrDefault

        Dim JVdetails = (From x In WBillJV _
                         Where _
                         x.BatchCode = cBatchCode _
                         Select x).FirstOrDefault


        Me.LBL_Charge.Text = "Collection Summary"

        Me.LBL_JVNo.Text = JVdetails.JVNumber.ToString
        Me.LBL_BPeriod.Text = ""
        Me.LBL_STLRun.Text = ""

        If Len(Trim(GPDetails.GPRefNo)) = 0 Then
            Me.LBL_GPRef.Text = "No GP Reference No. set"
        Else
            Me.LBL_GPRef.Text = GPDetails.GPRefNo

        End If
        Me.LBL_STLRun.Text = "From Collection"

        'For Each item In AdjustedBills
        '    Dim cParticipant = item.GroupNo
        '    Dim cSummary = (From x In WBillSummary _
        '                    Where x.GroupNo = cParticipant _
        '                    Select x.IDNumber).FirstOrDefault

        '    With item
        '        DGV_Details.Rows.Add(.BillingPeriod, cSummary.IDNumber, cSummary.ParticipantID, _
        '                             .Amount, .DueDate, .AllocationDate, .NewDueDate)
        '    End With
        'Next
        Me.State = "COLLECTION"
    End Sub

    Public Sub LoadReportPayment(ByVal ParticipantPayment As List(Of PaymentAllocationParticipant))
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = "JCLP"

        Dim WBillParticipants = WBillHelper.GetAMParticipantsAll()

        With DGV_Details
            .Columns.Add("hBillPeriod", "BillingPeriod")
            .Columns.Add("hIDNumber", "IDNumber")
            .Columns.Add("hParticipantID", "ParticipantID")
            .Columns.Add("hAllocatedEnergy", "AllocatedEnergy")
            .Columns.Add("hAllocatedVAT", "AllocatedVAT")
            .Columns.Add("hAllocatedDefault", "AllocatedDefault")
            .Columns.Add("hDefEnergy", "DeferredEnergy")
            .Columns.Add("hDefVAT", "DeferredVAT")
            .Columns.Add("hAllocatedTotal", "TotalAllocated")

            .Columns("hAllocatedEnergy").DefaultCellStyle.Format = "n2"
            .Columns("hAllocatedEnergy").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("hAllocatedVAT").DefaultCellStyle.Format = "n2"
            .Columns("hAllocatedVAT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("hAllocatedDefault").DefaultCellStyle.Format = "n2"
            .Columns("hAllocatedDefault").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("hDefEnergy").DefaultCellStyle.Format = "n2"
            .Columns("hDefEnergy").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("hDefVAT").DefaultCellStyle.Format = "n2"
            .Columns("hDefVAT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("hAllocatedTotal").DefaultCellStyle.Format = "n2"
            .Columns("hAllocatedTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .EditMode = DataGridViewEditMode.EditProgrammatically
        End With

        Dim WBillSummary = WBillHelper.GetWESMBillSummary()
        Dim WBillGP = WBillHelper.GetWESMBillGPPosted()
        Dim WBillJV = WBillHelper.GetJournalVoucher()

        Dim cBatchCode As String = ParticipantPayment.First.PaymentBatchCode

        Me.LBL_Batch.Text = cBatchCode

        Dim GPDetails = (From x In WBillGP _
                         Where _
                         x.BatchCode = cBatchCode _
                         Select x).FirstOrDefault

        Dim JVdetails = (From x In WBillJV _
                         Where _
                         x.BatchCode = cBatchCode _
                         Select x).FirstOrDefault

        Me.LBL_Charge.Text = "Payment Summary"

        Me.LBL_JVNo.Text = JVdetails.JVNumber.ToString
        Me.LBL_BPeriod.Text = ""
        Me.LBL_STLRun.Text = ""

        If Len(Trim(GPDetails.GPRefNo)) = 0 Then
            Me.LBL_GPRef.Text = "No GP Reference No. set"
        Else
            Me.LBL_GPRef.Text = GPDetails.GPRefNo

        End If
        Me.LBL_STLRun.Text = "From Payment"
        For Each item In ParticipantPayment
            Dim cParticipant = item.Participant
            Dim cParticipantDetails = (From x In WBillParticipants _
                                       Where x.IDNumber = cParticipant.IDNumber _
                                       Select x).FirstOrDefault
            'With item
            '    DGV_Details.Rows.Add(.BillPeriod, cParticipantDetails.IDNumber, _
            '                         cParticipantDetails.ParticipantID, .AllocEnergyPayment, _
            '                         .AllocVATPayment, .AllocDefault, .DeferredPaymentEnergy, _
            '                         .DeferredPaymentVAT, .AllocTotalPayment)
            'End With
        Next
        Me.State = "PAYMENT"
    End Sub

    Private Sub CMD_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMD_Close.Click
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub CMD_GenerateReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMD_GenerateReport.Click
        Dim ds As New DataSet
        Dim dt As New DataTable

        ds = Me.ForReport()
        frmProgress.Show()
        With frmReportViewer
            Select Case Me.State
                Case "UPLOADED"
                    .LoadJournalVoucherDetailsUploaded(ds, Me.LBL_BPeriod.Text.ToString, CInt(Me.LBL_JVNo.Text.ToString), _
                                                   Me.LBL_GPRef.Text.ToString, Me.LBL_Batch.Text.ToString, Me.LBL_Charge.Text.ToString)
                    .Show()
                Case "OFFSETTING"
                    .LoadJournalVoucherDetailsOffset(ds, Me.LBL_BPeriod.Text.ToString, CInt(Me.LBL_JVNo.Text.ToString), _
                                                 Me.LBL_GPRef.Text.ToString, Me.LBL_Batch.Text.ToString, Me.LBL_Charge.Text.ToString)
                    .Show()
                Case "ADJUSTMENT"
                    .LoadJournalVoucherDetailsAdjustment(ds, Me.LBL_BPeriod.Text.ToString, CInt(Me.LBL_JVNo.Text.ToString), _
                                                         Me.LBL_GPRef.Text.ToString, Me.LBL_Batch.Text.ToString, Me.LBL_Charge.Text.ToString)
                    .Show()
                Case "PAYMENT"
                    .LoadJournalVoucherDetailsAdjustment(ds, Me.LBL_BPeriod.Text.ToString, CInt(Me.LBL_JVNo.Text.ToString), _
                                     Me.LBL_GPRef.Text.ToString, Me.LBL_Batch.Text.ToString, Me.LBL_Charge.Text.ToString)
                    .Show()
                Case "COLLECTION"
                    .LoadJournalVoucherDetailsAdjustment(ds, Me.LBL_BPeriod.Text.ToString, CInt(Me.LBL_JVNo.Text.ToString), _
                                     Me.LBL_GPRef.Text.ToString, Me.LBL_Batch.Text.ToString, Me.LBL_Charge.Text.ToString)
                    .Show()
            End Select

        End With
    End Sub

    Private Function ForReport() As DataSet
        Dim JVHeaders = WBillHelper.GetJournalVoucher()
        Dim GPPosting = WBillHelper.GetWESMBillGPPosted()
        Dim AllParticipants = WBillHelper.GetAMParticipantsAll()

        Dim ds As New DataSet
        Dim dt As New DataTable

        If Me.State = "OFFSETTING" Then
            Dim JVListDetails As New List(Of JournalVoucherSupport)
            JVListDetails = CType(Me.DGV_Details.DataSource, List(Of JournalVoucherSupport))

            dt.TableName = "JVReportDetails"
            With dt.Columns
                .Add("ID", GetType(String))
                .Add("SHORTNAME", GetType(String))
                .Add("INVOICE_NO", GetType(String))
                .Add("INVOICE_DATE", GetType(Date))
                .Add("DUE_DATE", GetType(Date))
                .Add("ARAMOUNT", GetType(Decimal))
                .Add("APAMOUNT", GetType(Decimal))
            End With

            For Each item In JVListDetails
                Dim dr As DataRow
                dr = dt.NewRow
                With item
                    dr("ID") = .IDNumber
                    dr("SHORTNAME") = .ParticipantName
                    dr("APAMOUNT") = .APAmount
                    dr("ARAMOUNT") = .ARAmount
                    dr("INVOICE_NO") = .InvDMCMNo
                    dr("INVOICE_DATE") = .InvoiceDate
                    dr("DUE_DATE") = .DueDate
                End With
                dt.Rows.Add(dr)
            Next
            ds.Tables.Add(dt)
        ElseIf Me.State = "UPLOADED" Then
            'For Uploaded
            Dim WESMBillDetails = WBillHelper.GetWESMBills()
            Dim ForReportWESMBill = (From x In WESMBillDetails Where x.BatchCode = LBL_Batch.Text.ToString _
                                     Select x Order By x.InvoiceNumber Ascending, x.IDNumber Ascending).ToList
            dt.TableName = "JVReportDetails"
            With dt.Columns
                .Add("ID", GetType(String))
                .Add("SHORTNAME", GetType(String))
                .Add("INVOICE_NO", GetType(String))
                .Add("INVOICE_DATE", GetType(Date))
                .Add("DUE_DATE", GetType(Date))
                .Add("ARAMOUNT", GetType(Decimal))
                .Add("APAMOUNT", GetType(Decimal))
                .Add("CHARGETYPE", GetType(String))
            End With

            Dim dicChargeType As New Dictionary(Of EnumChargeType, String)
            dicChargeType.Add(EnumChargeType.E, "ENERGY")
            dicChargeType.Add(EnumChargeType.EV, "VAT ON ENERGY")
            dicChargeType.Add(EnumChargeType.MF, "MARKET FEES")
            dicChargeType.Add(EnumChargeType.MFV, "VAT ON MF")

            For Each item In ForReportWESMBill
                Dim dr As DataRow
                dr = dt.NewRow
                With item
                    dr("ID") = .IDNumber
                    dr("SHORTNAME") = (From X In AllParticipants Where X.IDNumber = .IDNumber Select X.ParticipantID).FirstOrDefault
                    dr("INVOICE_NO") = .InvoiceNumber
                    dr("INVOICE_DATE") = .InvoiceDate
                    dr("DUE_DATE") = .DueDate
                    dr("CHARGETYPE") = dicChargeType(.ChargeType)
                    If .Amount < 0 Then
                        dr("ARAMOUNT") = .Amount * -1D
                    Else
                        dr("APAMOUNT") = .Amount
                    End If
                End With
                dt.Rows.Add(dr)
            Next
            ds.Tables.Add(dt)

            AllParticipants = (From x In AllParticipants Join y In ForReportWESMBill On x.IDNumber Equals y.IDNumber _
                               Select x).ToList

        ElseIf Me.State = "ADJUSTMENT" Then
            'For Uploaded
            Dim ForReportWESMBill = WBillHelper.GetAdjustedDetails(LBL_Batch.Text.ToString)
            dt.TableName = "JVReportDetails"
            With dt.Columns
                .Add("BILLING_PERIOD", GetType(Integer))
                .Add("STL_RUN", GetType(String))
                .Add("ID", GetType(String))
                .Add("SHORTNAME", GetType(String))
                .Add("INVOICE_NO", GetType(String))
                .Add("INVOICE_DATE", GetType(Date))
                .Add("ARAMOUNT", GetType(Decimal))
                .Add("APAMOUNT", GetType(Decimal))
                .Add("DUE_DATE", GetType(Date))
            End With

            For Each item In ForReportWESMBill
                Dim dr As DataRow
                dr = dt.NewRow
                With item
                    dr("BILLING_PERIOD") = .BillPeriod
                    dr("STL_RUN") = ""
                    dr("ID") = .IDNumber.IDNumber.ToString
                    dr("SHORTNAME") = .IDNumber.ParticipantID
                    Dim _smryType As String
                    If .SummaryType = EnumSummaryType.DCM Then
                        _smryType = "DMCM-"
                    Else
                        _smryType = "INV-"
                    End If
                    dr("INVOICE_NO") = _smryType & .INVDMCMNo
                    dr("INVOICE_DATE") = DBNull.Value
                    If .EndingBalance > 0 Then
                        dr("APAMOUNT") = .EndingBalance
                    ElseIf .EndingBalance < 0 Then
                        dr("APAMOUNT") = .EndingBalance
                    End If

                    dr("DUE_DATE") = .DueDate
                End With
                dt.Rows.Add(dr)
            Next
            ds.Tables.Add(dt)

            AllParticipants = (From x In AllParticipants Join y In ForReportWESMBill On x.IDNumber Equals y.IDNumber.IDNumber _
                               Select x).ToList

        ElseIf Me.State = "COLLECTION" Then
            'For Uploaded
            Dim WESMSummary = WBillHelper.GetWESMBillSummary()
            Dim ForReportWESMBill = WBillHelper.GetCollectionAllocation()
            ForReportWESMBill = (From X In ForReportWESMBill _
                                 Where X.BatchCode = Me.LBL_Batch.Text.ToString _
                                 Select X).ToList
            dt.TableName = "WESMBill"
            With dt.Columns
                .Add("BILLING_PERIOD", GetType(Integer))
                .Add("STL_RUN", GetType(String))
                .Add("ID_NUMBER", GetType(Integer))
                .Add("INVOICE_NO", GetType(Integer))
                .Add("INVOICE_DATE", GetType(Date))
                .Add("AMOUNT", GetType(Decimal))
                .Add("DUE_DATE", GetType(Date))
            End With

            For Each item In ForReportWESMBill
                Dim cGroupNo = item.WESMBillSummaryNo
                Dim cParticipant = (From x In WESMSummary _
                                    Where x.WESMBillSummaryNo = cGroupNo.WESMBillSummaryNo _
                                    Select x).FirstOrDefault
                Dim dr As DataRow
                dr = dt.NewRow
                With item
                    dr("BILLING_PERIOD") = .BillingPeriod
                    dr("STL_RUN") = ""
                    dr("ID_NUMBER") = cParticipant.IDNumber.IDNumber
                    dr("INVOICE_NO") = DBNull.Value
                    dr("INVOICE_DATE") = DBNull.Value
                    dr("AMOUNT") = .EndingBalance
                    dr("DUE_DATE") = .DueDate
                End With
                dt.Rows.Add(dr)
            Next
            ds.Tables.Add(dt)

        ElseIf Me.State = "PAYMENT" Then
            'For Uploaded
            Dim WESMSummary = WBillHelper.GetWESMBillSummary()
            Dim ForReportWESMBill = WBillHelper.GetPaymentAllocationParticipant()
            ForReportWESMBill = (From X In ForReportWESMBill _
                                 Where X.PaymentBatchCode = Me.LBL_Batch.Text.ToString _
                                 Select X).ToList
            dt.TableName = "WESMBill"
            With dt.Columns
                .Add("BILLING_PERIOD", GetType(Integer))
                .Add("STL_RUN", GetType(String))
                .Add("ID_NUMBER", GetType(Integer))
                .Add("INVOICE_NO", GetType(Integer))
                .Add("INVOICE_DATE", GetType(Date))
                .Add("AMOUNT", GetType(Decimal))
                .Add("DUE_DATE", GetType(Date))
            End With

            For Each item In ForReportWESMBill
                Dim dr As DataRow
                dr = dt.NewRow
                With item
                    dr("BILLING_PERIOD") = .BillPeriod
                    dr("STL_RUN") = ""
                    dr("ID_NUMBER") = .Participant
                    dr("INVOICE_NO") = DBNull.Value
                    dr("INVOICE_DATE") = DBNull.Value
                    'dr("AMOUNT") = .AllocTotalPayment
                    dr("DUE_DATE") = .DueDate
                End With
                dt.Rows.Add(dr)
            Next
            ds.Tables.Add(dt)
        End If


        Dim ParticipantTable As New DataTable

        'Get Participants
        Dim ParticipantsTable As New DataTable
        ParticipantsTable.TableName = "BillingParticipants"
        With ParticipantsTable.Columns
            .Add("ID_NUMBER", GetType(Integer))
            .Add("PARTICIPANT_ID", GetType(String))
            .Add("PARTICIPANTADDRESS", GetType(String))
            .Add("PARTICIPANT_NAME", GetType(String))
        End With

        For Each item In AllParticipants
            Dim row As DataRow
            row = ParticipantsTable.NewRow
            row("ID_NUMBER") = item.IDNumber
            row("PARTICIPANT_ID") = item.ParticipantID
            row("PARTICIPANTADDRESS") = item.ParticipantAddress
            row("PARTICIPANT_NAME") = item.FullName
            ParticipantsTable.Rows.Add(row)
        Next
        ds.Tables.Add(ParticipantsTable)
        Return ds
    End Function

    Private Sub CMD_Download_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMD_Download.Click
        Dim saveFileDialogBox As New SaveFileDialog

        With saveFileDialogBox
            .Title = "Save WESM Bill"
            .Filter = "CSV Files (*.csv)|*.csv"
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                WBillHelper.subExportDGVToCSV(.FileName, DGV_Details, True)
                MsgBox("Successfully Downloaded!", MsgBoxStyle.Information, "Done")
            End If
        End With
    End Sub

End Class