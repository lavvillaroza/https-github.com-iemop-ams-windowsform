'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             SubsidiaryLedgerAR
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     August 26,2015
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
Public Class SubsidiaryLedgerAR

#Region "Initialization/Constructor"
    Public Sub New()

    End Sub

#End Region

#Region "RecordID"
    Private _RecordID As Integer
    Public Property RecordID() As Integer
        Get
            Return _RecordID
        End Get
        Set(ByVal value As Integer)
            _RecordID = value
        End Set
    End Property

#End Region

#Region "IDNumber"
    Private _IDNumber As AMParticipants
    Public Property IDNumber() As AMParticipants
        Get
            Return _IDNumber
        End Get
        Set(ByVal value As AMParticipants)
            _IDNumber = value
        End Set
    End Property

#End Region

#Region "CurrentDate"
    Private _CurrentDate As Date
    Public Property CurrentDate() As Date
        Get
            Return _CurrentDate
        End Get
        Set(ByVal value As Date)
            _CurrentDate = value
        End Set
    End Property

#End Region

#Region "TransactionDate"
    Private _TransactionDate As Date
    Public Property TransactionDate() As Date
        Get
            Return _TransactionDate
        End Get
        Set(ByVal value As Date)
            _TransactionDate = value
        End Set
    End Property

#End Region

#Region "InvoiceTransactionDate"
    Private _InvoiceTransactionDate As Date
    Public Property InvoiceTransactionDate() As Date
        Get
            Return _InvoiceTransactionDate
        End Get
        Set(ByVal value As Date)
            _InvoiceTransactionDate = value
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

#Region "ChargeTypeValue"
    Private _ChargeTypeValue As String
    Public Property ChargeTypeValue() As String
        Get
            Return _ChargeTypeValue
        End Get
        Set(ByVal value As String)
            _ChargeTypeValue = value
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

#Region "Amount"
    Private _Amount As Decimal
    Public Property Amount() As Decimal
        Get
            Return _Amount
        End Get
        Set(ByVal value As Decimal)
            _Amount = value
        End Set
    End Property

#End Region

#Region "CurrentAmount"
    Private _CurrentAmount As Decimal
    Public ReadOnly Property CurrentAmount() As Decimal
        Get
            Dim _dateDiff = DateDiff(DateInterval.DayOfYear, Me.InvoiceTransactionDate, Me.CurrentDate)

            If _dateDiff <= 30 Then
                _CurrentAmount = Me.Amount
            Else
                _CurrentAmount = 0
            End If

            Return _CurrentAmount
        End Get
    End Property


#End Region

#Region "Amount31to60"
    Private _Amount31to60 As Decimal
    Public ReadOnly Property Amount31to60() As Decimal
        Get
            Dim _dateDiff = DateDiff(DateInterval.DayOfYear, Me.InvoiceTransactionDate, Me.CurrentDate)

            If _dateDiff >= 31 And _dateDiff <= 60 Then
                _Amount31to60 = Me.Amount
            Else
                _Amount31to60 = 0
            End If

            Return _Amount31to60
        End Get
    End Property

#End Region

#Region "Amount61to90"
    Private _Amount61to90 As Decimal
    Public ReadOnly Property Amount61to90() As Decimal
        Get
            Dim _dateDiff = DateDiff(DateInterval.DayOfYear, Me.InvoiceTransactionDate, Me.CurrentDate)

            If _dateDiff >= 61 And _dateDiff <= 90 Then
                _Amount61to90 = Me.Amount
            Else
                _Amount61to90 = 0
            End If

            Return _Amount61to90
        End Get
    End Property

#End Region

#Region "Amount91over"
    Private _Amount91over As Decimal
    Public ReadOnly Property Amount91over() As Decimal
        Get
            Dim _dateDiff = DateDiff(DateInterval.DayOfYear, Me.InvoiceTransactionDate, Me.CurrentDate)

            If _dateDiff >= 91 Then
                _Amount91over = Me.Amount
            Else
                _Amount91over = 0
            End If

            Return _Amount91over
        End Get
    End Property

#End Region


End Class
