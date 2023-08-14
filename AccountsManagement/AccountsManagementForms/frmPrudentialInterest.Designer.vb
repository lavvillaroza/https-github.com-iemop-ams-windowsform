<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrudentialInterest
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnSummaryReport = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnPostToGP = New System.Windows.Forms.Button()
        Me.btnSearchInterest = New System.Windows.Forms.Button()
        Me.btnJV = New System.Windows.Forms.Button()
        Me.btnInterest = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.DGridViewPrudential = New System.Windows.Forms.DataGridView()
        Me.colTransDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParticipantName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colOR = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.colDMCMNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnSearch = New System.Windows.Forms.Button()
        CType(Me.DGridViewPrudential, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSummaryReport
        '
        Me.btnSummaryReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSummaryReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSummaryReport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSummaryReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSummaryReport.ForeColor = System.Drawing.Color.Black
        Me.btnSummaryReport.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.btnSummaryReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSummaryReport.Location = New System.Drawing.Point(18, 17)
        Me.btnSummaryReport.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSummaryReport.Name = "btnSummaryReport"
        Me.btnSummaryReport.Size = New System.Drawing.Size(169, 39)
        Me.btnSummaryReport.TabIndex = 49
        Me.btnSummaryReport.Text = "      &Summary Report (Draft)"
        Me.btnSummaryReport.UseVisualStyleBackColor = True
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
        Me.btnClose.Location = New System.Drawing.Point(968, 460)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(110, 39)
        Me.btnClose.TabIndex = 55
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnEdit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnEdit.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEdit.ForeColor = System.Drawing.Color.Black
        Me.btnEdit.Image = Global.AccountsManagementForms.My.Resources.Resources.Upload2Icon22x22
        Me.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEdit.Location = New System.Drawing.Point(494, 17)
        Me.btnEdit.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(110, 39)
        Me.btnEdit.TabIndex = 48
        Me.btnEdit.Text = "    &Modify"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnPostToGP
        '
        Me.btnPostToGP.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnPostToGP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnPostToGP.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnPostToGP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPostToGP.ForeColor = System.Drawing.Color.Black
        Me.btnPostToGP.Image = Global.AccountsManagementForms.My.Resources.Resources.PostIcon22x22
        Me.btnPostToGP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPostToGP.Location = New System.Drawing.Point(726, 460)
        Me.btnPostToGP.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnPostToGP.Name = "btnPostToGP"
        Me.btnPostToGP.Size = New System.Drawing.Size(120, 39)
        Me.btnPostToGP.TabIndex = 47
        Me.btnPostToGP.Text = "     &Post To GP"
        Me.btnPostToGP.UseVisualStyleBackColor = True
        '
        'btnSearchInterest
        '
        Me.btnSearchInterest.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSearchInterest.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSearchInterest.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSearchInterest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchInterest.ForeColor = System.Drawing.Color.Black
        Me.btnSearchInterest.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.btnSearchInterest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearchInterest.Location = New System.Drawing.Point(610, 17)
        Me.btnSearchInterest.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSearchInterest.Name = "btnSearchInterest"
        Me.btnSearchInterest.Size = New System.Drawing.Size(110, 39)
        Me.btnSearchInterest.TabIndex = 43
        Me.btnSearchInterest.Text = "     &Search"
        Me.btnSearchInterest.UseVisualStyleBackColor = True
        '
        'btnJV
        '
        Me.btnJV.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnJV.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnJV.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnJV.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnJV.ForeColor = System.Drawing.Color.Black
        Me.btnJV.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.btnJV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnJV.Location = New System.Drawing.Point(193, 17)
        Me.btnJV.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnJV.Name = "btnJV"
        Me.btnJV.Size = New System.Drawing.Size(169, 39)
        Me.btnJV.TabIndex = 46
        Me.btnJV.Text = "      &Journal Voucher (Draft)"
        Me.btnJV.UseVisualStyleBackColor = True
        '
        'btnInterest
        '
        Me.btnInterest.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnInterest.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnInterest.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnInterest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInterest.ForeColor = System.Drawing.Color.Black
        Me.btnInterest.Image = Global.AccountsManagementForms.My.Resources.Resources.EditDocumentColored22x22
        Me.btnInterest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnInterest.Location = New System.Drawing.Point(368, 17)
        Me.btnInterest.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnInterest.Name = "btnInterest"
        Me.btnInterest.Size = New System.Drawing.Size(120, 39)
        Me.btnInterest.TabIndex = 42
        Me.btnInterest.Text = "     &Input Interest"
        Me.btnInterest.UseVisualStyleBackColor = True
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
        Me.btnSave.Location = New System.Drawing.Point(852, 460)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(110, 39)
        Me.btnSave.TabIndex = 45
        Me.btnSave.Text = "    &Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(805, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 14)
        Me.Label5.TabIndex = 52
        Me.Label5.Text = "Participant ID:"
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(893, 36)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtSearch.MaxLength = 10
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(145, 20)
        Me.txtSearch.TabIndex = 50
        '
        'DGridViewPrudential
        '
        Me.DGridViewPrudential.AllowUserToAddRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridViewPrudential.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGridViewPrudential.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridViewPrudential.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colTransDate, Me.colIDNumber, Me.colParticipantID, Me.colParticipantName, Me.colAmount, Me.colOR, Me.colDMCMNo})
        Me.DGridViewPrudential.Location = New System.Drawing.Point(18, 69)
        Me.DGridViewPrudential.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGridViewPrudential.Name = "DGridViewPrudential"
        Me.DGridViewPrudential.RowHeadersWidth = 20
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGridViewPrudential.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DGridViewPrudential.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGridViewPrudential.Size = New System.Drawing.Size(1060, 382)
        Me.DGridViewPrudential.TabIndex = 49
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
        Me.colParticipantName.Width = 250
        '
        'colAmount
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        Me.colAmount.DefaultCellStyle = DataGridViewCellStyle2
        Me.colAmount.HeaderText = "Amount"
        Me.colAmount.Name = "colAmount"
        Me.colAmount.ReadOnly = True
        Me.colAmount.Width = 150
        '
        'colOR
        '
        Me.colOR.ActiveLinkColor = System.Drawing.Color.Blue
        Me.colOR.HeaderText = ""
        Me.colOR.Name = "colOR"
        Me.colOR.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colOR.Text = "&View DMCM"
        Me.colOR.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'colDMCMNo
        '
        Me.colDMCMNo.HeaderText = "DMCNumber"
        Me.colDMCMNo.Name = "colDMCMNo"
        Me.colDMCMNo.ReadOnly = True
        Me.colDMCMNo.Visible = False
        '
        'btnSearch
        '
        Me.btnSearch.AccessibleName = ""
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.BackgroundImage = Global.AccountsManagementForms.My.Resources.Resources.SearchIconColored22x22
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Location = New System.Drawing.Point(1044, 34)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(26, 24)
        Me.btnSearch.TabIndex = 51
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'frmPrudentialInterest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1094, 512)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnSummaryReport)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.btnPostToGP)
        Me.Controls.Add(Me.btnSearchInterest)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnJV)
        Me.Controls.Add(Me.DGridViewPrudential)
        Me.Controls.Add(Me.btnInterest)
        Me.Controls.Add(Me.btnSave)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmPrudentialInterest"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Prudential Interest"
        Me.TopMost = True
        CType(Me.DGridViewPrudential, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnPostToGP As System.Windows.Forms.Button
    Friend WithEvents btnInterest As System.Windows.Forms.Button
    Friend WithEvents btnJV As System.Windows.Forms.Button
    Friend WithEvents btnSearchInterest As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents DGridViewPrudential As System.Windows.Forms.DataGridView
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSummaryReport As System.Windows.Forms.Button
    Friend WithEvents colTransDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colOR As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents colDMCMNo As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
