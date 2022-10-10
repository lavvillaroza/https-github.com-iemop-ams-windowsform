'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             PaymentAllocationSummary
'Orginal Author:         Juan Carlo Panopio
'File Creation Date:     
'Development Group:      Software Development and Support Division
'Description:            Class for Summary of Allocations in Payment
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   
'

Public Class PaymentAllocation


#Region "Initialization"

    Public Sub New()
        Me.New(SystemDate, 0, SystemDate, New AMParticipants, 0, Nothing, New WESMBillSummary)
    End Sub

    Public Sub New(ByVal AllocDate As Date, ByVal BillingPeriod As Integer, ByVal DateDue As Date, ByVal Participant As AMParticipants, _
                   ByVal Amount As Decimal, ByVal PaymentAllocType As EnumPaymentAllocationType, ByVal WESMSummary As WESMBillSummary)

        Me._AllocationDate = AllocDate
        Me._BillPeriod = BillingPeriod
        Me._DueDate = DateDue
        Me._Participant = Participant
        Me._Amount = Amount
        Me._AllocationType = PaymentAllocType
        Me._WESMSummary = WESMSummary

    End Sub

#End Region

#Region "Allocation Date"

    Private _AllocationDate As Date
    Public Property AllocationDate() As Date
        Get
            Return _AllocationDate
        End Get
        Set(ByVal value As Date)
            _AllocationDate = value
        End Set
    End Property

#End Region

#Region "Billing Period"

    Private _BillPeriod As Integer
    Public Property BillPeriod() As Integer
        Get
            Return _BillPeriod
        End Get
        Set(ByVal value As Integer)
            _BillPeriod = value
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

#Region "Participant"

    Private _Participant As AMParticipants
    Public Property Participant() As AMParticipants
        Get
            Return _Participant
        End Get
        Set(ByVal value As AMParticipants)
            _Participant = value
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

#Region "Allocation Type"

    Private _AllocationType As EnumPaymentAllocationType
    Public Property AllocationType() As EnumPaymentAllocationType
        Get
            Return _AllocationType
        End Get
        Set(ByVal value As EnumPaymentAllocationType)
            _AllocationType = value
        End Set
    End Property

#End Region

#Region "WESMBill Summary"

    Private _WESMSummary As WESMBillSummary
    Public Property WESMSummary() As WESMBillSummary
        Get
            Return _WESMSummary
        End Get
        Set(ByVal value As WESMBillSummary)
            _WESMSummary = value
        End Set
    End Property

#End Region

End Class
