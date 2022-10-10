<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymentAllocationViewDetails
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
        Me.dgridView = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txt_AROffsetAMTVAT = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.txt_AROffsetAMTEnergy = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.txt_totalAR = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.txt_AREnergy = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txt_ARVATEnergy = New System.Windows.Forms.TextBox
        Me.txt_ARTotalDIEnergy = New System.Windows.Forms.TextBox
        Me.txt_TotalAREnergy = New System.Windows.Forms.TextBox
        Me.txt_ARMF = New System.Windows.Forms.TextBox
        Me.txt_ARTotalDefaultMF = New System.Windows.Forms.TextBox
        Me.txt_ARWHTax = New System.Windows.Forms.TextBox
        Me.txt_ARtotalMF = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.cmd_viewDMCM = New System.Windows.Forms.Button
        Me.cmd_close = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.tp_ApplicationAR = New System.Windows.Forms.TabPage
        Me.tp_ApplicationAP = New System.Windows.Forms.TabPage
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txt_APOffsetAllocatedVATEnergy = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.txt_APOffsetAllocatedEnergy = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.txt_TotalAP = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txt_APEnergy = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txt_APTotalVATEnergy = New System.Windows.Forms.TextBox
        Me.txt_APTotalDefaultEnergy = New System.Windows.Forms.TextBox
        Me.txt_APTotalEnergy = New System.Windows.Forms.TextBox
        Me.txt_DeferredApplied = New System.Windows.Forms.TextBox
        Me.txt_APDefaultInterestMF = New System.Windows.Forms.TextBox
        Me.txt_APMF = New System.Windows.Forms.TextBox
        Me.txt_APTotalMF = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.TextBox4 = New System.Windows.Forms.TextBox
        Me.TextBox5 = New System.Windows.Forms.TextBox
        Me.TextBox6 = New System.Windows.Forms.TextBox
        Me.TextBox7 = New System.Windows.Forms.TextBox
        Me.TextBox8 = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.cmd_DownloadCSV = New System.Windows.Forms.Button
        Me.TextBox10 = New System.Windows.Forms.TextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.TextBox11 = New System.Windows.Forms.TextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.TextBox12 = New System.Windows.Forms.TextBox
        Me.TextBox13 = New System.Windows.Forms.TextBox
        Me.TextBox14 = New System.Windows.Forms.TextBox
        Me.TextBox15 = New System.Windows.Forms.TextBox
        Me.TextBox16 = New System.Windows.Forms.TextBox
        Me.TextBox17 = New System.Windows.Forms.TextBox
        Me.TextBox18 = New System.Windows.Forms.TextBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.Label35 = New System.Windows.Forms.Label
        Me.Label36 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.cmd_viewOR = New System.Windows.Forms.Button
        Me.cmd_ToPDF = New System.Windows.Forms.Button
        CType(Me.dgridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tp_ApplicationAR.SuspendLayout()
        Me.tp_ApplicationAP.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgridView
        '
        Me.dgridView.AllowUserToAddRows = False
        Me.dgridView.AllowUserToDeleteRows = False
        Me.dgridView.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgridView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgridView.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgridView.Location = New System.Drawing.Point(12, 12)
        Me.dgridView.Name = "dgridView"
        Me.dgridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgridView.Size = New System.Drawing.Size(996, 294)
        Me.dgridView.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txt_AROffsetAMTVAT)
        Me.GroupBox1.Controls.Add(Me.Label28)
        Me.GroupBox1.Controls.Add(Me.txt_AROffsetAMTEnergy)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.txt_totalAR)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.txt_AREnergy)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txt_ARVATEnergy)
        Me.GroupBox1.Controls.Add(Me.txt_ARTotalDIEnergy)
        Me.GroupBox1.Controls.Add(Me.txt_TotalAREnergy)
        Me.GroupBox1.Controls.Add(Me.txt_ARMF)
        Me.GroupBox1.Controls.Add(Me.txt_ARTotalDefaultMF)
        Me.GroupBox1.Controls.Add(Me.txt_ARWHTax)
        Me.GroupBox1.Controls.Add(Me.txt_ARtotalMF)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(2, -2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(494, 320)
        Me.GroupBox1.TabIndex = 21
        Me.GroupBox1.TabStop = False
        '
        'txt_AROffsetAMTVAT
        '
        Me.txt_AROffsetAMTVAT.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_AROffsetAMTVAT.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_AROffsetAMTVAT.ForeColor = System.Drawing.Color.Black
        Me.txt_AROffsetAMTVAT.Location = New System.Drawing.Point(291, 254)
        Me.txt_AROffsetAMTVAT.Name = "txt_AROffsetAMTVAT"
        Me.txt_AROffsetAMTVAT.ReadOnly = True
        Me.txt_AROffsetAMTVAT.Size = New System.Drawing.Size(197, 24)
        Me.txt_AROffsetAMTVAT.TabIndex = 41
        Me.txt_AROffsetAMTVAT.Text = "0.00"
        Me.txt_AROffsetAMTVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(11, 258)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(146, 15)
        Me.Label28.TabIndex = 40
        Me.Label28.Text = "Offset Amount VAT on Energy"
        '
        'txt_AROffsetAMTEnergy
        '
        Me.txt_AROffsetAMTEnergy.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_AROffsetAMTEnergy.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_AROffsetAMTEnergy.ForeColor = System.Drawing.Color.Black
        Me.txt_AROffsetAMTEnergy.Location = New System.Drawing.Point(291, 198)
        Me.txt_AROffsetAMTEnergy.Name = "txt_AROffsetAMTEnergy"
        Me.txt_AROffsetAMTEnergy.ReadOnly = True
        Me.txt_AROffsetAMTEnergy.Size = New System.Drawing.Size(197, 24)
        Me.txt_AROffsetAMTEnergy.TabIndex = 39
        Me.txt_AROffsetAMTEnergy.Text = "0.00"
        Me.txt_AROffsetAMTEnergy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(11, 202)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(108, 15)
        Me.Label27.TabIndex = 38
        Me.Label27.Text = "Offset Amount Energy"
        '
        'txt_totalAR
        '
        Me.txt_totalAR.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_totalAR.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_totalAR.ForeColor = System.Drawing.Color.Black
        Me.txt_totalAR.Location = New System.Drawing.Point(291, 288)
        Me.txt_totalAR.Name = "txt_totalAR"
        Me.txt_totalAR.ReadOnly = True
        Me.txt_totalAR.Size = New System.Drawing.Size(197, 24)
        Me.txt_totalAR.TabIndex = 37
        Me.txt_totalAR.Text = "0.00"
        Me.txt_totalAR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(6, 294)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(159, 15)
        Me.Label26.TabIndex = 36
        Me.Label26.Text = "Total Application to Receivable:"
        '
        'txt_AREnergy
        '
        Me.txt_AREnergy.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_AREnergy.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_AREnergy.ForeColor = System.Drawing.Color.Black
        Me.txt_AREnergy.Location = New System.Drawing.Point(291, 172)
        Me.txt_AREnergy.Name = "txt_AREnergy"
        Me.txt_AREnergy.ReadOnly = True
        Me.txt_AREnergy.Size = New System.Drawing.Size(197, 24)
        Me.txt_AREnergy.TabIndex = 35
        Me.txt_AREnergy.Text = "0.00"
        Me.txt_AREnergy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 176)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 15)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "Energy"
        '
        'txt_ARVATEnergy
        '
        Me.txt_ARVATEnergy.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_ARVATEnergy.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_ARVATEnergy.ForeColor = System.Drawing.Color.Black
        Me.txt_ARVATEnergy.Location = New System.Drawing.Point(291, 227)
        Me.txt_ARVATEnergy.Name = "txt_ARVATEnergy"
        Me.txt_ARVATEnergy.ReadOnly = True
        Me.txt_ARVATEnergy.Size = New System.Drawing.Size(197, 24)
        Me.txt_ARVATEnergy.TabIndex = 33
        Me.txt_ARVATEnergy.Text = "0.00"
        Me.txt_ARVATEnergy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_ARTotalDIEnergy
        '
        Me.txt_ARTotalDIEnergy.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_ARTotalDIEnergy.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_ARTotalDIEnergy.ForeColor = System.Drawing.Color.Black
        Me.txt_ARTotalDIEnergy.Location = New System.Drawing.Point(291, 147)
        Me.txt_ARTotalDIEnergy.Name = "txt_ARTotalDIEnergy"
        Me.txt_ARTotalDIEnergy.ReadOnly = True
        Me.txt_ARTotalDIEnergy.Size = New System.Drawing.Size(197, 24)
        Me.txt_ARTotalDIEnergy.TabIndex = 32
        Me.txt_ARTotalDIEnergy.Text = "0.00"
        Me.txt_ARTotalDIEnergy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_TotalAREnergy
        '
        Me.txt_TotalAREnergy.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_TotalAREnergy.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_TotalAREnergy.ForeColor = System.Drawing.Color.Black
        Me.txt_TotalAREnergy.Location = New System.Drawing.Point(291, 123)
        Me.txt_TotalAREnergy.Name = "txt_TotalAREnergy"
        Me.txt_TotalAREnergy.ReadOnly = True
        Me.txt_TotalAREnergy.Size = New System.Drawing.Size(197, 24)
        Me.txt_TotalAREnergy.TabIndex = 31
        Me.txt_TotalAREnergy.Text = "0.00"
        Me.txt_TotalAREnergy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_ARMF
        '
        Me.txt_ARMF.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_ARMF.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_ARMF.ForeColor = System.Drawing.Color.Black
        Me.txt_ARMF.Location = New System.Drawing.Point(291, 69)
        Me.txt_ARMF.Name = "txt_ARMF"
        Me.txt_ARMF.ReadOnly = True
        Me.txt_ARMF.Size = New System.Drawing.Size(197, 24)
        Me.txt_ARMF.TabIndex = 30
        Me.txt_ARMF.Text = "0.00"
        Me.txt_ARMF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_ARTotalDefaultMF
        '
        Me.txt_ARTotalDefaultMF.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_ARTotalDefaultMF.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_ARTotalDefaultMF.ForeColor = System.Drawing.Color.Black
        Me.txt_ARTotalDefaultMF.Location = New System.Drawing.Point(291, 93)
        Me.txt_ARTotalDefaultMF.Name = "txt_ARTotalDefaultMF"
        Me.txt_ARTotalDefaultMF.ReadOnly = True
        Me.txt_ARTotalDefaultMF.Size = New System.Drawing.Size(197, 24)
        Me.txt_ARTotalDefaultMF.TabIndex = 29
        Me.txt_ARTotalDefaultMF.Text = "0.00"
        Me.txt_ARTotalDefaultMF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_ARWHTax
        '
        Me.txt_ARWHTax.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_ARWHTax.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_ARWHTax.ForeColor = System.Drawing.Color.Black
        Me.txt_ARWHTax.Location = New System.Drawing.Point(291, 15)
        Me.txt_ARWHTax.Name = "txt_ARWHTax"
        Me.txt_ARWHTax.ReadOnly = True
        Me.txt_ARWHTax.Size = New System.Drawing.Size(197, 24)
        Me.txt_ARWHTax.TabIndex = 28
        Me.txt_ARWHTax.Text = "0.00"
        Me.txt_ARWHTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_ARtotalMF
        '
        Me.txt_ARtotalMF.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_ARtotalMF.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_ARtotalMF.ForeColor = System.Drawing.Color.Black
        Me.txt_ARtotalMF.Location = New System.Drawing.Point(291, 45)
        Me.txt_ARtotalMF.Name = "txt_ARtotalMF"
        Me.txt_ARtotalMF.ReadOnly = True
        Me.txt_ARtotalMF.Size = New System.Drawing.Size(197, 24)
        Me.txt_ARtotalMF.TabIndex = 27
        Me.txt_ARtotalMF.Text = "0.00"
        Me.txt_ARtotalMF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Market fees"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(4, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(108, 15)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Withholding Tax/VAT"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(11, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 15)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Default Interest"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(11, 73)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 15)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Market Fees"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 127)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 15)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Energy"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(11, 151)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 15)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Default Interest"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(6, 231)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(78, 15)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "VAT on Energy"
        '
        'cmd_viewDMCM
        '
        Me.cmd_viewDMCM.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_viewDMCM.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_viewDMCM.Image = Global.AccountsManagementForms.My.Resources.Resources.report
        Me.cmd_viewDMCM.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_viewDMCM.Location = New System.Drawing.Point(816, 312)
        Me.cmd_viewDMCM.Name = "cmd_viewDMCM"
        Me.cmd_viewDMCM.Size = New System.Drawing.Size(192, 30)
        Me.cmd_viewDMCM.TabIndex = 11
        Me.cmd_viewDMCM.Text = "View All DebitCredit Memo"
        Me.cmd_viewDMCM.UseVisualStyleBackColor = True
        '
        'cmd_close
        '
        Me.cmd_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_close.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_close.Image = Global.AccountsManagementForms.My.Resources.Resources.close
        Me.cmd_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_close.Location = New System.Drawing.Point(816, 453)
        Me.cmd_close.Name = "cmd_close"
        Me.cmd_close.Size = New System.Drawing.Size(192, 30)
        Me.cmd_close.TabIndex = 22
        Me.cmd_close.Text = "Close"
        Me.cmd_close.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.tp_ApplicationAR)
        Me.TabControl1.Controls.Add(Me.tp_ApplicationAP)
        Me.TabControl1.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(12, 312)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(510, 352)
        Me.TabControl1.TabIndex = 23
        '
        'tp_ApplicationAR
        '
        Me.tp_ApplicationAR.Controls.Add(Me.GroupBox1)
        Me.tp_ApplicationAR.Location = New System.Drawing.Point(4, 24)
        Me.tp_ApplicationAR.Name = "tp_ApplicationAR"
        Me.tp_ApplicationAR.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_ApplicationAR.Size = New System.Drawing.Size(502, 324)
        Me.tp_ApplicationAR.TabIndex = 0
        Me.tp_ApplicationAR.Text = "Total Application To Receivable"
        Me.tp_ApplicationAR.UseVisualStyleBackColor = True
        '
        'tp_ApplicationAP
        '
        Me.tp_ApplicationAP.Controls.Add(Me.GroupBox2)
        Me.tp_ApplicationAP.Location = New System.Drawing.Point(4, 24)
        Me.tp_ApplicationAP.Name = "tp_ApplicationAP"
        Me.tp_ApplicationAP.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_ApplicationAP.Size = New System.Drawing.Size(502, 324)
        Me.tp_ApplicationAP.TabIndex = 1
        Me.tp_ApplicationAP.Text = "Total Application to Payable"
        Me.tp_ApplicationAP.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txt_APOffsetAllocatedVATEnergy)
        Me.GroupBox2.Controls.Add(Me.Label29)
        Me.GroupBox2.Controls.Add(Me.txt_APOffsetAllocatedEnergy)
        Me.GroupBox2.Controls.Add(Me.Label30)
        Me.GroupBox2.Controls.Add(Me.txt_TotalAP)
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.txt_APEnergy)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.txt_APTotalVATEnergy)
        Me.GroupBox2.Controls.Add(Me.txt_APTotalDefaultEnergy)
        Me.GroupBox2.Controls.Add(Me.txt_APTotalEnergy)
        Me.GroupBox2.Controls.Add(Me.txt_DeferredApplied)
        Me.GroupBox2.Controls.Add(Me.txt_APDefaultInterestMF)
        Me.GroupBox2.Controls.Add(Me.txt_APMF)
        Me.GroupBox2.Controls.Add(Me.txt_APTotalMF)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(2, -2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(494, 320)
        Me.GroupBox2.TabIndex = 22
        Me.GroupBox2.TabStop = False
        '
        'txt_APOffsetAllocatedVATEnergy
        '
        Me.txt_APOffsetAllocatedVATEnergy.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_APOffsetAllocatedVATEnergy.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_APOffsetAllocatedVATEnergy.ForeColor = System.Drawing.Color.Black
        Me.txt_APOffsetAllocatedVATEnergy.Location = New System.Drawing.Point(291, 254)
        Me.txt_APOffsetAllocatedVATEnergy.Name = "txt_APOffsetAllocatedVATEnergy"
        Me.txt_APOffsetAllocatedVATEnergy.ReadOnly = True
        Me.txt_APOffsetAllocatedVATEnergy.Size = New System.Drawing.Size(197, 24)
        Me.txt_APOffsetAllocatedVATEnergy.TabIndex = 45
        Me.txt_APOffsetAllocatedVATEnergy.Text = "0.00"
        Me.txt_APOffsetAllocatedVATEnergy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(6, 258)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(156, 15)
        Me.Label29.TabIndex = 44
        Me.Label29.Text = "Offset Allocated VAT on Energy"
        '
        'txt_APOffsetAllocatedEnergy
        '
        Me.txt_APOffsetAllocatedEnergy.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_APOffsetAllocatedEnergy.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_APOffsetAllocatedEnergy.ForeColor = System.Drawing.Color.Black
        Me.txt_APOffsetAllocatedEnergy.Location = New System.Drawing.Point(291, 198)
        Me.txt_APOffsetAllocatedEnergy.Name = "txt_APOffsetAllocatedEnergy"
        Me.txt_APOffsetAllocatedEnergy.ReadOnly = True
        Me.txt_APOffsetAllocatedEnergy.Size = New System.Drawing.Size(197, 24)
        Me.txt_APOffsetAllocatedEnergy.TabIndex = 43
        Me.txt_APOffsetAllocatedEnergy.Text = "0.00"
        Me.txt_APOffsetAllocatedEnergy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(6, 202)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(118, 15)
        Me.Label30.TabIndex = 42
        Me.Label30.Text = "Offset Allocated Energy"
        '
        'txt_TotalAP
        '
        Me.txt_TotalAP.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_TotalAP.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_TotalAP.ForeColor = System.Drawing.Color.Black
        Me.txt_TotalAP.Location = New System.Drawing.Point(291, 288)
        Me.txt_TotalAP.Name = "txt_TotalAP"
        Me.txt_TotalAP.ReadOnly = True
        Me.txt_TotalAP.Size = New System.Drawing.Size(197, 24)
        Me.txt_TotalAP.TabIndex = 40
        Me.txt_TotalAP.Text = "0.00"
        Me.txt_TotalAP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(6, 294)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(146, 15)
        Me.Label25.TabIndex = 39
        Me.Label25.Text = "Total Payment To Participant"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(4, 20)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(108, 15)
        Me.Label11.TabIndex = 36
        Me.Label11.Text = "Deferred Energy/VAT"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(11, 97)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(76, 15)
        Me.Label12.TabIndex = 37
        Me.Label12.Text = "Default Interest"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(11, 73)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(61, 15)
        Me.Label13.TabIndex = 38
        Me.Label13.Text = "Market Fees"
        '
        'txt_APEnergy
        '
        Me.txt_APEnergy.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_APEnergy.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_APEnergy.ForeColor = System.Drawing.Color.Black
        Me.txt_APEnergy.Location = New System.Drawing.Point(291, 172)
        Me.txt_APEnergy.Name = "txt_APEnergy"
        Me.txt_APEnergy.ReadOnly = True
        Me.txt_APEnergy.Size = New System.Drawing.Size(197, 24)
        Me.txt_APEnergy.TabIndex = 35
        Me.txt_APEnergy.Text = "0.00"
        Me.txt_APEnergy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(11, 176)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 15)
        Me.Label8.TabIndex = 34
        Me.Label8.Text = "Energy"
        '
        'txt_APTotalVATEnergy
        '
        Me.txt_APTotalVATEnergy.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_APTotalVATEnergy.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_APTotalVATEnergy.ForeColor = System.Drawing.Color.Black
        Me.txt_APTotalVATEnergy.Location = New System.Drawing.Point(291, 227)
        Me.txt_APTotalVATEnergy.Name = "txt_APTotalVATEnergy"
        Me.txt_APTotalVATEnergy.ReadOnly = True
        Me.txt_APTotalVATEnergy.Size = New System.Drawing.Size(197, 24)
        Me.txt_APTotalVATEnergy.TabIndex = 33
        Me.txt_APTotalVATEnergy.Text = "0.00"
        Me.txt_APTotalVATEnergy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_APTotalDefaultEnergy
        '
        Me.txt_APTotalDefaultEnergy.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_APTotalDefaultEnergy.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_APTotalDefaultEnergy.ForeColor = System.Drawing.Color.Black
        Me.txt_APTotalDefaultEnergy.Location = New System.Drawing.Point(291, 147)
        Me.txt_APTotalDefaultEnergy.Name = "txt_APTotalDefaultEnergy"
        Me.txt_APTotalDefaultEnergy.ReadOnly = True
        Me.txt_APTotalDefaultEnergy.Size = New System.Drawing.Size(197, 24)
        Me.txt_APTotalDefaultEnergy.TabIndex = 32
        Me.txt_APTotalDefaultEnergy.Text = "0.00"
        Me.txt_APTotalDefaultEnergy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_APTotalEnergy
        '
        Me.txt_APTotalEnergy.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_APTotalEnergy.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_APTotalEnergy.ForeColor = System.Drawing.Color.Black
        Me.txt_APTotalEnergy.Location = New System.Drawing.Point(291, 123)
        Me.txt_APTotalEnergy.Name = "txt_APTotalEnergy"
        Me.txt_APTotalEnergy.ReadOnly = True
        Me.txt_APTotalEnergy.Size = New System.Drawing.Size(197, 24)
        Me.txt_APTotalEnergy.TabIndex = 31
        Me.txt_APTotalEnergy.Text = "0.00"
        Me.txt_APTotalEnergy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_DeferredApplied
        '
        Me.txt_DeferredApplied.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_DeferredApplied.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_DeferredApplied.ForeColor = System.Drawing.Color.Black
        Me.txt_DeferredApplied.Location = New System.Drawing.Point(291, 15)
        Me.txt_DeferredApplied.Name = "txt_DeferredApplied"
        Me.txt_DeferredApplied.ReadOnly = True
        Me.txt_DeferredApplied.Size = New System.Drawing.Size(197, 24)
        Me.txt_DeferredApplied.TabIndex = 30
        Me.txt_DeferredApplied.Text = "0.00"
        Me.txt_DeferredApplied.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_APDefaultInterestMF
        '
        Me.txt_APDefaultInterestMF.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_APDefaultInterestMF.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_APDefaultInterestMF.ForeColor = System.Drawing.Color.Black
        Me.txt_APDefaultInterestMF.Location = New System.Drawing.Point(291, 93)
        Me.txt_APDefaultInterestMF.Name = "txt_APDefaultInterestMF"
        Me.txt_APDefaultInterestMF.ReadOnly = True
        Me.txt_APDefaultInterestMF.Size = New System.Drawing.Size(197, 24)
        Me.txt_APDefaultInterestMF.TabIndex = 29
        Me.txt_APDefaultInterestMF.Text = "0.00"
        Me.txt_APDefaultInterestMF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_APMF
        '
        Me.txt_APMF.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_APMF.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_APMF.ForeColor = System.Drawing.Color.Black
        Me.txt_APMF.Location = New System.Drawing.Point(291, 69)
        Me.txt_APMF.Name = "txt_APMF"
        Me.txt_APMF.ReadOnly = True
        Me.txt_APMF.Size = New System.Drawing.Size(197, 24)
        Me.txt_APMF.TabIndex = 28
        Me.txt_APMF.Text = "0.00"
        Me.txt_APMF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_APTotalMF
        '
        Me.txt_APTotalMF.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_APTotalMF.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_APTotalMF.ForeColor = System.Drawing.Color.Black
        Me.txt_APTotalMF.Location = New System.Drawing.Point(291, 45)
        Me.txt_APTotalMF.Name = "txt_APTotalMF"
        Me.txt_APTotalMF.ReadOnly = True
        Me.txt_APTotalMF.Size = New System.Drawing.Size(197, 24)
        Me.txt_APTotalMF.TabIndex = 27
        Me.txt_APTotalMF.Text = "0.00"
        Me.txt_APTotalMF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(6, 49)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(63, 15)
        Me.Label10.TabIndex = 3
        Me.Label10.Text = "Market fees"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(6, 127)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(40, 15)
        Me.Label14.TabIndex = 7
        Me.Label14.Text = "Energy"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(11, 151)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(76, 15)
        Me.Label15.TabIndex = 9
        Me.Label15.Text = "Default Interest"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(6, 231)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(78, 15)
        Me.Label16.TabIndex = 10
        Me.Label16.Text = "VAT on Energy"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBox1.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.ForeColor = System.Drawing.Color.Black
        Me.TextBox1.Location = New System.Drawing.Point(256, 164)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(197, 23)
        Me.TextBox1.TabIndex = 35
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(11, 167)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(39, 15)
        Me.Label17.TabIndex = 34
        Me.Label17.Text = "Energy"
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBox2.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.ForeColor = System.Drawing.Color.Black
        Me.TextBox2.Location = New System.Drawing.Point(256, 190)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(197, 24)
        Me.TextBox2.TabIndex = 33
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBox3.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.ForeColor = System.Drawing.Color.Black
        Me.TextBox3.Location = New System.Drawing.Point(256, 139)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(197, 23)
        Me.TextBox3.TabIndex = 32
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBox4.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox4.ForeColor = System.Drawing.Color.Black
        Me.TextBox4.Location = New System.Drawing.Point(256, 115)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.ReadOnly = True
        Me.TextBox4.Size = New System.Drawing.Size(197, 24)
        Me.TextBox4.TabIndex = 31
        Me.TextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox5
        '
        Me.TextBox5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBox5.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox5.ForeColor = System.Drawing.Color.Black
        Me.TextBox5.Location = New System.Drawing.Point(256, 90)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.ReadOnly = True
        Me.TextBox5.Size = New System.Drawing.Size(197, 23)
        Me.TextBox5.TabIndex = 30
        Me.TextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox6
        '
        Me.TextBox6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBox6.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox6.ForeColor = System.Drawing.Color.Black
        Me.TextBox6.Location = New System.Drawing.Point(256, 66)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.ReadOnly = True
        Me.TextBox6.Size = New System.Drawing.Size(197, 23)
        Me.TextBox6.TabIndex = 29
        Me.TextBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox7
        '
        Me.TextBox7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBox7.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox7.ForeColor = System.Drawing.Color.Black
        Me.TextBox7.Location = New System.Drawing.Point(256, 42)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.ReadOnly = True
        Me.TextBox7.Size = New System.Drawing.Size(197, 23)
        Me.TextBox7.TabIndex = 28
        Me.TextBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox8
        '
        Me.TextBox8.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBox8.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox8.ForeColor = System.Drawing.Color.Black
        Me.TextBox8.Location = New System.Drawing.Point(256, 18)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.ReadOnly = True
        Me.TextBox8.Size = New System.Drawing.Size(197, 24)
        Me.TextBox8.TabIndex = 27
        Me.TextBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(6, 21)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(63, 15)
        Me.Label18.TabIndex = 3
        Me.Label18.Text = "Market fees"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(11, 45)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(84, 15)
        Me.Label19.TabIndex = 4
        Me.Label19.Text = "Withholding Tax"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(13, 69)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(76, 15)
        Me.Label20.TabIndex = 5
        Me.Label20.Text = "Default Interest"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(11, 93)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(61, 15)
        Me.Label21.TabIndex = 6
        Me.Label21.Text = "Market Fees"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(6, 118)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(40, 15)
        Me.Label22.TabIndex = 7
        Me.Label22.Text = "Energy"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(11, 142)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(76, 15)
        Me.Label23.TabIndex = 9
        Me.Label23.Text = "Default Interest"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(6, 193)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(78, 15)
        Me.Label24.TabIndex = 10
        Me.Label24.Text = "VAT on Energy"
        '
        'cmd_DownloadCSV
        '
        Me.cmd_DownloadCSV.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_DownloadCSV.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_DownloadCSV.Image = Global.AccountsManagementForms.My.Resources.Resources.load
        Me.cmd_DownloadCSV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_DownloadCSV.Location = New System.Drawing.Point(816, 347)
        Me.cmd_DownloadCSV.Name = "cmd_DownloadCSV"
        Me.cmd_DownloadCSV.Size = New System.Drawing.Size(192, 30)
        Me.cmd_DownloadCSV.TabIndex = 45
        Me.cmd_DownloadCSV.Text = "Download To CSV"
        Me.cmd_DownloadCSV.UseVisualStyleBackColor = True
        '
        'TextBox10
        '
        Me.TextBox10.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBox10.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox10.ForeColor = System.Drawing.Color.Black
        Me.TextBox10.Location = New System.Drawing.Point(291, 225)
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.ReadOnly = True
        Me.TextBox10.Size = New System.Drawing.Size(197, 24)
        Me.TextBox10.TabIndex = 37
        Me.TextBox10.Text = "0.00"
        Me.TextBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(4, 230)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(159, 15)
        Me.Label32.TabIndex = 36
        Me.Label32.Text = "Total Application to Receivable:"
        '
        'TextBox11
        '
        Me.TextBox11.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBox11.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox11.ForeColor = System.Drawing.Color.Black
        Me.TextBox11.Location = New System.Drawing.Point(291, 163)
        Me.TextBox11.Name = "TextBox11"
        Me.TextBox11.ReadOnly = True
        Me.TextBox11.Size = New System.Drawing.Size(197, 24)
        Me.TextBox11.TabIndex = 35
        Me.TextBox11.Text = "0.00"
        Me.TextBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(11, 167)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(39, 15)
        Me.Label33.TabIndex = 34
        Me.Label33.Text = "Energy"
        '
        'TextBox12
        '
        Me.TextBox12.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBox12.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox12.ForeColor = System.Drawing.Color.Black
        Me.TextBox12.Location = New System.Drawing.Point(291, 189)
        Me.TextBox12.Name = "TextBox12"
        Me.TextBox12.ReadOnly = True
        Me.TextBox12.Size = New System.Drawing.Size(197, 24)
        Me.TextBox12.TabIndex = 33
        Me.TextBox12.Text = "0.00"
        Me.TextBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox13
        '
        Me.TextBox13.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBox13.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox13.ForeColor = System.Drawing.Color.Black
        Me.TextBox13.Location = New System.Drawing.Point(291, 138)
        Me.TextBox13.Name = "TextBox13"
        Me.TextBox13.ReadOnly = True
        Me.TextBox13.Size = New System.Drawing.Size(197, 24)
        Me.TextBox13.TabIndex = 32
        Me.TextBox13.Text = "0.00"
        Me.TextBox13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox14
        '
        Me.TextBox14.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBox14.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox14.ForeColor = System.Drawing.Color.Black
        Me.TextBox14.Location = New System.Drawing.Point(291, 114)
        Me.TextBox14.Name = "TextBox14"
        Me.TextBox14.ReadOnly = True
        Me.TextBox14.Size = New System.Drawing.Size(197, 24)
        Me.TextBox14.TabIndex = 31
        Me.TextBox14.Text = "0.00"
        Me.TextBox14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox15
        '
        Me.TextBox15.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBox15.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox15.ForeColor = System.Drawing.Color.Black
        Me.TextBox15.Location = New System.Drawing.Point(291, 41)
        Me.TextBox15.Name = "TextBox15"
        Me.TextBox15.ReadOnly = True
        Me.TextBox15.Size = New System.Drawing.Size(197, 24)
        Me.TextBox15.TabIndex = 30
        Me.TextBox15.Text = "0.00"
        Me.TextBox15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox16
        '
        Me.TextBox16.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBox16.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox16.ForeColor = System.Drawing.Color.Black
        Me.TextBox16.Location = New System.Drawing.Point(291, 65)
        Me.TextBox16.Name = "TextBox16"
        Me.TextBox16.ReadOnly = True
        Me.TextBox16.Size = New System.Drawing.Size(197, 24)
        Me.TextBox16.TabIndex = 29
        Me.TextBox16.Text = "0.00"
        Me.TextBox16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox17
        '
        Me.TextBox17.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBox17.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox17.ForeColor = System.Drawing.Color.Black
        Me.TextBox17.Location = New System.Drawing.Point(291, 89)
        Me.TextBox17.Name = "TextBox17"
        Me.TextBox17.ReadOnly = True
        Me.TextBox17.Size = New System.Drawing.Size(197, 24)
        Me.TextBox17.TabIndex = 28
        Me.TextBox17.Text = "0.00"
        Me.TextBox17.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox18
        '
        Me.TextBox18.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBox18.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox18.ForeColor = System.Drawing.Color.Black
        Me.TextBox18.Location = New System.Drawing.Point(291, 17)
        Me.TextBox18.Name = "TextBox18"
        Me.TextBox18.ReadOnly = True
        Me.TextBox18.Size = New System.Drawing.Size(197, 24)
        Me.TextBox18.TabIndex = 27
        Me.TextBox18.Text = "0.00"
        Me.TextBox18.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(6, 21)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(63, 15)
        Me.Label34.TabIndex = 3
        Me.Label34.Text = "Market fees"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(4, 94)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(85, 15)
        Me.Label35.TabIndex = 4
        Me.Label35.Text = "Withholding Tax"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(11, 69)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(76, 15)
        Me.Label36.TabIndex = 5
        Me.Label36.Text = "Default Interest"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(11, 45)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(61, 15)
        Me.Label37.TabIndex = 6
        Me.Label37.Text = "Market Fees"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(6, 118)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(40, 15)
        Me.Label38.TabIndex = 7
        Me.Label38.Text = "Energy"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(11, 142)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(76, 15)
        Me.Label39.TabIndex = 9
        Me.Label39.Text = "Default Interest"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(6, 193)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(78, 15)
        Me.Label40.TabIndex = 10
        Me.Label40.Text = "VAT on Energy"
        '
        'cmd_viewOR
        '
        Me.cmd_viewOR.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_viewOR.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_viewOR.Image = Global.AccountsManagementForms.My.Resources.Resources.search
        Me.cmd_viewOR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_viewOR.Location = New System.Drawing.Point(816, 419)
        Me.cmd_viewOR.Name = "cmd_viewOR"
        Me.cmd_viewOR.Size = New System.Drawing.Size(192, 30)
        Me.cmd_viewOR.TabIndex = 46
        Me.cmd_viewOR.Text = "View OR"
        Me.cmd_viewOR.UseVisualStyleBackColor = True
        '
        'cmd_ToPDF
        '
        Me.cmd_ToPDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_ToPDF.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ToPDF.Image = Global.AccountsManagementForms.My.Resources.Resources.load
        Me.cmd_ToPDF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_ToPDF.Location = New System.Drawing.Point(816, 383)
        Me.cmd_ToPDF.Name = "cmd_ToPDF"
        Me.cmd_ToPDF.Size = New System.Drawing.Size(192, 30)
        Me.cmd_ToPDF.TabIndex = 47
        Me.cmd_ToPDF.Text = "Export To PDF"
        Me.cmd_ToPDF.UseVisualStyleBackColor = True
        '
        'frmPaymentAllocationViewDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1020, 676)
        Me.Controls.Add(Me.cmd_ToPDF)
        Me.Controls.Add(Me.cmd_viewOR)
        Me.Controls.Add(Me.cmd_DownloadCSV)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.cmd_close)
        Me.Controls.Add(Me.dgridView)
        Me.Controls.Add(Me.cmd_viewDMCM)
        Me.Name = "frmPaymentAllocationViewDetails"
        Me.Text = "View Payment Per Participant's Account Details - "
        CType(Me.dgridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.tp_ApplicationAR.ResumeLayout(False)
        Me.tp_ApplicationAP.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgridView As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txt_ARVATEnergy As System.Windows.Forms.TextBox
    Friend WithEvents txt_ARTotalDIEnergy As System.Windows.Forms.TextBox
    Friend WithEvents txt_TotalAREnergy As System.Windows.Forms.TextBox
    Friend WithEvents txt_ARMF As System.Windows.Forms.TextBox
    Friend WithEvents txt_ARTotalDefaultMF As System.Windows.Forms.TextBox
    Friend WithEvents txt_ARWHTax As System.Windows.Forms.TextBox
    Friend WithEvents txt_ARtotalMF As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmd_viewDMCM As System.Windows.Forms.Button
    Friend WithEvents cmd_close As System.Windows.Forms.Button
    Friend WithEvents txt_AREnergy As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tp_ApplicationAR As System.Windows.Forms.TabPage
    Friend WithEvents tp_ApplicationAP As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txt_APEnergy As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txt_APTotalVATEnergy As System.Windows.Forms.TextBox
    Friend WithEvents txt_APTotalDefaultEnergy As System.Windows.Forms.TextBox
    Friend WithEvents txt_APTotalEnergy As System.Windows.Forms.TextBox
    Friend WithEvents txt_DeferredApplied As System.Windows.Forms.TextBox
    Friend WithEvents txt_APDefaultInterestMF As System.Windows.Forms.TextBox
    Friend WithEvents txt_APMF As System.Windows.Forms.TextBox
    Friend WithEvents txt_APTotalMF As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox8 As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents cmd_DownloadCSV As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txt_TotalAP As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txt_totalAR As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents TextBox10 As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents TextBox11 As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents TextBox12 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox13 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox14 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox15 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox16 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox17 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox18 As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents txt_AROffsetAMTVAT As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txt_AROffsetAMTEnergy As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txt_APOffsetAllocatedVATEnergy As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txt_APOffsetAllocatedEnergy As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents cmd_viewOR As System.Windows.Forms.Button
    Friend WithEvents cmd_ToPDF As System.Windows.Forms.Button
End Class
