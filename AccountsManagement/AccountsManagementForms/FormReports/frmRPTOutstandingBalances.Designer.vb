<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRPTOutstandingBalances
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dgv_OutstandingSummary = New System.Windows.Forms.DataGridView()
        Me.cmd_search = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_DestFile = New System.Windows.Forms.TextBox()
        Me.cmd_browse = New System.Windows.Forms.Button()
        Me.cmd_GenerateReport = New System.Windows.Forms.Button()
        Me.cmd_close = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rb_VATonEnergy = New System.Windows.Forms.RadioButton()
        Me.rb_MF = New System.Windows.Forms.RadioButton()
        Me.rb_Energy = New System.Windows.Forms.RadioButton()
        Me.cmd_GenerateReportToExcel = New System.Windows.Forms.Button()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgv_OutstandingSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.dgv_OutstandingSummary)
        Me.GroupBox2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(12, 98)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(727, 452)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Summary Of Outstanding Balances"
        '
        'dgv_OutstandingSummary
        '
        Me.dgv_OutstandingSummary.AllowUserToAddRows = False
        Me.dgv_OutstandingSummary.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgv_OutstandingSummary.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_OutstandingSummary.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_OutstandingSummary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_OutstandingSummary.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_OutstandingSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_OutstandingSummary.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv_OutstandingSummary.Location = New System.Drawing.Point(6, 21)
        Me.dgv_OutstandingSummary.Name = "dgv_OutstandingSummary"
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        Me.dgv_OutstandingSummary.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_OutstandingSummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_OutstandingSummary.Size = New System.Drawing.Size(715, 425)
        Me.dgv_OutstandingSummary.TabIndex = 11
        '
        'cmd_search
        '
        Me.cmd_search.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_search.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_search.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_search.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_search.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.cmd_search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_search.Location = New System.Drawing.Point(195, 16)
        Me.cmd_search.Name = "cmd_search"
        Me.cmd_search.Size = New System.Drawing.Size(101, 39)
        Me.cmd_search.TabIndex = 13
        Me.cmd_search.Text = "Search"
        Me.cmd_search.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(314, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 12)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Destination Folder:"
        '
        'txt_DestFile
        '
        Me.txt_DestFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_DestFile.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_DestFile.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_DestFile.Location = New System.Drawing.Point(415, 16)
        Me.txt_DestFile.Name = "txt_DestFile"
        Me.txt_DestFile.ReadOnly = True
        Me.txt_DestFile.Size = New System.Drawing.Size(259, 20)
        Me.txt_DestFile.TabIndex = 6
        '
        'cmd_browse
        '
        Me.cmd_browse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_browse.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_browse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_browse.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_browse.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_browse.Image = Global.AccountsManagementForms.My.Resources.Resources.SearchIconColored22x22
        Me.cmd_browse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_browse.Location = New System.Drawing.Point(680, 16)
        Me.cmd_browse.Name = "cmd_browse"
        Me.cmd_browse.Size = New System.Drawing.Size(35, 30)
        Me.cmd_browse.TabIndex = 9
        Me.cmd_browse.UseVisualStyleBackColor = True
        '
        'cmd_GenerateReport
        '
        Me.cmd_GenerateReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_GenerateReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_GenerateReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_GenerateReport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_GenerateReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_GenerateReport.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_GenerateReport.Image = Global.AccountsManagementForms.My.Resources.Resources.CSVIconColored22x22
        Me.cmd_GenerateReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_GenerateReport.Location = New System.Drawing.Point(437, 557)
        Me.cmd_GenerateReport.Name = "cmd_GenerateReport"
        Me.cmd_GenerateReport.Size = New System.Drawing.Size(150, 39)
        Me.cmd_GenerateReport.TabIndex = 7
        Me.cmd_GenerateReport.Text = "     Export Report to CSV"
        Me.cmd_GenerateReport.UseVisualStyleBackColor = True
        '
        'cmd_close
        '
        Me.cmd_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_close.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_close.Location = New System.Drawing.Point(593, 556)
        Me.cmd_close.Name = "cmd_close"
        Me.cmd_close.Size = New System.Drawing.Size(140, 39)
        Me.cmd_close.TabIndex = 8
        Me.cmd_close.Text = "Close"
        Me.cmd_close.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.rb_VATonEnergy)
        Me.GroupBox1.Controls.Add(Me.rb_MF)
        Me.GroupBox1.Controls.Add(Me.rb_Energy)
        Me.GroupBox1.Controls.Add(Me.cmd_search)
        Me.GroupBox1.Controls.Add(Me.cmd_browse)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txt_DestFile)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(727, 80)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        '
        'rb_VATonEnergy
        '
        Me.rb_VATonEnergy.AutoSize = True
        Me.rb_VATonEnergy.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_VATonEnergy.Location = New System.Drawing.Point(12, 44)
        Me.rb_VATonEnergy.Name = "rb_VATonEnergy"
        Me.rb_VATonEnergy.Size = New System.Drawing.Size(91, 16)
        Me.rb_VATonEnergy.TabIndex = 18
        Me.rb_VATonEnergy.Text = "VATonEnergy"
        Me.rb_VATonEnergy.UseVisualStyleBackColor = True
        '
        'rb_MF
        '
        Me.rb_MF.AutoSize = True
        Me.rb_MF.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_MF.Location = New System.Drawing.Point(105, 19)
        Me.rb_MF.Name = "rb_MF"
        Me.rb_MF.Size = New System.Drawing.Size(85, 16)
        Me.rb_MF.TabIndex = 16
        Me.rb_MF.Text = "Market Fees"
        Me.rb_MF.UseVisualStyleBackColor = True
        '
        'rb_Energy
        '
        Me.rb_Energy.AutoSize = True
        Me.rb_Energy.Checked = True
        Me.rb_Energy.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_Energy.Location = New System.Drawing.Point(12, 19)
        Me.rb_Energy.Name = "rb_Energy"
        Me.rb_Energy.Size = New System.Drawing.Size(58, 16)
        Me.rb_Energy.TabIndex = 15
        Me.rb_Energy.TabStop = True
        Me.rb_Energy.Text = "Energy"
        Me.rb_Energy.UseVisualStyleBackColor = True
        '
        'cmd_GenerateReportToExcel
        '
        Me.cmd_GenerateReportToExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_GenerateReportToExcel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_GenerateReportToExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_GenerateReportToExcel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_GenerateReportToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_GenerateReportToExcel.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_GenerateReportToExcel.Image = Global.AccountsManagementForms.My.Resources.Resources.ExcelIcon22x22
        Me.cmd_GenerateReportToExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_GenerateReportToExcel.Location = New System.Drawing.Point(281, 557)
        Me.cmd_GenerateReportToExcel.Name = "cmd_GenerateReportToExcel"
        Me.cmd_GenerateReportToExcel.Size = New System.Drawing.Size(150, 39)
        Me.cmd_GenerateReportToExcel.TabIndex = 17
        Me.cmd_GenerateReportToExcel.Text = " Generate"
        Me.cmd_GenerateReportToExcel.UseVisualStyleBackColor = True
        '
        'frmRPTOutstandingBalances
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(751, 607)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmd_GenerateReportToExcel)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.cmd_close)
        Me.Controls.Add(Me.cmd_GenerateReport)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MinimumSize = New System.Drawing.Size(767, 603)
        Me.Name = "frmRPTOutstandingBalances"
        Me.Text = " "
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgv_OutstandingSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmd_close As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_DestFile As System.Windows.Forms.TextBox
    Friend WithEvents dgv_OutstandingSummary As System.Windows.Forms.DataGridView
    Friend WithEvents cmd_browse As System.Windows.Forms.Button
    Friend WithEvents cmd_GenerateReport As System.Windows.Forms.Button
    Friend WithEvents cmd_search As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_MF As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Energy As System.Windows.Forms.RadioButton
    Friend WithEvents cmd_GenerateReportToExcel As System.Windows.Forms.Button
    Friend WithEvents rb_VATonEnergy As System.Windows.Forms.RadioButton
End Class
