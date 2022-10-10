Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.IO
Imports System.Net

Namespace Library.Core.FTP
    Public Class FtpControl
        Public Shared Sub GetFile(ByVal userId As String, ByVal password As String, ByVal ftpDir As String, ByVal localDir As String, ByVal filename As String)
            Try
                Dim tempFilename = If(ftpDir.Substring(0, 6).ToUpper() = "FTP://", ftpDir, "FTP://" & ftpDir)
                tempFilename = If(tempFilename.Substring(tempFilename.Length - 1, 1) = "/", tempFilename.Substring(0, tempFilename.Length - 1), tempFilename)

                Dim ftpReq As FtpWebRequest = DirectCast(WebRequest.Create(String.Format("{0}/{1}", tempFilename, filename)), FtpWebRequest)
                ftpReq.Method = WebRequestMethods.Ftp.DownloadFile
                ftpReq.Credentials = New NetworkCredential(userId, password)

                Dim ftpRes As FtpWebResponse = DirectCast(ftpReq.GetResponse(), FtpWebResponse)
                Dim stream As Stream = ftpRes.GetResponseStream()
                Dim fs As New FileStream(Path.Combine(localDir, filename), FileMode.Create)
                Dim buf As Byte() = New Byte(2047) {}
                Dim dataRead As Integer = 0

                Do
                    dataRead = stream.Read(buf, 0, buf.Length)
                    fs.Write(buf, 0, dataRead)
                Loop While dataRead > buf.Length

                stream.Close()
                fs.Flush()
                fs.Close()

                ftpReq = Nothing
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Shared Sub PutFile(ByVal userId As String, ByVal password As String, ByVal ftpDir As String, ByVal sourceDir As String, ByVal filename As String)
            Try
                Dim tempFilename = If(ftpDir.Substring(0, 6).ToUpper() = "FTP://", ftpDir, "FTP://" & ftpDir)
                tempFilename = If(tempFilename.Substring(tempFilename.Length - 1, 1) = "/", tempFilename.Substring(0, tempFilename.Length - 1), tempFilename)

                Dim ftpReq As FtpWebRequest = DirectCast(WebRequest.Create(String.Format("{0}/{1}", tempFilename, filename)), FtpWebRequest)
                ftpReq.Method = WebRequestMethods.Ftp.UploadFile
                ftpReq.Credentials = New NetworkCredential(userId, password)

                Dim buf As Byte() = New Byte(2047) {}
                Dim dataRead As Integer = 0

                Dim fi As New FileInfo(Path.Combine(sourceDir, filename))
                Dim fs As FileStream = fi.OpenRead()
                Dim stream As Stream = ftpReq.GetRequestStream()

                Do
                    dataRead = fs.Read(buf, 0, buf.Length)
                    stream.Write(buf, 0, dataRead)
                Loop While dataRead > buf.Length

                stream.Close()
                fs.Flush()
                fs.Close()

                ftpReq = Nothing
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Shared Sub RemoveFile(ByVal userId As String, ByVal password As String, ByVal ftpDir As String, ByVal filename As String)
            Try
                Dim ftpReq As FtpWebRequest = DirectCast(WebRequest.Create(String.Format("{0}/{1}", ftpDir, filename)), FtpWebRequest)
                ftpReq.Method = WebRequestMethods.Ftp.DeleteFile
                ftpReq.Credentials = New NetworkCredential(userId, password)
                ftpReq.GetResponse()

                ftpReq = Nothing
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Shared Function GetFileList(ByVal userId As String, ByVal password As String, ByVal ftpDir As String) As List(Of String)
            Dim ret As New List(Of String)()

            Try
                Dim ftpReq As FtpWebRequest = DirectCast(WebRequest.Create(ftpDir), FtpWebRequest)
                ftpReq.Method = WebRequestMethods.Ftp.ListDirectory
                ftpReq.Credentials = New NetworkCredential(userId, password)

                Dim ftpRes As FtpWebResponse = DirectCast(ftpReq.GetResponse(), FtpWebResponse)
                Dim stream As Stream = ftpRes.GetResponseStream()
                Dim sr As New StreamReader(stream)

                Dim line As String = ""

                Do
                    line = sr.ReadLine()
                    If Not String.IsNullOrEmpty(line) Then
                        ret.Add(line)
                    End If
                Loop While String.IsNullOrEmpty(line)


                stream.Dispose()
                sr.Close()
                ftpReq = Nothing
            Catch ex As Exception
                Throw ex
            End Try

            Return ret
        End Function
    End Class
End Namespace