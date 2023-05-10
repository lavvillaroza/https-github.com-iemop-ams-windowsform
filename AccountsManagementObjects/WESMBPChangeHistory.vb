Public Class WESMBPChangeHistory
#Region "Initialization/Constructor"
    Public Sub New()
        Me._NewBillingPeriod = Nothing
        Me._SettlementRun = ""
        Me._Charge = New EnumChargeType()
        Me._DueDate = Nothing
        Me._OldBillingPeriod = Nothing
        Me._BatchCode = "0"
        Me._UpdatedBy = ""
        Me._UpdatedDate = Nothing
    End Sub
#End Region

#Region "NewBillingPeriod"
    Private _NewBillingPeriod As Integer
    Public Property NewBillingPeriod() As Integer
        Get
            Return _NewBillingPeriod
        End Get
        Set(ByVal value As Integer)
            _NewBillingPeriod = value
        End Set
    End Property

#End Region

#Region "SettlementRun"
    Private _SettlementRun As String
    Public Property SettlementRun() As String
        Get
            Return _SettlementRun
        End Get
        Set(ByVal value As String)
            _SettlementRun = value
        End Set
    End Property

#End Region

#Region "Charge"
    Private _Charge As EnumChargeType
    Public Property Charge() As EnumChargeType
        Get
            Return _Charge
        End Get
        Set(ByVal value As EnumChargeType)
            _Charge = value
        End Set
    End Property
#End Region

#Region "DueDate"
    Private _DueDate As Date
    Public Property DueDate() As Date
        Get
            Return _DueDate
        End Get
        Set(ByVal value As Date)
            _DueDate = value
        End Set
    End Property
#End Region

#Region "Old Billing Period"
    Private _OldBillingPeriod As Integer
    Public Property OldBillingPeriod() As Integer
        Get
            Return _OldBillingPeriod
        End Get
        Set(ByVal value As Integer)
            _OldBillingPeriod = value
        End Set
    End Property
#End Region

#Region "BatchCode"
    Private _BatchCode As String
    Public Property BatchCode() As String
        Get
            Return _BatchCode
        End Get
        Set(ByVal value As String)
            _BatchCode = value
        End Set
    End Property
#End Region

#Region "UpdatedBy"
    Private _UpdatedBy As String
    Public Property UpdatedBy() As String
        Get
            Return _UpdatedBy
        End Get
        Set(ByVal value As String)
            _UpdatedBy = value
        End Set
    End Property
#End Region

#Region "UpdatedDate"
    Private _UpdatedDate As Date
    Public Property UpdatedDate() As Date
        Get
            Return _UpdatedDate
        End Get
        Set(ByVal value As Date)
            _UpdatedDate = value
        End Set
    End Property
#End Region
End Class
