Public Class ParentChildExemption
    Sub New()
        Me._ParentParticipantID = Nothing
        Me._ChildParticipantID = Nothing
        Me._ChargeType = Nothing
        Me._Status = Nothing
        Me._UpdatedBy = ""
        Me._UpdatedDate = Nothing
    End Sub

    Sub New(ByVal parentparticipant As AMParticipants, ByVal childparticipant As AMParticipants,
            ByVal chargetype As EnumChargeType, ByVal status As EnumStatus, ByVal updatedby As String, ByVal updateddate As Date)
        Me._ParentParticipantID = parentparticipant
        Me._ChildParticipantID = childparticipant
        Me._ChargeType = chargetype
        Me._Status = status
        Me._UpdatedBy = updatedby
        Me._UpdatedDate = updateddate
    End Sub

    Private _ParentParticipantID As AMParticipants
    Public Property ParentParticipantID() As AMParticipants
        Get
            Return _ParentParticipantID
        End Get
        Set(ByVal value As AMParticipants)
            _ParentParticipantID = value
        End Set
    End Property

    Private _ChildParticipantID As AMParticipants
    Public Property ChildParticipantID() As AMParticipants
        Get
            Return _ChildParticipantID
        End Get
        Set(ByVal value As AMParticipants)
            _ChildParticipantID = value
        End Set
    End Property

    Private _ChargeType As EnumChargeType
    Public Property ChargeType() As EnumChargeType
        Get
            Return _ChargeType
        End Get
        Set(ByVal value As EnumChargeType)
            _ChargeType = value
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
