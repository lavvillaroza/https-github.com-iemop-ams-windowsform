Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Library.Core.Cryptography

Namespace Library.Core.Configuration
    Public Class ConfigCrypto
        Public Property SaltKey() As String
            Get
                Return m_SaltKey
            End Get
            Set(ByVal value As String)
                m_SaltKey = value
            End Set
        End Property
        Private m_SaltKey As String
        Public Property PassPhrase() As String
            Get
                Return m_PassPhrase
            End Get
            Set(ByVal value As String)
                m_PassPhrase = value
            End Set
        End Property
        Private m_PassPhrase As String
        Public Property PassIteration() As Integer
            Get
                Return m_PassIteration
            End Get
            Set(ByVal value As Integer)
                m_PassIteration = value
            End Set
        End Property
        Private m_PassIteration As Integer
        Public Property InitVector() As String
            Get
                Return m_InitVector
            End Get
            Set(ByVal value As String)
                m_InitVector = value
            End Set
        End Property
        Private m_InitVector As String
        Public Property KeySize() As Integer
            Get
                Return m_KeySize
            End Get
            Set(ByVal value As Integer)
                m_KeySize = value
            End Set
        End Property
        Private m_KeySize As Integer

        Private _cryptSett As New CryptoSettings() With { _
          .Salt = "WesmPassword", _
          .PasswordPhrase = "WesmCrypto", _
          .PasswordIteration = 2, _
          .InitVector = "@1B2c3D4e5F6g7H8", _
          .Size = 128 _
        }

        Public Function Encrypt(ByVal plain As String) As String
            Return New Crypto(_cryptSett).Encrypt(plain)
        End Function

        Public Function Decrypt(ByVal cipher As String) As String
            Return New Crypto(_cryptSett).Decrypt(cipher)
        End Function
    End Class
End Namespace