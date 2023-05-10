Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmSPAMgt
    Public objSPAHelper As SPAHelper
    Public objfrmSPA As frmSPA
    Public IsSelected As Boolean = False

    Private Sub frmSPAMgt_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        objfrmSPA.LoadGridView()
    End Sub
    Private Sub frmSPAMgt_Load(sender As Object, e As EventArgs) Handles MyBase.Load        
        objSPAHelper = New SPAHelper
        objSPAHelper.InitializeAdding()
        RemoveHandler cmb_ParticipantID.SelectedIndexChanged, AddressOf cmb_ParticipantID_SelectedIndexChanged
        Me.cmb_ParticipantID.DataSource = Nothing
        Me.cmb_ParticipantID.Items.Clear()
        Me.cmb_ParticipantID.DataSource = objSPAHelper.ListOfParticipants
        Me.cmb_ParticipantID.SelectedIndex = -1        
        AddHandler cmb_ParticipantID.SelectedIndexChanged, AddressOf cmb_ParticipantID_SelectedIndexChanged
        Me.txtbox_SPARate.Text = "0"
        Me.txtbox_Months.Text = "0"        
        Me.RadioButtn_Energy.Checked = True
        Me.btn_Create.Enabled = False
    End Sub

    Private Sub txtbox_SPARate_LostFocus(sender As Object, e As EventArgs) Handles txtbox_SPARate.LostFocus
        If Me.txtbox_SPARate.Text.Trim.Length = 0 Then
            Me.txtbox_SPARate.Text = "0"
            Exit Sub
        ElseIf Not IsNumeric(Me.txtbox_SPARate.Text) Then
            MsgBox("Interest Rate must be numeric!", MsgBoxStyle.Critical, "Warning")
            Me.txtbox_SPARate.Text = "0"
            Exit Sub
        ElseIf CDec(Me.txtbox_SPARate.Text) < 0 Then
            MsgBox("Interest Rate must not be less than zero!", MsgBoxStyle.Critical, "Warning")
            Me.txtbox_SPARate.Text = "0"
            Exit Sub
        End If

        Me.txtbox_SPARate.Text = FormatNumber(CDec(Me.txtbox_SPARate.Text), 4)
        Dim TotalSPAAmount As Decimal = 0
        Dim TotalSPAInterestAmount As Decimal = 0
        Me.txtbox_GrandTotal.Text = "0"
        With Me.dgv_SPA_Mgt
            For i As Integer = 0 To .Rows.Count - 1
                If .Rows(i).Cells(0).Value = True Then
                    Dim SPAAmount As Decimal = CDec(.Rows(i).Cells(6).Value)
                    Dim SPAInterestAmount As Decimal = Math.Round(CDec(.Rows(i).Cells(6).Value) * CDec(Me.txtbox_SPARate.Text), 4)
                    TotalSPAAmount += SPAAmount
                    TotalSPAInterestAmount += SPAInterestAmount
                End If
            Next
        End With        
        Me.txtbox_GrandTotal.Text = FormatNumber(Math.Round(TotalSPAAmount + TotalSPAInterestAmount, 2), UseParensForNegativeNumbers:=TriState.True)

    End Sub

    Private Sub cmb_ParticipantID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_ParticipantID.SelectedIndexChanged

        If Me.cmb_ParticipantID.SelectedIndex = -1 Then
            Exit Sub
        ElseIf Len(Me.cmb_ParticipantID.SelectedItem) > 0 Then
            IsSelected = True
        End If

        If IsSelected = True Then
            Me.btn_Create.Enabled = True
            objSPAHelper.Reinitialize()
            Dim SelectedParticipantID As String = cmb_ParticipantID.SelectedItem.ToString()
            Dim SelectedChargeType As EnumChargeType = If(Me.RadioButtn_Energy.Checked = True, EnumChargeType.E, EnumChargeType.EV)

            Dim WESMBillSummaryList As List(Of WESMBillSummary) = Me.objSPAHelper.GetWESMBillSummaryByParticipantID(SelectedParticipantID, SelectedChargeType)

            Me.dgv_SPA_Mgt.Rows.Clear()

            For Each Item In WESMBillSummaryList
                Dim NewData As String() = New String() {False, Item.WESMBillSummaryNo.ToString, Item.BillPeriod.ToString, Item.INVDMCMNo.ToString, Item.ChargeType.ToString, Item.NewDueDate.ToShortDateString, FormatNumber(Item.EndingBalance, UseParensForNegativeNumbers:=TriState.True).ToString}
                With Me.dgv_SPA_Mgt
                    .Rows.Add(NewData)
                End With
            Next

            Me.txtbox_SPARate.Select()
        End If
    End Sub

    Private Sub btn_Create_Click(sender As Object, e As EventArgs) Handles btn_Create.Click        
        Try
            If cmb_ParticipantID.Text Is Nothing Then
                MessageBox.Show("Please select participant.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If txtbox_SPARate Is Nothing Then
                MessageBox.Show("Please inpute SPA interest rate.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim ListOfWESMBillSummaryNo As New List(Of WESMBillSummary)
            Dim ParticipantID As String = cmb_ParticipantID.SelectedItem.ToString()
            Dim DateFrom As Date = Me.dtp_DateFrom.Value.ToShortDateString
            Dim inMonths As Integer = Me.txtbox_Months.Text
            Dim IntRate As Decimal = CDec(Me.txtbox_SPARate.Text / 100)
            Dim ChargeType As EnumChargeType = If(Me.RadioButtn_Energy.Checked = True, EnumChargeType.E, EnumChargeType.EV)
            Dim CountSelectedInvoices As Integer = 0
            If inMonths > 0 Then
                For i As Integer = 0 To Me.dgv_SPA_Mgt.RowCount - 1
                    With Me.dgv_SPA_Mgt
                        Dim RowItem As Long = CLng(.Rows(i).Cells(1).Value)
                        If .Rows(i).Cells(0).Value = True Then
                            ListOfWESMBillSummaryNo.Add(New WESMBillSummary(RowItem))
                            CountSelectedInvoices += 1
                        End If
                    End With
                Next
                If CountSelectedInvoices > 0 Then
                    objSPAHelper.Reinitialize()
                    objSPAHelper.CreateSPA(ParticipantID, DateFrom, inMonths, IntRate, ListOfWESMBillSummaryNo, ChargeType)
                    Dim objfrmSPAView As New frmSPAView
                    With objfrmSPAView
                        .objfrmSPAMgt = Me
                        .oSPAHelper = objSPAHelper
                        .Text = "Special Payment Agreement - Add"
                        .ShowDialog()
                    End With
                Else
                    MessageBox.Show("Please select invoices.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

            Else
                MessageBox.Show("Please enter Terms of Loan (in months).", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgv_SPA_Mgt_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgv_SPA_Mgt.CurrentCellDirtyStateChanged
        If Not Me.dgv_SPA_Mgt.IsCurrentCellDirty Then
            Exit Sub
        End If        
        Dim TotalSPAAmount As Decimal = 0        
        Me.txtbox_GrandTotal.Text = "0.00"
        Select Case Me.dgv_SPA_Mgt.CurrentCell.ColumnIndex()
            Case 0
                Me.dgv_SPA_Mgt.CommitEdit(DataGridViewDataErrorContexts.Commit)
                With Me.dgv_SPA_Mgt
                    For i As Integer = 0 To .Rows.Count - 1                        
                        If .Rows(i).Cells(0).Value = True Then
                            Dim SPAAmount As Decimal = CDec(.Rows(i).Cells(6).Value)                            
                            TotalSPAAmount += SPAAmount                                            
                        End If
                    Next
                End With                
                Me.txtbox_GrandTotal.Text = FormatNumber(Math.Round(TotalSPAAmount, 2), UseParensForNegativeNumbers:=TriState.True)
        End Select
    End Sub

    Private Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click        
        Try
            objSPAHelper.Reinitialize()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try        
    End Sub

    Private Sub txtbox_Months_TextChanged(sender As Object, e As EventArgs) Handles txtbox_Months.TextChanged
        If Me.txtbox_Months.Text.Trim.Length = 0 Then
            Me.txtbox_Months.Text = "0"
            Exit Sub
        ElseIf Not IsNumeric(Me.txtbox_Months.Text) Then
            MsgBox("Terms of loan must be numeric!", MsgBoxStyle.Critical, "Warning")
            Me.txtbox_Months.Text = "0"
            Exit Sub
        ElseIf CDec(Me.txtbox_SPARate.Text) < 0 Then
            MsgBox("Terms of loan must not be less than zero!", MsgBoxStyle.Critical, "Warning")
            Me.txtbox_Months.Text = "0"
            Exit Sub
        End If
    End Sub

    Private Sub RadioButtn_Energy_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtn_Energy.CheckedChanged
        If Me.cmb_ParticipantID.SelectedIndex = -1 Then
            Exit Sub
        End If

        If RadioButtn_Energy.Checked = True Then
            objSPAHelper.Reinitialize()
            Dim SelectedParticipantID As String = cmb_ParticipantID.SelectedItem.ToString()
            Dim SelectedChargeType As EnumChargeType = EnumChargeType.E

            Dim WESMBillSummaryList As List(Of WESMBillSummary) = Me.objSPAHelper.GetWESMBillSummaryByParticipantID(SelectedParticipantID, SelectedChargeType)

            Me.dgv_SPA_Mgt.Rows.Clear()

            For Each Item In WESMBillSummaryList
                Dim NewData As String() = New String() {False, Item.WESMBillSummaryNo.ToString, Item.BillPeriod.ToString, Item.INVDMCMNo.ToString, Item.ChargeType.ToString, Item.NewDueDate.ToShortDateString, FormatNumber(Item.EndingBalance, UseParensForNegativeNumbers:=TriState.True).ToString}
                With Me.dgv_SPA_Mgt
                    .Rows.Add(NewData)
                End With
            Next
        End If
    End Sub

    Private Sub RadioButtn_VAT_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtn_VAT.CheckedChanged
        If Me.cmb_ParticipantID.SelectedIndex = -1 Then
            Exit Sub
        End If

        If RadioButtn_VAT.Checked = True Then
            objSPAHelper.Reinitialize()
            Dim SelectedParticipantID As String = cmb_ParticipantID.SelectedItem.ToString()
            Dim SelectedChargeType As EnumChargeType = EnumChargeType.EV

            Dim WESMBillSummaryList As List(Of WESMBillSummary) = Me.objSPAHelper.GetWESMBillSummaryByParticipantID(SelectedParticipantID, SelectedChargeType)

            Me.dgv_SPA_Mgt.Rows.Clear()

            For Each Item In WESMBillSummaryList
                Dim NewData As String() = New String() {False, Item.WESMBillSummaryNo.ToString, Item.BillPeriod.ToString, Item.INVDMCMNo.ToString, Item.ChargeType.ToString, Item.NewDueDate.ToShortDateString, FormatNumber(Item.EndingBalance, UseParensForNegativeNumbers:=TriState.True).ToString}
                With Me.dgv_SPA_Mgt
                    .Rows.Add(NewData)
                End With
            Next
        End If
    End Sub

    Private Sub chkbox_SelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkbox_SelectAll.CheckedChanged
        If Me.chkbox_SelectAll.Checked = True Then
            If Me.dgv_SPA_Mgt.RowCount <> 0 Then
                For x = 0 To Me.dgv_SPA_Mgt.RowCount - 1
                    Me.dgv_SPA_Mgt.Rows(x).Cells("CB_Select").Value = True
                Next
            End If
        Else
            If Me.dgv_SPA_Mgt.RowCount <> 0 Then
                For x = 0 To Me.dgv_SPA_Mgt.RowCount - 1
                    Me.dgv_SPA_Mgt.Rows(x).Cells("CB_Select").Value = False
                Next
            End If
        End If
    End Sub
End Class