'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             OfficialReceiptReportMain
'Orginal Author:         Joseph B. Gabriel
'File Creation Date:     June 09,2015
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
'   
'

Public Class DeferredMonitoring

#Region "Initialization/Constructor"
    Public Sub New()

    End Sub

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


End Class
