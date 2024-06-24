Imports AccountsManagementLogic
Imports AccountsManagementObjects
Public Class frmWTAInstallmentTermsView
    Private Sub frmWTAInstallmentTermsView_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Public Sub setDGView(ByVal listofWTAInstallmentTerms As List(Of WTAInstallmentTerms))
        With Me.DGridViewTerms
            .Rows.Clear()
            For Each item In listofWTAInstallmentTerms.OrderBy(Function(x) x.Term)
                .Rows.Add(item.Term,
                          item.TermDueDate.ToString("MM/dd/yyyy"),
                          item.TermNewDueDate.ToString("MM/dd/yyyy"),
                          FormatNumber(item.TermAmount.ToString("0.00"), UseParensForNegativeNumbers:=TriState.True),
                          FormatNumber(item.PaidAmount.ToString("0.00"), UseParensForNegativeNumbers:=TriState.True),
                          FormatNumber(item.DefaultAmount.ToString("0.00"), UseParensForNegativeNumbers:=TriState.True),
                          item.TermStatus.ToString())
            Next
        End With

    End Sub
End Class