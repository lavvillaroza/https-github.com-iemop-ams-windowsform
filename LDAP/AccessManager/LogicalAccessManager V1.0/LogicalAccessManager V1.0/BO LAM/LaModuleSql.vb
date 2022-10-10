Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data
Imports Library.OraDAL

Namespace LogicalAccessManager.BO
    Public Class LaModuleSql
        Friend ReadOnly Property ConnectionInfo() As ConnectionDetails
            Get
                Return ConnectionDetails.Instance
            End Get
        End Property
        Public Sub AddModule(ByVal _ModuleID As String, ByVal _AppID As String, ByVal _ModuleName As String, ByVal _Status As Integer, ByVal _UpdatedBy As String, ByVal _UpdatedDate As Date)
            Dim cmd = New DataCommand("INSERT INTO LA_MODULES (MODULE_ID, APP_ID, MODULE_NAME, STATUS, UPDATED_BY, UPDATED_DATE) " _
                                    & "VALUES (LA_MODULES_SEQ.NEXTVAL, '" & _AppID & "', '" & _ModuleName & "', " & _Status & ", '" & _UpdatedBy & "', TO_DATE('" & _UpdatedDate & "', 'mm/dd/yyyy'))")
            DataAccess.ExecuteNonQuery(cmd, ConnectionInfo.GetConnectionString)
        End Sub
        Public Sub EditModul(ByVal _ModuleID As String, ByVal _AppID As String, ByVal _ModuleName As String, ByVal _Status As Integer, ByVal _UpdatedBy As String, ByVal _UpdatedDate As Date)
            Dim cmd = New DataCommand("UPDATE LA_MODULES LM " _
                                    & "SET LM.APP_ID= '" & _AppID & "', " _
                                    & "LM.MODULE_NAME='" & _ModuleName & "', " _
                                    & "LM.STATUS='" & _Status & "', " _
                                    & "LM.UPDATED_BY= '" & _UpdatedBy & "', " _
                                    & "LM.UPDATED_DATE= TO_DATE('" & _UpdatedDate & "', 'mm/dd/yyyy') " _
                                    & "WHERE LM._ModuleID=" & _ModuleID)
            DataAccess.ExecuteNonQuery(cmd, ConnectionInfo.GetConnectionString)
        End Sub


    End Class
End Namespace