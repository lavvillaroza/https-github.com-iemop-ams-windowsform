Public Class WESMBillSummaryAmountAdjustmentMonitoring
    Inherits WESMBillSummary
    Private _DMCMEntry As Long
    Public Property DMCMEntry() As Long
        Get
            Return _DMCMEntry
        End Get
        Set(ByVal value As Long)
            _DMCMEntry = value
        End Set
    End Property
End Class
