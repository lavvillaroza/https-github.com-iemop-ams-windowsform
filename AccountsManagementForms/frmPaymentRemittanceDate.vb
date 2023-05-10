Imports AccountsManagementObjects
Public Class frmPaymentRemittanceDate
    Public RemittanceDateSelected As Date
    Public AllocationDate As Date
    Public iFSelected As Boolean
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Dim OffsetFromDate As Date
        Dim OffsetToDate As Date

        'Due Date must be in range based on the config dates        
        RemittanceDateSelected = CDate(DTRemittance.Value.ToShortDateString)
        OffsetFromDate = AllocationDate.AddDays(CInt(WESMBillOffsetFromDate) + 1)
        OffsetToDate = AllocationDate.AddDays(CInt(WESMBillOffsetToDate) + 1)
        If RemittanceDateSelected < OffsetFromDate Or RemittanceDateSelected > OffsetToDate Then
            MessageBox.Show("Specified remittance date must be in " & FormatDateTime(OffsetFromDate, DateFormat.ShortDate) & " to " & _
                       FormatDateTime(OffsetToDate, DateFormat.ShortDate) & " range!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            iFSelected = False        
        Else
            iFSelected = True
            Me.Close()
        End If

    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        iFSelected = False
        Me.Close()
    End Sub
End Class