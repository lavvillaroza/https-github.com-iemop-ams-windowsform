' AMS
' Jonathan Saura
' 08/07/2015
'jsaura@hotmail.ph
'
'Interest Earned Properties

Option Explicit On
Option Strict On

Public Class InterestEarned

#Region "Initialized"
    Public Sub New()
        Me.New(0, Nothing, 0, 0, Nothing)
    End Sub

    Public Sub New(ByVal ID As Integer, ByVal transaction_date As Date, ByVal interest_earned As Decimal, ByVal status As EnumPostedTypeStatus, ByVal dt_created As Date)
        Me._rowId = ID

        Me._transDate = transaction_date
        Me._intEarned = interest_earned
        Me._status = status
        Me._dtCreated = dt_created
        Me._JVNumber = 0
        Me._Remarks = Nothing
        Me._updatedby = Nothing

    End Sub

#End Region

#Region "rowId"
    Private _rowId As Integer

    Public Property rowId() As Integer
        Get
            Return _rowId

        End Get

        Set(ByVal value As Integer)
            _rowId = value
        End Set
    End Property
#End Region

#Region "transDate"
    Private _transDate As Date

    Public Property transDate() As Date
        Get
            Return CDate(_transDate)

        End Get

        Set(ByVal value As Date)
            _transDate = value
        End Set
    End Property
#End Region

#Region "intEarned"


    Private _intEarned As Decimal
    Public Property intEarned() As Decimal
        Get
            Return _intEarned
        End Get
        Set(ByVal value As Decimal)
            _intEarned = value
        End Set
    End Property

#End Region

#Region "Status"
    Private _status As EnumPostedTypeStatus
    Public Property status() As EnumPostedTypeStatus
        Get
            Return _status
        End Get
        Set(ByVal value As EnumPostedTypeStatus)
            _status = value
        End Set
    End Property
#End Region

#Region "dtCreated"
    Private _dtCreated As Date
    Public Property dtCreated() As Date
        Get
            Return _dtCreated
        End Get
        Set(ByVal value As Date)
            _dtCreated = value
        End Set
    End Property
#End Region

#Region "AM_JV_NO"
    Private _JVNumber As Long
    Public Property JVNumber() As Long
        Get
            Return _JVNumber
        End Get
        Set(ByVal value As Long)
            _JVNumber = value
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

#Region "Updated BY"
    Private _updatedby As String
    Public Property updatedby() As String
        Get
            Return _updatedby
        End Get
        Set(ByVal value As String)
            _updatedby = value
        End Set
    End Property

#End Region
End Class
