<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStatementOfAccount
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.cmd_GenerateReport = New System.Windows.Forms.Button()
        Me.Close_Button = New System.Windows.Forms.Button()
        Me.bw = New System.ComponentModel.BackgroundWorker()
        Me.DueDate_ComboBox = New System.Windows.Forms.ComboBox()
        Me.dgv_SOA = New System.Windows.Forms.DataGridView()
        Me.Preview_Button = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.GB_Allocation = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Save_Button = New System.Windows.Forms.Button()
        CType(Me.dgv_SOA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.GB_Allocation.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmd_GenerateReport
        '
        Me.cmd_GenerateReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_GenerateReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_GenerateReport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_GenerateReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_GenerateReport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_GenerateReport.ForeColor = System.Drawing.Color.Black
        Me.cmd_GenerateReport.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.cmd_GenerateReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_GenerateReport.Location = New System.Drawing.Point(3, 6)
        Me.cmd_GenerateReport.Name = "cmd_GenerateReport"
        Me.cmd_GenerateReport.Size = New System.Drawing.Size(106, 39)
        Me.cmd_GenerateReport.TabIndex = 35
        Me.cmd_GenerateReport.Text = "   Generate"
        Me.cmd_GenerateReport.UseVisualStyleBackColor = True
        '
        'Close_Button
        '
        Me.Close_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Close_Button.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.Close_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.Close_Button.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.Close_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Close_Button.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Close_Button.ForeColor = System.Drawing.Color.Black
        Me.Close_Button.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.Close_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Close_Button.Location = New System.Drawing.Point(495, 6)
        Me.Close_Button.Name = "Close_Button"
        Me.Close_Button.Size = New System.Drawing.Size(106, 39)
        Me.Close_Button.TabIndex = 36
        Me.Close_Button.Text = "Close"
        Me.Close_Button.UseVisualStyleBackColor = True
        '
        'DueDate_ComboBox
        '
        Me.DueDate_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DueDate_ComboBox.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.DueDate_ComboBox.FormattingEnabled = True
        Me.DueDate_ComboBox.Location = New System.Drawing.Point(6, 20)
        Me.DueDate_ComboBox.Name = "DueDate_ComboBox"
        Me.DueDate_ComboBox.Size = New System.Drawing.Size(176, 22)
        Me.DueDate_ComboBox.TabIndex = 44
        '
        'dgv_SOA
        '
        Me.dgv_SOA.AllowUserToAddRows = False
        Me.dgv_SOA.AllowUserToDeleteRows = False
        Me.dgv_SOA.AllowUserToResizeColumns = False
        Me.dgv_SOA.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_SOA.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_SOA.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_SOA.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_SOA.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_SOA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LemonChiffon
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_SOA.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_SOA.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv_SOA.Location = New System.Drawing.Point(3, 63)
        Me.dgv_SOA.Name = "dgv_SOA"
        Me.dgv_SOA.RowHeadersVisible = False
        Me.dgv_SOA.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgv_SOA.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_SOA.Size = New System.Drawing.Size(810, 475)
        Me.dgv_SOA.TabIndex = 45
        '
        'Preview_Button
        '
        Me.Preview_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Preview_Button.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.Preview_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.Preview_Button.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.Preview_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Preview_Button.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Preview_Button.ForeColor = System.Drawing.Color.Black
        Me.Preview_Button.Image = Global.AccountsManagementForms.My.Resources.Resources.PDFIcon22x22
        Me.Preview_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Preview_Button.Location = New System.Drawing.Point(115, 6)
        Me.Preview_Button.Name = "Preview_Button"
        Me.Preview_Button.Size = New System.Drawing.Size(106, 39)
        Me.Preview_Button.TabIndex = 46
        Me.Preview_Button.Text = "   Preview"
        Me.Preview_Button.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.dgv_SOA, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 12)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(816, 541)
        Me.TableLayoutPanel1.TabIndex = 47
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.GB_Allocation, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Panel1, 1, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(810, 54)
        Me.TableLayoutPanel2.TabIndex = 46
        '
        'GB_Allocation
        '
        Me.GB_Allocation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GB_Allocation.Controls.Add(Me.DueDate_ComboBox)
        Me.GB_Allocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GB_Allocation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GB_Allocation.Location = New System.Drawing.Point(3, 3)
        Me.GB_Allocation.Name = "GB_Allocation"
        Me.GB_Allocation.Size = New System.Drawing.Size(194, 48)
        Me.GB_Allocation.TabIndex = 52
        Me.GB_Allocation.TabStop = False
        Me.GB_Allocation.Text = "Select Due Date:"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Close_Button)
        Me.Panel1.Controls.Add(Me.cmd_GenerateReport)
        Me.Panel1.Controls.Add(Me.Save_Button)
        Me.Panel1.Controls.Add(Me.Preview_Button)
        Me.Panel1.Location = New System.Drawing.Point(203, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(604, 48)
        Me.Panel1.TabIndex = 0
        '
        'Save_Button
        '
        Me.Save_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Save_Button.BackColor = System.Drawing.Color.White
        Me.Save_Button.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.Save_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.Save_Button.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.Save_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Save_Button.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Save_Button.ForeColor = System.Drawing.Color.Black
        Me.Save_Button.Image = Global.AccountsManagementForms.My.Resources.Resources.SaveIconColored22x22
        Me.Save_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Save_Button.Location = New System.Drawing.Point(227, 6)
        Me.Save_Button.Name = "Save_Button"
        Me.Save_Button.Size = New System.Drawing.Size(106, 39)
        Me.Save_Button.TabIndex = 57
        Me.Save_Button.Text = "Save"
        Me.Save_Button.UseVisualStyleBackColor = False
        '
        'frmStatementOfAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(840, 565)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmStatementOfAccount"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Statement Of Account"
        CType(Me.dgv_SOA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.GB_Allocation.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmd_GenerateReport As System.Windows.Forms.Button
    Friend WithEvents Close_Button As System.Windows.Forms.Button
    Friend WithEvents bw As System.ComponentModel.BackgroundWorker
    Friend WithEvents DueDate_ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents dgv_SOA As System.Windows.Forms.DataGridView
    Friend WithEvents Preview_Button As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GB_Allocation As System.Windows.Forms.GroupBox
    Friend WithEvents Save_Button As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
