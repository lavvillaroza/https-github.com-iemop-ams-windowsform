'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmPaymentTransferToPR
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     May 28, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for the transfer of Payment to Participant to Participant's Prudential Account
'Arguments/Parameters:  
'Files/Database Tables:  AM_PEMC_PAYMENT
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description


Option Explicit On
Option Strict On

Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Public Class frmPaymentTransferToPR
    Private WBillHelper As WESMBillHelper
    Private PaymentAllocation As New FuncAutoPaymentAllocationResult
    Public _PRRequirements As New List(Of Prudential)
    Public DocSignatures As New List(Of DocSignatories)
    Public MainPaymentForm As New frmPayment
#Region "Form Functions"
    'Load Data to Datagrid
    Public Sub LoadForTransfer(ByRef ParticipantPaymentAllocation As FuncAutoPaymentAllocationResult)
        Me.MdiParent = MainForm

        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"

        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        Me.PaymentAllocation = ParticipantPaymentAllocation

        _PRRequirements = WBillHelper.GetParticipantsPrudential()
        'Prepare Payment Allocation to viewing
        'Get Distinct Participant with allocation
        Dim _ParticipantList = (From x In PaymentAllocation.lstPerParticipantAllocation _
                                Where x.AllocDetails.TotalEnergyPayment <> 0 _
                                Select x.Participant.IDNumber Distinct Order By IDNumber Ascending).ToList

        Dim _chkParticipant = (From x In PaymentAllocation.lstPerSummaryAllocation _
                               Where x.WESMBillSummary.ChargeType = EnumChargeType.E _
                               Select x.WESMBillSummary.IDNumber.IDNumber Distinct Order By IDNumber Ascending).ToList

        _ParticipantList.Clear()
        For Each itmCheck In _chkParticipant
            Dim _itmCheck = itmCheck
            Dim _GetTotal = (From x In PaymentAllocation.lstPerSummaryAllocation _
                             Where x.WESMBillSummary.IDNumber.IDNumber = _itmCheck _
                             And x.WESMBillSummary.ChargeType = EnumChargeType.E _
                             Select x.PaymentAmount).Sum

            If _GetTotal <> 0 Then
                _ParticipantList.Add(itmCheck)
            End If



        Next

        'Get Participant's Total Allocation then to table
        For Each itmParticipant In _ParticipantList
            Dim _itmParticipant = itmParticipant

            'get Total Allocated Payment of Participant
            Dim TotalAllocated = (From x In PaymentAllocation.ParticipantForEFT _
                                  Where x.Participant.IDNumber = _itmParticipant _
                                  Select x.TotalPayment).Sum - (From x In PaymentAllocation.ParticipantForEFT _
                                                                Where x.Participant.IDNumber = _itmParticipant _
                                                                Select x.VAT).Sum - (From x In PaymentAllocation.ParticipantForEFT _
                                                                                     Where x.Participant.IDNumber = _itmParticipant _
                                                                                     Select x.ExcessCollection).Sum - (From x In PaymentAllocation.ParticipantForEFT _
                                                                                                                       Where x.Participant.IDNumber = _itmParticipant _
                                                                                                                       Select x.STLInterest).Sum - (From x In PaymentAllocation.ParticipantForEFT _
                                                                                                                                            Where x.Participant.IDNumber = _itmParticipant _
                                                                                                                                            Select x.NSSInterest).Sum - (From x In PaymentAllocation.ParticipantForEFT _
                                                                                                                                                                         Where x.Participant.IDNumber = _itmParticipant _
                                                                                                                                                                         And x.MarketFees > 0 _
                                                                                                                                                                         Select x.MarketFees).Sum





            Dim ShortName = (From x In PaymentAllocation.lstPerParticipantAllocation _
                             Where x.Participant.IDNumber = _itmParticipant _
                             Select x.Participant.ParticipantID).FirstOrDefault
            If Math.Round(TotalAllocated, 2) > 0 Then
                dgv_TransferToPR.Rows.Add(_itmParticipant, ShortName, _
                                      TotalAllocated.ToString("#,##0.00"), 0)
            End If

        Next

    End Sub

    'Close Form
    Private Sub cmd_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Close.Click
        frmPayment.cmd_CommitAllocation.Enabled = True
        Me.Close()
    End Sub

    'Editing of Transfer to PR Cell
    Private Sub dgv_TransferToPR_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_TransferToPR.CellEndEdit
        Try
            With Me.dgv_TransferToPR.Rows(e.RowIndex)
                If e.ColumnIndex = 3 Then
                    If Not IsNumeric(.Cells("hAMTForPR").Value) Then
                        MsgBox("Only Accepts Numeric input", MsgBoxStyle.Critical, "Error")
                        .Cells("hAMTForPR").Value = "0.00"
                        Exit Sub
                    End If

                    Me.CheckInputAmount(CDec(.Cells("hAllocatedPayment").Value), CDec(.Cells("hAMTForPR").Value))
                    .Cells("hAMTForPR").Value = FormatNumber(.Cells("hAMTForPR").Value, 2)
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            With Me.dgv_TransferToPR.Rows(e.RowIndex)
                .Cells("hAMTForPR").Value = "0.00"
            End With
        End Try
    End Sub

    'To Check if the Input amount is valid
    Private Sub CheckInputAmount(ByVal AllocatedPayment As Decimal, ByVal TransfertoPR As Decimal)
        If TransfertoPR < 0 Then
            Throw New ApplicationException("Amount for Transfer is smaller than Zero.")
        End If

        If TransfertoPR > AllocatedPayment Then
            Throw New ApplicationException("Amount for Transfer is bigger than the Allocated Amount.")
        End If
    End Sub

    'Start Transfer of PR and Generation of PR Entries
    'Compute for the total payment to participant
    Private Sub cmd_Transfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Transfer.Click
        Dim msgAns As New MsgBoxResult
        Dim tStart As New DateTime

        Try
            msgAns = MsgBox("Do you really want to save the Payment Allocations?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "Payment Allocation")
            If msgAns = MsgBoxResult.No Then
                Me.Close()
                frmPayment.cmd_CommitAllocation.Enabled = True
                Exit Sub
            Else
                frmProgress.Show()

                DocSignatures = WBillHelper.GetSignatories()
                'FTF Signatories
                Dim compTotal As Decimal = 0
                Dim _FTFMaxCounter As Long = 0

                If PaymentAllocation.FundTransferform.Count <> 0 Then
                    'Get FTF Max counter
                    _FTFMaxCounter = (From x In PaymentAllocation.FundTransferform _
                                          Select x.RefNo).Max
                End If

                'Create FTF Here
                Dim FTFMain As New FundTransferFormMain
                With FTFMain
                    .DRDate = DateTime.Now
                    .CRDate = DateTime.Now
                    .RefNo = _FTFMaxCounter + 1
                    .TransType = EnumFTFTransType.Replenishment
                    .AllocationDate = PaymentAllocation.AllocationDate
                    Dim dmcmctr As Long = 0
                    For x = 0 To dgv_TransferToPR.Rows.Count - 1
                        If CInt(dgv_TransferToPR.Rows(x).Cells("hAMTforPR").Value.ToString) <> 0 Then
                            'get EFT List of Participant
                            Dim curParticipant = CStr(dgv_TransferToPR.Rows(x).Cells("hParticipantID").Value.ToString)
                            Dim curPrudentialAmount = CDec(dgv_TransferToPR.Rows(x).Cells("hAMTforPR").Value.ToString)

                            Dim cParticipantEFT = (From y In PaymentAllocation.ParticipantForEFT _
                                                   Where y.Participant.IDNumber = curParticipant _
                                                   Select y).FirstOrDefault
                            cParticipantEFT.TransferPrudential = curPrudentialAmount
                            compTotal += curPrudentialAmount
                            .ListOfFTFParticipants.Add(New FundTransferFormParticipant(.RefNo, cParticipantEFT.Participant, curPrudentialAmount))

                            'Create DMCM For PR
                            Dim _PRDMCM As New DebitCreditMemo
                            With _PRDMCM
                                .Vatable = curPrudentialAmount
                                .TotalAmountDue = curPrudentialAmount
                                .Particulars = "Transfer of Payment of Participant to Prudential Requirements"
                                .ChargeType = EnumChargeType.E
                                .IDNumber = cParticipantEFT.Participant.IDNumber
                                .TransType = EnumDMCMTransactionType.PaymentPrudentialReplenishment
                                If Me.PaymentAllocation.lstDebitCreditMemo.Count <> 0 Then
                                    .DMCMNumber = (Me.PaymentAllocation.lstDebitCreditMemo.Max(Function(z As DebitCreditMemo) z.DMCMNumber) + 1)
                                Else
                                    .DMCMNumber = dmcmctr
                                    dmcmctr += 1
                                End If


                                .DMCMDetails.Add(New DebitCreditMemoDetails(.DMCMNumber, DebitCode, curPrudentialAmount, 0))
                                .DMCMDetails.Add(New DebitCreditMemoDetails(.DMCMNumber, PRWESMCode, 0, curPrudentialAmount))

                            End With
                            Me.PaymentAllocation.lstDebitCreditMemo.Add(_PRDMCM)

                            Dim _PRDMCMSummary As New DebitCreditMemoSummary
                            With _PRDMCMSummary
                                .DMCMDate = SystemDate
                                .DMCMDetails = _PRDMCM
                                .DMCMNo = _PRDMCM.DMCMNumber
                                .DMCMSummaryType = EnumDMCMSummaryType.DMCMSummary
                                .IDNumber = cParticipantEFT.Participant.IDNumber
                                .PaymentBatchCode = PaymentAllocation.PaymentbatchCode
                            End With
                            PaymentAllocation.DMCMSummary.Add(_PRDMCMSummary)

                            Me.CreatePREntries(cParticipantEFT.Participant, curPrudentialAmount, EnumPrudentialTransType.Replenishment)
                        End If
                    Next

                    .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, DebitCode, SettlementBankAccountNo, compTotal, 0))
                    .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, CreditCode, PrudentialBankAccountNo, 0, compTotal))
                    .RequestingApproval = "_Ariston P. Martinez"
                    .ApprovedBy = "Marissa P. Gandia"
                    .TotalAmount = compTotal
                    .Status = EnumStatus.Active
                End With

                If compTotal <> 0 Then
                    PaymentAllocation.FundTransferform.Add(FTFMain)
                End If

                Me.ComputeTotalPaymentToParticipant()

                Me.CreateRFP()

                'Add Applied Deferred to JV-1 of payment allocation
                Dim jv1PaymentAlloc As New JournalVoucher
                jv1PaymentAlloc = (From x In PaymentAllocation.lstJournalVoucher _
                                   Where x.PostedType = EnumPostedType.P.ToString _
                                   Select x).FirstOrDefault

                'Get Applied Deferred
                Dim _DeferredCheck = (From x In PaymentAllocation.lstPerSummaryAllocation _
                                  Where x.PaymentType = EnumPaymentType.DeferredAppliedEnergy _
                                  Or x.PaymentType = EnumPaymentType.DeferredAppliedVAT _
                                  Select x).ToList

                Dim _sumDeferred = (From x In _DeferredCheck _
                                   Select x.PaymentAmount).Sum

                If _sumDeferred <> 0 Then
                    With jv1PaymentAlloc
                        .JVDetails.Add(New JournalVoucherDetails(.JVNumber, DeferredPaymentCode, _sumDeferred, 0))
                        'Update total
                        Dim _forUpdate = (From x In .JVDetails _
                                          Where x.AccountCode = ClearingAccountCode _
                                          Select x).FirstOrDefault
                        _forUpdate.Credit += _sumDeferred
                    End With
                End If

                'Add additional entries for NSS in JV-1 of Payment Allocation 01272014
                'Check NSS if + of Negative (NET of all Billing Period)
                Dim chkNSS = (From x In PaymentAllocation.lstPerBillPeriodAllocation _
                              Select x.Total_NSSRA).Sum

                If chkNSS < 0 Then
                    With jv1PaymentAlloc
                        .JVDetails.Add(New JournalVoucherDetails(.JVNumber, CashinBankAccountSurplusCode, chkNSS, 0))
                        .JVDetails.Add(New JournalVoucherDetails(.JVNumber, CashInbankSettlementcode, 0, chkNSS))

                        'Dim _ForUpdateAP = (From x In .JVDetails _
                        '                    Where x.AccountCode = DebitCode _
                        '                    Select x).FirstOrDefault
                        '_ForUpdateAP.Debit -= Math.Abs(chkNSS)

                        'Dim _ForUpdateClr = (From x In .JVDetails _
                        '                     Where x.AccountCode = ClearingAccountCode _
                        '                     Select x).FirstOrDefault
                        '_ForUpdateClr.Credit -= Math.Abs(chkNSS)
                    End With
                Else
                    With jv1PaymentAlloc
                        .JVDetails.Add(New JournalVoucherDetails(.JVNumber, CashInbankSettlementcode, chkNSS, 0))
                        .JVDetails.Add(New JournalVoucherDetails(.JVNumber, CashinBankAccountSurplusCode, 0, chkNSS))

                        'Dim _ForUpdateAP = (From x In .JVDetails _
                        '                    Where x.AccountCode = DebitCode _
                        '                    Select x).FirstOrDefault
                        '_ForUpdateAP.Debit += Math.Abs(chkNSS)

                        'Dim _ForUpdateClr = (From x In .JVDetails _
                        '                     Where x.AccountCode = ClearingAccountCode _
                        '                     Select x).FirstOrDefault
                        '_ForUpdateClr.Credit += Math.Abs(chkNSS)
                    End With
                End If

                'End NSS  01272014

                If PaymentAllocation.lstPerBillPeriodAllocation.Count = 0 Then
                    Dim _ForPaymentAllocation As New Payment
                    With _ForPaymentAllocation
                        .CollectionAllocationDate = PaymentAllocation.AllocationDate
                        .PaymentAllocationDate = PaymentAllocation.AllocationDate
                        .PaymentBatchCode = "1"
                        .PaymentDate = PaymentAllocation.AllocationDate
                        .PaymentPerBPNo = "1"
                    End With
                    PaymentAllocation.lstPerBillPeriodAllocation.Add(_ForPaymentAllocation)
                End If

                Me.CreateJournalVoucher()

                Dim chkJournalVoucher = (From x In PaymentAllocation.lstJournalVoucher _
                                         Select x).ToList

                '  MsgBox(DateTime.Now.ToString)
                WBillHelper.SavePaymentAllocation(PaymentAllocation)
                ' MsgBox(Now.ToString & " xxx " & FormatDateTime(Date.Now, DateFormat.LongTime))
                frmProgress.Close()
                MsgBox("Successfully Saved!", MsgBoxStyle.Information, "Success")
                MainPaymentForm.RefreshForm()
                MainPaymentForm.dgv_CollectionDetails.DataSource = Nothing
                MainPaymentForm.dgv_PaymentSummary.DataSource = Nothing
                MainPaymentForm.cbo_CollectionAllocDate.Items.Clear()
                MainPaymentForm.LoadComboItems(WBillHelper, frmPayment.cbo_CollectionAllocDate)
                MainPaymentForm.txt_CollectionTotal.Text = "0.00"
                MainPaymentForm.txt_NSSApplied.Text = "0.00"
                MainPaymentForm.txt_tDeferredApplied.Text = "0.00"
                MainPaymentForm.txt_tDefaultCol.Text = "0.00"
                MainPaymentForm.txt_tEnergyCol.Text = "0.00"
                MainPaymentForm.txt_tVATCol.Text = "0.00"
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            frmProgress.Close()
            Me.Close()
            Exit Sub
        End Try
    End Sub

    'Generation of Data to be placed in AM_PEMC_PAYMENT
    Private Sub ComputeTotalPaymentToParticipant()
        Dim GetPEMCPayment = WBillHelper.GetAMPEMCPaymentForEFT(PaymentAllocation.AllocationDate)

        'Craete FTF of NSS Interest to Participant
        Dim _FTFNSSInterest As New FundTransferFormMain
        With _FTFNSSInterest
            .DRDate = DateTime.Now
            .CRDate = DateTime.Now

            If PaymentAllocation.FundTransferform.Count <> 0 Then
                .RefNo = (From x In PaymentAllocation.FundTransferform _
                      Select x.RefNo).Max + 1
            Else
                .RefNo = 0
            End If

            .TransType = EnumFTFTransType.TransferNSSToSTL
            .AllocationDate = PaymentAllocation.AllocationDate

            Dim TotalNSSInterest As Decimal = 0

            For Each itmForEFT In PaymentAllocation.ParticipantForEFT
                Dim _itmForEFT = itmForEFT
                Dim _chkParticipant = (From x In GetPEMCPayment _
                                       Where x.Participant.IDNumber = _itmForEFT.Participant.IDNumber _
                                       Select x).FirstOrDefault

                itmForEFT.TotalPayment -= (itmForEFT.TransferPrudential)

                If itmForEFT.NSSInterest <> 0 Then
                    .ListOfFTFParticipants.Add(New FundTransferFormParticipant(.RefNo, itmForEFT.Participant, itmForEFT.NSSInterest))
                    TotalNSSInterest += itmForEFT.NSSInterest
                End If

                If itmForEFT.TotalPayment <= DeferredPayment Then
                    itmForEFT.Participant.DeferredPaymentEnergy = itmForEFT.Energy + itmForEFT.MarketFees - itmForEFT.TransferPrudential
                    itmForEFT.Participant.DeferredPaymentEnergyVat = itmForEFT.VAT
                    PaymentAllocation.lstDeferredPayment.Add(itmForEFT.Participant)
                Else
                    If Not _chkParticipant Is Nothing Then
                        itmForEFT.AllocationDate = PaymentAllocation.AllocationDate
                        itmForEFT.Participant = _chkParticipant.Participant
                        PaymentAllocation.ForEFTUpdate.Add(itmForEFT)
                    Else
                        PaymentAllocation.ForEFTNew.Add(itmForEFT)
                    End If
                End If
            Next

            For Each itmForEFT In GetPEMCPayment.Where(Function(x) x.TotalPayment = 0)
                Dim _itmforeft = itmForEFT

                Dim _chkUpdate = (From x In PaymentAllocation.ForEFTUpdate _
                                  Where x.Participant.IDNumber = _itmforeft.Participant.IDNumber _
                                  Select x).FirstOrDefault

                If Not _chkUpdate Is Nothing Then
                    Continue For
                End If

                Dim chkNew = (From x In PaymentAllocation.ForEFTNew _
                              Where x.Participant.IDNumber = _itmforeft.Participant.IDNumber _
                              Select x).FirstOrDefault

                If Not _chkUpdate Is Nothing Then
                    Continue For
                End If

                If itmForEFT.Participant.IDNumber = CStr(1207) Then
                    Dim z = 0
                End If

                itmForEFT.TotalPayment = itmForEFT.STLInterest + itmForEFT.NSSInterest - itmForEFT.TransferPrudential + itmForEFT.ExcessCollection
                TotalNSSInterest += itmForEFT.NSSInterest
                itmForEFT.AllocationDate = PaymentAllocation.AllocationDate
                PaymentAllocation.ForEFTUpdate.Add(itmForEFT)

                If itmForEFT.NSSInterest <> 0 Then
                    .ListOfFTFParticipants.Add(New FundTransferFormParticipant(.RefNo, itmForEFT.Participant, itmForEFT.NSSInterest))
                    TotalNSSInterest += itmForEFT.NSSInterest
                End If

                If itmForEFT.TotalPayment <= DeferredPayment Then
                    itmForEFT.AllocationDate = PaymentAllocation.AllocationDate
                    itmForEFT.Participant.DeferredPaymentEnergy = itmForEFT.TotalPayment
                    PaymentAllocation.lstDeferredPayment.Add(itmForEFT.Participant)
                End If
            Next

            .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, DebitCode, SettlementBankAccountNo, TotalNSSInterest, 0))
            .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, CreditCode, NSSCode, 0, TotalNSSInterest))
            .TotalAmount = TotalNSSInterest
            If TotalNSSInterest <> 0 Then
                PaymentAllocation.FundTransferform.Add(_FTFNSSInterest)
            End If

        End With

    End Sub

    'Create PR Entries (Monitoring and History)
    Private Sub CreatePREntries(ByVal Participant As AMParticipants, ByVal AmountToPR As Decimal, ByVal PRTransactionType As EnumPrudentialTransType)

        'Update Participant's PR
        Dim forPRChanges = (From x In _PRRequirements _
                            Where x.IDNumber = Participant.IDNumber _
                            Select x).FirstOrDefault

        Dim PRReq As New Prudential
        If forPRChanges Is Nothing Then
            With PRReq
                .IDNumber = Participant.IDNumber
                .InterestAmount = 0
                .PrudentialAmount = AmountToPR
            End With
            PaymentAllocation.PrudentialReq.Add(PRReq)
        Else
            forPRChanges.PrudentialAmount += AmountToPR
            PaymentAllocation.PrudentialReq.Add(forPRChanges)
        End If

        'Create PR History
        Dim forPRHistory As New PrudentialHistory
        With forPRHistory
            .Amount = AmountToPR
            .IDNumber = Participant
            .TransType = PRTransactionType
            .TransDate = DateTime.Now()
        End With
        PaymentAllocation.PrudentialHistory.Add(forPRHistory)
    End Sub

    'Create Entries for RFP
    Private Sub CreateRFP()
        Dim _GetAllParticipants = WBillHelper.GetAMParticipantsAll()
        Dim _GetSignatories = WBillHelper.GetSignatories("RFP").FirstOrDefault

        Dim ForRFP As New RequestForPayment

        Dim _ColMonitoring = WBillHelper.GetCollectionMonitoring(CDate(PaymentAllocation.AllocationDate))

        Dim chkSumHeldCollection = (From x In _ColMonitoring _
                      Where x.TransType = EnumCollectionMonitoringType.TransferToHeldCollection _
                      And x.CollectionNoTag = 0 _
                      Select x.Amount).Sum

        Dim chkSumPEMCAccount = (From x In _ColMonitoring _
                                 Where x.TransType = EnumCollectionMonitoringType.TransferToPEMCAccount _
                                 Select x.Amount).Sum

        Dim chkSumExcessCollection = (From x In _ColMonitoring _
                                       Where x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection _
                                       Select x.Amount).Sum

        Dim RFPRefNo As Long = 1
        With ForRFP
            .AllocationDate = PaymentAllocation.AllocationDate
            .ReferenceNo = RFPRefNo
            .toRFP = ""
            .FromRFP = ""
            .PurposeOfPayment = ""
            .ReviewedBy = _GetSignatories.Signatory_1
            .ApprovedBy = _GetSignatories.Signatory_2
            .PaymentDate = CDate(FormatDateTime(PaymentAllocation.AllocationDate, DateFormat.ShortDate))

            'Get Application of Market Fees in Payment 
            Dim MFOffsetting = (From x In PaymentAllocation.lstPerSummaryAllocation _
                                Where (x.PaymentType = EnumPaymentType.UnpaidMF _
                                       Or x.PaymentType = EnumPaymentType.UnpaidMFV _
                                       Or x.PaymentType = EnumPaymentType.UnpaidMFDefault _
                                       Or x.PaymentType = EnumPaymentType.UnpaidMFVDefault) _
                                And x.PaymentAmount < 0 _
                                Select x.PaymentAmount).ToList

            Dim _WithholdAmount = (From x In PaymentAllocation.lstPerSummaryAllocation _
                        Where x.PaymentType = EnumPaymentType.WHTaxDefault _
                        Or x.PaymentType = EnumPaymentType.WHVATDefault _
                        Or x.PaymentType = EnumPaymentType.UnpaidMFWHTax _
                        Or x.PaymentType = EnumPaymentType.UnpaidMFWHVAT _
                        Select x.PaymentAmount).Sum

            Dim _MFOffsetting As Decimal = 0
            For Each itmMFOffseting In MFOffsetting
                _MFOffsetting += CDec(FormatNumber(itmMFOffseting, 2))
            Next
            _MFOffsetting += _WithholdAmount

            Dim _tmpFTF = WBillHelper.GetFundTransferForm(PaymentAllocation.AllocationDate, PaymentAllocation.AllocationDate, EnumFTFTransType.TransferMarketFeesToPEMC)
            Dim MFCollection As Decimal = _tmpFTF.Sum(Function(x As FundTransferFormMain) x.TotalAmount)

            If Math.Abs(_MFOffsetting) + Math.Abs(MFCollection) <> 0 Then
                .MarketFees = Math.Abs(_MFOffsetting) + Math.Abs(MFCollection)
            End If


            .NSSAmount = Math.Abs((From x In PaymentAllocation.lstPerBillPeriodAllocation _
                          Where x.Total_NSSRA < 0 _
                          Select x.Total_NSSRA).Sum) '- (From x In PaymentAllocation.lstPerBillPeriodAllocation _
            '                             Where x.NSSBalance < 0 _
            '                             Select x.NSSBalance).Sum)

            'If PaymentAllocation.lstPerBillPeriodAllocation.Sum(Function(x As Payment) x.Total_NSSRA - x.NSSBalance) < 0 Then
            '    .NSSAmount = Math.Abs(PaymentAllocation.lstPerBillPeriodAllocation.Sum(Function(x As Payment) x.Total_NSSRA - x.NSSBalance))
            'End If

            'Get FTF For transfer to PR
            Dim PRReplenish = (From x In PaymentAllocation.FundTransferform _
                               Where x.TransType = EnumFTFTransType.Replenishment _
                               Select x.TotalAmount).Sum

            Dim _TmpReplenishment = WBillHelper.GetFundTransferForm(PaymentAllocation.AllocationDate, PaymentAllocation.AllocationDate, EnumFTFTransType.Replenishment)
            Dim totReplenishment As Decimal = _TmpReplenishment.Sum(Function(x As FundTransferFormMain) x.TotalAmount)

            If PRReplenish + totReplenishment <> 0 Then
                .PRReplenishment = PRReplenish + totReplenishment
            End If

            If chkSumHeldCollection <> 0 Then
                .HeldCollection = chkSumHeldCollection
            End If

            If chkSumPEMCAccount <> 0 Then
                .TransferToPEMC = chkSumPEMCAccount
            End If

        End With
        PaymentAllocation.RequestForPayment.Add(ForRFP)

        Dim GetParticipantsForRFP = (From x In PaymentAllocation.ParticipantForEFT _
                                     Select x).ToList

        'Payment RFP
        For Each itmParticipant In GetParticipantsForRFP

            Dim RFPDetails As New RequestForPaymentDetails
            With RFPDetails
                .Amount = itmParticipant.TotalPayment
                .Participant = itmParticipant.Participant.IDNumber.ToString
                .ReferenceNo = RFPRefNo
                .AllocationDate = itmParticipant.AllocationDate
                'Temporary date of deposit
                .DateOfDeposit = FormatDateTime(DateTime.Now, DateFormat.ShortDate)
                If itmParticipant.TotalPayment <= DeferredPayment Then
                    .Particulars = "Set as Deferred Payment"
                ElseIf itmParticipant.TotalPayment > DeferredPayment Then

                    If itmParticipant.Energy <> 0 Then
                        .Particulars &= "Energy; "
                    End If

                    Dim _chkDefault = (From x In PaymentAllocation.lstPerParticipantAllocationViewing _
                                       Where x.Participant.IDNumber = CStr(.Participant) _
                                       Select x.AllocDetails.DefaultInterest).Sum
                    If _chkDefault <> 0 Then
                        .Particulars &= "Default Interest; "
                    End If

                    If itmParticipant.VAT <> 0 Then
                        .Particulars &= "VAT; "
                    End If

                    If itmParticipant.MarketFees > 0 Then
                        .Particulars &= "Market Fees; "
                    End If

                    If itmParticipant.ExcessCollection <> 0 Then
                        .Particulars &= "Excess Collection; "
                    End If

                    If itmParticipant.NSSInterest <> 0 Then
                        .Particulars &= "NSS Interest; "
                    End If

                    If itmParticipant.STLInterest <> 0 Then
                        .Particulars &= "Settlement Interest; "
                    End If

                    If .Particulars.Length <> 0 Then
                        .Particulars = Trim(Mid(.Particulars, 1, Len(.Particulars) - 2))
                    End If

                End If
            End With
            If RFPDetails.Amount <> 0 Then
                PaymentAllocation.RFPDetails.Add(RFPDetails)
            End If
        Next

        'Check NSS/STL/Prudential Replenishment
        'Get Distinct Participants in Collection
        Dim ParticipantListCollection = (From x In PaymentAllocation.lstCollection _
                                         Select x.IDNumber Distinct).ToList

        For Each itmParticipant In ParticipantListCollection
            Dim _itmParticipant = itmParticipant
            Dim cParticipant = (From x In _GetAllParticipants _
                                Where x.IDNumber = _itmParticipant _
                                Select x).FirstOrDefault

            If cParticipant Is Nothing Then
                Throw New Exception("Cannot find Settlement ID: " & _itmParticipant & " in the database")
                Exit Sub
            End If

            Dim GetParticipantCollection = (From x In PaymentAllocation.lstCollection _
                                            Where x.IDNumber = _itmParticipant _
                                            And x.CollectionCategory = EnumCollectionCategory.Cash _
                                            Select x).ToList

            For Each itmCollection In GetParticipantCollection
                Dim ForRemarks As String = ""
                Dim _itmCollection = itmCollection
                'Generate Remarks 
                Dim lstRemarks = (From x In itmCollection.ListOfCollectionAllocation _
                                  Where x.CollectionType = EnumCollectionType.Energy _
                                  Or x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy _
                                  Or x.CollectionType = EnumCollectionType.VatOnEnergy _
                                  Or x.CollectionType = EnumCollectionType.MarketFees _
                                 Select x Distinct).ToList

                For Each itmRemarks In lstRemarks
                    If itmRemarks.CollectionType = EnumCollectionType.Energy Then
                        'ColAmount += itmRemarks.Amount
                        If ForRemarks.Contains("Energy") = False Then
                            ForRemarks &= "Energy; "
                        End If
                    ElseIf itmRemarks.CollectionType = EnumCollectionType.DefaultInterestOnEnergy Then
                        'ColAmount += itmRemarks.Amount
                        If ForRemarks.Contains("Default Interest") = False Then
                            ForRemarks &= "Default Interest; "
                        End If
                    ElseIf itmRemarks.CollectionType = EnumCollectionType.VatOnEnergy Then
                        'ColAmount += itmRemarks.Amount
                        If ForRemarks.Contains("VAT") = False Then
                            ForRemarks &= "VAT; "
                        End If

                    ElseIf itmRemarks.CollectionType = EnumCollectionType.MarketFees Then
                        If ForRemarks.Contains("Market Fees;") = False Then
                            ForRemarks &= "Market Fees; "
                        End If
                    End If
                Next

                Dim chkTransferToPR = (From x In _ColMonitoring _
                                     Where x.CollectionNo = _itmCollection.CollectionNumber _
                                     And x.TransType = EnumCollectionMonitoringType.TransferToPRReplenishment _
                                     Select x.Amount).Sum

                If chkTransferToPR <> 0 Then
                    ForRemarks &= "PR Replenishment; "
                End If

                Dim chkTransferExcess = (From x In _ColMonitoring _
                                         Where x.CollectionNo = _itmCollection.CollectionNumber _
                                        And x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection _
                                        Select x.Amount).Sum

                If chkTransferExcess <> 0 Then
                    ForRemarks &= "Excess Collection; "
                End If

                Dim chkTransferHeld = (From x In _ColMonitoring _
                                       Where x.CollectionNo = _itmCollection.CollectionNumber _
                                       And x.TransType = EnumCollectionMonitoringType.TransferToHeldCollection _
                                       Select x.Amount).Sum

                If chkTransferHeld <> 0 Then
                    ForRemarks &= "Held Collection; "
                End If


                If ForRemarks.Length <> 0 Then
                    ForRemarks = Trim(Mid(ForRemarks, 1, Len(ForRemarks) - 2))
                End If

                'Dim _AppliedCollectionHeld = (From x In _ColMonitoring _
                '                      Where x.CollectionNo = _itmCollection.CollectionNumber _
                '                      And x.TransType = EnumCollectionMonitoringType.AppliedHeldCollection _
                '                      Select x.Amount).Sum


                'If itmCollection.CollectedAmount + _AppliedCollectionHeld = 0 Then
                '    Continue For
                'End If

                PaymentAllocation.RFPDetails.Add(New RequestForPaymentDetails(RFPRefNo, _
                                                                          CStr(cParticipant.IDNumber), _
                                                                           Math.Abs(itmCollection.CollectedAmount) * -1D, _
                                                                          FormatDateTime(itmCollection.CollectionDate, DateFormat.ShortDate), ForRemarks, PaymentAllocation.AllocationDate, _
                                                                          cParticipant.PaymentType)) '+ AppliedCollectionHeld on itmcollection.collectedamount
            Next
        Next

        'Get Application of Market Fees in Payment (Refund)
        Dim _MFSum = (From x In PaymentAllocation.lstPerSummaryAllocation _
                            Where (x.PaymentType = EnumPaymentType.UnpaidMF _
                            Or x.PaymentType = EnumPaymentType.UnpaidMFDefault _
                            Or x.PaymentType = EnumPaymentType.UnpaidMFV _
                            Or x.PaymentType = EnumPaymentType.UnpaidMFVDefault) _
                            And x.PaymentAmount > 0 _
                            Select x.PaymentAmount).Sum

        Dim _MFSumList = (From x In PaymentAllocation.lstPerSummaryAllocation _
                            Where (x.PaymentType = EnumPaymentType.UnpaidMF _
                            Or x.PaymentType = EnumPaymentType.UnpaidMFDefault _
                            Or x.PaymentType = EnumPaymentType.UnpaidMFV _
                            Or x.PaymentType = EnumPaymentType.UnpaidMFVDefault) _
                            And x.PaymentAmount > 0 _
                            Select x.PaymentAmount).ToList

        If _MFSum > 0 Then
            PaymentAllocation.RFPDetails.Add(New RequestForPaymentDetails(RFPRefNo, _
                                                                           "Market Fees", _
                                                                          _MFSum, _
                                                                          CStr(PaymentAllocation.AllocationDate), "Market Fees Refund to Participants", PaymentAllocation.AllocationDate, _
                                                                          EnumParticipantPaymentType.None))
        End If

        'Get PR Drawdown
        Dim lstCollectionMonitoring = WBillHelper.GetCollectionMonitoring(PaymentAllocation.AllocationDate)
        Dim PRDrawdownSum = (From x In lstCollectionMonitoring _
                             Where x.TransType = EnumCollectionMonitoringType.TransferToPRDrawdown _
                             Select x.Amount).Sum

        'Get Sum of all DrawDown of PR
        If PRDrawdownSum <> 0 Then
            PaymentAllocation.RFPDetails.Add(New RequestForPaymentDetails(RFPRefNo, _
                                                                          "Prudential Requirement (Drawdown)", _
                                                                          PRDrawdownSum, _
                                                                          CStr(PaymentAllocation.AllocationDate), "Prudential Security Requirement - Drawdown", PaymentAllocation.AllocationDate, _
                                                                          EnumParticipantPaymentType.None))
        End If

        'Get NSS applied
        Dim NSSApplied = (From x In PaymentAllocation.lstPerBillPeriodAllocation _
                          Where x.Total_NSSRA > 0 _
                          Select x.Total_NSSRA).Sum
        If NSSApplied <> 0 Then
            PaymentAllocation.RFPDetails.Add(New RequestForPaymentDetails(RFPRefNo, _
                                                                          "NSSRA Applied", _
                                                                          NSSApplied, _
                                                                          CStr(PaymentAllocation.AllocationDate), "NSS Applied to Collection", PaymentAllocation.AllocationDate, _
                                                                          EnumParticipantPaymentType.None))
        End If

        'Get Deferred Applied
        Dim _DeferredCheck = (From x In PaymentAllocation.lstPerSummaryAllocation _
                              Where x.PaymentType = EnumPaymentType.DeferredAppliedEnergy _
                              Or x.PaymentType = EnumPaymentType.DeferredAppliedVAT _
                              Select x).ToList

        Dim _sumDeferred = (From x In _DeferredCheck _
                           Select x.PaymentAmount).Sum

        If _sumDeferred > 0 Then
            PaymentAllocation.RFPDetails.Add(New RequestForPaymentDetails(RFPRefNo, _
                                                                          "Deferred Payments - Current", _
                                                                          _sumDeferred, _
                                                                          CStr(PaymentAllocation.AllocationDate), "Deferred as of " & PaymentAllocation.AllocationDate, PaymentAllocation.AllocationDate, _
                                                                          EnumParticipantPaymentType.None))
        End If



        RFPRefNo += 1
    End Sub

#End Region

#Region "Create Journal Voucher for EFT/CHECKS/FTF "
    Public Sub CreateJournalVoucher()
        'Get Allocation from collection
        Dim TotalEnergy = (From x In PaymentAllocation.lstPerBillPeriodAllocation _
                  Select x.EnergyPayment).Sum

        Dim TotalDefault = (From x In PaymentAllocation.lstPerBillPeriodAllocation _
                            Select x.Total_DICollection).Sum

        Dim totalVAT = (From x In PaymentAllocation.lstPerBillPeriodAllocation _
                        Select x.Total_VATCollection).Sum

        Dim TotalNSSRA = (From x In PaymentAllocation.lstPerBillPeriodAllocation _
                        Select x.Total_NSSRA).Sum

        'Get All Clearing RMPM from DM/CM
        Dim DebitClearingAMT As Decimal = 0
        Dim CreditClearingAMT As Decimal = 0
        For Each itmDMCM In PaymentAllocation.lstDebitCreditMemo
            Dim chkClearingAMT = (From x In itmDMCM.DMCMDetails _
                                  Where x.AccountCode = AMModule.ClearingAccountCode _
                                  Select x).ToList 'And x.Debit <> 0 _

            If chkClearingAMT.Count = 0 Then
                Continue For
            Else
                For Each itmClearingAMT In itmDMCM.DMCMDetails
                    DebitClearingAMT += itmClearingAMT.Debit
                    CreditClearingAMT += itmClearingAMT.Credit
                Next
            End If
        Next

        'Get All Market Fees FTF in Payment
        Dim MFAmount = (From x In PaymentAllocation.FundTransferform _
                       Where x.TransType = EnumFTFTransType.TransferMarketFeesToPEMC _
                       Select x.TotalAmount).Sum

        'Get FTF from payment allocation
        Dim ColMFAmount = Me.WBillHelper.GetFundTransferForm(PaymentAllocation.AllocationDate, PaymentAllocation.AllocationDate, EnumFTFTransType.TransferMarketFeesToPEMC)
        Dim CollFTF = (From x In ColMFAmount _
                      Select x.TotalAmount).Sum()

        MFAmount += CollFTF

        'Get MF Refund in Payment
        Dim MFRefund = (From x In PaymentAllocation.FundTransferform _
                        Where x.TransType = EnumFTFTransType.TransferMarketFeesToSTL _
                        Select x.TotalAmount).Sum

        'Check Transfer to PR
        Dim AMTPRTransfer = (From x In PaymentAllocation.PrudentialReq _
                             Where x.PrudentialAmount > 0 _
                             Select x.PrudentialAmount + x.InterestAmount).Sum()

        'Get Excess Collection
        Dim GetCollectionAllocation = WBillHelper.GetCollectionMonitoring(PaymentAllocation.AllocationDate)
        Dim AmountExcess = (From x In GetCollectionAllocation _
                            Where x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection _
                            Select x.Amount).Sum

        'Get Current Deferred
        Dim GetCurrentDeferred = (From x In PaymentAllocation.ParticipantForEFT _
                                  Where x.TotalPayment < DeferredPayment _
                                  Select x).ToList
        Dim AmountDeferred = (From x In GetCurrentDeferred _
                              Select x.TotalPayment).Sum

        'Get Previous Deferred
        Dim GetAppliedDeferred = (From x In PaymentAllocation.lstPerSummaryAllocation _
                                  Where x.PaymentType = EnumPaymentType.DeferredAppliedEnergy _
                                  Or x.PaymentType = EnumPaymentType.DeferredAppliedVAT _
                                  Select x).ToList

        Dim AppliedDeferred = (From x In GetAppliedDeferred _
                               Select x.PaymentAmount).Sum

        Dim GTotalAllocation = TotalEnergy + TotalDefault + totalVAT + AMTPRTransfer + AmountExcess + AppliedDeferred + CollFTF

        Dim forEFTJV As New JournalVoucher
        With forEFTJV
            .BatchCode = "1"
            .JVDate = SystemDate
            If PaymentAllocation.lstJournalVoucher.Count = 0 Then
                .JVNumber = 1
            Else
                .JVNumber = (From x In PaymentAllocation.lstJournalVoucher _
                         Select x.JVNumber).Max + 1
            End If

            .PostedType = EnumPostedType.PEFT.ToString
            .Remarks = "To record payment through EFT/Checks/FTF for Allocation date " & PaymentAllocation.AllocationDate & "."

            Dim chktotal = GTotalAllocation - Math.Abs(MFAmount) - AMTPRTransfer

            If GTotalAllocation - Math.Abs(MFAmount) - AMTPRTransfer <> 0 Then
                If PaymentAllocation.lstJournalVoucher.Count = 0 Then
                    .JVDetails.Add(New JournalVoucherDetails(.JVNumber, AMModule.UnappliedcollectionCode, GTotalAllocation - Math.Abs(MFAmount) - AMTPRTransfer, 0))
                Else
                    .JVDetails.Add(New JournalVoucherDetails(.JVNumber, AMModule.ClearingAccountCode, GTotalAllocation - Math.Abs(MFAmount) - AMTPRTransfer, 0))
                End If
            End If

            If Math.Abs(MFAmount) <> 0 Then
                .JVDetails.Add(New JournalVoucherDetails(.JVNumber, AMModule.CashinBankPEMCCode, Math.Abs(MFAmount), 0))
            End If

            If AMTPRTransfer <> 0 Then
                .JVDetails.Add(New JournalVoucherDetails(.JVNumber, AMModule.CashinBankPrudentialCode, Math.Abs(AMTPRTransfer), 0))
            End If

            If Math.Abs(GTotalAllocation) <> 0 Then
                If MFRefund <> 0 Then
                    ' GTotalAllocation += MFRefund
                    .JVDetails.Add(New JournalVoucherDetails(.JVNumber, AMModule.CashInbankSettlementcode, Math.Abs(MFRefund), 0))
                    .JVDetails.Add(New JournalVoucherDetails(.JVNumber, AMModule.CashinBankPEMCCode, 0, Math.Abs(MFRefund)))
                End If
                .JVDetails.Add(New JournalVoucherDetails(.JVNumber, AMModule.CashInbankSettlementcode, 0, Math.Abs(GTotalAllocation - AmountDeferred)))
            End If

            If AmountDeferred <> 0 Then
                .JVDetails.Add(New JournalVoucherDetails(.JVNumber, DeferredPaymentCode, 0, AmountDeferred))
            End If

        End With
        If forEFTJV.JVDetails.Count <> 0 Then
            PaymentAllocation.lstJournalVoucher.Add(forEFTJV)
        End If
    End Sub
#End Region
End Class