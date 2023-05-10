'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             BankReconMain.vb
'Orginal Author:         Juan Carlo L. Panopio  
'File Creation Date:     February 15, 2013
'Development Group:      Software Development and Support Division
'Description:            Class for Bank Recon Statement Main
'Arguments/Parameters:  
'Files/Database Tables:  AM_BANK_RECON_MAIN
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************


Public Class BankReconMain

    Public Sub New()
        Me._BatchCode = ""
        Me._TransactionDate = Nothing
        Me._BankReconType = Nothing
        Me._BeginningBalance = 0
        Me._UpdatedBy = ""
        Me._UpdatedDate = Nothing
        Me._lstBankReconDetails = New List(Of BankReconDetails)
        Me._BankEndingbalance = 0
        Me._Status = EnumStatus.InActive
        Me._BankCharges = 0
        Me._BookEndingBalance = 0
    End Sub

    Private Sub New(ByVal BatchCode As String, ByVal TransactionDate As Date, ByVal BankReconType As EnumBankReconType, _
                    ByVal BeginningBalance As Decimal, ByVal UpdatedBy As String, ByVal UpdatedDate As Date, ByVal lstBankReconDetails As List(Of BankReconDetails), _
                    ByVal BankEndingBalance As Decimal, ByVal Status As EnumStatus, ByVal BankCharges As Decimal, ByVal BookEndingBalance As Decimal)


        Me._BankReconType = BankReconType
        Me._BatchCode = BatchCode
        Me._TransactionDate = TransactionDate
        Me._BeginningBalance = BeginningBalance
        Me._UpdatedBy = UpdatedBy
        Me._UpdatedDate = UpdatedDate
        Me._lstBankReconDetails = lstBankReconDetails
        Me._BankEndingbalance = BankEndingBalance
        Me._Status = Status
        Me._BankCharges = BankCharges
        Me._BookEndingBalance = BookEndingBalance
    End Sub

    Private _BatchCode As String
    Public Property BatchCode() As String
        Get
            Return _BatchCode
        End Get
        Set(ByVal value As String)
            _BatchCode = value
        End Set
    End Property

    Private _TransactionDate As Date
    Public Property TransactionDate() As Date
        Get
            Return _TransactionDate
        End Get
        Set(ByVal value As Date)
            _TransactionDate = value
        End Set
    End Property

    Private _BankReconType As EnumBankReconType
    Public Property BankReconType() As EnumBankReconType
        Get
            Return _BankReconType
        End Get
        Set(ByVal value As EnumBankReconType)
            _BankReconType = value
        End Set
    End Property

    Private _BeginningBalance As Decimal
    Public Property BeginningBalance() As Decimal
        Get
            Return _BeginningBalance
        End Get
        Set(ByVal value As Decimal)
            _BeginningBalance = value
        End Set
    End Property

    Private _UpdatedBy As String
    Public Property UpdatedBy() As String
        Get
            Return _UpdatedBy
        End Get
        Set(ByVal value As String)
            _UpdatedBy = value
        End Set
    End Property

    Private _UpdatedDate As Date
    Public Property UpdatedDate() As Date
        Get
            Return _UpdatedDate
        End Get
        Set(ByVal value As Date)
            _UpdatedDate = value
        End Set
    End Property

    Private _lstBankReconDetails As List(Of BankReconDetails)
    Public Property lstBankReconDetails() As List(Of BankReconDetails)
        Get
            Return _lstBankReconDetails
        End Get
        Set(ByVal value As List(Of BankReconDetails))
            _lstBankReconDetails = value
        End Set
    End Property

    Private _BankEndingbalance As Decimal
    Public Property BankEndingBalance() As Decimal
        Get
            Return _BankEndingbalance
        End Get
        Set(ByVal value As Decimal)
            _BankEndingbalance = value
        End Set
    End Property

    Private _Status As EnumStatus
    Public Property Status() As EnumStatus
        Get
            Return _Status
        End Get
        Set(ByVal value As EnumStatus)
            _Status = value
        End Set
    End Property

    Private _BankCharges As Decimal
    Public Property BankCharges() As Decimal
        Get
            Return _BankCharges
        End Get
        Set(ByVal value As Decimal)
            _BankCharges = value
        End Set
    End Property

    Private _BookEndingBalance As Decimal
    Public Property BookEndingbalance() As Decimal
        Get
            Return _BookEndingBalance
        End Get
        Set(ByVal value As Decimal)
            _BookEndingBalance = value
        End Set
    End Property


End Class
