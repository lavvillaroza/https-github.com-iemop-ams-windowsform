Public Class frmFlashScreen
    Private Sub frmFlashScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = True
        ProgressBar1.Increment(2)
        If ProgressBar1.Value = 100 Then
            Timer1.Enabled = False
            Me.Close()
        End If
    End Sub
End Class