<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrudentialRefund
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DGridViewPrudential = New System.Windows.Forms.DataGridView()
        Me.colTransDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParticipantName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPRAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColInterestAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAmountRefund = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnJVEFT = New System.Windows.Forms.Button()
        Me.BtnJVClosing = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnInputRefund = New System.Windows.Forms.Button()
        Me.btnJVSetup = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnGenerateFTF = New System.Windows.Forms.Button()
        CType(Me.DGridViewPrudential, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGridViewPrudential
        '
        Me.DGridViewPrudential.AllowUserToAddRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridViewPrudential.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGridViewPrudential.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridViewPrudential.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colTransDate, Me.colIDNumber, Me.colParticipantID, Me.colParticipantName, Me.colPRAmount, Me.ColInterestAmount, Me.colAmountRefund})
        Me.DGridViewPrudential.Location = New System.Drawing.Point(212, 15)
        Me.DGridViewPrudential.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGridViewPrudential.MultiSelect = False
        Me.DGridViewPrudential.Name = "DGridViewPrudential"
        Me.DGridViewPrudential.RowHeadersWidth = 20
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGridViewPrudential.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.DGridViewPrudential.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DGridViewPrudential.Size = New System.Drawing.Size(883, 321)
        Me.DGridViewPrudential.TabIndex = 57
        '
        'colTransDate
        '
        Me.colTransDate.HeaderText = "TransactionDate"
        Me.colTransDate.Name = "colTransDate"
        Me.colTransDate.ReadOnly = True
        '
        'colIDNumber
        '
        Me.colIDNumber.HeaderText = "IDNumber"
        Me.colIDNumber.Name = "colIDNumber"
        Me.colIDNumber.ReadOnly = True
        '
        'colParticipantID
        '
        Me.colParticipantID.HeaderText = "ParticipantID"
        Me.colParticipantID.Name = "colParticipantID"
        Me.colParticipantID.ReadOnly = True
        '
        'colParticipantName
        '
        Me.colParticipantName.HeaderText = "Name"
        Me.colParticipantName.Name = "colParticipantName"
        Me.colParticipantName.ReadOnly = True
        Me.colParticipantName.Width = 220
        '
        'colPRAmount
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colPRAmount.DefaultCellStyle = DataGridViewCellStyle2
        Me.colPRAmount.HeaderText = "PR Amount"
        Me.colPRAmount.Name = "colPRAmount"
        Me.colPRAmount.ReadOnly = True
        '
        'ColInterestAmount
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ColInterestAmount.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColInterestAmount.HeaderText = "Interest Amount"
        Me.ColInterestAmount.Name = "ColInterestAmount"
        Me.ColInterestAmount.Width = 110
        '
        'colAmountRefund
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colAmountRefund.DefaultCellStyle = DataGridViewCellStyle4
        Me.colAmountRefund.HeaderText = "Amount Refund"
        Me.colAmountRefund.Name = "colAmountRefund"
        Me.colAmountRefund.Width = 130
        '
        'BtnJVEFT
        '
        Me.BtnJVEFT.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.BtnJVEFT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.BtnJVEFT.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.BtnJVEFT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnJVEFT.ForeColor = System.Drawing.Color.Black
        Me.BtnJVEFT.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.BtnJVEFT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnJVEFT.Location = New System.Drawing.Point(13, 154)
        Me.BtnJVEFT.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BtnJVEFT.Name = "BtnJVEFT"
        Me.BtnJVEFT.Size = New System.Drawing.Size(194, 42)
        Me.BtnJVEFT.TabIndex = 70
        Me.BtnJVEFT.Text = "      Journal Voucher EFT (Draft)"
        Me.BtnJVEFT.UseVisualStyleBackColor = True
        '
        'BtnJVClosing
        '
        Me.BtnJVClosing.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.BtnJVClosing.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.BtnJVClosing.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.BtnJVClosing.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnJVClosing.ForeColor = System.Drawing.Color.Black
        Me.BtnJVClosing.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.BtnJVClosing.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnJVClosing.Location = New System.Drawing.Point(12, 108)
        Me.BtnJVClosing.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BtnJVClosing.Name = "BtnJVClosing"
        Me.BtnJVClosing.Size = New System.Drawing.Size(194, 42)
        Me.BtnJVClosing.TabIndex = 69
        Me.BtnJVClosing.Text = "      Journal Voucher Closing (Draft)"
        Me.BtnJVClosing.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(13, 294)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(194, 42)
        Me.btnClose.TabIndex = 68
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnInputRefund
        '
        Me.btnInputRefund.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnInputRefund.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnInputRefund.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnInputRefund.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInputRefund.ForeColor = System.Drawing.Color.Black
        Me.btnInputRefund.Image = Global.AccountsManagementForms.My.Resources.Resources.EditDocumentColored22x22
        Me.btnInputRefund.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnInputRefund.Location = New System.Drawing.Point(13, 15)
        Me.btnInputRefund.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnInputRefund.Name = "btnInputRefund"
        Me.btnInputRefund.Size = New System.Drawing.Size(194, 42)
        Me.btnInputRefund.TabIndex = 61
        Me.btnInputRefund.Text = "       &Input Refund"
        Me.btnInputRefund.UseVisualStyleBackColor = True
        '
        'btnJVSetup
        '
        Me.btnJVSetup.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnJVSetup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnJVSetup.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnJVSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnJVSetup.ForeColor = System.Drawing.Color.Black
        Me.btnJVSetup.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.btnJVSetup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnJVSetup.Location = New System.Drawing.Point(13, 61)
        Me.btnJVSetup.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnJVSetup.Name = "btnJVSetup"
        Me.btnJVSetup.Size = New System.Drawing.Size(194, 42)
        Me.btnJVSetup.TabIndex = 64
        Me.btnJVSetup.Text = "      Journal Voucher Setup (Draft)"
        Me.btnJVSetup.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Image = Global.AccountsManagementForms.My.Resources.Resources.SaveIconColored22x22
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(13, 247)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(194, 42)
        Me.btnSave.TabIndex = 63
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnGenerateFTF
        '
        Me.btnGenerateFTF.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnGenerateFTF.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnGenerateFTF.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnGenerateFTF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerateFTF.ForeColor = System.Drawing.Color.Black
        Me.btnGenerateFTF.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.btnGenerateFTF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerateFTF.Location = New System.Drawing.Point(12, 200)
        Me.btnGenerateFTF.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnGenerateFTF.Name = "btnGenerateFTF"
        Me.btnGenerateFTF.Size = New System.Drawing.Size(194, 42)
        Me.btnGenerateFTF.TabIndex = 72
        Me.btnGenerateFTF.Text = "      Generate FTF (Draft)"
        Me.btnGenerateFTF.UseVisualStyleBackColor = True
        '
        'frmPrudentialRefund
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(1105, 351)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnGenerateFTF)
        Me.Controls.Add(Me.BtnJVEFT)
        Me.Controls.Add(Me.BtnJVClosing)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnInputRefund)
        Me.Controls.Add(Me.btnJVSetup)
        Me.Controls.Add(Me.DGridViewPrudential)
        Me.Controls.Add(Me.btnSave)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmPrudentialRefund"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Prudential Requirements Refund"
        CType(Me.DGridViewPrudential, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnInputRefund As System.Windows.Forms.Button
    Friend WithEvents btnJVSetup As System.Windows.Forms.Button
    Friend WithEvents DGridViewPrudential As System.Windows.Forms.DataGridView
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents BtnJVClosing As System.Windows.Forms.Button
    Friend WithEvents BtnJVEFT As System.Windows.Forms.Button
    Friend WithEvents btnGenerateFTF As System.Windows.Forms.Button
    Friend WithEvents colTransDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPRAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColInterestAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAmountRefund As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
