'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             AMParticipants
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     February 7, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for AM Participants
'Arguments/Parameters:  
'Files/Database Tables:  AMParticipants
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision HistoryC:\Users\IMEMProgrammer\Desktop\AccountsManagement\AccountsManagementObjects\AMParticipants.vb
'	Date Modified		    Programmer		                Description
'   February 7, 2012        Vladimir E. Espiritu            Class initialization
'   February 10, 2012       Vladimir E. Espiritu            Added Bank Clearing property
'   February 15, 2012       Vladimir E. Espiritu            Removed DeferredPayment property 
'   February 15, 2012       Vladimir E. Espiritu            Added DeferredPaymentEnergy and DeferredPaymentEnergyVat properties
'   March 13, 2012          Vladimir E. Espiritu            Added new initialization with IDNumber parameter only
'   March 19, 2012          Vladimir E. Espiritu            Added Excess Collection property
'   March 24, 2012          Vladimir E. Espiritu            Added BPIAccountDeposit property
'   April 18, 2012          Vladimir E. Espiritu            Added Classification property
'   June 10, 2012           Vladimir E. Espiritu            Added VatOnMarketFeesTax property
'   August 13, 2012         Vladimir E. Espiritu            Added Bank Transaction Code property
'   November 5, 2012        Vladimir E. Espiritu            Added Street, Barangay, City, Province and ZipCode properties
'   August 29, 2013         Vladimir E. Espiritu            Added APEnergyWTax, AREnergyWTax, APEnergyWVAT and AREnergyWVAT properties
'   August 30, 2013         Vladimir E. Espiritu            Added SPARate property
'   September 18, 2013      Vladimir E. Espiritu            Added ZeroRatedMarketFees, ZeroRatedEnergy, 
'                                                           EcoZoneRegCertificateNo, EcoZoneEffectiveDate and VirtualAccountNo properties

<Serializable()> _
Public Class AMParticipants

#Region "Initialization/Constructor"
    Public Sub New()       
    End Sub

    Public Sub New(ByVal idnumber As String)
        Me._IDNumber = idnumber
    End Sub

    Public Sub New(ByVal idnumber As String, ByVal participantid As String)

        Me._IDNumber = idnumber
        Me._ParticipantID = participantid
    End Sub

    Public Sub New(ByVal idnumber As String, ByVal participantid As String, ByVal fullname As String)

        Me._IDNumber = idnumber
        Me._ParticipantID = participantid
        Me._FullName = fullname
    End Sub

    Public Sub New(ByVal idnumber As String, ByVal participantid As String, ByVal fullname As String, ByVal participantAddress As String)
        Me._IDNumber = idnumber
        Me._ParticipantID = participantid
        Me._FullName = fullname
        Me._ParticipantAddress = participantAddress
    End Sub

    Public Sub New(ByVal idnumber As String, ByVal participantid As String, ByVal participantaddress As String, ByVal city As String,
               ByVal province As String, ByVal zipcode As String)
        Me._IDNumber = idnumber
        Me._ParticipantID = participantid
        Me._ParticipantAddress = participantaddress
        Me._City = city
        Me._Province = province
        Me._ZipCode = zipcode
    End Sub

    Public Sub New(ByVal idnumber As String, ByVal participantid As String, ByVal billingadddress As String, ByVal participantaddress As String, ByVal city As String,
               ByVal province As String, ByVal zipcode As String)
        Me._IDNumber = idnumber
        Me._ParticipantID = participantid
        Me._ParticipantAddress = participantaddress
        Me._BillingAddress = billingadddress
        Me._City = city
        Me._Province = province
        Me._ZipCode = zipcode
    End Sub

    Public Sub New(ByVal idnumber As String, ByVal participantid As String, ByVal participantaddress As String, ByVal city As String,
                   ByVal province As String, ByVal zipcode As String, ByVal zeroratedmf As Boolean, zeroratedenergy As Boolean)
        Me._IDNumber = idnumber
        Me._ParticipantID = participantid
        Me._ParticipantAddress = participantaddress
        Me._City = city
        Me._Province = province
        Me._ZipCode = zipcode
        Me._ZeroRatedMarketFees = zeroratedmf
        Me._ZeroRatedEnergy = zeroratedenergy
    End Sub

    Public Sub New(ByVal idnumber As String, ByVal participantid As String, ByVal fullname As String, ByVal billingaddress As String, ByVal facilitytype As EnumGenLoad,
                   ByVal region As String, ByVal membershiptype As String)
        Me._IDNumber = idnumber
        Me._ParticipantID = participantid
        Me._FullName = fullname
        Me._BillingAddress = billingaddress
        Me._GenLoad = facilitytype
        Me._Region = region
        Me._MembershipType = membershiptype
    End Sub
#End Region

#Region "IDNumber"
    Private _IDNumber As String
    Public Property IDNumber() As String
        Get
            If String.IsNullOrEmpty(_IDNumber) Then
                _IDNumber = ""
            End If
            Return _IDNumber
        End Get
        Set(ByVal value As String)
            _IDNumber = value
        End Set
    End Property

#End Region

#Region "ParticipantID"
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

#Region "FullName"
    Private _FullName As String
    Public Property FullName() As String
        Get
            Return _FullName
        End Get
        Set(ByVal value As String)
            _FullName = value
        End Set
    End Property
#End Region

#Region "BusinessStyle"
    Private _BusinessStyle As String
    Public Property BusinessStyle() As String
        Get
            Return _BusinessStyle
        End Get
        Set(ByVal value As String)
            _BusinessStyle = value
        End Set
    End Property
#End Region

#Region "TIN"
    Private _TIN As String
    Public Property TIN() As String
        Get
            Return _TIN
        End Get
        Set(ByVal value As String)
            _TIN = value
        End Set
    End Property

#End Region

#Region "EcoZoneRegCertificateNo"
    Private _EcoZoneRegCertificateNo As String
    Public Property EcoZoneRegCertificateNo() As String
        Get
            Return _EcoZoneRegCertificateNo
        End Get
        Set(ByVal value As String)
            _EcoZoneRegCertificateNo = value
        End Set
    End Property


#End Region

#Region "EcoZoneEffectiveDate"
    Private _EcoZoneEffectiveDate As Date
    Public Property EcoZoneEffectiveDate() As Date
        Get
            Return _EcoZoneEffectiveDate
        End Get
        Set(ByVal value As Date)
            _EcoZoneEffectiveDate = value
        End Set
    End Property

#End Region

#Region "GenLoad"
    Private _GenLoad As EnumGenLoad
    Public Property GenLoad() As EnumGenLoad
        Get
            Return _GenLoad
        End Get
        Set(ByVal value As EnumGenLoad)
            _GenLoad = value
        End Set
    End Property

#End Region

#Region "MarketFeesWHTax"
    Private _MarketFeesWhTax As Decimal
    Public Property MarketFeesWHTax() As Decimal
        Get
            Return _MarketFeesWhTax
        End Get
        Set(ByVal value As Decimal)
            _MarketFeesWhTax = value
        End Set
    End Property
#End Region

#Region "MarketFeesWHVAT"
    Private _MarketFeesWHVAT As Decimal
    Public Property MarketFeesWHVAT() As Decimal
        Get
            Return _MarketFeesWHVAT
        End Get
        Set(ByVal value As Decimal)
            _MarketFeesWHVAT = value
        End Set
    End Property
#End Region

#Region "BankAccountNo"
    Private _BankAccountNo As String
    Public Property BankAccountNo() As String
        Get
            Return _BankAccountNo
        End Get
        Set(ByVal value As String)
            _BankAccountNo = value
        End Set
    End Property

#End Region

#Region "BankTransactionCode"
    Private _BankTransactionCode As String
    Public Property BankTransactionCode() As String
        Get
            Return _BankTransactionCode
        End Get
        Set(ByVal value As String)
            _BankTransactionCode = value
        End Set
    End Property

#End Region

#Region "PaymentType"
    Private _PaymentType As EnumParticipantPaymentType
    Public Property PaymentType() As EnumParticipantPaymentType
        Get
            Return _PaymentType
        End Get
        Set(ByVal value As EnumParticipantPaymentType)
            _PaymentType = value
        End Set
    End Property
#End Region

#Region "Representative"
    Private _Representative As ParticipantRepresentative
    Public Property Representative() As ParticipantRepresentative
        Get
            Return _Representative
        End Get
        Set(ByVal value As ParticipantRepresentative)
            _Representative = value
        End Set
    End Property
#End Region

#Region "Status"
    Private _Status As EnumStatus
    Public Property Status() As EnumStatus
        Get
            Return _Status
        End Get
        Set(ByVal value As EnumStatus)
            _Status = value
        End Set
    End Property

#End Region

#Region "ParticipantAddress"
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

#Region "BillingAddress"
    Private _BillingAddress As String
    Public Property BillingAddress() As String
        Get
            Return _BillingAddress
        End Get
        Set(ByVal value As String)
            _BillingAddress = value
        End Set
    End Property
#End Region

#Region "City"

    Private _City As String
    Public Property City() As String
        Get
            Return _City
        End Get
        Set(ByVal value As String)
            _City = value
        End Set
    End Property

#End Region

#Region "Province"

    Private _Province As String
    Public Property Province() As String
        Get
            Return _Province
        End Get
        Set(ByVal value As String)
            _Province = value
        End Set
    End Property

#End Region

#Region "Region"
    Private _Region As String
    Public Property Region() As String
        Get
            Return _Region
        End Get
        Set(ByVal value As String)
            _Region = value
        End Set
    End Property

#End Region

#Region "ZipCode"
    Private _ZipCode As String
    Public Property ZipCode() As String
        Get
            Return _ZipCode
        End Get
        Set(ByVal value As String)
            _ZipCode = value
        End Set
    End Property

#End Region

#Region "CheckPay"
    Private _CheckPay As String
    Public Property CheckPay() As String
        Get
            Return _CheckPay
        End Get
        Set(ByVal value As String)
            _CheckPay = value
        End Set
    End Property

#End Region

#Region "Bank"
    Private _Bank As String
    Public Property Bank() As String
        Get
            Return _Bank
        End Get
        Set(ByVal value As String)
            _Bank = value
        End Set
    End Property

#End Region

#Region "BankBranch"
    Private _BankBranch As String
    Public Property BankBranch() As String
        Get
            Return _BankBranch
        End Get
        Set(ByVal value As String)
            _BankBranch = value
        End Set
    End Property

#End Region

#Region "EnergyWHTax"
    Private _EnergyWHTax As Decimal
    Public Property EnergyWHTax() As Decimal
        Get
            Return _EnergyWHTax
        End Get
        Set(ByVal value As Decimal)
            _EnergyWHTax = value
        End Set
    End Property
#End Region

#Region "EnergyWHVAT"
    Private _EnergyWHVAT As Decimal
    Public Property EnergyWHVAT() As Decimal
        Get
            Return _EnergyWHVAT
        End Get
        Set(ByVal value As Decimal)
            _EnergyWHVAT = value
        End Set
    End Property
#End Region

#Region "ZeroRatedMarketFees"
    Private _ZeroRatedMarketFees As Boolean
    Public Property ZeroRatedMarketFees() As Boolean
        Get
            Return _ZeroRatedMarketFees
        End Get
        Set(ByVal value As Boolean)
            _ZeroRatedMarketFees = value
        End Set
    End Property

#End Region

#Region "ZeroRatedEnergy"
    Private _ZeroRatedEnergy As Boolean
    Public Property ZeroRatedEnergy() As Boolean
        Get
            Return _ZeroRatedEnergy
        End Get
        Set(ByVal value As Boolean)
            _ZeroRatedEnergy = value
        End Set
    End Property

#End Region

#Region "VirtualAccountNo"
    Private _VirtualAccountNo As String
    Public Property VirtualAccountNo() As String
        Get
            Return _VirtualAccountNo
        End Get
        Set(ByVal value As String)
            _VirtualAccountNo = value
        End Set
    End Property

#End Region

#Region "BIR Alphanumeric Tax Code"
    Private _BIRATCType As String
    Public Property BIRATCType() As String
        Get
            Return _BIRATCType
        End Get
        Set(ByVal value As String)
            _BIRATCType = value
        End Set
    End Property
#End Region

#Region "MembershipType"
    Private _MembershipType As String
    Public Property MembershipType() As String
        Get
            Return _MembershipType
        End Get
        Set(ByVal value As String)
            _MembershipType = value
        End Set
    End Property
#End Region

End Class

