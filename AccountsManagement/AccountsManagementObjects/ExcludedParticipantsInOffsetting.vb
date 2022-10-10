Public Class ExcludedParticipantsInOffsetting
#Region "Initialization/Constructor"
    Public Sub New()
        Me.New("", "")
    End Sub

    Public Sub New(ByVal idnumber As String, ByVal chargetype As EnumChargeType)
        Me.IDNumber = idnumber
        Me.ChargeType = chargetype
    End Sub
#End Region

#Region "IDNumber"
    Private _IDNumber As String
    Public Property IDNumber() As String
        Get
            Return _IDNumber
        End Get
        Set(ByVal value As String)
            _IDNumber = value
        End Set
    End Property

#End Region

#Region "ChargeType"
    Private _ChargeType As EnumChargeType
    Public Property ChargeType() As EnumChargeType
        Get
            Return _ChargeType
        End Get
        Set(ByVal value As EnumChargeType)
            _ChargeType = value
        End Set
    End Property

#End Region
End Class
