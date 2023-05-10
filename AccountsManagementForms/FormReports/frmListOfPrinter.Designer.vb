<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmListOfPrinter
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
        Me.GB_Allocation = New System.Windows.Forms.GroupBox()
        Me.btn_Ok = New System.Windows.Forms.Button()
        Me.btn_Close = New System.Windows.Forms.Button()
        Me.cbo_PrinterList = New System.Windows.Forms.ComboBox()
        Me.GB_Allocation.SuspendLayout()
        Me.SuspendLayout()
        '
        'GB_Allocation
        '
        Me.GB_Allocation.Controls.Add(Me.btn_Ok)
        Me.GB_Allocation.Controls.Add(Me.btn_Close)
        Me.GB_Allocation.Controls.Add(Me.cbo_PrinterList)
        Me.GB_Allocation.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GB_Allocation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GB_Allocation.Location = New System.Drawing.Point(12, 12)
        Me.GB_Allocation.Name = "GB_Allocation"
        Me.GB_Allocation.Size = New System.Drawing.Size(269, 98)
        Me.GB_Allocation.TabIndex = 61
        Me.GB_Allocation.TabStop = False
        '
        'btn_Ok
        '
        Me.btn_Ok.BackColor = System.Drawing.Color.White
        Me.btn_Ok.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Ok.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Ok.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Ok.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Ok.ForeColor = System.Drawing.Color.Black
        Me.btn_Ok.Image = Global.AccountsManagementForms.My.Resources.Resources.OkIcon22x22
        Me.btn_Ok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Ok.Location = New System.Drawing.Point(6, 46)
        Me.btn_Ok.Name = "btn_Ok"
        Me.btn_Ok.Size = New System.Drawing.Size(125, 39)
        Me.btn_Ok.TabIndex = 59
        Me.btn_Ok.Text = "OK"
        Me.btn_Ok.UseVisualStyleBackColor = False
        '
        'btn_Close
        '
        Me.btn_Close.BackColor = System.Drawing.Color.White
        Me.btn_Close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Close.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Close.ForeColor = System.Drawing.Color.Black
        Me.btn_Close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btn_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Close.Location = New System.Drawing.Point(137, 46)
        Me.btn_Close.Name = "btn_Close"
        Me.btn_Close.Size = New System.Drawing.Size(125, 39)
        Me.btn_Close.TabIndex = 58
        Me.btn_Close.Text = "Close"
        Me.btn_Close.UseVisualStyleBackColor = False
        '
        'cbo_PrinterList
        '
        Me.cbo_PrinterList.BackColor = System.Drawing.Color.White
        Me.cbo_PrinterList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_PrinterList.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cbo_PrinterList.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_PrinterList.ForeColor = System.Drawing.Color.DimGray
        Me.cbo_PrinterList.FormattingEnabled = True
        Me.cbo_PrinterList.Location = New System.Drawing.Point(9, 19)
        Me.cbo_PrinterList.Name = "cbo_PrinterList"
        Me.cbo_PrinterList.Size = New System.Drawing.Size(253, 20)
        Me.cbo_PrinterList.TabIndex = 49
        '
        'frmListOfPrinter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(291, 122)
        Me.Controls.Add(Me.GB_Allocation)
        Me.MaximizeBox = False
        Me.Name = "frmListOfPrinter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Printer"
        Me.GB_Allocation.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GB_Allocation As System.Windows.Forms.GroupBox
    Friend WithEvents cbo_PrinterList As System.Windows.Forms.ComboBox
    Friend WithEvents btn_Ok As System.Windows.Forms.Button
    Friend WithEvents btn_Close As System.Windows.Forms.Button
End Class
