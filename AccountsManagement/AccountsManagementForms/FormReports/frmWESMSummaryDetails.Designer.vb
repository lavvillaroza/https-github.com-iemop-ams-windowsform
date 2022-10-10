<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWESMSummaryDetails
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
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.tc_Transactions = New System.Windows.Forms.TabControl()
        Me.tpOffsetting = New System.Windows.Forms.TabPage()
        Me.cmd_GenOffsetRPT = New System.Windows.Forms.Button()
        Me.dgv_OffsettingTransaction = New System.Windows.Forms.DataGridView()
        Me.tpCollection = New System.Windows.Forms.TabPage()
        Me.dgv_CollectionTransactions = New System.Windows.Forms.DataGridView()
        Me.tpPayment = New System.Windows.Forms.TabPage()
        Me.dgv_PaymentTransactions = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txt_EWtax = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txt_TransDate = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt_isAdjustment = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txt_ChargeType = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txt_BillPeriod = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_NewDueDate = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_DueDate = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_OutstandingBal = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_BegBalance = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_RefNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_ParticipantID = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_IDNumber = New System.Windows.Forms.TextBox()
        Me.cmd_close = New System.Windows.Forms.Button()
        Me.tc_Transactions.SuspendLayout()
        Me.tpOffsetting.SuspendLayout()
        CType(Me.dgv_OffsettingTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpCollection.SuspendLayout()
        CType(Me.dgv_CollectionTransactions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpPayment.SuspendLayout()
        CType(Me.dgv_PaymentTransactions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tc_Transactions
        '
        Me.tc_Transactions.Controls.Add(Me.tpOffsetting)
        Me.tc_Transactions.Controls.Add(Me.tpCollection)
        Me.tc_Transactions.Controls.Add(Me.tpPayment)
        Me.tc_Transactions.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tc_Transactions.Location = New System.Drawing.Point(15, 206)
        Me.tc_Transactions.Name = "tc_Transactions"
        Me.tc_Transactions.SelectedIndex = 0
        Me.tc_Transactions.Size = New System.Drawing.Size(901, 385)
        Me.tc_Transactions.TabIndex = 0
        '
        'tpOffsetting
        '
        Me.tpOffsetting.Controls.Add(Me.cmd_GenOffsetRPT)
        Me.tpOffsetting.Controls.Add(Me.dgv_OffsettingTransaction)
        Me.tpOffsetting.Location = New System.Drawing.Point(4, 21)
        Me.tpOffsetting.Name = "tpOffsetting"
        Me.tpOffsetting.Padding = New System.Windows.Forms.Padding(3)
        Me.tpOffsetting.Size = New System.Drawing.Size(893, 360)
        Me.tpOffsetting.TabIndex = 0
        Me.tpOffsetting.Text = "Offsetting Transactions"
        Me.tpOffsetting.UseVisualStyleBackColor = True
        '
        'cmd_GenOffsetRPT
        '
        Me.cmd_GenOffsetRPT.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_GenOffsetRPT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_GenOffsetRPT.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_GenOffsetRPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_GenOffsetRPT.Image = Global.AccountsManagementForms.My.Resources.Resources.report1
        Me.cmd_GenOffsetRPT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_GenOffsetRPT.Location = New System.Drawing.Point(739, 321)
        Me.cmd_GenOffsetRPT.Name = "cmd_GenOffsetRPT"
        Me.cmd_GenOffsetRPT.Size = New System.Drawing.Size(148, 34)
        Me.cmd_GenOffsetRPT.TabIndex = 3
        Me.cmd_GenOffsetRPT.Text = "Generate Report"
        Me.cmd_GenOffsetRPT.UseVisualStyleBackColor = True
        '
        'dgv_OffsettingTransaction
        '
        Me.dgv_OffsettingTransaction.AllowUserToAddRows = False
        Me.dgv_OffsettingTransaction.AllowUserToDeleteRows = False
        DataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_OffsettingTransaction.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle13
        Me.dgv_OffsettingTransaction.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_OffsettingTransaction.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle14
        Me.dgv_OffsettingTransaction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_OffsettingTransaction.Location = New System.Drawing.Point(5, 6)
        Me.dgv_OffsettingTransaction.Name = "dgv_OffsettingTransaction"
        Me.dgv_OffsettingTransaction.ReadOnly = True
        Me.dgv_OffsettingTransaction.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_OffsettingTransaction.Size = New System.Drawing.Size(882, 309)
        Me.dgv_OffsettingTransaction.TabIndex = 2
        '
        'tpCollection
        '
        Me.tpCollection.Controls.Add(Me.dgv_CollectionTransactions)
        Me.tpCollection.Location = New System.Drawing.Point(4, 21)
        Me.tpCollection.Name = "tpCollection"
        Me.tpCollection.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCollection.Size = New System.Drawing.Size(893, 360)
        Me.tpCollection.TabIndex = 1
        Me.tpCollection.Text = "Collection Transactions"
        Me.tpCollection.UseVisualStyleBackColor = True
        '
        'dgv_CollectionTransactions
        '
        Me.dgv_CollectionTransactions.AllowUserToAddRows = False
        Me.dgv_CollectionTransactions.AllowUserToDeleteRows = False
        DataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_CollectionTransactions.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle15
        Me.dgv_CollectionTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_CollectionTransactions.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle16
        Me.dgv_CollectionTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_CollectionTransactions.Location = New System.Drawing.Point(5, 6)
        Me.dgv_CollectionTransactions.Name = "dgv_CollectionTransactions"
        Me.dgv_CollectionTransactions.ReadOnly = True
        Me.dgv_CollectionTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_CollectionTransactions.Size = New System.Drawing.Size(882, 309)
        Me.dgv_CollectionTransactions.TabIndex = 1
        '
        'tpPayment
        '
        Me.tpPayment.Controls.Add(Me.dgv_PaymentTransactions)
        Me.tpPayment.Location = New System.Drawing.Point(4, 21)
        Me.tpPayment.Name = "tpPayment"
        Me.tpPayment.Size = New System.Drawing.Size(893, 360)
        Me.tpPayment.TabIndex = 2
        Me.tpPayment.Text = "Payment Transactions"
        Me.tpPayment.UseVisualStyleBackColor = True
        '
        'dgv_PaymentTransactions
        '
        Me.dgv_PaymentTransactions.AllowUserToAddRows = False
        Me.dgv_PaymentTransactions.AllowUserToDeleteRows = False
        DataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_PaymentTransactions.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle17
        Me.dgv_PaymentTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle18.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_PaymentTransactions.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle18
        Me.dgv_PaymentTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_PaymentTransactions.Location = New System.Drawing.Point(5, 6)
        Me.dgv_PaymentTransactions.Name = "dgv_PaymentTransactions"
        Me.dgv_PaymentTransactions.ReadOnly = True
        Me.dgv_PaymentTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_PaymentTransactions.Size = New System.Drawing.Size(885, 309)
        Me.dgv_PaymentTransactions.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txt_EWtax)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txt_TransDate)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txt_isAdjustment)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txt_ChargeType)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txt_BillPeriod)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txt_NewDueDate)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txt_DueDate)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txt_OutstandingBal)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txt_BegBalance)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txt_RefNo)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txt_ParticipantID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txt_IDNumber)
        Me.GroupBox1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(15, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(901, 188)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(589, 157)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(138, 14)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "Energy Withholding Tax:"
        '
        'txt_EWtax
        '
        Me.txt_EWtax.BackColor = System.Drawing.SystemColors.Info
        Me.txt_EWtax.Location = New System.Drawing.Point(733, 154)
        Me.txt_EWtax.Name = "txt_EWtax"
        Me.txt_EWtax.ReadOnly = True
        Me.txt_EWtax.Size = New System.Drawing.Size(162, 21)
        Me.txt_EWtax.TabIndex = 26
        Me.txt_EWtax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(314, 25)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(101, 14)
        Me.Label11.TabIndex = 25
        Me.Label11.Text = "Transaction Date:"
        '
        'txt_TransDate
        '
        Me.txt_TransDate.BackColor = System.Drawing.SystemColors.Info
        Me.txt_TransDate.Location = New System.Drawing.Point(421, 22)
        Me.txt_TransDate.Name = "txt_TransDate"
        Me.txt_TransDate.ReadOnly = True
        Me.txt_TransDate.Size = New System.Drawing.Size(162, 21)
        Me.txt_TransDate.TabIndex = 24
        Me.txt_TransDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(654, 25)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 14)
        Me.Label10.TabIndex = 23
        Me.Label10.Text = "Adjustment:"
        '
        'txt_isAdjustment
        '
        Me.txt_isAdjustment.BackColor = System.Drawing.SystemColors.Info
        Me.txt_isAdjustment.Location = New System.Drawing.Point(733, 22)
        Me.txt_isAdjustment.Name = "txt_isAdjustment"
        Me.txt_isAdjustment.ReadOnly = True
        Me.txt_isAdjustment.Size = New System.Drawing.Size(162, 21)
        Me.txt_isAdjustment.TabIndex = 22
        Me.txt_isAdjustment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(38, 103)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(78, 14)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Charge Type:"
        '
        'txt_ChargeType
        '
        Me.txt_ChargeType.BackColor = System.Drawing.SystemColors.Info
        Me.txt_ChargeType.Location = New System.Drawing.Point(122, 100)
        Me.txt_ChargeType.Name = "txt_ChargeType"
        Me.txt_ChargeType.ReadOnly = True
        Me.txt_ChargeType.Size = New System.Drawing.Size(162, 21)
        Me.txt_ChargeType.TabIndex = 20
        Me.txt_ChargeType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(34, 77)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(82, 14)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Billing Period:"
        '
        'txt_BillPeriod
        '
        Me.txt_BillPeriod.BackColor = System.Drawing.SystemColors.Info
        Me.txt_BillPeriod.Location = New System.Drawing.Point(122, 74)
        Me.txt_BillPeriod.Name = "txt_BillPeriod"
        Me.txt_BillPeriod.ReadOnly = True
        Me.txt_BillPeriod.Size = New System.Drawing.Size(162, 21)
        Me.txt_BillPeriod.TabIndex = 18
        Me.txt_BillPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(642, 77)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 14)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "New Due Date:"
        '
        'txt_NewDueDate
        '
        Me.txt_NewDueDate.BackColor = System.Drawing.SystemColors.Info
        Me.txt_NewDueDate.Location = New System.Drawing.Point(733, 74)
        Me.txt_NewDueDate.Name = "txt_NewDueDate"
        Me.txt_NewDueDate.ReadOnly = True
        Me.txt_NewDueDate.Size = New System.Drawing.Size(162, 21)
        Me.txt_NewDueDate.TabIndex = 16
        Me.txt_NewDueDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(356, 77)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 14)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Due Date:"
        '
        'txt_DueDate
        '
        Me.txt_DueDate.BackColor = System.Drawing.SystemColors.Info
        Me.txt_DueDate.Location = New System.Drawing.Point(421, 74)
        Me.txt_DueDate.Name = "txt_DueDate"
        Me.txt_DueDate.ReadOnly = True
        Me.txt_DueDate.Size = New System.Drawing.Size(162, 21)
        Me.txt_DueDate.TabIndex = 14
        Me.txt_DueDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(292, 157)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(123, 14)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Outstanding Balance:"
        '
        'txt_OutstandingBal
        '
        Me.txt_OutstandingBal.BackColor = System.Drawing.SystemColors.Info
        Me.txt_OutstandingBal.Location = New System.Drawing.Point(421, 154)
        Me.txt_OutstandingBal.Name = "txt_OutstandingBal"
        Me.txt_OutstandingBal.ReadOnly = True
        Me.txt_OutstandingBal.Size = New System.Drawing.Size(162, 21)
        Me.txt_OutstandingBal.TabIndex = 12
        Me.txt_OutstandingBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(5, 160)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(111, 14)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Beginning Balance:"
        '
        'txt_BegBalance
        '
        Me.txt_BegBalance.BackColor = System.Drawing.SystemColors.Info
        Me.txt_BegBalance.Location = New System.Drawing.Point(122, 157)
        Me.txt_BegBalance.Name = "txt_BegBalance"
        Me.txt_BegBalance.ReadOnly = True
        Me.txt_BegBalance.Size = New System.Drawing.Size(162, 21)
        Me.txt_BegBalance.TabIndex = 10
        Me.txt_BegBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(7, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(109, 14)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Reference Number:"
        '
        'txt_RefNo
        '
        Me.txt_RefNo.BackColor = System.Drawing.SystemColors.Info
        Me.txt_RefNo.Location = New System.Drawing.Point(122, 22)
        Me.txt_RefNo.Name = "txt_RefNo"
        Me.txt_RefNo.ReadOnly = True
        Me.txt_RefNo.Size = New System.Drawing.Size(162, 21)
        Me.txt_RefNo.TabIndex = 8
        Me.txt_RefNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(333, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 14)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Participant ID:"
        '
        'txt_ParticipantID
        '
        Me.txt_ParticipantID.BackColor = System.Drawing.SystemColors.Info
        Me.txt_ParticipantID.Location = New System.Drawing.Point(421, 49)
        Me.txt_ParticipantID.Name = "txt_ParticipantID"
        Me.txt_ParticipantID.ReadOnly = True
        Me.txt_ParticipantID.Size = New System.Drawing.Size(162, 21)
        Me.txt_ParticipantID.TabIndex = 6
        Me.txt_ParticipantID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(49, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 14)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "ID Number:"
        '
        'txt_IDNumber
        '
        Me.txt_IDNumber.BackColor = System.Drawing.SystemColors.Info
        Me.txt_IDNumber.Location = New System.Drawing.Point(122, 48)
        Me.txt_IDNumber.Name = "txt_IDNumber"
        Me.txt_IDNumber.ReadOnly = True
        Me.txt_IDNumber.Size = New System.Drawing.Size(162, 21)
        Me.txt_IDNumber.TabIndex = 0
        Me.txt_IDNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmd_close
        '
        Me.cmd_close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_close.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_close.Location = New System.Drawing.Point(771, 597)
        Me.cmd_close.Name = "cmd_close"
        Me.cmd_close.Size = New System.Drawing.Size(145, 39)
        Me.cmd_close.TabIndex = 2
        Me.cmd_close.Text = "Close"
        Me.cmd_close.UseVisualStyleBackColor = True
        '
        'frmWESMSummaryDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(928, 646)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmd_close)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.tc_Transactions)
        Me.MaximizeBox = False
        Me.Name = "frmWESMSummaryDetails"
        Me.Text = "WESM Bill Summary - Details"
        Me.tc_Transactions.ResumeLayout(False)
        Me.tpOffsetting.ResumeLayout(False)
        CType(Me.dgv_OffsettingTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpCollection.ResumeLayout(False)
        CType(Me.dgv_CollectionTransactions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpPayment.ResumeLayout(False)
        CType(Me.dgv_PaymentTransactions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tc_Transactions As System.Windows.Forms.TabControl
    Friend WithEvents tpOffsetting As System.Windows.Forms.TabPage
    Friend WithEvents tpCollection As System.Windows.Forms.TabPage
    Friend WithEvents tpPayment As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt_RefNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_ParticipantID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_IDNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txt_EWtax As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txt_TransDate As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txt_isAdjustment As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txt_ChargeType As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txt_BillPeriod As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_NewDueDate As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txt_DueDate As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt_OutstandingBal As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txt_BegBalance As System.Windows.Forms.TextBox
    Friend WithEvents cmd_close As System.Windows.Forms.Button
    Friend WithEvents dgv_CollectionTransactions As System.Windows.Forms.DataGridView
    Friend WithEvents dgv_PaymentTransactions As System.Windows.Forms.DataGridView
    Friend WithEvents dgv_OffsettingTransaction As System.Windows.Forms.DataGridView
    Friend WithEvents cmd_GenOffsetRPT As System.Windows.Forms.Button
End Class
