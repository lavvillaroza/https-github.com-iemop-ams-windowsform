﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWHTaxCertificateSTLMgt
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
        Me.gbMenu2 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DGridViewCollection = New System.Windows.Forms.DataGridView()
        Me.colCertificateNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRemittanceDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColBillingIDNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCollectedAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAllocatedToAP = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colUntag = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.gbMenu1 = New System.Windows.Forms.GroupBox()
        Me.chkbox_Untagged = New System.Windows.Forms.CheckBox()
        Me.chkbox_Allocated = New System.Windows.Forms.CheckBox()
        Me.btnUntag = New System.Windows.Forms.Button()
        Me.btnAllocateToAP = New System.Windows.Forms.Button()
        Me.lblCollectionDate = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.btnView = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.chkbox_SelectAll = New System.Windows.Forms.CheckBox()
        Me.ctrl_statusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatus_LabelMsg = New System.Windows.Forms.ToolStripStatusLabel()
        Me.gbMenu2.SuspendLayout()
        CType(Me.DGridViewCollection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbMenu1.SuspendLayout()
        Me.ctrl_statusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbMenu2
        '
        Me.gbMenu2.Controls.Add(Me.Label3)
        Me.gbMenu2.Controls.Add(Me.DGridViewCollection)
        Me.gbMenu2.Location = New System.Drawing.Point(9, 119)
        Me.gbMenu2.Name = "gbMenu2"
        Me.gbMenu2.Size = New System.Drawing.Size(823, 315)
        Me.gbMenu2.TabIndex = 9
        Me.gbMenu2.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(12, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(132, 14)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Certificate Collections:"
        '
        'DGridViewCollection
        '
        Me.DGridViewCollection.AllowUserToAddRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridViewCollection.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DGridViewCollection.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGridViewCollection.BackgroundColor = System.Drawing.SystemColors.ButtonShadow
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.DimGray
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGridViewCollection.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DGridViewCollection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridViewCollection.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCertificateNo, Me.colRemittanceDate, Me.ColBillingIDNo, Me.colCollectedAmount, Me.colAllocatedToAP, Me.colUntag})
        Me.DGridViewCollection.Location = New System.Drawing.Point(9, 19)
        Me.DGridViewCollection.MultiSelect = False
        Me.DGridViewCollection.Name = "DGridViewCollection"
        Me.DGridViewCollection.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGridViewCollection.Size = New System.Drawing.Size(808, 290)
        Me.DGridViewCollection.TabIndex = 0
        '
        'colCertificateNo
        '
        Me.colCertificateNo.HeaderText = "Certificate No"
        Me.colCertificateNo.Name = "colCertificateNo"
        Me.colCertificateNo.ReadOnly = True
        Me.colCertificateNo.Width = 110
        '
        'colRemittanceDate
        '
        Me.colRemittanceDate.HeaderText = "Remittance Date"
        Me.colRemittanceDate.Name = "colRemittanceDate"
        Me.colRemittanceDate.ReadOnly = True
        Me.colRemittanceDate.Width = 130
        '
        'ColBillingIDNo
        '
        Me.ColBillingIDNo.HeaderText = "Billing ID No"
        Me.ColBillingIDNo.Name = "ColBillingIDNo"
        Me.ColBillingIDNo.ReadOnly = True
        Me.ColBillingIDNo.Width = 150
        '
        'colCollectedAmount
        '
        Me.colCollectedAmount.HeaderText = "Collected Amount"
        Me.colCollectedAmount.Name = "colCollectedAmount"
        Me.colCollectedAmount.ReadOnly = True
        Me.colCollectedAmount.Width = 150
        '
        'colAllocatedToAP
        '
        Me.colAllocatedToAP.HeaderText = "Allocated To AP"
        Me.colAllocatedToAP.Name = "colAllocatedToAP"
        Me.colAllocatedToAP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colAllocatedToAP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colAllocatedToAP.Width = 130
        '
        'colUntag
        '
        Me.colUntag.HeaderText = "Untag EWT"
        Me.colUntag.Name = "colUntag"
        Me.colUntag.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colUntag.Width = 80
        '
        'gbMenu1
        '
        Me.gbMenu1.Controls.Add(Me.chkbox_Untagged)
        Me.gbMenu1.Controls.Add(Me.chkbox_Allocated)
        Me.gbMenu1.Controls.Add(Me.btnUntag)
        Me.gbMenu1.Controls.Add(Me.btnAllocateToAP)
        Me.gbMenu1.Controls.Add(Me.lblCollectionDate)
        Me.gbMenu1.Controls.Add(Me.Label6)
        Me.gbMenu1.Controls.Add(Me.dtFrom)
        Me.gbMenu1.Controls.Add(Me.dtTo)
        Me.gbMenu1.Controls.Add(Me.btnView)
        Me.gbMenu1.Controls.Add(Me.btnSearch)
        Me.gbMenu1.Controls.Add(Me.btnAdd)
        Me.gbMenu1.Location = New System.Drawing.Point(9, 12)
        Me.gbMenu1.Name = "gbMenu1"
        Me.gbMenu1.Size = New System.Drawing.Size(823, 85)
        Me.gbMenu1.TabIndex = 29
        Me.gbMenu1.TabStop = False
        '
        'chkbox_Untagged
        '
        Me.chkbox_Untagged.AutoSize = True
        Me.chkbox_Untagged.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbox_Untagged.ForeColor = System.Drawing.Color.Black
        Me.chkbox_Untagged.Location = New System.Drawing.Point(86, 57)
        Me.chkbox_Untagged.Name = "chkbox_Untagged"
        Me.chkbox_Untagged.Size = New System.Drawing.Size(72, 18)
        Me.chkbox_Untagged.TabIndex = 54
        Me.chkbox_Untagged.Text = "Untagged"
        Me.chkbox_Untagged.UseVisualStyleBackColor = True
        '
        'chkbox_Allocated
        '
        Me.chkbox_Allocated.AutoSize = True
        Me.chkbox_Allocated.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbox_Allocated.ForeColor = System.Drawing.Color.Black
        Me.chkbox_Allocated.Location = New System.Drawing.Point(9, 57)
        Me.chkbox_Allocated.Name = "chkbox_Allocated"
        Me.chkbox_Allocated.Size = New System.Drawing.Size(71, 18)
        Me.chkbox_Allocated.TabIndex = 53
        Me.chkbox_Allocated.Text = "Allocated"
        Me.chkbox_Allocated.UseVisualStyleBackColor = True
        '
        'btnUntag
        '
        Me.btnUntag.BackColor = System.Drawing.Color.White
        Me.btnUntag.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnUntag.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnUntag.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnUntag.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUntag.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUntag.ForeColor = System.Drawing.Color.Black
        Me.btnUntag.Image = Global.AccountsManagementForms.My.Resources.Resources.TrashCanRedIcon22x22
        Me.btnUntag.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUntag.Location = New System.Drawing.Point(727, 19)
        Me.btnUntag.Name = "btnUntag"
        Me.btnUntag.Size = New System.Drawing.Size(90, 39)
        Me.btnUntag.TabIndex = 52
        Me.btnUntag.Text = "&Untag"
        Me.btnUntag.UseVisualStyleBackColor = False
        '
        'btnAllocateToAP
        '
        Me.btnAllocateToAP.BackColor = System.Drawing.Color.White
        Me.btnAllocateToAP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnAllocateToAP.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnAllocateToAP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnAllocateToAP.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnAllocateToAP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAllocateToAP.ForeColor = System.Drawing.Color.Black
        Me.btnAllocateToAP.Image = Global.AccountsManagementForms.My.Resources.Resources.ArrowRight
        Me.btnAllocateToAP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAllocateToAP.Location = New System.Drawing.Point(597, 19)
        Me.btnAllocateToAP.Name = "btnAllocateToAP"
        Me.btnAllocateToAP.Size = New System.Drawing.Size(124, 39)
        Me.btnAllocateToAP.TabIndex = 51
        Me.btnAllocateToAP.Text = "  Allocate &To AP"
        Me.btnAllocateToAP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnAllocateToAP.UseVisualStyleBackColor = False
        '
        'lblCollectionDate
        '
        Me.lblCollectionDate.AutoSize = True
        Me.lblCollectionDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCollectionDate.Location = New System.Drawing.Point(6, 12)
        Me.lblCollectionDate.Name = "lblCollectionDate"
        Me.lblCollectionDate.Size = New System.Drawing.Size(99, 14)
        Me.lblCollectionDate.TabIndex = 49
        Me.lblCollectionDate.Text = "Remittance Date:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(119, 34)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(21, 14)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "TO"
        '
        'dtFrom
        '
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFrom.Location = New System.Drawing.Point(9, 31)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(104, 20)
        Me.dtFrom.TabIndex = 19
        '
        'dtTo
        '
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTo.Location = New System.Drawing.Point(146, 31)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(104, 20)
        Me.dtTo.TabIndex = 20
        '
        'btnView
        '
        Me.btnView.BackColor = System.Drawing.Color.White
        Me.btnView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnView.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnView.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnView.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnView.ForeColor = System.Drawing.Color.Black
        Me.btnView.Image = Global.AccountsManagementForms.My.Resources.Resources.SearchIcon22x22
        Me.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnView.Location = New System.Drawing.Point(501, 19)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(90, 39)
        Me.btnView.TabIndex = 8
        Me.btnView.Text = "  &View"
        Me.btnView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnView.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.White
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.ForeColor = System.Drawing.Color.Black
        Me.btnSearch.Image = Global.AccountsManagementForms.My.Resources.Resources.SearchIconColored22x22
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(256, 26)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(35, 29)
        Me.btnSearch.TabIndex = 7
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.White
        Me.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.ForeColor = System.Drawing.Color.Black
        Me.btnAdd.Image = Global.AccountsManagementForms.My.Resources.Resources.NewGreenIcon22x22
        Me.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAdd.Location = New System.Drawing.Point(405, 19)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(90, 39)
        Me.btnAdd.TabIndex = 1
        Me.btnAdd.Text = "&Add"
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'chkbox_SelectAll
        '
        Me.chkbox_SelectAll.AutoSize = True
        Me.chkbox_SelectAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbox_SelectAll.ForeColor = System.Drawing.Color.Black
        Me.chkbox_SelectAll.Location = New System.Drawing.Point(762, 102)
        Me.chkbox_SelectAll.Name = "chkbox_SelectAll"
        Me.chkbox_SelectAll.Size = New System.Drawing.Size(70, 18)
        Me.chkbox_SelectAll.TabIndex = 30
        Me.chkbox_SelectAll.Text = "Select All"
        Me.chkbox_SelectAll.UseVisualStyleBackColor = True
        '
        'ctrl_statusStrip
        '
        Me.ctrl_statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatus_LabelMsg})
        Me.ctrl_statusStrip.Location = New System.Drawing.Point(0, 441)
        Me.ctrl_statusStrip.Name = "ctrl_statusStrip"
        Me.ctrl_statusStrip.Size = New System.Drawing.Size(842, 22)
        Me.ctrl_statusStrip.TabIndex = 59
        Me.ctrl_statusStrip.Text = "StatusStrip1"
        '
        'ToolStripStatus_LabelMsg
        '
        Me.ToolStripStatus_LabelMsg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripStatus_LabelMsg.Name = "ToolStripStatus_LabelMsg"
        Me.ToolStripStatus_LabelMsg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ToolStripStatus_LabelMsg.Size = New System.Drawing.Size(39, 17)
        Me.ToolStripStatus_LabelMsg.Text = "Ready"
        Me.ToolStripStatus_LabelMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripStatus_LabelMsg.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'frmWHTaxCertificateSTLMgt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(842, 463)
        Me.Controls.Add(Me.ctrl_statusStrip)
        Me.Controls.Add(Me.chkbox_SelectAll)
        Me.Controls.Add(Me.gbMenu1)
        Me.Controls.Add(Me.gbMenu2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmWHTaxCertificateSTLMgt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Withholding Tax Certificate Settlement"
        Me.gbMenu2.ResumeLayout(False)
        Me.gbMenu2.PerformLayout()
        CType(Me.DGridViewCollection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbMenu1.ResumeLayout(False)
        Me.gbMenu1.PerformLayout()
        Me.ctrl_statusStrip.ResumeLayout(False)
        Me.ctrl_statusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents gbMenu2 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents DGridViewCollection As DataGridView
    Friend WithEvents gbMenu1 As GroupBox
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnView As Button
    Friend WithEvents dtFrom As DateTimePicker
    Friend WithEvents dtTo As DateTimePicker
    Friend WithEvents Label6 As Label
    Friend WithEvents lblCollectionDate As Label
    Friend WithEvents btnAllocateToAP As Button
    Friend WithEvents chkbox_SelectAll As CheckBox
    Friend WithEvents colCertificateNo As DataGridViewTextBoxColumn
    Friend WithEvents colRemittanceDate As DataGridViewTextBoxColumn
    Friend WithEvents ColBillingIDNo As DataGridViewTextBoxColumn
    Friend WithEvents colCollectedAmount As DataGridViewTextBoxColumn
    Friend WithEvents colAllocatedToAP As DataGridViewCheckBoxColumn
    Friend WithEvents colUntag As DataGridViewCheckBoxColumn
    Friend WithEvents btnUntag As Button
    Friend WithEvents chkbox_Untagged As CheckBox
    Friend WithEvents chkbox_Allocated As CheckBox
    Friend WithEvents ctrl_statusStrip As StatusStrip
    Friend WithEvents ToolStripStatus_LabelMsg As ToolStripStatusLabel
End Class