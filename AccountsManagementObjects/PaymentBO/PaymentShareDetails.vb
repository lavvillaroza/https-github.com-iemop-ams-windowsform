Public Class PaymentShareDetails
    Implements IDisposable

    Private _PaymentShareNo As Long
    Public Property PaymentShareNo() As Long
        Get
            Return _PaymentShareNo
        End Get
        Set(value As Long)
            _PaymentShareNo = value
        End Set
    End Property


    Private _ForWESMBillSummaryNo As Long
    Public Property ForWESMBillSummaryNo() As Long
        Get
            Return _ForWESMBillSummaryNo
        End Get
        Set(value As Long)
            _ForWESMBillSummaryNo = value
        End Set
    End Property

    Private _BillingPeriod As Long
    Public Property BillingPeriod() As Long
        Get
            Return _BillingPeriod
        End Get
        Set(value As Long)
            _BillingPeriod = value
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

    Private _InvoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return _InvoiceNumber
        End Get
        Set(value As String)
            _InvoiceNumber = value
        End Set
    End Property

    Private _OffsetAmount As Decimal
    Public Property OffsetAmount() As Decimal
        Get
            Return _OffsetAmount
        End Get
        Set(value As Decimal)
            _OffsetAmount = value
        End Set
    End Property

#Region "OffsettingBatch"
    Private _BatchGroupSequence As Integer
    Public Property BatchGroupSequence As Integer
        Get
            Return _BatchGroupSequence
        End Get
        Set(ByVal value As Integer)
            _BatchGroupSequence = value
        End Set
    End Property
#End Region

#Region "Charge Type"
    Private _ChargeType As EnumChargeType
    Public Property ChargeType() As EnumChargeType
        Get
            Return _ChargeType
        End Get
        Set(value As EnumChargeType)
            _ChargeType = value
        End Set
    End Property
#End Region

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
