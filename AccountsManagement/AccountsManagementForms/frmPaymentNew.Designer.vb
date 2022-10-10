<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymentNew
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim GroupBox31 As System.Windows.Forms.GroupBox
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPaymentNew))
        Me.Label75 = New System.Windows.Forms.Label()
        Me.dgv_PaymentTransToPR = New System.Windows.Forms.DataGridView()
        Me.ToolTipOnPaymentForm = New System.Windows.Forms.ToolTip(Me.components)
        Me.Txtbox_TotalEnergy = New System.Windows.Forms.TextBox()
        Me.Txtbox_TotalPRReplenishment = New System.Windows.Forms.TextBox()
        Me.Txtbox_GrandTotal = New System.Windows.Forms.TextBox()
        Me.Txtbox_TotalVAT = New System.Windows.Forms.TextBox()
        Me.Txtbox_TotalRemittance = New System.Windows.Forms.TextBox()
        Me.Txtbox_TotalMF = New System.Windows.Forms.TextBox()
        Me.Txtbox_TotalExcessCollection = New System.Windows.Forms.TextBox()
        Me.Txtbox_TotalDeferredVATonEnergy = New System.Windows.Forms.TextBox()
        Me.Txtbox_TotalDeferredEnergy = New System.Windows.Forms.TextBox()
        Me.Txtbox_TotalFinancialPenalty = New System.Windows.Forms.TextBox()
        Me.Txtbox_TotalOffsetDeferredVATonEnergy = New System.Windows.Forms.TextBox()
        Me.Txtbox_TotalOffsetDeferredEnergy = New System.Windows.Forms.TextBox()
        Me.cbo_CollectionAllocDate = New System.Windows.Forms.ComboBox()
        Me.GB_Allocation = New System.Windows.Forms.GroupBox()
        Me.btn_Calculate = New System.Windows.Forms.Button()
        Me.GB_ProformaEntries = New System.Windows.Forms.GroupBox()
        Me.Btn_GeneratePaymentEFTandCheck = New System.Windows.Forms.Button()
        Me.Btn_GenerateJVPaymentAlloc = New System.Windows.Forms.Button()
        Me.Btn_GenerateJVPayment = New System.Windows.Forms.Button()
        Me.GB_ProformaEntriesDetails = New System.Windows.Forms.GroupBox()
        Me.btn_DeferredPaymentReport = New System.Windows.Forms.Button()
        Me.btn_FTFReport = New System.Windows.Forms.Button()
        Me.btn_RFPSummaryReport = New System.Windows.Forms.Button()
        Me.btn_ORSummaryReport = New System.Windows.Forms.Button()
        Me.btn_CollectionAndPaymentReport = New System.Windows.Forms.Button()
        Me.Btn_GenerateDMCMSummaryReport = New System.Windows.Forms.Button()
        Me.mainTLPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.MenuTLPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.GB_CntButtons = New System.Windows.Forms.GroupBox()
        Me.btn_Close = New System.Windows.Forms.Button()
        Me.btn_SaveAllocation = New System.Windows.Forms.Button()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.TC_Allocations = New System.Windows.Forms.TabControl()
        Me.TP_EFT = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox32 = New System.Windows.Forms.GroupBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ctrl_statusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatus_LabelMsg = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabelMsg2 = New System.Windows.Forms.ToolStripStatusLabel()
        GroupBox31 = New System.Windows.Forms.GroupBox()
        GroupBox31.SuspendLayout()
        CType(Me.dgv_PaymentTransToPR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GB_Allocation.SuspendLayout()
        Me.GB_ProformaEntries.SuspendLayout()
        Me.GB_ProformaEntriesDetails.SuspendLayout()
        Me.mainTLPanel.SuspendLayout()
        Me.MenuTLPanel.SuspendLayout()
        Me.GB_CntButtons.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        Me.TC_Allocations.SuspendLayout()
        Me.TP_EFT.SuspendLayout()
        Me.TableLayoutPanel9.SuspendLayout()
        Me.GroupBox32.SuspendLayout()
        Me.ctrl_statusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox31
        '
        GroupBox31.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        GroupBox31.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        GroupBox31.Controls.Add(Me.Label75)
        GroupBox31.Controls.Add(Me.dgv_PaymentTransToPR)
        GroupBox31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        GroupBox31.ForeColor = System.Drawing.Color.Black
        GroupBox31.Location = New System.Drawing.Point(5, 5)
        GroupBox31.Name = "GroupBox31"
        GroupBox31.Size = New System.Drawing.Size(928, 408)
        GroupBox31.TabIndex = 43
        GroupBox31.TabStop = False
        '
        'Label75
        '
        Me.Label75.AutoSize = True
        Me.Label75.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label75.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label75.Location = New System.Drawing.Point(6, 0)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(124, 14)
        Me.Label75.TabIndex = 25
        Me.Label75.Text = "Transfer Payment To:"
        '
        'dgv_PaymentTransToPR
        '
        Me.dgv_PaymentTransToPR.AllowUserToAddRows = False
        Me.dgv_PaymentTransToPR.AllowUserToDeleteRows = False
        Me.dgv_PaymentTransToPR.AllowUserToOrderColumns = True
        Me.dgv_PaymentTransToPR.AllowUserToResizeRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_PaymentTransToPR.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv_PaymentTransToPR.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_PaymentTransToPR.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_PaymentTransToPR.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgv_PaymentTransToPR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LemonChiffon
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_PaymentTransToPR.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgv_PaymentTransToPR.Location = New System.Drawing.Point(4, 19)
        Me.dgv_PaymentTransToPR.Name = "dgv_PaymentTransToPR"
        Me.dgv_PaymentTransToPR.RowHeadersVisible = False
        Me.dgv_PaymentTransToPR.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgv_PaymentTransToPR.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_PaymentTransToPR.Size = New System.Drawing.Size(919, 384)
        Me.dgv_PaymentTransToPR.TabIndex = 19
        '
        'ToolTipOnPaymentForm
        '
        Me.ToolTipOnPaymentForm.AutomaticDelay = 100
        Me.ToolTipOnPaymentForm.AutoPopDelay = 1000
        Me.ToolTipOnPaymentForm.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.ToolTipOnPaymentForm.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.ToolTipOnPaymentForm.InitialDelay = 10
        Me.ToolTipOnPaymentForm.ReshowDelay = 1
        Me.ToolTipOnPaymentForm.ShowAlways = True
        Me.ToolTipOnPaymentForm.StripAmpersands = True
        Me.ToolTipOnPaymentForm.UseFading = False
        '
        'Txtbox_TotalEnergy
        '
        Me.Txtbox_TotalEnergy.BackColor = System.Drawing.SystemColors.Info
        Me.Txtbox_TotalEnergy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtbox_TotalEnergy.ForeColor = System.Drawing.Color.Black
        Me.Txtbox_TotalEnergy.Location = New System.Drawing.Point(159, 68)
        Me.Txtbox_TotalEnergy.Name = "Txtbox_TotalEnergy"
        Me.Txtbox_TotalEnergy.ReadOnly = True
        Me.Txtbox_TotalEnergy.Size = New System.Drawing.Size(150, 20)
        Me.Txtbox_TotalEnergy.TabIndex = 1
        Me.Txtbox_TotalEnergy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTipOnPaymentForm.SetToolTip(Me.Txtbox_TotalEnergy, "Total Amount of Energy")
        '
        'Txtbox_TotalPRReplenishment
        '
        Me.Txtbox_TotalPRReplenishment.BackColor = System.Drawing.SystemColors.Info
        Me.Txtbox_TotalPRReplenishment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtbox_TotalPRReplenishment.ForeColor = System.Drawing.Color.Black
        Me.Txtbox_TotalPRReplenishment.Location = New System.Drawing.Point(743, 68)
        Me.Txtbox_TotalPRReplenishment.Name = "Txtbox_TotalPRReplenishment"
        Me.Txtbox_TotalPRReplenishment.ReadOnly = True
        Me.Txtbox_TotalPRReplenishment.Size = New System.Drawing.Size(183, 20)
        Me.Txtbox_TotalPRReplenishment.TabIndex = 3
        Me.Txtbox_TotalPRReplenishment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTipOnPaymentForm.SetToolTip(Me.Txtbox_TotalPRReplenishment, "Total Amount for Replenishment")
        '
        'Txtbox_GrandTotal
        '
        Me.Txtbox_GrandTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Txtbox_GrandTotal.BackColor = System.Drawing.SystemColors.Info
        Me.Txtbox_GrandTotal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtbox_GrandTotal.ForeColor = System.Drawing.Color.Black
        Me.Txtbox_GrandTotal.Location = New System.Drawing.Point(740, 138)
        Me.Txtbox_GrandTotal.Name = "Txtbox_GrandTotal"
        Me.Txtbox_GrandTotal.ReadOnly = True
        Me.Txtbox_GrandTotal.Size = New System.Drawing.Size(184, 20)
        Me.Txtbox_GrandTotal.TabIndex = 8
        Me.Txtbox_GrandTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTipOnPaymentForm.SetToolTip(Me.Txtbox_GrandTotal, "Grand Total Amount")
        '
        'Txtbox_TotalVAT
        '
        Me.Txtbox_TotalVAT.BackColor = System.Drawing.SystemColors.Info
        Me.Txtbox_TotalVAT.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtbox_TotalVAT.ForeColor = System.Drawing.Color.Black
        Me.Txtbox_TotalVAT.Location = New System.Drawing.Point(312, 68)
        Me.Txtbox_TotalVAT.Name = "Txtbox_TotalVAT"
        Me.Txtbox_TotalVAT.ReadOnly = True
        Me.Txtbox_TotalVAT.Size = New System.Drawing.Size(150, 20)
        Me.Txtbox_TotalVAT.TabIndex = 10
        Me.Txtbox_TotalVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTipOnPaymentForm.SetToolTip(Me.Txtbox_TotalVAT, "Total Amount of VAT on Energy")
        '
        'Txtbox_TotalRemittance
        '
        Me.Txtbox_TotalRemittance.BackColor = System.Drawing.SystemColors.Info
        Me.Txtbox_TotalRemittance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtbox_TotalRemittance.ForeColor = System.Drawing.Color.Black
        Me.Txtbox_TotalRemittance.Location = New System.Drawing.Point(742, 114)
        Me.Txtbox_TotalRemittance.Name = "Txtbox_TotalRemittance"
        Me.Txtbox_TotalRemittance.ReadOnly = True
        Me.Txtbox_TotalRemittance.Size = New System.Drawing.Size(184, 20)
        Me.Txtbox_TotalRemittance.TabIndex = 14
        Me.Txtbox_TotalRemittance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTipOnPaymentForm.SetToolTip(Me.Txtbox_TotalRemittance, "Total Amount for Remittance")
        '
        'Txtbox_TotalMF
        '
        Me.Txtbox_TotalMF.BackColor = System.Drawing.SystemColors.Info
        Me.Txtbox_TotalMF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtbox_TotalMF.ForeColor = System.Drawing.Color.Black
        Me.Txtbox_TotalMF.Location = New System.Drawing.Point(6, 68)
        Me.Txtbox_TotalMF.Name = "Txtbox_TotalMF"
        Me.Txtbox_TotalMF.ReadOnly = True
        Me.Txtbox_TotalMF.Size = New System.Drawing.Size(150, 20)
        Me.Txtbox_TotalMF.TabIndex = 16
        Me.Txtbox_TotalMF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTipOnPaymentForm.SetToolTip(Me.Txtbox_TotalMF, "Total Amount of Market Fees")
        '
        'Txtbox_TotalExcessCollection
        '
        Me.Txtbox_TotalExcessCollection.BackColor = System.Drawing.SystemColors.Info
        Me.Txtbox_TotalExcessCollection.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtbox_TotalExcessCollection.ForeColor = System.Drawing.Color.Black
        Me.Txtbox_TotalExcessCollection.Location = New System.Drawing.Point(6, 29)
        Me.Txtbox_TotalExcessCollection.Name = "Txtbox_TotalExcessCollection"
        Me.Txtbox_TotalExcessCollection.ReadOnly = True
        Me.Txtbox_TotalExcessCollection.Size = New System.Drawing.Size(150, 20)
        Me.Txtbox_TotalExcessCollection.TabIndex = 18
        Me.Txtbox_TotalExcessCollection.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTipOnPaymentForm.SetToolTip(Me.Txtbox_TotalExcessCollection, "Total of Excess Collections")
        '
        'Txtbox_TotalDeferredVATonEnergy
        '
        Me.Txtbox_TotalDeferredVATonEnergy.BackColor = System.Drawing.SystemColors.Info
        Me.Txtbox_TotalDeferredVATonEnergy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtbox_TotalDeferredVATonEnergy.ForeColor = System.Drawing.Color.Black
        Me.Txtbox_TotalDeferredVATonEnergy.Location = New System.Drawing.Point(312, 29)
        Me.Txtbox_TotalDeferredVATonEnergy.Name = "Txtbox_TotalDeferredVATonEnergy"
        Me.Txtbox_TotalDeferredVATonEnergy.ReadOnly = True
        Me.Txtbox_TotalDeferredVATonEnergy.Size = New System.Drawing.Size(150, 20)
        Me.Txtbox_TotalDeferredVATonEnergy.TabIndex = 19
        Me.Txtbox_TotalDeferredVATonEnergy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTipOnPaymentForm.SetToolTip(Me.Txtbox_TotalDeferredVATonEnergy, "Total Amount of Previous Deferred on VAT on Energy")
        '
        'Txtbox_TotalDeferredEnergy
        '
        Me.Txtbox_TotalDeferredEnergy.BackColor = System.Drawing.SystemColors.Info
        Me.Txtbox_TotalDeferredEnergy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtbox_TotalDeferredEnergy.ForeColor = System.Drawing.Color.Black
        Me.Txtbox_TotalDeferredEnergy.Location = New System.Drawing.Point(159, 29)
        Me.Txtbox_TotalDeferredEnergy.Name = "Txtbox_TotalDeferredEnergy"
        Me.Txtbox_TotalDeferredEnergy.ReadOnly = True
        Me.Txtbox_TotalDeferredEnergy.Size = New System.Drawing.Size(150, 20)
        Me.Txtbox_TotalDeferredEnergy.TabIndex = 20
        Me.Txtbox_TotalDeferredEnergy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTipOnPaymentForm.SetToolTip(Me.Txtbox_TotalDeferredEnergy, "Total Amount of Previous Deferred on Energy")
        '
        'Txtbox_TotalFinancialPenalty
        '
        Me.Txtbox_TotalFinancialPenalty.BackColor = System.Drawing.SystemColors.Info
        Me.Txtbox_TotalFinancialPenalty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtbox_TotalFinancialPenalty.ForeColor = System.Drawing.Color.Black
        Me.Txtbox_TotalFinancialPenalty.Location = New System.Drawing.Point(743, 90)
        Me.Txtbox_TotalFinancialPenalty.Name = "Txtbox_TotalFinancialPenalty"
        Me.Txtbox_TotalFinancialPenalty.ReadOnly = True
        Me.Txtbox_TotalFinancialPenalty.Size = New System.Drawing.Size(182, 20)
        Me.Txtbox_TotalFinancialPenalty.TabIndex = 22
        Me.Txtbox_TotalFinancialPenalty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTipOnPaymentForm.SetToolTip(Me.Txtbox_TotalFinancialPenalty, "Total Amount for Financial Penalty")
        '
        'Txtbox_TotalOffsetDeferredVATonEnergy
        '
        Me.Txtbox_TotalOffsetDeferredVATonEnergy.BackColor = System.Drawing.SystemColors.Info
        Me.Txtbox_TotalOffsetDeferredVATonEnergy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtbox_TotalOffsetDeferredVATonEnergy.ForeColor = System.Drawing.Color.Black
        Me.Txtbox_TotalOffsetDeferredVATonEnergy.Location = New System.Drawing.Point(618, 29)
        Me.Txtbox_TotalOffsetDeferredVATonEnergy.Name = "Txtbox_TotalOffsetDeferredVATonEnergy"
        Me.Txtbox_TotalOffsetDeferredVATonEnergy.ReadOnly = True
        Me.Txtbox_TotalOffsetDeferredVATonEnergy.Size = New System.Drawing.Size(150, 20)
        Me.Txtbox_TotalOffsetDeferredVATonEnergy.TabIndex = 47
        Me.Txtbox_TotalOffsetDeferredVATonEnergy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTipOnPaymentForm.SetToolTip(Me.Txtbox_TotalOffsetDeferredVATonEnergy, "Total Amount for Financial Penalty")
        '
        'Txtbox_TotalOffsetDeferredEnergy
        '
        Me.Txtbox_TotalOffsetDeferredEnergy.BackColor = System.Drawing.SystemColors.Info
        Me.Txtbox_TotalOffsetDeferredEnergy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtbox_TotalOffsetDeferredEnergy.ForeColor = System.Drawing.Color.Black
        Me.Txtbox_TotalOffsetDeferredEnergy.Location = New System.Drawing.Point(465, 29)
        Me.Txtbox_TotalOffsetDeferredEnergy.Name = "Txtbox_TotalOffsetDeferredEnergy"
        Me.Txtbox_TotalOffsetDeferredEnergy.ReadOnly = True
        Me.Txtbox_TotalOffsetDeferredEnergy.Size = New System.Drawing.Size(150, 20)
        Me.Txtbox_TotalOffsetDeferredEnergy.TabIndex = 46
        Me.Txtbox_TotalOffsetDeferredEnergy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTipOnPaymentForm.SetToolTip(Me.Txtbox_TotalOffsetDeferredEnergy, "Total Amount for Replenishment")
        '
        'cbo_CollectionAllocDate
        '
        Me.cbo_CollectionAllocDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbo_CollectionAllocDate.BackColor = System.Drawing.Color.White
        Me.cbo_CollectionAllocDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_CollectionAllocDate.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cbo_CollectionAllocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_CollectionAllocDate.ForeColor = System.Drawing.Color.Black
        Me.cbo_CollectionAllocDate.FormattingEnabled = True
        Me.cbo_CollectionAllocDate.Location = New System.Drawing.Point(9, 18)
        Me.cbo_CollectionAllocDate.Name = "cbo_CollectionAllocDate"
        Me.cbo_CollectionAllocDate.Size = New System.Drawing.Size(173, 22)
        Me.cbo_CollectionAllocDate.TabIndex = 49
        '
        'GB_Allocation
        '
        Me.GB_Allocation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GB_Allocation.Controls.Add(Me.cbo_CollectionAllocDate)
        Me.GB_Allocation.Controls.Add(Me.btn_Calculate)
        Me.GB_Allocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GB_Allocation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GB_Allocation.Location = New System.Drawing.Point(3, 3)
        Me.GB_Allocation.Name = "GB_Allocation"
        Me.GB_Allocation.Size = New System.Drawing.Size(188, 91)
        Me.GB_Allocation.TabIndex = 51
        Me.GB_Allocation.TabStop = False
        Me.GB_Allocation.Text = "Select Collection Date:"
        '
        'btn_Calculate
        '
        Me.btn_Calculate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Calculate.BackColor = System.Drawing.Color.White
        Me.btn_Calculate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Calculate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Calculate.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Calculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Calculate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Calculate.ForeColor = System.Drawing.Color.Black
        Me.btn_Calculate.Image = Global.AccountsManagementForms.My.Resources.Resources.ProcessDataIcon22x22
        Me.btn_Calculate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Calculate.Location = New System.Drawing.Point(8, 46)
        Me.btn_Calculate.Name = "btn_Calculate"
        Me.btn_Calculate.Size = New System.Drawing.Size(174, 36)
        Me.btn_Calculate.TabIndex = 50
        Me.btn_Calculate.Text = "Allocate"
        Me.btn_Calculate.UseVisualStyleBackColor = False
        '
        'GB_ProformaEntries
        '
        Me.GB_ProformaEntries.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GB_ProformaEntries.Controls.Add(Me.Btn_GeneratePaymentEFTandCheck)
        Me.GB_ProformaEntries.Controls.Add(Me.Btn_GenerateJVPaymentAlloc)
        Me.GB_ProformaEntries.Controls.Add(Me.Btn_GenerateJVPayment)
        Me.GB_ProformaEntries.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GB_ProformaEntries.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GB_ProformaEntries.Location = New System.Drawing.Point(3, 100)
        Me.GB_ProformaEntries.Name = "GB_ProformaEntries"
        Me.GB_ProformaEntries.Size = New System.Drawing.Size(188, 146)
        Me.GB_ProformaEntries.TabIndex = 52
        Me.GB_ProformaEntries.TabStop = False
        Me.GB_ProformaEntries.Text = "Proforma Entries:"
        '
        'Btn_GeneratePaymentEFTandCheck
        '
        Me.Btn_GeneratePaymentEFTandCheck.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_GeneratePaymentEFTandCheck.BackColor = System.Drawing.Color.White
        Me.Btn_GeneratePaymentEFTandCheck.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.Btn_GeneratePaymentEFTandCheck.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.Btn_GeneratePaymentEFTandCheck.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.Btn_GeneratePaymentEFTandCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_GeneratePaymentEFTandCheck.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_GeneratePaymentEFTandCheck.ForeColor = System.Drawing.Color.Black
        Me.Btn_GeneratePaymentEFTandCheck.Image = CType(resources.GetObject("Btn_GeneratePaymentEFTandCheck.Image"), System.Drawing.Image)
        Me.Btn_GeneratePaymentEFTandCheck.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btn_GeneratePaymentEFTandCheck.Location = New System.Drawing.Point(9, 102)
        Me.Btn_GeneratePaymentEFTandCheck.Name = "Btn_GeneratePaymentEFTandCheck"
        Me.Btn_GeneratePaymentEFTandCheck.Size = New System.Drawing.Size(174, 36)
        Me.Btn_GeneratePaymentEFTandCheck.TabIndex = 54
        Me.Btn_GeneratePaymentEFTandCheck.Text = "   JV Payment EFT/Check"
        Me.Btn_GeneratePaymentEFTandCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Btn_GeneratePaymentEFTandCheck.UseVisualStyleBackColor = False
        '
        'Btn_GenerateJVPaymentAlloc
        '
        Me.Btn_GenerateJVPaymentAlloc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_GenerateJVPaymentAlloc.BackColor = System.Drawing.Color.White
        Me.Btn_GenerateJVPaymentAlloc.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.Btn_GenerateJVPaymentAlloc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.Btn_GenerateJVPaymentAlloc.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.Btn_GenerateJVPaymentAlloc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_GenerateJVPaymentAlloc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_GenerateJVPaymentAlloc.ForeColor = System.Drawing.Color.Black
        Me.Btn_GenerateJVPaymentAlloc.Image = CType(resources.GetObject("Btn_GenerateJVPaymentAlloc.Image"), System.Drawing.Image)
        Me.Btn_GenerateJVPaymentAlloc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btn_GenerateJVPaymentAlloc.Location = New System.Drawing.Point(8, 60)
        Me.Btn_GenerateJVPaymentAlloc.Name = "Btn_GenerateJVPaymentAlloc"
        Me.Btn_GenerateJVPaymentAlloc.Size = New System.Drawing.Size(174, 36)
        Me.Btn_GenerateJVPaymentAlloc.TabIndex = 52
        Me.Btn_GenerateJVPaymentAlloc.Text = "   JV Payment Allocation"
        Me.Btn_GenerateJVPaymentAlloc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Btn_GenerateJVPaymentAlloc.UseVisualStyleBackColor = False
        '
        'Btn_GenerateJVPayment
        '
        Me.Btn_GenerateJVPayment.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_GenerateJVPayment.BackColor = System.Drawing.Color.White
        Me.Btn_GenerateJVPayment.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.Btn_GenerateJVPayment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.Btn_GenerateJVPayment.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.Btn_GenerateJVPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_GenerateJVPayment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_GenerateJVPayment.ForeColor = System.Drawing.Color.Black
        Me.Btn_GenerateJVPayment.Image = CType(resources.GetObject("Btn_GenerateJVPayment.Image"), System.Drawing.Image)
        Me.Btn_GenerateJVPayment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btn_GenerateJVPayment.Location = New System.Drawing.Point(8, 18)
        Me.Btn_GenerateJVPayment.Name = "Btn_GenerateJVPayment"
        Me.Btn_GenerateJVPayment.Size = New System.Drawing.Size(174, 36)
        Me.Btn_GenerateJVPayment.TabIndex = 51
        Me.Btn_GenerateJVPayment.Text = "   JV Payment"
        Me.Btn_GenerateJVPayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Btn_GenerateJVPayment.UseVisualStyleBackColor = False
        '
        'GB_ProformaEntriesDetails
        '
        Me.GB_ProformaEntriesDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GB_ProformaEntriesDetails.Controls.Add(Me.btn_DeferredPaymentReport)
        Me.GB_ProformaEntriesDetails.Controls.Add(Me.btn_FTFReport)
        Me.GB_ProformaEntriesDetails.Controls.Add(Me.btn_RFPSummaryReport)
        Me.GB_ProformaEntriesDetails.Controls.Add(Me.btn_ORSummaryReport)
        Me.GB_ProformaEntriesDetails.Controls.Add(Me.btn_CollectionAndPaymentReport)
        Me.GB_ProformaEntriesDetails.Controls.Add(Me.Btn_GenerateDMCMSummaryReport)
        Me.GB_ProformaEntriesDetails.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GB_ProformaEntriesDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GB_ProformaEntriesDetails.Location = New System.Drawing.Point(3, 252)
        Me.GB_ProformaEntriesDetails.Name = "GB_ProformaEntriesDetails"
        Me.GB_ProformaEntriesDetails.Size = New System.Drawing.Size(188, 271)
        Me.GB_ProformaEntriesDetails.TabIndex = 55
        Me.GB_ProformaEntriesDetails.TabStop = False
        Me.GB_ProformaEntriesDetails.Text = "Proforma Entries Details:"
        '
        'btn_DeferredPaymentReport
        '
        Me.btn_DeferredPaymentReport.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_DeferredPaymentReport.BackColor = System.Drawing.Color.White
        Me.btn_DeferredPaymentReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_DeferredPaymentReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_DeferredPaymentReport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_DeferredPaymentReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_DeferredPaymentReport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_DeferredPaymentReport.ForeColor = System.Drawing.Color.Black
        Me.btn_DeferredPaymentReport.Image = CType(resources.GetObject("btn_DeferredPaymentReport.Image"), System.Drawing.Image)
        Me.btn_DeferredPaymentReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_DeferredPaymentReport.Location = New System.Drawing.Point(8, 226)
        Me.btn_DeferredPaymentReport.Name = "btn_DeferredPaymentReport"
        Me.btn_DeferredPaymentReport.Size = New System.Drawing.Size(174, 36)
        Me.btn_DeferredPaymentReport.TabIndex = 59
        Me.btn_DeferredPaymentReport.Text = "   Deferred Payment Report"
        Me.btn_DeferredPaymentReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_DeferredPaymentReport.UseVisualStyleBackColor = False
        '
        'btn_FTFReport
        '
        Me.btn_FTFReport.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_FTFReport.BackColor = System.Drawing.Color.White
        Me.btn_FTFReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_FTFReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_FTFReport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_FTFReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_FTFReport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_FTFReport.ForeColor = System.Drawing.Color.Black
        Me.btn_FTFReport.Image = CType(resources.GetObject("btn_FTFReport.Image"), System.Drawing.Image)
        Me.btn_FTFReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_FTFReport.Location = New System.Drawing.Point(8, 185)
        Me.btn_FTFReport.Name = "btn_FTFReport"
        Me.btn_FTFReport.Size = New System.Drawing.Size(174, 36)
        Me.btn_FTFReport.TabIndex = 58
        Me.btn_FTFReport.Text = "   FTF Report"
        Me.btn_FTFReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_FTFReport.UseVisualStyleBackColor = False
        '
        'btn_RFPSummaryReport
        '
        Me.btn_RFPSummaryReport.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_RFPSummaryReport.BackColor = System.Drawing.Color.White
        Me.btn_RFPSummaryReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_RFPSummaryReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_RFPSummaryReport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_RFPSummaryReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_RFPSummaryReport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_RFPSummaryReport.ForeColor = System.Drawing.Color.Black
        Me.btn_RFPSummaryReport.Image = CType(resources.GetObject("btn_RFPSummaryReport.Image"), System.Drawing.Image)
        Me.btn_RFPSummaryReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_RFPSummaryReport.Location = New System.Drawing.Point(8, 143)
        Me.btn_RFPSummaryReport.Name = "btn_RFPSummaryReport"
        Me.btn_RFPSummaryReport.Size = New System.Drawing.Size(174, 36)
        Me.btn_RFPSummaryReport.TabIndex = 57
        Me.btn_RFPSummaryReport.Text = "   RFP Summary Report"
        Me.btn_RFPSummaryReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_RFPSummaryReport.UseVisualStyleBackColor = False
        '
        'btn_ORSummaryReport
        '
        Me.btn_ORSummaryReport.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_ORSummaryReport.BackColor = System.Drawing.Color.White
        Me.btn_ORSummaryReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_ORSummaryReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_ORSummaryReport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_ORSummaryReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ORSummaryReport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ORSummaryReport.ForeColor = System.Drawing.Color.Black
        Me.btn_ORSummaryReport.Image = CType(resources.GetObject("btn_ORSummaryReport.Image"), System.Drawing.Image)
        Me.btn_ORSummaryReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_ORSummaryReport.Location = New System.Drawing.Point(8, 102)
        Me.btn_ORSummaryReport.Name = "btn_ORSummaryReport"
        Me.btn_ORSummaryReport.Size = New System.Drawing.Size(174, 36)
        Me.btn_ORSummaryReport.TabIndex = 56
        Me.btn_ORSummaryReport.Text = "   OR Summary Report"
        Me.btn_ORSummaryReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_ORSummaryReport.UseVisualStyleBackColor = False
        '
        'btn_CollectionAndPaymentReport
        '
        Me.btn_CollectionAndPaymentReport.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_CollectionAndPaymentReport.BackColor = System.Drawing.Color.White
        Me.btn_CollectionAndPaymentReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_CollectionAndPaymentReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_CollectionAndPaymentReport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_CollectionAndPaymentReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_CollectionAndPaymentReport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_CollectionAndPaymentReport.ForeColor = System.Drawing.Color.Black
        Me.btn_CollectionAndPaymentReport.Image = CType(resources.GetObject("btn_CollectionAndPaymentReport.Image"), System.Drawing.Image)
        Me.btn_CollectionAndPaymentReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_CollectionAndPaymentReport.Location = New System.Drawing.Point(8, 60)
        Me.btn_CollectionAndPaymentReport.Name = "btn_CollectionAndPaymentReport"
        Me.btn_CollectionAndPaymentReport.Size = New System.Drawing.Size(174, 36)
        Me.btn_CollectionAndPaymentReport.TabIndex = 55
        Me.btn_CollectionAndPaymentReport.Text = "   Collection And Payment Summary Report"
        Me.btn_CollectionAndPaymentReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_CollectionAndPaymentReport.UseVisualStyleBackColor = False
        '
        'Btn_GenerateDMCMSummaryReport
        '
        Me.Btn_GenerateDMCMSummaryReport.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_GenerateDMCMSummaryReport.BackColor = System.Drawing.Color.White
        Me.Btn_GenerateDMCMSummaryReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.Btn_GenerateDMCMSummaryReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.Btn_GenerateDMCMSummaryReport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.Btn_GenerateDMCMSummaryReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_GenerateDMCMSummaryReport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_GenerateDMCMSummaryReport.ForeColor = System.Drawing.Color.Black
        Me.Btn_GenerateDMCMSummaryReport.Image = CType(resources.GetObject("Btn_GenerateDMCMSummaryReport.Image"), System.Drawing.Image)
        Me.Btn_GenerateDMCMSummaryReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btn_GenerateDMCMSummaryReport.Location = New System.Drawing.Point(8, 18)
        Me.Btn_GenerateDMCMSummaryReport.Name = "Btn_GenerateDMCMSummaryReport"
        Me.Btn_GenerateDMCMSummaryReport.Size = New System.Drawing.Size(174, 36)
        Me.Btn_GenerateDMCMSummaryReport.TabIndex = 54
        Me.Btn_GenerateDMCMSummaryReport.Text = "   DMCM Summary Report"
        Me.Btn_GenerateDMCMSummaryReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Btn_GenerateDMCMSummaryReport.UseVisualStyleBackColor = False
        '
        'mainTLPanel
        '
        Me.mainTLPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.mainTLPanel.ColumnCount = 2
        Me.mainTLPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.mainTLPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.mainTLPanel.Controls.Add(Me.MenuTLPanel, 0, 0)
        Me.mainTLPanel.Controls.Add(Me.TableLayoutPanel10, 1, 0)
        Me.mainTLPanel.Location = New System.Drawing.Point(3, 3)
        Me.mainTLPanel.Name = "mainTLPanel"
        Me.mainTLPanel.RowCount = 1
        Me.mainTLPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.mainTLPanel.Size = New System.Drawing.Size(1163, 642)
        Me.mainTLPanel.TabIndex = 59
        '
        'MenuTLPanel
        '
        Me.MenuTLPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MenuTLPanel.ColumnCount = 1
        Me.MenuTLPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.MenuTLPanel.Controls.Add(Me.GB_ProformaEntries, 0, 1)
        Me.MenuTLPanel.Controls.Add(Me.GB_ProformaEntriesDetails, 0, 2)
        Me.MenuTLPanel.Controls.Add(Me.GB_Allocation, 0, 0)
        Me.MenuTLPanel.Controls.Add(Me.GB_CntButtons, 0, 3)
        Me.MenuTLPanel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuTLPanel.Location = New System.Drawing.Point(3, 3)
        Me.MenuTLPanel.Name = "MenuTLPanel"
        Me.MenuTLPanel.RowCount = 4
        Me.MenuTLPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 97.0!))
        Me.MenuTLPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 152.0!))
        Me.MenuTLPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 277.0!))
        Me.MenuTLPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.MenuTLPanel.Size = New System.Drawing.Size(194, 636)
        Me.MenuTLPanel.TabIndex = 0
        '
        'GB_CntButtons
        '
        Me.GB_CntButtons.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GB_CntButtons.Controls.Add(Me.btn_Close)
        Me.GB_CntButtons.Controls.Add(Me.btn_SaveAllocation)
        Me.GB_CntButtons.Location = New System.Drawing.Point(3, 529)
        Me.GB_CntButtons.Name = "GB_CntButtons"
        Me.GB_CntButtons.Size = New System.Drawing.Size(188, 104)
        Me.GB_CntButtons.TabIndex = 56
        Me.GB_CntButtons.TabStop = False
        '
        'btn_Close
        '
        Me.btn_Close.BackColor = System.Drawing.Color.White
        Me.btn_Close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Close.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Close.ForeColor = System.Drawing.Color.Black
        Me.btn_Close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btn_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Close.Location = New System.Drawing.Point(8, 58)
        Me.btn_Close.Name = "btn_Close"
        Me.btn_Close.Size = New System.Drawing.Size(174, 36)
        Me.btn_Close.TabIndex = 57
        Me.btn_Close.Text = "Close"
        Me.btn_Close.UseVisualStyleBackColor = False
        '
        'btn_SaveAllocation
        '
        Me.btn_SaveAllocation.BackColor = System.Drawing.Color.White
        Me.btn_SaveAllocation.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_SaveAllocation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_SaveAllocation.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_SaveAllocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_SaveAllocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_SaveAllocation.ForeColor = System.Drawing.Color.Black
        Me.btn_SaveAllocation.Image = Global.AccountsManagementForms.My.Resources.Resources.SaveIconColored22x22
        Me.btn_SaveAllocation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_SaveAllocation.Location = New System.Drawing.Point(8, 17)
        Me.btn_SaveAllocation.Name = "btn_SaveAllocation"
        Me.btn_SaveAllocation.Size = New System.Drawing.Size(174, 36)
        Me.btn_SaveAllocation.TabIndex = 56
        Me.btn_SaveAllocation.Text = "Save Allocation"
        Me.btn_SaveAllocation.UseVisualStyleBackColor = False
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel10.ColumnCount = 1
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.TC_Allocations, 0, 0)
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(203, 3)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 1
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 636.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(957, 636)
        Me.TableLayoutPanel10.TabIndex = 1
        '
        'TC_Allocations
        '
        Me.TC_Allocations.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TC_Allocations.Controls.Add(Me.TP_EFT)
        Me.TC_Allocations.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TC_Allocations.Location = New System.Drawing.Point(3, 3)
        Me.TC_Allocations.Name = "TC_Allocations"
        Me.TC_Allocations.SelectedIndex = 0
        Me.TC_Allocations.Size = New System.Drawing.Size(951, 630)
        Me.TC_Allocations.TabIndex = 1
        '
        'TP_EFT
        '
        Me.TP_EFT.BackColor = System.Drawing.Color.White
        Me.TP_EFT.Controls.Add(Me.TableLayoutPanel9)
        Me.TP_EFT.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TP_EFT.Location = New System.Drawing.Point(4, 25)
        Me.TP_EFT.Name = "TP_EFT"
        Me.TP_EFT.Padding = New System.Windows.Forms.Padding(3)
        Me.TP_EFT.Size = New System.Drawing.Size(943, 601)
        Me.TP_EFT.TabIndex = 9
        Me.TP_EFT.Text = "For Remittance"
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel9.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel9.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel9.ColumnCount = 1
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.Controls.Add(GroupBox31, 0, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.GroupBox32, 0, 1)
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 2
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(938, 598)
        Me.TableLayoutPanel9.TabIndex = 3
        '
        'GroupBox32
        '
        Me.GroupBox32.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox32.Controls.Add(Me.Label27)
        Me.GroupBox32.Controls.Add(Me.Label81)
        Me.GroupBox32.Controls.Add(Me.Txtbox_TotalOffsetDeferredVATonEnergy)
        Me.GroupBox32.Controls.Add(Me.Txtbox_TotalOffsetDeferredEnergy)
        Me.GroupBox32.Controls.Add(Me.Label80)
        Me.GroupBox32.Controls.Add(Me.Label79)
        Me.GroupBox32.Controls.Add(Me.Label78)
        Me.GroupBox32.Controls.Add(Me.Label77)
        Me.GroupBox32.Controls.Add(Me.Label76)
        Me.GroupBox32.Controls.Add(Me.Label15)
        Me.GroupBox32.Controls.Add(Me.Label14)
        Me.GroupBox32.Controls.Add(Me.Label13)
        Me.GroupBox32.Controls.Add(Me.Label8)
        Me.GroupBox32.Controls.Add(Me.Txtbox_TotalFinancialPenalty)
        Me.GroupBox32.Controls.Add(Me.Label1)
        Me.GroupBox32.Controls.Add(Me.Txtbox_TotalDeferredEnergy)
        Me.GroupBox32.Controls.Add(Me.Txtbox_TotalDeferredVATonEnergy)
        Me.GroupBox32.Controls.Add(Me.Txtbox_TotalExcessCollection)
        Me.GroupBox32.Controls.Add(Me.Txtbox_TotalMF)
        Me.GroupBox32.Controls.Add(Me.Txtbox_TotalRemittance)
        Me.GroupBox32.Controls.Add(Me.Txtbox_TotalVAT)
        Me.GroupBox32.Controls.Add(Me.Txtbox_GrandTotal)
        Me.GroupBox32.Controls.Add(Me.Txtbox_TotalPRReplenishment)
        Me.GroupBox32.Controls.Add(Me.Txtbox_TotalEnergy)
        Me.GroupBox32.Location = New System.Drawing.Point(5, 421)
        Me.GroupBox32.Name = "GroupBox32"
        Me.GroupBox32.Size = New System.Drawing.Size(928, 172)
        Me.GroupBox32.TabIndex = 45
        Me.GroupBox32.TabStop = False
        '
        'Label27
        '
        Me.Label27.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(613, 13)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(112, 14)
        Me.Label27.TabIndex = 49
        Me.Label27.Text = "Offet Deferred VAT:"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label81
        '
        Me.Label81.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label81.AutoSize = True
        Me.Label81.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label81.ForeColor = System.Drawing.Color.Black
        Me.Label81.Location = New System.Drawing.Point(460, 13)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(137, 14)
        Me.Label81.TabIndex = 48
        Me.Label81.Text = "Offset Deferred Energy:"
        Me.Label81.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label80
        '
        Me.Label80.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label80.AutoSize = True
        Me.Label80.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label80.ForeColor = System.Drawing.Color.Black
        Me.Label80.Location = New System.Drawing.Point(633, 116)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(101, 14)
        Me.Label80.TabIndex = 45
        Me.Label80.Text = "Total Remittance:"
        Me.Label80.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label79
        '
        Me.Label79.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label79.AutoSize = True
        Me.Label79.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label79.ForeColor = System.Drawing.Color.Black
        Me.Label79.Location = New System.Drawing.Point(633, 92)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(100, 14)
        Me.Label79.TabIndex = 44
        Me.Label79.Text = "Financial Penalty:"
        Me.Label79.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label78
        '
        Me.Label78.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label78.AutoSize = True
        Me.Label78.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label78.ForeColor = System.Drawing.Color.Black
        Me.Label78.Location = New System.Drawing.Point(306, 53)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(89, 14)
        Me.Label78.TabIndex = 43
        Me.Label78.Text = "VAT on Energy:"
        Me.Label78.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label77
        '
        Me.Label77.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label77.AutoSize = True
        Me.Label77.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label77.ForeColor = System.Drawing.Color.Black
        Me.Label77.Location = New System.Drawing.Point(154, 53)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(48, 14)
        Me.Label77.TabIndex = 42
        Me.Label77.Text = "Energy:"
        Me.Label77.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label76
        '
        Me.Label76.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label76.AutoSize = True
        Me.Label76.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label76.ForeColor = System.Drawing.Color.Black
        Me.Label76.Location = New System.Drawing.Point(1, 53)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(79, 14)
        Me.Label76.TabIndex = 41
        Me.Label76.Text = "Market Fees:"
        Me.Label76.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(624, 70)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(111, 14)
        Me.Label15.TabIndex = 40
        Me.Label15.Text = "PR Replenishment:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(307, 14)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(82, 14)
        Me.Label14.TabIndex = 39
        Me.Label14.Text = "Deferred VAT:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(154, 14)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(100, 14)
        Me.Label13.TabIndex = 38
        Me.Label13.Text = "Deferred Energy:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(1, 14)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(107, 14)
        Me.Label8.TabIndex = 37
        Me.Label8.Text = "Excess Collection:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(660, 140)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 14)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Grand Total:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ctrl_statusStrip
        '
        Me.ctrl_statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatus_LabelMsg, Me.ToolStripStatusLabelMsg2})
        Me.ctrl_statusStrip.Location = New System.Drawing.Point(0, 648)
        Me.ctrl_statusStrip.Name = "ctrl_statusStrip"
        Me.ctrl_statusStrip.Size = New System.Drawing.Size(1169, 22)
        Me.ctrl_statusStrip.TabIndex = 60
        Me.ctrl_statusStrip.Text = "StatusStrip1"
        '
        'ToolStripStatus_LabelMsg
        '
        Me.ToolStripStatus_LabelMsg.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ToolStripStatus_LabelMsg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripStatus_LabelMsg.Name = "ToolStripStatus_LabelMsg"
        Me.ToolStripStatus_LabelMsg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ToolStripStatus_LabelMsg.Size = New System.Drawing.Size(45, 17)
        Me.ToolStripStatus_LabelMsg.Text = "Ready.."
        Me.ToolStripStatus_LabelMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripStatus_LabelMsg.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'ToolStripStatusLabelMsg2
        '
        Me.ToolStripStatusLabelMsg2.Name = "ToolStripStatusLabelMsg2"
        Me.ToolStripStatusLabelMsg2.Size = New System.Drawing.Size(0, 17)
        '
        'frmPaymentNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1169, 670)
        Me.Controls.Add(Me.ctrl_statusStrip)
        Me.Controls.Add(Me.mainTLPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPaymentNew"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Payment"
        GroupBox31.ResumeLayout(False)
        GroupBox31.PerformLayout()
        CType(Me.dgv_PaymentTransToPR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GB_Allocation.ResumeLayout(False)
        Me.GB_ProformaEntries.ResumeLayout(False)
        Me.GB_ProformaEntriesDetails.ResumeLayout(False)
        Me.mainTLPanel.ResumeLayout(False)
        Me.MenuTLPanel.ResumeLayout(False)
        Me.GB_CntButtons.ResumeLayout(False)
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TC_Allocations.ResumeLayout(False)
        Me.TP_EFT.ResumeLayout(False)
        Me.TableLayoutPanel9.ResumeLayout(False)
        Me.GroupBox32.ResumeLayout(False)
        Me.GroupBox32.PerformLayout()
        Me.ctrl_statusStrip.ResumeLayout(False)
        Me.ctrl_statusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTipOnPaymentForm As System.Windows.Forms.ToolTip
    Friend WithEvents btn_Calculate As System.Windows.Forms.Button
    Friend WithEvents cbo_CollectionAllocDate As System.Windows.Forms.ComboBox
    Friend WithEvents GB_Allocation As System.Windows.Forms.GroupBox
    Friend WithEvents GB_ProformaEntries As System.Windows.Forms.GroupBox
    Friend WithEvents Btn_GenerateJVPaymentAlloc As System.Windows.Forms.Button
    Friend WithEvents Btn_GenerateJVPayment As System.Windows.Forms.Button
    Friend WithEvents Btn_GeneratePaymentEFTandCheck As System.Windows.Forms.Button
    Friend WithEvents Btn_GenerateDMCMSummaryReport As System.Windows.Forms.Button
    Friend WithEvents GB_ProformaEntriesDetails As System.Windows.Forms.GroupBox
    Friend WithEvents btn_SaveAllocation As System.Windows.Forms.Button
    Friend WithEvents btn_Close As System.Windows.Forms.Button
    Friend WithEvents btn_CollectionAndPaymentReport As System.Windows.Forms.Button
    Friend WithEvents btn_RFPSummaryReport As System.Windows.Forms.Button
    Friend WithEvents btn_ORSummaryReport As System.Windows.Forms.Button
    Friend WithEvents btn_DeferredPaymentReport As System.Windows.Forms.Button
    Friend WithEvents btn_FTFReport As System.Windows.Forms.Button
    Friend WithEvents mainTLPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TC_Allocations As System.Windows.Forms.TabControl
    Friend WithEvents TP_EFT As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel9 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents dgv_PaymentTransToPR As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox32 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Txtbox_TotalDeferredEnergy As System.Windows.Forms.TextBox
    Friend WithEvents Txtbox_TotalDeferredVATonEnergy As System.Windows.Forms.TextBox
    Friend WithEvents Txtbox_TotalExcessCollection As System.Windows.Forms.TextBox
    Friend WithEvents Txtbox_TotalMF As System.Windows.Forms.TextBox
    Friend WithEvents Txtbox_TotalRemittance As System.Windows.Forms.TextBox
    Friend WithEvents Txtbox_TotalVAT As System.Windows.Forms.TextBox
    Friend WithEvents Txtbox_GrandTotal As System.Windows.Forms.TextBox
    Friend WithEvents Txtbox_TotalPRReplenishment As System.Windows.Forms.TextBox
    Friend WithEvents Txtbox_TotalEnergy As System.Windows.Forms.TextBox
    Friend WithEvents MenuTLPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel10 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GB_CntButtons As System.Windows.Forms.GroupBox
    Friend WithEvents Txtbox_TotalFinancialPenalty As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents Txtbox_TotalOffsetDeferredVATonEnergy As System.Windows.Forms.TextBox
    Friend WithEvents Txtbox_TotalOffsetDeferredEnergy As System.Windows.Forms.TextBox
    Friend WithEvents ctrl_statusStrip As StatusStrip
    Friend WithEvents ToolStripStatus_LabelMsg As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabelMsg2 As ToolStripStatusLabel
End Class
