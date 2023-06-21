Public Class WESMTransDetailsSummaryHistory
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

    Private _DueDate As Date
    Public Property DueDate() As Date
        Get
            Return _DueDate
        End Get
        Set(ByVal value As Date)
            _DueDate = value
        End Set
    End Property

    Private _AllocationDate As Date
    Public Property AllocationDate() As Date
        Get
            Return _AllocationDate
        End Get
        Set(ByVal value As Date)
            _AllocationDate = value
        End Set
    End Property

    Private _RemittanceDate As Date
    Public Property RemittanceDate() As Date
        Get
            Return _RemittanceDate
        End Get
        Set(ByVal value As Date)
            _RemittanceDate = value
        End Set
    End Property

    Private _AllocatedInEnergy As Decimal
    Public Property AllocatedInEnergy() As Decimal
        Get
            Return _AllocatedInEnergy
        End Get
        Set(ByVal value As Decimal)
            _AllocatedInEnergy = value
        End Set
    End Property

    Private _AllocatedInDefInt As Decimal
    Public Property AllocatedInDefInt() As Decimal
        Get
            Return _AllocatedInDefInt
        End Get
        Set(ByVal value As Decimal)
            _AllocatedInDefInt = value
        End Set
    End Property

    Private _AllocatedInVAT As Decimal
    Public Property AllocatedInVAT() As Decimal
        Get
            Return _AllocatedInVAT
        End Get
        Set(ByVal value As Decimal)
            _AllocatedInVAT = value
        End Set
    End Property

    Private _AllocatedInEWT As Decimal
    Public Property AllocatedInEWT() As Decimal
        Get
            Return _AllocatedInEWT
        End Get
        Set(ByVal value As Decimal)
            _AllocatedInEWT = value
        End Set
    End Property


    Private _AllocatedInWVAT As Decimal
    Public Property AllocatedInWVAT() As Decimal
        Get
            Return _AllocatedInWVAT
        End Get
        Set(ByVal value As Decimal)
            _AllocatedInWVAT = value
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
