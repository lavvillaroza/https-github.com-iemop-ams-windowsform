<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBRCollectionReportDetails
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
        Me.TableLayoutPanel_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbDaily = New System.Windows.Forms.RadioButton()
        Me.chkbox_SelectAll = New System.Windows.Forms.CheckBox()
        Me.rbMonthly = New System.Windows.Forms.RadioButton()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnShowReport = New System.Windows.Forms.Button()
        Me.btn_ExportToExcel = New System.Windows.Forms.Button()
        Me.btn_GeneratePDF = New System.Windows.Forms.Button()
        Me.gbox_Participants = New System.Windows.Forms.GroupBox()
        Me.chkLB_Participants = New System.Windows.Forms.CheckedListBox()
        Me.ToolStripStatusLabelCR = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ctrl_statusStrip = New System.Windows.Forms.StatusStrip()
        Me.TableLayoutPanel_Main.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.gbox_Participants.SuspendLayout()
        Me.ctrl_statusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel_Main
        '
        Me.TableLayoutPanel_Main.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel_Main.ColumnCount = 1
        Me.TableLayoutPanel_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Main.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel_Main.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TableLayoutPanel_Main.Location = New System.Drawing.Point(3, 6)
        Me.TableLayoutPanel_Main.Name = "TableLayoutPanel_Main"
        Me.TableLayoutPanel_Main.RowCount = 1
        Me.TableLayoutPanel_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 448.0!))
        Me.TableLayoutPanel_Main.Size = New System.Drawing.Size(331, 450)
        Me.TableLayoutPanel_Main.TabIndex = 7
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.rbDaily)
        Me.Panel1.Controls.Add(Me.chkbox_SelectAll)
        Me.Panel1.Controls.Add(Me.rbMonthly)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnShowReport)
        Me.Panel1.Controls.Add(Me.btn_ExportToExcel)
        Me.Panel1.Controls.Add(Me.btn_GeneratePDF)
        Me.Panel1.Controls.Add(Me.gbox_Participants)
        Me.Panel1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(321, 440)
        Me.Panel1.TabIndex = 0
        '
        'rbDaily
        '
        Me.rbDaily.AutoSize = True
        Me.rbDaily.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbDaily.ForeColor = System.Drawing.Color.Black
        Me.rbDaily.Location = New System.Drawing.Point(17, 11)
        Me.rbDaily.Name = "rbDaily"
        Me.rbDaily.Size = New System.Drawing.Size(48, 18)
        Me.rbDaily.TabIndex = 29
        Me.rbDaily.TabStop = True
        Me.rbDaily.Text = "Daily"
        Me.rbDaily.UseVisualStyleBackColor = True
        '
        'chkbox_SelectAll
        '
        Me.chkbox_SelectAll.AutoSize = True
        Me.chkbox_SelectAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbox_SelectAll.ForeColor = System.Drawing.Color.Black
        Me.chkbox_SelectAll.Location = New System.Drawing.Point(240, 12)
        Me.chkbox_SelectAll.Name = "chkbox_SelectAll"
        Me.chkbox_SelectAll.Size = New System.Drawing.Size(70, 18)
        Me.chkbox_SelectAll.TabIndex = 1
        Me.chkbox_SelectAll.Text = "Select All"
        Me.chkbox_SelectAll.UseVisualStyleBackColor = True
        '
        'rbMonthly
        '
        Me.rbMonthly.AutoSize = True
        Me.rbMonthly.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbMonthly.ForeColor = System.Drawing.Color.Black
        Me.rbMonthly.Location = New System.Drawing.Point(71, 11)
        Me.rbMonthly.Name = "rbMonthly"
        Me.rbMonthly.Size = New System.Drawing.Size(62, 18)
        Me.rbMonthly.TabIndex = 28
        Me.rbMonthly.TabStop = True
        Me.rbMonthly.Text = "Monthly"
        Me.rbMonthly.UseVisualStyleBackColor = True
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
        Me.btnClose.Location = New System.Drawing.Point(164, 391)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(146, 39)
        Me.btnClose.TabIndex = 27
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
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
        Me.btnShowReport.Location = New System.Drawing.Point(12, 346)
        Me.btnShowReport.Name = "btnShowReport"
        Me.btnShowReport.Size = New System.Drawing.Size(146, 39)
        Me.btnShowReport.TabIndex = 26
        Me.btnShowReport.Text = "Generate Report"
        Me.btnShowReport.UseVisualStyleBackColor = True
        '
        'btn_ExportToExcel
        '
        Me.btn_ExportToExcel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_ExportToExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_ExportToExcel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_ExportToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ExportToExcel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ExportToExcel.Image = Global.AccountsManagementForms.My.Resources.Resources.ExcelIcon22x22
        Me.btn_ExportToExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_ExportToExcel.Location = New System.Drawing.Point(164, 346)
        Me.btn_ExportToExcel.Name = "btn_ExportToExcel"
        Me.btn_ExportToExcel.Size = New System.Drawing.Size(146, 39)
        Me.btn_ExportToExcel.TabIndex = 16
        Me.btn_ExportToExcel.Text = "Export to Excel"
        Me.btn_ExportToExcel.UseVisualStyleBackColor = True
        '
        'btn_GeneratePDF
        '
        Me.btn_GeneratePDF.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_GeneratePDF.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_GeneratePDF.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_GeneratePDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_GeneratePDF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_GeneratePDF.Image = Global.AccountsManagementForms.My.Resources.Resources.PDFIcon22x22
        Me.btn_GeneratePDF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_GeneratePDF.Location = New System.Drawing.Point(12, 391)
        Me.btn_GeneratePDF.Name = "btn_GeneratePDF"
        Me.btn_GeneratePDF.Size = New System.Drawing.Size(146, 39)
        Me.btn_GeneratePDF.TabIndex = 15
        Me.btn_GeneratePDF.Text = "Export to PDF"
        Me.btn_GeneratePDF.UseVisualStyleBackColor = True
        '
        'gbox_Participants
        '
        Me.gbox_Participants.Controls.Add(Me.chkLB_Participants)
        Me.gbox_Participants.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbox_Participants.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.gbox_Participants.Location = New System.Drawing.Point(12, 41)
        Me.gbox_Participants.Name = "gbox_Participants"
        Me.gbox_Participants.Size = New System.Drawing.Size(298, 299)
        Me.gbox_Participants.TabIndex = 4
        Me.gbox_Participants.TabStop = False
        Me.gbox_Participants.Text = "Participant/s:"
        '
        'chkLB_Participants
        '
        Me.chkLB_Participants.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLB_Participants.FormattingEnabled = True
        Me.chkLB_Participants.Location = New System.Drawing.Point(5, 17)
        Me.chkLB_Participants.Name = "chkLB_Participants"
        Me.chkLB_Participants.Size = New System.Drawing.Size(285, 274)
        Me.chkLB_Participants.TabIndex = 2
        '
        'ToolStripStatusLabelCR
        '
        Me.ToolStripStatusLabelCR.Name = "ToolStripStatusLabelCR"
        Me.ToolStripStatusLabelCR.Size = New System.Drawing.Size(48, 17)
        Me.ToolStripStatusLabelCR.Text = "Ready..."
        '
        'ctrl_statusStrip
        '
        Me.ctrl_statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabelCR})
        Me.ctrl_statusStrip.Location = New System.Drawing.Point(0, 457)
        Me.ctrl_statusStrip.Name = "ctrl_statusStrip"
        Me.ctrl_statusStrip.Size = New System.Drawing.Size(339, 22)
        Me.ctrl_statusStrip.TabIndex = 8
        Me.ctrl_statusStrip.Text = "StatusStrip1"
        '
        'frmBRCollectionReportDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(339, 479)
        Me.Controls.Add(Me.ctrl_statusStrip)
        Me.Controls.Add(Me.TableLayoutPanel_Main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBRCollectionReportDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BIR Ruling Collection Report (View)"
        Me.TableLayoutPanel_Main.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.gbox_Participants.ResumeLayout(False)
        Me.ctrl_statusStrip.ResumeLayout(False)
        Me.ctrl_statusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel_Main As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnClose As Button
    Friend WithEvents btnShowReport As Button
    Friend WithEvents btn_ExportToExcel As Button
    Friend WithEvents btn_GeneratePDF As Button
    Friend WithEvents gbox_Participants As GroupBox
    Friend WithEvents chkLB_Participants As CheckedListBox
    Friend WithEvents chkbox_SelectAll As CheckBox
    Friend WithEvents ToolStripStatusLabelCR As ToolStripStatusLabel
    Friend WithEvents ctrl_statusStrip As StatusStrip
    Friend WithEvents rbDaily As RadioButton
    Friend WithEvents rbMonthly As RadioButton
End Class
