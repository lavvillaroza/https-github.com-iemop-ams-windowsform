<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMAPReport
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
        Me.DueDate_CB = New System.Windows.Forms.ComboBox()
        Me.cmd_Close = New System.Windows.Forms.Button()
        Me.GenerateMAPRaw_Button = New System.Windows.Forms.Button()
        Me.GenerateBIR2307_Button = New System.Windows.Forms.Button()
        Me.ParticipantList_CLB = New System.Windows.Forms.CheckedListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.gbox_Participants = New System.Windows.Forms.GroupBox()
        Me.chkbox_SelectAll = New System.Windows.Forms.CheckBox()
        Me.gbox_Participants.SuspendLayout()
        Me.SuspendLayout()
        '
        'DueDate_CB
        '
        Me.DueDate_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DueDate_CB.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DueDate_CB.FormattingEnabled = True
        Me.DueDate_CB.Location = New System.Drawing.Point(12, 43)
        Me.DueDate_CB.Name = "DueDate_CB"
        Me.DueDate_CB.Size = New System.Drawing.Size(185, 22)
        Me.DueDate_CB.TabIndex = 1
        '
        'cmd_Close
        '
        Me.cmd_Close.BackColor = System.Drawing.Color.White
        Me.cmd_Close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Close.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Close.Location = New System.Drawing.Point(12, 163)
        Me.cmd_Close.Name = "cmd_Close"
        Me.cmd_Close.Size = New System.Drawing.Size(185, 40)
        Me.cmd_Close.TabIndex = 5
        Me.cmd_Close.Text = "Close"
        Me.cmd_Close.UseVisualStyleBackColor = False
        '
        'GenerateMAPRaw_Button
        '
        Me.GenerateMAPRaw_Button.BackColor = System.Drawing.Color.White
        Me.GenerateMAPRaw_Button.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.GenerateMAPRaw_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.GenerateMAPRaw_Button.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.GenerateMAPRaw_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GenerateMAPRaw_Button.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GenerateMAPRaw_Button.Image = Global.AccountsManagementForms.My.Resources.Resources.ExcelIcon22x22
        Me.GenerateMAPRaw_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.GenerateMAPRaw_Button.Location = New System.Drawing.Point(12, 71)
        Me.GenerateMAPRaw_Button.Name = "GenerateMAPRaw_Button"
        Me.GenerateMAPRaw_Button.Size = New System.Drawing.Size(185, 40)
        Me.GenerateMAPRaw_Button.TabIndex = 4
        Me.GenerateMAPRaw_Button.Text = "       Monthly Alphabetical List of Payees Report"
        Me.GenerateMAPRaw_Button.UseVisualStyleBackColor = False
        '
        'GenerateBIR2307_Button
        '
        Me.GenerateBIR2307_Button.BackColor = System.Drawing.Color.White
        Me.GenerateBIR2307_Button.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.GenerateBIR2307_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.GenerateBIR2307_Button.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.GenerateBIR2307_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GenerateBIR2307_Button.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GenerateBIR2307_Button.Image = Global.AccountsManagementForms.My.Resources.Resources.ExcelIcon22x22
        Me.GenerateBIR2307_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.GenerateBIR2307_Button.Location = New System.Drawing.Point(12, 117)
        Me.GenerateBIR2307_Button.Name = "GenerateBIR2307_Button"
        Me.GenerateBIR2307_Button.Size = New System.Drawing.Size(185, 40)
        Me.GenerateBIR2307_Button.TabIndex = 9
        Me.GenerateBIR2307_Button.Text = "     EWT Certificates (BIR Form 2307)"
        Me.GenerateBIR2307_Button.UseVisualStyleBackColor = False
        '
        'ParticipantList_CLB
        '
        Me.ParticipantList_CLB.FormattingEnabled = True
        Me.ParticipantList_CLB.Location = New System.Drawing.Point(8, 39)
        Me.ParticipantList_CLB.Name = "ParticipantList_CLB"
        Me.ParticipantList_CLB.Size = New System.Drawing.Size(200, 139)
        Me.ParticipantList_CLB.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 14)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Due Date:"
        '
        'gbox_Participants
        '
        Me.gbox_Participants.Controls.Add(Me.chkbox_SelectAll)
        Me.gbox_Participants.Controls.Add(Me.ParticipantList_CLB)
        Me.gbox_Participants.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbox_Participants.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.gbox_Participants.Location = New System.Drawing.Point(203, 15)
        Me.gbox_Participants.Name = "gbox_Participants"
        Me.gbox_Participants.Size = New System.Drawing.Size(218, 188)
        Me.gbox_Participants.TabIndex = 14
        Me.gbox_Participants.TabStop = False
        Me.gbox_Participants.Text = "Participant/s:"
        '
        'chkbox_SelectAll
        '
        Me.chkbox_SelectAll.AutoSize = True
        Me.chkbox_SelectAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbox_SelectAll.ForeColor = System.Drawing.Color.Black
        Me.chkbox_SelectAll.Location = New System.Drawing.Point(8, 17)
        Me.chkbox_SelectAll.Name = "chkbox_SelectAll"
        Me.chkbox_SelectAll.Size = New System.Drawing.Size(70, 18)
        Me.chkbox_SelectAll.TabIndex = 1
        Me.chkbox_SelectAll.Text = "Select All"
        Me.chkbox_SelectAll.UseVisualStyleBackColor = True
        '
        'frmMAPReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(430, 213)
        Me.Controls.Add(Me.gbox_Participants)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DueDate_CB)
        Me.Controls.Add(Me.GenerateBIR2307_Button)
        Me.Controls.Add(Me.cmd_Close)
        Me.Controls.Add(Me.GenerateMAPRaw_Button)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmMAPReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BIR MAP And EWT Certificates"
        Me.gbox_Participants.ResumeLayout(False)
        Me.gbox_Participants.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmd_Close As System.Windows.Forms.Button
    Friend WithEvents GenerateMAPRaw_Button As System.Windows.Forms.Button
    Friend WithEvents DueDate_CB As System.Windows.Forms.ComboBox
    Friend WithEvents GenerateBIR2307_Button As System.Windows.Forms.Button
    Friend WithEvents ParticipantList_CLB As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents gbox_Participants As System.Windows.Forms.GroupBox
    Friend WithEvents chkbox_SelectAll As System.Windows.Forms.CheckBox
End Class
