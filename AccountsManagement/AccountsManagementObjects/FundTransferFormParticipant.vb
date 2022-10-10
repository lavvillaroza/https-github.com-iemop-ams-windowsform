'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             FundTransferFormParticipant
'Orginal Author:         Vladimir E.Espiritu
'File Creation Date:     April 22, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Accounting Code
'Arguments/Parameters:  
'Files/Database Tables:  AM_FTF_PARTICIPANT
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description
'   April 22, 2012          Vladimir E.Espiritu         Class initialization
'	

Option Explicit On
Option Strict On

<Serializable()> _
Public Class FundTransferFormParticipant

#Region "Initialization/Constructor"
    Public Sub New()
        Me.New(0, New AMParticipants, 0)
    End Sub

    Public Sub New(ByVal refno As Long, ByVal idnumber As AMParticipants, ByVal amount As Decimal)
        Me._RefNo = refno
        Me._IDNumber = idnumber
        Me._Amount = amount
    End Sub
#End Region


#Region "RefNo"
    Private _RefNo As Long
    Public Property RefNo() As Long
        Get
            Return _RefNo
        End Get
        Set(ByVal value As Long)
            _RefNo = value
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

End Class
