Public Class frmFlashScreen
    Private Sub frmFlashScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        panel_progressBar.Width += 15

        If panel_progressBar.Width >= 750 Then
            Timer1.Stop()
            Dim frmMain As MainForm = New MainForm
            Application.Run(frmMain)
            Me.Hide()
        End If
    End Sub
End Class