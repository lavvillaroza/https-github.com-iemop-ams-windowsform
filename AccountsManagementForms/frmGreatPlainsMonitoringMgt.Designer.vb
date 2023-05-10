<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGreatPlainsMonitoringMgt
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
        Me.xBillPeriod = New System.Windows.Forms.Label
        Me.lbl_STLRun = New System.Windows.Forms.Label
        Me.xSTLRun = New System.Windows.Forms.Label
        Me.lbl_ChargeType = New System.Windows.Forms.Label
        Me.xChargeType = New System.Windows.Forms.Label
        Me.lbl_ARAmount = New System.Windows.Forms.Label
        Me.xARAmount = New System.Windows.Forms.Label
        Me.lbl_APAmount = New System.Windows.Forms.Label
        Me.xAPAmount = New System.Windows.Forms.Label
        Me.lbl_NSSAmount = New System.Windows.Forms.Label
        Me.xNSSAmount = New System.Windows.Forms.Label
        Me.lbl_Posted = New System.Windows.Forms.Label
        Me.xPosted = New System.Windows.Forms.Label
        Me.lbl_Remarks = New System.Windows.Forms.Label
        Me.txt_Remarks = New System.Windows.Forms.TextBox
        Me.lbl_DueDate = New System.Windows.Forms.Label
        Me.xDueDate = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lbl_BatchOffset = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.CMD_Details = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.TXT_GPRefNo = New System.Windows.Forms.TextBox
        Me.cmd_close = New System.Windows.Forms.Button
        Me.cmd_Post = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Billing Period :"
        '
        'xBillPeriod
        '
        Me.xBillPeriod.AutoSize = True
        Me.xBillPeriod.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xBillPeriod.Location = New System.Drawing.Point(132, 16)
        Me.xBillPeriod.Name = "xBillPeriod"
        Me.xBillPeriod.Size = New System.Drawing.Size(66, 16)
        Me.xBillPeriod.TabIndex = 1
        Me.xBillPeriod.Text = "BillPeriod"
        '
        'lbl_STLRun
        '
        Me.lbl_STLRun.AutoSize = True
        Me.lbl_STLRun.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_STLRun.Location = New System.Drawing.Point(6, 39)
        Me.lbl_STLRun.Name = "lbl_STLRun"
        Me.lbl_STLRun.Size = New System.Drawing.Size(120, 16)
        Me.lbl_STLRun.TabIndex = 2
        Me.lbl_STLRun.Text = "Settlement Run :"
        '
        'xSTLRun
        '
        Me.xSTLRun.AutoSize = True
        Me.xSTLRun.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xSTLRun.Location = New System.Drawing.Point(132, 39)
        Me.xSTLRun.Name = "xSTLRun"
        Me.xSTLRun.Size = New System.Drawing.Size(70, 16)
        Me.xSTLRun.TabIndex = 3
        Me.xSTLRun.Text = "STL_RUN"
        '
        'lbl_ChargeType
        '
        Me.lbl_ChargeType.AutoSize = True
        Me.lbl_ChargeType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ChargeType.Location = New System.Drawing.Point(225, 39)
        Me.lbl_ChargeType.Name = "lbl_ChargeType"
        Me.lbl_ChargeType.Size = New System.Drawing.Size(106, 16)
        Me.lbl_ChargeType.TabIndex = 4
        Me.lbl_ChargeType.Text = "Charge Type :"
        '
        'xChargeType
        '
        Me.xChargeType.AutoSize = True
        Me.xChargeType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xChargeType.Location = New System.Drawing.Point(333, 39)
        Me.xChargeType.Name = "xChargeType"
        Me.xChargeType.Size = New System.Drawing.Size(91, 16)
        Me.xChargeType.TabIndex = 5
        Me.xChargeType.Text = "Charge_Type"
        '
        'lbl_ARAmount
        '
        Me.lbl_ARAmount.AutoSize = True
        Me.lbl_ARAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ARAmount.Location = New System.Drawing.Point(6, 92)
        Me.lbl_ARAmount.Name = "lbl_ARAmount"
        Me.lbl_ARAmount.Size = New System.Drawing.Size(92, 16)
        Me.lbl_ARAmount.TabIndex = 6
        Me.lbl_ARAmount.Text = "AR Amount :"
        '
        'xARAmount
        '
        Me.xARAmount.AutoSize = True
        Me.xARAmount.Font = New System.Drawing.Font("Helvetica Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xARAmount.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.xARAmount.Location = New System.Drawing.Point(222, 93)
        Me.xARAmount.Name = "xARAmount"
        Me.xARAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.xARAmount.Size = New System.Drawing.Size(75, 15)
        Me.xARAmount.TabIndex = 7
        Me.xARAmount.Text = "AR_AMOUNT"
        Me.xARAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_APAmount
        '
        Me.lbl_APAmount.AutoSize = True
        Me.lbl_APAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_APAmount.Location = New System.Drawing.Point(7, 76)
        Me.lbl_APAmount.Name = "lbl_APAmount"
        Me.lbl_APAmount.Size = New System.Drawing.Size(91, 16)
        Me.lbl_APAmount.TabIndex = 8
        Me.lbl_APAmount.Text = "AP Amount :"
        '
        'xAPAmount
        '
        Me.xAPAmount.AutoSize = True
        Me.xAPAmount.Font = New System.Drawing.Font("Helvetica Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xAPAmount.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.xAPAmount.Location = New System.Drawing.Point(222, 77)
        Me.xAPAmount.Name = "xAPAmount"
        Me.xAPAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.xAPAmount.Size = New System.Drawing.Size(74, 15)
        Me.xAPAmount.TabIndex = 9
        Me.xAPAmount.Text = "AP_AMOUNT"
        Me.xAPAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_NSSAmount
        '
        Me.lbl_NSSAmount.AutoSize = True
        Me.lbl_NSSAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NSSAmount.Location = New System.Drawing.Point(6, 108)
        Me.lbl_NSSAmount.Name = "lbl_NSSAmount"
        Me.lbl_NSSAmount.Size = New System.Drawing.Size(102, 16)
        Me.lbl_NSSAmount.TabIndex = 10
        Me.lbl_NSSAmount.Text = "NSS Amount :"
        '
        'xNSSAmount
        '
        Me.xNSSAmount.AutoSize = True
        Me.xNSSAmount.Font = New System.Drawing.Font("Helvetica Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xNSSAmount.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.xNSSAmount.Location = New System.Drawing.Point(222, 108)
        Me.xNSSAmount.Name = "xNSSAmount"
        Me.xNSSAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.xNSSAmount.Size = New System.Drawing.Size(82, 15)
        Me.xNSSAmount.TabIndex = 11
        Me.xNSSAmount.Text = "NSS_AMOUNT"
        Me.xNSSAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_Posted
        '
        Me.lbl_Posted.AutoSize = True
        Me.lbl_Posted.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Posted.Location = New System.Drawing.Point(6, 140)
        Me.lbl_Posted.Name = "lbl_Posted"
        Me.lbl_Posted.Size = New System.Drawing.Size(61, 16)
        Me.lbl_Posted.TabIndex = 12
        Me.lbl_Posted.Text = "Posted:"
        '
        'xPosted
        '
        Me.xPosted.AutoSize = True
        Me.xPosted.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xPosted.Location = New System.Drawing.Point(73, 140)
        Me.xPosted.Name = "xPosted"
        Me.xPosted.Size = New System.Drawing.Size(64, 16)
        Me.xPosted.TabIndex = 13
        Me.xPosted.Text = "POSTED"
        '
        'lbl_Remarks
        '
        Me.lbl_Remarks.AutoSize = True
        Me.lbl_Remarks.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Remarks.Location = New System.Drawing.Point(6, 165)
        Me.lbl_Remarks.Name = "lbl_Remarks"
        Me.lbl_Remarks.Size = New System.Drawing.Size(74, 16)
        Me.lbl_Remarks.TabIndex = 14
        Me.lbl_Remarks.Text = "Remarks:"
        '
        'txt_Remarks
        '
        Me.txt_Remarks.Location = New System.Drawing.Point(9, 181)
        Me.txt_Remarks.MaxLength = 200
        Me.txt_Remarks.Multiline = True
        Me.txt_Remarks.Name = "txt_Remarks"
        Me.txt_Remarks.Size = New System.Drawing.Size(459, 61)
        Me.txt_Remarks.TabIndex = 15
        '
        'lbl_DueDate
        '
        Me.lbl_DueDate.AutoSize = True
        Me.lbl_DueDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DueDate.Location = New System.Drawing.Point(250, 16)
        Me.lbl_DueDate.Name = "lbl_DueDate"
        Me.lbl_DueDate.Size = New System.Drawing.Size(81, 16)
        Me.lbl_DueDate.TabIndex = 16
        Me.lbl_DueDate.Text = "Due Date :"
        '
        'xDueDate
        '
        Me.xDueDate.AutoSize = True
        Me.xDueDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xDueDate.Location = New System.Drawing.Point(333, 16)
        Me.xDueDate.Name = "xDueDate"
        Me.xDueDate.Size = New System.Drawing.Size(69, 16)
        Me.xDueDate.TabIndex = 17
        Me.xDueDate.Text = "Due_Date"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lbl_BatchOffset)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.CMD_Details)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TXT_GPRefNo)
        Me.GroupBox1.Controls.Add(Me.cmd_close)
        Me.GroupBox1.Controls.Add(Me.cmd_Post)
        Me.GroupBox1.Controls.Add(Me.xDueDate)
        Me.GroupBox1.Controls.Add(Me.lbl_DueDate)
        Me.GroupBox1.Controls.Add(Me.txt_Remarks)
        Me.GroupBox1.Controls.Add(Me.lbl_Remarks)
        Me.GroupBox1.Controls.Add(Me.xPosted)
        Me.GroupBox1.Controls.Add(Me.lbl_Posted)
        Me.GroupBox1.Controls.Add(Me.xNSSAmount)
        Me.GroupBox1.Controls.Add(Me.lbl_NSSAmount)
        Me.GroupBox1.Controls.Add(Me.xAPAmount)
        Me.GroupBox1.Controls.Add(Me.lbl_APAmount)
        Me.GroupBox1.Controls.Add(Me.xARAmount)
        Me.GroupBox1.Controls.Add(Me.lbl_ARAmount)
        Me.GroupBox1.Controls.Add(Me.xChargeType)
        Me.GroupBox1.Controls.Add(Me.lbl_ChargeType)
        Me.GroupBox1.Controls.Add(Me.xSTLRun)
        Me.GroupBox1.Controls.Add(Me.lbl_STLRun)
        Me.GroupBox1.Controls.Add(Me.xBillPeriod)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(474, 295)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        '
        'lbl_BatchOffset
        '
        Me.lbl_BatchOffset.AutoSize = True
        Me.lbl_BatchOffset.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_BatchOffset.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lbl_BatchOffset.Location = New System.Drawing.Point(366, 136)
        Me.lbl_BatchOffset.Name = "lbl_BatchOffset"
        Me.lbl_BatchOffset.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbl_BatchOffset.Size = New System.Drawing.Size(94, 16)
        Me.lbl_BatchOffset.TabIndex = 24
        Me.lbl_BatchOffset.Text = "OffsetBatchNo"
        Me.lbl_BatchOffset.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(204, 136)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(120, 16)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "Batch/Offset No."
        '
        'CMD_Details
        '
        Me.CMD_Details.Image = Global.AccountsManagementForms.My.Resources.Resources.execute
        Me.CMD_Details.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CMD_Details.Location = New System.Drawing.Point(105, 248)
        Me.CMD_Details.Name = "CMD_Details"
        Me.CMD_Details.Size = New System.Drawing.Size(117, 34)
        Me.CMD_Details.TabIndex = 22
        Me.CMD_Details.Text = "View Details"
        Me.CMD_Details.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(205, 159)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 16)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "GP Ref No."
        '
        'TXT_GPRefNo
        '
        Me.TXT_GPRefNo.Location = New System.Drawing.Point(296, 155)
        Me.TXT_GPRefNo.MaxLength = 25
        Me.TXT_GPRefNo.Name = "TXT_GPRefNo"
        Me.TXT_GPRefNo.Size = New System.Drawing.Size(172, 20)
        Me.TXT_GPRefNo.TabIndex = 1
        '
        'cmd_close
        '
        Me.cmd_close.Image = Global.AccountsManagementForms.My.Resources.Resources.close
        Me.cmd_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_close.Location = New System.Drawing.Point(351, 248)
        Me.cmd_close.Name = "cmd_close"
        Me.cmd_close.Size = New System.Drawing.Size(117, 34)
        Me.cmd_close.TabIndex = 19
        Me.cmd_close.Text = "Close"
        Me.cmd_close.UseVisualStyleBackColor = True
        '
        'cmd_Post
        '
        Me.cmd_Post.Image = Global.AccountsManagementForms.My.Resources.Resources.save
        Me.cmd_Post.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Post.Location = New System.Drawing.Point(228, 248)
        Me.cmd_Post.Name = "cmd_Post"
        Me.cmd_Post.Size = New System.Drawing.Size(117, 34)
        Me.cmd_Post.TabIndex = 18
        Me.cmd_Post.Text = "Save"
        Me.cmd_Post.UseVisualStyleBackColor = True
        '
        'frmGreatPlainsMonitoringMgt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(492, 305)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.Name = "frmGreatPlainsMonitoringMgt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Post to Great Plains"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents xBillPeriod As System.Windows.Forms.Label
    Friend WithEvents lbl_STLRun As System.Windows.Forms.Label
    Friend WithEvents xSTLRun As System.Windows.Forms.Label
    Friend WithEvents lbl_ChargeType As System.Windows.Forms.Label
    Friend WithEvents xChargeType As System.Windows.Forms.Label
    Friend WithEvents lbl_ARAmount As System.Windows.Forms.Label
    Friend WithEvents xARAmount As System.Windows.Forms.Label
    Friend WithEvents lbl_APAmount As System.Windows.Forms.Label
    Friend WithEvents xAPAmount As System.Windows.Forms.Label
    Friend WithEvents lbl_NSSAmount As System.Windows.Forms.Label
    Friend WithEvents xNSSAmount As System.Windows.Forms.Label
    Friend WithEvents lbl_Posted As System.Windows.Forms.Label
    Friend WithEvents xPosted As System.Windows.Forms.Label
    Friend WithEvents lbl_Remarks As System.Windows.Forms.Label
    Friend WithEvents txt_Remarks As System.Windows.Forms.TextBox
    Friend WithEvents lbl_DueDate As System.Windows.Forms.Label
    Friend WithEvents xDueDate As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmd_Post As System.Windows.Forms.Button
    Friend WithEvents cmd_close As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TXT_GPRefNo As System.Windows.Forms.TextBox
    Friend WithEvents CMD_Details As System.Windows.Forms.Button
    Friend WithEvents lbl_BatchOffset As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
