<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserForm
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
        Me.UF_GRPBOX = New System.Windows.Forms.GroupBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.UF_CANCEL_BTN = New System.Windows.Forms.Button()
        Me.UF_SAVE_BTN = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.UF_USERNAME_TXTBOX = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.UF_LASTNAME_TXTBOX = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.UF_MI_TXTBOX = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.UF_FIRSTNAME_TXTBOX = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.UF_POSITION_TXTBOX = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.UF_GRPBOX.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UF_GRPBOX
        '
        Me.UF_GRPBOX.Controls.Add(Me.Panel2)
        Me.UF_GRPBOX.Controls.Add(Me.Panel1)
        Me.UF_GRPBOX.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UF_GRPBOX.Location = New System.Drawing.Point(9, 1)
        Me.UF_GRPBOX.Name = "UF_GRPBOX"
        Me.UF_GRPBOX.Size = New System.Drawing.Size(343, 229)
        Me.UF_GRPBOX.TabIndex = 0
        Me.UF_GRPBOX.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.UF_CANCEL_BTN)
        Me.Panel2.Controls.Add(Me.UF_SAVE_BTN)
        Me.Panel2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(6, 177)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(331, 34)
        Me.Panel2.TabIndex = 4
        '
        'UF_CANCEL_BTN
        '
        Me.UF_CANCEL_BTN.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UF_CANCEL_BTN.Location = New System.Drawing.Point(235, 6)
        Me.UF_CANCEL_BTN.Name = "UF_CANCEL_BTN"
        Me.UF_CANCEL_BTN.Size = New System.Drawing.Size(75, 23)
        Me.UF_CANCEL_BTN.TabIndex = 6
        Me.UF_CANCEL_BTN.Text = "Cancel"
        Me.UF_CANCEL_BTN.UseVisualStyleBackColor = True
        '
        'UF_SAVE_BTN
        '
        Me.UF_SAVE_BTN.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UF_SAVE_BTN.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UF_SAVE_BTN.Location = New System.Drawing.Point(154, 6)
        Me.UF_SAVE_BTN.Name = "UF_SAVE_BTN"
        Me.UF_SAVE_BTN.Size = New System.Drawing.Size(75, 23)
        Me.UF_SAVE_BTN.TabIndex = 5
        Me.UF_SAVE_BTN.Text = "Save"
        Me.UF_SAVE_BTN.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.UF_POSITION_TXTBOX)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.UF_USERNAME_TXTBOX)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.UF_LASTNAME_TXTBOX)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.UF_MI_TXTBOX)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.UF_FIRSTNAME_TXTBOX)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(6, 23)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(331, 154)
        Me.Panel1.TabIndex = 3
        '
        'UF_USERNAME_TXTBOX
        '
        Me.UF_USERNAME_TXTBOX.BackColor = System.Drawing.Color.White
        Me.UF_USERNAME_TXTBOX.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UF_USERNAME_TXTBOX.Location = New System.Drawing.Point(114, 9)
        Me.UF_USERNAME_TXTBOX.MaxLength = 50
        Me.UF_USERNAME_TXTBOX.Name = "UF_USERNAME_TXTBOX"
        Me.UF_USERNAME_TXTBOX.Size = New System.Drawing.Size(142, 22)
        Me.UF_USERNAME_TXTBOX.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(97, 13)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(10, 14)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = ":"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(22, 13)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(67, 14)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "User Name"
        '
        'UF_LASTNAME_TXTBOX
        '
        Me.UF_LASTNAME_TXTBOX.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UF_LASTNAME_TXTBOX.Location = New System.Drawing.Point(114, 87)
        Me.UF_LASTNAME_TXTBOX.MaxLength = 50
        Me.UF_LASTNAME_TXTBOX.Name = "UF_LASTNAME_TXTBOX"
        Me.UF_LASTNAME_TXTBOX.Size = New System.Drawing.Size(187, 22)
        Me.UF_LASTNAME_TXTBOX.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(97, 91)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 14)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = ":"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(22, 91)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 14)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Last Name"
        '
        'UF_MI_TXTBOX
        '
        Me.UF_MI_TXTBOX.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UF_MI_TXTBOX.Location = New System.Drawing.Point(114, 61)
        Me.UF_MI_TXTBOX.MaxLength = 2
        Me.UF_MI_TXTBOX.Name = "UF_MI_TXTBOX"
        Me.UF_MI_TXTBOX.Size = New System.Drawing.Size(61, 22)
        Me.UF_MI_TXTBOX.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(97, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 14)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = ":"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(22, 65)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(21, 14)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "MI"
        '
        'UF_FIRSTNAME_TXTBOX
        '
        Me.UF_FIRSTNAME_TXTBOX.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UF_FIRSTNAME_TXTBOX.Location = New System.Drawing.Point(114, 35)
        Me.UF_FIRSTNAME_TXTBOX.MaxLength = 50
        Me.UF_FIRSTNAME_TXTBOX.Name = "UF_FIRSTNAME_TXTBOX"
        Me.UF_FIRSTNAME_TXTBOX.Size = New System.Drawing.Size(187, 22)
        Me.UF_FIRSTNAME_TXTBOX.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(97, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = ":"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(22, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "First Name"
        '
        'UF_POSITION_TXTBOX
        '
        Me.UF_POSITION_TXTBOX.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UF_POSITION_TXTBOX.Location = New System.Drawing.Point(114, 115)
        Me.UF_POSITION_TXTBOX.MaxLength = 50
        Me.UF_POSITION_TXTBOX.Name = "UF_POSITION_TXTBOX"
        Me.UF_POSITION_TXTBOX.Size = New System.Drawing.Size(187, 22)
        Me.UF_POSITION_TXTBOX.TabIndex = 16
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(97, 119)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(10, 14)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = ":"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(22, 119)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 14)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Position"
        '
        'UserForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(364, 238)
        Me.Controls.Add(Me.UF_GRPBOX)
        Me.Name = "UserForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "UserForm"
        Me.UF_GRPBOX.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UF_GRPBOX As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents UF_USERNAME_TXTBOX As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents UF_LASTNAME_TXTBOX As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents UF_MI_TXTBOX As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents UF_FIRSTNAME_TXTBOX As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents UF_CANCEL_BTN As System.Windows.Forms.Button
    Friend WithEvents UF_SAVE_BTN As System.Windows.Forms.Button
    Friend WithEvents UF_POSITION_TXTBOX As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
End Class
