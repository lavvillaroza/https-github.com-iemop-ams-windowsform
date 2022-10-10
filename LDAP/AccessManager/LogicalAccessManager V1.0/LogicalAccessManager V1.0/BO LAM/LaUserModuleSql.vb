Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data
Imports Library.OraDAL

Namespace LogicalAccessManager.BO
    Public Class LaUserModuleSql
        Private objDic As New Dictionary(Of String, Integer)
        Friend ReadOnly Property ConnectionInfo() As ConnectionDetails
            Get
                Return ConnectionDetails.Instance
            End Get
        End Property

        Public Function GetModuleList(ByVal _AppName As String) As List(Of String)
            Dim ret = New List(Of String)
            Dim cmd = New DataCommand("SELECT T.MODULE_NAME, T.MODULE_ID FROM (SELECT LM.MODULE_ID, LA.APP_NAME, LM.MODULE_NAME FROM LA_MODULES LM " _
                                    & "INNER JOIN LA_APPLICATIONS LA ON LM.APP_ID = LA.APP_ID) T " _
                                    & "WHERE T.APP_NAME = '" & _AppName & "'")

            For Each r As DataRow In DataAccess.ExecuteQuery(cmd, ConnectionInfo.GetConnectionString).Rows
                ret.Add(r.Item("MODULE_NAME"))                
                With objDic
                    'If .ContainsKey(r.Item("MODULE_NAME")) = False Then
                    'MessageBox.Show(r.Item("MODULE_ID"))
                    .Add(r.Item("MODULE_NAME"), r.Item("MODULE_ID"))
                    'End If
                End With
            Next
            Return ret

        End Function

        Public Sub AddUserModule(ByVal _UserName As String, ByVal _ModuleName As String, ByVal _UpdatedBy As String, ByVal _UpdatedDate As Date)
            Dim iModuleID As Integer = CInt(objDic.Item(_ModuleName).ToString)
            Dim cmd = New DataCommand("INSERT INTO LA_USERS_MODULES (USER_NAME, MODULE_ID, UPDATED_BY, UPDATED_DATE) " _
                                    & "VALUES ('" & _UserName & "', '" & iModuleID & "', '" & _UpdatedBy & "', TO_DATE('" & _UpdatedDate & "', 'mm/dd/yyyy'))")
            DataAccess.ExecuteNonQuery(cmd, ConnectionInfo.GetConnectionString)
        End Sub

        Public Sub DeleteUserModule(ByVal _UserName As String, ByVal _ModuleName As String)
            Dim iModuleID As Integer = CInt(objDic.Item(_ModuleName).ToString)
            Dim cmd = New DataCommand("DELETE FROM LA_USERS_MODULES LUM " _
                                   & "WHERE LUM.USER_NAME = '" & _UserName & "' AND " _
                                   & "LUM.MODULE_ID= " & iModuleID)
            DataAccess.ExecuteNonQuery(cmd, ConnectionInfo.GetConnectionString)
        End Sub

    End Class
End Namespace