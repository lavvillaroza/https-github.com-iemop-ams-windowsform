<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class MainForm : Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStatusTip = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.TOTALREC_TXTBOX = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PAGENO_CMB = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.CLEAR_BTN = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SEARCH_BTN = New System.Windows.Forms.Button()
        Me.SEARCHVAL_TXTBOX = New System.Windows.Forms.TextBox()
        Me.MAIN_CATEGORY_CMB = New System.Windows.Forms.ComboBox()
        Me.DynamicDGV = New System.Windows.Forms.DataGridView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Delete_btn = New System.Windows.Forms.Button()
        Me.Add_btn = New System.Windows.Forms.Button()
        Me.Edit_btn = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Logs_btn = New System.Windows.Forms.Button()
        Me.UsersModules_btn = New System.Windows.Forms.Button()
        Me.Users_btn = New System.Windows.Forms.Button()
        Me.Modules_btn = New System.Windows.Forms.Button()
        Me.Applications_btn = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.DynamicDGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.StatusStrip1)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1018, 626)
        Me.Panel1.TabIndex = 0
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStatusTip})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 600)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1014, 22)
        Me.StatusStrip1.TabIndex = 6
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStatusTip
        '
        Me.ToolStatusTip.Name = "ToolStatusTip"
        Me.ToolStatusTip.Size = New System.Drawing.Size(0, 17)
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.PictureBox1)
        Me.Panel3.Location = New System.Drawing.Point(4, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1007, 67)
        Me.Panel3.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calisto MT", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Gold
        Me.Label1.Location = New System.Drawing.Point(74, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(360, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Philippine Electricity Market Corporation (PEMC)"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(67, 60)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Panel6)
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Controls.Add(Me.DynamicDGV)
        Me.Panel2.Location = New System.Drawing.Point(218, 75)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(793, 522)
        Me.Panel2.TabIndex = 1
        '
        'Panel6
        '
        Me.Panel6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel6.BackColor = System.Drawing.SystemColors.Control
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.TOTALREC_TXTBOX)
        Me.Panel6.Controls.Add(Me.Label6)
        Me.Panel6.Controls.Add(Me.Label5)
        Me.Panel6.Controls.Add(Me.PAGENO_CMB)
        Me.Panel6.Controls.Add(Me.Label4)
        Me.Panel6.Location = New System.Drawing.Point(6, 484)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(780, 33)
        Me.Panel6.TabIndex = 4
        '
        'TOTALREC_TXTBOX
        '
        Me.TOTALREC_TXTBOX.BackColor = System.Drawing.SystemColors.Control
        Me.TOTALREC_TXTBOX.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TOTALREC_TXTBOX.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TOTALREC_TXTBOX.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TOTALREC_TXTBOX.Location = New System.Drawing.Point(98, 6)
        Me.TOTALREC_TXTBOX.Name = "TOTALREC_TXTBOX"
        Me.TOTALREC_TXTBOX.ReadOnly = True
        Me.TOTALREC_TXTBOX.Size = New System.Drawing.Size(100, 16)
        Me.TOTALREC_TXTBOX.TabIndex = 6
        Me.TOTALREC_TXTBOX.Text = "0"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.DimGray
        Me.Label6.Location = New System.Drawing.Point(85, 7)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(11, 16)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = ":"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.DimGray
        Me.Label5.Location = New System.Drawing.Point(4, 7)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 15)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Total Records"
        '
        'PAGENO_CMB
        '
        Me.PAGENO_CMB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PAGENO_CMB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.PAGENO_CMB.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PAGENO_CMB.FormattingEnabled = True
        Me.PAGENO_CMB.Location = New System.Drawing.Point(715, 5)
        Me.PAGENO_CMB.Name = "PAGENO_CMB"
        Me.PAGENO_CMB.Size = New System.Drawing.Size(53, 23)
        Me.PAGENO_CMB.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(669, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 15)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Pages"
        '
        'Panel5
        '
        Me.Panel5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel5.Controls.Add(Me.CLEAR_BTN)
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Controls.Add(Me.Label2)
        Me.Panel5.Controls.Add(Me.SEARCH_BTN)
        Me.Panel5.Controls.Add(Me.SEARCHVAL_TXTBOX)
        Me.Panel5.Controls.Add(Me.MAIN_CATEGORY_CMB)
        Me.Panel5.Location = New System.Drawing.Point(6, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(780, 30)
        Me.Panel5.TabIndex = 0
        '
        'CLEAR_BTN
        '
        Me.CLEAR_BTN.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CLEAR_BTN.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CLEAR_BTN.Location = New System.Drawing.Point(696, 3)
        Me.CLEAR_BTN.Name = "CLEAR_BTN"
        Me.CLEAR_BTN.Size = New System.Drawing.Size(75, 23)
        Me.CLEAR_BTN.TabIndex = 11
        Me.CLEAR_BTN.Text = "Clear"
        Me.CLEAR_BTN.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(312, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 14)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = ":"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(258, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 14)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Category"
        '
        'SEARCH_BTN
        '
        Me.SEARCH_BTN.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SEARCH_BTN.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SEARCH_BTN.Location = New System.Drawing.Point(615, 4)
        Me.SEARCH_BTN.Name = "SEARCH_BTN"
        Me.SEARCH_BTN.Size = New System.Drawing.Size(75, 23)
        Me.SEARCH_BTN.TabIndex = 8
        Me.SEARCH_BTN.Text = "Search"
        Me.SEARCH_BTN.UseVisualStyleBackColor = True
        '
        'SEARCHVAL_TXTBOX
        '
        Me.SEARCHVAL_TXTBOX.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SEARCHVAL_TXTBOX.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SEARCHVAL_TXTBOX.Location = New System.Drawing.Point(458, 4)
        Me.SEARCHVAL_TXTBOX.Name = "SEARCHVAL_TXTBOX"
        Me.SEARCHVAL_TXTBOX.Size = New System.Drawing.Size(151, 22)
        Me.SEARCHVAL_TXTBOX.TabIndex = 7
        '
        'MAIN_CATEGORY_CMB
        '
        Me.MAIN_CATEGORY_CMB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MAIN_CATEGORY_CMB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.MAIN_CATEGORY_CMB.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MAIN_CATEGORY_CMB.FormattingEnabled = True
        Me.MAIN_CATEGORY_CMB.Location = New System.Drawing.Point(328, 4)
        Me.MAIN_CATEGORY_CMB.Name = "MAIN_CATEGORY_CMB"
        Me.MAIN_CATEGORY_CMB.Size = New System.Drawing.Size(121, 22)
        Me.MAIN_CATEGORY_CMB.TabIndex = 6
        '
        'DynamicDGV
        '
        Me.DynamicDGV.AllowUserToAddRows = False
        Me.DynamicDGV.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Gold
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SkyBlue
        Me.DynamicDGV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DynamicDGV.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DynamicDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DynamicDGV.BackgroundColor = System.Drawing.Color.DimGray
        Me.DynamicDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SkyBlue
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DynamicDGV.DefaultCellStyle = DataGridViewCellStyle2
        Me.DynamicDGV.Location = New System.Drawing.Point(6, 36)
        Me.DynamicDGV.MultiSelect = False
        Me.DynamicDGV.Name = "DynamicDGV"
        Me.DynamicDGV.RowHeadersWidth = 20
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SkyBlue
        Me.DynamicDGV.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DynamicDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DynamicDGV.Size = New System.Drawing.Size(780, 442)
        Me.DynamicDGV.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.GroupBox3)
        Me.Panel4.Controls.Add(Me.GroupBox1)
        Me.Panel4.Location = New System.Drawing.Point(4, 75)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(210, 522)
        Me.Panel4.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.Delete_btn)
        Me.GroupBox3.Controls.Add(Me.Add_btn)
        Me.GroupBox3.Controls.Add(Me.Edit_btn)
        Me.GroupBox3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(8, 257)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(194, 221)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Controls"
        '
        'Delete_btn
        '
        Me.Delete_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Delete_btn.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Delete_btn.Location = New System.Drawing.Point(15, 107)
        Me.Delete_btn.Name = "Delete_btn"
        Me.Delete_btn.Size = New System.Drawing.Size(167, 31)
        Me.Delete_btn.TabIndex = 2
        Me.Delete_btn.Text = "Delete"
        Me.Delete_btn.UseVisualStyleBackColor = True
        '
        'Add_btn
        '
        Me.Add_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Add_btn.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Add_btn.Location = New System.Drawing.Point(15, 32)
        Me.Add_btn.Name = "Add_btn"
        Me.Add_btn.Size = New System.Drawing.Size(167, 31)
        Me.Add_btn.TabIndex = 0
        Me.Add_btn.Text = "Add"
        Me.Add_btn.UseVisualStyleBackColor = True
        '
        'Edit_btn
        '
        Me.Edit_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Edit_btn.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Edit_btn.Location = New System.Drawing.Point(15, 69)
        Me.Edit_btn.Name = "Edit_btn"
        Me.Edit_btn.Size = New System.Drawing.Size(167, 31)
        Me.Edit_btn.TabIndex = 1
        Me.Edit_btn.Text = "Edit"
        Me.Edit_btn.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Logs_btn)
        Me.GroupBox1.Controls.Add(Me.UsersModules_btn)
        Me.GroupBox1.Controls.Add(Me.Users_btn)
        Me.GroupBox1.Controls.Add(Me.Modules_btn)
        Me.GroupBox1.Controls.Add(Me.Applications_btn)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(8, 17)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(194, 234)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Main Menu"
        '
        'Logs_btn
        '
        Me.Logs_btn.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Logs_btn.Location = New System.Drawing.Point(15, 19)
        Me.Logs_btn.Name = "Logs_btn"
        Me.Logs_btn.Size = New System.Drawing.Size(167, 36)
        Me.Logs_btn.TabIndex = 0
        Me.Logs_btn.Text = "Logs"
        Me.Logs_btn.UseVisualStyleBackColor = True
        '
        'UsersModules_btn
        '
        Me.UsersModules_btn.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsersModules_btn.Location = New System.Drawing.Point(15, 183)
        Me.UsersModules_btn.Name = "UsersModules_btn"
        Me.UsersModules_btn.Size = New System.Drawing.Size(167, 34)
        Me.UsersModules_btn.TabIndex = 4
        Me.UsersModules_btn.Text = "Users Modules"
        Me.UsersModules_btn.UseVisualStyleBackColor = True
        '
        'Users_btn
        '
        Me.Users_btn.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Users_btn.Location = New System.Drawing.Point(15, 61)
        Me.Users_btn.Name = "Users_btn"
        Me.Users_btn.Size = New System.Drawing.Size(167, 34)
        Me.Users_btn.TabIndex = 1
        Me.Users_btn.Text = "Users"
        Me.Users_btn.UseVisualStyleBackColor = True
        '
        'Modules_btn
        '
        Me.Modules_btn.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Modules_btn.Location = New System.Drawing.Point(15, 143)
        Me.Modules_btn.Name = "Modules_btn"
        Me.Modules_btn.Size = New System.Drawing.Size(167, 34)
        Me.Modules_btn.TabIndex = 3
        Me.Modules_btn.Text = "Modules"
        Me.Modules_btn.UseVisualStyleBackColor = True
        '
        'Applications_btn
        '
        Me.Applications_btn.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Applications_btn.Location = New System.Drawing.Point(15, 101)
        Me.Applications_btn.Name = "Applications_btn"
        Me.Applications_btn.Size = New System.Drawing.Size(167, 36)
        Me.Applications_btn.TabIndex = 2
        Me.Applications_btn.Text = "Applications"
        Me.Applications_btn.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1019, 626)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Logical Access Manager"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.DynamicDGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Logs_btn As System.Windows.Forms.Button
    Friend WithEvents UsersModules_btn As System.Windows.Forms.Button
    Friend WithEvents Users_btn As System.Windows.Forms.Button
    Friend WithEvents Modules_btn As System.Windows.Forms.Button
    Friend WithEvents Applications_btn As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Delete_btn As System.Windows.Forms.Button
    Friend WithEvents Edit_btn As System.Windows.Forms.Button
    Friend WithEvents Add_btn As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SEARCH_BTN As System.Windows.Forms.Button
    Friend WithEvents SEARCHVAL_TXTBOX As System.Windows.Forms.TextBox
    Friend WithEvents MAIN_CATEGORY_CMB As System.Windows.Forms.ComboBox
    Friend WithEvents DynamicDGV As System.Windows.Forms.DataGridView
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents CLEAR_BTN As System.Windows.Forms.Button
    Friend WithEvents ToolStatusTip As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents PAGENO_CMB As System.Windows.Forms.ComboBox
    Friend WithEvents TOTALREC_TXTBOX As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label

End Class
