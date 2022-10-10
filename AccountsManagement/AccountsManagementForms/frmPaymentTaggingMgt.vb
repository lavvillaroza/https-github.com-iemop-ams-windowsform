Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmPaymentTaggingMgt
    Private _PaymentTagHelper As New PaymentTaggingHelper
    Private Sub frmPaymentTaggingMgt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Me.DGridViewCollection.Rows.Clear()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            ProgressThread.Show("Please wait while loding...")
            Dim frmPaymentTaDetails As New frmPaymentTaggingDetails
            With frmPaymentTaDetails
                ._PaymentTagHelper = _PaymentTagHelper
                ProgressThread.Close()
                .ShowDialog()
            End With

            If _PaymentTagHelper.NewPaymentTag Is Nothing Then
                ProgressThread.Close()
                MessageBox.Show("No added data!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If _PaymentTagHelper.NewPaymentTag.PaymentTagNo = 0 Then
                ProgressThread.Close()
                MessageBox.Show("Data is not save!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            With DGridViewCollection
                .Rows.Add(_PaymentTagHelper.NewPaymentTag.PaymentTagNo.ToString("d7"),
                          _PaymentTagHelper.NewPaymentTag.RemittanceDate.ToString("MM/dd/yyyy"),
                          _PaymentTagHelper.NewPaymentTag.BillingIDNumber.IDNumber,
                          FormatNumber(_PaymentTagHelper.NewPaymentTag.TotalAmountTagged.ToString("0.00"), UseParensForNegativeNumbers:=TriState.True))
            End With
            _PaymentTagHelper.NewPaymentTag = New PaymentTag
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            ProgressThread.Show("Please wait while searching...")
            Dim dateFrom As Date = CDate(FormatDateTime(Me.dtFrom.Value, DateFormat.ShortDate))
            Dim dateTo As Date = CDate(FormatDateTime(Me.dtTo.Value, DateFormat.ShortDate))
            _PaymentTagHelper.SearchByDateRangeForPaymentTagged(dateFrom, dateTo)
            If _PaymentTagHelper.ViewListOfPaymentTag.Count <> 0 Then
                PutDataInDisplayGrid()
                ProgressThread.Close()

            Else
                ProgressThread.Close()
                MessageBox.Show("No available data found!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Try
            If Me.DGridViewCollection.Rows.Count = 0 Then
                MessageBox.Show("No available data!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.DGridViewCollection.CurrentRow.Index = -1 Then
                MessageBox.Show("No selected data!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim i As Integer = Me.DGridViewCollection.CurrentRow.Index
            Dim PaymentTagNo As Long = CLng(Me.DGridViewCollection.Rows(i).Cells(0).Value)

            ProgressThread.Show("Please wait while loding...")

            Dim frmPaymentTaDetails As New frmPaymentTaggingDetails
            With frmPaymentTaDetails
                ._PaymentTagHelper = _PaymentTagHelper
                ._ViewPaymentTag = True
                ._PaymentTagNo = PaymentTagNo
                ProgressThread.Close()
                .ShowDialog()
            End With

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PutDataInDisplayGrid()
        DGridViewCollection.Rows.Clear()
        For Each item In _PaymentTagHelper.ViewListOfPaymentTag
            With DGridViewCollection
                .Rows.Add(item.PaymentTagNo.ToString("d7"), item.RemittanceDate.ToString("MM/dd/yyyy"), item.BillingIDNumber.IDNumber,
                          FormatNumber(item.TotalAmountTagged.ToString("0.00"), UseParensForNegativeNumbers:=TriState.True))
            End With
        Next
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class