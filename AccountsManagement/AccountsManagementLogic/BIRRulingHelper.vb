Imports System.Text
Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Imports System.ComponentModel
Imports Microsoft.Office.Interop

Public Class BIRRulingHelper
    Dim _CollectionReportNo As Integer = 0
    Dim newProgress As ProgressClass
    Public Sub New()
        'Get the current instance of the dal
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._BIRRuling = New BIRRuling
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

#Region "BIR Ruling"
    Private _BIRRuling As BIRRuling
    Public Property BIRRuling() As BIRRuling
        Get
            Return _BIRRuling
        End Get
        Set(ByVal value As BIRRuling)
            _BIRRuling = value
        End Set
    End Property
#End Region

#Region "Initial BIR Ruling"
    Private _IniBIRRuling As BIRRuling
    Public Property IniBIRRuling() As BIRRuling
        Get
            Return _IniBIRRuling
        End Get
        Set(ByVal value As BIRRuling)
            _IniBIRRuling = value
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
                                "AND  APN.REMITTANCE_DATE <= TO_DATE('" & CDate(FormatDateTime(ARDateTo, DateFormat.ShortDate)) & "', 'MM/DD/YYYY') " & vbNewLine &
                                "AND AC.IS_ALLOCATED = " & EnumIsAllocated.Allocated & " AND AC.IS_POSTED = 1 AND ACA.COLLECTION_TYPE IN (" & vbNewLine &
                                EnumCollectionType.DefaultInterestOnEnergy & ", " &
                                EnumCollectionType.Energy & ", " &
                                EnumCollectionType.VatOnEnergy & ") AND AWS.INV_DM_CM LIKE 'TS-W%' AND AWS.INV_DM_CM NOT LIKE '%-ADJ'"

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
    Private Function GetListOfWTCertStl(ByVal dateFrom As Date, ByVal dateTo As Date)
        Dim ret As New List(Of WHTaxCertificateSTL)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.*, B.PARTICIPANT_ID, B.FULL_NAME FROM AM_CERTIFICATE_WHTAX_STL A " & vbNewLine _
                               & "LEFT JOIN AM_PARTICIPANTS B ON B.ID_NUMBER = A.BILLING_IDNUMBER " & vbNewLine _
                              & "WHERE A.REMITTANCE_DATE >= TO_DATE('" & dateFrom.ToShortDateString & "','MM/DD/YYYY') AND A.REMITTANCE_DATE <= TO_DATE('" & dateTo & "','MM/DD/YYYY')"
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

                    item.TagDetails = (From x In getDetails Where x.Amount > 0 Select x).ToList
                    item.AllocationDetails = (From x In getDetails Where x.Amount < 0 Select x).ToList
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
            Dim SQL As String = "SELECT A.CERTIFICATE_NO, A.WESMBILL_SUMMARY_NO AS WBSNO, A.ENDING_BALANCE AS EB, A.AMOUNT_TAGGED, A.NEW_ENDING_BALANCE AS NEB, A.WHTAX_AMOUNT, A.NEW_DUE_DATE, B.*, C.ID_NUMBER, C.PARTICIPANT_ID, D.REMARKS " & vbNewLine _
                              & "FROM AM_CERTIFICATE_WHTAX_DETAILS A " & vbNewLine _
                              & "LEFT JOIN AM_WESM_BILL_SUMMARY B ON B.WESMBILL_SUMMARY_NO = A.WESMBILL_SUMMARY_NO " & vbNewLine _
                              & "LEFT JOIN AM_PARTICIPANTS C ON C.ID_NUMBER = B.ID_NUMBER " & vbNewLine _
                              & "LEFT JOIN (SELECT DISTINCT INVOICE_NO, REMARKS FROM AM_WESM_BILL) D on D.INVOICE_NO = B.INV_DM_CM " & vbNewLine _
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

            SQL = "SELECT A.ID_NUMBER, A.ID_TYPE, A.GROUP_NO, B.PARTICIPANT_ID, A.billing_period, A.TRANSACTION_DATE, A.ENERGY_WITHHOLD, " & vbNewLine &
                                  "b.participant_address, b.city, b.province, b.zip_code, " & vbNewLine &
                                  "A.charge_type, A.due_date, A.ENDING_BALANCE, a.BEGINNING_BALANCE, a.NEW_DUEDATE, a.IS_MFWTAX_DEDUCTED, " & vbNewLine &
                                  "A.INV_DM_CM, A.SUMMARY_TYPE, a.WESMBILL_SUMMARY_NO, A.ADJUSTMENT, A.WESMBILL_BATCH_NO, A.ENERGY_WITHHOLD_STATUS, a.NO_OFFSET, A.NO_SOA, A.NO_DEFINT, c.remarks " & vbNewLine &
                      "FROM AM_WESM_BILL_SUMMARY A " & vbNewLine &
                      "JOIN  AM_PARTICIPANTS B on a.id_number = b.id_number " & vbNewLine &
                      "JOIN  AM_WESM_BILL C on c.invoice_no = a.inv_dm_cm And c.charge_type = a.charge_type " & vbNewLine &
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

#Region "Get Payments in AP"
    Public Function GetAMPaymentNewAP(ByVal ARDateFrom As Date, ByVal ARDateTo As Date) As List(Of APAllocation)
        Dim ret As New List(Of APAllocation)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT A.*, B.CHARGE_TYPE, B.INV_DM_CM, C.ID_NUMBER, C.PARTICIPANT_ID, B.WESMBILL_BATCH_NO, D.REMARKS, E.REMITTANCE_DATE FROM AM_PAYMENT_NEW_AP A " _
                              & "INNER JOIN AM_WESM_BILL_SUMMARY B ON A.WESMBILL_SUMMARY_NO = B.WESMBILL_SUMMARY_NO " _
                              & "INNER JOIN AM_PARTICIPANTS C ON C.ID_NUMBER = A.ID_NUMBER " _
                              & "INNER JOIN AM_WESM_BILL D ON D.INVOICE_NO = B.INV_DM_CM And D.CHARGE_TYPE = B.CHARGE_TYPE " _
                              & "INNER JOIN AM_PAYMENT_NEW E ON E.PAYMENT_NO = A.PAYMENT_NO " _
                              & "WHERE E.REMITTANCE_DATE  >= " & "TO_DATE('" & CDate(FormatDateTime(ARDateFrom, DateFormat.ShortDate)) & "', 'MM/DD/YYYY') " & vbNewLine _
                              & "AND E.REMITTANCE_DATE <= TO_DATE('" & CDate(FormatDateTime(ARDateTo, DateFormat.ShortDate)) & "', 'MM/DD/YYYY') " & vbNewLine _
                              & "AND B.BALANCE_TYPE = 'AP' AND B.INV_DM_CM LIKE 'TS-W%' AND B.INV_DM_CM NOT LIKE '%-ADJ'"

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

#Region "Get WESM Bills Sales And Purchases"

    Public Function GetWESMBillSalesAndPurchased(ByVal listBPNo As List(Of Integer)) As List(Of WESMBillSalesAndPurchased)
        Dim ret As New List(Of WESMBillSalesAndPurchased)
        Dim report As New DataReport
        Try
            Dim listOfBP As String = ""
            For Each item In listBPNo
                If listOfBP.Length = 0 Then
                    listOfBP = item
                Else
                    listOfBP &= "," & item
                End If
            Next
            Dim SQL As String = "SELECT A.*, C.PARTICIPANT_ID, C.FULL_NAME, C.BILLING_ADDRESS FROM AM_WESM_BILL_SALES_PURCHASED A " & vbNewLine _
                              & "LEFT JOIN AM_PARTICIPANTS C ON C.ID_NUMBER = A.ID_NUMBER " _
                              & "WHERE A.BILLING_PERIOD IN (" & listOfBP & ")"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetWESMBillSalesAndPurchased(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetWESMBillSalesAndPurchased(ByVal dr As IDataReader) As List(Of WESMBillSalesAndPurchased)
        Dim result As New List(Of WESMBillSalesAndPurchased)

        Try
            While dr.Read()
                Dim item As New WESMBillSalesAndPurchased

                With dr
                    item.BillingPeriod = CInt(.Item("BILLING_PERIOD"))
                    item.SettlementRun = CStr(.Item("STL_RUN"))
                    item.IDNumber = New AMParticipants(CStr(.Item("ID_NUMBER")), CStr(.Item("PARTICIPANT_ID")))
                    item.RegistrationID = CStr(.Item("REG_ID"))
                    item.InvoiceNumber = CStr(.Item("INVOICE_NO"))
                    item.VatableSales = CDec(.Item("VATABLE_SALES"))
                    item.ZeroRatedSales = CDec(.Item("ZERO_RATED_SALES"))
                    item.ZeroRatedEcozone = CDec(.Item("ZERO_RATED_ECOZONE"))
                    item.VatablePurchases = CDec(.Item("VATABLE_PURCHASES"))
                    item.ZeroRatedPurchases = CDec(.Item("ZERO_RATED_PURCHASES"))
                    item.NetSettlementAmount = CDec(.Item("TTA"))
                    item.VATonSales = CDec(.Item("VAT_ON_SALES"))
                    item.VATonPurchases = CDec(.Item("VAT_ON_PURCHASES"))
                    item.WithholdingTAX = CDec(.Item("WITHHOLDING_TAX"))
                    item.GMR = CDec(.Item("GMR"))
                    item.NSSRA = CDec(.Item("NSSRA"))
                    item.TransactionType = CType(System.Enum.Parse(GetType(EnumWESMBillSalesAndPurchasedTransType),
                                                                   CStr(.Item("TRANSACTION_TYPE"))), EnumWESMBillSalesAndPurchasedTransType)
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

#Region "Get WESM Transaction Cover Summary"
    Private Function GetListWESMTransCoverSummary(ByVal transNumber As String, ByVal getDetails As Boolean) As List(Of WESMBillAllocCoverSummary)
        Dim ret As New List(Of WESMBillAllocCoverSummary)
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
    Private Function GetListWESMTransCoverSummary(ByVal dr As IDataReader, ByVal getDetails As Boolean) As List(Of WESMBillAllocCoverSummary)
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
                    If getDetails = True Then
                        item.ListWBAllocDisDetails = GetWBAllocDisDetails(CLng(.Item("SUMMARY_ID")))
                    End If
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

#Region "Get BIR Ruling For Viewing"
    Public Function CheckTransactionDateRange(ByVal dtFrom As Date, ByVal dtTo As Date) As Boolean
        Dim ret As Boolean
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT A.* FROM AM_BIR_RULING_CR A " & vbNewLine _
                              & "WHERE (A.TRANS_DATE_FROM >= TO_DATE('" & dtFrom & "','MM/DD/YYYY') AND A.TRANS_DATE_FROM <= TO_DATE('" & dtTo & "','MM/DD/YYYY')) " & vbNewLine _
                              & "Or (A.TRANS_DATE_TO  >= TO_DATE('" & dtFrom & "','MM/DD/YYYY') AND A.TRANS_DATE_TO <= TO_DATE('" & dtTo & "','MM/DD/YYYY')) "

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
        Dim items As New List(Of BIRRuling)
        Try
            While dr.Read()
                Dim itemBIR As New BIRRuling
                With dr
                    itemBIR.TransactionDateFrom = CDate(.Item("TRANS_DATE_FROM"))
                    itemBIR.TransactionDateTo = CDate(.Item("TRANS_DATE_TO"))
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
    Public Sub SetBIRRulingData(ByVal transDateFrom As Date, ByVal transDateTo As Date)
        Me._BIRRuling = GetBIRRuling(transDateFrom, transDateTo)
    End Sub


    Public Function GetBIRRuling(ByVal transDateFrom As Date, ByVal transDateTo As Date) As BIRRuling
        Dim ret As New BIRRuling
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT A.* FROM AM_BIR_RULING_CR A " & vbNewLine _
                              & "WHERE A.TRANS_DATE_FROM = TO_DATE('" & transDateFrom & "', 'MM/DD/YYYY') AND A.TRANS_DATE_TO = TO_DATE('" & transDateTo & "', 'MM/DD/YYYY') AND STATUS = 0"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetBIRRuling(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function
    Private Function GetBIRRuling(ByVal dr As IDataReader) As BIRRuling
        Dim result As New BIRRuling

        Try
            While dr.Read()
                With dr
                    result.TransactionDateFrom = CDate(.Item("TRANS_DATE_FROM"))
                    result.TransactionDateTo = CDate(.Item("TRANS_DATE_TO"))
                    result.SellerDetails = GetBIRRulingParticipants(CInt(.Item("AM_BIRR_NO")))
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

    Public Function GetBIRRulingParticipants(ByVal _BIRRNumber As Integer) As List(Of BIRRulingParticipant)
        Dim ret As New List(Of BIRRulingParticipant)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT A.*, B.* FROM AM_BIR_RULING_CR_PARTICIPANTS A " & vbNewLine _
                              & "LEFT JOIN AM_PARTICIPANTS B ON B.ID_NUMBER = A.ID_NUMBER " & vbNewLine _
                              & "WHERE A.AM_BIRR_NO = " & _BIRRNumber & " ORDER BY A.ID_NUMBER"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetBIRRulingDetailsParticipants(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetBIRRulingDetailsParticipants(ByVal dr As IDataReader) As List(Of BIRRulingParticipant)
        Dim result As New List(Of BIRRulingParticipant)

        Try
            While dr.Read()
                Dim item As New BIRRulingParticipant

                With dr
                    item.CollReportNumber = CLng(.Item("AM_BIRR_CRNO"))
                    item.Participant = New AMParticipants(CStr(.Item("ID_NUMBER")), CStr(.Item("PARTICIPANT_ID")), CStr(.Item("FULL_NAME")), If(IsDBNull(.Item("PARTICIPANT_ADDRESS").ToString()), "", .Item("PARTICIPANT_ADDRESS").ToString()))
                    item.BIRRDetails = GetBIRRulingDetails(CInt(.Item("AM_BIRR_CRNO")))

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

    Public Function GetBIRRulingDetails(ByVal _CRNoBatch As Integer) As List(Of BIRRulingDetails)
        Dim ret As New List(Of BIRRulingDetails)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT A.*, B.FULL_NAME FROM AM_BIR_RULING_CR_DETAILS A " & vbNewLine _
                              & "LEFT JOIN AM_PARTICIPANTS B ON B.ID_NUMBER = A.BUYER_ID_NUMBER " & vbNewLine _
                              & "WHERE A.CRNO_BATCH = " & _CRNoBatch
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetBIRRulingDetails(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetBIRRulingDetails(ByVal dr As IDataReader) As List(Of BIRRulingDetails)
        Dim result As New List(Of BIRRulingDetails)

        Try
            While dr.Read()
                Dim item As New BIRRulingDetails

                With dr
                    item.CRBatchNo = CInt(.Item("CRNO_BATCH"))
                    item.BillingPeriod = CInt(.Item("BILLING_PERIOD"))
                    item.STLRun = CStr(.Item("STL_RUN"))
                    item.Particulars = CStr(.Item("PARTICULARS"))
                    item.CollectionDate = CDate(.Item("COLLECTION_DATE"))
                    item.BuyerParticipant = New AMParticipants(CStr(.Item("BUYER_ID_NUMBER")), CStr(.Item("BUYER_ID_NUMBER")), CStr(.Item("FULL_NAME")))
                    item.InvoiceNo = CStr(.Item("INVOICE_NO"))
                    item.VatableSales = CDec(.Item("VATABLE_SALES"))
                    item.ZeroRatedSales = CDec(.Item("ZERO_RATED_SALES"))
                    item.ZeroRatedEcoZone = CDec(.Item("ZERO_RATED_ECOZONE"))
                    item.VatOnSales = CDec(.Item("VAT_ON_SALES"))
                    item.WithholdingTax = CDec(.Item("WH_TAX"))
                    item.BuyerInvoiceNo = CStr(.Item("BUYER_INV_NO"))
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
    Private Sub SaveGenereatedBIRRuling(ByVal _BIRRuling As BIRRuling)
        Dim listSQL As New List(Of String)
        Dim SysDateTime As Date = WBillHelper.GetSystemDateTime()
        Dim SQL As String = ""
        Dim newBIRNumber As Long = GetNextValSequence("SEQ_AM_BIRRULING_NO")
        With _IniBIRRuling
            SQL = "INSERT INTO AM_BIR_RULING_CR (AM_BIRR_NO,TRANS_DATE_FROM, TRANS_DATE_TO,CREATED_BY,CREATED_DATE_TIME) " & vbNewLine _
                & "SELECT " & newBIRNumber & ", TO_DATE('" & .TransactionDateFrom & "','MM/DD/YYYY'), TO_DATE('" & .TransactionDateTo & "','MM/DD/YYYY'), '" & AMModule.UserName & "', " & vbNewLine _
                & "TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') FROM DUAL"
            listSQL.Add(SQL)

            For Each item In .SellerDetails
                Dim newBIRPNumber As Long = GetNextValSequence("SEQ_AM_BIRRULING_CRNO")
                SQL = "INSERT INTO AM_BIR_RULING_CR_PARTICIPANTS (AM_BIRR_CRNO, ID_NUMBER, AM_BIRR_NO)" & vbNewLine _
                    & "SELECT " & newBIRPNumber & ", '" & item.Participant.IDNumber & "', " & newBIRNumber & " FROM DUAL"
                listSQL.Add(SQL)
                For Each detail In item.BIRRDetails
                    SQL = "INSERT INTO AM_BIR_RULING_CR_DETAILS (CRNO_BATCH, BUYER_ID_NUMBER, BILLING_PERIOD, STL_RUN, PARTICULARS, COLLECTION_DATE, INVOICE_NO, VATABLE_SALES, ZERO_RATED_SALES, ZERO_RATED_ECOZONE, VAT_ON_SALES, WH_TAX, BUYER_INV_NO)" & vbNewLine _
                        & "SELECT " & newBIRPNumber & ", '" & detail.BuyerParticipant.IDNumber & "', " & detail.BillingPeriod & ", '" & detail.STLRun & "', '" & detail.Particulars & "', " & vbNewLine _
                        & "TO_DATE('" & detail.CollectionDate & "','MM/DD/YYYY'), '" & detail.InvoiceNo & "', " & detail.VatableSales & ", " & detail.ZeroRatedSales & ", " & vbNewLine _
                        & detail.ZeroRatedEcoZone & ", " & detail.VatOnSales & ", " & detail.WithholdingTax & ", '" & detail.BuyerInvoiceNo & "' FROM DUAL"
                    listSQL.Add(SQL)
                Next
            Next
        End With
        Me.SaveToDB(listSQL)
    End Sub
    Private Sub SaveToDB(ByVal lisOfSQL As List(Of String))
        Dim report As New DataReport
        Dim ListofSQL As New List(Of String)
        Try
            report = Me.DataAccess.ExecuteSaveQuery(lisOfSQL, New DataSet)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub

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
#End Region

#Region "Generate BIR Ruling"
    Public Sub GenerateBIRRuling(ByVal dateFrom As Date, ByVal dateTo As Date, ByVal progress As IProgress(Of ProgressClass))

        newProgress = New ProgressClass
        newProgress.ProgressMsg = "Please wait while fetching AR Tagged Collection from " & dateFrom & " to " & dateTo
        progress.Report(newProgress)

        Me._ARCollectionList = GetCollectionAllocation(dateFrom, dateTo)

        newProgress.ProgressMsg = "Please wait while fetching AP Payment Allocation from " & dateFrom & " to " & dateTo
        progress.Report(newProgress)

        Me._APAllocationList = GetAMPaymentNewAP(dateFrom, dateTo)

        newProgress.ProgressMsg = "Please wait while fetching EWT Certificate from " & dateFrom & " to " & dateTo
        progress.Report(newProgress)
        Me._WHTAXCertificateTrans = GetListOfWTCertStl(dateFrom, dateTo)

        Dim listOfBIRRulingParticipant As List(Of BIRRulingParticipant) = New List(Of BIRRulingParticipant)
        listOfBIRRulingParticipant = ProcessBIRRulingParticipants(progress)

        Dim newBIRRuling As New BIRRuling
        With newBIRRuling
            .TransactionDateFrom = dateFrom
            .TransactionDateTo = dateTo
            .SellerDetails = listOfBIRRulingParticipant
        End With
        Me._IniBIRRuling = newBIRRuling
        newProgress.ProgressMsg = "Please wait while saving Collection Report.."
        progress.Report(newProgress)
        SaveGenereatedBIRRuling(newBIRRuling)

        SetBIRRulingData(dateFrom, dateTo)
    End Sub

    Private Function ProcessBIRRulingParticipants(ByVal progress As IProgress(Of ProgressClass)) As List(Of BIRRulingParticipant)

        Dim ret As New List(Of BIRRulingParticipant)
        Dim finalListOfBIRRulingDetails As New List(Of BIRRulingDetails)
        Dim dicCRNumber As New Dictionary(Of String, Integer)
        Dim listOfDate As List(Of Date) = (From x In Me.ARCollectionList Select x.AllocationDate Distinct Order By AllocationDate).ToList()
        Dim listWESMTTranAllocCoverSummary As New List(Of WESMBillAllocCoverSummary)

        newProgress.ProgressMsg = "Please wait while processing Collection Report..."
        progress.Report(newProgress)

        For Each item In Me.ARCollectionList.Select(Function(x) x.InvoiceNumber).Distinct.ToList
            listWESMTTranAllocCoverSummary.Add(GetListWESMTransCoverSummary(item, True).First)
        Next

        For Each item In Me.APAllocationList.Select(Function(x) x.InvoiceNumber).Distinct.ToList
            listWESMTTranAllocCoverSummary.Add(GetListWESMTransCoverSummary(item, False).First)
        Next

        For Each iDate In listOfDate
            Dim getARCollectionInEnergy As List(Of ARCollection) = (From x In Me.ARCollectionList Where x.AllocationDate = iDate And x.CollectionType = EnumCollectionType.Energy Select x).ToList
            Dim getARCollectionInEnergyDI As List(Of ARCollection) = (From x In Me.ARCollectionList Where x.AllocationDate = iDate And (x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy) Select x).ToList
            Dim getARCollectionInVAT As List(Of ARCollection) = (From x In Me.ARCollectionList Where x.AllocationDate = iDate And x.CollectionType = EnumCollectionType.VatOnEnergy Select x).ToList

            Dim getAPALlocationInEnergy As List(Of APAllocation) = (From x In Me.APAllocationList Where x.AllocationDate = iDate And (x.PaymentType = EnumPaymentNewType.Energy) Select x).ToList
            Dim getAPALlocationInEnergyDI As List(Of APAllocation) = (From x In Me.APAllocationList Where x.AllocationDate = iDate And (x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy) Select x).ToList
            Dim getAPALlocationInVAT As List(Of APAllocation) = (From x In Me.APAllocationList Where x.AllocationDate = iDate And x.PaymentType = EnumPaymentNewType.VatOnEnergy Select x).ToList

            'Energy
            Dim getInvNoInARCollectionInEnergy As List(Of String) = (From x In getARCollectionInEnergy Select x.InvoiceNumber Distinct Order By InvoiceNumber).ToList
            For Each invAR In getInvNoInARCollectionInEnergy
                Dim invARList As List(Of ARCollection) = (From x In getARCollectionInEnergy Where x.InvoiceNumber = invAR Select x).ToList
                Dim invARItem As ARCollection = (From x In invARList Select x).First

                Dim buyerWTACoverSummary As WESMBillAllocCoverSummary = listWESMTTranAllocCoverSummary.Where(Function(x) x.TransactionNo = invARItem.InvoiceNumber).Select(Function(x) x).First
                Dim buyerParticipantInfo As AMParticipants = (From x In Me.AMParticipantsList Where x.IDNumber = invARItem.IDNumber).First

                Dim getInvNoInAPAllocationInEnergy As List(Of String) = getAPALlocationInEnergy.Where(Function(x) x.WESMBillBatchNo = invARItem.WESMBillBatchNo).Select(Function(y) y.InvoiceNumber).Distinct.ToList()
                Dim allocationAmount As Decimal = invARList.Select(Function(x) x.AllocationAmount).Sum

                Dim newListOfBRDetails As New List(Of BIRRulingDetails)

                If buyerWTACoverSummary.ZeroRatedTag.Equals("Y") Then 'Non Vatable Buyer
                    For Each invAP In getInvNoInAPAllocationInEnergy
                        Dim _InvAP As APAllocation = (From x In getAPALlocationInEnergy Where x.InvoiceNumber = invAP Select x).First
                        Dim sellerWTACoverSummary As WESMBillAllocCoverSummary = listWESMTTranAllocCoverSummary.Where(Function(x) x.TransactionNo = invAP).Select(Function(x) x).First
                        Dim sellerParticpantInfo As AMParticipants = (From x In Me.AMParticipantsList Where x.IDNumber = _InvAP.IDNumber).First
                        Dim getSellerDtlFromBuyerWTACS As WESMBillAllocDisaggDetails = buyerWTACoverSummary.ListWBAllocDisDetails.Where(Function(x) x.STLID = sellerWTACoverSummary.StlID _
                                                                                                                                            And x.BillingID = sellerWTACoverSummary.BillingID).First
                        Dim newBRDetails As New BIRRulingDetails
                        Dim allocAmountShare As Decimal = 0D
                        Dim _SMR As Decimal = CDec(getSellerDtlFromBuyerWTACS.NetPurchase) / CDec(buyerWTACoverSummary.ListWBAllocDisDetails.Select(Function(x) x.NetPurchase).Sum())

                        allocAmountShare = Math.Round(allocationAmount * _SMR, 2)

                        If Not dicCRNumber.ContainsKey(_InvAP.IDNumber) Then
                            _CollectionReportNo += 1
                            dicCRNumber.Add(_InvAP.IDNumber, _CollectionReportNo)
                        End If
                        newBRDetails.CRBatchNo = dicCRNumber.Item(_InvAP.IDNumber)
                        newBRDetails.BillingPeriod = sellerWTACoverSummary.BillingPeriod
                        newBRDetails.STLRun = sellerWTACoverSummary.STLRun
                        newBRDetails.BuyerParticipant = buyerParticipantInfo
                        newBRDetails.CollectionDate = _InvAP.RemittanceDate
                        newBRDetails.InvoiceNo = _InvAP.InvoiceNumber
                        newBRDetails.BuyerInvoiceNo = invARItem.InvoiceNumber

                        Dim boolCash As Boolean = False
                        Dim boolPR As Boolean = False

                        For Each ar In invARList
                            If ar.CollectionCategory = EnumCollectionCategory.Cash Then
                                boolCash = True
                            ElseIf ar.CollectionCategory = EnumCollectionCategory.Drawdown Then
                                boolPR = True
                            End If
                        Next

                        If boolCash = True And boolPR = True Then
                            newBRDetails.Particulars = "ENERGY - Cash/PR"
                        ElseIf boolCash = True And boolPR = False Then
                            newBRDetails.Particulars = "ENERGY - Cash"
                        ElseIf boolCash = False And boolPR = True Then
                            newBRDetails.Particulars = "ENERGY - PR"
                        End If

                        newBRDetails.VatOnSales = 0
                        newBRDetails.WithholdingTax = 0

                        If sellerParticpantInfo.GenLoad = EnumGenLoad.G Then
                            If sellerWTACoverSummary.NonVatableTag.Equals("Y") Then 'For NonVatable Seller Generator
                                newBRDetails.VatableSales = 0
                                newBRDetails.ZeroRatedSales = allocAmountShare
                                newBRDetails.ZeroRatedEcoZone = 0
                            ElseIf sellerWTACoverSummary.NonVatableTag.Equals("N") Then 'For Vatable Seller Generator                                            
                                newBRDetails.VatableSales = 0
                                newBRDetails.ZeroRatedSales = 0
                                newBRDetails.ZeroRatedEcoZone = allocAmountShare
                            End If
                        ElseIf sellerParticpantInfo.GenLoad = EnumGenLoad.L Or sellerParticpantInfo.GenLoad = EnumGenLoad.DCC Then
                            If sellerWTACoverSummary.ZeroRatedTag.Equals("Y") Then 'For Zerorated Seller Load
                                newBRDetails.VatableSales = 0
                                newBRDetails.ZeroRatedSales = allocAmountShare
                                newBRDetails.ZeroRatedEcoZone = 0
                            ElseIf sellerWTACoverSummary.ZeroRatedTag.Equals("N") Then 'For Vatable Seller Load
                                newBRDetails.VatableSales = 0
                                newBRDetails.ZeroRatedSales = 0
                                newBRDetails.ZeroRatedEcoZone = allocAmountShare
                            End If

                        End If
                        newListOfBRDetails.Add(newBRDetails)
                    Next

                    Dim totalAllocatedSales As Decimal = (From x In newListOfBRDetails Select x.ZeroRatedSales + x.ZeroRatedEcoZone).Sum

                    If allocationAmount <> totalAllocatedSales And totalAllocatedSales <> 0 Then
                        Dim diff As Decimal = allocationAmount - totalAllocatedSales
                        Dim getHighestShare = (From x In newListOfBRDetails Select x Order By x.NetSale Ascending).First
                        If Math.Abs(getHighestShare.ZeroRatedSales) >= Math.Abs(getHighestShare.ZeroRatedEcoZone) Then
                            getHighestShare.ZeroRatedSales += diff
                        Else
                            getHighestShare.ZeroRatedEcoZone += diff
                        End If
                    ElseIf allocationAmount <> totalAllocatedSales And totalAllocatedSales = 0 Then
                        Dim getHighestWESMBill = getAPALlocationInEnergy.Where(Function(x) x.WESMBillBatchNo = invARItem.WESMBillBatchNo).Select(Function(y) y).OrderByDescending(Function(z) z.AllocationAmount).First
                        Dim diff As Decimal = allocationAmount - totalAllocatedSales
                        Dim getHighestShare = (From x In newListOfBRDetails Where x.InvoiceNo = getHighestWESMBill.InvoiceNumber Select x).First
                        If Math.Abs(getHighestShare.ZeroRatedSales) >= Math.Abs(getHighestShare.ZeroRatedEcoZone) Then
                            getHighestShare.ZeroRatedSales += diff
                        Else
                            getHighestShare.ZeroRatedEcoZone += diff
                        End If
                    End If

                    'Dim dt As DataTable = ConvertToDataTable(newListOfBRDetails)
                    For Each item In newListOfBRDetails
                        finalListOfBIRRulingDetails.Add(item)
                    Next
                    finalListOfBIRRulingDetails.TrimExcess()

                ElseIf buyerWTACoverSummary.ZeroRatedTag.Equals("N") Then 'Vatable Buyer      
                    For Each invAP In getInvNoInAPAllocationInEnergy
                        Dim _InvAP As APAllocation = (From x In getAPALlocationInEnergy Where x.InvoiceNumber = invAP Select x).First
                        Dim sellerWTACoverSummary As WESMBillAllocCoverSummary = listWESMTTranAllocCoverSummary.Where(Function(x) x.TransactionNo = invAP).Select(Function(x) x).First
                        Dim sellerParticpantInfo As AMParticipants = (From x In Me.AMParticipantsList Where x.IDNumber = _InvAP.IDNumber).First
                        Dim getSellerDtlFromBuyerWTACS As WESMBillAllocDisaggDetails = buyerWTACoverSummary.ListWBAllocDisDetails.Where(Function(x) x.STLID = sellerWTACoverSummary.StlID _
                                                                                                                                            And x.BillingID = sellerWTACoverSummary.BillingID).First
                        Dim newBRDetails As New BIRRulingDetails
                        Dim allocAmountShare As Decimal = 0D
                        Dim _SMR As Decimal = CDec(getSellerDtlFromBuyerWTACS.NetPurchase) / CDec(buyerWTACoverSummary.ListWBAllocDisDetails.Select(Function(x) x.NetPurchase).Sum())

                        allocAmountShare = Math.Round(allocationAmount * _SMR, 2)

                        If Not dicCRNumber.ContainsKey(_InvAP.IDNumber) Then
                            _CollectionReportNo += 1
                            dicCRNumber.Add(_InvAP.IDNumber, _CollectionReportNo)
                        End If

                        newBRDetails.CRBatchNo = dicCRNumber.Item(_InvAP.IDNumber)
                        newBRDetails.BillingPeriod = sellerWTACoverSummary.BillingPeriod
                        newBRDetails.STLRun = sellerWTACoverSummary.STLRun
                        newBRDetails.BuyerParticipant = buyerParticipantInfo
                        newBRDetails.CollectionDate = _InvAP.RemittanceDate
                        newBRDetails.InvoiceNo = _InvAP.InvoiceNumber
                        newBRDetails.BuyerInvoiceNo = invARItem.InvoiceNumber

                        Dim boolCash As Boolean = False
                        Dim boolPR As Boolean = False

                        For Each ar In invARList
                            If ar.CollectionCategory = EnumCollectionCategory.Cash Then
                                boolCash = True
                            ElseIf ar.CollectionCategory = EnumCollectionCategory.Drawdown Then
                                boolPR = True
                            End If
                        Next

                        If boolCash = True And boolPR = True Then
                            newBRDetails.Particulars = "ENERGY - Cash/PR"
                        ElseIf boolCash = True And boolPR = False Then
                            newBRDetails.Particulars = "ENERGY - Cash"
                        ElseIf boolCash = False And boolPR = True Then
                            newBRDetails.Particulars = "ENERGY - PR"
                        End If

                        newBRDetails.VatOnSales = 0
                        newBRDetails.WithholdingTax = 0

                        If sellerParticpantInfo.GenLoad = EnumGenLoad.G Then
                            If sellerWTACoverSummary.NonVatableTag.Equals("Y") Then 'For NonVatable Seller Generator
                                newBRDetails.VatableSales = 0
                                newBRDetails.ZeroRatedSales = allocAmountShare
                                newBRDetails.ZeroRatedEcoZone = 0
                            ElseIf sellerWTACoverSummary.NonVatableTag.Equals("N") Then 'For Vatable Seller Generator
                                newBRDetails.VatableSales = allocAmountShare
                                newBRDetails.ZeroRatedSales = 0
                                newBRDetails.ZeroRatedEcoZone = 0
                            End If
                        ElseIf sellerParticpantInfo.GenLoad = EnumGenLoad.L Or sellerParticpantInfo.GenLoad = EnumGenLoad.DCC Then
                            If sellerWTACoverSummary.ZeroRatedTag.Equals("Y") Then 'For Zerorated Seller Load                                            
                                newBRDetails.VatableSales = 0
                                newBRDetails.ZeroRatedSales = allocAmountShare
                                newBRDetails.ZeroRatedEcoZone = 0
                            ElseIf sellerWTACoverSummary.ZeroRatedTag.Equals("N") Then 'For Vatable Seller Load                                            
                                newBRDetails.VatableSales = allocAmountShare
                                newBRDetails.ZeroRatedSales = 0
                                newBRDetails.ZeroRatedEcoZone = 0
                            End If

                        End If
                        newListOfBRDetails.Add(newBRDetails)

                    Next
                    Dim totalAllocatedSales As Decimal = (From x In newListOfBRDetails Select x.NetSale).Sum
                    If allocationAmount <> totalAllocatedSales And totalAllocatedSales <> 0 Then
                        Dim diff As Decimal = allocationAmount - totalAllocatedSales
                        Dim getHighestShare = (From x In newListOfBRDetails Select x Order By x.NetSale Ascending).First
                        If Math.Abs(getHighestShare.VatableSales) >= Math.Abs(getHighestShare.ZeroRatedSales) Then
                            getHighestShare.VatableSales += diff
                        Else
                            getHighestShare.ZeroRatedSales += diff
                        End If
                    ElseIf allocationAmount <> totalAllocatedSales And totalAllocatedSales = 0 Then
                        Dim getHighestWESMBill = getAPALlocationInEnergy.Where(Function(x) x.WESMBillBatchNo = invARItem.WESMBillBatchNo).Select(Function(y) y).OrderByDescending(Function(z) z.AllocationAmount).First
                        Dim diff As Decimal = allocationAmount - totalAllocatedSales
                        Dim getHighestShare = (From x In newListOfBRDetails Where x.InvoiceNo = getHighestWESMBill.InvoiceNumber Select x).First
                        If Math.Abs(getHighestShare.VatableSales) >= Math.Abs(getHighestShare.ZeroRatedSales) Then
                            getHighestShare.VatableSales += diff
                        Else
                            getHighestShare.ZeroRatedSales += diff
                        End If
                    End If
                    For Each item In newListOfBRDetails
                        finalListOfBIRRulingDetails.Add(item)
                    Next
                    finalListOfBIRRulingDetails.TrimExcess()
                End If
            Next

            'Default Interest
            Dim getInvNoInARCollectionInEnergyDI As List(Of String) = (From x In getARCollectionInEnergyDI Select x.InvoiceNumber Distinct Order By InvoiceNumber).ToList
            For Each invAR In getInvNoInARCollectionInEnergyDI
                Dim invARList As List(Of ARCollection) = (From x In getARCollectionInEnergyDI Where x.InvoiceNumber = invAR Select x).ToList
                Dim invARItem As ARCollection = (From x In invARList Select x).First

                Dim buyerWTACoverSummary As WESMBillAllocCoverSummary = listWESMTTranAllocCoverSummary.Where(Function(x) x.TransactionNo = invARItem.InvoiceNumber).Select(Function(x) x).First
                Dim buyerParticipantInfo As AMParticipants = (From x In Me.AMParticipantsList Where x.IDNumber = invARItem.IDNumber).First

                Dim getInvNoInAPAllocationInEnergyDI As List(Of String) = getAPALlocationInEnergyDI.Where(Function(x) x.WESMBillBatchNo = invARItem.WESMBillBatchNo).Select(Function(y) y.InvoiceNumber).Distinct.ToList()
                Dim allocationAmount As Decimal = invARList.Select(Function(x) x.AllocationAmount).Sum

                Dim newListOfBRDetails As New List(Of BIRRulingDetails)

                If buyerWTACoverSummary.ZeroRatedTag.Equals("Y") Then 'Non Vatable Buyer
                    For Each invAP In getInvNoInAPAllocationInEnergyDI
                        Dim _InvAP As APAllocation = (From x In getAPALlocationInEnergyDI Where x.InvoiceNumber = invAP Select x).First
                        Dim sellerWTACoverSummary As WESMBillAllocCoverSummary = listWESMTTranAllocCoverSummary.Where(Function(x) x.TransactionNo = invAP).Select(Function(x) x).First
                        Dim sellerParticpantInfo As AMParticipants = (From x In Me.AMParticipantsList Where x.IDNumber = _InvAP.IDNumber).First
                        Dim getSellerDtlFromBuyerWTACS As WESMBillAllocDisaggDetails = buyerWTACoverSummary.ListWBAllocDisDetails.Where(Function(x) x.STLID = sellerWTACoverSummary.StlID _
                                                                                                                                            And x.BillingID = sellerWTACoverSummary.BillingID).First
                        Dim newBRDetails As New BIRRulingDetails
                        Dim allocAmountShare As Decimal = 0D
                        Dim _SMR As Decimal = CDec(getSellerDtlFromBuyerWTACS.NetPurchase) / CDec(buyerWTACoverSummary.ListWBAllocDisDetails.Select(Function(x) x.NetPurchase).Sum())

                        allocAmountShare = Math.Round(allocationAmount * _SMR, 2)

                        If Not dicCRNumber.ContainsKey(_InvAP.IDNumber) Then
                            _CollectionReportNo += 1
                            dicCRNumber.Add(_InvAP.IDNumber, _CollectionReportNo)
                        End If

                        newBRDetails.CRBatchNo = dicCRNumber.Item(_InvAP.IDNumber)
                        newBRDetails.BillingPeriod = sellerWTACoverSummary.BillingPeriod
                        newBRDetails.STLRun = sellerWTACoverSummary.STLRun
                        newBRDetails.BuyerParticipant = buyerParticipantInfo
                        newBRDetails.CollectionDate = _InvAP.RemittanceDate
                        newBRDetails.InvoiceNo = _InvAP.InvoiceNumber
                        newBRDetails.BuyerInvoiceNo = invARItem.InvoiceNumber

                        Dim boolCash As Boolean = False
                        Dim boolPR As Boolean = False

                        For Each ar In invARList
                            If ar.CollectionCategory = EnumCollectionCategory.Cash Then
                                boolCash = True
                            ElseIf ar.CollectionCategory = EnumCollectionCategory.Drawdown Then
                                boolPR = True
                            End If
                        Next

                        If boolCash = True And boolPR = True Then
                            newBRDetails.Particulars = "DEFINT - Cash/PR"
                        ElseIf boolCash = True And boolPR = False Then
                            newBRDetails.Particulars = "DEFINT - Cash"
                        ElseIf boolCash = False And boolPR = True Then
                            newBRDetails.Particulars = "DEFINT - PR"
                        End If

                        newBRDetails.VatOnSales = 0
                        newBRDetails.WithholdingTax = 0

                        If sellerParticpantInfo.GenLoad = EnumGenLoad.G Then
                            If sellerWTACoverSummary.NonVatableTag.Equals("Y") Then 'For NonVatable Seller Generator
                                newBRDetails.VatableSales = 0
                                newBRDetails.ZeroRatedSales = allocAmountShare
                                newBRDetails.ZeroRatedEcoZone = 0
                            ElseIf sellerWTACoverSummary.NonVatableTag.Equals("N") Then 'For Vatable Seller Generator                                            
                                newBRDetails.VatableSales = 0
                                newBRDetails.ZeroRatedSales = 0
                                newBRDetails.ZeroRatedEcoZone = allocAmountShare
                            End If
                        ElseIf sellerParticpantInfo.GenLoad = EnumGenLoad.L Or sellerParticpantInfo.GenLoad = EnumGenLoad.DCC Then
                            If sellerWTACoverSummary.ZeroRatedTag.Equals("Y") Then 'For Zerorated Seller Load
                                newBRDetails.VatableSales = 0
                                newBRDetails.ZeroRatedSales = allocAmountShare
                                newBRDetails.ZeroRatedEcoZone = 0
                            ElseIf sellerWTACoverSummary.ZeroRatedTag.Equals("N") Then 'For Vatable Seller Load
                                newBRDetails.VatableSales = 0
                                newBRDetails.ZeroRatedSales = 0
                                newBRDetails.ZeroRatedEcoZone = allocAmountShare
                            End If

                        End If
                        newListOfBRDetails.Add(newBRDetails)
                    Next

                    Dim totalAllocatedSales As Decimal = (From x In newListOfBRDetails Select x.ZeroRatedSales + x.ZeroRatedEcoZone).Sum

                    If allocationAmount <> totalAllocatedSales And totalAllocatedSales <> 0 Then
                        Dim diff As Decimal = allocationAmount - totalAllocatedSales
                        Dim getHighestShare = (From x In newListOfBRDetails Select x Order By x.NetSale Ascending).First
                        If Math.Abs(getHighestShare.ZeroRatedSales) >= Math.Abs(getHighestShare.ZeroRatedEcoZone) Then
                            getHighestShare.ZeroRatedSales += diff
                        Else
                            getHighestShare.ZeroRatedEcoZone += diff
                        End If
                    ElseIf allocationAmount <> totalAllocatedSales And totalAllocatedSales = 0 Then
                        Dim getHighestWESMBill = getAPALlocationInEnergyDI.Where(Function(x) x.WESMBillBatchNo = invARItem.WESMBillBatchNo).Select(Function(y) y).OrderByDescending(Function(z) z.AllocationAmount).First
                        Dim diff As Decimal = allocationAmount - totalAllocatedSales
                        Dim getHighestShare = (From x In newListOfBRDetails Where x.InvoiceNo = getHighestWESMBill.InvoiceNumber Select x).First
                        If Math.Abs(getHighestShare.ZeroRatedSales) >= Math.Abs(getHighestShare.ZeroRatedEcoZone) Then
                            getHighestShare.ZeroRatedSales += diff
                        Else
                            getHighestShare.ZeroRatedEcoZone += diff
                        End If
                    End If

                    'Dim dt As DataTable = ConvertToDataTable(newListOfBRDetails)
                    For Each item In newListOfBRDetails
                        finalListOfBIRRulingDetails.Add(item)
                    Next
                    finalListOfBIRRulingDetails.TrimExcess()

                ElseIf buyerWTACoverSummary.ZeroRatedTag.Equals("N") Then 'Vatable Buyer      
                    For Each invAP In getInvNoInAPAllocationInEnergyDI
                        Dim _InvAP As APAllocation = (From x In getAPALlocationInEnergyDI Where x.InvoiceNumber = invAP Select x).First
                        Dim sellerWTACoverSummary As WESMBillAllocCoverSummary = listWESMTTranAllocCoverSummary.Where(Function(x) x.TransactionNo = invAP).Select(Function(x) x).First
                        Dim sellerParticpantInfo As AMParticipants = (From x In Me.AMParticipantsList Where x.IDNumber = _InvAP.IDNumber).First
                        Dim getSellerDtlFromBuyerWTACS As WESMBillAllocDisaggDetails = buyerWTACoverSummary.ListWBAllocDisDetails.Where(Function(x) x.STLID = sellerWTACoverSummary.StlID _
                                                                                                                                            And x.BillingID = sellerWTACoverSummary.BillingID).First
                        Dim newBRDetails As New BIRRulingDetails
                        Dim allocAmountShare As Decimal = 0D
                        Dim _SMR As Decimal = CDec(getSellerDtlFromBuyerWTACS.NetPurchase) / CDec(buyerWTACoverSummary.ListWBAllocDisDetails.Select(Function(x) x.NetPurchase).Sum())

                        allocAmountShare = Math.Round(allocationAmount * _SMR, 2)

                        If Not dicCRNumber.ContainsKey(_InvAP.IDNumber) Then
                            _CollectionReportNo += 1
                            dicCRNumber.Add(_InvAP.IDNumber, _CollectionReportNo)
                        End If

                        newBRDetails.CRBatchNo = dicCRNumber.Item(_InvAP.IDNumber)
                        newBRDetails.BillingPeriod = sellerWTACoverSummary.BillingPeriod
                        newBRDetails.STLRun = sellerWTACoverSummary.STLRun
                        newBRDetails.BuyerParticipant = buyerParticipantInfo
                        newBRDetails.CollectionDate = _InvAP.RemittanceDate
                        newBRDetails.InvoiceNo = _InvAP.InvoiceNumber
                        newBRDetails.BuyerInvoiceNo = invARItem.InvoiceNumber

                        newBRDetails.Particulars = "VAT - Cash"

                        newBRDetails.VatOnSales = 0
                        newBRDetails.WithholdingTax = 0

                        If sellerParticpantInfo.GenLoad = EnumGenLoad.G Then
                            If sellerWTACoverSummary.NonVatableTag.Equals("Y") Then 'For NonVatable Seller Generator
                                newBRDetails.VatableSales = 0
                                newBRDetails.ZeroRatedSales = allocAmountShare
                                newBRDetails.ZeroRatedEcoZone = 0
                            ElseIf sellerWTACoverSummary.NonVatableTag.Equals("N") Then 'For Vatable Seller Generator
                                newBRDetails.VatableSales = allocAmountShare
                                newBRDetails.ZeroRatedSales = 0
                                newBRDetails.ZeroRatedEcoZone = 0
                            End If
                        ElseIf sellerParticpantInfo.GenLoad = EnumGenLoad.L Or sellerParticpantInfo.GenLoad = EnumGenLoad.DCC Then
                            If sellerWTACoverSummary.ZeroRatedTag.Equals("Y") Then 'For Zerorated Seller Load                                            
                                newBRDetails.VatableSales = 0
                                newBRDetails.ZeroRatedSales = allocAmountShare
                                newBRDetails.ZeroRatedEcoZone = 0
                            ElseIf sellerWTACoverSummary.ZeroRatedTag.Equals("N") Then 'For Vatable Seller Load                                            
                                newBRDetails.VatableSales = allocAmountShare
                                newBRDetails.ZeroRatedSales = 0
                                newBRDetails.ZeroRatedEcoZone = 0
                            End If

                        End If
                        newListOfBRDetails.Add(newBRDetails)
                    Next
                    Dim totalAllocatedSales As Decimal = (From x In newListOfBRDetails Select x.NetSale).Sum
                    If allocationAmount <> totalAllocatedSales And totalAllocatedSales <> 0 Then
                        Dim diff As Decimal = allocationAmount - totalAllocatedSales
                        Dim getHighestShare = (From x In newListOfBRDetails Select x Order By x.NetSale Ascending).First
                        If Math.Abs(getHighestShare.VatableSales) >= Math.Abs(getHighestShare.ZeroRatedSales) Then
                            getHighestShare.VatableSales += diff
                        Else
                            getHighestShare.ZeroRatedSales += diff
                        End If
                    ElseIf allocationAmount <> totalAllocatedSales And totalAllocatedSales = 0 Then
                        Dim getHighestWESMBill = getAPALlocationInEnergyDI.Where(Function(x) x.WESMBillBatchNo = invARItem.WESMBillBatchNo).Select(Function(y) y).OrderByDescending(Function(z) z.AllocationAmount).First
                        Dim diff As Decimal = allocationAmount - totalAllocatedSales
                        Dim getHighestShare = (From x In newListOfBRDetails Where x.InvoiceNo = getHighestWESMBill.InvoiceNumber Select x).First
                        If Math.Abs(getHighestShare.VatableSales) >= Math.Abs(getHighestShare.ZeroRatedSales) Then
                            getHighestShare.VatableSales += diff
                        Else
                            getHighestShare.ZeroRatedSales += diff
                        End If
                    End If
                    For Each item In newListOfBRDetails
                        finalListOfBIRRulingDetails.Add(item)
                    Next
                    finalListOfBIRRulingDetails.TrimExcess()
                End If
            Next

            'VAT
            Dim getInvNoInARCollectionInVAT As List(Of String) = (From x In getARCollectionInVAT Select x.InvoiceNumber Distinct Order By InvoiceNumber).ToList
            For Each invAR In getInvNoInARCollectionInVAT
                Dim invARList As List(Of ARCollection) = (From x In getARCollectionInVAT Where x.InvoiceNumber = invAR Select x).ToList
                Dim invARItem As ARCollection = (From x In invARList Select x).First

                Dim buyerWTACoverSummary As WESMBillAllocCoverSummary = listWESMTTranAllocCoverSummary.Where(Function(x) x.TransactionNo = invARItem.InvoiceNumber).Select(Function(x) x).First
                Dim buyerParticipantInfo As AMParticipants = (From x In Me.AMParticipantsList Where x.IDNumber = invARItem.IDNumber).First

                Dim getInvNoInAPAllocationInVAT As List(Of String) = getAPALlocationInVAT.Where(Function(x) x.WESMBillBatchNo = invARItem.WESMBillBatchNo).Select(Function(y) y.InvoiceNumber).Distinct.ToList()
                Dim allocationAmount As Decimal = invARList.Select(Function(x) x.AllocationAmount).Sum

                Dim newListOfBRDetails As New List(Of BIRRulingDetails)

                For Each invAP In getInvNoInAPAllocationInVAT
                    Dim _InvAP As APAllocation = (From x In getAPALlocationInVAT Where x.InvoiceNumber = invAP Select x).First
                    Dim sellerWTACoverSummary As WESMBillAllocCoverSummary = listWESMTTranAllocCoverSummary.Where(Function(x) x.TransactionNo = invAP).Select(Function(x) x).First
                    Dim sellerParticpantInfo As AMParticipants = (From x In Me.AMParticipantsList Where x.IDNumber = _InvAP.IDNumber).First
                    Dim getSellerDtlFromBuyerWTACS As WESMBillAllocDisaggDetails = buyerWTACoverSummary.ListWBAllocDisDetails.Where(Function(x) x.STLID = sellerWTACoverSummary.StlID _
                                                                                                                                            And x.BillingID = sellerWTACoverSummary.BillingID).First
                    Dim newBRDetails As New BIRRulingDetails
                    Dim allocAmountShare As Decimal = 0D
                    Dim _SMR As Decimal = CDec(getSellerDtlFromBuyerWTACS.VatOnPurchases) / CDec(buyerWTACoverSummary.ListWBAllocDisDetails.Select(Function(x) x.VatOnPurchases).Sum())

                    allocAmountShare = Math.Round(allocationAmount * _SMR, 2)

                    If Not dicCRNumber.ContainsKey(_InvAP.IDNumber) Then
                        _CollectionReportNo += 1
                        dicCRNumber.Add(_InvAP.IDNumber, _CollectionReportNo)
                    End If
                    newBRDetails.CRBatchNo = dicCRNumber.Item(_InvAP.IDNumber)
                    newBRDetails.BillingPeriod = sellerWTACoverSummary.BillingPeriod
                    newBRDetails.STLRun = sellerWTACoverSummary.STLRun
                    newBRDetails.BuyerParticipant = buyerParticipantInfo
                    newBRDetails.CollectionDate = _InvAP.RemittanceDate
                    newBRDetails.InvoiceNo = _InvAP.InvoiceNumber
                    newBRDetails.BuyerInvoiceNo = invARItem.InvoiceNumber
                    newBRDetails.Particulars = "VAT - Cash"
                    newBRDetails.VatableSales = 0
                    newBRDetails.ZeroRatedSales = 0
                    newBRDetails.ZeroRatedEcoZone = 0
                    newBRDetails.VatOnSales = allocAmountShare
                    newBRDetails.WithholdingTax = 0
                    newListOfBRDetails.Add(newBRDetails)
                Next

                Dim totalAllocatedSales As Decimal = (From x In newListOfBRDetails Select x.VatOnSales).Sum
                If allocationAmount <> totalAllocatedSales And totalAllocatedSales <> 0 Then
                    Dim diff As Decimal = allocationAmount - totalAllocatedSales
                    Dim getHighestShare = (From x In newListOfBRDetails Select x Order By x.VatOnSales Ascending).First
                    getHighestShare.VatOnSales += diff
                ElseIf allocationAmount <> totalAllocatedSales And totalAllocatedSales = 0 Then
                    Dim getHighestWESMBill = getAPALlocationInVAT.Where(Function(x) x.WESMBillBatchNo = invARItem.WESMBillBatchNo).Select(Function(y) y).OrderByDescending(Function(z) z.AllocationAmount).First
                    Dim diff As Decimal = allocationAmount - totalAllocatedSales
                    Dim getHighestShare = (From x In newListOfBRDetails Where x.InvoiceNo = getHighestWESMBill.InvoiceNumber Select x).First
                    getHighestShare.VatOnSales += diff
                End If
                'Dim dt As DataTable = ConvertToDataTable(newListOfBRDetails)
                For Each item In newListOfBRDetails
                    finalListOfBIRRulingDetails.Add(item)
                Next
                newProgress.ProgressMsg = "Please wait while processing Collection Report.."
                progress.Report(newProgress)
            Next

            'EWT
            If getAPALlocationInEnergy.Count <> 0 Then
                Dim getSellerWHTAXCertificate As List(Of WHTaxCertificateSTL) = (From x In Me.WHTAXCertificateTrans
                                                                                 Where x.RemittanceDate = getAPALlocationInEnergy.Select(Function(z) z.RemittanceDate).First
                                                                                 Select x).ToList()
                For Each eWHTAX In getSellerWHTAXCertificate.OrderBy(Function(x) x.BillingIDNumber.IDNumber)
                    Dim allParticulars = New String() {"ENERGY - Cash/PR", "ENERGY - Cash", "ENERGY - PR"}

                    Dim getUniqWESMBillBatchNo As List(Of Long) = eWHTAX.TagDetails.Select(Function(x) x.WESMBillSummary.WESMBillBatchNo).Distinct.OrderBy(Function(y) y).ToList()

                    For Each wbBatchNo As Long In getUniqWESMBillBatchNo
                        Dim getTotalEWTAP As Decimal = eWHTAX.AllocationDetails.Where(Function(y) y.WESMBillSummary.WESMBillBatchNo = wbBatchNo).Select(Function(x) x.WithholdingTaxAmount).Sum()
                        Dim getTotalEWTAPAlloc As Decimal = 0
                        Dim getEWHTaxTagDtlperWBBatch As List(Of WHTaxCertificateDetails) = eWHTAX.TagDetails.Where(Function(x) x.WESMBillSummary.WESMBillBatchNo = wbBatchNo).ToList
                        Dim getTotalAmountTagged As Decimal = getEWHTaxTagDtlperWBBatch.Select(Function(x) x.Amount).Sum()
                        Dim getEWHTaxTagAlloc As List(Of WHTaxCertificateDetails) = eWHTAX.AllocationDetails.Where(Function(x) x.WESMBillSummary.WESMBillBatchNo = wbBatchNo).ToList
                        For Each itemAlloc In getEWHTaxTagAlloc
                            For Each itemTagged In getEWHTaxTagDtlperWBBatch

                                Dim getListOfBIRRulingDetails As BIRRulingDetails = finalListOfBIRRulingDetails.Where(Function(x) x.InvoiceNo = itemAlloc.WESMBillSummary.INVDMCMNo _
                                                                                                                  And allParticulars.Contains(x.Particulars) _
                                                                                                                  And x.BuyerInvoiceNo = itemTagged.WESMBillSummary.INVDMCMNo _
                                                                                                                  And x.CollectionDate = eWHTAX.RemittanceDate).First
                                Dim getEWTTaggedAmount As Decimal = Math.Round(itemAlloc.Amount * (itemTagged.Amount / getTotalAmountTagged), 2)

                                With getListOfBIRRulingDetails
                                    If getListOfBIRRulingDetails.VatableSales <> 0 Then
                                        getListOfBIRRulingDetails.VatableSales += getEWTTaggedAmount
                                    ElseIf getListOfBIRRulingDetails.ZeroRatedSales <> 0 Then
                                        getListOfBIRRulingDetails.ZeroRatedSales += getEWTTaggedAmount
                                    ElseIf getListOfBIRRulingDetails.ZeroRatedEcoZone <> 0 Then
                                        getListOfBIRRulingDetails.ZeroRatedEcoZone += getEWTTaggedAmount
                                    End If
                                    getListOfBIRRulingDetails.WithholdingTax += Math.Abs(getEWTTaggedAmount)
                                    getTotalEWTAPAlloc += Math.Abs(getEWTTaggedAmount)
                                End With
                            Next

                        Next

                        If getTotalAmountTagged <> getTotalEWTAPAlloc Then
                            Dim getDiffAmount As Decimal = getTotalAmountTagged - getTotalEWTAPAlloc
                            Dim getHigherInvAmountAP As String = eWHTAX.AllocationDetails.Where(Function(x) x.WESMBillSummary.WESMBillBatchNo = wbBatchNo).OrderBy(Function(z) z.WithholdingTaxAmount).Select(Function(y) y.WESMBillSummary.INVDMCMNo).First
                            Dim getHigherInvAmountAR As String = getEWHTaxTagDtlperWBBatch.OrderBy(Function(y) y.WithholdingTaxAmount).Select(Function(x) x.WESMBillSummary.INVDMCMNo).Last()

                            Dim getHigherAmountList = eWHTAX.AllocationDetails.Where(Function(x) x.WESMBillSummary.WESMBillBatchNo = wbBatchNo).OrderBy(Function(z) z.WithholdingTaxAmount).Select(Function(y) y).ToList
                            Dim getHigherAmount As BIRRulingDetails = finalListOfBIRRulingDetails.Where(Function(x) x.InvoiceNo = getHigherInvAmountAP _
                                                                                                        And allParticulars.Contains(x.Particulars) _
                                                                                                        And x.BuyerInvoiceNo = getHigherInvAmountAR _
                                                                                                        And x.CollectionDate = eWHTAX.RemittanceDate).First
                            If getHigherAmount.VatableSales <> 0 Then
                                getHigherAmount.VatableSales += getDiffAmount
                            ElseIf getHigherAmount.ZeroRatedSales <> 0 Then
                                getHigherAmount.ZeroRatedSales += getDiffAmount
                            ElseIf getHigherAmount.ZeroRatedEcoZone <> 0 Then
                                getHigherAmount.ZeroRatedEcoZone += getDiffAmount
                            End If
                            getHigherAmount.WithholdingTax += getDiffAmount
                        End If
                    Next
                Next
            End If
            finalListOfBIRRulingDetails.TrimExcess()
        Next

        For Each item In dicCRNumber.Keys
            Dim newBIRP As New BIRRulingParticipant
            Dim getParticipantInfo As AMParticipants = (From x In Me.AMParticipantsList Where x.IDNumber = item Select x).FirstOrDefault
            With newBIRP
                .CollReportNumber = dicCRNumber.Item(getParticipantInfo.IDNumber)
                .Participant = getParticipantInfo
                .BIRRDetails = (From x In finalListOfBIRRulingDetails Where x.CRBatchNo = dicCRNumber.Item(item) Select x).ToList
            End With
            ret.Add(newBIRP)
        Next
        ret.TrimExcess()

        Return ret
    End Function


#End Region

#Region "Get Process BIR Ruling from DB"
    Public Sub ViewBIRRulingData(ByVal dtFrom As Date, ByVal dtTo As Date)
        Me._BIRRuling = GetBIRRuling(dtFrom, dtTo)
    End Sub
#End Region

    Public Function GenerateCollectionReportDatatable(ByVal dt As DataTable, ByVal listOfSelectedParticipants As List(Of String)) As DataTable
        'Get the signatories for WESM Invoice
        Dim Signatory = WBillHelper.GetSignatories("BIR_COLLREPORT").First()

        For Each item In listOfSelectedParticipants
            Dim selectedParticipants As BIRRulingParticipant = (From x In Me.BIRRuling.SellerDetails Where x.Participant.IDNumber = item Select x).FirstOrDefault
            For Each detail In selectedParticipants.BIRRDetails.OrderBy(Function(x) x.CollectionDate)
                Dim row = dt.NewRow()
                row("TRANS_DATE_FROM") = Me.BIRRuling.TransactionDateFrom.ToShortDateString()
                row("TRANS_DATE_TO") = Me.BIRRuling.TransactionDateTo.ToShortDateString()
                row("CR_NUMBER") = "CR-N" & selectedParticipants.CollReportNumber.ToString("D7")
                row("ID_NUMBER") = selectedParticipants.Participant.IDNumber
                row("FULL_NAME") = selectedParticipants.Participant.FullName
                row("ADDRESS") = selectedParticipants.Participant.ParticipantAddress
                row("COLLECTION_DATE") = detail.CollectionDate
                row("BILLING_REMARKS") = detail.BillingPeriod.ToString & " - " & detail.STLRun.ToString
                row("PARTICULARS") = detail.Particulars.ToString
                row("RECEIVED_FROM") = detail.BuyerParticipant.FullName.ToString
                row("STATEMENT_NO") = detail.InvoiceNo.ToString
                row("VATABLE_SALES") = detail.VatableSales.ToString
                row("ZERO_RATED_SALES") = detail.ZeroRatedSales.ToString
                row("ZERO_RATED_ECOZONE") = detail.ZeroRatedEcoZone.ToString
                row("VAT_ON_SALES") = detail.VatOnSales.ToString
                row("WHTAX") = detail.WithholdingTax.ToString
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
        Return dt
    End Function

    Public Function GenerateCollectionReportDatatable(ByVal dt As DataTable, ByVal selectedParticipant As String) As DataTable
        'Get the signatories for WESM Invoice
        Dim Signatory = WBillHelper.GetSignatories("BIR_COLLREPORT").First()

        Dim selectedParticipants As BIRRulingParticipant = (From x In Me.BIRRuling.SellerDetails Where x.Participant.IDNumber = selectedParticipant Select x).FirstOrDefault
        For Each detail In selectedParticipants.BIRRDetails
            Dim row = dt.NewRow()
            row("TRANS_DATE_FROM") = Me.BIRRuling.TransactionDateFrom.ToShortDateString()
            row("TRANS_DATE_TO") = Me.BIRRuling.TransactionDateTo.ToShortDateString()
            row("CR_NUMBER") = "CR-N" & selectedParticipants.CollReportNumber.ToString("D7")
            row("ID_NUMBER") = selectedParticipants.Participant.IDNumber
            row("FULL_NAME") = selectedParticipants.Participant.FullName
            row("ADDRESS") = selectedParticipants.Participant.ParticipantAddress
            row("COLLECTION_DATE") = detail.CollectionDate.ToString
            row("BILLING_REMARKS") = detail.BillingPeriod.ToString & " - " & detail.STLRun.ToString
            row("PARTICULARS") = detail.Particulars.ToString
            row("RECEIVED_FROM") = detail.BuyerParticipant.FullName.ToString
            row("STATEMENT_NO") = detail.InvoiceNo.ToString
            row("VATABLE_SALES") = detail.VatableSales.ToString
            row("ZERO_RATED_SALES") = detail.ZeroRatedSales.ToString
            row("ZERO_RATED_ECOZONE") = detail.ZeroRatedEcoZone.ToString
            row("VAT_ON_SALES") = detail.VatOnSales.ToString
            row("WHTAX") = detail.WithholdingTax.ToString
            row("TOTAL") = detail.Total
            row("SIGNATORIES_1") = AMModule.FullName
            row("SIG1_POSITION") = AMModule.Position
            row("SINATORIES_2") = Signatory.Signatory_1
            row("SIG2_POSITION") = Signatory.Position_1
            row("SIGNATORIES_3") = Signatory.Signatory_2
            row("SIG3_POSITION") = Signatory.Position_2
            dt.Rows.Add(row)
        Next

        Return dt
    End Function


    Public Sub GenerateBIRCollectionReportExcel(ByVal Participant As String, ByVal SavingPathName As String, ByVal dateFrom As Date, ByVal dateTo As Date)
        Dim getParticipantInfo As AMParticipants = (From x In Me.AMParticipantsList Where x.IDNumber = Participant Select x).First
        Dim getBIRParticipant As BIRRulingParticipant = (From x In Me.BIRRuling.SellerDetails Where x.Participant.IDNumber = Participant Select x).First
        GenerateInExcel(SavingPathName, getParticipantInfo, dateFrom, dateTo, getBIRParticipant)
    End Sub

    Private Sub GenerateInExcel(ByVal SavingPathName As String, ByVal partifipantInfo As AMParticipants, ByVal dateFrom As Date, ByVal dateTo As Date, ByVal birParticipant As BIRRulingParticipant)
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
        If dateFrom = dateTo Then
            xlWorkSheet.Name = dateFrom.ToString("MMMM-dd-yyyy")
        Else
            xlWorkSheet.Name = dateFrom.ToString("MMMM-dd-yyyy") & " To " & dateTo.ToString("MMMM-dd-yyyy")
        End If


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
            If birParticipant.BIRRDetails.Count > 0 Then
                Dim bdIndx As Integer = 0
                Dim dicDate As New Dictionary(Of Date, Integer)

                Dim countCollDate As Long = (From x In birParticipant.BIRRDetails Select x.CollectionDate Distinct).Count
                Dim countDetails As Long = (From x In birParticipant.BIRRDetails Select x).Count

                ReDim contentDetails(countCollDate + countDetails + 1, 10)

                contentDetails(bdIndx, 0) = "Billing" & vbNewLine & "Remarks"
                contentDetails(bdIndx, 1) = "Particulars"
                contentDetails(bdIndx, 2) = "Received From"
                contentDetails(bdIndx, 3) = "(Seller) Statement No"
                contentDetails(bdIndx, 4) = "Vatable Sales"
                contentDetails(bdIndx, 5) = "Zero Rated Sales"
                contentDetails(bdIndx, 6) = "Zero Rated Ecozone"
                contentDetails(bdIndx, 7) = "VAT on Sales"
                contentDetails(bdIndx, 8) = "Withholding Tax"
                contentDetails(bdIndx, 9) = "Total"
                contentDetails(bdIndx, 10) = "(Buyer) Statement No"

                Dim gTotalVatableSales As Decimal = 0
                Dim gTotalZRSales As Decimal = 0
                Dim gTotalZREcozone As Decimal = 0
                Dim gTotalVATonSales As Decimal = 0
                Dim gTotalWhTax As Decimal = 0
                Dim gTotalTotal As Decimal = 0

                Dim TotalVatableSales As Decimal = 0
                Dim TotalZRSales As Decimal = 0
                Dim TotalZREcozone As Decimal = 0
                Dim TotalVATonSales As Decimal = 0
                Dim TotalWhTax As Decimal = 0
                Dim TotalTotal As Decimal = 0
                Dim previousCollectionDate As New Date

                For Each item In birParticipant.BIRRDetails.OrderBy(Function(n As BIRRulingDetails) n.CollectionDate).ThenBy(Function(x As BIRRulingDetails) x.InvoiceNo).ToList
                    bdIndx += 1
                    If Not dicDate.ContainsKey(item.CollectionDate) Then
                        If dicDate.Count <> 0 Then
                            contentDetails(bdIndx, 0) = ""
                            contentDetails(bdIndx, 1) = "Total Amount Collected For " & previousCollectionDate.ToString("MMMM dd, yyyy").ToUpper
                            contentDetails(bdIndx, 2) = ""
                            contentDetails(bdIndx, 3) = ""
                            contentDetails(bdIndx, 4) = TotalVatableSales
                            contentDetails(bdIndx, 5) = TotalZRSales
                            contentDetails(bdIndx, 6) = TotalZREcozone
                            contentDetails(bdIndx, 7) = TotalVATonSales
                            contentDetails(bdIndx, 8) = TotalWhTax
                            contentDetails(bdIndx, 9) = TotalTotal

                            TotalVatableSales = item.VatableSales
                            TotalZRSales = item.ZeroRatedSales
                            TotalZREcozone = item.ZeroRatedEcoZone
                            TotalVATonSales = item.VatOnSales
                            TotalWhTax = item.WithholdingTax
                            TotalTotal = item.Total

                            gTotalVatableSales += item.VatableSales
                            gTotalZRSales += item.ZeroRatedSales
                            gTotalZREcozone += item.ZeroRatedEcoZone
                            gTotalVATonSales += item.VatOnSales
                            gTotalWhTax += item.WithholdingTax
                            gTotalTotal += item.Total

                            bdIndx += 1
                            contentDetails(bdIndx, 0) = item.BillingPeriod & " - " & item.STLRun
                            contentDetails(bdIndx, 1) = item.Particulars
                            contentDetails(bdIndx, 2) = item.BuyerParticipant.FullName & "(" & item.BuyerParticipant.IDNumber & ")"
                            contentDetails(bdIndx, 3) = item.InvoiceNo
                            contentDetails(bdIndx, 4) = item.VatableSales
                            contentDetails(bdIndx, 5) = item.ZeroRatedSales
                            contentDetails(bdIndx, 6) = item.ZeroRatedEcoZone
                            contentDetails(bdIndx, 7) = item.VatOnSales
                            contentDetails(bdIndx, 8) = item.WithholdingTax
                            contentDetails(bdIndx, 9) = item.Total
                            contentDetails(bdIndx, 10) = item.BuyerInvoiceNo

                        Else
                            TotalVatableSales = item.VatableSales
                            TotalZRSales = item.ZeroRatedSales
                            TotalZREcozone = item.ZeroRatedEcoZone
                            TotalVATonSales = item.VatOnSales
                            TotalWhTax = item.WithholdingTax
                            TotalTotal = item.Total

                            gTotalVatableSales += item.VatableSales
                            gTotalZRSales += item.ZeroRatedSales
                            gTotalZREcozone += item.ZeroRatedEcoZone
                            gTotalVATonSales += item.VatOnSales
                            gTotalWhTax += item.WithholdingTax
                            gTotalTotal += item.Total

                            contentDetails(bdIndx, 0) = item.BillingPeriod & " - " & item.STLRun
                            contentDetails(bdIndx, 1) = item.Particulars
                            contentDetails(bdIndx, 2) = item.BuyerParticipant.FullName & "(" & item.BuyerParticipant.IDNumber & ")"
                            contentDetails(bdIndx, 3) = item.InvoiceNo
                            contentDetails(bdIndx, 4) = item.VatableSales
                            contentDetails(bdIndx, 5) = item.ZeroRatedSales
                            contentDetails(bdIndx, 6) = item.ZeroRatedEcoZone
                            contentDetails(bdIndx, 7) = item.VatOnSales
                            contentDetails(bdIndx, 8) = item.WithholdingTax
                            contentDetails(bdIndx, 9) = item.Total
                            contentDetails(bdIndx, 10) = item.BuyerInvoiceNo
                        End If
                        dicDate.Add(item.CollectionDate, 0)
                    Else
                        TotalVatableSales += item.VatableSales
                        TotalZRSales += item.ZeroRatedSales
                        TotalZREcozone += item.ZeroRatedEcoZone
                        TotalVATonSales += item.VatOnSales
                        TotalWhTax += item.WithholdingTax
                        TotalTotal += item.Total

                        gTotalVatableSales += item.VatableSales
                        gTotalZRSales += item.ZeroRatedSales
                        gTotalZREcozone += item.ZeroRatedEcoZone
                        gTotalVATonSales += item.VatOnSales
                        gTotalWhTax += item.WithholdingTax
                        gTotalTotal += item.Total

                        contentDetails(bdIndx, 0) = item.BillingPeriod & " - " & item.STLRun
                        contentDetails(bdIndx, 1) = item.Particulars
                        contentDetails(bdIndx, 2) = item.BuyerParticipant.FullName & "(" & item.BuyerParticipant.IDNumber & ")"
                        contentDetails(bdIndx, 3) = item.InvoiceNo
                        contentDetails(bdIndx, 4) = item.VatableSales
                        contentDetails(bdIndx, 5) = item.ZeroRatedSales
                        contentDetails(bdIndx, 6) = item.ZeroRatedEcoZone
                        contentDetails(bdIndx, 7) = item.VatOnSales
                        contentDetails(bdIndx, 8) = item.WithholdingTax
                        contentDetails(bdIndx, 9) = item.Total
                        contentDetails(bdIndx, 10) = item.BuyerInvoiceNo
                    End If
                    previousCollectionDate = item.CollectionDate
                Next

                bdIndx += 1
                contentDetails(bdIndx, 0) = ""
                contentDetails(bdIndx, 1) = "Grand Total: "
                contentDetails(bdIndx, 2) = ""
                contentDetails(bdIndx, 3) = ""
                contentDetails(bdIndx, 4) = TotalVatableSales
                contentDetails(bdIndx, 5) = TotalZRSales
                contentDetails(bdIndx, 6) = TotalZREcozone
                contentDetails(bdIndx, 7) = TotalVATonSales
                contentDetails(bdIndx, 8) = TotalWhTax
                contentDetails(bdIndx, 9) = TotalTotal
                contentDetails(bdIndx, 10) = ""

                xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                rowIndex += bdIndx
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 11), Excel.Range)
                xlContentDetails = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlContentDetails.Value = contentDetails
                Dim transDate As String = ""

                If dateFrom = dateTo Then
                    transDate = dateFrom.ToString("yyyyMMdd")
                Else
                    transDate = dateFrom.ToString("yyyyMMdd") & " - " & dateTo.ToString("yyyyMMdd")
                End If
                Dim FileName As String = "CSR_IEMOP_" & partifipantInfo.IDNumber.Replace("_FIT", "").ToString & "_" & "CR-N" & birParticipant.CollReportNumber.ToString("D7") & "_" & transDate

                xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbookMacroEnabled, misValue, misValue, misValue, misValue,
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
