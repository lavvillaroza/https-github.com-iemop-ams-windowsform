Public Class PaymentWBSHistoryBalance
    Implements IDisposable
    Public Sub New()

    End Sub

    Public Sub New(ByVal NewWESMBillBatchNo As Long, ByVal NewBillingPeriod As Integer, ByVal NewOriginalDueDate As Date,
                   ByVal NewBillingPeriodRemarks As String, ByVal NewIDnumber As AMParticipants,
                   ByVal NewAmountBalance As Decimal, ByVal NewChargeType As EnumChargeType)

        Me._WESMBillBatchNo = NewWESMBillBatchNo
        Me._BillingPeriod = NewBillingPeriod
        Me._OriginalDueDate = NewOriginalDueDate
        Me._BillingPeriodRemarks = NewBillingPeriodRemarks
        Me._IDNumber = NewIDnumber
        Me._AmountBalance = NewAmountBalance
        Me._ChargeType = NewChargeType
    End Sub

    Private _WESMBillBatchNo As Long
    Public Property WESMBillBatchNo() As Long
        Get
            Return _WESMBillBatchNo
        End Get
        Set(value As Long)
            _WESMBillBatchNo = value
        End Set
    End Property

    Private _BillingPeriod As Integer
    Public Property BillingPeriod() As Integer
        Get
            Return _BillingPeriod
        End Get
        Set(value As Integer)
            _BillingPeriod = value
        End Set
    End Property

    Private _OriginalDueDate As Date
    Public Property OriginalDueDate() As Date
        Get
            Return _OriginalDueDate
        End Get
        Set(value As Date)
            _OriginalDueDate = value
        End Set
    End Property

    Private _BillingPeriodRemarks As String
    Public Property BillingPeriodRemarks() As String
        Get
            Return _BillingPeriodRemarks
        End Get
        Set(value As String)
            _BillingPeriodRemarks = value
        End Set
    End Property

    Private _TotalBillAmount As Decimal
    Public Property TotalBillAmount() As Decimal
        Get
            Return _TotalBillAmount
        End Get
        Set(value As Decimal)
            _TotalBillAmount = value
        End Set
    End Property

    Private _IDNumber As New AMParticipants
    Public Property IDNumber() As AMParticipants
        Get
            Return _IDNumber
        End Get
        Set(value As AMParticipants)
            _IDNumber = value
        End Set
    End Property

    Private _AmountBalance As Decimal
    Public Property AmountBalance() As Decimal
        Get
            Return _AmountBalance
        End Get
        Set(value As Decimal)
            _AmountBalance = value
        End Set
    End Property

    Private _ChargeType As EnumChargeType
    Public Property ChargeType() As EnumChargeType
        Get
            Return _ChargeType
        End Get
        Set(value As EnumChargeType)
            _ChargeType = value
        End Set
    End Property

    Private _BalanceType As EnumBalanceType
    Public Property BalanceType() As EnumBalanceType
        Get
            Return _BalanceType
        End Get
        Set(value As EnumBalanceType)
            _BalanceType = value
        End Set
    End Property
#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
