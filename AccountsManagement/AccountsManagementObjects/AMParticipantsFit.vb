<Serializable()> _
Public Class AMParticipantsFIT

#Region "Initialization/Constructor"
    Public Sub New()

    End Sub
#End Region


#Region "FitID"
    Private _FitID As Long
    Public Property FitID() As Long
        Get
            Return _FitID
        End Get
        Set(ByVal value As Long)
            _FitID = value
        End Set
    End Property
#End Region

#Region "ParentIDNumber"
    Private _ParentIDNumber As AMParticipants
    Public Property ParentIDNumber() As AMParticipants
        Get
            Return _ParentIDNumber
        End Get
        Set(ByVal value As AMParticipants)
            _ParentIDNumber = value
        End Set
    End Property
#End Region

#Region "BillingPeriod"
    Private _BillingPeriod As Integer
    Public Property BillingPeriod() As Integer
        Get
            Return _BillingPeriod
        End Get
        Set(ByVal value As Integer)
            _BillingPeriod = value
        End Set
    End Property
#End Region

#Region "ListofChildFIT"
    Private _ListofChildFIT As List(Of AMParticipants)
    Public Property ListofChildFIT() As List(Of AMParticipants)
        Get
            Return _ListofChildFIT
        End Get
        Set(ByVal value As List(Of AMParticipants))
            _ListofChildFIT = value
        End Set
    End Property

#End Region

End Class
