Public Class WESMTransDetailsSummary
    Implements IDisposable

    Private _BuyerTransNo As String
    Public Property BuyerTransNo() As String
        Get
            Return _BuyerTransNo
        End Get
        Set(ByVal value As String)
            _BuyerTransNo = value
        End Set
    End Property

    Private _BuyerBillingID As String
    Public Property BuyerBillingID() As String
        Get
            Return _BuyerBillingID
        End Get
        Set(ByVal value As String)
            _BuyerBillingID = value
        End Set
    End Property

    Private _BuyerSTLID As String
    Public Property BuyerSTLID() As String
        Get
            Return _BuyerSTLID
        End Get
        Set(ByVal value As String)
            _BuyerSTLID = value
        End Set
    End Property

    Private _SellerTransNo As String
    Public Property SellerTransNo() As String
        Get
            Return _SellerTransNo
        End Get
        Set(ByVal value As String)
            _SellerTransNo = value
        End Set
    End Property

    Private _SellerBillingID As String
    Public Property SellerBillingID() As String
        Get
            Return _SellerBillingID
        End Get
        Set(ByVal value As String)
            _SellerBillingID = value
        End Set
    End Property

    Private _SellerSTLID As String
    Public Property SellerSTLID() As String
        Get
            Return _SellerSTLID
        End Get
        Set(ByVal value As String)
            _SellerSTLID = value
        End Set
    End Property

    Private _DueDate As Date
    Public Property DueDate() As Date
        Get
            Return _DueDate
        End Get
        Set(ByVal value As Date)
            _DueDate = value
        End Set
    End Property

    Private _NewDueDate As Date
    Public Property NewDueDate() As Date
        Get
            Return _NewDueDate
        End Get
        Set(ByVal value As Date)
            _NewDueDate = value
        End Set
    End Property

    Private _OrigBalanceInEnergy As Decimal
    Public Property OrigBalanceInEnergy() As Decimal
        Get
            Return _OrigBalanceInEnergy
        End Get
        Set(ByVal value As Decimal)
            _OrigBalanceInEnergy = value
        End Set
    End Property

    Private _OrigBalanceInVAT As Decimal
    Public Property OrigBalanceInVAT() As Decimal
        Get
            Return _OrigBalanceInVAT
        End Get
        Set(ByVal value As Decimal)
            _OrigBalanceInVAT = value
        End Set
    End Property

    Private _OrigBalanceInEWT As Decimal
    Public Property OrigBalanceInEWT() As Decimal
        Get
            Return _OrigBalanceInEWT
        End Get
        Set(ByVal value As Decimal)
            _OrigBalanceInEWT = value
        End Set
    End Property

    Private _OutstandingBalanceInEnergy As Decimal
    Public Property OutstandingBalanceInEnergy() As Decimal
        Get
            Return _OutstandingBalanceInEnergy
        End Get
        Set(ByVal value As Decimal)
            _OutstandingBalanceInEnergy = value
        End Set
    End Property

    Private _OutstandingBalanceInVAT As Decimal
    Public Property OutstandingBalanceInVAT() As Decimal
        Get
            Return _OutstandingBalanceInVAT
        End Get
        Set(ByVal value As Decimal)
            _OutstandingBalanceInVAT = value
        End Set
    End Property

    Private _OutstandingBalanceInEWT As Decimal
    Public Property OutstandingBalanceInEWT() As Decimal
        Get
            Return _OutstandingBalanceInEWT
        End Get
        Set(ByVal value As Decimal)
            _OutstandingBalanceInEWT = value
        End Set
    End Property

    Private _Status As String
    Public Property Status() As String
        Get
            Return _Status
        End Get
        Set(ByVal value As String)
            _Status = value
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
