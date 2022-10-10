<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdateWESMBillSummaryMgt
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BillingPeriodNo_TextBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.InvoiceNo_TextBox = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ChargeType_TextBox = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.EWTBalance_TextBox = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CurrentBalance_TextBox = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.BegginingBalance_TextBox = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.CurDueDate_DatePicker = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ChangeParticipantID_ComboBox = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.InvChangeHistory_DGV = New System.Windows.Forms.DataGridView()
        Me.OrigDueDate_TextBox = New System.Windows.Forms.TextBox()
        Me.GroupBox2.SuspendLayout()
        CType(Me.InvChangeHistory_DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(53, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 12)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Billing Period No.:"
        '
        'BillingPeriodNo_TextBox
        '
        Me.BillingPeriodNo_TextBox.BackColor = System.Drawing.SystemColors.Info
        Me.BillingPeriodNo_TextBox.Location = New System.Drawing.Point(181, 46)
        Me.BillingPeriodNo_TextBox.Name = "BillingPeriodNo_TextBox"
        Me.BillingPeriodNo_TextBox.Size = New System.Drawing.Size(98, 20)
        Me.BillingPeriodNo_TextBox.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(53, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 12)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Invoice No.:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(53, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(94, 12)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Original Due Date:"
        '
        'InvoiceNo_TextBox
        '
        Me.InvoiceNo_TextBox.BackColor = System.Drawing.SystemColors.Info
        Me.InvoiceNo_TextBox.Location = New System.Drawing.Point(181, 72)
        Me.InvoiceNo_TextBox.Name = "InvoiceNo_TextBox"
        Me.InvoiceNo_TextBox.Size = New System.Drawing.Size(122, 20)
        Me.InvoiceNo_TextBox.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(53, 130)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 12)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Current Due Date:"
        '
        'ChargeType_TextBox
        '
        Me.ChargeType_TextBox.BackColor = System.Drawing.SystemColors.Info
        Me.ChargeType_TextBox.Location = New System.Drawing.Point(536, 22)
        Me.ChargeType_TextBox.Name = "ChargeType_TextBox"
        Me.ChargeType_TextBox.Size = New System.Drawing.Size(98, 20)
        Me.ChargeType_TextBox.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(420, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 12)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Charge Type:"
        '
        'EWTBalance_TextBox
        '
        Me.EWTBalance_TextBox.BackColor = System.Drawing.Color.White
        Me.EWTBalance_TextBox.Location = New System.Drawing.Point(536, 49)
        Me.EWTBalance_TextBox.Name = "EWTBalance_TextBox"
        Me.EWTBalance_TextBox.Size = New System.Drawing.Size(98, 20)
        Me.EWTBalance_TextBox.TabIndex = 7
        Me.EWTBalance_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(420, 52)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 12)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "EWT Balance:"
        '
        'CurrentBalance_TextBox
        '
        Me.CurrentBalance_TextBox.BackColor = System.Drawing.SystemColors.Info
        Me.CurrentBalance_TextBox.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CurrentBalance_TextBox.Location = New System.Drawing.Point(536, 102)
        Me.CurrentBalance_TextBox.Name = "CurrentBalance_TextBox"
        Me.CurrentBalance_TextBox.Size = New System.Drawing.Size(144, 21)
        Me.CurrentBalance_TextBox.TabIndex = 9
        Me.CurrentBalance_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(421, 106)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(87, 12)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "Current Balance:"
        '
        'BegginingBalance_TextBox
        '
        Me.BegginingBalance_TextBox.BackColor = System.Drawing.SystemColors.Info
        Me.BegginingBalance_TextBox.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BegginingBalance_TextBox.Location = New System.Drawing.Point(536, 75)
        Me.BegginingBalance_TextBox.Name = "BegginingBalance_TextBox"
        Me.BegginingBalance_TextBox.Size = New System.Drawing.Size(144, 21)
        Me.BegginingBalance_TextBox.TabIndex = 8
        Me.BegginingBalance_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(421, 78)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(97, 12)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Beginning Balance:"
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.BackColor = System.Drawing.Color.White
        Me.cmdSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmdSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmdSave.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.Color.Black
        Me.cmdSave.Image = Global.AccountsManagementForms.My.Resources.Resources.SaveIconColored22x22
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSave.Location = New System.Drawing.Point(463, 508)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(121, 36)
        Me.cmdSave.TabIndex = 12
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.White
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.Black
        Me.btnCancel.Image = Global.AccountsManagementForms.My.Resources.Resources.BackRedIcon22x22
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(590, 508)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(121, 36)
        Me.btnCancel.TabIndex = 13
        Me.btnCancel.Text = "&Back"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'CurDueDate_DatePicker
        '
        Me.CurDueDate_DatePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.CurDueDate_DatePicker.Location = New System.Drawing.Point(181, 124)
        Me.CurDueDate_DatePicker.Name = "CurDueDate_DatePicker"
        Me.CurDueDate_DatePicker.Size = New System.Drawing.Size(122, 20)
        Me.CurDueDate_DatePicker.TabIndex = 5
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(53, 26)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(73, 12)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "Participant ID:"
        '
        'ChangeParticipantID_ComboBox
        '
        Me.ChangeParticipantID_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ChangeParticipantID_ComboBox.FormattingEnabled = True
        Me.ChangeParticipantID_ComboBox.Location = New System.Drawing.Point(181, 20)
        Me.ChangeParticipantID_ComboBox.Name = "ChangeParticipantID_ComboBox"
        Me.ChangeParticipantID_ComboBox.Size = New System.Drawing.Size(121, 20)
        Me.ChangeParticipantID_ComboBox.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.InvChangeHistory_DGV)
        Me.GroupBox2.Location = New System.Drawing.Point(15, 159)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(696, 341)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(125, 12)
        Me.Label10.TabIndex = 23
        Me.Label10.Text = "Invoice Changed History"
        '
        'InvChangeHistory_DGV
        '
        Me.InvChangeHistory_DGV.AllowUserToAddRows = False
        Me.InvChangeHistory_DGV.AllowUserToDeleteRows = False
        Me.InvChangeHistory_DGV.AllowUserToResizeColumns = False
        Me.InvChangeHistory_DGV.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.InvChangeHistory_DGV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.InvChangeHistory_DGV.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InvChangeHistory_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.InvChangeHistory_DGV.Location = New System.Drawing.Point(6, 19)
        Me.InvChangeHistory_DGV.Name = "InvChangeHistory_DGV"
        Me.InvChangeHistory_DGV.ReadOnly = True
        Me.InvChangeHistory_DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.InvChangeHistory_DGV.Size = New System.Drawing.Size(683, 316)
        Me.InvChangeHistory_DGV.TabIndex = 11
        '
        'OrigDueDate_TextBox
        '
        Me.OrigDueDate_TextBox.BackColor = System.Drawing.SystemColors.Info
        Me.OrigDueDate_TextBox.Location = New System.Drawing.Point(181, 98)
        Me.OrigDueDate_TextBox.Name = "OrigDueDate_TextBox"
        Me.OrigDueDate_TextBox.Size = New System.Drawing.Size(122, 20)
        Me.OrigDueDate_TextBox.TabIndex = 4
        '
        'frmUpdateWESMBillSummaryMgt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(727, 556)
        Me.ControlBox = False
        Me.Controls.Add(Me.OrigDueDate_TextBox)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.ChangeParticipantID_ComboBox)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.CurDueDate_DatePicker)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.CurrentBalance_TextBox)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.BegginingBalance_TextBox)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.EWTBalance_TextBox)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ChargeType_TextBox)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.InvoiceNo_TextBox)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.BillingPeriodNo_TextBox)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUpdateWESMBillSummaryMgt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WESM Bill Summary Adjustment Per Participant Management"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.InvChangeHistory_DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BillingPeriodNo_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents InvoiceNo_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ChargeType_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents EWTBalance_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CurrentBalance_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents BegginingBalance_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents CurDueDate_DatePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ChangeParticipantID_ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents InvChangeHistory_DGV As System.Windows.Forms.DataGridView
    Friend WithEvents OrigDueDate_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
