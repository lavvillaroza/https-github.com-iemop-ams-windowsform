'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             FuncGenerateWESMBillSummaryTransaction
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     September 01, 2015
'Development Group:      Software Development and Support Division
'Description:            Class to Generate the WESMBillSummary Transactions From AR
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   September 01, 2015      Vladimir E. Espiritu            Class initialization
'

Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementLogic
Public Class FuncGenerateWESMBillSummaryTransactionAR

#Region "Initialization/Constructor"
    Public Sub New()

    End Sub
#End Region


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

#Region "ListOfWESMBillTransactionFromCollection"
    Private _ListOfWESMBillTransactionFromCollection As List(Of WESMBillSummaryTransaction)
    Public Property ListOfWESMBillTransactionFromCollection() As List(Of WESMBillSummaryTransaction)
        Get
            Return _ListOfWESMBillTransactionFromCollection
        End Get
        Set(ByVal value As List(Of WESMBillSummaryTransaction))
            _ListOfWESMBillTransactionFromCollection = value
        End Set
    End Property


#End Region

#Region "ListOfWESMBillTransactionFromPayment"
    Private _ListOfWESMBillTransactionFromPayment As List(Of WESMBillSummaryTransaction)
    Public Property ListOfWESMBillTransactionFromPayment() As List(Of WESMBillSummaryTransaction)
        Get
            Return _ListOfWESMBillTransactionFromPayment
        End Get
        Set(ByVal value As List(Of WESMBillSummaryTransaction))
            _ListOfWESMBillTransactionFromPayment = value
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

#Region "GenerateDicWESMBillSummaryInvoice"
    Public Function GenerateDicWESMBillSummaryInvoice() As Dictionary(Of Long, WESMBillSummaryWithInvoice)
        Dim dicWESMBillSummaryTransactionFromOR As New Dictionary(Of Long, List(Of WESMBillSummaryTransaction))
        Dim dicWESMBillSummaryTransactionFromPayment As New Dictionary(Of Long, List(Of WESMBillSummaryTransaction))
        Dim dicWESMBillSummaryTransactionFromParentChildOffsetting As New Dictionary(Of Long, List(Of WESMBillSummaryTransaction))
        Dim dicWESMBillSummaryTransactionFromSPA As New Dictionary(Of Long, List(Of WESMBillSummaryTransaction))
        Dim dicWESMBillSummaryInvoice As New Dictionary(Of Long, WESMBillSummaryWithInvoice)

        'Populate the list of OR Transaction per WESM Bill Summary
        For Each item In Me.ListOfWESMBillTransactionFromCollection
            If dicWESMBillSummaryTransactionFromOR.ContainsKey(item.WESMBillSummaryNo) Then
                dicWESMBillSummaryTransactionFromOR(item.WESMBillSummaryNo).Add(item)
            Else
                dicWESMBillSummaryTransactionFromOR.Add(item.WESMBillSummaryNo, New List(Of WESMBillSummaryTransaction))
                dicWESMBillSummaryTransactionFromOR(item.WESMBillSummaryNo).Add(item)
            End If
        Next

        'Populate the list of Payment Transaction per WESM Bill Summary
        For Each item In Me.ListOfWESMBillTransactionFromPayment
            If dicWESMBillSummaryTransactionFromPayment.ContainsKey(item.WESMBillSummaryNo) Then
                dicWESMBillSummaryTransactionFromPayment(item.WESMBillSummaryNo).Add(item)
            Else
                dicWESMBillSummaryTransactionFromPayment.Add(item.WESMBillSummaryNo, New List(Of WESMBillSummaryTransaction))
                dicWESMBillSummaryTransactionFromPayment(item.WESMBillSummaryNo).Add(item)
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

        'Pouplate only the AR invoice 
        For Each item In Me.ListOfWESMBillSummaryInvoice
            If item.BalanceType = "AP" Then
                Continue For
            End If

            Dim listCollection As New List(Of WESMBillSummaryTransaction)
            Dim listPayment As New List(Of WESMBillSummaryTransaction)
            Dim listDMCMForParentAndChild As New List(Of WESMBillSummaryTransaction)
            Dim listDMCMForSPA As New List(Of WESMBillSummaryTransaction)
            Dim allocatedCollection As Decimal = 0
            Dim closingARThruSPA As Decimal = 0

            'Get only the invoice with outstanding balance after Collection Allocation
            If dicWESMBillSummaryTransactionFromOR.ContainsKey(item.WESMBillSummaryNo) Then

                For Each itemTransaction In dicWESMBillSummaryTransactionFromOR(item.WESMBillSummaryNo)
                    allocatedCollection += Math.Abs(itemTransaction.Amount)
                Next

                listCollection = dicWESMBillSummaryTransactionFromOR(item.WESMBillSummaryNo)
            End If

            'Get only the invoice with outstanding balance after Payment Allocation
            If dicWESMBillSummaryTransactionFromPayment.ContainsKey(item.WESMBillSummaryNo) Then

                For Each itemTransaction In dicWESMBillSummaryTransactionFromPayment(item.WESMBillSummaryNo)
                    allocatedCollection += Math.Abs(itemTransaction.Amount)
                Next

                listPayment = dicWESMBillSummaryTransactionFromPayment(item.WESMBillSummaryNo)
            End If

            'Get the SPA of the invoice with outstanding balance
            If dicWESMBillSummaryTransactionFromSPA.ContainsKey(item.WESMBillSummaryNo) Then
                listDMCMForSPA = dicWESMBillSummaryTransactionFromSPA(item.WESMBillSummaryNo)
                For Each itemTransaction In listDMCMForSPA
                    closingARThruSPA += itemTransaction.Amount
                Next
            End If

            'Do not add If the WESM Bill is already fully paid
            If allocatedCollection = Math.Abs(item.BeginningBalance) Then
                Continue For
            End If

            If (allocatedCollection + closingARThruSPA) = Math.Abs(item.BeginningBalance) Then
                Continue For
            End If

            'Get the Parent and Child Offsetting of the invoice with outstanding balance
            If dicWESMBillSummaryTransactionFromParentChildOffsetting.ContainsKey(item.WESMBillSummaryNo) Then
                listDMCMForParentAndChild = dicWESMBillSummaryTransactionFromParentChildOffsetting(item.WESMBillSummaryNo)
            End If

            item.ListOfWESMBillTransactionFromCollection = listCollection
            item.ListOfWESMBillTransactionFromParentChildOffsetting = listDMCMForParentAndChild
            item.ListOfWESMBillTransactionFromPaymentAllocation = listPayment
            item.ListOfWESMBillTransactionFromSPA = listDMCMForSPA
            
            dicWESMBillSummaryInvoice.Add(item.WESMBillSummaryNo, item)
        Next

        Return dicWESMBillSummaryInvoice
    End Function

#End Region

#Region "PopulateSubsidiaryLedger"
    Public Function PopulateSubsidiaryLedger() As List(Of SubsidiaryLedgerAR)
        Dim listSubsidiaryLedger As New List(Of SubsidiaryLedgerAR)
        Dim dicWESMBillSummaryInvoice As New Dictionary(Of Long, WESMBillSummaryWithInvoice)
        Dim recordid As Integer

        dicWESMBillSummaryInvoice = Me.GenerateDicWESMBillSummaryInvoice()

        recordid = 0
        For Each item In dicWESMBillSummaryInvoice

            recordid += 1
            'Add the WESM Bill
            Dim itemARInvoice As New SubsidiaryLedgerAR
            With itemARInvoice
                .RecordID = recordid
                .IDNumber = item.Value.IDNumber
                .TransactionDate = item.Value.InvoiceDate
                .InvoiceTransactionDate = item.Value.InvoiceDate
                .TransactionType = BIRDocumentsType.FinalStatement
                .ChargeTypeValue = item.Value.ChargeType.ToString()
                .ReferenceNumber = item.Value.InvoiceNumber
                .Amount = Math.Abs(item.Value.InvoiceAmount)
                .CurrentDate = Me.TransactionDate
            End With
            listSubsidiaryLedger.Add(itemARInvoice)

            'Add the Parent and Child Offsetting pertaining to the invoice number
            For Each itemTransaction In item.Value.ListOfWESMBillTransactionFromParentChildOffsetting
                Dim itemARInvoiceTransaction As New SubsidiaryLedgerAR
                recordid += 1
                With itemARInvoiceTransaction
                    .RecordID = recordid
                    .IDNumber = itemTransaction.IDNumber
                    .TransactionDate = itemTransaction.TransactionDate
                    .InvoiceTransactionDate = itemTransaction.TransactionDate
                    .TransactionType = itemTransaction.TransactionType
                    .ReferenceNumber = itemTransaction.ReferenceNo.ToString()
                    .Amount = Math.Abs(itemTransaction.Amount) * -1D
                    .CurrentDate = Me.TransactionDate
                End With
                listSubsidiaryLedger.Add(itemARInvoiceTransaction)
            Next

            'Add the collection pertaining to the invoice number
            For Each itemTransaction In item.Value.ListOfWESMBillTransactionFromCollection
                Dim itemARInvoiceTransaction As New SubsidiaryLedgerAR
                recordid += 1
                With itemARInvoiceTransaction
                    .RecordID = recordid
                    .IDNumber = itemTransaction.IDNumber
                    .TransactionDate = itemTransaction.TransactionDate
                    .InvoiceTransactionDate = itemTransaction.TransactionDate
                    .TransactionType = itemTransaction.TransactionType
                    .ReferenceNumber = itemTransaction.ReferenceNo.ToString()
                    .Amount = Math.Abs(itemTransaction.Amount) * -1D
                    .CurrentDate = Me.TransactionDate
                End With
                listSubsidiaryLedger.Add(itemARInvoiceTransaction)
            Next

            'Add the payment allocation pertaining to the invoice number
            For Each itemTransaction In item.Value.ListOfWESMBillTransactionFromPaymentAllocation
                Dim itemARInvoiceTransaction As New SubsidiaryLedgerAR
                recordid += 1

                With itemARInvoiceTransaction
                    .RecordID = recordid
                    .IDNumber = itemTransaction.IDNumber
                    .TransactionDate = itemTransaction.TransactionDate
                    .InvoiceTransactionDate = itemTransaction.TransactionDate
                    .TransactionType = itemTransaction.TransactionType
                    .ReferenceNumber = itemTransaction.ReferenceNo.ToString()
                    .Amount = itemTransaction.Amount
                    .CurrentDate = Me.TransactionDate
                End With
                listSubsidiaryLedger.Add(itemARInvoiceTransaction)
            Next

        Next
        listSubsidiaryLedger.TrimExcess()

        Return listSubsidiaryLedger
    End Function
#End Region


End Class
