Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Configuration
Imports Library.Core.Configuration
Imports WESMLib.Auth.BO
Imports WESMLib.Auth.Lib
Imports System.IO
Imports System.Security.AccessControl


Public Class Login
    Private _configPath As String = Path.Combine(Environment.CurrentDirectory, "config.xml")
    Private _logDir As String = "Logs"
    Private _as As AuthenticationService
    Private _ls As LogService
    Private _dsUsername As String = "ChangeConfig"
    Private _dsPassword As String = DateTime.Now.ToString("MMddyyyy")
    Private _HeaderTitle As String
    Private _ApplicationName As String
    Private _AccessWindow As String
    Private _UserLogged As String
    Private LogCounter As Integer
    Public Sub InsertLog(transactionDate As DateTime, applicationName As String, moduleName As String, misc1 As String, misc2 As String,
                         misc3 As String, colorCode As ColorCode, logtype As String)
        If _as.IsConfigValid Then
            _ls.InsertLog(transactionDate, applicationName, moduleName, misc1, misc2, misc3, colorCode, logtype, _as.UserLogged)
        End If
    End Sub

    Public Sub InsertLog(transactionDate As DateTime, applicationName As String, moduleName As String, misc1 As String, misc2 As String,
                         misc3 As String, colorCode As ColorCode, logtype As String, userId As String)
        If _as.IsConfigValid Then
            _ls.InsertLog(transactionDate, applicationName, moduleName, misc1, misc2, misc3, colorCode, logtype, userId)
        End If
    End Sub

    Public Sub SaveConfig()
        _as.SaveConfig()
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load        

        LogCounter = 0
        Try
            _dsPassword = Microsoft.VisualBasic.Strings.StrReverse(_dsPassword)
            _as = New AuthenticationService(_configPath)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Connection Error", MessageBoxButtons.OK)
            Exit Sub
        End Try

        If Not String.IsNullOrEmpty(HeaderTitle) Then
            Me.Text = HeaderTitle
        Else
            Me.Text = "Login"
        End If

        If _as.IsConfigValid Then
            _as.AccessWindow = AccessWindow
            _ls = New LogService(_as.AMSDbDatasource, _as.AMSDbUserId, _as.AMSDbPassword, _logDir)
        Else
            MessageBox.Show(Me, _as.GetErrMessage, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Dim _ocFrm = New Settings()
            'Ad
            _ocFrm.AdServer = _as.AdServer
            _ocFrm.Adport = _as.AdPort
            _ocFrm.AdBaseDn = _as.AdBaseDn
            'CRSSDb
            _ocFrm.CRSSDbServer = _as.CRSSDbServer
            _ocFrm.CRSSDbName = _as.CRSSDbDatabase
            _ocFrm.CRSSDbUserId = _as.CRSSDbUserId
            _ocFrm.CRSSDbPassword = _as.CRSSDbPassword
            'AMSDb
            _ocFrm.AMSDbDatasource = _as.AMSDbDatasource
            _ocFrm.AMSDbUserId = _as.AMSDbUserId
            _ocFrm.AMSDbPassword = _as.AMSDbPassword

            If _ocFrm.ShowDialog() = DialogResult.OK Then
                'ad
                _as.AdServer = _ocFrm.AdServer
                _as.AdPort = _ocFrm.Adport
                _as.AdBaseDn = _ocFrm.AdBaseDn
                'CRSSdb
                _as.CRSSDbServer = _ocFrm.CRSSDbServer
                _as.CRSSDbDatabase = _ocFrm.CRSSDbName
                _as.CRSSDbUserId = _ocFrm.CRSSDbUserId
                _as.CRSSDbPassword = _ocFrm.CRSSDbPassword
                'AMSdb
                _as.AMSDbDatasource = _ocFrm.AMSDbDatasource
                _as.AMSDbUserId = _ocFrm.AMSDbUserId
                _as.AMSDbPassword = _ocFrm.AMSDbPassword
                _as.SaveConfig()
                MessageBox.Show(Me, "Application will automatically restart.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Application.Restart()
            Else
                DialogResult = Windows.Forms.DialogResult.Cancel
            End If
        End If
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click        
        If _dsUsername = txtUsername.Text And _dsPassword = txtPassword.Text Then
            Dim _ocFrm = New Settings()
            'Ad
            _ocFrm.AdServer = _as.AdServer
            _ocFrm.Adport = _as.AdPort
            _ocFrm.AdBaseDn = _as.AdBaseDn
            'CRSSDb
            _ocFrm.CRSSDbServer = _as.CRSSDbServer
            _ocFrm.CRSSDbName = _as.CRSSDbDatabase
            _ocFrm.CRSSDbUserId = _as.CRSSDbUserId
            _ocFrm.CRSSDbPassword = _as.CRSSDbPassword
            'AMSDb
            _ocFrm.AMSDbDatasource = _as.AMSDbDatasource
            _ocFrm.AMSDbUserId = _as.AMSDbUserId
            _ocFrm.AMSDbPassword = _as.AMSDbPassword
            If _ocFrm.ShowDialog() = DialogResult.OK Then
                'ad
                _as.AdServer = _ocFrm.AdServer
                _as.AdPort = _ocFrm.Adport
                _as.AdBaseDn = _ocFrm.AdBaseDn
                'db
                _as.CRSSDbServer = _ocFrm.CRSSDbServer
                _as.CRSSDbDatabase = _ocFrm.CRSSDbName
                _as.CRSSDbUserId = _ocFrm.CRSSDbUserId
                _as.CRSSDbPassword = _ocFrm.CRSSDbPassword
                'AMSdb
                _as.AMSDbDatasource = _ocFrm.AMSDbDatasource
                _as.AMSDbUserId = _ocFrm.AMSDbUserId
                _as.AMSDbPassword = _ocFrm.AMSDbPassword
                _as.SaveConfig()
                MessageBox.Show(Me, "Application will automatically restart.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                System.Diagnostics.Process.Start(Application.ExecutablePath)
                Me.Close()
            End If
            txtUsername.Text = ""
            txtPassword.Text = ""
            Return
        End If
        Try            
            If _as.AuthUserCredentials(txtUsername.Text, txtPassword.Text, ApplicationName) Then
                DialogResult = DialogResult.OK
            Else
                If txtUsername.Text.Length = 0 And txtPassword.Text.Length = 0 And LogCounter <> 0 Then
                    MessageBox.Show(Me, "Please provide user credentials!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                MessageBox.Show(Me, "Access denied!", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtPassword.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtPassword.SelectAll()            
            Exit Sub
        End Try
    End Sub

    Private Sub txtUsername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUsername.KeyPress
        Select Case e.KeyChar
            Case Convert.ToChar(Keys.Enter) ' Enter is pressed
                txtPassword.Select()
        End Select

    End Sub

    Private Sub txtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress
        Select Case e.KeyChar
            Case Convert.ToChar(Keys.Enter) ' Enter is pressed
                btnLogin.Select()
        End Select
    End Sub

    Public Property HeaderTitle() As String
        Get
            Return _HeaderTitle
        End Get
        Set(value As String)
            _HeaderTitle = value
        End Set
    End Property

    Public Property ApplicationName() As String
        Get
            Return _ApplicationName
        End Get
        Set(value As String)
            _ApplicationName = value
        End Set
    End Property

    Public Property AccessWindow() As String
        Get
            Return _AccessWindow
        End Get
        Set(value As String)
            _AccessWindow = value
        End Set
    End Property

    Public ReadOnly Property UserLogged() As String
        Get
            Return _as.UserLogged
        End Get
    End Property

    Public ReadOnly Property CRSSConnectionString() As String
        Get
            Return _as.CRSSConnectionString
        End Get
    End Property


    Public ReadOnly Property AMSConnectionString() As String
        Get
            Return _as.AMSConnectionString
        End Get
    End Property

    Public Function HasAccess(ByVal access As String) As Boolean
        Return _as.HasAccess(access)
    End Function

    Private Sub btnLogin_Enter(sender As Object, e As EventArgs) Handles btnLogin.Enter
        If _dsUsername = txtUsername.Text And _dsPassword = txtPassword.Text Then
            Dim _ocFrm = New Settings()
            'Ad
            _ocFrm.AdServer = _as.AdServer
            _ocFrm.Adport = _as.AdPort
            _ocFrm.AdBaseDn = _as.AdBaseDn
            'CRSSDb
            _ocFrm.CRSSDbServer = _as.CRSSDbServer
            _ocFrm.CRSSDbName = _as.CRSSDbDatabase
            _ocFrm.CRSSDbUserId = _as.CRSSDbUserId
            _ocFrm.CRSSDbPassword = _as.CRSSDbPassword
            'AMSDb
            _ocFrm.AMSDbDatasource = _as.AMSDbDatasource
            _ocFrm.AMSDbUserId = _as.AMSDbUserId
            _ocFrm.AMSDbPassword = _as.AMSDbPassword
            If _ocFrm.ShowDialog() = DialogResult.OK Then
                'ad
                _as.AdServer = _ocFrm.AdServer
                _as.AdPort = _ocFrm.Adport
                _as.AdBaseDn = _ocFrm.AdBaseDn
                'db
                _as.CRSSDbServer = _ocFrm.CRSSDbServer
                _as.CRSSDbDatabase = _ocFrm.CRSSDbName
                _as.CRSSDbUserId = _ocFrm.CRSSDbUserId
                _as.CRSSDbPassword = _ocFrm.CRSSDbPassword
                'AMSdb
                _as.AMSDbDatasource = _ocFrm.AMSDbDatasource
                _as.AMSDbUserId = _ocFrm.AMSDbUserId
                _as.AMSDbPassword = _ocFrm.AMSDbPassword
                _as.SaveConfig()
                MessageBox.Show(Me, "Application will automatically restart.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                System.Diagnostics.Process.Start(Application.ExecutablePath)
                Me.Close()
            End If
            txtUsername.Text = ""
            txtPassword.Text = ""
            Return
        End If
        Try
            If _as.AuthUserCredentials(txtUsername.Text, txtPassword.Text, ApplicationName) Then
                DialogResult = DialogResult.OK
            Else
                If LogCounter = 0 Then
                    LogCounter += 1
                    Exit Sub
                End If
                If txtUsername.Text.Length = 0 And txtPassword.Text.Length = 0 Then
                    MessageBox.Show(Me, "Please provide user credentials!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If                
                MessageBox.Show(Me, "Access denied!", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtPassword.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End Try
    End Sub

    Private Sub cb_showPwd_CheckedChanged(sender As Object, e As EventArgs) Handles cb_showPwd.CheckedChanged
        If cb_showPwd.Checked = True Then
            txtPassword.PasswordChar = ""
        Else
            txtPassword.PasswordChar = "*"
        End If
    End Sub
End Class
