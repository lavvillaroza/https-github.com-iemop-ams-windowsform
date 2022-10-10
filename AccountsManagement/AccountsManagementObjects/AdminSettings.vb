'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             AMParticipants
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     December 02, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Admin Settings
'Arguments/Parameters:  
'Files/Database Tables:  AM_ADMIN_SETTINGS
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   February 7, 2012        Vladimir E. Espiritu            Class initialization
'

Option Explicit On
Option Strict On
Imports System.IO
Imports AccountsManagementObjects
Imports System.Configuration

Public Class AdminSettings

#Region "Initialization/Constructor"
    Public Sub New()
        Me.New("", "", "")
    End Sub

    Public Sub New(ByVal codename As String, ByVal description As String, ByVal value As String)
        Me.New(codename, description, value, "", Nothing)
    End Sub

    Public Sub New(ByVal codename As String, ByVal description As String, ByVal value As String, _
                   ByVal updatedby As String, ByVal updateddate As Date)
        Me._CodeName = codename
        Me._Description = description
        Me._Value = value
        Me._UpdatedBy = updatedby
        Me._UpdatedDate = updateddate
    End Sub
#End Region

#Region "CodeName"
    Private _CodeName As String
    Public Property CodeName() As String
        Get
            Return _CodeName
        End Get
        Set(ByVal value As String)
            _CodeName = value
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

#Region "Value"
    Private _Value As String
    Public Property Value() As String
        Get
            Return _Value
        End Get
        Set(ByVal value As String)
            _Value = value
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

End Class
