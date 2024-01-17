<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWESMSummary
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
        Me.grpSearch = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmd_ExportInExcel = New System.Windows.Forms.Button()
        Me.cmd_Close = New System.Windows.Forms.Button()
        Me.cbo_ParticipantId = New System.Windows.Forms.ComboBox()
        Me.chkParticipantID = New System.Windows.Forms.CheckBox()
        Me.grpSearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpSearch
        '
        Me.grpSearch.Controls.Add(Me.Label1)
        Me.grpSearch.Controls.Add(Me.dtFrom)
        Me.grpSearch.Controls.Add(Me.Label6)
        Me.grpSearch.Controls.Add(Me.dtTo)
        Me.grpSearch.Controls.Add(Me.Label3)
        Me.grpSearch.Controls.Add(Me.cmd_ExportInExcel)
        Me.grpSearch.Controls.Add(Me.cmd_Close)
        Me.grpSearch.Controls.Add(Me.cbo_ParticipantId)
        Me.grpSearch.Controls.Add(Me.chkParticipantID)
        Me.grpSearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpSearch.Location = New System.Drawing.Point(12, 7)
        Me.grpSearch.Name = "grpSearch"
        Me.grpSearch.Size = New System.Drawing.Size(297, 187)
        Me.grpSearch.TabIndex = 0
        Me.grpSearch.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.Label1.Location = New System.Drawing.Point(10, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 14)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "From"
        '
        'dtFrom
        '
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFrom.Location = New System.Drawing.Point(47, 31)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(104, 22)
        Me.dtFrom.TabIndex = 20
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.Label6.Location = New System.Drawing.Point(157, 39)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(18, 14)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "To"
        '
        'dtTo
        '
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTo.Location = New System.Drawing.Point(180, 31)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(104, 22)
        Me.dtTo.TabIndex = 21
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(9, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 14)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Categories:"
        '
        'cmd_ExportInExcel
        '
        Me.cmd_ExportInExcel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.cmd_ExportInExcel.BackColor = System.Drawing.Color.White
        Me.cmd_ExportInExcel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_ExportInExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_ExportInExcel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_ExportInExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_ExportInExcel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ExportInExcel.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.cmd_ExportInExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_ExportInExcel.Location = New System.Drawing.Point(10, 94)
        Me.cmd_ExportInExcel.Name = "cmd_ExportInExcel"
        Me.cmd_ExportInExcel.Size = New System.Drawing.Size(273, 37)
        Me.cmd_ExportInExcel.TabIndex = 11
        Me.cmd_ExportInExcel.Text = "Export in Excel"
        Me.cmd_ExportInExcel.UseVisualStyleBackColor = False
        '
        'cmd_Close
        '
        Me.cmd_Close.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.cmd_Close.BackColor = System.Drawing.Color.White
        Me.cmd_Close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Close.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Close.Location = New System.Drawing.Point(11, 137)
        Me.cmd_Close.Name = "cmd_Close"
        Me.cmd_Close.Size = New System.Drawing.Size(273, 37)
        Me.cmd_Close.TabIndex = 9
        Me.cmd_Close.Text = "Close"
        Me.cmd_Close.UseVisualStyleBackColor = False
        '
        'cbo_ParticipantId
        '
        Me.cbo_ParticipantId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cbo_ParticipantId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbo_ParticipantId.FormattingEnabled = True
        Me.cbo_ParticipantId.Location = New System.Drawing.Point(107, 60)
        Me.cbo_ParticipantId.Name = "cbo_ParticipantId"
        Me.cbo_ParticipantId.Size = New System.Drawing.Size(178, 24)
        Me.cbo_ParticipantId.TabIndex = 3
        '
        'chkParticipantID
        '
        Me.chkParticipantID.AutoSize = True
        Me.chkParticipantID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkParticipantID.Location = New System.Drawing.Point(12, 62)
        Me.chkParticipantID.Name = "chkParticipantID"
        Me.chkParticipantID.Size = New System.Drawing.Size(88, 18)
        Me.chkParticipantID.TabIndex = 0
        Me.chkParticipantID.Text = "Participant ID"
        Me.chkParticipantID.UseVisualStyleBackColor = True
        '
        'frmWESMSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(319, 210)
        Me.Controls.Add(Me.grpSearch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmWESMSummary"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WESM Bills Uploaded"
        Me.grpSearch.ResumeLayout(False)
        Me.grpSearch.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpSearch As System.Windows.Forms.GroupBox
    Friend WithEvents chkParticipantID As System.Windows.Forms.CheckBox
    Friend WithEvents cbo_ParticipantId As System.Windows.Forms.ComboBox
    Friend WithEvents cmd_Close As System.Windows.Forms.Button
    Friend WithEvents cmd_ExportInExcel As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
End Class
