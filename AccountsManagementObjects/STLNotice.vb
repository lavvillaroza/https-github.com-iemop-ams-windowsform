'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             STLNotice
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     November 14, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Header of STL Notice
'Arguments/Parameters:  
'Files/Database Tables:  AM_STL_NOTICE_MAIN
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description

Public Class STLNotice

    Public Sub New()
        Me._TransactionDate = SystemDate
        Me._TotalBeginningBalance = 0
        Me._UpdatedBy = ""
        Me._UpdatedDate = SystemDate
        Me._BalanceDetails = New List(Of STLBeginningBalance)
        Me._STLNoticeDetails = New List(Of STLNoticeDetails)
    End Sub

    Public Sub New(ByVal TransactionDate As Date, ByVal BalanceDetails As List(Of STLBeginningBalance), ByVal STLDetails As List(Of STLNoticeDetails), ByVal Participant As AMParticipants, _
                   ByVal TotalBeginning As Decimal)
        Me._TransactionDate = TransactionDate
        Me._BalanceDetails = BalanceDetails
        Me._STLNoticeDetails = STLDetails
        Me._Participant = Participant
        Me._TotalBeginningBalance = TotalBeginning
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

    Private _TotalBeginningBalance As Decimal
    Public Property TotalBeginningBalance() As Decimal
        Get
            Return _TotalBeginningBalance
        End Get
        Set(ByVal value As Decimal)
            _TotalBeginningBalance = value
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

    Private _BalanceDetails As List(Of STLBeginningBalance)
    Public Property BalanceDetails() As List(Of STLBeginningBalance)
        Get
            Return _BalanceDetails
        End Get
        Set(ByVal value As List(Of STLBeginningBalance))
            _BalanceDetails = value
        End Set
    End Property

    Private _STLNoticeDetails As List(Of STLNoticeDetails)
    Public Property STLNoticeDetails() As List(Of STLNoticeDetails)
        Get
            Return _STLNoticeDetails
        End Get
        Set(ByVal value As List(Of STLNoticeDetails))
            _STLNoticeDetails = value
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

End Class
