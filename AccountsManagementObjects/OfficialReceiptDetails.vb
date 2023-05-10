'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             OfficialReceiptDetails
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     May 07, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Collection
'Arguments/Parameters:  
'Files/Database Tables:  AM_OFFICIAL_RECEIPT_DETAILS
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   May 07, 2012            Vladimir E. Espiritu            Class initialization
'   May 08, 2012            Vladimir E. Espiritu            Added description property 
'

Public Class OfficialReceiptDetails

#Region "Initialization/Constructor"
    Public Sub New()
        Me.New(0, "", "", 0, 0)
    End Sub


    Public Sub New(ByVal orno As Long, ByVal accountcode As String, ByVal description As String, _
                   ByVal debit As Decimal, ByVal credit As Decimal)
        Me._ORNo = orno
        Me._AccountCode = accountcode
        Me._Description = description
        Me._Debit = debit
        Me._Credit = credit
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

#Region "AccountCode"
    Private _AccountCode As String
    Public Property AccountCode() As String
        Get
            Return _AccountCode
        End Get
        Set(ByVal value As String)
            _AccountCode = value
        End Set
    End Property
#End Region

#Region "Description"
    Private _Description As String
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property

#End Region

#Region "Debit"
    Private _Debit As Decimal
    Public Property Debit() As Decimal
        Get
            Return _Debit
        End Get
        Set(ByVal value As Decimal)
            _Debit = value
        End Set
    End Property
#End Region

#Region "Credit"
    Private _Credit As Decimal
    Public Property Credit() As Decimal
        Get
            Return _Credit
        End Get
        Set(ByVal value As Decimal)
            _Credit = value
        End Set
    End Property
#End Region

End Class
