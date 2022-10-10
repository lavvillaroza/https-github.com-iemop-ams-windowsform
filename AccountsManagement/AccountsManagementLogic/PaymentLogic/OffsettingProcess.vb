Imports AccountsManagementObjects
Imports System.Windows.Forms


Public Class OffsettingProcess
    Implements IDisposable
    Public Sub New()

    End Sub

#Region "Property of OffsettingEnergyCollectionList"
    Private _OffsettingEnergyCollListTemp As New List(Of ARCollection)
    Public ReadOnly Property OffsettingEnergyCollListTemp() As List(Of ARCollection)
        Get
            Return _OffsettingEnergyCollListTemp
        End Get
    End Property

    Private _OffsettingEnergyCollectionList As New List(Of ARCollection)
    Public ReadOnly Property OffsettingEnergyCollectionList() As List(Of ARCollection)
        Get
            Return _OffsettingEnergyCollectionList
        End Get
    End Property
#End Region

#Region "Property of OffsettingEnergyCollectionListDT"
    Private _OffsettingEnergyCollectionListDT As New DataTable
    Public ReadOnly Property OffsettingEnergyCollectionListDT() As DataTable
        Get
            Return _OffsettingEnergyCollectionListDT
        End Get
    End Property
#End Region

#Region "Property of OffsettingVATonEnergyCollectionList"
    Private _OffsettingVATonEnergyCollectionListTemp As New List(Of ARCollection)
    Public ReadOnly Property OffsettingVATonEnergyCollectionListTemp() As List(Of ARCollection)
        Get
            Return _OffsettingVATonEnergyCollectionListTemp
        End Get
    End Property

    Private _OffsettingVATonEnergyCollectionList As New List(Of ARCollection)
    Public ReadOnly Property OffsettingVATonEnergyCollectionList() As List(Of ARCollection)
        Get
            Return _OffsettingVATonEnergyCollectionList
        End Get
    End Property
#End Region

#Region "Property of OffsettingVATonEnergyCollectionListDT"
    Private _OffsettingVATonEnergyCollectionListDT As New DataTable
    Public ReadOnly Property OffsettingVATonEnergyCollectionListDT() As DataTable
        Get
            Return _OffsettingVATonEnergyCollectionListDT
        End Get
    End Property
#End Region

#Region "Properties OffsettingMFwithVATCollectionList"
    Private _OffsettingMFwithVATCollListTemp As New List(Of ARCollection)
    Public ReadOnly Property OffsettingMFwithVATCollListTemp() As List(Of ARCollection)
        Get
            Return _OffsettingMFwithVATCollListTemp
        End Get
    End Property

    Private _OffsettingMFwithVATCollectionList As New List(Of ARCollection)
    Public ReadOnly Property OffsettingMFwithVATCollectionList() As List(Of ARCollection)
        Get
            Return _OffsettingMFwithVATCollectionList
        End Get
    End Property
#End Region

#Region "Properties OffsettingMFwithVATCollectionListDT"
    Private _OffsettingMFwithVATCollectionListDT As New DataTable
    Public ReadOnly Property OffsettingMFwithVATCollectionListDT() As DataTable
        Get
            Return _OffsettingMFwithVATCollectionListDT
        End Get
    End Property
#End Region

#Region "Property of OffsettingEnergyAllocationList"
    Private _OffsettingEnergyAllocationList As New List(Of APAllocation)
    Public ReadOnly Property OffsettingEnergyAllocationList() As List(Of APAllocation)
        Get
            Return _OffsettingEnergyAllocationList
        End Get
    End Property
#End Region

#Region "Property of OffsettingEnergyAllocationListDT"
    Private _OffsettingEnergyAllocationListDT As New DataTable
    Public ReadOnly Property OffsettingEnergyAllocationListDT() As DataTable
        Get
            Return _OffsettingEnergyAllocationListDT
        End Get
    End Property
#End Region

#Region "Property of OffsettingVATonEnergyAllocationList"
    Private _OffsettingVATonEnergyAllocationList As New List(Of APAllocation)
    Public ReadOnly Property OffsettingVATonEnergyAllocationList() As List(Of APAllocation)
        Get
            Return _OffsettingVATonEnergyAllocationList
        End Get
    End Property
#End Region

#Region "Property of OffsettingEnergyAllocationListDT"
    Private _OffsettingVATonEnergyAllocationListDT As New DataTable
    Public ReadOnly Property OffsettingVATonEnergyAllocationListDT() As DataTable
        Get
            Return _OffsettingVATonEnergyAllocationListDT
        End Get
    End Property
#End Region

#Region "Property of OffsettingMFwithVATAllocationList"
    Private _OffsettingMFwithVATAllocation As New List(Of APAllocation)
    Public ReadOnly Property OffsettingMFwithVATAllocation() As List(Of APAllocation)
        Get
            Return _OffsettingMFwithVATAllocation
        End Get
    End Property
#End Region

#Region "Property of OffsettingMFwithVATAllocationListDT"
    Private _OffsettingMFwithVATAllocationListDT As New DataTable
    Public ReadOnly Property OffsettingMFwithVATAllocationListDT() As DataTable
        Get
            Return _OffsettingMFwithVATAllocationListDT
        End Get
    End Property
#End Region

#Region "Property of AMParticipants"
    Public _AMParticipants As List(Of AMParticipants)
    Private ReadOnly Property AMParticipants() As List(Of AMParticipants)
        Get
            Return _AMParticipants
        End Get
    End Property
#End Region

#Region "Property of MFwithVATShare"
    Private _MFwithVATShare As New List(Of PaymentShare)
    Public ReadOnly Property MFwithVATShare() As List(Of PaymentShare)
        Get
            Return _MFwithVATShare
        End Get
    End Property
#End Region

#Region "Property of MFFITShare"
    Private _MFwithVATFITShare As New List(Of PaymentShare)
    Public ReadOnly Property MFwithVATFITShare() As List(Of PaymentShare)
        Get
            Return _MFwithVATFITShare
        End Get
    End Property
#End Region

#Region "Method for MFwithVATShare"
    Private Sub AssignMFwithVATShareToList(ByVal _MFPayment As List(Of APAllocation))
        Dim ForMFFITShare As Boolean = False
        Dim distinctMPID As List(Of String) = (From x In _MFPayment Select x.IDNumber).Distinct().ToList
        Dim GetLastPaymentShareNo As Long = (From x In Me._VATonEnergyShare Select x.PaymentShareNo Order By PaymentShareNo Descending).FirstOrDefault
        If GetLastPaymentShareNo = 0 Then
            GetLastPaymentShareNo = (From x In Me._EnergyShare Select x.PaymentShareNo Order By PaymentShareNo Descending).FirstOrDefault
        End If
        For Each mpID In distinctMPID
            Dim getSharePerInvoice = (From x In _MFPayment Where x.IDNumber = mpID Select x.InvoiceNumber).Distinct.ToList()
            For Each item In getSharePerInvoice

                Dim getTotalPerInvoiceMF As Decimal = (From x In _MFPayment Where x.InvoiceNumber = item _
                                                        And x.PaymentType <> EnumPaymentNewType.VatOnMarketFees _
                                                        Select x.AllocationAmount).Sum()

                Dim getTotalPerInvoiceMFV As Decimal = (From x In _MFPayment Where x.InvoiceNumber = item _
                                                         And x.PaymentType = EnumPaymentNewType.VatOnMarketFees _
                                                         Select x.AllocationAmount).Sum()


                Dim getInvoiceMFInfo = (From x In _MFPayment Where x.InvoiceNumber = item _
                                        And x.PaymentType = EnumPaymentNewType.MarketFees And x.IDNumber = mpID Select x).FirstOrDefault

                If Not getInvoiceMFInfo Is Nothing Then
                    GetLastPaymentShareNo += 1
                    Using _Item As New PaymentShare
                        With _Item
                            .PaymentShareNo = GetLastPaymentShareNo
                            .WESMBillSummaryNo = getInvoiceMFInfo.WESMBillSummaryNo
                            .BillingPeriod = getInvoiceMFInfo.BillingPeriod
                            .IDNumber = getInvoiceMFInfo.IDNumber
                            .InvoiceNumber = getInvoiceMFInfo.InvoiceNumber
                            .AmountShare = getTotalPerInvoiceMF
                            .PaymentType = getInvoiceMFInfo.PaymentType
                            .ChargeType = getInvoiceMFInfo.ChargeType
                            .OffsettingSequence = getInvoiceMFInfo.OffsettingSequence
                            .WESMBillBatchNo = getInvoiceMFInfo.WESMBillBatchNo
                        End With
                        Me._MFwithVATShare.Add(_Item)
                    End Using
                End If
                Dim getInvoiceMFVInfo = (From x In _MFPayment Where x.InvoiceNumber = item _
                                        And x.PaymentType = EnumPaymentNewType.VatOnMarketFees And x.IDNumber = mpID Select x).FirstOrDefault
                If Not getInvoiceMFVInfo Is Nothing Then
                    GetLastPaymentShareNo += 1
                    Using _Item As New PaymentShare
                        With _Item
                            .PaymentShareNo = GetLastPaymentShareNo
                            .WESMBillSummaryNo = getInvoiceMFVInfo.WESMBillSummaryNo
                            .BillingPeriod = getInvoiceMFVInfo.BillingPeriod
                            .IDNumber = getInvoiceMFVInfo.IDNumber
                            .InvoiceNumber = getInvoiceMFVInfo.InvoiceNumber
                            .AmountShare = getTotalPerInvoiceMFV
                            .PaymentType = getInvoiceMFVInfo.PaymentType
                            .ChargeType = getInvoiceMFVInfo.ChargeType
                            .OffsettingSequence = getInvoiceMFVInfo.OffsettingSequence
                            .WESMBillBatchNo = getInvoiceMFVInfo.WESMBillBatchNo
                        End With
                        Me._MFwithVATShare.Add(_Item)
                    End Using
                End If
            Next
        Next

    End Sub
#End Region

#Region "Property EnergyShare"
    Private _EnergyShare As New List(Of PaymentShare)
    Public ReadOnly Property EnergyShare() As List(Of PaymentShare)
        Get
            Return _EnergyShare
        End Get
    End Property
#End Region

#Region "Property Previous Deferrred"
    Private _DeferredShare As New List(Of DeferredMain)
    Public ReadOnly Property DeferredShare() As List(Of DeferredMain)
        Get
            Return _DeferredShare
        End Get
    End Property
#End Region

#Region "Initial Payment Share no"
    Private _IniPaymentShareNo As Long
#End Region

#Region "Method for EnergyShare"
    Private Sub AssignEnergyShareToList(ByVal _EnergyPayment As List(Of APAllocation))
        Dim FilteredEnergyPayment As List(Of APAllocation) = (From x In _EnergyPayment _
                                                              Where x.AllocationAmount > 0).ToList()
        For Each Item In FilteredEnergyPayment
            Using _Item As New PaymentShare
                If Not Item.PaymentType = EnumPaymentNewType.WithholdingTaxOnEnergy Then
                    Me._IniPaymentShareNo += 1
                    With _Item
                        .PaymentShareNo = Me._IniPaymentShareNo
                        .WESMBillSummaryNo = Item.WESMBillSummaryNo
                        .BillingPeriod = Item.BillingPeriod
                        .IDNumber = Item.IDNumber
                        .InvoiceNumber = Item.InvoiceNumber
                        .AmountShare = Item.AllocationAmount
                        .PaymentType = Item.PaymentType
                        .ChargeType = Item.ChargeType
                        .OffsettingSequence = Item.OffsettingSequence
                        .WESMBillBatchNo = Item.WESMBillBatchNo
                        If .InvoiceNumber.Contains("TS-W") And Not .InvoiceNumber.Contains("-ADJ") Then
                            .AllowOffsetToAR = False
                        Else
                            .AllowOffsetToAR = True
                        End If
                    End With
                    Me._EnergyShare.Add(_Item)
                End If
            End Using
        Next
        Me._EnergyShare.TrimExcess()
    End Sub
#End Region

#Region "Method for EnergyShare Deferred"
    Private Sub AssignDeferredEnergyToList(ByVal _EnergyPayment As List(Of DeferredMain))
        Dim FilteredEnergyPayment As List(Of DeferredMain) = (From x In _EnergyPayment _
                                                              Where x.OutstandingBalanceDeferredPayment > 0).ToList()
        For Each Item In FilteredEnergyPayment
            Me._IniPaymentShareNo += 1
            Using _Item As New PaymentShare
                With _Item
                    .PaymentShareNo = Me._IniPaymentShareNo
                    .WESMBillSummaryNo = Item.DeferredPaymentNo.ToString()
                    .BillingPeriod = 0
                    .IDNumber = Item.IDNumber
                    .InvoiceNumber = "N/A"
                    .AmountShare = Item.OutstandingBalanceDeferredPayment
                    .PaymentType = EnumPaymentNewType.DeferredAppliedEnergy
                    .ChargeType = Item.ChargeType
                    .OffsettingSequence = 0
                    .WESMBillBatchNo = 0
                End With
                Me._EnergyShare.Add(_Item)
            End Using
        Next
        Me._EnergyShare.TrimExcess()
    End Sub
#End Region

#Region "Property VATShare"
    Private _VATonEnergyShare As New List(Of PaymentShare)
    Public ReadOnly Property VATonEnergyShare() As List(Of PaymentShare)
        Get
            Return _VATonEnergyShare
        End Get
    End Property
#End Region

#Region "Method For VATonEnergyShare"
    Private Sub AssignVATonEnergyShareToList(ByVal _VATPayment As List(Of APAllocation))
        Dim FilteredVATPayment As List(Of APAllocation) = (From x In _VATPayment _
                                                              Where x.AllocationAmount > 0).ToList()
        For Each Item In FilteredVATPayment
            Me._IniPaymentShareNo += 1
            Using _Item As New PaymentShare
                With _Item
                    .PaymentShareNo = Me._IniPaymentShareNo
                    .WESMBillSummaryNo = Item.WESMBillSummaryNo
                    .BillingPeriod = Item.BillingPeriod
                    .IDNumber = Item.IDNumber
                    .InvoiceNumber = Item.InvoiceNumber
                    .AmountShare = Item.AllocationAmount
                    .PaymentType = Item.PaymentType
                    .ChargeType = Item.ChargeType
                    .OffsettingSequence = Item.OffsettingSequence
                    .WESMBillBatchNo = Item.WESMBillBatchNo
                    If .InvoiceNumber.Contains("TS-W") And Not .InvoiceNumber.Contains("-ADJ") Then
                        .AllowOffsetToAR = False
                    Else
                        .AllowOffsetToAR = True
                    End If
                End With
                Me._VATonEnergyShare.Add(_Item)
            End Using
        Next
    End Sub
#End Region

#Region "Method for VAT Deferred"
    Private Sub AssignDeferredVATToList(ByVal _VATPayment As List(Of DeferredMain))
        Dim FilteredEnergyPayment As List(Of DeferredMain) = (From x In _VATPayment _
                                                              Where x.OutstandingBalanceDeferredPayment > 0).ToList()
        For Each Item In FilteredEnergyPayment
            Me._IniPaymentShareNo += 1
            Using _Item As New PaymentShare
                With _Item
                    .PaymentShareNo = Me._IniPaymentShareNo
                    .WESMBillSummaryNo = 0
                    .BillingPeriod = Item.DeferredPaymentNo.ToString()
                    .IDNumber = Item.IDNumber
                    .InvoiceNumber = "N/A"
                    .AmountShare = Item.OutstandingBalanceDeferredPayment
                    .PaymentType = EnumPaymentNewType.DeferredAppliedVAT
                    .ChargeType = Item.ChargeType
                    .OffsettingSequence = 0
                    .WESMBillBatchNo = 0
                End With
                Me._VATonEnergyShare.Add(_Item)
            End Using
        Next
        Me._VATonEnergyShare.TrimExcess()
    End Sub
#End Region

#Region "Property OffsettingSequence"
    Private _OffsettingSequence As Integer = 0
    Public ReadOnly Property OffsettingSequence() As Integer
        Get
            Return _OffsettingSequence
        End Get
    End Property
#End Region

#Region "Property ProformaEntries"
    Public _PaymentProformaEntries As New PaymentProformaEntries
    Private ReadOnly Property PaymentProformaEntries() As PaymentProformaEntries
        Get
            Return _PaymentProformaEntries
        End Get
    End Property
#End Region

#Region "Property DMCM List"
    Private _ListofDMCM As New List(Of DebitCreditMemo)
    Public ReadOnly Property ListofDMCM() As List(Of DebitCreditMemo)
        Get
            Return _ListofDMCM
        End Get
    End Property
#End Region

#Region "SPAInvoiceLIst"
    Public _SPAInvoiceList As New List(Of SPAInvoices)
    Private ReadOnly Property SPAInvoicelist() As List(Of SPAInvoices)
        Get
            Return _SPAInvoiceList
        End Get
    End Property
#End Region

#Region "Main Function fro Computation of Offsetting"
    Public Sub GetOffsettingList(ByVal AllocationDate As Date, ByVal DefaultInterestRate As Decimal,
                                ByVal iEnergyPayment As List(Of APAllocation),
                                ByVal iVATPayment As List(Of APAllocation),
                                ByVal iMFPayment As List(Of APAllocation),
                                ByRef WESMBillSummaryList As List(Of WESMBillSummary),
                                ByVal AMParticipants As List(Of AMParticipants),
                                ByRef deferredList As List(Of DeferredMain),
                                ByVal progress As IProgress(Of ProgressClass))
        Try
            Dim ARWESMBillSummaryListMFMFV As New List(Of WESMBillSummary)
            Dim APWESMBillSummaryListMFMFV As New List(Of WESMBillSummary)
            Dim ARWESMBillSummaryListEnergy As New List(Of WESMBillSummary)
            Dim ARWESMBillSummaryListVAT As New List(Of WESMBillSummary)
            Dim APWESMBillSummaryListEnergy As New List(Of WESMBillSummary)
            Dim APWESMBillSummaryListVAT As New List(Of WESMBillSummary)
            Dim ListofBPinEnergyShare As New List(Of Long)
            Dim newProgress As New ProgressClass

            newProgress.ProgressMsg = "Preparing for offsetting process..."
            progress.Report(newProgress)

            Me._AMParticipants = AMParticipants
            Me._IniPaymentShareNo = 0
            Me.AssignEnergyShareToList(iEnergyPayment)
            Me.AssignVATonEnergyShareToList(iVATPayment)
            Me.AssignMFwithVATShareToList(iMFPayment)

            'If iEnergyPayment.Count = 0 And iVATPayment.Count = 0 And iMFPayment.Count = 0 Then
            '    Exit Sub
            'End If

            Dim ContinueToMFOffset As Boolean = True
            Dim ContinueToMFFITOffset As Boolean = True
            Dim ContinueToEnergyOffset As Boolean = True
            Dim ContinueToEnergyVATOffset As Boolean = True
            Dim ContinueToEnergyFITOffset As Boolean = False
            Dim ContinueToEnergyVATFITOffset As Boolean = False

            Dim useDeferredShare As Boolean = False

            ARWESMBillSummaryListMFMFV = (From x In WESMBillSummaryList
                                          Where (x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV) _
                                           And x.EndingBalance < 0 And x.DueDate <= AllocationDate
                                          Select x Order By x.WESMBillBatchNo, x.DueDate, x.BillPeriod, x.EndingBalance Descending).ToList()

            ARWESMBillSummaryListEnergy = (From x In WESMBillSummaryList
                                           Where x.ChargeType = EnumChargeType.E _
                                                  And x.EndingBalance < 0 And x.DueDate <= AllocationDate _
                                                  And (Math.Abs(x.EndingBalance) - Math.Abs(x.EnergyWithhold)) > 0 _
                                                  And x.NoOffset = False
                                           Select x Order By x.WESMBillBatchNo, x.NewDueDate, x.BillPeriod, x.EndingBalance Descending).ToList()

            ARWESMBillSummaryListVAT = (From x In WESMBillSummaryList
                                        Where x.ChargeType = EnumChargeType.EV _
                                            And x.EndingBalance < 0 And x.DueDate <= AllocationDate _
                                            And (Math.Abs(x.EndingBalance) - Math.Abs(x.EnergyWithhold)) > 0 _
                                            And x.IDNumber.ZeroRatedEnergy = False _
                                            And x.NoOffset = False
                                        Select x Order By x.DueDate, x.WESMBillBatchNo, x.BillPeriod, x.EndingBalance Descending).ToList()

            Do While ContinueToMFOffset = True Or ContinueToMFFITOffset = True Or
                     ContinueToEnergyVATOffset = True Or ContinueToEnergyOffset = True Or
                     ContinueToEnergyFITOffset = True Or ContinueToEnergyVATFITOffset = True

                Me._OffsettingSequence += 1
                newProgress = New ProgressClass
                newProgress.ProgressMsg = "Offsetting Iteration Counter: " & Me._OffsettingSequence.ToString
                progress.Report(newProgress)

                Debug.Print("MFO:" & ContinueToMFOffset.ToString & "|" _
                            & "MFFitO:" & ContinueToMFFITOffset.ToString & "|" _
                            & "EO:" & ContinueToEnergyOffset.ToString & "|" _
                            & "EVO:" & ContinueToEnergyVATOffset.ToString & "|" _
                            & "EFitO:" & ContinueToEnergyFITOffset.ToString & "|" _
                            & "EVFitO:" & ContinueToEnergyVATFITOffset.ToString & "|" _
                            & "Offset Count:" & Me.OffsettingSequence.ToString)

                Me._OffsettingMFwithVATCollListTemp.Clear()
                Me._OffsettingEnergyCollListTemp.Clear()
                Me._OffsettingVATonEnergyCollectionListTemp.Clear()

                'MF Offsetting***************************************************************************************************************************************************************
                Dim _EnergyShareListForMF = (From x In Me.EnergyShare
                                             Where x.AmountBalance > 0
                                             Group By IDNumber = x.IDNumber
                                             Into Amount_Balance = Sum(x.AmountBalance)).ToList()

                Dim _GetEnergyShareListForMF = (From x In _EnergyShareListForMF Where x.Amount_Balance > 0 Select x).ToList()

                Dim _OffsetEnergySHareListForMF As List(Of PaymentShare) = (From x In Me.EnergyShare
                                                                            Join y In _GetEnergyShareListForMF On y.IDNumber Equals x.IDNumber
                                                                            Select x).ToList()

                Dim _ARWESMBillSummaryListMFMFV As List(Of WESMBillSummary) = (From x In _GetEnergyShareListForMF
                                                                               Join y In ARWESMBillSummaryListMFMFV On y.IDNumber.IDNumber Equals x.IDNumber
                                                                               Where y.EndingBalance < 0 And (Math.Abs(y.EndingBalance) - Math.Abs(y.EnergyWithhold)) > 0
                                                                               Select y
                                                                               Order By y.WESMBillBatchNo, y.OrigNewDueDate, y.BillPeriod, y.EndingBalance Descending).ToList()

                Dim _MFShareListForMF = (From x In Me.MFwithVATShare
                                         Where x.AmountBalance > 0
                                         Group By IDNumber = x.IDNumber
                                        Into Amount_Balance = Sum(x.AmountBalance)).ToList()

                Dim _GetMFShareListForMF = (From x In _MFShareListForMF Where x.Amount_Balance > 0 Select x).ToList()

                Dim _OffsetMFSHareListForMF As List(Of PaymentShare) = (From x In Me.MFwithVATShare
                                                                        Join y In _GetMFShareListForMF On y.IDNumber Equals x.IDNumber
                                                                        Select x).ToList()



                Dim _ARWESMBillSummaryListMFMFV2 As List(Of WESMBillSummary) = (From x In _GetMFShareListForMF
                                                                                Join y In ARWESMBillSummaryListMFMFV On y.IDNumber.IDNumber Equals x.IDNumber
                                                                                Where y.EndingBalance < 0 And (Math.Abs(y.EndingBalance) - Math.Abs(y.EnergyWithhold)) > 0
                                                                                Select y
                                                                                Order By y.WESMBillBatchNo, y.OrigNewDueDate, y.BillPeriod, y.EndingBalance Descending).ToList()

                If _ARWESMBillSummaryListMFMFV.Count > 0 And _OffsetEnergySHareListForMF.Count > 0 Then
                    ContinueToMFOffset = Me.GetOffsettingARCollectionsForMFMFV(_ARWESMBillSummaryListMFMFV, _OffsetEnergySHareListForMF, DefaultInterestRate, AllocationDate, AMParticipants)
                ElseIf _ARWESMBillSummaryListMFMFV2.Count > 0 And _OffsetMFSHareListForMF.Count > 0 Then
                    ContinueToMFOffset = Me.GetOffsettingARCollectionsForMFMFV(_ARWESMBillSummaryListMFMFV2, _OffsetMFSHareListForMF, DefaultInterestRate, AllocationDate, AMParticipants)
                Else
                    ContinueToMFOffset = False
                End If
                'End of MF Offsetting******************************************************************************************************************************************************

                'Energy offsetting for Non-Fit*********************************************************************************************************************************************
                Dim _EnergyShareListForEnergy = (From x In Me.EnergyShare
                                                 Where x.AmountBalance > 0
                                                 Group By IDNumber = x.IDNumber
                                                 Into Amount_Balance = Sum(x.AmountBalance)).ToList() 'And Not x.IDNumber.EndsWith("F") _

                Dim _GetEnergyShareListForEnergy = (From x In _EnergyShareListForEnergy Where x.Amount_Balance > 0 Select x).ToList()

                Dim _OffsetEnergyShareListForEnergy As List(Of PaymentShare) = (From x In Me.EnergyShare
                                                                                Join y In _GetEnergyShareListForEnergy On y.IDNumber Equals x.IDNumber
                                                                                Select x).ToList()

                Dim _ARWESMBillSummaryListEnergy As List(Of WESMBillSummary) = (From x In _GetEnergyShareListForEnergy
                                                                                Join y In ARWESMBillSummaryListEnergy On y.IDNumber.IDNumber Equals x.IDNumber
                                                                                Where (y.EndingBalance < 0 And (Math.Abs(y.EndingBalance) - Math.Abs(y.EnergyWithhold)) > 0 _
                                                                                       And y.EnergyWithholdStatus = EnumEnergyWithholdStatus.NotApplicable And Not y.INVDMCMNo.Contains("SPA")) _
                                                                                Or (y.EndingBalance = 0 And y.EnergyWithholdStatus = EnumEnergyWithholdStatus.UnpaidEWT And Not y.INVDMCMNo.Contains("SPA")) _
                                                                                Or (y.EndingBalance < 0 And y.EnergyWithholdStatus = EnumEnergyWithholdStatus.PaidEWT And Not y.INVDMCMNo.Contains("SPA"))
                                                                                Select y
                                                                                Order By y.WESMBillBatchNo, y.OrigNewDueDate, y.BillPeriod, y.EndingBalance Descending).Union _
                                                                                (From x In _GetEnergyShareListForEnergy
                                                                                 Join y In ARWESMBillSummaryListEnergy On y.IDNumber.IDNumber Equals x.IDNumber
                                                                                 Where (y.EndingBalance < 0 And (Math.Abs(y.EndingBalance) - Math.Abs(y.EnergyWithhold)) > 0 _
                                                                                       And y.EnergyWithholdStatus = EnumEnergyWithholdStatus.NotApplicable And y.INVDMCMNo.Contains("SPA"))
                                                                                 Select y
                                                                                 Order By y.WESMBillBatchNo, y.OrigNewDueDate, y.BillPeriod, y.EndingBalance Descending).ToList()


                If _ARWESMBillSummaryListEnergy.Count > 0 And _OffsetEnergyShareListForEnergy.Count > 0 Then
                    ContinueToEnergyOffset = Me.GetOffsettingARCollectionsForEnergy(_ARWESMBillSummaryListEnergy, _OffsetEnergyShareListForEnergy, DefaultInterestRate, AllocationDate, AMParticipants)
                Else
                    ContinueToEnergyOffset = False
                End If
                'End of Energy offsetting for Non-Fit***************************************************************************************************************************************

                'VAT Offsetting for Non-Fit*************************************************************************************************************************************************
                Dim _VATShareListForVAT = (From x In Me.VATonEnergyShare
                                           Where x.AmountBalance > 0 _
                                            And Not x.IDNumber.EndsWith(AMModule.FITParticipantCode.ToString)
                                           Group By IDNumber = x.IDNumber
                                            Into Amount_Balance = Sum(x.AmountBalance)).ToList()

                Dim _GetVATShareListForVAT = (From x In _VATShareListForVAT Where x.Amount_Balance > 0 Select x).ToList()

                Dim _OffsetVATShareListForVAT As List(Of PaymentShare) = (From x In Me.VATonEnergyShare
                                                                          Join y In _GetVATShareListForVAT On y.IDNumber Equals x.IDNumber
                                                                          Select x).ToList()

                Dim _ARWESMBillSummaryListVAT As List(Of WESMBillSummary) = (From x In _GetVATShareListForVAT
                                                                             Join y In ARWESMBillSummaryListVAT On y.IDNumber.IDNumber Equals x.IDNumber
                                                                             Where y.EndingBalance < 0 And (Math.Abs(y.EndingBalance) - Math.Abs(y.EnergyWithhold)) > 0
                                                                             Select y
                                                                             Order By y.WESMBillBatchNo, y.OrigNewDueDate, y.BillPeriod, y.EndingBalance Descending).ToList()

                If _ARWESMBillSummaryListVAT.Count > 0 And _OffsetVATShareListForVAT.Count > 0 Then
                    ContinueToEnergyVATOffset = Me.GetOffsettingARCollectionsForEnergyVAT(_ARWESMBillSummaryListVAT, _OffsetVATShareListForVAT, AllocationDate, AMParticipants)
                Else
                    ContinueToEnergyVATOffset = False
                End If
                'End of VAT Offsetting for Non-Fit*******************************************************************************************************************************************

                Dim OffsetARCollProc As New ARCollectionProcess
                Dim OffsetAPAllocProc As New APAllocationProcessNew
                OffsetAPAllocProc._AMParticipantsList = Me.AMParticipants
                OffsetAPAllocProc._PaymentProformaEntries = Me.PaymentProformaEntries

                If (_ARWESMBillSummaryListMFMFV.Count > 0 And _OffsetEnergySHareListForMF.Count > 0) _
                    Or (_ARWESMBillSummaryListMFMFV2.Count > 0 And _OffsetMFSHareListForMF.Count > 0) Then
                    'MF Offsetting AR
                    OffsetARCollProc.ComputeARCollection(Me.OffsettingMFwithVATCollListTemp, EnumCollectionType.MarketFees)
                    If OffsetARCollProc.MFwithVATCollectionList.Count() > 0 Then
                        For Each MFItem In OffsetARCollProc.MFwithVATCollectionList
                            Me._OffsettingMFwithVATCollectionList.Add(MFItem)
                        Next
                    End If
                End If

                If _ARWESMBillSummaryListEnergy.Count > 0 And _OffsetEnergyShareListForEnergy.Count > 0 Then
                    'Energy Offsetting AR
                    OffsetARCollProc.ComputeARCollection(Me.OffsettingEnergyCollListTemp, EnumCollectionType.Energy)
                    If OffsetARCollProc.EnergyCollection.Count() > 0 Then
                        For Each EnergyItem In OffsetARCollProc.EnergyCollection
                            Me._OffsettingEnergyCollectionList.Add(EnergyItem)
                        Next
                    End If
                End If

                If _ARWESMBillSummaryListVAT.Count > 0 And _OffsetVATShareListForVAT.Count > 0 Then
                    'VAT Offsetting AR
                    OffsetARCollProc.ComputeARCollection(Me.OffsettingVATonEnergyCollectionListTemp, EnumCollectionType.VatOnEnergy)
                    If OffsetARCollProc.VATCollection.Count() > 0 Then
                        For Each EnergyVATItem In OffsetARCollProc.VATCollection
                            Me._OffsettingVATonEnergyCollectionList.Add(EnergyVATItem)
                        Next
                    End If
                End If

                OffsetAPAllocProc.OffsettingSequence = Me.OffsettingSequence

                'MF Offsetting AP
                'Dim UpdatedWESMBillSummaryOnMFwithVAT = (From x In WESMBillSummaryList _
                '                                         Where x.EndingBalance > 0 _
                '                                         And (x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV) _
                '                                         Select x Order By x.NewDueDate, x.WESMBillBatchNo, x.BillPeriod, x.EndingBalance).ToList()

                'If UpdatedWESMBillSummaryOnMFwithVAT.Count > 0 And OffsetARCollProc.TotalMFCollectionPerBP.Count > 0 Then
                '    OffsetAPAllocProc.ComputeMFAPAllocationList(AllocationDate, _
                '                                                UpdatedWESMBillSummaryOnMFwithVAT, _
                '                                                OffsetARCollProc.TotalMFCollectionPerBP)
                '    For Each OffsetItem In OffsetAPAllocProc.MFWithVATAPAllocationList
                '        Me._OffsettingMFwithVATAllocation.Add(OffsetItem)
                '    Next
                '    Me.AssignMFwithVATShareToList(OffsetAPAllocProc.MFWithVATAPAllocationList)
                'End If

                'Energy and Energy FiT Offsetting AP
                Dim UpdatedWESMBillSummaryOnEnergy = (From x In WESMBillSummaryList
                                                      Where x.EndingBalance > 0 And x.ChargeType = EnumChargeType.E
                                                      Select x Order By x.NewDueDate, x.WESMBillBatchNo, x.BillPeriod, x.EndingBalance).ToList

                If UpdatedWESMBillSummaryOnEnergy.Count > 0 And OffsetARCollProc.TotalEnergyCollectionPerBP.Count > 0 Then
                    OffsetAPAllocProc.ComputeEnergyAPAllocationList(AllocationDate, UpdatedWESMBillSummaryOnEnergy,
                                                                  OffsetARCollProc.EnergyCollection, progress)
                    For Each OffsetItem In OffsetAPAllocProc.EnergyAPAllocationList
                        Me._OffsettingEnergyAllocationList.Add(OffsetItem)
                    Next
                    Me.AssignEnergyShareToList(OffsetAPAllocProc.EnergyAPAllocationList)
                End If

                'VAT and VAT FIT offsetting AP
                Dim UpdatedWESMBillSummaryOnVAT = (From x In WESMBillSummaryList
                                                   Where x.EndingBalance > 0 And x.ChargeType = EnumChargeType.EV
                                                   Select x Order By x.DueDate, x.WESMBillBatchNo, x.BillPeriod, x.EndingBalance).ToList()

                If UpdatedWESMBillSummaryOnVAT.Count > 0 And OffsetARCollProc.TotalVATCollectionPerBP.Count > 0 Then
                    OffsetAPAllocProc.ComputeVATAPAllocationList(AllocationDate, UpdatedWESMBillSummaryOnVAT,
                                                          OffsetARCollProc.VATCollection, progress)
                    For Each OffsetItem In OffsetAPAllocProc.VATonEnergyAPAllocationList
                        Me._OffsettingVATonEnergyAllocationList.Add(OffsetItem)
                    Next
                    Me.AssignVATonEnergyShareToList(OffsetAPAllocProc.VATonEnergyAPAllocationList)
                End If

                For Each ItemDMCM In OffsetAPAllocProc.ListofDMCMAP
                    Me._ListofDMCM.Add(ItemDMCM)
                Next

                'From Energyshare To VAT AR with outstanding balance Note: this is applicable only in Generator
                If ContinueToMFOffset = False And ContinueToEnergyVATOffset = False And ContinueToEnergyOffset = False _
                            And ContinueToEnergyFITOffset = False And ContinueToEnergyVATFITOffset = False Then

                    Me._OffsettingVATonEnergyCollectionListTemp.Clear()

                    Dim AMParticipantsGen = (From x In Me.AMParticipants
                                             Where x.GenLoad = EnumGenLoad.G
                                             Select x.IDNumber).ToList

                    Dim _EnergyShareForVAT = (From x In Me.EnergyShare
                                              Where x.AmountBalance > 0 _
                                              And AMParticipantsGen.Contains(x.IDNumber)
                                              Group By IDNumber = x.IDNumber
                                              Into Amount_Balance = Sum(x.AmountBalance)).ToList()


                    Dim _ARWESMBillSummaryListOffsetVAT2 As List(Of WESMBillSummary) = (From y In ARWESMBillSummaryListVAT
                                                                                        Where y.EndingBalance < 0 And (Math.Abs(y.EndingBalance) - Math.Abs(y.EnergyWithhold)) > 0
                                                                                        Join x In _EnergyShareForVAT On x.IDNumber Equals y.IDNumber.IDNumber
                                                                                        Select y Order By y.WESMBillBatchNo, y.OrigNewDueDate, y.BillPeriod, y.EndingBalance Descending).ToList()

                    If _ARWESMBillSummaryListOffsetVAT2.Count > 0 And _EnergyShareForVAT.Count > 0 Then

                        ContinueToEnergyVATOffset = Me.GetOffsettingARCollectionsForEnergyVAT2(_ARWESMBillSummaryListOffsetVAT2, AllocationDate, AMParticipants)

                        Dim OffsetARCollProc2 As New ARCollectionProcess
                        Dim OffsetAPAllocProc2 As New APAllocationProcessNew

                        OffsetAPAllocProc2._AMParticipantsList = Me.AMParticipants
                        OffsetAPAllocProc2._PaymentProformaEntries = Me.PaymentProformaEntries

                        'VAT Offsetting AR
                        OffsetARCollProc2.ComputeARCollection(Me.OffsettingVATonEnergyCollectionListTemp, EnumCollectionType.VatOnEnergy)
                        If OffsetARCollProc2.VATCollection.Count() > 0 Then
                            For Each EnergyVATItem In OffsetARCollProc2.VATCollection
                                Me._OffsettingVATonEnergyCollectionList.Add(EnergyVATItem)
                            Next
                        End If

                        OffsetAPAllocProc2.OffsettingSequence = Me.OffsettingSequence

                        'VAT Offsetting AP
                        Dim UpdatedWESMBillSummaryOnVAT2 = (From x In WESMBillSummaryList
                                                            Where x.EndingBalance > 0 And x.ChargeType = EnumChargeType.EV
                                                            Select x).ToList()

                        If UpdatedWESMBillSummaryOnVAT2.Count > 0 And OffsetARCollProc2.TotalVATCollectionPerBP.Count > 0 Then
                            OffsetAPAllocProc2.ComputeVATAPAllocationList(AllocationDate, UpdatedWESMBillSummaryOnVAT2,
                                                                          OffsetARCollProc2.VATCollection, progress)
                            For Each OffsetItem In OffsetAPAllocProc2.VATonEnergyAPAllocationList
                                Me._OffsettingVATonEnergyAllocationList.Add(OffsetItem)
                            Next

                            Me.AssignVATonEnergyShareToList(OffsetAPAllocProc2.VATonEnergyAPAllocationList)
                        End If

                        For Each ItemDMCM In OffsetAPAllocProc2.ListofDMCMAP
                            Me._ListofDMCM.Add(ItemDMCM)
                        Next

                    Else
                        ContinueToEnergyVATOffset = False
                    End If
                End If

                If ContinueToMFOffset = False And ContinueToEnergyOffset = False And ContinueToEnergyVATOffset = False Then
                    If useDeferredShare = False Then
                        Dim getEnergyDeferred As List(Of DeferredMain) = (From x In deferredList Where x.ChargeType = EnumChargeType.E Select x).ToList()
                        Dim getVATDeferred As List(Of DeferredMain) = (From x In deferredList Where x.ChargeType = EnumChargeType.EV Select x).ToList()
                        Me.AssignDeferredEnergyToList(getEnergyDeferred)
                        Me.AssignDeferredVATToList(getVATDeferred)
                        ContinueToEnergyOffset = True
                        ContinueToEnergyVATOffset = True
                        useDeferredShare = True
                    End If
                End If

                'From EnergyFit and VatFit with outstanding balance
                If ContinueToMFOffset = False And ContinueToEnergyOffset = False And ContinueToEnergyVATOffset = False Then

                    'MF FIT Offsetting***************************************************************************************************************************************************************
                    Dim ARWESMBillSummaryListMFFIT = (From x In ARWESMBillSummaryListMFMFV
                                                      Where x.IDNumber.IDNumber.EndsWith(AMModule.FITParticipantCode.ToString) _
                                                         And (x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV) _
                                                         And x.EndingBalance < 0
                                                      Select x Order By x.DueDate, x.WESMBillBatchNo, x.BillPeriod, x.EndingBalance Descending).ToList()

                    Dim _EnergyFITShareListForMF = (From x In Me.EnergyShare
                                                    Where x.AmountBalance > 0 _
                                                    And x.IDNumber.EndsWith(AMModule.FITParticipantCode.ToString)
                                                    Group By IDNumber = x.IDNumber
                                                    Into Amount_Balance = Sum(x.AmountBalance)).ToList()

                    Dim _GetEnergyFITShareListForMFFIT = (From x In _EnergyFITShareListForMF Where x.Amount_Balance > 1 Select x).ToList()

                    Dim _OffsetEnergyFITSHareListForMFFIT As List(Of PaymentShare) = (From x In Me.EnergyShare
                                                                                      Join y In _GetEnergyFITShareListForMFFIT
                                                                                   On y.IDNumber Equals x.IDNumber
                                                                                      Select x).ToList()

                    Dim _ARWESMBillSummaryListMFMFVFIT As List(Of WESMBillSummary) = (From y In ARWESMBillSummaryListMFFIT
                                                                                      Select y Order By y.WESMBillBatchNo, y.OrigNewDueDate,
                                                                                      y.BillPeriod, y.EndingBalance Descending).ToList()

                    If _ARWESMBillSummaryListMFMFVFIT.Count > 0 And _OffsetEnergyFITSHareListForMFFIT.Count > 0 Then
                        ContinueToMFFITOffset = Me.GetOffsettingARCollectionsForMFMFVFIT(_ARWESMBillSummaryListMFMFVFIT, _OffsetEnergyFITSHareListForMFFIT, DefaultInterestRate, AllocationDate, AMParticipants)
                    Else
                        ContinueToMFFITOffset = False
                    End If
                    'End of MF FIT Offsetting******************************************************************************************************************************************************

                    'Energy FIT Offsetting****************************************************************************************************************************************************
                    Dim ARWESMBillSummaryListEnergyFIT = (From x In ARWESMBillSummaryListEnergy
                                                          Where x.IDNumber.IDNumber.EndsWith(AMModule.FITParticipantCode.ToString) _
                                                         And x.ChargeType = EnumChargeType.E _
                                                         And x.EndingBalance < 0 _
                                                         And (Math.Abs(x.EndingBalance) - Math.Abs(x.EnergyWithhold)) > 0
                                                          Select x Order By x.DueDate, x.WESMBillBatchNo, x.BillPeriod, x.EndingBalance Descending).ToList()

                    Dim _EnergyFITShareListForEnergy = (From x In Me.EnergyShare
                                                        Where x.AmountBalance > 0 _
                                                       And x.IDNumber.EndsWith(AMModule.FITParticipantCode.ToString)
                                                        Group By IDNumber = x.IDNumber
                                                       Into Amount_Balance = Sum(x.AmountBalance)).ToList()

                    Dim _GetEnergyFITShareListForEnergy = (From x In _EnergyFITShareListForEnergy Where x.Amount_Balance > 1 Select x).ToList()

                    Dim _OffsetEnergyFITShareListForEnergy As List(Of PaymentShare) = (From x In Me.EnergyShare
                                                                                       Join y In _GetEnergyFITShareListForEnergy
                                                                                       On y.IDNumber Equals x.IDNumber
                                                                                       Select x).ToList()

                    Dim _ARWESMBillSUmmaryListFITEnergy As List(Of WESMBillSummary) = (From y In ARWESMBillSummaryListEnergyFIT
                                                                                       Select y Order By y.WESMBillBatchNo, y.OrigNewDueDate,
                                                                                                         y.BillPeriod, y.EndingBalance Descending).ToList()

                    If _ARWESMBillSUmmaryListFITEnergy.Count > 0 And _OffsetEnergyFITShareListForEnergy.Count > 0 Then
                        ContinueToEnergyFITOffset = Me.GetOffsettingARCollectionsForEnergyFIT(_ARWESMBillSUmmaryListFITEnergy, _OffsetEnergyFITShareListForEnergy,
                                                                                              DefaultInterestRate, AllocationDate, AMParticipants)
                    Else
                        ContinueToEnergyFITOffset = False
                    End If
                    'End of Energy FIT Offsetting**********************************************************************************************************************************************

                    'VAT FIT Offsetting********************************************************************************************************************************************************
                    'Dim ARWESMBillSummaryListVATFIT = (From x In ARWESMBillSummaryListVAT _
                    '                              Where x.IDNumber.IDNumber.EndsWith("F") _
                    '                              And x.ChargeType = EnumChargeType.EV _
                    '                              And x.EndingBalance < 0 _
                    '                              And (Math.Abs(x.EndingBalance) - Math.Abs(x.EnergyWithhold)) > 0 _
                    '                              Select x Order By x.DueDate, x.WESMBillBatchNo, x.BillPeriod, x.EndingBalance Descending).ToList()

                    'Dim _VATFITShareListForVAT = (From x In Me.VATonEnergyShare
                    '                            Where x.AmountBalance > 0 _
                    '                            And x.IDNumber.EndsWith("F") _
                    '                            Group By IDNumber = x.IDNumber _
                    '                            Into Amount_Balance = Sum(x.AmountBalance)).ToList()

                    'Dim _GetVATFITShareListForVAT = (From x In _VATFITShareListForVAT Where x.Amount_Balance > 1 Select x).ToList()

                    'Dim _OffsetVATFITShareListForVAT As List(Of PaymentShare) = (From x In Me.VATonEnergyShare _
                    '                                                             Join y In _GetVATFITShareListForVAT _
                    '                                                             On y.IDNumber Equals x.IDNumber _
                    '                                                             Select x).ToList()

                    'Dim _ARWESMBillSUmmaryListFITVAT As List(Of WESMBillSummary) = (From y In ARWESMBillSummaryListVATFIT _
                    '                                                                Select y Order By y.WESMBillBatchNo, _
                    '                                                                                  y.OrigNewDueDate, y.EndingBalance Descending).ToList()

                    'If _ARWESMBillSUmmaryListFITVAT.Count() > 0 And _OffsetVATFITShareListForVAT.Count > 0 Then
                    '    ContinueToEnergyVATFITOffset = Me.GetOffsettingARCollectionsForEnergyVATFIT(_ARWESMBillSUmmaryListFITVAT, _OffsetVATFITShareListForVAT,
                    '                                                                                AllocationDate, AMParticipants)
                    'Else
                    '    ContinueToEnergyVATFITOffset = False
                    'End If

                    'End of VAT FIT Offsetting**************************************************************************************************************************************************

                    Dim OffsetARCollProc3 As New ARCollectionProcess
                    Dim OffsetAPAllocProc3 As New APAllocationProcess

                    OffsetAPAllocProc3._AMParticipantsList = Me.AMParticipants
                    OffsetAPAllocProc3._PaymentProformaEntries = Me.PaymentProformaEntries
                    OffsetAPAllocProc3.OffsettingSequence = Me.OffsettingSequence

                    'Me._OffsettingVATonEnergyCollectionListTemp.Clear()
                    'Me._OffsettingEnergyCollListTemp.Clear()

                    If _ARWESMBillSummaryListMFMFVFIT.Count > 0 And _OffsetEnergyFITSHareListForMFFIT.Count > 0 Then
                        'MF Offsetting AR
                        OffsetARCollProc3.ComputeARCollection(Me.OffsettingMFwithVATCollListTemp, EnumCollectionType.MarketFees)
                        If OffsetARCollProc3.MFwithVATCollectionList.Count() > 0 Then
                            For Each MFItem In OffsetARCollProc3.MFwithVATCollectionList
                                Me._OffsettingMFwithVATCollectionList.Add(MFItem)
                            Next
                        End If
                    End If

                    If _ARWESMBillSUmmaryListFITEnergy.Count > 0 And _OffsetEnergyFITShareListForEnergy.Count > 0 Then
                        'Energy Offsetting AR
                        OffsetARCollProc3.ComputeARCollection(Me.OffsettingEnergyCollListTemp, EnumCollectionType.Energy)
                        If OffsetARCollProc3.EnergyCollection.Count() > 0 Then
                            For Each EnergyItem In OffsetARCollProc3.EnergyCollection
                                Me._OffsettingEnergyCollectionList.Add(EnergyItem)
                            Next
                        End If
                    End If

                    'If _ARWESMBillSUmmaryListFITVAT.Count > 0 And _OffsetVATFITShareListForVAT.Count > 0 Then
                    '    'VAT Offsetting AR
                    '    OffsetARCollProc3.ComputeARCollection(Me.OffsettingVATonEnergyCollectionListTemp, EnumCollectionType.VatOnEnergy)
                    '    If OffsetARCollProc3.VATCollection.Count() > 0 Then
                    '        For Each EnergyVATItem In OffsetARCollProc3.VATCollection
                    '            Me._OffsettingVATonEnergyCollectionList.Add(EnergyVATItem)
                    '        Next
                    '    End If
                    'End If

                    'Energy and Energy FiT Offsetting AP
                    Dim UpdatedWESMBillSummaryOnEnergyFIT = (From x In WESMBillSummaryList
                                                             Where x.EndingBalance > 0 And x.ChargeType = EnumChargeType.E
                                                             Select x Order By x.NewDueDate, x.WESMBillBatchNo, x.BillPeriod, x.EndingBalance).ToList()

                    If UpdatedWESMBillSummaryOnEnergyFIT.Count > 0 And OffsetARCollProc3.TotalEnergyCollectionPerBP.Count > 0 Then
                        OffsetAPAllocProc3.ComputeEnergyAPAllocationList(AllocationDate, UpdatedWESMBillSummaryOnEnergyFIT,
                                                                        OffsetARCollProc3.TotalEnergyCollectionPerBP)
                        For Each OffsetItem In OffsetAPAllocProc3.EnergyAPAllocationList
                            Me._OffsettingEnergyAllocationList.Add(OffsetItem)
                        Next
                        Me.AssignEnergyShareToList(OffsetAPAllocProc3.EnergyAPAllocationList)
                    End If

                    'VAT and VAT FIT offsetting AP
                    Dim UpdatedWESMBillSummaryOnVATFIT = (From x In WESMBillSummaryList
                                                          Where x.EndingBalance > 0 And x.ChargeType = EnumChargeType.EV
                                                          Select x Order By x.DueDate, x.WESMBillBatchNo, x.BillPeriod, x.EndingBalance).ToList()

                    If UpdatedWESMBillSummaryOnVATFIT.Count > 0 And OffsetARCollProc3.TotalVATCollectionPerBP.Count > 0 Then
                        OffsetAPAllocProc3.ComputeVATAPAllocationList(AllocationDate, UpdatedWESMBillSummaryOnVATFIT,
                                                              OffsetARCollProc3.TotalVATCollectionPerBP)
                        For Each OffsetItem In OffsetAPAllocProc3.VATonEnergyAPAllocationList
                            Me._OffsettingVATonEnergyAllocationList.Add(OffsetItem)
                        Next
                        Me.AssignVATonEnergyShareToList(OffsetAPAllocProc3.VATonEnergyAPAllocationList)
                    End If

                    For Each ItemDMCM In OffsetAPAllocProc3.ListofDMCMAP
                        Me._ListofDMCM.Add(ItemDMCM)
                    Next
                End If
                If Me._OffsettingSequence > AMModule.OffsettingLimit And AMModule.OffsettingLimit <> 0 Then
                    Exit Do
                End If
            Loop

            'Dim _OffsetARCollProc As New ARCollectionProcess
            'Dim _OffsetAPAllocProc As New APAllocationProcess

            'With _OffsetARCollProc
            '    Me._OffsettingMFwithVATCollectionListDT = .Create_ARMFDT(Me.OffsettingMFwithVATCollectionList)
            '    Me._OffsettingEnergyCollectionListDT = .Create_OffsettingAREnergyDT(Me.OffsettingEnergyCollectionList)
            '    Me._OffsettingVATonEnergyCollectionListDT = .Create_OffsettingARVATDT(Me.OffsettingVATonEnergyCollectionList)
            'End With

            'With _OffsetAPAllocProc
            '    Me._OffsettingMFwithVATAllocationListDT = .CreateAPMFDT(Me.OffsettingMFwithVATAllocation)
            '    Me._OffsettingEnergyAllocationListDT = .CreateOffsetAPEnergyDT(Me.OffsettingEnergyAllocationList)
            '    Me._OffsettingVATonEnergyAllocationListDT = .CreateOffsetAPVATDT(Me.OffsettingVATonEnergyAllocationList)
            'End With
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
#End Region

#Region "MarketFees Offsetting Process"
    Private Function GetOffsettingARCollectionsForMFMFV(ByRef MFMFVWESMBillSummaryList As List(Of WESMBillSummary),
                                                        ByRef GetEnergyShare As List(Of PaymentShare),
                                                        ByVal DefaultInterestRate As Decimal,
                                                        ByVal AllocationDate As Date,
                                                        ByVal AMParticipants As List(Of AMParticipants)) As Boolean
        Dim oDic As New Dictionary(Of String, String)
        Dim ret As Boolean = False

        For Each Item In MFMFVWESMBillSummaryList
            Dim WESMBillMFMFVItem = (From x In MFMFVWESMBillSummaryList _
                                     Where x.IDNumber.IDNumber = Item.IDNumber.IDNumber _
                                     And x.INVDMCMNo = Item.INVDMCMNo Select x).ToList()
            Dim ParticipantShare As List(Of PaymentShare) = (From x In GetEnergyShare _
                                                             Where x.IDNumber = Item.IDNumber.IDNumber And x.AmountBalance > 0 _
                                                             Select x Order By x.AmountBalance Descending).ToList()

            Dim _AMParticipants As AMParticipants = (From x In AMParticipants Where x.IDNumber = Item.IDNumber.IDNumber Select x).FirstOrDefault()
            Dim TotalParticipantsEnergyShare = (From x In ParticipantShare Select x.AmountBalance).Sum()

            If Not oDic.ContainsKey(Item.INVDMCMNo) Then
                oDic.Add(Item.INVDMCMNo, Item.IDNumber.ToString)
                If TotalParticipantsEnergyShare > AMModule.MinimumAmountToBeOffset Then
                    Dim ListARColl As List(Of ARCollection) = Me.CalculateOffsettingARCollectionMFMFV(WESMBillMFMFVItem, AllocationDate, DefaultInterestRate, ParticipantShare, _AMParticipants)

                    For Each ARColl In ListARColl
                        Me._OffsettingMFwithVATCollListTemp.Add(ARColl)
                    Next
                    If ListARColl.Count > 0 Then
                        ret = True
                    End If
                End If
            End If
        Next
        Return ret
    End Function
#End Region

#Region "MarketFees FIT Offsetting Process"
    Private Function GetOffsettingARCollectionsForMFMFVFIT(ByRef MFMFVWESMBillSummaryList As List(Of WESMBillSummary),
                                                        ByRef GetEnergyFITShare As List(Of PaymentShare),
                                                        ByVal DefaultInterestRate As Decimal,
                                                        ByVal AllocationDate As Date,
                                                        ByVal AMParticipants As List(Of AMParticipants)) As Boolean
        Dim oDic As New Dictionary(Of String, String)
        Dim ret As Boolean = False

        Dim GroupMFWESMBillSummaryList As List(Of Long) = (From x In MFMFVWESMBillSummaryList Select x.WESMBillBatchNo Order By WESMBillBatchNo).Distinct.ToList()

        For Each objWBBNo In GroupMFWESMBillSummaryList
            Dim GetGroupMFWESMBillSummaryList As List(Of WESMBillSummary) = (From x In MFMFVWESMBillSummaryList _
                                                                             Where x.WESMBillBatchNo = objWBBNo Select x).ToList()

            Dim DistinctGroupMFWESMBillSummaryList = (From x In GetGroupMFWESMBillSummaryList Select x.INVDMCMNo).Distinct.ToList()
            For Each Item In DistinctGroupMFWESMBillSummaryList
                Dim WESMBillMFMFVItem = (From x In MFMFVWESMBillSummaryList _
                                         Where x.INVDMCMNo = Item Select x).ToList()

                Dim getFromAnotherWBatchWithHighestShare = (From x In GetEnergyFITShare _
                                                               Where x.ChargeType = EnumChargeType.E _
                                                               And x.IDNumber.EndsWith(AMModule.FITParticipantCode.ToString) _
                                                               And x.AmountBalance > 0 _
                                                               Group x By x.WESMBillBatchNo _
                                                               Into TotalAmountSharePerBatch = Sum(x.AmountBalance) _
                                                               Order By TotalAmountSharePerBatch Descending).FirstOrDefault()

                Dim FITParticipantShare As List(Of PaymentShare) = (From x In GetEnergyFITShare _
                                                                    Where x.ChargeType = EnumChargeType.E _
                                                                    And x.IDNumber.EndsWith(AMModule.FITParticipantCode.ToString) And x.AmountBalance > 0 _
                                                                    And x.WESMBillBatchNo = getFromAnotherWBatchWithHighestShare.WESMBillBatchNo _
                                                                    And x.PaymentType <> EnumPaymentNewType.DeferredAppliedEnergy _
                                                                    Select x Order By x.AmountBalance Descending).ToList()

                Dim _AMParticipants As AMParticipants = (From x In AMParticipants Where x.IDNumber = WESMBillMFMFVItem.FirstOrDefault.IDNumber.IDNumber Select x).FirstOrDefault()
                Dim TotalParticipantsEnergyShare = (From x In FITParticipantShare Select x.AmountBalance).Sum()

                Dim SumOfMFFitAR = (From x In GetGroupMFWESMBillSummaryList _
                                    Where x.IDNumber.IDNumber.EndsWith(AMModule.FITParticipantCode.ToString) _
                                    Select x.EndingBalance).Sum()

                If TotalParticipantsEnergyShare > AMModule.MinimumAmountToBeOffset Then
                    Dim ListARColl As List(Of ARCollection) = Me.CalculateOffsettingARCollectionMFMFVFIT(WESMBillMFMFVItem, AllocationDate, DefaultInterestRate, FITParticipantShare, _AMParticipants, SumOfMFFitAR)
                    For Each ARColl In ListARColl
                        Me._OffsettingMFwithVATCollListTemp.Add(ARColl)
                    Next
                    If ListARColl.Count > 0 Then
                        ret = True
                    End If
                End If
            Next
        Next

        Return ret
    End Function
#End Region

#Region "Energy Offsetting Process"
    Private Function GetOffsettingARCollectionsForEnergy(ByRef EnergyWESMBillSummaryList As List(Of WESMBillSummary),
                                                         ByRef GetEnergyShare As List(Of PaymentShare),
                                                         ByVal DefaultInterestRate As Decimal,
                                                         ByVal AllocationDate As Date,
                                                         ByVal AMParticipants As List(Of AMParticipants)) As Boolean
        Dim ret As Boolean = False

        For Each Item In EnergyWESMBillSummaryList
            Dim ParticipantsShare As New List(Of PaymentShare)
            If Item.INVDMCMNo.ToUpper.Contains("TS-") And Item.INVDMCMNo.ToUpper.Contains("ADJ") Then
                ParticipantsShare = (From x In GetEnergyShare
                                     Where x.IDNumber = Item.IDNumber.IDNumber And x.AmountBalance > 0
                                     Select x Order By x.AmountBalance Descending).ToList()
            Else
                ParticipantsShare = (From x In GetEnergyShare
                                     Where x.IDNumber = Item.IDNumber.IDNumber And x.AmountBalance > 0 And x.AllowOffsetToAR = True
                                     Select x Order By x.AmountBalance Descending).ToList()
            End If

            Dim _AMParticipants As AMParticipants = (From x In AMParticipants Where x.IDNumber = Item.IDNumber.IDNumber Select x).FirstOrDefault()

            Dim TotalParticipantsEnergyShare = (From x In ParticipantsShare Select x.AmountBalance).Sum()
            'Dim TotalParticipantsEnergyShareRatio As Decimal = Math.Round(TotalParticipantsEnergyShareRatio / Math.Abs(Item.EndingBalance), 2)            
            If TotalParticipantsEnergyShare > AMModule.MinimumAmountToBeOffset Then  'Or TotalParticipantsEnergyShareRatio >= 0.01 Then
                Dim ListARColl = Me.CalculateOffsettingARCollection(Item, AllocationDate, DefaultInterestRate, ParticipantsShare, _AMParticipants)
                For Each ARColl In ListARColl
                    Me._OffsettingEnergyCollListTemp.Add(ARColl)
                Next
                If ListARColl.Count > 0 Then
                    ret = True
                End If
            End If
        Next
        Return ret
    End Function
#End Region

#Region "Energy FIT Offsetting Process"
    Private Function GetOffsettingARCollectionsForEnergyFIT(ByRef EnergyWESMBillSummaryList As List(Of WESMBillSummary),
                                                            ByRef GetEnergyFITShare As List(Of PaymentShare),
                                                            ByVal DefaultInterestRate As Decimal,
                                                            ByVal AllocationDate As Date,
                                                            ByVal AMParticipants As List(Of AMParticipants)) As Boolean
        Dim ret As Boolean = False
        Dim GroupEnergyWESMBillSummaryList As List(Of Long) = (From x In EnergyWESMBillSummaryList Select x.WESMBillBatchNo Order By WESMBillBatchNo).Distinct.ToList()

        For Each objWBBNo In GroupEnergyWESMBillSummaryList
            Dim GetGroupEnergyWESMBillSummaryARList As List(Of WESMBillSummary) = (From x In EnergyWESMBillSummaryList _
                                                                                    Where x.WESMBillBatchNo = objWBBNo Select x).ToList()
            For Each item In GetGroupEnergyWESMBillSummaryARList


                Dim FITParticipantShare As List(Of PaymentShare) = (From x In GetEnergyFITShare
                                                                    Where x.ChargeType = EnumChargeType.E _
                                                                    And x.IDNumber.EndsWith(AMModule.FITParticipantCode.ToString) And x.AmountBalance > 0 _
                                                                    And x.WESMBillBatchNo = objWBBNo _
                                                                    And x.PaymentType <> EnumPaymentNewType.DeferredAppliedEnergy _
                                                                    And x.AllowOffsetToAR = True
                                                                    Select x Order By x.AmountBalance Descending).ToList()

                If FITParticipantShare.Count = 0 Then
                    Dim getFromAnotherWBatchWithHighestShare = (From x In GetEnergyFITShare
                                                                Where x.ChargeType = EnumChargeType.E _
                                                                And x.IDNumber.EndsWith(AMModule.FITParticipantCode.ToString) _
                                                                And x.AmountBalance > 0 _
                                                                    And x.AllowOffsetToAR = True
                                                                Group x By x.WESMBillBatchNo
                                                                Into TotalAmountSharePerBatch = Sum(x.AmountBalance)
                                                                Order By TotalAmountSharePerBatch Descending).FirstOrDefault()

                    If Not getFromAnotherWBatchWithHighestShare Is Nothing Then
                        FITParticipantShare = (From x In GetEnergyFITShare
                                               Where x.ChargeType = EnumChargeType.E _
                                                And x.IDNumber.EndsWith(AMModule.FITParticipantCode.ToString) And x.AmountBalance > 0 _
                                                And x.WESMBillBatchNo = getFromAnotherWBatchWithHighestShare.WESMBillBatchNo _
                                                And x.PaymentType <> EnumPaymentNewType.DeferredAppliedEnergy _
                                                And x.AllowOffsetToAR = True
                                               Select x Order By x.AmountBalance Descending).ToList()
                    End If
                End If

                Dim _AMParticipants As AMParticipants = (From x In AMParticipants Where x.IDNumber = item.IDNumber.IDNumber Select x).FirstOrDefault()
                Dim TotalParticipantsEnergyShare = (From x In FITParticipantShare Select x.AmountBalance).Sum()
                Dim SumOfEnergyFitAR = (From x In GetGroupEnergyWESMBillSummaryARList Select x.EndingBalance).Sum()                
                If TotalParticipantsEnergyShare >= Math.Abs(SumOfEnergyFitAR) And TotalParticipantsEnergyShare > AMModule.MinimumAmountToBeOffset Then
                    Dim ListARColl = Me.CalculateOffsettingARCollectionFIT(item, AllocationDate, DefaultInterestRate, FITParticipantShare, _AMParticipants, SumOfEnergyFitAR)
                    For Each ARColl In ListARColl
                        Me._OffsettingEnergyCollListTemp.Add(ARColl)
                    Next
                    If ListARColl.Count > 0 Then
                        ret = True
                    End If
                Else
                    Exit For
                End If
            Next
        Next

        Return ret
    End Function
#End Region

#Region "EnergyVAT Offsetting Process"
    'Offsetting Between VATShare to VAT AR Invoices
    Private Function GetOffsettingARCollectionsForEnergyVAT(ByRef VATWESMBillSummaryList As List(Of WESMBillSummary),
                                                            ByRef GetVATOnEnergyShare As List(Of PaymentShare),
                                                            ByVal AllocationDate As Date,
                                                            ByVal AMParticipants As List(Of AMParticipants)) As Boolean
        Dim ret As Boolean = False

        For Each Item In VATWESMBillSummaryList
            Dim ParticipantsVATShare As New List(Of PaymentShare)
            If Item.INVDMCMNo.Contains("TS-") And Item.INVDMCMNo.Contains("ADJ") Then
                ParticipantsVATShare = (From x In GetVATOnEnergyShare
                                        Where x.IDNumber = Item.IDNumber.IDNumber And x.AmountBalance > 0
                                        Select x Order By x.AmountBalance Descending).ToList()
            Else
                ParticipantsVATShare = (From x In GetVATOnEnergyShare
                                        Where x.IDNumber = Item.IDNumber.IDNumber And x.AmountBalance > 0 And x.AllowOffsetToAR = True
                                        Select x Order By x.AmountBalance Descending).ToList()
            End If

            Dim _AMParticipants As AMParticipants = (From x In AMParticipants Where x.IDNumber = Item.IDNumber.IDNumber Select x).FirstOrDefault()
                Dim TotalParticipantsVATShare = (From x In ParticipantsVATShare Select x.AmountBalance).Sum()
                Dim TotalParticipantsVATShareRatio As Decimal = Math.Round(TotalParticipantsVATShare / Math.Abs(Item.EndingBalance), 2)
                If TotalParticipantsVATShare > AMModule.MinimumAmountToBeOffset Then 'Or TotalParticipantsVATShareRatio >= 0.01 Then

                    Dim ListARColl = Me.CalculateOffsettingARCollection(Item, AllocationDate, 0, ParticipantsVATShare, _AMParticipants)
                    For Each ARColl In ListARColl
                        Me._OffsettingVATonEnergyCollectionListTemp.Add(ARColl)
                    Next
                    If ListARColl.Count > 0 Then
                        ret = True
                    End If
                End If

        Next
        Return ret
    End Function

    'Offsetting between EnergyShare to VAT AR Invoices Note: This Function is used for Generator Only.
    Private Function GetOffsettingARCollectionsForEnergyVAT2(ByRef VATWESMBillSummaryList As List(Of WESMBillSummary),
                                                            ByVal AllocationDate As Date,
                                                            ByVal AMParticipants As List(Of AMParticipants)) As Boolean
        Dim ret As Boolean = False
        For Each Item In VATWESMBillSummaryList
            Dim ParticipantsEnergyShare As New List(Of PaymentShare)
            If Item.INVDMCMNo.Contains("TS-") And Item.INVDMCMNo.Contains("ADJ") Then
                ParticipantsEnergyShare = (From x In Me.EnergyShare
                                           Where x.IDNumber = Item.IDNumber.IDNumber And x.ChargeType = EnumChargeType.E _
                                           And x.AmountBalance > 0
                                           Select x Order By x.AmountBalance Descending).ToList()
            Else
                ParticipantsEnergyShare = (From x In Me.EnergyShare
                                           Where x.IDNumber = Item.IDNumber.IDNumber And x.ChargeType = EnumChargeType.E _
                                           And x.AmountBalance > 0 And x.AllowOffsetToAR = True
                                           Select x Order By x.AmountBalance Descending).ToList()
            End If

            Dim _AMParticipants As AMParticipants = (From x In AMParticipants Where x.IDNumber = Item.IDNumber.IDNumber Select x).FirstOrDefault()

            Dim TotalParticipantsEnergyShare = (From x In ParticipantsEnergyShare Select x.AmountBalance).Sum()
            Dim TotalParticipantsEnergyShareRatio As Decimal = Math.Round(TotalParticipantsEnergyShare / Math.Abs(Item.EndingBalance), 2)

            If TotalParticipantsEnergyShare > AMModule.MinimumAmountToBeOffset Then 'Or TotalParticipantsEnergyShareRatio >= 0.01 Then
                Dim ListARColl = Me.CalculateOffsettingARCollection(Item, AllocationDate, 0, ParticipantsEnergyShare, _AMParticipants)
                For Each ARColl In ListARColl
                    Me._OffsettingVATonEnergyCollectionListTemp.Add(ARColl)
                Next
                If ListARColl.Count > 0 Then
                    ret = True
                End If
            End If
        Next
        Return ret
    End Function

#End Region

#Region "EnergyVAT FIT Offsetting Process"
    Private Function GetOffsettingARCollectionsForEnergyVATFIT(ByRef VATWESMBillSummaryList As List(Of WESMBillSummary),
                                                               ByVal GetVATonEnergyShare As List(Of PaymentShare),
                                                               ByVal AllocationDate As Date,
                                                               ByVal AMParticipants As List(Of AMParticipants)) As Boolean
        Dim ret As Boolean = False
        Dim GroupVATWESMBillSummaryList As List(Of Long) = (From x In VATWESMBillSummaryList Select x.WESMBillBatchNo Order By WESMBillBatchNo).Distinct.ToList()

        For Each objWBBNo In GroupVATWESMBillSummaryList
            Dim GetGroupEnergyWESMBillSummaryList As List(Of WESMBillSummary) = (From x In VATWESMBillSummaryList _
                                                                                 Where x.WESMBillBatchNo = objWBBNo Select x).ToList()

            For Each item In GetGroupEnergyWESMBillSummaryList

                Dim FITParticipantShare As List(Of PaymentShare) = (From x In GetVATonEnergyShare
                                                                    Where x.ChargeType = EnumChargeType.EV _
                                                                  And x.IDNumber.EndsWith(AMModule.FITParticipantCode.ToString) And x.AmountBalance > 0 _
                                                                  And x.WESMBillBatchNo = item.WESMBillBatchNo And x.AllowOffsetToAR = True
                                                                    Select x Order By x.AmountBalance Descending).ToList()
                If FITParticipantShare.Count = 0 Then
                    Dim GetFromAnotherWBatch = (From x In GetVATonEnergyShare
                                                Where x.ChargeType = EnumChargeType.EV _
                                                And x.IDNumber.EndsWith(AMModule.FITParticipantCode.ToString) And x.AmountBalance > 0 _
                                                    And x.AllowOffsetToAR = True
                                                Select x Order By x.AmountBalance Descending).ToList()

                    Dim GetMinWBatch As Long = (From x In GetFromAnotherWBatch Select x.WESMBillBatchNo Order By WESMBillBatchNo).FirstOrDefault
                    FITParticipantShare = (From x In GetVATonEnergyShare
                                           Where x.ChargeType = EnumChargeType.EV _
                                            And x.IDNumber.EndsWith(AMModule.FITParticipantCode.ToString) And x.AmountBalance > 0 _
                                            And x.WESMBillBatchNo = GetMinWBatch And x.AllowOffsetToAR = True
                                           Select x Order By x.AmountBalance Descending).ToList()
                End If

                Dim _AMParticipants As AMParticipants = (From x In AMParticipants Where x.IDNumber = item.IDNumber.IDNumber Select x).FirstOrDefault()
                Dim TotalParticipantsEnergyShare = (From x In FITParticipantShare Select x.AmountBalance).Sum()
                Dim SumOfEnergyFitAR = (From x In GetGroupEnergyWESMBillSummaryList Select x.EndingBalance).Sum()

                Dim FitShareRatio As Decimal = Math.Round(Math.Abs(TotalParticipantsEnergyShare / Math.Abs(item.BeginningBalance)), 2)

                If TotalParticipantsEnergyShare > AMModule.MinimumAmountToBeOffset Then 'Or FitShareRatio >= 0.01 Then
                    Dim ListARColl = Me.CalculateOffsettingARCollectionFIT(item, AllocationDate, 0, FITParticipantShare, _AMParticipants, SumOfEnergyFitAR)
                    For Each ARColl In ListARColl
                        Me._OffsettingVATonEnergyCollectionListTemp.Add(ARColl)
                    Next
                    If ListARColl.Count > 0 Then
                        ret = True
                    End If
                End If
            Next
        Next
        Return ret
    End Function
#End Region

#Region "Functions for Fit Offsetting"
    Private Function CalculateOffsettingARCollectionFIT(ByRef WESMBillSummaryItem As WESMBillSummary,
                                                        ByVal AllocationDate As Date,
                                                        ByVal DefaultInterestRATE As Decimal,
                                                        ByRef ParticipantsShare As List(Of PaymentShare),
                                                        ByVal ParticipantsInfo As AMParticipants,
                                                        ByVal SumOfFITAR As Decimal) As List(Of ARCollection)
        Dim ret As New List(Of ARCollection)
        Dim DefaultInterestAmount As Decimal = 0
        Dim _BusinessFactory As New BusinessFactory
        Dim RatioDic As New Dictionary(Of String, Decimal)
        Dim ShareDetailsDic As New Dictionary(Of Long, Integer)
        _BusinessFactory = BusinessFactory.GetInstance()



        With WESMBillSummaryItem
            DefaultInterestAmount = _BusinessFactory.ComputeDefaultInterest(.DueDate, .NewDueDate, AllocationDate, (.EndingBalance - .EnergyWithhold), DefaultInterestRATE)
        End With
        Select Case WESMBillSummaryItem.ChargeType
            Case EnumChargeType.E
                Dim EnergyAR As New List(Of ARCollection)

                Dim OffsetBaseAmount As Decimal = WESMBillSummaryItem.EndingBalance - If(WESMBillSummaryItem.EnergyWithholdStatus = EnumEnergyWithholdStatus.NotApplicable, WESMBillSummaryItem.EnergyWithhold, 0)
                Dim OffsetDefaultAmount As Decimal = DefaultInterestAmount
                Dim OffsetTotalAmount As Decimal = 0D
                Dim AmountShareDeductedTotal As Decimal = 0
                Dim AmountShareDeductedForEnergy As Decimal = 0
                Dim AmountShareDeductedForEnergyDI As Decimal = 0
                Dim ARCollectionOnEnergy As New ARCollection
                Dim ARCollectionOnEnergyDefault As New ARCollection
                Dim _OffsettingShareDetails As New PaymentShareDetails

                OffsetTotalAmount = OffsetBaseAmount + OffsetDefaultAmount

                Dim TotalShareOfParticipant = (From x In ParticipantsShare Select x.AmountBalance).Sum()
                Dim WESMInvoice As String = WESMBillSummaryItem.INVDMCMNo
                Dim ListOfOffsettingShareDetails As New List(Of PaymentShareDetails)
                Dim ListOfIndex As New List(Of Integer)

                If Math.Abs(SumOfFITAR) >= TotalShareOfParticipant Then
                    If TotalShareOfParticipant >= Math.Abs(OffsetDefaultAmount) Then

                        Dim ShareRatio As Decimal = Math.Abs(WESMBillSummaryItem.EndingBalance / SumOfFITAR)
                        Dim getOffsetBaseAmountRatio As Decimal = Math.Abs(OffsetBaseAmount / (OffsetBaseAmount + OffsetDefaultAmount))
                        Dim getOffsetDefaultAmountRatio As Decimal = Math.Abs(OffsetDefaultAmount / (OffsetBaseAmount + OffsetDefaultAmount))

                        Dim TotalAmountShouldBeDeducted As Decimal = Math.Round(TotalShareOfParticipant * ShareRatio, 2)

                        Dim AmountShouldBeDeductedOnEnergy As Decimal = Math.Round(TotalAmountShouldBeDeducted * getOffsetBaseAmountRatio, 2)
                        Dim AmountShouldBeDeductedOnEnergyDI = Math.Round(TotalAmountShouldBeDeducted * getOffsetDefaultAmountRatio, 2)

                        AmountShareDeductedForEnergy += AmountShouldBeDeductedOnEnergy
                        AmountShareDeductedForEnergyDI += AmountShouldBeDeductedOnEnergyDI
                        AmountShareDeductedTotal += TotalAmountShouldBeDeducted

                        Dim DistinctIDShare As List(Of String) = (From x In ParticipantsShare Select x.IDNumber).Distinct.ToList 'distinct participant id that have 2 or more share 
                        Dim intIndx As Integer = 0                        

                        For Each id In DistinctIDShare
                            Dim selectShare As List(Of PaymentShare) = (From x In ParticipantsShare Where x.IDNumber = id Select x Order By x.AmountBalance Descending).ToList
                            Dim shareTotalPerID As Decimal = (From x In selectShare Select x.AmountBalance).Sum
                            Dim shareRatioPerID As Decimal = shareTotalPerID / TotalShareOfParticipant

                            Dim TotalSharePerIDDeducted As Decimal = Math.Round(TotalAmountShouldBeDeducted * shareRatioPerID, 2)
                            Dim ArrIndex As New List(Of Integer)
                            'Check if have shares calculated                           
                            If TotalSharePerIDDeducted <> 0 And shareTotalPerID <> 0 Then
                                Dim TotalDeductedAmountperShare As Decimal = 0
                                For i As Integer = 0 To selectShare.Count - 1
                                    Dim ShareItem As PaymentShare = selectShare(i)
                                    Dim computeRatioPerShare As Decimal = selectShare(i).AmountBalance / shareTotalPerID
                                    Dim DeductedAmountperShare = Math.Round(TotalSharePerIDDeducted * computeRatioPerShare, 2)
                                    TotalDeductedAmountperShare += DeductedAmountperShare
                                    If DeductedAmountperShare <> 0 Then
                                        If Not ShareDetailsDic.ContainsKey(selectShare(i).PaymentShareNo) Then
                                            ShareDetailsDic.Add(selectShare(i).PaymentShareNo, intIndx)
                                        End If
                                        _OffsettingShareDetails = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryItem, DeductedAmountperShare)
                                        ListOfOffsettingShareDetails.Add(_OffsettingShareDetails)
                                        ListOfIndex.Add(intIndx) 'add index of selected share per participant
                                        ArrIndex.Add(intIndx)   'add index of selected share per participant. It will be used for adjustment.
                                        intIndx += 1
                                    End If
                                Next
                                'Check if the actual amount that should be deducted is not equal to the amount that has been deducted.
                                If TotalSharePerIDDeducted <> TotalDeductedAmountperShare Then
                                    'if entered here total share deducted from the participant should be adjusted.                                
                                    Dim TotalAdjustedAmount As Decimal = Math.Round(TotalSharePerIDDeducted - TotalDeductedAmountperShare, 2)
                                    Dim GetListOfIndex As Integer = (From x In ListOfIndex Where x = (From y In ArrIndex Select y).First Select x).First
                                    ListOfOffsettingShareDetails(GetListOfIndex).OffsetAmount += TotalAdjustedAmount
                                End If
                            End If
                        Next

                        'Check if Total of deducted amount is equal to the amount that should be deducted on Energy Offsetting
                        If AmountShareDeductedTotal <> (AmountShareDeductedForEnergy + AmountShareDeductedForEnergyDI) Then

                            Dim TotalAdjustedAmount As Decimal = AmountShareDeductedTotal - (AmountShareDeductedForEnergy + AmountShareDeductedForEnergyDI)

                            If AmountShareDeductedForEnergy <> 0 And AmountShareDeductedForEnergyDI <> 0 Then
                                If AmountShareDeductedForEnergy >= AmountShareDeductedForEnergyDI Then
                                    AmountShareDeductedForEnergy += TotalAdjustedAmount
                                Else
                                    AmountShareDeductedForEnergyDI += TotalAdjustedAmount
                                End If
                            ElseIf AmountShareDeductedForEnergy <> 0 And AmountShareDeductedForEnergyDI = 0 Then
                                AmountShareDeductedForEnergy += TotalAdjustedAmount
                            ElseIf AmountShareDeductedForEnergy = 0 And AmountShareDeductedForEnergyDI <> 0 Then
                                AmountShareDeductedForEnergyDI += TotalAdjustedAmount
                            End If

                            'If no paymentsharedetails created but it has adjustment, create paymentsharedetails and put it to the participant with higher share.
                            If ListOfOffsettingShareDetails.Count = 0 And TotalAdjustedAmount <> 0 Then
                                _OffsettingShareDetails = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryItem, TotalAdjustedAmount)
                                Dim GetParticipantShare = (From x In ParticipantsShare Select x).FirstOrDefault
                                GetParticipantShare.AmountOffset.Add(_OffsettingShareDetails)
                            End If

                            Dim GetHigherOffsettingSHareDetails As PaymentShareDetails = (From x In ListOfOffsettingShareDetails Select x Order By x.OffsetAmount Descending).FirstOrDefault
                            If Not GetHigherOffsettingSHareDetails Is Nothing Then
                                GetHigherOffsettingSHareDetails.OffsetAmount += TotalAdjustedAmount
                            End If
                            'loop and put in participant with positive share
                            If ShareDetailsDic.Count <> 0 Then
                                For Each Key In ParticipantsShare
                                    If ShareDetailsDic.ContainsKey(Key.PaymentShareNo) Then
                                        Dim GetPSDetails As PaymentShareDetails = ListOfOffsettingShareDetails(ShareDetailsDic.Item(Key.PaymentShareNo))
                                        Key.AmountOffset.Add(GetPSDetails)
                                    End If
                                Next
                            End If
                        Else
                            If ShareDetailsDic.Count <> 0 Then
                                For Each Key In ParticipantsShare
                                    If ShareDetailsDic.ContainsKey(Key.PaymentShareNo) Then
                                        Dim GetPSDetails As PaymentShareDetails = ListOfOffsettingShareDetails(ShareDetailsDic.Item(Key.PaymentShareNo))
                                        Key.AmountOffset.Add(GetPSDetails)
                                    End If
                                Next

                            End If
                        End If

                        If AmountShareDeductedForEnergyDI <> 0 Then
                            ARCollectionOnEnergyDefault = Me.CreateOffsettingARCollection(WESMBillSummaryItem, AllocationDate, AmountShareDeductedForEnergyDI * -1, EnumCollectionType.DefaultInterestOnEnergy)
                            EnergyAR.Add(ARCollectionOnEnergyDefault)
                        End If

                        If AmountShareDeductedForEnergy <> 0 Then
                            ARCollectionOnEnergy = Me.CreateOffsettingARCollection(WESMBillSummaryItem, AllocationDate, AmountShareDeductedForEnergy * -1, EnumCollectionType.Energy)
                            EnergyAR.Add(ARCollectionOnEnergy)
                        End If
                    End If

                Else
                    If TotalShareOfParticipant >= Math.Abs(OffsetDefaultAmount) Then
                        Dim GetListOfShareForRatio = (From x In ParticipantsShare _
                                                      Group By IDNumber = x.IDNumber _
                                                      Into TTARatio = Sum(x.AmountShare)).ToList()

                        Dim GetListOfTotalShare = (From x In GetListOfShareForRatio Select x.TTARatio).Sum()

                        For Each item In GetListOfShareForRatio
                            If Not RatioDic.ContainsKey(item.IDNumber) Then
                                Dim ComputedRatio As Decimal = item.TTARatio / GetListOfTotalShare
                                RatioDic.Add(item.IDNumber, ComputedRatio)
                            End If
                        Next

                        Dim DistinctIDShare As List(Of String) = (From x In ParticipantsShare Select x.IDNumber).Distinct.ToList 'distinct participant id that have 2 or more share 
                        Dim intIndx As Integer = 0

                        For Each id In DistinctIDShare
                            Dim ParticipantRatio As Decimal = RatioDic(id)
                            Dim AmountShouldBeDeductedOnEnergy As Decimal = Math.Round(OffsetBaseAmount * ParticipantRatio, 2) * -1
                            Dim AmountShouldBeDeductedOnEnergyDI = Math.Round(OffsetDefaultAmount * ParticipantRatio, 2) * -1
                            Dim TotalAmountShouldBeDeducted As Decimal = (AmountShouldBeDeductedOnEnergy + AmountShouldBeDeductedOnEnergyDI)

                            AmountShareDeductedForEnergy += AmountShouldBeDeductedOnEnergy
                            AmountShareDeductedForEnergyDI += AmountShouldBeDeductedOnEnergyDI
                            AmountShareDeductedTotal += TotalAmountShouldBeDeducted

                            Dim selectShare As List(Of PaymentShare) = (From x In ParticipantsShare Where x.IDNumber = id Select x Order By x.AmountBalance Descending).ToList
                            Dim shareTotalPerID As Decimal = (From x In selectShare Select x.AmountBalance).Sum
                            Dim TotalSharePerIDDeducted As Decimal = 0
                            Dim ArrIndex As New List(Of Integer)
                            'Check if have shares calculated                           
                            If TotalAmountShouldBeDeducted <> 0 And shareTotalPerID <> 0 Then
                                For i As Integer = 0 To selectShare.Count - 1
                                    Dim ShareItem As PaymentShare = selectShare(i)
                                    Dim computeRatioPerShare As Decimal = selectShare(i).AmountBalance / shareTotalPerID
                                    Dim DeductedAmountperShare As Decimal = Math.Round(computeRatioPerShare * TotalAmountShouldBeDeducted, 2)
                                    TotalSharePerIDDeducted += DeductedAmountperShare
                                    If DeductedAmountperShare >= 0 Then
                                        If Not ShareDetailsDic.ContainsKey(selectShare(i).PaymentShareNo) Then
                                            ShareDetailsDic.Add(selectShare(i).PaymentShareNo, intIndx)
                                        End If
                                        _OffsettingShareDetails = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryItem, DeductedAmountperShare)
                                        ListOfOffsettingShareDetails.Add(_OffsettingShareDetails)
                                        ListOfIndex.Add(intIndx) 'add index of selected share per participant
                                        ArrIndex.Add(intIndx)   'add index of selected share per participant. It will be used for adjustment.
                                        intIndx += 1
                                    End If
                                Next
                            End If

                            'Check if the actual amount that should be deducted is not equal to the amount that has been deducted.
                            If TotalSharePerIDDeducted <> TotalAmountShouldBeDeducted Then
                                'if entered here total share deducted from the participant should be adjusted.                                
                                Dim TotalAdjustedAmount As Decimal = Math.Round(TotalAmountShouldBeDeducted - TotalSharePerIDDeducted, 2)
                                Dim GetListOfIndex As Integer = (From x In ListOfIndex Where x = (From y In ArrIndex Select y).First Select x).First
                                ListOfOffsettingShareDetails(GetListOfIndex).OffsetAmount += TotalAdjustedAmount
                            End If
                        Next

                        'Check if Total of deducted amount is equal to the amount that should be deducted on Energy Offsetting
                        If AmountShareDeductedTotal <> Math.Abs(OffsetTotalAmount) Then
                            Dim AdjustedAmountForEnergy As Decimal = Math.Round(((AmountShareDeductedForEnergy * -1) - OffsetBaseAmount), 2) 'Lance
                            Dim AdjustedAmountForEnergyDI As Decimal = Math.Round(((AmountShareDeductedForEnergyDI * -1) - OffsetDefaultAmount), 2)
                            Dim TotalAdjustedAmount As Decimal = AdjustedAmountForEnergy + AdjustedAmountForEnergyDI
                            AmountShareDeductedForEnergy += AdjustedAmountForEnergy
                            AmountShareDeductedForEnergyDI += AdjustedAmountForEnergyDI

                            'If no paymentsharedetails created but it has adjustment, create paymentsharedetails and put it to the participant with higher share.
                            If ListOfOffsettingShareDetails.Count = 0 And TotalAdjustedAmount <> 0 Then
                                _OffsettingShareDetails = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryItem, TotalAdjustedAmount)
                                Dim GetParticipantShare = (From x In ParticipantsShare Select x).FirstOrDefault
                                GetParticipantShare.AmountOffset.Add(_OffsettingShareDetails)
                            End If

                            Dim GetHigherOffsettingSHareDetails As PaymentShareDetails = (From x In ListOfOffsettingShareDetails Select x Order By x.OffsetAmount Descending).FirstOrDefault
                            If Not GetHigherOffsettingSHareDetails Is Nothing Then
                                GetHigherOffsettingSHareDetails.OffsetAmount += TotalAdjustedAmount
                            End If
                            'loop and put in participant with positive share
                            For Each Key In ParticipantsShare
                                If ShareDetailsDic.ContainsKey(Key.PaymentShareNo) Then
                                    Dim GetPSDetails As PaymentShareDetails = ListOfOffsettingShareDetails(ShareDetailsDic.Item(Key.PaymentShareNo))
                                    Key.AmountOffset.Add(GetPSDetails)
                                End If
                            Next
                        Else
                            For Each Key In ParticipantsShare
                                If ShareDetailsDic.ContainsKey(Key.PaymentShareNo) Then
                                    Dim GetPSDetails As PaymentShareDetails = ListOfOffsettingShareDetails(ShareDetailsDic.Item(Key.PaymentShareNo))
                                    Key.AmountOffset.Add(GetPSDetails)
                                End If
                            Next
                        End If


                        If AmountShareDeductedForEnergyDI <> 0 Then
                            ARCollectionOnEnergyDefault = Me.CreateOffsettingARCollection(WESMBillSummaryItem, AllocationDate, AmountShareDeductedForEnergyDI * -1, EnumCollectionType.DefaultInterestOnEnergy)
                            EnergyAR.Add(ARCollectionOnEnergyDefault)
                        End If

                        If AmountShareDeductedForEnergy <> 0 Then
                            ARCollectionOnEnergy = Me.CreateOffsettingARCollection(WESMBillSummaryItem, AllocationDate, AmountShareDeductedForEnergy * -1, EnumCollectionType.Energy)
                            EnergyAR.Add(ARCollectionOnEnergy)
                        End If
                    End If
                End If
                ret = CreateDMCMonEEV(EnergyAR, AllocationDate, DefaultInterestRATE)
            Case EnumChargeType.EV
                Dim VATAR As New List(Of ARCollection)
                Dim OffsetBaseAmount As Decimal = WESMBillSummaryItem.EndingBalance
                Dim OffsetTotalAmount As Decimal = 0D
                Dim AmountShareDeductedForVAT As Decimal = 0 'Rounded by 2
                Dim ARCollectionOnVAT As New ARCollection
                Dim _OffsettingShareDetails As New PaymentShareDetails

                OffsetTotalAmount = OffsetBaseAmount

                Dim TotalShareOfParticipant = (From x In ParticipantsShare Select x.AmountBalance).Sum()
                Dim WESMInvoice As String = WESMBillSummaryItem.INVDMCMNo
                Dim ListOfOffsettingShareDetails As New List(Of PaymentShareDetails)
                Dim ListOfIndex As New List(Of Integer)
                If Math.Abs(SumOfFITAR) > TotalShareOfParticipant Then
                    Dim GetListOfShareForRatio = (From x In ParticipantsShare _
                                                      Group By IDNumber = x.IDNumber _
                                                      Into TTARatio = Sum(x.AmountShare)).ToList()
                    Dim GetListOfTotalShare = (From x In GetListOfShareForRatio Select x.TTARatio).Sum()

                    For Each item In GetListOfShareForRatio
                        If Not RatioDic.ContainsKey(item.IDNumber) Then
                            Dim ComputedRatio As Decimal = item.TTARatio / GetListOfTotalShare
                            RatioDic.Add(item.IDNumber, ComputedRatio)
                        End If
                    Next

                    Dim DistinctIDShare As List(Of String) = (From x In ParticipantsShare Select x.IDNumber).Distinct.ToList 'distinct participant id that have 2 or more share 
                    Dim intIndx As Integer = 0
                    For Each id In DistinctIDShare
                        Dim ParticipantRatio As Decimal = RatioDic(id)
                        Dim AmountShouldBeDeductedOnVAT As Decimal = Math.Round(OffsetBaseAmount * ParticipantRatio, 2) * -1
                        AmountShareDeductedForVAT += AmountShouldBeDeductedOnVAT

                        Dim selectShare As List(Of PaymentShare) = (From x In ParticipantsShare Where x.IDNumber = id Select x).ToList
                        Dim shareTotalPerID As Decimal = (From x In selectShare Select x.AmountBalance).Sum
                        Dim TotalSharePerIDDeducted As Decimal = 0
                        Dim ArrIndex As New List(Of Integer)
                        'Check if have shares calculated
                        If AmountShareDeductedForVAT <> 0 And shareTotalPerID <> 0 Then
                            For i As Integer = 0 To selectShare.Count - 1
                                Dim ShareItem As PaymentShare = selectShare(i)
                                Dim DeductedAmountperShare As Decimal = selectShare(i).AmountBalance
                                TotalSharePerIDDeducted += DeductedAmountperShare
                                If TotalSharePerIDDeducted <> 0 Then
                                    If Not ShareDetailsDic.ContainsKey(selectShare(i).PaymentShareNo) Then
                                        ShareDetailsDic.Add(selectShare(i).PaymentShareNo, intIndx)
                                    End If
                                    _OffsettingShareDetails = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryItem, DeductedAmountperShare)
                                    ListOfOffsettingShareDetails.Add(_OffsettingShareDetails)
                                    ListOfIndex.Add(intIndx) 'add index of selected share per participant
                                    ArrIndex.Add(intIndx)   'add index of selected share per participant. It will be used for adjustment.
                                    intIndx += 1
                                End If
                            Next
                        End If

                        'Check if the actual amount that should be deducted is not equal to the amount that has been deducted.                        
                        If TotalSharePerIDDeducted <> AmountShouldBeDeductedOnVAT Then
                            'if entered here total share deducted from the participant should be adjusted.                                
                            Dim TotalAdjustedAmount As Decimal = Math.Round(AmountShouldBeDeductedOnVAT - TotalSharePerIDDeducted, 2)
                            Dim GetListOfIndex As Integer = (From x In ListOfIndex Where x = (From y In ArrIndex Select y).First Select x).First
                            ListOfOffsettingShareDetails(GetListOfIndex).OffsetAmount += TotalAdjustedAmount
                        End If
                    Next

                    'Check if Total of deducted amount is equal to the amount that should be deducted on Energy Offsetting
                    If AmountShareDeductedForVAT <> OffsetBaseAmount Then
                        Dim AdjustedDeductedAmount As Decimal = Math.Round(((AmountShareDeductedForVAT * -1) - OffsetBaseAmount), 2)
                        AmountShareDeductedForVAT += AdjustedDeductedAmount

                        'If no paymentsharedetails created but it has adjustment, create paymentsharedetails and put it to the participant with higher share.
                        If ListOfOffsettingShareDetails.Count = 0 And AdjustedDeductedAmount <> 0 Then
                            _OffsettingShareDetails = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryItem, AdjustedDeductedAmount)
                            Dim GetParticipantShare = (From x In ParticipantsShare Select x).FirstOrDefault
                            GetParticipantShare.AmountOffset.Add(_OffsettingShareDetails)
                        End If

                        Dim GetHigherOffsettingSHareDetails As PaymentShareDetails = (From x In ListOfOffsettingShareDetails Select x Order By x.OffsetAmount Descending).FirstOrDefault
                        GetHigherOffsettingSHareDetails.OffsetAmount += AdjustedDeductedAmount

                        'loop and put in participant with positive share
                        For Each Key In ParticipantsShare
                            If ShareDetailsDic.ContainsKey(Key.PaymentShareNo) Then
                                Dim GetPSDetails As PaymentShareDetails = ListOfOffsettingShareDetails(ShareDetailsDic.Item(Key.PaymentShareNo))
                                Key.AmountOffset.Add(GetPSDetails)
                            End If
                        Next
                    Else
                        'loop and put in participant with positive share
                        For Each Key In ParticipantsShare
                            If ShareDetailsDic.ContainsKey(Key.PaymentShareNo) Then
                                Dim GetPSDetails As PaymentShareDetails = ListOfOffsettingShareDetails(ShareDetailsDic.Item(Key.PaymentShareNo))
                                Key.AmountOffset.Add(GetPSDetails)
                            End If
                        Next
                    End If

                    If AmountShareDeductedForVAT <> 0 Then
                        ARCollectionOnVAT = Me.CreateOffsettingARCollection(WESMBillSummaryItem, AllocationDate, AmountShareDeductedForVAT * -1, EnumCollectionType.VatOnEnergy)
                        VATAR.Add(ARCollectionOnVAT)
                    End If
                Else

                    Dim GetListOfShareForRatio = (From x In ParticipantsShare _
                                                      Group By IDNumber = x.IDNumber _
                                                      Into TTARatio = Sum(x.AmountShare)).ToList()

                    Dim GetListOfTotalShare = (From x In GetListOfShareForRatio Select x.TTARatio).Sum()

                    For Each item In GetListOfShareForRatio
                        If Not RatioDic.ContainsKey(item.IDNumber) Then
                            Dim ComputedRatio As Decimal = item.TTARatio / GetListOfTotalShare
                            RatioDic.Add(item.IDNumber, ComputedRatio)
                        End If
                    Next

                    Dim DistinctIDShare As List(Of String) = (From x In ParticipantsShare Select x.IDNumber).Distinct.ToList 'distinct participant id that have 2 or more share 
                    Dim intIndx As Integer = 0
                    For Each id In DistinctIDShare
                        Dim ParticipantRatio As Decimal = RatioDic(id)
                        Dim AmountShouldBeDeductedOnVAT As Decimal = Math.Round(OffsetBaseAmount * ParticipantRatio, 2) * -1
                        AmountShareDeductedForVAT += AmountShouldBeDeductedOnVAT

                        Dim selectShare As List(Of PaymentShare) = (From x In ParticipantsShare Where x.IDNumber = id Select x).ToList
                        Dim shareTotalPerID As Decimal = (From x In selectShare Select x.AmountBalance).Sum
                        Dim TotalSharePerIDDeducted As Decimal = 0
                        Dim ArrIndex As New List(Of Integer)
                        'Check if have shares calculated
                        If AmountShareDeductedForVAT <> 0 And shareTotalPerID <> 0 Then
                            For i As Integer = 0 To selectShare.Count - 1
                                Dim ShareItem As PaymentShare = selectShare(i)
                                Dim computeRatioPerShare As Decimal = selectShare(i).AmountShare / shareTotalPerID
                                Dim DeductedAmountperShare As Decimal = Math.Round(computeRatioPerShare * AmountShouldBeDeductedOnVAT, 2)
                                TotalSharePerIDDeducted += DeductedAmountperShare
                                If TotalSharePerIDDeducted <> 0 Then
                                    If Not ShareDetailsDic.ContainsKey(selectShare(i).PaymentShareNo) Then
                                        ShareDetailsDic.Add(selectShare(i).PaymentShareNo, intIndx)
                                    End If
                                    _OffsettingShareDetails = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryItem, DeductedAmountperShare)
                                    ListOfOffsettingShareDetails.Add(_OffsettingShareDetails)
                                    ListOfIndex.Add(intIndx) 'add index of selected share per participant
                                    ArrIndex.Add(intIndx)   'add index of selected share per participant. It will be used for adjustment.
                                    intIndx += 1
                                End If
                            Next
                        End If

                        'Check if the actual amount that should be deducted is not equal to the amount that has been deducted.                        
                        If TotalSharePerIDDeducted <> AmountShouldBeDeductedOnVAT Then
                            'if entered here total share deducted from the participant should be adjusted.                                
                            Dim TotalAdjustedAmount As Decimal = Math.Round(AmountShouldBeDeductedOnVAT - TotalSharePerIDDeducted, 2)
                            Dim GetListOfIndex As Integer = (From x In ListOfIndex Where x = (From y In ArrIndex Select y).First Select x).First
                            ListOfOffsettingShareDetails(GetListOfIndex).OffsetAmount += TotalAdjustedAmount
                        End If
                    Next

                    'Check if Total of deducted amount is equal to the amount that should be deducted on Energy Offsetting
                    If AmountShareDeductedForVAT <> OffsetBaseAmount Then
                        Dim AdjustedDeductedAmount As Decimal = Math.Round(((AmountShareDeductedForVAT * -1) - OffsetBaseAmount), 2)
                        AmountShareDeductedForVAT += AdjustedDeductedAmount

                        'If no paymentsharedetails created but it has adjustment, create paymentsharedetails and put it to the participant with higher share.
                        If ListOfOffsettingShareDetails.Count = 0 And AdjustedDeductedAmount <> 0 Then
                            _OffsettingShareDetails = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryItem, AdjustedDeductedAmount)
                            Dim GetParticipantShare = (From x In ParticipantsShare Select x).FirstOrDefault
                            GetParticipantShare.AmountOffset.Add(_OffsettingShareDetails)
                        End If
                        Dim GetHigherOffsettingSHareDetails As PaymentShareDetails = (From x In ListOfOffsettingShareDetails Select x Order By x.OffsetAmount Descending).FirstOrDefault
                        GetHigherOffsettingSHareDetails.OffsetAmount += AdjustedDeductedAmount

                        For Each Key In ParticipantsShare
                            If ShareDetailsDic.ContainsKey(Key.PaymentShareNo) Then
                                Dim GetPSDetails As PaymentShareDetails = ListOfOffsettingShareDetails(ShareDetailsDic.Item(Key.PaymentShareNo))
                                Key.AmountOffset.Add(GetPSDetails)
                            End If
                        Next
                    Else
                        For Each Key In ParticipantsShare
                            If ShareDetailsDic.ContainsKey(Key.PaymentShareNo) Then
                                Dim GetPSDetails As PaymentShareDetails = ListOfOffsettingShareDetails(ShareDetailsDic.Item(Key.PaymentShareNo))
                                Key.AmountOffset.Add(GetPSDetails)
                            End If
                        Next
                    End If

                    ARCollectionOnVAT = Me.CreateOffsettingARCollection(WESMBillSummaryItem, AllocationDate, AmountShareDeductedForVAT * -1, EnumCollectionType.VatOnEnergy)
                    VATAR.Add(ARCollectionOnVAT)
                End If
                ret = CreateDMCMonEEV(VATAR, AllocationDate, DefaultInterestRATE)
        End Select
        Return ret
    End Function
#End Region

#Region "Functions For Offsetting MF and MFVAT"
    Private Function CalculateOffsettingARCollectionMFMFV(ByRef WESMBillSummaryItem As List(Of WESMBillSummary),
                                                        ByVal AllocationDate As Date,
                                                        ByVal DefaultInterestRATE As Decimal,
                                                        ByRef ParticipantsShare As List(Of PaymentShare),
                                                        ByVal ParticipantsInfo As AMParticipants) As List(Of ARCollection)
        Dim MarketFeesAR As New List(Of ARCollection)
        Dim ret As New List(Of ARCollection)
        Dim DIonMF As Decimal = 0
        Dim DIonMFV As Decimal = 0
        Dim _BusinessFactory As New BusinessFactory
        _BusinessFactory = BusinessFactory.GetInstance()
        Dim WESMBillSummaryMF As WESMBillSummary
        Dim WESMBillSummaryMFV As WESMBillSummary

        WESMBillSummaryMF = (From x In WESMBillSummaryItem Where x.ChargeType = EnumChargeType.MF Select x).FirstOrDefault
        WESMBillSummaryMFV = (From x In WESMBillSummaryItem Where x.ChargeType = EnumChargeType.MFV Select x).FirstOrDefault

        Dim MFEndingBalance As Decimal = 0D
        If Not WESMBillSummaryMF Is Nothing Then
            With WESMBillSummaryMF
                If .NewDueDate = AllocationDate Then
                    DIonMF = 0D
                Else
                    If .SummaryType = EnumSummaryType.DMCM Then
                        DIonMF = 0D
                    Else
                        DIonMF = _BusinessFactory.ComputeDefaultInterest(.DueDate, .NewDueDate, AllocationDate, (.EndingBalance - .EnergyWithhold), DefaultInterestRATE)
                    End If
                End If
                MFEndingBalance = .EndingBalance
            End With
        End If

        Dim MFVATEndingBalance As Decimal = 0D
        If Not WESMBillSummaryMFV Is Nothing Then
            With WESMBillSummaryMFV
                If .NewDueDate = AllocationDate Then
                    DIonMFV = 0D
                Else
                    DIonMFV = 0D '_BusinessFactory.ComputeDefaultInterest(.DueDate, .NewDueDate, AllocationDate, (.EndingBalance - .EnergyWithhold), DefaultInterestRATE)
                End If
                MFVATEndingBalance = .EndingBalance
            End With
        End If

        Dim MFOffsetAmount As Decimal = MFEndingBalance
        Dim MFDefaultInterestAmount As Decimal = DIonMF
        Dim MFWHTaxAmount As Decimal = 0
        Dim MFWHTaxDefaultAmount As Decimal = 0
        Dim MFOffsetAmountTotal As Decimal = 0

        Dim MFVOffsetAmount As Decimal = MFVATEndingBalance
        Dim MFVDefaultInterestAmount As Decimal = DIonMFV
        Dim MFVWHVATAmount As Decimal = 0
        Dim MFVWHVATDefaultAmount As Decimal = 0
        Dim MFVOffsetAmountTotal As Decimal = 0

        Dim EnergyShareTotal As Decimal = 0


        Dim _OffsettingShareDetailsMF As New PaymentShareDetails
        Dim _OffsettingShareDetailsMFV As New PaymentShareDetails

        Dim ARCollectionOnMF As New ARCollection
        Dim ARCollectionOnMFDefault As New ARCollection
        Dim ARCollectionOnMFWHTax As New ARCollection
        Dim ARCollectionONMFWHTaxDefault As New ARCollection

        Dim ARCollectionOnMFV As New ARCollection
        Dim ARCollectionOnMFVDefault As New ARCollection
        Dim ARCollectionOnMFVWHVAT As New ARCollection
        Dim ARCollectionONMFVWHVATDefault As New ARCollection

        Dim TotalShareOfParticipant = (From x In ParticipantsShare Select x.AmountBalance).Sum()

        MFWHTaxAmount = Math.Round(Math.Abs(MFOffsetAmount) * ParticipantsInfo.MarketFeesWHTax, 2)
        MFWHTaxDefaultAmount = Math.Round(Math.Abs(MFDefaultInterestAmount) * ParticipantsInfo.MarketFeesWHTax, 2)
        MFVWHVATAmount = Math.Round(Math.Abs(MFOffsetAmount) * ParticipantsInfo.MarketFeesWHVAT, 2)
        MFVWHVATDefaultAmount = Math.Round(Math.Abs(MFDefaultInterestAmount) * ParticipantsInfo.MarketFeesWHVAT, 2)

        MFOffsetAmountTotal = (MFOffsetAmount + MFDefaultInterestAmount) + (MFWHTaxAmount + MFWHTaxDefaultAmount) + (MFVWHVATAmount + MFVWHVATDefaultAmount)
        MFVOffsetAmountTotal = (MFVOffsetAmount + MFVDefaultInterestAmount)

        Dim EnergyShareRatioMF As Decimal = MFOffsetAmountTotal / (MFOffsetAmountTotal + MFVOffsetAmountTotal)
        Dim EnergyShareRatioMFV As Decimal = MFVOffsetAmountTotal / (MFOffsetAmountTotal + MFVOffsetAmountTotal)

        If TotalShareOfParticipant >= Math.Abs(MFOffsetAmountTotal + MFVOffsetAmountTotal) Then
            Dim EnergyShareDeductedAmount As Decimal = 0
            Dim EnergyShareRemainingAmount As Decimal = 0
            Dim EnergyShareAmount As Decimal = 0
            For i As Integer = 0 To ParticipantsShare.Count - 1
                If ParticipantsShare(i).AmountBalance >= Math.Abs(MFOffsetAmountTotal + MFVOffsetAmountTotal) Then

                    'Default Interest on MF
                    If MFDefaultInterestAmount <> 0 Then
                        ARCollectionOnMFDefault = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFDefaultInterestAmount, EnumCollectionType.DefaultInterestOnMF)
                        MarketFeesAR.Add(ARCollectionOnMFDefault)
                    End If

                    'Default Interest on MFV
                    If MFVDefaultInterestAmount <> 0 Then
                        ARCollectionOnMFVDefault = Me.CreateOffsettingARCollection(WESMBillSummaryMFV, AllocationDate, MFVDefaultInterestAmount, EnumCollectionType.DefaultInterestOnVatOnMF)
                        MarketFeesAR.Add(ARCollectionOnMFVDefault)
                    End If

                    'WithHolding Tax Amount
                    If MFWHTaxAmount <> 0 Then
                        ARCollectionOnMFWHTax = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFWHTaxAmount, EnumCollectionType.WithholdingTaxOnMF)
                        MarketFeesAR.Add(ARCollectionOnMFWHTax)
                    End If

                    'WithHolding Tax Default Interest Amount
                    If MFWHTaxDefaultAmount <> 0 Then
                        ARCollectionONMFWHTaxDefault = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFWHTaxDefaultAmount, EnumCollectionType.WithholdingTaxOnDefaultInterest)
                        MarketFeesAR.Add(ARCollectionONMFWHTaxDefault)
                    End If

                    'WithHolding VAT Amount
                    If MFVWHVATAmount <> 0 Then
                        ARCollectionOnMFVWHVAT = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFVWHVATAmount, EnumCollectionType.WithholdingVatOnMF)
                        MarketFeesAR.Add(ARCollectionOnMFVWHVAT)
                    End If

                    'WithHolding VAT Default Interest Amount
                    If MFVWHVATDefaultAmount <> 0 Then
                        ARCollectionONMFVWHVATDefault = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFVWHVATDefaultAmount, EnumCollectionType.WithholdingVatOnDefaultInterest)
                        MarketFeesAR.Add(ARCollectionONMFVWHVATDefault)
                    End If

                    If Not WESMBillSummaryMF Is Nothing Then
                        'Pay the Outstanding Balance on MF
                        ARCollectionOnMF = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFOffsetAmount, EnumCollectionType.MarketFees)
                        MarketFeesAR.Add(ARCollectionOnMF)

                        'Create offsetting Share Details 
                        _OffsettingShareDetailsMF = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryMF, MFOffsetAmountTotal)

                        'Add the created offsettingsharedetails to OffsettingShare properties
                        ParticipantsShare(i).AmountOffset.Add(_OffsettingShareDetailsMF)
                    End If

                    If Not WESMBillSummaryMFV Is Nothing Then
                        'Pay the Outstanding Balance on MFV
                        ARCollectionOnMFV = Me.CreateOffsettingARCollection(WESMBillSummaryMFV, AllocationDate, MFVOffsetAmount, EnumCollectionType.VatOnMarketFees)
                        MarketFeesAR.Add(ARCollectionOnMFV)

                        'Create offsetting Share Details 
                        _OffsettingShareDetailsMFV = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryMFV, MFVOffsetAmountTotal)

                        'Add the created offsettingsharedetails to OffsettingShare properties
                        ParticipantsShare(i).AmountOffset.Add(_OffsettingShareDetailsMFV)
                    End If
                    Exit For

                Else

                    EnergyShareAmount = ParticipantsShare(i).AmountBalance
                    EnergyShareTotal += EnergyShareAmount

                    If EnergyShareTotal >= Math.Abs(MFOffsetAmountTotal + MFVOffsetAmountTotal) Then
                        EnergyShareRemainingAmount = (EnergyShareTotal + (MFOffsetAmountTotal + MFVOffsetAmountTotal))
                        EnergyShareDeductedAmount = EnergyShareAmount - EnergyShareRemainingAmount

                        'Default Interest on MF
                        If MFDefaultInterestAmount <> 0 Then
                            ARCollectionOnMFDefault = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFDefaultInterestAmount, EnumCollectionType.DefaultInterestOnMF)
                            MarketFeesAR.Add(ARCollectionOnMFDefault)
                        End If

                        'Default Interest on MFV
                        If MFVDefaultInterestAmount <> 0 Then
                            ARCollectionOnMFVDefault = Me.CreateOffsettingARCollection(WESMBillSummaryMFV, AllocationDate, MFVDefaultInterestAmount, EnumCollectionType.DefaultInterestOnVatOnMF)
                            MarketFeesAR.Add(ARCollectionOnMFVDefault)
                        End If

                        'WithHolding Tax Amount
                        If MFWHTaxAmount <> 0 Then
                            ARCollectionOnMFWHTax = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFWHTaxAmount, EnumCollectionType.WithholdingTaxOnMF)
                            MarketFeesAR.Add(ARCollectionOnMFWHTax)
                        End If

                        'WithHolding Tax Default Interest Amount
                        If MFWHTaxDefaultAmount <> 0 Then
                            ARCollectionONMFWHTaxDefault = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFWHTaxDefaultAmount, EnumCollectionType.WithholdingTaxOnDefaultInterest)
                            MarketFeesAR.Add(ARCollectionONMFWHTaxDefault)
                        End If

                        'WithHolding VAT Amount
                        If MFVWHVATAmount <> 0 Then
                            ARCollectionOnMFVWHVAT = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFVWHVATAmount, EnumCollectionType.WithholdingVatOnMF)
                            MarketFeesAR.Add(ARCollectionOnMFVWHVAT)
                        End If

                        'WithHolding VAT Default Interest Amount
                        If MFVWHVATDefaultAmount <> 0 Then
                            ARCollectionONMFVWHVATDefault = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFVWHVATDefaultAmount, EnumCollectionType.WithholdingVatOnDefaultInterest)
                            MarketFeesAR.Add(ARCollectionONMFVWHVATDefault)
                        End If

                        If Not WESMBillSummaryMF Is Nothing Then
                            'Pay the Outstanding Balance on MF
                            ARCollectionOnMF = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFOffsetAmount, EnumCollectionType.MarketFees)
                            MarketFeesAR.Add(ARCollectionOnMF)

                            'Create offsetting Share Details 
                            _OffsettingShareDetailsMF = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryMF, Math.Round(EnergyShareDeductedAmount * EnergyShareRatioMF, 2))

                            'Add the created offsettingsharedetails to OffsettingShare properties
                            ParticipantsShare(i).AmountOffset.Add(_OffsettingShareDetailsMF)
                        End If

                        'Pay the Outstanding Balance on MFV
                        If Not WESMBillSummaryMFV Is Nothing Then
                            ARCollectionOnMFV = Me.CreateOffsettingARCollection(WESMBillSummaryMFV, AllocationDate, MFVOffsetAmount, EnumCollectionType.VatOnMarketFees)
                            MarketFeesAR.Add(ARCollectionOnMFV)

                            'Create offsetting Share Details 
                            _OffsettingShareDetailsMFV = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryMFV, Math.Round(EnergyShareDeductedAmount * EnergyShareRatioMFV, 2))

                            'Add the created offsettingsharedetails to OffsettingShare properties
                            ParticipantsShare(i).AmountOffset.Add(_OffsettingShareDetailsMFV)
                        End If
                        Exit For
                    Else
                        If Not WESMBillSummaryMF Is Nothing Then
                            'Create offsetting Share Details 
                            _OffsettingShareDetailsMF = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryMF, Math.Round(EnergyShareAmount * EnergyShareRatioMF, 2))

                            'Add the created offsettingsharedetails to OffsettingShare properties
                            ParticipantsShare(i).AmountOffset.Add(_OffsettingShareDetailsMF)
                        End If

                        If Not WESMBillSummaryMFV Is Nothing Then
                            'Create offsetting Share Details 
                            _OffsettingShareDetailsMFV = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryMFV, Math.Round(EnergyShareAmount * EnergyShareRatioMFV, 2))

                            'Add the created offsettingsharedetails to OffsettingShare properties
                            ParticipantsShare(i).AmountOffset.Add(_OffsettingShareDetailsMFV)
                        End If
                    End If
                End If
            Next

        Else

            Dim TotalMFDefaultAmount = (MFDefaultInterestAmount + MFVDefaultInterestAmount) + (MFWHTaxDefaultAmount + MFVWHVATDefaultAmount)
            Dim TotalShareAmount As Decimal = TotalShareOfParticipant + TotalMFDefaultAmount

            If TotalShareOfParticipant >= Math.Abs(TotalMFDefaultAmount) Then

                EnergyShareRatioMF = MFOffsetAmount / (MFOffsetAmount + MFVOffsetAmount)
                EnergyShareRatioMFV = MFVOffsetAmount / (MFOffsetAmount + MFVOffsetAmount)

                Dim ActualMFOffsetAmount As Decimal = 0
                Dim ActualMFVOffsetAmount As Decimal = 0

                If ParticipantsInfo.MarketFeesWHTax <> 0 And ParticipantsInfo.MarketFeesWHVAT <> 0 Then
                    If MFVOffsetAmount = 0 Then
                        EnergyShareTotal = Math.Round((TotalShareAmount / 0.93), 2, MidpointRounding.AwayFromZero)
                        ActualMFOffsetAmount = Math.Round(TotalShareAmount / 0.93, 2, MidpointRounding.AwayFromZero) * -1
                        ActualMFVOffsetAmount = 0
                    Else
                        EnergyShareTotal = Math.Round((TotalShareAmount / 1.05) + (TotalShareAmount / 1.05) * 0.12, 2, MidpointRounding.AwayFromZero)
                        ActualMFOffsetAmount = Math.Round(TotalShareAmount / 1.05, 2, MidpointRounding.AwayFromZero) * -1
                        ActualMFVOffsetAmount = Math.Round(ActualMFOffsetAmount * 0.12, 2, MidpointRounding.AwayFromZero)
                    End If                    
                ElseIf ParticipantsInfo.MarketFeesWHTax <> 0 And ParticipantsInfo.MarketFeesWHVAT = 0 Then
                    If MFVOffsetAmount = 0 Then
                        EnergyShareTotal = Math.Round((TotalShareAmount / 0.98), 2, MidpointRounding.AwayFromZero)
                        ActualMFOffsetAmount = Math.Round(TotalShareAmount / 0.98, 2, MidpointRounding.AwayFromZero) * -1
                        ActualMFVOffsetAmount = 0
                    Else
                        EnergyShareTotal = Math.Round((TotalShareAmount / 1.1) + (TotalShareAmount / 1.1) * 0.12, 2, MidpointRounding.AwayFromZero)
                        ActualMFOffsetAmount = Math.Round(TotalShareAmount / 1.1, 2, MidpointRounding.AwayFromZero) * -1
                        ActualMFVOffsetAmount = Math.Round(ActualMFOffsetAmount * 0.12, 2, MidpointRounding.AwayFromZero)
                    End If

                ElseIf ParticipantsInfo.MarketFeesWHTax = 0 And ParticipantsInfo.MarketFeesWHVAT <> 0 Then
                    If MFVOffsetAmount = 0 Then
                        EnergyShareTotal = Math.Round((TotalShareAmount / 0.95), 2, MidpointRounding.AwayFromZero)
                        ActualMFOffsetAmount = Math.Round(TotalShareAmount / 0.95, 2, MidpointRounding.AwayFromZero) * -1
                        ActualMFVOffsetAmount = 0
                    Else
                        EnergyShareTotal = Math.Round((TotalShareAmount / 1.07) + (TotalShareAmount / 1.07) * 0.12, 2, MidpointRounding.AwayFromZero)
                        ActualMFOffsetAmount = Math.Round(TotalShareAmount / 1.07, 2, MidpointRounding.AwayFromZero) * -1
                        ActualMFVOffsetAmount = Math.Round(ActualMFOffsetAmount * 0.12, 2, MidpointRounding.AwayFromZero)
                    End If

                Else
                    EnergyShareTotal = TotalShareOfParticipant
                    ActualMFOffsetAmount = TotalShareOfParticipant * -1
                End If

                MFOffsetAmount = Math.Round(EnergyShareTotal * EnergyShareRatioMF, 2, MidpointRounding.AwayFromZero) * -1
                MFVOffsetAmount = Math.Round(EnergyShareTotal * EnergyShareRatioMFV, 2, MidpointRounding.AwayFromZero) * -1
                
                'Correction of Value added by lance 11/06/2019
                If Math.Abs(MFOffsetAmount + MFVOffsetAmount) <> EnergyShareTotal Then
                    Dim GetMFadj As Decimal = MFOffsetAmount - ActualMFOffsetAmount
                    Dim GetMFVadj As Decimal = MFVOffsetAmount - ActualMFVOffsetAmount
                    If MFVOffsetAmount <> 0 Then
                        If GetMFVadj <> 0 Then
                            MFVOffsetAmount = ActualMFVOffsetAmount - GetMFVadj
                        End If
                        If GetMFadj <> 0 Then
                            MFOffsetAmount = MFOffsetAmount - GetMFadj
                        End If
                    Else                        
                        If GetMFadj <> 0 Then
                            MFOffsetAmount = MFOffsetAmount - GetMFadj
                        End If
                    End If
                End If
                
                MFWHTaxAmount = Math.Round(Math.Abs(MFOffsetAmount) * ParticipantsInfo.MarketFeesWHTax, 2)
                MFWHTaxDefaultAmount = Math.Round(Math.Abs(MFDefaultInterestAmount) * ParticipantsInfo.MarketFeesWHTax, 2)
                MFVWHVATAmount = Math.Round(Math.Abs(MFOffsetAmount) * ParticipantsInfo.MarketFeesWHVAT, 2)
                MFVWHVATDefaultAmount = Math.Round(Math.Abs(MFDefaultInterestAmount) * ParticipantsInfo.MarketFeesWHVAT, 2)

                MFOffsetAmountTotal = (MFOffsetAmount + MFDefaultInterestAmount) + (MFWHTaxAmount + MFWHTaxDefaultAmount) + (MFVWHVATAmount + MFVWHVATDefaultAmount)
                MFVOffsetAmountTotal = (MFVOffsetAmount + MFVDefaultInterestAmount)

                For i As Integer = 0 To ParticipantsShare.Count - 1
                    Dim EnergyShareAmount As Decimal = ParticipantsShare(i).AmountBalance
                    Dim EnergyShareAmountComputedMF As Decimal = Math.Round(EnergyShareAmount * EnergyShareRatioMF, 2) * -1
                    Dim EnergyShareAmountComputedMFV As Decimal = Math.Round(EnergyShareAmount * EnergyShareRatioMFV, 2) * -1

                    Dim computeDifference As Decimal = EnergyShareAmount + (EnergyShareAmountComputedMF + EnergyShareAmountComputedMFV)
                    If computeDifference <> 0 Then
                        Dim adjEnergyShareAmountComputedMF As Decimal = EnergyShareAmountComputedMF - computeDifference
                        Dim adjEnergyShareAmountComputedMFV As Decimal = EnergyShareAmountComputedMFV

                        If Not WESMBillSummaryMF Is Nothing Then
                            'Create offsetting Share Details 
                            _OffsettingShareDetailsMF = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryMF, adjEnergyShareAmountComputedMF)

                            'Add the created offsettingsharedetails to OffsettingShare properties
                            ParticipantsShare(i).AmountOffset.Add(_OffsettingShareDetailsMF)
                        End If

                        If Not WESMBillSummaryMFV Is Nothing Then
                            'Create offsetting Share Details 
                            _OffsettingShareDetailsMFV = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryMFV, adjEnergyShareAmountComputedMFV)

                            'Add the created offsettingsharedetails to OffsettingShare properties
                            ParticipantsShare(i).AmountOffset.Add(_OffsettingShareDetailsMFV)
                        End If
                    Else
                        If Not WESMBillSummaryMF Is Nothing Then
                            'Create offsetting Share Details 
                            _OffsettingShareDetailsMF = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryMF, EnergyShareAmountComputedMF)

                            'Add the created offsettingsharedetails to OffsettingShare properties
                            ParticipantsShare(i).AmountOffset.Add(_OffsettingShareDetailsMF)
                        End If

                        If Not WESMBillSummaryMFV Is Nothing Then
                            'Create offsetting Share Details 
                            _OffsettingShareDetailsMFV = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryMFV, EnergyShareAmountComputedMFV)

                            'Add the created offsettingsharedetails to OffsettingShare properties
                            ParticipantsShare(i).AmountOffset.Add(_OffsettingShareDetailsMFV)
                        End If
                    End If
                Next
                'Default Interest on MF
                If MFDefaultInterestAmount <> 0 Then
                    ARCollectionOnMFDefault = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFDefaultInterestAmount, EnumCollectionType.DefaultInterestOnMF)
                    MarketFeesAR.Add(ARCollectionOnMFDefault)
                End If

                'Default Interest on MFV
                If MFVDefaultInterestAmount <> 0 Then
                    ARCollectionOnMFVDefault = Me.CreateOffsettingARCollection(WESMBillSummaryMFV, AllocationDate, MFVDefaultInterestAmount, EnumCollectionType.DefaultInterestOnVatOnMF)
                    MarketFeesAR.Add(ARCollectionOnMFVDefault)
                End If

                'WithHolding Tax Amount
                If MFWHTaxAmount <> 0 Then
                    ARCollectionOnMFWHTax = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFWHTaxAmount, EnumCollectionType.WithholdingTaxOnMF)
                    MarketFeesAR.Add(ARCollectionOnMFWHTax)
                End If

                'WithHolding Tax Default Interest Amount
                If MFWHTaxDefaultAmount <> 0 Then
                    ARCollectionONMFWHTaxDefault = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFWHTaxDefaultAmount, EnumCollectionType.WithholdingTaxOnDefaultInterest)
                    MarketFeesAR.Add(ARCollectionONMFWHTaxDefault)
                End If

                'WithHolding VAT Amount
                If MFVWHVATAmount <> 0 Then
                    ARCollectionOnMFVWHVAT = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFVWHVATAmount, EnumCollectionType.WithholdingVatOnMF)
                    MarketFeesAR.Add(ARCollectionOnMFVWHVAT)
                End If

                'WithHolding VAT Default Interest Amount
                If MFVWHVATDefaultAmount <> 0 Then
                    ARCollectionONMFVWHVATDefault = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFVWHVATDefaultAmount, EnumCollectionType.WithholdingVatOnDefaultInterest)
                    MarketFeesAR.Add(ARCollectionONMFVWHVATDefault)
                End If

                If TotalShareOfParticipant < Math.Abs(MFOffsetAmountTotal + MFVOffsetAmountTotal) Then

                    Dim computediff As Decimal = (TotalShareOfParticipant * -1) - (MFOffsetAmountTotal + MFVOffsetAmountTotal)

                    MFOffsetAmount += computediff

                    'Pay the Outstanding Balance on MF            
                    If MFOffsetAmount <> 0 Then
                        ARCollectionOnMF = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFOffsetAmount, EnumCollectionType.MarketFees)
                        MarketFeesAR.Add(ARCollectionOnMF)
                    End If
                    'Pay the Outstanding Balance on MFV
                    If Not WESMBillSummaryMFV Is Nothing Then
                        ARCollectionOnMFV = Me.CreateOffsettingARCollection(WESMBillSummaryMFV, AllocationDate, MFVOffsetAmount, EnumCollectionType.VatOnMarketFees)
                        MarketFeesAR.Add(ARCollectionOnMFV)
                    End If
                Else
                    If MFOffsetAmount <> 0 Then
                        ARCollectionOnMF = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFOffsetAmount, EnumCollectionType.MarketFees)
                        MarketFeesAR.Add(ARCollectionOnMF)
                    End If
                    'Pay the Outstanding Balance on MFV
                    If Not WESMBillSummaryMFV Is Nothing Then
                        ARCollectionOnMFV = Me.CreateOffsettingARCollection(WESMBillSummaryMFV, AllocationDate, MFVOffsetAmount, EnumCollectionType.VatOnMarketFees)
                        MarketFeesAR.Add(ARCollectionOnMFV)
                    End If
                End If
            End If
        End If
        ret = CreateDMCMonMFMFV(MarketFeesAR, AllocationDate, DefaultInterestRATE)
        Return ret

    End Function

#End Region

#Region "Functions For Offsetting MF and MFVAT FIT"
    Private Function CalculateOffsettingARCollectionMFMFVFIT(ByRef WESMBillSummaryItem As List(Of WESMBillSummary),
                                                        ByVal AllocationDate As Date,
                                                        ByVal DefaultInterestRATE As Decimal,
                                                        ByRef ParticipantsShare As List(Of PaymentShare),
                                                        ByVal ParticipantsInfo As AMParticipants,
                                                        ByVal SumofARFIT As Decimal) As List(Of ARCollection)
        Dim MarketFeesAR As New List(Of ARCollection)
        Dim ret As New List(Of ARCollection)
        Dim DIonMF As Decimal = 0
        Dim DIonMFV As Decimal = 0
        Dim _BusinessFactory As New BusinessFactory
        _BusinessFactory = BusinessFactory.GetInstance()
        Dim WESMBillSummaryMF As WESMBillSummary
        Dim WESMBillSummaryMFV As WESMBillSummary

        WESMBillSummaryMF = (From x In WESMBillSummaryItem Where x.ChargeType = EnumChargeType.MF Select x).FirstOrDefault
        WESMBillSummaryMFV = (From x In WESMBillSummaryItem Where x.ChargeType = EnumChargeType.MFV Select x).FirstOrDefault

        With WESMBillSummaryMF
            If .NewDueDate = AllocationDate Then
                DIonMF = 0D
            Else
                DIonMF = _BusinessFactory.ComputeDefaultInterest(.DueDate, .NewDueDate, AllocationDate, (.EndingBalance - .EnergyWithhold), DefaultInterestRATE)
            End If

        End With

        Dim MFVATEndingBalance As Decimal = 0D
        If Not WESMBillSummaryMFV Is Nothing Then
            With WESMBillSummaryMFV
                If .NewDueDate = AllocationDate Then
                    DIonMFV = 0D
                Else
                    DIonMFV = 0D '_BusinessFactory.ComputeDefaultInterest(.DueDate, .NewDueDate, AllocationDate, (.EndingBalance - .EnergyWithhold), DefaultInterestRATE)
                End If
                MFVATEndingBalance = .EndingBalance
            End With
        End If

        Dim MFOffsetAmount As Decimal = WESMBillSummaryMF.EndingBalance
        Dim MFDefaultInterestAmount As Decimal = DIonMF
        Dim MFWHTaxAmount As Decimal = 0
        Dim MFWHTaxDefaultAmount As Decimal = 0
        Dim MFWHVATAmount As Decimal = 0
        Dim MFWHVATDefaultAmount As Decimal = 0
        Dim MFOffsetAmountTotal As Decimal = 0

        Dim MFVOffsetAmount As Decimal = MFVATEndingBalance
        Dim MFVDefaultInterestAmount As Decimal = DIonMFV
        Dim MFVOffsetAmountTotal As Decimal = 0

        Dim EnergyShareTotal As Decimal = 0


        Dim _OffsettingShareDetailsMF As New PaymentShareDetails
        Dim _OffsettingShareDetailsMFV As New PaymentShareDetails

        Dim ARCollectionOnMF As New ARCollection
        Dim ARCollectionOnMFDefault As New ARCollection
        Dim ARCollectionOnMFWHTax As New ARCollection
        Dim ARCollectionONMFWHTaxDefault As New ARCollection
        Dim ARCollectionOnMFVWHVAT As New ARCollection
        Dim ARCollectionONMFVWHVATDefault As New ARCollection

        Dim ARCollectionOnMFV As New ARCollection
        Dim ARCollectionOnMFVDefault As New ARCollection


        Dim TotalShareOfParticipant = (From x In ParticipantsShare Select x.AmountBalance).Sum()

        MFWHTaxAmount = Math.Round(Math.Abs(MFOffsetAmount) * ParticipantsInfo.MarketFeesWHTax, 2)
        MFWHTaxDefaultAmount = Math.Round(Math.Abs(MFDefaultInterestAmount) * ParticipantsInfo.MarketFeesWHTax, 2)
        MFWHVATAmount = Math.Round(Math.Abs(MFOffsetAmount) * ParticipantsInfo.MarketFeesWHVAT, 2)
        MFWHVATDefaultAmount = Math.Round(Math.Abs(MFDefaultInterestAmount) * ParticipantsInfo.MarketFeesWHVAT, 2)

        MFOffsetAmountTotal = (MFOffsetAmount + MFDefaultInterestAmount) + (MFWHTaxAmount + MFWHTaxDefaultAmount) + (MFWHVATAmount + MFWHVATDefaultAmount)
        MFVOffsetAmountTotal = (MFVOffsetAmount + MFVDefaultInterestAmount)

        Dim EnergyShareRatioMF As Decimal = MFOffsetAmountTotal / (MFOffsetAmountTotal + MFVOffsetAmountTotal)
        Dim EnergyShareRatioMFV As Decimal = MFVOffsetAmountTotal / (MFOffsetAmountTotal + MFVOffsetAmountTotal)
        Dim RatioDic As New Dictionary(Of String, Decimal)
        Dim ShareDetailsDic As New Dictionary(Of String, Integer)

        Dim TotalShareDeductedForMF As Decimal = 0
        Dim TotalShareDeductedForMFDI As Decimal = 0
        Dim TotalShareDeductedForMFWHTaxAmount As Decimal = 0
        Dim TotalShareDeductedForMFDIWHTaxAmount As Decimal = 0
        Dim TotalShareDeductedForMFWHVATAmount As Decimal = 0
        Dim TotalShareDeductedForMFDIWHVATAmount As Decimal = 0
        Dim TotalShareDeductedForMFV As Decimal = 0
        Dim TotalShareDeductedForMFVDI As Decimal = 0
        Dim TotalMFOffsetAmount As Decimal = 0
        Dim TotalMFVOffsetAmount As Decimal = 0

        Dim ListOfOffsettingShareDetails As New List(Of PaymentShareDetails)
        Dim ListOfIndex As New List(Of Integer)
        Dim GetListOfShareForRatio = (From x In ParticipantsShare _
                                    Group By IDNumber = x.IDNumber _
                                    Into TTARatio = Sum(x.AmountShare)).ToList()

        Dim GetListOfTotalShare = (From x In GetListOfShareForRatio Select x.TTARatio).Sum()

        For Each item In GetListOfShareForRatio
            If Not RatioDic.ContainsKey(item.IDNumber) Then
                Dim ComputedRatio As Decimal = item.TTARatio / GetListOfTotalShare
                RatioDic.Add(item.IDNumber, ComputedRatio)
            End If
        Next

        Dim DistinctIDShare As List(Of String) = (From x In ParticipantsShare Select x.IDNumber).Distinct.ToList 'distinct participant id that have 2 or more share 
        Dim intIndx As Integer = 0

        For Each id In DistinctIDShare
            Dim ParticipantRatio As Decimal = RatioDic(id)

            Dim oMFAmount As Decimal = Math.Round(MFOffsetAmount * ParticipantRatio, 2)
            Dim oMFDIAmount As Decimal = Math.Round(MFDefaultInterestAmount * ParticipantRatio, 2)
            Dim oMFWHTaxAmount As Decimal = Math.Round(MFWHTaxAmount * ParticipantRatio, 2)
            Dim oMFWHTaxDIAmount As Decimal = Math.Round(MFWHTaxDefaultAmount * ParticipantRatio, 2)
            Dim oMFWHVATAmount As Decimal = Math.Round(MFWHVATAmount * ParticipantRatio, 2)
            Dim oMFWHVATDIAmount As Decimal = Math.Round(MFWHVATDefaultAmount * ParticipantRatio, 2)
            Dim oMFVAmount As Decimal = Math.Round(MFVOffsetAmount * ParticipantRatio, 2)
            Dim oMFVDIAmount As Decimal = Math.Round(MFVDefaultInterestAmount * ParticipantRatio, 2)
            Dim oMFOffsetTotal As Decimal = (oMFAmount + oMFDIAmount) + (oMFWHTaxAmount + oMFWHTaxDIAmount) + (oMFWHVATAmount + oMFWHVATDIAmount)
            Dim oMFVOffsetTotal As Decimal = (oMFVAmount + oMFVDIAmount)

            TotalShareDeductedForMF += oMFAmount
            TotalShareDeductedForMFDI += oMFDIAmount
            TotalShareDeductedForMFWHTaxAmount += oMFWHTaxAmount
            TotalShareDeductedForMFDIWHTaxAmount += oMFWHTaxDIAmount
            TotalShareDeductedForMFWHVATAmount += oMFWHVATAmount
            TotalShareDeductedForMFDIWHVATAmount += oMFWHVATDIAmount
            TotalShareDeductedForMFV += oMFVAmount
            TotalShareDeductedForMFVDI += oMFVDIAmount
            TotalMFOffsetAmount += oMFOffsetTotal
            TotalMFVOffsetAmount += oMFVOffsetTotal

            Dim selectShare As List(Of PaymentShare) = (From x In ParticipantsShare Where x.IDNumber = id Select x).ToList
            Dim shareTotalPerID As Decimal = (From x In selectShare Select x.AmountShare).Sum
            Dim TotalDeductedShareForMF As Decimal = 0
            Dim TotalDeductedShareForMFV As Decimal = 0

            Dim ArrIndex As New List(Of Integer)
            'Check if have shares calculated
            If oMFOffsetTotal <> 0 Or oMFVOffsetTotal <> 0 Then
                For i As Integer = 0 To selectShare.Count - 1
                    Dim ShareItem As PaymentShare = selectShare(i)
                    Dim computeRatioPerShare As Decimal = selectShare(i).AmountShare / shareTotalPerID
                    Dim DeductedAmountperShareForMF As Decimal = Math.Round(computeRatioPerShare * oMFOffsetTotal, 2)
                    Dim DeductedAmountperShareForMFV As Decimal = Math.Round(computeRatioPerShare * oMFVOffsetTotal, 2)
                    TotalDeductedShareForMF += DeductedAmountperShareForMF
                    TotalDeductedShareForMFV += DeductedAmountperShareForMFV
                    If DeductedAmountperShareForMF <> 0 Then
                        Dim Key As String = selectShare(i).PaymentShareNo & "MF"
                        If Not ShareDetailsDic.ContainsKey(Key) Then
                            ShareDetailsDic.Add(Key, intIndx)
                        End If

                        Dim _OffsettingShareDetails As PaymentShareDetails = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryMF, DeductedAmountperShareForMF)
                        ListOfOffsettingShareDetails.Add(_OffsettingShareDetails)
                        ListOfIndex.Add(intIndx) 'add index of selected share per participant
                        ArrIndex.Add(intIndx)   'add index of selected share per participant. It will be used for adjustment.
                        intIndx += 1
                    End If

                    If DeductedAmountperShareForMFV <> 0 Then
                        Dim Key As String = selectShare(i).PaymentShareNo & "MFV"
                        If Not ShareDetailsDic.ContainsKey(Key) Then
                            ShareDetailsDic.Add(Key, intIndx)
                        End If

                        Dim _OffsettingShareDetails As PaymentShareDetails = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryMFV, DeductedAmountperShareForMFV)
                        ListOfOffsettingShareDetails.Add(_OffsettingShareDetails)
                        ListOfIndex.Add(intIndx) 'add index of selected share per participant
                        ArrIndex.Add(intIndx)   'add index of selected share per participant. It will be used for adjustment.
                        intIndx += 1
                    End If

                Next
            End If

            'Check if the actual amount that should be deducted is not equal to the amount that has been deducted.
            If (oMFOffsetTotal + oMFVOffsetTotal) <> (TotalDeductedShareForMF + TotalDeductedShareForMFV) Then
                'if entered here total share deducted from the participant should be adjusted.                                
                Dim TotalAdjustedAmount As Decimal = Math.Round(Math.Abs(oMFOffsetTotal + oMFVOffsetTotal) + (TotalDeductedShareForMF + TotalDeductedShareForMFV), 2)
                Dim GetListOfIndex As Integer = (From x In ListOfIndex Where x = (From y In ArrIndex Select y).First Select x).First
                ListOfOffsettingShareDetails(GetListOfIndex).OffsetAmount += TotalAdjustedAmount
            End If
        Next

        'Check if Total of deducted amount is equal to the amount that should be deducted on Energy Offsetting
        If ((MFOffsetAmountTotal + MFVOffsetAmountTotal) <> Math.Abs(TotalMFOffsetAmount + TotalMFVOffsetAmount)) Then
            Dim AdjustedTotalMFOffsetAmount As Decimal = Math.Round(((MFOffsetAmountTotal * -1) + TotalMFOffsetAmount), 2)
            Dim AdjustedTotalMFVOffsetAmount As Decimal = Math.Round(((MFVOffsetAmountTotal * -1) + TotalMFVOffsetAmount), 2)
            TotalShareDeductedForMF += AdjustedTotalMFOffsetAmount
            TotalShareDeductedForMFV += AdjustedTotalMFVOffsetAmount

            'If no paymentsharedetails created but it has adjustment, create paymentsharedetails and put it to the participant with higher share.
            If ListOfOffsettingShareDetails.Count = 0 And (TotalShareDeductedForMF + TotalShareDeductedForMFV) <> 0 Then
                Dim GetParticipantShare = (From x In ParticipantsShare Select x).FirstOrDefault
                If TotalShareDeductedForMF <> 0 Then
                    _OffsettingShareDetailsMF = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryMF, TotalShareDeductedForMF)
                    GetParticipantShare.AmountOffset.Add(_OffsettingShareDetailsMF)
                End If

                If TotalShareDeductedForMFV <> 0 Then
                    _OffsettingShareDetailsMFV = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryMFV, TotalMFVOffsetAmount)
                    GetParticipantShare.AmountOffset.Add(_OffsettingShareDetailsMFV)
                End If
            End If

            Dim GetHigherOffsettingSHareDetails As PaymentShareDetails = (From x In ListOfOffsettingShareDetails Select x Order By x.OffsetAmount Descending).FirstOrDefault
            Dim SumofShares As Decimal = (From x In ListOfOffsettingShareDetails Select x.OffsetAmount).Sum

            GetHigherOffsettingSHareDetails.OffsetAmount += (AdjustedTotalMFOffsetAmount + AdjustedTotalMFVOffsetAmount)

            'loop and put in participant with positive share
            For Each Key In ParticipantsShare
                If ShareDetailsDic.ContainsKey(Key.PaymentShareNo & "MF") Then
                    Dim GetPSDetails As PaymentShareDetails = ListOfOffsettingShareDetails(ShareDetailsDic.Item(Key.PaymentShareNo & "MF"))
                    Key.AmountOffset.Add(GetPSDetails)
                ElseIf ShareDetailsDic.ContainsKey(Key.PaymentShareNo & "MFV") Then
                    Dim GetPSDetails As PaymentShareDetails = ListOfOffsettingShareDetails(ShareDetailsDic.Item(Key.PaymentShareNo & "MFV"))
                    Key.AmountOffset.Add(GetPSDetails)
                End If
            Next
        Else
            'loop and put in participant with positive share
            For Each Key In ParticipantsShare
                If ShareDetailsDic.ContainsKey(Key.PaymentShareNo & "MF") Then
                    Dim GetPSDetails As PaymentShareDetails = ListOfOffsettingShareDetails(ShareDetailsDic.Item(Key.PaymentShareNo & "MF"))
                    Key.AmountOffset.Add(GetPSDetails)
                ElseIf ShareDetailsDic.ContainsKey(Key.PaymentShareNo & "MFV") Then
                    Dim GetPSDetails As PaymentShareDetails = ListOfOffsettingShareDetails(ShareDetailsDic.Item(Key.PaymentShareNo & "MFV"))
                    Key.AmountOffset.Add(GetPSDetails)
                End If
            Next
        End If

        'Pay the Outstanding Balance on MF
        ARCollectionOnMF = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFOffsetAmount, EnumCollectionType.MarketFees)
        MarketFeesAR.Add(ARCollectionOnMF)


        'Pay the Outstanding Balance on MFV
        If Not WESMBillSummaryMFV Is Nothing Then
            ARCollectionOnMFV = Me.CreateOffsettingARCollection(WESMBillSummaryMFV, AllocationDate, MFVOffsetAmount, EnumCollectionType.VatOnMarketFees)
            MarketFeesAR.Add(ARCollectionOnMFV)
        End If


        'Default Interest on MF
        If MFDefaultInterestAmount <> 0 Then
            ARCollectionOnMFDefault = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFDefaultInterestAmount, EnumCollectionType.DefaultInterestOnMF)
            MarketFeesAR.Add(ARCollectionOnMFDefault)
        End If

        'Default Interest on MFV
        If MFVDefaultInterestAmount <> 0 Then
            ARCollectionOnMFVDefault = Me.CreateOffsettingARCollection(WESMBillSummaryMFV, AllocationDate, MFVDefaultInterestAmount, EnumCollectionType.DefaultInterestOnVatOnMF)
            MarketFeesAR.Add(ARCollectionOnMFVDefault)
        End If

        'WithHolding Tax Amount
        If MFWHTaxAmount <> 0 Then
            ARCollectionOnMFWHTax = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFWHTaxAmount, EnumCollectionType.WithholdingTaxOnMF)
            MarketFeesAR.Add(ARCollectionOnMFWHTax)
        End If

        'WithHolding Tax Default Interest Amount
        If MFWHTaxDefaultAmount <> 0 Then
            ARCollectionONMFWHTaxDefault = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFWHTaxDefaultAmount, EnumCollectionType.WithholdingTaxOnDefaultInterest)
            MarketFeesAR.Add(ARCollectionONMFWHTaxDefault)
        End If

        'WithHolding VAT Amount
        If MFWHVATAmount <> 0 Then
            ARCollectionOnMFVWHVAT = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFWHVATAmount, EnumCollectionType.WithholdingVatOnMF)
            MarketFeesAR.Add(ARCollectionOnMFVWHVAT)
        End If

        'WithHolding VAT Default Interest Amount
        If MFWHVATDefaultAmount <> 0 Then
            ARCollectionONMFVWHVATDefault = Me.CreateOffsettingARCollection(WESMBillSummaryMF, AllocationDate, MFWHVATDefaultAmount, EnumCollectionType.WithholdingVatOnDefaultInterest)
            MarketFeesAR.Add(ARCollectionONMFVWHVATDefault)
        End If

        ret = CreateDMCMonMFMFV(MarketFeesAR, AllocationDate, DefaultInterestRATE)

        Return ret
    End Function
#End Region

#Region "Functions For Offsetting Energy and VAT"
    Private Function CalculateOffsettingARCollection(ByRef WESMBillSummaryItem As WESMBillSummary,
                                                      ByVal AllocationDate As Date,
                                                      ByVal DefaultInterestRATE As Decimal,
                                                      ByRef ParticipantsShare As List(Of PaymentShare),
                                                      ByVal ParticipantsInfo As AMParticipants) As List(Of ARCollection)
        Dim ret As New List(Of ARCollection)
        Dim DefaultInterestAmount As Decimal = 0
        Dim _BusinessFactory As New BusinessFactory
        _BusinessFactory = BusinessFactory.GetInstance()

        With WESMBillSummaryItem
            If .NewDueDate = AllocationDate Then
                DefaultInterestAmount = 0D
            Else
                If .SummaryType = EnumSummaryType.DMCM Or .NoDefInt = True Then
                    DefaultInterestAmount = 0D
                Else
                    DefaultInterestAmount = _BusinessFactory.ComputeDefaultInterest(.DueDate, .NewDueDate, AllocationDate, (.EndingBalance - .EnergyWithhold), DefaultInterestRATE)
                End If

            End If
        End With

        Select Case WESMBillSummaryItem.ChargeType
            Case EnumChargeType.E

                Dim EnergyAR As New List(Of ARCollection)
                Dim EOffsetAmount As Decimal = WESMBillSummaryItem.EndingBalance - If(WESMBillSummaryItem.EnergyWithholdStatus = EnumEnergyWithholdStatus.NotApplicable, WESMBillSummaryItem.EnergyWithhold, 0)
                Dim EDefaultInterestAmount As Decimal = DefaultInterestAmount
                Dim EOffsetAmountTotal As Decimal = 0D
                Dim EnergyShareTotal As Decimal = 0
                Dim _OffsettingShareDetails As New PaymentShareDetails
                Dim ARCollectionOnEnergy As New ARCollection
                Dim ARCollectionOnEnergyDefault As New ARCollection
                Dim ARCollectionOnEnergyWithhold As New ARCollection

                EOffsetAmountTotal = EOffsetAmount + EDefaultInterestAmount

                Dim TotalShareOfParticipant = (From x In ParticipantsShare Select x.AmountBalance).Sum()
                Dim WESMInvoice As String = WESMBillSummaryItem.INVDMCMNo
                If TotalShareOfParticipant >= Math.Abs(EOffsetAmountTotal) Then
                    Dim EnergyShareDeductedAmount As Decimal = 0
                    Dim EnergyShareRemainingAmount As Decimal = 0
                    Dim EnergyShareAmount As Decimal = 0
                    For i As Integer = 0 To ParticipantsShare.Count - 1
                        If ParticipantsShare(i).AmountBalance >= Math.Abs(EOffsetAmountTotal) Then
                            'Default Interest on Energy
                            If EDefaultInterestAmount <> 0 Then
                                ARCollectionOnEnergyDefault = Me.CreateOffsettingARCollection(WESMBillSummaryItem, AllocationDate, EDefaultInterestAmount, EnumCollectionType.DefaultInterestOnEnergy)
                                EnergyAR.Add(ARCollectionOnEnergyDefault)
                            End If

                            'Pay the Outstanding Balance on Energy
                            ARCollectionOnEnergy = Me.CreateOffsettingARCollection(WESMBillSummaryItem, AllocationDate, EOffsetAmount, EnumCollectionType.Energy)
                            EnergyAR.Add(ARCollectionOnEnergy)

                            'Create offsetting Share Details 
                            _OffsettingShareDetails = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryItem, EOffsetAmountTotal)

                            'Add the created offsettingsharedetails to OffsettingShare properties
                            ParticipantsShare(i).AmountOffset.Add(_OffsettingShareDetails)
                            Exit For
                        Else
                            EnergyShareAmount = ParticipantsShare(i).AmountBalance
                            EnergyShareTotal += EnergyShareAmount

                            If EnergyShareTotal >= Math.Abs(EOffsetAmountTotal) Then
                                EnergyShareRemainingAmount = (EnergyShareTotal + EOffsetAmountTotal)
                                EnergyShareDeductedAmount = EnergyShareAmount - EnergyShareRemainingAmount

                                'Default Interest on Energy
                                If EDefaultInterestAmount <> 0 Then
                                    ARCollectionOnEnergyDefault = Me.CreateOffsettingARCollection(WESMBillSummaryItem, AllocationDate, EDefaultInterestAmount, EnumCollectionType.DefaultInterestOnEnergy)
                                    EnergyAR.Add(ARCollectionOnEnergyDefault)
                                End If

                                'Pay the Outstanding Balance on Energy
                                ARCollectionOnEnergy = Me.CreateOffsettingARCollection(WESMBillSummaryItem, AllocationDate, EOffsetAmount, EnumCollectionType.Energy)
                                EnergyAR.Add(ARCollectionOnEnergy)

                                'Create offsetting Share Details 
                                _OffsettingShareDetails = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryItem, EnergyShareDeductedAmount)

                                'Add the created offsettingsharedetails to OffsettingShare properties
                                ParticipantsShare(i).AmountOffset.Add(_OffsettingShareDetails)
                                Exit For
                            Else
                                'Create offsetting Share Details 
                                _OffsettingShareDetails = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryItem, EnergyShareAmount)

                                'Add the created offsettingsharedetails to OffsettingShare properties
                                ParticipantsShare(i).AmountOffset.Add(_OffsettingShareDetails)
                            End If
                        End If
                    Next

                ElseIf TotalShareOfParticipant >= Math.Abs(EDefaultInterestAmount) Then 'if TotalShareofParticipant is greater than in default interest but less than in Total Amount to be offsetted                    
                    Dim EnergyShareAmount As Decimal = 0

                    For i As Integer = 0 To ParticipantsShare.Count - 1
                        EnergyShareAmount = ParticipantsShare(i).AmountBalance
                        EnergyShareTotal += EnergyShareAmount

                        'Create offsetting Share Details 
                        _OffsettingShareDetails = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryItem, EnergyShareAmount)

                        'Add the created offsettingsharedetails to OffsettingShare properties
                        ParticipantsShare(i).AmountOffset.Add(_OffsettingShareDetails)

                    Next

                    If EDefaultInterestAmount <> 0 And EnergyShareTotal >= Math.Abs(EDefaultInterestAmount) Then
                        ARCollectionOnEnergyDefault = Me.CreateOffsettingARCollection(WESMBillSummaryItem, AllocationDate, EDefaultInterestAmount, EnumCollectionType.DefaultInterestOnEnergy)
                        EnergyAR.Add(ARCollectionOnEnergyDefault)
                        EnergyShareTotal += EDefaultInterestAmount
                    End If

                    If EnergyShareTotal > 0 Then
                        ARCollectionOnEnergy = Me.CreateOffsettingARCollection(WESMBillSummaryItem, AllocationDate, EnergyShareTotal * -1, EnumCollectionType.Energy)
                        EnergyAR.Add(ARCollectionOnEnergy)
                    End If

                End If
                ret = CreateDMCMonEEV(EnergyAR, AllocationDate, DefaultInterestRATE)
            Case EnumChargeType.EV
                Dim EnergyVATAR As New List(Of ARCollection)
                Dim EVOffsetAmount As Decimal = WESMBillSummaryItem.EndingBalance
                Dim VATShareTotal As Decimal = 0

                Dim _OffsettingShareDetails As New PaymentShareDetails
                Dim ARCollectionOnVAT As New ARCollection

                Dim TotalShareOfParticipant = (From x In ParticipantsShare Select x.AmountBalance).Sum()

                If TotalShareOfParticipant >= Math.Abs(EVOffsetAmount) Then
                    Dim VATShareDeductedAmount As Decimal = 0
                    Dim VATShareRemainingAmount As Decimal = 0
                    Dim VATShareAmount As Decimal = 0
                    For i As Integer = 0 To ParticipantsShare.Count - 1
                        If ParticipantsShare(i).AmountBalance >= Math.Abs(EVOffsetAmount) Then
                            'Pay the Outstanding Balance on VAT
                            ARCollectionOnVAT = Me.CreateOffsettingARCollection(WESMBillSummaryItem, AllocationDate, EVOffsetAmount, EnumCollectionType.VatOnEnergy)
                            EnergyVATAR.Add(ARCollectionOnVAT)

                            'Create offsetting Share Details 
                            _OffsettingShareDetails = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryItem, EVOffsetAmount)

                            'Add the created offsettingsharedetails to OffsettingShare properties
                            ParticipantsShare(i).AmountOffset.Add(_OffsettingShareDetails)
                            Exit For
                        Else
                            VATShareAmount = ParticipantsShare(i).AmountBalance
                            VATShareTotal += VATShareAmount

                            If VATShareTotal >= Math.Abs(EVOffsetAmount) Then
                                VATShareRemainingAmount = (VATShareTotal + EVOffsetAmount)
                                VATShareDeductedAmount = VATShareAmount - VATShareRemainingAmount

                                'Pay the Outstanding Balance on Energy
                                ARCollectionOnVAT = Me.CreateOffsettingARCollection(WESMBillSummaryItem, AllocationDate, EVOffsetAmount, EnumCollectionType.VatOnEnergy)
                                EnergyVATAR.Add(ARCollectionOnVAT)

                                'Create offsetting Share Details 
                                _OffsettingShareDetails = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryItem, VATShareDeductedAmount)

                                'Add the created offsettingsharedetails to OffsettingShare properties
                                ParticipantsShare(i).AmountOffset.Add(_OffsettingShareDetails)
                                Exit For
                            Else

                                'Create offsetting Share Details 
                                _OffsettingShareDetails = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryItem, VATShareAmount)

                                'Add the created offsettingsharedetails to OffsettingShare properties
                                ParticipantsShare(i).AmountOffset.Add(_OffsettingShareDetails)
                            End If
                        End If
                    Next
                Else
                    Dim VATShareAmount As Decimal = 0

                    For i As Integer = 0 To ParticipantsShare.Count - 1
                        VATShareAmount = ParticipantsShare(i).AmountBalance
                        VATShareTotal += VATShareAmount

                        _OffsettingShareDetails = Me.CreateOffsettingShareDetailsItem(WESMBillSummaryItem, VATShareAmount)
                        ParticipantsShare(i).AmountOffset.Add(_OffsettingShareDetails)
                    Next

                    ARCollectionOnVAT = Me.CreateOffsettingARCollection(WESMBillSummaryItem, AllocationDate, VATShareTotal * -1, EnumCollectionType.VatOnEnergy)
                    EnergyVATAR.Add(ARCollectionOnVAT)

                End If
                ret = CreateDMCMonEEV(EnergyVATAR, AllocationDate, DefaultInterestRATE)
        End Select

        Return ret
    End Function

    Private Function CreateDMCMonEEV(ByVal EEVARList As List(Of ARCollection),
                                     ByVal AllocationDate As Date,
                                     ByVal DefaultInterestRate As Decimal) As List(Of ARCollection)
        Dim ret As New List(Of ARCollection)
        Dim SetUpDMCMList = _PaymentProformaEntries.CreateDMCM_AR(EEVARList, EnumCollectionCategory.Offset, DefaultInterestRate, AMParticipants)
        For Each y In SetUpDMCMList

            Me._ListofDMCM.Add(y)
            For Each Item In EEVARList
                Select Case Item.CollectionType
                    Case EnumCollectionType.Energy
                        If y.TransType = EnumDMCMTransactionType.PaymentSetupClearingAROnEnergyAndDefaultInterest Then
                            'Item.ListofDMCM.Add(y.DMCMNumber.ToString)
                            Item.GeneratedDMCM = y.DMCMNumber.ToString
                            ret.Add(Item)
                        End If
                    Case EnumCollectionType.DefaultInterestOnEnergy
                        If y.TransType = EnumDMCMTransactionType.PaymentSetupOffsettingAROfDefaultInterestOnEnergy Then
                            'Item.ListofDMCM.Add(y.DMCMNumber.ToString)
                            Item.GeneratedDMCM = y.DMCMNumber.ToString
                            ret.Add(Item)
                        End If
                    Case EnumCollectionType.VatOnEnergy
                        If y.TransType = EnumDMCMTransactionType.PaymentSetupClearingAROnVATonEnergy Then
                            'Item.ListofDMCM.Add(y.DMCMNumber.ToString)
                            Item.GeneratedDMCM = y.DMCMNumber.ToString
                            ret.Add(Item)
                        End If
                End Select
            Next
        Next

        Return ret
    End Function

    Private Function CreateDMCMonMFMFV(ByVal MFMFVARList As List(Of ARCollection),
                                        ByVal AllocationDate As Date,
                                        ByVal DefaultInterestRate As Decimal) As List(Of ARCollection)
        Dim ret As New List(Of ARCollection)

        Dim MFBaseAmount = (From x In MFMFVARList _
                            Select x).ToList()

        Dim MFBaseDIAmount = (From x In MFMFVARList _
                            Where x.CollectionType = EnumCollectionType.DefaultInterestOnMF _
                            Or x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF _
                            Or x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest _
                            Or x.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest _
                            Select x).ToList

        Dim MFEWTandWVAT = (From x In MFMFVARList _
                             Where x.CollectionType = EnumCollectionType.WithholdingTaxOnMF _
                             Or x.CollectionType = EnumCollectionType.WithholdingVatOnMF _
                             Select x).ToList

        Dim MFEWTandWVATDI = (From x In MFMFVARList _
                              Where x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest _
                              Or x.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest _
                              Select x).ToList

        Dim MFBaseAmountDMCM As New DebitCreditMemo
        If MFBaseAmount.Count > 0 Then
            MFBaseAmountDMCM = _PaymentProformaEntries.CreateDMCM_ARMFMFV(MFBaseAmount, AllocationDate, DefaultInterestRate, AMParticipants, EnumDMCMCode.MFMFV)
            Me._ListofDMCM.Add(MFBaseAmountDMCM)
            For Each Item In MFBaseAmount
                Select Case Item.CollectionType
                    Case EnumCollectionType.MarketFees
                        'Item.ListofDMCM.Add(MFBaseAmountDMCM.DMCMNumber.ToString)
                        Item.GeneratedDMCM = MFBaseAmountDMCM.DMCMNumber.ToString
                        ret.Add(Item)
                    Case EnumCollectionType.VatOnMarketFees
                        'Item.ListofDMCM.Add(MFBaseAmountDMCM.DMCMNumber.ToString)
                        Item.GeneratedDMCM = MFBaseAmountDMCM.DMCMNumber.ToString
                        ret.Add(Item)
                End Select
            Next
        End If

        Dim MFBaseDIAmountDMCM As New DebitCreditMemo
        If MFBaseDIAmount.Count > 0 Then
            MFBaseDIAmountDMCM = _PaymentProformaEntries.CreateDMCM_ARMFMFV(MFBaseDIAmount, AllocationDate, DefaultInterestRate, AMParticipants, EnumDMCMCode.MFMFVDI)
            Me._ListofDMCM.Add(MFBaseDIAmountDMCM)
            For Each Item In MFBaseDIAmount
                Select Case Item.CollectionType
                    Case EnumCollectionType.DefaultInterestOnMF
                        'Item.ListofDMCM.Add(MFBaseDIAmountDMCM.DMCMNumber.ToString)
                        Item.GeneratedDMCM = MFBaseDIAmountDMCM.DMCMNumber.ToString
                        ret.Add(Item)
                    Case EnumCollectionType.DefaultInterestOnVatOnMF
                        'Item.ListofDMCM.Add(MFBaseDIAmountDMCM.DMCMNumber.ToString)
                        Item.GeneratedDMCM = MFBaseDIAmountDMCM.DMCMNumber.ToString
                        ret.Add(Item)
                End Select
            Next
        End If

        Dim MFEWTandWVATDMCM As New DebitCreditMemo
        If MFEWTandWVAT.Count > 0 Then
            MFEWTandWVATDMCM = _PaymentProformaEntries.CreateDMCM_ARMFMFV(MFEWTandWVAT, AllocationDate, DefaultInterestRate, AMParticipants, EnumDMCMCode.MFEWTWVAT)
            Me._ListofDMCM.Add(MFEWTandWVATDMCM)
            For Each Item In MFEWTandWVAT
                Select Case Item.CollectionType
                    Case EnumCollectionType.WithholdingTaxOnMF
                        'Item.ListofDMCM.Add(MFEWTandWVATDMCM.DMCMNumber.ToString)
                        Item.GeneratedDMCM = MFEWTandWVATDMCM.DMCMNumber.ToString
                        ret.Add(Item)
                    Case EnumCollectionType.WithholdingVatOnMF
                        'Item.ListofDMCM.Add(MFEWTandWVATDMCM.DMCMNumber.ToString)
                        Item.GeneratedDMCM = MFEWTandWVATDMCM.DMCMNumber.ToString
                        ret.Add(Item)
                End Select
            Next
        End If

        Dim MFEWTandWVATDIDMCM As New DebitCreditMemo
        If MFEWTandWVATDI.Count > 0 Then
            MFEWTandWVATDIDMCM = _PaymentProformaEntries.CreateDMCM_ARMFMFV(MFEWTandWVATDI, AllocationDate, DefaultInterestRate, AMParticipants, EnumDMCMCode.MFEWTWVATDI)
            Me._ListofDMCM.Add(MFEWTandWVATDIDMCM)
            For Each Item In MFEWTandWVATDI
                Select Case Item.CollectionType
                    Case EnumCollectionType.WithholdingTaxOnDefaultInterest
                        'Item.ListofDMCM.Add(MFEWTandWVATDIDMCM.DMCMNumber.ToString)
                        Item.GeneratedDMCM = MFEWTandWVATDIDMCM.DMCMNumber.ToString
                        ret.Add(Item)
                    Case EnumCollectionType.WithholdingVatOnDefaultInterest
                        'Item.ListofDMCM.Add(MFEWTandWVATDIDMCM.DMCMNumber.ToString)
                        Item.GeneratedDMCM = MFEWTandWVATDIDMCM.DMCMNumber.ToString
                        ret.Add(Item)
                End Select
            Next
        End If
        Return ret
    End Function

    Private Function CreateOffsettingARCollection(ByRef WESMBillSummaryItem As WESMBillSummary,
                                                  ByVal AllocationDate As Date,
                                                  ByVal OffsetAmount As Decimal,
                                                  ByVal EnumCollType As EnumCollectionType) As ARCollection

        Dim ret As New ARCollection
        With ret
            .WESMBillBatchNo = WESMBillSummaryItem.WESMBillBatchNo
            .WESMBillSummaryNo = WESMBillSummaryItem.WESMBillSummaryNo
            .BillingPeriod = WESMBillSummaryItem.BillPeriod
            .IDNumber = WESMBillSummaryItem.IDNumber.IDNumber
            .ParticipantID = WESMBillSummaryItem.IDNumber.ParticipantID
            .InvoiceNumber = WESMBillSummaryItem.INVDMCMNo
            .DueDate = WESMBillSummaryItem.DueDate
            .EndingBalance = WESMBillSummaryItem.EndingBalance
            .EnergyWithHold = WESMBillSummaryItem.EnergyWithhold
            .NewDueDate = WESMBillSummaryItem.OrigNewDueDate
            If EnumCollType = EnumCollectionType.Energy Or EnumCollType = EnumCollectionType.VatOnEnergy Or
                    EnumCollType = EnumCollectionType.MarketFees Or EnumCollType = EnumCollectionType.VatOnMarketFees Then
                
                .NewEndingBalance = WESMBillSummaryItem.EndingBalance - OffsetAmount
                With WESMBillSummaryItem
                    .NewDueDate = AllocationDate
                    .TransactionDate = AllocationDate
                    .EndingBalance = .EndingBalance - OffsetAmount
                    .StatusUpdate = True
                End With
                'addded by lance to adjust the new due date for offseting the default interest only on Energy
            ElseIf EnumCollType = EnumCollectionType.DefaultInterestOnEnergy Then
                With WESMBillSummaryItem
                    .NewDueDate = AllocationDate
                    .TransactionDate = AllocationDate
                    .StatusUpdate = True
                End With
            Else
                .NewEndingBalance = WESMBillSummaryItem.EndingBalance
            End If
            .AllocationDate = AllocationDate
            .AllocationAmount = OffsetAmount
            .CollectionType = EnumCollType
            .CollectionCategory = EnumCollectionCategory.Offset
            .OffsettingSequence = Me.OffsettingSequence
            .BillingRemarks = WESMBillSummaryItem.BillingRemarks
        End With

        Return ret
    End Function

    Private Function CreateOffsettingShareDetailsItem(ByVal WESMBillSummaryItem As WESMBillSummary,
                                                      ByVal OffsetAmount As Decimal) As PaymentShareDetails
        Dim ret As New PaymentShareDetails

        Using _OffsetShareDetails As New PaymentShareDetails
            With _OffsetShareDetails
                .ForWESMBillSummaryNo = WESMBillSummaryItem.WESMBillSummaryNo
                .BillingPeriod = WESMBillSummaryItem.BillPeriod
                .IDNumber = WESMBillSummaryItem.IDNumber.IDNumber
                .InvoiceNumber = WESMBillSummaryItem.INVDMCMNo
                .OffsetAmount = Math.Abs(OffsetAmount)
                .BatchGroupSequence = Me.OffsettingSequence
                .ChargeType = WESMBillSummaryItem.ChargeType
            End With
            ret = _OffsetShareDetails
        End Using
        Return ret
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
