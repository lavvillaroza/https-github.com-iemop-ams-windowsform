﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmWHTaxCertificateSTLDetails
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.mainTLP = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ddlRemittanceDate = New System.Windows.Forms.ComboBox()
        Me.btn_Allocate = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblCollectionDate = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ddlParticipantID = New System.Windows.Forms.ComboBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.checkBoxAll = New System.Windows.Forms.CheckBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtTotalAROutstandingBalance = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTotalTaggedAmount = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dgTagging = New System.Windows.Forms.DataGridView()
        Me.colWBSummaryNoAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBatchNoAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBillingNoAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIDNumberAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTransactionNoAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colOrigDueDateAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNewDueDateAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEndingBalanceAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colWithholdingTaxAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFullyPaid = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colTagAmountAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNewEndingBalanceAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtTotalAllocAmount = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dgAllocation = New System.Windows.Forms.DataGridView()
        Me.colWBSummaryNoAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBatchNoAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBillingPeriodAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIDNumberAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTransctionNoAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colOrigDueDateAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNewDueDateAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colWithholdingTaxAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAllocAmountAP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ctrl_statusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatus_LabelMsg = New System.Windows.Forms.ToolStripStatusLabel()
        Me.mainTLP.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgTagging, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.dgAllocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ctrl_statusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'mainTLP
        '
        Me.mainTLP.ColumnCount = 1
        Me.mainTLP.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.mainTLP.Controls.Add(Me.GroupBox1, 0, 0)
        Me.mainTLP.Controls.Add(Me.Panel3, 0, 1)
        Me.mainTLP.Location = New System.Drawing.Point(12, 12)
        Me.mainTLP.Name = "mainTLP"
        Me.mainTLP.RowCount = 2
        Me.mainTLP.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.mainTLP.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.mainTLP.Size = New System.Drawing.Size(1183, 568)
        Me.mainTLP.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ddlRemittanceDate)
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
        'ddlRemittanceDate
        '
        Me.ddlRemittanceDate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.ddlRemittanceDate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ddlRemittanceDate.FormattingEnabled = True
        Me.ddlRemittanceDate.Location = New System.Drawing.Point(117, 13)
        Me.ddlRemittanceDate.Name = "ddlRemittanceDate"
        Me.ddlRemittanceDate.Size = New System.Drawing.Size(172, 21)
        Me.ddlRemittanceDate.TabIndex = 53
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
        Me.btn_Allocate.Image = Global.AccountsManagementForms.My.Resources.Resources.OKicon24x24
        Me.btn_Allocate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Allocate.Location = New System.Drawing.Point(759, 21)
        Me.btn_Allocate.Name = "btn_Allocate"
        Me.btn_Allocate.Size = New System.Drawing.Size(130, 39)
        Me.btn_Allocate.TabIndex = 52
        Me.btn_Allocate.Text = "&Commit"
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
        Me.btnCancel.Location = New System.Drawing.Point(1031, 19)
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
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.checkBoxAll)
        Me.Panel3.Controls.Add(Me.TabControl1)
        Me.Panel3.Location = New System.Drawing.Point(3, 83)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1177, 482)
        Me.Panel3.TabIndex = 2
        '
        'checkBoxAll
        '
        Me.checkBoxAll.AutoSize = True
        Me.checkBoxAll.Location = New System.Drawing.Point(1090, 6)
        Me.checkBoxAll.Name = "checkBoxAll"
        Me.checkBoxAll.Size = New System.Drawing.Size(71, 17)
        Me.checkBoxAll.TabIndex = 1
        Me.checkBoxAll.Text = "Check All"
        Me.checkBoxAll.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(3, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1171, 478)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1163, 452)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Certificate Tagging"
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
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1157, 446)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtTotalAROutstandingBalance)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtTotalTaggedAmount)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 369)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1151, 74)
        Me.Panel1.TabIndex = 28
        '
        'txtTotalAROutstandingBalance
        '
        Me.txtTotalAROutstandingBalance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTotalAROutstandingBalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTotalAROutstandingBalance.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAROutstandingBalance.ForeColor = System.Drawing.Color.Black
        Me.txtTotalAROutstandingBalance.Location = New System.Drawing.Point(969, 42)
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
        Me.Label1.Location = New System.Drawing.Point(810, 44)
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
        Me.txtTotalTaggedAmount.Location = New System.Drawing.Point(968, 16)
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
        Me.Label8.Location = New System.Drawing.Point(834, 19)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(126, 14)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "Total Tagged Amount:"
        '
        'dgTagging
        '
        Me.dgTagging.AllowUserToAddRows = False
        Me.dgTagging.AllowUserToDeleteRows = False
        Me.dgTagging.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgTagging.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgTagging.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        Me.dgTagging.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgTagging.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colWBSummaryNoAR, Me.colBatchNoAR, Me.colBillingNoAR, Me.colIDNumberAR, Me.colTransactionNoAR, Me.colOrigDueDateAR, Me.colNewDueDateAR, Me.colEndingBalanceAR, Me.colWithholdingTaxAR, Me.colFullyPaid, Me.colTagAmountAR, Me.colNewEndingBalanceAR})
        Me.dgTagging.Dock = System.Windows.Forms.DockStyle.Left
        Me.dgTagging.Location = New System.Drawing.Point(3, 3)
        Me.dgTagging.MultiSelect = False
        Me.dgTagging.Name = "dgTagging"
        Me.dgTagging.RowHeadersVisible = False
        Me.dgTagging.RowHeadersWidth = 20
        Me.dgTagging.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgTagging.Size = New System.Drawing.Size(1151, 360)
        Me.dgTagging.TabIndex = 27
        '
        'colWBSummaryNoAR
        '
        Me.colWBSummaryNoAR.Frozen = True
        Me.colWBSummaryNoAR.HeaderText = "WBSummaryNo"
        Me.colWBSummaryNoAR.Name = "colWBSummaryNoAR"
        Me.colWBSummaryNoAR.Visible = False
        '
        'colBatchNoAR
        '
        Me.colBatchNoAR.Frozen = True
        Me.colBatchNoAR.HeaderText = "Batch No"
        Me.colBatchNoAR.Name = "colBatchNoAR"
        Me.colBatchNoAR.Width = 75
        '
        'colBillingNoAR
        '
        Me.colBillingNoAR.Frozen = True
        Me.colBillingNoAR.HeaderText = "Billing Period"
        Me.colBillingNoAR.Name = "colBillingNoAR"
        Me.colBillingNoAR.Width = 90
        '
        'colIDNumberAR
        '
        Me.colIDNumberAR.Frozen = True
        Me.colIDNumberAR.HeaderText = "ID Number"
        Me.colIDNumberAR.Name = "colIDNumberAR"
        Me.colIDNumberAR.Width = 120
        '
        'colTransactionNoAR
        '
        Me.colTransactionNoAR.Frozen = True
        Me.colTransactionNoAR.HeaderText = "Transaction No"
        Me.colTransactionNoAR.Name = "colTransactionNoAR"
        Me.colTransactionNoAR.Width = 150
        '
        'colOrigDueDateAR
        '
        Me.colOrigDueDateAR.Frozen = True
        Me.colOrigDueDateAR.HeaderText = "Orig Due Date"
        Me.colOrigDueDateAR.Name = "colOrigDueDateAR"
        '
        'colNewDueDateAR
        '
        Me.colNewDueDateAR.Frozen = True
        Me.colNewDueDateAR.HeaderText = "New Due Date"
        Me.colNewDueDateAR.Name = "colNewDueDateAR"
        Me.colNewDueDateAR.Width = 110
        '
        'colEndingBalanceAR
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.colEndingBalanceAR.DefaultCellStyle = DataGridViewCellStyle2
        Me.colEndingBalanceAR.Frozen = True
        Me.colEndingBalanceAR.HeaderText = "Ending Balance"
        Me.colEndingBalanceAR.Name = "colEndingBalanceAR"
        Me.colEndingBalanceAR.Width = 110
        '
        'colWithholdingTaxAR
        '
        Me.colWithholdingTaxAR.Frozen = True
        Me.colWithholdingTaxAR.HeaderText = "Withholding Tax"
        Me.colWithholdingTaxAR.Name = "colWithholdingTaxAR"
        Me.colWithholdingTaxAR.Width = 110
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
        'colTagAmountAR
        '
        Me.colTagAmountAR.Frozen = True
        Me.colTagAmountAR.HeaderText = "Tag Amount"
        Me.colTagAmountAR.Name = "colTagAmountAR"
        Me.colTagAmountAR.Width = 120
        '
        'colNewEndingBalanceAR
        '
        Me.colNewEndingBalanceAR.HeaderText = "New Ending Balance"
        Me.colNewEndingBalanceAR.Name = "colNewEndingBalanceAR"
        Me.colNewEndingBalanceAR.Width = 130
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TableLayoutPanel3)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1163, 452)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Certificate Allocation"
        Me.TabPage2.UseVisualStyleBackColor = True
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
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(1157, 446)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtTotalAllocAmount)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 369)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1151, 74)
        Me.Panel2.TabIndex = 28
        '
        'txtTotalAllocAmount
        '
        Me.txtTotalAllocAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTotalAllocAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTotalAllocAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAllocAmount.ForeColor = System.Drawing.Color.Black
        Me.txtTotalAllocAmount.Location = New System.Drawing.Point(968, 16)
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
        Me.Label4.Location = New System.Drawing.Point(821, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(140, 14)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Total Allocation Amount:"
        '
        'dgAllocation
        '
        Me.dgAllocation.AllowUserToAddRows = False
        Me.dgAllocation.AllowUserToDeleteRows = False
        Me.dgAllocation.AllowUserToResizeRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgAllocation.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgAllocation.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        Me.dgAllocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgAllocation.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colWBSummaryNoAP, Me.colBatchNoAP, Me.colBillingPeriodAP, Me.colIDNumberAP, Me.colTransctionNoAP, Me.colOrigDueDateAP, Me.colNewDueDateAP, Me.colWithholdingTaxAP, Me.colAllocAmountAP})
        Me.dgAllocation.Dock = System.Windows.Forms.DockStyle.Left
        Me.dgAllocation.Location = New System.Drawing.Point(3, 3)
        Me.dgAllocation.Name = "dgAllocation"
        Me.dgAllocation.RowHeadersVisible = False
        Me.dgAllocation.RowHeadersWidth = 20
        Me.dgAllocation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgAllocation.Size = New System.Drawing.Size(1151, 360)
        Me.dgAllocation.TabIndex = 27
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
        'colBillingPeriodAP
        '
        Me.colBillingPeriodAP.Frozen = True
        Me.colBillingPeriodAP.HeaderText = "Billing Period"
        Me.colBillingPeriodAP.Name = "colBillingPeriodAP"
        Me.colBillingPeriodAP.ReadOnly = True
        Me.colBillingPeriodAP.Width = 90
        '
        'colIDNumberAP
        '
        Me.colIDNumberAP.Frozen = True
        Me.colIDNumberAP.HeaderText = "ID Number"
        Me.colIDNumberAP.Name = "colIDNumberAP"
        Me.colIDNumberAP.ReadOnly = True
        Me.colIDNumberAP.Width = 120
        '
        'colTransctionNoAP
        '
        Me.colTransctionNoAP.Frozen = True
        Me.colTransctionNoAP.HeaderText = "Transaction No"
        Me.colTransctionNoAP.Name = "colTransctionNoAP"
        Me.colTransctionNoAP.ReadOnly = True
        Me.colTransctionNoAP.Width = 140
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
        'colWithholdingTaxAP
        '
        Me.colWithholdingTaxAP.DataPropertyName = "colWithholdingTaxAP"
        Me.colWithholdingTaxAP.HeaderText = "Withholding Tax"
        Me.colWithholdingTaxAP.Name = "colWithholdingTaxAP"
        Me.colWithholdingTaxAP.ReadOnly = True
        Me.colWithholdingTaxAP.Width = 110
        '
        'colAllocAmountAP
        '
        Me.colAllocAmountAP.HeaderText = "Allocated Amount"
        Me.colAllocAmountAP.Name = "colAllocAmountAP"
        Me.colAllocAmountAP.ReadOnly = True
        Me.colAllocAmountAP.Width = 120
        '
        'ctrl_statusStrip
        '
        Me.ctrl_statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatus_LabelMsg})
        Me.ctrl_statusStrip.Location = New System.Drawing.Point(0, 594)
        Me.ctrl_statusStrip.Name = "ctrl_statusStrip"
        Me.ctrl_statusStrip.Size = New System.Drawing.Size(1208, 22)
        Me.ctrl_statusStrip.TabIndex = 59
        Me.ctrl_statusStrip.Text = "StatusStrip1"
        '
        'ToolStripStatus_LabelMsg
        '
        Me.ToolStripStatus_LabelMsg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripStatus_LabelMsg.Name = "ToolStripStatus_LabelMsg"
        Me.ToolStripStatus_LabelMsg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ToolStripStatus_LabelMsg.Size = New System.Drawing.Size(39, 17)
        Me.ToolStripStatus_LabelMsg.Text = "Ready"
        Me.ToolStripStatus_LabelMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripStatus_LabelMsg.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'frmWHTaxCertificateSTLDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1208, 616)
        Me.ControlBox = False
        Me.Controls.Add(Me.ctrl_statusStrip)
        Me.Controls.Add(Me.mainTLP)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmWHTaxCertificateSTLDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Withholding Tax Certificate Tagging"
        Me.mainTLP.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgTagging, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.dgAllocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ctrl_statusStrip.ResumeLayout(False)
        Me.ctrl_statusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents mainTLP As TableLayoutPanel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents ddlParticipantID As ComboBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents lblCollectionDate As Label
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents txtTotalAROutstandingBalance As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtTotalTaggedAmount As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents dgTagging As DataGridView
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txtTotalAllocAmount As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents dgAllocation As DataGridView
    Friend WithEvents btn_Allocate As Button
    Friend WithEvents ddlRemittanceDate As ComboBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents checkBoxAll As CheckBox
    Friend WithEvents colWBSummaryNoAR As DataGridViewTextBoxColumn
    Friend WithEvents colBatchNoAR As DataGridViewTextBoxColumn
    Friend WithEvents colBillingNoAR As DataGridViewTextBoxColumn
    Friend WithEvents colIDNumberAR As DataGridViewTextBoxColumn
    Friend WithEvents colTransactionNoAR As DataGridViewTextBoxColumn
    Friend WithEvents colOrigDueDateAR As DataGridViewTextBoxColumn
    Friend WithEvents colNewDueDateAR As DataGridViewTextBoxColumn
    Friend WithEvents colEndingBalanceAR As DataGridViewTextBoxColumn
    Friend WithEvents colWithholdingTaxAR As DataGridViewTextBoxColumn
    Friend WithEvents colFullyPaid As DataGridViewCheckBoxColumn
    Friend WithEvents colTagAmountAR As DataGridViewTextBoxColumn
    Friend WithEvents colNewEndingBalanceAR As DataGridViewTextBoxColumn
    Friend WithEvents colWBSummaryNoAP As DataGridViewTextBoxColumn
    Friend WithEvents colBatchNoAP As DataGridViewTextBoxColumn
    Friend WithEvents colBillingPeriodAP As DataGridViewTextBoxColumn
    Friend WithEvents colIDNumberAP As DataGridViewTextBoxColumn
    Friend WithEvents colTransctionNoAP As DataGridViewTextBoxColumn
    Friend WithEvents colOrigDueDateAP As DataGridViewTextBoxColumn
    Friend WithEvents colNewDueDateAP As DataGridViewTextBoxColumn
    Friend WithEvents colWithholdingTaxAP As DataGridViewTextBoxColumn
    Friend WithEvents colAllocAmountAP As DataGridViewTextBoxColumn
    Friend WithEvents ctrl_statusStrip As StatusStrip
    Friend WithEvents ToolStripStatus_LabelMsg As ToolStripStatusLabel
End Class
