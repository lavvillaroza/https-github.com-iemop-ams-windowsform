<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransferAdvancePayment
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
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DGridView = New System.Windows.Forms.DataGridView()
        Me.colColNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colORNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAutoHeld = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUnallocated = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colExcessCollection = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colManualHeld = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPrudential = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBPIAccount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnTransfer = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        CType(Me.DGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGridView
        '
        Me.DGridView.AllowUserToAddRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colColNo, Me.colORNo, Me.colIDNumber, Me.colParticipantID, Me.colAutoHeld, Me.colUnallocated, Me.colExcessCollection, Me.colManualHeld, Me.colPrudential, Me.colBPIAccount})
        Me.DGridView.Location = New System.Drawing.Point(12, 12)
        Me.DGridView.Name = "DGridView"
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGridView.RowsDefaultCellStyle = DataGridViewCellStyle8
        Me.DGridView.Size = New System.Drawing.Size(983, 390)
        Me.DGridView.TabIndex = 0
        '
        'colColNo
        '
        Me.colColNo.HeaderText = "ColNo"
        Me.colColNo.Name = "colColNo"
        Me.colColNo.ReadOnly = True
        Me.colColNo.Visible = False
        '
        'colORNo
        '
        Me.colORNo.HeaderText = "ORNo"
        Me.colORNo.Name = "colORNo"
        Me.colORNo.ReadOnly = True
        '
        'colIDNumber
        '
        Me.colIDNumber.HeaderText = "IDNumber"
        Me.colIDNumber.Name = "colIDNumber"
        Me.colIDNumber.ReadOnly = True
        Me.colIDNumber.Width = 80
        '
        'colParticipantID
        '
        Me.colParticipantID.HeaderText = "ParticipantID"
        Me.colParticipantID.Name = "colParticipantID"
        Me.colParticipantID.ReadOnly = True
        '
        'colAutoHeld
        '
        DataGridViewCellStyle2.Format = "N2"
        Me.colAutoHeld.DefaultCellStyle = DataGridViewCellStyle2
        Me.colAutoHeld.HeaderText = "HeldCollection"
        Me.colAutoHeld.Name = "colAutoHeld"
        '
        'colUnallocated
        '
        DataGridViewCellStyle3.Format = "N2"
        Me.colUnallocated.DefaultCellStyle = DataGridViewCellStyle3
        Me.colUnallocated.HeaderText = "UnallocatedAmount"
        Me.colUnallocated.Name = "colUnallocated"
        Me.colUnallocated.ReadOnly = True
        '
        'colExcessCollection
        '
        DataGridViewCellStyle4.Format = "N2"
        Me.colExcessCollection.DefaultCellStyle = DataGridViewCellStyle4
        Me.colExcessCollection.HeaderText = "ExcessCollection"
        Me.colExcessCollection.Name = "colExcessCollection"
        Me.colExcessCollection.Width = 165
        '
        'colManualHeld
        '
        DataGridViewCellStyle5.Format = "N2"
        Me.colManualHeld.DefaultCellStyle = DataGridViewCellStyle5
        Me.colManualHeld.HeaderText = "HeldCollection"
        Me.colManualHeld.Name = "colManualHeld"
        '
        'colPrudential
        '
        DataGridViewCellStyle6.Format = "N2"
        Me.colPrudential.DefaultCellStyle = DataGridViewCellStyle6
        Me.colPrudential.HeaderText = "Prudential"
        Me.colPrudential.Name = "colPrudential"
        Me.colPrudential.Width = 165
        '
        'colBPIAccount
        '
        DataGridViewCellStyle7.Format = "N2"
        Me.colBPIAccount.DefaultCellStyle = DataGridViewCellStyle7
        Me.colBPIAccount.HeaderText = "PEMCAccount"
        Me.colBPIAccount.Name = "colBPIAccount"
        Me.colBPIAccount.Width = 165
        '
        'btnTransfer
        '
        Me.btnTransfer.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnTransfer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnTransfer.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTransfer.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransfer.ForeColor = System.Drawing.Color.Black
        Me.btnTransfer.Image = Global.AccountsManagementForms.My.Resources.Resources.RefreshOrangeIcon22x22
        Me.btnTransfer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTransfer.Location = New System.Drawing.Point(769, 407)
        Me.btnTransfer.Name = "btnTransfer"
        Me.btnTransfer.Size = New System.Drawing.Size(110, 39)
        Me.btnTransfer.TabIndex = 1
        Me.btnTransfer.Text = "&Transfer"
        Me.btnTransfer.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.Black
        Me.btnCancel.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(885, 407)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(110, 39)
        Me.btnCancel.TabIndex = 22
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'frmTransferAdvancePayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1007, 460)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnTransfer)
        Me.Controls.Add(Me.DGridView)
        Me.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmTransferAdvancePayment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Transfer Not Allocated Collection"
        CType(Me.DGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGridView As System.Windows.Forms.DataGridView
    Friend WithEvents btnTransfer As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents colColNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colORNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAutoHeld As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUnallocated As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colExcessCollection As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colManualHeld As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPrudential As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBPIAccount As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
