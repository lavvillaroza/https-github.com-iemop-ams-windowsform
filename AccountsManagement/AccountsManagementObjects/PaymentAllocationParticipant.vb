'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             ParticipantLineage
'Orginal Author:         Juan Carlo Panopio
'File Creation Date:     December 26, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for Payment Allocation
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
<Serializable()> _
Public Class PaymentAllocationParticipant
    Implements ICloneable

    Public Sub New()
        Me.PaymentBatchCode = ""
        Me.PaymentGroupCode = 0
        Me.BillPeriod = 0
        Me.DueDate = System.DateTime.Now
        Me.AllocDetails = New AllocatedPayment
        Me.Participant = New AMParticipants
        Me.UpdatedBy = ""
        Me.UpdatedDate = Nothing
        Me.DMCMNo = 0
    End Sub

#Region "Initialization"
    Public Sub New(ByVal PayBatchCode As String, ByVal PayGroupCode As Long, ByVal BillPeriod As Integer, ByVal DueDate As Date, ByVal PaymentAllocation As AllocatedPayment, _
                   ByVal ParticipantDetails As AMParticipants, _
                   ByVal UpdatedBy As String, ByVal UpdatedDate As Date, ByVal DMCMNumber As Long)
        Me._PaymentBatchCode = PayBatchCode
        Me._PaymentGroupCode = PayGroupCode
        Me._BillPeriod = BillPeriod
        Me._DueDate = DueDate
        Me._allocDetails = PaymentAllocation
        Me._Participant = ParticipantDetails
        Me._UpdatedBy = UpdatedBy
        Me._UpdatedDate = UpdatedDate
        Me._DMCMNo = DMCMNumber
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return New PaymentAllocationParticipant With {.PaymentBatchCode = PaymentBatchCode, .PaymentGroupCode = PaymentGroupCode, .BillPeriod = BillPeriod, _
                                                      .DueDate = DueDate, .AllocDetails = AllocDetails.Clone, .Participant = Participant, _
                                                       .UpdatedBy = UpdatedBy, .UpdatedDate = UpdatedDate, .DMCMNo = DMCMNo}
    End Function
#End Region

#Region "Payment Batch Code"
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

#Region "Payment Group Code"
    Private _PaymentGroupCode As Long
    Public Property PaymentGroupCode() As Long
        Get
            Return _PaymentGroupCode
        End Get
        Set(ByVal value As Long)
            _PaymentGroupCode = value
        End Set
    End Property
#End Region

#Region "Billing Period"
    Private _BillPeriod As Integer
    Public Property BillPeriod() As Integer
        Get
            Return _BillPeriod
        End Get
        Set(ByVal value As Integer)
            _BillPeriod = value
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

#Region "Allocated Payments"
    Private _allocDetails As AllocatedPayment
    Public Property AllocDetails() As AllocatedPayment
        Get
            Return _allocDetails
        End Get
        Set(ByVal value As AllocatedPayment)
            _allocDetails = value
        End Set
    End Property

#End Region

#Region "Participant Details"
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

#Region "DMCM Number"

    Private _DMCMNo As Long
    Public Property DMCMNo() As Long
        Get
            Return _DMCMNo
        End Get
        Set(ByVal value As Long)
            _DMCMNo = value
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
