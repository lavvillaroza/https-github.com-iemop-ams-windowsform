<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWBSParentIdChange
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btn_Import = New System.Windows.Forms.Button()
        Me.btn_Close = New System.Windows.Forms.Button()
        Me.btn_Add = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtp_DateTo = New System.Windows.Forms.DateTimePicker()
        Me.dtp_DateFrom = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmb_BillingPeriodNo = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dgv_wbsChangeParentId = New System.Windows.Forms.DataGridView()
        Me.ParentIDNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChildIDNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChildName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NewParentIdNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NewParentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UpdatedBy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UpdatedDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ViewColumn = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.dgv_wbsChangeParentId, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox3, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.19747!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.14371!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.65882!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1146, 501)
        Me.TableLayoutPanel1.TabIndex = 11
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.btn_Import)
        Me.Panel1.Controls.Add(Me.btn_Close)
        Me.Panel1.Controls.Add(Me.btn_Add)
        Me.Panel1.Location = New System.Drawing.Point(3, 435)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1140, 63)
        Me.Panel1.TabIndex = 9
        '
        'btn_Import
        '
        Me.btn_Import.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btn_Import.BackColor = System.Drawing.Color.White
        Me.btn_Import.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Import.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Import.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Import.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Import.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Import.ForeColor = System.Drawing.Color.Black
        Me.btn_Import.Image = Global.AccountsManagementForms.My.Resources.Resources.Page
        Me.btn_Import.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Import.Location = New System.Drawing.Point(732, 11)
        Me.btn_Import.Name = "btn_Import"
        Me.btn_Import.Size = New System.Drawing.Size(129, 42)
        Me.btn_Import.TabIndex = 10
        Me.btn_Import.Text = "&Export To"
        Me.btn_Import.UseVisualStyleBackColor = False
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
        Me.btn_Close.Location = New System.Drawing.Point(1002, 11)
        Me.btn_Close.Name = "btn_Close"
        Me.btn_Close.Size = New System.Drawing.Size(129, 42)
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
        Me.btn_Add.Location = New System.Drawing.Point(867, 11)
        Me.btn_Add.Name = "btn_Add"
        Me.btn_Add.Size = New System.Drawing.Size(129, 42)
        Me.btn_Add.TabIndex = 7
        Me.btn_Add.Text = "&Add"
        Me.btn_Add.UseVisualStyleBackColor = False
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.GroupBox2, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.GroupBox1, 0, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(653, 57)
        Me.TableLayoutPanel2.TabIndex = 10
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.dtp_DateTo)
        Me.GroupBox2.Controls.Add(Me.dtp_DateFrom)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(231, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(359, 51)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(213, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(23, 15)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "To:"
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, -2)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 15)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Date Range:"
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(39, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 15)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "From:"
        '
        'dtp_DateTo
        '
        Me.dtp_DateTo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.dtp_DateTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_DateTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_DateTo.Location = New System.Drawing.Point(244, 19)
        Me.dtp_DateTo.Name = "dtp_DateTo"
        Me.dtp_DateTo.Size = New System.Drawing.Size(103, 20)
        Me.dtp_DateTo.TabIndex = 5
        '
        'dtp_DateFrom
        '
        Me.dtp_DateFrom.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.dtp_DateFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_DateFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_DateFrom.Location = New System.Drawing.Point(84, 19)
        Me.dtp_DateFrom.Name = "dtp_DateFrom"
        Me.dtp_DateFrom.Size = New System.Drawing.Size(103, 20)
        Me.dtp_DateFrom.TabIndex = 4
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmb_BillingPeriodNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(222, 51)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, -2)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 15)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Billing Period:"
        '
        'cmb_BillingPeriodNo
        '
        Me.cmb_BillingPeriodNo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmb_BillingPeriodNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmb_BillingPeriodNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmb_BillingPeriodNo.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmb_BillingPeriodNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_BillingPeriodNo.ForeColor = System.Drawing.Color.Black
        Me.cmb_BillingPeriodNo.FormattingEnabled = True
        Me.cmb_BillingPeriodNo.Location = New System.Drawing.Point(78, 19)
        Me.cmb_BillingPeriodNo.Name = "cmb_BillingPeriodNo"
        Me.cmb_BillingPeriodNo.Size = New System.Drawing.Size(126, 22)
        Me.cmb_BillingPeriodNo.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(46, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "No:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.dgv_wbsChangeParentId)
        Me.GroupBox3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(3, 69)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1140, 360)
        Me.GroupBox3.TabIndex = 11
        Me.GroupBox3.TabStop = False
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 15)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "List of Mapping:"
        '
        'dgv_wbsChangeParentId
        '
        Me.dgv_wbsChangeParentId.AllowUserToAddRows = False
        Me.dgv_wbsChangeParentId.AllowUserToDeleteRows = False
        Me.dgv_wbsChangeParentId.AllowUserToResizeColumns = False
        Me.dgv_wbsChangeParentId.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_wbsChangeParentId.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_wbsChangeParentId.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_wbsChangeParentId.ColumnHeadersHeight = 30
        Me.dgv_wbsChangeParentId.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgv_wbsChangeParentId.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ParentIDNo, Me.ParentName, Me.ChildIDNo, Me.ChildName, Me.NewParentIdNo, Me.NewParentName, Me.Status, Me.UpdatedBy, Me.UpdatedDate, Me.ViewColumn})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LemonChiffon
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_wbsChangeParentId.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_wbsChangeParentId.Location = New System.Drawing.Point(9, 17)
        Me.dgv_wbsChangeParentId.Name = "dgv_wbsChangeParentId"
        Me.dgv_wbsChangeParentId.ReadOnly = True
        Me.dgv_wbsChangeParentId.RowHeadersVisible = False
        Me.dgv_wbsChangeParentId.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_wbsChangeParentId.Size = New System.Drawing.Size(1125, 337)
        Me.dgv_wbsChangeParentId.TabIndex = 8
        '
        'ParentIDNo
        '
        Me.ParentIDNo.HeaderText = "Parent IDNo"
        Me.ParentIDNo.Name = "ParentIDNo"
        Me.ParentIDNo.ReadOnly = True
        Me.ParentIDNo.Width = 90
        '
        'ParentName
        '
        Me.ParentName.HeaderText = "Parent Name"
        Me.ParentName.Name = "ParentName"
        Me.ParentName.ReadOnly = True
        Me.ParentName.Width = 150
        '
        'ChildIDNo
        '
        Me.ChildIDNo.HeaderText = "Child IDNo"
        Me.ChildIDNo.Name = "ChildIDNo"
        Me.ChildIDNo.ReadOnly = True
        Me.ChildIDNo.Width = 80
        '
        'ChildName
        '
        Me.ChildName.HeaderText = "Child Name"
        Me.ChildName.Name = "ChildName"
        Me.ChildName.ReadOnly = True
        Me.ChildName.Width = 150
        '
        'NewParentIdNo
        '
        Me.NewParentIdNo.HeaderText = "New Parent IDNo"
        Me.NewParentIdNo.Name = "NewParentIdNo"
        Me.NewParentIdNo.ReadOnly = True
        '
        'NewParentName
        '
        Me.NewParentName.HeaderText = "New Parent Name"
        Me.NewParentName.Name = "NewParentName"
        Me.NewParentName.ReadOnly = True
        '
        'Status
        '
        Me.Status.HeaderText = "Status"
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'UpdatedBy
        '
        Me.UpdatedBy.HeaderText = "Updated By"
        Me.UpdatedBy.Name = "UpdatedBy"
        Me.UpdatedBy.ReadOnly = True
        '
        'UpdatedDate
        '
        Me.UpdatedDate.HeaderText = "Updated Date"
        Me.UpdatedDate.Name = "UpdatedDate"
        Me.UpdatedDate.ReadOnly = True
        Me.UpdatedDate.Width = 150
        '
        'ViewColumn
        '
        Me.ViewColumn.HeaderText = "*"
        Me.ViewColumn.Name = "ViewColumn"
        Me.ViewColumn.ReadOnly = True
        '
        'frmWBSParentIdChange
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1148, 501)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "frmWBSParentIdChange"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WESM Bills - Change Parent ID"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.dgv_wbsChangeParentId, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents dgv_wbsChangeParentId As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btn_Close As System.Windows.Forms.Button
    Friend WithEvents btn_Add As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmb_BillingPeriodNo As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtp_DateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_DateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ParentIDNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParentName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChildIDNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChildName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NewParentIdNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NewParentName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UpdatedBy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UpdatedDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ViewColumn As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents btn_Import As System.Windows.Forms.Button
End Class
