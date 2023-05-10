'***************************************************************************
'Copyright 2018: Philippine Electricity Market Corporation(PEMC)
'Class Name:             AccountingCode
'Orginal Author:         Vladimir E.Espiritu
'File Creation Date:     February 13, 2018
'Development Group:      Software Development and Support Division
'Description:            Class for Summary of Accounting Books
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description
'   February 13, 2018         Vladimir E.Espiritu         Class initialization
'	


Option Explicit On
Option Strict On

Public Class SummaryAccountingBooks

#Region "Initialization/Constructor"
    Public Sub New()
        Me._ItemAccountCode = New AccountingCode()
        Me._ItemParticipant = New AMParticipants()
    End Sub
#End Region


#Region "JVNo"
    Private _JVNo As String
    Public Property JVNo() As String
        Get
            Return _JVNo
        End Get
        Set(ByVal value As String)
            _JVNo = value
        End Set
    End Property

#End Region

#Region "JVDate"
    Private _JVDate As Date
    Public Property JVDate() As Date
        Get
            Return _JVDate
        End Get
        Set(ByVal value As Date)
            _JVDate = value
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

#Region "TransactionType"
    Private _TransactionType As String
    Public Property TransactionType() As String
        Get
            Return _TransactionType
        End Get
        Set(ByVal value As String)
            _TransactionType = value
        End Set
    End Property

#End Region

#Region "ItemParticipant"
    Private _ItemParticipant As AMParticipants
    Public Property ItemParticipant() As AMParticipants
        Get
            Return _ItemParticipant
        End Get
        Set(ByVal value As AMParticipants)
            _ItemParticipant = value
        End Set
    End Property

#End Region

#Region "ItemAccountCode"
    Private _ItemAccountCode As AccountingCode
    Public Property ItemAccountCode() As AccountingCode
        Get
            Return _ItemAccountCode
        End Get
        Set(ByVal value As AccountingCode)
            _ItemAccountCode = value
        End Set
    End Property

#End Region

#Region "Debit"
    Private _Debit As Decimal
    Public Property Debit() As Decimal
        Get
            Return _Debit
        End Get
        Set(ByVal value As Decimal)
            _Debit = value
        End Set
    End Property

#End Region

#Region "Credit"
    Private _Credit As Decimal
    Public Property Credit() As Decimal
        Get
            Return _Credit
        End Get
        Set(ByVal value As Decimal)
            _Credit = value
        End Set
    End Property

#End Region


End Class
