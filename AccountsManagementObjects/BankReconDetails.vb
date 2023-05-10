'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             BankReconDetails.vb
'Orginal Author:         Juan Carlo L. Panopio  
'File Creation Date:     February 15, 2013
'Development Group:      Software Development and Support Division
'Description:            Class for Bank Recon Statement Details
'Arguments/Parameters:  
'Files/Database Tables:  AM_BANK_RECON_DETAILS
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************

Public Class BankReconDetails

    Public Sub New()
        Me._BatchCode = ""
        Me._TransactionType = Nothing
        Me._Amount = 0
        Me._UpdatedBy = ""
        Me._UpdatedDate = Nothing
    End Sub

    Public Sub New(ByVal BatchCode As String, ByVal TransactionType As EnumBankReconTransactionType, ByVal Amount As Decimal, _
                   ByVal UpdatedBy As String, ByVal UpdatedDate As Date)

        Me._BatchCode = BatchCode
        Me._TransactionType = TransactionType
        Me._Amount = Amount
        Me._UpdatedDate = UpdatedDate
        Me._UpdatedBy = UpdatedBy
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

    Private _TransactionType As EnumBankReconTransactionType
    Public Property TransactionType() As EnumBankReconTransactionType
        Get
            Return _TransactionType
        End Get
        Set(ByVal value As EnumBankReconTransactionType)
            _TransactionType = value
        End Set
    End Property

    Private _Amount As Decimal
    Public Property Amount() As Decimal
        Get
            Return _Amount
        End Get
        Set(ByVal value As Decimal)
            _Amount = value
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

End Class
