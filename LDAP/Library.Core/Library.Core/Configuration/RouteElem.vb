Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml.Serialization

Namespace Library.Core.Configuration
    <Serializable(), XmlRoot("Route")> _
    Public Class RouteElem
        Private m_Name As String
        <XmlAttribute("Name")> _
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set(ByVal value As String)
                m_Name = value
            End Set
        End Property
        Private m_MustCrypt As Boolean
        <XmlAttribute("MustCrypt")> _
        Public Property MustCrypt() As Boolean
            Get
                Return m_MustCrypt
            End Get
            Set(ByVal value As Boolean)
                m_MustCrypt = value
            End Set
        End Property
        Private m_Crypt As Boolean
        <XmlAttribute("Crypt")> _
        Public Property Crypt() As Boolean
            Get
                Return m_Crypt
            End Get
            Set(ByVal value As Boolean)
                m_Crypt = value
            End Set
        End Property
        Private m_StringElems As List(Of StringElem)
        <XmlElement("String")> _
        Public Property StringElems() As List(Of StringElem)
            Get
                Return m_StringElems
            End Get
            Set(ByVal value As List(Of StringElem))
                m_StringElems = value
            End Set
        End Property
        Private m_GroupElems As List(Of GroupElem)
        <XmlElement("Group")> _        
        Public Property GroupElems() As List(Of GroupElem)
            Get
                Return m_GroupElems
            End Get
            Set(ByVal value As List(Of GroupElem))
                m_GroupElems = value
            End Set
        End Property
    End Class
End Namespace