<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDeferredMonitoring
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
        Me.cmd_GenSave = New System.Windows.Forms.Button()
        Me.gBox_Filters = New System.Windows.Forms.GroupBox()
        Me.cmd_Gen_Save_Main = New System.Windows.Forms.Button()
        Me.cmd_close = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cb_TransactionDate = New System.Windows.Forms.ComboBox()
        Me.gBox_Filters.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmd_GenSave
        '
        Me.cmd_GenSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_GenSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_GenSave.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_GenSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_GenSave.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_GenSave.ForeColor = System.Drawing.Color.Blue
        Me.cmd_GenSave.Image = Global.AccountsManagementForms.My.Resources.Resources.EditDocumentColored22x22
        Me.cmd_GenSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_GenSave.Location = New System.Drawing.Point(262, 15)
        Me.cmd_GenSave.Name = "cmd_GenSave"
        Me.cmd_GenSave.Size = New System.Drawing.Size(35, 30)
        Me.cmd_GenSave.TabIndex = 8
        Me.cmd_GenSave.UseVisualStyleBackColor = True
        Me.cmd_GenSave.Visible = False
        '
        'gBox_Filters
        '
        Me.gBox_Filters.Controls.Add(Me.cmd_Gen_Save_Main)
        Me.gBox_Filters.Controls.Add(Me.cmd_close)
        Me.gBox_Filters.Controls.Add(Me.Label1)
        Me.gBox_Filters.Controls.Add(Me.cb_TransactionDate)
        Me.gBox_Filters.Controls.Add(Me.cmd_GenSave)
        Me.gBox_Filters.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gBox_Filters.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.gBox_Filters.Location = New System.Drawing.Point(12, 12)
        Me.gBox_Filters.Name = "gBox_Filters"
        Me.gBox_Filters.Size = New System.Drawing.Size(364, 62)
        Me.gBox_Filters.TabIndex = 11
        Me.gBox_Filters.TabStop = False
        Me.gBox_Filters.Text = "Select Filters:"
        '
        'cmd_Gen_Save_Main
        '
        Me.cmd_Gen_Save_Main.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Gen_Save_Main.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Gen_Save_Main.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Gen_Save_Main.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Gen_Save_Main.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Gen_Save_Main.ForeColor = System.Drawing.Color.Blue
        Me.cmd_Gen_Save_Main.Image = Global.AccountsManagementForms.My.Resources.Resources.EditDocumentColored22x22
        Me.cmd_Gen_Save_Main.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Gen_Save_Main.Location = New System.Drawing.Point(262, 14)
        Me.cmd_Gen_Save_Main.Name = "cmd_Gen_Save_Main"
        Me.cmd_Gen_Save_Main.Size = New System.Drawing.Size(35, 30)
        Me.cmd_Gen_Save_Main.TabIndex = 13
        Me.cmd_Gen_Save_Main.UseVisualStyleBackColor = True
        '
        'cmd_close
        '
        Me.cmd_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_close.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_close.ForeColor = System.Drawing.Color.Black
        Me.cmd_close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_close.Location = New System.Drawing.Point(303, 14)
        Me.cmd_close.Name = "cmd_close"
        Me.cmd_close.Size = New System.Drawing.Size(35, 30)
        Me.cmd_close.TabIndex = 1
        Me.cmd_close.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(7, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 14)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Allocation Date:"
        '
        'cb_TransactionDate
        '
        Me.cb_TransactionDate.DisplayMember = "ALLOCATION_DATE"
        Me.cb_TransactionDate.DropDownHeight = 108
        Me.cb_TransactionDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_TransactionDate.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_TransactionDate.FormattingEnabled = True
        Me.cb_TransactionDate.IntegralHeight = False
        Me.cb_TransactionDate.Items.AddRange(New Object() {"ALL"})
        Me.cb_TransactionDate.Location = New System.Drawing.Point(104, 20)
        Me.cb_TransactionDate.Name = "cb_TransactionDate"
        Me.cb_TransactionDate.Size = New System.Drawing.Size(152, 20)
        Me.cb_TransactionDate.TabIndex = 9
        '
        'frmDeferredMonitoring
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(387, 88)
        Me.Controls.Add(Me.gBox_Filters)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmDeferredMonitoring"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Deferred Payment Report"
        Me.gBox_Filters.ResumeLayout(False)
        Me.gBox_Filters.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmd_GenSave As System.Windows.Forms.Button
    Friend WithEvents gBox_Filters As System.Windows.Forms.GroupBox
    Friend WithEvents cmd_close As System.Windows.Forms.Button
    Friend WithEvents cb_TransactionDate As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmd_Gen_Save_Main As System.Windows.Forms.Button
End Class
