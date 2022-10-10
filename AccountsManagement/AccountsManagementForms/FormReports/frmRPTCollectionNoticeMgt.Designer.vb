<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRPTCollectionNoticeMgt
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
        Me.chk_allParticipants = New System.Windows.Forms.CheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txt_remarks = New System.Windows.Forms.TextBox
        Me.clb_participants = New System.Windows.Forms.CheckedListBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmd_save = New System.Windows.Forms.Button
        Me.cmd_close = New System.Windows.Forms.Button
        Me.cmd_EmailToParticipants = New System.Windows.Forms.Button
        Me.cmd_PDFExport = New System.Windows.Forms.Button
        Me.txt_FilePath = New System.Windows.Forms.TextBox
        Me.cmd_browse = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmd_GenerateReport = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmd_refresh = New System.Windows.Forms.Button
        Me.cbo_dueDate = New System.Windows.Forms.ComboBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'chk_allParticipants
        '
        Me.chk_allParticipants.AutoSize = True
        Me.chk_allParticipants.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_allParticipants.Location = New System.Drawing.Point(178, 0)
        Me.chk_allParticipants.Name = "chk_allParticipants"
        Me.chk_allParticipants.Size = New System.Drawing.Size(97, 19)
        Me.chk_allParticipants.TabIndex = 1
        Me.chk_allParticipants.Text = "All Participants"
        Me.chk_allParticipants.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txt_remarks)
        Me.GroupBox1.Controls.Add(Me.chk_allParticipants)
        Me.GroupBox1.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(275, 38)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(281, 379)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Remarks:"
        '
        'txt_remarks
        '
        Me.txt_remarks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_remarks.Enabled = False
        Me.txt_remarks.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_remarks.Location = New System.Drawing.Point(3, 20)
        Me.txt_remarks.Multiline = True
        Me.txt_remarks.Name = "txt_remarks"
        Me.txt_remarks.Size = New System.Drawing.Size(275, 356)
        Me.txt_remarks.TabIndex = 0
        '
        'clb_participants
        '
        Me.clb_participants.CheckOnClick = True
        Me.clb_participants.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clb_participants.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.clb_participants.FormattingEnabled = True
        Me.clb_participants.Location = New System.Drawing.Point(3, 20)
        Me.clb_participants.Name = "clb_participants"
        Me.clb_participants.Size = New System.Drawing.Size(254, 346)
        Me.clb_participants.TabIndex = 4
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.clb_participants)
        Me.GroupBox2.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(10, 38)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(260, 379)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Choose Participant:"
        '
        'cmd_save
        '
        Me.cmd_save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_save.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_save.Image = Global.AccountsManagementForms.My.Resources.Resources.save
        Me.cmd_save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_save.Location = New System.Drawing.Point(410, 487)
        Me.cmd_save.Name = "cmd_save"
        Me.cmd_save.Size = New System.Drawing.Size(147, 25)
        Me.cmd_save.TabIndex = 4
        Me.cmd_save.Text = "Save"
        Me.cmd_save.UseVisualStyleBackColor = True
        '
        'cmd_close
        '
        Me.cmd_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_close.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_close.Image = Global.AccountsManagementForms.My.Resources.Resources.close
        Me.cmd_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_close.Location = New System.Drawing.Point(410, 517)
        Me.cmd_close.Name = "cmd_close"
        Me.cmd_close.Size = New System.Drawing.Size(147, 25)
        Me.cmd_close.TabIndex = 5
        Me.cmd_close.Text = "Close"
        Me.cmd_close.UseVisualStyleBackColor = True
        '
        'cmd_EmailToParticipants
        '
        Me.cmd_EmailToParticipants.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_EmailToParticipants.Enabled = False
        Me.cmd_EmailToParticipants.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_EmailToParticipants.Image = Global.AccountsManagementForms.My.Resources.Resources.contents
        Me.cmd_EmailToParticipants.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_EmailToParticipants.Location = New System.Drawing.Point(256, 518)
        Me.cmd_EmailToParticipants.Name = "cmd_EmailToParticipants"
        Me.cmd_EmailToParticipants.Size = New System.Drawing.Size(148, 25)
        Me.cmd_EmailToParticipants.TabIndex = 6
        Me.cmd_EmailToParticipants.Text = "Email to Participants"
        Me.cmd_EmailToParticipants.UseVisualStyleBackColor = True
        '
        'cmd_PDFExport
        '
        Me.cmd_PDFExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_PDFExport.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_PDFExport.Image = Global.AccountsManagementForms.My.Resources.Resources.contents
        Me.cmd_PDFExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_PDFExport.Location = New System.Drawing.Point(256, 487)
        Me.cmd_PDFExport.Name = "cmd_PDFExport"
        Me.cmd_PDFExport.Size = New System.Drawing.Size(148, 25)
        Me.cmd_PDFExport.TabIndex = 7
        Me.cmd_PDFExport.Text = "Export to PDF"
        Me.cmd_PDFExport.UseVisualStyleBackColor = True
        '
        'txt_FilePath
        '
        Me.txt_FilePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_FilePath.Enabled = False
        Me.txt_FilePath.Location = New System.Drawing.Point(150, 425)
        Me.txt_FilePath.Name = "txt_FilePath"
        Me.txt_FilePath.Size = New System.Drawing.Size(351, 20)
        Me.txt_FilePath.TabIndex = 8
        '
        'cmd_browse
        '
        Me.cmd_browse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_browse.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_browse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_browse.Location = New System.Drawing.Point(507, 425)
        Me.cmd_browse.Name = "cmd_browse"
        Me.cmd_browse.Size = New System.Drawing.Size(49, 20)
        Me.cmd_browse.TabIndex = 9
        Me.cmd_browse.Text = "....."
        Me.cmd_browse.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 426)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(131, 15)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Select Destination Folder:"
        '
        'cmd_GenerateReport
        '
        Me.cmd_GenerateReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_GenerateReport.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_GenerateReport.Image = Global.AccountsManagementForms.My.Resources.Resources.report
        Me.cmd_GenerateReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_GenerateReport.Location = New System.Drawing.Point(256, 451)
        Me.cmd_GenerateReport.Name = "cmd_GenerateReport"
        Me.cmd_GenerateReport.Size = New System.Drawing.Size(300, 30)
        Me.cmd_GenerateReport.TabIndex = 11
        Me.cmd_GenerateReport.Text = "Preview Statement Of Account"
        Me.cmd_GenerateReport.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(15, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 15)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Select Due Date"
        '
        'cmd_refresh
        '
        Me.cmd_refresh.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_refresh.Image = Global.AccountsManagementForms.My.Resources.Resources.refresh
        Me.cmd_refresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_refresh.Location = New System.Drawing.Point(228, 3)
        Me.cmd_refresh.Name = "cmd_refresh"
        Me.cmd_refresh.Size = New System.Drawing.Size(95, 31)
        Me.cmd_refresh.TabIndex = 16
        Me.cmd_refresh.Text = "Refresh"
        Me.cmd_refresh.UseVisualStyleBackColor = True
        '
        'cbo_dueDate
        '
        Me.cbo_dueDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_dueDate.FormattingEnabled = True
        Me.cbo_dueDate.Location = New System.Drawing.Point(105, 7)
        Me.cbo_dueDate.Name = "cbo_dueDate"
        Me.cbo_dueDate.Size = New System.Drawing.Size(117, 21)
        Me.cbo_dueDate.TabIndex = 17
        '
        'frmRPTCollectionNoticeMgt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(564, 554)
        Me.Controls.Add(Me.cbo_dueDate)
        Me.Controls.Add(Me.cmd_refresh)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmd_GenerateReport)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmd_browse)
        Me.Controls.Add(Me.txt_FilePath)
        Me.Controls.Add(Me.cmd_PDFExport)
        Me.Controls.Add(Me.cmd_EmailToParticipants)
        Me.Controls.Add(Me.cmd_close)
        Me.Controls.Add(Me.cmd_save)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.MinimumSize = New System.Drawing.Size(580, 592)
        Me.Name = "frmRPTCollectionNoticeMgt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Statement of Account"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chk_allParticipants As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txt_remarks As System.Windows.Forms.TextBox
    Friend WithEvents clb_participants As System.Windows.Forms.CheckedListBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmd_save As System.Windows.Forms.Button
    Friend WithEvents cmd_close As System.Windows.Forms.Button
    Friend WithEvents cmd_EmailToParticipants As System.Windows.Forms.Button
    Friend WithEvents cmd_PDFExport As System.Windows.Forms.Button
    Friend WithEvents txt_FilePath As System.Windows.Forms.TextBox
    Friend WithEvents cmd_browse As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmd_GenerateReport As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmd_refresh As System.Windows.Forms.Button
    Friend WithEvents cbo_dueDate As System.Windows.Forms.ComboBox
End Class
