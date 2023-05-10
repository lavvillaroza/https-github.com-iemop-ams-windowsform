'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             CollectionNotice
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 20, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for Collection Notice
'Arguments/Parameters:  
'Files/Database Tables:  AM_COLLECTION_NOTICE
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   September 20, 2011      Juan Carlo L. Panopio           Class initialization
'	


Option Strict On
Option Explicit On

Public Class CollectionNotice
    Implements ICloneable

    Public Sub New()
        Me.New(Nothing, 0, 0, "", "", "", Nothing, Nothing, Nothing, "", EnumStatus.Active)
    End Sub

    Public Sub New(ByVal CollectionNoticeDate As Date, ByVal ReferenceNumber As Long, ByVal CollectionNoticeNumber As Long, _
                   ByVal PreparedBy As String, ByVal ApprovedBy As String, ByVal UpdatedBy As String, ByVal UpdatedDate As Date, ByVal DueDate As Date, _
                   ByVal GenerateFlag As EnumCNFlag, ByVal _CheckedBy As String, ByVal Status As EnumStatus, Optional ByVal Remarks As String = "")

        Me._CNDate = CollectionNoticeDate
        Me._ReferenceNo = ReferenceNumber
        Me._CNNumber = CollectionNoticeNumber
        Me._Remarks = Remarks
        Me._PreparedBy = PreparedBy
        Me._ApprovedBy = ApprovedBy
        Me._UpdatedBy = UpdatedBy
        Me._UpdatedDate = UpdatedDate
        Me._DueDate = DueDate
        Me._GenerateFlag = GenerateFlag
        Me._CheckedBy = CheckedBy
        Me._Status = Status
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return New CollectionNotice With {.IDNumber = IDNumber, .CNDate = CNDate, .CNNumber = CNNumber, .ReferenceNo = ReferenceNo, .Remarks = Remarks, _
                                            .PreparedBy = PreparedBy, .ApprovedBy = ApprovedBy, .UpdatedBy = UpdatedBy, .UpdatedDate = UpdatedDate, .DueDate = DueDate, .GenerateFlag = GenerateFlag, _
                                            .Status = Status, .CheckedBy = CheckedBy}
    End Function

#Region "Collection Notice Date"
    Private _CNDate As Date
    Public Property CNDate() As Date
        Get
            Return _CNDate
        End Get
        Set(ByVal value As Date)
            _CNDate = value
        End Set
    End Property
#End Region

#Region "Reference number"
    Private _ReferenceNo As Long
    Public Property ReferenceNo() As Long
        Get
            Return _ReferenceNo
        End Get
        Set(ByVal value As Long)
            _ReferenceNo = value
        End Set
    End Property
#End Region

#Region "Collection Notice Number"
    Private _CNNumber As Long
    Public Property CNNumber() As Long
        Get
            Return _CNNumber
        End Get
        Set(ByVal value As Long)
            _CNNumber = value
        End Set
    End Property
#End Region

#Region "ID  number"
    Private _IDNumber As String
    Public Property IDNumber() As String
        Get
            Return _IDNumber
        End Get
        Set(ByVal value As String)
            _IDNumber = value
        End Set
    End Property
#End Region

#Region "Remarks"
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

#Region "Prepared BY"
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

#Region "Approved BY"
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

#Region "Due Date"
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


    Private _GenerateFlag As EnumCNFlag
    Public Property GenerateFlag() As EnumCNFlag
        Get
            Return _GenerateFlag
        End Get
        Set(ByVal value As EnumCNFlag)
            _GenerateFlag = value
        End Set
    End Property


    Private _Status As EnumStatus
    Public Property Status() As EnumStatus
        Get
            Return _Status
        End Get
        Set(ByVal value As EnumStatus)
            _Status = value
        End Set
    End Property


    Private _CheckedBy As String
    Public Property CheckedBy() As String
        Get
            Return _CheckedBy
        End Get
        Set(ByVal value As String)
            _CheckedBy = value
        End Set
    End Property


End Class
