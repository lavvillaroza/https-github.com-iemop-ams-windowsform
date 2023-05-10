'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             FundTransferFormMain
'Orginal Author:         Vladimir E.Espiritu
'File Creation Date:     April 22, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Accounting Code
'Arguments/Parameters:  
'Files/Database Tables:  AM_FTF_MAIN
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description
'   April 22, 2012          Vladimir E. Espiritu        Class initialization
'   April 25, 2012          Vladimir E. Espiritu        Added BatchCode property
'   June 06, 2012           Vladimir E. Espiritu        Added Allocation Date and IsPosted properties
'	

Option Explicit On
Option Strict On

<Serializable()> _
Public Class FundTransferFormMain
    Implements IDisposable

#Region "Initialization/Constructor"

    Public Sub New()
        Me.New(0, Nothing, "", "", New List(Of FundTransferFormParticipant), New List(Of FundTransferFormDetails))
    End Sub

    Public Sub New(ByVal totalamount As Decimal, ByVal transtype As EnumFTFTransType, ByVal requestingapproval As String, _
                   ByVal approvedby As String, ByVal listftfparticipants As List(Of FundTransferFormParticipant), _
                   ByVal listftfdetails As List(Of FundTransferFormDetails))
        Me._RefNo = 0
        Me._BSMRefNo = ""
        Me._DRDate = Nothing
        Me._CRDate = Nothing
        Me._TotalAmount = totalamount
        Me._TransType = transtype
        Me._MaturityDate = Nothing
        Me._DaysOfInvestment = 0
        Me._InterestRate = 0
        Me._InterestEarned = 0
        Me._Status = EnumStatus.Active
        Me._RequestingApproval = requestingapproval
        Me._ApprovedBy = approvedby
        Me._ListOfFTFParticipants = listftfparticipants
        Me._ListOfFTFDetails = listftfdetails
        Me._BatchCode = ""
    End Sub

    Public Sub New(ByVal allocataiondate As Date, ByVal totalamount As Decimal, ByVal transtype As EnumFTFTransType, ByVal requestingapproval As String, _
                   ByVal approvedby As String, ByVal listftfparticipants As List(Of FundTransferFormParticipant), _
                   ByVal listftfdetails As List(Of FundTransferFormDetails))
        Me._AllocationDate = allocataiondate
        Me._RefNo = 0
        Me._BSMRefNo = ""
        Me._DRDate = Nothing
        Me._CRDate = Nothing
        Me._TotalAmount = totalamount
        Me._TransType = transtype
        Me._MaturityDate = Nothing
        Me._DaysOfInvestment = 0
        Me._InterestRate = 0
        Me._InterestEarned = 0
        Me._Status = EnumStatus.Active
        Me._RequestingApproval = requestingapproval
        Me._ApprovedBy = approvedby
        Me._ListOfFTFParticipants = listftfparticipants
        Me._ListOfFTFDetails = listftfdetails
        Me._BatchCode = ""
    End Sub

#End Region

#Region "AllocationDate"
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

#Region "BatchCode"
    Private _BatchCode As String
    Public Property BatchCode() As String
        Get
            Return _BatchCode
        End Get
        Set(ByVal value As String)
            _BatchCode = value
        End Set
    End Property

#End Region

#Region "RefNo"
    Private _RefNo As Long
    Public Property RefNo() As Long
        Get
            Return _RefNo
        End Get
        Set(ByVal value As Long)
            _RefNo = value
        End Set
    End Property

#End Region

#Region "BSMRefNo"
    Private _BSMRefNo As String
    Public Property BSMRefNo() As String
        Get
            Return _BSMRefNo
        End Get
        Set(ByVal value As String)
            _BSMRefNo = value
        End Set
    End Property

#End Region

#Region "DRDate"
    Private _DRDate As Date
    Public Property DRDate() As Date
        Get
            Return _DRDate
        End Get
        Set(ByVal value As Date)
            _DRDate = value
        End Set
    End Property

#End Region

#Region "CRDate"
    Private _CRDate As Date
    Public Property CRDate() As Date
        Get
            Return _CRDate
        End Get
        Set(ByVal value As Date)
            _CRDate = value
        End Set
    End Property

#End Region

#Region "TotalAmount"
    Private _TotalAmount As Decimal
    Public Property TotalAmount() As Decimal
        Get
            Return _TotalAmount
        End Get
        Set(ByVal value As Decimal)
            _TotalAmount = value
        End Set
    End Property

#End Region

#Region "TransType"
    Private _TransType As EnumFTFTransType
    Public Property TransType() As EnumFTFTransType
        Get
            Return _TransType
        End Get
        Set(ByVal value As EnumFTFTransType)
            _TransType = value
        End Set
    End Property

#End Region

#Region "MaturityDate"
    Private _MaturityDate As Date
    Public Property MaturityDate() As Date
        Get
            Return _MaturityDate
        End Get
        Set(ByVal value As Date)
            _MaturityDate = value
        End Set
    End Property

#End Region

#Region "DaysOfInvestment"
    Private _DaysOfInvestment As Integer
    Public Property DaysOfInvestment() As Integer
        Get
            Return _DaysOfInvestment
        End Get
        Set(ByVal value As Integer)
            _DaysOfInvestment = value
        End Set
    End Property

#End Region

#Region "InterestRate"
    Private _InterestRate As Decimal
    Public Property InterestRate() As Decimal
        Get
            Return _InterestRate
        End Get
        Set(ByVal value As Decimal)
            _InterestRate = value
        End Set
    End Property

#End Region

#Region "InterestEarned"
    Private _InterestEarned As Decimal
    Public Property InterestEarned() As Decimal
        Get
            Return _InterestEarned
        End Get
        Set(ByVal value As Decimal)
            _InterestEarned = value
        End Set
    End Property

#End Region

#Region "MaturityValue"
    Private _MaturityValue As Decimal
    Public Property MaturityValue() As Decimal
        Get
            Return _MaturityValue
        End Get
        Set(ByVal value As Decimal)
            _MaturityValue = value
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

#Region "RequestingApproval"
    Private _RequestingApproval As String
    Public Property RequestingApproval() As String
        Get
            Return _RequestingApproval
        End Get
        Set(ByVal value As String)
            _RequestingApproval = value
        End Set
    End Property

#End Region

#Region "ApprovedBy"
    Private _ApprovedBy As String
    Public Property ApprovedBy() As String
        Get
            Return _ApprovedBy
        End Get
        Set(ByVal value As String)
            _ApprovedBy = value
        End Set
    End Property

#End Region

#Region "ListOfFTFParticipants"
    Private _ListOfFTFParticipants As List(Of FundTransferFormParticipant)
    Public Property ListOfFTFParticipants() As List(Of FundTransferFormParticipant)
        Get
            Return _ListOfFTFParticipants
        End Get
        Set(ByVal value As List(Of FundTransferFormParticipant))
            _ListOfFTFParticipants = value
        End Set
    End Property

#End Region

#Region "ListOfFTFDetails"
    Private _ListOfFTFDetails As List(Of FundTransferFormDetails)
    Public Property ListOfFTFDetails() As List(Of FundTransferFormDetails)
        Get
            Return _ListOfFTFDetails
        End Get
        Set(ByVal value As List(Of FundTransferFormDetails))
            _ListOfFTFDetails = value
        End Set
    End Property

#End Region

#Region "PreparedBy"
    Private _PreparedBy As String
    Public Property PreparedBy() As String
        Get
            Return _PreparedBy
        End Get
        Set(ByVal value As String)
            _PreparedBy = value
        End Set
    End Property

#End Region

#Region "IsPosted"
    Private _IsPosted As EnumIsPosted
    Public Property IsPosted() As EnumIsPosted
        Get
            Return _IsPosted
        End Get
        Set(ByVal value As EnumIsPosted)
            _IsPosted = value
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
