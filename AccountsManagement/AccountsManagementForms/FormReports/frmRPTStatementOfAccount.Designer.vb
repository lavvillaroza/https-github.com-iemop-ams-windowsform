<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRPTStatementOfAccount
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
        Me.cbo_dueDate = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmd_browse = New System.Windows.Forms.Button()
        Me.txt_FilePath = New System.Windows.Forms.TextBox()
        Me.cmd_GenerateReport = New System.Windows.Forms.Button()
        Me.cmd_PDFExport = New System.Windows.Forms.Button()
        Me.cmd_EmailToParticipants = New System.Windows.Forms.Button()
        Me.cmd_close = New System.Windows.Forms.Button()
        Me.cmd_preview = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cbo_dueDate
        '
        Me.cbo_dueDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_dueDate.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_dueDate.FormattingEnabled = True
        Me.cbo_dueDate.Location = New System.Drawing.Point(111, 7)
        Me.cbo_dueDate.Name = "cbo_dueDate"
        Me.cbo_dueDate.Size = New System.Drawing.Size(209, 22)
        Me.cbo_dueDate.TabIndex = 30
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(11, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 14)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Select Due Date:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(12, 104)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 14)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Select Destination Folder:"
        '
        'cmd_browse
        '
        Me.cmd_browse.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_browse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_browse.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_browse.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_browse.ForeColor = System.Drawing.Color.Black
        Me.cmd_browse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_browse.Location = New System.Drawing.Point(285, 117)
        Me.cmd_browse.Name = "cmd_browse"
        Me.cmd_browse.Size = New System.Drawing.Size(35, 30)
        Me.cmd_browse.TabIndex = 25
        Me.cmd_browse.Text = "....."
        Me.cmd_browse.UseVisualStyleBackColor = True
        '
        'txt_FilePath
        '
        Me.txt_FilePath.Enabled = False
        Me.txt_FilePath.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_FilePath.ForeColor = System.Drawing.Color.Black
        Me.txt_FilePath.Location = New System.Drawing.Point(15, 122)
        Me.txt_FilePath.Name = "txt_FilePath"
        Me.txt_FilePath.Size = New System.Drawing.Size(264, 20)
        Me.txt_FilePath.TabIndex = 24
        '
        'cmd_GenerateReport
        '
        Me.cmd_GenerateReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_GenerateReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_GenerateReport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_GenerateReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_GenerateReport.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_GenerateReport.ForeColor = System.Drawing.Color.Black
        Me.cmd_GenerateReport.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.cmd_GenerateReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_GenerateReport.Location = New System.Drawing.Point(15, 35)
        Me.cmd_GenerateReport.Name = "cmd_GenerateReport"
        Me.cmd_GenerateReport.Size = New System.Drawing.Size(150, 39)
        Me.cmd_GenerateReport.TabIndex = 27
        Me.cmd_GenerateReport.Text = "Generate SOA"
        Me.cmd_GenerateReport.UseVisualStyleBackColor = True
        '
        'cmd_PDFExport
        '
        Me.cmd_PDFExport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_PDFExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_PDFExport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_PDFExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_PDFExport.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_PDFExport.ForeColor = System.Drawing.Color.Black
        Me.cmd_PDFExport.Image = Global.AccountsManagementForms.My.Resources.Resources.PDFIcon22x22
        Me.cmd_PDFExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_PDFExport.Location = New System.Drawing.Point(15, 153)
        Me.cmd_PDFExport.Name = "cmd_PDFExport"
        Me.cmd_PDFExport.Size = New System.Drawing.Size(150, 39)
        Me.cmd_PDFExport.TabIndex = 23
        Me.cmd_PDFExport.Text = "Export to PDF"
        Me.cmd_PDFExport.UseVisualStyleBackColor = True
        '
        'cmd_EmailToParticipants
        '
        Me.cmd_EmailToParticipants.Enabled = False
        Me.cmd_EmailToParticipants.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_EmailToParticipants.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_EmailToParticipants.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_EmailToParticipants.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_EmailToParticipants.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_EmailToParticipants.ForeColor = System.Drawing.Color.Black
        Me.cmd_EmailToParticipants.Image = Global.AccountsManagementForms.My.Resources.Resources.PostIcon22x22
        Me.cmd_EmailToParticipants.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_EmailToParticipants.Location = New System.Drawing.Point(170, 153)
        Me.cmd_EmailToParticipants.Name = "cmd_EmailToParticipants"
        Me.cmd_EmailToParticipants.Size = New System.Drawing.Size(150, 39)
        Me.cmd_EmailToParticipants.TabIndex = 22
        Me.cmd_EmailToParticipants.Text = "     Email to Participants"
        Me.cmd_EmailToParticipants.UseVisualStyleBackColor = True
        '
        'cmd_close
        '
        Me.cmd_close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_close.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_close.ForeColor = System.Drawing.Color.Black
        Me.cmd_close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_close.Location = New System.Drawing.Point(136, 203)
        Me.cmd_close.Name = "cmd_close"
        Me.cmd_close.Size = New System.Drawing.Size(184, 39)
        Me.cmd_close.TabIndex = 21
        Me.cmd_close.Text = "Close"
        Me.cmd_close.UseVisualStyleBackColor = True
        '
        'cmd_preview
        '
        Me.cmd_preview.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_preview.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_preview.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_preview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_preview.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_preview.ForeColor = System.Drawing.Color.Black
        Me.cmd_preview.Image = Global.AccountsManagementForms.My.Resources.Resources.FileSearchIcon22x22
        Me.cmd_preview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_preview.Location = New System.Drawing.Point(170, 35)
        Me.cmd_preview.Name = "cmd_preview"
        Me.cmd_preview.Size = New System.Drawing.Size(150, 39)
        Me.cmd_preview.TabIndex = 20
        Me.cmd_preview.Text = "Preview"
        Me.cmd_preview.UseVisualStyleBackColor = True
        '
        'frmRPTStatementOfAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(335, 254)
        Me.Controls.Add(Me.cbo_dueDate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmd_GenerateReport)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmd_browse)
        Me.Controls.Add(Me.txt_FilePath)
        Me.Controls.Add(Me.cmd_PDFExport)
        Me.Controls.Add(Me.cmd_EmailToParticipants)
        Me.Controls.Add(Me.cmd_close)
        Me.Controls.Add(Me.cmd_preview)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmRPTStatementOfAccount"
        Me.Text = "Statement of Account"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbo_dueDate As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmd_GenerateReport As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmd_browse As System.Windows.Forms.Button
    Friend WithEvents txt_FilePath As System.Windows.Forms.TextBox
    Friend WithEvents cmd_PDFExport As System.Windows.Forms.Button
    Friend WithEvents cmd_EmailToParticipants As System.Windows.Forms.Button
    Friend WithEvents cmd_close As System.Windows.Forms.Button
    Friend WithEvents cmd_preview As System.Windows.Forms.Button
End Class
