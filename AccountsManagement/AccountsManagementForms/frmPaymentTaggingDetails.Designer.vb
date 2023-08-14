<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymentTaggingDetails
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
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtTotalAllocAmount = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dgAllocation = New System.Windows.Forms.DataGridView()
        Me.colWBSummaryNoAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBatchNoAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBillingPeriodAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIDNumberAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTransctionNoAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colChargeTypeAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colOrigDueDateAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNewDueDateAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAllocAmountAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dtRemittanceDate = New System.Windows.Forms.DateTimePicker()
        Me.btn_Allocate = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblCollectionDate = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ddlParticipantID = New System.Windows.Forms.ComboBox()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtTotalAROutstandingBalance = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTotalTaggedAmount = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dgTagging = New System.Windows.Forms.DataGridView()
        Me.colWBSummaryNoAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBatchNoAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBillingNoAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIDNumberAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTransactionNoAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colChargeTypeAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colOrigDueDateAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNewDueDateAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEndingBalanceAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFullyPaid = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colTagAmountAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNewEndingBalanceAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.dgAllocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgTagging, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Panel2, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.dgAllocation, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(1163, 549)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtTotalAllocAmount)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 472)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1157, 74)
        Me.Panel2.TabIndex = 28
        '
        'txtTotalAllocAmount
        '
        Me.txtTotalAllocAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTotalAllocAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTotalAllocAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAllocAmount.ForeColor = System.Drawing.Color.Black
        Me.txtTotalAllocAmount.Location = New System.Drawing.Point(974, 16)
        Me.txtTotalAllocAmount.MaxLength = 26
        Me.txtTotalAllocAmount.Name = "txtTotalAllocAmount"
        Me.txtTotalAllocAmount.ReadOnly = True
        Me.txtTotalAllocAmount.Size = New System.Drawing.Size(167, 21)
        Me.txtTotalAllocAmount.TabIndex = 23
        Me.txtTotalAllocAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(859, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(109, 14)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Total Paid Amount:"
        '
        'dgAllocation
        '
        Me.dgAllocation.AllowUserToAddRows = False
        Me.dgAllocation.AllowUserToDeleteRows = False
        Me.dgAllocation.AllowUserToResizeColumns = False
        Me.dgAllocation.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgAllocation.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgAllocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgAllocation.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colWBSummaryNoAR, Me.colBatchNoAR, Me.colBillingPeriodAR, Me.colIDNumberAR, Me.colTransctionNoAR, Me.colChargeTypeAR, Me.colOrigDueDateAR, Me.colNewDueDateAR, Me.colAllocAmountAR})
        Me.dgAllocation.Dock = System.Windows.Forms.DockStyle.Left
        Me.dgAllocation.Location = New System.Drawing.Point(3, 3)
        Me.dgAllocation.Name = "dgAllocation"
        Me.dgAllocation.RowHeadersVisible = False
        Me.dgAllocation.RowHeadersWidth = 20
        Me.dgAllocation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgAllocation.Size = New System.Drawing.Size(1157, 463)
        Me.dgAllocation.TabIndex = 27
        '
        'colWBSummaryNoAR
        '
        Me.colWBSummaryNoAR.Frozen = True
        Me.colWBSummaryNoAR.HeaderText = "WBSummaryNo"
        Me.colWBSummaryNoAR.Name = "colWBSummaryNoAR"
        Me.colWBSummaryNoAR.ReadOnly = True
        Me.colWBSummaryNoAR.Visible = False
        '
        'colBatchNoAR
        '
        Me.colBatchNoAR.Frozen = True
        Me.colBatchNoAR.HeaderText = "Batch No"
        Me.colBatchNoAR.Name = "colBatchNoAR"
        Me.colBatchNoAR.ReadOnly = True
        Me.colBatchNoAR.Width = 75
        '
        'colBillingPeriodAR
        '
        Me.colBillingPeriodAR.Frozen = True
        Me.colBillingPeriodAR.HeaderText = "Billing Period"
        Me.colBillingPeriodAR.Name = "colBillingPeriodAR"
        Me.colBillingPeriodAR.ReadOnly = True
        Me.colBillingPeriodAR.Width = 90
        '
        'colIDNumberAR
        '
        Me.colIDNumberAR.Frozen = True
        Me.colIDNumberAR.HeaderText = "ID Number"
        Me.colIDNumberAR.Name = "colIDNumberAR"
        Me.colIDNumberAR.ReadOnly = True
        Me.colIDNumberAR.Width = 120
        '
        'colTransctionNoAR
        '
        Me.colTransctionNoAR.Frozen = True
        Me.colTransctionNoAR.HeaderText = "Transaction No"
        Me.colTransctionNoAR.Name = "colTransctionNoAR"
        Me.colTransctionNoAR.ReadOnly = True
        Me.colTransctionNoAR.Width = 115
        '
        'colChargeTypeAR
        '
        Me.colChargeTypeAR.Frozen = True
        Me.colChargeTypeAR.HeaderText = "ChargeType"
        Me.colChargeTypeAR.Name = "colChargeTypeAR"
        '
        'colOrigDueDateAR
        '
        Me.colOrigDueDateAR.Frozen = True
        Me.colOrigDueDateAR.HeaderText = "Orig Due Date"
        Me.colOrigDueDateAR.Name = "colOrigDueDateAR"
        Me.colOrigDueDateAR.ReadOnly = True
        '
        'colNewDueDateAR
        '
        Me.colNewDueDateAR.Frozen = True
        Me.colNewDueDateAR.HeaderText = "New Due Date"
        Me.colNewDueDateAR.Name = "colNewDueDateAR"
        Me.colNewDueDateAR.ReadOnly = True
        Me.colNewDueDateAR.Width = 110
        '
        'colAllocAmountAR
        '
        Me.colAllocAmountAR.HeaderText = "Allocated Amount"
        Me.colAllocAmountAR.Name = "colAllocAmountAR"
        Me.colAllocAmountAR.ReadOnly = True
        Me.colAllocAmountAR.Width = 140
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dtRemittanceDate)
        Me.GroupBox1.Controls.Add(Me.btn_Allocate)
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Controls.Add(Me.btnCancel)
        Me.GroupBox1.Controls.Add(Me.lblCollectionDate)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.ddlParticipantID)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1177, 74)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'dtRemittanceDate
        '
        Me.dtRemittanceDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtRemittanceDate.Location = New System.Drawing.Point(117, 17)
        Me.dtRemittanceDate.Name = "dtRemittanceDate"
        Me.dtRemittanceDate.Size = New System.Drawing.Size(172, 20)
        Me.dtRemittanceDate.TabIndex = 53
        '
        'btn_Allocate
        '
        Me.btn_Allocate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Allocate.BackColor = System.Drawing.Color.White
        Me.btn_Allocate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Allocate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Allocate.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Allocate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Allocate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Allocate.ForeColor = System.Drawing.Color.Black
        Me.btn_Allocate.Image = Global.AccountsManagementForms.My.Resources.Resources.ProcessDataIcon22x22
        Me.btn_Allocate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Allocate.Location = New System.Drawing.Point(715, 20)
        Me.btn_Allocate.Name = "btn_Allocate"
        Me.btn_Allocate.Size = New System.Drawing.Size(174, 39)
        Me.btn_Allocate.TabIndex = 52
        Me.btn_Allocate.Text = "Allocate"
        Me.btn_Allocate.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.White
        Me.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Image = Global.AccountsManagementForms.My.Resources.Resources.SaveIconColored22x22
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(895, 20)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(130, 39)
        Me.btnSave.TabIndex = 50
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.White
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.Black
        Me.btnCancel.Image = Global.AccountsManagementForms.My.Resources.Resources.BackRedIcon22x22
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(1031, 20)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(130, 39)
        Me.btnCancel.TabIndex = 51
        Me.btnCancel.Text = "&Back"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'lblCollectionDate
        '
        Me.lblCollectionDate.AutoSize = True
        Me.lblCollectionDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCollectionDate.Location = New System.Drawing.Point(11, 20)
        Me.lblCollectionDate.Name = "lblCollectionDate"
        Me.lblCollectionDate.Size = New System.Drawing.Size(103, 14)
        Me.lblCollectionDate.TabIndex = 48
        Me.lblCollectionDate.Text = "Remitttance Date:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(33, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 14)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = "Participant ID:"
        '
        'ddlParticipantID
        '
        Me.ddlParticipantID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.ddlParticipantID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ddlParticipantID.FormattingEnabled = True
        Me.ddlParticipantID.Location = New System.Drawing.Point(117, 43)
        Me.ddlParticipantID.Name = "ddlParticipantID"
        Me.ddlParticipantID.Size = New System.Drawing.Size(172, 21)
        Me.ddlParticipantID.TabIndex = 43
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1169, 555)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "AP Tagging"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Panel1, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.dgTagging, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1163, 549)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtTotalAROutstandingBalance)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtTotalTaggedAmount)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 472)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1157, 74)
        Me.Panel1.TabIndex = 28
        '
        'txtTotalAROutstandingBalance
        '
        Me.txtTotalAROutstandingBalance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTotalAROutstandingBalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTotalAROutstandingBalance.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAROutstandingBalance.ForeColor = System.Drawing.Color.Black
        Me.txtTotalAROutstandingBalance.Location = New System.Drawing.Point(975, 42)
        Me.txtTotalAROutstandingBalance.MaxLength = 26
        Me.txtTotalAROutstandingBalance.Name = "txtTotalAROutstandingBalance"
        Me.txtTotalAROutstandingBalance.ReadOnly = True
        Me.txtTotalAROutstandingBalance.Size = New System.Drawing.Size(167, 21)
        Me.txtTotalAROutstandingBalance.TabIndex = 25
        Me.txtTotalAROutstandingBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(816, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(151, 14)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Total Outstanding Balance:"
        '
        'txtTotalTaggedAmount
        '
        Me.txtTotalTaggedAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTotalTaggedAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTotalTaggedAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalTaggedAmount.ForeColor = System.Drawing.Color.Black
        Me.txtTotalTaggedAmount.Location = New System.Drawing.Point(974, 16)
        Me.txtTotalTaggedAmount.MaxLength = 26
        Me.txtTotalTaggedAmount.Name = "txtTotalTaggedAmount"
        Me.txtTotalTaggedAmount.ReadOnly = True
        Me.txtTotalTaggedAmount.Size = New System.Drawing.Size(167, 21)
        Me.txtTotalTaggedAmount.TabIndex = 23
        Me.txtTotalTaggedAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(840, 19)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(126, 14)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "Total Tagged Amount:"
        '
        'dgTagging
        '
        Me.dgTagging.AllowUserToAddRows = False
        Me.dgTagging.AllowUserToDeleteRows = False
        Me.dgTagging.AllowUserToResizeColumns = False
        Me.dgTagging.AllowUserToResizeRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgTagging.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgTagging.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgTagging.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colWBSummaryNoAP, Me.colBatchNoAP, Me.colBillingNoAP, Me.colIDNumberAP, Me.colTransactionNoAP, Me.colChargeTypeAP, Me.colOrigDueDateAP, Me.colNewDueDateAP, Me.colEndingBalanceAP, Me.colFullyPaid, Me.colTagAmountAP, Me.colNewEndingBalanceAP})
        Me.dgTagging.Dock = System.Windows.Forms.DockStyle.Left
        Me.dgTagging.Location = New System.Drawing.Point(3, 3)
        Me.dgTagging.MultiSelect = False
        Me.dgTagging.Name = "dgTagging"
        Me.dgTagging.RowHeadersVisible = False
        Me.dgTagging.RowHeadersWidth = 20
        Me.dgTagging.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgTagging.Size = New System.Drawing.Size(1157, 463)
        Me.dgTagging.TabIndex = 27
        '
        'colWBSummaryNoAP
        '
        Me.colWBSummaryNoAP.Frozen = True
        Me.colWBSummaryNoAP.HeaderText = "WBSummaryNo"
        Me.colWBSummaryNoAP.Name = "colWBSummaryNoAP"
        Me.colWBSummaryNoAP.ReadOnly = True
        Me.colWBSummaryNoAP.Visible = False
        '
        'colBatchNoAP
        '
        Me.colBatchNoAP.Frozen = True
        Me.colBatchNoAP.HeaderText = "Batch No"
        Me.colBatchNoAP.Name = "colBatchNoAP"
        Me.colBatchNoAP.ReadOnly = True
        Me.colBatchNoAP.Width = 75
        '
        'colBillingNoAP
        '
        Me.colBillingNoAP.Frozen = True
        Me.colBillingNoAP.HeaderText = "Billing Period"
        Me.colBillingNoAP.Name = "colBillingNoAP"
        Me.colBillingNoAP.ReadOnly = True
        Me.colBillingNoAP.Width = 90
        '
        'colIDNumberAP
        '
        Me.colIDNumberAP.Frozen = True
        Me.colIDNumberAP.HeaderText = "ID Number"
        Me.colIDNumberAP.Name = "colIDNumberAP"
        Me.colIDNumberAP.ReadOnly = True
        Me.colIDNumberAP.Width = 120
        '
        'colTransactionNoAP
        '
        Me.colTransactionNoAP.Frozen = True
        Me.colTransactionNoAP.HeaderText = "Transaction No"
        Me.colTransactionNoAP.Name = "colTransactionNoAP"
        Me.colTransactionNoAP.ReadOnly = True
        Me.colTransactionNoAP.Width = 115
        '
        'colChargeTypeAP
        '
        Me.colChargeTypeAP.Frozen = True
        Me.colChargeTypeAP.HeaderText = "Charge Type"
        Me.colChargeTypeAP.Name = "colChargeTypeAP"
        '
        'colOrigDueDateAP
        '
        Me.colOrigDueDateAP.Frozen = True
        Me.colOrigDueDateAP.HeaderText = "Orig Due Date"
        Me.colOrigDueDateAP.Name = "colOrigDueDateAP"
        Me.colOrigDueDateAP.ReadOnly = True
        '
        'colNewDueDateAP
        '
        Me.colNewDueDateAP.Frozen = True
        Me.colNewDueDateAP.HeaderText = "New Due Date"
        Me.colNewDueDateAP.Name = "colNewDueDateAP"
        Me.colNewDueDateAP.ReadOnly = True
        Me.colNewDueDateAP.Width = 110
        '
        'colEndingBalanceAP
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.colEndingBalanceAP.DefaultCellStyle = DataGridViewCellStyle3
        Me.colEndingBalanceAP.Frozen = True
        Me.colEndingBalanceAP.HeaderText = "Ending Balance"
        Me.colEndingBalanceAP.Name = "colEndingBalanceAP"
        Me.colEndingBalanceAP.ReadOnly = True
        Me.colEndingBalanceAP.Width = 110
        '
        'colFullyPaid
        '
        Me.colFullyPaid.Frozen = True
        Me.colFullyPaid.HeaderText = "Fully Paid"
        Me.colFullyPaid.Name = "colFullyPaid"
        Me.colFullyPaid.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colFullyPaid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colFullyPaid.Width = 75
        '
        'colTagAmountAP
        '
        Me.colTagAmountAP.Frozen = True
        Me.colTagAmountAP.HeaderText = "Tag Amount"
        Me.colTagAmountAP.Name = "colTagAmountAP"
        Me.colTagAmountAP.Width = 120
        '
        'colNewEndingBalanceAP
        '
        Me.colNewEndingBalanceAP.HeaderText = "New Ending Balance"
        Me.colNewEndingBalanceAP.Name = "colNewEndingBalanceAP"
        Me.colNewEndingBalanceAP.ReadOnly = True
        Me.colNewEndingBalanceAP.Width = 130
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TableLayoutPanel3)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1169, 555)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "AR Allocation"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TabControl1, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1183, 667)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(3, 83)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1177, 581)
        Me.TabControl1.TabIndex = 0
        '
        'frmPaymentTaggingDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1189, 673)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmPaymentTaggingDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Payment Tagging Details"
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.dgAllocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgTagging, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txtTotalAllocAmount As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents dgAllocation As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btn_Allocate As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents lblCollectionDate As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ddlParticipantID As ComboBox
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents txtTotalAROutstandingBalance As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtTotalTaggedAmount As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents dgTagging As DataGridView
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents dtRemittanceDate As DateTimePicker
    Friend WithEvents colWBSummaryNoAP As DataGridViewTextBoxColumn
    Friend WithEvents colBatchNoAP As DataGridViewTextBoxColumn
    Friend WithEvents colBillingNoAP As DataGridViewTextBoxColumn
    Friend WithEvents colIDNumberAP As DataGridViewTextBoxColumn
    Friend WithEvents colTransactionNoAP As DataGridViewTextBoxColumn
    Friend WithEvents colChargeTypeAP As DataGridViewTextBoxColumn
    Friend WithEvents colOrigDueDateAP As DataGridViewTextBoxColumn
    Friend WithEvents colNewDueDateAP As DataGridViewTextBoxColumn
    Friend WithEvents colEndingBalanceAP As DataGridViewTextBoxColumn
    Friend WithEvents colFullyPaid As DataGridViewCheckBoxColumn
    Friend WithEvents colTagAmountAP As DataGridViewTextBoxColumn
    Friend WithEvents colNewEndingBalanceAP As DataGridViewTextBoxColumn
    Friend WithEvents colWBSummaryNoAR As DataGridViewTextBoxColumn
    Friend WithEvents colBatchNoAR As DataGridViewTextBoxColumn
    Friend WithEvents colBillingPeriodAR As DataGridViewTextBoxColumn
    Friend WithEvents colIDNumberAR As DataGridViewTextBoxColumn
    Friend WithEvents colTransctionNoAR As DataGridViewTextBoxColumn
    Friend WithEvents colChargeTypeAR As DataGridViewTextBoxColumn
    Friend WithEvents colOrigDueDateAR As DataGridViewTextBoxColumn
    Friend WithEvents colNewDueDateAR As DataGridViewTextBoxColumn
    Friend WithEvents colAllocAmountAR As DataGridViewTextBoxColumn
End Class
