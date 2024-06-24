<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWTAInstallmentMgt
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbSettlementRun = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbBillingPeriod = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.gbMenu2 = New System.Windows.Forms.GroupBox()
        Me.cmbSelectAll = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DGridViewWTA = New System.Windows.Forms.DataGridView()
        Me.ctrl_statusStrip = New System.Windows.Forms.StatusStrip()
        Me.tsslbl_Msg = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslbl_Timer = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.colSTLID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTransactionNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colChargeType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colOutstandingAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEWT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSelect = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Panel1.SuspendLayout()
        Me.gbMenu2.SuspendLayout()
        CType(Me.DGridViewWTA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ctrl_statusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnRefresh)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnAdd)
        Me.Panel1.Controls.Add(Me.btnSearch)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.cmbSettlementRun)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cmbBillingPeriod)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(694, 93)
        Me.Panel1.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(149, 29)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Settlement Run"
        '
        'cmbSettlementRun
        '
        Me.cmbSettlementRun.FormattingEnabled = True
        Me.cmbSettlementRun.Location = New System.Drawing.Point(152, 45)
        Me.cmbSettlementRun.Name = "cmbSettlementRun"
        Me.cmbSettlementRun.Size = New System.Drawing.Size(121, 21)
        Me.cmbSettlementRun.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Billing Period"
        '
        'cmbBillingPeriod
        '
        Me.cmbBillingPeriod.FormattingEnabled = True
        Me.cmbBillingPeriod.Location = New System.Drawing.Point(25, 45)
        Me.cmbBillingPeriod.Name = "cmbBillingPeriod"
        Me.cmbBillingPeriod.Size = New System.Drawing.Size(121, 21)
        Me.cmbBillingPeriod.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(8, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Search:"
        '
        'gbMenu2
        '
        Me.gbMenu2.Controls.Add(Me.cmbSelectAll)
        Me.gbMenu2.Controls.Add(Me.Label4)
        Me.gbMenu2.Controls.Add(Me.DGridViewWTA)
        Me.gbMenu2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.gbMenu2.Location = New System.Drawing.Point(4, 104)
        Me.gbMenu2.Name = "gbMenu2"
        Me.gbMenu2.Size = New System.Drawing.Size(694, 382)
        Me.gbMenu2.TabIndex = 10
        Me.gbMenu2.TabStop = False
        '
        'cmbSelectAll
        '
        Me.cmbSelectAll.AutoSize = True
        Me.cmbSelectAll.Location = New System.Drawing.Point(551, 12)
        Me.cmbSelectAll.Name = "cmbSelectAll"
        Me.cmbSelectAll.Size = New System.Drawing.Size(71, 17)
        Me.cmbSelectAll.TabIndex = 25
        Me.cmbSelectAll.Text = "Check All"
        Me.cmbSelectAll.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(12, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 14)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "For Installment:"
        '
        'DGridViewWTA
        '
        Me.DGridViewWTA.AllowUserToAddRows = False
        Me.DGridViewWTA.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridViewWTA.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGridViewWTA.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGridViewWTA.BackgroundColor = System.Drawing.SystemColors.ButtonShadow
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.DimGray
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGridViewWTA.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGridViewWTA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridViewWTA.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSTLID, Me.colTransactionNo, Me.colChargeType, Me.colOutstandingAmount, Me.colEWT, Me.colSelect})
        Me.DGridViewWTA.Location = New System.Drawing.Point(9, 35)
        Me.DGridViewWTA.Name = "DGridViewWTA"
        Me.DGridViewWTA.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGridViewWTA.Size = New System.Drawing.Size(679, 341)
        Me.DGridViewWTA.TabIndex = 0
        '
        'ctrl_statusStrip
        '
        Me.ctrl_statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslbl_Msg, Me.tsslbl_Timer})
        Me.ctrl_statusStrip.Location = New System.Drawing.Point(0, 498)
        Me.ctrl_statusStrip.Name = "ctrl_statusStrip"
        Me.ctrl_statusStrip.Size = New System.Drawing.Size(705, 22)
        Me.ctrl_statusStrip.TabIndex = 62
        Me.ctrl_statusStrip.Text = "StatusStrip1"
        '
        'tsslbl_Msg
        '
        Me.tsslbl_Msg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.tsslbl_Msg.Name = "tsslbl_Msg"
        Me.tsslbl_Msg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tsslbl_Msg.Size = New System.Drawing.Size(570, 17)
        Me.tsslbl_Msg.Spring = True
        Me.tsslbl_Msg.Text = "Ready"
        Me.tsslbl_Msg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.tsslbl_Msg.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'tsslbl_Timer
        '
        Me.tsslbl_Timer.AutoSize = False
        Me.tsslbl_Timer.Name = "tsslbl_Timer"
        Me.tsslbl_Timer.Size = New System.Drawing.Size(120, 17)
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.White
        Me.btnRefresh.BackgroundImage = Global.AccountsManagementForms.My.Resources.Resources.RefreshGreenIcon22x22
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.ForeColor = System.Drawing.Color.Black
        Me.btnRefresh.Location = New System.Drawing.Point(331, 29)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(46, 39)
        Me.btnRefresh.TabIndex = 57
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.White
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseRedIcon22x22
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(597, 29)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(90, 39)
        Me.btnClose.TabIndex = 56
        Me.btnClose.Text = "   &Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.White
        Me.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.ForeColor = System.Drawing.Color.Black
        Me.btnAdd.Image = Global.AccountsManagementForms.My.Resources.Resources.NewGreenIcon22x22
        Me.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAdd.Location = New System.Drawing.Point(501, 29)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(90, 39)
        Me.btnAdd.TabIndex = 9
        Me.btnAdd.Text = "&Add"
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.White
        Me.btnSearch.BackgroundImage = Global.AccountsManagementForms.My.Resources.Resources.SearchIconColored22x22
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.ForeColor = System.Drawing.Color.Black
        Me.btnSearch.Location = New System.Drawing.Point(279, 29)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(46, 39)
        Me.btnSearch.TabIndex = 8
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'colSTLID
        '
        Me.colSTLID.HeaderText = "Settlment ID"
        Me.colSTLID.Name = "colSTLID"
        Me.colSTLID.ReadOnly = True
        Me.colSTLID.Width = 120
        '
        'colTransactionNo
        '
        Me.colTransactionNo.HeaderText = "Transaction No"
        Me.colTransactionNo.Name = "colTransactionNo"
        Me.colTransactionNo.ReadOnly = True
        Me.colTransactionNo.Width = 140
        '
        'colChargeType
        '
        Me.colChargeType.HeaderText = "Charge Type"
        Me.colChargeType.Name = "colChargeType"
        Me.colChargeType.Width = 60
        '
        'colOutstandingAmount
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colOutstandingAmount.DefaultCellStyle = DataGridViewCellStyle3
        Me.colOutstandingAmount.HeaderText = "Net Outstanding Amount"
        Me.colOutstandingAmount.Name = "colOutstandingAmount"
        Me.colOutstandingAmount.ReadOnly = True
        Me.colOutstandingAmount.Width = 150
        '
        'colEWT
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colEWT.DefaultCellStyle = DataGridViewCellStyle4
        Me.colEWT.HeaderText = "EWT"
        Me.colEWT.Name = "colEWT"
        '
        'colSelect
        '
        Me.colSelect.HeaderText = ""
        Me.colSelect.Name = "colSelect"
        Me.colSelect.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colSelect.Width = 60
        '
        'frmWTAInstallmentMgt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(705, 520)
        Me.Controls.Add(Me.ctrl_statusStrip)
        Me.Controls.Add(Me.gbMenu2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmWTAInstallmentMgt"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WTA Installment Management"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.gbMenu2.ResumeLayout(False)
        Me.gbMenu2.PerformLayout()
        CType(Me.DGridViewWTA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ctrl_statusStrip.ResumeLayout(False)
        Me.ctrl_statusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbSettlementRun As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbBillingPeriod As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnSearch As Button
    Friend WithEvents gbMenu2 As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents DGridViewWTA As DataGridView
    Friend WithEvents cmbSelectAll As CheckBox
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents ctrl_statusStrip As StatusStrip
    Friend WithEvents tsslbl_Msg As ToolStripStatusLabel
    Friend WithEvents tsslbl_Timer As ToolStripStatusLabel
    Friend WithEvents Timer1 As Timer
    Friend WithEvents colSTLID As DataGridViewTextBoxColumn
    Friend WithEvents colTransactionNo As DataGridViewTextBoxColumn
    Friend WithEvents colChargeType As DataGridViewTextBoxColumn
    Friend WithEvents colOutstandingAmount As DataGridViewTextBoxColumn
    Friend WithEvents colEWT As DataGridViewTextBoxColumn
    Friend WithEvents colSelect As DataGridViewCheckBoxColumn
End Class
