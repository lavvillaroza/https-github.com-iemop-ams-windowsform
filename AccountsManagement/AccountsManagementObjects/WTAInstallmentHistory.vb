Public Class WTAInstallmentHistory
    Private _Term As Integer
    Public Property Term() As Integer
        Get
            Return _Term
        End Get
        Set(ByVal value As Integer)
            _Term = value
        End Set
    End Property
    Private _TermNewDueDate As Date
    Public Property TermNewDueDate() As Date
        Get
            Return _TermNewDueDate
        End Get
        Set(ByVal value As Date)
            _TermNewDueDate = value
        End Set
    End Property
    Private _PaidAmount As Decimal
    Public Property PaidAmount() As Decimal
        Get
            Return _PaidAmount
        End Get
        Set(ByVal value As Decimal)
            _PaidAmount = value
        End Set
    End Property

    Private _DefaultAmount As Decimal
    Public Property DefaultAmount() As Decimal
        Get
            Return _DefaultAmount
        End Get
        Set(ByVal value As Decimal)
            _DefaultAmount = value
        End Set
    End Property

    Private _WTAINO As Long
    Public Property WTAINO() As Long
        Get
            Return _WTAINO
        End Get
        Set(ByVal value As Long)
            _WTAINO = value
        End Set
    End Property
    Private _CollectionDate As Date
    Public Property CollectionDate() As Date
        Get
            Return _CollectionDate
        End Get
        Set(ByVal value As Date)
            _CollectionDate = value
        End Set
    End Property

    Private _CollectionNo As Long
    Public Property CollectionNo() As Long
        Get
            Return _CollectionNo
        End Get
        Set(ByVal value As Long)
            _CollectionNo = value
        End Set
    End Property
End Class
