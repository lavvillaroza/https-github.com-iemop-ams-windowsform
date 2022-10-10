Public Class StatementOfAccountMainDetails
    Private SOANumber As Long
    Public Property _SOANumber() As Long
        Get
            Return SOANumber
        End Get
        Set(ByVal value As Long)
            SOANumber = value
        End Set
    End Property

    Private Amount As Decimal
    Public Property _Amount() As Decimal
        Get
            Return Amount
        End Get
        Set(ByVal value As Decimal)
            Amount = value
        End Set
    End Property

    Private SOAType As EnumSOAMainType
    Public Property _SOAType() As EnumSOAMainType
        Get
            Return SOAType
        End Get
        Set(ByVal value As EnumSOAMainType)
            SOAType = value
        End Set
    End Property

    Public Sub New(SOANumber As Long, Amount As Decimal, SOAType As EnumSOAMainType)
        SOANumber = _SOANumber
        Amount = _Amount
        SOAType = _SOAType
    End Sub

    Public Sub New()
        Me.New(0, 0, EnumSOAMainType.CollectionEnergy)
    End Sub
End Class
