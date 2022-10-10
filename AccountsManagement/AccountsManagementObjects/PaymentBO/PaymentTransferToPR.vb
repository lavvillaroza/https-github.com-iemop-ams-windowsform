Public Class PaymentTransferToPR
    Implements IDisposable

    Public Sub New()

    End Sub

    Private _IDNumber As String
    Public Property IDNumber() As String
        Get
            Return _IDNumber
        End Get
        Set(value As String)
            _IDNumber = value
        End Set
    End Property

    Private _ParticipantID As String
    Public Property ParticipantID() As String
        Get
            Return _ParticipantID
        End Get
        Set(value As String)
            _ParticipantID = value
        End Set
    End Property

    Private _PaymentOnExcessCollection As Decimal
    Public Property PaymentOnExcessCollection() As Decimal
        Get
            Return _PaymentOnExcessCollection
        End Get
        Set(value As Decimal)
            _PaymentOnExcessCollection = value
        End Set
    End Property

    Private _PaymentOnDeferredonEnergy As Decimal
    Public Property PaymentOnDeferredonEnergy() As Decimal
        Get
            Return _PaymentOnDeferredonEnergy
        End Get
        Set(value As Decimal)
            _PaymentOnDeferredonEnergy = value
        End Set
    End Property

    Private _PaymentOnDeferredonVATonEnergy As Decimal
    Public Property PaymentOnDeferredonVATonEnergy() As Decimal
        Get
            Return _PaymentOnDeferredonVATonEnergy
        End Get
        Set(value As Decimal)
            _PaymentOnDeferredonVATonEnergy = value
        End Set
    End Property

    Private _OffsetOnOffsetDeferredonEnergy As Decimal
    Public Property OffsetOnOffsetDeferredonEnergy() As Decimal
        Get
            Return _OffsetOnOffsetDeferredonEnergy
        End Get
        Set(value As Decimal)
            _OffsetOnOffsetDeferredonEnergy = value
        End Set
    End Property

    Private _OffsetOnDeferredonVATonEnergy As Decimal
    Public Property OffsetOnOffsetDeferredonVATonEnergy() As Decimal
        Get
            Return _OffsetOnDeferredonVATonEnergy
        End Get
        Set(value As Decimal)
            _OffsetOnDeferredonVATonEnergy = value
        End Set
    End Property

    Private _PaymentOnMFWithVAT As Decimal
    Public Property PaymentOnMFWithVAT() As Decimal
        Get
            Return _PaymentOnMFWithVAT
        End Get
        Set(value As Decimal)
            _PaymentOnMFWithVAT = value
        End Set
    End Property

    Private _PaymentOnEnergy As Decimal
    Public Property PaymentOnEnergy() As Decimal
        Get
            Return _PaymentOnEnergy
        End Get
        Set(value As Decimal)
            _PaymentOnEnergy = value
        End Set
    End Property

    Private _PaymentOnVATonEnergy As Decimal
    Public Property PaymentOnVATonEnergy() As Decimal
        Get
            Return _PaymentOnVATonEnergy
        End Get
        Set(value As Decimal)
            _PaymentOnVATonEnergy = value
        End Set
    End Property

    Private _TotalPaymentAllocated As Decimal
    Public Property TotalPaymentAllocated() As Decimal
        Get
            Return _TotalPaymentAllocated
        End Get
        Set(value As Decimal)
            _TotalPaymentAllocated = value
        End Set
    End Property

    Private _FullyTransferToPR As Boolean
    Public Property FullyTransferToPR() As Boolean
        Get
            Return _FullyTransferToPR
        End Get
        Set(value As Boolean)
            _FullyTransferToPR = value
        End Set
    End Property

    Private _TransferToPrudential As Decimal
    Public Property TransferToPrudential() As Decimal
        Get
            Return _TransferToPrudential
        End Get
        Set(value As Decimal)
            _TransferToPrudential = value
        End Set
    End Property

    Private _FullyTransferToFinPen As Boolean
    Public Property FullyTransferToFinPen() As Boolean
        Get
            Return _FullyTransferToFinPen
        End Get
        Set(value As Boolean)
            _FullyTransferToFinPen = value
        End Set
    End Property

    Private _TransferToFinPen As Decimal
    Public Property TransferToFinPen() As Decimal
        Get
            Return _TransferToFinPen
        End Get
        Set(value As Decimal)
            _TransferToFinPen = value
        End Set
    End Property


    Private _TotalAmountForRemittance As Decimal
    Public Property TotalAmountForRemittance() As Decimal
        Get
            Return _TotalAmountForRemittance
        End Get
        Set(value As Decimal)
            _TotalAmountForRemittance = value
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
