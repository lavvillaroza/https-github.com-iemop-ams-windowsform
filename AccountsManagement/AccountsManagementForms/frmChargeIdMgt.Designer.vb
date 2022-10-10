<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChargeIdMgt
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
        Me.rb_active = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbo_Type = New System.Windows.Forms.ComboBox()
        Me.lbl_addEdit = New System.Windows.Forms.Label()
        Me.txt_desc = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lbl_dcomm = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_cId = New System.Windows.Forms.TextBox()
        Me.rb_inactive = New System.Windows.Forms.RadioButton()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'rb_active
        '
        Me.rb_active.AutoSize = True
        Me.rb_active.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_active.Location = New System.Drawing.Point(121, 188)
        Me.rb_active.Name = "rb_active"
        Me.rb_active.Size = New System.Drawing.Size(53, 16)
        Me.rb_active.TabIndex = 37
        Me.rb_active.TabStop = True
        Me.rb_active.Text = "Active"
        Me.rb_active.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 188)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 14)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "Status:"
        '
        'cbo_Type
        '
        Me.cbo_Type.BackColor = System.Drawing.Color.White
        Me.cbo_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_Type.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_Type.FormattingEnabled = True
        Me.cbo_Type.Location = New System.Drawing.Point(121, 47)
        Me.cbo_Type.Name = "cbo_Type"
        Me.cbo_Type.Size = New System.Drawing.Size(167, 20)
        Me.cbo_Type.TabIndex = 33
        '
        'lbl_addEdit
        '
        Me.lbl_addEdit.AutoSize = True
        Me.lbl_addEdit.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_addEdit.Location = New System.Drawing.Point(118, 241)
        Me.lbl_addEdit.Name = "lbl_addEdit"
        Me.lbl_addEdit.Size = New System.Drawing.Size(0, 12)
        Me.lbl_addEdit.TabIndex = 32
        '
        'txt_desc
        '
        Me.txt_desc.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_desc.Location = New System.Drawing.Point(121, 72)
        Me.txt_desc.MaxLength = 100
        Me.txt_desc.Multiline = True
        Me.txt_desc.Name = "txt_desc"
        Me.txt_desc.Size = New System.Drawing.Size(167, 93)
        Me.txt_desc.TabIndex = 31
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(12, 72)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 14)
        Me.Label7.TabIndex = 30
        Me.Label7.Text = "Description: "
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 241)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 14)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "Updated By:"
        '
        'lbl_dcomm
        '
        Me.lbl_dcomm.AutoSize = True
        Me.lbl_dcomm.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_dcomm.Location = New System.Drawing.Point(118, 217)
        Me.lbl_dcomm.Name = "lbl_dcomm"
        Me.lbl_dcomm.Size = New System.Drawing.Size(0, 12)
        Me.lbl_dcomm.TabIndex = 28
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 217)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 14)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Updated Date:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 14)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Charge ID Type:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 14)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Charge ID:"
        '
        'txt_cId
        '
        Me.txt_cId.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_cId.Location = New System.Drawing.Point(121, 21)
        Me.txt_cId.MaxLength = 13
        Me.txt_cId.Name = "txt_cId"
        Me.txt_cId.Size = New System.Drawing.Size(168, 20)
        Me.txt_cId.TabIndex = 24
        '
        'rb_inactive
        '
        Me.rb_inactive.AutoSize = True
        Me.rb_inactive.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_inactive.Location = New System.Drawing.Point(225, 188)
        Me.rb_inactive.Name = "rb_inactive"
        Me.rb_inactive.Size = New System.Drawing.Size(61, 16)
        Me.rb_inactive.TabIndex = 38
        Me.rb_inactive.TabStop = True
        Me.rb_inactive.Text = "Inactive"
        Me.rb_inactive.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.White
        Me.cmdCancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmdCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmdCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.Color.Black
        Me.cmdCancel.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCancel.Location = New System.Drawing.Point(165, 273)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(121, 39)
        Me.cmdCancel.TabIndex = 35
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.Color.White
        Me.cmdSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmdSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmdSave.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.Color.Black
        Me.cmdSave.Image = Global.AccountsManagementForms.My.Resources.Resources.SaveIconColored22x22
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSave.Location = New System.Drawing.Point(38, 273)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(121, 39)
        Me.cmdSave.TabIndex = 34
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'frmChargeIdMgt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(301, 322)
        Me.ControlBox = False
        Me.Controls.Add(Me.rb_active)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.rb_inactive)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.txt_cId)
        Me.Controls.Add(Me.cbo_Type)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbl_addEdit)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_desc)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.lbl_dcomm)
        Me.Controls.Add(Me.Label6)
        Me.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmChargeIdMgt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Charge Id Management"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rb_active As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cbo_Type As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_addEdit As System.Windows.Forms.Label
    Friend WithEvents txt_desc As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lbl_dcomm As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_cId As System.Windows.Forms.TextBox
    Friend WithEvents rb_inactive As System.Windows.Forms.RadioButton
End Class
