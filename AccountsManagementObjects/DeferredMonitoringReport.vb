Option Explicit On
Option Strict On

Public Class DeferredMonitoringReport

#Region "Initialization/Constructor"
    Public Sub New()
        Me._IDNumber = New AMParticipants()
        Me._OldEnergy = 0
        Me._OldVAT = 0
        Me._CurrentEnergy = 0
        Me._CurrentVAT = 0
        Me._RemittanceEnergy = 0
        Me._RemittanceVAT = 0
        Me._DeferralEnergy = 0
        Me._DeferralVAT = 0
    End Sub
#End Region


#Region "TransactionDate"
    Private _TransactionDate As Date
    Public Property TransactionDate() As Date
        Get
            Return _TransactionDate
        End Get
        Set(ByVal value As Date)
            _TransactionDate = value
        End Set
    End Property

#End Region

#Region "IDNumber"
    Private _IDNumber As AMParticipants
    Public Property IDNumber() As AMParticipants
        Get
            Return _IDNumber
        End Get
        Set(ByVal value As AMParticipants)
            _IDNumber = value
        End Set
    End Property


#End Region

#Region "OldEnergy"
    Private _OldEnergy As Decimal
    Public Property OldEnergy() As Decimal
        Get
            Return _OldEnergy
        End Get
        Set(ByVal value As Decimal)
            _OldEnergy = value
        End Set
    End Property
#End Region

#Region "OldVAT"
    Private _OldVAT As Decimal
    Public Property OldVAT() As Decimal
        Get
            Return _OldVAT
        End Get
        Set(ByVal value As Decimal)
            _OldVAT = value
        End Set
    End Property
#End Region

#Region "OldTotal"
    Public ReadOnly Property OldTotal() As Decimal
        Get
            Return Me.OldEnergy + Me.OldVAT
        End Get
    End Property
#End Region

#Region "CurrentEnergy"
    Private _CurrentEnergy As Decimal
    Public Property CurrentEnergy() As Decimal
        Get
            Return _CurrentEnergy
        End Get
        Set(ByVal value As Decimal)
            _CurrentEnergy = value
        End Set
    End Property
#End Region

#Region "CurrentVAT"
    Private _CurrentVAT As Decimal
    Public Property CurrentVAT() As Decimal
        Get
            Return _CurrentVAT
        End Get
        Set(ByVal value As Decimal)
            _CurrentVAT = value
        End Set
    End Property
#End Region

#Region "CurrentTotal"
    Public ReadOnly Property CurrentTotal() As Decimal
        Get
            Return Me.CurrentEnergy + Me.CurrentVAT
        End Get
    End Property
#End Region

#Region "RemittanceEnergy"
    Private _RemittanceEnergy As Decimal
    Public ReadOnly Property RemittanceEnergy() As Decimal
        Get
            _RemittanceEnergy = 0
            If Me.OldEnergy <> 0 And Me.CurrentEnergy = 0 Then
                _RemittanceEnergy = Me.OldEnergy
            End If

            Return _RemittanceEnergy
        End Get
    End Property
#End Region

#Region "RemittanceVAT"
    Private _RemittanceVAT As Decimal
    Public ReadOnly Property RemittanceVAT() As Decimal
        Get
            _RemittanceVAT = 0
            If Me.OldVAT <> 0 And Me.CurrentVAT = 0 Then
                _RemittanceVAT = Me.CurrentVAT
            End If

            Return _RemittanceVAT
        End Get
    End Property
#End Region

#Region "RemittanceTotal"
    Public ReadOnly Property RemittanceTotal() As Decimal
        Get
            Return Me.RemittanceEnergy + Me.RemittanceVAT
        End Get
    End Property
#End Region

#Region "DeferralEnergy"
    Private _DeferralEnergy As Decimal
    Public ReadOnly Property DeferralEnergy() As Decimal
        Get
            _DeferralEnergy = 0
            If Me.OldEnergy = 0 And Me.CurrentEnergy <> 0 Then
                _DeferralEnergy = Me.CurrentEnergy
            End If

            Return _DeferralEnergy
        End Get
    End Property
#End Region

#Region "DeferralVAT"
    Private _DeferralVAT As Decimal
    Public ReadOnly Property DeferralVAT() As Decimal
        Get
            _DeferralVAT = 0
            If Me.OldVAT = 0 And Me.CurrentVAT <> 0 Then
                _DeferralVAT = Me.CurrentVAT
            End If

            Return _DeferralVAT
        End Get
    End Property
#End Region

#Region "DeferralTotal"
    Public ReadOnly Property DeferralTotal() As Decimal
        Get
            Return Me.DeferralEnergy + DeferralVAT
        End Get
    End Property
#End Region

End Class
