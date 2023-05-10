'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             AllocatedPayment
'Orginal Author:         Juan Carlo Panopio
'File Creation Date:     August 22, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for AllocatedPayment
'Arguments/Parameters:  
'Files/Database Tables:  AM_ACCOUNTING_CODE
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description
'   April 2, 2012         Juan Carlo L Panopio      Class initialization


Public Class AllocatedPayment
    Implements ICloneable
#Region "Initialization"
    Public Sub New()
        Me._Energy = 0
        Me._DefaultInterest = 0
        Me._VAT = 0

        Me._OffsetAMTEnergy = 0
        Me._OffsetAllocatedEnergy = 0

        Me._OffsetAMTVAT = 0
        Me._OffsetAllocatedVAT = 0

        Me._DeferredEnergy = 0
        Me._DeferredVAT = 0

        Me._InterestEarnedNSS = 0
        Me._InterestEarnedSTL = 0
        Me._InterestEarnedPR = 0

        Me._TotalEnergyPayment = 0
        Me._TotalVATPayment = 0

        Me._TotalEnergyAllocation = 0
        Me._TotalEnergyPayment = 0

        Me._TotalVATAllocation = 0
        Me._TotalVATPayment = 0

        Me._ReturnToParticipant = 0
        Me._MarketFees = 0
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return New AllocatedPayment With {.Energy = Energy, .DefaultInterest = DefaultInterest, .VAT = VAT, _
                                          .OffsetAMTEnergy = OffsetAMTEnergy, .OffsetAllocatedEnergy = OffsetAllocatedEnergy, _
                                          .OffsetAllocatedVAT = OffsetAllocatedVAT, .OffsetAMTVAT = OffsetAMTVAT, _
                                          .DeferredEnergy = DeferredEnergy, .DeferredVAT = DeferredVAT, _
                                          .InterestEarnedNSS = InterestEarnedNSS, .InterestEarnedPR = InterestEarnedPR, .InterestEarnedSTL = InterestEarnedSTL, _
                                          .TotalEnergyPayment = TotalEnergyPayment, .TotalEnergyAllocation = TotalEnergyAllocation, _
                                          .TotalVATPayment = TotalVATPayment, .TotalVATAllocation = TotalVATAllocation, .ReturnToParticipant = ReturnToParticipant, _
                                          .MarketFees = MarketFees}
    End Function
#End Region

    Private _Energy As Decimal
    Public Property Energy() As Decimal
        Get
            Return _Energy
        End Get
        Set(ByVal value As Decimal)
            _Energy = value
        End Set
    End Property

    Private _DefaultInterest As Decimal
    Public Property DefaultInterest() As Decimal
        Get
            Return _DefaultInterest
        End Get
        Set(ByVal value As Decimal)
            _DefaultInterest = value
        End Set
    End Property

    Private _VAT As Decimal
    Public Property VAT() As Decimal
        Get
            Return _VAT
        End Get
        Set(ByVal value As Decimal)
            _VAT = value
        End Set
    End Property

    Private _OffsetAMTEnergy As Decimal
    Public Property OffsetAMTEnergy() As Decimal
        Get
            Return _OffsetAMTEnergy
        End Get
        Set(ByVal value As Decimal)
            _OffsetAMTEnergy = value
        End Set
    End Property

    Private _OffsetAMTVAT As Decimal
    Public Property OffsetAMTVAT() As Decimal
        Get
            Return _OffsetAMTVAT
        End Get
        Set(ByVal value As Decimal)
            _OffsetAMTVAT = value
        End Set
    End Property

    Private _OffsetAllocatedEnergy As Decimal
    Public Property OffsetAllocatedEnergy() As Decimal
        Get
            Return _OffsetAllocatedEnergy
        End Get
        Set(ByVal value As Decimal)
            _OffsetAllocatedEnergy = value
        End Set
    End Property

    Private _OffsetAllocatedVAT As Decimal
    Public Property OffsetAllocatedVAT() As Decimal
        Get
            Return _OffsetAllocatedVAT
        End Get
        Set(ByVal value As Decimal)
            _OffsetAllocatedVAT = value
        End Set
    End Property

    Private _DeferredEnergy As Decimal
    Public Property DeferredEnergy() As Decimal
        Get
            Return _DeferredEnergy
        End Get
        Set(ByVal value As Decimal)
            _DeferredEnergy = value
        End Set
    End Property

    Private _DeferredVAT As Decimal
    Public Property DeferredVAT() As Decimal
        Get
            Return _DeferredVAT
        End Get
        Set(ByVal value As Decimal)
            _DeferredVAT = value
        End Set
    End Property

    Private _InterestEarnedNSS As Decimal
    Public Property InterestEarnedNSS() As Decimal
        Get
            Return _InterestEarnedNSS
        End Get
        Set(ByVal value As Decimal)
            _InterestEarnedNSS = value
        End Set
    End Property

    Private _InterestEarnedSTL As Decimal
    Public Property InterestEarnedSTL() As Decimal
        Get
            Return _InterestEarnedSTL
        End Get
        Set(ByVal value As Decimal)
            _InterestEarnedSTL = value
        End Set
    End Property

    Private _InterestEarnedPR As Decimal
    Public Property InterestEarnedPR() As Decimal
        Get
            Return _InterestEarnedPR
        End Get
        Set(ByVal value As Decimal)
            _InterestEarnedPR = value
        End Set
    End Property

    Private _TotalEnergyPayment As Decimal
    Public Property TotalEnergyPayment() As Decimal
        Get
            Return _TotalEnergyPayment
        End Get
        Set(ByVal value As Decimal)
            _TotalEnergyPayment = value
        End Set
    End Property

    Private _TotalVATPayment As Decimal
    Public Property TotalVATPayment() As Decimal
        Get
            Return _TotalVATPayment
        End Get
        Set(ByVal value As Decimal)
            _TotalVATPayment = value
        End Set
    End Property

    Private _TotalEnergyAllocation As Decimal
    Public Property TotalEnergyAllocation() As Decimal
        Get
            Return _TotalEnergyAllocation
        End Get
        Set(ByVal value As Decimal)
            _TotalEnergyAllocation = value
        End Set
    End Property

    Private _TotalVATAllocation As Decimal
    Public Property TotalVATAllocation() As Decimal
        Get
            Return _TotalVATAllocation
        End Get
        Set(ByVal value As Decimal)
            _TotalVATAllocation = value
        End Set
    End Property

    Private _ReturnToParticipant As Decimal
    Public Property ReturnToParticipant() As Decimal
        Get
            Return _ReturnToParticipant
        End Get
        Set(ByVal value As Decimal)
            _ReturnToParticipant = value
        End Set
    End Property


    Private _MarketFees As Decimal
    Public Property MarketFees() As Decimal
        Get
            Return _MarketFees
        End Get
        Set(ByVal value As Decimal)
            _MarketFees = value
        End Set
    End Property

End Class
