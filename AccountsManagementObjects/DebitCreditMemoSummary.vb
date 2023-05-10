'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     June 19, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Debit/Credit Memo Summary
'Arguments/Parameters:  
'Files/Database Tables:       
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History


Public Class DebitCreditMemoSummary
    Implements IDisposable

#Region "Initialization"
    Public Sub New()
        Me.New(0, Nothing, New DebitCreditMemo, Nothing, "", "", "")
    End Sub

    Public Sub New(ByVal DMCMNumber As Integer, ByVal DMCMDate As Date, ByVal DMCMForSummary As DebitCreditMemo, ByVal SummaryType As EnumDMCMSummaryType, ByVal INVDMCMNo As String, _
                   ByVal PaymentBatchCode As String, ByVal IdNumber As String)
        Me._DMCMNo = DMCMNumber
        Me._DMCMDate = DMCMDate
        Me._DMCMDetails = DMCMForSummary
        Me._DMCMSummaryType = SummaryType
        Me._INVDMCMNo = INVDMCMNo
        Me._PaymentBatchCode = PaymentBatchCode
        Me._IDNumber = IdNumber
    End Sub
#End Region

#Region "DMCM Number"
    Private _DMCMNo As Long
    Public Property DMCMNo() As Long
        Get
            Return _DMCMNo
        End Get
        Set(ByVal value As Long)
            _DMCMNo = value
        End Set
    End Property

#End Region

#Region "ID NUMBER"

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

#Region "Payment Batch code"

    Private _PaymentBatchCode As String
    Public Property PaymentBatchCode() As String
        Get
            Return _PaymentBatchCode
        End Get
        Set(ByVal value As String)
            _PaymentBatchCode = value
        End Set
    End Property

#End Region

#Region "Date"
    Private _DMCMDate As Date
    Public Property DMCMDate() As Date
        Get
            Return _DMCMDate
        End Get
        Set(ByVal value As Date)
            _DMCMDate = value
        End Set
    End Property

#End Region

#Region "DMCM Details"

    Private _DMCMDetails As DebitCreditMemo
    Public Property DMCMDetails() As DebitCreditMemo
        Get
            Return _DMCMDetails
        End Get
        Set(ByVal value As DebitCreditMemo)
            _DMCMDetails = value
        End Set
    End Property

#End Region

#Region "DMCM Summary Type"

    Private _DMCMSummaryType As EnumDMCMSummaryType
    Public Property DMCMSummaryType() As EnumDMCMSummaryType
        Get
            Return _DMCMSummaryType
        End Get
        Set(ByVal value As EnumDMCMSummaryType)
            _DMCMSummaryType = value
        End Set
    End Property

#End Region

#Region "Invoice/DMCM Number"

    Private _INVDMCMNo As String
    Public Property INVDMCMNo() As String
        Get
            Return _INVDMCMNo
        End Get
        Set(ByVal value As String)
            _INVDMCMNo = value
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
