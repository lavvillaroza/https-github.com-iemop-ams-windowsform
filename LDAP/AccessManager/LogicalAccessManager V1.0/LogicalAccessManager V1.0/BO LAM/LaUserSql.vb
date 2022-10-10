Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data
Imports Library.OraDAL

Namespace LogicalAccessManager.BO
    Public Class LaUserSql
        Friend ReadOnly Property ConnectionInfo() As ConnectionDetails
            Get
                Return ConnectionDetails.Instance
            End Get
        End Property
        Public Sub AddUser(ByVal _UserName As String, ByVal _FirstName As String, ByVal _MI As String, ByVal _LastName As String, ByVal _Position As String, ByVal _UpdatedBy As String, ByVal _UpdatedDate As Date)
            Dim cmd = New DataCommand("INSERT INTO LA_USERS (USER_NAME, FIRST_NAME, MI, LAST_NAME, POSITION, UPDATED_BY, UPDATED_DATE) " _
                                    & "VALUES ('" & _UserName & "', '" & _FirstName & "', '" & _MI & "', '" & _LastName & "', '" & _Position & "', '" & _UpdatedBy & "', TO_DATE('" & _UpdatedDate & "', 'mm/dd/yyyy'))")
            DataAccess.ExecuteNonQuery(cmd, ConnectionInfo.GetConnectionString)
        End Sub
        Public Sub EditUser(ByVal _UserName As String, ByVal _FirstName As String, ByVal _MI As String, ByVal _LastName As String, ByVal _Position As String, ByVal _UpdatedBy As String, ByVal _UpdatedDate As Date)
            Dim cmd = New DataCommand("UPDATE LA_USERS LU " _
                                    & "SET LU.FIRST_NAME= '" & _FirstName & "', " _
                                    & "LU.MI='" & _MI & "', " _
                                    & "LU.LAST_NAME='" & _LastName & "', " _
                                    & "LU.POSITION='" & _Position & "', " _
                                    & "LU.UPDATED_BY= '" & _UpdatedBy & "', " _
                                    & "LU.UPDATED_DATE= TO_DATE('" & _UpdatedDate & "', 'mm/dd/yyyy') " _
                                    & "WHERE LU.USER_NAME='" & _UserName & "'")
            DataAccess.ExecuteNonQuery(cmd, ConnectionInfo.GetConnectionString)
        End Sub
        Public Sub DeleteUser(ByVal _UserName As String, ByVal _FirstName As String, ByVal _MI As String, ByVal _LastName As String, ByVal _Position As String, ByVal _UpdatedBy As String, ByVal _UpdatedDate As Date)
            Dim cmd = New DataCommand("DELETE FROM LA_USERS LU " _
                                    & "WHERE LU.USER_NAME = '" & _UserName & "', " _
                                    & "LU.FIRST_NAME= '" & _FirstName & "', " _
                                    & "LU.MI='" & _MI & "', " _
                                    & "LU.LAST_NAME='" & _LastName & "'" _
                                    & "LU.POSITION='" & _Position & "'")
            DataAccess.ExecuteNonQuery(cmd, ConnectionInfo.GetConnectionString)
        End Sub

    End Class
End Namespace

