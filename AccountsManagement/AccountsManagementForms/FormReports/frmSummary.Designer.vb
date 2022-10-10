<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSummary
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSummary))
        Me.gb_Filter = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtp_To = New System.Windows.Forms.DateTimePicker()
        Me.dtp_From = New System.Windows.Forms.DateTimePicker()
        Me.gb_ReprotType = New System.Windows.Forms.GroupBox()
        Me.rb_CheckVoucher = New System.Windows.Forms.RadioButton()
        Me.rb_DMCM = New System.Windows.Forms.RadioButton()
        Me.rb_Invoice = New System.Windows.Forms.RadioButton()
        Me.cmd_GenReport = New System.Windows.Forms.Button()
        Me.dgv_dataView = New System.Windows.Forms.DataGridView()
        Me.cmd_Export = New System.Windows.Forms.Button()
        Me.btn_ExportToText = New System.Windows.Forms.Button()
        Me.btn_ExportToDat = New System.Windows.Forms.Button()
        Me.btn_ExportToCSV = New System.Windows.Forms.Button()
        Me.gb_Filter.SuspendLayout()
        Me.gb_ReprotType.SuspendLayout()
        CType(Me.dgv_dataView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gb_Filter
        '
        Me.gb_Filter.Controls.Add(Me.Label2)
        Me.gb_Filter.Controls.Add(Me.Label1)
        Me.gb_Filter.Controls.Add(Me.dtp_To)
        Me.gb_Filter.Controls.Add(Me.dtp_From)
        Me.gb_Filter.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb_Filter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.gb_Filter.Location = New System.Drawing.Point(12, 7)
        Me.gb_Filter.Name = "gb_Filter"
        Me.gb_Filter.Size = New System.Drawing.Size(376, 52)
        Me.gb_Filter.TabIndex = 0
        Me.gb_Filter.TabStop = False
        Me.gb_Filter.Text = "Select Date Range:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(199, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "To:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(6, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "From:"
        '
        'dtp_To
        '
        Me.dtp_To.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_To.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_To.Location = New System.Drawing.Point(229, 17)
        Me.dtp_To.Name = "dtp_To"
        Me.dtp_To.Size = New System.Drawing.Size(114, 20)
        Me.dtp_To.TabIndex = 1
        '
        'dtp_From
        '
        Me.dtp_From.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_From.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_From.Location = New System.Drawing.Point(49, 19)
        Me.dtp_From.Name = "dtp_From"
        Me.dtp_From.Size = New System.Drawing.Size(114, 20)
        Me.dtp_From.TabIndex = 0
        '
        'gb_ReprotType
        '
        Me.gb_ReprotType.Controls.Add(Me.rb_CheckVoucher)
        Me.gb_ReprotType.Controls.Add(Me.rb_DMCM)
        Me.gb_ReprotType.Controls.Add(Me.rb_Invoice)
        Me.gb_ReprotType.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb_ReprotType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.gb_ReprotType.Location = New System.Drawing.Point(79, 349)
        Me.gb_ReprotType.Name = "gb_ReprotType"
        Me.gb_ReprotType.Size = New System.Drawing.Size(174, 117)
        Me.gb_ReprotType.TabIndex = 1
        Me.gb_ReprotType.TabStop = False
        Me.gb_ReprotType.Text = "Generate Summary:"
        '
        'rb_CheckVoucher
        '
        Me.rb_CheckVoucher.AutoSize = True
        Me.rb_CheckVoucher.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_CheckVoucher.ForeColor = System.Drawing.Color.Black
        Me.rb_CheckVoucher.Location = New System.Drawing.Point(9, 55)
        Me.rb_CheckVoucher.Name = "rb_CheckVoucher"
        Me.rb_CheckVoucher.Size = New System.Drawing.Size(99, 16)
        Me.rb_CheckVoucher.TabIndex = 2
        Me.rb_CheckVoucher.Text = "Check Voucher"
        Me.rb_CheckVoucher.UseVisualStyleBackColor = True
        '
        'rb_DMCM
        '
        Me.rb_DMCM.AutoSize = True
        Me.rb_DMCM.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_DMCM.ForeColor = System.Drawing.Color.Black
        Me.rb_DMCM.Location = New System.Drawing.Point(9, 80)
        Me.rb_DMCM.Name = "rb_DMCM"
        Me.rb_DMCM.Size = New System.Drawing.Size(113, 16)
        Me.rb_DMCM.TabIndex = 1
        Me.rb_DMCM.Text = "Debit/Credit Memo"
        Me.rb_DMCM.UseVisualStyleBackColor = True
        '
        'rb_Invoice
        '
        Me.rb_Invoice.AutoSize = True
        Me.rb_Invoice.Checked = True
        Me.rb_Invoice.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_Invoice.ForeColor = System.Drawing.Color.Black
        Me.rb_Invoice.Location = New System.Drawing.Point(9, 30)
        Me.rb_Invoice.Name = "rb_Invoice"
        Me.rb_Invoice.Size = New System.Drawing.Size(100, 16)
        Me.rb_Invoice.TabIndex = 0
        Me.rb_Invoice.TabStop = True
        Me.rb_Invoice.Text = "WESM Invoices"
        Me.rb_Invoice.UseVisualStyleBackColor = True
        '
        'cmd_GenReport
        '
        Me.cmd_GenReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_GenReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_GenReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black
        Me.cmd_GenReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_GenReport.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_GenReport.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.cmd_GenReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_GenReport.Location = New System.Drawing.Point(311, 408)
        Me.cmd_GenReport.Name = "cmd_GenReport"
        Me.cmd_GenReport.Size = New System.Drawing.Size(175, 39)
        Me.cmd_GenReport.TabIndex = 2
        Me.cmd_GenReport.Text = "Generate"
        Me.cmd_GenReport.UseVisualStyleBackColor = True
        '
        'dgv_dataView
        '
        Me.dgv_dataView.AllowUserToAddRows = False
        Me.dgv_dataView.AllowUserToDeleteRows = False
        Me.dgv_dataView.AllowUserToResizeRows = False
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_dataView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        Me.dgv_dataView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_dataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgv_dataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_dataView.DefaultCellStyle = DataGridViewCellStyle8
        Me.dgv_dataView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv_dataView.Location = New System.Drawing.Point(95, 499)
        Me.dgv_dataView.Name = "dgv_dataView"
        Me.dgv_dataView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.dgv_dataView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_dataView.Size = New System.Drawing.Size(202, 8)
        Me.dgv_dataView.TabIndex = 4
        '
        'cmd_Export
        '
        Me.cmd_Export.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Export.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Export.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black
        Me.cmd_Export.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Export.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Export.Image = Global.AccountsManagementForms.My.Resources.Resources.ExcelIcon22x22
        Me.cmd_Export.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Export.Location = New System.Drawing.Point(10, 65)
        Me.cmd_Export.Name = "cmd_Export"
        Me.cmd_Export.Size = New System.Drawing.Size(186, 39)
        Me.cmd_Export.TabIndex = 5
        Me.cmd_Export.Text = "Export to Excel"
        Me.cmd_Export.UseVisualStyleBackColor = True
        '
        'btn_ExportToText
        '
        Me.btn_ExportToText.BackColor = System.Drawing.Color.White
        Me.btn_ExportToText.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_ExportToText.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_ExportToText.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_ExportToText.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ExportToText.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ExportToText.ForeColor = System.Drawing.Color.Black
        Me.btn_ExportToText.Image = CType(resources.GetObject("btn_ExportToText.Image"), System.Drawing.Image)
        Me.btn_ExportToText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_ExportToText.Location = New System.Drawing.Point(11, 110)
        Me.btn_ExportToText.Name = "btn_ExportToText"
        Me.btn_ExportToText.Size = New System.Drawing.Size(186, 39)
        Me.btn_ExportToText.TabIndex = 56
        Me.btn_ExportToText.Text = "Export to Text"
        Me.btn_ExportToText.UseVisualStyleBackColor = False
        '
        'btn_ExportToDat
        '
        Me.btn_ExportToDat.BackColor = System.Drawing.Color.White
        Me.btn_ExportToDat.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_ExportToDat.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_ExportToDat.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_ExportToDat.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ExportToDat.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ExportToDat.ForeColor = System.Drawing.Color.Black
        Me.btn_ExportToDat.Image = CType(resources.GetObject("btn_ExportToDat.Image"), System.Drawing.Image)
        Me.btn_ExportToDat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_ExportToDat.Location = New System.Drawing.Point(202, 110)
        Me.btn_ExportToDat.Name = "btn_ExportToDat"
        Me.btn_ExportToDat.Size = New System.Drawing.Size(186, 39)
        Me.btn_ExportToDat.TabIndex = 61
        Me.btn_ExportToDat.Text = "Export to DAT File"
        Me.btn_ExportToDat.UseVisualStyleBackColor = False
        '
        'btn_ExportToCSV
        '
        Me.btn_ExportToCSV.BackColor = System.Drawing.Color.White
        Me.btn_ExportToCSV.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_ExportToCSV.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_ExportToCSV.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_ExportToCSV.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ExportToCSV.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ExportToCSV.ForeColor = System.Drawing.Color.Black
        Me.btn_ExportToCSV.Image = Global.AccountsManagementForms.My.Resources.Resources.CSVIconColored22x22
        Me.btn_ExportToCSV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_ExportToCSV.Location = New System.Drawing.Point(202, 65)
        Me.btn_ExportToCSV.Name = "btn_ExportToCSV"
        Me.btn_ExportToCSV.Size = New System.Drawing.Size(186, 39)
        Me.btn_ExportToCSV.TabIndex = 60
        Me.btn_ExportToCSV.Text = "Export to CSV"
        Me.btn_ExportToCSV.UseVisualStyleBackColor = False
        '
        'frmSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(400, 161)
        Me.Controls.Add(Me.btn_ExportToDat)
        Me.Controls.Add(Me.btn_ExportToCSV)
        Me.Controls.Add(Me.btn_ExportToText)
        Me.Controls.Add(Me.cmd_Export)
        Me.Controls.Add(Me.dgv_dataView)
        Me.Controls.Add(Me.cmd_GenReport)
        Me.Controls.Add(Me.gb_ReprotType)
        Me.Controls.Add(Me.gb_Filter)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(100, 100)
        Me.Name = "frmSummary"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " Summary of Accounting Books"
        Me.gb_Filter.ResumeLayout(False)
        Me.gb_Filter.PerformLayout()
        Me.gb_ReprotType.ResumeLayout(False)
        Me.gb_ReprotType.PerformLayout()
        CType(Me.dgv_dataView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gb_Filter As System.Windows.Forms.GroupBox
    Friend WithEvents dtp_From As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtp_To As System.Windows.Forms.DateTimePicker
    Friend WithEvents gb_ReprotType As System.Windows.Forms.GroupBox
    Friend WithEvents rb_DMCM As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Invoice As System.Windows.Forms.RadioButton
    Friend WithEvents rb_CheckVoucher As System.Windows.Forms.RadioButton
    Friend WithEvents cmd_GenReport As System.Windows.Forms.Button
    Friend WithEvents dgv_dataView As System.Windows.Forms.DataGridView
    Friend WithEvents cmd_Export As System.Windows.Forms.Button
    Friend WithEvents btn_ExportToText As System.Windows.Forms.Button
    Friend WithEvents btn_ExportToDat As System.Windows.Forms.Button
    Friend WithEvents btn_ExportToCSV As System.Windows.Forms.Button
End Class
