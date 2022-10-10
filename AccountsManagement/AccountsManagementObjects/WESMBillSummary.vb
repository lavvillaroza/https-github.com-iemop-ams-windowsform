'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             WESMBillSummary
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 19, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for WESM Bill Summary
'Arguments/Parameters:  
'Files/Database Tables:  AM_WESM_BILL_SUMMARY
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   September 19, 2011      Juan Carlo L. Panopio           Class initialization
'   September 26, 2011      Vladimir E. Espiritu            Added ListOfWESMBillSummaryDetails property
'   September 28, 2011      Vladimir E. Espiritu            Added BeginningBalance and EndingBalance properties
'   November 15, 2011       Vladimir E. Espiritu            Added IDType and GroupNo properties
'   January 16, 2012        Vladimir E. Espiritu            Added IsMFWTaxDeducted property
'   March 05. 2012          Vladimir E. Espiritu            Added WESMBillSummaryNo property
'   March 8, 2012           Juan Carlo L. Panopio           Added Adjustment Property
'   August 29, 2013         Vladimir E. Espiritu            Added EnergyWithhold 
'   February 24, 2016       Lance Arjay Villaroza           Added SPAWESMBillSummaryNO Property


Option Explicit On
Option Strict On

<Serializable()> _
Public Class WESMBillSummary

#Region "Initialization/Constructor"
    Implements ICloneable

    Public Sub New(ByVal WESMBillNo As Long)
        Me._WESMBillSummaryNo = WESMBillNo
    End Sub

    Public Sub New(ByVal WESMBillNo As Long, ByVal idnumber As AMParticipants)
        Me._WESMBillSummaryNo = WESMBillNo
        Me._IDNumber = idnumber
    End Sub

    Public Sub New()
        Me.New(Nothing, "", New AMParticipants, Nothing, Nothing, 0, 0, "", 0, Nothing, 0)
    End Sub

    Public Sub New(ByVal GroupNo As Integer, ByVal IDType As String, ByVal EndingBalance As Decimal, ByVal NewDueDate As Date)
        Me._GroupNo = GroupNo
        Me._IDType = IDType
        Me._EndingBalance = EndingBalance
        Me._NewDueDate = NewDueDate
    End Sub

    Public Sub New(ByVal billingperiod As Integer, ByVal idnumber As AMParticipants, _
                   ByVal duedate As Date, _
                   ByVal idtype As String, ByVal Energy As Decimal, _
                   ByVal VatEnergy As Decimal, ByVal MF As Decimal, ByVal VATMF As Decimal)
        Me._BillPeriod = billingperiod
        Me._BillingRemarks = ""
        Me._IDNumber = idnumber
        Me._DueDate = duedate
        Me._IDType = idtype
        Me._Energy = Energy
        Me._VATEnergy = VatEnergy
        Me._MarketFees = MF
        Me._VATMarketFees = VATMF
        Me._NewDueDate = Nothing
        Me._IsMFWTaxDeducted = 0
    End Sub

    Public Sub New(ByVal billingperiod As Integer, ByVal amcode As String, ByVal idnumber As AMParticipants, _
                   ByVal chargetype As EnumChargeType, ByVal duedate As Date, ByVal beginningbalance As Decimal, _
                   ByVal endingbalance As Decimal, ByVal idtype As String, ByVal groupno As Integer, ByVal TransactionDate As Date, _
                   Optional ByVal SummaryType As EnumSummaryType = Nothing, Optional ByVal INVDMCM_Number As String = "", _
                   Optional ByVal Adjustment As Integer = 0, Optional ByVal wesmSummaryNo As Long = 0)
        Me._AMCode = amcode
        Me._BillPeriod = billingperiod
        Me._BillingRemarks = ""
        Me._IDNumber = idnumber
        Me._ChargeType = chargetype
        Me._DueDate = duedate
        Me._BeginningBalance = beginningbalance
        Me._EndingBalance = endingbalance
        Me._ListOfWESMBillSummaryDetails = New List(Of OffsetP2PC2CDetails)
        Me._ListOfWESMBill = New List(Of WESMBill)
        Me._IDType = idtype
        Me._GroupNo = groupno
        Me._NewDueDate = Nothing
        Me._IsMFWTaxDeducted = 0
        Me._INVDMCMNo = INVDMCM_Number
        Me._SummaryType = SummaryType
        Me._adjustment = Adjustment
        Me._WESMBillSummaryNo = wesmSummaryNo
        Me._TransactionDate = TransactionDate        
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return New WESMBillSummary With {.AMCode = AMCode, .BillPeriod = BillPeriod, .IDNumber = IDNumber, .ChargeType = ChargeType, .DueDate = DueDate, .BeginningBalance = BeginningBalance, _
                                          .EndingBalance = EndingBalance, .ListOfWESMBillSummaryDetails = ListOfWESMBillSummaryDetails, .ListOfWESMBill = ListOfWESMBill, _
                                          .IDType = IDType, .GroupNo = GroupNo, .NewDueDate = NewDueDate, .IsMFWTaxDeducted = IsMFWTaxDeducted, .INVDMCMNo = INVDMCMNo, _
                                          .SummaryType = SummaryType, .Adjustment = Adjustment, .WESMBillSummaryNo = WESMBillSummaryNo, .WESMBillBatchNo = WESMBillBatchNo, _
                                         .TransactionDate = TransactionDate, .EnergyWithhold = EnergyWithhold, .BillingRemarks = BillingRemarks, .NoSOA = NoSOA, .NoOffset = NoSOA, .NoDefInt = NoDefInt}
    End Function

    Public Sub New(ByVal wesmbillno As Long, ByVal invdmcmno As String, ByVal summarytype As EnumSummaryType, _
                   ByVal chargetype As EnumChargeType)
        Me._AMCode = ""
        Me._BillPeriod = Nothing
        Me._BillingRemarks = ""
        Me._IDNumber = New AMParticipants()
        Me._ChargeType = chargetype
        Me._DueDate = Nothing
        Me._BeginningBalance = 0
        Me._EndingBalance = 0
        Me._ListOfWESMBillSummaryDetails = New List(Of OffsetP2PC2CDetails)
        Me._ListOfWESMBill = New List(Of WESMBill)
        Me._IDType = ""
        Me._GroupNo = 0
        Me._NewDueDate = Nothing
        Me._IsMFWTaxDeducted = 0
        Me._INVDMCMNo = invdmcmno
        Me._SummaryType = summarytype
        Me._adjustment = 0
        Me._WESMBillSummaryNo = wesmbillno
    End Sub


    Public Sub New(ByVal wesmbillno As Long, ByVal invdmcmno As String, ByVal summarytype As EnumSummaryType, _
                   ByVal chargetype As EnumChargeType, ByVal idnumber As AMParticipants)
        Me._AMCode = ""
        Me._BillPeriod = Nothing
        Me._BillingRemarks = ""
        Me._IDNumber = idnumber
        Me._ChargeType = chargetype
        Me._DueDate = Nothing
        Me._BeginningBalance = 0
        Me._EndingBalance = 0
        Me._ListOfWESMBillSummaryDetails = New List(Of OffsetP2PC2CDetails)
        Me._ListOfWESMBill = New List(Of WESMBill)
        Me._IDType = ""
        Me._GroupNo = 0
        Me._NewDueDate = Nothing
        Me._IsMFWTaxDeducted = 0
        Me._INVDMCMNo = invdmcmno
        Me._SummaryType = summarytype
        Me._adjustment = 0
        Me._WESMBillSummaryNo = wesmbillno
    End Sub

    Public Sub New(ByVal wesmbillno As Long, ByVal invdmcmno As String, ByVal summarytype As EnumSummaryType, _
                   ByVal chargetype As EnumChargeType, ByVal DateDue As Date)
        Me._AMCode = ""
        Me._BillPeriod = Nothing
        Me._BillingRemarks = ""
        Me._IDNumber = IDNumber
        Me._ChargeType = chargetype
        Me._DueDate = DateDue
        Me._BeginningBalance = 0
        Me._EndingBalance = 0
        Me._ListOfWESMBillSummaryDetails = New List(Of OffsetP2PC2CDetails)
        Me._ListOfWESMBill = New List(Of WESMBill)
        Me._IDType = ""
        Me._GroupNo = 0
        Me._NewDueDate = Nothing
        Me._IsMFWTaxDeducted = 0
        Me._INVDMCMNo = invdmcmno
        Me._SummaryType = summarytype
        Me._adjustment = 0
        Me._WESMBillSummaryNo = wesmbillno
    End Sub

    Public Sub New(ByVal wesmbillno As Long, ByVal invdmcmno As String, ByVal summarytype As EnumSummaryType, _
                   ByVal chargetype As EnumChargeType, ByVal DateDue As Date, ByVal energywithhold As Decimal)
        Me._AMCode = ""
        Me._BillPeriod = Nothing
        Me._BillingRemarks = ""
        Me._IDNumber = IDNumber
        Me._ChargeType = chargetype
        Me._DueDate = DateDue
        Me._BeginningBalance = 0
        Me._EndingBalance = 0
        Me._ListOfWESMBillSummaryDetails = New List(Of OffsetP2PC2CDetails)
        Me._ListOfWESMBill = New List(Of WESMBill)
        Me._IDType = ""
        Me._GroupNo = 0
        Me._NewDueDate = Nothing
        Me._IsMFWTaxDeducted = 0
        Me._INVDMCMNo = invdmcmno
        Me._SummaryType = summarytype
        Me._adjustment = 0
        Me._WESMBillSummaryNo = wesmbillno
        Me._EnergyWithhold = energywithhold
    End Sub

    Public Sub New(ByVal wesmbillno As Long, ByVal invdmcmno As String, ByVal summarytype As EnumSummaryType, _
               ByVal chargetype As EnumChargeType, ByVal idnumber As AMParticipants, ByVal energywithhold As Decimal)
        Me._AMCode = ""
        Me._BillPeriod = Nothing
        Me._BillingRemarks = ""
        Me._IDNumber = idnumber
        Me._ChargeType = chargetype
        Me._DueDate = Nothing
        Me._BeginningBalance = 0
        Me._EndingBalance = 0
        Me._ListOfWESMBillSummaryDetails = New List(Of OffsetP2PC2CDetails)
        Me._ListOfWESMBill = New List(Of WESMBill)
        Me._IDType = ""
        Me._GroupNo = 0
        Me._NewDueDate = Nothing
        Me._IsMFWTaxDeducted = 0
        Me._INVDMCMNo = invdmcmno
        Me._SummaryType = summarytype
        Me._adjustment = 0
        Me._WESMBillSummaryNo = wesmbillno
        Me._EnergyWithhold = energywithhold
    End Sub
#End Region

#Region "AM Code"
    Private _AMCode As String
    Public Property AMCode() As String
        Get
            Return _AMCode
        End Get
        Set(ByVal value As String)
            _AMCode = value
        End Set
    End Property

#End Region

#Region "ID Number"
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

#Region "Bill Period"
    Private _BillPeriod As Integer
    Public Property BillPeriod() As Integer
        Get
            Return _BillPeriod
        End Get
        Set(ByVal value As Integer)
            _BillPeriod = value
        End Set
    End Property
#End Region

#Region "Charge Type"
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

#Region "Due Date"
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

#Region "BeginningBalance"
    Private _BeginningBalance As Decimal
    Public Property BeginningBalance() As Decimal
        Get
            Return _BeginningBalance
        End Get
        Set(ByVal value As Decimal)
            _BeginningBalance = value
        End Set
    End Property

#End Region

#Region "EndingBalance"
    Private _EndingBalance As Decimal
    Public Property EndingBalance() As Decimal
        Get
            Return _EndingBalance
        End Get
        Set(ByVal value As Decimal)
            _EndingBalance = value
        End Set
    End Property
#End Region

#Region "OrigEndingBalance"
    Private _OrigEndingBalance As Decimal
    Public Property OrigEndingBalance() As Decimal
        Get
            Return _OrigEndingBalance
        End Get
        Set(ByVal value As Decimal)
            _OrigEndingBalance = value
        End Set
    End Property
#End Region

#Region "ListOfWESMBillSummaryDetails"
    Private _ListOfWESMBillSummaryDetails As List(Of OffsetP2PC2CDetails)
    Public Property ListOfWESMBillSummaryDetails() As List(Of OffsetP2PC2CDetails)
        Get
            Return _ListOfWESMBillSummaryDetails
        End Get
        Set(ByVal value As List(Of OffsetP2PC2CDetails))
            _ListOfWESMBillSummaryDetails = value
        End Set
    End Property
#End Region

#Region "ListOfWESMBills"
    Private _ListOfWESMBill As List(Of WESMBill)
    Public Property ListOfWESMBill() As List(Of WESMBill)
        Get
            Return _ListOfWESMBill
        End Get
        Set(ByVal value As List(Of WESMBill))
            _ListOfWESMBill = value
        End Set
    End Property
#End Region

#Region "IDType"
    Private _IDType As String
    Public Property IDType() As String
        Get
            Return _IDType
        End Get
        Set(ByVal value As String)
            _IDType = value
        End Set
    End Property
#End Region

#Region "GroupNo"
    Private _GroupNo As Long
    Public Property GroupNo() As Long
        Get
            Return _GroupNo
        End Get
        Set(ByVal value As Long)
            _GroupNo = value
        End Set
    End Property

#End Region

#Region "ENERGY"
    Private _Energy As Decimal
    Public Property Energy() As Decimal
        Get
            Return _Energy
        End Get
        Set(ByVal value As Decimal)
            _Energy = value
        End Set
    End Property

#End Region

#Region "VAT on Energy"
    Private _VATEnergy As Decimal
    Public Property VATEnergy() As Decimal
        Get
            Return _VATEnergy
        End Get
        Set(ByVal value As Decimal)
            _VATEnergy = value
        End Set
    End Property
#End Region

#Region "MF"
    Private _MarketFees As Decimal
    Public Property MarketFees() As Decimal
        Get
            Return _MarketFees
        End Get
        Set(ByVal value As Decimal)
            _MarketFees = value
        End Set
    End Property

#End Region

#Region "VAT on MF"
    Private _VATMarketFees As Decimal
    Public Property VATMarketFees() As Decimal
        Get
            Return _VATMarketFees
        End Get
        Set(ByVal value As Decimal)
            _VATMarketFees = value
        End Set
    End Property
#End Region

#Region "NewDueDate"
    Private _NewDueDate As Date
    Public Property NewDueDate() As Date
        Get
            Return _NewDueDate
        End Get
        Set(ByVal value As Date)
            _NewDueDate = value
        End Set
    End Property
#End Region

#Region "OrigNewDueDate"
    Private _OrigNewDueDate As Date
    Public Property OrigNewDueDate() As Date
        Get
            Return _OrigNewDueDate
        End Get
        Set(ByVal value As Date)
            _OrigNewDueDate = value
        End Set
    End Property
#End Region

#Region "INVDMCMNo"
    Private _INVDMCMNo As String
    Public Property INVDMCMNo() As String
        Get
            Return _INVDMCMNo
        End Get
        Set(ByVal value As String)
            _INVDMCMNo = value
        End Set
    End Property

#End Region

#Region "Summary Type"

    Private _SummaryType As EnumSummaryType
    Public Property SummaryType() As EnumSummaryType
        Get
            Return _SummaryType
        End Get
        Set(ByVal value As EnumSummaryType)
            _SummaryType = value
        End Set
    End Property

#End Region

#Region "IsMFWTaxDeducted"
    Private _IsMFWTaxDeducted As Integer
    Public Property IsMFWTaxDeducted() As Integer
        Get
            Return _IsMFWTaxDeducted
        End Get
        Set(ByVal value As Integer)
            _IsMFWTaxDeducted = value
        End Set
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

#Region "Adjustment"
    Private _adjustment As Integer
    Public Property Adjustment() As Integer
        Get
            Return _adjustment
        End Get
        Set(ByVal value As Integer)
            _adjustment = value
        End Set
    End Property
#End Region

#Region "Transaction Date"

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

#Region "Energy Withhold"
    Private _EnergyWithhold As Decimal
    Public Property EnergyWithhold() As Decimal
        Get
            Return _EnergyWithhold
        End Get
        Set(ByVal value As Decimal)
            _EnergyWithhold = value
        End Set
    End Property
#End Region

#Region "StatusUpdate"
    Private _StatusUpdate As Boolean = False
    Public Property StatusUpdate() As Boolean
        Get
            Return _StatusUpdate
        End Get
        Set(ByVal value As Boolean)
            _StatusUpdate = value
        End Set
    End Property
#End Region

#Region "Balance Type"
    Private _BalanceType As New EnumBalanceType
    Public Property BalanceType() As EnumBalanceType
        Get
            Return _BalanceType
        End Get
        Set(ByVal value As EnumBalanceType)
            _BalanceType = value
        End Set
    End Property
#End Region

#Region "SPA No"
    Private _SPANo As Long
    Public Property SPANo() As Long
        Get
            Return _SPANo
        End Get
        Set(ByVal value As Long)
            _SPANo = value
        End Set
    End Property
#End Region

#Region "WESMBill Batch No"
    Private _WESMBillBatchNo As Long
    Public Property WESMBillBatchNo() As Long
        Get
            Return _WESMBillBatchNo
        End Get
        Set(ByVal value As Long)
            _WESMBillBatchNo = value
        End Set
    End Property
#End Region

#Region "Energy Withhold Status"
    Private _EnergyWithholdStatus As EnumEnergyWithholdStatus
    Public Property EnergyWithholdStatus() As EnumEnergyWithholdStatus
        Get
            Return _EnergyWithholdStatus
        End Get
        Set(ByVal value As EnumEnergyWithholdStatus)
            _EnergyWithholdStatus = value
        End Set
    End Property
#End Region

#Region "No Offset"
    Private _NoOffset As Boolean
    Public Property NoOffset() As Boolean
        Get
            Return _NoOffset
        End Get
        Set(ByVal value As Boolean)
            _NoOffset = value
        End Set
    End Property
#End Region

#Region "No SOA"
    Private _NoSOA As Boolean
    Public Property NoSOA() As Boolean
        Get
            Return _NoSOA
        End Get
        Set(ByVal value As Boolean)
            _NoSOA = value
        End Set
    End Property
#End Region

#Region "No Default Interest"
    Private _NoDefInt As Boolean
    Public Property NoDefInt() As Boolean
        Get
            Return _NoDefInt
        End Get
        Set(ByVal value As Boolean)
            _NoDefInt = value
        End Set
    End Property
#End Region

#Region "Remarks"
    Private _BillingRemarks As String
    Public Property BillingRemarks() As String
        Get
            Return _BillingRemarks
        End Get
        Set(ByVal value As String)
            _BillingRemarks = value
        End Set
    End Property
#End Region

End Class
