'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             WESMLogsTransaction
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     August 25, 2014
'Development Group:      Software Development and Support Division
'Description:            Class for WESM logs Transaction
'Arguments/Parameters:  
'Files/Database Tables:  WESMLOGS_TRANSACTION database table
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   August 25, 2014         Vladimir E. Espiritu            Class initialization
'

Option Explicit On
Option Strict On

Public Class WESMLogsTransaction

#Region "Initialization/Constructor"
    Public Sub New()
        Me._TransactionDate = Nothing
        Me._ModuleName = ""
        Me._ComputerName = ""
        Me._IPAddress = ""
        Me._Miscellaneous1 = ""
        Me._Miscellaneous2 = ""
        Me._Miscellaneous3 = ""
        Me._Remarks = ""
        Me._ColorCode = Nothing
        Me._LogType = Nothing
        Me._UpdatedBy = ""
    End Sub
#End Region

#Region "TransactionDate"
    Private _TransactionDate As Date
    Public Property TransactionDate() As Date
        Get
            Return _TransactionDate
        End Get
        Set(ByVal value As Date)
            _TransactionDate = value
        End Set
    End Property

#End Region

#Region "ModuleName"
    Private _ModuleName As String
    Public Property ModuleName() As String
        Get
            Return _ModuleName
        End Get
        Set(ByVal value As String)
            _ModuleName = value
        End Set
    End Property

#End Region

#Region "ComputerName"
    Private _ComputerName As String
    Public Property ComputerName() As String
        Get
            Return _ComputerName
        End Get
        Set(ByVal value As String)
            _ComputerName = value
        End Set
    End Property

#End Region

#Region "IPAddress"
    Private _IPAddress As String
    Public Property IPAddress() As String
        Get
            Return _IPAddress
        End Get
        Set(ByVal value As String)
            _IPAddress = value
        End Set
    End Property

#End Region

#Region "Miscellaneous1"
    Private _Miscellaneous1 As String
    Public Property Miscellaneous1() As String
        Get
            Return _Miscellaneous1
        End Get
        Set(ByVal value As String)
            _Miscellaneous1 = value
        End Set
    End Property

#End Region

#Region "Miscellaneous2"
    Private _Miscellaneous2 As String
    Public Property Miscellaneous2() As String
        Get
            Return _Miscellaneous2
        End Get
        Set(ByVal value As String)
            _Miscellaneous2 = value
        End Set
    End Property

#End Region

#Region "Miscellaneous3"
    Private _Miscellaneous3 As String
    Public Property Miscellaneous3() As String
        Get
            Return _Miscellaneous3
        End Get
        Set(ByVal value As String)
            _Miscellaneous3 = value
        End Set
    End Property

#End Region

#Region "Remarks"
    Private _Remarks As String
    Public ReadOnly Property Remarks() As String
        Get
            _Remarks = Me.Miscellaneous1 & " " & Me.Miscellaneous2 & " " & Me.Miscellaneous3
            Return _Remarks
        End Get
    End Property


#End Region

#Region "ColorCode"
    Private _ColorCode As EnumColorCode
    Public Property ColorCode() As EnumColorCode
        Get
            Return _ColorCode
        End Get
        Set(ByVal value As EnumColorCode)
            _ColorCode = value
        End Set
    End Property

#End Region

#Region "LogType"
    Private _LogType As EnumLogType
    Public Property LogType() As EnumLogType
        Get
            Return _LogType
        End Get
        Set(ByVal value As EnumLogType)
            _LogType = value
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

End Class