Option Infer On
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports LogicalAccessManager.BO
Imports System.Globalization
Imports LogicalAccess.LDAP.Lib
Public Class MainForm        
    Private _LaLogs As LaCollection(Of LaLogs) = New LaLogsInfo()
    Private _laUsers As LaCollection(Of LaUser) = New LaUserInfo()
    Private _laApplications As LaCollection(Of LaApplication) = New LaApplicationInfo()
    Private _laModules As LaCollection(Of LaModule) = New LaModuleInfo()
    Private _laUsersModule As LaCollection(Of LaUserModule) = New LaUserModuleInfo()
    Private editLaUser As LaUser = New LaUser
    Private _userId As String = ""
    Private _userIdSet As String = ""
    Private _as As AuthenticationService = New AuthenticationService
    Private _ls As LogService = New LogService    

    Public Function SystemAuthentication() As Boolean
        Try
            Dim _LoginFrm As New Login
            _LoginFrm.HeaderTitle = "Login (Logical Access Manager)"
            _LoginFrm.ApplicationName = "Logical Access Manager"
            _LoginFrm.AccessWindow = "WinMainForm"
            If _LoginFrm.ShowDialog(Me) = DialogResult.OK Then
                _userId = _LoginFrm.UserLogged
                Return True
            End If
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try       
    End Function

    Public Function SettingsAuthentication() As Boolean
        Dim _loginMain = New Login()
        _loginMain.HeaderTitle = "Login (Logical Access Manager)"
        _loginMain.ApplicationName = "Logical Access Manager"
        _loginMain.AccessWindow = "WinSettings"

        If _loginMain.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            _userId = _loginMain.UserLogged
            Return True
        End If
        Return False
    End Function

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If SystemAuthentication() Then

        Else
            Me.Close()
        End If
    End Sub

    Public Sub ReInitialize()
        _LaLogs = New LaLogsInfo()
        _laUsers = New LaUserInfo()
        _laApplications = New LaApplicationInfo()
        _laModules = New LaModuleInfo()
        _laUsersModule = New LaUserModuleInfo()
        editLaUser = New LaUser
        _as = New AuthenticationService
        _ls = New LogService
    End Sub

    Private Sub Logs_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Logs_btn.Click
        If Add_btn.Enabled = True Then Add_btn.Enabled = False
        If Edit_btn.Enabled = True Then Edit_btn.Enabled = False
        If Delete_btn.Enabled = True Then Delete_btn.Enabled = False
        LogsReload()
    End Sub
    Private Sub LogsReload()
        ToolStatusTip.Text = "Logs View"
        PAGENO_CMB.Enabled = True
        PAGENO_CMB.Items.Clear()
        MAIN_CATEGORY_CMB.Items.Clear()
        With DynamicDGV
            .Rows.Clear()
            .Columns.Clear()
            .Columns.Add("TransactionDate", "TransactionDate")
            .Columns.Add("ApplicationName", "ApplicationName")
            .Columns.Add("ModuleName", "ModuleName")
            .Columns.Add("ComputerName", "ComputerName")
            .Columns.Add("IPAddress", "IPAddress")
            .Columns.Add("Misc1", "Misc1")
            .Columns.Add("Misc2", "Misc2")
            .Columns.Add("Misc3", "Misc3")
            .Columns.Add("ColorCode", "ColorCode")
            .Columns.Add("LogType", "LogType")
            .Columns.Add("UpdatedBy", "UpdatedBy")
            .Refresh()
        End With

        For col As Integer = 0 To DynamicDGV.ColumnCount - 1
            With MAIN_CATEGORY_CMB
                .Items.Add(DynamicDGV.Columns(col).Name)
            End With
        Next

        For page As Integer = 1 To _LaLogs.PageCount
            With PAGENO_CMB
                .Items.Add(page)
            End With
        Next

        PAGENO_CMB.SelectedIndex = 0

        'For Each logs As LaLogs In _LaLogs.PageAt(1)
        '    With DynamicDGV
        '        .Rows.Add(logs.TransactionDate, logs.ApplicationName, logs.ModuleName, logs.ComputerName, logs.IPAddress, logs.Misc1, logs.Misc2, logs.Misc3, logs.ColorCode, logs.LogType, logs.UpdatedBy)
        '        .AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine
        '    End With
        'Next        
        TOTALREC_TXTBOX.Text = _LaLogs.Items.Count.ToString("#,#", CultureInfo.InvariantCulture)
    End Sub

    Private Sub Users_btn_Click(sender As Object, e As EventArgs) Handles Users_btn.Click
        If Add_btn.Enabled = False Then Add_btn.Enabled = True
        If Edit_btn.Enabled = False Then Edit_btn.Enabled = True
        If Delete_btn.Enabled = True Then Delete_btn.Enabled = False
        UsersReload()
    End Sub

    Public Sub UsersReload()
        ToolStatusTip.Text = "Users View"
        PAGENO_CMB.Enabled = True
        PAGENO_CMB.Items.Clear()
        MAIN_CATEGORY_CMB.Items.Clear()
        With DynamicDGV
            .Rows.Clear()
            .Columns.Clear()
            .Columns.Add("UserName", "UserName")
            .Columns.Add("FirstName", "FirstName")
            .Columns.Add("MI", "MI")
            .Columns.Add("LastName", "LastName")
            .Columns.Add("Position", "Position")
            .Columns.Add("UpdatedBy", "UpdatedBy")
            .Columns.Add("UpdatedDate", "UpdatedDate")
            .Refresh()
        End With

        For col As Integer = 0 To DynamicDGV.ColumnCount - 1
            With MAIN_CATEGORY_CMB
                .Items.Add(DynamicDGV.Columns(col).Name)
            End With
        Next

        For page As Integer = 1 To _laUsers.PageCount
            With PAGENO_CMB
                .Items.Add(page)
            End With
        Next

        PAGENO_CMB.SelectedIndex = 0

        'For Each user As LaUser In _laUsers.PageAt(1)
        '    With DynamicDGV
        '        .Rows.Add(user.UserName, user.FirstName, user.MI, user.LastName, user.UpdatedBy, user.UpdatedDate)
        '        .AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine
        '    End With
        'Next
        TOTALREC_TXTBOX.Text = _laUsers.Items.Count.ToString("#,#", CultureInfo.InvariantCulture)
    End Sub

    Private Sub Applications_btn_Click(sender As Object, e As EventArgs) Handles Applications_btn.Click
        If Add_btn.Enabled = False Then Add_btn.Enabled = True
        If Edit_btn.Enabled = False Then Edit_btn.Enabled = True
        If Delete_btn.Enabled = True Then Delete_btn.Enabled = False
        ApplicationsReload()
    End Sub

    Public Sub ApplicationsReload()

        ToolStatusTip.Text = "Applications View"
        PAGENO_CMB.Enabled = True
        PAGENO_CMB.Items.Clear()
        MAIN_CATEGORY_CMB.Items.Clear()

        With DynamicDGV
            .Rows.Clear()
            .Columns.Clear()
            .Columns.Add("ApplicationId", "ApplicationId")
            .Columns.Add("ApplicationName", "ApplicationName")
            .Columns.Add("Status", "Status")
            .Columns.Add("UpdatedBy", "UpdatedBy")
            .Columns.Add("UpdatedDate", "UpdatedDate")
            .Refresh()
        End With

        For col As Integer = 0 To DynamicDGV.ColumnCount - 1
            With MAIN_CATEGORY_CMB
                .Items.Add(DynamicDGV.Columns(col).Name)
            End With
        Next

        For page As Integer = 1 To _laApplications.PageCount
            With PAGENO_CMB
                .Items.Add(page)
            End With
        Next

        PAGENO_CMB.SelectedIndex = 0

        'For Each app As LaApplication In _laApplications.PageAt(1)
        '    With DynamicDGV
        '        .Rows.Add(app.ApplicationId, app.ApplicationName, app.Status, app.UpdatedBy, app.UpdatedDate)
        '        .AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine
        '    End With
        'Next

        TOTALREC_TXTBOX.Text = _laApplications.Items.Count.ToString("#,#", CultureInfo.InvariantCulture)
    End Sub
    Private Sub Modules_btn_Click(sender As Object, e As EventArgs) Handles Modules_btn.Click
        If Add_btn.Enabled = False Then Add_btn.Enabled = True
        If Edit_btn.Enabled = False Then Edit_btn.Enabled = True
        If Delete_btn.Enabled = True Then Delete_btn.Enabled = False
        ModulesReload()
    End Sub

    Public Sub ModulesReload()

        ToolStatusTip.Text = "Modules View"
        PAGENO_CMB.Enabled = True
        PAGENO_CMB.Items.Clear()
        MAIN_CATEGORY_CMB.Items.Clear()

        With DynamicDGV
            .Rows.Clear()
            .Columns.Clear()
            .Columns.Add("ModuleId", "ModuleId")
            .Columns.Add("ApplicationId", "ApplicationId")
            .Columns.Add("ModuleName", "ModuleName")
            .Columns.Add("Status", "Status")
            .Columns.Add("UpdatedBy", "UpdatedBy")
            .Columns.Add("UpdatedDate", "UpdatedDate")
            .Refresh()
        End With

        For col As Integer = 0 To DynamicDGV.ColumnCount - 1
            With MAIN_CATEGORY_CMB
                .Items.Add(DynamicDGV.Columns(col).Name)
            End With
        Next

        For page As Integer = 1 To _laModules.PageCount
            With PAGENO_CMB
                .Items.Add(page)
            End With
        Next

        PAGENO_CMB.SelectedIndex = 0

        'For Each mdl As LaModule In _laModules.PageAt(1)
        '    With DynamicDGV
        '        .Rows.Add(mdl.ModuleId, mdl.ApplicationId, mdl.ModuleName, mdl.Status, mdl.UpdatedBy, mdl.UpdatedDate)
        '        .AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine
        '    End With
        'Next
        TOTALREC_TXTBOX.Text = _laModules.Items.Count.ToString("#,#", CultureInfo.InvariantCulture)

    End Sub

    Private Sub UsersModules_btn_Click(sender As Object, e As EventArgs) Handles UsersModules_btn.Click
        If Add_btn.Enabled = False Then Add_btn.Enabled = True
        If Edit_btn.Enabled = True Then Edit_btn.Enabled = False
        If Delete_btn.Enabled = False Then Delete_btn.Enabled = True
        UsersModulesReload()
    End Sub

    Public Sub UsersModulesReload()
        ToolStatusTip.Text = "UsersModules View"
        PAGENO_CMB.Enabled = True
        PAGENO_CMB.Items.Clear()
        MAIN_CATEGORY_CMB.Items.Clear()
        With DynamicDGV
            .Rows.Clear()
            .Columns.Clear()
            .Columns.Add("UserName", "UserName")
            .Columns.Add("FullName", "FullName")
            .Columns.Add("ApplicationName", "ApplicationName")
            .Columns.Add("ModuleName", "ModuleName")
            .Columns.Add("UpdatedBy", "UpdatedBy")
            .Columns.Add("UpdatedDate", "UpdatedDate")
            .Refresh()
        End With

        For col As Integer = 0 To DynamicDGV.ColumnCount - 1
            With MAIN_CATEGORY_CMB
                .Items.Add(DynamicDGV.Columns(col).Name)
            End With
        Next

        For page As Integer = 1 To _laUsersModule.PageCount
            With PAGENO_CMB
                .Items.Add(page)
            End With
        Next

        PAGENO_CMB.SelectedIndex = 0

        'For Each usermodule As LaUserModule In _laUsersModule.PageAt(1)
        '    With DynamicDGV
        '        .Rows.Add(usermodule.UserName, usermodule.FullName, usermodule.ModuleName, usermodule.ApplicationName, usermodule.UpdatedBy, usermodule.UpdatedDate)
        '        .AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine
        '    End With
        'Next
        TOTALREC_TXTBOX.Text = _laUsersModule.Items.Count.ToString("#,#", CultureInfo.InvariantCulture)
    End Sub

    Private Sub PAGENO_CMB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PAGENO_CMB.SelectedIndexChanged
        Dim ModuleViewed As String = ToolStatusTip.Text
        Dim PageNo As Integer = CInt(PAGENO_CMB.Text)

        Select Case ModuleViewed.ToUpper
            Case "LOGS VIEW"
                DynamicDGV.Rows.Clear()
                For Each logs As LaLogs In _LaLogs.PageAt(PageNo)
                    With DynamicDGV
                        .Rows.Add(logs.TransactionDate, logs.ApplicationName, logs.ModuleName, logs.ComputerName, logs.IPAddress, logs.Misc1, logs.Misc2, logs.Misc3, logs.ColorCode, logs.LogType, logs.UpdatedBy)
                    End With
                Next
                TOTALREC_TXTBOX.Text = _LaLogs.Items.Count.ToString("#,#", CultureInfo.InvariantCulture)
            Case "USERS VIEW"
                DynamicDGV.Rows.Clear()
                For Each user As LaUser In _laUsers.PageAt(PageNo)
                    With DynamicDGV
                        .Rows.Add(user.UserName, user.FirstName, user.MI, user.LastName, user.Position, user.UpdatedBy, user.UpdatedDate)
                    End With
                Next
                TOTALREC_TXTBOX.Text = _laUsers.Items.Count.ToString("#,#", CultureInfo.InvariantCulture)
            Case "APPLICATIONS VIEW"
                DynamicDGV.Rows.Clear()
                For Each app As LaApplication In _laApplications.PageAt(PageNo)
                    With DynamicDGV
                        .Rows.Add(app.ApplicationId, app.ApplicationName, app.Status, app.UpdatedBy, app.UpdatedDate)
                    End With
                Next
            Case "MODULES VIEW"
                DynamicDGV.Rows.Clear()
                For Each mdl As LaModule In _laModules.PageAt(PageNo)
                    With DynamicDGV
                        .Rows.Add(mdl.ModuleId, mdl.ApplicationId, mdl.ModuleName, mdl.Status, mdl.UpdatedBy, mdl.UpdatedDate)
                    End With
                Next
            Case "USERSMODULES VIEW"
                DynamicDGV.Rows.Clear()
                For Each usermodule As LaUserModule In _laUsersModule.PageAt(PageNo)
                    With DynamicDGV
                        .Rows.Add(usermodule.UserName, usermodule.FullName, usermodule.ApplicationName, usermodule.ModuleName, usermodule.UpdatedBy, usermodule.UpdatedDate)
                    End With
                Next
        End Select
    End Sub


    Private Sub SEARCH_BTN_Click(sender As Object, e As EventArgs) Handles SEARCH_BTN.Click
        Dim SearchCat As String = MAIN_CATEGORY_CMB.Text
        Dim SearchVal As String = "*" & SEARCHVAL_TXTBOX.Text & "*"
        Dim ModuleViewed As String = ToolStatusTip.Text

        Select Case ModuleViewed.ToUpper
            Case "LOGS VIEW"
                If Edit_btn.Enabled = False Then Edit_btn.Enabled = True
                Select Case SearchCat
                    Case "TransactionDate"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim LogsList = (From LL In _LaLogs.Items Where LL.TransactionDate Like SearchVal Select LL).ToList()
                        If LogsList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each LogItem In LogsList
                                With DynamicDGV
                                    .Rows.Add(LogItem.TransactionDate, LogItem.ApplicationName, LogItem.ModuleName, _
                                              LogItem.ComputerName, LogItem.IPAddress, LogItem.Misc1, LogItem.Misc2, LogItem.Misc3, LogItem.ColorCode, LogItem.LogType, LogItem.UpdatedBy)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = LogsList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "ApplicationName"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim LogsList = (From LL In _LaLogs.Items Where LL.ApplicationName.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If LogsList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each LogItem In LogsList
                                With DynamicDGV
                                    .Rows.Add(LogItem.TransactionDate, LogItem.ApplicationName, LogItem.ModuleName, _
                                              LogItem.ComputerName, LogItem.IPAddress, LogItem.Misc1, LogItem.Misc2, LogItem.Misc3, LogItem.ColorCode, LogItem.LogType, LogItem.UpdatedBy)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = LogsList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "ModuleName"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim LogsList = (From LL In _LaLogs.Items Where LL.ModuleName.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If LogsList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each LogItem In LogsList
                                With DynamicDGV
                                    .Rows.Add(LogItem.TransactionDate, LogItem.ApplicationName, LogItem.ModuleName, _
                                              LogItem.ComputerName, LogItem.IPAddress, LogItem.Misc1, LogItem.Misc2, LogItem.Misc3, LogItem.ColorCode, LogItem.LogType, LogItem.UpdatedBy)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = LogsList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "ComputerName"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim LogsList = (From LL In _LaLogs.Items Where LL.ComputerName.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If LogsList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each LogItem In LogsList
                                With DynamicDGV
                                    .Rows.Add(LogItem.TransactionDate, LogItem.ApplicationName, LogItem.ModuleName, _
                                              LogItem.ComputerName, LogItem.IPAddress, LogItem.Misc1, LogItem.Misc2, LogItem.Misc3, LogItem.ColorCode, LogItem.LogType, LogItem.UpdatedBy)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = LogsList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "IPAddress"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim LogsList = (From LL In _LaLogs.Items Where LL.IPAddress Like SearchVal Select LL).ToList()
                        If LogsList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each LogItem In LogsList
                                With DynamicDGV
                                    .Rows.Add(LogItem.TransactionDate, LogItem.ApplicationName, LogItem.ModuleName, _
                                              LogItem.ComputerName, LogItem.IPAddress, LogItem.Misc1, LogItem.Misc2, LogItem.Misc3, LogItem.ColorCode, LogItem.LogType, LogItem.UpdatedBy)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = LogsList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "Misc1"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim LogsList = (From LL In _LaLogs.Items Where LL.Misc1.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If LogsList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each LogItem In LogsList
                                With DynamicDGV
                                    .Rows.Add(LogItem.TransactionDate, LogItem.ApplicationName, LogItem.ModuleName, _
                                              LogItem.ComputerName, LogItem.IPAddress, LogItem.Misc1, LogItem.Misc2, LogItem.Misc3, LogItem.ColorCode, LogItem.LogType, LogItem.UpdatedBy)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = LogsList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "Misc2"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim LogsList = (From LL In _LaLogs.Items Where LL.Misc2.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If LogsList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each LogItem In LogsList
                                With DynamicDGV
                                    .Rows.Add(LogItem.TransactionDate, LogItem.ApplicationName, LogItem.ModuleName, _
                                              LogItem.ComputerName, LogItem.IPAddress, LogItem.Misc1, LogItem.Misc2, LogItem.Misc3, LogItem.ColorCode, LogItem.LogType, LogItem.UpdatedBy)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = LogsList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "Misc3"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim LogsList = (From LL In _LaLogs.Items Where LL.Misc3.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If LogsList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each LogItem In LogsList
                                With DynamicDGV
                                    .Rows.Add(LogItem.TransactionDate, LogItem.ApplicationName, LogItem.ModuleName, _
                                              LogItem.ComputerName, LogItem.IPAddress, LogItem.Misc1, LogItem.Misc2, LogItem.Misc3, LogItem.ColorCode, LogItem.LogType, LogItem.UpdatedBy)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = LogsList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "ColorCode"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim LogsList = (From LL In _LaLogs.Items Where LL.ColorCode.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If LogsList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each LogItem In LogsList
                                With DynamicDGV
                                    .Rows.Add(LogItem.TransactionDate, LogItem.ApplicationName, LogItem.ModuleName, _
                                              LogItem.ComputerName, LogItem.IPAddress, LogItem.Misc1, LogItem.Misc2, LogItem.Misc3, LogItem.ColorCode, LogItem.LogType, LogItem.UpdatedBy)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = LogsList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "LogType"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim LogsList = (From LL In _LaLogs.Items Where LL.LogType.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If LogsList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each LogItem In LogsList
                                With DynamicDGV
                                    .Rows.Add(LogItem.TransactionDate, LogItem.ApplicationName, LogItem.ModuleName, _
                                              LogItem.ComputerName, LogItem.IPAddress, LogItem.Misc1, LogItem.Misc2, LogItem.Misc3, LogItem.ColorCode, LogItem.LogType, LogItem.UpdatedBy)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = LogsList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "UpdatedBy"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim LogsList = (From LL In _LaLogs.Items Where LL.UpdatedBy.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If LogsList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each LogItem In LogsList
                                With DynamicDGV
                                    .Rows.Add(LogItem.TransactionDate, LogItem.ApplicationName, LogItem.ModuleName, _
                                              LogItem.ComputerName, LogItem.IPAddress, LogItem.Misc1, LogItem.Misc2, LogItem.Misc3, LogItem.ColorCode, LogItem.LogType, LogItem.UpdatedBy)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = LogsList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                End Select
            Case "USERS VIEW"
                If Edit_btn.Enabled = False Then Edit_btn.Enabled = True
                Select Case MAIN_CATEGORY_CMB.Text
                    Case "UserName"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim UsersList = (From LL In _laUsers.Items Where LL.UserName.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If UsersList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each UserItem In UsersList
                                With DynamicDGV
                                    .Rows.Add(UserItem.UserName, UserItem.FirstName, UserItem.MI, UserItem.LastName, UserItem.Position, _
                                              UserItem.UpdatedBy, UserItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = UsersList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "FirstName"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim UsersList = (From LL In _laUsers.Items Where LL.FirstName.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If UsersList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each UserItem In UsersList
                                With DynamicDGV
                                    .Rows.Add(UserItem.UserName, UserItem.FirstName, UserItem.MI, UserItem.LastName, UserItem.Position, _
                                              UserItem.UpdatedBy, UserItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = UsersList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "MI"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim UsersList = (From LL In _laUsers.Items Where LL.MI.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If UsersList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each UserItem In UsersList
                                With DynamicDGV
                                    .Rows.Add(UserItem.UserName, UserItem.FirstName, UserItem.MI, UserItem.LastName, UserItem.Position, _
                                              UserItem.UpdatedBy, UserItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = UsersList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "LastName"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim UsersList = (From LL In _laUsers.Items Where LL.LastName.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If UsersList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each UserItem In UsersList
                                With DynamicDGV
                                    .Rows.Add(UserItem.UserName, UserItem.FirstName, UserItem.MI, UserItem.LastName, UserItem.Position, _
                                              UserItem.UpdatedBy, UserItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = UsersList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "UpdatedBy"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim UsersList = (From LL In _laUsers.Items Where LL.UpdatedBy.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If UsersList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each UserItem In UsersList
                                With DynamicDGV
                                    .Rows.Add(UserItem.UserName, UserItem.FirstName, UserItem.MI, UserItem.LastName, UserItem.Position, _
                                              UserItem.UpdatedBy, UserItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = UsersList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "UpdatedDate"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim UsersList = (From LL In _laUsers.Items Where LL.UpdatedDate Like SearchVal Select LL).ToList()
                        If UsersList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each UserItem In UsersList
                                With DynamicDGV
                                    .Rows.Add(UserItem.UserName, UserItem.FirstName, UserItem.MI, UserItem.LastName, UserItem.Position, _
                                              UserItem.UpdatedBy, UserItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = UsersList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                End Select
            Case "APPLICATIONS VIEW"
                If Edit_btn.Enabled = False Then Edit_btn.Enabled = True
                Select Case MAIN_CATEGORY_CMB.Text
                    Case "ApplicationId"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim AppList = (From LL In _laApplications.Items Where LL.ApplicationId.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If AppList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each AppItem In AppList
                                With DynamicDGV
                                    .Rows.Add(AppItem.ApplicationId, AppItem.ApplicationName, AppItem.Status, AppItem.UpdatedBy, AppItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = AppList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "ApplicationName"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim AppList = (From LL In _laApplications.Items Where LL.ApplicationName.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If AppList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each AppItem In AppList
                                With DynamicDGV
                                    .Rows.Add(AppItem.ApplicationId, AppItem.ApplicationName, AppItem.Status, AppItem.UpdatedBy, AppItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = AppList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "Status"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim AppList = (From LL In _laApplications.Items Where LL.Status Like SearchVal Select LL).ToList()
                        If AppList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each AppItem In AppList
                                With DynamicDGV
                                    .Rows.Add(AppItem.ApplicationId, AppItem.ApplicationName, AppItem.Status, AppItem.UpdatedBy, AppItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = AppList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "UpdatedBy"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim AppList = (From LL In _laApplications.Items Where LL.UpdatedBy.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If AppList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each AppItem In AppList
                                With DynamicDGV
                                    .Rows.Add(AppItem.ApplicationId, AppItem.ApplicationName, AppItem.Status, AppItem.UpdatedBy, AppItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = AppList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "UpdatedDate"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim AppList = (From LL In _laApplications.Items Where LL.UpdatedDate Like SearchVal Select LL).ToList()
                        If AppList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each AppItem In AppList
                                With DynamicDGV
                                    .Rows.Add(AppItem.ApplicationId, AppItem.ApplicationName, AppItem.Status, AppItem.UpdatedBy, AppItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = AppList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                End Select

            Case "MODULES VIEW"
                If Edit_btn.Enabled = False Then Edit_btn.Enabled = True
                Select Case MAIN_CATEGORY_CMB.Text
                    Case "ModuleId"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim ModList = (From LL In _laModules.Items Where LL.ModuleId Like SearchVal Select LL).ToList()
                        If ModList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each ModItem In ModList
                                With DynamicDGV
                                    .Rows.Add(ModItem.ModuleId, ModItem.ApplicationId, ModItem.ModuleName, ModItem.Status, ModItem.UpdatedBy, ModItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = ModList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "ApplicationId"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim ModList = (From LL In _laModules.Items Where LL.ApplicationId Like SearchVal Select LL).ToList()
                        If ModList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each ModItem In ModList
                                With DynamicDGV
                                    .Rows.Add(ModItem.ModuleId, ModItem.ApplicationId, ModItem.ModuleName, ModItem.Status, ModItem.UpdatedBy, ModItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = ModList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "ModuleName"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim ModList = (From LL In _laModules.Items Where LL.ModuleName.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If ModList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each ModItem In ModList
                                With DynamicDGV
                                    .Rows.Add(ModItem.ModuleId, ModItem.ApplicationId, ModItem.ModuleName, ModItem.Status, ModItem.UpdatedBy, ModItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = ModList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "Status"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim ModList = (From LL In _laModules.Items Where LL.Status Like SearchVal Select LL).ToList()
                        If ModList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each ModItem In ModList
                                With DynamicDGV
                                    .Rows.Add(ModItem.ModuleId, ModItem.ApplicationId, ModItem.ModuleName, ModItem.Status, ModItem.UpdatedBy, ModItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = ModList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "UpdatedBy"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim ModList = (From LL In _laModules.Items Where LL.UpdatedBy.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If ModList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each ModItem In ModList
                                With DynamicDGV
                                    .Rows.Add(ModItem.ModuleId, ModItem.ApplicationId, ModItem.ModuleName, ModItem.Status, ModItem.UpdatedBy, ModItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = ModList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "UpdatedDate"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim ModList = (From LL In _laModules.Items Where LL.UpdatedDate Like SearchVal Select LL).ToList()
                        If ModList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each ModItem In ModList
                                With DynamicDGV
                                    .Rows.Add(ModItem.ModuleId, ModItem.ApplicationId, ModItem.ModuleName, ModItem.Status, ModItem.UpdatedBy, ModItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = ModList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                End Select
            Case "USERSMODULES VIEW"
                Edit_btn.Enabled = False
                Select Case MAIN_CATEGORY_CMB.Text
                    Case "UserName"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim UserModList = (From LL In _laUsersModule.Items Where LL.UserName.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If UserModList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each UserModItem In UserModList
                                With DynamicDGV
                                    .Rows.Add(UserModItem.UserName, UserModItem.FullName, UserModItem.ApplicationName, _
                                              UserModItem.ModuleName, UserModItem.UpdatedBy, UserModItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = UserModList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "FullName"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim UserModList = (From LL In _laUsersModule.Items Where LL.FullName.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If UserModList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each UserModItem In UserModList
                                With DynamicDGV
                                    .Rows.Add(UserModItem.UserName, UserModItem.FullName, UserModItem.ApplicationName, _
                                              UserModItem.ModuleName, UserModItem.UpdatedBy, UserModItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = UserModList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "ApplicationName"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim UserModList = (From LL In _laUsersModule.Items Where LL.ApplicationName.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If UserModList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each UserModItem In UserModList
                                With DynamicDGV
                                    .Rows.Add(UserModItem.UserName, UserModItem.FullName, UserModItem.ApplicationName, _
                                              UserModItem.ModuleName, UserModItem.UpdatedBy, UserModItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = UserModList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "ModuleName"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim UserModList = (From LL In _laUsersModule.Items Where LL.ModuleName.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If UserModList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each UserModItem In UserModList
                                With DynamicDGV
                                    .Rows.Add(UserModItem.UserName, UserModItem.FullName, UserModItem.ApplicationName, _
                                              UserModItem.ModuleName, UserModItem.UpdatedBy, UserModItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = UserModList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "UpdatedBy"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim UserModList = (From LL In _laUsersModule.Items Where LL.UpdatedBy.ToUpper Like SearchVal.ToUpper Select LL).ToList()
                        If UserModList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each UserModItem In UserModList
                                With DynamicDGV
                                    .Rows.Add(UserModItem.UserName, UserModItem.FullName, UserModItem.ApplicationName, _
                                              UserModItem.ModuleName, UserModItem.UpdatedBy, UserModItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = UserModList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                    Case "UpdatedDate"
                        DynamicDGV.Rows.Clear()
                        PAGENO_CMB.Enabled = False
                        Dim UserModList = (From LL In _laUsersModule.Items Where LL.UpdatedDate Like SearchVal Select LL).ToList()
                        If UserModList.Count = 0 Then
                            MessageBox.Show("No Record Found", "Category Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            For Each UserModItem In UserModList
                                With DynamicDGV
                                    .Rows.Add(UserModItem.UserName, UserModItem.FullName, UserModItem.ApplicationName, _
                                              UserModItem.ModuleName, UserModItem.UpdatedBy, UserModItem.UpdatedDate)
                                End With
                            Next
                        End If
                        TOTALREC_TXTBOX.Text = UserModList.Count.ToString("#,#", CultureInfo.InvariantCulture)
                End Select

        End Select
    End Sub

    Private Sub CLEAR_BTN_Click(sender As Object, e As EventArgs) Handles CLEAR_BTN.Click
        Dim ModuleViewed As String = ToolStatusTip.Text
        Select Case ModuleViewed.ToUpper
            Case "LOGS VIEW"
                LogsReload()
            Case "USERS VIEW"
                UsersReload()
            Case "APPLICATIONS VIEW"
                ApplicationsReload()
            Case "MODULES VIEW"
                ModulesReload()
            Case "USERSMODULES VIEW"
                UsersModulesReload()
        End Select
        SEARCHVAL_TXTBOX.Clear()
        PAGENO_CMB.Enabled = True
    End Sub

    Private Sub Add_btn_Click(sender As Object, e As EventArgs) Handles Add_btn.Click
        Dim ModuleViewed As String = ToolStatusTip.Text
        Select Case ModuleViewed.ToUpper
            Case "LOGS VIEW"

            Case "USERS VIEW"
                With UserForm
                    .Text = "Add New User"
                    If .UF_USERNAME_TXTBOX.ReadOnly = True Then
                        .UF_USERNAME_TXTBOX.ReadOnly = False
                    End If
                    .UF_USERNAME_TXTBOX.Clear()
                    .UF_FIRSTNAME_TXTBOX.Clear()
                    .UF_MI_TXTBOX.Clear()
                    .UF_LASTNAME_TXTBOX.Clear()
                    .UF_POSITION_TXTBOX.Clear()
                    .ShowDialog()
                End With                
            Case "APPLICATIONS VIEW"
                With ApplicationForm
                    .Text = "Add New Application"
                    .AF_APPID_TXTBOX.Clear()
                    .AF_APPNAME_TXTBOX.Clear()
                    .AF_ACTIVE_RADBTN.Checked = False
                    .AF_INACTIVE_RADBTN.Checked = False
                    .ShowDialog()
                End With                
            Case "MODULES VIEW"
                With ModuleForm
                    .Text = "Add New Module"
                    With .MF_MDLID_TXTBOX
                        .Clear()
                        .ReadOnly = True
                        .Text = "Auto Increment"
                        .BackColor = Color.Gold
                        .ForeColor = Color.Gray
                    End With

                    With .MF_APPID_CMBBOX
                        .Focus()
                        .Items.Clear()
                        For Each iLaApplication As LaApplication In _laApplications.Items
                            .Items.Add(iLaApplication.ApplicationId)
                        Next
                    End With
                    .MF_MDLNAME_TXTBOX.Clear()
                    .MF_ACTIVE_RADBTN.Checked = False
                    .MF_INACTIVE_RADBTN.Checked = False
                    .ShowDialog()
                End With
            Case "USERSMODULES VIEW"
                With UserModuleForm
                    With .UMF_USERNAME_CMBBOX
                        For Each iLauser As LaUser In _laUsers.Items
                            .Items.Add(iLauser.UserName)
                        Next
                    End With
                    With .UMF_APPNAME_CMBBOX
                        For Each iLaApplication As LaApplication In _laApplications.Items
                            .Items.Add(iLaApplication.ApplicationName)
                        Next
                    End With
                    .Text = "Add New User Module"
                    .ShowDialog()
                End With
        End Select
    End Sub

    Private Sub Edit_btn_Click(sender As Object, e As EventArgs) Handles Edit_btn.Click
        Dim ModuleViewed As String = ToolStatusTip.Text
        Select Case ModuleViewed.ToUpper
            Case "LOGS VIEW"

            Case "USERS VIEW"
                With DynamicDGV
                    For Each row As DataGridViewRow In DynamicDGV.Rows
                        If row.Selected = True Then
                            With UserForm
                                With .UF_USERNAME_TXTBOX                                    
                                    .ReadOnly = True
                                    .Text = row.Cells(0).Value.ToString
                                End With
                                With .UF_FIRSTNAME_TXTBOX
                                    .Focus()
                                    .Text = row.Cells(1).Value.ToString
                                End With
                                .UF_MI_TXTBOX.Text = If(row.Cells(2).Value.ToString.Length = 0, "", row.Cells(2).Value.ToString)
                                .UF_LASTNAME_TXTBOX.Text = If(row.Cells(3).Value.ToString.Length = 0, "", row.Cells(3).Value.ToString)
                                .UF_POSITION_TXTBOX.Text = If(row.Cells(4).Value.ToString.Length = 0, "", row.Cells(4).Value.ToString)
                            End With
                        End If
                    Next
                End With
                With UserForm
                    .Text = "Edit Existing User"                   
                    .ShowDialog()
                End With
            Case "APPLICATIONS VIEW"
                With DynamicDGV
                    For Each row As DataGridViewRow In DynamicDGV.Rows
                        If row.Selected = True Then
                            With ApplicationForm
                                With .AF_APPID_TXTBOX
                                    .ReadOnly = True
                                    .Text = row.Cells(0).Value.ToString
                                End With
                                With .AF_APPNAME_TXTBOX                                    
                                    .Text = row.Cells(1).Value.ToString
                                End With
                                If row.Cells(2).Value.ToString = "1" Then
                                    .AF_ACTIVE_RADBTN.Checked = True
                                    .AF_INACTIVE_RADBTN.Checked = False
                                ElseIf row.Cells(2).Value.ToString = "2" Then
                                    .AF_ACTIVE_RADBTN.Checked = False
                                    .AF_INACTIVE_RADBTN.Checked = True
                                End If
                            End With
                        End If
                    Next
                End With
                With ApplicationForm
                    .Text = "Edit Existing Application"
                    .ShowDialog()
                End With
            Case "MODULES VIEW"
                With DynamicDGV
                    For Each row As DataGridViewRow In DynamicDGV.Rows
                        If row.Selected = True Then
                            With ModuleForm
                                With .MF_MDLID_TXTBOX
                                    .ReadOnly = True
                                    .Text = row.Cells(0).Value.ToString
                                End With
                                With .MF_APPID_CMBBOX
                                    .Items.Add(row.Cells(1).Value.ToString)
                                    .SelectedIndex = 0
                                End With
                                With .MF_MDLNAME_TXTBOX
                                    .ReadOnly = True
                                    .Text = row.Cells(2).Value.ToString
                                End With
                                If row.Cells(3).Value.ToString = "1" Then
                                    .MF_ACTIVE_RADBTN.Checked = True
                                ElseIf row.Cells(3).Value.ToString = "2" Then
                                    .MF_INACTIVE_RADBTN.Checked = True
                                End If
                            End With
                        End If
                    Next
                End With
                With ModuleForm
                    .Text = "Edit Existing Module"
                    .ShowDialog()
                End With
            Case "USERSMODULES VIEW"

        End Select
        
    End Sub

    Private Sub Delete_btn_Click(sender As Object, e As EventArgs) Handles Delete_btn.Click
        Dim ModuleViewed As String = ToolStatusTip.Text
        Dim _iLaUserModule As New LaUserModuleSql
        Dim msgque As DialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Add New User", MessageBoxButtons.YesNo, MessageBoxIcon.Question)


        If msgque = DialogResult.Yes Then
            Select Case ModuleViewed.ToUpper
                Case "USERSMODULES VIEW"
                    For Each row As DataGridViewRow In DynamicDGV.Rows
                        If row.Selected = True Then
                            Try
                                _iLaUserModule.GetModuleList(row.Cells(2).Value)
                                _iLaUserModule.DeleteUserModule(row.Cells(0).Value, row.Cells(3).Value)
                                MessageBox.Show("You have been successfully deleted the record", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit For
                            Catch ex As Exception
                                MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit For
                            End Try                            
                        End If
                    Next
                    ReInitialize()
                    UsersModulesReload()
            End Select
        Else
            MessageBox.Show("You chose no.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
End Class