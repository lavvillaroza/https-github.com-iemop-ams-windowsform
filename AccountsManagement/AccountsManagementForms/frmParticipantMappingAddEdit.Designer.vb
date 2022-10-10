<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParticipantMappingAddEdit
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
        Me.grp_AddParent = New System.Windows.Forms.GroupBox()
        Me.cbo_BillPeriod = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_remarks = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmd_RemoveAll = New System.Windows.Forms.Button()
        Me.cmd_AddAll = New System.Windows.Forms.Button()
        Me.cmd_RemoveSingle = New System.Windows.Forms.Button()
        Me.cmd_AddSingle = New System.Windows.Forms.Button()
        Me.lstbox_ChildList = New System.Windows.Forms.ListBox()
        Me.lstbox_ParticipantList = New System.Windows.Forms.ListBox()
        Me.cbo_ParentID = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grp_AddChild = New System.Windows.Forms.GroupBox()
        Me.txt_BillPeriod = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.lstBox_ParentList = New System.Windows.Forms.ListBox()
        Me.rb_SetChild = New System.Windows.Forms.RadioButton()
        Me.rb_SetParent = New System.Windows.Forms.RadioButton()
        Me.txt_EditParticipant = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmd_Save = New System.Windows.Forms.Button()
        Me.cmd_Cancel = New System.Windows.Forms.Button()
        Me.grp_AddParent.SuspendLayout()
        Me.grp_AddChild.SuspendLayout()
        Me.SuspendLayout()
        '
        'grp_AddParent
        '
        Me.grp_AddParent.Controls.Add(Me.cbo_BillPeriod)
        Me.grp_AddParent.Controls.Add(Me.Label7)
        Me.grp_AddParent.Controls.Add(Me.Label5)
        Me.grp_AddParent.Controls.Add(Me.txt_remarks)
        Me.grp_AddParent.Controls.Add(Me.Label4)
        Me.grp_AddParent.Controls.Add(Me.Label3)
        Me.grp_AddParent.Controls.Add(Me.cmd_RemoveAll)
        Me.grp_AddParent.Controls.Add(Me.cmd_AddAll)
        Me.grp_AddParent.Controls.Add(Me.cmd_RemoveSingle)
        Me.grp_AddParent.Controls.Add(Me.cmd_AddSingle)
        Me.grp_AddParent.Controls.Add(Me.lstbox_ChildList)
        Me.grp_AddParent.Controls.Add(Me.lstbox_ParticipantList)
        Me.grp_AddParent.Controls.Add(Me.cbo_ParentID)
        Me.grp_AddParent.Controls.Add(Me.Label1)
        Me.grp_AddParent.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grp_AddParent.Location = New System.Drawing.Point(12, 12)
        Me.grp_AddParent.Name = "grp_AddParent"
        Me.grp_AddParent.Size = New System.Drawing.Size(378, 349)
        Me.grp_AddParent.TabIndex = 0
        Me.grp_AddParent.TabStop = False
        '
        'cbo_BillPeriod
        '
        Me.cbo_BillPeriod.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbo_BillPeriod.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbo_BillPeriod.FormattingEnabled = True
        Me.cbo_BillPeriod.Location = New System.Drawing.Point(211, 40)
        Me.cbo_BillPeriod.Name = "cbo_BillPeriod"
        Me.cbo_BillPeriod.Size = New System.Drawing.Size(158, 22)
        Me.cbo_BillPeriod.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 43)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(120, 14)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Select Billing Period:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 250)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 14)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Remarks"
        '
        'txt_remarks
        '
        Me.txt_remarks.Location = New System.Drawing.Point(6, 267)
        Me.txt_remarks.MaxLength = 250
        Me.txt_remarks.Multiline = True
        Me.txt_remarks.Name = "txt_remarks"
        Me.txt_remarks.Size = New System.Drawing.Size(363, 74)
        Me.txt_remarks.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(208, 86)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 14)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Add Child:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(105, 14)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Select Participant:"
        '
        'cmd_RemoveAll
        '
        Me.cmd_RemoveAll.Location = New System.Drawing.Point(170, 204)
        Me.cmd_RemoveAll.Name = "cmd_RemoveAll"
        Me.cmd_RemoveAll.Size = New System.Drawing.Size(35, 23)
        Me.cmd_RemoveAll.TabIndex = 7
        Me.cmd_RemoveAll.Text = "<<"
        Me.cmd_RemoveAll.UseVisualStyleBackColor = True
        '
        'cmd_AddAll
        '
        Me.cmd_AddAll.Location = New System.Drawing.Point(170, 122)
        Me.cmd_AddAll.Name = "cmd_AddAll"
        Me.cmd_AddAll.Size = New System.Drawing.Size(35, 23)
        Me.cmd_AddAll.TabIndex = 6
        Me.cmd_AddAll.Text = ">>"
        Me.cmd_AddAll.UseVisualStyleBackColor = True
        '
        'cmd_RemoveSingle
        '
        Me.cmd_RemoveSingle.Location = New System.Drawing.Point(170, 175)
        Me.cmd_RemoveSingle.Name = "cmd_RemoveSingle"
        Me.cmd_RemoveSingle.Size = New System.Drawing.Size(35, 23)
        Me.cmd_RemoveSingle.TabIndex = 5
        Me.cmd_RemoveSingle.Text = "<"
        Me.cmd_RemoveSingle.UseVisualStyleBackColor = True
        '
        'cmd_AddSingle
        '
        Me.cmd_AddSingle.Location = New System.Drawing.Point(170, 146)
        Me.cmd_AddSingle.Name = "cmd_AddSingle"
        Me.cmd_AddSingle.Size = New System.Drawing.Size(35, 23)
        Me.cmd_AddSingle.TabIndex = 4
        Me.cmd_AddSingle.Text = ">"
        Me.cmd_AddSingle.UseVisualStyleBackColor = True
        '
        'lstbox_ChildList
        '
        Me.lstbox_ChildList.FormattingEnabled = True
        Me.lstbox_ChildList.ItemHeight = 14
        Me.lstbox_ChildList.Location = New System.Drawing.Point(211, 103)
        Me.lstbox_ChildList.Name = "lstbox_ChildList"
        Me.lstbox_ChildList.Size = New System.Drawing.Size(158, 144)
        Me.lstbox_ChildList.TabIndex = 3
        '
        'lstbox_ParticipantList
        '
        Me.lstbox_ParticipantList.FormattingEnabled = True
        Me.lstbox_ParticipantList.ItemHeight = 14
        Me.lstbox_ParticipantList.Location = New System.Drawing.Point(6, 103)
        Me.lstbox_ParticipantList.Name = "lstbox_ParticipantList"
        Me.lstbox_ParticipantList.Size = New System.Drawing.Size(158, 144)
        Me.lstbox_ParticipantList.TabIndex = 2
        '
        'cbo_ParentID
        '
        Me.cbo_ParentID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbo_ParentID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbo_ParentID.FormattingEnabled = True
        Me.cbo_ParentID.Location = New System.Drawing.Point(157, 14)
        Me.cbo_ParentID.Name = "cbo_ParentID"
        Me.cbo_ParentID.Size = New System.Drawing.Size(212, 22)
        Me.cbo_ParentID.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Select Parent:"
        '
        'grp_AddChild
        '
        Me.grp_AddChild.Controls.Add(Me.txt_BillPeriod)
        Me.grp_AddChild.Controls.Add(Me.Label8)
        Me.grp_AddChild.Controls.Add(Me.Label6)
        Me.grp_AddChild.Controls.Add(Me.TextBox2)
        Me.grp_AddChild.Controls.Add(Me.lstBox_ParentList)
        Me.grp_AddChild.Controls.Add(Me.rb_SetChild)
        Me.grp_AddChild.Controls.Add(Me.rb_SetParent)
        Me.grp_AddChild.Controls.Add(Me.txt_EditParticipant)
        Me.grp_AddChild.Controls.Add(Me.Label2)
        Me.grp_AddChild.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grp_AddChild.Location = New System.Drawing.Point(12, 12)
        Me.grp_AddChild.Name = "grp_AddChild"
        Me.grp_AddChild.Size = New System.Drawing.Size(378, 349)
        Me.grp_AddChild.TabIndex = 1
        Me.grp_AddChild.TabStop = False
        '
        'txt_BillPeriod
        '
        Me.txt_BillPeriod.BackColor = System.Drawing.SystemColors.Info
        Me.txt_BillPeriod.Location = New System.Drawing.Point(189, 41)
        Me.txt_BillPeriod.Name = "txt_BillPeriod"
        Me.txt_BillPeriod.ReadOnly = True
        Me.txt_BillPeriod.Size = New System.Drawing.Size(183, 21)
        Me.txt_BillPeriod.TabIndex = 15
        Me.txt_BillPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(9, 44)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(83, 14)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Billing Period:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 250)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 14)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Remarks"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(9, 267)
        Me.TextBox2.MaxLength = 250
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(363, 74)
        Me.TextBox2.TabIndex = 12
        '
        'lstBox_ParentList
        '
        Me.lstBox_ParentList.FormattingEnabled = True
        Me.lstBox_ParentList.ItemHeight = 14
        Me.lstBox_ParentList.Location = New System.Drawing.Point(82, 93)
        Me.lstBox_ParentList.Name = "lstBox_ParentList"
        Me.lstBox_ParentList.Size = New System.Drawing.Size(215, 158)
        Me.lstBox_ParentList.TabIndex = 4
        '
        'rb_SetChild
        '
        Me.rb_SetChild.AutoSize = True
        Me.rb_SetChild.Location = New System.Drawing.Point(189, 69)
        Me.rb_SetChild.Name = "rb_SetChild"
        Me.rb_SetChild.Size = New System.Drawing.Size(106, 18)
        Me.rb_SetChild.TabIndex = 3
        Me.rb_SetChild.TabStop = True
        Me.rb_SetChild.Text = "Set as Child Of"
        Me.rb_SetChild.UseVisualStyleBackColor = True
        '
        'rb_SetParent
        '
        Me.rb_SetParent.AutoSize = True
        Me.rb_SetParent.Checked = True
        Me.rb_SetParent.Location = New System.Drawing.Point(82, 69)
        Me.rb_SetParent.Name = "rb_SetParent"
        Me.rb_SetParent.Size = New System.Drawing.Size(99, 18)
        Me.rb_SetParent.TabIndex = 2
        Me.rb_SetParent.TabStop = True
        Me.rb_SetParent.Text = "Set As Parent"
        Me.rb_SetParent.UseVisualStyleBackColor = True
        '
        'txt_EditParticipant
        '
        Me.txt_EditParticipant.BackColor = System.Drawing.SystemColors.Info
        Me.txt_EditParticipant.Location = New System.Drawing.Point(189, 14)
        Me.txt_EditParticipant.Name = "txt_EditParticipant"
        Me.txt_EditParticipant.ReadOnly = True
        Me.txt_EditParticipant.Size = New System.Drawing.Size(183, 21)
        Me.txt_EditParticipant.TabIndex = 1
        Me.txt_EditParticipant.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 14)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Child Particpant:"
        '
        'cmd_Save
        '
        Me.cmd_Save.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Save.Image = Global.AccountsManagementForms.My.Resources.Resources.Save10
        Me.cmd_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Save.Location = New System.Drawing.Point(158, 367)
        Me.cmd_Save.Name = "cmd_Save"
        Me.cmd_Save.Size = New System.Drawing.Size(114, 42)
        Me.cmd_Save.TabIndex = 2
        Me.cmd_Save.Text = "Save"
        Me.cmd_Save.UseVisualStyleBackColor = True
        '
        'cmd_Cancel
        '
        Me.cmd_Cancel.Font = New System.Drawing.Font("Helvetica", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Cancel.Image = Global.AccountsManagementForms.My.Resources.Resources.cancel
        Me.cmd_Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Cancel.Location = New System.Drawing.Point(276, 367)
        Me.cmd_Cancel.Name = "cmd_Cancel"
        Me.cmd_Cancel.Size = New System.Drawing.Size(114, 42)
        Me.cmd_Cancel.TabIndex = 3
        Me.cmd_Cancel.Text = "Cancel"
        Me.cmd_Cancel.UseVisualStyleBackColor = True
        '
        'frmParticipantMappingAddEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(397, 422)
        Me.Controls.Add(Me.cmd_Cancel)
        Me.Controls.Add(Me.cmd_Save)
        Me.Controls.Add(Me.grp_AddChild)
        Me.Controls.Add(Me.grp_AddParent)
        Me.MinimumSize = New System.Drawing.Size(413, 460)
        Me.Name = "frmParticipantMappingAddEdit"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Add/Edit Participant"
        Me.grp_AddParent.ResumeLayout(False)
        Me.grp_AddParent.PerformLayout()
        Me.grp_AddChild.ResumeLayout(False)
        Me.grp_AddChild.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grp_AddParent As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt_remarks As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmd_RemoveAll As System.Windows.Forms.Button
    Friend WithEvents cmd_AddAll As System.Windows.Forms.Button
    Friend WithEvents cmd_RemoveSingle As System.Windows.Forms.Button
    Friend WithEvents cmd_AddSingle As System.Windows.Forms.Button
    Friend WithEvents lstbox_ChildList As System.Windows.Forms.ListBox
    Friend WithEvents lstbox_ParticipantList As System.Windows.Forms.ListBox
    Friend WithEvents cbo_ParentID As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grp_AddChild As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rb_SetChild As System.Windows.Forms.RadioButton
    Friend WithEvents rb_SetParent As System.Windows.Forms.RadioButton
    Friend WithEvents txt_EditParticipant As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents lstBox_ParentList As System.Windows.Forms.ListBox
    Friend WithEvents cmd_Save As System.Windows.Forms.Button
    Friend WithEvents cmd_Cancel As System.Windows.Forms.Button
    Friend WithEvents cbo_BillPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txt_BillPeriod As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
End Class
