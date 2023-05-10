'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             Request For Payment
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     May 22, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Request For Payment
'Arguments/Parameters:  
'Files/Database Tables:  AM_REQUEST_FOR_PAYMENT
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description

Public Class RequestForPaymentDetails
    Implements IDisposable

    Public Sub New()
        Me.New(0, Nothing, 0, Nothing, "", Nothing, Nothing)
    End Sub

    Public Sub New(ByVal ReferenceNo As Long, ByVal Participant As String, ByVal Amount As Decimal, ByVal DepositDate As String, ByVal Particulars As String, _
                   ByVal AllocationDate As Date, ByVal ParticipantPayType As EnumParticipantPaymentType)
        Me._ReferenceNo = ReferenceNo
        Me._Participant = Participant
        Me._Amount = Amount
        Me._DateOfDeposit = DepositDate
        Me._Particulars = Particulars
        Me._AllocationDate = AllocationDate
        Me._PaymentType = ParticipantPayType
    End Sub

#Region "Reference No"

    Private _ReferenceNo As String
    Public Property ReferenceNo() As Long
        Get
            Return _ReferenceNo
        End Get
        Set(ByVal value As Long)
            _ReferenceNo = value
        End Set
    End Property

#End Region

#Region "Participant"

    Private _Participant As String
    Public Property Participant() As String
        Get
            Return _Participant
        End Get
        Set(ByVal value As String)
            _Participant = value
        End Set
    End Property

#End Region

#Region "Bank Branch"
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

#Region "AccountNo"
    Private _AccountNo As String
    Public Property AccountNo() As String
        Get
            Return _AccountNo
        End Get
        Set(ByVal value As String)
            _AccountNo = value
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

#Region "Amount"

    Private _Amount As Decimal
    Public Property Amount() As Decimal
        Get
            Return _Amount
        End Get
        Set(ByVal value As Decimal)
            _Amount = value
        End Set
    End Property

#End Region

#Region "Date of Deposit"

    Private _DateOfDeposit As String
    Public Property DateOfDeposit() As String
        Get
            Return _DateOfDeposit
        End Get
        Set(ByVal value As String)
            _DateOfDeposit = value
        End Set
    End Property

#End Region

#Region "Particulars"

    Private _Particulars As String
    Public Property Particulars() As String
        Get
            Return _Particulars
        End Get
        Set(ByVal value As String)
            _Particulars = value
        End Set
    End Property

#End Region

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

#Region "RFPDetailsType"
    Private _RFPDetailsType As New EnumRFPDetailsType
    Public Property RFPDetailsType() As EnumRFPDetailsType
        Get
            Return _RFPDetailsType
        End Get
        Set(value As EnumRFPDetailsType)
            _RFPDetailsType = value
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
