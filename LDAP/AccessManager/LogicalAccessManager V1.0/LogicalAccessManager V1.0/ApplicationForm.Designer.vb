<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ApplicationForm
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.AF_APPID_TXTBOX = New System.Windows.Forms.TextBox()
        Me.AF_INACTIVE_RADBTN = New System.Windows.Forms.RadioButton()
        Me.AF_APPNAME_TXTBOX = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.AF_ACTIVE_RADBTN = New System.Windows.Forms.RadioButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.AF_CANCEL_BTN = New System.Windows.Forms.Button()
        Me.AF_SAVE_BTN = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel2)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Location = New System.Drawing.Point(6, -1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(423, 165)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.AF_APPID_TXTBOX)
        Me.Panel2.Controls.Add(Me.AF_INACTIVE_RADBTN)
        Me.Panel2.Controls.Add(Me.AF_APPNAME_TXTBOX)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.AF_ACTIVE_RADBTN)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Location = New System.Drawing.Point(6, 13)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(412, 105)
        Me.Panel2.TabIndex = 12
        '
        'AF_APPID_TXTBOX
        '
        Me.AF_APPID_TXTBOX.BackColor = System.Drawing.Color.White
        Me.AF_APPID_TXTBOX.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AF_APPID_TXTBOX.Location = New System.Drawing.Point(135, 8)
        Me.AF_APPID_TXTBOX.MaxLength = 50
        Me.AF_APPID_TXTBOX.Name = "AF_APPID_TXTBOX"
        Me.AF_APPID_TXTBOX.Size = New System.Drawing.Size(147, 22)
        Me.AF_APPID_TXTBOX.TabIndex = 3
        '
        'AF_INACTIVE_RADBTN
        '
        Me.AF_INACTIVE_RADBTN.AutoSize = True
        Me.AF_INACTIVE_RADBTN.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AF_INACTIVE_RADBTN.Location = New System.Drawing.Point(196, 78)
        Me.AF_INACTIVE_RADBTN.Name = "AF_INACTIVE_RADBTN"
        Me.AF_INACTIVE_RADBTN.Size = New System.Drawing.Size(67, 18)
        Me.AF_INACTIVE_RADBTN.TabIndex = 10
        Me.AF_INACTIVE_RADBTN.TabStop = True
        Me.AF_INACTIVE_RADBTN.Text = "InActive"
        Me.AF_INACTIVE_RADBTN.UseVisualStyleBackColor = True
        '
        'AF_APPNAME_TXTBOX
        '
        Me.AF_APPNAME_TXTBOX.BackColor = System.Drawing.Color.White
        Me.AF_APPNAME_TXTBOX.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AF_APPNAME_TXTBOX.Location = New System.Drawing.Point(135, 42)
        Me.AF_APPNAME_TXTBOX.MaxLength = 255
        Me.AF_APPNAME_TXTBOX.Name = "AF_APPNAME_TXTBOX"
        Me.AF_APPNAME_TXTBOX.Size = New System.Drawing.Size(255, 22)
        Me.AF_APPNAME_TXTBOX.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(22, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Application ID"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(123, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 14)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = ":"
        '
        'AF_ACTIVE_RADBTN
        '
        Me.AF_ACTIVE_RADBTN.AutoSize = True
        Me.AF_ACTIVE_RADBTN.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AF_ACTIVE_RADBTN.Location = New System.Drawing.Point(135, 77)
        Me.AF_ACTIVE_RADBTN.Name = "AF_ACTIVE_RADBTN"
        Me.AF_ACTIVE_RADBTN.Size = New System.Drawing.Size(56, 18)
        Me.AF_ACTIVE_RADBTN.TabIndex = 9
        Me.AF_ACTIVE_RADBTN.TabStop = True
        Me.AF_ACTIVE_RADBTN.Text = "Active"
        Me.AF_ACTIVE_RADBTN.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(123, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 14)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = ":"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(22, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Application Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(22, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Status"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(123, 78)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(10, 14)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = ":"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.AF_CANCEL_BTN)
        Me.Panel1.Controls.Add(Me.AF_SAVE_BTN)
        Me.Panel1.Location = New System.Drawing.Point(6, 122)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(412, 36)
        Me.Panel1.TabIndex = 11
        '
        'AF_CANCEL_BTN
        '
        Me.AF_CANCEL_BTN.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AF_CANCEL_BTN.Location = New System.Drawing.Point(315, 5)
        Me.AF_CANCEL_BTN.Name = "AF_CANCEL_BTN"
        Me.AF_CANCEL_BTN.Size = New System.Drawing.Size(75, 25)
        Me.AF_CANCEL_BTN.TabIndex = 1
        Me.AF_CANCEL_BTN.Text = "Cancel"
        Me.AF_CANCEL_BTN.UseVisualStyleBackColor = True
        '
        'AF_SAVE_BTN
        '
        Me.AF_SAVE_BTN.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AF_SAVE_BTN.Location = New System.Drawing.Point(234, 5)
        Me.AF_SAVE_BTN.Name = "AF_SAVE_BTN"
        Me.AF_SAVE_BTN.Size = New System.Drawing.Size(75, 25)
        Me.AF_SAVE_BTN.TabIndex = 0
        Me.AF_SAVE_BTN.Text = "Save"
        Me.AF_SAVE_BTN.UseVisualStyleBackColor = True
        '
        'ApplicationForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(436, 171)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "ApplicationForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ApplicationForm"
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents AF_ACTIVE_RADBTN As System.Windows.Forms.RadioButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents AF_APPNAME_TXTBOX As System.Windows.Forms.TextBox
    Friend WithEvents AF_APPID_TXTBOX As System.Windows.Forms.TextBox
    Friend WithEvents AF_INACTIVE_RADBTN As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents AF_CANCEL_BTN As System.Windows.Forms.Button
    Friend WithEvents AF_SAVE_BTN As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
End Class
