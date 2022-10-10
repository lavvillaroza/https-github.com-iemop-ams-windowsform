Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Library.OraDAL
Imports System.Data

Namespace LogicalAccessManager.BO
    Public Class LaModuleInfo
        Inherits LaCollection(Of LaModule)
        Public Sub New()
            Reload()
        End Sub

        Public Overrides Sub Reload()
            MyBase.Items.Clear()

            Dim cmd = New DataCommand("SELECT module_id, app_id, module_name, status, updated_by, TO_CHAR(updated_date, 'MM/DD/YYYY HH24:MI:SS') as updated_date " & "FROM la_modules")
            Dim dt = DataAccess.ExecuteQuery(cmd, ConnectionInfo.GetConnectionString)

            For Each dr As DataRow In dt.Rows
                MyBase.Items.Add(New LaModule() With { _
                  .ModuleId = Convert.ToInt32(dr("module_id")), _
                  .ApplicationId = Convert.ToString(dr("app_id")), _
                  .ModuleName = Convert.ToString(dr("module_name")), _
                  .Status = Convert.ToInt32(dr("status")), _
                  .UpdatedBy = Convert.ToString(dr("updated_by")), _
                  .UpdatedDate = Convert.ToDateTime(dr("updated_date")) _
                })
            Next
        End Sub
    End Class
End Namespace