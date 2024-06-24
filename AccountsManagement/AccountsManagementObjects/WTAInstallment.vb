Public Class WTAInstallment
    Inherits WESMBillSummary
    Private _WTAINO As Long
    Public Property WTAINO() As Long
        Get
            Return _WTAINO
        End Get
        Set(ByVal value As Long)
            _WTAINO = value
        End Set
    End Property

    Private _STLRun As String
    Public Property STLRun() As String
        Get
            Return _STLRun
        End Get
        Set(ByVal value As String)
            _STLRun = value
        End Set
    End Property

    Private _Terms As Integer
    Public Property Terms() As Integer
        Get
            Return _Terms
        End Get
        Set(ByVal value As Integer)
            _Terms = value
        End Set
    End Property

    Private _ListofWTAInstallmentTerm As List(Of WTAInstallmentTerms)
    Public Property ListofWTAInstallmentTerm() As List(Of WTAInstallmentTerms)
        Get
            Return _ListofWTAInstallmentTerm
        End Get
        Set(ByVal value As List(Of WTAInstallmentTerms))
            _ListofWTAInstallmentTerm = value
        End Set
    End Property

    Private _Status As EnumWTAInstallmentStatus
    Public Property Status() As EnumWTAInstallmentStatus
        Get
            Return _Status
        End Get
        Set(ByVal value As EnumWTAInstallmentStatus)
            _Status = value
        End Set
    End Property
End Class
