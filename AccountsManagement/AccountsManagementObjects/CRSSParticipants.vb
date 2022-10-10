Public Class CRSSParticipants
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

#Region "FacilityType"
    Private _FacilityType As String
    Public Property FacilityType() As String
        Get
            Return _FacilityType
        End Get
        Set(ByVal value As String)
            _FacilityType = value
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

#Region "Renewable"
    Private _Renewable As String
    Public Property Renewable() As String
        Get
            Return _Renewable
        End Get
        Set(ByVal value As String)
            _Renewable = value
        End Set
    End Property
#End Region

#Region "ZeroRated"
    Private _ZeroRated As String
    Public Property ZeroRated() As String
        Get
            Return _ZeroRated
        End Get
        Set(ByVal value As String)
            _ZeroRated = value
        End Set
    End Property
#End Region

#Region "IncomeTaxHoliday"
    Private _IncomeTaxHoliday As String
    Public Property IncomeTaxHoliday() As String
        Get
            Return _IncomeTaxHoliday
        End Get
        Set(ByVal value As String)
            _IncomeTaxHoliday = value
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

End Class
