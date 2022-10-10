
Public Class STLNoticeNew
    Implements IDisposable

    Public Sub New()
        Me._BillingPeriod = ""
        Me._ParticularsChargeType = ""
        Me._ParticularsBillType = ""
        Me._OrigDueDate = Nothing
        Me._WESMBillInv = ""
        Me._Energy = 0
        Me._VAT = 0
        Me._DefaultOnEnergy = 0
        Me._MFAndVAT = 0
        Me._DefaultOnEnergy = 0
        Me._Others = 0        
        Me._CollPayAllocDate = Nothing
    End Sub

    Private _CollPayAllocDate As Date
    Public Property CollPayAllocDate() As Date
        Get
            Return _CollPayAllocDate
        End Get
        Set(value As Date)
            _CollPayAllocDate = value
        End Set
    End Property

    Private _AMParticipants As AMParticipants
    Public Property AMParticipants() As AMParticipants
        Get
            Return _AMParticipants
        End Get
        Set(value As AMParticipants)
            _AMParticipants = value
        End Set
    End Property

    Private _BillingPeriod As String
    Public Property BillingPeriod() As String
        Get
            Return _BillingPeriod
        End Get
        Set(value As String)
            _BillingPeriod = value
        End Set
    End Property

    Private _ParticularsChargeType As String
    Public Property ParticularsChargeType() As String
        Get
            Return _ParticularsChargeType
        End Get
        Set(value As String)
            _ParticularsChargeType = value
        End Set
    End Property

    Private _ParticularsBillType As String
    Public Property ParticularsBillType() As String
        Get
            Return _ParticularsBillType
        End Get
        Set(value As String)
            _ParticularsBillType = value
        End Set
    End Property

    Private _OrigDueDate As Date
    Public Property OrigDueDate() As Date
        Get
            Return _OrigDueDate
        End Get
        Set(value As Date)
            _OrigDueDate = value
        End Set
    End Property

    Private _WESMBillInv As String
    Public Property WESMBillInv() As String
        Get
            Return _WESMBillInv
        End Get
        Set(value As String)
            _WESMBillInv = value
        End Set
    End Property

    ' From PEMC -
    Private _Energy As Decimal
    Public Property Energy() As Decimal
        Get
            Return _Energy
        End Get
        Set(value As Decimal)
            _Energy = value
        End Set
    End Property

    Private _VAT As Decimal
    Public Property VAT() As Decimal
        Get
            Return _VAT
        End Get
        Set(value As Decimal)
            _VAT = value
        End Set
    End Property

    Private _DefaultOnEnergy As Decimal
    Public Property DefaultOnEnergy() As Decimal
        Get
            Return _DefaultOnEnergy
        End Get
        Set(value As Decimal)
            _DefaultOnEnergy = value
        End Set
    End Property

    Private _MFAndVAT As Decimal
    Public Property MFAndVAT() As Decimal
        Get
            Return _MFAndVAT
        End Get
        Set(value As Decimal)
            _MFAndVAT = value
        End Set
    End Property

    Private _DefaultOnMFwithVAT As Decimal
    Public Property DefaultOnMFwithVAT() As Decimal
        Get
            Return _DefaultOnMFwithVAT
        End Get
        Set(value As Decimal)
            _DefaultOnMFwithVAT = value
        End Set
    End Property

    Private _Others As Decimal
    Public Property Others() As Decimal
        Get
            Return _Others
        End Get
        Set(value As Decimal)
            _Others = value
        End Set
    End Property

    Private _TransType As Integer
    Public Property TransType() As Integer
        Get
            Return _TransType
        End Get
        Set(ByVal value As Integer)
            _TransType = value
        End Set
    End Property

    Private _IndexSorting As Integer
    Public Property IndexSorting() As Integer
        Get
            Return _IndexSorting
        End Get
        Set(ByVal value As Integer)
            _IndexSorting = value
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
