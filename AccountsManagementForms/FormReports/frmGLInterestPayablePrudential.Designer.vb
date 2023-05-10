<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGLInterestPayablePrudential
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGLInterestPayablePrudential))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtp_To = New System.Windows.Forms.DateTimePicker()
        Me.dtp_From = New System.Windows.Forms.DateTimePicker()
        Me.cmd_Generate = New System.Windows.Forms.Button()
        Me.btn_ExportToText = New System.Windows.Forms.Button()
        Me.btn_ExportToDat = New System.Windows.Forms.Button()
        Me.btn_ExportToCSV = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtp_To)
        Me.GroupBox1.Controls.Add(Me.dtp_From)
        Me.GroupBox1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(14, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(378, 55)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Choose Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(212, 20)
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
        Me.Label1.Location = New System.Drawing.Point(15, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "From:"
        '
        'dtp_To
        '
        Me.dtp_To.CalendarFont = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_To.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_To.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_To.Location = New System.Drawing.Point(244, 20)
        Me.dtp_To.Name = "dtp_To"
        Me.dtp_To.Size = New System.Drawing.Size(118, 21)
        Me.dtp_To.TabIndex = 1
        '
        'dtp_From
        '
        Me.dtp_From.CalendarFont = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_From.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_From.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_From.Location = New System.Drawing.Point(59, 20)
        Me.dtp_From.Name = "dtp_From"
        Me.dtp_From.Size = New System.Drawing.Size(118, 21)
        Me.dtp_From.TabIndex = 0
        '
        'cmd_Generate
        '
        Me.cmd_Generate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Generate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Generate.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Generate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Generate.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Generate.Image = Global.AccountsManagementForms.My.Resources.Resources.PDFIcon22x22
        Me.cmd_Generate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Generate.Location = New System.Drawing.Point(14, 74)
        Me.cmd_Generate.Name = "cmd_Generate"
        Me.cmd_Generate.Size = New System.Drawing.Size(186, 39)
        Me.cmd_Generate.TabIndex = 13
        Me.cmd_Generate.Text = "Generate"
        Me.cmd_Generate.UseVisualStyleBackColor = True
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
        Me.btn_ExportToText.Location = New System.Drawing.Point(14, 119)
        Me.btn_ExportToText.Name = "btn_ExportToText"
        Me.btn_ExportToText.Size = New System.Drawing.Size(186, 39)
        Me.btn_ExportToText.TabIndex = 61
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
        Me.btn_ExportToDat.Location = New System.Drawing.Point(206, 119)
        Me.btn_ExportToDat.Name = "btn_ExportToDat"
        Me.btn_ExportToDat.Size = New System.Drawing.Size(186, 39)
        Me.btn_ExportToDat.TabIndex = 63
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
        Me.btn_ExportToCSV.Location = New System.Drawing.Point(206, 74)
        Me.btn_ExportToCSV.Name = "btn_ExportToCSV"
        Me.btn_ExportToCSV.Size = New System.Drawing.Size(186, 39)
        Me.btn_ExportToCSV.TabIndex = 62
        Me.btn_ExportToCSV.Text = "Export to CSV"
        Me.btn_ExportToCSV.UseVisualStyleBackColor = False
        '
        'frmGLInterestPayablePrudential
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(400, 167)
        Me.Controls.Add(Me.btn_ExportToDat)
        Me.Controls.Add(Me.btn_ExportToCSV)
        Me.Controls.Add(Me.btn_ExportToText)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmd_Generate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmGLInterestPayablePrudential"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "General Ledger (Interest Payable Prudential)"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtp_To As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_From As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmd_Generate As System.Windows.Forms.Button
    Friend WithEvents btn_ExportToText As System.Windows.Forms.Button
    Friend WithEvents btn_ExportToDat As System.Windows.Forms.Button
    Friend WithEvents btn_ExportToCSV As System.Windows.Forms.Button
End Class
