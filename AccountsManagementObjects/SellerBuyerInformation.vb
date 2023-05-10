Public Class SellerBuyerInformation

    Private _StlID As String
    Public Property StlID() As String
        Get
            Return _StlID
        End Get
        Set(ByVal value As String)
            _StlID = value.ToUpper
        End Set
    End Property
    Private _BillingID As String
    Public Property BillingId() As String
        Get
            Return _BillingID
        End Get
        Set(ByVal value As String)
            _BillingID = value.ToUpper
        End Set
    End Property

    Private _InvoiceID As String
    Public Property InvoiceID() As String
        Get
            Return _InvoiceID
        End Get
        Set(ByVal value As String)
            _InvoiceID = value.ToUpper
        End Set
    End Property

    Private _FacilityType As String
    Public Property FacilityType() As String
        Get
            Return _FacilityType
        End Get
        Set(ByVal value As String)
            _FacilityType = value.ToUpper
        End Set
    End Property

    Private _WHTTag As String
    Public Property WHTTag() As String
        Get
            Return _WHTTag
        End Get
        Set(ByVal value As String)
            _WHTTag = value.ToUpper
        End Set
    End Property

    Private _ITHTag As String
    Public Property ITHTag() As String
        Get
            Return _ITHTag
        End Get
        Set(ByVal value As String)
            _ITHTag = value.ToUpper
        End Set
    End Property

    Private _NonVatableTag As String
    Public Property NonVatableTag() As String
        Get
            Return _NonVatableTag
        End Get
        Set(ByVal value As String)
            _NonVatableTag = value.ToUpper
        End Set
    End Property

    Private _ZeroRatedTag As String
    Public Property ZeroRatedTag() As String
        Get
            Return _ZeroRatedTag
        End Get
        Set(ByVal value As String)
            _ZeroRatedTag = value.ToUpper
        End Set
    End Property

    Private _NetSellerBuyerTag As String
    Public Property NetSellerBuyerTag() As String
        Get
            Return _NetSellerBuyerTag
        End Get
        Set(ByVal value As String)
            _NetSellerBuyerTag = value.ToUpper
        End Set
    End Property

End Class
