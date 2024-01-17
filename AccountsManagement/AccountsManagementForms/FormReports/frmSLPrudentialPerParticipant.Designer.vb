<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSLPrudentialPerParticipant
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
        Me.dtp_To = New System.Windows.Forms.DateTimePicker()
        Me.dtp_From = New System.Windows.Forms.DateTimePicker()
        Me.cmd_Close = New System.Windows.Forms.Button()
        Me.cmd_Generate = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.ddlParticipantID = New System.Windows.Forms.ComboBox()
        Me.txtIDNumber = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btn_ExportToCSV = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtp_To)
        Me.GroupBox1.Controls.Add(Me.dtp_From)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 157)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(313, 75)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Choose Date:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(50, 46)
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
        Me.Label1.Location = New System.Drawing.Point(33, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "From:"
        '
        'dtp_To
        '
        Me.dtp_To.CalendarFont = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_To.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_To.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_To.Location = New System.Drawing.Point(81, 49)
        Me.dtp_To.Name = "dtp_To"
        Me.dtp_To.Size = New System.Drawing.Size(139, 20)
        Me.dtp_To.TabIndex = 1
        '
        'dtp_From
        '
        Me.dtp_From.CalendarFont = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_From.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_From.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_From.Location = New System.Drawing.Point(81, 21)
        Me.dtp_From.Name = "dtp_From"
        Me.dtp_From.Size = New System.Drawing.Size(139, 20)
        Me.dtp_From.TabIndex = 0
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
        Me.cmd_Close.Location = New System.Drawing.Point(331, 120)
        Me.cmd_Close.Name = "cmd_Close"
        Me.cmd_Close.Size = New System.Drawing.Size(145, 39)
        Me.cmd_Close.TabIndex = 9
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
        Me.cmd_Generate.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.cmd_Generate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Generate.Location = New System.Drawing.Point(331, 30)
        Me.cmd_Generate.Name = "cmd_Generate"
        Me.cmd_Generate.Size = New System.Drawing.Size(145, 39)
        Me.cmd_Generate.TabIndex = 8
        Me.cmd_Generate.Text = "Generate"
        Me.cmd_Generate.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtName)
        Me.GroupBox2.Controls.Add(Me.ddlParticipantID)
        Me.GroupBox2.Controls.Add(Me.txtIDNumber)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(313, 139)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Choose Participant:"
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.Color.Black
        Me.txtName.Location = New System.Drawing.Point(116, 71)
        Me.txtName.Multiline = True
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(191, 62)
        Me.txtName.TabIndex = 9
        '
        'ddlParticipantID
        '
        Me.ddlParticipantID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlParticipantID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlParticipantID.ForeColor = System.Drawing.Color.Black
        Me.ddlParticipantID.FormattingEnabled = True
        Me.ddlParticipantID.Location = New System.Drawing.Point(114, 43)
        Me.ddlParticipantID.Name = "ddlParticipantID"
        Me.ddlParticipantID.Size = New System.Drawing.Size(190, 22)
        Me.ddlParticipantID.TabIndex = 8
        '
        'txtIDNumber
        '
        Me.txtIDNumber.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtIDNumber.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIDNumber.ForeColor = System.Drawing.Color.Black
        Me.txtIDNumber.Location = New System.Drawing.Point(114, 18)
        Me.txtIDNumber.Name = "txtIDNumber"
        Me.txtIDNumber.ReadOnly = True
        Me.txtIDNumber.Size = New System.Drawing.Size(191, 20)
        Me.txtIDNumber.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(68, 75)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 14)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Name:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(25, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 14)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Participant ID:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(41, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 14)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "ID Number:"
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
        Me.btn_ExportToCSV.Location = New System.Drawing.Point(331, 75)
        Me.btn_ExportToCSV.Name = "btn_ExportToCSV"
        Me.btn_ExportToCSV.Size = New System.Drawing.Size(145, 39)
        Me.btn_ExportToCSV.TabIndex = 66
        Me.btn_ExportToCSV.Text = "Export to CSV"
        Me.btn_ExportToCSV.UseVisualStyleBackColor = False
        '
        'frmSLPrudentialPerParticipant
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(486, 238)
        Me.Controls.Add(Me.btn_ExportToCSV)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmd_Close)
        Me.Controls.Add(Me.cmd_Generate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmSLPrudentialPerParticipant"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Subsidiary Ledger (Prudential - Per Participant)"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtp_To As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_From As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmd_Close As System.Windows.Forms.Button
    Friend WithEvents cmd_Generate As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents ddlParticipantID As System.Windows.Forms.ComboBox
    Friend WithEvents txtIDNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btn_ExportToCSV As System.Windows.Forms.Button
End Class
