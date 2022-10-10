Public Class SPADetails
    Implements IDisposable

#Region "WESMBillSummary"
    Private _WESMBillSummaryInfo As WESMBillSummary
    Public Property WESMBillSummaryInfo() As WESMBillSummary
        Get
            Return _WESMBillSummaryInfo
        End Get
        Set(ByVal value As WESMBillSummary)
            _WESMBillSummaryInfo = value
        End Set
    End Property
#End Region

#Region "BalanceAmount"
    Private _BalanceAmount As Decimal
    Public Property BalanceAmount() As Decimal
        Get
            Return _BalanceAmount
        End Get
        Set(ByVal value As Decimal)
            _BalanceAmount = value
        End Set
    End Property
#End Region

#Region "Balance Type"
    Private _BalanceType As New EnumBalanceType
    Public Property BalanceType() As EnumBalanceType
        Get
            Return _BalanceType
        End Get
        Set(ByVal value As EnumBalanceType)
            _BalanceType = value
        End Set
    End Property
#End Region

#Region "Charge Type"
    Private _ChargeType As EnumChargeType

    Public Property ChargeType() As EnumChargeType
        Get
            Return _ChargeType
        End Get
        Set(ByVal value As EnumChargeType)
            _ChargeType = value
        End Set
    End Property
#End Region

#Region "DebitCreditMemo"
    Private _DMCMNumber As Long
    Public Property DMCMNumber() As Long
        Get
            Return _DMCMNumber
        End Get
        Set(ByVal value As Long)
            _DMCMNumber = value
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
