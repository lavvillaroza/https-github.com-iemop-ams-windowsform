Imports System.Drawing.Printing
Public Class frmListOfPrinter

    Private Sub frmListOfPrinter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim pkInstalledPrinters As String

        ' Find all printers installed
        For Each pkInstalledPrinters In _
            PrinterSettings.InstalledPrinters

            If Not pkInstalledPrinters.Contains("PDF") Then
                cbo_PrinterList.Items.Add(pkInstalledPrinters)
            End If
        Next pkInstalledPrinters

        ' Set the combo to the first printer in the list
        cbo_PrinterList.SelectedIndex = 0
    End Sub

    
    Private Sub btn_Ok_Click(sender As Object, e As EventArgs) Handles btn_Ok.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btn_Close_Click(sender As Object, e As EventArgs) Handles btn_Close.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class