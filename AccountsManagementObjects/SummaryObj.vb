'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             Prudential
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     March 19, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for AM Participants
'Arguments/Parameters:  
'Files/Database Tables:  AM_PRUDENTIAL
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'

Public Class SummaryObj

    Public Sub New()
        Me._DocumentNumber = ""
        Me._DocumentDate = SystemDate
        Me._CustomerName = ""
        Me._CustomerNumber = ""
        Me._DocumentAmount = 0
        Me._AMSBatchCode = ""
        Me._GPReferenceNo = ""
        Me._BaseAmount = 0
        Me._VATAmount = 0
        Me._CheckNumber = ""
        Me._ChargeType = Nothing
    End Sub

    Public Sub New(ByVal DocumentNumber As String, ByVal DocumentDate As Date, ByVal CustomerNo As String, ByVal CustomerName As String, _
                   ByVal DocumentAmount As Decimal, ByVal AMSBatchCode As String, ByVal GPReference As String, ByVal BaseAMT As Decimal, _
                   ByVal VATAmt As Decimal, ByVal CheckNo As String, ByVal ChargeType As EnumChargeType)

        Me._DocumentNumber = DocumentNumber
        Me._DocumentDate = DocumentDate
        Me._CustomerNumber = CustomerNo
        Me._CustomerName = CustomerName
        Me._DocumentAmount = DocumentAmount
        Me._AMSBatchCode = AMSBatchCode
        Me._GPReferenceNo = GPReference
        Me._BaseAmount = BaseAMT
        Me._VATAmount = VATAmount
        Me._CheckNumber = CheckNo
        Me._ChargeType = ChargeType
    End Sub

    Private _DocumentNumber As String
    Public Property DocumentNumber() As String
        Get
            Return _DocumentNumber
        End Get
        Set(ByVal value As String)
            _DocumentNumber = value
        End Set
    End Property

    Private _DocumentDate As Date
    Public Property DocumentDate() As Date
        Get
            Return _DocumentDate
        End Get
        Set(ByVal value As Date)
            _DocumentDAte = value
        End Set
    End Property

    Private _CustomerNumber As String
    Public Property CustomerNumber() As String
        Get
            Return _CustomerNumber
        End Get
        Set(ByVal value As String)
            _CustomerNumber = value
        End Set
    End Property

    Private _CustomerName As String
    Public Property CustomerName() As String
        Get
            Return _CustomerName
        End Get
        Set(ByVal value As String)
            _CustomerName = value
        End Set
    End Property

    Private _DocumentAmount As Decimal
    Public Property DocumentAmount() As Decimal
        Get
            Return _DocumentAmount
        End Get
        Set(ByVal value As Decimal)
            _DocumentAmount = value
        End Set
    End Property


    Private _AMSBatchCode As String
    Public Property AMSBatchCode() As String
        Get
            Return _AMSBatchCode
        End Get
        Set(ByVal value As String)
            _AMSBatchCode = value
        End Set
    End Property

    Private _GPReferenceNo As String
    Public Property GPReferenceNo() As String
        Get
            Return _GPReferenceNo
        End Get
        Set(ByVal value As String)
            _GPReferenceNo = value
        End Set
    End Property

    Private _BaseAmount As Decimal
    Public Property BaseAmount() As Decimal
        Get
            Return _BaseAmount
        End Get
        Set(ByVal value As Decimal)
            _BaseAmount = value
        End Set
    End Property

    Private _VATAmount As Decimal
    Public Property VATAmount() As Decimal
        Get
            Return _VATAmount
        End Get
        Set(ByVal value As Decimal)
            _VATAmount = value
        End Set
    End Property

    Private _CheckNumber As String
    Public Property CheckNumber() As String
        Get
            Return _CheckNumber
        End Get
        Set(ByVal value As String)
            _CheckNumber = value
        End Set
    End Property

    Private _ChargeType As EnumChargeType
    Public Property ChargeType() As EnumChargeType
        Get
            Return _ChargeType
        End Get
        Set(ByVal value As EnumChargeType)
            _ChargeType = value
        End Set
    End Property

End Class
