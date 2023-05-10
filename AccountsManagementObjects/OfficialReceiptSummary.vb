'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             OfficialReceiptSummary
'Orginal Author:         Vladimir E.Espiritu
'File Creation Date:     November 12, 2013
'Development Group:      Software Development and Support Division
'Description:            Class for Official Receipt Summary
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description
'   November 12, 2013       Vladimir E.Espiritu         Class initialization
'	

Option Explicit On
Option Strict On

Public Class OfficialReceiptSummary


#Region "Initialization/Constructor"
    Public Sub New()
        Me.New(0, 0, Nothing, 0, Nothing)
    End Sub

    Public Sub New(ByVal orno As Long, ByVal wbsummaryno As Long, ByVal duedate As Date, _
                   ByVal amount As Decimal, ByVal collectiontype As EnumCollectionType)
        Me._ORNo = orno
        Me._WESMBillSummaryNo = wbsummaryno
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
