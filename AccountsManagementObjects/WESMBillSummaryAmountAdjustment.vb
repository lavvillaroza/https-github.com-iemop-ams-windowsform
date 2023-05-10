Public Class WBSAmountAdjustmentInWHTAX
    Sub New()
        Me._ReferenceID = 0
        Me._BillingPeriod = 0
        Me._BillingBatchNo = 0
        Me._BillingType = Nothing
        Me._ChargeType = Nothing
        Me._TotalARAmountAdjusted = 0D
        Me._TotalAPAmountAdjusted = 0D
        Me._WBSAmountAdjWHTAXDetailsList = Nothing
        Me._WBSDummiesList = Nothing
        Me._UpdatedBy = ""
        Me._UpdatedDate = Nothing
        Me._JVSetupNo = 0
    End Sub

    Sub New(ByVal refid As Integer, ByVal bpno As Integer, ByVal bbatchno As Integer, ByVal billingtype As EnumBalanceType, ByVal chargetype As EnumChargeType, ByVal totalaramountadj As Decimal, ByVal totalapamountadj As Decimal, ByVal jvsetupno As Long)
        Me._ReferenceID = refid
        Me._BillingPeriod = bpno
        Me._BillingBatchNo = bbatchno
        Me._BillingType = billingtype
        Me._ChargeType = chargetype
        Me._TotalARAmountAdjusted = totalaramountadj
        Me._TotalAPAmountAdjusted = totalapamountadj
        Me._JVSetupNo = jvsetupno
    End Sub

    Private _ReferenceID As Integer
    Public Property ReferenceID() As Integer
        Get
            Return _ReferenceID
        End Get
        Set(ByVal value As Integer)
            _ReferenceID = value
        End Set
    End Property

    Private _BillingPeriod As Integer
    Public Property BillingPeriod() As Integer
        Get
            Return _BillingPeriod
        End Get
        Set(ByVal value As Integer)
            _BillingPeriod = value
        End Set
    End Property

    Private _BillingBatchNo As Integer
    Public Property BillingBatchNo() As Integer
        Get
            Return _BillingBatchNo
        End Get
        Set(ByVal value As Integer)
            _BillingBatchNo = value
        End Set
    End Property

    Private _BillingType As EnumBalanceType
    Public Property BillingType() As EnumBalanceType
        Get
            Return _BillingType
        End Get
        Set(ByVal value As EnumBalanceType)
            _BillingType = value
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

    Private _Remarks As String
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property

    Private _JVSetupNo As Long
    Public Property JVSetupNo() As Long
        Get
            Return _JVSetupNo
        End Get
        Set(ByVal value As Long)
            _JVSetupNo = value
        End Set
    End Property

    Private _WBSAmountAdjWHTAXDetailsList As List(Of WBSAmountAdjustmentWHTAXDetails)
    Public Property WBSAmountAdjWHTAXDetailsList() As List(Of WBSAmountAdjustmentWHTAXDetails)
        Get
            Return _WBSAmountAdjWHTAXDetailsList
        End Get
        Set(ByVal value As List(Of WBSAmountAdjustmentWHTAXDetails))
            _WBSAmountAdjWHTAXDetailsList = value
        End Set
    End Property

    Private _WBSDummiesList As List(Of WESMBillSummary)
    Public Property WBSDummiesList() As List(Of WESMBillSummary)
        Get
            Return _WBSDummiesList
        End Get
        Set(ByVal value As List(Of WESMBillSummary))
            _WBSDummiesList = value
        End Set
    End Property

    Private _TotalARAmountAdjusted As Decimal
    Public Property TotalARAmountAdjusted() As Decimal
        Get
            Return _TotalARAmountAdjusted
        End Get
        Set(ByVal value As Decimal)
            _TotalARAmountAdjusted = value
        End Set
    End Property

    Private _TotalAPAmountAdjusted As Decimal
    Public Property TotalAPAmountAdjusted() As Decimal
        Get
            Return _TotalAPAmountAdjusted
        End Get
        Set(ByVal value As Decimal)
            _TotalAPAmountAdjusted = value
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
