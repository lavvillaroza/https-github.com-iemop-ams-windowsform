Imports LogicalAccessManager.BO
Public Class UserForm

    Private Sub AddUserForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub UF_SAVE_BTN_Click(sender As Object, e As EventArgs) Handles UF_SAVE_BTN.Click
        Dim oLaUser As New LaUserSql

        Select Case Me.Text
            Case "Add New User"
                Dim msgque As DialogResult = MessageBox.Show("Are you sure you want to save the additional user?", "Add New User", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If msgque = DialogResult.Yes Then
                    If Trim(UF_USERNAME_TXTBOX.TextLength) > 0 And Trim(UF_FIRSTNAME_TXTBOX.TextLength) > 0 And _
                        Trim(UF_MI_TXTBOX.TextLength) > 0 And Trim(UF_LASTNAME_TXTBOX.TextLength) > 0 And Trim(UF_POSITION_TXTBOX.TextLength) > 0 Then
                        Try
                            oLaUser.AddUser(UF_USERNAME_TXTBOX.Text, UF_FIRSTNAME_TXTBOX.Text, UF_MI_TXTBOX.Text, UF_LASTNAME_TXTBOX.Text, UF_POSITION_TXTBOX.Text, "LAVV", Date.Now.ToShortDateString)
                            MessageBox.Show("You have been successfully added new user", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.Close()
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    End If
                Else
                    MessageBox.Show("You chose no.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Case "Edit Existing User"
                Dim msgque As DialogResult = MessageBox.Show("Are you sure you want to update this user?", "Edit New User", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If msgque = DialogResult.Yes Then
                    If Trim(UF_USERNAME_TXTBOX.TextLength) > 0 And Trim(UF_FIRSTNAME_TXTBOX.TextLength) > 0 And _
                        Trim(UF_MI_TXTBOX.TextLength) > 0 And Trim(UF_LASTNAME_TXTBOX.TextLength) > 0 And Trim(UF_POSITION_TXTBOX.TextLength) > 0 Then
                        Try
                            oLaUser.EditUser(UF_USERNAME_TXTBOX.Text, UF_FIRSTNAME_TXTBOX.Text, UF_MI_TXTBOX.Text, UF_LASTNAME_TXTBOX.Text, UF_POSITION_TXTBOX.Text, "LAVV", Date.Now.ToShortDateString)
                            MessageBox.Show("You have been successfully edit the existing user", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.Close()
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    End If
                Else
                    MessageBox.Show("You chose no.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
        End Select
        MainForm.ReInitialize()
        MainForm.UsersReload()
    End Sub

    Private Sub UF_CANCEL_BTN_Click(sender As Object, e As EventArgs) Handles UF_CANCEL_BTN.Click
        With Me
            .Close()
        End With
    End Sub
End Class