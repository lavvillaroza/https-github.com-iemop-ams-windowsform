Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Library.Core.Configuration

Namespace LogicalAccessManager.BO
    Friend Class ConfigurationDetails
        Private _routeName As String = "LAM Route"
        Private _cd As ConfigDetails

        'Database
        Public Property DbDatasource() As String
            Get
                Return _cd.GetRouteGroupStringElem(_routeName, "Db", "Datasource").Value
            End Get
            Set(ByVal value As String)
                _cd.GetRouteGroupStringElem(_routeName, "Db", "Datasource").Value = value
            End Set
        End Property
        Public Property DbUserId() As String
            Get
                Return _cd.GetRouteGroupStringElem(_routeName, "Db", "UserId").Value
            End Get
            Set(ByVal value As String)
                _cd.GetRouteGroupStringElem(_routeName, "Db", "UserId").Value = value
            End Set
        End Property
        Public Property DbPassword() As String
            Get
                Return _cd.GetRouteGroupStringElem(_routeName, "Db", "Password").Value
            End Get
            Set(ByVal value As String)
                _cd.GetRouteGroupStringElem(_routeName, "Db", "Password").Value = value
            End Set
        End Property

        Public Sub Save()            
            _cd.SaveConfig()
            _cd.Reload()
        End Sub

        Public Sub New()
            _cd = New ConfigDetails("appconfig.xml")
        End Sub
    End Class
End Namespace