Imports System.Text
Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Imports System.ComponentModel
Imports Microsoft.Office.Interop

Public Class PaymentTaggingHelper
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

    Private _ViewListOfPaymentTag As List(Of PaymentTag)
    Public Property ViewListOfPaymentTag() As List(Of PaymentTag)
        Get
            Return _ViewListOfPaymentTag
        End Get
        Set(ByVal value As List(Of PaymentTag))
            _ViewListOfPaymentTag = value
        End Set
    End Property

    Private _NewPaymentTag As PaymentTag
    Public Property NewPaymentTag() As PaymentTag
        Get
            Return _NewPaymentTag
        End Get
        Set(ByVal value As PaymentTag)
            _NewPaymentTag = value
        End Set
    End Property

    Private _FetchListPaymentTagDetails As List(Of PaymentTagDetails)
    Public Property FetchListPaymentTagDetails() As List(Of PaymentTagDetails)
        Get
            Return _FetchListPaymentTagDetails
        End Get
        Set(ByVal value As List(Of PaymentTagDetails))
            _FetchListPaymentTagDetails = value
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

#Region "Search By Date"
    Public Sub SearchByDateRangeForPaymentTagged(ByVal dFrom As Date, ByVal dTo As Date)
        Me._ViewListOfPaymentTag = GetListOfPaymentTag(dFrom, dTo)
    End Sub

    Private Function GetListOfPaymentTag(ByVal dateFrom As Date, ByVal dateTo As Date) As List(Of PaymentTag)
        Dim ret As New List(Of PaymentTag)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.*, B.PARTICIPANT_ID, B.FULL_NAME FROM AM_PAYMENT_TAG A " & vbNewLine _
                               & "LEFT JOIN AM_PARTICIPANTS B ON B.ID_NUMBER = A.ID_NUMBER " & vbNewLine _
                              & "WHERE A.REMITTANCE_DATE >= TO_DATE('" & dateFrom.ToShortDateString & "','MM/DD/YYYY') " & vbNewLine _
                              & "AND A.REMITTANCE_DATE <= TO_DATE('" & dateTo & "','MM/DD/YYYY') ORDER BY A.PAYMENT_TAG_NO, A.ID_NUMBER"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetListOfPaymentTag(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetListOfPaymentTag(ByVal dr As IDataReader) As List(Of PaymentTag)
        Dim result As New List(Of PaymentTag)

        Try
            While dr.Read()
                Dim item As New PaymentTag

                With dr
                    item.PaymentTagNo = CLng(.Item("PAYMENT_TAG_NO"))
                    item.RemittanceDate = CDate(.Item("REMITTANCE_DATE"))
                    item.BillingIDNumber = New AMParticipants(CStr(.Item("ID_NUMBER")), CStr(.Item("PARTICIPANT_ID")), CStr(.Item("FULL_NAME")))
                    item.TotalAmountTagged = CDec(.Item("TOTAL_AMOUNT_TAGGED"))
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

    Private Function GetListOfPaymentTagDetails(ByVal paymentTagNo As Long) As List(Of PaymentTagDetails)
        Dim ret As New List(Of PaymentTagDetails)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.*, B.INV_DM_CM, C.PARTICIPANT_ID, C.FULL_NAME, B.BILLING_PERIOD, D.STL_RUN FROM AM_PAYMENT_TAG_DETAILS A " & vbNewLine _
                               & "LEFT JOIN AM_WESM_BILL_SUMMARY B ON B.WESMBILL_SUMMARY_NO = A.WESMBILL_SUMMARY_NO " & vbNewLine _
                               & "LEFT JOIN AM_PARTICIPANTS C ON C.ID_NUMBER = B.ID_NUMBER " & vbNewLine _
                               & "LEFT JOIN (SELECT * FROM AM_WESM_BILL WHERE CHARGE_TYPE = 'E') D ON D.INVOICE_NO = B.INV_DM_CM " & vbNewLine _
                              & "WHERE A.PAYMENT_TAG_NO = " & paymentTagNo
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetListOfPaymentTagDetails(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetListOfPaymentTagDetails(ByVal dr As IDataReader) As List(Of PaymentTagDetails)
        Dim result As New List(Of PaymentTagDetails)

        Try
            While dr.Read()
                Dim item As New PaymentTagDetails

                With dr
                    item.WESMBillSummary = GetWESMBillSummaryItem(.Item("WESMBILL_SUMMARY_NO"))
                    item.NewDueDate = CDate(.Item("NEW_DUE_DATE").ToString())
                    item.EndingBalance = CDec(.Item("ENDING_BALANCE"))
                    item.AmountTagAlloc = CDec(.Item("AMOUNT_TAGALLOC"))
                    item.NewEndingBalance = CDec(.Item("NEW_ENDING_BALANCE"))
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

#Region "View Payment Tag"
    Public Function GetPaymentTag(ByVal paymentTagNo As Long) As PaymentTag
        Dim ret As New PaymentTag
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.*, B.PARTICIPANT_ID, B.FULL_NAME FROM AM_PAYMENT_TAG A " & vbNewLine _
                               & "LEFT JOIN AM_PARTICIPANTS B ON B.ID_NUMBER = A.ID_NUMBER " & vbNewLine _
                              & "WHERE A.PAYMENT_TAG_NO  = " & paymentTagNo

            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetPaymentTag(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetPaymentTag(ByVal dr As IDataReader) As PaymentTag
        Dim result As New PaymentTag

        Try
            While dr.Read()
                Dim item As New PaymentTag

                With dr
                    item.PaymentTagNo = CLng(.Item("PAYMENT_TAG_NO"))
                    item.RemittanceDate = CDate(.Item("REMITTANCE_DATE"))
                    item.BillingIDNumber = New AMParticipants(CStr(.Item("ID_NUMBER")), CStr(.Item("PARTICIPANT_ID")), CStr(.Item("FULL_NAME")))
                    item.TotalAmountTagged = CDec(.Item("TOTAL_AMOUNT_TAGGED"))
                    Dim getDetails As List(Of PaymentTagDetails) = GetListOfPaymentTagDetails(CLng(.Item("PAYMENT_TAG_NO")))
                    item.TagDetails = (From x In getDetails Where x.WESMBillSummary.BalanceType = EnumBalanceType.AP Select x).ToList
                    item.AllocDetails = (From x In getDetails Where x.WESMBillSummary.BalanceType = EnumBalanceType.AR Select x).ToList
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

#Region "Get Participant List with Payment Tagging"
    Public Function GetListOfParticipants() As List(Of String)
        Dim ret As New List(Of String)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT DISTINCT A.ID_NUMBER FROM AM_WESM_BILL_SUMMARY A " & vbNewLine _
                               & "INNER JOIN (SELECT DISTINCT WESMBILL_BATCH_NO FROM AM_WESM_BILL_SUMMARY WHERE ID_NUMBER = '" & AMModule.CompanyShortName.ToUpper & "') B ON B.WESMBILL_BATCH_NO = A.WESMBILL_BATCH_NO " & vbNewLine _
                               & "WHERE A.ENDING_BALANCE > 0 ORDER BY A.ID_NUMBER"
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

#End Region

#Region "Get WESM BILLS with AP Balance that should be paid by IEMOP"
    Public Sub GetWESMBillSummaryAP(ByVal idNumber As String)
        Me._FetchListPaymentTagDetails = GetWESMBillSummaryAPForPaymentTag(idNumber)
    End Sub

    Private Function GetWESMBillSummaryAPForPaymentTag(ByVal idNumber As String) As List(Of PaymentTagDetails)
        Dim ret As New List(Of PaymentTagDetails)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.*, B.PARTICIPANT_ID, B.FULL_NAME, B.PARTICIPANT_ADDRESS FROM AM_WESM_BILL_SUMMARY A " & vbNewLine _
                               & "INNER JOIN AM_PARTICIPANTS B ON B.ID_NUMBER = A.ID_NUMBER " & vbNewLine _
                               & "INNER JOIN (SELECT DISTINCT  WESMBILL_BATCH_NO FROM AM_WESM_BILL_SUMMARY WHERE ID_NUMBER = '" & AMModule.CompanyShortName.ToUpper & "') C ON C.WESMBILL_BATCH_NO = A.WESMBILL_BATCH_NO " & vbNewLine _
                               & "WHERE A.ENDING_BALANCE > 0 AND A.ID_NUMBER = '" & idNumber & "'"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetWESMBillSummaryAPForPaymentTag(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetWESMBillSummaryAPForPaymentTag(ByVal dr As IDataReader) As List(Of PaymentTagDetails)
        Dim result As New List(Of PaymentTagDetails)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As New PaymentTagDetails
                    item.WESMBillSummary = GetWESMBillSummaryItem(.Item("WESMBILL_SUMMARY_NO"))
                    item.NewDueDate = CDate(.Item("NEW_DUEDATE").ToString())
                    item.EndingBalance = item.WESMBillSummary.EndingBalance
                    item.NewDueDate = item.WESMBillSummary.NewDueDate
                    item.AmountTagAlloc = 0
                    item.NewEndingBalance = 0
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

    Private Function GetWESMBillSummaryARForCollectionAlloc(ByVal wesmBillBatchNo As Long) As List(Of PaymentTagDetails)
        Dim ret As New List(Of PaymentTagDetails)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT DISTINCT A.*, B.PARTICIPANT_ID, B.FULL_NAME, B.PARTICIPANT_ADDRESS " & vbNewLine _
                               & "FROM AM_WESM_BILL_SUMMARY A " & vbNewLine _
                               & "INNER JOIN AM_PARTICIPANTS B ON B.ID_NUMBER = A.ID_NUMBER " & vbNewLine _
                              & "WHERE A.ENDING_BALANCE < 0 AND A.ID_NUMBER = '" & AMModule.CompanyShortName & "' AND A.WESMBILL_BATCH_NO = " & wesmBillBatchNo

            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetWESMBillSummaryARForCollectionAlloc(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetWESMBillSummaryARForCollectionAlloc(ByVal dr As IDataReader) As List(Of PaymentTagDetails)
        Dim result As New List(Of PaymentTagDetails)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As New PaymentTagDetails
                    item.WESMBillSummary = GetWESMBillSummaryItem(.Item("WESMBILL_SUMMARY_NO"))
                    item.EndingBalance = item.WESMBillSummary.EndingBalance
                    item.NewDueDate = item.WESMBillSummary.NewDueDate
                    item.AmountTagAlloc = 0
                    item.NewEndingBalance = 0
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

    Public Function GetWESMBillSummaryItem(ByVal WESMBillNo As Long) As WESMBillSummary
        Dim result As New WESMBillSummary
        Dim report As New DataReport
        Try
            Dim SQL As String

            SQL = "SELECT A.ID_NUMBER, A.ID_TYPE, A.GROUP_NO, B.PARTICIPANT_ID, A.billing_period, A.TRANSACTION_DATE, A.ENERGY_WITHHOLD, " & vbNewLine &
                                  "b.participant_address, b.city, b.province, b.zip_code, " & vbNewLine &
                                  "A.charge_type, A.due_date, A.ENDING_BALANCE, a.BEGINNING_BALANCE, a.NEW_DUEDATE, a.IS_MFWTAX_DEDUCTED, " & vbNewLine &
                                  "A.INV_DM_CM, A.SUMMARY_TYPE, a.WESMBILL_SUMMARY_NO, A.ADJUSTMENT, A.WESMBILL_BATCH_NO, A.ENERGY_WITHHOLD_STATUS, a.NO_OFFSET, A.NO_SOA, A.NO_DEFINT, c.remarks, A.BALANCE_TYPE " & vbNewLine &
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
                    item.BalanceType = CType(System.Enum.Parse(GetType(EnumBalanceType), CStr(.Item("BALANCE_TYPE").ToString())), EnumBalanceType)
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

#Region "Process and Create Payment Tagging in AP and then Allocate to AR"
    Public Sub GenerateNewPaymentTagging(ByVal remittanceDate As Date, ByVal participantId As String, ByVal listOfPaymentTagged As List(Of PaymentTagDetails))
        Dim newPaymentTag As New PaymentTag
        Dim participantInfo As AMParticipants = WBillHelper.GetAMParticipants(participantId).FirstOrDefault
        With newPaymentTag
            .PaymentTagNo = 0
            .BillingIDNumber = participantInfo
            .RemittanceDate = remittanceDate
            .TotalAmountTagged = (From x In listOfPaymentTagged Select x.AmountTagAlloc).Sum()
            .TagDetails = listOfPaymentTagged
            .AllocDetails = AllocateToARFromTaggedAP(listOfPaymentTagged)
        End With
        Me._NewPaymentTag = newPaymentTag
    End Sub

    Private Function AllocateToARFromTaggedAP(ByVal listOfPaymentTagged As List(Of PaymentTagDetails)) As List(Of PaymentTagDetails)
        Dim ret As New List(Of PaymentTagDetails)
        Dim getListWESMBillBatchNo As List(Of Long) = (From x In listOfPaymentTagged Select x.WESMBillSummary.WESMBillBatchNo Distinct).ToList
        For Each item In getListWESMBillBatchNo
            Dim getListOfAPPaymentTagged As List(Of PaymentTagDetails) = (From x In listOfPaymentTagged Where x.WESMBillSummary.WESMBillBatchNo = item Select x).ToList
            Dim getListOfARCollectedAlloc As List(Of PaymentTagDetails) = Me.GetWESMBillSummaryARForCollectionAlloc(item)
            For Each i In AllocateTaggedWHTaxCertPerBatch(getListOfAPPaymentTagged, getListOfARCollectedAlloc)
                ret.Add(i)
            Next
        Next
        Return ret
    End Function

    Private Function AllocateTaggedWHTaxCertPerBatch(ByVal listOfAPPaymentTagged As List(Of PaymentTagDetails), ByRef listOfARCollectionAlloc As List(Of PaymentTagDetails)) As List(Of PaymentTagDetails)
        Dim ret As New List(Of PaymentTagDetails)

        Dim getListOfAPPaymentTaggedInEV = (From x In listOfAPPaymentTagged Where x.WESMBillSummary.ChargeType = EnumChargeType.EV Select x).ToList
        Dim getListOfARCollectionAllocInEV = (From x In listOfARCollectionAlloc Where x.WESMBillSummary.ChargeType = EnumChargeType.EV Select x).ToList

        If getListOfAPPaymentTaggedInEV.Count <> 0 Then
            Dim getTotalPaymentTaggedInEV As Decimal = (From x In getListOfAPPaymentTaggedInEV Select x.AmountTagAlloc).Sum()
            Dim getTtotalARAmountInEV As Decimal = (From x In getListOfARCollectionAllocInEV Select x.WESMBillSummary.BeginningBalance).Sum()

            For Each item In getListOfARCollectionAllocInEV
                Dim shareAmount As Decimal = Math.Round(getTotalPaymentTaggedInEV * (item.WESMBillSummary.BeginningBalance / getTtotalARAmountInEV), 2)
                item.AmountTagAlloc = shareAmount
            Next

            Dim getTotalAllocatedAmountInAREV As Decimal = (From x In getListOfARCollectionAllocInEV Select x.AmountTagAlloc).Sum

            If getTotalPaymentTaggedInEV <> getTotalAllocatedAmountInAREV Then
                Dim getDiff As Decimal = Math.Round(getTotalPaymentTaggedInEV - getTotalAllocatedAmountInAREV, 2)
                Dim getMaxWHTaxAmount As PaymentTagDetails = (From x In getListOfARCollectionAllocInEV Select x Order By x.AmountTagAlloc Ascending).First
                getMaxWHTaxAmount.AmountTagAlloc += getDiff
            End If

        End If

        Dim getListOfAPPaymentTaggedInEnergy = (From x In listOfAPPaymentTagged Where x.WESMBillSummary.ChargeType = EnumChargeType.E Select x).ToList
        Dim getListOfARCollectionAllocInEnergy = (From x In listOfARCollectionAlloc Where x.WESMBillSummary.ChargeType = EnumChargeType.E Select x).ToList

        If getListOfAPPaymentTaggedInEnergy.Count <> 0 Then
            Dim getTotalPaymentTaggedInEnergy As Decimal = (From x In getListOfAPPaymentTaggedInEnergy Select x.AmountTagAlloc).Sum()
            Dim getTotalARAmountInEnergy As Decimal = (From x In getListOfARCollectionAllocInEnergy Select x.WESMBillSummary.BeginningBalance).Sum()

            For Each item In getListOfARCollectionAllocInEnergy
                Dim shareAmount As Decimal = Math.Round(getTotalPaymentTaggedInEnergy * (item.WESMBillSummary.BeginningBalance / getTotalARAmountInEnergy), 2)
                item.AmountTagAlloc = shareAmount
            Next

            Dim getTotalAllocatedAmountInAREnergy As Decimal = (From x In getListOfARCollectionAllocInEnergy Select x.AmountTagAlloc).Sum

            If getTotalPaymentTaggedInEnergy <> getTotalAllocatedAmountInAREnergy Then
                Dim getDiff As Decimal = Math.Round(getTotalPaymentTaggedInEnergy - getTotalAllocatedAmountInAREnergy, 2)
                Dim getMaxWHTaxAmount As PaymentTagDetails = (From x In getListOfARCollectionAllocInEnergy Select x Order By x.AmountTagAlloc Ascending).First
                getMaxWHTaxAmount.AmountTagAlloc += getDiff
            End If
        End If

        ret = listOfARCollectionAlloc
        Return ret
    End Function
#End Region

#Region "Save"
    Public Sub SavePaymentTag()
        Dim listSQL As New List(Of String)
        Dim SysDateTime As Date = WBillHelper.GetSystemDateTime()
        Dim SQL As String = ""
        Dim newPaymentTagNo As Long = GetNextValSequence("SEQ_AM_PAY_TAG_NO")
        With Me.NewPaymentTag
            .PaymentTagNo = newPaymentTagNo
            SQL = "INSERT INTO AM_PAYMENT_TAG (PAYMENT_TAG_NO,REMITTANCE_DATE,ID_NUMBER,TOTAL_AMOUNT_TAGGED,UPDATED_BY,UPDATED_DATE) " & vbNewLine _
                & "SELECT " & newPaymentTagNo & ", TO_DATE('" & .RemittanceDate & "','MM/DD/YYYY'), '" & .BillingIDNumber.IDNumber & "', " & .TotalAmountTagged & vbNewLine _
                & ", '" & AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') FROM DUAL"
            listSQL.Add(SQL)

            For Each item In .TagDetails
                SQL = "INSERT INTO AM_PAYMENT_TAG_DETAILS (PAYMENT_TAG_NO,WESMBILL_SUMMARY_NO,ENDING_BALANCE,AMOUNT_TAGALLOC,NEW_ENDING_BALANCE,NEW_DUE_DATE)" & vbNewLine _
                    & "SELECT " & newPaymentTagNo & ", " & item.WESMBillSummary.WESMBillSummaryNo & ", " & item.EndingBalance & ", " & item.AmountTagAlloc & ", " & item.NewEndingBalance & ", TO_DATE('" & item.NewDueDate & "', 'MM/dd/yyyy') FROM DUAL"
                listSQL.Add(SQL)

                'Updating AM_WESM_BILL_SUMMARY
                SQL = "UPDATE AM_WESM_BILL_SUMMARY SET ENDING_BALANCE = ENDING_BALANCE - " & item.AmountTagAlloc & ", " &
                                                      "NEW_DUEDATE = TO_DATE('" & .RemittanceDate.ToShortDateString & "','mm/dd/yyyy'), " &
                                                      "TRANSACTION_DATE = TO_DATE('" & .RemittanceDate.ToShortDateString & "','mm/dd/yyyy'), " &
                                                      "UPDATED_DATE = TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), UPDATED_BY = '" & AMModule.UserName & "'" &
                                                      "WHERE WESMBILL_SUMMARY_NO = '" & item.WESMBillSummary.WESMBillSummaryNo & "'"
                listSQL.Add(SQL)
                If item.WESMBillSummary.ChargeType = EnumChargeType.E Then
                    'Add AM_WESM_BILL_SUMMARY_HISTORY
                    SQL = "INSERT INTO AM_WESM_BILL_SUMMARY_HISTORY (WESMBILL_SUMMARY_NO, DUE_DATE, AMOUNT, UPDATED_BY, UPDATED_DATE, PAYMENT_TYPE, PAYMENT_TAG_NO) " &
                        "SELECT '" & item.WESMBillSummary.WESMBillSummaryNo & "', TO_DATE('" & .RemittanceDate & "','MM/DD/YYYY'), " & item.AmountTagAlloc & ", '" &
                        AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & EnumPaymentNewType.Energy & "', '" & newPaymentTagNo & "' FROM DUAL"
                ElseIf item.WESMBillSummary.ChargeType = EnumChargeType.EV Then
                    'Add AM_WESM_BILL_SUMMARY_HISTORY
                    SQL = "INSERT INTO AM_WESM_BILL_SUMMARY_HISTORY (WESMBILL_SUMMARY_NO, DUE_DATE, AMOUNT, UPDATED_BY, UPDATED_DATE, PAYMENT_TYPE, PAYMENT_TAG_NO) " &
                        "SELECT '" & item.WESMBillSummary.WESMBillSummaryNo & "', TO_DATE('" & .RemittanceDate & "','MM/DD/YYYY'), " & item.AmountTagAlloc & ", '" &
                        AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & EnumPaymentNewType.VatOnEnergy & "', '" & newPaymentTagNo & "' FROM DUAL"
                End If

                listSQL.Add(SQL)

            Next

            For Each item In .AllocDetails
                SQL = "INSERT INTO AM_PAYMENT_TAG_DETAILS (PAYMENT_TAG_NO,WESMBILL_SUMMARY_NO,ENDING_BALANCE,AMOUNT_TAGALLOC,NEW_ENDING_BALANCE,NEW_DUE_DATE)" & vbNewLine _
                    & "SELECT " & newPaymentTagNo & ", " & item.WESMBillSummary.WESMBillSummaryNo & ", " & item.EndingBalance & ", " & item.AmountTagAlloc & ", " & item.NewEndingBalance & ", TO_DATE('" & item.NewDueDate & "', 'MM/dd/yyyy') FROM DUAL"
                listSQL.Add(SQL)

                'Updating AM_WESM_BILL_SUMMARY
                SQL = "UPDATE AM_WESM_BILL_SUMMARY SET ENDING_BALANCE = ENDING_BALANCE - " & item.AmountTagAlloc & ", " &
                                                      "UPDATED_DATE = TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), UPDATED_BY = '" & AMModule.UserName & "' " &
                                                      "WHERE WESMBILL_SUMMARY_NO = '" & item.WESMBillSummary.WESMBillSummaryNo & "'"
                listSQL.Add(SQL)
                If item.WESMBillSummary.ChargeType = EnumChargeType.E Then
                    'Add AM_WESM_BILL_SUMMARY_HISTORY
                    SQL = "INSERT INTO AM_WESM_BILL_SUMMARY_HISTORY (WESMBILL_SUMMARY_NO, DUE_DATE, AMOUNT, UPDATED_BY, UPDATED_DATE, COLLECTION_TYPE, PAYMENT_TAG_NO) " &
                        "SELECT '" & item.WESMBillSummary.WESMBillSummaryNo & "', TO_DATE('" & .RemittanceDate & "','MM/DD/YYYY'), " & item.AmountTagAlloc & ", '" &
                        AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & EnumCollectionType.Energy & "', '" & newPaymentTagNo & "' FROM DUAL"
                ElseIf item.WESMBillSummary.ChargeType = EnumChargeType.EV Then
                    'Add AM_WESM_BILL_SUMMARY_HISTORY
                    SQL = "INSERT INTO AM_WESM_BILL_SUMMARY_HISTORY (WESMBILL_SUMMARY_NO, DUE_DATE, AMOUNT, UPDATED_BY, UPDATED_DATE, COLLECTION_TYPE, PAYMENT_TAG_NO) " &
                        "SELECT '" & item.WESMBillSummary.WESMBillSummaryNo & "', TO_DATE('" & .RemittanceDate & "','MM/DD/YYYY'), " & item.AmountTagAlloc & ", '" &
                        AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & EnumCollectionType.VatOnEnergy & "', '" & newPaymentTagNo & "' FROM DUAL"
                End If

                listSQL.Add(SQL)
            Next

            'Delete AM_STL_NOTICE_NEW
            SQL = "DELETE FROM AM_STL_NOTICE_NEW WHERE STL_NOTICE_DATE = LAST_DAY(TO_DATE('" & FormatDateTime(.RemittanceDate, DateFormat.ShortDate) & "','mm/dd/yyyy'))"
            listSQL.Add(SQL)

            'Insert into AM_STL_NOTICE_NEW
            SQL = "INSERT INTO AM_STL_NOTICE_NEW(STL_NOTICE_DATE, ENDING_BALANCE, WESMBILL_SUMMARY_NO) " &
                      "(SELECT LAST_DAY(TO_DATE('" & FormatDateTime(.RemittanceDate, DateFormat.ShortDate) & "','mm/dd/yyyy')) AS TRANSACTIONDATE, ENDING_BALANCE, WESMBILL_SUMMARY_NO " &
                      " FROM AM_WESM_BILL_SUMMARY WHERE ENDING_BALANCE <> 0 OR TO_CHAR(NEW_DUEDATE,'MM/YYYY') = TO_CHAR(TO_DATE('" & FormatDateTime(.RemittanceDate, DateFormat.ShortDate) & "','MM/DD/YYYY'),'MM/YYYY'))"

            listSQL.Add(SQL)

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


End Class
