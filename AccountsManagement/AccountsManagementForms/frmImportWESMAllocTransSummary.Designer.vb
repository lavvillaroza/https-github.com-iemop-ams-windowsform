<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportWESMAllocTransSummary
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtDirectory = New System.Windows.Forms.TextBox()
        Me.btnOpenFile = New System.Windows.Forms.Button()
        Me.dtTransDate = New System.Windows.Forms.DateTimePicker()
        Me.dtDueDate = New System.Windows.Forms.DateTimePicker()
        Me.lblCollectionDate = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_BillingRemarks = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.panel_ImportBIRRTransSummary = New System.Windows.Forms.Panel()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.ctrl_statusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatus_LabelMsg = New System.Windows.Forms.ToolStripStatusLabel()
        Me.GroupBox1.SuspendLayout()
        Me.panel_ImportBIRRTransSummary.SuspendLayout()
        Me.ctrl_statusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtDirectory)
        Me.GroupBox1.Controls.Add(Me.btnOpenFile)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(431, 56)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Choose Filename:"
        '
        'txtDirectory
        '
        Me.txtDirectory.BackColor = System.Drawing.Color.White
        Me.txtDirectory.Location = New System.Drawing.Point(6, 22)
        Me.txtDirectory.Name = "txtDirectory"
        Me.txtDirectory.ReadOnly = True
        Me.txtDirectory.Size = New System.Drawing.Size(373, 20)
        Me.txtDirectory.TabIndex = 2
        '
        'btnOpenFile
        '
        Me.btnOpenFile.BackColor = System.Drawing.Color.White
        Me.btnOpenFile.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnOpenFile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnOpenFile.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenFile.Image = Global.AccountsManagementForms.My.Resources.Resources.FileSearchIcon22x22
        Me.btnOpenFile.Location = New System.Drawing.Point(385, 17)
        Me.btnOpenFile.Name = "btnOpenFile"
        Me.btnOpenFile.Size = New System.Drawing.Size(35, 30)
        Me.btnOpenFile.TabIndex = 3
        Me.btnOpenFile.UseVisualStyleBackColor = False
        '
        'dtTransDate
        '
        Me.dtTransDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTransDate.Location = New System.Drawing.Point(111, 64)
        Me.dtTransDate.Name = "dtTransDate"
        Me.dtTransDate.Size = New System.Drawing.Size(104, 20)
        Me.dtTransDate.TabIndex = 21
        '
        'dtDueDate
        '
        Me.dtDueDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtDueDate.Location = New System.Drawing.Point(298, 64)
        Me.dtDueDate.Name = "dtDueDate"
        Me.dtDueDate.Size = New System.Drawing.Size(104, 20)
        Me.dtDueDate.TabIndex = 22
        '
        'lblCollectionDate
        '
        Me.lblCollectionDate.AutoSize = True
        Me.lblCollectionDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCollectionDate.Location = New System.Drawing.Point(4, 67)
        Me.lblCollectionDate.Name = "lblCollectionDate"
        Me.lblCollectionDate.Size = New System.Drawing.Size(101, 14)
        Me.lblCollectionDate.TabIndex = 50
        Me.lblCollectionDate.Text = "Transaction Date:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(234, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 14)
        Me.Label1.TabIndex = 51
        Me.Label1.Text = "Due Date:"
        '
        'txt_BillingRemarks
        '
        Me.txt_BillingRemarks.BackColor = System.Drawing.Color.White
        Me.txt_BillingRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_BillingRemarks.Location = New System.Drawing.Point(3, 110)
        Me.txt_BillingRemarks.Multiline = True
        Me.txt_BillingRemarks.Name = "txt_BillingRemarks"
        Me.txt_BillingRemarks.Size = New System.Drawing.Size(289, 70)
        Me.txt_BillingRemarks.TabIndex = 52
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 93)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 14)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "Billing Remarks:"
        '
        'panel_ImportBIRRTransSummary
        '
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.GroupBox1)
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.btnImport)
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.btnClose)
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.dtDueDate)
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.Label2)
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.dtTransDate)
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.txt_BillingRemarks)
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.lblCollectionDate)
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.Label1)
        Me.panel_ImportBIRRTransSummary.Location = New System.Drawing.Point(12, 12)
        Me.panel_ImportBIRRTransSummary.Name = "panel_ImportBIRRTransSummary"
        Me.panel_ImportBIRRTransSummary.Size = New System.Drawing.Size(440, 195)
        Me.panel_ImportBIRRTransSummary.TabIndex = 55
        '
        'btnImport
        '
        Me.btnImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImport.BackColor = System.Drawing.Color.White
        Me.btnImport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnImport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnImport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImport.ForeColor = System.Drawing.Color.Black
        Me.btnImport.Image = Global.AccountsManagementForms.My.Resources.Resources.Upload2Icon22x22
        Me.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImport.Location = New System.Drawing.Point(310, 100)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(121, 39)
        Me.btnImport.TabIndex = 10
        Me.btnImport.Text = "   &Upload File"
        Me.btnImport.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.White
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(310, 147)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(121, 39)
        Me.btnClose.TabIndex = 54
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'ctrl_statusStrip
        '
        Me.ctrl_statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatus_LabelMsg})
        Me.ctrl_statusStrip.Location = New System.Drawing.Point(0, 225)
        Me.ctrl_statusStrip.Name = "ctrl_statusStrip"
        Me.ctrl_statusStrip.Size = New System.Drawing.Size(465, 22)
        Me.ctrl_statusStrip.TabIndex = 56
        Me.ctrl_statusStrip.Text = "StatusStrip1"
        '
        'ToolStripStatus_LabelMsg
        '
        Me.ToolStripStatus_LabelMsg.Name = "ToolStripStatus_LabelMsg"
        Me.ToolStripStatus_LabelMsg.Size = New System.Drawing.Size(48, 17)
        Me.ToolStripStatus_LabelMsg.Text = "Ready..."
        '
        'frmImportWESMAllocTransSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(465, 247)
        Me.Controls.Add(Me.ctrl_statusStrip)
        Me.Controls.Add(Me.panel_ImportBIRRTransSummary)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmImportWESMAllocTransSummary"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import BIR Ruling Transaction Summary"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.panel_ImportBIRRTransSummary.ResumeLayout(False)
        Me.panel_ImportBIRRTransSummary.PerformLayout()
        Me.ctrl_statusStrip.ResumeLayout(False)
        Me.ctrl_statusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtDirectory As TextBox
    Friend WithEvents btnOpenFile As Button
    Friend WithEvents btnImport As Button
    Friend WithEvents dtTransDate As DateTimePicker
    Friend WithEvents dtDueDate As DateTimePicker
    Friend WithEvents lblCollectionDate As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_BillingRemarks As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnClose As Button
    Friend WithEvents panel_ImportBIRRTransSummary As Panel
    Friend WithEvents ctrl_statusStrip As StatusStrip
    Friend WithEvents ToolStripStatus_LabelMsg As ToolStripStatusLabel
End Class
