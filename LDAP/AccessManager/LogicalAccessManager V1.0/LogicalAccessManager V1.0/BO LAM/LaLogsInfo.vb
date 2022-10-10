Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Library.OraDAL
Imports System.Data

Namespace LogicalAccessManager.BO
    Public Class LaLogsInfo : Inherits LaCollection(Of LaLogs)        
        Public Sub New()
            Reload()
        End Sub

        Public Overrides Sub Reload()
            MyBase.Items.Clear()

            Dim cmd = New DataCommand("SELECT * FROM wesmlogs_transaction WHERE APPLICATION_NAME='Logical Access Manager'")
            Dim dt = DataAccess.ExecuteQuery(cmd, ConnectionInfo.GetConnectionString)
            For Each dr As DataRow In dt.Rows                
                MyBase.Items.Add(New LaLogs() With { _
                 .TransactionDate = Convert.ToDateTime(dr("transaction_date")),
                 .ApplicationName = Convert.ToString(dr("application_name")),
                 .ModuleName = Convert.ToString(dr("module_name")),
                 .ComputerName = Convert.ToString(dr("computer_name")),
                 .IPAddress = Convert.ToString(dr("ip_address")),
                 .Misc1 = Convert.ToString(dr("misc_1")),
                 .Misc2 = Convert.ToString(dr("misc_2")),
                 .Misc3 = Convert.ToString(dr("misc_3")),
                 .ColorCode = Convert.ToString(dr("color_code")),
                 .LogType = Convert.ToString(dr("log_type")),
                .UpdatedBy = Convert.ToString(dr("updated_by"))
                })

            Next
        End Sub
    End Class
End Namespace
