'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             DefaultNotice
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     October 12, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Default Notice
'Arguments/Parameters:  
'Files/Database Tables:  AM_DEFAULT_NOTICE
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************


Public Class DefaultNotice

    Public Sub New()
        Me._Participant = New AMParticipants
        Me._ListofDefaulNoticeDetails = New List(Of DefaultNoticeDetails)
        Me._DNNumber = 0
        Me._UpdatedBy = ""
        Me._UpdatedDate = Nothing
        Me._TransactionDate = Nothing
    End Sub

    Private _Participant As AMParticipants
    Public Property Participant() As AMParticipants
        Get
            Return _Participant
        End Get
        Set(ByVal value As AMParticipants)
            _Participant = value
        End Set
    End Property

    Private _ListofDefaulNoticeDetails As List(Of DefaultNoticeDetails)
    Public Property ListofDefaulNoticeDetails() As List(Of DefaultNoticeDetails)
        Get
            Return _ListofDefaulNoticeDetails
        End Get
        Set(ByVal value As List(Of DefaultNoticeDetails))
            _ListofDefaulNoticeDetails = value
        End Set
    End Property

    Private _DNNumber As Long
    Public Property DNNumber() As Long
        Get
            Return _DNNumber
        End Get
        Set(ByVal value As Long)
            _DNNumber = value
        End Set
    End Property

    Private _AllocationDate As Date
    Public Property AllocationDate() As Date
        Get
            Return _AllocationDate
        End Get
        Set(ByVal value As Date)
            _AllocationDate = value
        End Set
    End Property

    Private _UpdatedBy As String
    Public Property UpdatedBy() As String
        Get
            Return _UpdatedBy
        End Get
        Set(ByVal value As String)
            _UpdatedBy = value
        End Set
    End Property

    Private _UpdatedDate As Date
    Public Property UpdatedDate() As Date
        Get
            Return _UpdatedDate
        End Get
        Set(ByVal value As Date)
            _UpdatedDate = value
        End Set
    End Property

    Private _TransactionDate As Date
    Public Property TransactionDate() As Date
        Get
            Return _TransactionDate
        End Get
        Set(ByVal value As Date)
            _TransactionDate = value
        End Set
    End Property

End Class
