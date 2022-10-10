Option Explicit On
Option Strict On

Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports System.Threading
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib
Imports System.Threading.Tasks

Public Class frmPaymentNewView
    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory
    Private PaymntHelper As New PaymentHelper
    Private PaymentAllocationDateList As New List(Of AllocationDate)

    Private Sub frmPaymentNewView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Try
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName
            BFactory = BusinessFactory.GetInstance()

            Me.LoadComboItems()
            Me.btn_Calculate.Enabled = False

            Me.GB_ProformaEntries.Enabled = False
            Me.GB_ProformaEntriesDetails.Enabled = False
            Me.GB_CntButton.Enabled = False
            Me.ts_StatusDesc.Text = "Ready"


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)           
        End Try
    End Sub

    Public Sub LoadComboItems()
        Me.btn_Calculate.Enabled = False
        Me.GB_ProformaEntries.Enabled = False
        Me.GB_ProformaEntriesDetails.Enabled = False
        Me.GB_CntButton.Enabled = False

        Me.cbo_CollectionAllocDate.Items.Clear() 'Clear Combobox        
        Me.PaymentAllocationDateList = Me.WBillHelper.GetPayAllocDate()

        If PaymentAllocationDateList.Count = 0 Then
            MsgBox("No history of payment data was found.", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, MsgBoxStyle), "No Collection Data")
            Exit Sub
        End If

        For Each PayAllocDate In PaymentAllocationDateList
            Me.cbo_CollectionAllocDate.Items.Add(PayAllocDate.CollAllocationDate)
        Next

    End Sub

    Dim PayAllocationDate As AllocationDate = New AllocationDate

    Private Async Sub cbo_CollectionAllocDate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_CollectionAllocDate.SelectedIndexChanged
        Try
            Me.ClearControls()
            Dim SelectedDate As Date = CDate(FormatDateTime(CDate(cbo_CollectionAllocDate.SelectedItem), DateFormat.ShortDate))
            Dim SelectedPayAllocDateItem As AllocationDate = (From x In Me.PaymentAllocationDateList _
                                                              Where x.CollAllocationDate = SelectedDate _
                                                              Select x).First()
            PayAllocationDate = SelectedPayAllocDateItem

            Dim _Colls As New List(Of PaymentNew)
            Dim _ARAlloc As New List(Of DataTable)
            Dim _PaymentProformaEntries As New PaymentProformaEntries

            Me.ts_Progressbar.Visible = True
            Me.ts_Progressbar.Style = ProgressBarStyle.Marquee
            Me.ts_StatusDesc.Text = "Please wait while fetching payment history."

            'ProgressThread.Show("Please wait while preparing payment view.")
            Me.PaymntHelper = New PaymentHelper 'PaymentHelper.GetInstance()
            Me.PaymntHelper.InitializeObjectView(BFactory, WBillHelper, PayAllocationDate)

            Await Task.Run(Sub() PaymntHelper.GetCollections())

            ''Energy View
            'dgv_EnergyAR.DataSource = PaymntHelper.EnergyCollectionListDT
            'Me.DataGridViewFormatForEnergy(dgv_EnergyAR)
            'Me.CalculateTotalARonEnergy(PaymntHelper.EnergyListCollection)

            ''VAT View
            'dgv_VATAR.DataSource = PaymntHelper.VATonEnergyCollectionListDT
            'Me.DataGridViewFormatForVAT(dgv_VATAR)
            'Me.CalculateTotalARonVAT(PaymntHelper.VATonEnergyCollectionList)

            ''MF View
            'dgv_MFAR.DataSource = PaymntHelper.MFwithVATCollectionListDT
            'Me.DataGridViewFormatForMF(dgv_MFAR)
            'Me.CalculateTotalARonMF(PaymntHelper.MFwithVATCollectionList)
            Me.ts_Progressbar.Visible = False            
            Me.ts_StatusDesc.Text = "Please select 'View Payment' to fetch payment history."
            Await Task.Delay(1000)

            btn_Calculate.Enabled = True
            Me.GB_ProformaEntries.Enabled = False
            Me.GB_ProformaEntriesDetails.Enabled = False
            Me.GB_CntButton.Enabled = False

        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'ProgressThread.Close()
        End Try
    End Sub

#Region "Clearing Controls"
    Private Sub ClearControls()
        'Me.dgv_EnergyAR.DataSource = Nothing
        'Me.dgv_EnergyAP.DataSource = Nothing
        'Me.dgv_VATAR.DataSource = Nothing
        'Me.dgv_VATAP.DataSource = Nothing
        'Me.dgv_MFAR.DataSource = Nothing
        'Me.dgv_MFAP.DataSource = Nothing

        'Me.dgv_OffsetMFAR.DataSource = Nothing
        'Me.dgv_OffsetMFAP.DataSource = Nothing
        'Me.dgv_OffsetEnergyAR.DataSource = Nothing
        'Me.dgv_OffsetEnergyAP.DataSource = Nothing
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

        If dgv.Columns.Count > 14 Then
            With dgv.Columns(14)
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

#Region "Input Total AR on VAT to Textbox"
    Private Sub CalculateTotalARonVAT(ByVal Collections As List(Of ARCollection))

        'Dim TotalARCollonEnergyVAT = (From x In Collections Where x.CollectionType = EnumCollectionType.VatOnEnergy And x.CollectionCategory = EnumCollectionCategory.Cash
        '                           Select x.AllocationAmount).ToList()

        'Me.Txtbox_GrandTotalofAREnergyVAT.Text = FormatNumber(TotalARCollonEnergyVAT.Sum(), 2, , TriState.True).ToString()
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

        'Dim TotalARCollonWHVATMF = (From x In Collections Where x.CollectionType = EnumCollectionType.WithholdingVatonMF And x.CollectionCategory = EnumCollectionCategory.Cash
        '                             Select x.AllocationAmount).ToList()
        'Dim TotalARCollonWHVATDIMF = (From x In Collections Where x.CollectionType = EnumCollectionType.WithholdingVatonDefaultInterest And x.CollectionCategory = EnumCollectionCategory.Cash
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

#Region "Button Calculate"
    Private Async Sub btn_Calculate_Click(sender As Object, e As EventArgs) Handles btn_Calculate.Click
        Try
            'ProgressThread.Show("Please wait while preparing payment view.")
            Me.GB_Allocation.Enabled = False
            Me.ts_Progressbar.Visible = True
            Me.ts_Progressbar.Style = ProgressBarStyle.Marquee
            Me.ts_StatusDesc.Text = "Please wait while preparing payment history."
            Await Task.Delay(1000)
            'Allocation       
            Await Task.Run(Sub() PaymntHelper.GetPayments())

            'Me.dgv_EnergyAP.DataSource = PaymntHelper.EnergyAllocationListDT
            'Me.DataGridViewFormatForEnergy(Me.dgv_EnergyAP)
            'Me.CalculateTotalAPonEnergy(PaymntHelper.EnergyAllocationList)

            'Me.dgv_VATAP.DataSource = PaymntHelper.VATonEnergyAllocationListDT
            'Me.DataGridViewFormatForVAT(Me.dgv_VATAP)
            'Me.CalculateTotalAPonVAT(PaymntHelper.VATonEnergyAllocationList)

            'Me.dgv_MFAP.DataSource = PaymntHelper.MFwithVATAllocationListDT
            'Me.DataGridViewFormatForMF(dgv_MFAP)
            'Me.CalculateTotalAPonMF(PaymntHelper.MFwithVATAllocationList)

            Await Task.Run(Sub() PaymntHelper.GetPaymentOffsetting())

            ''Offsetting MF
            ''AR
            'dgv_OffsetMFAR.DataSource = PaymntHelper.OffsettingMFCollectionListDT
            'Me.DataGridViewFormatForMF(dgv_OffsetMFAR)
            'Me.CalculateTotalOffsetARonMF(PaymntHelper.OffsettingMFwithVATCollectionList)
            ''AP
            'dgv_OffsetMFAP.DataSource = PaymntHelper.OffsettingMFAllocationListDT
            'Me.DataGridViewFormatForMF(dgv_OffsetMFAP)
            'Me.CalculateTotalOffsetAPonMF(PaymntHelper.OffsettingMFwithVATAllocationList)

            ''Offsetting Energy
            ''AR
            'dgv_OffsetEnergyAR.DataSource = PaymntHelper.OffsettingEnergyCollectionListDT
            'Me.DataGridViewFormatForOffsetEnergy(dgv_OffsetEnergyAR)
            'Me.CalculateTotalOffsetARonEnergy(PaymntHelper.OffsettingEnergyCollectionList)
            ''AP
            'Me.dgv_OffsetEnergyAP.DataSource = PaymntHelper.OffsettingEnergyAllocationListDT
            'Me.DataGridViewFormatForOffsetEnergy(dgv_OffsetEnergyAP)
            'Me.CalculateTotalOffsetAPonEnergy(PaymntHelper.OffsettingEnergyAllocationList)

            ''Offsetting VAT
            ''AR
            'Me.dgv_OffsetVATAR.DataSource = PaymntHelper.OffsettingVATonEnergyCollectionListDT
            'Me.DataGridViewFormatForVAT(Me.dgv_OffsetVATAR)
            'Me.CalculateTotalOffsetARonVAT(PaymntHelper.OffsettingVATonEnergyCollectionList)
            ''AP
            'Me.dgv_OffsetVATAP.DataSource = PaymntHelper.OffsettingVATonEnergyAllocationListDT
            'Me.DataGridViewFormatForVAT(Me.dgv_OffsetVATAP)
            'Me.CalculateTotalOffsetAPonVAT(PaymntHelper.OffsettingVATonEnergyAllocationList)

            Me.dgv_PaymentTransToPR.DataSource = PaymntHelper.PaymentTrasferToPRDT()
            Me.DataGridViewFormatTransferToPR(Me.dgv_PaymentTransToPR)
            Me.FormatTextBox()

            Me.btn_Calculate.Enabled = False
            Me.GB_Allocation.Enabled = True
            Me.GB_CntButton.Enabled = True
            Me.GB_ProformaEntries.Enabled = True
            Me.GB_ProformaEntriesDetails.Enabled = True

            Me.ts_Progressbar.Visible = False
            Me.ts_StatusDesc.Text = "Ready for viewing"
            Me.ts_LabelName.Text = Me.dgv_PaymentTransToPR.RowCount.ToString("N0") & " records"

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ProgressThread.Close()
        End Try
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

        'Dim TotalARCollonWHVATMF = (From x In Offsettings Where x.CollectionType = EnumCollectionType.WithholdingVatonMF And x.CollectionCategory = EnumCollectionCategory.Offset
        '                             Select x.AllocationAmount).ToList()
        'Dim TotalARCollonWHVATDIMF = (From x In Offsettings Where x.CollectionType = EnumCollectionType.WithholdingVatonDefaultInterest And x.CollectionCategory = EnumCollectionCategory.Offset
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

#Region "DataGridView Format for TransferToPR"
    Private Sub DataGridViewFormatTransferToPR(ByVal dgv As DataGridView)
        For i As Int32 = 0 To dgv.ColumnCount - 1
            dgv.Columns(i).ReadOnly = True
        Next
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

    Private Async Sub Btn_GenerateJVPayment_Click(sender As Object, e As EventArgs) Handles Btn_GenerateJVPayment.Click
        Try
            Dim DS As New DataSet

            Me.GB_Allocation.Enabled = False
            Me.GB_ProformaEntries.Enabled = False
            Me.GB_ProformaEntriesDetails.Enabled = False
            Me.ts_StatusDesc.Text = "Please wait while preparing JV Payment Report."
            Await Task.Delay(1000)

            DS = Await Task.Run(Function() PaymntHelper.GenerateJVReport(EnumPostedType.P))

            If DS.Tables.Count > 0 Then
                Me.ts_StatusDesc.Text = "..."
                Dim frmReport As New frmReportViewer
                With frmReport
                    .LoadJournalVoucher(DS)                    
                    .ShowDialog()
                End With
            Else                
                MessageBox.Show("No available JV Payment, no movement on settlement invoices.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            DS = Nothing
           
        Catch ex As Exception            
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.GB_Allocation.Enabled = True
            Me.GB_ProformaEntries.Enabled = True
            Me.GB_ProformaEntriesDetails.Enabled = True
            Me.ts_StatusDesc.Text = "Ready for viewing."
        End Try
    End Sub

    Private Async Sub Btn_GenerateJVPaymentAlloc_Click(sender As Object, e As EventArgs) Handles Btn_GenerateJVPaymentAlloc.Click
        Try
            Dim DS As New DataSet

            Me.GB_Allocation.Enabled = False
            Me.GB_ProformaEntries.Enabled = False
            Me.GB_ProformaEntriesDetails.Enabled = False
            Me.ts_StatusDesc.Text = "Please wait while preparing JV Payment Allcoation Report."
            Await Task.Delay(1000)

            DS = Await Task.Run(Function() PaymntHelper.GenerateJVReport(EnumPostedType.PA))            

            If DS.Tables.Count > 0 Then
                Me.ts_StatusDesc.Text = "..."
                Dim frmReport As New frmReportViewer
                With frmReport
                    .LoadJournalVoucher(DS)
                    .ShowDialog()
                End With
            Else
                MessageBox.Show("No available JV Payment Allocation, no movement on settlement invoices.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            DS = Nothing
        Catch ex As Exception            
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.GB_Allocation.Enabled = True
            Me.GB_ProformaEntries.Enabled = True
            Me.GB_ProformaEntriesDetails.Enabled = True
            Me.ts_StatusDesc.Text = "Ready for viewing."
        End Try
    End Sub

    Private Async Sub Btn_GeneratePaymentEFTandCheck_Click(sender As Object, e As EventArgs) Handles Btn_GeneratePaymentEFTandCheck.Click
        Try
            Dim DS As New DataSet
            Me.GB_Allocation.Enabled = False
            Me.GB_ProformaEntries.Enabled = False
            Me.GB_ProformaEntriesDetails.Enabled = False
            Me.ts_StatusDesc.Text = "Please wait while preparing JV Payment EFT and Check Report."
            Await Task.Delay(1000)

            DS = Await Task.Run(Function() PaymntHelper.GenerateJVReport(EnumPostedType.PEFT))            
            If DS.Tables.Count > 0 Then
                Me.ts_StatusDesc.Text = "..."
                Dim frmReport As New frmReportViewer
                With frmReport
                    .LoadJournalVoucher(DS)
                    .ShowDialog()
                End With
            Else
                MessageBox.Show("No available JV Payment EFT And Check, no movement on settlement invoices.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            DS = Nothing
        Catch ex As Exception            
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.GB_Allocation.Enabled = True
            Me.GB_ProformaEntries.Enabled = True
            Me.GB_ProformaEntriesDetails.Enabled = True
            Me.ts_StatusDesc.Text = "Ready for viewing."
        End Try
    End Sub

    Private Async Sub Btn_GenerateDMCMSummaryReport_Click(sender As Object, e As EventArgs) Handles Btn_GenerateDMCMSummaryReport.Click
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

            Me.GB_Allocation.Enabled = False
            Me.GB_ProformaEntries.Enabled = False
            Me.GB_ProformaEntriesDetails.Enabled = False
            Me.ts_StatusDesc.Text = "Please wait while preparing DMCM Summary Report."
            Await Task.Delay(1000)

            Await Task.Run(Sub() PaymntHelper.CreateDMCMSummaryDoc(FilePath))

            Me.ts_StatusDesc.Text = "..."
            Await Task.Delay(1000)
            MessageBox.Show("Successfully exported please see in targeted path.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception            
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.GB_Allocation.Enabled = True
            Me.GB_ProformaEntries.Enabled = True
            Me.GB_ProformaEntriesDetails.Enabled = True
            Me.ts_StatusDesc.Text = "Ready for viewing."
        End Try
    End Sub

    Private Async Sub btn_CollectionAndPaymentReport_Click(sender As Object, e As EventArgs) Handles btn_CollectionAndPaymentReport.Click
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

            Me.GB_Allocation.Enabled = False
            Me.ts_Progressbar.Visible = True
            Me.GB_ProformaEntries.Enabled = False
            Me.GB_ProformaEntriesDetails.Enabled = False
            Me.ts_Progressbar.Style = ProgressBarStyle.Marquee
            Me.ts_StatusDesc.Text = "Please wait while preparing CAP Summary Report."
            Await Task.Delay(1000)

            Await Task.Run(Sub() PaymntHelper.CreateCollAndPaySummReport(FilePath))

            Me.ts_StatusDesc.Text = "..."
            Await Task.Delay(1000)

            MessageBox.Show("Successfully exported please see in targeted path.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception            
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.GB_Allocation.Enabled = True
            Me.ts_Progressbar.Visible = False
            Me.GB_ProformaEntries.Enabled = True
            Me.GB_ProformaEntriesDetails.Enabled = True
            Me.ts_StatusDesc.Text = "Ready for viewing."
        End Try
    End Sub

    Private Async Sub btn_ORSummaryReport_Click(sender As Object, e As EventArgs) Handles btn_ORSummaryReport.Click
        Try
            Dim SelectedAllocationDate As AllocationDate = New AllocationDate(CDate(FormatDateTime(CDate(cbo_CollectionAllocDate.SelectedItem), DateFormat.ShortDate)), 0)
            Dim ViewReport As New frmReportViewer
            Dim _DT As New DSReport.CollectionDataTable
            Dim ORDT As New DSReport.CollectionDataTable

            Me.GB_Allocation.Enabled = False
            Me.GB_ProformaEntries.Enabled = False
            Me.GB_ProformaEntriesDetails.Enabled = False
            Me.ts_StatusDesc.Text = "Please wait while preparing OR Sumary Report."
            Await Task.Delay(1000)

            ORDT = Await Task.Run(Function() CType(PaymntHelper.GenerateORSummary(_DT), DSReport.CollectionDataTable))

            Me.ts_StatusDesc.Text = "..."
            Await Task.Delay(1000)
            With ViewReport
                .LoadPaymentORSummary(ORDT, SelectedAllocationDate.CollAllocationDate, SelectedAllocationDate.CollAllocationDate)
                .Show()
            End With
            ORDT = Nothing
        Catch ex As Exception            
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.GB_Allocation.Enabled = True
            Me.GB_ProformaEntries.Enabled = True
            Me.GB_ProformaEntriesDetails.Enabled = True
            Me.ts_StatusDesc.Text = "Ready for viewing."        
        End Try
    End Sub

    Private Async Sub btn_RFPSummaryReport_Click(sender As Object, e As EventArgs) Handles btn_RFPSummaryReport.Click
        Try
            Dim DS As New DataSet
            Dim tblRFPMain As New DSReport.RFPMainDataTable
            Dim tblRFPColl As New DSReport.RFPDetailsCollectionDataTable
            Dim tblRFPPay As New DSReport.RFPDetailsPaymentDataTable

            Me.GB_Allocation.Enabled = False
            Me.GB_ProformaEntries.Enabled = False
            Me.GB_ProformaEntriesDetails.Enabled = False
            Me.ts_StatusDesc.Text = "Please wait while preparing RFP Summary Report."
            Await Task.Delay(1000)

            DS = Await Task.Run(Function() PaymntHelper.GenerateRFP(tblRFPMain, tblRFPColl, tblRFPPay))            

            Me.ts_StatusDesc.Text = "..."
            Await Task.Delay(1000)

            Dim RPTViewer As New frmReportViewer
            With RPTViewer
                .LoadRFP(DS)                
                .ShowDialog()
            End With
            DS = Nothing
        Catch ex As Exception            
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.GB_Allocation.Enabled = True
            Me.GB_ProformaEntries.Enabled = True
            Me.GB_ProformaEntriesDetails.Enabled = True
            Me.ts_StatusDesc.Text = "Ready for viewing."
        End Try
    End Sub

    Private Async Sub btn_FTFReport_Click(sender As Object, e As EventArgs) Handles btn_FTFReport.Click
        Try
            Dim _frmPaymentNewFTF As New frmPaymentNewFTF
            Dim Signatory = WBillHelper.GetSignatories("FTF").First()
            Dim Signatory2 = WBillHelper.GetSignatories("FTF2").First()

            Me.GB_Allocation.Enabled = False
            Me.GB_ProformaEntries.Enabled = False
            Me.GB_ProformaEntriesDetails.Enabled = False
            Me.ts_StatusDesc.Text = "Extracting Payment FTF report."
            Await Task.Delay(1000)

            With _frmPaymentNewFTF
                ._PymtHelper = PaymntHelper
                ._Signatory = Signatory
                ._Signatory2 = Signatory2
                .ShowDialog()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.GB_Allocation.Enabled = True
            Me.GB_ProformaEntries.Enabled = True
            Me.GB_ProformaEntriesDetails.Enabled = True
            Me.ts_StatusDesc.Text = "Ready for viewing."
        End Try
    End Sub

    Private Async Sub btn_DeferredPaymentReport_Click(sender As Object, e As EventArgs) Handles btn_DeferredPaymentReport.Click
        Try
            Dim RPTViewer As New frmReportViewer
            Dim DSReprotDT As New DSReport.DeferredMonitoringDataTable
            Dim DT As New DSReport.DeferredMonitoringDataTable

            Me.GB_Allocation.Enabled = False
            Me.GB_ProformaEntries.Enabled = False
            Me.GB_ProformaEntriesDetails.Enabled = False
            Me.ts_StatusDesc.Text = "Please wait while preparing Deferred Payment Report."
            Await Task.Delay(1000)

            DT = Await Task.Run(Function() CType(PaymntHelper.GenerateDeferredPaymentsDT(DSReprotDT), DSReport.DeferredMonitoringDataTable))

            Me.ts_StatusDesc.Text = "..."
            Await Task.Delay(1000)

            With RPTViewer
                .LoadDeferredMonitoringReport(DT)                
                .ShowDialog()
            End With
            DT = Nothing
        Catch ex As Exception            
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.GB_Allocation.Enabled = True
            Me.GB_ProformaEntries.Enabled = True
            Me.GB_ProformaEntriesDetails.Enabled = True
            Me.ts_StatusDesc.Text = "Ready for viewing."
        End Try
    End Sub

    Private Sub btn_Close_Click(sender As Object, e As EventArgs) Handles btn_Close.Click
        Me.Close()
    End Sub

    Private Sub dgv_OffsetMFAR_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        'Try
        '    If Not e.RowIndex = -1 Then
        '        Dim r As Integer = dgv_OffsetMFAR.CurrentRow.Index
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
        '            ._DetailsLabel = "Generated Accounts Receivables in Market Fees including VAT"
        '            ._OffsetToInvoiceDT = DT
        '            ._ARAPDT = DT2
        '            .ShowDialog()
        '        End With
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub dgv_OffsetEnergyAR_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        'Try
        '    If Not e.RowIndex = -1 Then
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
        '    MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub dgv_OffsetVATAR_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        'Try
        '    If Not e.RowIndex = -1 Then
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
        '    MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub dgv_EnergyAP_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        'Try
        '    If Not e.RowIndex = -1 Then
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
        '    MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub dgv_OffsetEnergyAP_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        'Try
        '    If Not e.RowIndex = -1 Then
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
        '    MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub dgv_OffsetVATAP_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        'Try
        '    If Not e.RowIndex = -1 Then
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
        '    MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub TC_Allocations_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles TC_Allocations.DrawItem
        'Firstly we'll define some parameters.
        Dim CurrentTab As TabPage = TC_Allocations.TabPages(e.Index)
        Dim ItemRect As Rectangle = TC_Allocations.GetTabRect(e.Index)
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
        If TC_Allocations.Alignment = TabAlignment.Left Or TC_Allocations.Alignment = TabAlignment.Right Then
            Dim RotateAngle As Single = 90
            If TC_Allocations.Alignment = TabAlignment.Left Then RotateAngle = 270
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

    Private Sub dgv_PaymentTransToPR_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv_PaymentTransToPR.CellMouseDoubleClick
        Try
            If e.RowIndex >= 0 Then
                Dim r As Integer = Me.dgv_PaymentTransToPR.CurrentRow.Index
                Dim frmViewer As New frmReportViewer()
                Dim IDNumber As String = dgv_PaymentTransToPR.Item(0, r).Value.ToString

                ProgressThread.Show("Please wait while processing OR Report.")
                Dim result = PaymntHelper.GenerateORFinPenReport(New DSReport.OfficialReceiptMainNewDataTable, IDNumber)

                If result.Rows.Count = 0 Then
                    Exit Sub
                End If

                With frmViewer
                    .LoadOR(result)
                    .ShowDialog()
                End With
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)        
        End Try
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ProgressThread.Close()
    End Sub


End Class