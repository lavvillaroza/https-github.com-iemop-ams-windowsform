<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAgingReport
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
        Me.cbo_TransactionDate = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.rb_APBills = New System.Windows.Forms.RadioButton()
        Me.rb_ARBills = New System.Windows.Forms.RadioButton()
        Me.cmd_Close = New System.Windows.Forms.Button()
        Me.cmd_Generate = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.lstChkBox = New System.Windows.Forms.CheckedListBox()
        Me.chbox_AllParticipants = New System.Windows.Forms.CheckBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.rb_OutstandingBills = New System.Windows.Forms.RadioButton()
        Me.rb_AllBills = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'cbo_TransactionDate
        '
        Me.cbo_TransactionDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_TransactionDate.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_TransactionDate.FormattingEnabled = True
        Me.cbo_TransactionDate.Location = New System.Drawing.Point(6, 19)
        Me.cbo_TransactionDate.Name = "cbo_TransactionDate"
        Me.cbo_TransactionDate.Size = New System.Drawing.Size(254, 20)
        Me.cbo_TransactionDate.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cbo_TransactionDate)
        Me.GroupBox1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(266, 51)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Transaction Date (As Of):"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(17, 262)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(243, 16)
        Me.CheckBox1.TabIndex = 7
        Me.CheckBox1.Text = "Include Collection and Payment Transactions"
        Me.CheckBox1.UseVisualStyleBackColor = True
        Me.CheckBox1.Visible = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.rb_APBills)
        Me.GroupBox5.Controls.Add(Me.rb_ARBills)
        Me.GroupBox5.Location = New System.Drawing.Point(12, 275)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(266, 51)
        Me.GroupBox5.TabIndex = 5
        Me.GroupBox5.TabStop = False
        '
        'rb_APBills
        '
        Me.rb_APBills.AutoSize = True
        Me.rb_APBills.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_APBills.Location = New System.Drawing.Point(143, 20)
        Me.rb_APBills.Name = "rb_APBills"
        Me.rb_APBills.Size = New System.Drawing.Size(111, 16)
        Me.rb_APBills.TabIndex = 1
        Me.rb_APBills.Text = "Accounts Payable"
        Me.rb_APBills.UseVisualStyleBackColor = True
        '
        'rb_ARBills
        '
        Me.rb_ARBills.AutoSize = True
        Me.rb_ARBills.Checked = True
        Me.rb_ARBills.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_ARBills.Location = New System.Drawing.Point(6, 20)
        Me.rb_ARBills.Name = "rb_ARBills"
        Me.rb_ARBills.Size = New System.Drawing.Size(126, 16)
        Me.rb_ARBills.TabIndex = 0
        Me.rb_ARBills.TabStop = True
        Me.rb_ARBills.Text = "Accounts Receivable"
        Me.rb_ARBills.UseVisualStyleBackColor = True
        '
        'cmd_Close
        '
        Me.cmd_Close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Close.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Close.ForeColor = System.Drawing.Color.Black
        Me.cmd_Close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Close.Location = New System.Drawing.Point(12, 380)
        Me.cmd_Close.Name = "cmd_Close"
        Me.cmd_Close.Size = New System.Drawing.Size(266, 42)
        Me.cmd_Close.TabIndex = 6
        Me.cmd_Close.Text = "Close"
        Me.cmd_Close.UseVisualStyleBackColor = True
        '
        'cmd_Generate
        '
        Me.cmd_Generate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Generate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Generate.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Generate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Generate.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Generate.ForeColor = System.Drawing.Color.Black
        Me.cmd_Generate.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.cmd_Generate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Generate.Location = New System.Drawing.Point(12, 332)
        Me.cmd_Generate.Name = "cmd_Generate"
        Me.cmd_Generate.Size = New System.Drawing.Size(266, 42)
        Me.cmd_Generate.TabIndex = 5
        Me.cmd_Generate.Text = "Generate Report"
        Me.cmd_Generate.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lstChkBox)
        Me.GroupBox4.Controls.Add(Me.chbox_AllParticipants)
        Me.GroupBox4.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox4.Location = New System.Drawing.Point(12, 69)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(266, 191)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Select Participants:"
        '
        'lstChkBox
        '
        Me.lstChkBox.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstChkBox.FormattingEnabled = True
        Me.lstChkBox.Location = New System.Drawing.Point(6, 49)
        Me.lstChkBox.Name = "lstChkBox"
        Me.lstChkBox.Size = New System.Drawing.Size(254, 124)
        Me.lstChkBox.TabIndex = 2
        '
        'chbox_AllParticipants
        '
        Me.chbox_AllParticipants.AutoSize = True
        Me.chbox_AllParticipants.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbox_AllParticipants.ForeColor = System.Drawing.Color.Black
        Me.chbox_AllParticipants.Location = New System.Drawing.Point(6, 20)
        Me.chbox_AllParticipants.Name = "chbox_AllParticipants"
        Me.chbox_AllParticipants.Size = New System.Drawing.Size(95, 16)
        Me.chbox_AllParticipants.TabIndex = 0
        Me.chbox_AllParticipants.Text = "All Participants"
        Me.chbox_AllParticipants.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rb_OutstandingBills)
        Me.GroupBox3.Controls.Add(Me.rb_AllBills)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 280)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(266, 51)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Select WESM Bills"
        Me.GroupBox3.Visible = False
        '
        'rb_OutstandingBills
        '
        Me.rb_OutstandingBills.AutoSize = True
        Me.rb_OutstandingBills.Checked = True
        Me.rb_OutstandingBills.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_OutstandingBills.Location = New System.Drawing.Point(104, 19)
        Me.rb_OutstandingBills.Name = "rb_OutstandingBills"
        Me.rb_OutstandingBills.Size = New System.Drawing.Size(154, 16)
        Me.rb_OutstandingBills.TabIndex = 0
        Me.rb_OutstandingBills.TabStop = True
        Me.rb_OutstandingBills.Text = "With Outstanding Balances"
        Me.rb_OutstandingBills.UseVisualStyleBackColor = True
        '
        'rb_AllBills
        '
        Me.rb_AllBills.AutoSize = True
        Me.rb_AllBills.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_AllBills.Location = New System.Drawing.Point(6, 19)
        Me.rb_AllBills.Name = "rb_AllBills"
        Me.rb_AllBills.Size = New System.Drawing.Size(92, 16)
        Me.rb_AllBills.TabIndex = 0
        Me.rb_AllBills.Text = "All WESM Bills"
        Me.rb_AllBills.UseVisualStyleBackColor = True
        '
        'frmAgingReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(292, 437)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.cmd_Close)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmd_Generate)
        Me.Controls.Add(Me.GroupBox4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(320, 508)
        Me.MinimumSize = New System.Drawing.Size(300, 470)
        Me.Name = "frmAgingReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Historical Aged Trial Balance"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbo_TransactionDate As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_OutstandingBills As System.Windows.Forms.RadioButton
    Friend WithEvents rb_AllBills As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents chbox_AllParticipants As System.Windows.Forms.CheckBox
    Friend WithEvents cmd_Close As System.Windows.Forms.Button
    Friend WithEvents cmd_Generate As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_APBills As System.Windows.Forms.RadioButton
    Friend WithEvents rb_ARBills As System.Windows.Forms.RadioButton
    Friend WithEvents lstChkBox As System.Windows.Forms.CheckedListBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
End Class
