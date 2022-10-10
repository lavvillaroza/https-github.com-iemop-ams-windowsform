<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreateDMCM
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.cbo_BillPeriod = New System.Windows.Forms.ComboBox()
        Me.cbo_Participant = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dgv_ViewSummary = New System.Windows.Forms.DataGridView()
        Me.hIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.hSummaryType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.hINVDMCMNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.hGroupNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.hParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.hIDType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.hChargeType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.hDueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.hNewDueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.hBeginningBalance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.hEndingBalance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.hAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.hChkBox = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lbl_DestParticipant = New System.Windows.Forms.Label()
        Me.cbo_destParticipant = New System.Windows.Forms.ComboBox()
        Me.cmd_clear = New System.Windows.Forms.Button()
        Me.cmd_Search = New System.Windows.Forms.Button()
        Me.cmd_Save = New System.Windows.Forms.Button()
        Me.tControl_1 = New System.Windows.Forms.TabControl()
        Me.tcPerSummary = New System.Windows.Forms.TabPage()
        Me.tcPerInvoice = New System.Windows.Forms.TabPage()
        Me.dgv_viewInvoice = New System.Windows.Forms.DataGridView()
        Me.invBillPeriod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.invSTLRun = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.invIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.invParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.invNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.invDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.invChargeType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.invDueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.invAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.invAdjustmentAmt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.invAdjust = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cmd_close = New System.Windows.Forms.Button()
        CType(Me.dgv_ViewSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.tControl_1.SuspendLayout()
        Me.tcPerSummary.SuspendLayout()
        Me.tcPerInvoice.SuspendLayout()
        CType(Me.dgv_viewInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cbo_BillPeriod
        '
        Me.cbo_BillPeriod.BackColor = System.Drawing.Color.White
        Me.cbo_BillPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_BillPeriod.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_BillPeriod.FormattingEnabled = True
        Me.cbo_BillPeriod.Location = New System.Drawing.Point(98, 18)
        Me.cbo_BillPeriod.Name = "cbo_BillPeriod"
        Me.cbo_BillPeriod.Size = New System.Drawing.Size(121, 22)
        Me.cbo_BillPeriod.TabIndex = 0
        '
        'cbo_Participant
        '
        Me.cbo_Participant.BackColor = System.Drawing.Color.White
        Me.cbo_Participant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_Participant.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_Participant.FormattingEnabled = True
        Me.cbo_Participant.Location = New System.Drawing.Point(314, 18)
        Me.cbo_Participant.Name = "cbo_Participant"
        Me.cbo_Participant.Size = New System.Drawing.Size(121, 22)
        Me.cbo_Participant.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Billing Period:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(228, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Participant ID:"
        '
        'dgv_ViewSummary
        '
        Me.dgv_ViewSummary.AllowUserToAddRows = False
        Me.dgv_ViewSummary.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Cyan
        Me.dgv_ViewSummary.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_ViewSummary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgv_ViewSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_ViewSummary.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.hIDNumber, Me.hSummaryType, Me.hINVDMCMNo, Me.hGroupNo, Me.hParticipantID, Me.hIDType, Me.hChargeType, Me.hDueDate, Me.hNewDueDate, Me.hBeginningBalance, Me.hEndingBalance, Me.hAmount, Me.hChkBox})
        Me.dgv_ViewSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_ViewSummary.Location = New System.Drawing.Point(3, 3)
        Me.dgv_ViewSummary.MultiSelect = False
        Me.dgv_ViewSummary.Name = "dgv_ViewSummary"
        Me.dgv_ViewSummary.RowHeadersWidth = 20
        Me.dgv_ViewSummary.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgv_ViewSummary.RowsDefaultCellStyle = DataGridViewCellStyle7
        Me.dgv_ViewSummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_ViewSummary.Size = New System.Drawing.Size(986, 283)
        Me.dgv_ViewSummary.TabIndex = 6
        '
        'hIDNumber
        '
        Me.hIDNumber.HeaderText = "ID Number"
        Me.hIDNumber.Name = "hIDNumber"
        Me.hIDNumber.ReadOnly = True
        Me.hIDNumber.Width = 82
        '
        'hSummaryType
        '
        Me.hSummaryType.HeaderText = "SummaryType"
        Me.hSummaryType.Name = "hSummaryType"
        Me.hSummaryType.ReadOnly = True
        Me.hSummaryType.Width = 99
        '
        'hINVDMCMNo
        '
        Me.hINVDMCMNo.HeaderText = "InvoiceDMCMNo"
        Me.hINVDMCMNo.Name = "hINVDMCMNo"
        Me.hINVDMCMNo.ReadOnly = True
        Me.hINVDMCMNo.Width = 110
        '
        'hGroupNo
        '
        Me.hGroupNo.HeaderText = "GroupNumber"
        Me.hGroupNo.Name = "hGroupNo"
        Me.hGroupNo.ReadOnly = True
        Me.hGroupNo.Visible = False
        Me.hGroupNo.Width = 98
        '
        'hParticipantID
        '
        Me.hParticipantID.HeaderText = "ParticipantID"
        Me.hParticipantID.Name = "hParticipantID"
        Me.hParticipantID.ReadOnly = True
        Me.hParticipantID.Width = 92
        '
        'hIDType
        '
        Me.hIDType.HeaderText = "IDType"
        Me.hIDType.Name = "hIDType"
        Me.hIDType.ReadOnly = True
        Me.hIDType.Width = 64
        '
        'hChargeType
        '
        Me.hChargeType.HeaderText = "ChargeType"
        Me.hChargeType.Name = "hChargeType"
        Me.hChargeType.ReadOnly = True
        Me.hChargeType.Width = 87
        '
        'hDueDate
        '
        DataGridViewCellStyle2.Format = "d"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.hDueDate.DefaultCellStyle = DataGridViewCellStyle2
        Me.hDueDate.HeaderText = "DueDate"
        Me.hDueDate.Name = "hDueDate"
        Me.hDueDate.ReadOnly = True
        Me.hDueDate.Width = 70
        '
        'hNewDueDate
        '
        DataGridViewCellStyle3.Format = "d"
        Me.hNewDueDate.DefaultCellStyle = DataGridViewCellStyle3
        Me.hNewDueDate.HeaderText = "NewDueDate"
        Me.hNewDueDate.Name = "hNewDueDate"
        Me.hNewDueDate.ReadOnly = True
        Me.hNewDueDate.Width = 90
        '
        'hBeginningBalance
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "n2"
        Me.hBeginningBalance.DefaultCellStyle = DataGridViewCellStyle4
        Me.hBeginningBalance.HeaderText = "BeginningBalance"
        Me.hBeginningBalance.Name = "hBeginningBalance"
        Me.hBeginningBalance.ReadOnly = True
        Me.hBeginningBalance.Width = 116
        '
        'hEndingBalance
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "n2"
        Me.hEndingBalance.DefaultCellStyle = DataGridViewCellStyle5
        Me.hEndingBalance.HeaderText = "EndingBalance"
        Me.hEndingBalance.Name = "hEndingBalance"
        Me.hEndingBalance.ReadOnly = True
        Me.hEndingBalance.Width = 101
        '
        'hAmount
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "n2"
        DataGridViewCellStyle6.NullValue = "0.00"
        Me.hAmount.DefaultCellStyle = DataGridViewCellStyle6
        Me.hAmount.HeaderText = "AdjustmentAMT"
        Me.hAmount.Name = "hAmount"
        Me.hAmount.ReadOnly = True
        Me.hAmount.Width = 107
        '
        'hChkBox
        '
        Me.hChkBox.HeaderText = "AdjustBill"
        Me.hChkBox.Name = "hChkBox"
        Me.hChkBox.Width = 59
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lbl_DestParticipant)
        Me.GroupBox1.Controls.Add(Me.cbo_destParticipant)
        Me.GroupBox1.Controls.Add(Me.cbo_BillPeriod)
        Me.GroupBox1.Controls.Add(Me.cmd_clear)
        Me.GroupBox1.Controls.Add(Me.cmd_Search)
        Me.GroupBox1.Controls.Add(Me.cmd_Save)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cbo_Participant)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1000, 81)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'lbl_DestParticipant
        '
        Me.lbl_DestParticipant.AutoSize = True
        Me.lbl_DestParticipant.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DestParticipant.Location = New System.Drawing.Point(139, 48)
        Me.lbl_DestParticipant.Name = "lbl_DestParticipant"
        Me.lbl_DestParticipant.Size = New System.Drawing.Size(167, 14)
        Me.lbl_DestParticipant.TabIndex = 11
        Me.lbl_DestParticipant.Text = "Select Destination participant:"
        '
        'cbo_destParticipant
        '
        Me.cbo_destParticipant.BackColor = System.Drawing.Color.White
        Me.cbo_destParticipant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_destParticipant.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_destParticipant.FormattingEnabled = True
        Me.cbo_destParticipant.Location = New System.Drawing.Point(314, 45)
        Me.cbo_destParticipant.Name = "cbo_destParticipant"
        Me.cbo_destParticipant.Size = New System.Drawing.Size(121, 22)
        Me.cbo_destParticipant.TabIndex = 10
        '
        'cmd_clear
        '
        Me.cmd_clear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_clear.BackColor = System.Drawing.Color.White
        Me.cmd_clear.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_clear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_clear.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_clear.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_clear.ForeColor = System.Drawing.Color.Black
        Me.cmd_clear.Image = Global.AccountsManagementForms.My.Resources.Resources.CancelIconRed22x22
        Me.cmd_clear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_clear.Location = New System.Drawing.Point(856, 23)
        Me.cmd_clear.Name = "cmd_clear"
        Me.cmd_clear.Size = New System.Drawing.Size(133, 39)
        Me.cmd_clear.TabIndex = 7
        Me.cmd_clear.Text = "  Clear"
        Me.cmd_clear.UseVisualStyleBackColor = False
        '
        'cmd_Search
        '
        Me.cmd_Search.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_Search.BackColor = System.Drawing.Color.White
        Me.cmd_Search.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Search.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Search.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Search.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Search.ForeColor = System.Drawing.Color.Black
        Me.cmd_Search.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.cmd_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Search.Location = New System.Drawing.Point(579, 23)
        Me.cmd_Search.Name = "cmd_Search"
        Me.cmd_Search.Size = New System.Drawing.Size(133, 39)
        Me.cmd_Search.TabIndex = 4
        Me.cmd_Search.Text = "   Search"
        Me.cmd_Search.UseVisualStyleBackColor = False
        '
        'cmd_Save
        '
        Me.cmd_Save.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_Save.BackColor = System.Drawing.Color.White
        Me.cmd_Save.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Save.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Save.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Save.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Save.ForeColor = System.Drawing.Color.Black
        Me.cmd_Save.Image = Global.AccountsManagementForms.My.Resources.Resources.SaveIconColored22x22
        Me.cmd_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Save.Location = New System.Drawing.Point(717, 23)
        Me.cmd_Save.Name = "cmd_Save"
        Me.cmd_Save.Size = New System.Drawing.Size(133, 39)
        Me.cmd_Save.TabIndex = 5
        Me.cmd_Save.Text = "  Save"
        Me.cmd_Save.UseVisualStyleBackColor = False
        '
        'tControl_1
        '
        Me.tControl_1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tControl_1.Controls.Add(Me.tcPerSummary)
        Me.tControl_1.Controls.Add(Me.tcPerInvoice)
        Me.tControl_1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.tControl_1.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tControl_1.Location = New System.Drawing.Point(12, 99)
        Me.tControl_1.Name = "tControl_1"
        Me.tControl_1.SelectedIndex = 0
        Me.tControl_1.Size = New System.Drawing.Size(1000, 317)
        Me.tControl_1.TabIndex = 9
        '
        'tcPerSummary
        '
        Me.tcPerSummary.Controls.Add(Me.dgv_ViewSummary)
        Me.tcPerSummary.Location = New System.Drawing.Point(4, 24)
        Me.tcPerSummary.Name = "tcPerSummary"
        Me.tcPerSummary.Padding = New System.Windows.Forms.Padding(3)
        Me.tcPerSummary.Size = New System.Drawing.Size(992, 289)
        Me.tcPerSummary.TabIndex = 0
        Me.tcPerSummary.Text = "Create DMCM Per Summary"
        Me.tcPerSummary.UseVisualStyleBackColor = True
        '
        'tcPerInvoice
        '
        Me.tcPerInvoice.Controls.Add(Me.dgv_viewInvoice)
        Me.tcPerInvoice.Location = New System.Drawing.Point(4, 24)
        Me.tcPerInvoice.Name = "tcPerInvoice"
        Me.tcPerInvoice.Padding = New System.Windows.Forms.Padding(3)
        Me.tcPerInvoice.Size = New System.Drawing.Size(992, 289)
        Me.tcPerInvoice.TabIndex = 1
        Me.tcPerInvoice.Text = "Create DMCM Per Invoice"
        Me.tcPerInvoice.UseVisualStyleBackColor = True
        '
        'dgv_viewInvoice
        '
        Me.dgv_viewInvoice.AllowUserToAddRows = False
        Me.dgv_viewInvoice.AllowUserToDeleteRows = False
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_viewInvoice.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle8
        Me.dgv_viewInvoice.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgv_viewInvoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_viewInvoice.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.invBillPeriod, Me.invSTLRun, Me.invIDNumber, Me.invParticipantID, Me.invNumber, Me.invDate, Me.invChargeType, Me.invDueDate, Me.invAmount, Me.invAdjustmentAmt, Me.invAdjust})
        Me.dgv_viewInvoice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_viewInvoice.Location = New System.Drawing.Point(3, 3)
        Me.dgv_viewInvoice.Name = "dgv_viewInvoice"
        Me.dgv_viewInvoice.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgv_viewInvoice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_viewInvoice.Size = New System.Drawing.Size(986, 283)
        Me.dgv_viewInvoice.TabIndex = 0
        '
        'invBillPeriod
        '
        Me.invBillPeriod.HeaderText = "BillingPeriod"
        Me.invBillPeriod.Name = "invBillPeriod"
        Me.invBillPeriod.ReadOnly = True
        Me.invBillPeriod.Width = 94
        '
        'invSTLRun
        '
        Me.invSTLRun.HeaderText = "STL Run"
        Me.invSTLRun.Name = "invSTLRun"
        Me.invSTLRun.ReadOnly = True
        Me.invSTLRun.Width = 73
        '
        'invIDNumber
        '
        Me.invIDNumber.HeaderText = "IDNumber"
        Me.invIDNumber.Name = "invIDNumber"
        Me.invIDNumber.ReadOnly = True
        Me.invIDNumber.Width = 79
        '
        'invParticipantID
        '
        Me.invParticipantID.HeaderText = "ParticipantID"
        Me.invParticipantID.Name = "invParticipantID"
        Me.invParticipantID.ReadOnly = True
        Me.invParticipantID.Width = 92
        '
        'invNumber
        '
        Me.invNumber.HeaderText = "InvoiceNumber"
        Me.invNumber.Name = "invNumber"
        Me.invNumber.ReadOnly = True
        Me.invNumber.Width = 102
        '
        'invDate
        '
        DataGridViewCellStyle9.Format = "d"
        Me.invDate.DefaultCellStyle = DataGridViewCellStyle9
        Me.invDate.HeaderText = "InvoiceDate"
        Me.invDate.Name = "invDate"
        Me.invDate.ReadOnly = True
        Me.invDate.Width = 85
        '
        'invChargeType
        '
        Me.invChargeType.HeaderText = "ChargeType"
        Me.invChargeType.Name = "invChargeType"
        Me.invChargeType.ReadOnly = True
        Me.invChargeType.Width = 87
        '
        'invDueDate
        '
        DataGridViewCellStyle10.Format = "d"
        DataGridViewCellStyle10.NullValue = Nothing
        Me.invDueDate.DefaultCellStyle = DataGridViewCellStyle10
        Me.invDueDate.HeaderText = "DueDate"
        Me.invDueDate.Name = "invDueDate"
        Me.invDueDate.ReadOnly = True
        Me.invDueDate.Width = 70
        '
        'invAmount
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Format = "n2"
        Me.invAmount.DefaultCellStyle = DataGridViewCellStyle11
        Me.invAmount.HeaderText = "Amount"
        Me.invAmount.Name = "invAmount"
        Me.invAmount.ReadOnly = True
        Me.invAmount.Width = 69
        '
        'invAdjustmentAmt
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle12.Format = "n2"
        DataGridViewCellStyle12.NullValue = "0.00"
        Me.invAdjustmentAmt.DefaultCellStyle = DataGridViewCellStyle12
        Me.invAdjustmentAmt.HeaderText = "AdjustmentAMT"
        Me.invAdjustmentAmt.Name = "invAdjustmentAmt"
        Me.invAdjustmentAmt.ReadOnly = True
        Me.invAdjustmentAmt.Width = 107
        '
        'invAdjust
        '
        Me.invAdjust.HeaderText = "Adjust"
        Me.invAdjust.Name = "invAdjust"
        Me.invAdjust.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.invAdjust.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.invAdjust.Width = 62
        '
        'cmd_close
        '
        Me.cmd_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_close.BackColor = System.Drawing.Color.White
        Me.cmd_close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_close.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_close.ForeColor = System.Drawing.Color.Black
        Me.cmd_close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_close.Location = New System.Drawing.Point(879, 422)
        Me.cmd_close.Name = "cmd_close"
        Me.cmd_close.Size = New System.Drawing.Size(133, 39)
        Me.cmd_close.TabIndex = 8
        Me.cmd_close.Text = "  Close"
        Me.cmd_close.UseVisualStyleBackColor = False
        '
        'frmCreateDMCM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1019, 472)
        Me.Controls.Add(Me.tControl_1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmd_close)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmCreateDMCM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Create Adjustments (DM/CM)"
        CType(Me.dgv_ViewSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.tControl_1.ResumeLayout(False)
        Me.tcPerSummary.ResumeLayout(False)
        Me.tcPerInvoice.ResumeLayout(False)
        CType(Me.dgv_viewInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cbo_BillPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents cbo_Participant As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmd_Search As System.Windows.Forms.Button
    Friend WithEvents cmd_Save As System.Windows.Forms.Button
    Friend WithEvents dgv_ViewSummary As System.Windows.Forms.DataGridView
    Friend WithEvents cmd_clear As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmd_close As System.Windows.Forms.Button
    Friend WithEvents tControl_1 As System.Windows.Forms.TabControl
    Friend WithEvents tcPerSummary As System.Windows.Forms.TabPage
    Friend WithEvents tcPerInvoice As System.Windows.Forms.TabPage
    Friend WithEvents dgv_viewInvoice As System.Windows.Forms.DataGridView
    Friend WithEvents lbl_DestParticipant As System.Windows.Forms.Label
    Friend WithEvents cbo_destParticipant As System.Windows.Forms.ComboBox
    Friend WithEvents hIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hSummaryType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hINVDMCMNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hGroupNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hIDType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hChargeType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hDueDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hNewDueDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hBeginningBalance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hEndingBalance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hChkBox As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents invBillPeriod As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents invSTLRun As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents invIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents invParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents invNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents invDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents invChargeType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents invDueDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents invAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents invAdjustmentAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents invAdjust As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
