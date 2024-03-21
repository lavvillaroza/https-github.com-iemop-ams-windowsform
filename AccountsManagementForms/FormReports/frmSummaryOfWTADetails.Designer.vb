<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSummaryOfWTADetails
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
        Me.ctrl_statusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabelCR = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TableLayoutPanel_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ddlYear_cmb = New System.Windows.Forms.ComboBox()
        Me.chkbox_ZeroBalance = New System.Windows.Forms.CheckBox()
        Me.chkbox_SelectAll = New System.Windows.Forms.CheckBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btn_ExportToExcel = New System.Windows.Forms.Button()
        Me.gbox_Participants = New System.Windows.Forms.GroupBox()
        Me.chkLB_Participants = New System.Windows.Forms.CheckedListBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ctrl_statusStrip.SuspendLayout()
        Me.TableLayoutPanel_Main.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.gbox_Participants.SuspendLayout()
        Me.SuspendLayout()
        '
        'ctrl_statusStrip
        '
        Me.ctrl_statusStrip.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ctrl_statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabelCR})
        Me.ctrl_statusStrip.Location = New System.Drawing.Point(0, 450)
        Me.ctrl_statusStrip.Name = "ctrl_statusStrip"
        Me.ctrl_statusStrip.Size = New System.Drawing.Size(332, 22)
        Me.ctrl_statusStrip.TabIndex = 9
        Me.ctrl_statusStrip.Text = "StatusStrip1"
        '
        'ToolStripStatusLabelCR
        '
        Me.ToolStripStatusLabelCR.Name = "ToolStripStatusLabelCR"
        Me.ToolStripStatusLabelCR.Size = New System.Drawing.Size(48, 17)
        Me.ToolStripStatusLabelCR.Text = "Ready..."
        '
        'TableLayoutPanel_Main
        '
        Me.TableLayoutPanel_Main.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel_Main.ColumnCount = 1
        Me.TableLayoutPanel_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Main.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel_Main.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TableLayoutPanel_Main.Location = New System.Drawing.Point(0, 1)
        Me.TableLayoutPanel_Main.Name = "TableLayoutPanel_Main"
        Me.TableLayoutPanel_Main.RowCount = 1
        Me.TableLayoutPanel_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 448.0!))
        Me.TableLayoutPanel_Main.Size = New System.Drawing.Size(331, 447)
        Me.TableLayoutPanel_Main.TabIndex = 10
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.ddlYear_cmb)
        Me.Panel1.Controls.Add(Me.chkbox_ZeroBalance)
        Me.Panel1.Controls.Add(Me.chkbox_SelectAll)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btn_ExportToExcel)
        Me.Panel1.Controls.Add(Me.gbox_Participants)
        Me.Panel1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(321, 437)
        Me.Panel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(6, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 15)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Transaction Year:"
        '
        'ddlYear_cmb
        '
        Me.ddlYear_cmb.BackColor = System.Drawing.SystemColors.Window
        Me.ddlYear_cmb.FormattingEnabled = True
        Me.ddlYear_cmb.Location = New System.Drawing.Point(16, 30)
        Me.ddlYear_cmb.Name = "ddlYear_cmb"
        Me.ddlYear_cmb.Size = New System.Drawing.Size(122, 23)
        Me.ddlYear_cmb.TabIndex = 29
        '
        'chkbox_ZeroBalance
        '
        Me.chkbox_ZeroBalance.AutoSize = True
        Me.chkbox_ZeroBalance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbox_ZeroBalance.ForeColor = System.Drawing.Color.Black
        Me.chkbox_ZeroBalance.Location = New System.Drawing.Point(16, 63)
        Me.chkbox_ZeroBalance.Name = "chkbox_ZeroBalance"
        Me.chkbox_ZeroBalance.Size = New System.Drawing.Size(128, 18)
        Me.chkbox_ZeroBalance.TabIndex = 28
        Me.chkbox_ZeroBalance.Text = "Include Zero Balance"
        Me.chkbox_ZeroBalance.UseVisualStyleBackColor = True
        '
        'chkbox_SelectAll
        '
        Me.chkbox_SelectAll.AutoSize = True
        Me.chkbox_SelectAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbox_SelectAll.ForeColor = System.Drawing.Color.Black
        Me.chkbox_SelectAll.Location = New System.Drawing.Point(239, 65)
        Me.chkbox_SelectAll.Name = "chkbox_SelectAll"
        Me.chkbox_SelectAll.Size = New System.Drawing.Size(70, 18)
        Me.chkbox_SelectAll.TabIndex = 1
        Me.chkbox_SelectAll.Text = "Select All"
        Me.chkbox_SelectAll.UseVisualStyleBackColor = True
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
        Me.btnClose.Location = New System.Drawing.Point(163, 391)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(146, 39)
        Me.btnClose.TabIndex = 27
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btn_ExportToExcel
        '
        Me.btn_ExportToExcel.BackColor = System.Drawing.Color.White
        Me.btn_ExportToExcel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_ExportToExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_ExportToExcel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_ExportToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ExportToExcel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ExportToExcel.Image = Global.AccountsManagementForms.My.Resources.Resources.ExcelIcon22x22
        Me.btn_ExportToExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_ExportToExcel.Location = New System.Drawing.Point(11, 391)
        Me.btn_ExportToExcel.Name = "btn_ExportToExcel"
        Me.btn_ExportToExcel.Size = New System.Drawing.Size(146, 39)
        Me.btn_ExportToExcel.TabIndex = 16
        Me.btn_ExportToExcel.Text = "Export to Excel"
        Me.btn_ExportToExcel.UseVisualStyleBackColor = False
        '
        'gbox_Participants
        '
        Me.gbox_Participants.Controls.Add(Me.chkLB_Participants)
        Me.gbox_Participants.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbox_Participants.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.gbox_Participants.Location = New System.Drawing.Point(11, 86)
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
        'frmSummaryOfWTADetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(332, 472)
        Me.Controls.Add(Me.TableLayoutPanel_Main)
        Me.Controls.Add(Me.ctrl_statusStrip)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSummaryOfWTADetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Summary Of WTA Details Per Participant"
        Me.ctrl_statusStrip.ResumeLayout(False)
        Me.ctrl_statusStrip.PerformLayout()
        Me.TableLayoutPanel_Main.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.gbox_Participants.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ctrl_statusStrip As StatusStrip
    Friend WithEvents ToolStripStatusLabelCR As ToolStripStatusLabel
    Friend WithEvents TableLayoutPanel_Main As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents chkbox_SelectAll As CheckBox
    Friend WithEvents btnClose As Button
    Friend WithEvents btn_ExportToExcel As Button
    Friend WithEvents gbox_Participants As GroupBox
    Friend WithEvents chkLB_Participants As CheckedListBox
    Friend WithEvents chkbox_ZeroBalance As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ddlYear_cmb As ComboBox
    Friend WithEvents Timer1 As Timer
End Class
