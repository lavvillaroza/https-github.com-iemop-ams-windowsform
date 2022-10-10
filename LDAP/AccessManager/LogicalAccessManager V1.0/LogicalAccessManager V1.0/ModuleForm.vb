Imports LogicalAccessManager.BO
Public Class ModuleForm

    Private Sub MF_SAVE_BTN_Click(sender As Object, e As EventArgs) Handles MF_SAVE_BTN.Click
        Dim oLaModule As New LaModuleSQl

        Select Case Me.Text
            Case "Add New Module"
                Dim msgque As DialogResult = MessageBox.Show("Are you sure you want to save the additional module?", "System Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If msgque = DialogResult.Yes Then
                    If Trim(MF_MDLID_TXTBOX.TextLength) > 0 And Trim(MF_APPID_CMBBOX.Text.Length) > 0 And _
                        Trim(MF_MDLNAME_TXTBOX.TextLength) > 0 Then
                        Try
                            If MF_ACTIVE_RADBTN.Checked = True Then
                                oLaModule.AddModule(MF_MDLID_TXTBOX.Text, MF_APPID_CMBBOX.Text, MF_MDLNAME_TXTBOX.Text, 1, "LAVV", Date.Now.ToShortDateString)
                                MessageBox.Show("You have been successfully added new module", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Close()
                            ElseIf MF_INACTIVE_RADBTN.Checked = True Then
                                oLaModule.AddModule(MF_MDLID_TXTBOX.Text, MF_APPID_CMBBOX.Text, MF_MDLNAME_TXTBOX.Text, 0, "LAVV", Date.Now.ToShortDateString)
                                MessageBox.Show("You have been successfully added new module", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Close()
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                        MainForm.ReInitialize()
                        MainForm.ModulesReload()
                    Else
                        MessageBox.Show("Please supply the required fields!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Else
                    MessageBox.Show("You chose no.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Case "Edit Existing Module"
                Dim msgque As DialogResult = MessageBox.Show("Are you sure you want to update this module?", "System Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If msgque = DialogResult.Yes Then
                    If Trim(MF_MDLID_TXTBOX.TextLength) > 0 And Trim(MF_APPID_CMBBOX.Text.Length) > 0 And _
                        Trim(MF_MDLNAME_TXTBOX.TextLength) > 0 Then
                        Try
                            If MF_ACTIVE_RADBTN.Checked = True Then
                                oLaModule.AddModule(MF_MDLID_TXTBOX.Text, MF_APPID_CMBBOX.Text, MF_MDLNAME_TXTBOX.Text, 1, "LAVV", Date.Now.ToShortDateString)
                                MessageBox.Show("You have been successfully edited existing module", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Close()
                            ElseIf MF_INACTIVE_RADBTN.Checked = True Then
                                oLaModule.AddModule(MF_MDLID_TXTBOX.Text, MF_APPID_CMBBOX.Text, MF_MDLNAME_TXTBOX.Text, 0, "LAVV", Date.Now.ToShortDateString)
                                MessageBox.Show("You have been successfully edited existing module", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Close()
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                        MainForm.ReInitialize()
                        MainForm.ModulesReload()
                    Else
                        MessageBox.Show("Please supply the required fields!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Else
                    MessageBox.Show("You chose no.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
        End Select        
    End Sub

    Private Sub MF_CANCEL_BTN_Click(sender As Object, e As EventArgs) Handles MF_CANCEL_BTN.Click
        With Me
            .Close()
        End With
    End Sub

    Private Sub ModuleForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class