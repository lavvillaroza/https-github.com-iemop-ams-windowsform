<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymentAllocationMgt
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.dgv_ParticipantAllocation = New System.Windows.Forms.DataGridView
        Me.hGroupNo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hPayBatchCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hBillPeriod = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hAllocEnergy = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hDefaultPayment = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hAllocatedVAT = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hDefPaymentEnergy = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hDefPaymentVAT = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hInterestNSS = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hInterestSTL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hInterestPrudential = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hAllocatedEnergy = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hOffsetAmountEnergy = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hOffsetAllocatedVAT = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hOffsetAmountVAT = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hTotalAllocatedEnergy = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hAllocatedAMTVAT = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hTotalEnergyPayment = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hTotalVATPayment = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.txt_tDefaultInterest = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txt_tVATAlloc = New System.Windows.Forms.TextBox
        Me.txt_tEnergyAlloc = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmd_DownloadCSV = New System.Windows.Forms.Button
        Me.cmd_ViewPayment = New System.Windows.Forms.Button
        Me.cmd_close = New System.Windows.Forms.Button
        Me.cmd_Settings = New System.Windows.Forms.Button
        Me.cmd_refresh = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgv_ParticipantAllocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.dgv_ParticipantAllocation)
        Me.GroupBox1.Font = New System.Drawing.Font("Helvetica Condensed", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(12, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1032, 529)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Participant Payment Allocation"
        '
        'dgv_ParticipantAllocation
        '
        Me.dgv_ParticipantAllocation.AllowUserToAddRows = False
        Me.dgv_ParticipantAllocation.AllowUserToDeleteRows = False
        Me.dgv_ParticipantAllocation.AllowUserToOrderColumns = True
        Me.dgv_ParticipantAllocation.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        Me.dgv_ParticipantAllocation.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_ParticipantAllocation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Helvetica Condensed", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_ParticipantAllocation.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_ParticipantAllocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_ParticipantAllocation.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.hGroupNo, Me.hPayBatchCode, Me.hBillPeriod, Me.hIDNumber, Me.hParticipantID, Me.hAllocEnergy, Me.hDefaultPayment, Me.hAllocatedVAT, Me.hDefPaymentEnergy, Me.hDefPaymentVAT, Me.hInterestNSS, Me.hInterestSTL, Me.hInterestPrudential, Me.hAllocatedEnergy, Me.hOffsetAmountEnergy, Me.hOffsetAllocatedVAT, Me.hOffsetAmountVAT, Me.hTotalAllocatedEnergy, Me.hAllocatedAMTVAT, Me.hTotalEnergyPayment, Me.hTotalVATPayment})
        Me.dgv_ParticipantAllocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_ParticipantAllocation.Location = New System.Drawing.Point(3, 16)
        Me.dgv_ParticipantAllocation.MultiSelect = False
        Me.dgv_ParticipantAllocation.Name = "dgv_ParticipantAllocation"
        Me.dgv_ParticipantAllocation.ReadOnly = True
        Me.dgv_ParticipantAllocation.RowTemplate.Height = 24
        Me.dgv_ParticipantAllocation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_ParticipantAllocation.Size = New System.Drawing.Size(1026, 510)
        Me.dgv_ParticipantAllocation.TabIndex = 2
        '
        'hGroupNo
        '
        Me.hGroupNo.Frozen = True
        Me.hGroupNo.HeaderText = "GroupNo"
        Me.hGroupNo.Name = "hGroupNo"
        Me.hGroupNo.ReadOnly = True
        Me.hGroupNo.Visible = False
        Me.hGroupNo.Width = 74
        '
        'hPayBatchCode
        '
        Me.hPayBatchCode.Frozen = True
        Me.hPayBatchCode.HeaderText = "PaymentBatchCode"
        Me.hPayBatchCode.Name = "hPayBatchCode"
        Me.hPayBatchCode.ReadOnly = True
        Me.hPayBatchCode.Visible = False
        Me.hPayBatchCode.Width = 123
        '
        'hBillPeriod
        '
        Me.hBillPeriod.Frozen = True
        Me.hBillPeriod.HeaderText = "BillPeriod"
        Me.hBillPeriod.Name = "hBillPeriod"
        Me.hBillPeriod.ReadOnly = True
        Me.hBillPeriod.Width = 73
        '
        'hIDNumber
        '
        Me.hIDNumber.Frozen = True
        Me.hIDNumber.HeaderText = "IdNumber"
        Me.hIDNumber.Name = "hIDNumber"
        Me.hIDNumber.ReadOnly = True
        Me.hIDNumber.Width = 78
        '
        'hParticipantID
        '
        Me.hParticipantID.Frozen = True
        Me.hParticipantID.HeaderText = "ParticipantID"
        Me.hParticipantID.Name = "hParticipantID"
        Me.hParticipantID.ReadOnly = True
        Me.hParticipantID.Width = 89
        '
        'hAllocEnergy
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "n2"
        Me.hAllocEnergy.DefaultCellStyle = DataGridViewCellStyle3
        Me.hAllocEnergy.HeaderText = "AllocatedEnergy"
        Me.hAllocEnergy.Name = "hAllocEnergy"
        Me.hAllocEnergy.ReadOnly = True
        Me.hAllocEnergy.Width = 104
        '
        'hDefaultPayment
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "n2"
        Me.hDefaultPayment.DefaultCellStyle = DataGridViewCellStyle4
        Me.hDefaultPayment.HeaderText = "DefaultInterest"
        Me.hDefaultPayment.Name = "hDefaultPayment"
        Me.hDefaultPayment.ReadOnly = True
        Me.hDefaultPayment.Width = 97
        '
        'hAllocatedVAT
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "n2"
        Me.hAllocatedVAT.DefaultCellStyle = DataGridViewCellStyle5
        Me.hAllocatedVAT.HeaderText = "AllocatedVAT"
        Me.hAllocatedVAT.Name = "hAllocatedVAT"
        Me.hAllocatedVAT.ReadOnly = True
        Me.hAllocatedVAT.Width = 90
        '
        'hDefPaymentEnergy
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "n2"
        Me.hDefPaymentEnergy.DefaultCellStyle = DataGridViewCellStyle6
        Me.hDefPaymentEnergy.HeaderText = "DeferredEnergy"
        Me.hDefPaymentEnergy.Name = "hDefPaymentEnergy"
        Me.hDefPaymentEnergy.ReadOnly = True
        Me.hDefPaymentEnergy.Width = 103
        '
        'hDefPaymentVAT
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "n2"
        Me.hDefPaymentVAT.DefaultCellStyle = DataGridViewCellStyle7
        Me.hDefPaymentVAT.HeaderText = "DeferredVAT"
        Me.hDefPaymentVAT.Name = "hDefPaymentVAT"
        Me.hDefPaymentVAT.ReadOnly = True
        Me.hDefPaymentVAT.Width = 89
        '
        'hInterestNSS
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "n2"
        Me.hInterestNSS.DefaultCellStyle = DataGridViewCellStyle8
        Me.hInterestNSS.HeaderText = "InterestNSS"
        Me.hInterestNSS.Name = "hInterestNSS"
        Me.hInterestNSS.ReadOnly = True
        Me.hInterestNSS.Width = 85
        '
        'hInterestSTL
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "n2"
        Me.hInterestSTL.DefaultCellStyle = DataGridViewCellStyle9
        Me.hInterestSTL.HeaderText = "InterestSTL"
        Me.hInterestSTL.Name = "hInterestSTL"
        Me.hInterestSTL.ReadOnly = True
        Me.hInterestSTL.Width = 84
        '
        'hInterestPrudential
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "n2"
        Me.hInterestPrudential.DefaultCellStyle = DataGridViewCellStyle10
        Me.hInterestPrudential.HeaderText = "InterestPrudential"
        Me.hInterestPrudential.Name = "hInterestPrudential"
        Me.hInterestPrudential.ReadOnly = True
        Me.hInterestPrudential.Width = 111
        '
        'hAllocatedEnergy
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Format = "n2"
        DataGridViewCellStyle11.NullValue = "0.00"
        Me.hAllocatedEnergy.DefaultCellStyle = DataGridViewCellStyle11
        Me.hAllocatedEnergy.HeaderText = "Offset Allocated Energy"
        Me.hAllocatedEnergy.Name = "hAllocatedEnergy"
        Me.hAllocatedEnergy.ReadOnly = True
        Me.hAllocatedEnergy.Width = 124
        '
        'hOffsetAmountEnergy
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle12.Format = "n2"
        DataGridViewCellStyle12.NullValue = "0.00"
        Me.hOffsetAmountEnergy.DefaultCellStyle = DataGridViewCellStyle12
        Me.hOffsetAmountEnergy.HeaderText = "Offset Amount Energy"
        Me.hOffsetAmountEnergy.Name = "hOffsetAmountEnergy"
        Me.hOffsetAmountEnergy.ReadOnly = True
        Me.hOffsetAmountEnergy.Width = 121
        '
        'hOffsetAllocatedVAT
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle13.Format = "n2"
        DataGridViewCellStyle13.NullValue = "0.00"
        Me.hOffsetAllocatedVAT.DefaultCellStyle = DataGridViewCellStyle13
        Me.hOffsetAllocatedVAT.HeaderText = "Offset Allocated VAT"
        Me.hOffsetAllocatedVAT.Name = "hOffsetAllocatedVAT"
        Me.hOffsetAllocatedVAT.ReadOnly = True
        Me.hOffsetAllocatedVAT.Width = 96
        '
        'hOffsetAmountVAT
        '
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle14.Format = "n2"
        DataGridViewCellStyle14.NullValue = "0.00"
        Me.hOffsetAmountVAT.DefaultCellStyle = DataGridViewCellStyle14
        Me.hOffsetAmountVAT.HeaderText = "Offset Amount VAT"
        Me.hOffsetAmountVAT.Name = "hOffsetAmountVAT"
        Me.hOffsetAmountVAT.ReadOnly = True
        Me.hOffsetAmountVAT.Width = 92
        '
        'hTotalAllocatedEnergy
        '
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle15.Format = "n2"
        Me.hTotalAllocatedEnergy.DefaultCellStyle = DataGridViewCellStyle15
        Me.hTotalAllocatedEnergy.HeaderText = "AllocatedAmountEnergy"
        Me.hTotalAllocatedEnergy.Name = "hTotalAllocatedEnergy"
        Me.hTotalAllocatedEnergy.ReadOnly = True
        Me.hTotalAllocatedEnergy.Width = 140
        '
        'hAllocatedAMTVAT
        '
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle16.Format = "n2"
        DataGridViewCellStyle16.NullValue = "0.00"
        Me.hAllocatedAMTVAT.DefaultCellStyle = DataGridViewCellStyle16
        Me.hAllocatedAMTVAT.HeaderText = "AllocatedAmountVAT"
        Me.hAllocatedAMTVAT.Name = "hAllocatedAMTVAT"
        Me.hAllocatedAMTVAT.ReadOnly = True
        Me.hAllocatedAMTVAT.Width = 126
        '
        'hTotalEnergyPayment
        '
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle17.Format = "n2"
        Me.hTotalEnergyPayment.DefaultCellStyle = DataGridViewCellStyle17
        Me.hTotalEnergyPayment.HeaderText = "Total Energy Payment"
        Me.hTotalEnergyPayment.Name = "hTotalEnergyPayment"
        Me.hTotalEnergyPayment.ReadOnly = True
        Me.hTotalEnergyPayment.Width = 120
        '
        'hTotalVATPayment
        '
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle18.Format = "n2"
        DataGridViewCellStyle18.NullValue = "0.00"
        Me.hTotalVATPayment.DefaultCellStyle = DataGridViewCellStyle18
        Me.hTotalVATPayment.HeaderText = "Total VAT Payment"
        Me.hTotalVATPayment.Name = "hTotalVATPayment"
        Me.hTotalVATPayment.ReadOnly = True
        Me.hTotalVATPayment.Width = 107
        '
        'txt_tDefaultInterest
        '
        Me.txt_tDefaultInterest.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt_tDefaultInterest.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_tDefaultInterest.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_tDefaultInterest.ForeColor = System.Drawing.Color.Black
        Me.txt_tDefaultInterest.Location = New System.Drawing.Point(497, 13)
        Me.txt_tDefaultInterest.Name = "txt_tDefaultInterest"
        Me.txt_tDefaultInterest.ReadOnly = True
        Me.txt_tDefaultInterest.Size = New System.Drawing.Size(194, 23)
        Me.txt_tDefaultInterest.TabIndex = 42
        Me.txt_tDefaultInterest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(358, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(133, 15)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "Total Default Interest:"
        '
        'txt_tVATAlloc
        '
        Me.txt_tVATAlloc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt_tVATAlloc.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_tVATAlloc.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_tVATAlloc.ForeColor = System.Drawing.Color.Black
        Me.txt_tVATAlloc.Location = New System.Drawing.Point(834, 13)
        Me.txt_tVATAlloc.Name = "txt_tVATAlloc"
        Me.txt_tVATAlloc.ReadOnly = True
        Me.txt_tVATAlloc.Size = New System.Drawing.Size(194, 23)
        Me.txt_tVATAlloc.TabIndex = 36
        Me.txt_tVATAlloc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_tEnergyAlloc
        '
        Me.txt_tEnergyAlloc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt_tEnergyAlloc.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_tEnergyAlloc.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_tEnergyAlloc.ForeColor = System.Drawing.Color.Black
        Me.txt_tEnergyAlloc.Location = New System.Drawing.Point(158, 13)
        Me.txt_tEnergyAlloc.Name = "txt_tEnergyAlloc"
        Me.txt_tEnergyAlloc.ReadOnly = True
        Me.txt_tEnergyAlloc.Size = New System.Drawing.Size(194, 23)
        Me.txt_tEnergyAlloc.TabIndex = 35
        Me.txt_tEnergyAlloc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(697, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(131, 15)
        Me.Label7.TabIndex = 34
        Me.Label7.Text = "Total VAT Allocation:"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(146, 15)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "Total Energy Allocation:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txt_tDefaultInterest)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txt_tVATAlloc)
        Me.GroupBox2.Controls.Add(Me.txt_tEnergyAlloc)
        Me.GroupBox2.Location = New System.Drawing.Point(20, 542)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1041, 49)
        Me.GroupBox2.TabIndex = 43
        Me.GroupBox2.TabStop = False
        '
        'cmd_DownloadCSV
        '
        Me.cmd_DownloadCSV.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_DownloadCSV.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_DownloadCSV.Image = Global.AccountsManagementForms.My.Resources.Resources.report
        Me.cmd_DownloadCSV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_DownloadCSV.Location = New System.Drawing.Point(654, 598)
        Me.cmd_DownloadCSV.Name = "cmd_DownloadCSV"
        Me.cmd_DownloadCSV.Size = New System.Drawing.Size(192, 30)
        Me.cmd_DownloadCSV.TabIndex = 44
        Me.cmd_DownloadCSV.Text = "Download To CSV"
        Me.cmd_DownloadCSV.UseVisualStyleBackColor = True
        '
        'cmd_ViewPayment
        '
        Me.cmd_ViewPayment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_ViewPayment.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ViewPayment.Image = Global.AccountsManagementForms.My.Resources.Resources.report
        Me.cmd_ViewPayment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_ViewPayment.Location = New System.Drawing.Point(852, 597)
        Me.cmd_ViewPayment.Name = "cmd_ViewPayment"
        Me.cmd_ViewPayment.Size = New System.Drawing.Size(192, 30)
        Me.cmd_ViewPayment.TabIndex = 29
        Me.cmd_ViewPayment.Text = "     View Application to Receivable"
        Me.cmd_ViewPayment.UseVisualStyleBackColor = True
        '
        'cmd_close
        '
        Me.cmd_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_close.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_close.Image = Global.AccountsManagementForms.My.Resources.Resources.close
        Me.cmd_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_close.Location = New System.Drawing.Point(1215, 597)
        Me.cmd_close.Name = "cmd_close"
        Me.cmd_close.Size = New System.Drawing.Size(159, 30)
        Me.cmd_close.TabIndex = 0
        Me.cmd_close.Text = "Close"
        Me.cmd_close.UseVisualStyleBackColor = True
        '
        'cmd_Settings
        '
        Me.cmd_Settings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_Settings.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Settings.Image = Global.AccountsManagementForms.My.Resources.Resources.edit_1
        Me.cmd_Settings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Settings.Location = New System.Drawing.Point(330, 598)
        Me.cmd_Settings.Name = "cmd_Settings"
        Me.cmd_Settings.Size = New System.Drawing.Size(122, 30)
        Me.cmd_Settings.TabIndex = 44
        Me.cmd_Settings.Text = "Settings"
        Me.cmd_Settings.UseVisualStyleBackColor = True
        '
        'cmd_refresh
        '
        Me.cmd_refresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_refresh.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_refresh.Image = Global.AccountsManagementForms.My.Resources.Resources.refresh
        Me.cmd_refresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_refresh.Location = New System.Drawing.Point(458, 597)
        Me.cmd_refresh.Name = "cmd_refresh"
        Me.cmd_refresh.Size = New System.Drawing.Size(122, 30)
        Me.cmd_refresh.TabIndex = 45
        Me.cmd_refresh.Text = "Refresh"
        Me.cmd_refresh.UseVisualStyleBackColor = True
        '
        'frmPaymentAllocationMgt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1054, 639)
        Me.Controls.Add(Me.cmd_refresh)
        Me.Controls.Add(Me.cmd_Settings)
        Me.Controls.Add(Me.cmd_DownloadCSV)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.cmd_ViewPayment)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmd_close)
        Me.MinimumSize = New System.Drawing.Size(1052, 629)
        Me.Name = "frmPaymentAllocationMgt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Payment Allocation Details"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgv_ParticipantAllocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmd_close As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dgv_ParticipantAllocation As System.Windows.Forms.DataGridView
    Friend WithEvents cmd_ViewPayment As System.Windows.Forms.Button
    Friend WithEvents txt_tDefaultInterest As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt_tVATAlloc As System.Windows.Forms.TextBox
    Friend WithEvents txt_tEnergyAlloc As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmd_DownloadCSV As System.Windows.Forms.Button
    Friend WithEvents cmd_Settings As System.Windows.Forms.Button
    Friend WithEvents cmd_refresh As System.Windows.Forms.Button
    Friend WithEvents hGroupNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hPayBatchCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hBillPeriod As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hAllocEnergy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hDefaultPayment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hAllocatedVAT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hDefPaymentEnergy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hDefPaymentVAT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hInterestNSS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hInterestSTL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hInterestPrudential As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hAllocatedEnergy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hOffsetAmountEnergy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hOffsetAllocatedVAT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hOffsetAmountVAT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hTotalAllocatedEnergy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hAllocatedAMTVAT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hTotalEnergyPayment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hTotalVATPayment As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
