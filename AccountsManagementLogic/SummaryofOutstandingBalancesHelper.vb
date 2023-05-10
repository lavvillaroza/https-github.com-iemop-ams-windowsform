
Option Explicit On
Option Strict On


Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Imports Excel = Microsoft.Office.Interop.Excel

Public Class SummaryofOutstandingBalancesHelper

#Region "WESMBillHelper"
    Public _WBillHelper As WESMBillHelper
    Private ReadOnly Property WBillHelper() As WESMBillHelper
        Get
            Return _WBillHelper
        End Get
    End Property
#End Region

#Region "DAL"
    Private _DataAccess As DAL
    Public ReadOnly Property DataAccess() As DAL
        Get
            Return Me._DataAccess
        End Get
    End Property
#End Region

    Public Sub New()
        'Get the current instance of the dal
        Me._DataAccess = DAL.GetInstance()
        Me._WBillHelper = WESMBillHelper.GetInstance      
        Me._AMParticipants = WBillHelper.GetAMParticipants()

        Me._AMDueDateList = (From x In Me.WBillHelper.GetDueDateInWESMBillSummary() _
                            Select x Order By x Descending).Distinct.ToList()
    End Sub

#Region "Property of WESMBillSummary"
    Private _WESMBillSummaryList As New List(Of WESMBillSummary)
    Public ReadOnly Property WESMBillSummaryList() As List(Of WESMBillSummary)
        Get
            Return _WESMBillSummaryList
        End Get
    End Property
#End Region

#Region "Property of Transaction Summary"
    Private _WESMTransactionSummary As List(Of WESMBillAllocCoverSummary)
    Public Property WESMTransactionSummary() As List(Of WESMBillAllocCoverSummary)
        Get
            Return _WESMTransactionSummary
        End Get
        Set(ByVal value As List(Of WESMBillAllocCoverSummary))
            _WESMTransactionSummary = value
        End Set
    End Property
#End Region

#Region "Property of AMParticipants"
    Private _AMParticipants As New List(Of AMParticipants)
    Public ReadOnly Property AMParticipants() As List(Of AMParticipants)
        Get
            Return _AMParticipants
        End Get
    End Property
#End Region

#Region "Property of DueDate List"
    Private _AMDueDateList As New List(Of Date)
    Public ReadOnly Property AMDueDateList() As List(Of Date)
        Get
            Return _AMDueDateList
        End Get
    End Property
#End Region

#Region "Get WESM Transaction Summary"
    Private Function GetListWESMTransCoverSummary() As List(Of WESMBillAllocCoverSummary)
        Dim ret As New List(Of WESMBillAllocCoverSummary)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.* FROM AM_WESM_ALLOC_COVER_SUMMARY A "
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetListWESMTransCoverSummary(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetListWESMTransCoverSummary(ByVal dr As IDataReader) As List(Of WESMBillAllocCoverSummary)
        Dim result As New List(Of WESMBillAllocCoverSummary)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As New WESMBillAllocCoverSummary
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
                    item.NSSFlowBack = CDec(.Item("NSS_FLOWBACK"))
                    item.VatOnSales = CDec(.Item("VAT_ON_SALES"))
                    item.VatOnPurchases = CDec(.Item("VAT_ON_PURCHASES"))
                    item.EWTSales = CDec(.Item("EWT_SALES"))
                    item.EWTPurchases = CDec(.Item("EWT_PURCHASES"))
                    item.GMR = CDec(.Item("GMR"))
                    item.SpotQty = CDec(.Item("SPOT_QTY"))
                    item.MarketFeesRate = CDec(.Item("MARKET_FEES_RATE"))
                    item.Remarks = CStr(.Item("REMARKS"))
                    'item.ListWBAllocDisDetails = GetWBAllocDisDetails(CLng(.Item("SUMMARY_ID")))
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
    Private Function GetWBAllocDisDetails(ByVal summaryID As Long) As List(Of WESMBillAllocDisaggDetails)
        Dim ret As New List(Of WESMBillAllocDisaggDetails)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.* FROM AM_WESM_ALLOC_DISAGG_DETAILS A " & vbNewLine _
                              & "WHERE A.SUMMARY_ID =  " & summaryID
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetWBAllocDisDetails(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetWBAllocDisDetails(ByVal dr As IDataReader) As List(Of WESMBillAllocDisaggDetails)
        Dim result As New List(Of WESMBillAllocDisaggDetails)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As New WESMBillAllocDisaggDetails
                    item.BillingID = CStr(.Item("BILLING_ID"))
                    item.FacilityType = CStr(.Item("FACILITY_TYPE"))
                    item.NonVatableTag = CStr(.Item("NON_VATABLE_TAG"))
                    item.ZeroRatedTag = CStr(.Item("ZERO_RATED_TAG"))
                    item.WHTTag = CStr(.Item("WHT"))
                    item.ITHTag = CStr(.Item("ITH"))
                    item.NetSellerBuyerTag = CStr(.Item("NET_SELLER_BUYER_TAG"))
                    item.VatableSales = CDec(.Item("VATABLE_SALES"))
                    item.ZeroRatedSales = CDec(.Item("ZERO_RATED_SALES"))
                    item.ZeroRatedEcoZoneSales = CDec(.Item("ZERO_RATED_ECOZONE_SALES"))
                    item.VatablePurchases = CDec(.Item("VATABLE_PURCHASES"))
                    item.ZeroRatedPurchases = CDec(.Item("ZERO_RATED_PURCHASES"))
                    item.ZeroRatedEcoZonePurchases = CDec(.Item("ZERO_RATED_ECOZONE_PURCHASES"))
                    item.VatOnSales = CDec(.Item("VAT_ON_SALES"))
                    item.VatOnPurchases = CDec(.Item("VAT_ON_PURCHASES"))
                    item.EWT = CDec(.Item("EWT"))
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

    Public Sub InitializedBasedOnSelectedDate(ByVal SelectedDueDate As Date, ByVal selectAll As Boolean)
        Me._WESMTransactionSummary = GetListWESMTransCoverSummary()

        If selectAll = True Then
            Me._WESMBillSummaryList = (From x In WBillHelper.GetWESMBillSummaryWithBalance()
                                       Select x).ToList()
        Else
            Me._WESMBillSummaryList = (From x In WBillHelper.GetWESMBillSummaryByDate(SelectedDueDate)
                                       Select x).ToList()
            Me._WESMTransactionSummary = GetListWESMTransCoverSummary()
        End If
    End Sub

    Public Sub ResetWESMBillSummaryList()
        Me._WESMBillSummaryList = New List(Of WESMBillSummary)
    End Sub

#Region "Method and Functions For Generation of Collection And Payment Report on MS-Excel"
    Public Sub CreateOutstandingSummaryReport(ByVal SavingPathName As String, ByVal SelectedDueDate As Date, ByVal SelectAll As Boolean)
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet1 As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlEnergyOB As Excel.Range
        Dim xlVATOB As Excel.Range
        Dim xlMFOB As Excel.Range

        Dim GetDateNow As Date = CDate(AMModule.SystemDate.ToShortDateString)
        Dim misValue As Object = System.Reflection.Missing.Value
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0
        Dim TemplatePathFile = AppDomain.CurrentDomain.BaseDirectory & "Excel_Template\OutstandingBalancesSummary.xltm"

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add(TemplatePathFile)

        'Start Energy Outstanding Balances ****************************************************************************************************************************************************
        Dim ForEnergyOB As New List(Of WESMBillSummary)
        If SelectAll = True Then
            ForEnergyOB = (From x In Me.WESMBillSummaryList _
                           Where x.ChargeType = EnumChargeType.E _
                           And x.EndingBalance <> 0 _
                           Or (x.EnergyWithholdStatus <> EnumEnergyWithholdStatus.PaidEWT _
                                And x.EndingBalance = 0) _
                           Select x).ToList
        Else
            ForEnergyOB = (From x In Me.WESMBillSummaryList _
                           Where x.ChargeType = EnumChargeType.E _
                           And x.EndingBalance <> 0 _
                           Or (x.EnergyWithholdStatus <> EnumEnergyWithholdStatus.PaidEWT _
                                And x.EndingBalance = 0) _
                           And x.DueDate = CDate(SelectedDueDate.ToShortDateString) _
                           Select x).ToList
        End If
 

        Dim DistinctEBP = (From x In ForEnergyOB
                            Order By x.WESMBillBatchNo, x.BillPeriod, x.DueDate
                            Group x By x.WESMBillBatchNo, x.BillPeriod, x.DueDate _
                            Into y = Group Let Grp = New With {.WESMBillBatchNo = WESMBillBatchNo, .BillPeriod = BillPeriod, .DueDate = DueDate}
                            Select Grp).ToList()

        'Energy Allocation Header Setup
        Dim EnergyOBHeader As Object(,) = New Object(,) {}
        ReDim EnergyOBHeader(1, 10)
        EnergyOBHeader(1, 0) = "Particulars"
        EnergyOBHeader(1, 1) = "BillingBatchNo"
        EnergyOBHeader(1, 2) = "IDNumber"
        EnergyOBHeader(1, 3) = "ParticipantID"
        EnergyOBHeader(1, 4) = "InvoiceNumber"
        EnergyOBHeader(1, 5) = "OrigDueDate"
        EnergyOBHeader(1, 6) = "NewDueDate"
        EnergyOBHeader(1, 7) = "WithHoldingTax"
        EnergyOBHeader(1, 8) = "OriginalBalance"
        EnergyOBHeader(1, 9) = "OutstandingBalance"
        EnergyOBHeader(1, 10) = "ChargeType"

        'EnergyOutstandingBalance sheets
        xlWorkSheet1 = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(3, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(4, UBound(EnergyOBHeader, 2) + 1), Excel.Range)
        xlEnergyOB = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlEnergyOB.Value = EnergyOBHeader

        For Each BP In DistinctEBP
            Dim lrow1 As Integer = xlWorkSheet1.Range("A" & xlWorkSheet1.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
            Dim GetEnergyOB = (From x In ForEnergyOB _
                                 Where x.WESMBillBatchNo = BP.WESMBillBatchNo And x.BillPeriod = BP.BillPeriod And x.DueDate = BP.DueDate _
                                 Select x Order By x.DueDate, x.EndingBalance).ToList()
            'Dim getWESMBillList = (From x In WESMBillList Where x.BillingPeriod = BP.BillPeriod And x.DueDate = BP.DueDate And x.ChargeType = EnumChargeType.E Select x).ToList()
            If GetEnergyOB.Count <> 0 Then
                Dim EnergyOBArr As Object(,) = Me.GenerateWESMBillSummaryOBArr(GetEnergyOB, EnumChargeType.E)
                xlRowRange1 = DirectCast(xlWorkSheet1.Cells(lrow1, 1), Excel.Range)
                xlRowRange2 = DirectCast(xlWorkSheet1.Cells(lrow1 + UBound(EnergyOBArr, 1), UBound(EnergyOBHeader, 2) + 1), Excel.Range)
                xlEnergyOB = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
                xlEnergyOB.Value = EnergyOBArr
            End If
        Next
        'End Energy Outstanding Balances ****************************************************************************************************************************************************


        'Start VAT Outstanding Balances *******************************************************************************************************************************************************
        Dim ForVATOB as New List(Of WESMBillSummary)

        If SelectAll = True Then
            ForVATOB = (From x In Me.WESMBillSummaryList _
                           Where x.ChargeType = EnumChargeType.EV _
                           And x.EndingBalance <> 0 _
                           Select x).ToList
        Else
            ForVATOB = (From x In Me.WESMBillSummaryList _
                           Where x.ChargeType = EnumChargeType.EV _
                           And x.EndingBalance <> 0 _
                           And x.DueDate = CDate(SelectedDueDate.ToShortDateString) _
                           Select x).ToList
        End If

        Dim DistinctEVBP = (From x In ForVATOB
                            Order By x.WESMBillBatchNo, x.BillPeriod, x.DueDate
                            Group x By x.WESMBillBatchNo, x.BillPeriod, x.DueDate _
                            Into y = Group Let Grp = New With {.WESMBillBatchNo = WESMBillBatchNo, .BillPeriod = BillPeriod, .DueDate = DueDate}
                            Select Grp).ToList()

        'VAT OB Header Setup
        Dim VATOBHeader As Object(,) = New Object(,) {}
        ReDim VATOBHeader(1, 9)
        VATOBHeader(1, 0) = "Particulars"
        VATOBHeader(1, 1) = "BillingBatchNo"
        VATOBHeader(1, 2) = "IDNumber"
        VATOBHeader(1, 3) = "ParticipantID"
        VATOBHeader(1, 4) = "InvoiceNumber"
        VATOBHeader(1, 5) = "OrigDueDate"
        VATOBHeader(1, 6) = "NewDueDate"
        VATOBHeader(1, 7) = "OriginalBalance"
        VATOBHeader(1, 8) = "OutstandingBalance"
        VATOBHeader(1, 9) = "ChargeType"

        'VAT OB sheets
        xlWorkSheet1 = CType(xlWorkBook.Sheets(2), Excel.Worksheet)
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(3, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(4, UBound(VATOBHeader, 2) + 1), Excel.Range)
        xlVATOB = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlVATOB.Value = VATOBHeader

        For Each BP In DistinctEVBP
            Dim lrow1 As Integer = xlWorkSheet1.Range("A" & xlWorkSheet1.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
            Dim GetVATOB = (From x In ForVATOB _
                              Where x.WESMBillBatchNo = BP.WESMBillBatchNo And x.BillPeriod = BP.BillPeriod And x.DueDate = BP.DueDate _
                              Select x Order By x.DueDate, x.EndingBalance).ToList()
            'Dim getWESMBillList = (From x In WESMBillList Where x.BillingPeriod = BP.BillPeriod And x.DueDate = BP.DueDate And x.ChargeType = EnumChargeType.EV Select x).ToList()
            If GetVATOB.Count <> 0 Then
                Dim VATOBArr As Object(,) = Me.GenerateWESMBillSummaryOBArr(GetVATOB, EnumChargeType.EV)
                xlRowRange1 = DirectCast(xlWorkSheet1.Cells(lrow1, 1), Excel.Range)
                xlRowRange2 = DirectCast(xlWorkSheet1.Cells(lrow1 + UBound(VATOBArr, 1), UBound(VATOBHeader, 2) + 1), Excel.Range)
                xlVATOB = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
                xlVATOB.Value = VATOBArr
            End If

        Next
        'End VATAllocation  Outstanding Balances *******************************************************************************************************************************************************

        'Start MarketFeesAllocation ************************************************************************************************************************************************
        Dim ForMFMFVOB As New List(Of WESMBillSummary)

        If SelectAll = True Then
            ForMFMFVOB = (From x In Me.WESMBillSummaryList _
                          Where (x.ChargeType = EnumChargeType.MF _
                                 Or x.ChargeType = EnumChargeType.MFV) _
                             And x.EndingBalance <> 0 _
                             Select x).ToList
        Else
            ForMFMFVOB = (From x In Me.WESMBillSummaryList _
                          Where (x.ChargeType = EnumChargeType.MF _
                                 Or x.ChargeType = EnumChargeType.MFV) _
                             And x.EndingBalance <> 0 _
                             And x.DueDate = CDate(SelectedDueDate.ToShortDateString) _
                             Select x).ToList
        End If

        Dim DistinctMFMFVBP = (From x In ForMFMFVOB
                            Order By x.WESMBillBatchNo, x.BillPeriod, x.DueDate
                            Group x By x.WESMBillBatchNo, x.BillPeriod, x.DueDate _
                            Into y = Group Let Grp = New With {.WESMBillBatchNo = WESMBillBatchNo, .BillPeriod = BillPeriod, .DueDate = DueDate}
                            Select Grp).ToList()


        'Energy Allocation Header Setup
        Dim MFOBHeader As Object(,) = New Object(,) {}
        ReDim MFOBHeader(1, 10)
        MFOBHeader(1, 0) = "Particulars"
        MFOBHeader(1, 1) = "BillingBatchNo"
        MFOBHeader(1, 2) = "IDNumber"
        MFOBHeader(1, 3) = "ParticipantID"
        MFOBHeader(1, 4) = "InvoiceNumber"
        MFOBHeader(1, 5) = "OrigDueDate"
        MFOBHeader(1, 6) = "NewDueDate"
        MFOBHeader(1, 7) = "MFBase"
        MFOBHeader(1, 8) = "MFVAT"
        MFOBHeader(1, 9) = "OutstandingBalance"
        MFOBHeader(1, 10) = "ChargeType"

        'MFMFVAllocation sheets
        xlWorkSheet1 = CType(xlWorkBook.Sheets(3), Excel.Worksheet)
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(3, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(4, UBound(MFOBHeader, 2) + 1), Excel.Range)
        xlMFOB = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlMFOB.Value = MFOBHeader

        For Each BP In DistinctMFMFVBP
            Dim lrow1 As Integer = xlWorkSheet1.Range("A" & xlWorkSheet1.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
            Dim GetMFMFVOB = (From x In ForMFMFVOB _
                                 Where x.WESMBillBatchNo = BP.WESMBillBatchNo And x.BillPeriod = BP.BillPeriod And x.DueDate = BP.DueDate _
                                 Select x Order By x.DueDate, x.EndingBalance).ToList()
            'Dim getWESMBillList = (From x In WESMBillList Where x.BillingPeriod = BP.BillPeriod And x.DueDate = BP.DueDate And (x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV) Select x).ToList()
            If GetMFMFVOB.Count <> 0 Then
                Dim MFMFVBOArr As Object(,) = Me.GenerateWESMBillSummaryOBArr(GetMFMFVOB, EnumChargeType.MF)

                xlRowRange1 = DirectCast(xlWorkSheet1.Cells(lrow1, 1), Excel.Range)
                xlRowRange2 = DirectCast(xlWorkSheet1.Cells(lrow1 + UBound(MFMFVBOArr, 1), UBound(MFOBHeader, 2) + 1), Excel.Range)
                xlMFOB = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
                xlMFOB.Value = MFMFVBOArr
            End If

        Next

        'End   MarketFeesAllocation ************************************************************************************************************************************************
        Dim FileName As String = ""
        If SelectedDueDate = DateTime.MinValue Then
            FileName = "Summary of OutstandingBalances as of  " & WBillHelper.GetSystemDate.ToString("MMMMddyyyy")
        Else
            FileName = "Summary of OutstandingBalances (DueDate " & SelectedDueDate.ToString("MMM-dd-yyyy") & ")"
        End If        

        xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbookMacroEnabled, misValue, misValue, misValue, misValue,
                    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
        xlWorkBook.Close(False)
        xlApp.Quit()

        releaseObject(xlMFOB)
        releaseObject(xlVATOB)
        releaseObject(xlEnergyOB)
        releaseObject(xlRowRange2)
        releaseObject(xlRowRange1)
        releaseObject(xlWorkSheet1)
        releaseObject(xlWorkBook)
        releaseObject(xlApp)
    End Sub

    Private Function GenerateWESMBillSummaryOBArr(ByVal WESMBillSummaryARList As List(Of WESMBillSummary), _
                                                  ByVal iChargeType As EnumChargeType) As Object(,)
        Dim ret As Object(,) = New Object(,) {}
        Dim ObjDicInvNo As New Dictionary(Of String, Integer)
        Dim ObjDicSeq As New Dictionary(Of Integer, Integer)
        Dim RowIndex As Integer = 0
        Dim DistinctGroup = (From x In WESMBillSummaryARList Select x.INVDMCMNo, x.WESMBillBatchNo).Distinct.ToList
        Dim RowCount As Integer = DistinctGroup.Count()

        Select Case iChargeType
            Case EnumChargeType.E
                Dim EnergyArr As Object(,) = New Object(,) {}
                ReDim EnergyArr(RowCount, 10)
                For Each GroupItem In DistinctGroup
                    Dim GetWESMBillSummaryAR As WESMBillSummary = (From x In WESMBillSummaryARList
                                                                   Where x.INVDMCMNo = GroupItem.INVDMCMNo _
                                                           And x.WESMBillBatchNo = GroupItem.WESMBillBatchNo
                                                                   Select x Order By x.WESMBillBatchNo).FirstOrDefault
                    'Dim GetEWTinWESMTransaction As WESMBillAllocCoverSummary = (From x In Me.WESMTransactionSummary Where x.TransactionNo = GroupItem.INVDMCMNo Select x).FirstOrDefault

                    If GetWESMBillSummaryAR Is Nothing Then
                        Exit For
                    End If
                    EnergyArr(RowIndex, 0) = GetWESMBillSummaryAR.BillingRemarks
                    EnergyArr(RowIndex, 1) = GetWESMBillSummaryAR.WESMBillBatchNo
                    EnergyArr(RowIndex, 2) = GetWESMBillSummaryAR.IDNumber.IDNumber
                    EnergyArr(RowIndex, 3) = GetWESMBillSummaryAR.IDNumber.ParticipantID
                    EnergyArr(RowIndex, 4) = GetWESMBillSummaryAR.INVDMCMNo
                    EnergyArr(RowIndex, 5) = GetWESMBillSummaryAR.DueDate
                    EnergyArr(RowIndex, 6) = GetWESMBillSummaryAR.NewDueDate
                    EnergyArr(RowIndex, 7) = GetWESMBillSummaryAR.EnergyWithhold
                    'If GetEWTinWESMTransaction Is Nothing Then
                    '    EnergyArr(RowIndex, 7) = GetWESMBillSummaryAR.EnergyWithhold
                    'Else
                    '    EnergyArr(RowIndex, 7) = GetEWTinWESMTransaction.EWTSales + GetEWTinWESMTransaction.EWTPurchases
                    'End If

                    EnergyArr(RowIndex, 8) = GetWESMBillSummaryAR.BeginningBalance
                    EnergyArr(RowIndex, 9) = GetWESMBillSummaryAR.EndingBalance
                    EnergyArr(RowIndex, 10) = GetWESMBillSummaryAR.BalanceType.ToString
                    RowIndex += 1
                Next
                ret = EnergyArr

            Case EnumChargeType.EV
                Dim VATArr As Object(,) = New Object(,) {}
                ReDim VATArr(RowCount, 9)
                For Each GroupItem In DistinctGroup
                    Dim GetWESMBillSummaryAR As WESMBillSummary = (From x In WESMBillSummaryARList _
                                                          Where x.INVDMCMNo = GroupItem.INVDMCMNo _
                                                          And x.WESMBillBatchNo = GroupItem.WESMBillBatchNo _
                                                          Select x Order By x.WESMBillBatchNo).FirstOrDefault

                    'Dim getRemarks = (From x In oWESMBillList Where x.InvoiceNumber = GroupItem.INVDMCMNo Select x).FirstOrDefault
                    'Dim remarks As String = ""
                    If GetWESMBillSummaryAR Is Nothing Then
                        Exit For
                    End If
                    'If Not getRemarks Is Nothing Then
                    '    remarks = getRemarks.Remarks
                    'End If
                    VATArr(RowIndex, 0) = GetWESMBillSummaryAR.BillingRemarks
                    VATArr(RowIndex, 1) = GetWESMBillSummaryAR.WESMBillBatchNo
                    VATArr(RowIndex, 2) = GetWESMBillSummaryAR.IDNumber.IDNumber
                    VATArr(RowIndex, 3) = GetWESMBillSummaryAR.IDNumber.ParticipantID
                    VATArr(RowIndex, 4) = GetWESMBillSummaryAR.INVDMCMNo
                    VATArr(RowIndex, 5) = GetWESMBillSummaryAR.DueDate
                    VATArr(RowIndex, 6) = GetWESMBillSummaryAR.NewDueDate
                    VATArr(RowIndex, 7) = GetWESMBillSummaryAR.BeginningBalance
                    VATArr(RowIndex, 8) = GetWESMBillSummaryAR.EndingBalance
                    VATArr(RowIndex, 9) = GetWESMBillSummaryAR.BalanceType.ToString
                    RowIndex += 1
                Next
                ret = VATArr

            Case EnumChargeType.MF, EnumChargeType.MFV
                Dim MFArr As Object(,) = New Object(,) {}
                ReDim MFArr(RowCount, 10)

                For Each GroupItem In DistinctGroup
                    Dim GetWESMBillSummaryAR As List(Of WESMBillSummary) = (From x In WESMBillSummaryARList _
                                                                            Where x.INVDMCMNo = GroupItem.INVDMCMNo _
                                                                            And x.WESMBillBatchNo = GroupItem.WESMBillBatchNo _
                                                                            Select x Order By x.WESMBillBatchNo, x.ChargeType).ToList()
                    'Dim getRemarks = (From x In oWESMBillList Where x.InvoiceNumber = GroupItem.INVDMCMNo Select x).FirstOrDefault
                    'Dim remarks As String = ""                    
                    'If Not getRemarks Is Nothing Then
                    '    remarks = getRemarks.Remarks
                    'End If
                    Dim CheckItem As Boolean = False
                    For Each item In GetWESMBillSummaryAR
                        If GetWESMBillSummaryAR.Count = 1 Then
                            If item.ChargeType = EnumChargeType.MF Then
                                MFArr(RowIndex, 0) = item.BillingRemarks
                                MFArr(RowIndex, 1) = item.WESMBillBatchNo
                                MFArr(RowIndex, 2) = item.IDNumber.IDNumber
                                MFArr(RowIndex, 3) = item.IDNumber.ParticipantID
                                MFArr(RowIndex, 4) = item.INVDMCMNo
                                MFArr(RowIndex, 5) = item.DueDate
                                MFArr(RowIndex, 6) = item.NewDueDate
                                MFArr(RowIndex, 7) = item.EndingBalance
                                MFArr(RowIndex, 9) = item.EndingBalance
                                MFArr(RowIndex, 10) = item.BalanceType.ToString
                            ElseIf item.ChargeType = EnumChargeType.MFV Then
                                MFArr(RowIndex, 0) = item.BillingRemarks
                                MFArr(RowIndex, 1) = item.WESMBillBatchNo
                                MFArr(RowIndex, 2) = item.IDNumber.IDNumber
                                MFArr(RowIndex, 3) = item.IDNumber.ParticipantID
                                MFArr(RowIndex, 4) = item.INVDMCMNo
                                MFArr(RowIndex, 5) = item.DueDate
                                MFArr(RowIndex, 6) = item.NewDueDate
                                MFArr(RowIndex, 8) = item.EndingBalance                                
                                MFArr(RowIndex, 9) = item.EndingBalance
                                MFArr(RowIndex, 10) = item.BalanceType.ToString
                            End If
                        ElseIf GetWESMBillSummaryAR.Count = 2 Then
                            If item.ChargeType = EnumChargeType.MF Then
                                MFArr(RowIndex, 0) = item.BillingRemarks
                                MFArr(RowIndex, 1) = item.WESMBillBatchNo
                                MFArr(RowIndex, 2) = item.IDNumber.IDNumber
                                MFArr(RowIndex, 3) = item.IDNumber.ParticipantID
                                MFArr(RowIndex, 4) = item.INVDMCMNo
                                MFArr(RowIndex, 5) = item.DueDate
                                MFArr(RowIndex, 6) = item.NewDueDate
                                MFArr(RowIndex, 7) = item.EndingBalance
                                MFArr(RowIndex, 9) = item.EndingBalance
                                MFArr(RowIndex, 10) = item.BalanceType.ToString
                            ElseIf item.ChargeType = EnumChargeType.MFV Then
                                MFArr(RowIndex, 8) = item.EndingBalance
                                MFArr(RowIndex, 9) = CDec(MFArr(RowIndex, 7)) + item.EndingBalance
                            End If
                        End If                        
                    Next
                    RowIndex += 1
                Next
                ret = MFArr
        End Select
        Return ret
    End Function

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
#End Region
End Class
