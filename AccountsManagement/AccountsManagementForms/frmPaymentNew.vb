Option Explicit On
Option Strict On

Imports System.IO
Imports System.Data
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports System.Threading.Tasks
Imports WESMLib.Auth.Lib
Imports System.Threading

Public Class frmPaymentNew
    
    Dim WBillHelper As WESMBillHelper
    Dim BFactory As BusinessFactory
    Dim SelectedAllocationDate As New AllocationDate
    Private PaymntHelper As New PaymentHelper
    Private cts As CancellationTokenSource

    Private Sub frmPaymentNew_Load(sender As Object, e As EventArgs) Handles Me.Load      
        Me.MdiParent = MainForm
        Try
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName
            BFactory = BusinessFactory.GetInstance()

            Me.LoadComboItems()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.PaymentAllocationWindow.ToString,
                                    ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
        End Try
    End Sub
    Private Sub UpdateProgress(_ProgressMsg As ProgressClass)
        ToolStripStatus_LabelMsg.Text = _ProgressMsg.ProgressMsg
        ctrl_statusStrip.Refresh()
    End Sub
    Public Sub LoadComboItems()
        Me.btn_Calculate.Enabled = False
        Me.GB_ProformaEntries.Enabled = False
        Me.GB_ProformaEntriesDetails.Enabled = False
        Me.GB_CntButtons.Enabled = False

        Me.cbo_CollectionAllocDate.Items.Clear() 'Clear Combobox
        Dim CollAllocDate As New List(Of AllocationDate)

        CollAllocDate = (From x In Me.WBillHelper.GetCollAllocDate() Select x Order By x.CollAllocationDate Descending).ToList

        If CollAllocDate.Count = 0 Then
            MessageBox.Show("No Allocation data was found.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        For Each DateItem In CollAllocDate
            Me.cbo_CollectionAllocDate.Items.Add(DateItem.CollAllocationDate.ToShortDateString)
        Next

    End Sub

    Private Async Sub btn_Calculate_Click(sender As Object, e As EventArgs) Handles btn_Calculate.Click
        If PaymntHelper.DailyInterestRate = 0 Then
            MsgBox("Cannot find Daily Interest rate for the allocation date " & FormatDateTime(CDate(cbo_CollectionAllocDate.SelectedItem.ToString), DateFormat.ShortDate) & ".", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, MsgBoxStyle), "No Interest Rate Found")
            Exit Sub
        End If

        Try
            Dim getTimeStart As New DateTime
            Dim getTimeEnd As New DateTime
            Dim progressIndicator As New Progress(Of ProgressClass)(AddressOf UpdateProgress)

            With frmPaymentRemittanceDate
                .AllocationDate = CDate(cbo_CollectionAllocDate.SelectedItem.ToString)
                .ShowDialog()
                If .iFSelected = True Then
                    PaymntHelper._PayAllocDate.RemittanceDate = .RemittanceDateSelected
                Else
                    MessageBox.Show("No selected remittance date!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End With

            cts = New CancellationTokenSource
            getTimeStart = WBillHelper.GetSystemDateTime

            ProgressThread.Show("Please wait while processing payment allocations.")

            'Allocation
            Await Task.Run(Sub() PaymntHelper.GetAPAllocations(progressIndicator))

            ProgressThread.Close()

            ProgressThread.Show("Please wait while processing payment offsetting.")
            'Offsetting        
            Await Task.Run(Sub() PaymntHelper.GetOffsetting(progressIndicator))

            ProgressThread.Close()
            ProgressThread.Show("Please wait while preparing the processed data for viewing.")

            Me.dgv_PaymentTransToPR.DataSource = PaymntHelper.PaymentTrasferToPRDT()
            Me.DataGridViewFormatTransferToPR(Me.dgv_PaymentTransToPR)
            Me.FormatTextBox()

            ProgressThread.Close()
            Me.btn_Calculate.Enabled = False
            Me.GB_CntButtons.Enabled = True
            Me.GB_ProformaEntries.Enabled = True
            Me.GB_ProformaEntriesDetails.Enabled = True

            getTimeEnd = WBillHelper.GetSystemDateTime

            If PaymntHelper.OffsettingIterationCount <> AMModule.OffsettingLimit And AMModule.OffsettingLimit <> 0 Then
                MessageBox.Show("The payment allocation was successfully run with " & PaymntHelper.OffsettingIterationCount & " offsetting iterations and does not exceed to " & AMModule.OffsettingLimit & " offsetting iterations limit." & vbNewLine _
                                & "Date Time Start: " & getTimeStart.ToString("MM/dd/yyyy hh:mm") & vbNewLine _
                                & "Date Time End: " & getTimeEnd.ToString("MM/dd/yyyy hh:mm"), "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf AMModule.OffsettingLimit = 0 Then
                MessageBox.Show("The payment allocation was successfully run with " & PaymntHelper.OffsettingIterationCount & " offsetting iterations and without the offsetting iterations limit." & vbNewLine _
                                & "Date Time Start: " & getTimeStart.ToString("MM/dd/yyyy hh:mm") & vbNewLine _
                                & "Date Time End: " & getTimeEnd.ToString("MM/dd/yyyy hh:mm"), "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("The payment allocation was successfully run with " & AMModule.OffsettingLimit & " offsetting iterations limit." & vbNewLine _
                                & "Date Time Start: " & getTimeStart.ToString("MM/dd/yyyy hh:mm") & vbNewLine _
                                & "Date Time End: " & getTimeEnd.ToString("MM/dd/yyyy hh:mm"), "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            ToolStripStatus_LabelMsg.Text = "Ready..."
            ctrl_statusStrip.Refresh()
            cts = Nothing
        Catch ex As Exception
            cts = Nothing
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub cbo_CollectionAllocDate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_CollectionAllocDate.SelectedIndexChanged
        Try
            Me.ClearControlls()

            SelectedAllocationDate = New AllocationDate(CDate(FormatDateTime(CDate(cbo_CollectionAllocDate.SelectedItem), DateFormat.ShortDate)), 0)
            Dim _Colls As New List(Of PaymentNew)
            Dim _ARAlloc As New List(Of DataTable)
            Dim _PaymentProformaEntries As New PaymentProformaEntries

            ProgressThread.Show("Please wait while preparing collections for viewing.")
            btn_Calculate.Enabled = True

            If Me.PaymntHelper IsNot Nothing Then
                Me.PaymntHelper = Nothing
            End If

            Me.PaymntHelper = New PaymentHelper 'PaymentHelper.GetInstance()
            Me.PaymntHelper.InitializeObject(SelectedAllocationDate)
            Await Task.Run(Sub() PaymntHelper.GetARCollections())

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ProgressThread.Close()
        End Try
    End Sub

    Private Sub ClearControlls()
        'Me.dgv_EnergyAR.DataSource = Nothing
        'Me.dgv_VATAR.DataSource = Nothing
        'Me.dgv_MFAR.DataSource = Nothing
        'Me.dgv_EnergyAP.DataSource = Nothing
        'Me.dgv_VATAP.DataSource = Nothing
        'Me.dgv_MFAP.DataSource = Nothing

        'Me.dgv_OffsetEnergyAR.DataSource = Nothing
        'Me.dgv_OffsetEnergyAP.DataSource = Nothing
        'Me.dgv_OffsetMFAR.DataSource = Nothing
        'Me.dgv_OffsetMFAP.DataSource = Nothing
        'Me.dgv_OffsetVATAR.DataSource = Nothing
        'Me.dgv_OffsetVATAP.DataSource = Nothing



        'Me.dgv_EnergyAR.Rows.Clear()
        'Me.dgv_VATAR.Rows.Clear()
        'Me.dgv_MFAR.Rows.Clear()
        'Me.dgv_EnergyAP.Rows.Clear()
        'Me.dgv_VATAP.Rows.Clear()
        'Me.dgv_MFAP.Rows.Clear()

        'Me.dgv_OffsetEnergyAR.Rows.Clear()
        'Me.dgv_OffsetEnergyAP.Rows.Clear()
        'Me.dgv_OffsetMFAR.Rows.Clear()
        'Me.dgv_OffsetMFAP.Rows.Clear()
        'Me.dgv_OffsetVATAR.Rows.Clear()
        'Me.dgv_OffsetVATAP.Rows.Clear()

        Me.dgv_PaymentTransToPR.DataSource = Nothing
        Me.dgv_PaymentTransToPR.Rows.Clear()

        Dim allTxt As New List(Of Control)
        For Each txt As TextBox In FindControlRecursive(allTxt, Me, GetType(TextBox))
            txt.Text = ""
        Next
    End Sub

    Public Shared Function FindControlRecursive(ByVal list As List(Of Control), ByVal parent As Control, ByVal ctrlType As System.Type) As List(Of Control)
        If parent Is Nothing Then Return list
        If parent.GetType Is ctrlType Then
            list.Add(parent)
        End If
        For Each child As Control In parent.Controls
            FindControlRecursive(list, child, ctrlType)
        Next
        Return list
    End Function

#Region "DataGridView Format for TransferToPR"
    Private Sub DataGridViewFormatTransferToPR(ByVal dgv As DataGridView)
        For i As Int32 = 0 To dgv.ColumnCount - 1
            Select Case i
                Case 11
                    dgv.Columns(i).ReadOnly = False
                Case 12
                    dgv.Columns(i).ReadOnly = False
                Case 13
                    dgv.Columns(i).ReadOnly = False
                Case 14
                    dgv.Columns(i).ReadOnly = False
                Case Else
                    dgv.Columns(i).ReadOnly = True
            End Select
        Next
    End Sub
#End Region

#Region "DataGridView Format For Energy"
    Private Sub DataGridViewFormatForEnergy(ByVal dgv As DataGridView)

        With dgv.Columns(6).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(7).DefaultCellStyle
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(9).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(10).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(11).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(12).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(13).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With


        'If dgv.Columns.Count > 14 Then
        '    With dgv.Columns(14)
        '        .Visible = False
        '    End With
        'End If

        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub
#End Region

#Region "DataGridView Format For Offset Energy"
    Private Sub DataGridViewFormatForOffsetEnergy(ByVal dgv As DataGridView)

        With dgv.Columns(6).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(7).DefaultCellStyle
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(9).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(10).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(11).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        If dgv.Columns.Count > 12 Then
            With dgv.Columns(12)
                .Visible = False
            End With
        End If
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub
#End Region

#Region "DataGridView Format For VAT"
    Private Sub DataGridViewFormatForVAT(ByVal dgv As DataGridView)

        With dgv.Columns(5).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(8).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(9).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        If dgv.Columns.Count > 10 Then
            With dgv.Columns(10)
                .Visible = False
            End With
        End If

        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub
#End Region

#Region "DataGridView Format For MF"
    Private Sub DataGridViewFormatForMF(ByVal dgv As DataGridView)

        With dgv.Columns(6).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(7).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(9).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(10).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(11).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(12).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(13).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(14).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(15).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(16).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(17).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(18).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        If dgv.Columns.Count > 19 Then
            With dgv.Columns(19)
                .Visible = False
            End With
        End If

        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub
#End Region

#Region "Input Total AR on Energy to Textbox"
    Private Sub CalculateTotalARonEnergy(ByVal Collections As List(Of ARCollection))

        'Get Total AR Collections on Energy
        Dim TotalARCollonEnergy = (From x In Collections Where x.CollectionType = EnumCollectionType.Energy And x.CollectionCategory = EnumCollectionCategory.Cash
                                   Select x.AllocationAmount).ToList()

        'Get AR Collections on Energy Per Billing Period
        Dim ARCollOnEnergyPerBP = (From x In Collections Where x.CollectionType = EnumCollectionType.Energy And x.CollectionCategory = EnumCollectionCategory.Cash
                                   Select x).ToList()

        'Get Total AR Collections on Default Interest of Energy
        Dim TotalARCollonEnergyDI = (From x In Collections Where x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy And x.CollectionCategory = EnumCollectionCategory.Cash
                                     Select x.AllocationAmount).ToList()

        'Get AR Collections on Default Interest of Energy Per Billing Period
        Dim ARCollOnEnergyDIPerBP = (From x In Collections Where x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy And x.CollectionCategory = EnumCollectionCategory.Cash
                                     Select x).ToList()

        'Get Total AR Draw Down on Energy
        Dim TotalARCollDDonEnergy = (From x In Collections Where x.CollectionType = EnumCollectionType.Energy And x.CollectionCategory = EnumCollectionCategory.Drawdown
                                     Select x.AllocationAmount).ToList()

        'Get Total AR Draw Down on Energy Per Billing Period
        Dim ARCollDDonEnergy = (From x In Collections Where x.CollectionType = EnumCollectionType.Energy And x.CollectionCategory = EnumCollectionCategory.Drawdown
                                     Select x).ToList()

        'Get Total AR Draw Down on Default Interest of Energy
        Dim TotalARCollDDonEnergyDI = (From x In Collections Where x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy And x.CollectionCategory = EnumCollectionCategory.Drawdown
                                     Select x.AllocationAmount).ToList()

        'Get AR Draw Down on Default Interest of Energy Per Billing Period
        Dim ARCollDDonEnergyDI = (From x In Collections Where x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy And x.CollectionCategory = EnumCollectionCategory.Drawdown
                                     Select x).ToList()


        'Me.Txtbox_TotalCollOnEnergy.Text = FormatNumber(TotalARCollonEnergy.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_TotalCollOnEnergyDI.Text = FormatNumber(TotalARCollonEnergyDI.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_TotalCollDDOnEnergy.Text = FormatNumber(TotalARCollDDonEnergy.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_TotalCollDDOnEnergyDI.Text = FormatNumber(TotalARCollDDonEnergyDI.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_ARGrandTotal.Text = FormatNumber(TotalARCollonEnergy.Sum() + TotalARCollonEnergyDI.Sum() + TotalARCollDDonEnergy.Sum() + TotalARCollDDonEnergyDI.Sum(), 2, , TriState.True).ToString()

    End Sub
#End Region

#Region "Input Total AR on VAT to Textbox"
    Private Sub CalculateTotalARonVAT(ByVal Collections As List(Of ARCollection))

        'Dim TotalARCollonEnergyVAT = (From x In Collections Where x.CollectionType = EnumCollectionType.VatOnEnergy And x.CollectionCategory = EnumCollectionCategory.Cash
        '                           Select x.AllocationAmount).ToList()

        'Me.Txtbox_GrandTotalofAREnergyVAT.Text = FormatNumber(TotalARCollonEnergyVAT.Sum(), 2, , TriState.True).ToString()
    End Sub
#End Region

#Region "Input Total AR on MF to Textbox"
    Private Sub CalculateTotalARonMF(ByVal Collections As List(Of ARCollection))

        'Dim TotalARCollonMF = (From x In Collections Where x.CollectionType = EnumCollectionType.MarketFees And x.CollectionCategory = EnumCollectionCategory.Cash
        '                           Select x.AllocationAmount).ToList()
        'Dim TotalARCollonDIMF = (From x In Collections Where x.CollectionType = EnumCollectionType.DefaultInterestOnMF And x.CollectionCategory = EnumCollectionCategory.Cash
        '                             Select x.AllocationAmount).ToList()
        'Dim TotalARCollonWHTAXMF = (From x In Collections Where x.CollectionType = EnumCollectionType.WithholdingTaxOnMF And x.CollectionCategory = EnumCollectionCategory.Cash
        '                             Select x.AllocationAmount).ToList()
        'Dim TotalARCollonWHTAXDIMF = (From x In Collections Where x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest And x.CollectionCategory = EnumCollectionCategory.Cash
        '                             Select x.AllocationAmount).ToList()

        'Dim TotalARCollonMFV = (From x In Collections Where x.CollectionType = EnumCollectionType.VatOnMarketFees And x.CollectionCategory = EnumCollectionCategory.Cash
        '                             Select x.AllocationAmount).ToList()

        'Dim TotalARCollonDIMFV = (From x In Collections Where x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF And x.CollectionCategory = EnumCollectionCategory.Cash
        '                             Select x.AllocationAmount).ToList()

        'Dim TotalARCollonWHVATMF = (From x In Collections Where x.CollectionType = EnumCollectionType.WithholdingVatOnMF And x.CollectionCategory = EnumCollectionCategory.Cash
        '                             Select x.AllocationAmount).ToList()
        'Dim TotalARCollonWHVATDIMF = (From x In Collections Where x.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest And x.CollectionCategory = EnumCollectionCategory.Cash
        '                             Select x.AllocationAmount).ToList()


        'Me.Txtbox_TotalCollOnMF.Text = FormatNumber(TotalARCollonMF.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_TotalCollOnDIMF.Text = FormatNumber(TotalARCollonDIMF.Sum(), 2, , TriState.True).ToString()

        'Me.Txtbox_TotalCollOnWHTAXMF.Text = FormatNumber(TotalARCollonWHTAXMF.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_TotalCollOnWHTAXDIMF.Text = FormatNumber(TotalARCollonWHTAXDIMF.Sum(), 2, , TriState.True).ToString()


        'Me.Txtbox_TotalCollOnMFVAT.Text = FormatNumber(TotalARCollonMFV.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_TotalCollOnDIMFVAT.Text = FormatNumber(TotalARCollonDIMFV.Sum(), 2, , TriState.True).ToString()

        'Me.Txtbox_TotalCollOnWHVATMFVAT.Text = FormatNumber(TotalARCollonWHVATMF.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_TotalCollOnWHVATDIMFVAT.Text = FormatNumber(TotalARCollonWHVATDIMF.Sum(), 2, , TriState.True).ToString()


        'Me.Txtbox_GrandTotalMF.Text = FormatNumber(TotalARCollonMF.Sum() + TotalARCollonDIMF.Sum() + _
        '                                           TotalARCollonMFV.Sum() + TotalARCollonDIMFV.Sum(), 2, , TriState.True).ToString()

    End Sub
#End Region

#Region "Input Total AP on Energy to Textbox"
    Private Sub CalculateTotalAPonEnergy(ByVal Payments As List(Of APAllocation))

        'Get Total AP Collections on Energy
        Dim TotalAPonEnergy = (From x In Payments Where x.PaymentType = EnumPaymentNewType.Energy And x.PaymentCategory = EnumCollectionCategory.Cash And x.ChargeType = EnumChargeType.E
                                   Select x.AllocationAmount Order By AllocationAmount).ToList()


        'Get AR Collections on Energy Per Billing Period
        Dim APOnEnergyPerBP = (From x In Payments Where x.PaymentType = EnumPaymentNewType.Energy And x.PaymentCategory = EnumCollectionCategory.Cash
                                   Select x).ToList()

        'Get Total AR Collections on Default Interest of Energy
        Dim TotalAPonEnergyDI = (From x In Payments Where x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy And x.PaymentCategory = EnumCollectionCategory.Cash
                                     Select x.AllocationAmount).ToList()

        'Get AR Collections on Default Interest of Energy Per Billing Period
        Dim APOnEnergyDIPerBP = (From x In Payments Where x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy And x.PaymentCategory = EnumCollectionCategory.Cash
                                     Select x).ToList()

        'Get Total AR Draw Down on Energy
        Dim TotalAPDDonEnergy = (From x In Payments Where x.PaymentType = EnumPaymentNewType.Energy And x.PaymentCategory = EnumCollectionCategory.Drawdown
                                     Select x.AllocationAmount).ToList()

        'Get Total AR Draw Down on Energy Per Billing Period
        Dim APDDonEnergy = (From x In Payments Where x.PaymentType = EnumPaymentNewType.Energy And x.PaymentCategory = EnumCollectionCategory.Drawdown
                                     Select x).ToList()

        'Get Total AR Draw Down on Default Interest of Energy
        Dim TotalAPDDonEnergyDI = (From x In Payments Where x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy And x.PaymentCategory = EnumCollectionCategory.Drawdown
                                     Select x.AllocationAmount).ToList()

        'Get AR Draw Down on Default Interest of Energy Per Billing Period
        Dim APDDonEnergyDI = (From x In Payments Where x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy And x.PaymentCategory = EnumCollectionCategory.Drawdown
                                     Select x).ToList()


        'Me.Txtbox_TotalPayOnEnergy.Text = FormatNumber(TotalAPonEnergy.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_TotalPayOnEnergyDI.Text = FormatNumber(TotalAPonEnergyDI.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_TotalPayDDOnEnergy.Text = FormatNumber(TotalAPDDonEnergy.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_TotalPayDDOnEnergyDI.Text = FormatNumber(TotalAPDDonEnergyDI.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_APGrandTotal.Text = FormatNumber(TotalAPonEnergy.Sum() + TotalAPonEnergyDI.Sum() + TotalAPDDonEnergy.Sum() + TotalAPDDonEnergyDI.Sum(), 2, , TriState.True).ToString()

    End Sub
#End Region

#Region "Input Total AP on VAT to Textbox"
    Private Sub CalculateTotalAPonVAT(ByVal Payments As List(Of APAllocation))

        'Dim TotalAPonEnergyVAT = (From x In Payments Where x.PaymentType = EnumPaymentNewType.VatOnEnergy And x.PaymentCategory = EnumCollectionCategory.Cash
        '                           Select x.AllocationAmount).ToList()

        'Me.Txtbox_GrandTotalofAPEnergyVAT.Text = FormatNumber(TotalAPonEnergyVAT.Sum(), 2, , TriState.True).ToString()
    End Sub
#End Region

#Region "Input Total AR on Offsetting MF to Textbox"
    Private Sub CalculateTotalOffsetARonMF(ByVal Offsettings As List(Of ARCollection))

        'Dim TotalARCollonMF = (From x In Offsettings Where x.CollectionType = EnumCollectionType.MarketFees And x.CollectionCategory = EnumCollectionCategory.Offset
        '                           Select x.AllocationAmount).ToList()
        'Dim TotalARCollonDIMF = (From x In Offsettings Where x.CollectionType = EnumCollectionType.DefaultInterestOnMF And x.CollectionCategory = EnumCollectionCategory.Offset
        '                             Select x.AllocationAmount).ToList()
        'Dim TotalARCollonWHTAXMF = (From x In Offsettings Where x.CollectionType = EnumCollectionType.WithholdingTaxOnMF And x.CollectionCategory = EnumCollectionCategory.Offset
        '                             Select x.AllocationAmount).ToList()
        'Dim TotalARCollonWHTAXDIMF = (From x In Offsettings Where x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest And x.CollectionCategory = EnumCollectionCategory.Offset
        '                             Select x.AllocationAmount).ToList()

        'Dim TotalARCollonMFV = (From x In Offsettings Where x.CollectionType = EnumCollectionType.VatOnMarketFees And x.CollectionCategory = EnumCollectionCategory.Offset
        '                             Select x.AllocationAmount).ToList()

        'Dim TotalARCollonDIMFV = (From x In Offsettings Where x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF And x.CollectionCategory = EnumCollectionCategory.Offset
        '                             Select x.AllocationAmount).ToList()

        'Dim TotalARCollonWHVATMF = (From x In Offsettings Where x.CollectionType = EnumCollectionType.WithholdingVatOnMF And x.CollectionCategory = EnumCollectionCategory.Offset
        '                             Select x.AllocationAmount).ToList()
        'Dim TotalARCollonWHVATDIMF = (From x In Offsettings Where x.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest And x.CollectionCategory = EnumCollectionCategory.Offset
        '                             Select x.AllocationAmount).ToList()


        'Me.Txtbox_TotalOffsetOnMF.Text = FormatNumber(TotalARCollonMF.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_TotalOffsetOnDIMF.Text = FormatNumber(TotalARCollonDIMF.Sum(), 2, , TriState.True).ToString()

        'Me.Txtbox_TotalOffsetOnWHTAXMF.Text = FormatNumber(TotalARCollonWHTAXMF.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_TotalOffsetOnWHTAXDIMF.Text = FormatNumber(TotalARCollonWHTAXDIMF.Sum(), 2, , TriState.True).ToString()


        'Me.Txtbox_TotalOffsetOnMFVAT.Text = FormatNumber(TotalARCollonMFV.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_TotalOffsetOnDIMFVAT.Text = FormatNumber(TotalARCollonDIMFV.Sum(), 2, , TriState.True).ToString()

        'Me.Txtbox_TotalOffsetOnWHVATMFVAT.Text = FormatNumber(TotalARCollonWHVATMF.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_TotalOffsetOnWHVATDIMFVAT.Text = FormatNumber(TotalARCollonWHVATDIMF.Sum(), 2, , TriState.True).ToString()


        'Me.Txtbox_GrandTotalOffsetMF.Text = FormatNumber(TotalARCollonMF.Sum() + TotalARCollonDIMF.Sum() + _
        '                                           TotalARCollonMFV.Sum() + TotalARCollonDIMFV.Sum(), 2, , TriState.True).ToString()

    End Sub
#End Region

#Region "Input Total AP on Offsetting MF to Textbox"
    Private Sub CalculateTotalOffsetAPonMF(ByVal Offsettings As List(Of APAllocation))

        'Dim TotalAPCollonMF = (From x In Offsettings Where x.PaymentType = EnumCollectionType.MarketFees And x.PaymentCategory = EnumCollectionCategory.Offset
        '                           Select x.AllocationAmount).ToList()
        'Dim TotalAPCollonDIMF = (From x In Offsettings Where x.PaymentType = EnumCollectionType.DefaultInterestOnMF And x.PaymentCategory = EnumCollectionCategory.Offset
        '                             Select x.AllocationAmount).ToList()
        'Dim TotalAPCollonWHTAXMF = (From x In Offsettings Where x.PaymentType = EnumCollectionType.WithholdingTaxOnMF And x.PaymentCategory = EnumCollectionCategory.Offset
        '                             Select x.AllocationAmount).ToList()
        'Dim TotalAPCollonWHTAXDIMF = (From x In Offsettings Where x.PaymentType = EnumCollectionType.WithholdingTaxOnDefaultInterest And x.PaymentCategory = EnumCollectionCategory.Offset
        '                             Select x.AllocationAmount).ToList()

        'Dim TotalAPCollonMFV = (From x In Offsettings Where x.PaymentType = EnumCollectionType.VatOnMarketFees And x.PaymentCategory = EnumCollectionCategory.Offset
        '                             Select x.AllocationAmount).ToList()

        'Dim TotalAPCollonDIMFV = (From x In Offsettings Where x.PaymentType = EnumCollectionType.DefaultInterestOnMF And x.PaymentCategory = EnumCollectionCategory.Offset
        '                             Select x.AllocationAmount).ToList()

        'Dim TotalAPCollonWHVATMF = (From x In Offsettings Where x.PaymentType = EnumCollectionType.WithholdingVatOnMF And x.PaymentCategory = EnumCollectionCategory.Offset
        '                             Select x.AllocationAmount).ToList()
        'Dim TotalAPCollonWHVATDIMF = (From x In Offsettings Where x.PaymentType = EnumCollectionType.WithholdingVatOnDefaultInterest And x.PaymentCategory = EnumCollectionCategory.Offset
        '                             Select x.AllocationAmount).ToList()


        'Me.Txtbox_TotalOffsetOnMFAP.Text = FormatNumber(TotalAPCollonMF.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_TotalOffsetOnDIMFAP.Text = FormatNumber(TotalAPCollonDIMF.Sum(), 2, , TriState.True).ToString()

        'Me.Txtbox_TotalOffsetOnWHTAXMFAP.Text = FormatNumber(TotalAPCollonWHTAXMF.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_TotalOffsetOnWHTAXDIMFAP.Text = FormatNumber(TotalAPCollonWHTAXDIMF.Sum(), 2, , TriState.True).ToString()


        'Me.Txtbox_TotalOffsetOnMFVATAP.Text = FormatNumber(TotalAPCollonMFV.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_TotalOffsetOnDIMFVATAP.Text = FormatNumber(TotalAPCollonDIMFV.Sum(), 2, , TriState.True).ToString()

        'Me.Txtbox_TotalOffsetOnWHVATMFVATAP.Text = FormatNumber(TotalAPCollonWHVATMF.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_TotalOffsetOnWHVATDIMFVATAP.Text = FormatNumber(TotalAPCollonWHVATDIMF.Sum(), 2, , TriState.True).ToString()


        'Me.Txtbox_GrandTotalOffsetMFAP.Text = FormatNumber(TotalAPCollonMF.Sum() + TotalAPCollonDIMF.Sum() + _
        '                                           TotalAPCollonMFV.Sum() + TotalAPCollonDIMFV.Sum(), 2, , TriState.True).ToString()

    End Sub
#End Region

#Region "Input Total AR on Offsetting Energy to Textbox"
    Private Sub CalculateTotalOffsetARonEnergy(ByVal Collections As List(Of ARCollection))

        ''Get Total AR Offsetting on Energy
        'Dim TotalAROffsetonEnergy = (From x In Collections Where x.CollectionType = EnumCollectionType.Energy And x.CollectionCategory = EnumCollectionCategory.Offset
        '                           Select x.AllocationAmount).ToList()

        ''Get AR Offsetting on Energy Per Billing Period
        'Dim AROffsetOnEnergyPerBP = (From x In Collections Where x.CollectionType = EnumCollectionType.Energy And x.CollectionCategory = EnumCollectionCategory.Offset
        '                           Select x).ToList()

        ''Get Total AR Offsetting on Default Interest of Energy
        'Dim TotalAROffsetonEnergyDI = (From x In Collections Where x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy And x.CollectionCategory = EnumCollectionCategory.Offset
        '                             Select x.AllocationAmount).ToList()

        ''Get AR Offsetting on Default Interest of Energy Per Billing Period
        'Dim AROffsetOnEnergyDIPerBP = (From x In Collections Where x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy And x.CollectionCategory = EnumCollectionCategory.Offset
        '                             Select x).ToList()

        'Me.Txtbox_TotalOffsetOnEnergy.Text = FormatNumber(TotalAROffsetonEnergy.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_TotalOffsetOnEnergyDI.Text = FormatNumber(TotalAROffsetonEnergyDI.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_OffsetARGrandTotal.Text = FormatNumber(TotalAROffsetonEnergy.Sum() + TotalAROffsetonEnergyDI.Sum(), 2, , TriState.True).ToString()
    End Sub
#End Region

#Region "Input Total AP on Offsetting Energy to Textbox"
    Private Sub CalculateTotalOffsetAPonEnergy(ByVal Payments As List(Of APAllocation))

        ''Get Total AP Offsetting on Energy
        'Dim TotalAPOffsetonEnergy = (From x In Payments Where x.PaymentType = EnumPaymentNewType.Energy And x.PaymentCategory = EnumCollectionCategory.Offset
        '                           Select x.AllocationAmount).ToList()

        ''Get AP Offsetting on Energy Per Billing Period
        'Dim AROffsetOnEnergyPerBP = (From x In Payments Where x.PaymentType = EnumPaymentNewType.Energy And x.PaymentCategory = EnumCollectionCategory.Offset
        '                           Select x).ToList()

        ''Get Total AP Offsetting on Default Interest of Energy
        'Dim TotalAPOffsetonEnergyDI = (From x In Payments Where x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy And x.PaymentCategory = EnumCollectionCategory.Offset
        '                             Select x.AllocationAmount).ToList()

        ''Get AP Offsetting on Default Interest of Energy Per Billing Period
        'Dim APOffsetOnEnergyDIPerBP = (From x In Payments Where x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy And x.PaymentCategory = EnumCollectionCategory.Offset
        '                             Select x).ToList()

        'Me.Txtbox_TotalOffsetPayOnEnergy.Text = FormatNumber(TotalAPOffsetonEnergy.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_TotalOffsetPayOnEnergyDI.Text = FormatNumber(TotalAPOffsetonEnergyDI.Sum(), 2, , TriState.True).ToString()
        'Me.Txtbox_OffsetAPGrandTotal.Text = FormatNumber(TotalAPOffsetonEnergy.Sum() + TotalAPOffsetonEnergyDI.Sum(), 2, , TriState.True).ToString()
    End Sub
#End Region

#Region "Input Total AR on Offsetting VAT to Textbox"
    Private Sub CalculateTotalOffsetARonVAT(ByVal Collections As List(Of ARCollection))

        'Dim TotalAROffsetonEnergyVAT = (From x In Collections Where x.CollectionType = EnumCollectionType.VatOnEnergy And x.CollectionCategory = EnumCollectionCategory.Offset
        '                           Select x.AllocationAmount).ToList()

        'Me.Txtbox_GrandTotalofOffsetARVAT.Text = FormatNumber(TotalAROffsetonEnergyVAT.Sum(), 2, , TriState.True).ToString()
    End Sub
#End Region

#Region "Input Total AP on Offsetting VAT to Textbox"
    Private Sub CalculateTotalOffsetAPonVAT(ByVal Payments As List(Of APAllocation))

        'Dim TotalAPOffsetonEnergyVAT = (From x In Payments Where x.PaymentType = EnumPaymentNewType.VatOnEnergy And x.PaymentCategory = EnumCollectionCategory.Offset
        '                           Select x.AllocationAmount).ToList()

        'Me.Txtbox_GrandTotalofOffsetAPVAT.Text = FormatNumber(TotalAPOffsetonEnergyVAT.Sum(), 2, , TriState.True).ToString()
    End Sub
#End Region

#Region "Total AP on MF"
    Private Sub CalculateTotalAPonMF(ByVal MFAPAllocList As List(Of APAllocation))
        'Dim TotalAPAlloconMF = (From x In MFAPAllocList _
        '                        Where x.PaymentType = EnumPaymentNewType.MarketFees And x.PaymentCategory = EnumCollectionCategory.Cash _
        '                        Select x.AllocationAmount).ToList()

        'Dim TotalAPAlloconMFDI = (From x In MFAPAllocList _
        '                          Where x.PaymentType = EnumPaymentNewType.DefaultInterestOnMF And x.PaymentCategory = EnumCollectionCategory.Cash _
        '                          Select x.AllocationAmount).ToList()

        'Dim TotalAPAlloconMFWHTAX = (From x In MFAPAllocList Where x.PaymentType = EnumPaymentNewType.WithholdingTaxOnMF And x.PaymentCategory = EnumCollectionCategory.Cash
        '                             Select x.AllocationAmount).ToList()

        'Dim TotalAPAlloconMFDIWHTAX = (From x In MFAPAllocList Where x.PaymentType = EnumPaymentNewType.WithholdingTaxOnDefaultInterest And x.PaymentCategory = EnumCollectionCategory.Cash
        '                             Select x.AllocationAmount).ToList()

        'Dim TotalAPAlloconMFV = (From x In MFAPAllocList _
        '                        Where x.PaymentType = EnumPaymentNewType.VatOnMarketFees And x.PaymentCategory = EnumCollectionCategory.Cash _
        '                        Select x.AllocationAmount).ToList()

        'Dim TotalAPAlloconMFVDI = (From x In MFAPAllocList _
        '                        Where x.PaymentType = EnumPaymentNewType.DefaultInterestOnVatOnMF And x.PaymentCategory = EnumCollectionCategory.Cash _
        '                        Select x.AllocationAmount).ToList()

        'Dim TotalAPAlloconMFVWHVAT = (From x In MFAPAllocList _
        '                            Where x.PaymentType = EnumPaymentNewType.WithholdingVatOnMF And x.PaymentCategory = EnumCollectionCategory.Cash _
        '                            Select x.AllocationAmount).ToList()

        'Dim TotalAPAlloconMFVDIWHVAT = (From x In MFAPAllocList _
        '                           Where x.PaymentType = EnumPaymentNewType.WithholdingVatOnDefaultInterest And x.PaymentCategory = EnumCollectionCategory.Cash _
        '                           Select x.AllocationAmount).ToList()


        'Me.txtbox_MFAP.Text = FormatNumber(TotalAPAlloconMF.Sum(), 2, , TriState.True).ToString()
        'Me.txtbox_MFDI.Text = FormatNumber(TotalAPAlloconMFDI.Sum(), 2, , TriState.True).ToString()

        'Me.txtbox_MFWHTAX.Text = FormatNumber(TotalAPAlloconMFWHTAX.Sum(), 2, , TriState.True).ToString()
        'Me.txtbox_MFWHTAXDI.Text = FormatNumber(TotalAPAlloconMFDIWHTAX.Sum(), 2, , TriState.True).ToString()


        'Me.txtbox_MFVAP.Text = FormatNumber(TotalAPAlloconMFV.Sum(), 2, , TriState.True).ToString()
        'Me.txtbox_MFVDI.Text = FormatNumber(TotalAPAlloconMFVDI.Sum(), 2, , TriState.True).ToString()

        'Me.txtbox_MFWHVAT.Text = FormatNumber(TotalAPAlloconMFVWHVAT.Sum(), 2, , TriState.True).ToString()
        'Me.txtbox_MFWHVATDI.Text = FormatNumber(TotalAPAlloconMFVDIWHVAT.Sum(), 2, , TriState.True).ToString()


        'Me.txtbox_GrandTotalAP.Text = FormatNumber(TotalAPAlloconMF.Sum() + TotalAPAlloconMFDI.Sum() + _
        '                                           TotalAPAlloconMFV.Sum() + TotalAPAlloconMFVDI.Sum(), 2, , TriState.True).ToString()
    End Sub
#End Region

#Region "Input Total of Transfer To PR"
    Private Sub FormatTextBox()
        Dim subTotalExcessColl As Decimal = 0
        Dim subTotalDeferredEnergy As Decimal = 0
        Dim subTotalDeferredVATonEnergy As Decimal = 0
        Dim subTotalOffsetDeferredEnergy As Decimal = 0
        Dim subTotalOffsetDeferredVATonEnergy As Decimal = 0
        Dim subTotalMF As Decimal = 0
        Dim subTotalEnergy As Decimal = 0
        Dim subTotalVATOnEnergy As Decimal = 0
        Dim subTotalPR As Decimal = 0
        Dim SubTotalFinPen As Decimal = 0
        Dim subTotalRemittance As Decimal = 0
        Dim GrandTotal As Decimal = 0

        'Txtbox_TotalFinancialPenalty

        With Me.dgv_PaymentTransToPR
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight            
            .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For i As Integer = 0 To .Rows.Count - 1
                subTotalExcessColl += CDec(.Rows(i).Cells(2).Value)
                subTotalDeferredEnergy += CDec(.Rows(i).Cells(3).Value)
                subTotalDeferredVATonEnergy += CDec(.Rows(i).Cells(4).Value)
                subTotalOffsetDeferredEnergy += CDec(.Rows(i).Cells(5).Value)
                subTotalOffsetDeferredVATonEnergy += CDec(.Rows(i).Cells(6).Value)
                subTotalMF += CDec(.Rows(i).Cells(7).Value)
                subTotalEnergy += CDec(.Rows(i).Cells(8).Value)
                subTotalVATOnEnergy += CDec(.Rows(i).Cells(9).Value)
                subTotalPR += CDec(.Rows(i).Cells(12).Value)
                SubTotalFinPen += CDec(.Rows(i).Cells(14).Value)
                subTotalRemittance += CDec(.Rows(i).Cells(15).Value)
            Next
            '.AutoResizeColumns()
        End With
        Txtbox_TotalExcessCollection.Text = FormatNumber(subTotalExcessColl, 2, , TriState.True).ToString()
        Txtbox_TotalDeferredEnergy.Text = FormatNumber(subTotalDeferredEnergy, 2, , TriState.True).ToString()
        Txtbox_TotalDeferredVATonEnergy.Text = FormatNumber(subTotalDeferredVATonEnergy, 2, , TriState.True).ToString()
        Txtbox_TotalOffsetDeferredEnergy.Text = FormatNumber(subTotalOffsetDeferredEnergy, 2, , TriState.True).ToString()
        Txtbox_TotalOffsetDeferredVATonEnergy.Text = FormatNumber(subTotalOffsetDeferredVATonEnergy, 2, , TriState.True).ToString()
        Txtbox_TotalMF.Text = FormatNumber(subTotalMF, 2, , TriState.True).ToString()
        Txtbox_TotalEnergy.Text = FormatNumber(subTotalEnergy, 2, , TriState.True).ToString()
        Txtbox_TotalVAT.Text = FormatNumber(subTotalVATOnEnergy, 2, , TriState.True).ToString()
        Txtbox_TotalPRReplenishment.Text = FormatNumber(subTotalPR, 2, , TriState.True).ToString()
        Txtbox_TotalRemittance.Text = FormatNumber(subTotalRemittance, 2, , TriState.True).ToString()
        Txtbox_TotalFinancialPenalty.Text = FormatNumber(SubTotalFinPen, 2, , TriState.True).ToString()
        Txtbox_GrandTotal.Text = FormatNumber(subTotalRemittance + subTotalPR + SubTotalFinPen, 2, , TriState.True).ToString()
    End Sub
#End Region
    Private Sub dgv_OffsetMFAR_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        'Try
        '    Dim r As Integer = dgv_OffsetMFAR.CurrentRow.Index
        '    If e.RowIndex >= 0 Then
        '        Dim ListOfOffsettingSeq As String = dgv_OffsetMFAR.Item(19, r).Value.ToString
        '        Dim WESMBillIDNumber As String = dgv_OffsetMFAR.Item(1, r).Value.ToString
        '        Dim BillingPeriod As String = dgv_OffsetMFAR.Item(0, r).Value.ToString

        '        Dim DT As DataTable = PaymntHelper.GetShareDetailsARDT(ListOfOffsettingSeq, WESMBillIDNumber, BillingPeriod, PaymntHelper.EnergyShare)
        '        Dim DT2 As DataTable = PaymntHelper.GetOffsettingARDetailsDT(ListOfOffsettingSeq, WESMBillIDNumber, BillingPeriod, PaymntHelper.OffsettingMFwithVATCollectionList)
        '        Dim ViewOffsettingARDetails As New frmPaymentNewOffsetting
        '        With ViewOffsettingARDetails
        '            .HideORButton = False
        '            ._PaymentHelper = PaymntHelper
        '            ._WBillHelper = WBillHelper
        '            ._MainLabel = "Energy Share on Non-FIT"
        '            ._DetailsLabel = "Generated Accounts Receivables in Market Fees with VAT"
        '            ._OffsetToInvoiceDT = DT
        '            ._ARAPDT = DT2
        '            .ShowDialog()
        '        End With
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub dgv_OffsetEnergyAR_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        'Try
        '    If e.RowIndex >= 0 Then
        '        Dim r As Integer = dgv_OffsetEnergyAR.CurrentRow.Index
        '        Dim ListOfOffsettingSeq As String = dgv_OffsetEnergyAR.Item(12, r).Value.ToString
        '        Dim WESMBillIDNumber As String = dgv_OffsetEnergyAR.Item(1, r).Value.ToString
        '        Dim BillingPeriod As String = dgv_OffsetEnergyAR.Item(0, r).Value.ToString

        '        Dim DT As DataTable = PaymntHelper.GetShareDetailsARDT(ListOfOffsettingSeq, WESMBillIDNumber, BillingPeriod, PaymntHelper.EnergyShare)
        '        Dim DT2 As DataTable = PaymntHelper.GetOffsettingARDetailsDT(ListOfOffsettingSeq, WESMBillIDNumber, BillingPeriod, PaymntHelper.OffsettingEnergyCollectionList)
        '        Dim ViewOffsettingARDetails As New frmPaymentNewOffsetting
        '        With ViewOffsettingARDetails
        '            ._PaymentHelper = PaymntHelper
        '            ._WBillHelper = WBillHelper
        '            ._MainLabel = "Energy Share on Non-FIT"
        '            ._DetailsLabel = "Generated Accounts Receivables in Energy"
        '            ._OffsetToInvoiceDT = DT
        '            ._ARAPDT = DT2
        '            .ShowDialog()
        '        End With
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub dgv_OffsetVATAR_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        'Try
        '    If e.RowIndex >= 0 Then
        '        Dim r As Integer = dgv_OffsetVATAR.CurrentRow.Index
        '        Dim ListOfOffsettingSeq As String = dgv_OffsetVATAR.Item(10, r).Value.ToString
        '        Dim WESMBillIDNumber As String = dgv_OffsetVATAR.Item(1, r).Value.ToString
        '        Dim BillingPeriod As String = dgv_OffsetVATAR.Item(0, r).Value.ToString

        '        Dim _EnergyAndVATShare As List(Of PaymentShare) = PaymntHelper.EnergyShare.Union(PaymntHelper.VATonEnergyShare).ToList

        '        Dim DT As DataTable = PaymntHelper.GetShareDetailsARDT(ListOfOffsettingSeq, WESMBillIDNumber, BillingPeriod, _EnergyAndVATShare)
        '        Dim DT2 As DataTable = PaymntHelper.GetOffsettingARDetailsDT(ListOfOffsettingSeq, WESMBillIDNumber, BillingPeriod, PaymntHelper.OffsettingVATonEnergyCollectionList)
        '        Dim ViewOffsettingARDetails As New frmPaymentNewOffsetting
        '        With ViewOffsettingARDetails
        '            ._PaymentHelper = PaymntHelper
        '            ._WBillHelper = WBillHelper
        '            ._MainLabel = "VAT Share on Non-FIT"
        '            ._DetailsLabel = "Generated Accounts Receivables in VATonEnergy"
        '            ._OffsetToInvoiceDT = DT
        '            ._ARAPDT = DT2
        '            .ShowDialog()
        '        End With
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub dgv_EnergyAP_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        'Try
        '    If e.RowIndex >= 0 Then
        '        Dim r As Integer = dgv_EnergyAP.CurrentRow.Index
        '        Dim OffsettingSeq As String = "0"
        '        Dim BillingPeriod As String = dgv_EnergyAP.Item(0, r).Value.ToString
        '        Dim IDNumber As String = dgv_EnergyAP.Item(1, r).Value.ToString
        '        Dim InvNumber As String = dgv_EnergyAP.Item(3, r).Value.ToString

        '        Dim DT As DataTable = PaymntHelper.GetShareDetailsAPDT(OffsettingSeq, BillingPeriod, IDNumber, InvNumber, PaymntHelper.EnergyShare)
        '        Dim DT2 As DataTable = PaymntHelper.GetOffsettingAPDetailsDT(OffsettingSeq, BillingPeriod, IDNumber, InvNumber, PaymntHelper.EnergyAllocationList)
        '        Dim ViewOffsettingARDetails As New frmPaymentNewOffsetting
        '        With ViewOffsettingARDetails
        '            .isAP = True
        '            .HideORButton = True
        '            ._PaymentHelper = PaymntHelper
        '            ._WBillHelper = WBillHelper
        '            ._MainLabel = "Energy Share Offsetted on AR Non-FIT"
        '            ._DetailsLabel = "Generated Acounts Payables in Energy"
        '            ._OffsetToInvoiceDT = DT
        '            ._ARAPDT = DT2
        '            .ShowDialog()
        '        End With
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub dgv_OffsetEnergyAP_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        'Try
        '    If e.RowIndex >= 0 Then
        '        Dim r As Integer = dgv_OffsetEnergyAP.CurrentRow.Index

        '        Dim OffsettingSeq As String = dgv_OffsetEnergyAP.Item(12, r).Value.ToString
        '        Dim BillingPeriod As String = dgv_OffsetEnergyAP.Item(0, r).Value.ToString
        '        Dim IDNumber As String = dgv_OffsetEnergyAP.Item(1, r).Value.ToString
        '        Dim InvNumber As String = dgv_OffsetEnergyAP.Item(3, r).Value.ToString


        '        Dim DT As DataTable = PaymntHelper.GetShareDetailsAPDT(OffsettingSeq, BillingPeriod, IDNumber, InvNumber, PaymntHelper.EnergyShare)
        '        Dim DT2 As DataTable = PaymntHelper.GetOffsettingAPDetailsDT(OffsettingSeq, BillingPeriod, IDNumber, InvNumber, PaymntHelper.OffsettingEnergyAllocationList)
        '        Dim ViewOffsettingARDetails As New frmPaymentNewOffsetting
        '        With ViewOffsettingARDetails
        '            .isAP = True
        '            .HideORButton = True
        '            ._PaymentHelper = PaymntHelper
        '            ._WBillHelper = WBillHelper
        '            ._MainLabel = "Energy Share on Non-FIT"
        '            ._DetailsLabel = "Generated Accounts Payables in Energy"
        '            ._OffsetToInvoiceDT = DT
        '            ._ARAPDT = DT2
        '            .ShowDialog()
        '        End With
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub dgv_OffsetVATAP_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        'Try
        '    If e.RowIndex >= 0 Then
        '        Dim r As Integer = dgv_OffsetVATAP.CurrentRow.Index
        '        Dim OffsettingSeq As String = dgv_OffsetVATAP.Item(10, r).Value.ToString
        '        Dim BillingPeriod As String = dgv_OffsetVATAP.Item(0, r).Value.ToString
        '        Dim IDNumber As String = dgv_OffsetVATAP.Item(1, r).Value.ToString
        '        Dim InvNumber As String = dgv_OffsetVATAP.Item(3, r).Value.ToString


        '        Dim DT As DataTable = PaymntHelper.GetShareDetailsAPDT(OffsettingSeq, BillingPeriod, IDNumber, InvNumber, PaymntHelper.VATonEnergyShare)
        '        Dim DT2 As DataTable = PaymntHelper.GetOffsettingAPDetailsDT(OffsettingSeq, BillingPeriod, IDNumber, InvNumber, PaymntHelper.OffsettingVATonEnergyAllocationList)
        '        Dim ViewOffsettingARDetails As New frmPaymentNewOffsetting
        '        With ViewOffsettingARDetails
        '            .isAP = True
        '            ._PaymentHelper = PaymntHelper
        '            ._WBillHelper = WBillHelper
        '            ._MainLabel = "VAT Share on Non-FIT"
        '            ._DetailsLabel = "Generated Accounts Payables in VATonEnergy"
        '            ._OffsetToInvoiceDT = DT
        '            ._ARAPDT = DT2
        '            .ShowDialog()
        '        End With
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub Btn_GenerateJVPayment_Click(sender As Object, e As EventArgs) Handles Btn_GenerateJVPayment.Click
        Try
            Dim DS As New DataSet
            DS = PaymntHelper.GenerateJVReport(EnumPostedType.P)
            If DS.Tables.Count > 0 Then
                ProgressThread.Show("Please wait while preparing JV Payment Report.")                
                Dim frmReport As New frmReportViewer
                With frmReport
                    .LoadJournalVoucher(DS)
                    ProgressThread.Close()
                    .ShowDialog()
                End With
                DS = Nothing
            Else
                MessageBox.Show("No available JV Payment, no movement on settlement invoices.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)        
        End Try
    End Sub

    Private Sub Btn_GenerateJVPaymentAlloc_Click(sender As Object, e As EventArgs) Handles Btn_GenerateJVPaymentAlloc.Click
        Try
            Dim DS As New DataSet

            DS = PaymntHelper.GenerateJVReport(EnumPostedType.PA)
            If DS.Tables.Count > 0 Then
                ProgressThread.Show("Please wait while preparing JV Payment Allocation Report.")                
                Dim frmReport As New frmReportViewer
                With frmReport
                    .LoadJournalVoucher(DS)
                    ProgressThread.Close()
                    .ShowDialog()
                End With
                DS = Nothing
            Else
                MessageBox.Show("No available JV Payment Allocation, no movement on settlement invoices.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Btn_GeneratePaymentEFTandCheck_Click(sender As Object, e As EventArgs) Handles Btn_GeneratePaymentEFTandCheck.Click
        Try
            Dim DS As New DataSet
            DS = PaymntHelper.GenerateJVReport(EnumPostedType.PEFT)
            If DS.Tables.Count > 0 Then
                ProgressThread.Show("Please wait while preparing JV Payment EFT/Check Report.")                
                Dim frmReport As New frmReportViewer
                With frmReport
                    .LoadJournalVoucher(DS)
                    ProgressThread.Close()
                    .ShowDialog()
                End With
                DS = Nothing
            Else
                MessageBox.Show("No available JV Payment EFT And Check, no movement on settlement invoices.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Private Sub dgv_PaymentTransToPR_CellEndEdit(ByVal sender As Object, _
        ByVal e As DataGridViewCellEventArgs) _
        Handles dgv_PaymentTransToPR.CellEndEdit
        Try
            Select Case e.ColumnIndex
                Case 12
                    If Not IsNumeric(Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                        MessageBox.Show("Replenish Amount is not numeric.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "0.00"
                    ElseIf CDec(Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                        MessageBox.Show("Replenish Amount shall be positive or shall not be equal to 0.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "0.00"
                    Else
                        Dim InputtedAmount As Decimal = CDec(Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(12).Value)
                        Dim FinPenAmount As Decimal = CDec(Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(14).Value)
                        Dim EnergyAmount As Decimal = CDec(Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(8).Value)
                        Dim netofEnergyShare As Decimal = EnergyAmount - FinPenAmount
                        If InputtedAmount = 0D Then
                            Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(12).Value = FormatNumber(0, 2, , TriState.True).ToString()
                            Exit Sub
                        End If
                        If InputtedAmount > netofEnergyShare Then
                            MessageBox.Show("The amount you entered is greater than the amount of Energy Share minus the Financial Penalty, please input the correct amount.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "0.00"
                        ElseIf InputtedAmount = netofEnergyShare Then
                            Dim TotalPaymentAllocated As Decimal = CDec(Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(10).Value.ToString) - (FinPenAmount + InputtedAmount)
                            With Me.dgv_PaymentTransToPR.Rows(e.RowIndex)
                                .Cells(11).Value = True
                                .Cells(12).Value = FormatNumber(InputtedAmount, 2, , TriState.True).ToString()
                                .Cells(12).ReadOnly = True
                                .Cells(15).Value = FormatNumber(TotalPaymentAllocated, 2, , TriState.True).ToString()
                                Using _PaymentTranToPR As New PaymentTransferToPR
                                    _PaymentTranToPR.IDNumber = .Cells(0).Value.ToString
                                    _PaymentTranToPR.ParticipantID = .Cells(1).Value.ToString
                                    _PaymentTranToPR.PaymentOnExcessCollection = CDec(.Cells(2).Value.ToString)
                                    _PaymentTranToPR.PaymentOnDeferredonEnergy = CDec(.Cells(3).Value.ToString)
                                    _PaymentTranToPR.PaymentOnDeferredonVATonEnergy = CDec(.Cells(4).Value.ToString)
                                    _PaymentTranToPR.OffsetOnOffsetDeferredonEnergy = CDec(.Cells(5).Value.ToString)
                                    _PaymentTranToPR.OffsetOnOffsetDeferredonVATonEnergy = CDec(.Cells(6).Value.ToString)
                                    _PaymentTranToPR.PaymentOnMFWithVAT = CDec(.Cells(7).Value.ToString)
                                    _PaymentTranToPR.PaymentOnEnergy = CDec(.Cells(8).Value.ToString)
                                    _PaymentTranToPR.PaymentOnVATonEnergy = CDec(.Cells(9).Value.ToString)
                                    _PaymentTranToPR.TotalPaymentAllocated = CDec(.Cells(10).Value.ToString)
                                    _PaymentTranToPR.FullyTransferToPR = CType(.Cells(11).Value.ToString, Boolean)
                                    _PaymentTranToPR.TransferToPrudential = InputtedAmount
                                    _PaymentTranToPR.FullyTransferToFinPen = CType(.Cells(13).Value.ToString, Boolean)
                                    _PaymentTranToPR.TransferToFinPen = CDec(.Cells(14).Value.ToString)
                                    _PaymentTranToPR.TotalAmountForRemittance = Math.Round(TotalPaymentAllocated, 2)
                                    PaymntHelper.UpdateTransferToPR(_PaymentTranToPR)
                                End Using
                            End With
                        Else
                            Dim TotalPaymentAllocated As Decimal = CDec(Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(10).Value.ToString) - (FinPenAmount + InputtedAmount)
                            With Me.dgv_PaymentTransToPR.Rows(e.RowIndex)
                                .Cells(12).Value = FormatNumber(InputtedAmount, 2, , TriState.True).ToString()
                                .Cells(15).Value = FormatNumber(TotalPaymentAllocated, 2, , TriState.True).ToString()
                                Using _PaymentTranToPR As New PaymentTransferToPR
                                    _PaymentTranToPR.IDNumber = .Cells(0).Value.ToString
                                    _PaymentTranToPR.ParticipantID = .Cells(1).Value.ToString
                                    _PaymentTranToPR.PaymentOnExcessCollection = CDec(.Cells(2).Value.ToString)
                                    _PaymentTranToPR.PaymentOnDeferredonEnergy = CDec(.Cells(3).Value.ToString)
                                    _PaymentTranToPR.PaymentOnDeferredonVATonEnergy = CDec(.Cells(4).Value.ToString)
                                    _PaymentTranToPR.OffsetOnOffsetDeferredonEnergy = CDec(.Cells(5).Value.ToString)
                                    _PaymentTranToPR.OffsetOnOffsetDeferredonVATonEnergy = CDec(.Cells(6).Value.ToString)
                                    _PaymentTranToPR.PaymentOnMFWithVAT = CDec(.Cells(7).Value.ToString)
                                    _PaymentTranToPR.PaymentOnEnergy = CDec(.Cells(8).Value.ToString)
                                    _PaymentTranToPR.PaymentOnVATonEnergy = CDec(.Cells(9).Value.ToString)
                                    _PaymentTranToPR.TotalPaymentAllocated = CDec(.Cells(10).Value.ToString)
                                    _PaymentTranToPR.FullyTransferToPR = CType(.Cells(11).Value.ToString, Boolean)
                                    _PaymentTranToPR.TransferToPrudential = InputtedAmount
                                    _PaymentTranToPR.FullyTransferToFinPen = CType(.Cells(13).Value.ToString, Boolean)
                                    _PaymentTranToPR.TransferToFinPen = CDec(.Cells(14).Value.ToString)
                                    _PaymentTranToPR.TotalAmountForRemittance = Math.Round(TotalPaymentAllocated, 2)
                                    PaymntHelper.UpdateTransferToPR(_PaymentTranToPR)
                                End Using
                            End With
                        End If
                        Me.FormatTextBox()
                    End If
                Case 14
                    If Not IsNumeric(Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                        MessageBox.Show("Financial Penalty Amount is not numeric.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "0.00"
                    ElseIf CDec(Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                        MessageBox.Show("Financial Penalty Amount shall be positive or shall not be equal to 0", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "0.00"                    
                    Else
                        Dim InputtedAmount As Decimal = CDec(Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(14).Value)
                        Dim PRReplenishmentAmount As Decimal = CDec(CDec(Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(12).Value))
                        Dim EnergyAmount As Decimal = CDec(Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(8).Value)
                        Dim netofEnergyShare As Decimal = EnergyAmount - PRReplenishmentAmount

                        If InputtedAmount = 0D Then
                            Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(14).Value = FormatNumber(0, 2, , TriState.True).ToString()
                            Exit Sub
                        End If

                        If InputtedAmount > netofEnergyShare Then
                            MessageBox.Show("The amount you entered is greater than the amount of the Energy Share minus the PR replineshment, please input the correct amount!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "0.00"
                        ElseIf InputtedAmount = netofEnergyShare Then
                            Dim TotalPaymentAllocated As Decimal = CDec(Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(10).Value.ToString) - (PRReplenishmentAmount + InputtedAmount)
                            With Me.dgv_PaymentTransToPR.Rows(e.RowIndex)
                                .Cells(13).Value = True
                                .Cells(14).Value = FormatNumber(InputtedAmount, 2, , TriState.True).ToString()
                                .Cells(14).ReadOnly = True
                                .Cells(15).Value = FormatNumber(TotalPaymentAllocated, 2, , TriState.True).ToString()
                                Using _PaymentTranToPR As New PaymentTransferToPR
                                    _PaymentTranToPR.IDNumber = .Cells(0).Value.ToString
                                    _PaymentTranToPR.ParticipantID = .Cells(1).Value.ToString
                                    _PaymentTranToPR.PaymentOnExcessCollection = CDec(.Cells(2).Value.ToString)
                                    _PaymentTranToPR.PaymentOnDeferredonEnergy = CDec(.Cells(3).Value.ToString)
                                    _PaymentTranToPR.PaymentOnDeferredonVATonEnergy = CDec(.Cells(4).Value.ToString)
                                    _PaymentTranToPR.OffsetOnOffsetDeferredonEnergy = CDec(.Cells(5).Value.ToString)
                                    _PaymentTranToPR.OffsetOnOffsetDeferredonVATonEnergy = CDec(.Cells(6).Value.ToString)
                                    _PaymentTranToPR.PaymentOnMFWithVAT = CDec(.Cells(7).Value.ToString)
                                    _PaymentTranToPR.PaymentOnEnergy = CDec(.Cells(8).Value.ToString)
                                    _PaymentTranToPR.PaymentOnVATonEnergy = CDec(.Cells(9).Value.ToString)
                                    _PaymentTranToPR.TotalPaymentAllocated = CDec(.Cells(10).Value.ToString)
                                    _PaymentTranToPR.FullyTransferToPR = CType(.Cells(11).Value.ToString, Boolean)
                                    _PaymentTranToPR.TransferToPrudential = CDec(.Cells(12).Value.ToString)
                                    _PaymentTranToPR.FullyTransferToFinPen = CType(.Cells(13).Value.ToString, Boolean)
                                    _PaymentTranToPR.TransferToFinPen = InputtedAmount
                                    _PaymentTranToPR.TotalAmountForRemittance = Math.Round(TotalPaymentAllocated, 2)
                                    PaymntHelper.UpdateTransferToFinPen(_PaymentTranToPR)
                                End Using
                            End With
                        Else
                            Dim TotalPaymentAllocated As Decimal = CDec(Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(10).Value.ToString) - (PRReplenishmentAmount + InputtedAmount)
                            With Me.dgv_PaymentTransToPR.Rows(e.RowIndex)
                                .Cells(14).Value = FormatNumber(InputtedAmount, 2, , TriState.True).ToString()
                                .Cells(15).Value = FormatNumber(TotalPaymentAllocated, 2, , TriState.True).ToString()
                                Using _PaymentTranToPR As New PaymentTransferToPR
                                    _PaymentTranToPR.IDNumber = .Cells(0).Value.ToString
                                    _PaymentTranToPR.ParticipantID = .Cells(1).Value.ToString
                                    _PaymentTranToPR.PaymentOnExcessCollection = CDec(.Cells(2).Value.ToString)
                                    _PaymentTranToPR.PaymentOnDeferredonEnergy = CDec(.Cells(3).Value.ToString)
                                    _PaymentTranToPR.PaymentOnDeferredonVATonEnergy = CDec(.Cells(4).Value.ToString)
                                    _PaymentTranToPR.OffsetOnOffsetDeferredonEnergy = CDec(.Cells(5).Value.ToString)
                                    _PaymentTranToPR.OffsetOnOffsetDeferredonVATonEnergy = CDec(.Cells(6).Value.ToString)
                                    _PaymentTranToPR.PaymentOnMFWithVAT = CDec(.Cells(7).Value.ToString)
                                    _PaymentTranToPR.PaymentOnEnergy = CDec(.Cells(8).Value.ToString)
                                    _PaymentTranToPR.PaymentOnVATonEnergy = CDec(.Cells(9).Value.ToString)
                                    _PaymentTranToPR.TotalPaymentAllocated = CDec(.Cells(10).Value.ToString)
                                    _PaymentTranToPR.FullyTransferToPR = CType(.Cells(11).Value.ToString, Boolean)
                                    _PaymentTranToPR.TransferToPrudential = CDec(.Cells(12).Value.ToString)
                                    _PaymentTranToPR.FullyTransferToFinPen = CType(.Cells(13).Value.ToString, Boolean)
                                    _PaymentTranToPR.TransferToFinPen = InputtedAmount
                                    _PaymentTranToPR.TotalAmountForRemittance = Math.Round(TotalPaymentAllocated, 2)
                                    PaymntHelper.UpdateTransferToFinPen(_PaymentTranToPR)
                                End Using
                            End With
                        End If
                        Me.FormatTextBox()
                    End If
                Case Else
                    Me.dgv_PaymentTransToPR.CommitEdit(DataGridViewDataErrorContexts.Commit)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgv_PaymentTransToPR_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgv_PaymentTransToPR.CurrentCellDirtyStateChanged
        If Not Me.dgv_PaymentTransToPR.IsCurrentCellDirty Then
            Exit Sub
        End If
        Try
            Select Case Me.dgv_PaymentTransToPR.CurrentCell.ColumnIndex()
                Case 11
                    Me.dgv_PaymentTransToPR.CommitEdit(DataGridViewDataErrorContexts.Commit)
                    With Me.dgv_PaymentTransToPR.CurrentRow
                        Dim netofEnergyShare As Decimal = CDec(.Cells(8).Value) - CDec(.Cells(14).Value)
                        Dim netofPaymentAllocated As Decimal = CDec(.Cells(10).Value) - CDec(.Cells(14).Value)

                        If CBool(.Cells(11).Value) = True And CBool(.Cells(13).Value) = False Then
                            If CDec(.Cells(8).Value) = 0 Then
                                .Cells(11).Value = False
                                .Cells(12).Value = "0.00"
                                Me.dgv_PaymentTransToPR.RefreshEdit()
                                MessageBox.Show("No available share for energy!", "Invalid!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                            .Cells(11).Value = True
                            .Cells(12).Value = FormatNumber(netofEnergyShare, 2, , TriState.True).ToString()
                            .Cells(12).ReadOnly = True
                            .Cells(13).Value = False
                            '.Cells(14).Value = FormatNumber(0, 2, , TriState.True).ToString()
                            .Cells(15).Value = FormatNumber(netofPaymentAllocated - netofEnergyShare, 2, , TriState.True).ToString()
                            Using _PaymentTranToPR As New PaymentTransferToPR
                                _PaymentTranToPR.IDNumber = .Cells(0).Value.ToString
                                _PaymentTranToPR.ParticipantID = .Cells(1).Value.ToString
                                _PaymentTranToPR.PaymentOnExcessCollection = CDec(.Cells(2).Value.ToString)
                                _PaymentTranToPR.PaymentOnDeferredonEnergy = CDec(.Cells(3).Value.ToString)
                                _PaymentTranToPR.PaymentOnDeferredonVATonEnergy = CDec(.Cells(4).Value.ToString)
                                _PaymentTranToPR.OffsetOnOffsetDeferredonEnergy = CDec(.Cells(5).Value.ToString)
                                _PaymentTranToPR.OffsetOnOffsetDeferredonVATonEnergy = CDec(.Cells(6).Value.ToString)
                                _PaymentTranToPR.PaymentOnMFWithVAT = CDec(.Cells(7).Value.ToString)
                                _PaymentTranToPR.PaymentOnEnergy = CDec(.Cells(8).Value.ToString)
                                _PaymentTranToPR.PaymentOnVATonEnergy = CDec(.Cells(9).Value.ToString)
                                _PaymentTranToPR.TotalPaymentAllocated = CDec(.Cells(10).Value.ToString)
                                _PaymentTranToPR.FullyTransferToPR = CType(.Cells(11).Value.ToString, Boolean)
                                _PaymentTranToPR.TransferToPrudential = _PaymentTranToPR.PaymentOnEnergy - CDec(.Cells(14).Value.ToString)
                                _PaymentTranToPR.FullyTransferToFinPen = CType(.Cells(13).Value.ToString, Boolean)
                                _PaymentTranToPR.TransferToFinPen = CDec(.Cells(14).Value.ToString)
                                _PaymentTranToPR.TotalAmountForRemittance = Math.Round(_PaymentTranToPR.TotalPaymentAllocated - _PaymentTranToPR.TransferToFinPen - _PaymentTranToPR.TransferToPrudential, 2)
                                PaymntHelper.UpdateTransferToPR(_PaymentTranToPR)
                            End Using
                        ElseIf CBool(.Cells(13).Value) = True And CBool(.Cells(11).Value) = True Then
                            MessageBox.Show("You have already transfered the amount in Financial Penalty!", "System Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            .Cells(11).Value = False
                            .Cells(12).Value = "0.00"
                            Me.dgv_PaymentTransToPR.RefreshEdit()
                        Else
                            .Cells(11).Value = False
                            .Cells(12).Value = "0.00"
                            .Cells(12).ReadOnly = False
                            .Cells(15).Value = FormatNumber(CDec(.Cells(10).Value), 2, , TriState.True).ToString
                            Using _PaymentTranToPR As New PaymentTransferToPR
                                _PaymentTranToPR.IDNumber = .Cells(0).Value.ToString
                                _PaymentTranToPR.ParticipantID = .Cells(1).Value.ToString
                                _PaymentTranToPR.PaymentOnExcessCollection = CDec(.Cells(2).Value.ToString)
                                _PaymentTranToPR.PaymentOnDeferredonEnergy = CDec(.Cells(3).Value.ToString)
                                _PaymentTranToPR.PaymentOnDeferredonVATonEnergy = CDec(.Cells(4).Value.ToString)
                                _PaymentTranToPR.OffsetOnOffsetDeferredonEnergy = CDec(.Cells(5).Value.ToString)
                                _PaymentTranToPR.OffsetOnOffsetDeferredonVATonEnergy = CDec(.Cells(6).Value.ToString)
                                _PaymentTranToPR.PaymentOnMFWithVAT = CDec(.Cells(7).Value.ToString)
                                _PaymentTranToPR.PaymentOnEnergy = CDec(.Cells(8).Value.ToString)
                                _PaymentTranToPR.PaymentOnVATonEnergy = CDec(.Cells(9).Value.ToString)
                                _PaymentTranToPR.TotalPaymentAllocated = CDec(.Cells(10).Value.ToString)
                                _PaymentTranToPR.FullyTransferToPR = CType(.Cells(11).Value.ToString, Boolean)
                                _PaymentTranToPR.TransferToPrudential = 0D
                                _PaymentTranToPR.FullyTransferToFinPen = CType(.Cells(13).Value.ToString, Boolean)
                                _PaymentTranToPR.TransferToFinPen = CDec(.Cells(14).Value.ToString)
                                _PaymentTranToPR.TotalAmountForRemittance = Math.Round(_PaymentTranToPR.TotalPaymentAllocated - _PaymentTranToPR.TransferToFinPen - _PaymentTranToPR.TransferToPrudential, 2)
                                PaymntHelper.UpdateTransferToPR(_PaymentTranToPR)
                            End Using
                        End If
                        Me.FormatTextBox()
                    End With
                Case 13
                    Me.dgv_PaymentTransToPR.CommitEdit(DataGridViewDataErrorContexts.Commit)
                    With Me.dgv_PaymentTransToPR.CurrentRow
                        Dim netofEnergyShare As Decimal = CDec(.Cells(8).Value) - CDec(.Cells(12).Value)
                        Dim netofPaymentAllocated As Decimal = CDec(.Cells(10).Value) - CDec(.Cells(12).Value)

                        If CBool(.Cells(13).Value) = True And CBool(.Cells(11).Value) = False Then
                            If CDec(.Cells(8).Value) = 0 Then
                                .Cells(13).Value = False
                                .Cells(14).Value = "0.00"
                                Me.dgv_PaymentTransToPR.RefreshEdit()
                                MessageBox.Show("No available share for energy!", "Invalid!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                            .Cells(13).Value = True
                            .Cells(14).Value = FormatNumber(netofEnergyShare, 2, , TriState.True).ToString()
                            .Cells(14).ReadOnly = True
                            .Cells(15).Value = FormatNumber(Math.Round(netofPaymentAllocated - netofEnergyShare, 2), 2, , TriState.True).ToString()
                            Using _PaymentTranToPR As New PaymentTransferToPR
                                _PaymentTranToPR.IDNumber = .Cells(0).Value.ToString
                                _PaymentTranToPR.ParticipantID = .Cells(1).Value.ToString
                                _PaymentTranToPR.PaymentOnExcessCollection = CDec(.Cells(2).Value.ToString)
                                _PaymentTranToPR.PaymentOnDeferredonEnergy = CDec(.Cells(3).Value.ToString)
                                _PaymentTranToPR.PaymentOnDeferredonVATonEnergy = CDec(.Cells(4).Value.ToString)
                                _PaymentTranToPR.OffsetOnOffsetDeferredonEnergy = CDec(.Cells(5).Value.ToString)
                                _PaymentTranToPR.OffsetOnOffsetDeferredonVATonEnergy = CDec(.Cells(6).Value.ToString)
                                _PaymentTranToPR.PaymentOnMFWithVAT = CDec(.Cells(7).Value.ToString)
                                _PaymentTranToPR.PaymentOnEnergy = CDec(.Cells(8).Value.ToString)
                                _PaymentTranToPR.PaymentOnVATonEnergy = CDec(.Cells(9).Value.ToString)
                                _PaymentTranToPR.TotalPaymentAllocated = CDec(.Cells(10).Value.ToString)
                                _PaymentTranToPR.FullyTransferToPR = CType(.Cells(11).Value.ToString, Boolean)
                                _PaymentTranToPR.TransferToPrudential = CDec(.Cells(12).Value.ToString)
                                _PaymentTranToPR.FullyTransferToFinPen = True
                                _PaymentTranToPR.TransferToFinPen = _PaymentTranToPR.PaymentOnEnergy - CDec(.Cells(12).Value.ToString)
                                _PaymentTranToPR.TotalAmountForRemittance = Math.Round(_PaymentTranToPR.TotalPaymentAllocated - _PaymentTranToPR.TransferToPrudential - _PaymentTranToPR.TransferToFinPen, 2)
                                PaymntHelper.UpdateTransferToFinPen(_PaymentTranToPR)
                            End Using
                        ElseIf CBool(.Cells(13).Value) = True And CBool(.Cells(11).Value) = True Then
                            MessageBox.Show("You have already transfered the amount in PR Replenishment!", "System Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            .Cells(13).Value = False
                            .Cells(14).Value = "0.00"
                            Me.dgv_PaymentTransToPR.RefreshEdit()
                        Else
                            .Cells(13).Value = False
                            .Cells(14).Value = "0.00"
                            .Cells(14).ReadOnly = False
                            .Cells(15).Value = FormatNumber(netofPaymentAllocated, 2, , TriState.True).ToString
                            Using _PaymentTranToPR As New PaymentTransferToPR
                                _PaymentTranToPR.IDNumber = .Cells(0).Value.ToString
                                _PaymentTranToPR.ParticipantID = .Cells(1).Value.ToString
                                _PaymentTranToPR.PaymentOnExcessCollection = CDec(.Cells(2).Value.ToString)
                                _PaymentTranToPR.PaymentOnDeferredonEnergy = CDec(.Cells(3).Value.ToString)
                                _PaymentTranToPR.PaymentOnDeferredonVATonEnergy = CDec(.Cells(4).Value.ToString)
                                _PaymentTranToPR.OffsetOnOffsetDeferredonEnergy = CDec(.Cells(5).Value.ToString)
                                _PaymentTranToPR.OffsetOnOffsetDeferredonVATonEnergy = CDec(.Cells(6).Value.ToString)
                                _PaymentTranToPR.PaymentOnMFWithVAT = CDec(.Cells(7).Value.ToString)
                                _PaymentTranToPR.PaymentOnEnergy = CDec(.Cells(8).Value.ToString)
                                _PaymentTranToPR.PaymentOnVATonEnergy = CDec(.Cells(9).Value.ToString)
                                _PaymentTranToPR.TotalPaymentAllocated = CDec(.Cells(10).Value.ToString)
                                _PaymentTranToPR.FullyTransferToPR = CType(.Cells(11).Value.ToString, Boolean)
                                _PaymentTranToPR.TransferToPrudential = CDec(.Cells(12).Value.ToString)
                                _PaymentTranToPR.FullyTransferToFinPen = CType(.Cells(13).Value.ToString, Boolean)
                                _PaymentTranToPR.TransferToFinPen = 0D
                                _PaymentTranToPR.TotalAmountForRemittance = Math.Round(_PaymentTranToPR.TotalPaymentAllocated - _PaymentTranToPR.TransferToPrudential - _PaymentTranToPR.TransferToFinPen, 2)
                                PaymntHelper.UpdateTransferToFinPen(_PaymentTranToPR)
                            End Using
                        End If
                        Me.FormatTextBox()
                    End With
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Btn_GenerateDMCMSummaryReport_Click(sender As Object, e As EventArgs) Handles Btn_GenerateDMCMSummaryReport.Click
        Dim sFolderDialog As New FolderBrowserDialog
        Dim FilePath As String = ""
        Try
            If PaymntHelper.GetDMCMSummaryList.Count > 0 Then                
                With sFolderDialog
                    .ShowDialog()
                    If .SelectedPath.ToString.Trim.Length = 0 Then
                        Exit Sub
                    Else
                        FilePath = sFolderDialog.SelectedPath
                    End If
                End With
                ProgressThread.Show("Please wait while preparing DMCM Summary Report.")
                PaymntHelper.CreateDMCMSummaryDoc(FilePath)
                ProgressThread.Close()
                MessageBox.Show("Successfully exported please see in targeted path.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("No available DMCM Summary Report.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)        
        End Try
    End Sub

    Private Sub btn_CollectionAndPaymentReport_Click(sender As Object, e As EventArgs) Handles btn_CollectionAndPaymentReport.Click
        Dim sFolderDialog As New FolderBrowserDialog
        Dim FilePath As String = ""
        Try
            With sFolderDialog
                .ShowDialog()
                If .SelectedPath.ToString.Trim.Length = 0 Then
                    Exit Sub
                Else
                    FilePath = sFolderDialog.SelectedPath
                End If
            End With
            ProgressThread.Show("Please wait while preparing CAPSummary Report.")            
            PaymntHelper.CreateCollAndPaySummReport(FilePath)
            ProgressThread.Close()
            MessageBox.Show("Successfully exported please see in targeted path.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_ORSummaryReport_Click(sender As Object, e As EventArgs) Handles btn_ORSummaryReport.Click
        Try
            Dim SelectedAllocationDate As AllocationDate = New AllocationDate(CDate(FormatDateTime(CDate(cbo_CollectionAllocDate.SelectedItem), DateFormat.ShortDate)), 0)
            Dim ViewReport As New frmReportViewer
            Dim _DT As New DSReport.CollectionDataTable
            Dim ORDT As New DSReport.CollectionDataTable
            ProgressThread.Show("Please wait while preparing OR Summary Report.")
            ORDT = CType(PaymntHelper.GenerateORSummary(_DT), DSReport.CollectionDataTable)
            If ORDT.Count > 0 Then
                With ViewReport
                    .LoadPaymentORSummary(ORDT, SelectedAllocationDate.CollAllocationDate, SelectedAllocationDate.CollAllocationDate)
                    ProgressThread.Close()
                    .Show()
                End With
                ORDT = Nothing
            Else
                ProgressThread.Close()
                MessageBox.Show("No available OR Summary Report.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)        
        End Try
    End Sub

    Private Sub btn_RFPSummaryReport_Click(sender As Object, e As EventArgs) Handles btn_RFPSummaryReport.Click
        Try
            Dim DS As New DataSet
            Dim tblRFPMain As New DSReport.RFPMainDataTable
            Dim tblRFPColl As New DSReport.RFPDetailsCollectionDataTable
            Dim tblRFPPay As New DSReport.RFPDetailsPaymentDataTable
            DS = PaymntHelper.GenerateRFP(tblRFPMain, tblRFPColl, tblRFPPay)

            Dim RPTViewer As New frmReportViewer

            ProgressThread.Show("Please wait while preparing RFP Summary Report.")
            With RPTViewer
                .LoadRFP(DS)
                ProgressThread.Close()
                .ShowDialog()
            End With
            DS = Nothing
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)        
        End Try
    End Sub

    Private Async Sub btn_SaveAllocation_Click(sender As Object, e As EventArgs) Handles btn_SaveAllocation.Click
        Try
            Dim msgAns As New MsgBoxResult
            msgAns = MsgBox("Do you really want to save the Payment Allocations?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "System Message")

            If msgAns = MsgBoxResult.Yes Then
                mainTLPanel.Enabled = False
                Dim getTimeStart As New DateTime
                Dim getTimeEnd As New DateTime
                cts = New CancellationTokenSource
                ProgressThread.Show("Please wait while SAVING.")
                Me.GB_CntButtons.Enabled = False
                getTimeStart = WBillHelper.GetSystemDateTime
                Dim progressIndicator As New Progress(Of ProgressClass)(AddressOf UpdateProgress)

                Await Task.Run(Sub() PaymntHelper.SavePaymentProcess(progressIndicator, cts.Token))

                getTimeEnd = WBillHelper.GetSystemDateTime
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.PaymentAllocationWindow.ToString, "", "Saving allocation date " & PaymntHelper._PayAllocDate.CollAllocationDate.ToShortDateString, "", CType(EnumColorCode.Green, ColorCode), EnumLogType.SuccesffullySaved.ToString, AMModule.UserName)

                'Disposing trash data in memory                
                PaymntHelper.Dispose()
                PaymntHelper = Nothing
                PaymntHelper = New PaymentHelper

                ProgressThread.Close()

                MessageBox.Show("The processed data have been successfully saved." & vbNewLine _
                                & "Date Time Start: " & getTimeStart.ToString("MM/dd/yyyy hh:mm") & vbNewLine _
                                & "Date Time End: " & getTimeEnd.ToString("MM/dd/yyyy hh:mm"), "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.ClearControlls()
                Me.LoadComboItems()
                cts = Nothing
                mainTLPanel.Enabled = True
            End If
        Catch ex As Exception
            mainTLPanel.Enabled = True
            cts = Nothing
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.GB_CntButtons.Enabled = True
        End Try
    End Sub

    Private Sub btn_FTFReport_Click(sender As Object, e As EventArgs) Handles btn_FTFReport.Click
        Try
            Dim _frmPaymentNewFTF As New frmPaymentNewFTF
            Dim Signatory = WBillHelper.GetSignatories("FTF").First()
            Dim Signatory2 = WBillHelper.GetSignatories("FTF2").First()
            With _frmPaymentNewFTF
                ._PymtHelper = PaymntHelper
                ._Signatory = Signatory
                ._Signatory2 = Signatory2
                .ShowDialog()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_Close_Click(sender As Object, e As EventArgs) Handles btn_Close.Click
        If cts IsNot Nothing Then
            If MessageBox.Show("Are you sure to cancel the saving process?", "Close",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                cts.Cancel()
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub btn_DeferredPaymentReport_Click(sender As Object, e As EventArgs) Handles btn_DeferredPaymentReport.Click
        Try
            Dim RPTViewer As New frmReportViewer
            Dim DSReprotDT As New DSReport.DeferredMonitoringDataTable
            Dim DT As New DSReport.DeferredMonitoringDataTable
            DT = CType(PaymntHelper.GenerateDeferredPaymentsDT(DSReprotDT), DSReport.DeferredMonitoringDataTable)

            ProgressThread.Show("Please wait while preparing Deferred Payment Report.")            
            With RPTViewer
                .LoadDeferredMonitoringReport(DT)
                ProgressThread.Close()
                .ShowDialog()
            End With

            DT = Nothing
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgv_PaymentTransToPR_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv_PaymentTransToPR.CellMouseDoubleClick
        Try
            If e.RowIndex >= 0 Then
                If CDec(Me.dgv_PaymentTransToPR.Rows(e.RowIndex).Cells(14).Value) = 0 Then
                    Exit Sub
                End If

                Dim r As Integer = Me.dgv_PaymentTransToPR.CurrentRow.Index
                Dim frmViewer As New frmReportViewer()
                Dim IDNumber As String = dgv_PaymentTransToPR.Item(0, r).Value.ToString

                ProgressThread.Show("Please wait while preparing OR Report for FinPen.")
                Dim result = PaymntHelper.GenerateORFinPenReport(New DSReport.OfficialReceiptMainNewDataTable, IDNumber)
                If Not result Is Nothing Then
                    With frmViewer
                        .LoadOR(result)
                        ProgressThread.Close()
                        .ShowDialog()
                    End With
                End If
            End If
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub
    Private Sub frmPaymentNew_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If cts IsNot Nothing Then
            ProgressThread.Close()
            If MessageBox.Show("Are you sure to cancel this process?", "Close",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                cts.Cancel()
                e.Cancel = True
                MessageBox.Show("Please try to close again!", "Close", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ProgressThread.Show("Please wait while continuing the process.")
                e.Cancel = True
                Me.Show()
            End If
        End If
    End Sub
End Class