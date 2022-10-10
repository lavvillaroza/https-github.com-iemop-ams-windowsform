'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             OffsetP2PC2CDetails
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 19, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for parent to parent/child to child and parent to child offsetting
'Arguments/Parameters:  
'Files/Database Tables:  AM_P2PC2C_OFFSET_DETAILS
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   September 05, 2011      Juan Carlo L. Panopio           Class initialization
'   September 26, 2011      Vladimir E. Espiritu            Revised the properties
'   September 30, 2011      Vladimir E. Espiritu            Added DebitCreditNumber property
'   November 15, 2011       Vladimir E. Espiritu            Added GroupNo
'


Option Explicit On
Option Strict On

<Serializable()> _
Public Class OffsetP2PC2CDetails

#Region "Initialization/Constructor"
    Public Sub New()
        Me._TransDate = Nothing
        Me._OffsetNumber = ""
        Me._AMCode = ""
        Me._InvoiceNumber = ""
        Me._Debit = 0
        Me._Credit = 0
        Me._DMCMNumber = 0
        Me._UpdatedBy = ""
        Me._UpdatedDate = Nothing
        Me._WESMBillSummaryNo = 0
    End Sub


    Public Sub New(ByVal TransDate As Date, ByVal OffsetNumber As String, ByVal WBNo As Integer, ByVal DMCMNo As Long, _
                 ByVal AccountCode As String, ByVal Debit As Decimal, ByVal Credit As Decimal)
        Me._TransDate = TransDate
        Me._OffsetNumber = OffsetNumber
        Me._WESMBillSummaryNo = WBNo
        Me._DMCMNumber = DMCMNo
        Me._AccountCode = AccountCode
        Me._Debit = Debit
        Me._Credit = Credit
    End Sub

    Public Sub New(ByVal offsetnumber As String, ByVal amcode As String, ByVal invoiceno As String, _
                    ByVal dmcmno As Long, ByVal WBNo As Long)
        Me._OffsetNumber = offsetnumber
        Me._TransDate = TransDate
        Me._AMCode = amcode
        Me._InvoiceNumber = invoiceno
        Me._DMCMNumber = dmcmno
        Me._WESMBillSummaryNo = WBNo
    End Sub
#End Region

#Region "Offset number"
    Private _OffsetNumber As String
    Public Property OffsetNumber() As String
        Get
            Return _OffsetNumber
        End Get
        Set(ByVal value As String)
            _OffsetNumber = value
        End Set
    End Property

#End Region

#Region "DMCM Number"
    Private _DMCMNumber As Long
    Public Property DMCMNumber() As Long
        Get
            Return _DMCMNumber
        End Get
        Set(ByVal value As Long)
            _DMCMNumber = value
        End Set
    End Property
#End Region

#Region "Transaction Date"
    Private _TransDate As Date

    Public Property TransDate() As Date
        Get
            Return _TransDate
        End Get
        Set(ByVal value As Date)
            _TransDate = value
        End Set
    End Property
#End Region

#Region "AM Code"
    Private _AMCode As String
    Public Property AMCode() As String
        Get
            Return _AMCode
        End Get
        Set(ByVal value As String)
            _AMCode = value
        End Set
    End Property
#End Region

#Region "Invoice Number"
    Private _InvoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return _InvoiceNumber
        End Get
        Set(ByVal value As String)
            _InvoiceNumber = value
        End Set
    End Property
#End Region

#Region "Debit"
    Private _Debit As Decimal

    Public Property Debit() As Decimal
        Get
            Return _Debit
        End Get
        Set(ByVal value As Decimal)
            _Debit = value
        End Set
    End Property
#End Region

#Region "Credit"
    Private _Credit As Decimal

    Public Property Credit() As Decimal
        Get
            Return _Credit
        End Get
        Set(ByVal value As Decimal)
            _Credit = value
        End Set
    End Property
#End Region

#Region "AccountCode"
    Private _AccountCode As String
    Public Property AccountCode() As String
        Get
            Return _AccountCode
        End Get
        Set(ByVal value As String)
            _AccountCode = value
        End Set
    End Property

#End Region

#Region "WESMBillSummaryNo"
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

#Region "Updated By"
    Private _UpdatedBy As String
    Public Property UpdatedBy() As String
        Get
            Return _UpdatedBy
        End Get
        Set(ByVal value As String)
            _UpdatedBy = value
        End Set
    End Property
#End Region

#Region "Updated Date"
    Private _UpdatedDate As Date
    Public Property UpdatedDate() As Date
        Get
            Return _UpdatedDate
        End Get
        Set(ByVal value As Date)
            _UpdatedDate = value
        End Set
    End Property
#End Region

End Class
