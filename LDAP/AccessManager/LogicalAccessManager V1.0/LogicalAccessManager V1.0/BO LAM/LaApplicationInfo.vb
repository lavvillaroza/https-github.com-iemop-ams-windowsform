Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Library.OraDAL
Imports System.Data

Namespace LogicalAccessManager.BO
    Public Class LaApplicationInfo
        Inherits LaCollection(Of LaApplication)
        Public Sub New()
            Reload()
        End Sub

        Public Overrides Sub Reload()
            MyBase.Items.Clear()

            Dim cmd = New DataCommand("SELECT app_id, app_name, status, updated_by, TO_CHAR(updated_date, 'MM/DD/YYYY HH24:MI:SS') as updated_date " & "FROM la_applications")
            Dim dt = DataAccess.ExecuteQuery(cmd, ConnectionInfo.GetConnectionString)

            For Each dr As DataRow In dt.Rows
                MyBase.Items.Add(New LaApplication() With { _
                 .ApplicationId = Convert.ToString(dr("app_id")), _
                 .ApplicationName = Convert.ToString(dr("app_name")), _
                 .Status = Convert.ToInt32(dr("status")), _
                 .UpdatedBy = Convert.ToString(dr("updated_by")), _
                 .UpdatedDate = Convert.ToDateTime(dr("updated_date")) _
                })
            Next
        End Sub
    End Class
End Namespace