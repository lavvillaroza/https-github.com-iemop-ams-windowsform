<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportWESMBillFromCRSS
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.btn_Close = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmb_DueDate = New System.Windows.Forms.ComboBox()
        Me.tc_Viewer = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.dgv_WESMInvoices = New System.Windows.Forms.DataGridView()
        Me.chkbox_select = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.txtbox_BillingPeriod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtbox_stlRun = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtbox_FileType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtbox_TotalARAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtbox_TotalAPAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtbox_Remarks = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.dgv_wesmbillsalesandpurchases = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.tc_Viewer.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgv_WESMInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgv_wesmbillsalesandpurchases, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.tc_Viewer, 0, 1)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(880, 509)
        Me.TableLayoutPanel2.TabIndex = 14
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.btnRefresh)
        Me.Panel1.Controls.Add(Me.btnImport)
        Me.Panel1.Controls.Add(Me.btn_Close)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cmb_DueDate)
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(874, 54)
        Me.Panel1.TabIndex = 12
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
        Me.btnImport.Location = New System.Drawing.Point(626, 9)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(113, 39)
        Me.btnImport.TabIndex = 18
        Me.btnImport.Text = "   &Upload File"
        Me.btnImport.UseVisualStyleBackColor = False
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
        Me.btn_Close.Location = New System.Drawing.Point(745, 9)
        Me.btn_Close.Name = "btn_Close"
        Me.btn_Close.Size = New System.Drawing.Size(113, 39)
        Me.btn_Close.TabIndex = 17
        Me.btn_Close.Text = "&Close"
        Me.btn_Close.UseVisualStyleBackColor = False
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
        'tc_Viewer
        '
        Me.tc_Viewer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tc_Viewer.Controls.Add(Me.TabPage1)
        Me.tc_Viewer.Controls.Add(Me.TabPage2)
        Me.tc_Viewer.Location = New System.Drawing.Point(3, 63)
        Me.tc_Viewer.Name = "tc_Viewer"
        Me.tc_Viewer.SelectedIndex = 0
        Me.tc_Viewer.Size = New System.Drawing.Size(874, 443)
        Me.tc_Viewer.TabIndex = 13
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgv_WESMInvoices)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(866, 417)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "WESM Bill Invoices"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'dgv_WESMInvoices
        '
        Me.dgv_WESMInvoices.AllowUserToAddRows = False
        Me.dgv_WESMInvoices.AllowUserToDeleteRows = False
        Me.dgv_WESMInvoices.AllowUserToResizeColumns = False
        Me.dgv_WESMInvoices.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_WESMInvoices.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_WESMInvoices.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_WESMInvoices.ColumnHeadersHeight = 30
        Me.dgv_WESMInvoices.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chkbox_select, Me.txtbox_BillingPeriod, Me.txtbox_stlRun, Me.txtbox_FileType, Me.txtbox_TotalARAmount, Me.txtbox_TotalAPAmount, Me.txtbox_Remarks})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LemonChiffon
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_WESMInvoices.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_WESMInvoices.Location = New System.Drawing.Point(0, 0)
        Me.dgv_WESMInvoices.Name = "dgv_WESMInvoices"
        Me.dgv_WESMInvoices.ReadOnly = True
        Me.dgv_WESMInvoices.RowHeadersVisible = False
        Me.dgv_WESMInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_WESMInvoices.Size = New System.Drawing.Size(863, 408)
        Me.dgv_WESMInvoices.TabIndex = 16
        '
        'chkbox_select
        '
        Me.chkbox_select.HeaderText = ""
        Me.chkbox_select.Name = "chkbox_select"
        Me.chkbox_select.ReadOnly = True
        Me.chkbox_select.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.chkbox_select.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.chkbox_select.Width = 40
        '
        'txtbox_BillingPeriod
        '
        Me.txtbox_BillingPeriod.HeaderText = "Billing Period"
        Me.txtbox_BillingPeriod.Name = "txtbox_BillingPeriod"
        Me.txtbox_BillingPeriod.ReadOnly = True
        '
        'txtbox_stlRun
        '
        Me.txtbox_stlRun.HeaderText = "Settlement Run"
        Me.txtbox_stlRun.Name = "txtbox_stlRun"
        Me.txtbox_stlRun.ReadOnly = True
        Me.txtbox_stlRun.Width = 110
        '
        'txtbox_FileType
        '
        Me.txtbox_FileType.HeaderText = "File Type"
        Me.txtbox_FileType.Name = "txtbox_FileType"
        Me.txtbox_FileType.ReadOnly = True
        '
        'txtbox_TotalARAmount
        '
        Me.txtbox_TotalARAmount.HeaderText = "Total AR Amount"
        Me.txtbox_TotalARAmount.Name = "txtbox_TotalARAmount"
        Me.txtbox_TotalARAmount.ReadOnly = True
        Me.txtbox_TotalARAmount.Width = 150
        '
        'txtbox_TotalAPAmount
        '
        Me.txtbox_TotalAPAmount.HeaderText = "Total AP Amount"
        Me.txtbox_TotalAPAmount.Name = "txtbox_TotalAPAmount"
        Me.txtbox_TotalAPAmount.ReadOnly = True
        Me.txtbox_TotalAPAmount.Width = 150
        '
        'txtbox_Remarks
        '
        Me.txtbox_Remarks.HeaderText = "Remarks"
        Me.txtbox_Remarks.Name = "txtbox_Remarks"
        Me.txtbox_Remarks.ReadOnly = True
        Me.txtbox_Remarks.Width = 300
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.dgv_wesmbillsalesandpurchases)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(866, 417)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "WESM Bill Sales And Purchases"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'dgv_wesmbillsalesandpurchases
        '
        Me.dgv_wesmbillsalesandpurchases.AllowUserToAddRows = False
        Me.dgv_wesmbillsalesandpurchases.AllowUserToDeleteRows = False
        Me.dgv_wesmbillsalesandpurchases.AllowUserToResizeColumns = False
        Me.dgv_wesmbillsalesandpurchases.AllowUserToResizeRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_wesmbillsalesandpurchases.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_wesmbillsalesandpurchases.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_wesmbillsalesandpurchases.ColumnHeadersHeight = 30
        Me.dgv_wesmbillsalesandpurchases.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LemonChiffon
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_wesmbillsalesandpurchases.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgv_wesmbillsalesandpurchases.Location = New System.Drawing.Point(5, 4)
        Me.dgv_wesmbillsalesandpurchases.Name = "dgv_wesmbillsalesandpurchases"
        Me.dgv_wesmbillsalesandpurchases.ReadOnly = True
        Me.dgv_wesmbillsalesandpurchases.RowHeadersVisible = False
        Me.dgv_wesmbillsalesandpurchases.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_wesmbillsalesandpurchases.Size = New System.Drawing.Size(858, 408)
        Me.dgv_wesmbillsalesandpurchases.TabIndex = 17
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Billing Period"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Settlement Run"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 110
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "File Type"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Total AR Amount"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 200
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "Total AP Amount"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 200
        '
        'frmImportWESMBillFromCRSS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(889, 515)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmImportWESMBillFromCRSS"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import WESMBill From CRSSDB (Market Fees Only)"
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tc_Viewer.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.dgv_WESMInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.dgv_wesmbillsalesandpurchases, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btn_Close As System.Windows.Forms.Button
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents cmb_DueDate As System.Windows.Forms.ComboBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents tc_Viewer As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents dgv_WESMInvoices As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents dgv_wesmbillsalesandpurchases As System.Windows.Forms.DataGridView
    Friend WithEvents chkbox_select As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents txtbox_BillingPeriod As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtbox_stlRun As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtbox_FileType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtbox_TotalARAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtbox_TotalAPAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtbox_Remarks As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
