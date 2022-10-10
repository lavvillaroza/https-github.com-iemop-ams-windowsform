Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Net
Imports System.Net.Sockets

Namespace LogicalAccess.LDAP.Lib
    Public Class OtherService
        Public Shared Function GetIpAddress() As String
            Dim host As IPHostEntry
            Dim localIP As String = ""
            host = Dns.GetHostEntry(Dns.GetHostName())
            For Each ip As IPAddress In host.AddressList
                If ip.AddressFamily = AddressFamily.InterNetwork Then
                    localIP = ip.ToString()
                    Exit For
                End If
            Next
            Return localIP
        End Function

        Public Shared Function GetComputerName() As String
            Return Environment.MachineName
        End Function
    End Class
End Namespace


