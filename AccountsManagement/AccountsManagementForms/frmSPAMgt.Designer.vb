<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSPAMgt
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
        Me.dtp_DateFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtbox_SPARate = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmb_ParticipantID = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgv_SPA_Mgt = New System.Windows.Forms.DataGridView()
        Me.CB_Select = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.WESMBillSummaryNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BillingPeriod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InvoiceNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChargeType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EndingBalance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButtn_VAT = New System.Windows.Forms.RadioButton()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.RadioButtn_Energy = New System.Windows.Forms.RadioButton()
        Me.txtbox_Months = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtbox_GrandTotal = New System.Windows.Forms.TextBox()
        Me.chkbox_SelectAll = New System.Windows.Forms.CheckBox()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.btn_Create = New System.Windows.Forms.Button()
        CType(Me.dgv_SPA_Mgt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtp_DateFrom
        '
        Me.dtp_DateFrom.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.dtp_DateFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_DateFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_DateFrom.Location = New System.Drawing.Point(437, 6)
        Me.dtp_DateFrom.Name = "dtp_DateFrom"
        Me.dtp_DateFrom.Size = New System.Drawing.Size(101, 20)
        Me.dtp_DateFrom.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(274, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "First Payment Date:"
        '
        'txtbox_SPARate
        '
        Me.txtbox_SPARate.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtbox_SPARate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbox_SPARate.ForeColor = System.Drawing.Color.DimGray
        Me.txtbox_SPARate.Location = New System.Drawing.Point(3, 3)
        Me.txtbox_SPARate.Name = "txtbox_SPARate"
        Me.txtbox_SPARate.Size = New System.Drawing.Size(89, 20)
        Me.txtbox_SPARate.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(3, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(122, 14)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Annual Interest Rate:"
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(274, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(157, 14)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Terms of Loan (in months):"
        '
        'cmb_ParticipantID
        '
        Me.cmb_ParticipantID.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmb_ParticipantID.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmb_ParticipantID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_ParticipantID.ForeColor = System.Drawing.Color.Black
        Me.cmb_ParticipantID.FormattingEnabled = True
        Me.cmb_ParticipantID.Location = New System.Drawing.Point(131, 5)
        Me.cmb_ParticipantID.Name = "cmb_ParticipantID"
        Me.cmb_ParticipantID.Size = New System.Drawing.Size(137, 22)
        Me.cmb_ParticipantID.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(3, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Participant ID:"
        '
        'dgv_SPA_Mgt
        '
        Me.dgv_SPA_Mgt.AllowUserToAddRows = False
        Me.dgv_SPA_Mgt.AllowUserToDeleteRows = False
        Me.dgv_SPA_Mgt.AllowUserToResizeColumns = False
        Me.dgv_SPA_Mgt.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_SPA_Mgt.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_SPA_Mgt.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_SPA_Mgt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_SPA_Mgt.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CB_Select, Me.WESMBillSummaryNo, Me.BillingPeriod, Me.InvoiceNo, Me.ChargeType, Me.DueDate, Me.EndingBalance})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LemonChiffon
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_SPA_Mgt.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_SPA_Mgt.Location = New System.Drawing.Point(3, 103)
        Me.dgv_SPA_Mgt.Name = "dgv_SPA_Mgt"
        Me.dgv_SPA_Mgt.RowHeadersVisible = False
        Me.dgv_SPA_Mgt.Size = New System.Drawing.Size(734, 223)
        Me.dgv_SPA_Mgt.TabIndex = 5
        '
        'CB_Select
        '
        Me.CB_Select.HeaderText = ""
        Me.CB_Select.Name = "CB_Select"
        Me.CB_Select.Width = 40
        '
        'WESMBillSummaryNo
        '
        Me.WESMBillSummaryNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.WESMBillSummaryNo.HeaderText = "WESMBillSummaryNo"
        Me.WESMBillSummaryNo.Name = "WESMBillSummaryNo"
        Me.WESMBillSummaryNo.Visible = False
        '
        'BillingPeriod
        '
        Me.BillingPeriod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.BillingPeriod.HeaderText = "BillingPeriod"
        Me.BillingPeriod.Name = "BillingPeriod"
        Me.BillingPeriod.ReadOnly = True
        Me.BillingPeriod.Width = 89
        '
        'InvoiceNo
        '
        Me.InvoiceNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.InvoiceNo.HeaderText = "InvoiceNo"
        Me.InvoiceNo.Name = "InvoiceNo"
        Me.InvoiceNo.ReadOnly = True
        Me.InvoiceNo.Width = 79
        '
        'ChargeType
        '
        Me.ChargeType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ChargeType.HeaderText = "ChargeType"
        Me.ChargeType.Name = "ChargeType"
        Me.ChargeType.ReadOnly = True
        Me.ChargeType.Width = 90
        '
        'DueDate
        '
        Me.DueDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DueDate.HeaderText = "DueDate"
        Me.DueDate.Name = "DueDate"
        Me.DueDate.ReadOnly = True
        Me.DueDate.Width = 73
        '
        'EndingBalance
        '
        Me.EndingBalance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.EndingBalance.HeaderText = "EndingBalance"
        Me.EndingBalance.Name = "EndingBalance"
        Me.EndingBalance.ReadOnly = True
        Me.EndingBalance.Width = 103
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.dgv_SPA_Mgt, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.chkbox_SelectAll, 0, 1)
        Me.TableLayoutPanel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 12)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(740, 369)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel3.ColumnCount = 6
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.RadioButtn_VAT, 5, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label8, 4, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.RadioButtn_Energy, 5, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.txtbox_Months, 3, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.dtp_DateFrom, 3, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label4, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label3, 2, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label2, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.cmb_ParticipantID, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel2, 1, 1)
        Me.TableLayoutPanel3.ForeColor = System.Drawing.Color.Black
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(734, 64)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'RadioButtn_VAT
        '
        Me.RadioButtn_VAT.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RadioButtn_VAT.AutoSize = True
        Me.RadioButtn_VAT.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButtn_VAT.ForeColor = System.Drawing.Color.Black
        Me.RadioButtn_VAT.Location = New System.Drawing.Point(629, 39)
        Me.RadioButtn_VAT.Name = "RadioButtn_VAT"
        Me.RadioButtn_VAT.Size = New System.Drawing.Size(45, 18)
        Me.RadioButtn_VAT.TabIndex = 6
        Me.RadioButtn_VAT.TabStop = True
        Me.RadioButtn_VAT.Text = "VAT"
        Me.RadioButtn_VAT.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(544, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(79, 14)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Charge Type:"
        '
        'RadioButtn_Energy
        '
        Me.RadioButtn_Energy.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RadioButtn_Energy.AutoSize = True
        Me.RadioButtn_Energy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButtn_Energy.ForeColor = System.Drawing.Color.Black
        Me.RadioButtn_Energy.Location = New System.Drawing.Point(629, 7)
        Me.RadioButtn_Energy.Name = "RadioButtn_Energy"
        Me.RadioButtn_Energy.Size = New System.Drawing.Size(59, 18)
        Me.RadioButtn_Energy.TabIndex = 5
        Me.RadioButtn_Energy.TabStop = True
        Me.RadioButtn_Energy.Text = "Energy"
        Me.RadioButtn_Energy.UseVisualStyleBackColor = True
        '
        'txtbox_Months
        '
        Me.txtbox_Months.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtbox_Months.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbox_Months.ForeColor = System.Drawing.Color.DimGray
        Me.txtbox_Months.Location = New System.Drawing.Point(437, 38)
        Me.txtbox_Months.Name = "txtbox_Months"
        Me.txtbox_Months.Size = New System.Drawing.Size(101, 20)
        Me.txtbox_Months.TabIndex = 4
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Label5, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.txtbox_SPARate, 0, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(131, 35)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(137, 26)
        Me.TableLayoutPanel2.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(98, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(16, 14)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "%"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtbox_GrandTotal)
        Me.Panel1.Location = New System.Drawing.Point(3, 332)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(734, 34)
        Me.Panel1.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(479, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 14)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Grand Total :"
        '
        'txtbox_GrandTotal
        '
        Me.txtbox_GrandTotal.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.txtbox_GrandTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtbox_GrandTotal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbox_GrandTotal.ForeColor = System.Drawing.Color.Black
        Me.txtbox_GrandTotal.Location = New System.Drawing.Point(562, 7)
        Me.txtbox_GrandTotal.Name = "txtbox_GrandTotal"
        Me.txtbox_GrandTotal.ReadOnly = True
        Me.txtbox_GrandTotal.Size = New System.Drawing.Size(169, 20)
        Me.txtbox_GrandTotal.TabIndex = 0
        Me.txtbox_GrandTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkbox_SelectAll
        '
        Me.chkbox_SelectAll.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkbox_SelectAll.AutoSize = True
        Me.chkbox_SelectAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbox_SelectAll.ForeColor = System.Drawing.Color.Black
        Me.chkbox_SelectAll.Location = New System.Drawing.Point(3, 76)
        Me.chkbox_SelectAll.Name = "chkbox_SelectAll"
        Me.chkbox_SelectAll.Size = New System.Drawing.Size(70, 18)
        Me.chkbox_SelectAll.TabIndex = 8
        Me.chkbox_SelectAll.Text = "Select All"
        Me.chkbox_SelectAll.UseVisualStyleBackColor = True
        '
        'btn_Cancel
        '
        Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Cancel.BackColor = System.Drawing.Color.White
        Me.btn_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Cancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Cancel.ForeColor = System.Drawing.Color.Black
        Me.btn_Cancel.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btn_Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Cancel.Location = New System.Drawing.Point(642, 387)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(110, 39)
        Me.btn_Cancel.TabIndex = 7
        Me.btn_Cancel.Text = "&Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = False
        '
        'btn_Create
        '
        Me.btn_Create.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Create.BackColor = System.Drawing.Color.White
        Me.btn_Create.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Create.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Create.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Create.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Create.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Create.ForeColor = System.Drawing.Color.Black
        Me.btn_Create.Image = Global.AccountsManagementForms.My.Resources.Resources.NewGreenIcon22x22
        Me.btn_Create.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Create.Location = New System.Drawing.Point(522, 387)
        Me.btn_Create.Name = "btn_Create"
        Me.btn_Create.Size = New System.Drawing.Size(110, 39)
        Me.btn_Create.TabIndex = 6
        Me.btn_Create.Text = "&Create"
        Me.btn_Create.UseVisualStyleBackColor = False
        '
        'frmSPAMgt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(767, 436)
        Me.ControlBox = False
        Me.Controls.Add(Me.btn_Cancel)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.btn_Create)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmSPAMgt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SPA Management"
        CType(Me.dgv_SPA_Mgt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtbox_SPARate As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtp_DateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmb_ParticipantID As System.Windows.Forms.ComboBox
    Friend WithEvents dgv_SPA_Mgt As System.Windows.Forms.DataGridView
    Friend WithEvents btn_Create As System.Windows.Forms.Button
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtbox_GrandTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtbox_Months As System.Windows.Forms.TextBox
    Friend WithEvents RadioButtn_Energy As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtn_VAT As System.Windows.Forms.RadioButton
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CB_Select As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents WESMBillSummaryNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BillingPeriod As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InvoiceNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChargeType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DueDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EndingBalance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkbox_SelectAll As System.Windows.Forms.CheckBox
End Class
