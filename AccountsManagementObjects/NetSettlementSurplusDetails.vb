'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             NetSettlementSurplusDetails
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     February 10, 2013
'Development Group:      Software Development and Support Division
'Description:            Class for Net Settlement Surplus (NSS)
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   February 10, 2013       Vladimir E. Espiritu            Class initialization


Public Class NetSettlementSurplusDetails

#Region "Initialization/Constructor"
    Public Sub New()
        Me._IDNumber = New AMParticipants()
        Me._BillingPeriod = New CalendarBillingPeriod
        Me._NSSAmount = 0
        Me._NSSRAAmount = 0
    End Sub
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

#Region "BillingPeriod"
    Private _BillingPeriod As CalendarBillingPeriod
    Public Property BillingPeriod() As CalendarBillingPeriod
        Get
            Return _BillingPeriod
        End Get
        Set(ByVal value As CalendarBillingPeriod)
            _BillingPeriod = value
        End Set
    End Property

#End Region

#Region "NSSAmount"
    Private _NSSAmount As Decimal
    Public Property NSSAmount() As Decimal
        Get
            Return _NSSAmount
        End Get
        Set(ByVal value As Decimal)
            _NSSAmount = value
        End Set
    End Property

#End Region

#Region "NSSRAAmount"
    Private _NSSRAAmount As Decimal
    Public ReadOnly Property NSSRAAmount() As Decimal
        Get
            _NSSRAAmount = Math.Round(Me.NSSAmount * 0.1D, 2)
            Return _NSSRAAmount
        End Get
    End Property

#End Region

End Class
