<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrudentialTransferInterest
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
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.DGridViewPrudential = New System.Windows.Forms.DataGridView()
        Me.colIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParticipantName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCheck = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.btnPostToGP = New System.Windows.Forms.Button()
        Me.btnSummaryReport = New System.Windows.Forms.Button()
        Me.btnSearchReplenishment = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnJV = New System.Windows.Forms.Button()
        Me.btnTransfer = New System.Windows.Forms.Button()
        Me.btnLoadInterest = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        CType(Me.DGridViewPrudential, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(820, 45)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 14)
        Me.Label5.TabIndex = 52
        Me.Label5.Text = "Participant ID:"
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(908, 43)
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
        Me.DGridViewPrudential.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGridViewPrudential.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridViewPrudential.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colIDNumber, Me.colParticipantID, Me.colParticipantName, Me.colAmount, Me.colCheck})
        Me.DGridViewPrudential.Location = New System.Drawing.Point(14, 76)
        Me.DGridViewPrudential.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGridViewPrudential.MultiSelect = False
        Me.DGridViewPrudential.Name = "DGridViewPrudential"
        Me.DGridViewPrudential.RowHeadersWidth = 20
        Me.DGridViewPrudential.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DGridViewPrudential.Size = New System.Drawing.Size(1078, 343)
        Me.DGridViewPrudential.TabIndex = 49
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
        'colCheck
        '
        Me.colCheck.HeaderText = "Transfer"
        Me.colCheck.Name = "colCheck"
        Me.colCheck.ReadOnly = True
        '
        'btnPostToGP
        '
        Me.btnPostToGP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPostToGP.BackColor = System.Drawing.Color.White
        Me.btnPostToGP.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnPostToGP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnPostToGP.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnPostToGP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPostToGP.ForeColor = System.Drawing.Color.Black
        Me.btnPostToGP.Image = Global.AccountsManagementForms.My.Resources.Resources.PostIcon22x22
        Me.btnPostToGP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPostToGP.Location = New System.Drawing.Point(680, 431)
        Me.btnPostToGP.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnPostToGP.Name = "btnPostToGP"
        Me.btnPostToGP.Size = New System.Drawing.Size(135, 39)
        Me.btnPostToGP.TabIndex = 47
        Me.btnPostToGP.Text = "       &Post To GP"
        Me.btnPostToGP.UseVisualStyleBackColor = False
        '
        'btnSummaryReport
        '
        Me.btnSummaryReport.BackColor = System.Drawing.Color.White
        Me.btnSummaryReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSummaryReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSummaryReport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSummaryReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSummaryReport.ForeColor = System.Drawing.Color.Black
        Me.btnSummaryReport.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.btnSummaryReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSummaryReport.Location = New System.Drawing.Point(189, 22)
        Me.btnSummaryReport.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSummaryReport.Name = "btnSummaryReport"
        Me.btnSummaryReport.Size = New System.Drawing.Size(169, 39)
        Me.btnSummaryReport.TabIndex = 51
        Me.btnSummaryReport.Text = "      &Summary Report (Draft)"
        Me.btnSummaryReport.UseVisualStyleBackColor = False
        '
        'btnSearchReplenishment
        '
        Me.btnSearchReplenishment.BackColor = System.Drawing.Color.White
        Me.btnSearchReplenishment.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSearchReplenishment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSearchReplenishment.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSearchReplenishment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchReplenishment.ForeColor = System.Drawing.Color.Black
        Me.btnSearchReplenishment.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.btnSearchReplenishment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearchReplenishment.Location = New System.Drawing.Point(596, 22)
        Me.btnSearchReplenishment.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSearchReplenishment.Name = "btnSearchReplenishment"
        Me.btnSearchReplenishment.Size = New System.Drawing.Size(110, 39)
        Me.btnSearchReplenishment.TabIndex = 43
        Me.btnSearchReplenishment.Text = "       &Search"
        Me.btnSearchReplenishment.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.White
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(956, 431)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(136, 39)
        Me.btnClose.TabIndex = 56
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnEdit
        '
        Me.btnEdit.BackColor = System.Drawing.Color.White
        Me.btnEdit.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnEdit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnEdit.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEdit.ForeColor = System.Drawing.Color.Black
        Me.btnEdit.Image = Global.AccountsManagementForms.My.Resources.Resources.EditDocumentColored22x22
        Me.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEdit.Location = New System.Drawing.Point(480, 22)
        Me.btnEdit.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(110, 39)
        Me.btnEdit.TabIndex = 48
        Me.btnEdit.Text = "      &Modify"
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'btnJV
        '
        Me.btnJV.BackColor = System.Drawing.Color.White
        Me.btnJV.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnJV.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnJV.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnJV.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnJV.ForeColor = System.Drawing.Color.Black
        Me.btnJV.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.btnJV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnJV.Location = New System.Drawing.Point(14, 22)
        Me.btnJV.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnJV.Name = "btnJV"
        Me.btnJV.Size = New System.Drawing.Size(169, 39)
        Me.btnJV.TabIndex = 46
        Me.btnJV.Text = "      &Journal Voucher (Draft)"
        Me.btnJV.UseVisualStyleBackColor = False
        '
        'btnTransfer
        '
        Me.btnTransfer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTransfer.BackColor = System.Drawing.Color.White
        Me.btnTransfer.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnTransfer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnTransfer.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTransfer.ForeColor = System.Drawing.Color.Black
        Me.btnTransfer.Image = Global.AccountsManagementForms.My.Resources.Resources.RefreshOrangeIcon22x22
        Me.btnTransfer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTransfer.Location = New System.Drawing.Point(821, 431)
        Me.btnTransfer.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnTransfer.Name = "btnTransfer"
        Me.btnTransfer.Size = New System.Drawing.Size(131, 39)
        Me.btnTransfer.TabIndex = 45
        Me.btnTransfer.Text = "      &Transfer"
        Me.btnTransfer.UseVisualStyleBackColor = False
        '
        'btnLoadInterest
        '
        Me.btnLoadInterest.BackColor = System.Drawing.Color.White
        Me.btnLoadInterest.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnLoadInterest.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnLoadInterest.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnLoadInterest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoadInterest.ForeColor = System.Drawing.Color.Black
        Me.btnLoadInterest.Image = Global.AccountsManagementForms.My.Resources.Resources.DownloadIcon22x22
        Me.btnLoadInterest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLoadInterest.Location = New System.Drawing.Point(364, 22)
        Me.btnLoadInterest.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnLoadInterest.Name = "btnLoadInterest"
        Me.btnLoadInterest.Size = New System.Drawing.Size(110, 39)
        Me.btnLoadInterest.TabIndex = 42
        Me.btnLoadInterest.Text = "      &Load Interest"
        Me.btnLoadInterest.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.AccessibleName = ""
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.BackColor = System.Drawing.Color.White
        Me.btnSearch.BackgroundImage = Global.AccountsManagementForms.My.Resources.Resources.SearchIconColored22x22
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Location = New System.Drawing.Point(1057, 33)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(35, 30)
        Me.btnSearch.TabIndex = 51
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'frmPrudentialTransferInterest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1102, 484)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnPostToGP)
        Me.Controls.Add(Me.btnSummaryReport)
        Me.Controls.Add(Me.btnSearchReplenishment)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnJV)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnTransfer)
        Me.Controls.Add(Me.btnLoadInterest)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.DGridViewPrudential)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmPrudentialTransferInterest"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Transfer Prudential Interest"
        CType(Me.DGridViewPrudential, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnPostToGP As System.Windows.Forms.Button
    Friend WithEvents btnLoadInterest As System.Windows.Forms.Button
    Friend WithEvents btnJV As System.Windows.Forms.Button
    Friend WithEvents btnSearchReplenishment As System.Windows.Forms.Button
    Friend WithEvents btnTransfer As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents DGridViewPrudential As System.Windows.Forms.DataGridView
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSummaryReport As System.Windows.Forms.Button
    Friend WithEvents colIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCheck As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
