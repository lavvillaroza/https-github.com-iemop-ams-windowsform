<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCollectionSearch
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCollectionSearch))
        Me.gpSearch = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.chckUnallocated = New System.Windows.Forms.CheckBox()
        Me.chckAllocated = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.rbAllocationDate = New System.Windows.Forms.RadioButton()
        Me.rbCollectionDate = New System.Windows.Forms.RadioButton()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.chckManual = New System.Windows.Forms.CheckBox()
        Me.chckAuto = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ddlParticipantID = New System.Windows.Forms.ComboBox()
        Me.chckParticipantID = New System.Windows.Forms.CheckBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.chckNotPost = New System.Windows.Forms.CheckBox()
        Me.chckPosted = New System.Windows.Forms.CheckBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.gpCollection = New System.Windows.Forms.GroupBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.lblTransaction = New System.Windows.Forms.Label()
        Me.dtAllocationDate = New System.Windows.Forms.DateTimePicker()
        Me.gpSearch.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.gpCollection.SuspendLayout()
        Me.SuspendLayout()
        '
        'gpSearch
        '
        Me.gpSearch.Controls.Add(Me.GroupBox4)
        Me.gpSearch.Controls.Add(Me.GroupBox6)
        Me.gpSearch.Controls.Add(Me.btnClose)
        Me.gpSearch.Controls.Add(Me.btnSearch)
        Me.gpSearch.Controls.Add(Me.GroupBox5)
        Me.gpSearch.Controls.Add(Me.ddlParticipantID)
        Me.gpSearch.Controls.Add(Me.chckParticipantID)
        Me.gpSearch.Controls.Add(Me.GroupBox7)
        Me.gpSearch.Location = New System.Drawing.Point(6, 4)
        Me.gpSearch.Name = "gpSearch"
        Me.gpSearch.Size = New System.Drawing.Size(279, 272)
        Me.gpSearch.TabIndex = 12
        Me.gpSearch.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.chckUnallocated)
        Me.GroupBox4.Controls.Add(Me.chckAllocated)
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Location = New System.Drawing.Point(13, 116)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(255, 28)
        Me.GroupBox4.TabIndex = 32
        Me.GroupBox4.TabStop = False
        '
        'chckUnallocated
        '
        Me.chckUnallocated.AutoSize = True
        Me.chckUnallocated.Checked = True
        Me.chckUnallocated.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chckUnallocated.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckUnallocated.ForeColor = System.Drawing.Color.Black
        Me.chckUnallocated.Location = New System.Drawing.Point(161, 10)
        Me.chckUnallocated.Name = "chckUnallocated"
        Me.chckUnallocated.Size = New System.Drawing.Size(82, 18)
        Me.chckUnallocated.TabIndex = 15
        Me.chckUnallocated.Text = "&Unallocated"
        Me.chckUnallocated.UseVisualStyleBackColor = True
        '
        'chckAllocated
        '
        Me.chckAllocated.AutoSize = True
        Me.chckAllocated.Checked = True
        Me.chckAllocated.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chckAllocated.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckAllocated.ForeColor = System.Drawing.Color.Black
        Me.chckAllocated.Location = New System.Drawing.Point(73, 10)
        Me.chckAllocated.Name = "chckAllocated"
        Me.chckAllocated.Size = New System.Drawing.Size(71, 18)
        Me.chckAllocated.TabIndex = 14
        Me.chckAllocated.Text = "&Allocated"
        Me.chckAllocated.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(22, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 14)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Status:"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.rbAllocationDate)
        Me.GroupBox6.Controls.Add(Me.rbCollectionDate)
        Me.GroupBox6.Controls.Add(Me.dtFrom)
        Me.GroupBox6.Controls.Add(Me.Label6)
        Me.GroupBox6.Controls.Add(Me.dtTo)
        Me.GroupBox6.Location = New System.Drawing.Point(13, 10)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(255, 71)
        Me.GroupBox6.TabIndex = 27
        Me.GroupBox6.TabStop = False
        '
        'rbAllocationDate
        '
        Me.rbAllocationDate.AutoSize = True
        Me.rbAllocationDate.ForeColor = System.Drawing.Color.Black
        Me.rbAllocationDate.Location = New System.Drawing.Point(140, 12)
        Me.rbAllocationDate.Name = "rbAllocationDate"
        Me.rbAllocationDate.Size = New System.Drawing.Size(97, 18)
        Me.rbAllocationDate.TabIndex = 20
        Me.rbAllocationDate.Text = "&Allocation Date"
        Me.rbAllocationDate.UseVisualStyleBackColor = True
        '
        'rbCollectionDate
        '
        Me.rbCollectionDate.AutoSize = True
        Me.rbCollectionDate.Checked = True
        Me.rbCollectionDate.ForeColor = System.Drawing.Color.Black
        Me.rbCollectionDate.Location = New System.Drawing.Point(13, 12)
        Me.rbCollectionDate.Name = "rbCollectionDate"
        Me.rbCollectionDate.Size = New System.Drawing.Size(96, 18)
        Me.rbCollectionDate.TabIndex = 19
        Me.rbCollectionDate.TabStop = True
        Me.rbCollectionDate.Text = "&Collection Date"
        Me.rbCollectionDate.UseVisualStyleBackColor = True
        '
        'dtFrom
        '
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFrom.Location = New System.Drawing.Point(14, 43)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(104, 20)
        Me.dtFrom.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(119, 51)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(21, 14)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "TO"
        '
        'dtTo
        '
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTo.Location = New System.Drawing.Point(140, 43)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(104, 20)
        Me.dtTo.TabIndex = 18
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.White
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Blue
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(227, 207)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(41, 39)
        Me.btnClose.TabIndex = 11
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.White
        Me.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.ForeColor = System.Drawing.Color.Blue
        Me.btnSearch.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.btnSearch.Location = New System.Drawing.Point(180, 207)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(41, 39)
        Me.btnSearch.TabIndex = 8
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.chckManual)
        Me.GroupBox5.Controls.Add(Me.chckAuto)
        Me.GroupBox5.Controls.Add(Me.Label5)
        Me.GroupBox5.Location = New System.Drawing.Point(13, 87)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(255, 28)
        Me.GroupBox5.TabIndex = 26
        Me.GroupBox5.TabStop = False
        '
        'chckManual
        '
        Me.chckManual.AutoSize = True
        Me.chckManual.Checked = True
        Me.chckManual.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chckManual.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckManual.ForeColor = System.Drawing.Color.Black
        Me.chckManual.Location = New System.Drawing.Point(161, 10)
        Me.chckManual.Name = "chckManual"
        Me.chckManual.Size = New System.Drawing.Size(60, 18)
        Me.chckManual.TabIndex = 15
        Me.chckManual.Text = "&Manual"
        Me.chckManual.UseVisualStyleBackColor = True
        '
        'chckAuto
        '
        Me.chckAuto.AutoSize = True
        Me.chckAuto.Checked = True
        Me.chckAuto.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chckAuto.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckAuto.ForeColor = System.Drawing.Color.Black
        Me.chckAuto.Location = New System.Drawing.Point(73, 10)
        Me.chckAuto.Name = "chckAuto"
        Me.chckAuto.Size = New System.Drawing.Size(74, 18)
        Me.chckAuto.TabIndex = 14
        Me.chckAuto.Text = "&Automatic"
        Me.chckAuto.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(30, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(36, 14)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "Type:"
        '
        'ddlParticipantID
        '
        Me.ddlParticipantID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlParticipantID.FormattingEnabled = True
        Me.ddlParticipantID.Location = New System.Drawing.Point(108, 181)
        Me.ddlParticipantID.Name = "ddlParticipantID"
        Me.ddlParticipantID.Size = New System.Drawing.Size(160, 22)
        Me.ddlParticipantID.TabIndex = 29
        '
        'chckParticipantID
        '
        Me.chckParticipantID.AutoSize = True
        Me.chckParticipantID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckParticipantID.ForeColor = System.Drawing.Color.Black
        Me.chckParticipantID.Location = New System.Drawing.Point(6, 183)
        Me.chckParticipantID.Name = "chckParticipantID"
        Me.chckParticipantID.Size = New System.Drawing.Size(100, 18)
        Me.chckParticipantID.TabIndex = 28
        Me.chckParticipantID.Text = "&Participant ID:"
        Me.chckParticipantID.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.chckNotPost)
        Me.GroupBox7.Controls.Add(Me.chckPosted)
        Me.GroupBox7.Controls.Add(Me.Label17)
        Me.GroupBox7.Location = New System.Drawing.Point(13, 146)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(255, 28)
        Me.GroupBox7.TabIndex = 30
        Me.GroupBox7.TabStop = False
        '
        'chckNotPost
        '
        Me.chckNotPost.AutoSize = True
        Me.chckNotPost.Checked = True
        Me.chckNotPost.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chckNotPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckNotPost.ForeColor = System.Drawing.Color.Black
        Me.chckNotPost.Location = New System.Drawing.Point(161, 10)
        Me.chckNotPost.Name = "chckNotPost"
        Me.chckNotPost.Size = New System.Drawing.Size(63, 18)
        Me.chckNotPost.TabIndex = 15
        Me.chckNotPost.Text = "&NotPost"
        Me.chckNotPost.UseVisualStyleBackColor = True
        '
        'chckPosted
        '
        Me.chckPosted.AutoSize = True
        Me.chckPosted.Checked = True
        Me.chckPosted.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chckPosted.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckPosted.ForeColor = System.Drawing.Color.Black
        Me.chckPosted.Location = New System.Drawing.Point(73, 10)
        Me.chckPosted.Name = "chckPosted"
        Me.chckPosted.Size = New System.Drawing.Size(59, 18)
        Me.chckPosted.TabIndex = 14
        Me.chckPosted.Text = "&Posted"
        Me.chckPosted.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(10, 10)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(59, 14)
        Me.Label17.TabIndex = 25
        Me.Label17.Text = "IsPosted:"
        '
        'gpCollection
        '
        Me.gpCollection.Controls.Add(Me.btnCancel)
        Me.gpCollection.Controls.Add(Me.btnOK)
        Me.gpCollection.Controls.Add(Me.lblTransaction)
        Me.gpCollection.Controls.Add(Me.dtAllocationDate)
        Me.gpCollection.Location = New System.Drawing.Point(6, 282)
        Me.gpCollection.Name = "gpCollection"
        Me.gpCollection.Size = New System.Drawing.Size(279, 94)
        Me.gpCollection.TabIndex = 13
        Me.gpCollection.TabStop = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.White
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.ForeColor = System.Drawing.Color.Blue
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.Location = New System.Drawing.Point(227, 45)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(41, 39)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.White
        Me.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.ForeColor = System.Drawing.Color.Blue
        Me.btnOK.Image = Global.AccountsManagementForms.My.Resources.Resources.OkIcon22x22
        Me.btnOK.Location = New System.Drawing.Point(180, 45)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(41, 39)
        Me.btnOK.TabIndex = 4
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'lblTransaction
        '
        Me.lblTransaction.AutoSize = True
        Me.lblTransaction.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransaction.ForeColor = System.Drawing.Color.Black
        Me.lblTransaction.Location = New System.Drawing.Point(10, 21)
        Me.lblTransaction.Name = "lblTransaction"
        Me.lblTransaction.Size = New System.Drawing.Size(91, 14)
        Me.lblTransaction.TabIndex = 2
        Me.lblTransaction.Text = "Allocation Date:"
        '
        'dtAllocationDate
        '
        Me.dtAllocationDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtAllocationDate.Location = New System.Drawing.Point(108, 19)
        Me.dtAllocationDate.Name = "dtAllocationDate"
        Me.dtAllocationDate.Size = New System.Drawing.Size(160, 20)
        Me.dtAllocationDate.TabIndex = 3
        '
        'frmCollectionSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(290, 388)
        Me.ControlBox = False
        Me.Controls.Add(Me.gpCollection)
        Me.Controls.Add(Me.gpSearch)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmCollectionSearch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Collection Search"
        Me.gpSearch.ResumeLayout(False)
        Me.gpSearch.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.gpCollection.ResumeLayout(False)
        Me.gpCollection.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents gpSearch As System.Windows.Forms.GroupBox
    Friend WithEvents gpCollection As System.Windows.Forms.GroupBox
    Friend WithEvents lblTransaction As System.Windows.Forms.Label
    Friend WithEvents dtAllocationDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents rbAllocationDate As System.Windows.Forms.RadioButton
    Friend WithEvents rbCollectionDate As System.Windows.Forms.RadioButton
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents chckManual As System.Windows.Forms.CheckBox
    Friend WithEvents chckAuto As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ddlParticipantID As System.Windows.Forms.ComboBox
    Friend WithEvents chckParticipantID As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents chckNotPost As System.Windows.Forms.CheckBox
    Friend WithEvents chckPosted As System.Windows.Forms.CheckBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents chckUnallocated As System.Windows.Forms.CheckBox
    Friend WithEvents chckAllocated As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
