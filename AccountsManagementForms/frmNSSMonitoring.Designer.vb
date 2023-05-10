<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNSSMonitoring
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNSSMonitoring))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnCompute = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.DGridViewNSSRA = New System.Windows.Forms.DataGridView()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNSSRAAdvanceSearch = New System.Windows.Forms.ToolStripButton()
        Me.btnNSSRASearch = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.txtNSSRASearch = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DataGridView3 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.DateTimePicker3 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker4 = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ToolStrip3 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.ToolStrip5 = New System.Windows.Forms.ToolStrip()
        Me.btnNSSSummaryAdvanceSearch = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnNSSSummaryUpload = New System.Windows.Forms.ToolStripButton()
        Me.btnNSSSummarySearch = New System.Windows.Forms.ToolStripButton()
        Me.txtNSSSummarySearch = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripLabel4 = New System.Windows.Forms.ToolStripLabel()
        Me.btnNSSSummaryViewSummary = New System.Windows.Forms.Button()
        Me.btnNSSSummarySave = New System.Windows.Forms.Button()
        Me.DGridNSSSummary = New System.Windows.Forms.DataGridView()
        Me.colNSSSummaryBatchCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNSSSummaryYear = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNSSSummaryQuarterly = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNSSSummaryAllocationDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNSSSummaryPeriod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNSSSummaryIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNSSSummaryParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNSSSummaryTotalNSSInterest = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNSSSummaryTotalNSSInterestNetWTax = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNSSSummaryTotalSTLInterest = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNSSSummarySTLInterestNetWTax = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNSSSummaryTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DGridViewNSSRA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.ToolStrip3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.ToolStrip5.SuspendLayout()
        CType(Me.DGridNSSSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCompute
        '
        Me.btnCompute.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnCompute.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnCompute.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnCompute.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCompute.Location = New System.Drawing.Point(917, 413)
        Me.btnCompute.Name = "btnCompute"
        Me.btnCompute.Size = New System.Drawing.Size(117, 39)
        Me.btnCompute.TabIndex = 4
        Me.btnCompute.Text = "&Compute NSSRA"
        Me.btnCompute.UseVisualStyleBackColor = True
        Me.btnCompute.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(12, 17)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1048, 488)
        Me.TabControl1.TabIndex = 7
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.DGridViewNSSRA)
        Me.TabPage1.Controls.Add(Me.ToolStrip1)
        Me.TabPage1.Controls.Add(Me.btnCompute)
        Me.TabPage1.Location = New System.Drawing.Point(4, 21)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1040, 463)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "NSSRA"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'DGridViewNSSRA
        '
        Me.DGridViewNSSRA.AllowUserToAddRows = False
        Me.DGridViewNSSRA.AllowUserToDeleteRows = False
        Me.DGridViewNSSRA.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGridViewNSSRA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridViewNSSRA.Location = New System.Drawing.Point(6, 31)
        Me.DGridViewNSSRA.Name = "DGridViewNSSRA"
        Me.DGridViewNSSRA.Size = New System.Drawing.Size(1031, 376)
        Me.DGridViewNSSRA.TabIndex = 7
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.White
        Me.ToolStrip1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNSSRAAdvanceSearch, Me.btnNSSRASearch, Me.ToolStripSeparator2, Me.txtNSSRASearch, Me.ToolStripLabel1})
        Me.ToolStrip1.Location = New System.Drawing.Point(3, 3)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1034, 25)
        Me.ToolStrip1.TabIndex = 6
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNSSRAAdvanceSearch
        '
        Me.btnNSSRAAdvanceSearch.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNSSRAAdvanceSearch.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.btnNSSRAAdvanceSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNSSRAAdvanceSearch.Name = "btnNSSRAAdvanceSearch"
        Me.btnNSSRAAdvanceSearch.Size = New System.Drawing.Size(63, 22)
        Me.btnNSSRAAdvanceSearch.Text = "&Search"
        '
        'btnNSSRASearch
        '
        Me.btnNSSRASearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnNSSRASearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnNSSRASearch.Image = Global.AccountsManagementForms.My.Resources.Resources.SearchIconColored22x22
        Me.btnNSSRASearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNSSRASearch.Name = "btnNSSRASearch"
        Me.btnNSSRASearch.Size = New System.Drawing.Size(23, 22)
        Me.btnNSSRASearch.Text = "ToolStripButton1"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'txtNSSRASearch
        '
        Me.txtNSSRASearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.txtNSSRASearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNSSRASearch.Name = "txtNSSRASearch"
        Me.txtNSSRASearch.Size = New System.Drawing.Size(100, 25)
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripLabel1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(82, 22)
        Me.ToolStripLabel1.Text = "Participant ID:"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.ToolStrip2)
        Me.TabPage2.Controls.Add(Me.Button1)
        Me.TabPage2.Controls.Add(Me.DataGridView2)
        Me.TabPage2.Controls.Add(Me.TextBox1)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.btnGenerate)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 21)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1040, 463)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Bank Statement"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(13, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 12)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Transaction Date"
        '
        'ToolStrip2
        '
        Me.ToolStrip2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton3, Me.ToolStripSeparator1, Me.ToolStripTextBox1, Me.ToolStripLabel2})
        Me.ToolStrip2.Location = New System.Drawing.Point(3, 3)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(1034, 25)
        Me.ToolStrip2.TabIndex = 14
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(112, 22)
        Me.ToolStripButton1.Text = "&Advanced Search"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton3.Text = "ToolStripButton1"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(100, 25)
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripLabel2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(73, 22)
        Me.ToolStripLabel2.Text = "Participant ID:"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(598, 53)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(117, 23)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "&Save"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column7, Me.Column8, Me.Column9, Me.Column10, Me.Column11})
        Me.DataGridView2.Location = New System.Drawing.Point(14, 87)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(1020, 400)
        Me.DataGridView2.TabIndex = 12
        '
        'Column7
        '
        Me.Column7.HeaderText = "TransactionDate"
        Me.Column7.Name = "Column7"
        '
        'Column8
        '
        Me.Column8.HeaderText = "Principal"
        Me.Column8.Name = "Column8"
        '
        'Column9
        '
        Me.Column9.HeaderText = "InterestRate"
        Me.Column9.Name = "Column9"
        '
        'Column10
        '
        Me.Column10.HeaderText = "GrossInterest"
        Me.Column10.Name = "Column10"
        '
        'Column11
        '
        Me.Column11.HeaderText = "Credit/Debit"
        Me.Column11.Name = "Column11"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(369, 56)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(292, 59)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 12)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Interest Rate:"
        '
        'btnGenerate
        '
        Me.btnGenerate.Location = New System.Drawing.Point(475, 53)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(117, 23)
        Me.btnGenerate.TabIndex = 9
        Me.btnGenerate.Text = "&Generate"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DateTimePicker2)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(6, 40)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(280, 41)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker2.Location = New System.Drawing.Point(175, 17)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(99, 20)
        Me.DateTimePicker2.TabIndex = 3
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(43, 17)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(99, 20)
        Me.DateTimePicker1.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(148, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(21, 12)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "To:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 12)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "From:"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Label7)
        Me.TabPage3.Controls.Add(Me.DataGridView3)
        Me.TabPage3.Controls.Add(Me.Button2)
        Me.TabPage3.Controls.Add(Me.Button3)
        Me.TabPage3.Controls.Add(Me.GroupBox2)
        Me.TabPage3.Controls.Add(Me.ToolStrip3)
        Me.TabPage3.Location = New System.Drawing.Point(4, 21)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(1040, 463)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Interest Allocation"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(19, 39)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(89, 12)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Transaction Date"
        '
        'DataGridView3
        '
        Me.DataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView3.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.Column12, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6})
        Me.DataGridView3.Location = New System.Drawing.Point(11, 88)
        Me.DataGridView3.Name = "DataGridView3"
        Me.DataGridView3.Size = New System.Drawing.Size(1023, 396)
        Me.DataGridView3.TabIndex = 17
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "IDNumber"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "ParticipantID"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'Column12
        '
        Me.Column12.HeaderText = "NSSRA"
        Me.Column12.Name = "Column12"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Jan. 1-25, 2012                   20                           2.50%"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 120
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Feb. 1-25, 2012                   30                           2.50%"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 120
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Mar. 1-25, 2012                   45                           2.50%"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Width = 120
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "Apr. 1-25, 2012                   65                           2.50%"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.Width = 120
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(426, 51)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(117, 23)
        Me.Button2.TabIndex = 16
        Me.Button2.Text = "&Save"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(303, 51)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(117, 23)
        Me.Button3.TabIndex = 15
        Me.Button3.Text = "&Generate"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.DateTimePicker3)
        Me.GroupBox2.Controls.Add(Me.DateTimePicker4)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(11, 37)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(280, 45)
        Me.GroupBox2.TabIndex = 14
        Me.GroupBox2.TabStop = False
        '
        'DateTimePicker3
        '
        Me.DateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker3.Location = New System.Drawing.Point(175, 17)
        Me.DateTimePicker3.Name = "DateTimePicker3"
        Me.DateTimePicker3.Size = New System.Drawing.Size(99, 20)
        Me.DateTimePicker3.TabIndex = 3
        '
        'DateTimePicker4
        '
        Me.DateTimePicker4.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker4.Location = New System.Drawing.Point(43, 17)
        Me.DateTimePicker4.Name = "DateTimePicker4"
        Me.DateTimePicker4.Size = New System.Drawing.Size(99, 20)
        Me.DateTimePicker4.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(148, 20)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(21, 12)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "To:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 20)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(34, 12)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "From:"
        '
        'ToolStrip3
        '
        Me.ToolStrip3.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton2})
        Me.ToolStrip3.Location = New System.Drawing.Point(3, 3)
        Me.ToolStrip3.Name = "ToolStrip3"
        Me.ToolStrip3.Size = New System.Drawing.Size(1034, 25)
        Me.ToolStrip3.TabIndex = 8
        Me.ToolStrip3.Text = "ToolStrip3"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(112, 22)
        Me.ToolStripButton2.Text = "&Advanced Search"
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.GroupBox3)
        Me.TabPage4.Controls.Add(Me.btnNSSSummaryViewSummary)
        Me.TabPage4.Controls.Add(Me.btnNSSSummarySave)
        Me.TabPage4.Controls.Add(Me.DGridNSSSummary)
        Me.TabPage4.Location = New System.Drawing.Point(4, 21)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(1040, 463)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Summary"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.ToolStrip5)
        Me.GroupBox3.Location = New System.Drawing.Point(3, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1031, 46)
        Me.GroupBox3.TabIndex = 30
        Me.GroupBox3.TabStop = False
        '
        'ToolStrip5
        '
        Me.ToolStrip5.BackColor = System.Drawing.Color.White
        Me.ToolStrip5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip5.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip5.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNSSSummaryAdvanceSearch, Me.ToolStripSeparator6, Me.btnNSSSummaryUpload, Me.btnNSSSummarySearch, Me.txtNSSSummarySearch, Me.ToolStripLabel4})
        Me.ToolStrip5.Location = New System.Drawing.Point(3, 18)
        Me.ToolStrip5.Name = "ToolStrip5"
        Me.ToolStrip5.Size = New System.Drawing.Size(1025, 25)
        Me.ToolStrip5.TabIndex = 0
        Me.ToolStrip5.Text = "ToolStrip5"
        '
        'btnNSSSummaryAdvanceSearch
        '
        Me.btnNSSSummaryAdvanceSearch.ForeColor = System.Drawing.Color.Black
        Me.btnNSSSummaryAdvanceSearch.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.btnNSSSummaryAdvanceSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNSSSummaryAdvanceSearch.Name = "btnNSSSummaryAdvanceSearch"
        Me.btnNSSSummaryAdvanceSearch.Size = New System.Drawing.Size(112, 22)
        Me.btnNSSSummaryAdvanceSearch.Text = "&Advanced Search"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'btnNSSSummaryUpload
        '
        Me.btnNSSSummaryUpload.ForeColor = System.Drawing.Color.Black
        Me.btnNSSSummaryUpload.Image = Global.AccountsManagementForms.My.Resources.Resources.Upload2Icon22x22
        Me.btnNSSSummaryUpload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNSSSummaryUpload.Name = "btnNSSSummaryUpload"
        Me.btnNSSSummaryUpload.Size = New System.Drawing.Size(108, 22)
        Me.btnNSSSummaryUpload.Text = "&Upload From File"
        '
        'btnNSSSummarySearch
        '
        Me.btnNSSSummarySearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnNSSSummarySearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnNSSSummarySearch.Image = Global.AccountsManagementForms.My.Resources.Resources.SearchIconColored22x22
        Me.btnNSSSummarySearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNSSSummarySearch.Name = "btnNSSSummarySearch"
        Me.btnNSSSummarySearch.Size = New System.Drawing.Size(23, 22)
        Me.btnNSSSummarySearch.Text = "Type participant id"
        '
        'txtNSSSummarySearch
        '
        Me.txtNSSSummarySearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.txtNSSSummarySearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNSSSummarySearch.Name = "txtNSSSummarySearch"
        Me.txtNSSSummarySearch.Size = New System.Drawing.Size(100, 25)
        '
        'ToolStripLabel4
        '
        Me.ToolStripLabel4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripLabel4.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel4.ForeColor = System.Drawing.Color.Blue
        Me.ToolStripLabel4.LinkColor = System.Drawing.Color.Black
        Me.ToolStripLabel4.Name = "ToolStripLabel4"
        Me.ToolStripLabel4.Size = New System.Drawing.Size(73, 22)
        Me.ToolStripLabel4.Text = "Participant ID:"
        '
        'btnNSSSummaryViewSummary
        '
        Me.btnNSSSummaryViewSummary.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNSSSummaryViewSummary.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNSSSummaryViewSummary.ForeColor = System.Drawing.Color.Blue
        Me.btnNSSSummaryViewSummary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNSSSummaryViewSummary.Location = New System.Drawing.Point(896, 430)
        Me.btnNSSSummaryViewSummary.Name = "btnNSSSummaryViewSummary"
        Me.btnNSSSummaryViewSummary.Size = New System.Drawing.Size(141, 27)
        Me.btnNSSSummaryViewSummary.TabIndex = 28
        Me.btnNSSSummaryViewSummary.Text = "&View Summary"
        Me.btnNSSSummaryViewSummary.UseVisualStyleBackColor = True
        '
        'btnNSSSummarySave
        '
        Me.btnNSSSummarySave.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNSSSummarySave.ForeColor = System.Drawing.Color.Blue
        Me.btnNSSSummarySave.Image = Global.AccountsManagementForms.My.Resources.Resources.save
        Me.btnNSSSummarySave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNSSSummarySave.Location = New System.Drawing.Point(746, 466)
        Me.btnNSSSummarySave.Name = "btnNSSSummarySave"
        Me.btnNSSSummarySave.Size = New System.Drawing.Size(141, 27)
        Me.btnNSSSummarySave.TabIndex = 22
        Me.btnNSSSummarySave.Text = "&Save"
        Me.btnNSSSummarySave.UseVisualStyleBackColor = True
        '
        'DGridNSSSummary
        '
        Me.DGridNSSSummary.AllowUserToAddRows = False
        Me.DGridNSSSummary.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridNSSSummary.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGridNSSSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridNSSSummary.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colNSSSummaryBatchCode, Me.colNSSSummaryYear, Me.colNSSSummaryQuarterly, Me.colNSSSummaryAllocationDate, Me.colNSSSummaryPeriod, Me.colNSSSummaryIDNumber, Me.colNSSSummaryParticipantID, Me.colNSSSummaryTotalNSSInterest, Me.colNSSSummaryTotalNSSInterestNetWTax, Me.colNSSSummaryTotalSTLInterest, Me.colNSSSummarySTLInterestNetWTax, Me.colNSSSummaryTotal})
        Me.DGridNSSSummary.Location = New System.Drawing.Point(6, 58)
        Me.DGridNSSSummary.Name = "DGridNSSSummary"
        Me.DGridNSSSummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGridNSSSummary.Size = New System.Drawing.Size(1028, 366)
        Me.DGridNSSSummary.TabIndex = 21
        '
        'colNSSSummaryBatchCode
        '
        Me.colNSSSummaryBatchCode.HeaderText = "BatchCode"
        Me.colNSSSummaryBatchCode.Name = "colNSSSummaryBatchCode"
        Me.colNSSSummaryBatchCode.ReadOnly = True
        '
        'colNSSSummaryYear
        '
        Me.colNSSSummaryYear.HeaderText = "Year"
        Me.colNSSSummaryYear.Name = "colNSSSummaryYear"
        Me.colNSSSummaryYear.Visible = False
        '
        'colNSSSummaryQuarterly
        '
        Me.colNSSSummaryQuarterly.HeaderText = "Quarterly"
        Me.colNSSSummaryQuarterly.Name = "colNSSSummaryQuarterly"
        Me.colNSSSummaryQuarterly.Visible = False
        '
        'colNSSSummaryAllocationDate
        '
        Me.colNSSSummaryAllocationDate.HeaderText = "AllocationDate"
        Me.colNSSSummaryAllocationDate.Name = "colNSSSummaryAllocationDate"
        Me.colNSSSummaryAllocationDate.ReadOnly = True
        '
        'colNSSSummaryPeriod
        '
        Me.colNSSSummaryPeriod.HeaderText = "Period"
        Me.colNSSSummaryPeriod.Name = "colNSSSummaryPeriod"
        Me.colNSSSummaryPeriod.ReadOnly = True
        '
        'colNSSSummaryIDNumber
        '
        Me.colNSSSummaryIDNumber.HeaderText = "IDNumber"
        Me.colNSSSummaryIDNumber.Name = "colNSSSummaryIDNumber"
        Me.colNSSSummaryIDNumber.ReadOnly = True
        '
        'colNSSSummaryParticipantID
        '
        Me.colNSSSummaryParticipantID.HeaderText = "ParticipantID"
        Me.colNSSSummaryParticipantID.Name = "colNSSSummaryParticipantID"
        Me.colNSSSummaryParticipantID.ReadOnly = True
        '
        'colNSSSummaryTotalNSSInterest
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colNSSSummaryTotalNSSInterest.DefaultCellStyle = DataGridViewCellStyle2
        Me.colNSSSummaryTotalNSSInterest.HeaderText = "Total NSS Interest"
        Me.colNSSSummaryTotalNSSInterest.Name = "colNSSSummaryTotalNSSInterest"
        Me.colNSSSummaryTotalNSSInterest.ReadOnly = True
        '
        'colNSSSummaryTotalNSSInterestNetWTax
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colNSSSummaryTotalNSSInterestNetWTax.DefaultCellStyle = DataGridViewCellStyle3
        Me.colNSSSummaryTotalNSSInterestNetWTax.HeaderText = "TOTAL NSS Interest Net of W/holding Tax"
        Me.colNSSSummaryTotalNSSInterestNetWTax.Name = "colNSSSummaryTotalNSSInterestNetWTax"
        Me.colNSSSummaryTotalNSSInterestNetWTax.ReadOnly = True
        '
        'colNSSSummaryTotalSTLInterest
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colNSSSummaryTotalSTLInterest.DefaultCellStyle = DataGridViewCellStyle4
        Me.colNSSSummaryTotalSTLInterest.HeaderText = "TOTAL STL Interest"
        Me.colNSSSummaryTotalSTLInterest.Name = "colNSSSummaryTotalSTLInterest"
        Me.colNSSSummaryTotalSTLInterest.ReadOnly = True
        '
        'colNSSSummarySTLInterestNetWTax
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colNSSSummarySTLInterestNetWTax.DefaultCellStyle = DataGridViewCellStyle5
        Me.colNSSSummarySTLInterestNetWTax.HeaderText = "TOTAL STL Interest Net of W/holding Tax"
        Me.colNSSSummarySTLInterestNetWTax.Name = "colNSSSummarySTLInterestNetWTax"
        Me.colNSSSummarySTLInterestNetWTax.ReadOnly = True
        '
        'colNSSSummaryTotal
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colNSSSummaryTotal.DefaultCellStyle = DataGridViewCellStyle6
        Me.colNSSSummaryTotal.HeaderText = "TOTAL"
        Me.colNSSSummaryTotal.Name = "colNSSSummaryTotal"
        Me.colNSSSummaryTotal.ReadOnly = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(905, 508)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(155, 42)
        Me.btnClose.TabIndex = 23
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmNSSMonitoring
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1073, 562)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmNSSMonitoring"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "NSS Monitoring"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.DGridViewNSSRA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ToolStrip3.ResumeLayout(False)
        Me.ToolStrip3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ToolStrip5.ResumeLayout(False)
        Me.ToolStrip5.PerformLayout()
        CType(Me.DGridNSSSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCompute As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents DGridViewNSSRA As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripTextBox1 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker3 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker4 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip3 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents DataGridView3 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DGridNSSSummary As System.Windows.Forms.DataGridView
    Friend WithEvents btnNSSSummarySave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnNSSSummaryViewSummary As System.Windows.Forms.Button
    Friend WithEvents colNSSSummaryBatchCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNSSSummaryYear As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNSSSummaryQuarterly As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNSSSummaryAllocationDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNSSSummaryPeriod As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNSSSummaryIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNSSSummaryParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNSSSummaryTotalNSSInterest As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNSSSummaryTotalNSSInterestNetWTax As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNSSSummaryTotalSTLInterest As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNSSSummarySTLInterestNetWTax As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNSSSummaryTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNSSRAAdvanceSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnNSSRASearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents txtNSSRASearch As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents ToolStrip5 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNSSSummaryAdvanceSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnNSSSummaryUpload As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnNSSSummarySearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtNSSSummarySearch As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripLabel4 As System.Windows.Forms.ToolStripLabel
End Class
