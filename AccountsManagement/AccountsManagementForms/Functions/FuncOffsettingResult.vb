'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             FuncOffsettingResult
'Orginal Author:         Vladimir E.Espiritu
'File Creation Date:     February 27, 2012
'Development Group:      Software Development and Support Division
'Description:            Result class for FunctionOffsetting class
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description
'   February 27, 2012       Vladimir E.Espiritu         Class initialization
'   March 05, 2012          Vladimir E. Espiritu        Added WESMBillSummaryNo property
'

Option Explicit On
Option Strict On

Imports AccountsManagementObjects

Public Class FuncOffsettingResult

#Region "Initialization/Constructor"
    Public Sub New()
        Me._WESMBillSummaryNo = 0
        Me._DMCMNo = 0
        Me._JVNo = 0
        Me._OffsetNo = 0
        Me._ListOfDebitCreditMemo = New List(Of DebitCreditMemo)
        Me._ListOfOffsetting = New List(Of OffsetP2PC2CDetails)
        Me._ListOfJournalVoucher = New List(Of JournalVoucher)
        Me._ListOfGPPosted = New List(Of WESMBillGPPosted)
        Me._ListOfWESMBillSummary = New List(Of WESMBillSummary)
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

#Region "DMCMNo"
    Private _DMCMNo As Long
    Public Property DMCMNo() As Long
        Get
            Return _DMCMNo
        End Get
        Set(ByVal value As Long)
            _DMCMNo = value
        End Set
    End Property

#End Region

#Region "JVNo"
    Private _JVNo As Long
    Public Property JVNo() As Long
        Get
            Return _JVNo
        End Get
        Set(ByVal value As Long)
            _JVNo = value
        End Set
    End Property

#End Region

#Region "OffsetNo"
    Private _OffsetNo As Long
    Public Property OffsetNo() As Long
        Get
            Return _OffsetNo
        End Get
        Set(ByVal value As Long)
            _OffsetNo = value
        End Set
    End Property

#End Region

#Region "ListOfDebitCreditMemo"
    Private _ListOfDebitCreditMemo As List(Of DebitCreditMemo)
    Public Property ListOfDebitCreditMemo() As List(Of DebitCreditMemo)
        Get
            Return _listOfDebitCreditMemo
        End Get
        Set(ByVal value As List(Of DebitCreditMemo))
            _listOfDebitCreditMemo = value
        End Set
    End Property
#End Region

#Region "ListOfOffsetting"
    Private _ListOfOffsetting As List(Of OffsetP2PC2CDetails)
    Public Property ListOfOffsetting() As List(Of OffsetP2PC2CDetails)
        Get
            Return _ListOfOffsetting
        End Get
        Set(ByVal value As List(Of OffsetP2PC2CDetails))
            _ListOfOffsetting = value
        End Set
    End Property

#End Region

#Region "ListOfJournalVoucher"
    Private _ListOfJournalVoucher As List(Of JournalVoucher)
    Public Property ListOfJournalVoucher() As List(Of JournalVoucher)
        Get
            Return _ListOfJournalVoucher
        End Get
        Set(ByVal value As List(Of JournalVoucher))
            _ListOfJournalVoucher = value
        End Set
    End Property

#End Region

#Region "ListOfGPPosted"
    Private _ListOfGPPosted As List(Of WESMBillGPPosted)
    Public Property ListOfGPPosted() As List(Of WESMBillGPPosted)
        Get
            Return _ListOfGPPosted
        End Get
        Set(ByVal value As List(Of WESMBillGPPosted))
            _ListOfGPPosted = value
        End Set
    End Property

#End Region

#Region "ListOfWESMBillSummary"
    Private _ListOfWESMBillSummary As List(Of WESMBillSummary)
    Public Property ListOfWESMBillSummary() As List(Of WESMBillSummary)
        Get
            Return _ListOfWESMBillSummary
        End Get
        Set(ByVal value As List(Of WESMBillSummary))
            _ListOfWESMBillSummary = value
        End Set
    End Property

#End Region

End Class
