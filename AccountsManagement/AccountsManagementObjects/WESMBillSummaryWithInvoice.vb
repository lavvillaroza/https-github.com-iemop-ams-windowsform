'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             WESMBillSummaryWithInvoiceAmount
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     September 01, 2015
'Development Group:      Software Development and Support Division
'Description:            Class for WESMBillSummary the Original Invoice Amount
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   September 01, 2015      Vladimir E. Espiritu            Class initialization
'   October 21, 2019        Lance Arjay Villaroza           additional properties: ListOfWESMBillTransactionFromOffsetCollection, ListOfWESMBillTransactionFromOffsetPaymentAllocation


Public Class WESMBillSummaryWithInvoice

#Region "Initialization/Constructor"
    Public Sub New()

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

#Region "ChargeType"
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

#Region "InvoiceAmount"
    Private _InvoiceAmount As Decimal
    Public Property InvoiceAmount() As Decimal
        Get
            Return _InvoiceAmount
        End Get
        Set(ByVal value As Decimal)
            _InvoiceAmount = value
        End Set
    End Property

#End Region

#Region "BeginningBalance"
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

#Region "EndingBalance"
    Private _EndingBalance As Decimal
    Public Property EndingBalance() As Decimal
        Get
            Return _EndingBalance
        End Get
        Set(ByVal value As Decimal)
            _EndingBalance = value
        End Set
    End Property
#End Region

#Region "EnergyWithhold"

    Private _EnergyWithhold As Decimal
    Public Property EnergyWithhold() As Decimal
        Get
            Return _EnergyWithhold
        End Get
        Set(ByVal value As Decimal)
            _EnergyWithhold = value
        End Set
    End Property

#End Region

#Region "ListOfWESMBillTransactionFromCollection"
    Private _ListOfWESMBillTransactionFromCollection As List(Of WESMBillSummaryTransaction)
    Public Property ListOfWESMBillTransactionFromCollection() As List(Of WESMBillSummaryTransaction)
        Get
            Return _ListOfWESMBillTransactionFromCollection
        End Get
        Set(ByVal value As List(Of WESMBillSummaryTransaction))
            _ListOfWESMBillTransactionFromCollection = value
        End Set
    End Property


#End Region

#Region "ListOfWESMBillTransactionFromParentChildOffsetting"
    Private _ListOfWESMBillTransactionFromParentChildOffsetting As List(Of WESMBillSummaryTransaction)
    Public Property ListOfWESMBillTransactionFromParentChildOffsetting() As List(Of WESMBillSummaryTransaction)
        Get
            Return _ListOfWESMBillTransactionFromParentChildOffsetting
        End Get
        Set(ByVal value As List(Of WESMBillSummaryTransaction))
            _ListOfWESMBillTransactionFromParentChildOffsetting = value
        End Set
    End Property

#End Region

#Region "ListOfWESMBillTransactionFromPaymentAllocation"
    Private _ListOfWESMBillTransactionFromPaymentAllocation As List(Of WESMBillSummaryTransaction)
    Public Property ListOfWESMBillTransactionFromPaymentAllocation() As List(Of WESMBillSummaryTransaction)
        Get
            Return _ListOfWESMBillTransactionFromPaymentAllocation
        End Get
        Set(ByVal value As List(Of WESMBillSummaryTransaction))
            _ListOfWESMBillTransactionFromPaymentAllocation = value
        End Set
    End Property
#End Region

#Region "ListOfWESMBillTransactionFromSPA"
    Private _ListOfWESMBillTransactionFromSPA As List(Of WESMBillSummaryTransaction)
    Public Property ListOfWESMBillTransactionFromSPA() As List(Of WESMBillSummaryTransaction)
        Get
            Return _ListOfWESMBillTransactionFromSPA
        End Get
        Set(ByVal value As List(Of WESMBillSummaryTransaction))
            _ListOfWESMBillTransactionFromSPA = value
        End Set
    End Property

#End Region

#Region "BalanceType"
    Private _BalanceType As String
    Public Property BalanceType() As String
        Get
            Return _BalanceType
        End Get
        Set(ByVal value As String)
            _BalanceType = value
        End Set
    End Property
#End Region

End Class
