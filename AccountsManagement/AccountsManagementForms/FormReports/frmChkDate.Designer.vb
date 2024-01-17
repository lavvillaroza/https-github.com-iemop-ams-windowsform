<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChkDate
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
        Me.gb_DateSelection = New System.Windows.Forms.GroupBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtp_Date = New System.Windows.Forms.DateTimePicker()
        Me.gb_DateSelection.SuspendLayout()
        Me.SuspendLayout()
        '
        'gb_DateSelection
        '
        Me.gb_DateSelection.Controls.Add(Me.Button2)
        Me.gb_DateSelection.Controls.Add(Me.Button1)
        Me.gb_DateSelection.Controls.Add(Me.Label1)
        Me.gb_DateSelection.Controls.Add(Me.dtp_Date)
        Me.gb_DateSelection.Location = New System.Drawing.Point(12, 8)
        Me.gb_DateSelection.Name = "gb_DateSelection"
        Me.gb_DateSelection.Size = New System.Drawing.Size(223, 97)
        Me.gb_DateSelection.TabIndex = 0
        Me.gb_DateSelection.TabStop = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.White
        Me.Button2.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.Button2.FlatAppearance.CheckedBackColor = System.Drawing.Color.LemonChiffon
        Me.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Image = Global.AccountsManagementForms.My.Resources.Resources.BackRedIcon22x22
        Me.Button2.Location = New System.Drawing.Point(174, 53)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(35, 30)
        Me.Button2.TabIndex = 3
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.Button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.LemonChiffon
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = Global.AccountsManagementForms.My.Resources.Resources.OkIcon22x22
        Me.Button1.Location = New System.Drawing.Point(130, 53)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(35, 30)
        Me.Button1.TabIndex = 2
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Select Date:"
        '
        'dtp_Date
        '
        Me.dtp_Date.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_Date.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_Date.Location = New System.Drawing.Point(85, 26)
        Me.dtp_Date.Name = "dtp_Date"
        Me.dtp_Date.Size = New System.Drawing.Size(124, 21)
        Me.dtp_Date.TabIndex = 0
        '
        'frmChkDate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(248, 117)
        Me.Controls.Add(Me.gb_DateSelection)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmChkDate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Checks"
        Me.gb_DateSelection.ResumeLayout(False)
        Me.gb_DateSelection.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gb_DateSelection As System.Windows.Forms.GroupBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtp_Date As System.Windows.Forms.DateTimePicker
End Class
