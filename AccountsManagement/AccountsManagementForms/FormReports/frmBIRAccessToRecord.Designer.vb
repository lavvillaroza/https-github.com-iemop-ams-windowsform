<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBIRAccessToRecord
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
        Me.ddlYear_cmb = New System.Windows.Forms.ComboBox()
        Me.cmd_Close = New System.Windows.Forms.Button()
        Me.gbox_Participants = New System.Windows.Forms.GroupBox()
        Me.chkLB_Participants = New System.Windows.Forms.CheckedListBox()
        Me.chkbox_SelectAll = New System.Windows.Forms.CheckBox()
        Me.btn_ExportToExcel = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.gbox_Participants.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ddlYear_cmb)
        Me.GroupBox1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(214, 50)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Transaction Year (As Of) :"
        '
        'ddlYear_cmb
        '
        Me.ddlYear_cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlYear_cmb.FormattingEnabled = True
        Me.ddlYear_cmb.Location = New System.Drawing.Point(6, 19)
        Me.ddlYear_cmb.Name = "ddlYear_cmb"
        Me.ddlYear_cmb.Size = New System.Drawing.Size(202, 20)
        Me.ddlYear_cmb.TabIndex = 0
        '
        'cmd_Close
        '
        Me.cmd_Close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Close.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Close.Location = New System.Drawing.Point(12, 119)
        Me.cmd_Close.Name = "cmd_Close"
        Me.cmd_Close.Size = New System.Drawing.Size(214, 45)
        Me.cmd_Close.TabIndex = 8
        Me.cmd_Close.Text = "Close"
        Me.cmd_Close.UseVisualStyleBackColor = True
        '
        'gbox_Participants
        '
        Me.gbox_Participants.Controls.Add(Me.chkLB_Participants)
        Me.gbox_Participants.Controls.Add(Me.chkbox_SelectAll)
        Me.gbox_Participants.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbox_Participants.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.gbox_Participants.Location = New System.Drawing.Point(232, 12)
        Me.gbox_Participants.Name = "gbox_Participants"
        Me.gbox_Participants.Size = New System.Drawing.Size(214, 171)
        Me.gbox_Participants.TabIndex = 9
        Me.gbox_Participants.TabStop = False
        Me.gbox_Participants.Text = "Participant/s:"
        '
        'chkLB_Participants
        '
        Me.chkLB_Participants.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLB_Participants.FormattingEnabled = True
        Me.chkLB_Participants.Location = New System.Drawing.Point(5, 38)
        Me.chkLB_Participants.Name = "chkLB_Participants"
        Me.chkLB_Participants.Size = New System.Drawing.Size(203, 124)
        Me.chkLB_Participants.TabIndex = 2
        '
        'chkbox_SelectAll
        '
        Me.chkbox_SelectAll.AutoSize = True
        Me.chkbox_SelectAll.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbox_SelectAll.ForeColor = System.Drawing.Color.Black
        Me.chkbox_SelectAll.Location = New System.Drawing.Point(8, 17)
        Me.chkbox_SelectAll.Name = "chkbox_SelectAll"
        Me.chkbox_SelectAll.Size = New System.Drawing.Size(68, 16)
        Me.chkbox_SelectAll.TabIndex = 1
        Me.chkbox_SelectAll.Text = "Select All"
        Me.chkbox_SelectAll.UseVisualStyleBackColor = True
        '
        'btn_ExportToExcel
        '
        Me.btn_ExportToExcel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_ExportToExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_ExportToExcel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_ExportToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ExportToExcel.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ExportToExcel.Image = Global.AccountsManagementForms.My.Resources.Resources.ExcelIcon22x22
        Me.btn_ExportToExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_ExportToExcel.Location = New System.Drawing.Point(12, 68)
        Me.btn_ExportToExcel.Name = "btn_ExportToExcel"
        Me.btn_ExportToExcel.Size = New System.Drawing.Size(214, 45)
        Me.btn_ExportToExcel.TabIndex = 10
        Me.btn_ExportToExcel.Text = "Export to Excel"
        Me.btn_ExportToExcel.UseVisualStyleBackColor = True
        '
        'frmBIRAccessToRecord
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(459, 189)
        Me.Controls.Add(Me.btn_ExportToExcel)
        Me.Controls.Add(Me.gbox_Participants)
        Me.Controls.Add(Me.cmd_Close)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmBIRAccessToRecord"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BIR Access To Record"
        Me.GroupBox1.ResumeLayout(False)
        Me.gbox_Participants.ResumeLayout(False)
        Me.gbox_Participants.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmd_Close As System.Windows.Forms.Button
    Friend WithEvents ddlYear_cmb As System.Windows.Forms.ComboBox
    Friend WithEvents gbox_Participants As System.Windows.Forms.GroupBox
    Friend WithEvents chkLB_Participants As System.Windows.Forms.CheckedListBox
    Friend WithEvents chkbox_SelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents btn_ExportToExcel As System.Windows.Forms.Button
End Class
