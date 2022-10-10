<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFTF
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DGridViewMain = New System.Windows.Forms.DataGridView()
        Me.colRefNoMain = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAllocationDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBatchCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDRDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCRDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTotalAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DGridViewParticipant = New System.Windows.Forms.DataGridView()
        Me.colRefNoParticipant = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DGridViewDetails = New System.Windows.Forms.DataGridView()
        Me.colRefNoDetails = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBankAccountNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIssuingBank = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colReceivingBank = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDebit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCredit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParticulars = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.ddlTransType = New System.Windows.Forms.ComboBox()
        Me.dtDateTo = New System.Windows.Forms.DateTimePicker()
        Me.dtDateFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnReport = New System.Windows.Forms.Button()
        CType(Me.DGridViewMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGridViewParticipant, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGridViewDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DGridViewMain
        '
        Me.DGridViewMain.AllowUserToAddRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridViewMain.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGridViewMain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGridViewMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridViewMain.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colRefNoMain, Me.colAllocationDate, Me.colBatchCode, Me.colDRDate, Me.colCRDate, Me.colTotalAmount})
        Me.DGridViewMain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGridViewMain.Location = New System.Drawing.Point(12, 66)
        Me.DGridViewMain.MultiSelect = False
        Me.DGridViewMain.Name = "DGridViewMain"
        Me.DGridViewMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGridViewMain.Size = New System.Drawing.Size(753, 152)
        Me.DGridViewMain.TabIndex = 0
        '
        'colRefNoMain
        '
        Me.colRefNoMain.HeaderText = "RefNo"
        Me.colRefNoMain.Name = "colRefNoMain"
        Me.colRefNoMain.ReadOnly = True
        '
        'colAllocationDate
        '
        Me.colAllocationDate.HeaderText = "AllocationDate"
        Me.colAllocationDate.Name = "colAllocationDate"
        Me.colAllocationDate.ReadOnly = True
        '
        'colBatchCode
        '
        Me.colBatchCode.HeaderText = "BatchCode"
        Me.colBatchCode.Name = "colBatchCode"
        Me.colBatchCode.ReadOnly = True
        '
        'colDRDate
        '
        Me.colDRDate.HeaderText = "DRDate"
        Me.colDRDate.Name = "colDRDate"
        Me.colDRDate.ReadOnly = True
        '
        'colCRDate
        '
        Me.colCRDate.HeaderText = "CRDate"
        Me.colCRDate.Name = "colCRDate"
        Me.colCRDate.ReadOnly = True
        '
        'colTotalAmount
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.colTotalAmount.DefaultCellStyle = DataGridViewCellStyle2
        Me.colTotalAmount.HeaderText = "TotalAmount"
        Me.colTotalAmount.Name = "colTotalAmount"
        Me.colTotalAmount.ReadOnly = True
        '
        'DGridViewParticipant
        '
        Me.DGridViewParticipant.AllowUserToAddRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridViewParticipant.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DGridViewParticipant.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGridViewParticipant.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridViewParticipant.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colRefNoParticipant, Me.colIDNumber, Me.colParticipantID, Me.colAmount})
        Me.DGridViewParticipant.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGridViewParticipant.Location = New System.Drawing.Point(12, 224)
        Me.DGridViewParticipant.Name = "DGridViewParticipant"
        Me.DGridViewParticipant.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGridViewParticipant.Size = New System.Drawing.Size(753, 143)
        Me.DGridViewParticipant.TabIndex = 1
        '
        'colRefNoParticipant
        '
        Me.colRefNoParticipant.HeaderText = "RefNo"
        Me.colRefNoParticipant.Name = "colRefNoParticipant"
        Me.colRefNoParticipant.ReadOnly = True
        Me.colRefNoParticipant.Visible = False
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
        'colAmount
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.colAmount.DefaultCellStyle = DataGridViewCellStyle4
        Me.colAmount.HeaderText = "Amount"
        Me.colAmount.Name = "colAmount"
        Me.colAmount.ReadOnly = True
        '
        'DGridViewDetails
        '
        Me.DGridViewDetails.AllowUserToAddRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridViewDetails.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.DGridViewDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGridViewDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridViewDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colRefNoDetails, Me.colBankAccountNo, Me.colIssuingBank, Me.colReceivingBank, Me.colDebit, Me.colCredit, Me.colParticulars})
        Me.DGridViewDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGridViewDetails.Location = New System.Drawing.Point(10, 373)
        Me.DGridViewDetails.Name = "DGridViewDetails"
        Me.DGridViewDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGridViewDetails.Size = New System.Drawing.Size(753, 152)
        Me.DGridViewDetails.TabIndex = 2
        '
        'colRefNoDetails
        '
        Me.colRefNoDetails.HeaderText = "RefNo"
        Me.colRefNoDetails.Name = "colRefNoDetails"
        Me.colRefNoDetails.ReadOnly = True
        Me.colRefNoDetails.Visible = False
        '
        'colBankAccountNo
        '
        Me.colBankAccountNo.HeaderText = "BankAccountNo"
        Me.colBankAccountNo.Name = "colBankAccountNo"
        Me.colBankAccountNo.ReadOnly = True
        '
        'colIssuingBank
        '
        Me.colIssuingBank.HeaderText = "IssuingBank"
        Me.colIssuingBank.Name = "colIssuingBank"
        Me.colIssuingBank.ReadOnly = True
        '
        'colReceivingBank
        '
        Me.colReceivingBank.HeaderText = "ReceivingBank"
        Me.colReceivingBank.Name = "colReceivingBank"
        Me.colReceivingBank.ReadOnly = True
        '
        'colDebit
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.colDebit.DefaultCellStyle = DataGridViewCellStyle6
        Me.colDebit.HeaderText = "Debit"
        Me.colDebit.Name = "colDebit"
        Me.colDebit.ReadOnly = True
        '
        'colCredit
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N2"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.colCredit.DefaultCellStyle = DataGridViewCellStyle7
        Me.colCredit.HeaderText = "Credit"
        Me.colCredit.Name = "colCredit"
        Me.colCredit.ReadOnly = True
        '
        'colParticulars
        '
        Me.colParticulars.HeaderText = "Particulars"
        Me.colParticulars.Name = "colParticulars"
        Me.colParticulars.ReadOnly = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.ddlTransType)
        Me.GroupBox1.Controls.Add(Me.dtDateTo)
        Me.GroupBox1.Controls.Add(Me.dtDateFrom)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(754, 51)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(6, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(147, 14)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Search by Allocation Date:"
        '
        'btnSearch
        '
        Me.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.ForeColor = System.Drawing.Color.Blue
        Me.btnSearch.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(708, 13)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(35, 30)
        Me.btnSearch.TabIndex = 6
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'ddlTransType
        '
        Me.ddlTransType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlTransType.FormattingEnabled = True
        Me.ddlTransType.Location = New System.Drawing.Point(485, 19)
        Me.ddlTransType.Name = "ddlTransType"
        Me.ddlTransType.Size = New System.Drawing.Size(217, 20)
        Me.ddlTransType.TabIndex = 5
        '
        'dtDateTo
        '
        Me.dtDateTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtDateTo.Location = New System.Drawing.Point(251, 19)
        Me.dtDateTo.Name = "dtDateTo"
        Me.dtDateTo.Size = New System.Drawing.Size(91, 20)
        Me.dtDateTo.TabIndex = 4
        '
        'dtDateFrom
        '
        Me.dtDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtDateFrom.Location = New System.Drawing.Point(78, 19)
        Me.dtDateFrom.Name = "dtDateFrom"
        Me.dtDateFrom.Size = New System.Drawing.Size(91, 20)
        Me.dtDateFrom.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(376, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Transaction Type:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(193, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Date To:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Date From:"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(622, 531)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(141, 39)
        Me.btnClose.TabIndex = 24
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnReport
        '
        Me.btnReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnReport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReport.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReport.ForeColor = System.Drawing.Color.Black
        Me.btnReport.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.btnReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReport.Location = New System.Drawing.Point(475, 531)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(141, 39)
        Me.btnReport.TabIndex = 25
        Me.btnReport.Text = "   &Generate Report"
        Me.btnReport.UseVisualStyleBackColor = True
        '
        'frmFTF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(775, 582)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnReport)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DGridViewDetails)
        Me.Controls.Add(Me.DGridViewParticipant)
        Me.Controls.Add(Me.DGridViewMain)
        Me.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmFTF"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Fund Transfer Form"
        CType(Me.DGridViewMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGridViewParticipant, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGridViewDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGridViewMain As System.Windows.Forms.DataGridView
    Friend WithEvents DGridViewParticipant As System.Windows.Forms.DataGridView
    Friend WithEvents DGridViewDetails As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents ddlTransType As System.Windows.Forms.ComboBox
    Friend WithEvents dtDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnReport As System.Windows.Forms.Button
    Friend WithEvents colRefNoMain As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAllocationDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBatchCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDRDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCRDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTotalAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRefNoParticipant As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRefNoDetails As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBankAccountNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIssuingBank As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colReceivingBank As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDebit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCredit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticulars As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
