'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmUploadedWESMBill
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     August 24, 2011
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
'   August 24, 2011         Vladimir E. Espiritu            GUI initialization
'   September 07, 2011      Vladimir E. Espiritu            Added market fees in filter
'   September 29, 2011      Vladimir E. Espiritu            Generate the WESM Bill Report
'   December 09, 2011       Vladimir E. Espiritu            Added Invoice Date and Participant ID in Filtering.
'                                                           Separate the summary of Energy/Market Fees and VAT
'   December 14, 2011       Vladimir E. Espiritu            Added method to download WESM Bill into CSV format
'   February 21, 2011       Vladimir E. Espiritu            Format decimal columns
'   July 06, 2012           Vladimir E. Espiritu            Added validations in generating/downloading the reports
'


Option Explicit On
Option Strict On

Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmUploadedWESMBill
    Private WBillHelper As WESMBillHelper
    Private cntTime As Integer

    Private _listFilters As List(Of WESMBill)
    Private _billingPeriods As List(Of CalendarBillingPeriod)
    Private _ctrlEnergy As Boolean
    Private _ctrlMarketFees As Boolean
    Private _ctrlBillingPeriod As Boolean
    Private _ctrlBillingPeriodValue As String
    Private _ctrlInvoiceDate As Boolean
    Private _ctrlInvoiceDateFromValue As Date
    Private _ctrlInvoiceDateToValue As Date
    Private _ctrlStlRun As Boolean
    Private _ctrlStlRunValue As String
    Private _ctrlParticipantID As Boolean
    Private _ctrlParticipantIDValue As String

    Private Enum FilterType
        Energy
        MarketFees
        BillingPeriod
        InvoiceDate
        Participants
        SettlementRun
    End Enum

    Private Sub frmWESMBillSummaryRPT_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.MdiParent = MainForm
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        Me.ClearControls()
        'Me.LoadDefaultGrid()
        'Me.ManipulateGridView()

        'Get the values for Billing Periods and Settlement Runs per Charge Type 
        Me._listFilters = WBillHelper.GetFilterValuesForWESMBillInquiry()

        Me._billingPeriods = WBillHelper.GetCalendarBP()

        'Get the bill Participants
        Dim listParticipants = WBillHelper.GetAMParticipants()

        With Me.ddlParticipant
            .DataSource = listParticipants
            .DisplayMember = "ParticipantID"
            .ValueMember = "IDNumber"
        End With

        AddHandler Me.rbEnergy.CheckedChanged, AddressOf Me.rbChargeTypeCheckedChange
        AddHandler Me.rbMF.CheckedChanged, AddressOf Me.rbChargeTypeCheckedChange
        AddHandler ddlBillingPeriod.SelectedIndexChanged, AddressOf Me.ddlBillingPeriod_SelectedIndexChanged

        Me.txtLoading.Visible = False
        Me.ViewStatus.Maximum = 20
        Me.ViewStatus.Value = 1
        Me.ViewStatus.Visible = False
        Me.ddlParticipant.Enabled = False
        Me.ddlSTLRun.Enabled = False
        Me.rbBillingPeriod.Select()
        Me.rbEnergy.Checked = False 'Unselect first so that it will trigger the checkedchange
        Me.rbEnergy.Select()
        Me.ddlParticipant.SelectedIndex = -1
    End Sub

    Private Sub chckStlRun_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chckStlRun.CheckedChanged
        With Me.ddlSTLRun
            If Me.chckStlRun.Checked Then
                .Enabled = True
            Else
                .Enabled = False
                .SelectedIndex = -1
            End If
        End With
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.cntTime += 1
        If Me.cntTime = 20 Then
            Me.ViewStatus.Value = Me.ViewStatus.Maximum
            Me.Timer1.Enabled = False
            cntTime = 0
        Else
            Me.ViewStatus.Value += 1
        End If
    End Sub

    Private Sub ddlBillingPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.LoadFilters(FilterType.BillingPeriod)
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Dim dt As New DataTable
        Dim chargeType As EnumChargeType
        Dim billingPeriod As Integer = Nothing
        Dim startDate As Date = Nothing, endDate As Date = Nothing
        Dim stlRun As String = Nothing, IDNumber As String = ""

        If Me.btnLoad.Text = "Search" Then
            MsgBox("Records are updated!", MsgBoxStyle.Information, "Uploaded WESM Bill")
            Exit Sub
        End If

        If Me.Validation = False Then
            Exit Sub
        End If

        If Me.rbBillingPeriod.Checked Then
            billingPeriod = CInt(Me.ddlBillingPeriod.Text)
        Else
            startDate = Me.dtFrom.Value
            endDate = Me.dtTo.Value
        End If

        If Me.chckStlRun.Checked Then
            stlRun = Me.ddlSTLRun.Text
        End If

        If Me.chckParticipant.Checked Then
            IDNumber = CStr(Me.ddlParticipant.SelectedValue)
        End If

        If Me.rbEnergy.Checked = True Then
            chargeType = EnumChargeType.E
        Else
            chargeType = EnumChargeType.MF
        End If

        If billingPeriod <> Nothing And stlRun <> Nothing And IDNumber <> Nothing Then
            dt = WBillHelper.GetWESMBillReport(chargeType, billingPeriod, stlRun, IDNumber)
        ElseIf billingPeriod <> Nothing And stlRun <> Nothing And IDNumber = Nothing Then
            dt = WBillHelper.GetWESMBillReport(stlRun, chargeType, billingPeriod)
        ElseIf billingPeriod <> Nothing And stlRun = Nothing And IDNumber <> Nothing Then
            dt = WBillHelper.GetWESMBillReport(chargeType, billingPeriod, IDNumber)
        ElseIf billingPeriod <> Nothing And stlRun = Nothing And IDNumber = Nothing Then
            dt = WBillHelper.GetWESMBillReport(chargeType, billingPeriod)
        ElseIf startDate <> Nothing And endDate <> Nothing And stlRun <> Nothing And IDNumber <> Nothing Then
            dt = WBillHelper.GetWESMBillReport(chargeType, startDate, endDate, stlRun, IDNumber)
        ElseIf startDate <> Nothing And endDate <> Nothing And stlRun <> Nothing And IDNumber = Nothing Then
            dt = WBillHelper.GetWESMBillReport(stlRun, chargeType, startDate, endDate)
        ElseIf startDate <> Nothing And endDate <> Nothing And stlRun = Nothing And IDNumber <> Nothing Then
            dt = WBillHelper.GetWESMBillReport(chargeType, startDate, endDate, IDNumber)
        ElseIf startDate <> Nothing And endDate <> Nothing And stlRun = Nothing And IDNumber = Nothing Then
            dt = WBillHelper.GetWESMBillReport(chargeType, startDate, endDate)
        Else
            MsgBox("Error in system's code!", MsgBoxStyle.Critical, "Contact the Software Developer")
            Exit Sub
        End If

        'Check if there are data
        If dt.Rows.Count = 0 Then
            MsgBox("No records found!", MsgBoxStyle.Information, "No data")
            Exit Sub
        End If

        'Compute the AP and AR for ENERGY/MF
        Dim APEnergyMF As Decimal = (From x In dt.AsEnumerable() Where x.Field(Of Decimal)("ENERGY_MF") > 0 _
                                     Select x.Field(Of Decimal)("ENERGY_MF")).Sum()

        Dim AREnergyMF As Decimal = (From x In dt.AsEnumerable() Where x.Field(Of Decimal)("ENERGY_MF") < 0 _
                                     Select x.Field(Of Decimal)("ENERGY_MF")).Sum()

        Dim NSSEnergyMF As Decimal = APEnergyMF + AREnergyMF

        'Compute the AP and AR for VAT
        Dim APVatMF As Decimal = (From x In dt.AsEnumerable() Where x.Field(Of Decimal)("VAT") > 0 _
                                  Select x.Field(Of Decimal)("VAT")).Sum()

        Dim ARVatMF As Decimal = (From x In dt.AsEnumerable() Where x.Field(Of Decimal)("VAT") < 0 _
                                  Select x.Field(Of Decimal)("VAT")).Sum()

        Dim NSSVatMF As Decimal = APVatMF + ARVatMF

        Me.txtAPEnergyMF.Text = APEnergyMF.ToString("#,###.#0")
        Me.txtAREnergyMF.Text = AREnergyMF.ToString("#,###.#0")

        If chargeType = EnumChargeType.E Then
            If APEnergyMF <> 0D And AREnergyMF <> 0D Then
                Me.txtNSSEnergyMF.Text = NSSEnergyMF.ToString("#,###.#0")
            Else
                Me.txtNSSEnergyMF.Text = "0.00"
            End If
        Else
            Me.txtNSSEnergyMF.Text = "0.00"
        End If
        
        Me.txtAPVat.Text = APVatMF.ToString("#,###.#0")
        Me.txtARVat.Text = ARVatMF.ToString("#,###.#0")

        If chargeType = EnumChargeType.E Then
            If APVatMF <> 0D And ARVatMF <> 0D Then
                Me.txtNSSVat.Text = NSSVatMF.ToString("#,###.#0")
            Else
                Me.txtNSSVat.Text = "0.00"
            End If
        Else
            Me.txtNSSVat.Text = "0.00"
        End If
        
        'Set the datasource
        Me.LoadDataIntoDataGrid(dt)

        'Get the current values of controls
        Me._ctrlEnergy = Me.rbEnergy.Checked
        Me._ctrlMarketFees = Me.rbMF.Checked
        Me._ctrlBillingPeriod = Me.rbBillingPeriod.Checked
        Me._ctrlBillingPeriodValue = Me.ddlBillingPeriod.Text
        Me._ctrlInvoiceDate = Me.rbInvoiceDate.Checked
        Me._ctrlInvoiceDateFromValue = CDate(Me.dtFrom.Value)
        Me._ctrlInvoiceDateToValue = CDate(Me.dtTo.Value)
        Me._ctrlStlRun = Me.chckStlRun.Checked
        Me._ctrlStlRunValue = Me.ddlSTLRun.Text
        Me._ctrlParticipantID = Me.chckParticipant.Checked
        Me._ctrlParticipantIDValue = Me.ddlParticipant.Text
    End Sub

    Private Sub btnShowReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowReport.Click
        Dim dt As New DataTable
        Dim bp As Integer
        Dim bplabel As String
        Dim bpvalue As String
        Dim billtype As String

        'Get the reccords in datagrid
        dt = Me.GetDataInDataGrid()

        'Check if there are data
        If dt.Rows.Count = 0 Then
            MsgBox("No records to view", MsgBoxStyle.Information, "No data")
            Exit Sub
        End If

        If Me.ControlsChangedValues Then
            MsgBox("Filter values were changed. Poke Search button to refresh the records!", MsgBoxStyle.Exclamation, "Warning")
            Exit Sub
        End If

        'Get if Energy or MF is selected
        If Me.rbEnergy.Checked Then
            billtype = "ENERGY"
        Else
            billtype = "MF"
        End If

        If Me.rbBillingPeriod.Checked Then
            bplabel = "Billing Period:"

            'Get the Billing period
            bp = CInt(Me.ddlBillingPeriod.Text)

            If bp Mod 10 = 1 And (bp > 20 Or bp < 10) Then
                bpvalue = Me.ddlBillingPeriod.Text & "st (" & Me.ddlBillingPeriod.SelectedValue.ToString() & ")"
            ElseIf bp Mod 10 = 2 And (bp > 20 Or bp < 10) Then
                bpvalue = Me.ddlBillingPeriod.Text & "nd (" & Me.ddlBillingPeriod.SelectedValue.ToString() & ")"
            ElseIf bp Mod 10 = 3 And (bp > 20 Or bp < 10) Then
                bpvalue = Me.ddlBillingPeriod.Text & "rd (" & Me.ddlBillingPeriod.SelectedValue.ToString() & ")"
            Else
                bpvalue = Me.ddlBillingPeriod.Text & "th (" & Me.ddlBillingPeriod.SelectedValue.ToString() & ")"
            End If
        Else
            bplabel = "Invoice Date:"
            bpvalue = Me.dtFrom.Value.ToString("MM/dd/yyyy") & " - " & Me.dtTo.Value.ToString("MM/dd/yyyy")
        End If

        Me.ViewStatus.Visible = True
        Me.Timer1.Enabled = True
        Me.txtLoading.Visible = True

        Dim frmViewer As New frmReportViewer()
        With frmViewer
            .LoadWESMBillReport(dt, bpvalue, bplabel, billtype, CDec(Me.txtAPEnergyMF.Text.Replace("'", "")), _
                                CDec(Me.txtAREnergyMF.Text.Replace("'", "")), CDec(Me.txtNSSEnergyMF.Text.Replace("'", "")), _
                                CDec(Me.txtAPVat.Text.Replace("'", "")), CDec(Me.txtARVat.Text.Replace("'", "")), _
                                CDec(Me.txtNSSVat.Text.Replace("'", "")))
            .ShowDialog()
        End With

        Me.txtLoading.Visible = False
        Me.ViewStatus.Visible = False
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.ClearControls()
        Me.rbBillingPeriod.Select()
        Me.rbEnergy.Select()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rbChargeTypeCheckedChange(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.DGridView.Rows.Clear()

        If Me.rbEnergy.Checked Then
            Me.LoadFilters(FilterType.Energy)
            Me.lblEnergyMF.Text = "ENERGY"
            Me.lblVat.Text = "VAT On ENERGY"
            Me.DGridView.Columns("colEnergyMFBCode").HeaderText = "Energy-BatchCode"
            Me.DGridView.Columns("colEnergyMF").HeaderText = "Energy"
        Else
            Me.LoadFilters(FilterType.MarketFees)
            Me.lblEnergyMF.Text = "MARKET FEES"
            Me.lblVat.Text = "VAT On MARKET FEES"
            Me.DGridView.Columns("colEnergyMFBCode").HeaderText = "MarketFees-BatchCode"
            Me.DGridView.Columns("colEnergyMF").HeaderText = "MarketFees"
        End If

        Me.txtAPEnergyMF.Text = "0.00"
        Me.txtAREnergyMF.Text = "0.00"
        Me.txtNSSEnergyMF.Text = "0.00"
        Me.txtAPVat.Text = "0.00"
        Me.txtARVat.Text = "0.00"
        Me.txtNSSVat.Text = "0.00"
    End Sub

    Private Sub rdDateCheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
                                     Handles rbBillingPeriod.CheckedChanged, rbInvoiceDate.CheckedChanged
        If Me.rbBillingPeriod.Checked Then
            Me.ddlBillingPeriod.Enabled = True
            Me.dtFrom.Enabled = False
            Me.dtTo.Enabled = False
            Me.ddlSTLRun.DataSource = Nothing
            Me.ddlSTLRun.SelectedIndex = -1
        Else
            Me.ddlBillingPeriod.Enabled = False
            Me.dtFrom.Enabled = True
            Me.dtTo.Enabled = True
            Me.ddlBillingPeriod.SelectedIndex = -1
            Me.LoadFilters(FilterType.InvoiceDate)
        End If

    End Sub

    Private Sub chckParticipant_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chckParticipant.CheckedChanged
        With Me.ddlParticipant
            If Me.chckParticipant.Checked Then
                .Enabled = True
            Else
                .Enabled = False
                .SelectedIndex = -1
            End If
        End With
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim dt As DataTable

        'Get the records in datagrid
        dt = Me.GetDataInDataGrid()

        If dt.Rows.Count = 0 Then
            MsgBox("No records to download!", MsgBoxStyle.Information, "No Data")
            Exit Sub
        End If

        If Me.ControlsChangedValues Then
            MsgBox("Filter values were changed. Poke Search button to refresh the records!", MsgBoxStyle.Exclamation, "Warning")
            Exit Sub
        End If

        Dim saveFileDialogBox As New SaveFileDialog

        With saveFileDialogBox
            .Title = "Save WESM Bill"
            .Filter = "CSV Files (*.csv)|*.csv"
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim Csv As New Exporter()
                Using CsvWriter As New StreamWriter(.FileName)
                    CsvWriter.Write(Csv.CsvFromDatatable(dt))
                End Using

                'System.Diagnostics.Process.Start("C:\Users\dd_w002\Desktop\TestFile.csv")
                MsgBox("Successfully Downloaded!", MsgBoxStyle.Information, "Done")
            End If
        End With
    End Sub

    Private Sub DGridView_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        If e.RowIndex = -1 Then
            Exit Sub
        End If

        Dim invoiceNo As Long = CLng(Me.DGridView.Rows(e.RowIndex).Cells("colInvoiceNo").Value)
        Dim EnergyMFAmount As Long = CLng(Me.DGridView.Rows(e.RowIndex).Cells("colEnergyMF").Value)
        Dim VATAmount As Long = CLng(Me.DGridView.Rows(e.RowIndex).Cells("colVAT").Value)


        If EnergyMFAmount = 0 And VATAmount = 0 Then
            MsgBox("Nothing to View!", MsgBoxStyle.Critical, "Warning")
            Exit Sub
        End If

        Dim frm As New frmChargeTypeSelection
        With frm
            If Not Me.rbEnergy.Checked Then
                .rbEnergyMF.Text = "Market Fees"
                .rbVAT.Text = "VAT on Market Fees"
            End If

            If EnergyMFAmount = 0 Then
                .rbEnergyMF.Enabled = False
                .rbVAT.Checked = True
            End If

            If VATAmount = 0 Then
                .rbVAT.Checked = False
                .rbEnergyMF.Checked = True
            End If
            .ViewType = 1
            .InvoiceNumber = invoiceNo
            .ShowDialog()
        End With
    End Sub

    Private Sub DGridView_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGridView.CellFormatting
        If e.ColumnIndex = 6 Or e.ColumnIndex = 8 Or e.ColumnIndex = 9 Then
            Me.DGridView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = FormatNumber(Me.DGridView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value, 2)
        End If
    End Sub

#Region "Methods/Functions"

    Private Sub ClearControls()
        Me.rbEnergy.Checked = False
        Me.rbMF.Checked = False
        Me.rbBillingPeriod.Checked = False
        Me.rbInvoiceDate.Checked = False
        Me.dtFrom.Value = Today
        Me.dtTo.Value = Today
        Me.ddlBillingPeriod.DataSource = Nothing
        Me.ddlBillingPeriod.SelectedIndex = -1
        Me.chckStlRun.Checked = False
        Me.ddlSTLRun.DataSource = Nothing
        Me.ddlSTLRun.SelectedIndex = -1
        Me.chckParticipant.Checked = False
        Me.ddlParticipant.SelectedIndex = -1
    End Sub

    Private Sub LoadFilters(ByVal filterValue As FilterType)

        Select Case filterValue
            Case FilterType.Energy
                Dim BPs = (From x In Me._listFilters Join y In Me._billingPeriods On x.BillingPeriod Equals y.BillingPeriod _
                           Where x.ChargeType = EnumChargeType.E _
                           Select BillingPeriod = y.BillingPeriod, _
                                  Rangedate = (y.StartDate.ToString("MM/dd/yyyy") & " - " & y.EndDate.ToString("MM/dd/yyyy")).ToString() Distinct _
                                  Order By BillingPeriod Descending).ToList()

                With Me.ddlBillingPeriod
                    .DisplayMember = "BillingPeriod"
                    .ValueMember = "Rangedate"
                    .DataSource = BPs
                    .SelectedIndex = -1
                End With

                Me.ddlSTLRun.DataSource = Nothing
                If Me.rbInvoiceDate.Checked Then
                    Dim STLRuns = (From x In Me._listFilters _
                                   Select x.SettlementRun Distinct Order By SettlementRun).ToList()
                    Me.ddlSTLRun.DataSource = STLRuns
                    Me.ddlSTLRun.SelectedIndex = -1
                End If

            Case FilterType.MarketFees
                Dim BPs = (From x In Me._listFilters Join y In Me._billingPeriods On x.BillingPeriod Equals y.BillingPeriod _
                           Where x.ChargeType = EnumChargeType.MF _
                           Select BillingPeriod = y.BillingPeriod, _
                                  Rangedate = (y.StartDate.ToString("MM/dd/yyyy") & " - " & y.EndDate.ToString("MM/dd/yyyy")).ToString() Distinct _
                                  Order By BillingPeriod Descending).ToList()

                With Me.ddlBillingPeriod
                    .DisplayMember = "BillingPeriod"
                    .ValueMember = "Rangedate"
                    .DataSource = BPs
                    .SelectedIndex = -1
                End With
                Me.ddlSTLRun.DataSource = Nothing

                If Me.rbInvoiceDate.Checked Then
                    Dim STLRuns = (From x In Me._listFilters _
                                   Select x.SettlementRun Distinct Order By SettlementRun).ToList()
                    Me.ddlSTLRun.DataSource = STLRuns
                    Me.ddlSTLRun.SelectedIndex = -1
                End If

            Case FilterType.BillingPeriod
                If Me.ddlBillingPeriod.Text.Trim.Length = 0 Then
                    Exit Sub
                End If

                Dim STLRuns As List(Of String)
                If Me.rbEnergy.Checked Then
                    STLRuns = (From x In Me._listFilters Where x.ChargeType = EnumChargeType.E _
                               And x.BillingPeriod = CInt(Me.ddlBillingPeriod.Text) _
                               Select x.SettlementRun Distinct Order By SettlementRun).ToList()
                Else
                    STLRuns = (From x In Me._listFilters Where x.ChargeType = EnumChargeType.MF _
                               And x.BillingPeriod = CInt(Me.ddlBillingPeriod.Text) _
                               Select x.SettlementRun Distinct Order By SettlementRun).ToList()
                End If

                Me.ddlSTLRun.DataSource = STLRuns
                Me.ddlSTLRun.SelectedIndex = -1

            Case FilterType.InvoiceDate
                Dim STLRuns = (From x In Me._listFilters _
                               Select x.SettlementRun Distinct Order By SettlementRun).ToList()
                Me.ddlSTLRun.DataSource = STLRuns
                Me.ddlSTLRun.SelectedIndex = -1

            Case FilterType.Participants

            Case FilterType.SettlementRun

            Case Else
        End Select
    End Sub

    Private Function Validation() As Boolean
        If Not Me.rbEnergy.Checked And Not Me.rbMF.Checked Then
            MsgBox("Please select first the charge type!", MsgBoxStyle.Critical, "Specify the inputs")
            Exit Function
        ElseIf Not Me.rbBillingPeriod.Checked And Not Me.rbInvoiceDate.Checked Then
            MsgBox("Please select between Billing Period and Invoice Date!", MsgBoxStyle.Critical, "Specify the inputs")
            Exit Function
        ElseIf Me.rbBillingPeriod.Checked And Me.ddlBillingPeriod.Items.Count = 0 Then
            MsgBox("No existing records!", MsgBoxStyle.Critical, "No data")
            Exit Function
        ElseIf Me.rbBillingPeriod.Checked And Me.ddlBillingPeriod.Text = "" Then
            MsgBox("Please select the Billing Period!", MsgBoxStyle.Critical, "Specify the inputs")
            Exit Function
        ElseIf Me.rbInvoiceDate.Checked And Me.dtFrom.Value > Me.dtTo.Value Then
            MsgBox("Invalid Date Range!", MsgBoxStyle.Critical, "Specify the inputs")
            Exit Function
        ElseIf Me.chckStlRun.Checked And Me.ddlSTLRun.Items.Count = 0 Then
            MsgBox("No existing records!", MsgBoxStyle.Critical, "No data")
            Exit Function
        ElseIf Me.chckStlRun.Checked And Me.ddlSTLRun.Text = "" Then
            MsgBox("Please select the Settlement Run!", MsgBoxStyle.Critical, "Specify the inputs")
            Exit Function
        ElseIf Me.chckParticipant.Checked And Me.ddlParticipant.Text = "" Then
            MsgBox("Please select the Participant ID!", MsgBoxStyle.Critical, "Specify the inputs")
            Exit Function
        ElseIf Me.chckStlRun.Checked And Me.ddlSTLRun.SelectedIndex = -1 Then
            MsgBox("Please select first the settlement run!", MsgBoxStyle.Critical, "Specify the inputs")
            Exit Function
        Else
            Validation = True
        End If
    End Function

    Private Sub LoadDataIntoDataGrid(ByVal dt As DataTable)
        Me.DGridView.Rows.Clear()

        For index As Integer = 0 To dt.Rows.Count - 1
            Dim row As DataRow = dt.Rows(index)
            Me.DGridView.Rows.Add(CStr(row("INVOICE_NO")), CStr(row("PARTICIPANT_ID")), CStr(row("INVOICE_DATE")), _
                                  CStr(row("DUE_DATE")), CStr(row("STL_RUN")), CStr(row("ENERGY_MF_BCODE")), CStr(row("ENERGY_MF")), _
                                  CStr(row("VAT_BCODE")), CStr(row("VAT")), CStr(row("TOTAL")), CStr(row("UPDATED_BY")), CStr(row("UPDATED_DATE")))
        Next
    End Sub

    Private Function GetDataInDataGrid() As DataTable
        Dim dt As New DataTable

        With dt.Columns
            .Add("INVOICE_NO", GetType(String))
            .Add("PARTICIPANT_ID", GetType(String))
            .Add("INVOICE_DATE", GetType(String))
            .Add("DUE_DATE", GetType(String))
            .Add("STL_RUN", GetType(String))
            .Add("ENERGY_MF_BCODE", GetType(String))
            .Add("ENERGY_MF", GetType(Decimal))
            .Add("VAT_BCODE", GetType(String))
            .Add("VAT", GetType(Decimal))
            .Add("TOTAL", GetType(Decimal))
            .Add("UPDATED_BY", GetType(String))
            .Add("UPDATED_DATE", GetType(Date))
        End With

        dt.TableName = "WESMBillReport"
        dt.AcceptChanges()

        For index As Integer = 0 To Me.DGridView.RowCount - 1
            With Me.DGridView.Rows(index)
                Dim row = dt.NewRow()
                row("INVOICE_NO") = .Cells("colInvoiceNo").Value
                row("PARTICIPANT_ID") = .Cells("colParticipantID").Value
                row("INVOICE_DATE") = .Cells("colInvoiceDate").Value
                row("DUE_DATE") = .Cells("colDueDate").Value
                row("STL_RUN") = .Cells("colStlRun").Value
                row("ENERGY_MF_BCODE") = .Cells("colEnergyMFBCode").Value
                row("ENERGY_MF") = .Cells("colEnergyMF").Value
                row("VAT_BCODE") = .Cells("colVatBCode").Value
                row("VAT") = .Cells("colVAT").Value
                row("TOTAL") = .Cells("colTotal").Value
                row("UPDATED_BY") = .Cells("colUpdatedBy").Value
                row("UPDATED_DATE") = .Cells("colUpdatedDate").Value
                dt.Rows.Add(row)
            End With
        Next

        dt.AcceptChanges()
        Return dt
    End Function

    Private Function ControlsChangedValues() As Boolean
        If Me.rbEnergy.Checked <> Me._ctrlEnergy Or Me.rbMF.Checked <> Me._ctrlMarketFees _
             Or Me.rbBillingPeriod.Checked <> Me._ctrlBillingPeriod _
             Or Me.rbInvoiceDate.Checked <> Me._ctrlInvoiceDate _
             Or Me.chckStlRun.Checked <> Me._ctrlStlRun _
             Or Me.chckParticipant.Checked <> Me._ctrlParticipantID Then

            Return True
        End If

        If Me.rbBillingPeriod.Checked Then
            If Me.ddlBillingPeriod.Text <> Me._ctrlBillingPeriodValue Then
                Return True
            End If
        End If

        If Me.rbInvoiceDate.Checked Then
            If CDate(FormatDateTime(Me.dtFrom.Value, DateFormat.ShortDate)) <> Me._ctrlInvoiceDateFromValue Or _
               CDate(FormatDateTime(Me.dtTo.Value, DateFormat.ShortDate)) <> Me._ctrlInvoiceDateToValue Then

                Return False
            End If
        End If

        If Me.chckStlRun.Checked Then
            If Me.ddlSTLRun.Text <> Me._ctrlStlRunValue Then
                Return True
            End If
        End If

        If Me.chckParticipant.Checked Then
            If Me.ddlParticipant.Text <> Me._ctrlParticipantIDValue Then
                Return True
            End If
        End If

        Return False
    End Function

#End Region


End Class