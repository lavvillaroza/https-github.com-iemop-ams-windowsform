'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             Statement of Account Helper
'Orginal Author:         Lance Arjay Villaroza
'File Creation Date:     July 21, 2016
'Development Group:      Software Development and Support Division
'Description:            Class Statement Of Account Helper
'Arguments/Parameters:   -
'Files/Database Tables:  -
'Return Value:           -
'Error codes/Exceptions: -
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description


Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Imports Excel = Microsoft.Office.Interop.Excel


Public Class StatementOfAccountHelper
#Region "WESMBillHelper"
    Public _WBillHelper As WESMBillHelper
    Private ReadOnly Property WBillHelper() As WESMBillHelper
        Get
            Return _WBillHelper
        End Get
    End Property
#End Region

#Region "BFactory"
    Public _BFactory As New BusinessFactory
    Private ReadOnly Property BFactory() As BusinessFactory
        Get
            Return _BFactory
        End Get
    End Property
#End Region

#Region "DAL"
    Private _DataAccess As DAL
    Public ReadOnly Property DataAccess() As DAL
        Get
            Return Me._DataAccess
        End Get
    End Property
#End Region

    Private InitialSOANo As Long = 0

    Public Sub New()
        Me._DataAccess = DAL.GetInstance()
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._BFactory = BusinessFactory.GetInstance
        Me._objParticipantsList = New List(Of AMParticipants)

    End Sub

    Private _objStatementofAccountList As New List(Of StatementofAccountNew)
    Public ReadOnly Property objStatementofAccountList() As List(Of StatementofAccountNew)
        Get
            Return _objStatementofAccountList
        End Get
    End Property

    Private _objParticipantsList As New List(Of AMParticipants)
    Public ReadOnly Property objParticipantsList() As List(Of AMParticipants)
        Get
            Return _objParticipantsList
        End Get
    End Property

    Private _objDueDateList As New List(Of Date)
    Public ReadOnly Property objDueDateList() As List(Of Date)
        Get
            Return _objDueDateList
        End Get
    End Property

    Private _objWESMBillSummaryList As New List(Of WESMBillSummary)
    Public ReadOnly Property objWESMBillSummaryList() As List(Of WESMBillSummary)
        Get
            Return _objWESMBillSummaryList
        End Get
    End Property

    Private _objWESMBillList As New List(Of WESMBill)
    Public ReadOnly Property objWESMBillList() As List(Of WESMBill)
        Get
            Return _objWESMBillList
        End Get
    End Property

    Private _objDMCMList As New List(Of DebitCreditMemo)
    Public ReadOnly Property objDMCMList() As List(Of DebitCreditMemo)
        Get
            Return _objDMCMList
        End Get
    End Property


    Private _dueDate As New Date
    Public ReadOnly Property dueDate() As Date
        Get
            Return _dueDate
        End Get
    End Property

    Public Function VerifyIfSOACreated(ByVal SelectedDueDate As String) As Boolean
        Dim ret As Boolean
        Try
            ret = WBillHelper.VerifyIfSOAHasCreated(SelectedDueDate)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return ret
    End Function

    Public Sub SOAForAdding()
        Try
            Me._objDueDateList = (From x In Me.WBillHelper.GetWESMBillSummaryDueDateList("DUE_DATE") _
                                  Order By x Descending _
                                  Select x).ToList()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try        
    End Sub

    Public Sub SOAForViewing()
        Try
            Me._objDueDateList = Me.WBillHelper.GetSOADueDateList()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub GenerateSOAViewing(ByVal SelectedDueDate As String)
        Me._objParticipantsList = Me.WBillHelper.GetAMParticipants()
        Me._objStatementofAccountList = Me.WBillHelper.GetStatementOfAccountNew(SelectedDueDate)
        Me._objWESMBillList = Me.WBillHelper.GetWESMBills(CDate(SelectedDueDate))
        Me._objDMCMList = Me.WBillHelper.GetDebitCreditMemoMain(CDate(SelectedDueDate))
        Me._dueDate = CDate(SelectedDueDate)
    End Sub

    Public Sub SelectedDueDate(ByVal SelectedDueDate As String)
        Me._dueDate = CDate(SelectedDueDate)
    End Sub

    Public Sub GenerateSOA(ByVal SelectedDueDate As String)
        Me._objParticipantsList = Me.WBillHelper.GetAMParticipants()
        Me._objWESMBillSummaryList = Me.WBillHelper.GetWESMBillSummaryWithAREndingBalance()
        Me._objDMCMList = Me.WBillHelper.GetDebitCreditMemoMain(CDate(SelectedDueDate))
        Me._dueDate = CDate(SelectedDueDate)
        Dim DateFrom As New Date
        Dim DateTo As New Date
        Dim GetSystemDate As Date = Me.WBillHelper.GetSystemDate
        InitialSOANo = 0
        Dim oobjStatementofAccount = New List(Of StatementofAccountNew)

        InitialSOANo = Me.WBillHelper.GetCurrentSOANumber + 1

        'Get Previous Statement of acocunt
        Dim PrevSOA As List(Of StatementofAccountNew) = Me.WBillHelper.GetStatementOfAccountNew()
        Dim itmPrevious As StatementofAccountNew = (From x In PrevSOA _
                                                    Where x.DueDate < Me.WBillHelper.GetSystemDate _
                                                    Select x Order By x.DueDate Descending).FirstOrDefault

        If itmPrevious IsNot Nothing Then
            DateFrom = itmPrevious.SOADate
        Else
            DateFrom = (From x In Me.WBillHelper.GetWESMBillSummaryDueDateList("NEW_DUEDATE") _
                        Where x < GetSystemDate _
                        Order By x Descending _
                        Select x).FirstOrDefault
        End If

        DateTo = GetSystemDate

        'Collection or Payment Received by PEMC
        Dim WESMPayReceivedList As List(Of CollectionAllocation) = Me.WBillHelper.GetCollectionAllocation(DateFrom, DateTo)

        'Offsetting Collection
        Dim WESMOffsettingList As List(Of ARCollection) = Me.WBillHelper.GetSOAOffsetting(DateFrom, DateTo)

        'WESM Bill Summary
        Dim _WESMBillSummaryPerParticipant As List(Of String) = (From x In Me.objWESMBillSummaryList _
                                                                 Select x.IDNumber.IDNumber _
                                                                 Order By IDNumber).Distinct.ToList()
        'WESM Bill
        Dim _WESMBillPerParticipant As List(Of WESMBill) = (From x In WBillHelper.GetWESMBills(CDate(SelectedDueDate)) _
                                                            Select x).ToList()

        Me._objWESMBillList = _WESMBillPerParticipant

        For Each item In _WESMBillSummaryPerParticipant
            Dim PreviousEnergy As Decimal = 0
            Dim PreviousVAT As Decimal = 0
            Dim PreviousMF As Decimal = 0
            Dim PayReceivedEnergy As Decimal = 0
            Dim PayReceivedVAT As Decimal = 0
            Dim PayReceivedMF As Decimal = 0
            Dim PayReceivedDefault As Decimal = 0
            Dim OffsettingEnergy As Decimal = 0
            Dim OffsettingVAT As Decimal = 0
            Dim OffsettingMF As Decimal = 0
            Dim OffsettingDefault As Decimal = 0

            Dim getParticipantInfo As AMParticipants = (From x In Me.objParticipantsList Where x.IDNumber = item Select x).FirstOrDefault

            If getParticipantInfo Is Nothing Then
                Throw New Exception("No participants information for " & item & ", please report to the administrator.")
                Exit Sub
            End If

            Dim GetParticipantCurrWESMBillSummaryList = (From x In Me.objWESMBillSummaryList _
                                                     Where x.IDNumber.IDNumber = item _
                                                     And x.DueDate = CDate(SelectedDueDate) _
                                                     Select x).ToList

            Dim GetParticipantWESMBillSummaryList = (From x In Me.objWESMBillSummaryList _
                                                     Where x.IDNumber.IDNumber = item _
                                                     And x.DueDate < CDate(SelectedDueDate) _
                                                     Select x).ToList

            Dim GetParticipantWESMPayReceivedList = (From x In WESMPayReceivedList _
                                                    Join y In GetParticipantWESMBillSummaryList _
                                                    On x.WESMBillSummaryNo.WESMBillSummaryNo Equals y.WESMBillSummaryNo _
                                                    Select x).ToList

            Dim GetParticipantWESMOffsettingList = (From x In WESMOffsettingList _
                                                    Join y In GetParticipantWESMBillSummaryList _
                                                    On x.WESMBillSummaryNo Equals y.WESMBillSummaryNo _
                                                    Select x).ToList

            Dim SOAItem As New StatementofAccountNew
            With SOAItem
                .SOADate = GetSystemDate
                .SOANumber = InitialSOANo
                .DueDate = CDate(SelectedDueDate)
                .IDNumber = item

                PreviousEnergy = (From x In GetParticipantWESMBillSummaryList _
                                  Where x.ChargeType = EnumChargeType.E _
                                  Select x.EndingBalance).Sum()

                If getParticipantInfo.ZeroRatedEnergy = False Then
                    PreviousVAT = (From x In GetParticipantWESMBillSummaryList _
                               Where x.ChargeType = EnumChargeType.EV _
                               Select x.EndingBalance).Sum()
                Else
                    PreviousVAT = 0
                End If

                If getParticipantInfo.ZeroRatedMarketFees = False Then
                    PreviousMF = (From x In GetParticipantWESMBillSummaryList _
                              Where x.ChargeType = EnumChargeType.MF _
                              Or x.ChargeType = EnumChargeType.MFV _
                              Select x.EndingBalance).Sum()
                Else
                    PreviousMF = (From x In GetParticipantWESMBillSummaryList _
                              Where x.ChargeType = EnumChargeType.MF _
                              Select x.EndingBalance).Sum()

                End If


                PayReceivedEnergy = (From x In GetParticipantWESMPayReceivedList
                                     Where x.CollectionType = EnumCollectionType.Energy
                                     Select x.Amount).Sum()

                PayReceivedVAT = (From x In GetParticipantWESMPayReceivedList _
                                  Where x.CollectionType = EnumCollectionType.VatOnEnergy _
                                  Select x.Amount).Sum()

                PayReceivedMF = (From x In GetParticipantWESMPayReceivedList _
                                 Where x.CollectionType = EnumCollectionType.MarketFees _
                                 And x.CollectionType = EnumCollectionType.VatOnMarketFees _
                                 Select x.Amount).Sum()

                PayReceivedDefault = (From x In GetParticipantWESMPayReceivedList _
                                      Where x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy _
                                      Or x.CollectionType = EnumCollectionType.DefaultInterestOnMF _
                                      Or x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF _
                                      Or x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnEnergy _
                                      Or x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest _
                                      Or x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest _
                                      Select x.Amount).Sum

                OffsettingEnergy = (From x In GetParticipantWESMOffsettingList
                                    Where x.CollectionType = EnumCollectionType.Energy
                                    Select x.AllocationAmount).Sum()

                OffsettingVAT = (From x In GetParticipantWESMOffsettingList _
                                  Where x.CollectionType = EnumCollectionType.VatOnEnergy _
                                  Select x.AllocationAmount).Sum()

                OffsettingMF = (From x In GetParticipantWESMOffsettingList _
                                 Where x.CollectionType = EnumCollectionType.MarketFees _
                                 And x.CollectionType = EnumCollectionType.VatOnMarketFees _
                                 Select x.AllocationAmount).Sum()

                OffsettingDefault = (From x In GetParticipantWESMOffsettingList _
                                      Where x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy _
                                      Or x.CollectionType = EnumCollectionType.DefaultInterestOnMF _
                                      Or x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF _
                                      Or x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnEnergy _
                                      Select x.AllocationAmount).Sum()

                .PreviousBalanceDueOnEnergy = PreviousEnergy + PayReceivedEnergy + OffsettingEnergy
                .PreviousBalanceDueOnVAT = PreviousVAT + PayReceivedVAT + OffsettingVAT
                .PreviousBalanceDueOnMF = PreviousMF + PayReceivedMF + OffsettingMF

                .PaymentReceivedOnEnergy = PayReceivedEnergy
                .PaymentReceivedOnVAT = PayReceivedVAT
                .PaymentReceivedOnMF = PayReceivedMF
                .PaymentReceivedOnDefaultInterest = PayReceivedDefault

                .SettledThruOffsettingOnEnergy = OffsettingEnergy
                .SettledThruOffsettingOnVAT = OffsettingVAT
                .SettledThruOffsettingOnMF = OffsettingMF
                .SettledThruOffsettingOnDefaultInterest = OffsettingDefault

                Dim CopyWESMBillSummaryList As New List(Of WESMBillSummary)
                For Each itemMon In GetParticipantCurrWESMBillSummaryList
                    If itemMon.ChargeType = EnumChargeType.EV Then
                        CopyWESMBillSummaryList.Add(itemMon)
                    ElseIf itemMon.ChargeType = EnumChargeType.MFV Then
                        If getParticipantInfo.ZeroRatedMarketFees = False Then
                            CopyWESMBillSummaryList.Add(itemMon)
                        End If
                    Else
                        CopyWESMBillSummaryList.Add(itemMon)
                    End If
                Next
                .WESMBillSummaryList = CopyWESMBillSummaryList
            End With
            InitialSOANo += 1
            oobjStatementofAccount.Add(SOAItem)
        Next
        Me._objStatementofAccountList = oobjStatementofAccount
    End Sub

    Public Function GenerateSOADT() As DataTable
        Dim SOATable As New DataTable
        SOATable.TableName = "SOATable"
        With SOATable.Columns
            .Add("SOANumber", GetType(String))
            .Add("SOADate", GetType(Date))
            .Add("IDNumber", GetType(String))
            .Add("ParticipantID", GetType(String))
            .Add("InvoiceDueDate", GetType(Date))
            .Add("PrevEnergy", GetType(String))
            .Add("PrevVAT", GetType(String))
            .Add("PrevMF", GetType(String))
            .Add("PayReceivedEnergy", GetType(String))
            .Add("PayReceivedVAT", GetType(String))
            .Add("PayReceivedMF", GetType(String))
            .Add("PayReceivedDefault", GetType(String))
            .Add("OffsetEnergy", GetType(String))
            .Add("OffsetVAT", GetType(String))
            .Add("OffsetMF", GetType(String))
            .Add("OffsetDefault", GetType(String))
        End With

        For Each SOAItem In Me.objStatementofAccountList
            Dim row As DataRow            
            row = SOATable.NewRow()
            row("SOANumber") = SOAItem.SOANumber
            row("SOADate") = SOAItem.SOADate
            row("IDNumber") = SOAItem.IDNumber
            row("ParticipantID") = (From x In Me.objParticipantsList Where x.IDNumber = SOAItem.IDNumber Select x.ParticipantID).FirstOrDefault
            row("InvoiceDueDate") = SOAItem.DueDate
            row("PrevEnergy") = FormatNumber(SOAItem.PreviousBalanceDueOnEnergy, UseParensForNegativeNumbers:=TriState.True)
            row("PrevVAT") = FormatNumber(SOAItem.PreviousBalanceDueOnVAT, UseParensForNegativeNumbers:=TriState.True)
            row("PrevMF") = FormatNumber(SOAItem.PreviousBalanceDueOnMF, UseParensForNegativeNumbers:=TriState.True)
            row("PayReceivedEnergy") = FormatNumber(SOAItem.PaymentReceivedOnEnergy, UseParensForNegativeNumbers:=TriState.True)
            row("PayReceivedVAT") = FormatNumber(SOAItem.PaymentReceivedOnVAT, UseParensForNegativeNumbers:=TriState.True)
            row("PayReceivedMF") = FormatNumber(SOAItem.PaymentReceivedOnMF, UseParensForNegativeNumbers:=TriState.True)
            row("PayReceivedDefault") = FormatNumber(SOAItem.PaymentReceivedOnDefaultInterest, UseParensForNegativeNumbers:=TriState.True)
            row("OffsetEnergy") = FormatNumber(SOAItem.SettledThruOffsettingOnEnergy, UseParensForNegativeNumbers:=TriState.True)
            row("OffsetVAT") = FormatNumber(SOAItem.SettledThruOffsettingOnVAT, UseParensForNegativeNumbers:=TriState.True)
            row("OffsetMF") = FormatNumber(SOAItem.SettledThruOffsettingOnMF, UseParensForNegativeNumbers:=TriState.True)
            row("OffsetDefault") = FormatNumber(SOAItem.SettledThruOffsettingOnDefaultInterest, UseParensForNegativeNumbers:=TriState.True)
            SOATable.Rows.Add(row)
        Next
        SOATable.AcceptChanges()
        Return SOATable
    End Function

    Public Function GenerateSOAReport(ByVal _DTMain As DataTable, ByVal _DTDetails As DataTable) As DataSet
        Dim ret As New DataSet
        Dim ds As New DataSet
        Dim dicDMCMList As New Dictionary(Of String, DebitCreditMemoDetails)
        Dim dicInvInWESMBillSummaryList As New Dictionary(Of String, WESMBillSummary)
        Dim getWESMBillCalendar As List(Of CalendarBillingPeriod) = Me.WBillHelper.GetCalendarBP()

        For Each item In Me.objDMCMList
            For Each itemx In item.DMCMDetails
                Dim createKey As String = itemx.InvDMCMNo & item.ChargeType.ToString
                If Not dicDMCMList.ContainsKey(createKey) Then
                    dicDMCMList.Add(createKey, itemx)
                End If
            Next
        Next

        For Each itmMain In Me.objStatementofAccountList
            If itmMain.IDNumber.Equals("ALECO") Then
                Dim GetItemParticipantInfo As AMParticipants = (From x In Me.objParticipantsList
                                                                Where x.IDNumber = itmMain.IDNumber
                                                                Select x).FirstOrDefault

                Dim dtMainRow As DataRow
                dtMainRow = _DTMain.NewRow

                With itmMain
                    dtMainRow("SOA_NUMBER") = .SOANumber
                    dtMainRow("SOA_DATE") = .SOADate
                    dtMainRow("DUE_DATE") = .DueDate
                    dtMainRow("ID_NUMBER") = .IDNumber
                    dtMainRow("PARTICIPANT_ID") = GetItemParticipantInfo.IDNumber
                    dtMainRow("PARTICIPANT_ADDRESS") = GetItemParticipantInfo.ParticipantID
                    dtMainRow("ENERGY_OUTSTANDING") = .PreviousBalanceDueOnEnergy
                    dtMainRow("VAT_OUTSTANDING") = .PreviousBalanceDueOnVAT
                    dtMainRow("MF_OUTSTANDING") = .PreviousBalanceDueOnMF

                    dtMainRow("COL_ENERGY") = .PaymentReceivedOnEnergy
                    dtMainRow("COL_VAT") = .PaymentReceivedOnVAT
                    dtMainRow("COL_MF") = .PaymentReceivedOnMF
                    dtMainRow("COL_DEFAULT") = .PaymentReceivedOnDefaultInterest

                    dtMainRow("PAY_ENERGY") = .SettledThruOffsettingOnEnergy
                    dtMainRow("PAY_VAT") = .SettledThruOffsettingOnVAT
                    dtMainRow("PAY_MF") = .SettledThruOffsettingOnMF
                    dtMainRow("PAY_DEFAULT") = .SettledThruOffsettingOnDefaultInterest

                    _DTMain.Rows.Add(dtMainRow)
                    _DTMain.AcceptChanges()

                    If itmMain.WESMBillSummaryList.Count = 0 Then
                        Dim dtDetailsRow As DataRow
                        dtDetailsRow = _DTDetails.NewRow
                        dtDetailsRow("SOA_NUMBER") = .SOANumber
                        dtDetailsRow("DATE_COVERED") = "NoRecord"

                        _DTDetails.Rows.Add(dtDetailsRow)
                        _DTDetails.AcceptChanges()
                    Else
                        Dim getWESMBillList = (From x In Me.objWESMBillList
                                               Where x.IDNumber = itmMain.IDNumber
                                               Select x Order By x.BillingPeriod, x.Remarks).ToList

                        Dim getWESMBillListByInvoiceNo = (From x In getWESMBillList Select x.InvoiceNumber).Distinct().ToList
                        For Each itmwb In getWESMBillListByInvoiceNo
                            Dim dtDetailsRow As DataRow
                            dtDetailsRow = _DTDetails.NewRow
                            For Each itemWB In (From x In getWESMBillList Where x.InvoiceNumber = itmwb Select x Order By x.BatchCode).ToList()
                                Dim getTotalAmountperBatch = (From x In getWESMBillList Where x.BatchCode = itemWB.BatchCode And x.ChargeType = itemWB.ChargeType Select x.Amount).Sum
                                If getTotalAmountperBatch < 0 Then
                                    dtDetailsRow("SOA_NUMBER") = .SOANumber
                                    dtDetailsRow("INVOICE_NO") = itemWB.InvoiceNumber
                                    Select Case itemWB.ChargeType
                                        Case EnumChargeType.E
                                            dtDetailsRow("ENERGY") = itemWB.Amount
                                        Case EnumChargeType.EV
                                            dtDetailsRow("VAT") = itemWB.Amount
                                        Case EnumChargeType.MF, EnumChargeType.MFV
                                            If IsDBNull(dtDetailsRow("MF")) Then
                                                dtDetailsRow("MF") = itemWB.Amount
                                            Else
                                                dtDetailsRow("MF") = CDec(dtDetailsRow("MF")) + itemWB.Amount
                                            End If
                                    End Select

                                    If itemWB.RegistrationID = itemWB.IDNumber Then
                                        dtDetailsRow("PARENT_ID") = (From x In Me.objParticipantsList
                                                                     Where x.IDNumber = itemWB.IDNumber
                                                                     Select x.ParticipantID).FirstOrDefault
                                    Else
                                        dtDetailsRow("CHILD_ID") = (From x In Me.objParticipantsList
                                                                    Where x.IDNumber = itemWB.RegistrationID
                                                                    Select x.ParticipantID).FirstOrDefault
                                    End If

                                    dtDetailsRow("BILLING_PERIOD") = itemWB.BillingPeriod
                                    dtDetailsRow("DATE_COVERED") = "(" & itemWB.Remarks & ")" '"(" & getStartEndDate.StartDate.ToShortDateString & " - " & getStartEndDate.EndDate.ToShortDateString & ")"                                
                                Else
                                    Dim getKey As String = itemWB.InvoiceNumber & itemWB.ChargeType.ToString
                                    If dicDMCMList.ContainsKey(getKey) Then
                                        Dim getDMCMDetails As DebitCreditMemoDetails = dicDMCMList.Item(getKey)
                                        'Dim dtDetailsRow As DataRow
                                        'dtDetailsRow = _DTDetails.NewRow
                                        dtDetailsRow("SOA_NUMBER") = .SOANumber
                                        dtDetailsRow("INVOICE_NO") = itemWB.InvoiceNumber

                                        Select Case itemWB.ChargeType
                                            Case EnumChargeType.E
                                                If getDMCMDetails.Credit <> 0 Then
                                                    dtDetailsRow("ENERGY") = getDMCMDetails.Credit * -1
                                                Else
                                                    dtDetailsRow("ENERGY") = getDMCMDetails.Debit
                                                End If
                                            Case EnumChargeType.EV
                                                If getDMCMDetails.Credit <> 0 Then
                                                    dtDetailsRow("VAT") = getDMCMDetails.Credit * -1
                                                Else
                                                    dtDetailsRow("VAT") = getDMCMDetails.Debit
                                                End If
                                            Case EnumChargeType.MF, EnumChargeType.MFV
                                                If getDMCMDetails.Credit <> 0 Then
                                                    If IsDBNull(dtDetailsRow("MF")) Then
                                                        dtDetailsRow("MF") = getDMCMDetails.Credit * -1
                                                    Else
                                                        dtDetailsRow("MF") = CDec(dtDetailsRow("MF")) + getDMCMDetails.Credit * -1
                                                    End If

                                                    dtDetailsRow("MF") = getDMCMDetails.Credit * -1
                                                Else
                                                    If IsDBNull(dtDetailsRow("MF")) Then
                                                        dtDetailsRow("MF") = getDMCMDetails.Debit
                                                    Else
                                                        dtDetailsRow("MF") = CDec(dtDetailsRow("MF")) + getDMCMDetails.Debit
                                                    End If
                                                End If
                                        End Select

                                        If itemWB.RegistrationID = itemWB.IDNumber Then
                                            dtDetailsRow("PARENT_ID") = (From x In Me.objParticipantsList
                                                                         Where x.IDNumber = itemWB.IDNumber
                                                                         Select x.ParticipantID).FirstOrDefault
                                        Else
                                            dtDetailsRow("CHILD_ID") = (From x In Me.objParticipantsList
                                                                        Where x.IDNumber = itemWB.RegistrationID
                                                                        Select x.ParticipantID).FirstOrDefault
                                        End If
                                        dtDetailsRow("BILLING_PERIOD") = itemWB.BillingPeriod
                                        dtDetailsRow("DATE_COVERED") = "(" & itemWB.Remarks & ")" '"(" & getStartEndDate.StartDate.ToShortDateString & " - " & getStartEndDate.EndDate.ToShortDateString & ")"                                   
                                    End If
                                End If
                            Next
                            _DTDetails.Rows.Add(dtDetailsRow)
                            _DTDetails.AcceptChanges()
                        Next
                    End If
                End With
            End If
        Next
        ds.Tables.Add(_DTMain)
        ds.Tables.Add(_DTDetails)
        ret = ds
        Return ret
    End Function



    Public Sub SaveSOA(ByVal ChangeOldSOA As Boolean)
        Try
            Dim report As New DataReport
            Dim listSQL As New List(Of String)
            Dim SQL As String
            Dim UpdatedBy As String = AMModule.UserName
            Dim AMSOANo As Long = 0
            Dim SysDateTime As Date = WBillHelper.GetSystemDateTime()

            If ChangeOldSOA = True Then
                SQL = "DELETE FROM AM_SOA_DETAILS A WHERE EXISTS(SELECT AM_SOA_NO FROM AM_SOA B WHERE B.INV_DUEDATE = TO_DATE('" & Me._dueDate.ToShortDateString & "','mm/dd/yyyy') AND A.AM_SOA_NO = B.AM_SOA_NO)"
                listSQL.Add(SQL)
                SQL = "DELETE FROM AM_SOA A WHERE A.INV_DUEDATE = TO_DATE('" & Me._dueDate.ToShortDateString & "','mm/dd/yyyy')"
                listSQL.Add(SQL)
                For Each item In Me.objStatementofAccountList
                    AMSOANo = WBillHelper.GetSequenceID("SEQ_AM_SOA_NO")
                    SQL = "INSERT INTO AM_SOA (AM_SOA_NO, AM_SOA_DATE, INV_DUEDATE, ID_NUMBER, PREV_BALANCE_DUE_ENE, PREV_BALANCE_DUE_VAT, PREV_BALANCE_DUE_MF, " & vbNewLine _
                                            & "PAY_RECEIVED_ENE, PAY_RECEIVED_VAT, PAY_RECEIVED_MF, PAY_RECEIVED_DEFAULT, SETL_OFFSETING_ENE, SETL_OFFSETING_VAT, SETL_OFFSETING_MF, SETL_OFFSETTING_DEFAULT, UPDATED_BY, UPDATED_DATE) " & vbNewLine _
                        & "VALUES (" & AMSOANo & ", TO_DATE('" & item.SOADate.ToString("MM/dd/yyyy") & "','mm/dd/yyyy'), TO_DATE('" & item.DueDate & "','mm/dd/yyyy'), '" & item.IDNumber & "', " & vbNewLine _
                                & item.PreviousBalanceDueOnEnergy & ", " & item.PreviousBalanceDueOnVAT & ", " & item.PreviousBalanceDueOnMF & ", " & item.PaymentReceivedOnEnergy & ", " & item.PaymentReceivedOnVAT & ", " & vbNewLine _
                                & item.PaymentReceivedOnMF & ", " & item.PaymentReceivedOnDefaultInterest & ", " & item.SettledThruOffsettingOnEnergy & ", " & item.SettledThruOffsettingOnVAT & ", " & item.SettledThruOffsettingOnMF & ", " & vbNewLine _
                                & item.SettledThruOffsettingOnDefaultInterest & ", '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'))"
                    listSQL.Add(SQL)
                    For Each itemDetails In item.WESMBillSummaryList
                        SQL = "INSERT INTO AM_SOA_DETAILS(AM_SOA_NO, WESMBILL_SUMMARY_NO) VALUES (" & AMSOANo & ", " & itemDetails.WESMBillSummaryNo & ")"
                        listSQL.Add(SQL)
                    Next
                Next
            Else
                For Each item In Me.objStatementofAccountList
                    AMSOANo = WBillHelper.GetSequenceID("SEQ_AM_SOA_NO")

                    SQL = "INSERT INTO AM_SOA (AM_SOA_NO, AM_SOA_DATE, INV_DUEDATE, ID_NUMBER, PREV_BALANCE_DUE_ENE, PREV_BALANCE_DUE_VAT, PREV_BALANCE_DUE_MF, " & vbNewLine _
                                            & "PAY_RECEIVED_ENE, PAY_RECEIVED_VAT, PAY_RECEIVED_MF, PAY_RECEIVED_DEFAULT, SETL_OFFSETING_ENE, SETL_OFFSETING_VAT, SETL_OFFSETING_MF, SETL_OFFSETTING_DEFAULT, UPDATED_BY, UPDATED_DATE) " & vbNewLine _
                        & "VALUES (" & AMSOANo & ", TO_DATE('" & item.SOADate.ToString("MM/dd/yyyy") & "','mm/dd/yyyy'), TO_DATE('" & item.DueDate & "','mm/dd/yyyy'), '" & item.IDNumber & "', " & vbNewLine _
                                & item.PreviousBalanceDueOnEnergy & ", " & item.PreviousBalanceDueOnVAT & ", " & item.PreviousBalanceDueOnMF & ", " & item.PaymentReceivedOnEnergy & ", " & item.PaymentReceivedOnVAT & ", " & vbNewLine _
                                & item.PaymentReceivedOnMF & ", " & item.PaymentReceivedOnDefaultInterest & ", " & item.SettledThruOffsettingOnEnergy & ", " & item.SettledThruOffsettingOnVAT & ", " & item.SettledThruOffsettingOnMF & ", " & vbNewLine _
                                & item.SettledThruOffsettingOnDefaultInterest & ", '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'))"
                    listSQL.Add(SQL)
                    For Each itemDetails In item.WESMBillSummaryList
                        SQL = "INSERT INTO AM_SOA_DETAILS(AM_SOA_NO, WESMBILL_SUMMARY_NO) VALUES (" & AMSOANo & ", " & itemDetails.WESMBillSummaryNo & ")"
                        listSQL.Add(SQL)
                    Next
                Next
            End If
            
            report = Me.DataAccess.ExecuteSaveQuery(listSQL, New DataSet)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub
End Class
