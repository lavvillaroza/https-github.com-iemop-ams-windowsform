'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             OfficialReceiptReportDetails
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     November 18,2013
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

Public Class OfficialReceiptReportDetails

#Region "Initialization/Constructor"
    Public Sub New()
        Me.New("", "", "", 0, 0, 0, 0, 0, 0, 0)
    End Sub

    Public Sub New(ByVal documentno As String, ByVal documentdate As String, ByVal duedate As String, ByVal excesscollection As Decimal, _
                   ByVal energy As Decimal, ByVal vat As Decimal, ByVal defaultinterest As Decimal, ByVal marketfees As Decimal, _
                   ByVal withholdingtax As Decimal, ByVal withholdingvat As Decimal)
        Me._DocumentNo = documentno
        Me._DocumentDate = documentdate
        Me._DueDate = duedate
        Me._ExcessCollection = excesscollection
        Me._Energy = energy
        Me._VAT = vat
        Me._DefaultInterest = defaultinterest
        Me._MarketFees = marketfees
        Me._WithholdingTax = withholdingtax
        Me._WitholdingVAT = withholdingvat
    End Sub
#End Region


#Region "DocumentNo"
    Private _DocumentNo As String
    Public Property DocumentNo() As String
        Get
            Return _DocumentNo
        End Get
        Set(ByVal value As String)
            _DocumentNo = value
        End Set
    End Property
#End Region
   
#Region "DocumentDate"
    Private _DocumentDate As String
    Public Property DocumentDate() As String
        Get
            Return _DocumentDate
        End Get
        Set(ByVal value As String)
            _DocumentDate = value
        End Set
    End Property

#End Region

#Region "DueDate"
    Private _DueDate As String
    Public Property DueDate() As String
        Get
            Return _DueDate
        End Get
        Set(ByVal value As String)
            _DueDate = value
        End Set
    End Property

#End Region

#Region "ExcessCollection"
    Private _ExcessCollection As Decimal
    Public Property ExcessCollection() As Decimal
        Get
            Return _ExcessCollection
        End Get
        Set(ByVal value As Decimal)
            _ExcessCollection = value
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

#Region "DefaultInterest"
    Private _DefaultInterest As Decimal
    Public Property DefaultInterest() As Decimal
        Get
            Return _DefaultInterest
        End Get
        Set(ByVal value As Decimal)
            _DefaultInterest = value
        End Set
    End Property

#End Region

#Region "MarketFees"
    Private _MarketFees As Decimal
    Public Property MarketFees() As Decimal
        Get
            Return _MarketFees
        End Get
        Set(ByVal value As Decimal)
            _MarketFees = value
        End Set
    End Property

#End Region

#Region "WithholdingTax"
    Private _WithholdingTax As Decimal
    Public Property WithholdingTax() As Decimal
        Get
            Return _WithholdingTax
        End Get
        Set(ByVal value As Decimal)
            _WithholdingTax = value
        End Set
    End Property

#End Region

#Region "WitholdingVAT"
    Private _WitholdingVAT As Decimal
    Public Property WitholdingVAT() As Decimal
        Get
            Return _WitholdingVAT
        End Get
        Set(ByVal value As Decimal)
            _WitholdingVAT = value
        End Set
    End Property

#End Region

#Region "Total"
    Private _Total As Decimal
    Public ReadOnly Property Total() As Decimal
        Get
            _Total += Me.ExcessCollection + Me.Energy + Me.VAT _
                      + Me.DefaultInterest + Me.MarketFees + Me.WithholdingTax + Me.WitholdingVAT
            Return _Total
        End Get
    End Property

#End Region

End Class
