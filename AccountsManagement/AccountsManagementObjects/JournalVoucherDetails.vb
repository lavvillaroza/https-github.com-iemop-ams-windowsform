'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             JournalVoucherDetails
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 29, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for Journal Voucher Details
'Arguments/Parameters:  
'Files/Database Tables:  AM_JV_DETAILS
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   September 29, 2011      Juan Carlo L. Panopio           Class initialization
'   October 03, 2011        Vladimir E. Espiritu            Revised the class initialization
'


Option Strict On
Option Explicit On

Public Class JournalVoucherDetails

#Region "Initialization/Constructor"

    Public Sub New()
        Me.New(0, "", 0, 0, "", Nothing)
    End Sub

    Public Sub New(ByVal jvnumber As Long, ByVal accountcode As String, ByVal debit As Decimal, ByVal credit As Decimal)
        Me.New(jvnumber, accountcode, debit, credit, "", Nothing)
    End Sub

    Public Sub New(ByVal jvnumber As Long, ByVal accountcode As String, ByVal debit As Decimal, ByVal credit As Decimal, _
                   ByVal updatedby As String, ByVal updateddate As Date)

        Me._JVNumber = jvnumber
        Me._AccountCode = accountcode
        Me._Credit = credit
        Me._Debit = debit
        Me._UpdatedBy = updatedby
        Me._UpdatedDate = updateddate
    End Sub

#End Region

#Region "AM_JV_NO"
    Private _JVNumber As Long
    Public Property JVNumber() As Long
        Get
            Return _JVNumber
        End Get
        Set(ByVal value As Long)
            _JVNumber = value
        End Set
    End Property
#End Region

#Region "ACCOUNT CODE"
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

#Region "DEBIT"
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

#Region "CREDIT"
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

#Region "UPDATED BY"
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

#Region "UPDATED DATE"
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
