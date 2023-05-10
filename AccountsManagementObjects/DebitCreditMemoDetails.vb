'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             DebitCreditMemo
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 29, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for Debit Credit Memo Details
'Arguments/Parameters:  
'Files/Database Tables:  AM_DMCM_DETAILS        
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   September 29, 2011      Juan Carlo L. Panopio           Class initialization
'   October 03, 2011        Vladimir E. Espiritu            Revised the initialization
'   February 24, 2012       Vladimir E. Espiritu            Added InvDMCMNo and SummaryType properties
'   March 13, 2012          Vladimir E. Espiritu            Added IDNumber
'


Option Strict On
Option Explicit On



Public Class DebitCreditMemoDetails

#Region "Initialization/Constructor"

    Public Sub New()
        Me.New(0, "", 0, 0)
    End Sub

    Public Sub New(ByVal dmcmnumber As Long, ByVal accountCode As String, ByVal debit As Decimal, ByVal credit As Decimal)
        Me.New(dmcmnumber, accountCode, debit, credit, "", Nothing)
    End Sub

    Public Sub New(ByVal dmcmnumber As Long, ByVal accountCode As String, ByVal debit As Decimal, ByVal credit As Decimal, ByVal isComputed As EnumDMCMComputed)
        Me._DMCMNumber = dmcmnumber
        Me._AccountCode = accountCode
        Me._Debit = debit
        Me._Credit = credit
        Me._IsComputed = isComputed
    End Sub

    Public Sub New(ByVal dmcmnumber As Long, ByVal accountCode As String, ByVal debit As Decimal, ByVal credit As Decimal, _
                   ByVal updatedby As String, ByVal updateddate As Date)

        Me._DMCMNumber = dmcmnumber
        Me._AccountCode = accountCode
        Me._Credit = credit
        Me._Debit = debit
        Me._UpdatedBy = updatedby
        Me._UpdatedDate = updateddate
        Me._InvDMCMNo = "0"
        Me._SummaryType = Nothing
    End Sub

    Public Sub New(ByVal dmcmnumber As Long, ByVal accountCode As String, ByVal debit As Decimal, ByVal credit As Decimal, _
                   ByVal invdmcmno As String, ByVal summarytype As EnumSummaryType, ByVal idnumber As AMParticipants)
        Me._DMCMNumber = dmcmnumber
        Me._AccountCode = accountCode
        Me._Credit = credit
        Me._Debit = debit
        Me._InvDMCMNo = invdmcmno
        Me._SummaryType = summarytype
        Me._IDNumber = idnumber
    End Sub

    Public Sub New(ByVal dmcmnumber As Long, ByVal accountCode As String, ByVal debit As Decimal, ByVal credit As Decimal, _
               ByVal invdmcmno As String, ByVal summarytype As EnumSummaryType, ByVal idnumber As AMParticipants, ByVal isComputed As EnumDMCMComputed)
        Me._DMCMNumber = dmcmnumber
        Me._AccountCode = accountCode
        Me._Credit = credit
        Me._Debit = debit
        Me._InvDMCMNo = invdmcmno
        Me._SummaryType = summarytype
        Me._IDNumber = idnumber
        Me._IsComputed = isComputed
    End Sub
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

#Region "InvDMCMNo"
    Private _InvDMCMNo As String
    Public Property InvDMCMNo() As String
        Get
            Return _InvDMCMNo
        End Get
        Set(ByVal value As String)
            _InvDMCMNo = value
        End Set
    End Property
#End Region

#Region "SummaryType"
    Private _SummaryType As EnumSummaryType
    Public Property SummaryType() As EnumSummaryType
        Get
            Return _SummaryType
        End Get
        Set(ByVal value As EnumSummaryType)
            _SummaryType = value
        End Set
    End Property

#End Region

#Region "IDNumber"
    Private _IDNumber As AMParticipants
    Public Property IDNumber() As AMParticipants
        Get
            Return _IDNumber
        End Get
        Set(ByVal value As AMParticipants)
            _IDNumber = value
        End Set
    End Property

#End Region

#Region "IsComputed"
    Private _IsComputed As EnumDMCMComputed
    Public Property IsComputed() As EnumDMCMComputed
        Get
            Return _IsComputed
        End Get
        Set(ByVal value As EnumDMCMComputed)
            _IsComputed = value
        End Set
    End Property

#End Region

End Class
