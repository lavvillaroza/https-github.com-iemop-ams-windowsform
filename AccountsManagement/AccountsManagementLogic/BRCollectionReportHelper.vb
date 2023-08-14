Imports System.Text
Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Imports System.ComponentModel
Imports Microsoft.Office.Interop
Imports System.Threading

Public Class BRCollectionReportHelper
    Dim _CollectionReportNo As Integer = 0
    Dim newProgress As ProgressClass



    Public Sub New()
        'Get the current instance of the dal
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._BIRRulingCR = New BRCollectionReport
        Me._DataAccess = DAL.GetInstance()
        Me._ARCollectionList = New List(Of ARCollection)
        Me._APAllocationList = New List(Of APAllocation)
        Me._AMParticipantsList = WBillHelper.GetAMParticipants()
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

#Region "BIR Ruling Collection Report"
    Private _BIRRulingCR As BRCollectionReport
    Public Property BIRRulingCR() As BRCollectionReport
        Get
            Return _BIRRulingCR
        End Get
        Set(ByVal value As BRCollectionReport)
            _BIRRulingCR = value
        End Set
    End Property
#End Region

#Region "Property of AM_COLLECTION_ALLOCATION Table"
    Private _ARCollectionList As New List(Of ARCollection)
    Public Property ARCollectionList() As List(Of ARCollection)
        Get
            Return _ARCollectionList
        End Get
        Set(ByVal value As List(Of ARCollection))
            _ARCollectionList = value
        End Set
    End Property
#End Region

#Region "Property of AM_PAYMENT_AP Table"
    Private _APAllocationList As New List(Of APAllocation)
    Public Property APAllocationList() As List(Of APAllocation)
        Get
            Return _APAllocationList
        End Get
        Set(ByVal value As List(Of APAllocation))
            _APAllocationList = value
        End Set
    End Property
#End Region

#Region "Property of AM Participants"
    Private _AMParticipantsList As List(Of AMParticipants)
    Public Property AMParticipantsList() As List(Of AMParticipants)
        Get
            Return _AMParticipantsList
        End Get
        Set(ByVal value As List(Of AMParticipants))
            _AMParticipantsList = value
        End Set
    End Property
#End Region

#Region "Property of WHTAX Certificate Transaction"
    Private _WHTAXCertificateTrans As List(Of WHTaxCertificateSTL)
    Public Property WHTAXCertificateTrans() As List(Of WHTaxCertificateSTL)
        Get
            Return _WHTAXCertificateTrans
        End Get
        Set(ByVal value As List(Of WHTaxCertificateSTL))
            _WHTAXCertificateTrans = value
        End Set
    End Property

#End Region

#Region "Get Collections in AR"
    Public Function GetCollectionAllocation(ByVal ARDateFrom As Date, ByVal ARDateTo As Date) As List(Of ARCollection)
        Dim result As New List(Of ARCollection)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT AWS.WESMBILL_SUMMARY_NO, AWS.BILLING_PERIOD, ACA.ID_NUMBER, AP.PARTICIPANT_ID, AWS.INV_DM_CM, AWS.DUE_DATE, ACA.ENDING_BALANCE, " & vbNewLine &
                                       "CASE WHEN ACA.AMOUNT<0 AND COLLECTION_TYPE IN (0,1,6,7,9,11) THEN ACA.ENDING_BALANCE - ABS(ACA.AMOUNT) " & vbNewLine &
                                        "WHEN ACA.AMOUNT>0  AND COLLECTION_TYPE IN (0,1,6,7,9,11) THEN ACA.ENDING_BALANCE - ACA.AMOUNT WHEN ACA.AMOUNT>0 AND COLLECTION_TYPE = 8 THEN ACA.ENDING_BALANCE ELSE 0 END AS NEW_ENDING_BALANCE, " & vbNewLine &
                                       "ACA.DUE_DATE AS NEW_DUEDATE, ACA.ALLOCATION_DATE, ACA.AMOUNT, ACA.ENERGY_WITHHOLD, ACA.COLLECTION_TYPE, AC.COLLECTION_CATEGORY, AWS.WESMBILL_BATCH_NO, AW.REMARKS " & vbNewLine &
                                "FROM AM_COLLECTION AC " & vbNewLine &
                                "JOIN AM_COLLECTION_ALLOCATION ACA ON AC.COLLECTION_NO = ACA.COLLECTION_NO " & vbNewLine &
                                "JOIN AM_PAYMENT_NEW APN ON APN.ALLOCATION_DATE = AC.ALLOCATION_DATE " & vbNewLine &
                                "JOIN AM_WESM_BILL_SUMMARY AWS ON AWS.WESMBILL_SUMMARY_NO = ACA.WESMBILL_SUMMARY_NO " & vbNewLine &
                                "JOIN AM_PARTICIPANTS AP ON AP.ID_NUMBER = ACA.ID_NUMBER " & vbNewLine &
                                "JOIN AM_WESM_BILL AW ON AW.INVOICE_NO = AWS.INV_DM_CM AND AW.CHARGE_TYPE = AWS.CHARGE_TYPE " & vbNewLine &
                                "WHERE APN.REMITTANCE_DATE >= TO_DATE('" & CDate(FormatDateTime(ARDateFrom, DateFormat.ShortDate)) & "', 'MM/DD/YYYY') " & vbNewLine &
                                "AND APN.REMITTANCE_DATE <= TO_DATE('" & CDate(FormatDateTime(ARDateTo, DateFormat.ShortDate)) & "', 'MM/DD/YYYY') " & vbNewLine &
                                "AND AC.IS_ALLOCATED = " & EnumIsAllocated.Allocated & " AND AC.IS_POSTED = 1 AND ACA.COLLECTION_TYPE IN (" & vbNewLine &
                                EnumCollectionType.DefaultInterestOnEnergy & ", " &
                                EnumCollectionType.Energy & ", " &
                                EnumCollectionType.VatOnEnergy & ") AND AWS.INV_DM_CM LIKE 'TS-W%' AND NOT UPPER(AWS.INV_DM_CM) LIKE '%-ADJ%'"

            report = DataAccess.ExecuteSelectQueryReturningDataReader(SQL)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.GetCollectionAllocationDetails(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function

    Public Function GetCollectionAllocationDetails(ByVal dr As IDataReader) As List(Of ARCollection)
        Dim result As New List(Of ARCollection)

        Try
            While dr.Read()
                Dim item As New ARCollection
                With dr
                    item.WESMBillBatchNo = CLng(.Item("WESMBILL_BATCH_NO"))
                    item.WESMBillSummaryNo = CLng(.Item("WESMBILL_SUMMARY_NO"))
                    item.BillingPeriod = CInt(.Item("BILLING_PERIOD").ToString())
                    item.IDNumber = CStr(.Item("ID_NUMBER").ToString())
                    item.ParticipantID = .Item("PARTICIPANT_ID").ToString()
                    item.InvoiceNumber = .Item("INV_DM_CM").ToString()
                    item.DueDate = CDate(FormatDateTime(CDate(.Item("DUE_DATE")), DateFormat.ShortDate))
                    item.EndingBalance = CDec(.Item("ENDING_BALANCE").ToString()) * -1
                    item.EnergyWithHold = CDec(.Item("ENERGY_WITHHOLD").ToString())
                    item.NewDueDate = CDate(FormatDateTime(CDate(.Item("NEW_DUEDATE")), DateFormat.ShortDate))
                    item.NewEndingBalance = CDec(.Item("NEW_ENDING_BALANCE").ToString()) * -1
                    item.AllocationDate = CDate(.Item("ALLOCATION_DATE").ToString())
                    item.AllocationAmount = CDec(.Item("AMOUNT").ToString()) * -1
                    item.CollectionType = CType(.Item("COLLECTION_TYPE"), EnumCollectionType)
                    item.CollectionCategory = CType(.Item("COLLECTION_CATEGORY"), EnumCollectionCategory)
                    item.BillingRemarks = CStr(.Item("REMARKS").ToString)
                    item.OffsettingSequence = 0
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

#Region "Get CollectionsAndPayments For EWT"
    Private Function GetListOfWTCertStl(ByVal dateFrom As Date, ByVal dateTo As Date) As List(Of WHTaxCertificateSTL)
        Dim ret As New List(Of WHTaxCertificateSTL)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.*, B.PARTICIPANT_ID, B.FULL_NAME FROM AM_CERTIFICATE_WHTAX_STL A " & vbNewLine _
                               & "LEFT JOIN AM_PARTICIPANTS B ON B.ID_NUMBER = A.BILLING_IDNUMBER " & vbNewLine _
                               & "INNER JOIN (SELECT DISTINCT T1.CERTIFICATE_NO, T2.DUE_DATE FROM AM_CERTIFICATE_WHTAX_DETAILS T1 " & vbNewLine _
                                            & "INNER JOIN AM_WESM_BILL_SUMMARY T2 ON T2.WESMBILL_SUMMARY_NO = T1.WESMBILL_SUMMARY_NO) C ON C.CERTIFICATE_NO = A.CERTIFICATE_NO " & vbNewLine _
                               & "WHERE A.REMITTANCE_DATE >= TO_DATE('" & dateFrom.ToShortDateString & "','MM/DD/YYYY') AND A.REMITTANCE_DATE <= TO_DATE('" & dateTo & "','MM/DD/YYYY') "
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

                    Dim getDetails As List(Of WHTaxCertificateDetails) = GetListOfWHTAXCertDetails(CLng(.Item("CERTIFICATE_NO")))
                    item.TagDetails = (From x In getDetails Where x.WESMBillSummary.BalanceType.ToString = "AR" Select x).ToList
                    item.AllocationDetails = (From x In getDetails Where x.WESMBillSummary.BalanceType.ToString = "AP" Select x).ToList
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
            Dim SQL As String = "SELECT A.CERTIFICATE_NO, A.WESMBILL_SUMMARY_NO As WBSNO, A.ENDING_BALANCE As EB, A.AMOUNT_TAGGED, A.NEW_ENDING_BALANCE As NEB, A.WHTAX_AMOUNT, A.NEW_DUE_DATE, B.*, C.ID_NUMBER, C.PARTICIPANT_ID, D.REMARKS, B.BALANCE_TYPE " & vbNewLine _
                              & "FROM AM_CERTIFICATE_WHTAX_DETAILS A " & vbNewLine _
                              & "INNER JOIN AM_WESM_BILL_SUMMARY B On B.WESMBILL_SUMMARY_NO = A.WESMBILL_SUMMARY_NO " & vbNewLine _
                              & "LEFT JOIN AM_PARTICIPANTS C On C.ID_NUMBER = B.ID_NUMBER " & vbNewLine _
                              & "LEFT JOIN (Select DISTINCT INVOICE_NO, REMARKS FROM AM_WESM_BILL) D On D.INVOICE_NO = B.INV_DM_CM " & vbNewLine _
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
                Dim itemWBS As New WESMBillSummary
                With dr

                    itemWBS.IDNumber = New AMParticipants(CStr(.Item("ID_NUMBER").ToString()), CStr(.Item("PARTICIPANT_ID").ToString()))
                    itemWBS.BillPeriod = CInt(.Item("BILLING_PERIOD").ToString())
                    itemWBS.ChargeType = CType(System.Enum.Parse(GetType(EnumChargeType), CStr(.Item("CHARGE_TYPE").ToString())), EnumChargeType)
                    itemWBS.DueDate = CDate(.Item("DUE_DATE").ToString())
                    itemWBS.BeginningBalance = Math.Round(CDec(.Item("BEGINNING_BALANCE").ToString()), 2)
                    itemWBS.EndingBalance = Math.Round(CDec(.Item("ENDING_BALANCE").ToString()), 2)
                    itemWBS.OrigEndingBalance = Math.Round(CDec(.Item("ENDING_BALANCE").ToString()), 2)
                    itemWBS.IDType = CStr(.Item("ID_TYPE").ToString())
                    itemWBS.NewDueDate = CDate(.Item("NEW_DUEDATE").ToString())
                    itemWBS.OrigNewDueDate = CDate(.Item("NEW_DUEDATE").ToString())
                    itemWBS.IsMFWTaxDeducted = CInt(.Item("IS_MFWTAX_DEDUCTED").ToString())
                    itemWBS.INVDMCMNo = CStr(.Item("INV_DM_CM").ToString())
                    If .Item("SUMMARY_TYPE") IsNot DBNull.Value Then
                        itemWBS.SummaryType = CType(System.Enum.Parse(GetType(EnumSummaryType), CStr(.Item("SUMMARY_TYPE").ToString())), EnumSummaryType)
                    End If
                    itemWBS.WESMBillSummaryNo = CLng(.Item("WESMBILL_SUMMARY_NO"))
                    itemWBS.Adjustment = CInt(.Item("ADJUSTMENT").ToString())
                    itemWBS.TransactionDate = CDate(.Item("TRANSACTION_DATE"))
                    itemWBS.EnergyWithhold = CDec(.Item("ENERGY_WITHHOLD"))
                    itemWBS.WESMBillBatchNo = CLng(.Item("WESMBILL_BATCH_NO"))
                    If .Item("ENERGY_WITHHOLD_STATUS") IsNot DBNull.Value Then
                        itemWBS.EnergyWithholdStatus = CType(CInt(.Item("ENERGY_WITHHOLD_STATUS")), EnumEnergyWithholdStatus)
                    Else
                        itemWBS.EnergyWithholdStatus = EnumEnergyWithholdStatus.NotApplicable
                    End If
                    itemWBS.NoOffset = CBool(IIf(CInt(.Item("NO_OFFSET")) = 1, True, False))
                    itemWBS.NoSOA = CBool(IIf(CInt(.Item("NO_SOA")) = 1, True, False))
                    itemWBS.NoDefInt = CBool(IIf(CInt(.Item("NO_DEFINT")) = 1, True, False))
                    itemWBS.BillingRemarks = CStr(.Item("REMARKS").ToString)
                    itemWBS.BalanceType = CType(System.Enum.Parse(GetType(EnumBalanceType), CStr(.Item("BALANCE_TYPE").ToString())), EnumBalanceType)

                    item.WESMBillSummary = itemWBS
                    item.NewDueDate = CDate(.Item("NEW_DUE_DATE").ToString())
                    item.EndingBalance = CDec(.Item("EB"))
                    item.Amount = CDec(.Item("AMOUNT_TAGGED"))
                    item.NewEndingBalance = CDec(.Item("NEB"))
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

    Public Function GetWESMBillSummaryItem(ByVal WESMBillNo As Long) As WESMBillSummary
        Dim result As New WESMBillSummary
        Dim report As New DataReport

        Try
            Dim SQL As String

            SQL = "Select A.ID_NUMBER, A.ID_TYPE, A.GROUP_NO, B.PARTICIPANT_ID, A.billing_period, A.TRANSACTION_DATE, A.ENERGY_WITHHOLD, " & vbNewLine &
                                  "b.participant_address, b.city, b.province, b.zip_code, " & vbNewLine &
                                  "A.charge_type, A.due_date, A.ENDING_BALANCE, a.BEGINNING_BALANCE, a.NEW_DUEDATE, a.IS_MFWTAX_DEDUCTED, " & vbNewLine &
                                  "A.INV_DM_CM, A.SUMMARY_TYPE, a.WESMBILL_SUMMARY_NO, A.ADJUSTMENT, A.WESMBILL_BATCH_NO, A.ENERGY_WITHHOLD_STATUS, a.NO_OFFSET, A.NO_SOA, A.NO_DEFINT, c.remarks " & vbNewLine &
                      "FROM AM_WESM_BILL_SUMMARY A " & vbNewLine &
                      "JOIN  AM_PARTICIPANTS B On a.id_number = b.id_number " & vbNewLine &
                      "JOIN  AM_WESM_BILL C On c.invoice_no = a.inv_dm_cm And c.charge_type = a.charge_type " & vbNewLine &
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
            Throw New ApplicationException("Error In row " & index & " --- " & ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return result
    End Function
#End Region

#Region "Get Payments In AP"
    Public Function GetAMPaymentNewAP(ByVal ARDateFrom As Date, ByVal ARDateTo As Date) As List(Of APAllocation)
        Dim ret As New List(Of APAllocation)
        Dim report As New DataReport
        Try
            Dim SQL As String = "Select A.*, B.CHARGE_TYPE, B.INV_DM_CM, C.ID_NUMBER, C.PARTICIPANT_ID, B.WESMBILL_BATCH_NO, D.REMARKS, E.REMITTANCE_DATE FROM AM_PAYMENT_NEW_AP A " _
                              & "INNER JOIN AM_WESM_BILL_SUMMARY B On A.WESMBILL_SUMMARY_NO = B.WESMBILL_SUMMARY_NO " _
                              & "INNER JOIN AM_PARTICIPANTS C On C.ID_NUMBER = A.ID_NUMBER " _
                              & "INNER JOIN AM_WESM_BILL D On D.INVOICE_NO = B.INV_DM_CM And D.CHARGE_TYPE = B.CHARGE_TYPE " _
                              & "INNER JOIN AM_PAYMENT_NEW E On E.PAYMENT_NO = A.PAYMENT_NO " _
                              & "WHERE E.REMITTANCE_DATE  >= " & "TO_DATE('" & CDate(FormatDateTime(ARDateFrom, DateFormat.ShortDate)) & "', 'MM/DD/YYYY') " & vbNewLine _
                              & "AND E.REMITTANCE_DATE <= TO_DATE('" & CDate(FormatDateTime(ARDateTo, DateFormat.ShortDate)) & "', 'MM/DD/YYYY') " & vbNewLine _
                              & "AND B.BALANCE_TYPE = 'AP' AND B.INV_DM_CM LIKE 'TS-W%' AND NOT UPPER(B.INV_DM_CM) LIKE '%-ADJ%'"

            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            ret = Me.GetAMPaymentNewAP(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetAMPaymentNewAP(ByVal dr As IDataReader) As List(Of APAllocation)
        Dim ret As New List(Of APAllocation)
        Try
            While dr.Read()
                With dr
                    Dim item As New APAllocation
                    item.BillingRemarks = CStr(.Item("REMARKS"))
                    item.BillingPeriod = CInt(.Item("BILLING_PERIOD"))
                    item.EndingBalance = CDec(.Item("ENDING_BALANCE"))
                    item.NewEndingBalance = CDec(.Item("NEW_ENDING_BALANCE"))
                    item.DueDate = CDate(FormatDateTime(CDate(.Item("DUE_DATE")), DateFormat.ShortDate))
                    item.NewDueDate = CDate(FormatDateTime(CDate(.Item("NEW_DUEDATE")), DateFormat.ShortDate))
                    item.AllocationAmount = CDec(.Item("ALLOCATION_AMOUNT"))
                    item.PaymentType = CType(.Item("PAYMENT_TYPE"), EnumPaymentNewType)
                    item.PaymentCategory = CType(.Item("PAYMENT_CATEGORY"), EnumCollectionCategory)
                    item.AllocationDate = CDate(FormatDateTime(CDate(.Item("ALLOCATION_DATE")), DateFormat.ShortDate))
                    item.RemittanceDate = CDate(FormatDateTime(CDate(.Item("REMITTANCE_DATE")), DateFormat.ShortDate))
                    item.WESMBillSummaryNo = CLng(.Item("WESMBILL_SUMMARY_NO"))
                    item.WESMBillBatchNo = CLng(.Item("WESMBILL_BATCH_NO"))
                    item.ChargeType = CType(CStr(System.Enum.Parse(GetType(EnumChargeType), CStr(.Item("CHARGE_TYPE")))), EnumChargeType)
                    item.OffsettingSequence = 0
                    item.InvoiceNumber = CStr(.Item("INV_DM_CM"))
                    item.IDNumber = CStr(.Item("ID_NUMBER"))
                    item.EnergyWithHold = CDec(.Item("ENERGY_WITHHOLD"))
                    item.ParticipantID = CStr(.Item("PARTICIPANT_ID"))
                    item.GeneratedDMCM = CLng(.Item("AM_DMCM_NO"))
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

#Region "Get WESM Transaction Cover Summary"
    Private Function GetListWESMTransCoverSummary(ByVal transNumber As String, ByVal getDetails As Boolean) As WESMBillAllocCoverSummary
        Dim ret As New WESMBillAllocCoverSummary
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.* FROM AM_WESM_ALLOC_COVER_SUMMARY A " & vbNewLine _
                              & "WHERE A.TRANSACTION_NUMBER =  '" & transNumber & "'"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetListWESMTransCoverSummary(report.ReturnedIDatareader, getDetails)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function
    Private Function GetListWESMTransCoverSummary(ByVal dr As IDataReader, ByVal getDetails As Boolean) As WESMBillAllocCoverSummary
        Dim result As New WESMBillAllocCoverSummary
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
                    If getDetails = True Then
                        item.ListWBAllocDisDetails = GetWBAllocDisDetails(CLng(.Item("SUMMARY_ID")))
                    End If
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

#Region "Get WESM Transaction Details Summary History"
    Private Function GetListWESMTransDetailsSummaryHistory(ByVal buyerTransNo As String) As List(Of WESMTransDetailsSummaryHistory)
        Dim ret As New List(Of WESMTransDetailsSummaryHistory)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.* FROM AM_WESM_TRANS_DETAILS_SUMMARY_HISTORY A " & vbNewLine _
                              & "WHERE A.BUYER_TRANS_NO = '" & buyerTransNo & "'"
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

    Private Function GetListWESMTransDetailsSummaryHistoryByRemittanceDate(ByVal remittanceDate As Date) As List(Of WESMTransDetailsSummaryHistory)
        Dim ret As New List(Of WESMTransDetailsSummaryHistory)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.* FROM AM_WESM_TRANS_DETAILS_SUMMARY_HISTORY A " & vbNewLine _
                              & "WHERE A.REMITTANCE_DATE = TO_DATE('" & remittanceDate & "','MM/DD/YYYY')"
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

#Region "Get DueDate List"
    Public Function GetListOfDueDate() As List(Of Date)
        Dim ret As New List(Of Date)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT DISTINCT A.DUE_DATE FROM AM_WESM_BILL_SUMMARY A WHERE A.INV_DM_CM LIKE 'TS-%' Order By A.DUE_DATE DESC FETCH FIRST 6 ROWS ONLY"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetListOfDueDate(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function
    Private Function GetListOfDueDate(ByVal dr As IDataReader) As List(Of Date)
        Dim result As New List(Of Date)
        Try
            While dr.Read()
                With dr
                    result.Add(CDate(.Item("DUE_DATE")))
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

#Region "Get BIR Ruling For Viewing"
    Public Function GetListOfBRCollectionReport() As List(Of BRCollectionReport)
        Dim ret As New List(Of BRCollectionReport)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT A.* FROM AM_BRCOLLECTION_REPORT A Order By A.AM_BIRR_NO"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetListOfBRCollectionReport(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetListOfBRCollectionReport(ByVal dr As IDataReader) As List(Of BRCollectionReport)
        Dim result As New List(Of BRCollectionReport)
        Try
            While dr.Read()
                Dim itemBIR As New BRCollectionReport
                With dr
                    itemBIR.BIRRNo = CLng(.Item("AM_BIRR_NO"))
                    itemBIR.RemittanceDateFrom = CDate(.Item("REMITTANCE_DATE_FROM"))
                    itemBIR.RemittanceDateTo = CDate(.Item("REMITTANCE_DATE_TO"))
                    itemBIR.GeneratedDate = CDate(.Item("CREATED_DATE_TIME"))
                    itemBIR.GeneratedBy = CStr(.Item("CREATED_BY"))
                End With
                result.Add(itemBIR)
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
    Public Function CheckTransactionDateRange(ByVal dtFrom As Date, ByVal dtTo As Date) As Boolean
        Dim ret As Boolean
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT A.* FROM AM_BRCOLLECTION_REPORT A " & vbNewLine _
                              & "WHERE (A.REMITTANCE_DATE_FROM >= TO_DATE('" & dtFrom & "','MM/DD/YYYY') AND A.REMITTANCE_DATE_FROM <= TO_DATE('" & dtTo & "','MM/DD/YYYY')) " & vbNewLine _
                              & "AND (A.REMITTANCE_DATE_TO  >= TO_DATE('" & dtFrom & "','MM/DD/YYYY') AND A.REMITTANCE_DATE_TO <= TO_DATE('" & dtTo & "','MM/DD/YYYY'))"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.CheckTransactionDateRange(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return ret
    End Function

    Private Function CheckTransactionDateRange(ByVal dr As IDataReader) As Boolean
        Dim result As New Boolean
        Dim items As New List(Of BRCollectionReport)
        Try
            While dr.Read()
                Dim itemBIR As New BRCollectionReport
                With dr
                    itemBIR.BIRRNo = CLng(.Item("AM_BIRR_NO"))
                    itemBIR.RemittanceDateFrom = CDate(.Item("REMITTANCE_DATE_FROM"))
                    itemBIR.RemittanceDateTo = CDate(.Item("REMITTANCE_DATE_TO"))
                    itemBIR.GeneratedDate = CDate(.Item("CREATED_DATE_TIME"))
                    itemBIR.GeneratedBy = CStr(.Item("CREATED_BY"))
                End With
                items.Add(itemBIR)
            End While

            If items.Count = 0 Then
                result = False
            Else
                result = True
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

    Public Function GetBIRRulingCRDateFromList() As List(Of BIRRuling)
        Dim ret As New List(Of BIRRuling)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT A.* FROM AM_BIR_RULING_CR A Order By A.TRANS_DATE_FROM"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetBIRRulingCRDateFromList(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetBIRRulingCRDateFromList(ByVal dr As IDataReader) As List(Of BIRRuling)
        Dim result As New List(Of BIRRuling)
        Try
            While dr.Read()
                Dim itemBIR As New BIRRuling
                With dr
                    itemBIR.TransactionDateFrom = CDate(.Item("TRANS_DATE_FROM"))
                    itemBIR.TransactionDateTo = CDate(.Item("TRANS_DATE_TO"))
                End With
                result.Add(itemBIR)
            End While

            'If result.Count = 0 Then
            '    Throw New Exception("No available record.")
            'End If

        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return result
    End Function
    Public Sub SetBIRRulingData(ByVal _BIRRNo)
        Me._BIRRulingCR = GetBIRRulingCR(_BIRRNo)
    End Sub

    Public Sub SetBIRRulingDataForBuyers()
        Dim dicBuyerDetails As New Dictionary(Of String, List(Of BRCollectionReportPurchasesDetails))
        For Each item In Me.BIRRulingCR.SellerDetails
            For Each oitem In item.BRCollReportDetails
                If Not dicBuyerDetails.ContainsKey(oitem.BuyerParticipant.IDNumber) Then
                    Dim newBRCRDetailsList As New List(Of BRCollectionReportPurchasesDetails)
                    Dim newBRCRDetail As New BRCollectionReportPurchasesDetails
                    With newBRCRDetail
                        .BillingPeriod = oitem.BillingPeriod
                        .STLRun = oitem.STLRun
                        .Particulars = oitem.Particulars
                        .CollectionDate = oitem.CollectionDate
                        .SellerParticipant = item.SellerParticipantInfo
                        .InvoiceNo = oitem.BuyerInvoiceNo
                        .SellerInvoiceNo = oitem.InvoiceNo
                        .VatablePurchases = oitem.VatableSales * -1
                        .ZeroRatedPurchases = oitem.ZeroRatedSales * -1
                        .ZeroRatedEcoZonePurchases = oitem.ZeroRatedEcoZoneSales * -1
                        .VatOnPurchases = oitem.VatOnSales * -1
                        .WithholdingTax = oitem.WithholdingTax * -1
                    End With
                    newBRCRDetailsList.Add(newBRCRDetail)
                    dicBuyerDetails.Add(oitem.BuyerParticipant.IDNumber, newBRCRDetailsList)
                Else
                    Dim newBRCRDetail As New BRCollectionReportPurchasesDetails
                    With newBRCRDetail
                        .BillingPeriod = oitem.BillingPeriod
                        .STLRun = oitem.STLRun
                        .Particulars = oitem.Particulars
                        .CollectionDate = oitem.CollectionDate
                        .SellerParticipant = item.SellerParticipantInfo
                        .InvoiceNo = oitem.BuyerInvoiceNo
                        .SellerInvoiceNo = oitem.InvoiceNo
                        .VatablePurchases = oitem.VatableSales * -1
                        .ZeroRatedPurchases = oitem.ZeroRatedSales * -1
                        .ZeroRatedEcoZonePurchases = oitem.ZeroRatedEcoZoneSales * -1
                        .VatOnPurchases = oitem.VatOnSales * -1
                        .WithholdingTax = oitem.WithholdingTax * -1
                    End With
                    dicBuyerDetails.Item(oitem.BuyerParticipant.IDNumber).Add(newBRCRDetail)
                End If
            Next
        Next
        Dim buyerCRNo As Long = 0
        Dim listOfBRCRPartiBuyer As New List(Of BRCollectionReportBuyerParticipant)
        Dim keys As List(Of String) = dicBuyerDetails.Keys.ToList
        keys.Sort()

        For Each iKey As String In keys
            buyerCRNo += 1
            Dim newBRCRParticipantBuyer As New BRCollectionReportBuyerParticipant
            With newBRCRParticipantBuyer
                .BRCollReportNumber = buyerCRNo
                .Participant = AMParticipantsList.Where(Function(x) x.IDNumber = iKey).First
                .BRCollReportDetails = dicBuyerDetails.Item(iKey)
            End With
            listOfBRCRPartiBuyer.Add(newBRCRParticipantBuyer)
        Next
        listOfBRCRPartiBuyer.TrimExcess()

        Me.BIRRulingCR.BuyerDetails = listOfBRCRPartiBuyer

        listOfBRCRPartiBuyer = New List(Of BRCollectionReportBuyerParticipant)
    End Sub

    Public Function GetBIRRulingCR(ByVal _BIRRNo As Long) As BRCollectionReport
        Dim ret As New BRCollectionReport
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT A.* FROM AM_BRCOLLECTION_REPORT A " & vbNewLine _
                              & "WHERE A.AM_BIRR_NO = " & _BIRRNo
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetBIRRulingCR(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function
    Private Function GetBIRRulingCR(ByVal dr As IDataReader) As BRCollectionReport
        Dim result As New BRCollectionReport
        Try
            While dr.Read()
                With dr
                    result.BIRRNo = CLng(.Item("AM_BIRR_NO"))
                    result.RemittanceDateFrom = CDate(.Item("REMITTANCE_DATE_FROM"))
                    result.RemittanceDateTo = CDate(.Item("REMITTANCE_DATE_TO"))
                    result.GeneratedDate = CDate(.Item("CREATED_DATE_TIME"))
                    result.GeneratedBy = CStr(.Item("CREATED_BY"))
                    result.SellerDetails = GetBRCollReportParticipants(CInt(.Item("AM_BIRR_NO")))
                End With
            End While

            If result.SellerDetails Is Nothing Then
                Throw New Exception("No available record.")
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

    Public Function GetBRCollReportParticipants(ByVal _BIRRNo As Long) As List(Of BRCollectionReportSellerParticipant)
        Dim ret As New List(Of BRCollectionReportSellerParticipant)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT A.*, B.* FROM AM_BRCOLLECTION_REPORT_TP A " & vbNewLine _
                              & "LEFT JOIN AM_PARTICIPANTS B ON B.ID_NUMBER = A.ID_NUMBER " & vbNewLine _
                              & "WHERE A.AM_BIRR_NO = " & _BIRRNo & " ORDER BY A.ID_NUMBER"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetBRCollReportParticipants(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetBRCollReportParticipants(ByVal dr As IDataReader) As List(Of BRCollectionReportSellerParticipant)
        Dim result As New List(Of BRCollectionReportSellerParticipant)

        Try
            While dr.Read()
                Dim item As New BRCollectionReportSellerParticipant

                With dr
                    item.BRCollReportNumber = CLng(.Item("AM_BIRR_CRNO"))
                    item.SellerParticipantInfo = New AMParticipants(CStr(.Item("ID_NUMBER")), CStr(.Item("PARTICIPANT_ID")), CStr(.Item("FULL_NAME")), If(IsDBNull(.Item("PARTICIPANT_ADDRESS").ToString()), "", .Item("PARTICIPANT_ADDRESS").ToString()))
                    item.BRCollReportDetails = GetBRCollReportDetails(CInt(.Item("AM_BIRR_CRNO")))
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

    Public Function GetBRCollReportDetails(ByVal _BRCRNo As Integer) As List(Of BRCollectionReportSalesDetails)
        Dim ret As New List(Of BRCollectionReportSalesDetails)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT A.*, B.FULL_NAME FROM AM_BRCOLLECTION_REPORT_DETAILS A " & vbNewLine _
                              & "LEFT JOIN AM_PARTICIPANTS B ON B.ID_NUMBER = A.BUYER_ID_NUMBER " & vbNewLine _
                              & "WHERE A.AM_BIRR_CRNO = " & _BRCRNo
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetBRCollReportDetails(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetBRCollReportDetails(ByVal dr As IDataReader) As List(Of BRCollectionReportSalesDetails)
        Dim result As New List(Of BRCollectionReportSalesDetails)

        Try
            While dr.Read()
                Dim item As New BRCollectionReportSalesDetails
                With dr
                    item.CRBatchNo = CInt(.Item("AM_BIRR_CRNO"))
                    item.BillingPeriod = CInt(.Item("BILLING_PERIOD"))
                    item.STLRun = CStr(.Item("STL_RUN"))
                    item.Particulars = CStr(.Item("PARTICULARS"))
                    item.CollectionDate = CDate(.Item("COLLECTION_DATE"))
                    item.BuyerParticipant = New AMParticipants(CStr(.Item("BUYER_ID_NUMBER")), CStr(.Item("BUYER_ID_NUMBER")), CStr(.Item("FULL_NAME")))
                    item.InvoiceNo = CStr(.Item("INVOICE_NO"))
                    item.VatableSales = CDec(.Item("VATABLE_SALES"))
                    item.ZeroRatedSales = CDec(.Item("ZERO_RATED_SALES"))
                    item.ZeroRatedEcoZoneSales = CDec(.Item("ZERO_RATED_ECOZONE"))
                    item.VatOnSales = CDec(.Item("VAT_ON_SALES"))
                    item.WithholdingTax = CDec(.Item("WH_TAX"))
                    item.BuyerInvoiceNo = CStr(If(IsDBNull(.Item("BUYER_INV_NO")), "", .Item("BUYER_INV_NO")))
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

#Region "Save Genereated BIR Ruling"
    Private Sub SaveGenereatedBIRRuling(ByRef _BRCollReport As BRCollectionReport, ByVal progress As IProgress(Of ProgressClass),
                                   ByVal ct As CancellationToken)
        Dim listSQL As New List(Of String)
        Dim SysDateTime As Date = WBillHelper.GetSystemDateTime()
        Dim SQL As String = ""
        Dim newBIRNumber As Long = GetSequenceID("SEQ_AM_BIRRULING_NO")

        newProgress = New ProgressClass
        newProgress.ProgressMsg = "Creating SQL scripts to save!"
        progress.Report(newProgress)

        With _BRCollReport
            .BIRRNo = newBIRNumber
            SQL = "INSERT INTO AM_BRCOLLECTION_REPORT (AM_BIRR_NO,REMITTANCE_DATE_FROM,REMITTANCE_DATE_TO,CREATED_BY,CREATED_DATE_TIME) " & vbNewLine _
                & "SELECT " & newBIRNumber & ", TO_DATE('" & .RemittanceDateFrom & "','MM/DD/YYYY'), TO_DATE('" & .RemittanceDateTo & "','MM/DD/YYYY'), '" & AMModule.UserName & "', " & vbNewLine _
                & "TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') FROM DUAL"
            listSQL.Add(SQL)

            For Each item In .SellerDetails
                Dim newBIRPNumber As Long = GetSequenceID("SEQ_AM_BIRRULING_CRNO")
                SQL = "INSERT INTO AM_BRCOLLECTION_REPORT_TP (AM_BIRR_CRNO, ID_NUMBER, AM_BIRR_NO)" & vbNewLine _
                    & "SELECT " & newBIRPNumber & ", '" & item.SellerParticipantInfo.IDNumber & "', " & newBIRNumber & " FROM DUAL"
                listSQL.Add(SQL)
                For Each detail In item.BRCollReportDetails
                    SQL = "INSERT INTO AM_BRCOLLECTION_REPORT_DETAILS (AM_BIRR_CRNO, BUYER_ID_NUMBER, BILLING_PERIOD, STL_RUN, PARTICULARS, COLLECTION_DATE, INVOICE_NO, VATABLE_SALES, ZERO_RATED_SALES, ZERO_RATED_ECOZONE, VAT_ON_SALES, WH_TAX, BUYER_INV_NO, WH_VAT)" & vbNewLine _
                        & "SELECT " & newBIRPNumber & ", '" & detail.BuyerParticipant.IDNumber & "', " & detail.BillingPeriod & ", '" & detail.STLRun & "', '" & detail.Particulars & "', " & vbNewLine _
                        & "TO_DATE('" & detail.CollectionDate & "','MM/DD/YYYY'), '" & detail.InvoiceNo & "', " & detail.VatableSales & ", " & detail.ZeroRatedSales & ", " & vbNewLine _
                        & detail.ZeroRatedEcoZoneSales & ", " & detail.VatOnSales & ", " & detail.WithholdingTax & ", '" & detail.BuyerInvoiceNo & "', " & detail.WithholdingVat & " FROM DUAL"
                    listSQL.Add(SQL)
                Next
            Next
        End With

        newProgress = New ProgressClass
        newProgress.ProgressMsg = "Saving execution is on going!"
        progress.Report(newProgress)
        Me.SaveToDB(listSQL, progress, ct)
    End Sub

    Private Sub SaveToDB(ByVal lisOfSQL As List(Of String), ByVal progress As IProgress(Of ProgressClass),
                                   ByVal ct As CancellationToken)
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

#Region "Get Sequence ID"
    Public Function GetSequenceID(ByVal SequenceName As String) As Long
        Dim result As Long
        Dim report As New DataReport
        Dim SQL As String
        Try
            SQL = "SELECT " & SequenceName & ".NEXTVAL FROM DUAL"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            result = Me.GetSequenceID(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function

    Private Function GetSequenceID(ByVal dr As IDataReader) As Long
        Dim result As Long = 0

        Try
            While dr.Read()
                With dr
                    result = CLng(.Item("NEXTVAL"))
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

#Region "Generate BIR Ruling Collection Report"
    Public Sub GenerateBRCollectionReport(ByVal dateFrom As Date, ByVal dateTo As Date, ByVal progress As IProgress(Of ProgressClass),
                                   ByVal ct As CancellationToken)
        'Try
        newProgress = New ProgressClass
        newProgress.ProgressMsg = "Fetching AR Tagged Collection from " & dateFrom & " to " & dateTo
        progress.Report(newProgress)

        Me._ARCollectionList = GetCollectionAllocation(dateFrom, dateTo)

        newProgress.ProgressMsg = "Fetching AP Payment Allocation from " & dateFrom & " to " & dateTo
        progress.Report(newProgress)

        Me._APAllocationList = GetAMPaymentNewAP(dateFrom, dateTo)

        'newProgress.ProgressMsg = "Fetching EWT Certificate from " & dateFrom & " to " & dateTo
        'progress.Report(newProgress)
        'Me._WHTAXCertificateTrans = GetListOfWTCertStl(dateFrom, dateTo)

        Dim listOfBIRRulingParticipant As List(Of BRCollectionReportSellerParticipant) = New List(Of BRCollectionReportSellerParticipant)
        listOfBIRRulingParticipant = ProcessBRCollectionReportParticipants(progress)
        Dim newBRCollectionReport As New BRCollectionReport
        With newBRCollectionReport
            .RemittanceDateFrom = dateFrom
            .RemittanceDateTo = dateTo
            .GeneratedDate = Now()
            .GeneratedBy = AMModule.UserName
            .SellerDetails = listOfBIRRulingParticipant
        End With

        SaveGenereatedBIRRuling(newBRCollectionReport, progress, ct)
        SetBIRRulingData(newBRCollectionReport.BIRRNo)
        Me._BIRRulingCR = newBRCollectionReport
        'Catch ex As Exception
        '    Throw New Exception(ex.Message)
        'End Try
    End Sub

    Private Function ProcessBRCollectionReportParticipants(ByVal progress As IProgress(Of ProgressClass)) As List(Of BRCollectionReportSellerParticipant)
        Dim ret As New List(Of BRCollectionReportSellerParticipant)
        Dim finalListOfBIRRulingDetails As New List(Of BRCollectionReportSalesDetails)
        Dim dicCRNumber As New Dictionary(Of String, Integer)
        Dim listOfDate As List(Of Date) = (From x In Me.ARCollectionList Select x.AllocationDate Distinct Order By AllocationDate).ToList()
        Dim listWESMTransDetailsSummaryHistory As New List(Of WESMTransDetailsSummaryHistory)
        Dim listWESMTranAllocCoverSummary As New List(Of WESMBillAllocCoverSummary)
        Dim dicWTACoverSummary As New Dictionary(Of String, WESMBillAllocCoverSummary)
        Dim dicWESMTransDetailsSummaryHistory As New Dictionary(Of String, List(Of WESMTransDetailsSummaryHistory))

        newProgress.ProgressMsg = "Fetching WESM Transaction Allocation Cover Summary For Collection Report..."
        progress.Report(newProgress)

        For Each item In Me.ARCollectionList.Select(Function(y) y.InvoiceNumber).Distinct.ToList
            Dim getListWESMTransDetailsSummaryHist As List(Of WESMTransDetailsSummaryHistory) = GetListWESMTransDetailsSummaryHistory(item)
            If Not dicWESMTransDetailsSummaryHistory.ContainsKey(item) Then
                dicWESMTransDetailsSummaryHistory.Add(item, getListWESMTransDetailsSummaryHist)
            End If
            'If getListWESMTransDetailsSummaryHist.Count <> 0 Then
            '    listWESMTransDetailsSummaryHistory.AddRange(getListWESMTransDetailsSummaryHist)
            'End If
            Dim listWESMTransCoverSummaryAR As WESMBillAllocCoverSummary = GetListWESMTransCoverSummary(item, True)
            If Not dicWTACoverSummary.ContainsKey(item) Then
                dicWTACoverSummary.Add(item, listWESMTransCoverSummaryAR)
            End If
            'If listWESMTransCoverSummary.Count <> 0 Then
            '    listWESMTranAllocCoverSummary.AddRange(listWESMTransCoverSummary)
            'End If
        Next
        For Each item In Me.APAllocationList.Select(Function(x) x.InvoiceNumber).Distinct.ToList
            Dim listWESMTransCoverSummaryAP As WESMBillAllocCoverSummary = GetListWESMTransCoverSummary(item, True)
            If Not dicWTACoverSummary.ContainsKey(item) Then
                dicWTACoverSummary.Add(item, listWESMTransCoverSummaryAP)
            End If
            'listWESMTranAllocCoverSummary.Add(GetListWESMTransCoverSummary(item, True).First)
        Next

        newProgress.ProgressMsg = "Generating Collection Report started..."
        progress.Report(newProgress)

        For Each iDate In listOfDate
            'Get AR Collections
            Dim getARCollectionInEnergy As List(Of ARCollection) = (From x In Me.ARCollectionList Where x.AllocationDate = iDate And x.CollectionType = EnumCollectionType.Energy Select x).ToList
            Dim getARCollectionInEnergyDI As List(Of ARCollection) = (From x In Me.ARCollectionList Where x.AllocationDate = iDate And x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy Select x).ToList
            Dim getARCollectionInVAT As List(Of ARCollection) = (From x In Me.ARCollectionList Where x.AllocationDate = iDate And x.CollectionType = EnumCollectionType.VatOnEnergy Select x).ToList

            'Get AP Payments
            Dim getAPALlocationInEnergy As List(Of APAllocation) = (From x In Me.APAllocationList Where x.AllocationDate = iDate And (x.PaymentType = EnumPaymentNewType.Energy) Select x).ToList
            Dim getAPALlocationInEnergyDI As List(Of APAllocation) = (From x In Me.APAllocationList Where x.AllocationDate = iDate And (x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy) Select x).ToList
            Dim getAPALlocationInVAT As List(Of APAllocation) = (From x In Me.APAllocationList Where x.AllocationDate = iDate And x.PaymentType = EnumPaymentNewType.VatOnEnergy Select x).ToList

            'Energy
            Dim getInvNoInARCollectionInEnergy As List(Of String) = (From x In getARCollectionInEnergy Select x.InvoiceNumber Distinct Order By InvoiceNumber).ToList
            For Each invAR In getInvNoInARCollectionInEnergy
                Dim invARList As List(Of ARCollection) = (From x In getARCollectionInEnergy Where x.InvoiceNumber = invAR Select x).ToList
                Dim invARItem As ARCollection = (From x In invARList Select x).First
                Dim buyerWTACoverSummary As WESMBillAllocCoverSummary = dicWTACoverSummary(invARItem.InvoiceNumber) 'listWESMTranAllocCoverSummary.Where(Function(x) x.TransactionNo = invARItem.InvoiceNumber).First
                Dim buyerWTDSummaryHistory As List(Of WESMTransDetailsSummaryHistory) = dicWESMTransDetailsSummaryHistory(invARItem.InvoiceNumber).Where(Function(x) x.AllocationDate = invARItem.AllocationDate).ToList() 'listWESMTransDetailsSummaryHistory.Where(Function(x) x.BuyerTransNo = invARItem.InvoiceNumber And x.AllocationDate = invARItem.AllocationDate).ToList
                Dim buyerParticipantInfo As AMParticipants = (From x In Me.AMParticipantsList Where x.IDNumber = invARItem.IDNumber).First()

                Dim getInvNoInAPAllocationInEnergy As List(Of String) = getAPALlocationInEnergy.Where(Function(x) x.WESMBillBatchNo = invARItem.WESMBillBatchNo).Select(Function(y) y.InvoiceNumber).Distinct.ToList()
                Dim allocationAmount As Decimal = invARList.Select(Function(x) x.AllocationAmount).Sum

                Dim newListOfBRDetails As New List(Of BRCollectionReportSalesDetails)
                For Each invAP In getInvNoInAPAllocationInEnergy
                    Dim _InvAP As APAllocation = (From x In getAPALlocationInEnergy Where x.InvoiceNumber = invAP Select x).First
                    Dim sellerWTACoverSummary As WESMBillAllocCoverSummary = dicWTACoverSummary(invARItem.InvoiceNumber) 'listWESMTranAllocCoverSummary.Where(Function(x) x.TransactionNo = invAP).Select(Function(x) x).First
                    Dim sellerParticpantInfo As AMParticipants = (From x In Me.AMParticipantsList Where x.IDNumber = _InvAP.IDNumber).First
                    Dim sellerWTDSummaryHistory As List(Of WESMTransDetailsSummaryHistory) = buyerWTDSummaryHistory.Where(Function(x) x.SellerTransNo = invAP).ToList

                    Dim newBRDetails As New BRCollectionReportSalesDetails
                    Dim allocAmountShare As Decimal = 0D
                    Dim allocAmountShareEWT As Decimal = 0D

                    Dim getWTAllocDisDetails As WESMBillAllocDisaggDetails = sellerWTACoverSummary.ListWBAllocDisDetails.
                                                                             Where(Function(x) x.BillingID = buyerWTACoverSummary.BillingID).FirstOrDefault
                    If Not getWTAllocDisDetails Is Nothing Then
                        If sellerWTDSummaryHistory.Count = 1 Then
                            allocAmountShare = sellerWTDSummaryHistory.Select(Function(x) x.AllocatedInEnergy).FirstOrDefault
                            allocAmountShareEWT = sellerWTDSummaryHistory.Select(Function(x) x.AllocatedInEWT).FirstOrDefault
                        Else
                            allocAmountShare = sellerWTDSummaryHistory.Select(Function(x) x.AllocatedInEnergy).Sum
                            allocAmountShareEWT = sellerWTDSummaryHistory.Select(Function(x) x.AllocatedInEWT).Sum
                        End If

                        If Not dicCRNumber.ContainsKey(_InvAP.IDNumber) Then
                            _CollectionReportNo += 1
                            dicCRNumber.Add(_InvAP.IDNumber, _CollectionReportNo)
                        End If
                        newBRDetails.CRBatchNo = dicCRNumber.Item(_InvAP.IDNumber)
                        newBRDetails.BillingPeriod = sellerWTACoverSummary.BillingPeriod
                        newBRDetails.STLRun = sellerWTACoverSummary.STLRun
                        newBRDetails.BuyerParticipant = buyerParticipantInfo
                        newBRDetails.BuyerBillingID = buyerWTACoverSummary.BillingID
                        newBRDetails.CollectionDate = _InvAP.RemittanceDate
                        newBRDetails.InvoiceNo = _InvAP.InvoiceNumber
                        newBRDetails.BuyerInvoiceNo = invARItem.InvoiceNumber

                        newBRDetails.Particulars = "ENERGY - Cash"

                        newBRDetails.VatOnSales = 0
                        newBRDetails.WithholdingTax = 0

                        If getWTAllocDisDetails.VatableSales <> 0 Then
                            newBRDetails.VatableSales = (allocAmountShare + allocAmountShareEWT) * -1
                            newBRDetails.ZeroRatedSales = 0
                            newBRDetails.ZeroRatedEcoZoneSales = 0
                            newBRDetails.WithholdingTax = allocAmountShareEWT
                        ElseIf getWTAllocDisDetails.ZeroRatedSales <> 0 Then
                            newBRDetails.VatableSales = 0
                            newBRDetails.ZeroRatedSales = (allocAmountShare + allocAmountShareEWT) * -1
                            newBRDetails.ZeroRatedEcoZoneSales = 0
                            newBRDetails.WithholdingTax = allocAmountShareEWT
                        ElseIf getWTAllocDisDetails.ZeroRatedEcoZoneSales <> 0 Then
                            newBRDetails.VatableSales = 0
                            newBRDetails.ZeroRatedSales = 0
                            newBRDetails.ZeroRatedEcoZoneSales = (allocAmountShare + allocAmountShareEWT) * -1
                            newBRDetails.WithholdingTax = allocAmountShareEWT
                            newBRDetails.WithholdingVat = 0
                        End If
                        finalListOfBIRRulingDetails.Add(newBRDetails)
                    End If
                Next
            Next

            'Default Interest
            Dim getInvNoInARCollectionInEnergyDI As List(Of String) = (From x In getARCollectionInEnergyDI Select x.InvoiceNumber Distinct Order By InvoiceNumber).ToList
            For Each invAR In getInvNoInARCollectionInEnergyDI
                Dim invARList As List(Of ARCollection) = (From x In getARCollectionInEnergyDI Where x.InvoiceNumber = invAR Select x).ToList
                Dim invARItem As ARCollection = (From x In invARList Select x).First

                Dim buyerWTACoverSummary As WESMBillAllocCoverSummary = listWESMTranAllocCoverSummary.Where(Function(x) x.TransactionNo = invARItem.InvoiceNumber).Select(Function(x) x).First
                Dim buyerWTDSummaryHistory As List(Of WESMTransDetailsSummaryHistory) = listWESMTransDetailsSummaryHistory.
                                                                                Where(Function(x) x.BuyerTransNo = invARItem.InvoiceNumber And x.AllocationDate = invARItem.AllocationDate).ToList
                If buyerWTDSummaryHistory.Count = 0 Then
                    Exit For
                End If

                Dim buyerParticipantInfo As AMParticipants = (From x In Me.AMParticipantsList Where x.IDNumber = invARItem.IDNumber).First

                Dim getInvNoInAPAllocationInEnergyDI As List(Of String) = getAPALlocationInEnergyDI.Where(Function(x) x.WESMBillBatchNo = invARItem.WESMBillBatchNo).Select(Function(y) y.InvoiceNumber).Distinct.ToList()
                Dim allocationAmount As Decimal = invARList.Select(Function(x) x.AllocationAmount).Sum

                Dim newListOfBRDetails As New List(Of BRCollectionReportSalesDetails)
                For Each invAP In getInvNoInAPAllocationInEnergyDI
                    Dim _InvAP As APAllocation = (From x In getAPALlocationInEnergyDI Where x.InvoiceNumber = invAP Select x).First
                    Dim sellerWTACoverSummary As WESMBillAllocCoverSummary = listWESMTranAllocCoverSummary.Where(Function(x) x.TransactionNo = invAP).Select(Function(x) x).First
                    Dim sellerParticpantInfo As AMParticipants = (From x In Me.AMParticipantsList Where x.IDNumber = _InvAP.IDNumber).First
                    Dim sellerWTDSummaryHistory As List(Of WESMTransDetailsSummaryHistory) = buyerWTDSummaryHistory.Where(Function(x) x.SellerTransNo = invAP).ToList

                    Dim newBRDetails As New BRCollectionReportSalesDetails
                    Dim allocAmountShare As Decimal = 0D

                    Dim getWTAllocDisDetails As WESMBillAllocDisaggDetails = sellerWTACoverSummary.ListWBAllocDisDetails.
                                                                             Where(Function(x) x.BillingID = buyerWTACoverSummary.BillingID).FirstOrDefault

                    If Not getWTAllocDisDetails Is Nothing Then
                        If sellerWTDSummaryHistory.Count = 1 Then
                            allocAmountShare = sellerWTDSummaryHistory.Select(Function(x) x.AllocatedInDefInt).FirstOrDefault
                        Else
                            allocAmountShare = sellerWTDSummaryHistory.Select(Function(x) x.AllocatedInDefInt).Sum
                        End If

                        If Not dicCRNumber.ContainsKey(_InvAP.IDNumber) Then
                            _CollectionReportNo += 1
                            dicCRNumber.Add(_InvAP.IDNumber, _CollectionReportNo)
                        End If

                        newBRDetails.CRBatchNo = dicCRNumber.Item(_InvAP.IDNumber)
                        newBRDetails.BillingPeriod = sellerWTACoverSummary.BillingPeriod
                        newBRDetails.STLRun = sellerWTACoverSummary.STLRun
                        newBRDetails.BuyerParticipant = buyerParticipantInfo
                        newBRDetails.BuyerBillingID = buyerWTACoverSummary.BillingID
                        newBRDetails.CollectionDate = _InvAP.RemittanceDate
                        newBRDetails.InvoiceNo = _InvAP.InvoiceNumber
                        newBRDetails.BuyerInvoiceNo = invARItem.InvoiceNumber

                        newBRDetails.Particulars = "DEFINT - Cash"

                        newBRDetails.VatOnSales = 0
                        newBRDetails.WithholdingTax = 0

                        If getWTAllocDisDetails.VatableSales <> 0 Then
                            newBRDetails.VatableSales = allocAmountShare * -1
                            newBRDetails.ZeroRatedSales = 0
                            newBRDetails.ZeroRatedEcoZoneSales = 0
                            newBRDetails.WithholdingTax = 0
                        ElseIf getWTAllocDisDetails.ZeroRatedSales <> 0 Then
                            newBRDetails.VatableSales = 0
                            newBRDetails.ZeroRatedSales = allocAmountShare * -1
                            newBRDetails.ZeroRatedEcoZoneSales = 0
                            newBRDetails.WithholdingTax = 0
                        ElseIf getWTAllocDisDetails.ZeroRatedEcoZoneSales <> 0 Then
                            newBRDetails.VatableSales = 0
                            newBRDetails.ZeroRatedSales = 0
                            newBRDetails.ZeroRatedEcoZoneSales = allocAmountShare * -1
                            newBRDetails.WithholdingTax = 0
                            newBRDetails.WithholdingVat = 0
                        End If
                        If newBRDetails.NetSale <> 0 Then
                            finalListOfBIRRulingDetails.Add(newBRDetails)
                        End If
                    End If
                Next
                finalListOfBIRRulingDetails.TrimExcess()
            Next

            'VAT
            Dim getInvNoInARCollectionInVAT As List(Of String) = (From x In getARCollectionInVAT Select x.InvoiceNumber Distinct Order By InvoiceNumber).ToList
            For Each invAR In getInvNoInARCollectionInVAT
                Dim invARList As List(Of ARCollection) = (From x In getARCollectionInVAT Where x.InvoiceNumber = invAR Select x).ToList
                Dim invARItem As ARCollection = (From x In invARList Select x).First

                Dim buyerWTACoverSummary As WESMBillAllocCoverSummary = listWESMTranAllocCoverSummary.Where(Function(x) x.TransactionNo = invARItem.InvoiceNumber).Select(Function(x) x).First
                Dim buyerWTDSummaryHistory As List(Of WESMTransDetailsSummaryHistory) = listWESMTransDetailsSummaryHistory.
                                                                                Where(Function(x) x.BuyerTransNo = invARItem.InvoiceNumber And x.AllocationDate = invARItem.AllocationDate).ToList

                Dim buyerParticipantInfo As AMParticipants = (From x In Me.AMParticipantsList Where x.IDNumber = invARItem.IDNumber).First

                Dim getInvNoInAPAllocationInVAT As List(Of String) = getAPALlocationInVAT.Where(Function(x) x.WESMBillBatchNo = invARItem.WESMBillBatchNo).Select(Function(y) y.InvoiceNumber).Distinct.ToList()
                Dim allocationAmount As Decimal = invARList.Select(Function(x) x.AllocationAmount).Sum

                Dim newListOfBRDetails As New List(Of BRCollectionReportSalesDetails)

                For Each invAP In getInvNoInAPAllocationInVAT
                    Dim _InvAP As APAllocation = (From x In getAPALlocationInVAT Where x.InvoiceNumber = invAP Select x).First
                    Dim sellerWTACoverSummary As WESMBillAllocCoverSummary = listWESMTranAllocCoverSummary.Where(Function(x) x.TransactionNo = invAP).Select(Function(x) x).First
                    Dim sellerParticpantInfo As AMParticipants = (From x In Me.AMParticipantsList Where x.IDNumber = _InvAP.IDNumber).First
                    'Dim sellerWTDSummaryHistory As WESMTransDetailsSummaryHistory = buyerWTDSummaryHistory.Where(Function(x) x.SellerTransNo = invAP).FirstOrDefault
                    Dim sellerWTDSummaryHistory As List(Of WESMTransDetailsSummaryHistory) = buyerWTDSummaryHistory.Where(Function(x) x.SellerTransNo = invAP).ToList

                    Dim newBRDetails As New BRCollectionReportSalesDetails
                    Dim allocAmountShare As Decimal = 0D
                    Dim allocAmountShareWHVAT As Decimal = 0D
                    Dim getWTAllocDisDetails As WESMBillAllocDisaggDetails = sellerWTACoverSummary.ListWBAllocDisDetails.
                                                                             Where(Function(x) x.BillingID = buyerWTACoverSummary.BillingID).FirstOrDefault

                    If Not getWTAllocDisDetails Is Nothing Then
                        If sellerWTDSummaryHistory.Count = 1 Then
                            allocAmountShare = sellerWTDSummaryHistory.Select(Function(x) x.AllocatedInVAT).FirstOrDefault
                            allocAmountShareWHVAT = sellerWTDSummaryHistory.Select(Function(x) x.AllocatedInWVAT).FirstOrDefault
                        Else
                            allocAmountShare = sellerWTDSummaryHistory.Select(Function(x) x.AllocatedInVAT).Sum
                            allocAmountShareWHVAT = sellerWTDSummaryHistory.Select(Function(x) x.AllocatedInWVAT).Sum
                        End If
                        If Not dicCRNumber.ContainsKey(_InvAP.IDNumber) Then
                            _CollectionReportNo += 1
                            dicCRNumber.Add(_InvAP.IDNumber, _CollectionReportNo)
                        End If
                        newBRDetails.CRBatchNo = dicCRNumber.Item(_InvAP.IDNumber)
                        newBRDetails.BillingPeriod = sellerWTACoverSummary.BillingPeriod
                        newBRDetails.STLRun = sellerWTACoverSummary.STLRun
                        newBRDetails.BuyerParticipant = buyerParticipantInfo
                        newBRDetails.BuyerBillingID = buyerWTACoverSummary.BillingID
                        newBRDetails.CollectionDate = _InvAP.RemittanceDate
                        newBRDetails.InvoiceNo = _InvAP.InvoiceNumber
                        newBRDetails.BuyerInvoiceNo = invARItem.InvoiceNumber
                        newBRDetails.Particulars = "VAT - Cash"
                        newBRDetails.VatableSales = 0
                        newBRDetails.ZeroRatedSales = 0
                        newBRDetails.ZeroRatedEcoZoneSales = 0
                        newBRDetails.VatOnSales = (allocAmountShare + allocAmountShareWHVAT) * -1
                        newBRDetails.WithholdingTax = 0
                        newBRDetails.WithholdingVat = allocAmountShareWHVAT
                        finalListOfBIRRulingDetails.Add(newBRDetails)
                    End If
                Next
                finalListOfBIRRulingDetails.TrimExcess()
            Next
            finalListOfBIRRulingDetails.TrimExcess()
        Next

        For Each item In dicCRNumber.Keys
            Dim newBIRP As New BRCollectionReportSellerParticipant
            Dim getParticipantInfo As AMParticipants = (From x In Me.AMParticipantsList Where x.IDNumber = item Select x).FirstOrDefault
            With newBIRP
                .BRCollReportNumber = dicCRNumber.Item(getParticipantInfo.IDNumber)
                .SellerParticipantInfo = getParticipantInfo
                .BRCollReportDetails = (From x In finalListOfBIRRulingDetails Where x.CRBatchNo = dicCRNumber.Item(item) Select x).ToList
            End With
            ret.Add(newBIRP)
        Next
        ret.TrimExcess()

        Return ret
    End Function


#End Region

#Region "Get Process BIR Ruling from DB"
    Public Sub ViewBIRRulingData(ByVal _BIRRNo As Long)
        Me._BIRRulingCR = GetBIRRulingCR(_BIRRNo)
    End Sub
#End Region

    Public Function GenerateCollectionReportDatatableDaily(ByVal dt As DataTable, ByVal listOfSelectedParticipants As List(Of String), ByVal isSeller As Boolean) As DataTable
        'Get the signatories for WESM Invoice
        Dim Signatory = WBillHelper.GetSignatories("BIR_COLLREPORT").First()
        If isSeller = True Then
            For Each item In listOfSelectedParticipants
                Dim selectedParticipants As BRCollectionReportSellerParticipant = (From x In Me.BIRRulingCR.SellerDetails Where x.SellerParticipantInfo.IDNumber = item Select x).FirstOrDefault
                For Each detail In selectedParticipants.BRCollReportDetails.OrderBy(Function(x) x.CollectionDate).ThenBy(Function(y) y.Particulars).ThenBy(Function(z) z.InvoiceNo)
                    Dim row = dt.NewRow()
                    row("TRANS_DATE_FROM") = Me.BIRRulingCR.RemittanceDateFrom.ToShortDateString()
                    row("TRANS_DATE_TO") = Me.BIRRulingCR.RemittanceDateTo.ToShortDateString()
                    row("CR_NUMBER") = "CR-N" & selectedParticipants.BRCollReportNumber.ToString("D7")
                    row("ID_NUMBER") = selectedParticipants.SellerParticipantInfo.IDNumber
                    row("FULL_NAME") = selectedParticipants.SellerParticipantInfo.FullName
                    row("ADDRESS") = selectedParticipants.SellerParticipantInfo.ParticipantAddress
                    row("COLLECTION_DATE") = detail.CollectionDate
                    row("BILLING_REMARKS") = detail.BillingPeriod.ToString & " - " & detail.STLRun.ToString
                    row("PARTICULARS") = detail.Particulars.ToString
                    row("RECEIVED_FROM") = detail.BuyerParticipant.FullName.ToString & " (" & detail.BuyerParticipant.IDNumber.ToString & "-" & detail.BuyerBillingID & ")"
                    row("STATEMENT_NO") = detail.InvoiceNo.ToString
                    row("VATABLE_SALES") = detail.VatableSales.ToString
                    row("ZERO_RATED_SALES") = detail.ZeroRatedSales.ToString
                    row("ZERO_RATED_ECOZONE") = detail.ZeroRatedEcoZoneSales.ToString
                    row("VAT_ON_SALES") = detail.VatOnSales.ToString
                    row("WHTAX") = detail.WithholdingTax.ToString
                    row("WHVAT") = detail.WithholdingVat.ToString
                    row("TOTAL") = detail.Total
                    row("SIGNATORIES_1") = AMModule.FullName
                    row("SIG1_POSITION") = AMModule.Position
                    row("SINATORIES_2") = Signatory.Signatory_1
                    row("SIG2_POSITION") = Signatory.Position_1
                    row("SIGNATORIES_3") = Signatory.Signatory_2
                    row("SIG3_POSITION") = Signatory.Position_2
                    dt.Rows.Add(row)
                Next
            Next
        Else
            For Each item In listOfSelectedParticipants
                Dim selectedParticipants As BRCollectionReportBuyerParticipant = (From x In Me.BIRRulingCR.BuyerDetails Where x.Participant.IDNumber = item Select x).FirstOrDefault
                For Each detail In selectedParticipants.BRCollReportDetails.OrderBy(Function(x) x.CollectionDate).ThenBy(Function(y) y.Particulars).ThenBy(Function(z) z.InvoiceNo)
                    Dim row = dt.NewRow()
                    row("TRANS_DATE_FROM") = Me.BIRRulingCR.RemittanceDateFrom.ToShortDateString()
                    row("TRANS_DATE_TO") = Me.BIRRulingCR.RemittanceDateTo.ToShortDateString()
                    row("CR_NUMBER") = "CR-N" & Me.BIRRulingCR.BIRRNo.ToString("D4") & "-" & selectedParticipants.BRCollReportNumber.ToString("D4")
                    row("ID_NUMBER") = selectedParticipants.Participant.IDNumber
                    row("FULL_NAME") = selectedParticipants.Participant.FullName
                    row("ADDRESS") = selectedParticipants.Participant.ParticipantAddress
                    row("COLLECTION_DATE") = detail.CollectionDate
                    row("BILLING_REMARKS") = detail.BillingPeriod.ToString & " - " & detail.STLRun.ToString
                    row("PARTICULARS") = detail.Particulars.ToString
                    row("RECEIVED_FROM") = detail.SellerParticipant.FullName.ToString & " (" & detail.SellerParticipant.IDNumber.ToString & "-" & detail.SellerBillingID & ")"
                    row("STATEMENT_NO") = detail.InvoiceNo.ToString
                    row("VATABLE_SALES") = detail.VatablePurchases.ToString
                    row("ZERO_RATED_SALES") = detail.ZeroRatedPurchases.ToString
                    row("ZERO_RATED_ECOZONE") = detail.ZeroRatedEcoZonePurchases.ToString
                    row("VAT_ON_SALES") = detail.VatOnPurchases.ToString
                    row("WHTAX") = detail.WithholdingTax.ToString
                    row("WHVAT") = detail.WithholdingVat.ToString
                    row("TOTAL") = detail.Total
                    row("SIGNATORIES_1") = AMModule.FullName
                    row("SIG1_POSITION") = AMModule.Position
                    row("SINATORIES_2") = Signatory.Signatory_1
                    row("SIG2_POSITION") = Signatory.Position_1
                    row("SIGNATORIES_3") = Signatory.Signatory_2
                    row("SIG3_POSITION") = Signatory.Position_2
                    dt.Rows.Add(row)
                Next
            Next
        End If

        Return dt
    End Function

    Public Function GenerateCollectionReportDatatableMonthly(ByVal dt As DataTable, ByVal listOfSelectedParticipants As List(Of String), ByVal isSeller As Boolean) As DataTable
        'Get the signatories for WESM Invoice
        Dim Signatory = WBillHelper.GetSignatories("BIR_COLLREPORT").First()

        If isSeller = True Then
            For Each item In listOfSelectedParticipants
                Dim selectedParticipants As BRCollectionReportSellerParticipant = (From x In Me.BIRRulingCR.SellerDetails Where x.SellerParticipantInfo.IDNumber = item Select x).FirstOrDefault

                Dim groupByBRCollReportDetails As List(Of BRCollectionReportSalesDetails) = selectedParticipants.BRCollReportDetails.
                                                                                        GroupBy(Function(cr) New With {Key cr.BillingPeriod, Key cr.STLRun, Key cr.Particulars,
                                                                                                Key cr.BuyerParticipant, Key cr.InvoiceNo, Key cr.BuyerInvoiceNo}).
                                                                                        Select(Function(t) New BRCollectionReportSalesDetails With {
                                                                                               .BillingPeriod = t.Key.BillingPeriod,
                                                                                               .STLRun = t.Key.STLRun,
                                                                                               .Particulars = t.Key.Particulars,
                                                                                               .BuyerParticipant = t.Key.BuyerParticipant,
                                                                                               .BuyerInvoiceNo = t.Key.BuyerInvoiceNo,
                                                                                               .InvoiceNo = t.Key.InvoiceNo,
                                                                                               .VatableSales = t.Sum(Function(x) x.VatableSales),
                                                                                               .ZeroRatedSales = t.Sum(Function(x) x.ZeroRatedSales),
                                                                                               .ZeroRatedEcoZoneSales = t.Sum(Function(x) x.ZeroRatedEcoZoneSales),
                                                                                               .VatOnSales = t.Sum(Function(x) x.VatOnSales),
                                                                                               .WithholdingTax = t.Sum(Function(x) x.WithholdingTax),
                                                                                               .WithholdingVat = t.Sum(Function(x) x.WithholdingVat)}).ToList

                For Each detail In groupByBRCollReportDetails
                    Dim row = dt.NewRow()
                    row("TRANS_DATE_FROM") = Me.BIRRulingCR.RemittanceDateFrom.ToShortDateString()
                    row("TRANS_DATE_TO") = Me.BIRRulingCR.RemittanceDateTo.ToShortDateString()
                    row("CR_NUMBER") = "CR-N" & selectedParticipants.BRCollReportNumber.ToString("D7")
                    row("ID_NUMBER") = selectedParticipants.SellerParticipantInfo.IDNumber
                    row("FULL_NAME") = selectedParticipants.SellerParticipantInfo.FullName
                    row("ADDRESS") = selectedParticipants.SellerParticipantInfo.ParticipantAddress
                    'row("COLLECTION_DATE") = detail.CollectionDate
                    row("BILLING_REMARKS") = detail.BillingPeriod.ToString & " - " & detail.STLRun.ToString
                    row("PARTICULARS") = detail.Particulars.ToString
                    row("RECEIVED_FROM") = detail.BuyerParticipant.FullName.ToString & " (" & detail.BuyerParticipant.IDNumber.ToString & "-" & detail.BuyerBillingID & ")"
                    row("STATEMENT_NO") = detail.InvoiceNo.ToString
                    row("VATABLE_SALES") = detail.VatableSales.ToString
                    row("ZERO_RATED_SALES") = detail.ZeroRatedSales.ToString
                    row("ZERO_RATED_ECOZONE") = detail.ZeroRatedEcoZoneSales.ToString
                    row("VAT_ON_SALES") = detail.VatOnSales.ToString
                    row("WHTAX") = detail.WithholdingTax.ToString
                    row("WHVAT") = detail.WithholdingVat.ToString
                    row("TOTAL") = detail.Total
                    row("SIGNATORIES_1") = AMModule.FullName
                    row("SIG1_POSITION") = AMModule.Position
                    row("SINATORIES_2") = Signatory.Signatory_1
                    row("SIG2_POSITION") = Signatory.Position_1
                    row("SIGNATORIES_3") = Signatory.Signatory_2
                    row("SIG3_POSITION") = Signatory.Position_2
                    dt.Rows.Add(row)
                Next
            Next
        Else
            For Each item In listOfSelectedParticipants
                Dim selectedParticipants As BRCollectionReportBuyerParticipant = (From x In Me.BIRRulingCR.BuyerDetails Where x.Participant.IDNumber = item Select x).FirstOrDefault

                Dim groupByBRCollReportDetails As List(Of BRCollectionReportPurchasesDetails) = selectedParticipants.BRCollReportDetails.
                                                                                        GroupBy(Function(cr) New With {Key cr.BillingPeriod, Key cr.STLRun, Key cr.Particulars,
                                                                                                Key cr.SellerParticipant, Key cr.InvoiceNo, Key cr.SellerInvoiceNo}).
                                                                                        Select(Function(t) New BRCollectionReportPurchasesDetails With {
                                                                                               .BillingPeriod = t.Key.BillingPeriod,
                                                                                               .STLRun = t.Key.STLRun,
                                                                                               .Particulars = t.Key.Particulars,
                                                                                               .SellerParticipant = t.Key.SellerParticipant,
                                                                                               .SellerInvoiceNo = t.Key.SellerInvoiceNo,
                                                                                               .InvoiceNo = t.Key.InvoiceNo,
                                                                                               .VatablePurchases = t.Sum(Function(x) x.VatablePurchases),
                                                                                               .ZeroRatedPurchases = t.Sum(Function(x) x.ZeroRatedPurchases),
                                                                                               .ZeroRatedEcoZonePurchases = t.Sum(Function(x) x.ZeroRatedEcoZonePurchases),
                                                                                               .VatOnPurchases = t.Sum(Function(x) x.VatOnPurchases),
                                                                                               .WithholdingTax = t.Sum(Function(x) x.WithholdingTax),
                                                                                               .WithholdingVat = t.Sum(Function(x) x.WithholdingVat)}).ToList

                For Each detail In groupByBRCollReportDetails
                    Dim row = dt.NewRow()
                    row("TRANS_DATE_FROM") = Me.BIRRulingCR.RemittanceDateFrom.ToShortDateString()
                    row("TRANS_DATE_TO") = Me.BIRRulingCR.RemittanceDateTo.ToShortDateString()
                    row("CR_NUMBER") = "CR-N" & Me.BIRRulingCR.BIRRNo.ToString("D4") & "-" & selectedParticipants.BRCollReportNumber.ToString("D4")
                    row("ID_NUMBER") = selectedParticipants.Participant.IDNumber
                    row("FULL_NAME") = selectedParticipants.Participant.FullName
                    row("ADDRESS") = selectedParticipants.Participant.ParticipantAddress
                    'row("COLLECTION_DATE") = detail.CollectionDate
                    row("BILLING_REMARKS") = detail.BillingPeriod.ToString & " - " & detail.STLRun.ToString
                    row("PARTICULARS") = detail.Particulars.ToString
                    row("RECEIVED_FROM") = detail.SellerParticipant.FullName.ToString & " (" & detail.SellerParticipant.IDNumber.ToString & "-" & detail.SellerBillingID & ")"
                    row("STATEMENT_NO") = detail.InvoiceNo.ToString
                    row("VATABLE_SALES") = detail.VatablePurchases.ToString
                    row("ZERO_RATED_SALES") = detail.ZeroRatedPurchases.ToString
                    row("ZERO_RATED_ECOZONE") = detail.ZeroRatedEcoZonePurchases.ToString
                    row("VAT_ON_SALES") = detail.VatOnPurchases.ToString
                    row("WHTAX") = detail.WithholdingTax.ToString
                    row("WHVAT") = detail.WithholdingVat.ToString
                    row("TOTAL") = detail.Total
                    row("SIGNATORIES_1") = AMModule.FullName
                    row("SIG1_POSITION") = AMModule.Position
                    row("SINATORIES_2") = Signatory.Signatory_1
                    row("SIG2_POSITION") = Signatory.Position_1
                    row("SIGNATORIES_3") = Signatory.Signatory_2
                    row("SIG3_POSITION") = Signatory.Position_2
                    dt.Rows.Add(row)
                Next
            Next
        End If

        Return dt
    End Function

    Public Function GenerateCollectionReportDatatableDaily(ByVal dt As DataTable, ByVal selectedParticipant As String, ByVal isSeller As Boolean) As DataTable
        'Get the signatories for WESM Invoice
        Dim Signatory = WBillHelper.GetSignatories("BIR_COLLREPORT").First()
        If isSeller = True Then
            Dim selectedParticipants As New BRCollectionReportSellerParticipant
            selectedParticipants = (From x In Me.BIRRulingCR.SellerDetails Where x.SellerParticipantInfo.IDNumber = selectedParticipant Select x).FirstOrDefault

            For Each detail In selectedParticipants.BRCollReportDetails
                Dim row = dt.NewRow()
                row("TRANS_DATE_FROM") = Me.BIRRulingCR.RemittanceDateFrom.ToShortDateString()
                row("TRANS_DATE_TO") = Me.BIRRulingCR.RemittanceDateTo.ToShortDateString()
                row("CR_NUMBER") = "CR-N" & selectedParticipants.BRCollReportNumber.ToString("D7")
                row("ID_NUMBER") = selectedParticipants.SellerParticipantInfo.IDNumber
                row("FULL_NAME") = selectedParticipants.SellerParticipantInfo.FullName
                row("ADDRESS") = selectedParticipants.SellerParticipantInfo.ParticipantAddress
                row("COLLECTION_DATE") = detail.CollectionDate.ToString
                row("BILLING_REMARKS") = detail.BillingPeriod.ToString & " - " & detail.STLRun.ToString
                row("PARTICULARS") = detail.Particulars.ToString
                row("RECEIVED_FROM") = detail.BuyerParticipant.FullName.ToString & " (" & detail.BuyerParticipant.IDNumber.ToString & "-" & detail.BuyerBillingID & ")"
                row("STATEMENT_NO") = detail.InvoiceNo.ToString
                row("VATABLE_SALES") = detail.VatableSales.ToString
                row("ZERO_RATED_SALES") = detail.ZeroRatedSales.ToString
                row("ZERO_RATED_ECOZONE") = detail.ZeroRatedEcoZoneSales.ToString
                row("VAT_ON_SALES") = detail.VatOnSales.ToString
                row("WHTAX") = detail.WithholdingTax.ToString
                row("WHVAT") = detail.WithholdingVat.ToString
                row("TOTAL") = detail.Total
                row("SIGNATORIES_1") = AMModule.FullName
                row("SIG1_POSITION") = AMModule.Position
                row("SINATORIES_2") = Signatory.Signatory_1
                row("SIG2_POSITION") = Signatory.Position_1
                row("SIGNATORIES_3") = Signatory.Signatory_2
                row("SIG3_POSITION") = Signatory.Position_2
                dt.Rows.Add(row)
            Next
        Else
            Dim selectedParticipants As New BRCollectionReportBuyerParticipant
            selectedParticipants = (From x In Me.BIRRulingCR.BuyerDetails Where x.Participant.IDNumber = selectedParticipant Select x).FirstOrDefault

            For Each detail In selectedParticipants.BRCollReportDetails
                Dim row = dt.NewRow()
                row("TRANS_DATE_FROM") = Me.BIRRulingCR.RemittanceDateFrom.ToShortDateString()
                row("TRANS_DATE_TO") = Me.BIRRulingCR.RemittanceDateTo.ToShortDateString()
                row("CR_NUMBER") = "CR-N" & Me.BIRRulingCR.BIRRNo.ToString("D4") & "-" & selectedParticipants.BRCollReportNumber.ToString("D4")
                row("ID_NUMBER") = selectedParticipants.Participant.IDNumber
                row("FULL_NAME") = selectedParticipants.Participant.FullName
                row("ADDRESS") = selectedParticipants.Participant.ParticipantAddress
                row("COLLECTION_DATE") = detail.CollectionDate.ToString
                row("BILLING_REMARKS") = detail.BillingPeriod.ToString & " - " & detail.STLRun.ToString
                row("PARTICULARS") = detail.Particulars.ToString
                row("RECEIVED_FROM") = detail.SellerParticipant.FullName.ToString & " (" & detail.SellerParticipant.IDNumber.ToString & "-" & detail.SellerBillingID & ")"
                row("STATEMENT_NO") = detail.InvoiceNo.ToString
                row("VATABLE_SALES") = detail.VatablePurchases.ToString
                row("ZERO_RATED_SALES") = detail.ZeroRatedPurchases.ToString
                row("ZERO_RATED_ECOZONE") = detail.ZeroRatedEcoZonePurchases.ToString
                row("VAT_ON_SALES") = detail.VatOnPurchases.ToString
                row("WHTAX") = detail.WithholdingTax.ToString
                row("WHVAT") = detail.WithholdingVat.ToString
                row("TOTAL") = detail.Total
                row("SIGNATORIES_1") = AMModule.FullName
                row("SIG1_POSITION") = AMModule.Position
                row("SINATORIES_2") = Signatory.Signatory_1
                row("SIG2_POSITION") = Signatory.Position_1
                row("SIGNATORIES_3") = Signatory.Signatory_2
                row("SIG3_POSITION") = Signatory.Position_2
                dt.Rows.Add(row)
            Next
        End If

        Return dt
    End Function

    Public Function GenerateCollectionReportDatatableMonthly(ByVal dt As DataTable, ByVal selectedParticipant As String, ByVal isSeller As Boolean) As DataTable
        'Get the signatories for WESM Invoice
        Dim Signatory = WBillHelper.GetSignatories("BIR_COLLREPORT").First()
        If isSeller = True Then
            Dim selectedParticipants As New BRCollectionReportSellerParticipant
            selectedParticipants = (From x In Me.BIRRulingCR.SellerDetails Where x.SellerParticipantInfo.IDNumber = selectedParticipant Select x).FirstOrDefault

            Dim groupByBRCollReportDetails As List(Of BRCollectionReportSalesDetails) = selectedParticipants.BRCollReportDetails.
                                                                                   GroupBy(Function(cr) New With {Key cr.BillingPeriod, Key cr.STLRun, Key cr.Particulars,
                                                                                           Key cr.BuyerParticipant, Key cr.InvoiceNo, Key cr.BuyerInvoiceNo}).
                                                                                   Select(Function(t) New BRCollectionReportSalesDetails With {
                                                                                          .BillingPeriod = t.Key.BillingPeriod,
                                                                                          .STLRun = t.Key.STLRun,
                                                                                          .Particulars = t.Key.Particulars,
                                                                                          .BuyerParticipant = t.Key.BuyerParticipant,
                                                                                          .BuyerInvoiceNo = t.Key.BuyerInvoiceNo,
                                                                                          .InvoiceNo = t.Key.InvoiceNo,
                                                                                          .VatableSales = t.Sum(Function(x) x.VatableSales),
                                                                                          .ZeroRatedSales = t.Sum(Function(x) x.ZeroRatedSales),
                                                                                          .ZeroRatedEcoZoneSales = t.Sum(Function(x) x.ZeroRatedEcoZoneSales),
                                                                                          .VatOnSales = t.Sum(Function(x) x.VatOnSales),
                                                                                          .WithholdingTax = t.Sum(Function(x) x.WithholdingTax),
                                                                                          .WithholdingVat = t.Sum(Function(x) x.WithholdingVat)}).ToList

            For Each detail In groupByBRCollReportDetails
                Dim row = dt.NewRow()
                row("TRANS_DATE_FROM") = Me.BIRRulingCR.RemittanceDateFrom.ToShortDateString()
                row("TRANS_DATE_TO") = Me.BIRRulingCR.RemittanceDateTo.ToShortDateString()
                row("CR_NUMBER") = "CR-N" & selectedParticipants.BRCollReportNumber.ToString("D7")
                row("ID_NUMBER") = selectedParticipants.SellerParticipantInfo.IDNumber
                row("FULL_NAME") = selectedParticipants.SellerParticipantInfo.FullName
                row("ADDRESS") = selectedParticipants.SellerParticipantInfo.ParticipantAddress
                'row("COLLECTION_DATE") = detail.CollectionDate
                row("BILLING_REMARKS") = detail.BillingPeriod.ToString & " - " & detail.STLRun.ToString
                row("PARTICULARS") = detail.Particulars.ToString
                row("RECEIVED_FROM") = detail.BuyerParticipant.FullName.ToString & " (" & detail.BuyerParticipant.IDNumber.ToString & "-" & detail.BuyerBillingID & ")"
                row("STATEMENT_NO") = detail.InvoiceNo.ToString
                row("VATABLE_SALES") = detail.VatableSales.ToString
                row("ZERO_RATED_SALES") = detail.ZeroRatedSales.ToString
                row("ZERO_RATED_ECOZONE") = detail.ZeroRatedEcoZoneSales.ToString
                row("VAT_ON_SALES") = detail.VatOnSales.ToString
                row("WHTAX") = detail.WithholdingTax.ToString
                row("WHVAT") = detail.WithholdingVat.ToString
                row("TOTAL") = detail.Total
                row("SIGNATORIES_1") = AMModule.FullName
                row("SIG1_POSITION") = AMModule.Position
                row("SINATORIES_2") = Signatory.Signatory_1
                row("SIG2_POSITION") = Signatory.Position_1
                row("SIGNATORIES_3") = Signatory.Signatory_2
                row("SIG3_POSITION") = Signatory.Position_2
                dt.Rows.Add(row)
            Next
        Else
            Dim selectedParticipants As New BRCollectionReportBuyerParticipant
            selectedParticipants = (From x In Me.BIRRulingCR.BuyerDetails Where x.Participant.IDNumber = selectedParticipant Select x).FirstOrDefault

            Dim groupByBRCollReportDetails As List(Of BRCollectionReportPurchasesDetails) = selectedParticipants.BRCollReportDetails.
                                                                                   GroupBy(Function(cr) New With {Key cr.BillingPeriod, Key cr.STLRun, Key cr.Particulars,
                                                                                           Key cr.SellerParticipant, Key cr.InvoiceNo, Key cr.SellerInvoiceNo}).
                                                                                   Select(Function(t) New BRCollectionReportPurchasesDetails With {
                                                                                          .BillingPeriod = t.Key.BillingPeriod,
                                                                                          .STLRun = t.Key.STLRun,
                                                                                          .Particulars = t.Key.Particulars,
                                                                                          .SellerParticipant = t.Key.SellerParticipant,
                                                                                          .SellerInvoiceNo = t.Key.SellerInvoiceNo,
                                                                                          .InvoiceNo = t.Key.InvoiceNo,
                                                                                          .VatablePurchases = t.Sum(Function(x) x.VatablePurchases),
                                                                                          .ZeroRatedPurchases = t.Sum(Function(x) x.ZeroRatedPurchases),
                                                                                          .ZeroRatedEcoZonePurchases = t.Sum(Function(x) x.ZeroRatedEcoZonePurchases),
                                                                                          .VatOnPurchases = t.Sum(Function(x) x.VatOnPurchases),
                                                                                          .WithholdingTax = t.Sum(Function(x) x.WithholdingTax),
                                                                                          .WithholdingVat = t.Sum(Function(x) x.WithholdingVat)}).ToList

            For Each detail In groupByBRCollReportDetails
                Dim row = dt.NewRow()
                row("TRANS_DATE_FROM") = Me.BIRRulingCR.RemittanceDateFrom.ToShortDateString()
                row("TRANS_DATE_TO") = Me.BIRRulingCR.RemittanceDateTo.ToShortDateString()
                row("CR_NUMBER") = "CR-N" & Me.BIRRulingCR.BIRRNo.ToString("D4") & "-" & selectedParticipants.BRCollReportNumber.ToString("D4")
                row("ID_NUMBER") = selectedParticipants.Participant.IDNumber
                row("FULL_NAME") = selectedParticipants.Participant.FullName
                row("ADDRESS") = selectedParticipants.Participant.ParticipantAddress
                'row("COLLECTION_DATE") = detail.CollectionDate
                row("BILLING_REMARKS") = detail.BillingPeriod.ToString & " - " & detail.STLRun.ToString
                row("PARTICULARS") = detail.Particulars.ToString
                row("RECEIVED_FROM") = detail.SellerParticipant.FullName.ToString & " (" & detail.SellerParticipant.IDNumber.ToString & "-" & detail.SellerBillingID & ")"
                row("STATEMENT_NO") = detail.InvoiceNo.ToString
                row("VATABLE_SALES") = detail.VatablePurchases.ToString
                row("ZERO_RATED_SALES") = detail.ZeroRatedPurchases.ToString
                row("ZERO_RATED_ECOZONE") = detail.ZeroRatedEcoZonePurchases.ToString
                row("VAT_ON_SALES") = detail.VatOnPurchases.ToString
                row("WHTAX") = detail.WithholdingTax.ToString
                row("WHVAT") = detail.WithholdingVat.ToString
                row("TOTAL") = detail.Total
                row("SIGNATORIES_1") = AMModule.FullName
                row("SIG1_POSITION") = AMModule.Position
                row("SINATORIES_2") = Signatory.Signatory_1
                row("SIG2_POSITION") = Signatory.Position_1
                row("SIGNATORIES_3") = Signatory.Signatory_2
                row("SIG3_POSITION") = Signatory.Position_2
                dt.Rows.Add(row)
            Next
        End If
        Return dt
    End Function

    Public Sub GenerateBIRCollectionReportExcel(ByVal Participant As String, ByVal SavingPathName As String, ByVal oneFileOnly As Boolean, ByVal collReporType As String, ByVal progress As IProgress(Of ProgressClass), ByVal isSeller As Boolean)
        newProgress = New ProgressClass
        If oneFileOnly = False Then
            Dim getParticipantInfo As AMParticipants = (From x In Me.AMParticipantsList Where x.IDNumber = Participant Select x).First
            If isSeller = True Then
                Dim getBIRParticipant As New BRCollectionReportSellerParticipant
                getBIRParticipant = (From x In Me.BIRRulingCR.SellerDetails Where x.SellerParticipantInfo.IDNumber = Participant Select x).First
                If collReporType.Equals("DAILY") Then
                    newProgress.ProgressMsg = "Extracting Consolidated Daily Collection Report " & Me.BIRRulingCR.RemittanceDateFrom.ToString("MMMM dd, yyyy") & " to " & Me.BIRRulingCR.RemittanceDateTo.ToString("MMMM dd, yyyy")
                    progress.Report(newProgress)
                    GenerateInExcelDailySeller(SavingPathName, getParticipantInfo, Me.BIRRulingCR.RemittanceDateFrom, Me.BIRRulingCR.RemittanceDateTo, getBIRParticipant)
                Else
                    newProgress.ProgressMsg = "Extracting Consolidated Monthly Collection Report " & Me.BIRRulingCR.RemittanceDateFrom.ToString("MMMM dd, yyyy") & " to " & Me.BIRRulingCR.RemittanceDateTo.ToString("MMMM dd, yyyy")
                    progress.Report(newProgress)
                    GenerateInExcelMonthlySeller(SavingPathName, getParticipantInfo, Me.BIRRulingCR.RemittanceDateFrom, Me.BIRRulingCR.RemittanceDateTo, getBIRParticipant)
                End If
            ElseIf isSeller = False Then
                Dim getBIRParticipant As New BRCollectionReportBuyerParticipant
                getBIRParticipant = (From x In Me.BIRRulingCR.BuyerDetails Where x.Participant.IDNumber = Participant Select x).First
                If collReporType.Equals("DAILY") Then
                    newProgress.ProgressMsg = "Extracting Consolidated Daily Collection Report " & Me.BIRRulingCR.RemittanceDateFrom.ToString("MMMM dd, yyyy") & " to " & Me.BIRRulingCR.RemittanceDateTo.ToString("MMMM dd, yyyy")
                    progress.Report(newProgress)
                    GenerateInExcelDailyBuyer(SavingPathName, getParticipantInfo, Me.BIRRulingCR.RemittanceDateFrom, Me.BIRRulingCR.RemittanceDateTo, getBIRParticipant)
                Else
                    newProgress.ProgressMsg = "Extracting Consolidated Monthly Collection Report " & Me.BIRRulingCR.RemittanceDateFrom.ToString("MMMM dd, yyyy") & " to " & Me.BIRRulingCR.RemittanceDateTo.ToString("MMMM dd, yyyy")
                    progress.Report(newProgress)
                    GenerateInExcelMonthlyBuyer(SavingPathName, getParticipantInfo, Me.BIRRulingCR.RemittanceDateFrom, Me.BIRRulingCR.RemittanceDateTo, getBIRParticipant)
                End If
            End If
        Else
            If isSeller = True Then
                If collReporType.Equals("DAILY") Then
                    newProgress.ProgressMsg = "Extracting Daily Collection Report per TP " & Me.BIRRulingCR.RemittanceDateFrom.ToString("MMMM dd, yyyy") & " to " & Me.BIRRulingCR.RemittanceDateTo.ToString("MMMM dd, yyyy")
                    progress.Report(newProgress)
                    GenerateAllInExcelDailySeller(SavingPathName, Me.BIRRulingCR.RemittanceDateFrom, Me.BIRRulingCR.RemittanceDateTo)
                Else
                    newProgress.ProgressMsg = "Extracting Monthly Collection Report per TP " & Me.BIRRulingCR.RemittanceDateFrom.ToString("MMMM dd, yyyy") & " to " & Me.BIRRulingCR.RemittanceDateTo.ToString("MMMM dd, yyyy")
                    progress.Report(newProgress)
                    GenerateAllInExcelMonthlySeller(SavingPathName, Me.BIRRulingCR.RemittanceDateFrom, Me.BIRRulingCR.RemittanceDateTo)
                End If
            ElseIf isSeller = False Then
                If collReporType.Equals("DAILY") Then
                    newProgress.ProgressMsg = "Extracting Daily Collection Report per TP " & Me.BIRRulingCR.RemittanceDateFrom.ToString("MMMM dd, yyyy") & " to " & Me.BIRRulingCR.RemittanceDateTo.ToString("MMMM dd, yyyy")
                    progress.Report(newProgress)
                    GenerateAllInExcelDailyBuyer(SavingPathName, Me.BIRRulingCR.RemittanceDateFrom, Me.BIRRulingCR.RemittanceDateTo)
                Else
                    newProgress.ProgressMsg = "Extracting Monthly Collection Report per TP " & Me.BIRRulingCR.RemittanceDateFrom.ToString("MMMM dd, yyyy") & " to " & Me.BIRRulingCR.RemittanceDateTo.ToString("MMMM dd, yyyy")
                    progress.Report(newProgress)
                    GenerateAllInExcelMonthlyBuyer(SavingPathName, Me.BIRRulingCR.RemittanceDateFrom, Me.BIRRulingCR.RemittanceDateTo)
                End If
            End If
        End If
    End Sub

    Private Sub GenerateAllInExcelDailySeller(ByVal SavingPathName As String, ByVal dateFrom As Date, ByVal dateTo As Date)
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlContentDetails As Excel.Range

        Dim misValue As Object = System.Reflection.Missing.Value
        Dim rowIndex As Integer = 1

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add()
        xlWorkSheet = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet.Name = "Collection Report Details"

        Dim contentDetails As Object(,) = New Object(,) {}
        Dim grandTotal As Object(,) = New Object(,) {}

        Try
            If Me.BIRRulingCR.SellerDetails.Count > 0 Then
                Dim bdIndx As Integer = 0
                Dim dicDate As New Dictionary(Of Date, Integer)

                Dim countDetails As Long = 0
                For Each item In Me.BIRRulingCR.SellerDetails
                    countDetails += item.BRCollReportDetails.Count
                Next

                ReDim contentDetails(countDetails + 1, 17)

                contentDetails(bdIndx, 0) = "Billing" & vbNewLine & "Remarks"
                contentDetails(bdIndx, 1) = "Remittance Date"
                contentDetails(bdIndx, 2) = "Particulars"
                contentDetails(bdIndx, 3) = "Seller STL ID"
                contentDetails(bdIndx, 4) = "Seller BILLING ID"
                contentDetails(bdIndx, 5) = "Seller Transaction No"
                contentDetails(bdIndx, 6) = "Buyer STL ID"
                contentDetails(bdIndx, 7) = "Buyer BILLING ID"
                contentDetails(bdIndx, 8) = "Buyer Full name"
                contentDetails(bdIndx, 9) = "Buyer BIR Registered Address"
                contentDetails(bdIndx, 10) = "Buyer BIR TIN"
                contentDetails(bdIndx, 11) = "Buyer Transaction No"
                contentDetails(bdIndx, 12) = "Vatable Sales"
                contentDetails(bdIndx, 13) = "Zero Rated Sales"
                contentDetails(bdIndx, 14) = "Zero Rated Ecozone"
                contentDetails(bdIndx, 15) = "VAT on Sales"
                contentDetails(bdIndx, 16) = "Withholding Tax"
                contentDetails(bdIndx, 17) = "Total"


                Dim gTotalVatableSales As Decimal = 0
                Dim gTotalZRSales As Decimal = 0
                Dim gTotalZREcozone As Decimal = 0
                Dim gTotalVATonSales As Decimal = 0
                Dim gTotalWhTax As Decimal = 0
                Dim gTotalTotal As Decimal = 0

                For Each birCRParticipant In Me.BIRRulingCR.SellerDetails
                    For Each item In birCRParticipant.BRCollReportDetails.OrderBy(Function(n As BRCollectionReportSalesDetails) n.CollectionDate).ThenBy(Function(x As BRCollectionReportSalesDetails) x.InvoiceNo).ToList
                        bdIndx += 1
                        gTotalVatableSales += item.VatableSales
                        gTotalZRSales += item.ZeroRatedSales
                        gTotalZREcozone += item.ZeroRatedEcoZoneSales
                        gTotalVATonSales += item.VatOnSales
                        gTotalWhTax += item.WithholdingTax
                        gTotalTotal += item.Total

                        contentDetails(bdIndx, 0) = item.BillingPeriod & " - " & item.STLRun
                        contentDetails(bdIndx, 1) = item.CollectionDate.ToShortDateString
                        contentDetails(bdIndx, 2) = item.Particulars
                        contentDetails(bdIndx, 3) = birCRParticipant.SellerParticipantInfo.IDNumber
                        contentDetails(bdIndx, 4) = birCRParticipant.SellerBillingID
                        contentDetails(bdIndx, 5) = item.InvoiceNo
                        contentDetails(bdIndx, 6) = item.BuyerParticipant.IDNumber
                        contentDetails(bdIndx, 7) = item.BuyerBillingID
                        contentDetails(bdIndx, 8) = item.BuyerParticipant.FullName
                        contentDetails(bdIndx, 9) = item.BuyerParticipant.ParticipantAddress
                        contentDetails(bdIndx, 10) = item.BuyerParticipant.TIN
                        contentDetails(bdIndx, 11) = item.BuyerInvoiceNo
                        contentDetails(bdIndx, 12) = item.VatableSales
                        contentDetails(bdIndx, 13) = item.ZeroRatedSales
                        contentDetails(bdIndx, 14) = item.ZeroRatedEcoZoneSales
                        contentDetails(bdIndx, 15) = item.VatOnSales
                        contentDetails(bdIndx, 16) = item.WithholdingTax
                        contentDetails(bdIndx, 17) = item.Total
                    Next
                Next

                bdIndx += 1
                contentDetails(bdIndx, 0) = ""
                contentDetails(bdIndx, 1) = "Grand Total: "
                contentDetails(bdIndx, 2) = ""
                contentDetails(bdIndx, 3) = ""
                contentDetails(bdIndx, 4) = ""
                contentDetails(bdIndx, 5) = ""
                contentDetails(bdIndx, 6) = ""
                contentDetails(bdIndx, 7) = ""
                contentDetails(bdIndx, 8) = ""
                contentDetails(bdIndx, 9) = ""
                contentDetails(bdIndx, 10) = ""
                contentDetails(bdIndx, 11) = gTotalVatableSales
                contentDetails(bdIndx, 12) = gTotalZRSales
                contentDetails(bdIndx, 13) = gTotalZREcozone
                contentDetails(bdIndx, 14) = gTotalVATonSales
                contentDetails(bdIndx, 15) = gTotalWhTax
                contentDetails(bdIndx, 16) = gTotalTotal
                contentDetails(bdIndx, 17) = ""

                xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                rowIndex += bdIndx
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 18), Excel.Range)

                xlContentDetails = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlContentDetails.Value = contentDetails
                Dim transDate As String = dateFrom.ToString("MM-dd-yyyy") & " - " & dateTo.ToString("MM-dd-yyyy") & "_DAILY"

                Dim FileName As String = "CSR_ALL_" & transDate & ".xlsx"

                xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                                    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
                xlWorkBook.Close(False)
                xlApp.Quit()

                releaseObject(xlContentDetails)
                releaseObject(xlRowRange2)
                releaseObject(xlRowRange1)
                releaseObject(xlWorkSheet)
                releaseObject(xlWorkBook)
                releaseObject(xlApp)

            End If
            '********************************************* Supply BIR Ruling Details per Participant
        Catch ex As Exception
            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlApp)
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub GenerateAllInExcelDailyBuyer(ByVal SavingPathName As String, ByVal dateFrom As Date, ByVal dateTo As Date)
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlContentDetails As Excel.Range

        Dim misValue As Object = System.Reflection.Missing.Value
        Dim rowIndex As Integer = 1

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add()
        xlWorkSheet = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet.Name = "Collection Report Details"

        Dim contentDetails As Object(,) = New Object(,) {}
        Dim grandTotal As Object(,) = New Object(,) {}

        Try
            If Me.BIRRulingCR.BuyerDetails.Count > 0 Then
                Dim bdIndx As Integer = 0
                Dim dicDate As New Dictionary(Of Date, Integer)

                Dim countDetails As Long = 0
                For Each item In Me.BIRRulingCR.BuyerDetails
                    countDetails += item.BRCollReportDetails.Count
                Next

                ReDim contentDetails(countDetails + 1, 13)

                contentDetails(bdIndx, 0) = "Billing" & vbNewLine & "Remarks"
                contentDetails(bdIndx, 1) = "Remittance Date"
                contentDetails(bdIndx, 2) = "Particulars"
                contentDetails(bdIndx, 3) = "Buyer STL ID"
                contentDetails(bdIndx, 4) = "Buyer BILLING ID"
                contentDetails(bdIndx, 5) = "Buyer Transaction No"
                contentDetails(bdIndx, 6) = "Seller STL ID"
                contentDetails(bdIndx, 7) = "Seller BILLING ID"
                contentDetails(bdIndx, 8) = "Seller Full name"
                contentDetails(bdIndx, 9) = "Seller BIR Registered Address"
                contentDetails(bdIndx, 10) = "Seller BIR TIN"
                contentDetails(bdIndx, 11) = "Seller Transaction No"
                contentDetails(bdIndx, 12) = "Vatable Purchases"
                contentDetails(bdIndx, 13) = "Zero Rated Purchases"
                contentDetails(bdIndx, 14) = "Zero Rated Ecozone Purchases"
                contentDetails(bdIndx, 15) = "VAT on Purchases"
                contentDetails(bdIndx, 16) = "Withholding Tax"
                contentDetails(bdIndx, 17) = "Total"


                Dim gTotalVatablePurchases As Decimal = 0
                Dim gTotalZRPurchases As Decimal = 0
                Dim gTotalZREcozonePurchases As Decimal = 0
                Dim gTotalVATonPurchases As Decimal = 0
                Dim gTotalWhTax As Decimal = 0
                Dim gTotalTotal As Decimal = 0

                For Each birCRParticipant In Me.BIRRulingCR.BuyerDetails
                    For Each item In birCRParticipant.BRCollReportDetails.OrderBy(Function(n As BRCollectionReportPurchasesDetails) n.CollectionDate).ThenBy(Function(x As BRCollectionReportPurchasesDetails) x.InvoiceNo).ToList
                        bdIndx += 1
                        gTotalVatablePurchases += item.VatablePurchases
                        gTotalZRPurchases += item.ZeroRatedPurchases
                        gTotalZREcozonePurchases += item.ZeroRatedEcoZonePurchases
                        gTotalVATonPurchases += item.VatOnPurchases
                        gTotalWhTax += item.WithholdingTax
                        gTotalTotal += item.Total

                        contentDetails(bdIndx, 0) = item.BillingPeriod & " - " & item.STLRun
                        contentDetails(bdIndx, 1) = item.CollectionDate.ToShortDateString
                        contentDetails(bdIndx, 2) = item.Particulars
                        contentDetails(bdIndx, 3) = birCRParticipant.Participant.IDNumber

                        contentDetails(bdIndx, 4) = item.SellerParticipant.IDNumber
                        contentDetails(bdIndx, 5) = item.SellerParticipant.FullName
                        contentDetails(bdIndx, 6) = item.InvoiceNo
                        contentDetails(bdIndx, 7) = item.VatablePurchases
                        contentDetails(bdIndx, 8) = item.ZeroRatedPurchases
                        contentDetails(bdIndx, 9) = item.ZeroRatedEcoZonePurchases
                        contentDetails(bdIndx, 10) = item.VatOnPurchases
                        contentDetails(bdIndx, 11) = item.WithholdingTax
                        contentDetails(bdIndx, 12) = item.Total
                        contentDetails(bdIndx, 13) = item.SellerInvoiceNo
                    Next
                Next

                bdIndx += 1
                contentDetails(bdIndx, 0) = ""
                contentDetails(bdIndx, 1) = "Grand Total: "
                contentDetails(bdIndx, 2) = ""
                contentDetails(bdIndx, 3) = ""
                contentDetails(bdIndx, 4) = ""
                contentDetails(bdIndx, 5) = ""
                contentDetails(bdIndx, 6) = ""
                contentDetails(bdIndx, 7) = gTotalVatablePurchases
                contentDetails(bdIndx, 8) = gTotalZRPurchases
                contentDetails(bdIndx, 9) = gTotalZREcozonePurchases
                contentDetails(bdIndx, 10) = gTotalVATonPurchases
                contentDetails(bdIndx, 11) = gTotalWhTax
                contentDetails(bdIndx, 12) = gTotalTotal
                contentDetails(bdIndx, 13) = ""

                xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                rowIndex += bdIndx
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 14), Excel.Range)
                xlContentDetails = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlContentDetails.Value = contentDetails
                Dim transDate As String = dateFrom.ToString("MM-dd-yyyy") & " - " & dateTo.ToString("MM-dd-yyyy") & "_DAILY"

                Dim FileName As String = "CSR_ALL_" & transDate & ".xlsx"

                xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                                    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
                xlWorkBook.Close(False)
                xlApp.Quit()

                releaseObject(xlContentDetails)
                releaseObject(xlRowRange2)
                releaseObject(xlRowRange1)
                releaseObject(xlWorkSheet)
                releaseObject(xlWorkBook)
                releaseObject(xlApp)

            End If
            '********************************************* Supply BIR Ruling Details per Participant


        Catch ex As Exception
            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlApp)
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub GenerateAllInExcelMonthlySeller(ByVal SavingPathName As String, ByVal dateFrom As Date, ByVal dateTo As Date)
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlContentDetails As Excel.Range

        Dim misValue As Object = System.Reflection.Missing.Value
        Dim rowIndex As Integer = 1

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add()
        xlWorkSheet = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet.Name = "Collection Report Details"

        Dim contentDetails As Object(,) = New Object(,) {}
        Dim grandTotal As Object(,) = New Object(,) {}

        Try
            '********************************************* Supply BIR Ruling Details per Participant
            If Me.BIRRulingCR.SellerDetails.Count > 0 Then
                Dim bdIndx As Integer = 0
                Dim dicDate As New Dictionary(Of Date, Integer)
                Dim countDetails As Long = 0
                For Each birCRParticipant In Me.BIRRulingCR.SellerDetails
                    Dim groupByBRCollReportDetails As List(Of BRCollectionReportSalesDetails) = birCRParticipant.BRCollReportDetails.
                                                                                        GroupBy(Function(cr) New With {Key cr.BillingPeriod, Key cr.STLRun, Key cr.Particulars,
                                                                                                Key cr.BuyerParticipant, Key cr.InvoiceNo, Key cr.BuyerInvoiceNo}).
                                                                                        Select(Function(t) New BRCollectionReportSalesDetails With {
                                                                                               .BillingPeriod = t.Key.BillingPeriod,
                                                                                               .STLRun = t.Key.STLRun,
                                                                                               .Particulars = t.Key.Particulars,
                                                                                               .BuyerParticipant = t.Key.BuyerParticipant,
                                                                                               .BuyerInvoiceNo = t.Key.BuyerInvoiceNo,
                                                                                               .InvoiceNo = t.Key.InvoiceNo,
                                                                                               .VatableSales = t.Sum(Function(x) x.VatableSales),
                                                                                               .ZeroRatedSales = t.Sum(Function(x) x.ZeroRatedSales),
                                                                                               .ZeroRatedEcoZoneSales = t.Sum(Function(x) x.ZeroRatedEcoZoneSales),
                                                                                               .VatOnSales = t.Sum(Function(x) x.VatOnSales),
                                                                                               .WithholdingTax = t.Sum(Function(x) x.WithholdingTax),
                                                                                               .WithholdingVat = t.Sum(Function(x) x.WithholdingVat)}).ToList
                    countDetails += groupByBRCollReportDetails.Count
                Next

                ReDim contentDetails(countDetails + 1, 13)

                contentDetails(bdIndx, 0) = "Billing" & vbNewLine & "Remarks"
                contentDetails(bdIndx, 1) = "Particulars"
                contentDetails(bdIndx, 2) = "Seller STL ID"
                contentDetails(bdIndx, 3) = "Received From (Buyer STL ID)"
                contentDetails(bdIndx, 4) = "Received From (Buyer BILLING ID)"
                contentDetails(bdIndx, 5) = "Received From (Buyer Regisered Name)"
                contentDetails(bdIndx, 6) = "Buyer BIR Regisered Address"
                contentDetails(bdIndx, 7) = "Buyer BIR TIN"
                contentDetails(bdIndx, 8) = "Transaction No (Seller)"
                contentDetails(bdIndx, 9) = "Vatable Sales"
                contentDetails(bdIndx, 10) = "Zero Rated Sales"
                contentDetails(bdIndx, 11) = "Zero Rated Ecozone"
                contentDetails(bdIndx, 12) = "VAT on Sales"
                contentDetails(bdIndx, 13) = "Withholding Tax"
                contentDetails(bdIndx, 14) = "Withholding Vat"
                contentDetails(bdIndx, 15) = "Total"
                contentDetails(bdIndx, 16) = "Transaction No (Buyer)"

                Dim gTotalVatableSales As Decimal = 0
                Dim gTotalZRSales As Decimal = 0
                Dim gTotalZREcozone As Decimal = 0
                Dim gTotalVATonSales As Decimal = 0
                Dim gTotalWhTax As Decimal = 0
                Dim gTotalWhVat As Decimal = 0
                Dim gTotalTotal As Decimal = 0

                For Each birCRParticipant In Me.BIRRulingCR.SellerDetails
                    Dim groupByBRCollReportDetails As List(Of BRCollectionReportSalesDetails) = birCRParticipant.BRCollReportDetails.
                                                                                        GroupBy(Function(cr) New With {Key cr.BillingPeriod, Key cr.STLRun, Key cr.Particulars,
                                                                                                Key cr.BuyerParticipant, Key cr.InvoiceNo, Key cr.BuyerInvoiceNo}).
                                                                                        Select(Function(t) New BRCollectionReportSalesDetails With {
                                                                                               .BillingPeriod = t.Key.BillingPeriod,
                                                                                               .STLRun = t.Key.STLRun,
                                                                                               .Particulars = t.Key.Particulars,
                                                                                               .BuyerParticipant = t.Key.BuyerParticipant,
                                                                                               .BuyerInvoiceNo = t.Key.BuyerInvoiceNo,
                                                                                               .InvoiceNo = t.Key.InvoiceNo,
                                                                                               .VatableSales = t.Sum(Function(x) x.VatableSales),
                                                                                               .ZeroRatedSales = t.Sum(Function(x) x.ZeroRatedSales),
                                                                                               .ZeroRatedEcoZoneSales = t.Sum(Function(x) x.ZeroRatedEcoZoneSales),
                                                                                               .VatOnSales = t.Sum(Function(x) x.VatOnSales),
                                                                                               .WithholdingTax = t.Sum(Function(x) x.WithholdingTax),
                                                                                               .WithholdingVat = t.Sum(Function(x) x.WithholdingVat)}).ToList
                    For Each item In groupByBRCollReportDetails.OrderBy(Function(n As BRCollectionReportSalesDetails) n.BuyerParticipant.IDNumber).ThenBy(Function(x As BRCollectionReportSalesDetails) x.InvoiceNo).ToList
                        bdIndx += 1
                        gTotalVatableSales += item.VatableSales
                        gTotalZRSales += item.ZeroRatedSales
                        gTotalZREcozone += item.ZeroRatedEcoZoneSales
                        gTotalVATonSales += item.VatOnSales
                        gTotalWhTax += item.WithholdingTax
                        gTotalWhVat += item.WithholdingVat
                        gTotalTotal += item.Total

                        contentDetails(bdIndx, 0) = item.BillingPeriod & " - " & item.STLRun
                        contentDetails(bdIndx, 1) = item.Particulars
                        contentDetails(bdIndx, 2) = birCRParticipant.SellerParticipantInfo.IDNumber
                        contentDetails(bdIndx, 3) = item.BuyerParticipant.IDNumber
                        contentDetails(bdIndx, 4) = item.BuyerParticipant.FullName
                        contentDetails(bdIndx, 5) = item.InvoiceNo
                        contentDetails(bdIndx, 6) = item.VatableSales
                        contentDetails(bdIndx, 7) = item.ZeroRatedSales
                        contentDetails(bdIndx, 8) = item.ZeroRatedEcoZoneSales
                        contentDetails(bdIndx, 9) = item.VatOnSales
                        contentDetails(bdIndx, 10) = item.WithholdingTax
                        contentDetails(bdIndx, 11) = item.WithholdingVat
                        contentDetails(bdIndx, 12) = item.Total
                        contentDetails(bdIndx, 13) = item.BuyerInvoiceNo
                    Next
                Next

                bdIndx += 1
                contentDetails(bdIndx, 0) = ""
                contentDetails(bdIndx, 1) = "Grand Total: "
                contentDetails(bdIndx, 2) = ""
                contentDetails(bdIndx, 3) = ""
                contentDetails(bdIndx, 4) = ""
                contentDetails(bdIndx, 5) = ""
                contentDetails(bdIndx, 6) = gTotalVatableSales
                contentDetails(bdIndx, 7) = gTotalZRSales
                contentDetails(bdIndx, 8) = gTotalZREcozone
                contentDetails(bdIndx, 9) = gTotalVATonSales
                contentDetails(bdIndx, 10) = gTotalWhTax
                contentDetails(bdIndx, 11) = gTotalWhVat
                contentDetails(bdIndx, 12) = gTotalTotal
                contentDetails(bdIndx, 13) = ""

                xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                rowIndex += bdIndx
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 14), Excel.Range)
                xlContentDetails = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlContentDetails.Value = contentDetails
                Dim transDate As String = dateFrom.ToString("MM-dd-yyyy") & " - " & dateTo.ToString("MM-dd-yyyy") & "_MONTHLY"

                Dim FileName As String = "CSR_ALL_" & transDate & ".xlsx"

                xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                                    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
                xlWorkBook.Close(False)
                xlApp.Quit()

                releaseObject(xlContentDetails)
                releaseObject(xlRowRange2)
                releaseObject(xlRowRange1)
                releaseObject(xlWorkSheet)
                releaseObject(xlWorkBook)
                releaseObject(xlApp)
            End If
        Catch ex As Exception
            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlApp)
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub GenerateAllInExcelMonthlyBuyer(ByVal SavingPathName As String, ByVal dateFrom As Date, ByVal dateTo As Date)
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlContentDetails As Excel.Range

        Dim misValue As Object = System.Reflection.Missing.Value
        Dim rowIndex As Integer = 1

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add()
        xlWorkSheet = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet.Name = "Collection Report Details"

        Dim contentDetails As Object(,) = New Object(,) {}
        Dim grandTotal As Object(,) = New Object(,) {}

        Try
            '********************************************* Supply BIR Ruling Details per Participant
            If Me.BIRRulingCR.BuyerDetails.Count > 0 Then
                Dim bdIndx As Integer = 0
                Dim dicDate As New Dictionary(Of Date, Integer)
                Dim countDetails As Long = 0
                For Each birCRParticipant In Me.BIRRulingCR.BuyerDetails
                    Dim groupByBRCollReportDetails As List(Of BRCollectionReportPurchasesDetails) = birCRParticipant.BRCollReportDetails.
                                                                                        GroupBy(Function(cr) New With {Key cr.BillingPeriod, Key cr.STLRun, Key cr.Particulars,
                                                                                                Key cr.SellerParticipant, Key cr.InvoiceNo, Key cr.SellerInvoiceNo}).
                                                                                        Select(Function(t) New BRCollectionReportPurchasesDetails With {
                                                                                               .BillingPeriod = t.Key.BillingPeriod,
                                                                                               .STLRun = t.Key.STLRun,
                                                                                               .Particulars = t.Key.Particulars,
                                                                                               .SellerParticipant = t.Key.SellerParticipant,
                                                                                               .SellerInvoiceNo = t.Key.SellerInvoiceNo,
                                                                                               .InvoiceNo = t.Key.InvoiceNo,
                                                                                               .VatablePurchases = t.Sum(Function(x) x.VatablePurchases),
                                                                                               .ZeroRatedPurchases = t.Sum(Function(x) x.ZeroRatedPurchases),
                                                                                               .ZeroRatedEcoZonePurchases = t.Sum(Function(x) x.ZeroRatedEcoZonePurchases),
                                                                                               .VatOnPurchases = t.Sum(Function(x) x.VatOnPurchases),
                                                                                               .WithholdingTax = t.Sum(Function(x) x.WithholdingTax)}).ToList
                    countDetails += groupByBRCollReportDetails.Count
                Next

                ReDim contentDetails(countDetails + 1, 16)

                contentDetails(bdIndx, 0) = "Billing" & vbNewLine & "Remarks"
                contentDetails(bdIndx, 1) = "Particulars"
                contentDetails(bdIndx, 2) = "Buyer STL ID"
                contentDetails(bdIndx, 3) = "Paid To (Seller STL ID)"
                contentDetails(bdIndx, 4) = "Paid To (Seller BILLING ID)"
                contentDetails(bdIndx, 5) = "Paid To (Seller Registered Name)"
                contentDetails(bdIndx, 6) = "Seller BIR Registered Address"
                contentDetails(bdIndx, 7) = "Seller BIR TIN"
                contentDetails(bdIndx, 8) = "Transaction No (Buyer)"
                contentDetails(bdIndx, 9) = "Vatable Purchases"
                contentDetails(bdIndx, 10) = "Zero Rated Purchases"
                contentDetails(bdIndx, 11) = "Zero Rated Ecozone Purchases"
                contentDetails(bdIndx, 12) = "VAT on Purchases"
                contentDetails(bdIndx, 13) = "Withholding Tax"
                contentDetails(bdIndx, 14) = "Withholding Vat"
                contentDetails(bdIndx, 15) = "Total"
                contentDetails(bdIndx, 16) = "Transaction No (Seller)"

                Dim gTotalVatablePurchases As Decimal = 0
                Dim gTotalZRPurchases As Decimal = 0
                Dim gTotalZREcozonePurchases As Decimal = 0
                Dim gTotalVATonPurchases As Decimal = 0
                Dim gTotalWhTax As Decimal = 0
                Dim gTotalTotal As Decimal = 0

                For Each birCRParticipant In Me.BIRRulingCR.BuyerDetails
                    Dim groupByBRCollReportDetails As List(Of BRCollectionReportPurchasesDetails) = birCRParticipant.BRCollReportDetails.
                                                                                        GroupBy(Function(cr) New With {Key cr.BillingPeriod, Key cr.STLRun, Key cr.Particulars,
                                                                                                Key cr.SellerParticipant, Key cr.InvoiceNo, Key cr.SellerInvoiceNo}).
                                                                                        Select(Function(t) New BRCollectionReportPurchasesDetails With {
                                                                                               .BillingPeriod = t.Key.BillingPeriod,
                                                                                               .STLRun = t.Key.STLRun,
                                                                                               .Particulars = t.Key.Particulars,
                                                                                               .SellerParticipant = t.Key.SellerParticipant,
                                                                                               .SellerInvoiceNo = t.Key.SellerInvoiceNo,
                                                                                               .InvoiceNo = t.Key.InvoiceNo,
                                                                                               .VatablePurchases = t.Sum(Function(x) x.VatablePurchases),
                                                                                               .ZeroRatedPurchases = t.Sum(Function(x) x.ZeroRatedPurchases),
                                                                                               .ZeroRatedEcoZonePurchases = t.Sum(Function(x) x.ZeroRatedEcoZonePurchases),
                                                                                               .VatOnPurchases = t.Sum(Function(x) x.VatOnPurchases),
                                                                                               .WithholdingTax = t.Sum(Function(x) x.WithholdingTax)}).ToList
                    For Each item In groupByBRCollReportDetails.OrderBy(Function(n As BRCollectionReportPurchasesDetails) n.SellerParticipant.IDNumber).ThenBy(Function(x As BRCollectionReportPurchasesDetails) x.InvoiceNo).ToList
                        bdIndx += 1
                        gTotalVatablePurchases += item.VatablePurchases
                        gTotalZRPurchases += item.ZeroRatedPurchases
                        gTotalZREcozonePurchases += item.ZeroRatedEcoZonePurchases
                        gTotalVATonPurchases += item.VatOnPurchases
                        gTotalWhTax += item.WithholdingTax
                        gTotalTotal += item.Total

                        contentDetails(bdIndx, 0) = item.BillingPeriod & " - " & item.STLRun
                        contentDetails(bdIndx, 1) = item.Particulars
                        contentDetails(bdIndx, 2) = birCRParticipant.Participant.IDNumber
                        contentDetails(bdIndx, 3) = item.SellerParticipant.IDNumber
                        contentDetails(bdIndx, 4) = item.SellerBillingID
                        contentDetails(bdIndx, 5) = item.SellerParticipant.FullName
                        contentDetails(bdIndx, 6) = item.SellerParticipant.ParticipantAddress
                        contentDetails(bdIndx, 7) = item.SellerParticipant.TIN
                        contentDetails(bdIndx, 8) = item.InvoiceNo
                        contentDetails(bdIndx, 9) = item.VatablePurchases
                        contentDetails(bdIndx, 10) = item.ZeroRatedPurchases
                        contentDetails(bdIndx, 11) = item.ZeroRatedEcoZonePurchases
                        contentDetails(bdIndx, 12) = item.VatOnPurchases
                        contentDetails(bdIndx, 13) = item.WithholdingTax
                        contentDetails(bdIndx, 14) = item.Total
                        contentDetails(bdIndx, 15) = item.SellerInvoiceNo
                    Next
                Next

                bdIndx += 1
                contentDetails(bdIndx, 0) = ""
                contentDetails(bdIndx, 1) = "Grand Total: "
                contentDetails(bdIndx, 2) = ""
                contentDetails(bdIndx, 3) = ""
                contentDetails(bdIndx, 4) = ""
                contentDetails(bdIndx, 5) = ""
                contentDetails(bdIndx, 6) = ""
                contentDetails(bdIndx, 7) = ""
                contentDetails(bdIndx, 8) = ""
                contentDetails(bdIndx, 9) = gTotalVatablePurchases
                contentDetails(bdIndx, 10) = gTotalZRPurchases
                contentDetails(bdIndx, 11) = gTotalZREcozonePurchases
                contentDetails(bdIndx, 12) = gTotalVATonPurchases
                contentDetails(bdIndx, 13) = gTotalWhTax
                contentDetails(bdIndx, 14) = gTotalTotal
                contentDetails(bdIndx, 15) = ""

                xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                rowIndex += bdIndx
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 16), Excel.Range)
                xlContentDetails = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlContentDetails.Value = contentDetails
                Dim transDate As String = dateFrom.ToString("MM-dd-yyyy") & " - " & dateTo.ToString("MM-dd-yyyy") & "_MONTHLY"

                Dim FileName As String = "CSR_ALL_" & transDate & ".xlsx"

                xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                                    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
                xlWorkBook.Close(False)
                xlApp.Quit()

                releaseObject(xlContentDetails)
                releaseObject(xlRowRange2)
                releaseObject(xlRowRange1)
                releaseObject(xlWorkSheet)
                releaseObject(xlWorkBook)
                releaseObject(xlApp)
            End If
        Catch ex As Exception
            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlApp)
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub GenerateInExcelDailySeller(ByVal SavingPathName As String, ByVal partifipantInfo As AMParticipants, ByVal dateFrom As Date,
                                     ByVal dateTo As Date, ByVal birCRParticipant As BRCollectionReportSellerParticipant)
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlContentHeader As Excel.Range
        Dim xlContentDetails As Excel.Range

        Dim misValue As Object = System.Reflection.Missing.Value
        Dim rowIndex As Integer = 5

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add()
        xlWorkSheet = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet.Name = "Collection Report Details"

        Dim contentHeader As Object(,) = New Object(,) {}
        Dim contentDetails As Object(,) = New Object(,) {}
        Dim grandTotal As Object(,) = New Object(,) {}

        Try
            '********************************************* Supply Content Header
            ReDim contentHeader(3, 0)
            contentHeader(0, 0) = "MP Name: " & partifipantInfo.FullName.ToString()
            contentHeader(1, 0) = "MP ID No.: " & partifipantInfo.IDNumber.ToString()
            If dateFrom = dateTo Then
                contentHeader(2, 0) = "As of " & dateFrom.ToString("MMMM dd, yyyy")
            Else
                contentHeader(2, 0) = "As of " & dateFrom.ToString("MMMM dd, yyyy") & " to " & dateTo.ToString("MMMM dd, yyyy")
            End If


            xlRowRange1 = DirectCast(xlWorkSheet.Cells(2, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet.Cells(4, 1), Excel.Range)
            xlContentHeader = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
            xlContentHeader.Value = contentHeader

            '********************************************* Supply BIR Ruling Details per Participant
            If birCRParticipant.BRCollReportDetails.Count > 0 Then
                Dim bdIndx As Integer = 0
                Dim dicDate As New Dictionary(Of Date, Integer)

                Dim countCollDate As Long = (From x In birCRParticipant.BRCollReportDetails Select x.CollectionDate Distinct).Count
                Dim countDetails As Long = (From x In birCRParticipant.BRCollReportDetails Select x).Count

                ReDim contentDetails(countCollDate + countDetails + 1, 15)

                contentDetails(bdIndx, 0) = "Billing" & vbNewLine & "Remarks"
                contentDetails(bdIndx, 1) = "Remittance Date"
                contentDetails(bdIndx, 2) = "Particulars"
                contentDetails(bdIndx, 3) = "Received From (Buyer STL ID)"
                contentDetails(bdIndx, 4) = "Received From (Buyer BILLING ID)"
                contentDetails(bdIndx, 5) = "Received From (Buyer Registered Name)"
                contentDetails(bdIndx, 6) = "Buyer BIR Registered Address"
                contentDetails(bdIndx, 7) = "Buyer BIR Tin Number"
                contentDetails(bdIndx, 8) = "Transaction No (Seller)"
                contentDetails(bdIndx, 9) = "Vatable Sales"
                contentDetails(bdIndx, 10) = "Zero Rated Sales"
                contentDetails(bdIndx, 11) = "Zero Rated Ecozone"
                contentDetails(bdIndx, 12) = "VAT on Sales"
                contentDetails(bdIndx, 13) = "Withholding Tax"
                contentDetails(bdIndx, 14) = "Total"

                Dim gTotalVatableSales As Decimal = 0
                Dim gTotalZRSales As Decimal = 0
                Dim gTotalZREcozone As Decimal = 0
                Dim gTotalVATonSales As Decimal = 0
                Dim gTotalWhTax As Decimal = 0
                Dim gTotalTotal As Decimal = 0

                For Each item In birCRParticipant.BRCollReportDetails.OrderBy(Function(n As BRCollectionReportSalesDetails) n.CollectionDate).ThenBy(Function(x As BRCollectionReportSalesDetails) x.InvoiceNo).ToList
                    bdIndx += 1

                    gTotalVatableSales += item.VatableSales
                    gTotalZRSales += item.ZeroRatedSales
                    gTotalZREcozone += item.ZeroRatedEcoZoneSales
                    gTotalVATonSales += item.VatOnSales
                    gTotalWhTax += item.WithholdingTax
                    gTotalTotal += item.Total

                    contentDetails(bdIndx, 0) = item.BillingPeriod & " - " & item.STLRun
                    contentDetails(bdIndx, 1) = item.CollectionDate.ToShortDateString
                    contentDetails(bdIndx, 2) = item.Particulars
                    contentDetails(bdIndx, 3) = item.BuyerParticipant.IDNumber
                    contentDetails(bdIndx, 4) = item.BuyerBillingID
                    contentDetails(bdIndx, 5) = item.BuyerParticipant.FullName
                    contentDetails(bdIndx, 6) = item.BuyerParticipant.ParticipantAddress
                    contentDetails(bdIndx, 7) = item.BuyerParticipant.TIN
                    contentDetails(bdIndx, 8) = item.InvoiceNo
                    contentDetails(bdIndx, 9) = item.VatableSales
                    contentDetails(bdIndx, 10) = item.ZeroRatedSales
                    contentDetails(bdIndx, 11) = item.ZeroRatedEcoZoneSales
                    contentDetails(bdIndx, 12) = item.VatOnSales
                    contentDetails(bdIndx, 13) = item.WithholdingTax
                    contentDetails(bdIndx, 14) = item.Total
                Next

                bdIndx += 1
                contentDetails(bdIndx, 0) = ""
                contentDetails(bdIndx, 1) = "Grand Total: "
                contentDetails(bdIndx, 2) = ""
                contentDetails(bdIndx, 3) = ""
                contentDetails(bdIndx, 4) = ""
                contentDetails(bdIndx, 5) = ""
                contentDetails(bdIndx, 6) = ""
                contentDetails(bdIndx, 7) = ""
                contentDetails(bdIndx, 8) = ""
                contentDetails(bdIndx, 9) = gTotalVatableSales
                contentDetails(bdIndx, 10) = gTotalZRSales
                contentDetails(bdIndx, 11) = gTotalZREcozone
                contentDetails(bdIndx, 12) = gTotalVATonSales
                contentDetails(bdIndx, 13) = gTotalWhTax
                contentDetails(bdIndx, 14) = gTotalTotal


                xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                rowIndex += bdIndx
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 15), Excel.Range)
                xlContentDetails = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlContentDetails.Value = contentDetails
                Dim transDate As String = ""

                transDate = dateTo.ToString("yyyyMMdd")

                Dim FileName As String = "CSR_IEMOP_" & partifipantInfo.IDNumber.Replace("_FIT", "").ToString & "_" & "CR-N" & birCRParticipant.BRCollReportNumber.ToString("D7") & "_" & transDate & ".xlsx"

                xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                            Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
                xlWorkBook.Close(False)
                xlApp.Quit()

                releaseObject(xlContentDetails)
                releaseObject(xlContentHeader)
                releaseObject(xlRowRange2)
                releaseObject(xlRowRange1)
                releaseObject(xlWorkSheet)
                releaseObject(xlWorkBook)
                releaseObject(xlApp)

            End If

        Catch ex As Exception
            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlApp)
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub GenerateInExcelDailyBuyer(ByVal SavingPathName As String, ByVal partifipantInfo As AMParticipants, ByVal dateFrom As Date,
                                     ByVal dateTo As Date, ByVal birCRParticipant As BRCollectionReportBuyerParticipant)
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlContentHeader As Excel.Range
        Dim xlContentDetails As Excel.Range

        Dim misValue As Object = System.Reflection.Missing.Value
        Dim rowIndex As Integer = 5

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add()
        xlWorkSheet = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet.Name = "Collection Report Details"

        Dim contentHeader As Object(,) = New Object(,) {}
        Dim contentDetails As Object(,) = New Object(,) {}
        Dim grandTotal As Object(,) = New Object(,) {}

        Try
            '********************************************* Supply Content Header
            ReDim contentHeader(3, 0)
            contentHeader(0, 0) = "MP Name: " & partifipantInfo.FullName.ToString()
            contentHeader(1, 0) = "MP ID No.: " & partifipantInfo.IDNumber.ToString()
            If dateFrom = dateTo Then
                contentHeader(2, 0) = "As of " & dateFrom.ToString("MMMM dd, yyyy")
            Else
                contentHeader(2, 0) = "As of " & dateFrom.ToString("MMMM dd, yyyy") & " to " & dateTo.ToString("MMMM dd, yyyy")
            End If


            xlRowRange1 = DirectCast(xlWorkSheet.Cells(2, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet.Cells(4, 1), Excel.Range)
            xlContentHeader = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
            xlContentHeader.Value = contentHeader

            '********************************************* Supply BIR Ruling Details per Participant
            If birCRParticipant.BRCollReportDetails.Count > 0 Then
                Dim bdIndx As Integer = 0
                Dim dicDate As New Dictionary(Of Date, Integer)

                Dim countCollDate As Long = (From x In birCRParticipant.BRCollReportDetails Select x.CollectionDate Distinct).Count
                Dim countDetails As Long = (From x In birCRParticipant.BRCollReportDetails Select x).Count

                ReDim contentDetails(countCollDate + countDetails + 1, 14)

                contentDetails(bdIndx, 0) = "Billing" & vbNewLine & "Remarks"
                contentDetails(bdIndx, 1) = "Remittance Date"
                contentDetails(bdIndx, 2) = "Particulars"
                contentDetails(bdIndx, 3) = "Paid To (Seller STL ID)"
                contentDetails(bdIndx, 4) = "Paid To (Seller BILLING ID)"
                contentDetails(bdIndx, 5) = "Paid To (Seller REGISTRED Name)"
                contentDetails(bdIndx, 6) = "Seller BIR REGISTRED Address)"
                contentDetails(bdIndx, 7) = "Seller BIR TIN Number)"
                contentDetails(bdIndx, 8) = "Transaction No (Buyer)"
                contentDetails(bdIndx, 9) = "Vatable Purchases"
                contentDetails(bdIndx, 10) = "Zero Rated Purchases"
                contentDetails(bdIndx, 11) = "Zero Rated Ecozone Purchases"
                contentDetails(bdIndx, 12) = "VAT on Purchases"
                contentDetails(bdIndx, 13) = "Withholding Tax"
                contentDetails(bdIndx, 14) = "Total"

                Dim gTotalVatablePurchases As Decimal = 0
                Dim gTotalZRPurchases As Decimal = 0
                Dim gTotalZREcozonePurchases As Decimal = 0
                Dim gTotalVATonPurchases As Decimal = 0
                Dim gTotalWhTax As Decimal = 0
                Dim gTotalTotal As Decimal = 0

                For Each item In birCRParticipant.BRCollReportDetails.OrderBy(Function(n As BRCollectionReportPurchasesDetails) n.CollectionDate).ThenBy(Function(x As BRCollectionReportPurchasesDetails) x.InvoiceNo).ToList
                    bdIndx += 1

                    gTotalVatablePurchases += item.VatablePurchases
                    gTotalZRPurchases += item.ZeroRatedPurchases
                    gTotalZREcozonePurchases += item.ZeroRatedEcoZonePurchases
                    gTotalVATonPurchases += item.VatOnPurchases
                    gTotalWhTax += item.WithholdingTax
                    gTotalTotal += item.Total

                    contentDetails(bdIndx, 0) = item.BillingPeriod & " - " & item.STLRun
                    contentDetails(bdIndx, 1) = item.CollectionDate.ToShortDateString
                    contentDetails(bdIndx, 2) = item.Particulars
                    contentDetails(bdIndx, 3) = item.SellerParticipant.IDNumber
                    contentDetails(bdIndx, 4) = item.SellerBillingID
                    contentDetails(bdIndx, 5) = item.SellerParticipant.FullName
                    contentDetails(bdIndx, 6) = item.SellerParticipant.ParticipantAddress
                    contentDetails(bdIndx, 7) = item.SellerParticipant.TIN
                    contentDetails(bdIndx, 8) = item.InvoiceNo
                    contentDetails(bdIndx, 9) = item.VatablePurchases
                    contentDetails(bdIndx, 10) = item.ZeroRatedPurchases
                    contentDetails(bdIndx, 11) = item.ZeroRatedEcoZonePurchases
                    contentDetails(bdIndx, 12) = item.VatOnPurchases
                    contentDetails(bdIndx, 13) = item.WithholdingTax
                    contentDetails(bdIndx, 14) = item.Total
                Next

                bdIndx += 1
                contentDetails(bdIndx, 0) = ""
                contentDetails(bdIndx, 1) = "Grand Total: "
                contentDetails(bdIndx, 2) = ""
                contentDetails(bdIndx, 3) = ""
                contentDetails(bdIndx, 4) = ""
                contentDetails(bdIndx, 5) = ""
                contentDetails(bdIndx, 6) = ""
                contentDetails(bdIndx, 7) = ""
                contentDetails(bdIndx, 8) = ""
                contentDetails(bdIndx, 9) = gTotalVatablePurchases
                contentDetails(bdIndx, 10) = gTotalZRPurchases
                contentDetails(bdIndx, 11) = gTotalZREcozonePurchases
                contentDetails(bdIndx, 12) = gTotalVATonPurchases
                contentDetails(bdIndx, 13) = gTotalWhTax
                contentDetails(bdIndx, 14) = gTotalTotal


                xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                rowIndex += bdIndx
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 15), Excel.Range)
                xlContentDetails = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlContentDetails.Value = contentDetails
                Dim transDate As String = ""

                transDate = dateTo.ToString("yyyyMMdd")

                Dim FileName As String = "CSR_IEMOP_" & partifipantInfo.IDNumber.Replace("_FIT", "").ToString & "_" & "CR-N" & Me.BIRRulingCR.BIRRNo.ToString("D4") & "-" & birCRParticipant.BRCollReportNumber.ToString("D4") & "_" & transDate & ".xlsx"

                xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                            Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
                xlWorkBook.Close(False)
                xlApp.Quit()

                releaseObject(xlContentDetails)
                releaseObject(xlContentHeader)
                releaseObject(xlRowRange2)
                releaseObject(xlRowRange1)
                releaseObject(xlWorkSheet)
                releaseObject(xlWorkBook)
                releaseObject(xlApp)
            End If
        Catch ex As Exception
            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlApp)
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub GenerateInExcelMonthlySeller(ByVal SavingPathName As String, ByVal partifipantInfo As AMParticipants, ByVal dateFrom As Date,
                                       ByVal dateTo As Date, ByVal birCRParticipant As BRCollectionReportSellerParticipant)
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlContentHeader As Excel.Range
        Dim xlContentDetails As Excel.Range

        Dim misValue As Object = System.Reflection.Missing.Value
        Dim rowIndex As Integer = 5

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add()
        xlWorkSheet = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet.Name = "Collection Report Details"

        Dim contentHeader As Object(,) = New Object(,) {}
        Dim contentDetails As Object(,) = New Object(,) {}
        Dim grandTotal As Object(,) = New Object(,) {}

        Try
            '********************************************* Supply Content Header
            ReDim contentHeader(3, 0)
            contentHeader(0, 0) = "MP Name: " & partifipantInfo.FullName.ToString()
            contentHeader(1, 0) = "MP ID No.: " & partifipantInfo.IDNumber.ToString()
            If dateFrom = dateTo Then
                contentHeader(2, 0) = "As of " & dateFrom.ToString("MMMM dd, yyyy")
            Else
                contentHeader(2, 0) = "As of " & dateFrom.ToString("MMMM dd, yyyy") & " to " & dateTo.ToString("MMMM dd, yyyy")
            End If

            xlRowRange1 = DirectCast(xlWorkSheet.Cells(2, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet.Cells(4, 1), Excel.Range)
            xlContentHeader = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
            xlContentHeader.Value = contentHeader

            '********************************************* Supply BIR Ruling Details per Participant
            If birCRParticipant.BRCollReportDetails.Count > 0 Then
                Dim bdIndx As Integer = 0
                Dim dicDate As New Dictionary(Of Date, Integer)
                Dim groupByBRCollReportDetails As List(Of BRCollectionReportSalesDetails) = birCRParticipant.BRCollReportDetails.
                                                                                    GroupBy(Function(cr) New With {Key cr.BillingPeriod, Key cr.STLRun, Key cr.Particulars,
                                                                                            Key cr.BuyerParticipant, Key cr.InvoiceNo, Key cr.BuyerInvoiceNo}).
                                                                                    Select(Function(t) New BRCollectionReportSalesDetails With {
                                                                                           .BillingPeriod = t.Key.BillingPeriod,
                                                                                           .STLRun = t.Key.STLRun,
                                                                                           .Particulars = t.Key.Particulars,
                                                                                           .BuyerParticipant = t.Key.BuyerParticipant,
                                                                                           .BuyerInvoiceNo = t.Key.BuyerInvoiceNo,
                                                                                           .InvoiceNo = t.Key.InvoiceNo,
                                                                                           .VatableSales = t.Sum(Function(x) x.VatableSales),
                                                                                           .ZeroRatedSales = t.Sum(Function(x) x.ZeroRatedSales),
                                                                                           .ZeroRatedEcoZoneSales = t.Sum(Function(x) x.ZeroRatedEcoZoneSales),
                                                                                           .VatOnSales = t.Sum(Function(x) x.VatOnSales),
                                                                                           .WithholdingTax = t.Sum(Function(x) x.WithholdingTax)}).ToList

                Dim countDetails As Long = (From x In groupByBRCollReportDetails Select x).Count

                ReDim contentDetails(countDetails + 1, 13)

                contentDetails(bdIndx, 0) = "Billing" & vbNewLine & "Remarks"
                contentDetails(bdIndx, 1) = "Particulars"
                contentDetails(bdIndx, 2) = "Received From (Buyer STL ID)"
                contentDetails(bdIndx, 3) = "Received From (Buyer BILLING ID)"
                contentDetails(bdIndx, 4) = "Received From (Buyer Registered Name)"
                contentDetails(bdIndx, 5) = "Buyer BIR Registered Address"
                contentDetails(bdIndx, 6) = "Buyer BIR Tin Number"
                contentDetails(bdIndx, 7) = "Transaction No (Seller)"
                contentDetails(bdIndx, 8) = "Vatable Sales"
                contentDetails(bdIndx, 9) = "Zero Rated Sales"
                contentDetails(bdIndx, 10) = "Zero Rated Ecozone"
                contentDetails(bdIndx, 11) = "VAT on Sales"
                contentDetails(bdIndx, 12) = "Withholding Tax"
                contentDetails(bdIndx, 13) = "Total"

                Dim gTotalVatableSales As Decimal = 0
                Dim gTotalZRSales As Decimal = 0
                Dim gTotalZREcozone As Decimal = 0
                Dim gTotalVATonSales As Decimal = 0
                Dim gTotalWhTax As Decimal = 0
                Dim gTotalTotal As Decimal = 0

                For Each item In groupByBRCollReportDetails.OrderBy(Function(n As BRCollectionReportSalesDetails) n.BuyerParticipant.IDNumber).
                                                            ThenBy(Function(x As BRCollectionReportSalesDetails) x.InvoiceNo).ToList
                    bdIndx += 1

                    gTotalVatableSales += item.VatableSales
                    gTotalZRSales += item.ZeroRatedSales
                    gTotalZREcozone += item.ZeroRatedEcoZoneSales
                    gTotalVATonSales += item.VatOnSales
                    gTotalWhTax += item.WithholdingTax
                    gTotalTotal += item.Total

                    contentDetails(bdIndx, 0) = item.BillingPeriod & " - " & item.STLRun
                    contentDetails(bdIndx, 1) = item.Particulars
                    contentDetails(bdIndx, 2) = item.BuyerParticipant.IDNumber
                    contentDetails(bdIndx, 3) = item.BuyerBillingID
                    contentDetails(bdIndx, 4) = item.BuyerParticipant.FullName
                    contentDetails(bdIndx, 5) = item.BuyerParticipant.ParticipantAddress
                    contentDetails(bdIndx, 6) = item.BuyerParticipant.TIN
                    contentDetails(bdIndx, 7) = item.InvoiceNo
                    contentDetails(bdIndx, 8) = item.VatableSales
                    contentDetails(bdIndx, 9) = item.ZeroRatedSales
                    contentDetails(bdIndx, 10) = item.ZeroRatedEcoZoneSales
                    contentDetails(bdIndx, 11) = item.VatOnSales
                    contentDetails(bdIndx, 12) = item.WithholdingTax
                    contentDetails(bdIndx, 13) = item.Total
                Next

                bdIndx += 1
                contentDetails(bdIndx, 0) = ""
                contentDetails(bdIndx, 1) = "Grand Total: "
                contentDetails(bdIndx, 2) = ""
                contentDetails(bdIndx, 3) = ""
                contentDetails(bdIndx, 4) = ""
                contentDetails(bdIndx, 5) = ""
                contentDetails(bdIndx, 6) = ""
                contentDetails(bdIndx, 7) = ""
                contentDetails(bdIndx, 8) = gTotalVatableSales
                contentDetails(bdIndx, 9) = gTotalZRSales
                contentDetails(bdIndx, 10) = gTotalZREcozone
                contentDetails(bdIndx, 11) = gTotalVATonSales
                contentDetails(bdIndx, 12) = gTotalWhTax
                contentDetails(bdIndx, 13) = gTotalTotal

                xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                rowIndex += bdIndx
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 14), Excel.Range)
                xlContentDetails = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlContentDetails.Value = contentDetails
                Dim transDate As String = ""

                If dateFrom = dateTo Then
                    transDate = dateFrom.ToString("yyyyMMdd")
                Else
                    transDate = dateFrom.ToString("yyyyMMdd") & " - " & dateTo.ToString("yyyyMMdd")
                End If

                Dim FileName As String = "CSR_IEMOP_" & partifipantInfo.IDNumber.Replace("_FIT", "").ToString & "_" & "CR-N" & birCRParticipant.BRCollReportNumber.ToString("D7") & "_" & transDate & ".xlsx"
                xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                            Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
                xlWorkBook.Close(False)
                xlApp.Quit()

                releaseObject(xlContentDetails)
                releaseObject(xlContentHeader)
                releaseObject(xlRowRange2)
                releaseObject(xlRowRange1)
                releaseObject(xlWorkSheet)
                releaseObject(xlWorkBook)
                releaseObject(xlApp)

            End If

        Catch ex As Exception
            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlApp)
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub GenerateInExcelMonthlyBuyer(ByVal SavingPathName As String, ByVal partifipantInfo As AMParticipants, ByVal dateFrom As Date,
                                       ByVal dateTo As Date, ByVal birCRParticipant As BRCollectionReportBuyerParticipant)
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlContentHeader As Excel.Range
        Dim xlContentDetails As Excel.Range

        Dim misValue As Object = System.Reflection.Missing.Value
        Dim rowIndex As Integer = 5

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add()
        xlWorkSheet = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet.Name = "Collection Report Details"

        Dim contentHeader As Object(,) = New Object(,) {}
        Dim contentDetails As Object(,) = New Object(,) {}
        Dim grandTotal As Object(,) = New Object(,) {}

        Try
            '********************************************* Supply Content Header
            ReDim contentHeader(3, 0)
            contentHeader(0, 0) = "MP Name: " & partifipantInfo.FullName.ToString()
            contentHeader(1, 0) = "MP ID No.: " & partifipantInfo.IDNumber.ToString()
            If dateFrom = dateTo Then
                contentHeader(2, 0) = "As of " & dateFrom.ToString("MMMM dd, yyyy")
            Else
                contentHeader(2, 0) = "As of " & dateFrom.ToString("MMMM dd, yyyy") & " to " & dateTo.ToString("MMMM dd, yyyy")
            End If

            xlRowRange1 = DirectCast(xlWorkSheet.Cells(2, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet.Cells(4, 1), Excel.Range)
            xlContentHeader = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
            xlContentHeader.Value = contentHeader

            '********************************************* Supply BIR Ruling Details per Participant
            If birCRParticipant.BRCollReportDetails.Count > 0 Then
                Dim bdIndx As Integer = 0
                Dim dicDate As New Dictionary(Of Date, Integer)
                Dim groupByBRCollReportDetails As List(Of BRCollectionReportPurchasesDetails) = birCRParticipant.BRCollReportDetails.
                                                                                    GroupBy(Function(cr) New With {Key cr.BillingPeriod, Key cr.STLRun, Key cr.Particulars,
                                                                                            Key cr.SellerParticipant, Key cr.InvoiceNo, Key cr.SellerInvoiceNo}).
                                                                                    Select(Function(t) New BRCollectionReportPurchasesDetails With {
                                                                                           .BillingPeriod = t.Key.BillingPeriod,
                                                                                           .STLRun = t.Key.STLRun,
                                                                                           .Particulars = t.Key.Particulars,
                                                                                           .SellerParticipant = t.Key.SellerParticipant,
                                                                                           .SellerInvoiceNo = t.Key.SellerInvoiceNo,
                                                                                           .InvoiceNo = t.Key.InvoiceNo,
                                                                                           .VatablePurchases = t.Sum(Function(x) x.VatablePurchases),
                                                                                           .ZeroRatedPurchases = t.Sum(Function(x) x.ZeroRatedPurchases),
                                                                                           .ZeroRatedEcoZonePurchases = t.Sum(Function(x) x.ZeroRatedEcoZonePurchases),
                                                                                           .VatOnPurchases = t.Sum(Function(x) x.VatOnPurchases),
                                                                                           .WithholdingTax = t.Sum(Function(x) x.WithholdingTax)}).ToList

                Dim countDetails As Long = (From x In groupByBRCollReportDetails Select x).Count

                ReDim contentDetails(countDetails + 1, 13)

                contentDetails(bdIndx, 0) = "Billing" & vbNewLine & "Remarks"
                contentDetails(bdIndx, 1) = "Particulars"
                contentDetails(bdIndx, 2) = "Paid To (Seller STL ID)"
                contentDetails(bdIndx, 3) = "Paid To (Seller BILLING ID)"
                contentDetails(bdIndx, 4) = "Paid To (Seller Registered name)"
                contentDetails(bdIndx, 5) = "Seller BIR Registered Address"
                contentDetails(bdIndx, 6) = "Seller BIR Tin Number"
                contentDetails(bdIndx, 7) = "Transaction No (Seller)"
                contentDetails(bdIndx, 8) = "Vatable Sales"
                contentDetails(bdIndx, 9) = "Zero Rated Sales"
                contentDetails(bdIndx, 10) = "Zero Rated Ecozone"
                contentDetails(bdIndx, 11) = "VAT on Sales"
                contentDetails(bdIndx, 12) = "Withholding Tax"
                contentDetails(bdIndx, 13) = "Total"


                Dim gTotalVatablePurchases As Decimal = 0
                Dim gTotalZRPurchases As Decimal = 0
                Dim gTotalZREcozonePurchases As Decimal = 0
                Dim gTotalVATonPurchases As Decimal = 0
                Dim gTotalWhTax As Decimal = 0
                Dim gTotalTotal As Decimal = 0

                For Each item In groupByBRCollReportDetails.OrderBy(Function(n As BRCollectionReportPurchasesDetails) n.SellerParticipant.IDNumber).
                                                            ThenBy(Function(x As BRCollectionReportPurchasesDetails) x.InvoiceNo).ToList
                    bdIndx += 1

                    gTotalVatablePurchases += item.VatablePurchases
                    gTotalZRPurchases += item.ZeroRatedPurchases
                    gTotalZREcozonePurchases += item.ZeroRatedEcoZonePurchases
                    gTotalVATonPurchases += item.VatOnPurchases
                    gTotalWhTax += item.WithholdingTax
                    gTotalTotal += item.Total

                    contentDetails(bdIndx, 0) = item.BillingPeriod & " - " & item.STLRun
                    contentDetails(bdIndx, 1) = item.Particulars
                    contentDetails(bdIndx, 2) = item.SellerParticipant.IDNumber
                    contentDetails(bdIndx, 3) = item.SellerBillingID
                    contentDetails(bdIndx, 4) = item.SellerParticipant.FullName
                    contentDetails(bdIndx, 5) = item.SellerParticipant.ParticipantAddress
                    contentDetails(bdIndx, 6) = item.SellerParticipant.TIN
                    contentDetails(bdIndx, 7) = item.InvoiceNo
                    contentDetails(bdIndx, 8) = item.VatablePurchases
                    contentDetails(bdIndx, 9) = item.ZeroRatedPurchases
                    contentDetails(bdIndx, 10) = item.ZeroRatedEcoZonePurchases
                    contentDetails(bdIndx, 11) = item.VatOnPurchases
                    contentDetails(bdIndx, 12) = item.WithholdingTax
                    contentDetails(bdIndx, 13) = item.Total
                Next

                bdIndx += 1
                contentDetails(bdIndx, 0) = ""
                contentDetails(bdIndx, 1) = "Grand Total: "
                contentDetails(bdIndx, 2) = ""
                contentDetails(bdIndx, 3) = ""
                contentDetails(bdIndx, 4) = ""
                contentDetails(bdIndx, 5) = ""
                contentDetails(bdIndx, 6) = ""
                contentDetails(bdIndx, 7) = ""
                contentDetails(bdIndx, 8) = gTotalVatablePurchases
                contentDetails(bdIndx, 9) = gTotalZRPurchases
                contentDetails(bdIndx, 10) = gTotalZREcozonePurchases
                contentDetails(bdIndx, 11) = gTotalVATonPurchases
                contentDetails(bdIndx, 12) = gTotalWhTax
                contentDetails(bdIndx, 13) = gTotalTotal

                xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                rowIndex += bdIndx
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 14), Excel.Range)
                xlContentDetails = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlContentDetails.Value = contentDetails
                Dim transDate As String = ""

                If dateFrom = dateTo Then
                    transDate = dateFrom.ToString("yyyyMMdd")
                Else
                    transDate = dateFrom.ToString("yyyyMMdd") & " - " & dateTo.ToString("yyyyMMdd")
                End If

                Dim FileName As String = "CSR_IEMOP_" & partifipantInfo.IDNumber.Replace("_FIT", "").ToString & "_" & "CR-N" & Me.BIRRulingCR.BIRRNo.ToString("D4") & "-" & birCRParticipant.BRCollReportNumber.ToString("D4") & "_" & transDate & ".xlsx"
                xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                            Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
                xlWorkBook.Close(False)
                xlApp.Quit()

                releaseObject(xlContentDetails)
                releaseObject(xlContentHeader)
                releaseObject(xlRowRange2)
                releaseObject(xlRowRange1)
                releaseObject(xlWorkSheet)
                releaseObject(xlWorkBook)
                releaseObject(xlApp)

            End If

        Catch ex As Exception
            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlApp)
            Throw New Exception(ex.Message)
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

    Private Function ConvertToDataTable(list As List(Of BIRRulingDetails)) As DataTable
        Dim table As New DataTable()

        table.TableName = "SampleAlloc"
        table.Columns.Add("CRBatchNo")
        table.Columns.Add("BillingPeriod")
        table.Columns.Add("STLRun")
        table.Columns.Add("Particulars")
        table.Columns.Add("CollectionDate")
        table.Columns.Add("BuyerParticipant")
        table.Columns.Add("Renewable")
        table.Columns.Add("ZeroRated")
        table.Columns.Add("IncomeTaxHoliday")
        table.Columns.Add("InvoiceNo")
        table.Columns.Add("VatableSales")
        table.Columns.Add("ZeroRatedSales")
        table.Columns.Add("ZeroRatedEcoZone")
        table.Columns.Add("VatOnSales")
        table.Columns.Add("WithholdingTax")

        For Each item In list
            Dim row As DataRow = table.NewRow()
            row("CRBatchNo") = item.CRBatchNo
            row("BillingPeriod") = item.BillingPeriod
            row("STLRun") = item.STLRun
            row("Particulars") = item.Particulars
            row("CollectionDate") = item.CollectionDate
            row("BuyerParticipant") = item.BuyerParticipant.IDNumber
            row("InvoiceNo") = item.InvoiceNo
            row("VatableSales") = item.VatableSales
            row("ZeroRatedSales") = item.ZeroRatedSales
            row("ZeroRatedEcoZone") = item.ZeroRatedEcoZone
            row("VatOnSales") = item.VatOnSales
            row("WithholdingTax") = item.WithholdingTax
            table.Rows.Add(row)
        Next
        Return table
    End Function

End Class
