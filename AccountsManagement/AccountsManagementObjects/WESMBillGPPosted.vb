'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             WESMBillGPPosted
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 14, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for posting GP posted
'Arguments/Parameters:  
'Files/Database Tables:  AM_WESM_BILL_GP_POSTED
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   September 14, 2011      Juan Carlo L. Panopio           Class initialization
'   February 13, 2012       Vladimir E. Espiritu            Added Posted type
'   June 06, 2012           Vladimir E. Espiritu            Added MarketTransactionFees and OutputTax
'   July 09, 2012           Vladimir E. Espiritu            Added DocumentAmount
'

Public Class WESMBillGPPosted

#Region "Initialization/Constructor"
    ' <summary>
    ' Initialization of InvoiceSummary Class.
    ' </summary>
    ' <remarks></remarks>
    Public Sub New()
        Me._BillingPeriod = Nothing
        Me._SettlementRun = ""
        Me._Charge = New EnumChargeType()
        Me._DueDate = Nothing
        Me._Remarks = ""
        Me._BatchCode = "0"
        Me._GPRefNo = ""
        Me._PostType = ""
        Me._DocumentAmount = 0
        Me._JVNumber = 0
        Me._TransactionDate = DateTime.Now
        Me._TransactionType = Nothing
    End Sub
#End Region

#Region "BillingPeriod"
    Private _BillingPeriod As Integer
    ' <summary>
    ' Gets or sets the billing date.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property BillingPeriod() As Integer
        Get
            Return _BillingPeriod
        End Get
        Set(ByVal value As Integer)
            _BillingPeriod = value
        End Set
    End Property

#End Region

#Region "SettlementRun"
    Private _SettlementRun As String
    ' <summary>
    ' Gets or sets Settlement run.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property SettlementRun() As String
        Get
            Return _SettlementRun
        End Get
        Set(ByVal value As String)
            _SettlementRun = value
        End Set
    End Property

#End Region

#Region "Charge"
    Private _Charge As EnumChargeType
    ' <summary>
    ' Gets or sets Invoice Charge.
    ' </summary>
    ' <remarks></remarks>
    Public Property Charge() As EnumChargeType
        Get
            Return _Charge
        End Get
        Set(ByVal value As EnumChargeType)
            _Charge = value
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

#Region "BatchCode"
    Private _BatchCode As String
    ' <summary>
    ' Gets or sets Settlement run.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property BatchCode() As String
        Get
            Return _BatchCode
        End Get
        Set(ByVal value As String)
            _BatchCode = value
        End Set
    End Property

#End Region

#Region "Is Posted"
    Private _Posted As Integer
    ' <summary>
    ' Gets or sets Amount. Default value is 0.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property Posted() As Integer
        Get
            Return _Posted
        End Get
        Set(ByVal value As Integer)
            _Posted = value
        End Set
    End Property
#End Region

#Region "Post Type"
    Private _PostType As String
    ' <summary>
    ' Gets or sets Amount. Default value is 0.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property PostType() As String
        Get
            Return _PostType
        End Get
        Set(ByVal value As String)
            _PostType = value
        End Set
    End Property
#End Region

#Region "GP Reference Number"
    Private _GPRefNo As String
    ' <summary>
    ' Gets or sets GP Reference Number.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property GPRefNo() As String
        Get
            Return _GPRefNo
        End Get
        Set(ByVal value As String)
            _GPRefNo = value
        End Set
    End Property

#End Region

#Region "DocumentAmount"
    Private _DocumentAmount As Decimal
    Public Property DocumentAmount() As Decimal
        Get
            Return _DocumentAmount
        End Get
        Set(ByVal value As Decimal)
            _DocumentAmount = value
        End Set
    End Property

#End Region

#Region "JV Number"

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

#Region "Transaction/Updated Date"

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

#Region "Transaction Type"

    Private _TransactionType As EnumPostedType
    Public Property TransactionType() As EnumPostedType
        Get
            Return _TransactionType
        End Get
        Set(ByVal value As EnumPostedType)
            _TransactionType = value
        End Set
    End Property

#End Region

End Class
