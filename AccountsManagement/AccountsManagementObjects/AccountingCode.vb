'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             AccountingCode
'Orginal Author:         Vladimir E.Espiritu
'File Creation Date:     August 22, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for Accounting Code
'Arguments/Parameters:  
'Files/Database Tables:  AM_ACCOUNTING_CODE
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description
'   August 22, 2011         Vladimir E.Espiritu         Class initialization
'	



Option Explicit On
Option Strict On

Public Class AccountingCode

#Region "Initialization/Constructor"
    Public Sub New()
        Me.New("", "")
    End Sub

    Public Sub New(ByVal acctcode As String, ByVal description As String)
        Me.New(acctcode, description, 1, Nothing, "")
    End Sub

    Public Sub New(ByVal acctcode As String, ByVal description As String, ByVal status As Integer, ByVal DateCommitted As Date, ByVal UpdatedBy As String)
        Me._AccountCode = acctcode
        Me._Description = description
        Me._Status = status
        Me._DateCommit = DateCommitted
        Me._updatedBy = UpdatedBy

    End Sub
#End Region

#Region "AccountCode"
    Private _AccountCode As String
    Public Property AccountCode() As String
        Get
            Return _AccountCode
        End Get
        Set(ByVal value As String)
            _AccountCode = value
        End Set
    End Property

#End Region

#Region "Description"
    Private _Description As String
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
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

#Region "Date Committed"
    Private _DateCommit As Date
    Public Property DateCommit() As Date
        Get
            Return _DateCommit
        End Get
        Set(ByVal value As Date)
            _DateCommit = value
        End Set
    End Property

#End Region

#Region "Updated By"
    Private _updatedBy As String
    Public Property updatedBy() As String
        Get
            Return _updatedBy
        End Get
        Set(ByVal value As String)
            _updatedBy = value
        End Set
    End Property

#End Region

End Class
