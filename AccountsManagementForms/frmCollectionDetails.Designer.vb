<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCollectionDetails
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtBatchCode = New System.Windows.Forms.TextBox
        Me.txtBillingPeriod = New System.Windows.Forms.TextBox
        Me.txtStlRun = New System.Windows.Forms.TextBox
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox
        Me.txtAmount = New System.Windows.Forms.TextBox
        Me.txtDueDate = New System.Windows.Forms.TextBox
        Me.txtInvoiceDate = New System.Windows.Forms.TextBox
        Me.txtParticipantID = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.DGridViewDetails = New System.Windows.Forms.DataGridView
        Me.colInvDMCMValue = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colParticipantID1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colAcctCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colDescription = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colDebit = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colCredit = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Label10 = New System.Windows.Forms.Label
        Me.DGridViewMain = New System.Windows.Forms.DataGridView
        Me.colDMCMNo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colJVNo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colParticulars = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colAmount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colPreparedBy = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colCheckedBy = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colApprovedBy = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colUpdatedDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnClose = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DGridViewDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGridViewMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtBatchCode)
        Me.GroupBox1.Controls.Add(Me.txtBillingPeriod)
        Me.GroupBox1.Controls.Add(Me.txtStlRun)
        Me.GroupBox1.Controls.Add(Me.txtInvoiceNo)
        Me.GroupBox1.Controls.Add(Me.txtAmount)
        Me.GroupBox1.Controls.Add(Me.txtDueDate)
        Me.GroupBox1.Controls.Add(Me.txtInvoiceDate)
        Me.GroupBox1.Controls.Add(Me.txtParticipantID)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(784, 62)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                        Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(6, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(58, 15)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "WESM Bill"
        '
        'txtBatchCode
        '
        Me.txtBatchCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBatchCode.Location = New System.Drawing.Point(109, 37)
        Me.txtBatchCode.Name = "txtBatchCode"
        Me.txtBatchCode.ReadOnly = True
        Me.txtBatchCode.Size = New System.Drawing.Size(94, 20)
        Me.txtBatchCode.TabIndex = 16
        Me.txtBatchCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtBillingPeriod
        '
        Me.txtBillingPeriod.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillingPeriod.Location = New System.Drawing.Point(205, 37)
        Me.txtBillingPeriod.Name = "txtBillingPeriod"
        Me.txtBillingPeriod.ReadOnly = True
        Me.txtBillingPeriod.Size = New System.Drawing.Size(94, 20)
        Me.txtBillingPeriod.TabIndex = 15
        Me.txtBillingPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStlRun
        '
        Me.txtStlRun.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtStlRun.Location = New System.Drawing.Point(301, 37)
        Me.txtStlRun.Name = "txtStlRun"
        Me.txtStlRun.ReadOnly = True
        Me.txtStlRun.Size = New System.Drawing.Size(94, 20)
        Me.txtStlRun.TabIndex = 14
        Me.txtStlRun.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInvoiceNo.Location = New System.Drawing.Point(397, 37)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.ReadOnly = True
        Me.txtInvoiceNo.Size = New System.Drawing.Size(94, 20)
        Me.txtInvoiceNo.TabIndex = 13
        Me.txtInvoiceNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtAmount
        '
        Me.txtAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAmount.Location = New System.Drawing.Point(588, 37)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.ReadOnly = True
        Me.txtAmount.Size = New System.Drawing.Size(94, 20)
        Me.txtAmount.TabIndex = 12
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtDueDate
        '
        Me.txtDueDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDueDate.Location = New System.Drawing.Point(683, 37)
        Me.txtDueDate.Name = "txtDueDate"
        Me.txtDueDate.ReadOnly = True
        Me.txtDueDate.Size = New System.Drawing.Size(94, 20)
        Me.txtDueDate.TabIndex = 11
        Me.txtDueDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtInvoiceDate
        '
        Me.txtInvoiceDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInvoiceDate.Location = New System.Drawing.Point(493, 37)
        Me.txtInvoiceDate.Name = "txtInvoiceDate"
        Me.txtInvoiceDate.ReadOnly = True
        Me.txtInvoiceDate.Size = New System.Drawing.Size(94, 20)
        Me.txtInvoiceDate.TabIndex = 10
        Me.txtInvoiceDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtParticipantID
        '
        Me.txtParticipantID.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtParticipantID.Location = New System.Drawing.Point(13, 37)
        Me.txtParticipantID.Name = "txtParticipantID"
        Me.txtParticipantID.ReadOnly = True
        Me.txtParticipantID.Size = New System.Drawing.Size(94, 20)
        Me.txtParticipantID.TabIndex = 9
        Me.txtParticipantID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(705, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 12)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Due Date"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(612, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(42, 12)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Amount"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(502, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 12)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Invoice Date"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(412, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 12)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Invoice No."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(26, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 12)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Participant ID"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(320, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 12)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "STL Run"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(221, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Billing Period"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(122, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Batch Code"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.DGridViewDetails)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.DGridViewMain)
        Me.GroupBox2.Location = New System.Drawing.Point(15, 85)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(781, 372)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'DGridViewDetails
        '
        Me.DGridViewDetails.AllowUserToAddRows = False
        Me.DGridViewDetails.AllowUserToDeleteRows = False
        Me.DGridViewDetails.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridViewDetails.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGridViewDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridViewDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colInvDMCMValue, Me.colParticipantID1, Me.colAcctCode, Me.colDescription, Me.colDebit, Me.colCredit})
        Me.DGridViewDetails.Location = New System.Drawing.Point(11, 180)
        Me.DGridViewDetails.Name = "DGridViewDetails"
        Me.DGridViewDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGridViewDetails.Size = New System.Drawing.Size(764, 185)
        Me.DGridViewDetails.TabIndex = 3
        '
        'colInvDMCMValue
        '
        Me.colInvDMCMValue.HeaderText = "InvDMCMValue"
        Me.colInvDMCMValue.Name = "colInvDMCMValue"
        Me.colInvDMCMValue.ReadOnly = True
        '
        'colParticipantID1
        '
        Me.colParticipantID1.HeaderText = "ParticipantID"
        Me.colParticipantID1.Name = "colParticipantID1"
        Me.colParticipantID1.ReadOnly = True
        '
        'colAcctCode
        '
        Me.colAcctCode.HeaderText = "AccountCode"
        Me.colAcctCode.Name = "colAcctCode"
        Me.colAcctCode.ReadOnly = True
        '
        'colDescription
        '
        Me.colDescription.HeaderText = "Description"
        Me.colDescription.Name = "colDescription"
        Me.colDescription.ReadOnly = True
        '
        'colDebit
        '
        Me.colDebit.HeaderText = "Debit"
        Me.colDebit.Name = "colDebit"
        Me.colDebit.ReadOnly = True
        '
        'colCredit
        '
        Me.colCredit.HeaderText = "Credit"
        Me.colCredit.Name = "colCredit"
        Me.colCredit.ReadOnly = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                        Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Blue
        Me.Label10.Location = New System.Drawing.Point(6, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(42, 15)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "DMCM "
        '
        'DGridViewMain
        '
        Me.DGridViewMain.AllowUserToAddRows = False
        Me.DGridViewMain.AllowUserToDeleteRows = False
        Me.DGridViewMain.AllowUserToOrderColumns = True
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridViewMain.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.DGridViewMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridViewMain.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDMCMNo, Me.colJVNo, Me.colParticipantID, Me.colParticulars, Me.colAmount, Me.colPreparedBy, Me.colCheckedBy, Me.colApprovedBy, Me.colUpdatedDate})
        Me.DGridViewMain.Location = New System.Drawing.Point(10, 24)
        Me.DGridViewMain.MultiSelect = False
        Me.DGridViewMain.Name = "DGridViewMain"
        Me.DGridViewMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGridViewMain.Size = New System.Drawing.Size(764, 150)
        Me.DGridViewMain.TabIndex = 0
        '
        'colDMCMNo
        '
        Me.colDMCMNo.HeaderText = "DMCMNo"
        Me.colDMCMNo.Name = "colDMCMNo"
        Me.colDMCMNo.ReadOnly = True
        '
        'colJVNo
        '
        Me.colJVNo.HeaderText = "JVNo"
        Me.colJVNo.Name = "colJVNo"
        Me.colJVNo.ReadOnly = True
        '
        'colParticipantID
        '
        Me.colParticipantID.HeaderText = "ParticipantID"
        Me.colParticipantID.Name = "colParticipantID"
        Me.colParticipantID.ReadOnly = True
        '
        'colParticulars
        '
        Me.colParticulars.HeaderText = "Particulars"
        Me.colParticulars.Name = "colParticulars"
        Me.colParticulars.ReadOnly = True
        '
        'colAmount
        '
        Me.colAmount.HeaderText = "Amount"
        Me.colAmount.Name = "colAmount"
        Me.colAmount.ReadOnly = True
        '
        'colPreparedBy
        '
        Me.colPreparedBy.HeaderText = "PreparedBy"
        Me.colPreparedBy.Name = "colPreparedBy"
        Me.colPreparedBy.ReadOnly = True
        '
        'colCheckedBy
        '
        Me.colCheckedBy.HeaderText = "CheckedBy"
        Me.colCheckedBy.Name = "colCheckedBy"
        Me.colCheckedBy.ReadOnly = True
        '
        'colApprovedBy
        '
        Me.colApprovedBy.HeaderText = "ApprovedBy"
        Me.colApprovedBy.Name = "colApprovedBy"
        Me.colApprovedBy.ReadOnly = True
        '
        'colUpdatedDate
        '
        Me.colUpdatedDate.HeaderText = "UpdatedDate"
        Me.colUpdatedDate.Name = "colUpdatedDate"
        Me.colUpdatedDate.ReadOnly = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Blue
        Me.btnClose.Image = Global.AccountsManagementForms.My.Resources.Resources.close
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(641, 461)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(155, 29)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmCollectionDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(802, 491)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmCollectionDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Collection Details"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.DGridViewDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGridViewMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtBatchCode As System.Windows.Forms.TextBox
    Friend WithEvents txtBillingPeriod As System.Windows.Forms.TextBox
    Friend WithEvents txtStlRun As System.Windows.Forms.TextBox
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtDueDate As System.Windows.Forms.TextBox
    Friend WithEvents txtInvoiceDate As System.Windows.Forms.TextBox
    Friend WithEvents txtParticipantID As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents DGridViewMain As System.Windows.Forms.DataGridView
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents DGridViewDetails As System.Windows.Forms.DataGridView
    Friend WithEvents colInvDMCMValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantID1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAcctCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDebit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCredit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDMCMNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colJVNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticulars As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPreparedBy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCheckedBy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colApprovedBy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUpdatedDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class
