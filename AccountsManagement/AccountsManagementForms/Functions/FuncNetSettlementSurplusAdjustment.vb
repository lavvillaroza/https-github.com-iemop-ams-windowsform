'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             FuncNetSettlementSurplusAdjustment
'Orginal Author:         Vladimir E.Espiritu
'File Creation Date:     February 19, 2013
'Development Group:      Software Development and Support Division
'Description:            Class for re-usable functions
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description
'   February 19, 2013       Vladimir E.Espiritu         Class initialization
'

Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementLogic

Public Class FuncNetSettlementSurplusAdjustment

#Region "Initialization/Constructor"
    Public Sub New()
        Me._ListOfBillingPeriod = New List(Of CalendarBillingPeriod)
        Me._ListOfParticipants = New List(Of AMParticipants)
        Me._ListOfNetSettlementSurplusDetails = New List(Of NetSettlementSurplusDetails)
        Me._GenerateDatatable = New DataTable()
    End Sub
#End Region

#Region "ListOfBillingPeriod"
    Private _ListOfBillingPeriod As List(Of CalendarBillingPeriod)
    Public Property ListOfBillingPeriod() As List(Of CalendarBillingPeriod)
        Get
            Return _ListOfBillingPeriod
        End Get
        Set(ByVal value As List(Of CalendarBillingPeriod))
            _ListOfBillingPeriod = value
        End Set
    End Property

#End Region

#Region "ListOfParticipants"
    Private _ListOfParticipants As List(Of AMParticipants)
    Public Property ListOfParticipants() As List(Of AMParticipants)
        Get
            Return _ListOfParticipants
        End Get
        Set(ByVal value As List(Of AMParticipants))
            _ListOfParticipants = value
        End Set
    End Property

#End Region

#Region "ListOfNetSettlementSurplusDetails"
    Private _ListOfNetSettlementSurplusDetails As List(Of NetSettlementSurplusDetails)
    Public Property ListOfNetSettlementSurplusDetails() As List(Of NetSettlementSurplusDetails)
        Get
            Return _ListOfNetSettlementSurplusDetails
        End Get
        Set(ByVal value As List(Of NetSettlementSurplusDetails))
            _ListOfNetSettlementSurplusDetails = value
        End Set
    End Property

#End Region

#Region "GenerateDatatable"
    Private _GenerateDatatable As DataTable
    Public ReadOnly Property GenerateDatatable() As DataTable
        Get
            Dim dt As New DataTable
            Dim row As DataRow

            dt.TableName = "NSSRATable"
            With dt.Columns
                .Add("IDNumber", GetType(String))
                .Add("ParticipantID", GetType(String))

                For Each item In Me.ListOfBillingPeriod
                    .Add(item.BillingPeriod.ToString() & vbCrLf & item.StartDate.ToString("MM/dd/yyyy") & _
                         " to " & item.EndDate.ToString("MM/dd/yyyy"), GetType(String))
                Next
            End With
            dt.AcceptChanges()

            For Each item In Me.ListOfParticipants
                Dim selectedParticipant = item

                row = dt.NewRow()
                row("IDNUMBER") = item.IDNumber.ToString()
                row("ParticipantID") = item.ParticipantID

                For Each itemBP In Me.ListOfBillingPeriod
                    Dim selectedBP = itemBP
                    Dim header = itemBP.BillingPeriod.ToString() & vbCrLf & itemBP.StartDate.ToString("MM/dd/yyyy") & _
                                 " to " & itemBP.EndDate.ToString("MM/dd/yyyy")

                    Dim itemNSSRAAmount = (From x In Me.ListOfNetSettlementSurplusDetails _
                                           Where x.IDNumber.IDNumber = selectedParticipant.IDNumber _
                                           And x.BillingPeriod.BillingPeriod = selectedBP.BillingPeriod _
                                           Select x.NSSRAAmount).Sum()

                    row(header) = FormatNumber(itemNSSRAAmount, 2)
                Next
                dt.Rows.Add(row)

            Next
            dt.AcceptChanges()

            'For blank rows
            For index As Integer = 1 To 2
                row = dt.NewRow()
                dt.Rows.Add(row)
            Next
            dt.AcceptChanges()

            'Get the Total
            row = dt.NewRow()
            row("ParticipantID") = "TOTAL"
            For Each itemBP In Me.ListOfBillingPeriod
                Dim selectedBP = itemBP

                Dim header = itemBP.BillingPeriod.ToString() & vbCrLf & itemBP.StartDate.ToString("MM/dd/yyyy") & _
                             " to " & itemBP.EndDate.ToString("MM/dd/yyyy")

                Dim itemNSSRAAmount = (From x In Me.ListOfNetSettlementSurplusDetails _
                                       Where x.BillingPeriod.BillingPeriod = selectedBP.BillingPeriod _
                                       Select x.NSSRAAmount).Sum()

                row(header) = FormatNumber(itemNSSRAAmount, 2)
            Next
            dt.Rows.Add(row)
            dt.AcceptChanges()

            Return dt
        End Get
    End Property

#End Region

End Class
