<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWESMBillTransactionSummaryPrint
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
        Me.chckList = New System.Windows.Forms.CheckedListBox()
        Me.chckAll = New System.Windows.Forms.CheckBox()
        Me.ddlBillingPeriod = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ddlSTLRun = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ctrl_statusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatus_LabelMsg = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btnExportToExcel = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnExportToPDF = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.ctrl_statusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnExportToExcel)
        Me.GroupBox1.Controls.Add(Me.btnClose)
        Me.GroupBox1.Controls.Add(Me.chckList)
        Me.GroupBox1.Controls.Add(Me.btnExportToPDF)
        Me.GroupBox1.Controls.Add(Me.chckAll)
        Me.GroupBox1.Controls.Add(Me.ddlBillingPeriod)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.ddlSTLRun)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(494, 303)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'chckList
        '
        Me.chckList.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckList.FormattingEnabled = True
        Me.chckList.Location = New System.Drawing.Point(253, 19)
        Me.chckList.Name = "chckList"
        Me.chckList.Size = New System.Drawing.Size(228, 274)
        Me.chckList.TabIndex = 3
        '
        'chckAll
        '
        Me.chckAll.AutoSize = True
        Me.chckAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckAll.Location = New System.Drawing.Point(177, 73)
        Me.chckAll.Name = "chckAll"
        Me.chckAll.Size = New System.Drawing.Size(70, 18)
        Me.chckAll.TabIndex = 2
        Me.chckAll.Text = "Check All"
        Me.chckAll.UseVisualStyleBackColor = True
        '
        'ddlBillingPeriod
        '
        Me.ddlBillingPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlBillingPeriod.FormattingEnabled = True
        Me.ddlBillingPeriod.Location = New System.Drawing.Point(111, 20)
        Me.ddlBillingPeriod.Name = "ddlBillingPeriod"
        Me.ddlBillingPeriod.Size = New System.Drawing.Size(136, 21)
        Me.ddlBillingPeriod.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Billing Period:"
        '
        'ddlSTLRun
        '
        Me.ddlSTLRun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlSTLRun.FormattingEnabled = True
        Me.ddlSTLRun.Location = New System.Drawing.Point(111, 46)
        Me.ddlSTLRun.Name = "ddlSTLRun"
        Me.ddlSTLRun.Size = New System.Drawing.Size(136, 21)
        Me.ddlSTLRun.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 14)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Settlement Run:"
        '
        'ctrl_statusStrip
        '
        Me.ctrl_statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatus_LabelMsg})
        Me.ctrl_statusStrip.Location = New System.Drawing.Point(0, 328)
        Me.ctrl_statusStrip.Name = "ctrl_statusStrip"
        Me.ctrl_statusStrip.Size = New System.Drawing.Size(516, 22)
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
        'btnExportToExcel
        '
        Me.btnExportToExcel.BackColor = System.Drawing.Color.White
        Me.btnExportToExcel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnExportToExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnExportToExcel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnExportToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportToExcel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportToExcel.ForeColor = System.Drawing.Color.Black
        Me.btnExportToExcel.Image = Global.AccountsManagementForms.My.Resources.Resources.ExcelIcon22x22
        Me.btnExportToExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExportToExcel.Location = New System.Drawing.Point(19, 209)
        Me.btnExportToExcel.Name = "btnExportToExcel"
        Me.btnExportToExcel.Size = New System.Drawing.Size(228, 39)
        Me.btnExportToExcel.TabIndex = 28
        Me.btnExportToExcel.Text = "Export to Excel"
        Me.btnExportToExcel.UseVisualStyleBackColor = False
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
        Me.btnClose.Location = New System.Drawing.Point(19, 254)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(228, 39)
        Me.btnClose.TabIndex = 26
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnExportToPDF
        '
        Me.btnExportToPDF.BackColor = System.Drawing.Color.White
        Me.btnExportToPDF.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnExportToPDF.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnExportToPDF.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnExportToPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportToPDF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportToPDF.ForeColor = System.Drawing.Color.Black
        Me.btnExportToPDF.Image = Global.AccountsManagementForms.My.Resources.Resources.PDFIcon22x22
        Me.btnExportToPDF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExportToPDF.Location = New System.Drawing.Point(19, 164)
        Me.btnExportToPDF.Name = "btnExportToPDF"
        Me.btnExportToPDF.Size = New System.Drawing.Size(228, 39)
        Me.btnExportToPDF.TabIndex = 27
        Me.btnExportToPDF.Text = "Export to PDF"
        Me.btnExportToPDF.UseVisualStyleBackColor = False
        '
        'frmWESMBillTransactionSummaryPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(516, 350)
        Me.Controls.Add(Me.ctrl_statusStrip)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmWESMBillTransactionSummaryPrint"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WTA And RTA Print Management"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ctrl_statusStrip.ResumeLayout(False)
        Me.ctrl_statusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnClose As Button
    Friend WithEvents btnExportToPDF As Button
    Friend WithEvents chckList As CheckedListBox
    Friend WithEvents chckAll As CheckBox
    Friend WithEvents ddlBillingPeriod As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ddlSTLRun As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnExportToExcel As Button
    Friend WithEvents ctrl_statusStrip As StatusStrip
    Friend WithEvents ToolStripStatus_LabelMsg As ToolStripStatusLabel
End Class
