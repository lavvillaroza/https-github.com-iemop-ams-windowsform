<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChargeTypeSelection
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
        Me.rbEnergyMF = New System.Windows.Forms.RadioButton()
        Me.rbVAT = New System.Windows.Forms.RadioButton()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.rbMF = New System.Windows.Forms.RadioButton()
        Me.rbVATMF = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'rbEnergyMF
        '
        Me.rbEnergyMF.AutoSize = True
        Me.rbEnergyMF.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbEnergyMF.ForeColor = System.Drawing.Color.Black
        Me.rbEnergyMF.Location = New System.Drawing.Point(16, 29)
        Me.rbEnergyMF.Name = "rbEnergyMF"
        Me.rbEnergyMF.Size = New System.Drawing.Size(58, 16)
        Me.rbEnergyMF.TabIndex = 0
        Me.rbEnergyMF.TabStop = True
        Me.rbEnergyMF.Text = "Energy"
        Me.rbEnergyMF.UseVisualStyleBackColor = True
        '
        'rbVAT
        '
        Me.rbVAT.AutoSize = True
        Me.rbVAT.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbVAT.ForeColor = System.Drawing.Color.Black
        Me.rbVAT.Location = New System.Drawing.Point(16, 60)
        Me.rbVAT.Name = "rbVAT"
        Me.rbVAT.Size = New System.Drawing.Size(97, 16)
        Me.rbVAT.TabIndex = 1
        Me.rbVAT.TabStop = True
        Me.rbVAT.Text = "VAT on Energy"
        Me.rbVAT.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.Black
        Me.btnCancel.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIcon22x22
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(178, 113)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(120, 39)
        Me.btnCancel.TabIndex = 13
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnShow
        '
        Me.btnShow.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnShow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnShow.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShow.ForeColor = System.Drawing.Color.Black
        Me.btnShow.Image = Global.AccountsManagementForms.My.Resources.Resources.FileSearchIcon22x22
        Me.btnShow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnShow.Location = New System.Drawing.Point(52, 113)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(120, 39)
        Me.btnShow.TabIndex = 12
        Me.btnShow.Text = "&View"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'rbMF
        '
        Me.rbMF.AutoSize = True
        Me.rbMF.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbMF.ForeColor = System.Drawing.Color.Black
        Me.rbMF.Location = New System.Drawing.Point(140, 29)
        Me.rbMF.Name = "rbMF"
        Me.rbMF.Size = New System.Drawing.Size(85, 16)
        Me.rbMF.TabIndex = 2
        Me.rbMF.TabStop = True
        Me.rbMF.Text = "Market Fees"
        Me.rbMF.UseVisualStyleBackColor = True
        Me.rbMF.Visible = False
        '
        'rbVATMF
        '
        Me.rbVATMF.AutoSize = True
        Me.rbVATMF.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbVATMF.ForeColor = System.Drawing.Color.Black
        Me.rbVATMF.Location = New System.Drawing.Point(140, 58)
        Me.rbVATMF.Name = "rbVATMF"
        Me.rbVATMF.Size = New System.Drawing.Size(124, 16)
        Me.rbVATMF.TabIndex = 3
        Me.rbVATMF.TabStop = True
        Me.rbVATMF.Text = "VAT on Market Fees"
        Me.rbVATMF.UseVisualStyleBackColor = True
        Me.rbVATMF.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbEnergyMF)
        Me.GroupBox1.Controls.Add(Me.rbVATMF)
        Me.GroupBox1.Controls.Add(Me.rbVAT)
        Me.GroupBox1.Controls.Add(Me.rbMF)
        Me.GroupBox1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(286, 95)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Selection:"
        '
        'frmChargeTypeSelection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(308, 166)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnShow)
        Me.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmChargeTypeSelection"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Charge Type"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rbVAT As System.Windows.Forms.RadioButton
    Friend WithEvents rbEnergyMF As System.Windows.Forms.RadioButton
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents rbVATMF As System.Windows.Forms.RadioButton
    Friend WithEvents rbMF As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
