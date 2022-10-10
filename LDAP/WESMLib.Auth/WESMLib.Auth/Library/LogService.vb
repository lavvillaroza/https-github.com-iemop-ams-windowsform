Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.IO
Imports WESMLib.Auth.BO
Imports WESMLib.Auth.OraDAL

Namespace WESMLib.Auth.Lib
    Public Class LogService
        Private _datasource As String
        Private _userId As String
        Private _password As String
        Private _logDir As String

        Public Sub InsertLog(transactionDate As DateTime, applicationName As String, moduleName As String, misc1 As String, misc2 As String, misc3 As String, _
            colorCode As ColorCode, logType As String, updatedBy As String)
            Dim ld = New LogDetails(_datasource, _userId, _password)
            Dim tempLog = New Log() With { _
                 .TransactionDate = DateTime.Now, _
                 .ApplicationName = applicationName, _
                 .ModuleName = moduleName, _
                 .ComputerName = OtherService.GetComputerName(), _
                 .IpAddress = OtherService.GetIpAddress(), _
                 .Misc1 = misc1, _
                 .Misc2 = misc2, _
                 .Misc3 = misc3, _
                 .ColorCode = colorCode.ToString(), _
                 .LogType = logType, _
                 .UpdatedBy = updatedBy _
            }

            ld.InsertLog(tempLog)

            Dim sb = New StringBuilder()
            sb.Append(String.Format("[{0}], ", tempLog.TransactionDate.ToString("MM/dd/yyyy HH:mm:ss fff")))
            sb.Append(String.Format("{0}, ", tempLog.ApplicationName))
            sb.Append(String.Format("{0}, ", tempLog.ModuleName))
            sb.Append(String.Format("{0}, ", tempLog.ComputerName))
            sb.Append(String.Format("{0}, ", tempLog.IpAddress))
            sb.Append(String.Format("{0}, ", tempLog.Misc1))
            sb.Append(String.Format("{0}, ", tempLog.Misc2))
            sb.Append(String.Format("{0}, ", tempLog.Misc3))
            sb.Append(String.Format("{0}, ", tempLog.ColorCode))
            sb.Append(String.Format("{0}, ", tempLog.LogType))
            sb.Append(String.Format("{0}", tempLog.UpdatedBy))

            Dim logFilename = String.Format("log_{0}.txt", DateTime.Now.ToString("MMddyyyy"))
            Dim tfs = New TextfileService(Path.Combine(_logDir, logFilename))
            tfs.WriteToTextfile(sb.ToString(), True)
        End Sub

        Sub New(datasource As String, userId As String, password As String, logDir As String)
            _datasource = datasource
            _userId = userId
            _password = password

            _logDir = logDir
            If Not Directory.Exists(_logDir) Then
                Directory.CreateDirectory(_logDir)
            End If
        End Sub

    End Class
End Namespace
