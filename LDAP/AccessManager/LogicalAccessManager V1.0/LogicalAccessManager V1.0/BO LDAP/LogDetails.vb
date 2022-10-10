Imports System
Imports System.Data
Imports System.Collections.Generic
Imports System.Text
Imports Library.OraDAL

Namespace LogicalAccess.LDAP.BO
    Public Class LogDetails : Implements IDisposable
        Private _connectionString As String

        Public ReadOnly Property ApplicationName() As String
            Get
                Return "Logical Access Manager"
            End Get
        End Property

        Public Function GetLogs(transDate As DateTime) As List(Of Log)
            Dim ret = New List(Of Log)()

            Dim cmd = New DataCommand("SELECT transaction_date, application_name, module_name, computer_name, ip_address, " + "misc_1, misc_2, misc_3, color_code, log_type, updated_by " + "FROM wesmlogs_transaction " + "WHERE application_name=:0 AND delivery_date=TO_DATE(:1, 'MM/DD/YYYY')", ApplicationName, transDate)
            Dim dt = DataAccess.ExecuteQuery(cmd, _connectionString)

            For Each dr As DataRow In dt.Rows
                ret.Add(New Log() With { _
                    .TransactionDate = Convert.ToDateTime(dr("transaction_date")), _
                    .ModuleName = Convert.ToString(dr("module_name")), _
                    .ComputerName = Convert.ToString(dr("computer_name")), _
                    .IpAddress = Convert.ToString(dr("ip_address")), _
                    .Misc1 = Convert.ToString(dr("misc_1")), _
                    .Misc2 = Convert.ToString(dr("misc_2")), _
                    .Misc3 = Convert.ToString(dr("misc_3")), _
                    .ColorCode = Convert.ToString(dr("color_code")), _
                    .LogType = Convert.ToString(dr("log_type")), _
                    .UpdatedBy = Convert.ToString(dr("updated_by")) _
                })
            Next

            Return ret
        End Function

        Public Sub InsertLog(log As Log)
            Dim cmd = New DataCommand("INSERT INTO wesmlogs_transaction(transaction_date, application_name, module_name, computer_name, ip_address, " + "misc_1, misc_2, misc_3, color_code, log_type, updated_by) " + "VALUES(TO_DATE(:0, 'MM/DD/YYYY HH24:MI:SS'),:1,:2,:3,:4,:5,:6,:7,:8,:9,:10)", log.TransactionDate.ToString("MM/dd/yyyy HH:mm:ss"), log.ApplicationName, log.ModuleName, log.ComputerName, log.IpAddress, _
                log.Misc1, log.Misc2, log.Misc3, log.ColorCode, log.LogType, log.UpdatedBy)
            DataAccess.ExecuteNonQuery(cmd, _connectionString)
        End Sub

        Public Sub New(datasource As String, userId As String, password As String)
            _connectionString = DataAccess.ParseDatasource(datasource, userId, password)
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If
            End If
            Me.disposedValue = True
        End Sub
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class
End Namespace
