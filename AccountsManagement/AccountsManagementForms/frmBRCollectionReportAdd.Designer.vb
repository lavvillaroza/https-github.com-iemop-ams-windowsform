<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBRCollectionReportAdd
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(112, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(21, 14)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "To:"
        '
        'btnOk
        '
        Me.btnOk.AccessibleName = ""
        Me.btnOk.BackColor = System.Drawing.SystemColors.Control
        Me.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOk.Image = Global.AccountsManagementForms.My.Resources.Resources.OkIcon22x22
        Me.btnOk.Location = New System.Drawing.Point(226, 15)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(35, 30)
        Me.btnOk.TabIndex = 19
        Me.btnOk.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(2, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 14)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "From:"
        '
        'btnClose
        '
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseRedIcon22x22
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(267, 15)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(35, 30)
        Me.btnClose.TabIndex = 20
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'dtFrom
        '
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFrom.Location = New System.Drawing.Point(5, 25)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(104, 20)
        Me.dtFrom.TabIndex = 21
        '
        'dtTo
        '
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTo.Location = New System.Drawing.Point(115, 25)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(104, 20)
        Me.dtTo.TabIndex = 22
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnOk)
        Me.Panel1.Controls.Add(Me.dtFrom)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.dtTo)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(7, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(314, 60)
        Me.Panel1.TabIndex = 23
        '
        'frmBRCollectionReportAdd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(328, 71)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBRCollectionReportAdd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generate New Collection Report"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label3 As Label
    Friend WithEvents btnOk As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents btnClose As Button
    Friend WithEvents dtFrom As DateTimePicker
    Friend WithEvents dtTo As DateTimePicker
    Friend WithEvents Panel1 As Panel
End Class
