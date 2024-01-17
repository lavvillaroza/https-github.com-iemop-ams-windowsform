<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewCollectionNotice
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
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DGV_Previous = New System.Windows.Forms.DataGridView()
        Me.BillingPeriod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.prevInvNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.prevInvoiceDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DueDat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Energy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EnergyVAT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDefaultInterest = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UpdatedBy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UpdatedDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DGV_Current = New System.Windows.Forms.DataGridView()
        Me.currBPeriod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.currInvoice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.currInvDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.currDueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.currEnergy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.currVatEnergy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.currMF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDefultInterest = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.currTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.currUpdatedBy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.currUpdateDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cbo_ParticipantId = New System.Windows.Forms.ComboBox()
        Me.cbo_RefNo = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_SearchNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmd_close = New System.Windows.Forms.Button()
        Me.btnSearchSOA = New System.Windows.Forms.Button()
        Me.cmd_genReport = New System.Windows.Forms.Button()
        Me.cmd_Refresh = New System.Windows.Forms.Button()
        Me.cmd_search = New System.Windows.Forms.Button()
        CType(Me.DGV_Previous, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV_Current, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DGV_Previous
        '
        Me.DGV_Previous.AllowUserToAddRows = False
        Me.DGV_Previous.AllowUserToDeleteRows = False
        Me.DGV_Previous.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGV_Previous.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGV_Previous.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV_Previous.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGV_Previous.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_Previous.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.BillingPeriod, Me.prevInvNumber, Me.prevInvoiceDate, Me.DueDat, Me.Energy, Me.EnergyVAT, Me.MF, Me.colDefaultInterest, Me.Total, Me.UpdatedBy, Me.UpdatedDate})
        Me.DGV_Previous.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGV_Previous.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGV_Previous.Location = New System.Drawing.Point(3, 16)
        Me.DGV_Previous.MultiSelect = False
        Me.DGV_Previous.Name = "DGV_Previous"
        Me.DGV_Previous.ReadOnly = True
        Me.DGV_Previous.RowHeadersVisible = False
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGV_Previous.RowsDefaultCellStyle = DataGridViewCellStyle10
        Me.DGV_Previous.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGV_Previous.Size = New System.Drawing.Size(876, 164)
        Me.DGV_Previous.TabIndex = 0
        '
        'BillingPeriod
        '
        Me.BillingPeriod.HeaderText = "Billing Period"
        Me.BillingPeriod.Name = "BillingPeriod"
        Me.BillingPeriod.ReadOnly = True
        Me.BillingPeriod.Width = 96
        '
        'prevInvNumber
        '
        DataGridViewCellStyle3.NullValue = Nothing
        Me.prevInvNumber.DefaultCellStyle = DataGridViewCellStyle3
        Me.prevInvNumber.HeaderText = "Invoice Number"
        Me.prevInvNumber.Name = "prevInvNumber"
        Me.prevInvNumber.ReadOnly = True
        Me.prevInvNumber.Visible = False
        Me.prevInvNumber.Width = 115
        '
        'prevInvoiceDate
        '
        DataGridViewCellStyle4.Format = "d"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.prevInvoiceDate.DefaultCellStyle = DataGridViewCellStyle4
        Me.prevInvoiceDate.HeaderText = "InvoiceDate"
        Me.prevInvoiceDate.Name = "prevInvoiceDate"
        Me.prevInvoiceDate.ReadOnly = True
        Me.prevInvoiceDate.Visible = False
        Me.prevInvoiceDate.Width = 94
        '
        'DueDat
        '
        Me.DueDat.HeaderText = "Due Date"
        Me.DueDat.Name = "DueDat"
        Me.DueDat.ReadOnly = True
        Me.DueDat.Width = 74
        '
        'Energy
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.Energy.DefaultCellStyle = DataGridViewCellStyle5
        Me.Energy.HeaderText = "Energy"
        Me.Energy.Name = "Energy"
        Me.Energy.ReadOnly = True
        Me.Energy.Width = 70
        '
        'EnergyVAT
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.EnergyVAT.DefaultCellStyle = DataGridViewCellStyle6
        Me.EnergyVAT.HeaderText = "VAT on Energy"
        Me.EnergyVAT.Name = "EnergyVAT"
        Me.EnergyVAT.ReadOnly = True
        Me.EnergyVAT.Width = 102
        '
        'MF
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N2"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.MF.DefaultCellStyle = DataGridViewCellStyle7
        Me.MF.HeaderText = "Market Fees"
        Me.MF.Name = "MF"
        Me.MF.ReadOnly = True
        Me.MF.Width = 93
        '
        'colDefaultInterest
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "n2"
        Me.colDefaultInterest.DefaultCellStyle = DataGridViewCellStyle8
        Me.colDefaultInterest.HeaderText = "Default Interest"
        Me.colDefaultInterest.Name = "colDefaultInterest"
        Me.colDefaultInterest.ReadOnly = True
        Me.colDefaultInterest.Width = 107
        '
        'Total
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N2"
        DataGridViewCellStyle9.NullValue = Nothing
        Me.Total.DefaultCellStyle = DataGridViewCellStyle9
        Me.Total.HeaderText = "Total"
        Me.Total.Name = "Total"
        Me.Total.ReadOnly = True
        Me.Total.Width = 58
        '
        'UpdatedBy
        '
        Me.UpdatedBy.HeaderText = "Updated By"
        Me.UpdatedBy.Name = "UpdatedBy"
        Me.UpdatedBy.ReadOnly = True
        Me.UpdatedBy.Width = 86
        '
        'UpdatedDate
        '
        Me.UpdatedDate.HeaderText = "UpdatedDate"
        Me.UpdatedDate.Name = "UpdatedDate"
        Me.UpdatedDate.ReadOnly = True
        Me.UpdatedDate.Width = 101
        '
        'DGV_Current
        '
        Me.DGV_Current.AllowUserToAddRows = False
        Me.DGV_Current.AllowUserToDeleteRows = False
        Me.DGV_Current.AllowUserToResizeRows = False
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGV_Current.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle11
        Me.DGV_Current.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_Current.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV_Current.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.DGV_Current.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_Current.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.currBPeriod, Me.currInvoice, Me.currInvDate, Me.currDueDate, Me.currEnergy, Me.currVatEnergy, Me.currMF, Me.colDefultInterest, Me.currTotal, Me.currUpdatedBy, Me.currUpdateDate})
        Me.DGV_Current.Location = New System.Drawing.Point(3, 19)
        Me.DGV_Current.Name = "DGV_Current"
        Me.DGV_Current.ReadOnly = True
        Me.DGV_Current.RowHeadersVisible = False
        DataGridViewCellStyle20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGV_Current.RowsDefaultCellStyle = DataGridViewCellStyle20
        Me.DGV_Current.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGV_Current.Size = New System.Drawing.Size(876, 144)
        Me.DGV_Current.TabIndex = 1
        '
        'currBPeriod
        '
        Me.currBPeriod.HeaderText = "BillingPeriod"
        Me.currBPeriod.Name = "currBPeriod"
        Me.currBPeriod.ReadOnly = True
        Me.currBPeriod.Width = 101
        '
        'currInvoice
        '
        Me.currInvoice.HeaderText = "InvoiceNumber"
        Me.currInvoice.Name = "currInvoice"
        Me.currInvoice.ReadOnly = True
        Me.currInvoice.Width = 115
        '
        'currInvDate
        '
        DataGridViewCellStyle13.Format = "d"
        DataGridViewCellStyle13.NullValue = Nothing
        Me.currInvDate.DefaultCellStyle = DataGridViewCellStyle13
        Me.currInvDate.HeaderText = "InvoiceDate"
        Me.currInvDate.Name = "currInvDate"
        Me.currInvDate.ReadOnly = True
        Me.currInvDate.Width = 95
        '
        'currDueDate
        '
        DataGridViewCellStyle14.Format = "d"
        DataGridViewCellStyle14.NullValue = Nothing
        Me.currDueDate.DefaultCellStyle = DataGridViewCellStyle14
        Me.currDueDate.HeaderText = "DueDate"
        Me.currDueDate.Name = "currDueDate"
        Me.currDueDate.ReadOnly = True
        Me.currDueDate.Width = 77
        '
        'currEnergy
        '
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle15.Format = "N2"
        DataGridViewCellStyle15.NullValue = Nothing
        Me.currEnergy.DefaultCellStyle = DataGridViewCellStyle15
        Me.currEnergy.HeaderText = "Energy"
        Me.currEnergy.Name = "currEnergy"
        Me.currEnergy.ReadOnly = True
        Me.currEnergy.Width = 70
        '
        'currVatEnergy
        '
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle16.Format = "N2"
        DataGridViewCellStyle16.NullValue = Nothing
        Me.currVatEnergy.DefaultCellStyle = DataGridViewCellStyle16
        Me.currVatEnergy.HeaderText = "VATonEnergy"
        Me.currVatEnergy.Name = "currVatEnergy"
        Me.currVatEnergy.ReadOnly = True
        Me.currVatEnergy.Width = 104
        '
        'currMF
        '
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle17.Format = "N2"
        DataGridViewCellStyle17.NullValue = Nothing
        Me.currMF.DefaultCellStyle = DataGridViewCellStyle17
        Me.currMF.HeaderText = "MarketFees"
        Me.currMF.Name = "currMF"
        Me.currMF.ReadOnly = True
        Me.currMF.Width = 98
        '
        'colDefultInterest
        '
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle18.Format = "n2"
        Me.colDefultInterest.DefaultCellStyle = DataGridViewCellStyle18
        Me.colDefultInterest.HeaderText = "Default Interest"
        Me.colDefultInterest.Name = "colDefultInterest"
        Me.colDefultInterest.ReadOnly = True
        Me.colDefultInterest.Width = 107
        '
        'currTotal
        '
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle19.Format = "N2"
        DataGridViewCellStyle19.NullValue = Nothing
        Me.currTotal.DefaultCellStyle = DataGridViewCellStyle19
        Me.currTotal.HeaderText = "Total"
        Me.currTotal.Name = "currTotal"
        Me.currTotal.ReadOnly = True
        Me.currTotal.Width = 58
        '
        'currUpdatedBy
        '
        Me.currUpdatedBy.HeaderText = "UpdatedBy"
        Me.currUpdatedBy.Name = "currUpdatedBy"
        Me.currUpdatedBy.ReadOnly = True
        Me.currUpdatedBy.Width = 90
        '
        'currUpdateDate
        '
        Me.currUpdateDate.HeaderText = "UpdatedDate"
        Me.currUpdateDate.Name = "currUpdateDate"
        Me.currUpdateDate.ReadOnly = True
        Me.currUpdateDate.Width = 101
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DGV_Previous)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(9, 87)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(882, 183)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Previous Transactions:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.DGV_Current)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(9, 275)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(882, 169)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Current Transaction:"
        '
        'cbo_ParticipantId
        '
        Me.cbo_ParticipantId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_ParticipantId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_ParticipantId.FormattingEnabled = True
        Me.cbo_ParticipantId.Location = New System.Drawing.Point(143, 27)
        Me.cbo_ParticipantId.Name = "cbo_ParticipantId"
        Me.cbo_ParticipantId.Size = New System.Drawing.Size(121, 22)
        Me.cbo_ParticipantId.TabIndex = 4
        '
        'cbo_RefNo
        '
        Me.cbo_RefNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_RefNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_RefNo.FormattingEnabled = True
        Me.cbo_RefNo.Location = New System.Drawing.Point(143, 52)
        Me.cbo_RefNo.Name = "cbo_RefNo"
        Me.cbo_RefNo.Size = New System.Drawing.Size(121, 22)
        Me.cbo_RefNo.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(55, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 14)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Participant ID:"
        '
        'txt_SearchNo
        '
        Me.txt_SearchNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_SearchNo.ForeColor = System.Drawing.Color.Gray
        Me.txt_SearchNo.Location = New System.Drawing.Point(685, 11)
        Me.txt_SearchNo.Name = "txt_SearchNo"
        Me.txt_SearchNo.Size = New System.Drawing.Size(162, 20)
        Me.txt_SearchNo.TabIndex = 13
        Me.txt_SearchNo.Text = "Enter Collection Notice No."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(28, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 14)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Reference Number:"
        '
        'cmd_close
        '
        Me.cmd_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_close.BackColor = System.Drawing.Color.White
        Me.cmd_close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_close.Location = New System.Drawing.Point(714, 452)
        Me.cmd_close.Name = "cmd_close"
        Me.cmd_close.Size = New System.Drawing.Size(171, 36)
        Me.cmd_close.TabIndex = 10
        Me.cmd_close.Text = "   Close"
        Me.cmd_close.UseVisualStyleBackColor = False
        '
        'btnSearchSOA
        '
        Me.btnSearchSOA.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSearchSOA.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSearchSOA.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSearchSOA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchSOA.Image = Global.AccountsManagementForms.My.Resources.Resources.search
        Me.btnSearchSOA.Location = New System.Drawing.Point(853, 8)
        Me.btnSearchSOA.Name = "btnSearchSOA"
        Me.btnSearchSOA.Size = New System.Drawing.Size(32, 21)
        Me.btnSearchSOA.TabIndex = 25
        Me.btnSearchSOA.UseVisualStyleBackColor = True
        '
        'cmd_genReport
        '
        Me.cmd_genReport.BackColor = System.Drawing.Color.White
        Me.cmd_genReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_genReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_genReport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_genReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_genReport.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.cmd_genReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_genReport.Location = New System.Drawing.Point(727, 45)
        Me.cmd_genReport.Name = "cmd_genReport"
        Me.cmd_genReport.Size = New System.Drawing.Size(158, 36)
        Me.cmd_genReport.TabIndex = 12
        Me.cmd_genReport.Text = "     Generate Report"
        Me.cmd_genReport.UseVisualStyleBackColor = False
        '
        'cmd_Refresh
        '
        Me.cmd_Refresh.BackColor = System.Drawing.Color.White
        Me.cmd_Refresh.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Refresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Refresh.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Refresh.Image = Global.AccountsManagementForms.My.Resources.Resources.RefreshGreenIcon22x22
        Me.cmd_Refresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Refresh.Location = New System.Drawing.Point(610, 45)
        Me.cmd_Refresh.Name = "cmd_Refresh"
        Me.cmd_Refresh.Size = New System.Drawing.Size(111, 36)
        Me.cmd_Refresh.TabIndex = 11
        Me.cmd_Refresh.Text = "   Refresh"
        Me.cmd_Refresh.UseVisualStyleBackColor = False
        '
        'cmd_search
        '
        Me.cmd_search.BackColor = System.Drawing.Color.White
        Me.cmd_search.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_search.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_search.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_search.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.cmd_search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_search.Location = New System.Drawing.Point(493, 45)
        Me.cmd_search.Name = "cmd_search"
        Me.cmd_search.Size = New System.Drawing.Size(111, 36)
        Me.cmd_search.TabIndex = 7
        Me.cmd_search.Text = "   Search"
        Me.cmd_search.UseVisualStyleBackColor = False
        '
        'frmViewCollectionNotice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(901, 499)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmd_close)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSearchSOA)
        Me.Controls.Add(Me.cbo_RefNo)
        Me.Controls.Add(Me.txt_SearchNo)
        Me.Controls.Add(Me.cbo_ParticipantId)
        Me.Controls.Add(Me.cmd_genReport)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmd_Refresh)
        Me.Controls.Add(Me.cmd_search)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmViewCollectionNotice"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Statement Of Account Inquiry"
        CType(Me.DGV_Previous, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGV_Current, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DGV_Previous As System.Windows.Forms.DataGridView
    Friend WithEvents DGV_Current As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cbo_ParticipantId As System.Windows.Forms.ComboBox
    Friend WithEvents cbo_RefNo As System.Windows.Forms.ComboBox
    Friend WithEvents cmd_search As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmd_close As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmd_Refresh As System.Windows.Forms.Button
    Friend WithEvents cmd_genReport As System.Windows.Forms.Button
    Friend WithEvents txt_SearchNo As System.Windows.Forms.TextBox
    Friend WithEvents btnSearchSOA As System.Windows.Forms.Button
    Friend WithEvents BillingPeriod As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents prevInvNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents prevInvoiceDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DueDat As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Energy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EnergyVAT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDefaultInterest As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UpdatedBy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UpdatedDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents currBPeriod As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents currInvoice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents currInvDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents currDueDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents currEnergy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents currVatEnergy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents currMF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDefultInterest As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents currTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents currUpdatedBy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents currUpdateDate As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
