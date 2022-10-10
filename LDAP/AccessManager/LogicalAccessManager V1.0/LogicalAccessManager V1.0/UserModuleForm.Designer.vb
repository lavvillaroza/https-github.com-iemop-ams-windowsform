<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserModuleForm
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.UMF_CANCEL_BTN = New System.Windows.Forms.Button()
        Me.UMF_SAVE_BTN = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.UMF_MDLNAME_CMBBOX = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.UMF_APPNAME_CMBBOX = New System.Windows.Forms.ComboBox()
        Me.UMF_USERNAME_CMBBOX = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel2)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(397, 168)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.UMF_CANCEL_BTN)
        Me.Panel2.Controls.Add(Me.UMF_SAVE_BTN)
        Me.Panel2.Location = New System.Drawing.Point(6, 126)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(385, 30)
        Me.Panel2.TabIndex = 1
        '
        'UMF_CANCEL_BTN
        '
        Me.UMF_CANCEL_BTN.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UMF_CANCEL_BTN.Location = New System.Drawing.Point(299, 3)
        Me.UMF_CANCEL_BTN.Name = "UMF_CANCEL_BTN"
        Me.UMF_CANCEL_BTN.Size = New System.Drawing.Size(75, 23)
        Me.UMF_CANCEL_BTN.TabIndex = 8
        Me.UMF_CANCEL_BTN.Text = "Cancel"
        Me.UMF_CANCEL_BTN.UseVisualStyleBackColor = True
        '
        'UMF_SAVE_BTN
        '
        Me.UMF_SAVE_BTN.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UMF_SAVE_BTN.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UMF_SAVE_BTN.Location = New System.Drawing.Point(218, 3)
        Me.UMF_SAVE_BTN.Name = "UMF_SAVE_BTN"
        Me.UMF_SAVE_BTN.Size = New System.Drawing.Size(75, 23)
        Me.UMF_SAVE_BTN.TabIndex = 7
        Me.UMF_SAVE_BTN.Text = "Save"
        Me.UMF_SAVE_BTN.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.UMF_MDLNAME_CMBBOX)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.UMF_APPNAME_CMBBOX)
        Me.Panel1.Controls.Add(Me.UMF_USERNAME_CMBBOX)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Location = New System.Drawing.Point(6, 13)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(385, 109)
        Me.Panel1.TabIndex = 0
        '
        'UMF_MDLNAME_CMBBOX
        '
        Me.UMF_MDLNAME_CMBBOX.BackColor = System.Drawing.Color.White
        Me.UMF_MDLNAME_CMBBOX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.UMF_MDLNAME_CMBBOX.FormattingEnabled = True
        Me.UMF_MDLNAME_CMBBOX.Location = New System.Drawing.Point(134, 74)
        Me.UMF_MDLNAME_CMBBOX.Name = "UMF_MDLNAME_CMBBOX"
        Me.UMF_MDLNAME_CMBBOX.Size = New System.Drawing.Size(239, 21)
        Me.UMF_MDLNAME_CMBBOX.TabIndex = 54
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(118, 78)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(10, 14)
        Me.Label6.TabIndex = 53
        Me.Label6.Text = ":"
        '
        'UMF_APPNAME_CMBBOX
        '
        Me.UMF_APPNAME_CMBBOX.BackColor = System.Drawing.Color.White
        Me.UMF_APPNAME_CMBBOX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.UMF_APPNAME_CMBBOX.FormattingEnabled = True
        Me.UMF_APPNAME_CMBBOX.Location = New System.Drawing.Point(134, 46)
        Me.UMF_APPNAME_CMBBOX.Name = "UMF_APPNAME_CMBBOX"
        Me.UMF_APPNAME_CMBBOX.Size = New System.Drawing.Size(239, 21)
        Me.UMF_APPNAME_CMBBOX.TabIndex = 51
        '
        'UMF_USERNAME_CMBBOX
        '
        Me.UMF_USERNAME_CMBBOX.BackColor = System.Drawing.Color.White
        Me.UMF_USERNAME_CMBBOX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.UMF_USERNAME_CMBBOX.FormattingEnabled = True
        Me.UMF_USERNAME_CMBBOX.Location = New System.Drawing.Point(134, 19)
        Me.UMF_USERNAME_CMBBOX.Name = "UMF_USERNAME_CMBBOX"
        Me.UMF_USERNAME_CMBBOX.Size = New System.Drawing.Size(121, 21)
        Me.UMF_USERNAME_CMBBOX.TabIndex = 50
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(12, 74)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(84, 14)
        Me.Label7.TabIndex = 46
        Me.Label7.Text = "Module Name"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(118, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 14)
        Me.Label4.TabIndex = 45
        Me.Label4.Text = ":"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 14)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "Application Name"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(117, 26)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(10, 14)
        Me.Label9.TabIndex = 36
        Me.Label9.Text = ":"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(12, 26)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(67, 14)
        Me.Label10.TabIndex = 35
        Me.Label10.Text = "User Name"
        '
        'UserModuleForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(412, 179)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "UserModuleForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "UserModuleForm"
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents UMF_USERNAME_CMBBOX As System.Windows.Forms.ComboBox
    Friend WithEvents UMF_APPNAME_CMBBOX As System.Windows.Forms.ComboBox
    Friend WithEvents UMF_CANCEL_BTN As System.Windows.Forms.Button
    Friend WithEvents UMF_SAVE_BTN As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents UMF_MDLNAME_CMBBOX As System.Windows.Forms.ComboBox
End Class
