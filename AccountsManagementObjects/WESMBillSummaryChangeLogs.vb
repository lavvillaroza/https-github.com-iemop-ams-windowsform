Public Class WESMBillSummaryChangeLogs
    Implements IDisposable

#Region "WESMBillSummaryNo"
    Private _WESMBillSummaryNo As Long
    Public Property WESMBillSummaryNo() As Long
        Get
            Return _WESMBillSummaryNo
        End Get
        Set(ByVal value As Long)
            _WESMBillSummaryNo = value
        End Set
    End Property
#End Region

#Region "Change Information Type"
    Private _ChangeInfoType As String
    Public Property ChangeInfoType() As String
        Get
            Return _ChangeInfoType
        End Get
        Set(ByVal value As String)
            _ChangeInfoType = value
        End Set
    End Property
#End Region

#Region "OLD Value"
    Private _OldValue As String
    Public Property OldValue() As String
        Get
            Return _OldValue
        End Get
        Set(ByVal value As String)
            _OldValue = value
        End Set
    End Property
#End Region

#Region "Current Value"
    Private _NewValue As String
    Public Property NewValue() As String
        Get
            Return _NewValue
        End Get
        Set(ByVal value As String)
            _NewValue = value
        End Set
    End Property
#End Region

#Region "Updated Date"
    Private _UpdatedDate As String
    Public Property UpdatedDate() As String
        Get
            Return _UpdatedDate
        End Get
        Set(ByVal value As String)
            _UpdatedDate = value
        End Set
    End Property
#End Region

#Region "UpdatedBy"
    Private _UpdatedBy As String
    Public Property UpdatedBy() As String
        Get
            Return _UpdatedBy
        End Get
        Set(ByVal value As String)
            _UpdatedBy = value
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
