

Public Class frmPaymentAllocationSettings


    Private _CalledBy As New Form
    Public Property CalledBy() As Form
        Get
            Return _CalledBy
        End Get
        Set(ByVal value As Form)
            _CalledBy = value
        End Set
    End Property

    Private Sub frmPaymentAllocationSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        chk_ColumnList.Items.Clear()
        For Each item In CalledBy.Controls
            If item.GetType() Is GetType(GroupBox) Then
                Dim grpBox = CType(item, GroupBox)
                For Each ctrl In grpBox.Controls
                    If ctrl.GetType() Is GetType(DataGridView) Then
                        Dim dgView = CType(ctrl, DataGridView)
                        For x = 1 To dgView.ColumnCount - 1
                            With dgView
                                If dgView.Columns(x).Visible = True Then
                                    If dgView.Columns(x).Frozen = False Then
                                        chk_ColumnList.Items.Add(.Columns(x).HeaderText, .Columns(x).Visible)
                                    End If
                                End If
                            End With
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub chk_ColumnList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_ColumnList.SelectedIndexChanged
        For Each item In CalledBy.Controls
            If item.GetType() Is GetType(GroupBox) Then
                Dim grpBox = CType(item, GroupBox)
                For Each ctrl In grpBox.Controls
                    If ctrl.GetType() Is GetType(DataGridView) Then
                        Dim dgView = CType(ctrl, DataGridView)
                        For x = 1 To dgView.ColumnCount - 1
                            If dgView.Columns(x).HeaderText = Me.chk_ColumnList.SelectedItem.ToString Then
                                dgView.Columns(x).Visible = Me.chk_ColumnList.GetItemChecked(Me.chk_ColumnList.SelectedIndex)
                                dgView.Refresh()
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        frmProgress.Show()
        Application.DoEvents()
        For Each item In chk_ColumnList.Items
            For Each ctrl In CalledBy.Controls
                If ctrl.GetType() Is GetType(GroupBox) Then
                    Dim grpBox = CType(ctrl, GroupBox)
                    For Each grpCtrl In grpBox.Controls
                        If grpCtrl.GetType() Is GetType(DataGridView) Then
                            Dim dgView = CType(grpCtrl, DataGridView)
                            For x = 1 To dgView.ColumnCount - 1
                                If dgView.Columns(x).HeaderText = item.ToString Then
                                    dgView.Columns(x).Visible = True
                                    dgView.Refresh()
                                    Exit For
                                End If
                            Next
                            Continue For
                        End If
                    Next
                    Continue For
                End If
            Next
        Next
        Application.DoEvents()

        Me.frmPaymentAllocationSettings_Load(sender, e)
        frmProgress.Close()
    End Sub

    Private Sub cmd_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub
End Class