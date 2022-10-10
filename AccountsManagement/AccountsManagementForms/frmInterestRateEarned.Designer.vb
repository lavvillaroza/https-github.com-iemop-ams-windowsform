<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInterestRateEarned
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInterestRateEarned))
        Me.dgvIntRateEarned = New System.Windows.Forms.DataGridView()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.trans_Date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.int_Earned = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.int_Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.jv_number = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.int_remarks = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.int_update = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.int_updatedby = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtInterestRate = New System.Windows.Forms.TextBox()
        Me.btnFilterOnOff = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.rtbRemarks = New System.Windows.Forms.RichTextBox()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.txtid = New System.Windows.Forms.TextBox()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdNew = New System.Windows.Forms.Button()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.transactionDate = New System.Windows.Forms.DateTimePicker()
        Me.grpSearch = New System.Windows.Forms.GroupBox()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboSearch = New System.Windows.Forms.ComboBox()
        Me.dtSearchTo = New System.Windows.Forms.DateTimePicker()
        Me.dtSearchFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblRecordCount = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnlFilter = New System.Windows.Forms.Panel()
        Me.btnFilterClose = New System.Windows.Forms.Button()
        CType(Me.dgvIntRateEarned, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.grpSearch.SuspendLayout()
        Me.pnlFilter.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvIntRateEarned
        '
        Me.dgvIntRateEarned.AllowUserToAddRows = False
        Me.dgvIntRateEarned.AllowUserToDeleteRows = False
        Me.dgvIntRateEarned.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Cyan
        Me.dgvIntRateEarned.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvIntRateEarned.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvIntRateEarned.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.trans_Date, Me.int_Earned, Me.int_Status, Me.jv_number, Me.int_remarks, Me.int_update, Me.int_updatedby})
        Me.dgvIntRateEarned.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvIntRateEarned.Location = New System.Drawing.Point(10, 188)
        Me.dgvIntRateEarned.MultiSelect = False
        Me.dgvIntRateEarned.Name = "dgvIntRateEarned"
        Me.dgvIntRateEarned.RowHeadersVisible = False
        Me.dgvIntRateEarned.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvIntRateEarned.Size = New System.Drawing.Size(807, 342)
        Me.dgvIntRateEarned.TabIndex = 0
        '
        'ID
        '
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.Visible = False
        Me.ID.Width = 50
        '
        'trans_Date
        '
        Me.trans_Date.HeaderText = "Transaction Date"
        Me.trans_Date.Name = "trans_Date"
        Me.trans_Date.Width = 115
        '
        'int_Earned
        '
        Me.int_Earned.HeaderText = "Interest Earned"
        Me.int_Earned.Name = "int_Earned"
        Me.int_Earned.Width = 105
        '
        'int_Status
        '
        Me.int_Status.HeaderText = "Status"
        Me.int_Status.Name = "int_Status"
        Me.int_Status.Width = 80
        '
        'jv_number
        '
        Me.jv_number.HeaderText = "JV Number"
        Me.jv_number.Name = "jv_number"
        '
        'int_remarks
        '
        Me.int_remarks.HeaderText = "Remarks"
        Me.int_remarks.Name = "int_remarks"
        Me.int_remarks.Width = 200
        '
        'int_update
        '
        Me.int_update.HeaderText = "Last Update"
        Me.int_update.Name = "int_update"
        '
        'int_updatedby
        '
        Me.int_updatedby.HeaderText = "Updated by"
        Me.int_updatedby.Name = "int_updatedby"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtInterestRate)
        Me.GroupBox1.Controls.Add(Me.btnFilterOnOff)
        Me.GroupBox1.Controls.Add(Me.cmdDelete)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.rtbRemarks)
        Me.GroupBox1.Controls.Add(Me.cmdCancel)
        Me.GroupBox1.Controls.Add(Me.txtid)
        Me.GroupBox1.Controls.Add(Me.cmdSave)
        Me.GroupBox1.Controls.Add(Me.cmdNew)
        Me.GroupBox1.Controls.Add(Me.cboStatus)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.transactionDate)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(807, 173)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'txtInterestRate
        '
        Me.txtInterestRate.Location = New System.Drawing.Point(161, 59)
        Me.txtInterestRate.Name = "txtInterestRate"
        Me.txtInterestRate.Size = New System.Drawing.Size(163, 21)
        Me.txtInterestRate.TabIndex = 18
        '
        'btnFilterOnOff
        '
        Me.btnFilterOnOff.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnFilterOnOff.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnFilterOnOff.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnFilterOnOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFilterOnOff.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFilterOnOff.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.btnFilterOnOff.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFilterOnOff.Location = New System.Drawing.Point(761, 131)
        Me.btnFilterOnOff.Name = "btnFilterOnOff"
        Me.btnFilterOnOff.Size = New System.Drawing.Size(35, 30)
        Me.btnFilterOnOff.TabIndex = 17
        Me.btnFilterOnOff.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFilterOnOff.UseVisualStyleBackColor = True
        '
        'cmdDelete
        '
        Me.cmdDelete.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmdDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmdDelete.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDelete.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdDelete.Location = New System.Drawing.Point(679, 131)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(35, 30)
        Me.cmdDelete.TabIndex = 14
        Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdDelete.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(382, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 14)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Remarks :"
        '
        'rtbRemarks
        '
        Me.rtbRemarks.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbRemarks.Location = New System.Drawing.Point(380, 29)
        Me.rtbRemarks.MaxLength = 50
        Me.rtbRemarks.Name = "rtbRemarks"
        Me.rtbRemarks.Size = New System.Drawing.Size(421, 87)
        Me.rtbRemarks.TabIndex = 12
        Me.rtbRemarks.Text = ""
        '
        'cmdCancel
        '
        Me.cmdCancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmdCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmdCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.Image = Global.AccountsManagementForms.My.Resources.Resources.CancelIconRed22x22
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCancel.Location = New System.Drawing.Point(720, 131)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(35, 30)
        Me.cmdCancel.TabIndex = 11
        Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'txtid
        '
        Me.txtid.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtid.Location = New System.Drawing.Point(48, 94)
        Me.txtid.Name = "txtid"
        Me.txtid.Size = New System.Drawing.Size(32, 20)
        Me.txtid.TabIndex = 10
        Me.txtid.Visible = False
        '
        'cmdSave
        '
        Me.cmdSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmdSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmdSave.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Image = Global.AccountsManagementForms.My.Resources.Resources.SaveIconColored22x22
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSave.Location = New System.Drawing.Point(638, 131)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(35, 30)
        Me.cmdSave.TabIndex = 9
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdNew
        '
        Me.cmdNew.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmdNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmdNew.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmdNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdNew.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNew.Image = Global.AccountsManagementForms.My.Resources.Resources.NewGreenIcon22x22
        Me.cmdNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdNew.Location = New System.Drawing.Point(597, 131)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(35, 30)
        Me.cmdNew.TabIndex = 8
        Me.cmdNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdNew.UseVisualStyleBackColor = True
        '
        'cboStatus
        '
        Me.cboStatus.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(161, 96)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(163, 20)
        Me.cboStatus.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(108, 98)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 14)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Status :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(56, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 14)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Interest Amount :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(51, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 14)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Transaction Date :"
        '
        'transactionDate
        '
        Me.transactionDate.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.transactionDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.transactionDate.Location = New System.Drawing.Point(161, 20)
        Me.transactionDate.Name = "transactionDate"
        Me.transactionDate.Size = New System.Drawing.Size(163, 20)
        Me.transactionDate.TabIndex = 2
        '
        'grpSearch
        '
        Me.grpSearch.Controls.Add(Me.cmdSearch)
        Me.grpSearch.Controls.Add(Me.Label7)
        Me.grpSearch.Controls.Add(Me.cboSearch)
        Me.grpSearch.Controls.Add(Me.dtSearchTo)
        Me.grpSearch.Controls.Add(Me.dtSearchFrom)
        Me.grpSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpSearch.Location = New System.Drawing.Point(6, 48)
        Me.grpSearch.Name = "grpSearch"
        Me.grpSearch.Size = New System.Drawing.Size(343, 59)
        Me.grpSearch.TabIndex = 2
        Me.grpSearch.TabStop = False
        '
        'cmdSearch
        '
        Me.cmdSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmdSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmdSearch.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSearch.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.Image = Global.AccountsManagementForms.My.Resources.Resources.OkIcon22x22
        Me.cmdSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSearch.Location = New System.Drawing.Point(291, 12)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(45, 39)
        Me.cmdSearch.TabIndex = 12
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSearch.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(92, 18)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(11, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "-"
        '
        'cboSearch
        '
        Me.cboSearch.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSearch.FormattingEnabled = True
        Me.cboSearch.Location = New System.Drawing.Point(198, 13)
        Me.cboSearch.Name = "cboSearch"
        Me.cboSearch.Size = New System.Drawing.Size(84, 23)
        Me.cboSearch.TabIndex = 8
        '
        'dtSearchTo
        '
        Me.dtSearchTo.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtSearchTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtSearchTo.Location = New System.Drawing.Point(107, 14)
        Me.dtSearchTo.Name = "dtSearchTo"
        Me.dtSearchTo.Size = New System.Drawing.Size(82, 22)
        Me.dtSearchTo.TabIndex = 4
        '
        'dtSearchFrom
        '
        Me.dtSearchFrom.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtSearchFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtSearchFrom.Location = New System.Drawing.Point(8, 13)
        Me.dtSearchFrom.Name = "dtSearchFrom"
        Me.dtSearchFrom.Size = New System.Drawing.Size(81, 22)
        Me.dtSearchFrom.TabIndex = 3
        '
        'lblRecordCount
        '
        Me.lblRecordCount.AutoSize = True
        Me.lblRecordCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRecordCount.Location = New System.Drawing.Point(694, 811)
        Me.lblRecordCount.Name = "lblRecordCount"
        Me.lblRecordCount.Size = New System.Drawing.Size(0, 15)
        Me.lblRecordCount.TabIndex = 15
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 542)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(827, 22)
        Me.StatusStrip1.TabIndex = 16
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(3, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 14)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Search :"
        '
        'pnlFilter
        '
        Me.pnlFilter.Controls.Add(Me.btnFilterClose)
        Me.pnlFilter.Controls.Add(Me.Label5)
        Me.pnlFilter.Controls.Add(Me.grpSearch)
        Me.pnlFilter.Location = New System.Drawing.Point(245, 257)
        Me.pnlFilter.Name = "pnlFilter"
        Me.pnlFilter.Size = New System.Drawing.Size(354, 126)
        Me.pnlFilter.TabIndex = 15
        Me.pnlFilter.Visible = False
        '
        'btnFilterClose
        '
        Me.btnFilterClose.BackColor = System.Drawing.Color.White
        Me.btnFilterClose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnFilterClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnFilterClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnFilterClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFilterClose.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFilterClose.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseRedIcon22x22
        Me.btnFilterClose.Location = New System.Drawing.Point(320, 3)
        Me.btnFilterClose.Name = "btnFilterClose"
        Me.btnFilterClose.Size = New System.Drawing.Size(30, 30)
        Me.btnFilterClose.TabIndex = 18
        Me.btnFilterClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFilterClose.UseVisualStyleBackColor = False
        '
        'frmInterestRateEarned
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(827, 564)
        Me.Controls.Add(Me.pnlFilter)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.lblRecordCount)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvIntRateEarned)
        Me.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmInterestRateEarned"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Interest  Earned"
        CType(Me.dgvIntRateEarned,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.grpSearch.ResumeLayout(false)
        Me.grpSearch.PerformLayout
        Me.pnlFilter.ResumeLayout(false)
        Me.pnlFilter.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents dgvIntRateEarned As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents transactionDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdNew As System.Windows.Forms.Button
    Friend WithEvents txtid As System.Windows.Forms.TextBox
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents rtbRemarks As System.Windows.Forms.RichTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents trans_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents int_Earned As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents int_Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents jv_number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents int_remarks As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents int_update As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents int_updatedby As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents grpSearch As System.Windows.Forms.GroupBox
    Friend WithEvents cboSearch As System.Windows.Forms.ComboBox
    Friend WithEvents dtSearchTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtSearchFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblRecordCount As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pnlFilter As System.Windows.Forms.Panel
    Friend WithEvents btnFilterOnOff As System.Windows.Forms.Button
    Friend WithEvents btnFilterClose As System.Windows.Forms.Button
    Friend WithEvents txtInterestRate As System.Windows.Forms.TextBox
End Class
