Imports System.Text
Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Imports System.Threading

Public Class ImportWTAFromCRSSDBHelper
    Public _ListOfErrorDic As Dictionary(Of String, List(Of String))
    Public _WBillHelper As WESMBillHelper
    Private _NpgDataAccess As NpgsqlDAL
    Private _AMParticipantsList As New List(Of AMParticipants)
    Public Sub New()
        Me._NpgDataAccess = NpgsqlDAL.GetInstance
        Me._NpgDataAccess.ConnectionString = AMModule.ConnectionStringCRSS
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._WBillHelper.ConnectionString = AMModule.ConnectionString

        Me._NewWTADueDateList = Me.GetNewWESMInvoicesDueDate()
        Me._ListOfErrorDic = New Dictionary(Of String, List(Of String))
        Me._ParentChangeID = Me._WBillHelper.GetWESMBillSummaryChangeParentIDAll()
        Me._AMParticipantsList = Me._WBillHelper.GetAMParticipants()
    End Sub

#Region "Property of WESM Bill Due Date List"
    Private _NewWTADueDateList As New List(Of Date)
    Public Property NewWTADueDateList() As List(Of Date)
        Get
            Return _NewWTADueDateList
        End Get
        Set(ByVal value As List(Of Date))
            _NewWTADueDateList = value
        End Set
    End Property
#End Region

#Region "Property of New Import WTA From CRSS List"
    Private _newImportWTAFromCRSSList As New List(Of ImportWTAFromCRSS)
    Public Property newImportWTAFromCRSSList() As List(Of ImportWTAFromCRSS)
        Get
            Return _newImportWTAFromCRSSList
        End Get
        Set(ByVal value As List(Of ImportWTAFromCRSS))
            _newImportWTAFromCRSSList = value
        End Set
    End Property
#End Region

#Region "Property of ParentChangeID"
    Private _ParentChangeID As List(Of WESMBillSummaryChangeParentId)
    Public Property ParentChangeID() As List(Of WESMBillSummaryChangeParentId)
        Get
            Return _ParentChangeID
        End Get
        Set(ByVal value As List(Of WESMBillSummaryChangeParentId))
            _ParentChangeID = value
        End Set
    End Property
#End Region

#Region "Get New WESM Bill Due Date"
    Public Function GetNewWESMInvoicesDueDate() As List(Of Date)
        Dim result As New List(Of Date)
        Dim report As New DataReport
        Dim SQL As String

        Try
            If AMModule.RegionType = "LV" Then
                SQL = "SELECT DISTINCT DUE_DATE FROM settlement.txn_alloc_cover_summary WHERE REGION_GROUP = 'LUZON_VISAYAS' and due_date is not null Order by due_date desc"
            ElseIf AMModule.RegionType = "M" Then
                SQL = "SELECT DISTINCT DUE_DATE FROM settlement.txn_alloc_cover_summary WHERE REGION_GROUP = 'MINDANAO'  and due_date is not null Order by due_date desc"
            Else
                SQL = "SELECT DISTINCT DUE_DATE FROM settlement.txn_alloc_cover_summary WHERE due_date is not null Order by due_date desc"
            End If

            report = Me._NpgDataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.GetNewWESMInvoicesDueDate(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function
    Private Function GetNewWESMInvoicesDueDate(ByVal dr As IDataReader) As List(Of Date)
        Dim result As New List(Of Date)

        Try
            While dr.Read()
                With dr
                    result.Add(CDate(.Item("DUE_DATE")))
                End With
            End While
            result.TrimExcess()

        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return result
    End Function
#End Region

#Region "Get WESM Transaction Allocation Cover Summary"
    Public Function GetWTACoverySummaryList(ByVal bp As Integer, ByVal stlRun As String, ByVal duedate As Date, ByVal groupID As String) As List(Of WESMBillAllocCoverSummary)
        Dim result As New List(Of WESMBillAllocCoverSummary)
        Dim report As New DataReport
        Dim SQL As String

        Try
            If AMModule.RegionType = "LV" Then
                SQL = "SELECT tacs.summary_id, bp.billing_period, tacs.stl_run, tacs.group_id, tacs.billing_id, tacs.trading_participant_settlement_id, tacs.invoice_id, " & vbNewLine _
                    & "tacs.net_seller_buyer_tag, tacs.transaction_number, tacs.due_date, tacs.date, tacs.non_vatable, tacs.zero_rated, " & vbNewLine _
                    & "tacs.vatable_sales, tacs.vatable_purchases, tacs.zero_rated_sales, tacs.zero_rated_ecozone_sales, tacs.zero_rated_purchases, tacs.zero_rated_ecozone_purchases, " & vbNewLine _
                    & "tacs.vat_on_sales, tacs.vat_on_purchases, tacs.tta, tacs.nss_flowback, tacs.energy_witholding_tax_sales, tacs.energy_witholding_tax_purchases, " & vbNewLine _
                    & "tacs.net_energy_qty, tacs.gmr, tacs.market_fees_rate, tacs.wht, tacs.ith, tacs.region_group, tacs.remarks, tacs.genx_flowback " & vbNewLine _
                    & "FROM settlement.txn_alloc_cover_summary tacs " & vbNewLine _
                    & "JOIN meterprocess.txn_billing_period bp ON bp.start_date = tacs.billing_period_start " & vbNewLine _
                    & "And bp.end_date = tacs.billing_period_end " & vbNewLine _
                    & "WHERE tacs.due_date = to_date('" & duedate & "', 'mm/dd/yyyy') and bp.billing_period = " & bp & " " & vbNewLine _
                    & "and tacs.stl_run = '" & stlRun & "' and tacs.group_id = '" & groupID & "' and tacs.region_group = 'LUZON_VISAYAS' " & vbNewLine _
                    & "ORDER BY tacs.due_date, bp.billing_period, tacs.stl_run, tacs.group_id"
            ElseIf AMModule.RegionType = "M" Then
                SQL = "SELECT tacs.summary_id, bp.billing_period, tacs.stl_run, tacs.group_id, tacs.billing_id, tacs.trading_participant_settlement_id, tacs.invoice_id, " & vbNewLine _
                    & "tacs.net_seller_buyer_tag, tacs.transaction_number, tacs.due_date, tacs.date, tacs.non_vatable, tacs.zero_rated, " & vbNewLine _
                    & "tacs.vatable_sales, tacs.vatable_purchases, tacs.zero_rated_sales, tacs.zero_rated_ecozone_sales, tacs.zero_rated_purchases, tacs.zero_rated_ecozone_purchases, " & vbNewLine _
                    & "tacs.vat_on_sales, tacs.vat_on_purchases, tacs.tta, tacs.nss_flowback, tacs.energy_witholding_tax_sales, tacs.energy_witholding_tax_purchases, " & vbNewLine _
                    & "tacs.net_energy_qty, tacs.gmr, tacs.market_fees_rate, tacs.wht, tacs.ith, tacs.region_group, tacs.remarks, tacs.genx_flowback " & vbNewLine _
                    & "FROM settlement.txn_alloc_cover_summary tacs " & vbNewLine _
                    & "JOIN meterprocess.txn_billing_period bp ON bp.start_date = tacs.billing_period_start " & vbNewLine _
                    & "And bp.end_date = tacs.billing_period_end " & vbNewLine _
                    & "WHERE tacs.due_date = to_date('" & duedate & "', 'mm/dd/yyyy') and bp.billing_period = " & bp & " " & vbNewLine _
                    & "and tacs.stl_run = '" & stlRun & "' and tacs.group_id = '" & groupID & "' and tacs.region_group = 'MINDANAO' " & vbNewLine _
                    & "ORDER BY tacs.due_date, bp.billing_period, tacs.stl_run, tacs.group_id"
            Else
                SQL = "SELECT tacs.summary_id, bp.billing_period, tacs.stl_run, tacs.group_id, tacs.billing_id, tacs.trading_participant_settlement_id, tacs.invoice_id, " & vbNewLine _
                    & "tacs.net_seller_buyer_tag, tacs.transaction_number, tacs.due_date, tacs.date, tacs.non_vatable, tacs.zero_rated, " & vbNewLine _
                    & "tacs.vatable_sales, tacs.vatable_purchases, tacs.zero_rated_sales, tacs.zero_rated_ecozone_sales, tacs.zero_rated_purchases, tacs.zero_rated_ecozone_purchases, " & vbNewLine _
                    & "tacs.vat_on_sales, tacs.vat_on_purchases, tacs.tta, tacs.nss_flowback, tacs.energy_witholding_tax_sales, tacs.energy_witholding_tax_purchases, " & vbNewLine _
                    & "tacs.net_energy_qty, tacs.gmr, tacs.market_fees_rate, tacs.wht, tacs.ith, tacs.region_group, tacs.remarks, tacs.genx_flowback " & vbNewLine _
                    & "FROM settlement.txn_alloc_cover_summary tacs " & vbNewLine _
                    & "JOIN meterprocess.txn_billing_period bp ON bp.start_date = tacs.billing_period_start " & vbNewLine _
                    & "And bp.end_date = tacs.billing_period_end " & vbNewLine _
                    & "WHERE tacs.due_date = to_date('" & duedate & "', 'mm/dd/yyyy') and bp.billing_period = " & bp & " " & vbNewLine _
                    & "and tacs.stl_run = '" & stlRun & "' and tacs.group_id = '" & groupID & "' " & vbNewLine _
                    & "ORDER BY tacs.due_date, bp.billing_period, tacs.stl_run, tacs.group_id"
            End If

            report = Me._NpgDataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.GetWTACoverySummaryList(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function


    Private Function GetWTACoverySummaryList(ByVal dr As IDataReader) As List(Of WESMBillAllocCoverSummary)
        Dim result As New List(Of WESMBillAllocCoverSummary)

        Try
            While dr.Read()
                Dim item As New WESMBillAllocCoverSummary

                With dr
                    item.SummaryId = CLng(Trim(.Item("summary_id")))
                    item.STLRun = CStr(Trim(.Item("STL_RUN")))
                    item.BillingPeriod = CInt(Trim(.Item("BILLING_PERIOD")))
                    item.StlID = CStr(Trim(.Item("trading_participant_settlement_id"))).ToUpper
                    item.BillingID = CStr(Trim(.Item("billing_id"))).ToUpper
                    item.WHT = CStr(Trim(.Item("wht")))
                    item.ITH = CStr(Trim(.Item("ith")))
                    item.NonVatableTag = CStr(Trim(.Item("non_vatable")))
                    item.ZeroRatedTag = CStr(Trim(.Item("zero_rated")))
                    item.NetSellerBuyerTag = CStr(Trim(.Item("net_seller_buyer_tag")))
                    item.TransactionNo = CStr(Trim(.Item("transaction_number")))
                    item.TransactionDate = CDate(Trim(.Item("date")))
                    item.DueDate = CDate(Trim(.Item("due_date")))
                    item.VatableSales = Math.Round(CDec(Trim(.Item("vatable_sales"))), 2)
                    item.ZeroRatedSales = Math.Round(CDec(Trim(.Item("zero_rated_sales"))), 2)
                    item.ZeroRatedEcoZoneSales = Math.Round(CDec(Trim(.Item("zero_rated_ecozone_sales"))), 2)
                    item.VatablePurchases = Math.Round(CDec(Trim(.Item("vatable_purchases"))), 2)
                    item.ZeroRatedPurchases = Math.Round(CDec(Trim(.Item("zero_rated_purchases"))), 2)
                    item.ZeroRatedEcoZonePurchases = Math.Round(CDec(Trim(.Item("zero_rated_ecozone_purchases"))), 2)
                    item.NSSFlowBack = Math.Round(CDec(Trim(.Item("nss_flowback"))), 2)
                    item.VatOnSales = Math.Round(CDec(Trim(.Item("vat_on_sales"))), 2)
                    item.VatOnPurchases = Math.Round(CDec(Trim(.Item("vat_on_purchases"))), 2)
                    item.EWTSales = Math.Round(CDec(Trim(.Item("energy_witholding_tax_sales"))), 2)
                    item.EWTPurchases = Math.Round(CDec(Trim(.Item("energy_witholding_tax_purchases"))), 2)
                    item.GMR = CDec(Trim(.Item("gmr")))
                    item.SpotQty = CDec(Trim(.Item("net_energy_qty")))
                    item.MarketFeesRate = CDec(Trim(.Item("MARKET_FEES_RATE")))
                    item.Remarks = CStr(Trim(.Item("REMARKS").ToString()))
                    item.GenXAmount = Math.Round(CDec(Trim(.Item("genx_flowback"))), 2)
                    result.Add(item)
                End With
            End While
            result.TrimExcess()

        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return result
    End Function
#End Region

#Region "Get WESM Transaction Allocation"
    Public Function GetWTADetailsList(ByVal startSummaryID As Long, ByVal endSummaryId As Long) As List(Of WESMBillAllocDisaggDetails)
        Dim result As New List(Of WESMBillAllocDisaggDetails)
        Dim report As New DataReport
        Dim SQL As String

        Try
            SQL = "SELECT txn_allocation_id, summary_id, billing_period_start, billing_period_end, billing_id, tp_billing_id, trading_participant_name, " & vbNewLine _
                    & "trading_participant_short_name, facility_type, wht, ith, non_vatable, zero_rated, vatable_sales, zero_rated_sales, zero_rated_ecozone_sales, vat_on_sales, " & vbNewLine _
                    & "vatable_purchases, zero_rated_purchases, zero_rated_ecozone_purchases, vat_on_purchases, tta, nss_flowback, trading_participant_settlement_id, " & vbNewLine _
                    & "calculation_remarks, energy_witholding_tax_sales, energy_witholding_tax_purchases, created_by, created_date, modified_by, modified_date, version, " & vbNewLine _
                    & "vatable_sales_raw, vatable_purchases_raw, zero_rated_sales_raw, zero_rated_purchases_raw, zero_rated_ecozone_sales_raw, zero_rated_ecozone_purchases_raw, " & vbNewLine _
                    & "tta_raw, nss_flowback_raw, vat_on_sales_raw, vat_on_purchases_raw, energy_witholding_tax_sales_raw, energy_witholding_tax_purchases_raw " & vbNewLine _
                    & "FROM settlement.txn_alloc_txn_allocation WHERE summary_id >= " & startSummaryID & " AND summary_id <=" & endSummaryId & " ORDER BY summary_id"

            report = Me._NpgDataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.GetWTADetailsList(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function


    Private Function GetWTADetailsList(ByVal dr As IDataReader) As List(Of WESMBillAllocDisaggDetails)
        Dim result As New List(Of WESMBillAllocDisaggDetails)
        Try
            While dr.Read()
                Dim item As New WESMBillAllocDisaggDetails
                With dr
                    Dim grossSales As Decimal = CDec(Trim(.Item("vatable_sales"))) + CDec(Trim(.Item("zero_rated_sales"))) + CDec(Trim(.Item("zero_rated_ecozone_sales")))
                    Dim grossPurchases As Decimal = CDec(Trim(.Item("vatable_purchases"))) + CDec(Trim(.Item("zero_rated_purchases"))) + CDec(Trim(.Item("zero_rated_ecozone_purchases")))
                    If grossSales <> 0 And grossPurchases <> 0 Then
                        item.STLID = CStr(Trim(.Item("trading_participant_settlement_id"))).ToUpper
                        item.BillingID = CStr(Trim(.Item("tp_billing_id"))).ToUpper
                        item.FacilityType = CStr(Trim(.Item("facility_type")))
                        item.WHTTag = CStr(Trim(.Item("wht")))
                        item.ITHTag = CStr(Trim(.Item("ith")))
                        item.NonVatableTag = CStr(Trim(.Item("non_vatable")))
                        item.ZeroRatedTag = CStr(Trim(.Item("zero_rated")))
                        item.NetSellerBuyerTag = "BUYER"
                        item.VatableSales = Math.Round(CDec(Trim(.Item("vatable_sales"))), 2)
                        item.ZeroRatedSales = Math.Round(CDec(Trim(.Item("zero_rated_sales"))), 2)
                        item.ZeroRatedEcoZoneSales = Math.Round(CDec(Trim(.Item("zero_rated_ecozone_sales"))), 2)
                        item.VatablePurchases = 0
                        item.ZeroRatedPurchases = 0
                        item.ZeroRatedEcoZonePurchases = 0
                        item.VatOnSales = Math.Round(CDec(Trim(.Item("vat_on_sales"))), 2)
                        item.VatOnPurchases = 0
                        item.EWT = Math.Round(CDec(Trim(.Item("energy_witholding_tax_sales"))), 2)
                        item.SummaryId = CLng(Trim(.Item("summary_id")))
                        result.Add(item)

                        item = New WESMBillAllocDisaggDetails
                        item.STLID = CStr(Trim(.Item("trading_participant_settlement_id"))).ToUpper
                        item.BillingID = CStr(Trim(.Item("tp_billing_id"))).ToUpper
                        item.FacilityType = CStr(Trim(.Item("facility_type")))
                        item.WHTTag = CStr(Trim(.Item("wht")))
                        item.ITHTag = CStr(Trim(.Item("ith")))
                        item.NonVatableTag = CStr(Trim(.Item("non_vatable")))
                        item.ZeroRatedTag = CStr(Trim(.Item("zero_rated")))
                        item.NetSellerBuyerTag = "SELLER"
                        item.VatableSales = 0
                        item.ZeroRatedSales = 0
                        item.ZeroRatedEcoZoneSales = 0
                        item.VatablePurchases = Math.Round(CDec(Trim(.Item("vatable_purchases"))), 2)
                        item.ZeroRatedPurchases = Math.Round(CDec(Trim(.Item("zero_rated_purchases"))), 2)
                        item.ZeroRatedEcoZonePurchases = Math.Round(CDec(Trim(.Item("zero_rated_ecozone_purchases"))), 2)
                        item.VatOnSales = 0
                        item.VatOnPurchases = Math.Round(CDec(Trim(.Item("vat_on_purchases"))), 2)
                        item.EWT = Math.Round(CDec(Trim(.Item("energy_witholding_tax_purchases"))), 2)
                        item.SummaryId = CLng(Trim(.Item("summary_id")))
                        result.Add(item)

                    ElseIf grossSales <> 0 And grossPurchases = 0 Then
                        item.STLID = CStr(Trim(.Item("trading_participant_settlement_id"))).ToUpper
                        item.BillingID = CStr(Trim(.Item("tp_billing_id"))).ToUpper
                        item.FacilityType = CStr(Trim(.Item("facility_type")))
                        item.WHTTag = CStr(Trim(.Item("wht")))
                        item.ITHTag = CStr(Trim(.Item("ith")))
                        item.NonVatableTag = CStr(Trim(.Item("non_vatable")))
                        item.ZeroRatedTag = CStr(Trim(.Item("zero_rated")))
                        item.NetSellerBuyerTag = "BUYER"
                        item.VatableSales = Math.Round(CDec(Trim(.Item("vatable_sales"))), 2)
                        item.ZeroRatedSales = Math.Round(CDec(Trim(.Item("zero_rated_sales"))), 2)
                        item.ZeroRatedEcoZoneSales = Math.Round(CDec(Trim(.Item("zero_rated_ecozone_sales"))), 2)
                        item.VatablePurchases = 0
                        item.ZeroRatedPurchases = 0
                        item.ZeroRatedEcoZonePurchases = 0
                        item.VatOnSales = Math.Round(CDec(Trim(.Item("vat_on_sales"))), 2)
                        item.VatOnPurchases = 0
                        item.EWT = Math.Round(CDec(Trim(.Item("energy_witholding_tax_sales"))), 2)
                        item.SummaryId = CLng(Trim(.Item("summary_id")))
                        result.Add(item)
                    ElseIf grossSales = 0 And grossPurchases <> 0 Then
                        item.STLID = CStr(Trim(.Item("trading_participant_settlement_id"))).ToUpper
                        item.BillingID = CStr(Trim(.Item("tp_billing_id"))).ToUpper
                        item.FacilityType = CStr(Trim(.Item("facility_type")))
                        item.WHTTag = CStr(Trim(.Item("wht")))
                        item.ITHTag = CStr(Trim(.Item("ith")))
                        item.NonVatableTag = CStr(Trim(.Item("non_vatable")))
                        item.ZeroRatedTag = CStr(Trim(.Item("zero_rated")))
                        item.NetSellerBuyerTag = "SELLER"
                        item.VatableSales = 0
                        item.ZeroRatedSales = 0
                        item.ZeroRatedEcoZoneSales = 0
                        item.VatablePurchases = Math.Round(CDec(Trim(.Item("vatable_purchases"))), 2)
                        item.ZeroRatedPurchases = Math.Round(CDec(Trim(.Item("zero_rated_purchases"))), 2)
                        item.ZeroRatedEcoZonePurchases = Math.Round(CDec(Trim(.Item("zero_rated_ecozone_purchases"))), 2)
                        item.VatOnSales = 0
                        item.VatOnPurchases = Math.Round(CDec(Trim(.Item("vat_on_purchases"))), 2)
                        item.EWT = Math.Round(CDec(Trim(.Item("energy_witholding_tax_purchases"))), 2)
                        item.SummaryId = CLng(Trim(.Item("summary_id")))
                        result.Add(item)
                    End If
                End With
            End While
            result.TrimExcess()

        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return result
    End Function
#End Region

#Region "Get WESM Transaction Allocation List For Upload"
    Public Function GetWTAList(ByVal duedate As Date) As List(Of ImportWTAFromCRSS)
        Dim result As New List(Of ImportWTAFromCRSS)
        Dim report As New DataReport
        Dim SQL As String

        Try

            If AMModule.RegionType = "LV" Then
                SQL = "SELECT bp.billing_period, tacs.stl_run, tacs.group_id, tacs.due_date, tacs.date, " & vbNewLine _
                    & "SUM(tacs.vatable_sales + tacs.zero_rated_sales + tacs.zero_rated_ecozone_sales) as Net_Sales, " & vbNewLine _
                    & "SUM(tacs.vatable_purchases + tacs.zero_rated_purchases + tacs.zero_rated_ecozone_purchases) as Net_Purchases, " & vbNewLine _
                    & "SUM(tacs.vat_on_sales) as Net_VAT_On_Sales, SUM(tacs.vat_on_purchases) as Net_VAT_On_Purchases, " & vbNewLine _
                    & "SUM(tacs.nss_flowback) as Total_NSS_Flowback, SUM(tacs.energy_witholding_tax_sales) as EWT_Sales, " & vbNewLine _
                    & "SUM(tacs.energy_witholding_tax_purchases) as EWT_Purchases, " & vbNewLine _
                    & "SUM(tacs.net_energy_qty) as Total_Energy_QTY, SUM(tacs.genx_flowback) as Total_Genx_Flowback, " & vbNewLine _
                    & "tacs.gmr, tacs.market_fees_rate, tacs.remarks " & vbNewLine _
                    & "FROM settlement.txn_alloc_cover_summary tacs " & vbNewLine _
                    & "JOIN meterprocess.txn_billing_period bp ON bp.start_date = tacs.billing_period_start " & vbNewLine _
                    & "AND bp.end_date = tacs.billing_period_end " & vbNewLine _
                    & "WHERE tacs.stl_run Like 'F%' and tacs.due_date = to_date('" & duedate & "', 'mm/dd/yyyy') and region_group = 'LUZON_VISAYAS' " & vbNewLine _
                    & "GROUP BY bp.billing_period, tacs.stl_run, tacs.group_id, " & vbNewLine _
                    & "tacs.due_date, tacs.date, tacs.gmr, tacs.market_fees_rate, tacs.remarks " & vbNewLine _
                    & "ORDER BY tacs.due_date, bp.billing_period, tacs.stl_run"
            ElseIf AMModule.RegionType = "M" Then
                SQL = "SELECT bp.billing_period, tacs.stl_run, tacs.group_id, tacs.due_date, tacs.date, " & vbNewLine _
                    & "SUM(tacs.vatable_sales + tacs.zero_rated_sales + tacs.zero_rated_ecozone_sales) as Net_Sales, " & vbNewLine _
                    & "SUM(tacs.vatable_purchases + tacs.zero_rated_purchases + tacs.zero_rated_ecozone_purchases) as Net_Purchases, " & vbNewLine _
                    & "SUM(tacs.vat_on_sales) as Net_VAT_On_Sales, SUM(tacs.vat_on_purchases) as Net_VAT_On_Purchases, " & vbNewLine _
                    & "SUM(tacs.nss_flowback) as Total_NSS_Flowback, SUM(tacs.energy_witholding_tax_sales) as EWT_Sales, " & vbNewLine _
                    & "SUM(tacs.energy_witholding_tax_purchases) as EWT_Purchases, " & vbNewLine _
                    & "SUM(tacs.net_energy_qty) as Total_Energy_QTY, SUM(tacs.genx_flowback) as Total_Genx_Flowback, " & vbNewLine _
                    & "tacs.gmr, tacs.market_fees_rate, tacs.remarks " & vbNewLine _
                    & "FROM settlement.txn_alloc_cover_summary tacs " & vbNewLine _
                    & "JOIN meterprocess.txn_billing_period bp ON bp.start_date = tacs.billing_period_start " & vbNewLine _
                    & "AND bp.end_date = tacs.billing_period_end " & vbNewLine _
                    & "WHERE tacs.stl_run Like 'F%' and tacs.due_date = to_date('" & duedate & "', 'mm/dd/yyyy') and region_group = 'MINDANAO' " & vbNewLine _
                    & "GROUP BY bp.billing_period, tacs.stl_run, tacs.group_id, " & vbNewLine _
                    & "tacs.due_date, tacs.date, tacs.gmr, tacs.market_fees_rate, tacs.remarks " & vbNewLine _
                    & "ORDER BY tacs.due_date, bp.billing_period, tacs.stl_run"
            Else
                SQL = "SELECT bp.billing_period, tacs.stl_run, tacs.group_id, tacs.due_date, tacs.date, " & vbNewLine _
                   & "SUM(tacs.vatable_sales + tacs.zero_rated_sales + tacs.zero_rated_ecozone_sales) as Net_Sales, " & vbNewLine _
                   & "SUM(tacs.vatable_purchases + tacs.zero_rated_purchases + tacs.zero_rated_ecozone_purchases) as Net_Purchases, " & vbNewLine _
                   & "SUM(tacs.vat_on_sales) as Net_VAT_On_Sales, SUM(tacs.vat_on_purchases) as Net_VAT_On_Purchases, " & vbNewLine _
                   & "SUM(tacs.nss_flowback) as Total_NSS_Flowback, SUM(tacs.energy_witholding_tax_sales) as EWT_Sales, " & vbNewLine _
                   & "SUM(tacs.energy_witholding_tax_purchases) as EWT_Purchases, " & vbNewLine _
                   & "SUM(tacs.net_energy_qty) as Total_Energy_QTY, SUM(tacs.genx_flowback) as Total_Genx_Flowback, " & vbNewLine _
                   & "tacs.gmr, tacs.market_fees_rate, tacs.remarks " & vbNewLine _
                   & "FROM settlement.txn_alloc_cover_summary tacs " & vbNewLine _
                   & "JOIN meterprocess.txn_billing_period bp ON bp.start_date = tacs.billing_period_start " & vbNewLine _
                   & "AND bp.end_date = tacs.billing_period_end " & vbNewLine _
                   & "WHERE tacs.stl_run Like 'F%' and tacs.due_date = to_date('" & duedate & "', 'mm/dd/yyyy') " & vbNewLine _
                   & "GROUP BY bp.billing_period, tacs.stl_run, tacs.group_id, " & vbNewLine _
                   & "tacs.due_date, tacs.date, tacs.gmr, tacs.market_fees_rate, tacs.remarks " & vbNewLine _
                   & "ORDER BY tacs.due_date, bp.billing_period, tacs.stl_run"
            End If

            report = Me._NpgDataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.GetWTAList(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function

    Private Function GetWTAList(ByVal dr As IDataReader) As List(Of ImportWTAFromCRSS)
        Dim result As New List(Of ImportWTAFromCRSS)

        Try
            While dr.Read()
                Dim item As New ImportWTAFromCRSS
                With dr
                    item.SettlementRun = CStr(Trim(.Item("STL_RUN")))
                    item.BillingPeriod = CInt(Trim(.Item("BILLING_PERIOD")))
                    item.GroupID = CLng(Trim(.Item("group_id")))
                    item.TotalNetSales = CDec(Trim(.Item("Net_Sales"))) + CDec(Trim(.Item("Net_VAT_On_Sales")))
                    item.TotalNetPurchases = CDec(Trim(.Item("Net_Purchases"))) + CDec(Trim(.Item("Net_VAT_On_Purchases")))
                    item.TotalEWTSales = CDec(Trim(.Item("EWT_Sales")))
                    item.TotalEWTPurchases = CDec(Trim(.Item("EWT_Purchases")))
                    item.Remarks = CStr(Trim(.Item("remarks")))
                    result.Add(item)
                End With
            End While
            result.TrimExcess()

        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return result
    End Function
#End Region

    Public Sub InitializeData()
        Me._NewWTADueDateList = Me._WBillHelper.GetNewWESMInvoicesDueDate()
    End Sub

    Public Sub FillTheDGV(ByVal duedate As Date)
        Dim iniImportWTAFromCRSSList As List(Of ImportWTAFromCRSS) = Me.GetWTAList(duedate)
        Dim objImportWESMBillList As New List(Of ImportWTAFromCRSS)
        For Each item In iniImportWTAFromCRSSList
            Dim newWTA As New ImportWTAFromCRSS
            With newWTA
                .BillingPeriod = item.BillingPeriod
                .SettlementRun = item.SettlementRun
                .GroupID = item.GroupID
                .Remarks = item.Remarks
                .SettlementRun = item.SettlementRun
                .TotalNetSales = item.TotalNetSales
                .TotalNetPurchases = item.TotalNetPurchases
                .TotalEWTSales = item.TotalEWTSales
                .TotalEWTPurchases = item.TotalEWTPurchases
                .Remarks = item.Remarks
                '.WTACoverSummaryList = FillWTACoverSummaryList(item.BillingPeriod, item.SettlementRun, duedate, item.GroupID)
                '.WESMBillList = FillWESMBillsList(.WTACoverSummaryList)
                '.ListOfError = ValidateWTACoverSummaryList(.WTACoverSummaryList)
            End With
            objImportWESMBillList.Add(newWTA)
        Next
        Me.newImportWTAFromCRSSList = iniImportWTAFromCRSSList
    End Sub

    Public Sub FetchSelectedData(ByVal bp As Integer, ByVal stlRun As String, ByVal duedate As Date, ByVal groupID As String)
        Dim getImportWTAFromCRSS As ImportWTAFromCRSS = Me.newImportWTAFromCRSSList.Where(Function(x) x.BillingPeriod = bp And x.SettlementRun = stlRun And x.GroupID = groupID).FirstOrDefault
        With getImportWTAFromCRSS
            .WTACoverSummaryList = FillWTACoverSummaryList(bp, stlRun, duedate, groupID)
            .WESMBillList = FillWESMBillsList(.WTACoverSummaryList)
            .ListOfError = ValidateWTACoverSummaryList(.WTACoverSummaryList)
        End With
    End Sub

    Public Function FillWTACoverSummaryList(ByVal bp As Integer, ByVal stlRun As String, ByVal duedate As Date, ByVal groupID As String) As List(Of WESMBillAllocCoverSummary)
        Dim ret As New List(Of WESMBillAllocCoverSummary)
        Dim fetchWTACoverSummaryList As New List(Of WESMBillAllocCoverSummary)
        Dim fetchWTADetailsList As New List(Of WESMBillAllocDisaggDetails)
        fetchWTACoverSummaryList = Me.GetWTACoverySummaryList(bp, stlRun, duedate, groupID)

        Dim getMinSummaryID As Long = fetchWTACoverSummaryList.Min(Function(x) x.SummaryId)
        Dim getMaxSummaryID As Long = fetchWTACoverSummaryList.Max(Function(x) x.SummaryId)
        fetchWTADetailsList = Me.GetWTADetailsList(getMinSummaryID, getMaxSummaryID)

        Dim summaryNo As Integer = 0
        For Each item In fetchWTACoverSummaryList
            Dim changeSTLID As String = Me.ParentChangeID.
                                        Where(Function(x) x.BillingPeriod = item.BillingPeriod And x.ParentParticipants.IDNumber = item.StlID And x.ChildParticipants.IDNumber = item.BillingID).
                                        Select(Function(y) y.NewParentParticipants.IDNumber).FirstOrDefault
            If item.GrossSale <> 0 And item.GrossPurchase <> 0 Then
                summaryNo += 1
                Dim wesmAllocCoverSummarySales As New WESMBillAllocCoverSummary
                With wesmAllocCoverSummarySales
                    .SummaryId = summaryNo
                    .BillingPeriod = item.BillingPeriod
                    .STLRun = item.STLRun
                    If changeSTLID Is Nothing Then
                        .StlID = item.StlID
                    Else
                        .StlID = changeSTLID.Replace("_" & AMModule.FITParticipantCode.ToString, "")
                    End If
                    .BillingID = item.BillingID
                    .NonVatableTag = item.NonVatableTag
                    .ZeroRatedTag = item.ZeroRatedTag
                    .WHT = item.WHT
                    .ITH = item.ITH
                    .NetSellerBuyerTag = "NET_SELLER"
                    .TransactionDate = item.TransactionDate
                    .DueDate = item.DueDate
                    .TransactionNo = item.TransactionNo & "S"
                    .VatableSales = item.VatableSales
                    .ZeroRatedSales = item.ZeroRatedSales
                    .ZeroRatedEcoZoneSales = item.ZeroRatedEcoZoneSales
                    .VatablePurchases = 0
                    .ZeroRatedPurchases = 0
                    .VatOnSales = item.VatOnSales
                    .VatOnPurchases = 0
                    .NSSFlowBack = item.NSSFlowBack
                    .EWTSales = item.EWTSales
                    .EWTPurchases = 0
                    .GMR = item.GMR
                    .SpotQty = item.SpotQty
                    .MarketFeesRate = item.MarketFeesRate
                    .Remarks = item.Remarks
                    .GenXAmount = item.GenXAmount
                    Dim getWTADetailsList As List(Of WESMBillAllocDisaggDetails) = fetchWTADetailsList.Where(Function(x) x.SummaryId = item.SummaryId And x.GrossSale <> 0).ToList
                    Dim newWTADetailsList As New List(Of WESMBillAllocDisaggDetails)
                    For Each ditem In getWTADetailsList
                        Dim wesmAllocDissagDetails As New WESMBillAllocDisaggDetails
                        With wesmAllocDissagDetails
                            If changeSTLID Is Nothing Then
                                .STLID = ditem.STLID
                            Else
                                .STLID = changeSTLID.Replace("_" & AMModule.FITParticipantCode.ToString, "")
                            End If
                            .BillingID = ditem.BillingID
                            .FacilityType = ditem.FacilityType
                            .WHTTag = ditem.WHTTag
                            .ITHTag = ditem.ITHTag
                            .NonVatableTag = ditem.NonVatableTag
                            .ZeroRatedTag = ditem.ZeroRatedTag
                            .NetSellerBuyerTag = ditem.NetSellerBuyerTag
                            .VatableSales = ditem.VatableSales
                            .ZeroRatedSales = ditem.ZeroRatedSales
                            .ZeroRatedEcoZoneSales = ditem.ZeroRatedEcoZoneSales
                            .VatablePurchases = 0
                            .ZeroRatedPurchases = 0
                            .ZeroRatedEcoZonePurchases = 0
                            .VatOnSales = ditem.VatOnSales
                            .VatOnPurchases = 0
                            .EWT = ditem.EWT
                        End With
                        newWTADetailsList.Add(wesmAllocDissagDetails)
                    Next
                    .ListWBAllocDisDetails = newWTADetailsList
                End With

                ret.Add(wesmAllocCoverSummarySales)

                summaryNo += 1
                Dim wesmAllocCoverSummaryPurchases As New WESMBillAllocCoverSummary
                With wesmAllocCoverSummaryPurchases
                    .SummaryId = summaryNo
                    .BillingPeriod = item.BillingPeriod
                    .STLRun = item.STLRun
                    If changeSTLID Is Nothing Then
                        .StlID = item.StlID
                    Else
                        .StlID = changeSTLID.Replace("_" & AMModule.FITParticipantCode.ToString, "")
                    End If
                    .BillingID = item.BillingID
                    .NonVatableTag = item.NonVatableTag
                    .ZeroRatedTag = item.ZeroRatedTag
                    .WHT = item.WHT
                    .ITH = item.ITH
                    .NetSellerBuyerTag = "NET_BUYER"
                    .TransactionDate = item.TransactionDate
                    .DueDate = item.DueDate
                    .TransactionNo = item.TransactionNo & "P"
                    .VatableSales = 0
                    .ZeroRatedSales = 0
                    .ZeroRatedEcoZoneSales = 0
                    .VatablePurchases = item.VatablePurchases
                    .ZeroRatedPurchases = item.ZeroRatedPurchases
                    .ZeroRatedEcoZonePurchases = item.ZeroRatedEcoZonePurchases
                    .VatOnSales = 0
                    .VatOnPurchases = item.VatOnPurchases
                    .NSSFlowBack = item.NSSFlowBack
                    .EWTSales = 0
                    .EWTPurchases = item.EWTPurchases
                    .GMR = item.GMR
                    .SpotQty = item.SpotQty
                    .MarketFeesRate = item.MarketFeesRate
                    .Remarks = item.Remarks
                    .GenXAmount = item.GenXAmount
                    Dim getWTADetailsList As List(Of WESMBillAllocDisaggDetails) = fetchWTADetailsList.Where(Function(x) x.SummaryId = item.SummaryId And x.GrossPurchase <> 0).ToList
                    Dim newWTADetailsList As New List(Of WESMBillAllocDisaggDetails)
                    For Each ditem In getWTADetailsList
                        Dim wesmAllocDissagDetails As New WESMBillAllocDisaggDetails
                        With wesmAllocDissagDetails
                            If changeSTLID Is Nothing Then
                                .STLID = ditem.STLID
                            Else
                                .STLID = changeSTLID.Replace("_" & AMModule.FITParticipantCode.ToString, "")
                            End If
                            .BillingID = ditem.BillingID
                            .FacilityType = ditem.FacilityType
                            .WHTTag = ditem.WHTTag
                            .ITHTag = ditem.ITHTag
                            .NonVatableTag = ditem.NonVatableTag
                            .ZeroRatedTag = ditem.ZeroRatedTag
                            .NetSellerBuyerTag = ditem.NetSellerBuyerTag
                            .VatableSales = 0
                            .ZeroRatedSales = 0
                            .ZeroRatedEcoZoneSales = 0
                            .VatablePurchases = ditem.VatablePurchases
                            .ZeroRatedPurchases = ditem.ZeroRatedPurchases
                            .ZeroRatedEcoZonePurchases = ditem.ZeroRatedEcoZonePurchases
                            .VatOnSales = 0
                            .VatOnPurchases = ditem.VatOnPurchases
                            .EWT = ditem.EWT
                        End With
                        newWTADetailsList.Add(wesmAllocDissagDetails)
                    Next
                    .ListWBAllocDisDetails = newWTADetailsList
                End With
                ret.Add(wesmAllocCoverSummaryPurchases)

            ElseIf item.GrossSale <> 0 And item.GrossPurchase = 0 Then
                summaryNo += 1
                Dim wesmAllocCoverSummarySales As New WESMBillAllocCoverSummary
                With wesmAllocCoverSummarySales
                    .SummaryId = summaryNo
                    .BillingPeriod = item.BillingPeriod
                    .STLRun = item.STLRun
                    If changeSTLID Is Nothing Then
                        .StlID = item.StlID
                    Else
                        .StlID = changeSTLID.Replace("_" & AMModule.FITParticipantCode.ToString, "")
                    End If
                    .BillingID = item.BillingID
                    .NonVatableTag = item.NonVatableTag
                    .ZeroRatedTag = item.ZeroRatedTag
                    .WHT = item.WHT
                    .ITH = item.ITH
                    .NetSellerBuyerTag = item.NetSellerBuyerTag
                    .TransactionDate = item.TransactionDate
                    .DueDate = item.DueDate
                    .TransactionNo = item.TransactionNo
                    .VatableSales = item.VatableSales
                    .ZeroRatedSales = item.ZeroRatedSales
                    .ZeroRatedEcoZoneSales = item.ZeroRatedEcoZoneSales
                    .VatablePurchases = 0
                    .ZeroRatedPurchases = 0
                    .VatOnSales = item.VatOnSales
                    .VatOnPurchases = 0
                    .NSSFlowBack = item.NSSFlowBack
                    .EWTSales = item.EWTSales
                    .EWTPurchases = 0
                    .GMR = item.GMR
                    .SpotQty = item.SpotQty
                    .MarketFeesRate = item.MarketFeesRate
                    .Remarks = item.Remarks
                    .GenXAmount = item.GenXAmount
                    Dim getWTADetailsList As List(Of WESMBillAllocDisaggDetails) = fetchWTADetailsList.Where(Function(x) x.SummaryId = item.SummaryId And x.GrossSale <> 0).ToList
                    Dim newWTADetailsList As New List(Of WESMBillAllocDisaggDetails)
                    For Each ditem In getWTADetailsList
                        Dim wesmAllocDissagDetails As New WESMBillAllocDisaggDetails
                        With wesmAllocDissagDetails
                            If changeSTLID Is Nothing Then
                                .STLID = ditem.STLID
                            Else
                                .STLID = changeSTLID.Replace("_" & AMModule.FITParticipantCode.ToString, "")
                            End If
                            .BillingID = ditem.BillingID
                            .FacilityType = ditem.FacilityType
                            .WHTTag = ditem.WHTTag
                            .ITHTag = ditem.ITHTag
                            .NonVatableTag = ditem.NonVatableTag
                            .ZeroRatedTag = ditem.ZeroRatedTag
                            .NetSellerBuyerTag = ditem.NetSellerBuyerTag
                            .VatableSales = ditem.VatableSales
                            .ZeroRatedSales = ditem.ZeroRatedSales
                            .ZeroRatedEcoZoneSales = ditem.ZeroRatedEcoZoneSales
                            .VatablePurchases = 0
                            .ZeroRatedPurchases = 0
                            .ZeroRatedEcoZonePurchases = 0
                            .VatOnSales = ditem.VatOnSales
                            .VatOnPurchases = 0
                            .EWT = ditem.EWT
                        End With
                        newWTADetailsList.Add(wesmAllocDissagDetails)
                    Next
                    .ListWBAllocDisDetails = newWTADetailsList
                End With
                ret.Add(wesmAllocCoverSummarySales)

            ElseIf item.GrossPurchase <> 0 And item.GrossSale = 0 Then
                summaryNo += 1
                Dim wesmAllocCoverSummaryPurchases As New WESMBillAllocCoverSummary
                With wesmAllocCoverSummaryPurchases
                    .SummaryId = summaryNo
                    .BillingPeriod = item.BillingPeriod
                    .STLRun = item.STLRun
                    If changeSTLID Is Nothing Then
                        .StlID = item.StlID
                    Else
                        .StlID = changeSTLID.Replace("_" & AMModule.FITParticipantCode.ToString, "")
                    End If
                    .BillingID = item.BillingID
                    .NonVatableTag = item.NonVatableTag
                    .ZeroRatedTag = item.ZeroRatedTag
                    .WHT = item.WHT
                    .ITH = item.ITH
                    .NetSellerBuyerTag = item.NetSellerBuyerTag
                    .TransactionDate = item.TransactionDate
                    .DueDate = item.DueDate
                    .TransactionNo = item.TransactionNo
                    .VatableSales = 0
                    .ZeroRatedSales = 0
                    .ZeroRatedEcoZoneSales = 0
                    .VatablePurchases = item.VatablePurchases
                    .ZeroRatedPurchases = item.ZeroRatedPurchases
                    .ZeroRatedEcoZonePurchases = item.ZeroRatedEcoZonePurchases
                    .VatOnSales = 0
                    .VatOnPurchases = item.VatOnPurchases
                    .NSSFlowBack = item.NSSFlowBack
                    .EWTSales = 0
                    .EWTPurchases = item.EWTPurchases
                    .GMR = item.GMR
                    .SpotQty = item.SpotQty
                    .MarketFeesRate = item.MarketFeesRate
                    .Remarks = item.Remarks
                    .GenXAmount = item.GenXAmount
                    Dim getWTADetailsList As List(Of WESMBillAllocDisaggDetails) = fetchWTADetailsList.Where(Function(x) x.SummaryId = item.SummaryId And x.GrossPurchase <> 0).ToList
                    Dim newWTADetailsList As New List(Of WESMBillAllocDisaggDetails)
                    For Each ditem In getWTADetailsList
                        Dim wesmAllocDissagDetails As New WESMBillAllocDisaggDetails
                        With wesmAllocDissagDetails
                            If changeSTLID Is Nothing Then
                                .STLID = ditem.STLID
                            Else
                                .STLID = changeSTLID.Replace("_" & AMModule.FITParticipantCode.ToString, "")
                            End If
                            .BillingID = ditem.BillingID
                            .FacilityType = ditem.FacilityType
                            .WHTTag = ditem.WHTTag
                            .ITHTag = ditem.ITHTag
                            .NonVatableTag = ditem.NonVatableTag
                            .ZeroRatedTag = ditem.ZeroRatedTag
                            .NetSellerBuyerTag = ditem.NetSellerBuyerTag
                            .VatableSales = 0
                            .ZeroRatedSales = 0
                            .ZeroRatedEcoZoneSales = 0
                            .VatablePurchases = ditem.VatablePurchases
                            .ZeroRatedPurchases = ditem.ZeroRatedPurchases
                            .ZeroRatedEcoZonePurchases = ditem.ZeroRatedEcoZonePurchases
                            .VatOnSales = 0
                            .VatOnPurchases = ditem.VatOnPurchases
                            .EWT = ditem.EWT
                        End With
                        newWTADetailsList.Add(wesmAllocDissagDetails)
                    Next
                    .ListWBAllocDisDetails = newWTADetailsList
                End With
                ret.Add(wesmAllocCoverSummaryPurchases)
            End If
        Next
        Return ret
    End Function

    Public Function FillWESMBillsList(ByVal newWTACoverSummaryList As List(Of WESMBillAllocCoverSummary)) As List(Of WESMBill)
        Dim ret As New List(Of WESMBill)
        For Each item In newWTACoverSummaryList
            Dim changeSTLID As String = Me.ParentChangeID.
                                        Where(Function(x) x.BillingPeriod = item.BillingPeriod _
                                                        And x.ParentParticipants.IDNumber = item.StlID _
                                                        And x.ChildParticipants.IDNumber = item.BillingID _
                                                        And x.Status = EnumStatus.Active).
                                        Select(Function(y) y.NewParentParticipants.IDNumber).FirstOrDefault

            Dim wesmBillItemEnergy As New WESMBill
            With wesmBillItemEnergy
                .BillingPeriod = item.BillingPeriod
                .SettlementRun = item.STLRun
                If changeSTLID Is Nothing Then
                    .IDNumber = item.StlID
                Else
                    .IDNumber = changeSTLID
                End If
                .ChargeType = EnumChargeType.E
                .RegistrationID = item.BillingID
                .InvoiceDate = item.TransactionDate
                .InvoiceNumber = item.TransactionNo
                .DueDate = item.DueDate
                .Amount = Math.Round(item.VatableSales + item.ZeroRatedSales + item.ZeroRatedEcoZoneSales + item.VatablePurchases + item.ZeroRatedPurchases + item.ZeroRatedEcoZonePurchases, 2)
                .MarketFeesRate = item.MarketFeesRate
                .Remarks = item.Remarks
            End With
            ret.Add(wesmBillItemEnergy)
            If (item.VatOnSales + item.VatOnPurchases) <> 0 Then
                Dim wesmBillItemVAT As New WESMBill
                With wesmBillItemVAT
                    .BillingPeriod = item.BillingPeriod
                    .SettlementRun = item.STLRun
                    If changeSTLID Is Nothing Then
                        .IDNumber = item.StlID
                    Else
                        .IDNumber = changeSTLID
                    End If
                    .ChargeType = EnumChargeType.EV
                    .RegistrationID = item.BillingID
                    .InvoiceDate = item.TransactionDate
                    .InvoiceNumber = item.TransactionNo
                    .DueDate = item.DueDate
                    .Amount = Math.Round(item.VatOnSales + item.VatOnPurchases, 2)
                    .MarketFeesRate = item.MarketFeesRate
                    .Remarks = item.Remarks
                End With
                ret.Add(wesmBillItemVAT)
            End If
        Next
        ret.TrimExcess()
        Return ret
    End Function

    Public Function ValidateWTACoverSummaryList(ByVal newWTACoverSummaryList As List(Of WESMBillAllocCoverSummary)) As List(Of String)
        Dim ret As New List(Of String)

        For Each item In newWTACoverSummaryList
            Dim getParticipantInfo As AMParticipants = Me._AMParticipantsList.Where(Function(x) x.IDNumber = item.StlID).FirstOrDefault
            If getParticipantInfo Is Nothing Then
                ret.Add(item.StlID & " is not exist in Participant Infomration List!")
            End If
            If item.GrossSale <> 0 Then
                Dim getSumGrossSalesInDetails As Decimal = item.ListWBAllocDisDetails.Where(Function(y) y.GrossSale <> 0).Select(Function(x) x.GrossSale).Sum()
                Dim getSumEWTSalesInDetails As Decimal = item.ListWBAllocDisDetails.Where(Function(y) y.GrossSale <> 0).Select(Function(x) x.EWT).Sum()

                If item.GrossSale <> getSumGrossSalesInDetails Then
                    ret.Add(item.TransactionNo & " Gross Sales is not equal to Total Allocation Gross Sales!")
                End If

                If item.EWTSales <> getSumEWTSalesInDetails Then
                    ret.Add(item.TransactionNo & " EWT Sales is not equal to Total Allocation EWT Sales!")
                End If
            End If

            If item.GrossPurchase <> 0 Then
                Dim getSumGrossPurchasesInDetails As Decimal = item.ListWBAllocDisDetails.Where(Function(y) y.GrossPurchase <> 0).Select(Function(x) x.GrossPurchase).Sum()
                Dim getSumEWTPurchasesInDetails As Decimal = item.ListWBAllocDisDetails.Where(Function(y) y.GrossPurchase <> 0).Select(Function(x) x.EWT).Sum()

                If item.GrossPurchase <> getSumGrossPurchasesInDetails Then
                    ret.Add(item.TransactionNo & " Gross Purchases is not equal to Total Allocation EWT Purchases!")
                End If

                If item.EWTPurchases <> getSumEWTPurchasesInDetails Then
                    ret.Add(item.TransactionNo & " EWT Purchases is not equal to Total Allocation EWT Purchases!")
                End If
            End If
        Next
        Return ret
    End Function
    Public Sub SaveUplodedWESMBill(ByVal calendarBP As CalendarBillingPeriod, ByVal stlRun As String, ByVal groupID As Long, ByVal chargeType As EnumChargeType,
                                   ByVal filetype As EnumFileType, ByVal itemJV As JournalVoucher, ByVal itemGP As WESMBillGPPosted,
                                   ByVal countWESMBillExist As Integer, ByVal progress As IProgress(Of ProgressClass),
                                   ByVal ct As CancellationToken)
        Dim report As New DataReport
        Dim listSQL As New List(Of String)
        Dim dicInvRef As New Dictionary(Of String, Long)
        Dim ds As New DataSet
        Dim SQL As String
        Dim batchCode As String

        Dim newProgress As ProgressClass = New ProgressClass
        newProgress.ProgressMsg = "Please wait while preparing SQL Scripts to save in AMS Database..."
        progress.Report(newProgress)

        Dim bpValue As String = calendarBP.BillingPeriod.ToString() & "(" & FormatDateTime(calendarBP.StartDate, DateFormat.ShortDate) &
                                " - " & FormatDateTime(calendarBP.EndDate, DateFormat.ShortDate) & ")"

        'For Deletion of exsting records in AM_WESM_Bill
        'Generate also the Revision Number
        Select Case chargeType
            Case EnumChargeType.E
                If countWESMBillExist <> 0 Then
                    'For Deletion
                    SQL = "DELETE FROM AM_WESM_BILL  " &
                          "WHERE (CHARGE_TYPE = '" & EnumChargeType.E.ToString() & "' " &
                          "       OR CHARGE_TYPE = '" & EnumChargeType.EV.ToString() & "') AND " &
                          "       BILLING_PERIOD = " & calendarBP.BillingPeriod & " " &
                          "       AND STL_RUN = '" & stlRun & "'"
                    listSQL.Add(SQL)

                    Dim getListofSummaryIDForDeletion As List(Of Long) = Me._WBillHelper.GetListSummaryIDForDeletion(calendarBP.BillingPeriod, stlRun)
                    For Each item In getListofSummaryIDForDeletion
                        SQL = "DELETE FROM AM_WESM_ALLOC_DISAGG_DETAILS " & vbNewLine _
                                & "WHERE SUMMARY_ID = " & item
                        listSQL.Add(SQL)
                    Next

                    SQL = "DELETE FROM AM_WESM_ALLOC_COVER_SUMMARY " & vbNewLine _
                         & "WHERE BILLING_PERIOD = " & calendarBP.BillingPeriod & " " & vbNewLine _
                         & "AND STL_RUN = '" & stlRun & "'"
                    listSQL.Add(SQL)
                End If
        End Select

        'Get the Batch Code per charge type
        batchCode = EnumPostedType.U.ToString() & "-" & Me._WBillHelper.GetSequenceID("SEQ_AM_BATCH_CODE").ToString()

        'For deletion of Journal Voucher if there are records to be replaced
        SQL = "UPDATE AM_JV SET STATUS = " & EnumStatus.InActive & " " &
                  "WHERE STATUS = " & EnumStatus.Active & " AND BATCH_CODE = " &
                  "                   (SELECT BATCH_CODE FROM AM_WESM_BILL_GP_POSTED " &
                  "                    WHERE CHARGE_TYPE = '" & chargeType.ToString() & "' " &
                  "                    AND BILLING_PERIOD = " & calendarBP.BillingPeriod & " " &
                  "                    AND STL_RUN = '" & stlRun & "') "
        listSQL.Add(SQL)

        'For Deletion in WESM Bill GP Posted
        SQL = "DELETE FROM AM_WESM_BILL_GP_POSTED " &
                  "WHERE CHARGE_TYPE = '" & chargeType.ToString() & "' AND BILLING_PERIOD = " & calendarBP.BillingPeriod & " " &
                  "AND STL_RUN = '" & stlRun & "'"
        listSQL.Add(SQL)

        Dim getWBSChangeParentList As List(Of WESMBillSummaryChangeParentId) = (From x In Me._WBillHelper.GetWESMBillSummaryChangeParentIDAll() Where x.Status = EnumStatus.Active Select x).ToList()
        'Data for WESM Bills
        For Each item In Me.newImportWTAFromCRSSList.Where(Function(x) x.BillingPeriod = calendarBP.BillingPeriod And x.SettlementRun = stlRun And x.GroupID = groupID).Select(Function(y) y.WESMBillList).FirstOrDefault
            Dim amcode As String = calendarBP.BillingPeriod.ToString() & "-" &
                                       item.IDNumber & "-" & _WBillHelper.GetSequenceID("SEQ_AM_CODE").ToString()
            With item
                'Get the unique key to set invoice no
                Dim UniqueKey = .IDNumber & .RegistrationID & .InvoiceDate.ToString("MMddyyyy") & .DueDate.ToString("MMddyyyy")
                'Get the final invoice no
                SQL = "INSERT INTO AM_WESM_BILL(BATCH_CODE,AM_CODE,BILLING_PERIOD,STL_RUN,ID_NUMBER,REG_ID,INVOICE_NO,INVOICE_DATE,AMOUNT,CHARGE_TYPE,DUE_DATE,MARKET_FEES_RATE,REMARKS,UPDATED_BY)" & vbNewLine _
                        & "SELECT '" & batchCode & "', '" & amcode & "'," & .BillingPeriod & ",'" & .SettlementRun & "', '" & .IDNumber & "','" & .RegistrationID & "','" & .InvoiceNumber & "'," & vbNewLine _
                        & "TO_DATE('" & .InvoiceDate & "','MM/DD/yyyy')," & .Amount & ",'" & .ChargeType.ToString & "',TO_DATE('" & .DueDate & "','MM/DD/yyyy')," & .MarketFeesRate & ",'" & .Remarks & "','" & AMModule.UserName & "' FROM DUAL"
                listSQL.Add(SQL)
            End With
        Next

        Dim jvNo As Long = _WBillHelper.GetSequenceID("SEQ_AM_JV_NO")
        With itemJV
            SQL = "INSERT INTO AM_JV (AM_JV_NO,BATCH_CODE,STATUS,PREPARED_BY,CHECKED_BY,APPROVED_BY,UPDATED_BY,POSTED_TYPE)" & vbNewLine _
                    & "SELECT " & jvNo & ",'" & batchCode & "',1,'" & AMModule.UserName & "','" & .CheckedBy & "','" & .ApprovedBy & "','" & vbNewLine _
                            & AMModule.UserName & "','" & .PostedType & "' FROM DUAL"
            listSQL.Add(SQL)

            For Each item In itemJV.JVDetails
                SQL = "INSERT INTO AM_JV_DETAILS(AM_JV_NO,ACCT_CODE,DEBIT,CREDIT,UPDATED_BY)" & vbNewLine _
                        & "SELECT " & jvNo & ",'" & item.AccountCode & "'," & item.Debit & "," & item.Credit & ",'" & AMModule.UserName & "' FROM DUAL"
                listSQL.Add(SQL)
            Next
        End With

        Dim chargeTypeValue As String = ""
        With itemGP
            'Get the charge type
            Select Case chargeType
                Case EnumChargeType.E
                    chargeTypeValue = "Energy"
                    'Case EnumChargeType.MF
                    '    chargeTypeValue = "Market Fees"
            End Select
            Dim remarks As String = "Final Statement for " & chargeTypeValue & "." &
                                         " Billing Period = " & bpValue &
                                         ", Settlement Run = " & stlRun & ", Due Date = " & .DueDate.ToString("MM/dd/yyyy") &
                                         " and Batch Code = " & batchCode

            SQL = "INSERT INTO AM_WESM_BILL_GP_POSTED (BILLING_PERIOD,STL_RUN,CHARGE_TYPE,DUE_DATE,REMARKS,POSTED,UPDATED_BY,BATCH_CODE,POSTED_TYPE,DOCUMENT_AMOUNT,AM_JV_NO)" & vbNewLine _
                    & "SELECT " & calendarBP.BillingPeriod & ",'" & stlRun & "','" & .Charge.ToString & "',TO_DATE('" & .DueDate & "','MM/DD/yyyy')," & vbNewLine _
                    & "'" & remarks & "',0" & ",'" & AMModule.UserName & "','" & batchCode & "','" & .PostType & "'," & vbNewLine _
                    & .DocumentAmount & "," & jvNo & " FROM DUAL"
            listSQL.Add(SQL)
        End With

        For Each item In Me.newImportWTAFromCRSSList.Where(Function(x) x.BillingPeriod = calendarBP.BillingPeriod And x.SettlementRun = stlRun And x.GroupID = groupID).Select(Function(y) y.WTACoverSummaryList).FirstOrDefault
            Dim summaryNo As Long = Me._WBillHelper.GetSequenceID("SEQ_AM_SUMMARY_ID")
            SQL = "INSERT INTO AM_WESM_ALLOC_COVER_SUMMARY(SUMMARY_ID,STL_RUN,GROUP_ID,BILLING_PERIOD,STL_ID,BILLING_ID,NON_VATABLE_TAG,ZERO_RATED_TAG,WHT_TAG,ITH_TAG,NET_SELLER_BUYER_TAG," & vbNewLine _
                                                               & "TRANSACTION_NUMBER,TRANSACTION_DATE,DUE_DATE,VATABLE_SALES,ZERO_RATED_SALES,ZERO_RATED_ECOZONE_SALES,VATABLE_PURCHASES,ZERO_RATED_PURCHASES," & vbNewLine _
                                                               & "NSS_FLOWBACK,VAT_ON_SALES,VAT_ON_PURCHASES,EWT_SALES,EWT_PURCHASES,NET_ENERGY_QTY,GMR,MARKET_FEES_RATE,REMARKS,UPLOAD_FROM,SPOT_QTY,GENX_AMT,ZERO_RATED_ECOZONE_PURCHASES)" & vbNewLine _
                      & "SELECT " & summaryNo & ", '" & item.STLRun & "', ''," & item.BillingPeriod & ", '" & item.StlID & "', '" & item.BillingID & "', '" & item.NonVatableTag & "', '" & item.ZeroRatedTag & "', " & vbNewLine _
                                  & "'" & item.WHT & "', '" & item.ITH & "', '" & item.NetSellerBuyerTag & "', '" & item.TransactionNo & "', TO_DATE('" & item.TransactionDate.ToShortDateString & "','MM/DD/YYYY'), " & vbNewLine _
                                  & "TO_DATE('" & item.DueDate.ToShortDateString & "','MM/DD/YYYY'), " & item.VatableSales & ", " & item.ZeroRatedSales & ", " & item.ZeroRatedEcoZoneSales & ", " & item.VatablePurchases & vbNewLine _
                                  & ", " & item.ZeroRatedPurchases & ", " & item.NSSFlowBack & ", " & item.VatOnSales & ", " & item.VatOnPurchases & ", " & item.EWTSales & ", " & item.EWTPurchases & vbNewLine _
                                  & ", 0, " & item.GMR & ", " & item.MarketFeesRate & ", '" & item.Remarks & "', 'FLATFILE'," & item.SpotQty & "," & item.GenXAmount & "," & item.ZeroRatedEcoZonePurchases & " FROM DUAL"
            listSQL.Add(SQL)
            For Each dtl In item.ListWBAllocDisDetails
                SQL = "INSERT INTO AM_WESM_ALLOC_DISAGG_DETAILS(SUMMARY_ID,BILLING_PERIOD,BILLING_ID,FACILITY_TYPE,WHT,ITH,NON_VATABLE_TAG,ZERO_RATED_TAG, NET_SELLER_BUYER_TAG,VATABLE_SALES,ZERO_RATED_SALES,ZERO_RATED_ECOZONE_SALES," & vbNewLine _
                                                                    & "VAT_ON_SALES,VATABLE_PURCHASES,ZERO_RATED_PURCHASES,ZERO_RATED_ECOZONE_PURCHASES,VAT_ON_PURCHASES,EWT,STL_ID) " & vbNewLine _
                                & "SELECT " & summaryNo & ", " & item.BillingPeriod & ", '" & dtl.BillingID & "', '" & dtl.FacilityType & "', '" & dtl.WHTTag & "', '" & dtl.ITHTag & "', '" & dtl.NonVatableTag & "', " & vbNewLine _
                                    & "'" & dtl.ZeroRatedTag & "', '" & dtl.NetSellerBuyerTag & "', " & dtl.VatableSales & ", " & dtl.ZeroRatedSales & ", " & dtl.ZeroRatedEcoZoneSales & ", " & dtl.VatOnSales & ", " & dtl.VatablePurchases & ", " & vbNewLine _
                                    & dtl.ZeroRatedPurchases & ", " & dtl.ZeroRatedEcoZonePurchases & ", " & dtl.VatOnPurchases & ", " & dtl.EWT & ",'" & dtl.STLID & "' FROM DUAL"
                listSQL.Add(SQL)
            Next
        Next

        report = Me._WBillHelper.DataAccess.ExecuteSaveQuery2(listSQL, progress, ct)

        If report.ErrorMessage.Length <> 0 Then
            Throw New ApplicationException(report.ErrorMessage)
        End If
        newProgress = New ProgressClass
        newProgress.ProgressMsg = "Successfully saved!"
        progress.Report(newProgress)
    End Sub
End Class
