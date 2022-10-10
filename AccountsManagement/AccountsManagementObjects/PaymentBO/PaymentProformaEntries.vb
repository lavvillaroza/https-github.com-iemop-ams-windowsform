Option Strict On
Public Class PaymentProformaEntries
    Private DMCMSeqNo As Long = 0
    Private JVSeqNo As Long = 0

    Private JVBatchCodeP As String = "P-001"
    Private JVBatchCodePA As String = "PA-002"
    Private JVBatchCodePEFT As String = "PEFT-003"


    Private _WESMBillGPPostedList As New List(Of WESMBillGPPosted)
    Public ReadOnly Property WESMBillGPPostedList() As List(Of WESMBillGPPosted)
        Get
            Return _WESMBillGPPostedList
        End Get
    End Property

#Region "JV Signatories"
    Private _JVSign As New DocSignatories
    Public Property JVSign() As DocSignatories
        Get
            Return _JVSign
        End Get
        Set(value As DocSignatories)
            _JVSign = value
        End Set
    End Property
#End Region

#Region "DMCM Signatories"
    Private _DMCMSign As New DocSignatories
    Public Property DMCMSign() As DocSignatories
        Get
            Return _DMCMSign
        End Get
        Set(value As DocSignatories)
            _DMCMSign = value
        End Set
    End Property
#End Region

#Region "JV Entry From Payment"
    Public Function GenerateJVFromCollection(ByVal CollDate As Date,
                                             ByVal TotalAmountColl As Decimal,
                                             ByVal ExcessCollectionAmount As Decimal,
                                             ByVal WithholdingTaxOnEnergy As Decimal) As JournalVoucher
        Me.JVSeqNo += 1

        Dim JVCollection As New JournalVoucher
        With JVCollection
            .JVDate = CollDate
            .JVNumber = Me.JVSeqNo
            .PostedType = EnumPostedType.P.ToString
            .Status = EnumPostedTypeStatus.NotPosted
            .BatchCode = Me.JVBatchCodeP
            .Remarks = "Allocated amount from Collection Allocation date " & CollDate & "."            
            .JVDetails.Add(New JournalVoucherDetails(.JVNumber, AMModule.DebitCode, TotalAmountColl - WithholdingTaxOnEnergy, 0))
            If WithholdingTaxOnEnergy <> 0 Then
                .JVDetails.Add(New JournalVoucherDetails(.JVNumber, AMModule.EWTPayable, WithholdingTaxOnEnergy, 0))
            End If
            If ExcessCollectionAmount <> 0 Then
                .JVDetails.Add(New JournalVoucherDetails(.JVNumber, AMModule.UnappliedcollectionCode, ExcessCollectionAmount, 0))
            End If
            .JVDetails.Add(New JournalVoucherDetails(.JVNumber, AMModule.ClearingAccountCode, 0, TotalAmountColl + ExcessCollectionAmount))
            .UpdatedBy = AMModule.UserName
            .PreparedBy = AMModule.FullName
            .CheckedBy = JVSign.Signatory_1
            .ApprovedBy = JVSign.Signatory_2
        End With

        Dim WESMBillGPPostedItem As New WESMBillGPPosted

        With WESMBillGPPostedItem
            .Remarks = JVCollection.Remarks
            .Posted = 0
            .BatchCode = JVCollection.BatchCode
            .PostType = JVCollection.PostedType
            .DocumentAmount = Math.Round((From x In JVCollection.JVDetails Select x.Debit).Sum, 2)
            .JVNumber = JVCollection.JVNumber
        End With

        Me._WESMBillGPPostedList.Add(WESMBillGPPostedItem)

        Return JVCollection
    End Function
#End Region

#Region "JV Entry From Payment Allocation"
    Public Function GenerateJVFromPaymentAlloc(ByVal AllocDate As Date,
                                               ByVal DMCMList As List(Of DebitCreditMemo),
                                               ByVal MFRefund As List(Of APAllocation)) As JournalVoucher
        Me.JVSeqNo += 1

        Dim JVPaymentAlloc As New JournalVoucher
        Dim JVPaymentAllocDic As New Dictionary(Of String, Integer)

        With JVPaymentAlloc
            .JVDate = AllocDate
            .JVNumber = Me.JVSeqNo
            .PostedType = EnumPostedType.PA.ToString
            .Status = EnumPostedTypeStatus.NotPosted
            .Remarks = "Payment Allocation details for Allocation date of " & AllocDate & "."
            .BatchCode = Me.JVBatchCodePA
            .UpdatedBy = AMModule.UserName
            .PreparedBy = AMModule.FullName
            .CheckedBy = JVSign.Signatory_1
            .ApprovedBy = JVSign.Signatory_2
        End With
        Dim ctrl As Integer = 0
        For Each DMCMItem In DMCMList.OrderBy(Function(x) x.DMCMNumber)
            Select Case DMCMItem.TransType
                Case EnumDMCMTransactionType.PaymentSetupClearingOnMFAndMFV
                    For Each _ItemDetails In DMCMItem.DMCMDetails
                        Dim Key As String = _ItemDetails.AccountCode & EnumDMCMTransactionType.PaymentSetupClearingOnMFAndMFV.ToString()
                        If Not JVPaymentAllocDic.ContainsKey(Key) Then
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, _ItemDetails.Debit, 0))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, 0, _ItemDetails.Credit))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            End If
                        Else
                            Dim DicItem As Integer = JVPaymentAllocDic(Key)
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Debit += _ItemDetails.Debit

                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Credit += _ItemDetails.Credit
                            End If
                        End If
                    Next
                Case EnumDMCMTransactionType.PaymentSetupClearingOnEWTAndWVAT
                    For Each _ItemDetails In DMCMItem.DMCMDetails
                        Dim Key As String = _ItemDetails.AccountCode & EnumDMCMTransactionType.PaymentSetupClearingOnEWTAndWVAT.ToString()
                        If Not JVPaymentAllocDic.ContainsKey(Key) Then
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, _ItemDetails.Debit, 0))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, 0, _ItemDetails.Credit))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            End If
                        Else
                            Dim DicItem As Integer = JVPaymentAllocDic(Key)
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Debit += _ItemDetails.Debit
                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Credit += _ItemDetails.Credit
                            End If
                        End If
                    Next               
                Case EnumDMCMTransactionType.PaymentSetupClearingOnMFAndMFVDefaultInterest
                    For Each _ItemDetails In DMCMItem.DMCMDetails
                        Dim Key As String = _ItemDetails.AccountCode & EnumDMCMTransactionType.PaymentSetupClearingOnMFAndMFVDefaultInterest.ToString()
                        If Not JVPaymentAllocDic.ContainsKey(Key) Then
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, _ItemDetails.Debit, 0))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, 0, _ItemDetails.Credit))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            End If
                        Else
                            Dim DicItem As Integer = JVPaymentAllocDic(Key)
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Debit += _ItemDetails.Debit

                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Credit += _ItemDetails.Credit
                            End If
                        End If
                    Next
                Case EnumDMCMTransactionType.PaymentSetupClearingOnEWTAndWVATDefaultInterest
                    For Each _ItemDetails In DMCMItem.DMCMDetails
                        Dim Key As String = _ItemDetails.AccountCode & EnumDMCMTransactionType.PaymentSetupClearingOnEWTAndWVATDefaultInterest.ToString()
                        If Not JVPaymentAllocDic.ContainsKey(Key) Then
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, _ItemDetails.Debit, 0))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, 0, _ItemDetails.Credit))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            End If
                        Else
                            Dim DicItem As Integer = JVPaymentAllocDic(Key)
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Debit += _ItemDetails.Debit
                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Credit += _ItemDetails.Credit
                            End If
                        End If
                    Next
                Case EnumDMCMTransactionType.PaymentSetupClearingAROnEnergyAndDefaultInterest, EnumDMCMTransactionType.PaymentSetupClearingAROnVATonEnergy
                    For Each _ItemDetails In DMCMItem.DMCMDetails
                        Dim Key As String = _ItemDetails.AccountCode & EnumDMCMTransactionType.PaymentSetupClearingAROnEnergyAndDefaultInterest.ToString() & EnumDMCMTransactionType.PaymentSetupClearingAROnVATonEnergy.ToString()
                        If Not JVPaymentAllocDic.ContainsKey(Key) Then
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, _ItemDetails.Debit, 0))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, 0, _ItemDetails.Credit))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            End If
                        Else
                            Dim DicItem As Integer = JVPaymentAllocDic(Key)
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Debit += _ItemDetails.Debit

                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Credit += _ItemDetails.Credit
                            End If
                        End If
                    Next
                Case EnumDMCMTransactionType.PaymentSetupOffsettingAROfDefaultInterestOnEnergy
                    For Each _ItemDetails In DMCMItem.DMCMDetails
                        Dim Key As String = _ItemDetails.AccountCode & EnumDMCMTransactionType.PaymentSetupOffsettingAROfDefaultInterestOnEnergy.ToString()
                        If Not JVPaymentAllocDic.ContainsKey(Key) Then
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, _ItemDetails.Debit, 0))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, 0, _ItemDetails.Credit))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            End If
                        Else
                            Dim DicItem As Integer = JVPaymentAllocDic(Key)
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Debit += _ItemDetails.Debit
                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Credit += _ItemDetails.Credit
                            End If
                        End If
                    Next
                Case EnumDMCMTransactionType.PaymentSetupClearingAPOfEnergyAndDefaultInterest, EnumDMCMTransactionType.PaymentSetupClearingAPOfVATonEnergy
                    For Each _ItemDetails In DMCMItem.DMCMDetails
                        Dim Key As String = _ItemDetails.AccountCode & EnumDMCMTransactionType.PaymentSetupClearingAPOfEnergyAndDefaultInterest.ToString() & "And" & EnumDMCMTransactionType.PaymentSetupClearingAPOfVATonEnergy
                        If Not JVPaymentAllocDic.ContainsKey(Key) Then
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, _ItemDetails.Debit, 0))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, 0, _ItemDetails.Credit))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            End If
                        Else
                            Dim DicItem As Integer = JVPaymentAllocDic(Key)
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Debit += _ItemDetails.Debit

                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Credit += _ItemDetails.Credit
                            End If
                        End If
                    Next
                Case EnumDMCMTransactionType.PaymentSetupOffsettingAPOfDefaultInterestOnEnergy
                    For Each _ItemDetails In DMCMItem.DMCMDetails
                        Dim Key As String = _ItemDetails.AccountCode & EnumDMCMTransactionType.PaymentSetupOffsettingAPOfDefaultInterestOnEnergy.ToString()
                        If Not JVPaymentAllocDic.ContainsKey(Key) Then
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, _ItemDetails.Debit, 0))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, 0, _ItemDetails.Credit))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            End If
                        Else
                            Dim DicItem As Integer = JVPaymentAllocDic(Key)
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Debit += _ItemDetails.Debit
                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Credit += _ItemDetails.Credit
                            End If
                        End If
                    Next
                Case EnumDMCMTransactionType.PaymentSetupEnergyDefaultInterest
                    For Each _ItemDetails In DMCMItem.DMCMDetails
                        Dim Key As String = _ItemDetails.AccountCode & EnumDMCMTransactionType.PaymentSetupEnergyDefaultInterest.ToString()
                        If Not JVPaymentAllocDic.ContainsKey(Key) Then
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, _ItemDetails.Debit, 0))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, 0, _ItemDetails.Credit))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            End If
                        Else
                            Dim DicItem As Integer = JVPaymentAllocDic(Key)
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Debit += _ItemDetails.Debit

                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Credit += _ItemDetails.Credit
                            End If
                        End If
                    Next
                Case EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAROnEnergyAddBack
                    For Each _ItemDetails In DMCMItem.DMCMDetails
                        Dim Key As String = _ItemDetails.AccountCode & EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAROnEnergyAddBack.ToString()
                        If Not JVPaymentAllocDic.ContainsKey(Key) Then
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, _ItemDetails.Debit, 0))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, 0, _ItemDetails.Credit))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            End If
                        Else
                            Dim DicItem As Integer = JVPaymentAllocDic(Key)
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Debit += _ItemDetails.Debit

                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Credit += _ItemDetails.Credit
                            End If
                        End If
                    Next
                Case EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAPOnEnergyAddBack
                    For Each _ItemDetails In DMCMItem.DMCMDetails
                        Dim Key As String = _ItemDetails.AccountCode & EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAPOnEnergyAddBack.ToString()
                        If Not JVPaymentAllocDic.ContainsKey(Key) Then
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, _ItemDetails.Debit, 0))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, _ItemDetails.AccountCode, 0, _ItemDetails.Credit))
                                JVPaymentAllocDic.Add(Key, ctrl)
                                ctrl += 1
                            End If
                        Else
                            Dim DicItem As Integer = JVPaymentAllocDic(Key)
                            If _ItemDetails.Credit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Debit += _ItemDetails.Debit

                            ElseIf _ItemDetails.Debit = 0 Then
                                JVPaymentAlloc.JVDetails(DicItem).Credit += _ItemDetails.Credit
                            End If
                        End If
                    Next
            End Select
        Next

        Dim MFREfundBaseAmount As Decimal = (From x In MFRefund Select x.AllocationAmount).Sum

        If MFREfundBaseAmount <> 0 Then
            JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, AMModule.DebitCode, MFREfundBaseAmount, 0))
            JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, AMModule.ClearingAccountCode, 0, MFREfundBaseAmount))
        End If


        Dim MFRefundEWTAmount As Decimal = (From x In MFRefund _
                                               Where x.PaymentType = EnumPaymentNewType.WithholdingTaxOnMF _
                                               Select x.AllocationAmount).Sum

        Dim MFRefundEWVAmount As Decimal = (From x In MFRefund _
                                               Where x.PaymentType = EnumPaymentNewType.WithholdingVatOnMF _
                                               Select x.AllocationAmount).Sum

        If (MFRefundEWTAmount + MFRefundEWVAmount) <> 0 Then
            JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, AMModule.DebitCode, Math.Abs(MFRefundEWTAmount + MFRefundEWVAmount), 0))            
            If MFRefundEWTAmount <> 0 Then
                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, AMModule.EWTReceivable, 0, Math.Abs(MFRefundEWTAmount)))
            End If
            If MFRefundEWVAmount <> 0 Then
                JVPaymentAlloc.JVDetails.Add(New JournalVoucherDetails(JVPaymentAlloc.JVNumber, AMModule.EWVReceivableCode, 0, Math.Abs(MFRefundEWVAmount)))
            End If
        End If

        Dim WESMBillGPPostedItem As New WESMBillGPPosted

        With WESMBillGPPostedItem
            .Remarks = JVPaymentAlloc.Remarks
            .Posted = 0
            .BatchCode = JVPaymentAlloc.BatchCode
            .PostType = JVPaymentAlloc.PostedType
            .DocumentAmount = Math.Round((From x In JVPaymentAlloc.JVDetails Select x.Debit).Sum, 2)
            .JVNumber = JVPaymentAlloc.JVNumber
        End With

        Me._WESMBillGPPostedList.Add(WESMBillGPPostedItem)

        Return JVPaymentAlloc
    End Function
#End Region

#Region "JV Entry From Payment EFT/Check"
    Public Function GenerateJVFromPaymentEFTCheck(ByVal AllocDate As Date,
                                                  ByVal RFPList As RequestForPayment, _
                                                  ByVal PayFTFList As List(Of FundTransferFormMain), _
                                                  ByVal CollFTFList As List(Of FundTransferFormMain), _
                                                  ByVal PrevDeferredPayment As List(Of DeferredMain), _
                                                  ByVal CurrDeferredPayment As List(Of DeferredMain)) As JournalVoucher
        Me.JVSeqNo += 1
        Dim JVPaymentEFTCheck As New JournalVoucher

        With JVPaymentEFTCheck
            .JVDate = AllocDate
            .JVNumber = Me.JVSeqNo
            .PostedType = EnumPostedType.PEFT.ToString
            .Status = EnumPostedTypeStatus.NotPosted
            .Remarks = "To record payment through EFT/Checks/FTF for Allocation date " & AllocDate & "."
            .BatchCode = Me.JVBatchCodePEFT
            .UpdatedBy = AMModule.UserName
            .PreparedBy = AMModule.FullName
            .CheckedBy = JVSign.Signatory_1
            .ApprovedBy = JVSign.Signatory_2
        End With

        Dim TotalPayment = (From x In RFPList.RFPDetails _
                            Where x.RFPDetailsType = EnumRFPDetailsType.Payment _
                            Select x.Amount).Sum()

        'added by LAVV to remove JV EFT if the collection is for PR Replenishment only
        If TotalPayment = 0 Then
            JVPaymentEFTCheck.JVNumber = 0
        End If

        Dim SumOfPayFTFListMFRefund As Decimal = (From x In PayFTFList Where x.TransType = EnumFTFTransType.TransferMarketFeesToSTL Select x.TotalAmount).Sum
        Dim SumOfPayFTFListEWT As Decimal = (From x In PayFTFList Where x.TransType = EnumFTFTransType.EnergyWithholdingTax Select x.TotalAmount).Sum
        Dim MarketFees As Decimal = RFPList.MarketFees + SumOfPayFTFListMFRefund
        Dim SumOfPayFTFListTransToPR As Decimal = (From x In PayFTFList Where x.TransType = EnumFTFTransType.Replenishment Select x.TotalAmount).Sum
        Dim SumOfPrevDeferred As Decimal = (From x In PrevDeferredPayment Select x.OutstandingBalanceDeferredPayment).Sum()
        Dim SumOfCurrDeferred As Decimal = (From x In CurrDeferredPayment Select x.OutstandingBalanceDeferredPayment).Sum()

        If TotalPayment <> 0 Then
            TotalPayment -= SumOfPrevDeferred
            If TotalPayment <> 0 Then
                JVPaymentEFTCheck.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.ClearingAccountCode, TotalPayment + SumOfPayFTFListEWT, 0))                            
            End If
        End If

        If SumOfPayFTFListEWT <> 0 Then
            JVPaymentEFTCheck.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashInbankSettlementcode, 0, SumOfPayFTFListEWT))
            JVPaymentEFTCheck.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashinBankPEMCCode, SumOfPayFTFListEWT, 0))
            JVPaymentEFTCheck.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.EWTPayable, 0, SumOfPayFTFListEWT))
        End If

        If MarketFees <> 0 Then
            JVPaymentEFTCheck.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashinBankPEMCCode, MarketFees, 0))
        End If

        If SumOfPayFTFListMFRefund <> 0 Then
            JVPaymentEFTCheck.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashInbankSettlementcode, SumOfPayFTFListMFRefund, 0))
            JVPaymentEFTCheck.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashinBankPEMCCode, 0, SumOfPayFTFListMFRefund))
        End If

        If SumOfPayFTFListTransToPR <> 0 Then
            JVPaymentEFTCheck.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.ClearingAccountCode, SumOfPayFTFListTransToPR, 0))
            JVPaymentEFTCheck.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.PRWESMCode, 0, SumOfPayFTFListTransToPR))
            JVPaymentEFTCheck.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashinBankPrudentialCode, SumOfPayFTFListTransToPR, 0))
            JVPaymentEFTCheck.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashInbankSettlementcode, 0, SumOfPayFTFListTransToPR))
        End If

        If SumOfPrevDeferred <> 0 Then
            JVPaymentEFTCheck.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.DeferredPaymentCode, SumOfPrevDeferred, 0))
        End If

        If TotalPayment <> 0 Or MarketFees <> 0 Then
            JVPaymentEFTCheck.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashInbankSettlementcode, 0, TotalPayment + MarketFees + SumOfPrevDeferred))
        ElseIf SumOfPrevDeferred = SumOfCurrDeferred And TotalPayment = 0 Then
            JVPaymentEFTCheck.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashInbankSettlementcode, 0, SumOfPrevDeferred))
        End If

        If SumOfCurrDeferred <> 0 Then
            JVPaymentEFTCheck.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashInbankSettlementcode, SumOfCurrDeferred, 0))
            JVPaymentEFTCheck.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.DeferredPaymentCode, 0, SumOfCurrDeferred))
        End If

        Dim WESMBillGPPostedItem As New WESMBillGPPosted

        With WESMBillGPPostedItem
            .Remarks = JVPaymentEFTCheck.Remarks
            .Posted = 0
            .BatchCode = JVPaymentEFTCheck.BatchCode
            .PostType = JVPaymentEFTCheck.PostedType
            .DocumentAmount = Math.Round((From x In JVPaymentEFTCheck.JVDetails Select x.Debit).Sum, 2)
            .JVNumber = JVPaymentEFTCheck.JVNumber
        End With

        Me._WESMBillGPPostedList.Add(WESMBillGPPostedItem)

        Return JVPaymentEFTCheck
    End Function

    Public Sub UpdateJVFromPaymentEFTCheck(ByRef JournalVoucherEFT As JournalVoucher,
                                            ByVal RFPList As RequestForPayment, _
                                            ByVal PayFTFList As List(Of FundTransferFormMain), _
                                            ByVal CollFTFList As List(Of FundTransferFormMain), _
                                            ByVal PrevDeferredPayment As List(Of DeferredMain), _
                                            ByVal CurrDeferredPayment As List(Of DeferredMain))

        JournalVoucherEFT.JVDetails.Clear()

        Dim TotalPayment = (From x In RFPList.RFPDetails _
                            Where x.RFPDetailsType = EnumRFPDetailsType.Payment _
                            Select x.Amount).Sum()

        Dim SumOfPayFTFListMFRefund As Decimal = (From x In PayFTFList Where x.TransType = EnumFTFTransType.TransferMarketFeesToSTL Select x.TotalAmount).Sum
        Dim MarketFees As Decimal = RFPList.MarketFees + SumOfPayFTFListMFRefund
        Dim SumOfPayFTFListTransToPR As Decimal = (From x In PayFTFList Where x.TransType = EnumFTFTransType.Replenishment Select x.TotalAmount).Sum
        Dim SumOfPayFTFListTransToFinPen As Decimal = (From x In PayFTFList Where x.TransType = EnumFTFTransType.TransferPEMCAccount Select x.TotalAmount).Sum
        Dim SumOfPrevDeferred As Decimal = (From x In PrevDeferredPayment Select x.OutstandingBalanceDeferredPayment).Sum()
        Dim SumOfCurrDeferred As Decimal = (From x In CurrDeferredPayment Select x.OutstandingBalanceDeferredPayment).Sum()

        If TotalPayment <> 0 Then
            TotalPayment -= SumOfPrevDeferred
            JournalVoucherEFT.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.ClearingAccountCode, TotalPayment, 0))
        End If

        If MarketFees <> 0 Then
            JournalVoucherEFT.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashinBankPEMCCode, MarketFees, 0))
        End If

        If SumOfPayFTFListMFRefund <> 0 Then
            JournalVoucherEFT.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashInbankSettlementcode, SumOfPayFTFListMFRefund, 0))
            JournalVoucherEFT.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashinBankPEMCCode, 0, SumOfPayFTFListMFRefund))
        End If

        If SumOfPayFTFListTransToFinPen <> 0 Then
            JournalVoucherEFT.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashinBankPEMCCode, SumOfPayFTFListTransToFinPen, 0))
            JournalVoucherEFT.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.DebitCodeNonWESM, 0, SumOfPayFTFListTransToFinPen))
            JournalVoucherEFT.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.ClearingAccountCode, SumOfPayFTFListTransToFinPen, 0))
            JournalVoucherEFT.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashInbankSettlementcode, 0, SumOfPayFTFListTransToFinPen))
        End If

        If SumOfPayFTFListTransToPR <> 0 Then
            JournalVoucherEFT.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.ClearingAccountCode, SumOfPayFTFListTransToPR, 0))
            JournalVoucherEFT.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.PRWESMCode, 0, SumOfPayFTFListTransToPR))
            JournalVoucherEFT.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashinBankPrudentialCode, SumOfPayFTFListTransToPR, 0))
            JournalVoucherEFT.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashInbankSettlementcode, 0, SumOfPayFTFListTransToPR))
        End If

        If SumOfPrevDeferred <> 0 Then
            JournalVoucherEFT.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.DeferredPaymentCode, SumOfPrevDeferred, 0))
        End If

        If TotalPayment <> 0 Then
            JournalVoucherEFT.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashInbankSettlementcode, 0, TotalPayment + MarketFees + SumOfPrevDeferred))
        End If

        If SumOfCurrDeferred <> 0 Then
            JournalVoucherEFT.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashInbankSettlementcode, SumOfCurrDeferred, 0))
            JournalVoucherEFT.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.DeferredPaymentCode, 0, SumOfCurrDeferred))
        End If

        Dim PostType As String = JournalVoucherEFT.PostedType
        Dim UpdateWESMBillGPPosted = (From x In _WESMBillGPPostedList Where x.PostType = PostType Select x).First

        UpdateWESMBillGPPosted.DocumentAmount = (From x In JournalVoucherEFT.JVDetails Select x.Debit).Sum()
    End Sub

#End Region

#Region "CreateDMCMAPMFPaidByIEMOP"
    Public Function CreateDMCM_APMF(ByVal APAllocationItem As List(Of APAllocation),
                                  ByVal AllocationDate As Date,                                  
                                  ByVal AMParticipantsList As List(Of AMParticipants)) As DebitCreditMemo
        Dim ODays As Integer = 0
        Dim DMCMEEV As New DebitCreditMemo
        Dim DMCMEDI As New DebitCreditMemo
        Dim ret As New DebitCreditMemo
        Dim _Particulars As String = ""
        Me.DMCMSeqNo += 1

        Dim getInvoiceDetails As APAllocation = (From x In APAllocationItem Where x.ChargeType = EnumChargeType.MF Select x).FirstOrDefault

        Dim AMparticipantInfo As AMParticipants = (From x In AMParticipantsList Where x.IDNumber = getInvoiceDetails.IDNumber Select x).FirstOrDefault

        With ret
            .BillingPeriod = getInvoiceDetails.BillingPeriod
            .DueDate = getInvoiceDetails.DueDate
            .IDNumber = getInvoiceDetails.IDNumber
            .DMCMNumber = Me.DMCMSeqNo
            .JVNumber = Me.JVSeqNo
            .VATExempt = getInvoiceDetails.AllocationAmount
            .TotalAmountDue = getInvoiceDetails.AllocationAmount
            .TransType = EnumDMCMTransactionType.PaymentSetupForMFAPPaidByIEMOPAccount
            .Particulars = "Debiting IEMOP account for the payment of AP on MF"
            .UpdatedBy = AMModule.UserName
            .PreparedBy = AMModule.FullName
            .CheckedBy = DMCMSign.Signatory_1
            .ApprovedBy = DMCMSign.Signatory_2
        End With
        Dim MFAmount As Decimal = 0, WTHTax As Decimal = 0, WTHVat As Decimal = 0
        For Each item In APAllocationItem
            Select Case item.PaymentType
                Case EnumPaymentNewType.MarketFees, EnumPaymentNewType.VatOnMarketFees
                    MFAmount += item.AllocationAmount
                Case EnumPaymentNewType.WithholdingTaxOnMF
                    WTHTax += Math.Abs(item.AllocationAmount)
                Case EnumPaymentNewType.WithholdingVatOnMF
                    WTHVat += Math.Abs(item.AllocationAmount)
            End Select
        Next
        With ret
            If MFAmount <> 0 Then
                .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.DebitCode, MFAmount - WTHTax - WTHVat, 0, getInvoiceDetails.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.Compute))
                .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.ClearingAccountCode, 0, MFAmount - WTHTax - WTHVat, getInvoiceDetails.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.Compute))
            End If
            If WTHTax <> 0 Then
                If WTHVat <> 0 Then
                    .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.DebitCode, WTHTax + WTHVat, 0, getInvoiceDetails.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.Compute))
                    .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.EWTReceivable, 0, WTHTax, getInvoiceDetails.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.Compute))
                    .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.EWVReceivableCode, 0, WTHVat, getInvoiceDetails.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.Compute))
                Else
                    .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.DebitCode, WTHTax, 0, getInvoiceDetails.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.Compute))
                    .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.EWTReceivable, 0, WTHTax, getInvoiceDetails.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.Compute))
                End If
            Else
                If WTHVat <> 0 Then
                    .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.DebitCode, WTHVat, 0, getInvoiceDetails.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.Compute))
                    .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.EWVReceivableCode, 0, WTHVat, getInvoiceDetails.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.Compute))                
                End If
            End If
        End With

        Return ret
    End Function
#End Region

#Region "CreateDMCMAP"
    Public Function CreateDMCM_AP(ByVal APAllocationItem As APAllocation,
                                  ByVal AllocationDate As Date,
                                  ByVal DInterestRate As Decimal,
                                  ByVal AMParticipantsList As List(Of AMParticipants)) As DebitCreditMemo
        Dim ODays As Integer = 0
        Dim DMCMEEV As New DebitCreditMemo
        Dim DMCMEDI As New DebitCreditMemo
        Dim ret As New DebitCreditMemo
        Dim _Particulars As String = ""
        Me.DMCMSeqNo += 1

        Dim AMparticipantInfo As AMParticipants = (From x In AMParticipantsList Where x.IDNumber = APAllocationItem.IDNumber Select x).FirstOrDefault

        Select Case APAllocationItem.PaymentCategory
            Case EnumCollectionCategory.Cash, EnumCollectionCategory.Drawdown
                If APAllocationItem.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy Then
                    With APAllocationItem
                        ODays = ComputeOutstandingDays(.DueDate, .NewDueDate, AllocationDate)
                    End With

                    _Particulars = ParticularsFromAPDMCM(APAllocationItem, ODays, DInterestRate)

                    With ret
                        .BillingPeriod = APAllocationItem.BillingPeriod
                        .DueDate = APAllocationItem.DueDate
                        .IDNumber = APAllocationItem.IDNumber
                        .DMCMNumber = Me.DMCMSeqNo
                        .JVNumber = Me.JVSeqNo
                        .VATExempt = APAllocationItem.AllocationAmount
                        .TotalAmountDue = APAllocationItem.AllocationAmount
                        .Particulars = _Particulars
                        .ChargeType = EnumChargeType.E
                        .TransType = EnumDMCMTransactionType.PaymentSetupEnergyDefaultInterest
                        .UpdatedBy = AMModule.UserName
                        .PreparedBy = AMModule.FullName
                        .CheckedBy = DMCMSign.Signatory_1
                        .ApprovedBy = DMCMSign.Signatory_2
                        .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.DefaultInterestCode, APAllocationItem.AllocationAmount, 0, APAllocationItem.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.Compute))
                        .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.DebitCode, 0, APAllocationItem.AllocationAmount, APAllocationItem.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.Compute))
                    End With
                End If
            Case EnumCollectionCategory.Offset

                With APAllocationItem
                    ODays = ComputeOutstandingDays(.DueDate, .NewDueDate, AllocationDate)
                End With

                _Particulars = ParticularsFromAPDMCM(APAllocationItem, ODays, DInterestRate)

                With ret
                    .BillingPeriod = APAllocationItem.BillingPeriod
                    .DueDate = APAllocationItem.DueDate
                    .IDNumber = APAllocationItem.IDNumber
                    .DMCMNumber = Me.DMCMSeqNo
                    .JVNumber = Me.JVSeqNo
                    .VATExempt = APAllocationItem.AllocationAmount
                    .TotalAmountDue = APAllocationItem.AllocationAmount
                    .Particulars = _Particulars
                    .UpdatedBy = AMModule.UserName
                    .PreparedBy = AMModule.FullName
                    .CheckedBy = DMCMSign.Signatory_1
                    .ApprovedBy = DMCMSign.Signatory_2
                End With

                Select Case APAllocationItem.PaymentType
                    Case EnumPaymentNewType.Energy
                        With ret
                            .ChargeType = EnumChargeType.E
                            .TransType = EnumDMCMTransactionType.PaymentSetupClearingAPOfEnergyAndDefaultInterest
                            .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.DebitCode, APAllocationItem.AllocationAmount, 0, APAllocationItem.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.Compute))
                            .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.ClearingAccountCode, 0, APAllocationItem.AllocationAmount, APAllocationItem.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.Compute))
                        End With
                    Case EnumPaymentNewType.DefaultInterestOnEnergy
                        With ret
                            .ChargeType = EnumChargeType.E
                            .TransType = EnumDMCMTransactionType.PaymentSetupOffsettingAPOfDefaultInterestOnEnergy
                            .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.DefaultInterestCode, APAllocationItem.AllocationAmount, 0, APAllocationItem.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.Compute))
                            .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.DebitCode, 0, APAllocationItem.AllocationAmount, APAllocationItem.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.Compute))
                        End With
                    Case EnumPaymentNewType.VatOnEnergy
                        With ret
                            .ChargeType = EnumChargeType.EV
                            .TransType = EnumDMCMTransactionType.PaymentSetupClearingAPOfVATonEnergy
                            .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.DebitCode, APAllocationItem.AllocationAmount, 0, APAllocationItem.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.Compute))
                            .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.ClearingAccountCode, 0, APAllocationItem.AllocationAmount, APAllocationItem.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.Compute))
                        End With                  
                End Select
        End Select

        Return ret
    End Function
#End Region

#Region "CreateDMCMAR"
    Public Function CreateDMCM_AR(ByVal ARCollectionItem As List(Of ARCollection),
                                  ByVal Category As EnumCollectionCategory,
                                  ByVal DInterestRate As Decimal,
                                  ByVal AMParticipantsList As List(Of AMParticipants)) As List(Of DebitCreditMemo)

        Dim ODays As Integer = 0
        Dim DMCMEEV As New DebitCreditMemo
        Dim DMCMEDI As New DebitCreditMemo
        Dim ret As New List(Of DebitCreditMemo)
        Dim _Particulars As String = ""

        Dim ARCollectionOnEnergyDI = (From x In ARCollectionItem
                                     Where x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy
                                     Select x).FirstOrDefault

        Dim ARCollectionOnEnergy = (From x In ARCollectionItem _
                                     Where x.CollectionType = EnumCollectionType.Energy _
                                     Select x).FirstOrDefault

        Dim ARCollectionOnVATonEnergy = (From x In ARCollectionItem _
                                         Where x.CollectionType = EnumCollectionType.VatOnEnergy _
                                         Select x).FirstOrDefault

        If Not ARCollectionOnEnergy Is Nothing Then
            Me.DMCMSeqNo += 1
            Dim DefaultAmount As Decimal = 0
            If Not ARCollectionOnEnergyDI Is Nothing Then
                DefaultAmount = ARCollectionOnEnergyDI.AllocationAmount
            End If
            With ARCollectionOnEnergy
                Dim _AmparticipantInfo As AMParticipants = (From x In AMParticipantsList Where x.IDNumber = .IDNumber Select x).FirstOrDefault
                If Category = EnumCollectionCategory.Offset Then
                    ODays = ComputeOutstandingDays(.DueDate, .NewDueDate, .AllocationDate)
                    _Particulars = ParticularsFromARDMCM(ARCollectionOnEnergy, ODays, DInterestRate)

                    With DMCMEEV
                        .BillingPeriod = ARCollectionOnEnergy.BillingPeriod
                        .DueDate = ARCollectionOnEnergy.DueDate
                        .IDNumber = ARCollectionOnEnergy.IDNumber
                        .DMCMNumber = Me.DMCMSeqNo
                        .JVNumber = Me.JVSeqNo
                        .VATExempt = Math.Abs(ARCollectionOnEnergy.AllocationAmount + DefaultAmount)
                        .TotalAmountDue = Math.Abs(ARCollectionOnEnergy.AllocationAmount + DefaultAmount)
                        .Particulars = _Particulars
                        .ChargeType = EnumChargeType.E
                        .TransType = EnumDMCMTransactionType.PaymentSetupClearingAROnEnergyAndDefaultInterest
                        .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.ClearingAccountCode, Math.Abs(ARCollectionOnEnergy.AllocationAmount + DefaultAmount), 0, ARCollectionOnEnergy.InvoiceNumber, EnumSummaryType.INV, _AmparticipantInfo, EnumDMCMComputed.Compute))
                        .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.CreditCode, 0, Math.Abs(ARCollectionOnEnergy.AllocationAmount + DefaultAmount), ARCollectionOnEnergy.InvoiceNumber, EnumSummaryType.INV, _AmparticipantInfo, EnumDMCMComputed.Compute))
                        .UpdatedBy = AMModule.UserName
                        .PreparedBy = AMModule.FullName
                        .CheckedBy = DMCMSign.Signatory_1
                        .ApprovedBy = DMCMSign.Signatory_2
                    End With
                End If
            End With
            ret.Add(DMCMEEV)
        End If

        If Not ARCollectionOnEnergyDI Is Nothing Then
            Me.DMCMSeqNo += 1
            With ARCollectionOnEnergyDI
                Dim _AmparticipantInfo As AMParticipants = (From x In AMParticipantsList Where x.IDNumber = .IDNumber Select x).FirstOrDefault
                If Category = EnumCollectionCategory.Offset Then
                    ODays = ComputeOutstandingDays(.DueDate, .NewDueDate, .AllocationDate)
                    _Particulars = ParticularsFromARDMCM(ARCollectionOnEnergyDI, ODays, DInterestRate)

                    With DMCMEDI
                        .BillingPeriod = ARCollectionOnEnergyDI.BillingPeriod
                        .DueDate = ARCollectionOnEnergyDI.DueDate
                        .IDNumber = ARCollectionOnEnergyDI.IDNumber
                        .DMCMNumber = Me.DMCMSeqNo
                        .JVNumber = Me.JVSeqNo
                        .VATExempt = Math.Abs(ARCollectionOnEnergyDI.AllocationAmount)
                        .TotalAmountDue = Math.Abs(ARCollectionOnEnergyDI.AllocationAmount)
                        .Particulars = _Particulars
                        .ChargeType = EnumChargeType.E
                        .TransType = EnumDMCMTransactionType.PaymentSetupOffsettingAROfDefaultInterestOnEnergy
                        .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.CreditCode, Math.Abs(ARCollectionOnEnergyDI.AllocationAmount), 0, ARCollectionOnEnergyDI.InvoiceNumber, EnumSummaryType.INV, _AmparticipantInfo, EnumDMCMComputed.Compute))
                        .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.DefaultInterestCode, 0, Math.Abs(ARCollectionOnEnergyDI.AllocationAmount), ARCollectionOnEnergyDI.InvoiceNumber, EnumSummaryType.INV, _AmparticipantInfo, EnumDMCMComputed.Compute))
                        .UpdatedBy = AMModule.UserName
                        .PreparedBy = AMModule.FullName
                        .CheckedBy = DMCMSign.Signatory_1
                        .ApprovedBy = DMCMSign.Signatory_2
                    End With
                End If
            End With
            ret.Add(DMCMEDI)
        End If

        If Not ARCollectionOnVATonEnergy Is Nothing Then
            Me.DMCMSeqNo += 1
            With ARCollectionOnVATonEnergy
                Dim _AmparticipantInfo As AMParticipants = (From x In AMParticipantsList Where x.IDNumber = .IDNumber Select x).FirstOrDefault
                If Category = EnumCollectionCategory.Offset Then
                    ODays = ComputeOutstandingDays(.DueDate, .NewDueDate, .AllocationDate)
                    _Particulars = ParticularsFromARDMCM(ARCollectionOnVATonEnergy, ODays, DInterestRate)

                    With DMCMEEV
                        .BillingPeriod = ARCollectionOnVATonEnergy.BillingPeriod
                        .DueDate = ARCollectionOnVATonEnergy.DueDate
                        .IDNumber = ARCollectionOnVATonEnergy.IDNumber
                        .DMCMNumber = Me.DMCMSeqNo
                        .JVNumber = Me.JVSeqNo
                        .VATExempt = Math.Abs(ARCollectionOnVATonEnergy.AllocationAmount)
                        .TotalAmountDue = Math.Abs(ARCollectionOnVATonEnergy.AllocationAmount)
                        .Particulars = _Particulars
                        .ChargeType = EnumChargeType.EV
                        .TransType = EnumDMCMTransactionType.PaymentSetupClearingAROnVATonEnergy
                        .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.ClearingAccountCode, Math.Abs(ARCollectionOnVATonEnergy.AllocationAmount), 0, ARCollectionOnVATonEnergy.InvoiceNumber, EnumSummaryType.INV, _AmparticipantInfo, EnumDMCMComputed.Compute))
                        .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.CreditCode, 0, Math.Abs(ARCollectionOnVATonEnergy.AllocationAmount), ARCollectionOnVATonEnergy.InvoiceNumber, EnumSummaryType.INV, _AmparticipantInfo, EnumDMCMComputed.Compute))
                        .UpdatedBy = AMModule.UserName
                        .PreparedBy = AMModule.FullName
                        .CheckedBy = DMCMSign.Signatory_1
                        .ApprovedBy = DMCMSign.Signatory_2
                    End With
                End If
            End With
            ret.Add(DMCMEEV)
        End If

        Return ret
    End Function

    Public Function CreateDMCM_ARMFMFV(ByVal ARItem As List(Of ARCollection),
                                        ByVal AllocationDate As Date,
                                        ByVal DInterestRate As Decimal,
                                        ByVal AMParticipantsList As List(Of AMParticipants), _
                                        ByVal Code As EnumDMCMCode) As DebitCreditMemo
        Dim MMVDic As New Dictionary(Of String, Integer)
        Dim ODays As Integer = 0
        Dim ret As New DebitCreditMemo
        Me.DMCMSeqNo += 1

        Dim _Particulars As String = ParticularsFromARDMCM(ARItem.FirstOrDefault, ODays, DInterestRate)

        Dim AMparticipantInfo As AMParticipants = (From x In AMParticipantsList Where x.IDNumber = ARItem.FirstOrDefault.IDNumber Select x).FirstOrDefault

        With ARItem.FirstOrDefault
            ODays = ComputeOutstandingDays(.DueDate, .NewDueDate, .AllocationDate)
        End With

        Select Case Code
            Case EnumDMCMCode.MFMFV
                Dim BaseAmount As Decimal = Math.Abs((From x In ARItem Select x.AllocationAmount).Sum())
                With ret
                    .BillingPeriod = ARItem.FirstOrDefault.BillingPeriod
                    .DueDate = ARItem.FirstOrDefault.DueDate
                    .IDNumber = ARItem.FirstOrDefault.IDNumber
                    .DMCMNumber = Me.DMCMSeqNo
                    .JVNumber = Me.JVSeqNo
                    .VATExempt = BaseAmount
                    .TotalAmountDue = BaseAmount
                    .Particulars = _Particulars
                    .ChargeType = EnumChargeType.MF
                    .TransType = EnumDMCMTransactionType.PaymentSetupClearingOnMFAndMFV
                    .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.ClearingAccountCode, BaseAmount, 0, ARItem.FirstOrDefault.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.NotCompute))
                    .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.CreditCode, 0, BaseAmount, ARItem.FirstOrDefault.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.NotCompute))
                    .UpdatedBy = AMModule.UserName
                    .PreparedBy = AMModule.FullName
                    .CheckedBy = DMCMSign.Signatory_1
                    .ApprovedBy = DMCMSign.Signatory_2
                End With
            Case EnumDMCMCode.MFMFVDI
                Dim DIonBaseAmount As Decimal = Math.Abs((From x In ARItem _
                                                          Where x.CollectionType = EnumCollectionType.DefaultInterestOnMF _
                                                          Or x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF _
                                                          Select x.AllocationAmount).Sum())

                Dim OutPutTax As Decimal = Math.Abs((From x In ARItem _
                                               Where x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF _
                                               Select x.AllocationAmount).Sum())

                Dim TDIonMFMFV As Decimal = Math.Abs((From x In ARItem _
                                               Where x.CollectionType = EnumCollectionType.DefaultInterestOnMF _
                                               Select x.AllocationAmount).Sum())

                With ret
                    .BillingPeriod = ARItem.FirstOrDefault.BillingPeriod
                    .DueDate = ARItem.FirstOrDefault.DueDate
                    .IDNumber = ARItem.FirstOrDefault.IDNumber
                    .DMCMNumber = Me.DMCMSeqNo
                    .JVNumber = Me.JVSeqNo
                    .VATExempt = DIonBaseAmount
                    .TotalAmountDue = DIonBaseAmount
                    .Particulars = _Particulars
                    .ChargeType = EnumChargeType.MF
                    .TransType = EnumDMCMTransactionType.PaymentSetupClearingOnMFAndMFVDefaultInterest
                    .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.CreditCode, DIonBaseAmount, 0, ARItem.FirstOrDefault.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.NotCompute))
                    If OutPutTax <> 0 Then
                        .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.MarketFeesOutputTaxCode, 0, OutPutTax, ARItem.FirstOrDefault.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.NotCompute))
                    End If
                    If TDIonMFMFV <> 0 Then
                        .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.DefaultInterestMarketFees, 0, TDIonMFMFV, ARItem.FirstOrDefault.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.NotCompute))
                    End If
                    .UpdatedBy = AMModule.UserName
                    .PreparedBy = AMModule.FullName
                    .CheckedBy = DMCMSign.Signatory_1
                    .ApprovedBy = DMCMSign.Signatory_2
                End With
            Case EnumDMCMCode.MFEWTWVAT
                Dim EWTWVATAmount As Decimal = Math.Abs((From x In ARItem Select x.AllocationAmount).Sum())
                Dim MFEWT As Decimal = Math.Abs((From x In ARItem _
                                      Where x.CollectionType = EnumCollectionType.WithholdingTaxOnMF _
                                      Select x.AllocationAmount).Sum)
                Dim MFWVAT As Decimal = Math.Abs((From x In ARItem _
                                       Where x.CollectionType = EnumCollectionType.WithholdingVatOnMF _
                                       Select x.AllocationAmount).Sum)
                With ret
                    .BillingPeriod = ARItem.FirstOrDefault.BillingPeriod
                    .DueDate = ARItem.FirstOrDefault.DueDate
                    .IDNumber = ARItem.FirstOrDefault.IDNumber
                    .DMCMNumber = Me.DMCMSeqNo
                    .JVNumber = Me.JVSeqNo
                    .EWT = MFEWT
                    .EWV = MFWVAT
                    .TotalAmountDue = EWTWVATAmount
                    .Particulars = _Particulars
                    .ChargeType = EnumChargeType.MF
                    .TransType = EnumDMCMTransactionType.PaymentSetupClearingOnEWTAndWVAT
                    If MFEWT <> 0 Then
                        .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.EWTReceivable, MFEWT, 0, ARItem.FirstOrDefault.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.NotCompute))
                    End If
                    If MFWVAT <> 0 Then
                        .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.EWVReceivableCode, MFWVAT, 0, ARItem.FirstOrDefault.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.NotCompute))
                    End If
                    .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.CreditCode, 0, EWTWVATAmount, ARItem.FirstOrDefault.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.NotCompute))
                    .UpdatedBy = AMModule.UserName
                    .PreparedBy = AMModule.FullName
                    .CheckedBy = DMCMSign.Signatory_1
                    .ApprovedBy = DMCMSign.Signatory_2
                End With
            Case EnumDMCMCode.MFEWTWVATDI
                Dim EWTWVATDI As Decimal = Math.Abs((From x In ARItem Select x.AllocationAmount).Sum)
                Dim MFEWTDI As Decimal = Math.Abs((From x In ARItem _
                                        Where x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest _
                                        Select x.AllocationAmount).Sum)
                Dim MFWVATDI As Decimal = Math.Abs((From x In ARItem _
                                         Where x.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest _
                                         Select x.AllocationAmount).Sum)

                With ret
                    .BillingPeriod = ARItem.FirstOrDefault.BillingPeriod
                    .DueDate = ARItem.FirstOrDefault.DueDate
                    .IDNumber = ARItem.FirstOrDefault.IDNumber
                    .DMCMNumber = Me.DMCMSeqNo
                    .JVNumber = Me.JVSeqNo
                    .EWT = MFEWTDI
                    .EWV = MFWVATDI
                    .TotalAmountDue = EWTWVATDI
                    .Particulars = _Particulars
                    .ChargeType = EnumChargeType.MF
                    .TransType = EnumDMCMTransactionType.PaymentSetupClearingOnEWTAndWVATDefaultInterest
                    If MFEWTDI <> 0 Then
                        .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.EWTReceivable, MFEWTDI, 0, ARItem.FirstOrDefault.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.NotCompute))
                    End If
                    If MFWVATDI <> 0 Then
                        .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.EWVReceivableCode, MFWVATDI, 0, ARItem.FirstOrDefault.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.NotCompute))
                    End If
                    .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.CreditCode, 0, EWTWVATDI, ARItem.FirstOrDefault.InvoiceNumber, EnumSummaryType.INV, AMparticipantInfo, EnumDMCMComputed.NotCompute))
                    .UpdatedBy = AMModule.UserName
                    .PreparedBy = AMModule.FullName
                    .CheckedBy = DMCMSign.Signatory_1
                    .ApprovedBy = DMCMSign.Signatory_2
                End With
        End Select

        Return ret
    End Function
#End Region

#Region "CreateDMCMARWTaxAdjustmentToAddBack"
    Public Function CreateARDMCM_WHTax_Toaddback(ByVal wesmbillsummaryItem As WESMBillSummary, ByVal amountadjusted As Decimal, ByVal participantinfo As AMParticipants, ByVal allocdate As Date) As DebitCreditMemo
        Dim ret As New DebitCreditMemo
        Me.DMCMSeqNo += 1
        With ret
            .BillingPeriod = wesmbillsummaryItem.BillPeriod
            .DueDate = wesmbillsummaryItem.DueDate
            .IDNumber = wesmbillsummaryItem.IDNumber.IDNumber
            .DMCMNumber = Me.DMCMSeqNo
            .JVNumber = Me.JVSeqNo
            .VATExempt = Math.Abs(amountadjusted)
            .TotalAmountDue = Math.Abs(amountadjusted)
            .Particulars = "Withholding Tax Adjustment (To add the balance of AR) " & wesmbillsummaryItem.INVDMCMNo
            .ChargeType = EnumChargeType.E
            .TransType = EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAROnEnergyAddBack
            .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.CreditCode, Math.Abs(amountadjusted), 0, wesmbillsummaryItem.INVDMCMNo, EnumSummaryType.INV, participantinfo, EnumDMCMComputed.Compute))
            .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.ClearingAccountCode, 0, Math.Abs(amountadjusted), wesmbillsummaryItem.INVDMCMNo, EnumSummaryType.INV, participantinfo, EnumDMCMComputed.Compute))
            .UpdatedBy = AMModule.UserName
            .PreparedBy = AMModule.FullName
            .CheckedBy = DMCMSign.Signatory_1
            .ApprovedBy = DMCMSign.Signatory_2
        End With
        Return ret
    End Function
#End Region

#Region "CreateDMCMAPWTaxAdjustmentToAddBack"
    Public Function CreateAPDMCM_WHTax_Toaddback(ByVal wesmbillsummaryItem As WESMBillSummary, ByVal amountadjusted As Decimal, ByVal participantinfo As AMParticipants, ByVal allocdate As Date) As DebitCreditMemo
        Dim ret As New DebitCreditMemo
        Me.DMCMSeqNo += 1
        With ret
            .BillingPeriod = wesmbillsummaryItem.BillPeriod
            .DueDate = wesmbillsummaryItem.DueDate
            .IDNumber = wesmbillsummaryItem.IDNumber.IDNumber
            .DMCMNumber = Me.DMCMSeqNo
            .JVNumber = Me.JVSeqNo
            .VATExempt = Math.Abs(amountadjusted)
            .TotalAmountDue = Math.Abs(amountadjusted)
            .Particulars = "Withholding Tax Adjustment (To add back balance of AP) " & wesmbillsummaryItem.INVDMCMNo
            .ChargeType = EnumChargeType.E
            .TransType = EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAPOnEnergyAddBack
            .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.ClearingAccountCode, Math.Abs(amountadjusted), 0, wesmbillsummaryItem.INVDMCMNo, EnumSummaryType.INV, participantinfo, EnumDMCMComputed.Compute))
            .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.DebitCode, 0, Math.Abs(amountadjusted), wesmbillsummaryItem.INVDMCMNo, EnumSummaryType.INV, participantinfo, EnumDMCMComputed.Compute))
            .UpdatedBy = AMModule.UserName
            .PreparedBy = AMModule.FullName
            .CheckedBy = DMCMSign.Signatory_1
            .ApprovedBy = DMCMSign.Signatory_2
        End With
        Return ret
    End Function
#End Region

#Region "Compute Outstanding Days"
    Private Function ComputeOutstandingDays(ByVal oDueDate As Date, ByVal nDueDate As Date, ByVal AllocDate As Date) As Integer
        Dim outstandingDays As Integer = 0
        If oDueDate = nDueDate Then
            If nDueDate < AllocDate Then
                outstandingDays = CInt(DateDiff(DateInterval.Day, nDueDate, AllocDate)) + 1
            End If
        Else
            outstandingDays = CInt(DateDiff(DateInterval.Day, nDueDate, AllocDate))
        End If

        Return outstandingDays
    End Function
#End Region

#Region "DMCM Particulars for AP"
    Function ParticularsFromAPDMCM(ByVal APAllocItem As APAllocation,
                                   ByVal OutstandingDays As Integer,
                                   ByVal DailyInterestRate As Decimal) As String
        Dim retParticular As String = ""
        If APAllocItem.PaymentCategory = EnumCollectionCategory.Cash Or APAllocItem.PaymentCategory = EnumCollectionCategory.Drawdown Then
            Select Case APAllocItem.PaymentType
                Case EnumPaymentNewType.DefaultInterestOnEnergy
                    retParticular = "Debiting your account through offsetting for Default Interest on Energy for the"
                Case EnumPaymentNewType.WithholdingTaxOnEnergy
                    retParticular = "Debiting your account to record payment of withholding tax payable."
            End Select
        Else
            Select Case APAllocItem.PaymentType
                Case EnumPaymentNewType.Energy
                    retParticular = "Debiting your account through offsetting on Energy for the "
                Case EnumPaymentNewType.DefaultInterestOnEnergy
                    retParticular = "Debiting your account through offsetting for Default Interest on Energy with outstanding days of " & OutstandingDays & ", using the rate " & DailyInterestRate & " for the "
                Case EnumPaymentNewType.VatOnEnergy
                    retParticular = "Debiting your account through offsetting on VAT on Energy for the "
            End Select
        End If

        retParticular &= " Billing Period: " & APAllocItem.BillingPeriod & "; Due Date: " & APAllocItem.NewDueDate & "."

        Return retParticular
    End Function
#End Region

#Region "DMCM Particulars for AR"
    Function ParticularsFromARDMCM(ByVal ARCollItem As ARCollection,
                                   ByVal OutstandingDays As Integer,
                                   ByVal DailyInterestRate As Decimal) As String

        Dim retParticular As String = ""
        If Not ARCollItem.CollectionCategory = EnumCollectionCategory.Cash Or Not ARCollItem.CollectionCategory = EnumCollectionCategory.Drawdown Then
            Select Case ARCollItem.CollectionType
                Case EnumCollectionType.MarketFees, EnumCollectionType.VatOnMarketFees
                    retParticular = "Crediting your account through offsetting for Market Fees including VAT for the "
                Case EnumCollectionType.DefaultInterestOnMF, EnumCollectionType.DefaultInterestOnVatOnMF
                    retParticular = "Crediting your account through offsetting for Default Interest on Market Fees including VAT with outstanding days of " & OutstandingDays & ", using the rate " & DailyInterestRate & " for the "
                Case EnumCollectionType.WithholdingTaxOnMF, EnumCollectionType.WithholdingVatOnMF
                    retParticular = "Debiting your account through offsetting for Withholding Tax/Withholding VAT on Market Fees  for the "
                Case EnumCollectionType.WithholdingTaxOnDefaultInterest, EnumCollectionType.WithholdingVatOnDefaultInterest
                    retParticular = "Debiting your account through offsetting for Withholding Tax/Withholding VAT on Default Interest on Market Fees with outstanding days of " & OutstandingDays & ", using the rate " & DailyInterestRate & " for the "
                Case EnumCollectionType.Energy
                    retParticular = "Crediting your account through offsetting on Energy for the"
                Case EnumCollectionType.DefaultInterestOnEnergy
                    retParticular = "Crediting your account through offsetting for Default Interest on Energy with outstanding days of " & OutstandingDays & ", using the rate " & DailyInterestRate & " for the "
                Case EnumCollectionType.VatOnEnergy
                    retParticular = "Crediting your account through offsetting on VAT on Energy for the "
            End Select
        End If

        retParticular &= " Billing Period: " & ARCollItem.BillingPeriod & "; Due Date: " & ARCollItem.NewDueDate & "."

        Return retParticular
    End Function
#End Region

End Class
