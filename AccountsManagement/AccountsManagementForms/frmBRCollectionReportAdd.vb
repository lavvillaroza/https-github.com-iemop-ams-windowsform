Public Class frmBRCollectionReportAdd

    Public dateFrom As Date
    Public dateTo As Date
    Public listDueDate As List(Of Date)
    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub frmBRCollectionReportAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub
End Class