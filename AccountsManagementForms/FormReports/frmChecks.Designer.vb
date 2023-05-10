<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChecks
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
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgv_ViewDetails = New System.Windows.Forms.DataGridView()
        Me.batchCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.transDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.participantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colChkBox = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.chNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cvNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.amount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dteReleased = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dtCleared = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Remarks = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chk_allparticipants = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.rb_Released = New System.Windows.Forms.RadioButton()
        Me.rb_Unreleased = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.rb_UnCleared = New System.Windows.Forms.RadioButton()
        Me.rb_Cleared = New System.Windows.Forms.RadioButton()
        Me.chkbox_Status = New System.Windows.Forms.CheckBox()
        Me.gBox_Status = New System.Windows.Forms.GroupBox()
        Me.chk_Status5 = New System.Windows.Forms.CheckBox()
        Me.chk_Status4 = New System.Windows.Forms.CheckBox()
        Me.chk_Status2 = New System.Windows.Forms.CheckBox()
        Me.chk_Status3 = New System.Windows.Forms.CheckBox()
        Me.chk_Status1 = New System.Windows.Forms.CheckBox()
        Me.chkbox_EnablePID = New System.Windows.Forms.CheckBox()
        Me.chkbox_EnableDate = New System.Windows.Forms.CheckBox()
        Me.gBox_ParticipantID = New System.Windows.Forms.GroupBox()
        Me.cbo_Participants = New System.Windows.Forms.ComboBox()
        Me.gBox_TransactionDate = New System.Windows.Forms.GroupBox()
        Me.rb_TransDate = New System.Windows.Forms.RadioButton()
        Me.rb_AllocDate = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtp_From = New System.Windows.Forms.DateTimePicker()
        Me.dtp_To = New System.Windows.Forms.DateTimePicker()
        Me.chkbox_EnableNumber = New System.Windows.Forms.CheckBox()
        Me.gBox_Number = New System.Windows.Forms.GroupBox()
        Me.rb_VoucherNo = New System.Windows.Forms.RadioButton()
        Me.rb_CheckNo = New System.Windows.Forms.RadioButton()
        Me.txt_Number = New System.Windows.Forms.TextBox()
        Me.cmd_NewSequence = New System.Windows.Forms.Button()
        Me.cmd_PrintCheck = New System.Windows.Forms.Button()
        Me.cmd_DateCleared = New System.Windows.Forms.Button()
        Me.cmd_DateReleased = New System.Windows.Forms.Button()
        Me.cmd_CreateCheck = New System.Windows.Forms.Button()
        Me.cmd_OutstandingSummary = New System.Windows.Forms.Button()
        Me.cmd_ChangeCheckNo = New System.Windows.Forms.Button()
        Me.cmd_GenerateRegister = New System.Windows.Forms.Button()
        Me.cmd_Search = New System.Windows.Forms.Button()
        Me.cmd_GenerateCV = New System.Windows.Forms.Button()
        Me.cmd_close = New System.Windows.Forms.Button()
        CType(Me.dgv_ViewDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.gBox_Status.SuspendLayout()
        Me.gBox_ParticipantID.SuspendLayout()
        Me.gBox_TransactionDate.SuspendLayout()
        Me.gBox_Number.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgv_ViewDetails
        '
        Me.dgv_ViewDetails.AllowUserToAddRows = False
        Me.dgv_ViewDetails.AllowUserToDeleteRows = False
        Me.dgv_ViewDetails.AllowUserToOrderColumns = True
        Me.dgv_ViewDetails.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_ViewDetails.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_ViewDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_ViewDetails.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_ViewDetails.ColumnHeadersHeight = 21
        Me.dgv_ViewDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.batchCode, Me.transDate, Me.participantID, Me.Status, Me.colChkBox, Me.chNumber, Me.cvNumber, Me.amount, Me.dteReleased, Me.dtCleared, Me.Remarks})
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_ViewDetails.DefaultCellStyle = DataGridViewCellStyle10
        Me.dgv_ViewDetails.Location = New System.Drawing.Point(274, 34)
        Me.dgv_ViewDetails.MultiSelect = False
        Me.dgv_ViewDetails.Name = "dgv_ViewDetails"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_ViewDetails.RowHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.dgv_ViewDetails.RowHeadersVisible = False
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgv_ViewDetails.RowsDefaultCellStyle = DataGridViewCellStyle12
        Me.dgv_ViewDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_ViewDetails.Size = New System.Drawing.Size(854, 472)
        Me.dgv_ViewDetails.TabIndex = 1
        '
        'batchCode
        '
        Me.batchCode.HeaderText = "BatchCode"
        Me.batchCode.Name = "batchCode"
        Me.batchCode.ReadOnly = True
        Me.batchCode.Width = 84
        '
        'transDate
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Format = "d"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.transDate.DefaultCellStyle = DataGridViewCellStyle3
        Me.transDate.HeaderText = "Transaction Date"
        Me.transDate.Name = "transDate"
        Me.transDate.ReadOnly = True
        Me.transDate.Width = 114
        '
        'participantID
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.participantID.DefaultCellStyle = DataGridViewCellStyle4
        Me.participantID.HeaderText = "Participant ID"
        Me.participantID.Name = "participantID"
        Me.participantID.ReadOnly = True
        Me.participantID.Width = 95
        '
        'Status
        '
        Me.Status.HeaderText = "Status"
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Width = 61
        '
        'colChkBox
        '
        Me.colChkBox.HeaderText = "Generate"
        Me.colChkBox.Name = "colChkBox"
        Me.colChkBox.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colChkBox.Width = 57
        '
        'chNumber
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.chNumber.DefaultCellStyle = DataGridViewCellStyle5
        Me.chNumber.HeaderText = "Check Number"
        Me.chNumber.Name = "chNumber"
        Me.chNumber.ReadOnly = True
        Me.chNumber.Width = 104
        '
        'cvNumber
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.cvNumber.DefaultCellStyle = DataGridViewCellStyle6
        Me.cvNumber.HeaderText = "Voucher Number"
        Me.cvNumber.Name = "cvNumber"
        Me.cvNumber.ReadOnly = True
        Me.cvNumber.Width = 113
        '
        'amount
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.NullValue = "0"
        Me.amount.DefaultCellStyle = DataGridViewCellStyle7
        Me.amount.HeaderText = "Amount"
        Me.amount.Name = "amount"
        Me.amount.ReadOnly = True
        Me.amount.Width = 67
        '
        'dteReleased
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dteReleased.DefaultCellStyle = DataGridViewCellStyle8
        Me.dteReleased.HeaderText = "DateReleased"
        Me.dteReleased.Name = "dteReleased"
        Me.dteReleased.ReadOnly = True
        Me.dteReleased.Width = 99
        '
        'dtCleared
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dtCleared.DefaultCellStyle = DataGridViewCellStyle9
        Me.dtCleared.HeaderText = "DateCleared"
        Me.dtCleared.Name = "dtCleared"
        Me.dtCleared.ReadOnly = True
        Me.dtCleared.Width = 91
        '
        'Remarks
        '
        Me.Remarks.HeaderText = "Remarks"
        Me.Remarks.Name = "Remarks"
        Me.Remarks.ReadOnly = True
        Me.Remarks.Width = 75
        '
        'chk_allparticipants
        '
        Me.chk_allparticipants.AutoSize = True
        Me.chk_allparticipants.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_allparticipants.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.chk_allparticipants.Location = New System.Drawing.Point(274, 13)
        Me.chk_allparticipants.Name = "chk_allparticipants"
        Me.chk_allparticipants.Size = New System.Drawing.Size(141, 18)
        Me.chk_allparticipants.TabIndex = 2
        Me.chk_allparticipants.Text = "Select All Participants"
        Me.chk_allparticipants.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.chkbox_Status)
        Me.GroupBox1.Controls.Add(Me.gBox_Status)
        Me.GroupBox1.Controls.Add(Me.chkbox_EnablePID)
        Me.GroupBox1.Controls.Add(Me.chkbox_EnableDate)
        Me.GroupBox1.Controls.Add(Me.gBox_ParticipantID)
        Me.GroupBox1.Controls.Add(Me.gBox_TransactionDate)
        Me.GroupBox1.Controls.Add(Me.chkbox_EnableNumber)
        Me.GroupBox1.Controls.Add(Me.gBox_Number)
        Me.GroupBox1.Font = New System.Drawing.Font("Helvetica Narrow", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(848, 22)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(279, 393)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Visible = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.rb_Released)
        Me.GroupBox4.Controls.Add(Me.rb_Unreleased)
        Me.GroupBox4.Location = New System.Drawing.Point(5, 428)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(268, 50)
        Me.GroupBox4.TabIndex = 21
        Me.GroupBox4.TabStop = False
        '
        'rb_Released
        '
        Me.rb_Released.AutoSize = True
        Me.rb_Released.Checked = True
        Me.rb_Released.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_Released.Location = New System.Drawing.Point(6, 21)
        Me.rb_Released.Name = "rb_Released"
        Me.rb_Released.Size = New System.Drawing.Size(104, 16)
        Me.rb_Released.TabIndex = 9
        Me.rb_Released.TabStop = True
        Me.rb_Released.Text = "Released Check"
        Me.rb_Released.UseVisualStyleBackColor = True
        '
        'rb_Unreleased
        '
        Me.rb_Unreleased.AutoSize = True
        Me.rb_Unreleased.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_Unreleased.Location = New System.Drawing.Point(134, 21)
        Me.rb_Unreleased.Name = "rb_Unreleased"
        Me.rb_Unreleased.Size = New System.Drawing.Size(114, 16)
        Me.rb_Unreleased.TabIndex = 10
        Me.rb_Unreleased.Text = "Unreleased Check"
        Me.rb_Unreleased.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rb_UnCleared)
        Me.GroupBox3.Controls.Add(Me.rb_Cleared)
        Me.GroupBox3.Location = New System.Drawing.Point(5, 372)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(268, 50)
        Me.GroupBox3.TabIndex = 20
        Me.GroupBox3.TabStop = False
        '
        'rb_UnCleared
        '
        Me.rb_UnCleared.AutoSize = True
        Me.rb_UnCleared.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_UnCleared.Location = New System.Drawing.Point(134, 21)
        Me.rb_UnCleared.Name = "rb_UnCleared"
        Me.rb_UnCleared.Size = New System.Drawing.Size(114, 16)
        Me.rb_UnCleared.TabIndex = 9
        Me.rb_UnCleared.Text = "Uncleared Checks"
        Me.rb_UnCleared.UseVisualStyleBackColor = True
        '
        'rb_Cleared
        '
        Me.rb_Cleared.AutoSize = True
        Me.rb_Cleared.Checked = True
        Me.rb_Cleared.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_Cleared.Location = New System.Drawing.Point(6, 21)
        Me.rb_Cleared.Name = "rb_Cleared"
        Me.rb_Cleared.Size = New System.Drawing.Size(102, 16)
        Me.rb_Cleared.TabIndex = 10
        Me.rb_Cleared.TabStop = True
        Me.rb_Cleared.Text = "Cleared Checks"
        Me.rb_Cleared.UseVisualStyleBackColor = True
        '
        'chkbox_Status
        '
        Me.chkbox_Status.AutoSize = True
        Me.chkbox_Status.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbox_Status.Location = New System.Drawing.Point(5, 266)
        Me.chkbox_Status.Name = "chkbox_Status"
        Me.chkbox_Status.Size = New System.Drawing.Size(55, 16)
        Me.chkbox_Status.TabIndex = 18
        Me.chkbox_Status.Text = "Status"
        Me.chkbox_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkbox_Status.UseVisualStyleBackColor = True
        '
        'gBox_Status
        '
        Me.gBox_Status.Controls.Add(Me.chk_Status5)
        Me.gBox_Status.Controls.Add(Me.chk_Status4)
        Me.gBox_Status.Controls.Add(Me.chk_Status2)
        Me.gBox_Status.Controls.Add(Me.chk_Status3)
        Me.gBox_Status.Controls.Add(Me.chk_Status1)
        Me.gBox_Status.Enabled = False
        Me.gBox_Status.Location = New System.Drawing.Point(5, 278)
        Me.gBox_Status.Name = "gBox_Status"
        Me.gBox_Status.Size = New System.Drawing.Size(268, 88)
        Me.gBox_Status.TabIndex = 19
        Me.gBox_Status.TabStop = False
        '
        'chk_Status5
        '
        Me.chk_Status5.AutoSize = True
        Me.chk_Status5.Checked = True
        Me.chk_Status5.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_Status5.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_Status5.Location = New System.Drawing.Point(6, 65)
        Me.chk_Status5.Name = "chk_Status5"
        Me.chk_Status5.Size = New System.Drawing.Size(171, 16)
        Me.chk_Status5.TabIndex = 4
        Me.chk_Status5.Text = "Manual Input (Check Number)"
        Me.chk_Status5.UseVisualStyleBackColor = True
        '
        'chk_Status4
        '
        Me.chk_Status4.AutoSize = True
        Me.chk_Status4.Checked = True
        Me.chk_Status4.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_Status4.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_Status4.Location = New System.Drawing.Point(153, 21)
        Me.chk_Status4.Name = "chk_Status4"
        Me.chk_Status4.Size = New System.Drawing.Size(72, 16)
        Me.chk_Status4.TabIndex = 3
        Me.chk_Status4.Text = "Cancelled"
        Me.chk_Status4.UseVisualStyleBackColor = True
        '
        'chk_Status2
        '
        Me.chk_Status2.AutoSize = True
        Me.chk_Status2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_Status2.Location = New System.Drawing.Point(153, 43)
        Me.chk_Status2.Name = "chk_Status2"
        Me.chk_Status2.Size = New System.Drawing.Size(78, 16)
        Me.chk_Status2.TabIndex = 2
        Me.chk_Status2.Text = "Not Printed"
        Me.chk_Status2.UseVisualStyleBackColor = True
        Me.chk_Status2.Visible = False
        '
        'chk_Status3
        '
        Me.chk_Status3.AutoSize = True
        Me.chk_Status3.Checked = True
        Me.chk_Status3.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_Status3.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_Status3.Location = New System.Drawing.Point(6, 43)
        Me.chk_Status3.Name = "chk_Status3"
        Me.chk_Status3.Size = New System.Drawing.Size(58, 16)
        Me.chk_Status3.TabIndex = 1
        Me.chk_Status3.Text = "Printed"
        Me.chk_Status3.UseVisualStyleBackColor = True
        '
        'chk_Status1
        '
        Me.chk_Status1.AutoSize = True
        Me.chk_Status1.Checked = True
        Me.chk_Status1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_Status1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_Status1.Location = New System.Drawing.Point(6, 21)
        Me.chk_Status1.Name = "chk_Status1"
        Me.chk_Status1.Size = New System.Drawing.Size(116, 16)
        Me.chk_Status1.TabIndex = 0
        Me.chk_Status1.Text = "System Generated"
        Me.chk_Status1.UseVisualStyleBackColor = True
        '
        'chkbox_EnablePID
        '
        Me.chkbox_EnablePID.AutoSize = True
        Me.chkbox_EnablePID.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbox_EnablePID.Location = New System.Drawing.Point(6, 97)
        Me.chkbox_EnablePID.Name = "chkbox_EnablePID"
        Me.chkbox_EnablePID.Size = New System.Drawing.Size(89, 16)
        Me.chkbox_EnablePID.TabIndex = 16
        Me.chkbox_EnablePID.Text = "Participant ID"
        Me.chkbox_EnablePID.UseVisualStyleBackColor = True
        '
        'chkbox_EnableDate
        '
        Me.chkbox_EnableDate.AutoSize = True
        Me.chkbox_EnableDate.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbox_EnableDate.Location = New System.Drawing.Point(6, 12)
        Me.chkbox_EnableDate.Name = "chkbox_EnableDate"
        Me.chkbox_EnableDate.Size = New System.Drawing.Size(108, 16)
        Me.chkbox_EnableDate.TabIndex = 10
        Me.chkbox_EnableDate.Text = "Transaction Date"
        Me.chkbox_EnableDate.UseVisualStyleBackColor = True
        '
        'gBox_ParticipantID
        '
        Me.gBox_ParticipantID.Controls.Add(Me.cbo_Participants)
        Me.gBox_ParticipantID.Enabled = False
        Me.gBox_ParticipantID.Location = New System.Drawing.Point(5, 110)
        Me.gBox_ParticipantID.Name = "gBox_ParticipantID"
        Me.gBox_ParticipantID.Size = New System.Drawing.Size(268, 54)
        Me.gBox_ParticipantID.TabIndex = 11
        Me.gBox_ParticipantID.TabStop = False
        '
        'cbo_Participants
        '
        Me.cbo_Participants.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_Participants.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_Participants.FormattingEnabled = True
        Me.cbo_Participants.Location = New System.Drawing.Point(6, 21)
        Me.cbo_Participants.Name = "cbo_Participants"
        Me.cbo_Participants.Size = New System.Drawing.Size(242, 20)
        Me.cbo_Participants.TabIndex = 4
        '
        'gBox_TransactionDate
        '
        Me.gBox_TransactionDate.Controls.Add(Me.rb_TransDate)
        Me.gBox_TransactionDate.Controls.Add(Me.rb_AllocDate)
        Me.gBox_TransactionDate.Controls.Add(Me.Label1)
        Me.gBox_TransactionDate.Controls.Add(Me.dtp_From)
        Me.gBox_TransactionDate.Controls.Add(Me.dtp_To)
        Me.gBox_TransactionDate.Enabled = False
        Me.gBox_TransactionDate.Location = New System.Drawing.Point(5, 25)
        Me.gBox_TransactionDate.Name = "gBox_TransactionDate"
        Me.gBox_TransactionDate.Size = New System.Drawing.Size(268, 66)
        Me.gBox_TransactionDate.TabIndex = 2
        Me.gBox_TransactionDate.TabStop = False
        '
        'rb_TransDate
        '
        Me.rb_TransDate.AutoSize = True
        Me.rb_TransDate.Checked = True
        Me.rb_TransDate.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_TransDate.Location = New System.Drawing.Point(6, 13)
        Me.rb_TransDate.Name = "rb_TransDate"
        Me.rb_TransDate.Size = New System.Drawing.Size(107, 16)
        Me.rb_TransDate.TabIndex = 9
        Me.rb_TransDate.TabStop = True
        Me.rb_TransDate.Text = "Transaction Date"
        Me.rb_TransDate.UseVisualStyleBackColor = True
        '
        'rb_AllocDate
        '
        Me.rb_AllocDate.AutoSize = True
        Me.rb_AllocDate.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_AllocDate.Location = New System.Drawing.Point(130, 13)
        Me.rb_AllocDate.Name = "rb_AllocDate"
        Me.rb_AllocDate.Size = New System.Drawing.Size(95, 16)
        Me.rb_AllocDate.TabIndex = 10
        Me.rb_AllocDate.Text = "Allocation Date"
        Me.rb_AllocDate.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(112, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(19, 14)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "To"
        '
        'dtp_From
        '
        Me.dtp_From.CustomFormat = ""
        Me.dtp_From.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_From.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_From.Location = New System.Drawing.Point(6, 35)
        Me.dtp_From.Name = "dtp_From"
        Me.dtp_From.Size = New System.Drawing.Size(91, 20)
        Me.dtp_From.TabIndex = 11
        '
        'dtp_To
        '
        Me.dtp_To.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_To.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_To.Location = New System.Drawing.Point(151, 35)
        Me.dtp_To.Name = "dtp_To"
        Me.dtp_To.Size = New System.Drawing.Size(97, 20)
        Me.dtp_To.TabIndex = 12
        '
        'chkbox_EnableNumber
        '
        Me.chkbox_EnableNumber.AutoSize = True
        Me.chkbox_EnableNumber.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbox_EnableNumber.Location = New System.Drawing.Point(6, 170)
        Me.chkbox_EnableNumber.Name = "chkbox_EnableNumber"
        Me.chkbox_EnableNumber.Size = New System.Drawing.Size(142, 16)
        Me.chkbox_EnableNumber.TabIndex = 15
        Me.chkbox_EnableNumber.Text = "Check/Voucher Number"
        Me.chkbox_EnableNumber.UseVisualStyleBackColor = True
        '
        'gBox_Number
        '
        Me.gBox_Number.Controls.Add(Me.rb_VoucherNo)
        Me.gBox_Number.Controls.Add(Me.rb_CheckNo)
        Me.gBox_Number.Controls.Add(Me.txt_Number)
        Me.gBox_Number.Enabled = False
        Me.gBox_Number.Location = New System.Drawing.Point(5, 182)
        Me.gBox_Number.Name = "gBox_Number"
        Me.gBox_Number.Size = New System.Drawing.Size(268, 78)
        Me.gBox_Number.TabIndex = 1
        Me.gBox_Number.TabStop = False
        '
        'rb_VoucherNo
        '
        Me.rb_VoucherNo.AutoSize = True
        Me.rb_VoucherNo.Checked = True
        Me.rb_VoucherNo.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_VoucherNo.Location = New System.Drawing.Point(5, 19)
        Me.rb_VoucherNo.Name = "rb_VoucherNo"
        Me.rb_VoucherNo.Size = New System.Drawing.Size(106, 16)
        Me.rb_VoucherNo.TabIndex = 6
        Me.rb_VoucherNo.TabStop = True
        Me.rb_VoucherNo.Text = "Voucher Number"
        Me.rb_VoucherNo.UseVisualStyleBackColor = True
        '
        'rb_CheckNo
        '
        Me.rb_CheckNo.AutoSize = True
        Me.rb_CheckNo.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_CheckNo.Location = New System.Drawing.Point(128, 19)
        Me.rb_CheckNo.Name = "rb_CheckNo"
        Me.rb_CheckNo.Size = New System.Drawing.Size(97, 16)
        Me.rb_CheckNo.TabIndex = 7
        Me.rb_CheckNo.Text = "Check Number"
        Me.rb_CheckNo.UseVisualStyleBackColor = True
        '
        'txt_Number
        '
        Me.txt_Number.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Number.Location = New System.Drawing.Point(5, 44)
        Me.txt_Number.Name = "txt_Number"
        Me.txt_Number.Size = New System.Drawing.Size(243, 20)
        Me.txt_Number.TabIndex = 8
        '
        'cmd_NewSequence
        '
        Me.cmd_NewSequence.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_NewSequence.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_NewSequence.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_NewSequence.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_NewSequence.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_NewSequence.ForeColor = System.Drawing.Color.Black
        Me.cmd_NewSequence.Image = Global.AccountsManagementForms.My.Resources.Resources.NewGreenIcon22x22
        Me.cmd_NewSequence.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_NewSequence.Location = New System.Drawing.Point(12, 421)
        Me.cmd_NewSequence.Name = "cmd_NewSequence"
        Me.cmd_NewSequence.Size = New System.Drawing.Size(250, 39)
        Me.cmd_NewSequence.TabIndex = 27
        Me.cmd_NewSequence.Text = "Check Sequence"
        Me.cmd_NewSequence.UseVisualStyleBackColor = True
        '
        'cmd_PrintCheck
        '
        Me.cmd_PrintCheck.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_PrintCheck.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_PrintCheck.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_PrintCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_PrintCheck.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_PrintCheck.ForeColor = System.Drawing.Color.Black
        Me.cmd_PrintCheck.Image = Global.AccountsManagementForms.My.Resources.Resources.PrintColoredIcon22x22
        Me.cmd_PrintCheck.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_PrintCheck.Location = New System.Drawing.Point(12, 152)
        Me.cmd_PrintCheck.Name = "cmd_PrintCheck"
        Me.cmd_PrintCheck.Size = New System.Drawing.Size(250, 39)
        Me.cmd_PrintCheck.TabIndex = 26
        Me.cmd_PrintCheck.Text = "Print Check"
        Me.cmd_PrintCheck.UseVisualStyleBackColor = True
        '
        'cmd_DateCleared
        '
        Me.cmd_DateCleared.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_DateCleared.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_DateCleared.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_DateCleared.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_DateCleared.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_DateCleared.ForeColor = System.Drawing.Color.Black
        Me.cmd_DateCleared.Image = Global.AccountsManagementForms.My.Resources.Resources.DateIcon22x22
        Me.cmd_DateCleared.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_DateCleared.Location = New System.Drawing.Point(12, 286)
        Me.cmd_DateCleared.Name = "cmd_DateCleared"
        Me.cmd_DateCleared.Size = New System.Drawing.Size(250, 39)
        Me.cmd_DateCleared.TabIndex = 25
        Me.cmd_DateCleared.Text = "Date Cleared"
        Me.cmd_DateCleared.UseVisualStyleBackColor = True
        '
        'cmd_DateReleased
        '
        Me.cmd_DateReleased.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_DateReleased.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_DateReleased.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_DateReleased.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_DateReleased.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_DateReleased.ForeColor = System.Drawing.Color.Black
        Me.cmd_DateReleased.Image = Global.AccountsManagementForms.My.Resources.Resources.DateIcon22x22
        Me.cmd_DateReleased.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_DateReleased.Location = New System.Drawing.Point(12, 241)
        Me.cmd_DateReleased.Name = "cmd_DateReleased"
        Me.cmd_DateReleased.Size = New System.Drawing.Size(250, 39)
        Me.cmd_DateReleased.TabIndex = 24
        Me.cmd_DateReleased.Text = "Date Released"
        Me.cmd_DateReleased.UseVisualStyleBackColor = True
        '
        'cmd_CreateCheck
        '
        Me.cmd_CreateCheck.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_CreateCheck.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_CreateCheck.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_CreateCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_CreateCheck.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_CreateCheck.ForeColor = System.Drawing.Color.Black
        Me.cmd_CreateCheck.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.cmd_CreateCheck.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_CreateCheck.Location = New System.Drawing.Point(12, 107)
        Me.cmd_CreateCheck.Name = "cmd_CreateCheck"
        Me.cmd_CreateCheck.Size = New System.Drawing.Size(250, 39)
        Me.cmd_CreateCheck.TabIndex = 23
        Me.cmd_CreateCheck.Text = "Generate Check/Check Voucher"
        Me.cmd_CreateCheck.UseVisualStyleBackColor = True
        '
        'cmd_OutstandingSummary
        '
        Me.cmd_OutstandingSummary.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_OutstandingSummary.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_OutstandingSummary.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_OutstandingSummary.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_OutstandingSummary.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_OutstandingSummary.ForeColor = System.Drawing.Color.Black
        Me.cmd_OutstandingSummary.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.cmd_OutstandingSummary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_OutstandingSummary.Location = New System.Drawing.Point(12, 376)
        Me.cmd_OutstandingSummary.Name = "cmd_OutstandingSummary"
        Me.cmd_OutstandingSummary.Size = New System.Drawing.Size(250, 39)
        Me.cmd_OutstandingSummary.TabIndex = 22
        Me.cmd_OutstandingSummary.Text = "     Generate Outstanding Check Summary"
        Me.cmd_OutstandingSummary.UseVisualStyleBackColor = True
        '
        'cmd_ChangeCheckNo
        '
        Me.cmd_ChangeCheckNo.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_ChangeCheckNo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_ChangeCheckNo.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_ChangeCheckNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_ChangeCheckNo.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ChangeCheckNo.ForeColor = System.Drawing.Color.Black
        Me.cmd_ChangeCheckNo.Image = Global.AccountsManagementForms.My.Resources.Resources.CancelIconRed22x22
        Me.cmd_ChangeCheckNo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_ChangeCheckNo.Location = New System.Drawing.Point(12, 62)
        Me.cmd_ChangeCheckNo.Name = "cmd_ChangeCheckNo"
        Me.cmd_ChangeCheckNo.Size = New System.Drawing.Size(250, 39)
        Me.cmd_ChangeCheckNo.TabIndex = 21
        Me.cmd_ChangeCheckNo.Text = "Cancel Check Number"
        Me.cmd_ChangeCheckNo.UseVisualStyleBackColor = True
        '
        'cmd_GenerateRegister
        '
        Me.cmd_GenerateRegister.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_GenerateRegister.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_GenerateRegister.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_GenerateRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_GenerateRegister.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_GenerateRegister.ForeColor = System.Drawing.Color.Black
        Me.cmd_GenerateRegister.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.cmd_GenerateRegister.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_GenerateRegister.Location = New System.Drawing.Point(12, 331)
        Me.cmd_GenerateRegister.Name = "cmd_GenerateRegister"
        Me.cmd_GenerateRegister.Size = New System.Drawing.Size(250, 39)
        Me.cmd_GenerateRegister.TabIndex = 14
        Me.cmd_GenerateRegister.Text = "Generate Check  Register"
        Me.cmd_GenerateRegister.UseVisualStyleBackColor = True
        '
        'cmd_Search
        '
        Me.cmd_Search.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Search.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Search.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Search.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Search.ForeColor = System.Drawing.Color.Black
        Me.cmd_Search.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.cmd_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Search.Location = New System.Drawing.Point(12, 17)
        Me.cmd_Search.Name = "cmd_Search"
        Me.cmd_Search.Size = New System.Drawing.Size(250, 39)
        Me.cmd_Search.TabIndex = 9
        Me.cmd_Search.Text = "Search"
        Me.cmd_Search.UseVisualStyleBackColor = True
        '
        'cmd_GenerateCV
        '
        Me.cmd_GenerateCV.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_GenerateCV.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_GenerateCV.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_GenerateCV.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_GenerateCV.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_GenerateCV.ForeColor = System.Drawing.Color.Black
        Me.cmd_GenerateCV.Image = Global.AccountsManagementForms.My.Resources.Resources.PrintColoredIcon22x22
        Me.cmd_GenerateCV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_GenerateCV.Location = New System.Drawing.Point(12, 197)
        Me.cmd_GenerateCV.Name = "cmd_GenerateCV"
        Me.cmd_GenerateCV.Size = New System.Drawing.Size(250, 39)
        Me.cmd_GenerateCV.TabIndex = 12
        Me.cmd_GenerateCV.Text = "Print Check Voucher"
        Me.cmd_GenerateCV.UseVisualStyleBackColor = True
        '
        'cmd_close
        '
        Me.cmd_close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_close.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_close.ForeColor = System.Drawing.Color.Black
        Me.cmd_close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_close.Location = New System.Drawing.Point(12, 466)
        Me.cmd_close.Name = "cmd_close"
        Me.cmd_close.Size = New System.Drawing.Size(250, 39)
        Me.cmd_close.TabIndex = 12
        Me.cmd_close.Text = "Close"
        Me.cmd_close.UseVisualStyleBackColor = True
        '
        'frmChecks
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1139, 518)
        Me.Controls.Add(Me.dgv_ViewDetails)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.chk_allparticipants)
        Me.Controls.Add(Me.cmd_NewSequence)
        Me.Controls.Add(Me.cmd_PrintCheck)
        Me.Controls.Add(Me.cmd_DateCleared)
        Me.Controls.Add(Me.cmd_DateReleased)
        Me.Controls.Add(Me.cmd_CreateCheck)
        Me.Controls.Add(Me.cmd_OutstandingSummary)
        Me.Controls.Add(Me.cmd_ChangeCheckNo)
        Me.Controls.Add(Me.cmd_GenerateRegister)
        Me.Controls.Add(Me.cmd_Search)
        Me.Controls.Add(Me.cmd_GenerateCV)
        Me.Controls.Add(Me.cmd_close)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmChecks"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Check / Check Voucher / Check Register"
        CType(Me.dgv_ViewDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.gBox_Status.ResumeLayout(False)
        Me.gBox_Status.PerformLayout()
        Me.gBox_ParticipantID.ResumeLayout(False)
        Me.gBox_TransactionDate.ResumeLayout(False)
        Me.gBox_TransactionDate.PerformLayout()
        Me.gBox_Number.ResumeLayout(False)
        Me.gBox_Number.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmd_Search As System.Windows.Forms.Button
    Friend WithEvents dgv_ViewDetails As System.Windows.Forms.DataGridView
    Friend WithEvents cmd_GenerateCV As System.Windows.Forms.Button
    Friend WithEvents cmd_GenerateRegister As System.Windows.Forms.Button
    Friend WithEvents cmd_close As System.Windows.Forms.Button
    Friend WithEvents cmd_ChangeCheckNo As System.Windows.Forms.Button
    Friend WithEvents cmd_OutstandingSummary As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_Released As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Unreleased As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_UnCleared As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Cleared As System.Windows.Forms.RadioButton
    Friend WithEvents chkbox_Status As System.Windows.Forms.CheckBox
    Friend WithEvents gBox_Status As System.Windows.Forms.GroupBox
    Friend WithEvents chk_Status5 As System.Windows.Forms.CheckBox
    Friend WithEvents chk_Status4 As System.Windows.Forms.CheckBox
    Friend WithEvents chk_Status2 As System.Windows.Forms.CheckBox
    Friend WithEvents chk_Status3 As System.Windows.Forms.CheckBox
    Friend WithEvents chk_Status1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_EnablePID As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_EnableDate As System.Windows.Forms.CheckBox
    Friend WithEvents gBox_ParticipantID As System.Windows.Forms.GroupBox
    Friend WithEvents cbo_Participants As System.Windows.Forms.ComboBox
    Friend WithEvents gBox_TransactionDate As System.Windows.Forms.GroupBox
    Friend WithEvents rb_TransDate As System.Windows.Forms.RadioButton
    Friend WithEvents rb_AllocDate As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtp_From As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_To As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkbox_EnableNumber As System.Windows.Forms.CheckBox
    Friend WithEvents gBox_Number As System.Windows.Forms.GroupBox
    Friend WithEvents rb_VoucherNo As System.Windows.Forms.RadioButton
    Friend WithEvents rb_CheckNo As System.Windows.Forms.RadioButton
    Friend WithEvents txt_Number As System.Windows.Forms.TextBox
    Friend WithEvents cmd_CreateCheck As System.Windows.Forms.Button
    Friend WithEvents cmd_DateReleased As System.Windows.Forms.Button
    Friend WithEvents cmd_DateCleared As System.Windows.Forms.Button
    Friend WithEvents chk_allparticipants As System.Windows.Forms.CheckBox
    Friend WithEvents cmd_PrintCheck As System.Windows.Forms.Button
    Friend WithEvents cmd_NewSequence As System.Windows.Forms.Button
    Friend WithEvents batchCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents transDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents participantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colChkBox As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents chNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cvNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents amount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dteReleased As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dtCleared As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Remarks As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
