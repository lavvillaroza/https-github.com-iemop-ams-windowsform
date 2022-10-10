<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParticipantFitMappingMgt
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
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.GB_Add = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtParentFIT = New System.Windows.Forms.TextBox()
        Me.CB_ParticipantID = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtBillingPeriod = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CB_BPeriod = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbUnselectedAdd = New System.Windows.Forms.ListBox()
        Me.lbSelectedAdd = New System.Windows.Forms.ListBox()
        Me.btnUnselect = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.btnUnselectAll = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.GB_Add.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.SystemColors.Control
        Me.btnSave.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark
        Me.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSave.Image = Global.AccountsManagementForms.My.Resources.Resources.save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(267, 359)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(91, 30)
        Me.btnSave.TabIndex = 22
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCancel.Image = Global.AccountsManagementForms.My.Resources.Resources.close
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(364, 359)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(84, 30)
        Me.btnCancel.TabIndex = 23
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'GB_Add
        '
        Me.GB_Add.BackColor = System.Drawing.SystemColors.ControlLight
        Me.GB_Add.Controls.Add(Me.GroupBox2)
        Me.GB_Add.Controls.Add(Me.GroupBox1)
        Me.GB_Add.Controls.Add(Me.Label3)
        Me.GB_Add.Controls.Add(Me.lbUnselectedAdd)
        Me.GB_Add.Controls.Add(Me.lbSelectedAdd)
        Me.GB_Add.Controls.Add(Me.btnUnselect)
        Me.GB_Add.Controls.Add(Me.Label2)
        Me.GB_Add.Controls.Add(Me.btnSelect)
        Me.GB_Add.Controls.Add(Me.btnSelectAll)
        Me.GB_Add.Controls.Add(Me.btnUnselectAll)
        Me.GB_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GB_Add.Location = New System.Drawing.Point(11, 3)
        Me.GB_Add.Name = "GB_Add"
        Me.GB_Add.Size = New System.Drawing.Size(437, 340)
        Me.GB_Add.TabIndex = 24
        Me.GB_Add.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtParentFIT)
        Me.GroupBox2.Controls.Add(Me.CB_ParticipantID)
        Me.GroupBox2.Location = New System.Drawing.Point(224, 19)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(203, 106)
        Me.GroupBox2.TabIndex = 69
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Parent FIT"
        '
        'txtParentFIT
        '
        Me.txtParentFIT.Location = New System.Drawing.Point(6, 46)
        Me.txtParentFIT.Multiline = True
        Me.txtParentFIT.Name = "txtParentFIT"
        Me.txtParentFIT.Size = New System.Drawing.Size(191, 54)
        Me.txtParentFIT.TabIndex = 57
        '
        'CB_ParticipantID
        '
        Me.CB_ParticipantID.BackColor = System.Drawing.SystemColors.ControlLight
        Me.CB_ParticipantID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_ParticipantID.FormattingEnabled = True
        Me.CB_ParticipantID.Location = New System.Drawing.Point(6, 19)
        Me.CB_ParticipantID.Name = "CB_ParticipantID"
        Me.CB_ParticipantID.Size = New System.Drawing.Size(191, 21)
        Me.CB_ParticipantID.TabIndex = 56
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtBillingPeriod)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.CB_BPeriod)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 19)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(203, 106)
        Me.GroupBox1.TabIndex = 68
        Me.GroupBox1.TabStop = False
        '
        'txtBillingPeriod
        '
        Me.txtBillingPeriod.Location = New System.Drawing.Point(9, 46)
        Me.txtBillingPeriod.Multiline = True
        Me.txtBillingPeriod.Name = "txtBillingPeriod"
        Me.txtBillingPeriod.Size = New System.Drawing.Size(189, 54)
        Me.txtBillingPeriod.TabIndex = 55
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 13)
        Me.Label4.TabIndex = 54
        Me.Label4.Text = "Billing Period"
        '
        'CB_BPeriod
        '
        Me.CB_BPeriod.BackColor = System.Drawing.Color.White
        Me.CB_BPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_BPeriod.FormattingEnabled = True
        Me.CB_BPeriod.Location = New System.Drawing.Point(6, 19)
        Me.CB_BPeriod.Name = "CB_BPeriod"
        Me.CB_BPeriod.Size = New System.Drawing.Size(192, 21)
        Me.CB_BPeriod.TabIndex = 53
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(253, 134)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 61
        Me.Label3.Text = "Selected:"
        '
        'lbUnselectedAdd
        '
        Me.lbUnselectedAdd.BackColor = System.Drawing.SystemColors.Control
        Me.lbUnselectedAdd.FormattingEnabled = True
        Me.lbUnselectedAdd.Location = New System.Drawing.Point(15, 150)
        Me.lbUnselectedAdd.Name = "lbUnselectedAdd"
        Me.lbUnselectedAdd.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lbUnselectedAdd.Size = New System.Drawing.Size(175, 173)
        Me.lbUnselectedAdd.TabIndex = 58
        '
        'lbSelectedAdd
        '
        Me.lbSelectedAdd.BackColor = System.Drawing.SystemColors.Control
        Me.lbSelectedAdd.FormattingEnabled = True
        Me.lbSelectedAdd.Location = New System.Drawing.Point(252, 150)
        Me.lbSelectedAdd.Name = "lbSelectedAdd"
        Me.lbSelectedAdd.Size = New System.Drawing.Size(175, 173)
        Me.lbSelectedAdd.TabIndex = 59
        '
        'btnUnselect
        '
        Me.btnUnselect.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark
        Me.btnUnselect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUnselect.Image = Global.AccountsManagementForms.My.Resources.Resources.ArrowLeft
        Me.btnUnselect.Location = New System.Drawing.Point(198, 269)
        Me.btnUnselect.Name = "btnUnselect"
        Me.btnUnselect.Size = New System.Drawing.Size(46, 23)
        Me.btnUnselect.TabIndex = 65
        Me.btnUnselect.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 132)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 60
        Me.Label2.Text = "Unselected:"
        '
        'btnSelect
        '
        Me.btnSelect.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark
        Me.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelect.Image = Global.AccountsManagementForms.My.Resources.Resources.ArrowRight
        Me.btnSelect.Location = New System.Drawing.Point(198, 240)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(46, 23)
        Me.btnSelect.TabIndex = 64
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'btnSelectAll
        '
        Me.btnSelectAll.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark
        Me.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectAll.Image = Global.AccountsManagementForms.My.Resources.Resources.DoubleArrowRight
        Me.btnSelectAll.Location = New System.Drawing.Point(198, 182)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(46, 23)
        Me.btnSelectAll.TabIndex = 62
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'btnUnselectAll
        '
        Me.btnUnselectAll.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark
        Me.btnUnselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUnselectAll.Image = Global.AccountsManagementForms.My.Resources.Resources.DoubleArrowLeft
        Me.btnUnselectAll.Location = New System.Drawing.Point(198, 211)
        Me.btnUnselectAll.Name = "btnUnselectAll"
        Me.btnUnselectAll.Size = New System.Drawing.Size(46, 23)
        Me.btnUnselectAll.TabIndex = 63
        Me.btnUnselectAll.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 395)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(461, 22)
        Me.StatusStrip1.TabIndex = 25
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'frmParticipantFitMappingMgt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(461, 417)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GB_Add)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmParticipantFitMappingMgt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Participant FIT Management"
        Me.GB_Add.ResumeLayout(False)
        Me.GB_Add.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents GB_Add As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbUnselectedAdd As System.Windows.Forms.ListBox
    Friend WithEvents lbSelectedAdd As System.Windows.Forms.ListBox
    Friend WithEvents btnUnselect As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents btnUnselectAll As System.Windows.Forms.Button
    Friend WithEvents CB_ParticipantID As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CB_BPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtParentFIT As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtBillingPeriod As System.Windows.Forms.TextBox
End Class
