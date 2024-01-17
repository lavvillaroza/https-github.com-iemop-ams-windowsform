<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBillingCalendarMgt
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
        Me.txtMonthFrom = New System.Windows.Forms.TextBox()
        Me.txtYearFrom = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ddlMonthTo = New System.Windows.Forms.ComboBox()
        Me.ddlYearTo = New System.Windows.Forms.ComboBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtMonthFrom)
        Me.GroupBox1.Controls.Add(Me.txtYearFrom)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(260, 44)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "From:"
        '
        'txtMonthFrom
        '
        Me.txtMonthFrom.BackColor = System.Drawing.Color.White
        Me.txtMonthFrom.Location = New System.Drawing.Point(93, 17)
        Me.txtMonthFrom.Name = "txtMonthFrom"
        Me.txtMonthFrom.ReadOnly = True
        Me.txtMonthFrom.Size = New System.Drawing.Size(159, 20)
        Me.txtMonthFrom.TabIndex = 1
        '
        'txtYearFrom
        '
        Me.txtYearFrom.BackColor = System.Drawing.Color.White
        Me.txtYearFrom.Location = New System.Drawing.Point(6, 17)
        Me.txtYearFrom.Name = "txtYearFrom"
        Me.txtYearFrom.ReadOnly = True
        Me.txtYearFrom.Size = New System.Drawing.Size(81, 20)
        Me.txtYearFrom.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ddlMonthTo)
        Me.GroupBox2.Controls.Add(Me.ddlYearTo)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(12, 59)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(260, 44)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "To:"
        '
        'ddlMonthTo
        '
        Me.ddlMonthTo.FormattingEnabled = True
        Me.ddlMonthTo.Location = New System.Drawing.Point(93, 17)
        Me.ddlMonthTo.Name = "ddlMonthTo"
        Me.ddlMonthTo.Size = New System.Drawing.Size(159, 22)
        Me.ddlMonthTo.TabIndex = 1
        '
        'ddlYearTo
        '
        Me.ddlYearTo.FormattingEnabled = True
        Me.ddlYearTo.Location = New System.Drawing.Point(6, 17)
        Me.ddlYearTo.Name = "ddlYearTo"
        Me.ddlYearTo.Size = New System.Drawing.Size(81, 22)
        Me.ddlYearTo.TabIndex = 0
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.White
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnCancel.FlatAppearance.CheckedBackColor = System.Drawing.Color.LemonChiffon
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCancel.Image = Global.AccountsManagementForms.My.Resources.Resources.BackRedIcon22x22
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(231, 114)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(41, 36)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.White
        Me.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSave.FlatAppearance.CheckedBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSave.Image = Global.AccountsManagementForms.My.Resources.Resources.SaveIconColored22x22
        Me.btnSave.Location = New System.Drawing.Point(184, 114)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(41, 36)
        Me.btnSave.TabIndex = 3
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'frmBillingCalendarMgt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(284, 162)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmBillingCalendarMgt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Billing Calendar Period Management"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtMonthFrom As System.Windows.Forms.TextBox
    Friend WithEvents txtYearFrom As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ddlMonthTo As System.Windows.Forms.ComboBox
    Friend WithEvents ddlYearTo As System.Windows.Forms.ComboBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
