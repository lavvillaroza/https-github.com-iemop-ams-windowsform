Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmUpdateWESMBillSummaryMgt
    Public objWBSummaryNo As New Long
    Public objWBSHelper As New UpdateWBillSummaryHelper
    Private SelectedWBSItem As New WESMBillSummary

    Private Sub frmUpdateWESMBillSummaryMgt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.DesignateItemInfo()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DesignateItemInfo()
        Dim GetWBSItem As WESMBillSummary = (From x In Me.objWBSHelper.ListOfWESMBillSummary Where x.WESMBillSummaryNo = objWBSummaryNo Select x).FirstOrDefault
        Me.SelectedWBSItem = GetWBSItem
        If GetWBSItem IsNot Nothing Then
            With GetWBSItem
                Me.ChangeParticipantID_ComboBox.DataSource = objWBSHelper.GetListofParticipantID
                Me.ChangeParticipantID_ComboBox.SelectedItem = .IDNumber.ParticipantID                
                Me.BillingPeriodNo_TextBox.Text = .BillPeriod
                Me.BillingPeriodNo_TextBox.ReadOnly = True
                Me.InvoiceNo_TextBox.Text = .INVDMCMNo
                Me.InvoiceNo_TextBox.ReadOnly = True
                Me.OrigDueDate_TextBox.Text = .DueDate
                Me.OrigDueDate_TextBox.ReadOnly = True
                Me.CurDueDate_DatePicker.Text = .OrigNewDueDate                
                Me.ChargeType_TextBox.Text = .ChargeType.ToString
                Me.ChargeType_TextBox.ReadOnly = True
                Me.EWTBalance_TextBox.Text = FormatNumber(.EnergyWithhold, UseParensForNegativeNumbers:=TriState.True).ToString
                Me.BegginingBalance_TextBox.Text = FormatNumber(.BeginningBalance, UseParensForNegativeNumbers:=TriState.True).ToString
                Me.BegginingBalance_TextBox.ReadOnly = True
                Me.CurrentBalance_TextBox.Text = FormatNumber(.EndingBalance, UseParensForNegativeNumbers:=TriState.True).ToString
                Me.CurrentBalance_TextBox.ReadOnly = True
            End With
            Me.InvChangeHistory_DGV.Columns.Clear()
            Me.InvChangeHistory_DGV.DataSource = Nothing
            Me.InvChangeHistory_DGV.DataSource = objWBSHelper.GetAMWESMBillSUmmaryClogs(objWBSummaryNo)
            Me.InvChangeHistory_DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            objWBSHelper.isCurDueDateChange = False
            objWBSHelper.isEWTBalanceChange = False
            objWBSHelper.isParticipantIDChange = False
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub CurDueDate_DatePicker_ValueChanged(sender As Object, e As EventArgs) Handles CurDueDate_DatePicker.ValueChanged
        If Not CurDueDate_DatePicker.Value = SelectedWBSItem.OrigNewDueDate Then
            objWBSHelper.isCurDueDateChange = True
        End If
    End Sub

    Private Sub ChangeParticipantID_ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChangeParticipantID_ComboBox.SelectedIndexChanged
        If Not ChangeParticipantID_ComboBox.SelectedItem = SelectedWBSItem.IDNumber.ParticipantID Then
            objWBSHelper.isParticipantIDChange = True
        End If
    End Sub

    Private Sub EWTBalance_TextBox_LostFocus(sender As Object, e As EventArgs) Handles EWTBalance_TextBox.LostFocus
        If IsNumeric(EWTBalance_TextBox.Text) = True And CDec(EWTBalance_TextBox.Text) <= 0 Then
            If Me.ChargeType_TextBox.Text = EnumChargeType.E.ToString Then
                EWTBalance_TextBox.Text = FormatNumber(EWTBalance_TextBox.Text, UseParensForNegativeNumbers:=TriState.True).ToString
                objWBSHelper.isEWTBalanceChange = True
            ElseIf Me.ChargeType_TextBox.Text = EnumChargeType.EV.ToString Then
                MessageBox.Show("Adding W/TAx is not allowed in VATonEnergy Invoice.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                EWTBalance_TextBox.Text = "0.00"
            End If
        Else
            EWTBalance_TextBox.Text = "0.00"
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If objWBSHelper.isParticipantIDChange = False And objWBSHelper.isCurDueDateChange = False And objWBSHelper.isEWTBalanceChange = False Then
            MessageBox.Show("No changes has been made.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Try
                Dim msgAns As New MsgBoxResult
                msgAns = MsgBox("Do you really want to save?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "System Message")
                If msgAns = MsgBoxResult.Yes Then
                    objWBSHelper.SaveChanges(SelectedWBSItem, Me.ChangeParticipantID_ComboBox.SelectedItem, Me.CurDueDate_DatePicker.Value, Me.EWTBalance_TextBox.Text)
                    MessageBox.Show("Changes has been saved successfully.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

End Class