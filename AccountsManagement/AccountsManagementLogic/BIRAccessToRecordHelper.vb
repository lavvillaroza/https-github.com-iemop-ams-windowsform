'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             BIRAcessToRecordHelper
'Orginal Author:         Lance Arjay Villaroza
'File Creation Date:     March 28, 2016
'Development Group:      Software Development and Support Division
'Description:            Class Settlement Notice Helper
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
Public Class BIRAccessToRecordHelper

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

    Private _objParticipantsList As New List(Of AMParticipants)
    Public ReadOnly Property objParticipantsList() As List(Of AMParticipants)
        Get
            Return _objParticipantsList
        End Get
    End Property

    Private _objTransactionYearList As New List(Of String)
    Public ReadOnly Property objTransactionYearList() As List(Of String)
        Get
            Return _objTransactionYearList
        End Get
    End Property

    Private _objWESMBill As New List(Of WESMBill)
    Public ReadOnly Property objWESMBill() As List(Of WESMBill)
        Get
            Return _objWESMBill
        End Get
    End Property

    Private _objWESMBillSalesAndPurchased As New List(Of WESMBillSalesAndPurchased)
    Public ReadOnly Property objWESMBillSalesAndPurchased() As List(Of WESMBillSalesAndPurchased)
        Get
            Return _objWESMBillSalesAndPurchased
        End Get
    End Property

    Private _objWESMBillSummary As New List(Of WESMBillSummary)
    Public ReadOnly Property objWESMBillSummary() As List(Of WESMBillSummary)
        Get
            Return _objWESMBillSummary
        End Get
    End Property

    Private _objCollectionList As New List(Of Collection)
    Public ReadOnly Property objCollectionList() As List(Of Collection)
        Get
            Return _objCollectionList
        End Get
    End Property

    Private _objCollectionAllocationList As New List(Of CollectionAllocation)
    Public ReadOnly Property objCollectionAllocationList() As List(Of CollectionAllocation)
        Get
            Return _objCollectionAllocationList
        End Get
    End Property

    Private _objOffsetARCollectionList As New List(Of ARCollection)
    Public ReadOnly Property objOffsetARCollectionList() As List(Of ARCollection)
        Get
            Return _objOffsetARCollectionList
        End Get
    End Property

    Private _objPaymentAllocationList As New List(Of APAllocation)
    Public ReadOnly Property objPaymentAllocationList() As List(Of APAllocation)
        Get
            Return _objPaymentAllocationList
        End Get
    End Property

    Private _objOffsetAPAllocationList As New List(Of APAllocation)
    Public ReadOnly Property objOffsetAPAllocationList() As List(Of APAllocation)
        Get
            Return _objOffsetAPAllocationList
        End Get
    End Property

    Private _objOfficialReceiptList As New List(Of OfficialReceiptMain)
    Public ReadOnly Property objOfficialReceiptList() As List(Of OfficialReceiptMain)
        Get
            Return _objOfficialReceiptList
        End Get
    End Property

    Public Sub New()
        Me._DataAccess = DAL.GetInstance()
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._BFactory = BusinessFactory.GetInstance
        Me._objParticipantsList = Me.WBillHelper.GetAMParticipants()
        Me._objTransactionYearList = Me.WBillHelper.GetAMTransactionYearList()
    End Sub

    Public Function GetParticipantsInvolvedInSelectedYear(ByVal SelectedYear As String) As List(Of AMParticipants)
        Dim ret As New List(Of AMParticipants)
        Me._objWESMBill = Me.WBillHelper.GetWESMBills(SelectedYear)

        Me._objWESMBillSalesAndPurchased = Me.WBillHelper.GetBIRWBSalesAndPurchased(SelectedYear)

        Me._objWESMBillSummary = Me.WBillHelper.GetWESMBillSummaryPerYear(SelectedYear)

        Me._objCollectionAllocationList = Me.WBillHelper.GetCollectionAllocationByYear(SelectedYear)
        Me._objCollectionList = Me.WBillHelper.GetCollectionsByYear(SelectedYear, Me.objCollectionAllocationList)
        Me._objOffsetARCollectionList = Me.WBillHelper.GetAMPaymentNewOffsetARByYear(SelectedYear)

        Me._objPaymentAllocationList = Me.WBillHelper.GetAMPaymentNewAPByYear(SelectedYear)
        Me._objOffsetAPAllocationList = Me.WBillHelper.GetAMPaymentNewOffsetAPByYear(SelectedYear)
        Me._objOfficialReceiptList = Me.WBillHelper.GetOfficialReceipt(SelectedYear)


        Dim IDNumberList As List(Of String) = (From x In Me.objWESMBillSalesAndPurchased _
                                               Select x.IDNumber.IDNumber Distinct).ToList()

        Dim oParticipantsList As List(Of AMParticipants) = (From x In Me.objParticipantsList _
                                                           Where IDNumberList.Contains(x.IDNumber) _
                                                           Select x).ToList()
        ret = oParticipantsList
        Return ret
    End Function

    Public ReportCreatedCount As Integer = 0

    Public Sub GenerateBIRAcessToRecordReport(ByVal TargetPathFolder As String, ByVal SelectedIDNumber As String, ByVal SelectedYear As String)
        Dim oParticipantInfo As AMParticipants = (From x In Me.objParticipantsList _
                                                  Where x.ParticipantID = SelectedIDNumber _
                                                  Select x).FirstOrDefault

        Dim oWESMBillSalesAndPurchased As List(Of WESMBillSalesAndPurchased) = (From x In Me.objWESMBillSalesAndPurchased _
                                                                                Where x.IDNumber.IDNumber = oParticipantInfo.IDNumber _                                                                                
                                                                                Select x).ToList
        If oWESMBillSalesAndPurchased.Count = 0 Then
            Exit Sub
        End If

        ReportCreatedCount += 1
        Dim oWESMBills As List(Of WESMBill) = (From x In Me.objWESMBill _
                                                Where x.IDNumber = oParticipantInfo.IDNumber _
                                                Select x).ToList()
        Dim oWESMBillSummary As List(Of WESMBillSummary) = (From x In Me.objWESMBillSummary _
                                                Where x.IDNumber.IDNumber = oParticipantInfo.IDNumber _
                                                Select x).ToList()
     
        Dim SalesOfMP As Object(,) = New Object(,) {}
        Dim PurchasesOfMP As Object(,) = New Object(,) {}                
        Dim MFPaid As Object(,) = New Object(,) {}
        Dim rowCountInSales As Integer = 0
        Dim rowCountInPurchases As Integer = 0
        Dim rowCountInMF As Integer = 0
        Dim i As Integer = 1


        'Sales of Vatable Generator
        Dim GetSalesOfMP = (From x In oWESMBillSalesAndPurchased _
                            Where (x.VatableSales + x.ZeroRatedSales + x.ZeroRatedEcozone + x.VatablePurchases + x.ZeroRatedPurchases) > 0 _
                            Or (x.VATonSales + x.VATonPurchases) > 0 _
                            Select x).ToList()

       

        rowCountInSales = GetSalesOfMP.Count
        If rowCountInSales > 0 Then
            ReDim SalesOfMP(rowCountInSales + 2, 16)
            SalesOfMP(0, 0) = "A. Power Sold/Traded to " & AMModule.CompanyShortName
            SalesOfMP(1, 0) = "BILLING_PERIOD"
            SalesOfMP(1, 1) = "INVOICE_NO"
            SalesOfMP(1, 2) = "INVOICE_DATE"
            SalesOfMP(1, 3) = "VATABLE_SALES"
            SalesOfMP(1, 4) = "ZERO_RATED_SALES"
            SalesOfMP(1, 5) = "ZERO_RATED_ECOZONE"
            SalesOfMP(1, 6) = "VATABLE_PURCHASES"
            SalesOfMP(1, 7) = "ZERO_RATED_PURCHASES"
            SalesOfMP(1, 8) = "TOTAL_ENERGY"
            SalesOfMP(1, 9) = "VAT_ON_SALES"
            SalesOfMP(1, 10) = "VAT_ON_PURCHASES"
            SalesOfMP(1, 11) = "TOTAL_VAT"
            SalesOfMP(1, 12) = "TOTAL_BILLING"
            SalesOfMP(1, 13) = ""
            SalesOfMP(1, 14) = "REMITTANCE(ENERGY)"
            SalesOfMP(1, 15) = "REMITTANCE(VAT)"
            SalesOfMP(1, 16) = "REMITTANCE_TOTAL"

            SalesOfMP(UBound(SalesOfMP, 1), 0) = "TOTAL"

            For Each Item In GetSalesOfMP
                Dim getWESMBill = (From x In oWESMBills Where x.InvoiceNumber = Item.InvoiceNumber Select x).FirstOrDefault

                Dim getWESMBillEnergy = (From x In oWESMBills Where x.InvoiceNumber = Item.InvoiceNumber And x.ChargeType = EnumChargeType.E Select x).FirstOrDefault
                Dim getWESMBillVAT = (From x In oWESMBills Where x.InvoiceNumber = Item.InvoiceNumber And x.ChargeType = EnumChargeType.EV Select x).FirstOrDefault
                Dim getWESMBIllSUmmaryEnergy = (From x In oWESMBillSummary Where x.INVDMCMNo = Item.InvoiceNumber And x.ChargeType = EnumChargeType.E Select x).FirstOrDefault
                Dim getWESMBIllSUmmaryVAT = (From x In oWESMBillSummary Where x.INVDMCMNo = Item.InvoiceNumber And x.ChargeType = EnumChargeType.EV Select x).FirstOrDefault

                i += 1

                Dim totalEnergy As Decimal = Item.VatableSales + Item.ZeroRatedSales + Item.ZeroRatedEcozone + Item.VatablePurchases + Item.ZeroRatedPurchases
                Dim totalVat As Decimal = Item.VATonSales + Item.VATonPurchases

                If totalEnergy > 0 Then
                    SalesOfMP(i, 3) = Item.VatableSales
                    SalesOfMP(i, 4) = Item.ZeroRatedSales
                    SalesOfMP(i, 5) = Item.ZeroRatedEcozone
                    SalesOfMP(i, 6) = Item.VatablePurchases
                    SalesOfMP(i, 7) = Item.ZeroRatedPurchases
                    SalesOfMP(i, 8) = Item.VatableSales + Item.ZeroRatedSales + Item.ZeroRatedEcozone + Item.VatablePurchases + Item.ZeroRatedPurchases
                    If Not getWESMBIllSUmmaryEnergy Is Nothing Then
                        SalesOfMP(i, 14) = Math.Abs(getWESMBillEnergy.Amount - getWESMBIllSUmmaryEnergy.EndingBalance)
                    Else
                        SalesOfMP(i, 14) = Math.Abs(getWESMBillEnergy.Amount)
                    End If
               
                End If


                If totalVat > 0 Then
                    SalesOfMP(i, 9) = Item.VATonSales
                    SalesOfMP(i, 10) = Item.VATonPurchases
                    SalesOfMP(i, 11) = Item.VATonSales + Item.VATonPurchases
                    If Not getWESMBIllSUmmaryVAT Is Nothing Then
                        SalesOfMP(i, 15) = Math.Abs(getWESMBillVAT.Amount - getWESMBIllSUmmaryVAT.EndingBalance)
                    Else
                        SalesOfMP(i, 15) = Math.Abs(getWESMBillVAT.Amount)
                    End If
                End If

                If totalEnergy > 0 Or totalVat > 0 Then
                    SalesOfMP(i, 0) = MonthName(getWESMBill.DueDate.Month)
                    SalesOfMP(i, 1) = getWESMBill.InvoiceNumber
                    SalesOfMP(i, 2) = getWESMBill.InvoiceDate
                    SalesOfMP(i, 12) = CDec(SalesOfMP(i, 8)) + CDec(SalesOfMP(i, 11))
                    SalesOfMP(i, 13) = ""


                    SalesOfMP(i, 16) = CDec(SalesOfMP(i, 14)) + CDec(SalesOfMP(i, 15))

                    SalesOfMP(UBound(SalesOfMP, 1), 3) = CDec(SalesOfMP(UBound(SalesOfMP, 1), 3)) + CDec(SalesOfMP(i, 3))
                    SalesOfMP(UBound(SalesOfMP, 1), 4) = CDec(SalesOfMP(UBound(SalesOfMP, 1), 4)) + CDec(SalesOfMP(i, 4))
                    SalesOfMP(UBound(SalesOfMP, 1), 5) = CDec(SalesOfMP(UBound(SalesOfMP, 1), 5)) + CDec(SalesOfMP(i, 5))
                    SalesOfMP(UBound(SalesOfMP, 1), 6) = CDec(SalesOfMP(UBound(SalesOfMP, 1), 6)) + CDec(SalesOfMP(i, 6))
                    SalesOfMP(UBound(SalesOfMP, 1), 7) = CDec(SalesOfMP(UBound(SalesOfMP, 1), 7)) + CDec(SalesOfMP(i, 7))
                    SalesOfMP(UBound(SalesOfMP, 1), 8) = CDec(SalesOfMP(UBound(SalesOfMP, 1), 8)) + CDec(SalesOfMP(i, 8))
                    SalesOfMP(UBound(SalesOfMP, 1), 9) = CDec(SalesOfMP(UBound(SalesOfMP, 1), 9)) + CDec(SalesOfMP(i, 9))
                    SalesOfMP(UBound(SalesOfMP, 1), 10) = CDec(SalesOfMP(UBound(SalesOfMP, 1), 10)) + CDec(SalesOfMP(i, 10))
                    SalesOfMP(UBound(SalesOfMP, 1), 11) = CDec(SalesOfMP(UBound(SalesOfMP, 1), 11)) + CDec(SalesOfMP(i, 11))
                    SalesOfMP(UBound(SalesOfMP, 1), 12) = CDec(SalesOfMP(UBound(SalesOfMP, 1), 12)) + CDec(SalesOfMP(i, 12))
                    SalesOfMP(UBound(SalesOfMP, 1), 14) = CDec(SalesOfMP(UBound(SalesOfMP, 1), 14)) + CDec(SalesOfMP(i, 14))
                    SalesOfMP(UBound(SalesOfMP, 1), 15) = CDec(SalesOfMP(UBound(SalesOfMP, 1), 15)) + CDec(SalesOfMP(i, 15))
                    SalesOfMP(UBound(SalesOfMP, 1), 16) = CDec(SalesOfMP(UBound(SalesOfMP, 1), 16)) + CDec(SalesOfMP(i, 16))
                End If

            Next
        Else
            ReDim SalesOfMP(rowCountInSales + 2, 16)
            SalesOfMP(0, 0) = "A. Power Sold/Traded to " & AMModule.CompanyShortName
            SalesOfMP(1, 0) = "BILLING_PERIOD"
            SalesOfMP(1, 1) = "INVOICE_NO"
            SalesOfMP(1, 2) = "INVOICE_DATE"
            SalesOfMP(1, 3) = "VATABLE_SALES"
            SalesOfMP(1, 4) = "ZERO_RATED_SALES"
            SalesOfMP(1, 5) = "ZERO_RATED_ECOZONE"
            SalesOfMP(1, 6) = "VATABLE_PURCHASES"
            SalesOfMP(1, 7) = "ZERO_RATED_PURCHASES"
            SalesOfMP(1, 8) = "TOTAL_ENERGY"
            SalesOfMP(1, 9) = "VAT_ON_SALES"
            SalesOfMP(1, 10) = "VAT_ON_PURCHASES"
            SalesOfMP(1, 11) = "TOTAL_VAT"
            SalesOfMP(1, 12) = "TOTAL_BILLING"
            SalesOfMP(1, 13) = ""
            SalesOfMP(1, 14) = "REMITTANCE(ENERGY)"
            SalesOfMP(1, 15) = "REMITTANCE(VAT)"
            SalesOfMP(1, 16) = "REMITTANCE_TOTAL"

            SalesOfMP(UBound(SalesOfMP, 1), 0) = "TOTAL"
        End If

        'Purchases of Vatable Generators
        Dim GetPurchasesOfMP = (From x In oWESMBillSalesAndPurchased _
                            Where (x.VatableSales + x.ZeroRatedSales + x.ZeroRatedEcozone + x.VatablePurchases + x.ZeroRatedPurchases) < 0 _
                            Or (x.VATonSales + x.VATonPurchases) < 0 _
                            Select x).ToList()

        rowCountInPurchases = GetPurchasesOfMP.Count
        If rowCountInPurchases > 0 Then
            ReDim PurchasesOfMP(rowCountInPurchases + 2, 16)
            PurchasesOfMP(0, 0) = "B. Power Purchased by " & oParticipantInfo.FullName            
            PurchasesOfMP(1, 0) = "BILLING_PERIOD"
            PurchasesOfMP(1, 1) = "INVOICE_NO"
            PurchasesOfMP(1, 2) = "INVOICE_DATE"
            PurchasesOfMP(1, 3) = "VATABLE_SALES"
            PurchasesOfMP(1, 4) = "ZERO_RATED_SALES"
            PurchasesOfMP(1, 5) = "ZERO_RATED_ECOZONE"
            PurchasesOfMP(1, 6) = "VATABLE_PURCHASES"
            PurchasesOfMP(1, 7) = "ZERO_RATED_PURCHASES"
            PurchasesOfMP(1, 8) = "TOTAL_ENERGY"
            PurchasesOfMP(1, 9) = "VAT_ON_SALES"
            PurchasesOfMP(1, 10) = "VAT_ON_PURCHASES"
            PurchasesOfMP(1, 11) = "TOTAL_VAT"
            PurchasesOfMP(1, 12) = "TOTAL_BILLING"
            PurchasesOfMP(1, 13) = ""
            PurchasesOfMP(1, 14) = "COLLECTION(ENERGY)"
            PurchasesOfMP(1, 15) = "COLLECTION(VAT)"
            PurchasesOfMP(1, 16) = "COLLECTION_TOTAL"

            PurchasesOfMP(UBound(PurchasesOfMP, 1), 0) = "TOTAL"

            i = 1
            For Each Item In GetPurchasesOfMP
                Dim getWESMBill = (From x In oWESMBills Where x.InvoiceNumber = Item.InvoiceNumber Select x).FirstOrDefault
                Dim getWESMBillEnergy = (From x In oWESMBills Where x.InvoiceNumber = Item.InvoiceNumber And x.ChargeType = EnumChargeType.E Select x).FirstOrDefault
                Dim getWESMBillVAT = (From x In oWESMBills Where x.InvoiceNumber = Item.InvoiceNumber And x.ChargeType = EnumChargeType.EV Select x).FirstOrDefault
                Dim getWESMBIllSUmmaryEnergy = (From x In oWESMBillSummary Where x.INVDMCMNo = Item.InvoiceNumber And x.ChargeType = EnumChargeType.E Select x).FirstOrDefault
                Dim getWESMBIllSUmmaryVAT = (From x In oWESMBillSummary Where x.INVDMCMNo = Item.InvoiceNumber And x.ChargeType = EnumChargeType.EV Select x).FirstOrDefault

                i += 1

                Dim totalEnergy As Decimal = Item.VatableSales + Item.ZeroRatedSales + Item.ZeroRatedEcozone + Item.VatablePurchases + Item.ZeroRatedPurchases
                Dim totalVat As Decimal = Item.VATonSales + Item.VATonPurchases
                If totalEnergy < 0 Then
                    PurchasesOfMP(i, 3) = Item.VatableSales
                    PurchasesOfMP(i, 4) = Item.ZeroRatedSales
                    PurchasesOfMP(i, 5) = Item.ZeroRatedEcozone
                    PurchasesOfMP(i, 6) = Item.VatablePurchases
                    PurchasesOfMP(i, 7) = Item.ZeroRatedPurchases
                    PurchasesOfMP(i, 8) = Item.VatableSales + Item.ZeroRatedSales + Item.ZeroRatedEcozone + Item.VatablePurchases + Item.ZeroRatedPurchases
                    If Not getWESMBIllSUmmaryEnergy Is Nothing Then
                        PurchasesOfMP(i, 14) = Math.Abs(getWESMBillEnergy.Amount - getWESMBIllSUmmaryEnergy.EndingBalance) * -1
                    Else
                        PurchasesOfMP(i, 14) = Math.Abs(getWESMBillEnergy.Amount) * -1
                    End If
                End If

                If totalVat < 0 Then
                    PurchasesOfMP(i, 9) = Item.VATonSales
                    PurchasesOfMP(i, 10) = Item.VATonPurchases
                    PurchasesOfMP(i, 11) = Item.VATonSales + Item.VATonPurchases
                    If Not getWESMBIllSUmmaryVAT Is Nothing Then
                        PurchasesOfMP(i, 15) = Math.Abs(getWESMBillVAT.Amount - getWESMBIllSUmmaryVAT.EndingBalance) * -1
                    Else
                        PurchasesOfMP(i, 15) = Math.Abs(getWESMBillVAT.Amount) * -1
                    End If
                End If

                If totalEnergy < 0 Or totalVat < 0 Then
                    PurchasesOfMP(i, 0) = MonthName(getWESMBill.DueDate.Month)
                    PurchasesOfMP(i, 1) = getWESMBill.InvoiceNumber
                    PurchasesOfMP(i, 2) = getWESMBill.InvoiceDate
                    PurchasesOfMP(i, 12) = CDec(PurchasesOfMP(i, 8)) + CDec(PurchasesOfMP(i, 11))
                    PurchasesOfMP(i, 13) = ""


                    PurchasesOfMP(i, 16) = CDec(PurchasesOfMP(i, 14)) + CDec(PurchasesOfMP(i, 15))

                    PurchasesOfMP(UBound(PurchasesOfMP, 1), 3) = CDec(PurchasesOfMP(UBound(PurchasesOfMP, 1), 3)) + CDec(PurchasesOfMP(i, 3))
                    PurchasesOfMP(UBound(PurchasesOfMP, 1), 4) = CDec(PurchasesOfMP(UBound(PurchasesOfMP, 1), 4)) + CDec(PurchasesOfMP(i, 4))
                    PurchasesOfMP(UBound(PurchasesOfMP, 1), 5) = CDec(PurchasesOfMP(UBound(PurchasesOfMP, 1), 5)) + CDec(PurchasesOfMP(i, 5))
                    PurchasesOfMP(UBound(PurchasesOfMP, 1), 6) = CDec(PurchasesOfMP(UBound(PurchasesOfMP, 1), 6)) + CDec(PurchasesOfMP(i, 6))
                    PurchasesOfMP(UBound(PurchasesOfMP, 1), 7) = CDec(PurchasesOfMP(UBound(PurchasesOfMP, 1), 7)) + CDec(PurchasesOfMP(i, 7))
                    PurchasesOfMP(UBound(PurchasesOfMP, 1), 8) = CDec(PurchasesOfMP(UBound(PurchasesOfMP, 1), 8)) + CDec(PurchasesOfMP(i, 8))
                    PurchasesOfMP(UBound(PurchasesOfMP, 1), 9) = CDec(PurchasesOfMP(UBound(PurchasesOfMP, 1), 9)) + CDec(PurchasesOfMP(i, 9))
                    PurchasesOfMP(UBound(PurchasesOfMP, 1), 10) = CDec(PurchasesOfMP(UBound(PurchasesOfMP, 1), 10)) + CDec(PurchasesOfMP(i, 10))
                    PurchasesOfMP(UBound(PurchasesOfMP, 1), 11) = CDec(PurchasesOfMP(UBound(PurchasesOfMP, 1), 11)) + CDec(PurchasesOfMP(i, 11))
                    PurchasesOfMP(UBound(PurchasesOfMP, 1), 12) = CDec(PurchasesOfMP(UBound(PurchasesOfMP, 1), 12)) + CDec(PurchasesOfMP(i, 12))
                    PurchasesOfMP(UBound(PurchasesOfMP, 1), 14) = CDec(PurchasesOfMP(UBound(PurchasesOfMP, 1), 14)) + CDec(PurchasesOfMP(i, 14))
                    PurchasesOfMP(UBound(PurchasesOfMP, 1), 15) = CDec(PurchasesOfMP(UBound(PurchasesOfMP, 1), 15)) + CDec(PurchasesOfMP(i, 15))
                    PurchasesOfMP(UBound(PurchasesOfMP, 1), 16) = CDec(PurchasesOfMP(UBound(PurchasesOfMP, 1), 16)) + CDec(PurchasesOfMP(i, 16))
                End If
                
            Next
        Else
            ReDim PurchasesOfMP(rowCountInPurchases + 2, 16)
            PurchasesOfMP(0, 0) = "B. Power Purchased by " & oParticipantInfo.FullName
            PurchasesOfMP(1, 0) = "BILLING_PERIOD"
            PurchasesOfMP(1, 1) = "INVOICE_NO"
            PurchasesOfMP(1, 2) = "INVOICE_DATE"
            PurchasesOfMP(1, 3) = "VATABLE_SALES"
            PurchasesOfMP(1, 4) = "ZERO_RATED_SALES"
            PurchasesOfMP(1, 5) = "ZERO_RATED_ECOZONE"
            PurchasesOfMP(1, 6) = "VATABLE_PURCHASES"
            PurchasesOfMP(1, 7) = "ZERO_RATED_PURCHASES"
            PurchasesOfMP(1, 8) = "TOTAL_ENERGY"
            PurchasesOfMP(1, 9) = "VAT_ON_SALES"
            PurchasesOfMP(1, 10) = "VAT_ON_PURCHASES"
            PurchasesOfMP(1, 11) = "TOTAL_VAT"
            PurchasesOfMP(1, 12) = "TOTAL_BILLING"
            PurchasesOfMP(1, 13) = ""
            PurchasesOfMP(1, 14) = "COLLECTION(ENERGY)"
            PurchasesOfMP(1, 15) = "COLLECTION(VAT)"
            PurchasesOfMP(1, 16) = "COLLECTION_TOTAL"

            PurchasesOfMP(UBound(PurchasesOfMP, 1), 0) = "TOTAL"
        End If
        
        
        'AMOUNT PAID BY GENERATORS (FOR MARKET FEES)
        Dim getWESMBillSummaryMF = (From x In oWESMBillSummary Where x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV Select x).ToList
        Dim getWESMBillSummaryMFInv = getWESMBillSummaryMF.Select(Function(x) x.INVDMCMNo).Distinct.ToList()

        'AMOUNT PAID BY GENERATORS (FOR MARKET FEES)        
        Dim GetAmountPaidByMP = (From x In objCollectionAllocationList _
                                 Join y In objCollectionList On y.CollectionNumber Equals x.CollectionNumber _
                                 Where x.WESMBillSummaryNo.IDNumber.IDNumber = oParticipantInfo.IDNumber _
                                         And (x.CollectionType = EnumCollectionType.MarketFees _
                                              Or x.CollectionType = EnumCollectionType.DefaultInterestOnMF _
                                              Or x.CollectionType = EnumCollectionType.VatOnMarketFees _
                                              Or x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF) _
                                  Select x.AllocationDate, x.WESMBillSummaryNo.INVDMCMNo, y.ORNo, x.Amount, y.CollectedAmount, y.CollectionDate).ToList

        Dim GetAmountPaidByMPWHTax = (From x In objCollectionAllocationList _
                                            Join y In objCollectionList On y.CollectionNumber Equals x.CollectionNumber _
                                            Where x.WESMBillSummaryNo.IDNumber.IDNumber = oParticipantInfo.IDNumber _
                                                And (x.CollectionType = EnumCollectionType.WithholdingTaxOnMF _
                                                    Or x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest) _
                                            Select x.AllocationDate, x.WESMBillSummaryNo.INVDMCMNo, y.ORNo, x.Amount, y.CollectedAmount, y.CollectionDate).ToList

        Dim GetAmountPaidByMPWHVAT = (From x In objCollectionAllocationList _
                                            Join y In objCollectionList On y.CollectionNumber Equals x.CollectionNumber _
                                            Where x.WESMBillSummaryNo.IDNumber.IDNumber = oParticipantInfo.IDNumber _
                                                And (x.CollectionType = EnumCollectionType.WithholdingVatOnMF _
                                                    Or x.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest) _
                                            Select x.AllocationDate, x.WESMBillSummaryNo.INVDMCMNo, y.ORNo, x.Amount, y.CollectedAmount, y.CollectionDate).ToList


        Dim GetTotalAmountPaidByMP = (From x In GetAmountPaidByMP _
                                      Group By x.AllocationDate, x.INVDMCMNo, x.ORNo, x.CollectedAmount, x.CollectionDate _
                                      Into TotalAmount = Sum(x.Amount)).ToList

        Dim GetTotalAmountPaidByMPWHTax = (From x In GetAmountPaidByMPWHTax _
                                                    Group By x.AllocationDate, x.INVDMCMNo, x.ORNo, x.CollectedAmount, x.CollectionDate _
                                                    Into TotalAmount = Sum(x.Amount)).ToList

        Dim GetTotalAmountPaidByMPWHVAT = (From x In GetAmountPaidByMPWHVAT _
                                                    Group By x.AllocationDate, x.INVDMCMNo, x.ORNo, x.CollectedAmount, x.CollectionDate _
                                                    Into TotalAmount = Sum(x.Amount)).ToList

        Dim GetOffsetAmountPaidByMP = (From x In objOffsetARCollectionList _
                                       Where x.IDNumber = oParticipantInfo.IDNumber _
                                        And (x.CollectionType = EnumCollectionType.MarketFees _
                                             Or x.CollectionType = EnumCollectionType.DefaultInterestOnMF _
                                             Or x.CollectionType = EnumCollectionType.VatOnMarketFees _
                                             Or x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF) _
                                    Select x.AllocationDate, x.InvoiceNumber, x.AllocationAmount, x.WESMBillSummaryNo).ToList

        Dim GetOffsetAmountPaidByMPWHTax = (From x In objOffsetARCollectionList _
                                            Where x.IDNumber = oParticipantInfo.IDNumber _
                                            And (x.CollectionType = EnumCollectionType.WithholdingTaxOnMF _
                                                    Or x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest) _
                                            Select x.AllocationDate, x.InvoiceNumber, x.AllocationAmount, x.WESMBillSummaryNo).ToList

        Dim GetOffsetAmountPaidByMPWHVat = (From x In objOffsetARCollectionList _
                                            Where x.IDNumber = oParticipantInfo.IDNumber _
                                            And (x.CollectionType = EnumCollectionType.WithholdingVatOnMF _
                                                    Or x.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest) _
                                            Select x.AllocationDate, x.InvoiceNumber, x.AllocationAmount, x.WESMBillSummaryNo).ToList


        Dim GetOffsetTotalAmountPaidByMP = (From x In GetOffsetAmountPaidByMP _
                                            Group By x.AllocationDate, x.InvoiceNumber, x.WESMBillSummaryNo _
                                            Into TotalAmount = Sum(x.AllocationAmount)).ToList

        Dim GetOffsetTotalAmountPaidByMPWHTax = (From x In GetOffsetAmountPaidByMPWHTax _
                                                Group By x.AllocationDate, x.InvoiceNumber, x.WESMBillSummaryNo _
                                                Into TotalAmount = Sum(x.AllocationAmount)).ToList

        Dim GetOffsetTotalAmountPaidByMPWHVAT = (From x In GetOffsetAmountPaidByMPWHVat _
                                                Group By x.AllocationDate, x.InvoiceNumber, x.WESMBillSummaryNo _
                                                Into TotalAmount = Sum(x.AllocationAmount)).ToList

        Dim GetAmountPaidByIEMOPInMF = (From x In objPaymentAllocationList _
                                 Where x.IDNumber = oParticipantInfo.IDNumber _
                                         And (x.PaymentType = EnumPaymentNewType.MarketFees _
                                              Or x.PaymentType = EnumPaymentNewType.DefaultInterestOnMF _
                                              Or x.PaymentType = EnumPaymentNewType.VatOnMarketFees _
                                              Or x.PaymentType = EnumPaymentNewType.DefaultInterestOnVatOnMF) _
                                  Select x.AllocationDate, x.InvoiceNumber, x.AllocationAmount).ToList

        Dim GetAmountPaidByIEMOPInMFWHTax = (From x In objPaymentAllocationList _
                                 Where x.IDNumber = oParticipantInfo.IDNumber _
                                                And (x.PaymentType = EnumPaymentNewType.WithholdingTaxOnMF _
                                                    Or x.PaymentType = EnumPaymentNewType.WithholdingTaxOnDefaultInterest) _
                                            Select x.AllocationDate, x.InvoiceNumber, x.AllocationAmount).ToList

        Dim GetAmountPaidByIEMOPInMFWHVAT = (From x In objPaymentAllocationList _
                                 Where x.IDNumber = oParticipantInfo.IDNumber _
                                                And (x.PaymentType = EnumPaymentNewType.WithholdingVatOnMF _
                                                    Or x.PaymentType = EnumPaymentNewType.WithholdingVatOnDefaultInterest) _
                                            Select x.AllocationDate, x.InvoiceNumber, x.AllocationAmount).ToList

        Dim GetTotalAmountPaidByIEMOPInMF = (From x In GetAmountPaidByIEMOPInMF _
                                     Group By x.AllocationDate, x.InvoiceNumber _
                                     Into TotalAmount = Sum(x.AllocationAmount)).ToList

        Dim GetTotalAmountPaidByIEMOPInMFWHTax = (From x In GetAmountPaidByIEMOPInMFWHTax _
                                                Group By x.AllocationDate, x.InvoiceNumber _
                                                Into TotalAmount = Sum(x.AllocationAmount)).ToList

        Dim GetTotalAmountPaidByIEMOPInMFWHVAT = (From x In GetAmountPaidByIEMOPInMFWHVAT _
                                                Group By x.AllocationDate, x.InvoiceNumber _
                                                Into TotalAmount = Sum(x.AllocationAmount)).ToList


        rowCountInMF = getWESMBillSummaryMFInv.Count

        If rowCountInMF > 0 Then            
            ReDim MFPaid(rowCountInMF + 2, 6)
            MFPaid(0, 0) = "C. Market Fees"
            MFPaid(1, 0) = "BILLING_PERIOD"
            MFPaid(1, 1) = "INVOICE_NO"
            MFPaid(1, 2) = "INVOICE_DATE"
            MFPaid(1, 3) = "MARKET FEES"
            MFPaid(1, 4) = "OUTPUTTAX"
            MFPaid(1, 5) = "TOTAL AMOUNT PAID"
            MFPaid(1, 6) = "DATE PAID"

            MFPaid(UBound(MFPaid, 1), 0) = "TOTAL"

            i = 1
            For Each item In getWESMBillSummaryMFInv
                Dim getWESMBillMF = (From x In oWESMBills Where x.InvoiceNumber = item And x.ChargeType = EnumChargeType.MF Select x).FirstOrDefault
                Dim getWESMBillMFV = (From x In oWESMBills Where x.InvoiceNumber = item And x.ChargeType = EnumChargeType.MFV Select x).FirstOrDefault

                Dim getWESMBillSummMF = (From x In getWESMBillSummaryMF Where x.INVDMCMNo = item And x.ChargeType = EnumChargeType.MF Select x).FirstOrDefault
                Dim getWESMBillSummMFV = (From x In getWESMBillSummaryMF Where x.INVDMCMNo = item And x.ChargeType = EnumChargeType.MFV Select x).FirstOrDefault
                Dim getWESMBill = (From x In oWESMBills Where x.InvoiceNumber = item Select x).ToList()
                i += 1
                If Not getWESMBillSummMF Is Nothing Then
                    MFPaid(i, 0) = MonthName(getWESMBillSummMF.DueDate.Month)
                    MFPaid(i, 6) = getWESMBillSummMF.NewDueDate.ToShortDateString()
                Else
                    MFPaid(i, 0) = MonthName(getWESMBillSummMFV.DueDate.Month)
                    MFPaid(i, 6) = getWESMBillSummMFV.NewDueDate.ToShortDateString()
                End If

                MFPaid(i, 1) = getWESMBill.Select(Function(x) x.InvoiceNumber).FirstOrDefault
                MFPaid(i, 2) = getWESMBill.Select(Function(x) x.InvoiceDate).FirstOrDefault
                If Not getWESMBillMF Is Nothing Then
                    MFPaid(i, 3) = getWESMBillMF.Amount
                Else
                    MFPaid(i, 3) = 0
                End If
                If Not getWESMBillMFV Is Nothing Then
                    MFPaid(i, 4) = getWESMBillMFV.Amount
                Else
                    MFPaid(i, 4) = 0
                End If

                If (CDec(MFPaid(i, 3)) + CDec(MFPaid(i, 4))) > 0 Then
                    Dim TotalAmountPaidByIEMOPMF = (From x In GetTotalAmountPaidByIEMOPInMF Where x.InvoiceNumber = item Select x.TotalAmount).Sum()
                    Dim TotalAmountPaidByIEMOPWHTax = (From x In GetTotalAmountPaidByIEMOPInMFWHTax Where x.InvoiceNumber = item Select x.TotalAmount).Sum()
                    Dim TotalAmountPaidByIEMOPWHVAT = (From x In GetTotalAmountPaidByIEMOPInMFWHVAT Where x.InvoiceNumber = item Select x.TotalAmount).Sum()

                    MFPaid(i, 5) = Math.Abs(TotalAmountPaidByIEMOPMF + TotalAmountPaidByIEMOPWHTax + TotalAmountPaidByIEMOPWHVAT)
                Else
                    Dim TotalAmountPaidByMPMF = (From x In GetTotalAmountPaidByMP Where x.INVDMCMNo = item Select x.TotalAmount).Sum()
                    Dim TotalAmountPaidByMPWHTax = (From x In GetTotalAmountPaidByMPWHTax Where x.INVDMCMNo = item Select x.TotalAmount).Sum()
                    Dim TotalAmountPaidByMPWHVAT = (From x In GetTotalAmountPaidByMPWHVAT Where x.INVDMCMNo = item Select x.TotalAmount).Sum()
                    Dim OffsetTotalAmountPaidByMP = (From x In GetOffsetTotalAmountPaidByMP Where x.InvoiceNumber = item Select x.TotalAmount).Sum()
                    Dim OffsetTotalAmountPaidByMPWHTax = (From x In GetOffsetTotalAmountPaidByMPWHTax Where x.InvoiceNumber = item Select x.TotalAmount).Sum()
                    Dim OffsetTotalAmountPaidByMPWHVAT = (From x In GetOffsetTotalAmountPaidByMPWHVAT Where x.InvoiceNumber = item Select x.TotalAmount).Sum()

                    MFPaid(i, 5) = Math.Abs(TotalAmountPaidByMPMF + TotalAmountPaidByMPWHTax + TotalAmountPaidByMPWHVAT + OffsetTotalAmountPaidByMP + OffsetTotalAmountPaidByMPWHTax + OffsetTotalAmountPaidByMPWHVAT) * -1
                End If

                MFPaid(UBound(MFPaid, 1), 3) = CDec(MFPaid(UBound(MFPaid, 1), 3)) + CDec(MFPaid(i, 3))
                MFPaid(UBound(MFPaid, 1), 4) = CDec(MFPaid(UBound(MFPaid, 1), 4)) + CDec(MFPaid(i, 4))
                MFPaid(UBound(MFPaid, 1), 5) = CDec(MFPaid(UBound(MFPaid, 1), 5)) + CDec(MFPaid(i, 5))

            Next
        Else
            ReDim MFPaid(rowCountInMF + 2, 6)
            MFPaid(0, 0) = "C. Market Fees"
            MFPaid(1, 0) = "BILLING_PERIOD"
            MFPaid(1, 1) = "INVOICE_NO"
            MFPaid(1, 2) = "INVOICE_DATE"
            MFPaid(1, 3) = "MARKET FEES"
            MFPaid(1, 4) = "OUTPUTTAX"
            MFPaid(1, 5) = "TOTAL AMOUNT PAID"
            MFPaid(1, 6) = "DATE PAID"

            MFPaid(UBound(MFPaid, 1), 0) = "TOTAL"
        End If
        
        'Generate Excel File
        Me.SaveToExcelGen(TargetPathFolder, SelectedYear, oParticipantInfo, _
                          SalesOfMP, PurchasesOfMP, MFPaid)

    End Sub

    Public Sub SaveToExcelGen(ByVal TargetPathFolder As String, ByVal SelectedYear As String, ByVal ParticipantInfo As AMParticipants, _
                              ByVal sales As Object(,), ByVal purchases As Object(,), ByVal amountPaid As Object(,))

        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlContentHeader As Excel.Range
        Dim xlSalesOfMP As Excel.Range
        Dim xlPurchasesOfMP As Excel.Range
        Dim xlAmountPaid As Excel.Range

        Dim ContentHeader As Object(,) = New Object(,) {}
        Dim misValue As Object = System.Reflection.Missing.Value
        Dim TemplatePathFile = AppDomain.CurrentDomain.BaseDirectory & "Excel_Template\BIRAccessToRecord.xltm"
        Dim RowIndx As Integer = 0

        xlAmountPaid = Nothing
        xlPurchasesOfMP = Nothing
        xlSalesOfMP = Nothing
        xlContentHeader = Nothing
        xlRowRange2 = Nothing
        xlRowRange1 = Nothing

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add(TemplatePathFile)
        xlWorkSheet = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        'xlWorkSheet.Name = ParticipantInfo.ParticipantID & "_BATR_" & SelectedYear
        Try
            '********************************************* Supply Content Header
            ReDim ContentHeader(4, 0)
            ContentHeader(0, 0) = AMModule.CompanyFullName
            ContentHeader(1, 0) = "MP Name: " & ParticipantInfo.FullName.ToString()
            ContentHeader(2, 0) = "MP ID No.: " & ParticipantInfo.IDNumber.ToString()
            ContentHeader(3, 0) = "Transactions for the year " & SelectedYear
            RowIndx = 1
            xlRowRange1 = DirectCast(xlWorkSheet.Cells(RowIndx, 1), Excel.Range)
            RowIndx += 3
            xlRowRange2 = DirectCast(xlWorkSheet.Cells(4, 1), Excel.Range)
            xlContentHeader = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
            xlContentHeader.Value = ContentHeader

            '********************************************* Supply Sales of Market Participant           
            If UBound(sales, 1) > 0 Then
                RowIndx += 2
                xlRowRange1 = DirectCast(xlWorkSheet.Cells(RowIndx, 1), Excel.Range)
                RowIndx += UBound(sales, 1)
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(RowIndx, UBound(sales, 2) + 1), Excel.Range)
                xlSalesOfMP = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlSalesOfMP.Value = sales
            End If
            '********************************************* Supply Purchases of Market Participant
            If UBound(purchases, 1) > 0 Then
                RowIndx += 2
                xlRowRange1 = DirectCast(xlWorkSheet.Cells(RowIndx, 1), Excel.Range)
                RowIndx += UBound(purchases, 1)
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(RowIndx, UBound(purchases, 2) + 1), Excel.Range)
                xlPurchasesOfMP = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlPurchasesOfMP.Value = purchases
            End If
            '********************************************* Supply Amount Paid  of Market Participant
            If UBound(amountPaid, 1) > 0 Then
                RowIndx += 2
                xlRowRange1 = DirectCast(xlWorkSheet.Cells(RowIndx, 1), Excel.Range)
                RowIndx += UBound(amountPaid, 1)
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(RowIndx, UBound(amountPaid, 2) + 1), Excel.Range)
                xlAmountPaid = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlAmountPaid.Value = amountPaid
            End If

            Dim FileName As String = ParticipantInfo.ParticipantID & "_BATR_" & SelectedYear
            xlWorkBook.SaveAs(TargetPathFolder & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbookMacroEnabled, misValue, misValue, misValue, misValue,
                        Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)

            xlWorkBook.Close(False)
            xlApp.Quit()
            releaseObject(CObj(xlAmountPaid))
            releaseObject(CObj(xlPurchasesOfMP))
            releaseObject(CObj(xlSalesOfMP))
            releaseObject(CObj(xlContentHeader))
            releaseObject(CObj(xlRowRange2))
            releaseObject(CObj(xlRowRange1))
            releaseObject(CObj(xlWorkSheet))
            releaseObject(CObj(xlWorkBook))
            releaseObject(CObj(xlApp))

        Catch ex As Exception
            releaseObject(CObj(xlAmountPaid))
            releaseObject(CObj(xlPurchasesOfMP))
            releaseObject(CObj(xlSalesOfMP))
            releaseObject(CObj(xlContentHeader))
            releaseObject(CObj(xlRowRange2))
            releaseObject(CObj(xlRowRange1))
            releaseObject(CObj(xlWorkSheet))
            releaseObject(CObj(xlWorkBook))
            releaseObject(CObj(xlApp))
            Throw New Exception(ex.Message)
        End Try
    End Sub
    
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub
End Class
