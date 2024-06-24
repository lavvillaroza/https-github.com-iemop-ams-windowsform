<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWTAInstallmentTermsView
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.gbMenu2 = New System.Windows.Forms.GroupBox()
        Me.DGridViewTerms = New System.Windows.Forms.DataGridView()
        Me.colTermNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNewDueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTermAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPaidAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDefaultAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTermStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gbMenu2.SuspendLayout()
        CType(Me.DGridViewTerms, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gbMenu2
        '
        Me.gbMenu2.Controls.Add(Me.DGridViewTerms)
        Me.gbMenu2.Location = New System.Drawing.Point(7, 5)
        Me.gbMenu2.Name = "gbMenu2"
        Me.gbMenu2.Size = New System.Drawing.Size(623, 285)
        Me.gbMenu2.TabIndex = 12
        Me.gbMenu2.TabStop = False
        '
        'DGridViewTerms
        '
        Me.DGridViewTerms.AllowUserToAddRows = False
        Me.DGridViewTerms.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridViewTerms.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGridViewTerms.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGridViewTerms.BackgroundColor = System.Drawing.SystemColors.ButtonShadow
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.DimGray
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGridViewTerms.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGridViewTerms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridViewTerms.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colTermNo, Me.colDueDate, Me.colNewDueDate, Me.colTermAmount, Me.colPaidAmount, Me.colDefaultAmount, Me.colTermStatus})
        Me.DGridViewTerms.Location = New System.Drawing.Point(8, 16)
        Me.DGridViewTerms.Name = "DGridViewTerms"
        Me.DGridViewTerms.ReadOnly = True
        Me.DGridViewTerms.RowHeadersVisible = False
        Me.DGridViewTerms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DGridViewTerms.Size = New System.Drawing.Size(608, 260)
        Me.DGridViewTerms.TabIndex = 0
        '
        'colTermNo
        '
        Me.colTermNo.HeaderText = "Term"
        Me.colTermNo.Name = "colTermNo"
        Me.colTermNo.ReadOnly = True
        Me.colTermNo.Width = 60
        '
        'colDueDate
        '
        Me.colDueDate.HeaderText = "Due Date"
        Me.colDueDate.Name = "colDueDate"
        Me.colDueDate.ReadOnly = True
        Me.colDueDate.Width = 80
        '
        'colNewDueDate
        '
        Me.colNewDueDate.HeaderText = "New Due Date"
        Me.colNewDueDate.Name = "colNewDueDate"
        Me.colNewDueDate.ReadOnly = True
        Me.colNewDueDate.Width = 80
        '
        'colTermAmount
        '
        Me.colTermAmount.HeaderText = "Term Amount"
        Me.colTermAmount.Name = "colTermAmount"
        Me.colTermAmount.ReadOnly = True
        '
        'colPaidAmount
        '
        Me.colPaidAmount.HeaderText = "Paid Amount"
        Me.colPaidAmount.Name = "colPaidAmount"
        Me.colPaidAmount.ReadOnly = True
        '
        'colDefaultAmount
        '
        Me.colDefaultAmount.HeaderText = "Default Amount"
        Me.colDefaultAmount.Name = "colDefaultAmount"
        Me.colDefaultAmount.ReadOnly = True
        '
        'colTermStatus
        '
        Me.colTermStatus.HeaderText = "Term Status"
        Me.colTermStatus.Name = "colTermStatus"
        Me.colTermStatus.ReadOnly = True
        Me.colTermStatus.Width = 80
        '
        'frmWTAInstallmentTermsView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(637, 296)
        Me.Controls.Add(Me.gbMenu2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmWTAInstallmentTermsView"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Payment Terms"
        Me.gbMenu2.ResumeLayout(False)
        CType(Me.DGridViewTerms, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents gbMenu2 As GroupBox
    Friend WithEvents DGridViewTerms As DataGridView
    Friend WithEvents colTermNo As DataGridViewTextBoxColumn
    Friend WithEvents colDueDate As DataGridViewTextBoxColumn
    Friend WithEvents colNewDueDate As DataGridViewTextBoxColumn
    Friend WithEvents colTermAmount As DataGridViewTextBoxColumn
    Friend WithEvents colPaidAmount As DataGridViewTextBoxColumn
    Friend WithEvents colDefaultAmount As DataGridViewTextBoxColumn
    Friend WithEvents colTermStatus As DataGridViewTextBoxColumn
End Class
