<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWESMBillSummary
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWESMBillSummary))
        Me.Grid_Summary = New System.Windows.Forms.DataGridView
        Me.GroupNo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hSummaryType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hINVDMCMNo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ParentFlag = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.IDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.BillPeriod = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ChargeType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DueDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.BeginningBalance = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EndingBalance = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SummaryNo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hAdjustment = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cb_ShowDetails = New System.Windows.Forms.CheckBox
        Me.cmd_ParticipantReport = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.cmd_Refresh = New System.Windows.Forms.Button
        Me.cmd_generateReport = New System.Windows.Forms.Button
        Me.cmd_ViewDetails = New System.Windows.Forms.Button
        Me.cmd_Close = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cbox_E = New System.Windows.Forms.CheckBox
        Me.cbox_MFV = New System.Windows.Forms.CheckBox
        Me.cbox_All = New System.Windows.Forms.CheckBox
        Me.cbox_MF = New System.Windows.Forms.CheckBox
        Me.cbox_EV = New System.Windows.Forms.CheckBox
        Me.cmd_Search = New System.Windows.Forms.Button
        Me.cmd_clear = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbo_IDNumber = New System.Windows.Forms.ComboBox
        Me.cbo_DueDate = New System.Windows.Forms.ComboBox
        Me.cbo_BillPeriod = New System.Windows.Forms.ComboBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.txtNSSVat = New System.Windows.Forms.TextBox
        Me.txtAPVat = New System.Windows.Forms.TextBox
        Me.txtARVat = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.txtNSSEnergyMF = New System.Windows.Forms.TextBox
        Me.txtAPEnergyMF = New System.Windows.Forms.TextBox
        Me.txtAREnergyMF = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.rb_Invoice = New System.Windows.Forms.RadioButton
        Me.rb_DMCM = New System.Windows.Forms.RadioButton
        Me.txt_SearchNo = New System.Windows.Forms.TextBox
        Me.btnSearchDMCM = New System.Windows.Forms.Button
        CType(Me.Grid_Summary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.SuspendLayout()
        '
        'Grid_Summary
        '
        Me.Grid_Summary.AllowUserToAddRows = False
        Me.Grid_Summary.AllowUserToDeleteRows = False
        Me.Grid_Summary.AllowUserToOrderColumns = True
        Me.Grid_Summary.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Cyan
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        Me.Grid_Summary.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.Grid_Summary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Grid_Summary.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.Grid_Summary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Grid_Summary.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GroupNo, Me.hSummaryType, Me.hINVDMCMNo, Me.ParentFlag, Me.IDNumber, Me.BillPeriod, Me.ChargeType, Me.DueDate, Me.BeginningBalance, Me.EndingBalance, Me.SummaryNo, Me.hAdjustment})
        Me.Grid_Summary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid_Summary.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.Grid_Summary.Location = New System.Drawing.Point(3, 17)
        Me.Grid_Summary.MultiSelect = False
        Me.Grid_Summary.Name = "Grid_Summary"
        Me.Grid_Summary.ReadOnly = True
        Me.Grid_Summary.RowHeadersVisible = False
        Me.Grid_Summary.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White
        Me.Grid_Summary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Grid_Summary.Size = New System.Drawing.Size(649, 291)
        Me.Grid_Summary.TabIndex = 0
        '
        'GroupNo
        '
        Me.GroupNo.HeaderText = "GroupNumber"
        Me.GroupNo.Name = "GroupNo"
        Me.GroupNo.ReadOnly = True
        Me.GroupNo.Visible = False
        Me.GroupNo.Width = 80
        '
        'hSummaryType
        '
        Me.hSummaryType.HeaderText = "ReferenceType"
        Me.hSummaryType.Name = "hSummaryType"
        Me.hSummaryType.ReadOnly = True
        Me.hSummaryType.Width = 104
        '
        'hINVDMCMNo
        '
        Me.hINVDMCMNo.HeaderText = "ReferenceNo"
        Me.hINVDMCMNo.Name = "hINVDMCMNo"
        Me.hINVDMCMNo.ReadOnly = True
        Me.hINVDMCMNo.Width = 94
        '
        'ParentFlag
        '
        Me.ParentFlag.HeaderText = "ParentFlag"
        Me.ParentFlag.Name = "ParentFlag"
        Me.ParentFlag.ReadOnly = True
        Me.ParentFlag.Width = 84
        '
        'IDNumber
        '
        Me.IDNumber.HeaderText = "ParticipantID"
        Me.IDNumber.Name = "IDNumber"
        Me.IDNumber.ReadOnly = True
        Me.IDNumber.Width = 93
        '
        'BillPeriod
        '
        Me.BillPeriod.HeaderText = "BillingPeriod"
        Me.BillPeriod.Name = "BillPeriod"
        Me.BillPeriod.ReadOnly = True
        Me.BillPeriod.Width = 93
        '
        'ChargeType
        '
        Me.ChargeType.HeaderText = "ChargeType"
        Me.ChargeType.Name = "ChargeType"
        Me.ChargeType.ReadOnly = True
        Me.ChargeType.Width = 89
        '
        'DueDate
        '
        DataGridViewCellStyle3.Format = "d"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.DueDate.DefaultCellStyle = DataGridViewCellStyle3
        Me.DueDate.HeaderText = "DueDate"
        Me.DueDate.Name = "DueDate"
        Me.DueDate.ReadOnly = True
        Me.DueDate.Width = 73
        '
        'BeginningBalance
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.NullValue = Nothing
        Me.BeginningBalance.DefaultCellStyle = DataGridViewCellStyle4
        Me.BeginningBalance.HeaderText = "BeginningBalance"
        Me.BeginningBalance.Name = "BeginningBalance"
        Me.BeginningBalance.ReadOnly = True
        Me.BeginningBalance.Width = 118
        '
        'EndingBalance
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.EndingBalance.DefaultCellStyle = DataGridViewCellStyle5
        Me.EndingBalance.HeaderText = "EndingBalance"
        Me.EndingBalance.Name = "EndingBalance"
        Me.EndingBalance.ReadOnly = True
        Me.EndingBalance.Width = 103
        '
        'SummaryNo
        '
        Me.SummaryNo.HeaderText = "SummaryNo"
        Me.SummaryNo.Name = "SummaryNo"
        Me.SummaryNo.ReadOnly = True
        Me.SummaryNo.Visible = False
        Me.SummaryNo.Width = 90
        '
        'hAdjustment
        '
        Me.hAdjustment.HeaderText = "Adjustment"
        Me.hAdjustment.Name = "hAdjustment"
        Me.hAdjustment.ReadOnly = True
        Me.hAdjustment.Width = 85
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Grid_Summary)
        Me.GroupBox1.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(274, 31)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(655, 311)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "WESM BILL Summaries"
        '
        'cb_ShowDetails
        '
        Me.cb_ShowDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cb_ShowDetails.AutoSize = True
        Me.cb_ShowDetails.Font = New System.Drawing.Font("Helvetica Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_ShowDetails.ForeColor = System.Drawing.Color.Blue
        Me.cb_ShowDetails.Location = New System.Drawing.Point(108, 343)
        Me.cb_ShowDetails.Name = "cb_ShowDetails"
        Me.cb_ShowDetails.Size = New System.Drawing.Size(139, 19)
        Me.cb_ShowDetails.TabIndex = 19
        Me.cb_ShowDetails.Text = "Show Details on Report"
        Me.cb_ShowDetails.UseVisualStyleBackColor = True
        '
        'cmd_ParticipantReport
        '
        Me.cmd_ParticipantReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ParticipantReport.ForeColor = System.Drawing.Color.Blue
        Me.cmd_ParticipantReport.Location = New System.Drawing.Point(441, 825)
        Me.cmd_ParticipantReport.Name = "cmd_ParticipantReport"
        Me.cmd_ParticipantReport.Size = New System.Drawing.Size(250, 39)
        Me.cmd_ParticipantReport.TabIndex = 13
        Me.cmd_ParticipantReport.Text = "Generate WESM Bill Summary Report"
        Me.cmd_ParticipantReport.UseVisualStyleBackColor = True
        Me.cmd_ParticipantReport.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.cmd_Refresh)
        Me.GroupBox3.Controls.Add(Me.cmd_generateReport)
        Me.GroupBox3.Controls.Add(Me.cmd_ViewDetails)
        Me.GroupBox3.Controls.Add(Me.cb_ShowDetails)
        Me.GroupBox3.Controls.Add(Me.cmd_Close)
        Me.GroupBox3.Controls.Add(Me.GroupBox2)
        Me.GroupBox3.Controls.Add(Me.cmd_Search)
        Me.GroupBox3.Controls.Add(Me.cmd_clear)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.cbo_IDNumber)
        Me.GroupBox3.Controls.Add(Me.cbo_DueDate)
        Me.GroupBox3.Controls.Add(Me.cbo_BillPeriod)
        Me.GroupBox3.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(12, 5)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(256, 475)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = False
        '
        'cmd_Refresh
        '
        Me.cmd_Refresh.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Refresh.ForeColor = System.Drawing.Color.Black
        Me.cmd_Refresh.Image = Global.AccountsManagementForms.My.Resources.Resources.refresh
        Me.cmd_Refresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Refresh.Location = New System.Drawing.Point(13, 267)
        Me.cmd_Refresh.Name = "cmd_Refresh"
        Me.cmd_Refresh.Size = New System.Drawing.Size(234, 32)
        Me.cmd_Refresh.TabIndex = 20
        Me.cmd_Refresh.Text = "Refresh"
        Me.cmd_Refresh.UseVisualStyleBackColor = True
        '
        'cmd_generateReport
        '
        Me.cmd_generateReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmd_generateReport.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_generateReport.ForeColor = System.Drawing.Color.Black
        Me.cmd_generateReport.Image = CType(resources.GetObject("cmd_generateReport.Image"), System.Drawing.Image)
        Me.cmd_generateReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_generateReport.Location = New System.Drawing.Point(13, 400)
        Me.cmd_generateReport.Name = "cmd_generateReport"
        Me.cmd_generateReport.Size = New System.Drawing.Size(234, 26)
        Me.cmd_generateReport.TabIndex = 18
        Me.cmd_generateReport.Text = "    Generate Summary Report"
        Me.cmd_generateReport.UseVisualStyleBackColor = True
        '
        'cmd_ViewDetails
        '
        Me.cmd_ViewDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmd_ViewDetails.Enabled = False
        Me.cmd_ViewDetails.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ViewDetails.ForeColor = System.Drawing.Color.Black
        Me.cmd_ViewDetails.Image = CType(resources.GetObject("cmd_ViewDetails.Image"), System.Drawing.Image)
        Me.cmd_ViewDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_ViewDetails.Location = New System.Drawing.Point(13, 368)
        Me.cmd_ViewDetails.Name = "cmd_ViewDetails"
        Me.cmd_ViewDetails.Size = New System.Drawing.Size(234, 26)
        Me.cmd_ViewDetails.TabIndex = 19
        Me.cmd_ViewDetails.Text = "View Participant Details"
        Me.cmd_ViewDetails.UseVisualStyleBackColor = True
        '
        'cmd_Close
        '
        Me.cmd_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmd_Close.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Close.Image = CType(resources.GetObject("cmd_Close.Image"), System.Drawing.Image)
        Me.cmd_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Close.Location = New System.Drawing.Point(13, 432)
        Me.cmd_Close.Name = "cmd_Close"
        Me.cmd_Close.Size = New System.Drawing.Size(234, 26)
        Me.cmd_Close.TabIndex = 11
        Me.cmd_Close.Text = "Close"
        Me.cmd_Close.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cbox_E)
        Me.GroupBox2.Controls.Add(Me.cbox_MFV)
        Me.GroupBox2.Controls.Add(Me.cbox_All)
        Me.GroupBox2.Controls.Add(Me.cbox_MF)
        Me.GroupBox2.Controls.Add(Me.cbox_EV)
        Me.GroupBox2.Location = New System.Drawing.Point(13, 96)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(234, 127)
        Me.GroupBox2.TabIndex = 19
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Select Charge Type"
        '
        'cbox_E
        '
        Me.cbox_E.AutoSize = True
        Me.cbox_E.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbox_E.Location = New System.Drawing.Point(27, 27)
        Me.cbox_E.Name = "cbox_E"
        Me.cbox_E.Size = New System.Drawing.Size(65, 18)
        Me.cbox_E.TabIndex = 4
        Me.cbox_E.Text = "Energy"
        Me.cbox_E.UseVisualStyleBackColor = True
        '
        'cbox_MFV
        '
        Me.cbox_MFV.AutoSize = True
        Me.cbox_MFV.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbox_MFV.Location = New System.Drawing.Point(27, 99)
        Me.cbox_MFV.Name = "cbox_MFV"
        Me.cbox_MFV.Size = New System.Drawing.Size(136, 18)
        Me.cbox_MFV.TabIndex = 7
        Me.cbox_MFV.Text = "VAT on Market Fees"
        Me.cbox_MFV.UseVisualStyleBackColor = True
        '
        'cbox_All
        '
        Me.cbox_All.AutoSize = True
        Me.cbox_All.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbox_All.Location = New System.Drawing.Point(188, 0)
        Me.cbox_All.Name = "cbox_All"
        Me.cbox_All.Size = New System.Drawing.Size(40, 18)
        Me.cbox_All.TabIndex = 8
        Me.cbox_All.Text = "All"
        Me.cbox_All.UseVisualStyleBackColor = True
        '
        'cbox_MF
        '
        Me.cbox_MF.AutoSize = True
        Me.cbox_MF.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbox_MF.Location = New System.Drawing.Point(27, 75)
        Me.cbox_MF.Name = "cbox_MF"
        Me.cbox_MF.Size = New System.Drawing.Size(93, 18)
        Me.cbox_MF.TabIndex = 6
        Me.cbox_MF.Text = "Market Fees"
        Me.cbox_MF.UseVisualStyleBackColor = True
        '
        'cbox_EV
        '
        Me.cbox_EV.AutoSize = True
        Me.cbox_EV.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbox_EV.Location = New System.Drawing.Point(27, 51)
        Me.cbox_EV.Name = "cbox_EV"
        Me.cbox_EV.Size = New System.Drawing.Size(108, 18)
        Me.cbox_EV.TabIndex = 5
        Me.cbox_EV.Text = "VAT on Energy"
        Me.cbox_EV.UseVisualStyleBackColor = True
        '
        'cmd_Search
        '
        Me.cmd_Search.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Search.ForeColor = System.Drawing.Color.Black
        Me.cmd_Search.Image = CType(resources.GetObject("cmd_Search.Image"), System.Drawing.Image)
        Me.cmd_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Search.Location = New System.Drawing.Point(13, 229)
        Me.cmd_Search.Name = "cmd_Search"
        Me.cmd_Search.Size = New System.Drawing.Size(234, 32)
        Me.cmd_Search.TabIndex = 9
        Me.cmd_Search.Text = "Search"
        Me.cmd_Search.UseVisualStyleBackColor = True
        '
        'cmd_clear
        '
        Me.cmd_clear.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_clear.ForeColor = System.Drawing.Color.Black
        Me.cmd_clear.Image = CType(resources.GetObject("cmd_clear.Image"), System.Drawing.Image)
        Me.cmd_clear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_clear.Location = New System.Drawing.Point(13, 305)
        Me.cmd_clear.Name = "cmd_clear"
        Me.cmd_clear.Size = New System.Drawing.Size(234, 32)
        Me.cmd_clear.TabIndex = 10
        Me.cmd_clear.Text = "Clear"
        Me.cmd_clear.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 15)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Due Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 15)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Participant ID"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 15)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Billing Period"
        '
        'cbo_IDNumber
        '
        Me.cbo_IDNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbo_IDNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbo_IDNumber.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_IDNumber.FormattingEnabled = True
        Me.cbo_IDNumber.Location = New System.Drawing.Point(92, 12)
        Me.cbo_IDNumber.Name = "cbo_IDNumber"
        Me.cbo_IDNumber.Size = New System.Drawing.Size(155, 22)
        Me.cbo_IDNumber.TabIndex = 3
        '
        'cbo_DueDate
        '
        Me.cbo_DueDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_DueDate.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_DueDate.FormattingEnabled = True
        Me.cbo_DueDate.Location = New System.Drawing.Point(92, 68)
        Me.cbo_DueDate.Name = "cbo_DueDate"
        Me.cbo_DueDate.Size = New System.Drawing.Size(155, 22)
        Me.cbo_DueDate.TabIndex = 2
        '
        'cbo_BillPeriod
        '
        Me.cbo_BillPeriod.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbo_BillPeriod.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbo_BillPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_BillPeriod.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_BillPeriod.FormattingEnabled = True
        Me.cbo_BillPeriod.Location = New System.Drawing.Point(92, 40)
        Me.cbo_BillPeriod.Name = "cbo_BillPeriod"
        Me.cbo_BillPeriod.Size = New System.Drawing.Size(155, 22)
        Me.cbo_BillPeriod.TabIndex = 1
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.GroupBox5)
        Me.GroupBox4.Controls.Add(Me.GroupBox6)
        Me.GroupBox4.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(277, 348)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(652, 132)
        Me.GroupBox4.TabIndex = 20
        Me.GroupBox4.TabStop = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.txtNSSVat)
        Me.GroupBox5.Controls.Add(Me.txtAPVat)
        Me.GroupBox5.Controls.Add(Me.txtARVat)
        Me.GroupBox5.Controls.Add(Me.Label10)
        Me.GroupBox5.Controls.Add(Me.Label11)
        Me.GroupBox5.Controls.Add(Me.Label12)
        Me.GroupBox5.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(397, 18)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(237, 97)
        Me.GroupBox5.TabIndex = 25
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Total VAT on Energy/MF"
        '
        'txtNSSVat
        '
        Me.txtNSSVat.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNSSVat.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNSSVat.ForeColor = System.Drawing.Color.Black
        Me.txtNSSVat.Location = New System.Drawing.Point(82, 68)
        Me.txtNSSVat.Name = "txtNSSVat"
        Me.txtNSSVat.ReadOnly = True
        Me.txtNSSVat.Size = New System.Drawing.Size(147, 23)
        Me.txtNSSVat.TabIndex = 29
        Me.txtNSSVat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAPVat
        '
        Me.txtAPVat.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAPVat.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAPVat.ForeColor = System.Drawing.Color.Black
        Me.txtAPVat.Location = New System.Drawing.Point(82, 19)
        Me.txtAPVat.Name = "txtAPVat"
        Me.txtAPVat.ReadOnly = True
        Me.txtAPVat.Size = New System.Drawing.Size(147, 23)
        Me.txtAPVat.TabIndex = 28
        Me.txtAPVat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtARVat
        '
        Me.txtARVat.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtARVat.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtARVat.ForeColor = System.Drawing.Color.Black
        Me.txtARVat.Location = New System.Drawing.Point(82, 44)
        Me.txtARVat.Name = "txtARVat"
        Me.txtARVat.ReadOnly = True
        Me.txtARVat.Size = New System.Drawing.Size(147, 23)
        Me.txtARVat.TabIndex = 27
        Me.txtARVat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(5, 73)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(71, 15)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "Total NSS:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(14, 24)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(62, 15)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Total AP:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(14, 47)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(62, 15)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Total AR:"
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox6.Controls.Add(Me.txtNSSEnergyMF)
        Me.GroupBox6.Controls.Add(Me.txtAPEnergyMF)
        Me.GroupBox6.Controls.Add(Me.txtAREnergyMF)
        Me.GroupBox6.Controls.Add(Me.Label7)
        Me.GroupBox6.Controls.Add(Me.Label6)
        Me.GroupBox6.Controls.Add(Me.Label5)
        Me.GroupBox6.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox6.Location = New System.Drawing.Point(52, 18)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(236, 97)
        Me.GroupBox6.TabIndex = 24
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Total Energy/MF"
        '
        'txtNSSEnergyMF
        '
        Me.txtNSSEnergyMF.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNSSEnergyMF.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNSSEnergyMF.ForeColor = System.Drawing.Color.Black
        Me.txtNSSEnergyMF.Location = New System.Drawing.Point(81, 68)
        Me.txtNSSEnergyMF.Name = "txtNSSEnergyMF"
        Me.txtNSSEnergyMF.ReadOnly = True
        Me.txtNSSEnergyMF.Size = New System.Drawing.Size(147, 23)
        Me.txtNSSEnergyMF.TabIndex = 26
        Me.txtNSSEnergyMF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAPEnergyMF
        '
        Me.txtAPEnergyMF.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAPEnergyMF.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAPEnergyMF.ForeColor = System.Drawing.Color.Black
        Me.txtAPEnergyMF.Location = New System.Drawing.Point(81, 17)
        Me.txtAPEnergyMF.Name = "txtAPEnergyMF"
        Me.txtAPEnergyMF.ReadOnly = True
        Me.txtAPEnergyMF.Size = New System.Drawing.Size(147, 23)
        Me.txtAPEnergyMF.TabIndex = 25
        Me.txtAPEnergyMF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAREnergyMF
        '
        Me.txtAREnergyMF.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAREnergyMF.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAREnergyMF.ForeColor = System.Drawing.Color.Black
        Me.txtAREnergyMF.Location = New System.Drawing.Point(80, 42)
        Me.txtAREnergyMF.Name = "txtAREnergyMF"
        Me.txtAREnergyMF.ReadOnly = True
        Me.txtAREnergyMF.Size = New System.Drawing.Size(148, 23)
        Me.txtAREnergyMF.TabIndex = 24
        Me.txtAREnergyMF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(11, 71)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 15)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Total NSS:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(20, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 15)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Total AP:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(20, 45)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 15)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Total AR:"
        '
        'rb_Invoice
        '
        Me.rb_Invoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rb_Invoice.AutoSize = True
        Me.rb_Invoice.Checked = True
        Me.rb_Invoice.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_Invoice.Location = New System.Drawing.Point(498, 9)
        Me.rb_Invoice.Name = "rb_Invoice"
        Me.rb_Invoice.Size = New System.Drawing.Size(58, 16)
        Me.rb_Invoice.TabIndex = 21
        Me.rb_Invoice.TabStop = True
        Me.rb_Invoice.Text = "Invoice"
        Me.rb_Invoice.UseVisualStyleBackColor = True
        '
        'rb_DMCM
        '
        Me.rb_DMCM.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rb_DMCM.AutoSize = True
        Me.rb_DMCM.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_DMCM.Location = New System.Drawing.Point(557, 9)
        Me.rb_DMCM.Name = "rb_DMCM"
        Me.rb_DMCM.Size = New System.Drawing.Size(113, 16)
        Me.rb_DMCM.TabIndex = 22
        Me.rb_DMCM.Text = "Debit/Credit Memo"
        Me.rb_DMCM.UseVisualStyleBackColor = True
        '
        'txt_SearchNo
        '
        Me.txt_SearchNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_SearchNo.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_SearchNo.ForeColor = System.Drawing.Color.Gray
        Me.txt_SearchNo.Location = New System.Drawing.Point(676, 7)
        Me.txt_SearchNo.Name = "txt_SearchNo"
        Me.txt_SearchNo.Size = New System.Drawing.Size(215, 20)
        Me.txt_SearchNo.TabIndex = 23
        Me.txt_SearchNo.Text = "Enter Invoice/DMCM No Here"
        '
        'btnSearchDMCM
        '
        Me.btnSearchDMCM.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearchDMCM.Image = Global.AccountsManagementForms.My.Resources.Resources.search
        Me.btnSearchDMCM.Location = New System.Drawing.Point(897, 5)
        Me.btnSearchDMCM.Name = "btnSearchDMCM"
        Me.btnSearchDMCM.Size = New System.Drawing.Size(32, 23)
        Me.btnSearchDMCM.TabIndex = 24
        Me.btnSearchDMCM.UseVisualStyleBackColor = True
        '
        'frmWESMBillSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(935, 490)
        Me.Controls.Add(Me.btnSearchDMCM)
        Me.Controls.Add(Me.txt_SearchNo)
        Me.Controls.Add(Me.rb_DMCM)
        Me.Controls.Add(Me.rb_Invoice)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.cmd_ParticipantReport)
        Me.Controls.Add(Me.GroupBox1)
        Me.MinimumSize = New System.Drawing.Size(951, 528)
        Me.Name = "frmWESMBillSummary"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WESM Bill Summary"
        CType(Me.Grid_Summary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Grid_Summary As System.Windows.Forms.DataGridView
    Friend WithEvents cmd_Close As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbo_IDNumber As System.Windows.Forms.ComboBox
    Friend WithEvents cbo_DueDate As System.Windows.Forms.ComboBox
    Friend WithEvents cbo_BillPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents cbox_All As System.Windows.Forms.CheckBox
    Friend WithEvents cbox_E As System.Windows.Forms.CheckBox
    Friend WithEvents cbox_MFV As System.Windows.Forms.CheckBox
    Friend WithEvents cbox_EV As System.Windows.Forms.CheckBox
    Friend WithEvents cbox_MF As System.Windows.Forms.CheckBox
    Friend WithEvents cmd_Search As System.Windows.Forms.Button
    Friend WithEvents cmd_clear As System.Windows.Forms.Button
    Friend WithEvents cmd_ParticipantReport As System.Windows.Forms.Button
    Friend WithEvents cmd_generateReport As System.Windows.Forms.Button
    Friend WithEvents cb_ShowDetails As System.Windows.Forms.CheckBox
    Friend WithEvents cmd_ViewDetails As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmd_Refresh As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNSSVat As System.Windows.Forms.TextBox
    Friend WithEvents txtAPVat As System.Windows.Forms.TextBox
    Friend WithEvents txtARVat As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNSSEnergyMF As System.Windows.Forms.TextBox
    Friend WithEvents txtAPEnergyMF As System.Windows.Forms.TextBox
    Friend WithEvents txtAREnergyMF As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hSummaryType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hINVDMCMNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParentFlag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BillPeriod As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChargeType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DueDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BeginningBalance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EndingBalance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SummaryNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hAdjustment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents rb_Invoice As System.Windows.Forms.RadioButton
    Friend WithEvents rb_DMCM As System.Windows.Forms.RadioButton
    Friend WithEvents txt_SearchNo As System.Windows.Forms.TextBox
    Friend WithEvents btnSearchDMCM As System.Windows.Forms.Button
End Class
