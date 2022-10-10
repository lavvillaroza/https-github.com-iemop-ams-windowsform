<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrudentialMgt
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ddlParticipantID = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtReplenishmentAmount = New System.Windows.Forms.TextBox()
        Me.txtInterestAmount = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DGridMain = New System.Windows.Forms.DataGridView()
        Me.colIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCheck = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtTransDate = New System.Windows.Forms.DateTimePicker()
        Me.chckAll = New System.Windows.Forms.CheckBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        CType(Me.DGridMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(65, 419)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ParticipantID:"
        '
        'ddlParticipantID
        '
        Me.ddlParticipantID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlParticipantID.FormattingEnabled = True
        Me.ddlParticipantID.Location = New System.Drawing.Point(141, 416)
        Me.ddlParticipantID.Name = "ddlParticipantID"
        Me.ddlParticipantID.Size = New System.Drawing.Size(184, 20)
        Me.ddlParticipantID.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 441)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(120, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Replenishment Amount:"
        '
        'txtReplenishmentAmount
        '
        Me.txtReplenishmentAmount.Location = New System.Drawing.Point(141, 438)
        Me.txtReplenishmentAmount.Name = "txtReplenishmentAmount"
        Me.txtReplenishmentAmount.Size = New System.Drawing.Size(184, 20)
        Me.txtReplenishmentAmount.TabIndex = 3
        '
        'txtInterestAmount
        '
        Me.txtInterestAmount.Location = New System.Drawing.Point(141, 460)
        Me.txtInterestAmount.Name = "txtInterestAmount"
        Me.txtInterestAmount.Size = New System.Drawing.Size(184, 20)
        Me.txtInterestAmount.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(55, 461)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 12)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Interest Amount:"
        '
        'DGridMain
        '
        Me.DGridMain.AllowUserToAddRows = False
        Me.DGridMain.AllowUserToResizeColumns = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridMain.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGridMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridMain.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colIDNumber, Me.colParticipantID, Me.colAmount, Me.colCheck})
        Me.DGridMain.Location = New System.Drawing.Point(12, 49)
        Me.DGridMain.MultiSelect = False
        Me.DGridMain.Name = "DGridMain"
        Me.DGridMain.RowHeadersWidth = 20
        Me.DGridMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGridMain.Size = New System.Drawing.Size(537, 491)
        Me.DGridMain.TabIndex = 8
        '
        'colIDNumber
        '
        Me.colIDNumber.HeaderText = "ID Number"
        Me.colIDNumber.Name = "colIDNumber"
        Me.colIDNumber.ReadOnly = True
        '
        'colParticipantID
        '
        Me.colParticipantID.HeaderText = "Participant ID"
        Me.colParticipantID.Name = "colParticipantID"
        Me.colParticipantID.ReadOnly = True
        '
        'colAmount
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        Me.colAmount.DefaultCellStyle = DataGridViewCellStyle2
        Me.colAmount.HeaderText = "Amount"
        Me.colAmount.Name = "colAmount"
        Me.colAmount.Width = 200
        '
        'colCheck
        '
        Me.colCheck.HeaderText = "       "
        Me.colCheck.Name = "colCheck"
        Me.colCheck.Width = 50
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(10, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 14)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Transaction Date:"
        '
        'dtTransDate
        '
        Me.dtTransDate.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtTransDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTransDate.Location = New System.Drawing.Point(117, 14)
        Me.dtTransDate.Name = "dtTransDate"
        Me.dtTransDate.Size = New System.Drawing.Size(124, 20)
        Me.dtTransDate.TabIndex = 15
        '
        'chckAll
        '
        Me.chckAll.AutoSize = True
        Me.chckAll.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckAll.ForeColor = System.Drawing.Color.Black
        Me.chckAll.Location = New System.Drawing.Point(475, 18)
        Me.chckAll.Name = "chckAll"
        Me.chckAll.Size = New System.Drawing.Size(70, 16)
        Me.chckAll.TabIndex = 16
        Me.chckAll.Text = "Check All"
        Me.chckAll.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.White
        Me.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.ForeColor = System.Drawing.Color.Black
        Me.btnClear.Image = Global.AccountsManagementForms.My.Resources.Resources.CancelIconRed22x22
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.Location = New System.Drawing.Point(303, 548)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(120, 39)
        Me.btnClear.TabIndex = 13
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.White
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.ForeColor = System.Drawing.Color.Black
        Me.btnCancel.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(429, 548)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(120, 39)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.White
        Me.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Image = Global.AccountsManagementForms.My.Resources.Resources.SaveIconColored22x22
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(177, 548)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(120, 39)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'frmPrudentialMgt
        '
        Me.AccessibleDescription = " "
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(557, 601)
        Me.Controls.Add(Me.chckAll)
        Me.Controls.Add(Me.dtTransDate)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.DGridMain)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtInterestAmount)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtReplenishmentAmount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ddlParticipantID)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmPrudentialMgt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Prudential Management"
        CType(Me.DGridMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ddlParticipantID As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtReplenishmentAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtInterestAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents DGridMain As System.Windows.Forms.DataGridView
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtTransDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chckAll As System.Windows.Forms.CheckBox
    Friend WithEvents colIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCheck As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
