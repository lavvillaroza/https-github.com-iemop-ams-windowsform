<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOffSetWESMBill
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ddlSettlementRun = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.gpChargeType = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.rbMF = New System.Windows.Forms.RadioButton()
        Me.rbEnergy = New System.Windows.Forms.RadioButton()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.ddlDueDate = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ddlBillingPeriod = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DGridView = New System.Windows.Forms.DataGridView()
        Me.colSettlementID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRegID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colInvoiceNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colInvoiceDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colChargeType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colStlRun = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnClose = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.gpChargeType.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.ddlSettlementRun)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.gpChargeType)
        Me.GroupBox1.Controls.Add(Me.btnRefresh)
        Me.GroupBox1.Controls.Add(Me.btnLoad)
        Me.GroupBox1.Controls.Add(Me.btnGenerate)
        Me.GroupBox1.Controls.Add(Me.ddlDueDate)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.ddlBillingPeriod)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(941, 73)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'ddlSettlementRun
        '
        Me.ddlSettlementRun.BackColor = System.Drawing.Color.White
        Me.ddlSettlementRun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlSettlementRun.FormattingEnabled = True
        Me.ddlSettlementRun.Location = New System.Drawing.Point(503, 21)
        Me.ddlSettlementRun.Name = "ddlSettlementRun"
        Me.ddlSettlementRun.Size = New System.Drawing.Size(119, 22)
        Me.ddlSettlementRun.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(440, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 14)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "STL Run:"
        '
        'gpChargeType
        '
        Me.gpChargeType.Controls.Add(Me.Label4)
        Me.gpChargeType.Controls.Add(Me.rbMF)
        Me.gpChargeType.Controls.Add(Me.rbEnergy)
        Me.gpChargeType.Location = New System.Drawing.Point(6, 13)
        Me.gpChargeType.Name = "gpChargeType"
        Me.gpChargeType.Size = New System.Drawing.Size(206, 51)
        Me.gpChargeType.TabIndex = 10
        Me.gpChargeType.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(7, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 14)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Charge Type:"
        '
        'rbMF
        '
        Me.rbMF.AutoSize = True
        Me.rbMF.Location = New System.Drawing.Point(93, 27)
        Me.rbMF.Name = "rbMF"
        Me.rbMF.Size = New System.Drawing.Size(84, 18)
        Me.rbMF.TabIndex = 8
        Me.rbMF.TabStop = True
        Me.rbMF.Text = "Market Fees"
        Me.rbMF.UseVisualStyleBackColor = True
        '
        'rbEnergy
        '
        Me.rbEnergy.AutoSize = True
        Me.rbEnergy.Location = New System.Drawing.Point(15, 27)
        Me.rbEnergy.Name = "rbEnergy"
        Me.rbEnergy.Size = New System.Drawing.Size(59, 18)
        Me.rbEnergy.TabIndex = 7
        Me.rbEnergy.TabStop = True
        Me.rbEnergy.Text = "Energy"
        Me.rbEnergy.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.White
        Me.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.ForeColor = System.Drawing.Color.Blue
        Me.btnRefresh.Image = Global.AccountsManagementForms.My.Resources.Resources.RefreshGreenIcon22x22
        Me.btnRefresh.Location = New System.Drawing.Point(721, 19)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(35, 30)
        Me.btnRefresh.TabIndex = 8
        Me.btnRefresh.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnRefresh, "Refresh")
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnLoad
        '
        Me.btnLoad.BackColor = System.Drawing.Color.White
        Me.btnLoad.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnLoad.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnLoad.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoad.ForeColor = System.Drawing.Color.Blue
        Me.btnLoad.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.btnLoad.Location = New System.Drawing.Point(639, 19)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(35, 30)
        Me.btnLoad.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.btnLoad, "Search")
        Me.btnLoad.UseVisualStyleBackColor = False
        '
        'btnGenerate
        '
        Me.btnGenerate.BackColor = System.Drawing.Color.White
        Me.btnGenerate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnGenerate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnGenerate.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerate.ForeColor = System.Drawing.Color.Blue
        Me.btnGenerate.Image = Global.AccountsManagementForms.My.Resources.Resources.OkIcon22x22
        Me.btnGenerate.Location = New System.Drawing.Point(680, 19)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(35, 30)
        Me.btnGenerate.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.btnGenerate, "Offset Balance")
        Me.btnGenerate.UseVisualStyleBackColor = False
        '
        'ddlDueDate
        '
        Me.ddlDueDate.BackColor = System.Drawing.Color.White
        Me.ddlDueDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlDueDate.FormattingEnabled = True
        Me.ddlDueDate.Location = New System.Drawing.Point(309, 45)
        Me.ddlDueDate.Name = "ddlDueDate"
        Me.ddlDueDate.Size = New System.Drawing.Size(119, 22)
        Me.ddlDueDate.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(244, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Due Date:"
        '
        'ddlBillingPeriod
        '
        Me.ddlBillingPeriod.BackColor = System.Drawing.Color.White
        Me.ddlBillingPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlBillingPeriod.FormattingEnabled = True
        Me.ddlBillingPeriod.Location = New System.Drawing.Point(309, 21)
        Me.ddlBillingPeriod.Name = "ddlBillingPeriod"
        Me.ddlBillingPeriod.Size = New System.Drawing.Size(119, 22)
        Me.ddlBillingPeriod.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(221, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Billing Period:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.DGridView)
        Me.GroupBox2.Location = New System.Drawing.Point(5, 82)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(941, 395)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(19, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(169, 14)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "Final Statement for Offsetting"
        '
        'DGridView
        '
        Me.DGridView.AllowUserToAddRows = False
        Me.DGridView.AllowUserToResizeColumns = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSettlementID, Me.colRegID, Me.colInvoiceNo, Me.colInvoiceDate, Me.colChargeType, Me.colAmount, Me.colStlRun})
        Me.DGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGridView.Location = New System.Drawing.Point(6, 20)
        Me.DGridView.Name = "DGridView"
        Me.DGridView.RowHeadersVisible = False
        Me.DGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGridView.Size = New System.Drawing.Size(929, 369)
        Me.DGridView.TabIndex = 0
        '
        'colSettlementID
        '
        Me.colSettlementID.HeaderText = "SettlementName"
        Me.colSettlementID.Name = "colSettlementID"
        '
        'colRegID
        '
        Me.colRegID.HeaderText = "ForTheAccountOf"
        Me.colRegID.Name = "colRegID"
        Me.colRegID.Visible = False
        Me.colRegID.Width = 120
        '
        'colInvoiceNo
        '
        Me.colInvoiceNo.HeaderText = "InvoiceNo"
        Me.colInvoiceNo.Name = "colInvoiceNo"
        '
        'colInvoiceDate
        '
        Me.colInvoiceDate.HeaderText = "InvoiceDate"
        Me.colInvoiceDate.Name = "colInvoiceDate"
        '
        'colChargeType
        '
        Me.colChargeType.HeaderText = "ChargeType"
        Me.colChargeType.Name = "colChargeType"
        '
        'colAmount
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.colAmount.DefaultCellStyle = DataGridViewCellStyle2
        Me.colAmount.HeaderText = "Amount"
        Me.colAmount.Name = "colAmount"
        '
        'colStlRun
        '
        Me.colStlRun.HeaderText = "SettlementRun"
        Me.colStlRun.Name = "colStlRun"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.White
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(801, 483)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(139, 39)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'frmOffSetWESMBill
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(952, 534)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnClose)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmOffSetWESMBill"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Offset WESM Bills"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gpChargeType.ResumeLayout(False)
        Me.gpChargeType.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.DGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ddlDueDate As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ddlBillingPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents DGridView As System.Windows.Forms.DataGridView
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents gpChargeType As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents rbMF As System.Windows.Forms.RadioButton
    Friend WithEvents rbEnergy As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents colSettlementID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRegID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colInvoiceNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colInvoiceDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colChargeType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStlRun As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ddlSettlementRun As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
