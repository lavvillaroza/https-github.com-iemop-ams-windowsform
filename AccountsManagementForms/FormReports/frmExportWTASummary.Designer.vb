<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmExportWTASummary
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.ddlDueDate = New System.Windows.Forms.ComboBox()
        Me.grpSearch = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmd_ExportInExcel = New System.Windows.Forms.Button()
        Me.cmd_Close = New System.Windows.Forms.Button()
        Me.grpSearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'ddlDueDate
        '
        Me.ddlDueDate.BackColor = System.Drawing.Color.White
        Me.ddlDueDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlDueDate.FormattingEnabled = True
        Me.ddlDueDate.Location = New System.Drawing.Point(15, 36)
        Me.ddlDueDate.Name = "ddlDueDate"
        Me.ddlDueDate.Size = New System.Drawing.Size(233, 24)
        Me.ddlDueDate.TabIndex = 4
        '
        'grpSearch
        '
        Me.grpSearch.Controls.Add(Me.Label3)
        Me.grpSearch.Controls.Add(Me.cmd_ExportInExcel)
        Me.grpSearch.Controls.Add(Me.cmd_Close)
        Me.grpSearch.Controls.Add(Me.ddlDueDate)
        Me.grpSearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpSearch.Location = New System.Drawing.Point(25, 12)
        Me.grpSearch.Name = "grpSearch"
        Me.grpSearch.Size = New System.Drawing.Size(261, 201)
        Me.grpSearch.TabIndex = 5
        Me.grpSearch.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(4, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 14)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Due Date:"
        '
        'cmd_ExportInExcel
        '
        Me.cmd_ExportInExcel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmd_ExportInExcel.BackColor = System.Drawing.Color.White
        Me.cmd_ExportInExcel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_ExportInExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_ExportInExcel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_ExportInExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_ExportInExcel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ExportInExcel.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.cmd_ExportInExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_ExportInExcel.Location = New System.Drawing.Point(16, 100)
        Me.cmd_ExportInExcel.Name = "cmd_ExportInExcel"
        Me.cmd_ExportInExcel.Size = New System.Drawing.Size(232, 37)
        Me.cmd_ExportInExcel.TabIndex = 11
        Me.cmd_ExportInExcel.Text = "Export in Excel"
        Me.cmd_ExportInExcel.UseVisualStyleBackColor = False
        '
        'cmd_Close
        '
        Me.cmd_Close.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmd_Close.BackColor = System.Drawing.Color.White
        Me.cmd_Close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Close.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Close.Location = New System.Drawing.Point(17, 143)
        Me.cmd_Close.Name = "cmd_Close"
        Me.cmd_Close.Size = New System.Drawing.Size(232, 37)
        Me.cmd_Close.TabIndex = 9
        Me.cmd_Close.Text = "Close"
        Me.cmd_Close.UseVisualStyleBackColor = False
        '
        'frmExportWTASummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(306, 225)
        Me.Controls.Add(Me.grpSearch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExportWTASummary"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WESM Transaction Allocation Summary"
        Me.grpSearch.ResumeLayout(False)
        Me.grpSearch.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ddlDueDate As ComboBox
    Friend WithEvents grpSearch As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cmd_ExportInExcel As Button
    Friend WithEvents cmd_Close As Button
End Class
