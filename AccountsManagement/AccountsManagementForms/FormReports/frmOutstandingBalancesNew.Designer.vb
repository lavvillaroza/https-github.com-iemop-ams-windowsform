<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOutstandingBalancesNew
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
        Me.cmd_GenerateReportToExcel = New System.Windows.Forms.Button()
        Me.cmd_close = New System.Windows.Forms.Button()
        Me.cmd_browse = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_DestFile = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cbSelectAll = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ddl_DueDateList = New System.Windows.Forms.ComboBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmd_GenerateReportToExcel
        '
        Me.cmd_GenerateReportToExcel.BackColor = System.Drawing.Color.White
        Me.cmd_GenerateReportToExcel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_GenerateReportToExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_GenerateReportToExcel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_GenerateReportToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_GenerateReportToExcel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_GenerateReportToExcel.Image = Global.AccountsManagementForms.My.Resources.Resources.ExcelIcon22x22
        Me.cmd_GenerateReportToExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_GenerateReportToExcel.Location = New System.Drawing.Point(14, 112)
        Me.cmd_GenerateReportToExcel.Name = "cmd_GenerateReportToExcel"
        Me.cmd_GenerateReportToExcel.Size = New System.Drawing.Size(150, 39)
        Me.cmd_GenerateReportToExcel.TabIndex = 19
        Me.cmd_GenerateReportToExcel.Text = " Export To Excel"
        Me.cmd_GenerateReportToExcel.UseVisualStyleBackColor = False
        '
        'cmd_close
        '
        Me.cmd_close.BackColor = System.Drawing.Color.White
        Me.cmd_close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_close.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_close.Location = New System.Drawing.Point(170, 112)
        Me.cmd_close.Name = "cmd_close"
        Me.cmd_close.Size = New System.Drawing.Size(150, 39)
        Me.cmd_close.TabIndex = 18
        Me.cmd_close.Text = "Close"
        Me.cmd_close.UseVisualStyleBackColor = False
        '
        'cmd_browse
        '
        Me.cmd_browse.BackColor = System.Drawing.Color.White
        Me.cmd_browse.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_browse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_browse.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_browse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_browse.Image = Global.AccountsManagementForms.My.Resources.Resources.SearchIconColored22x22
        Me.cmd_browse.Location = New System.Drawing.Point(282, 82)
        Me.cmd_browse.Name = "cmd_browse"
        Me.cmd_browse.Size = New System.Drawing.Size(38, 24)
        Me.cmd_browse.TabIndex = 21
        Me.cmd_browse.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 14)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Destination Folder:"
        '
        'txt_DestFile
        '
        Me.txt_DestFile.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_DestFile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_DestFile.Location = New System.Drawing.Point(14, 82)
        Me.txt_DestFile.Name = "txt_DestFile"
        Me.txt_DestFile.ReadOnly = True
        Me.txt_DestFile.Size = New System.Drawing.Size(262, 20)
        Me.txt_DestFile.TabIndex = 20
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cbSelectAll)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.ddl_DueDateList)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmd_browse)
        Me.GroupBox1.Controls.Add(Me.cmd_close)
        Me.GroupBox1.Controls.Add(Me.cmd_GenerateReportToExcel)
        Me.GroupBox1.Controls.Add(Me.txt_DestFile)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(336, 164)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        '
        'cbSelectAll
        '
        Me.cbSelectAll.AutoSize = True
        Me.cbSelectAll.Location = New System.Drawing.Point(206, 35)
        Me.cbSelectAll.Name = "cbSelectAll"
        Me.cbSelectAll.Size = New System.Drawing.Size(70, 17)
        Me.cbSelectAll.TabIndex = 25
        Me.cbSelectAll.Text = "Select All"
        Me.cbSelectAll.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 14)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "Due Date :"
        '
        'ddl_DueDateList
        '
        Me.ddl_DueDateList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddl_DueDateList.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddl_DueDateList.FormattingEnabled = True
        Me.ddl_DueDateList.Location = New System.Drawing.Point(14, 33)
        Me.ddl_DueDateList.Name = "ddl_DueDateList"
        Me.ddl_DueDateList.Size = New System.Drawing.Size(186, 22)
        Me.ddl_DueDateList.TabIndex = 23
        '
        'frmOutstandingBalancesNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(359, 188)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmOutstandingBalancesNew"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Summary of Outstanding Balances"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmd_GenerateReportToExcel As System.Windows.Forms.Button
    Friend WithEvents cmd_close As System.Windows.Forms.Button
    Friend WithEvents cmd_browse As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_DestFile As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ddl_DueDateList As System.Windows.Forms.ComboBox
    Friend WithEvents cbSelectAll As System.Windows.Forms.CheckBox
End Class
