﻿Public Class WHTaxCertificateSTL
    Private _CertificateNo As Long
    Public Property CertificateNo() As Long
        Get
            Return _CertificateNo
        End Get
        Set(ByVal value As Long)
            _CertificateNo = value
        End Set
    End Property

    Private _RemittanceDate As Date
    Public Property RemittanceDate() As Date
        Get
            Return _RemittanceDate
        End Get
        Set(ByVal value As Date)
            _RemittanceDate = value
        End Set
    End Property

    Private _BillingIDNumber As AMParticipants
    Public Property BillingIDNumber() As AMParticipants
        Get
            Return _BillingIDNumber
        End Get
        Set(ByVal value As AMParticipants)
            _BillingIDNumber = value
        End Set
    End Property

    Private _CollectedAmount As Decimal
    Public Property CollectedAmount() As Decimal
        Get
            Return _CollectedAmount
        End Get
        Set(ByVal value As Decimal)
            _CollectedAmount = value
        End Set
    End Property

    Private _TagDetails As List(Of WHTaxCertificateDetails)
    Public Property TagDetails() As List(Of WHTaxCertificateDetails)
        Get
            Return _TagDetails
        End Get
        Set(ByVal value As List(Of WHTaxCertificateDetails))
            _TagDetails = value
        End Set
    End Property

    Private _AllocationDetails As List(Of WHTaxCertificateDetails)
    Public Property AllocationDetails() As List(Of WHTaxCertificateDetails)
        Get
            Return _AllocationDetails
        End Get
        Set(ByVal value As List(Of WHTaxCertificateDetails))
            _AllocationDetails = value
        End Set
    End Property

    Private _AllocatedToAP As String
    Public Property AllocatedToAP() As String
        Get
            Return _AllocatedToAP
        End Get
        Set(ByVal value As String)
            _AllocatedToAP = value
        End Set
    End Property

    Private _UntagEWT As String
    Public Property UntagEWT() As String
        Get
            Return _UntagEWT
        End Get
        Set(ByVal value As String)
            _UntagEWT = value
        End Set
    End Property

End Class
