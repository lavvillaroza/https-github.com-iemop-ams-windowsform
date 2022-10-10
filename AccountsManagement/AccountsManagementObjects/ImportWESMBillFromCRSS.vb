Option Explicit On
Option Strict On

Public Class ImportWESMBillFromCRSS
    Private _BillingPeriod As Integer
    Public Property BillingPeriod() As Integer
        Get
            Return _BillingPeriod
        End Get
        Set(ByVal value As Integer)
            _BillingPeriod = value
        End Set
    End Property

    Private _SettlementRun As String
    Public Property SettlementRun() As String
        Get
            Return _SettlementRun
        End Get
        Set(ByVal value As String)
            _SettlementRun = value
        End Set
    End Property

    Private _FileType As EnumFileType
    Public Property FileType() As EnumFileType
        Get
            Return _FileType
        End Get
        Set(ByVal value As EnumFileType)
            _FileType = value
        End Set
    End Property

    Private _TotalARAmount As Decimal
    Public Property TotalARAmount() As Decimal
        Get
            Return _TotalARAmount
        End Get
        Set(ByVal value As Decimal)
            _TotalARAmount = value
        End Set
    End Property

    Private _TotalAPAmount As Decimal
    Public Property TotalAPAmount() As Decimal
        Get
            Return _TotalAPAmount
        End Get
        Set(ByVal value As Decimal)
            _TotalAPAmount = value
        End Set
    End Property

    Private _ListOfError As List(Of String)
    Public Property ListOfError As List(Of String)
        Get
            Return _ListOfError
        End Get
        Set(ByVal value As List(Of String))
            _ListOfError = value
        End Set
    End Property

    Private _Remarks As String
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property


    Private _WESMBillType As EnumImportFileType
    Public Property WESMBillType() As EnumImportFileType
        Get
            Return _WESMBillType
        End Get
        Set(ByVal value As EnumImportFileType)
            _WESMBillType = value
        End Set
    End Property
End Class
