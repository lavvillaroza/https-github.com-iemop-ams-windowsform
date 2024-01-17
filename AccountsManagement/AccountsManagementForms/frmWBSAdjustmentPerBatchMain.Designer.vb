<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWBSAdjustmentPerBatchMain
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.dgv_WBSPerBatchMain = New System.Windows.Forms.DataGridView()
        Me.colRefID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBillingPeriodNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colWESMBillBatchNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colChargeType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colARAdjustedWithholdingTax = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAPAdjustedWithholdingTAX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUpdatedBy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUpdatedDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAction = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btn_Refresh = New System.Windows.Forms.Button()
        Me.btn_Close = New System.Windows.Forms.Button()
        Me.btn_Add = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.dgv_WBSPerBatchMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.dgv_WBSPerBatchMain, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(980, 365)
        Me.TableLayoutPanel1.TabIndex = 10
        '
        'dgv_WBSPerBatchMain
        '
        Me.dgv_WBSPerBatchMain.AllowUserToAddRows = False
        Me.dgv_WBSPerBatchMain.AllowUserToDeleteRows = False
        Me.dgv_WBSPerBatchMain.AllowUserToResizeColumns = False
        Me.dgv_WBSPerBatchMain.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_WBSPerBatchMain.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_WBSPerBatchMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_WBSPerBatchMain.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_WBSPerBatchMain.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_WBSPerBatchMain.ColumnHeadersHeight = 35
        Me.dgv_WBSPerBatchMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgv_WBSPerBatchMain.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colRefID, Me.colBillingPeriodNo, Me.colWESMBillBatchNo, Me.colChargeType, Me.colARAdjustedWithholdingTax, Me.colAPAdjustedWithholdingTAX, Me.colUpdatedBy, Me.colUpdatedDate, Me.colAction})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LemonChiffon
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_WBSPerBatchMain.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_WBSPerBatchMain.Location = New System.Drawing.Point(3, 3)
        Me.dgv_WBSPerBatchMain.MultiSelect = False
        Me.dgv_WBSPerBatchMain.Name = "dgv_WBSPerBatchMain"
        Me.dgv_WBSPerBatchMain.ReadOnly = True
        Me.dgv_WBSPerBatchMain.RowHeadersVisible = False
        Me.dgv_WBSPerBatchMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_WBSPerBatchMain.Size = New System.Drawing.Size(974, 304)
        Me.dgv_WBSPerBatchMain.TabIndex = 8
        '
        'colRefID
        '
        Me.colRefID.HeaderText = "Reference No"
        Me.colRefID.Name = "colRefID"
        Me.colRefID.ReadOnly = True
        '
        'colBillingPeriodNo
        '
        Me.colBillingPeriodNo.HeaderText = "Billing Period No"
        Me.colBillingPeriodNo.Name = "colBillingPeriodNo"
        Me.colBillingPeriodNo.ReadOnly = True
        '
        'colWESMBillBatchNo
        '
        Me.colWESMBillBatchNo.HeaderText = "WESMBill Batch No"
        Me.colWESMBillBatchNo.Name = "colWESMBillBatchNo"
        Me.colWESMBillBatchNo.ReadOnly = True
        Me.colWESMBillBatchNo.Width = 120
        '
        'colChargeType
        '
        Me.colChargeType.HeaderText = "Charge Type"
        Me.colChargeType.Name = "colChargeType"
        Me.colChargeType.ReadOnly = True
        '
        'colARAdjustedWithholdingTax
        '
        Me.colARAdjustedWithholdingTax.HeaderText = "(-) Adjusted Withholding TAX"
        Me.colARAdjustedWithholdingTax.Name = "colARAdjustedWithholdingTax"
        Me.colARAdjustedWithholdingTax.ReadOnly = True
        '
        'colAPAdjustedWithholdingTAX
        '
        Me.colAPAdjustedWithholdingTAX.HeaderText = "(+) Adjusted Withholding TAX"
        Me.colAPAdjustedWithholdingTAX.Name = "colAPAdjustedWithholdingTAX"
        Me.colAPAdjustedWithholdingTAX.ReadOnly = True
        '
        'colUpdatedBy
        '
        Me.colUpdatedBy.HeaderText = "Updated By"
        Me.colUpdatedBy.Name = "colUpdatedBy"
        Me.colUpdatedBy.ReadOnly = True
        '
        'colUpdatedDate
        '
        Me.colUpdatedDate.HeaderText = "Updated Date"
        Me.colUpdatedDate.Name = "colUpdatedDate"
        Me.colUpdatedDate.ReadOnly = True
        Me.colUpdatedDate.Width = 150
        '
        'colAction
        '
        Me.colAction.HeaderText = "Action"
        Me.colAction.Name = "colAction"
        Me.colAction.ReadOnly = True
        Me.colAction.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colAction.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.btn_Refresh)
        Me.Panel1.Controls.Add(Me.btn_Close)
        Me.Panel1.Controls.Add(Me.btn_Add)
        Me.Panel1.Location = New System.Drawing.Point(3, 313)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(974, 49)
        Me.Panel1.TabIndex = 9
        '
        'btn_Refresh
        '
        Me.btn_Refresh.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btn_Refresh.BackColor = System.Drawing.Color.White
        Me.btn_Refresh.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Refresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Refresh.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Refresh.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Refresh.ForeColor = System.Drawing.Color.Black
        Me.btn_Refresh.Image = Global.AccountsManagementForms.My.Resources.Resources.RefreshGreenIcon22x22
        Me.btn_Refresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Refresh.Location = New System.Drawing.Point(587, 3)
        Me.btn_Refresh.Name = "btn_Refresh"
        Me.btn_Refresh.Size = New System.Drawing.Size(124, 40)
        Me.btn_Refresh.TabIndex = 10
        Me.btn_Refresh.Text = "&Refresh"
        Me.btn_Refresh.UseVisualStyleBackColor = False
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
        Me.btn_Close.Location = New System.Drawing.Point(847, 3)
        Me.btn_Close.Name = "btn_Close"
        Me.btn_Close.Size = New System.Drawing.Size(124, 40)
        Me.btn_Close.TabIndex = 9
        Me.btn_Close.Text = "&Close"
        Me.btn_Close.UseVisualStyleBackColor = False
        '
        'btn_Add
        '
        Me.btn_Add.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btn_Add.BackColor = System.Drawing.Color.White
        Me.btn_Add.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Add.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Add.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Add.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Add.ForeColor = System.Drawing.Color.Black
        Me.btn_Add.Image = Global.AccountsManagementForms.My.Resources.Resources.NewGreenIcon22x22
        Me.btn_Add.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Add.Location = New System.Drawing.Point(717, 3)
        Me.btn_Add.Name = "btn_Add"
        Me.btn_Add.Size = New System.Drawing.Size(124, 40)
        Me.btn_Add.TabIndex = 7
        Me.btn_Add.Text = "&Adjustment"
        Me.btn_Add.UseVisualStyleBackColor = False
        '
        'frmWBSAdjustmentPerBatchMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(986, 369)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmWBSAdjustmentPerBatchMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Withholding Tax Adjustment (Management)"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.dgv_WBSPerBatchMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents dgv_WBSPerBatchMain As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btn_Close As System.Windows.Forms.Button
    Friend WithEvents btn_Add As System.Windows.Forms.Button
    Friend WithEvents btn_Refresh As System.Windows.Forms.Button
    Friend WithEvents colRefID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBillingPeriodNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colWESMBillBatchNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colChargeType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colARAdjustedWithholdingTax As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAPAdjustedWithholdingTAX As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUpdatedBy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUpdatedDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAction As System.Windows.Forms.DataGridViewLinkColumn
End Class
