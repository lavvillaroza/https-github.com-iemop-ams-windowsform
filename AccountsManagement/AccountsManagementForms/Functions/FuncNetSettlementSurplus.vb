'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             FuncNetSettlementSurplus
'Orginal Author:         Vladimir E.Espiritu
'File Creation Date:     February 12, 2013
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
'   February 12, 2013       Vladimir E.Espiritu         Class initialization
'

Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementLogic

Public Class FuncNetSettlementSurplus

#Region "Initialization/Constructor"
    Public Sub New()
        Me._ListOfBillingPeriod = New List(Of CalendarBillingPeriod)
        Me._ListOfParticipants = New List(Of AMParticipants)
        Me._ListOfPreviousNetSettlementSurplusMain = New List(Of NetSettlementSurplusMain)
        Me._ListOfCurrentNetSettlmentSurplusMain = New List(Of NetSettlementSurplusMain)
        Me._ListOfNetSettlementSurplusMain = New List(Of NetSettlementSurplusMain)
        Me._ListOfFinalNetSettlementSurplusMain = New List(Of NetSettlementSurplusMain)
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

#Region "ListOfPreviousNetSettlementSurplusMain"
    Private _ListOfPreviousNetSettlementSurplusMain As List(Of NetSettlementSurplusMain)
    Public Property ListOfPreviousNetSettlementSurplusMain() As List(Of NetSettlementSurplusMain)
        Get
            Return _ListOfPreviousNetSettlementSurplusMain
        End Get
        Set(ByVal value As List(Of NetSettlementSurplusMain))
            _ListOfPreviousNetSettlementSurplusMain = value
        End Set
    End Property

#End Region

#Region "ListOfCurrentNetSettlmentSurplusMain"
    Private _ListOfCurrentNetSettlmentSurplusMain As List(Of NetSettlementSurplusMain)
    Public Property ListOfCurrentNetSettlmentSurplusMain() As List(Of NetSettlementSurplusMain)
        Get
            Return _ListOfCurrentNetSettlmentSurplusMain
        End Get
        Set(ByVal value As List(Of NetSettlementSurplusMain))
            _ListOfCurrentNetSettlmentSurplusMain = value
        End Set
    End Property

#End Region

#Region "ListOfNetSettlementSurplusMain"
    Private _ListOfNetSettlementSurplusMain As List(Of NetSettlementSurplusMain)
    Public ReadOnly Property ListOfNetSettlementSurplusMain() As List(Of NetSettlementSurplusMain)
        Get
            _ListOfNetSettlementSurplusMain = New List(Of NetSettlementSurplusMain)
            _ListOfNetSettlementSurplusMain.AddRange(Me.ListOfPreviousNetSettlementSurplusMain)
            _ListOfNetSettlementSurplusMain.AddRange(Me.ListOfCurrentNetSettlmentSurplusMain)

            Return _ListOfNetSettlementSurplusMain
        End Get
    End Property

#End Region

#Region "ListOfFinalNetSettlementSurplusMain"
    Private _ListOfFinalNetSettlementSurplusMain As List(Of NetSettlementSurplusMain)
    Public ReadOnly Property ListOfFinalNetSettlementSurplusMain() As List(Of NetSettlementSurplusMain)
        Get
            'Compute the Total NSS Retention Balance for the previous 3 months
            For Each item In Me.ListOfCurrentNetSettlmentSurplusMain
                Dim selectedItem = item

                item.NetSettlementSurplusAdjustment = (From x In Me.ListOfNetSettlementSurplusMain _
                                                       Where x.BillingPeriod.BillingPeriod = selectedItem.BillingPeriod.BillingPeriod - 1 _
                                                       Select x.NetSettlementSurplus * 0.1D).Sum()

                item.NSSRetentionBalance = (From x In Me.ListOfNetSettlementSurplusMain _
                                            Where x.BillingPeriod.BillingPeriod >= selectedItem.BillingPeriod.BillingPeriod - 2 _
                                            And x.BillingPeriod.BillingPeriod <= selectedItem.BillingPeriod.BillingPeriod _
                                            Select x.NetSettlementSurplusAdjustment).Sum()

                item.ReturnOfInterest = (From x In Me.ListOfNetSettlementSurplusMain _
                                         Where x.BillingPeriod.BillingPeriod >= selectedItem.BillingPeriod.BillingPeriod - 3 _
                                         And x.BillingPeriod.BillingPeriod < selectedItem.BillingPeriod.BillingPeriod _
                                         Select x.Interest).Sum()

                item.ReturnOfInterest += (From x In Me.ListOfNetSettlementSurplusMain _
                                          Where x.BillingPeriod.BillingPeriod = selectedItem.BillingPeriod.BillingPeriod - 3 _
                                          Select x.InterestOnInterest).Sum()

                item.NSSRABankStatement = (From x In Me.ListOfNetSettlementSurplusMain _
                                           Where x.BillingPeriod.BillingPeriod = selectedItem.BillingPeriod.BillingPeriod - 3 _
                                           Select x.NetSettlementSurplusAdjustment).Sum()

                item.NSSRABankStatement += Math.Abs(item.NetSettlementSurplusAdjustment) - Math.Abs(item.NSSRABankStatement) _
                                         - item.InterestOnInterest
            Next

            _ListOfFinalNetSettlementSurplusMain.AddRange(Me.ListOfCurrentNetSettlmentSurplusMain)
            _ListOfFinalNetSettlementSurplusMain.TrimExcess()

            Return _ListOfFinalNetSettlementSurplusMain
        End Get
    End Property

#End Region


#Region "GenerateDatatable"
    Public Function GenerateDatatable() As DataTable
        Dim dt As New DataTable

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

        Dim listCurrent = Me.ListOfFinalNetSettlementSurplusMain
        For Each item In Me.ListOfParticipants
            Dim selectedParticipant = item
            Dim row = dt.NewRow()

            row("IDNUMBER") = item.IDNumber.ToString()
            row("ParticipantID") = item.ParticipantID

            For Each itemBP In Me.ListOfBillingPeriod
                Dim selectedBP = itemBP
                Dim header = itemBP.BillingPeriod.ToString() & vbCrLf & itemBP.StartDate.ToString("MM/dd/yyyy") & _
                             " to " & itemBP.EndDate.ToString("MM/dd/yyyy")

                Dim cnt = (From x In listCurrent _
                           Where x.BillingPeriod.BillingPeriod = selectedBP.BillingPeriod - 1 _
                           Select x).Count()

                If cnt > 0 Then
                    Dim itemMain = (From x In listCurrent _
                                    Where x.BillingPeriod.BillingPeriod = selectedBP.BillingPeriod - 1 _
                                    Select x).First()

                    Dim itemNSSRAAmount = (From x In itemMain.ListOfNetSettlementSurplusDetails _
                                           Where x.IDNumber.IDNumber = selectedParticipant.IDNumber _
                                           Select x.NSSRAAmount).Sum()
                    row(header) = FormatNumber(itemNSSRAAmount, 2)
                Else
                    row(header) = "0.00"
                End If
            Next
            dt.Rows.Add(row)
        Next

        'For Summary
        For index As Integer = 1 To 8
            Dim row = dt.NewRow()

            For Each itemBP In Me.ListOfBillingPeriod
                Dim selectedBP = itemBP
                Dim header = itemBP.BillingPeriod.ToString() & vbCrLf & itemBP.StartDate.ToString("MM/dd/yyyy") & _
                             " to " & itemBP.EndDate.ToString("MM/dd/yyyy")

                Dim itemMain = (From x In listCurrent _
                                Where x.BillingPeriod.BillingPeriod = selectedBP.BillingPeriod _
                                Select x).First()

                Select Case index
                    Case 4
                        row("IDNUMBER") = "NSS Retention Balance"
                        row(header) = FormatNumber(itemMain.NSSRetentionBalance, 2)
                    Case 5
                        row("IDNUMBER") = "Interest"
                        row(header) = FormatNumber(itemMain.Interest, 2)
                    Case 6
                        row("IDNUMBER") = "Return of Interest"
                        row(header) = FormatNumber(itemMain.ReturnOfInterest, 2)
                    Case 7
                        row("IDNUMBER") = "Interest on Interest"
                        row(header) = FormatNumber(itemMain.InterestOnInterest, 2)
                    Case 8
                        row("IDNUMBER") = "NSSRA in Bank Statement"
                        row(header) = FormatNumber(itemMain.NSSRABankStatement, 2)
                End Select
            Next

            dt.Rows.Add(row)
        Next
        dt.AcceptChanges()

        Return dt
    End Function
#End Region


End Class
