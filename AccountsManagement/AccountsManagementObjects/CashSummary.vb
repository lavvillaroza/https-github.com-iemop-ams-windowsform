Public Class CashSummary

    Public Sub New()
        Me._TransactionDate = SystemDate
        Me._JournalVoucherNo = ""
        Me._DistributionRef = ""
        Me._DocumentNo = ""
        Me._Participant = New AMParticipants
        Me._Debit = 0
        Me._Credit = 0
    End Sub

    Public Sub New(ByVal TransactionDate As Date, ByVal JournalVoucherNo As String, ByVal DistribReference As String, _
            ByVal DocumentNo As String, ByVal Participant As AMParticipants, ByVal Debit As Decimal, ByVal Credit As Decimal)
        Me._TransactionDate = TransactionDate
        Me._JournalVoucherNo = JournalVoucherNo
        Me._DistributionRef = DistribReference
        Me._DocumentNo = DocumentNo
        Me._Participant = Participant
        Me._Debit = Debit
        Me._Credit = Credit
    End Sub

    Private _TransactionDate As Date
    Public Property TransactionDate() As Date
        Get
            Return _TransactionDate
        End Get
        Set(ByVal value As Date)
            _TransactionDate = value
        End Set
    End Property

    Private _JournalVoucherNo As String
    Public Property JournalVoucherNo() As String
        Get
            Return _JournalVoucherNo
        End Get
        Set(ByVal value As String)
            _JournalVoucherNo = value
        End Set
    End Property

    Private _DistributionRef As String
    Public Property DistributionRef() As String
        Get
            Return _DistributionRef
        End Get
        Set(ByVal value As String)
            _DistributionRef = value
        End Set
    End Property

    Private _DocumentNo As String
    Public Property DocumentNo() As String
        Get
            Return _DocumentNo
        End Get
        Set(ByVal value As String)
            _DocumentNo = value
        End Set
    End Property

    Private _Participant As AMParticipants
    Public Property Participant() As AMParticipants
        Get
            Return _Participant
        End Get
        Set(ByVal value As AMParticipants)
            _Participant = value
        End Set
    End Property


    Private _Debit As Decimal
    Public Property Debit() As Decimal
        Get
            Return _Debit
        End Get
        Set(ByVal value As Decimal)
            _Debit = value
        End Set
    End Property

    Private _Credit As Decimal
    Public Property Credit() As Decimal
        Get
            Return _Credit
        End Get
        Set(ByVal value As Decimal)
            _Credit = value
        End Set
    End Property





End Class
