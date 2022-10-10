<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCheckDates
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
        Me.grp_SelectDate = New System.Windows.Forms.GroupBox()
        Me.cmd_Cancel = New System.Windows.Forms.Button()
        Me.cmd_OK = New System.Windows.Forms.Button()
        Me.dtp_DateUpdate = New System.Windows.Forms.DateTimePicker()
        Me.grp_SelectDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'grp_SelectDate
        '
        Me.grp_SelectDate.Controls.Add(Me.cmd_Cancel)
        Me.grp_SelectDate.Controls.Add(Me.cmd_OK)
        Me.grp_SelectDate.Controls.Add(Me.dtp_DateUpdate)
        Me.grp_SelectDate.Location = New System.Drawing.Point(5, 12)
        Me.grp_SelectDate.Name = "grp_SelectDate"
        Me.grp_SelectDate.Size = New System.Drawing.Size(223, 89)
        Me.grp_SelectDate.TabIndex = 0
        Me.grp_SelectDate.TabStop = False
        '
        'cmd_Cancel
        '
        Me.cmd_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Cancel.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Cancel.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_Cancel.Location = New System.Drawing.Point(172, 45)
        Me.cmd_Cancel.Name = "cmd_Cancel"
        Me.cmd_Cancel.Size = New System.Drawing.Size(35, 30)
        Me.cmd_Cancel.TabIndex = 2
        Me.cmd_Cancel.UseVisualStyleBackColor = True
        '
        'cmd_OK
        '
        Me.cmd_OK.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_OK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_OK.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_OK.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_OK.Image = Global.AccountsManagementForms.My.Resources.Resources.OkIcon22x22
        Me.cmd_OK.Location = New System.Drawing.Point(131, 45)
        Me.cmd_OK.Name = "cmd_OK"
        Me.cmd_OK.Size = New System.Drawing.Size(35, 30)
        Me.cmd_OK.TabIndex = 1
        Me.cmd_OK.UseVisualStyleBackColor = True
        '
        'dtp_DateUpdate
        '
        Me.dtp_DateUpdate.Location = New System.Drawing.Point(7, 19)
        Me.dtp_DateUpdate.Name = "dtp_DateUpdate"
        Me.dtp_DateUpdate.Size = New System.Drawing.Size(200, 20)
        Me.dtp_DateUpdate.TabIndex = 0
        '
        'frmCheckDates
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(233, 108)
        Me.Controls.Add(Me.grp_SelectDate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmCheckDates"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Select Date"
        Me.grp_SelectDate.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grp_SelectDate As System.Windows.Forms.GroupBox
    Friend WithEvents cmd_Cancel As System.Windows.Forms.Button
    Friend WithEvents cmd_OK As System.Windows.Forms.Button
    Friend WithEvents dtp_DateUpdate As System.Windows.Forms.DateTimePicker
End Class
