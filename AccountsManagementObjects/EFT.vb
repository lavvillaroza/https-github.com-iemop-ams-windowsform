'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             EFT
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     May 16, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for EFT (Electronic Fund Transfer)
'Arguments/Parameters:  
'Files/Database Tables:  AM_PEMC_PAYMENT
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description
'   

Public Class EFT
    Implements IDisposable

    Public Sub New()
        Me.New(Nothing, Nothing, Nothing, 0)
    End Sub

    Public Sub New(ByVal AllocationDate As Date, ByVal ParticipantDetails As AMParticipants, _
                   ByVal PaymentType As EnumParticipantPaymentType, ByVal ExcessCollection As Decimal)

        Me.New(AllocationDate, ParticipantDetails, PaymentType, 0, ExcessCollection, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", Nothing, 0, 0, 0, 0)
    End Sub

    Public Sub New(ByVal AllocationDate As Date, ByVal ParticipantDetails As AMParticipants, ByVal PaymentType As EnumParticipantPaymentType, _
                   ByVal CheckNumber As Long, ByVal ExcessCollection As Decimal, ByVal DeferredEnergy As Decimal, ByVal DeferredVAT As Decimal, ByVal Energy As Decimal, ByVal VAT As Decimal, _
                   ByVal MF As Decimal, ByVal ReturnAmount As Decimal, ByVal TotalPayment As Decimal, _
                   ByVal TransferPrudential As Decimal, ByVal TransferFinPen As Decimal, ByVal UpdatedBy As String, _
                   ByVal UpdatedDate As Date, ByVal STLInterest As Decimal, ByVal NSSInterest As Decimal, ByVal OffsetOnDeferredEnergy As Decimal, ByVal OffsetOnDeferredVat As Decimal)


        Me._AllocationDate = AllocationDate
        Me._Participant = ParticipantDetails
        Me._PaymentType = PaymentType
        Me._CheckNumber = CheckNumber
        Me._ExcessCollection = ExcessCollection
        Me._DeferredEnergy = DeferredEnergy
        Me._DeferredVAT = DeferredVAT
        Me._Energy = Energy
        Me._VAT = VAT
        Me._MarketFees = MF
        Me._ReturnAmount = ReturnAmount
        Me._TotalPayment = TotalPayment
        Me._UpdatedBy = UpdatedBy
        Me._UpdatedDate = UpdatedDate
        Me._TransferPrudential = TransferPrudential
        Me._TransferFinPen = TransferFinPen
        Me._STLInterest = STLInterest
        Me._NSSInterest = NSSInterest
        Me._OffsetOnDeferredEnergy = OffsetOnDeferredEnergy
        Me._OffsetOnDeferredVAT = OffsetOnDeferredVat
    End Sub

#Region "Allocation Date"
    Private _AllocationDate As Date
    Public Property AllocationDate() As Date
        Get
            Return _AllocationDate
        End Get
        Set(ByVal value As Date)
            _AllocationDate = value
        End Set
    End Property

#End Region

#Region "ID Number"
    Private _Participant As AMParticipants
    Public Property Participant() As AMParticipants
        Get
            Return _Participant
        End Get
        Set(ByVal value As AMParticipants)
            _Participant = value
        End Set
    End Property

#End Region

#Region "Payment Type"
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

#Region "CheckNumber"
    Private _CheckNumber As Long
    Public Property CheckNumber() As Long
        Get
            Return _CheckNumber
        End Get
        Set(ByVal value As Long)
            _CheckNumber = value
        End Set
    End Property
#End Region

#Region "Excess Collection"
    Private _ExcessCollection As Decimal
    Public Property ExcessCollection() As Decimal
        Get
            Return _ExcessCollection
        End Get
        Set(ByVal value As Decimal)
            _ExcessCollection = value
        End Set
    End Property
#End Region

#Region "Deferred Energy"
    Private _DeferredEnergy As Decimal
    Public Property DeferredEnergy() As Decimal
        Get
            Return _DeferredEnergy
        End Get
        Set(ByVal value As Decimal)
            _DeferredEnergy = value
        End Set
    End Property
#End Region

#Region "Deferred VAT"
    Private _DeferredVAT As Decimal
    Public Property DeferredVAT() As Decimal
        Get
            Return _DeferredVAT
        End Get
        Set(ByVal value As Decimal)
            _DeferredVAT = value
        End Set
    End Property
#End Region

#Region "Offset On Deferred Energy"
    Private _OffsetOnDeferredEnergy As Decimal
    Public Property OffsetOnDeferredEnergy() As Decimal
        Get
            Return _OffsetOnDeferredEnergy
        End Get
        Set(ByVal value As Decimal)
            _OffsetOnDeferredEnergy = value
        End Set
    End Property
#End Region

#Region "Offset On Deferred VAT"
    Private _OffsetOnDeferredVAT As Decimal
    Public Property OffsetOnDeferredVAT() As Decimal
        Get
            Return _OffsetOnDeferredVAT
        End Get
        Set(ByVal value As Decimal)
            _OffsetOnDeferredVAT = value
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

#Region "VAT"
    Private _VAT As Decimal
    Public Property VAT() As Decimal
        Get
            Return _VAT
        End Get
        Set(ByVal value As Decimal)
            _VAT = value
        End Set
    End Property
#End Region

#Region "MF"

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

#Region "ReturnAmount"

    Private _ReturnAmount As Decimal
    Public Property ReturnAmount() As Decimal
        Get
            Return _ReturnAmount
        End Get
        Set(ByVal value As Decimal)
            _ReturnAmount = value
        End Set
    End Property

#End Region

#Region "Total Payment"

    Private _TotalPayment As Decimal
    Public Property TotalPayment() As Decimal
        Get
            Return _TotalPayment
        End Get
        Set(ByVal value As Decimal)
            _TotalPayment = value
        End Set
    End Property

#End Region

#Region "Transfer to Prudential"

    Private _TransferPrudential As Decimal
    Public Property TransferPrudential() As Decimal
        Get
            Return _TransferPrudential
        End Get
        Set(ByVal value As Decimal)
            _TransferPrudential = value
        End Set
    End Property

#End Region

#Region "Prudential Refund"
    Private _PrudentialRefund As Decimal
    Public Property PrudentialRefund() As Decimal
        Get
            Return _PrudentialRefund
        End Get
        Set(ByVal value As Decimal)
            _PrudentialRefund = value
        End Set
    End Property
#End Region


#Region "Transfer to Financial Penalty"

    Private _TransferFinPen As Decimal
    Public Property TransferFinPen() As Decimal
        Get
            Return _TransferFinPen
        End Get
        Set(ByVal value As Decimal)
            _TransferFinPen = value
        End Set
    End Property

#End Region


    Private _NSSInterest As Decimal
    Public Property NSSInterest() As Decimal
        Get
            Return _NSSInterest
        End Get
        Set(ByVal value As Decimal)
            _NSSInterest = value
        End Set
    End Property


    Private _STLInterest As Decimal
    Public Property STLInterest() As Decimal
        Get
            Return _STLInterest
        End Get
        Set(ByVal value As Decimal)
            _STLInterest = value
        End Set
    End Property



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

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
