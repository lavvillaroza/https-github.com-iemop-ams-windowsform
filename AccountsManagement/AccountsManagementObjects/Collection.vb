'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             CalendarBillingPeriod
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     December 27, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for Collection
'Arguments/Parameters:  
'Files/Database Tables:  AM_COLLECTION
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   December 27, 2011       Vladimir E. Espiritu            Class initialization
'	January 04, 2012        Vladimir E. Espiritu            Added InterestRate and AllocationType properties
'   January 08, 2012        Vladimir E. Espiritu            Removed AccountId and Interest Rate
'   February 08, 2012       Vladimir E. Espiritu            Removed AllocatedAmount
'                                                           Added CollectedPrudential and CollectedHeld properties
'   March 06, 2012          Vladimir E. Espiritu            Added PaidTo property
'   March 21, 2012          Vladimir E. Espiritu            Added PrudentialReplenishment and AmountForAllocation properties
'   March 23, 2012          Vladimir E. Espiritu            Added CollectionCategory property
'   March 28, 2012          Vladimir E. Espiritu            Added IsPosted property
'   March 28, 2012          Vladimir E. Espiritu            Added ListOfCollectionAllocation property
'   April 23, 2012          Vladimir E. Espiritu            Added BatchCode property
'   May 22, 2012            Vladimir E. Espiritu            Added AllocationDate property
'   October 09, 2012        Vladimir E. Espiritu            Added DailyBatchCode properties
'

<Serializable()> _
Public Class Collection

#Region "Initialization/Constructor"
    Public Sub New(ByVal orno As Long, ByVal collectionnumber As Long, ByVal collectiondate As Date, ByVal idnumber As String, _
                   ByVal collectedamount As Decimal, ByVal allocationtype As EnumAllocationType, ByVal collectedprudential As Decimal, _
                   ByVal category As EnumCollectionCategory, ByVal status As EnumCollectionStatus, ByVal collectedheld As Decimal, _
                   ByVal dmcmno As Long)
        Me._CollectionNumber = collectionnumber
        Me._CollectionDate = collectiondate
        Me._IDNumber = idnumber
        Me._CollectedAmount = collectedamount
        Me._CollectedPrudential = collectedprudential
        Me._AllocationType = allocationtype
        Me._Status = status
        Me._ORNo = orno
        Me._CollectionCategory = category
        Me._IsPosted = Nothing
        Me._ListOfCollectionAllocation = New List(Of CollectionAllocation)
        Me._BatchCode = ""
        Me._AllocationDate = New Date()
        Me._DMCMNumber = dmcmno
        Me._DailyBatchCode = ""
    End Sub

    Public Sub New(ByVal orno As Long, ByVal collectionnumber As Long, ByVal collectiondate As Date, ByVal idnumber As String, _
                   ByVal collectedamount As Decimal, ByVal allocationtype As EnumAllocationType, ByVal collectedprudential As Decimal, _
                   ByVal category As EnumCollectionCategory, ByVal status As EnumCollectionStatus, ByVal collectedheld As Decimal, _
                   ByVal dmcmno As Long, ByVal dailybatchcode As String)
        Me._CollectionNumber = collectionnumber
        Me._CollectionDate = collectiondate
        Me._IDNumber = idnumber
        Me._CollectedAmount = collectedamount
        Me._CollectedPrudential = collectedprudential
        Me._AllocationType = allocationtype
        Me._Status = status
        Me._ORNo = orno
        Me._CollectionCategory = category
        Me._IsPosted = Nothing
        Me._ListOfCollectionAllocation = New List(Of CollectionAllocation)
        Me._BatchCode = ""
        Me._AllocationDate = New Date()
        Me._DMCMNumber = dmcmno
        Me._DailyBatchCode = dailybatchcode
    End Sub

    Public Sub New()
        Me.New(0, 0, Nothing, "", 0, Nothing, 0, Nothing, Nothing, 0, 0)
    End Sub
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

#Region "CollectionDate"
    Private _CollectionDate As Date
    Public Property CollectionDate() As Date
        Get
            Return _CollectionDate
        End Get
        Set(ByVal value As Date)
            _CollectionDate = value
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

#Region "CollectedAmount"
    Private _CollectedAmount As Decimal
    Public Property CollectedAmount() As Decimal
        Get
            Return _CollectedAmount
        End Get
        Set(ByVal value As Decimal)
            _CollectedAmount = value
        End Set
    End Property

#End Region

#Region "AmountForAllocation"
    Private _AmountForAllocation As Decimal
    Public Property AmountForAllocation() As Decimal
        Get
            Return _AmountForAllocation
        End Get
        Set(ByVal value As Decimal)
            _AmountForAllocation = value
        End Set
    End Property

#End Region

#Region "CollectedPrudential"
    Private _CollectedPrudential As Decimal
    Public Property CollectedPrudential() As Decimal
        Get
            Return _CollectedPrudential
        End Get
        Set(ByVal value As Decimal)
            _CollectedPrudential = value
        End Set
    End Property

#End Region

#Region "AllocationType"
    Private _AllocationType As EnumAllocationType
    Public Property AllocationType() As EnumAllocationType
        Get
            Return _AllocationType
        End Get
        Set(ByVal value As EnumAllocationType)
            _AllocationType = value
        End Set
    End Property

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

#Region "Status"
    Private _Status As EnumCollectionStatus
    Public Property Status() As EnumCollectionStatus
        Get
            Return _Status
        End Get
        Set(ByVal value As EnumCollectionStatus)
            _Status = value
        End Set
    End Property

#End Region

#Region "CollectionCategory"
    Private _CollectionCategory As EnumCollectionCategory
    Public Property CollectionCategory() As EnumCollectionCategory
        Get
            Return _CollectionCategory
        End Get
        Set(ByVal value As EnumCollectionCategory)
            _CollectionCategory = value
        End Set
    End Property

#End Region

#Region "CollectedHeld"
    Private _CollectedHeld As Decimal
    Public Property CollectedHeld() As Decimal
        Get
            Return _CollectedHeld
        End Get
        Set(ByVal value As Decimal)
            _CollectedHeld = value
        End Set
    End Property

#End Region

#Region "IsPosted"
    Private _IsPosted As EnumIsPosted
    Public Property IsPosted() As EnumIsPosted
        Get
            Return _IsPosted
        End Get
        Set(ByVal value As EnumIsPosted)
            _IsPosted = value
        End Set
    End Property

#End Region

#Region "DailyBatchCode"
    Private _DailyBatchCode As String
    Public Property DailyBatchCode() As String
        Get
            Return _DailyBatchCode
        End Get
        Set(ByVal value As String)
            _DailyBatchCode = value
        End Set
    End Property

#End Region

#Region "ListOfCollectionAllocation"
    Private _ListOfCollectionAllocation As List(Of CollectionAllocation)
    Public Property ListOfCollectionAllocation() As List(Of CollectionAllocation)
        Get
            Return _ListOfCollectionAllocation
        End Get
        Set(ByVal value As List(Of CollectionAllocation))
            _ListOfCollectionAllocation = value
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

End Class
