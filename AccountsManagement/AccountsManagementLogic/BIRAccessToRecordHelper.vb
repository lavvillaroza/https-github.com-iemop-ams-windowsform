'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             BIRAcessToRecordHelper
'Orginal Author:         Lance Arjay Villaroza
'File Creation Date:     March 28, 2016
'Development Group:      Software Development and Support Division
'Description:            Class Settlement Notice Helper
'Arguments/Parameters:   -
'Files/Database Tables:  -
'Return Value:           -
'Error codes/Exceptions: -
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description

Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Threading.Tasks
Imports Microsoft.Office.Interop.Excel
Imports System.IO

Public Class BIRAccessToRecordHelper
    Public Sub New()
        _DataAccess = DAL.GetInstance()
        _WBillHelper = WESMBillHelper.GetInstance
    End Sub
#Region "DAL"
    Private _DataAccess As DAL
    Public ReadOnly Property DataAccess() As DAL
        Get
            Return Me._DataAccess
        End Get
    End Property
#End Region
#Region "WESMBillHelper"
    Private _WBillHelper As WESMBillHelper
    Public ReadOnly Property WBillHelper() As WESMBillHelper
        Get
            Return _WBillHelper
        End Get
    End Property
#End Region
#Region "Object Properties"
    Private _ParticipantsList As New List(Of AMParticipants)
    Public ReadOnly Property ParticipantsList() As List(Of AMParticipants)
        Get
            Return _ParticipantsList
        End Get
    End Property

    Private _WESMBillList As New List(Of WESMBill)
    Public ReadOnly Property WESMBillList() As List(Of WESMBill)
        Get
            Return _WESMBillList
        End Get
    End Property

    Private _WESMBillSummaryList As New List(Of WESMBillSummary)
    Public ReadOnly Property WESMBillSummaryList() As List(Of WESMBillSummary)
        Get
            Return _WESMBillSummaryList
        End Get
    End Property

    Private _WESMBillSummaryHisList As New List(Of WESMBillSummaryHistory)
    Public ReadOnly Property WESMBillSummaryHisList() As List(Of WESMBillSummaryHistory)
        Get
            Return _WESMBillSummaryHisList
        End Get
    End Property

    Private _WTAMainSummaryList As New List(Of WESMBillAllocCoverSummary)
    Public ReadOnly Property WTAMainSummaryList() As List(Of WESMBillAllocCoverSummary)
        Get
            Return _WTAMainSummaryList
        End Get
    End Property

    Private _WTADetailsSummaryList As New List(Of WESMTransDetailsSummary)
    Public ReadOnly Property WTADetailsSummaryList() As List(Of WESMTransDetailsSummary)
        Get
            Return _WTADetailsSummaryList
        End Get
    End Property

    Private _WTADetailsSummaryHisList As New List(Of WESMTransDetailsSummaryHistory)
    Public ReadOnly Property WTADetailsSummaryHisList() As List(Of WESMTransDetailsSummaryHistory)
        Get
            Return _WTADetailsSummaryHisList
        End Get
    End Property

    Private _MFCollectionList As List(Of CollectionAllocation)
    Public ReadOnly Property MFCollectionList() As List(Of CollectionAllocation)
        Get
            Return _MFCollectionList
        End Get
    End Property

    Private _MFOffsetFromPaymentList As List(Of ARCollection)
    Public ReadOnly Property MFOffsetFromPaymentList() As List(Of ARCollection)
        Get
            Return _MFOffsetFromPaymentList
        End Get
    End Property
#End Region

#Region "Get WESMBillsList"
    Private Async Function GetWESMBillsListAsync(ByVal dateFrom As Date, ByVal dateTo As Date) As Threading.Tasks.Task(Of List(Of WESMBill))
        Dim ret As New List(Of WESMBill)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT * FROM AM_WESM_BILL " _
                              & "WHERE DUE_DATE  >= " & "TO_DATE('" & CDate(FormatDateTime(dateFrom, DateFormat.ShortDate)) & "', 'MM/DD/YYYY') " & vbNewLine _
                              & "AND DUE_DATE <= TO_DATE('" & CDate(FormatDateTime(dateTo, DateFormat.ShortDate)) & "', 'MM/DD/YYYY') " & vbNewLine _
                              & "AND (INVOICE_NO LIKE 'TS-%' OR INVOICE_NO LIKE 'FS-%' OR INVOICE_NO LIKE 'SI-%') AND NOT UPPER(INVOICE_NO) LIKE '%ADJ%'"

            report = Await Me.DataAccess.ExecuteSelectQueryReturningDataReaderAsync(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            ret = Me.GetWESMBillsList(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetWESMBillsList(ByVal dr As IDataReader) As List(Of WESMBill)
        Dim ret As New List(Of WESMBill)
        Try
            While dr.Read()
                Dim item As New WESMBill

                With dr
                    item.BatchCode = .Item("BATCH_CODE").ToString()
                    item.AMCode = .Item("AM_CODE").ToString()
                    item.BillingPeriod = CInt(.Item("BILLING_PERIOD"))
                    item.SettlementRun = CStr(.Item("STL_RUN"))
                    item.IDNumber = CStr(.Item("ID_NUMBER"))
                    item.RegistrationID = CStr(.Item("REG_ID"))
                    item.ForTheAccountOf = CStr(.Item("FOR_ACCOUNT_OF").ToString())
                    item.FullName = CStr(.Item("FULL_NAME").ToString())
                    item.InvoiceNumber = CStr(.Item("INVOICE_NO"))
                    item.InvoiceDate = CDate(.Item("INVOICE_DATE"))
                    item.Amount = CDec(.Item("AMOUNT"))
                    item.ChargeType = CType(System.Enum.Parse(GetType(EnumChargeType), CStr(.Item("CHARGE_TYPE"))), EnumChargeType)
                    item.DueDate = CDate(.Item("DUE_DATE"))
                    item.MarketFeesRate = CDec(.Item("MARKET_FEES_RATE"))
                    item.Remarks = Trim(.Item("REMARKS").ToString())
                    ret.Add(item)
                End With
            End While
            ret.TrimExcess()
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try
        Return ret
    End Function
#End Region

#Region "Get WESMBillSummaryList"
    Private Async Function GetWESMBillSummaryListAsync(ByVal dateFrom As Date, ByVal dateTo As Date) As Threading.Tasks.Task(Of List(Of WESMBillSummary))
        Dim result As New List(Of WESMBillSummary)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.ID_NUMBER, A.ID_TYPE, A.GROUP_NO, B.PARTICIPANT_ID,  A.billing_period, A.TRANSACTION_DATE, A.ENERGY_WITHHOLD, " & vbNewLine _
                                & "B.PARTICIPANT_ADDRESS, B.CITY, B.PROVINCE, B.ZIP_CODE, B.ZERO_RATED_MARKET_FEES, B.ZERO_RATED_ENERGY, " & vbNewLine _
                                & "A.CHARGE_TYPE, A.DUE_DATE, A.ENDING_BALANCE, a.BEGINNING_BALANCE, a.NEW_DUEDATE, a.IS_MFWTAX_DEDUCTED, " & vbNewLine _
                                & "A.INV_DM_CM, A.SUMMARY_TYPE, a.WESMBILL_SUMMARY_NO, A.ADJUSTMENT, a.WESMBILL_BATCH_NO, A.ENERGY_WITHHOLD_STATUS, A.NO_OFFSET, A.NO_SOA, A.NO_DEFINT, D.REMARKS, A.BALANCE_TYPE " & vbNewLine _
                                & "FROM AM_WESM_BILL_SUMMARY A " & vbNewLine _
                                & "INNER JOIN  AM_PARTICIPANTS B on a.id_number = b.id_number " & vbNewLine _
                                & "INNER JOIN  AM_WESM_BILL D on D.invoice_no = a.inv_dm_cm and D.charge_type = a.charge_type " & vbNewLine _
                                & "WHERE A.DUE_DATE  >= " & "TO_DATE('" & CDate(FormatDateTime(dateFrom, DateFormat.ShortDate)) & "', 'MM/DD/YYYY') " & vbNewLine _
                                & "AND A.DUE_DATE <= TO_DATE('" & CDate(FormatDateTime(dateTo, DateFormat.ShortDate)) & "', 'MM/DD/YYYY')"

            report = Await Me.DataAccess.ExecuteSelectQueryReturningDataReaderAsync(SQL)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            result = Me.GetWESMBillSummaryList(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function
    Private Function GetWESMBillSummaryList(ByVal dr As IDataReader) As List(Of WESMBillSummary)
        Dim result As New List(Of WESMBillSummary)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As New WESMBillSummary
                    item.IDNumber = New AMParticipants(CStr(.Item("ID_NUMBER").ToString()), CStr(.Item("PARTICIPANT_ID").ToString()),
                                                       CStr(.Item("PARTICIPANT_ADDRESS").ToString()), CStr(.Item("CITY").ToString()), CStr(.Item("PROVINCE").ToString()),
                                                       CStr(.Item("ZIP_CODE").ToString()), CBool(IIf(CInt(.Item("ZERO_RATED_MARKET_FEES")) = 1, True, False)),
                                                       CBool(IIf(CInt(.Item("ZERO_RATED_ENERGY")) = 1, True, False)))

                    item.BillPeriod = CInt(.Item("BILLING_PERIOD").ToString())
                    item.ChargeType = CType(System.Enum.Parse(GetType(EnumChargeType), CStr(.Item("CHARGE_TYPE").ToString())), EnumChargeType)
                    item.DueDate = CDate(.Item("DUE_DATE").ToString())
                    item.BeginningBalance = Math.Round(CDec(.Item("BEGINNING_BALANCE").ToString()), 2)
                    item.BalanceType = CType(System.Enum.Parse(GetType(EnumBalanceType), CStr(.Item("BALANCE_TYPE").ToString)), EnumBalanceType)
                    item.EndingBalance = Math.Round(CDec(.Item("ENDING_BALANCE").ToString()), 2)
                    item.OrigEndingBalance = Math.Round(CDec(.Item("ENDING_BALANCE").ToString()), 2)
                    item.IDType = CStr(.Item("ID_TYPE").ToString())
                    item.NewDueDate = CDate(.Item("NEW_DUEDATE").ToString())
                    item.OrigNewDueDate = CDate(.Item("NEW_DUEDATE").ToString())
                    item.IsMFWTaxDeducted = CInt(.Item("IS_MFWTAX_DEDUCTED").ToString())
                    item.INVDMCMNo = CStr(.Item("INV_DM_CM").ToString())
                    If .Item("SUMMARY_TYPE") IsNot DBNull.Value Then
                        item.SummaryType = CType(System.Enum.Parse(GetType(EnumSummaryType), CStr(.Item("SUMMARY_TYPE").ToString())), EnumSummaryType)
                    End If
                    item.WESMBillSummaryNo = CLng(.Item("WESMBILL_SUMMARY_NO"))
                    item.Adjustment = CInt(.Item("ADJUSTMENT").ToString())
                    item.TransactionDate = CDate(.Item("TRANSACTION_DATE"))
                    item.EnergyWithhold = CDec(.Item("ENERGY_WITHHOLD"))
                    item.WESMBillBatchNo = CLng(.Item("WESMBILL_BATCH_NO"))
                    If .Item("ENERGY_WITHHOLD_STATUS") IsNot DBNull.Value Then
                        item.EnergyWithholdStatus = CType(CInt(.Item("ENERGY_WITHHOLD_STATUS")), EnumEnergyWithholdStatus)
                    Else
                        item.EnergyWithholdStatus = EnumEnergyWithholdStatus.NotApplicable
                    End If

                    item.NoOffset = CBool(IIf(CInt(.Item("NO_OFFSET")) = 1, True, False))
                    item.NoSOA = CBool(IIf(CInt(.Item("NO_SOA")) = 1, True, False))
                    item.NoDefInt = CBool(IIf(CInt(.Item("NO_DEFINT")) = 1, True, False))
                    item.BillingRemarks = CStr(.Item("REMARKS").ToString)
                    result.Add(item)
                End With
            End While
        Catch ex As Exception
            Throw New ApplicationException("Error in row " & index & " --- " & ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return result
    End Function
#End Region

#Region "Get WESMBillSummaryHistoryList"
    Private Async Function GetWESMBillSummaryHistoryListAsync(ByVal dateFrom As Date, ByVal dateTo As Date) As Threading.Tasks.Task(Of List(Of WESMBillSummaryHistory))
        Dim result As New List(Of WESMBillSummaryHistory)
        Dim report As New DataReport
        Dim SQL As String
        Try
            SQL = "SELECT * FROM AM_WESM_BILL_SUMMARY_HISTORY " &
                  "WHERE UPDATED_DATE >= TO_DATE('" & dateFrom & "','MM/DD/YYYY') AND " &
                  "UPDATED_DATE <=  TO_DATE('" & dateTo & "','MM/DD/YYYY') AND " &
                  " CERTIFICATE_NO = 0 ORDER BY UPDATED_DATE ASC"

            report = Await Me.DataAccess.ExecuteSelectQueryReturningDataReaderAsync(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.GetWESMBillSummaryHistory(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function
    Private Function GetWESMBillSummaryHistory(ByVal dr As IDataReader) As List(Of WESMBillSummaryHistory)
        Dim result As New List(Of WESMBillSummaryHistory)

        Try
            While dr.Read()
                Dim item As New WESMBillSummaryHistory

                With dr
                    item.WESMBillSummaryNo = CLng(.Item("WESMBILL_SUMMARY_NO"))
                    item.CollectionNumber = CLng(.Item("COLLECTION_NO"))
                    item.PaymentBatchCode = If(IsDBNull(.Item("PAYMENT_BATCH_CODE")), Nothing, CStr(.Item("PAYMENT_BATCH_CODE").ToString()))
                    item.DueDate = CDate(.Item("DUE_DATE"))
                    item.Amount = CDec(.Item("AMOUNT"))
                    If CLng(.Item("COLLECTION_NO")) = 0 And (CLng(.Item("PAYMENT_NO")) <> 0 Or CLng(.Item("CERTIFICATE_NO")) <> 0) Then
                        item.PaymentType = CType(System.Enum.Parse(GetType(EnumPaymentNewType), CStr(.Item("PAYMENT_TYPE").ToString)), EnumPaymentNewType)
                    End If
                    If CLng(.Item("COLLECTION_NO")) <> 0 And CLng(.Item("PAYMENT_NO")) = 0 Then
                        item.CollectionType = CType(System.Enum.Parse(GetType(EnumCollectionType), CStr(.Item("COLLECTION_TYPE").ToString)), EnumCollectionType)
                    End If

                    item.Status = CType(System.Enum.Parse(GetType(EnumStatus), CStr(.Item("STATUS").ToString())), EnumStatus)
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

#Region "Get WTAMainSummaryList"
    Private Async Function GetWTAMainSummaryList(ByVal dateFrom As Date, ByVal dateTo As Date) As Task(Of List(Of WESMBillAllocCoverSummary))
        Dim ret As New List(Of WESMBillAllocCoverSummary)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT * FROM AM_WESM_ALLOC_COVER_SUMMARY " _
                              & "WHERE DUE_DATE >= " & "TO_DATE('" & CDate(FormatDateTime(dateFrom, DateFormat.ShortDate)) & "', 'MM/DD/YYYY') " & vbNewLine _
                              & "AND DUE_DATE <= TO_DATE('" & CDate(FormatDateTime(dateTo, DateFormat.ShortDate)) & "', 'MM/DD/YYYY')"

            report = Await Me.DataAccess.ExecuteSelectQueryReturningDataReaderAsync(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            ret = Me.GetWTAMainSummaryList(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetWTAMainSummaryList(ByVal dr As IDataReader) As List(Of WESMBillAllocCoverSummary)
        Dim ret As New List(Of WESMBillAllocCoverSummary)
        Try
            While dr.Read()
                Dim item As New WESMBillAllocCoverSummary
                With dr
                    item.SummaryId = CLng(.Item("SUMMARY_ID"))
                    item.STLRun = CStr(.Item("STL_RUN"))
                    item.BillingPeriod = CInt(.Item("BILLING_PERIOD"))
                    item.StlID = CStr(.Item("STL_ID"))
                    item.BillingID = CStr(.Item("BILLING_ID"))
                    item.NonVatableTag = CStr(.Item("NON_VATABLE_TAG"))
                    item.ZeroRatedTag = CStr(.Item("ZERO_RATED_TAG"))
                    item.WHT = CStr(.Item("WHT_TAG"))
                    item.ITH = CStr(.Item("ITH_TAG"))
                    item.NetSellerBuyerTag = CStr(.Item("NET_SELLER_BUYER_TAG"))
                    item.TransactionNo = CStr(.Item("TRANSACTION_NUMBER"))
                    item.TransactionDate = CDate(.Item("TRANSACTION_DATE"))
                    item.DueDate = CDate(.Item("DUE_DATE"))
                    item.VatableSales = CDec(.Item("VATABLE_SALES"))
                    item.ZeroRatedSales = CDec(.Item("ZERO_RATED_SALES"))
                    item.ZeroRatedEcoZoneSales = CDec(.Item("ZERO_RATED_ECOZONE_SALES"))
                    item.VatablePurchases = CDec(.Item("VATABLE_PURCHASES"))
                    item.ZeroRatedPurchases = CDec(.Item("ZERO_RATED_PURCHASES"))
                    item.ZeroRatedEcoZonePurchases = CDec(.Item("ZERO_RATED_ECOZONE_PURCHASES"))
                    item.NSSFlowBack = CDec(.Item("NSS_FLOWBACK"))
                    item.VatOnSales = CDec(.Item("VAT_ON_SALES"))
                    item.VatOnPurchases = CDec(.Item("VAT_ON_PURCHASES"))
                    item.EWTSales = CDec(.Item("EWT_SALES"))
                    item.EWTPurchases = CDec(.Item("EWT_PURCHASES"))
                    item.GMR = CDec(.Item("GMR"))
                    item.SpotQty = CDec(.Item("SPOT_QTY"))
                    item.MarketFeesRate = CDec(.Item("MARKET_FEES_RATE"))
                    item.Remarks = CStr(.Item("REMARKS"))
                    ret.Add(item)
                End With
            End While
            ret.TrimExcess()
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try
        Return ret
    End Function
#End Region

#Region "Get WTADetailsSummaryList"
    Private Async Function GetListWESMTransDetailsSummaryAsync(ByVal dateFrom As Date, ByVal dateTo As Date) As Task(Of List(Of WESMTransDetailsSummary))
        Dim ret As New List(Of WESMTransDetailsSummary)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT A.* FROM AM_WESM_TRANS_DETAILS_SUMMARY A " & vbNewLine _
                              & "WHERE DUE_DATE >= " & "TO_DATE('" & CDate(FormatDateTime(dateFrom, DateFormat.ShortDate)) & "', 'MM/DD/YYYY') " & vbNewLine _
                              & "AND DUE_DATE <= TO_DATE('" & CDate(FormatDateTime(dateTo, DateFormat.ShortDate)) & "', 'MM/DD/YYYY')"
            report = Await Me.DataAccess.ExecuteSelectQueryReturningDataReaderAsync(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetListWESMTransDetailsSummary(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function
    Private Function GetListWESMTransDetailsSummary(ByVal dr As IDataReader) As List(Of WESMTransDetailsSummary)
        Dim result As New List(Of WESMTransDetailsSummary)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As New WESMTransDetailsSummary
                    item.BuyerTransNo = CStr(.Item("BUYER_TRANS_NO"))
                    item.BuyerBillingID = CStr(.Item("BUYER_BILLING_ID"))
                    item.SellerTransNo = CStr(.Item("SELLER_TRANS_NO"))
                    item.SellerBillingID = CStr(.Item("SELLER_BILLING_ID"))
                    item.DueDate = CDate(.Item("DUE_DATE"))
                    item.NewDueDate = CDate(.Item("NEW_DUE_DATE"))
                    item.OrigBalanceInEnergy = CDec(.Item("ORIG_AMOUNT_ENERGY"))
                    item.OrigBalanceInVAT = CDec(.Item("ORIG_AMOUNT_VAT"))
                    item.OrigBalanceInEWT = CDec(.Item("ORIG_AMOUNT_EWT"))
                    item.OutstandingBalanceInEnergy = CDec(.Item("OBIN_ENERGY"))
                    item.OutstandingBalanceInVAT = CDec(.Item("OBIN_VAT"))
                    item.OutstandingBalanceInEWT = CDec(.Item("OBIN_EWT"))
                    item.Status = EnumWESMTransDetailsSummaryStatus.CURRENT.ToString
                    result.Add(item)
                End With
            End While
        Catch ex As Exception
            Throw New ApplicationException("Error in row " & index & " --- " & ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try
        Return result
    End Function
#End Region

#Region "Get WTADetailsSummaryHisList"
    Private Async Function GetListWESMTransDetailsSummaryHistory(ByVal dateFrom As Date, ByVal dateTo As Date) As Task(Of List(Of WESMTransDetailsSummaryHistory))
        Dim ret As New List(Of WESMTransDetailsSummaryHistory)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT * FROM AM_WESM_TRANS_DETAILS_SUMMARY_HISTORY A " & vbNewLine _
                              & "WHERE ALLOCATION_DATE  >= " & "TO_DATE('" & CDate(FormatDateTime(dateFrom, DateFormat.ShortDate)) & "', 'MM/DD/YYYY') " & vbNewLine _
                              & "AND ALLOCATION_DATE <= TO_DATE('" & CDate(FormatDateTime(dateTo, DateFormat.ShortDate)) & "', 'MM/DD/YYYY')"

            report = Await Me.DataAccess.ExecuteSelectQueryReturningDataReaderAsync(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetListWESMTransDetailsSummaryHistory(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function
    Private Function GetListWESMTransDetailsSummaryHistory(ByVal dr As IDataReader) As List(Of WESMTransDetailsSummaryHistory)
        Dim result As New List(Of WESMTransDetailsSummaryHistory)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As New WESMTransDetailsSummaryHistory
                    item.BuyerTransNo = CStr(.Item("BUYER_TRANS_NO"))
                    item.BuyerBillingID = CStr(.Item("BUYER_BILLING_ID"))
                    item.SellerTransNo = CStr(.Item("SELLER_TRANS_NO"))
                    item.SellerBillingID = CStr(.Item("SELLER_BILLING_ID"))
                    item.DueDate = CDate(.Item("DUE_DATE"))
                    item.AllocationDate = CDate(.Item("ALLOCATION_DATE"))
                    item.RemittanceDate = CDate(.Item("REMITTANCE_DATE"))
                    item.AllocatedInEnergy = CDec(.Item("ALLOCATED_IN_ENERGY"))
                    item.AllocatedInVAT = CDec(.Item("ALLOCATED_IN_VAT"))
                    item.AllocatedInEWT = CDec(.Item("ALLOCATED_IN_EWT"))
                    item.AllocatedInDefInt = CDec(.Item("ALLOCATED_IN_DEFINT"))
                    result.Add(item)
                End With
            End While
        Catch ex As Exception
            Throw New ApplicationException("Error in row " & index & " --- " & ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try
        Return result
    End Function
#End Region

#Region "Get Collections"
    Private Async Function GetCollectionAllocation(ByVal dateFrom As Date, ByVal dateTo As Date) As Task(Of List(Of CollectionAllocation))
        Dim result As New List(Of CollectionAllocation)
        Dim report As New DataReport
        Dim SQL As String
        Try
            SQL = "SELECT a.*, b.INV_DM_CM, b.SUMMARY_TYPE, b.CHARGE_TYPE FROM AM_COLLECTION_ALLOCATION a, AM_WESM_BILL_SUMMARY b " & vbNewLine _
                & "WHERE a.WESMBILL_SUMMARY_NO = b.WESMBILL_SUMMARY_NO AND a.STATUS = 1 " & vbNewLine _
                & "AND a.COLLECTION_TYPE IN (6, 7) " & vbNewLine _
                & "AND a.ALLOCATION_DATE  >= " & "TO_DATE('" & CDate(FormatDateTime(dateFrom, DateFormat.ShortDate)) & "', 'MM/DD/YYYY') " & vbNewLine _
                & "AND a.ALLOCATION_DATE <= TO_DATE('" & CDate(FormatDateTime(dateTo, DateFormat.ShortDate)) & "', 'MM/DD/YYYY')"

            report = Await Me.DataAccess.ExecuteSelectQueryReturningDataReaderAsync(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            result = Me.GetCollectionAllocation(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return result
    End Function
    Private Function GetCollectionAllocation(ByVal dr As IDataReader) As List(Of CollectionAllocation)
        Dim result As New List(Of CollectionAllocation)
        Try
            While dr.Read()
                Dim item As New CollectionAllocation
                With dr
                    item.WESMBillSummaryNo = New WESMBillSummary(CLng(.Item("WESMBILL_SUMMARY_NO")), CStr(.Item("INV_DM_CM")),
                                                                 CType(System.Enum.Parse(GetType(EnumSummaryType), CStr(.Item("SUMMARY_TYPE"))), EnumSummaryType),
                                                                 CType(System.Enum.Parse(GetType(EnumChargeType), CStr(.Item("CHARGE_TYPE"))), EnumChargeType),
                                                                 CDate(.Item("DUE_DATE")), CDec(.Item("ENERGY_WITHHOLD")))
                    item.BillingPeriod = CInt(.Item("BILLING_PERIOD").ToString)
                    item.CollectionNumber = CLng(.Item("COLLECTION_NO").ToString)
                    item.Amount = CDec(.Item("AMOUNT").ToString)
                    item.EndingBalance = CDec(.Item("ENDING_BALANCE").ToString)
                    item.NewEndingBalance = CDec(.Item("NEW_ENDING_BALANCE").ToString)
                    item.DueDate = CDate(.Item("DUE_DATE").ToString)
                    item.NewDueDate = CDate(.Item("NEW_DUEDATE").ToString)
                    item.CollectionType = CType(System.Enum.Parse(GetType(EnumCollectionType), CStr(.Item("COLLECTION_TYPE").ToString)), EnumCollectionType)
                    item.Status = CInt(.Item("STATUS").ToString)
                    item.AllocationDate = CDate(.Item("ALLOCATION_DATE").ToString)
                    item.DMCMNumber = CLng(.Item("AM_DMCM_NO"))
                    item.ReferenceNumber = CStr(.Item("AM_REF_NO"))
                    item.ReferenceType = CType(System.Enum.Parse(GetType(EnumSummaryType), CStr(.Item("AM_REF_TYPE").ToString)), EnumSummaryType)
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

#Region "Get Offset Payments to Collections"
    Public Async Function GetAMPaymentNewOffsetAR(ByVal dateFrom As Date, ByVal dateTo As Date) As Task(Of List(Of ARCollection))
        Dim ret As New List(Of ARCollection)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT A.*, B.CHARGE_TYPE, B.INV_DM_CM, C.ID_NUMBER, C.PARTICIPANT_ID, B.WESMBILL_BATCH_NO, D.REMARKS " & vbNewLine _
                              & "FROM AM_PAYMENT_NEW_OFFSETTING_AR A " & vbNewLine _
                              & "LEFT JOIN AM_WESM_BILL_SUMMARY B ON A.WESMBILL_SUMMARY_NO = B.WESMBILL_SUMMARY_NO " & vbNewLine _
                              & "LEFT JOIN AM_PARTICIPANTS C ON C.ID_NUMBER = A.ID_NUMBER " & vbNewLine _
                              & "LEFT JOIN AM_WESM_BILL D ON D.INVOICE_NO = B.INV_DM_CM AND D.CHARGE_TYPE = B.CHARGE_TYPE " & vbNewLine _
                              & "WHERE B.BALANCE_TYPE = 'AR' " & vbNewLine _
                              & "AND A.COLLECTION_TYPE IN (6, 7)" & vbNewLine _
                              & "AND A.ALLOCATION_DATE  >= " & "TO_DATE('" & CDate(FormatDateTime(dateFrom, DateFormat.ShortDate)) & "', 'MM/DD/YYYY') " & vbNewLine _
                              & "AND A.ALLOCATION_DATE <= TO_DATE('" & CDate(FormatDateTime(dateTo, DateFormat.ShortDate)) & "', 'MM/DD/YYYY')"
            report = Await Me.DataAccess.ExecuteSelectQueryReturningDataReaderAsync(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            ret = Me.GetAMPaymentNewOffsetAR(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function
    Private Function GetAMPaymentNewOffsetAR(ByVal dr As IDataReader) As List(Of ARCollection)
        Dim ret As New List(Of ARCollection)
        Try
            While dr.Read()
                With dr
                    Dim item As New ARCollection
                    item.BillingRemarks = If(IsDBNull(.Item("REMARKS")), "", CStr(.Item("REMARKS")))
                    item.BillingPeriod = CInt(.Item("BILLING_PERIOD"))
                    item.EndingBalance = CDec(.Item("ENDING_BALANCE"))
                    item.NewEndingBalance = CDec(.Item("NEW_ENDING_BALANCE"))
                    item.DueDate = CDate(FormatDateTime(CDate(.Item("DUE_DATE")), DateFormat.ShortDate))
                    item.NewDueDate = CDate(FormatDateTime(CDate(.Item("NEW_DUEDATE")), DateFormat.ShortDate))
                    item.AllocationAmount = CDec(.Item("ALLOCATION_AMOUNT"))
                    item.CollectionType = CType(.Item("COLLECTION_TYPE"), EnumCollectionType)
                    item.CollectionCategory = CType(.Item("COLLECTION_CATEGORY"), EnumCollectionCategory)
                    item.AllocationDate = CDate(FormatDateTime(CDate(.Item("ALLOCATION_DATE")), DateFormat.ShortDate))
                    item.WESMBillSummaryNo = CLng(.Item("WESMBILL_SUMMARY_NO"))
                    item.WESMBillBatchNo = CLng(.Item("WESMBILL_BATCH_NO"))
                    item.OffsettingSequence = CInt(.Item("OFFSET_SEQ"))
                    item.InvoiceNumber = CStr(.Item("INV_DM_CM"))
                    item.IDNumber = CStr(.Item("ID_NUMBER"))
                    item.ParticipantID = CStr(.Item("PARTICIPANT_ID"))
                    item.GeneratedDMCM = CLng(.Item("AM_DMCM_NO"))
                    item.EnergyWithHold = CDec(.Item("ENERGY_WITHHOLD"))
                    ret.Add(item)
                End With
            End While
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try
        Return ret
    End Function
#End Region

#Region "Main Functions"
    Private ReportCreatedCount As Integer = 0
    Public Async Function GetTPByDateRange(ByVal dateFrom As Date, ByVal dateTo As Date) As Task(Of List(Of AMParticipants))
        Dim ret As New List(Of AMParticipants)
        Try
            Me._WESMBillList = Await Me.GetWESMBillsListAsync(dateFrom, dateTo)
            Me._WESMBillSummaryList = Await Me.GetWESMBillSummaryListAsync(dateFrom, dateTo)
            Me._WESMBillSummaryHisList = Await Me.GetWESMBillSummaryHistoryListAsync(dateFrom, dateTo)

            Me._WTAMainSummaryList = Await Me.GetWTAMainSummaryList(dateFrom, dateTo)
            'Me._WTADetailsSummaryList = Await Me.GetListWESMTransDetailsSummaryAsync(dateFrom, dateTo)
            'Me._WTADetailsSummaryHisList = Await Me.GetListWESMTransDetailsSummaryHistory(dateFrom, dateTo)

            Me._MFCollectionList = Await Me.GetCollectionAllocation(dateFrom, dateTo)
            Me._MFOffsetFromPaymentList = Await Me.GetAMPaymentNewOffsetAR(dateFrom, dateTo)
            Me._ParticipantsList = _WBillHelper.GetAMParticipants()

            Dim IDNumberList As List(Of String) = (From x In Me.WESMBillList
                                                   Select x.IDNumber Distinct).ToList()

            ret = (From x In Me.ParticipantsList
                   Where IDNumberList.Contains(x.IDNumber)
                   Select x).ToList()

            Return ret
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Sub GenerateBIRAcessToRecordReport(ByVal TargetPathFolder As String, ByVal SelectedIDNumber As String, ByVal dateFrom As Date, ByVal dateTo As Date)
        Dim oParticipantInfo As AMParticipants = (From x In Me.ParticipantsList
                                                  Where x.ParticipantID = SelectedIDNumber
                                                  Select x).FirstOrDefault

        Dim _WTAMainSummaryPerTP As List(Of WESMBillAllocCoverSummary) = (From x In Me.WTAMainSummaryList
                                                                          Where x.StlID = oParticipantInfo.IDNumber
                                                                          Select x).ToList

        If _WTAMainSummaryPerTP.Count = 0 Then
            Exit Sub
        End If

        ReportCreatedCount += 1
        Dim _WESMBillsPerTP As List(Of WESMBill) = (From x In Me.WESMBillList
                                                    Where x.IDNumber = oParticipantInfo.IDNumber
                                                    Select x).ToList()
        Dim _WESMBillSummaryPerTP As List(Of WESMBillSummary) = (From x In Me.WESMBillSummaryList
                                                                 Where x.IDNumber.IDNumber = oParticipantInfo.IDNumber
                                                                 Select x).ToList()

        Dim SalesOfMP As Object(,) = New Object(,) {}
        Dim PurchasesOfMP As Object(,) = New Object(,) {}
        Dim MFPaid As Object(,) = New Object(,) {}
        Dim rowCountInSales As Integer = 0
        Dim rowCountInPurchases As Integer = 0
        Dim rowCountInMF As Integer = 0
        Dim i As Integer = 2
        'Sales of Vatable Generator
        Dim GetSalesOfMP = (From x In _WTAMainSummaryPerTP
                            Where x.GrossSale <> 0
                            Select x Order By x.DueDate).ToList()
        rowCountInSales = GetSalesOfMP.Count
        ReDim SalesOfMP(rowCountInSales + 4, 9)
        SalesOfMP(0, 0) = "A. Power Sold/Traded to " & AMModule.CompanyShortName
        SalesOfMP(2, 0) = "Period"
        SalesOfMP(2, 1) = "KWH"
        SalesOfMP(2, 2) = "WESM Transaction No."
        SalesOfMP(2, 3) = "Transaction Date"
        SalesOfMP(1, 4) = "Power Traded to " & AMModule.CompanyShortName
        SalesOfMP(2, 4) = "Energy Fee"
        SalesOfMP(2, 5) = "VAT"
        SalesOfMP(2, 6) = "Total"
        SalesOfMP(1, 7) = "Actual Amount Remitted to " & oParticipantInfo.IDNumber
        SalesOfMP(2, 7) = "Energy Fee"
        SalesOfMP(2, 8) = "VAT"
        SalesOfMP(2, 9) = "Total"
        SalesOfMP(UBound(SalesOfMP, 1), 0) = "Total"
        If rowCountInSales > 0 Then
            For Each Item In GetSalesOfMP
                If Item.TransactionNo = "TS-WF-198F-0002601" Then
                    Dim x0 = 0
                End If
                Dim getWESMBillEnergy = (From x In WESMBillList Where x.InvoiceNumber = Item.TransactionNo And x.ChargeType = EnumChargeType.E Select x).FirstOrDefault
                Dim getWESMBillVAT = (From x In WESMBillList Where x.InvoiceNumber = Item.TransactionNo And x.ChargeType = EnumChargeType.EV Select x).FirstOrDefault
                Dim getWESMBIllSummaryEnergy = (From x In WESMBillSummaryList Where x.INVDMCMNo = Item.TransactionNo And x.ChargeType = EnumChargeType.E Select x).FirstOrDefault
                Dim getWESMBIllSummaryVAT = (From x In WESMBillSummaryList Where x.INVDMCMNo = Item.TransactionNo And x.ChargeType = EnumChargeType.EV Select x).FirstOrDefault
                Dim getWTADetailsSummary = (From x In WTADetailsSummaryList Where x.SellerTransNo = Item.TransactionNo Select x).ToList()

                Dim getWESMBillSummaryHisListEnergy As List(Of WESMBillSummaryHistory) = New List(Of WESMBillSummaryHistory)
                If Not getWESMBIllSummaryEnergy Is Nothing Then
                    getWESMBillSummaryHisListEnergy = (From x In WESMBillSummaryHisList Where x.WESMBillSummaryNo = getWESMBIllSummaryEnergy.WESMBillSummaryNo And x.PaymentType = EnumPaymentNewType.Energy Select x).ToList()
                End If

                Dim getWESMBillSummaryHisListVAT As List(Of WESMBillSummaryHistory) = New List(Of WESMBillSummaryHistory)
                If Not getWESMBIllSummaryVAT Is Nothing Then
                    getWESMBillSummaryHisListVAT = (From x In WESMBillSummaryHisList Where x.WESMBillSummaryNo = getWESMBIllSummaryVAT.WESMBillSummaryNo And x.PaymentType = EnumPaymentNewType.VatOnEnergy Select x).ToList()
                End If

                i += 1
                SalesOfMP(i, 0) = MonthName(Item.DueDate.Month) & " " & Item.DueDate.Year.ToString()
                SalesOfMP(i, 1) = ""
                SalesOfMP(i, 2) = Item.TransactionNo
                SalesOfMP(i, 3) = Item.TransactionDate

                SalesOfMP(i, 4) = If(getWESMBillEnergy Is Nothing, 0, getWESMBillEnergy.Amount)
                SalesOfMP(i, 5) = If(getWESMBillVAT Is Nothing, 0, getWESMBillVAT.Amount)
                SalesOfMP(i, 6) = If(getWESMBillEnergy Is Nothing, 0, getWESMBillEnergy.Amount) + If(getWESMBillVAT Is Nothing, 0, getWESMBillVAT.Amount)

                Dim ActualRemittedInEnergy As Decimal = CDec(getWESMBillSummaryHisListEnergy.Select(Function(x) x.Amount).Sum())
                Dim ActualRemittedInVAT As Decimal = CDec(getWESMBillSummaryHisListVAT.Select(Function(x) x.Amount).Sum())

                SalesOfMP(i, 7) = ActualRemittedInEnergy
                SalesOfMP(i, 8) = ActualRemittedInVAT
                SalesOfMP(i, 9) = ActualRemittedInEnergy + ActualRemittedInVAT

                SalesOfMP(UBound(SalesOfMP, 1), 4) = CDec(SalesOfMP(UBound(SalesOfMP, 1), 4)) + CDec(SalesOfMP(i, 4))
                SalesOfMP(UBound(SalesOfMP, 1), 5) = CDec(SalesOfMP(UBound(SalesOfMP, 1), 5)) + CDec(SalesOfMP(i, 5))
                SalesOfMP(UBound(SalesOfMP, 1), 6) = CDec(SalesOfMP(UBound(SalesOfMP, 1), 6)) + CDec(SalesOfMP(i, 6))
                SalesOfMP(UBound(SalesOfMP, 1), 7) = CDec(SalesOfMP(UBound(SalesOfMP, 1), 7)) + CDec(SalesOfMP(i, 7))
                SalesOfMP(UBound(SalesOfMP, 1), 8) = CDec(SalesOfMP(UBound(SalesOfMP, 1), 8)) + CDec(SalesOfMP(i, 8))
                SalesOfMP(UBound(SalesOfMP, 1), 9) = CDec(SalesOfMP(UBound(SalesOfMP, 1), 9)) + CDec(SalesOfMP(i, 9))
            Next
        End If

        'Purchases of Vatable Generators
        Dim GetPurchasesOfMP = (From x In _WTAMainSummaryPerTP
                                Where x.GrossPurchase <> 0
                                Select x Order By x.DueDate).ToList()

        rowCountInPurchases = GetPurchasesOfMP.Count
        ReDim PurchasesOfMP(rowCountInPurchases + 4, 9)
        PurchasesOfMP(0, 0) = "B. Power Purchased by " & oParticipantInfo.FullName
        PurchasesOfMP(2, 0) = "Period"
        PurchasesOfMP(2, 1) = "KWH"
        PurchasesOfMP(2, 2) = "WESM Transaction No."
        PurchasesOfMP(2, 3) = "Transaction Date"
        PurchasesOfMP(1, 4) = "Power Purchased by " & oParticipantInfo.IDNumber
        PurchasesOfMP(2, 4) = "Energy Fee"
        PurchasesOfMP(2, 5) = "VAT"
        PurchasesOfMP(2, 6) = "Total"
        PurchasesOfMP(1, 7) = "Actual Amount Remitted to " & oParticipantInfo.IDNumber
        PurchasesOfMP(2, 7) = "Energy Fee"
        PurchasesOfMP(2, 8) = "VAT"
        PurchasesOfMP(2, 9) = "Total"
        PurchasesOfMP(UBound(PurchasesOfMP, 1), 0) = "Total"
        rowCountInPurchases = GetPurchasesOfMP.Count
        If rowCountInPurchases > 0 Then
            i = 2
            For Each Item In GetPurchasesOfMP
                Dim getWESMBillEnergy = (From x In WESMBillList Where x.InvoiceNumber = Item.TransactionNo And x.ChargeType = EnumChargeType.E Select x).FirstOrDefault
                Dim getWESMBillVAT = (From x In WESMBillList Where x.InvoiceNumber = Item.TransactionNo And x.ChargeType = EnumChargeType.EV Select x).FirstOrDefault
                Dim getWESMBIllSummaryEnergy = (From x In WESMBillSummaryList Where x.INVDMCMNo = Item.TransactionNo And x.ChargeType = EnumChargeType.E Select x).FirstOrDefault
                Dim getWESMBIllSummaryVAT = (From x In WESMBillSummaryList Where x.INVDMCMNo = Item.TransactionNo And x.ChargeType = EnumChargeType.EV Select x).FirstOrDefault
                Dim getWTADetailsSummary = (From x In WTADetailsSummaryList Where x.BuyerTransNo = Item.TransactionNo Select x).ToList()

                Dim getWESMBillSummaryHisListEnergy As List(Of WESMBillSummaryHistory) = New List(Of WESMBillSummaryHistory)
                If Not getWESMBIllSummaryEnergy Is Nothing Then
                    getWESMBillSummaryHisListEnergy = (From x In WESMBillSummaryHisList Where x.WESMBillSummaryNo = getWESMBIllSummaryEnergy.WESMBillSummaryNo And x.CollectionType = EnumCollectionType.Energy Select x).ToList()
                End If

                Dim getWESMBillSummaryHisListVAT As List(Of WESMBillSummaryHistory) = New List(Of WESMBillSummaryHistory)
                If Not getWESMBIllSummaryVAT Is Nothing Then
                    getWESMBillSummaryHisListVAT = (From x In WESMBillSummaryHisList Where x.WESMBillSummaryNo = getWESMBIllSummaryVAT.WESMBillSummaryNo And x.CollectionType = EnumCollectionType.VatOnEnergy Select x).ToList()
                End If

                i += 1

                PurchasesOfMP(i, 0) = MonthName(Item.DueDate.Month) & " " & Item.DueDate.Year.ToString()
                PurchasesOfMP(i, 1) = ""
                PurchasesOfMP(i, 2) = Item.TransactionNo
                PurchasesOfMP(i, 3) = Item.TransactionDate

                PurchasesOfMP(i, 4) = If(getWESMBillEnergy Is Nothing, 0, getWESMBillEnergy.Amount)
                PurchasesOfMP(i, 5) = If(getWESMBillVAT Is Nothing, 0, getWESMBillVAT.Amount)
                PurchasesOfMP(i, 6) = If(getWESMBillEnergy Is Nothing, 0, getWESMBillEnergy.Amount) + If(getWESMBillVAT Is Nothing, 0, getWESMBillVAT.Amount)

                Dim ActualAmountPaidInEnergy As Decimal = CDec(getWESMBillSummaryHisListEnergy.Select(Function(x) x.Amount).Sum())
                Dim ActualAmountPaidInVAT As Decimal = CDec(getWESMBillSummaryHisListVAT.Select(Function(x) x.Amount).Sum())

                PurchasesOfMP(i, 7) = ActualAmountPaidInEnergy
                PurchasesOfMP(i, 8) = ActualAmountPaidInVAT
                PurchasesOfMP(i, 9) = ActualAmountPaidInEnergy + ActualAmountPaidInVAT

                PurchasesOfMP(UBound(PurchasesOfMP, 1), 4) = CDec(PurchasesOfMP(UBound(PurchasesOfMP, 1), 4)) + CDec(PurchasesOfMP(i, 4))
                PurchasesOfMP(UBound(PurchasesOfMP, 1), 5) = CDec(PurchasesOfMP(UBound(PurchasesOfMP, 1), 5)) + CDec(PurchasesOfMP(i, 5))
                PurchasesOfMP(UBound(PurchasesOfMP, 1), 6) = CDec(PurchasesOfMP(UBound(PurchasesOfMP, 1), 6)) + CDec(PurchasesOfMP(i, 6))
                PurchasesOfMP(UBound(PurchasesOfMP, 1), 7) = CDec(PurchasesOfMP(UBound(PurchasesOfMP, 1), 7)) + CDec(PurchasesOfMP(i, 7))
                PurchasesOfMP(UBound(PurchasesOfMP, 1), 8) = CDec(PurchasesOfMP(UBound(PurchasesOfMP, 1), 8)) + CDec(PurchasesOfMP(i, 8))
                PurchasesOfMP(UBound(PurchasesOfMP, 1), 9) = CDec(PurchasesOfMP(UBound(PurchasesOfMP, 1), 9)) + CDec(PurchasesOfMP(i, 9))
            Next
        End If

        'AMOUNT PAID BY GENERATORS (FOR MARKET FEES)
        Dim getWESMBillMFList = (From x In _WESMBillsPerTP Where x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV Select x Order By x.DueDate).ToList
        Dim getWESMBillMFInv = getWESMBillMFList.Select(Function(x) x.InvoiceNumber).Distinct.ToList()

        rowCountInMF = getWESMBillMFInv.Count
        ReDim MFPaid(rowCountInMF + 3, 5)
        MFPaid(0, 0) = "C. Market Fees Billed by " & AMModule.CompanyShortName
        MFPaid(1, 0) = "Period"
        MFPaid(1, 1) = "KWH"
        MFPaid(1, 2) = "Market Fees"
        MFPaid(1, 3) = "Output Tax"
        MFPaid(1, 4) = "Total Amount Paid"
        MFPaid(1, 5) = "Date Paid"
        MFPaid(UBound(MFPaid, 1), 0) = "Total"
        If rowCountInMF > 0 Then
            i = 1
            For Each item In getWESMBillMFInv
                Dim getWESMBillMF = (From x In getWESMBillMFList Where x.InvoiceNumber = item And x.ChargeType = EnumChargeType.MF Select x).FirstOrDefault
                Dim getWESMBillMFV = (From x In getWESMBillMFList Where x.InvoiceNumber = item And x.ChargeType = EnumChargeType.MFV Select x).FirstOrDefault

                Dim getWESMBIllSummaryMF = (From x In WESMBillSummaryList Where x.INVDMCMNo = item And x.ChargeType = EnumChargeType.MF Select x).FirstOrDefault
                Dim getWESMBIllSummaryMFV = (From x In WESMBillSummaryList Where x.INVDMCMNo = item And x.ChargeType = EnumChargeType.MFV Select x).FirstOrDefault

                Dim getWESMBillSummaryHisListMF As List(Of WESMBillSummaryHistory) = New List(Of WESMBillSummaryHistory)
                Dim getCollectionHisListMF As List(Of CollectionAllocation) = New List(Of CollectionAllocation)
                Dim getOffsetHisListMF As List(Of ARCollection) = New List(Of ARCollection)
                If Not getWESMBIllSummaryMF Is Nothing Then
                    getWESMBillSummaryHisListMF = (From x In WESMBillSummaryHisList Where x.WESMBillSummaryNo = getWESMBIllSummaryMF.WESMBillSummaryNo And x.CollectionType = EnumCollectionType.MarketFees Select x).ToList()
                    getCollectionHisListMF = (From x In MFCollectionList Where x.WESMBillSummaryNo.WESMBillSummaryNo = getWESMBIllSummaryMF.WESMBillSummaryNo And x.CollectionType = EnumCollectionType.MarketFees Select x).ToList()
                    getOffsetHisListMF = (From x In MFOffsetFromPaymentList Where x.WESMBillSummaryNo = getWESMBIllSummaryMF.WESMBillSummaryNo And x.CollectionType = EnumCollectionType.MarketFees Select x).ToList()
                End If

                Dim getWESMBillSummaryHisListMFV As List(Of WESMBillSummaryHistory) = New List(Of WESMBillSummaryHistory)
                Dim getCollectionHisListMFV As List(Of CollectionAllocation) = New List(Of CollectionAllocation)
                Dim getOffsetHisListMFV As List(Of ARCollection) = New List(Of ARCollection)
                If Not getWESMBIllSummaryMFV Is Nothing Then
                    getWESMBillSummaryHisListMFV = (From x In WESMBillSummaryHisList Where x.WESMBillSummaryNo = getWESMBIllSummaryMFV.WESMBillSummaryNo And x.CollectionType = EnumCollectionType.VatOnMarketFees Select x).ToList()
                    getCollectionHisListMFV = (From x In MFCollectionList Where x.WESMBillSummaryNo.WESMBillSummaryNo = getWESMBIllSummaryMFV.WESMBillSummaryNo And x.CollectionType = EnumCollectionType.VatOnMarketFees Select x).ToList()
                    getOffsetHisListMFV = (From x In MFOffsetFromPaymentList Where x.WESMBillSummaryNo = getWESMBIllSummaryMFV.WESMBillSummaryNo And x.CollectionType = EnumCollectionType.VatOnMarketFees Select x).ToList()
                End If

                i += 1
                MFPaid(i, 0) = MonthName(getWESMBillMF.DueDate.Month) & " " & getWESMBillMF.DueDate.Year.ToString()
                MFPaid(i, 1) = ""
                MFPaid(i, 2) = getWESMBillMF.Amount
                MFPaid(i, 3) = If(getWESMBillMFV Is Nothing, 0, getWESMBillMFV.Amount)

                Dim totalAmountPaidInMF As Decimal = getCollectionHisListMF.Select(Function(x) x.Amount).Sum() + getOffsetHisListMF.Select(Function(x) x.AllocationAmount).Sum()
                Dim totalAmountPaidInMFV As Decimal = getCollectionHisListMFV.Select(Function(x) x.Amount).Sum() + getOffsetHisListMFV.Select(Function(x) x.AllocationAmount).Sum()

                MFPaid(i, 4) = totalAmountPaidInMF + totalAmountPaidInMFV
                If Not getCollectionHisListMF Is Nothing Then
                    Dim currDate As Date = getCollectionHisListMF.Select(Function(x) x.AllocationDate).OrderByDescending(Function(x) x).FirstOrDefault()
                    MFPaid(i, 5) = currDate.ToString("MM/dd/yyyy")
                End If
                If Not getOffsetHisListMF Is Nothing Then
                    Dim currDate As Date = getOffsetHisListMF.Select(Function(x) x.AllocationDate).OrderByDescending(Function(x) x).FirstOrDefault()
                    MFPaid(i, 5) = If(CDate(MFPaid(i, 5)) > currDate, MFPaid(i, 5), currDate.ToString("MM/dd/yyyy"))
                End If
                MFPaid(UBound(MFPaid, 1), 2) = CDec(MFPaid(UBound(MFPaid, 1), 2)) + CDec(MFPaid(i, 2))
                MFPaid(UBound(MFPaid, 1), 3) = CDec(MFPaid(UBound(MFPaid, 1), 3)) + CDec(MFPaid(i, 3))
                MFPaid(UBound(MFPaid, 1), 4) = CDec(MFPaid(UBound(MFPaid, 1), 4)) + CDec(MFPaid(i, 4))
            Next
        End If
        'Generate Excel File
        Me.SaveToExcelGen(TargetPathFolder, dateFrom, dateTo, oParticipantInfo,
                          SalesOfMP, PurchasesOfMP, MFPaid)
    End Sub
    Public Sub SaveToExcelGen(ByVal TargetPathFolder As String, ByVal dateFrom As Date, ByVal dateTo As Date, ByVal ParticipantInfo As AMParticipants,
                              ByVal sales As Object(,), ByVal purchases As Object(,), ByVal amountPaid As Object(,))

        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlContentHeader As Excel.Range
        Dim xlSalesOfMP As Excel.Range
        Dim xlPurchasesOfMP As Excel.Range
        Dim xlAmountPaid As Excel.Range

        Dim ContentHeader As Object(,) = New Object(,) {}
        Dim misValue As Object = System.Reflection.Missing.Value
        Dim TemplatePathFile = AppDomain.CurrentDomain.BaseDirectory & "Excel_Template\BIRAccessToRecord.xltm"
        Dim RowIndx As Integer = 0

        xlAmountPaid = Nothing
        xlPurchasesOfMP = Nothing
        xlSalesOfMP = Nothing
        xlContentHeader = Nothing
        xlRowRange2 = Nothing
        xlRowRange1 = Nothing

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add(TemplatePathFile)
        xlWorkSheet = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet.Name = "BIR Access To Record"
        Try
            '********************************************* Supply Content Header
            ReDim ContentHeader(5, 0)
            ContentHeader(0, 0) = AMModule.CompanyFullName
            ContentHeader(2, 0) = "MP Name: " & ParticipantInfo.FullName.ToString()
            ContentHeader(3, 0) = "MP ID No.: " & ParticipantInfo.IDNumber.ToString()
            ContentHeader(4, 0) = "Transactions for " & dateFrom.ToString("MMM yyyy") & "-" & dateTo.ToString("MMM yyyy")
            RowIndx = 1
            xlRowRange1 = DirectCast(xlWorkSheet.Cells(RowIndx, 2), Excel.Range)
            RowIndx += 4
            xlRowRange2 = DirectCast(xlWorkSheet.Cells(5, 2), Excel.Range)
            xlContentHeader = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
            xlContentHeader.Value = ContentHeader

            '********************************************* Supply Sales of Market Participant           
            If UBound(sales, 2) > 0 Then
                RowIndx += 2
                xlRowRange1 = DirectCast(xlWorkSheet.Cells(RowIndx, 2), Excel.Range)
                RowIndx += UBound(sales, 1)
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(RowIndx, UBound(sales, 2) + 2), Excel.Range)
                xlSalesOfMP = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlSalesOfMP.Value = sales
            End If
            '********************************************* Supply Purchases of Market Participant
            If UBound(purchases, 2) > 0 Then
                RowIndx += 2
                xlRowRange1 = DirectCast(xlWorkSheet.Cells(RowIndx, 2), Excel.Range)
                RowIndx += UBound(purchases, 1)
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(RowIndx, UBound(purchases, 2) + 2), Excel.Range)
                xlPurchasesOfMP = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlPurchasesOfMP.Value = purchases
            End If
            '********************************************* Supply Amount Paid  of Market Participant
            If UBound(amountPaid, 2) > 0 Then
                RowIndx += 2
                xlRowRange1 = DirectCast(xlWorkSheet.Cells(RowIndx, 2), Excel.Range)
                RowIndx += UBound(amountPaid, 1)
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(RowIndx, UBound(amountPaid, 2) + 2), Excel.Range)
                xlAmountPaid = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlAmountPaid.Value = amountPaid
            End If

            Dim FileName As String = TargetPathFolder & "\" & ParticipantInfo.ParticipantID & "_BATR_" & dateFrom.ToString("MMMyyyy") & "-" & dateTo.ToString("MMMyyyy") & ".xlsm"
            FileName = FileExistIncrementer(FileName)
            xlWorkBook.SaveAs(FileName, Excel.XlFileFormat.xlOpenXMLWorkbookMacroEnabled, misValue, misValue, misValue, misValue,
                        Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            xlWorkBook.Close(False)
            xlApp.Quit()
            releaseObject(CObj(xlAmountPaid))
            releaseObject(CObj(xlPurchasesOfMP))
            releaseObject(CObj(xlSalesOfMP))
            releaseObject(CObj(xlContentHeader))
            releaseObject(CObj(xlRowRange2))
            releaseObject(CObj(xlRowRange1))
            releaseObject(CObj(xlWorkSheet))
            releaseObject(CObj(xlWorkBook))
            releaseObject(CObj(xlApp))
        End Try
    End Sub
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub
    Public Shared Function FileExistIncrementer(ByVal orginialFileName As String) As String
        Dim counter As Integer = 0
        Dim NewFileName As String = orginialFileName
        While File.Exists(NewFileName)
            counter = counter + 1
            NewFileName = String.Format("{0}\{1}{2}{3}", Path.GetDirectoryName(orginialFileName), Path.GetFileNameWithoutExtension(orginialFileName), " (" & counter.ToString() & ")", Path.GetExtension(orginialFileName))
        End While
        Return NewFileName
    End Function

    Public Function GetReportCreatedCount() As Integer
        Return ReportCreatedCount
    End Function
#End Region

End Class
