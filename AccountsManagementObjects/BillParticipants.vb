'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             BillParticipants
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 20, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for Bill Participants
'Arguments/Parameters:  
'Files/Database Tables:  BILL_PARTICIPANTS
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   September 20, 2011      Juan Carlo L. Panopio           Class initialization
'   November 26, 2011       Vladimir E. Espiritu            Added new initialization
'	January 10, 2012        Vladimir E. Espiritu            Added HeldPayment property
'   January 16, 2012        Vladimir E. Espiritu            Added IsPayingWitholdingTax property
'	January 10, 2011        Juan Carlo L. Panopio           Added Deferred Payment
'   January 18, 2012        Vladimir E. Espiritu            Added Synonyms property
'   January 24, 2012        Vladimir E. Espiritu            Added MarketFeesTax and delete IsPayingWitholdingTax property

<Serializable()> _
Public Class XBillParticipants

#Region "Initialization/Constructor"
    Public Sub New()
        Me.New(0)
    End Sub

    Public Sub New(ByVal IDNum As String)
        Me.New(IDNum, "")
    End Sub

    Public Sub New(ByVal IDNumber As String, ByVal ParticipantID As String)
        Me.New(IDNumber, ParticipantID, "")
    End Sub

    Public Sub New(ByVal IDNumber As String, ByVal ParticipantID As String, ByVal ParticipantAddress As String)
        Me.New(IDNumber, ParticipantID, ParticipantAddress, "", 1, 0, 0, "")
    End Sub

    Public Sub New(ByVal IDNumber As String, ByVal Participant As String, ByVal ParticipantAddress As String, ByVal remarks As String, ByVal status As Integer, _
                   ByVal DeferredPaymentEnergy As Decimal, ByVal DeferredPaymentVAT As Decimal, ByVal genload As String)
        Me._IDNumber = IDNumber
        Me._ParticipantID = Participant
        Me._ParticipantAddress = ParticipantAddress
        Me._Remarks = remarks
        Me._Status = status
        Me._DefPaymentEnergy = DeferredPaymentEnergy
        Me._DefPaymentVAT = DeferredPaymentVAT
        Me._GenLoad = genload
        Me._Synonyms = ""
        Me._MarketFeesTax = 0
    End Sub

    Public Sub New(ByVal IDNumber As String, ByVal DeferredPaymentEnergy As Decimal, ByVal DeferredPaymentVAT As Decimal)
        Me._IDNumber = IDNumber
        Me._DefPaymentEnergy = DeferredPaymentEnergy
        Me._DefPaymentVAT = DeferredPaymentVAT
    End Sub

#End Region

#Region "ID Number"
    Private _IDNumber As String
    Public Property IDNumber() As String
        Get
            Return _IDNumber
        End Get
        Set(ByVal value As String)
            _IDNumber = value
        End Set
    End Property
#End Region

#Region "Participant ID"
    Private _ParticipantID As String
    Public Property ParticipantID() As String
        Get
            Return _ParticipantID
        End Get
        Set(ByVal value As String)
            _ParticipantID = value
        End Set
    End Property
#End Region

#Region "Synonyms"
    Private _Synonyms As String
    Public Property Synonyms() As String
        Get
            Return _Synonyms
        End Get
        Set(ByVal value As String)
            _Synonyms = value
        End Set
    End Property

#End Region

#Region "If Participant is GEN/Load"
    Private _GenLoad As String
    Public Property GenLoad() As String
        Get
            Return _GenLoad
        End Get
        Set(ByVal value As String)
            _GenLoad = value
        End Set
    End Property
#End Region


#Region "Remarks"
    Private _Remarks As String
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property
#End Region

#Region "Deferred Payment ENERGY"
    Private _DefPaymentEnergy As Decimal
    Public Property DefPaymentEnergy() As Decimal
        Get
            Return _DefPaymentEnergy
        End Get
        Set(ByVal value As Decimal)
            _DefPaymentEnergy = value
        End Set
    End Property
#End Region

#Region "Deferred Payment ENERGY"
    Private _DefPaymentVAT As Decimal
    Public Property DefPaymentVAT() As Decimal
        Get
            Return _DefPaymentVAT
        End Get
        Set(ByVal value As Decimal)
            _DefPaymentVAT = value
        End Set
    End Property
#End Region

#Region "Participant Address"
    Private _ParticipantAddress As String
    Public Property ParticipantAddress() As String
        Get
            Return _ParticipantAddress
        End Get
        Set(ByVal value As String)
            _ParticipantAddress = value
        End Set
    End Property
#End Region

#Region "Status"
    Private _Status As Integer
    Public Property Status() As Integer
        Get
            Return _Status
        End Get
        Set(ByVal value As Integer)
            _Status = value
        End Set
    End Property
#End Region

#Region "HeldPayment"
    Private _HeldPayment As Decimal
    Public Property HeldPayment() As Decimal
        Get
            Return _HeldPayment
        End Get
        Set(ByVal value As Decimal)
            _HeldPayment = value
        End Set
    End Property

#End Region


#Region "MarketFeesTax"
    Private _MarketFeesTax As Decimal
    Public Property MarketFeesTax() As Decimal
        Get
            Return _MarketFeesTax
        End Get
        Set(ByVal value As Decimal)
            _MarketFeesTax = value
        End Set
    End Property

#End Region


End Class
