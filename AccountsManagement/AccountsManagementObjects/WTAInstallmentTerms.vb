Public Class WTAInstallmentTerms
    Implements IDisposable

    Private _Term As Integer
    Public Property Term() As Integer
        Get
            Return _Term
        End Get
        Set(ByVal value As Integer)
            _Term = value
        End Set
    End Property

    Private _TermDueDate As Date
    Public Property TermDueDate() As Date
        Get
            Return _TermDueDate
        End Get
        Set(ByVal value As Date)
            _TermDueDate = value
        End Set
    End Property

    Private _TermNewDueDate As Date
    Public Property TermNewDueDate() As Date
        Get
            Return _TermNewDueDate
        End Get
        Set(ByVal value As Date)
            _TermNewDueDate = value
        End Set
    End Property

    Private _TermAmount As Decimal
    Public Property TermAmount() As Decimal
        Get
            Return _TermAmount
        End Get
        Set(ByVal value As Decimal)
            _TermAmount = value
        End Set
    End Property

    Private _PaidAmount As Decimal
    Public Property PaidAmount() As Decimal
        Get
            Return _PaidAmount
        End Get
        Set(ByVal value As Decimal)
            _PaidAmount = value
        End Set
    End Property

    Private _DefaultAmount As Decimal
    Public Property DefaultAmount() As Decimal
        Get
            Return _DefaultAmount
        End Get
        Set(ByVal value As Decimal)
            _DefaultAmount = value
        End Set
    End Property

    Private _TermStatus As EnumWTAInstallmentTermStatus
    Public Property TermStatus() As EnumWTAInstallmentTermStatus
        Get
            Return _TermStatus
        End Get
        Set(ByVal value As EnumWTAInstallmentTermStatus)
            _TermStatus = value
        End Set
    End Property

    Private _WTAINO As Long
    Public Property WTAINO() As Long
        Get
            Return _WTAINO
        End Get
        Set(ByVal value As Long)
            _WTAINO = value
        End Set
    End Property

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
