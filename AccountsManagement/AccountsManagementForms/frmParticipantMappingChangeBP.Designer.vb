<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParticipantMappingChangeBP
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmd_cancel = New System.Windows.Forms.Button
        Me.cmd_save = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbo_ToBP = New System.Windows.Forms.ComboBox
        Me.cbo_fromBP = New System.Windows.Forms.ComboBox
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmd_cancel)
        Me.GroupBox1.Controls.Add(Me.cmd_save)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cbo_ToBP)
        Me.GroupBox1.Controls.Add(Me.cbo_fromBP)
        Me.GroupBox1.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(340, 115)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'cmd_cancel
        '
        Me.cmd_cancel.Image = Global.AccountsManagementForms.My.Resources.Resources.close
        Me.cmd_cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_cancel.Location = New System.Drawing.Point(229, 74)
        Me.cmd_cancel.Name = "cmd_cancel"
        Me.cmd_cancel.Size = New System.Drawing.Size(97, 35)
        Me.cmd_cancel.TabIndex = 6
        Me.cmd_cancel.Text = "Close"
        Me.cmd_cancel.UseVisualStyleBackColor = True
        '
        'cmd_save
        '
        Me.cmd_save.Image = Global.AccountsManagementForms.My.Resources.Resources.save
        Me.cmd_save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_save.Location = New System.Drawing.Point(114, 74)
        Me.cmd_save.Name = "cmd_save"
        Me.cmd_save.Size = New System.Drawing.Size(109, 35)
        Me.cmd_save.TabIndex = 5
        Me.cmd_save.Text = "Save"
        Me.cmd_save.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(165, 16)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Copy Mapping of Participants"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(220, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "To:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Billing Period From:"
        '
        'cbo_ToBP
        '
        Me.cbo_ToBP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_ToBP.FormattingEnabled = True
        Me.cbo_ToBP.Location = New System.Drawing.Point(251, 44)
        Me.cbo_ToBP.Name = "cbo_ToBP"
        Me.cbo_ToBP.Size = New System.Drawing.Size(75, 24)
        Me.cbo_ToBP.TabIndex = 1
        '
        'cbo_fromBP
        '
        Me.cbo_fromBP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_fromBP.FormattingEnabled = True
        Me.cbo_fromBP.Location = New System.Drawing.Point(128, 44)
        Me.cbo_fromBP.Name = "cbo_fromBP"
        Me.cbo_fromBP.Size = New System.Drawing.Size(75, 24)
        Me.cbo_fromBP.TabIndex = 0
        '
        'frmParticipantMappingChangeBP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(359, 138)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximumSize = New System.Drawing.Size(375, 176)
        Me.MinimumSize = New System.Drawing.Size(375, 176)
        Me.Name = "frmParticipantMappingChangeBP"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Billing Period"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbo_ToBP As System.Windows.Forms.ComboBox
    Friend WithEvents cbo_fromBP As System.Windows.Forms.ComboBox
    Friend WithEvents cmd_cancel As System.Windows.Forms.Button
    Friend WithEvents cmd_save As System.Windows.Forms.Button
End Class
