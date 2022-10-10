<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportWESMAllocTransSummaryPrelim
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
        Me.ctrl_statusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatus_LabelMsg = New System.Windows.Forms.ToolStripStatusLabel()
        Me.panel_ImportBIRRTransSummary = New System.Windows.Forms.Panel()
        Me.rb_Final = New System.Windows.Forms.RadioButton()
        Me.rb_Prelim = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtDirectory = New System.Windows.Forms.TextBox()
        Me.btnOpenFile = New System.Windows.Forms.Button()
        Me.dtDueDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtTransDate = New System.Windows.Forms.DateTimePicker()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.txt_BillingRemarks = New System.Windows.Forms.TextBox()
        Me.lblCollectionDate = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.panel_CheckList = New System.Windows.Forms.Panel()
        Me.btnExportperSTLID = New System.Windows.Forms.Button()
        Me.chckList = New System.Windows.Forms.CheckedListBox()
        Me.chckAll = New System.Windows.Forms.CheckBox()
        Me.btnDownload = New System.Windows.Forms.Button()
        Me.btn_Clear = New System.Windows.Forms.Button()
        Me.btnExportWTASummary = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Panel_Main = New System.Windows.Forms.Panel()
        Me.ctrl_statusStrip.SuspendLayout()
        Me.panel_ImportBIRRTransSummary.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.panel_CheckList.SuspendLayout()
        Me.Panel_Main.SuspendLayout()
        Me.SuspendLayout()
        '
        'ctrl_statusStrip
        '
        Me.ctrl_statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatus_LabelMsg})
        Me.ctrl_statusStrip.Location = New System.Drawing.Point(0, 333)
        Me.ctrl_statusStrip.Name = "ctrl_statusStrip"
        Me.ctrl_statusStrip.Size = New System.Drawing.Size(730, 22)
        Me.ctrl_statusStrip.TabIndex = 58
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
        'panel_ImportBIRRTransSummary
        '
        Me.panel_ImportBIRRTransSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.rb_Final)
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.rb_Prelim)
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.GroupBox1)
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.dtDueDate)
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.Label2)
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.dtTransDate)
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.btnImport)
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.txt_BillingRemarks)
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.lblCollectionDate)
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.Label1)
        Me.panel_ImportBIRRTransSummary.Location = New System.Drawing.Point(14, 15)
        Me.panel_ImportBIRRTransSummary.Name = "panel_ImportBIRRTransSummary"
        Me.panel_ImportBIRRTransSummary.Size = New System.Drawing.Size(440, 255)
        Me.panel_ImportBIRRTransSummary.TabIndex = 57
        '
        'rb_Final
        '
        Me.rb_Final.AutoSize = True
        Me.rb_Final.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.rb_Final.Location = New System.Drawing.Point(76, 65)
        Me.rb_Final.Name = "rb_Final"
        Me.rb_Final.Size = New System.Drawing.Size(50, 18)
        Me.rb_Final.TabIndex = 55
        Me.rb_Final.TabStop = True
        Me.rb_Final.Text = "Final"
        Me.rb_Final.UseVisualStyleBackColor = True
        '
        'rb_Prelim
        '
        Me.rb_Prelim.AutoSize = True
        Me.rb_Prelim.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.rb_Prelim.Location = New System.Drawing.Point(9, 65)
        Me.rb_Prelim.Name = "rb_Prelim"
        Me.rb_Prelim.Size = New System.Drawing.Size(61, 18)
        Me.rb_Prelim.TabIndex = 54
        Me.rb_Prelim.TabStop = True
        Me.rb_Prelim.Text = "Prelim"
        Me.rb_Prelim.UseVisualStyleBackColor = True
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
        Me.btnOpenFile.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnOpenFile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnOpenFile.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenFile.Image = Global.AccountsManagementForms.My.Resources.Resources.FileSearchIcon22x22
        Me.btnOpenFile.Location = New System.Drawing.Point(385, 17)
        Me.btnOpenFile.Name = "btnOpenFile"
        Me.btnOpenFile.Size = New System.Drawing.Size(35, 30)
        Me.btnOpenFile.TabIndex = 3
        Me.btnOpenFile.UseVisualStyleBackColor = True
        '
        'dtDueDate
        '
        Me.dtDueDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtDueDate.Location = New System.Drawing.Point(301, 87)
        Me.dtDueDate.Name = "dtDueDate"
        Me.dtDueDate.Size = New System.Drawing.Size(104, 20)
        Me.dtDueDate.TabIndex = 22
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 116)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 14)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "Billing Remarks:"
        '
        'dtTransDate
        '
        Me.dtTransDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTransDate.Location = New System.Drawing.Point(114, 87)
        Me.dtTransDate.Name = "dtTransDate"
        Me.dtTransDate.Size = New System.Drawing.Size(104, 20)
        Me.dtTransDate.TabIndex = 21
        '
        'btnImport
        '
        Me.btnImport.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnImport.BackColor = System.Drawing.Color.White
        Me.btnImport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnImport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnImport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImport.ForeColor = System.Drawing.Color.Black
        Me.btnImport.Image = Global.AccountsManagementForms.My.Resources.Resources.Upload2Icon22x22
        Me.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImport.Location = New System.Drawing.Point(322, 215)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(101, 34)
        Me.btnImport.TabIndex = 10
        Me.btnImport.Text = "&Import File"
        Me.btnImport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnImport.UseVisualStyleBackColor = False
        '
        'txt_BillingRemarks
        '
        Me.txt_BillingRemarks.BackColor = System.Drawing.Color.White
        Me.txt_BillingRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_BillingRemarks.Location = New System.Drawing.Point(114, 116)
        Me.txt_BillingRemarks.Multiline = True
        Me.txt_BillingRemarks.Name = "txt_BillingRemarks"
        Me.txt_BillingRemarks.Size = New System.Drawing.Size(311, 93)
        Me.txt_BillingRemarks.TabIndex = 52
        '
        'lblCollectionDate
        '
        Me.lblCollectionDate.AutoSize = True
        Me.lblCollectionDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCollectionDate.Location = New System.Drawing.Point(7, 90)
        Me.lblCollectionDate.Name = "lblCollectionDate"
        Me.lblCollectionDate.Size = New System.Drawing.Size(101, 14)
        Me.lblCollectionDate.TabIndex = 50
        Me.lblCollectionDate.Text = "Transaction Date:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(237, 91)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 14)
        Me.Label1.TabIndex = 51
        Me.Label1.Text = "Due Date:"
        '
        'panel_CheckList
        '
        Me.panel_CheckList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panel_CheckList.Controls.Add(Me.btnExportperSTLID)
        Me.panel_CheckList.Controls.Add(Me.chckList)
        Me.panel_CheckList.Controls.Add(Me.chckAll)
        Me.panel_CheckList.Location = New System.Drawing.Point(460, 15)
        Me.panel_CheckList.Name = "panel_CheckList"
        Me.panel_CheckList.Size = New System.Drawing.Size(252, 255)
        Me.panel_CheckList.TabIndex = 60
        '
        'btnExportperSTLID
        '
        Me.btnExportperSTLID.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnExportperSTLID.BackColor = System.Drawing.Color.White
        Me.btnExportperSTLID.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnExportperSTLID.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnExportperSTLID.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnExportperSTLID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportperSTLID.ForeColor = System.Drawing.Color.Black
        Me.btnExportperSTLID.Image = Global.AccountsManagementForms.My.Resources.Resources.ExcelIcon22x22
        Me.btnExportperSTLID.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExportperSTLID.Location = New System.Drawing.Point(11, 215)
        Me.btnExportperSTLID.Name = "btnExportperSTLID"
        Me.btnExportperSTLID.Size = New System.Drawing.Size(229, 34)
        Me.btnExportperSTLID.TabIndex = 30
        Me.btnExportperSTLID.Text = "Export per Settlement ID"
        Me.btnExportperSTLID.UseVisualStyleBackColor = False
        '
        'chckList
        '
        Me.chckList.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckList.FormattingEnabled = True
        Me.chckList.Location = New System.Drawing.Point(11, 25)
        Me.chckList.Name = "chckList"
        Me.chckList.Size = New System.Drawing.Size(229, 184)
        Me.chckList.TabIndex = 29
        '
        'chckAll
        '
        Me.chckAll.AutoSize = True
        Me.chckAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckAll.Location = New System.Drawing.Point(11, 5)
        Me.chckAll.Name = "chckAll"
        Me.chckAll.Size = New System.Drawing.Size(70, 18)
        Me.chckAll.TabIndex = 28
        Me.chckAll.Text = "Check All"
        Me.chckAll.UseVisualStyleBackColor = True
        '
        'btnDownload
        '
        Me.btnDownload.BackColor = System.Drawing.Color.White
        Me.btnDownload.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnDownload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnDownload.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDownload.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDownload.ForeColor = System.Drawing.Color.Black
        Me.btnDownload.Image = Global.AccountsManagementForms.My.Resources.Resources.PDFIcon22x22
        Me.btnDownload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDownload.Location = New System.Drawing.Point(337, 277)
        Me.btnDownload.Name = "btnDownload"
        Me.btnDownload.Size = New System.Drawing.Size(121, 39)
        Me.btnDownload.TabIndex = 30
        Me.btnDownload.Text = "      &Export to PDF"
        Me.btnDownload.UseVisualStyleBackColor = False
        '
        'btn_Clear
        '
        Me.btn_Clear.BackColor = System.Drawing.Color.White
        Me.btn_Clear.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Clear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Clear.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Clear.ForeColor = System.Drawing.Color.Black
        Me.btn_Clear.Image = Global.AccountsManagementForms.My.Resources.Resources.clear
        Me.btn_Clear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Clear.Location = New System.Drawing.Point(591, 276)
        Me.btn_Clear.Name = "btn_Clear"
        Me.btn_Clear.Size = New System.Drawing.Size(121, 39)
        Me.btn_Clear.TabIndex = 61
        Me.btn_Clear.Text = "&Clear"
        Me.btn_Clear.UseVisualStyleBackColor = False
        '
        'btnExportWTASummary
        '
        Me.btnExportWTASummary.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnExportWTASummary.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnExportWTASummary.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnExportWTASummary.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportWTASummary.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportWTASummary.Image = Global.AccountsManagementForms.My.Resources.Resources.ExcelIcon22x22
        Me.btnExportWTASummary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExportWTASummary.Location = New System.Drawing.Point(177, 277)
        Me.btnExportWTASummary.Name = "btnExportWTASummary"
        Me.btnExportWTASummary.Size = New System.Drawing.Size(154, 39)
        Me.btnExportWTASummary.TabIndex = 62
        Me.btnExportWTASummary.Text = "       E&xport WTA Summary"
        Me.btnExportWTASummary.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.White
        Me.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Image = Global.AccountsManagementForms.My.Resources.Resources.SaveIconColored22x22
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(464, 277)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(121, 39)
        Me.btnSave.TabIndex = 63
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'Panel_Main
        '
        Me.Panel_Main.Controls.Add(Me.panel_ImportBIRRTransSummary)
        Me.Panel_Main.Controls.Add(Me.btnSave)
        Me.Panel_Main.Controls.Add(Me.panel_CheckList)
        Me.Panel_Main.Controls.Add(Me.btnExportWTASummary)
        Me.Panel_Main.Controls.Add(Me.btnDownload)
        Me.Panel_Main.Controls.Add(Me.btn_Clear)
        Me.Panel_Main.Location = New System.Drawing.Point(0, 1)
        Me.Panel_Main.Name = "Panel_Main"
        Me.Panel_Main.Size = New System.Drawing.Size(730, 329)
        Me.Panel_Main.TabIndex = 64
        '
        'frmImportWESMAllocTransSummaryPrelim
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(730, 355)
        Me.Controls.Add(Me.Panel_Main)
        Me.Controls.Add(Me.ctrl_statusStrip)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmImportWESMAllocTransSummaryPrelim"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Upload WESM Allocation Transaction Summary Prelim or Final"
        Me.ctrl_statusStrip.ResumeLayout(False)
        Me.ctrl_statusStrip.PerformLayout()
        Me.panel_ImportBIRRTransSummary.ResumeLayout(False)
        Me.panel_ImportBIRRTransSummary.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.panel_CheckList.ResumeLayout(False)
        Me.panel_CheckList.PerformLayout()
        Me.Panel_Main.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ctrl_statusStrip As StatusStrip
    Friend WithEvents ToolStripStatus_LabelMsg As ToolStripStatusLabel
    Friend WithEvents panel_ImportBIRRTransSummary As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtDirectory As TextBox
    Friend WithEvents btnOpenFile As Button
    Friend WithEvents btnImport As Button
    Friend WithEvents dtDueDate As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents dtTransDate As DateTimePicker
    Friend WithEvents txt_BillingRemarks As TextBox
    Friend WithEvents lblCollectionDate As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents panel_CheckList As Panel
    Friend WithEvents chckList As CheckedListBox
    Friend WithEvents chckAll As CheckBox
    Friend WithEvents btnDownload As Button
    Friend WithEvents btn_Clear As Button
    Friend WithEvents btnExportWTASummary As Button
    Friend WithEvents rb_Final As RadioButton
    Friend WithEvents rb_Prelim As RadioButton
    Friend WithEvents btnSave As Button
    Friend WithEvents btnExportperSTLID As Button
    Friend WithEvents Panel_Main As Panel
End Class
