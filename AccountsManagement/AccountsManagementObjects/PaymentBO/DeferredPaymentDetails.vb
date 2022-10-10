'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             DeferredPaymentDetails
'Orginal Author:         Vladimir E.Espiritu
'File Creation Date:     September 29, 2015
'Development Group:      Software Development and Support Division
'Description:            Class for Deferred Payment Details
'Arguments/Parameters:  
'Files/Database Tables:  AM_DEFERRED_PAYMENT_DETAILS
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

Public Class DeferredPaymentDetails

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

#Region "DeferredAmount"
    Private _DeferredAmount As Decimal
    Public Property DeferredAmount() As Decimal
        Get
            Return _DeferredAmount
        End Get
        Set(value As Decimal)
            _DeferredAmount = value
        End Set
    End Property
#End Region

#Region "DeferredType"
    Private _DeferredType As New EnumDeferredType
    Public Property DeferredType() As EnumDeferredType
        Get
            Return _DeferredType
        End Get
        Set(value As EnumDeferredType)
            _DeferredType = value
        End Set
    End Property
#End Region

#Region "DMCM Number"
    Private _DMCMNumber As Long
    Public Property DMCMNumber() As Long
        Get
            Return _DMCMNumber
        End Get
        Set(ByVal value As Long)
            _DMCMNumber = value
        End Set
    End Property
#End Region


End Class
