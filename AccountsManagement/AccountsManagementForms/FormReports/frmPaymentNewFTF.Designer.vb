<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymentNewFTF
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
        Me.cmb_FTFTransType = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btn_GenerateFTF = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmb_FTFTransType
        '
        Me.cmb_FTFTransType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_FTFTransType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_FTFTransType.FormattingEnabled = True
        Me.cmb_FTFTransType.Location = New System.Drawing.Point(9, 19)
        Me.cmb_FTFTransType.Name = "cmb_FTFTransType"
        Me.cmb_FTFTransType.Size = New System.Drawing.Size(222, 22)
        Me.cmb_FTFTransType.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Location = New System.Drawing.Point(0, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(266, 115)
        Me.Panel1.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmb_FTFTransType)
        Me.GroupBox1.Controls.Add(Me.btn_GenerateFTF)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(15, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(237, 99)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Transaction"
        '
        'btn_GenerateFTF
        '
        Me.btn_GenerateFTF.BackColor = System.Drawing.Color.White
        Me.btn_GenerateFTF.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_GenerateFTF.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_GenerateFTF.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_GenerateFTF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_GenerateFTF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_GenerateFTF.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.btn_GenerateFTF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_GenerateFTF.Location = New System.Drawing.Point(126, 52)
        Me.btn_GenerateFTF.Name = "btn_GenerateFTF"
        Me.btn_GenerateFTF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btn_GenerateFTF.Size = New System.Drawing.Size(105, 39)
        Me.btn_GenerateFTF.TabIndex = 1
        Me.btn_GenerateFTF.Text = "Generate"
        Me.btn_GenerateFTF.UseVisualStyleBackColor = False
        '
        'frmPaymentNewFTF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(267, 116)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPaymentNewFTF"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Payment FTF"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmb_FTFTransType As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btn_GenerateFTF As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
