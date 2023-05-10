Public Class WESMBillSummaryChangeParentId    
    Sub New()
        Me.New(Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub
    Sub New(ByVal billingperiod As Integer, ByVal parent As AMParticipants, ByVal child As AMParticipants, ByVal newparent As AMParticipants, ByVal status As EnumStatus)
        Me.BillingPeriod = billingperiod
        Me.ParentParticipants = parent
        Me.ChildParticipants = child
        Me.NewParentParticipants = newparent
        Me.Status = status
    End Sub

    Private _BillingPeriod As Integer
    Public Property BillingPeriod() As Integer
        Get
            Return _BillingPeriod
        End Get
        Set(ByVal value As Integer)
            _BillingPeriod = value
        End Set
    End Property

    Private _ParentParticipants As AMParticipants
    Public Property ParentParticipants() As AMParticipants
        Get
            Return _ParentParticipants
        End Get
        Set(ByVal value As AMParticipants)
            _ParentParticipants = value
        End Set
    End Property

    Private _ChildParticipants As AMParticipants
    Public Property ChildParticipants() As AMParticipants
        Get
            Return _ChildParticipants
        End Get
        Set(ByVal value As AMParticipants)
            _ChildParticipants = value
        End Set
    End Property

    Private _NewParentParticipants As AMParticipants
    Public Property NewParentParticipants() As AMParticipants
        Get
            Return _NewParentParticipants
        End Get
        Set(ByVal value As AMParticipants)
            _NewParentParticipants = value
        End Set
    End Property

    Private _Status As EnumStatus
    Public Property Status() As EnumStatus
        Get
            Return _Status
        End Get
        Set(ByVal value As EnumStatus)
            _Status = value
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

    Private _UpdatedDate As Date
    Public Property UpdatedDate() As Date
        Get
            Return _UpdatedDate
        End Get
        Set(ByVal value As Date)
            _UpdatedDate = value
        End Set
    End Property

End Class
