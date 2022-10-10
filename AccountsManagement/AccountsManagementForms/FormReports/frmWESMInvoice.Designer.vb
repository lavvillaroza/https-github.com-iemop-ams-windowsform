<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWESMInvoice
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
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnDownload = New System.Windows.Forms.Button()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.txtDestination = New System.Windows.Forms.TextBox()
        Me.rbMarketFees = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.rbEnergy = New System.Windows.Forms.RadioButton()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnShowReport = New System.Windows.Forms.Button()
        Me.chckList = New System.Windows.Forms.CheckedListBox()
        Me.chckAll = New System.Windows.Forms.CheckBox()
        Me.ddlBillingPeriod = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ddlSTLRun = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CheckBox1)
        Me.GroupBox1.Controls.Add(Me.btnClose)
        Me.GroupBox1.Controls.Add(Me.btnDownload)
        Me.GroupBox1.Controls.Add(Me.btnOpen)
        Me.GroupBox1.Controls.Add(Me.txtDestination)
        Me.GroupBox1.Controls.Add(Me.rbMarketFees)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.rbEnergy)
        Me.GroupBox1.Controls.Add(Me.btnRefresh)
        Me.GroupBox1.Controls.Add(Me.btnShowReport)
        Me.GroupBox1.Controls.Add(Me.chckList)
        Me.GroupBox1.Controls.Add(Me.chckAll)
        Me.GroupBox1.Controls.Add(Me.ddlBillingPeriod)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.ddlSTLRun)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(325, 487)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(19, 356)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(186, 18)
        Me.CheckBox1.TabIndex = 31
        Me.CheckBox1.Text = "Add System Generated Message"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(164, 434)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(140, 39)
        Me.btnClose.TabIndex = 26
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnDownload
        '
        Me.btnDownload.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnDownload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnDownload.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDownload.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDownload.ForeColor = System.Drawing.Color.Black
        Me.btnDownload.Image = Global.AccountsManagementForms.My.Resources.Resources.PDFIcon22x22
        Me.btnDownload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDownload.Location = New System.Drawing.Point(19, 434)
        Me.btnDownload.Name = "btnDownload"
        Me.btnDownload.Size = New System.Drawing.Size(140, 39)
        Me.btnDownload.TabIndex = 27
        Me.btnDownload.Text = "Export to PDF"
        Me.btnDownload.UseVisualStyleBackColor = True
        '
        'btnOpen
        '
        Me.btnOpen.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnOpen.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpen.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpen.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.btnOpen.Location = New System.Drawing.Point(290, 324)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(20, 20)
        Me.btnOpen.TabIndex = 30
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'txtDestination
        '
        Me.txtDestination.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDestination.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDestination.Location = New System.Drawing.Point(18, 325)
        Me.txtDestination.Name = "txtDestination"
        Me.txtDestination.ReadOnly = True
        Me.txtDestination.Size = New System.Drawing.Size(268, 20)
        Me.txtDestination.TabIndex = 29
        '
        'rbMarketFees
        '
        Me.rbMarketFees.AutoSize = True
        Me.rbMarketFees.Location = New System.Drawing.Point(162, 19)
        Me.rbMarketFees.Name = "rbMarketFees"
        Me.rbMarketFees.Size = New System.Drawing.Size(84, 18)
        Me.rbMarketFees.TabIndex = 3
        Me.rbMarketFees.TabStop = True
        Me.rbMarketFees.Text = "Market Fees"
        Me.rbMarketFees.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 307)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(110, 14)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Destination Folder:"
        '
        'rbEnergy
        '
        Me.rbEnergy.AutoSize = True
        Me.rbEnergy.Location = New System.Drawing.Point(19, 19)
        Me.rbEnergy.Name = "rbEnergy"
        Me.rbEnergy.Size = New System.Drawing.Size(59, 18)
        Me.rbEnergy.TabIndex = 2
        Me.rbEnergy.TabStop = True
        Me.rbEnergy.Text = "Energy"
        Me.rbEnergy.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.ForeColor = System.Drawing.Color.Black
        Me.btnRefresh.Image = Global.AccountsManagementForms.My.Resources.Resources.RefreshGreenIcon22x22
        Me.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRefresh.Location = New System.Drawing.Point(19, 389)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(140, 39)
        Me.btnRefresh.TabIndex = 6
        Me.btnRefresh.Text = "&Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnShowReport
        '
        Me.btnShowReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnShowReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnShowReport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnShowReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShowReport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowReport.ForeColor = System.Drawing.Color.Black
        Me.btnShowReport.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.btnShowReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnShowReport.Location = New System.Drawing.Point(164, 389)
        Me.btnShowReport.Name = "btnShowReport"
        Me.btnShowReport.Size = New System.Drawing.Size(140, 39)
        Me.btnShowReport.TabIndex = 25
        Me.btnShowReport.Text = "Generate Report"
        Me.btnShowReport.UseVisualStyleBackColor = True
        '
        'chckList
        '
        Me.chckList.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckList.FormattingEnabled = True
        Me.chckList.Location = New System.Drawing.Point(19, 113)
        Me.chckList.Name = "chckList"
        Me.chckList.Size = New System.Drawing.Size(289, 184)
        Me.chckList.TabIndex = 3
        '
        'chckAll
        '
        Me.chckAll.AutoSize = True
        Me.chckAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckAll.Location = New System.Drawing.Point(19, 92)
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
        Me.ddlBillingPeriod.Location = New System.Drawing.Point(111, 41)
        Me.ddlBillingPeriod.Name = "ddlBillingPeriod"
        Me.ddlBillingPeriod.Size = New System.Drawing.Size(197, 22)
        Me.ddlBillingPeriod.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Billing Period:"
        '
        'ddlSTLRun
        '
        Me.ddlSTLRun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlSTLRun.FormattingEnabled = True
        Me.ddlSTLRun.Location = New System.Drawing.Point(111, 67)
        Me.ddlSTLRun.Name = "ddlSTLRun"
        Me.ddlSTLRun.Size = New System.Drawing.Size(197, 22)
        Me.ddlSTLRun.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 14)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Settlement Run:"
        '
        'frmWESMInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(349, 501)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmWESMInvoice"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Final Statement"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbMarketFees As System.Windows.Forms.RadioButton
    Friend WithEvents rbEnergy As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ddlBillingPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ddlSTLRun As System.Windows.Forms.ComboBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnDownload As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnShowReport As System.Windows.Forms.Button
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents txtDestination As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chckList As System.Windows.Forms.CheckedListBox
    Friend WithEvents chckAll As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
End Class
