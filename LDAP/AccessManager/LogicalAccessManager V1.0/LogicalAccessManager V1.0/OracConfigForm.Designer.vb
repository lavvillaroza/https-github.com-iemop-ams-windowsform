<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OracConfigForm
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
        Me.components = New System.ComponentModel.Container()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.OCF_CANCEL_BTN = New System.Windows.Forms.Button()
        Me.OCF_SAVE_BTN = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtBaseDnAd = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtServerAd = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtPortAd = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPasswordDb = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtDatasourceDb = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtUserIdDb = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel2)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(332, 312)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.OCF_CANCEL_BTN)
        Me.Panel2.Controls.Add(Me.OCF_SAVE_BTN)
        Me.Panel2.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(6, 272)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(320, 34)
        Me.Panel2.TabIndex = 3
        '
        'OCF_CANCEL_BTN
        '
        Me.OCF_CANCEL_BTN.Location = New System.Drawing.Point(227, 5)
        Me.OCF_CANCEL_BTN.Name = "OCF_CANCEL_BTN"
        Me.OCF_CANCEL_BTN.Size = New System.Drawing.Size(75, 23)
        Me.OCF_CANCEL_BTN.TabIndex = 6
        Me.OCF_CANCEL_BTN.Text = "Cancel"
        Me.OCF_CANCEL_BTN.UseVisualStyleBackColor = True
        '
        'OCF_SAVE_BTN
        '
        Me.OCF_SAVE_BTN.Location = New System.Drawing.Point(146, 5)
        Me.OCF_SAVE_BTN.Name = "OCF_SAVE_BTN"
        Me.OCF_SAVE_BTN.Size = New System.Drawing.Size(75, 23)
        Me.OCF_SAVE_BTN.TabIndex = 5
        Me.OCF_SAVE_BTN.Text = "Save"
        Me.OCF_SAVE_BTN.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Location = New System.Drawing.Point(6, 14)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(320, 257)
        Me.Panel1.TabIndex = 2
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.txtBaseDnAd)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.txtServerAd)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.txtPortAd)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.ForeColor = System.Drawing.Color.Gray
        Me.GroupBox3.Location = New System.Drawing.Point(18, 19)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(284, 100)
        Me.GroupBox3.TabIndex = 14
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Active Dir"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(6, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Server"
        '
        'txtBaseDnAd
        '
        Me.txtBaseDnAd.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBaseDnAd.ForeColor = System.Drawing.Color.Black
        Me.txtBaseDnAd.Location = New System.Drawing.Point(98, 71)
        Me.txtBaseDnAd.MaxLength = 25
        Me.txtBaseDnAd.Name = "txtBaseDnAd"
        Me.txtBaseDnAd.Size = New System.Drawing.Size(176, 22)
        Me.txtBaseDnAd.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(81, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 14)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = ":"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(81, 75)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(10, 14)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = ":"
        '
        'txtServerAd
        '
        Me.txtServerAd.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServerAd.ForeColor = System.Drawing.Color.Black
        Me.txtServerAd.Location = New System.Drawing.Point(98, 21)
        Me.txtServerAd.MaxLength = 30
        Me.txtServerAd.Name = "txtServerAd"
        Me.txtServerAd.Size = New System.Drawing.Size(176, 22)
        Me.txtServerAd.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(6, 75)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(52, 14)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "Base Dn"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(6, 49)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(28, 14)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "Port"
        '
        'txtPortAd
        '
        Me.txtPortAd.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPortAd.ForeColor = System.Drawing.Color.Black
        Me.txtPortAd.Location = New System.Drawing.Point(98, 45)
        Me.txtPortAd.MaxLength = 25
        Me.txtPortAd.Name = "txtPortAd"
        Me.txtPortAd.Size = New System.Drawing.Size(176, 22)
        Me.txtPortAd.TabIndex = 3
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(81, 49)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(10, 14)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = ":"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtPasswordDb)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtDatasourceDb)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txtUserIdDb)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.ForeColor = System.Drawing.Color.Gray
        Me.GroupBox2.Location = New System.Drawing.Point(18, 141)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(284, 100)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Database"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(6, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "DataSource"
        '
        'txtPasswordDb
        '
        Me.txtPasswordDb.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPasswordDb.ForeColor = System.Drawing.Color.Black
        Me.txtPasswordDb.Location = New System.Drawing.Point(98, 71)
        Me.txtPasswordDb.MaxLength = 25
        Me.txtPasswordDb.Name = "txtPasswordDb"
        Me.txtPasswordDb.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPasswordDb.Size = New System.Drawing.Size(176, 22)
        Me.txtPasswordDb.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(81, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = ":"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(81, 75)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(10, 14)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = ":"
        '
        'txtDatasourceDb
        '
        Me.txtDatasourceDb.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDatasourceDb.ForeColor = System.Drawing.Color.Black
        Me.txtDatasourceDb.Location = New System.Drawing.Point(98, 21)
        Me.txtDatasourceDb.MaxLength = 30
        Me.txtDatasourceDb.Name = "txtDatasourceDb"
        Me.txtDatasourceDb.Size = New System.Drawing.Size(176, 22)
        Me.txtDatasourceDb.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(6, 75)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 14)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Password"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(6, 49)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 14)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "UserID"
        '
        'txtUserIdDb
        '
        Me.txtUserIdDb.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserIdDb.ForeColor = System.Drawing.Color.Black
        Me.txtUserIdDb.Location = New System.Drawing.Point(98, 45)
        Me.txtUserIdDb.MaxLength = 25
        Me.txtUserIdDb.Name = "txtUserIdDb"
        Me.txtUserIdDb.Size = New System.Drawing.Size(176, 22)
        Me.txtUserIdDb.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(81, 49)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 14)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = ":"
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'OracConfigForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(351, 323)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.Name = "OracConfigForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "OracConfigForm"
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents OCF_CANCEL_BTN As System.Windows.Forms.Button
    Friend WithEvents OCF_SAVE_BTN As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtPasswordDb As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtUserIdDb As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtDatasourceDb As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBaseDnAd As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtServerAd As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtPortAd As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
End Class
