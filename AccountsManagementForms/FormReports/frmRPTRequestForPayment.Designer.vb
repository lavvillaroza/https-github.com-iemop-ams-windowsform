<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRPTRequestForPayment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRPTRequestForPayment))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_RFPTo = New System.Windows.Forms.TextBox()
        Me.txt_RFPFrom = New System.Windows.Forms.TextBox()
        Me.txt_RFPPurpose = New System.Windows.Forms.TextBox()
        Me.cmd_editTo = New System.Windows.Forms.Button()
        Me.cmd_editFrom = New System.Windows.Forms.Button()
        Me.cmd_editPurpose = New System.Windows.Forms.Button()
        Me.gbox_PaymentDetails = New System.Windows.Forms.GroupBox()
        Me.dgv_PaymentDetails = New System.Windows.Forms.DataGridView()
        Me.pPayee = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pBankBranch = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pAccountNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pPaymentType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pParticulars = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gbox_CollectionDetails = New System.Windows.Forms.GroupBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.dgv_CollectionDetails = New System.Windows.Forms.DataGridView()
        Me.cParticipant = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cDateOfDeposit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cParticulars = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gbox_PaymentTotals = New System.Windows.Forms.GroupBox()
        Me.txt_Collection = New System.Windows.Forms.TextBox()
        Me.txt_HeldCollection = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txt_TransferPEMC = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txt_MFApplication = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txt_PRReplenish = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txt_NSSApplied = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txt_totDeferred = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt_TotLBC = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txt_TotRTGS = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_RefNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_TotalPayment = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_Remarks = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmd_EditNote = New System.Windows.Forms.Button()
        Me.cbo_AllocationDate = New System.Windows.Forms.ComboBox()
        Me.dtp_PaymentDate = New System.Windows.Forms.DateTimePicker()
        Me.cmd_GenerateReport = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.gbox_PaymentDetails.SuspendLayout()
        CType(Me.dgv_PaymentDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbox_CollectionDetails.SuspendLayout()
        CType(Me.dgv_CollectionDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbox_PaymentTotals.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "To:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 15)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "From:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(640, 43)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(127, 15)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Purpose of Payment:"
        '
        'txt_RFPTo
        '
        Me.txt_RFPTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_RFPTo.Enabled = False
        Me.txt_RFPTo.Location = New System.Drawing.Point(134, 43)
        Me.txt_RFPTo.Name = "txt_RFPTo"
        Me.txt_RFPTo.Size = New System.Drawing.Size(183, 20)
        Me.txt_RFPTo.TabIndex = 4
        '
        'txt_RFPFrom
        '
        Me.txt_RFPFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_RFPFrom.Enabled = False
        Me.txt_RFPFrom.Location = New System.Drawing.Point(134, 69)
        Me.txt_RFPFrom.Name = "txt_RFPFrom"
        Me.txt_RFPFrom.Size = New System.Drawing.Size(183, 20)
        Me.txt_RFPFrom.TabIndex = 5
        '
        'txt_RFPPurpose
        '
        Me.txt_RFPPurpose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_RFPPurpose.Enabled = False
        Me.txt_RFPPurpose.Location = New System.Drawing.Point(768, 42)
        Me.txt_RFPPurpose.Multiline = True
        Me.txt_RFPPurpose.Name = "txt_RFPPurpose"
        Me.txt_RFPPurpose.Size = New System.Drawing.Size(183, 47)
        Me.txt_RFPPurpose.TabIndex = 6
        '
        'cmd_editTo
        '
        Me.cmd_editTo.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_editTo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_editTo.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_editTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_editTo.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_editTo.Image = CType(resources.GetObject("cmd_editTo.Image"), System.Drawing.Image)
        Me.cmd_editTo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_editTo.Location = New System.Drawing.Point(323, 35)
        Me.cmd_editTo.Name = "cmd_editTo"
        Me.cmd_editTo.Size = New System.Drawing.Size(80, 30)
        Me.cmd_editTo.TabIndex = 9
        Me.cmd_editTo.Text = "Edit"
        Me.cmd_editTo.UseVisualStyleBackColor = True
        '
        'cmd_editFrom
        '
        Me.cmd_editFrom.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_editFrom.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_editFrom.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_editFrom.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_editFrom.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_editFrom.Image = CType(resources.GetObject("cmd_editFrom.Image"), System.Drawing.Image)
        Me.cmd_editFrom.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_editFrom.Location = New System.Drawing.Point(323, 68)
        Me.cmd_editFrom.Name = "cmd_editFrom"
        Me.cmd_editFrom.Size = New System.Drawing.Size(80, 30)
        Me.cmd_editFrom.TabIndex = 10
        Me.cmd_editFrom.Text = "Edit"
        Me.cmd_editFrom.UseVisualStyleBackColor = True
        '
        'cmd_editPurpose
        '
        Me.cmd_editPurpose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_editPurpose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_editPurpose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_editPurpose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_editPurpose.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_editPurpose.Image = CType(resources.GetObject("cmd_editPurpose.Image"), System.Drawing.Image)
        Me.cmd_editPurpose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_editPurpose.Location = New System.Drawing.Point(871, 95)
        Me.cmd_editPurpose.Name = "cmd_editPurpose"
        Me.cmd_editPurpose.Size = New System.Drawing.Size(80, 30)
        Me.cmd_editPurpose.TabIndex = 11
        Me.cmd_editPurpose.Text = "Edit"
        Me.cmd_editPurpose.UseVisualStyleBackColor = True
        '
        'gbox_PaymentDetails
        '
        Me.gbox_PaymentDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbox_PaymentDetails.Controls.Add(Me.dgv_PaymentDetails)
        Me.gbox_PaymentDetails.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbox_PaymentDetails.ForeColor = System.Drawing.Color.Black
        Me.gbox_PaymentDetails.Location = New System.Drawing.Point(12, 164)
        Me.gbox_PaymentDetails.Name = "gbox_PaymentDetails"
        Me.gbox_PaymentDetails.Size = New System.Drawing.Size(946, 186)
        Me.gbox_PaymentDetails.TabIndex = 12
        Me.gbox_PaymentDetails.TabStop = False
        '
        'dgv_PaymentDetails
        '
        Me.dgv_PaymentDetails.AllowUserToAddRows = False
        Me.dgv_PaymentDetails.AllowUserToDeleteRows = False
        Me.dgv_PaymentDetails.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_PaymentDetails.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_PaymentDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_PaymentDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgv_PaymentDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_PaymentDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.pPayee, Me.pIDNumber, Me.pBankBranch, Me.pAccountNo, Me.pPaymentType, Me.pAmount, Me.pParticulars})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LemonChiffon
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_PaymentDetails.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_PaymentDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv_PaymentDetails.Location = New System.Drawing.Point(3, 16)
        Me.dgv_PaymentDetails.Name = "dgv_PaymentDetails"
        Me.dgv_PaymentDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_PaymentDetails.Size = New System.Drawing.Size(940, 163)
        Me.dgv_PaymentDetails.TabIndex = 0
        '
        'pPayee
        '
        Me.pPayee.HeaderText = "Payee"
        Me.pPayee.Name = "pPayee"
        Me.pPayee.Width = 61
        '
        'pIDNumber
        '
        Me.pIDNumber.HeaderText = "IDNumber"
        Me.pIDNumber.Name = "pIDNumber"
        Me.pIDNumber.Width = 80
        '
        'pBankBranch
        '
        Me.pBankBranch.HeaderText = "Bank/Branch"
        Me.pBankBranch.Name = "pBankBranch"
        Me.pBankBranch.Width = 93
        '
        'pAccountNo
        '
        Me.pAccountNo.HeaderText = "AccountNo"
        Me.pAccountNo.Name = "pAccountNo"
        Me.pAccountNo.Width = 84
        '
        'pPaymentType
        '
        Me.pPaymentType.HeaderText = "PaymentType"
        Me.pPaymentType.Name = "pPaymentType"
        Me.pPaymentType.Width = 98
        '
        'pAmount
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "n2"
        DataGridViewCellStyle2.NullValue = "0.00"
        Me.pAmount.DefaultCellStyle = DataGridViewCellStyle2
        Me.pAmount.HeaderText = "Amount"
        Me.pAmount.Name = "pAmount"
        Me.pAmount.Width = 67
        '
        'pParticulars
        '
        Me.pParticulars.HeaderText = "Particulars"
        Me.pParticulars.Name = "pParticulars"
        Me.pParticulars.Width = 82
        '
        'gbox_CollectionDetails
        '
        Me.gbox_CollectionDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbox_CollectionDetails.Controls.Add(Me.Label19)
        Me.gbox_CollectionDetails.Controls.Add(Me.dgv_CollectionDetails)
        Me.gbox_CollectionDetails.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbox_CollectionDetails.ForeColor = System.Drawing.Color.Black
        Me.gbox_CollectionDetails.Location = New System.Drawing.Point(9, 447)
        Me.gbox_CollectionDetails.Name = "gbox_CollectionDetails"
        Me.gbox_CollectionDetails.Size = New System.Drawing.Size(949, 186)
        Me.gbox_CollectionDetails.TabIndex = 13
        Me.gbox_CollectionDetails.TabStop = False
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label19.Location = New System.Drawing.Point(4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(303, 14)
        Me.Label19.TabIndex = 34
        Me.Label19.Text = "II. Sources of Funds (Collections/NSS/Security Deposit):"
        '
        'dgv_CollectionDetails
        '
        Me.dgv_CollectionDetails.AllowUserToAddRows = False
        Me.dgv_CollectionDetails.AllowUserToDeleteRows = False
        Me.dgv_CollectionDetails.AllowUserToResizeRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_CollectionDetails.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv_CollectionDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_CollectionDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgv_CollectionDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_CollectionDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cParticipant, Me.cIDNumber, Me.cDateOfDeposit, Me.cAmount, Me.cParticulars})
        Me.dgv_CollectionDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv_CollectionDetails.Location = New System.Drawing.Point(3, 16)
        Me.dgv_CollectionDetails.Name = "dgv_CollectionDetails"
        Me.dgv_CollectionDetails.ReadOnly = True
        Me.dgv_CollectionDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_CollectionDetails.Size = New System.Drawing.Size(943, 167)
        Me.dgv_CollectionDetails.TabIndex = 1
        '
        'cParticipant
        '
        Me.cParticipant.HeaderText = "Payor"
        Me.cParticipant.Name = "cParticipant"
        Me.cParticipant.ReadOnly = True
        Me.cParticipant.Width = 59
        '
        'cIDNumber
        '
        Me.cIDNumber.HeaderText = "IDNumber"
        Me.cIDNumber.Name = "cIDNumber"
        Me.cIDNumber.ReadOnly = True
        Me.cIDNumber.Width = 80
        '
        'cDateOfDeposit
        '
        Me.cDateOfDeposit.HeaderText = "DateOfDeposit"
        Me.cDateOfDeposit.Name = "cDateOfDeposit"
        Me.cDateOfDeposit.ReadOnly = True
        Me.cDateOfDeposit.Width = 102
        '
        'cAmount
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "n2"
        DataGridViewCellStyle5.NullValue = "0.00"
        Me.cAmount.DefaultCellStyle = DataGridViewCellStyle5
        Me.cAmount.HeaderText = "Amount"
        Me.cAmount.Name = "cAmount"
        Me.cAmount.ReadOnly = True
        Me.cAmount.Width = 67
        '
        'cParticulars
        '
        Me.cParticulars.HeaderText = "Particulars"
        Me.cParticulars.Name = "cParticulars"
        Me.cParticulars.ReadOnly = True
        Me.cParticulars.Width = 82
        '
        'gbox_PaymentTotals
        '
        Me.gbox_PaymentTotals.Controls.Add(Me.txt_Collection)
        Me.gbox_PaymentTotals.Controls.Add(Me.txt_HeldCollection)
        Me.gbox_PaymentTotals.Controls.Add(Me.Label22)
        Me.gbox_PaymentTotals.Controls.Add(Me.Label13)
        Me.gbox_PaymentTotals.Controls.Add(Me.txt_TransferPEMC)
        Me.gbox_PaymentTotals.Controls.Add(Me.Label12)
        Me.gbox_PaymentTotals.Controls.Add(Me.txt_MFApplication)
        Me.gbox_PaymentTotals.Controls.Add(Me.Label16)
        Me.gbox_PaymentTotals.Controls.Add(Me.txt_PRReplenish)
        Me.gbox_PaymentTotals.Controls.Add(Me.Label15)
        Me.gbox_PaymentTotals.Controls.Add(Me.txt_NSSApplied)
        Me.gbox_PaymentTotals.Controls.Add(Me.Label14)
        Me.gbox_PaymentTotals.Controls.Add(Me.txt_totDeferred)
        Me.gbox_PaymentTotals.Controls.Add(Me.Label10)
        Me.gbox_PaymentTotals.Controls.Add(Me.txt_TotLBC)
        Me.gbox_PaymentTotals.Controls.Add(Me.Label9)
        Me.gbox_PaymentTotals.Controls.Add(Me.txt_TotRTGS)
        Me.gbox_PaymentTotals.Controls.Add(Me.Label8)
        Me.gbox_PaymentTotals.Location = New System.Drawing.Point(12, 356)
        Me.gbox_PaymentTotals.Name = "gbox_PaymentTotals"
        Me.gbox_PaymentTotals.Size = New System.Drawing.Size(941, 85)
        Me.gbox_PaymentTotals.TabIndex = 22
        Me.gbox_PaymentTotals.TabStop = False
        '
        'txt_Collection
        '
        Me.txt_Collection.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_Collection.Enabled = False
        Me.txt_Collection.Location = New System.Drawing.Point(749, 54)
        Me.txt_Collection.Name = "txt_Collection"
        Me.txt_Collection.ReadOnly = True
        Me.txt_Collection.Size = New System.Drawing.Size(183, 20)
        Me.txt_Collection.TabIndex = 28
        Me.txt_Collection.Text = "0.00"
        Me.txt_Collection.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_HeldCollection
        '
        Me.txt_HeldCollection.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_HeldCollection.Enabled = False
        Me.txt_HeldCollection.Location = New System.Drawing.Point(749, 9)
        Me.txt_HeldCollection.Name = "txt_HeldCollection"
        Me.txt_HeldCollection.ReadOnly = True
        Me.txt_HeldCollection.Size = New System.Drawing.Size(183, 20)
        Me.txt_HeldCollection.TabIndex = 38
        Me.txt_HeldCollection.Text = "0.00"
        Me.txt_HeldCollection.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(611, 58)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(125, 15)
        Me.Label22.TabIndex = 27
        Me.Label22.Text = "Total Collection Applied:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(611, 11)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(110, 15)
        Me.Label13.TabIndex = 37
        Me.Label13.Text = "Total Held Collection:"
        '
        'txt_TransferPEMC
        '
        Me.txt_TransferPEMC.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_TransferPEMC.Enabled = False
        Me.txt_TransferPEMC.Location = New System.Drawing.Point(749, 31)
        Me.txt_TransferPEMC.Name = "txt_TransferPEMC"
        Me.txt_TransferPEMC.ReadOnly = True
        Me.txt_TransferPEMC.Size = New System.Drawing.Size(183, 20)
        Me.txt_TransferPEMC.TabIndex = 36
        Me.txt_TransferPEMC.Text = "0.00"
        Me.txt_TransferPEMC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(611, 34)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(81, 15)
        Me.Label12.TabIndex = 35
        Me.Label12.Text = "Total To PEMC:"
        '
        'txt_MFApplication
        '
        Me.txt_MFApplication.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_MFApplication.Enabled = False
        Me.txt_MFApplication.Location = New System.Drawing.Point(91, 54)
        Me.txt_MFApplication.Name = "txt_MFApplication"
        Me.txt_MFApplication.ReadOnly = True
        Me.txt_MFApplication.Size = New System.Drawing.Size(183, 20)
        Me.txt_MFApplication.TabIndex = 34
        Me.txt_MFApplication.Text = "0.00"
        Me.txt_MFApplication.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(6, 56)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(52, 15)
        Me.Label16.TabIndex = 33
        Me.Label16.Text = "Total MF:"
        '
        'txt_PRReplenish
        '
        Me.txt_PRReplenish.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_PRReplenish.Enabled = False
        Me.txt_PRReplenish.Location = New System.Drawing.Point(422, 54)
        Me.txt_PRReplenish.Name = "txt_PRReplenish"
        Me.txt_PRReplenish.ReadOnly = True
        Me.txt_PRReplenish.Size = New System.Drawing.Size(183, 20)
        Me.txt_PRReplenish.TabIndex = 32
        Me.txt_PRReplenish.Text = "0.00"
        Me.txt_PRReplenish.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(284, 56)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(132, 15)
        Me.Label15.TabIndex = 31
        Me.Label15.Text = "Prudential Replenishment"
        '
        'txt_NSSApplied
        '
        Me.txt_NSSApplied.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_NSSApplied.Enabled = False
        Me.txt_NSSApplied.Location = New System.Drawing.Point(422, 32)
        Me.txt_NSSApplied.Name = "txt_NSSApplied"
        Me.txt_NSSApplied.ReadOnly = True
        Me.txt_NSSApplied.Size = New System.Drawing.Size(183, 20)
        Me.txt_NSSApplied.TabIndex = 30
        Me.txt_NSSApplied.Text = "0.00"
        Me.txt_NSSApplied.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(284, 36)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(71, 15)
        Me.Label14.TabIndex = 29
        Me.Label14.Text = "NSS Applied:"
        '
        'txt_totDeferred
        '
        Me.txt_totDeferred.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_totDeferred.Enabled = False
        Me.txt_totDeferred.Location = New System.Drawing.Point(422, 9)
        Me.txt_totDeferred.Name = "txt_totDeferred"
        Me.txt_totDeferred.ReadOnly = True
        Me.txt_totDeferred.Size = New System.Drawing.Size(183, 20)
        Me.txt_totDeferred.TabIndex = 28
        Me.txt_totDeferred.Text = "0.00"
        Me.txt_totDeferred.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(284, 13)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(79, 15)
        Me.Label10.TabIndex = 27
        Me.Label10.Text = "Total Deferred:"
        '
        'txt_TotLBC
        '
        Me.txt_TotLBC.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_TotLBC.Enabled = False
        Me.txt_TotLBC.Location = New System.Drawing.Point(91, 32)
        Me.txt_TotLBC.Name = "txt_TotLBC"
        Me.txt_TotLBC.ReadOnly = True
        Me.txt_TotLBC.Size = New System.Drawing.Size(183, 20)
        Me.txt_TotLBC.TabIndex = 26
        Me.txt_TotLBC.Text = "0.00"
        Me.txt_TotLBC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(6, 34)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(57, 15)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "Total LBC:"
        '
        'txt_TotRTGS
        '
        Me.txt_TotRTGS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_TotRTGS.Enabled = False
        Me.txt_TotRTGS.Location = New System.Drawing.Point(91, 9)
        Me.txt_TotRTGS.Name = "txt_TotRTGS"
        Me.txt_TotRTGS.ReadOnly = True
        Me.txt_TotRTGS.Size = New System.Drawing.Size(183, 20)
        Me.txt_TotRTGS.TabIndex = 24
        Me.txt_TotRTGS.Text = "0.00"
        Me.txt_TotRTGS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 11)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 15)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "Total RTGS:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(742, 17)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(90, 15)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Reference No:"
        '
        'txt_RefNo
        '
        Me.txt_RefNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_RefNo.Enabled = False
        Me.txt_RefNo.Location = New System.Drawing.Point(835, 16)
        Me.txt_RefNo.Name = "txt_RefNo"
        Me.txt_RefNo.ReadOnly = True
        Me.txt_RefNo.Size = New System.Drawing.Size(116, 20)
        Me.txt_RefNo.TabIndex = 15
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 95)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(106, 15)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Date of Payment:"
        '
        'txt_TotalPayment
        '
        Me.txt_TotalPayment.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_TotalPayment.Enabled = False
        Me.txt_TotalPayment.Location = New System.Drawing.Point(134, 121)
        Me.txt_TotalPayment.Name = "txt_TotalPayment"
        Me.txt_TotalPayment.ReadOnly = True
        Me.txt_TotalPayment.Size = New System.Drawing.Size(183, 20)
        Me.txt_TotalPayment.TabIndex = 21
        Me.txt_TotalPayment.Text = "0.00"
        Me.txt_TotalPayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 121)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 15)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Total Payment:"
        '
        'txt_Remarks
        '
        Me.txt_Remarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_Remarks.Enabled = False
        Me.txt_Remarks.Location = New System.Drawing.Point(465, 42)
        Me.txt_Remarks.Multiline = True
        Me.txt_Remarks.Name = "txt_Remarks"
        Me.txt_Remarks.Size = New System.Drawing.Size(161, 47)
        Me.txt_Remarks.TabIndex = 24
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(417, 43)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(38, 15)
        Me.Label11.TabIndex = 23
        Me.Label11.Text = "Note:"
        '
        'cmd_EditNote
        '
        Me.cmd_EditNote.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_EditNote.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_EditNote.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_EditNote.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_EditNote.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_EditNote.Image = CType(resources.GetObject("cmd_EditNote.Image"), System.Drawing.Image)
        Me.cmd_EditNote.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_EditNote.Location = New System.Drawing.Point(551, 95)
        Me.cmd_EditNote.Name = "cmd_EditNote"
        Me.cmd_EditNote.Size = New System.Drawing.Size(80, 30)
        Me.cmd_EditNote.TabIndex = 25
        Me.cmd_EditNote.Text = "Edit"
        Me.cmd_EditNote.UseVisualStyleBackColor = True
        '
        'cbo_AllocationDate
        '
        Me.cbo_AllocationDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_AllocationDate.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cbo_AllocationDate.FormattingEnabled = True
        Me.cbo_AllocationDate.Location = New System.Drawing.Point(134, 16)
        Me.cbo_AllocationDate.Name = "cbo_AllocationDate"
        Me.cbo_AllocationDate.Size = New System.Drawing.Size(121, 21)
        Me.cbo_AllocationDate.TabIndex = 26
        '
        'dtp_PaymentDate
        '
        Me.dtp_PaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_PaymentDate.Location = New System.Drawing.Point(134, 95)
        Me.dtp_PaymentDate.Name = "dtp_PaymentDate"
        Me.dtp_PaymentDate.Size = New System.Drawing.Size(183, 20)
        Me.dtp_PaymentDate.TabIndex = 30
        '
        'cmd_GenerateReport
        '
        Me.cmd_GenerateReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_GenerateReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_GenerateReport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_GenerateReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_GenerateReport.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_GenerateReport.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.cmd_GenerateReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_GenerateReport.Location = New System.Drawing.Point(817, 639)
        Me.cmd_GenerateReport.Name = "cmd_GenerateReport"
        Me.cmd_GenerateReport.Size = New System.Drawing.Size(141, 39)
        Me.cmd_GenerateReport.TabIndex = 16
        Me.cmd_GenerateReport.Text = "      Generate Report"
        Me.cmd_GenerateReport.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(6, 22)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(92, 14)
        Me.Label17.TabIndex = 32
        Me.Label17.Text = "Allocation Date:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(18, 166)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(119, 14)
        Me.Label18.TabIndex = 33
        Me.Label18.Text = "I. Details of Payment:"
        '
        'frmRPTRequestForPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(964, 691)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.gbox_PaymentTotals)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.dtp_PaymentDate)
        Me.Controls.Add(Me.cbo_AllocationDate)
        Me.Controls.Add(Me.cmd_EditNote)
        Me.Controls.Add(Me.txt_Remarks)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txt_TotalPayment)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cmd_GenerateReport)
        Me.Controls.Add(Me.txt_RefNo)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.gbox_CollectionDetails)
        Me.Controls.Add(Me.gbox_PaymentDetails)
        Me.Controls.Add(Me.cmd_editPurpose)
        Me.Controls.Add(Me.cmd_editFrom)
        Me.Controls.Add(Me.cmd_editTo)
        Me.Controls.Add(Me.txt_RFPPurpose)
        Me.Controls.Add(Me.txt_RFPFrom)
        Me.Controls.Add(Me.txt_RFPTo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(975, 680)
        Me.Name = "frmRPTRequestForPayment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Request For Payment"
        Me.gbox_PaymentDetails.ResumeLayout(False)
        CType(Me.dgv_PaymentDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbox_CollectionDetails.ResumeLayout(False)
        Me.gbox_CollectionDetails.PerformLayout()
        CType(Me.dgv_CollectionDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbox_PaymentTotals.ResumeLayout(False)
        Me.gbox_PaymentTotals.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txt_RFPTo As System.Windows.Forms.TextBox
    Friend WithEvents txt_RFPFrom As System.Windows.Forms.TextBox
    Friend WithEvents txt_RFPPurpose As System.Windows.Forms.TextBox
    Friend WithEvents cmd_editTo As System.Windows.Forms.Button
    Friend WithEvents cmd_editFrom As System.Windows.Forms.Button
    Friend WithEvents cmd_editPurpose As System.Windows.Forms.Button
    Friend WithEvents gbox_PaymentDetails As System.Windows.Forms.GroupBox
    Friend WithEvents gbox_CollectionDetails As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_RefNo As System.Windows.Forms.TextBox
    Friend WithEvents cmd_GenerateReport As System.Windows.Forms.Button
    Friend WithEvents dgv_PaymentDetails As System.Windows.Forms.DataGridView
    Friend WithEvents dgv_CollectionDetails As System.Windows.Forms.DataGridView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txt_TotalPayment As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents gbox_PaymentTotals As System.Windows.Forms.GroupBox
    Friend WithEvents txt_TotLBC As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txt_TotRTGS As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txt_totDeferred As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txt_Remarks As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmd_EditNote As System.Windows.Forms.Button
    Friend WithEvents cbo_AllocationDate As System.Windows.Forms.ComboBox
    Friend WithEvents pPayee As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pBankBranch As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pAccountNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pPaymentType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pParticulars As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dtp_PaymentDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txt_PRReplenish As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txt_NSSApplied As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cParticipant As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cDateOfDeposit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cParticulars As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txt_MFApplication As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txt_HeldCollection As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txt_TransferPEMC As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txt_Collection As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
End Class
