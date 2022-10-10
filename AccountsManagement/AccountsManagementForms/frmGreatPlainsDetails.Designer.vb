<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGreatPlainsDetails
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
        Me.DGV_Details = New System.Windows.Forms.DataGridView
        Me.CMD_Close = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.CMD_GenerateReport = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.LBL_Batch = New System.Windows.Forms.Label
        Me.LBL_GPRef = New System.Windows.Forms.Label
        Me.LBL_Charge = New System.Windows.Forms.Label
        Me.LBL_STLRun = New System.Windows.Forms.Label
        Me.LBL_BPeriod = New System.Windows.Forms.Label
        Me.LBL_JVNo = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.TXT_AP = New System.Windows.Forms.TextBox
        Me.TXT_AR = New System.Windows.Forms.TextBox
        Me.TXT_TotalNSS = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.CMD_Download = New System.Windows.Forms.Button
        CType(Me.DGV_Details, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DGV_Details
        '
        Me.DGV_Details.AllowUserToAddRows = False
        Me.DGV_Details.AllowUserToDeleteRows = False
        Me.DGV_Details.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_Details.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_Details.Location = New System.Drawing.Point(12, 110)
        Me.DGV_Details.Name = "DGV_Details"
        Me.DGV_Details.ReadOnly = True
        Me.DGV_Details.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGV_Details.Size = New System.Drawing.Size(678, 300)
        Me.DGV_Details.TabIndex = 0
        '
        'CMD_Close
        '
        Me.CMD_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CMD_Close.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMD_Close.Image = Global.AccountsManagementForms.My.Resources.Resources.close
        Me.CMD_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CMD_Close.Location = New System.Drawing.Point(537, 493)
        Me.CMD_Close.Name = "CMD_Close"
        Me.CMD_Close.Size = New System.Drawing.Size(153, 26)
        Me.CMD_Close.TabIndex = 1
        Me.CMD_Close.Text = "Close"
        Me.CMD_Close.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica Condensed", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(143, 19)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Journal Voucher No.:"
        '
        'CMD_GenerateReport
        '
        Me.CMD_GenerateReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CMD_GenerateReport.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMD_GenerateReport.Image = Global.AccountsManagementForms.My.Resources.Resources.report
        Me.CMD_GenerateReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CMD_GenerateReport.Location = New System.Drawing.Point(537, 461)
        Me.CMD_GenerateReport.Name = "CMD_GenerateReport"
        Me.CMD_GenerateReport.Size = New System.Drawing.Size(153, 26)
        Me.CMD_GenerateReport.TabIndex = 3
        Me.CMD_GenerateReport.Text = "Generate Report"
        Me.CMD_GenerateReport.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica Condensed", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(368, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 19)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Charge Type:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Helvetica Condensed", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(98, 19)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Billing Period:"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Helvetica Condensed", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(368, 39)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 19)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Batch Code:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Helvetica Condensed", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 60)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 19)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "STL Run:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.LBL_Batch)
        Me.GroupBox1.Controls.Add(Me.LBL_GPRef)
        Me.GroupBox1.Controls.Add(Me.LBL_Charge)
        Me.GroupBox1.Controls.Add(Me.LBL_STLRun)
        Me.GroupBox1.Controls.Add(Me.LBL_BPeriod)
        Me.GroupBox1.Controls.Add(Me.LBL_JVNo)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(678, 92)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        '
        'LBL_Batch
        '
        Me.LBL_Batch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LBL_Batch.AutoSize = True
        Me.LBL_Batch.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL_Batch.Location = New System.Drawing.Point(514, 41)
        Me.LBL_Batch.Name = "LBL_Batch"
        Me.LBL_Batch.Size = New System.Drawing.Size(67, 15)
        Me.LBL_Batch.TabIndex = 15
        Me.LBL_Batch.Text = "BatchText"
        Me.LBL_Batch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBL_GPRef
        '
        Me.LBL_GPRef.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LBL_GPRef.AutoSize = True
        Me.LBL_GPRef.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL_GPRef.Location = New System.Drawing.Point(514, 18)
        Me.LBL_GPRef.Name = "LBL_GPRef"
        Me.LBL_GPRef.Size = New System.Drawing.Size(72, 15)
        Me.LBL_GPRef.TabIndex = 14
        Me.LBL_GPRef.Text = "GPRefText"
        Me.LBL_GPRef.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBL_Charge
        '
        Me.LBL_Charge.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LBL_Charge.AutoSize = True
        Me.LBL_Charge.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL_Charge.Location = New System.Drawing.Point(514, 62)
        Me.LBL_Charge.Name = "LBL_Charge"
        Me.LBL_Charge.Size = New System.Drawing.Size(74, 15)
        Me.LBL_Charge.TabIndex = 13
        Me.LBL_Charge.Text = "ChargeText"
        Me.LBL_Charge.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBL_STLRun
        '
        Me.LBL_STLRun.AutoSize = True
        Me.LBL_STLRun.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL_STLRun.Location = New System.Drawing.Point(165, 62)
        Me.LBL_STLRun.Name = "LBL_STLRun"
        Me.LBL_STLRun.Size = New System.Drawing.Size(57, 15)
        Me.LBL_STLRun.TabIndex = 12
        Me.LBL_STLRun.Text = "STLText"
        Me.LBL_STLRun.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBL_BPeriod
        '
        Me.LBL_BPeriod.AutoSize = True
        Me.LBL_BPeriod.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL_BPeriod.Location = New System.Drawing.Point(165, 41)
        Me.LBL_BPeriod.Name = "LBL_BPeriod"
        Me.LBL_BPeriod.Size = New System.Drawing.Size(51, 15)
        Me.LBL_BPeriod.TabIndex = 11
        Me.LBL_BPeriod.Text = "BPText"
        Me.LBL_BPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBL_JVNo
        '
        Me.LBL_JVNo.AutoSize = True
        Me.LBL_JVNo.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL_JVNo.Location = New System.Drawing.Point(165, 18)
        Me.LBL_JVNo.Name = "LBL_JVNo"
        Me.LBL_JVNo.Size = New System.Drawing.Size(49, 15)
        Me.LBL_JVNo.TabIndex = 10
        Me.LBL_JVNo.Text = "JVText"
        Me.LBL_JVNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Helvetica Condensed", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(368, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(120, 19)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "GP Reference No:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Helvetica Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(25, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 17)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Total AP:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Helvetica Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(25, 45)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(63, 17)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Total AR:"
        '
        'TXT_AP
        '
        Me.TXT_AP.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TXT_AP.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXT_AP.Location = New System.Drawing.Point(106, 14)
        Me.TXT_AP.Name = "TXT_AP"
        Me.TXT_AP.ReadOnly = True
        Me.TXT_AP.Size = New System.Drawing.Size(197, 24)
        Me.TXT_AP.TabIndex = 18
        Me.TXT_AP.TabStop = False
        Me.TXT_AP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXT_AR
        '
        Me.TXT_AR.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TXT_AR.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXT_AR.Location = New System.Drawing.Point(106, 41)
        Me.TXT_AR.Name = "TXT_AR"
        Me.TXT_AR.ReadOnly = True
        Me.TXT_AR.Size = New System.Drawing.Size(197, 24)
        Me.TXT_AR.TabIndex = 19
        Me.TXT_AR.TabStop = False
        Me.TXT_AR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXT_TotalNSS
        '
        Me.TXT_TotalNSS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TXT_TotalNSS.Font = New System.Drawing.Font("Helvetica Condensed", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXT_TotalNSS.Location = New System.Drawing.Point(106, 69)
        Me.TXT_TotalNSS.Name = "TXT_TotalNSS"
        Me.TXT_TotalNSS.ReadOnly = True
        Me.TXT_TotalNSS.Size = New System.Drawing.Size(197, 24)
        Me.TXT_TotalNSS.TabIndex = 21
        Me.TXT_TotalNSS.TabStop = False
        Me.TXT_TotalNSS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Helvetica Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(18, 73)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 17)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "Total NSS:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.TXT_TotalNSS)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.TXT_AP)
        Me.GroupBox2.Controls.Add(Me.TXT_AR)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 416)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(309, 103)
        Me.GroupBox2.TabIndex = 22
        Me.GroupBox2.TabStop = False
        '
        'CMD_Download
        '
        Me.CMD_Download.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CMD_Download.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMD_Download.Image = Global.AccountsManagementForms.My.Resources.Resources.execute
        Me.CMD_Download.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CMD_Download.Location = New System.Drawing.Point(537, 424)
        Me.CMD_Download.Name = "CMD_Download"
        Me.CMD_Download.Size = New System.Drawing.Size(153, 26)
        Me.CMD_Download.TabIndex = 23
        Me.CMD_Download.Text = "Download CSV"
        Me.CMD_Download.UseVisualStyleBackColor = True
        '
        'frmGreatPlainsDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(702, 531)
        Me.Controls.Add(Me.CMD_Download)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CMD_GenerateReport)
        Me.Controls.Add(Me.CMD_Close)
        Me.Controls.Add(Me.DGV_Details)
        Me.MinimumSize = New System.Drawing.Size(718, 569)
        Me.Name = "frmGreatPlainsDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Journal Voucher Details"
        CType(Me.DGV_Details, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGV_Details As System.Windows.Forms.DataGridView
    Friend WithEvents CMD_Close As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CMD_GenerateReport As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents LBL_Batch As System.Windows.Forms.Label
    Friend WithEvents LBL_GPRef As System.Windows.Forms.Label
    Friend WithEvents LBL_Charge As System.Windows.Forms.Label
    Friend WithEvents LBL_STLRun As System.Windows.Forms.Label
    Friend WithEvents LBL_BPeriod As System.Windows.Forms.Label
    Friend WithEvents LBL_JVNo As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TXT_AP As System.Windows.Forms.TextBox
    Friend WithEvents TXT_AR As System.Windows.Forms.TextBox
    Friend WithEvents TXT_TotalNSS As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents CMD_Download As System.Windows.Forms.Button
End Class
