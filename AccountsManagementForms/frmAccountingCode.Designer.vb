﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAccountingCode
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
        Me.GridView = New System.Windows.Forms.DataGridView()
        Me.ac_Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ac_desc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ac_Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ac_DateCommitted = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ac_updBy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.txtFormStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btn_TSearch = New System.Windows.Forms.Button()
        Me.btn_New = New System.Windows.Forms.Button()
        Me.btn_Edit = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btn_Del = New System.Windows.Forms.Button()
        Me.btn_refresh = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmd_close = New System.Windows.Forms.Button()
        CType(Me.GridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridView
        '
        Me.GridView.AllowUserToAddRows = False
        Me.GridView.AllowUserToDeleteRows = False
        Me.GridView.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ac_Code, Me.ac_desc, Me.ac_Status, Me.ac_DateCommitted, Me.ac_updBy})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GridView.DefaultCellStyle = DataGridViewCellStyle3
        Me.GridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.GridView.Location = New System.Drawing.Point(9, 59)
        Me.GridView.MultiSelect = False
        Me.GridView.Name = "GridView"
        Me.GridView.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.GridView.RowHeadersVisible = False
        Me.GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridView.Size = New System.Drawing.Size(636, 317)
        Me.GridView.TabIndex = 7
        '
        'ac_Code
        '
        Me.ac_Code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.ac_Code.HeaderText = "AccountCode"
        Me.ac_Code.Name = "ac_Code"
        Me.ac_Code.ReadOnly = True
        Me.ac_Code.Width = 105
        '
        'ac_desc
        '
        Me.ac_desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.ac_desc.FillWeight = 80.0!
        Me.ac_desc.HeaderText = "Description"
        Me.ac_desc.Name = "ac_desc"
        Me.ac_desc.ReadOnly = True
        Me.ac_desc.Width = 95
        '
        'ac_Status
        '
        Me.ac_Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.ac_Status.HeaderText = "Status"
        Me.ac_Status.Name = "ac_Status"
        Me.ac_Status.ReadOnly = True
        Me.ac_Status.Width = 67
        '
        'ac_DateCommitted
        '
        Me.ac_DateCommitted.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.ac_DateCommitted.HeaderText = "DateCommitted"
        Me.ac_DateCommitted.Name = "ac_DateCommitted"
        Me.ac_DateCommitted.ReadOnly = True
        Me.ac_DateCommitted.Width = 119
        '
        'ac_updBy
        '
        Me.ac_updBy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.ac_updBy.HeaderText = "UpdatedBy"
        Me.ac_updBy.Name = "ac_updBy"
        Me.ac_updBy.ReadOnly = True
        Me.ac_updBy.Width = 92
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.txtFormStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 429)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(657, 22)
        Me.StatusStrip1.TabIndex = 8
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(42, 17)
        Me.ToolStripStatusLabel1.Text = "Status:"
        Me.ToolStripStatusLabel1.Visible = False
        '
        'txtFormStatus
        '
        Me.txtFormStatus.Name = "txtFormStatus"
        Me.txtFormStatus.Size = New System.Drawing.Size(67, 17)
        Me.txtFormStatus.Text = "StatusValue"
        Me.txtFormStatus.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.btn_TSearch)
        Me.GroupBox2.Controls.Add(Me.btn_New)
        Me.GroupBox2.Controls.Add(Me.btn_Edit)
        Me.GroupBox2.Controls.Add(Me.txtSearch)
        Me.GroupBox2.Controls.Add(Me.btn_Del)
        Me.GroupBox2.Controls.Add(Me.btn_refresh)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(636, 53)
        Me.GroupBox2.TabIndex = 35
        Me.GroupBox2.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 14)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Accounting Code:"
        '
        'btn_TSearch
        '
        Me.btn_TSearch.BackColor = System.Drawing.Color.White
        Me.btn_TSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btn_TSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_TSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_TSearch.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_TSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_TSearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_TSearch.Image = Global.AccountsManagementForms.My.Resources.Resources.SearchIconColored22x22
        Me.btn_TSearch.Location = New System.Drawing.Point(252, 12)
        Me.btn_TSearch.Name = "btn_TSearch"
        Me.btn_TSearch.Size = New System.Drawing.Size(40, 30)
        Me.btn_TSearch.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.btn_TSearch, "Search Records")
        Me.btn_TSearch.UseVisualStyleBackColor = False
        '
        'btn_New
        '
        Me.btn_New.BackColor = System.Drawing.Color.White
        Me.btn_New.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_New.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_New.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_New.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_New.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_New.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_New.Image = Global.AccountsManagementForms.My.Resources.Resources.NewGreenIcon22x22
        Me.btn_New.Location = New System.Drawing.Point(298, 12)
        Me.btn_New.Name = "btn_New"
        Me.btn_New.Size = New System.Drawing.Size(40, 30)
        Me.btn_New.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.btn_New, "Add New Record")
        Me.btn_New.UseVisualStyleBackColor = False
        '
        'btn_Edit
        '
        Me.btn_Edit.BackColor = System.Drawing.Color.White
        Me.btn_Edit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btn_Edit.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Edit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Edit.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Edit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Edit.Image = Global.AccountsManagementForms.My.Resources.Resources.EditDocumentColored22x22
        Me.btn_Edit.Location = New System.Drawing.Point(344, 12)
        Me.btn_Edit.Name = "btn_Edit"
        Me.btn_Edit.Size = New System.Drawing.Size(40, 30)
        Me.btn_Edit.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.btn_Edit, "Edit Record")
        Me.btn_Edit.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(116, 16)
        Me.txtSearch.MaxLength = 20
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(126, 22)
        Me.txtSearch.TabIndex = 1
        '
        'btn_Del
        '
        Me.btn_Del.BackColor = System.Drawing.Color.White
        Me.btn_Del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_Del.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Del.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Del.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Del.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Del.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Del.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseRedIcon22x22
        Me.btn_Del.Location = New System.Drawing.Point(390, 12)
        Me.btn_Del.Name = "btn_Del"
        Me.btn_Del.Size = New System.Drawing.Size(40, 30)
        Me.btn_Del.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.btn_Del, "Delete Record")
        Me.btn_Del.UseVisualStyleBackColor = False
        '
        'btn_refresh
        '
        Me.btn_refresh.BackColor = System.Drawing.Color.White
        Me.btn_refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btn_refresh.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_refresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_refresh.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_refresh.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_refresh.Image = Global.AccountsManagementForms.My.Resources.Resources.RefreshGreenIcon22x22
        Me.btn_refresh.Location = New System.Drawing.Point(436, 12)
        Me.btn_refresh.Name = "btn_refresh"
        Me.btn_refresh.Size = New System.Drawing.Size(40, 30)
        Me.btn_refresh.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.btn_refresh, "Refresh")
        Me.btn_refresh.UseVisualStyleBackColor = False
        '
        'cmd_close
        '
        Me.cmd_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_close.BackColor = System.Drawing.Color.White
        Me.cmd_close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_close.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_close.ForeColor = System.Drawing.Color.Black
        Me.cmd_close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_close.Location = New System.Drawing.Point(524, 382)
        Me.cmd_close.Name = "cmd_close"
        Me.cmd_close.Size = New System.Drawing.Size(121, 39)
        Me.cmd_close.TabIndex = 8
        Me.cmd_close.Text = "Close"
        Me.cmd_close.UseVisualStyleBackColor = False
        '
        'frmAccountingCode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(657, 451)
        Me.Controls.Add(Me.cmd_close)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GridView)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(542, 454)
        Me.Name = "frmAccountingCode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Accounting Code"
        CType(Me.GridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GridView As System.Windows.Forms.DataGridView
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtFormStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmd_close As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btn_TSearch As System.Windows.Forms.Button
    Friend WithEvents btn_New As System.Windows.Forms.Button
    Friend WithEvents btn_Edit As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btn_Del As System.Windows.Forms.Button
    Friend WithEvents btn_refresh As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ac_Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ac_desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ac_Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ac_DateCommitted As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ac_updBy As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
