'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             ParticipantParentChildMapping
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     November 26, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for Mapping of Parent Child
'Arguments/Parameters:  
'Files/Database Tables:  AM_PARENT_CHILD_MAPPING
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   November 26, 2011      Juan Carlo L. Panopio           Class initialization
'   November 28, 2011      Juan Carlo L. Panopio           Changed Class Structure based on new Table Design
'   February 22, 2012      Juan carlo L. Panopio           Added Billing Period

Public Class ParticipantParentChildMapping

#Region "Initialization/Constructor"
    Public Sub New()
        Me.New("", "", 0, 0, Nothing, Nothing, Nothing, Nothing, 0)
    End Sub

    Public Sub New(ByVal PCNumber As String, ByVal IDNumber As String, ByVal Parent_Flag As Integer, ByVal vStatus As Integer, ByVal DateCreated As Date, _
                   ByVal Remarks As String, ByVal UpdatedBy As String, ByVal DateUpdated As Date, ByVal BillPeriod As Integer)
        Me._IDNumber = IDNumber
        Me._PCNumber = PCNumber
        Me._ParentFlag = Parent_Flag
        Me._Status = vStatus
        Me._DateCreated = DateCreated
        Me._UpdatedDate = DateUpdated
        Me._UpdatedBy = UpdatedBy
        Me._Remarks = Remarks
        Me._BillPeriod = BillPeriod
    End Sub

#End Region

#Region "Parent ID Number"
    Private _PCNumber As String
    Public Property PCNumber() As String
        Get
            Return _PCNumber
        End Get
        Set(ByVal value As String)
            _PCNumber = value
        End Set
    End Property
#End Region

#Region "Child Id Number"
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

#Region "Parent Flag"
    Private _ParentFlag As Integer
    Public Property ParentFlag() As Integer
        Get
            Return _ParentFlag
        End Get
        Set(ByVal value As Integer)
            _ParentFlag = value
        End Set
    End Property
#End Region

#Region "BillingPeriod"
    Private _BillPeriod As Integer
    Public Property BillPeriod() As Integer
        Get
            Return _BillPeriod
        End Get
        Set(ByVal value As Integer)
            _BillPeriod = value
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

#Region "Start Date"
    Private _DateCreated As Date
    Public Property DateCreated() As Date
        Get
            Return _DateCreated
        End Get
        Set(ByVal value As Date)
            _DateCreated = value
        End Set
    End Property
#End Region

#Region "Updated Date"
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

#Region "Update By"
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

End Class
