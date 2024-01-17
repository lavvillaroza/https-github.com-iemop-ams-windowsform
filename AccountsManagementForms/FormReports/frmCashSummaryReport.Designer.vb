<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCashSummaryReport
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtp_to = New System.Windows.Forms.DateTimePicker()
        Me.dtp_From = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_AccountCode = New System.Windows.Forms.TextBox()
        Me.cbo_AccountToGenerate = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.rb_Descending = New System.Windows.Forms.RadioButton()
        Me.rb_Ascending = New System.Windows.Forms.RadioButton()
        Me.cbo_ColumnSort = New System.Windows.Forms.ComboBox()
        Me.cmd_Generate = New System.Windows.Forms.Button()
        Me.cmd_Close = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtp_to)
        Me.GroupBox1.Controls.Add(Me.dtp_From)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(226, 94)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Choose Date:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(27, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "To:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(27, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "From:"
        '
        'dtp_to
        '
        Me.dtp_to.CalendarFont = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_to.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_to.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_to.Location = New System.Drawing.Point(73, 58)
        Me.dtp_to.Name = "dtp_to"
        Me.dtp_to.Size = New System.Drawing.Size(118, 20)
        Me.dtp_to.TabIndex = 1
        '
        'dtp_From
        '
        Me.dtp_From.CalendarFont = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_From.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_From.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_From.Location = New System.Drawing.Point(73, 19)
        Me.dtp_From.Name = "dtp_From"
        Me.dtp_From.Size = New System.Drawing.Size(118, 20)
        Me.dtp_From.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txt_AccountCode)
        Me.GroupBox2.Controls.Add(Me.cbo_AccountToGenerate)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(12, 112)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(226, 95)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Choose Account:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 14)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Account Code:"
        '
        'txt_AccountCode
        '
        Me.txt_AccountCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_AccountCode.Location = New System.Drawing.Point(6, 65)
        Me.txt_AccountCode.Name = "txt_AccountCode"
        Me.txt_AccountCode.ReadOnly = True
        Me.txt_AccountCode.Size = New System.Drawing.Size(214, 20)
        Me.txt_AccountCode.TabIndex = 1
        Me.txt_AccountCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cbo_AccountToGenerate
        '
        Me.cbo_AccountToGenerate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbo_AccountToGenerate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbo_AccountToGenerate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_AccountToGenerate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_AccountToGenerate.FormattingEnabled = True
        Me.cbo_AccountToGenerate.Location = New System.Drawing.Point(6, 21)
        Me.cbo_AccountToGenerate.Name = "cbo_AccountToGenerate"
        Me.cbo_AccountToGenerate.Size = New System.Drawing.Size(214, 22)
        Me.cbo_AccountToGenerate.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rb_Descending)
        Me.GroupBox3.Controls.Add(Me.rb_Ascending)
        Me.GroupBox3.Controls.Add(Me.cbo_ColumnSort)
        Me.GroupBox3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox3.Location = New System.Drawing.Point(12, 215)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(226, 78)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Sort Columns By:"
        '
        'rb_Descending
        '
        Me.rb_Descending.AutoSize = True
        Me.rb_Descending.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_Descending.ForeColor = System.Drawing.Color.Black
        Me.rb_Descending.Location = New System.Drawing.Point(129, 51)
        Me.rb_Descending.Name = "rb_Descending"
        Me.rb_Descending.Size = New System.Drawing.Size(82, 18)
        Me.rb_Descending.TabIndex = 7
        Me.rb_Descending.Text = "Descending"
        Me.rb_Descending.UseVisualStyleBackColor = True
        '
        'rb_Ascending
        '
        Me.rb_Ascending.AutoSize = True
        Me.rb_Ascending.Checked = True
        Me.rb_Ascending.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_Ascending.ForeColor = System.Drawing.Color.Black
        Me.rb_Ascending.Location = New System.Drawing.Point(6, 51)
        Me.rb_Ascending.Name = "rb_Ascending"
        Me.rb_Ascending.Size = New System.Drawing.Size(77, 18)
        Me.rb_Ascending.TabIndex = 6
        Me.rb_Ascending.TabStop = True
        Me.rb_Ascending.Text = "Ascending"
        Me.rb_Ascending.UseVisualStyleBackColor = True
        '
        'cbo_ColumnSort
        '
        Me.cbo_ColumnSort.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbo_ColumnSort.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbo_ColumnSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_ColumnSort.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_ColumnSort.FormattingEnabled = True
        Me.cbo_ColumnSort.Location = New System.Drawing.Point(6, 21)
        Me.cbo_ColumnSort.Name = "cbo_ColumnSort"
        Me.cbo_ColumnSort.Size = New System.Drawing.Size(214, 23)
        Me.cbo_ColumnSort.TabIndex = 5
        '
        'cmd_Generate
        '
        Me.cmd_Generate.BackColor = System.Drawing.Color.White
        Me.cmd_Generate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Generate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Generate.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Generate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Generate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Generate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.cmd_Generate.Image = Global.AccountsManagementForms.My.Resources.Resources.DownloadIcon22x22
        Me.cmd_Generate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Generate.Location = New System.Drawing.Point(11, 299)
        Me.cmd_Generate.Name = "cmd_Generate"
        Me.cmd_Generate.Size = New System.Drawing.Size(226, 39)
        Me.cmd_Generate.TabIndex = 3
        Me.cmd_Generate.Text = "&Generate"
        Me.cmd_Generate.UseVisualStyleBackColor = False
        '
        'cmd_Close
        '
        Me.cmd_Close.BackColor = System.Drawing.Color.White
        Me.cmd_Close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Close.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Close.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.cmd_Close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIcon22x22
        Me.cmd_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Close.Location = New System.Drawing.Point(11, 344)
        Me.cmd_Close.Name = "cmd_Close"
        Me.cmd_Close.Size = New System.Drawing.Size(226, 39)
        Me.cmd_Close.TabIndex = 4
        Me.cmd_Close.Text = "&Close"
        Me.cmd_Close.UseVisualStyleBackColor = False
        '
        'frmCashSummaryReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(249, 403)
        Me.Controls.Add(Me.cmd_Close)
        Me.Controls.Add(Me.cmd_Generate)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmCashSummaryReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Cash Summary Report"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtp_to As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_From As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt_AccountCode As System.Windows.Forms.TextBox
    Friend WithEvents cbo_AccountToGenerate As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cbo_ColumnSort As System.Windows.Forms.ComboBox
    Friend WithEvents cmd_Generate As System.Windows.Forms.Button
    Friend WithEvents cmd_Close As System.Windows.Forms.Button
    Friend WithEvents rb_Descending As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Ascending As System.Windows.Forms.RadioButton
End Class
