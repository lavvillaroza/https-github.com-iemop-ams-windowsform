Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data
Imports Library.OraDAL

Namespace LogicalAccessManager.BO
    Public Class LaApplicationSql
        Friend ReadOnly Property ConnectionInfo() As ConnectionDetails
            Get
                Return ConnectionDetails.Instance
            End Get
        End Property
        Public Sub AddApp(ByVal _AppID As String, ByVal _AppName As String, ByVal _Status As Integer, ByVal _UpdatedBy As String, ByVal _UpdatedDate As Date)
            Dim cmd = New DataCommand("INSERT INTO LA_APPLICATIONS (APP_ID, APP_NAME, STATUS, UPDATED_BY, UPDATED_DATE) " _
                                    & "VALUES ('" & _AppID & "', '" & _AppName & "', '" & _Status & "', '" & _UpdatedBy & "', TO_DATE('" & _UpdatedDate & "', 'mm/dd/yyyy'))")
            DataAccess.ExecuteNonQuery(cmd, ConnectionInfo.GetConnectionString)
        End Sub
        Public Sub EditApp(ByVal _AppID As String, ByVal _AppName As String, ByVal _Status As Integer, ByVal _UpdatedBy As String, ByVal _UpdatedDate As Date)
            Dim cmd = New DataCommand("UPDATE LA_APPLICATIONS LA " _
                                    & "SET LA.APP_NAME= '" & _AppName & "', " _
                                    & "LA.STATUS='" & _Status & "', " _
                                    & "LA.UPDATED_BY= '" & _UpdatedBy & "', " _
                                    & "LA.UPDATED_DATE= TO_DATE('" & _UpdatedDate & "', 'mm/dd/yyyy') " _
                                    & "WHERE LA.APP_ID='" & _AppID & "'")
            DataAccess.ExecuteNonQuery(cmd, ConnectionInfo.GetConnectionString)
        End Sub
    End Class
End Namespace

