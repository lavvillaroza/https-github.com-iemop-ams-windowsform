'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             Signatories
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     December 7, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for Document Signatories
'Arguments/Parameters:  
'Files/Database Tables:  AM_SIGNATORIES
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'	December 7, 2011        Juan Carlo L. Panopio           Changed Charge ID Type from Integer to EnumChargeType
'   November 28, 2012       Vladimir E. Espiritu            Deleted CheckedBy and ApprovedBy properties
'


Option Explicit On
Option Strict On

Public Class DocSignatories

#Region "Initialization/Constructor"
    Public Sub New()
        Me.New("", "", "", Nothing, "", "", "", "", "", "")
    End Sub

    Public Sub New(ByVal DocumentCode As String, ByVal DocumentDescription As String, _
                   ByVal UpdatedBy As String, ByVal UpdatedDate As Date, ByVal Position_1 As String, ByVal Position_2 As String, ByVal Position_3 As String, _
                   ByVal Signatory_1 As String, ByVal Signatory_2 As String, ByVal Signatory_3 As String)
        Me._DocCode = DocumentCode
        Me._DocDescription = DocumentDescription
        Me._UpdatedBy = UpdatedBy
        Me._UpdatedDate = UpdatedDate

        Me._Signatory_1 = Signatory_1
        Me._Signatory_2 = Signatory_2
        Me._Signatory_3 = Signatory_3

        Me._Position_1 = Position_1
        Me._Position_2 = Position_2
        Me._Position_3 = Position_3
    End Sub
#End Region

#Region "Document Code"
    Private _DocCode As String
    Public Property DocCode() As String
        Get
            Return _DocCode
        End Get
        Set(ByVal value As String)
            _DocCode = value
        End Set
    End Property
#End Region

#Region "Document Description"
    Private _DocDescription As String
    Public Property DocDescription() As String
        Get
            Return _DocDescription
        End Get
        Set(ByVal value As String)
            _DocDescription = value
        End Set
    End Property
#End Region

#Region "Signatory 1"

    Private _Signatory_1 As String
    Public Property Signatory_1() As String
        Get
            Return _Signatory_1
        End Get
        Set(ByVal value As String)
            _Signatory_1 = value
        End Set
    End Property

#End Region

#Region "Signatory 2"

    Private _Signatory_2 As String
    Public Property Signatory_2() As String
        Get
            Return _Signatory_2
        End Get
        Set(ByVal value As String)
            _Signatory_2 = value
        End Set
    End Property

#End Region

#Region "Signatory 3"

    Private _Signatory_3 As String
    Public Property Signatory_3() As String
        Get
            Return _Signatory_3
        End Get
        Set(ByVal value As String)
            _Signatory_3 = value
        End Set
    End Property

#End Region

#Region "Position 1"

    Private _Position_1 As String
    Public Property Position_1() As String
        Get
            Return _Position_1
        End Get
        Set(ByVal value As String)
            _Position_1 = value
        End Set
    End Property

#End Region

#Region "Position 2"

    Private _Position_2 As String
    Public Property Position_2() As String
        Get
            Return _Position_2
        End Get
        Set(ByVal value As String)
            _Position_2 = value
        End Set
    End Property

#End Region

#Region "Position 3"

    Private _Position_3 As String
    Public Property Position_3() As String
        Get
            Return _Position_3
        End Get
        Set(ByVal value As String)
            _Position_3 = value
        End Set
    End Property

#End Region

#Region "Updated By"
    Private _UpdatedBy As String
    Public Property UpdatedBy() As String
        Get
            Return _UpdatedBy
        End Get
        Set(ByVal value As String)
            _UpdatedBy = value
        End Set
    End Property
#End Region

#Region "Updated Date"
    Private _UpdatedDate As Date
    Public Property UpdatedDate() As Date
        Get
            Return _UpdatedDate
        End Get
        Set(ByVal value As Date)
            _UpdatedDate = value
        End Set
    End Property
#End Region


End Class
