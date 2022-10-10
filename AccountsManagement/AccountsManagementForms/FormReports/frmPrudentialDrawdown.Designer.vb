<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrudentialDrawdown
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
        Me.DGridView = New System.Windows.Forms.DataGridView()
        Me.colTransDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFullName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDrawnAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPrudentail = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        CType(Me.DGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGridView
        '
        Me.DGridView.AllowUserToAddRows = False
        Me.DGridView.AllowUserToDeleteRows = False
        Me.DGridView.AllowUserToOrderColumns = True
        Me.DGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colTransDate, Me.colDueDate, Me.colIDNumber, Me.colParticipantID, Me.colFullName, Me.colDrawnAmount, Me.colPrudentail})
        Me.DGridView.Location = New System.Drawing.Point(12, 12)
        Me.DGridView.Name = "DGridView"
        Me.DGridView.Size = New System.Drawing.Size(781, 388)
        Me.DGridView.TabIndex = 0
        '
        'colTransDate
        '
        Me.colTransDate.HeaderText = "TransDate"
        Me.colTransDate.Name = "colTransDate"
        Me.colTransDate.ReadOnly = True
        '
        'colDueDate
        '
        Me.colDueDate.HeaderText = "DueDate"
        Me.colDueDate.Name = "colDueDate"
        Me.colDueDate.ReadOnly = True
        '
        'colIDNumber
        '
        Me.colIDNumber.HeaderText = "IDNumber"
        Me.colIDNumber.Name = "colIDNumber"
        Me.colIDNumber.ReadOnly = True
        '
        'colParticipantID
        '
        Me.colParticipantID.HeaderText = "ParticipantID"
        Me.colParticipantID.Name = "colParticipantID"
        Me.colParticipantID.ReadOnly = True
        '
        'colFullName
        '
        Me.colFullName.HeaderText = "Name"
        Me.colFullName.Name = "colFullName"
        Me.colFullName.ReadOnly = True
        '
        'colDrawnAmount
        '
        Me.colDrawnAmount.HeaderText = "Drawdown"
        Me.colDrawnAmount.Name = "colDrawnAmount"
        Me.colDrawnAmount.ReadOnly = True
        '
        'colPrudentail
        '
        Me.colPrudentail.HeaderText = "RemainingPrudential"
        Me.colPrudentail.Name = "colPrudentail"
        Me.colPrudentail.ReadOnly = True
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.ForeColor = System.Drawing.Color.Black
        Me.btnSearch.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(517, 406)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(135, 39)
        Me.btnSearch.TabIndex = 8
        Me.btnSearch.Text = "&Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnGenerate
        '
        Me.btnGenerate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenerate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnGenerate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnGenerate.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerate.ForeColor = System.Drawing.Color.Black
        Me.btnGenerate.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.btnGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerate.Location = New System.Drawing.Point(376, 406)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(135, 39)
        Me.btnGenerate.TabIndex = 6
        Me.btnGenerate.Text = "&Generate"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnPrint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnPrint.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.ForeColor = System.Drawing.Color.Black
        Me.btnPrint.Image = Global.AccountsManagementForms.My.Resources.Resources.PrintColoredIcon22x22
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(658, 406)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(135, 39)
        Me.btnPrint.TabIndex = 7
        Me.btnPrint.Text = "&Print"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'frmPrudentialDrawdown
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(805, 470)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnGenerate)
        Me.Controls.Add(Me.DGridView)
        Me.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmPrudentialDrawdown"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Notice of Drawdown"
        CType(Me.DGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGridView As System.Windows.Forms.DataGridView
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents colTransDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDueDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFullName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDrawnAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPrudentail As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
