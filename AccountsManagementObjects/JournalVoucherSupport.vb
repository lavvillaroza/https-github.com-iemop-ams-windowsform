'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             Journal Voucher Support
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     March 22, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Report for Journal Voucher Details
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   March 22, 2012        Juan Carlo L. Panopio         Class initialization

Option Explicit On
Option Strict On
Public Class JournalVoucherSupport

#Region "Initialization/Constructor"

    Public Sub New()
        Me.New("", "", "", Nothing, 0, Nothing, 0, 0)
    End Sub

    Public Sub New(ByVal IDNumber As String, ByVal ParticipantName As String, ByVal InvDMCMNo As String, ByVal InvDate As Date, ByVal DMCMNo As Long, _
                   ByVal DueDate As Date, ByVal APAmount As Decimal, ByVal ARAmount As Decimal)
        Me._IDNumber = IDNumber
        Me._ParticipantName = ParticipantName
        Me._InvDMCMNo = InvDMCMNo
        Me._DMCMNo = DMCMNo
        Me._InvoiceDate = InvDate
        Me._DueDate = DueDate
        Me._APAmount = APAmount
        Me._ARAmount = ARAmount
    End Sub
#End Region


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


    Private _InvDMCMNo As String
    Public Property InvDMCMNo() As String
        Get
            Return _InvDMCMNo
        End Get
        Set(ByVal value As String)
            _InvDMCMNo = value
        End Set
    End Property


    Private _InvoiceDate As Date
    Public Property InvoiceDate() As Date
        Get
            Return _InvoiceDate
        End Get
        Set(ByVal value As Date)
            _InvoiceDate = value
        End Set
    End Property

    Private _DMCMNo As Long
    Public Property DMCMNo() As Long
        Get
            Return _DMCMNo
        End Get
        Set(ByVal value As Long)
            _DMCMNo = value
        End Set
    End Property


    Private _DueDate As Date
    Public Property DueDate() As Date
        Get
            Return _DueDate
        End Get
        Set(ByVal value As Date)
            _DueDate = value
        End Set
    End Property


    Private _ARAmount As Decimal
    Public Property ARAmount() As Decimal
        Get
            Return _ARAmount
        End Get
        Set(ByVal value As Decimal)
            _ARAmount = CDec(value.ToString("#,##0.00"))
        End Set
    End Property


    Private _APAmount As Decimal
    Public Property APAmount() As Decimal
        Get
            Return _APAmount
        End Get
        Set(ByVal value As Decimal)
            _APAmount = CDec(value.ToString("#,##0.00"))
        End Set
    End Property

End Class
