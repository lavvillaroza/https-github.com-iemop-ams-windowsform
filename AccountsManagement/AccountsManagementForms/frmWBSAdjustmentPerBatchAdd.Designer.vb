<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWBSAdjustmentPerBatchAdd
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.btn_Create = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.dgv_WBSWhTAX = New System.Windows.Forms.DataGridView()
        Me.InvoiceNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrigDueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NewDueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParticipantName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_WithholdingTAX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BeginningBalance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EndingBalance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AmountAdjusted = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtbox_APAmountAdjusted = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtbox_ARAmountAdjusted = New System.Windows.Forms.TextBox()
        Me.txtbox_TotalEndingBalance = New System.Windows.Forms.TextBox()
        Me.txtbox_TotalBeginningBalance = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmb_BillingPeriodNo = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmb_BillingBatchNo = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtbox_Remarks = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmb_BillingType = New System.Windows.Forms.ComboBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmb_ChargeType = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.dgv_WBSWhTAX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel7, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1046, 629)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Panel7
        '
        Me.Panel7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel7.Controls.Add(Me.btn_Cancel)
        Me.Panel7.Controls.Add(Me.btn_Create)
        Me.Panel7.Location = New System.Drawing.Point(3, 581)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1040, 45)
        Me.Panel7.TabIndex = 6
        '
        'btn_Cancel
        '
        Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Cancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Cancel.ForeColor = System.Drawing.Color.Black
        Me.btn_Cancel.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btn_Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Cancel.Location = New System.Drawing.Point(927, 4)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(110, 39)
        Me.btn_Cancel.TabIndex = 9
        Me.btn_Cancel.Text = "&Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'btn_Create
        '
        Me.btn_Create.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Create.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Create.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Create.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Create.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Create.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Create.ForeColor = System.Drawing.Color.Black
        Me.btn_Create.Image = Global.AccountsManagementForms.My.Resources.Resources.NewGreenIcon22x22
        Me.btn_Create.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Create.Location = New System.Drawing.Point(811, 4)
        Me.btn_Create.Name = "btn_Create"
        Me.btn_Create.Size = New System.Drawing.Size(110, 39)
        Me.btn_Create.TabIndex = 8
        Me.btn_Create.Text = "&Create"
        Me.btn_Create.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.dgv_WBSWhTAX, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Panel3, 0, 1)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 78)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1040, 497)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'dgv_WBSWhTAX
        '
        Me.dgv_WBSWhTAX.AllowUserToAddRows = False
        Me.dgv_WBSWhTAX.AllowUserToDeleteRows = False
        Me.dgv_WBSWhTAX.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_WBSWhTAX.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_WBSWhTAX.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_WBSWhTAX.ColumnHeadersHeight = 30
        Me.dgv_WBSWhTAX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgv_WBSWhTAX.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.InvoiceNumber, Me.OrigDueDate, Me.NewDueDate, Me.IDNumber, Me.ParticipantName, Me.col_WithholdingTAX, Me.BeginningBalance, Me.EndingBalance, Me.AmountAdjusted})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LemonChiffon
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_WBSWhTAX.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_WBSWhTAX.Location = New System.Drawing.Point(3, 3)
        Me.dgv_WBSWhTAX.Name = "dgv_WBSWhTAX"
        Me.dgv_WBSWhTAX.RowHeadersVisible = False
        Me.dgv_WBSWhTAX.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_WBSWhTAX.Size = New System.Drawing.Size(1034, 416)
        Me.dgv_WBSWhTAX.TabIndex = 9
        '
        'InvoiceNumber
        '
        Me.InvoiceNumber.HeaderText = "Invoice Number"
        Me.InvoiceNumber.Name = "InvoiceNumber"
        '
        'OrigDueDate
        '
        Me.OrigDueDate.HeaderText = "Original Due Date"
        Me.OrigDueDate.Name = "OrigDueDate"
        '
        'NewDueDate
        '
        Me.NewDueDate.HeaderText = "New Due Date"
        Me.NewDueDate.Name = "NewDueDate"
        '
        'IDNumber
        '
        Me.IDNumber.HeaderText = "ID Number"
        Me.IDNumber.Name = "IDNumber"
        Me.IDNumber.Width = 80
        '
        'ParticipantName
        '
        Me.ParticipantName.HeaderText = "Participant Name"
        Me.ParticipantName.Name = "ParticipantName"
        Me.ParticipantName.Width = 150
        '
        'col_WithholdingTAX
        '
        Me.col_WithholdingTAX.HeaderText = "WithholdingTAX"
        Me.col_WithholdingTAX.Name = "col_WithholdingTAX"
        '
        'BeginningBalance
        '
        Me.BeginningBalance.HeaderText = "Beginning Balance"
        Me.BeginningBalance.Name = "BeginningBalance"
        Me.BeginningBalance.Width = 150
        '
        'EndingBalance
        '
        Me.EndingBalance.HeaderText = "Ending Balance"
        Me.EndingBalance.Name = "EndingBalance"
        Me.EndingBalance.Width = 150
        '
        'AmountAdjusted
        '
        Me.AmountAdjusted.HeaderText = "Amount Adjusted"
        Me.AmountAdjusted.Name = "AmountAdjusted"
        Me.AmountAdjusted.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AmountAdjusted.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.txtbox_APAmountAdjusted)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.txtbox_ARAmountAdjusted)
        Me.Panel3.Controls.Add(Me.txtbox_TotalEndingBalance)
        Me.Panel3.Controls.Add(Me.txtbox_TotalBeginningBalance)
        Me.Panel3.Location = New System.Drawing.Point(3, 425)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1034, 69)
        Me.Panel3.TabIndex = 10
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(705, 38)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(135, 19)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Total AP Amount Adjusted:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtbox_APAmountAdjusted
        '
        Me.txtbox_APAmountAdjusted.BackColor = System.Drawing.SystemColors.Info
        Me.txtbox_APAmountAdjusted.Location = New System.Drawing.Point(843, 37)
        Me.txtbox_APAmountAdjusted.Name = "txtbox_APAmountAdjusted"
        Me.txtbox_APAmountAdjusted.ReadOnly = True
        Me.txtbox_APAmountAdjusted.Size = New System.Drawing.Size(136, 20)
        Me.txtbox_APAmountAdjusted.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(703, 12)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(137, 19)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Total AR Amount Adjusted:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(429, 12)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(117, 19)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Total Ending Balance:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(143, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(126, 19)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Total Beginning Balance:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtbox_ARAmountAdjusted
        '
        Me.txtbox_ARAmountAdjusted.BackColor = System.Drawing.SystemColors.Info
        Me.txtbox_ARAmountAdjusted.Location = New System.Drawing.Point(843, 11)
        Me.txtbox_ARAmountAdjusted.Name = "txtbox_ARAmountAdjusted"
        Me.txtbox_ARAmountAdjusted.ReadOnly = True
        Me.txtbox_ARAmountAdjusted.Size = New System.Drawing.Size(136, 20)
        Me.txtbox_ARAmountAdjusted.TabIndex = 2
        '
        'txtbox_TotalEndingBalance
        '
        Me.txtbox_TotalEndingBalance.BackColor = System.Drawing.SystemColors.Info
        Me.txtbox_TotalEndingBalance.Location = New System.Drawing.Point(552, 11)
        Me.txtbox_TotalEndingBalance.Name = "txtbox_TotalEndingBalance"
        Me.txtbox_TotalEndingBalance.ReadOnly = True
        Me.txtbox_TotalEndingBalance.Size = New System.Drawing.Size(136, 20)
        Me.txtbox_TotalEndingBalance.TabIndex = 1
        '
        'txtbox_TotalBeginningBalance
        '
        Me.txtbox_TotalBeginningBalance.BackColor = System.Drawing.SystemColors.Info
        Me.txtbox_TotalBeginningBalance.Location = New System.Drawing.Point(275, 11)
        Me.txtbox_TotalBeginningBalance.Name = "txtbox_TotalBeginningBalance"
        Me.txtbox_TotalBeginningBalance.ReadOnly = True
        Me.txtbox_TotalBeginningBalance.Size = New System.Drawing.Size(136, 20)
        Me.txtbox_TotalBeginningBalance.TabIndex = 0
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel3.ColumnCount = 5
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.GroupBox2, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.GroupBox3, 3, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.GroupBox1, 4, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.GroupBox4, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.GroupBox5, 1, 0)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(1040, 69)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.cmb_BillingPeriodNo)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(142, 60)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 19)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Billing Period No:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmb_BillingPeriodNo
        '
        Me.cmb_BillingPeriodNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_BillingPeriodNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmb_BillingPeriodNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmb_BillingPeriodNo.BackColor = System.Drawing.Color.White
        Me.cmb_BillingPeriodNo.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmb_BillingPeriodNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_BillingPeriodNo.FormattingEnabled = True
        Me.cmb_BillingPeriodNo.Location = New System.Drawing.Point(6, 23)
        Me.cmb_BillingPeriodNo.Name = "cmb_BillingPeriodNo"
        Me.cmb_BillingPeriodNo.Size = New System.Drawing.Size(120, 23)
        Me.cmb_BillingPeriodNo.TabIndex = 1
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.cmb_BillingBatchNo)
        Me.GroupBox3.Location = New System.Drawing.Point(471, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(142, 60)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(6, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 19)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Billing Batch No:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmb_BillingBatchNo
        '
        Me.cmb_BillingBatchNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_BillingBatchNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmb_BillingBatchNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmb_BillingBatchNo.BackColor = System.Drawing.Color.White
        Me.cmb_BillingBatchNo.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmb_BillingBatchNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_BillingBatchNo.FormattingEnabled = True
        Me.cmb_BillingBatchNo.Location = New System.Drawing.Point(6, 23)
        Me.cmb_BillingBatchNo.Name = "cmb_BillingBatchNo"
        Me.cmb_BillingBatchNo.Size = New System.Drawing.Size(124, 23)
        Me.cmb_BillingBatchNo.TabIndex = 14
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtbox_Remarks)
        Me.GroupBox1.Location = New System.Drawing.Point(627, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(292, 63)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Remarks"
        '
        'txtbox_Remarks
        '
        Me.txtbox_Remarks.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtbox_Remarks.Location = New System.Drawing.Point(6, 15)
        Me.txtbox_Remarks.Multiline = True
        Me.txtbox_Remarks.Name = "txtbox_Remarks"
        Me.txtbox_Remarks.Size = New System.Drawing.Size(280, 42)
        Me.txtbox_Remarks.TabIndex = 15
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Controls.Add(Me.cmb_BillingType)
        Me.GroupBox4.Location = New System.Drawing.Point(315, 3)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(142, 60)
        Me.GroupBox4.TabIndex = 5
        Me.GroupBox4.TabStop = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(6, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 19)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Billing Type:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmb_BillingType
        '
        Me.cmb_BillingType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_BillingType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmb_BillingType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmb_BillingType.BackColor = System.Drawing.Color.White
        Me.cmb_BillingType.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmb_BillingType.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_BillingType.FormattingEnabled = True
        Me.cmb_BillingType.Location = New System.Drawing.Point(8, 22)
        Me.cmb_BillingType.Name = "cmb_BillingType"
        Me.cmb_BillingType.Size = New System.Drawing.Size(121, 23)
        Me.cmb_BillingType.TabIndex = 13
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Label3)
        Me.GroupBox5.Controls.Add(Me.cmb_ChargeType)
        Me.GroupBox5.Location = New System.Drawing.Point(159, 3)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(142, 60)
        Me.GroupBox5.TabIndex = 10
        Me.GroupBox5.TabStop = False
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(6, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(93, 19)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Charge Type:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmb_ChargeType
        '
        Me.cmb_ChargeType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_ChargeType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmb_ChargeType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmb_ChargeType.BackColor = System.Drawing.Color.White
        Me.cmb_ChargeType.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmb_ChargeType.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_ChargeType.FormattingEnabled = True
        Me.cmb_ChargeType.Location = New System.Drawing.Point(6, 23)
        Me.cmb_ChargeType.Name = "cmb_ChargeType"
        Me.cmb_ChargeType.Size = New System.Drawing.Size(124, 23)
        Me.cmb_ChargeType.TabIndex = 11
        '
        'frmWBSAdjustmentPerBatchAdd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1051, 633)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmWBSAdjustmentPerBatchAdd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Withholding TAX Adjustment (Create)"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.dgv_WBSWhTAX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmb_BillingPeriodNo As System.Windows.Forms.ComboBox
    Friend WithEvents dgv_WBSWhTAX As System.Windows.Forms.DataGridView
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents txtbox_ARAmountAdjusted As System.Windows.Forms.TextBox
    Friend WithEvents txtbox_TotalEndingBalance As System.Windows.Forms.TextBox
    Friend WithEvents txtbox_TotalBeginningBalance As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents btn_Create As System.Windows.Forms.Button
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmb_BillingBatchNo As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtbox_Remarks As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmb_BillingType As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmb_ChargeType As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtbox_APAmountAdjusted As System.Windows.Forms.TextBox
    Friend WithEvents InvoiceNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrigDueDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NewDueDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParticipantName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_WithholdingTAX As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BeginningBalance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EndingBalance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AmountAdjusted As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
