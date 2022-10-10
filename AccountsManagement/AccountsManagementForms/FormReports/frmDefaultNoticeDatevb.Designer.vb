<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDefaultNoticeDatevb
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
        Me.dtp_PaymentDate = New System.Windows.Forms.DateTimePicker()
        Me.btn_Ok = New System.Windows.Forms.Button()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'dtp_PaymentDate
        '
        Me.dtp_PaymentDate.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_PaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_PaymentDate.Location = New System.Drawing.Point(140, 24)
        Me.dtp_PaymentDate.Name = "dtp_PaymentDate"
        Me.dtp_PaymentDate.Size = New System.Drawing.Size(115, 20)
        Me.dtp_PaymentDate.TabIndex = 32
        '
        'btn_Ok
        '
        Me.btn_Ok.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Ok.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Ok.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Ok.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Ok.Image = Global.AccountsManagementForms.My.Resources.Resources.OkIcon22x22
        Me.btn_Ok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Ok.Location = New System.Drawing.Point(55, 50)
        Me.btn_Ok.Name = "btn_Ok"
        Me.btn_Ok.Size = New System.Drawing.Size(97, 30)
        Me.btn_Ok.TabIndex = 33
        Me.btn_Ok.Text = "Ok"
        Me.btn_Ok.UseVisualStyleBackColor = True
        '
        'btn_Cancel
        '
        Me.btn_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Cancel.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Cancel.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIcon22x22
        Me.btn_Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Cancel.Location = New System.Drawing.Point(158, 50)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(97, 30)
        Me.btn_Cancel.TabIndex = 34
        Me.btn_Cancel.Text = "Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(12, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(122, 15)
        Me.Label7.TabIndex = 31
        Me.Label7.Text = "Default Notice Date:"
        '
        'frmDefaultNoticeDatevb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(274, 96)
        Me.Controls.Add(Me.btn_Cancel)
        Me.Controls.Add(Me.btn_Ok)
        Me.Controls.Add(Me.dtp_PaymentDate)
        Me.Controls.Add(Me.Label7)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDefaultNoticeDatevb"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Default Notice Date"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtp_PaymentDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btn_Ok As System.Windows.Forms.Button
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
