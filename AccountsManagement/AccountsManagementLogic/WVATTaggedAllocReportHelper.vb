Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Public Class WVATTaggedAllocReportHelper
    Public Sub New()
        'Get the current instance of the dal
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._DataAccess = DAL.GetInstance()
        Me._WVATTaggedAllocatedList = New List(Of WVATTaggedAllocated)
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

#Region "Property of WVAT Certificate Transaction"
    Private _WVATTaggedAllocatedList As List(Of WVATTaggedAllocated)
    Public Property WVATTaggedAllocatedList() As List(Of WVATTaggedAllocated)
        Get
            Return _WVATTaggedAllocatedList
        End Get
        Set(ByVal value As List(Of WVATTaggedAllocated))
            _WVATTaggedAllocatedList = value
        End Set
    End Property
#End Region

#Region "Main Function"
    Public Sub GetWVATTaggedAllocatedList(ByVal dateFrom As Date, ByVal dateTo As Date)
        Me._WVATTaggedAllocatedList = GetListOfWVATTaggedAllocated(dateFrom, dateTo)
    End Sub
#End Region

#Region "Get CollectionsAndPayments For EWT"
    Private Function GetListOfWVATTaggedAllocated(ByVal dateFrom As Date, ByVal dateTo As Date) As List(Of WVATTaggedAllocated)
        Dim ret As New List(Of WVATTaggedAllocated)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.CERTIFICATE_NO, C.ID_NUMBER, C.INV_DM_CM, A.REMITTANCE_DATE, (D.EWT_SALES + EWT_PURCHASES) AS ORIG_EWT_AMOUNT, " & vbNewLine _
                                    & "B.ENDING_BALANCE, B.AMOUNT_TAGGED, C.BALANCE_TYPE, CASE A.ALLOCATED_TO_AP WHEN 0 THEN 'NO' WHEN 1 THEN 'YES' END AS ALLOCATED_TO_AP_YESNO " & vbNewLine _
                               & "FROM AM_CERTIFICATE_WHVAT_STL A " & vbNewLine _
                               & "LEFT JOIN AM_CERTIFICATE_WHVAT_DETAILS B ON A.CERTIFICATE_NO = B.CERTIFICATE_NO " & vbNewLine _
                               & "LEFT JOIN AM_WESM_BILL_SUMMARY C ON C.WESMBILL_SUMMARY_NO = B.WESMBILL_SUMMARY_NO " & vbNewLine _
                               & "JOIN AM_WESM_ALLOC_COVER_SUMMARY D ON D.TRANSACTION_NUMBER = C.INV_DM_CM " & vbNewLine _
                               & "WHERE A.REMITTANCE_DATE BETWEEN TO_DATE('" & dateFrom.ToShortDateString & "','MM/DD/YYYY') AND TO_DATE('" & dateTo & "','MM/DD/YYYY') AND UNTAG_EWT <> 1"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetListOfWVATTaggedAllocated(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function
    Private Function GetListOfWVATTaggedAllocated(ByVal dr As IDataReader) As List(Of WVATTaggedAllocated)
        Dim result As New List(Of WVATTaggedAllocated)

        Try
            While dr.Read()
                Dim item As New WVATTaggedAllocated
                With dr
                    item.CertificateNo = CLng(.Item("CERTIFICATE_NO"))
                    item.BillingIDNumber = CStr(.Item("ID_NUMBER"))
                    item.WESMTransNumber = CStr(.Item("INV_DM_CM"))
                    item.RemittanceDate = CDate(.Item("REMITTANCE_DATE"))
                    item.AmountTaggedAlloc = CDec(.Item("AMOUNT_TAGGED"))
                    item.BalanceType = CStr(.Item("BALANCE_TYPE"))
                    item.AllocatedToAP = CStr(.Item("ALLOCATED_TO_AP_YESNO"))
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

End Class
