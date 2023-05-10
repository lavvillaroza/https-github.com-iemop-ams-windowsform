<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWBSExemption
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnAddNew = New System.Windows.Forms.Button()
        Me.btnViewChanges = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnResetChanges = New System.Windows.Forms.Button()
        Me.cb_ColumnName = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtBoxSearchValue = New System.Windows.Forms.TextBox()
        Me.cb_SearchValue = New System.Windows.Forms.ComboBox()
        Me.dataGridView = New System.Windows.Forms.DataGridView()
        Me.colWBSummaryNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.batch_no = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.billing_no = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.invoice_no = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.original_due_date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colChargeType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ending_balance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.for_offset = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.for_soa = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colForDefaultInt = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.lblPagination = New System.Windows.Forms.Label()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnPrevious = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.dataGridView, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(14, 12)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1023, 534)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.btnAddNew)
        Me.GroupBox1.Controls.Add(Me.btnViewChanges)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Controls.Add(Me.btnResetChanges)
        Me.GroupBox1.Controls.Add(Me.cb_ColumnName)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.txtBoxSearchValue)
        Me.GroupBox1.Controls.Add(Me.cb_SearchValue)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1017, 74)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'btnSearch
        '
        Me.btnSearch.BackgroundImage = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSearch.Location = New System.Drawing.Point(343, 33)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(39, 30)
        Me.btnSearch.TabIndex = 64
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnAddNew
        '
        Me.btnAddNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddNew.BackColor = System.Drawing.Color.White
        Me.btnAddNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnAddNew.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnAddNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnAddNew.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnAddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddNew.ForeColor = System.Drawing.Color.Black
        Me.btnAddNew.Image = Global.AccountsManagementForms.My.Resources.Resources.NewGreenIcon22x22
        Me.btnAddNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddNew.Location = New System.Drawing.Point(454, 24)
        Me.btnAddNew.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(135, 39)
        Me.btnAddNew.TabIndex = 62
        Me.btnAddNew.Text = "     &Add Changes"
        Me.btnAddNew.UseVisualStyleBackColor = False
        '
        'btnViewChanges
        '
        Me.btnViewChanges.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnViewChanges.BackColor = System.Drawing.Color.White
        Me.btnViewChanges.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnViewChanges.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnViewChanges.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnViewChanges.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnViewChanges.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewChanges.ForeColor = System.Drawing.Color.Black
        Me.btnViewChanges.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassicon22x22
        Me.btnViewChanges.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnViewChanges.Location = New System.Drawing.Point(736, 24)
        Me.btnViewChanges.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnViewChanges.Name = "btnViewChanges"
        Me.btnViewChanges.Size = New System.Drawing.Size(135, 39)
        Me.btnViewChanges.TabIndex = 61
        Me.btnViewChanges.Text = "     &View Changes"
        Me.btnViewChanges.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(148, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 60
        Me.Label2.Text = "Search Value:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 13)
        Me.Label1.TabIndex = 58
        Me.Label1.Text = "Column Name:"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.White
        Me.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Image = Global.AccountsManagementForms.My.Resources.Resources.SaveIconColored22x22
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(877, 24)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(135, 39)
        Me.btnSave.TabIndex = 57
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnResetChanges
        '
        Me.btnResetChanges.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnResetChanges.BackColor = System.Drawing.Color.White
        Me.btnResetChanges.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnResetChanges.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnResetChanges.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnResetChanges.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnResetChanges.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnResetChanges.ForeColor = System.Drawing.Color.Black
        Me.btnResetChanges.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseRedIcon22x22
        Me.btnResetChanges.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnResetChanges.Location = New System.Drawing.Point(595, 24)
        Me.btnResetChanges.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnResetChanges.Name = "btnResetChanges"
        Me.btnResetChanges.Size = New System.Drawing.Size(135, 39)
        Me.btnResetChanges.TabIndex = 34
        Me.btnResetChanges.Text = "     &Cancel Changes"
        Me.btnResetChanges.UseVisualStyleBackColor = False
        '
        'cb_ColumnName
        '
        Me.cb_ColumnName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cb_ColumnName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cb_ColumnName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_ColumnName.FormattingEnabled = True
        Me.cb_ColumnName.Location = New System.Drawing.Point(10, 39)
        Me.cb_ColumnName.Name = "cb_ColumnName"
        Me.cb_ColumnName.Size = New System.Drawing.Size(135, 21)
        Me.cb_ColumnName.TabIndex = 23
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(7, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(56, 13)
        Me.Label18.TabIndex = 21
        Me.Label18.Text = "Search:"
        '
        'txtBoxSearchValue
        '
        Me.txtBoxSearchValue.Location = New System.Drawing.Point(151, 39)
        Me.txtBoxSearchValue.Name = "txtBoxSearchValue"
        Me.txtBoxSearchValue.Size = New System.Drawing.Size(186, 21)
        Me.txtBoxSearchValue.TabIndex = 63
        '
        'cb_SearchValue
        '
        Me.cb_SearchValue.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cb_SearchValue.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cb_SearchValue.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_SearchValue.FormattingEnabled = True
        Me.cb_SearchValue.Location = New System.Drawing.Point(151, 39)
        Me.cb_SearchValue.Name = "cb_SearchValue"
        Me.cb_SearchValue.Size = New System.Drawing.Size(186, 21)
        Me.cb_SearchValue.TabIndex = 59
        '
        'dataGridView
        '
        Me.dataGridView.AllowUserToAddRows = False
        Me.dataGridView.AllowUserToDeleteRows = False
        Me.dataGridView.AllowUserToResizeColumns = False
        Me.dataGridView.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dataGridView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colWBSummaryNo, Me.batch_no, Me.billing_no, Me.invoice_no, Me.colIDNumber, Me.original_due_date, Me.colChargeType, Me.ending_balance, Me.for_offset, Me.for_soa, Me.colForDefaultInt})
        Me.dataGridView.Location = New System.Drawing.Point(3, 83)
        Me.dataGridView.Name = "dataGridView"
        Me.dataGridView.RowHeadersVisible = False
        Me.dataGridView.RowHeadersWidth = 20
        Me.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dataGridView.Size = New System.Drawing.Size(1017, 448)
        Me.dataGridView.TabIndex = 7
        '
        'colWBSummaryNo
        '
        Me.colWBSummaryNo.Frozen = True
        Me.colWBSummaryNo.HeaderText = "WBSummaryNo"
        Me.colWBSummaryNo.Name = "colWBSummaryNo"
        Me.colWBSummaryNo.ReadOnly = True
        Me.colWBSummaryNo.Visible = False
        '
        'batch_no
        '
        Me.batch_no.Frozen = True
        Me.batch_no.HeaderText = "Batch No"
        Me.batch_no.Name = "batch_no"
        Me.batch_no.ReadOnly = True
        Me.batch_no.Width = 85
        '
        'billing_no
        '
        Me.billing_no.Frozen = True
        Me.billing_no.HeaderText = "Billing No"
        Me.billing_no.Name = "billing_no"
        Me.billing_no.ReadOnly = True
        Me.billing_no.Width = 85
        '
        'invoice_no
        '
        Me.invoice_no.Frozen = True
        Me.invoice_no.HeaderText = "Invoice No"
        Me.invoice_no.Name = "invoice_no"
        Me.invoice_no.ReadOnly = True
        Me.invoice_no.Width = 120
        '
        'colIDNumber
        '
        Me.colIDNumber.Frozen = True
        Me.colIDNumber.HeaderText = "ID Number"
        Me.colIDNumber.Name = "colIDNumber"
        Me.colIDNumber.ReadOnly = True
        '
        'original_due_date
        '
        Me.original_due_date.Frozen = True
        Me.original_due_date.HeaderText = "Due Date"
        Me.original_due_date.Name = "original_due_date"
        Me.original_due_date.ReadOnly = True
        '
        'colChargeType
        '
        Me.colChargeType.Frozen = True
        Me.colChargeType.HeaderText = "Charge Type"
        Me.colChargeType.Name = "colChargeType"
        Me.colChargeType.ReadOnly = True
        '
        'ending_balance
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.ending_balance.DefaultCellStyle = DataGridViewCellStyle2
        Me.ending_balance.Frozen = True
        Me.ending_balance.HeaderText = "Ending Balance"
        Me.ending_balance.Name = "ending_balance"
        Me.ending_balance.ReadOnly = True
        Me.ending_balance.Width = 150
        '
        'for_offset
        '
        Me.for_offset.HeaderText = "No Offset"
        Me.for_offset.Name = "for_offset"
        Me.for_offset.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.for_offset.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.for_offset.Width = 90
        '
        'for_soa
        '
        Me.for_soa.HeaderText = "No SOA"
        Me.for_soa.Name = "for_soa"
        Me.for_soa.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.for_soa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.for_soa.Width = 80
        '
        'colForDefaultInt
        '
        Me.colForDefaultInt.HeaderText = "No Default Interest"
        Me.colForDefaultInt.Name = "colForDefaultInt"
        Me.colForDefaultInt.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colForDefaultInt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colForDefaultInt.Width = 110
        '
        'lblPagination
        '
        Me.lblPagination.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPagination.Location = New System.Drawing.Point(626, 557)
        Me.lblPagination.Name = "lblPagination"
        Me.lblPagination.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPagination.Size = New System.Drawing.Size(329, 18)
        Me.lblPagination.TabIndex = 1
        Me.lblPagination.Text = "No Page Available."
        Me.lblPagination.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnNext
        '
        Me.btnNext.BackgroundImage = Global.AccountsManagementForms.My.Resources.Resources.ArrowRight
        Me.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnNext.Location = New System.Drawing.Point(1003, 555)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(34, 23)
        Me.btnNext.TabIndex = 2
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnPrevious
        '
        Me.btnPrevious.BackgroundImage = Global.AccountsManagementForms.My.Resources.Resources.ArrowLeft
        Me.btnPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnPrevious.Location = New System.Drawing.Point(963, 555)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(34, 23)
        Me.btnPrevious.TabIndex = 3
        Me.btnPrevious.UseVisualStyleBackColor = True
        '
        'frmWBSExemption
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1046, 583)
        Me.Controls.Add(Me.btnPrevious)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.lblPagination)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmWBSExemption"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WESM and NSS Bill Exemption Management"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents dataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cb_ColumnName As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents btnResetChanges As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cb_SearchValue As System.Windows.Forms.ComboBox
    Friend WithEvents btnViewChanges As System.Windows.Forms.Button
    Friend WithEvents btnAddNew As System.Windows.Forms.Button
    Friend WithEvents colWBSummaryNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents batch_no As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents billing_no As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents invoice_no As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents original_due_date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colChargeType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ending_balance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents for_offset As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents for_soa As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colForDefaultInt As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents lblPagination As System.Windows.Forms.Label
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents txtBoxSearchValue As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
End Class
