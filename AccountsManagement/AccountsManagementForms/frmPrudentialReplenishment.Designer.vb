<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrudentialReplenishment
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
        Me.DGridViewPrudential = New System.Windows.Forms.DataGridView()
        Me.colTransDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParticipantName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colOR = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.colORNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnReplenishment = New System.Windows.Forms.Button()
        Me.btnSearchReplenishment = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnJV = New System.Windows.Forms.Button()
        Me.btnPostToGP = New System.Windows.Forms.Button()
        Me.btnSummaryReport = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        CType(Me.DGridViewPrudential, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGridViewPrudential
        '
        Me.DGridViewPrudential.AllowUserToAddRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridViewPrudential.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGridViewPrudential.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridViewPrudential.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colTransDate, Me.colIDNumber, Me.colParticipantID, Me.colParticipantName, Me.colAmount, Me.colOR, Me.colORNumber})
        Me.DGridViewPrudential.Location = New System.Drawing.Point(18, 69)
        Me.DGridViewPrudential.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGridViewPrudential.MultiSelect = False
        Me.DGridViewPrudential.Name = "DGridViewPrudential"
        Me.DGridViewPrudential.RowHeadersWidth = 20
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGridViewPrudential.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DGridViewPrudential.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DGridViewPrudential.Size = New System.Drawing.Size(1078, 333)
        Me.DGridViewPrudential.TabIndex = 1
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
        Me.colOR.Text = "&View OR"
        Me.colOR.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'colORNumber
        '
        Me.colORNumber.HeaderText = "ORNumber"
        Me.colORNumber.Name = "colORNumber"
        Me.colORNumber.ReadOnly = True
        Me.colORNumber.Visible = False
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.DimGray
        Me.Label5.Location = New System.Drawing.Point(813, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 14)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "Participant ID:"
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.Location = New System.Drawing.Point(901, 36)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtSearch.MaxLength = 10
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(145, 20)
        Me.txtSearch.TabIndex = 39
        '
        'btnReplenishment
        '
        Me.btnReplenishment.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnReplenishment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnReplenishment.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnReplenishment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReplenishment.ForeColor = System.Drawing.Color.Black
        Me.btnReplenishment.Image = Global.AccountsManagementForms.My.Resources.Resources.EditDocumentColored22x22
        Me.btnReplenishment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReplenishment.Location = New System.Drawing.Point(368, 17)
        Me.btnReplenishment.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnReplenishment.Name = "btnReplenishment"
        Me.btnReplenishment.Size = New System.Drawing.Size(169, 39)
        Me.btnReplenishment.TabIndex = 42
        Me.btnReplenishment.Text = "       &Input Replenishment"
        Me.btnReplenishment.UseVisualStyleBackColor = True
        '
        'btnSearchReplenishment
        '
        Me.btnSearchReplenishment.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSearchReplenishment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSearchReplenishment.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSearchReplenishment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchReplenishment.ForeColor = System.Drawing.Color.Black
        Me.btnSearchReplenishment.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.btnSearchReplenishment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearchReplenishment.Location = New System.Drawing.Point(659, 17)
        Me.btnSearchReplenishment.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSearchReplenishment.Name = "btnSearchReplenishment"
        Me.btnSearchReplenishment.Size = New System.Drawing.Size(110, 39)
        Me.btnSearchReplenishment.TabIndex = 43
        Me.btnSearchReplenishment.Text = "   S&earch"
        Me.btnSearchReplenishment.UseVisualStyleBackColor = True
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
        Me.btnSave.Location = New System.Drawing.Point(850, 416)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(120, 39)
        Me.btnSave.TabIndex = 45
        Me.btnSave.Text = "   &Save"
        Me.btnSave.UseVisualStyleBackColor = True
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
        Me.btnJV.Location = New System.Drawing.Point(18, 17)
        Me.btnJV.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnJV.Name = "btnJV"
        Me.btnJV.Size = New System.Drawing.Size(169, 39)
        Me.btnJV.TabIndex = 46
        Me.btnJV.Text = "      &Journal Voucher (Draft)"
        Me.btnJV.UseVisualStyleBackColor = True
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
        Me.btnPostToGP.Location = New System.Drawing.Point(724, 416)
        Me.btnPostToGP.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnPostToGP.Name = "btnPostToGP"
        Me.btnPostToGP.Size = New System.Drawing.Size(120, 39)
        Me.btnPostToGP.TabIndex = 47
        Me.btnPostToGP.Text = "    &Post To GP"
        Me.btnPostToGP.UseVisualStyleBackColor = True
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
        Me.btnSummaryReport.Location = New System.Drawing.Point(193, 17)
        Me.btnSummaryReport.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSummaryReport.Name = "btnSummaryReport"
        Me.btnSummaryReport.Size = New System.Drawing.Size(169, 39)
        Me.btnSummaryReport.TabIndex = 50
        Me.btnSummaryReport.Text = "      &Summary Report (Draft)"
        Me.btnSummaryReport.UseVisualStyleBackColor = True
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
        Me.btnEdit.Location = New System.Drawing.Point(543, 17)
        Me.btnEdit.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(110, 39)
        Me.btnEdit.TabIndex = 48
        Me.btnEdit.Text = "&Modify"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.AccessibleName = ""
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Image = Global.AccountsManagementForms.My.Resources.Resources.SearchIconColored22x22
        Me.btnSearch.Location = New System.Drawing.Point(1052, 33)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(35, 30)
        Me.btnSearch.TabIndex = 40
        Me.btnSearch.UseVisualStyleBackColor = True
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
        Me.btnClose.Location = New System.Drawing.Point(976, 416)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(120, 39)
        Me.btnClose.TabIndex = 56
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmPrudentialReplenishment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1112, 467)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnSummaryReport)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnPostToGP)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnReplenishment)
        Me.Controls.Add(Me.btnJV)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.btnSearchReplenishment)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.DGridViewPrudential)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "frmPrudentialReplenishment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Prudential Replenishment"
        CType(Me.DGridViewPrudential, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DGridViewPrudential As System.Windows.Forms.DataGridView
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnReplenishment As System.Windows.Forms.Button
    Friend WithEvents btnSearchReplenishment As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnJV As System.Windows.Forms.Button
    Friend WithEvents btnPostToGP As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSummaryReport As System.Windows.Forms.Button
    Friend WithEvents colTransDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colOR As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents colORNumber As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
