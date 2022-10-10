Imports AccountsManagementObjects
Imports AccountsManagementDataAccess

Public Class ARCollectionProcess

#Region "Property of EnergyCollectionList"
    Private _EnergyCollection As New List(Of ARCollection)
    Public ReadOnly Property EnergyCollection() As List(Of ARCollection)
        Get
            Return _EnergyCollection
        End Get
    End Property
#End Region

#Region "Property of EnergyCollectionListDT"
    Private _EnergyCollectionDT As New DataTable
    Public ReadOnly Property EnergyCollectionDT() As DataTable
        Get
            Return _EnergyCollectionDT
        End Get
    End Property
#End Region

#Region "Property of VATonEnergyCollectionList"
    Private _VATCollection As New List(Of ARCollection)
    Public ReadOnly Property VATCollection() As List(Of ARCollection)
        Get
            Return _VATCollection
        End Get
    End Property
#End Region

#Region "Property of VATonEnergyCollectionListDT"
    Private _VATonEnergyCollectionDT As New DataTable
    Public ReadOnly Property VATonEnergyCollectionDT() As DataTable
        Get
            Return _VATonEnergyCollectionDT
        End Get
    End Property
#End Region

#Region "Property of MFwithVATCollectionList"
    Private _MFwithVATCollectionList As New List(Of ARCollection)
    Public ReadOnly Property MFwithVATCollectionList() As List(Of ARCollection)
        Get
            Return _MFwithVATCollectionList
        End Get
    End Property
#End Region

#Region "Property of MFwithVATCollectionListDT"
    Private _MFwithVATCollectionDT As New DataTable
    Public ReadOnly Property MFwithVATCollectionDT() As DataTable
        Get
            Return _MFwithVATCollectionDT
        End Get
    End Property
#End Region    

#Region "Total Collections Per BP of Energy"
    Private _TotalEnergyCollectionPerBP As New List(Of ARCollectionPerBP)
    Public ReadOnly Property TotalEnergyCollectionPerBP() As List(Of ARCollectionPerBP)
        Get
            Return _TotalEnergyCollectionPerBP
        End Get
    End Property
#End Region

#Region "Total Collections Per BP of VATonEnergy"
    Private _TotalVATCollectionPerBP As New List(Of ARCollectionPerBP)
    Public ReadOnly Property TotalVATCollectionPerBP() As List(Of ARCollectionPerBP)
        Get
            Return _TotalVATCollectionPerBP
        End Get
    End Property
#End Region
    
#Region "Total Collections Per BP of MFwithVAT"
    Private _TotalMFCollectionPerBP As New List(Of ARCollectionPerBP)
    Public ReadOnly Property TotalMFCollectionPerBP() As List(Of ARCollectionPerBP)
        Get
            Return _TotalMFCollectionPerBP
        End Get
    End Property
#End Region

#Region "Main Function"
    Public Sub ComputeARCollection(ByVal _ARAllocationList As List(Of ARCollection), ByVal EnumCollType As EnumCollectionType)
        Select Case EnumCollType
            Case EnumCollectionType.Energy
                Me.Create_AREnergy(_ARAllocationList)
            Case EnumCollectionType.VatOnEnergy
                Me.Create_ARVAT(_ARAllocationList)
            Case EnumCollectionType.MarketFees
                Me.Create_ARMF(_ARAllocationList)
        End Select
    End Sub

    Public Sub GetARAllocationList(ByVal _ARAllocationList As List(Of ARCollection))
        Dim ret As New List(Of DataTable)
        Me.Create_AREnergy(_ARAllocationList)
        'Me._EnergyCollectionDT = Me.Create_AREnergyDT(_ARAllocationList)

        Me.Create_ARVAT(_ARAllocationList)
        'Me._VATonEnergyCollectionDT = Me.Create_ARVATDT(_ARAllocationList)

        Me.Create_ARMF(_ARAllocationList)
        'Me._MFwithVATCollectionDT = Me.Create_ARMFDT(_ARAllocationList)
    End Sub
#End Region

#Region "AR Energy"
    Public Sub Create_AREnergy(_ARAllocation As List(Of ARCollection))
        Dim ARAllocation_ = (From x In _ARAllocation
                            Where x.CollectionType = EnumCollectionType.Energy Or x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy
                            Select x Order By x.BillingPeriod).ToList()

        Me._EnergyCollection = ARAllocation_

        Dim GetDistinctBillingPeriod = (From x In ARAllocation_
                                        Order By x.BillingPeriod, x.DueDate
                                        Group x By x.WESMBillBatchNo, x.BillingPeriod, x.DueDate _
                                        Into y = Group Let Grp = New With {.WESMBillBatchNo = WESMBillBatchNo, .BillingPeriod = BillingPeriod, .DueDate = DueDate}
                                        Select Grp).ToList()

        For Each BPItem In GetDistinctBillingPeriod
            Dim ARCollPerBP As New ARCollectionPerBP
            Dim OriginalDueDate = (From x In ARAllocation_
                                   Where x.BillingPeriod = BPItem.BillingPeriod _
                                   And x.DueDate = BPItem.DueDate _
                                   And x.CollectionType = EnumCollectionType.Energy _
                                   And x.CollectionCategory = EnumCollectionCategory.Cash _
                                   Select x.DueDate).FirstOrDefault

            Dim CashColl_EnergyPerBPItem = (From x In ARAllocation_
                                            Where x.WESMBillBatchNo = BPItem.WESMBillBatchNo _
                                            And x.BillingPeriod = BPItem.BillingPeriod _
                                            And x.DueDate = BPItem.DueDate _
                                            And x.CollectionType = EnumCollectionType.Energy _
                                            And x.CollectionCategory = EnumCollectionCategory.Cash _
                                            Select x.AllocationAmount).Sum()

            Dim CashColl_EnergyDIPerBPItem = (From x In ARAllocation_
                                              Where x.WESMBillBatchNo = BPItem.WESMBillBatchNo _
                                              And x.BillingPeriod = BPItem.BillingPeriod _
                                              And x.DueDate = BPItem.DueDate _
                                              And x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy _
                                              And x.CollectionCategory = EnumCollectionCategory.Cash _
                                              Select x.AllocationAmount).Sum()

            Dim DDColl_EnergyPerBPItem = (From x In ARAllocation_
                                          Where x.WESMBillBatchNo = BPItem.WESMBillBatchNo _
                                          And x.BillingPeriod = BPItem.BillingPeriod _
                                          And x.DueDate = BPItem.DueDate _
                                          And x.CollectionType = EnumCollectionType.Energy _
                                          And x.CollectionCategory = EnumCollectionCategory.Drawdown _
                                          Select x.AllocationAmount).Sum()

            Dim DDColl_EnergyDIPerBPItem = (From x In ARAllocation_
                                            Where x.WESMBillBatchNo = BPItem.WESMBillBatchNo _
                                            And x.BillingPeriod = BPItem.BillingPeriod _
                                            And x.DueDate = BPItem.DueDate _
                                            And x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy _
                                            And x.CollectionCategory = EnumCollectionCategory.Drawdown _
                                            Select x.AllocationAmount).Sum()

            Dim OffsetColl_EnergyPerBPItem = (From x In ARAllocation_
                                              Where x.WESMBillBatchNo = BPItem.WESMBillBatchNo _
                                              And x.BillingPeriod = BPItem.BillingPeriod _
                                              And x.DueDate = BPItem.DueDate _
                                              And x.CollectionType = EnumCollectionType.Energy _
                                              And x.CollectionCategory = EnumCollectionCategory.Offset _
                                              Select x.AllocationAmount).Sum()

            Dim OffsetColl_EnergyDIPerBPItem = (From x In ARAllocation_
                                                Where x.WESMBillBatchNo = BPItem.WESMBillBatchNo _
                                                And x.BillingPeriod = BPItem.BillingPeriod _
                                                And x.DueDate = BPItem.DueDate _
                                                And x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy _
                                                And x.CollectionCategory = EnumCollectionCategory.Offset _
                                                Select x.AllocationAmount).Sum()

            ARCollPerBP.WESMBillBatchNo = BPItem.WESMBillBatchNo
            ARCollPerBP.BillingPeriod = BPItem.BillingPeriod
            ARCollPerBP.OrigDueDate = BPItem.DueDate
            ARCollPerBP.CashAmount = Math.Abs(CashColl_EnergyPerBPItem)
            ARCollPerBP.CashAmountDI = Math.Abs(CashColl_EnergyDIPerBPItem)
            ARCollPerBP.DrawDownAmount = Math.Abs(DDColl_EnergyPerBPItem)
            ARCollPerBP.DrawDownAmountDI = Math.Abs(DDColl_EnergyDIPerBPItem)
            ARCollPerBP.OffsetAmount = Math.Abs(OffsetColl_EnergyPerBPItem)
            ARCollPerBP.OffsetAmountDI = Math.Abs(OffsetColl_EnergyDIPerBPItem)

            Me._TotalEnergyCollectionPerBP.Add(ARCollPerBP)
        Next
        Me._TotalEnergyCollectionPerBP.TrimExcess()
    End Sub

    Private Function ComparingEndingBalance(ByVal EndingBalance1 As Decimal, ByVal EndingBalance2 As Decimal) As Decimal
        Dim ret As Decimal = 0D

        If Math.Abs(EndingBalance1) > Math.Abs(EndingBalance2) Then
            ret = EndingBalance1
        Else
            ret = EndingBalance2
        End If
        Return ret
    End Function

    Private Function ComparingNewEndingBalance(ByVal EndingBalance1 As Decimal, ByVal EndingBalance2 As Decimal) As Decimal
        Dim ret As Decimal = 0D

        If Math.Abs(EndingBalance1) > Math.Abs(EndingBalance2) Then
            ret = EndingBalance2
        Else
            ret = EndingBalance1
        End If
        Return ret
    End Function

    Public Function Create_AREnergyDT(_ARAllocation As List(Of ARCollection)) As DataTable
        Dim EnergyARDT As New DataTable
        Dim ARDictionary As New Dictionary(Of String, Integer)
        Dim ARAllocation_ = (From x In _ARAllocation
                           Where x.CollectionType = EnumCollectionType.Energy Or x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy
                           Select x Order By x.DueDate, x.BillingPeriod, x.IDNumber, x.CollectionCategory, x.CollectionType Descending).ToList()

        EnergyARDT.TableName = "AccountReceivablesEnergy"
        With EnergyARDT.Columns
            .Add("BillingPeriod", GetType(Long))
            .Add("IDNumber", GetType(String))
            .Add("ParticipantID", GetType(String))
            .Add("InvoiceNumber", GetType(String))
            .Add("DueDate", GetType(Date))
            .Add("NewDueDate", GetType(Date))
            .Add("OutstandingBalance", GetType(String))
            .Add("EnergyWithHold", GetType(String))
            .Add("CollectionDate", GetType(Date))
            .Add("EnergyAmount", GetType(String))
            .Add("EnergyWithHoldAmount", GetType(String))
            .Add("EnergyAmountDI", GetType(String))
            .Add("EnergyDrawDown", GetType(String))
            .Add("EnergyDrawDownDI", GetType(String))
            .Add("NewOutstandingBalance", GetType(String))
        End With

        Dim Counter As Long = 0
        For Each ARItem As ARCollection In ARAllocation_
            Dim row As DataRow
            If Not ARDictionary.ContainsKey(ARItem.InvoiceNumber) Then
                ARDictionary.Add(ARItem.InvoiceNumber, Counter)
                row = EnergyARDT.NewRow()
                row("BillingPeriod") = ARItem.BillingPeriod
                row("IDNumber") = ARItem.IDNumber
                row("ParticipantID") = ARItem.ParticipantID
                row("InvoiceNumber") = ARItem.InvoiceNumber
                row("DueDate") = ARItem.DueDate
                row("OutstandingBalance") = FormatNumber(ARItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                row("EnergyWithHold") = If(ARItem.EnergyWithHold <> 0, FormatNumber(ARItem.EnergyWithHold, UseParensForNegativeNumbers:=TriState.True), "")
                row("NewDueDate") = ARItem.NewDueDate
                row("CollectionDate") = ARItem.AllocationDate

                Select Case ARItem.CollectionCategory
                    Case EnumCollectionCategory.Cash
                        Select Case ARItem.CollectionType
                            Case EnumCollectionType.Energy
                                row("EnergyAmount") = FormatNumber(ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                                row("NewOutstandingBalance") = FormatNumber(ARItem.NewEndingBalance, UseParensForNegativeNumbers:=TriState.True)
                            Case EnumCollectionType.WithholdingTaxonEnergy
                                row("EnergyWithHoldAmount") = FormatNumber(ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                                row("NewOutstandingBalance") = FormatNumber(ARItem.NewEndingBalance, UseParensForNegativeNumbers:=TriState.True)
                            Case EnumCollectionType.DefaultInterestOnEnergy
                                row("EnergyAmountDI") = FormatNumber(ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                                row("NewOutstandingBalance") = FormatNumber(ARItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                        End Select
                    Case EnumCollectionCategory.Drawdown
                        Select Case ARItem.CollectionType
                            Case EnumCollectionType.Energy
                                row("EnergyDrawDown") = FormatNumber(ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                                row("NewOutstandingBalance") = FormatNumber(ARItem.NewEndingBalance, UseParensForNegativeNumbers:=TriState.True)
                            Case EnumCollectionType.DefaultInterestOnEnergy
                                row("EnergyDrawDownDI") = FormatNumber(ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                                row("NewOutstandingBalance") = FormatNumber(ARItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                        End Select
                    Case EnumCollectionCategory.Offset
                        Select Case ARItem.CollectionType
                            Case EnumCollectionType.Energy
                                row("EnergyAmount") = FormatNumber(ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                                row("NewOutstandingBalance") = FormatNumber(ARItem.NewEndingBalance, UseParensForNegativeNumbers:=TriState.True)
                            Case EnumCollectionType.DefaultInterestOnEnergy
                                row("EnergyAmountDI") = FormatNumber(ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                                row("NewOutstandingBalance") = FormatNumber(ARItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                        End Select
                End Select

                Counter += 1
                EnergyARDT.Rows.Add(row)
            Else
                row = EnergyARDT(ARDictionary(ARItem.InvoiceNumber))
                Dim ActualEndingBalance As Decimal = Me.ComparingEndingBalance(CDec(row("OutstandingBalance")), ARItem.EndingBalance)
                row("OutstandingBalance") = FormatNumber(ActualEndingBalance, UseParensForNegativeNumbers:=TriState.True)

                Select Case ARItem.CollectionCategory
                    Case EnumCollectionCategory.Cash
                        Select Case ARItem.CollectionType
                            Case EnumCollectionType.Energy
                                row("EnergyAmount") = FormatNumber(If(IsDBNull(row("EnergyAmount")), 0, CDec(row("EnergyAmount"))) + ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                                row("NewOutstandingBalance") = FormatNumber(Me.ComparingNewEndingBalance(ARItem.NewEndingBalance, CDec(row("NewOutstandingBalance"))), UseParensForNegativeNumbers:=TriState.True)
                            Case EnumCollectionType.WithholdingTaxonEnergy
                                row("EnergyWithHoldAmount") = FormatNumber(If(IsDBNull(row("EnergyWithHoldAmount")), 0, CDec(row("EnergyWithHoldAmount"))) + ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                                row("NewOutstandingBalance") = FormatNumber(Me.ComparingNewEndingBalance(ARItem.NewEndingBalance, CDec(row("NewOutstandingBalance"))), UseParensForNegativeNumbers:=TriState.True)
                            Case EnumCollectionType.DefaultInterestOnEnergy
                                row("EnergyAmountDI") = FormatNumber(If(IsDBNull(row("EnergyDrawDownDI")), 0, CDec(row("EnergyDrawDownDI"))) + ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                        End Select
                    Case EnumCollectionCategory.Drawdown
                        Select Case ARItem.CollectionType
                            Case EnumCollectionType.Energy
                                row("EnergyAmount") = FormatNumber(If(IsDBNull(row("EnergyAmount")), 0, CDec(row("EnergyAmount"))) + ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                                row("NewOutstandingBalance") = FormatNumber(Me.ComparingNewEndingBalance(ARItem.NewEndingBalance, CDec(row("NewOutstandingBalance"))), UseParensForNegativeNumbers:=TriState.True)
                            Case EnumCollectionType.DefaultInterestOnEnergy
                                row("EnergyDrawDownDI") = FormatNumber(If(IsDBNull(row("EnergyDrawDownDI")), 0, CDec(row("EnergyDrawDownDI"))) + ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                        End Select
                End Select
            End If
        Next
        EnergyARDT.AcceptChanges()
        Return EnergyARDT
    End Function

    Public Function Create_OffsettingAREnergyDT(_ARAllocation As List(Of ARCollection)) As DataTable
        Dim EnergyARDT As New DataTable
        Dim ARDictionary As New Dictionary(Of String, Integer)
        Dim ARDicOS As New Dictionary(Of String, String)
        Dim ARAllocation_ = (From x In _ARAllocation
                           Where x.CollectionType = EnumCollectionType.Energy Or x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy
                           Select x Order By x.DueDate, x.BillingPeriod, x.IDNumber, x.CollectionCategory, x.CollectionType Descending).ToList()

        EnergyARDT.TableName = "AccountReceivablesEnergy"
        With EnergyARDT.Columns
            .Add("BillingPeriod", GetType(Long))
            .Add("IDNumber", GetType(String))
            .Add("ParticipantID", GetType(String))
            .Add("InvoiceNumber", GetType(String))
            .Add("DueDate", GetType(Date))
            .Add("NewDueDate", GetType(Date))
            .Add("OutstandingBalance", GetType(String))
            .Add("EnergyWithHold", GetType(String))
            .Add("CollectionDate", GetType(Date))
            .Add("EnergyOffsetAmount", GetType(String))
            .Add("EnergyOffsetAmountDI", GetType(String))
            .Add("NewOutstandingBalance", GetType(String))
            .Add("OffsetSeqNo", GetType(String))
        End With

        Dim Counter As Long = 0
        For Each ARItem As ARCollection In ARAllocation_
            Dim row As DataRow            
            If Not ARDictionary.ContainsKey(ARItem.InvoiceNumber) Then
                ARDictionary.Add(ARItem.InvoiceNumber, Counter)
                row = EnergyARDT.NewRow()

                row("BillingPeriod") = ARItem.BillingPeriod
                row("IDNumber") = ARItem.IDNumber
                row("ParticipantID") = ARItem.ParticipantID
                row("InvoiceNumber") = ARItem.InvoiceNumber
                row("DueDate") = ARItem.DueDate
                row("OutstandingBalance") = FormatNumber(ARItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                row("EnergyWithHold") = If(ARItem.EnergyWithHold <> 0, FormatNumber(ARItem.EnergyWithHold, UseParensForNegativeNumbers:=TriState.True), "")
                row("NewDueDate") = ARItem.NewDueDate
                row("CollectionDate") = ARItem.AllocationDate
                If Not ARDicOS.ContainsKey(ARItem.InvoiceNumber & ARItem.OffsettingSequence) Then
                    ARDicOS.Add(ARItem.InvoiceNumber & ARItem.OffsettingSequence, ARItem.OffsettingSequence.ToString)
                    row("OffsetSeqNo") = ARItem.OffsettingSequence
                End If

                Select Case ARItem.CollectionCategory
                    Case EnumCollectionCategory.Offset
                        Select Case ARItem.CollectionType
                            Case EnumCollectionType.Energy
                                row("EnergyOffsetAmount") = FormatNumber(ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                                row("NewOutstandingBalance") = FormatNumber(ARItem.NewEndingBalance, UseParensForNegativeNumbers:=TriState.True)
                            Case EnumCollectionType.DefaultInterestOnEnergy
                                row("EnergyOffsetAmountDI") = FormatNumber(ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                                row("NewOutstandingBalance") = FormatNumber(ARItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                        End Select
                End Select

                Counter += 1
                EnergyARDT.Rows.Add(row)
            Else
                row = EnergyARDT(ARDictionary(ARItem.InvoiceNumber))
                Dim ActualEndingBalance As Decimal = Me.ComparingEndingBalance(CDec(row("OutstandingBalance")), ARItem.EndingBalance)
                row("OutstandingBalance") = FormatNumber(ActualEndingBalance, UseParensForNegativeNumbers:=TriState.True)

                If Not ARDicOS.ContainsKey(ARItem.InvoiceNumber & ARItem.OffsettingSequence) Then
                    ARDicOS.Add(ARItem.InvoiceNumber & ARItem.OffsettingSequence, ARItem.OffsettingSequence.ToString)
                    row("OffsetSeqNo") &= ";" & ARItem.OffsettingSequence
                End If

                Select Case ARItem.CollectionCategory
                    Case EnumCollectionCategory.Offset
                        Select Case ARItem.CollectionType
                            Case EnumCollectionType.Energy
                                row("EnergyOffsetAmount") = FormatNumber(If(IsDBNull(row("EnergyOffsetAmount")), 0, CDec(row("EnergyOffsetAmount"))) + ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                                row("NewOutstandingBalance") = FormatNumber(Me.ComparingNewEndingBalance(ARItem.NewEndingBalance, CDec(row("NewOutstandingBalance"))), UseParensForNegativeNumbers:=TriState.True)
                            Case EnumCollectionType.DefaultInterestOnEnergy
                                row("EnergyOffsetAmountDI") = FormatNumber(ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                                row("NewOutstandingBalance") = FormatNumber(ActualEndingBalance, UseParensForNegativeNumbers:=TriState.True)
                        End Select
                End Select
            End If
        Next
        EnergyARDT.AcceptChanges()
        Return EnergyARDT
    End Function

#End Region

#Region "AR VAT"
    Public Sub Create_ARVAT(_ARAllocation As List(Of ARCollection))
        Dim ARAllocation_ = (From x In _ARAllocation
                            Where x.CollectionType = EnumCollectionType.VatOnEnergy
                            Select x Order By x.BillingPeriod).ToList()
        Me._VATCollection = ARAllocation_
        Dim GetDistinctBillingPeriod = (From x In ARAllocation_
                                        Order By x.WESMBillBatchNo,  x.BillingPeriod, x.DueDate
                                        Group x By x.WESMBillBatchNo, x.BillingPeriod, x.DueDate _
                                        Into y = Group Let Grp = New With {.WESMBillBatchNo = WESMBillBatchNo,.BillingPeriod = BillingPeriod, .DueDate = DueDate}
                                        Select Grp).ToList()

        For Each BPItem In GetDistinctBillingPeriod
            Dim ARCollPerBP As New ARCollectionPerBP
            Dim CashColl_VATPerBPItem = (From x In ARAllocation_
                                         Where x.WESMBillBatchNo = BPItem.WESMBillBatchNo _
                                         And x.BillingPeriod = BPItem.BillingPeriod _
                                         And x.DueDate = BPItem.DueDate _
                                         And x.CollectionType = EnumCollectionType.VatOnEnergy _
                                         And x.CollectionCategory = EnumCollectionCategory.Cash _
                                         Select x.AllocationAmount).Sum()

            Dim OffsetColl_VATPerBPItem = (From x In ARAllocation_
                                           Where x.WESMBillBatchNo = BPItem.WESMBillBatchNo _
                                           And x.BillingPeriod = BPItem.BillingPeriod _
                                           And x.DueDate = BPItem.DueDate _
                                           And x.CollectionType = EnumCollectionType.VatOnEnergy _
                                           And x.CollectionCategory = EnumCollectionCategory.Offset _
                                           Select x.AllocationAmount).Sum()

            ARCollPerBP.WESMBillBatchNo = BPItem.WESMBillBatchNo
            ARCollPerBP.BillingPeriod = BPItem.BillingPeriod
            ARCollPerBP.OrigDueDate = BPItem.DueDate
            ARCollPerBP.CashAmount = Math.Abs(CashColl_VATPerBPItem)
            ARCollPerBP.CashAmountDI = 0
            ARCollPerBP.DrawDownAmount = 0
            ARCollPerBP.DrawDownAmountDI = 0
            ARCollPerBP.OffsetAmount = Math.Abs(OffsetColl_VATPerBPItem)
            ARCollPerBP.OffsetAmountDI = 0
            _TotalVATCollectionPerBP.Add(ARCollPerBP)
        Next
        _TotalVATCollectionPerBP.TrimExcess()
    End Sub

    Public Function Create_ARVATDT(_ARAllocation As List(Of ARCollection)) As DataTable
        Dim VATARDT As New DataTable
        Dim ARDictionary As New Dictionary(Of String, Integer)
        Dim ARAllocation_ = (From x In _ARAllocation
                           Where x.CollectionType = EnumCollectionType.VatOnEnergy
                           Select x Order By x.BillingPeriod).ToList()

        VATARDT.TableName = "AccountReceivablesVAT"
        With VATARDT.Columns
            .Add("BillingPeriod", GetType(Long))
            .Add("IDNumber", GetType(String))
            .Add("ParticipantID", GetType(String))
            .Add("InvoiceNumber", GetType(String))
            .Add("DueDate", GetType(Date))
            .Add("OutstandingBalance", GetType(String))
            .Add("NewDueDate", GetType(Date))
            .Add("AllocationDate", GetType(Date))
            .Add("VATAmount", GetType(String))
            .Add("NewOutstandingBalance", GetType(String))
        End With

        Dim Counter As Long = 0
        For Each ARItem As ARCollection In ARAllocation_
            Dim row As DataRow
            If Not ARDictionary.ContainsKey(ARItem.InvoiceNumber) Then
                ARDictionary.Add(ARItem.InvoiceNumber, Counter)
                row = VATARDT.NewRow()

                row("BillingPeriod") = ARItem.BillingPeriod
                row("IDNumber") = ARItem.IDNumber
                row("ParticipantID") = ARItem.ParticipantID
                row("InvoiceNumber") = ARItem.InvoiceNumber
                row("DueDate") = ARItem.DueDate
                row("OutstandingBalance") = FormatNumber(ARItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                row("NewDueDate") = ARItem.NewDueDate
                row("AllocationDate") = ARItem.AllocationDate
                row("VATAmount") = FormatNumber(ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                row("NewOutstandingBalance") = FormatNumber(ARItem.EndingBalance - ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                Counter += 1
                VATARDT.Rows.Add(row)
            Else
                row = VATARDT(ARDictionary(ARItem.InvoiceNumber))
                row("VATAmount") = FormatNumber(CDec(row("VATAmount")) + ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                row("NewOutstandingBalance") = FormatNumber(Me.ComparingNewEndingBalance(ARItem.NewEndingBalance, CDec(row("NewOutstandingBalance"))), UseParensForNegativeNumbers:=TriState.True)
            End If
        Next
        VATARDT.AcceptChanges()

        Return VATARDT
    End Function

    Public Function Create_OffsettingARVATDT(_ARAllocation As List(Of ARCollection)) As DataTable
        Dim VATARDT As New DataTable
        Dim ARDictionary As New Dictionary(Of String, Integer)
        Dim ARDicOS As New Dictionary(Of String, String)
        Dim ARAllocation_ = (From x In _ARAllocation
                           Where x.CollectionType = EnumCollectionType.VatOnEnergy
                           Select x Order By x.BillingPeriod).ToList()

        VATARDT.TableName = "AccountReceivablesVAT"
        With VATARDT.Columns
            .Add("BillingPeriod", GetType(Long))
            .Add("IDNumber", GetType(String))
            .Add("ParticipantID", GetType(String))
            .Add("InvoiceNumber", GetType(String))
            .Add("DueDate", GetType(Date))
            .Add("OutstandingBalance", GetType(String))
            .Add("NewDueDate", GetType(Date))
            .Add("AllocationDate", GetType(Date))
            .Add("OffsetVATAmount", GetType(String))
            .Add("NewOutstandingBalance", GetType(String))
            .Add("OffsetSeqNo", GetType(String))
        End With

        Dim Counter As Long = 0
        For Each ARItem As ARCollection In _ARAllocation
            Dim row As DataRow
            If Not ARDictionary.ContainsKey(ARItem.InvoiceNumber) Then
                ARDictionary.Add(ARItem.InvoiceNumber, Counter)
                row = VATARDT.NewRow()
                row("BillingPeriod") = ARItem.BillingPeriod
                row("IDNumber") = ARItem.IDNumber
                row("ParticipantID") = ARItem.ParticipantID
                row("InvoiceNumber") = ARItem.InvoiceNumber
                row("DueDate") = ARItem.DueDate
                row("OutstandingBalance") = FormatNumber(ARItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                row("NewDueDate") = ARItem.NewDueDate
                row("AllocationDate") = ARItem.AllocationDate
                row("OffsetVATAmount") = FormatNumber(ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                row("NewOutstandingBalance") = FormatNumber(ARItem.EndingBalance - ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)

                If Not ARDicOS.ContainsKey(ARItem.InvoiceNumber & ARItem.OffsettingSequence) Then
                    ARDicOS.Add(ARItem.InvoiceNumber & ARItem.OffsettingSequence, ARItem.OffsettingSequence.ToString)
                    row("OffsetSeqNo") = ARItem.OffsettingSequence
                End If
                Counter += 1
                VATARDT.Rows.Add(row)
            Else
                row = VATARDT(ARDictionary(ARItem.InvoiceNumber))
                row("OffsetVATAmount") = FormatNumber(CDec(row("OffsetVATAmount")) + ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                row("NewOutstandingBalance") = FormatNumber(Me.ComparingNewEndingBalance(ARItem.NewEndingBalance, CDec(row("NewOutstandingBalance"))), UseParensForNegativeNumbers:=TriState.True)
                If Not ARDicOS.ContainsKey(ARItem.InvoiceNumber & ARItem.OffsettingSequence) Then
                    ARDicOS.Add(ARItem.InvoiceNumber & ARItem.OffsettingSequence, ARItem.OffsettingSequence.ToString)
                    row("OffsetSeqNo") &= ";" & ARItem.OffsettingSequence
                End If

            End If
        Next
        VATARDT.AcceptChanges()

        Return VATARDT
    End Function
#End Region

#Region "AR MF"
    Public Sub Create_ARMF(_ARAllocation As List(Of ARCollection))

        Dim ARAllocation_ = (From x In _ARAllocation _
                            Where (x.CollectionType = EnumCollectionType.MarketFees Or
                                    x.CollectionType = EnumCollectionType.VatOnMarketFees Or _
                                    x.CollectionType = EnumCollectionType.DefaultInterestOnMF Or _
                                    x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF Or _
                                    x.CollectionType = EnumCollectionType.WithholdingTaxOnMF Or _
                                    x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest Or _
                                    x.CollectionType = EnumCollectionType.WithholdingVatOnMF Or _
                                    x.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest) _
                            Select x Order By x.BillingPeriod).ToList()

        Me._MFwithVATCollectionList = ARAllocation_

        Dim GetDistinctBillingPeriod = (From x In ARAllocation_
                                        Order By x.WESMBillBatchNo, x.BillingPeriod, x.DueDate
                                        Group x By x.WESMBillBatchNo, x.BillingPeriod, x.DueDate _
                                        Into y = Group Let Grp = New With {.WESMBillBatchNo = WESMBillBatchNo, .BillingPeriod = BillingPeriod, .DueDate = DueDate}
                                        Select Grp).ToList()

        For Each BPItem In GetDistinctBillingPeriod
            Dim ARCollPerBP As New ARCollectionPerBP

            Dim OriginalDueDate = (From x In ARAllocation_
                                   Where x.WESMBillBatchNo = BPItem.WESMBillBatchNo _
                                   And x.BillingPeriod = BPItem.BillingPeriod _
                                   And x.DueDate = BPItem.DueDate _
                                   And (x.CollectionType = EnumCollectionType.MarketFees Or
                                        x.CollectionType = EnumCollectionType.VatOnMarketFees Or _
                                        x.CollectionType = EnumCollectionType.DefaultInterestOnMF Or _
                                        x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF Or _
                                        x.CollectionType = EnumCollectionType.WithholdingTaxOnMF Or _
                                        x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest Or _
                                        x.CollectionType = EnumCollectionType.WithholdingVatOnMF Or _
                                        x.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest) _
                                   And x.CollectionCategory = EnumCollectionCategory.Cash _
                                   Select x.DueDate).FirstOrDefault

            Dim CashColl_MFPerBPItem = (From x In ARAllocation_
                                            Where x.WESMBillBatchNo = BPItem.WESMBillBatchNo _
                                            And x.BillingPeriod = BPItem.BillingPeriod _
                                            And x.DueDate = BPItem.DueDate _
                                            And (x.CollectionType = EnumCollectionType.MarketFees Or
                                                x.CollectionType = EnumCollectionType.VatOnMarketFees Or _
                                                x.CollectionType = EnumCollectionType.WithholdingTaxOnMF Or _
                                                x.CollectionType = EnumCollectionType.WithholdingVatOnMF) _
                                            And x.CollectionCategory = EnumCollectionCategory.Cash _
                                            Select x.AllocationAmount).Sum()

            Dim CashColl_MFDIPerBPItem = (From x In ARAllocation_
                                              Where x.WESMBillBatchNo = BPItem.WESMBillBatchNo _
                                              And x.BillingPeriod = BPItem.BillingPeriod _
                                              And x.DueDate = BPItem.DueDate _
                                              And (x.CollectionType = EnumCollectionType.DefaultInterestOnMF Or _
                                                   x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF Or _
                                                   x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest Or _
                                                   x.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest) _
                                              And x.CollectionCategory = EnumCollectionCategory.Cash _
                                              Select x.AllocationAmount).Sum()

            Dim DDColl_MFPerBPItem = (From x In ARAllocation_
                                          Where x.WESMBillBatchNo = BPItem.WESMBillBatchNo _
                                          And x.BillingPeriod = BPItem.BillingPeriod _
                                          And x.DueDate = BPItem.DueDate _
                                          And (x.CollectionType = EnumCollectionType.MarketFees Or
                                                x.CollectionType = EnumCollectionType.VatOnMarketFees Or _
                                                x.CollectionType = EnumCollectionType.WithholdingTaxOnMF Or _
                                                x.CollectionType = EnumCollectionType.WithholdingVatOnMF) _
                                          And x.CollectionCategory = EnumCollectionCategory.Drawdown _
                                          Select x.AllocationAmount).Sum()

            Dim DDColl_MFDIPerBPItem = (From x In ARAllocation_
                                            Where x.WESMBillBatchNo = BPItem.WESMBillBatchNo _
                                            And x.BillingPeriod = BPItem.BillingPeriod _
                                            And x.DueDate = BPItem.DueDate _
                                            And (x.CollectionType = EnumCollectionType.DefaultInterestOnMF Or _
                                                   x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF Or _
                                                   x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest Or _
                                                   x.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest) _
                                            And x.CollectionCategory = EnumCollectionCategory.Drawdown _
                                            Select x.AllocationAmount).Sum()

            Dim OffsetColl_MFPerBPItem = (From x In ARAllocation_
                                              Where x.WESMBillBatchNo = BPItem.WESMBillBatchNo _
                                              And x.BillingPeriod = BPItem.BillingPeriod _
                                              And x.DueDate = BPItem.DueDate _
                                              And (x.CollectionType = EnumCollectionType.MarketFees Or
                                                x.CollectionType = EnumCollectionType.VatOnMarketFees Or _
                                                x.CollectionType = EnumCollectionType.WithholdingTaxOnMF Or _
                                                x.CollectionType = EnumCollectionType.WithholdingVatOnMF) _
                                              And x.CollectionCategory = EnumCollectionCategory.Offset _
                                              Select x.AllocationAmount).Sum()

            Dim OffsetColl_MFDIPerBPItem = (From x In ARAllocation_
                                            Where x.WESMBillBatchNo = BPItem.WESMBillBatchNo _
                                            And x.BillingPeriod = BPItem.BillingPeriod _
                                            And x.DueDate = BPItem.DueDate _
                                            And (x.CollectionType = EnumCollectionType.DefaultInterestOnMF Or _
                                                x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF Or _
                                                x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest Or _
                                                x.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest) _
                                            And x.CollectionCategory = EnumCollectionCategory.Offset _
                                            Select x.AllocationAmount).Sum()

            ARCollPerBP.WESMBillBatchNo = BPItem.WESMBillBatchNo
            ARCollPerBP.BillingPeriod = BPItem.BillingPeriod
            ARCollPerBP.OrigDueDate = BPItem.DueDate
            ARCollPerBP.CashAmount = Math.Abs(CashColl_MFPerBPItem)
            ARCollPerBP.CashAmountDI = Math.Abs(CashColl_MFDIPerBPItem)
            ARCollPerBP.DrawDownAmount = Math.Abs(DDColl_MFPerBPItem)
            ARCollPerBP.DrawDownAmountDI = Math.Abs(DDColl_MFDIPerBPItem)
            ARCollPerBP.OffsetAmount = Math.Abs(OffsetColl_MFPerBPItem)
            ARCollPerBP.OffsetAmountDI = Math.Abs(OffsetColl_MFDIPerBPItem)

            Me._TotalMFCollectionPerBP.Add(ARCollPerBP)
        Next
        Me._TotalMFCollectionPerBP.TrimExcess()
    End Sub

    Public Function Create_ARMFDT(_ARAllocation As List(Of ARCollection)) As DataTable

        Dim ARDicOS As New Dictionary(Of String, String)
        Dim ARAllocation = (From x In _ARAllocation _
                            Where Not (x.CollectionType = EnumCollectionType.Energy) _
                            And Not (x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy) _
                            And Not (x.CollectionType = EnumCollectionType.VatOnEnergy) _
                            Select x Order By x.BillingPeriod).ToList()
        Dim MFARDT As New DataTable
        Dim ARDictionary As New Dictionary(Of String, Integer)
        MFARDT.TableName = "AccountReceivablesMF"
        With MFARDT.Columns
            .Add("BillingPeriod", GetType(Long))
            .Add("IDNumber", GetType(String))
            .Add("ParticipantID", GetType(String))
            .Add("InvoiceNumber", GetType(String))
            .Add("DueDate", GetType(Date))
            .Add("NewDueDate", GetType(Date))
            .Add("OutstandingBalanceonMF", GetType(String))
            .Add("OutstandingBalanceonMFV", GetType(String))
            .Add("CollectionDate", GetType(Date))
            .Add("AmountonMF", GetType(String))
            .Add("AmountonMFDI", GetType(String))
            .Add("WTAXonMF", GetType(String))
            .Add("WTAXDIonMF", GetType(String))
            .Add("AmountonMFV", GetType(String))
            .Add("AmountonMFVDI", GetType(String))
            .Add("WVATonMF", GetType(String))
            .Add("WVATDIonMF", GetType(String))
            .Add("NewOutstandingBalanceonMF", GetType(String))
            .Add("NewOutstandingBalanceonMFV", GetType(String))
            .Add("OffsettingSequence", GetType(String))
        End With

        Dim Counter As Long = 0
        For Each ARItem As ARCollection In ARAllocation
            Dim row As DataRow
            If Not ARDictionary.ContainsKey(ARItem.InvoiceNumber) Then
                ARDictionary.Add(ARItem.InvoiceNumber, Counter)
                row = MFARDT.NewRow()
                row("BillingPeriod") = ARItem.BillingPeriod
                row("ParticipantID") = ARItem.ParticipantID
                row("IDNumber") = ARItem.IDNumber
                row("DueDate") = ARItem.DueDate
                row("NewDueDate") = ARItem.NewDueDate
                row("InvoiceNumber") = ARItem.InvoiceNumber
                row("CollectionDate") = ARItem.AllocationDate
                If Not ARDicOS.ContainsKey(ARItem.InvoiceNumber & ARItem.OffsettingSequence) Then
                    ARDicOS.Add(ARItem.InvoiceNumber & ARItem.OffsettingSequence, ARItem.OffsettingSequence.ToString)
                    row("OffsettingSequence") = ARItem.OffsettingSequence
                End If
                Select Case ARItem.CollectionType
                    Case EnumCollectionType.MarketFees
                        row("OutstandingBalanceonMF") = FormatNumber(ARItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                        row("AmountonMF") = FormatNumber(ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                        row("NewOutstandingBalanceonMF") = FormatNumber(ARItem.NewEndingBalance, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumCollectionType.DefaultInterestOnMF
                        row("AmountonMFDI") = FormatNumber(ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumCollectionType.VatOnMarketFees
                        row("OutstandingBalanceonMFV") = FormatNumber(ARItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                        row("AmountonMFV") = FormatNumber(ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                        row("NewOutstandingBalanceonMFV") = FormatNumber(ARItem.NewEndingBalance, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumCollectionType.DefaultInterestOnVatOnMF
                        row("AmountonMFVDI") = FormatNumber(ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumCollectionType.WithholdingVatOnMF
                        row("WVATonMF") = If(ARItem.AllocationAmount <> 0, FormatNumber(ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True), "")
                    Case EnumCollectionType.WithholdingVatOnDefaultInterest
                        row("WVATDIonMF") = If(ARItem.AllocationAmount <> 0, FormatNumber(ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True), "")
                    Case EnumCollectionType.WithholdingTaxOnMF
                        row("WTAXonMF") = If(ARItem.AllocationAmount <> 0, FormatNumber(ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True), "")
                    Case EnumCollectionType.WithholdingTaxOnDefaultInterest
                        row("WTAXDIonMF") = If(ARItem.AllocationAmount <> 0, FormatNumber(ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True), "")
                End Select
                Counter += 1
                MFARDT.Rows.Add(row)
            Else
                row = MFARDT(ARDictionary(ARItem.InvoiceNumber))
                If Not ARDicOS.ContainsKey(ARItem.InvoiceNumber & ARItem.OffsettingSequence) Then
                    ARDicOS.Add(ARItem.InvoiceNumber & ARItem.OffsettingSequence, ARItem.OffsettingSequence.ToString)
                    row("OffsettingSequence") &= ";" & ARItem.OffsettingSequence
                End If
                Select Case ARItem.CollectionType
                    Case EnumCollectionType.MarketFees
                        If IsDBNull(row("OutstandingBalanceonMF")) Then
                            row("OutstandingBalanceonMF") = FormatNumber(ARItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                        ElseIf Math.Abs(ARItem.EndingBalance) > Math.Abs(CDec(row("OutstandingBalanceonMF"))) Then
                            row("OutstandingBalanceonMF") = FormatNumber(ARItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                        End If
                        row("AmountonMF") = FormatNumber(CDec(If(IsDBNull(row("AmountonMF")), 0, row("AmountonMF"))) + ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                        row("NewOutstandingBalanceonMF") = FormatNumber(If(IsDBNull(row("OutstandingBalanceonMF")), 0, CDec(row("OutstandingBalanceonMF"))) - ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumCollectionType.DefaultInterestOnMF
                        row("AmountonMFDI") = FormatNumber(If(IsDBNull(row("AmountonMFDI")), 0, CDec(row("AmountonMFDI"))) + ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumCollectionType.VatOnMarketFees
                        If IsDBNull(row("OutstandingBalanceonMFV")) Then
                            row("OutstandingBalanceonMFV") = FormatNumber(ARItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                        ElseIf Math.Abs(ARItem.EndingBalance) > Math.Abs(CDec(row("OutstandingBalanceonMFV"))) Then
                            row("OutstandingBalanceonMFV") = FormatNumber(ARItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                        End If
                        row("AmountonMFV") = FormatNumber(If(IsDBNull(row("AmountonMFV")), 0, CDec(row("AmountonMFV"))) + ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                        row("NewOutstandingBalanceonMFV") = FormatNumber(If(IsDBNull(row("OutstandingBalanceonMFV")), 0, CDec(row("OutstandingBalanceonMFV"))) - ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumCollectionType.DefaultInterestOnVatOnMF
                        row("AmountonMFVDI") = FormatNumber(If(IsDBNull(row("AmountonMFVDI")), 0, CDec(row("AmountonMFVDI"))) + ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumCollectionType.WithholdingVatOnMF
                        row("WVATonMF") = FormatNumber(If(IsDBNull(row("WVATonMF")), 0, CDec(row("WVATonMF"))) + ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumCollectionType.WithholdingVatOnDefaultInterest
                        row("WVATDIonMF") = FormatNumber(If(IsDBNull(row("WVATDIonMF")), 0, CDec(row("WVATDIonMF"))) + ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumCollectionType.WithholdingTaxOnMF
                        row("WTAXonMF") = FormatNumber(If(IsDBNull(row("WTAXonMF")), 0, CDec(row("WTAXonMF"))) + ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumCollectionType.WithholdingTaxOnDefaultInterest
                        row("WTAXDIonMF") = FormatNumber(If(IsDBNull(row("WTAXDIonMF")), 0, CDec(row("WTAXDIonMF"))) + ARItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                End Select
            End If
        Next
        MFARDT.AcceptChanges()
        Return MFARDT
    End Function

#End Region

End Class
