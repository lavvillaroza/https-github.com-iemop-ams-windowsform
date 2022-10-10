<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdateWESMBillSummary
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ParticipantID_CB = New System.Windows.Forms.ComboBox()
        Me.dgv_UWBSummaryList = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.VATonEnergy_RB = New System.Windows.Forms.RadioButton()
        Me.Energy_RB = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.SearchOption_GroupBox = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.dgv_UWBSummaryList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SearchOption_GroupBox.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ParticipantID:"
        '
        'ParticipantID_CB
        '
        Me.ParticipantID_CB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.ParticipantID_CB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ParticipantID_CB.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ParticipantID_CB.FormattingEnabled = True
        Me.ParticipantID_CB.Location = New System.Drawing.Point(94, 23)
        Me.ParticipantID_CB.Name = "ParticipantID_CB"
        Me.ParticipantID_CB.Size = New System.Drawing.Size(121, 20)
        Me.ParticipantID_CB.TabIndex = 1
        '
        'dgv_UWBSummaryList
        '
        Me.dgv_UWBSummaryList.AllowUserToAddRows = False
        Me.dgv_UWBSummaryList.AllowUserToDeleteRows = False
        Me.dgv_UWBSummaryList.AllowUserToResizeColumns = False
        Me.dgv_UWBSummaryList.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_UWBSummaryList.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_UWBSummaryList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_UWBSummaryList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_UWBSummaryList.Location = New System.Drawing.Point(7, 17)
        Me.dgv_UWBSummaryList.Name = "dgv_UWBSummaryList"
        Me.dgv_UWBSummaryList.ReadOnly = True
        Me.dgv_UWBSummaryList.RowHeadersWidth = 20
        Me.dgv_UWBSummaryList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_UWBSummaryList.Size = New System.Drawing.Size(987, 325)
        Me.dgv_UWBSummaryList.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(242, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "ChargeType:"
        '
        'VATonEnergy_RB
        '
        Me.VATonEnergy_RB.AutoSize = True
        Me.VATonEnergy_RB.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VATonEnergy_RB.Location = New System.Drawing.Point(387, 24)
        Me.VATonEnergy_RB.Name = "VATonEnergy_RB"
        Me.VATonEnergy_RB.Size = New System.Drawing.Size(91, 16)
        Me.VATonEnergy_RB.TabIndex = 1
        Me.VATonEnergy_RB.TabStop = True
        Me.VATonEnergy_RB.Text = "VATonEnergy"
        Me.VATonEnergy_RB.UseVisualStyleBackColor = True
        '
        'Energy_RB
        '
        Me.Energy_RB.AutoSize = True
        Me.Energy_RB.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Energy_RB.Location = New System.Drawing.Point(323, 24)
        Me.Energy_RB.Name = "Energy_RB"
        Me.Energy_RB.Size = New System.Drawing.Size(58, 16)
        Me.Energy_RB.TabIndex = 0
        Me.Energy_RB.TabStop = True
        Me.Energy_RB.Text = "Energy"
        Me.Energy_RB.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.dgv_UWBSummaryList)
        Me.GroupBox1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(8, 76)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1000, 348)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(10, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 14)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "List of Invoice"
        '
        'btn_Cancel
        '
        Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Cancel.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Cancel.ForeColor = System.Drawing.Color.Black
        Me.btn_Cancel.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btn_Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Cancel.Location = New System.Drawing.Point(898, 445)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(110, 39)
        Me.btn_Cancel.TabIndex = 9
        Me.btn_Cancel.Text = "&Close"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'SearchOption_GroupBox
        '
        Me.SearchOption_GroupBox.Controls.Add(Me.ParticipantID_CB)
        Me.SearchOption_GroupBox.Controls.Add(Me.Label2)
        Me.SearchOption_GroupBox.Controls.Add(Me.Label1)
        Me.SearchOption_GroupBox.Controls.Add(Me.Energy_RB)
        Me.SearchOption_GroupBox.Controls.Add(Me.VATonEnergy_RB)
        Me.SearchOption_GroupBox.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchOption_GroupBox.Location = New System.Drawing.Point(8, 12)
        Me.SearchOption_GroupBox.Name = "SearchOption_GroupBox"
        Me.SearchOption_GroupBox.Size = New System.Drawing.Size(720, 59)
        Me.SearchOption_GroupBox.TabIndex = 7
        Me.SearchOption_GroupBox.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(734, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(274, 59)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Notes:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(28, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(214, 12)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Double click to update the selected invoice."
        '
        'frmUpdateWESMBillSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1020, 494)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btn_Cancel)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.SearchOption_GroupBox)
        Me.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmUpdateWESMBillSummary"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WESM Bill Summary Adjustment Per Participant"
        CType(Me.dgv_UWBSummaryList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.SearchOption_GroupBox.ResumeLayout(False)
        Me.SearchOption_GroupBox.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ParticipantID_CB As System.Windows.Forms.ComboBox
    Friend WithEvents dgv_UWBSummaryList As System.Windows.Forms.DataGridView
    Friend WithEvents VATonEnergy_RB As System.Windows.Forms.RadioButton
    Friend WithEvents Energy_RB As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents SearchOption_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
