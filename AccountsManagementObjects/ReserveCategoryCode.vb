
Option Explicit On
Option Strict On
Public Class ReserveCategoryCode
    Public Sub New(ByVal codename As String, ByVal description As String, ByVal updatedby As String, ByVal updateddate As Date)
        Me._CodeName = codename
        Me._Description = description
        Me._UpdatedBy = updatedby
        Me._UpdatedDate = updateddate
    End Sub
#Region "CodeName"
    Private _CodeName As String
    Public Property CodeName() As String
        Get
            Return _CodeName
        End Get
        Set(ByVal value As String)
            _CodeName = value
        End Set
    End Property
#End Region

#Region "Description"
    Private _Description As String
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property
#End Region

#Region "UpdatedBy"
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

#Region "UpdatedDate"
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
