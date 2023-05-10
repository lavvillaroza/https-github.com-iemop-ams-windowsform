Public Class WESMBillsExemption
    Public Sub New()

    End Sub
    Public Sub New(ByVal wesmbillsummaryno As Long,
                   ByVal noOffset As Boolean, ByVal noOffsetOrig As Boolean,
                   ByVal noSOA As Boolean, ByVal noSOAOrig As Boolean,
                   ByVal noDI As Boolean, ByVal noDIOrig As Boolean)
        Me._WESMBillSummaryNo = wesmbillsummaryno
        Me._NoOffset = noOffset
        Me._NoOffsetOrig = noOffsetOrig
        Me._NoSOA = noSOA
        Me._NoSOAOrig = noSOAOrig
        Me._NoDefaultInterest = noDI
        Me._NoDefaultInterestOrig = noDIOrig
    End Sub

    Public Sub New(ByVal wesmbillsummaryno As Long, ByVal idnumber As String, ByVal participant As String, ByVal wesmbillbatchno As Integer,
                   ByVal billperiod As Integer, ByVal chargetype As EnumChargeType, ByVal invoiceno As String, ByVal origduedate As Date,
                   ByVal newduedate As Date, ByVal beginningbalance As Decimal, ByVal endingbalance As Decimal,
                   ByVal noOffset As Boolean, ByVal noOffsetOrig As Boolean,
                   ByVal noSOA As Boolean, ByVal noSOAOrig As Boolean,
                   ByVal noDI As Boolean, ByVal noDIOrig As Boolean)

        Me._IDNumber = idnumber
        Me._ParticipantName = participant
        Me._WESMBillSummaryNo = wesmbillsummaryno
        Me._WESMBillBatchNo = wesmbillbatchno
        Me._BillPeriod = billperiod
        Me._ChargeType = chargetype
        Me._InvoiceNo = invoiceno
        Me._OrigDueDate = origduedate
        Me._NewDueDate = newduedate
        Me._BeginningBalance = beginningbalance
        Me._EndingBalance = endingbalance
        Me._NoOffset = noOffset
        Me._NoOffsetOrig = noOffsetOrig
        Me._NoSOA = noSOA
        Me._NoSOAOrig = noSOAOrig
        Me._NoDefaultInterest = noDI
        Me._NoDefaultInterestOrig = noDIOrig
    End Sub

#Region "Participant"
    Private _IDNumber As String
    Public Property IDNumber() As String
        Get
            Return _IDNumber
        End Get
        Set(ByVal value As String)
            _IDNumber = value
        End Set
    End Property
    Private _ParticipantName As String
    Public Property ParticipantName() As String
        Get
            Return _ParticipantName
        End Get
        Set(ByVal value As String)
            _ParticipantName = value
        End Set
    End Property
#End Region

#Region "WESMBill Summary No"
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


#Region "WESMBill Batch No"
    Private _WESMBillBatchNo As Integer
    Public Property WESMBillBatchNo() As Integer
        Get
            Return _WESMBillBatchNo
        End Get
        Set(ByVal value As Integer)
            _WESMBillBatchNo = value
        End Set
    End Property
#End Region

#Region "Bill Period"
    Private _BillPeriod As Integer
    Public Property BillPeriod() As Integer
        Get
            Return _BillPeriod
        End Get
        Set(ByVal value As Integer)
            _BillPeriod = value
        End Set
    End Property
#End Region

#Region "Charge Type"
    Private _ChargeType As EnumChargeType
    Public Property ChargeType() As EnumChargeType
        Get
            Return _ChargeType
        End Get
        Set(ByVal value As EnumChargeType)
            _ChargeType = value
        End Set
    End Property
#End Region

#Region "Invoice No"
    Private _InvoiceNo As String
    Public Property InvoiceNo() As String
        Get
            Return _InvoiceNo
        End Get
        Set(ByVal value As String)
            _InvoiceNo = value
        End Set
    End Property
#End Region

#Region "OrigDueDate"
    Private _OrigDueDate As Date
    Public Property OrigDueDate() As Date
        Get
            Return _OrigDueDate
        End Get
        Set(ByVal value As Date)
            _OrigDueDate = value
        End Set
    End Property
#End Region

#Region "NewDueDate"
    Private _NewDueDate As Date
    Public Property NewDueDate() As Date
        Get
            Return _NewDueDate
        End Get
        Set(ByVal value As Date)
            _NewDueDate = value
        End Set
    End Property
#End Region

#Region "BeginningBalance"
    Private _BeginningBalance As Decimal
    Public Property BeginningBalance() As Decimal
        Get
            Return _BeginningBalance
        End Get
        Set(ByVal value As Decimal)
            _BeginningBalance = value
        End Set
    End Property

#End Region

#Region "EndingBalance"
    Private _EndingBalance As Decimal
    Public Property EndingBalance() As Decimal
        Get
            Return _EndingBalance
        End Get
        Set(ByVal value As Decimal)
            _EndingBalance = value
        End Set
    End Property
#End Region

#Region "NoOffset"
    Private _NoOffset As Boolean
    Public Property NoOffset() As Boolean
        Get
            Return _NoOffset
        End Get
        Set(ByVal value As Boolean)
            _NoOffset = value
        End Set
    End Property
#End Region

#Region "NoOffsetOrig"
    Private _NoOffsetOrig As Boolean
    Public Property NoOffsetOrig() As Boolean
        Get
            Return _NoOffsetOrig
        End Get
        Set(ByVal value As Boolean)
            _NoOffsetOrig = value
        End Set
    End Property
#End Region

#Region "NoSOA"
    Private _NoSOA As Boolean
    Public Property NoSOA() As Boolean
        Get
            Return _NoSOA
        End Get
        Set(ByVal value As Boolean)
            _NoSOA = value
        End Set
    End Property
#End Region

#Region "NoSOAOrig"
    Private _NoSOAOrig As Boolean
    Public Property NoSOAOrig() As Boolean
        Get
            Return _NoSOAOrig
        End Get
        Set(ByVal value As Boolean)
            _NoSOAOrig = value
        End Set
    End Property
#End Region

#Region "NoDefaultInterest"
    Private _NoDefaultInterest As Boolean
    Public Property NoDefaultInterest() As Boolean
        Get
            Return _NoDefaultInterest
        End Get
        Set(ByVal value As Boolean)
            _NoDefaultInterest = value
        End Set
    End Property
#End Region

#Region "NoNSSOrig"
    Private _NoDefaultInterestOrig As Boolean
    Public Property NoDefaultInterestOrig() As Boolean
        Get
            Return _NoDefaultInterestOrig
        End Get
        Set(ByVal value As Boolean)
            _NoDefaultInterestOrig = value
        End Set
    End Property
#End Region

End Class
