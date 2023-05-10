Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib
Public Class frmAddMappingMgt    
    Public _ParentChild1stOffsetExHelper As ParentChildExempHelper
    Public WBillHelper As WESMBillHelper   
    Public _isView As Boolean
    Public _frmName As String

    Private Sub frm1stOffsettingExemptMng_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try           
            Me.cmb_chargeId.Items.Add(EnumChargeType.E.ToString)
            Me.cmb_chargeId.Items.Add(EnumChargeType.EV.ToString)
            Me.cmb_chargeId.Items.Add(EnumChargeType.MF.ToString)
            Me.cmb_chargeId.Items.Add(EnumChargeType.MFV.ToString)

            If _isView = True Then
                Me.Text = _frmName

                With _ParentChild1stOffsetExHelper
                    Me.cmb_parentParticipant.Items.Add(.ParentChildExempView.ParentParticipantID.IDNumber)
                    Me.txtbox_ParentID.Text = .ParentChildExempView.ParentParticipantID.ParticipantID
                    Me.cmb_parentParticipant.SelectedItem = .ParentChildExempView.ParentParticipantID.IDNumber
                    Me.cmb_parentParticipant.Enabled = False

                    Me.cmb_childParticipant.Items.Add(.ParentChildExempView.ChildParticipantID.IDNumber)
                    Me.txtbox_ChildID.Text = .ParentChildExempView.ChildParticipantID.ParticipantID
                    Me.cmb_childParticipant.SelectedItem = .ParentChildExempView.ChildParticipantID.IDNumber
                    Me.cmb_childParticipant.Enabled = False

                    Me.cmb_chargeId.SelectedItem = .ParentChildExempView.ChargeType.ToString
                    Me.cmb_chargeId.Enabled = False
                    If .ParentChildExempView.Status = EnumStatus.Active Then
                        Me.rd_active.Checked = True
                    Else
                        Me.rd_Inactive.Checked = True
                    End If
                End With

            Else
                With _ParentChild1stOffsetExHelper
                    For Each item In .ListOfParticipantsInfo
                        Me.cmb_parentParticipant.Items.Add(item.IDNumber)
                        Me.cmb_childParticipant.Items.Add(item.IDNumber)
                    Next
                End With
                Me.cmb_chargeId.SelectedItem = EnumChargeType.E.ToString
                Me.rd_active.Checked = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.SAPWESMBillOffsetingWindow.ToString,
                                    ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
        End Try
    End Sub

    Private Sub btn_Save_Click(sender As Object, e As EventArgs) Handles btn_Save.Click
        Try
            Dim ParentID As String = Me.cmb_parentParticipant.SelectedItem.ToString
            Dim ChildID As String = Me.cmb_childParticipant.SelectedItem.ToString
            Dim oChargeType As EnumChargeType = CType(CStr(System.Enum.Parse(GetType(EnumChargeType), Me.cmb_chargeId.SelectedItem.ToString)), EnumChargeType)
            Dim oStatus As EnumStatus
            If Me.rd_active.Checked = True Then
                oStatus = EnumStatus.Active
            Else
                oStatus = EnumStatus.InActive
            End If
            If Not _isView = True Then
                Dim msgAns As New MsgBoxResult
                msgAns = MsgBox("Do you really want to save this record?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "System Message")

                If msgAns = MsgBoxResult.Yes Then
                    ProgressThread.Show("Please wait while saving.")
                    Me._ParentChild1stOffsetExHelper.SaveNewExemption(ParentID, ChildID, oChargeType, oStatus)
                    ProgressThread.Close()
                    _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibParticipantPACEOWindow.ToString,
                                    "Adding Parent:" + ParentID + "with the Child:" + ChildID + " have been successfully saved", "", "", CType(EnumColorCode.Green, ColorCode), EnumLogType.SuccesffullySaved.ToString, AMModule.UserName)
                    MessageBox.Show("The processed data have been successfully saved.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                    frmParticipantPCMapping.LoadDataTable()
                End If
            Else
                Dim msgAns As New MsgBoxResult
                msgAns = MsgBox("Do you really want to update and save this changes?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "System Message")

                If msgAns = MsgBoxResult.Yes Then
                    ProgressThread.Show("Please wait while saving.")
                    Me._ParentChild1stOffsetExHelper.UpdateExemption(ParentID, ChildID, oChargeType, oStatus)
                    ProgressThread.Close()
                    _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibParticipantPACEOWindow.ToString,
                                    "Updating Parent:" + ParentID + "with the Child:" + ChildID + " have been successfully saved", "", "", CType(EnumColorCode.Green, ColorCode), EnumLogType.SuccesffullySaved.ToString, AMModule.UserName)
                    MessageBox.Show("The processed data have been successfully saved.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                    frmParticipantPCMapping.LoadDataTable()
                End If
            End If

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibParticipantPACEOWindow.ToString,
                                    ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
        End Try
    End Sub

    Private Sub btn_Close_Click(sender As Object, e As EventArgs) Handles btn_Close.Click
        Me.Close()
    End Sub

    Private Sub cmb_parentParticipant_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_parentParticipant.SelectedIndexChanged
        Try
            If _isView = False Then
                With _ParentChild1stOffsetExHelper
                    Dim getParentName As String = (From x In .ListOfParticipantsInfo Where x.IDNumber = Me.cmb_parentParticipant.SelectedItem Select x.ParticipantID).FirstOrDefault
                    Me.txtbox_ParentID.Enabled = True
                    Me.txtbox_ParentID.Text = getParentName
                    Me.txtbox_ParentID.Enabled = False
                End With
            End If            
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibParticipantPACEOWindow.ToString,
                                    ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
        End Try
    End Sub

    Private Sub cmb_childParticipant_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_childParticipant.SelectedIndexChanged
        Try
            If _isView = False Then
                With _ParentChild1stOffsetExHelper
                    Dim getChildName As String = (From x In .ListOfParticipantsInfo Where x.IDNumber = Me.cmb_childParticipant.SelectedItem Select x.ParticipantID).FirstOrDefault
                    Me.txtbox_ChildID.Enabled = True
                    Me.txtbox_ChildID.Text = getChildName
                    Me.txtbox_ChildID.Enabled = False
                End With
            End If
            
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibParticipantPACEOWindow.ToString,
                                    ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
        End Try
    End Sub


End Class