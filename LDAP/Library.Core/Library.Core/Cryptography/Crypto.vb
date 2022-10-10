Imports System.IO
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Security.Cryptography

Namespace Library.Core.Cryptography
    Public Class Crypto
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

        Public Function Encrypt(ByVal regString As String) As String
            Dim ret = ""
            Try
                Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(InitVector)
                Dim saltBytes As Byte() = Encoding.ASCII.GetBytes(Salt)
                Dim regStringByte As Byte() = Encoding.ASCII.GetBytes(regString)
                Dim password As New Rfc2898DeriveBytes(PasswordPhrase, saltBytes, PasswordIteration)

                Dim intSize As Integer = Convert.ToInt32(Size \ 8)
                Dim Bytes As Byte() = password.GetBytes(intSize)

                Dim symmetric As New RijndaelManaged()
                symmetric.Mode = System.Security.Cryptography.CipherMode.CBC

                Dim encryptor As ICryptoTransform = symmetric.CreateEncryptor(Bytes, initVectorBytes)
                Dim memoryStream As New MemoryStream()

                Dim cryptoStream As New CryptoStream(memoryStream, encryptor, System.Security.Cryptography.CryptoStreamMode.Write)
                cryptoStream.Write(regStringByte, 0, regStringByte.Length)
                cryptoStream.FlushFinalBlock()

                Dim cipherTextBytes As Byte() = memoryStream.ToArray()

                memoryStream.Close()
                cryptoStream.Close()

                Dim cipherText As String
                cipherText = Convert.ToBase64String(cipherTextBytes)

                ret = cipherText
            Catch
            End Try
            Return ret
        End Function

        Public Function Decrypt(ByVal cipherString As String) As String
            Dim ret = ""

            Try
                Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(InitVector)
                Dim saltValueBytes As Byte() = Encoding.ASCII.GetBytes(Salt)
                Dim cipherTextBytes As Byte() = Convert.FromBase64String(cipherString)
                Dim password As New Rfc2898DeriveBytes(PasswordPhrase, saltValueBytes, PasswordIteration)

                Dim intSize As Integer = Convert.ToInt32(Size \ 8)
                Dim Bytes As Byte() = password.GetBytes(intSize)

                Dim symmetric As New RijndaelManaged()
                symmetric.Mode = CipherMode.CBC

                Dim decryptor As ICryptoTransform = symmetric.CreateDecryptor(Bytes, initVectorBytes)

                Dim memoryStream As New MemoryStream(cipherTextBytes)
                Dim cryptoStream As New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)
                Dim plainTextBytes As Byte() = New Byte(cipherTextBytes.Length - 1) {}

                Dim decryptedByteCount As Integer = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length)

                memoryStream.Close()
                cryptoStream.Close()

                ret = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount)
            Catch
            End Try
            Return ret
        End Function

        Public Sub New(ByVal cryptoSettings As CryptoSettings)
            Salt = cryptoSettings.Salt
            PasswordPhrase = cryptoSettings.PasswordPhrase
            PasswordIteration = cryptoSettings.PasswordIteration
            InitVector = cryptoSettings.InitVector
            Size = cryptoSettings.Size
        End Sub
    End Class
End Namespace