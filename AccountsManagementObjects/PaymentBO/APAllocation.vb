Public Class APAllocation
    Implements IDisposable

    Public Sub New()

    End Sub

    Public Sub New(ByVal WESMBillBatchNo As Long, ByVal BillingPeriod As Integer, ByVal IDNumber As String, ByVal ParticipantID As String,
                   ByVal INVnumber As String, ByVal EndingBalance As Decimal, ByVal EnergyWithhold As Decimal, ByVal NewEndingBalance As Decimal, ByVal DueDate As Date,
                   ByVal NewDueDate As Date, ByVal WESMBillSummaryNo As Long, ByVal PaymentCategory As EnumCollectionCategory, ByVal PaymentType As EnumPaymentNewType, ByVal ChargeType As EnumChargeType, ByVal BillingRemarks As String)
        Me._WESMBillBatchNo = WESMBillBatchNo
        Me._BillingPeriod = BillingPeriod
        Me._IDNumber = IDNumber
        Me._ParticipantID = ParticipantID
        Me._EndingBalance = EndingBalance
        Me._EnergyWithHold = EnergyWithhold
        Me._NewEndingBalance = NewEndingBalance
        Me._DueDate = DueDate
        Me._NewDueDate = NewDueDate
        Me._WESMBillSummaryNo = WESMBillSummaryNo
        Me._InvoiceNumber = INVnumber
        Me._PaymentCategory = PaymentCategory
        Me._PaymentType = PaymentType
        Me._ChargeType = ChargeType
        Me._BillingRemarks = BillingRemarks
    End Sub
#Region "WESMBillSummaryNo"
    Private _WESMBillSummaryNo As Long
    Public Property WESMBillSummaryNo() As Long
        Get
            Return _WESMBillSummaryNo
        End Get
        Set(value As Long)
            _WESMBillSummaryNo = value
        End Set
    End Property
#End Region

#Region "BillingPeriod"
    Private _BillingPeriod As Integer
    Public Property BillingPeriod() As Integer
        Get
            Return _BillingPeriod
        End Get
        Set(ByVal value As Integer)
            _BillingPeriod = value
        End Set
    End Property
#End Region

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

#Region "InvoiceNumber"
    Private _InvoiceNumber As String
    Public Property InvoiceNumber As String
        Get
            Return _InvoiceNumber
        End Get
        Set(ByVal value As String)
            _InvoiceNumber = value
        End Set
    End Property
#End Region

#Region "DueDate"
    Private _DueDate As Date
    Public Property DueDate As Date
        Get
            Return _DueDate
        End Get
        Set(ByVal value As Date)
            _DueDate = value
        End Set
    End Property
#End Region

#Region "EndingBalance"
    Private _EndingBalance As Decimal
    Public Property EndingBalance As Decimal
        Get
            Return _EndingBalance
        End Get
        Set(ByVal value As Decimal)
            _EndingBalance = value
        End Set
    End Property
#End Region

#Region "EnergyWithHolding"
    Private _EnergyWithHold As Decimal
    Public Property EnergyWithHold As Decimal
        Get
            Return _EnergyWithHold
        End Get
        Set(ByVal value As Decimal)
            _EnergyWithHold = value
        End Set
    End Property
#End Region

#Region "NewDueDate"
    Private _NewDueDate As Date
    Public Property NewDueDate As Date
        Get
            Return _NewDueDate
        End Get
        Set(ByVal value As Date)
            _NewDueDate = value
        End Set
    End Property
#End Region

#Region "NewEndingBalance"
    Private _NewEndingBalance As Decimal
    Public Property NewEndingBalance As Decimal
        Get
            Return _NewEndingBalance
        End Get
        Set(ByVal value As Decimal)
            _NewEndingBalance = value
        End Set
    End Property
#End Region

#Region "AllocationDate"
    Private _AllocationDate As Date
    Public Property AllocationDate As Date
        Get
            Return _AllocationDate
        End Get
        Set(ByVal value As Date)
            _AllocationDate = value
        End Set
    End Property
#End Region

#Region "RemittanceDate"
    Private _RemittanceDate As Date
    Public Property RemittanceDate As Date
        Get
            Return _RemittanceDate
        End Get
        Set(ByVal value As Date)
            _RemittanceDate = value
        End Set
    End Property
#End Region

#Region "AllocationAmount"
    Private _AllocationAmount As Decimal
    Public Property AllocationAmount As Decimal
        Get
            Return _AllocationAmount
        End Get
        Set(ByVal value As Decimal)
            _AllocationAmount = value
        End Set
    End Property
#End Region

#Region "PaymentType"
    Private _PaymentType As EnumPaymentNewType
    Public Property PaymentType As EnumPaymentNewType
        Get
            Return _PaymentType
        End Get
        Set(ByVal value As EnumPaymentNewType)
            _PaymentType = value
        End Set
    End Property
#End Region

#Region "PaymentCategory"
    Private _PaymentCategory As EnumCollectionCategory
    Public Property PaymentCategory As EnumCollectionCategory
        Get
            Return _PaymentCategory
        End Get
        Set(ByVal value As EnumCollectionCategory)
            _PaymentCategory = value
        End Set
    End Property
#End Region

#Region "ChargeType"
    Private _ChargeType As EnumChargeType
    Public Property ChargeType As EnumChargeType
        Get
            Return _ChargeType
        End Get
        Set(ByVal value As EnumChargeType)
            _ChargeType = value
        End Set
    End Property
#End Region

#Region "OffsettingSequence"
    Private _OffsettingSequence As Integer
    Public Property OffsettingSequence As Integer
        Get
            Return _OffsettingSequence
        End Get
        Set(ByVal value As Integer)
            _OffsettingSequence = value
        End Set
    End Property
#End Region

#Region "DMCM List"
    Private _GeneratedDMCM As New Long
    Public Property GeneratedDMCM() As Long
        Get
            Return _GeneratedDMCM
        End Get
        Set(value As Long)
            _GeneratedDMCM = value
        End Set
    End Property
#End Region

#Region "WESMBillBatchNo"
    Private _WESMBillBatchNo As Long
    Public Property WESMBillBatchNo() As Long
        Get
            Return _WESMBillBatchNo
        End Get
        Set(ByVal value As Long)
            _WESMBillBatchNo = value
        End Set
    End Property
#End Region

#Region "BillingRemarks"
    Private _BillingRemarks As String
    Public Property BillingRemarks() As String
        Get
            Return _BillingRemarks
        End Get
        Set(ByVal value As String)
            _BillingRemarks = value
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
