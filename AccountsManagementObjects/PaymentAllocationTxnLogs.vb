Option Explicit On
Option Strict On

Public Class PaymentAllocationTxnLogs

#Region "Properties"
    Private _TransactionID As Long
    Public Property TransactionID() As Long
        Get
            Return _TransactionID
        End Get
        Set(ByVal value As Long)
            _TransactionID = value
        End Set
    End Property

    Private _ParticipantID As String
    Public Property ParticipantID() As String
        Get
            Return _ParticipantID
        End Get
        Set(ByVal value As String)
            _ParticipantID = value
        End Set
    End Property

    Private _WESMBillNo As String
    Public Property WESMBillNo() As String
        Get
            Return _WESMBillNo
        End Get
        Set(ByVal value As String)
            _WESMBillNo = value
        End Set
    End Property

    Private _OutstandingBalance As Decimal
    Public Property OutstandingBalance() As Decimal
        Get
            Return _OutstandingBalance
        End Get
        Set(ByVal value As Decimal)
            _OutstandingBalance = value
        End Set
    End Property

    Private _PaymentAmount As Decimal
    Public Property PaymentAmount() As Decimal
        Get
            Return _PaymentAmount
        End Get
        Set(ByVal value As Decimal)
            _PaymentAmount = value
        End Set
    End Property

    Private _TransactionType As String
    Public Property TransactionType() As String
        Get
            Return _TransactionType
        End Get
        Set(ByVal value As String)
            _TransactionType = value
        End Set
    End Property

    Private _BillPeriod As Long
    Public Property BillPeriod() As Long
        Get
            Return _BillPeriod
        End Get
        Set(ByVal value As Long)
            _BillPeriod = value
        End Set
    End Property

    Private _DueDate As Date
    Public Property DueDate() As Date
        Get
            Return _DueDate
        End Get
        Set(ByVal value As Date)
            _DueDate = value
        End Set
    End Property

    Private _ChargeType As EnumChargeType
    Public Property ChargeType() As EnumChargeType
        Get
            Return _ChargeType
        End Get
        Set(ByVal value As EnumChargeType)
            _ChargeType = value
        End Set
    End Property

    Private _OffsettingNo As Long
    Public Property OffsettingNo() As Long
        Get
            Return _OffsettingNo
        End Get
        Set(ByVal value As Long)
            _OffsettingNo = value
        End Set
    End Property


    Private _CreatedDocument As String
    Public Property CreatedDocument() As String
        Get
            Return _CreatedDocument
        End Get
        Set(ByVal value As String)
            _CreatedDocument = value
        End Set
    End Property



#End Region
    
End Class
