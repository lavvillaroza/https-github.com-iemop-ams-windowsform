Public Class WBSAmountAdjustmentWHTAXDetails
    Sub New()
        Me._WBSDetails = New WESMBillSummary
        Me._AmountAdjusted = 0D
        Me._CreatedDocumentNo = 0
        Me._CreatedDocumentType = Nothing
        Me._FTFDMCMDoc = Nothing
    End Sub
    Private _WBSDetails As WESMBillSummary
    Public Property WBSDetails() As WESMBillSummary
        Get
            Return _WBSDetails
        End Get
        Set(ByVal value As WESMBillSummary)
            _WBSDetails = value
        End Set
    End Property

    Private _AmountAdjusted As Decimal
    Public Property AmountAdjusted() As Decimal
        Get
            Return _AmountAdjusted
        End Get
        Set(ByVal value As Decimal)
            _AmountAdjusted = value
        End Set
    End Property

    Private _CreatedDocumentNo As String
    Public Property CreatedDocumentNo() As Long
        Get
            Return _CreatedDocumentNo
        End Get
        Set(ByVal value As Long)
            _CreatedDocumentNo = value
        End Set
    End Property

    Private _CreatedDocumentType As BIRDocumentsType
    Public Property CreatedDocumentType() As BIRDocumentsType
        Get
            Return _CreatedDocumentType
        End Get
        Set(ByVal value As BIRDocumentsType)
            _CreatedDocumentType = value
        End Set
    End Property

    Private _FTFDMCMDoc As String
    Public Property FTFDMCMDoc() As String
        Get
            Return _FTFDMCMDoc
        End Get
        Set(ByVal value As String)
            _FTFDMCMDoc = value
        End Set
    End Property

End Class
