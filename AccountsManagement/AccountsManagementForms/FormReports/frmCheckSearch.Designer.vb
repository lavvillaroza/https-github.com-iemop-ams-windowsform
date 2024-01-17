<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCheckSearch
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
        Me.chk_Released = New System.Windows.Forms.CheckBox()
        Me.chk_Cleared = New System.Windows.Forms.CheckBox()
        Me.gBox_Released = New System.Windows.Forms.GroupBox()
        Me.rb_Released = New System.Windows.Forms.RadioButton()
        Me.rb_Unreleased = New System.Windows.Forms.RadioButton()
        Me.gbox_Cleared = New System.Windows.Forms.GroupBox()
        Me.rb_UnCleared = New System.Windows.Forms.RadioButton()
        Me.rb_Cleared = New System.Windows.Forms.RadioButton()
        Me.chkbox_Status = New System.Windows.Forms.CheckBox()
        Me.gBox_Status = New System.Windows.Forms.GroupBox()
        Me.chk_Status6 = New System.Windows.Forms.CheckBox()
        Me.chk_Status5 = New System.Windows.Forms.CheckBox()
        Me.chk_Status4 = New System.Windows.Forms.CheckBox()
        Me.chk_Status2 = New System.Windows.Forms.CheckBox()
        Me.chk_Status3 = New System.Windows.Forms.CheckBox()
        Me.chk_Status1 = New System.Windows.Forms.CheckBox()
        Me.chkbox_EnablePID = New System.Windows.Forms.CheckBox()
        Me.chkbox_EnableDate = New System.Windows.Forms.CheckBox()
        Me.gBox_ParticipantID = New System.Windows.Forms.GroupBox()
        Me.cbo_Participants = New System.Windows.Forms.ComboBox()
        Me.cmd_close = New System.Windows.Forms.Button()
        Me.gBox_TransactionDate = New System.Windows.Forms.GroupBox()
        Me.rb_TransDate = New System.Windows.Forms.RadioButton()
        Me.rb_AllocDate = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtp_From = New System.Windows.Forms.DateTimePicker()
        Me.dtp_To = New System.Windows.Forms.DateTimePicker()
        Me.cmd_Search = New System.Windows.Forms.Button()
        Me.chkbox_EnableNumber = New System.Windows.Forms.CheckBox()
        Me.gBox_Number = New System.Windows.Forms.GroupBox()
        Me.rb_VoucherNo = New System.Windows.Forms.RadioButton()
        Me.rb_CheckNo = New System.Windows.Forms.RadioButton()
        Me.txt_Number = New System.Windows.Forms.TextBox()
        Me.gBox_Released.SuspendLayout()
        Me.gbox_Cleared.SuspendLayout()
        Me.gBox_Status.SuspendLayout()
        Me.gBox_ParticipantID.SuspendLayout()
        Me.gBox_TransactionDate.SuspendLayout()
        Me.gBox_Number.SuspendLayout()
        Me.SuspendLayout()
        '
        'chk_Released
        '
        Me.chk_Released.AutoSize = True
        Me.chk_Released.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_Released.Location = New System.Drawing.Point(285, 217)
        Me.chk_Released.Name = "chk_Released"
        Me.chk_Released.Size = New System.Drawing.Size(174, 18)
        Me.chk_Released.TabIndex = 23
        Me.chk_Released.Text = "Released / Unreleased Checks"
        Me.chk_Released.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chk_Released.UseVisualStyleBackColor = True
        '
        'chk_Cleared
        '
        Me.chk_Cleared.AutoSize = True
        Me.chk_Cleared.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_Cleared.Location = New System.Drawing.Point(11, 217)
        Me.chk_Cleared.Name = "chk_Cleared"
        Me.chk_Cleared.Size = New System.Drawing.Size(160, 18)
        Me.chk_Cleared.TabIndex = 22
        Me.chk_Cleared.Text = "Cleared / Uncleared Checks"
        Me.chk_Cleared.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chk_Cleared.UseVisualStyleBackColor = True
        '
        'gBox_Released
        '
        Me.gBox_Released.Controls.Add(Me.rb_Released)
        Me.gBox_Released.Controls.Add(Me.rb_Unreleased)
        Me.gBox_Released.Enabled = False
        Me.gBox_Released.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gBox_Released.Location = New System.Drawing.Point(285, 232)
        Me.gBox_Released.Name = "gBox_Released"
        Me.gBox_Released.Size = New System.Drawing.Size(268, 50)
        Me.gBox_Released.TabIndex = 21
        Me.gBox_Released.TabStop = False
        '
        'rb_Released
        '
        Me.rb_Released.AutoSize = True
        Me.rb_Released.Checked = True
        Me.rb_Released.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_Released.Location = New System.Drawing.Point(6, 21)
        Me.rb_Released.Name = "rb_Released"
        Me.rb_Released.Size = New System.Drawing.Size(103, 18)
        Me.rb_Released.TabIndex = 9
        Me.rb_Released.TabStop = True
        Me.rb_Released.Text = "Released Check"
        Me.rb_Released.UseVisualStyleBackColor = True
        '
        'rb_Unreleased
        '
        Me.rb_Unreleased.AutoSize = True
        Me.rb_Unreleased.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_Unreleased.Location = New System.Drawing.Point(134, 21)
        Me.rb_Unreleased.Name = "rb_Unreleased"
        Me.rb_Unreleased.Size = New System.Drawing.Size(113, 18)
        Me.rb_Unreleased.TabIndex = 10
        Me.rb_Unreleased.Text = "Unreleased Check"
        Me.rb_Unreleased.UseVisualStyleBackColor = True
        '
        'gbox_Cleared
        '
        Me.gbox_Cleared.Controls.Add(Me.rb_UnCleared)
        Me.gbox_Cleared.Controls.Add(Me.rb_Cleared)
        Me.gbox_Cleared.Enabled = False
        Me.gbox_Cleared.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbox_Cleared.Location = New System.Drawing.Point(12, 232)
        Me.gbox_Cleared.Name = "gbox_Cleared"
        Me.gbox_Cleared.Size = New System.Drawing.Size(267, 50)
        Me.gbox_Cleared.TabIndex = 20
        Me.gbox_Cleared.TabStop = False
        '
        'rb_UnCleared
        '
        Me.rb_UnCleared.AutoSize = True
        Me.rb_UnCleared.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_UnCleared.Location = New System.Drawing.Point(134, 21)
        Me.rb_UnCleared.Name = "rb_UnCleared"
        Me.rb_UnCleared.Size = New System.Drawing.Size(113, 18)
        Me.rb_UnCleared.TabIndex = 9
        Me.rb_UnCleared.Text = "Uncleared Checks"
        Me.rb_UnCleared.UseVisualStyleBackColor = True
        '
        'rb_Cleared
        '
        Me.rb_Cleared.AutoSize = True
        Me.rb_Cleared.Checked = True
        Me.rb_Cleared.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_Cleared.Location = New System.Drawing.Point(6, 21)
        Me.rb_Cleared.Name = "rb_Cleared"
        Me.rb_Cleared.Size = New System.Drawing.Size(101, 18)
        Me.rb_Cleared.TabIndex = 10
        Me.rb_Cleared.TabStop = True
        Me.rb_Cleared.Text = "Cleared Checks"
        Me.rb_Cleared.UseVisualStyleBackColor = True
        '
        'chkbox_Status
        '
        Me.chkbox_Status.AutoSize = True
        Me.chkbox_Status.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbox_Status.Location = New System.Drawing.Point(285, 97)
        Me.chkbox_Status.Name = "chkbox_Status"
        Me.chkbox_Status.Size = New System.Drawing.Size(57, 18)
        Me.chkbox_Status.TabIndex = 18
        Me.chkbox_Status.Text = "Status"
        Me.chkbox_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkbox_Status.UseVisualStyleBackColor = True
        '
        'gBox_Status
        '
        Me.gBox_Status.Controls.Add(Me.chk_Status6)
        Me.gBox_Status.Controls.Add(Me.chk_Status5)
        Me.gBox_Status.Controls.Add(Me.chk_Status4)
        Me.gBox_Status.Controls.Add(Me.chk_Status2)
        Me.gBox_Status.Controls.Add(Me.chk_Status3)
        Me.gBox_Status.Controls.Add(Me.chk_Status1)
        Me.gBox_Status.Enabled = False
        Me.gBox_Status.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gBox_Status.Location = New System.Drawing.Point(285, 116)
        Me.gBox_Status.Name = "gBox_Status"
        Me.gBox_Status.Size = New System.Drawing.Size(268, 95)
        Me.gBox_Status.TabIndex = 19
        Me.gBox_Status.TabStop = False
        '
        'chk_Status6
        '
        Me.chk_Status6.AutoSize = True
        Me.chk_Status6.Checked = True
        Me.chk_Status6.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_Status6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_Status6.Location = New System.Drawing.Point(153, 73)
        Me.chk_Status6.Name = "chk_Status6"
        Me.chk_Status6.Size = New System.Drawing.Size(86, 18)
        Me.chk_Status6.TabIndex = 5
        Me.chk_Status6.Text = "Manual Input"
        Me.chk_Status6.UseVisualStyleBackColor = True
        Me.chk_Status6.Visible = False
        '
        'chk_Status5
        '
        Me.chk_Status5.AutoSize = True
        Me.chk_Status5.Checked = True
        Me.chk_Status5.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_Status5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_Status5.Location = New System.Drawing.Point(153, 43)
        Me.chk_Status5.Name = "chk_Status5"
        Me.chk_Status5.Size = New System.Drawing.Size(112, 32)
        Me.chk_Status5.TabIndex = 4
        Me.chk_Status5.Text = "Replacement For " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Cancelled Checks" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.chk_Status5.UseVisualStyleBackColor = True
        '
        'chk_Status4
        '
        Me.chk_Status4.AutoSize = True
        Me.chk_Status4.Checked = True
        Me.chk_Status4.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_Status4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_Status4.Location = New System.Drawing.Point(153, 21)
        Me.chk_Status4.Name = "chk_Status4"
        Me.chk_Status4.Size = New System.Drawing.Size(73, 18)
        Me.chk_Status4.TabIndex = 3
        Me.chk_Status4.Text = "Cancelled"
        Me.chk_Status4.UseVisualStyleBackColor = True
        '
        'chk_Status2
        '
        Me.chk_Status2.AutoSize = True
        Me.chk_Status2.Checked = True
        Me.chk_Status2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_Status2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_Status2.Location = New System.Drawing.Point(6, 43)
        Me.chk_Status2.Name = "chk_Status2"
        Me.chk_Status2.Size = New System.Drawing.Size(110, 18)
        Me.chk_Status2.TabIndex = 2
        Me.chk_Status2.Text = "Generated Check"
        Me.chk_Status2.UseVisualStyleBackColor = True
        '
        'chk_Status3
        '
        Me.chk_Status3.AutoSize = True
        Me.chk_Status3.Checked = True
        Me.chk_Status3.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_Status3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_Status3.Location = New System.Drawing.Point(6, 73)
        Me.chk_Status3.Name = "chk_Status3"
        Me.chk_Status3.Size = New System.Drawing.Size(59, 18)
        Me.chk_Status3.TabIndex = 1
        Me.chk_Status3.Text = "Printed"
        Me.chk_Status3.UseVisualStyleBackColor = True
        Me.chk_Status3.Visible = False
        '
        'chk_Status1
        '
        Me.chk_Status1.AutoSize = True
        Me.chk_Status1.Checked = True
        Me.chk_Status1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_Status1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_Status1.Location = New System.Drawing.Point(6, 21)
        Me.chk_Status1.Name = "chk_Status1"
        Me.chk_Status1.Size = New System.Drawing.Size(116, 18)
        Me.chk_Status1.TabIndex = 0
        Me.chk_Status1.Text = "System Generated"
        Me.chk_Status1.UseVisualStyleBackColor = True
        '
        'chkbox_EnablePID
        '
        Me.chkbox_EnablePID.AutoSize = True
        Me.chkbox_EnablePID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbox_EnablePID.Location = New System.Drawing.Point(285, 12)
        Me.chkbox_EnablePID.Name = "chkbox_EnablePID"
        Me.chkbox_EnablePID.Size = New System.Drawing.Size(88, 18)
        Me.chkbox_EnablePID.TabIndex = 16
        Me.chkbox_EnablePID.Text = "Participant ID"
        Me.chkbox_EnablePID.UseVisualStyleBackColor = True
        '
        'chkbox_EnableDate
        '
        Me.chkbox_EnableDate.AutoSize = True
        Me.chkbox_EnableDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbox_EnableDate.Location = New System.Drawing.Point(12, 12)
        Me.chkbox_EnableDate.Name = "chkbox_EnableDate"
        Me.chkbox_EnableDate.Size = New System.Drawing.Size(108, 18)
        Me.chkbox_EnableDate.TabIndex = 10
        Me.chkbox_EnableDate.Text = "Transaction Date"
        Me.chkbox_EnableDate.UseVisualStyleBackColor = True
        '
        'gBox_ParticipantID
        '
        Me.gBox_ParticipantID.Controls.Add(Me.cbo_Participants)
        Me.gBox_ParticipantID.Enabled = False
        Me.gBox_ParticipantID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gBox_ParticipantID.Location = New System.Drawing.Point(285, 25)
        Me.gBox_ParticipantID.Name = "gBox_ParticipantID"
        Me.gBox_ParticipantID.Size = New System.Drawing.Size(268, 66)
        Me.gBox_ParticipantID.TabIndex = 11
        Me.gBox_ParticipantID.TabStop = False
        '
        'cbo_Participants
        '
        Me.cbo_Participants.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_Participants.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_Participants.FormattingEnabled = True
        Me.cbo_Participants.Location = New System.Drawing.Point(6, 21)
        Me.cbo_Participants.Name = "cbo_Participants"
        Me.cbo_Participants.Size = New System.Drawing.Size(256, 22)
        Me.cbo_Participants.TabIndex = 4
        '
        'cmd_close
        '
        Me.cmd_close.BackColor = System.Drawing.Color.White
        Me.cmd_close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_close.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_close.Location = New System.Drawing.Point(403, 288)
        Me.cmd_close.Name = "cmd_close"
        Me.cmd_close.Size = New System.Drawing.Size(150, 39)
        Me.cmd_close.TabIndex = 12
        Me.cmd_close.Text = "Close"
        Me.cmd_close.UseVisualStyleBackColor = False
        '
        'gBox_TransactionDate
        '
        Me.gBox_TransactionDate.Controls.Add(Me.rb_TransDate)
        Me.gBox_TransactionDate.Controls.Add(Me.rb_AllocDate)
        Me.gBox_TransactionDate.Controls.Add(Me.Label1)
        Me.gBox_TransactionDate.Controls.Add(Me.dtp_From)
        Me.gBox_TransactionDate.Controls.Add(Me.dtp_To)
        Me.gBox_TransactionDate.Enabled = False
        Me.gBox_TransactionDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gBox_TransactionDate.Location = New System.Drawing.Point(11, 25)
        Me.gBox_TransactionDate.Name = "gBox_TransactionDate"
        Me.gBox_TransactionDate.Size = New System.Drawing.Size(268, 66)
        Me.gBox_TransactionDate.TabIndex = 2
        Me.gBox_TransactionDate.TabStop = False
        '
        'rb_TransDate
        '
        Me.rb_TransDate.AutoSize = True
        Me.rb_TransDate.Checked = True
        Me.rb_TransDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_TransDate.Location = New System.Drawing.Point(6, 13)
        Me.rb_TransDate.Name = "rb_TransDate"
        Me.rb_TransDate.Size = New System.Drawing.Size(107, 18)
        Me.rb_TransDate.TabIndex = 9
        Me.rb_TransDate.TabStop = True
        Me.rb_TransDate.Text = "Transaction Date"
        Me.rb_TransDate.UseVisualStyleBackColor = True
        '
        'rb_AllocDate
        '
        Me.rb_AllocDate.AutoSize = True
        Me.rb_AllocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_AllocDate.Location = New System.Drawing.Point(130, 13)
        Me.rb_AllocDate.Name = "rb_AllocDate"
        Me.rb_AllocDate.Size = New System.Drawing.Size(97, 18)
        Me.rb_AllocDate.TabIndex = 10
        Me.rb_AllocDate.Text = "Allocation Date"
        Me.rb_AllocDate.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(112, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(22, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "To"
        '
        'dtp_From
        '
        Me.dtp_From.CustomFormat = ""
        Me.dtp_From.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_From.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_From.Location = New System.Drawing.Point(6, 35)
        Me.dtp_From.Name = "dtp_From"
        Me.dtp_From.Size = New System.Drawing.Size(91, 20)
        Me.dtp_From.TabIndex = 11
        '
        'dtp_To
        '
        Me.dtp_To.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_To.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_To.Location = New System.Drawing.Point(151, 35)
        Me.dtp_To.Name = "dtp_To"
        Me.dtp_To.Size = New System.Drawing.Size(97, 20)
        Me.dtp_To.TabIndex = 12
        '
        'cmd_Search
        '
        Me.cmd_Search.BackColor = System.Drawing.Color.White
        Me.cmd_Search.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Search.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Search.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Search.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Search.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.cmd_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Search.Location = New System.Drawing.Point(247, 288)
        Me.cmd_Search.Name = "cmd_Search"
        Me.cmd_Search.Size = New System.Drawing.Size(150, 39)
        Me.cmd_Search.TabIndex = 9
        Me.cmd_Search.Text = "Search"
        Me.cmd_Search.UseVisualStyleBackColor = False
        '
        'chkbox_EnableNumber
        '
        Me.chkbox_EnableNumber.AutoSize = True
        Me.chkbox_EnableNumber.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbox_EnableNumber.Location = New System.Drawing.Point(12, 97)
        Me.chkbox_EnableNumber.Name = "chkbox_EnableNumber"
        Me.chkbox_EnableNumber.Size = New System.Drawing.Size(140, 18)
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
        Me.gBox_Number.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gBox_Number.Location = New System.Drawing.Point(11, 116)
        Me.gBox_Number.Name = "gBox_Number"
        Me.gBox_Number.Size = New System.Drawing.Size(268, 95)
        Me.gBox_Number.TabIndex = 1
        Me.gBox_Number.TabStop = False
        '
        'rb_VoucherNo
        '
        Me.rb_VoucherNo.AutoSize = True
        Me.rb_VoucherNo.Checked = True
        Me.rb_VoucherNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_VoucherNo.Location = New System.Drawing.Point(5, 19)
        Me.rb_VoucherNo.Name = "rb_VoucherNo"
        Me.rb_VoucherNo.Size = New System.Drawing.Size(106, 18)
        Me.rb_VoucherNo.TabIndex = 6
        Me.rb_VoucherNo.TabStop = True
        Me.rb_VoucherNo.Text = "Voucher Number"
        Me.rb_VoucherNo.UseVisualStyleBackColor = True
        '
        'rb_CheckNo
        '
        Me.rb_CheckNo.AutoSize = True
        Me.rb_CheckNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_CheckNo.Location = New System.Drawing.Point(128, 19)
        Me.rb_CheckNo.Name = "rb_CheckNo"
        Me.rb_CheckNo.Size = New System.Drawing.Size(95, 18)
        Me.rb_CheckNo.TabIndex = 7
        Me.rb_CheckNo.Text = "Check Number"
        Me.rb_CheckNo.UseVisualStyleBackColor = True
        '
        'txt_Number
        '
        Me.txt_Number.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Number.Location = New System.Drawing.Point(5, 44)
        Me.txt_Number.Name = "txt_Number"
        Me.txt_Number.Size = New System.Drawing.Size(243, 20)
        Me.txt_Number.TabIndex = 8
        '
        'frmCheckSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(563, 337)
        Me.ControlBox = False
        Me.Controls.Add(Me.chk_Released)
        Me.Controls.Add(Me.chk_Cleared)
        Me.Controls.Add(Me.chkbox_EnableDate)
        Me.Controls.Add(Me.gBox_Released)
        Me.Controls.Add(Me.gBox_Number)
        Me.Controls.Add(Me.gbox_Cleared)
        Me.Controls.Add(Me.chkbox_EnableNumber)
        Me.Controls.Add(Me.chkbox_Status)
        Me.Controls.Add(Me.cmd_Search)
        Me.Controls.Add(Me.gBox_Status)
        Me.Controls.Add(Me.gBox_TransactionDate)
        Me.Controls.Add(Me.chkbox_EnablePID)
        Me.Controls.Add(Me.cmd_close)
        Me.Controls.Add(Me.gBox_ParticipantID)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmCheckSearch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Check Search"
        Me.gBox_Released.ResumeLayout(False)
        Me.gBox_Released.PerformLayout()
        Me.gbox_Cleared.ResumeLayout(False)
        Me.gbox_Cleared.PerformLayout()
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
    Friend WithEvents gBox_Released As System.Windows.Forms.GroupBox
    Friend WithEvents rb_Released As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Unreleased As System.Windows.Forms.RadioButton
    Friend WithEvents gbox_Cleared As System.Windows.Forms.GroupBox
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
    Friend WithEvents cmd_close As System.Windows.Forms.Button
    Friend WithEvents gBox_TransactionDate As System.Windows.Forms.GroupBox
    Friend WithEvents rb_TransDate As System.Windows.Forms.RadioButton
    Friend WithEvents rb_AllocDate As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtp_From As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_To As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmd_Search As System.Windows.Forms.Button
    Friend WithEvents chkbox_EnableNumber As System.Windows.Forms.CheckBox
    Friend WithEvents gBox_Number As System.Windows.Forms.GroupBox
    Friend WithEvents rb_VoucherNo As System.Windows.Forms.RadioButton
    Friend WithEvents rb_CheckNo As System.Windows.Forms.RadioButton
    Friend WithEvents txt_Number As System.Windows.Forms.TextBox
    Friend WithEvents chk_Status6 As System.Windows.Forms.CheckBox
    Friend WithEvents chk_Cleared As System.Windows.Forms.CheckBox
    Friend WithEvents chk_Released As System.Windows.Forms.CheckBox
End Class
