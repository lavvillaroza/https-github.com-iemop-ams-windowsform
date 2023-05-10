<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymentAllocationDetails
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
        Me.dgv_detailTable = New System.Windows.Forms.DataGridView()
        Me.grpBox_filter = New System.Windows.Forms.GroupBox()
        Me.cmd_GenerateReport = New System.Windows.Forms.Button()
        Me.cmd_Filter = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbo_PaymentType = New System.Windows.Forms.ComboBox()
        Me.cbo_DueDate = New System.Windows.Forms.ComboBox()
        Me.cbo_BillPeriod = New System.Windows.Forms.ComboBox()
        CType(Me.dgv_detailTable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBox_filter.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgv_detailTable
        '
        Me.dgv_detailTable.AllowUserToAddRows = False
        Me.dgv_detailTable.AllowUserToDeleteRows = False
        Me.dgv_detailTable.AllowUserToResizeColumns = False
        Me.dgv_detailTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgv_detailTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_detailTable.Location = New System.Drawing.Point(12, 141)
        Me.dgv_detailTable.Name = "dgv_detailTable"
        Me.dgv_detailTable.ReadOnly = True
        Me.dgv_detailTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgv_detailTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_detailTable.Size = New System.Drawing.Size(727, 497)
        Me.dgv_detailTable.TabIndex = 0
        '
        'grpBox_filter
        '
        Me.grpBox_filter.Controls.Add(Me.cmd_GenerateReport)
        Me.grpBox_filter.Controls.Add(Me.cmd_Filter)
        Me.grpBox_filter.Controls.Add(Me.Label3)
        Me.grpBox_filter.Controls.Add(Me.Label2)
        Me.grpBox_filter.Controls.Add(Me.Label1)
        Me.grpBox_filter.Controls.Add(Me.cbo_PaymentType)
        Me.grpBox_filter.Controls.Add(Me.cbo_DueDate)
        Me.grpBox_filter.Controls.Add(Me.cbo_BillPeriod)
        Me.grpBox_filter.Location = New System.Drawing.Point(12, 12)
        Me.grpBox_filter.Name = "grpBox_filter"
        Me.grpBox_filter.Size = New System.Drawing.Size(727, 123)
        Me.grpBox_filter.TabIndex = 1
        Me.grpBox_filter.TabStop = False
        '
        'cmd_GenerateReport
        '
        Me.cmd_GenerateReport.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_GenerateReport.Image = Global.AccountsManagementForms.My.Resources.Resources.report1
        Me.cmd_GenerateReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_GenerateReport.Location = New System.Drawing.Point(581, 54)
        Me.cmd_GenerateReport.Name = "cmd_GenerateReport"
        Me.cmd_GenerateReport.Size = New System.Drawing.Size(140, 34)
        Me.cmd_GenerateReport.TabIndex = 2
        Me.cmd_GenerateReport.Text = "Generate Report"
        Me.cmd_GenerateReport.UseVisualStyleBackColor = True
        '
        'cmd_Filter
        '
        Me.cmd_Filter.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Filter.Image = Global.AccountsManagementForms.My.Resources.Resources.search
        Me.cmd_Filter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Filter.Location = New System.Drawing.Point(581, 94)
        Me.cmd_Filter.Name = "cmd_Filter"
        Me.cmd_Filter.Size = New System.Drawing.Size(140, 23)
        Me.cmd_Filter.TabIndex = 2
        Me.cmd_Filter.Text = "Search"
        Me.cmd_Filter.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(309, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 15)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Payment Type"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(155, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Due Date"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica Condensed", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Billing Period"
        '
        'cbo_PaymentType
        '
        Me.cbo_PaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_PaymentType.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_PaymentType.FormattingEnabled = True
        Me.cbo_PaymentType.Location = New System.Drawing.Point(312, 29)
        Me.cbo_PaymentType.Name = "cbo_PaymentType"
        Me.cbo_PaymentType.Size = New System.Drawing.Size(162, 22)
        Me.cbo_PaymentType.TabIndex = 0
        '
        'cbo_DueDate
        '
        Me.cbo_DueDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_DueDate.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_DueDate.FormattingEnabled = True
        Me.cbo_DueDate.Location = New System.Drawing.Point(158, 29)
        Me.cbo_DueDate.Name = "cbo_DueDate"
        Me.cbo_DueDate.Size = New System.Drawing.Size(121, 22)
        Me.cbo_DueDate.TabIndex = 0
        '
        'cbo_BillPeriod
        '
        Me.cbo_BillPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_BillPeriod.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_BillPeriod.FormattingEnabled = True
        Me.cbo_BillPeriod.Location = New System.Drawing.Point(6, 29)
        Me.cbo_BillPeriod.Name = "cbo_BillPeriod"
        Me.cbo_BillPeriod.Size = New System.Drawing.Size(121, 22)
        Me.cbo_BillPeriod.TabIndex = 0
        '
        'frmPaymentAllocationDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(751, 650)
        Me.Controls.Add(Me.grpBox_filter)
        Me.Controls.Add(Me.dgv_detailTable)
        Me.Name = "frmPaymentAllocationDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "View Payment Allocation Details"
        CType(Me.dgv_detailTable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBox_filter.ResumeLayout(False)
        Me.grpBox_filter.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgv_detailTable As System.Windows.Forms.DataGridView
    Friend WithEvents grpBox_filter As System.Windows.Forms.GroupBox
    Friend WithEvents cmd_Filter As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbo_PaymentType As System.Windows.Forms.ComboBox
    Friend WithEvents cbo_DueDate As System.Windows.Forms.ComboBox
    Friend WithEvents cbo_BillPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents cmd_GenerateReport As System.Windows.Forms.Button
End Class
