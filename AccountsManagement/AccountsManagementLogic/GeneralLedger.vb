'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              GeneralLedger
'Orginal Author:         Joseph B. Gabriel
'File Creation Date:     June 09, 2015
'Development Group:      Software Development and Support Division
'Description:            A file for storing functions of DeferredPayment
'Arguments/Parameters:  
'Files/Database Tables:  GeneralLedger
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'
Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Imports System.Data
Imports System.Net

Public Class GeneralLedger

#Region "Single Instance Code"
    ' <summary>
    ' This variable stores the reference of the single instance
    ' </summary>
    ' <remarks></remarks>
    Private Shared m_Instance As GeneralLedger = Nothing

    ' <summary>
    ' Gets the current instance of this class
    ' Dependencies:
    '  None
    '   
    '  Output
    '   the reference instance
    ' </summary>
    ' <returns>
    ' The single instance of this class
    ' </returns>
    ' <remarks></remarks>
    Public Shared Function GetInstance() As GeneralLedger
        If m_Instance Is Nothing Then
            m_Instance = New GeneralLedger()
        End If
        Return m_Instance
    End Function

    Private Sub New()
        'Get the current instance of the dal
        Me._DataAccess = DAL.GetInstance()
        Me._BFactory = BusinessFactory.GetInstance()
    End Sub


    Private _DataAccess As DAL
    ' <summary>
    ' gets the DataAccessLayer
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public ReadOnly Property DataAccess() As DAL
        Get
            Return Me._DataAccess
        End Get
    End Property

    Private _ConnectionString As String

    ' <summary>
    ' gets or sets the database connection string
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property ConnectionString() As String
        Get
            Return _ConnectionString
        End Get
        Set(ByVal value As String)
            Me._ConnectionString = value
        End Set
    End Property

    Private _UserName As String

    Public Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property

    Private _BFactory As BusinessFactory
    Public Property BFactory() As BusinessFactory
        Get
            Return _BFactory
        End Get
        Set(ByVal value As BusinessFactory)
            _BFactory = value
        End Set
    End Property


#End Region

    '#Region "GetGeneralLedger"
    '    Public Function GetGeneralLedger(ByVal TransactionBegin As String, ByVal TransactionEnd As String) As List(Of GeneralLedgerObj)
    '        Dim result As New List(Of GeneralLedgerObj)
    '        Dim report As New DataReport

    '        Try
    '            Dim SQL As String = "SELECT AM_COLLECTION.COLLECTION_DATE," & _
    '                                    "AM_JV.AM_JV_NO," & _
    '                                    "AM_COLLECTION.OR_NO," & _
    '                                    "AM_PARTICIPANTS.FULL_NAME," & _
    '                                    "AM_COLLECTION.COLLECTED_AMOUNT," & _
    '                                    "0 as CREDIT," & _
    '                                    "AM_ADMIN_SETTINGS.VALUE," & _
    '                                    "0 as BEGINNING_BALANCE" & _
    '                                " FROM AM_COLLECTION" & _
    '                                " INNER JOIN AM_JV" & _
    '                                " ON AM_COLLECTION.DAILY_BATCH_CODE = AM_JV.BATCH_CODE" & _
    '                                " LEFT JOIN AM_PARTICIPANTS" & _
    '                                " ON AM_PARTICIPANTS.ID_NUMBER = AM_COLLECTION.ID_NUMBER" & _
    '                                " LEFT JOIN AM_ADMIN_SETTINGS" & _
    '                                " ON AM_ADMIN_SETTINGS.CODE_NAME       = 'CashInBankSettlementCode'" & _
    '                                " WHERE AM_COLLECTION.COLLECTION_DATE BETWEEN TO_DATE('" & TransactionBegin & "', 'mm/dd/yyyy','NLS_DATE_LANGUAGE = American') AND TO_DATE('" & TransactionEnd & "', 'mm/dd/yyyy','NLS_DATE_LANGUAGE = American')" & _
    '                                " AND AM_COLLECTION.STATUS             <> 0" & _
    '                                " AND AM_COLLECTION.COLLECTION_CATEGORY = 1" & _
    '                                " AND AM_COLLECTION.ID_NUMBER           = AM_PARTICIPANTS.ID_NUMBER" & _
    '                                " ORDER BY AM_PARTICIPANTS.FULL_NAME"

    '            '"AM_STL_NOTICE_BEGINNING_BAL.BEGINNING_BALANCE" & _


    '            '" LEFT JOIN AM_STL_NOTICE_BEGINNING_BAL" & _
    '            '" ON AM_STL_NOTICE_BEGINNING_BAL.ID_NUMBER = AM_PARTICIPANTS.ID_NUMBER" & _


    '            Me.DataAccess.ConnectionString = Me.ConnectionString
    '            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)

    '            If report.ErrorMessage.Length <> 0 Then
    '                Throw New ApplicationException(report.ErrorMessage)
    '            End If

    '            result = Me.GetGeneralLedgerReport(report.ReturnedIDatareader)
    '        Catch ex As Exception
    '            Throw New ApplicationException(ex.Message)
    '            'MsgBox(ex.ToString)
    '        End Try

    '        Return result
    '    End Function

    '    Private Function GetGeneralLedgerReport(ByVal dr As IDataReader) As List(Of GeneralLedgerObj)
    '        Dim result As New List(Of GeneralLedgerObj)
    '        Dim index As Integer = 0
    '        Try
    '            While dr.Read()
    '                index += 1
    '                With dr
    '                    Dim item As New GeneralLedgerObj
    '                    item.TransactionDate = .Item("COLLECTION_DATE").ToString()
    '                    item.JournalNumber = .Item("AM_JV_NO").ToString()
    '                    item.ReferenceNumber = .Item("OR_NO").ToString()
    '                    item.ParticipantName = .Item("FULL_NAME").ToString()
    '                    item.Debit = CDec(.Item("COLLECTED_AMOUNT").ToString())
    '                    item.Credit = CDec(.Item("CREDIT").ToString())
    '                    item.AccountNumber = .Item("VALUE").ToString()
    '                    item.BeginningBalance = CDec(.Item("BEGINNING_BALANCE").ToString())
    '                    result.Add(item)
    '                End With
    '            End While
    '        Catch ex As Exception
    '            Throw New ApplicationException("Error in row " & index & " --- " & ex.Message)
    '        Finally
    '            If Not dr.IsClosed Then
    '                dr.Close()
    '            End If
    '        End Try
    '        Return result
    '    End Function

    '#End Region

    '#Region "GetGeneralLedgerCashinBankDebit"
    '    Public Function GetGeneralLedgerCashinBankDebit(ByVal TransactionBegin As String, ByVal TransactionEnd As String) As List(Of GeneralLedgerObj)
    '        Dim result As New List(Of GeneralLedgerObj)
    '        Dim report As New DataReport

    '        Try
    '            Dim SQL As String = "SELECT AM_FTF_MAIN.ALLOCATION_DATE," & _
    '                                    "AM_JV.AM_JV_NO," & _
    '                                    "AM_FTF_MAIN.REF_NO," & _
    '                                    "AM_PARTICIPANTS.FULL_NAME," & _
    '                                    "AM_FTF_DETAILS.DEBIT," & _
    '                                    "AM_FTF_DETAILS.CREDIT," & _
    '                                    "AM_ADMIN_SETTINGS.VALUE," & _
    '                                    "0 as BEGINNING_BALANCE" & _
    '                                " FROM AM_FTF_MAIN" & _
    '                                " INNER JOIN AM_FTF_DETAILS" & _
    '                                " ON AM_FTF_MAIN.REF_NO = AM_FTF_DETAILS.REF_NO" & _
    '                                " INNER JOIN AM_JV" & _
    '                                " ON AM_FTF_MAIN.BATCH_CODE = AM_JV.BATCH_CODE" & _
    '                                " INNER JOIN AM_FTF_PARTICIPANT" & _
    '                                " ON AM_FTF_MAIN.REF_NO = AM_FTF_PARTICIPANT.REF_NO" & _
    '                                " LEFT JOIN AM_PARTICIPANTS" & _
    '                                " ON AM_FTF_PARTICIPANT.ID_NUMBER = AM_PARTICIPANTS.ID_NUMBER" & _
    '                                " LEFT JOIN AM_ADMIN_SETTINGS" & _
    '                                " ON AM_FTF_DETAILS.ACCT_CODE = AM_ADMIN_SETTINGS.VALUE" & _
    '                                " WHERE AM_FTF_MAIN.ALLOCATION_DATE BETWEEN TO_DATE('" & TransactionBegin & "', 'mm/dd/yyyy','NLS_DATE_LANGUAGE = American') AND TO_DATE('" & TransactionEnd & "', 'mm/dd/yyyy','NLS_DATE_LANGUAGE = American')" & _
    '                                " AND AM_FTF_MAIN.STATUS                = 1" & _
    '                                " AND AM_ADMIN_SETTINGS.CODE_NAME       = 'CashInBankSettlementCode'" & _
    '                                " AND AM_FTF_DETAILS.DEBIT             <> 0" & _
    '                                " ORDER BY AM_PARTICIPANTS.FULL_NAME"

    '            Me.DataAccess.ConnectionString = Me.ConnectionString
    '            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)

    '            If report.ErrorMessage.Length <> 0 Then
    '                Throw New ApplicationException(report.ErrorMessage)
    '            End If

    '            result = Me.GetGeneralLedgerCashinBankDebitReport(report.ReturnedIDatareader)
    '        Catch ex As Exception
    '            Throw New ApplicationException(ex.Message)
    '            'MsgBox(ex.ToString)
    '        End Try

    '        Return result
    '    End Function

    '    Private Function GetGeneralLedgerCashinBankDebitReport(ByVal dr As IDataReader) As List(Of GeneralLedgerObj)
    '        Dim result As New List(Of GeneralLedgerObj)
    '        Dim index As Integer = 0
    '        Try
    '            While dr.Read()
    '                index += 1
    '                With dr
    '                    Dim item As New GeneralLedgerObj
    '                    item.TransactionDate = .Item("ALLOCATION_DATE").ToString()
    '                    item.JournalNumber = .Item("AM_JV_NO").ToString()
    '                    item.ReferenceNumber = .Item("REF_NO").ToString()
    '                    item.ParticipantName = .Item("FULL_NAME").ToString()
    '                    'item.Debit = CDec(Me.GetGeneralLedgerCashinBankDebit(CDate(item.TransactionDate).Date, CStr(item.JournalNumber), CStr(item.ReferenceNumber), CStr(item.ParticipantName)))
    '                    item.Debit = CDec(.Item("DEBIT").ToString())
    '                    item.Credit = CDec(.Item("CREDIT").ToString())
    '                    item.AccountNumber = .Item("VALUE").ToString()
    '                    item.BeginningBalance = CDec(.Item("BEGINNING_BALANCE").ToString())
    '                    result.Add(item)
    '                End With
    '            End While
    '        Catch ex As Exception
    '            Throw New ApplicationException("Error in row " & index & " --- " & ex.Message)
    '        Finally
    '            If Not dr.IsClosed Then
    '                dr.Close()
    '            End If
    '        End Try
    '        Return result
    '    End Function

    '#End Region

    '#Region "GetGeneralLedgerCashinBankCredit"
    '    Public Function GetGeneralLedgerCashinBankCredit(ByVal TransactionBegin As String, ByVal TransactionEnd As String) As List(Of GeneralLedgerObj)
    '        Dim result As New List(Of GeneralLedgerObj)
    '        Dim report As New DataReport

    '        Try
    '            Dim SQL As String = "SELECT AM_FTF_MAIN.ALLOCATION_DATE," & _
    '                                    "AM_JV.AM_JV_NO," & _
    '                                    "AM_FTF_MAIN.REF_NO," & _
    '                                    "AM_PARTICIPANTS.FULL_NAME," & _
    '                                    "AM_FTF_DETAILS.DEBIT," & _
    '                                    "AM_FTF_DETAILS.CREDIT," & _
    '                                    "AM_ADMIN_SETTINGS.VALUE," & _
    '                                    "0 as BEGINNING_BALANCE" & _
    '                                " FROM AM_FTF_MAIN" & _
    '                                " INNER JOIN AM_FTF_DETAILS" & _
    '                                " ON AM_FTF_MAIN.REF_NO = AM_FTF_DETAILS.REF_NO" & _
    '                                " INNER JOIN AM_JV" & _
    '                                " ON AM_FTF_MAIN.BATCH_CODE = AM_JV.BATCH_CODE" & _
    '                                " INNER JOIN AM_FTF_PARTICIPANT" & _
    '                                " ON AM_FTF_MAIN.REF_NO = AM_FTF_PARTICIPANT.REF_NO" & _
    '                                " LEFT JOIN AM_PARTICIPANTS" & _
    '                                " ON AM_FTF_PARTICIPANT.ID_NUMBER = AM_PARTICIPANTS.ID_NUMBER" & _
    '                                " LEFT JOIN AM_ADMIN_SETTINGS" & _
    '                                " ON AM_FTF_DETAILS.ACCT_CODE = AM_ADMIN_SETTINGS.VALUE" & _
    '                                " WHERE AM_FTF_MAIN.ALLOCATION_DATE BETWEEN TO_DATE('" & TransactionBegin & "', 'mm/dd/yyyy','NLS_DATE_LANGUAGE = American') AND TO_DATE('" & TransactionEnd & "', 'mm/dd/yyyy','NLS_DATE_LANGUAGE = American')" & _
    '                                " AND AM_FTF_MAIN.STATUS                = 1" & _
    '                                " AND AM_ADMIN_SETTINGS.CODE_NAME       = 'CashInBankSettlementCode'" & _
    '                                " AND AM_FTF_DETAILS.CREDIT             <> 0" & _
    '                                " ORDER BY AM_PARTICIPANTS.FULL_NAME"

    '            Me.DataAccess.ConnectionString = Me.ConnectionString
    '            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)

    '            If report.ErrorMessage.Length <> 0 Then
    '                Throw New ApplicationException(report.ErrorMessage)
    '            End If

    '            result = Me.GetGeneralLedgerCashinBankCreditReport(report.ReturnedIDatareader)
    '        Catch ex As Exception
    '            Throw New ApplicationException(ex.Message)
    '            'MsgBox(ex.ToString)
    '        End Try

    '        Return result
    '    End Function

    '    Private Function GetGeneralLedgerCashinBankCreditReport(ByVal dr As IDataReader) As List(Of GeneralLedgerObj)
    '        Dim result As New List(Of GeneralLedgerObj)
    '        Dim index As Integer = 0
    '        Try
    '            While dr.Read()
    '                index += 1
    '                With dr
    '                    Dim item As New GeneralLedgerObj
    '                    item.TransactionDate = .Item("ALLOCATION_DATE").ToString()
    '                    item.JournalNumber = .Item("AM_JV_NO").ToString()
    '                    item.ReferenceNumber = .Item("REF_NO").ToString()
    '                    item.ParticipantName = .Item("FULL_NAME").ToString()
    '                    'item.Debit = CDec(Me.GetGeneralLedgerCashinBankDebit(CDate(item.TransactionDate).Date, CStr(item.JournalNumber), CStr(item.ReferenceNumber), CStr(item.ParticipantName)))
    '                    item.Debit = CDec(.Item("DEBIT").ToString())
    '                    item.Credit = CDec(.Item("CREDIT").ToString())
    '                    item.AccountNumber = .Item("VALUE").ToString()
    '                    item.BeginningBalance = CDec(.Item("BEGINNING_BALANCE").ToString())
    '                    result.Add(item)
    '                End With
    '            End While
    '        Catch ex As Exception
    '            Throw New ApplicationException("Error in row " & index & " --- " & ex.Message)
    '        Finally
    '            If Not dr.IsClosed Then
    '                dr.Close()
    '            End If
    '        End Try
    '        Return result
    '    End Function

    '#End Region

    '#Region "GetGeneralLedgerPayment"
    '    Public Function GetGeneralLedgerPayment(ByVal TransactionBegin As String, ByVal TransactionEnd As String) As List(Of GeneralLedgerObj)
    '        Dim result As New List(Of GeneralLedgerObj)
    '        Dim report As New DataReport

    '        Try
    '            Dim SQL As String = "SELECT DISTINCT AM_PEMC_PAYMENT.ALLOCATION_DATE ," & _
    '                                    "AM_JV.AM_JV_NO," & _
    '                                    "AM_JV.AM_JV_NO," & _
    '                                    "AM_PARTICIPANTS.FULL_NAME," & _
    '                                    "0 AS DEBIT," & _
    '                                    "AM_PEMC_PAYMENT.TOTAL_PAYMENT as CREDIT," & _
    '                                    "AM_ADMIN_SETTINGS.VALUE," & _
    '                                    "0 as BEGINNING_BALANCE" & _
    '                                " FROM AM_PEMC_PAYMENT" & _
    '                                " INNER JOIN AM_PAYMENT" & _
    '                                " ON AM_PEMC_PAYMENT.ALLOCATION_DATE = AM_PAYMENT.COLLECTION_ALLOCATION_DATE" & _
    '                                " INNER JOIN AM_JV" & _
    '                                " ON AM_PAYMENT.PAYMENT_BATCH_CODE = AM_JV.BATCH_CODE" & _
    '                                " LEFT JOIN AM_PARTICIPANTS" & _
    '                                " ON AM_PEMC_PAYMENT.ID_NUMBER = AM_PARTICIPANTS.ID_NUMBER" & _
    '                                " LEFT JOIN AM_ADMIN_SETTINGS" & _
    '                                " ON AM_ADMIN_SETTINGS.CODE_NAME       = 'CashInBankSettlementCode'" & _
    '                                " WHERE AM_PEMC_PAYMENT.ALLOCATION_DATE BETWEEN TO_DATE('" & TransactionBegin & "', 'mm/dd/yyyy','NLS_DATE_LANGUAGE = American')" & _
    '                                " AND TO_DATE('" & TransactionEnd & "', 'mm/dd/yyyy','NLS_DATE_LANGUAGE = American') AND AM_JV.POSTED_TYPE = '" & EnumPostedType.PEFT.ToString & "' " & _
    '                                " ORDER BY AM_PARTICIPANTS.FULL_NAME"

    '            Me.DataAccess.ConnectionString = Me.ConnectionString
    '            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)

    '            If report.ErrorMessage.Length <> 0 Then
    '                Throw New ApplicationException(report.ErrorMessage)
    '            End If

    '            result = Me.GetGeneralLedgerPaymentReport(report.ReturnedIDatareader)
    '        Catch ex As Exception
    '            Throw New ApplicationException(ex.Message)
    '            'MsgBox(ex.ToString)
    '        End Try

    '        Return result
    '    End Function

    '    Private Function GetGeneralLedgerPaymentReport(ByVal dr As IDataReader) As List(Of GeneralLedgerObj)
    '        Dim result As New List(Of GeneralLedgerObj)
    '        Dim index As Integer = 0
    '        Try
    '            While dr.Read()
    '                index += 1
    '                With dr
    '                    Dim item As New GeneralLedgerObj
    '                    item.TransactionDate = .Item("ALLOCATION_DATE").ToString()
    '                    item.JournalNumber = .Item("AM_JV_NO").ToString()
    '                    item.ReferenceNumber = .Item("AM_JV_NO").ToString()
    '                    item.ParticipantName = .Item("FULL_NAME").ToString()
    '                    item.Debit = CDec(.Item("DEBIT").ToString())
    '                    item.Credit = CDec(.Item("CREDIT").ToString())
    '                    item.AccountNumber = .Item("VALUE").ToString()
    '                    item.BeginningBalance = CDec(.Item("BEGINNING_BALANCE").ToString())
    '                    result.Add(item)
    '                End With
    '            End While
    '        Catch ex As Exception
    '            Throw New ApplicationException("Error in row " & index & " --- " & ex.Message)
    '        Finally
    '            If Not dr.IsClosed Then
    '                dr.Close()
    '            End If
    '        End Try
    '        Return result
    '    End Function

    '#End Region

    '#Region "GenerateGeneralLedgerReport"
    '    Public Function GenerateGeneralLedgerReport(ByVal dt As DataTable, TransactionBegin As String, TransactionEnd As String, ByVal GeneralLedgerObjReport As List(Of GeneralLedgerObj)) As DataTable
    '        For Each item In GeneralLedgerObjReport

    '            Dim row = dt.NewRow()
    '            row("TRANS_DATE") = DateTime.Parse(item.TransactionDate.ToString()).ToString("MM/dd/yyyy")
    '            row("JOURNAL_NUMBER") = item.JournalNumber.ToString()
    '            row("REFERENCE_NUMBER") = item.ReferenceNumber.ToString()
    '            row("DESCRIPTION") = item.ParticipantName.ToString()
    '            row("DEBIT") = item.Debit
    '            row("CREDIT") = item.Credit
    '            row("FROM_DATE") = TransactionBegin
    '            row("TO_DATE") = TransactionEnd
    '            row("ACCOUNT_NUMBER") = item.AccountNumber.ToString()
    '            row("BEGINNING_BALANCE") = item.BeginningBalance.ToString()
    '            dt.Rows.Add(row)

    '        Next
    '        Return dt
    '    End Function
    '#End Region

End Class
