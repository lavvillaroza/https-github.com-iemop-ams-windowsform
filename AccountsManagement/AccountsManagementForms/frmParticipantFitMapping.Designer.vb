<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParticipantFitMapping
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParticipantFitMapping))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DGV_ParticipantsFit = New System.Windows.Forms.DataGridView()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.colFITID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBillingPeriod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBillingPeriodValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIDNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParentFIT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFull = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colView = New System.Windows.Forms.DataGridViewImageColumn()
        Me.colEdit = New System.Windows.Forms.DataGridViewImageColumn()
        Me.colDelete = New System.Windows.Forms.DataGridViewImageColumn()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DGV_ParticipantsFit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.DGV_ParticipantsFit)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(716, 286)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'DGV_ParticipantsFit
        '
        Me.DGV_ParticipantsFit.AllowUserToAddRows = False
        Me.DGV_ParticipantsFit.AllowUserToDeleteRows = False
        Me.DGV_ParticipantsFit.AllowUserToResizeColumns = False
        Me.DGV_ParticipantsFit.AllowUserToResizeRows = False
        Me.DGV_ParticipantsFit.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_ParticipantsFit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DGV_ParticipantsFit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_ParticipantsFit.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colFITID, Me.colBillingPeriod, Me.colBillingPeriodValue, Me.colIDNumber, Me.colParentFIT, Me.colFull, Me.colView, Me.colEdit, Me.colDelete})
        Me.DGV_ParticipantsFit.Location = New System.Drawing.Point(6, 13)
        Me.DGV_ParticipantsFit.Name = "DGV_ParticipantsFit"
        Me.DGV_ParticipantsFit.ReadOnly = True
        Me.DGV_ParticipantsFit.Size = New System.Drawing.Size(704, 267)
        Me.DGV_ParticipantsFit.TabIndex = 12
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 330)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(734, 22)
        Me.StatusStrip1.TabIndex = 18
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'DataGridViewImageColumn1
        '
        Me.DataGridViewImageColumn1.HeaderText = ""
        Me.DataGridViewImageColumn1.Image = Global.AccountsManagementForms.My.Resources.Resources.text
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        Me.DataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.DataGridViewImageColumn1.Width = 50
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.BackColor = System.Drawing.SystemColors.ControlLight
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAdd.Location = New System.Drawing.Point(557, 297)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(80, 30)
        Me.btnAdd.TabIndex = 17
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.SystemColors.ControlLight
        Me.btnClose.Image = Global.AccountsManagementForms.My.Resources.Resources.close
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(643, 297)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(82, 30)
        Me.btnClose.TabIndex = 15
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'colFITID
        '
        Me.colFITID.FillWeight = 200.0!
        Me.colFITID.HeaderText = "FITID"
        Me.colFITID.Name = "colFITID"
        Me.colFITID.ReadOnly = True
        Me.colFITID.Visible = False
        '
        'colBillingPeriod
        '
        Me.colBillingPeriod.HeaderText = "BillingPeriod"
        Me.colBillingPeriod.Name = "colBillingPeriod"
        Me.colBillingPeriod.ReadOnly = True
        Me.colBillingPeriod.Visible = False
        '
        'colBillingPeriodValue
        '
        Me.colBillingPeriodValue.HeaderText = "Billing Period"
        Me.colBillingPeriodValue.Name = "colBillingPeriodValue"
        Me.colBillingPeriodValue.ReadOnly = True
        Me.colBillingPeriodValue.Width = 160
        '
        'colIDNumber
        '
        Me.colIDNumber.HeaderText = "ID Number"
        Me.colIDNumber.Name = "colIDNumber"
        Me.colIDNumber.ReadOnly = True
        Me.colIDNumber.Visible = False
        '
        'colParentFIT
        '
        Me.colParentFIT.HeaderText = "Parent FIT"
        Me.colParentFIT.Name = "colParentFIT"
        Me.colParentFIT.ReadOnly = True
        '
        'colFull
        '
        Me.colFull.HeaderText = "Full Name"
        Me.colFull.Name = "colFull"
        Me.colFull.ReadOnly = True
        Me.colFull.Width = 250
        '
        'colView
        '
        Me.colView.HeaderText = ""
        Me.colView.Image = Global.AccountsManagementForms.My.Resources.Resources.report
        Me.colView.Name = "colView"
        Me.colView.ReadOnly = True
        Me.colView.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colView.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colView.ToolTipText = "View"
        Me.colView.Width = 50
        '
        'colEdit
        '
        Me.colEdit.HeaderText = ""
        Me.colEdit.Image = Global.AccountsManagementForms.My.Resources.Resources.edit_1
        Me.colEdit.Name = "colEdit"
        Me.colEdit.ReadOnly = True
        Me.colEdit.ToolTipText = "Edit"
        Me.colEdit.Width = 50
        '
        'colDelete
        '
        Me.colDelete.HeaderText = ""
        Me.colDelete.Image = Global.AccountsManagementForms.My.Resources.Resources.cancel
        Me.colDelete.Name = "colDelete"
        Me.colDelete.ReadOnly = True
        Me.colDelete.ToolTipText = "Delete"
        Me.colDelete.Width = 50
        '
        'frmParticipantFitMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(734, 352)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmParticipantFitMapping"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Participants FIT Mapping"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.DGV_ParticipantsFit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents DGV_ParticipantsFit As System.Windows.Forms.DataGridView
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents DataGridViewImageColumn1 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents colFITID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBillingPeriod As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBillingPeriodValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIDNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colParentFIT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFull As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colView As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents colEdit As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents colDelete As System.Windows.Forms.DataGridViewImageColumn
End Class
