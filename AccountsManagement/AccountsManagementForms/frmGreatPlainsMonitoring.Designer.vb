<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGreatPlainsMonitoring
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
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbo_PostType = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rb_Open = New System.Windows.Forms.RadioButton
        Me.rb_Posted = New System.Windows.Forms.RadioButton
        Me.rb_All = New System.Windows.Forms.RadioButton
        Me.btn_Remarks = New System.Windows.Forms.Button
        Me.btn_Post = New System.Windows.Forms.Button
        Me.cbo_BillPd = New System.Windows.Forms.ComboBox
        Me.lbl_BillPd = New System.Windows.Forms.Label
        Me.btn_TSearch = New System.Windows.Forms.Button
        Me.dgv_summary = New System.Windows.Forms.DataGridView
        Me.BillPd = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.STLRun = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hJVNo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GPRefNo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.BatchNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Offsetno = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ChargeType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.APAmt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ArAMT = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NSSAmt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DUEDATE = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Remarks = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hPostType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cmd_GenerateReport = New System.Windows.Forms.Button
        Me.btn_close = New System.Windows.Forms.Button
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgv_summary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.ComboBox1)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.cbo_PostType)
        Me.GroupBox2.Controls.Add(Me.GroupBox1)
        Me.GroupBox2.Controls.Add(Me.btn_Remarks)
        Me.GroupBox2.Controls.Add(Me.btn_Post)
        Me.GroupBox2.Controls.Add(Me.cbo_BillPd)
        Me.GroupBox2.Controls.Add(Me.lbl_BillPd)
        Me.GroupBox2.Controls.Add(Me.btn_TSearch)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1025, 116)
        Me.GroupBox2.TabIndex = 35
        Me.GroupBox2.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 16)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "Charge Type:"
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(106, 46)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(171, 21)
        Me.ComboBox1.TabIndex = 44
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 16)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = "Posting Type:"
        '
        'cbo_PostType
        '
        Me.cbo_PostType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_PostType.FormattingEnabled = True
        Me.cbo_PostType.Location = New System.Drawing.Point(106, 19)
        Me.cbo_PostType.Name = "cbo_PostType"
        Me.cbo_PostType.Size = New System.Drawing.Size(171, 21)
        Me.cbo_PostType.TabIndex = 42
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rb_Open)
        Me.GroupBox1.Controls.Add(Me.rb_Posted)
        Me.GroupBox1.Controls.Add(Me.rb_All)
        Me.GroupBox1.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(665, 14)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(196, 43)
        Me.GroupBox1.TabIndex = 40
        Me.GroupBox1.TabStop = False
        '
        'rb_Open
        '
        Me.rb_Open.AutoSize = True
        Me.rb_Open.Location = New System.Drawing.Point(5, 12)
        Me.rb_Open.Name = "rb_Open"
        Me.rb_Open.Size = New System.Drawing.Size(79, 19)
        Me.rb_Open.TabIndex = 38
        Me.rb_Open.TabStop = True
        Me.rb_Open.Text = "For Posting"
        Me.rb_Open.UseVisualStyleBackColor = True
        '
        'rb_Posted
        '
        Me.rb_Posted.AutoSize = True
        Me.rb_Posted.Location = New System.Drawing.Point(90, 12)
        Me.rb_Posted.Name = "rb_Posted"
        Me.rb_Posted.Size = New System.Drawing.Size(57, 19)
        Me.rb_Posted.TabIndex = 37
        Me.rb_Posted.TabStop = True
        Me.rb_Posted.Text = "Posted"
        Me.rb_Posted.UseVisualStyleBackColor = True
        '
        'rb_All
        '
        Me.rb_All.AutoSize = True
        Me.rb_All.Location = New System.Drawing.Point(153, 12)
        Me.rb_All.Name = "rb_All"
        Me.rb_All.Size = New System.Drawing.Size(38, 19)
        Me.rb_All.TabIndex = 39
        Me.rb_All.TabStop = True
        Me.rb_All.Text = "All"
        Me.rb_All.UseVisualStyleBackColor = True
        '
        'btn_Remarks
        '
        Me.btn_Remarks.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Remarks.ForeColor = System.Drawing.Color.Black
        Me.btn_Remarks.Image = Global.AccountsManagementForms.My.Resources.Resources.save
        Me.btn_Remarks.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Remarks.Location = New System.Drawing.Point(867, 81)
        Me.btn_Remarks.Name = "btn_Remarks"
        Me.btn_Remarks.Size = New System.Drawing.Size(151, 29)
        Me.btn_Remarks.TabIndex = 1
        Me.btn_Remarks.Text = "      Update GP Ref No."
        Me.btn_Remarks.UseVisualStyleBackColor = True
        '
        'btn_Post
        '
        Me.btn_Post.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Post.ForeColor = System.Drawing.Color.Black
        Me.btn_Post.Image = Global.AccountsManagementForms.My.Resources.Resources.execute
        Me.btn_Post.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Post.Location = New System.Drawing.Point(867, 46)
        Me.btn_Post.Name = "btn_Post"
        Me.btn_Post.Size = New System.Drawing.Size(151, 29)
        Me.btn_Post.TabIndex = 37
        Me.btn_Post.Text = "Post"
        Me.btn_Post.UseVisualStyleBackColor = True
        '
        'cbo_BillPd
        '
        Me.cbo_BillPd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_BillPd.FormattingEnabled = True
        Me.cbo_BillPd.Location = New System.Drawing.Point(106, 73)
        Me.cbo_BillPd.Name = "cbo_BillPd"
        Me.cbo_BillPd.Size = New System.Drawing.Size(171, 21)
        Me.cbo_BillPd.TabIndex = 36
        '
        'lbl_BillPd
        '
        Me.lbl_BillPd.AutoSize = True
        Me.lbl_BillPd.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_BillPd.Location = New System.Drawing.Point(12, 74)
        Me.lbl_BillPd.Name = "lbl_BillPd"
        Me.lbl_BillPd.Size = New System.Drawing.Size(88, 16)
        Me.lbl_BillPd.TabIndex = 32
        Me.lbl_BillPd.Text = "Billing Period:"
        '
        'btn_TSearch
        '
        Me.btn_TSearch.AccessibleName = ""
        Me.btn_TSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btn_TSearch.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_TSearch.ForeColor = System.Drawing.Color.Black
        Me.btn_TSearch.Image = Global.AccountsManagementForms.My.Resources.Resources.search
        Me.btn_TSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_TSearch.Location = New System.Drawing.Point(867, 14)
        Me.btn_TSearch.Name = "btn_TSearch"
        Me.btn_TSearch.Size = New System.Drawing.Size(151, 29)
        Me.btn_TSearch.TabIndex = 2
        Me.btn_TSearch.Text = "   Search"
        Me.btn_TSearch.UseVisualStyleBackColor = True
        '
        'dgv_summary
        '
        Me.dgv_summary.AllowUserToAddRows = False
        Me.dgv_summary.AllowUserToDeleteRows = False
        Me.dgv_summary.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_summary.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_summary.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_summary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_summary.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_summary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_summary.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.BillPd, Me.STLRun, Me.hJVNo, Me.GPRefNo, Me.BatchNumber, Me.Offsetno, Me.ChargeType, Me.APAmt, Me.ArAMT, Me.NSSAmt, Me.DUEDATE, Me.Status, Me.Remarks, Me.hPostType})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_summary.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgv_summary.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv_summary.Location = New System.Drawing.Point(0, 135)
        Me.dgv_summary.Name = "dgv_summary"
        Me.dgv_summary.ReadOnly = True
        Me.dgv_summary.RowHeadersVisible = False
        Me.dgv_summary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_summary.Size = New System.Drawing.Size(1025, 406)
        Me.dgv_summary.TabIndex = 36
        '
        'BillPd
        '
        Me.BillPd.HeaderText = "BillingPeriod"
        Me.BillPd.Name = "BillPd"
        Me.BillPd.ReadOnly = True
        Me.BillPd.Width = 95
        '
        'STLRun
        '
        Me.STLRun.HeaderText = "SettlementRun"
        Me.STLRun.Name = "STLRun"
        Me.STLRun.ReadOnly = True
        Me.STLRun.Width = 103
        '
        'hJVNo
        '
        Me.hJVNo.HeaderText = "JVNumber"
        Me.hJVNo.Name = "hJVNo"
        Me.hJVNo.ReadOnly = True
        Me.hJVNo.Width = 82
        '
        'GPRefNo
        '
        Me.GPRefNo.HeaderText = "GPRefNo"
        Me.GPRefNo.Name = "GPRefNo"
        Me.GPRefNo.ReadOnly = True
        Me.GPRefNo.Width = 75
        '
        'BatchNumber
        '
        Me.BatchNumber.HeaderText = "BatchNum"
        Me.BatchNumber.Name = "BatchNumber"
        Me.BatchNumber.ReadOnly = True
        Me.BatchNumber.Visible = False
        Me.BatchNumber.Width = 82
        '
        'Offsetno
        '
        Me.Offsetno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Offsetno.HeaderText = "OffsetNumber"
        Me.Offsetno.Name = "Offsetno"
        Me.Offsetno.ReadOnly = True
        Me.Offsetno.Visible = False
        '
        'ChargeType
        '
        Me.ChargeType.HeaderText = "ChargeType"
        Me.ChargeType.Name = "ChargeType"
        Me.ChargeType.ReadOnly = True
        Me.ChargeType.Width = 90
        '
        'APAmt
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.APAmt.DefaultCellStyle = DataGridViewCellStyle3
        Me.APAmt.HeaderText = "APAmount"
        Me.APAmt.Name = "APAmt"
        Me.APAmt.ReadOnly = True
        Me.APAmt.Width = 83
        '
        'ArAMT
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ArAMT.DefaultCellStyle = DataGridViewCellStyle4
        Me.ArAMT.HeaderText = "ARAmount"
        Me.ArAMT.Name = "ArAMT"
        Me.ArAMT.ReadOnly = True
        Me.ArAMT.Width = 83
        '
        'NSSAmt
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.NSSAmt.DefaultCellStyle = DataGridViewCellStyle5
        Me.NSSAmt.HeaderText = "NSSAmount"
        Me.NSSAmt.Name = "NSSAmt"
        Me.NSSAmt.ReadOnly = True
        Me.NSSAmt.Width = 90
        '
        'DUEDATE
        '
        Me.DUEDATE.HeaderText = "DueDate"
        Me.DUEDATE.Name = "DUEDATE"
        Me.DUEDATE.ReadOnly = True
        Me.DUEDATE.Width = 73
        '
        'Status
        '
        Me.Status.HeaderText = "Posted"
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Width = 65
        '
        'Remarks
        '
        Me.Remarks.HeaderText = "Remarks"
        Me.Remarks.Name = "Remarks"
        Me.Remarks.ReadOnly = True
        Me.Remarks.Width = 74
        '
        'hPostType
        '
        Me.hPostType.HeaderText = "postType"
        Me.hPostType.Name = "hPostType"
        Me.hPostType.ReadOnly = True
        Me.hPostType.Visible = False
        Me.hPostType.Width = 76
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmd_GenerateReport)
        Me.Panel1.Controls.Add(Me.btn_close)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 547)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1025, 51)
        Me.Panel1.TabIndex = 47
        '
        'cmd_GenerateReport
        '
        Me.cmd_GenerateReport.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_GenerateReport.ForeColor = System.Drawing.Color.Black
        Me.cmd_GenerateReport.Image = Global.AccountsManagementForms.My.Resources.Resources.report
        Me.cmd_GenerateReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_GenerateReport.Location = New System.Drawing.Point(12, 6)
        Me.cmd_GenerateReport.Name = "cmd_GenerateReport"
        Me.cmd_GenerateReport.Size = New System.Drawing.Size(220, 33)
        Me.cmd_GenerateReport.TabIndex = 46
        Me.cmd_GenerateReport.Text = "Generate Journal Voucher"
        Me.cmd_GenerateReport.UseVisualStyleBackColor = True
        '
        'btn_close
        '
        Me.btn_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_close.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_close.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btn_close.Image = Global.AccountsManagementForms.My.Resources.Resources.close
        Me.btn_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_close.Location = New System.Drawing.Point(867, 6)
        Me.btn_close.MinimumSize = New System.Drawing.Size(151, 33)
        Me.btn_close.Name = "btn_close"
        Me.btn_close.Size = New System.Drawing.Size(151, 33)
        Me.btn_close.TabIndex = 37
        Me.btn_close.Text = "Close"
        Me.btn_close.UseVisualStyleBackColor = True
        '
        'frmGreatPlainsMonitoring
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1025, 598)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.dgv_summary)
        Me.Controls.Add(Me.GroupBox2)
        Me.Name = "frmGreatPlainsMonitoring"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Posting to Great Plains"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgv_summary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_BillPd As System.Windows.Forms.Label
    Friend WithEvents btn_TSearch As System.Windows.Forms.Button
    Friend WithEvents cbo_BillPd As System.Windows.Forms.ComboBox
    Friend WithEvents dgv_summary As System.Windows.Forms.DataGridView
    Friend WithEvents rb_All As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Open As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Posted As System.Windows.Forms.RadioButton
    Friend WithEvents btn_Post As System.Windows.Forms.Button
    Friend WithEvents btn_Remarks As System.Windows.Forms.Button
    Friend WithEvents btn_close As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmd_GenerateReport As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cbo_PostType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BillPd As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents STLRun As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hJVNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GPRefNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BatchNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Offsetno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChargeType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents APAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ArAMT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NSSAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DUEDATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Remarks As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hPostType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
End Class
