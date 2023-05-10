'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              DeferredPayment
'Orginal Author:         Joseph B. Gabriel
'File Creation Date:     July 03, 2015
'Development Group:      Software Development and Support Division
'Description:            A file for storing functions of PrudentialModel
'Arguments/Parameters:  
'Files/Database Tables:  PrudentialModel
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

Public Class PrudentialModel

#Region "Single Instance Code"
    ' <summary>
    ' This variable stores the reference of the single instance
    ' </summary>
    ' <remarks></remarks>
    Private Shared m_Instance As PrudentialModel = Nothing

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
    Public Shared Function GetInstance() As PrudentialModel
        If m_Instance Is Nothing Then
            m_Instance = New PrudentialModel()
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

#Region "GetPrudentialDebit"
    Public Function GetPrudentialDebit(ByVal TransactionBegin As String, ByVal TransactionEnd As String) As List(Of PrudentialObj)
        Dim result As New List(Of PrudentialObj)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT AM_FTF_MAIN.ALLOCATION_DATE," & _
                                    "AM_JV.AM_JV_NO," & _
                                    "AM_FTF_MAIN.REF_NO," & _
                                    "AM_PARTICIPANTS.FULL_NAME," & _
                                    "AM_FTF_PARTICIPANT.AMOUNT" & _
                                " FROM AM_FTF_MAIN" & _
                                " INNER JOIN AM_FTF_DETAILS" & _
                                " ON AM_FTF_MAIN.REF_NO                 = AM_FTF_DETAILS.REF_NO" & _
                                " INNER JOIN AM_JV" & _
                                " ON AM_FTF_MAIN.BATCH_CODE             = AM_JV.BATCH_CODE" & _
                                " INNER JOIN AM_FTF_PARTICIPANT" & _
                                " ON AM_FTF_MAIN.REF_NO                 = AM_FTF_PARTICIPANT.REF_NO" & _
                                " LEFT JOIN AM_PARTICIPANTS" & _
                                " ON AM_FTF_PARTICIPANT.ID_NUMBER       = AM_PARTICIPANTS.ID_NUMBER" & _
                                " LEFT JOIN AM_ADMIN_SETTINGS" & _
                                " ON AM_FTF_DETAILS.ACCT_CODE           = AM_ADMIN_SETTINGS.VALUE" & _
                                " WHERE AM_FTF_MAIN.ALLOCATION_DATE BETWEEN TO_DATE('" & TransactionBegin & "', 'mm/dd/yyyy','NLS_DATE_LANGUAGE = American')" & _
                                " AND TO_DATE('" & TransactionEnd & "', 'mm/dd/yyyy','NLS_DATE_LANGUAGE = American')" & _
                                " AND AM_FTF_MAIN.STATUS                = 1" & _
                                " AND AM_ADMIN_SETTINGS.CODE_NAME       = 'CashInBankPrudentialCode'" & _
                                " AND AM_FTF_DETAILS.DEBIT              <> 0"

            Me.DataAccess.ConnectionString = Me.ConnectionString
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.GetPrudentialDebitReport(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
            'MsgBox(ex.ToString)
        End Try

        Return result
    End Function

    Private Function GetPrudentialDebitReport(ByVal dr As IDataReader) As List(Of PrudentialObj)
        Dim result As New List(Of PrudentialObj)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As New PrudentialObj
                    item.TransactionDate = .Item("ALLOCATION_DATE").ToString()
                    item.JournalNumber = .Item("AM_JV_NO").ToString()
                    item.ReferenceNumber = .Item("REF_NO").ToString()
                    item.ParticipantName = .Item("FULL_NAME").ToString()
                    item.Debit = CDec(.Item("AMOUNT").ToString())
                    item.Credit = CDec(Me.GetPrudentialCredit(CDate(item.TransactionDate).Date, CStr(item.JournalNumber), CStr(item.ReferenceNumber), CStr(item.ParticipantName)))
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

#Region "GetPrudentialCredit"
    Public Function GetPrudentialCredit(ByVal AllocationDate As Date, ByVal JournalNo As String, ByVal ReferenceNo As String, ByVal ParticipantName As String) As Decimal
        Dim result As Decimal
        Dim report As New DataReport

        Try
            Dim SQL As String = " SELECT AM_FTF_PARTICIPANT.AMOUNT" & _
                                " FROM AM_FTF_MAIN" & _
                                " INNER JOIN AM_FTF_DETAILS" & _
                                " ON AM_FTF_MAIN.REF_NO                 = AM_FTF_DETAILS.REF_NO" & _
                                " INNER JOIN AM_JV" & _
                                " ON AM_FTF_MAIN.BATCH_CODE             = AM_JV.BATCH_CODE" & _
                                " INNER JOIN AM_FTF_PARTICIPANT" & _
                                " ON AM_FTF_MAIN.REF_NO                 = AM_FTF_PARTICIPANT.REF_NO" & _
                                " LEFT JOIN AM_PARTICIPANTS" & _
                                " ON AM_FTF_PARTICIPANT.ID_NUMBER       = AM_PARTICIPANTS.ID_NUMBER" & _
                                " LEFT JOIN AM_ADMIN_SETTINGS" & _
                                " ON AM_FTF_DETAILS.ACCT_CODE           = AM_ADMIN_SETTINGS.VALUE" & _
                                " WHERE AM_FTF_MAIN.ALLOCATION_DATE = TO_DATE('" & AllocationDate & "', 'mm/dd/yyyy','NLS_DATE_LANGUAGE = American') " & _
                                " AND AM_FTF_MAIN.STATUS                = 1" & _
                                " AND AM_ADMIN_SETTINGS.CODE_NAME       = 'CashInBankPrudentialCode'" & _
                                " AND AM_FTF_DETAILS.CREDIT              <> 0" & _
                                " AND AM_JV.AM_JV_NO                    = '" & JournalNo & "'" & _
                                " AND AM_FTF_MAIN.REF_NO                = '" & ReferenceNo & "'" & _
                                " AND AM_PARTICIPANTS.FULL_NAME         = '" & ParticipantName & "'"

            Me.DataAccess.ConnectionString = Me.ConnectionString
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.GetCredit(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
            'MsgBox(ex.ToString)
        End Try

        Return result

    End Function

    Private Function GetCredit(ByVal dr As IDataReader) As Decimal
        Dim result As Decimal
        'result = 0
        Try
            While dr.Read()
                With dr
                    Dim item As Decimal
                    item = CDec(.Item("AMOUNT"))
                    'result.Add(item)
                    result = item
                End With
            End While
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function

#End Region

#Region "GetPrudentialOR"
    Public Function GetPrudentialOR(ByVal TransactionBegin As String, ByVal TransactionEnd As String) As List(Of PrudentialObj)
        Dim result As New List(Of PrudentialObj)
        Dim report As New DataReport

        Try
            Dim SQL As String = " SELECT AM_OFFICIAL_RECEIPT_MAIN.OR_DATE ," & _
                                    " AM_JV.AM_JV_NO," & _
                                    " AM_OFFICIAL_RECEIPT_MAIN.OR_No," & _
                                    " AM_PARTICIPANTS.FULL_NAME," & _
                                    " AM_OFFICIAL_RECEIPT_DETAILS.DEBIT" & _
                                " FROM AM_OFFICIAL_RECEIPT_MAIN" & _
                                " INNER JOIN AM_OFFICIAL_RECEIPT_DETAILS" & _
                                " ON AM_OFFICIAL_RECEIPT_MAIN.OR_NO = AM_OFFICIAL_RECEIPT_DETAILS.OR_NO" & _
                                " INNER JOIN AM_JV" & _
                                " ON AM_OFFICIAL_RECEIPT_MAIN.BATCH_CODE = AM_JV.BATCH_CODE" & _
                                " LEFT JOIN AM_PARTICIPANTS" & _
                                " ON AM_OFFICIAL_RECEIPT_MAIN.ID_NUMBER = AM_PARTICIPANTS.ID_NUMBER" & _
                                " LEFT JOIN AM_ADMIN_SETTINGS" & _
                                " ON AM_OFFICIAL_RECEIPT_DETAILS.ACCT_CODE = AM_ADMIN_SETTINGS.VALUE" & _
                                " WHERE AM_OFFICIAL_RECEIPT_MAIN.OR_DATE BETWEEN TO_DATE('" & TransactionBegin & "', 'mm/dd/yyyy','NLS_DATE_LANGUAGE = American')" & _
                                " AND TO_DATE('" & TransactionEnd & "', 'mm/dd/yyyy','NLS_DATE_LANGUAGE = American')" & _
                                " AND AM_OFFICIAL_RECEIPT_MAIN.TRANS_TYPE = '" & EnumORTransactionType.Replenishment & "'" & _
                                " AND AM_ADMIN_SETTINGS.CODE_NAME = 'CashInBankPrudentialCode'" & _
                                " AND AM_OFFICIAL_RECEIPT_MAIN.STATUS = 1"

            Me.DataAccess.ConnectionString = Me.ConnectionString
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.GetPrudentialORReport(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
            'MsgBox(ex.ToString)
        End Try

        Return result
    End Function

    Private Function GetPrudentialORReport(ByVal dr As IDataReader) As List(Of PrudentialObj)
        Dim result As New List(Of PrudentialObj)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As New PrudentialObj
                    item.TransactionDate = .Item("OR_DATE").ToString()
                    item.JournalNumber = .Item("AM_JV_NO").ToString()
                    item.ReferenceNumber = .Item("OR_No").ToString()
                    item.ParticipantName = .Item("FULL_NAME").ToString()
                    item.Debit = CDec(.Item("DEBIT").ToString())
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

#Region "GetPrudentialDMCM"
    Public Function GetPrudentialDMCM(ByVal TransactionBegin As String, ByVal TransactionEnd As String) As List(Of PrudentialObj)
        Dim result As New List(Of PrudentialObj)
        Dim report As New DataReport

        Try
            Dim SQL As String = " SELECT TO_DATE(AM_DMCM.UPDATED_DATE,'MM/DD/YYYY') AS UPDATED_DATE ," & _
                                    " AM_DMCM.AM_DMCM_NO," & _
                                    " AM_DMCM.AM_JV_NO," & _
                                    " AM_PARTICIPANTS.FULL_NAME," & _
                                    " AM_DMCM_DETAILS.DEBIT" & _
                                " FROM AM_DMCM" & _
                                " INNER JOIN AM_DMCM_DETAILS" & _
                                " ON AM_DMCM.AM_DMCM_NO = AM_DMCM_DETAILS.AM_DMCM_NO" & _
                                " INNER JOIN AM_JV" & _
                                " ON AM_DMCM.AM_JV_NO = AM_JV.AM_JV_NO" & _
                                " LEFT JOIN AM_PARTICIPANTS" & _
                                " ON AM_DMCM.ID_NUMBER = AM_PARTICIPANTS.ID_NUMBER" & _
                                " LEFT JOIN AM_ADMIN_SETTINGS" & _
                                " ON AM_DMCM_DETAILS.ACCT_CODE = AM_ADMIN_SETTINGS.VALUE" & _
                                " WHERE AM_DMCM.UPDATED_DATE BETWEEN TO_DATE('" & TransactionBegin & "', 'mm/dd/yyyy','NLS_DATE_LANGUAGE = American')" & _
                                " AND TO_DATE('" & TransactionEnd & "', 'mm/dd/yyyy','NLS_DATE_LANGUAGE = American')" & _
                                " AND AM_DMCM.TRANS_TYPE = '" & EnumDMCMTransactionType.PrudentialInterest & "'" & _
                                " AND AM_ADMIN_SETTINGS.CODE_NAME = 'CashInBankPrudentialCode'" & _
                                " AND AM_DMCM.STATUS = 1"

            Me.DataAccess.ConnectionString = Me.ConnectionString
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.GetPrudentialDMCMReport(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
            'MsgBox(ex.ToString)
        End Try

        Return result
    End Function

    Private Function GetPrudentialDMCMReport(ByVal dr As IDataReader) As List(Of PrudentialObj)
        Dim result As New List(Of PrudentialObj)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As New PrudentialObj
                    item.TransactionDate = .Item("UPDATED_DATE").ToString()
                    item.JournalNumber = .Item("AM_DMCM_NO").ToString()
                    item.ReferenceNumber = .Item("AM_JV_NO").ToString()
                    item.ParticipantName = .Item("FULL_NAME").ToString()
                    item.Debit = CDec(.Item("DEBIT").ToString())
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

#Region "GeneratePrudentialReport"
    Public Function GeneratePrudentialReport(ByVal dt As DataTable, TransactionBegin As String, TransactionEnd As String, ByVal GeneratePrudentialObjReport As List(Of PrudentialObj)) As DataTable
        For Each item In GeneratePrudentialObjReport

            Dim row = dt.NewRow()
            row("TRANS_DATE") = DateTime.Parse(item.TransactionDate.ToString()).ToString("MM/dd/yyyy")
            row("JOURNAL_NUMBER") = item.JournalNumber.ToString()
            row("REFERENCE_NUMBER") = item.ReferenceNumber.ToString()
            row("DESCRIPTION") = item.ParticipantName.ToString()
            row("DEBIT") = item.Debit
            row("CREDIT") = item.Credit
            row("FROM_DATE") = TransactionBegin
            row("TO_DATE") = TransactionEnd
            dt.Rows.Add(row)

        Next
        Return dt
    End Function
#End Region

End Class
