﻿Imports System.Text
Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Imports System.ComponentModel
Imports Microsoft.Office.Interop
Imports System.Threading

Public Class WHTaxCertificateSTLHelper
    Public Sub New()
        'Get the current instance of the dal
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._DataAccess = DAL.GetInstance()
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

    Private _ViewListOfWHTCertSTL As List(Of WHTaxCertificateSTL)
    Public Property ViewListOfWHTCertSTL() As List(Of WHTaxCertificateSTL)
        Get
            Return _ViewListOfWHTCertSTL
        End Get
        Set(ByVal value As List(Of WHTaxCertificateSTL))
            _ViewListOfWHTCertSTL = value
        End Set
    End Property

    Private _NewWHTaxCertSTL As WHTaxCertificateSTL
    Public Property NewWHTaxCertSTL() As WHTaxCertificateSTL
        Get
            Return _NewWHTaxCertSTL
        End Get
        Set(ByVal value As WHTaxCertificateSTL)
            _NewWHTaxCertSTL = value
        End Set
    End Property

    Private _FetchListWHTCertDetails As List(Of WHTaxCertificateDetails)
    Public Property FetchListWHTCertDetails() As List(Of WHTaxCertificateDetails)
        Get
            Return _FetchListWHTCertDetails
        End Get
        Set(ByVal value As List(Of WHTaxCertificateDetails))
            _FetchListWHTCertDetails = value
        End Set
    End Property

    Private _ListOfParticipants As List(Of String)
    Public Property ListOfParticipants() As List(Of String)
        Get
            Return _ListOfParticipants
        End Get
        Set(ByVal value As List(Of String))
            _ListOfParticipants = value
        End Set
    End Property

#Region "Property of WESM Transaction Cover Summary"
    Private _WESMTransCoverSummaryList As New List(Of WESMBillAllocCoverSummary)
    Public ReadOnly Property WESMTransCoverSummaryList() As List(Of WESMBillAllocCoverSummary)
        Get
            Return _WESMTransCoverSummaryList
        End Get
    End Property
#End Region

#Region "Property of WESM Transaction Details Summary List"
    Private _WESMTransDetailsSummaryList As New List(Of WESMTransDetailsSummary)
    Public Property WESMTransDetailsSummaryList() As List(Of WESMTransDetailsSummary)
        Get
            Return _WESMTransDetailsSummaryList
        End Get
        Set(ByVal value As List(Of WESMTransDetailsSummary))
            _WESMTransDetailsSummaryList = value
        End Set
    End Property
#End Region

#Region "Property of WESM Transaction Details Summary History List"
    Private _WESMTransDetailsSummaryHistoryList As New List(Of WESMTransDetailsSummaryHistory)
    Public Property WESMTransDetailsSummaryHistoryList() As List(Of WESMTransDetailsSummaryHistory)
        Get
            Return _WESMTransDetailsSummaryHistoryList
        End Get
        Set(ByVal value As List(Of WESMTransDetailsSummaryHistory))
            _WESMTransDetailsSummaryHistoryList = value
        End Set
    End Property
#End Region

#Region "Property of ALLOCATION DATE"
    Private _AllocRemittDate As AllocationDate
    Public Property AllocRemittDate() As AllocationDate
        Get
            Return _AllocRemittDate
        End Get
        Set(ByVal value As AllocationDate)
            _AllocRemittDate = value
        End Set
    End Property
#End Region

#Region "Search By Date"
    Public Sub SearchByDateRangeForWTCertCollection(ByVal dFrom As Date, ByVal dTo As Date, ByVal notAllocated As Boolean, ByVal notUntagged As Boolean)
        Me._ViewListOfWHTCertSTL = GetListOfWTCertStl(dFrom, dTo, notAllocated, notUntagged)
    End Sub
    Private Function GetListOfWTCertStl(ByVal dateFrom As Date, ByVal dateTo As Date, ByVal notAllocated As Boolean, ByVal notUntagged As Boolean) As List(Of WHTaxCertificateSTL)
        Dim ret As New List(Of WHTaxCertificateSTL)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.*, B.PARTICIPANT_ID, B.FULL_NAME FROM AM_CERTIFICATE_WHTAX_STL A " & vbNewLine _
                               & "LEFT JOIN AM_PARTICIPANTS B ON B.ID_NUMBER = A.BILLING_IDNUMBER " & vbNewLine _
                              & "WHERE A.REMITTANCE_DATE >= TO_DATE('" & dateFrom.ToShortDateString & "','MM/DD/YYYY') " & vbNewLine _
                              & "AND A.REMITTANCE_DATE <= TO_DATE('" & dateTo & "','MM/DD/YYYY') " & vbNewLine _
                              & "AND A.ALLOCATED_TO_AP = " & If(notAllocated = False, 0, 1) & " AND UNTAG_EWT = " & If(notUntagged = False, 0, 1) & " " & vbNewLine _
                              & "ORDER BY A.REMITTANCE_DATE, A.CERTIFICATE_NO, A.BILLING_IDNUMBER"

            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetListOfWTCertStl(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function
    Private Function GetListOfWTCertStl(ByVal dr As IDataReader) As List(Of WHTaxCertificateSTL)
        Dim result As New List(Of WHTaxCertificateSTL)

        Try
            While dr.Read()
                Dim item As New WHTaxCertificateSTL

                With dr
                    item.CertificateNo = CLng(.Item("CERTIFICATE_NO"))
                    item.RemittanceDate = CDate(.Item("REMITTANCE_DATE"))
                    item.BillingIDNumber = New AMParticipants(CStr(.Item("BILLING_IDNUMBER")), CStr(.Item("PARTICIPANT_ID")), CStr(.Item("FULL_NAME")))
                    item.CollectedAmount = CDec(.Item("COLLECTED_AMOUNT"))
                    item.AllocatedToAP = If(CInt(.Item("ALLOCATED_TO_AP")) = 0, "NO", "YES")
                    item.UntagEWT = If(CInt(.Item("UNTAG_EWT")) = 0, "NO", "YES")
                    result.Add(item)
                End With
            End While
            result.TrimExcess()
            If result.Count = 0 Then
                'Throw New Exception("No available record.")
            End If

        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return result
    End Function

    Private Function GetListOfWHTAXCertDetails(ByVal certNumber As Long) As List(Of WHTaxCertificateDetails)
        Dim ret As New List(Of WHTaxCertificateDetails)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.*, B.INV_DM_CM, C.PARTICIPANT_ID, C.FULL_NAME, B.BILLING_PERIOD, D.STL_RUN FROM AM_CERTIFICATE_WHTAX_DETAILS A " & vbNewLine _
                               & "LEFT JOIN AM_WESM_BILL_SUMMARY B ON B.WESMBILL_SUMMARY_NO = A.WESMBILL_SUMMARY_NO " & vbNewLine _
                               & "LEFT JOIN AM_PARTICIPANTS C ON C.ID_NUMBER = B.ID_NUMBER " & vbNewLine _
                               & "LEFT JOIN (SELECT * FROM AM_WESM_BILL WHERE CHARGE_TYPE = 'E') D ON D.INVOICE_NO = B.INV_DM_CM " & vbNewLine _
                              & "WHERE A.CERTIFICATE_NO = " & certNumber
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetListOfWHTAXCertDetails(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetListOfWHTAXCertDetails(ByVal dr As IDataReader) As List(Of WHTaxCertificateDetails)
        Dim result As New List(Of WHTaxCertificateDetails)

        Try
            While dr.Read()
                Dim item As New WHTaxCertificateDetails

                With dr
                    item.WESMBillSummary = GetWESMBillSummaryItem(.Item("WESMBILL_SUMMARY_NO"))
                    item.NewDueDate = CDate(.Item("NEW_DUE_DATE").ToString())
                    item.EndingBalance = CDec(.Item("ENDING_BALANCE"))
                    item.AmountTagged = CDec(.Item("AMOUNT_TAGGED"))
                    item.NewEndingBalance = CDec(.Item("NEW_ENDING_BALANCE"))
                    item.WithholdingTaxAmount = CDec(.Item("WHTAX_AMOUNT"))
                    result.Add(item)
                End With
            End While
            result.TrimExcess()
            If result.Count = 0 Then
                'Throw New Exception("No available record.")
            End If

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

#Region "View Certificate"
    Public Function GetWTCertStl(ByVal certifNo As Long) As WHTaxCertificateSTL
        Dim ret As New WHTaxCertificateSTL
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.*, B.PARTICIPANT_ID, B.FULL_NAME FROM AM_CERTIFICATE_WHTAX_STL A " & vbNewLine _
                               & "LEFT JOIN AM_PARTICIPANTS B ON B.ID_NUMBER = A.BILLING_IDNUMBER " & vbNewLine _
                              & "WHERE A.CERTIFICATE_NO  = " & certifNo

            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetWTCertStl(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetWTCertStl(ByVal dr As IDataReader) As WHTaxCertificateSTL
        Dim result As New WHTaxCertificateSTL

        Try
            While dr.Read()
                Dim item As New WHTaxCertificateSTL

                With dr
                    item.CertificateNo = CLng(.Item("CERTIFICATE_NO"))
                    item.RemittanceDate = CDate(.Item("REMITTANCE_DATE"))
                    item.BillingIDNumber = New AMParticipants(CStr(.Item("BILLING_IDNUMBER")), CStr(.Item("PARTICIPANT_ID")), CStr(.Item("FULL_NAME")))
                    item.CollectedAmount = CDec(.Item("COLLECTED_AMOUNT"))
                    Dim getDetails As List(Of WHTaxCertificateDetails) = GetListOfWHTAXCertDetails(CLng(.Item("CERTIFICATE_NO")))
                    item.TagDetails = (From x In getDetails Where x.AmountTagged > 0 Select x).ToList
                    item.AllocationDetails = (From x In getDetails Where x.AmountTagged < 0 Select x).ToList
                    result = item
                End With
            End While
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

#Region "Get Remittance Date"
    Public Function InitializeListOfRemittanceDate() As List(Of AllocationDate)
        Dim ret As New List(Of AllocationDate)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT * FROM AM_PAYMENT_NEW A ORDER BY REMITTANCE_DATE DESC"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.InitializeListOfRemittanceDate(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Public Function GetAllocRemittanceDate(ByVal remittanceDate As Date) As List(Of AllocationDate)
        Dim ret As New List(Of AllocationDate)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT * FROM AM_PAYMENT_NEW A WHERE A.REMITTANCE_DATE = TO_DATE('" & remittanceDate.ToShortDateString & "','MM/DD/YYYY')"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.InitializeListOfRemittanceDate(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function InitializeListOfRemittanceDate(ByVal dr As IDataReader) As List(Of AllocationDate)
        Dim result As New List(Of AllocationDate)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As New AllocationDate
                    item.PaymentNo = CLng(.Item("PAYMENT_NO"))
                    item.CollAllocationDate = CDate(.Item("ALLOCATION_DATE"))
                    item.RemittanceDate = CDate(.Item("REMITTANCE_DATE"))
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

#Region "Get WESM BILLS with Outstanding Balance for Withholding Tax"
    Public Sub GetWESMBillSummaryWithWTAX(ByVal idNumber As String, ByVal remittance As Date)
        Me._FetchListWHTCertDetails = GetWESMBillSummaryForTaggingEWT(idNumber, remittance)
    End Sub

    Public Function GetListWESMTransDetailsSummaryBuyer(ByVal transNo As String) As List(Of WESMTransDetailsSummary)
        Dim ret As New List(Of WESMTransDetailsSummary)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.* FROM AM_WESM_TRANS_DETAILS_SUMMARY A " & vbNewLine _
                              & "WHERE A.BUYER_TRANS_NO = '" & transNo & "' AND A.ORIG_AMOUNT_EWT <= 0"

            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetListWESMTransDetailsSummary(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Public Function GetListWESMTransDetailsSummarySeller(ByVal transNo As String) As List(Of WESMTransDetailsSummary)
        Dim ret As New List(Of WESMTransDetailsSummary)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.* FROM AM_WESM_TRANS_DETAILS_SUMMARY A " & vbNewLine _
                              & "WHERE A.SELLER_TRANS_NO = '" & transNo & "'"

            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
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

    Public Function GetListOfParticipantsForAdvanceEWT() As List(Of String)
        Dim ret As New List(Of String)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT DISTINCT A.ID_NUMBER FROM AM_WESM_BILL_SUMMARY A " & vbNewLine _
                               & "INNER JOIN AM_WESM_ALLOC_COVER_SUMMARY C ON C.TRANSACTION_NUMBER = A.INV_DM_CM " & vbNewLine _
                               & "WHERE A.ENDING_BALANCE < 0 ORDER BY A.ID_NUMBER"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetListOfParticipants(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Public Function GetListOfParticipants(ByVal remittanceDate As Date) As List(Of String)
        Dim ret As New List(Of String)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT DISTINCT A.ID_NUMBER FROM AM_COLLECTION_ALLOCATION A " & vbNewLine _
                               & "LEFT JOIN AM_PAYMENT_NEW B ON B.ALLOCATION_DATE = A.ALLOCATION_DATE " & vbNewLine _
                               & "INNER JOIN AM_WESM_ALLOC_COVER_SUMMARY C ON C.TRANSACTION_NUMBER = A.AM_REF_NO " & vbNewLine _
                               & "WHERE A.COLLECTION_TYPE IN (8,9) AND B.REMITTANCE_DATE = TO_DATE('" & remittanceDate & "','mm/dd/yyyy') ORDER BY A.ID_NUMBER"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetListOfParticipants(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetListOfParticipants(ByVal dr As IDataReader) As List(Of String)
        Dim result As New List(Of String)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    result.Add(CStr(.Item("ID_NUMBER")))
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
    Private Function GetWESMBillSummaryForAllocEWT(ByVal wesmBillBatchNo As Long) As List(Of WHTaxCertificateDetails)
        Dim ret As New List(Of WHTaxCertificateDetails)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.*, B.PARTICIPANT_ID, B.FULL_NAME, B.PARTICIPANT_ADDRESS, A.ENERGY_WITHHOLD " & vbNewLine _
                               & "FROM AM_WESM_BILL_SUMMARY A " & vbNewLine _
                               & "LEFT JOIN AM_PARTICIPANTS B ON B.ID_NUMBER = A.ID_NUMBER " & vbNewLine _
                               & "WHERE A.ENDING_BALANCE > 0 And A.CHARGE_TYPE = 'E' AND A.WESMBILL_BATCH_NO = " & wesmBillBatchNo

            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetWESMBillSummaryForAllocEWT(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetWESMBillSummaryForAllocEWT(ByVal dr As IDataReader) As List(Of WHTaxCertificateDetails)
        Dim result As New List(Of WHTaxCertificateDetails)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As New WESMBillSummary
                    item.IDNumber = New AMParticipants(CStr(.Item("ID_NUMBER").ToString()), CStr(.Item("PARTICIPANT_ID").ToString()),
                                                       CStr(.Item("PARTICIPANT_ADDRESS").ToString()))
                    item.BillPeriod = CInt(.Item("BILLING_PERIOD").ToString())
                    item.ChargeType = CType(System.Enum.Parse(GetType(EnumChargeType), CStr(.Item("CHARGE_TYPE").ToString())), EnumChargeType)
                    item.DueDate = CDate(.Item("DUE_DATE").ToString())
                    item.BeginningBalance = Math.Round(CDec(.Item("BEGINNING_BALANCE").ToString()), 2)
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

                    Dim witem As New WHTaxCertificateDetails
                    witem.WESMBillSummary = item
                    witem.NewDueDate = CDate(.Item("NEW_DUEDATE").ToString())
                    witem.EndingBalance = item.EndingBalance
                    witem.WithholdingTaxAmount = CDec(.Item("ENERGY_WITHHOLD") * -1)

                    result.Add(witem)
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

    Private Function GetWESMBillSummaryForAdvanceTaggingEWT(ByVal idNumber As String, ByVal remittance As Date) As List(Of WHTaxCertificateDetails)
        Dim ret As New List(Of WHTaxCertificateDetails)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT DISTINCT A.*, B.PARTICIPANT_ID, B.FULL_NAME, B.PARTICIPANT_ADDRESS, A.ENERGY_WITHHOLD, TO_DATE('" & remittance.ToShortDateString & "','MM/DD/yyyy') AS ALLOCATION_DATE " & vbNewLine _
                               & "FROM AM_WESM_BILL_SUMMARY A " & vbNewLine _
                               & "INNER JOIN AM_PARTICIPANTS B ON B.ID_NUMBER = A.ID_NUMBER " & vbNewLine _
                               & "WHERE A.ENDING_BALANCE < 0 AND A.CHARGE_TYPE = 'E' AND A.ID_NUMBER = '" & idNumber & "' AND INV_DM_CM LIKE 'TS-W%' AND NOT INV_DM_CM LIKE '%ADJ%'"

            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetWESMBillsSummaryForTaggingEWT(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetWESMBillSummaryForTaggingEWT(ByVal idNumber As String, ByVal remittanceDate As Date) As List(Of WHTaxCertificateDetails)
        Dim ret As New List(Of WHTaxCertificateDetails)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT DISTINCT A.*, B.PARTICIPANT_ID, B.FULL_NAME, B.PARTICIPANT_ADDRESS, D.ALLOCATION_DATE, E.REMITTANCE_DATE " & vbNewLine _
                               & "FROM AM_WESM_BILL_SUMMARY A " & vbNewLine _
                               & "INNER JOIN AM_PARTICIPANTS B ON B.ID_NUMBER = A.ID_NUMBER " & vbNewLine _
                               & "INNER JOIN AM_COLLECTION_ALLOCATION D ON D.AM_REF_NO = A.INV_DM_CM AND D.WESMBILL_SUMMARY_NO = A.WESMBILL_SUMMARY_NO " & vbNewLine _
                               & "INNER JOIN AM_PAYMENT_NEW E ON E.ALLOCATION_DATE = D.ALLOCATION_DATE " & vbNewLine _
                               & "WHERE A.ENDING_BALANCE < 0 AND A.CHARGE_TYPE = 'E' AND A.ID_NUMBER = '" & idNumber & "' " & vbNewLine _
                               & "AND E.REMITTANCE_DATE = TO_DATE('" & remittanceDate & "', 'mm/dd/yyyy') AND INV_DM_CM LIKE 'TS-W%' AND NOT INV_DM_CM LIKE '%ADJ%'"

            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetWESMBillsSummaryForTaggingEWT(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetWESMBillsSummaryForTaggingEWT(ByVal dr As IDataReader) As List(Of WHTaxCertificateDetails)
        Dim result As New List(Of WHTaxCertificateDetails)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr

                    Dim item As New WESMBillSummary
                    item.IDNumber = New AMParticipants(CStr(.Item("ID_NUMBER").ToString()), CStr(.Item("PARTICIPANT_ID").ToString()),
                                                       CStr(.Item("PARTICIPANT_ADDRESS").ToString()))
                    item.BillPeriod = CInt(.Item("BILLING_PERIOD").ToString())
                    item.ChargeType = CType(System.Enum.Parse(GetType(EnumChargeType), CStr(.Item("CHARGE_TYPE").ToString())), EnumChargeType)
                    item.DueDate = CDate(.Item("DUE_DATE").ToString())
                    item.BeginningBalance = Math.Round(CDec(.Item("BEGINNING_BALANCE").ToString()), 2)
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

                    Dim witem As New WHTaxCertificateDetails
                    witem.WESMBillSummary = item
                    witem.NewDueDate = CDate(.Item("NEW_DUEDATE").ToString())
                    witem.EndingBalance = CDec(.Item("ENERGY_WITHHOLD"))
                    witem.WithholdingTaxAmount = CDec(.Item("ENERGY_WITHHOLD") * -1)

                    result.Add(witem)
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

    Public Function GetWESMBillSummaryItem(ByVal WESMBillNo As Long) As WESMBillSummary
        Dim result As New WESMBillSummary
        Dim report As New DataReport

        Try
            Dim SQL As String

            SQL = "SELECT A.ID_NUMBER, A.ID_TYPE, A.GROUP_NO, B.PARTICIPANT_ID, A.billing_period, A.TRANSACTION_DATE, A.ENERGY_WITHHOLD, " & vbNewLine &
                                  "b.participant_address, b.city, b.province, b.zip_code, " & vbNewLine &
                                  "A.charge_type, A.due_date, A.ENDING_BALANCE, a.BEGINNING_BALANCE, a.NEW_DUEDATE, a.IS_MFWTAX_DEDUCTED, " & vbNewLine &
                                  "A.INV_DM_CM, A.SUMMARY_TYPE, a.WESMBILL_SUMMARY_NO, A.ADJUSTMENT, A.WESMBILL_BATCH_NO, A.ENERGY_WITHHOLD_STATUS, a.NO_OFFSET, A.NO_SOA, A.NO_DEFINT, c.remarks " & vbNewLine &
                      "FROM AM_WESM_BILL_SUMMARY A " & vbNewLine &
                      "JOIN  AM_PARTICIPANTS B on a.id_number = b.id_number " & vbNewLine &
                      "JOIN  AM_WESM_BILL C on c.invoice_no = a.inv_dm_cm and c.charge_type = a.charge_type " & vbNewLine &
                      "WHERE A.WESMBILL_SUMMARY_NO = " & WESMBillNo

            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.GetWESMBillSummaryPerItem(report.ReturnedIDatareader)


        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function

    Private Function GetWESMBillSummaryPerItem(ByVal dr As IDataReader) As WESMBillSummary
        Dim result As New WESMBillSummary
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As New WESMBillSummary
                    'item.GroupNo = CInt(.Item("GROUP_NO").ToString)
                    item.IDNumber = New AMParticipants(CStr(.Item("ID_NUMBER").ToString()), CStr(.Item("PARTICIPANT_ID").ToString()),
                                                       CStr(.Item("PARTICIPANT_ADDRESS").ToString()), CStr(.Item("CITY").ToString()), CStr(.Item("PROVINCE").ToString()),
                                                       CStr(.Item("ZIP_CODE").ToString()))
                    item.BillPeriod = CInt(.Item("BILLING_PERIOD").ToString())
                    item.ChargeType = CType(System.Enum.Parse(GetType(EnumChargeType), CStr(.Item("CHARGE_TYPE").ToString())), EnumChargeType)
                    item.DueDate = CDate(.Item("DUE_DATE").ToString())
                    item.BeginningBalance = Math.Round(CDec(.Item("BEGINNING_BALANCE").ToString()), 2)
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
                    result = item
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

#Region "Get WESM Transaction Details Summary History"
    Private Function GetListWESMTransDetailsSummaryHistory(ByVal buyerTransNo As String, ByVal remittanceDate As Date) As List(Of WESMTransDetailsSummaryHistory)
        Dim ret As New List(Of WESMTransDetailsSummaryHistory)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.* FROM AM_WESM_TRANS_DETAILS_SUMMARY_HISTORY A " & vbNewLine _
                              & "WHERE A.BUYER_TRANS_NO = '" & buyerTransNo & "' AND REMITTANCE_DATE = TO_DATE('" & remittanceDate.ToShortDateString & "','MM/DD/YYYY')"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
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

#Region "Create NewWHTaxCerticationCollection"
    Public Sub GenerateNewWHTaxCertTagAlloc(ByVal remittanceDate As Date, ByVal participantId As String, ByVal listOfWHTaxCertCollectionTag As List(Of WHTaxCertificateDetails))
        Dim newWHTaxCertColl As New WHTaxCertificateSTL
        Dim participantInfo As AMParticipants = WBillHelper.GetAMParticipants(participantId).FirstOrDefault

        With newWHTaxCertColl
            .CertificateNo = 0
            .BillingIDNumber = participantInfo
            .RemittanceDate = remittanceDate
            .CollectedAmount = (From x In listOfWHTaxCertCollectionTag Select x.AmountTagged).Sum()
            .TagDetails = listOfWHTaxCertCollectionTag
            .AllocationDetails = New List(Of WHTaxCertificateDetails)
        End With
        Me._NewWHTaxCertSTL = newWHTaxCertColl
    End Sub

    Private Function AllocateTaggedWHTaxCert(ByRef listOfWHTaxCertSTL As WHTaxCertificateSTL, ByVal remittanceDate As Date) As List(Of WHTaxCertificateDetails)
        Dim ret As New List(Of WHTaxCertificateDetails)
        Dim getListWESMBillBatchNo As List(Of Long) = (From x In listOfWHTaxCertSTL.TagDetails Select x.WESMBillSummary.WESMBillBatchNo Distinct).ToList
        For Each item In getListWESMBillBatchNo
            Dim getListOfWHTCertCollTag As List(Of WHTaxCertificateDetails) = (From x In listOfWHTaxCertSTL.TagDetails Where x.WESMBillSummary.WESMBillBatchNo = item Select x).ToList
            Dim getListOfWHTCertAlloc As List(Of WHTaxCertificateDetails) = Me.GetWESMBillSummaryForAllocEWT(item)
            If getListOfWHTCertAlloc.Count = 0 Then
                Throw New Exception("No AP found for WESMBillBatchNo: " & item)
            End If
            For Each i In AllocateTaggedWHTaxCertPerBatchNew(remittanceDate, listOfWHTaxCertSTL, getListOfWHTCertCollTag, getListOfWHTCertAlloc)
                ret.Add(i)
            Next
        Next
        Return ret
    End Function

    Private Function AllocateTaggedWHTaxCertPerBatch(ByVal listOfWHTCertCollTag As List(Of WHTaxCertificateDetails), ByRef listOfWHTCertAlloc As List(Of WHTaxCertificateDetails)) As List(Of WHTaxCertificateDetails)
        Dim ret As New List(Of WHTaxCertificateDetails)
        Dim getTotalCollectedAmount As Decimal = (From x In listOfWHTCertCollTag Select x.AmountTagged).Sum()
        Dim getTotalSalesWHTax As Decimal = (From x In listOfWHTCertAlloc Select x.WithholdingTaxAmount).Sum()

        For Each item In listOfWHTCertAlloc
            Dim shareAmount As Decimal = Math.Round(getTotalCollectedAmount * (item.WithholdingTaxAmount / getTotalSalesWHTax), 2, MidpointRounding.AwayFromZero)
            item.AmountTagged = shareAmount * -1
        Next

        Dim getTotalAllocatedAmount As Decimal = (From x In listOfWHTCertAlloc Select x.AmountTagged).Sum

        If getTotalCollectedAmount <> Math.Abs(getTotalAllocatedAmount) Then
            Dim getDiff As Decimal = Math.Round(getTotalCollectedAmount - Math.Abs(getTotalAllocatedAmount), 2)
            Dim getMaxWHTaxAmount As WHTaxCertificateDetails = (From x In listOfWHTCertAlloc Select x Order By x.AmountTagged Ascending).First
            getMaxWHTaxAmount.AmountTagged -= getDiff
        End If

        ret = listOfWHTCertAlloc
        Return ret
    End Function

    Private Function AllocateTaggedWHTaxCertPerBatchNew(ByVal remittanceDate As Date, ByRef listOfWHTaxCertSTL As WHTaxCertificateSTL, ByVal listOfWHTCertCollTag As List(Of WHTaxCertificateDetails), ByRef listOfWHTCertAlloc As List(Of WHTaxCertificateDetails)) As List(Of WHTaxCertificateDetails)
        Dim ret As New List(Of WHTaxCertificateDetails)
        Dim listWTDSummaryHistory As New List(Of WESMTransDetailsSummaryHistory)
        Dim AllocRemittanceDate As AllocationDate = GetAllocRemittanceDate(remittanceDate).FirstOrDefault

        If AllocRemittanceDate Is Nothing Then
            Throw New Exception("Please check the remittance date! Remittance date does not exist.")
        End If

        For Each itemAR In listOfWHTCertCollTag

            Dim getWTDSummary As List(Of WESMTransDetailsSummary) = GetListWESMTransDetailsSummaryBuyer(itemAR.WESMBillSummary.INVDMCMNo).ToList
            Dim getWTDSummaryHistory As List(Of WESMTransDetailsSummaryHistory) = GetListWESMTransDetailsSummaryHistory(itemAR.WESMBillSummary.INVDMCMNo, remittanceDate).ToList
            Dim getWTCSummaryAR As WESMBillAllocCoverSummary = Me.WBillHelper.GetListWESMTransCoverSummaryPerTransNo(itemAR.WESMBillSummary.INVDMCMNo).FirstOrDefault

            Dim iniWTDSummaryHistory As New List(Of WESMTransDetailsSummaryHistory)
            For Each itemAP In listOfWHTCertAlloc
                Dim shareEWTAmount As Decimal = 0D
                Dim getWTDSummaryAP As WESMTransDetailsSummary = getWTDSummary.Where(Function(x) x.SellerTransNo = itemAP.WESMBillSummary.INVDMCMNo).FirstOrDefault
                If Not getWTDSummaryAP Is Nothing Then
                    shareEWTAmount = ComputeAllocation(Math.Abs(getWTDSummaryAP.OutstandingBalanceInEWT), Math.Abs(getWTDSummary.Select(Function(x) x.OutstandingBalanceInEWT).Sum()), itemAR.AmountTagged)
                    Using itemWTDSummaryHistory As New WESMTransDetailsSummaryHistory
                        With itemWTDSummaryHistory
                            .BuyerTransNo = getWTDSummaryAP.BuyerTransNo
                            .BuyerBillingID = getWTDSummaryAP.BuyerBillingID
                            .SellerTransNo = getWTDSummaryAP.SellerTransNo
                            .SellerBillingID = getWTDSummaryAP.SellerBillingID
                            .DueDate = getWTDSummaryAP.DueDate
                            .AllocationDate = AllocRemittanceDate.CollAllocationDate
                            .RemittanceDate = remittanceDate
                            .AllocatedInEWT = shareEWTAmount
                            .Status = EnumWESMTransDetailsSummaryStatus.ADDED.ToString
                        End With
                        iniWTDSummaryHistory.Add(itemWTDSummaryHistory)
                    End Using
                Else
                    If getWTCSummaryAR Is Nothing Then
                        Throw New Exception("No available WESM Transaction Details Summary for SellerTransNo:" & itemAP.WESMBillSummary.INVDMCMNo)
                    End If

                    Dim getWTCSummaryAP As WESMBillAllocCoverSummary = Me.WBillHelper.GetListWESMTransCoverSummaryPerTransNo(itemAP.WESMBillSummary.INVDMCMNo).FirstOrDefault
                    Dim getEWTAmountAP As Decimal = getWTCSummaryAR.ListWBAllocDisDetails.Where(Function(x) x.BillingID = getWTCSummaryAP.BillingID).Select(Function(x) x.EWT).FirstOrDefault
                    Dim totalEWTofAR As Decimal = getWTCSummaryAR.EWTPurchases
                    Dim getEWTAmountAPTotal As Decimal = getWTCSummaryAR.ListWBAllocDisDetails.Select(Function(x) x.EWT).Sum
                    shareEWTAmount = ComputeAllocation(Math.Abs(getEWTAmountAP), Math.Abs(totalEWTofAR), itemAR.AmountTagged)

                    Using itemWTDSummaryHistory As New WESMTransDetailsSummaryHistory
                        With itemWTDSummaryHistory
                            .BuyerTransNo = getWTCSummaryAR.TransactionNo
                            .BuyerBillingID = getWTCSummaryAR.BillingID
                            .SellerTransNo = getWTCSummaryAP.TransactionNo
                            .SellerBillingID = getWTCSummaryAP.BillingID
                            .DueDate = getWTCSummaryAP.DueDate
                            .AllocationDate = AllocRemittanceDate.CollAllocationDate
                            .RemittanceDate = remittanceDate
                            .AllocatedInEWT = shareEWTAmount
                            .Status = EnumWESMTransDetailsSummaryStatus.ADDED.ToString
                        End With
                        iniWTDSummaryHistory.Add(itemWTDSummaryHistory)
                    End Using
                End If
            Next
            Dim totalAllocatedEWTAP As Decimal = Math.Abs(iniWTDSummaryHistory.Select(Function(x) x.AllocatedInEWT).Sum())
            If itemAR.AmountTagged <> totalAllocatedEWTAP Then
                Dim getDiff As Decimal = itemAR.AmountTagged - totalAllocatedEWTAP
                Me.AdjustComputedAllocation(getWTDSummary, iniWTDSummaryHistory, getDiff)
            End If

            Dim checktotalAllocatedEWTAP As Decimal = Math.Abs(iniWTDSummaryHistory.Select(Function(x) x.AllocatedInEWT).Sum())

            For Each itemAP In listOfWHTCertAlloc
                Dim getiniWTDSummaryHistory = iniWTDSummaryHistory.
                                              Where(Function(x) x.BuyerTransNo = itemAR.WESMBillSummary.INVDMCMNo And x.SellerTransNo = itemAP.WESMBillSummary.INVDMCMNo).
                                              FirstOrDefault
                Dim getIniWTDSummary = getWTDSummary.Where(Function(x) x.BuyerTransNo = itemAR.WESMBillSummary.INVDMCMNo And x.SellerTransNo = itemAP.WESMBillSummary.INVDMCMNo).
                                       FirstOrDefault

                If Not getIniWTDSummary Is Nothing Then
                    If getiniWTDSummaryHistory.AllocatedInEWT <> 0 Then
                        With getIniWTDSummary
                            .OutstandingBalanceInEWT += getiniWTDSummaryHistory.AllocatedInEWT
                            .Status = EnumWESMTransDetailsSummaryStatus.UPDATED.ToString
                        End With
                        Me._WESMTransDetailsSummaryList.Add(getIniWTDSummary)
                    End If
                End If

                Dim getExistWTDSummaryHistory = getWTDSummaryHistory.
                                                Where(Function(x) x.BuyerTransNo = getiniWTDSummaryHistory.BuyerTransNo And x.SellerTransNo = getiniWTDSummaryHistory.SellerTransNo).
                                                FirstOrDefault
                If Not getExistWTDSummaryHistory Is Nothing Then
                    If getiniWTDSummaryHistory.AllocatedInEWT <> 0 Then
                        With getExistWTDSummaryHistory
                            .AllocatedInEWT = getiniWTDSummaryHistory.AllocatedInEWT
                            .Status = EnumWESMTransDetailsSummaryStatus.UPDATED.ToString
                        End With
                        Me._WESMTransDetailsSummaryHistoryList.Add(getExistWTDSummaryHistory)
                    End If
                Else
                    If getiniWTDSummaryHistory.AllocatedInEWT <> 0 Then
                        Me._WESMTransDetailsSummaryHistoryList.Add(getiniWTDSummaryHistory)
                    End If
                End If
                itemAP.AmountTagged += (getiniWTDSummaryHistory.AllocatedInEWT * -1)
            Next
        Next

        For Each item In listOfWHTCertAlloc
            listOfWHTaxCertSTL.AllocationDetails.Add(item)
        Next

        listOfWHTaxCertSTL.AllocationDetails.TrimExcess()

        ret = listOfWHTCertAlloc
        Return ret
    End Function

    Private Function ComputeAllocation(ByVal InvOutstandingBalance As Decimal, ByVal CurrentBP_TotalBalance As Decimal, ByVal TotalPaymentForBP As Decimal) As Decimal
        Dim returnAmntAlloc As Decimal
        If InvOutstandingBalance <> 0 Then
            returnAmntAlloc = (InvOutstandingBalance / CurrentBP_TotalBalance) * TotalPaymentForBP
        Else
            returnAmntAlloc = 0
        End If
        returnAmntAlloc = Math.Round(returnAmntAlloc, 2, MidpointRounding.AwayFromZero)
        Return returnAmntAlloc
    End Function

    Private Sub AdjustComputedAllocation(ByRef listOfWTDSummary As List(Of WESMTransDetailsSummary), ByRef listWTDSummaryHistory As List(Of WESMTransDetailsSummaryHistory),
                                         ByVal AmountDiff As Decimal)
        Dim UpdatedlistWTDSummaryHistory As New List(Of WESMTransDetailsSummaryHistory)
        Dim totalAllocDiffAmount As Decimal = 0D
        Do While totalAllocDiffAmount <> AmountDiff
            If listOfWTDSummary.Count <> 0 Then
                Dim GetListOfInvoiceWithEOAmount = (From x In listOfWTDSummary Select x Where x.OutstandingBalanceInEWT < 0 Order By x.OutstandingBalanceInEWT Ascending).ToList
                If GetListOfInvoiceWithEOAmount.Count = 0 And AmountDiff <> 0 Then
                    GetListOfInvoiceWithEOAmount = (From x In listOfWTDSummary Select x Where x.OrigBalanceInEWT < 0 Order By x.OutstandingBalanceInEWT Ascending).ToList
                End If
                For Each item In GetListOfInvoiceWithEOAmount
                    Dim computedShareDiffAmount As Decimal = If(AmountDiff > 0D, 0.01D, -0.01D)
                    If totalAllocDiffAmount <> AmountDiff Then
                        Dim UpdateWTDSummaryHistory As WESMTransDetailsSummaryHistory = (From x In listWTDSummaryHistory
                                                                                         Where x.BuyerTransNo = item.BuyerTransNo And x.SellerTransNo = item.SellerTransNo
                                                                                         Select x).FirstOrDefault
                        Dim AmountChecker As Decimal = item.OutstandingBalanceInEWT + (UpdateWTDSummaryHistory.AllocatedInEWT + computedShareDiffAmount)
                        If Not AmountChecker > 0 Then
                            With UpdateWTDSummaryHistory
                                .AllocatedInEWT += computedShareDiffAmount
                            End With
                            totalAllocDiffAmount += computedShareDiffAmount
                        End If
                    Else
                        Exit For
                    End If
                Next
            Else
                For Each item In listWTDSummaryHistory.OrderByDescending(Function(x) x.AllocatedInEWT).ToList
                    Dim computedShareDiffAmount As Decimal = If(AmountDiff > 0D, 0.01D, -0.01D)
                    If totalAllocDiffAmount <> AmountDiff Then
                        item.AllocatedInEWT += computedShareDiffAmount
                    Else
                        Exit For
                    End If
                    totalAllocDiffAmount += computedShareDiffAmount
                Next
            End If
        Loop
    End Sub
#End Region

#Region "Allocated To AP"
    Public Sub SaveSTLNoticeNew(ByVal getRemittanceDate As Date, ByVal progress As IProgress(Of ProgressClass), ByVal ct As CancellationToken)
        Dim listSQL As New List(Of String)
        Dim SQL As String = ""

        Dim getCollectionDate As Date = (From x In Me.InitializeListOfRemittanceDate() Where x.RemittanceDate = getRemittanceDate Select x.CollAllocationDate).FirstOrDefault

        'Delete AM_STL_NOTICE_NEW
        SQL = "DELETE FROM AM_STL_NOTICE_NEW WHERE STL_NOTICE_DATE = LAST_DAY(TO_DATE('" & FormatDateTime(getRemittanceDate, DateFormat.ShortDate) & "','mm/dd/yyyy'))"
        listSQL.Add(SQL)

        'Insert into AM_STL_NOTICE_NEW
        SQL = "INSERT INTO AM_STL_NOTICE_NEW(STL_NOTICE_DATE, ENDING_BALANCE, WESMBILL_SUMMARY_NO) " &
                  "(SELECT LAST_DAY(TO_DATE('" & FormatDateTime(getRemittanceDate, DateFormat.ShortDate) & "','mm/dd/yyyy')) AS TRANSACTIONDATE, ENDING_BALANCE, WESMBILL_SUMMARY_NO " &
                  " FROM AM_WESM_BILL_SUMMARY WHERE ENDING_BALANCE <> 0 OR TO_CHAR(NEW_DUEDATE,'MM/YYYY') = TO_CHAR(TO_DATE('" & FormatDateTime(getCollectionDate, DateFormat.ShortDate) & "','MM/DD/YYYY'),'MM/YYYY'))"

        Dim newProgress As ProgressClass = New ProgressClass
        newProgress.ProgressIndicator = 0
        newProgress.ProgressMsg = "Updating data for Settlement Notice Report as of " & getRemittanceDate.ToShortDateString
        progress.Report(newProgress)

        listSQL.Add(SQL)

        If ct.IsCancellationRequested Then
            Throw New OperationCanceledException
        End If

        Me.SaveToDB(listSQL, progress, ct)

    End Sub
    Public Sub SaveAllocatedToAp(ByVal certificateNo As Long, ByVal progress As IProgress(Of ProgressClass), ByVal ct As CancellationToken)
        Dim listSQL As New List(Of String)
        Dim SysDateTime As Date = WBillHelper.GetSystemDateTime()
        Dim SQL As String = ""

        Dim newProgress As ProgressClass = New ProgressClass
        newProgress.ProgressIndicator = 0
        newProgress.ProgressMsg = "Fetching the EWT AR Tagged for CertificateNo: " & certificateNo.ToString("N0")
        progress.Report(newProgress)

        Dim getSelectedCertificate As WHTaxCertificateSTL = Me.GetWTCertStl(certificateNo)

        Me._WESMTransCoverSummaryList = New List(Of WESMBillAllocCoverSummary)
        Me._WESMTransDetailsSummaryList = New List(Of WESMTransDetailsSummary)
        Me._WESMTransDetailsSummaryHistoryList = New List(Of WESMTransDetailsSummaryHistory)

        newProgress = New ProgressClass
        newProgress.ProgressIndicator = 0
        newProgress.ProgressMsg = "Allocating to EWT AP for CertificateNo: " & certificateNo.ToString("N0")
        progress.Report(newProgress)

        Me.AllocateTaggedWHTaxCert(getSelectedCertificate, getSelectedCertificate.RemittanceDate)

        SQL = "UPDATE AM_CERTIFICATE_WHTAX_STL SET ALLOCATED_TO_AP = 1 WHERE CERTIFICATE_NO = " & certificateNo & " AND ALLOCATED_TO_AP = 0"
        listSQL.Add(SQL)

        For Each item In getSelectedCertificate.AllocationDetails
            SQL = "INSERT INTO AM_CERTIFICATE_WHTAX_DETAILS (CERTIFICATE_NO,WESMBILL_SUMMARY_NO,ENDING_BALANCE,AMOUNT_TAGGED,NEW_ENDING_BALANCE,WHTAX_AMOUNT,NEW_DUE_DATE)" & vbNewLine _
                    & "SELECT " & getSelectedCertificate.CertificateNo & ", " & item.WESMBillSummary.WESMBillSummaryNo & ", " & item.EndingBalance & ", " & item.AmountTagged & ", " & item.NewEndingBalance & ", " & item.WithholdingTaxAmount & ", TO_DATE('" & item.NewDueDate & "', 'MM/dd/yyyy') FROM DUAL"
            listSQL.Add(SQL)

            If item.AmountTagged <> 0 Then
                'Updating AM_WESM_BILL_SUMMARY
                SQL = "UPDATE AM_WESM_BILL_SUMMARY SET ENDING_BALANCE = ENDING_BALANCE + " & item.AmountTagged & ", ENERGY_WITHHOLD = ENERGY_WITHHOLD + " & item.AmountTagged & ", " & vbNewLine _
                                                        & "UPDATED_DATE = TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), UPDATED_BY = '" & AMModule.UserName & "'" & vbNewLine _
                                                        & "WHERE WESMBILL_SUMMARY_NO = '" & item.WESMBillSummary.WESMBillSummaryNo & "'"

                listSQL.Add(SQL)
                'Add AM_WESM_BILL_SUMMARY_HISTORY
                SQL = "INSERT INTO AM_WESM_BILL_SUMMARY_HISTORY (WESMBILL_SUMMARY_NO, DUE_DATE, AMOUNT, UPDATED_BY, UPDATED_DATE, PAYMENT_TYPE, CERTIFICATE_NO) " &
                    "SELECT '" & item.WESMBillSummary.WESMBillSummaryNo & "', TO_DATE('" & getSelectedCertificate.RemittanceDate & "','MM/DD/YYYY'), " & item.AmountTagged * -1 & ", '" &
                    AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & EnumPaymentNewType.Energy & "', " & getSelectedCertificate.CertificateNo & " FROM DUAL"
                listSQL.Add(SQL)
            End If
        Next

        For Each item In Me.WESMTransDetailsSummaryList.Where(Function(x) x.Status = EnumWESMTransDetailsSummaryStatus.UPDATED.ToString).ToList
            SQL = "UPDATE AM_WESM_TRANS_DETAILS_SUMMARY SET OBIN_EWT = " & item.OutstandingBalanceInEWT & vbNewLine _
               & " WHERE BUYER_TRANS_NO = '" & item.BuyerTransNo & "' AND SELLER_TRANS_NO = '" & item.SellerTransNo & "'"
            listSQL.Add(SQL)
        Next

        Dim getWTDSHisListAdded As List(Of WESMTransDetailsSummaryHistory) = Me.WESMTransDetailsSummaryHistoryList.Where(Function(x) x.Status = EnumWESMTransDetailsSummaryStatus.ADDED.ToString).ToList()
        For Each wItem In getWTDSHisListAdded
            SQL = "INSERT INTO AM_WESM_TRANS_DETAILS_SUMMARY_HISTORY(BUYER_TRANS_NO,BUYER_BILLING_ID,SELLER_TRANS_NO,SELLER_BILLING_ID,DUE_DATE,ALLOCATION_DATE,REMITTANCE_DATE,ALLOCATED_IN_ENERGY,ALLOCATED_IN_EWT,ALLOCATED_IN_DEFINT,ALLOCATED_IN_VAT,PROCESED_BY) " & vbNewLine _
                & "SELECT '" & wItem.BuyerTransNo & "', '" & wItem.BuyerBillingID & "', '" & wItem.SellerTransNo & "', '" & wItem.SellerBillingID & "', TO_DATE('" & wItem.DueDate.ToShortDateString & "', 'MM/DD/YYYY'), TO_DATE('" & wItem.AllocationDate.ToShortDateString & "','MM/DD/YYYY'), " & vbNewLine _
                       & "TO_DATE('" & wItem.RemittanceDate.ToShortDateString & "','MM/DD/YYYY'), 0, " & wItem.AllocatedInEWT & ", 0, 0, '" & AMModule.UserName & "' FROM DUAL"
            listSQL.Add(SQL)
        Next

        Dim getWTDSHisListUpdated As List(Of WESMTransDetailsSummaryHistory) = Me.WESMTransDetailsSummaryHistoryList.Where(Function(x) x.Status = EnumWESMTransDetailsSummaryStatus.UPDATED.ToString).ToList()
        For Each wItem In getWTDSHisListUpdated
            SQL = "UPDATE AM_WESM_TRANS_DETAILS_SUMMARY_HISTORY SET ALLOCATED_IN_EWT = ALLOCATED_IN_EWT +" & wItem.AllocatedInEWT & ", PROCESSED_DATE = SYSDATE, PROCESED_BY = '" & AMModule.UserName & "' " & vbNewLine _
                & "WHERE BUYER_TRANS_NO = '" & wItem.BuyerTransNo & "' AND SELLER_TRANS_NO = '" & wItem.SellerTransNo & "' " & vbNewLine _
                & "AND REMITTANCE_DATE = TO_DATE('" & wItem.RemittanceDate.ToShortDateString & "', 'MM/DD/YYYY') "
            listSQL.Add(SQL)
        Next

        If ct.IsCancellationRequested Then
            Throw New OperationCanceledException
        End If

        Me.SaveToDB(listSQL, progress, ct)
    End Sub
#End Region

#Region "Untag"
    Public Sub UntagEWTSelected(ByVal certificateNo As Long, ByVal progress As IProgress(Of ProgressClass), ByVal ct As CancellationToken)
        Dim listSQL As New List(Of String)
        Dim SysDateTime As Date = WBillHelper.GetSystemDateTime()
        Dim SQL As String = ""
        Dim getSelectedCertificate As WHTaxCertificateSTL = Me.GetWTCertStl(certificateNo)

        SQL = "UPDATE AM_CERTIFICATE_WHTAX_STL SET UNTAG_EWT = 1, UPDATED_DATE = TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), UPDATED_BY = '" & AMModule.UserName & "' WHERE CERTIFICATE_NO = " & certificateNo & " AND UNTAG_EWT = 0"
        listSQL.Add(SQL)

        For Each item In getSelectedCertificate.TagDetails
            If item.AmountTagged <> 0 Then
                'Updating AM_WESM_BILL_SUMMARY
                SQL = "UPDATE AM_WESM_BILL_SUMMARY SET ENDING_BALANCE = ENDING_BALANCE + " & (item.AmountTagged * -1) & ", ENERGY_WITHHOLD = ENERGY_WITHHOLD + " & (item.AmountTagged * -1) & ", " & vbNewLine _
                                                        & "UPDATED_DATE = TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), UPDATED_BY = '" & AMModule.UserName & "'" & vbNewLine _
                                                        & "WHERE WESMBILL_SUMMARY_NO = " & item.WESMBillSummary.WESMBillSummaryNo

                listSQL.Add(SQL)
                'Delete AM_WESM_BILL_SUMMARY_HISTORY               
                SQL = "DELETE FROM AM_WESM_BILL_SUMMARY_HISTORY" & vbNewLine _
                   & " WHERE WESMBILL_SUMMARY_NO = " & item.WESMBillSummary.WESMBillSummaryNo & vbNewLine _
                   & " AND PAYMENT_TYPE = " & EnumPaymentNewType.Energy & " AND CERTIFICATE_NO = " & getSelectedCertificate.CertificateNo
                listSQL.Add(SQL)
            End If
        Next

        Me.SaveToDB(listSQL, progress, ct)
    End Sub
#End Region

#Region "Save"
    Public Sub SaveWHTaxTransaction(ByVal progress As IProgress(Of ProgressClass), ByVal ct As CancellationToken)
        Dim listSQL As New List(Of String)
        Dim SysDateTime As Date = WBillHelper.GetSystemDateTime()
        Dim SQL As String = ""
        Dim newCertNo As Long = GetNextValSequence("SEQ_AM_CERTIF_NO")
        With Me.NewWHTaxCertSTL
            .CertificateNo = newCertNo
            SQL = "INSERT INTO AM_CERTIFICATE_WHTAX_STL (CERTIFICATE_NO,REMITTANCE_DATE,BILLING_IDNUMBER,COLLECTED_AMOUNT,UPDATED_BY,UPDATED_DATE) " & vbNewLine _
                & "SELECT " & newCertNo & ", TO_DATE('" & .RemittanceDate & "','MM/DD/YYYY'), '" & .BillingIDNumber.IDNumber & "', " & .CollectedAmount & vbNewLine _
                & ", '" & AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') FROM DUAL"
            listSQL.Add(SQL)

            For Each item In .TagDetails
                SQL = "INSERT INTO AM_CERTIFICATE_WHTAX_DETAILS (CERTIFICATE_NO,WESMBILL_SUMMARY_NO,ENDING_BALANCE,AMOUNT_TAGGED,NEW_ENDING_BALANCE,WHTAX_AMOUNT,NEW_DUE_DATE)" & vbNewLine _
                    & "SELECT " & newCertNo & ", " & item.WESMBillSummary.WESMBillSummaryNo & ", " & item.EndingBalance & ", " & item.AmountTagged & ", " & item.NewEndingBalance & ", " & item.WithholdingTaxAmount & ", TO_DATE('" & item.NewDueDate & "', 'MM/dd/yyyy') FROM DUAL"
                listSQL.Add(SQL)

                'Updating AM_WESM_BILL_SUMMARY
                SQL = "UPDATE AM_WESM_BILL_SUMMARY SET ENDING_BALANCE = ENDING_BALANCE + " & item.AmountTagged & ", ENERGY_WITHHOLD = ENERGY_WITHHOLD + " & item.AmountTagged & ", " & vbNewLine _
                                                    & "UPDATED_DATE = TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), UPDATED_BY = '" & AMModule.UserName & "'" & vbNewLine _
                                                    & "WHERE WESMBILL_SUMMARY_NO = '" & item.WESMBillSummary.WESMBillSummaryNo & "'"
                listSQL.Add(SQL)

                'Add AM_WESM_BILL_SUMMARY_HISTORY
                SQL = "INSERT INTO AM_WESM_BILL_SUMMARY_HISTORY (WESMBILL_SUMMARY_NO, DUE_DATE, AMOUNT, UPDATED_BY, UPDATED_DATE, COLLECTION_TYPE, CERTIFICATE_NO) " &
                    "SELECT '" & item.WESMBillSummary.WESMBillSummaryNo & "', TO_DATE('" & .RemittanceDate & "','MM/DD/YYYY'), " & item.AmountTagged & ", '" &
                    AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & EnumCollectionType.Energy & "', '" & newCertNo & "' FROM DUAL"
                listSQL.Add(SQL)

            Next
            listSQL.Add(SQL)
        End With

        Me.SaveToDB(listSQL, progress, ct)
    End Sub
    Private Sub SaveToDB(ByVal lisOfSQL As List(Of String), ByVal progress As IProgress(Of ProgressClass), ByVal ct As CancellationToken)
        Dim report As New DataReport
        Dim ListofSQL As New List(Of String)
        Try
            report = Me.DataAccess.ExecuteSaveQuery2(lisOfSQL, progress, ct)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub

#End Region

    Private Function GetNextValSequence(ByVal seqName As String) As Long
        Dim ret As Long
        Dim report As New DataReport
        Dim sql As String = ""
        Try
            sql = "SELECT " & seqName & ".NEXTVAL FROM DUAL"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(sql)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            While report.ReturnedIDatareader.Read()
                With report.ReturnedIDatareader
                    ret = CLng(.Item("NEXTVAL"))
                End With
            End While
            Return ret
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        Finally
            If Not report.ReturnedIDatareader.IsClosed Then
                report.ReturnedIDatareader.Close()
            End If
        End Try
    End Function
End Class
