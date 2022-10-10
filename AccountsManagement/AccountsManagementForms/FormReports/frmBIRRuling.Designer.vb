<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBIRRuling
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
        Me.TableLayoutPanel_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnShowReport = New System.Windows.Forms.Button()
        Me.btn_ExportToExcel = New System.Windows.Forms.Button()
        Me.btn_GeneratePDF = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtBox_DateTo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmd_Generate = New System.Windows.Forms.Button()
        Me.ddl_TransDateFrom = New System.Windows.Forms.ComboBox()
        Me.gbox_Participants = New System.Windows.Forms.GroupBox()
        Me.chkLB_Participants = New System.Windows.Forms.CheckedListBox()
        Me.chkbox_SelectAll = New System.Windows.Forms.CheckBox()
        Me.ToolTipCR = New System.Windows.Forms.ToolTip(Me.components)
        Me.ctrl_statusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabelCR = New System.Windows.Forms.ToolStripStatusLabel()
        Me.cbo_date = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.TableLayoutPanel_Main.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
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
        Me.TableLayoutPanel_Main.Location = New System.Drawing.Point(12, 12)
        Me.TableLayoutPanel_Main.Name = "TableLayoutPanel_Main"
        Me.TableLayoutPanel_Main.RowCount = 1
        Me.TableLayoutPanel_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 540.0!))
        Me.TableLayoutPanel_Main.Size = New System.Drawing.Size(390, 581)
        Me.TableLayoutPanel_Main.TabIndex = 5
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnShowReport)
        Me.Panel1.Controls.Add(Me.btn_ExportToExcel)
        Me.Panel1.Controls.Add(Me.btn_GeneratePDF)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.gbox_Participants)
        Me.Panel1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(380, 571)
        Me.Panel1.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(194, 525)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(173, 39)
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
        Me.btnShowReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btnShowReport.ForeColor = System.Drawing.Color.Black
        Me.btnShowReport.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.btnShowReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnShowReport.Location = New System.Drawing.Point(13, 480)
        Me.btnShowReport.Name = "btnShowReport"
        Me.btnShowReport.Size = New System.Drawing.Size(165, 39)
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
        Me.btn_ExportToExcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btn_ExportToExcel.Image = Global.AccountsManagementForms.My.Resources.Resources.ExcelIcon22x22
        Me.btn_ExportToExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_ExportToExcel.Location = New System.Drawing.Point(194, 479)
        Me.btn_ExportToExcel.Name = "btn_ExportToExcel"
        Me.btn_ExportToExcel.Size = New System.Drawing.Size(173, 40)
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
        Me.btn_GeneratePDF.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_GeneratePDF.Image = Global.AccountsManagementForms.My.Resources.Resources.PDFIcon22x22
        Me.btn_GeneratePDF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_GeneratePDF.Location = New System.Drawing.Point(13, 525)
        Me.btn_GeneratePDF.Name = "btn_GeneratePDF"
        Me.btn_GeneratePDF.Size = New System.Drawing.Size(165, 39)
        Me.btn_GeneratePDF.TabIndex = 15
        Me.btn_GeneratePDF.Text = "Export to PDF"
        Me.btn_GeneratePDF.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RadioButton2)
        Me.GroupBox2.Controls.Add(Me.RadioButton1)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.cbo_date)
        Me.GroupBox2.Controls.Add(Me.txtBox_DateTo)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.btnSearch)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.cmd_Generate)
        Me.GroupBox2.Controls.Add(Me.ddl_TransDateFrom)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(13, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(354, 133)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Transaction Date:"
        '
        'txtBox_DateTo
        '
        Me.txtBox_DateTo.Location = New System.Drawing.Point(142, 44)
        Me.txtBox_DateTo.Multiline = True
        Me.txtBox_DateTo.Name = "txtBox_DateTo"
        Me.txtBox_DateTo.ReadOnly = True
        Me.txtBox_DateTo.Size = New System.Drawing.Size(125, 22)
        Me.txtBox_DateTo.TabIndex = 15
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(145, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(21, 14)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "To:"
        '
        'btnSearch
        '
        Me.btnSearch.AccessibleName = ""
        Me.btnSearch.BackColor = System.Drawing.SystemColors.Control
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Image = Global.AccountsManagementForms.My.Resources.Resources.search
        Me.btnSearch.Location = New System.Drawing.Point(272, 39)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(35, 30)
        Me.btnSearch.TabIndex = 14
        Me.ToolTipCR.SetToolTip(Me.btnSearch, "Search")
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(14, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 14)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "From:"
        '
        'cmd_Generate
        '
        Me.cmd_Generate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Generate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Generate.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Generate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Generate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Generate.Image = Global.AccountsManagementForms.My.Resources.Resources.NewGreenIcon22x22
        Me.cmd_Generate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Generate.Location = New System.Drawing.Point(313, 39)
        Me.cmd_Generate.Name = "cmd_Generate"
        Me.cmd_Generate.Size = New System.Drawing.Size(35, 30)
        Me.cmd_Generate.TabIndex = 14
        Me.ToolTipCR.SetToolTip(Me.cmd_Generate, "Generate")
        Me.cmd_Generate.UseVisualStyleBackColor = True
        '
        'ddl_TransDateFrom
        '
        Me.ddl_TransDateFrom.BackColor = System.Drawing.SystemColors.Window
        Me.ddl_TransDateFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddl_TransDateFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddl_TransDateFrom.FormattingEnabled = True
        Me.ddl_TransDateFrom.Location = New System.Drawing.Point(11, 44)
        Me.ddl_TransDateFrom.Name = "ddl_TransDateFrom"
        Me.ddl_TransDateFrom.Size = New System.Drawing.Size(125, 22)
        Me.ddl_TransDateFrom.TabIndex = 1
        '
        'gbox_Participants
        '
        Me.gbox_Participants.Controls.Add(Me.chkLB_Participants)
        Me.gbox_Participants.Controls.Add(Me.chkbox_SelectAll)
        Me.gbox_Participants.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbox_Participants.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.gbox_Participants.Location = New System.Drawing.Point(13, 147)
        Me.gbox_Participants.Name = "gbox_Participants"
        Me.gbox_Participants.Size = New System.Drawing.Size(354, 326)
        Me.gbox_Participants.TabIndex = 4
        Me.gbox_Participants.TabStop = False
        Me.gbox_Participants.Text = "Participant/s:"
        '
        'chkLB_Participants
        '
        Me.chkLB_Participants.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLB_Participants.FormattingEnabled = True
        Me.chkLB_Participants.Location = New System.Drawing.Point(5, 38)
        Me.chkLB_Participants.Name = "chkLB_Participants"
        Me.chkLB_Participants.Size = New System.Drawing.Size(343, 274)
        Me.chkLB_Participants.TabIndex = 2
        '
        'chkbox_SelectAll
        '
        Me.chkbox_SelectAll.AutoSize = True
        Me.chkbox_SelectAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbox_SelectAll.ForeColor = System.Drawing.Color.Black
        Me.chkbox_SelectAll.Location = New System.Drawing.Point(8, 17)
        Me.chkbox_SelectAll.Name = "chkbox_SelectAll"
        Me.chkbox_SelectAll.Size = New System.Drawing.Size(70, 18)
        Me.chkbox_SelectAll.TabIndex = 1
        Me.chkbox_SelectAll.Text = "Select All"
        Me.chkbox_SelectAll.UseVisualStyleBackColor = True
        '
        'ctrl_statusStrip
        '
        Me.ctrl_statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabelCR})
        Me.ctrl_statusStrip.Location = New System.Drawing.Point(0, 602)
        Me.ctrl_statusStrip.Name = "ctrl_statusStrip"
        Me.ctrl_statusStrip.Size = New System.Drawing.Size(409, 22)
        Me.ctrl_statusStrip.TabIndex = 6
        Me.ctrl_statusStrip.Text = "StatusStrip1"
        '
        'ToolStripStatusLabelCR
        '
        Me.ToolStripStatusLabelCR.Name = "ToolStripStatusLabelCR"
        Me.ToolStripStatusLabelCR.Size = New System.Drawing.Size(48, 17)
        Me.ToolStripStatusLabelCR.Text = "Ready..."
        '
        'cbo_date
        '
        Me.cbo_date.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_date.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_date.FormattingEnabled = True
        Me.cbo_date.Location = New System.Drawing.Point(11, 89)
        Me.cbo_date.Name = "cbo_date"
        Me.cbo_date.Size = New System.Drawing.Size(125, 22)
        Me.cbo_date.TabIndex = 16
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(14, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 14)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Due Date:"
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton1.ForeColor = System.Drawing.Color.Black
        Me.RadioButton1.Location = New System.Drawing.Point(142, 90)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(62, 18)
        Me.RadioButton1.TabIndex = 18
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Monthly"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton2.ForeColor = System.Drawing.Color.Black
        Me.RadioButton2.Location = New System.Drawing.Point(210, 89)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(48, 18)
        Me.RadioButton2.TabIndex = 19
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "Daily"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'frmBIRRuling
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(409, 624)
        Me.Controls.Add(Me.ctrl_statusStrip)
        Me.Controls.Add(Me.TableLayoutPanel_Main)
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmBIRRuling"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Collection Report Management (BIR Ruling)"
        Me.TableLayoutPanel_Main.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.gbox_Participants.ResumeLayout(False)
        Me.gbox_Participants.PerformLayout()
        Me.ctrl_statusStrip.ResumeLayout(False)
        Me.ctrl_statusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel_Main As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents gbox_Participants As System.Windows.Forms.GroupBox
    Friend WithEvents chkLB_Participants As System.Windows.Forms.CheckedListBox
    Friend WithEvents chkbox_SelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents cmd_Generate As System.Windows.Forms.Button
    Friend WithEvents btn_GeneratePDF As System.Windows.Forms.Button
    Friend WithEvents btn_ExportToExcel As Button
    Friend WithEvents btnShowReport As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents ToolTipCR As ToolTip
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ddl_TransDateFrom As ComboBox
    Friend WithEvents txtBox_DateTo As TextBox
    Friend WithEvents ctrl_statusStrip As StatusStrip
    Friend WithEvents ToolStripStatusLabelCR As ToolStripStatusLabel
    Friend WithEvents cbo_date As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton
End Class
