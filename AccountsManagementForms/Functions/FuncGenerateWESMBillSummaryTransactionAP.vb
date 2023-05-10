'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             FuncGenerateWESMBillSummaryTransactionAP
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     October 09, 2015
'Development Group:      Software Development and Support Division
'Description:            Class to Generate the WESMBillSummary Transactions From AP
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   September 01, 2015      Vladimir E. Espiritu            Class initialization
'   October 21, 2020        Lance Arjay Villaroza           Including Payment Offsetting JV

Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementLogic

Public Class FuncGenerateWESMBillSummaryTransactionAP


#Region "TransactionDate"
    Private _TransactionDate As Date
    Public Property TransactionDate() As Date
        Get
            Return _TransactionDate
        End Get
        Set(ByVal value As Date)
            _TransactionDate = value
        End Set
    End Property

#End Region

#Region "ListOfWESMBillSummaryInvoice"
    Private _ListOfWESMBillSummaryInvoice As List(Of WESMBillSummaryWithInvoice)
    Public Property ListOfWESMBillSummaryInvoice() As List(Of WESMBillSummaryWithInvoice)
        Get
            Return _ListOfWESMBillSummaryInvoice
        End Get
        Set(ByVal value As List(Of WESMBillSummaryWithInvoice))
            _ListOfWESMBillSummaryInvoice = value
        End Set
    End Property

#End Region

#Region "ListOfWESMBillTransactionFromParentChildOffsetting"
    Private _ListOfWESMBillTransactionFromParentChildOffsetting As List(Of WESMBillSummaryTransaction)
    Public Property ListOfWESMBillTransactionFromParentChildOffsetting() As List(Of WESMBillSummaryTransaction)
        Get
            Return _ListOfWESMBillTransactionFromParentChildOffsetting
        End Get
        Set(ByVal value As List(Of WESMBillSummaryTransaction))
            _ListOfWESMBillTransactionFromParentChildOffsetting = value
        End Set
    End Property

#End Region

#Region "ListOfWESMBillTransactionFromSPA"
    Private _ListOfWESMBillTransactionFromSPA As List(Of WESMBillSummaryTransaction)
    Public Property ListOfWESMBillTransactionFromSPA() As List(Of WESMBillSummaryTransaction)
        Get
            Return _ListOfWESMBillTransactionFromSPA
        End Get
        Set(ByVal value As List(Of WESMBillSummaryTransaction))
            _ListOfWESMBillTransactionFromSPA = value
        End Set
    End Property

#End Region

#Region "ListOfWESMBillTransactionPaymentAllocation"
    Private _ListOfWESMBillTransactionPaymentAllocation As List(Of WESMBillSummaryTransaction)
    Public Property ListOfWESMBillTransactionPaymentAllocation() As List(Of WESMBillSummaryTransaction)
        Get
            Return _ListOfWESMBillTransactionPaymentAllocation
        End Get
        Set(ByVal value As List(Of WESMBillSummaryTransaction))
            _ListOfWESMBillTransactionPaymentAllocation = value
        End Set
    End Property
#End Region

#Region "GenerateDicWESMBillSummaryInvoice"
    Public Function GenerateDicWESMBillSummaryInvoice() As Dictionary(Of Long, WESMBillSummaryWithInvoice)
        Dim dicWESMBillSummaryTransaction As New Dictionary(Of Long, List(Of WESMBillSummaryTransaction))
        Dim dicWESMBillSummaryTransactionFromPaymentAllocation As New Dictionary(Of Long, List(Of WESMBillSummaryTransaction))        
        Dim dicWESMBillSummaryTransactionFromParentChildOffsetting As New Dictionary(Of Long, List(Of WESMBillSummaryTransaction))
        Dim dicWESMBillSummaryTransactionFromSPA As New Dictionary(Of Long, List(Of WESMBillSummaryTransaction))
        Dim dicWESMBillSummaryInvoice As New Dictionary(Of Long, WESMBillSummaryWithInvoice)

        'Populate the list of Transaction per WESM Bill Summary from Payment Allocation
        For Each item In Me.ListOfWESMBillTransactionPaymentAllocation
            If dicWESMBillSummaryTransactionFromPaymentAllocation.ContainsKey(item.WESMBillSummaryNo) Then
                dicWESMBillSummaryTransactionFromPaymentAllocation(item.WESMBillSummaryNo).Add(item)
            Else
                dicWESMBillSummaryTransactionFromPaymentAllocation.Add(item.WESMBillSummaryNo, New List(Of WESMBillSummaryTransaction))
                dicWESMBillSummaryTransactionFromPaymentAllocation(item.WESMBillSummaryNo).Add(item)
            End If
        Next

        'Populate the list of Parent and Child Offsetting per WESM Bill Summary
        For Each item In Me.ListOfWESMBillTransactionFromParentChildOffsetting
            If dicWESMBillSummaryTransactionFromParentChildOffsetting.ContainsKey(item.WESMBillSummaryNo) Then
                dicWESMBillSummaryTransactionFromParentChildOffsetting(item.WESMBillSummaryNo).Add(item)
            Else
                dicWESMBillSummaryTransactionFromParentChildOffsetting.Add(item.WESMBillSummaryNo, New List(Of WESMBillSummaryTransaction))
                dicWESMBillSummaryTransactionFromParentChildOffsetting(item.WESMBillSummaryNo).Add(item)
            End If
        Next

        'Populate the list of SPA per WESM Bill Summary
        For Each item In Me.ListOfWESMBillTransactionFromSPA
            If dicWESMBillSummaryTransactionFromSPA.ContainsKey(item.WESMBillSummaryNo) Then
                dicWESMBillSummaryTransactionFromSPA(item.WESMBillSummaryNo).Add(item)
            Else
                dicWESMBillSummaryTransactionFromSPA.Add(item.WESMBillSummaryNo, New List(Of WESMBillSummaryTransaction))
                dicWESMBillSummaryTransactionFromSPA(item.WESMBillSummaryNo).Add(item)
            End If
        Next

        'Populate only the AP invoice 
        For Each item In Me.ListOfWESMBillSummaryInvoice            
            If item.BeginningBalance < 0 Then
                Continue For
            End If

            Dim listPayment As New List(Of WESMBillSummaryTransaction)
            Dim listDMCM As New List(Of WESMBillSummaryTransaction)
            Dim listDMCMForSPA As New List(Of WESMBillSummaryTransaction)
            Dim closingAPThruSPA As Decimal = 0
            Dim allocatedShare As Decimal = 0

            'Get only the invoice with outstanding balance
            If dicWESMBillSummaryTransactionFromPaymentAllocation.ContainsKey(item.WESMBillSummaryNo) Then                

                For Each itemTransaction In dicWESMBillSummaryTransactionFromPaymentAllocation(item.WESMBillSummaryNo)
                    allocatedShare += Math.Abs(itemTransaction.Amount)
                Next

                If allocatedShare <> Math.Abs(item.BeginningBalance) Then
                    listPayment = dicWESMBillSummaryTransactionFromPaymentAllocation(item.WESMBillSummaryNo)
                Else
                    Continue For
                End If
            End If

            'Get the SPA of the invoice with outstanding balance
            If dicWESMBillSummaryTransactionFromSPA.ContainsKey(item.WESMBillSummaryNo) Then
                listDMCMForSPA = dicWESMBillSummaryTransactionFromSPA(item.WESMBillSummaryNo)
                For Each itemTransaction In listDMCMForSPA
                    closingAPThruSPA += itemTransaction.Amount
                Next
                If (allocatedShare + closingAPThruSPA) = Math.Abs(item.BeginningBalance) Then
                    Continue For
                End If
            End If

            'Get the Parent and Child Offsetting of the invoice with outstanding balance
            If dicWESMBillSummaryTransactionFromParentChildOffsetting.ContainsKey(item.WESMBillSummaryNo) Then
                listDMCM = dicWESMBillSummaryTransactionFromParentChildOffsetting(item.WESMBillSummaryNo)
            End If

            item.ListOfWESMBillTransactionFromPaymentAllocation = listPayment            
            item.ListOfWESMBillTransactionFromParentChildOffsetting = listDMCM
            item.ListOfWESMBillTransactionFromSPA = listDMCMForSPA
            dicWESMBillSummaryInvoice.Add(item.WESMBillSummaryNo, item)
        Next

        Return dicWESMBillSummaryInvoice
    End Function

#End Region

#Region "PopulateSubsidiaryLedger"
    Public Function PopulateSubsidiaryLedger() As List(Of SubsidiaryLedgerAP)
        Dim listSubsidiaryLedger As New List(Of SubsidiaryLedgerAP)
        Dim dicWESMBillSummaryInvoice As New Dictionary(Of Long, WESMBillSummaryWithInvoice)
        Dim recordid As Integer

        dicWESMBillSummaryInvoice = Me.GenerateDicWESMBillSummaryInvoice()

        recordid = 0
        For Each item In dicWESMBillSummaryInvoice
            'Add the WESM Bill
            Dim itemAPInvoice As New SubsidiaryLedgerAP
            If item.Value.InvoiceAmount > 0 Then
                recordid += 1
                With itemAPInvoice
                    .RecordID = recordid
                    .IDNumber = item.Value.IDNumber
                    .TransactionDate = item.Value.InvoiceDate
                    .InvoiceTransactionDate = item.Value.InvoiceDate
                    .TransactionType = BIRDocumentsType.FinalStatement
                    .ChargeTypeValue = item.Value.ChargeType.ToString()
                    .ReferenceNumber = item.Value.InvoiceNumber
                    .Amount = item.Value.InvoiceAmount
                    .CurrentDate = Me.TransactionDate
                End With
                listSubsidiaryLedger.Add(itemAPInvoice)

                'Add the Parent and Child Offsetting pertaining to the invoice number
                For Each itemTransaction In item.Value.ListOfWESMBillTransactionFromParentChildOffsetting
                    Dim itemARInvoiceTransaction As New SubsidiaryLedgerAP

                    recordid += 1
                    With itemARInvoiceTransaction
                        .RecordID = recordid
                        .IDNumber = itemTransaction.IDNumber
                        .TransactionDate = itemTransaction.TransactionDate
                        .InvoiceTransactionDate = itemTransaction.TransactionDate
                        .TransactionType = itemTransaction.TransactionType
                        .ReferenceNumber = itemTransaction.ReferenceNo.ToString()
                        .Amount = itemTransaction.Amount * -1D
                        .CurrentDate = Me.TransactionDate
                    End With
                    listSubsidiaryLedger.Add(itemARInvoiceTransaction)
                Next

                'Add the collection pertaining to the invoice number
                For Each itemTransaction In item.Value.ListOfWESMBillTransactionFromPaymentAllocation
                    Dim itemAPInvoiceTransaction As New SubsidiaryLedgerAP
                    recordid += 1
                    With itemAPInvoiceTransaction
                        .RecordID = recordid
                        .IDNumber = itemTransaction.IDNumber
                        .TransactionDate = itemTransaction.TransactionDate
                        .InvoiceTransactionDate = itemTransaction.TransactionDate
                        .TransactionType = itemTransaction.TransactionType
                        .ReferenceNumber = itemTransaction.ReferenceNo.ToString()
                        .Amount = itemTransaction.Amount * -1D
                        .CurrentDate = Me.TransactionDate
                    End With
                    listSubsidiaryLedger.Add(itemAPInvoiceTransaction)
                Next

                For Each itemTransaction In item.Value.ListOfWESMBillTransactionFromSPA
                    Dim itemAPInvoiceTransaction As New SubsidiaryLedgerAP
                    recordid += 1
                    With itemAPInvoiceTransaction
                        .RecordID = recordid
                        .IDNumber = itemTransaction.IDNumber
                        .TransactionDate = itemTransaction.TransactionDate
                        .InvoiceTransactionDate = itemTransaction.TransactionDate
                        .TransactionType = itemTransaction.TransactionType
                        .ReferenceNumber = itemTransaction.ReferenceNo.ToString()
                        .Amount = itemTransaction.Amount * -1D
                        .CurrentDate = Me.TransactionDate
                    End With
                    listSubsidiaryLedger.Add(itemAPInvoiceTransaction)
                Next
            End If            
        Next
        listSubsidiaryLedger.TrimExcess()

        Return listSubsidiaryLedger
    End Function
#End Region

End Class
