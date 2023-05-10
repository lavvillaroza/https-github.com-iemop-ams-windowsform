'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             ParticipantLineage
'Orginal Author:         Juan Carlo Panopio
'File Creation Date:     January 4, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for Payments
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

Public Class Payment

#Region "Initialization"
    Implements ICloneable

    Public Sub New()
        Me.New(Nothing, Nothing, 0, 0, 0, 0, 0, 0, Nothing, "", Nothing, Nothing, 0, 0, 0, Nothing, "", Nothing)
    End Sub

    Public Sub New(ByVal CollectionAllocDate As Date, ByVal PaymentBatchCode As String, ByVal BillPd As Integer, ByVal Total_ECollection As Decimal, _
                   ByVal Total_DICollection As Decimal, ByVal Total_VATCollection As Decimal, ByVal Total_NSSRA As Decimal, _
                   ByVal TotalEnergyPayment As Decimal, ByVal Status As EnumCollectionStatus, ByVal UpdatedBy As String, ByVal UpdatedDate As Date, _
                   ByVal DueDate As Date, ByVal APEnergy As Decimal, ByVal APVAT As Decimal, ByVal NSSBalance As Decimal, ByVal PaymentAllocationDate As Date, _
                   ByVal PaymentPerBPNo As String, ByVal PaymentDate As Date)
        Me._CollectionAllocationDate = CollectionAllocDate
        Me._BillingPeriod = BillPd
        Me._PaymentBatchCode = PaymentBatchCode
        Me._DueDate = DueDate
        Me._Total_EnergyCollection = Total_ECollection
        Me._Total_DICollection = Total_DICollection
        Me._Total_VATCollection = Total_VATCollection
        Me._Total_NSSRA = Total_NSSRA
        Me._EnergyPayment = TotalEnergyPayment
        Me._Status = Status
        Me._UpdatedBy = UpdatedBy
        Me._UpdatedDate = UpdatedDate
        Me._TotalAPEnergy = APEnergy
        Me._TotalAPVAT = APVAT
        Me._NSSBalance = NSSBalance
        Me._PaymentAllocationDate = PaymentAllocationDate
        Me._PaymentPerBPNo = PaymentPerBPNo
        Me._PaymentDate = PaymentDate
    End Sub

    Public Sub New(ByVal CollectionAllocDate As Date, ByVal PaymentBatchCode As String, ByVal BillPd As Integer, ByVal Total_ECollection As Decimal, _
                   ByVal Total_DICollection As Decimal, ByVal Total_VATCollection As Decimal, ByVal Total_NSSRA As Decimal, _
                   ByVal TotalEnergyPayment As Decimal, ByVal Status As EnumCollectionStatus, ByVal UpdatedBy As String, ByVal UpdatedDate As Date, _
                   ByVal DueDate As Date, ByVal APEnergy As Decimal, ByVal APVAT As Decimal, ByVal NSSBalance As Decimal, ByVal PaymentAllocationDate As Date, _
                   ByVal PaymentPerBPNo As String)
        Me._CollectionAllocationDate = CollectionAllocDate
        Me._BillingPeriod = BillPd
        Me._PaymentBatchCode = PaymentBatchCode
        Me._DueDate = DueDate
        Me._Total_EnergyCollection = Total_ECollection
        Me._Total_DICollection = Total_DICollection
        Me._Total_VATCollection = Total_VATCollection
        Me._Total_NSSRA = Total_NSSRA
        Me._EnergyPayment = TotalEnergyPayment
        Me._Status = Status
        Me._UpdatedBy = UpdatedBy
        Me._UpdatedDate = UpdatedDate
        Me._TotalAPEnergy = APEnergy
        Me._TotalAPVAT = APVAT
        Me._NSSBalance = NSSBalance
        Me._PaymentAllocationDate = PaymentAllocationDate
        Me._PaymentPerBPNo = PaymentPerBPNo
    End Sub

    Public Sub New(ByVal CollectionAllocDate As Date, ByVal PaymentBatchCode As String, ByVal BillPd As Integer, ByVal Total_ECollection As Decimal, _
               ByVal Total_DICollection As Decimal, ByVal Total_VATCollection As Decimal, ByVal Total_NSSRA As Decimal, _
               ByVal TotalEnergyPayment As Decimal, ByVal Status As EnumCollectionStatus, ByVal UpdatedBy As String, ByVal UpdatedDate As Date, ByVal APEnergy As Decimal, ByVal APVAT As Decimal, _
               ByVal NSSBalance As Decimal, ByVal PaymentAllocationDate As Date, ByVal PaymentPerBPNo As String)
        Me.CollectionAllocationDate = CollectionAllocDate
        Me._BillingPeriod = BillPd
        Me._PaymentBatchCode = PaymentBatchCode
        Me._DueDate = DueDate
        Me._Total_EnergyCollection = Total_ECollection
        Me._Total_DICollection = Total_DICollection
        Me._Total_VATCollection = Total_VATCollection
        Me._Total_NSSRA = Total_NSSRA
        Me._EnergyPayment = TotalEnergyPayment
        Me._Status = Status
        Me._TotalAPEnergy = APEnergy
        Me._TotalAPVAT = APVAT
        Me._UpdatedBy = UpdatedBy
        Me._UpdatedDate = UpdatedDate
        Me._NSSBalance = NSSBalance
        Me._PaymentAllocationDate = PaymentAllocationDate
        Me._PaymentPerBPNo = PaymentPerBPNo
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return New Payment With {.CollectionAllocationDate = CollectionAllocationDate, .PaymentBatchCode = PaymentBatchCode, .BillingPeriod = BillingPeriod, .DueDate = DueDate, _
                                  .Total_EnergyCollection = Total_EnergyCollection, .Total_DICollection = .Total_DICollection, .Total_VATCollection = Total_VATCollection, _
                                  .Total_NSSRA = Total_NSSRA, .EnergyPayment = EnergyPayment, .Status = Status, .TotalAPEnergy = TotalAPEnergy, .TotalAPVAT = TotalAPVAT, _
                                  .UpdatedBy = UpdatedBy, .UpdatedDate = UpdatedDate, .PaymentPerBPNo = PaymentPerBPNo, .PaymentDate = PaymentDate}
    End Function


#End Region

#Region "COLLECTION ALLOCATION DATE"
    Private _CollectionAllocationDate As Date
    Public Property CollectionAllocationDate() As Date
        Get
            Return _CollectionAllocationDate
        End Get
        Set(ByVal value As Date)
            _CollectionAllocationDate = value
        End Set
    End Property
#End Region

#Region "PAYMENT BATCH CODE"
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

#Region "Payment Per BP Number"
    Private _PaymentPerBPNo As String
    Public Property PaymentPerBPNo() As String
        Get
            Return _PaymentPerBPNo
        End Get
        Set(ByVal value As String)
            _PaymentPerBPNo = value
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

#Region "BillingPeriod"
    Private _BillingPeriod As Integer
    Public Property BillingPeriod() As Integer
        Get
            Return _BillingPeriod
        End Get
        Set(ByVal value As Integer)
            _BillingPeriod = value
        End Set
    End Property
#End Region

#Region "DueDate"
    Private _DueDate As Date
    Public Property DueDate() As Date
        Get
            Return _DueDate
        End Get
        Set(ByVal value As Date)
            _DueDate = value
        End Set
    End Property
#End Region

#Region "Total Energy Collection "
    Private _Total_EnergyCollection As Decimal
    Public Property Total_EnergyCollection() As Decimal
        Get
            Return _Total_EnergyCollection
        End Get
        Set(ByVal value As Decimal)
            _Total_EnergyCollection = value
        End Set
    End Property
#End Region

#Region "Total Default Interest (DI) Collection "
    Private _Total_DICollection As Decimal
    Public Property Total_DICollection() As Decimal
        Get
            Return _Total_DICollection
        End Get
        Set(ByVal value As Decimal)
            _Total_DICollection = value
        End Set
    End Property
#End Region

#Region "Total VAT Collection "
    Private _Total_VATCollection As Decimal
    Public Property Total_VATCollection() As Decimal
        Get
            Return _Total_VATCollection
        End Get
        Set(ByVal value As Decimal)
            _Total_VATCollection = value
        End Set
    End Property
#End Region

#Region "Total NSSRA Collection "
    Private _Total_NSSRA As Decimal
    Public Property Total_NSSRA() As Decimal
        Get
            Return _Total_NSSRA
        End Get
        Set(ByVal value As Decimal)
            _Total_NSSRA = value
        End Set
    End Property
#End Region

#Region "Total AP Energy "
    Private _TotalAPEnergy As Decimal
    Public Property TotalAPEnergy() As Decimal
        Get
            Return _TotalAPEnergy
        End Get
        Set(ByVal value As Decimal)
            _TotalAPEnergy = value
        End Set
    End Property
#End Region

#Region "Total AP VAT "
    Private _TotalAPVAT As Decimal
    Public Property TotalAPVAT() As Decimal
        Get
            Return _TotalAPVAT
        End Get
        Set(ByVal value As Decimal)
            _TotalAPVAT = value
        End Set
    End Property
#End Region

#Region "Energy Payment"
    Private _EnergyPayment As Decimal
    Public Property EnergyPayment() As Decimal
        Get
            Return _EnergyPayment
        End Get
        Set(ByVal value As Decimal)
            _EnergyPayment = value.ToString("#,##0.00")
        End Set
    End Property
#End Region

#Region "Status"
    Private _Status As EnumCollectionStatus
    Public Property Status() As EnumCollectionStatus
        Get
            Return _Status
        End Get
        Set(ByVal value As EnumCollectionStatus)
            _Status = value
        End Set
    End Property
#End Region

#Region "NSS DEDUCTED"
    Private _NSSBalance As Decimal
    Public Property NSSBalance() As Decimal
        Get
            Return _NSSBalance
        End Get
        Set(ByVal value As Decimal)
            _NSSBalance = value
        End Set
    End Property
#End Region

#Region "Payment Allocation Date"

    Private _PaymentAllocationDate As Date
    Public Property PaymentAllocationDate() As Date
        Get
            Return _PaymentAllocationDate
        End Get
        Set(ByVal value As Date)
            _PaymentAllocationDate = value
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

End Class
