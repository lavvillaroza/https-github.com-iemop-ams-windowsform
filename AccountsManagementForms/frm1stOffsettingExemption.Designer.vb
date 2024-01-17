<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParticipantPCMapping
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.dgv_1stOffsettingEx = New System.Windows.Forms.DataGridView()
        Me.col_ParentID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_ParentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_ChildID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_ChildName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_ChargeType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_UpdatedBy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_UpdatedDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_View = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btn_Close = New System.Windows.Forms.Button()
        Me.btn_Add = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.dgv_1stOffsettingEx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.dgv_1stOffsettingEx, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(970, 329)
        Me.TableLayoutPanel1.TabIndex = 10
        '
        'dgv_1stOffsettingEx
        '
        Me.dgv_1stOffsettingEx.AllowUserToAddRows = False
        Me.dgv_1stOffsettingEx.AllowUserToDeleteRows = False
        Me.dgv_1stOffsettingEx.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_1stOffsettingEx.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_1stOffsettingEx.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_1stOffsettingEx.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_1stOffsettingEx.ColumnHeadersHeight = 30
        Me.dgv_1stOffsettingEx.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgv_1stOffsettingEx.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_ParentID, Me.col_ParentName, Me.col_ChildID, Me.col_ChildName, Me.col_ChargeType, Me.col_Status, Me.col_UpdatedBy, Me.col_UpdatedDate, Me.col_View})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LemonChiffon
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_1stOffsettingEx.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_1stOffsettingEx.Location = New System.Drawing.Point(3, 3)
        Me.dgv_1stOffsettingEx.Name = "dgv_1stOffsettingEx"
        Me.dgv_1stOffsettingEx.ReadOnly = True
        Me.dgv_1stOffsettingEx.RowHeadersVisible = False
        Me.dgv_1stOffsettingEx.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_1stOffsettingEx.Size = New System.Drawing.Size(964, 273)
        Me.dgv_1stOffsettingEx.TabIndex = 8
        '
        'col_ParentID
        '
        Me.col_ParentID.HeaderText = "Parent ID"
        Me.col_ParentID.Name = "col_ParentID"
        Me.col_ParentID.ReadOnly = True
        '
        'col_ParentName
        '
        Me.col_ParentName.HeaderText = "Parent Name"
        Me.col_ParentName.Name = "col_ParentName"
        Me.col_ParentName.ReadOnly = True
        '
        'col_ChildID
        '
        Me.col_ChildID.HeaderText = "Child ID"
        Me.col_ChildID.Name = "col_ChildID"
        Me.col_ChildID.ReadOnly = True
        '
        'col_ChildName
        '
        Me.col_ChildName.HeaderText = "Child Name"
        Me.col_ChildName.Name = "col_ChildName"
        Me.col_ChildName.ReadOnly = True
        '
        'col_ChargeType
        '
        Me.col_ChargeType.HeaderText = "Charge Type"
        Me.col_ChargeType.Name = "col_ChargeType"
        Me.col_ChargeType.ReadOnly = True
        '
        'col_Status
        '
        Me.col_Status.HeaderText = "Status"
        Me.col_Status.Name = "col_Status"
        Me.col_Status.ReadOnly = True
        '
        'col_UpdatedBy
        '
        Me.col_UpdatedBy.HeaderText = "Updated By"
        Me.col_UpdatedBy.Name = "col_UpdatedBy"
        Me.col_UpdatedBy.ReadOnly = True
        '
        'col_UpdatedDate
        '
        Me.col_UpdatedDate.HeaderText = "Updated Date"
        Me.col_UpdatedDate.Name = "col_UpdatedDate"
        Me.col_UpdatedDate.ReadOnly = True
        Me.col_UpdatedDate.Width = 140
        '
        'col_View
        '
        Me.col_View.HeaderText = "Action"
        Me.col_View.Name = "col_View"
        Me.col_View.ReadOnly = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.btn_Close)
        Me.Panel1.Controls.Add(Me.btn_Add)
        Me.Panel1.Location = New System.Drawing.Point(3, 282)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(964, 44)
        Me.Panel1.TabIndex = 9
        '
        'btn_Close
        '
        Me.btn_Close.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btn_Close.BackColor = System.Drawing.Color.White
        Me.btn_Close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Close.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Close.ForeColor = System.Drawing.Color.Black
        Me.btn_Close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btn_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Close.Location = New System.Drawing.Point(837, 2)
        Me.btn_Close.Name = "btn_Close"
        Me.btn_Close.Size = New System.Drawing.Size(124, 37)
        Me.btn_Close.TabIndex = 9
        Me.btn_Close.Text = "&Close"
        Me.btn_Close.UseVisualStyleBackColor = False
        '
        'btn_Add
        '
        Me.btn_Add.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btn_Add.BackColor = System.Drawing.Color.White
        Me.btn_Add.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btn_Add.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btn_Add.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Add.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Add.ForeColor = System.Drawing.Color.Black
        Me.btn_Add.Image = Global.AccountsManagementForms.My.Resources.Resources.NewGreenIcon22x22
        Me.btn_Add.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Add.Location = New System.Drawing.Point(707, 2)
        Me.btn_Add.Name = "btn_Add"
        Me.btn_Add.Size = New System.Drawing.Size(124, 37)
        Me.btn_Add.TabIndex = 7
        Me.btn_Add.Text = "&Exemption"
        Me.btn_Add.UseVisualStyleBackColor = False
        '
        'frmParticipantPCMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(973, 332)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParticipantPCMapping"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Parent & Child 1st Offsetting Exemption Mapping"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.dgv_1stOffsettingEx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents dgv_1stOffsettingEx As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btn_Close As System.Windows.Forms.Button
    Friend WithEvents btn_Add As System.Windows.Forms.Button
    Friend WithEvents col_ParentID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ParentName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ChildID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ChildName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ChargeType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_UpdatedBy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_UpdatedDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_View As System.Windows.Forms.DataGridViewLinkColumn
End Class
