Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace LogicalAccessManager.BO
    Public Class UserDetails
        Private _userName As String = ""
        Private _user As LaUser
        Private _applications As List(Of LaApplication)
        Private _modules As List(Of LaModule)

        'Constructor
        Public Sub New(ByVal userName As String)
            _userName = userName
            'Reload()
        End Sub

        'Read Only Property
        Public ReadOnly Property User() As LaUser
            Get
                Return _user
            End Get
        End Property

        Public ReadOnly Property Applications() As List(Of LaApplication)
            Get
                Return _applications
            End Get
        End Property

        Public ReadOnly Property Modules() As List(Of LaModule)
            Get
                Return _modules
            End Get
        End Property

        'Functions
        Public Function FilterModules(ByVal applicationId As String) As List(Of LaModule)
            If User Is Nothing Then
                Throw New Exception("Null User property not supported.")
            End If
            If Applications Is Nothing Then
                Throw New Exception("Null Applications property not supported.")
            End If
            If Modules Is Nothing Then
                Throw New Exception("Null Modules property not supported.")
            End If

            Return (From x In Modules Where x.ApplicationId = applicationId Select x).ToList()
        End Function

        'Public Sub Reload()
        '    Dim laUserInfo As LaCollection(Of LaUser) = New LaUserInfo()
        '    Dim laAppInfo As LaCollection(Of LaApplication) = New LaApplicationInfo()
        '    Dim laMdlInfo As LaCollection(Of LaModule) = New LaModuleInfo()
        '    Dim laUserMdlInfo As LaCollection(Of LaUserModule) = New LaUserModuleInfo()

        '    _user = (From x In laUserInfo.Items _
        '             Where x.UserName = _userName Select x).FirstOrDefault()

        '    _applications = (From x In laUserMdlInfo.Items Join y In laMdlInfo.Items _
        '                     On x.ModuleId Equals y.ModuleId Join z In laAppInfo.Items _
        '                     On y.ApplicationId Equals z.ApplicationId _
        '                     Where x.UserName = _userName Select z).Distinct().ToList()

        '    _modules = (From x In laUserMdlInfo.Items Join y In laMdlInfo.Items _
        '                On x.ModuleId Equals y.ModuleId Where x.UserName = _userName Select y).ToList()
        'End Sub
    End Class
End Namespace