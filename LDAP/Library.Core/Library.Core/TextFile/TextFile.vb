Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.IO

Namespace Library.Core.TextFile
    Public Class TextFile
        Public Shared Sub WriteTextFileSLine(ByVal line As String, ByVal path As String, ByVal append As Boolean)
            Using sw = New StreamWriter(path, append)
                sw.WriteLine(line)
                sw.Flush()
                sw.Close()
            End Using
        End Sub

        Public Shared Sub WriteTextFileMLines(ByVal lines As List(Of String), ByVal path As String, ByVal append As Boolean)
            Using sw = New StreamWriter(path, append)
                For Each s In lines
                    sw.WriteLine(s)
                Next

                sw.Flush()
                sw.Close()
            End Using
        End Sub

        Public Shared Function ReadTextFile(ByVal path As String) As List(Of String)
            Dim ret = New List(Of String)()

            Using sr = New StreamReader(path)
                Dim line = ""

                While (InlineAssignHelper(line, sr.ReadLine())) IsNot Nothing
                    ret.Add(line)
                End While

                sr.Close()
            End Using

            Return ret
        End Function
        Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
            target = value
            Return value
        End Function
    End Class
End Namespace