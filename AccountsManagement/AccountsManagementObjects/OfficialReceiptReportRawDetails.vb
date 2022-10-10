'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             OfficialReceiptReportRawDetails
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     November 18, 2013
'Development Group:      Software Development and Support Division
'Description:            Class for Collection
'Arguments/Parameters:  
'Files/Database Tables:  NONE
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   November 18, 2012       Vladimir E. Espiritu            Class initialization
'

Public Class OfficialReceiptReportRawDetails

#Region "Initialization/Constructor"
    Public Sub New()
        Me.New(0, 0, EnumDocumentType.None, Nothing, 0, Nothing, 0, Nothing)
    End Sub

    Public Sub New(ByVal orno As Long, ByVal documentno As Long, ByVal documenttype As EnumDocumentType, ByVal documentdate As Date, ByVal wbillno As Long, _
                   ByVal duedate As Date, ByVal amount As Decimal, ByVal collectiontype As EnumCollectionType)
        Me._ORNo = orno
        Me._DocumentNo = documentno
        Me._DocumentType = documenttype
        Me._DocumentDate = documentdate
        Me._WESMBillSummaryNo = wbillno
        Me._DueDate = duedate
        Me._Amount = amount
        Me._CollectionType = collectiontype
    End Sub
#End Region


#Region "ORNo"
    Private _ORNo As Long
    Public Property ORNo() As Long
        Get
            Return _ORNo
        End Get
        Set(ByVal value As Long)
            _ORNo = value
        End Set
    End Property

#End Region

#Region "DocumentNo"
    Private _DocumentNo As Long
    Public Property DocumentNo() As Long
        Get
            Return _DocumentNo
        End Get
        Set(ByVal value As Long)
            _DocumentNo = value
        End Set
    End Property


#End Region

#Region "DocumentType"
    Private _DocumentType As EnumDocumentType
    Public Property DocumentType() As EnumDocumentType
        Get
            Return _DocumentType
        End Get
        Set(ByVal value As EnumDocumentType)
            _DocumentType = value
        End Set
    End Property

#End Region

#Region "DocumentDate"
    Private _DocumentDate As Date
    Public Property DocumentDate() As Date
        Get
            Return _DocumentDate
        End Get
        Set(ByVal value As Date)
            _DocumentDate = value
        End Set
    End Property

#End Region

#Region "WESMBillSummaryNo"
    Private _WESMBillSummaryNo As Long
    Public Property WESMBillSummaryNo() As Long
        Get
            Return _WESMBillSummaryNo
        End Get
        Set(ByVal value As Long)
            _WESMBillSummaryNo = value
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

#Region "CollectionType"
    Private _CollectionType As EnumCollectionType
    Public Property CollectionType() As EnumCollectionType
        Get
            Return _CollectionType
        End Get
        Set(ByVal value As EnumCollectionType)
            _CollectionType = value
        End Set
    End Property

#End Region




End Class
