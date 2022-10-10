Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace Library.Core.Cryptography
    Public Class CryptoSettings
        Public Property Salt() As String
            Get
                Return m_Salt
            End Get
            Set(ByVal value As String)
                m_Salt = value
            End Set
        End Property
        Private m_Salt As String
        Public Property PasswordPhrase() As String
            Get
                Return m_PasswordPhrase
            End Get
            Set(ByVal value As String)
                m_PasswordPhrase = value
            End Set
        End Property
        Private m_PasswordPhrase As String
        Public Property PasswordIteration() As Integer
            Get
                Return m_PasswordIteration
            End Get
            Set(ByVal value As Integer)
                m_PasswordIteration = value
            End Set
        End Property
        Private m_PasswordIteration As Integer
        Public Property InitVector() As String
            Get
                Return m_InitVector
            End Get
            Set(ByVal value As String)
                m_InitVector = value
            End Set
        End Property
        Private m_InitVector As String
        Public Property Size() As Integer
            Get
                Return m_Size
            End Get
            Set(ByVal value As Integer)
                m_Size = value
            End Set
        End Property
        Private m_Size As Integer
    End Class
End Namespace