Public Class DebitCreditMemoSummaryNew
    Implements IDisposable

#Region "Participants"
    Private _AMParticipantInfo As AMParticipants
    Public Property AMParticipantInfo() As AMParticipants
        Get
            Return _AMParticipantInfo
        End Get
        Set(value As AMParticipants)
            _AMParticipantInfo = value
        End Set
    End Property
#End Region

#Region "DMCMNumber"
    Private _DMCMNumber As Long
    Public Property DMCMNumber() As Long
        Get
            Return _DMCMNumber
        End Get
        Set(value As Long)
            _DMCMNumber = value
        End Set
    End Property
#End Region

#Region "DMCMAmount"
    Private _DMCMAmount As Decimal
    Public Property DMCMAmount() As Decimal
        Get
            Return _DMCMAmount
        End Get
        Set(value As Decimal)
            _DMCMAmount = value
        End Set
    End Property
#End Region

#Region "DMCMSummaryType"
    Private _DMCMSummaryType As EnumDMCMSummaryType
    Public Property DMCMSummaryType() As EnumDMCMSummaryType
        Get
            Return _DMCMSummaryType
        End Get
        Set(value As EnumDMCMSummaryType)
            _DMCMSummaryType = value
        End Set
    End Property
#End Region

#Region "UpdatedBy"
    Private _UpdatedBy As String
    Public Property UpdatedBy() As String
        Get
            Return _UpdatedBy
        End Get
        Set(value As String)
            _UpdatedBy = value
        End Set
    End Property
#End Region

#Region "UpdatedDate"
    Private _UpdatedDate As Date
    Public Property UpdatedDate() As Date
        Get
            Return _UpdatedDate
        End Get
        Set(value As Date)
            _UpdatedDate = value
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
