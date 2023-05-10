<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNSSMonitoringSearch
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtBilligPeriodTo = New System.Windows.Forms.TextBox()
        Me.txtBillingPeriodFrom = New System.Windows.Forms.TextBox()
        Me.ddlBillingPeriodTo = New System.Windows.Forms.ComboBox()
        Me.ddlBillingPeriodFrom = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DGridView = New System.Windows.Forms.DataGridView()
        Me.colBillingPeriod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colStartDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEndDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBillingDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBillingPeriodValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colInterestRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colInterestRateOnInterest = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnOk)
        Me.GroupBox1.Controls.Add(Me.btnCancel)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(395, 129)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOk.Location = New System.Drawing.Point(173, 83)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(102, 31)
        Me.btnOk.TabIndex = 2
        Me.btnOk.Text = "&OK"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(281, 83)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(102, 31)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtBilligPeriodTo)
        Me.GroupBox2.Controls.Add(Me.txtBillingPeriodFrom)
        Me.GroupBox2.Controls.Add(Me.ddlBillingPeriodTo)
        Me.GroupBox2.Controls.Add(Me.ddlBillingPeriodFrom)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 14)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(375, 63)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'txtBilligPeriodTo
        '
        Me.txtBilligPeriodTo.Location = New System.Drawing.Point(178, 37)
        Me.txtBilligPeriodTo.Name = "txtBilligPeriodTo"
        Me.txtBilligPeriodTo.Size = New System.Drawing.Size(189, 20)
        Me.txtBilligPeriodTo.TabIndex = 5
        '
        'txtBillingPeriodFrom
        '
        Me.txtBillingPeriodFrom.Location = New System.Drawing.Point(178, 14)
        Me.txtBillingPeriodFrom.Name = "txtBillingPeriodFrom"
        Me.txtBillingPeriodFrom.Size = New System.Drawing.Size(189, 20)
        Me.txtBillingPeriodFrom.TabIndex = 4
        '
        'ddlBillingPeriodTo
        '
        Me.ddlBillingPeriodTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlBillingPeriodTo.FormattingEnabled = True
        Me.ddlBillingPeriodTo.Location = New System.Drawing.Point(46, 36)
        Me.ddlBillingPeriodTo.Name = "ddlBillingPeriodTo"
        Me.ddlBillingPeriodTo.Size = New System.Drawing.Size(126, 20)
        Me.ddlBillingPeriodTo.TabIndex = 3
        '
        'ddlBillingPeriodFrom
        '
        Me.ddlBillingPeriodFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlBillingPeriodFrom.FormattingEnabled = True
        Me.ddlBillingPeriodFrom.Location = New System.Drawing.Point(46, 13)
        Me.ddlBillingPeriodFrom.Name = "ddlBillingPeriodFrom"
        Me.ddlBillingPeriodFrom.Size = New System.Drawing.Size(126, 20)
        Me.ddlBillingPeriodFrom.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(21, 12)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "To:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "From:"
        '
        'DGridView
        '
        Me.DGridView.AllowUserToAddRows = False
        Me.DGridView.AllowUserToDeleteRows = False
        Me.DGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colBillingPeriod, Me.colStartDate, Me.colEndDate, Me.colBillingDate, Me.colBillingPeriodValue, Me.colInterestRate, Me.colInterestRateOnInterest})
        Me.DGridView.Location = New System.Drawing.Point(7, 138)
        Me.DGridView.Name = "DGridView"
        Me.DGridView.Size = New System.Drawing.Size(228, 87)
        Me.DGridView.TabIndex = 1
        Me.DGridView.Visible = False
        '
        'colBillingPeriod
        '
        Me.colBillingPeriod.HeaderText = "BillingPeriod"
        Me.colBillingPeriod.Name = "colBillingPeriod"
        '
        'colStartDate
        '
        Me.colStartDate.HeaderText = "StartDate"
        Me.colStartDate.Name = "colStartDate"
        Me.colStartDate.Visible = False
        '
        'colEndDate
        '
        Me.colEndDate.HeaderText = "EndDate"
        Me.colEndDate.Name = "colEndDate"
        Me.colEndDate.Visible = False
        '
        'colBillingDate
        '
        Me.colBillingDate.HeaderText = "BillingDate"
        Me.colBillingDate.Name = "colBillingDate"
        Me.colBillingDate.Visible = False
        '
        'colBillingPeriodValue
        '
        Me.colBillingPeriodValue.HeaderText = "DateRange"
        Me.colBillingPeriodValue.Name = "colBillingPeriodValue"
        Me.colBillingPeriodValue.Width = 150
        '
        'colInterestRate
        '
        Me.colInterestRate.HeaderText = "InterestRate"
        Me.colInterestRate.Name = "colInterestRate"
        '
        'colInterestRateOnInterest
        '
        Me.colInterestRateOnInterest.HeaderText = "InterestRateOnInterest"
        Me.colInterestRateOnInterest.Name = "colInterestRateOnInterest"
        Me.colInterestRateOnInterest.Width = 120
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 243)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(413, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'frmNSSMonitoringSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(413, 265)
        Me.ControlBox = False
        Me.Controls.Add(Me.DGridView)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmNSSMonitoringSearch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "NSS Monitoring Search"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.DGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtBillingPeriodFrom As System.Windows.Forms.TextBox
    Friend WithEvents ddlBillingPeriodTo As System.Windows.Forms.ComboBox
    Friend WithEvents ddlBillingPeriodFrom As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBilligPeriodTo As System.Windows.Forms.TextBox
    Friend WithEvents DGridView As System.Windows.Forms.DataGridView
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents colBillingPeriod As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStartDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEndDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBillingDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBillingPeriodValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colInterestRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colInterestRateOnInterest As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
