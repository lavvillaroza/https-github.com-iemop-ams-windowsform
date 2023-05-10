'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             JournalVoucher
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 29, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for Journal Voucher
'Arguments/Parameters:  
'Files/Database Tables:  AM_JV
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   September 29, 2011      Juan Carlo L. Panopio           Class initialization
'   October 03, 2011        Vladimir E. Espiritu            Revised the class initialization
'   February 14, 2012       Vladimir E. Espiritu            Added PostedType in properties
'   July 11, 2012           Juan carlo L. Panopio           Removed OffsetNumber


Option Strict On
Option Explicit On


Public Class JournalVoucher

#Region "Initialization/Constructor"

    Public Sub New()
        Me.New(0, Nothing, "", 1, New List(Of JournalVoucherDetails), "", "", "", "", Nothing, "", "", "")
    End Sub

    Public Sub New(ByVal jvnumber As Long, ByVal jvdate As Date, ByVal batchcode As String, _
                   ByVal status As Integer, ByVal jvdetails As List(Of JournalVoucherDetails), _
                   ByVal preparedby As String, ByVal checkedby As String, ByVal approvedby As String, ByVal updatedby As String, _
                   ByVal updateddate As Date, ByVal postedtype As String, ByVal GPRefNo As String, ByVal remarks As String)
        'ByVal offsetnumber As String,
        Me._JVNumber = jvnumber
        Me._JVDate = jvdate
        Me._BatchCode = batchcode
        ' Me._OffsetNumber = offsetnumber
        Me._Status = status
        Me._JVDetails = jvdetails
        Me._PreparedBy = preparedby
        Me._CheckedBy = checkedby
        Me._ApprovedBy = approvedby
        Me._UpdatedBy = updatedby
        Me._UpdatedDate = updateddate
        Me._PostedType = postedtype
        Me._GPRefNo = GPRefNo
        Me._Remarks = remarks
    End Sub

#End Region

#Region "AM_JV_NO"
    Private _JVNumber As Long
    Public Property JVNumber() As Long
        Get
            Return _JVNumber
        End Get
        Set(ByVal value As Long)
            _JVNumber = value
        End Set
    End Property
#End Region

#Region "AM_JV_DATE"
    Private _JVDate As Date
    Public Property JVDate() As Date
        Get
            Return _JVDate
        End Get
        Set(ByVal value As Date)
            _JVDate = value
        End Set
    End Property
#End Region

#Region "BATCH_CODE"
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

#Region "OFFSET_NO"
    Private _OffsetNumber As String
    Public Property OffsetNumber() As String
        Get
            Return _OffsetNumber
        End Get
        Set(ByVal value As String)
            _OffsetNumber = value
        End Set
    End Property
#End Region

#Region "Status"
    Private _Status As Integer
    Public Property Status() As Integer
        Get
            Return _Status
        End Get
        Set(ByVal value As Integer)
            _Status = value
        End Set
    End Property

#End Region

#Region "Posted Type"
    Private _PostedType As String
    Public Property PostedType() As String
        Get
            Return _PostedType
        End Get
        Set(ByVal value As String)
            _PostedType = value
        End Set
    End Property

#End Region

#Region "Remarks Type"
    Private _Remarks As String
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property

#End Region

#Region "JVDetails"
    Private _JVDetails As List(Of JournalVoucherDetails)
    Public Property JVDetails() As List(Of JournalVoucherDetails)
        Get
            Return _JVDetails
        End Get
        Set(ByVal value As List(Of JournalVoucherDetails))
            _JVDetails = value
        End Set
    End Property

#End Region

#Region "GP Reference No"

    Private _GPRefNo As String
    Public Property GPRefNo() As String
        Get
            Return _GPRefNo
        End Get
        Set(ByVal value As String)
            _GPRefNo = value
        End Set
    End Property

#End Region

#Region "PREPARED BY"
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

#Region "Checked by"
    Private _CheckedBy As String
    Public Property CheckedBy() As String
        Get
            Return _CheckedBy
        End Get
        Set(ByVal value As String)
            _CheckedBy = value
        End Set
    End Property
#End Region

#Region "APPROVED BY"
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
