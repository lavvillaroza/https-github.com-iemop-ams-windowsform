'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             ParticipantLineage
'Orginal Author:         Juan Carlo Panopio
'File Creation Date:     January 12, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Payment Allocation per Account of Participant
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   
'
<Serializable()> _
Public Class PaymentAllocationAccount
    Implements ICloneable
#Region "Initialization"
    Public Sub New()
        Me.PaymentBatchCode = ""
        Me.PaymentGroupCode = 0
        Me.PaymentType = Nothing

        Me.PaymentAmount = 0
        Me.BeginningBalance = 0
        Me.EndingBalance = 0

        Me.WHTax = 0
        Me.WHVAT = 0
        Me.DefaultInterest = 0
        Me.DefaultWHtax = 0
        Me.DefaultWHVAT = 0

        Me.Energy = 0
        Me.VATEnergy = 0
        Me._VATEnergy = 0
        Me._VATMF = 0

        Me.OffsetNo = 0
        Me.DMCMNo = 0
        Me.ORNumber = 0
        Me.WESMBillSummary = New WESMBillSummary

        Me._DueDate = SystemDate
        Me._NewDueDate = SystemDate
    End Sub

    Public Sub New(ByVal PaymentBatchCode As String, ByVal PaymentGroupCode As Long, ByVal PaymentType As EnumPaymentType, ByVal PaymentAmount As Decimal, ByVal BeginningBalance As Decimal, ByVal EndingBalance As Decimal, _
                   ByVal OffsetNo As String, ByVal DMCMNo As Long, ByVal WESMSummary As WESMBillSummary, _
                   ByVal WHVAT As Decimal, ByVal WHTax As Decimal, ByVal DefaultInterest As Decimal, ByVal WHTaxDefault As Decimal, ByVal WHVATDefault As Decimal, _
                   ByVal EnergyAmount As Decimal, ByVal VATEnergyAmount As Decimal, ByVal MFAmount As Decimal, ByVal MFVAmount As Decimal, Optional ByVal ORNumber As Long = 0)

        Me._PaymentBatchCode = PaymentBatchCode
        Me._PaymentGroupCode = PaymentGroupCode
        Me._PaymentType = PaymentType

        Me._WHVAT = WHVAT
        Me._WHTax = WHTax
        Me._DefaultInterest = DefaultInterest
        Me._DefaultWHTax = WHTaxDefault
        Me._DefaultWHVAT = WHVATDefault

        Me._Energy = EnergyAmount
        Me._VATEnergy = VATEnergyAmount
        Me._MarketFees = MFAmount
        Me._VATMF = MFVAmount

        Me._PaymentAmount = PaymentAmount
        Me._BeginningBalance = BeginningBalance
        Me._EndingBalance = EndingBalance
        Me._OffsetNo = OffsetNo
        Me._DMCMNo = DMCMNo
        Me._WESMBillSummary = WESMSummary
        Me._ORNumber = ORNumber
    End Sub

    Public Sub New(ByVal PaymentBatchCode As String, ByVal PaymentGroupCode As Long, ByVal PaymentType As EnumPaymentType, ByVal PaymentAmount As Decimal, ByVal BeginningBalance As Decimal, ByVal EndingBalance As Decimal, _
               ByVal OffsetNo As String, ByVal DMCMNo As Long, ByVal WESMSummary As WESMBillSummary, _
               ByVal WHVAT As Decimal, ByVal WHTax As Decimal, ByVal DefaultInterest As Decimal, ByVal WHTaxDefault As Decimal, ByVal WHVATDefault As Decimal, _
               ByVal EnergyAmount As Decimal, ByVal VATEnergyAmount As Decimal, ByVal MFAmount As Decimal, ByVal MFVAmount As Decimal, ByVal Duedate As Date, _
               ByVal NewDueDate As Date, Optional ByVal ORNumber As Long = 0)

        Me._PaymentBatchCode = PaymentBatchCode
        Me._PaymentGroupCode = PaymentGroupCode
        Me._PaymentType = PaymentType

        Me._WHVAT = WHVAT
        Me._WHTax = WHTax
        Me._DefaultInterest = DefaultInterest
        Me._DefaultWHTax = WHTaxDefault
        Me._DefaultWHVAT = WHVATDefault

        Me._Energy = EnergyAmount
        Me._VATEnergy = VATEnergyAmount
        Me._MarketFees = MFAmount
        Me._VATMF = MFVAmount

        Me._PaymentAmount = PaymentAmount
        Me._BeginningBalance = BeginningBalance
        Me._EndingBalance = EndingBalance
        Me._OffsetNo = OffsetNo
        Me._DMCMNo = DMCMNo
        Me._ORNumber = ORNumber
        Me._WESMBillSummary = WESMSummary

        Me._DueDate = Duedate
        Me._NewDueDate = NewDueDate
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return New PaymentAllocationAccount With {.PaymentBatchCode = PaymentBatchCode, .PaymentGroupCode = PaymentGroupCode, .PaymentType = PaymentType, _
                                                  .PaymentAmount = PaymentAmount, .BeginningBalance = BeginningBalance, .EndingBalance = EndingBalance, _
                                                  .OffsetNo = OffsetNo, .DMCMNo = DMCMNo, .WESMBillSummary = WESMBillSummary, .WHTax = WHTax, .WHVAT = WHVAT, _
                                                  .DefaultInterest = DefaultInterest, .DueDate = DueDate, .NewDueDate = NewDueDate, .ORNumber = ORNumber}
    End Function

#End Region

#Region "Payment Batch Code"
    Private _PaymentBatchCode As String
    Public Property PaymentBatchCode() As String
        Get
            Return _PaymentBatchCode
        End Get
        Set(ByVal value As String)
            _PaymentBatchCode = value
        End Set
    End Property
#End Region

#Region "Payment Group Code"
    Private _PaymentGroupCode As Long
    Public Property PaymentGroupCode() As Long
        Get
            Return _PaymentGroupCode
        End Get
        Set(ByVal value As Long)
            _PaymentGroupCode = value
        End Set
    End Property
#End Region

#Region "Payment Type"
    Private _PaymentType As EnumPaymentType
    Public Property PaymentType() As EnumPaymentType
        Get
            Return _PaymentType
        End Get
        Set(ByVal value As EnumPaymentType)
            _PaymentType = value
        End Set
    End Property
#End Region

#Region "Payment Amount"
    Private _PaymentAmount As Decimal
    Public Property PaymentAmount() As Decimal
        Get
            Return _PaymentAmount
        End Get
        Set(ByVal value As Decimal)
            _PaymentAmount = value
        End Set
    End Property
#End Region

#Region "Energy"
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

#Region "VAT On Energy"
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

#Region "Market Fees"
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

#Region "VAT On MF"
    Private _VATMF As Decimal
    Public Property VATMF() As Decimal
        Get
            Return _VATMF
        End Get
        Set(ByVal value As Decimal)
            _VATMF = value
        End Set
    End Property
#End Region

#Region "Beginning Balance"
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

#Region "Ending Balance"
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

#Region "Offset Number"
    Private _OffsetNo As String
    Public Property OffsetNo() As String
        Get
            Return _OffsetNo
        End Get
        Set(ByVal value As String)
            _OffsetNo = value
        End Set
    End Property
#End Region

#Region "WHTax"

    Private _WHTax As Decimal
    Public Property WHTax() As Decimal
        Get
            Return _WHTax
        End Get
        Set(ByVal value As Decimal)
            _WHTax = value
        End Set
    End Property

#End Region

#Region "WHVAT"

    Private _WHVAT As Decimal
    Public Property WHVAT() As Decimal
        Get
            Return _WHVAT
        End Get
        Set(ByVal value As Decimal)
            _WHVAT = value
        End Set
    End Property

#End Region

#Region "Default Interest - WHTax"

    Private _DefaultWHTax As Decimal
    Public Property DefaultWHtax() As Decimal
        Get
            Return _DefaultWHTax
        End Get
        Set(ByVal value As Decimal)
            _DefaultWHTax = value
        End Set
    End Property

#End Region

#Region "Default Interest - WHVat"

    Private _DefaultWHVAT As Decimal
    Public Property DefaultWHVAT() As Decimal
        Get
            Return _DefaultWHVAT
        End Get
        Set(ByVal value As Decimal)
            _DefaultWHVAT = value
        End Set
    End Property

#End Region

#Region "Default Interest"

    Private _DefaultInterest As Decimal
    Public Property DefaultInterest() As Decimal
        Get
            Return _DefaultInterest
        End Get
        Set(ByVal value As Decimal)
            _DefaultInterest = value
        End Set
    End Property

#End Region

#Region "WESM Bill Summary"
    Private _WESMBillSummary As WESMBillSummary
    Public Property WESMBillSummary() As WESMBillSummary
        Get
            Return _WESMBillSummary
        End Get
        Set(ByVal value As WESMBillSummary)
            _WESMBillSummary = value
        End Set
    End Property

#End Region

#Region "DMCM Number"
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

#Region "UPDATED BY"
    Private _UpdatedBy As String
    Public Property UpdatedBy() As String
        Get
            Return _UpdatedBy
        End Get
        Set(ByVal value As String)
            _UpdatedBy = value
        End Set
    End Property
#End Region

#Region "UPDATED DATE"
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

    Private _DueDate As Date
    Public Property DueDate() As Date
        Get
            Return _DueDate
        End Get
        Set(ByVal value As Date)
            _DueDate = value
        End Set
    End Property


    Private _NewDueDate As Date
    Public Property NewDueDate() As Date
        Get
            Return _NewDueDate
        End Get
        Set(ByVal value As Date)
            _NewDueDate = value
        End Set
    End Property

    Private _ORNumber As Long
    Public Property ORNumber() As Long
        Get
            Return _ORNumber
        End Get
        Set(ByVal value As Long)
            _ORNumber = value
        End Set
    End Property

End Class

