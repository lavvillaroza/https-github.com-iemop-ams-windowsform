<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBRCollectionReportMgt
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
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.btnViewByBuyer = New System.Windows.Forms.Button()
        Me.btnViewBySeller = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DGVBRCollectionReport = New System.Windows.Forms.DataGridView()
        Me.colBIRRNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRemittanceDateFrom = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRemittanceDateTo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colGeneratedBy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colGeneratedDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ctrl_statusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabelCR = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MainPanel = New System.Windows.Forms.Panel()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DGVBRCollectionReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ctrl_statusStrip.SuspendLayout()
        Me.MainPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.btnViewByBuyer)
        Me.GroupBox8.Controls.Add(Me.btnViewBySeller)
        Me.GroupBox8.Controls.Add(Me.btnAdd)
        Me.GroupBox8.Location = New System.Drawing.Point(3, 324)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(804, 59)
        Me.GroupBox8.TabIndex = 31
        Me.GroupBox8.TabStop = False
        '
        'btnViewByBuyer
        '
        Me.btnViewByBuyer.BackColor = System.Drawing.Color.White
        Me.btnViewByBuyer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnViewByBuyer.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnViewByBuyer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnViewByBuyer.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnViewByBuyer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewByBuyer.ForeColor = System.Drawing.Color.Black
        Me.btnViewByBuyer.Image = Global.AccountsManagementForms.My.Resources.Resources.SearchIcon22x22
        Me.btnViewByBuyer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnViewByBuyer.Location = New System.Drawing.Point(708, 14)
        Me.btnViewByBuyer.Name = "btnViewByBuyer"
        Me.btnViewByBuyer.Size = New System.Drawing.Size(90, 39)
        Me.btnViewByBuyer.TabIndex = 9
        Me.btnViewByBuyer.Text = "View by &Buyer"
        Me.btnViewByBuyer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnViewByBuyer.UseVisualStyleBackColor = False
        '
        'btnViewBySeller
        '
        Me.btnViewBySeller.BackColor = System.Drawing.Color.White
        Me.btnViewBySeller.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnViewBySeller.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnViewBySeller.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnViewBySeller.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnViewBySeller.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewBySeller.ForeColor = System.Drawing.Color.Black
        Me.btnViewBySeller.Image = Global.AccountsManagementForms.My.Resources.Resources.SearchIcon22x22
        Me.btnViewBySeller.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnViewBySeller.Location = New System.Drawing.Point(612, 14)
        Me.btnViewBySeller.Name = "btnViewBySeller"
        Me.btnViewBySeller.Size = New System.Drawing.Size(90, 39)
        Me.btnViewBySeller.TabIndex = 8
        Me.btnViewBySeller.Text = "View by &Seller"
        Me.btnViewBySeller.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnViewBySeller.UseVisualStyleBackColor = False
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
        Me.btnAdd.Location = New System.Drawing.Point(516, 14)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(90, 39)
        Me.btnAdd.TabIndex = 1
        Me.btnAdd.Text = "&Add"
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.DGVBRCollectionReport)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(804, 315)
        Me.GroupBox2.TabIndex = 30
        Me.GroupBox2.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(176, 14)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Created Collection Report List:"
        '
        'DGVBRCollectionReport
        '
        Me.DGVBRCollectionReport.AllowUserToAddRows = False
        Me.DGVBRCollectionReport.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGVBRCollectionReport.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVBRCollectionReport.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGVBRCollectionReport.BackgroundColor = System.Drawing.SystemColors.ButtonShadow
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.DimGray
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVBRCollectionReport.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGVBRCollectionReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVBRCollectionReport.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colBIRRNo, Me.colRemittanceDateFrom, Me.colRemittanceDateTo, Me.colGeneratedBy, Me.colGeneratedDate})
        Me.DGVBRCollectionReport.Location = New System.Drawing.Point(9, 19)
        Me.DGVBRCollectionReport.MultiSelect = False
        Me.DGVBRCollectionReport.Name = "DGVBRCollectionReport"
        Me.DGVBRCollectionReport.ReadOnly = True
        Me.DGVBRCollectionReport.RowHeadersVisible = False
        Me.DGVBRCollectionReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVBRCollectionReport.Size = New System.Drawing.Size(789, 290)
        Me.DGVBRCollectionReport.TabIndex = 0
        '
        'colBIRRNo
        '
        Me.colBIRRNo.HeaderText = "BIRR Number"
        Me.colBIRRNo.Name = "colBIRRNo"
        Me.colBIRRNo.ReadOnly = True
        Me.colBIRRNo.Width = 130
        '
        'colRemittanceDateFrom
        '
        Me.colRemittanceDateFrom.HeaderText = "Remittance Date From"
        Me.colRemittanceDateFrom.Name = "colRemittanceDateFrom"
        Me.colRemittanceDateFrom.ReadOnly = True
        Me.colRemittanceDateFrom.Width = 160
        '
        'colRemittanceDateTo
        '
        Me.colRemittanceDateTo.HeaderText = "Remittance Date To"
        Me.colRemittanceDateTo.Name = "colRemittanceDateTo"
        Me.colRemittanceDateTo.ReadOnly = True
        Me.colRemittanceDateTo.Width = 160
        '
        'colGeneratedBy
        '
        Me.colGeneratedBy.HeaderText = "Generated By"
        Me.colGeneratedBy.Name = "colGeneratedBy"
        Me.colGeneratedBy.ReadOnly = True
        Me.colGeneratedBy.Width = 200
        '
        'colGeneratedDate
        '
        Me.colGeneratedDate.HeaderText = "Generated Date"
        Me.colGeneratedDate.Name = "colGeneratedDate"
        Me.colGeneratedDate.ReadOnly = True
        Me.colGeneratedDate.Width = 140
        '
        'ctrl_statusStrip
        '
        Me.ctrl_statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabelCR})
        Me.ctrl_statusStrip.Location = New System.Drawing.Point(0, 399)
        Me.ctrl_statusStrip.Name = "ctrl_statusStrip"
        Me.ctrl_statusStrip.Size = New System.Drawing.Size(812, 22)
        Me.ctrl_statusStrip.TabIndex = 32
        Me.ctrl_statusStrip.Text = "StatusStrip1"
        '
        'ToolStripStatusLabelCR
        '
        Me.ToolStripStatusLabelCR.Name = "ToolStripStatusLabelCR"
        Me.ToolStripStatusLabelCR.Size = New System.Drawing.Size(39, 17)
        Me.ToolStripStatusLabelCR.Text = "Ready"
        '
        'MainPanel
        '
        Me.MainPanel.Controls.Add(Me.GroupBox2)
        Me.MainPanel.Controls.Add(Me.GroupBox8)
        Me.MainPanel.Location = New System.Drawing.Point(0, 1)
        Me.MainPanel.Name = "MainPanel"
        Me.MainPanel.Size = New System.Drawing.Size(810, 393)
        Me.MainPanel.TabIndex = 33
        '
        'frmBRCollectionReportMgt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(812, 421)
        Me.Controls.Add(Me.MainPanel)
        Me.Controls.Add(Me.ctrl_statusStrip)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmBRCollectionReportMgt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BIR Ruling Collection Report Management"
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.DGVBRCollectionReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ctrl_statusStrip.ResumeLayout(False)
        Me.ctrl_statusStrip.PerformLayout()
        Me.MainPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox8 As GroupBox
    Friend WithEvents btnViewBySeller As Button
    Friend WithEvents btnAdd As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents DGVBRCollectionReport As DataGridView
    Friend WithEvents ctrl_statusStrip As StatusStrip
    Friend WithEvents ToolStripStatusLabelCR As ToolStripStatusLabel
    Friend WithEvents colBIRRNo As DataGridViewTextBoxColumn
    Friend WithEvents colRemittanceDateFrom As DataGridViewTextBoxColumn
    Friend WithEvents colRemittanceDateTo As DataGridViewTextBoxColumn
    Friend WithEvents colGeneratedBy As DataGridViewTextBoxColumn
    Friend WithEvents colGeneratedDate As DataGridViewTextBoxColumn
    Friend WithEvents MainPanel As Panel
    Friend WithEvents btnViewByBuyer As Button
End Class
