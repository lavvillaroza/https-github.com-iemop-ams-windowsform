<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewDetails
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
        Me.dgridView = New System.Windows.Forms.DataGridView()
        Me.gb_totalColl = New System.Windows.Forms.GroupBox()
        Me.txt_tDICollection = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_tVATCollection = New System.Windows.Forms.TextBox()
        Me.txt_tEnergyCollection = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmd_ExportToExcel = New System.Windows.Forms.Button()
        Me.cmd_ToCSV = New System.Windows.Forms.Button()
        Me.cmd_viewDetails = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        CType(Me.dgridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gb_totalColl.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgridView
        '
        Me.dgridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgridView.Location = New System.Drawing.Point(9, 11)
        Me.dgridView.Name = "dgridView"
        Me.dgridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgridView.Size = New System.Drawing.Size(691, 478)
        Me.dgridView.TabIndex = 0
        '
        'gb_totalColl
        '
        Me.gb_totalColl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gb_totalColl.Controls.Add(Me.txt_tDICollection)
        Me.gb_totalColl.Controls.Add(Me.Label1)
        Me.gb_totalColl.Controls.Add(Me.txt_tVATCollection)
        Me.gb_totalColl.Controls.Add(Me.txt_tEnergyCollection)
        Me.gb_totalColl.Controls.Add(Me.Label7)
        Me.gb_totalColl.Controls.Add(Me.Label6)
        Me.gb_totalColl.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb_totalColl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.gb_totalColl.Location = New System.Drawing.Point(9, 495)
        Me.gb_totalColl.Name = "gb_totalColl"
        Me.gb_totalColl.Size = New System.Drawing.Size(703, 109)
        Me.gb_totalColl.TabIndex = 26
        Me.gb_totalColl.TabStop = False
        Me.gb_totalColl.Text = "Total Collection:"
        '
        'txt_tDICollection
        '
        Me.txt_tDICollection.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_tDICollection.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_tDICollection.ForeColor = System.Drawing.Color.Black
        Me.txt_tDICollection.Location = New System.Drawing.Point(380, 72)
        Me.txt_tDICollection.Name = "txt_tDICollection"
        Me.txt_tDICollection.ReadOnly = True
        Me.txt_tDICollection.Size = New System.Drawing.Size(274, 22)
        Me.txt_tDICollection.TabIndex = 28
        Me.txt_tDICollection.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(35, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(182, 14)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Total Default Interest Collection:"
        '
        'txt_tVATCollection
        '
        Me.txt_tVATCollection.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_tVATCollection.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_tVATCollection.ForeColor = System.Drawing.Color.Black
        Me.txt_tVATCollection.Location = New System.Drawing.Point(380, 43)
        Me.txt_tVATCollection.Name = "txt_tVATCollection"
        Me.txt_tVATCollection.ReadOnly = True
        Me.txt_tVATCollection.Size = New System.Drawing.Size(274, 22)
        Me.txt_tVATCollection.TabIndex = 26
        Me.txt_tVATCollection.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_tEnergyCollection
        '
        Me.txt_tEnergyCollection.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_tEnergyCollection.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_tEnergyCollection.ForeColor = System.Drawing.Color.Black
        Me.txt_tEnergyCollection.Location = New System.Drawing.Point(380, 14)
        Me.txt_tEnergyCollection.Name = "txt_tEnergyCollection"
        Me.txt_tEnergyCollection.ReadOnly = True
        Me.txt_tEnergyCollection.Size = New System.Drawing.Size(274, 22)
        Me.txt_tEnergyCollection.TabIndex = 25
        Me.txt_tEnergyCollection.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(94, 47)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(118, 14)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Total VAT Collection:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(79, 18)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(135, 14)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Total Energy Collection:"
        '
        'cmd_ExportToExcel
        '
        Me.cmd_ExportToExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_ExportToExcel.BackColor = System.Drawing.Color.White
        Me.cmd_ExportToExcel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_ExportToExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_ExportToExcel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_ExportToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_ExportToExcel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ExportToExcel.ForeColor = System.Drawing.Color.Black
        Me.cmd_ExportToExcel.Image = Global.AccountsManagementForms.My.Resources.Resources.ExcelIcon22x22
        Me.cmd_ExportToExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_ExportToExcel.Location = New System.Drawing.Point(335, 610)
        Me.cmd_ExportToExcel.Name = "cmd_ExportToExcel"
        Me.cmd_ExportToExcel.Size = New System.Drawing.Size(204, 39)
        Me.cmd_ExportToExcel.TabIndex = 29
        Me.cmd_ExportToExcel.Text = "Export to Excel"
        Me.cmd_ExportToExcel.UseVisualStyleBackColor = False
        Me.cmd_ExportToExcel.Visible = False
        '
        'cmd_ToCSV
        '
        Me.cmd_ToCSV.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_ToCSV.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_ToCSV.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_ToCSV.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_ToCSV.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_ToCSV.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ToCSV.ForeColor = System.Drawing.Color.Black
        Me.cmd_ToCSV.Image = Global.AccountsManagementForms.My.Resources.Resources.execute
        Me.cmd_ToCSV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_ToCSV.Location = New System.Drawing.Point(335, 610)
        Me.cmd_ToCSV.Name = "cmd_ToCSV"
        Me.cmd_ToCSV.Size = New System.Drawing.Size(204, 29)
        Me.cmd_ToCSV.TabIndex = 28
        Me.cmd_ToCSV.Text = "Download CSV"
        Me.cmd_ToCSV.UseVisualStyleBackColor = True
        Me.cmd_ToCSV.Visible = False
        '
        'cmd_viewDetails
        '
        Me.cmd_viewDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_viewDetails.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_viewDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_viewDetails.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_viewDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_viewDetails.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_viewDetails.ForeColor = System.Drawing.Color.Black
        Me.cmd_viewDetails.Image = Global.AccountsManagementForms.My.Resources.Resources.close
        Me.cmd_viewDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_viewDetails.Location = New System.Drawing.Point(335, 610)
        Me.cmd_viewDetails.Name = "cmd_viewDetails"
        Me.cmd_viewDetails.Size = New System.Drawing.Size(204, 29)
        Me.cmd_viewDetails.TabIndex = 27
        Me.cmd_viewDetails.Text = "View Distributed Amount"
        Me.cmd_viewDetails.UseVisualStyleBackColor = True
        Me.cmd_viewDetails.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.White
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(545, 610)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(155, 39)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'frmViewDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(711, 661)
        Me.Controls.Add(Me.cmd_ExportToExcel)
        Me.Controls.Add(Me.cmd_ToCSV)
        Me.Controls.Add(Me.cmd_viewDetails)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.dgridView)
        Me.Controls.Add(Me.gb_totalColl)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmViewDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.dgridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gb_totalColl.ResumeLayout(False)
        Me.gb_totalColl.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgridView As System.Windows.Forms.DataGridView
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents gb_totalColl As System.Windows.Forms.GroupBox
    Friend WithEvents txt_tVATCollection As System.Windows.Forms.TextBox
    Friend WithEvents txt_tEnergyCollection As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_tDICollection As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmd_viewDetails As System.Windows.Forms.Button
    Friend WithEvents cmd_ToCSV As System.Windows.Forms.Button
    Friend WithEvents cmd_ExportToExcel As System.Windows.Forms.Button
End Class
