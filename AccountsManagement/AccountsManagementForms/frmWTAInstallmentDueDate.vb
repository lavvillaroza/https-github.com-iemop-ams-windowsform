Public Class frmWTAInstallmentDueDate
    Public newDueDate As Nullable(Of Date)
    Public oldDueDate As Date
    Private Sub frmWTAInstallmentDueDate_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub setDateSelection(ByVal dueDate As Date)
        Me.dtNewDueDate.Value = dueDate
        Me.oldDueDate = dueDate
    End Sub

    Private Sub btn_back_Click(sender As Object, e As EventArgs) Handles btn_back.Click
        newDueDate = Nothing
        Me.Close()
    End Sub

    Private Sub btn_ok_Click(sender As Object, e As EventArgs) Handles btn_ok.Click
        newDueDate = Me.dtNewDueDate.Value
        Me.Close()
    End Sub

End Class