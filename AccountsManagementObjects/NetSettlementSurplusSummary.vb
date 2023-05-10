'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             NetSettlementSurplusSummary
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     August 17, 2013
'Development Group:      Software Development and Support Division
'Description:            Class for Net Settlement Surplus Summary
'Arguments/Parameters:  
'Files/Database Tables:  NetSettlementSurplusSummary
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   August 17, 2012         Vladimir E. Espiritu            Class initialization

Public Class NetSettlementSurplusSummary


#Region "Initialization/Constructor"
    Public Sub New()
        Me._AllocationDate = Nothing
        Me._YearPeriod = 0
        Me._QuarterlyPeriod = 0
        Me._BatchCode = ""
        Me._IDNumber = New AMParticipants
        Me._TotalNSSInterest = 0
        Me._TotalNSSInterestNetWTax = 0
        Me._TotalSTLInterest = 0
        Me._TotalSTLInterestNetWTax = 0
        Me._Total = 0
    End Sub
#End Region



#Region "AllocationDate"
    Private _AllocationDate As Date
    Public Property AllocationDate() As Date
        Get
            Return _AllocationDate
        End Get
        Set(ByVal value As Date)
            _AllocationDate = value
        End Set
    End Property

#End Region

#Region "YearPeriod"
    Private _YearPeriod As Integer
    Public Property YearPeriod() As Integer
        Get
            Return _YearPeriod
        End Get
        Set(ByVal value As Integer)
            _YearPeriod = value
        End Set
    End Property

#End Region

#Region "QuaterlyPeriod"
    Private _QuarterlyPeriod As EnumQuarterlyPeriod
    Public Property QuarterlyPeriod() As EnumQuarterlyPeriod
        Get
            Return _QuarterlyPeriod
        End Get
        Set(ByVal value As EnumQuarterlyPeriod)
            _QuarterlyPeriod = value
        End Set
    End Property


#End Region

#Region "BatchCode"
    Private _BatchCode As String
    Public Property BatchCode() As String
        Get
            Return _BatchCode
        End Get
        Set(ByVal value As String)
            _BatchCode = value
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

#Region "TotalNSSInterest"
    Private _TotalNSSInterest As Decimal
    Public Property TotalNSSInterest() As Decimal
        Get
            Return _TotalNSSInterest
        End Get
        Set(ByVal value As Decimal)
            _TotalNSSInterest = value
        End Set
    End Property

#End Region

#Region "TotalNSSInterestNetWTax"
    Private _TotalNSSInterestNetWTax As Decimal
    Public Property TotalNSSInterestNetWTax() As Decimal
        Get
            Return _TotalNSSInterestNetWTax
        End Get
        Set(ByVal value As Decimal)
            _TotalNSSInterestNetWTax = value
        End Set
    End Property

#End Region

#Region "TotalSTLInterest"
    Private _TotalSTLInterest As Decimal
    Public Property TotalSTLInterest() As Decimal
        Get
            Return _TotalSTLInterest
        End Get
        Set(ByVal value As Decimal)
            _TotalSTLInterest = value
        End Set
    End Property

#End Region

#Region "TotalSTLInterestNetWTax"
    Private _TotalSTLInterestNetWTax As Decimal
    Public Property TotalSTLInterestNetWTax() As Decimal
        Get
            Return _TotalSTLInterestNetWTax
        End Get
        Set(ByVal value As Decimal)
            _TotalSTLInterestNetWTax = value
        End Set
    End Property

#End Region

#Region "Total"
    Private _Total As Decimal
    Public Property Total() As Decimal
        Get
            Return _Total
        End Get
        Set(ByVal value As Decimal)
            _Total = value
        End Set
    End Property

#End Region


End Class
