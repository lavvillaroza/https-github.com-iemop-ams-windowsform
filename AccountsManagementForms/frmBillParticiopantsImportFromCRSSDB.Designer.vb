<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBillParticiopantsImportFromCRSSDB
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
        Me.TC_Import = New System.Windows.Forms.TabControl()
        Me.tp_New = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.DGridViewNew = New System.Windows.Forms.DataGridView()
        Me.cbAdd = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.IDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FullName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Billing_Address = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Renewable = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ZeroRated = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IncomeTaxHoliday = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FacilityType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColRegion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MembershipType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cb_SelectAll = New System.Windows.Forms.CheckBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.TC_Import.SuspendLayout()
        Me.tp_New.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.DGridViewNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TC_Import
        '
        Me.TC_Import.Controls.Add(Me.tp_New)
        Me.TC_Import.Location = New System.Drawing.Point(12, 12)
        Me.TC_Import.Name = "TC_Import"
        Me.TC_Import.SelectedIndex = 0
        Me.TC_Import.Size = New System.Drawing.Size(1080, 603)
        Me.TC_Import.TabIndex = 0
        '
        'tp_New
        '
        Me.tp_New.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tp_New.Controls.Add(Me.TableLayoutPanel1)
        Me.tp_New.Location = New System.Drawing.Point(4, 23)
        Me.tp_New.Name = "tp_New"
        Me.tp_New.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_New.Size = New System.Drawing.Size(1072, 576)
        Me.tp_New.TabIndex = 0
        Me.tp_New.Text = "New Participants"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.DGridViewNew, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1066, 570)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'DGridViewNew
        '
        Me.DGridViewNew.AllowUserToAddRows = False
        Me.DGridViewNew.AllowUserToDeleteRows = False
        Me.DGridViewNew.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridViewNew.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGridViewNew.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGridViewNew.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGridViewNew.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridViewNew.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cbAdd, Me.IDNumber, Me.FullName, Me.Billing_Address, Me.Renewable, Me.ZeroRated, Me.IncomeTaxHoliday, Me.FacilityType, Me.ColRegion, Me.MembershipType})
        Me.DGridViewNew.Location = New System.Drawing.Point(3, 3)
        Me.DGridViewNew.MultiSelect = False
        Me.DGridViewNew.Name = "DGridViewNew"
        Me.DGridViewNew.ReadOnly = True
        Me.DGridViewNew.RowHeadersVisible = False
        Me.DGridViewNew.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGridViewNew.Size = New System.Drawing.Size(1060, 507)
        Me.DGridViewNew.TabIndex = 37
        '
        'cbAdd
        '
        Me.cbAdd.HeaderText = ""
        Me.cbAdd.Name = "cbAdd"
        Me.cbAdd.ReadOnly = True
        Me.cbAdd.Width = 25
        '
        'IDNumber
        '
        Me.IDNumber.HeaderText = "ID Number"
        Me.IDNumber.Name = "IDNumber"
        Me.IDNumber.ReadOnly = True
        '
        'FullName
        '
        Me.FullName.HeaderText = "Name"
        Me.FullName.Name = "FullName"
        Me.FullName.ReadOnly = True
        Me.FullName.Width = 200
        '
        'Billing_Address
        '
        Me.Billing_Address.HeaderText = "Billing Address"
        Me.Billing_Address.Name = "Billing_Address"
        Me.Billing_Address.ReadOnly = True
        Me.Billing_Address.Width = 200
        '
        'Renewable
        '
        Me.Renewable.HeaderText = "Renewable"
        Me.Renewable.Name = "Renewable"
        Me.Renewable.ReadOnly = True
        '
        'ZeroRated
        '
        Me.ZeroRated.HeaderText = "ZeroRated"
        Me.ZeroRated.Name = "ZeroRated"
        Me.ZeroRated.ReadOnly = True
        '
        'IncomeTaxHoliday
        '
        Me.IncomeTaxHoliday.HeaderText = "IncomeTaxHoliday"
        Me.IncomeTaxHoliday.Name = "IncomeTaxHoliday"
        Me.IncomeTaxHoliday.ReadOnly = True
        '
        'FacilityType
        '
        Me.FacilityType.HeaderText = "FacilityType"
        Me.FacilityType.Name = "FacilityType"
        Me.FacilityType.ReadOnly = True
        '
        'ColRegion
        '
        Me.ColRegion.HeaderText = "Region"
        Me.ColRegion.Name = "ColRegion"
        Me.ColRegion.ReadOnly = True
        '
        'MembershipType
        '
        Me.MembershipType.HeaderText = "Membership Type"
        Me.MembershipType.Name = "MembershipType"
        Me.MembershipType.ReadOnly = True
        Me.MembershipType.Width = 120
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel1.Controls.Add(Me.cb_SelectAll)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 516)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1060, 51)
        Me.Panel1.TabIndex = 38
        '
        'cb_SelectAll
        '
        Me.cb_SelectAll.AutoSize = True
        Me.cb_SelectAll.Location = New System.Drawing.Point(14, 6)
        Me.cb_SelectAll.Name = "cb_SelectAll"
        Me.cb_SelectAll.Size = New System.Drawing.Size(70, 18)
        Me.cb_SelectAll.TabIndex = 12
        Me.cb_SelectAll.Text = "Select All"
        Me.cb_SelectAll.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.White
        Me.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.PaleGoldenrod
        Me.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Image = Global.AccountsManagementForms.My.Resources.Resources.SaveIconColored22x22
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(753, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(170, 39)
        Me.btnSave.TabIndex = 11
        Me.btnSave.Text = "&Import To AMS"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.White
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.PaleGoldenrod
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.ForeColor = System.Drawing.Color.Black
        Me.btnCancel.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(931, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(121, 39)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'frmBillParticiopantsImportFromCRSSDB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1104, 628)
        Me.Controls.Add(Me.TC_Import)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBillParticiopantsImportFromCRSSDB"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import Participants From CRSS DB"
        Me.TC_Import.ResumeLayout(False)
        Me.tp_New.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.DGridViewNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TC_Import As System.Windows.Forms.TabControl
    Friend WithEvents tp_New As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents DGridViewNew As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents cb_SelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents cbAdd As DataGridViewCheckBoxColumn
    Friend WithEvents IDNumber As DataGridViewTextBoxColumn
    Friend WithEvents FullName As DataGridViewTextBoxColumn
    Friend WithEvents Billing_Address As DataGridViewTextBoxColumn
    Friend WithEvents Renewable As DataGridViewTextBoxColumn
    Friend WithEvents ZeroRated As DataGridViewTextBoxColumn
    Friend WithEvents IncomeTaxHoliday As DataGridViewTextBoxColumn
    Friend WithEvents FacilityType As DataGridViewTextBoxColumn
    Friend WithEvents ColRegion As DataGridViewTextBoxColumn
    Friend WithEvents MembershipType As DataGridViewTextBoxColumn
End Class
