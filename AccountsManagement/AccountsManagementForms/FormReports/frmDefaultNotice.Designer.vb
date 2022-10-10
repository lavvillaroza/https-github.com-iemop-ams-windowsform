<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDefaultNotice
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDefaultNotice))
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.gBox_Participants = New System.Windows.Forms.GroupBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.cbox_AllParticipants = New System.Windows.Forms.CheckBox()
        Me.dgv_ViewDetails = New System.Windows.Forms.DataGridView()
        Me.tDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GrandTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cnNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chkReport = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cmd_close = New System.Windows.Forms.Button()
        Me.cmd_GenerateDefault = New System.Windows.Forms.Button()
        Me.gBox_Filters = New System.Windows.Forms.GroupBox()
        Me.cmb_TransactionDate = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cBox_Participant = New System.Windows.Forms.CheckBox()
        Me.cbox_TransDate = New System.Windows.Forms.CheckBox()
        Me.cmd_Search = New System.Windows.Forms.Button()
        Me.cbo_ParticipantID = New System.Windows.Forms.ComboBox()
        Me.cmd_GenSave = New System.Windows.Forms.Button()
        Me.dtp_TransactionDate = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.gBox_Participants.SuspendLayout()
        CType(Me.dgv_ViewDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gBox_Filters.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'gBox_Participants
        '
        Me.gBox_Participants.Controls.Add(Me.btnExport)
        Me.gBox_Participants.Controls.Add(Me.cbox_AllParticipants)
        Me.gBox_Participants.Controls.Add(Me.dgv_ViewDetails)
        Me.gBox_Participants.Controls.Add(Me.cmd_close)
        Me.gBox_Participants.Controls.Add(Me.cmd_GenerateDefault)
        Me.gBox_Participants.Location = New System.Drawing.Point(12, 111)
        Me.gBox_Participants.Name = "gBox_Participants"
        Me.gBox_Participants.Size = New System.Drawing.Size(889, 474)
        Me.gBox_Participants.TabIndex = 2
        Me.gBox_Participants.TabStop = False
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnExport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExport.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExport.ForeColor = System.Drawing.Color.Black
        Me.btnExport.Image = CType(resources.GetObject("btnExport.Image"), System.Drawing.Image)
        Me.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExport.Location = New System.Drawing.Point(330, 416)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(200, 39)
        Me.btnExport.TabIndex = 5
        Me.btnExport.Text = "Export Default Notice"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'cbox_AllParticipants
        '
        Me.cbox_AllParticipants.AutoSize = True
        Me.cbox_AllParticipants.Location = New System.Drawing.Point(9, 19)
        Me.cbox_AllParticipants.Name = "cbox_AllParticipants"
        Me.cbox_AllParticipants.Size = New System.Drawing.Size(129, 17)
        Me.cbox_AllParticipants.TabIndex = 4
        Me.cbox_AllParticipants.Text = "Check All Participants"
        Me.cbox_AllParticipants.UseVisualStyleBackColor = True
        '
        'dgv_ViewDetails
        '
        Me.dgv_ViewDetails.AllowUserToAddRows = False
        Me.dgv_ViewDetails.AllowUserToDeleteRows = False
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_ViewDetails.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        Me.dgv_ViewDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_ViewDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_ViewDetails.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.dgv_ViewDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_ViewDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.tDate, Me.IDNumber, Me.ParticipantID, Me.GrandTotal, Me.cnNumber, Me.chkReport})
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_ViewDetails.DefaultCellStyle = DataGridViewCellStyle12
        Me.dgv_ViewDetails.Location = New System.Drawing.Point(6, 42)
        Me.dgv_ViewDetails.Name = "dgv_ViewDetails"
        Me.dgv_ViewDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_ViewDetails.Size = New System.Drawing.Size(874, 368)
        Me.dgv_ViewDetails.TabIndex = 0
        '
        'tDate
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.Format = "d"
        DataGridViewCellStyle9.NullValue = Nothing
        Me.tDate.DefaultCellStyle = DataGridViewCellStyle9
        Me.tDate.HeaderText = "Transaction Date"
        Me.tDate.Name = "tDate"
        Me.tDate.ReadOnly = True
        Me.tDate.Width = 104
        '
        'IDNumber
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.IDNumber.DefaultCellStyle = DataGridViewCellStyle10
        Me.IDNumber.HeaderText = "ID Number"
        Me.IDNumber.Name = "IDNumber"
        Me.IDNumber.ReadOnly = True
        Me.IDNumber.Width = 77
        '
        'ParticipantID
        '
        Me.ParticipantID.HeaderText = "Participant ID"
        Me.ParticipantID.Name = "ParticipantID"
        Me.ParticipantID.ReadOnly = True
        Me.ParticipantID.Width = 88
        '
        'GrandTotal
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.NullValue = "0.00"
        Me.GrandTotal.DefaultCellStyle = DataGridViewCellStyle11
        Me.GrandTotal.HeaderText = "Grand Total"
        Me.GrandTotal.Name = "GrandTotal"
        Me.GrandTotal.ReadOnly = True
        Me.GrandTotal.Width = 81
        '
        'cnNumber
        '
        Me.cnNumber.HeaderText = "CollectionNotice Number"
        Me.cnNumber.Name = "cnNumber"
        Me.cnNumber.Visible = False
        Me.cnNumber.Width = 137
        '
        'chkReport
        '
        Me.chkReport.HeaderText = "Display Report"
        Me.chkReport.Name = "chkReport"
        Me.chkReport.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.chkReport.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.chkReport.Width = 94
        '
        'cmd_close
        '
        Me.cmd_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_close.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_close.ForeColor = System.Drawing.Color.Black
        Me.cmd_close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_close.Location = New System.Drawing.Point(742, 416)
        Me.cmd_close.Name = "cmd_close"
        Me.cmd_close.Size = New System.Drawing.Size(138, 39)
        Me.cmd_close.TabIndex = 1
        Me.cmd_close.Text = "Close"
        Me.cmd_close.UseVisualStyleBackColor = True
        '
        'cmd_GenerateDefault
        '
        Me.cmd_GenerateDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_GenerateDefault.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_GenerateDefault.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_GenerateDefault.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_GenerateDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_GenerateDefault.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_GenerateDefault.ForeColor = System.Drawing.Color.Black
        Me.cmd_GenerateDefault.Image = CType(resources.GetObject("cmd_GenerateDefault.Image"), System.Drawing.Image)
        Me.cmd_GenerateDefault.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_GenerateDefault.Location = New System.Drawing.Point(536, 416)
        Me.cmd_GenerateDefault.Name = "cmd_GenerateDefault"
        Me.cmd_GenerateDefault.Size = New System.Drawing.Size(200, 39)
        Me.cmd_GenerateDefault.TabIndex = 0
        Me.cmd_GenerateDefault.Text = "Generate Default Notice"
        Me.cmd_GenerateDefault.UseVisualStyleBackColor = True
        '
        'gBox_Filters
        '
        Me.gBox_Filters.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gBox_Filters.Controls.Add(Me.cmb_TransactionDate)
        Me.gBox_Filters.Controls.Add(Me.Label1)
        Me.gBox_Filters.Controls.Add(Me.cBox_Participant)
        Me.gBox_Filters.Controls.Add(Me.cbox_TransDate)
        Me.gBox_Filters.Controls.Add(Me.cmd_Search)
        Me.gBox_Filters.Controls.Add(Me.cbo_ParticipantID)
        Me.gBox_Filters.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gBox_Filters.ForeColor = System.Drawing.Color.Black
        Me.gBox_Filters.Location = New System.Drawing.Point(12, 12)
        Me.gBox_Filters.Name = "gBox_Filters"
        Me.gBox_Filters.Size = New System.Drawing.Size(438, 93)
        Me.gBox_Filters.TabIndex = 3
        Me.gBox_Filters.TabStop = False
        '
        'cmb_TransactionDate
        '
        Me.cmb_TransactionDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_TransactionDate.Enabled = False
        Me.cmb_TransactionDate.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_TransactionDate.FormattingEnabled = True
        Me.cmb_TransactionDate.Location = New System.Drawing.Point(131, 17)
        Me.cmb_TransactionDate.Name = "cmb_TransactionDate"
        Me.cmb_TransactionDate.Size = New System.Drawing.Size(165, 20)
        Me.cmb_TransactionDate.TabIndex = 26
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 14)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Search Filters:"
        '
        'cBox_Participant
        '
        Me.cBox_Participant.AutoSize = True
        Me.cBox_Participant.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cBox_Participant.ForeColor = System.Drawing.Color.Black
        Me.cBox_Participant.Location = New System.Drawing.Point(9, 52)
        Me.cBox_Participant.Name = "cBox_Participant"
        Me.cBox_Participant.Size = New System.Drawing.Size(83, 18)
        Me.cBox_Participant.TabIndex = 9
        Me.cBox_Participant.Text = "Participant"
        Me.cBox_Participant.UseVisualStyleBackColor = True
        '
        'cbox_TransDate
        '
        Me.cbox_TransDate.AutoSize = True
        Me.cbox_TransDate.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbox_TransDate.ForeColor = System.Drawing.Color.Black
        Me.cbox_TransDate.Location = New System.Drawing.Point(9, 19)
        Me.cbox_TransDate.Name = "cbox_TransDate"
        Me.cbox_TransDate.Size = New System.Drawing.Size(116, 18)
        Me.cbox_TransDate.TabIndex = 8
        Me.cbox_TransDate.Text = "Transaction Date"
        Me.cbox_TransDate.UseVisualStyleBackColor = True
        '
        'cmd_Search
        '
        Me.cmd_Search.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_Search.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Search.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Search.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Search.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Search.ForeColor = System.Drawing.Color.Black
        Me.cmd_Search.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.cmd_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Search.Location = New System.Drawing.Point(303, 29)
        Me.cmd_Search.Name = "cmd_Search"
        Me.cmd_Search.Size = New System.Drawing.Size(129, 39)
        Me.cmd_Search.TabIndex = 4
        Me.cmd_Search.Text = "Search"
        Me.cmd_Search.UseVisualStyleBackColor = True
        '
        'cbo_ParticipantID
        '
        Me.cbo_ParticipantID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_ParticipantID.Enabled = False
        Me.cbo_ParticipantID.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_ParticipantID.FormattingEnabled = True
        Me.cbo_ParticipantID.Location = New System.Drawing.Point(131, 52)
        Me.cbo_ParticipantID.Name = "cbo_ParticipantID"
        Me.cbo_ParticipantID.Size = New System.Drawing.Size(165, 20)
        Me.cbo_ParticipantID.TabIndex = 1
        '
        'cmd_GenSave
        '
        Me.cmd_GenSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_GenSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_GenSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_GenSave.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_GenSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_GenSave.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_GenSave.ForeColor = System.Drawing.Color.Black
        Me.cmd_GenSave.Image = Global.AccountsManagementForms.My.Resources.Resources.SaveIconColored22x22
        Me.cmd_GenSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_GenSave.Location = New System.Drawing.Point(301, 31)
        Me.cmd_GenSave.Name = "cmd_GenSave"
        Me.cmd_GenSave.Size = New System.Drawing.Size(138, 39)
        Me.cmd_GenSave.TabIndex = 7
        Me.cmd_GenSave.Text = "      Generate Report"
        Me.cmd_GenSave.UseVisualStyleBackColor = True
        '
        'dtp_TransactionDate
        '
        Me.dtp_TransactionDate.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_TransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_TransactionDate.Location = New System.Drawing.Point(129, 38)
        Me.dtp_TransactionDate.Name = "dtp_TransactionDate"
        Me.dtp_TransactionDate.Size = New System.Drawing.Size(166, 20)
        Me.dtp_TransactionDate.TabIndex = 34
        Me.dtp_TransactionDate.Value = New Date(2021, 9, 28, 0, 0, 0, 0)
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(6, 40)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(109, 15)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "Transaction Date:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.dtp_TransactionDate)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.cmd_GenSave)
        Me.GroupBox1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(456, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(445, 93)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(6, -3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(148, 14)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "Creation of Default Notice:"
        '
        'frmDefaultNotice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(913, 596)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.gBox_Filters)
        Me.Controls.Add(Me.gBox_Participants)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(929, 614)
        Me.Name = "frmDefaultNotice"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Default Notice"
        Me.gBox_Participants.ResumeLayout(False)
        Me.gBox_Participants.PerformLayout()
        CType(Me.dgv_ViewDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gBox_Filters.ResumeLayout(False)
        Me.gBox_Filters.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmd_GenerateDefault As System.Windows.Forms.Button
    Friend WithEvents cmd_close As System.Windows.Forms.Button
    Friend WithEvents gBox_Participants As System.Windows.Forms.GroupBox
    Friend WithEvents gBox_Filters As System.Windows.Forms.GroupBox
    Friend WithEvents cbo_ParticipantID As System.Windows.Forms.ComboBox
    Friend WithEvents dgv_ViewDetails As System.Windows.Forms.DataGridView
    Friend WithEvents cmd_Search As System.Windows.Forms.Button
    Friend WithEvents cmd_GenSave As System.Windows.Forms.Button
    Friend WithEvents cbox_TransDate As System.Windows.Forms.CheckBox
    Friend WithEvents cBox_Participant As System.Windows.Forms.CheckBox
    Friend WithEvents tDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GrandTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cnNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkReport As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents cbox_AllParticipants As System.Windows.Forms.CheckBox
    Friend WithEvents dtp_TransactionDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmb_TransactionDate As System.Windows.Forms.ComboBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
End Class
