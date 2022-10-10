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
'

<Serializable()> _
Public Class Prudential

#Region "Initialization/Constructor"

    Public Sub New()
        Me.New("", 0, 0)
    End Sub

    Public Sub New(ByVal idnumber As String, ByVal prudentialamount As Decimal, ByVal interestamount As Decimal)
        Me._IDNumber = idnumber
        Me._PrudentialAmount = prudentialamount
        Me._InterestAmount = interestamount
    End Sub

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

#Region "PrudentialAmount"
    Private _PrudentialAmount As Decimal
    Public Property PrudentialAmount() As Decimal
        Get
            Return _PrudentialAmount
        End Get
        Set(ByVal value As Decimal)
            _PrudentialAmount = value
        End Set
    End Property

#End Region

#Region "InterestAmount"
    Private _InterestAmount As Decimal
    Public Property InterestAmount() As Decimal
        Get
            Return _InterestAmount
        End Get
        Set(ByVal value As Decimal)
            _InterestAmount = value
        End Set
    End Property

#End Region

End Class
