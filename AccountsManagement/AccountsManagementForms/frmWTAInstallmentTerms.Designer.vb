<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWTAInstallmentTerms
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DGridViewTerms = New System.Windows.Forms.DataGridView()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.gbMenu2 = New System.Windows.Forms.GroupBox()
        Me.cmbTerms = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblCollectionDate = New System.Windows.Forms.Label()
        Me.dtStartDD = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.colTermNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSelect = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnCreate = New System.Windows.Forms.Button()
        CType(Me.DGridViewTerms, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbMenu2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DGridViewTerms
        '
        Me.DGridViewTerms.AllowUserToAddRows = False
        Me.DGridViewTerms.AllowUserToResizeRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridViewTerms.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DGridViewTerms.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGridViewTerms.BackgroundColor = System.Drawing.SystemColors.ButtonShadow
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.DimGray
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGridViewTerms.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DGridViewTerms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridViewTerms.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colTermNo, Me.colDueDate, Me.colSelect})
        Me.DGridViewTerms.Location = New System.Drawing.Point(6, 17)
        Me.DGridViewTerms.Name = "DGridViewTerms"
        Me.DGridViewTerms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGridViewTerms.Size = New System.Drawing.Size(493, 262)
        Me.DGridViewTerms.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(12, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 15)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "Due Dates:"
        '
        'gbMenu2
        '
        Me.gbMenu2.Controls.Add(Me.Label4)
        Me.gbMenu2.Controls.Add(Me.DGridViewTerms)
        Me.gbMenu2.Location = New System.Drawing.Point(12, 87)
        Me.gbMenu2.Name = "gbMenu2"
        Me.gbMenu2.Size = New System.Drawing.Size(508, 285)
        Me.gbMenu2.TabIndex = 11
        Me.gbMenu2.TabStop = False
        '
        'cmbTerms
        '
        Me.cmbTerms.FormattingEnabled = True
        Me.cmbTerms.Location = New System.Drawing.Point(6, 36)
        Me.cmbTerms.Name = "cmbTerms"
        Me.cmbTerms.Size = New System.Drawing.Size(51, 23)
        Me.cmbTerms.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblCollectionDate)
        Me.GroupBox1.Controls.Add(Me.btnCancel)
        Me.GroupBox1.Controls.Add(Me.dtStartDD)
        Me.GroupBox1.Controls.Add(Me.btnCreate)
        Me.GroupBox1.Controls.Add(Me.cmbTerms)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(508, 69)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'lblCollectionDate
        '
        Me.lblCollectionDate.AutoSize = True
        Me.lblCollectionDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCollectionDate.Location = New System.Drawing.Point(63, 17)
        Me.lblCollectionDate.Name = "lblCollectionDate"
        Me.lblCollectionDate.Size = New System.Drawing.Size(125, 15)
        Me.lblCollectionDate.TabIndex = 60
        Me.lblCollectionDate.Text = "Starting Due Date:"
        '
        'dtStartDD
        '
        Me.dtStartDD.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtStartDD.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtStartDD.Location = New System.Drawing.Point(66, 36)
        Me.dtStartDD.Name = "dtStartDD"
        Me.dtStartDD.Size = New System.Drawing.Size(121, 21)
        Me.dtStartDD.TabIndex = 59
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 15)
        Me.Label1.TabIndex = 61
        Me.Label1.Text = "Terms:"
        '
        'colTermNo
        '
        Me.colTermNo.HeaderText = "Term"
        Me.colTermNo.Name = "colTermNo"
        Me.colTermNo.Width = 80
        '
        'colDueDate
        '
        Me.colDueDate.HeaderText = "DueDate"
        Me.colDueDate.Name = "colDueDate"
        Me.colDueDate.ReadOnly = True
        Me.colDueDate.Width = 120
        '
        'colSelect
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colSelect.DefaultCellStyle = DataGridViewCellStyle6
        Me.colSelect.HeaderText = "Action"
        Me.colSelect.Name = "colSelect"
        Me.colSelect.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.White
        Me.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Image = Global.AccountsManagementForms.My.Resources.Resources.SaveIconColored22x22
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(316, 18)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(90, 39)
        Me.btnSave.TabIndex = 62
        Me.btnSave.Text = "    &Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.White
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.Black
        Me.btnCancel.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseRedIcon22x22
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(412, 17)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(90, 39)
        Me.btnCancel.TabIndex = 58
        Me.btnCancel.Text = "   &Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnCreate
        '
        Me.btnCreate.BackColor = System.Drawing.Color.White
        Me.btnCreate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCreate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnCreate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnCreate.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCreate.ForeColor = System.Drawing.Color.Black
        Me.btnCreate.Image = Global.AccountsManagementForms.My.Resources.Resources.NewGreenIcon22x22
        Me.btnCreate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCreate.Location = New System.Drawing.Point(220, 18)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(90, 39)
        Me.btnCreate.TabIndex = 57
        Me.btnCreate.Text = "    C&reate"
        Me.btnCreate.UseVisualStyleBackColor = False
        '
        'frmWTAInstallmentTerms
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(532, 389)
        Me.Controls.Add(Me.gbMenu2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmWTAInstallmentTerms"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Payment Terms"
        CType(Me.DGridViewTerms, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbMenu2.ResumeLayout(False)
        Me.gbMenu2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DGridViewTerms As DataGridView
    Friend WithEvents Label4 As Label
    Friend WithEvents gbMenu2 As GroupBox
    Friend WithEvents cmbTerms As ComboBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnCreate As Button
    Friend WithEvents lblCollectionDate As Label
    Friend WithEvents dtStartDD As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents colTermNo As DataGridViewTextBoxColumn
    Friend WithEvents colDueDate As DataGridViewTextBoxColumn
    Friend WithEvents colSelect As DataGridViewLinkColumn
End Class
