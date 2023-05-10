Imports System.Windows.Forms
Imports AccountsManagementObjects
Imports AccountsManagementLogic
Imports WESMLib.Auth.Lib

Public Class frmSPAView

    Public oSPAHelper As New SPAHelper
    Public isView As Boolean = False
    Public SPANumberSelected As Long
    Public objfrmSPAMgt As New frmSPAMgt
    Public DicUpdatedDueDate As New Dictionary(Of Date, List(Of Long))

    Private Sub frmSPAView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If isView = True Then
                ProgressThread.Show("Please wait while preparing.")
                oSPAHelper.InitializeViewing(SPANumberSelected)
                Me.btn_Save.Enabled = False
            Else
                Me.btn_Save.Enabled = True
            End If
            Dim TotalSPABalanceAR As Decimal = (From x In oSPAHelper.SPAWESMBillSummaryListAR
                                                    Where x.BalanceType = EnumBalanceType.AR
                                                    Select x.BeginningBalance).Sum()

            Dim TotalSPAPrincipalAR As Decimal = (From x In oSPAHelper.SPAWESMBillSummaryListAR Where x.BalanceType = EnumBalanceType.AR _
                                                 Join y In oSPAHelper.objSPAMain.SPAMonitoringList On y.WESMBillSummaryInfo.WESMBillSummaryNo Equals x.WESMBillSummaryNo _
                                                 Select y.PrincipalAmount).Sum()

            Dim TotalSPAInterestAR As Decimal = (From x In oSPAHelper.SPAWESMBillSummaryListAR Where x.BalanceType = EnumBalanceType.AR _
                                                 Join y In oSPAHelper.objSPAMain.SPAMonitoringList On y.WESMBillSummaryInfo.WESMBillSummaryNo Equals x.WESMBillSummaryNo _
                                                 Select y.InterestAmount).Sum()

            Me.GetDueDateList()
            Me.cmb_DueDateList.SelectedIndex = 0
            Me.txtbox_TotalPrincipal.Text = FormatNumber(TotalSPAPrincipalAR, UseParensForNegativeNumbers:=TriState.True).ToString
            Me.txtbox_TotalInterest.Text = FormatNumber(TotalSPAInterestAR, UseParensForNegativeNumbers:=TriState.True).ToString
            Me.txtbox_GrandTotal.Text = FormatNumber(TotalSPABalanceAR, UseParensForNegativeNumbers:=TriState.True).ToString
            Me.FillDGVonWESMAR()
            Me.FillDGVonWESMAP()
            gb_NDueDate.Enabled = False
            ProgressThread.Close()
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GetDueDateList()
        Dim GetDueDateDistinct As List(Of Date) = (From x In oSPAHelper.SPAWESMBillSummaryListAR Select x.DueDate Order By DueDate).Distinct.ToList()
        Me.cmb_DueDateList.Items.Clear()
        For Each Item In GetDueDateDistinct
            Me.cmb_DueDateList.Items.Add(Item.ToShortDateString)
        Next
    End Sub

    Private Sub FillDGVonWESMAR()
        Me.DGV_WESMINV_AR.DataSource = Nothing
        Me.DGV_WESMINV_AR.DataSource = oSPAHelper.AREWESMBillSummaryListDT
        Me.WESMDataGridViewFormat(Me.DGV_WESMINV_AR)
        Dim SubTotalAREnergy As Decimal = 0
        For Each row As DataRow In oSPAHelper.AREWESMBillSummaryListDT.Rows
            SubTotalAREnergy += CDec(row.Item("SPAAMount"))
        Next row

        Me.tb_GrandTotalARE.Text = FormatNumber(SubTotalAREnergy, UseParensForNegativeNumbers:=TriState.True).ToString
    End Sub

    Private Sub FillDGVonWESMAP()
        Me.DGV_WESMINV_AP.DataSource = Nothing
        Me.DGV_WESMINV_AP.DataSource = oSPAHelper.APEWESMBillSummaryListDT
        Me.WESMDataGridViewFormat(Me.DGV_WESMINV_AP)
        Dim SubTotalAPEnergy As Decimal = 0
        For Each row As DataRow In oSPAHelper.APEWESMBillSummaryListDT.Rows
            SubTotalAPEnergy += CDec(row.Item("SPAAMount"))
        Next
        Me.tb_GrandTotalAPE.Text = FormatNumber(SubTotalAPEnergy, UseParensForNegativeNumbers:=TriState.True).ToString
    End Sub

    Private Sub FillDGVonSPAAR()
        Dim ARSPABillSummaryDT As DataTable = oSPAHelper.CreateSPAARDataTable(Me.cmb_DueDateList.SelectedItem)
        Me.DGV_SPAINV_AR.DataSource = Nothing
        Me.DGV_SPAINV_AR.DataSource = ARSPABillSummaryDT
        Me.SPADataGridViewFormat(DGV_SPAINV_AR)
        Dim SubTotalBal As Decimal = 0
        Dim SubTotalPrin As Decimal = 0
        Dim SubTotalInt As Decimal = 0

        For Each item In (From x In ARSPABillSummaryDT.AsEnumerable Select x).ToList
            SubTotalBal += CDec(item("MonthlyPayment").ToString)
            SubTotalPrin += CDec(item("Principal").ToString)
            SubTotalInt += CDec(item("Interest").ToString)
        Next
        Me.txtbox_SubTotalPrinAR.Text = FormatNumber(SubTotalPrin, UseParensForNegativeNumbers:=TriState.True).ToString
        Me.txtbox_SubTotalIntAR.Text = FormatNumber(SubTotalInt, UseParensForNegativeNumbers:=TriState.True).ToString
        Me.txtbox_SubTotalBalAR.Text = FormatNumber(SubTotalBal, UseParensForNegativeNumbers:=TriState.True).ToString
    End Sub

    Private Sub FillDGVonSPAAP()
        Dim APSPABillSummaryDT As DataTable = oSPAHelper.CreateSPAAPDataTable(Me.cmb_DueDateList.SelectedItem)
        Me.DGV_SPAINV_AP.DataSource = Nothing
        Me.DGV_SPAINV_AP.DataSource = APSPABillSummaryDT
        Me.SPADataGridViewFormat(DGV_SPAINV_AP)
        Dim SubTotalBal As Decimal = 0
        Dim SubTotalPrin As Decimal = 0
        Dim SubTotalInt As Decimal = 0
        For Each item In (From x In APSPABillSummaryDT.AsEnumerable Select x).ToList
            SubTotalBal += CDec(item("MonthlyPayment"))
            SubTotalPrin += CDec(item("Principal").ToString)
            SubTotalInt += CDec(item("Interest").ToString)
        Next
        Me.txtbox_SubTotalPrinAP.Text = FormatNumber(SubTotalPrin, UseParensForNegativeNumbers:=TriState.True).ToString
        Me.txtbox_SubTotalIntAP.Text = FormatNumber(SubTotalInt, UseParensForNegativeNumbers:=TriState.True).ToString
        Me.txtbox_SubTotalBalAP.Text = FormatNumber(SubTotalBal, UseParensForNegativeNumbers:=TriState.True).ToString
    End Sub

    Private Sub cmb_DueDateList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_DueDateList.SelectedIndexChanged
        Me.FillDGVonSPAAR()
        Me.FillDGVonSPAAP()
    End Sub

    Private Sub btn_UpdateDueDate_Click(sender As Object, e As EventArgs) Handles btn_UpdateDueDate.Click
        TabCntrl_Main.Enabled = False
        gb_SelectionDueDate.Enabled = False
        gb_ProformaEntry.Enabled = False
        gb_ProformaEntry.Enabled = False
        gb_Buttons.Enabled = False
        gb_NDueDate.Enabled = True
        Me.DTP_UpdateDueDate.Value = New Date(CDate(Me.cmb_DueDateList.SelectedItem).Year, CDate(Me.cmb_DueDateList.SelectedItem).Month, CDate(Me.cmb_DueDateList.SelectedItem).Day)
    End Sub

    Private Sub btn_backCancel_Click(sender As Object, e As EventArgs) Handles btn_backCancel.Click
        TabCntrl_Main.Enabled = True
        gb_SelectionDueDate.Enabled = True
        gb_ProformaEntry.Enabled = True
        gb_ProformaEntry.Enabled = True
        gb_Buttons.Enabled = True
        gb_NDueDate.Enabled = False
    End Sub

    Private Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
        Me.Close()
    End Sub

    Private Sub btn_Update_Click(sender As Object, e As EventArgs) Handles btn_Update.Click
        If CDate(DTP_UpdateDueDate.Value) > CDate(cmb_DueDateList.SelectedItem) Then
            If CDate(DTP_UpdateDueDate.Value).Year > CDate(cmb_DueDateList.SelectedItem).Year Then
                MessageBox.Show("Please do not adjust the year of original due date.", "System Message - Invalid year selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            ElseIf CDate(DTP_UpdateDueDate.Value).Month > CDate(cmb_DueDateList.SelectedItem).Month Then
                MessageBox.Show("Please do not adjust the month of originaldue date.", "System Message - Invalid month selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        ElseIf CDate(DTP_UpdateDueDate.Value) <= CDate(cmb_DueDateList.SelectedItem) Then
            If CDate(DTP_UpdateDueDate.Value).Year < CDate(cmb_DueDateList.SelectedItem).Year Then
                MessageBox.Show("Please do not adjust the year of original due date.", "System Message - Invalid year selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            ElseIf CDate(DTP_UpdateDueDate.Value).Month < CDate(cmb_DueDateList.SelectedItem).Month Then
                MessageBox.Show("Please do not adjust the month of original due date.", "System Message - Invalid month selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        Try
            Dim GetSelectedIndex As Integer = cmb_DueDateList.SelectedIndex
            Dim SelectedDueDate As Date = CDate(DTP_UpdateDueDate.Value)
            Dim CurrentDueDate As Date = CDate(cmb_DueDateList.SelectedItem)

            Dim GetFirstPaymentOnSPA = (From x In oSPAHelper.SPAWESMBillSummaryListAR Select x.DueDate Order By DueDate).FirstOrDefault

            If oSPAHelper.objSPAMain.FirstPaymentDate = GetFirstPaymentOnSPA Then
                oSPAHelper.EditSPA(SelectedDueDate)
            End If

            Dim GetListforSelectedDate As List(Of WESMBillSummary) = (From x In oSPAHelper.SPAWESMBillSummaryListAR Where x.DueDate = CurrentDueDate Select x).Union _
                                                                     (From x In oSPAHelper.SPAWESMBillSummaryListAP Where x.DueDate = CurrentDueDate Select x).ToList
            Dim ctr As Long = 0
            For Each item In GetListforSelectedDate
                If item.BeginningBalance <> item.EndingBalance Then
                    ctr += 1
                End If
            Next

            If ctr > 0 Then
                MessageBox.Show("Updating duedate is not applicable!" & vbNewLine & "The selected duedate has been updated by collections and payments process.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            Else
                Dim ListOfWESMBillSummaryNo As New List(Of Long)
                For Each item In GetListforSelectedDate
                    ListOfWESMBillSummaryNo.Add(item.WESMBillSummaryNo)
                    item.NewDueDate = SelectedDueDate
                    item.DueDate = SelectedDueDate
                Next

                If Not DicUpdatedDueDate.ContainsKey(SelectedDueDate) Then
                    DicUpdatedDueDate.Add(SelectedDueDate, ListOfWESMBillSummaryNo)
                Else
                    DicUpdatedDueDate(SelectedDueDate) = ListOfWESMBillSummaryNo
                End If

                Me.GetDueDateList()
                Me.cmb_DueDateList.SelectedIndex = GetSelectedIndex

            End If

            TabCntrl_Main.Enabled = True
            gb_SelectionDueDate.Enabled = True
            gb_ProformaEntry.Enabled = True
            gb_ProformaEntry.Enabled = True
            gb_Buttons.Enabled = True
            gb_NDueDate.Enabled = False
            If isView = True Then
                Me.btn_Save.Enabled = True
            End If

            MessageBox.Show("The Selected duedate has been successfully updated.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "DataGridView Format WESMBillSummary"
    Private Sub WESMDataGridViewFormat(ByVal dgv As DataGridView)
        With dgv.Columns(5).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(6).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(7).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(9)
            With .DefaultCellStyle
                .Format = "N2"
                .Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            .Visible = False
        End With
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub
#End Region

#Region "DataGridview Format SPA"
    Private Sub SPADataGridViewFormat(ByVal dgv As DataGridView)
        With dgv.Columns(6).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        With dgv.Columns(7).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        With dgv.Columns(8).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        With dgv.Columns(10)
            With .DefaultCellStyle
                .Format = "N2"
                .Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            .Visible = False
        End With
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub
#End Region

    Private Sub DGV_WESMINV_AR_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_WESMINV_AR.CellDoubleClick
        If Not e.RowIndex = -1 Then
            If DGV_WESMINV_AR.RowCount > 0 Then
                If isView = True Then
                    Dim r As Integer = DGV_WESMINV_AR.CurrentRow.Index
                    Dim DMCMNo As Long = CLng(DGV_WESMINV_AR.Item(9, r).Value.ToString)
                    Dim IDnumber As String = CStr(DGV_WESMINV_AR.Item(1, r).Value.ToString)
                    'Get the datasource for the report
                    Dim frmViewer As New frmReportViewer()
                    Dim dt = oSPAHelper.GenerateDMCM(New DSReport.DebitCreditMemoDataTable, IDnumber, DMCMNo)
                    With frmViewer
                        .LoadDebitCreditMemo(dt)
                        .ShowDialog()
                    End With
                Else
                    Dim r As Integer = DGV_WESMINV_AR.CurrentRow.Index
                    Dim DMCMNo As Long = CLng(DGV_WESMINV_AR.Item(9, r).Value.ToString)
                    Dim IDnumber As String = CStr(DGV_WESMINV_AR.Item(1, r).Value.ToString)
                    'Get the datasource for the report
                    Dim frmViewer As New frmReportViewer()
                    Dim dt = oSPAHelper.GenerateDMCM(New DSReport.DebitCreditMemoDataTable, IDnumber, DMCMNo)
                    With frmViewer
                        .LoadDebitCreditMemoDraft(dt)
                        .ShowDialog()
                    End With
                End If

            End If
        End If
    End Sub


    Private Sub DGV_WESMINV_AP_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_WESMINV_AP.CellDoubleClick
        If Not e.RowIndex = -1 Then
            If DGV_WESMINV_AP.RowCount > 0 Then
                If isView = True Then
                    Dim r As Integer = DGV_WESMINV_AP.CurrentRow.Index
                    Dim DMCMNo As Long = CLng(DGV_WESMINV_AP.Item(9, r).Value.ToString)
                    Dim IDnumber As String = CStr(DGV_WESMINV_AP.Item(1, r).Value.ToString)
                    'Get the datasource for the report
                    Dim frmViewer As New frmReportViewer()
                    Dim dt = oSPAHelper.GenerateDMCM(New DSReport.DebitCreditMemoDataTable, IDnumber, DMCMNo)
                    With frmViewer
                        .LoadDebitCreditMemo(dt)
                        .ShowDialog()
                    End With
                Else
                    Dim r As Integer = DGV_WESMINV_AP.CurrentRow.Index
                    Dim DMCMNo As Long = CLng(DGV_WESMINV_AP.Item(9, r).Value.ToString)
                    Dim IDnumber As String = CStr(DGV_WESMINV_AP.Item(1, r).Value.ToString)
                    'Get the datasource for the report
                    Dim frmViewer As New frmReportViewer()
                    Dim dt = oSPAHelper.GenerateDMCM(New DSReport.DebitCreditMemoDataTable, IDnumber, DMCMNo)
                    With frmViewer
                        .LoadDebitCreditMemoDraft(dt)
                        .ShowDialog()
                    End With
                End If

            End If
        End If
    End Sub


    Private Sub DGV_SPAINV_AR_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_SPAINV_AR.CellDoubleClick
        If Not e.RowIndex = -1 Then
            If DGV_SPAINV_AR.RowCount > 0 Then
                If isView = True Then
                    Dim r As Integer = DGV_SPAINV_AR.CurrentRow.Index
                    Dim DMCMNo As Long = CLng(DGV_SPAINV_AR.Item(10, r).Value.ToString)
                    Dim IDnumber As String = CStr(DGV_SPAINV_AR.Item(1, r).Value.ToString)
                    'Get the datasource for the report
                    Dim frmViewer As New frmReportViewer()
                    Dim dt = oSPAHelper.GenerateDMCM(New DSReport.DebitCreditMemoDataTable, IDnumber, DMCMNo)
                    With frmViewer
                        .LoadDebitCreditMemo(dt)
                        .ShowDialog()
                    End With
                Else
                    Dim r As Integer = DGV_SPAINV_AP.CurrentRow.Index
                    Dim DMCMNo As Long = CLng(DGV_SPAINV_AP.Item(10, r).Value.ToString)
                    Dim IDnumber As String = CStr(DGV_SPAINV_AP.Item(1, r).Value.ToString)
                    'Get the datasource for the report
                    Dim frmViewer As New frmReportViewer()
                    Dim dt = oSPAHelper.GenerateDMCM(New DSReport.DebitCreditMemoDataTable, IDnumber, DMCMNo)
                    With frmViewer
                        .LoadDebitCreditMemoDraft(dt)
                        .ShowDialog()
                    End With
                End If

            End If
        End If
    End Sub


    Private Sub DGV_SPAINV_AP_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_SPAINV_AP.CellDoubleClick
        If Not e.RowIndex = -1 Then
            If DGV_SPAINV_AP.RowCount > 0 Then
                If isView = True Then
                    Dim r As Integer = DGV_SPAINV_AP.CurrentRow.Index
                    Dim DMCMNo As Long = CLng(DGV_SPAINV_AP.Item(10, r).Value.ToString)
                    Dim IDnumber As String = CStr(DGV_SPAINV_AP.Item(1, r).Value.ToString)
                    'Get the datasource for the report
                    Dim frmViewer As New frmReportViewer()
                    Dim dt = oSPAHelper.GenerateDMCM(New DSReport.DebitCreditMemoDataTable, IDnumber, DMCMNo)
                    With frmViewer
                        .LoadDebitCreditMemo(dt)
                        .ShowDialog()
                    End With
                Else
                    Dim r As Integer = DGV_SPAINV_AP.CurrentRow.Index
                    Dim DMCMNo As Long = CLng(DGV_SPAINV_AP.Item(10, r).Value.ToString)
                    Dim IDnumber As String = CStr(DGV_SPAINV_AP.Item(1, r).Value.ToString)
                    'Get the datasource for the report
                    Dim frmViewer As New frmReportViewer()
                    Dim dt = oSPAHelper.GenerateDMCM(New DSReport.DebitCreditMemoDataTable, IDnumber, DMCMNo)
                    With frmViewer
                        .LoadDebitCreditMemoDraft(dt)
                        .ShowDialog()
                    End With
                End If
            End If
        End If
    End Sub

    Private Sub btn_JVSPA_Click(sender As Object, e As EventArgs) Handles btn_JVClosing.Click
        If isView = True Then
            Try
                Dim DS As New DataSet
                DS = oSPAHelper.GenerateJVClosingReport(1)
                Dim frmReport As New frmReportViewer
                With frmReport
                    .LoadJournalVoucher(DS)
                    .ShowDialog()
                End With
            Catch ex As Exception
                MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            Try
                Dim DS As New DataSet
                DS = oSPAHelper.GenerateJVClosingReport(1)
                Dim frmReport As New frmReportViewer
                With frmReport
                    .LoadJournalVoucherDraft(DS)
                    .ShowDialog()
                End With
            Catch ex As Exception
                MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

    End Sub

    Private Sub btn_GenerateSummary_Click(sender As Object, e As EventArgs) Handles btn_GenerateSummary.Click
        Try
            Dim DS As New DataSet
            DS = oSPAHelper.GeneratePaymentScheduleSummary(New DSReport.SPAMainDataTable, New DSReport.SPAInvoicesDataTable)
            'Get the datasource for the report
            Dim frmViewer As New frmReportViewer()
            With frmViewer
                .LoadSPAPaymentScheduleSummary(DS)
                .ShowDialog()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_Save_Click(sender As Object, e As EventArgs) Handles btn_Save.Click
        Try
            If isView = True Then
                If DicUpdatedDueDate.Count > 0 Then
                    Dim msgAns As New MsgBoxResult
                    msgAns = MsgBox("Do you really want to save the Payment Allocations?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "System Message")
                    If msgAns = MsgBoxResult.Yes Then
                        ProgressThread.Show("Please wait while saving..")
                        oSPAHelper.SPASaveUpdatedDueDateToDB(DicUpdatedDueDate)
                        DicUpdatedDueDate.Clear()
                        DicUpdatedDueDate = New Dictionary(Of Date, List(Of Long))
                        Me.btn_Save.Enabled = False
                        ProgressThread.Close()
                        MessageBox.Show("Changes on due date has been successfully saved.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("No changes has been made.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            Else

                Dim msgAns As New MsgBoxResult
                msgAns = MsgBox("Do you really want to save the proccesed data?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "System Message")

                If msgAns = MsgBoxResult.Yes Then
                    ProgressThread.Show("Please wait while saving..")
                    oSPAHelper.SPASaveAddToDB()
                    oSPAHelper.Reinitialize()
                    ProgressThread.Close()
                    MessageBox.Show("The processed data have been successfully saved.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    Exit Sub
                End If
            End If
            Me.Close()
            Me.objfrmSPAMgt.Close()
        Catch ex As Exception
            ProgressThread.Close()            
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btn_JVSetup_Click(sender As Object, e As EventArgs) Handles btn_JVSetup.Click
        If isView = True Then
            Try
                Dim DS As New DataSet
                DS = oSPAHelper.GenerateJVClosingReport(2)
                Dim frmReport As New frmReportViewer
                With frmReport
                    .LoadJournalVoucher(DS)
                    .ShowDialog()
                End With
            Catch ex As Exception
                MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            Try
                Dim DS As New DataSet
                DS = oSPAHelper.GenerateJVClosingReport(2)
                Dim frmReport As New frmReportViewer
                With frmReport
                    .LoadJournalVoucherDraft(DS)
                    .ShowDialog()
                End With
            Catch ex As Exception
                MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
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

End Class