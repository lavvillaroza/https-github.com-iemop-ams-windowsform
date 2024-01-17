<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewJournalVoucher
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgv_VoucherHeader = New System.Windows.Forms.DataGridView()
        Me.dtp_to = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtp_from = New System.Windows.Forms.DateTimePicker()
        Me.cbox_Date = New System.Windows.Forms.CheckBox()
        Me.cbox_No = New System.Windows.Forms.CheckBox()
        Me.cbo_JVNO = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dgv_JournalDetails = New System.Windows.Forms.DataGridView()
        Me.cmd_search = New System.Windows.Forms.Button()
        Me.cmd_refresh = New System.Windows.Forms.Button()
        Me.cmd_GenerateJV = New System.Windows.Forms.Button()
        Me.cmd_close = New System.Windows.Forms.Button()
        CType(Me.dgv_VoucherHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.dgv_JournalDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgv_VoucherHeader
        '
        Me.dgv_VoucherHeader.AllowUserToAddRows = False
        Me.dgv_VoucherHeader.AllowUserToDeleteRows = False
        Me.dgv_VoucherHeader.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_VoucherHeader.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_VoucherHeader.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_VoucherHeader.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_VoucherHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LemonChiffon
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_VoucherHeader.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_VoucherHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_VoucherHeader.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv_VoucherHeader.Location = New System.Drawing.Point(3, 16)
        Me.dgv_VoucherHeader.MultiSelect = False
        Me.dgv_VoucherHeader.Name = "dgv_VoucherHeader"
        Me.dgv_VoucherHeader.ReadOnly = True
        Me.dgv_VoucherHeader.RowHeadersVisible = False
        Me.dgv_VoucherHeader.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_VoucherHeader.Size = New System.Drawing.Size(827, 224)
        Me.dgv_VoucherHeader.TabIndex = 0
        '
        'dtp_to
        '
        Me.dtp_to.Enabled = False
        Me.dtp_to.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_to.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_to.Location = New System.Drawing.Point(234, 19)
        Me.dtp_to.Name = "dtp_to"
        Me.dtp_to.Size = New System.Drawing.Size(89, 20)
        Me.dtp_to.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(206, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(18, 14)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "To"
        '
        'dtp_from
        '
        Me.dtp_from.Enabled = False
        Me.dtp_from.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_from.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_from.Location = New System.Drawing.Point(111, 19)
        Me.dtp_from.Name = "dtp_from"
        Me.dtp_from.Size = New System.Drawing.Size(89, 20)
        Me.dtp_from.TabIndex = 7
        '
        'cbox_Date
        '
        Me.cbox_Date.AutoSize = True
        Me.cbox_Date.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbox_Date.Location = New System.Drawing.Point(18, 23)
        Me.cbox_Date.Name = "cbox_Date"
        Me.cbox_Date.Size = New System.Drawing.Size(75, 18)
        Me.cbox_Date.TabIndex = 6
        Me.cbox_Date.Text = "Date From"
        Me.cbox_Date.UseVisualStyleBackColor = True
        '
        'cbox_No
        '
        Me.cbox_No.AutoSize = True
        Me.cbox_No.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbox_No.Location = New System.Drawing.Point(350, 12)
        Me.cbox_No.Name = "cbox_No"
        Me.cbox_No.Size = New System.Drawing.Size(72, 20)
        Me.cbox_No.TabIndex = 5
        Me.cbox_No.Text = "Number"
        Me.cbox_No.UseVisualStyleBackColor = True
        Me.cbox_No.Visible = False
        '
        'cbo_JVNO
        '
        Me.cbo_JVNO.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbo_JVNO.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbo_JVNO.Enabled = False
        Me.cbo_JVNO.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_JVNO.FormattingEnabled = True
        Me.cbo_JVNO.Location = New System.Drawing.Point(427, 10)
        Me.cbo_JVNO.Name = "cbo_JVNO"
        Me.cbo_JVNO.Size = New System.Drawing.Size(121, 22)
        Me.cbo_JVNO.TabIndex = 1
        Me.cbo_JVNO.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.dgv_VoucherHeader)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(15, 64)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(833, 243)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(6, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(100, 14)
        Me.Label18.TabIndex = 22
        Me.Label18.Text = "Journal Voucher:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.dgv_JournalDetails)
        Me.GroupBox3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.Black
        Me.GroupBox3.Location = New System.Drawing.Point(15, 313)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(833, 322)
        Me.GroupBox3.TabIndex = 8
        Me.GroupBox3.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(6, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(143, 14)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Journal Voucher Details: "
        '
        'dgv_JournalDetails
        '
        Me.dgv_JournalDetails.AllowUserToAddRows = False
        Me.dgv_JournalDetails.AllowUserToDeleteRows = False
        Me.dgv_JournalDetails.AllowUserToResizeRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_JournalDetails.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv_JournalDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_JournalDetails.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgv_JournalDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LemonChiffon
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_JournalDetails.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgv_JournalDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_JournalDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv_JournalDetails.Location = New System.Drawing.Point(3, 16)
        Me.dgv_JournalDetails.MultiSelect = False
        Me.dgv_JournalDetails.Name = "dgv_JournalDetails"
        Me.dgv_JournalDetails.ReadOnly = True
        Me.dgv_JournalDetails.RowHeadersVisible = False
        Me.dgv_JournalDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_JournalDetails.Size = New System.Drawing.Size(827, 303)
        Me.dgv_JournalDetails.TabIndex = 0
        '
        'cmd_search
        '
        Me.cmd_search.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_search.BackColor = System.Drawing.Color.White
        Me.cmd_search.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_search.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_search.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_search.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_search.ForeColor = System.Drawing.Color.Black
        Me.cmd_search.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.cmd_search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_search.Location = New System.Drawing.Point(350, 19)
        Me.cmd_search.Name = "cmd_search"
        Me.cmd_search.Size = New System.Drawing.Size(110, 39)
        Me.cmd_search.TabIndex = 2
        Me.cmd_search.Text = "&Search"
        Me.cmd_search.UseVisualStyleBackColor = False
        '
        'cmd_refresh
        '
        Me.cmd_refresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_refresh.BackColor = System.Drawing.Color.White
        Me.cmd_refresh.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_refresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_refresh.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_refresh.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_refresh.ForeColor = System.Drawing.Color.Black
        Me.cmd_refresh.Image = Global.AccountsManagementForms.My.Resources.Resources.RefreshGreenIcon22x22
        Me.cmd_refresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_refresh.Location = New System.Drawing.Point(478, 19)
        Me.cmd_refresh.Name = "cmd_refresh"
        Me.cmd_refresh.Size = New System.Drawing.Size(110, 39)
        Me.cmd_refresh.TabIndex = 3
        Me.cmd_refresh.Text = "&Refresh"
        Me.cmd_refresh.UseVisualStyleBackColor = False
        '
        'cmd_GenerateJV
        '
        Me.cmd_GenerateJV.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_GenerateJV.BackColor = System.Drawing.Color.White
        Me.cmd_GenerateJV.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_GenerateJV.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_GenerateJV.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_GenerateJV.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_GenerateJV.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_GenerateJV.ForeColor = System.Drawing.Color.Black
        Me.cmd_GenerateJV.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.cmd_GenerateJV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_GenerateJV.Location = New System.Drawing.Point(594, 19)
        Me.cmd_GenerateJV.Name = "cmd_GenerateJV"
        Me.cmd_GenerateJV.Size = New System.Drawing.Size(120, 39)
        Me.cmd_GenerateJV.TabIndex = 4
        Me.cmd_GenerateJV.Text = "       &Generate Report"
        Me.cmd_GenerateJV.UseVisualStyleBackColor = False
        '
        'cmd_close
        '
        Me.cmd_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_close.BackColor = System.Drawing.Color.White
        Me.cmd_close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_close.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_close.ForeColor = System.Drawing.Color.Black
        Me.cmd_close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_close.Location = New System.Drawing.Point(720, 19)
        Me.cmd_close.Name = "cmd_close"
        Me.cmd_close.Size = New System.Drawing.Size(110, 39)
        Me.cmd_close.TabIndex = 1
        Me.cmd_close.Text = "&Close"
        Me.cmd_close.UseVisualStyleBackColor = False
        '
        'frmViewJournalVoucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(857, 659)
        Me.Controls.Add(Me.dtp_to)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.dtp_from)
        Me.Controls.Add(Me.cbox_Date)
        Me.Controls.Add(Me.cbox_No)
        Me.Controls.Add(Me.cmd_search)
        Me.Controls.Add(Me.cbo_JVNO)
        Me.Controls.Add(Me.cmd_refresh)
        Me.Controls.Add(Me.cmd_GenerateJV)
        Me.Controls.Add(Me.cmd_close)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmViewJournalVoucher"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "View Journal Voucher"
        CType(Me.dgv_VoucherHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.dgv_JournalDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgv_VoucherHeader As System.Windows.Forms.DataGridView
    Friend WithEvents cmd_close As System.Windows.Forms.Button
    Friend WithEvents cmd_search As System.Windows.Forms.Button
    Friend WithEvents cmd_refresh As System.Windows.Forms.Button
    Friend WithEvents cmd_GenerateJV As System.Windows.Forms.Button
    Friend WithEvents cbo_JVNO As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents dgv_JournalDetails As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtp_from As System.Windows.Forms.DateTimePicker
    Friend WithEvents cbox_Date As System.Windows.Forms.CheckBox
    Friend WithEvents cbox_No As System.Windows.Forms.CheckBox
    Friend WithEvents dtp_to As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
