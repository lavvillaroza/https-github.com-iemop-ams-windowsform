<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWTAInstallmentDueDate
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btn_back = New System.Windows.Forms.Button()
        Me.btn_ok = New System.Windows.Forms.Button()
        Me.lblCollectionDate = New System.Windows.Forms.Label()
        Me.dtNewDueDate = New System.Windows.Forms.DateTimePicker()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btn_back)
        Me.Panel1.Controls.Add(Me.btn_ok)
        Me.Panel1.Controls.Add(Me.lblCollectionDate)
        Me.Panel1.Controls.Add(Me.dtNewDueDate)
        Me.Panel1.Location = New System.Drawing.Point(6, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(237, 76)
        Me.Panel1.TabIndex = 0
        '
        'btn_back
        '
        Me.btn_back.BackColor = System.Drawing.Color.White
        Me.btn_back.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_back.FlatAppearance.CheckedBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_back.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control
        Me.btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_back.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_back.Image = Global.AccountsManagementForms.My.Resources.Resources.BackRedIcon22x22
        Me.btn_back.Location = New System.Drawing.Point(189, 23)
        Me.btn_back.Name = "btn_back"
        Me.btn_back.Size = New System.Drawing.Size(35, 30)
        Me.btn_back.TabIndex = 63
        Me.btn_back.UseVisualStyleBackColor = False
        '
        'btn_ok
        '
        Me.btn_ok.BackColor = System.Drawing.Color.White
        Me.btn_ok.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_ok.FlatAppearance.CheckedBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_ok.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control
        Me.btn_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ok.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ok.Image = Global.AccountsManagementForms.My.Resources.Resources.OkIcon22x22
        Me.btn_ok.Location = New System.Drawing.Point(145, 23)
        Me.btn_ok.Name = "btn_ok"
        Me.btn_ok.Size = New System.Drawing.Size(35, 30)
        Me.btn_ok.TabIndex = 62
        Me.btn_ok.UseVisualStyleBackColor = False
        '
        'lblCollectionDate
        '
        Me.lblCollectionDate.AutoSize = True
        Me.lblCollectionDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCollectionDate.Location = New System.Drawing.Point(10, 8)
        Me.lblCollectionDate.Name = "lblCollectionDate"
        Me.lblCollectionDate.Size = New System.Drawing.Size(103, 15)
        Me.lblCollectionDate.TabIndex = 61
        Me.lblCollectionDate.Text = "New Due Date:"
        '
        'dtNewDueDate
        '
        Me.dtNewDueDate.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtNewDueDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtNewDueDate.Location = New System.Drawing.Point(13, 33)
        Me.dtNewDueDate.Name = "dtNewDueDate"
        Me.dtNewDueDate.Size = New System.Drawing.Size(121, 20)
        Me.dtNewDueDate.TabIndex = 60
        '
        'frmWTAInstallmentDueDate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(252, 93)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmWTAInstallmentDueDate"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WTA Installment DueDate"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents dtNewDueDate As DateTimePicker
    Friend WithEvents lblCollectionDate As Label
    Friend WithEvents btn_back As Button
    Friend WithEvents btn_ok As Button
End Class
