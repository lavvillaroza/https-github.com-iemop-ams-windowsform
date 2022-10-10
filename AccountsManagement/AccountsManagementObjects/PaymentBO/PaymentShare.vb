Public Class PaymentShare
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

    Private _WESMBillSummaryNo As Long
    Public Property WESMBillSummaryNo() As Long
        Get
            Return _WESMBillSummaryNo
        End Get
        Set(value As Long)
            _WESMBillSummaryNo = value
        End Set
    End Property

    Private _WESMBillBatchNo As Long
    Public Property WESMBillBatchNo() As Long
        Get
            Return _WESMBillBatchNo
        End Get
        Set(value As Long)
            _WESMBillBatchNo = value
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

    Private _AmountShare As Decimal
    Public Property AmountShare() As Decimal
        Get
            Return _AmountShare
        End Get
        Set(value As Decimal)
            _AmountShare = value
        End Set
    End Property

    Private _AmountOffset As New List(Of PaymentShareDetails)
    Public Property AmountOffset() As List(Of PaymentShareDetails)
        Get
            Return _AmountOffset
        End Get
        Set(value As List(Of PaymentShareDetails))
            _AmountOffset = value
        End Set
    End Property

    Public ReadOnly Property AmountBalance() As Decimal
        Get            
            Dim AmountBal As Decimal = (From x In AmountOffset Select x.OffsetAmount).Sum            
            Return _AmountShare - AmountBal
        End Get
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

#Region "PaymentType"
    Private _PaymentType As EnumPaymentNewType
    Public Property PaymentType As EnumPaymentNewType
        Get
            Return _PaymentType
        End Get
        Set(ByVal value As EnumPaymentNewType)
            _PaymentType = value
        End Set
    End Property
#End Region

#Region "OffsettingSequence"
    Private _OffsettingSequence As Integer
    Public Property OffsettingSequence As Integer
        Get
            Return _OffsettingSequence
        End Get
        Set(ByVal value As Integer)
            _OffsettingSequence = value
        End Set
    End Property
#End Region

    Private _AllowOffsetToAR As Boolean
    Public Property AllowOffsetToAR() As Boolean
        Get
            Return _AllowOffsetToAR
        End Get
        Set(ByVal value As Boolean)
            _AllowOffsetToAR = value
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
