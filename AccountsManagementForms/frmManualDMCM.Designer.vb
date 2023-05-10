<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManualDMCM
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.gpMain = New System.Windows.Forms.GroupBox()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.txtJVNo = New System.Windows.Forms.TextBox()
        Me.txtDMCMNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtParticipantName = New System.Windows.Forms.TextBox()
        Me.txtIDNumber = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ddlParticipantID = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DGridView = New System.Windows.Forms.DataGridView()
        Me.colAcctCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAcctCodeButton = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.colDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDebit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCredit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtParticulars = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.gpDetails = New System.Windows.Forms.GroupBox()
        Me.rbVATonEnergy = New System.Windows.Forms.RadioButton()
        Me.rbEnergy = New System.Windows.Forms.RadioButton()
        Me.rbVATonMarketFees = New System.Windows.Forms.RadioButton()
        Me.rbMarketFees = New System.Windows.Forms.RadioButton()
        Me.txtVATonEnergy = New System.Windows.Forms.TextBox()
        Me.txtEnergy = New System.Windows.Forms.TextBox()
        Me.txtVATonMarketFees = New System.Windows.Forms.TextBox()
        Me.txtMarketFees = New System.Windows.Forms.TextBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnPrintJV = New System.Windows.Forms.Button()
        Me.btnPrintDMCM = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.gpMain.SuspendLayout()
        CType(Me.DGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpDetails.SuspendLayout()
        Me.SuspendLayout()
        '
        'gpMain
        '
        Me.gpMain.Controls.Add(Me.txtDate)
        Me.gpMain.Controls.Add(Me.txtJVNo)
        Me.gpMain.Controls.Add(Me.txtDMCMNo)
        Me.gpMain.Controls.Add(Me.Label7)
        Me.gpMain.Controls.Add(Me.Label6)
        Me.gpMain.Controls.Add(Me.Label5)
        Me.gpMain.Controls.Add(Me.txtParticipantName)
        Me.gpMain.Controls.Add(Me.txtIDNumber)
        Me.gpMain.Controls.Add(Me.Label3)
        Me.gpMain.Controls.Add(Me.Label2)
        Me.gpMain.Controls.Add(Me.ddlParticipantID)
        Me.gpMain.Controls.Add(Me.Label1)
        Me.gpMain.Location = New System.Drawing.Point(12, 5)
        Me.gpMain.Name = "gpMain"
        Me.gpMain.Size = New System.Drawing.Size(765, 83)
        Me.gpMain.TabIndex = 0
        Me.gpMain.TabStop = False
        '
        'txtDate
        '
        Me.txtDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDate.Location = New System.Drawing.Point(499, 58)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.ReadOnly = True
        Me.txtDate.Size = New System.Drawing.Size(253, 20)
        Me.txtDate.TabIndex = 17
        '
        'txtJVNo
        '
        Me.txtJVNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtJVNo.Location = New System.Drawing.Point(499, 35)
        Me.txtJVNo.Name = "txtJVNo"
        Me.txtJVNo.ReadOnly = True
        Me.txtJVNo.Size = New System.Drawing.Size(253, 20)
        Me.txtJVNo.TabIndex = 13
        '
        'txtDMCMNo
        '
        Me.txtDMCMNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDMCMNo.Location = New System.Drawing.Point(499, 13)
        Me.txtDMCMNo.Name = "txtDMCMNo"
        Me.txtDMCMNo.ReadOnly = True
        Me.txtDMCMNo.Size = New System.Drawing.Size(253, 20)
        Me.txtDMCMNo.TabIndex = 12
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(458, 59)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 14)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Date:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(448, 37)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 14)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "JV No.:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(427, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 14)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "DMCM No.:"
        '
        'txtParticipantName
        '
        Me.txtParticipantName.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtParticipantName.Location = New System.Drawing.Point(107, 57)
        Me.txtParticipantName.Name = "txtParticipantName"
        Me.txtParticipantName.ReadOnly = True
        Me.txtParticipantName.Size = New System.Drawing.Size(253, 20)
        Me.txtParticipantName.TabIndex = 5
        '
        'txtIDNumber
        '
        Me.txtIDNumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtIDNumber.Location = New System.Drawing.Point(107, 34)
        Me.txtIDNumber.Name = "txtIDNumber"
        Me.txtIDNumber.ReadOnly = True
        Me.txtIDNumber.Size = New System.Drawing.Size(253, 20)
        Me.txtIDNumber.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(34, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 14)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "ID Number:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Participant Name:"
        '
        'ddlParticipantID
        '
        Me.ddlParticipantID.FormattingEnabled = True
        Me.ddlParticipantID.Location = New System.Drawing.Point(107, 12)
        Me.ddlParticipantID.Name = "ddlParticipantID"
        Me.ddlParticipantID.Size = New System.Drawing.Size(253, 20)
        Me.ddlParticipantID.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(19, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Participant ID:"
        '
        'DGridView
        '
        Me.DGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colAcctCode, Me.colAcctCodeButton, Me.colDescription, Me.colDebit, Me.colCredit})
        Me.DGridView.Location = New System.Drawing.Point(10, 236)
        Me.DGridView.Name = "DGridView"
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGridView.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DGridView.Size = New System.Drawing.Size(768, 338)
        Me.DGridView.TabIndex = 9
        '
        'colAcctCode
        '
        Me.colAcctCode.HeaderText = "AccountCode"
        Me.colAcctCode.Name = "colAcctCode"
        Me.colAcctCode.ReadOnly = True
        Me.colAcctCode.Width = 120
        '
        'colAcctCodeButton
        '
        Me.colAcctCodeButton.HeaderText = ""
        Me.colAcctCodeButton.Name = "colAcctCodeButton"
        Me.colAcctCodeButton.Text = "..."
        Me.colAcctCodeButton.UseColumnTextForButtonValue = True
        Me.colAcctCodeButton.Width = 30
        '
        'colDescription
        '
        Me.colDescription.HeaderText = "Description"
        Me.colDescription.Name = "colDescription"
        Me.colDescription.ReadOnly = True
        Me.colDescription.Width = 280
        '
        'colDebit
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        Me.colDebit.DefaultCellStyle = DataGridViewCellStyle1
        Me.colDebit.HeaderText = "Debit"
        Me.colDebit.Name = "colDebit"
        Me.colDebit.Width = 120
        '
        'colCredit
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        Me.colCredit.DefaultCellStyle = DataGridViewCellStyle2
        Me.colCredit.HeaderText = "Credit"
        Me.colCredit.Name = "colCredit"
        Me.colCredit.Width = 120
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(134, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(63, 14)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Particulars"
        '
        'txtParticulars
        '
        Me.txtParticulars.Location = New System.Drawing.Point(8, 31)
        Me.txtParticulars.Multiline = True
        Me.txtParticulars.Name = "txtParticulars"
        Me.txtParticulars.Size = New System.Drawing.Size(350, 90)
        Me.txtParticulars.TabIndex = 8
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(596, 16)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(50, 14)
        Me.Label16.TabIndex = 16
        Me.Label16.Text = "Amount"
        '
        'gpDetails
        '
        Me.gpDetails.Controls.Add(Me.rbVATonEnergy)
        Me.gpDetails.Controls.Add(Me.rbEnergy)
        Me.gpDetails.Controls.Add(Me.rbVATonMarketFees)
        Me.gpDetails.Controls.Add(Me.rbMarketFees)
        Me.gpDetails.Controls.Add(Me.txtVATonEnergy)
        Me.gpDetails.Controls.Add(Me.txtEnergy)
        Me.gpDetails.Controls.Add(Me.txtVATonMarketFees)
        Me.gpDetails.Controls.Add(Me.txtMarketFees)
        Me.gpDetails.Controls.Add(Me.Label16)
        Me.gpDetails.Controls.Add(Me.txtParticulars)
        Me.gpDetails.Controls.Add(Me.Label8)
        Me.gpDetails.Location = New System.Drawing.Point(12, 94)
        Me.gpDetails.Name = "gpDetails"
        Me.gpDetails.Size = New System.Drawing.Size(765, 131)
        Me.gpDetails.TabIndex = 8
        Me.gpDetails.TabStop = False
        '
        'rbVATonEnergy
        '
        Me.rbVATonEnergy.AutoSize = True
        Me.rbVATonEnergy.Location = New System.Drawing.Point(370, 101)
        Me.rbVATonEnergy.Name = "rbVATonEnergy"
        Me.rbVATonEnergy.Size = New System.Drawing.Size(97, 16)
        Me.rbVATonEnergy.TabIndex = 28
        Me.rbVATonEnergy.TabStop = True
        Me.rbVATonEnergy.Text = "VAT on Energy"
        Me.rbVATonEnergy.UseVisualStyleBackColor = True
        '
        'rbEnergy
        '
        Me.rbEnergy.AutoSize = True
        Me.rbEnergy.Location = New System.Drawing.Point(370, 79)
        Me.rbEnergy.Name = "rbEnergy"
        Me.rbEnergy.Size = New System.Drawing.Size(58, 16)
        Me.rbEnergy.TabIndex = 27
        Me.rbEnergy.TabStop = True
        Me.rbEnergy.Text = "Energy"
        Me.rbEnergy.UseVisualStyleBackColor = True
        '
        'rbVATonMarketFees
        '
        Me.rbVATonMarketFees.AutoSize = True
        Me.rbVATonMarketFees.Location = New System.Drawing.Point(370, 58)
        Me.rbVATonMarketFees.Name = "rbVATonMarketFees"
        Me.rbVATonMarketFees.Size = New System.Drawing.Size(124, 16)
        Me.rbVATonMarketFees.TabIndex = 26
        Me.rbVATonMarketFees.TabStop = True
        Me.rbVATonMarketFees.Text = "VAT on Market Fees"
        Me.rbVATonMarketFees.UseVisualStyleBackColor = True
        '
        'rbMarketFees
        '
        Me.rbMarketFees.AutoSize = True
        Me.rbMarketFees.Location = New System.Drawing.Point(370, 35)
        Me.rbMarketFees.Name = "rbMarketFees"
        Me.rbMarketFees.Size = New System.Drawing.Size(85, 16)
        Me.rbMarketFees.TabIndex = 25
        Me.rbMarketFees.TabStop = True
        Me.rbMarketFees.Text = "Market Fees"
        Me.rbMarketFees.UseVisualStyleBackColor = True
        '
        'txtVATonEnergy
        '
        Me.txtVATonEnergy.Location = New System.Drawing.Point(500, 101)
        Me.txtVATonEnergy.Name = "txtVATonEnergy"
        Me.txtVATonEnergy.Size = New System.Drawing.Size(253, 20)
        Me.txtVATonEnergy.TabIndex = 20
        '
        'txtEnergy
        '
        Me.txtEnergy.Location = New System.Drawing.Point(500, 79)
        Me.txtEnergy.Name = "txtEnergy"
        Me.txtEnergy.Size = New System.Drawing.Size(253, 20)
        Me.txtEnergy.TabIndex = 19
        '
        'txtVATonMarketFees
        '
        Me.txtVATonMarketFees.Location = New System.Drawing.Point(500, 57)
        Me.txtVATonMarketFees.Name = "txtVATonMarketFees"
        Me.txtVATonMarketFees.Size = New System.Drawing.Size(253, 20)
        Me.txtVATonMarketFees.TabIndex = 18
        '
        'txtMarketFees
        '
        Me.txtMarketFees.Location = New System.Drawing.Point(500, 35)
        Me.txtMarketFees.Name = "txtMarketFees"
        Me.txtMarketFees.Size = New System.Drawing.Size(253, 20)
        Me.txtMarketFees.TabIndex = 17
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
        Me.btnClose.Location = New System.Drawing.Point(650, 591)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(137, 35)
        Me.btnClose.TabIndex = 48
        Me.btnClose.Text = "     &Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnPrintJV
        '
        Me.btnPrintJV.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnPrintJV.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnPrintJV.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnPrintJV.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintJV.ForeColor = System.Drawing.Color.Black
        Me.btnPrintJV.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.btnPrintJV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrintJV.Location = New System.Drawing.Point(507, 591)
        Me.btnPrintJV.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnPrintJV.Name = "btnPrintJV"
        Me.btnPrintJV.Size = New System.Drawing.Size(137, 35)
        Me.btnPrintJV.TabIndex = 47
        Me.btnPrintJV.Text = "     &Print JV"
        Me.btnPrintJV.UseVisualStyleBackColor = True
        '
        'btnPrintDMCM
        '
        Me.btnPrintDMCM.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnPrintDMCM.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnPrintDMCM.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnPrintDMCM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintDMCM.ForeColor = System.Drawing.Color.Black
        Me.btnPrintDMCM.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.btnPrintDMCM.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrintDMCM.Location = New System.Drawing.Point(368, 591)
        Me.btnPrintDMCM.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnPrintDMCM.Name = "btnPrintDMCM"
        Me.btnPrintDMCM.Size = New System.Drawing.Size(137, 35)
        Me.btnPrintDMCM.TabIndex = 46
        Me.btnPrintDMCM.Text = "     &Print DMCM"
        Me.btnPrintDMCM.UseVisualStyleBackColor = True
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
        Me.btnSave.Location = New System.Drawing.Point(229, 591)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(137, 35)
        Me.btnSave.TabIndex = 45
        Me.btnSave.Text = "     &Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNew.ForeColor = System.Drawing.Color.Black
        Me.btnNew.Image = Global.AccountsManagementForms.My.Resources.Resources.NewGreenIcon22x22
        Me.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNew.Location = New System.Drawing.Point(89, 591)
        Me.btnNew.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(137, 35)
        Me.btnNew.TabIndex = 44
        Me.btnNew.Text = "     &New"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'frmManualDMCM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(790, 637)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnPrintJV)
        Me.Controls.Add(Me.btnPrintDMCM)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.DGridView)
        Me.Controls.Add(Me.gpDetails)
        Me.Controls.Add(Me.gpMain)
        Me.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmManualDMCM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Manual DMCM"
        Me.gpMain.ResumeLayout(False)
        Me.gpMain.PerformLayout()
        CType(Me.DGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpDetails.ResumeLayout(False)
        Me.gpDetails.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gpMain As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtParticipantName As System.Windows.Forms.TextBox
    Friend WithEvents txtIDNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ddlParticipantID As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtJVNo As System.Windows.Forms.TextBox
    Friend WithEvents txtDMCMNo As System.Windows.Forms.TextBox
    Friend WithEvents DGridView As System.Windows.Forms.DataGridView
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnPrintDMCM As System.Windows.Forms.Button
    Friend WithEvents btnPrintJV As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtDate As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtParticulars As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents gpDetails As System.Windows.Forms.GroupBox
    Friend WithEvents txtVATonEnergy As System.Windows.Forms.TextBox
    Friend WithEvents txtEnergy As System.Windows.Forms.TextBox
    Friend WithEvents txtVATonMarketFees As System.Windows.Forms.TextBox
    Friend WithEvents txtMarketFees As System.Windows.Forms.TextBox
    Friend WithEvents rbMarketFees As System.Windows.Forms.RadioButton
    Friend WithEvents rbVATonEnergy As System.Windows.Forms.RadioButton
    Friend WithEvents rbEnergy As System.Windows.Forms.RadioButton
    Friend WithEvents rbVATonMarketFees As System.Windows.Forms.RadioButton
    Friend WithEvents colAcctCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAcctCodeButton As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents colDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDebit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCredit As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
