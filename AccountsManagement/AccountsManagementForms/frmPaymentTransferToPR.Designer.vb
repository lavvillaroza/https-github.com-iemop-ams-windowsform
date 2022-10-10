<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymentTransferToPR
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.cmd_Transfer = New System.Windows.Forms.Button()
        Me.cmd_Close = New System.Windows.Forms.Button()
        Me.dgv_TransferToPR = New System.Windows.Forms.DataGridView()
        Me.hParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.hParticipantName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.hAllocatedPayment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.hAMTForPR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgv_TransferToPR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmd_Transfer
        '
        Me.cmd_Transfer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_Transfer.Image = Global.AccountsManagementForms.My.Resources.Resources.tab_decimal
        Me.cmd_Transfer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Transfer.Location = New System.Drawing.Point(413, 397)
        Me.cmd_Transfer.Name = "cmd_Transfer"
        Me.cmd_Transfer.Size = New System.Drawing.Size(138, 23)
        Me.cmd_Transfer.TabIndex = 0
        Me.cmd_Transfer.Text = "Save"
        Me.cmd_Transfer.UseVisualStyleBackColor = True
        '
        'cmd_Close
        '
        Me.cmd_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_Close.Image = Global.AccountsManagementForms.My.Resources.Resources.close
        Me.cmd_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Close.Location = New System.Drawing.Point(557, 397)
        Me.cmd_Close.Name = "cmd_Close"
        Me.cmd_Close.Size = New System.Drawing.Size(170, 23)
        Me.cmd_Close.TabIndex = 1
        Me.cmd_Close.Text = "Close"
        Me.cmd_Close.UseVisualStyleBackColor = True
        '
        'dgv_TransferToPR
        '
        Me.dgv_TransferToPR.AllowUserToAddRows = False
        Me.dgv_TransferToPR.AllowUserToDeleteRows = False
        Me.dgv_TransferToPR.AllowUserToOrderColumns = True
        Me.dgv_TransferToPR.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Cyan
        Me.dgv_TransferToPR.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_TransferToPR.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_TransferToPR.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_TransferToPR.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_TransferToPR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_TransferToPR.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.hParticipantID, Me.hParticipantName, Me.hAllocatedPayment, Me.hAMTForPR})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_TransferToPR.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgv_TransferToPR.Location = New System.Drawing.Point(12, 12)
        Me.dgv_TransferToPR.Name = "dgv_TransferToPR"
        Me.dgv_TransferToPR.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_TransferToPR.Size = New System.Drawing.Size(715, 379)
        Me.dgv_TransferToPR.TabIndex = 2
        '
        'hParticipantID
        '
        Me.hParticipantID.HeaderText = "IDNumber"
        Me.hParticipantID.Name = "hParticipantID"
        Me.hParticipantID.ReadOnly = True
        '
        'hParticipantName
        '
        Me.hParticipantName.HeaderText = "ParticipantID"
        Me.hParticipantName.Name = "hParticipantName"
        Me.hParticipantName.ReadOnly = True
        '
        'hAllocatedPayment
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "n2"
        DataGridViewCellStyle3.NullValue = "0"
        Me.hAllocatedPayment.DefaultCellStyle = DataGridViewCellStyle3
        Me.hAllocatedPayment.HeaderText = "AllocatedPayment"
        Me.hAllocatedPayment.Name = "hAllocatedPayment"
        Me.hAllocatedPayment.ReadOnly = True
        '
        'hAMTForPR
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "n2"
        DataGridViewCellStyle4.NullValue = "0.00"
        Me.hAMTForPR.DefaultCellStyle = DataGridViewCellStyle4
        Me.hAMTForPR.HeaderText = "TransferToPrudential"
        Me.hAMTForPR.Name = "hAMTForPR"
        '
        'frmPaymentTransferToPR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(742, 435)
        Me.ControlBox = False
        Me.Controls.Add(Me.dgv_TransferToPR)
        Me.Controls.Add(Me.cmd_Close)
        Me.Controls.Add(Me.cmd_Transfer)
        Me.MinimumSize = New System.Drawing.Size(487, 451)
        Me.Name = "frmPaymentTransferToPR"
        Me.Text = "Transfer Payment To Prudential"
        CType(Me.dgv_TransferToPR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmd_Transfer As System.Windows.Forms.Button
    Friend WithEvents cmd_Close As System.Windows.Forms.Button
    Friend WithEvents dgv_TransferToPR As System.Windows.Forms.DataGridView
    Friend WithEvents hParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hParticipantName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hAllocatedPayment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hAMTForPR As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
