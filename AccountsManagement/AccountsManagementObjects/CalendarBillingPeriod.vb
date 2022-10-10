'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             CalendarBillingPeriod
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     August 22, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for Billing Period
'Arguments/Parameters:  
'Files/Database Tables:  CALENDAR_BP
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   August 22, 2011         Vladimir E. Espiritu            Class initialization
'	

Option Explicit On
Option Strict On

Public Class CalendarBillingPeriod

#Region "Initialization/Constructor"
    Public Sub New()
        Me.New(0, Nothing, Nothing, Nothing)
    End Sub
    Public Sub New(ByVal billperiod As Integer, ByVal sdate As Date, _
                   ByVal edate As Date, ByVal billdate As Date)

        Me._BillingPeriod = billperiod
        Me._BillingDate = billdate
        Me._EndDate = edate
        Me._StartDate = sdate
    End Sub

    Public Sub New(ByVal billperiod As Integer)
        Me._BillingPeriod = billperiod
    End Sub

#End Region


#Region "BillingPeriod"
    Private _BillingPeriod As Integer
    Public ReadOnly Property BillingPeriod() As Integer
        Get
            Return _BillingPeriod
        End Get
    End Property
#End Region

#Region "StartDate"
    Private _StartDate As Date
    ' <summary>
    ' gets the start date
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public ReadOnly Property StartDate() As Date
        Get
            Return _StartDate
        End Get
    End Property
#End Region

#Region "EndDate"
    Private _EndDate As Date
    ' <summary>
    ' gets the end date
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public ReadOnly Property EndDate() As Date
        Get
            Return _EndDate
        End Get
    End Property
#End Region

#Region "BillingDate"
    Private _BillingDate As Date
    ' <summary>
    ' gets the billing date
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public ReadOnly Property BillingDate() As Date
        Get
            Return _BillingDate
        End Get
    End Property
#End Region

End Class
