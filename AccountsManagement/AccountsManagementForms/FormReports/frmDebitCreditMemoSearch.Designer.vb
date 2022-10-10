<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDebitCreditMemoSearch
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
        Me.rbCollection = New System.Windows.Forms.RadioButton()
        Me.gpDetails = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chckParticipantID = New System.Windows.Forms.CheckBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.chckTransType = New System.Windows.Forms.CheckedListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ddlParticipant = New System.Windows.Forms.ComboBox()
        Me.ddlChargeType = New System.Windows.Forms.ComboBox()
        Me.ddlDueDate = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ddlBillingPeriod = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rbPayment = New System.Windows.Forms.RadioButton()
        Me.rbPrudential = New System.Windows.Forms.RadioButton()
        Me.rbOffsetting = New System.Windows.Forms.RadioButton()
        Me.gpMain = New System.Windows.Forms.GroupBox()
        Me.rbSettlement = New System.Windows.Forms.RadioButton()
        Me.rbSPA = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.rbwthtax = New System.Windows.Forms.RadioButton()
        Me.gpDetails.SuspendLayout()
        Me.gpMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'rbCollection
        '
        Me.rbCollection.AutoSize = True
        Me.rbCollection.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbCollection.Location = New System.Drawing.Point(9, 65)
        Me.rbCollection.Name = "rbCollection"
        Me.rbCollection.Size = New System.Drawing.Size(70, 16)
        Me.rbCollection.TabIndex = 1
        Me.rbCollection.TabStop = True
        Me.rbCollection.Text = "Collection"
        Me.rbCollection.UseVisualStyleBackColor = True
        '
        'gpDetails
        '
        Me.gpDetails.Controls.Add(Me.Label8)
        Me.gpDetails.Controls.Add(Me.Label5)
        Me.gpDetails.Controls.Add(Me.chckParticipantID)
        Me.gpDetails.Controls.Add(Me.btnClear)
        Me.gpDetails.Controls.Add(Me.btnSearch)
        Me.gpDetails.Controls.Add(Me.dtTo)
        Me.gpDetails.Controls.Add(Me.Label7)
        Me.gpDetails.Controls.Add(Me.dtFrom)
        Me.gpDetails.Controls.Add(Me.chckTransType)
        Me.gpDetails.Controls.Add(Me.Label3)
        Me.gpDetails.Controls.Add(Me.ddlParticipant)
        Me.gpDetails.Controls.Add(Me.ddlChargeType)
        Me.gpDetails.Controls.Add(Me.ddlDueDate)
        Me.gpDetails.Controls.Add(Me.Label4)
        Me.gpDetails.Controls.Add(Me.Label2)
        Me.gpDetails.Controls.Add(Me.ddlBillingPeriod)
        Me.gpDetails.Controls.Add(Me.Label1)
        Me.gpDetails.Location = New System.Drawing.Point(168, 12)
        Me.gpDetails.Name = "gpDetails"
        Me.gpDetails.Size = New System.Drawing.Size(420, 382)
        Me.gpDetails.TabIndex = 2
        Me.gpDetails.TabStop = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(16, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 14)
        Me.Label8.TabIndex = 26
        Me.Label8.Text = "Filters:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(21, 183)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 14)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "Transaction Date:"
        '
        'chckParticipantID
        '
        Me.chckParticipantID.AutoSize = True
        Me.chckParticipantID.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckParticipantID.Location = New System.Drawing.Point(23, 299)
        Me.chckParticipantID.Name = "chckParticipantID"
        Me.chckParticipantID.Size = New System.Drawing.Size(101, 18)
        Me.chckParticipantID.TabIndex = 24
        Me.chckParticipantID.Text = "Participant ID:"
        Me.chckParticipantID.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.Black
        Me.btnClear.Image = Global.AccountsManagementForms.My.Resources.Resources.CancelIconRed22x22
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.Location = New System.Drawing.Point(320, 324)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(93, 39)
        Me.btnClear.TabIndex = 12
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.ForeColor = System.Drawing.Color.Black
        Me.btnSearch.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(221, 324)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(93, 39)
        Me.btnSearch.TabIndex = 11
        Me.btnSearch.Text = "  Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'dtTo
        '
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTo.Location = New System.Drawing.Point(295, 181)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(119, 20)
        Me.dtTo.TabIndex = 23
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(267, 181)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(14, 12)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "to"
        '
        'dtFrom
        '
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFrom.Location = New System.Drawing.Point(128, 180)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(133, 20)
        Me.dtFrom.TabIndex = 21
        '
        'chckTransType
        '
        Me.chckTransType.CheckOnClick = True
        Me.chckTransType.FormattingEnabled = True
        Me.chckTransType.Location = New System.Drawing.Point(32, 32)
        Me.chckTransType.Name = "chckTransType"
        Me.chckTransType.Size = New System.Drawing.Size(381, 139)
        Me.chckTransType.TabIndex = 19
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(30, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 14)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Transaction Type:"
        '
        'ddlParticipant
        '
        Me.ddlParticipant.FormattingEnabled = True
        Me.ddlParticipant.Location = New System.Drawing.Point(130, 297)
        Me.ddlParticipant.Name = "ddlParticipant"
        Me.ddlParticipant.Size = New System.Drawing.Size(284, 20)
        Me.ddlParticipant.TabIndex = 11
        '
        'ddlChargeType
        '
        Me.ddlChargeType.FormattingEnabled = True
        Me.ddlChargeType.Location = New System.Drawing.Point(130, 270)
        Me.ddlChargeType.Name = "ddlChargeType"
        Me.ddlChargeType.Size = New System.Drawing.Size(284, 20)
        Me.ddlChargeType.TabIndex = 16
        '
        'ddlDueDate
        '
        Me.ddlDueDate.FormattingEnabled = True
        Me.ddlDueDate.Location = New System.Drawing.Point(130, 241)
        Me.ddlDueDate.Name = "ddlDueDate"
        Me.ddlDueDate.Size = New System.Drawing.Size(284, 20)
        Me.ddlDueDate.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(46, 272)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 14)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Charge Type:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(63, 243)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 14)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Due Date:"
        '
        'ddlBillingPeriod
        '
        Me.ddlBillingPeriod.FormattingEnabled = True
        Me.ddlBillingPeriod.Location = New System.Drawing.Point(130, 215)
        Me.ddlBillingPeriod.Name = "ddlBillingPeriod"
        Me.ddlBillingPeriod.Size = New System.Drawing.Size(284, 20)
        Me.ddlBillingPeriod.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(42, 217)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 14)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Billing Period:"
        '
        'rbPayment
        '
        Me.rbPayment.AutoSize = True
        Me.rbPayment.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbPayment.Location = New System.Drawing.Point(9, 97)
        Me.rbPayment.Name = "rbPayment"
        Me.rbPayment.Size = New System.Drawing.Size(66, 16)
        Me.rbPayment.TabIndex = 4
        Me.rbPayment.TabStop = True
        Me.rbPayment.Text = "Payment"
        Me.rbPayment.UseVisualStyleBackColor = True
        '
        'rbPrudential
        '
        Me.rbPrudential.AutoSize = True
        Me.rbPrudential.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbPrudential.Location = New System.Drawing.Point(9, 131)
        Me.rbPrudential.Name = "rbPrudential"
        Me.rbPrudential.Size = New System.Drawing.Size(111, 16)
        Me.rbPrudential.TabIndex = 6
        Me.rbPrudential.TabStop = True
        Me.rbPrudential.Text = "Prudential Interest"
        Me.rbPrudential.UseVisualStyleBackColor = True
        '
        'rbOffsetting
        '
        Me.rbOffsetting.AutoSize = True
        Me.rbOffsetting.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbOffsetting.Location = New System.Drawing.Point(9, 32)
        Me.rbOffsetting.Name = "rbOffsetting"
        Me.rbOffsetting.Size = New System.Drawing.Size(122, 16)
        Me.rbOffsetting.TabIndex = 8
        Me.rbOffsetting.TabStop = True
        Me.rbOffsetting.Text = "WESM Bill Offsetting"
        Me.rbOffsetting.UseVisualStyleBackColor = True
        '
        'gpMain
        '
        Me.gpMain.Controls.Add(Me.rbwthtax)
        Me.gpMain.Controls.Add(Me.rbSettlement)
        Me.gpMain.Controls.Add(Me.rbSPA)
        Me.gpMain.Controls.Add(Me.Label6)
        Me.gpMain.Controls.Add(Me.rbPrudential)
        Me.gpMain.Controls.Add(Me.rbOffsetting)
        Me.gpMain.Controls.Add(Me.rbPayment)
        Me.gpMain.Controls.Add(Me.rbCollection)
        Me.gpMain.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gpMain.Location = New System.Drawing.Point(12, 12)
        Me.gpMain.Name = "gpMain"
        Me.gpMain.Size = New System.Drawing.Size(150, 382)
        Me.gpMain.TabIndex = 9
        Me.gpMain.TabStop = False
        '
        'rbSettlement
        '
        Me.rbSettlement.AutoSize = True
        Me.rbSettlement.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbSettlement.Location = New System.Drawing.Point(9, 166)
        Me.rbSettlement.Name = "rbSettlement"
        Me.rbSettlement.Size = New System.Drawing.Size(114, 16)
        Me.rbSettlement.TabIndex = 12
        Me.rbSettlement.TabStop = True
        Me.rbSettlement.Text = "Settlement Interest"
        Me.rbSettlement.UseVisualStyleBackColor = True
        '
        'rbSPA
        '
        Me.rbSPA.AutoSize = True
        Me.rbSPA.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbSPA.Location = New System.Drawing.Point(9, 198)
        Me.rbSPA.Name = "rbSPA"
        Me.rbSPA.Size = New System.Drawing.Size(44, 16)
        Me.rbSPA.TabIndex = 11
        Me.rbSPA.TabStop = True
        Me.rbSPA.Text = "SPA"
        Me.rbSPA.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(6, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 14)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "DMCM Type:"
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
        Me.btnClose.Location = New System.Drawing.Point(448, 406)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(140, 39)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'rbwthtax
        '
        Me.rbwthtax.AutoSize = True
        Me.rbwthtax.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbwthtax.Location = New System.Drawing.Point(9, 230)
        Me.rbwthtax.Name = "rbwthtax"
        Me.rbwthtax.Size = New System.Drawing.Size(117, 16)
        Me.rbwthtax.TabIndex = 13
        Me.rbwthtax.TabStop = True
        Me.rbwthtax.Text = "WHTax Adjustment"
        Me.rbwthtax.UseVisualStyleBackColor = True
        '
        'frmDebitCreditMemoSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(593, 457)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.gpMain)
        Me.Controls.Add(Me.gpDetails)
        Me.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDebitCreditMemoSearch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DMCM Search Engine"
        Me.gpDetails.ResumeLayout(False)
        Me.gpDetails.PerformLayout()
        Me.gpMain.ResumeLayout(False)
        Me.gpMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rbCollection As System.Windows.Forms.RadioButton
    Friend WithEvents gpDetails As System.Windows.Forms.GroupBox
    Friend WithEvents rbPayment As System.Windows.Forms.RadioButton
    Friend WithEvents rbPrudential As System.Windows.Forms.RadioButton
    Friend WithEvents ddlParticipant As System.Windows.Forms.ComboBox
    Friend WithEvents ddlChargeType As System.Windows.Forms.ComboBox
    Friend WithEvents ddlDueDate As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ddlBillingPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rbOffsetting As System.Windows.Forms.RadioButton
    Friend WithEvents gpMain As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chckTransType As System.Windows.Forms.CheckedListBox
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents chckParticipantID As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents rbSPA As System.Windows.Forms.RadioButton
    Friend WithEvents rbSettlement As System.Windows.Forms.RadioButton
    Friend WithEvents rbwthtax As System.Windows.Forms.RadioButton
End Class
