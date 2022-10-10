Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.IO
Imports System.Xml.Serialization

Namespace Library.Core.XmlSerialize
    Public Class Serializer
        Public Shared Sub Serialize(ByVal obj As Object, ByVal xmlPath As String)
            Try
                Dim sw = New StreamWriter(xmlPath)
                Dim x = New XmlSerializer(obj.[GetType]())
                x.Serialize(sw, obj)
                sw.Close()            
            Catch ex As Exception
                Throw New Exception(ex.Message, ex.InnerException)
            End Try
        End Sub

        Public Shared Function Deserialize(ByVal objType As Type, ByVal xmlPath As String) As Object
            Dim ret = New Object()
            Try
                Dim sr = New StreamReader(xmlPath)
                Dim x = New XmlSerializer(objType)
                ret = DirectCast(x.Deserialize(sr), Object)
                sr.Close()
            Catch ex As Exception
                Throw New Exception(ex.Message, ex.InnerException)
            End Try
            Return ret
        End Function
    End Class
End Namespace