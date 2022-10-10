<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSummaryOfDefaultInterest
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
        Me.gBox_Filters = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtp_To = New System.Windows.Forms.DateTimePicker()
        Me.dtp_From = New System.Windows.Forms.DateTimePicker()
        Me.dgv_ViewRecords = New System.Windows.Forms.DataGridView()
        Me.cmd_GenerateReport = New System.Windows.Forms.Button()
        Me.cmd_Close = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rb_MarketFees = New System.Windows.Forms.RadioButton()
        Me.rb_Energy = New System.Windows.Forms.RadioButton()
        Me.cmd_ExportFile = New System.Windows.Forms.Button()
        Me.gBox_Filters.SuspendLayout()
        CType(Me.dgv_ViewRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'gBox_Filters
        '
        Me.gBox_Filters.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gBox_Filters.Controls.Add(Me.Label2)
        Me.gBox_Filters.Controls.Add(Me.Label1)
        Me.gBox_Filters.Controls.Add(Me.dtp_To)
        Me.gBox_Filters.Controls.Add(Me.dtp_From)
        Me.gBox_Filters.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gBox_Filters.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.gBox_Filters.Location = New System.Drawing.Point(12, 12)
        Me.gBox_Filters.Name = "gBox_Filters"
        Me.gBox_Filters.Size = New System.Drawing.Size(333, 48)
        Me.gBox_Filters.TabIndex = 0
        Me.gBox_Filters.TabStop = False
        Me.gBox_Filters.Text = "Select Date Range:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(173, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(21, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "To"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(6, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "From"
        '
        'dtp_To
        '
        Me.dtp_To.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_To.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_To.Location = New System.Drawing.Point(200, 19)
        Me.dtp_To.Name = "dtp_To"
        Me.dtp_To.Size = New System.Drawing.Size(118, 20)
        Me.dtp_To.TabIndex = 1
        '
        'dtp_From
        '
        Me.dtp_From.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_From.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_From.Location = New System.Drawing.Point(49, 19)
        Me.dtp_From.Name = "dtp_From"
        Me.dtp_From.Size = New System.Drawing.Size(118, 20)
        Me.dtp_From.TabIndex = 0
        '
        'dgv_ViewRecords
        '
        Me.dgv_ViewRecords.AllowUserToAddRows = False
        Me.dgv_ViewRecords.AllowUserToDeleteRows = False
        Me.dgv_ViewRecords.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_ViewRecords.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_ViewRecords.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_ViewRecords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_ViewRecords.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_ViewRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_ViewRecords.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_ViewRecords.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv_ViewRecords.Location = New System.Drawing.Point(12, 109)
        Me.dgv_ViewRecords.Name = "dgv_ViewRecords"
        Me.dgv_ViewRecords.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgv_ViewRecords.Size = New System.Drawing.Size(550, 573)
        Me.dgv_ViewRecords.TabIndex = 1
        '
        'cmd_GenerateReport
        '
        Me.cmd_GenerateReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_GenerateReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_GenerateReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_GenerateReport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_GenerateReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_GenerateReport.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_GenerateReport.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.cmd_GenerateReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_GenerateReport.Location = New System.Drawing.Point(58, 66)
        Me.cmd_GenerateReport.Name = "cmd_GenerateReport"
        Me.cmd_GenerateReport.Size = New System.Drawing.Size(162, 37)
        Me.cmd_GenerateReport.TabIndex = 2
        Me.cmd_GenerateReport.Text = "Generate Report"
        Me.cmd_GenerateReport.UseVisualStyleBackColor = True
        '
        'cmd_Close
        '
        Me.cmd_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_Close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Close.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Close.Location = New System.Drawing.Point(394, 66)
        Me.cmd_Close.Name = "cmd_Close"
        Me.cmd_Close.Size = New System.Drawing.Size(162, 37)
        Me.cmd_Close.TabIndex = 3
        Me.cmd_Close.Text = "Close"
        Me.cmd_Close.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.rb_MarketFees)
        Me.GroupBox1.Controls.Add(Me.rb_Energy)
        Me.GroupBox1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(351, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(211, 48)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Charge Type:"
        '
        'rb_MarketFees
        '
        Me.rb_MarketFees.AutoSize = True
        Me.rb_MarketFees.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_MarketFees.ForeColor = System.Drawing.Color.Black
        Me.rb_MarketFees.Location = New System.Drawing.Point(116, 21)
        Me.rb_MarketFees.Name = "rb_MarketFees"
        Me.rb_MarketFees.Size = New System.Drawing.Size(85, 16)
        Me.rb_MarketFees.TabIndex = 1
        Me.rb_MarketFees.Text = "Market Fees"
        Me.rb_MarketFees.UseVisualStyleBackColor = True
        '
        'rb_Energy
        '
        Me.rb_Energy.AutoSize = True
        Me.rb_Energy.Checked = True
        Me.rb_Energy.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_Energy.ForeColor = System.Drawing.Color.Black
        Me.rb_Energy.Location = New System.Drawing.Point(22, 21)
        Me.rb_Energy.Name = "rb_Energy"
        Me.rb_Energy.Size = New System.Drawing.Size(58, 16)
        Me.rb_Energy.TabIndex = 0
        Me.rb_Energy.TabStop = True
        Me.rb_Energy.Text = "Energy"
        Me.rb_Energy.UseVisualStyleBackColor = True
        '
        'cmd_ExportFile
        '
        Me.cmd_ExportFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_ExportFile.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_ExportFile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_ExportFile.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_ExportFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_ExportFile.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ExportFile.Image = Global.AccountsManagementForms.My.Resources.Resources.CSVIconColored22x22
        Me.cmd_ExportFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_ExportFile.Location = New System.Drawing.Point(226, 66)
        Me.cmd_ExportFile.Name = "cmd_ExportFile"
        Me.cmd_ExportFile.Size = New System.Drawing.Size(162, 37)
        Me.cmd_ExportFile.TabIndex = 5
        Me.cmd_ExportFile.Text = "Export to File"
        Me.cmd_ExportFile.UseVisualStyleBackColor = True
        '
        'frmSummaryOfDefaultInterest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(577, 699)
        Me.Controls.Add(Me.cmd_ExportFile)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmd_Close)
        Me.Controls.Add(Me.cmd_GenerateReport)
        Me.Controls.Add(Me.dgv_ViewRecords)
        Me.Controls.Add(Me.gBox_Filters)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MinimumSize = New System.Drawing.Size(593, 737)
        Me.Name = "frmSummaryOfDefaultInterest"
        Me.Text = "Default Interest Summary"
        Me.gBox_Filters.ResumeLayout(False)
        Me.gBox_Filters.PerformLayout()
        CType(Me.dgv_ViewRecords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gBox_Filters As System.Windows.Forms.GroupBox
    Friend WithEvents dtp_To As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_From As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgv_ViewRecords As System.Windows.Forms.DataGridView
    Friend WithEvents cmd_GenerateReport As System.Windows.Forms.Button
    Friend WithEvents cmd_Close As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_MarketFees As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Energy As System.Windows.Forms.RadioButton
    Friend WithEvents cmd_ExportFile As System.Windows.Forms.Button
End Class
