Public Class DeferredMain    
    Implements IDisposable

    Private _AllocationDate As Date
    Public Property AllocationDate() As Date
        Get
            Return _AllocationDate
        End Get
        Set(value As Date)
            _AllocationDate = value
        End Set
    End Property

    Private _DeferredPaymentNo As Long
    Public Property DeferredPaymentNo() As Long
        Get
            Return _DeferredPaymentNo
        End Get
        Set(value As Long)
            _DeferredPaymentNo = value
        End Set
    End Property

    Private _IDNumber As String
    Public Property IDNumber() As String
        Get
            Return _IDNumber
        End Get
        Set(value As String)
            _IDNumber = value
        End Set
    End Property

    Private _OutstandingBalanceDeferredPayment As Decimal
    Public Property OutstandingBalanceDeferredPayment() As Decimal
        Get
            Return _OutstandingBalanceDeferredPayment
        End Get
        Set(value As Decimal)
            _OutstandingBalanceDeferredPayment = value
        End Set
    End Property

    Private _PaymentNo As Long
    Public Property PaymentNo() As Long
        Get
            Return _PaymentNo
        End Get
        Set(value As Long)
            _PaymentNo = value
        End Set
    End Property

    Private _DeferredAmount As Decimal
    Public Property DeferredAmount() As Decimal
        Get
            Return _DeferredAmount
        End Get
        Set(value As Decimal)
            _DeferredAmount = value
        End Set
    End Property

    Private _DeferredType As New EnumDeferredType
    Public Property DeferredType() As EnumDeferredType
        Get
            Return _DeferredType
        End Get
        Set(value As EnumDeferredType)
            _DeferredType = value
        End Set
    End Property

    Private _ChargeType As New EnumChargeType
    Public Property ChargeType() As EnumChargeType
        Get
            Return _ChargeType
        End Get
        Set(value As EnumChargeType)
            _ChargeType = value
        End Set
    End Property

    Private _DMCMNumber As Long
    Public Property DMCMNumber() As Long
        Get
            Return _DMCMNumber
        End Get
        Set(value As Long)
            _DMCMNumber = value
        End Set
    End Property

    Private _Remarks As String
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(value As String)
            _Remarks = value
        End Set
    End Property

    Private _OriginalDate As Date
    Public Property OriginalDate() As Date
        Get
            Return _OriginalDate
        End Get
        Set(value As Date)
            _OriginalDate = value
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
