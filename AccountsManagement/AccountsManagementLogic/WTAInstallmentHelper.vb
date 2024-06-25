Imports System.Text
Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Imports System.ComponentModel
Imports Microsoft.Office.Interop
Imports System.Threading
Imports System.Threading.Tasks
Imports System.IO

Public Class WTAInstallmentHelper
    Public Sub New()
        'Get the current instance of the dal
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._DataAccess = DAL.GetInstance()
    End Sub

#Region "DAL Instance"
    Private _DataAccess As DAL
    Public ReadOnly Property DataAccess() As DAL
        Get
            Return Me._DataAccess
        End Get
    End Property
#End Region

#Region "WESMBillHelper Instance"
    Private _WBillHelper As WESMBillHelper
    Public ReadOnly Property WBillHelper() As WESMBillHelper
        Get
            Return _WBillHelper
        End Get
    End Property
#End Region

#Region "Property of List of WTA Installment"
    Private _ListofWTAInstallment As List(Of WTAInstallment)
    Public ReadOnly Property ListofWTAInstallment() As List(Of WTAInstallment)
        Get
            Return _ListofWTAInstallment
        End Get
    End Property
#End Region

#Region "Property of Newly addded WTA Installment"
    Private _NewlyAddedWTAInstallment As WTAInstallment
    Public ReadOnly Property NewlyAddedWTAInstallment() As WTAInstallment
        Get
            Return _NewlyAddedWTAInstallment
        End Get
    End Property
#End Region

#Region "Untag paid in WTA Installment"
    Public Async Function UntagPaidWTAInstallmentHistory(ByVal listofWTAInstallment As List(Of WTAInstallment),
                                                         ByVal listofWTAInstallmentHis As List(Of WTAInstallmentHistory)) As Task
        Try
            Dim listSQL As New List(Of String)
            Dim sQL As String = ""
            For Each item In listofWTAInstallmentHis
                Dim getWTAInstallment As WTAInstallment = listofWTAInstallment.Where(Function(x) x.WTAINO = item.WTAINO).FirstOrDefault
                If Not getWTAInstallment Is Nothing Then
                    Dim getWTAInstallmentTerm As WTAInstallmentTerms = getWTAInstallment.ListofWTAInstallmentTerm _
                                                                       .Where(Function(x) x.Term = item.Term).FirstOrDefault()
                    Dim untagPaidAmount As Decimal = getWTAInstallmentTerm.PaidAmount - item.PaidAmount
                    Dim untagDefaultAmount As Decimal = getWTAInstallmentTerm.DefaultAmount - item.DefaultAmount

                    If untagPaidAmount < 0 Then
                        Throw New Exception("Cannot untag Paid Amount becoming negative!")
                    End If
                    If untagDefaultAmount < 0 Then
                        Throw New Exception("Cannot untag Default Amount becoming negative!")
                    End If

                    sQL = "UPDATE AM_WTA_INSTALLMENT_DETAILS SET PAID_AMOUNT = " & untagPaidAmount & ", " & vbNewLine _
                        & "DEFAULT_AMOUNT = " & untagDefaultAmount & ", TERM_STATUS = " & EnumWTAInstallmentTermStatus.Unpaid & ", " & vbNewLine _
                        & "UPDATED_BY = '" & AMModule.UserName & "', UPDATED_DATETIME = SYSDATE " & vbNewLine _
                        & "WHERE TERM = " & item.Term & " AND WTAI_NO = " & item.WTAINO
                    listSQL.Add(sQL)

                    sQL = "UPDATE AM_WTA_INSTALLMENT " & vbNewLine _
                        & "SET STATUS = " & EnumWTAInstallmentStatus.OnGoing & " " & vbNewLine _
                        & "WHERE WTAI_NO = " & item.WTAINO
                    listSQL.Add(sQL)

                    sQL = "DELETE FROM AM_WTA_INSTALLMENT_HISTORY WHERE TERM = " & item.Term & " AND COLLECTION_NO = " & item.CollectionNo
                    listSQL.Add(sQL)
                End If
            Next
            Await SaveToDBAsync(listSQL)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Function

#End Region

#Region "Get the WTA Installment History"
    Public Async Function GetWTAInstallmentHisAsync(ByVal listofWTAInstallment As List(Of WTAInstallment),
                                                    ByVal collectionDate As Date,
                                                    ByVal collectionNo As Long) As Task(Of List(Of WTAInstallmentHistory))
        Dim ret As New List(Of WTAInstallmentHistory)
        Dim report As New DataReport
        Try
            Dim getWTAINos As String = String.Join(",", listofWTAInstallment.Select(Function(x) x.WTAINO).ToList())

            Dim SQL As String = "SELECT * FROM AM_WTA_INSTALLMENT_HISTORY " & vbNewLine _
                              & "WHERE WTAI_NO IN (" & getWTAINos & ") " & vbNewLine _
                              & "And COLLECTION_DATE = TO_DATE('" & collectionDate & "','MM/DD/YYYY') " & vbNewLine _
                              & "AND COLLECTION_NO = " & collectionNo

            report = Await Me.DataAccess.ExecuteSelectQueryReturningDataReaderAsync(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Await Me.GetWTAInstallmentHisAsync(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetWTAInstallmentHisAsync(ByVal dr As IDataReader) As Task(Of List(Of WTAInstallmentHistory))
        Dim result As New List(Of WTAInstallmentHistory)
        Try
            While dr.Read()
                With dr
                    Dim item As New WTAInstallmentHistory
                    item.Term = CInt(.Item("TERM"))
                    item.TermNewDueDate = CDate(.Item("TERM_NEW_DUEDATE"))
                    item.PaidAmount = CDec(.Item("PAID_AMOUNT"))
                    item.DefaultAmount = CDec(.Item("DEFAULT_AMOUNT"))
                    item.CollectionDate = CDate(.Item("COLLECTION_DATE"))
                    item.WTAINO = CLng(.Item("WTAI_NO"))
                    item.CollectionNo = CLng(.Item("COLLECTION_NO"))
                    result.Add(item)
                End With
            End While
        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try
        Return Task.FromResult(result)
    End Function

#End Region

#Region "Get the WTA Installment"
    Public Async Function GetWTAInstallmentByWBSNoAsync(ByVal wbsNo As Long) As Task(Of WTAInstallment)
        Dim ret As WTAInstallment
        Dim report As New DataReport
        Try
            Dim SQL As String = "WITH WBS AS (SELECT * FROM AM_WESM_BILL_SUMMARY WHERE WESMBILL_SUMMARY_NO = " & wbsNo & " AND CHARGE_TYPE IN ('E','EV')), " & vbNewLine _
                                   & "WTAI As (Select * FROM AM_WTA_INSTALLMENT WHERE WESMBILL_SUMMARY_NO = " & wbsNo & "), " & vbNewLine _
                                   & "TP AS (SELECT * FROM AM_PARTICIPANTS) " & vbNewLine _
                                   & "SELECT WBS.*, WTAI.*, TP.PARTICIPANT_ID " & vbNewLine _
                                   & "FROM WBS " & vbNewLine _
                                   & "INNER JOIN WTAI ON WTAI.WESMBILL_SUMMARY_NO = WBS.WESMBILL_SUMMARY_NO " & vbNewLine _
                                   & "LEFT JOIN TP ON TP.ID_NUMBER = WBS.ID_NUMBER"

            report = Await Me.DataAccess.ExecuteSelectQueryReturningDataReaderAsync(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Await Me.GetWTAInstallmentByWBSNoAsync(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Async Function GetWTAInstallmentByWBSNoAsync(ByVal dr As IDataReader) As Task(Of WTAInstallment)
        Dim result As New WTAInstallment
        Try
            While dr.Read()
                With dr
                    result.WTAINO = CLng(.Item("WTAI_NO"))
                    result.STLRun = CStr(.Item("STL_RUN"))
                    result.WESMBillSummaryNo = CLng(.Item("WESMBILL_SUMMARY_NO"))
                    result.Terms = CInt(.Item("TERMS"))
                    result.Status = CType(System.Enum.Parse(GetType(EnumWTAInstallmentStatus), CStr(.Item("STATUS"))), EnumWTAInstallmentStatus)

                    result.IDNumber = New AMParticipants(CStr(.Item("ID_NUMBER")), CStr(.Item("PARTICIPANT_ID")))
                    result.BillPeriod = CInt(.Item("BILLING_PERIOD"))
                    result.ChargeType = CType(System.Enum.Parse(GetType(EnumChargeType), CStr(.Item("CHARGE_TYPE"))), EnumChargeType)
                    result.DueDate = CDate(.Item("DUE_DATE"))
                    result.BeginningBalance = CDec(.Item("BEGINNING_BALANCE"))
                    result.EndingBalance = CDec(.Item("ENDING_BALANCE"))
                    result.NewDueDate = CDate(.Item("NEW_DUEDATE"))
                    result.IsMFWTaxDeducted = CInt(.Item("IS_MFWTAX_DEDUCTED"))
                    result.INVDMCMNo = CStr(.Item("INV_DM_CM"))
                    result.SummaryType = CType(System.Enum.Parse(GetType(EnumSummaryType), CStr(.Item("SUMMARY_TYPE"))), EnumSummaryType)
                    result.EnergyWithhold = If(CInt(.Item("ENERGY_WITHHOLD_STATUS")) = 2, 0D, CDec(.Item("ENERGY_WITHHOLD")))
                    result.EnergyWithholdStatus = CType(.Item("ENERGY_WITHHOLD_STATUS"), EnumEnergyWithholdStatus)
                    result.NoDefInt = CBool(IIf(CInt(.Item("NO_DEFINT")) = 1, True, False))
                End With
            End While

            If result.WTAINO <> 0 Then
                Dim getWTAInstallmentDetails As List(Of WTAInstallmentTerms) = Await GetWTAInstallmentDetailsByWBSNoAsync(result.WTAINO)
                result.ListofWTAInstallmentTerm = getWTAInstallmentDetails
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

    Public Async Function GetWTAInstallmentDetailsByWBSNoAsync(ByVal wtaiNo As Long) As Task(Of List(Of WTAInstallmentTerms))
        Dim ret As New List(Of WTAInstallmentTerms)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT * FROM AM_WTA_INSTALLMENT_DETAILS " & vbNewLine _
                              & "WHERE WTAI_NO = " & wtaiNo & " " & vbNewLine _
                              & "ORDER BY TERM"

            report = Await Me.DataAccess.ExecuteSelectQueryReturningDataReaderAsync(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Await Me.GetWTAInstallmentDetailsByWBSNoAsync(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetWTAInstallmentDetailsByWBSNoAsync(ByVal dr As IDataReader) As Task(Of List(Of WTAInstallmentTerms))
        Dim result As New List(Of WTAInstallmentTerms)
        Try
            While dr.Read()
                With dr
                    Dim item As New WTAInstallmentTerms
                    item.Term = CInt(.Item("TERM"))
                    item.TermDueDate = CDate(.Item("TERM_DUEDATE"))
                    item.TermNewDueDate = CDate(.Item("TERM_NEW_DUEDATE"))
                    item.TermAmount = CDec(.Item("TERM_AMOUNT"))
                    item.PaidAmount = CDec(.Item("PAID_AMOUNT"))
                    item.DefaultAmount = CDec(.Item("DEFAULT_AMOUNT"))
                    item.TermStatus = CType(.Item("TERM_STATUS"), EnumWTAInstallmentTermStatus)
                    item.WTAINO = CLng(.Item("WTAI_NO"))
                    result.Add(item)
                End With
            End While
        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return Task.FromResult(result)
    End Function
#End Region

#Region "Get the List of Billing Period and STL Run wiht WTA Staggered/Installment Payments"
    Public Async Function GetListofBPSTLWTAInstallmentAsync() As Task(Of Dictionary(Of Integer, String))
        Dim ret As New Dictionary(Of Integer, String)
        Dim report As New DataReport
        Try
            Dim SQL As String = "WITH WBS AS (SELECT * FROM AM_WESM_BILL_SUMMARY WHERE CHARGE_TYPE IN ('E','EV')), " & vbNewLine _
                                   & "WTAI As (Select * FROM AM_WTA_INSTALLMENT) " & vbNewLine _
                                   & "SELECT WBS.BILLING_PERIOD, WTAI.STL_RUN, COUNT(WBS.ID_NUMBER) AS COUNT_ID_NUMBER" & vbNewLine _
                                   & "FROM WTAI " & vbNewLine _
                                   & "INNER JOIN WBS ON WBS.WESMBILL_SUMMARY_NO = WTAI.WESMBILL_SUMMARY_NO " & vbNewLine _
                                   & "GROUP BY WBS.BILLING_PERIOD, WTAI.STL_RUN"

            report = Await Me.DataAccess.ExecuteSelectQueryReturningDataReaderAsync(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Await Me.GetListofBPSTLWTAInstallmentAsync(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetListofBPSTLWTAInstallmentAsync(ByVal dr As IDataReader) As Task(Of Dictionary(Of Integer, String))
        Dim result As New Dictionary(Of Integer, String)
        Try
            While dr.Read()
                With dr
                    Dim key As Integer = CInt(.Item("BILLING_PERIOD"))
                    Dim item As String = CStr(.Item("STL_RUN"))
                    If Not result.ContainsKey(key) Then
                        result.Add(key, item)
                    Else
                        result.Item(key) = String.Join(result.Item(key), item)
                    End If
                End With
            End While
        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return Task.FromResult(result)
    End Function
#End Region

#Region "Get the List of WTA Installment"
    Public Async Function GetListofWTAInstallmentAsync(ByVal bp As Integer, ByVal stlrun As String) As Task(Of List(Of WTAInstallment))
        Dim ret As New List(Of WTAInstallment)
        Dim report As New DataReport
        Try
            Dim SQL As String = "WITH WBS AS (SELECT * FROM AM_WESM_BILL_SUMMARY WHERE CHARGE_TYPE IN ('E','EV')), " & vbNewLine _
                                   & "WTAI As (Select * FROM AM_WTA_INSTALLMENT), " & vbNewLine _
                                   & "TP AS (SELECT * FROM AM_PARTICIPANTS) " & vbNewLine _
                                   & "SELECT WBS.*, WTAI.*, TP.PARTICIPANT_ID " & vbNewLine _
                                   & "FROM WTAI " & vbNewLine _
                                   & "INNER JOIN WBS ON WBS.WESMBILL_SUMMARY_NO = WTAI.WESMBILL_SUMMARY_NO " & vbNewLine _
                                   & "LEFT JOIN TP ON TP.ID_NUMBER = WBS.ID_NUMBER " & vbNewLine _
                                   & "WHERE WTAI.STL_RUN = '" & stlrun & "' AND WBS.BILLING_PERIOD = " & bp

            report = Await Me.DataAccess.ExecuteSelectQueryReturningDataReaderAsync(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Await Me.GetWTAInstallmentAsync(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Public Async Function GetWTAInstallmentAsync(ByVal selectedDueDate As Date) As Task(Of List(Of WTAInstallment))
        Dim ret As New List(Of WTAInstallment)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT A.*,B.* FROM AM_WESM_BILL_SUMMARY A " & vbNewLine _
                              & "INNER JOIN AM_WTA_INSTALLMENT B ON B.WESMBILL_SUMMARY_NO = A.WESMBILL_SUMMARY_NO " & vbNewLine _
                              & "WHERE A.DUE_DATE = TO_DATE('" & selectedDueDate.ToString("MM/dd/yyyy") & "', 'MM/DD/YYYY')"

            report = Await Me.DataAccess.ExecuteSelectQueryReturningDataReaderAsync(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Await Me.GetWTAInstallmentAsync(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Async Function GetWTAInstallmentAsync(ByVal dr As IDataReader) As Task(Of List(Of WTAInstallment))
        Dim result As New List(Of WTAInstallment)
        Try
            While dr.Read()
                With dr
                    Dim item As New WTAInstallment
                    item.WTAINO = CLng(.Item("WTAI_NO"))
                    item.STLRun = CStr(.Item("STL_RUN"))
                    item.WESMBillSummaryNo = CLng(.Item("WESMBILL_SUMMARY_NO"))
                    item.Terms = CInt(.Item("TERMS"))
                    item.Status = CType(System.Enum.Parse(GetType(EnumWTAInstallmentStatus), CStr(.Item("STATUS"))), EnumWTAInstallmentStatus)

                    item.IDNumber = New AMParticipants(CStr(.Item("ID_NUMBER")), CStr(.Item("PARTICIPANT_ID")))
                    item.BillPeriod = CInt(.Item("BILLING_PERIOD"))
                    item.ChargeType = CType(System.Enum.Parse(GetType(EnumChargeType), CStr(.Item("CHARGE_TYPE"))), EnumChargeType)
                    item.DueDate = CDate(.Item("DUE_DATE"))
                    item.BeginningBalance = CDec(.Item("BEGINNING_BALANCE"))
                    item.EndingBalance = CDec(.Item("ENDING_BALANCE"))
                    item.NewDueDate = CDate(.Item("NEW_DUEDATE"))
                    item.IsMFWTaxDeducted = CInt(.Item("IS_MFWTAX_DEDUCTED"))
                    item.INVDMCMNo = CStr(.Item("INV_DM_CM"))
                    item.SummaryType = CType(System.Enum.Parse(GetType(EnumSummaryType), CStr(.Item("SUMMARY_TYPE"))), EnumSummaryType)
                    item.EnergyWithhold = If(CInt(.Item("ENERGY_WITHHOLD_STATUS")) = 2, 0D, CDec(.Item("ENERGY_WITHHOLD")))
                    item.EnergyWithholdStatus = CType(.Item("ENERGY_WITHHOLD_STATUS"), EnumEnergyWithholdStatus)
                    item.NoDefInt = CBool(IIf(CInt(.Item("NO_DEFINT")) = 1, True, False))
                    result.Add(item)
                End With
            End While
            Dim getMinID As Long = result.Select(Function(x) x.WTAINO).Min()
            Dim getMaxID As Long = result.Select(Function(x) x.WTAINO).Max()
            Dim getWTAInstallmentDetails As List(Of WTAInstallmentTerms) = Await GetWTAInstallmentDetailsAsync(getMinID, getMaxID)
            For Each item In result
                Dim getWTAIDetails As List(Of WTAInstallmentTerms) = getWTAInstallmentDetails.Where(Function(x) x.WTAINO = item.WTAINO).ToList()
                item.ListofWTAInstallmentTerm = getWTAIDetails
            Next
        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return result
    End Function

    Public Async Function GetWTAInstallmentDetailsAsync(ByVal minID As Long, ByVal maxID As Long) As Task(Of List(Of WTAInstallmentTerms))
        Dim ret As New List(Of WTAInstallmentTerms)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT A.* FROM AM_WTA_INSTALLMENT_DETAILS A " & vbNewLine _
                              & "WHERE A.WTAI_NO >= " & minID & " AND A.WTAI_NO <= " & maxID

            report = Await Me.DataAccess.ExecuteSelectQueryReturningDataReaderAsync(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Await Me.GetWTAInstallmentDetailsAsync(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetWTAInstallmentDetailsAsync(ByVal dr As IDataReader) As Task(Of List(Of WTAInstallmentTerms))
        Dim result As New List(Of WTAInstallmentTerms)
        Try
            While dr.Read()
                With dr
                    Dim item As New WTAInstallmentTerms
                    item.Term = CInt(.Item("TERM"))
                    item.TermDueDate = CDate(.Item("TERM_DUEDATE"))
                    item.TermNewDueDate = CDate(.Item("TERM_NEW_DUEDATE"))
                    item.TermAmount = CDec(.Item("TERM_AMOUNT"))
                    item.PaidAmount = CDec(.Item("PAID_AMOUNT"))
                    item.DefaultAmount = CDec(.Item("DEFAULT_AMOUNT"))
                    item.TermStatus = CType(.Item("TERM_STATUS"), EnumWTAInstallmentTermStatus)
                    item.WTAINO = CLng(.Item("WTAI_NO"))
                    result.Add(item)
                End With
            End While
        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return Task.FromResult(result)
    End Function
#End Region

#Region "Get the List of WESMBill and WESMBillSummary for creation of WTA Installment"
    Public Async Function GetWESMBillSummaryByBPSTLAsync(ByVal billingperiod As Integer, ByVal settlementrun As String) As Task(Of List(Of WTAInstallment))
        Dim result As New List(Of WTAInstallment)
        Dim report As New DataReport
        Dim SQL As String
        Try
            SQL = "WITH WBS AS (SELECT * FROM AM_WESM_BILL_SUMMARY WHERE CHARGE_TYPE IN ('E','EV') AND DUE_DATE >= TO_DATE('" & AMModule.WTAInstallmentStartDate & "', 'MM-DD-YYYY') AND BALANCE_TYPE = 'AR'), " & vbNewLine _
                & "WB As (Select * FROM AM_WESM_BILL WHERE CHARGE_TYPE IN ('E','EV') AND DUE_DATE >= TO_DATE('" & AMModule.WTAInstallmentStartDate & "', 'MM-DD-YYYY') AND " & vbNewLine _
                       & "WHERE BILLING_PERIOD = " & billingperiod & " AND STL_RUN = '" & settlementrun & "')" & vbNewLine _
                & "SELECT WBS.*, WB.STL_RUN " & vbNewLine _
                & "FROM WBS INNER JOIN WB ON WB.INVOICE_NO = WBS.INV_DM_CM "

            report = Await Me.DataAccess.ExecuteSelectQueryReturningDataReaderAsync(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            result = Await Me.GetWESMBillSummaryAsync(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function

    Public Async Function GetWESMBillSummaryAsync() As Task(Of List(Of WTAInstallment))
        Dim result As New List(Of WTAInstallment)
        Dim report As New DataReport
        Dim SQL As String
        Try
            SQL = "WITH WBS AS (SELECT * FROM AM_WESM_BILL_SUMMARY WHERE CHARGE_TYPE IN ('E', 'EV') AND DUE_DATE >= TO_DATE('" & AMModule.WTAInstallmentStartDate & "', 'MM-DD-YYYY') AND BALANCE_TYPE = 'AR' AND ENDING_BALANCE <> 0), " & vbNewLine _
                & "WB As (Select * FROM AM_WESM_BILL WHERE  CHARGE_TYPE IN ('E', 'EV') AND DUE_DATE >= TO_DATE('" & AMModule.WTAInstallmentStartDate & "', 'MM-DD-YYYY')), " & vbNewLine _
                & "TP AS (SELECT * FROM AM_PARTICIPANTS) " & vbNewLine _
                & "SELECT WBS.*, WB.STL_RUN, TP.PARTICIPANT_ID " & vbNewLine _
                & "FROM WBS " & vbNewLine _
                & "LEFT JOIN WB ON WB.INVOICE_NO = WBS.INV_DM_CM AND WB.CHARGE_TYPE = WBS.CHARGE_TYPE " & vbNewLine _
                & "LEFT JOIN TP ON TP.ID_NUMBER = WBS.ID_NUMBER " & vbNewLine _
                & "WHERE NOT EXISTS (SELECT * FROM AM_WTA_INSTALLMENT WTAI WHERE WTAI.WESMBILL_SUMMARY_NO = WBS.WESMBILL_SUMMARY_NO)"

            report = Await Me.DataAccess.ExecuteSelectQueryReturningDataReaderAsync(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            result = Await Me.GetWESMBillSummaryAsync(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function

    Private Function GetWESMBillSummaryAsync(ByVal dr As IDataReader) As Task(Of List(Of WTAInstallment))
        Dim result As New List(Of WTAInstallment)
        Try
            While dr.Read()
                With dr
                    Dim item As New WTAInstallment

                    item.STLRun = CStr(.Item("STL_RUN"))
                    item.WESMBillSummaryNo = CLng(.Item("WESMBILL_SUMMARY_NO"))

                    item.IDNumber = New AMParticipants(CStr(.Item("ID_NUMBER")), CStr(.Item("PARTICIPANT_ID")))
                    item.BillPeriod = CInt(.Item("BILLING_PERIOD"))
                    item.ChargeType = CType(System.Enum.Parse(GetType(EnumChargeType), CStr(.Item("CHARGE_TYPE"))), EnumChargeType)
                    item.DueDate = CDate(.Item("DUE_DATE"))
                    item.BeginningBalance = CDec(.Item("BEGINNING_BALANCE"))
                    item.EndingBalance = CDec(.Item("ENDING_BALANCE"))
                    item.NewDueDate = CDate(.Item("NEW_DUEDATE"))
                    item.IsMFWTaxDeducted = CInt(.Item("IS_MFWTAX_DEDUCTED"))
                    item.INVDMCMNo = CStr(.Item("INV_DM_CM"))
                    item.SummaryType = CType(System.Enum.Parse(GetType(EnumSummaryType), CStr(.Item("SUMMARY_TYPE"))), EnumSummaryType)
                    item.EnergyWithhold = If(CInt(.Item("ENERGY_WITHHOLD_STATUS")) = 2, 0D, CDec(.Item("ENERGY_WITHHOLD")))
                    item.EnergyWithholdStatus = CType(.Item("ENERGY_WITHHOLD_STATUS"), EnumEnergyWithholdStatus)
                    item.NoDefInt = CBool(IIf(CInt(.Item("NO_DEFINT")) = 1, True, False))
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
        Return Task.FromResult(result)
    End Function
#End Region

#Region "Saving"
    Public Async Function SaveWTAInstallment(ByVal listofWTA As List(Of WTAInstallment),
                                             ByVal listofTerms As Dictionary(Of Integer, Date),
                                             ByVal progress As IProgress(Of ProgressClass), ByVal ct As CancellationToken) As Task
        Try
            For Each item In listofWTA
                Dim terms As Integer = listofTerms.Keys.Count()
                Dim netAmount As Decimal = Math.Abs(item.EndingBalance - item.EnergyWithhold)
                Dim termAmount As Decimal = Math.Round(netAmount / terms, 2)

                item.Terms = terms
                item.Status = EnumWTAInstallmentStatus.Unpaid
                Dim listofWTAITerms As New List(Of WTAInstallmentTerms)
                For Each iKey As Integer In listofTerms.Keys
                    If iKey = terms Then
                        Dim computeDiff As Decimal = netAmount - Math.Round((termAmount * terms), 2)
                        termAmount += computeDiff
                    End If
                    Dim t As New WTAInstallmentTerms
                    With t
                        .Term = iKey
                        .TermDueDate = listofTerms(iKey)
                        .TermNewDueDate = listofTerms(iKey)
                        .TermAmount = termAmount
                        .PaidAmount = 0
                        .DefaultAmount = 0
                        .TermStatus = EnumWTAInstallmentTermStatus.Unpaid
                    End With
                    listofWTAITerms.Add(t)
                Next
                item.ListofWTAInstallmentTerm = listofWTAITerms
            Next

            Dim listSQL As New List(Of String)
            Dim sQL As String = ""
            For Each item In listofWTA
                Dim newWTAINo As Long = GetNextValSequence("SEQ_AM_WTAI_NO")
                sQL = "INSERT INTO AM_WTA_INSTALLMENT(WTAI_NO, WESMBILL_SUMMARY_NO, STL_RUN, TERMS, CREATED_BY, CREATED_DATETIME, STATUS) " & vbNewLine _
                    & "SELECT " & newWTAINo & ", " & item.WESMBillSummaryNo & ", '" & item.STLRun & "', " & item.Terms & ", '" & AMModule.UserName & "', SYSDATE, " & item.Status & " " & vbNewLine _
                    & "FROM DUAL"
                listSQL.Add(sQL)
                For Each term In item.ListofWTAInstallmentTerm
                    sQL = "INSERT INTO AM_WTA_INSTALLMENT_DETAILS(TERM, TERM_DUEDATE, TERM_NEW_DUEDATE, TERM_AMOUNT, PAID_AMOUNT, DEFAULT_AMOUNT, TERM_STATUS, UPDATED_BY, UPDATED_DATETIME, WTAI_NO) " & vbNewLine _
                    & "SELECT " & term.Term & ", TO_DATE('" & term.TermDueDate & "','MM/DD/YYYY'), TO_DATE('" & term.TermNewDueDate & "','MM/DD/YYYY'), " & term.TermAmount & ", " & term.PaidAmount & ", " & term.DefaultAmount & ", " & vbNewLine _
                    & term.TermStatus & ", '" & AMModule.UserName & "', SYSDATE, " & newWTAINo & " " & vbNewLine _
                    & "FROM DUAL"
                    listSQL.Add(sQL)
                Next
            Next

            Await SaveToDBAsync(listSQL, progress, ct)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Function

    Public Async Function SaveWTAInstallmentHistory(ByVal listofWTAInstallmentHis As List(Of WTAInstallmentHistory),
                                                    ByVal listofWTAInstallment As List(Of WTAInstallment)) As Task
        Try
            Dim listSQL As New List(Of String)
            Dim sQL As String = ""
            For Each item In listofWTAInstallmentHis
                sQL = "INSERT INTO AM_WTA_INSTALLMENT_HISTORY(TERM, TERM_NEW_DUEDATE, PAID_AMOUNT, DEFAULT_AMOUNT, COLLECTION_DATE, COLLECTION_NO, WTAI_NO, UPDATED_BY, UPDATED_DATETIME) " & vbNewLine _
                       & "SELECT " & item.Term & ",  TO_DATE('" & item.TermNewDueDate & "','MM/DD/YYYY'), " & item.PaidAmount & ", " & item.DefaultAmount & ", TO_DATE('" & item.CollectionDate & "','MM/DD/YYYY'), " & vbNewLine _
                       & item.CollectionNo & ", " & item.WTAINO & ", '" & AMModule.UserName & "', SYSDATE FROM DUAL"
                listSQL.Add(sQL)
            Next
            For Each item In listofWTAInstallment
                For Each term In item.ListofWTAInstallmentTerm.OrderBy(Function(x) x.Term)
                    Dim getWTAInstallmentHisItem As WTAInstallmentHistory = listofWTAInstallmentHis.Where(Function(x) x.Term = term.Term And x.WTAINO = term.WTAINO).FirstOrDefault()
                    If Not getWTAInstallmentHisItem Is Nothing Then
                        term.PaidAmount = term.PaidAmount + getWTAInstallmentHisItem.PaidAmount
                        term.DefaultAmount = term.DefaultAmount + getWTAInstallmentHisItem.DefaultAmount

                        Dim outstanding As Decimal = term.TermAmount - term.PaidAmount
                        term.TermStatus = If(outstanding = 0, EnumWTAInstallmentTermStatus.Paid, EnumWTAInstallmentTermStatus.Unpaid)
                        Dim newDueDate As Date = If(term.TermNewDueDate > getWTAInstallmentHisItem.CollectionDate, term.TermNewDueDate, getWTAInstallmentHisItem.CollectionDate)

                        sQL = "UPDATE AM_WTA_INSTALLMENT_DETAILS SET PAID_AMOUNT = " & term.PaidAmount & ", DEFAULT_AMOUNT = " & term.DefaultAmount & ", " & vbNewLine _
                            & "TERM_STATUS = " & term.TermStatus & ", " & vbNewLine _
                            & "UPDATED_BY = '" & AMModule.UserName & "', UPDATED_DATETIME = SYSDATE, TERM_NEW_DUEDATE = TO_DATE('" & newDueDate & "','MM/DD/YYYY') " & vbNewLine _
                            & "WHERE WTAI_NO = " & getWTAInstallmentHisItem.WTAINO & " AND TERM = " & getWTAInstallmentHisItem.Term
                        listSQL.Add(sQL)
                    End If
                Next
                Dim getPaidTermCount As Integer = item.ListofWTAInstallmentTerm.Where(Function(x) x.TermStatus = EnumWTAInstallmentTermStatus.Paid).Count()
                Dim getTermCount As Integer = item.ListofWTAInstallmentTerm.Count()
                If getPaidTermCount = getTermCount Then
                    sQL = "UPDATE AM_WTA_INSTALLMENT SET STATUS = " & EnumWTAInstallmentStatus.Paid & " WHERE WTAI_NO = " & item.WTAINO
                    listSQL.Add(sQL)
                ElseIf getPaidTermCount >= 1 And getPaidTermCount <> getTermCount Then
                    sQL = "UPDATE AM_WTA_INSTALLMENT SET STATUS = " & EnumWTAInstallmentStatus.OnGoing & " WHERE WTAI_NO = " & item.WTAINO
                    listSQL.Add(sQL)
                End If
            Next

            Await SaveToDBAsync(listSQL)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Function
#End Region

#Region "Generate Excel Report"
    Public Async Function GenerateWTAInstallmentSummaryReport(ByVal targetPathFolder As String,
                                                        ByVal listofWTAInstallment As List(Of WTAInstallment),
                                                        ByVal progress As IProgress(Of ProgressClass),
                                                        ByVal ct As CancellationToken) As Task


        Dim newProgress As ProgressClass = New ProgressClass
        newProgress.ProgressIndicator = 0
        newProgress.ProgressMsg = "Generating WTA Installment Summary in Excel format."
        progress.Report(newProgress)

        Await CreateWTAInstallmentSummaryInExcel(targetPathFolder, listofWTAInstallment)

        If ct.IsCancellationRequested Then
            Throw New OperationCanceledException
        End If
    End Function
    Public Async Function CreateWTAInstallmentSummaryInExcel(ByVal targetPathFolder As String,
                                              ByVal listofWTAInstallment As List(Of WTAInstallment)) As Task
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet1 As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlWTAISummary As Excel.Range

        Dim GetDateNow As Date = CDate(AMModule.SystemDate.ToShortDateString)
        Dim misValue As Object = System.Reflection.Missing.Value
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add()
        Dim getMaxTerms As Integer = listofWTAInstallment.Select(Function(x) x.Terms).Max()
        Dim getTotalColumn As Integer = 6 + (getMaxTerms * 4)
        Dim getBP As Integer = listofWTAInstallment.Select(Function(x) x.BillPeriod).Distinct().FirstOrDefault()
        Dim getSTLRun As String = listofWTAInstallment.Select(Function(x) x.STLRun).Distinct().FirstOrDefault()

        Dim WTAISummaryHeader As Object(,) = New Object(,) {}
        ReDim WTAISummaryHeader(1, getTotalColumn)
        WTAISummaryHeader(1, 0) = "STL ID"
        WTAISummaryHeader(1, 1) = "Participant Name"
        WTAISummaryHeader(1, 2) = "Transaction Number"
        WTAISummaryHeader(1, 3) = "Due Date"
        WTAISummaryHeader(1, 4) = "Charge Type"
        WTAISummaryHeader(1, 5) = "Outstanding Amount"
        WTAISummaryHeader(1, 6) = "Terms"

        Dim col As Integer = 6
        For i As Integer = 1 To getMaxTerms
            WTAISummaryHeader(0, col + 1) = "Term " & i
            WTAISummaryHeader(1, col + 1) = "Term Amount"
            WTAISummaryHeader(1, col + 2) = "Paid Amount"
            WTAISummaryHeader(1, col + 3) = "Default Amount"
            WTAISummaryHeader(1, col + 4) = "Outstanding Amount"
            col += 4
        Next

        'As Seller WorkSheet
        xlWorkSheet1 = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet1.Name = "WTA Installment Payments"
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(1, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(2, UBound(WTAISummaryHeader, 2) + 1), Excel.Range)
        xlWTAISummary = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlWTAISummary.Value = WTAISummaryHeader
        Dim lrow1 As Integer = xlWorkSheet1.Range("A" & xlWorkSheet1.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
        If listofWTAInstallment.Count <> 0 Then
            Dim WTADetailsSummaryArr As Object(,) = Await Me.GenerateWTAInstallmentArray(listofWTAInstallment, getTotalColumn)
            xlRowRange1 = DirectCast(xlWorkSheet1.Cells(lrow1, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet1.Cells(lrow1 + UBound(WTADetailsSummaryArr, 1), UBound(WTAISummaryHeader, 2) + 1), Excel.Range)
            xlWTAISummary = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
            xlWTAISummary.Value = WTADetailsSummaryArr
        End If


        Dim FileName As String
        FileName = FileExistIncrementer(targetPathFolder & "\" & "WTAStaggeredPaymentsSummary_BP" & getBP & getSTLRun & DateTime.Now().ToString("yyyyMMddHHmm") & ".xlsx")

        xlWorkBook.SaveAs(FileName, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
        xlWorkBook.Close(False)
        xlApp.Quit()

        releaseObject(xlWTAISummary)
        releaseObject(xlRowRange2)
        releaseObject(xlRowRange1)
        releaseObject(xlWorkSheet1)
        releaseObject(xlWorkBook)
        releaseObject(xlApp)
    End Function

    Private Function GenerateWTAInstallmentArray(ByVal listofWTAInstallment As List(Of WTAInstallment),
                                                 totalSizeArr As Integer) As Task(Of Object(,))
        Dim ret As Object(,) = New Object(,) {}
        Dim ObjDicInvNo As New Dictionary(Of String, Integer)
        Dim ObjDicSeq As New Dictionary(Of Integer, Integer)
        Dim RowIndex As Integer = 0
        Dim RowCount As Integer = listofWTAInstallment.Count()

        Dim WTADSArr As Object(,) = New Object(,) {}
        ReDim WTADSArr(RowCount, totalSizeArr)

        For Each item In listofWTAInstallment.OrderBy(Function(x) x.IDNumber.IDNumber).ThenBy(Function(x) x.ChargeType)
            WTADSArr(RowIndex, 0) = item.IDNumber.IDNumber
            WTADSArr(RowIndex, 1) = item.IDNumber.ParticipantID
            WTADSArr(RowIndex, 2) = item.INVDMCMNo
            WTADSArr(RowIndex, 3) = item.DueDate.ToString("MM/dd/yyyy")
            WTADSArr(RowIndex, 4) = item.ChargeType.ToString()
            WTADSArr(RowIndex, 5) = item.ListofWTAInstallmentTerm().Select(Function(x) x.TermAmount).Sum()
            WTADSArr(RowIndex, 6) = item.Terms
            Dim col As Integer = 6
            For Each term In item.ListofWTAInstallmentTerm
                WTADSArr(RowIndex, col + 1) = term.TermAmount
                WTADSArr(RowIndex, col + 2) = term.PaidAmount
                WTADSArr(RowIndex, col + 3) = term.DefaultAmount
                WTADSArr(RowIndex, col + 4) = term.TermAmount - term.PaidAmount
                col += 4
            Next
            RowIndex += 1
        Next
        ret = WTADSArr
        Return Task.FromResult(ret)
    End Function

    Public Shared Function FileExistIncrementer(ByVal orginialFileName As String) As String
        Dim counter As Integer = 0
        Dim NewFileName As String = orginialFileName
        While File.Exists(NewFileName)
            counter = counter + 1
            NewFileName = String.Format("{0}\{1}{2}{3}", Path.GetDirectoryName(orginialFileName), Path.GetFileNameWithoutExtension(orginialFileName), " (" & counter.ToString() & ")", Path.GetExtension(orginialFileName))
        End While
        Return NewFileName
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

#Region "DAL Functions"
    Private Async Function SaveToDBAsync(ByVal lisOfSQL As List(Of String)) As Task
        Dim report As New DataReport
        Dim ListofSQL As New List(Of String)
        Try
            report = Await Me.DataAccess.ExecuteSaveQueryAsync(lisOfSQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Function
    Private Async Function SaveToDBAsync(ByVal lisOfSQL As List(Of String), ByVal progress As IProgress(Of ProgressClass), ByVal ct As CancellationToken) As Task
        Dim report As New DataReport
        Dim ListofSQL As New List(Of String)
        Try
            report = Await Me.DataAccess.ExecuteSaveQueryAsync(lisOfSQL, progress, ct)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Function
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
