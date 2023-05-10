'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             STLNoticeDetails
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     November 14, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for STL Notice Details
'Arguments/Parameters:  
'Files/Database Tables:  AM_STL_NOTICE_DETAILS
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description

Option Explicit On
Option Strict On
Public Class STLNoticeDetails
    Public Sub New()
        Me._TransactionDate = SystemDate
        Me._CategoryType = Nothing
        Me._STLTransactionType = Nothing
        Me._BillPeriod = 0
        Me._InvoiceDate = SystemDate
        Me._SummaryType = Nothing
        Me._INVDMCMNo = ""
        Me._Remarks_1 = ""
        Me._Remarks_2 = ""
        Me._Remarks_3 = ""
        Me._Participant = New AMParticipants
        Me._Amount = 0
        Me.AllocationDate = SystemDate
    End Sub

    Private _TransactionDate As Date
    Public Property TransactionDate() As Date
        Get
            Return _TransactionDate
        End Get
        Set(ByVal value As Date)
            _TransactionDate = value
        End Set
    End Property

    Private _CategoryType As EnumSTLCategory
    Public Property CategoryType() As EnumSTLCategory
        Get
            Return _CategoryType
        End Get
        Set(ByVal value As EnumSTLCategory)
            _CategoryType = value
        End Set
    End Property

    Private _STLTransactionType As EnumSTLTransactionType
    Public Property STLTransactionType() As EnumSTLTransactionType
        Get
            Return _STLTransactionType
        End Get
        Set(ByVal value As EnumSTLTransactionType)
            _STLTransactionType = value
        End Set
    End Property

    Private _BillPeriod As Integer
    Public Property BillPeriod() As Integer
        Get
            Return _BillPeriod
        End Get
        Set(ByVal value As Integer)
            _BillPeriod = value
        End Set
    End Property

    Private _InvoiceDate As Date
    Public Property InvoiceDate() As Date
        Get
            Return _InvoiceDate
        End Get
        Set(ByVal value As Date)
            _InvoiceDate = value
        End Set
    End Property

    Private _SummaryType As EnumSummaryType
    Public Property SummaryType() As EnumSummaryType
        Get
            Return _SummaryType
        End Get
        Set(ByVal value As EnumSummaryType)
            _SummaryType = value
        End Set
    End Property

    Private _INVDMCMNo As String
    Public Property INVDMCMNo() As String
        Get
            Return _INVDMCMNo
        End Get
        Set(ByVal value As String)
            _INVDMCMNo = value
        End Set
    End Property

    Private _Remarks_1 As String
    Public Property Remarks_1() As String
        Get
            Return _Remarks_1
        End Get
        Set(ByVal value As String)
            _Remarks_1 = value
        End Set
    End Property

    Private _Remarks_2 As String
    Public Property Remarks_2() As String
        Get
            Return _Remarks_2
        End Get
        Set(ByVal value As String)
            _Remarks_2 = value
        End Set
    End Property

    Private _Remarks_3 As String
    Public Property Remarks_3() As String
        Get
            Return _Remarks_3
        End Get
        Set(ByVal value As String)
            _Remarks_3 = value
        End Set
    End Property

    Private _Participant As AMParticipants
    Public Property Participant() As AMParticipants
        Get
            Return _Participant
        End Get
        Set(ByVal value As AMParticipants)
            _Participant = value
        End Set
    End Property

    Private _Amount As Decimal
    Public Property Amount() As Decimal
        Get
            Return _Amount
        End Get
        Set(ByVal value As Decimal)
            _Amount = value
        End Set
    End Property


    Private _AllocationDate As Date
    Public Property AllocationDate() As Date
        Get
            Return _AllocationDate
        End Get
        Set(ByVal value As Date)
            _AllocationDate = value
        End Set
    End Property

End Class
