Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports LogicalAccess.LDAP.BO
Imports Library.Core.Configuration
Imports LogicalAccess.LDAP.Lib
Imports System.IO

Public Class Login
    Private _configPath As String = "appconfig.xml" 'Path.Combine(Environment.CurrentDirectory, "appconfig.xml") 
    Private _logDir As String = "Logs"
    Private _as As AuthenticationService
    Private _ls As LogService
    Private _dsUsername As String = "ChangeConfig"
    Private _dsPassword As String = DateTime.Now.ToString("MMddyyyy")
    Private _HeaderTitle As String
    Private _ApplicationName As String
    Private _AccessWindow As String
    Private _UserLogged As String

    
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

        _dsPassword = Microsoft.VisualBasic.Strings.StrReverse(_dsPassword)
        _as = New AuthenticationService(_configPath)

        If Not String.IsNullOrEmpty(HeaderTitle) Then
            Me.Text = HeaderTitle
        Else
            Me.Text = "Login"
        End If

        lblVersion.Text = "v " + Application.ProductVersion

        If _as.IsConfigValid Then
            _as.AccessWindow = AccessWindow
            _ls = New LogService(_as.DbDatasource, _as.DbUserId, _as.DbPassword)
        Else
            MessageBox.Show(Me, _as.GetErrMessage, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Dim _ocFrm = New OracConfigForm()

            'Ad
            _ocFrm.AdServer = _as.AdServer
            _ocFrm.Adport = _as.AdPort
            _ocFrm.AdBaseDn = _as.AdBaseDn
            'Db
            _ocFrm.DbDatasource = _as.DbDatasource
            _ocFrm.DbUserId = _as.DbUserId
            _ocFrm.DbPassword = _as.DbPassword

            If _ocFrm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                'ad
                _as.AdServer = _ocFrm.AdServer
                _as.AdPort = _ocFrm.Adport
                _as.AdBaseDn = _ocFrm.AdBaseDn
                'db
                _as.DbDatasource = _ocFrm.DbDatasource
                _as.DbUserId = _ocFrm.DbUserId
                _as.DbPassword = _ocFrm.DbPassword
                _as.SaveConfig()

                MessageBox.Show(Me, "Application will automatically restart.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Application.Restart()
            Else
                DialogResult = DialogResult.Cancel
            End If
        End If
    End Sub


    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If _dsUsername = txtUsername.Text And _dsPassword = txtPassword.Text Then
            Dim _ocFrm = New OracConfigForm()
            'Ad
            _ocFrm.AdServer = _as.AdServer
            _ocFrm.Adport = _as.AdPort
            _ocFrm.AdBaseDn = _as.AdBaseDn
            'Db
            _ocFrm.DbDatasource = _as.DbDatasource
            _ocFrm.DbUserId = _as.DbUserId
            _ocFrm.DbPassword = _as.DbPassword            
            If _ocFrm.ShowDialog() = DialogResult.OK Then
                'ad
                _as.AdServer = _ocFrm.AdServer
                _as.AdPort = _ocFrm.Adport
                _as.AdBaseDn = _ocFrm.AdBaseDn
                'db
                _as.DbDatasource = _ocFrm.DbDatasource
                _as.DbUserId = _ocFrm.DbUserId
                _as.DbPassword = _ocFrm.DbPassword
                _as.SaveConfig()
                MessageBox.Show(Me, "Application will automatically restart.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Application.Restart()
            End If            
            txtUsername.Text = ""
            txtPassword.Text = ""
            Return
        End If
        If _as.AuthUserCredentials(txtUsername.Text, txtPassword.Text, ApplicationName) Then
            DialogResult = DialogResult.OK
        Else
            MessageBox.Show(Me, "Access denied!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtPassword.Text = ""
        End If
    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
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

End Class
