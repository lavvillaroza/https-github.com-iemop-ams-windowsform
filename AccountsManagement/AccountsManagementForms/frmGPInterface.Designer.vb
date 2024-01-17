<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGPInterface
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.gb_TransType = New System.Windows.Forms.GroupBox()
        Me.rb_WhTaxAdj = New System.Windows.Forms.RadioButton()
        Me.rb_SPA = New System.Windows.Forms.RadioButton()
        Me.rb_EarnedInterest = New System.Windows.Forms.RadioButton()
        Me.rb_nssInterest = New System.Windows.Forms.RadioButton()
        Me.rb_prudential = New System.Windows.Forms.RadioButton()
        Me.rb_Adjustment = New System.Windows.Forms.RadioButton()
        Me.rb_Payment = New System.Windows.Forms.RadioButton()
        Me.rb_Collection = New System.Windows.Forms.RadioButton()
        Me.rb_Offsetting = New System.Windows.Forms.RadioButton()
        Me.rb_Uploaded = New System.Windows.Forms.RadioButton()
        Me.dtp_From = New System.Windows.Forms.DateTimePicker()
        Me.dtp_To = New System.Windows.Forms.DateTimePicker()
        Me.gb_TransDate = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.gb_Status = New System.Windows.Forms.GroupBox()
        Me.rb_All = New System.Windows.Forms.RadioButton()
        Me.rb_NotPost = New System.Windows.Forms.RadioButton()
        Me.rb_Posted = New System.Windows.Forms.RadioButton()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.gb_ChargeType = New System.Windows.Forms.GroupBox()
        Me.cb_MFVat = New System.Windows.Forms.CheckBox()
        Me.cb_EVat = New System.Windows.Forms.CheckBox()
        Me.cb_MarketFees = New System.Windows.Forms.CheckBox()
        Me.cb_Energy = New System.Windows.Forms.CheckBox()
        Me.cmd_search = New System.Windows.Forms.Button()
        Me.cmd_close = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.cmd_UpdateGPRef = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_GPRefNo = New System.Windows.Forms.TextBox()
        Me.cmd_Details1 = New System.Windows.Forms.Button()
        Me.grp_Details = New System.Windows.Forms.GroupBox()
        Me.txt_BillPeriod = New System.Windows.Forms.TextBox()
        Me.lbl_BillingPeriod = New System.Windows.Forms.Label()
        Me.txt_BatchCode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_STLRun = New System.Windows.Forms.TextBox()
        Me.txt_DueDate = New System.Windows.Forms.TextBox()
        Me.txt_ChargeType = New System.Windows.Forms.TextBox()
        Me.lbl_STLRun = New System.Windows.Forms.Label()
        Me.lbl_DueDate = New System.Windows.Forms.Label()
        Me.lbl_ChargeType = New System.Windows.Forms.Label()
        Me.txt_Amount3 = New System.Windows.Forms.TextBox()
        Me.lbl_Amount3 = New System.Windows.Forms.Label()
        Me.txt_Amount2 = New System.Windows.Forms.TextBox()
        Me.txt_Amount1 = New System.Windows.Forms.TextBox()
        Me.lbl_Amount2 = New System.Windows.Forms.Label()
        Me.lbl_Amount1 = New System.Windows.Forms.Label()
        Me.dgv_ViewJV = New System.Windows.Forms.DataGridView()
        Me.cmd_GenReport = New System.Windows.Forms.Button()
        Me.cmd_Post = New System.Windows.Forms.Button()
        Me.gb_TransType.SuspendLayout()
        Me.gb_TransDate.SuspendLayout()
        Me.gb_Status.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.gb_ChargeType.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.grp_Details.SuspendLayout()
        CType(Me.dgv_ViewJV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gb_TransType
        '
        Me.gb_TransType.Controls.Add(Me.rb_WhTaxAdj)
        Me.gb_TransType.Controls.Add(Me.rb_SPA)
        Me.gb_TransType.Controls.Add(Me.rb_EarnedInterest)
        Me.gb_TransType.Controls.Add(Me.rb_nssInterest)
        Me.gb_TransType.Controls.Add(Me.rb_prudential)
        Me.gb_TransType.Controls.Add(Me.rb_Adjustment)
        Me.gb_TransType.Controls.Add(Me.rb_Payment)
        Me.gb_TransType.Controls.Add(Me.rb_Collection)
        Me.gb_TransType.Controls.Add(Me.rb_Offsetting)
        Me.gb_TransType.Controls.Add(Me.rb_Uploaded)
        Me.gb_TransType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb_TransType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.gb_TransType.Location = New System.Drawing.Point(6, 89)
        Me.gb_TransType.Name = "gb_TransType"
        Me.gb_TransType.Size = New System.Drawing.Size(170, 247)
        Me.gb_TransType.TabIndex = 0
        Me.gb_TransType.TabStop = False
        Me.gb_TransType.Text = "Transaction Type:"
        '
        'rb_WhTaxAdj
        '
        Me.rb_WhTaxAdj.AutoSize = True
        Me.rb_WhTaxAdj.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_WhTaxAdj.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rb_WhTaxAdj.Location = New System.Drawing.Point(17, 225)
        Me.rb_WhTaxAdj.Name = "rb_WhTaxAdj"
        Me.rb_WhTaxAdj.Size = New System.Drawing.Size(115, 18)
        Me.rb_WhTaxAdj.TabIndex = 9
        Me.rb_WhTaxAdj.TabStop = True
        Me.rb_WhTaxAdj.Text = "WHTax Adjustment"
        Me.rb_WhTaxAdj.UseVisualStyleBackColor = True
        '
        'rb_SPA
        '
        Me.rb_SPA.AutoSize = True
        Me.rb_SPA.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_SPA.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rb_SPA.Location = New System.Drawing.Point(17, 203)
        Me.rb_SPA.Name = "rb_SPA"
        Me.rb_SPA.Size = New System.Drawing.Size(45, 18)
        Me.rb_SPA.TabIndex = 8
        Me.rb_SPA.TabStop = True
        Me.rb_SPA.Text = "SPA"
        Me.rb_SPA.UseVisualStyleBackColor = True
        '
        'rb_EarnedInterest
        '
        Me.rb_EarnedInterest.AutoSize = True
        Me.rb_EarnedInterest.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_EarnedInterest.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rb_EarnedInterest.Location = New System.Drawing.Point(18, 181)
        Me.rb_EarnedInterest.Name = "rb_EarnedInterest"
        Me.rb_EarnedInterest.Size = New System.Drawing.Size(98, 18)
        Me.rb_EarnedInterest.TabIndex = 7
        Me.rb_EarnedInterest.TabStop = True
        Me.rb_EarnedInterest.Text = "Earned Interest"
        Me.rb_EarnedInterest.UseVisualStyleBackColor = True
        '
        'rb_nssInterest
        '
        Me.rb_nssInterest.AutoSize = True
        Me.rb_nssInterest.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_nssInterest.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rb_nssInterest.Location = New System.Drawing.Point(17, 158)
        Me.rb_nssInterest.Name = "rb_nssInterest"
        Me.rb_nssInterest.Size = New System.Drawing.Size(85, 18)
        Me.rb_nssInterest.TabIndex = 6
        Me.rb_nssInterest.TabStop = True
        Me.rb_nssInterest.Text = "NSS Interest"
        Me.rb_nssInterest.UseVisualStyleBackColor = True
        '
        'rb_prudential
        '
        Me.rb_prudential.AutoSize = True
        Me.rb_prudential.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_prudential.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rb_prudential.Location = New System.Drawing.Point(17, 135)
        Me.rb_prudential.Name = "rb_prudential"
        Me.rb_prudential.Size = New System.Drawing.Size(72, 18)
        Me.rb_prudential.TabIndex = 5
        Me.rb_prudential.TabStop = True
        Me.rb_prudential.Text = "Prudential"
        Me.rb_prudential.UseVisualStyleBackColor = True
        '
        'rb_Adjustment
        '
        Me.rb_Adjustment.AutoSize = True
        Me.rb_Adjustment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_Adjustment.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rb_Adjustment.Location = New System.Drawing.Point(17, 112)
        Me.rb_Adjustment.Name = "rb_Adjustment"
        Me.rb_Adjustment.Size = New System.Drawing.Size(79, 18)
        Me.rb_Adjustment.TabIndex = 4
        Me.rb_Adjustment.TabStop = True
        Me.rb_Adjustment.Text = "Adjustment"
        Me.rb_Adjustment.UseVisualStyleBackColor = True
        '
        'rb_Payment
        '
        Me.rb_Payment.AutoSize = True
        Me.rb_Payment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_Payment.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rb_Payment.Location = New System.Drawing.Point(17, 89)
        Me.rb_Payment.Name = "rb_Payment"
        Me.rb_Payment.Size = New System.Drawing.Size(66, 18)
        Me.rb_Payment.TabIndex = 3
        Me.rb_Payment.TabStop = True
        Me.rb_Payment.Text = "Payment"
        Me.rb_Payment.UseVisualStyleBackColor = True
        '
        'rb_Collection
        '
        Me.rb_Collection.AutoSize = True
        Me.rb_Collection.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_Collection.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rb_Collection.Location = New System.Drawing.Point(17, 66)
        Me.rb_Collection.Name = "rb_Collection"
        Me.rb_Collection.Size = New System.Drawing.Size(71, 18)
        Me.rb_Collection.TabIndex = 2
        Me.rb_Collection.TabStop = True
        Me.rb_Collection.Text = "Collection"
        Me.rb_Collection.UseVisualStyleBackColor = True
        '
        'rb_Offsetting
        '
        Me.rb_Offsetting.AutoSize = True
        Me.rb_Offsetting.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_Offsetting.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rb_Offsetting.Location = New System.Drawing.Point(17, 42)
        Me.rb_Offsetting.Name = "rb_Offsetting"
        Me.rb_Offsetting.Size = New System.Drawing.Size(73, 18)
        Me.rb_Offsetting.TabIndex = 1
        Me.rb_Offsetting.TabStop = True
        Me.rb_Offsetting.Text = "Offsetting"
        Me.rb_Offsetting.UseVisualStyleBackColor = True
        '
        'rb_Uploaded
        '
        Me.rb_Uploaded.AutoSize = True
        Me.rb_Uploaded.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_Uploaded.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rb_Uploaded.Location = New System.Drawing.Point(17, 19)
        Me.rb_Uploaded.Name = "rb_Uploaded"
        Me.rb_Uploaded.Size = New System.Drawing.Size(70, 18)
        Me.rb_Uploaded.TabIndex = 0
        Me.rb_Uploaded.TabStop = True
        Me.rb_Uploaded.Text = "Uploaded"
        Me.rb_Uploaded.UseVisualStyleBackColor = True
        '
        'dtp_From
        '
        Me.dtp_From.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_From.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_From.Location = New System.Drawing.Point(59, 16)
        Me.dtp_From.Name = "dtp_From"
        Me.dtp_From.Size = New System.Drawing.Size(87, 21)
        Me.dtp_From.TabIndex = 1
        '
        'dtp_To
        '
        Me.dtp_To.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_To.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_To.Location = New System.Drawing.Point(59, 43)
        Me.dtp_To.Name = "dtp_To"
        Me.dtp_To.Size = New System.Drawing.Size(87, 21)
        Me.dtp_To.TabIndex = 2
        '
        'gb_TransDate
        '
        Me.gb_TransDate.Controls.Add(Me.Label2)
        Me.gb_TransDate.Controls.Add(Me.Label1)
        Me.gb_TransDate.Controls.Add(Me.dtp_To)
        Me.gb_TransDate.Controls.Add(Me.dtp_From)
        Me.gb_TransDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb_TransDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.gb_TransDate.Location = New System.Drawing.Point(6, 9)
        Me.gb_TransDate.Name = "gb_TransDate"
        Me.gb_TransDate.Size = New System.Drawing.Size(170, 74)
        Me.gb_TransDate.TabIndex = 3
        Me.gb_TransDate.TabStop = False
        Me.gb_TransDate.Text = "JV Date:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(15, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "To:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(15, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "From:"
        '
        'gb_Status
        '
        Me.gb_Status.Controls.Add(Me.rb_All)
        Me.gb_Status.Controls.Add(Me.rb_NotPost)
        Me.gb_Status.Controls.Add(Me.rb_Posted)
        Me.gb_Status.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb_Status.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.gb_Status.Location = New System.Drawing.Point(6, 463)
        Me.gb_Status.Name = "gb_Status"
        Me.gb_Status.Size = New System.Drawing.Size(170, 90)
        Me.gb_Status.TabIndex = 4
        Me.gb_Status.TabStop = False
        Me.gb_Status.Text = "Status:"
        '
        'rb_All
        '
        Me.rb_All.AutoSize = True
        Me.rb_All.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_All.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rb_All.Location = New System.Drawing.Point(17, 63)
        Me.rb_All.Name = "rb_All"
        Me.rb_All.Size = New System.Drawing.Size(37, 18)
        Me.rb_All.TabIndex = 2
        Me.rb_All.Text = "All"
        Me.rb_All.UseVisualStyleBackColor = True
        '
        'rb_NotPost
        '
        Me.rb_NotPost.AutoSize = True
        Me.rb_NotPost.Checked = True
        Me.rb_NotPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_NotPost.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rb_NotPost.Location = New System.Drawing.Point(17, 17)
        Me.rb_NotPost.Name = "rb_NotPost"
        Me.rb_NotPost.Size = New System.Drawing.Size(78, 18)
        Me.rb_NotPost.TabIndex = 1
        Me.rb_NotPost.TabStop = True
        Me.rb_NotPost.Text = "Not-Posted"
        Me.rb_NotPost.UseVisualStyleBackColor = True
        '
        'rb_Posted
        '
        Me.rb_Posted.AutoSize = True
        Me.rb_Posted.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_Posted.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rb_Posted.Location = New System.Drawing.Point(17, 40)
        Me.rb_Posted.Name = "rb_Posted"
        Me.rb_Posted.Size = New System.Drawing.Size(58, 18)
        Me.rb_Posted.TabIndex = 0
        Me.rb_Posted.Text = "Posted"
        Me.rb_Posted.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.gb_ChargeType)
        Me.GroupBox4.Controls.Add(Me.gb_TransType)
        Me.GroupBox4.Controls.Add(Me.gb_TransDate)
        Me.GroupBox4.Controls.Add(Me.cmd_search)
        Me.GroupBox4.Controls.Add(Me.gb_Status)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 2)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(180, 604)
        Me.GroupBox4.TabIndex = 7
        Me.GroupBox4.TabStop = False
        '
        'gb_ChargeType
        '
        Me.gb_ChargeType.Controls.Add(Me.cb_MFVat)
        Me.gb_ChargeType.Controls.Add(Me.cb_EVat)
        Me.gb_ChargeType.Controls.Add(Me.cb_MarketFees)
        Me.gb_ChargeType.Controls.Add(Me.cb_Energy)
        Me.gb_ChargeType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb_ChargeType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.gb_ChargeType.Location = New System.Drawing.Point(6, 342)
        Me.gb_ChargeType.Name = "gb_ChargeType"
        Me.gb_ChargeType.Size = New System.Drawing.Size(170, 115)
        Me.gb_ChargeType.TabIndex = 5
        Me.gb_ChargeType.TabStop = False
        Me.gb_ChargeType.Text = "Charge Type:"
        '
        'cb_MFVat
        '
        Me.cb_MFVat.AutoSize = True
        Me.cb_MFVat.Checked = True
        Me.cb_MFVat.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cb_MFVat.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_MFVat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cb_MFVat.Location = New System.Drawing.Point(17, 87)
        Me.cb_MFVat.Name = "cb_MFVat"
        Me.cb_MFVat.Size = New System.Drawing.Size(123, 18)
        Me.cb_MFVat.TabIndex = 3
        Me.cb_MFVat.Text = "VAT on Market Fees"
        Me.cb_MFVat.UseVisualStyleBackColor = True
        '
        'cb_EVat
        '
        Me.cb_EVat.AutoSize = True
        Me.cb_EVat.Checked = True
        Me.cb_EVat.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cb_EVat.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_EVat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cb_EVat.Location = New System.Drawing.Point(17, 42)
        Me.cb_EVat.Name = "cb_EVat"
        Me.cb_EVat.Size = New System.Drawing.Size(98, 18)
        Me.cb_EVat.TabIndex = 2
        Me.cb_EVat.Text = "VAT on Energy"
        Me.cb_EVat.UseVisualStyleBackColor = True
        '
        'cb_MarketFees
        '
        Me.cb_MarketFees.AutoSize = True
        Me.cb_MarketFees.Checked = True
        Me.cb_MarketFees.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cb_MarketFees.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_MarketFees.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cb_MarketFees.Location = New System.Drawing.Point(17, 66)
        Me.cb_MarketFees.Name = "cb_MarketFees"
        Me.cb_MarketFees.Size = New System.Drawing.Size(85, 18)
        Me.cb_MarketFees.TabIndex = 1
        Me.cb_MarketFees.Text = "Market Fees"
        Me.cb_MarketFees.UseVisualStyleBackColor = True
        '
        'cb_Energy
        '
        Me.cb_Energy.AutoSize = True
        Me.cb_Energy.Checked = True
        Me.cb_Energy.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cb_Energy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_Energy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cb_Energy.Location = New System.Drawing.Point(17, 19)
        Me.cb_Energy.Name = "cb_Energy"
        Me.cb_Energy.Size = New System.Drawing.Size(60, 18)
        Me.cb_Energy.TabIndex = 0
        Me.cb_Energy.Text = "Energy"
        Me.cb_Energy.UseVisualStyleBackColor = True
        '
        'cmd_search
        '
        Me.cmd_search.BackColor = System.Drawing.Color.White
        Me.cmd_search.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_search.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_search.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_search.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_search.ForeColor = System.Drawing.Color.Black
        Me.cmd_search.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.cmd_search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_search.Location = New System.Drawing.Point(6, 559)
        Me.cmd_search.Name = "cmd_search"
        Me.cmd_search.Size = New System.Drawing.Size(168, 39)
        Me.cmd_search.TabIndex = 5
        Me.cmd_search.Text = "Search"
        Me.cmd_search.UseVisualStyleBackColor = False
        '
        'cmd_close
        '
        Me.cmd_close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_close.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_close.ForeColor = System.Drawing.Color.Black
        Me.cmd_close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_close.Location = New System.Drawing.Point(725, 555)
        Me.cmd_close.Name = "cmd_close"
        Me.cmd_close.Size = New System.Drawing.Size(168, 39)
        Me.cmd_close.TabIndex = 6
        Me.cmd_close.Text = "Close"
        Me.cmd_close.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.cmd_UpdateGPRef)
        Me.GroupBox5.Controls.Add(Me.Label4)
        Me.GroupBox5.Controls.Add(Me.cmd_close)
        Me.GroupBox5.Controls.Add(Me.txt_GPRefNo)
        Me.GroupBox5.Controls.Add(Me.cmd_Details1)
        Me.GroupBox5.Controls.Add(Me.grp_Details)
        Me.GroupBox5.Controls.Add(Me.dgv_ViewJV)
        Me.GroupBox5.Controls.Add(Me.cmd_GenReport)
        Me.GroupBox5.Controls.Add(Me.cmd_Post)
        Me.GroupBox5.Location = New System.Drawing.Point(201, 1)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(905, 605)
        Me.GroupBox5.TabIndex = 8
        Me.GroupBox5.TabStop = False
        '
        'cmd_UpdateGPRef
        '
        Me.cmd_UpdateGPRef.BackColor = System.Drawing.Color.White
        Me.cmd_UpdateGPRef.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_UpdateGPRef.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_UpdateGPRef.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_UpdateGPRef.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_UpdateGPRef.Image = Global.AccountsManagementForms.My.Resources.Resources.EditDocumentColored22x22
        Me.cmd_UpdateGPRef.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_UpdateGPRef.Location = New System.Drawing.Point(301, 395)
        Me.cmd_UpdateGPRef.Name = "cmd_UpdateGPRef"
        Me.cmd_UpdateGPRef.Size = New System.Drawing.Size(35, 30)
        Me.cmd_UpdateGPRef.TabIndex = 38
        Me.cmd_UpdateGPRef.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(4, 403)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(102, 14)
        Me.Label4.TabIndex = 47
        Me.Label4.Text = "GP Reference No:"
        '
        'txt_GPRefNo
        '
        Me.txt_GPRefNo.BackColor = System.Drawing.Color.White
        Me.txt_GPRefNo.Location = New System.Drawing.Point(105, 400)
        Me.txt_GPRefNo.MaxLength = 26
        Me.txt_GPRefNo.Name = "txt_GPRefNo"
        Me.txt_GPRefNo.Size = New System.Drawing.Size(190, 20)
        Me.txt_GPRefNo.TabIndex = 43
        Me.txt_GPRefNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmd_Details1
        '
        Me.cmd_Details1.BackColor = System.Drawing.Color.White
        Me.cmd_Details1.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Details1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Details1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Details1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Details1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Details1.ForeColor = System.Drawing.Color.Black
        Me.cmd_Details1.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.cmd_Details1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Details1.Location = New System.Drawing.Point(593, 396)
        Me.cmd_Details1.Name = "cmd_Details1"
        Me.cmd_Details1.Size = New System.Drawing.Size(150, 39)
        Me.cmd_Details1.TabIndex = 18
        Me.cmd_Details1.Text = "Report Details"
        Me.cmd_Details1.UseVisualStyleBackColor = False
        '
        'grp_Details
        '
        Me.grp_Details.Controls.Add(Me.txt_BillPeriod)
        Me.grp_Details.Controls.Add(Me.lbl_BillingPeriod)
        Me.grp_Details.Controls.Add(Me.txt_BatchCode)
        Me.grp_Details.Controls.Add(Me.Label3)
        Me.grp_Details.Controls.Add(Me.txt_STLRun)
        Me.grp_Details.Controls.Add(Me.txt_DueDate)
        Me.grp_Details.Controls.Add(Me.txt_ChargeType)
        Me.grp_Details.Controls.Add(Me.lbl_STLRun)
        Me.grp_Details.Controls.Add(Me.lbl_DueDate)
        Me.grp_Details.Controls.Add(Me.lbl_ChargeType)
        Me.grp_Details.Controls.Add(Me.txt_Amount3)
        Me.grp_Details.Controls.Add(Me.lbl_Amount3)
        Me.grp_Details.Controls.Add(Me.txt_Amount2)
        Me.grp_Details.Controls.Add(Me.txt_Amount1)
        Me.grp_Details.Controls.Add(Me.lbl_Amount2)
        Me.grp_Details.Controls.Add(Me.lbl_Amount1)
        Me.grp_Details.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grp_Details.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.grp_Details.Location = New System.Drawing.Point(7, 439)
        Me.grp_Details.Name = "grp_Details"
        Me.grp_Details.Size = New System.Drawing.Size(892, 104)
        Me.grp_Details.TabIndex = 46
        Me.grp_Details.TabStop = False
        Me.grp_Details.Text = "Transaction Details"
        '
        'txt_BillPeriod
        '
        Me.txt_BillPeriod.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_BillPeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_BillPeriod.ForeColor = System.Drawing.Color.Black
        Me.txt_BillPeriod.Location = New System.Drawing.Point(94, 47)
        Me.txt_BillPeriod.Name = "txt_BillPeriod"
        Me.txt_BillPeriod.ReadOnly = True
        Me.txt_BillPeriod.Size = New System.Drawing.Size(190, 20)
        Me.txt_BillPeriod.TabIndex = 17
        Me.txt_BillPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_BillingPeriod
        '
        Me.lbl_BillingPeriod.AutoSize = True
        Me.lbl_BillingPeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_BillingPeriod.ForeColor = System.Drawing.Color.Black
        Me.lbl_BillingPeriod.Location = New System.Drawing.Point(6, 49)
        Me.lbl_BillingPeriod.Name = "lbl_BillingPeriod"
        Me.lbl_BillingPeriod.Size = New System.Drawing.Size(82, 14)
        Me.lbl_BillingPeriod.TabIndex = 16
        Me.lbl_BillingPeriod.Text = "Billing Period:"
        '
        'txt_BatchCode
        '
        Me.txt_BatchCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_BatchCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_BatchCode.ForeColor = System.Drawing.Color.Black
        Me.txt_BatchCode.Location = New System.Drawing.Point(94, 21)
        Me.txt_BatchCode.Name = "txt_BatchCode"
        Me.txt_BatchCode.ReadOnly = True
        Me.txt_BatchCode.Size = New System.Drawing.Size(190, 20)
        Me.txt_BatchCode.TabIndex = 13
        Me.txt_BatchCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(15, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 14)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Batch Code:"
        '
        'txt_STLRun
        '
        Me.txt_STLRun.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_STLRun.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_STLRun.ForeColor = System.Drawing.Color.Black
        Me.txt_STLRun.Location = New System.Drawing.Point(390, 72)
        Me.txt_STLRun.Name = "txt_STLRun"
        Me.txt_STLRun.ReadOnly = True
        Me.txt_STLRun.Size = New System.Drawing.Size(190, 20)
        Me.txt_STLRun.TabIndex = 11
        Me.txt_STLRun.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_DueDate
        '
        Me.txt_DueDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_DueDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_DueDate.ForeColor = System.Drawing.Color.Black
        Me.txt_DueDate.Location = New System.Drawing.Point(390, 46)
        Me.txt_DueDate.Name = "txt_DueDate"
        Me.txt_DueDate.ReadOnly = True
        Me.txt_DueDate.Size = New System.Drawing.Size(190, 20)
        Me.txt_DueDate.TabIndex = 10
        Me.txt_DueDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_ChargeType
        '
        Me.txt_ChargeType.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_ChargeType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_ChargeType.ForeColor = System.Drawing.Color.Black
        Me.txt_ChargeType.Location = New System.Drawing.Point(390, 21)
        Me.txt_ChargeType.Name = "txt_ChargeType"
        Me.txt_ChargeType.ReadOnly = True
        Me.txt_ChargeType.Size = New System.Drawing.Size(190, 20)
        Me.txt_ChargeType.TabIndex = 9
        Me.txt_ChargeType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_STLRun
        '
        Me.lbl_STLRun.AutoSize = True
        Me.lbl_STLRun.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_STLRun.ForeColor = System.Drawing.Color.Black
        Me.lbl_STLRun.Location = New System.Drawing.Point(327, 75)
        Me.lbl_STLRun.Name = "lbl_STLRun"
        Me.lbl_STLRun.Size = New System.Drawing.Size(55, 14)
        Me.lbl_STLRun.TabIndex = 8
        Me.lbl_STLRun.Text = "STL Run:"
        '
        'lbl_DueDate
        '
        Me.lbl_DueDate.AutoSize = True
        Me.lbl_DueDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DueDate.ForeColor = System.Drawing.Color.Black
        Me.lbl_DueDate.Location = New System.Drawing.Point(325, 48)
        Me.lbl_DueDate.Name = "lbl_DueDate"
        Me.lbl_DueDate.Size = New System.Drawing.Size(58, 14)
        Me.lbl_DueDate.TabIndex = 7
        Me.lbl_DueDate.Text = "Due Date:"
        '
        'lbl_ChargeType
        '
        Me.lbl_ChargeType.AutoSize = True
        Me.lbl_ChargeType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ChargeType.ForeColor = System.Drawing.Color.Black
        Me.lbl_ChargeType.Location = New System.Drawing.Point(306, 22)
        Me.lbl_ChargeType.Name = "lbl_ChargeType"
        Me.lbl_ChargeType.Size = New System.Drawing.Size(79, 14)
        Me.lbl_ChargeType.TabIndex = 6
        Me.lbl_ChargeType.Text = "Charge Type:"
        '
        'txt_Amount3
        '
        Me.txt_Amount3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_Amount3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Amount3.ForeColor = System.Drawing.Color.Black
        Me.txt_Amount3.Location = New System.Drawing.Point(696, 72)
        Me.txt_Amount3.Name = "txt_Amount3"
        Me.txt_Amount3.ReadOnly = True
        Me.txt_Amount3.Size = New System.Drawing.Size(190, 20)
        Me.txt_Amount3.TabIndex = 5
        Me.txt_Amount3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_Amount3
        '
        Me.lbl_Amount3.AutoSize = True
        Me.lbl_Amount3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Amount3.ForeColor = System.Drawing.Color.Black
        Me.lbl_Amount3.Location = New System.Drawing.Point(627, 76)
        Me.lbl_Amount3.Name = "lbl_Amount3"
        Me.lbl_Amount3.Size = New System.Drawing.Size(60, 14)
        Me.lbl_Amount3.TabIndex = 4
        Me.lbl_Amount3.Text = "Total NSS:"
        '
        'txt_Amount2
        '
        Me.txt_Amount2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_Amount2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Amount2.ForeColor = System.Drawing.Color.Black
        Me.txt_Amount2.Location = New System.Drawing.Point(696, 46)
        Me.txt_Amount2.Name = "txt_Amount2"
        Me.txt_Amount2.ReadOnly = True
        Me.txt_Amount2.Size = New System.Drawing.Size(190, 20)
        Me.txt_Amount2.TabIndex = 3
        Me.txt_Amount2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_Amount1
        '
        Me.txt_Amount1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_Amount1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Amount1.ForeColor = System.Drawing.Color.Black
        Me.txt_Amount1.Location = New System.Drawing.Point(696, 20)
        Me.txt_Amount1.Name = "txt_Amount1"
        Me.txt_Amount1.ReadOnly = True
        Me.txt_Amount1.Size = New System.Drawing.Size(190, 20)
        Me.txt_Amount1.TabIndex = 2
        Me.txt_Amount1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_Amount2
        '
        Me.lbl_Amount2.AutoSize = True
        Me.lbl_Amount2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Amount2.ForeColor = System.Drawing.Color.Black
        Me.lbl_Amount2.Location = New System.Drawing.Point(633, 48)
        Me.lbl_Amount2.Name = "lbl_Amount2"
        Me.lbl_Amount2.Size = New System.Drawing.Size(54, 14)
        Me.lbl_Amount2.TabIndex = 1
        Me.lbl_Amount2.Text = "Total AR:"
        '
        'lbl_Amount1
        '
        Me.lbl_Amount1.AutoSize = True
        Me.lbl_Amount1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Amount1.ForeColor = System.Drawing.Color.Black
        Me.lbl_Amount1.Location = New System.Drawing.Point(634, 22)
        Me.lbl_Amount1.Name = "lbl_Amount1"
        Me.lbl_Amount1.Size = New System.Drawing.Size(54, 14)
        Me.lbl_Amount1.TabIndex = 0
        Me.lbl_Amount1.Text = "Total AP:"
        '
        'dgv_ViewJV
        '
        Me.dgv_ViewJV.AllowUserToAddRows = False
        Me.dgv_ViewJV.AllowUserToDeleteRows = False
        Me.dgv_ViewJV.AllowUserToResizeRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_ViewJV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_ViewJV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_ViewJV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv_ViewJV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_ViewJV.Location = New System.Drawing.Point(6, 15)
        Me.dgv_ViewJV.Name = "dgv_ViewJV"
        Me.dgv_ViewJV.ReadOnly = True
        Me.dgv_ViewJV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_ViewJV.Size = New System.Drawing.Size(893, 373)
        Me.dgv_ViewJV.TabIndex = 0
        '
        'cmd_GenReport
        '
        Me.cmd_GenReport.BackColor = System.Drawing.Color.White
        Me.cmd_GenReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_GenReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_GenReport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_GenReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_GenReport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_GenReport.ForeColor = System.Drawing.Color.Black
        Me.cmd_GenReport.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.cmd_GenReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_GenReport.Location = New System.Drawing.Point(749, 396)
        Me.cmd_GenReport.Name = "cmd_GenReport"
        Me.cmd_GenReport.Size = New System.Drawing.Size(150, 39)
        Me.cmd_GenReport.TabIndex = 44
        Me.cmd_GenReport.Text = "Journal Voucher"
        Me.cmd_GenReport.UseVisualStyleBackColor = False
        '
        'cmd_Post
        '
        Me.cmd_Post.BackColor = System.Drawing.Color.White
        Me.cmd_Post.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Post.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Post.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Post.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Post.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Post.ForeColor = System.Drawing.Color.Black
        Me.cmd_Post.Image = Global.AccountsManagementForms.My.Resources.Resources.PostIcon22x22
        Me.cmd_Post.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Post.Location = New System.Drawing.Point(437, 396)
        Me.cmd_Post.Name = "cmd_Post"
        Me.cmd_Post.Size = New System.Drawing.Size(150, 39)
        Me.cmd_Post.TabIndex = 45
        Me.cmd_Post.Text = "Post To GP"
        Me.cmd_Post.UseVisualStyleBackColor = False
        '
        'frmGPInterface
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1116, 618)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGPInterface"
        Me.Text = "Journal Posting Interface"
        Me.gb_TransType.ResumeLayout(False)
        Me.gb_TransType.PerformLayout()
        Me.gb_TransDate.ResumeLayout(False)
        Me.gb_TransDate.PerformLayout()
        Me.gb_Status.ResumeLayout(False)
        Me.gb_Status.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.gb_ChargeType.ResumeLayout(False)
        Me.gb_ChargeType.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.grp_Details.ResumeLayout(False)
        Me.grp_Details.PerformLayout()
        CType(Me.dgv_ViewJV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gb_TransType As System.Windows.Forms.GroupBox
    Friend WithEvents dtp_From As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_To As System.Windows.Forms.DateTimePicker
    Friend WithEvents gb_TransDate As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents gb_Status As System.Windows.Forms.GroupBox
    Friend WithEvents cmd_search As System.Windows.Forms.Button
    Friend WithEvents cmd_close As System.Windows.Forms.Button
    Friend WithEvents rb_All As System.Windows.Forms.RadioButton
    Friend WithEvents rb_NotPost As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Posted As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents gb_ChargeType As System.Windows.Forms.GroupBox
    Friend WithEvents cb_MFVat As System.Windows.Forms.CheckBox
    Friend WithEvents cb_EVat As System.Windows.Forms.CheckBox
    Friend WithEvents cb_MarketFees As System.Windows.Forms.CheckBox
    Friend WithEvents cb_Energy As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents dgv_ViewJV As System.Windows.Forms.DataGridView
    Friend WithEvents cmd_GenReport As System.Windows.Forms.Button
    Friend WithEvents txt_GPRefNo As System.Windows.Forms.TextBox
    Friend WithEvents cmd_UpdateGPRef As System.Windows.Forms.Button
    Friend WithEvents rb_Adjustment As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Payment As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Collection As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Offsetting As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Uploaded As System.Windows.Forms.RadioButton
    Friend WithEvents rb_prudential As System.Windows.Forms.RadioButton
    Friend WithEvents cmd_Post As System.Windows.Forms.Button
    Friend WithEvents grp_Details As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_Amount1 As System.Windows.Forms.Label
    Friend WithEvents txt_STLRun As System.Windows.Forms.TextBox
    Friend WithEvents txt_DueDate As System.Windows.Forms.TextBox
    Friend WithEvents txt_ChargeType As System.Windows.Forms.TextBox
    Friend WithEvents lbl_STLRun As System.Windows.Forms.Label
    Friend WithEvents lbl_DueDate As System.Windows.Forms.Label
    Friend WithEvents lbl_ChargeType As System.Windows.Forms.Label
    Friend WithEvents txt_Amount3 As System.Windows.Forms.TextBox
    Friend WithEvents lbl_Amount3 As System.Windows.Forms.Label
    Friend WithEvents txt_Amount2 As System.Windows.Forms.TextBox
    Friend WithEvents txt_Amount1 As System.Windows.Forms.TextBox
    Friend WithEvents lbl_Amount2 As System.Windows.Forms.Label
    Friend WithEvents txt_BillPeriod As System.Windows.Forms.TextBox
    Friend WithEvents lbl_BillingPeriod As System.Windows.Forms.Label
    Friend WithEvents txt_BatchCode As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmd_Details1 As System.Windows.Forms.Button
    Friend WithEvents rb_nssInterest As System.Windows.Forms.RadioButton
    Friend WithEvents rb_EarnedInterest As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents rb_SPA As System.Windows.Forms.RadioButton
    Friend WithEvents rb_WhTaxAdj As System.Windows.Forms.RadioButton
End Class
