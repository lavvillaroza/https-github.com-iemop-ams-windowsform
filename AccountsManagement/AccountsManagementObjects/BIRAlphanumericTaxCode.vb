Public Class BIRAlphanumericTaxCode


    Public Sub New(ByVal oATCName As String, ByVal oATCDesc As String, ByVal oATCRate As Decimal, ByVal oUpdatedDate As Date, ByVal oUpdatedBy As String)
        Me._ATCName = oATCName
        Me._ATCDescription = oATCDesc
        Me._ATCRate = oATCRate
        Me._UpdatedBy = oUpdatedBy
        Me._UpdatedDate = oUpdatedDate
    End Sub

    Sub New()
        ' TODO: Complete member initialization 
    End Sub

    Private _ATCName As String

    Public Property ATCName() As String
        Get
            Return _ATCName
        End Get
        Set(ByVal value As String)
            _ATCName = value
        End Set
    End Property

    Private _ATCDescription As String
    Public Property ATCDescription() As String
        Get
            Return _ATCDescription
        End Get
        Set(ByVal value As String)
            _ATCDescription = value
        End Set
    End Property

    Private _ATCRate As Decimal
    Public Property ATCRate() As Decimal
        Get
            Return _ATCRate
        End Get
        Set(ByVal value As Decimal)
            _ATCRate = value
        End Set
    End Property

    Private _UpdatedDate As Date
    Public Property UpdateDate() As Date
        Get
            Return _UpdatedDate
        End Get
        Set(ByVal value As Date)
            _UpdatedDate = value
        End Set
    End Property

    Private _UpdatedBy As String
    Public Property UpdatedBy() As String
        Get
            Return _UpdatedBy
        End Get
        Set(ByVal value As String)
            _UpdatedBy = value
        End Set
    End Property
End Class
