<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSummaryOfMarginCall
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
        Me.colIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFullName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMNE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTradingLimit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMinimumRequirement = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ddlTransactionDate = New System.Windows.Forms.ComboBox()
        Me.btnSearch = New System.Windows.Forms.Button()
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
        Me.DGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colIDNumber, Me.colParticipantID, Me.colFullName, Me.colMNE, Me.colTradingLimit, Me.colMinimumRequirement})
        Me.DGridView.Location = New System.Drawing.Point(12, 43)
        Me.DGridView.MultiSelect = False
        Me.DGridView.Name = "DGridView"
        Me.DGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGridView.Size = New System.Drawing.Size(716, 391)
        Me.DGridView.TabIndex = 0
        '
        'colIDNumber
        '
        Me.colIDNumber.HeaderText = "IDNumber"
        Me.colIDNumber.Name = "colIDNumber"
        Me.colIDNumber.Visible = False
        '
        'colParticipantID
        '
        Me.colParticipantID.HeaderText = "ParticipantID"
        Me.colParticipantID.Name = "colParticipantID"
        Me.colParticipantID.ReadOnly = True
        '
        'colFullName
        '
        Me.colFullName.HeaderText = "FullName"
        Me.colFullName.Name = "colFullName"
        '
        'colMNE
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.colMNE.DefaultCellStyle = DataGridViewCellStyle2
        Me.colMNE.HeaderText = "MNE"
        Me.colMNE.Name = "colMNE"
        Me.colMNE.ReadOnly = True
        Me.colMNE.Width = 120
        '
        'colTradingLimit
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.colTradingLimit.DefaultCellStyle = DataGridViewCellStyle3
        Me.colTradingLimit.HeaderText = "TradingLimit"
        Me.colTradingLimit.Name = "colTradingLimit"
        Me.colTradingLimit.ReadOnly = True
        Me.colTradingLimit.Width = 120
        '
        'colMinimumRequirement
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.colMinimumRequirement.DefaultCellStyle = DataGridViewCellStyle4
        Me.colMinimumRequirement.HeaderText = "MinimumRequirement"
        Me.colMinimumRequirement.Name = "colMinimumRequirement"
        Me.colMinimumRequirement.ReadOnly = True
        Me.colMinimumRequirement.Width = 120
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnPrint.FlatAppearance.CheckedBackColor = System.Drawing.Color.LemonChiffon
        Me.btnPrint.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.ForeColor = System.Drawing.Color.Black
        Me.btnPrint.Image = Global.AccountsManagementForms.My.Resources.Resources.PrintColoredIcon22x22
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(609, 440)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(119, 29)
        Me.btnPrint.TabIndex = 1
        Me.btnPrint.Text = "&Print"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Transaction Date:"
        '
        'ddlTransactionDate
        '
        Me.ddlTransactionDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlTransactionDate.FormattingEnabled = True
        Me.ddlTransactionDate.Location = New System.Drawing.Point(119, 10)
        Me.ddlTransactionDate.Name = "ddlTransactionDate"
        Me.ddlTransactionDate.Size = New System.Drawing.Size(163, 20)
        Me.ddlTransactionDate.TabIndex = 3
        '
        'btnSearch
        '
        Me.btnSearch.BackgroundImage = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSearch.FlatAppearance.CheckedBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Location = New System.Drawing.Point(288, 7)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(35, 30)
        Me.btnSearch.TabIndex = 8
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'frmSummaryOfMarginCall
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(740, 481)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.ddlTransactionDate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.DGridView)
        Me.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmSummaryOfMarginCall"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Prudential Margin Call"
        CType(Me.DGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DGridView As System.Windows.Forms.DataGridView
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ddlTransactionDate As System.Windows.Forms.ComboBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents colIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFullName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMNE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTradingLimit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMinimumRequirement As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
