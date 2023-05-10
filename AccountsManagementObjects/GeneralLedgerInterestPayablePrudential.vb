'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             GeneralLedgerInterestPayablePrudential
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     August 12,2015
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

Public Class GeneralLedgerInterestPayablePrudential

#Region "Initialization/Constructor"
    Public Sub New()

    End Sub

#End Region

#Region "Transaction Date"
    Private _TransDate As Date
    Public Property TransactionDate() As Date
        Get
            Return _TransDate
        End Get
        Set(ByVal value As Date)
            _TransDate = value
        End Set
    End Property
#End Region

#Region "TransactionType"
    Private _TransactionType As BIRDocumentsType
    Public Property TransactionType() As BIRDocumentsType
        Get
            Return _TransactionType
        End Get
        Set(ByVal value As BIRDocumentsType)
            _TransactionType = value
        End Set
    End Property

#End Region

#Region "Description"
    Private _Description As String
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property
#End Region

#Region "Journal Number"
    Private _JournalNumber As Long
    Public Property JournalNumber() As Long
        Get
            Return _JournalNumber
        End Get
        Set(ByVal value As Long)
            _JournalNumber = value
        End Set
    End Property

#End Region

#Region "Reference Number"
    Private _ReferenceNumber As Long
    Public Property ReferenceNumber() As Long
        Get
            Return _ReferenceNumber
        End Get
        Set(ByVal value As Long)
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

End Class
