<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUploadedWESMBill
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.chckStlRun = New System.Windows.Forms.CheckBox()
        Me.ddlBillingPeriod = New System.Windows.Forms.ComboBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.txtLoading = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ViewStatus = New System.Windows.Forms.ToolStripProgressBar()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ddlSTLRun = New System.Windows.Forms.ComboBox()
        Me.rbEnergy = New System.Windows.Forms.RadioButton()
        Me.rbMF = New System.Windows.Forms.RadioButton()
        Me.gpChargeType = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DGridView = New System.Windows.Forms.DataGridView()
        Me.colInvoiceNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colInvoiceDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colStlRun = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEnergyMFBCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEnergyMF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colVatBCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colVAT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUpdatedBy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUpdatedDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbInvoiceDate = New System.Windows.Forms.RadioButton()
        Me.rbBillingPeriod = New System.Windows.Forms.RadioButton()
        Me.chckParticipant = New System.Windows.Forms.CheckBox()
        Me.ddlParticipant = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtNSSEnergyMF = New System.Windows.Forms.TextBox()
        Me.txtAPEnergyMF = New System.Windows.Forms.TextBox()
        Me.txtAREnergyMF = New System.Windows.Forms.TextBox()
        Me.lblEnergyMF = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtNSSVat = New System.Windows.Forms.TextBox()
        Me.txtAPVat = New System.Windows.Forms.TextBox()
        Me.txtARVat = New System.Windows.Forms.TextBox()
        Me.lblVat = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnDownload = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnShowReport = New System.Windows.Forms.Button()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.StatusStrip1.SuspendLayout()
        Me.gpChargeType.SuspendLayout()
        CType(Me.DGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'chckStlRun
        '
        Me.chckStlRun.AutoSize = True
        Me.chckStlRun.Location = New System.Drawing.Point(324, 18)
        Me.chckStlRun.Name = "chckStlRun"
        Me.chckStlRun.Size = New System.Drawing.Size(101, 16)
        Me.chckStlRun.TabIndex = 1
        Me.chckStlRun.Text = "Settlement Run:"
        Me.chckStlRun.UseVisualStyleBackColor = True
        '
        'ddlBillingPeriod
        '
        Me.ddlBillingPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlBillingPeriod.FormattingEnabled = True
        Me.ddlBillingPeriod.Location = New System.Drawing.Point(105, 15)
        Me.ddlBillingPeriod.Name = "ddlBillingPeriod"
        Me.ddlBillingPeriod.Size = New System.Drawing.Size(193, 20)
        Me.ddlBillingPeriod.TabIndex = 2
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtLoading, Me.ViewStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 515)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1048, 22)
        Me.StatusStrip1.TabIndex = 5
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'txtLoading
        '
        Me.txtLoading.Name = "txtLoading"
        Me.txtLoading.Size = New System.Drawing.Size(53, 17)
        Me.txtLoading.Text = "Loading:"
        '
        'ViewStatus
        '
        Me.ViewStatus.Name = "ViewStatus"
        Me.ViewStatus.Size = New System.Drawing.Size(100, 16)
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'ddlSTLRun
        '
        Me.ddlSTLRun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlSTLRun.FormattingEnabled = True
        Me.ddlSTLRun.Location = New System.Drawing.Point(436, 15)
        Me.ddlSTLRun.Name = "ddlSTLRun"
        Me.ddlSTLRun.Size = New System.Drawing.Size(191, 20)
        Me.ddlSTLRun.TabIndex = 6
        '
        'rbEnergy
        '
        Me.rbEnergy.AutoSize = True
        Me.rbEnergy.Location = New System.Drawing.Point(16, 32)
        Me.rbEnergy.Name = "rbEnergy"
        Me.rbEnergy.Size = New System.Drawing.Size(58, 16)
        Me.rbEnergy.TabIndex = 7
        Me.rbEnergy.TabStop = True
        Me.rbEnergy.Text = "Energy"
        Me.rbEnergy.UseVisualStyleBackColor = True
        '
        'rbMF
        '
        Me.rbMF.AutoSize = True
        Me.rbMF.Location = New System.Drawing.Point(93, 32)
        Me.rbMF.Name = "rbMF"
        Me.rbMF.Size = New System.Drawing.Size(85, 16)
        Me.rbMF.TabIndex = 8
        Me.rbMF.TabStop = True
        Me.rbMF.Text = "Market Fees"
        Me.rbMF.UseVisualStyleBackColor = True
        '
        'gpChargeType
        '
        Me.gpChargeType.Controls.Add(Me.Label4)
        Me.gpChargeType.Controls.Add(Me.rbMF)
        Me.gpChargeType.Controls.Add(Me.rbEnergy)
        Me.gpChargeType.Location = New System.Drawing.Point(14, 12)
        Me.gpChargeType.Name = "gpChargeType"
        Me.gpChargeType.Size = New System.Drawing.Size(206, 75)
        Me.gpChargeType.TabIndex = 9
        Me.gpChargeType.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(7, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 14)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Charge Type:"
        '
        'DGridView
        '
        Me.DGridView.AllowUserToAddRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colInvoiceNo, Me.colParticipantID, Me.colInvoiceDate, Me.colDueDate, Me.colStlRun, Me.colEnergyMFBCode, Me.colEnergyMF, Me.colVatBCode, Me.colVAT, Me.colTotal, Me.colUpdatedBy, Me.colUpdatedDate})
        Me.DGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGridView.Location = New System.Drawing.Point(5, 93)
        Me.DGridView.Name = "DGridView"
        Me.DGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGridView.Size = New System.Drawing.Size(1031, 269)
        Me.DGridView.TabIndex = 12
        '
        'colInvoiceNo
        '
        Me.colInvoiceNo.HeaderText = "InvoiceNo"
        Me.colInvoiceNo.Name = "colInvoiceNo"
        Me.colInvoiceNo.ReadOnly = True
        '
        'colParticipantID
        '
        Me.colParticipantID.HeaderText = "ParticipantID"
        Me.colParticipantID.Name = "colParticipantID"
        '
        'colInvoiceDate
        '
        Me.colInvoiceDate.HeaderText = "InvoiceDate"
        Me.colInvoiceDate.Name = "colInvoiceDate"
        '
        'colDueDate
        '
        Me.colDueDate.HeaderText = "DueDate"
        Me.colDueDate.Name = "colDueDate"
        '
        'colStlRun
        '
        Me.colStlRun.HeaderText = "StlRun"
        Me.colStlRun.Name = "colStlRun"
        '
        'colEnergyMFBCode
        '
        Me.colEnergyMFBCode.HeaderText = "Energy-BatchCode"
        Me.colEnergyMFBCode.Name = "colEnergyMFBCode"
        '
        'colEnergyMF
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colEnergyMF.DefaultCellStyle = DataGridViewCellStyle2
        Me.colEnergyMF.HeaderText = "Energy"
        Me.colEnergyMF.Name = "colEnergyMF"
        '
        'colVatBCode
        '
        Me.colVatBCode.HeaderText = "VAT-BatchCode"
        Me.colVatBCode.Name = "colVatBCode"
        '
        'colVAT
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colVAT.DefaultCellStyle = DataGridViewCellStyle3
        Me.colVAT.HeaderText = "VAT"
        Me.colVAT.Name = "colVAT"
        '
        'colTotal
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colTotal.DefaultCellStyle = DataGridViewCellStyle4
        Me.colTotal.HeaderText = "Total"
        Me.colTotal.Name = "colTotal"
        '
        'colUpdatedBy
        '
        Me.colUpdatedBy.HeaderText = "UpdatedBy"
        Me.colUpdatedBy.Name = "colUpdatedBy"
        '
        'colUpdatedDate
        '
        Me.colUpdatedDate.HeaderText = "UpdatedDate"
        Me.colUpdatedDate.Name = "colUpdatedDate"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbInvoiceDate)
        Me.GroupBox1.Controls.Add(Me.rbBillingPeriod)
        Me.GroupBox1.Controls.Add(Me.chckParticipant)
        Me.GroupBox1.Controls.Add(Me.ddlParticipant)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtTo)
        Me.GroupBox1.Controls.Add(Me.dtFrom)
        Me.GroupBox1.Controls.Add(Me.ddlBillingPeriod)
        Me.GroupBox1.Controls.Add(Me.chckStlRun)
        Me.GroupBox1.Controls.Add(Me.ddlSTLRun)
        Me.GroupBox1.Location = New System.Drawing.Point(226, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(637, 75)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        '
        'rbInvoiceDate
        '
        Me.rbInvoiceDate.AutoSize = True
        Me.rbInvoiceDate.Location = New System.Drawing.Point(15, 44)
        Me.rbInvoiceDate.Name = "rbInvoiceDate"
        Me.rbInvoiceDate.Size = New System.Drawing.Size(84, 16)
        Me.rbInvoiceDate.TabIndex = 17
        Me.rbInvoiceDate.TabStop = True
        Me.rbInvoiceDate.Text = "Invoice Date"
        Me.rbInvoiceDate.UseVisualStyleBackColor = True
        '
        'rbBillingPeriod
        '
        Me.rbBillingPeriod.AutoSize = True
        Me.rbBillingPeriod.Location = New System.Drawing.Point(15, 20)
        Me.rbBillingPeriod.Name = "rbBillingPeriod"
        Me.rbBillingPeriod.Size = New System.Drawing.Size(84, 16)
        Me.rbBillingPeriod.TabIndex = 16
        Me.rbBillingPeriod.TabStop = True
        Me.rbBillingPeriod.Text = "Billing Period"
        Me.rbBillingPeriod.UseVisualStyleBackColor = True
        '
        'chckParticipant
        '
        Me.chckParticipant.AutoSize = True
        Me.chckParticipant.Location = New System.Drawing.Point(324, 45)
        Me.chckParticipant.Name = "chckParticipant"
        Me.chckParticipant.Size = New System.Drawing.Size(92, 16)
        Me.chckParticipant.TabIndex = 14
        Me.chckParticipant.Text = "Participant ID:"
        Me.chckParticipant.UseVisualStyleBackColor = True
        '
        'ddlParticipant
        '
        Me.ddlParticipant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlParticipant.FormattingEnabled = True
        Me.ddlParticipant.Location = New System.Drawing.Point(436, 42)
        Me.ddlParticipant.Name = "ddlParticipant"
        Me.ddlParticipant.Size = New System.Drawing.Size(191, 20)
        Me.ddlParticipant.TabIndex = 15
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(196, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(11, 14)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "-"
        '
        'dtTo
        '
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTo.Location = New System.Drawing.Point(213, 42)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(85, 20)
        Me.dtTo.TabIndex = 10
        '
        'dtFrom
        '
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFrom.Location = New System.Drawing.Point(105, 42)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(85, 20)
        Me.dtFrom.TabIndex = 9
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.txtNSSEnergyMF)
        Me.GroupBox2.Controls.Add(Me.txtAPEnergyMF)
        Me.GroupBox2.Controls.Add(Me.txtAREnergyMF)
        Me.GroupBox2.Controls.Add(Me.lblEnergyMF)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Location = New System.Drawing.Point(557, 368)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(236, 97)
        Me.GroupBox2.TabIndex = 22
        Me.GroupBox2.TabStop = False
        '
        'txtNSSEnergyMF
        '
        Me.txtNSSEnergyMF.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNSSEnergyMF.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNSSEnergyMF.ForeColor = System.Drawing.Color.Black
        Me.txtNSSEnergyMF.Location = New System.Drawing.Point(81, 68)
        Me.txtNSSEnergyMF.Name = "txtNSSEnergyMF"
        Me.txtNSSEnergyMF.ReadOnly = True
        Me.txtNSSEnergyMF.Size = New System.Drawing.Size(147, 20)
        Me.txtNSSEnergyMF.TabIndex = 26
        Me.txtNSSEnergyMF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAPEnergyMF
        '
        Me.txtAPEnergyMF.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAPEnergyMF.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAPEnergyMF.ForeColor = System.Drawing.Color.Black
        Me.txtAPEnergyMF.Location = New System.Drawing.Point(81, 17)
        Me.txtAPEnergyMF.Name = "txtAPEnergyMF"
        Me.txtAPEnergyMF.ReadOnly = True
        Me.txtAPEnergyMF.Size = New System.Drawing.Size(147, 20)
        Me.txtAPEnergyMF.TabIndex = 25
        Me.txtAPEnergyMF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAREnergyMF
        '
        Me.txtAREnergyMF.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAREnergyMF.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAREnergyMF.ForeColor = System.Drawing.Color.Black
        Me.txtAREnergyMF.Location = New System.Drawing.Point(80, 42)
        Me.txtAREnergyMF.Name = "txtAREnergyMF"
        Me.txtAREnergyMF.ReadOnly = True
        Me.txtAREnergyMF.Size = New System.Drawing.Size(148, 20)
        Me.txtAREnergyMF.TabIndex = 24
        Me.txtAREnergyMF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblEnergyMF
        '
        Me.lblEnergyMF.AutoSize = True
        Me.lblEnergyMF.Font = New System.Drawing.Font("Helvetica", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnergyMF.ForeColor = System.Drawing.Color.Blue
        Me.lblEnergyMF.Location = New System.Drawing.Point(9, 0)
        Me.lblEnergyMF.Name = "lblEnergyMF"
        Me.lblEnergyMF.Size = New System.Drawing.Size(12, 17)
        Me.lblEnergyMF.TabIndex = 23
        Me.lblEnergyMF.Text = " "
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(11, 71)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(63, 14)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Total NSS:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(20, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 14)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Total AP:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(20, 45)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 14)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Total AR:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.txtNSSVat)
        Me.GroupBox3.Controls.Add(Me.txtAPVat)
        Me.GroupBox3.Controls.Add(Me.txtARVat)
        Me.GroupBox3.Controls.Add(Me.lblVat)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Location = New System.Drawing.Point(799, 368)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(237, 99)
        Me.GroupBox3.TabIndex = 23
        Me.GroupBox3.TabStop = False
        '
        'txtNSSVat
        '
        Me.txtNSSVat.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNSSVat.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNSSVat.ForeColor = System.Drawing.Color.Black
        Me.txtNSSVat.Location = New System.Drawing.Point(82, 68)
        Me.txtNSSVat.Name = "txtNSSVat"
        Me.txtNSSVat.ReadOnly = True
        Me.txtNSSVat.Size = New System.Drawing.Size(147, 20)
        Me.txtNSSVat.TabIndex = 29
        Me.txtNSSVat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAPVat
        '
        Me.txtAPVat.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAPVat.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAPVat.ForeColor = System.Drawing.Color.Black
        Me.txtAPVat.Location = New System.Drawing.Point(82, 19)
        Me.txtAPVat.Name = "txtAPVat"
        Me.txtAPVat.ReadOnly = True
        Me.txtAPVat.Size = New System.Drawing.Size(147, 20)
        Me.txtAPVat.TabIndex = 28
        Me.txtAPVat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtARVat
        '
        Me.txtARVat.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtARVat.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtARVat.ForeColor = System.Drawing.Color.Black
        Me.txtARVat.Location = New System.Drawing.Point(82, 44)
        Me.txtARVat.Name = "txtARVat"
        Me.txtARVat.ReadOnly = True
        Me.txtARVat.Size = New System.Drawing.Size(147, 20)
        Me.txtARVat.TabIndex = 27
        Me.txtARVat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblVat
        '
        Me.lblVat.AutoSize = True
        Me.lblVat.Font = New System.Drawing.Font("Helvetica", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVat.ForeColor = System.Drawing.Color.Blue
        Me.lblVat.Location = New System.Drawing.Point(9, 0)
        Me.lblVat.Name = "lblVat"
        Me.lblVat.Size = New System.Drawing.Size(12, 17)
        Me.lblVat.TabIndex = 23
        Me.lblVat.Text = " "
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(5, 73)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(63, 14)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "Total NSS:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(14, 24)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(56, 14)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Total AP:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(14, 47)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(57, 14)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Total AR:"
        '
        'btnDownload
        '
        Me.btnDownload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDownload.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnDownload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnDownload.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDownload.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDownload.ForeColor = System.Drawing.Color.Black
        Me.btnDownload.Image = Global.AccountsManagementForms.My.Resources.Resources.DownloadIcon22x22
        Me.btnDownload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDownload.Location = New System.Drawing.Point(750, 473)
        Me.btnDownload.Name = "btnDownload"
        Me.btnDownload.Size = New System.Drawing.Size(140, 39)
        Me.btnDownload.TabIndex = 24
        Me.btnDownload.Text = "Download"
        Me.btnDownload.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(896, 473)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(140, 39)
        Me.btnClose.TabIndex = 21
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnShowReport
        '
        Me.btnShowReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnShowReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnShowReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnShowReport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnShowReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShowReport.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowReport.ForeColor = System.Drawing.Color.Black
        Me.btnShowReport.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.btnShowReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnShowReport.Location = New System.Drawing.Point(604, 471)
        Me.btnShowReport.Name = "btnShowReport"
        Me.btnShowReport.Size = New System.Drawing.Size(140, 39)
        Me.btnShowReport.TabIndex = 20
        Me.btnShowReport.Text = "Generate Report"
        Me.btnShowReport.UseVisualStyleBackColor = True
        '
        'btnLoad
        '
        Me.btnLoad.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnLoad.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnLoad.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoad.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.ForeColor = System.Drawing.Color.Black
        Me.btnLoad.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.btnLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLoad.Location = New System.Drawing.Point(869, 17)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(35, 30)
        Me.btnLoad.TabIndex = 11
        Me.btnLoad.Text = "     Search"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.Black
        Me.btnClear.Image = Global.AccountsManagementForms.My.Resources.Resources.CancelIconRed22x22
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.Location = New System.Drawing.Point(869, 57)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(35, 30)
        Me.btnClear.TabIndex = 10
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'frmUploadedWESMBill
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1048, 537)
        Me.Controls.Add(Me.btnDownload)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnShowReport)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DGridView)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.gpChargeType)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmUploadedWESMBill"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WESM Bill Inquiry"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.gpChargeType.ResumeLayout(False)
        Me.gpChargeType.PerformLayout()
        CType(Me.DGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chckStlRun As System.Windows.Forms.CheckBox
    Friend WithEvents ddlBillingPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents txtLoading As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ViewStatus As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ddlSTLRun As System.Windows.Forms.ComboBox
    Friend WithEvents rbEnergy As System.Windows.Forms.RadioButton
    Friend WithEvents rbMF As System.Windows.Forms.RadioButton
    Friend WithEvents gpChargeType As System.Windows.Forms.GroupBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents DGridView As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnShowReport As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblEnergyMF As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtNSSEnergyMF As System.Windows.Forms.TextBox
    Friend WithEvents txtAPEnergyMF As System.Windows.Forms.TextBox
    Friend WithEvents txtAREnergyMF As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNSSVat As System.Windows.Forms.TextBox
    Friend WithEvents txtAPVat As System.Windows.Forms.TextBox
    Friend WithEvents txtARVat As System.Windows.Forms.TextBox
    Friend WithEvents lblVat As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents chckParticipant As System.Windows.Forms.CheckBox
    Friend WithEvents ddlParticipant As System.Windows.Forms.ComboBox
    Friend WithEvents rbInvoiceDate As System.Windows.Forms.RadioButton
    Friend WithEvents rbBillingPeriod As System.Windows.Forms.RadioButton
    Friend WithEvents btnDownload As System.Windows.Forms.Button
    Friend WithEvents colInvoiceNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colInvoiceDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDueDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStlRun As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEnergyMFBCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEnergyMF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colVatBCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colVAT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUpdatedBy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUpdatedDate As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
