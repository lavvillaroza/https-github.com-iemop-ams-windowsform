Public Class PrudentialRefundDetails
    Implements IDisposable

    Public Sub New()

    End Sub
#Region "IDNumber"
    Private _IDNumber As String
    Public Property IDNumber() As String
        Get
            Return _IDNumber
        End Get
        Set(ByVal value As String)
            _IDNumber = value
        End Set
    End Property
#End Region

#Region "ParticipantID"
    Private _ParticipantID As String
    Public Property ParticipantID() As String
        Get
            Return _ParticipantID
        End Get
        Set(ByVal value As String)
            _ParticipantID = value
        End Set
    End Property
#End Region

#Region "FullName"
    Private _FullName As String
    Public Property FullName() As String
        Get
            Return _FullName
        End Get
        Set(ByVal value As String)
            _FullName = value
        End Set
    End Property
#End Region

#Region "PRAmount"
    Private _PRAmount As Decimal
    Public Property PRAmount() As Decimal
        Get
            Return _PRAmount
        End Get
        Set(ByVal value As Decimal)
            _PRAmount = value
        End Set
    End Property
#End Region

#Region "AmountRefund"
    Private _AmountRefund As Decimal
    Public Property AmountRefund() As Decimal
        Get
            Return _AmountRefund
        End Get
        Set(ByVal value As Decimal)
            _AmountRefund = value
        End Set
    End Property
#End Region


#Region "Interest Amount"
    Private _InterestAmount As Decimal
    Public Property InterestAmount() As Decimal
        Get
            Return _InterestAmount
        End Get
        Set(ByVal value As Decimal)
            _InterestAmount = value
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
