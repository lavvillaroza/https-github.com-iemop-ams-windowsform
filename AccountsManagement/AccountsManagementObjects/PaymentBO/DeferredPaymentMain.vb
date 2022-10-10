'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             DeferredPaymentMain
'Orginal Author:         Vladimir E.Espiritu
'File Creation Date:     September 29, 2015 
'Development Group:      Software Development and Support Division
'Description:            Deferred Payment Main
'Arguments/Parameters:  
'Files/Database Tables:  AM_DEFERRED_PAYMENT_MAIN
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description
'   September 29, 2015      Vladimir E.Espiritu         Class initialization
'	

Option Explicit On
Option Strict On

Public Class DeferredPaymentMain

#Region "AllocationDate"
    Private _AllocationDate As Date
    Public Property AllocationDate() As Date
        Get
            Return _AllocationDate
        End Get
        Set(value As Date)
            _AllocationDate = value
        End Set
    End Property
#End Region

#Region "IDNumber"
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

#Region "DeferredPaymentNo"
    Private _DeferredPaymentNo As Long
    Public Property DeferredPaymentNo() As Long
        Get
            Return _DeferredPaymentNo
        End Get
        Set(value As Long)
            _DeferredPaymentNo = value
        End Set
    End Property
#End Region

#Region "OutstandingDeferredPayment"
    Private _OutstandingDeferredPayment As Decimal
    Public Property OutstandingDeferredPayment() As Decimal
        Get
            Return _OutstandingDeferredPayment
        End Get
        Set(value As Decimal)
            _OutstandingDeferredPayment = value
        End Set
    End Property
#End Region

#Region "PaymentNo"
    Private _PaymentNo As Long
    Public Property PaymentNo() As Long
        Get
            Return _PaymentNo
        End Get
        Set(value As Long)
            _PaymentNo = value
        End Set
    End Property
#End Region

#Region "Charge Type"
    Private _ChargeType As EnumChargeType

    Public Property ChargeType() As EnumChargeType
        Get
            Return _ChargeType
        End Get
        Set(ByVal value As EnumChargeType)
            _ChargeType = value
        End Set
    End Property

#End Region

End Class
