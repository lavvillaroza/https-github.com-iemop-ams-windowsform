<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParticipantMaintenance
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParticipantMaintenance))
        Me.DGV_ParentParticipant = New System.Windows.Forms.DataGridView
        Me.pIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.pParticipantName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.pParticipantRemarks = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.pFlag = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GRP_Parent = New System.Windows.Forms.GroupBox
        Me.cmd_viewDetails = New System.Windows.Forms.Button
        Me.cmd_CopyToBillPd = New System.Windows.Forms.Button
        Me.cmd_ViewLogs = New System.Windows.Forms.Button
        Me.CMD_SetParent = New System.Windows.Forms.Button
        Me.CMD_SetChild = New System.Windows.Forms.Button
        Me.CMD_TransferParent = New System.Windows.Forms.Button
        Me.CMD_Close = New System.Windows.Forms.Button
        Me.GRP_Child = New System.Windows.Forms.GroupBox
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.TS_LBL_NewParticipants = New System.Windows.Forms.ToolStripStatusLabel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbo_BillPeriod = New System.Windows.Forms.ComboBox
        Me.RB_ActiveInactiveParticipants = New System.Windows.Forms.RadioButton
        Me.RB_ActiveParticipants = New System.Windows.Forms.RadioButton
        Me.PNL_Top = New System.Windows.Forms.Panel
        Me.PNL_Fill = New System.Windows.Forms.Panel
        CType(Me.DGV_ParentParticipant, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GRP_Parent.SuspendLayout()
        Me.GRP_Child.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.PNL_Top.SuspendLayout()
        Me.PNL_Fill.SuspendLayout()
        Me.SuspendLayout()
        '
        'DGV_ParentParticipant
        '
        Me.DGV_ParentParticipant.AllowUserToAddRows = False
        Me.DGV_ParentParticipant.AllowUserToDeleteRows = False
        Me.DGV_ParentParticipant.AllowUserToResizeRows = False
        Me.DGV_ParentParticipant.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_ParentParticipant.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DGV_ParentParticipant.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_ParentParticipant.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.pIDNumber, Me.pParticipantName, Me.pParticipantRemarks, Me.pFlag})
        Me.DGV_ParentParticipant.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGV_ParentParticipant.Location = New System.Drawing.Point(9, 16)
        Me.DGV_ParentParticipant.MultiSelect = False
        Me.DGV_ParentParticipant.Name = "DGV_ParentParticipant"
        Me.DGV_ParentParticipant.RowHeadersVisible = False
        Me.DGV_ParentParticipant.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGV_ParentParticipant.Size = New System.Drawing.Size(812, 345)
        Me.DGV_ParentParticipant.TabIndex = 0
        '
        'pIDNumber
        '
        Me.pIDNumber.HeaderText = "Id Number"
        Me.pIDNumber.Name = "pIDNumber"
        Me.pIDNumber.Width = 81
        '
        'pParticipantName
        '
        Me.pParticipantName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.pParticipantName.HeaderText = "Participant ID"
        Me.pParticipantName.Name = "pParticipantName"
        Me.pParticipantName.Width = 92
        '
        'pParticipantRemarks
        '
        Me.pParticipantRemarks.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.pParticipantRemarks.HeaderText = "Participant Name"
        Me.pParticipantRemarks.Name = "pParticipantRemarks"
        Me.pParticipantRemarks.Width = 108
        '
        'pFlag
        '
        Me.pFlag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.pFlag.HeaderText = "Flag"
        Me.pFlag.Name = "pFlag"
        Me.pFlag.Width = 50
        '
        'GRP_Parent
        '
        Me.GRP_Parent.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GRP_Parent.Controls.Add(Me.cmd_viewDetails)
        Me.GRP_Parent.Controls.Add(Me.cmd_CopyToBillPd)
        Me.GRP_Parent.Controls.Add(Me.cmd_ViewLogs)
        Me.GRP_Parent.Controls.Add(Me.DGV_ParentParticipant)
        Me.GRP_Parent.Controls.Add(Me.CMD_SetParent)
        Me.GRP_Parent.Controls.Add(Me.CMD_SetChild)
        Me.GRP_Parent.Controls.Add(Me.CMD_TransferParent)
        Me.GRP_Parent.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRP_Parent.Location = New System.Drawing.Point(3, 52)
        Me.GRP_Parent.Name = "GRP_Parent"
        Me.GRP_Parent.Size = New System.Drawing.Size(827, 394)
        Me.GRP_Parent.TabIndex = 3
        Me.GRP_Parent.TabStop = False
        Me.GRP_Parent.Text = "Participant List"
        '
        'cmd_viewDetails
        '
        Me.cmd_viewDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_viewDetails.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_viewDetails.Image = Global.AccountsManagementForms.My.Resources.Resources.report
        Me.cmd_viewDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_viewDetails.Location = New System.Drawing.Point(612, 364)
        Me.cmd_viewDetails.Name = "cmd_viewDetails"
        Me.cmd_viewDetails.Size = New System.Drawing.Size(109, 25)
        Me.cmd_viewDetails.TabIndex = 14
        Me.cmd_viewDetails.Text = "View Details"
        Me.cmd_viewDetails.UseVisualStyleBackColor = True
        '
        'cmd_CopyToBillPd
        '
        Me.cmd_CopyToBillPd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_CopyToBillPd.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_CopyToBillPd.Image = Global.AccountsManagementForms.My.Resources.Resources.contents
        Me.cmd_CopyToBillPd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_CopyToBillPd.Location = New System.Drawing.Point(451, 364)
        Me.cmd_CopyToBillPd.Name = "cmd_CopyToBillPd"
        Me.cmd_CopyToBillPd.Size = New System.Drawing.Size(155, 25)
        Me.cmd_CopyToBillPd.TabIndex = 13
        Me.cmd_CopyToBillPd.Text = "Copy to Billing Period"
        Me.cmd_CopyToBillPd.UseVisualStyleBackColor = True
        '
        'cmd_ViewLogs
        '
        Me.cmd_ViewLogs.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_ViewLogs.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ViewLogs.Image = Global.AccountsManagementForms.My.Resources.Resources.report
        Me.cmd_ViewLogs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_ViewLogs.Location = New System.Drawing.Point(727, 364)
        Me.cmd_ViewLogs.Name = "cmd_ViewLogs"
        Me.cmd_ViewLogs.Size = New System.Drawing.Size(97, 25)
        Me.cmd_ViewLogs.TabIndex = 11
        Me.cmd_ViewLogs.Text = "View Logs"
        Me.cmd_ViewLogs.UseVisualStyleBackColor = True
        '
        'CMD_SetParent
        '
        Me.CMD_SetParent.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CMD_SetParent.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMD_SetParent.Image = CType(resources.GetObject("CMD_SetParent.Image"), System.Drawing.Image)
        Me.CMD_SetParent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CMD_SetParent.Location = New System.Drawing.Point(79, 364)
        Me.CMD_SetParent.Name = "CMD_SetParent"
        Me.CMD_SetParent.Size = New System.Drawing.Size(118, 25)
        Me.CMD_SetParent.TabIndex = 8
        Me.CMD_SetParent.Text = "Set as Parent"
        Me.CMD_SetParent.UseVisualStyleBackColor = True
        '
        'CMD_SetChild
        '
        Me.CMD_SetChild.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CMD_SetChild.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMD_SetChild.Image = CType(resources.GetObject("CMD_SetChild.Image"), System.Drawing.Image)
        Me.CMD_SetChild.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CMD_SetChild.Location = New System.Drawing.Point(203, 364)
        Me.CMD_SetChild.Name = "CMD_SetChild"
        Me.CMD_SetChild.Size = New System.Drawing.Size(118, 25)
        Me.CMD_SetChild.TabIndex = 10
        Me.CMD_SetChild.Text = "Set as Child"
        Me.CMD_SetChild.UseVisualStyleBackColor = True
        '
        'CMD_TransferParent
        '
        Me.CMD_TransferParent.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CMD_TransferParent.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMD_TransferParent.Image = CType(resources.GetObject("CMD_TransferParent.Image"), System.Drawing.Image)
        Me.CMD_TransferParent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CMD_TransferParent.Location = New System.Drawing.Point(327, 364)
        Me.CMD_TransferParent.Name = "CMD_TransferParent"
        Me.CMD_TransferParent.Size = New System.Drawing.Size(118, 25)
        Me.CMD_TransferParent.TabIndex = 9
        Me.CMD_TransferParent.Text = "Transfer Parent"
        Me.CMD_TransferParent.UseVisualStyleBackColor = True
        '
        'CMD_Close
        '
        Me.CMD_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CMD_Close.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMD_Close.Image = Global.AccountsManagementForms.My.Resources.Resources.close
        Me.CMD_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CMD_Close.Location = New System.Drawing.Point(739, 14)
        Me.CMD_Close.Name = "CMD_Close"
        Me.CMD_Close.Size = New System.Drawing.Size(85, 26)
        Me.CMD_Close.TabIndex = 12
        Me.CMD_Close.Text = "Close"
        Me.CMD_Close.UseVisualStyleBackColor = True
        '
        'GRP_Child
        '
        Me.GRP_Child.Controls.Add(Me.StatusStrip1)
        Me.GRP_Child.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GRP_Child.Location = New System.Drawing.Point(0, 462)
        Me.GRP_Child.Name = "GRP_Child"
        Me.GRP_Child.Size = New System.Drawing.Size(836, 26)
        Me.GRP_Child.TabIndex = 4
        Me.GRP_Child.TabStop = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TS_LBL_NewParticipants})
        Me.StatusStrip1.Location = New System.Drawing.Point(3, 1)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(830, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'TS_LBL_NewParticipants
        '
        Me.TS_LBL_NewParticipants.Name = "TS_LBL_NewParticipants"
        Me.TS_LBL_NewParticipants.Size = New System.Drawing.Size(0, 17)
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.CMD_Close)
        Me.GroupBox1.Controls.Add(Me.cbo_BillPeriod)
        Me.GroupBox1.Controls.Add(Me.RB_ActiveInactiveParticipants)
        Me.GroupBox1.Controls.Add(Me.RB_ActiveParticipants)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(833, 46)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "View Options"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = Global.AccountsManagementForms.My.Resources.Resources.refresh
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(615, 14)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(118, 26)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "   Refresh"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(402, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 15)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Billing Period"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbo_BillPeriod
        '
        Me.cbo_BillPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_BillPeriod.FormattingEnabled = True
        Me.cbo_BillPeriod.Location = New System.Drawing.Point(481, 17)
        Me.cbo_BillPeriod.Name = "cbo_BillPeriod"
        Me.cbo_BillPeriod.Size = New System.Drawing.Size(118, 23)
        Me.cbo_BillPeriod.TabIndex = 10
        '
        'RB_ActiveInactiveParticipants
        '
        Me.RB_ActiveInactiveParticipants.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RB_ActiveInactiveParticipants.AutoSize = True
        Me.RB_ActiveInactiveParticipants.Location = New System.Drawing.Point(224, 19)
        Me.RB_ActiveInactiveParticipants.Name = "RB_ActiveInactiveParticipants"
        Me.RB_ActiveInactiveParticipants.Size = New System.Drawing.Size(154, 19)
        Me.RB_ActiveInactiveParticipants.TabIndex = 9
        Me.RB_ActiveInactiveParticipants.TabStop = True
        Me.RB_ActiveInactiveParticipants.Text = "Active/Inactive Participants"
        Me.RB_ActiveInactiveParticipants.UseVisualStyleBackColor = True
        '
        'RB_ActiveParticipants
        '
        Me.RB_ActiveParticipants.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RB_ActiveParticipants.AutoSize = True
        Me.RB_ActiveParticipants.Location = New System.Drawing.Point(64, 19)
        Me.RB_ActiveParticipants.Name = "RB_ActiveParticipants"
        Me.RB_ActiveParticipants.Size = New System.Drawing.Size(114, 19)
        Me.RB_ActiveParticipants.TabIndex = 8
        Me.RB_ActiveParticipants.TabStop = True
        Me.RB_ActiveParticipants.Text = "Active Participants"
        Me.RB_ActiveParticipants.UseVisualStyleBackColor = True
        '
        'PNL_Top
        '
        Me.PNL_Top.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PNL_Top.Controls.Add(Me.GroupBox1)
        Me.PNL_Top.Controls.Add(Me.GRP_Parent)
        Me.PNL_Top.Location = New System.Drawing.Point(0, 0)
        Me.PNL_Top.Name = "PNL_Top"
        Me.PNL_Top.Size = New System.Drawing.Size(833, 460)
        Me.PNL_Top.TabIndex = 11
        '
        'PNL_Fill
        '
        Me.PNL_Fill.Controls.Add(Me.GRP_Child)
        Me.PNL_Fill.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PNL_Fill.Location = New System.Drawing.Point(0, 0)
        Me.PNL_Fill.Name = "PNL_Fill"
        Me.PNL_Fill.Size = New System.Drawing.Size(836, 488)
        Me.PNL_Fill.TabIndex = 12
        '
        'frmParticipantMaintenance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(836, 488)
        Me.Controls.Add(Me.PNL_Top)
        Me.Controls.Add(Me.PNL_Fill)
        Me.MinimumSize = New System.Drawing.Size(852, 522)
        Me.Name = "frmParticipantMaintenance"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Participant Mapping"
        CType(Me.DGV_ParentParticipant, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GRP_Parent.ResumeLayout(False)
        Me.GRP_Child.ResumeLayout(False)
        Me.GRP_Child.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.PNL_Top.ResumeLayout(False)
        Me.PNL_Fill.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGV_ParentParticipant As System.Windows.Forms.DataGridView
    Friend WithEvents GRP_Parent As System.Windows.Forms.GroupBox
    Friend WithEvents GRP_Child As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RB_ActiveInactiveParticipants As System.Windows.Forms.RadioButton
    Friend WithEvents RB_ActiveParticipants As System.Windows.Forms.RadioButton
    Friend WithEvents CMD_SetParent As System.Windows.Forms.Button
    Friend WithEvents CMD_TransferParent As System.Windows.Forms.Button
    Friend WithEvents CMD_SetChild As System.Windows.Forms.Button
    Friend WithEvents PNL_Top As System.Windows.Forms.Panel
    Friend WithEvents PNL_Fill As System.Windows.Forms.Panel
    Friend WithEvents cmd_ViewLogs As System.Windows.Forms.Button
    Friend WithEvents CMD_Close As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbo_BillPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents TS_LBL_NewParticipants As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents cmd_CopyToBillPd As System.Windows.Forms.Button
    Friend WithEvents cmd_viewDetails As System.Windows.Forms.Button
    Friend WithEvents pIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pParticipantName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pParticipantRemarks As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pFlag As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
