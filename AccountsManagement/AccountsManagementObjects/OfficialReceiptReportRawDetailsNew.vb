'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             OfficialReceiptReportRawDetails
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     November 18, 2013
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
'   November 18, 2012       Vladimir E. Espiritu            Class initialization
'
Option Explicit On
Option Strict On

Public Class OfficialReceiptReportRawDetailsNew

#Region "Initialization/Constructor"
    Public Sub New()
        Me.New(0, "", EnumDocumentType.NONE, Nothing, 0, Nothing, 0, 0, 0, 0, 0, Nothing)
    End Sub

    Public Sub New(ByVal orno As Long, ByVal documentno As String, ByVal documenttype As EnumDocumentType, ByVal documentdate As Date, ByVal wbillno As Long, _
                   ByVal duedate As Date, ByVal amount As Decimal, ByVal vat As Decimal, ByVal defaultinterest As Decimal, _
                   ByVal withholdingtax As Decimal, ByVal withholdingvat As Decimal, ByVal transactiontype As EnumORTransactionType)
        Me._ORNo = orno
        Me._DocumentNo = documentno
        Me._DocumentType = documenttype
        Me._DocumentDate = documentdate
        Me._WESMBillSummaryNo = wbillno
        Me._DueDate = duedate
        Me._Amount = amount
        Me._Vat = vat
        Me._DefaultInterest = defaultinterest
        Me._WithHoldingTax = withholdingtax
        Me._WithHoldingVat = withholdingvat
        Me._TransactionType = transactiontype
    End Sub
#End Region


#Region "ORNo"
    Private _ORNo As Long
    Public Property ORNo() As Long
        Get
            Return _ORNo
        End Get
        Set(ByVal value As Long)
            _ORNo = value
        End Set
    End Property

#End Region

#Region "DocumentNo"
    Private _DocumentNo As String
    Public Property DocumentNo() As String
        Get
            Return _DocumentNo
        End Get
        Set(ByVal value As String)
            _DocumentNo = value
        End Set
    End Property


#End Region

#Region "DocumentType"
    Private _DocumentType As EnumDocumentType
    Public Property DocumentType() As EnumDocumentType
        Get
            Return _DocumentType
        End Get
        Set(ByVal value As EnumDocumentType)
            _DocumentType = value
        End Set
    End Property

#End Region

#Region "DocumentDate"
    Private _DocumentDate As Date
    Public Property DocumentDate() As Date
        Get
            Return _DocumentDate
        End Get
        Set(ByVal value As Date)
            _DocumentDate = value
        End Set
    End Property

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

#Region "VAT"
    Private _Vat As Decimal
    Public Property Vat() As Decimal
        Get
            Return _Vat
        End Get
        Set(ByVal value As Decimal)
            _Vat = value
        End Set
    End Property
#End Region

#Region "DefaultInterest"
    Private _DefaultInterest As Decimal
    Public Property DefaultInterest() As Decimal
        Get
            Return _DefaultInterest
        End Get
        Set(ByVal value As Decimal)
            _DefaultInterest = value
        End Set
    End Property
#End Region

#Region "WithHoldingTax"
    Private _WithHoldingTax As Decimal
    Public Property WithHoldingTax() As Decimal
        Get
            Return _WithHoldingTax
        End Get
        Set(ByVal value As Decimal)
            _WithHoldingTax = value
        End Set
    End Property
#End Region

#Region "WithHoldingVat"
    Private _WithHoldingVat As Decimal
    Public Property WithHoldingVat() As Decimal
        Get
            Return _WithHoldingVat
        End Get
        Set(ByVal value As Decimal)
            _WithHoldingVat = value
        End Set
    End Property
#End Region

#Region "TransactionType"
    Private _TransactionType As EnumORTransactionType
    Public Property TransactionType() As EnumORTransactionType
        Get
            Return _TransactionType
        End Get
        Set(ByVal value As EnumORTransactionType)
            _TransactionType = value
        End Set
    End Property

#End Region

End Class
