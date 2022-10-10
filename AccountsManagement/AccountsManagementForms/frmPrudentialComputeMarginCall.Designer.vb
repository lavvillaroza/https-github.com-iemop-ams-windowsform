<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrudentialComputeMarginCall
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DGridView = New System.Windows.Forms.DataGridView()
        Me.colTransDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMNE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTradingLimit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMinimumRequirement = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnMarginCall = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.DGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGridView
        '
        Me.DGridView.AllowUserToAddRows = False
        Me.DGridView.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGridView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colTransDate, Me.colDueDate, Me.colParticipantID, Me.colMNE, Me.colTradingLimit, Me.colMinimumRequirement, Me.colStatus})
        Me.DGridView.Location = New System.Drawing.Point(12, 12)
        Me.DGridView.MultiSelect = False
        Me.DGridView.Name = "DGridView"
        Me.DGridView.RowHeadersWidth = 20
        Me.DGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGridView.Size = New System.Drawing.Size(823, 580)
        Me.DGridView.TabIndex = 1
        '
        'colTransDate
        '
        Me.colTransDate.HeaderText = "TransactionDate"
        Me.colTransDate.Name = "colTransDate"
        Me.colTransDate.ReadOnly = True
        '
        'colDueDate
        '
        Me.colDueDate.HeaderText = "DueDate"
        Me.colDueDate.Name = "colDueDate"
        Me.colDueDate.ReadOnly = True
        '
        'colParticipantID
        '
        Me.colParticipantID.HeaderText = "ParticipantID"
        Me.colParticipantID.Name = "colParticipantID"
        Me.colParticipantID.ReadOnly = True
        '
        'colMNE
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colMNE.DefaultCellStyle = DataGridViewCellStyle2
        Me.colMNE.HeaderText = "MNE"
        Me.colMNE.Name = "colMNE"
        Me.colMNE.ReadOnly = True
        Me.colMNE.Width = 120
        '
        'colTradingLimit
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colTradingLimit.DefaultCellStyle = DataGridViewCellStyle3
        Me.colTradingLimit.HeaderText = "TradingLimit"
        Me.colTradingLimit.Name = "colTradingLimit"
        Me.colTradingLimit.ReadOnly = True
        Me.colTradingLimit.Width = 120
        '
        'colMinimumRequirement
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colMinimumRequirement.DefaultCellStyle = DataGridViewCellStyle4
        Me.colMinimumRequirement.HeaderText = "MinimumRequirement"
        Me.colMinimumRequirement.Name = "colMinimumRequirement"
        Me.colMinimumRequirement.ReadOnly = True
        Me.colMinimumRequirement.Width = 120
        '
        'colStatus
        '
        Me.colStatus.HeaderText = "Status"
        Me.colStatus.Name = "colStatus"
        Me.colStatus.ReadOnly = True
        '
        'btnMarginCall
        '
        Me.btnMarginCall.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMarginCall.BackColor = System.Drawing.Color.White
        Me.btnMarginCall.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnMarginCall.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnMarginCall.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnMarginCall.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMarginCall.ForeColor = System.Drawing.Color.Black
        Me.btnMarginCall.Image = Global.AccountsManagementForms.My.Resources.Resources.ProcessDataIcon22x22
        Me.btnMarginCall.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMarginCall.Location = New System.Drawing.Point(261, 602)
        Me.btnMarginCall.Name = "btnMarginCall"
        Me.btnMarginCall.Size = New System.Drawing.Size(139, 39)
        Me.btnMarginCall.TabIndex = 2
        Me.btnMarginCall.Text = "&Generate"
        Me.btnMarginCall.UseVisualStyleBackColor = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.BackColor = System.Drawing.Color.White
        Me.btnPrint.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnPrint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnPrint.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.ForeColor = System.Drawing.Color.Black
        Me.btnPrint.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(551, 602)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(139, 39)
        Me.btnPrint.TabIndex = 4
        Me.btnPrint.Text = "&View"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.BackColor = System.Drawing.Color.White
        Me.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.ForeColor = System.Drawing.Color.Black
        Me.btnSearch.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(406, 602)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(139, 39)
        Me.btnSearch.TabIndex = 5
        Me.btnSearch.Text = "&Search"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(696, 602)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(139, 39)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "&Close"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'frmPrudentialComputeMarginCall
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(847, 658)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnMarginCall)
        Me.Controls.Add(Me.DGridView)
        Me.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmPrudentialComputeMarginCall"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Compute Margin Call"
        CType(Me.DGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGridView As System.Windows.Forms.DataGridView
    Friend WithEvents btnMarginCall As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents colTransDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDueDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMNE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTradingLimit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMinimumRequirement As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStatus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
