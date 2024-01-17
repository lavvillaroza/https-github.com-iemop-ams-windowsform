<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSLAccountsPayable
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
        Me.dtTransaction = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtFullName = New System.Windows.Forms.TextBox()
        Me.ddlParticipantID = New System.Windows.Forms.ComboBox()
        Me.cmd_Close = New System.Windows.Forms.Button()
        Me.cmd_Generate = New System.Windows.Forms.Button()
        Me.btn_ExportToCSV = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dtTransaction)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(265, 56)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Transaction Date:"
        '
        'dtTransaction
        '
        Me.dtTransaction.CalendarFont = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtTransaction.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtTransaction.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTransaction.Location = New System.Drawing.Point(28, 20)
        Me.dtTransaction.Name = "dtTransaction"
        Me.dtTransaction.Size = New System.Drawing.Size(217, 20)
        Me.dtTransaction.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtFullName)
        Me.GroupBox2.Controls.Add(Me.ddlParticipantID)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(12, 68)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(259, 108)
        Me.GroupBox2.TabIndex = 16
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Participant ID:"
        '
        'txtFullName
        '
        Me.txtFullName.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFullName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFullName.Location = New System.Drawing.Point(22, 49)
        Me.txtFullName.Multiline = True
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.ReadOnly = True
        Me.txtFullName.Size = New System.Drawing.Size(217, 53)
        Me.txtFullName.TabIndex = 1
        '
        'ddlParticipantID
        '
        Me.ddlParticipantID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlParticipantID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlParticipantID.FormattingEnabled = True
        Me.ddlParticipantID.Location = New System.Drawing.Point(22, 20)
        Me.ddlParticipantID.Name = "ddlParticipantID"
        Me.ddlParticipantID.Size = New System.Drawing.Size(217, 22)
        Me.ddlParticipantID.TabIndex = 0
        '
        'cmd_Close
        '
        Me.cmd_Close.BackColor = System.Drawing.Color.White
        Me.cmd_Close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Close.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Close.Location = New System.Drawing.Point(277, 102)
        Me.cmd_Close.Name = "cmd_Close"
        Me.cmd_Close.Size = New System.Drawing.Size(145, 39)
        Me.cmd_Close.TabIndex = 14
        Me.cmd_Close.Text = "Close"
        Me.cmd_Close.UseVisualStyleBackColor = False
        '
        'cmd_Generate
        '
        Me.cmd_Generate.BackColor = System.Drawing.Color.White
        Me.cmd_Generate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Generate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Generate.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Generate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Generate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Generate.Image = Global.AccountsManagementForms.My.Resources.Resources.PDFIcon22x22
        Me.cmd_Generate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Generate.Location = New System.Drawing.Point(277, 12)
        Me.cmd_Generate.Name = "cmd_Generate"
        Me.cmd_Generate.Size = New System.Drawing.Size(145, 39)
        Me.cmd_Generate.TabIndex = 13
        Me.cmd_Generate.Text = "Generate"
        Me.cmd_Generate.UseVisualStyleBackColor = False
        '
        'btn_ExportToCSV
        '
        Me.btn_ExportToCSV.BackColor = System.Drawing.Color.White
        Me.btn_ExportToCSV.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_ExportToCSV.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_ExportToCSV.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_ExportToCSV.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ExportToCSV.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ExportToCSV.ForeColor = System.Drawing.Color.Black
        Me.btn_ExportToCSV.Image = Global.AccountsManagementForms.My.Resources.Resources.CSVIconColored22x22
        Me.btn_ExportToCSV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_ExportToCSV.Location = New System.Drawing.Point(277, 57)
        Me.btn_ExportToCSV.Name = "btn_ExportToCSV"
        Me.btn_ExportToCSV.Size = New System.Drawing.Size(145, 39)
        Me.btn_ExportToCSV.TabIndex = 65
        Me.btn_ExportToCSV.Text = "Export to CSV"
        Me.btn_ExportToCSV.UseVisualStyleBackColor = False
        '
        'frmSLAccountsPayable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(435, 188)
        Me.Controls.Add(Me.btn_ExportToCSV)
        Me.Controls.Add(Me.cmd_Close)
        Me.Controls.Add(Me.cmd_Generate)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmSLAccountsPayable"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Subsidiary Ledger - Accounts Payable"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmd_Close As System.Windows.Forms.Button
    Friend WithEvents cmd_Generate As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtTransaction As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtFullName As System.Windows.Forms.TextBox
    Friend WithEvents ddlParticipantID As System.Windows.Forms.ComboBox
    Friend WithEvents btn_ExportToCSV As System.Windows.Forms.Button
End Class
