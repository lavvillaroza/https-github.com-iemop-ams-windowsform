'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             Check
'Orginal Author:         Juan Carlo Panopio
'File Creation Date:     January 7, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Checks
'Arguments/Parameters:  
'Files/Database Tables:  AM_CHECKS, AM_CHECK_VOUCHER_MAIN, AM_CHECK_VOUCHER_DETAILS
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description
'   

Public Class Check

    Public Sub New()
        Me._Participant = New AMParticipants
        Me._TransactionDate = SystemDate
        Me._Amount = 0
        Me._CheckNumber = ""
        Me._CheckType = Nothing
        Me._VoucherNumber = ""
        Me._Status = Nothing
        Me._CheckVoucher = New CheckVoucherMain
        Me._Remarks = ""
        Me._BatchCode = ""
        Me._DateReleased = ""
        Me._DateCleared = ""
    End Sub

    Public Sub New(ByVal Participant As AMParticipants, ByVal TransactionDate As Date, ByVal Amount As Decimal, _
                   ByVal CheckNo As String, ByVal CheckType As EnumCheckType, ByVal VoucherNo As String, ByVal Status As EnumCheckStatus, _
                   ByVal CheckVoucher As CheckVoucherMain, ByVal Remarks As String, ByVal BatchCode As String, ByVal DateReleased As String, _
                   ByVal DateCleared As String)

        Me._Participant = Participant
        Me._TransactionDate = TransactionDate
        Me._Amount = Amount
        Me._CheckNumber = CheckNo
        Me._CheckType = CheckType
        Me._VoucherNumber = VoucherNo
        Me._Status = Status
        Me._CheckVoucher = CheckVoucher
        Me._Remarks = Remarks
        Me._BatchCode = BatchCode
        Me._DateReleased = DateReleased
    End Sub

    Private _Participant As AMParticipants
    Public Property Participant() As AMParticipants
        Get
            Return _Participant
        End Get
        Set(ByVal value As AMParticipants)
            _Participant = value
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

    Private _Amount As Decimal
    Public Property Amount() As Decimal
        Get
            Return _Amount
        End Get
        Set(ByVal value As Decimal)
            _Amount = value
        End Set
    End Property

    Private _CheckNumber As String
    Public Property CheckNumber() As String
        Get
            Return _CheckNumber
        End Get
        Set(ByVal value As String)
            _CheckNumber = value
        End Set
    End Property

    Private _CheckType As EnumCheckType
    Public Property CheckType() As EnumCheckType
        Get
            Return _CheckType
        End Get
        Set(ByVal value As EnumCheckType)
            _CheckType = value
        End Set
    End Property

    Private _VoucherNumber As String
    Public Property VoucherNumber() As String
        Get
            Return _VoucherNumber
        End Get
        Set(ByVal value As String)
            _VoucherNumber = value
        End Set
    End Property

    Private _Status As EnumCheckStatus
    Public Property Status() As EnumCheckStatus
        Get
            Return _Status
        End Get
        Set(ByVal value As EnumCheckStatus)
            _Status = value
        End Set
    End Property

    Private _CheckVoucher As CheckVoucherMain
    Public Property CheckVoucher() As CheckVoucherMain
        Get
            Return _CheckVoucher
        End Get
        Set(ByVal value As CheckVoucherMain)
            _CheckVoucher = value
        End Set
    End Property

    Private _Remarks As String
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property

    Private _BatchCode As String
    Public Property BatchCode() As String
        Get
            Return _BatchCode
        End Get
        Set(ByVal value As String)
            _BatchCode = value
        End Set
    End Property

    Private _DateReleased As String
    Public Property DateReleased() As String
        Get
            Return _DateReleased
        End Get
        Set(ByVal value As String)
            _DateReleased = value
        End Set
    End Property

    Private _DateCleared As String
    Public Property DateCleared() As String
        Get
            Return _DateCleared
        End Get
        Set(ByVal value As String)
            _DateCleared = value
        End Set
    End Property


End Class
