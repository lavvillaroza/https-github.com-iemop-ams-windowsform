Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmWESMBillBillingPeriodChanger
    Private WBillHelper As WESMBillHelper
    Private _ListOfBP As New List(Of CalendarBillingPeriod)
    Private _WESMBPChangeHist As List(Of WESMBPChangeHistory)   

    Private _GetWESMBillGPPosted As WESMBillGPPosted
    Public Property GetWESMBillGPPosted() As WESMBillGPPosted
        Get
            Return _GetWESMBillGPPosted
        End Get
        Set(ByVal value As WESMBillGPPosted)
            _GetWESMBillGPPosted = value
        End Set
    End Property


    Private Sub frmWESMBillBillingPeriodChanger_Load(sender As Object, e As EventArgs) Handles MyBase.Load        
        Me.MdiParent = MainForm
        Try
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName
            _ListOfBP = WBillHelper.GetCalendarBP()
            Me.fillCBNewBillingPeriod()
            Me.fillCBOldBillingPeriod()
            Me.fillDataGridView()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.ChangeWESMBillBP.ToString,
                                    ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
        End Try
    End Sub

    Private Sub fillCBNewBillingPeriod()
        cb_NewBillingPeriod.Items.Clear()
        For Each item In _ListOfBP.OrderByDescending(Function(x) x.BillingPeriod)
            cb_NewBillingPeriod.Items.Add(item.BillingPeriod)
        Next
    End Sub

    Private Sub fillCBOldBillingPeriod()
        cb_CurrBillingPeriod.Items.Clear()
        cb_CurrBillingPeriod.Items.Add(_GetWESMBillGPPosted.BillingPeriod)
        cb_CurrBillingPeriod.SelectedIndex = 0
        cb_CurrBillingPeriod.Enabled = False
    End Sub

    Private Sub fillDataGridView()
        Me.DGridView.Rows.Clear()
        Me._WESMBPChangeHist = WBillHelper.GetWBBPChangeHistory(_GetWESMBillGPPosted.BatchCode)
        For Each item In Me._WESMBPChangeHist.OrderBy(Function(x) x.UpdatedDate)
            Me.DGridView.Rows.Add(item.BatchCode, item.DueDate.ToString("MM/dd/yyyy"), item.NewBillingPeriod,
                                  item.OldBillingPeriod, item.SettlementRun, item.Charge.ToString, item.UpdatedBy, item.UpdatedDate.ToString("MM/dd/yyyy hh:mm:ss"))
        Next
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If Me.cb_NewBillingPeriod.SelectedIndex = -1 Then
            MessageBox.Show("Please select new billing period.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim _WESMBillChangeHistory As New WESMBPChangeHistory
        With _WESMBillChangeHistory
            .OldBillingPeriod = _GetWESMBillGPPosted.BillingPeriod
            .BatchCode = _GetWESMBillGPPosted.BatchCode
            .DueDate = _GetWESMBillGPPosted.DueDate
            .SettlementRun = _GetWESMBillGPPosted.SettlementRun
            .Charge = _GetWESMBillGPPosted.Charge
            .NewBillingPeriod = cb_NewBillingPeriod.SelectedItem.ToString            
        End With
        
        Try
            Dim msgAns As New MsgBoxResult
            msgAns = MsgBox("Do you really want to save this changes?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "System Message")

            If msgAns = MsgBoxResult.Yes Then
                ProgressThread.Show("Please wait while SAVING.")
                WBillHelper.SaveWBBPChangeHistory(_WESMBillChangeHistory)
                WBillHelper.UpdateWESMBillsTable(_WESMBillChangeHistory)
                WBillHelper.UpdateWESMBillGPPostedTable(_WESMBillChangeHistory)
                ProgressThread.Close()
                MessageBox.Show("The processed data have been successfully saved.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
                With frmWESMBillBillingPeriodMgt
                    .refereshDGV()
                End With
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.ChangeWESMBillBP.ToString, "Changing Billing Period from " & _WESMBillChangeHistory.OldBillingPeriod & " to " & _WESMBillChangeHistory.NewBillingPeriod, "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccesffullySaved.ToString, AMModule.UserName)
            End If
        Catch ex As Exception
            ProgressThread.Close()            
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnResetChanges_Click(sender As Object, e As EventArgs) Handles btnResetChanges.Click
        Me.Close()
    End Sub
End Class