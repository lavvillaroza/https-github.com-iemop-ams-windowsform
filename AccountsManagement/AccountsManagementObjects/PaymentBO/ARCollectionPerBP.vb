Public Class ARCollectionPerBP
    Implements IDisposable

#Region "Billing Period"
    Private _BillingPeriod As Integer
    Public Property BillingPeriod() As Integer
        Get
            Return _BillingPeriod
        End Get
        Set(value As Integer)
            _BillingPeriod = value
        End Set
    End Property
#End Region

#Region "Original Due Date"
    Private _OrigDueDate As Date
    Public Property OrigDueDate() As Date
        Get
            Return _OrigDueDate
        End Get
        Set(value As Date)
            _OrigDueDate = value
        End Set
    End Property
#End Region

#Region "Cash Amount"
    Private _CashAmount As Decimal
    Public Property CashAmount() As Decimal
        Get
            Return _CashAmount
        End Get
        Set(value As Decimal)
            _CashAmount = value
        End Set
    End Property
#End Region

#Region "Cash Amount Default Interest"
    Private _CashAmountDI As Decimal
    Public Property CashAmountDI() As Decimal
        Get
            Return _CashAmountDI
        End Get
        Set(value As Decimal)
            _CashAmountDI = value
        End Set
    End Property
#End Region

#Region "Draw Down Amount"
    Private _DrawDownAmount As Decimal
    Public Property DrawDownAmount() As Decimal
        Get
            Return _DrawDownAmount
        End Get
        Set(value As Decimal)
            _DrawDownAmount = value
        End Set
    End Property
#End Region

#Region "Draw Down Amount Default Interest"
    Private _DrawDownAmountDI As Decimal
    Public Property DrawDownAmountDI() As Decimal
        Get
            Return _DrawDownAmountDI
        End Get
        Set(value As Decimal)
            _DrawDownAmountDI = value
        End Set
    End Property
#End Region

#Region "Offset Amount"
    Private _OffsetAmount As Decimal
    Public Property OffsetAmount() As Decimal
        Get
            Return _OffsetAmount
        End Get
        Set(value As Decimal)
            _OffsetAmount = value
        End Set
    End Property
#End Region

#Region "Offset Amount Default Interest"
    Private _OffsetAmountDI As Decimal
    Public Property OffsetAmountDI() As Decimal
        Get
            Return _OffsetAmountDI
        End Get
        Set(value As Decimal)
            _OffsetAmountDI = value
        End Set
    End Property
#End Region

#Region "WESMBillBatchNo"
    Private _WESMBillBatchNo As Long
    Public Property WESMBillBatchNo() As Long
        Get
            Return _WESMBillBatchNo
        End Get
        Set(ByVal value As Long)
            _WESMBillBatchNo = value
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
