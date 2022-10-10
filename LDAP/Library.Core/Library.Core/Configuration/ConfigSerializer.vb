Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Library.Core.XmlSerialize

Namespace Library.Core.Configuration
    Public Class ConfigSerializer
        Public Shared Sub SerializeConfig(ByVal config As Config, ByVal filePath As [String])
            Serializer.Serialize(config, filePath)
        End Sub

        Public Shared Function DeserializeConfig(ByVal filePath As String) As Config
            Return DirectCast(Serializer.Deserialize(New Config().[GetType](), filePath), Config)
        End Function
    End Class
End Namespace