<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSTLNotice
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
        Me.lstbx_Participants = New System.Windows.Forms.ListBox()
        Me.cbo_date = New System.Windows.Forms.ComboBox()
        Me.gbox_main = New System.Windows.Forms.GroupBox()
        Me.gbox_fltrDate = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txt_Note = New System.Windows.Forms.TextBox()
        Me.chk_Note = New System.Windows.Forms.CheckBox()
        Me.gbox_Participants = New System.Windows.Forms.GroupBox()
        Me.cmd_gen = New System.Windows.Forms.Button()
        Me.cmd_Generate = New System.Windows.Forms.Button()
        Me.gbox_Details = New System.Windows.Forms.GroupBox()
        Me.dgView = New System.Windows.Forms.DataGridView()
        Me.gbox_main.SuspendLayout()
        Me.gbox_fltrDate.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.gbox_Participants.SuspendLayout()
        Me.gbox_Details.SuspendLayout()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lstbx_Participants
        '
        Me.lstbx_Participants.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstbx_Participants.FormattingEnabled = True
        Me.lstbx_Participants.ItemHeight = 15
        Me.lstbx_Participants.Location = New System.Drawing.Point(3, 19)
        Me.lstbx_Participants.Name = "lstbx_Participants"
        Me.lstbx_Participants.Size = New System.Drawing.Size(200, 290)
        Me.lstbx_Participants.TabIndex = 0
        '
        'cbo_date
        '
        Me.cbo_date.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_date.FormattingEnabled = True
        Me.cbo_date.Location = New System.Drawing.Point(6, 19)
        Me.cbo_date.Name = "cbo_date"
        Me.cbo_date.Size = New System.Drawing.Size(188, 23)
        Me.cbo_date.TabIndex = 1
        '
        'gbox_main
        '
        Me.gbox_main.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gbox_main.Controls.Add(Me.gbox_fltrDate)
        Me.gbox_main.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbox_main.Location = New System.Drawing.Point(12, 12)
        Me.gbox_main.Name = "gbox_main"
        Me.gbox_main.Size = New System.Drawing.Size(222, 602)
        Me.gbox_main.TabIndex = 2
        Me.gbox_main.TabStop = False
        Me.gbox_main.Text = "Settlement Notice"
        '
        'gbox_fltrDate
        '
        Me.gbox_fltrDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gbox_fltrDate.Controls.Add(Me.GroupBox1)
        Me.gbox_fltrDate.Controls.Add(Me.gbox_Participants)
        Me.gbox_fltrDate.Controls.Add(Me.cmd_gen)
        Me.gbox_fltrDate.Controls.Add(Me.cbo_date)
        Me.gbox_fltrDate.Controls.Add(Me.cmd_Generate)
        Me.gbox_fltrDate.Location = New System.Drawing.Point(6, 19)
        Me.gbox_fltrDate.Name = "gbox_fltrDate"
        Me.gbox_fltrDate.Size = New System.Drawing.Size(206, 577)
        Me.gbox_fltrDate.TabIndex = 3
        Me.gbox_fltrDate.TabStop = False
        Me.gbox_fltrDate.Text = "Transaction Date (As Of)"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txt_Note)
        Me.GroupBox1.Controls.Add(Me.chk_Note)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 124)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(202, 135)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        '
        'txt_Note
        '
        Me.txt_Note.Location = New System.Drawing.Point(6, 38)
        Me.txt_Note.Multiline = True
        Me.txt_Note.Name = "txt_Note"
        Me.txt_Note.ReadOnly = True
        Me.txt_Note.Size = New System.Drawing.Size(188, 91)
        Me.txt_Note.TabIndex = 1
        Me.txt_Note.Text = "Payments will be remitted on the 1st banking day of the following month."
        '
        'chk_Note
        '
        Me.chk_Note.AutoSize = True
        Me.chk_Note.Location = New System.Drawing.Point(6, 13)
        Me.chk_Note.Name = "chk_Note"
        Me.chk_Note.Size = New System.Drawing.Size(99, 19)
        Me.chk_Note.TabIndex = 0
        Me.chk_Note.Text = "Include Note"
        Me.chk_Note.UseVisualStyleBackColor = True
        '
        'gbox_Participants
        '
        Me.gbox_Participants.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gbox_Participants.Controls.Add(Me.lstbx_Participants)
        Me.gbox_Participants.Location = New System.Drawing.Point(0, 265)
        Me.gbox_Participants.Name = "gbox_Participants"
        Me.gbox_Participants.Size = New System.Drawing.Size(206, 312)
        Me.gbox_Participants.TabIndex = 4
        Me.gbox_Participants.TabStop = False
        Me.gbox_Participants.Text = "Participant/s:"
        '
        'cmd_gen
        '
        Me.cmd_gen.Image = Global.AccountsManagementForms.My.Resources.Resources.report1
        Me.cmd_gen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_gen.Location = New System.Drawing.Point(6, 86)
        Me.cmd_gen.Name = "cmd_gen"
        Me.cmd_gen.Size = New System.Drawing.Size(188, 32)
        Me.cmd_gen.TabIndex = 6
        Me.cmd_gen.Text = "Export to CSV"
        Me.cmd_gen.UseVisualStyleBackColor = True
        '
        'cmd_Generate
        '
        Me.cmd_Generate.Image = Global.AccountsManagementForms.My.Resources.Resources.execute
        Me.cmd_Generate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Generate.Location = New System.Drawing.Point(6, 48)
        Me.cmd_Generate.Name = "cmd_Generate"
        Me.cmd_Generate.Size = New System.Drawing.Size(188, 32)
        Me.cmd_Generate.TabIndex = 5
        Me.cmd_Generate.Text = "Generate"
        Me.cmd_Generate.UseVisualStyleBackColor = True
        '
        'gbox_Details
        '
        Me.gbox_Details.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbox_Details.Controls.Add(Me.dgView)
        Me.gbox_Details.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbox_Details.Location = New System.Drawing.Point(240, 12)
        Me.gbox_Details.Name = "gbox_Details"
        Me.gbox_Details.Size = New System.Drawing.Size(894, 602)
        Me.gbox_Details.TabIndex = 3
        Me.gbox_Details.TabStop = False
        '
        'dgView
        '
        Me.dgView.AllowUserToAddRows = False
        Me.dgView.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgView.Location = New System.Drawing.Point(3, 17)
        Me.dgView.Name = "dgView"
        Me.dgView.Size = New System.Drawing.Size(888, 582)
        Me.dgView.TabIndex = 0
        '
        'frmSTLNotice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1146, 629)
        Me.Controls.Add(Me.gbox_Details)
        Me.Controls.Add(Me.gbox_main)
        Me.MinimumSize = New System.Drawing.Size(1162, 667)
        Me.Name = "frmSTLNotice"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settlement Notice"
        Me.gbox_main.ResumeLayout(False)
        Me.gbox_fltrDate.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gbox_Participants.ResumeLayout(False)
        Me.gbox_Details.ResumeLayout(False)
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstbx_Participants As System.Windows.Forms.ListBox
    Friend WithEvents cbo_date As System.Windows.Forms.ComboBox
    Friend WithEvents gbox_main As System.Windows.Forms.GroupBox
    Friend WithEvents gbox_Participants As System.Windows.Forms.GroupBox
    Friend WithEvents gbox_fltrDate As System.Windows.Forms.GroupBox
    Friend WithEvents gbox_Details As System.Windows.Forms.GroupBox
    Friend WithEvents dgView As System.Windows.Forms.DataGridView
    Friend WithEvents cmd_gen As System.Windows.Forms.Button
    Friend WithEvents cmd_Generate As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txt_Note As System.Windows.Forms.TextBox
    Friend WithEvents chk_Note As System.Windows.Forms.CheckBox
End Class
