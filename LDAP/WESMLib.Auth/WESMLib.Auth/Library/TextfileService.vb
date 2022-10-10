Imports System.IO
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Library.Core.TextFile

Namespace WESMLib.Auth.Lib
    Public Class TextfileService : Implements IDisposable

        Private _filePath As String
        Public ReadOnly Property FilePath() As String
            Get
                Return _filePath
            End Get
        End Property

        Public Sub WriteToTextfile(stringList As List(Of String), append As Boolean)
            TextFile.WriteTextFileMLines(stringList, FilePath, append)
        End Sub

        Public Sub WriteToTextfile(msgLine As String, append As Boolean)
            TextFile.WriteTextFileSLine(msgLine, FilePath, append)
        End Sub

        Public Sub New(filePath As String)
            If Not Directory.Exists(Path.GetDirectoryName(filePath)) Then
                Directory.CreateDirectory(Path.GetDirectoryName(filePath))
            End If
            _filePath = filePath
        End Sub


#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace
