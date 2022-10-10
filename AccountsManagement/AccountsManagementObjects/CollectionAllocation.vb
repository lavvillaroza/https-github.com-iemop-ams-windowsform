'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             CalendarBillingPeriod
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     January 02, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Collection Allocation
'Arguments/Parameters:  
'Files/Database Tables:  AM_COLLECTION_ALLOCATION
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   January 02, 2012        Vladimir E. Espiritu            Class initialization
'   January 04, 2012        Vladimir E. Espiritu            Added BillingPeriod property
'   January 17, 2012        Vladimir E. Espiritu            Removed Balance property
'   February 02, 2012       Vladimir E. Espiritu            Added EndingBalance, NewEndingBalance and NewDueDate properties
'   February 03, 2012       Juan Carlo L. Panopio           Added Allocation Date
'   February 09, 2012       Vladimir E. Espiritu            Added DueDate
'   March 05, 2012          Vladimir E. Espiritu            Added WESMBillSummaryNo
'   March 07, 2012          Vladimir E. Espiritu            Removed IDType and GroupNo properties
'   March 24, 2012          Vladimir E. Espiritu            Change WESMBillSummaryNo datatype
'   August 26, 2012         Vladimir E. Espiritu            Added DMCM Number property
'   September 05, 2012      Vladimir E. Espiritu            Added IsDMCMChanged property
'   September 06, 2012      Vladimir E. Espiritu            Added ReferenceNumber and ReferenceType properties
'   October 17, 2012        Vladimir E. Espiritu            Added CollectionTypeCode property
'

Option Explicit On
Option Strict On
<Serializable()> _
Public Class CollectionAllocation

#Region "Initialization/Constructor"
    Public Sub New()
        Me._BatchCode = "0"
        Me._BillingPeriod = Nothing
        Me._CollectionNumber = 0
        Me._Amount = 0D
        Me._EndingBalance = 0D
        Me._DueDate = Nothing
        Me._NewEndingBalance = 0D
        Me._NewDueDate = Nothing
        Me._CollectionType = Nothing
        Me._Status = 1
        Me._AllocationDate = Nothing
        Me._WESMBillSummaryNo = New WESMBillSummary()
        Me._DMCMNumber = 0
        Me._IsDMCMChanged = 0
        Me._ReferenceNumber = ""
        Me._ReferenceType = Nothing
        Me._CollectionTypeCode = Nothing
    End Sub
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

#Region "CollectionNumber"
    Private _CollectionNumber As Long
    Public Property CollectionNumber() As Long
        Get
            Return _CollectionNumber
        End Get
        Set(ByVal value As Long)
            _CollectionNumber = value
        End Set
    End Property

#End Region

#Region "EndingBalance"
    Private _EndingBalance As Decimal
    Public Property EndingBalance() As Decimal
        Get
            Return _EndingBalance
        End Get
        Set(ByVal value As Decimal)
            _EndingBalance = value
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

#Region "NewEndingBalance"
    Private _NewEndingBalance As Decimal
    Public Property NewEndingBalance() As Decimal
        Get
            Return _NewEndingBalance
        End Get
        Set(ByVal value As Decimal)
            _NewEndingBalance = value
        End Set
    End Property

#End Region

#Region "NewDueDate"
    Private _NewDueDate As Date
    Public Property NewDueDate() As Date
        Get
            Return _NewDueDate
        End Get
        Set(ByVal value As Date)
            _NewDueDate = value
        End Set
    End Property

#End Region

#Region "AllocationDate"
    Private _AllocationDate As Date
    Public Property AllocationDate() As Date
        Get
            Return _AllocationDate
        End Get
        Set(ByVal value As Date)
            _AllocationDate = value
        End Set
    End Property

#End Region

#Region "CollectionType"
    Private _CollectionType As EnumCollectionType
    Public Property CollectionType() As EnumCollectionType
        Get
            Return _CollectionType
        End Get
        Set(ByVal value As EnumCollectionType)
            _CollectionType = value
        End Set
    End Property

#End Region

#Region "CollectionTypeCode"
    Private _CollectionTypeCode As EnumCollectionTypeCode
    Public ReadOnly Property CollectionTypeCode() As EnumCollectionTypeCode
        Get
            Select Case Me.CollectionType
                Case EnumCollectionType.WithholdingTaxOnMF
                    Me._CollectionTypeCode = EnumCollectionTypeCode.WTMF

                Case EnumCollectionType.WithholdingVatonMF
                    Me._CollectionTypeCode = EnumCollectionTypeCode.WVMF

                Case EnumCollectionType.WithholdingTaxOnDefaultInterest
                    Me._CollectionTypeCode = EnumCollectionTypeCode.WTDI

                Case EnumCollectionType.WithholdingVatonDefaultInterest
                    Me._CollectionTypeCode = EnumCollectionTypeCode.WVDI

                Case EnumCollectionType.DefaultInterestOnMF
                    Me._CollectionTypeCode = EnumCollectionTypeCode.DIMF

                Case EnumCollectionType.DefaultInterestOnVatOnMF
                    Me._CollectionTypeCode = EnumCollectionTypeCode.DIVMF

                Case EnumCollectionType.MarketFees
                    Me._CollectionTypeCode = EnumCollectionTypeCode.MF

                Case EnumCollectionType.VatOnMarketFees
                    Me._CollectionTypeCode = EnumCollectionTypeCode.MFV

                Case EnumCollectionType.DefaultInterestOnEnergy
                    Me._CollectionTypeCode = EnumCollectionTypeCode.DIE

                Case EnumCollectionType.Energy
                    Me._CollectionTypeCode = EnumCollectionTypeCode.E

                Case EnumCollectionType.VatOnEnergy
                    Me._CollectionTypeCode = EnumCollectionTypeCode.VE

            End Select

            Return _CollectionTypeCode
        End Get
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

#Region "WESMBillSummaryNo"
    Private _WESMBillSummaryNo As WESMBillSummary
    Public Property WESMBillSummaryNo() As WESMBillSummary
        Get
            Return _WESMBillSummaryNo
        End Get
        Set(ByVal value As WESMBillSummary)
            _WESMBillSummaryNo = value
        End Set
    End Property

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

#Region "IsDMCMChanged"
    Private _IsDMCMChanged As Integer
    Public Property IsDMCMChanged() As Integer
        Get
            Return _IsDMCMChanged
        End Get
        Set(ByVal value As Integer)
            _IsDMCMChanged = value
        End Set
    End Property

#End Region

#Region "ReferenceNumber"
    Private _ReferenceNumber As String
    Public Property ReferenceNumber() As String
        Get
            Return _ReferenceNumber
        End Get
        Set(ByVal value As String)
            _ReferenceNumber = value
        End Set
    End Property

#End Region

#Region "Reference Type"
    Private _ReferenceType As EnumSummaryType
    Public Property ReferenceType() As EnumSummaryType
        Get
            Return _ReferenceType
        End Get
        Set(ByVal value As EnumSummaryType)
            _ReferenceType = value
        End Set
    End Property


#End Region

End Class
