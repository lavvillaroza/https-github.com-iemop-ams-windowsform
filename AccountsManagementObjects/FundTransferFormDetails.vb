'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             FundTransferFormDetails
'Orginal Author:         Vladimir E.Espiritu
'File Creation Date:     April 22, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Accounting Code
'Arguments/Parameters:  
'Files/Database Tables:  AM_FTF_DETAILS
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description
'   April 22, 2012          Vladimir E. Espiritu        Class initialization
'   May 10, 2012            Vladimir E. Espiritu        Changed Amount into Debit and Credit Property
'	

Option Explicit On
Option Strict On

<Serializable()> _
Public Class FundTransferFormDetails

#Region "Initialization/Constructor"
    Public Sub New()
        Me.New(0, "", "", 0, 0)
    End Sub

    Public Sub New(ByVal refno As Long, ByVal accountcode As String, ByVal bankaccount As String, _
                   ByVal debit As Decimal, ByVal credit As Decimal)
        Me._RefNo = refno
        Me._AccountCode = accountcode
        Me._BankAccountNo = bankaccount
        Me._IssuingBank = ""
        Me._ReceivingBank = ""
        Me._Debit = debit
        Me._Credit = credit
        Me._Particulars = ""
    End Sub

#End Region


#Region "RefNo"
    Private _RefNo As Long
    Public Property RefNo() As Long
        Get
            Return _RefNo
        End Get
        Set(ByVal value As Long)
            _RefNo = value
        End Set
    End Property

#End Region

#Region "SequenceNo"
    Private _SequenceNo As Integer
    Public Property SequenceNo() As Integer
        Get
            Return _SequenceNo
        End Get
        Set(ByVal value As Integer)
            _SequenceNo = value
        End Set
    End Property

#End Region

#Region "ACCOUNT CODE"
    Private _AccountCode As String
    Public Property AccountCode() As String
        Get
            Return _AccountCode
        End Get
        Set(ByVal value As String)
            _AccountCode = value
        End Set
    End Property
#End Region

#Region "BankAccountNo"
    Private _BankAccountNo As String
    Public Property BankAccountNo() As String
        Get
            Return _BankAccountNo
        End Get
        Set(ByVal value As String)
            _BankAccountNo = value
        End Set
    End Property

#End Region

#Region "IssuingBank"
    Private _IssuingBank As String
    Public Property IssuingBank() As String
        Get
            Return _IssuingBank
        End Get
        Set(ByVal value As String)
            _IssuingBank = value
        End Set
    End Property

#End Region

#Region "ReceivingBank"
    Private _ReceivingBank As String
    Public Property ReceivingBank() As String
        Get
            Return _ReceivingBank
        End Get
        Set(ByVal value As String)
            _ReceivingBank = value
        End Set
    End Property

#End Region

#Region "Particulars"
    Private _Particulars As String
    Public Property Particulars() As String
        Get
            Return _Particulars
        End Get
        Set(ByVal value As String)
            _Particulars = value
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
