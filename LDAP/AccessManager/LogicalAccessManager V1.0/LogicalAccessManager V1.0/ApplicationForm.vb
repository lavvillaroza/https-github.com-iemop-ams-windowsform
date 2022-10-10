Imports LogicalAccessManager.BO
Public Class ApplicationForm

    Private Sub ApplicationForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub AF_SAVE_BTN_Click(sender As Object, e As EventArgs) Handles AF_SAVE_BTN.Click
        Dim oLaApplication As New LaApplicationsql

        Select Case Me.Text
            Case "Add New Application"
                Dim msgque As DialogResult = MessageBox.Show("Are you sure you want to save the additional application?", "Add New User", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If msgque = DialogResult.Yes Then
                    If Trim(AF_APPID_TXTBOX.TextLength) > 0 And Trim(AF_APPNAME_TXTBOX.TextLength) > 0 Then
                        Try
                            If AF_ACTIVE_RADBTN.Checked = True Then
                                oLaApplication.AddApp(AF_APPID_TXTBOX.Text, AF_APPNAME_TXTBOX.Text, 1, "LAVV", Date.Now.ToShortDateString)
                                MessageBox.Show("You have been successfully added new application", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Close()
                            ElseIf AF_INACTIVE_RADBTN.Checked = True Then
                                oLaApplication.AddApp(AF_APPID_TXTBOX.Text, AF_APPNAME_TXTBOX.Text, 0, "LAVV", Date.Now.ToShortDateString)
                                MessageBox.Show("You have been successfully added new application", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Close()
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    End If
                Else
                    MessageBox.Show("You chose no.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Case "Edit Existing Application"
                Dim msgque As DialogResult = MessageBox.Show("Are you sure you want to update this application?", "Edit New User", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If msgque = DialogResult.Yes Then
                    If Trim(AF_APPID_TXTBOX.TextLength) > 0 And Trim(AF_APPNAME_TXTBOX.TextLength) > 0 Then
                        Try
                            If AF_ACTIVE_RADBTN.Checked = True Then
                                oLaApplication.EditApp(AF_APPID_TXTBOX.Text, AF_APPNAME_TXTBOX.Text, 1, "LAVV", Date.Now.ToShortDateString)
                                MessageBox.Show("You have been successfully updated existing application", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Close()
                            ElseIf AF_INACTIVE_RADBTN.Checked = True Then
                                oLaApplication.EditApp(AF_APPID_TXTBOX.Text, AF_APPNAME_TXTBOX.Text, 0, "LAVV", Date.Now.ToShortDateString)
                                MessageBox.Show("You have been successfully updated existing application", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Close()
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    End If
                Else
                    MessageBox.Show("You chose no.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
        End Select
        MainForm.ReInitialize()
        MainForm.ApplicationsReload()
    End Sub

    Private Sub AF_CANCEL_BTN_Click(sender As Object, e As EventArgs) Handles AF_CANCEL_BTN.Click
        With Me
            .Close()
        End With
    End Sub
End Class