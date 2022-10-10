Imports LogicalAccessManager.BO
Public Class UserModuleForm

    Private iLaUserModuleSql As New LaUserModuleSql
    Private Sub UMF_APPNAME_CMBBOX_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UMF_APPNAME_CMBBOX.SelectedIndexChanged

        Dim iAppName As String

        iAppName = UMF_APPNAME_CMBBOX.Text
        With UMF_MDLNAME_CMBBOX
            .Items.Clear()
            For Each iItem In iLaUserModuleSql.GetModuleList(iAppName).ToList()
                .Items.Add(iItem)
            Next
        End With
    End Sub

    Private Sub UserModuleForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub UMF_SAVE_BTN_Click(sender As Object, e As EventArgs) Handles UMF_SAVE_BTN.Click
        Dim _UserName As String = If(UMF_USERNAME_CMBBOX.Text.Length = 0, "", UMF_USERNAME_CMBBOX.Text)
        Dim _AppName As String = If(UMF_APPNAME_CMBBOX.Text.Length = 0, "", UMF_APPNAME_CMBBOX.Text)
        Dim _MdlName As String = If(UMF_MDLNAME_CMBBOX.Text.Length = 0, "", UMF_MDLNAME_CMBBOX.Text)

        If _UserName.Length > 0 And _AppName.Length > 0 And _MdlName.Length > 0 Then
            Try
                iLaUserModuleSql.AddUserModule(_UserName, _MdlName, "LAVV", Date.Now.ToShortDateString)
                MessageBox.Show("You have been successfully added new module for the selected user", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        MainForm.ReInitialize()
        MainForm.UsersModulesReload()
    End Sub

    Private Sub UMF_CANCEL_BTN_Click(sender As Object, e As EventArgs) Handles UMF_CANCEL_BTN.Click
        Me.Close()
    End Sub
End Class