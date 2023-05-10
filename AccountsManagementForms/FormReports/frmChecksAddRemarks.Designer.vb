<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChecksAddRemarks
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
        Me.txt_Remarks = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmd_Ok = New System.Windows.Forms.Button()
        Me.cmd_Cancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txt_Remarks
        '
        Me.txt_Remarks.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Remarks.Location = New System.Drawing.Point(12, 25)
        Me.txt_Remarks.MaxLength = 150
        Me.txt_Remarks.Multiline = True
        Me.txt_Remarks.Name = "txt_Remarks"
        Me.txt_Remarks.Size = New System.Drawing.Size(331, 66)
        Me.txt_Remarks.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Remarks:"
        '
        'cmd_Ok
        '
        Me.cmd_Ok.BackColor = System.Drawing.Color.White
        Me.cmd_Ok.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Ok.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Ok.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Ok.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.cmd_Ok.Image = Global.AccountsManagementForms.My.Resources.Resources.OkIcon22x22
        Me.cmd_Ok.Location = New System.Drawing.Point(267, 96)
        Me.cmd_Ok.Name = "cmd_Ok"
        Me.cmd_Ok.Size = New System.Drawing.Size(35, 30)
        Me.cmd_Ok.TabIndex = 2
        Me.cmd_Ok.UseVisualStyleBackColor = False
        '
        'cmd_Cancel
        '
        Me.cmd_Cancel.BackColor = System.Drawing.Color.White
        Me.cmd_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Cancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.cmd_Cancel.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_Cancel.Location = New System.Drawing.Point(308, 96)
        Me.cmd_Cancel.Name = "cmd_Cancel"
        Me.cmd_Cancel.Size = New System.Drawing.Size(35, 30)
        Me.cmd_Cancel.TabIndex = 3
        Me.cmd_Cancel.UseVisualStyleBackColor = False
        '
        'frmChecksAddRemarks
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(350, 145)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmd_Cancel)
        Me.Controls.Add(Me.cmd_Ok)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_Remarks)
        Me.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmChecksAddRemarks"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add Remarks to Check"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txt_Remarks As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmd_Ok As System.Windows.Forms.Button
    Friend WithEvents cmd_Cancel As System.Windows.Forms.Button
End Class
