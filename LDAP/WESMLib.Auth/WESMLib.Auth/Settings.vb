Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Text.RegularExpressions

Public Class Settings
    ReadOnly AllowedKeys As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789-_.:/\"
    Private _AdServer As String
    Private _AdPort As String
    Private _AdBaseDn As String
    Private _CRSSDbServer As String
    Private _CRSSDbName As String
    Private _CRSSDbUserId As String
    Private _CRSSDbPassword As String

    Private _AMSDbDatasource As String
    Private _AMSDbUserId As String
    Private _AMSDbPassword As String
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

    Public Property CRSSDbServer() As String
        Get
            Return _CRSSDbServer
        End Get
        Set(value As String)
            _CRSSDbServer = value
        End Set
    End Property

    Public Property CRSSDbName() As String
        Get
            Return _CRSSDbName
        End Get
        Set(value As String)
            _CRSSDbName = value
        End Set
    End Property

    Public Property CRSSDbUserId() As String
        Get
            Return _CRSSDbUserId
        End Get
        Set(value As String)
            _CRSSDbUserId = value
        End Set
    End Property

    Public Property CRSSDbPassword() As String
        Get
            Return _CRSSDbPassword
        End Get
        Set(value As String)
            _CRSSDbPassword = value
        End Set
    End Property

    Public Property AMSDbDatasource() As String
        Get
            Return _AMSDbDatasource
        End Get
        Set(value As String)
            _AMSDbDatasource = value
        End Set
    End Property

    Public Property AMSDbUserId() As String
        Get
            Return _AMSDbUserId
        End Get
        Set(value As String)
            _AMSDbUserId = value
        End Set
    End Property

    Public Property AMSDbPassword() As String
        Get
            Return _AMSDbPassword
        End Get
        Set(value As String)
            _AMSDbPassword = value
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
        If String.IsNullOrEmpty(txtDataBaseDb.Text) Then
            errCount += 1
            ErrorProvider1.SetError(txtDataBaseDb, "This field cannot be blank.")
        End If
        If String.IsNullOrEmpty(txtUserIdDb.Text) Then
            errCount += 1
            errorProvider1.SetError(txtUserIdDb, "This field cannot be blank.")
        End If
        If String.IsNullOrEmpty(txtPasswordDb.Text) Then
            errCount += 1
            errorProvider1.SetError(txtPasswordDb, "This field cannot be blank.")
        End If

        'Db2
        If String.IsNullOrEmpty(txtDatasourceDb2.Text) Then
            errCount += 1
            ErrorProvider1.SetError(txtDatasourceDb2, "This field cannot be blank.")
        End If
        If String.IsNullOrEmpty(txtUserIdDb2.Text) Then
            errCount += 1
            ErrorProvider1.SetError(txtUserIdDb2, "This field cannot be blank.")
        End If
        If String.IsNullOrEmpty(txtPasswordDb2.Text) Then
            errCount += 1
            ErrorProvider1.SetError(txtPasswordDb2, "This field cannot be blank.")
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

        'CRSS Database
        If _CRSSDbServer <> txtDatasourceDb.Text Then
            Return True
        End If
        If _CRSSDbName <> txtDataBaseDb.Text Then
            Return True
        End If
        If CRSSDbUserId <> txtUserIdDb.Text Then
            Return True
        End If
        If CRSSDbPassword <> txtPasswordDb.Text Then
            Return True
        End If

        'AMSDatabase 2
        If AMSDbDatasource <> txtDatasourceDb2.Text Then
            Return True
        End If
        If AMSDbUserId <> txtUserIdDb2.Text Then
            Return True
        End If
        If AMSDbPassword <> txtPasswordDb2.Text Then
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

                CRSSDbServer = txtDatasourceDb.Text
                CRSSDbName = txtDataBaseDb.Text
                CRSSDbUserId = txtUserIdDb.Text
                CRSSDbPassword = txtPasswordDb.Text

                AMSDbDatasource = txtDatasourceDb2.Text
                AMSDbUserId = txtUserIdDb2.Text
                AMSDbPassword = txtPasswordDb2.Text

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
        txtDatasourceDb.Text = CRSSDbServer
        txtDataBaseDb.Text = CRSSDbName
        txtUserIdDb.Text = CRSSDbUserId
        txtPasswordDb.Text = CRSSDbPassword
        txtDatasourceDb2.Text = AMSDbDatasource
        txtUserIdDb2.Text = AMSDbUserId
        txtPasswordDb2.Text = AMSDbPassword
    End Sub

    Private Sub txtDatasourceDb2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDatasourceDb2.KeyPress
        Select Case e.KeyChar
            Case Convert.ToChar(Keys.Enter) ' Enter is pressed
                Me.txtUserIdDb2.Select()
            Case Convert.ToChar(Keys.Back) ' Backspace is pressed
                'e.Handled = False ' Delete the character
            Case Convert.ToChar(Keys.Capital Or Keys.RButton) ' CTRL+V is pressed
                ' Paste clipboard content only if contains allowed keys
                e.Handled = Not Clipboard.GetText().All(Function(c) AllowedKeys.Contains(c))
            Case Else ' Other key is pressed
                e.Handled = Not AllowedKeys.Contains(e.KeyChar)
        End Select
    End Sub

    Private Sub txtUserIdDb2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUserIdDb2.KeyPress
        Select Case e.KeyChar
            Case Convert.ToChar(Keys.Enter) ' Enter is pressed
                Me.txtPasswordDb2.Select()
            Case Convert.ToChar(Keys.Back) ' Backspace is pressed
                'e.Handled = False ' Delete the character
            Case Convert.ToChar(Keys.Capital Or Keys.RButton) ' CTRL+V is pressed
                ' Paste clipboard content only if contains allowed keys
                e.Handled = Not Clipboard.GetText().All(Function(c) AllowedKeys.Contains(c))
            Case Else ' Other key is pressed
                e.Handled = Not AllowedKeys.Contains(e.KeyChar)
        End Select
    End Sub

    Private Sub txtPasswordDb2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPasswordDb2.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            Me.OCF_SAVE_BTN.Select()
        End If
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