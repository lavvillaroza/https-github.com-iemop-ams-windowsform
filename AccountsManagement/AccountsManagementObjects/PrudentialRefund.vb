Public Class PrudentialRefund
    Implements IDisposable


    Public Sub New()

    End Sub


#Region "Trans No"
    Private _TransactionNo As Long
    Public Property TransactionNo() As Long
        Get
            Return _TransactionNo
        End Get
        Set(ByVal value As Long)
            _TransactionNo = value
        End Set
    End Property
#End Region

#Region "Remittance Date"
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

#Region "PRRefundDetails"
    Private _PRRefundDetails As List(Of PrudentialRefundDetails)
    Public Property PRRefundDetails() As List(Of PrudentialRefundDetails)
        Get
            Return _PRRefundDetails
        End Get
        Set(ByVal value As List(Of PrudentialRefundDetails))
            _PRRefundDetails = value
        End Set
    End Property
#End Region

#Region "PRJournalVoucher"
    Private _PRJournalVouchers As List(Of JournalVoucher)
    Public Property PRJournalVouchers() As List(Of JournalVoucher)
        Get
            Return _PRJournalVouchers
        End Get
        Set(ByVal value As List(Of JournalVoucher))
            _PRJournalVouchers = value
        End Set
    End Property
#End Region

#Region "EFT"
    Private _PREFT As List(Of EFT)
    Public Property PREFT() As List(Of EFT)
        Get
            Return _PREFT
        End Get
        Set(ByVal value As List(Of EFT))
            _PREFT = value
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
