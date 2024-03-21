<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportReservedFromCRSS
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ctrl_statusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatus_LabelMsg = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TLP_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.tc_Viewer = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.dgv_WTAList = New System.Windows.Forms.DataGridView()
        Me.colChckBox = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colBP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSTLRun = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colGroupID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTotalNetSales = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTotalEWTSales = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTotalNetPurchases = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalEWTPurchases = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRemarks = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel_Head = New System.Windows.Forms.Panel()
        Me.btn_Close = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmb_DueDate = New System.Windows.Forms.ComboBox()
        Me.ctrl_statusStrip.SuspendLayout()
        Me.TLP_Main.SuspendLayout()
        Me.tc_Viewer.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgv_WTAList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel_Head.SuspendLayout()
        Me.SuspendLayout()
        '
        'ctrl_statusStrip
        '
        Me.ctrl_statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatus_LabelMsg})
        Me.ctrl_statusStrip.Location = New System.Drawing.Point(0, 516)
        Me.ctrl_statusStrip.Name = "ctrl_statusStrip"
        Me.ctrl_statusStrip.Size = New System.Drawing.Size(1080, 22)
        Me.ctrl_statusStrip.TabIndex = 58
        Me.ctrl_statusStrip.Text = "StatusStrip1"
        '
        'ToolStripStatus_LabelMsg
        '
        Me.ToolStripStatus_LabelMsg.Name = "ToolStripStatus_LabelMsg"
        Me.ToolStripStatus_LabelMsg.Size = New System.Drawing.Size(48, 17)
        Me.ToolStripStatus_LabelMsg.Text = "Ready..."
        '
        'TLP_Main
        '
        Me.TLP_Main.ColumnCount = 1
        Me.TLP_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLP_Main.Controls.Add(Me.tc_Viewer, 0, 1)
        Me.TLP_Main.Controls.Add(Me.Panel_Head, 0, 0)
        Me.TLP_Main.Location = New System.Drawing.Point(5, 3)
        Me.TLP_Main.Name = "TLP_Main"
        Me.TLP_Main.RowCount = 2
        Me.TLP_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TLP_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLP_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLP_Main.Size = New System.Drawing.Size(1071, 509)
        Me.TLP_Main.TabIndex = 59
        '
        'tc_Viewer
        '
        Me.tc_Viewer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tc_Viewer.Controls.Add(Me.TabPage1)
        Me.tc_Viewer.Location = New System.Drawing.Point(3, 63)
        Me.tc_Viewer.Name = "tc_Viewer"
        Me.tc_Viewer.SelectedIndex = 0
        Me.tc_Viewer.Size = New System.Drawing.Size(1065, 443)
        Me.tc_Viewer.TabIndex = 13
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgv_WTAList)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1057, 417)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "List of RTA"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'dgv_WTAList
        '
        Me.dgv_WTAList.AllowUserToAddRows = False
        Me.dgv_WTAList.AllowUserToDeleteRows = False
        Me.dgv_WTAList.AllowUserToResizeColumns = False
        Me.dgv_WTAList.AllowUserToResizeRows = False
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_WTAList.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        Me.dgv_WTAList.ColumnHeadersHeight = 30
        Me.dgv_WTAList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colChckBox, Me.colBP, Me.colSTLRun, Me.colGroupID, Me.colTotalNetSales, Me.colTotalEWTSales, Me.colTotalNetPurchases, Me.TotalEWTPurchases, Me.colRemarks})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LemonChiffon
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_WTAList.DefaultCellStyle = DataGridViewCellStyle8
        Me.dgv_WTAList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_WTAList.Location = New System.Drawing.Point(3, 3)
        Me.dgv_WTAList.Name = "dgv_WTAList"
        Me.dgv_WTAList.ReadOnly = True
        Me.dgv_WTAList.RowHeadersVisible = False
        Me.dgv_WTAList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_WTAList.Size = New System.Drawing.Size(1051, 411)
        Me.dgv_WTAList.TabIndex = 17
        '
        'colChckBox
        '
        Me.colChckBox.HeaderText = ""
        Me.colChckBox.Name = "colChckBox"
        Me.colChckBox.ReadOnly = True
        Me.colChckBox.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colChckBox.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colChckBox.Width = 40
        '
        'colBP
        '
        Me.colBP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.colBP.HeaderText = "Billing Period"
        Me.colBP.Name = "colBP"
        Me.colBP.ReadOnly = True
        Me.colBP.Width = 92
        '
        'colSTLRun
        '
        Me.colSTLRun.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.colSTLRun.HeaderText = "Settlement Run"
        Me.colSTLRun.Name = "colSTLRun"
        Me.colSTLRun.ReadOnly = True
        Me.colSTLRun.Width = 105
        '
        'colGroupID
        '
        Me.colGroupID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.colGroupID.HeaderText = "Group ID"
        Me.colGroupID.Name = "colGroupID"
        Me.colGroupID.ReadOnly = True
        Me.colGroupID.Width = 75
        '
        'colTotalNetSales
        '
        Me.colTotalNetSales.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.colTotalNetSales.HeaderText = "Total Net Sales"
        Me.colTotalNetSales.Name = "colTotalNetSales"
        Me.colTotalNetSales.ReadOnly = True
        Me.colTotalNetSales.Width = 105
        '
        'colTotalEWTSales
        '
        Me.colTotalEWTSales.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.colTotalEWTSales.HeaderText = "Total EWT Sales"
        Me.colTotalEWTSales.Name = "colTotalEWTSales"
        Me.colTotalEWTSales.ReadOnly = True
        Me.colTotalEWTSales.Width = 113
        '
        'colTotalNetPurchases
        '
        Me.colTotalNetPurchases.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.colTotalNetPurchases.HeaderText = "Total Net Purchases"
        Me.colTotalNetPurchases.Name = "colTotalNetPurchases"
        Me.colTotalNetPurchases.ReadOnly = True
        Me.colTotalNetPurchases.Width = 129
        '
        'TotalEWTPurchases
        '
        Me.TotalEWTPurchases.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.TotalEWTPurchases.HeaderText = "Total EWT Purchases"
        Me.TotalEWTPurchases.Name = "TotalEWTPurchases"
        Me.TotalEWTPurchases.ReadOnly = True
        Me.TotalEWTPurchases.Width = 137
        '
        'colRemarks
        '
        Me.colRemarks.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colRemarks.HeaderText = "Remarks"
        Me.colRemarks.Name = "colRemarks"
        Me.colRemarks.ReadOnly = True
        '
        'Panel_Head
        '
        Me.Panel_Head.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_Head.Controls.Add(Me.btn_Close)
        Me.Panel_Head.Controls.Add(Me.btnRefresh)
        Me.Panel_Head.Controls.Add(Me.btnImport)
        Me.Panel_Head.Controls.Add(Me.Label2)
        Me.Panel_Head.Controls.Add(Me.cmb_DueDate)
        Me.Panel_Head.Location = New System.Drawing.Point(3, 3)
        Me.Panel_Head.Name = "Panel_Head"
        Me.Panel_Head.Size = New System.Drawing.Size(1065, 54)
        Me.Panel_Head.TabIndex = 12
        '
        'btn_Close
        '
        Me.btn_Close.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btn_Close.BackColor = System.Drawing.Color.White
        Me.btn_Close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Close.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Close.ForeColor = System.Drawing.Color.Black
        Me.btn_Close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btn_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Close.Location = New System.Drawing.Point(905, 7)
        Me.btn_Close.Name = "btn_Close"
        Me.btn_Close.Size = New System.Drawing.Size(140, 39)
        Me.btn_Close.TabIndex = 20
        Me.btn_Close.Text = "&Close"
        Me.btn_Close.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.ForeColor = System.Drawing.Color.Blue
        Me.btnRefresh.Image = Global.AccountsManagementForms.My.Resources.Resources.RefreshGreenIcon22x22
        Me.btnRefresh.Location = New System.Drawing.Point(224, 16)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(35, 30)
        Me.btnRefresh.TabIndex = 19
        Me.btnRefresh.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImport.BackColor = System.Drawing.Color.White
        Me.btnImport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnImport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnImport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImport.ForeColor = System.Drawing.Color.Black
        Me.btnImport.Image = Global.AccountsManagementForms.My.Resources.Resources.Upload2Icon22x22
        Me.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImport.Location = New System.Drawing.Point(759, 8)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(140, 39)
        Me.btnImport.TabIndex = 18
        Me.btnImport.Text = "   &Upload File"
        Me.btnImport.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(17, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 15)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Due Date:"
        '
        'cmb_DueDate
        '
        Me.cmb_DueDate.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmb_DueDate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmb_DueDate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmb_DueDate.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmb_DueDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_DueDate.ForeColor = System.Drawing.Color.Black
        Me.cmb_DueDate.FormattingEnabled = True
        Me.cmb_DueDate.Location = New System.Drawing.Point(87, 20)
        Me.cmb_DueDate.Name = "cmb_DueDate"
        Me.cmb_DueDate.Size = New System.Drawing.Size(131, 22)
        Me.cmb_DueDate.TabIndex = 2
        '
        'frmImportReservedFromCRSS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1080, 538)
        Me.Controls.Add(Me.TLP_Main)
        Me.Controls.Add(Me.ctrl_statusStrip)
        Me.Name = "frmImportReservedFromCRSS"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import Reserved Transaction Allocation From CRSS"
        Me.ctrl_statusStrip.ResumeLayout(False)
        Me.ctrl_statusStrip.PerformLayout()
        Me.TLP_Main.ResumeLayout(False)
        Me.tc_Viewer.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.dgv_WTAList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel_Head.ResumeLayout(False)
        Me.Panel_Head.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ctrl_statusStrip As StatusStrip
    Friend WithEvents ToolStripStatus_LabelMsg As ToolStripStatusLabel
    Friend WithEvents TLP_Main As TableLayoutPanel
    Friend WithEvents Panel_Head As Panel
    Friend WithEvents btn_Close As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnImport As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents cmb_DueDate As ComboBox
    Friend WithEvents tc_Viewer As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents dgv_WTAList As DataGridView
    Friend WithEvents colChckBox As DataGridViewCheckBoxColumn
    Friend WithEvents colBP As DataGridViewTextBoxColumn
    Friend WithEvents colSTLRun As DataGridViewTextBoxColumn
    Friend WithEvents colGroupID As DataGridViewTextBoxColumn
    Friend WithEvents colTotalNetSales As DataGridViewTextBoxColumn
    Friend WithEvents colTotalEWTSales As DataGridViewTextBoxColumn
    Friend WithEvents colTotalNetPurchases As DataGridViewTextBoxColumn
    Friend WithEvents TotalEWTPurchases As DataGridViewTextBoxColumn
    Friend WithEvents colRemarks As DataGridViewTextBoxColumn
End Class
