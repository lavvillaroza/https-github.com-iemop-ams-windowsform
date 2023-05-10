<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParticipantMaintenanceMgt
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.CBO_Parent = New System.Windows.Forms.ComboBox
        Me.TXT_Child = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.CMD_Save = New System.Windows.Forms.Button
        Me.CMD_Cancel = New System.Windows.Forms.Button
        Me.TXT_Remarks = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.TXT_UpdatedDate = New System.Windows.Forms.TextBox
        Me.TXT_UpdatedBy = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(211, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Child"
        '
        'CBO_Parent
        '
        Me.CBO_Parent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBO_Parent.FormattingEnabled = True
        Me.CBO_Parent.Location = New System.Drawing.Point(15, 27)
        Me.CBO_Parent.Name = "CBO_Parent"
        Me.CBO_Parent.Size = New System.Drawing.Size(193, 21)
        Me.CBO_Parent.TabIndex = 2
        '
        'TXT_Child
        '
        Me.TXT_Child.Location = New System.Drawing.Point(217, 27)
        Me.TXT_Child.MaxLength = 6
        Me.TXT_Child.Name = "TXT_Child"
        Me.TXT_Child.Size = New System.Drawing.Size(193, 20)
        Me.TXT_Child.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(93, 15)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Select Parent"
        '
        'CMD_Save
        '
        Me.CMD_Save.Location = New System.Drawing.Point(214, 139)
        Me.CMD_Save.Name = "CMD_Save"
        Me.CMD_Save.Size = New System.Drawing.Size(94, 44)
        Me.CMD_Save.TabIndex = 5
        Me.CMD_Save.Text = "Save"
        Me.CMD_Save.UseVisualStyleBackColor = True
        '
        'CMD_Cancel
        '
        Me.CMD_Cancel.Location = New System.Drawing.Point(314, 139)
        Me.CMD_Cancel.Name = "CMD_Cancel"
        Me.CMD_Cancel.Size = New System.Drawing.Size(96, 44)
        Me.CMD_Cancel.TabIndex = 6
        Me.CMD_Cancel.Text = "Cancel"
        Me.CMD_Cancel.UseVisualStyleBackColor = True
        '
        'TXT_Remarks
        '
        Me.TXT_Remarks.Location = New System.Drawing.Point(15, 75)
        Me.TXT_Remarks.MaxLength = 250
        Me.TXT_Remarks.Multiline = True
        Me.TXT_Remarks.Name = "TXT_Remarks"
        Me.TXT_Remarks.Size = New System.Drawing.Size(193, 108)
        Me.TXT_Remarks.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 15)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Remarks"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(214, 58)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Last Update"
        '
        'TXT_UpdatedDate
        '
        Me.TXT_UpdatedDate.Location = New System.Drawing.Point(217, 74)
        Me.TXT_UpdatedDate.MaxLength = 6
        Me.TXT_UpdatedDate.Name = "TXT_UpdatedDate"
        Me.TXT_UpdatedDate.Size = New System.Drawing.Size(193, 20)
        Me.TXT_UpdatedDate.TabIndex = 10
        '
        'TXT_UpdatedBy
        '
        Me.TXT_UpdatedBy.Location = New System.Drawing.Point(217, 113)
        Me.TXT_UpdatedBy.MaxLength = 6
        Me.TXT_UpdatedBy.Name = "TXT_UpdatedBy"
        Me.TXT_UpdatedBy.Size = New System.Drawing.Size(193, 20)
        Me.TXT_UpdatedBy.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(214, 97)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Updated By"
        '
        'frmParticipantMaintenanceMgt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(424, 205)
        Me.Controls.Add(Me.TXT_UpdatedBy)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TXT_UpdatedDate)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TXT_Remarks)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CMD_Cancel)
        Me.Controls.Add(Me.CMD_Save)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TXT_Child)
        Me.Controls.Add(Me.CBO_Parent)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximumSize = New System.Drawing.Size(430, 233)
        Me.MinimumSize = New System.Drawing.Size(430, 233)
        Me.Name = "frmParticipantMaintenanceMgt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Participant Mapping Management"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CBO_Parent As System.Windows.Forms.ComboBox
    Friend WithEvents TXT_Child As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CMD_Save As System.Windows.Forms.Button
    Friend WithEvents CMD_Cancel As System.Windows.Forms.Button
    Friend WithEvents TXT_Remarks As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TXT_UpdatedDate As System.Windows.Forms.TextBox
    Friend WithEvents TXT_UpdatedBy As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
