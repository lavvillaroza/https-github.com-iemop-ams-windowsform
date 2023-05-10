'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             OfficialReceiptMain
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     February 10, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Collection
'Arguments/Parameters:  
'Files/Database Tables:  AM_OFFICIAL_RECEIPT_MAIN
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   February 10, 2012       Vladimir E. Espiritu            Class initialization
'   May 07, 2012            Vladimir E. Espiritu            Added ListORDetails property
'   May 09, 2012            Vladimir E. Espiritu            Added Batchcode
'   September 24, 2012      Vladimir E. Espiritu            Added Remarks property
'

Public Class OfficialReceiptMain

#Region "Initialization/Constructor"
    Public Sub New()
        Me.New(0, Nothing, 0, 0, 1, New List(Of OfficialReceiptDetails))
    End Sub

    Public Sub New(ByVal orno As Long, ByVal ordate As Date, ByVal idnumber As String, ByVal amount As Decimal, _
                    ByVal status As Integer, ByVal listordetails As List(Of OfficialReceiptDetails))
        Me.New(orno, ordate, idnumber, amount, status, listordetails, "", "")
    End Sub

    Public Sub New(ByVal orno As Long, ByVal ordate As Date, ByVal idnumber As String, ByVal amount As Decimal, _
                   ByVal status As Integer, ByVal listordetails As List(Of OfficialReceiptDetails), _
                   ByVal batchcode As String, ByVal remarks As String)
        Me.New(orno, ordate, idnumber, amount, status, listordetails, "", "", EnumORTransactionType.Collection)
    End Sub

    Public Sub New(ByVal orno As Long, ByVal ordate As Date, ByVal idnumber As String, ByVal amount As Decimal, _
                  ByVal status As Integer, ByVal listordetails As List(Of OfficialReceiptDetails), _
                  ByVal batchcode As String, ByVal remarks As String, ByVal transtype As EnumORTransactionType)
        Me._ORNo = orno
        Me._ORDate = ordate
        Me._IDNumber = idnumber
        Me._Amount = amount
        Me._Status = status
        Me._ListORDetails = listordetails
        Me._BatchCode = batchcode
        Me._Remarks = remarks
        Me._TransactionType = transtype
    End Sub

    Public Sub New(ByVal orno As Long, ByVal ordate As Date, ByVal idnumber As String, ByVal amount As Decimal, _
                   ByVal status As Integer, ByVal listordetails As List(Of OfficialReceiptDetails), _
                   ByVal batchCode As String, ByVal remarks As String, ByVal wesmbill As WESMBillSummary, ByVal transtype As EnumORTransactionType)

        Me._ORNo = orno
        Me._ORDate = ordate
        Me._IDNumber = idnumber
        Me._Amount = amount
        Me._Status = status
        Me._ListORDetails = listordetails
        Me._BatchCode = batchCode
        Me._Remarks = remarks
        Me._WESMSummary = wesmbill
        Me._TransactionType = transtype
    End Sub

    Public Sub New(ByVal orno As Long, ByVal ordate As Date, ByVal idnumber As String, ByVal amount As Decimal, _
                   ByVal status As Integer, ByVal listordetails As List(Of OfficialReceiptDetails), ByVal listorsummary As List(Of OfficialReceiptSummary), _
                   ByVal batchCode As String, ByVal remarks As String, ByVal transtype As EnumORTransactionType, _
                   ByVal vatexempt As Decimal, ByVal vatable As Decimal, ByVal vat As Decimal, ByVal vatzerorated As Decimal, ByVal others As Decimal, _
                   ByVal withholdingtax As Decimal, ByVal withholdingvat As Decimal)

        Me._ORNo = orno
        Me._ORDate = ordate
        Me._IDNumber = idnumber
        Me._Amount = amount
        Me._Status = status
        Me._ListORDetails = listordetails
        Me._ListORSummary = listorsummary
        Me._BatchCode = batchCode
        Me._Remarks = remarks
        Me._TransactionType = transtype
        Me._VATExempt = vatexempt
        Me._Vatable = vatable
        Me._VAT = vat
        Me._VATZeroRated = vatzerorated
        Me._Others = others
        Me._WithholdingTax = withholdingtax
        Me._WithholdingVAT = withholdingvat
    End Sub

#End Region


#Region "ORNo"
    Private _ORNo As Long
    Public Property ORNo() As Long
        Get
            Return _ORNo
        End Get
        Set(ByVal value As Long)
            _ORNo = value
        End Set
    End Property

#End Region

#Region "ORDate"
    Private _ORDate As Date
    Public Property ORDate() As Date
        Get
            Return _ORDate
        End Get
        Set(ByVal value As Date)
            _ORDate = value
        End Set
    End Property

#End Region

#Region "IDNumber"
    Private _IDNumber As String
    Public Property IDNumber() As String
        Get
            Return _IDNumber
        End Get
        Set(ByVal value As String)
            _IDNumber = value
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
    Private _Status As Integer
    Public Property Status() As Integer
        Get
            Return _Status
        End Get
        Set(ByVal value As Integer)
            _Status = value
        End Set
    End Property

#End Region

#Region "ListORDetails"
    Private _ListORDetails As List(Of OfficialReceiptDetails)
    Public Property ListORDetails() As List(Of OfficialReceiptDetails)
        Get
            Return _ListORDetails
        End Get
        Set(ByVal value As List(Of OfficialReceiptDetails))
            _ListORDetails = value
        End Set
    End Property

#End Region

#Region "ListORSummary"
    Private _ListORSummary As List(Of OfficialReceiptSummary)
    Public Property ListORSummary() As List(Of OfficialReceiptSummary)
        Get
            Return _ListORSummary
        End Get
        Set(ByVal value As List(Of OfficialReceiptSummary))
            _ListORSummary = value
        End Set
    End Property

#End Region

#Region "BatchCode"
    Private _BatchCode As String
    Public Property BatchCode() As String
        Get
            Return _BatchCode
        End Get
        Set(ByVal value As String)
            _BatchCode = value
        End Set
    End Property

#End Region

#Region "Remarks"
    Private _Remarks As String
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property

#End Region

#Region "TransactionType"
    Private _TransactionType As EnumORTransactionType
    Public Property TransactionType() As EnumORTransactionType
        Get
            Return _TransactionType
        End Get
        Set(ByVal value As EnumORTransactionType)
            _TransactionType = value
        End Set
    End Property

#End Region

#Region "WESM Bill Summary - Payment"
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

#Region "VATExempt"
    Private _VATExempt As Decimal
    Public Property VATExempt() As Decimal
        Get
            Return _VATExempt
        End Get
        Set(ByVal value As Decimal)
            _VATExempt = value
        End Set
    End Property

#End Region

#Region "Vatable"
    Private _Vatable As Decimal
    Public Property Vatable() As Decimal
        Get
            Return _Vatable
        End Get
        Set(ByVal value As Decimal)
            _Vatable = value
        End Set
    End Property

#End Region

#Region "VAT"
    Private _VAT As Decimal
    Public Property VAT() As Decimal
        Get
            Return _VAT
        End Get
        Set(ByVal value As Decimal)
            _VAT = value
        End Set
    End Property

#End Region

#Region "VATZeroRated"
    Private _VATZeroRated As Decimal
    Public Property VATZeroRated() As Decimal
        Get
            Return _VATZeroRated
        End Get
        Set(ByVal value As Decimal)
            _VATZeroRated = value
        End Set
    End Property

#End Region

#Region "Others"
    Private _Others As Decimal
    Public Property Others() As Decimal
        Get
            Return _Others
        End Get
        Set(ByVal value As Decimal)
            _Others = value
        End Set
    End Property

#End Region

#Region "WithholdingTax"
    Private _WithholdingTax As Decimal
    Public Property WithholdingTax() As Decimal
        Get
            Return _WithholdingTax
        End Get
        Set(ByVal value As Decimal)
            _WithholdingTax = value
        End Set
    End Property

#End Region

#Region "WithholdingVAT"
    Private _WithholdingVAT As Decimal
    Public Property WithholdingVAT() As Decimal
        Get
            Return _WithholdingVAT
        End Get
        Set(ByVal value As Decimal)
            _WithholdingVAT = value
        End Set
    End Property

#End Region

#Region "TotalPayment"
    Private _TotalPayment As Decimal
    Public ReadOnly Property TotalPayment() As Decimal
        Get
            _TotalPayment = Me.VATExempt + Me.Vatable + Me.VAT + Me.VATZeroRated + Me.Others + Me.WithholdingTax + Me.WithholdingVAT
            Return _TotalPayment
        End Get
    End Property

#End Region

End Class
