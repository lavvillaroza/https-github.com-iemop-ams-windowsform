<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCheckNumber
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
        Me.dgv_History = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_BatchNo = New System.Windows.Forms.TextBox()
        Me.txt_InitCheckNo = New System.Windows.Forms.TextBox()
        Me.txt_LastCheckNo = New System.Windows.Forms.TextBox()
        Me.txt_RemainingAvailable = New System.Windows.Forms.TextBox()
        Me.txt_LastSeqUsed = New System.Windows.Forms.TextBox()
        Me.cmd_Close = New System.Windows.Forms.Button()
        Me.cmd_Refresh = New System.Windows.Forms.Button()
        Me.cmd_Add = New System.Windows.Forms.Button()
        CType(Me.dgv_History, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgv_History
        '
        Me.dgv_History.AllowUserToAddRows = False
        Me.dgv_History.AllowUserToDeleteRows = False
        Me.dgv_History.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_History.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgv_History.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_History.Location = New System.Drawing.Point(6, 19)
        Me.dgv_History.Name = "dgv_History"
        Me.dgv_History.Size = New System.Drawing.Size(552, 216)
        Me.dgv_History.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgv_History)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 135)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(564, 241)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Check History:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(34, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Current Batch:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(22, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Initial Check No.:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(28, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 14)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Last Check No.:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(306, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(123, 14)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Last Sequence Used:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(294, 37)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(133, 14)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Total Checks Available:"
        '
        'txt_BatchNo
        '
        Me.txt_BatchNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_BatchNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_BatchNo.Location = New System.Drawing.Point(125, 6)
        Me.txt_BatchNo.Name = "txt_BatchNo"
        Me.txt_BatchNo.ReadOnly = True
        Me.txt_BatchNo.Size = New System.Drawing.Size(141, 20)
        Me.txt_BatchNo.TabIndex = 7
        '
        'txt_InitCheckNo
        '
        Me.txt_InitCheckNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_InitCheckNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_InitCheckNo.Location = New System.Drawing.Point(125, 35)
        Me.txt_InitCheckNo.Name = "txt_InitCheckNo"
        Me.txt_InitCheckNo.ReadOnly = True
        Me.txt_InitCheckNo.Size = New System.Drawing.Size(141, 20)
        Me.txt_InitCheckNo.TabIndex = 8
        '
        'txt_LastCheckNo
        '
        Me.txt_LastCheckNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_LastCheckNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_LastCheckNo.Location = New System.Drawing.Point(125, 64)
        Me.txt_LastCheckNo.Name = "txt_LastCheckNo"
        Me.txt_LastCheckNo.ReadOnly = True
        Me.txt_LastCheckNo.Size = New System.Drawing.Size(141, 20)
        Me.txt_LastCheckNo.TabIndex = 9
        '
        'txt_RemainingAvailable
        '
        Me.txt_RemainingAvailable.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_RemainingAvailable.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_RemainingAvailable.Location = New System.Drawing.Point(431, 35)
        Me.txt_RemainingAvailable.Name = "txt_RemainingAvailable"
        Me.txt_RemainingAvailable.ReadOnly = True
        Me.txt_RemainingAvailable.Size = New System.Drawing.Size(141, 20)
        Me.txt_RemainingAvailable.TabIndex = 11
        '
        'txt_LastSeqUsed
        '
        Me.txt_LastSeqUsed.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_LastSeqUsed.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_LastSeqUsed.Location = New System.Drawing.Point(431, 6)
        Me.txt_LastSeqUsed.Name = "txt_LastSeqUsed"
        Me.txt_LastSeqUsed.ReadOnly = True
        Me.txt_LastSeqUsed.Size = New System.Drawing.Size(141, 20)
        Me.txt_LastSeqUsed.TabIndex = 10
        '
        'cmd_Close
        '
        Me.cmd_Close.BackColor = System.Drawing.Color.White
        Me.cmd_Close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Close.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Close.ForeColor = System.Drawing.Color.Black
        Me.cmd_Close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Close.Location = New System.Drawing.Point(461, 92)
        Me.cmd_Close.Name = "cmd_Close"
        Me.cmd_Close.Size = New System.Drawing.Size(115, 37)
        Me.cmd_Close.TabIndex = 14
        Me.cmd_Close.Text = "Close"
        Me.cmd_Close.UseVisualStyleBackColor = False
        '
        'cmd_Refresh
        '
        Me.cmd_Refresh.BackColor = System.Drawing.Color.White
        Me.cmd_Refresh.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Refresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Refresh.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Refresh.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Refresh.ForeColor = System.Drawing.Color.Black
        Me.cmd_Refresh.Image = Global.AccountsManagementForms.My.Resources.Resources.RefreshGreenIcon22x22
        Me.cmd_Refresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Refresh.Location = New System.Drawing.Point(340, 92)
        Me.cmd_Refresh.Name = "cmd_Refresh"
        Me.cmd_Refresh.Size = New System.Drawing.Size(115, 37)
        Me.cmd_Refresh.TabIndex = 13
        Me.cmd_Refresh.Text = "Refresh"
        Me.cmd_Refresh.UseVisualStyleBackColor = False
        '
        'cmd_Add
        '
        Me.cmd_Add.BackColor = System.Drawing.Color.White
        Me.cmd_Add.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Add.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Add.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Add.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Add.ForeColor = System.Drawing.Color.Black
        Me.cmd_Add.Image = Global.AccountsManagementForms.My.Resources.Resources.NewGreenIcon22x22
        Me.cmd_Add.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Add.Location = New System.Drawing.Point(219, 93)
        Me.cmd_Add.Name = "cmd_Add"
        Me.cmd_Add.Size = New System.Drawing.Size(115, 37)
        Me.cmd_Add.TabIndex = 12
        Me.cmd_Add.Text = "Add"
        Me.cmd_Add.UseVisualStyleBackColor = False
        '
        'frmCheckNumber
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(588, 388)
        Me.Controls.Add(Me.cmd_Close)
        Me.Controls.Add(Me.cmd_Refresh)
        Me.Controls.Add(Me.cmd_Add)
        Me.Controls.Add(Me.txt_RemainingAvailable)
        Me.Controls.Add(Me.txt_LastSeqUsed)
        Me.Controls.Add(Me.txt_LastCheckNo)
        Me.Controls.Add(Me.txt_InitCheckNo)
        Me.Controls.Add(Me.txt_BatchNo)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCheckNumber"
        Me.Text = "Check Number Input"
        CType(Me.dgv_History, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgv_History As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt_BatchNo As System.Windows.Forms.TextBox
    Friend WithEvents txt_InitCheckNo As System.Windows.Forms.TextBox
    Friend WithEvents txt_LastCheckNo As System.Windows.Forms.TextBox
    Friend WithEvents txt_RemainingAvailable As System.Windows.Forms.TextBox
    Friend WithEvents txt_LastSeqUsed As System.Windows.Forms.TextBox
    Friend WithEvents cmd_Add As System.Windows.Forms.Button
    Friend WithEvents cmd_Refresh As System.Windows.Forms.Button
    Friend WithEvents cmd_Close As System.Windows.Forms.Button
End Class
