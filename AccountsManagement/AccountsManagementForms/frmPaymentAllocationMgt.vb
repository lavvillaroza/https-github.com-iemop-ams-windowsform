'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmPaymentAllocationMgt
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     January 23, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for viewing of Payment Allocation Details
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   January 4, 2012         Juan Carlo L. Panopio       Changed whole design based from meeting.

Option Explicit On
Option Strict On

Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmPaymentAllocationMgt

    Private PaymentAllocation As FuncAutoPaymentAllocationResult
    Private PaymentBatch As Long
    Private SelectedGroup As Long

    Dim WBillHelper As WESMBillHelper


    Public Sub ViewPaymentAllocation(ByVal FullPaymentAlloc As FuncAutoPaymentAllocationResult, ByVal PaymentBatchCode As Long)
        PaymentAllocation = FullPaymentAlloc

        Dim filterPerParticipantAllocation = (From x In PaymentAllocation.lstPerParticipantAllocationViewing _
                                              Where x.PaymentBatchCode = CStr(PaymentBatchCode))
        PaymentBatch = PaymentBatchCode
        Dim TotalEnergyAllocated As Decimal = 0
        Dim TotalDefaultInterestAllocated As Decimal = 0
        Dim TotalVATAllocated As Decimal = 0

        For Each item In filterPerParticipantAllocation
            With item
                Me.dgv_ParticipantAllocation.Rows.Add(.PaymentGroupCode, .PaymentBatchCode, .BillPeriod, .Participant.IDNumber, _
                                                      .Participant.ParticipantID, .AllocDetails.Energy, .AllocDetails.DefaultInterest, .AllocDetails.VAT, _
                                                      .AllocDetails.DeferredEnergy, .AllocDetails.DeferredVAT, .AllocDetails.InterestEarnedNSS, _
                                                      .AllocDetails.InterestEarnedSTL, .AllocDetails.InterestEarnedPR, _
                                                      .AllocDetails.OffsetAllocatedEnergy, .AllocDetails.OffsetAMTEnergy, .AllocDetails.OffsetAllocatedVAT, .AllocDetails.OffsetAMTVAT, _
                                                      .AllocDetails.TotalEnergyAllocation, .AllocDetails.TotalVATAllocation, _
                                                      .AllocDetails.TotalEnergyPayment, .AllocDetails.TotalVATPayment)

                TotalEnergyAllocated += .AllocDetails.Energy
                TotalDefaultInterestAllocated += .AllocDetails.DefaultInterest
                TotalVATAllocated += .AllocDetails.VAT

            End With
        Next

        txt_tEnergyAlloc.Text = TotalEnergyAllocated.ToString("#,##0.00")
        txt_tDefaultInterest.Text = TotalDefaultInterestAllocated.ToString("#,##0.00")
        txt_tVATAlloc.Text = TotalVATAllocated.ToString("#,##0.00")

    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_DownloadCSV.Click
        'subExportDGVToCSV
        Me.MdiParent = MainForm
        Try
            Dim saveFileDialogBox As New SaveFileDialog
            Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = "JCLP"
            With saveFileDialogBox
                .Title = "Save WESM Bill"
                .Filter = "CSV Files (*.csv)|*.csv"
                If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                    WBillHelper.subExportDGVToCSV(.FileName, dgv_ParticipantAllocation, True)
                    MsgBox("Successfully Downloaded!", MsgBoxStyle.Information, "Done")
                End If
            End With

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub

    Private Sub cmd_ViewPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ViewPayment.Click
        PaymentBatch = CLng(dgv_ParticipantAllocation.CurrentRow.Cells("hPayBatchCode").Value)
        SelectedGroup = CLng(dgv_ParticipantAllocation.CurrentRow.Cells("hGroupNo").Value)

        Dim FilterParticipantSummary = (From x In PaymentAllocation.lstPerSummaryAllocation _
                                           Where x.PaymentBatchCode = CStr(PaymentBatch) _
                                           And x.PaymentGroupCode = SelectedGroup _
                                           Select x Order By x.WESMBillSummary.INVDMCMNo Ascending).ToList

        Dim FilterParticipantAllocation = (From x In PaymentAllocation.lstPerParticipantAllocation _
                                           Where x.PaymentBatchCode = CStr(PaymentBatch) _
                                           And x.PaymentGroupCode = SelectedGroup _
                                           Select x Order By x.UpdatedDate Ascending).ToList

        Dim FilterDMCMPerParticipant = (From x In PaymentAllocation.lstDebitCreditMemo _
                                        Join y In FilterParticipantSummary _
                                        On x.DMCMNumber Equals y.DMCMNo _
                                        Select x Distinct Order By x.UpdatedDate Ascending).ToList


        '                                And x.BillingPeriod = CInt(dgv_ParticipantAllocation.CurrentRow.Cells("hBillPeriod").Value) _

        Dim _tmpForm As New frmPaymentAllocationViewDetails
        With _tmpForm
            .ShowPaymentDetails(FilterParticipantAllocation, FilterParticipantSummary, FilterDMCMPerParticipant, PaymentAllocation.lstAccountCodes)
            .ShowDialog()
        End With

    End Sub

    Private Sub cmd_Settings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Settings.Click
        frmPaymentAllocationSettings.CalledBy = Me
        frmPaymentAllocationSettings.Show()
    End Sub

    Private Sub cmd_refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_refresh.Click
        For x = 1 To Me.dgv_ParticipantAllocation.ColumnCount - 1
            If x <> 1 Then
                Me.dgv_ParticipantAllocation.Columns(x).Visible = True
            End If
        Next
        Me.dgv_ParticipantAllocation.Refresh()
    End Sub

    'Dim a As New frmViewDetails
    'a.dgridView.DataSource = (From x In Me.PaymentAllocation.lstPerParticipantAllocation _
    '                          Where x.AllocDetails.TotalEnergyAllocation > 0 _
    '                          And x.PaymentGroupCode = 1 _
    '                          Select New With {.IDNumber = x.Participant.IDNumber, .ExcessAmount = x.AllocDetails.TotalEnergyAllocation, .ExcessVAT = x.AllocDetails.TotalVATAllocation}).ToList
    'a.ShowDialog()

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim a As New frmViewDetails
        a.dgridView.DataSource = (From x In Me.PaymentAllocation.lstPerParticipantAllocation _
                                  Where x.AllocDetails.TotalEnergyAllocation > 0 _
                                  And x.PaymentGroupCode = 1 _
                                  Select New With {.IDNumber = x.Participant.IDNumber, .ExcessAmount = x.AllocDetails.TotalEnergyAllocation, .ExcessVAT = x.AllocDetails.TotalVATAllocation}).ToList
        a.ShowDialog()
    End Sub

    
End Class