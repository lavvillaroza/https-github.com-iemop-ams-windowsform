<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCollectionSumary
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
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.chckNotPost = New System.Windows.Forms.CheckBox()
        Me.chckPosted = New System.Windows.Forms.CheckBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.rbAllocationDate = New System.Windows.Forms.RadioButton()
        Me.rbCollectionDate = New System.Windows.Forms.RadioButton()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.chckManual = New System.Windows.Forms.CheckBox()
        Me.chckAuto = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.chckUnallocated = New System.Windows.Forms.CheckBox()
        Me.chckAllocated = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ddlParticipantID = New System.Windows.Forms.ComboBox()
        Me.chckParticipantID = New System.Windows.Forms.CheckBox()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(190, 201)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(110, 39)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnShow
        '
        Me.btnShow.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnShow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnShow.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShow.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShow.ForeColor = System.Drawing.Color.Black
        Me.btnShow.Image = Global.AccountsManagementForms.My.Resources.Resources.FileSearchIcon22x22
        Me.btnShow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnShow.Location = New System.Drawing.Point(74, 201)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(110, 39)
        Me.btnShow.TabIndex = 8
        Me.btnShow.Text = "&View/Print"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.chckNotPost)
        Me.GroupBox7.Controls.Add(Me.chckPosted)
        Me.GroupBox7.Controls.Add(Me.Label17)
        Me.GroupBox7.Location = New System.Drawing.Point(9, 158)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(284, 28)
        Me.GroupBox7.TabIndex = 29
        Me.GroupBox7.TabStop = False
        '
        'chckNotPost
        '
        Me.chckNotPost.AutoSize = True
        Me.chckNotPost.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckNotPost.ForeColor = System.Drawing.Color.Black
        Me.chckNotPost.Location = New System.Drawing.Point(157, 10)
        Me.chckNotPost.Name = "chckNotPost"
        Me.chckNotPost.Size = New System.Drawing.Size(63, 16)
        Me.chckNotPost.TabIndex = 15
        Me.chckNotPost.Text = "&NotPost"
        Me.chckNotPost.UseVisualStyleBackColor = True
        '
        'chckPosted
        '
        Me.chckPosted.AutoSize = True
        Me.chckPosted.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckPosted.ForeColor = System.Drawing.Color.Black
        Me.chckPosted.Location = New System.Drawing.Point(80, 10)
        Me.chckPosted.Name = "chckPosted"
        Me.chckPosted.Size = New System.Drawing.Size(58, 16)
        Me.chckPosted.TabIndex = 14
        Me.chckPosted.Text = "&Posted"
        Me.chckPosted.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(8, 9)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(57, 14)
        Me.Label17.TabIndex = 25
        Me.Label17.Text = "IsPosted:"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.rbAllocationDate)
        Me.GroupBox6.Controls.Add(Me.rbCollectionDate)
        Me.GroupBox6.Controls.Add(Me.dtFrom)
        Me.GroupBox6.Controls.Add(Me.Label6)
        Me.GroupBox6.Controls.Add(Me.dtTo)
        Me.GroupBox6.Controls.Add(Me.Label8)
        Me.GroupBox6.Location = New System.Drawing.Point(9, 33)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(285, 63)
        Me.GroupBox6.TabIndex = 26
        Me.GroupBox6.TabStop = False
        '
        'rbAllocationDate
        '
        Me.rbAllocationDate.AutoSize = True
        Me.rbAllocationDate.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbAllocationDate.ForeColor = System.Drawing.Color.Black
        Me.rbAllocationDate.Location = New System.Drawing.Point(5, 37)
        Me.rbAllocationDate.Name = "rbAllocationDate"
        Me.rbAllocationDate.Size = New System.Drawing.Size(95, 16)
        Me.rbAllocationDate.TabIndex = 20
        Me.rbAllocationDate.TabStop = True
        Me.rbAllocationDate.Text = "&Allocation Date"
        Me.rbAllocationDate.UseVisualStyleBackColor = True
        '
        'rbCollectionDate
        '
        Me.rbCollectionDate.AutoSize = True
        Me.rbCollectionDate.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbCollectionDate.ForeColor = System.Drawing.Color.Black
        Me.rbCollectionDate.Location = New System.Drawing.Point(5, 12)
        Me.rbCollectionDate.Name = "rbCollectionDate"
        Me.rbCollectionDate.Size = New System.Drawing.Size(96, 16)
        Me.rbCollectionDate.TabIndex = 19
        Me.rbCollectionDate.TabStop = True
        Me.rbCollectionDate.Text = "&Collection Date"
        Me.rbCollectionDate.UseVisualStyleBackColor = True
        '
        'dtFrom
        '
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFrom.Location = New System.Drawing.Point(174, 12)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(104, 20)
        Me.dtFrom.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(104, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 14)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Date From:"
        '
        'dtTo
        '
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTo.Location = New System.Drawing.Point(174, 36)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(104, 20)
        Me.dtTo.TabIndex = 18
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(117, 39)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 14)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Date To:"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.chckManual)
        Me.GroupBox5.Controls.Add(Me.chckAuto)
        Me.GroupBox5.Controls.Add(Me.Label5)
        Me.GroupBox5.Location = New System.Drawing.Point(9, 102)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(284, 28)
        Me.GroupBox5.TabIndex = 25
        Me.GroupBox5.TabStop = False
        '
        'chckManual
        '
        Me.chckManual.AutoSize = True
        Me.chckManual.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckManual.ForeColor = System.Drawing.Color.Black
        Me.chckManual.Location = New System.Drawing.Point(157, 10)
        Me.chckManual.Name = "chckManual"
        Me.chckManual.Size = New System.Drawing.Size(59, 16)
        Me.chckManual.TabIndex = 15
        Me.chckManual.Text = "&Manual"
        Me.chckManual.UseVisualStyleBackColor = True
        '
        'chckAuto
        '
        Me.chckAuto.AutoSize = True
        Me.chckAuto.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckAuto.ForeColor = System.Drawing.Color.Black
        Me.chckAuto.Location = New System.Drawing.Point(80, 10)
        Me.chckAuto.Name = "chckAuto"
        Me.chckAuto.Size = New System.Drawing.Size(72, 16)
        Me.chckAuto.TabIndex = 14
        Me.chckAuto.Text = "&Automatic"
        Me.chckAuto.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(26, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 14)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "Type:"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.chckUnallocated)
        Me.GroupBox4.Controls.Add(Me.chckAllocated)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Location = New System.Drawing.Point(9, 130)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(284, 28)
        Me.GroupBox4.TabIndex = 24
        Me.GroupBox4.TabStop = False
        '
        'chckUnallocated
        '
        Me.chckUnallocated.AutoSize = True
        Me.chckUnallocated.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckUnallocated.ForeColor = System.Drawing.Color.Black
        Me.chckUnallocated.Location = New System.Drawing.Point(157, 7)
        Me.chckUnallocated.Name = "chckUnallocated"
        Me.chckUnallocated.Size = New System.Drawing.Size(81, 16)
        Me.chckUnallocated.TabIndex = 15
        Me.chckUnallocated.Text = "&Unallocated"
        Me.chckUnallocated.UseVisualStyleBackColor = True
        '
        'chckAllocated
        '
        Me.chckAllocated.AutoSize = True
        Me.chckAllocated.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckAllocated.ForeColor = System.Drawing.Color.Black
        Me.chckAllocated.Location = New System.Drawing.Point(80, 10)
        Me.chckAllocated.Name = "chckAllocated"
        Me.chckAllocated.Size = New System.Drawing.Size(68, 16)
        Me.chckAllocated.TabIndex = 14
        Me.chckAllocated.Text = "&Allocated"
        Me.chckAllocated.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(20, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 14)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Status:"
        '
        'ddlParticipantID
        '
        Me.ddlParticipantID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlParticipantID.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlParticipantID.FormattingEnabled = True
        Me.ddlParticipantID.Location = New System.Drawing.Point(119, 12)
        Me.ddlParticipantID.Name = "ddlParticipantID"
        Me.ddlParticipantID.Size = New System.Drawing.Size(174, 20)
        Me.ddlParticipantID.TabIndex = 28
        '
        'chckParticipantID
        '
        Me.chckParticipantID.AutoSize = True
        Me.chckParticipantID.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckParticipantID.ForeColor = System.Drawing.Color.Black
        Me.chckParticipantID.Location = New System.Drawing.Point(21, 15)
        Me.chckParticipantID.Name = "chckParticipantID"
        Me.chckParticipantID.Size = New System.Drawing.Size(92, 16)
        Me.chckParticipantID.TabIndex = 27
        Me.chckParticipantID.Text = "&Participant ID:"
        Me.chckParticipantID.UseVisualStyleBackColor = True
        '
        'frmCollectionSumary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(302, 253)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.ddlParticipantID)
        Me.Controls.Add(Me.chckParticipantID)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnShow)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmCollectionSumary"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Collection Summary"
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents chckNotPost As System.Windows.Forms.CheckBox
    Friend WithEvents chckPosted As System.Windows.Forms.CheckBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents rbAllocationDate As System.Windows.Forms.RadioButton
    Friend WithEvents rbCollectionDate As System.Windows.Forms.RadioButton
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents chckManual As System.Windows.Forms.CheckBox
    Friend WithEvents chckAuto As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents chckUnallocated As System.Windows.Forms.CheckBox
    Friend WithEvents chckAllocated As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ddlParticipantID As System.Windows.Forms.ComboBox
    Friend WithEvents chckParticipantID As System.Windows.Forms.CheckBox
End Class
