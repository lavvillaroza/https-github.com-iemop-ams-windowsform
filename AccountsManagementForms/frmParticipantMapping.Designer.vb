<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParticipantMapping
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParticipantMapping))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rb_Active = New System.Windows.Forms.RadioButton
        Me.rb_InActive = New System.Windows.Forms.RadioButton
        Me.rb_All = New System.Windows.Forms.RadioButton
        Me.grp_MapType = New System.Windows.Forms.GroupBox
        Me.chk_childID = New System.Windows.Forms.CheckBox
        Me.chk_ParentID = New System.Windows.Forms.CheckBox
        Me.chk_BillPeriod = New System.Windows.Forms.CheckBox
        Me.cmd_close = New System.Windows.Forms.Button
        Me.chk_participantId = New System.Windows.Forms.CheckBox
        Me.cbo_billPeriod = New System.Windows.Forms.ComboBox
        Me.cbo_ParticipantID = New System.Windows.Forms.ComboBox
        Me.btn_TSearch = New System.Windows.Forms.Button
        Me.btn_New = New System.Windows.Forms.Button
        Me.btn_Edit = New System.Windows.Forms.Button
        Me.btn_Del = New System.Windows.Forms.Button
        Me.btn_refresh = New System.Windows.Forms.Button
        Me.dgv_ViewDetails = New System.Windows.Forms.DataGridView
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grp_MapType.SuspendLayout()
        CType(Me.dgv_ViewDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GroupBox1)
        Me.GroupBox2.Controls.Add(Me.grp_MapType)
        Me.GroupBox2.Controls.Add(Me.chk_BillPeriod)
        Me.GroupBox2.Controls.Add(Me.cmd_close)
        Me.GroupBox2.Controls.Add(Me.chk_participantId)
        Me.GroupBox2.Controls.Add(Me.cbo_billPeriod)
        Me.GroupBox2.Controls.Add(Me.cbo_ParticipantID)
        Me.GroupBox2.Controls.Add(Me.btn_TSearch)
        Me.GroupBox2.Controls.Add(Me.btn_New)
        Me.GroupBox2.Controls.Add(Me.btn_Edit)
        Me.GroupBox2.Controls.Add(Me.btn_Del)
        Me.GroupBox2.Controls.Add(Me.btn_refresh)
        Me.GroupBox2.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(596, 135)
        Me.GroupBox2.TabIndex = 36
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Search Participant Mapping"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rb_Active)
        Me.GroupBox1.Controls.Add(Me.rb_InActive)
        Me.GroupBox1.Controls.Add(Me.rb_All)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 79)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(242, 50)
        Me.GroupBox1.TabIndex = 46
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filter By Status"
        '
        'rb_Active
        '
        Me.rb_Active.AutoSize = True
        Me.rb_Active.Checked = True
        Me.rb_Active.Location = New System.Drawing.Point(6, 20)
        Me.rb_Active.Name = "rb_Active"
        Me.rb_Active.Size = New System.Drawing.Size(58, 18)
        Me.rb_Active.TabIndex = 43
        Me.rb_Active.TabStop = True
        Me.rb_Active.Text = "Active"
        Me.rb_Active.UseVisualStyleBackColor = True
        '
        'rb_InActive
        '
        Me.rb_InActive.AutoSize = True
        Me.rb_InActive.Location = New System.Drawing.Point(84, 20)
        Me.rb_InActive.Name = "rb_InActive"
        Me.rb_InActive.Size = New System.Drawing.Size(72, 18)
        Me.rb_InActive.TabIndex = 45
        Me.rb_InActive.TabStop = True
        Me.rb_InActive.Text = "In-Active"
        Me.rb_InActive.UseVisualStyleBackColor = True
        '
        'rb_All
        '
        Me.rb_All.AutoSize = True
        Me.rb_All.Location = New System.Drawing.Point(197, 20)
        Me.rb_All.Name = "rb_All"
        Me.rb_All.Size = New System.Drawing.Size(39, 18)
        Me.rb_All.TabIndex = 44
        Me.rb_All.TabStop = True
        Me.rb_All.Text = "All"
        Me.rb_All.UseVisualStyleBackColor = True
        '
        'grp_MapType
        '
        Me.grp_MapType.Controls.Add(Me.chk_childID)
        Me.grp_MapType.Controls.Add(Me.chk_ParentID)
        Me.grp_MapType.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grp_MapType.Location = New System.Drawing.Point(254, 19)
        Me.grp_MapType.Name = "grp_MapType"
        Me.grp_MapType.Size = New System.Drawing.Size(150, 54)
        Me.grp_MapType.TabIndex = 42
        Me.grp_MapType.TabStop = False
        Me.grp_MapType.Text = "Participant Mapping Type"
        '
        'chk_childID
        '
        Me.chk_childID.AutoSize = True
        Me.chk_childID.Location = New System.Drawing.Point(96, 27)
        Me.chk_childID.Name = "chk_childID"
        Me.chk_childID.Size = New System.Drawing.Size(48, 16)
        Me.chk_childID.TabIndex = 40
        Me.chk_childID.Text = "Child"
        Me.chk_childID.UseVisualStyleBackColor = True
        '
        'chk_ParentID
        '
        Me.chk_ParentID.AutoSize = True
        Me.chk_ParentID.Location = New System.Drawing.Point(6, 27)
        Me.chk_ParentID.Name = "chk_ParentID"
        Me.chk_ParentID.Size = New System.Drawing.Size(56, 16)
        Me.chk_ParentID.TabIndex = 41
        Me.chk_ParentID.Text = "Parent"
        Me.chk_ParentID.UseVisualStyleBackColor = True
        '
        'chk_BillPeriod
        '
        Me.chk_BillPeriod.AutoSize = True
        Me.chk_BillPeriod.Location = New System.Drawing.Point(6, 54)
        Me.chk_BillPeriod.Name = "chk_BillPeriod"
        Me.chk_BillPeriod.Size = New System.Drawing.Size(99, 18)
        Me.chk_BillPeriod.TabIndex = 38
        Me.chk_BillPeriod.Text = "Billing Period"
        Me.chk_BillPeriod.UseVisualStyleBackColor = True
        '
        'cmd_close
        '
        Me.cmd_close.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmd_close.Font = New System.Drawing.Font("Helvetica Condensed", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_close.Image = Global.AccountsManagementForms.My.Resources.Resources.close
        Me.cmd_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_close.Location = New System.Drawing.Point(410, 47)
        Me.cmd_close.Name = "cmd_close"
        Me.cmd_close.Size = New System.Drawing.Size(177, 25)
        Me.cmd_close.TabIndex = 8
        Me.cmd_close.Text = "Close"
        Me.cmd_close.UseVisualStyleBackColor = True
        '
        'chk_participantId
        '
        Me.chk_participantId.AutoSize = True
        Me.chk_participantId.Location = New System.Drawing.Point(6, 21)
        Me.chk_participantId.Name = "chk_participantId"
        Me.chk_participantId.Size = New System.Drawing.Size(99, 18)
        Me.chk_participantId.TabIndex = 37
        Me.chk_participantId.Text = "Participant ID"
        Me.chk_participantId.UseVisualStyleBackColor = True
        '
        'cbo_billPeriod
        '
        Me.cbo_billPeriod.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbo_billPeriod.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbo_billPeriod.FormattingEnabled = True
        Me.cbo_billPeriod.Location = New System.Drawing.Point(116, 52)
        Me.cbo_billPeriod.Name = "cbo_billPeriod"
        Me.cbo_billPeriod.Size = New System.Drawing.Size(132, 22)
        Me.cbo_billPeriod.TabIndex = 10
        '
        'cbo_ParticipantID
        '
        Me.cbo_ParticipantID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbo_ParticipantID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbo_ParticipantID.FormattingEnabled = True
        Me.cbo_ParticipantID.Location = New System.Drawing.Point(116, 19)
        Me.cbo_ParticipantID.Name = "cbo_ParticipantID"
        Me.cbo_ParticipantID.Size = New System.Drawing.Size(132, 22)
        Me.cbo_ParticipantID.TabIndex = 9
        '
        'btn_TSearch
        '
        Me.btn_TSearch.BackgroundImage = CType(resources.GetObject("btn_TSearch.BackgroundImage"), System.Drawing.Image)
        Me.btn_TSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btn_TSearch.Location = New System.Drawing.Point(410, 15)
        Me.btn_TSearch.Name = "btn_TSearch"
        Me.btn_TSearch.Size = New System.Drawing.Size(29, 27)
        Me.btn_TSearch.TabIndex = 2
        Me.btn_TSearch.UseVisualStyleBackColor = True
        '
        'btn_New
        '
        Me.btn_New.BackgroundImage = Global.AccountsManagementForms.My.Resources.Resources.Add
        Me.btn_New.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_New.Location = New System.Drawing.Point(445, 15)
        Me.btn_New.Name = "btn_New"
        Me.btn_New.Size = New System.Drawing.Size(33, 27)
        Me.btn_New.TabIndex = 3
        Me.btn_New.UseVisualStyleBackColor = True
        '
        'btn_Edit
        '
        Me.btn_Edit.BackgroundImage = Global.AccountsManagementForms.My.Resources.Resources.Edit
        Me.btn_Edit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_Edit.Location = New System.Drawing.Point(484, 15)
        Me.btn_Edit.Name = "btn_Edit"
        Me.btn_Edit.Size = New System.Drawing.Size(29, 27)
        Me.btn_Edit.TabIndex = 4
        Me.btn_Edit.UseVisualStyleBackColor = True
        '
        'btn_Del
        '
        Me.btn_Del.BackgroundImage = Global.AccountsManagementForms.My.Resources.Resources.delete1
        Me.btn_Del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_Del.Location = New System.Drawing.Point(519, 15)
        Me.btn_Del.Name = "btn_Del"
        Me.btn_Del.Size = New System.Drawing.Size(33, 27)
        Me.btn_Del.TabIndex = 5
        Me.btn_Del.UseVisualStyleBackColor = True
        '
        'btn_refresh
        '
        Me.btn_refresh.BackgroundImage = CType(resources.GetObject("btn_refresh.BackgroundImage"), System.Drawing.Image)
        Me.btn_refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btn_refresh.Location = New System.Drawing.Point(558, 15)
        Me.btn_refresh.Name = "btn_refresh"
        Me.btn_refresh.Size = New System.Drawing.Size(29, 27)
        Me.btn_refresh.TabIndex = 6
        Me.btn_refresh.UseVisualStyleBackColor = True
        '
        'dgv_ViewDetails
        '
        Me.dgv_ViewDetails.AllowUserToAddRows = False
        Me.dgv_ViewDetails.AllowUserToDeleteRows = False
        Me.dgv_ViewDetails.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_ViewDetails.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_ViewDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgv_ViewDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_ViewDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv_ViewDetails.Location = New System.Drawing.Point(18, 148)
        Me.dgv_ViewDetails.Name = "dgv_ViewDetails"
        Me.dgv_ViewDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_ViewDetails.Size = New System.Drawing.Size(590, 434)
        Me.dgv_ViewDetails.TabIndex = 37
        '
        'frmParticipantMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(618, 594)
        Me.Controls.Add(Me.dgv_ViewDetails)
        Me.Controls.Add(Me.GroupBox2)
        Me.Name = "frmParticipantMapping"
        Me.Text = "Parent-Child Mapping"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grp_MapType.ResumeLayout(False)
        Me.grp_MapType.PerformLayout()
        CType(Me.dgv_ViewDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chk_BillPeriod As System.Windows.Forms.CheckBox
    Friend WithEvents chk_participantId As System.Windows.Forms.CheckBox
    Friend WithEvents cbo_billPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents cbo_ParticipantID As System.Windows.Forms.ComboBox
    Friend WithEvents cmd_close As System.Windows.Forms.Button
    Friend WithEvents btn_TSearch As System.Windows.Forms.Button
    Friend WithEvents btn_New As System.Windows.Forms.Button
    Friend WithEvents btn_Edit As System.Windows.Forms.Button
    Friend WithEvents btn_Del As System.Windows.Forms.Button
    Friend WithEvents btn_refresh As System.Windows.Forms.Button
    Friend WithEvents dgv_ViewDetails As System.Windows.Forms.DataGridView
    Friend WithEvents chk_childID As System.Windows.Forms.CheckBox
    Friend WithEvents chk_ParentID As System.Windows.Forms.CheckBox
    Friend WithEvents grp_MapType As System.Windows.Forms.GroupBox
    Private WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_Active As System.Windows.Forms.RadioButton
    Friend WithEvents rb_InActive As System.Windows.Forms.RadioButton
    Friend WithEvents rb_All As System.Windows.Forms.RadioButton
End Class
