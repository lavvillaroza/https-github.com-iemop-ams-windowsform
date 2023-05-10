<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymentSummaryDetails
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btn_ExportToExcel = New System.Windows.Forms.Button()
        Me.cbo_date = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txt_Note = New System.Windows.Forms.TextBox()
        Me.chk_Note = New System.Windows.Forms.CheckBox()
        Me.gbox_Participants = New System.Windows.Forms.GroupBox()
        Me.chkLB_Participants = New System.Windows.Forms.CheckedListBox()
        Me.chkbox_SelectAll = New System.Windows.Forms.CheckBox()
        Me.StatusStrip_STLNotice = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel_Text = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripProgressBar_STLNotice = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripStatusLabel_Percent = New System.Windows.Forms.ToolStripStatusLabel()
        Me.bgw_PaymentSummaryDetails = New System.ComponentModel.BackgroundWorker()
        Me.TableLayoutPanel_Main.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.gbox_Participants.SuspendLayout()
        Me.StatusStrip_STLNotice.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel_Main
        '
        Me.TableLayoutPanel_Main.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel_Main.ColumnCount = 1
        Me.TableLayoutPanel_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Main.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel_Main.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TableLayoutPanel_Main.Location = New System.Drawing.Point(10, 12)
        Me.TableLayoutPanel_Main.Name = "TableLayoutPanel_Main"
        Me.TableLayoutPanel_Main.RowCount = 1
        Me.TableLayoutPanel_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 339.0!))
        Me.TableLayoutPanel_Main.Size = New System.Drawing.Size(471, 341)
        Me.TableLayoutPanel_Main.TabIndex = 5
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btn_ExportToExcel)
        Me.Panel1.Controls.Add(Me.cbo_date)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.gbox_Participants)
        Me.Panel1.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(461, 331)
        Me.Panel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(141, 12)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Transaction Month (As Of) :"
        '
        'btn_ExportToExcel
        '
        Me.btn_ExportToExcel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_ExportToExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_ExportToExcel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_ExportToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ExportToExcel.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ExportToExcel.Image = Global.AccountsManagementForms.My.Resources.Resources.ExcelIcon22x22
        Me.btn_ExportToExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_ExportToExcel.Location = New System.Drawing.Point(261, 275)
        Me.btn_ExportToExcel.Name = "btn_ExportToExcel"
        Me.btn_ExportToExcel.Size = New System.Drawing.Size(188, 45)
        Me.btn_ExportToExcel.TabIndex = 8
        Me.btn_ExportToExcel.Text = "Export to Excel"
        Me.btn_ExportToExcel.UseVisualStyleBackColor = True
        '
        'cbo_date
        '
        Me.cbo_date.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_date.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_date.FormattingEnabled = True
        Me.cbo_date.Location = New System.Drawing.Point(13, 24)
        Me.cbo_date.Name = "cbo_date"
        Me.cbo_date.Size = New System.Drawing.Size(186, 20)
        Me.cbo_date.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txt_Note)
        Me.GroupBox1.Controls.Add(Me.chk_Note)
        Me.GroupBox1.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(247, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(202, 261)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        '
        'txt_Note
        '
        Me.txt_Note.BackColor = System.Drawing.Color.White
        Me.txt_Note.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Note.Location = New System.Drawing.Point(6, 39)
        Me.txt_Note.Multiline = True
        Me.txt_Note.Name = "txt_Note"
        Me.txt_Note.ReadOnly = True
        Me.txt_Note.Size = New System.Drawing.Size(188, 211)
        Me.txt_Note.TabIndex = 1
        '
        'chk_Note
        '
        Me.chk_Note.AutoSize = True
        Me.chk_Note.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_Note.Location = New System.Drawing.Point(6, 13)
        Me.chk_Note.Name = "chk_Note"
        Me.chk_Note.Size = New System.Drawing.Size(85, 16)
        Me.chk_Note.TabIndex = 0
        Me.chk_Note.Text = "Include Note"
        Me.chk_Note.UseVisualStyleBackColor = True
        '
        'gbox_Participants
        '
        Me.gbox_Participants.Controls.Add(Me.chkLB_Participants)
        Me.gbox_Participants.Controls.Add(Me.chkbox_SelectAll)
        Me.gbox_Participants.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbox_Participants.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.gbox_Participants.Location = New System.Drawing.Point(13, 61)
        Me.gbox_Participants.Name = "gbox_Participants"
        Me.gbox_Participants.Size = New System.Drawing.Size(214, 261)
        Me.gbox_Participants.TabIndex = 4
        Me.gbox_Participants.TabStop = False
        Me.gbox_Participants.Text = "Participant/s:"
        '
        'chkLB_Participants
        '
        Me.chkLB_Participants.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLB_Participants.FormattingEnabled = True
        Me.chkLB_Participants.Location = New System.Drawing.Point(5, 38)
        Me.chkLB_Participants.Name = "chkLB_Participants"
        Me.chkLB_Participants.Size = New System.Drawing.Size(203, 199)
        Me.chkLB_Participants.TabIndex = 2
        '
        'chkbox_SelectAll
        '
        Me.chkbox_SelectAll.AutoSize = True
        Me.chkbox_SelectAll.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbox_SelectAll.ForeColor = System.Drawing.Color.Black
        Me.chkbox_SelectAll.Location = New System.Drawing.Point(8, 17)
        Me.chkbox_SelectAll.Name = "chkbox_SelectAll"
        Me.chkbox_SelectAll.Size = New System.Drawing.Size(68, 16)
        Me.chkbox_SelectAll.TabIndex = 1
        Me.chkbox_SelectAll.Text = "Select All"
        Me.chkbox_SelectAll.UseVisualStyleBackColor = True
        '
        'StatusStrip_STLNotice
        '
        Me.StatusStrip_STLNotice.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel_Text, Me.ToolStripProgressBar_STLNotice, Me.ToolStripStatusLabel_Percent})
        Me.StatusStrip_STLNotice.Location = New System.Drawing.Point(0, 394)
        Me.StatusStrip_STLNotice.Name = "StatusStrip_STLNotice"
        Me.StatusStrip_STLNotice.Size = New System.Drawing.Size(493, 26)
        Me.StatusStrip_STLNotice.TabIndex = 6
        Me.StatusStrip_STLNotice.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(42, 21)
        Me.ToolStripStatusLabel1.Text = "Status:"
        '
        'ToolStripStatusLabel_Text
        '
        Me.ToolStripStatusLabel_Text.Name = "ToolStripStatusLabel_Text"
        Me.ToolStripStatusLabel_Text.Size = New System.Drawing.Size(291, 21)
        Me.ToolStripStatusLabel_Text.Spring = True
        Me.ToolStripStatusLabel_Text.Text = "..."
        Me.ToolStripStatusLabel_Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripProgressBar_STLNotice
        '
        Me.ToolStripProgressBar_STLNotice.Name = "ToolStripProgressBar_STLNotice"
        Me.ToolStripProgressBar_STLNotice.Size = New System.Drawing.Size(120, 20)
        '
        'ToolStripStatusLabel_Percent
        '
        Me.ToolStripStatusLabel_Percent.Name = "ToolStripStatusLabel_Percent"
        Me.ToolStripStatusLabel_Percent.Size = New System.Drawing.Size(23, 21)
        Me.ToolStripStatusLabel_Percent.Text = "0%"
        '
        'bgw_PaymentSummaryDetails
        '
        '
        'frmPaymentSummaryDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(493, 420)
        Me.Controls.Add(Me.StatusStrip_STLNotice)
        Me.Controls.Add(Me.TableLayoutPanel_Main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmPaymentSummaryDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Payment Summary Details"
        Me.TableLayoutPanel_Main.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gbox_Participants.ResumeLayout(False)
        Me.gbox_Participants.PerformLayout()
        Me.StatusStrip_STLNotice.ResumeLayout(False)
        Me.StatusStrip_STLNotice.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel_Main As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btn_ExportToExcel As System.Windows.Forms.Button
    Friend WithEvents cbo_date As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txt_Note As System.Windows.Forms.TextBox
    Friend WithEvents chk_Note As System.Windows.Forms.CheckBox
    Friend WithEvents gbox_Participants As System.Windows.Forms.GroupBox
    Friend WithEvents chkLB_Participants As System.Windows.Forms.CheckedListBox
    Friend WithEvents chkbox_SelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents StatusStrip_STLNotice As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel_Text As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar_STLNotice As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ToolStripStatusLabel_Percent As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents bgw_PaymentSummaryDetails As System.ComponentModel.BackgroundWorker
End Class
