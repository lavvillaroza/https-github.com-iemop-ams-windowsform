Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib
Public Class frmParticipantPCMapping
    Dim WBillHelper As WESMBillHelper
    Dim _ParentChildExempHlpr As New ParentChildExempHelper
    Private Sub frm1stOffsettingExemption_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Try
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName
            Me.LoadDataTable()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibParticipantPACEOWindow.ToString,
                                     ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
        End Try
    End Sub

    Public Sub LoadDataTable()
        Me._ParentChildExempHlpr.Initialize()
        Me.dgv_1stOffsettingEx.Rows.Clear()
        For Each item In _ParentChildExempHlpr.ParentChildExempList
            Me.dgv_1stOffsettingEx.Rows.Add(item.ParentParticipantID.IDNumber, item.ParentParticipantID.ParticipantID, item.ChildParticipantID.IDNumber, item.ChildParticipantID.ParticipantID, item.ChargeType, item.Status, item.UpdatedBy, item.UpdatedDate.ToString(), "VIEW")
        Next
    End Sub
    Private Sub btn_Add_Click(sender As Object, e As EventArgs) Handles btn_Add.Click
        Dim _frm1stOffsetExemption As New frmAddMappingMgt
        With _frm1stOffsetExemption            
            ._ParentChild1stOffsetExHelper = _ParentChildExempHlpr
            .WBillHelper = WBillHelper
            ._isView = False
            ._frmName = "Add P2C 1st Offsetting"
            .ShowDialog()
        End With
    End Sub

    Private Sub btn_Close_Click(sender As Object, e As EventArgs) Handles btn_Close.Click
        Me.Close()
    End Sub

    Private Sub dgv_1stOffsettingEx_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_1stOffsettingEx.CellContentClick
        Try
            If e.ColumnIndex = 8 Then                
                Dim ParentID As String = CStr(dgv_1stOffsettingEx.Rows(e.RowIndex).Cells("col_ParentID").Value)
                Dim ChildID As String = CStr(dgv_1stOffsettingEx.Rows(e.RowIndex).Cells("col_ChildID").Value)
                Dim ChargeType As EnumChargeType = CType(System.Enum.Parse(GetType(EnumChargeType), CStr(dgv_1stOffsettingEx.Rows(e.RowIndex).Cells("col_ChargeType").Value)), EnumChargeType)
                Dim Status As EnumStatus = CType(System.Enum.Parse(GetType(EnumStatus), CStr(dgv_1stOffsettingEx.Rows(e.RowIndex).Cells("col_Status").Value)), EnumStatus)
                Dim UpdatedBy As String = CStr(dgv_1stOffsettingEx.Rows(e.RowIndex).Cells("col_UpdatedBy").Value)
                Dim UpdatedDate As Date = CDate(dgv_1stOffsettingEx.Rows(e.RowIndex).Cells("col_UpdatedDate").Value)

                _ParentChildExempHlpr.GetSelectedExemption(ParentID, ChildID, ChargeType, Status, UpdatedBy, UpdatedDate)
                'Get the datasource for the report  
                Dim _frm1stOffsetExemption As New frmAddMappingMgt
                With _frm1stOffsetExemption
                    ._ParentChild1stOffsetExHelper = _ParentChildExempHlpr
                    .WBillHelper = WBillHelper
                    ._isView = True
                    ._frmName = "View P2C 1st Offsetting"
                    .ShowDialog()
                End With
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibParticipantPACEOWindow.ToString,
                                    ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
        End Try
    End Sub
End Class