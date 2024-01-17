<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymentNewOffsetting
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GB_Details = New System.Windows.Forms.GroupBox()
        Me.dgv_ARAPDetails = New System.Windows.Forms.DataGridView()
        Me.GB_Main = New System.Windows.Forms.GroupBox()
        Me.dgv_ShareOnInvoice = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Btn_Close = New System.Windows.Forms.Button()
        Me.Btn_GenerateOR = New System.Windows.Forms.Button()
        Me.Btn_GenerateDMCM = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtbox_TotalAmountShareARAP = New System.Windows.Forms.TextBox()
        Me.txtbox_TotalAmountShareARAPBalance = New System.Windows.Forms.TextBox()
        Me.txtbox_TotalAmountOffsetAPAR = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtbox_TotalAmountAllocARAP = New System.Windows.Forms.TextBox()
        Me.ToolTip_2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GB_Details.SuspendLayout()
        CType(Me.dgv_ARAPDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GB_Main.SuspendLayout()
        CType(Me.dgv_ShareOnInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GB_Details, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.GB_Main, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel6, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox2, 0, 3)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(932, 551)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'GB_Details
        '
        Me.GB_Details.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GB_Details.Controls.Add(Me.dgv_ARAPDetails)
        Me.GB_Details.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GB_Details.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GB_Details.Location = New System.Drawing.Point(3, 248)
        Me.GB_Details.Name = "GB_Details"
        Me.GB_Details.Size = New System.Drawing.Size(926, 190)
        Me.GB_Details.TabIndex = 4
        Me.GB_Details.TabStop = False
        Me.GB_Details.Text = "Details"
        '
        'dgv_ARAPDetails
        '
        Me.dgv_ARAPDetails.AllowUserToAddRows = False
        Me.dgv_ARAPDetails.AllowUserToDeleteRows = False
        Me.dgv_ARAPDetails.AllowUserToResizeColumns = False
        Me.dgv_ARAPDetails.AllowUserToResizeRows = False
        Me.dgv_ARAPDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_ARAPDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_ARAPDetails.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_ARAPDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LemonChiffon
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_ARAPDetails.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_ARAPDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv_ARAPDetails.Location = New System.Drawing.Point(6, 17)
        Me.dgv_ARAPDetails.Name = "dgv_ARAPDetails"
        Me.dgv_ARAPDetails.RowHeadersVisible = False
        Me.dgv_ARAPDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgv_ARAPDetails.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_ARAPDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_ARAPDetails.Size = New System.Drawing.Size(914, 167)
        Me.dgv_ARAPDetails.TabIndex = 20
        '
        'GB_Main
        '
        Me.GB_Main.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GB_Main.Controls.Add(Me.dgv_ShareOnInvoice)
        Me.GB_Main.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GB_Main.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GB_Main.Location = New System.Drawing.Point(3, 3)
        Me.GB_Main.Name = "GB_Main"
        Me.GB_Main.Size = New System.Drawing.Size(926, 190)
        Me.GB_Main.TabIndex = 3
        Me.GB_Main.TabStop = False
        Me.GB_Main.Text = "Main"
        '
        'dgv_ShareOnInvoice
        '
        Me.dgv_ShareOnInvoice.AllowUserToAddRows = False
        Me.dgv_ShareOnInvoice.AllowUserToDeleteRows = False
        Me.dgv_ShareOnInvoice.AllowUserToResizeColumns = False
        Me.dgv_ShareOnInvoice.AllowUserToResizeRows = False
        Me.dgv_ShareOnInvoice.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_ShareOnInvoice.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_ShareOnInvoice.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv_ShareOnInvoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.LemonChiffon
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_ShareOnInvoice.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgv_ShareOnInvoice.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv_ShareOnInvoice.Location = New System.Drawing.Point(6, 17)
        Me.dgv_ShareOnInvoice.Name = "dgv_ShareOnInvoice"
        Me.dgv_ShareOnInvoice.RowHeadersVisible = False
        Me.dgv_ShareOnInvoice.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgv_ShareOnInvoice.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.dgv_ShareOnInvoice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_ShareOnInvoice.Size = New System.Drawing.Size(914, 167)
        Me.dgv_ShareOnInvoice.TabIndex = 20
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel6.ColumnCount = 2
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 550.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.Panel1, 1, 0)
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(3, 493)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(926, 55)
        Me.TableLayoutPanel6.TabIndex = 8
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Btn_Close)
        Me.Panel1.Controls.Add(Me.Btn_GenerateOR)
        Me.Panel1.Controls.Add(Me.Btn_GenerateDMCM)
        Me.Panel1.Location = New System.Drawing.Point(379, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(544, 49)
        Me.Panel1.TabIndex = 0
        '
        'Btn_Close
        '
        Me.Btn_Close.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Btn_Close.BackColor = System.Drawing.Color.White
        Me.Btn_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Btn_Close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.Btn_Close.FlatAppearance.CheckedBackColor = System.Drawing.Color.LemonChiffon
        Me.Btn_Close.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control
        Me.Btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Close.ForeColor = System.Drawing.Color.Black
        Me.Btn_Close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.Btn_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btn_Close.Location = New System.Drawing.Point(400, 7)
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.Size = New System.Drawing.Size(139, 39)
        Me.Btn_Close.TabIndex = 1
        Me.Btn_Close.Text = "Cancel"
        Me.Btn_Close.UseVisualStyleBackColor = False
        '
        'Btn_GenerateOR
        '
        Me.Btn_GenerateOR.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Btn_GenerateOR.BackColor = System.Drawing.Color.White
        Me.Btn_GenerateOR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Btn_GenerateOR.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.Btn_GenerateOR.FlatAppearance.CheckedBackColor = System.Drawing.Color.LemonChiffon
        Me.Btn_GenerateOR.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control
        Me.Btn_GenerateOR.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_GenerateOR.ForeColor = System.Drawing.Color.Black
        Me.Btn_GenerateOR.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.Btn_GenerateOR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btn_GenerateOR.Location = New System.Drawing.Point(110, 7)
        Me.Btn_GenerateOR.Name = "Btn_GenerateOR"
        Me.Btn_GenerateOR.Size = New System.Drawing.Size(139, 39)
        Me.Btn_GenerateOR.TabIndex = 3
        Me.Btn_GenerateOR.Text = "Generate OR"
        Me.Btn_GenerateOR.UseVisualStyleBackColor = False
        '
        'Btn_GenerateDMCM
        '
        Me.Btn_GenerateDMCM.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Btn_GenerateDMCM.BackColor = System.Drawing.Color.White
        Me.Btn_GenerateDMCM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Btn_GenerateDMCM.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.Btn_GenerateDMCM.FlatAppearance.CheckedBackColor = System.Drawing.Color.LemonChiffon
        Me.Btn_GenerateDMCM.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control
        Me.Btn_GenerateDMCM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_GenerateDMCM.ForeColor = System.Drawing.Color.Black
        Me.Btn_GenerateDMCM.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.Btn_GenerateDMCM.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btn_GenerateDMCM.Location = New System.Drawing.Point(255, 7)
        Me.Btn_GenerateDMCM.Name = "Btn_GenerateDMCM"
        Me.Btn_GenerateDMCM.Size = New System.Drawing.Size(139, 39)
        Me.Btn_GenerateDMCM.TabIndex = 2
        Me.Btn_GenerateDMCM.Text = "   Generate DMCM"
        Me.Btn_GenerateDMCM.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txtbox_TotalAmountShareARAP)
        Me.GroupBox1.Controls.Add(Me.txtbox_TotalAmountShareARAPBalance)
        Me.GroupBox1.Controls.Add(Me.txtbox_TotalAmountOffsetAPAR)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 199)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.Size = New System.Drawing.Size(926, 43)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        '
        'txtbox_TotalAmountShareARAP
        '
        Me.txtbox_TotalAmountShareARAP.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.txtbox_TotalAmountShareARAP.BackColor = System.Drawing.SystemColors.Info
        Me.txtbox_TotalAmountShareARAP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbox_TotalAmountShareARAP.ForeColor = System.Drawing.Color.Black
        Me.txtbox_TotalAmountShareARAP.Location = New System.Drawing.Point(404, 16)
        Me.txtbox_TotalAmountShareARAP.Name = "txtbox_TotalAmountShareARAP"
        Me.txtbox_TotalAmountShareARAP.ReadOnly = True
        Me.txtbox_TotalAmountShareARAP.Size = New System.Drawing.Size(168, 20)
        Me.txtbox_TotalAmountShareARAP.TabIndex = 12
        Me.txtbox_TotalAmountShareARAP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip_2.SetToolTip(Me.txtbox_TotalAmountShareARAP, "Total Amount Share Received")
        '
        'txtbox_TotalAmountShareARAPBalance
        '
        Me.txtbox_TotalAmountShareARAPBalance.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.txtbox_TotalAmountShareARAPBalance.BackColor = System.Drawing.SystemColors.Info
        Me.txtbox_TotalAmountShareARAPBalance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbox_TotalAmountShareARAPBalance.ForeColor = System.Drawing.Color.Black
        Me.txtbox_TotalAmountShareARAPBalance.Location = New System.Drawing.Point(752, 16)
        Me.txtbox_TotalAmountShareARAPBalance.Name = "txtbox_TotalAmountShareARAPBalance"
        Me.txtbox_TotalAmountShareARAPBalance.ReadOnly = True
        Me.txtbox_TotalAmountShareARAPBalance.Size = New System.Drawing.Size(168, 20)
        Me.txtbox_TotalAmountShareARAPBalance.TabIndex = 11
        Me.txtbox_TotalAmountShareARAPBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip_2.SetToolTip(Me.txtbox_TotalAmountShareARAPBalance, "Total Amount Share Balance")
        '
        'txtbox_TotalAmountOffsetAPAR
        '
        Me.txtbox_TotalAmountOffsetAPAR.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.txtbox_TotalAmountOffsetAPAR.BackColor = System.Drawing.SystemColors.Info
        Me.txtbox_TotalAmountOffsetAPAR.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbox_TotalAmountOffsetAPAR.ForeColor = System.Drawing.Color.Black
        Me.txtbox_TotalAmountOffsetAPAR.Location = New System.Drawing.Point(578, 16)
        Me.txtbox_TotalAmountOffsetAPAR.Name = "txtbox_TotalAmountOffsetAPAR"
        Me.txtbox_TotalAmountOffsetAPAR.ReadOnly = True
        Me.txtbox_TotalAmountOffsetAPAR.Size = New System.Drawing.Size(168, 20)
        Me.txtbox_TotalAmountOffsetAPAR.TabIndex = 10
        Me.txtbox_TotalAmountOffsetAPAR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip_2.SetToolTip(Me.txtbox_TotalAmountOffsetAPAR, "Total Amount Offsetted")
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.txtbox_TotalAmountAllocARAP)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 444)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(926, 43)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        '
        'txtbox_TotalAmountAllocARAP
        '
        Me.txtbox_TotalAmountAllocARAP.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.txtbox_TotalAmountAllocARAP.BackColor = System.Drawing.SystemColors.Info
        Me.txtbox_TotalAmountAllocARAP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbox_TotalAmountAllocARAP.ForeColor = System.Drawing.Color.Black
        Me.txtbox_TotalAmountAllocARAP.Location = New System.Drawing.Point(752, 17)
        Me.txtbox_TotalAmountAllocARAP.Name = "txtbox_TotalAmountAllocARAP"
        Me.txtbox_TotalAmountAllocARAP.ReadOnly = True
        Me.txtbox_TotalAmountAllocARAP.Size = New System.Drawing.Size(168, 20)
        Me.txtbox_TotalAmountAllocARAP.TabIndex = 10
        Me.txtbox_TotalAmountAllocARAP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip_2.SetToolTip(Me.txtbox_TotalAmountAllocARAP, "Total Amount Received")
        '
        'ToolTip_2
        '
        Me.ToolTip_2.AutomaticDelay = 100
        Me.ToolTip_2.AutoPopDelay = 1000
        Me.ToolTip_2.BackColor = System.Drawing.SystemColors.Menu
        Me.ToolTip_2.ForeColor = System.Drawing.Color.Red
        Me.ToolTip_2.InitialDelay = 100
        Me.ToolTip_2.ReshowDelay = 10
        Me.ToolTip_2.ShowAlways = True
        Me.ToolTip_2.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTip_2.ToolTipTitle = "Tool Tip"
        '
        'frmPaymentNewOffsetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(935, 554)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmPaymentNewOffsetting"
        Me.Text = "Payment DMCM Break Down"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GB_Details.ResumeLayout(False)
        CType(Me.dgv_ARAPDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GB_Main.ResumeLayout(False)
        CType(Me.dgv_ShareOnInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Btn_Close As System.Windows.Forms.Button
    Friend WithEvents txtbox_TotalAmountOffsetAPAR As System.Windows.Forms.TextBox
    Friend WithEvents GB_Main As System.Windows.Forms.GroupBox
    Friend WithEvents dgv_ShareOnInvoice As System.Windows.Forms.DataGridView
    Friend WithEvents txtbox_TotalAmountAllocARAP As System.Windows.Forms.TextBox
    Friend WithEvents GB_Details As System.Windows.Forms.GroupBox
    Friend WithEvents dgv_ARAPDetails As System.Windows.Forms.DataGridView
    Friend WithEvents txtbox_TotalAmountShareARAP As System.Windows.Forms.TextBox
    Friend WithEvents txtbox_TotalAmountShareARAPBalance As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip_2 As System.Windows.Forms.ToolTip
    Friend WithEvents Btn_GenerateDMCM As System.Windows.Forms.Button
    Friend WithEvents Btn_GenerateOR As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel6 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
End Class
