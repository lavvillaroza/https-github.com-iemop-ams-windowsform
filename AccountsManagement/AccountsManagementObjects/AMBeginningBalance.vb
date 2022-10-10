
'***************************************************************************
'Copyright 2015: Philippine Electricity Market Corporation(PEMC)
'Class Name:             AMBeginningBalance
'Orginal Author:         Vladimir E.Espiritu
'File Creation Date:     December 08, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for Beginning Balance
'Arguments/Parameters:  
'Files/Database Tables:  AM_BEGINNING_BALANCE
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description
'   December 08, 2015       Vladimir E.Espiritu         Class initialization
'	

Option Explicit On
Option Strict On

Public Class AMBeginningBalance

#Region "Initialization/Constructor"
    Public Sub New()

    End Sub
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

#Region "AccountCode"
    Private _AccountCode As AccountingCode
    Public Property AccountCode() As AccountingCode
        Get
            Return _AccountCode
        End Get
        Set(ByVal value As AccountingCode)
            _AccountCode = value
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

#Region "Status"
    Private _Status As EnumStatus
    Public Property Status() As EnumStatus
        Get
            Return _Status
        End Get
        Set(ByVal value As EnumStatus)
            _Status = value
        End Set
    End Property

#End Region

End Class
