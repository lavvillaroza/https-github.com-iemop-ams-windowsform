<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMarketFeesSummary
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.DGridView = New System.Windows.Forms.DataGridView()
        Me.colIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBillNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMarketFees = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colVATonMF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colWVatMF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colWTaxOnMF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDIOnMF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDIVATOnMF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDIOnWithhold = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTotalDI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colGrandTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTransType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbTransaction = New System.Windows.Forms.RadioButton()
        Me.rbAllocation = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.DGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnLoad
        '
        Me.btnLoad.BackColor = System.Drawing.Color.White
        Me.btnLoad.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnLoad.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnLoad.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoad.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.ForeColor = System.Drawing.Color.Black
        Me.btnLoad.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.btnLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLoad.Location = New System.Drawing.Point(685, 24)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(95, 39)
        Me.btnLoad.TabIndex = 12
        Me.btnLoad.Text = "     Search"
        Me.btnLoad.UseVisualStyleBackColor = False
        '
        'DGridView
        '
        Me.DGridView.AllowUserToAddRows = False
        Me.DGridView.AllowUserToDeleteRows = False
        Me.DGridView.AllowUserToResizeColumns = False
        Me.DGridView.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Transparent
        Me.DGridView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colIDNumber, Me.colParticipantID, Me.colBillNo, Me.colMarketFees, Me.colVATonMF, Me.colWVatMF, Me.colWTaxOnMF, Me.colTotal, Me.colDIOnMF, Me.colDIVATOnMF, Me.colDIOnWithhold, Me.colTotalDI, Me.colGrandTotal, Me.colTransType})
        Me.DGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGridView.Location = New System.Drawing.Point(12, 71)
        Me.DGridView.Name = "DGridView"
        Me.DGridView.Size = New System.Drawing.Size(1012, 356)
        Me.DGridView.TabIndex = 13
        '
        'colIDNumber
        '
        Me.colIDNumber.HeaderText = "IDNumber"
        Me.colIDNumber.Name = "colIDNumber"
        Me.colIDNumber.ReadOnly = True
        Me.colIDNumber.Visible = False
        '
        'colParticipantID
        '
        Me.colParticipantID.HeaderText = "ParticipantID"
        Me.colParticipantID.Name = "colParticipantID"
        Me.colParticipantID.ReadOnly = True
        '
        'colBillNo
        '
        Me.colBillNo.HeaderText = "BillNo."
        Me.colBillNo.Name = "colBillNo"
        Me.colBillNo.ReadOnly = True
        '
        'colMarketFees
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.colMarketFees.DefaultCellStyle = DataGridViewCellStyle2
        Me.colMarketFees.HeaderText = "MarketFees"
        Me.colMarketFees.Name = "colMarketFees"
        Me.colMarketFees.ReadOnly = True
        '
        'colVATonMF
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.colVATonMF.DefaultCellStyle = DataGridViewCellStyle3
        Me.colVATonMF.HeaderText = "VATonMF"
        Me.colVATonMF.Name = "colVATonMF"
        Me.colVATonMF.ReadOnly = True
        '
        'colWVatMF
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.colWVatMF.DefaultCellStyle = DataGridViewCellStyle4
        Me.colWVatMF.HeaderText = "WitholdingVATOnMF"
        Me.colWVatMF.Name = "colWVatMF"
        Me.colWVatMF.ReadOnly = True
        Me.colWVatMF.Width = 120
        '
        'colWTaxOnMF
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.colWTaxOnMF.DefaultCellStyle = DataGridViewCellStyle5
        Me.colWTaxOnMF.HeaderText = "WitholdingTaxOnMF"
        Me.colWTaxOnMF.Name = "colWTaxOnMF"
        Me.colWTaxOnMF.ReadOnly = True
        Me.colWTaxOnMF.Width = 120
        '
        'colTotal
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.colTotal.DefaultCellStyle = DataGridViewCellStyle6
        Me.colTotal.HeaderText = "Total"
        Me.colTotal.Name = "colTotal"
        Me.colTotal.ReadOnly = True
        '
        'colDIOnMF
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N2"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.colDIOnMF.DefaultCellStyle = DataGridViewCellStyle7
        Me.colDIOnMF.HeaderText = "DefaultInterestOnMF"
        Me.colDIOnMF.Name = "colDIOnMF"
        Me.colDIOnMF.ReadOnly = True
        Me.colDIOnMF.Width = 120
        '
        'colDIVATOnMF
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N2"
        DataGridViewCellStyle8.NullValue = Nothing
        Me.colDIVATOnMF.DefaultCellStyle = DataGridViewCellStyle8
        Me.colDIVATOnMF.HeaderText = "DefaultInterestOnVATonMF"
        Me.colDIVATOnMF.Name = "colDIVATOnMF"
        Me.colDIVATOnMF.ReadOnly = True
        Me.colDIVATOnMF.Width = 150
        '
        'colDIOnWithhold
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N2"
        DataGridViewCellStyle9.NullValue = Nothing
        Me.colDIOnWithhold.DefaultCellStyle = DataGridViewCellStyle9
        Me.colDIOnWithhold.HeaderText = "DefaultInterestOnWithhold"
        Me.colDIOnWithhold.Name = "colDIOnWithhold"
        Me.colDIOnWithhold.ReadOnly = True
        Me.colDIOnWithhold.Width = 150
        '
        'colTotalDI
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "N2"
        DataGridViewCellStyle10.NullValue = Nothing
        Me.colTotalDI.DefaultCellStyle = DataGridViewCellStyle10
        Me.colTotalDI.HeaderText = "TotalDefaultInterest"
        Me.colTotalDI.Name = "colTotalDI"
        Me.colTotalDI.ReadOnly = True
        Me.colTotalDI.Width = 130
        '
        'colGrandTotal
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Format = "N2"
        DataGridViewCellStyle11.NullValue = Nothing
        Me.colGrandTotal.DefaultCellStyle = DataGridViewCellStyle11
        Me.colGrandTotal.HeaderText = "GrandTotal"
        Me.colGrandTotal.Name = "colGrandTotal"
        Me.colGrandTotal.ReadOnly = True
        '
        'colTransType
        '
        Me.colTransType.HeaderText = "TransType"
        Me.colTransType.Name = "colTransType"
        Me.colTransType.Visible = False
        '
        'btnGenerate
        '
        Me.btnGenerate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenerate.BackColor = System.Drawing.Color.White
        Me.btnGenerate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnGenerate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnGenerate.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerate.ForeColor = System.Drawing.Color.Black
        Me.btnGenerate.Image = Global.AccountsManagementForms.My.Resources.Resources.ExportDocumentsColored22x22
        Me.btnGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerate.Location = New System.Drawing.Point(728, 433)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(145, 39)
        Me.btnGenerate.TabIndex = 15
        Me.btnGenerate.Text = "Generate Report"
        Me.btnGenerate.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.White
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(879, 433)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(145, 39)
        Me.btnClose.TabIndex = 14
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbTransaction)
        Me.GroupBox1.Controls.Add(Me.rbAllocation)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(277, 53)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        '
        'rbTransaction
        '
        Me.rbTransaction.AutoSize = True
        Me.rbTransaction.Location = New System.Drawing.Point(129, 19)
        Me.rbTransaction.Name = "rbTransaction"
        Me.rbTransaction.Size = New System.Drawing.Size(107, 18)
        Me.rbTransaction.TabIndex = 1
        Me.rbTransaction.TabStop = True
        Me.rbTransaction.Text = "Transaction Date"
        Me.rbTransaction.UseVisualStyleBackColor = True
        '
        'rbAllocation
        '
        Me.rbAllocation.AutoSize = True
        Me.rbAllocation.Location = New System.Drawing.Point(18, 19)
        Me.rbAllocation.Name = "rbAllocation"
        Me.rbAllocation.Size = New System.Drawing.Size(97, 18)
        Me.rbAllocation.TabIndex = 0
        Me.rbAllocation.TabStop = True
        Me.rbAllocation.Text = "&Allocation Date"
        Me.rbAllocation.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dtTo)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.dtFrom)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(295, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(384, 53)
        Me.GroupBox2.TabIndex = 17
        Me.GroupBox2.TabStop = False
        '
        'dtTo
        '
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTo.Location = New System.Drawing.Point(269, 19)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(109, 20)
        Me.dtTo.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(207, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Date To:"
        '
        'dtFrom
        '
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFrom.Location = New System.Drawing.Point(78, 18)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(109, 20)
        Me.dtFrom.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Date From:"
        '
        'frmMarketFeesSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1036, 484)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnGenerate)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.DGridView)
        Me.Controls.Add(Me.btnLoad)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmMarketFeesSummary"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Market Fees Summary"
        CType(Me.DGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents DGridView As System.Windows.Forms.DataGridView
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbTransaction As System.Windows.Forms.RadioButton
    Friend WithEvents rbAllocation As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents colIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBillNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMarketFees As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colVATonMF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colWVatMF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colWTaxOnMF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDIOnMF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDIVATOnMF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDIOnWithhold As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTotalDI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colGrandTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTransType As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
