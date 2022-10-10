Option Explicit On
Option Strict On

Imports System.IO
Public Class frmUploadFile

#Region "Properties"
    Private _FilterType As Integer
    Public Property Filtertype() As Integer
        Get
            Return _FilterType
        End Get
        Set(ByVal value As Integer)
            _FilterType = value
        End Set
    End Property

    Private _DirectoryName As String
    Public Property DirectoryName() As String
        Get
            Return _DirectoryName
        End Get
        Set(ByVal value As String)
            _DirectoryName = value
        End Set
    End Property

    Private _FileName As String
    Public Property FileName() As String
        Get
            Return _FileName
        End Get
        Set(ByVal value As String)
            _FileName = value
        End Set
    End Property


#End Region

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        If Not Me.FormValidation Then
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnOpenFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFile.Click
        Dim openFD As New OpenFileDialog

        With openFD
            Select Case Me.Filtertype
                'CSV
                Case 1
                    .Filter = "CSV Files (*.csv)|*.csv"
                Case 2

            End Select

            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.txtDirectory.Text = .FileName

                Me.DirectoryName = Path.GetDirectoryName(Me.txtDirectory.Text.Trim())
                Me.FileName = Path.GetFileName(Me.txtDirectory.Text.Trim())
            End If
        End With
    End Sub

    Private Function FormValidation() As Boolean
        If Me.txtDirectory.Text.Trim.Length = 0 Then
            MsgBox("Please specify the file to be uploaded.!", MsgBoxStyle.Critical, "Invalid")
            Exit Function
        ElseIf Not File.Exists(Me.txtDirectory.Text.Trim()) Then
            MsgBox("File does not exist!", MsgBoxStyle.Critical, "Invalid")
            Exit Function
        End If

        Return True
    End Function
End Class