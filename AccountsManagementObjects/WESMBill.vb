'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             WESMBill
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     September 14, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for WESM Bills
'Arguments/Parameters:  
'Files/Database Tables:  WESM Bill file coming from BSMD in csv format. AM_WESM_BILL and WESM_BILL_AUDIT database table
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   September 14, 2011      Vladimir E. Espiritu            Class initialization
'   September 16, 2011      Vladimir E. Espiritu            Added InvoiceDate property
'   September 20, 2011      Vladimir E. Espiritu            Added BatchCode property
'   September 21, 2011      Juan Carlo Panopio              Removed Balance property
'   October 30, 2012        Vladimir E. Espiritu            Added MarketFeesRate and Remarks properties
'   June 27, 2013           Vladimir E. Espiritu            Added Reference Number property
'   August 27, 2013         Vladimir E. Espiritu            Added RegistrationID, ForTheAccountOf and FullName properties
'   August 27, 2013         Vladimir E. Espiritu            Removed ReferenceNo
'

Option Explicit On
Option Strict On

<Serializable()> _
Public Class WESMBill
    Implements IDisposable

#Region "Initialization/Constructor"
    Public Sub New()
        Me._BatchCode = ""
        Me._AMCode = ""
        Me._BillingPeriod = Nothing
        Me._IDNumber = ""
        Me._RegistrationID = ""
        Me._ForTheAccountOf = ""
        Me._ForTheAccountOf = ""
        Me._SettlementRun = ""
        Me._InvoiceNumber = ""
        Me._InvoiceDate = Nothing
        Me._Amount = 0
        Me._ChargeType = Nothing
        Me._DueDate = Nothing
    End Sub
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

#Region "AMCode"
    Private _AMCode As String
    Public Property AMCode() As String
        Get
            Return _AMCode
        End Get
        Set(ByVal value As String)
            _AMCode = value
        End Set
    End Property

#End Region

#Region "BillingPeriod"
    Private _BillingPeriod As Integer
    Public Property BillingPeriod() As Integer
        Get
            Return _BillingPeriod
        End Get
        Set(ByVal value As Integer)
            _BillingPeriod = value
        End Set
    End Property
#End Region

#Region "SettlementRun"
    Private _SettlementRun As String
    Public Property SettlementRun() As String
        Get
            Return _SettlementRun
        End Get
        Set(ByVal value As String)
            _SettlementRun = value
        End Set
    End Property
#End Region

#Region "IDNumber"
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

#Region "RegistrationID"
    Private _RegistrationID As String
    Public Property RegistrationID() As String
        Get
            Return _RegistrationID
        End Get
        Set(ByVal value As String)
            _RegistrationID = value
        End Set
    End Property

#End Region

#Region "ForTheAccountOf"
    Private _ForTheAccountOf As String
    Public Property ForTheAccountOf() As String
        Get
            Return _ForTheAccountOf
        End Get
        Set(ByVal value As String)
            _ForTheAccountOf = value
        End Set
    End Property

#End Region

#Region "FullName"
    Private _FullName As String
    Public Property FullName() As String
        Get
            Return _FullName
        End Get
        Set(ByVal value As String)
            _FullName = value
        End Set
    End Property

#End Region

#Region "InvoiceNumber"
    Private _InvoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return _InvoiceNumber
        End Get
        Set(ByVal value As String)
            _InvoiceNumber = value
        End Set
    End Property
#End Region

#Region "InvoiceDate"
    Private _InvoiceDate As Date
    Public Property InvoiceDate() As Date
        Get
            Return _InvoiceDate
        End Get
        Set(ByVal value As Date)
            _InvoiceDate = value
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

#Region "ChargeType"
    Private _ChargeType As EnumChargeType
    Public Property ChargeType() As EnumChargeType
        Get
            Return _ChargeType
        End Get
        Set(ByVal value As EnumChargeType)
            _ChargeType = value
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

#Region "MarketFeesRate"
    Private _MarketFeesRate As Decimal
    Public Property MarketFeesRate() As Decimal
        Get
            Return _MarketFeesRate
        End Get
        Set(ByVal value As Decimal)
            _MarketFeesRate = value
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
