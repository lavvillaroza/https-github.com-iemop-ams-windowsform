'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             GeneralLedger
'Orginal Author:         Joseph B. Gabriel
'File Creation Date:     July 03,2015
'Development Group:      Software Development and Support Division
'Description:            Class for Collection
'Arguments/Parameters:  
'Files/Database Tables:  NONE
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   
'

Public Class PrudentialObj

#Region "Transaction Date"
    Private _TransDate As String
    Public Property TransactionDate() As String
        Get
            Return _TransDate
        End Get
        Set(ByVal value As String)
            _TransDate = value
        End Set
    End Property
#End Region

#Region "Journal Number"
    Private _JournalNumber As String
    Public Property JournalNumber() As String
        Get
            Return _JournalNumber
        End Get
        Set(ByVal value As String)
            _JournalNumber = value
        End Set
    End Property

#End Region

#Region "Reference Number"
    Private _ReferenceNumber As String
    Public Property ReferenceNumber() As String
        Get
            Return _ReferenceNumber
        End Get
        Set(ByVal value As String)
            _ReferenceNumber = value
        End Set
    End Property

#End Region

#Region "Participant Name"
    Private _ParticipantName As String
    Public Property ParticipantName() As String
        Get
            Return _ParticipantName
        End Get
        Set(ByVal value As String)
            _ParticipantName = value
        End Set
    End Property
#End Region

#Region "Debit"
    Private _Debit As Decimal
    Public Property Debit() As Decimal
        Get
            Return _Debit
        End Get
        Set(ByVal value As Decimal)
            _Debit = value
        End Set
    End Property
#End Region

#Region "Credit"
    Private _Credit As Decimal
    Public Property Credit() As Decimal
        Get
            Return _Credit
        End Get
        Set(ByVal value As Decimal)
            _Credit = value
        End Set
    End Property
#End Region

#Region "Account Number"
    Private _AccountNumber As String
    Public Property AccountNumber() As String
        Get
            Return _AccountNumber
        End Get
        Set(ByVal value As String)
            _AccountNumber = value
        End Set
    End Property
#End Region

#Region "Beginning Balance"
    Private _BeginningBalance As Decimal
    Public Property BeginningBalance() As Decimal
        Get
            Return _BeginningBalance
        End Get
        Set(ByVal value As Decimal)
            _BeginningBalance = value
        End Set
    End Property
#End Region

End Class
