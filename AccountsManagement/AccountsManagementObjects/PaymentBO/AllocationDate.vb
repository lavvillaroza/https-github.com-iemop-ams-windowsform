Option Explicit On
Option Strict On

Public Class AllocationDate
    Public Sub New(Allocdate As Date, PayNo As Long, Optional ByVal RemittanceDate As Date = Nothing)
        Me._PaymentNo = PayNo
        Me._CollAllocationDate = Allocdate
        Me._RemittanceDate = RemittanceDate
    End Sub

    Public Sub New()
        Me._PaymentNo = 0
        Me._CollAllocationDate = Nothing
        Me._RemittanceDate = Nothing
    End Sub

    Private _CollAllocationDate As Date
    Public Property CollAllocationDate() As Date
        Get
            Return _CollAllocationDate
        End Get
        Set(value As Date)
            _CollAllocationDate = value
        End Set
    End Property

    Private _PaymentNo As Long
    Public Property PaymentNo() As Long
        Get
            Return _PaymentNo
        End Get
        Set(value As Long)
            _PaymentNo = value
        End Set
    End Property

    Private _RemittanceDate As Date
    Public Property RemittanceDate() As Date
        Get
            Return _RemittanceDate
        End Get
        Set(value As Date)
            _RemittanceDate = value
        End Set
    End Property
End Class