'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             CheckVoucherMain
'Orginal Author:         Juan Carlo Panopio
'File Creation Date:     January 7, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Check Voucher Main
'Arguments/Parameters:  
'Files/Database Tables:  AM_CHECK_VOUCHER_MAIN
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description
'

Public Class CheckVoucherMain

    Public Sub New()
        Me._VoucherNumber = ""
        Me._Participant = New AMParticipants
        Me._PreparedBy = ""
        Me._PreparedPosition = ""
        Me._CheckedBy = ""
        Me._CheckedPosition = ""
        Me._ApprovedBy = ""
        Me._ApprovedPosition = ""
        Me._VoucherDetails = New List(Of CheckVoucherDetails)
        Me._TransactionDate = SystemDate
        Me._Particulars = ""
    End Sub

    Public Sub New(ByVal VoucherNo As String, ByVal Participant As AMParticipants, _
                   ByVal PreparedBy As String, ByVal PreparedPosition As String, ByVal CheckedBy As String, _
                   ByVal CheckedPosition As String, ByVal ApprovedBy As String, ByVal ApprovedPosition As String, _
                   ByVal VoucherDetails As List(Of CheckVoucherDetails), ByVal TransactionDate As Date, ByVal Particulars As String)
        Me._VoucherNumber = VoucherNo
        Me._Participant = Participant
        Me._PreparedBy = PreparedBy
        Me._PreparedPosition = PreparedPosition
        Me._CheckedBy = CheckedBy
        Me._CheckedPosition = CheckedPosition
        Me._ApprovedBy = ApprovedBy
        Me._ApprovedPosition = ApprovedPosition
        Me._VoucherDetails = VoucherDetails
        Me._TransactionDate = TransactionDate
        Me._Particulars = Particulars
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

    Private _VoucherNumber As String
    Public Property VoucherNumber() As String
        Get
            Return _VoucherNumber
        End Get
        Set(ByVal value As String)
            _VoucherNumber = value
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

    Private _PreparedBy As String
    Public Property PreparedBy() As String
        Get
            Return _PreparedBy
        End Get
        Set(ByVal value As String)
            _PreparedBy = value
        End Set
    End Property

    Private _PreparedPosition As String
    Public Property PreparedPosition() As String
        Get
            Return _PreparedPosition
        End Get
        Set(ByVal value As String)
            _PreparedPosition = value
        End Set
    End Property

    Private _CheckedBy As String
    Public Property CheckedBy() As String
        Get
            Return _CheckedBy
        End Get
        Set(ByVal value As String)
            _CheckedBy = value
        End Set
    End Property

    Private _CheckedPosition As String
    Public Property CheckedPosition() As String
        Get
            Return _CheckedPosition
        End Get
        Set(ByVal value As String)
            _CheckedPosition = value
        End Set
    End Property

    Private _ApprovedBy As String
    Public Property ApprovedBy() As String
        Get
            Return _ApprovedBy
        End Get
        Set(ByVal value As String)
            _ApprovedBy = value
        End Set
    End Property

    Private _ApprovedPosition As String
    Public Property ApprovedPosition() As String
        Get
            Return _ApprovedPosition
        End Get
        Set(ByVal value As String)
            _ApprovedPosition = value
        End Set
    End Property

    Private _VoucherDetails As List(Of CheckVoucherDetails)
    Public Property VoucherDetails() As List(Of CheckVoucherDetails)
        Get
            Return _VoucherDetails
        End Get
        Set(ByVal value As List(Of CheckVoucherDetails))
            _VoucherDetails = value
        End Set
    End Property

    Private _Particulars As String
    Public Property Particulars() As String
        Get
            Return _Particulars
        End Get
        Set(ByVal value As String)
            _Particulars = value
        End Set
    End Property


End Class
