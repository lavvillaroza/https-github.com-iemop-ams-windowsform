'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             WESMBillSummaryHistory
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     August 09, 2013
'Development Group:      Software Development and Support Division
'Description:            Class for Collection
'Arguments/Parameters:  
'Files/Database Tables:  AM_WESM_BILL_SUMMARY_HISTORY
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   August 09, 2013         Vladimir E. Espiritu            Class initialization

Option Explicit On
Option Strict On

Public Class WESMBillSummaryHistory

#Region "Initialization/Constructor"
    Public Sub New()
        Me.New(0, 0, "", Nothing, 0, Nothing, Nothing, Nothing)
    End Sub

    Public Sub New(ByVal WESMBillSummaryNo As Long, ByVal CollectionNumber As Long,
                    ByVal PaymentBatchCode As String, ByVal DueDate As Date, ByVal Amount As Decimal,
                    ByVal CollectionType As EnumCollectionType, ByVal Status As EnumStatus, ByVal PaymentType As EnumPaymentNewType)
        Me._WESMBillSummaryNo = WESMBillSummaryNo
        Me._CollectionNumber = CollectionNumber
        Me._PaymentBatchCode = PaymentBatchCode
        Me._DueDate = DueDate
        Me._Amount = Amount
        Me._CollectionType = CollectionType
        Me._PaymentType = PaymentType
        Me._Status = Status
    End Sub
#End Region

#Region "WESMBillSummaryNo"
    Private _WESMBillSummaryNo As Long
    Public Property WESMBillSummaryNo() As Long
        Get
            Return _WESMBillSummaryNo
        End Get
        Set(ByVal value As Long)
            _WESMBillSummaryNo = value
        End Set
    End Property

#End Region

#Region "CollectionNumber"
    Private _CollectionNumber As Long
    Public Property CollectionNumber() As Long
        Get
            Return _CollectionNumber
        End Get
        Set(ByVal value As Long)
            _CollectionNumber = value
        End Set
    End Property

#End Region

#Region "PaymentBatchCode"
    Private _PaymentBatchCode As String
    Public Property PaymentBatchCode() As String
        Get
            Return _PaymentBatchCode
        End Get
        Set(ByVal value As String)
            _PaymentBatchCode = value
        End Set
    End Property

#End Region

#Region "DueDate"
    Private _DueDate As Date
    Public Property DueDate() As Date
        Get
            Return _DueDate
        End Get
        Set(ByVal value As Date)
            _DueDate = value
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

#Region "CollectionType"
    Private _CollectionType As EnumCollectionType
    Public Property CollectionType() As EnumCollectionType
        Get
            Return _CollectionType
        End Get
        Set(ByVal value As EnumCollectionType)
            _CollectionType = value
        End Set
    End Property

#End Region

#Region "Payment Type"
    Private _PaymentType As EnumPaymentNewType
    Public Property PaymentType() As EnumPaymentNewType
        Get
            Return _PaymentType
        End Get
        Set(ByVal value As EnumPaymentNewType)
            _PaymentType = value
        End Set
    End Property
#End Region

#Region "Status"
    Private _Status As EnumStatus
    Public Property Status() As EnumStatus
        Get
            Return _Status
        End Get
        Set(ByVal value As EnumStatus)
            _Status = value
        End Set
    End Property

#End Region

End Class
