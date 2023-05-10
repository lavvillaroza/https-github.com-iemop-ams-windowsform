'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             InitializationTable
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     January 9, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Initialization and last sequence used in Check and OR
'Arguments/Parameters:  
'Files/Database Tables:  AM_INIT_TABLE
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   


Option Strict On
Option Explicit On


Public Class InitializationTable

    Public Sub New()
        Me._DocumentName = ""
        Me._InitialNo = 0
        Me._LastNo = 0
        Me._LastSeqUsed = 0
        Me._BatchNo = ""
    End Sub

    Public Sub New(ByVal DocumentName As String, ByVal InitialNo As Long, ByVal LastNo As Long, _
                   ByVal LastSeq As Long, ByVal BatchNo As String)
        Me._DocumentName = DocumentName
        Me._InitialNo = InitialNo
        Me._LastNo = LastNo
        Me._LastSeqUsed = LastSeq
        Me._BatchNo = BatchNo
    End Sub

    Private _DocumentName As String
    Public Property DocumentName() As String
        Get
            Return _DocumentName
        End Get
        Set(ByVal value As String)
            _DocumentName = value
        End Set
    End Property

    Private _InitialNo As Long
    Public Property InitialNo() As Long
        Get
            Return _InitialNo
        End Get
        Set(ByVal value As Long)
            _InitialNo = value
        End Set
    End Property

    Private _LastNo As Long
    Public Property LastNo() As Long
        Get
            Return _LastNo
        End Get
        Set(ByVal value As Long)
            _LastNo = value
        End Set
    End Property

    Private _LastSeqUsed As Long
    Public Property LastSeqUsed() As Long
        Get
            Return _LastSeqUsed
        End Get
        Set(ByVal value As Long)
            _LastSeqUsed = value
        End Set
    End Property

    Private _BatchNo As String
    Public Property BatchNo() As String
        Get
            Return _BatchNo
        End Get
        Set(ByVal value As String)
            _BatchNo = value
        End Set
    End Property

End Class
