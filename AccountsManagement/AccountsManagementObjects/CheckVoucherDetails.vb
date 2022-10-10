'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             CheckVoucherDetails
'Orginal Author:         Juan Carlo Panopio
'File Creation Date:     January 7, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Check Voucher Details
'Arguments/Parameters:  
'Files/Database Tables:  AM_CHECK_VOUCHER_DETAILS
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description
'

Public Class CheckVoucherDetails

    Public Sub New()
        Me._VoucherNumber = ""
        Me._AccountCode = ""
        Me._Debit = 0
        Me._Credit = 0
    End Sub

    Public Sub New(ByVal VoucherNumber As String, ByVal AcctCode As String, ByVal Debit As Decimal, ByVal Credit As Decimal)
        Me._VoucherNumber = VoucherNumber
        Me._AccountCode = AcctCode
        Me._Debit = Debit
        Me._Credit = Credit
    End Sub

    Private _VoucherNumber As String
    Public Property VoucherNumber() As String
        Get
            Return _VoucherNumber
        End Get
        Set(ByVal value As String)
            _VoucherNumber = value
        End Set
    End Property


    Private _AccountCode As String
    Public Property AccountCode() As String
        Get
            Return _AccountCode
        End Get
        Set(ByVal value As String)
            _AccountCode = value
        End Set
    End Property


    Private _Debit As Decimal
    Public Property Debit() As Decimal
        Get
            Return _Debit
        End Get
        Set(ByVal value As Decimal)
            _Debit = value
        End Set
    End Property


    Private _Credit As Decimal
    Public Property Credit() As Decimal
        Get
            Return _Credit
        End Get
        Set(ByVal value As Decimal)
            _Credit = value
        End Set
    End Property

End Class
