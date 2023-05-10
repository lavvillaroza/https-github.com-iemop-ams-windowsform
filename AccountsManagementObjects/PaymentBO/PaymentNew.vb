Public Class PaymentNew

#Region "PaymentNo"
    Private _PaymentNo As Long
    Public Property PaymentNo() As Long
        Get
            Return _PaymentNo
        End Get
        Set(value As Long)
            _PaymentNo = value
        End Set
    End Property
#End Region

#Region "PaymentAllocationDate"
    Private _PaymentAllocationDate As Date
    Public Property PaymentAllocationDate() As Date
        Get
            Return _PaymentAllocationDate
        End Get
        Set(value As Date)
            _PaymentAllocationDate = value
        End Set
    End Property
#End Region

#Region "TotalofEnergyCash"
    Private _TotalEnergyCash As Decimal
    Public Property TotalEnergyCash() As Decimal
        Get
            Return _TotalEnergyCash
        End Get
        Set(value As Decimal)
            _TotalEnergyCash = value
        End Set
    End Property
#End Region

#Region "TotalofEnergyDICash"
    Private _TotalofEnergyDICash As Decimal
    Public Property TotalofEnergyDICash() As Decimal
        Get
            Return _TotalofEnergyDICash
        End Get
        Set(value As Decimal)
            _TotalofEnergyDICash = value
        End Set
    End Property
#End Region

#Region "TotalofEnergyPR"
    Private _TotalofEnergyPR As Decimal
    Public Property TotalofEnergyPR() As Decimal
        Get
            Return _TotalofEnergyPR
        End Get
        Set(value As Decimal)
            _TotalofEnergyPR = value
        End Set
    End Property
#End Region

#Region "TotalofEnergyDIPR"
    Private _TotalofEnergyDIPR As Decimal
    Public Property TotalofEnergyDIPR() As Decimal
        Get
            Return _TotalofEnergyDIPR
        End Get
        Set(value As Decimal)
            _TotalofEnergyDIPR = value
        End Set
    End Property
#End Region

#Region "TotalofVATCash"
    Private _TotalofVATCash As Decimal
    Public Property TotalofVATCash() As Decimal
        Get
            Return _TotalofVATCash
        End Get
        Set(value As Decimal)
            _TotalofVATCash = value
        End Set
    End Property
#End Region

#Region "TotalofNSSRA"
    Private _TotalofNSSRA As Decimal
    Public Property TotalofNSSRA() As Decimal
        Get
            Return _TotalofNSSRA
        End Get
        Set(value As Decimal)
            _TotalofNSSRA = value
        End Set
    End Property
#End Region

#Region "TotalofNSSBalance"
    Private _TotalofNSSBalance As Decimal
    Public Property TotalofNSSBalance() As Decimal
        Get
            Return _TotalofNSSBalance
        End Get
        Set(value As Decimal)
            _TotalofNSSBalance = value
        End Set
    End Property
#End Region

    '#Region "Status"
    '    Private _Status As Integer
    '    Public Property Status() As Integer
    '        Get
    '            Return _Status
    '        End Get
    '        Set(value As Integer)
    '            _Status = value
    '        End Set
    '    End Property
    '#End Region

#Region "PaymentBatchCodeForJV"
    Private _PaymentBatchCode As String
    Public Property PaymentBatchCode() As String
        Get
            Return _PaymentBatchCode
        End Get
        Set(value As String)
            _PaymentBatchCode = value
        End Set
    End Property
#End Region

#Region "Remarks"
    Private _Remarks As String
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(value As String)
            _Remarks = value
        End Set
    End Property
#End Region
End Class
