Imports AccountsManagementLogic
Imports AccountsManagementObjects
Public Class frmWTAInstallmentTerms
    Public _WTAInstallerHelper As WTAInstallmentHelper
    Public _ListofWTASummaryForInstallment As List(Of WTAInstallment)
    Public _DicTerms As Dictionary(Of Integer, Date)

    Private Sub frmWTAInstallmentTerms_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.createTerms()
        Me.dtStartDD.Value = DateTime.Now()
        Me.btnSave.Enabled = False
    End Sub
    Private Sub createTerms()
        Me.cmbTerms.Items.Clear()
        Dim splitTerms() As String = AMModule.WTAInstallmentTerms.Split(",")
        For x = 0 To splitTerms.Length - 1
            Me.cmbTerms.Items.Add(splitTerms(x))
        Next
    End Sub

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        If Me.cmbTerms.SelectedIndex = -1 Then
            MessageBox.Show("Please selecte terms for installment!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Me.btnSave.Enabled = True

        Dim getTerm As Integer = CInt(Me.cmbTerms.SelectedItem)
        Dim getDueDate As Date = CDate(Me.dtStartDD.Value)
        With Me.DGridViewTerms
            .Rows.Clear()
            For x = 1 To getTerm
                .Rows.Add(x, getDueDate.ToString("MM/dd/yyyy"), "Edit")
                getDueDate = getDueDate.AddMonths(1)
            Next
        End With

        Me.btnCreate.Enabled = False
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub DGridViewTerms_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGridViewTerms.CellContentClick
        If DGridViewTerms.Rows(e.RowIndex).Cells(2).Value.ToString() = "Edit" Then
            Dim getDueDateSelected As Date = CDate(DGridViewTerms.Rows(e.RowIndex).Cells(1).Value)
            With frmWTAInstallmentDueDate
                .setDateSelection(getDueDateSelected)
                .ShowDialog()
                If Not .newDueDate Is Nothing Then
                    DGridViewTerms.Rows(e.RowIndex).Cells(1).Value = .newDueDate.Value.ToString("MM/dd/yyyy")
                End If
            End With
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        _DicTerms = New Dictionary(Of Integer, Date)
        For Each row As DataGridViewRow In DGridViewTerms.Rows
            Dim termNo As Integer = row.Cells("colTermNo").Value
            Dim dueDate As Date = CDate(row.Cells("colDueDate").Value)
            If Not _DicTerms.ContainsKey(termNo) Then
                _DicTerms.Add(termNo, dueDate)
            End If
        Next
        Dim ans As DialogResult = MessageBox.Show("Do you really want to save?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If ans = DialogResult.No Then
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class