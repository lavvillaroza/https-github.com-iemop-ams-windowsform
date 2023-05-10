'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             WESMBillSummaryTransaction
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     August 27, 2015
'Development Group:      Software Development and Support Division
'Description:            Class for the Transactions of WESMBillSummary
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   September 01, 2015      Vladimir E. Espiritu            Class initialization
'


Public Class WESMBillSummaryTransaction

#Region "Initialization/Constructor"
    Public Sub New()

    End Sub
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

#Region "TransactionDate"
    Private _TransactionDate As Date
    Public Property TransactionDate() As Date
        Get
            Return _TransactionDate
        End Get
        Set(ByVal value As Date)
            _TransactionDate = value
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

#Region "ReferenceNo"
    Private _ReferenceNo As Long
    Public Property ReferenceNo() As Long
        Get
            Return _ReferenceNo
        End Get
        Set(ByVal value As Long)
            _ReferenceNo = value
        End Set
    End Property

#End Region

#Region "TransactionType"
    Private _TransactionType As BIRDocumentsType
    Public Property TransactionType() As BIRDocumentsType
        Get
            Return _TransactionType
        End Get
        Set(ByVal value As BIRDocumentsType)
            _TransactionType = value
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


End Class
