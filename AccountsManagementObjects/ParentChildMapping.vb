'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             ParentChildMapping
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     November 25, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for Parent to Child Mapping
'Arguments/Parameters:  
'Files/Database Tables:  AM_PARENT_CHILD_MAPPING
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   November 25, 2011       Vladimir E. Espiritu            Class initialization
'

Public Class ParentChildMapping

#Region "Intitialization/Constructor"
    Public Sub New()
        Me._ParentIDNumber = ""
        Me._ChildIDNumber = ""
        Me._ParentFlag = 0
        Me._Status = 1
        Me._StartDate = Nothing
        Me._EndDate = Nothing
        Me._UpdatedBy = "AM"
        Me._UpdatedDate = Nothing
        Me._Remarks = ""
    End Sub
#End Region

#Region "ParentIDNumber"
    Private _ParentIDNumber As String
    Public Property ParentIDNumber() As String
        Get
            Return _ParentIDNumber
        End Get
        Set(ByVal value As String)
            _ParentIDNumber = value
        End Set
    End Property

#End Region

#Region "ChildIDNumber"
    Private _ChildIDNumber As String
    Public Property ChildIDNumber() As String
        Get
            Return _ChildIDNumber
        End Get
        Set(ByVal value As String)
            _ChildIDNumber = value
        End Set
    End Property
#End Region

#Region "ParentFlag"
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

#Region "StartDate"
    Private _StartDate As Date
    Public Property StartDate() As Date
        Get
            Return _StartDate
        End Get
        Set(ByVal value As Date)
            _StartDate = value
        End Set
    End Property

#End Region

#Region "EndDate"
    Private _EndDate As Date
    Public Property EndDate() As Date
        Get
            Return _EndDate
        End Get
        Set(ByVal value As Date)
            _EndDate = value
        End Set
    End Property

#End Region

#Region "UpdatedBy"
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

#Region "UpdatedDate"
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
