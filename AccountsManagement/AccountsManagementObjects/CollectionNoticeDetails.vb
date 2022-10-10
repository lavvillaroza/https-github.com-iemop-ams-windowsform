'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             CollectionNoticeDetails
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 29, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for Collection Notice Details
'Arguments/Parameters:  
'Files/Database Tables:  AM_COLLECTION_NOTICE_DETAILS
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   September 29, 2011      Juan Carlo L. Panopio           Class initialization
'	



Option Strict On
Option Explicit On

Public Class CollectionNoticeDetails

    Public Sub New()
        Me.New(0, "", 0, "", Nothing, Nothing, 0, 0, 0, 0, 0, "", "", "", Nothing, "", "", Nothing, Nothing, "", 0)
    End Sub


    Public Sub New(ByVal ReferenceNo As Long, ByVal TransactionType As String, ByVal BillingPeriod As Integer, ByVal InvDMCMNo As String, _
                   ByVal InvoiceDate As Date, ByVal DueDate As Date, ByVal Energy As Decimal, ByVal VAT_ENERGY As Decimal, ByVal MF As Decimal, ByVal VAT_MF As Decimal, _
                   ByVal Total As Decimal, ByVal ApprovedBy As String, ByVal PreparedBy As String, ByVal UpdatedBy As String, ByVal UpdatedDate As Date, ByVal Participant As String, _
                   ByVal ParentFlag As String, ByVal BillCalendar As Date, ByVal SummaryType As EnumSummaryType, ByVal Remarks As String, ByVal DefaultInterest As Decimal)

        Me._ReferenceNo = ReferenceNo
        Me._BillingPeriod = BillingPeriod
        Me._InvoiceDate = InvoiceDate
        Me._INVDMCM = InvDMCMNo
        Me._DueDate = DueDate
        Me._Energy = Energy
        Me._VATEnergy = VAT_ENERGY
        Me._MF = MF
        Me._VATMF = VAT_MF
        Me._Total = Total
        Me._TransactionType = TransactionType
        Me._UpdatedBy = UpdatedBy
        Me._UpdatedDate = UpdatedDate
        Me._ParticipantID = Participant
        Me._ParentId = ParentFlag
        Me._BPCalendar = BillCalendar
        Me._SummaryType = SummaryType
        Me._Remarks = Remarks
        Me._DefaultInterest = DefaultInterest

    End Sub

#Region "Reference number"
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

#Region "ParticipantID"
    Private _ParticipantID As String
    Public Property ParticipantID() As String
        Get
            Return _ParticipantID
        End Get
        Set(ByVal value As String)
            _ParticipantID = value
        End Set
    End Property
#End Region

#Region "IsParent"
    Private _ParentId As String
    Public Property ParentId() As String
        Get
            Return _ParentId
        End Get
        Set(ByVal value As String)
            _ParentId = value
        End Set
    End Property
#End Region

#Region "Transaction Type(P - previous/C - current)"
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

#Region "Summary Type"
    Private _SummaryType As EnumSummaryType
    Public Property SummaryType() As EnumSummaryType
        Get
            Return _SummaryType
        End Get
        Set(ByVal value As EnumSummaryType)
            _SummaryType = value
        End Set
    End Property
#End Region

#Region "Billing Period"
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

#Region "INV/DMCM NUMBER"
    Private _INVDMCM As String
    Public Property INVDMCM() As String
        Get
            Return _INVDMCM
        End Get
        Set(ByVal value As String)
            _INVDMCM = value
        End Set
    End Property
#End Region

#Region "Invoice Date"
    Private _InvoiceDate As Date
    Public Property InvoiceDate() As Date
        Get
            Return _InvoiceDate
        End Get
        Set(ByVal value As Date)
            _InvoiceDate = value
        End Set
    End Property
#End Region

#Region "Due Date"
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

#Region "Energy"
    Private _Energy As Decimal
    Public Property Energy() As Decimal
        Get
            Return _Energy
        End Get
        Set(ByVal value As Decimal)
            _Energy = value
        End Set
    End Property
#End Region

#Region "Billing Period Calendar"
    Private _BPCalendar As Date
    Public Property BPCalendar() As Date
        Get
            Return _BPCalendar
        End Get
        Set(ByVal value As Date)
            _BPCalendar = value
        End Set
    End Property
#End Region

#Region "VAT_ENERGY"
    Private _VATEnergy As Decimal
    Public Property VATEnergy() As Decimal
        Get
            Return _VATEnergy
        End Get
        Set(ByVal value As Decimal)
            _VATEnergy = value
        End Set
    End Property
#End Region

#Region "TOTAL"
    Private _Total As Decimal
    Public Property Total() As Decimal
        Get
            Return _Total
        End Get
        Set(ByVal value As Decimal)
            _Total = value
        End Set
    End Property
#End Region

#Region "MF"
    Private _MF As Decimal
    Public Property MF() As Decimal
        Get
            Return _MF
        End Get
        Set(ByVal value As Decimal)
            _MF = value
        End Set
    End Property
#End Region

#Region "VAT_MF"
    Private _VATMF As Decimal
    Public Property VATMF() As Decimal
        Get
            Return _VATMf
        End Get
        Set(ByVal value As Decimal)
            _VATMF = value
        End Set
    End Property
#End Region


    Private _DefaultInterest As Decimal
    Public Property DefaultInterest() As Decimal
        Get
            Return _DefaultInterest
        End Get
        Set(ByVal value As Decimal)
            _DefaultInterest = value
        End Set
    End Property


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
