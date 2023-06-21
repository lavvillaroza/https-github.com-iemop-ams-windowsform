<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrudentialRefundTransDate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrudentialRefundTransDate))
        Me.gpCollection = New System.Windows.Forms.GroupBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.lblTransaction = New System.Windows.Forms.Label()
        Me.dtAllocationDate = New System.Windows.Forms.DateTimePicker()
        Me.gpCollection.SuspendLayout()
        Me.SuspendLayout()
        '
        'gpCollection
        '
        Me.gpCollection.Controls.Add(Me.btnCancel)
        Me.gpCollection.Controls.Add(Me.btnOK)
        Me.gpCollection.Controls.Add(Me.lblTransaction)
        Me.gpCollection.Controls.Add(Me.dtAllocationDate)
        Me.gpCollection.Location = New System.Drawing.Point(7, 4)
        Me.gpCollection.Name = "gpCollection"
        Me.gpCollection.Size = New System.Drawing.Size(285, 94)
        Me.gpCollection.TabIndex = 14
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
        Me.btnCancel.Location = New System.Drawing.Point(237, 45)
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
        Me.btnOK.Location = New System.Drawing.Point(190, 45)
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
        Me.lblTransaction.Location = New System.Drawing.Point(9, 21)
        Me.lblTransaction.Name = "lblTransaction"
        Me.lblTransaction.Size = New System.Drawing.Size(101, 14)
        Me.lblTransaction.TabIndex = 2
        Me.lblTransaction.Text = "Transaction Date:"
        '
        'dtAllocationDate
        '
        Me.dtAllocationDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtAllocationDate.Location = New System.Drawing.Point(118, 19)
        Me.dtAllocationDate.Name = "dtAllocationDate"
        Me.dtAllocationDate.Size = New System.Drawing.Size(160, 20)
        Me.dtAllocationDate.TabIndex = 3
        '
        'frmPrudentialRefundTransDate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(299, 101)
        Me.ControlBox = False
        Me.Controls.Add(Me.gpCollection)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmPrudentialRefundTransDate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Transaction Date"
        Me.gpCollection.ResumeLayout(False)
        Me.gpCollection.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gpCollection As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lblTransaction As System.Windows.Forms.Label
    Friend WithEvents dtAllocationDate As System.Windows.Forms.DateTimePicker
End Class
