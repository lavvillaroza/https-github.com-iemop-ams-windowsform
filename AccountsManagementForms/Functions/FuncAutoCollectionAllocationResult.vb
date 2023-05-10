'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             FuncAutoCollectionAllocationResult
'Orginal Author:         Vladimir E.Espiritu
'File Creation Date:     March 23, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for re-usable functions
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description
'   March 23, 2012          Vladimir E. Espiritu        Class initialization
'   April 23, 2012          Vladimir E. Espiritu        Added ListCollectionMonitoring property
'   May 07, 2012            Vladimir E. Espiritu        Removed itemJournal and itemGPPosted properties
'   May 07, 2012            Vladimir E. Espiritu        Added ListDMCM property
'

Option Explicit On
Option Strict On
Imports AccountsManagementObjects

Public Class FuncAutoCollectionAllocationResult

#Region "Initialization/Constructor"
    Public Sub New()
        Me._ListCollections = New List(Of Collection)
        Me._ListWESMBillSummaries = New List(Of WESMBillSummary)
        Me._ListCollectionAllocation = New List(Of CollectionAllocation)
        Me._ListPrudentials = New List(Of Prudential)
        Me._ListPrudentialsHistory = New List(Of PrudentialHistory)
        Me._ListParticipantsHeld = New List(Of AMParticipants)
        Me._ListParticipantsExcess = New List(Of AMParticipants)
        Me._ListCollectionMonitoring = New List(Of CollectionMonitoring)
        Me._ListDMCM = New List(Of DebitCreditMemo)
    End Sub
#End Region


#Region "ListCollections"
    Private _ListCollections As List(Of Collection)
    Public Property ListCollections() As List(Of Collection)
        Get
            Return _ListCollections
        End Get
        Set(ByVal value As List(Of Collection))
            _ListCollections = value
        End Set
    End Property

#End Region

#Region "ListWESMBillSummaries"
    Private _ListWESMBillSummaries As List(Of WESMBillSummary)
    Public Property ListWESMBillSummaries() As List(Of WESMBillSummary)
        Get
            Return _ListWESMBillSummaries
        End Get
        Set(ByVal value As List(Of WESMBillSummary))
            _ListWESMBillSummaries = value
        End Set
    End Property

#End Region

#Region "ListCollectionAllocation"
    Private _ListCollectionAllocation As List(Of CollectionAllocation)
    Public Property ListCollectionAllocation() As List(Of CollectionAllocation)
        Get
            Return _ListCollectionAllocation
        End Get
        Set(ByVal value As List(Of CollectionAllocation))
            _ListCollectionAllocation = value
        End Set
    End Property

#End Region

#Region "ListPrudentials"
    Private _ListPrudentials As List(Of Prudential)
    Public Property ListPrudentials() As List(Of Prudential)
        Get
            Return _ListPrudentials
        End Get
        Set(ByVal value As List(Of Prudential))
            _ListPrudentials = value
        End Set
    End Property

#End Region

#Region "ListPrudentialsHistory"
    Private _ListPrudentialsHistory As List(Of PrudentialHistory)
    Public Property ListPrudentialsHistory() As List(Of PrudentialHistory)
        Get
            Return _ListPrudentialsHistory
        End Get
        Set(ByVal value As List(Of PrudentialHistory))
            _ListPrudentialsHistory = value
        End Set
    End Property

#End Region

#Region "ListParticipantsHeld"
    Private _ListParticipantsHeld As List(Of AMParticipants)
    Public Property ListParticipantsHeld() As List(Of AMParticipants)
        Get
            Return _ListParticipantsHeld
        End Get
        Set(ByVal value As List(Of AMParticipants))
            _ListParticipantsHeld = value
        End Set
    End Property


#End Region

#Region "ListParticipantsExcess"
    Private _ListParticipantsExcess As List(Of AMParticipants)
    Public Property ListParticipantsExcess() As List(Of AMParticipants)
        Get
            Return _ListParticipantsExcess
        End Get
        Set(ByVal value As List(Of AMParticipants))
            _ListParticipantsExcess = value
        End Set
    End Property

#End Region

#Region "ListCollectionMonitoring"
    Private _ListCollectionMonitoring As List(Of CollectionMonitoring)
    Public Property ListCollectionMonitoring() As List(Of CollectionMonitoring)
        Get
            Return _ListCollectionMonitoring
        End Get
        Set(ByVal value As List(Of CollectionMonitoring))
            _ListCollectionMonitoring = value
        End Set
    End Property

#End Region

#Region "ListDMCM"
    Private _ListDMCM As List(Of DebitCreditMemo)
    Public Property ListDMCM() As List(Of DebitCreditMemo)
        Get
            Return _ListDMCM
        End Get
        Set(ByVal value As List(Of DebitCreditMemo))
            _ListDMCM = value
        End Set
    End Property

#End Region


End Class
