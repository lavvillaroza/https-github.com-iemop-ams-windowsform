<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEFT
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
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.cbo_AllocationDate = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgv_dataView = New System.Windows.Forms.DataGridView()
        Me.AllocDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParticipantID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PayType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheckNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExcessCollection = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DeferredEnergy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DeferredVATonEnergy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OffsetDeferredEnergy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OffsetDeferredVATonEnergy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Energy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VAT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TransferToPrudential = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TransferToFinPen = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InterestNSS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IntSTL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalPayment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmd_ExportToCSV = New System.Windows.Forms.Button()
        Me.cmd_Search = New System.Windows.Forms.Button()
        Me.cmd_refresh = New System.Windows.Forms.Button()
        Me.cmd_close = New System.Windows.Forms.Button()
        CType(Me.dgv_dataView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cbo_AllocationDate
        '
        Me.cbo_AllocationDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_AllocationDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_AllocationDate.FormattingEnabled = True
        Me.cbo_AllocationDate.Location = New System.Drawing.Point(113, 18)
        Me.cbo_AllocationDate.Name = "cbo_AllocationDate"
        Me.cbo_AllocationDate.Size = New System.Drawing.Size(121, 23)
        Me.cbo_AllocationDate.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Remittance Date:"
        '
        'dgv_dataView
        '
        Me.dgv_dataView.AllowUserToAddRows = False
        Me.dgv_dataView.AllowUserToDeleteRows = False
        DataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_dataView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle12
        Me.dgv_dataView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_dataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_dataView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle13
        Me.dgv_dataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_dataView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.AllocDate, Me.IDNumber, Me.ParticipantID, Me.PayType, Me.CheckNo, Me.ExcessCollection, Me.DeferredEnergy, Me.DeferredVATonEnergy, Me.OffsetDeferredEnergy, Me.OffsetDeferredVATonEnergy, Me.Energy, Me.VAT, Me.MF, Me.TransferToPrudential, Me.TransferToFinPen, Me.InterestNSS, Me.IntSTL, Me.TotalPayment})
        DataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_dataView.DefaultCellStyle = DataGridViewCellStyle21
        Me.dgv_dataView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv_dataView.Location = New System.Drawing.Point(12, 68)
        Me.dgv_dataView.Name = "dgv_dataView"
        DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_dataView.RowHeadersDefaultCellStyle = DataGridViewCellStyle22
        Me.dgv_dataView.RowHeadersWidth = 20
        Me.dgv_dataView.RowTemplate.Height = 24
        Me.dgv_dataView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_dataView.Size = New System.Drawing.Size(1012, 407)
        Me.dgv_dataView.TabIndex = 2
        '
        'AllocDate
        '
        DataGridViewCellStyle14.Format = "d"
        DataGridViewCellStyle14.NullValue = Nothing
        Me.AllocDate.DefaultCellStyle = DataGridViewCellStyle14
        Me.AllocDate.HeaderText = "AllocationDate"
        Me.AllocDate.Name = "AllocDate"
        Me.AllocDate.Width = 101
        '
        'IDNumber
        '
        Me.IDNumber.HeaderText = "IDNumber"
        Me.IDNumber.Name = "IDNumber"
        Me.IDNumber.Width = 78
        '
        'ParticipantID
        '
        Me.ParticipantID.HeaderText = "ParticipantId"
        Me.ParticipantID.Name = "ParticipantID"
        Me.ParticipantID.Width = 90
        '
        'PayType
        '
        Me.PayType.HeaderText = "PaymentType"
        Me.PayType.Name = "PayType"
        Me.PayType.Width = 96
        '
        'CheckNo
        '
        Me.CheckNo.HeaderText = "CheckNumber"
        Me.CheckNo.Name = "CheckNo"
        Me.CheckNo.Width = 99
        '
        'ExcessCollection
        '
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle15.Format = "n2"
        Me.ExcessCollection.DefaultCellStyle = DataGridViewCellStyle15
        Me.ExcessCollection.HeaderText = "ExcessCollection"
        Me.ExcessCollection.Name = "ExcessCollection"
        Me.ExcessCollection.Width = 114
        '
        'DeferredEnergy
        '
        Me.DeferredEnergy.HeaderText = "DeferredEnergy"
        Me.DeferredEnergy.Name = "DeferredEnergy"
        Me.DeferredEnergy.Width = 109
        '
        'DeferredVATonEnergy
        '
        Me.DeferredVATonEnergy.HeaderText = "DeferredVATonEnergy"
        Me.DeferredVATonEnergy.Name = "DeferredVATonEnergy"
        Me.DeferredVATonEnergy.Width = 140
        '
        'OffsetDeferredEnergy
        '
        Me.OffsetDeferredEnergy.HeaderText = "OffsetDeferredEnergy"
        Me.OffsetDeferredEnergy.Name = "OffsetDeferredEnergy"
        Me.OffsetDeferredEnergy.Width = 140
        '
        'OffsetDeferredVATonEnergy
        '
        Me.OffsetDeferredVATonEnergy.HeaderText = "OffsetDeferredVATonEnergy"
        Me.OffsetDeferredVATonEnergy.Name = "OffsetDeferredVATonEnergy"
        Me.OffsetDeferredVATonEnergy.Width = 171
        '
        'Energy
        '
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle16.Format = "n2"
        Me.Energy.DefaultCellStyle = DataGridViewCellStyle16
        Me.Energy.HeaderText = "Energy"
        Me.Energy.Name = "Energy"
        Me.Energy.Width = 66
        '
        'VAT
        '
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle17.Format = "n2"
        Me.VAT.DefaultCellStyle = DataGridViewCellStyle17
        Me.VAT.HeaderText = "VAT"
        Me.VAT.Name = "VAT"
        Me.VAT.Width = 52
        '
        'MF
        '
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle18.Format = "n2"
        Me.MF.DefaultCellStyle = DataGridViewCellStyle18
        Me.MF.HeaderText = "MarketFees"
        Me.MF.Name = "MF"
        Me.MF.Width = 88
        '
        'TransferToPrudential
        '
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle19.Format = "n2"
        Me.TransferToPrudential.DefaultCellStyle = DataGridViewCellStyle19
        Me.TransferToPrudential.HeaderText = "TransferToPrudential"
        Me.TransferToPrudential.Name = "TransferToPrudential"
        Me.TransferToPrudential.Width = 132
        '
        'TransferToFinPen
        '
        Me.TransferToFinPen.HeaderText = "TransferToFinPen"
        Me.TransferToFinPen.Name = "TransferToFinPen"
        Me.TransferToFinPen.Width = 117
        '
        'InterestNSS
        '
        Me.InterestNSS.HeaderText = "Interest NSS"
        Me.InterestNSS.Name = "InterestNSS"
        Me.InterestNSS.Width = 92
        '
        'IntSTL
        '
        Me.IntSTL.HeaderText = "Interest STL"
        Me.IntSTL.Name = "IntSTL"
        Me.IntSTL.Width = 90
        '
        'TotalPayment
        '
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle20.Format = "n2"
        Me.TotalPayment.DefaultCellStyle = DataGridViewCellStyle20
        Me.TotalPayment.HeaderText = "TotalPayment"
        Me.TotalPayment.Name = "TotalPayment"
        Me.TotalPayment.Width = 95
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cmd_ExportToCSV)
        Me.GroupBox1.Controls.Add(Me.cbo_AllocationDate)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmd_Search)
        Me.GroupBox1.Controls.Add(Me.cmd_refresh)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1012, 60)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'cmd_ExportToCSV
        '
        Me.cmd_ExportToCSV.BackColor = System.Drawing.Color.White
        Me.cmd_ExportToCSV.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_ExportToCSV.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_ExportToCSV.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_ExportToCSV.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_ExportToCSV.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ExportToCSV.ForeColor = System.Drawing.Color.Black
        Me.cmd_ExportToCSV.Image = Global.AccountsManagementForms.My.Resources.Resources.CSVIconColored22x22
        Me.cmd_ExportToCSV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_ExportToCSV.Location = New System.Drawing.Point(856, 13)
        Me.cmd_ExportToCSV.Name = "cmd_ExportToCSV"
        Me.cmd_ExportToCSV.Size = New System.Drawing.Size(150, 39)
        Me.cmd_ExportToCSV.TabIndex = 5
        Me.cmd_ExportToCSV.Text = "    Generate &H2H File"
        Me.cmd_ExportToCSV.UseVisualStyleBackColor = False
        '
        'cmd_Search
        '
        Me.cmd_Search.BackColor = System.Drawing.Color.White
        Me.cmd_Search.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Search.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Search.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Search.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Search.Image = Global.AccountsManagementForms.My.Resources.Resources.magnifyingglassvector22x22
        Me.cmd_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Search.Location = New System.Drawing.Point(240, 18)
        Me.cmd_Search.Name = "cmd_Search"
        Me.cmd_Search.Size = New System.Drawing.Size(32, 24)
        Me.cmd_Search.TabIndex = 2
        Me.cmd_Search.UseVisualStyleBackColor = False
        '
        'cmd_refresh
        '
        Me.cmd_refresh.BackColor = System.Drawing.Color.White
        Me.cmd_refresh.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_refresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_refresh.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_refresh.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_refresh.Image = Global.AccountsManagementForms.My.Resources.Resources.RefreshGreenIcon22x22
        Me.cmd_refresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_refresh.Location = New System.Drawing.Point(278, 17)
        Me.cmd_refresh.Name = "cmd_refresh"
        Me.cmd_refresh.Size = New System.Drawing.Size(32, 24)
        Me.cmd_refresh.TabIndex = 4
        Me.cmd_refresh.UseVisualStyleBackColor = False
        '
        'cmd_close
        '
        Me.cmd_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_close.BackColor = System.Drawing.Color.White
        Me.cmd_close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.cmd_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_close.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_close.ForeColor = System.Drawing.Color.Black
        Me.cmd_close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_close.Location = New System.Drawing.Point(891, 481)
        Me.cmd_close.Name = "cmd_close"
        Me.cmd_close.Size = New System.Drawing.Size(133, 39)
        Me.cmd_close.TabIndex = 3
        Me.cmd_close.Text = "&Close"
        Me.cmd_close.UseVisualStyleBackColor = False
        '
        'frmEFT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1036, 526)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgv_dataView)
        Me.Controls.Add(Me.cmd_close)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MinimumSize = New System.Drawing.Size(1052, 448)
        Me.Name = "frmEFT"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Electronic Fund Transfer (EFT) "
        CType(Me.dgv_dataView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cbo_AllocationDate As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgv_dataView As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmd_refresh As System.Windows.Forms.Button
    Friend WithEvents cmd_close As System.Windows.Forms.Button
    Friend WithEvents cmd_Search As System.Windows.Forms.Button
    Friend WithEvents cmd_ExportToCSV As System.Windows.Forms.Button
    Friend WithEvents AllocDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParticipantID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PayType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CheckNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExcessCollection As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DeferredEnergy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DeferredVATonEnergy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OffsetDeferredEnergy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OffsetDeferredVATonEnergy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Energy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VAT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TransferToPrudential As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TransferToFinPen As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InterestNSS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IntSTL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TotalPayment As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
