<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRPTCollectionNotice
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.CHB_AllParticipants = New System.Windows.Forms.CheckBox
        Me.LB_ParentParticipants = New System.Windows.Forms.ListBox
        Me.cmd_close = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.CHB_AllParticipants)
        Me.GroupBox1.Controls.Add(Me.LB_ParentParticipants)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(294, 349)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Parent Participant ID:"
        '
        'CHB_AllParticipants
        '
        Me.CHB_AllParticipants.AutoSize = True
        Me.CHB_AllParticipants.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHB_AllParticipants.Location = New System.Drawing.Point(6, 20)
        Me.CHB_AllParticipants.Name = "CHB_AllParticipants"
        Me.CHB_AllParticipants.Size = New System.Drawing.Size(106, 19)
        Me.CHB_AllParticipants.TabIndex = 1
        Me.CHB_AllParticipants.Text = "All Participants"
        Me.CHB_AllParticipants.UseVisualStyleBackColor = True
        '
        'LB_ParentParticipants
        '
        Me.LB_ParentParticipants.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LB_ParentParticipants.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LB_ParentParticipants.FormattingEnabled = True
        Me.LB_ParentParticipants.ItemHeight = 15
        Me.LB_ParentParticipants.Location = New System.Drawing.Point(6, 45)
        Me.LB_ParentParticipants.Name = "LB_ParentParticipants"
        Me.LB_ParentParticipants.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.LB_ParentParticipants.Size = New System.Drawing.Size(277, 289)
        Me.LB_ParentParticipants.TabIndex = 0
        '
        'cmd_close
        '
        Me.cmd_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_close.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_close.ForeColor = System.Drawing.Color.Black
        Me.cmd_close.Image = Global.AccountsManagementForms.My.Resources.Resources.close
        Me.cmd_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_close.Location = New System.Drawing.Point(118, 400)
        Me.cmd_close.Name = "cmd_close"
        Me.cmd_close.Size = New System.Drawing.Size(188, 27)
        Me.cmd_close.TabIndex = 7
        Me.cmd_close.Text = "Close"
        Me.cmd_close.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Image = Global.AccountsManagementForms.My.Resources.Resources.report
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(118, 367)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(188, 27)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "     Generate Collection Notice"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmRPTCollectionNotice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(318, 439)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmd_close)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmRPTCollectionNotice"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Collection Notice"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmd_close As System.Windows.Forms.Button
    Friend WithEvents LB_ParentParticipants As System.Windows.Forms.ListBox
    Friend WithEvents CHB_AllParticipants As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
