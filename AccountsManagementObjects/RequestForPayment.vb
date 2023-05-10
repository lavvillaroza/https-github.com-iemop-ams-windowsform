'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             Request For Payment
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     May 22, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Request For Payment
'Arguments/Parameters:  
'Files/Database Tables:  AM_REQUEST_FOR_PAYMENT
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'

Public Class RequestForPayment

    Public Sub New()
        Me.New(Nothing, Nothing, "", "", 0, "", "", "", "", "", Nothing, New List(Of RequestForPaymentDetails), 0, 0, 0, 0, 0, 0)
    End Sub

    Public Sub New(ByVal AllocationDate As Date, ByVal Paymentdate As Date, ByVal RFPTo As String, ByVal RFPFrom As String, _
                   ByVal ReferenceNo As Long, ByVal PurposeOfPayment As String, ByVal PreparedBy As String, ByVal ApprovedBy As String, _
                   ByVal ReviewedBy As String, ByVal UpdatedBy As String, ByVal UpdatedDate As Date, ByVal RequestForPaymentDetails As List(Of RequestForPaymentDetails), _
                   ByVal NSSAmount As Decimal, ByVal PRReplenish As Decimal, ByVal MarketFees As Decimal, ByVal HeldCollection As Decimal, ByVal PEMCAccount As Decimal, _
                   ByVal ExcessCollection As Decimal)

        Me._AllocationDate = AllocationDate
        Me._PaymentDate = Paymentdate
        Me._ToRFP = RFPTo
        Me._FromRFP = RFPFrom
        Me._ReferenceNo = ReferenceNo
        Me._PurposeOfPayment = PurposeOfPayment
        Me._PreparedBy = PreparedBy
        Me._ApprovedBy = ApprovedBy
        Me._ReviewedBy = ReviewedBy
        Me._UpdatedBy = UpdatedBy
        Me._UpdatedDate = UpdatedDate
        Me._RFPDetails = RequestForPaymentDetails

        Me._NSSAmount = NSSAmount
        Me._PRReplenishment = PRReplenish
        Me._MarketFees = MarketFees

        Me._TransferToPEMC = PEMCAccount
        Me._HeldCollection = HeldCollection

        Me._ExcessCollection = ExcessCollection
    End Sub

#Region "Allocation Date"

    Private _AllocationDate As Date
    Public Property AllocationDate() As Date
        Get
            Return _AllocationDate
        End Get
        Set(ByVal value As Date)
            _AllocationDate = value
        End Set
    End Property

#End Region

#Region "Payment Date"


    Private _PaymentDate As Date
    Public Property PaymentDate() As Date
        Get
            Return _PaymentDate
        End Get
        Set(ByVal value As Date)
            _PaymentDate = value
        End Set
    End Property

#End Region

#Region "RFP_To"

    Private _ToRFP As String
    Public Property toRFP() As String
        Get
            Return _ToRFP
        End Get
        Set(ByVal value As String)
            _ToRFP = value
        End Set
    End Property

#End Region

#Region "RFP_From"

    Private _FromRFP As String
    Public Property FromRFP() As String
        Get
            Return _FromRFP
        End Get
        Set(ByVal value As String)
            _FromRFP = value
        End Set
    End Property

#End Region

#Region "Reference No"

    Private _ReferenceNo As Long
    Public Property ReferenceNo() As Long
        Get
            Return _ReferenceNo
        End Get
        Set(ByVal value As Long)
            _ReferenceNo = value
        End Set
    End Property

#End Region

#Region "Purpose of Payment"

    Private _PurposeOfPayment As String
    Public Property PurposeOfPayment() As String
        Get
            Return _PurposeOfPayment
        End Get
        Set(ByVal value As String)
            _PurposeOfPayment = value
        End Set
    End Property

#End Region

#Region "Prepared By"

    Private _PreparedBy As String
    Public Property PreparedBy() As String
        Get
            Return _PreparedBy
        End Get
        Set(ByVal value As String)
            _PreparedBy = value
        End Set
    End Property
    Private _PreparedByPosition As String
    Public Property PreparedByPosition() As String
        Get
            Return _PreparedByPosition
        End Get
        Set(ByVal value As String)
            _PreparedByPosition = value
        End Set
    End Property
#End Region

#Region "Approved By"
    Private _ApprovedBy As String
    Public Property ApprovedBy() As String
        Get
            Return _ApprovedBy
        End Get
        Set(ByVal value As String)
            _ApprovedBy = value
        End Set
    End Property

    Private _ApprovedByPosition As String
    Public Property ApprovedByPosition As String
        Get
            Return _ApprovedByPosition
        End Get
        Set(value As String)
            _ApprovedByPosition = value
        End Set
    End Property
#End Region

#Region "Reviewed By"

    Private _ReviewedBy As String
    Public Property ReviewedBy() As String
        Get
            Return _ReviewedBy
        End Get
        Set(ByVal value As String)
            _ReviewedBy = value
        End Set
    End Property

    Private _ReviewedByPosition As String
    Public Property ReviewedByPosition As String
        Get
            Return _ReviewedByPosition
        End Get
        Set(value As String)
            _ReviewedByPosition = value
        End Set
    End Property

#End Region

#Region "Updated By"
    Private _UpdatedBy As String
    Public Property UpdatedBy() As String
        Get
            Return _UpdatedBy
        End Get
        Set(ByVal value As String)
            _UpdatedBy = value
        End Set
    End Property

    Private _UpdatedDate As Date
    Public Property UpdatedDate() As Date
        Get
            Return _UpdatedDate
        End Get
        Set(ByVal value As Date)
            _UpdatedDate = value
        End Set
    End Property
#End Region

#Region "Request for payment Details"

    Private _RFPDetails As List(Of RequestForPaymentDetails)
    Public Property RFPDetails() As List(Of RequestForPaymentDetails)
        Get
            Return _RFPDetails
        End Get
        Set(ByVal value As List(Of RequestForPaymentDetails))
            _RFPDetails = value
        End Set
    End Property

#End Region

#Region "NSS Amount"
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

#Region "PR Replenishment"
    Private _PRReplenishment As Decimal
    Public Property PRReplenishment() As Decimal
        Get
            Return _PRReplenishment
        End Get
        Set(ByVal value As Decimal)
            _PRReplenishment = value
        End Set
    End Property
#End Region

#Region "Market Fees Application"

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

#Region "Transfer To PEMC Account"
    Private _TransferToPEMC As Decimal
    Public Property TransferToPEMC() As Decimal
        Get
            Return _TransferToPEMC
        End Get
        Set(ByVal value As Decimal)
            _TransferToPEMC = value
        End Set
    End Property
#End Region
    
#Region "Transfer to Held Collection"
    Private _HeldCollection As Decimal
    Public Property HeldCollection() As Decimal
        Get
            Return _HeldCollection
        End Get
        Set(ByVal value As Decimal)
            _HeldCollection = value
        End Set
    End Property
#End Region

#Region "Transfer to Excess Collection"
    Private _ExcessCollection As Decimal
    Public Property ExcessCollection() As Decimal
        Get
            Return _ExcessCollection
        End Get
        Set(ByVal value As Decimal)
            _ExcessCollection = value
        End Set
    End Property
#End Region


End Class
