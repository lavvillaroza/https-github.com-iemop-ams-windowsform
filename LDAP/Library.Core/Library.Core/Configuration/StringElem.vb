Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml.Serialization

Namespace Library.Core.Configuration
    <Serializable(), XmlRoot("String")> _
    Public Class StringElem
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

        Private m_Value As String
        <XmlText()> _
        Public Property Value() As String
            Get
                Return m_Value
            End Get
            Set(ByVal value As String)
                m_Value = value
            End Set
        End Property

    End Class
End Namespace