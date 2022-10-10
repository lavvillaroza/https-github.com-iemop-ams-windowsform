Public Class SPAInvoices
    Implements IDisposable
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

    Private _AllowedToOffset As Integer
    Public Property AllowedToOffset() As Integer
        Get
            Return _AllowedToOffset
        End Get
        Set(value As Integer)
            _AllowedToOffset = value
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
