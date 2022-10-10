Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Text.RegularExpressions
Imports LogicalAccessManager.BO

Public Class OracConfigForm    
    ReadOnly AllowedKeys As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789-_.:/\"
    Private _connDetails As ConnectionDetails = ConnectionDetails.Instance
    Private _AdServer As String
    Private _AdPort As String
    Private _AdBaseDn As String
    Private _DbDatasource As String
    Private _DbUserId As String
    Private _DbPassword As String

    Public Property AdServer() As String
        Get
            Return _AdServer
        End Get
        Set(value As String)
            _AdServer = value
        End Set
    End Property

    Public Property Adport() As String
        Get
            Return _AdPort
        End Get
        Set(value As String)
            _AdPort = value
        End Set
    End Property

    Public Property AdBaseDn() As String
        Get
            Return _AdBaseDn
        End Get
        Set(value As String)
            _AdBaseDn = value
        End Set
    End Property

    Public Property DbDatasource() As String
        Get
            Return _DbDatasource
        End Get
        Set(value As String)
            _DbDatasource = value
        End Set
    End Property

    Public Property DbUserId() As String
        Get
            Return _DbUserId
        End Get
        Set(value As String)
            _DbUserId = value
        End Set
    End Property

    Public Property DbPassword() As String
        Get
            Return _DbPassword
        End Get
        Set(value As String)
            _DbPassword = value
        End Set
    End Property

    Private Function IsValidConfig() As Boolean
        Dim errCount = 0

        errorProvider1.Clear()
        'Clearing all errors
        'Active Dir
        If String.IsNullOrEmpty(txtServerAd.Text) Then
            errCount += 1
            errorProvider1.SetError(txtServerAd, "This field cannot be blank.")
        End If
        If String.IsNullOrEmpty(txtPortAd.Text) Then
            errCount += 1
            errorProvider1.SetError(txtPortAd, "This field cannot be blank.")
        End If
        If String.IsNullOrEmpty(txtBaseDnAd.Text) Then
            errCount += 1
            errorProvider1.SetError(txtBaseDnAd, "This field cannot be blank.")
        End If

        'Db
        If String.IsNullOrEmpty(txtDatasourceDb.Text) Then
            errCount += 1
            ErrorProvider1.SetError(txtDatasourceDb, "This field cannot be blank.")
        End If
        If String.IsNullOrEmpty(txtUserIdDb.Text) Then
            errCount += 1
            errorProvider1.SetError(txtUserIdDb, "This field cannot be blank.")
        End If
        If String.IsNullOrEmpty(txtPasswordDb.Text) Then
            errCount += 1
            errorProvider1.SetError(txtPasswordDb, "This field cannot be blank.")
        End If

        If errCount > 0 Then
            Return False
        End If

        Return True
    End Function

    Private Function HasChanged() As Boolean
        'Active Dir
        If AdServer <> txtServerAd.Text Then
            Return True
        End If
        If AdPort <> txtPortAd.Text Then
            Return True
        End If
        If AdBaseDn <> txtBaseDnAd.Text Then
            Return True
        End If

        'Database
        If DbDatasource <> txtDatasourceDb.Text Then
            Return True
        End If
        If DbUserId <> txtUserIdDb.Text Then
            Return True
        End If
        If DbPassword <> txtPasswordDb.Text Then
            Return True
        End If

        Return False
    End Function

    Private Sub OCF_SAVE_BTN_Click(sender As Object, e As EventArgs) Handles OCF_SAVE_BTN.Click

        If IsValidConfig() Then
            If HasChanged() Then
                AdServer = txtServerAd.Text
                Adport = txtPortAd.Text
                AdBaseDn = txtBaseDnAd.Text

                DbDatasource = txtDatasourceDb.Text
                DbUserId = txtUserIdDb.Text
                DbPassword = txtPasswordDb.Text

                DialogResult = DialogResult.OK
            Else
                MessageBox.Show(Me, "No changes.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            MessageBox.Show(Me, "Please fill the fields with the valid value. " & vbLf + "Hover the error icon to see an error description.", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End If

    End Sub

    Private Sub OCF_CANCEL_BTN_Click(sender As Object, e As EventArgs) Handles OCF_CANCEL_BTN.Click
        Me.Close()
    End Sub

    Private Sub OracConfigForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtServerAd.Text = AdServer
        txtPortAd.Text = Adport
        txtBaseDnAd.Text = AdBaseDn
        txtDatasourceDb.Text = DbDatasource
        txtUserIdDb.Text = DbUserId
        txtPasswordDb.Text = DbPassword
    End Sub

    Private Sub txtDatasourceDb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDatasourceDb.KeyPress
        Select Case e.KeyChar
            Case Convert.ToChar(Keys.Enter) ' Enter is pressed
                Me.txtUserIdDb.Select()
            Case Convert.ToChar(Keys.Back) ' Backspace is pressed
                'e.Handled = False ' Delete the character
            Case Convert.ToChar(Keys.Capital Or Keys.RButton) ' CTRL+V is pressed
                ' Paste clipboard content only if contains allowed keys
                e.Handled = Not Clipboard.GetText().All(Function(c) AllowedKeys.Contains(c))
            Case Else ' Other key is pressed
                e.Handled = Not AllowedKeys.Contains(e.KeyChar)
        End Select
    End Sub

    Private Sub txtUserIdDb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUserIdDb.KeyPress
        Select Case e.KeyChar
            Case Convert.ToChar(Keys.Enter) ' Enter is pressed
                Me.txtPasswordDb.Select()
            Case Convert.ToChar(Keys.Back) ' Backspace is pressed
                'e.Handled = False ' Delete the character
            Case Convert.ToChar(Keys.Capital Or Keys.RButton) ' CTRL+V is pressed
                ' Paste clipboard content only if contains allowed keys
                e.Handled = Not Clipboard.GetText().All(Function(c) AllowedKeys.Contains(c))
            Case Else ' Other key is pressed
                e.Handled = Not AllowedKeys.Contains(e.KeyChar)
        End Select
    End Sub

    Private Sub txtPasswordDb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPasswordDb.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            Me.OCF_SAVE_BTN.Select()
        End If
    End Sub

    Private Sub txtServerAd_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtServerAd.KeyPress
        Select Case e.KeyChar
            Case Convert.ToChar(Keys.Enter) ' Enter is pressed
                Me.txtPortAd.Select()
            Case Convert.ToChar(Keys.Back) ' Backspace is pressed
                'e.Handled = False ' Delete the character
            Case Convert.ToChar(Keys.Capital Or Keys.RButton) ' CTRL+V is pressed
                ' Paste clipboard content only if contains allowed keys
                e.Handled = Not Clipboard.GetText().All(Function(c) AllowedKeys.Contains(c))
            Case Else ' Other key is pressed
                e.Handled = Not AllowedKeys.Contains(e.KeyChar)
        End Select
    End Sub

    Private Sub txtPortAd_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPortAd.KeyPress
        Select Case e.KeyChar
            Case Convert.ToChar(Keys.Enter) ' Enter is pressed
                Me.txtBaseDnAd.Select()
            Case Convert.ToChar(Keys.Back) ' Backspace is pressed
                'e.Handled = False ' Delete the character
            Case Convert.ToChar(Keys.Capital Or Keys.RButton) ' CTRL+V is pressed
                ' Paste clipboard content only if contains allowed keys
                e.Handled = Not Clipboard.GetText().All(Function(c) AllowedKeys.Contains(c))
            Case Else ' Other key is pressed
                e.Handled = Not AllowedKeys.Contains(e.KeyChar)
        End Select
    End Sub

    Private Sub txtBaseDnAd_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBaseDnAd.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            Me.OCF_SAVE_BTN.Select()
        End If
    End Sub
End Class