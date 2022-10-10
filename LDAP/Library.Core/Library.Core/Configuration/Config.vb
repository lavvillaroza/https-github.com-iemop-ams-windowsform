Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml.Serialization

Namespace Library.Core.Configuration
    <Serializable(), XmlRoot("Configuration")> _
    Public Class Config
        Private m_Routes As List(Of RouteElem)
        <XmlElement("Route")> _
        Public Property Routes() As List(Of RouteElem)
            Get
                Return m_Routes
            End Get
            Set(ByVal value As List(Of RouteElem))
                m_Routes = value
            End Set
        End Property

    End Class
End Namespace