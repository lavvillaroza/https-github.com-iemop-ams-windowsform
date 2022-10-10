<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrudentialComputeMarginCallSearch
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
        Me.gpDueDate = New System.Windows.Forms.GroupBox()
        Me.dtMarginCallDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.gpSearch = New System.Windows.Forms.GroupBox()
        Me.chckParticipantID = New System.Windows.Forms.CheckBox()
        Me.ddlParticipantID = New System.Windows.Forms.ComboBox()
        Me.ddlTransDate = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.gpDueDate.SuspendLayout()
        Me.gpSearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'gpDueDate
        '
        Me.gpDueDate.Controls.Add(Me.dtMarginCallDate)
        Me.gpDueDate.Controls.Add(Me.Label1)
        Me.gpDueDate.ForeColor = System.Drawing.Color.Black
        Me.gpDueDate.Location = New System.Drawing.Point(11, 12)
        Me.gpDueDate.Name = "gpDueDate"
        Me.gpDueDate.Size = New System.Drawing.Size(281, 42)
        Me.gpDueDate.TabIndex = 0
        Me.gpDueDate.TabStop = False
        '
        'dtMarginCallDate
        '
        Me.dtMarginCallDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtMarginCallDate.Location = New System.Drawing.Point(69, 15)
        Me.dtMarginCallDate.Name = "dtMarginCallDate"
        Me.dtMarginCallDate.Size = New System.Drawing.Size(94, 20)
        Me.dtMarginCallDate.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "MC Date:"
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Image = Global.AccountsManagementForms.My.Resources.Resources.OkIcon22x22
        Me.btnOK.Location = New System.Drawing.Point(196, 134)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(45, 39)
        Me.btnOK.TabIndex = 1
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Image = Global.AccountsManagementForms.My.Resources.Resources.CancelIconRed22x22
        Me.btnCancel.Location = New System.Drawing.Point(247, 134)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(45, 39)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'gpSearch
        '
        Me.gpSearch.Controls.Add(Me.chckParticipantID)
        Me.gpSearch.Controls.Add(Me.ddlParticipantID)
        Me.gpSearch.Controls.Add(Me.ddlTransDate)
        Me.gpSearch.Controls.Add(Me.Label2)
        Me.gpSearch.ForeColor = System.Drawing.Color.Black
        Me.gpSearch.Location = New System.Drawing.Point(11, 60)
        Me.gpSearch.Name = "gpSearch"
        Me.gpSearch.Size = New System.Drawing.Size(281, 68)
        Me.gpSearch.TabIndex = 4
        Me.gpSearch.TabStop = False
        '
        'chckParticipantID
        '
        Me.chckParticipantID.AutoSize = True
        Me.chckParticipantID.Location = New System.Drawing.Point(12, 42)
        Me.chckParticipantID.Name = "chckParticipantID"
        Me.chckParticipantID.Size = New System.Drawing.Size(89, 16)
        Me.chckParticipantID.TabIndex = 7
        Me.chckParticipantID.Text = "ParticipantID:"
        Me.chckParticipantID.UseVisualStyleBackColor = True
        '
        'ddlParticipantID
        '
        Me.ddlParticipantID.FormattingEnabled = True
        Me.ddlParticipantID.Location = New System.Drawing.Point(111, 40)
        Me.ddlParticipantID.Name = "ddlParticipantID"
        Me.ddlParticipantID.Size = New System.Drawing.Size(160, 20)
        Me.ddlParticipantID.TabIndex = 6
        '
        'ddlTransDate
        '
        Me.ddlTransDate.FormattingEnabled = True
        Me.ddlTransDate.Location = New System.Drawing.Point(111, 14)
        Me.ddlTransDate.Name = "ddlTransDate"
        Me.ddlTransDate.Size = New System.Drawing.Size(160, 20)
        Me.ddlTransDate.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Transaction Date:"
        '
        'frmPrudentialComputeMarginCallSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(300, 182)
        Me.Controls.Add(Me.gpSearch)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.gpDueDate)
        Me.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmPrudentialComputeMarginCallSearch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Search"
        Me.gpDueDate.ResumeLayout(False)
        Me.gpDueDate.PerformLayout()
        Me.gpSearch.ResumeLayout(False)
        Me.gpSearch.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gpDueDate As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtMarginCallDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents gpSearch As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ddlParticipantID As System.Windows.Forms.ComboBox
    Friend WithEvents ddlTransDate As System.Windows.Forms.ComboBox
    Friend WithEvents chckParticipantID As System.Windows.Forms.CheckBox
End Class
