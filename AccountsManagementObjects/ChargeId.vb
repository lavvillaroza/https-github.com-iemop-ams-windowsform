'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             ChargeId
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     August 24, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for Charges
'Arguments/Parameters:  
'Files/Database Tables:  AM_CHARGE_ID_LIB
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   August 24, 2011         Vladimir E. Espiritu            Class initialization
'	September 14, 2011      Juan Carlo L. Panopio           Changed Charge ID Type from Integer to EnumChargeType
'


Option Explicit On
Option Strict On

Public Class ChargeId

#Region "Initialization/Constructor"
    Public Sub New()
        Me.New("", "", Nothing, 0)
    End Sub



    Public Sub New(ByVal chId As String, ByVal description As String, ByVal chIdType As EnumChargeType)
        Me._ChargeId = chId
        Me._Description = description
        Me._cIDType = chIdType
    End Sub

    Public Sub New(ByVal chId As String, ByVal description As String, ByVal chIdType As EnumChargeType, ByVal status As Integer)
        Me._ChargeId = chId
        Me._Description = description
        Me._cIDType = chIdType
        Me._Status = status
    End Sub
#End Region

#Region "ChargeId"
    Private _ChargeId As String
    Public Property ChargeId() As String
        Get
            Return _ChargeId
        End Get
        Set(ByVal value As String)
            _ChargeId = value
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

#Region "ChargeID Type"
    Private _cIDType As EnumChargeType

    Public Property cIDType() As EnumChargeType
        Get
            Return _cIDType
        End Get
        Set(ByVal value As EnumChargeType)
            _cIDType = value
        End Set
    End Property

#End Region

#Region "Updated Date"
    Private _updDate As Date

    Public Property updDate() As Date
        Get
            Return _updDate
        End Get
        Set(ByVal value As Date)
            _updDate = value
        End Set
    End Property

#End Region

#Region "Updated By"
    Private _updBy As String

    Public Property updBy() As String
        Get
            Return _updBy
        End Get
        Set(ByVal value As String)
            _updBy = value
        End Set
    End Property

#End Region
End Class
