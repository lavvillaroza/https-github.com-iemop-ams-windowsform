'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             WESMInvoice
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     February 02, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for WESM Invoice
'Arguments/Parameters:  
'Files/Database Tables:  WESM Bill file coming from BSMD in csv format. AM_WESM_BILL and WESM_BILL_AUDIT database table
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   September 14, 2011      Vladimir E. Espiritu            Class initialization
'   October 30, 2012        Vladimir E. Espiritu            Added MarketFeesRate and Remarks properties
'   August 27, 2013         Vladimir E. Espiritu            Added IDNumber, RegistrationID, ForTheAccountOf and FullName properties     
'   August 27, 2013         Vladimir E. Espiritu            Removed ReferenceNo
'   January 26, 2015        Vladimir E. Espiritu            Added WESM Invoice Code
'

Option Explicit On
Option Strict On
Public Class WESMInvoice

#Region "Initialization/Constructor"
    Public Sub New()
        Me._FileType = Nothing
        Me._BillingPeriod = Nothing
        Me._SettlementRun = ""
        Me._IDNumber = ""
        Me._RegistrationID = ""
        Me._ForTheAccountOf = ""
        Me._FullName = ""
        Me._InvoiceNumber = ""
        Me._InvoiceDate = Nothing
        Me._Quantity = 0
        Me._Amount = 0
        Me._ChargeID = ""
        Me._DueDate = Nothing
        Me._InvoiceCode = 0
    End Sub
#End Region


#Region "FileType"
    Private _FileType As EnumFileType
    Public Property FileType() As EnumFileType
        Get
            Return _FileType
        End Get
        Set(ByVal value As EnumFileType)
            _FileType = value
        End Set
    End Property


#End Region

#Region "BillingPeriod"
    Private _BillingPeriod As Integer
    Public Property BillingPeriod() As Integer
        Get
            Return _BillingPeriod
        End Get
        Set(ByVal value As Integer)
            _BillingPeriod = value
        End Set
    End Property
#End Region

#Region "SettlementRun"
    Private _SettlementRun As String
    Public Property SettlementRun() As String
        Get
            Return _SettlementRun
        End Get
        Set(ByVal value As String)
            _SettlementRun = value
        End Set
    End Property
#End Region

#Region "SettlementID"
    Private _IDNumber As String
    Public Property IDNumber() As String
        Get
            Return _IDNumber
        End Get
        Set(ByVal value As String)
            _IDNumber = value
        End Set
    End Property

#End Region

#Region "RegistrationID"
    Private _RegistrationID As String
    Public Property RegistrationID() As String
        Get
            Return _RegistrationID
        End Get
        Set(ByVal value As String)
            _RegistrationID = value
        End Set
    End Property

#End Region

#Region "ForTheAccountOf"
    Private _ForTheAccountOf As String
    Public Property ForTheAccountOf() As String
        Get
            Return _ForTheAccountOf
        End Get
        Set(ByVal value As String)
            _ForTheAccountOf = value
        End Set
    End Property

#End Region

#Region "FullName"
    Private _FullName As String
    Public Property FullName() As String
        Get
            Return _FullName
        End Get
        Set(ByVal value As String)
            _FullName = value
        End Set
    End Property

#End Region

#Region "InvoiceNumber"
    Private _InvoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return _InvoiceNumber
        End Get
        Set(ByVal value As String)
            _InvoiceNumber = value
        End Set
    End Property
#End Region

#Region "InvoiceDate"
    Private _InvoiceDate As Date
    Public Property InvoiceDate() As Date
        Get
            Return _InvoiceDate
        End Get
        Set(ByVal value As Date)
            _InvoiceDate = value
        End Set
    End Property

#End Region

#Region "Quantity"
    Private _Quantity As Decimal
    Public Property Quantity() As Decimal
        Get
            Return _Quantity
        End Get
        Set(ByVal value As Decimal)
            _Quantity = value
        End Set
    End Property

#End Region

#Region "ChargeID"
    Private _ChargeID As String
    Public Property ChargeID() As String
        Get
            Return _ChargeID
        End Get
        Set(ByVal value As String)
            _ChargeID = value
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

#Region "MarketFeesRate"
    Private _MarketFeesRate As Decimal
    Public Property MarketFeesRate() As Decimal
        Get
            Return _MarketFeesRate
        End Get
        Set(ByVal value As Decimal)
            _MarketFeesRate = value
        End Set
    End Property

#End Region

#Region "Remarks"
    Private _Remarks As String
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property

#End Region

#Region "InvoiceCode"
    Private _InvoiceCode As Long
    Public Property InvoiceCode() As Long
        Get
            Return _InvoiceCode
        End Get
        Set(ByVal value As Long)
            _InvoiceCode = value
        End Set
    End Property

#End Region


End Class
