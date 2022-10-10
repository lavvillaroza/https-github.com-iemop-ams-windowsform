<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWBSAdjustmentPerBatchView
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWBSAdjustmentPerBatchView))
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TabCntrl_Main = New System.Windows.Forms.TabControl()
        Me.TP_WESMBill = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.dgv_WESMInvoices_AP = New System.Windows.Forms.DataGridView()
        Me.col_APBillingPeriodNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_APBillingBatchNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_APInvoiceNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_APDueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_APNewDueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_APBeginningBalance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_APEnding_Balance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_APAmountAdjusted = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_APCreatedDoc = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.dgv_WESMInvoices_AR = New System.Windows.Forms.DataGridView()
        Me.col_ARBillingPeriodNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_ARBillingBatchNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_ARInvoiceNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_ARDueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_ARNewDueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_ARBeginningBalance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_AREndingBalance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_ARAmountAdjusted = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_ARCreatedDoc = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtbox_TotalARAdj = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtbox_TotalAPAdj = New System.Windows.Forms.TextBox()
        Me.TP_WESMBillAdj = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dgv_AdjAPDummy = New System.Windows.Forms.DataGridView()
        Me.col_APBillingPeriodNo2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_APBillingBatchNo2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_APInvoceNo2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_APDueDate2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_APNewDueDate2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_APAmountAdj2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dgv_AdjARDummy = New System.Windows.Forms.DataGridView()
        Me.col_ARBillingPeriodNo2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_ARBillingBatchNo2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_ARInvoiceNo2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_ARDueDate2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_ARNewDueDate2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_ARAmountAdj2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txtbox_TotalARAmountAdj = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtbox_TotalAPAmountAdj = New System.Windows.Forms.TextBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.btn_JVSetup = New System.Windows.Forms.Button()
        Me.btn_Save = New System.Windows.Forms.Button()
        Me.btn_Back = New System.Windows.Forms.Button()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TabCntrl_Main.SuspendLayout()
        Me.TP_WESMBill.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        CType(Me.dgv_WESMInvoices_AP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox10.SuspendLayout()
        CType(Me.dgv_WESMInvoices_AR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TP_WESMBillAdj.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgv_AdjAPDummy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgv_AdjARDummy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.TabCntrl_Main, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Panel5, 0, 1)
        Me.TableLayoutPanel3.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(966, 544)
        Me.TableLayoutPanel3.TabIndex = 4
        '
        'TabCntrl_Main
        '
        Me.TabCntrl_Main.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabCntrl_Main.Controls.Add(Me.TP_WESMBill)
        Me.TabCntrl_Main.Controls.Add(Me.TP_WESMBillAdj)
        Me.TabCntrl_Main.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.TabCntrl_Main.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabCntrl_Main.Location = New System.Drawing.Point(3, 3)
        Me.TabCntrl_Main.Name = "TabCntrl_Main"
        Me.TabCntrl_Main.SelectedIndex = 0
        Me.TabCntrl_Main.Size = New System.Drawing.Size(960, 478)
        Me.TabCntrl_Main.TabIndex = 6
        '
        'TP_WESMBill
        '
        Me.TP_WESMBill.BackColor = System.Drawing.Color.White
        Me.TP_WESMBill.Controls.Add(Me.TableLayoutPanel8)
        Me.TP_WESMBill.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TP_WESMBill.Location = New System.Drawing.Point(4, 21)
        Me.TP_WESMBill.Name = "TP_WESMBill"
        Me.TP_WESMBill.Size = New System.Drawing.Size(952, 453)
        Me.TP_WESMBill.TabIndex = 0
        Me.TP_WESMBill.Text = "WESMBill Original"
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel8.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel8.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel8.ColumnCount = 1
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.GroupBox9, 0, 2)
        Me.TableLayoutPanel8.Controls.Add(Me.GroupBox10, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.Panel1, 0, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.Panel2, 0, 3)
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 4
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(946, 447)
        Me.TableLayoutPanel8.TabIndex = 0
        '
        'GroupBox9
        '
        Me.GroupBox9.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox9.Controls.Add(Me.dgv_WESMInvoices_AP)
        Me.GroupBox9.Controls.Add(Me.Label28)
        Me.GroupBox9.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox9.ForeColor = System.Drawing.Color.Black
        Me.GroupBox9.Location = New System.Drawing.Point(5, 227)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(936, 172)
        Me.GroupBox9.TabIndex = 4
        Me.GroupBox9.TabStop = False
        '
        'dgv_WESMInvoices_AP
        '
        Me.dgv_WESMInvoices_AP.AllowUserToAddRows = False
        Me.dgv_WESMInvoices_AP.AllowUserToDeleteRows = False
        Me.dgv_WESMInvoices_AP.AllowUserToResizeColumns = False
        Me.dgv_WESMInvoices_AP.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_WESMInvoices_AP.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_WESMInvoices_AP.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_WESMInvoices_AP.ColumnHeadersHeight = 30
        Me.dgv_WESMInvoices_AP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgv_WESMInvoices_AP.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_APBillingPeriodNo, Me.col_APBillingBatchNo, Me.col_APInvoiceNo, Me.col_APDueDate, Me.col_APNewDueDate, Me.col_APBeginningBalance, Me.col_APEnding_Balance, Me.col_APAmountAdjusted, Me.col_APCreatedDoc})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LemonChiffon
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_WESMInvoices_AP.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_WESMInvoices_AP.Location = New System.Drawing.Point(6, 19)
        Me.dgv_WESMInvoices_AP.Name = "dgv_WESMInvoices_AP"
        Me.dgv_WESMInvoices_AP.ReadOnly = True
        Me.dgv_WESMInvoices_AP.RowHeadersVisible = False
        Me.dgv_WESMInvoices_AP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_WESMInvoices_AP.Size = New System.Drawing.Size(924, 149)
        Me.dgv_WESMInvoices_AP.TabIndex = 23
        '
        'col_APBillingPeriodNo
        '
        Me.col_APBillingPeriodNo.HeaderText = "Billing Period No"
        Me.col_APBillingPeriodNo.Name = "col_APBillingPeriodNo"
        Me.col_APBillingPeriodNo.ReadOnly = True
        '
        'col_APBillingBatchNo
        '
        Me.col_APBillingBatchNo.HeaderText = "Billing Batch No"
        Me.col_APBillingBatchNo.Name = "col_APBillingBatchNo"
        Me.col_APBillingBatchNo.ReadOnly = True
        '
        'col_APInvoiceNo
        '
        Me.col_APInvoiceNo.HeaderText = "Invoice No"
        Me.col_APInvoiceNo.Name = "col_APInvoiceNo"
        Me.col_APInvoiceNo.ReadOnly = True
        '
        'col_APDueDate
        '
        Me.col_APDueDate.HeaderText = "DueDate"
        Me.col_APDueDate.Name = "col_APDueDate"
        Me.col_APDueDate.ReadOnly = True
        '
        'col_APNewDueDate
        '
        Me.col_APNewDueDate.HeaderText = "New Due Date"
        Me.col_APNewDueDate.Name = "col_APNewDueDate"
        Me.col_APNewDueDate.ReadOnly = True
        '
        'col_APBeginningBalance
        '
        Me.col_APBeginningBalance.HeaderText = "Beginning Balance"
        Me.col_APBeginningBalance.Name = "col_APBeginningBalance"
        Me.col_APBeginningBalance.ReadOnly = True
        '
        'col_APEnding_Balance
        '
        Me.col_APEnding_Balance.HeaderText = "Ending Balance"
        Me.col_APEnding_Balance.Name = "col_APEnding_Balance"
        Me.col_APEnding_Balance.ReadOnly = True
        '
        'col_APAmountAdjusted
        '
        Me.col_APAmountAdjusted.HeaderText = "Amount Adjusted"
        Me.col_APAmountAdjusted.Name = "col_APAmountAdjusted"
        Me.col_APAmountAdjusted.ReadOnly = True
        '
        'col_APCreatedDoc
        '
        Me.col_APCreatedDoc.HeaderText = "Created Document"
        Me.col_APCreatedDoc.Name = "col_APCreatedDoc"
        Me.col_APCreatedDoc.ReadOnly = True
        Me.col_APCreatedDoc.Width = 120
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label28.Location = New System.Drawing.Point(6, 2)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(112, 14)
        Me.Label28.TabIndex = 22
        Me.Label28.Text = "Accounts Payables:"
        '
        'GroupBox10
        '
        Me.GroupBox10.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox10.Controls.Add(Me.Label18)
        Me.GroupBox10.Controls.Add(Me.dgv_WESMInvoices_AR)
        Me.GroupBox10.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox10.ForeColor = System.Drawing.Color.Black
        Me.GroupBox10.Location = New System.Drawing.Point(5, 5)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(936, 172)
        Me.GroupBox10.TabIndex = 3
        Me.GroupBox10.TabStop = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(6, 2)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(122, 14)
        Me.Label18.TabIndex = 21
        Me.Label18.Text = "Account Receivables:"
        '
        'dgv_WESMInvoices_AR
        '
        Me.dgv_WESMInvoices_AR.AllowUserToAddRows = False
        Me.dgv_WESMInvoices_AR.AllowUserToDeleteRows = False
        Me.dgv_WESMInvoices_AR.AllowUserToResizeColumns = False
        Me.dgv_WESMInvoices_AR.AllowUserToResizeRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_WESMInvoices_AR.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_WESMInvoices_AR.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_WESMInvoices_AR.ColumnHeadersHeight = 30
        Me.dgv_WESMInvoices_AR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgv_WESMInvoices_AR.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_ARBillingPeriodNo, Me.col_ARBillingBatchNo, Me.col_ARInvoiceNo, Me.col_ARDueDate, Me.col_ARNewDueDate, Me.col_ARBeginningBalance, Me.col_AREndingBalance, Me.col_ARAmountAdjusted, Me.col_ARCreatedDoc})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LemonChiffon
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_WESMInvoices_AR.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgv_WESMInvoices_AR.Location = New System.Drawing.Point(6, 19)
        Me.dgv_WESMInvoices_AR.Name = "dgv_WESMInvoices_AR"
        Me.dgv_WESMInvoices_AR.ReadOnly = True
        Me.dgv_WESMInvoices_AR.RowHeadersVisible = False
        Me.dgv_WESMInvoices_AR.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_WESMInvoices_AR.Size = New System.Drawing.Size(924, 149)
        Me.dgv_WESMInvoices_AR.TabIndex = 2
        '
        'col_ARBillingPeriodNo
        '
        Me.col_ARBillingPeriodNo.HeaderText = "Billing Period No"
        Me.col_ARBillingPeriodNo.Name = "col_ARBillingPeriodNo"
        Me.col_ARBillingPeriodNo.ReadOnly = True
        '
        'col_ARBillingBatchNo
        '
        Me.col_ARBillingBatchNo.HeaderText = "Billing Batch No"
        Me.col_ARBillingBatchNo.Name = "col_ARBillingBatchNo"
        Me.col_ARBillingBatchNo.ReadOnly = True
        '
        'col_ARInvoiceNo
        '
        Me.col_ARInvoiceNo.HeaderText = "Invoice No"
        Me.col_ARInvoiceNo.Name = "col_ARInvoiceNo"
        Me.col_ARInvoiceNo.ReadOnly = True
        '
        'col_ARDueDate
        '
        Me.col_ARDueDate.HeaderText = "DueDate"
        Me.col_ARDueDate.Name = "col_ARDueDate"
        Me.col_ARDueDate.ReadOnly = True
        '
        'col_ARNewDueDate
        '
        Me.col_ARNewDueDate.HeaderText = "New Due Date"
        Me.col_ARNewDueDate.Name = "col_ARNewDueDate"
        Me.col_ARNewDueDate.ReadOnly = True
        '
        'col_ARBeginningBalance
        '
        Me.col_ARBeginningBalance.HeaderText = "Beginning Balance"
        Me.col_ARBeginningBalance.Name = "col_ARBeginningBalance"
        Me.col_ARBeginningBalance.ReadOnly = True
        '
        'col_AREndingBalance
        '
        Me.col_AREndingBalance.HeaderText = "Ending Balance"
        Me.col_AREndingBalance.Name = "col_AREndingBalance"
        Me.col_AREndingBalance.ReadOnly = True
        '
        'col_ARAmountAdjusted
        '
        Me.col_ARAmountAdjusted.HeaderText = "Amount Adjusted"
        Me.col_ARAmountAdjusted.Name = "col_ARAmountAdjusted"
        Me.col_ARAmountAdjusted.ReadOnly = True
        '
        'col_ARCreatedDoc
        '
        Me.col_ARCreatedDoc.HeaderText = "Created Document"
        Me.col_ARCreatedDoc.Name = "col_ARCreatedDoc"
        Me.col_ARCreatedDoc.ReadOnly = True
        Me.col_ARCreatedDoc.Width = 120
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.txtbox_TotalARAdj)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(5, 185)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(936, 34)
        Me.Panel1.TabIndex = 8
        '
        'txtbox_TotalARAdj
        '
        Me.txtbox_TotalARAdj.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtbox_TotalARAdj.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtbox_TotalARAdj.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbox_TotalARAdj.ForeColor = System.Drawing.Color.Black
        Me.txtbox_TotalARAdj.Location = New System.Drawing.Point(756, 8)
        Me.txtbox_TotalARAdj.Name = "txtbox_TotalARAdj"
        Me.txtbox_TotalARAdj.Size = New System.Drawing.Size(154, 20)
        Me.txtbox_TotalARAdj.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(633, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(122, 14)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Total AR Adjustment:"
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.txtbox_TotalAPAdj)
        Me.Panel2.Location = New System.Drawing.Point(5, 407)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(936, 35)
        Me.Panel2.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(633, 10)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(121, 14)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Total AP Adjustment:"
        '
        'txtbox_TotalAPAdj
        '
        Me.txtbox_TotalAPAdj.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtbox_TotalAPAdj.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtbox_TotalAPAdj.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbox_TotalAPAdj.ForeColor = System.Drawing.Color.Black
        Me.txtbox_TotalAPAdj.Location = New System.Drawing.Point(756, 7)
        Me.txtbox_TotalAPAdj.Name = "txtbox_TotalAPAdj"
        Me.txtbox_TotalAPAdj.Size = New System.Drawing.Size(154, 20)
        Me.txtbox_TotalAPAdj.TabIndex = 0
        '
        'TP_WESMBillAdj
        '
        Me.TP_WESMBillAdj.BackColor = System.Drawing.Color.White
        Me.TP_WESMBillAdj.Controls.Add(Me.TableLayoutPanel1)
        Me.TP_WESMBillAdj.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TP_WESMBillAdj.Location = New System.Drawing.Point(4, 21)
        Me.TP_WESMBillAdj.Name = "TP_WESMBillAdj"
        Me.TP_WESMBillAdj.Size = New System.Drawing.Size(952, 453)
        Me.TP_WESMBillAdj.TabIndex = 0
        Me.TP_WESMBillAdj.Text = "WESMBill Adjustment"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel3, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel4, 0, 3)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(926, 447)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.dgv_AdjAPDummy)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(5, 227)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(916, 172)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'dgv_AdjAPDummy
        '
        Me.dgv_AdjAPDummy.AllowUserToAddRows = False
        Me.dgv_AdjAPDummy.AllowUserToDeleteRows = False
        Me.dgv_AdjAPDummy.AllowUserToResizeRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_AdjAPDummy.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgv_AdjAPDummy.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_AdjAPDummy.ColumnHeadersHeight = 30
        Me.dgv_AdjAPDummy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgv_AdjAPDummy.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_APBillingPeriodNo2, Me.col_APBillingBatchNo2, Me.col_APInvoceNo2, Me.col_APDueDate2, Me.col_APNewDueDate2, Me.col_APAmountAdj2})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LemonChiffon
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_AdjAPDummy.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgv_AdjAPDummy.Location = New System.Drawing.Point(6, 19)
        Me.dgv_AdjAPDummy.Name = "dgv_AdjAPDummy"
        Me.dgv_AdjAPDummy.ReadOnly = True
        Me.dgv_AdjAPDummy.RowHeadersVisible = False
        Me.dgv_AdjAPDummy.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_AdjAPDummy.Size = New System.Drawing.Size(904, 149)
        Me.dgv_AdjAPDummy.TabIndex = 23
        '
        'col_APBillingPeriodNo2
        '
        Me.col_APBillingPeriodNo2.HeaderText = "Billing Period No"
        Me.col_APBillingPeriodNo2.Name = "col_APBillingPeriodNo2"
        Me.col_APBillingPeriodNo2.ReadOnly = True
        '
        'col_APBillingBatchNo2
        '
        Me.col_APBillingBatchNo2.HeaderText = "Billing Batch No"
        Me.col_APBillingBatchNo2.Name = "col_APBillingBatchNo2"
        Me.col_APBillingBatchNo2.ReadOnly = True
        '
        'col_APInvoceNo2
        '
        Me.col_APInvoceNo2.HeaderText = "Invoice No"
        Me.col_APInvoceNo2.Name = "col_APInvoceNo2"
        Me.col_APInvoceNo2.ReadOnly = True
        Me.col_APInvoceNo2.Width = 150
        '
        'col_APDueDate2
        '
        Me.col_APDueDate2.HeaderText = "DueDate"
        Me.col_APDueDate2.Name = "col_APDueDate2"
        Me.col_APDueDate2.ReadOnly = True
        '
        'col_APNewDueDate2
        '
        Me.col_APNewDueDate2.HeaderText = "New Due Date"
        Me.col_APNewDueDate2.Name = "col_APNewDueDate2"
        Me.col_APNewDueDate2.ReadOnly = True
        '
        'col_APAmountAdj2
        '
        Me.col_APAmountAdj2.HeaderText = "Amount Adjusted"
        Me.col_APAmountAdj2.Name = "col_APAmountAdj2"
        Me.col_APAmountAdj2.ReadOnly = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(6, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 14)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Accounts Payables:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.dgv_AdjARDummy)
        Me.GroupBox2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(5, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(916, 172)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(6, 2)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(122, 14)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Account Receivables:"
        '
        'dgv_AdjARDummy
        '
        Me.dgv_AdjARDummy.AllowUserToAddRows = False
        Me.dgv_AdjARDummy.AllowUserToDeleteRows = False
        Me.dgv_AdjARDummy.AllowUserToResizeRows = False
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_AdjARDummy.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        Me.dgv_AdjARDummy.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_AdjARDummy.ColumnHeadersHeight = 30
        Me.dgv_AdjARDummy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgv_AdjARDummy.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_ARBillingPeriodNo2, Me.col_ARBillingBatchNo2, Me.col_ARInvoiceNo2, Me.col_ARDueDate2, Me.col_ARNewDueDate2, Me.col_ARAmountAdj2})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LemonChiffon
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_AdjARDummy.DefaultCellStyle = DataGridViewCellStyle8
        Me.dgv_AdjARDummy.Location = New System.Drawing.Point(6, 19)
        Me.dgv_AdjARDummy.Name = "dgv_AdjARDummy"
        Me.dgv_AdjARDummy.ReadOnly = True
        Me.dgv_AdjARDummy.RowHeadersVisible = False
        Me.dgv_AdjARDummy.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_AdjARDummy.Size = New System.Drawing.Size(904, 149)
        Me.dgv_AdjARDummy.TabIndex = 2
        '
        'col_ARBillingPeriodNo2
        '
        Me.col_ARBillingPeriodNo2.HeaderText = "Billing Period No"
        Me.col_ARBillingPeriodNo2.Name = "col_ARBillingPeriodNo2"
        Me.col_ARBillingPeriodNo2.ReadOnly = True
        '
        'col_ARBillingBatchNo2
        '
        Me.col_ARBillingBatchNo2.HeaderText = "Billing Batch No"
        Me.col_ARBillingBatchNo2.Name = "col_ARBillingBatchNo2"
        Me.col_ARBillingBatchNo2.ReadOnly = True
        '
        'col_ARInvoiceNo2
        '
        Me.col_ARInvoiceNo2.HeaderText = "Invoice No"
        Me.col_ARInvoiceNo2.Name = "col_ARInvoiceNo2"
        Me.col_ARInvoiceNo2.ReadOnly = True
        Me.col_ARInvoiceNo2.Width = 150
        '
        'col_ARDueDate2
        '
        Me.col_ARDueDate2.HeaderText = "DueDate"
        Me.col_ARDueDate2.Name = "col_ARDueDate2"
        Me.col_ARDueDate2.ReadOnly = True
        '
        'col_ARNewDueDate2
        '
        Me.col_ARNewDueDate2.HeaderText = "New Due Date"
        Me.col_ARNewDueDate2.Name = "col_ARNewDueDate2"
        Me.col_ARNewDueDate2.ReadOnly = True
        '
        'col_ARAmountAdj2
        '
        Me.col_ARAmountAdj2.HeaderText = "Amount Adjusted"
        Me.col_ARAmountAdj2.Name = "col_ARAmountAdj2"
        Me.col_ARAmountAdj2.ReadOnly = True
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.Controls.Add(Me.txtbox_TotalARAmountAdj)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Location = New System.Drawing.Point(5, 185)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(916, 34)
        Me.Panel3.TabIndex = 8
        '
        'txtbox_TotalARAmountAdj
        '
        Me.txtbox_TotalARAmountAdj.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtbox_TotalARAmountAdj.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtbox_TotalARAmountAdj.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbox_TotalARAmountAdj.ForeColor = System.Drawing.Color.Black
        Me.txtbox_TotalARAmountAdj.Location = New System.Drawing.Point(756, 8)
        Me.txtbox_TotalARAmountAdj.Name = "txtbox_TotalARAmountAdj"
        Me.txtbox_TotalARAmountAdj.Size = New System.Drawing.Size(154, 20)
        Me.txtbox_TotalARAmountAdj.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(633, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(122, 14)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Total AR Adjustment:"
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.txtbox_TotalAPAmountAdj)
        Me.Panel4.Location = New System.Drawing.Point(5, 407)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(916, 35)
        Me.Panel4.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(633, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(121, 14)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Total AP Adjustment:"
        '
        'txtbox_TotalAPAmountAdj
        '
        Me.txtbox_TotalAPAmountAdj.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtbox_TotalAPAmountAdj.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtbox_TotalAPAmountAdj.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbox_TotalAPAmountAdj.ForeColor = System.Drawing.Color.Black
        Me.txtbox_TotalAPAmountAdj.Location = New System.Drawing.Point(756, 7)
        Me.txtbox_TotalAPAmountAdj.Name = "txtbox_TotalAPAmountAdj"
        Me.txtbox_TotalAPAmountAdj.Size = New System.Drawing.Size(154, 20)
        Me.txtbox_TotalAPAmountAdj.TabIndex = 0
        '
        'Panel5
        '
        Me.Panel5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.btn_JVSetup)
        Me.Panel5.Controls.Add(Me.btn_Save)
        Me.Panel5.Controls.Add(Me.btn_Back)
        Me.Panel5.Location = New System.Drawing.Point(3, 487)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(960, 54)
        Me.Panel5.TabIndex = 7
        '
        'btn_JVSetup
        '
        Me.btn_JVSetup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_JVSetup.BackColor = System.Drawing.Color.White
        Me.btn_JVSetup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btn_JVSetup.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_JVSetup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_JVSetup.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_JVSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_JVSetup.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_JVSetup.ForeColor = System.Drawing.Color.Black
        Me.btn_JVSetup.Image = CType(resources.GetObject("btn_JVSetup.Image"), System.Drawing.Image)
        Me.btn_JVSetup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_JVSetup.Location = New System.Drawing.Point(513, 8)
        Me.btn_JVSetup.Name = "btn_JVSetup"
        Me.btn_JVSetup.Size = New System.Drawing.Size(143, 39)
        Me.btn_JVSetup.TabIndex = 3
        Me.btn_JVSetup.Text = "JV Setup"
        Me.btn_JVSetup.UseVisualStyleBackColor = False
        '
        'btn_Save
        '
        Me.btn_Save.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Save.BackColor = System.Drawing.Color.White
        Me.btn_Save.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Save.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Save.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Save.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Save.ForeColor = System.Drawing.Color.Black
        Me.btn_Save.Image = Global.AccountsManagementForms.My.Resources.Resources.SaveIconColored22x22
        Me.btn_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Save.Location = New System.Drawing.Point(661, 8)
        Me.btn_Save.Name = "btn_Save"
        Me.btn_Save.Size = New System.Drawing.Size(143, 39)
        Me.btn_Save.TabIndex = 8
        Me.btn_Save.Text = "Save"
        Me.btn_Save.UseVisualStyleBackColor = False
        '
        'btn_Back
        '
        Me.btn_Back.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Back.BackColor = System.Drawing.Color.White
        Me.btn_Back.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Back.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Back.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Back.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Back.ForeColor = System.Drawing.Color.Black
        Me.btn_Back.Image = Global.AccountsManagementForms.My.Resources.Resources.BackRedIcon22x22
        Me.btn_Back.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Back.Location = New System.Drawing.Point(809, 8)
        Me.btn_Back.Name = "btn_Back"
        Me.btn_Back.Size = New System.Drawing.Size(143, 39)
        Me.btn_Back.TabIndex = 9
        Me.btn_Back.Text = "Back"
        Me.btn_Back.UseVisualStyleBackColor = False
        '
        'frmWESMBillSummaryAdjustmentPerBatchView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(970, 549)
        Me.Controls.Add(Me.TableLayoutPanel3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmWESMBillSummaryAdjustmentPerBatchView"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Withholding TAX Adjustment (View)"
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TabCntrl_Main.ResumeLayout(False)
        Me.TP_WESMBill.ResumeLayout(False)
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        CType(Me.dgv_WESMInvoices_AP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        CType(Me.dgv_WESMInvoices_AR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.TP_WESMBillAdj.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgv_AdjAPDummy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dgv_AdjARDummy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TabCntrl_Main As System.Windows.Forms.TabControl
    Friend WithEvents TP_WESMBill As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel8 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents dgv_WESMInvoices_AR As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtbox_TotalARAdj As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtbox_TotalAPAdj As System.Windows.Forms.TextBox
    Friend WithEvents TP_WESMBillAdj As System.Windows.Forms.TabPage
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents btn_JVSetup As System.Windows.Forms.Button
    Friend WithEvents btn_Save As System.Windows.Forms.Button
    Friend WithEvents btn_Back As System.Windows.Forms.Button
    Friend WithEvents dgv_WESMInvoices_AP As System.Windows.Forms.DataGridView
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dgv_AdjAPDummy As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dgv_AdjARDummy As System.Windows.Forms.DataGridView
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents txtbox_TotalARAmountAdj As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtbox_TotalAPAmountAdj As System.Windows.Forms.TextBox
    Friend WithEvents col_APBillingPeriodNo2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_APBillingBatchNo2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_APInvoceNo2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_APDueDate2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_APNewDueDate2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_APAmountAdj2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ARBillingPeriodNo2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ARBillingBatchNo2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ARInvoiceNo2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ARDueDate2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ARNewDueDate2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ARAmountAdj2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_APBillingPeriodNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_APBillingBatchNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_APInvoiceNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_APDueDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_APNewDueDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_APBeginningBalance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_APEnding_Balance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_APAmountAdjusted As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_APCreatedDoc As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents col_ARBillingPeriodNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ARBillingBatchNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ARInvoiceNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ARDueDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ARNewDueDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ARBeginningBalance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_AREndingBalance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ARAmountAdjusted As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ARCreatedDoc As System.Windows.Forms.DataGridViewLinkColumn
End Class
