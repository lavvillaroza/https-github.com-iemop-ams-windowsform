<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportViewer
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
        Me.RPTViewer = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.SuspendLayout()
        '
        'RPTViewer
        '
        Me.RPTViewer.ActiveViewIndex = -1
        Me.RPTViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RPTViewer.CachedPageNumberPerDoc = 10
        Me.RPTViewer.Cursor = System.Windows.Forms.Cursors.Default
        Me.RPTViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RPTViewer.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RPTViewer.Location = New System.Drawing.Point(0, 0)
        Me.RPTViewer.Name = "RPTViewer"
        Me.RPTViewer.SelectionFormula = ""
        Me.RPTViewer.ShowCloseButton = False
        Me.RPTViewer.ShowCopyButton = False
        Me.RPTViewer.ShowGroupTreeButton = False
        Me.RPTViewer.ShowParameterPanelButton = False
        Me.RPTViewer.ShowRefreshButton = False
        Me.RPTViewer.Size = New System.Drawing.Size(984, 562)
        Me.RPTViewer.TabIndex = 0
        Me.RPTViewer.ViewTimeSelectionFormula = ""
        '
        'frmReportViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 562)
        Me.Controls.Add(Me.RPTViewer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmReportViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report Viewer"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RPTViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
