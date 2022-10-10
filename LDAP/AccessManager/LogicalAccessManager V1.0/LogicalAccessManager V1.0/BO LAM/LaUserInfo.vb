Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data
Imports Library.OraDAL

Namespace LogicalAccessManager.BO
    Public Class LaUserInfo
        Inherits LaCollection(Of LaUser)
        Public Sub New()
            Reload()
        End Sub

        Public Overrides Sub Reload()
            MyBase.Items.Clear()

            Dim cmd = New DataCommand("SELECT user_name, first_name, mi, last_name, position, updated_by, TO_CHAR(updated_date, 'MM/DD/YYYY HH24:MI:SS') as updated_date " & "FROM la_users")
            Dim dt = DataAccess.ExecuteQuery(cmd, ConnectionInfo.GetConnectionString)

            For Each dr As DataRow In dt.Rows
                MyBase.Items.Add(New LaUser() With { _
                 .UserName = Convert.ToString(dr("user_name")), _
                 .FirstName = Convert.ToString(dr("first_name")), _
                 .MI = Convert.ToString(dr("mi")), _
                 .LastName = Convert.ToString(dr("last_name")), _
                 .Position = Convert.ToString(dr("position")), _
                 .UpdatedBy = Convert.ToString(dr("updated_by")), _
                 .UpdatedDate = Convert.ToDateTime(dr("updated_date")) _
                })
            Next
        End Sub
    End Class
End Namespace