'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             CalendarBillingPeriod
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     April 23, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Collection
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   April 23, 2012          Vladimir E. Espiritu            Class initialization
'   August 05, 2013         Vladimir E. Espiritu            Added CollectionNoTag property
'

Option Explicit On
Option Strict On

<Serializable()> _
Public Class CollectionMonitoring


#Region "Initialization/Constructor"
    Public Sub New()
        Me.New(0, "", 0, Nothing, Nothing, New AMParticipants(), 0, 0, Nothing, EnumStatus.Active)
    End Sub

    Public Sub New(ByVal colmonno As Long, ByVal batchcode As String, ByVal colno As Long, ByVal allocationdate As Date, _
                    ByVal allocationtype As EnumAllocationType, ByVal idnumber As AMParticipants, _
                    ByVal orno As Long, ByVal amount As Decimal, ByVal transtype As EnumCollectionMonitoringType, _
                    ByVal status As EnumStatus)

        Me._CollectionMonitoringNo = colmonno
        Me._BatchCode = batchcode
        Me._CollectionNo = colno
        Me._AllocationDate = allocationdate
        Me._AllocationType = allocationtype
        Me._IDNumber = idnumber
        Me._ORNo = orno
        Me._Amount = amount
        Me._TransType = transtype
        Me._Status = status
    End Sub

#End Region

#Region "CollectionMonitoringNo"
    Private _CollectionMonitoringNo As Long
    Public Property CollectionMonitoringNo() As Long
        Get
            Return _CollectionMonitoringNo
        End Get
        Set(ByVal value As Long)
            _CollectionMonitoringNo = value
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

#Region "CollectionNo"
    Private _CollectionNo As Long
    Public Property CollectionNo() As Long
        Get
            Return _CollectionNo
        End Get
        Set(ByVal value As Long)
            _CollectionNo = value
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

#Region "IDNumber"
    Private _IDNumber As AMParticipants
    Public Property IDNumber() As AMParticipants
        Get
            Return _IDNumber
        End Get
        Set(ByVal value As AMParticipants)
            _IDNumber = value
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

#Region "TransType"
    Private _TransType As EnumCollectionMonitoringType
    Public Property TransType() As EnumCollectionMonitoringType
        Get
            Return _TransType
        End Get
        Set(ByVal value As EnumCollectionMonitoringType)
            _TransType = value
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

#Region "CollectionNoTag"
    Private _CollectionNoTag As Long
    Public Property CollectionNoTag() As Long
        Get
            Return _CollectionNoTag
        End Get
        Set(ByVal value As Long)
            _CollectionNoTag = value
        End Set
    End Property

#End Region

End Class
