<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParticipantMaintenanceLogs
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.CMD_Close = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.LBL_Participant = New System.Windows.Forms.Label
        Me.DGV_Logs = New System.Windows.Forms.DataGridView
        Me.hBillPeriod = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hParentID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Remarks = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.UpdatedDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.UpdatedBy = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.DGV_Logs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CMD_Close
        '
        Me.CMD_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CMD_Close.Location = New System.Drawing.Point(421, 421)
        Me.CMD_Close.Name = "CMD_Close"
        Me.CMD_Close.Size = New System.Drawing.Size(117, 32)
        Me.CMD_Close.TabIndex = 0
        Me.CMD_Close.Text = "Close"
        Me.CMD_Close.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "History of:"
        '
        'LBL_Participant
        '
        Me.LBL_Participant.AutoSize = True
        Me.LBL_Participant.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL_Participant.Location = New System.Drawing.Point(89, 9)
        Me.LBL_Participant.Name = "LBL_Participant"
        Me.LBL_Participant.Size = New System.Drawing.Size(21, 15)
        Me.LBL_Participant.TabIndex = 2
        Me.LBL_Participant.Text = "<>"
        '
        'DGV_Logs
        '
        Me.DGV_Logs.AllowUserToAddRows = False
        Me.DGV_Logs.AllowUserToDeleteRows = False
        Me.DGV_Logs.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGV_Logs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGV_Logs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV_Logs.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGV_Logs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_Logs.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.hBillPeriod, Me.hParentID, Me.Remarks, Me.UpdatedDate, Me.UpdatedBy})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGV_Logs.DefaultCellStyle = DataGridViewCellStyle3
        Me.DGV_Logs.Location = New System.Drawing.Point(15, 39)
        Me.DGV_Logs.Name = "DGV_Logs"
        Me.DGV_Logs.ReadOnly = True
        Me.DGV_Logs.RowHeadersVisible = False
        Me.DGV_Logs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGV_Logs.Size = New System.Drawing.Size(523, 376)
        Me.DGV_Logs.TabIndex = 3
        '
        'hBillPeriod
        '
        Me.hBillPeriod.HeaderText = "BillingPeriod"
        Me.hBillPeriod.Name = "hBillPeriod"
        Me.hBillPeriod.ReadOnly = True
        '
        'hParentID
        '
        Me.hParentID.HeaderText = "Parent ID"
        Me.hParentID.Name = "hParentID"
        Me.hParentID.ReadOnly = True
        Me.hParentID.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'Remarks
        '
        Me.Remarks.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Remarks.HeaderText = "Remarks"
        Me.Remarks.Name = "Remarks"
        Me.Remarks.ReadOnly = True
        Me.Remarks.Width = 74
        '
        'UpdatedDate
        '
        Me.UpdatedDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.UpdatedDate.HeaderText = "DateUpdated"
        Me.UpdatedDate.Name = "UpdatedDate"
        Me.UpdatedDate.ReadOnly = True
        Me.UpdatedDate.Width = 94
        '
        'UpdatedBy
        '
        Me.UpdatedBy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.UpdatedBy.HeaderText = "UpdatedBy"
        Me.UpdatedBy.Name = "UpdatedBy"
        Me.UpdatedBy.ReadOnly = True
        Me.UpdatedBy.Width = 84
        '
        'frmParticipantMaintenanceLogs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(552, 465)
        Me.Controls.Add(Me.DGV_Logs)
        Me.Controls.Add(Me.LBL_Participant)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CMD_Close)
        Me.MinimumSize = New System.Drawing.Size(568, 503)
        Me.Name = "frmParticipantMaintenanceLogs"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Participant Mapping"
        CType(Me.DGV_Logs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CMD_Close As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LBL_Participant As System.Windows.Forms.Label
    Friend WithEvents DGV_Logs As System.Windows.Forms.DataGridView
    Friend WithEvents hBillPeriod As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hParentID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Remarks As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UpdatedDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UpdatedBy As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
