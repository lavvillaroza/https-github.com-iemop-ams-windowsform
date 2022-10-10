Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Library.Core
Imports Library.OraDAL

Namespace LogicalAccessManager.BO
    Friend Class ConnectionDetails
        Private Shared _instance As ConnectionDetails = Nothing
        Private Shared _configDetails As ConfigurationDetails
        Private Shared _syncRoot As Object = New [Object]()

        Private _datasource As String
        Private _userId As String
        Private _password As String

        Private Sub New()
            _configDetails = New ConfigurationDetails()
        End Sub

        Public Property Datasource() As String
            Get
                Return _configDetails.DbDatasource
            End Get
            Set(ByVal value As String)
                _configDetails.DbDatasource = value
            End Set
        End Property

        Public Property UserId() As String
            Get
                Return _configDetails.DbUserId
            End Get
            Set(ByVal value As String)
                _configDetails.DbUserId = value
            End Set
        End Property

        Public Property Password() As String
            Get
                Return _configDetails.DbPassword
            End Get
            Set(ByVal value As String)
                _configDetails.DbPassword = value
            End Set
        End Property

        Public ReadOnly Property GetConnectionString() As String
            Get
                Return DataAccess.ParseDatasource(Datasource, UserId, Password)
            End Get
        End Property

        Public Sub Save()
            _configDetails.Save()
        End Sub

        Public Shared ReadOnly Property Instance() As ConnectionDetails
            Get
                If _instance Is Nothing Then
                    SyncLock _syncRoot
                        If _instance Is Nothing Then
                            _instance = New ConnectionDetails()
                        End If
                    End SyncLock
                End If
                Return _instance
            End Get
        End Property
    End Class
End Namespace