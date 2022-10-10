<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPayment
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
        Dim grpBox_PaymentAllocationDetails As System.Windows.Forms.GroupBox
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim grpBox_CollectionDetails As System.Windows.Forms.GroupBox
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgv_PaymentSummary = New System.Windows.Forms.DataGridView()
        Me.dgv_CollectionDetails = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tslbl_Energy = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslbl_DefaultInterest = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslbl_VAT = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslbl_UnpaidMF = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslbl_DeferredEnergy = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslbl_DeferredVAT = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslbl_GrandTotal = New System.Windows.Forms.ToolStripStatusLabel()
        Me.cbo_CollectionAllocDate = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txt_tDeferredApplied = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_NSSApplied = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_CollectionTotal = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txt_tDefaultCol = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_tVATCol = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_tEnergyCol = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmd_Close = New System.Windows.Forms.Button()
        Me.grpBox_Reports = New System.Windows.Forms.GroupBox()
        Me.cmd_ViewORSummary = New System.Windows.Forms.Button()
        Me.cmd_ViewAllocation = New System.Windows.Forms.Button()
        Me.cmd_ViewOffsetSummary = New System.Windows.Forms.Button()
        Me.cmd_ColPaymentSummary = New System.Windows.Forms.Button()
        Me.cmd_DMCMSummary = New System.Windows.Forms.Button()
        Me.cmd_PaymentSummary = New System.Windows.Forms.Button()
        Me.cmd_ViewEFTCheck = New System.Windows.Forms.Button()
        Me.cmd_ViewFTF = New System.Windows.Forms.Button()
        Me.cmd_ViewJV = New System.Windows.Forms.Button()
        Me.cmd_ViewPayment = New System.Windows.Forms.Button()
        Me.cmd_CommitAllocation = New System.Windows.Forms.Button()
        Me.cmd_Refresh = New System.Windows.Forms.Button()
        Me.cmd_Search = New System.Windows.Forms.Button()
        grpBox_PaymentAllocationDetails = New System.Windows.Forms.GroupBox()
        grpBox_CollectionDetails = New System.Windows.Forms.GroupBox()
        grpBox_PaymentAllocationDetails.SuspendLayout()
        CType(Me.dgv_PaymentSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        grpBox_CollectionDetails.SuspendLayout()
        CType(Me.dgv_CollectionDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grpBox_Reports.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpBox_PaymentAllocationDetails
        '
        grpBox_PaymentAllocationDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grpBox_PaymentAllocationDetails.Controls.Add(Me.dgv_PaymentSummary)
        grpBox_PaymentAllocationDetails.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        grpBox_PaymentAllocationDetails.ForeColor = System.Drawing.Color.Black
        grpBox_PaymentAllocationDetails.Location = New System.Drawing.Point(282, 357)
        grpBox_PaymentAllocationDetails.Name = "grpBox_PaymentAllocationDetails"
        grpBox_PaymentAllocationDetails.Size = New System.Drawing.Size(921, 266)
        grpBox_PaymentAllocationDetails.TabIndex = 30
        grpBox_PaymentAllocationDetails.TabStop = False
        grpBox_PaymentAllocationDetails.Text = "Payment Allocation Summary"
        '
        'dgv_PaymentSummary
        '
        Me.dgv_PaymentSummary.AllowUserToAddRows = False
        Me.dgv_PaymentSummary.AllowUserToDeleteRows = False
        Me.dgv_PaymentSummary.AllowUserToResizeColumns = False
        Me.dgv_PaymentSummary.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_PaymentSummary.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_PaymentSummary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_PaymentSummary.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_PaymentSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_PaymentSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_PaymentSummary.Location = New System.Drawing.Point(3, 18)
        Me.dgv_PaymentSummary.Name = "dgv_PaymentSummary"
        Me.dgv_PaymentSummary.ReadOnly = True
        Me.dgv_PaymentSummary.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgv_PaymentSummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgv_PaymentSummary.Size = New System.Drawing.Size(915, 245)
        Me.dgv_PaymentSummary.TabIndex = 0
        '
        'grpBox_CollectionDetails
        '
        grpBox_CollectionDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grpBox_CollectionDetails.Controls.Add(Me.dgv_CollectionDetails)
        grpBox_CollectionDetails.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        grpBox_CollectionDetails.ForeColor = System.Drawing.Color.Black
        grpBox_CollectionDetails.Location = New System.Drawing.Point(279, 16)
        grpBox_CollectionDetails.Name = "grpBox_CollectionDetails"
        grpBox_CollectionDetails.Size = New System.Drawing.Size(924, 252)
        grpBox_CollectionDetails.TabIndex = 40
        grpBox_CollectionDetails.TabStop = False
        grpBox_CollectionDetails.Text = "Collection For Allocation Summary"
        '
        'dgv_CollectionDetails
        '
        Me.dgv_CollectionDetails.AllowUserToAddRows = False
        Me.dgv_CollectionDetails.AllowUserToDeleteRows = False
        Me.dgv_CollectionDetails.AllowUserToResizeColumns = False
        Me.dgv_CollectionDetails.AllowUserToResizeRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Helvetica Narrow", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        Me.dgv_CollectionDetails.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_CollectionDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_CollectionDetails.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv_CollectionDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_CollectionDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_CollectionDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv_CollectionDetails.Location = New System.Drawing.Point(3, 18)
        Me.dgv_CollectionDetails.MultiSelect = False
        Me.dgv_CollectionDetails.Name = "dgv_CollectionDetails"
        Me.dgv_CollectionDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgv_CollectionDetails.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgv_CollectionDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_CollectionDetails.Size = New System.Drawing.Size(918, 231)
        Me.dgv_CollectionDetails.TabIndex = 19
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Select Allocation Date:"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(0, 17)
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.tslbl_Energy, Me.tslbl_DefaultInterest, Me.tslbl_VAT, Me.tslbl_UnpaidMF, Me.tslbl_DeferredEnergy, Me.tslbl_DeferredVAT, Me.tslbl_GrandTotal})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 629)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1221, 22)
        Me.StatusStrip1.TabIndex = 29
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'tslbl_Energy
        '
        Me.tslbl_Energy.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tslbl_Energy.Name = "tslbl_Energy"
        Me.tslbl_Energy.Size = New System.Drawing.Size(76, 17)
        Me.tslbl_Energy.Text = "Energy: 0.00"
        '
        'tslbl_DefaultInterest
        '
        Me.tslbl_DefaultInterest.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tslbl_DefaultInterest.Name = "tslbl_DefaultInterest"
        Me.tslbl_DefaultInterest.Size = New System.Drawing.Size(116, 17)
        Me.tslbl_DefaultInterest.Text = "DefaultInterest: 0.00"
        '
        'tslbl_VAT
        '
        Me.tslbl_VAT.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tslbl_VAT.Name = "tslbl_VAT"
        Me.tslbl_VAT.Size = New System.Drawing.Size(115, 17)
        Me.tslbl_VAT.Text = "VATOnEnergy: 0.00"
        '
        'tslbl_UnpaidMF
        '
        Me.tslbl_UnpaidMF.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tslbl_UnpaidMF.Name = "tslbl_UnpaidMF"
        Me.tslbl_UnpaidMF.Size = New System.Drawing.Size(101, 17)
        Me.tslbl_UnpaidMF.Text = "MarketFees: 0.00"
        '
        'tslbl_DeferredEnergy
        '
        Me.tslbl_DeferredEnergy.Name = "tslbl_DeferredEnergy"
        Me.tslbl_DeferredEnergy.Size = New System.Drawing.Size(120, 17)
        Me.tslbl_DeferredEnergy.Text = "Deferred-Energy: 0.00"
        '
        'tslbl_DeferredVAT
        '
        Me.tslbl_DeferredVAT.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tslbl_DeferredVAT.Name = "tslbl_DeferredVAT"
        Me.tslbl_DeferredVAT.Size = New System.Drawing.Size(112, 17)
        Me.tslbl_DeferredVAT.Text = "Deferred-VAT: 0.00"
        '
        'tslbl_GrandTotal
        '
        Me.tslbl_GrandTotal.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tslbl_GrandTotal.Name = "tslbl_GrandTotal"
        Me.tslbl_GrandTotal.Size = New System.Drawing.Size(101, 17)
        Me.tslbl_GrandTotal.Text = "Grand Total: 0.00"
        '
        'cbo_CollectionAllocDate
        '
        Me.cbo_CollectionAllocDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_CollectionAllocDate.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_CollectionAllocDate.FormattingEnabled = True
        Me.cbo_CollectionAllocDate.Location = New System.Drawing.Point(10, 37)
        Me.cbo_CollectionAllocDate.Name = "cbo_CollectionAllocDate"
        Me.cbo_CollectionAllocDate.Size = New System.Drawing.Size(233, 24)
        Me.cbo_CollectionAllocDate.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txt_tDeferredApplied)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txt_NSSApplied)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txt_CollectionTotal)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txt_tDefaultCol)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txt_tVATCol)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txt_tEnergyCol)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Location = New System.Drawing.Point(282, 274)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(921, 77)
        Me.GroupBox1.TabIndex = 41
        Me.GroupBox1.TabStop = False
        '
        'txt_tDeferredApplied
        '
        Me.txt_tDeferredApplied.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_tDeferredApplied.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_tDeferredApplied.ForeColor = System.Drawing.Color.Black
        Me.txt_tDeferredApplied.Location = New System.Drawing.Point(134, 42)
        Me.txt_tDeferredApplied.Name = "txt_tDeferredApplied"
        Me.txt_tDeferredApplied.ReadOnly = True
        Me.txt_tDeferredApplied.Size = New System.Drawing.Size(175, 23)
        Me.txt_tDeferredApplied.TabIndex = 45
        Me.txt_tDeferredApplied.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(7, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(119, 15)
        Me.Label3.TabIndex = 44
        Me.Label3.Text = "Total Deferred Applied:"
        '
        'txt_NSSApplied
        '
        Me.txt_NSSApplied.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_NSSApplied.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_NSSApplied.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_NSSApplied.ForeColor = System.Drawing.Color.Black
        Me.txt_NSSApplied.Location = New System.Drawing.Point(445, 42)
        Me.txt_NSSApplied.Name = "txt_NSSApplied"
        Me.txt_NSSApplied.ReadOnly = True
        Me.txt_NSSApplied.Size = New System.Drawing.Size(175, 24)
        Me.txt_NSSApplied.TabIndex = 43
        Me.txt_NSSApplied.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(381, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 15)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = "Total NSS:"
        '
        'txt_CollectionTotal
        '
        Me.txt_CollectionTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_CollectionTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_CollectionTotal.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_CollectionTotal.ForeColor = System.Drawing.Color.Black
        Me.txt_CollectionTotal.Location = New System.Drawing.Point(740, 42)
        Me.txt_CollectionTotal.Name = "txt_CollectionTotal"
        Me.txt_CollectionTotal.ReadOnly = True
        Me.txt_CollectionTotal.Size = New System.Drawing.Size(175, 24)
        Me.txt_CollectionTotal.TabIndex = 41
        Me.txt_CollectionTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(626, 45)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(85, 15)
        Me.Label8.TabIndex = 40
        Me.Label8.Text = "Total Collection:"
        '
        'txt_tDefaultCol
        '
        Me.txt_tDefaultCol.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_tDefaultCol.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_tDefaultCol.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_tDefaultCol.ForeColor = System.Drawing.Color.Black
        Me.txt_tDefaultCol.Location = New System.Drawing.Point(445, 13)
        Me.txt_tDefaultCol.Name = "txt_tDefaultCol"
        Me.txt_tDefaultCol.ReadOnly = True
        Me.txt_tDefaultCol.Size = New System.Drawing.Size(175, 23)
        Me.txt_tDefaultCol.TabIndex = 39
        Me.txt_tDefaultCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(319, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(122, 15)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = "Total Default Collection:"
        '
        'txt_tVATCol
        '
        Me.txt_tVATCol.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_tVATCol.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_tVATCol.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_tVATCol.ForeColor = System.Drawing.Color.Black
        Me.txt_tVATCol.Location = New System.Drawing.Point(740, 13)
        Me.txt_tVATCol.Name = "txt_tVATCol"
        Me.txt_tVATCol.ReadOnly = True
        Me.txt_tVATCol.Size = New System.Drawing.Size(175, 23)
        Me.txt_tVATCol.TabIndex = 37
        Me.txt_tVATCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(626, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(108, 15)
        Me.Label6.TabIndex = 36
        Me.Label6.Text = "Total VAT Collection:"
        '
        'txt_tEnergyCol
        '
        Me.txt_tEnergyCol.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_tEnergyCol.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_tEnergyCol.ForeColor = System.Drawing.Color.Black
        Me.txt_tEnergyCol.Location = New System.Drawing.Point(134, 13)
        Me.txt_tEnergyCol.Name = "txt_tEnergyCol"
        Me.txt_tEnergyCol.ReadOnly = True
        Me.txt_tEnergyCol.Size = New System.Drawing.Size(175, 23)
        Me.txt_tEnergyCol.TabIndex = 37
        Me.txt_tEnergyCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(7, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(121, 15)
        Me.Label5.TabIndex = 36
        Me.Label5.Text = "Total Energy Collection:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.cmd_Close)
        Me.GroupBox2.Controls.Add(Me.grpBox_Reports)
        Me.GroupBox2.Controls.Add(Me.cmd_ViewFTF)
        Me.GroupBox2.Controls.Add(Me.cmd_ViewJV)
        Me.GroupBox2.Controls.Add(Me.cmd_ViewPayment)
        Me.GroupBox2.Controls.Add(Me.cmd_CommitAllocation)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.cmd_Refresh)
        Me.GroupBox2.Controls.Add(Me.cbo_CollectionAllocDate)
        Me.GroupBox2.Controls.Add(Me.cmd_Search)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(261, 611)
        Me.GroupBox2.TabIndex = 42
        Me.GroupBox2.TabStop = False
        '
        'cmd_Close
        '
        Me.cmd_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmd_Close.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Close.Image = Global.AccountsManagementForms.My.Resources.Resources.close1
        Me.cmd_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Close.Location = New System.Drawing.Point(10, 567)
        Me.cmd_Close.Name = "cmd_Close"
        Me.cmd_Close.Size = New System.Drawing.Size(233, 38)
        Me.cmd_Close.TabIndex = 32
        Me.cmd_Close.Text = "Close"
        Me.cmd_Close.UseVisualStyleBackColor = True
        '
        'grpBox_Reports
        '
        Me.grpBox_Reports.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpBox_Reports.Controls.Add(Me.cmd_ViewORSummary)
        Me.grpBox_Reports.Controls.Add(Me.cmd_ViewAllocation)
        Me.grpBox_Reports.Controls.Add(Me.cmd_ViewOffsetSummary)
        Me.grpBox_Reports.Controls.Add(Me.cmd_ColPaymentSummary)
        Me.grpBox_Reports.Controls.Add(Me.cmd_DMCMSummary)
        Me.grpBox_Reports.Controls.Add(Me.cmd_PaymentSummary)
        Me.grpBox_Reports.Controls.Add(Me.cmd_ViewEFTCheck)
        Me.grpBox_Reports.Enabled = False
        Me.grpBox_Reports.Location = New System.Drawing.Point(6, 256)
        Me.grpBox_Reports.Name = "grpBox_Reports"
        Me.grpBox_Reports.Size = New System.Drawing.Size(243, 261)
        Me.grpBox_Reports.TabIndex = 15
        Me.grpBox_Reports.TabStop = False
        Me.grpBox_Reports.Text = "Reports:"
        '
        'cmd_ViewORSummary
        '
        Me.cmd_ViewORSummary.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmd_ViewORSummary.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ViewORSummary.Image = Global.AccountsManagementForms.My.Resources.Resources.report
        Me.cmd_ViewORSummary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_ViewORSummary.Location = New System.Drawing.Point(4, 159)
        Me.cmd_ViewORSummary.Name = "cmd_ViewORSummary"
        Me.cmd_ViewORSummary.Size = New System.Drawing.Size(233, 29)
        Me.cmd_ViewORSummary.TabIndex = 35
        Me.cmd_ViewORSummary.Text = "View OR Summary"
        Me.cmd_ViewORSummary.UseVisualStyleBackColor = True
        '
        'cmd_ViewAllocation
        '
        Me.cmd_ViewAllocation.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmd_ViewAllocation.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ViewAllocation.Image = Global.AccountsManagementForms.My.Resources.Resources.report
        Me.cmd_ViewAllocation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_ViewAllocation.Location = New System.Drawing.Point(4, 124)
        Me.cmd_ViewAllocation.Name = "cmd_ViewAllocation"
        Me.cmd_ViewAllocation.Size = New System.Drawing.Size(233, 29)
        Me.cmd_ViewAllocation.TabIndex = 29
        Me.cmd_ViewAllocation.Text = "View Allocation Summary"
        Me.cmd_ViewAllocation.UseVisualStyleBackColor = True
        '
        'cmd_ViewOffsetSummary
        '
        Me.cmd_ViewOffsetSummary.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmd_ViewOffsetSummary.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ViewOffsetSummary.Image = Global.AccountsManagementForms.My.Resources.Resources.report
        Me.cmd_ViewOffsetSummary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_ViewOffsetSummary.Location = New System.Drawing.Point(4, 89)
        Me.cmd_ViewOffsetSummary.Name = "cmd_ViewOffsetSummary"
        Me.cmd_ViewOffsetSummary.Size = New System.Drawing.Size(233, 29)
        Me.cmd_ViewOffsetSummary.TabIndex = 34
        Me.cmd_ViewOffsetSummary.Text = "View Offsetting Summary"
        Me.cmd_ViewOffsetSummary.UseVisualStyleBackColor = True
        '
        'cmd_ColPaymentSummary
        '
        Me.cmd_ColPaymentSummary.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmd_ColPaymentSummary.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ColPaymentSummary.Image = Global.AccountsManagementForms.My.Resources.Resources.report
        Me.cmd_ColPaymentSummary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_ColPaymentSummary.Location = New System.Drawing.Point(4, 54)
        Me.cmd_ColPaymentSummary.Name = "cmd_ColPaymentSummary"
        Me.cmd_ColPaymentSummary.Size = New System.Drawing.Size(233, 29)
        Me.cmd_ColPaymentSummary.TabIndex = 31
        Me.cmd_ColPaymentSummary.Text = "Collection and Payment Summary"
        Me.cmd_ColPaymentSummary.UseVisualStyleBackColor = True
        '
        'cmd_DMCMSummary
        '
        Me.cmd_DMCMSummary.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmd_DMCMSummary.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_DMCMSummary.Image = Global.AccountsManagementForms.My.Resources.Resources.report
        Me.cmd_DMCMSummary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_DMCMSummary.Location = New System.Drawing.Point(4, 19)
        Me.cmd_DMCMSummary.Name = "cmd_DMCMSummary"
        Me.cmd_DMCMSummary.Size = New System.Drawing.Size(233, 29)
        Me.cmd_DMCMSummary.TabIndex = 33
        Me.cmd_DMCMSummary.Text = "View DMCM Summary"
        Me.cmd_DMCMSummary.UseVisualStyleBackColor = True
        '
        'cmd_PaymentSummary
        '
        Me.cmd_PaymentSummary.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmd_PaymentSummary.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_PaymentSummary.Image = Global.AccountsManagementForms.My.Resources.Resources.report
        Me.cmd_PaymentSummary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_PaymentSummary.Location = New System.Drawing.Point(4, 194)
        Me.cmd_PaymentSummary.Name = "cmd_PaymentSummary"
        Me.cmd_PaymentSummary.Size = New System.Drawing.Size(233, 29)
        Me.cmd_PaymentSummary.TabIndex = 35
        Me.cmd_PaymentSummary.Text = "View Payment Summary"
        Me.cmd_PaymentSummary.UseVisualStyleBackColor = True
        '
        'cmd_ViewEFTCheck
        '
        Me.cmd_ViewEFTCheck.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmd_ViewEFTCheck.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ViewEFTCheck.Image = Global.AccountsManagementForms.My.Resources.Resources.report
        Me.cmd_ViewEFTCheck.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_ViewEFTCheck.Location = New System.Drawing.Point(4, 226)
        Me.cmd_ViewEFTCheck.Name = "cmd_ViewEFTCheck"
        Me.cmd_ViewEFTCheck.Size = New System.Drawing.Size(233, 29)
        Me.cmd_ViewEFTCheck.TabIndex = 31
        Me.cmd_ViewEFTCheck.Text = "View EFT/Check"
        Me.cmd_ViewEFTCheck.UseVisualStyleBackColor = True
        '
        'cmd_ViewFTF
        '
        Me.cmd_ViewFTF.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmd_ViewFTF.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ViewFTF.Image = Global.AccountsManagementForms.My.Resources.Resources.report
        Me.cmd_ViewFTF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_ViewFTF.Location = New System.Drawing.Point(6, 582)
        Me.cmd_ViewFTF.Name = "cmd_ViewFTF"
        Me.cmd_ViewFTF.Size = New System.Drawing.Size(237, 23)
        Me.cmd_ViewFTF.TabIndex = 35
        Me.cmd_ViewFTF.Text = "View Fund Transfer Forms"
        Me.cmd_ViewFTF.UseVisualStyleBackColor = True
        Me.cmd_ViewFTF.Visible = False
        '
        'cmd_ViewJV
        '
        Me.cmd_ViewJV.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmd_ViewJV.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ViewJV.Image = Global.AccountsManagementForms.My.Resources.Resources.report
        Me.cmd_ViewJV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_ViewJV.Location = New System.Drawing.Point(6, 582)
        Me.cmd_ViewJV.Name = "cmd_ViewJV"
        Me.cmd_ViewJV.Size = New System.Drawing.Size(237, 23)
        Me.cmd_ViewJV.TabIndex = 30
        Me.cmd_ViewJV.Text = "View Journal Voucher"
        Me.cmd_ViewJV.UseVisualStyleBackColor = True
        Me.cmd_ViewJV.Visible = False
        '
        'cmd_ViewPayment
        '
        Me.cmd_ViewPayment.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmd_ViewPayment.Enabled = False
        Me.cmd_ViewPayment.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ViewPayment.Image = Global.AccountsManagementForms.My.Resources.Resources.search
        Me.cmd_ViewPayment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_ViewPayment.Location = New System.Drawing.Point(10, 151)
        Me.cmd_ViewPayment.Name = "cmd_ViewPayment"
        Me.cmd_ViewPayment.Size = New System.Drawing.Size(233, 36)
        Me.cmd_ViewPayment.TabIndex = 28
        Me.cmd_ViewPayment.Text = "View Payment Allocation Details"
        Me.cmd_ViewPayment.UseVisualStyleBackColor = True
        '
        'cmd_CommitAllocation
        '
        Me.cmd_CommitAllocation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmd_CommitAllocation.Enabled = False
        Me.cmd_CommitAllocation.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_CommitAllocation.Image = Global.AccountsManagementForms.My.Resources.Resources.save
        Me.cmd_CommitAllocation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_CommitAllocation.Location = New System.Drawing.Point(10, 523)
        Me.cmd_CommitAllocation.Name = "cmd_CommitAllocation"
        Me.cmd_CommitAllocation.Size = New System.Drawing.Size(233, 38)
        Me.cmd_CommitAllocation.TabIndex = 31
        Me.cmd_CommitAllocation.Text = "Save Allocation"
        Me.cmd_CommitAllocation.UseVisualStyleBackColor = True
        '
        'cmd_Refresh
        '
        Me.cmd_Refresh.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmd_Refresh.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Refresh.Image = Global.AccountsManagementForms.My.Resources.Resources.Sync
        Me.cmd_Refresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Refresh.Location = New System.Drawing.Point(10, 109)
        Me.cmd_Refresh.Name = "cmd_Refresh"
        Me.cmd_Refresh.Size = New System.Drawing.Size(233, 36)
        Me.cmd_Refresh.TabIndex = 29
        Me.cmd_Refresh.Text = "Refresh"
        Me.cmd_Refresh.UseVisualStyleBackColor = True
        '
        'cmd_Search
        '
        Me.cmd_Search.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Search.Image = Global.AccountsManagementForms.My.Resources.Resources.execute1
        Me.cmd_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Search.Location = New System.Drawing.Point(10, 67)
        Me.cmd_Search.Name = "cmd_Search"
        Me.cmd_Search.Size = New System.Drawing.Size(233, 36)
        Me.cmd_Search.TabIndex = 14
        Me.cmd_Search.Text = "Allocate"
        Me.cmd_Search.UseVisualStyleBackColor = True
        '
        'frmPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1221, 651)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(grpBox_CollectionDetails)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(grpBox_PaymentAllocationDetails)
        Me.MinimumSize = New System.Drawing.Size(1237, 689)
        Me.Name = "frmPayment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Payment Allocation"
        grpBox_PaymentAllocationDetails.ResumeLayout(False)
        CType(Me.dgv_PaymentSummary, System.ComponentModel.ISupportInitialize).EndInit()
        grpBox_CollectionDetails.ResumeLayout(False)
        CType(Me.dgv_CollectionDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.grpBox_Reports.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents cmd_Close As System.Windows.Forms.Button
    Friend WithEvents cmd_Refresh As System.Windows.Forms.Button
    Friend WithEvents cmd_CommitAllocation As System.Windows.Forms.Button
    Friend WithEvents cmd_ViewPayment As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents cmd_Search As System.Windows.Forms.Button
    Friend WithEvents cbo_CollectionAllocDate As System.Windows.Forms.ComboBox
    Friend WithEvents dgv_CollectionDetails As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txt_tVATCol As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_tEnergyCol As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt_tDefaultCol As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dgv_PaymentSummary As System.Windows.Forms.DataGridView
    Friend WithEvents tslbl_GrandTotal As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tslbl_VAT As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tslbl_DefaultInterest As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tslbl_Energy As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tslbl_UnpaidMF As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tslbl_DeferredEnergy As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tslbl_DeferredVAT As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents cmd_ViewAllocation As System.Windows.Forms.Button
    Friend WithEvents cmd_ViewJV As System.Windows.Forms.Button
    Friend WithEvents cmd_DMCMSummary As System.Windows.Forms.Button
    Friend WithEvents cmd_ViewEFTCheck As System.Windows.Forms.Button
    Friend WithEvents txt_CollectionTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmd_ViewOffsetSummary As System.Windows.Forms.Button
    Friend WithEvents txt_NSSApplied As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmd_ViewFTF As System.Windows.Forms.Button
    Friend WithEvents cmd_ColPaymentSummary As System.Windows.Forms.Button
    Friend WithEvents txt_tDeferredApplied As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmd_PaymentSummary As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grpBox_Reports As System.Windows.Forms.GroupBox
    Friend WithEvents cmd_ViewORSummary As System.Windows.Forms.Button
End Class
