'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             NetSettlementSurplusAdjustmentReport
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     March 03, 2013
'Development Group:      Software Development and Support Division
'Description:            Class for NSSRA Report
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   March 03, 2013          Vladimir E. Espiritu            Class initialization
'   

Option Explicit On
Option Strict On

Public Class NetSettlementSurplusAdjustmentReport

#Region "Initialization/Constructor"
    Public Sub New()
        Me._IDNumber = New AMParticipants()
        Me._BillingPeriod1 = 0
        Me._InvoiceNumberNSS1 = 0
        Me._NSSAmount1 = 0
        Me._InvoiceNumberNSSRA1 = 0
        Me._NSSRAAmount1 = 0

        Me._BillingPeriod2 = 0
        Me._InvoiceNumberNSS2 = 0
        Me._NSSAmount2 = 0
        Me._InvoiceNumberNSSRA2 = 0
        Me._NSSRAAmount2 = 0

        Me._BillingPeriod3 = 0
        Me._InvoiceNumberNSS3 = 0
        Me._NSSAmount3 = 0
        Me._InvoiceNumberNSSRA3 = 0
        Me._NSSRAAmount3 = 0
    End Sub
#End Region



#Region "CategoryType"
    Private _CategoryType As EnumNSSRAReportCategory
    Public Property CategoryType() As EnumNSSRAReportCategory
        Get
            Return _CategoryType
        End Get
        Set(ByVal value As EnumNSSRAReportCategory)
            _CategoryType = value
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

#Region "BillingPeriod1"
    Private _BillingPeriod1 As Integer
    Public Property BillingPeriod1() As Integer
        Get
            Return _BillingPeriod1
        End Get
        Set(ByVal value As Integer)
            _BillingPeriod1 = value
        End Set
    End Property

#End Region

#Region "InvoiceNumberNSS1"
    Private _InvoiceNumberNSS1 As Long
    Public Property InvoiceNumberNSS1() As Long
        Get
            Return _InvoiceNumberNSS1
        End Get
        Set(ByVal value As Long)
            _InvoiceNumberNSS1 = value
        End Set
    End Property

#End Region

#Region "NSSAmount1"
    Private _NSSAmount1 As Decimal
    Public Property NSSAmount1() As Decimal
        Get
            Return _NSSAmount1
        End Get
        Set(ByVal value As Decimal)
            _NSSAmount1 = value
        End Set
    End Property


#End Region

#Region "InvoiceNumberNSSR1"
    Private _InvoiceNumberNSSRA1 As Long
    Public Property InvoiceNumberNSSRA1() As Long
        Get
            Return _InvoiceNumberNSSRA1
        End Get
        Set(ByVal value As Long)
            _InvoiceNumberNSSRA1 = value
        End Set
    End Property

#End Region

#Region "NSSRAAmount1"
    Private _NSSRAAmount1 As Decimal
    Public Property NSSRAAmount1() As Decimal
        Get
            Return _NSSRAAmount1
        End Get
        Set(ByVal value As Decimal)
            _NSSRAAmount1 = value
        End Set
    End Property


#End Region

#Region "BillingPeriod2"
    Private _BillingPeriod2 As Integer
    Public Property BillingPeriod2() As Integer
        Get
            Return _BillingPeriod2
        End Get
        Set(ByVal value As Integer)
            _BillingPeriod2 = value
        End Set
    End Property

#End Region

#Region "InvoiceNumberNSS2"
    Private _InvoiceNumberNSS2 As Long
    Public Property InvoiceNumberNSS2() As Long
        Get
            Return _InvoiceNumberNSS2
        End Get
        Set(ByVal value As Long)
            _InvoiceNumberNSS2 = value
        End Set
    End Property

#End Region

#Region "NSSAmount2"
    Private _NSSAmount2 As Decimal
    Public Property NSSAmount2() As Decimal
        Get
            Return _NSSAmount2
        End Get
        Set(ByVal value As Decimal)
            _NSSAmount2 = value
        End Set
    End Property


#End Region

#Region "InvoiceNumberNSSRA2"
    Private _InvoiceNumberNSSRA2 As Long
    Public Property InvoiceNumberNSSRA2() As Long
        Get
            Return _InvoiceNumberNSSRA2
        End Get
        Set(ByVal value As Long)
            _InvoiceNumberNSSRA2 = value
        End Set
    End Property

#End Region

#Region "NSSRAAmount2"
    Private _NSSRAAmount2 As Decimal
    Public Property NSSRAAmount2() As Decimal
        Get
            Return _NSSRAAmount2
        End Get
        Set(ByVal value As Decimal)
            _NSSRAAmount2 = value
        End Set
    End Property


#End Region

#Region "BillingPeriod3"
    Private _BillingPeriod3 As Integer
    Public Property BillingPeriod3() As Integer
        Get
            Return _BillingPeriod3
        End Get
        Set(ByVal value As Integer)
            _BillingPeriod3 = value
        End Set
    End Property

#End Region

#Region "InvoiceNumberNSS3"
    Private _InvoiceNumberNSS3 As Long
    Public Property InvoiceNumberNSS3() As Long
        Get
            Return _InvoiceNumberNSS3
        End Get
        Set(ByVal value As Long)
            _InvoiceNumberNSS3 = value
        End Set
    End Property

#End Region

#Region "NSSAmount3"
    Private _NSSAmount3 As Decimal
    Public Property NSSAmount3() As Decimal
        Get
            Return _NSSAmount3
        End Get
        Set(ByVal value As Decimal)
            _NSSAmount3 = value
        End Set
    End Property


#End Region

#Region "InvoiceNumberNSSRA3"
    Private _InvoiceNumberNSSRA3 As Long
    Public Property InvoiceNumberNSSRA3() As Long
        Get
            Return _InvoiceNumberNSSRA3
        End Get
        Set(ByVal value As Long)
            _InvoiceNumberNSSRA3 = value
        End Set
    End Property

#End Region

#Region "NSSRAAmount3"
    Private _NSSRAAmount3 As Decimal
    Public Property NSSRAAmount3() As Decimal
        Get
            Return _NSSRAAmount3
        End Get
        Set(ByVal value As Decimal)
            _NSSRAAmount3 = value
        End Set
    End Property


#End Region

End Class
