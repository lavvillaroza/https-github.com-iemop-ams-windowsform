'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             DebitCreditMemo
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 29, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for Debit Credit Memo
'Arguments/Parameters:  
'Files/Database Tables:  AM_DMCM
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   September 29, 2011      Juan Carlo L. Panopio           Class initialization
'	September 30, 2011      Vladimir E. Espiritu            Revised properties
'   October 04, 2011        Vladimir E. Espiritu            Added IDNumber, BillingPeriod and DueDate properties
'   December 14, 2011       Vladimir E. Espiritu            Added JVNumber property
'   February 29, 2012       Vladimir E. Espiritu            Added Particulars Column
'   July 26, 2012           Vladimir E. Espiritu            Added Vatable, Vat, VatExempt, VatZeroRated 
'                                                           TotalAmountDue, EWT and EWV properties
'   January 13, 2014        Vladimir E. Espiritu            Added Others properties
'


Option Explicit On
Option Strict On

Public Class DebitCreditMemo

#Region "Initialization/Constructor"
    Public Sub New()
        Me._DMCMNumber = 0
        Me._JVNumber = 0
        Me._IDNumber = ""
        Me._Particulars = ""
        Me._ChargeType = Nothing
        Me._DMCMNumber = Nothing
        Me._DMCMDetails = New List(Of DebitCreditMemoDetails)
        Me._PreparedBy = ""
        Me._CheckedBy = ""
        Me._ApprovedBy = ""
        Me._UpdatedBy = ""
        Me._UpdatedDate = Nothing
        Me._TransType = Nothing
        Me._Vatable = 0
        Me._VAT = 0
        Me._VATExempt = 0
        Me._VatZeroRated = 0
        Me._TotalAmountDue = 0
        Me._EWT = 0
        Me._EWV = 0
        Me._Others = 0
    End Sub
#End Region


#Region "DMCM Number"
    Private _DMCMNumber As Long
    Public Property DMCMNumber() As Long
        Get
            Return _DMCMNumber
        End Get
        Set(ByVal value As Long)
            _DMCMNumber = value
        End Set
    End Property
#End Region

#Region "JVNumber"
    Private _JVNumber As Long
    Public Property JVNumber() As Long
        Get
            Return _JVNumber
        End Get
        Set(ByVal value As Long)
            _JVNumber = value
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

#Region "BillingPeriod"
    Private _BillingPeriod As Integer
    Public Property BillingPeriod() As Integer
        Get
            Return _BillingPeriod
        End Get
        Set(ByVal value As Integer)
            _BillingPeriod = value
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

#Region "Charge Type"
    Private _ChargeType As EnumChargeType
    Public Property ChargeType() As EnumChargeType
        Get
            Return _ChargeType
        End Get
        Set(ByVal value As EnumChargeType)
            _ChargeType = value
        End Set
    End Property
#End Region

#Region "DMCM Details"
    Private _DMCMDetails As List(Of DebitCreditMemoDetails)
    Public Property DMCMDetails() As List(Of DebitCreditMemoDetails)
        Get
            Return _DMCMDetails
        End Get
        Set(ByVal value As List(Of DebitCreditMemoDetails))
            _DMCMDetails = value
        End Set
    End Property

#End Region

#Region "TransType"
    Private _TransType As EnumDMCMTransactionType
    Public Property TransType() As EnumDMCMTransactionType
        Get
            Return _TransType
        End Get
        Set(ByVal value As EnumDMCMTransactionType)
            _TransType = value
        End Set
    End Property

#End Region

#Region "EWT"
    Private _EWT As Decimal
    Public Property EWT() As Decimal
        Get
            Return _EWT
        End Get
        Set(ByVal value As Decimal)
            _EWT = value
        End Set
    End Property

#End Region

#Region "EWV"
    Private _EWV As Decimal
    Public Property EWV() As Decimal
        Get
            Return _EWV
        End Get
        Set(ByVal value As Decimal)
            _EWV = value
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

#Region "VatZeroRated"
    Private _VatZeroRated As Decimal
    Public Property VatZeroRated() As Decimal
        Get
            Return _VatZeroRated
        End Get
        Set(ByVal value As Decimal)
            _VatZeroRated = value
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

#Region "TotalAmountDue"
    Private _TotalAmountDue As Decimal
    Public Property TotalAmountDue() As Decimal
        Get
            Return _TotalAmountDue
        End Get
        Set(ByVal value As Decimal)
            _TotalAmountDue = value
        End Set
    End Property

#End Region

#Region "PreparedBy"
    Private _PreparedBy As String
    Public Property PreparedBy() As String
        Get
            Return _PreparedBy
        End Get
        Set(ByVal value As String)
            _PreparedBy = value
        End Set
    End Property
#End Region

#Region "Checked by"
    Private _CheckedBy As String
    Public Property CheckedBy() As String
        Get
            Return _CheckedBy
        End Get
        Set(ByVal value As String)
            _CheckedBy = value
        End Set
    End Property
#End Region

#Region "APPROVED BY"
    Private _ApprovedBy As String
    Public Property ApprovedBy() As String
        Get
            Return _ApprovedBy
        End Get
        Set(ByVal value As String)
            _ApprovedBy = value
        End Set
    End Property
#End Region

#Region "UPDATED BY"
    Private _UpdatedBy As String
    Public Property UpdatedBy() As String
        Get
            Return _UpdatedBy
        End Get
        Set(ByVal value As String)
            _UpdatedBy = value
        End Set
    End Property
#End Region

#Region "UPDATED DATE"
    Private _UpdatedDate As Date

    Public Property UpdatedDate() As Date
        Get
            Return _UpdatedDate
        End Get
        Set(ByVal value As Date)
            _UpdatedDate = value
        End Set
    End Property

#End Region

End Class
