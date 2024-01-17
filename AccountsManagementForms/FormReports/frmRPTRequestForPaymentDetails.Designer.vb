<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRPTRequestForPaymentDetails
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
        Me.gbox_PaymentTotals = New System.Windows.Forms.GroupBox()
        Me.txt_Payment = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_HeldCollection = New System.Windows.Forms.TextBox()
        Me.txt_Collection = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txt_TransferPEMC = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txt_MFApplication = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txt_PRReplenish = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txt_NSSApplied = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txt_totDeferred = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt_TotLBC = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txt_TotRTGS = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.gbox_PaymentTotals.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbox_PaymentTotals
        '
        Me.gbox_PaymentTotals.Controls.Add(Me.txt_Payment)
        Me.gbox_PaymentTotals.Controls.Add(Me.Label1)
        Me.gbox_PaymentTotals.Controls.Add(Me.txt_HeldCollection)
        Me.gbox_PaymentTotals.Controls.Add(Me.txt_Collection)
        Me.gbox_PaymentTotals.Controls.Add(Me.Label13)
        Me.gbox_PaymentTotals.Controls.Add(Me.txt_TransferPEMC)
        Me.gbox_PaymentTotals.Controls.Add(Me.Label22)
        Me.gbox_PaymentTotals.Controls.Add(Me.Label12)
        Me.gbox_PaymentTotals.Controls.Add(Me.txt_MFApplication)
        Me.gbox_PaymentTotals.Controls.Add(Me.Label16)
        Me.gbox_PaymentTotals.Controls.Add(Me.txt_PRReplenish)
        Me.gbox_PaymentTotals.Controls.Add(Me.Label15)
        Me.gbox_PaymentTotals.Controls.Add(Me.txt_NSSApplied)
        Me.gbox_PaymentTotals.Controls.Add(Me.Label14)
        Me.gbox_PaymentTotals.Controls.Add(Me.txt_totDeferred)
        Me.gbox_PaymentTotals.Controls.Add(Me.Label10)
        Me.gbox_PaymentTotals.Controls.Add(Me.txt_TotLBC)
        Me.gbox_PaymentTotals.Controls.Add(Me.Label9)
        Me.gbox_PaymentTotals.Controls.Add(Me.txt_TotRTGS)
        Me.gbox_PaymentTotals.Controls.Add(Me.Label8)
        Me.gbox_PaymentTotals.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbox_PaymentTotals.Location = New System.Drawing.Point(12, 12)
        Me.gbox_PaymentTotals.Name = "gbox_PaymentTotals"
        Me.gbox_PaymentTotals.Size = New System.Drawing.Size(764, 164)
        Me.gbox_PaymentTotals.TabIndex = 23
        Me.gbox_PaymentTotals.TabStop = False
        '
        'txt_Payment
        '
        Me.txt_Payment.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_Payment.Enabled = False
        Me.txt_Payment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Payment.Location = New System.Drawing.Point(561, 95)
        Me.txt_Payment.Name = "txt_Payment"
        Me.txt_Payment.ReadOnly = True
        Me.txt_Payment.Size = New System.Drawing.Size(183, 20)
        Me.txt_Payment.TabIndex = 30
        Me.txt_Payment.Text = "0.00"
        Me.txt_Payment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(398, 97)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 14)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Total Payments:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txt_HeldCollection
        '
        Me.txt_HeldCollection.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_HeldCollection.Enabled = False
        Me.txt_HeldCollection.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_HeldCollection.Location = New System.Drawing.Point(146, 95)
        Me.txt_HeldCollection.Name = "txt_HeldCollection"
        Me.txt_HeldCollection.ReadOnly = True
        Me.txt_HeldCollection.Size = New System.Drawing.Size(183, 20)
        Me.txt_HeldCollection.TabIndex = 38
        Me.txt_HeldCollection.Text = "0.00"
        Me.txt_HeldCollection.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_Collection
        '
        Me.txt_Collection.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_Collection.Enabled = False
        Me.txt_Collection.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Collection.Location = New System.Drawing.Point(561, 117)
        Me.txt_Collection.Name = "txt_Collection"
        Me.txt_Collection.ReadOnly = True
        Me.txt_Collection.Size = New System.Drawing.Size(183, 20)
        Me.txt_Collection.TabIndex = 28
        Me.txt_Collection.Text = "0.00"
        Me.txt_Collection.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(8, 97)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(121, 14)
        Me.Label13.TabIndex = 37
        Me.Label13.Text = "Total Held Collection:"
        '
        'txt_TransferPEMC
        '
        Me.txt_TransferPEMC.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_TransferPEMC.Enabled = False
        Me.txt_TransferPEMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_TransferPEMC.Location = New System.Drawing.Point(146, 117)
        Me.txt_TransferPEMC.Name = "txt_TransferPEMC"
        Me.txt_TransferPEMC.ReadOnly = True
        Me.txt_TransferPEMC.Size = New System.Drawing.Size(183, 20)
        Me.txt_TransferPEMC.TabIndex = 36
        Me.txt_TransferPEMC.Text = "0.00"
        Me.txt_TransferPEMC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(398, 119)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(139, 14)
        Me.Label22.TabIndex = 27
        Me.Label22.Text = "Total Collection Applied:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(8, 119)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(86, 14)
        Me.Label12.TabIndex = 35
        Me.Label12.Text = "Total To PEMC:"
        '
        'txt_MFApplication
        '
        Me.txt_MFApplication.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_MFApplication.Enabled = False
        Me.txt_MFApplication.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_MFApplication.Location = New System.Drawing.Point(146, 64)
        Me.txt_MFApplication.Name = "txt_MFApplication"
        Me.txt_MFApplication.ReadOnly = True
        Me.txt_MFApplication.Size = New System.Drawing.Size(183, 20)
        Me.txt_MFApplication.TabIndex = 34
        Me.txt_MFApplication.Text = "0.00"
        Me.txt_MFApplication.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(8, 66)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(55, 14)
        Me.Label16.TabIndex = 33
        Me.Label16.Text = "Total MF:"
        '
        'txt_PRReplenish
        '
        Me.txt_PRReplenish.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_PRReplenish.Enabled = False
        Me.txt_PRReplenish.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_PRReplenish.Location = New System.Drawing.Point(561, 64)
        Me.txt_PRReplenish.Name = "txt_PRReplenish"
        Me.txt_PRReplenish.ReadOnly = True
        Me.txt_PRReplenish.Size = New System.Drawing.Size(183, 20)
        Me.txt_PRReplenish.TabIndex = 32
        Me.txt_PRReplenish.Text = "0.00"
        Me.txt_PRReplenish.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(398, 66)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(153, 14)
        Me.Label15.TabIndex = 31
        Me.Label15.Text = "Prudential Replenishment:"
        '
        'txt_NSSApplied
        '
        Me.txt_NSSApplied.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_NSSApplied.Enabled = False
        Me.txt_NSSApplied.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_NSSApplied.Location = New System.Drawing.Point(561, 42)
        Me.txt_NSSApplied.Name = "txt_NSSApplied"
        Me.txt_NSSApplied.ReadOnly = True
        Me.txt_NSSApplied.Size = New System.Drawing.Size(183, 20)
        Me.txt_NSSApplied.TabIndex = 30
        Me.txt_NSSApplied.Text = "0.00"
        Me.txt_NSSApplied.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(398, 44)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(76, 14)
        Me.Label14.TabIndex = 29
        Me.Label14.Text = "NSS Applied:"
        '
        'txt_totDeferred
        '
        Me.txt_totDeferred.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_totDeferred.Enabled = False
        Me.txt_totDeferred.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_totDeferred.Location = New System.Drawing.Point(561, 19)
        Me.txt_totDeferred.Name = "txt_totDeferred"
        Me.txt_totDeferred.ReadOnly = True
        Me.txt_totDeferred.Size = New System.Drawing.Size(183, 20)
        Me.txt_totDeferred.TabIndex = 28
        Me.txt_totDeferred.Text = "0.00"
        Me.txt_totDeferred.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(398, 21)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(88, 14)
        Me.Label10.TabIndex = 27
        Me.Label10.Text = "Total Deferred:"
        '
        'txt_TotLBC
        '
        Me.txt_TotLBC.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_TotLBC.Enabled = False
        Me.txt_TotLBC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_TotLBC.Location = New System.Drawing.Point(146, 42)
        Me.txt_TotLBC.Name = "txt_TotLBC"
        Me.txt_TotLBC.ReadOnly = True
        Me.txt_TotLBC.Size = New System.Drawing.Size(183, 20)
        Me.txt_TotLBC.TabIndex = 26
        Me.txt_TotLBC.Text = "0.00"
        Me.txt_TotLBC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(8, 44)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 14)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "Total LBC:"
        '
        'txt_TotRTGS
        '
        Me.txt_TotRTGS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_TotRTGS.Enabled = False
        Me.txt_TotRTGS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_TotRTGS.Location = New System.Drawing.Point(146, 19)
        Me.txt_TotRTGS.Name = "txt_TotRTGS"
        Me.txt_TotRTGS.ReadOnly = True
        Me.txt_TotRTGS.Size = New System.Drawing.Size(183, 20)
        Me.txt_TotRTGS.TabIndex = 24
        Me.txt_TotRTGS.Text = "0.00"
        Me.txt_TotRTGS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(8, 21)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 14)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "Total RTGS:"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(657, 182)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(119, 37)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Close"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'frmRPTRequestForPaymentDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(789, 229)
        Me.ControlBox = False
        Me.Controls.Add(Me.gbox_PaymentTotals)
        Me.Controls.Add(Me.Button1)
        Me.Name = "frmRPTRequestForPaymentDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Request For Payment - Summary of Payments"
        Me.gbox_PaymentTotals.ResumeLayout(False)
        Me.gbox_PaymentTotals.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents gbox_PaymentTotals As System.Windows.Forms.GroupBox
    Friend WithEvents txt_HeldCollection As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txt_TransferPEMC As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txt_MFApplication As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txt_PRReplenish As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txt_NSSApplied As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txt_totDeferred As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txt_TotLBC As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txt_TotRTGS As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txt_Collection As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txt_Payment As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
