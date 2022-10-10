'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             ParticipantLineage
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     August 22, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for participant line age
'Arguments/Parameters:  
'Files/Database Tables:  BILL_PARTICIPANT_LINEAGE
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   August 22, 2011         Vladimir E. Espiritu            Class initialization
'


Option Strict On
Option Explicit On

Public Class ParticipantLineage

#Region "Initialization/Constructor"

    Public Sub New()
        Me.New("", "", "", New Date(1900, 1, 1), New Date(1900, 1, 1), 1, 1, "", 0)
    End Sub

    Public Sub New(ByVal idnumber As String, ByVal participantid As String, ByVal parentid As String, _
                   ByVal startdate As Date, ByVal enddate As Date, ByVal starthour As Integer, ByVal endhour As Integer, _
                   ByVal billedto As String, ByVal parentflag As Integer)

        Me._IDNumber = idnumber
        Me._ParticipantID = participantid
        Me._ParentID = parentid
        Me._StartDate = startdate
        Me._EndDate = enddate
        Me._StartHour = starthour
        Me._EndHour = endhour
        Me._ParentFlag = parentflag
        Me._BilledTo = billedto
    End Sub
#End Region


#Region "IDNumber"
    Private _IDNumber As String
    ' <summary>
    ' Gets the ID Number.
    ' </summary>
    ' <remarks></remarks>
    Public Property IDNumber() As String
        Get
            Return _IDNumber
        End Get
        Set(ByVal value As String)
            _IDNumber = value
        End Set
    End Property
#End Region

#Region "ParticipantID"
    Private _ParticipantID As String
    ' <summary>
    ' Gets or sets Participant ID.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property ParticipantID() As String
        Get
            Return _ParticipantID
        End Get
        Set(ByVal value As String)
            _ParticipantID = value
        End Set
    End Property
#End Region

#Region "ParentID"
    Private _ParentID As String
    ' <summary>
    ' Gets or sets the Parent ID.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property ParentID() As String
        Get
            Return _ParentID
        End Get
        Set(ByVal value As String)
            _ParentID = value
        End Set
    End Property
#End Region

#Region "Billed_To"
    Private _BilledTo As String
    ' <summary>
    ' Gets or sets Billed To.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property BilledTo() As String
        Get
            Return _BilledTo
        End Get
        Set(ByVal value As String)
            _BilledTo = value
        End Set
    End Property

#End Region

#Region "StartDate"
    Private _StartDate As Date
    ' <summary>
    ' Gets or sets Start Date.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property StartDate() As Date
        Get
            Return _StartDate
        End Get
        Set(ByVal value As Date)
            _StartDate = value
        End Set
    End Property
#End Region

#Region "EndDate"
    Private _EndDate As Date
    ' <summary>
    ' Gets or sets End Date.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property EndDate() As Date
        Get
            Return _EndDate
        End Get
        Set(ByVal value As Date)
            _EndDate = value
        End Set
    End Property
#End Region
    
#Region "StartHour"
    Private _StartHour As Integer
    ' <summary>
    ' Gets or sets Start Hour.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property StartHour() As Integer
        Get
            Return _StartHour
        End Get
        Set(ByVal value As Integer)
            _StartHour = value
        End Set
    End Property

#End Region

#Region "EndHour"
    Private _EndHour As Integer
    ' <summary>
    ' Gets or sets End Hour.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property EndHour() As Integer
        Get
            Return _EndHour
        End Get
        Set(ByVal value As Integer)
            _EndHour = value
        End Set
    End Property
#End Region

#Region "ParentFlag"
    Private _ParentFlag As Integer
    ' <summary>
    ' Gets or sets Parent Flag. 1 means parent, 0 if child.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property ParentFlag() As Integer
        Get
            Return _ParentFlag
        End Get
        Set(ByVal value As Integer)
            _ParentFlag = value
        End Set
    End Property
#End Region

End Class
