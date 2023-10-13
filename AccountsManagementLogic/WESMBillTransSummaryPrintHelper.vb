Imports AccountsManagementObjects
Imports AccountsManagementDataAccess

Public Class WESMBillTransSummaryPrintHelper
    Public Sub New()
        'Get the current instance of the dal
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._DataAccess = DAL.GetInstance()
        Me._ListWESMBill = New List(Of WESMBill)
        Me._ListWBAllocCoverSummary = New List(Of WESMBillAllocCoverSummary)
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

#Region "Get List of Billing Period"
    Public Function GetListBillingperiod() As List(Of Integer)
        Dim ret As New List(Of Integer)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT DISTINCT BILLING_PERIOD FROM AM_WESM_ALLOC_COVER_SUMMARY ORDER BY BILLING_PERIOD"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetListBillingperiod(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret

    End Function

    Private Function GetListBillingperiod(ByVal dr As IDataReader) As List(Of Integer)
        Dim result As New List(Of Integer)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As Integer = .Item("BILLING_PERIOD")
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

#Region "Get List of STL RUn"
    Public Function GetListSTLRun(ByVal bpNo As Integer) As List(Of String)
        Dim ret As New List(Of String)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT DISTINCT STL_RUN, TO_NUMBER(REPLACE(STL_RUN, 'F','')) AS SEQ_STL_RUN FROM AM_WESM_ALLOC_COVER_SUMMARY WHERE BILLING_PERIOD =  " & bpNo & " ORDER BY SEQ_STL_RUN DESC"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetListSTLRun(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetListSTLRun(ByVal dr As IDataReader) As List(Of String)
        Dim result As New List(Of String)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As String = CStr(.Item("STL_RUN"))
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

#Region "Get List of Participant"
    Public Function GetListParticipant(ByVal bpNo As Integer, ByVal stlRun As String) As List(Of String)
        Dim ret As New List(Of String)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT DISTINCT STL_ID FROM AM_WESM_ALLOC_COVER_SUMMARY WHERE BILLING_PERIOD =  " & bpNo & " AND STL_RUN = '" & stlRun & "' ORDER BY STL_ID"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetListParticipant(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetListParticipant(ByVal dr As IDataReader) As List(Of String)
        Dim result As New List(Of String)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As String = CStr(.Item("STL_ID"))
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

#Region "Get List of WESM Transaction Summary"
    Private Function GetListWESMTransSummary(ByVal bpNo As Integer, ByVal stlRun As String, ByVal participantID As String) As List(Of WESMBillAllocCoverSummary)
        Dim ret As New List(Of WESMBillAllocCoverSummary)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.* FROM AM_WESM_ALLOC_COVER_SUMMARY A " & vbNewLine _
                              & "WHERE A.BILLING_PERIOD =  " & bpNo & " And A.STL_RUN = '" & stlRun & "' AND A.STL_ID = '" & participantID & "'"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetListWESMTransSummary(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetListWESMTransSummary(ByVal dr As IDataReader) As List(Of WESMBillAllocCoverSummary)
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
                    item.GenXAmount = CStr(.Item("GENX_AMT"))
                    item.ListWBAllocDisDetails = GetWBAllocDisDetails(CLng(.Item("SUMMARY_ID")))
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
                    item.STLID = CStr(.Item("STL_ID"))
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

    Private _ListWESMBill As List(Of WESMBill)
    Public Property ListWESMBill() As List(Of WESMBill)
        Get
            Return _ListWESMBill
        End Get
        Set(ByVal value As List(Of WESMBill))
            _ListWESMBill = value
        End Set
    End Property

    Private _ListWBAllocCoverSummary As List(Of WESMBillAllocCoverSummary)
    Public Property ListWBAllocCoverSummary() As List(Of WESMBillAllocCoverSummary)
        Get
            Return _ListWBAllocCoverSummary
        End Get
        Set(ByVal value As List(Of WESMBillAllocCoverSummary))
            _ListWBAllocCoverSummary = value
        End Set
    End Property

    Public Function GetListWESMTransSummaryPerNo(ByVal bpNo As Integer, ByVal stlRun As String,
                                                ByVal participant As String) As List(Of WESMBillAllocCoverSummary)
        Dim ret As New List(Of WESMBillAllocCoverSummary)
        ret = GetListWESMTransSummary(bpNo, stlRun, participant)

        Return ret
    End Function
    Public Function PrintWESMTransactionSummary(ByVal dtableCover As DataTable, ByVal dtableDtl As DataTable,
                                                ByVal bpNo As Integer, ByVal stlRun As String,
                                                ByVal participant As String, ByVal calendarBP As CalendarBillingPeriod,
                                                ByVal participantInfo As AMParticipants,
                                                ByVal wesmTransactionSummary As WESMBillAllocCoverSummary) As DataSet
        Dim ret As New DataSet

        With wesmTransactionSummary
            Dim rowMain = dtableCover.NewRow()

            rowMain("TRANSACTION_NO") = .TransactionNo
            rowMain("TRANSACTION_DATE") = .TransactionDate
            rowMain("BILLING_PERIOD_START") = CDate(calendarBP.StartDate.ToShortDateString)
            rowMain("BILLING_PERIOD_END") = CDate(calendarBP.EndDate.ToShortDateString)
            rowMain("DUE_DATE") = CDate(.DueDate.ToShortDateString)
            rowMain("STL_ID") = .StlID
            rowMain("FULL_NAME") = participantInfo.FullName
            rowMain("BUSINESS_STYLE") = participantInfo.BusinessStyle
            rowMain("ADDRESS") = If(participantInfo.BillingAddress.ToString.Length = 0, participantInfo.ParticipantAddress, participantInfo.BillingAddress)
            rowMain("BILLING_ID") = .BillingID
            rowMain("VATABLE_SALES") = .VatableSales
            rowMain("ZERO_RATED_SALES") = .ZeroRatedSales
            rowMain("ZERO_RATED_ECOZONES_SALES") = .ZeroRatedEcoZoneSales
            rowMain("NET_SALES") = .VatableSales + .ZeroRatedSales + .ZeroRatedEcoZoneSales
            rowMain("VATABLE_PURCHASES") = .VatablePurchases
            rowMain("ZERO_RATED_PURCHASES") = .ZeroRatedPurchases
            rowMain("ZERO_RATED_ECOZONES_PURCHASES") = .ZeroRatedEcoZonePurchases
            rowMain("NET_PURCHASES") = .VatablePurchases + .ZeroRatedPurchases + .ZeroRatedEcoZonePurchases
            rowMain("VAT_ON_SALES") = .VatOnSales
            rowMain("VAT_ON_PURCHASES") = .VatOnPurchases
            rowMain("EWT") = .EWTSales + .EWTPurchases
            rowMain("NSS_FLOWBACK") = .NSSFlowBack
            rowMain("GMR_SMR") = .GMR
            rowMain("SPOT_QTY") = .SpotQty
            rowMain("MARKET_FEES_RATE") = .MarketFeesRate
            rowMain("REMARKS") = .Remarks
            rowMain("GENX_AMOUNT") = .GenXAmount
            dtableCover.Rows.Add(rowMain)
            dtableCover.AcceptChanges()
            Dim counter As Integer = 0
            For Each dtl In .ListWBAllocDisDetails
                Dim rowDtl = dtableDtl.NewRow()
                counter += 1
                rowDtl("SELLER_BUYER_ID") = .StlID
                rowDtl("FULL_NAME") = participantInfo.FullName
                rowDtl("BILLING_ID") = dtl.BillingID
                rowDtl("STL_ID") = If(dtl.STLID.Length = 0, "", dtl.STLID)
                rowDtl("FACILITY_TYPE") = If(dtl.FacilityType.Contains("LOAD"), "LOAD", "GEN")
                rowDtl("WHT_TAG") = dtl.WHTTag
                rowDtl("ITH_TAG") = dtl.ITHTag
                rowDtl("NON_VATABLE_TAG") = dtl.NonVatableTag
                rowDtl("ZERO_RATED_TAG") = dtl.ZeroRatedTag
                rowDtl("NET_SELLER_BUYER_TAG") = dtl.NetSellerBuyerTag
                rowDtl("VATABLE_SALES") = dtl.VatableSales
                rowDtl("ZERO_RATED_SALES") = dtl.ZeroRatedSales
                rowDtl("ZERO_RATED_ECOZONES_SALES") = dtl.ZeroRatedEcoZoneSales
                rowDtl("VAT_ON_SALES") = dtl.VatOnSales
                rowDtl("VATABLE_PURCHASES") = dtl.VatablePurchases
                rowDtl("ZERO_RATED_PURCHASES") = dtl.ZeroRatedPurchases
                rowDtl("ZERO_RATED_ECOZONES_PURCHASES") = dtl.ZeroRatedEcoZonePurchases
                rowDtl("VAT_ON_PURCHASES") = dtl.VatOnPurchases
                rowDtl("EWT") = dtl.EWT
                rowDtl("BILLING_PERIOD") = CInt(calendarBP.BillingPeriod)
                rowDtl("BILLING_PERIOD_START") = CDate(calendarBP.StartDate.ToShortDateString)
                rowDtl("BILLING_PERIOD_END") = CDate(calendarBP.EndDate.ToShortDateString)
                rowDtl("STL_RUN") = stlRun
                rowDtl("DUE_DATE") = CDate(.DueDate.ToShortDateString)
                dtableDtl.Rows.Add(rowDtl)
                dtableDtl.AcceptChanges()
            Next
        End With
        dtableCover.TableName = "WESMBillTransCoverSummary"
        dtableDtl.TableName = "WESMBillTransAllocDetails"

        ret.Tables.Add(dtableCover)
        ret.Tables.Add(dtableDtl)
        ret.AcceptChanges()

        Return ret
    End Function

End Class
