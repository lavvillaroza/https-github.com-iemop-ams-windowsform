'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             Prudential
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     March 19, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for AM Participants
'Arguments/Parameters:  
'Files/Database Tables:  AM_PRUDENTIAL
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   March 19, 2012          Vladimir E. Espiritu            Class initialization
'   August 30, 2012         Vladimir E. Espiritu            Added batch code and dmcm no properties
'

<Serializable()> _
Public Class PrudentialHistory

#Region "Initialization/Constructor"

    Public Sub New()
        Me.New(New AMParticipants, 0, Nothing, Nothing)
    End Sub


    Public Sub New(ByVal idnumber As AMParticipants, ByVal prudentialamount As Decimal, ByVal transtype As EnumPrudentialTransType, _
                   ByVal transdate As Date)
        Me._ORNo = 0
        Me._IDNumber = idnumber
        Me._Amount = prudentialamount
        Me._TransType = transtype
        Me._TransDate = transdate
        Me._BatchCode = ""
    End Sub

    Public Sub New(ByVal ORNumber As Long, ByVal idnumber As AMParticipants, ByVal prudentialamount As Decimal, _
                   ByVal transtype As EnumPrudentialTransType, ByVal transdate As Date)

        Me.New(ORNumber, idnumber, prudentialamount, transtype, transdate, "", 0)
    End Sub

    Public Sub New(ByVal ORNumber As Long, ByVal idnumber As AMParticipants, ByVal prudentialamount As Decimal, _
                   ByVal transtype As EnumPrudentialTransType, ByVal transdate As Date, ByVal batchcode As String, _
                   ByVal dmcmno As Long)

        Me._ORNo = ORNumber
        Me._IDNumber = idnumber
        Me._Amount = prudentialamount
        Me._TransType = transtype
        Me._TransDate = transdate
        Me._BatchCode = batchcode
        Me._DMCMNumber = dmcmno
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

#Region "PrudentialAmount"
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

#Region "TransType"
    Private _TransType As EnumPrudentialTransType
    Public Property TransType() As EnumPrudentialTransType
        Get
            Return _TransType
        End Get
        Set(ByVal value As EnumPrudentialTransType)
            _TransType = value
        End Set
    End Property

#End Region

#Region "TransDate"
    Private _TransDate As Date
    Public Property TransDate() As Date
        Get
            Return _TransDate
        End Get
        Set(ByVal value As Date)
            _TransDate = value
        End Set
    End Property

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

#Region "DMCM Number"
    Private _DMCMNumber As Long
    Public Property DMCMNumber() As Long
        Get
            Return _DMCMNumber
        End Get
        Set(ByVal value As Long)
            _DMCMNumber = value
        End Set
    End Property
#End Region

#Region "FTFNumber"
    Private _FTFNumber As Long
    Public Property FTFNumber() As Long
        Get
            Return _FTFNumber
        End Get
        Set(ByVal value As Long)
            _FTFNumber = value
        End Set
    End Property

#End Region


End Class

