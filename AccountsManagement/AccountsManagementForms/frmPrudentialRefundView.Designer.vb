<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrudentialRefundView
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
        Me.btnGenerateFTF = New System.Windows.Forms.Button()
        Me.BtnJVEFT = New System.Windows.Forms.Button()
        Me.BtnJVClosing = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnJVSetup = New System.Windows.Forms.Button()
        Me.GB_Allocation = New System.Windows.Forms.GroupBox()
        Me.cbo_TransDate = New System.Windows.Forms.ComboBox()
        Me.btn_ViewTrans = New System.Windows.Forms.Button()
        Me.DGridViewPrudential = New System.Windows.Forms.DataGridView()
        Me.colTransDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParticipantName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPRAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColInterestAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAmountRefund = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GB_Allocation.SuspendLayout()
        CType(Me.DGridViewPrudential, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnGenerateFTF
        '
        Me.btnGenerateFTF.BackColor = System.Drawing.Color.White
        Me.btnGenerateFTF.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnGenerateFTF.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnGenerateFTF.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnGenerateFTF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerateFTF.ForeColor = System.Drawing.Color.Black
        Me.btnGenerateFTF.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.btnGenerateFTF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerateFTF.Location = New System.Drawing.Point(12, 264)
        Me.btnGenerateFTF.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnGenerateFTF.Name = "btnGenerateFTF"
        Me.btnGenerateFTF.Size = New System.Drawing.Size(203, 42)
        Me.btnGenerateFTF.TabIndex = 80
        Me.btnGenerateFTF.Text = "      Generate FTF"
        Me.btnGenerateFTF.UseVisualStyleBackColor = False
        '
        'BtnJVEFT
        '
        Me.BtnJVEFT.BackColor = System.Drawing.Color.White
        Me.BtnJVEFT.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.BtnJVEFT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.BtnJVEFT.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.BtnJVEFT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnJVEFT.ForeColor = System.Drawing.Color.Black
        Me.BtnJVEFT.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.BtnJVEFT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnJVEFT.Location = New System.Drawing.Point(13, 218)
        Me.BtnJVEFT.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BtnJVEFT.Name = "BtnJVEFT"
        Me.BtnJVEFT.Size = New System.Drawing.Size(203, 42)
        Me.BtnJVEFT.TabIndex = 79
        Me.BtnJVEFT.Text = "      Journal Voucher EFT"
        Me.BtnJVEFT.UseVisualStyleBackColor = False
        '
        'BtnJVClosing
        '
        Me.BtnJVClosing.BackColor = System.Drawing.Color.White
        Me.BtnJVClosing.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.BtnJVClosing.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.BtnJVClosing.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.BtnJVClosing.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnJVClosing.ForeColor = System.Drawing.Color.Black
        Me.BtnJVClosing.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.BtnJVClosing.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnJVClosing.Location = New System.Drawing.Point(12, 171)
        Me.BtnJVClosing.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BtnJVClosing.Name = "BtnJVClosing"
        Me.BtnJVClosing.Size = New System.Drawing.Size(203, 42)
        Me.BtnJVClosing.TabIndex = 78
        Me.BtnJVClosing.Text = "      Journal Voucher Closing"
        Me.BtnJVClosing.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.White
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(12, 311)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(203, 42)
        Me.btnClose.TabIndex = 77
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnJVSetup
        '
        Me.btnJVSetup.BackColor = System.Drawing.Color.White
        Me.btnJVSetup.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnJVSetup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnJVSetup.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnJVSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnJVSetup.ForeColor = System.Drawing.Color.Black
        Me.btnJVSetup.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.btnJVSetup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnJVSetup.Location = New System.Drawing.Point(13, 125)
        Me.btnJVSetup.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnJVSetup.Name = "btnJVSetup"
        Me.btnJVSetup.Size = New System.Drawing.Size(203, 42)
        Me.btnJVSetup.TabIndex = 76
        Me.btnJVSetup.Text = "      Journal Voucher Setup"
        Me.btnJVSetup.UseVisualStyleBackColor = False
        '
        'GB_Allocation
        '
        Me.GB_Allocation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GB_Allocation.Controls.Add(Me.cbo_TransDate)
        Me.GB_Allocation.Controls.Add(Me.btn_ViewTrans)
        Me.GB_Allocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GB_Allocation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GB_Allocation.Location = New System.Drawing.Point(12, 13)
        Me.GB_Allocation.Name = "GB_Allocation"
        Me.GB_Allocation.Size = New System.Drawing.Size(235, 107)
        Me.GB_Allocation.TabIndex = 81
        Me.GB_Allocation.TabStop = False
        Me.GB_Allocation.Text = "Select Transaction Date:"
        '
        'cbo_TransDate
        '
        Me.cbo_TransDate.BackColor = System.Drawing.Color.White
        Me.cbo_TransDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_TransDate.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cbo_TransDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_TransDate.ForeColor = System.Drawing.Color.DimGray
        Me.cbo_TransDate.FormattingEnabled = True
        Me.cbo_TransDate.Location = New System.Drawing.Point(9, 22)
        Me.cbo_TransDate.Name = "cbo_TransDate"
        Me.cbo_TransDate.Size = New System.Drawing.Size(187, 22)
        Me.cbo_TransDate.TabIndex = 49
        '
        'btn_ViewTrans
        '
        Me.btn_ViewTrans.BackColor = System.Drawing.Color.White
        Me.btn_ViewTrans.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_ViewTrans.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_ViewTrans.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_ViewTrans.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ViewTrans.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ViewTrans.ForeColor = System.Drawing.Color.Black
        Me.btn_ViewTrans.Image = Global.AccountsManagementForms.My.Resources.Resources.FileSearchIcon22x22
        Me.btn_ViewTrans.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_ViewTrans.Location = New System.Drawing.Point(8, 48)
        Me.btn_ViewTrans.Name = "btn_ViewTrans"
        Me.btn_ViewTrans.Size = New System.Drawing.Size(187, 42)
        Me.btn_ViewTrans.TabIndex = 50
        Me.btn_ViewTrans.Text = "View Refund"
        Me.btn_ViewTrans.UseVisualStyleBackColor = False
        '
        'DGridViewPrudential
        '
        Me.DGridViewPrudential.AllowUserToAddRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridViewPrudential.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGridViewPrudential.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridViewPrudential.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colTransDate, Me.colIDNumber, Me.colParticipantID, Me.colParticipantName, Me.colPRAmount, Me.ColInterestAmount, Me.colAmountRefund})
        Me.DGridViewPrudential.Location = New System.Drawing.Point(222, 12)
        Me.DGridViewPrudential.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGridViewPrudential.MultiSelect = False
        Me.DGridViewPrudential.Name = "DGridViewPrudential"
        Me.DGridViewPrudential.RowHeadersWidth = 20
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGridViewPrudential.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.DGridViewPrudential.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DGridViewPrudential.Size = New System.Drawing.Size(888, 407)
        Me.DGridViewPrudential.TabIndex = 82
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
        'frmPrudentialRefundView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1121, 434)
        Me.ControlBox = False
        Me.Controls.Add(Me.DGridViewPrudential)
        Me.Controls.Add(Me.GB_Allocation)
        Me.Controls.Add(Me.btnGenerateFTF)
        Me.Controls.Add(Me.BtnJVEFT)
        Me.Controls.Add(Me.BtnJVClosing)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnJVSetup)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmPrudentialRefundView"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Prudential Refund View"
        Me.GB_Allocation.ResumeLayout(False)
        CType(Me.DGridViewPrudential, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnGenerateFTF As System.Windows.Forms.Button
    Friend WithEvents BtnJVEFT As System.Windows.Forms.Button
    Friend WithEvents BtnJVClosing As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnJVSetup As System.Windows.Forms.Button
    Friend WithEvents GB_Allocation As System.Windows.Forms.GroupBox
    Friend WithEvents cbo_TransDate As System.Windows.Forms.ComboBox
    Friend WithEvents btn_ViewTrans As System.Windows.Forms.Button
    Friend WithEvents DGridViewPrudential As System.Windows.Forms.DataGridView
    Friend WithEvents colTransDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPRAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColInterestAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAmountRefund As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
