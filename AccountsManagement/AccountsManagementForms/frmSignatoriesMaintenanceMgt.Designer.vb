<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSignatoriesMaintenanceMgt
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
        Me.CMD_Ok = New System.Windows.Forms.Button()
        Me.CMD_Cancel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TXT_description = New System.Windows.Forms.TextBox()
        Me.LBL_DocumentContainer = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_p3 = New System.Windows.Forms.TextBox()
        Me.txt_s3 = New System.Windows.Forms.TextBox()
        Me.txt_p2 = New System.Windows.Forms.TextBox()
        Me.txt_s2 = New System.Windows.Forms.TextBox()
        Me.txt_p1 = New System.Windows.Forms.TextBox()
        Me.txt_s1 = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'CMD_Ok
        '
        Me.CMD_Ok.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.CMD_Ok.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.CMD_Ok.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.CMD_Ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CMD_Ok.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMD_Ok.ForeColor = System.Drawing.Color.Black
        Me.CMD_Ok.Image = Global.AccountsManagementForms.My.Resources.Resources.SaveIconColored22x22
        Me.CMD_Ok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CMD_Ok.Location = New System.Drawing.Point(471, 198)
        Me.CMD_Ok.Name = "CMD_Ok"
        Me.CMD_Ok.Size = New System.Drawing.Size(110, 36)
        Me.CMD_Ok.TabIndex = 0
        Me.CMD_Ok.Text = "&Save"
        Me.CMD_Ok.UseVisualStyleBackColor = True
        '
        'CMD_Cancel
        '
        Me.CMD_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.CMD_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.CMD_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.CMD_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CMD_Cancel.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMD_Cancel.ForeColor = System.Drawing.Color.Black
        Me.CMD_Cancel.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.CMD_Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CMD_Cancel.Location = New System.Drawing.Point(587, 198)
        Me.CMD_Cancel.Name = "CMD_Cancel"
        Me.CMD_Cancel.Size = New System.Drawing.Size(110, 36)
        Me.CMD_Cancel.TabIndex = 1
        Me.CMD_Cancel.Text = "&Cancel"
        Me.CMD_Cancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(9, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Document Code:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(9, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(130, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Document Description:"
        '
        'TXT_description
        '
        Me.TXT_description.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXT_description.ForeColor = System.Drawing.Color.Black
        Me.TXT_description.Location = New System.Drawing.Point(12, 45)
        Me.TXT_description.MaxLength = 200
        Me.TXT_description.Multiline = True
        Me.TXT_description.Name = "TXT_description"
        Me.TXT_description.Size = New System.Drawing.Size(218, 165)
        Me.TXT_description.TabIndex = 10
        '
        'LBL_DocumentContainer
        '
        Me.LBL_DocumentContainer.AutoSize = True
        Me.LBL_DocumentContainer.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL_DocumentContainer.ForeColor = System.Drawing.Color.Black
        Me.LBL_DocumentContainer.Location = New System.Drawing.Point(112, 7)
        Me.LBL_DocumentContainer.Name = "LBL_DocumentContainer"
        Me.LBL_DocumentContainer.Size = New System.Drawing.Size(37, 12)
        Me.LBL_DocumentContainer.TabIndex = 11
        Me.LBL_DocumentContainer.Text = "Label6"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(254, 165)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(55, 14)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Position:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(254, 141)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(71, 14)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Signatory 3:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(254, 117)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(55, 14)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Position:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(254, 93)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(71, 14)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Signatory 2:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(254, 69)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 14)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Position:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(254, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 14)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Signatory 1:"
        '
        'txt_p3
        '
        Me.txt_p3.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_p3.ForeColor = System.Drawing.Color.Black
        Me.txt_p3.Location = New System.Drawing.Point(331, 165)
        Me.txt_p3.MaxLength = 100
        Me.txt_p3.Name = "txt_p3"
        Me.txt_p3.Size = New System.Drawing.Size(369, 20)
        Me.txt_p3.TabIndex = 14
        '
        'txt_s3
        '
        Me.txt_s3.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_s3.ForeColor = System.Drawing.Color.Black
        Me.txt_s3.Location = New System.Drawing.Point(331, 141)
        Me.txt_s3.MaxLength = 100
        Me.txt_s3.Name = "txt_s3"
        Me.txt_s3.Size = New System.Drawing.Size(369, 20)
        Me.txt_s3.TabIndex = 13
        '
        'txt_p2
        '
        Me.txt_p2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_p2.ForeColor = System.Drawing.Color.Black
        Me.txt_p2.Location = New System.Drawing.Point(331, 117)
        Me.txt_p2.MaxLength = 100
        Me.txt_p2.Name = "txt_p2"
        Me.txt_p2.Size = New System.Drawing.Size(369, 20)
        Me.txt_p2.TabIndex = 12
        '
        'txt_s2
        '
        Me.txt_s2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_s2.ForeColor = System.Drawing.Color.Black
        Me.txt_s2.Location = New System.Drawing.Point(331, 93)
        Me.txt_s2.MaxLength = 100
        Me.txt_s2.Name = "txt_s2"
        Me.txt_s2.Size = New System.Drawing.Size(369, 20)
        Me.txt_s2.TabIndex = 11
        '
        'txt_p1
        '
        Me.txt_p1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_p1.ForeColor = System.Drawing.Color.Black
        Me.txt_p1.Location = New System.Drawing.Point(331, 69)
        Me.txt_p1.MaxLength = 100
        Me.txt_p1.Name = "txt_p1"
        Me.txt_p1.Size = New System.Drawing.Size(369, 20)
        Me.txt_p1.TabIndex = 10
        '
        'txt_s1
        '
        Me.txt_s1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_s1.ForeColor = System.Drawing.Color.Black
        Me.txt_s1.Location = New System.Drawing.Point(331, 45)
        Me.txt_s1.MaxLength = 100
        Me.txt_s1.Name = "txt_s1"
        Me.txt_s1.Size = New System.Drawing.Size(369, 20)
        Me.txt_s1.TabIndex = 9
        '
        'frmSignatoriesMaintenanceMgt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(712, 254)
        Me.ControlBox = False
        Me.Controls.Add(Me.txt_p3)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txt_s3)
        Me.Controls.Add(Me.txt_p2)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txt_s2)
        Me.Controls.Add(Me.LBL_DocumentContainer)
        Me.Controls.Add(Me.txt_p1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txt_s1)
        Me.Controls.Add(Me.TXT_description)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CMD_Cancel)
        Me.Controls.Add(Me.CMD_Ok)
        Me.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSignatoriesMaintenanceMgt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Document Signatories Maintenance"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CMD_Ok As System.Windows.Forms.Button
    Friend WithEvents CMD_Cancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TXT_description As System.Windows.Forms.TextBox
    Friend WithEvents LBL_DocumentContainer As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt_p3 As System.Windows.Forms.TextBox
    Friend WithEvents txt_s3 As System.Windows.Forms.TextBox
    Friend WithEvents txt_p2 As System.Windows.Forms.TextBox
    Friend WithEvents txt_s2 As System.Windows.Forms.TextBox
    Friend WithEvents txt_p1 As System.Windows.Forms.TextBox
    Friend WithEvents txt_s1 As System.Windows.Forms.TextBox
End Class
