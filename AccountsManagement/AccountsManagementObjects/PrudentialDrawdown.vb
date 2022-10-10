'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             PrudentialDrawdown
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     February 08, 2013
'Development Group:      Software Development and Support Division
'Description:            Class for Drawdown Notice
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   February 08, 2013       Vladimir E. Espiritu            Class initialization
'   May 24, 2013            Vladimir E. Espiritu            Added Collection Number
'

Public Class PrudentialDrawdown

#Region "Initialization/Constructor"
    Public Sub New()
        Me._CollectionNo = 0
        Me._TransactionDate = Nothing
        Me._DueDate = Nothing
        Me._IDNumber = New AMParticipants()
        Me._DrawdownAmount = 0
        Me._RemaningPrudential = 0
    End Sub
#End Region

#Region "CollectionNo"
    Private _CollectionNo As Long
    Public Property CollectionNo() As Long
        Get
            Return _CollectionNo
        End Get
        Set(ByVal value As Long)
            _CollectionNo = value
        End Set
    End Property

#End Region

#Region "TransactionDate"
    Private _TransactionDate As Date
    Public Property TransactionDate() As Date
        Get
            Return _TransactionDate
        End Get
        Set(ByVal value As Date)
            _TransactionDate = value
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

#Region "IDNumber"
    Private _IDNumber As AMParticipants
    Public Property IDNumber() As AMParticipants
        Get
            Return _IDNumber
        End Get
        Set(ByVal value As AMParticipants)
            _IDNumber = value
        End Set
    End Property

#End Region

#Region "DrawdownAmount"
    Private _DrawdownAmount As Decimal
    Public Property DrawdownAmount() As Decimal
        Get
            Return _DrawdownAmount
        End Get
        Set(ByVal value As Decimal)
            _DrawdownAmount = value
        End Set
    End Property

#End Region

#Region "RemaningPrudential"
    Private _RemaningPrudential As Decimal
    Public Property RemaningPrudential() As Decimal
        Get
            Return _RemaningPrudential
        End Get
        Set(ByVal value As Decimal)
            _RemaningPrudential = value
        End Set
    End Property

#End Region

End Class
