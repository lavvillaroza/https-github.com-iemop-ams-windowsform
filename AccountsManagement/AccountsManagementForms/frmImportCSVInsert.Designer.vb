<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportCSVInsert
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
        Me.txtDirectory = New System.Windows.Forms.TextBox()
        Me.btnOpenFile = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtDirectory)
        Me.GroupBox1.Controls.Add(Me.btnOpenFile)
        Me.GroupBox1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(28, 33)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(431, 56)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Choose Filename:"
        '
        'txtDirectory
        '
        Me.txtDirectory.BackColor = System.Drawing.Color.White
        Me.txtDirectory.Location = New System.Drawing.Point(6, 22)
        Me.txtDirectory.Name = "txtDirectory"
        Me.txtDirectory.ReadOnly = True
        Me.txtDirectory.Size = New System.Drawing.Size(373, 21)
        Me.txtDirectory.TabIndex = 2
        '
        'btnOpenFile
        '
        Me.btnOpenFile.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnOpenFile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnOpenFile.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenFile.Image = Global.AccountsManagementForms.My.Resources.Resources.FileSearchIcon22x22
        Me.btnOpenFile.Location = New System.Drawing.Point(385, 17)
        Me.btnOpenFile.Name = "btnOpenFile"
        Me.btnOpenFile.Size = New System.Drawing.Size(35, 30)
        Me.btnOpenFile.TabIndex = 3
        Me.btnOpenFile.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImport.BackColor = System.Drawing.Color.White
        Me.btnImport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnImport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnImport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImport.ForeColor = System.Drawing.Color.Black
        Me.btnImport.Image = Global.AccountsManagementForms.My.Resources.Resources.Upload2Icon22x22
        Me.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImport.Location = New System.Drawing.Point(346, 94)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(113, 39)
        Me.btnImport.TabIndex = 10
        Me.btnImport.Text = "   &Upload File"
        Me.btnImport.UseVisualStyleBackColor = False
        '
        'frmImportCSVInsert
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(486, 166)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnImport)
        Me.Name = "frmImportCSVInsert"
        Me.Text = "frmImportCSVInsert"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtDirectory As System.Windows.Forms.TextBox
    Friend WithEvents btnOpenFile As System.Windows.Forms.Button
    Friend WithEvents btnImport As System.Windows.Forms.Button
End Class
