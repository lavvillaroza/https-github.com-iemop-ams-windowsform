'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             FuncOffsetting
'Orginal Author:         Vladimir E.Espiritu
'File Creation Date:     February 22, 2012
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
'   February 22, 2012       Vladimir E.Espiritu         Class initialization
'   March 05, 2012          Vladimir E. Espiritu        Added WESMBillSummaryNo property
'   March 13, 2012          Vladimir E. Espiritu        Removed BillingPeriod and add CalendarBP
'   March 13, 2012          Vladimir E. Espiritu        Added ID Number into DMCM details
'   May 15, 2012            Vladimir E. Espiritu        Revision the Summary of DMCM for JV entry
'   November 05, 2012       Vladimir E. Espiritu        Save the WESM Bill which its value is equal to zero
'   August 29, 2013         Vladimir E. Espiritu        Added ListOfAllParticipants property
'

Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementLogic

Public Class FuncOffsetting

    Private Enum EnumOffsetting
        P2PC2C
        P2C
    End Enum

    Private Enum EnumIDType
        P
        C
    End Enum

#Region "Initialization/Constructor"
    Public Sub New()
        Me._BFactory = BusinessFactory.GetInstance()
        Me._DMCMNo = 0
        Me._JVNo = 0
        Me._OffsetNo = 1
        Me._ChargeType = Nothing
        Me._DueDate = Nothing
        Me._CalendarBP = Nothing
        Me._ListOfWESMBill = New List(Of WESMBill)
        Me._ListOfAllParticipants = New List(Of AMParticipants)
        Me._ListOfP2CMapping = New List(Of ParticipantParentChildMapping)
        Me._ListDocSignatories = New List(Of DocSignatories)
    End Sub
#End Region

#Region "BFactory"
    Private _BFactory As BusinessFactory
    Public ReadOnly Property BFactory() As BusinessFactory
        Get
            Return Me._BFactory
        End Get
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

#Region "DMCMNo"
    Private _DMCMNo As Long
    Public Property DMCMNo() As Long
        Get
            Return _DMCMNo
        End Get
        Set(ByVal value As Long)
            _DMCMNo = value
        End Set
    End Property

#End Region

#Region "JVNo"
    Private _JVNo As Long
    Public Property JVNo() As Long
        Get
            Return _JVNo
        End Get
        Set(ByVal value As Long)
            _JVNo = value
        End Set
    End Property

#End Region

#Region "OffsetNo"
    Private _OffsetNo As Long
    Public Property OffsetNo() As Long
        Get
            Return _OffsetNo
        End Get
        Set(ByVal value As Long)
            _OffsetNo = value
        End Set
    End Property
#End Region

#Region "SettlementRun"
    Private _StlRun As String
    Public Property StlRun() As String
        Get
            Return _StlRun
        End Get
        Set(ByVal value As String)
            _StlRun = value
        End Set
    End Property
#End Region

#Region "ChargeType"
    Private _ChargeType As EnumChargeType
    Public Property ChargeType() As EnumChargeType
        Get
            Return _ChargeType
        End Get
        Set(ByVal value As EnumChargeType)
            _ChargeType = value
        End Set
    End Property

#End Region

#Region "DueDate"
    Private _DueDate As Date
    Public Property DueDate() As Date
        Get
            Return _DueDate
        End Get
        Set(ByVal value As Date)
            _DueDate = value
        End Set
    End Property

#End Region

#Region "CalendarBP"
    Private _CalendarBP As CalendarBillingPeriod
    Public Property CalendarBP() As CalendarBillingPeriod
        Get
            Return _CalendarBP
        End Get
        Set(ByVal value As CalendarBillingPeriod)
            _CalendarBP = value
        End Set
    End Property

#End Region

#Region "ListOfWESMBill"
    Private _ListOfWESMBill As List(Of WESMBill)
    Public Property ListOfWESMBill() As List(Of WESMBill)
        Get
            Me._ListOfWESMBill = (From x In Me._ListOfWESMBill
                                  Where x.ChargeType = Me.ChargeType).ToList()
            Return _ListOfWESMBill
        End Get
        Set(ByVal value As List(Of WESMBill))
            _ListOfWESMBill = value
        End Set
    End Property
#End Region

#Region "ListOfWESMTransctionAllocationCoverSummary"
    Private _ListOfWESMBillAllocCoverySummary As List(Of WESMBillAllocCoverSummary)
    Public Property ListOfWESMBillAllocCoverySummary() As List(Of WESMBillAllocCoverSummary)
        Get
            Return _ListOfWESMBillAllocCoverySummary
        End Get
        Set(ByVal value As List(Of WESMBillAllocCoverSummary))
            _ListOfWESMBillAllocCoverySummary = value
        End Set
    End Property

#End Region

#Region "ListOfWESMBillSalesAndPurchases"
    Private _ListOfWESMBillSalesAndPurchases As List(Of WESMBillSalesAndPurchased)
    Public Property ListOfWESMBillSalesAndPurchases() As List(Of WESMBillSalesAndPurchased)
        Get
            'Me._ListOfWESMBillSalesAndPurchases = (From x In Me._ListOfWESMBill _
            '                      Where x.ChargeType = Me.ChargeType).ToList()
            Return _ListOfWESMBillSalesAndPurchases
        End Get
        Set(ByVal value As List(Of WESMBillSalesAndPurchased))
            _ListOfWESMBillSalesAndPurchases = value
        End Set
    End Property

#End Region

#Region "ListOfP2CMapping"
    Private _ListOfP2CMapping As List(Of ParticipantParentChildMapping)
    Public Property ListOfP2CMapping() As List(Of ParticipantParentChildMapping)
        Get
            Return _ListOfP2CMapping
        End Get
        Set(ByVal value As List(Of ParticipantParentChildMapping))
            _ListOfP2CMapping = value
        End Set
    End Property

#End Region

#Region "ListDocSignatories"
    Private _ListDocSignatories As List(Of DocSignatories)
    Public Property ListDocSignatories() As List(Of DocSignatories)
        Get
            Return _ListDocSignatories
        End Get
        Set(ByVal value As List(Of DocSignatories))
            _ListDocSignatories = value
        End Set
    End Property

#End Region

#Region "ListOfAllParticipants"
    Private _ListOfAllParticipants As List(Of AMParticipants)
    Public Property ListOfAllParticipants() As List(Of AMParticipants)
        Get
            Return _ListOfAllParticipants
        End Get
        Set(ByVal value As List(Of AMParticipants))
            _ListOfAllParticipants = value
        End Set
    End Property

#End Region

#Region "ChargeTypeValue"
    Private _ChargeTypeValue As String
    Public ReadOnly Property ChargeTypeValue() As String
        Get
            Select Case Me.ChargeType
                Case EnumChargeType.E
                    Me._ChargeTypeValue = "Energy"
                Case EnumChargeType.EV
                    Me._ChargeTypeValue = "VAT on Energy"
                Case EnumChargeType.MF
                    Me._ChargeTypeValue = "Market Fees"
                Case EnumChargeType.MFV
                    Me._ChargeTypeValue = "VAT on Market Fees"
            End Select

            Return _ChargeTypeValue
        End Get
    End Property

#End Region

#Region "dic EWT"
    Private _dicEWT As Dictionary(Of String, Decimal)
    Public Property dicEWT() As Dictionary(Of String, Decimal)
        Get
            Return _dicEWT
        End Get
        Set(ByVal value As Dictionary(Of String, Decimal))
            _dicEWT = value
        End Set
    End Property
#End Region

#Region "GenerateOffsetting"
    Public Function GenerateOffsetting(ByVal P2C1stOffsettingExemption As List(Of ParentChildExemption), ByVal chargeTypeValue As EnumChargeType) As FuncOffsettingResult
        Dim result As New FuncOffsettingResult
        Dim listOfDebitCreditMemo As New List(Of DebitCreditMemo)
        Dim listOfOffsetting As New List(Of OffsetP2PC2CDetails)
        Dim listOfWESMBillSummary As New List(Of WESMBillSummary)
        Dim listOfJournalVoucher As New List(Of JournalVoucher)
        Dim listOfGPPosted As New List(Of WESMBillGPPosted)

        'Use this variables because there are instance that Accounts Payable 
        'is not always debit, same to Accounts Receivable
        Dim TotalAPDebit As Decimal = 0
        Dim TotalARDebit As Decimal = 0
        Dim TotalAPCredit As Decimal = 0
        Dim TotalARCredit As Decimal = 0


        'Get the final WESM Bill
        Dim listFinalWESMBill = Me.GenerateFinalWESMBill(Me.ListOfWESMBill)


        'Get the list of participants
        Dim listOfDistinctParticipant = (From x In listFinalWESMBill
                                         Order By x.IDNumber Select x.IDNumber Distinct).ToList()

        For Each item In listOfDistinctParticipant
            Dim selectedParticipant = item
            'Get the participant details
            Dim itemParticipant = (From x In Me.ListOfAllParticipants
                                   Where x.IDNumber = selectedParticipant
                                   Select x).First()

            'Get the WESM Bills of the selected participants
            Dim listWESMBill = (From x In listFinalWESMBill
                                Where x.IDNumber = selectedParticipant
                                Select x).ToList()

            'Get the WESM Bill Sales and Purchased
            Dim listWESMBillSalesAndPurchased = (From x In Me.ListOfWESMBillSalesAndPurchases
                                                 Where x.IDNumber.IDNumber = selectedParticipant
                                                 Select x).ToList

            Dim getParentChildExemption As List(Of ParentChildExemption) = (From x In P2C1stOffsettingExemption
                                                                            Where x.ParentParticipantID.IDNumber = selectedParticipant
                                                                            Select x).ToList()

            'Generate the datatable
            Me.GenerateOffsetDataset(itemParticipant, listWESMBill, listOfDebitCreditMemo,
                                        listOfWESMBillSummary, listOfOffsetting, listWESMBillSalesAndPurchased,
                                        getParentChildExemption, chargeTypeValue)
        Next

        'Update the Offset No
        Me.OffsetNo += 1

        If listOfDebitCreditMemo.Count <> 0 Then
            TotalAPDebit = Me.GetAccountsAPPayableAmount(listOfDebitCreditMemo)
            TotalARDebit = Me.GetAccountsARPayableAmount(listOfDebitCreditMemo)
            TotalAPCredit = Me.GetAccountsAPReceivableAmount(listOfDebitCreditMemo)
            TotalARCredit = Me.GetAccountsARReceivableAmount(listOfDebitCreditMemo)

            'Check if Debit and Credit are equal
            If TotalAPDebit + TotalARDebit <> TotalAPCredit + TotalARCredit Then
                Throw New ApplicationException("Total Debit and Credit are not balance.")
            End If

            'Update the JV No
            Me.JVNo += 1

            'Generate the Journal Voucher
            listOfJournalVoucher.Add(Me.GenerateJournalVoucher(Me.JVNo, TotalAPDebit, TotalARDebit, TotalAPCredit, TotalARCredit))
            listOfJournalVoucher.TrimExcess()

            'Generate the WESM GP Posted
            If Me.StlRun.Contains(";") Then
                Dim splitSTLrun = Me.StlRun.Split(CChar(";"))
                Dim counter As Integer = 0
                For Each sitem In splitSTLrun
                    counter += 1
                    If counter = 1 Then
                        listOfGPPosted.Add(Me.GenerateWESMGPPosted(Me.JVNo, Me.OffsetNo, TotalAPDebit + TotalARDebit, TotalAPCredit + TotalARCredit, sitem))
                    Else
                        listOfGPPosted.Add(Me.GenerateWESMGPPosted(0, Me.OffsetNo, 0, 0, sitem))
                    End If
                Next
            Else
                listOfGPPosted.Add(Me.GenerateWESMGPPosted(Me.JVNo, Me.OffsetNo, TotalAPDebit + TotalARDebit, TotalAPCredit + TotalARCredit, Me.StlRun))
            End If

            listOfGPPosted.TrimExcess()

            'Update the JV No of Debit/Credit Memo
            For Each item In listOfDebitCreditMemo
                item.JVNumber = Me.JVNo
            Next

            'Update the Offset No of OffsetDetails
            For Each item In listOfOffsetting
                item.OffsetNumber = Me.OffsetNo.ToString()
            Next
        Else
            'Generate the WESM GP Posted
            If Me.StlRun.Contains(";") Then
                Dim splitSTLrun = Me.StlRun.Split(CChar(";"))
                Dim counter As Integer = 0
                For Each sitem In splitSTLrun
                    listOfGPPosted.Add(Me.GenerateWESMGPPosted(0, Me.OffsetNo, 0, 0, sitem))
                Next
            Else
                listOfGPPosted.Add(Me.GenerateWESMGPPosted(0, Me.OffsetNo, 0, 0, Me.StlRun))
            End If
            listOfGPPosted.TrimExcess()
        End If

        'Generate the results
        With result
            .WESMBillSummaryNo = Me.WESMBillSummaryNo
            .DMCMNo = Me.DMCMNo
            .OffsetNo = Me.OffsetNo
            .JVNo = Me.JVNo
            .ListOfDebitCreditMemo = listOfDebitCreditMemo
            .ListOfGPPosted = listOfGPPosted
            .ListOfJournalVoucher = listOfJournalVoucher
            .ListOfOffsetting = listOfOffsetting
            .ListOfWESMBillSummary = listOfWESMBillSummary
        End With

        Return result
    End Function

#End Region

#Region "GenerateFinalWESMBill"
    Private Function GenerateFinalWESMBill(ByVal listWESMBill As List(Of WESMBill)) As List(Of WESMBill)
        Dim listFinalWESMBill As New List(Of WESMBill)

        Dim itemList1 = From x In listWESMBill _
                        Join y In Me.ListOfP2CMapping On CStr(x.IDNumber) Equals y.IDNumber _
                        Where y.ParentFlag = 0 _
                        Select New WESMBill _
                        With {.BatchCode = x.BatchCode, .AMCode = x.AMCode, .BillingPeriod = x.BillingPeriod, _
                              .SettlementRun = x.SettlementRun, .IDNumber = CStr(y.PCNumber), .InvoiceNumber = x.InvoiceNumber, _
                              .RegistrationID = x.RegistrationID, .ForTheAccountOf = x.ForTheAccountOf, .InvoiceDate = x.InvoiceDate, _
                              .Amount = x.Amount, .ChargeType = x.ChargeType, .DueDate = x.DueDate, .MarketFeesRate = x.MarketFeesRate, _
                              .Remarks = x.Remarks}

        listFinalWESMBill.AddRange(itemList1.ToList())
        listFinalWESMBill.TrimExcess()

        Dim itemList2 = From x In listWESMBill _
                        Where Not (From y In Me.ListOfP2CMapping Where y.ParentFlag = 0 _
                                   Select y.IDNumber).Contains(x.IDNumber) _
                        Select x

        listFinalWESMBill.AddRange(itemList2.ToList())
        listFinalWESMBill.TrimExcess()

        Return listFinalWESMBill
    End Function
#End Region

#Region "GenerateOffsetDataset"
    Private Sub GenerateOffsetDataset(ByVal itemParticipant As AMParticipants,
                                      ByRef listWESMBill As List(Of WESMBill),
                                      ByRef listDMCM As List(Of DebitCreditMemo),
                                      ByRef listWESMBillSummary As List(Of WESMBillSummary),
                                      ByRef listOffsetDetails As List(Of OffsetP2PC2CDetails),
                                      ByVal listWESMBillSalesAndPurchased As List(Of WESMBillSalesAndPurchased),
                                      ByVal listP2C1stOffsettingExemption As List(Of ParentChildExemption), ByVal chargeTypeValue As EnumChargeType)
        Dim listDMCMDetails As New List(Of DebitCreditMemoDetails)
        Dim LastOffsetInvoice As String = ""
        Dim WESMBillNoOfLastOffsetInvoice As Long = 0

        'This variable will contains all the WESM Bills 
        'that were offset
        Dim tempListWESMBill As New List(Of WESMBill)

        'Get the Total AP
        Dim totalAP As Decimal = 0D
        'Get the Total AR
        Dim totalAR As Decimal = 0D

        If listP2C1stOffsettingExemption Is Nothing Then
            totalAP = (From x In listWESMBill
                       Where x.Amount > 0
                       Select x.Amount).Sum()

            totalAR = (From x In listWESMBill
                       Where x.Amount < 0
                       Select x.Amount).Sum()
        Else
            For Each item In listWESMBill
                Dim getPC1stOffsetting = (From x In listP2C1stOffsettingExemption
                                          Where x.ParentParticipantID.IDNumber = item.IDNumber And x.ChildParticipantID.IDNumber = item.RegistrationID _
                                          And x.ChargeType = item.ChargeType Select x).FirstOrDefault
                If getPC1stOffsetting Is Nothing Then
                    If item.Amount > 0 Then
                        totalAP += item.Amount
                    ElseIf item.Amount < 0 Then
                        totalAR += item.Amount
                    End If
                End If
            Next
        End If

        If chargeTypeValue = EnumChargeType.MF Then
            If totalAP <> 0 And totalAR <> 0 Then
                Dim listAP As New List(Of WESMBill)
                Dim listAR As New List(Of WESMBill)

                If listP2C1stOffsettingExemption.Count = 0 Then
                    'Get the WESM Bills which are payable
                    listAP = (From x In listWESMBill
                              Where x.Amount > 0
                              Select x Order By x.Amount, x.InvoiceNumber).ToList()
                    'Get the WESM Bills which are receivable
                    listAR = (From x In listWESMBill
                              Where x.Amount < 0
                              Select x Order By x.Amount Descending, x.InvoiceNumber Ascending).ToList()
                Else
                    For Each item In listWESMBill
                        Dim getPC1stOffsetting = (From x In listP2C1stOffsettingExemption
                                                  Where x.ParentParticipantID.IDNumber = item.IDNumber _
                                                  And x.ChildParticipantID.IDNumber = item.RegistrationID And x.ChargeType = item.ChargeType Select x).FirstOrDefault
                        If getPC1stOffsetting Is Nothing Then
                            If item.Amount > 0 Then
                                listAP.Add(item)
                            ElseIf item.Amount < 0 Then
                                listAR.Add(item)
                            End If
                        End If
                    Next

                    listAP.TrimExcess()
                    listAR.TrimExcess()

                End If

                'Removed by Vlad 05/27/2018 for EWT
                'If Math.Abs(totalAP + totalAPEWT) >= Math.Abs(totalAR) Then
                If Math.Abs(totalAP) >= Math.Abs(totalAR) Then
                    'This temporary variable is use to save the AP amount and compare into AR amount
                    Dim tempAP As Decimal = 0
                    'Add the accounts receivable WESM Bills into temporary variable
                    'NOTE: Clone the object before turning the amount into zero
                    tempListWESMBill.AddRange(CType(BFactory.CloneObject(listAR), List(Of WESMBill)))

                    'Update all accounts receivable amount into zero
                    For Each item In (From x In listAR Select x Order By x.Amount Descending, x.InvoiceNumber Ascending).ToList()
                        item.Amount = 0D
                    Next

                    'Deduct the WESM Bills which are equal to Accounts Receivable
                    'and update the WESM Bills amount which are affected
                    For Each item In (From x In listAP Select x Order By x.Amount, x.InvoiceNumber).ToList()
                        If tempAP < Math.Abs(totalAR) Then
                            If tempAP + item.Amount >= Math.Abs(totalAR) Then
                                'Clone the WESM Bill before saving in Debit/Credit memo
                                Dim tempWESMBill = CType(BFactory.CloneObject(item), WESMBill)

                                'Get the difference of AP and AR 
                                tempWESMBill.Amount = Math.Abs(totalAR) - tempAP
                                tempListWESMBill.Add(tempWESMBill)

                                'Compute the balance of the WESM Bill
                                item.Amount = (tempAP + item.Amount) - Math.Abs(totalAR)

                                'Select the last offset invoice
                                'This will be save in WESM Bill Summary
                                LastOffsetInvoice = item.InvoiceNumber
                                Exit For
                            Else
                                tempAP += item.Amount

                                'Clone the WESM Bill before updating the amount
                                tempListWESMBill.Add(CType(BFactory.CloneObject(item), WESMBill))
                                item.Amount = 0D
                            End If
                        End If
                    Next
                Else
                    'This temporary variable is use to save the AR amount and compare into AP amount
                    Dim tempAR As Decimal = 0
                    'Removed by Vlad 05/27/2018 for EWT
                    'Dim totalAP2 As Decimal = totalAP + totalAPEWT
                    Dim totalAP2 As Decimal = totalAP
                    'Add the accounts receivable WESM Bills into temporary variable
                    'NOTE: Clone the object before turning the amount into zero
                    tempListWESMBill.AddRange(CType(BFactory.CloneObject(listAP), List(Of WESMBill)))

                    'Update all accounts payable amount into zero
                    For Each item In (From x In listAP Select x Order By x.Amount, x.InvoiceNumber).ToList()
                        item.Amount = 0D
                    Next

                    'Deduct the WESM Bills which are equal to Accounts Payable
                    'and update the WESM Bills amount which are affected
                    For Each item In (From x In listAR Select x Order By x.Amount Descending, x.InvoiceNumber Ascending).ToList()
                        If tempAR < totalAP2 Then
                            If tempAR + Math.Abs(item.Amount) > totalAP2 Then
                                'Clone the WESM Bill before saving in Debit/Credit memo
                                Dim tempWESMBill = CType(BFactory.CloneObject(item), WESMBill)

                                'Get the difference of AP and AR 
                                tempWESMBill.Amount = (totalAP2 - tempAR) * -1D
                                tempListWESMBill.Add(tempWESMBill)

                                'Compute the balance of the WESM Bill
                                item.Amount = ((tempAR + Math.Abs(item.Amount)) - totalAP2) * -1D

                                'Select the last offset invoice
                                'This will be save in WESM Bill Summary
                                LastOffsetInvoice = item.InvoiceNumber

                                Exit For
                            Else
                                tempAR += Math.Abs(item.Amount)

                                'Clone the WESM Bill before updating the amount into zero
                                tempListWESMBill.Add(CType(BFactory.CloneObject(item), WESMBill))
                                item.Amount = 0D
                            End If
                        End If
                    Next
                End If

                'Increment the DMCMNo
                Me.DMCMNo += 1

                'Entry for accounts payable and accounts receivable
                'The WESM Bill to be closed will be the entry for Vatable and Zero Rated
                For Each item In tempListWESMBill
                    If item.Amount > 0 Then
                        If totalAP >= Math.Abs(totalAR) Then
                            listDMCMDetails.Add(New DebitCreditMemoDetails(DMCMNo, AMModule.DebitCode,
                                                                           item.Amount, 0, item.InvoiceNumber, EnumSummaryType.INV,
                                                                           itemParticipant))
                        Else
                            listDMCMDetails.Add(New DebitCreditMemoDetails(DMCMNo, AMModule.DebitCode,
                                                                           item.Amount, 0, item.InvoiceNumber, EnumSummaryType.INV,
                                                                           itemParticipant, EnumDMCMComputed.Compute))
                        End If
                    Else
                        If totalAP < Math.Abs(totalAR) Then
                            listDMCMDetails.Add(New DebitCreditMemoDetails(DMCMNo, AMModule.CreditCode,
                                                                           0, Math.Abs(item.Amount), item.InvoiceNumber, EnumSummaryType.INV,
                                                                           itemParticipant))
                        Else
                            listDMCMDetails.Add(New DebitCreditMemoDetails(DMCMNo, AMModule.CreditCode,
                                                                           0, Math.Abs(item.Amount), item.InvoiceNumber, EnumSummaryType.INV,
                                                                           itemParticipant, EnumDMCMComputed.Compute))
                        End If

                    End If
                Next

                'Get the Signatories for DMCM
                Dim signatories = (From x In Me.ListDocSignatories
                                   Where x.DocCode = EnumDocCode.DMCM.ToString()
                                   Select x).First()

                Dim TotalAmount As Decimal = 0
                For Each item In listDMCMDetails
                    TotalAmount += item.Debit
                Next

                Dim Invoices As String = ""
                For index As Integer = 0 To tempListWESMBill.Count - 1
                    If index <> tempListWESMBill.Count - 1 Then
                        Invoices = Invoices & tempListWESMBill(index).InvoiceNumber & ", "
                    Else
                        Invoices = Invoices & tempListWESMBill(index).InvoiceNumber
                    End If
                Next

                'Create DMCM
                Dim itemDMCM As New DebitCreditMemo
                With itemDMCM
                    .DMCMNumber = Me.DMCMNo

                    If totalAP > Math.Abs(totalAR) Then
                        .Particulars = "Debiting your account through offsetting of Final Statement(s) (" & Invoices & ")."
                    ElseIf totalAP < Math.Abs(totalAR) Then
                        .Particulars = "Crediting your account through offsetting of Final Statement(s) (" & Invoices & ")."
                    Else
                        .Particulars = "Closing the Final Statement(s) (" & Invoices & ") through offsetting."
                    End If
                    .IDNumber = itemParticipant.IDNumber
                    .BillingPeriod = Me.CalendarBP.BillingPeriod
                    .DueDate = Me.DueDate
                    .DMCMDetails = listDMCMDetails
                    .ChargeType = Me.ChargeType
                    .TransType = EnumDMCMTransactionType.WESMBillP2PC2COffsetting
                    .TotalAmountDue = TotalAmount
                    .CheckedBy = signatories.Signatory_1
                    .ApprovedBy = signatories.Signatory_2

                    Select Case Me.ChargeType
                        Case EnumChargeType.E

                        Case EnumChargeType.MF
                            .Vatable = TotalAmount

                        Case EnumChargeType.MFV, EnumChargeType.EV
                            .VAT = TotalAmount
                    End Select
                End With

                listDMCM.Add(itemDMCM)
                listDMCM.TrimExcess()
            End If
        End If

        Dim WBSAPTotalEWT As Decimal = (From x In Me.ListOfWESMBillSalesAndPurchases Select x.WithholdingTAX).Sum()

        'Generate the WESMBillSummary
        For Each item In listWESMBill
            Dim GetEWTPerItem As WESMBillSalesAndPurchased = (From x In Me.ListOfWESMBillSalesAndPurchases
                                                              Where x.InvoiceNumber = item.InvoiceNumber
                                                              Select x).FirstOrDefault
            Dim getWTACoverSummaryitem As WESMBillAllocCoverSummary = Me.ListOfWESMBillAllocCoverySummary.Where(Function(x) x.TransactionNo = item.InvoiceNumber).FirstOrDefault
            If GetEWTPerItem Is Nothing Then
                GetEWTPerItem = New WESMBillSalesAndPurchased
            End If
            If (item.Amount <> 0) Or (item.Amount = 0 And GetEWTPerItem.WithholdingTAX <> 0 And item.ChargeType = EnumChargeType.E) Then
                Dim itemWESMSummary As New WESMBillSummary()
                Me.WESMBillSummaryNo += 1
                With itemWESMSummary
                    'Get the WESMBillNo of last offset invoice
                    'This will update all the WESMBillNo of OffsetP2PC2CDetails table
                    If item.InvoiceNumber = LastOffsetInvoice Then
                        WESMBillNoOfLastOffsetInvoice = Me.WESMBillSummaryNo
                    End If
                    .BillPeriod = Me.CalendarBP.BillingPeriod
                    .IDNumber = itemParticipant
                    .ChargeType = Me.ChargeType
                    .DueDate = Me.DueDate
                    .BeginningBalance = item.Amount
                    .EndingBalance = item.Amount
                    .IDType = EnumIDType.P.ToString()
                    .NewDueDate = Me.DueDate
                    .INVDMCMNo = item.InvoiceNumber
                    .SummaryType = EnumSummaryType.INV
                    .WESMBillSummaryNo = Me.WESMBillSummaryNo

                    If WBSAPTotalEWT = 0 Then
                        .EnergyWithholdStatus = EnumEnergyWithholdStatus.NotApplicable
                        'added by LAVV for moving balance of EWT for BIR Ruling as of 05/13/2022
                        If item.ChargeType = EnumChargeType.E And item.Amount < 0 Then
                            .EnergyWithhold = getWTACoverSummaryitem.EWTPurchases * -1
                        ElseIf item.ChargeType = EnumChargeType.E And item.Amount > 0 Then
                            .EnergyWithhold = getWTACoverSummaryitem.EWTSales * -1
                        End If
                    Else
                        If GetEWTPerItem IsNot Nothing Then
                            If GetEWTPerItem.WithholdingTAX <> 0 And .ChargeType = EnumChargeType.E Then
                                .EnergyWithhold = GetEWTPerItem.WithholdingTAX
                                .EnergyWithholdStatus = EnumEnergyWithholdStatus.UnpaidEWT
                            Else
                                .EnergyWithholdStatus = EnumEnergyWithholdStatus.NotApplicable
                                .EnergyWithhold = 0D
                            End If
                        Else
                            .EnergyWithholdStatus = EnumEnergyWithholdStatus.NotApplicable
                            .EnergyWithhold = 0D
                        End If
                    End If

                    If .BeginningBalance > 0 Then
                        .BalanceType = EnumBalanceType.AP
                    Else
                        .BalanceType = EnumBalanceType.AR
                    End If

                    If item.InvoiceNumber.Contains("TS-") Then
                        .NoOffset = True
                    End If
                End With
                listWESMBillSummary.Add(itemWESMSummary)
                listWESMBillSummary.TrimExcess()
            End If
        Next

        'Generate OffsetP2PC2CDetails
        If listDMCMDetails.Count > 0 Then
            Dim listOffset = (From w In listWESMBill Join x In listDMCMDetails
                              On w.InvoiceNumber Equals x.InvDMCMNo
                              Select New OffsetP2PC2CDetails("0", w.AMCode, w.InvoiceNumber,
                                                             x.DMCMNumber, WESMBillNoOfLastOffsetInvoice)).ToList()

            listOffsetDetails.AddRange(listOffset)
            listOffsetDetails.TrimExcess()
        End If

    End Sub
#End Region

#Region "GenerateJournalVoucher"
    Private Function GenerateJournalVoucher(ByVal jvNo As Long, ByVal APDebitAmount As Decimal, ByVal ARDebitAmount As Decimal, _
                                            ByVal APCreditAmount As Decimal, ByVal ARCreditAmount As Decimal) As JournalVoucher
        Dim result As New JournalVoucher

        'Get the Signatories for DMCM
        Dim signatories = (From x In Me.ListDocSignatories _
                           Where x.DocCode = EnumDocCode.JV.ToString() _
                           Select x).First()

        'Create the JV Details
        Dim JVDetails As New List(Of JournalVoucherDetails)
        With JVDetails
            If APDebitAmount <> 0 Then
                .Add(New JournalVoucherDetails(jvNo, AMModule.DebitCode, APDebitAmount, 0))
            End If

            If ARDebitAmount <> 0 Then
                .Add(New JournalVoucherDetails(jvNo, AMModule.CreditCode, ARDebitAmount, 0))
            End If

            If APCreditAmount <> 0 Then
                .Add(New JournalVoucherDetails(jvNo, AMModule.DebitCode, 0, APCreditAmount))
            End If

            If ARCreditAmount <> 0 Then
                .Add(New JournalVoucherDetails(jvNo, AMModule.CreditCode, 0, ARCreditAmount))
            End If
        End With

        'Create the JV Main
        With result
            .JVNumber = jvNo
            .BatchCode = OffsetNo.ToString()
            .PostedType = EnumPostedType.O.ToString()
            .JVDetails = JVDetails
            .CheckedBy = signatories.Signatory_1
            .ApprovedBy = signatories.Signatory_2
        End With
        Return result
    End Function

#End Region

#Region "GenerateWESMGPPosted"
    Private Function GenerateWESMGPPosted(ByVal jvNo As Long, ByVal offsetNo As Long, ByVal DebitAmount As Decimal, _
                                          ByVal CreditAmount As Decimal, ByVal fStlRun As String) As WESMBillGPPosted
        Dim result As New WESMBillGPPosted

        Dim chargeType As String = ""
        Dim remarks As String = ""

        Dim bpValue As String = Me.CalendarBP.BillingPeriod.ToString() & _
                                "(" & FormatDateTime(Me.CalendarBP.StartDate, DateFormat.ShortDate) & " - " & _
                                FormatDateTime(Me.CalendarBP.EndDate, DateFormat.ShortDate) & ")" '

        'Create the reamrks for GP Posted
        remarks = "To record offsetting of Final Statement for " & Me.ChargeTypeValue & " " & _
                  "(Billing Period = " & bpValue & ", Due Date = " & DueDate.ToString("MM/dd/yyyy") & ", "

        If DebitAmount <> CreditAmount Then
            Throw New ApplicationException("Debit amount and credit amount are not equal!")
        End If

        With result
            .BillingPeriod = Me.CalendarBP.BillingPeriod
            .DueDate = DueDate
            .SettlementRun = fStlRun
            .Charge = Me.ChargeType
            .DocumentAmount = DebitAmount
            .BatchCode = offsetNo.ToString()
            .PostType = EnumPostedType.O.ToString()
            .Remarks = remarks
            .JVNumber = jvNo
        End With
        Return result
    End Function
#End Region

#Region "GetAccountsAPPayableAmount"
    Private Function GetAccountsAPPayableAmount(ByVal listItems As List(Of DebitCreditMemo)) As Decimal
        Dim result As Decimal = 0

        For Each itemMain In listItems
            For Each item In itemMain.DMCMDetails
                If item.AccountCode = AMModule.DebitCode Then
                    result += item.Debit
                End If
            Next
        Next
        Return result
    End Function
#End Region

#Region "GetAccountsARPayableAmount"
    Private Function GetAccountsARPayableAmount(ByVal listItems As List(Of DebitCreditMemo)) As Decimal
        Dim result As Decimal = 0

        For Each itemMain In listItems
            For Each item In itemMain.DMCMDetails
                If item.AccountCode = AMModule.CreditCode Then
                    result += item.Debit
                End If
            Next
        Next
        Return result
    End Function
#End Region

#Region "GetAccountsAPReceivableAmount"
    Private Function GetAccountsAPReceivableAmount(ByVal listItems As List(Of DebitCreditMemo)) As Decimal
        Dim result As Decimal = 0

        For Each itemMain In listItems
            For Each item In itemMain.DMCMDetails
                If item.AccountCode = AMModule.DebitCode Then
                    result += item.Credit
                End If
            Next
        Next
        Return result
    End Function
#End Region

#Region "GetAccountsARReceivableAmount"
    Private Function GetAccountsARReceivableAmount(ByVal listItems As List(Of DebitCreditMemo)) As Decimal
        Dim result As Decimal = 0

        For Each itemMain In listItems
            For Each item In itemMain.DMCMDetails
                If item.AccountCode = AMModule.CreditCode Then
                    result += item.Credit
                End If
            Next
        Next
        Return result
    End Function
#End Region

End Class
