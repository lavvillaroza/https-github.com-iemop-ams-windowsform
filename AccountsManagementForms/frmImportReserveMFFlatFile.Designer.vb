<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmImportReserveMFFlatFile
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.ctrl_statusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatus_LabelMsg = New System.Windows.Forms.ToolStripStatusLabel()
        Me.panel_ImportBIRRTransSummary = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtDirectory = New System.Windows.Forms.TextBox()
        Me.btnOpenFile = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.btnDownload = New System.Windows.Forms.Button()
        Me.btn_Clear = New System.Windows.Forms.Button()
        Me.Panel_Main = New System.Windows.Forms.Panel()
        Me.ctrl_statusStrip.SuspendLayout()
        Me.panel_ImportBIRRTransSummary.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel_Main.SuspendLayout()
        Me.SuspendLayout()
        '
        'ctrl_statusStrip
        '
        Me.ctrl_statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatus_LabelMsg})
        Me.ctrl_statusStrip.Location = New System.Drawing.Point(0, 178)
        Me.ctrl_statusStrip.Name = "ctrl_statusStrip"
        Me.ctrl_statusStrip.Size = New System.Drawing.Size(505, 22)
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
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.GroupBox1)
        Me.panel_ImportBIRRTransSummary.Controls.Add(Me.btnImport)
        Me.panel_ImportBIRRTransSummary.Location = New System.Drawing.Point(14, 15)
        Me.panel_ImportBIRRTransSummary.Name = "panel_ImportBIRRTransSummary"
        Me.panel_ImportBIRRTransSummary.Size = New System.Drawing.Size(474, 105)
        Me.panel_ImportBIRRTransSummary.TabIndex = 57
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtDirectory)
        Me.GroupBox1.Controls.Add(Me.btnOpenFile)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(466, 56)
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
        Me.txtDirectory.Size = New System.Drawing.Size(413, 20)
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
        Me.btnOpenFile.Location = New System.Drawing.Point(425, 17)
        Me.btnOpenFile.Name = "btnOpenFile"
        Me.btnOpenFile.Size = New System.Drawing.Size(35, 30)
        Me.btnOpenFile.TabIndex = 3
        Me.btnOpenFile.UseVisualStyleBackColor = False
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
        Me.btnImport.Location = New System.Drawing.Point(319, 66)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(150, 34)
        Me.btnImport.TabIndex = 10
        Me.btnImport.Text = "&Import File"
        Me.btnImport.UseVisualStyleBackColor = False
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
        Me.btnDownload.Location = New System.Drawing.Point(174, 127)
        Me.btnDownload.Name = "btnDownload"
        Me.btnDownload.Size = New System.Drawing.Size(154, 39)
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
        Me.btn_Clear.Location = New System.Drawing.Point(334, 126)
        Me.btn_Clear.Name = "btn_Clear"
        Me.btn_Clear.Size = New System.Drawing.Size(154, 39)
        Me.btn_Clear.TabIndex = 61
        Me.btn_Clear.Text = "&Clear"
        Me.btn_Clear.UseVisualStyleBackColor = False
        '
        'Panel_Main
        '
        Me.Panel_Main.Controls.Add(Me.btn_Clear)
        Me.Panel_Main.Controls.Add(Me.panel_ImportBIRRTransSummary)
        Me.Panel_Main.Controls.Add(Me.btnDownload)
        Me.Panel_Main.Location = New System.Drawing.Point(0, 1)
        Me.Panel_Main.Name = "Panel_Main"
        Me.Panel_Main.Size = New System.Drawing.Size(502, 178)
        Me.Panel_Main.TabIndex = 64
        '
        'frmImportReserveMFFlatFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(505, 200)
        Me.Controls.Add(Me.Panel_Main)
        Me.Controls.Add(Me.ctrl_statusStrip)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmImportReserveMFFlatFile"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Upload Reserve Market Fees (From CSV)"
        Me.ctrl_statusStrip.ResumeLayout(False)
        Me.ctrl_statusStrip.PerformLayout()
        Me.panel_ImportBIRRTransSummary.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
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
    Friend WithEvents btnDownload As Button
    Friend WithEvents btn_Clear As Button
    Friend WithEvents Panel_Main As Panel
End Class
