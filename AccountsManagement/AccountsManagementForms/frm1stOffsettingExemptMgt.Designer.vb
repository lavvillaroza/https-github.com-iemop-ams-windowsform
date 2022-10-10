<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddMappingMgt
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
        Me.btn_Close = New System.Windows.Forms.Button()
        Me.btn_Save = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmb_parentParticipant = New System.Windows.Forms.ComboBox()
        Me.cmb_childParticipant = New System.Windows.Forms.ComboBox()
        Me.cmb_chargeId = New System.Windows.Forms.ComboBox()
        Me.txtbox_ParentID = New System.Windows.Forms.TextBox()
        Me.txtbox_ChildID = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rd_Inactive = New System.Windows.Forms.RadioButton()
        Me.rd_active = New System.Windows.Forms.RadioButton()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btn_Close
        '
        Me.btn_Close.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btn_Close.BackColor = System.Drawing.Color.White
        Me.btn_Close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Close.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Close.ForeColor = System.Drawing.Color.Black
        Me.btn_Close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btn_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Close.Location = New System.Drawing.Point(323, 168)
        Me.btn_Close.Name = "btn_Close"
        Me.btn_Close.Size = New System.Drawing.Size(129, 42)
        Me.btn_Close.TabIndex = 12
        Me.btn_Close.Text = "&Close"
        Me.btn_Close.UseVisualStyleBackColor = False
        '
        'btn_Save
        '
        Me.btn_Save.BackColor = System.Drawing.Color.White
        Me.btn_Save.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Save.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Save.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Save.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Save.ForeColor = System.Drawing.Color.Black
        Me.btn_Save.Image = Global.AccountsManagementForms.My.Resources.Resources.SaveIconColored22x22
        Me.btn_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Save.Location = New System.Drawing.Point(192, 168)
        Me.btn_Save.Name = "btn_Save"
        Me.btn_Save.Size = New System.Drawing.Size(129, 42)
        Me.btn_Save.TabIndex = 11
        Me.btn_Save.Text = "Save"
        Me.btn_Save.UseVisualStyleBackColor = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 12)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 161.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(440, 150)
        Me.TableLayoutPanel1.TabIndex = 10
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Label5, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label6, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label7, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Label8, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.cmb_parentParticipant, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.cmb_childParticipant, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.cmb_chargeId, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.txtbox_ParentID, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.txtbox_ChildID, 2, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Panel1, 1, 3)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(4, 4)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 4
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(432, 142)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(3, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 14)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Participant ID:"
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(3, 45)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 14)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Child ID:"
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(3, 80)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(63, 14)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Charge ID:"
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(3, 116)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(45, 14)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Status:"
        '
        'cmb_parentParticipant
        '
        Me.cmb_parentParticipant.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmb_parentParticipant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmb_parentParticipant.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmb_parentParticipant.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmb_parentParticipant.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_parentParticipant.ForeColor = System.Drawing.Color.Black
        Me.cmb_parentParticipant.FormattingEnabled = True
        Me.cmb_parentParticipant.Location = New System.Drawing.Point(132, 7)
        Me.cmb_parentParticipant.Name = "cmb_parentParticipant"
        Me.cmb_parentParticipant.Size = New System.Drawing.Size(118, 20)
        Me.cmb_parentParticipant.TabIndex = 1
        '
        'cmb_childParticipant
        '
        Me.cmb_childParticipant.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmb_childParticipant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmb_childParticipant.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmb_childParticipant.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmb_childParticipant.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_childParticipant.ForeColor = System.Drawing.Color.Black
        Me.cmb_childParticipant.FormattingEnabled = True
        Me.cmb_childParticipant.ItemHeight = 12
        Me.cmb_childParticipant.Location = New System.Drawing.Point(132, 42)
        Me.cmb_childParticipant.Name = "cmb_childParticipant"
        Me.cmb_childParticipant.Size = New System.Drawing.Size(118, 20)
        Me.cmb_childParticipant.TabIndex = 2
        '
        'cmb_chargeId
        '
        Me.cmb_chargeId.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmb_chargeId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmb_chargeId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmb_chargeId.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmb_chargeId.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_chargeId.ForeColor = System.Drawing.Color.Black
        Me.cmb_chargeId.FormattingEnabled = True
        Me.cmb_chargeId.ItemHeight = 12
        Me.cmb_chargeId.Location = New System.Drawing.Point(132, 77)
        Me.cmb_chargeId.Name = "cmb_chargeId"
        Me.cmb_chargeId.Size = New System.Drawing.Size(118, 20)
        Me.cmb_chargeId.TabIndex = 3
        '
        'txtbox_ParentID
        '
        Me.txtbox_ParentID.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtbox_ParentID.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbox_ParentID.ForeColor = System.Drawing.Color.DimGray
        Me.txtbox_ParentID.Location = New System.Drawing.Point(261, 7)
        Me.txtbox_ParentID.Name = "txtbox_ParentID"
        Me.txtbox_ParentID.Size = New System.Drawing.Size(168, 20)
        Me.txtbox_ParentID.TabIndex = 2
        '
        'txtbox_ChildID
        '
        Me.txtbox_ChildID.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtbox_ChildID.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbox_ChildID.ForeColor = System.Drawing.Color.DimGray
        Me.txtbox_ChildID.Location = New System.Drawing.Point(261, 42)
        Me.txtbox_ChildID.Name = "txtbox_ChildID"
        Me.txtbox_ChildID.Size = New System.Drawing.Size(168, 20)
        Me.txtbox_ChildID.TabIndex = 4
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.rd_Inactive)
        Me.Panel1.Controls.Add(Me.rd_active)
        Me.Panel1.Location = New System.Drawing.Point(132, 108)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(123, 31)
        Me.Panel1.TabIndex = 15
        '
        'rd_Inactive
        '
        Me.rd_Inactive.AutoSize = True
        Me.rd_Inactive.Location = New System.Drawing.Point(58, 8)
        Me.rd_Inactive.Name = "rd_Inactive"
        Me.rd_Inactive.Size = New System.Drawing.Size(64, 17)
        Me.rd_Inactive.TabIndex = 1
        Me.rd_Inactive.TabStop = True
        Me.rd_Inactive.Text = "InActive"
        Me.rd_Inactive.UseVisualStyleBackColor = True
        '
        'rd_active
        '
        Me.rd_active.AutoSize = True
        Me.rd_active.Location = New System.Drawing.Point(3, 8)
        Me.rd_active.Name = "rd_active"
        Me.rd_active.Size = New System.Drawing.Size(55, 17)
        Me.rd_active.TabIndex = 0
        Me.rd_active.TabStop = True
        Me.rd_active.Text = "Active"
        Me.rd_active.UseVisualStyleBackColor = True
        '
        'frmAddMappingMgt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(464, 222)
        Me.Controls.Add(Me.btn_Close)
        Me.Controls.Add(Me.btn_Save)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddMappingMgt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Excemption"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btn_Close As System.Windows.Forms.Button
    Friend WithEvents btn_Save As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmb_parentParticipant As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_childParticipant As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_chargeId As System.Windows.Forms.ComboBox
    Friend WithEvents txtbox_ParentID As System.Windows.Forms.TextBox
    Friend WithEvents txtbox_ChildID As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rd_Inactive As System.Windows.Forms.RadioButton
    Friend WithEvents rd_active As System.Windows.Forms.RadioButton
End Class
