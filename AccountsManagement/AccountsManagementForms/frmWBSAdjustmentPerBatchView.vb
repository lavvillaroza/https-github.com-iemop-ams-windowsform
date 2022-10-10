Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports System.Threading
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmWBSAdjustmentPerBatchView
    Public mainForm As frmWBSAdjustmentPerBatchMain
    Public addForm As frmWBSAdjustmentPerBatchAdd
    Public WBSAdjOnWTaxHelper As WBSAdjOnWTAXHelper
    Private WBillHelper As WESMBillHelper
    Public isView As Boolean
    Private Sub frmWESMBillSummaryAdjustmentPerBatchView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WBillHelper = WESMBillHelper.GetInstance()
        Me.GetWBSAdjustmentList()
        If Me.isView = True Then
            Me.btn_Save.Enabled = False
        Else
            Me.btn_Save.Enabled = True
        End If
    End Sub
    Private Sub GetWBSAdjustmentList()
        Me.dgv_WESMInvoices_AR.Rows.Clear()
        Me.dgv_WESMInvoices_AP.Rows.Clear()
        Dim getTotalARAmountAdj As Decimal = 0D
        Dim getTotalAPAmountAdj As Decimal = 0D
        For Each item In Me.WBSAdjOnWTaxHelper.WBSAmountAdjustmentWhTax.WBSAmountAdjWHTAXDetailsList
            With item
                If item.AmountAdjusted <= 0 Then
                    Me.dgv_WESMInvoices_AR.Rows.Add(.WBSDetails.BillPeriod, .WBSDetails.WESMBillBatchNo, .WBSDetails.INVDMCMNo, .WBSDetails.DueDate.ToShortDateString, .WBSDetails.NewDueDate.ToShortDateString, FormatNumber(.WBSDetails.BeginningBalance, UseParensForNegativeNumbers:=TriState.True), FormatNumber(.WBSDetails.EndingBalance, UseParensForNegativeNumbers:=TriState.True), FormatNumber(.AmountAdjusted, UseParensForNegativeNumbers:=TriState.True), .FTFDMCMDoc)
                    getTotalARAmountAdj += .AmountAdjusted
                Else
                    Me.dgv_WESMInvoices_AP.Rows.Add(.WBSDetails.BillPeriod, .WBSDetails.WESMBillBatchNo, .WBSDetails.INVDMCMNo, .WBSDetails.DueDate.ToShortDateString, .WBSDetails.NewDueDate.ToShortDateString, FormatNumber(.WBSDetails.BeginningBalance, UseParensForNegativeNumbers:=TriState.True), FormatNumber(.WBSDetails.EndingBalance, UseParensForNegativeNumbers:=TriState.True), FormatNumber(.AmountAdjusted, UseParensForNegativeNumbers:=TriState.True), .FTFDMCMDoc)
                    getTotalAPAmountAdj += .AmountAdjusted
                End If
            End With
        Next
        Me.txtbox_TotalARAdj.Text = FormatNumber(getTotalARAmountAdj, UseParensForNegativeNumbers:=TriState.True)
        Me.txtbox_TotalAPAdj.Text = FormatNumber(getTotalAPAmountAdj, UseParensForNegativeNumbers:=TriState.True)
        Me.dgv_AdjARDummy.Rows.Clear()
        Me.dgv_AdjAPDummy.Rows.Clear()
        Dim getTotalARAmountAdj2 As Decimal = 0D
        Dim getTotalAPAmountAdj2 As Decimal = 0D
        For Each item In Me.WBSAdjOnWTaxHelper.WBSAmountAdjustmentWhTax.WBSDummiesList
            With item
                If .BeginningBalance <= 0 Then
                    Me.dgv_AdjARDummy.Rows.Add(.BillPeriod, .WESMBillBatchNo, .INVDMCMNo, .DueDate.ToShortDateString, .NewDueDate.ToShortDateString, FormatNumber(.BeginningBalance, UseParensForNegativeNumbers:=TriState.True))
                    getTotalARAmountAdj2 += .BeginningBalance
                Else
                    Me.dgv_AdjAPDummy.Rows.Add(.BillPeriod, .WESMBillBatchNo, .INVDMCMNo, .DueDate.ToShortDateString, .NewDueDate.ToShortDateString, FormatNumber(.BeginningBalance, UseParensForNegativeNumbers:=TriState.True))
                    getTotalAPAmountAdj2 += .BeginningBalance
                End If
            End With
        Next

        Me.txtbox_TotalARAmountAdj.Text = FormatNumber(getTotalARAmountAdj2, UseParensForNegativeNumbers:=TriState.True)
        Me.txtbox_TotalAPAmountAdj.Text = FormatNumber(getTotalAPAmountAdj2, UseParensForNegativeNumbers:=TriState.True)

    End Sub
    Private Sub TabCntrl_Main_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles TabCntrl_Main.DrawItem

        'Firstly we'll define some parameters.
        Dim CurrentTab As TabPage = TabCntrl_Main.TabPages(e.Index)
        Dim ItemRect As Rectangle = TabCntrl_Main.GetTabRect(e.Index)
        Dim FillBrush As New SolidBrush(Color.White)
        Dim TextBrush As New SolidBrush(System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer)))
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        'If we are currently painting the Selected TabItem we'll 
        'change the brush colors and inflate the rectangle.
        If CBool(e.State And DrawItemState.Selected) Then
            FillBrush.Color = Color.White
            TextBrush.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
            ItemRect.Inflate(2, 2)
        End If

        'Set up rotation for left and right aligned tabs
        If TabCntrl_Main.Alignment = TabAlignment.Left Or TabCntrl_Main.Alignment = TabAlignment.Right Then
            Dim RotateAngle As Single = 90
            If TabCntrl_Main.Alignment = TabAlignment.Left Then RotateAngle = 270
            Dim cp As New PointF(ItemRect.Left + (ItemRect.Width \ 2), ItemRect.Top + (ItemRect.Height \ 2))
            e.Graphics.TranslateTransform(cp.X, cp.Y)
            e.Graphics.RotateTransform(RotateAngle)
            ItemRect = New Rectangle(-(ItemRect.Height \ 2), -(ItemRect.Width \ 2), ItemRect.Height, ItemRect.Width)
        End If

        'Next we'll paint the TabItem with our Fill Brush
        e.Graphics.FillRectangle(FillBrush, ItemRect)

        'Now draw the text.
        e.Graphics.DrawString(CurrentTab.Text, e.Font, TextBrush, RectangleF.op_Implicit(ItemRect), sf)

        'Reset any Graphics rotation
        e.Graphics.ResetTransform()

        'Finally, we should Dispose of our brushes.
        FillBrush.Dispose()
        TextBrush.Dispose()

    End Sub

    Private Sub btn_Back_Click(sender As Object, e As EventArgs) Handles btn_Back.Click
        Me.Close()
    End Sub

    Private Sub btn_JVSetup_Click(sender As Object, e As EventArgs) Handles btn_JVSetup.Click
        Try
            Dim DS As New DataSet

            DS = WBSAdjOnWTaxHelper.GenerateJVReport()
            If DS.Tables.Count > 0 Then
                ProgressThread.Show("Please wait while preparing JV Payment Report.")
                Dim frmReport As New frmReportViewer
                With frmReport
                    .LoadJournalVoucher(DS)
                    ProgressThread.Close()
                    .ShowDialog()
                End With
            Else
                MessageBox.Show("No available JV Payment, no movement on settlement invoices.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_Save_Click(sender As Object, e As EventArgs) Handles btn_Save.Click
        Try
            Dim msgAns As New MsgBoxResult
            msgAns = MsgBox("Do you really want to save the Withholding Tax Adjustment?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "System Message")

            If msgAns = MsgBoxResult.Yes Then
                ProgressThread.Show("Please wait while SAVING.")
                WBSAdjOnWTaxHelper.Save()

                WBSAdjOnWTaxHelper.Dispose()
                WBSAdjOnWTaxHelper = Nothing
                WBSAdjOnWTaxHelper = New WBSAdjOnWTAXHelper

                ProgressThread.Close()
                MessageBox.Show("The processed data have been successfully saved.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Me.Close()
                Me.addForm.Close()
                Me.mainForm.fillupDataGridViewMain()
            End If
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Private Function GenerateFTFDS(ByVal FTF As FundTransferFormMain) As DataSet
        Dim DS As New DataSet
        Dim dtMain As New DSReport.FTFMainDataTable
        Dim dtParticipant As New DSReport.FTFParticipantDataTable
        Dim dtDetails As New DSReport.FTFDetailsDataTable
        Dim Signatory = WBillHelper.GetSignatories("FTF").First()
        With FTF
            Dim row = dtMain.NewRow()
            row("REF_NO") = .RefNo
            row("DR_DATE") = .DRDate.ToString("MMMM dd,yyyy")
            row("CR_DATE") = .CRDate.ToString("MMMM dd,yyyy")
            row("REMARKS") = "Transfer of Market Fees to Settlement from PEMC Account     P" & FormatNumber(.TotalAmount, 2)
            Select Case .TransType
                Case EnumFTFTransType.TransferMarketFeesToPEMC
                    row("REMARKS") = "Transfer of Market Fees to PEMC Account     P" & FormatNumber(.TotalAmount, 2)
                Case EnumFTFTransType.TransferMarketFeesToSTL
                    row("REMARKS") = "Transfer of Market Fees to Settlement from PEMC Account     P" & FormatNumber(.TotalAmount, 2)
            End Select

            row("TOTAL_AMOUNT") = .TotalAmount
            row("PREPARED_BY") = AMModule.FullName
            row("POSITION") = AMModule.Position
            row("REQUESTING_APPROVAL") = .RequestingApproval
            row("POSITION1") = Signatory.Position_1
            row("APPROVED_BY") = .ApprovedBy
            row("POSITION2") = Signatory.Position_2

            If .TransType = EnumFTFTransType.DrawDown Then
                row("NOTED_BY") = Signatory.Signatory_3
                row("POSITION3") = Signatory.Position_3
            End If
            dtMain.Rows.Add(row)
        End With
        dtMain.AcceptChanges()


        For Each item In FTF.ListOfFTFParticipants
            Dim row = dtParticipant.NewRow()

            With item
                row("REF_NO") = .RefNo
                row("ID_NUMBER") = .IDNumber.IDNumber
                row("PARTICPANT_ID") = .IDNumber.ParticipantID
                row("AMOUNT") = .Amount

                dtParticipant.Rows.Add(row)
            End With
        Next
        dtParticipant.AcceptChanges()


        For Each item In FTF.ListOfFTFDetails
            Dim row = dtDetails.NewRow()

            With item
                row("REF_NO") = .RefNo
                row("BANK_ACCT_NO") = .BankAccountNo
                row("AMOUNT") = .Debit - .Credit

                dtDetails.Rows.Add(row)
            End With
        Next
        dtDetails.AcceptChanges()


        With DS.Tables
            .Add(dtMain)
            .Add(dtParticipant)
            .Add(dtDetails)
        End With
        DS.AcceptChanges()

        Return DS
    End Function

    Private Sub dgv_WESMInvoices_AR_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_WESMInvoices_AR.CellContentClick
        Try
            If e.RowIndex = -1 Then
                Exit Sub
            End If
            Select Case e.ColumnIndex
                Case 8
                    If WBSAdjOnWTaxHelper.WBSAmountAdjustmentWhTax.ChargeType = EnumChargeType.E Then
                        Dim getCreatedDocu As String = Me.dgv_WESMInvoices_AR.Rows(e.RowIndex).Cells("col_ARCreatedDoc").Value
                        Dim dmcmNo As Long = CLng(getCreatedDocu.Replace(AMModule.DMCMNumberPrefix, ""))
                        Dim dt = WBSAdjOnWTaxHelper.GenerateDMCM(New DSReport.DebitCreditMemoDataTable, dmcmNo)
                        ProgressThread.Show("Please wait while generating DMCM Report.")
                        'Get the datasource for the report
                        Dim frmViewer As New frmReportViewer()
                        With frmViewer
                            .LoadDebitCreditMemo(dt)
                            ProgressThread.Close()
                            .ShowDialog()
                        End With
                    ElseIf WBSAdjOnWTaxHelper.WBSAmountAdjustmentWhTax.ChargeType = EnumChargeType.MF Then
                        Dim getCreatedDocu As String = Me.dgv_WESMInvoices_AR.Rows(e.RowIndex).Cells("col_ARCreatedDoc").Value
                        Dim refNo As Long = CLng(getCreatedDocu.Replace(AMModule.FTFNumberPrefix, ""))
                        Dim ds As New DataSet
                        Dim dtMain As New DSReport.FTFMainDataTable
                        Dim GetFTF = (From x In WBSAdjOnWTaxHelper.FundTransferform _
                                      Where x.RefNo = refNo _
                                      Select x).FirstOrDefault

                        If Not GetFTF Is Nothing Then
                            Dim DTSet As DataSet = Me.GenerateFTFDS(GetFTF)
                            Dim frmViewer As New frmReportViewer()
                            With frmViewer
                                .LoadFTF(DTSet)
                                .ShowDialog()
                            End With
                        Else
                            MessageBox.Show("No available FTF report based on selected transaction.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgv_WESMInvoices_AP_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_WESMInvoices_AP.CellContentClick
        Try
            If e.RowIndex = -1 Then
                Exit Sub
            End If
            Select Case e.ColumnIndex
                Case 8
                    If WBSAdjOnWTaxHelper.WBSAmountAdjustmentWhTax.ChargeType = EnumChargeType.E Then
                        Dim getCreatedDocu As String = Me.dgv_WESMInvoices_AP.Rows(e.RowIndex).Cells("col_APCreatedDoc").Value
                        Dim dmcmNo As Long = CLng(getCreatedDocu.Replace(AMModule.DMCMNumberPrefix, ""))
                        Dim dt = WBSAdjOnWTaxHelper.GenerateDMCM(New DSReport.DebitCreditMemoDataTable, dmcmNo)
                        ProgressThread.Show("Please wait while generating DMCM Report.")
                        'Get the datasource for the report
                        Dim frmViewer As New frmReportViewer()
                        With frmViewer
                            .LoadDebitCreditMemo(dt)
                            ProgressThread.Close()
                            .ShowDialog()
                        End With
                    ElseIf WBSAdjOnWTaxHelper.WBSAmountAdjustmentWhTax.ChargeType = EnumChargeType.MF Then
                        Dim getCreatedDocu As String = Me.dgv_WESMInvoices_AP.Rows(e.RowIndex).Cells("col_APCreatedDoc").Value
                        Dim refNo As Long = CLng(getCreatedDocu.Replace(AMModule.FTFNumberPrefix, ""))
                        Dim ds As New DataSet
                        Dim dtMain As New DSReport.FTFMainDataTable
                        Dim GetFTF = (From x In WBSAdjOnWTaxHelper.FundTransferform _
                                      Where x.RefNo = refNo _
                                      Select x).FirstOrDefault

                        If Not GetFTF Is Nothing Then
                            Dim DTSet As DataSet = Me.GenerateFTFDS(GetFTF)
                            Dim frmViewer As New frmReportViewer()
                            With frmViewer
                                .LoadFTF(DTSet)
                                .ShowDialog()
                            End With
                        Else
                            MessageBox.Show("No available FTF report based on selected transaction.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class