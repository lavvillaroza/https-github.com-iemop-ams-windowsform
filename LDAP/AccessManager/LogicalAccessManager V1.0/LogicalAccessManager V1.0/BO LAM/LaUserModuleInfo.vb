Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Library.OraDAL
Imports System.Data

Namespace LogicalAccessManager.BO
    Public Class LaUserModuleInfo
        Inherits LaCollection(Of LaUserModule)
        Public Sub New()
            Reload()
        End Sub

        Public Overrides Sub Reload()
            MyBase.Items.Clear()

            'Dim cmd = New DataCommand("SELECT user_name, module_id, updated_by, TO_CHAR(updated_date, 'MM/DD/YYYY HH24:MI:SS') as updated_date " & "FROM la_users_modules")
            Dim cmd = New DataCommand("SELECT LUM.USER_NAME, " _
                                    & "CASE WHEN LENGTH(LU.MI)=0 THEN LU.FIRST_NAME||' '||LU.LAST_NAME " _
                                    & "WHEN NVL(LU.MI,'NULL') = 'NULL' THEN LU.FIRST_NAME||' '||LU.LAST_NAME " _
                                    & "ELSE LU.FIRST_NAME||' '||LU.MI||' '||LU.LAST_NAME " _
                                    & "END AS FULL_NAME, T1.APP_NAME, T1.MODULE_NAME, LUM.UPDATED_BY, LUM.UPDATED_DATE " _
                                    & "FROM LA_USERS_MODULES LUM " _
                                    & "INNER JOIN LA_USERS LU ON LU.USER_NAME=LUM.USER_NAME " _
                                    & "INNER JOIN (SELECT LA.APP_NAME, LAM.MODULE_ID, LAM.MODULE_NAME " _
                                                & "FROM LA_APPLICATIONS LA " _
                                                & "INNER JOIN LA_MODULES LAM ON LAM.APP_ID=LA.APP_ID) T1 ON T1.MODULE_ID=LUM.MODULE_ID")

            Dim dt = DataAccess.ExecuteQuery(cmd, ConnectionInfo.GetConnectionString)

            For Each dr As DataRow In dt.Rows
                MyBase.Items.Add(New LaUserModule() With { _
                 .UserName = Convert.ToString(dr("user_name")), _
                 .FullName = Convert.ToString(dr("full_name")), _
                 .ApplicationName = Convert.ToString(dr("app_name")), _
                 .ModuleName = Convert.ToString(dr("module_name")), _
                 .UpdatedBy = Convert.ToString(dr("updated_by")), _
                 .UpdatedDate = Convert.ToDateTime(dr("updated_date")) _
                })
            Next
        End Sub
    End Class
End Namespace