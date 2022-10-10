<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCollectionPost
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
        Me.lblTransactionDate = New System.Windows.Forms.Label()
        Me.dtTransactionDate = New System.Windows.Forms.DateTimePicker()
        Me.DGridViewCollection = New System.Windows.Forms.DataGridView()
        Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colORNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDMCMNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCreatedDocumentMain = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCollectionDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCollected = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAllocationType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnCollectionSummary = New System.Windows.Forms.Button()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnPost = New System.Windows.Forms.Button()
        Me.btnViewJV = New System.Windows.Forms.Button()
        CType(Me.DGridViewCollection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTransactionDate
        '
        Me.lblTransactionDate.AutoSize = True
        Me.lblTransactionDate.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransactionDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTransactionDate.Location = New System.Drawing.Point(20, 17)
        Me.lblTransactionDate.Name = "lblTransactionDate"
        Me.lblTransactionDate.Size = New System.Drawing.Size(90, 14)
        Me.lblTransactionDate.TabIndex = 7
        Me.lblTransactionDate.Text = "lblTransacDate:"
        Me.lblTransactionDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtTransactionDate
        '
        Me.dtTransactionDate.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtTransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTransactionDate.Location = New System.Drawing.Point(116, 12)
        Me.dtTransactionDate.Name = "dtTransactionDate"
        Me.dtTransactionDate.Size = New System.Drawing.Size(147, 23)
        Me.dtTransactionDate.TabIndex = 8
        '
        'DGridViewCollection
        '
        Me.DGridViewCollection.AllowUserToAddRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridViewCollection.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGridViewCollection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridViewCollection.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colORNo, Me.colDMCMNo, Me.colCreatedDocumentMain, Me.colCollectionDate, Me.colIDNumber, Me.colParticipantID, Me.colCollected, Me.colType, Me.colAllocationType})
        Me.DGridViewCollection.Location = New System.Drawing.Point(15, 50)
        Me.DGridViewCollection.MultiSelect = False
        Me.DGridViewCollection.Name = "DGridViewCollection"
        Me.DGridViewCollection.RowHeadersWidth = 20
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGridViewCollection.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DGridViewCollection.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGridViewCollection.Size = New System.Drawing.Size(809, 355)
        Me.DGridViewCollection.TabIndex = 10
        '
        'colID
        '
        Me.colID.HeaderText = "ID"
        Me.colID.Name = "colID"
        Me.colID.ReadOnly = True
        Me.colID.Visible = False
        '
        'colORNo
        '
        Me.colORNo.HeaderText = "ORNo"
        Me.colORNo.Name = "colORNo"
        Me.colORNo.ReadOnly = True
        Me.colORNo.Visible = False
        Me.colORNo.Width = 70
        '
        'colDMCMNo
        '
        Me.colDMCMNo.HeaderText = "DMCMNo"
        Me.colDMCMNo.Name = "colDMCMNo"
        Me.colDMCMNo.ReadOnly = True
        Me.colDMCMNo.Visible = False
        '
        'colCreatedDocumentMain
        '
        Me.colCreatedDocumentMain.HeaderText = "CreatedDocument"
        Me.colCreatedDocumentMain.Name = "colCreatedDocumentMain"
        Me.colCreatedDocumentMain.ReadOnly = True
        Me.colCreatedDocumentMain.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colCreatedDocumentMain.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colCollectionDate
        '
        Me.colCollectionDate.HeaderText = "CollectionDate"
        Me.colCollectionDate.Name = "colCollectionDate"
        Me.colCollectionDate.ReadOnly = True
        Me.colCollectionDate.Width = 90
        '
        'colIDNumber
        '
        Me.colIDNumber.HeaderText = "IDNumber"
        Me.colIDNumber.Name = "colIDNumber"
        Me.colIDNumber.ReadOnly = True
        Me.colIDNumber.Visible = False
        '
        'colParticipantID
        '
        Me.colParticipantID.HeaderText = "ParticipantID"
        Me.colParticipantID.Name = "colParticipantID"
        Me.colParticipantID.ReadOnly = True
        Me.colParticipantID.Width = 90
        '
        'colCollected
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.colCollected.DefaultCellStyle = DataGridViewCellStyle2
        Me.colCollected.HeaderText = "CollectedAmount"
        Me.colCollected.Name = "colCollected"
        Me.colCollected.ReadOnly = True
        '
        'colType
        '
        Me.colType.HeaderText = "Type"
        Me.colType.Name = "colType"
        Me.colType.ReadOnly = True
        Me.colType.Width = 80
        '
        'colAllocationType
        '
        Me.colAllocationType.HeaderText = "AllocationType"
        Me.colAllocationType.Name = "colAllocationType"
        Me.colAllocationType.ReadOnly = True
        '
        'btnCollectionSummary
        '
        Me.btnCollectionSummary.BackColor = System.Drawing.Color.White
        Me.btnCollectionSummary.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnCollectionSummary.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnCollectionSummary.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnCollectionSummary.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCollectionSummary.ForeColor = System.Drawing.Color.Black
        Me.btnCollectionSummary.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.btnCollectionSummary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCollectionSummary.Location = New System.Drawing.Point(505, 5)
        Me.btnCollectionSummary.Name = "btnCollectionSummary"
        Me.btnCollectionSummary.Size = New System.Drawing.Size(180, 39)
        Me.btnCollectionSummary.TabIndex = 11
        Me.btnCollectionSummary.Text = "     &Collection Summary (Draft)"
        Me.btnCollectionSummary.UseVisualStyleBackColor = False
        '
        'btnGenerate
        '
        Me.btnGenerate.BackColor = System.Drawing.Color.White
        Me.btnGenerate.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.btnGenerate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnGenerate.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.btnGenerate.Image = Global.AccountsManagementForms.My.Resources.Resources.SearchIconColored22x22
        Me.btnGenerate.Location = New System.Drawing.Point(269, 9)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(40, 30)
        Me.btnGenerate.TabIndex = 9
        Me.btnGenerate.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.White
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.ForeColor = System.Drawing.Color.Black
        Me.btnCancel.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIcon22x22
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(691, 410)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(133, 39)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "&Close"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnPost
        '
        Me.btnPost.BackColor = System.Drawing.Color.White
        Me.btnPost.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnPost.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnPost.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnPost.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPost.ForeColor = System.Drawing.Color.Black
        Me.btnPost.Image = Global.AccountsManagementForms.My.Resources.Resources.PostIcon22x22
        Me.btnPost.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPost.Location = New System.Drawing.Point(691, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(133, 39)
        Me.btnPost.TabIndex = 5
        Me.btnPost.Text = "&Post to GP"
        Me.btnPost.UseVisualStyleBackColor = False
        '
        'btnViewJV
        '
        Me.btnViewJV.BackColor = System.Drawing.Color.White
        Me.btnViewJV.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnViewJV.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnViewJV.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnViewJV.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewJV.ForeColor = System.Drawing.Color.Black
        Me.btnViewJV.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.btnViewJV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnViewJV.Location = New System.Drawing.Point(319, 5)
        Me.btnViewJV.Name = "btnViewJV"
        Me.btnViewJV.Size = New System.Drawing.Size(180, 39)
        Me.btnViewJV.TabIndex = 4
        Me.btnViewJV.Text = "    &Journal Voucher (Draft)"
        Me.btnViewJV.UseVisualStyleBackColor = False
        '
        'frmCollectionPost
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(841, 461)
        Me.Controls.Add(Me.btnCollectionSummary)
        Me.Controls.Add(Me.DGridViewCollection)
        Me.Controls.Add(Me.btnGenerate)
        Me.Controls.Add(Me.dtTransactionDate)
        Me.Controls.Add(Me.lblTransactionDate)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnPost)
        Me.Controls.Add(Me.btnViewJV)
        Me.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmCollectionPost"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ";"
        CType(Me.DGridViewCollection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnViewJV As System.Windows.Forms.Button
    Friend WithEvents btnPost As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblTransactionDate As System.Windows.Forms.Label
    Friend WithEvents dtTransactionDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents DGridViewCollection As System.Windows.Forms.DataGridView
    Friend WithEvents btnCollectionSummary As System.Windows.Forms.Button
    Friend WithEvents colID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colORNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDMCMNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCreatedDocumentMain As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCollectionDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCollected As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAllocationType As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
