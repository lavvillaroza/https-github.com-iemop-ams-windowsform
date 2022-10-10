'***************************************************************************
'Copyright 2016: Philippine Electricity Market Corporation(PEMC)
'Class Name:             Check
'Orginal Author:         Lance Arjay V. Villaroza
'File Creation Date:     February 2, 2016
'Development Group:      Software Development and Support Division
'Description:            Class for SPA
'Arguments/Parameters:  
'Files/Database Tables:  AM_SPA
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description


Public Class SPAMain
    Implements IDisposable

    Public Sub New()
        Me._SPANo = 0
        Me._ParticipantInfo = Nothing
        Me._FirstPaymentDate = Nothing
        Me._InMonths = 0        
        Me._InterestRate = 0
        Me._TotalPrincipalAmount = Nothing
        Me._TotalInterestAmount = Nothing        
    End Sub

#Region "Special Payment Agreement Number"
    Private _SPANo As Long
    Public Property SPANo() As Long
        Get
            Return _SPANo
        End Get
        Set(value As Long)
            _SPANo = value
        End Set
    End Property
#End Region

#Region "Participant Details"
    Private _ParticipantInfo As AMParticipants
    Public Property ParticipantInfo() As AMParticipants
        Get
            Return _ParticipantInfo
        End Get
        Set(value As AMParticipants)
            _ParticipantInfo = value
        End Set
    End Property
#End Region

#Region "First Payment Date"
    Private _FirstPaymentDate As Date
    Public Property FirstPaymentDate() As Date
        Get
            Return _FirstPaymentDate
        End Get
        Set(value As Date)
            _FirstPaymentDate = value
        End Set
    End Property
#End Region

#Region "Terms of loans(in Months)"
    Private _InMonths As Integer
    Public Property InMonths() As Integer
        Get
            Return _InMonths
        End Get
        Set(value As Integer)
            _InMonths = value
        End Set
    End Property
#End Region

#Region "Interest Rate"
    Private _InterestRate As Decimal
    Public Property InterestRate() As Decimal
        Get
            Return _InterestRate
        End Get
        Set(ByVal value As Decimal)
            _InterestRate = value
        End Set
    End Property
#End Region

#Region "Total Principal Amount"
    Private _TotalPrincipalAmount As Decimal
    Public Property TotalPrincipalAmount() As Decimal
        Get
            Return _TotalPrincipalAmount
        End Get
        Set(ByVal value As Decimal)
            _TotalPrincipalAmount = value
        End Set
    End Property
#End Region

#Region "Total Interest Amount"
    Private _TotalInterestAmount As Decimal
    Public Property TotalInterestAmount() As Decimal
        Get
            Return _TotalInterestAmount
        End Get
        Set(ByVal value As Decimal)
            _TotalInterestAmount = value
        End Set
    End Property
#End Region

#Region "Total Balance"
    Private _TotalBalance As Decimal
    Public ReadOnly Property TotalBalance() As Decimal
        Get
            _TotalBalance = Me.TotalPrincipalAmount + Me.TotalInterestAmount
            Return _TotalBalance
        End Get
    End Property
#End Region

#Region "JV Closing"
    Private _JVClosing As JournalVoucher
    Public Property JVClosing() As JournalVoucher
        Get
            Return _JVClosing
        End Get
        Set(ByVal value As JournalVoucher)
            _JVClosing = value
        End Set
    End Property
#End Region

#Region "JV Setup"
    Private _JVSetup As JournalVoucher
    Public Property JVSetup() As JournalVoucher
        Get
            Return _JVSetup
        End Get
        Set(ByVal value As JournalVoucher)
            _JVSetup = value
        End Set
    End Property
#End Region

#Region "SPA Details"
    Private _SPADetailsList As New List(Of SPADetails)
    Public Property SPADetailsList() As List(Of SPADetails)
        Get
            Return _SPADetailsList
        End Get
        Set(ByVal value As List(Of SPADetails))
            _SPADetailsList = value
        End Set
    End Property
#End Region

#Region "SPA Monitoring"
    Private _SPAMonitoringList As New List(Of SPAMonitoring)
    Public Property SPAMonitoringList() As List(Of SPAMonitoring)
        Get
            Return _SPAMonitoringList
        End Get
        Set(ByVal value As List(Of SPAMonitoring))
            _SPAMonitoringList = value
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



Public Class MyComparer
    Implements IEqualityComparer(Of AMParticipants)

    Public Overloads Function Equals(ByVal x As AMParticipants, ByVal y As AMParticipants) _
        As Boolean Implements _
        System.Collections.Generic.IEqualityComparer(Of AMParticipants).Equals
        Return x.IDNumber = y.IDNumber
    End Function
    Public Overloads Function GetHashCode(ByVal obj As AMParticipants) _
        As Integer Implements _
        System.Collections.Generic.IEqualityComparer(Of AMParticipants).GetHashCode
        Return Me.GetHashCode()
    End Function
End Class


