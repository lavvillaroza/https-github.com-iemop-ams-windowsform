'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             LogLib
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 5, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for logging the user actions
'Arguments/Parameters:  
'Files/Database Tables:  AC_USER_ACTIONS
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   September 05, 2011      Juan Carlo L. Panopio           Class initialization
'

Option Explicit On
Option Strict On


Public Class LogLib

#Region "Initialization/Constructor"

    Public Sub New()
        Me.New("", 0, "", "", "", "", My.Computer.Name, "")
    End Sub

    Public Sub New(ByVal uName As String, ByVal uLevel As Integer, ByVal modName As String, ByVal funName As String, ByVal frmName As String, ByVal rMarks As String, _
                     ByVal cName As String, ByVal ipAdd As String)
        Me._cName = cName
        Me._ipAdd = ipAdd
        Me._uName = uName
        Me._uLevel = uLevel
        Me._modName = modName
        Me._funName = funName
        Me._frmName = frmName
        Me._rMarks = rMarks
    End Sub
#End Region

#Region "User Name"
    Private _uName As String
    Public Property uName() As String
        Get
            Return _uName
        End Get
        Set(ByVal value As String)
            _uName = value
        End Set
    End Property

#End Region

#Region "User level"
    Private _uLevel As Integer
    Public Property uLevel() As Integer
        Get
            Return _uLevel
        End Get
        Set(ByVal value As Integer)
            _uLevel = value
        End Set
    End Property
#End Region

#Region "Module Name"
    Private _modName As String
    Public Property modName() As String
        Get
            Return _modName
        End Get
        Set(ByVal value As String)
            _modName = value
        End Set
    End Property
#End Region

#Region "Function Name"
    Private _funName As String
    Public Property funName() As String
        Get
            Return _funName
        End Get
        Set(ByVal value As String)
            _funName = value
        End Set
    End Property
#End Region

#Region "Form Name"
    Private _frmName As String
    Public Property frmName() As String
        Get
            Return _frmName
        End Get
        Set(ByVal value As String)
            _frmName = value
        End Set
    End Property
#End Region

#Region "Remarks"
    Private _rMarks As String
    Public Property rMarks() As String
        Get
            Return _rMarks
        End Get
        Set(ByVal value As String)
            _rMarks = value
        End Set
    End Property
#End Region

#Region "Remarks"
    Private _cName As String
    Public Property cName() As String
        Get
            Return _cName
        End Get
        Set(ByVal value As String)
            _cName = value
        End Set
    End Property
#End Region

#Region "IP Address"
    Private _ipAdd As String
    Public Property ipAdd() As String
        Get
            Return _ipAdd
        End Get
        Set(ByVal value As String)
            _ipAdd = value
        End Set
    End Property
#End Region
End Class
