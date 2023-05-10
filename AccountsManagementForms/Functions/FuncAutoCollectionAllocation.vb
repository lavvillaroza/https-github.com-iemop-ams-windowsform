'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             FuncAutoCollectionAllocation
'Orginal Author:         Vladimir E.Espiritu
'File Creation Date:     March 21, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for re-usable functions
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description
'   March 21, 2012          Vladimir E.Espiritu         Class initialization
'   May 20, 2013            Vladimir E. Espiritu        Added DrawdownDate
'

Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementLogic

Public Class FuncAutoCollectionAllocation


    Private Enum AllocationCategory
        WithHoldingTaxOnMF
        WithHoldingTaxOnMFV
        DefaultInterestOnMFandMFV
        MarketFees
        VatOnMarketFees
        DefaultInterestOnEnergy
        Energy
        VatOnEnergy
    End Enum


#Region "Initialization/Constructor"
    Public Sub New()
        Me._ListPrudentials = New List(Of Prudential)
        Me._ListCollections = New List(Of Collection)
        Me._ListWESMBillSummaries = New List(Of WESMBillSummary)
        Me._DicInterestRate = New Dictionary(Of Date, Decimal)
        Me._ListParticipants = New List(Of AMParticipants)
        Me._ListDocSignatories = New List(Of DocSignatories)
        Me._ListCollectionMonitoring = New List(Of CollectionMonitoring)
        Me._AllocationDate = Nothing
        Me._BFactory = BusinessFactory.GetInstance()
        Me._CollectionNo = 0
        Me._DicWESMBillDueDate = New Dictionary(Of Long, Date)
    End Sub
#End Region



#Region "BFactory"
    Private _BFactory As BusinessFactory
    Public ReadOnly Property BFactory() As BusinessFactory
        Get
            Return Me._BFactory
        End Get
    End Property
#End Region

#Region "ListPrudentials"
    Private _ListPrudentials As List(Of Prudential)
    Public Property ListPrudentials() As List(Of Prudential)
        Get
            Return _ListPrudentials
        End Get
        Set(ByVal value As List(Of Prudential))
            _ListPrudentials = value
        End Set
    End Property

#End Region

#Region "ListCollections"
    Private _ListCollections As List(Of Collection)
    Public Property ListCollections() As List(Of Collection)
        Get
            Return _ListCollections
        End Get
        Set(ByVal value As List(Of Collection))
            _ListCollections = value
        End Set
    End Property

#End Region

#Region "ListWESMBillSummaries"
    Private _ListWESMBillSummaries As List(Of WESMBillSummary)
    Public Property ListWESMBillSummaries() As List(Of WESMBillSummary)
        Get
            Return _ListWESMBillSummaries
        End Get
        Set(ByVal value As List(Of WESMBillSummary))
            _ListWESMBillSummaries = value
        End Set
    End Property

#End Region

#Region "DicInterestRate"
    Private _DicInterestRate As Dictionary(Of Date, Decimal)
    Public Property DicInterestRate() As Dictionary(Of Date, Decimal)
        Get
            Return _DicInterestRate
        End Get
        Set(ByVal value As Dictionary(Of Date, Decimal))
            _DicInterestRate = value
        End Set
    End Property


#End Region

#Region "ListParticipants"
    Private _ListParticipants As List(Of AMParticipants)
    Public Property ListParticipants() As List(Of AMParticipants)
        Get
            Dim item1 = (From x In Me.ListWESMBillSummaries _
                         Select x.IDNumber.IDNumber Distinct).ToList()

            Dim item2 = (From x In Me.ListCollections _
                         Select x.IDNumber Distinct).ToList()

            Dim item3 As New List(Of String)
            item3.AddRange(item1)
            item3.AddRange(item2)

            _ListParticipants = (From x In item3 Join y In Me._ListParticipants On x Equals y.IDNumber _
                                 Select y Distinct).ToList()

            Return _ListParticipants
        End Get
        Set(ByVal value As List(Of AMParticipants))
            _ListParticipants = value
        End Set
    End Property

#End Region

#Region "ListDocSignatories"
    Private _ListDocSignatories As List(Of DocSignatories)
    Public Property ListDocSignatories() As List(Of DocSignatories)
        Get
            Return _ListDocSignatories
        End Get
        Set(ByVal value As List(Of DocSignatories))
            _ListDocSignatories = value
        End Set
    End Property

#End Region

#Region "ListCollectionMonitoring"
    Private _ListCollectionMonitoring As List(Of CollectionMonitoring)
    Public Property ListCollectionMonitoring() As List(Of CollectionMonitoring)
        Get
            Return _ListCollectionMonitoring
        End Get
        Set(ByVal value As List(Of CollectionMonitoring))
            _ListCollectionMonitoring = value
        End Set
    End Property

#End Region

#Region "AllocationDate"
    Private _AllocationDate As Date
    Public Property AllocationDate() As Date
        Get
            Return _AllocationDate
        End Get
        Set(ByVal value As Date)
            _AllocationDate = value
        End Set
    End Property

#End Region

#Region "CollectionNo"
    Private _CollectionNo As Long
    Public Property CollectionNo() As Long
        Get
            Return _CollectionNo
        End Get
        Set(ByVal value As Long)
            _CollectionNo = value
        End Set
    End Property

#End Region

#Region "DMCM Number"
    Private _DMCMNumber As Long
    Public Property DMCMNumber() As Long
        Get
            Return _DMCMNumber
        End Get
        Set(ByVal value As Long)
            _DMCMNumber = value
        End Set
    End Property
#End Region

#Region "DicWESMBillDueDate"
    Private _DicWESMBillDueDate As Dictionary(Of Long, Date)
    Public Property DicWESMBillDueDate() As Dictionary(Of Long, Date)
        Get
            Return _DicWESMBillDueDate
        End Get
        Set(ByVal value As Dictionary(Of Long, Date))
            _DicWESMBillDueDate = value
        End Set
    End Property

#End Region

#Region "DrawdownDate"
    Private _DrawdownDate As Date
    Public ReadOnly Property DrawdownDate() As Date
        Get
            _DrawdownDate = Me.AllocationDate.AddDays(-1)
            Return _DrawdownDate
        End Get
    End Property

#End Region


#Region "GenerateCollectionAllocation"

    Public Function GenerateCollectionAllocation() As FuncAutoCollectionAllocationResult
        Dim dicWESMBillSummary As New Dictionary(Of Long, WESMBillSummary)
        Dim dicParticipantHeld As New Dictionary(Of Long, AMParticipants)
        Dim dicParticipantExcess As New Dictionary(Of Long, AMParticipants)
        Dim result As New FuncAutoCollectionAllocationResult

        Dim InterestRate As Decimal
        For Each item In Me.ListParticipants
            Dim listCollectionAllocation As New List(Of CollectionAllocation)
            Dim participant = item

            Dim itemCollections = (From x In Me.ListCollections _
                                   Where x.IDNumber = participant.IDNumber _
                                   Select x Order By x.CollectionDate Ascending, x.CollectionNumber Ascending).ToList()

            'Dim totalPrudential = (From x In Me.ListPrudentials _
            '                        Where x.IDNumber = participant.IDNumber _
            '                        Select x.PrudentialAmount).Sum()

            'Dim tmpPrudential = CType(Me.BFactory.CloneObject(totalPrudential), Decimal)

            If itemCollections.Count > 0 Then
                'Check and transfer the held collection
                Dim cntHeld = (From x In Me.ListCollectionMonitoring _
                               Where x.IDNumber.IDNumber = participant.IDNumber _
                               Select x).Count

                If cntHeld > 0 Then
                    Dim itemColMon = (From x In Me.ListCollectionMonitoring _
                                      Where x.IDNumber.IDNumber = participant.IDNumber _
                                      Select x).First()

                    result.ListCollectionMonitoring.Add(itemColMon)
                End If

                For Each itemCol In itemCollections
                    Dim heldAmount As Decimal = 0

                    'Get and check for Interest Rate
                    If Not Me._DicInterestRate.ContainsKey(itemCol.CollectionDate) Then
                        Throw New ApplicationException("No interest rate for " & itemCol.CollectionDate.ToString("MM/dd/yyyy"))
                        Exit Function
                    Else
                        InterestRate = Me._DicInterestRate(itemCol.CollectionDate)
                    End If

                    'Get the held collection and update the trans type
                    Dim cnt = (From x In result.ListCollectionMonitoring _
                               Where x.IDNumber.IDNumber = participant.IDNumber _
                               And x.TransType = EnumCollectionMonitoringType.TransferToHeldCollection And x.CollectionNoTag = 0 _
                               Select x).Count

                    If cnt > 0 Then
                        Dim itemColMon = (From x In result.ListCollectionMonitoring _
                                          Where x.IDNumber.IDNumber = participant.IDNumber _
                                          And x.TransType = EnumCollectionMonitoringType.TransferToHeldCollection And x.CollectionNoTag = 0 _
                                          Select x).First()

                        heldAmount = itemColMon.Amount
                        itemColMon.CollectionNoTag = itemCol.CollectionNumber
                    End If

                    'Get the amount for collection allocation
                    Dim CollectedAmount As Decimal = CType(BFactory.CloneObject(itemCol.CollectedAmount), Decimal) + heldAmount

                    'Update first held amount into zero
                    Dim oldHeldAmount As Decimal = 0

                    'Allocate the collection
                    'Me.AllocateCollection(participant, listCollectionAllocation, dicWESMBillSummary, _
                    '                      InterestRate, itemCol, CollectedAmount, tmpPrudential, oldHeldAmount)

                    'Allocate the collection
                    Me.AllocateCollection(participant, listCollectionAllocation, dicWESMBillSummary, _
                                          InterestRate, itemCol, CollectedAmount, 0, oldHeldAmount)

                    'Add the held collection in collection monitoring
                    If oldHeldAmount > 0 Then
                        result.ListCollectionMonitoring.Add(New CollectionMonitoring(0, "", itemCol.CollectionNumber, _
                                                            Me.AllocationDate, itemCol.AllocationType, participant, _
                                                            itemCol.ORNo, oldHeldAmount, EnumCollectionMonitoringType.TransferToHeldCollection, _
                                                            EnumStatus.Active))
                    End If

                    'For excess collection
                    If CollectedAmount > 0 Then
                        result.ListCollectionMonitoring.Add(New CollectionMonitoring(0, "", itemCol.CollectionNumber, _
                                                            Me.AllocationDate, itemCol.AllocationType, participant, _
                                                            itemCol.ORNo, CollectedAmount, EnumCollectionMonitoringType.TransferToExcessCollection, _
                                                            EnumStatus.Active))
                    End If

                    'Update the held
                    itemCol.CollectedHeld = heldAmount
                    itemCol.Status = EnumCollectionStatus.PreAllocated
                    itemCol.AllocationDate = Me.AllocationDate
                    itemCol.ListOfCollectionAllocation = listCollectionAllocation
                    result.ListCollections.Add(itemCol)
                Next
            Else
                'If tmpPrudential > 0 Then

                '    'Get and check for Interest Rate
                '    If Not Me._DicInterestRate.ContainsKey(Me.DrawdownDate) Then
                '        Throw New ApplicationException("No interest rate for " & Me.DrawdownDate.ToString("MM/dd/yyyy"))
                '        Exit Function
                '    Else
                '        InterestRate = Me._DicInterestRate(Me.DrawdownDate)
                '    End If

                '    Me.AllocateDrawdown(participant, listCollectionAllocation, dicWESMBillSummary, _
                '                        InterestRate, tmpPrudential)
                'End If
            End If

            'Check if there is deduction in prudential
            'If totalPrudential <> tmpPrudential Then
            '    Me.CollectionNo += 1
            '    Me.DMCMNumber += 1

            '    Dim itemCol As New Collection
            '    With itemCol
            '        .CollectionNumber = Me.CollectionNo
            '        .IDNumber = participant.IDNumber
            '        .CollectionDate = Me.DrawdownDate
            '        .ORNo = 0
            '        .CollectedAmount = (totalPrudential - tmpPrudential) * -1D
            '        .AllocationType = EnumAllocationType.Automatic
            '        .CollectionCategory = EnumCollectionCategory.Drawdown
            '        .Status = EnumCollectionStatus.NotAllocated
            '        .AllocationDate = Me.AllocationDate
            '        .DMCMNumber = Me.DMCMNumber
            '    End With

            '    Dim itemsColAlloc = (From x In listCollectionAllocation _
            '                         Where x.CollectionNumber = 0 _
            '                         Select x).ToList()

            '    For Each itemColAlloc In itemsColAlloc
            '        itemColAlloc.CollectionNumber = Me.CollectionNo
            '    Next

            '    With result
            '        'For drawdown in collection monitoring
            '        .ListCollectionMonitoring.Add(New CollectionMonitoring(0, "", Me.CollectionNo, _
            '                                      Me.AllocationDate, itemCol.AllocationType, participant, _
            '                                      0, totalPrudential - tmpPrudential, EnumCollectionMonitoringType.TransferToPRDrawdown, _
            '                                      EnumStatus.Active))
            '        .ListCollections.Add(itemCol)
            '        .ListPrudentials.Add(New Prudential(participant.IDNumber, (totalPrudential - tmpPrudential) * -1D, 0))
            '        .ListPrudentialsHistory.Add(New PrudentialHistory(0, participant, totalPrudential - tmpPrudential, _
            '                                                          EnumPrudentialTransType.Drawdown, Me.DrawdownDate, "", Me.DMCMNumber))
            '    End With
            'End If

            result.ListCollectionAllocation.AddRange(listCollectionAllocation)
        Next

        'Add the WESM Bill Summaries
        For Each item In dicWESMBillSummary
            result.ListWESMBillSummaries.Add(item.Value)
        Next
        result.ListWESMBillSummaries.TrimExcess()

        'Update the list of collection allocations of an collection
        For Each item In result.ListCollections
            Dim selectedItem = item

            Dim listAlloc = (From x In result.ListCollectionAllocation _
                             Where x.CollectionNumber = selectedItem.CollectionNumber _
                             Select x).ToList()

            item.ListOfCollectionAllocation = listAlloc
        Next

        Return result
    End Function

#End Region

#Region "AllocateCollection"

    Private Function AllocateCollection(ByVal participant As AMParticipants, _
                                        ByRef resultCollectionAllocations As List(Of CollectionAllocation), _
                                        ByRef dicWESMBillSummary As Dictionary(Of Long, WESMBillSummary), _
                                        ByVal InterestRate As Decimal, ByVal itemCollection As Collection, _
                                        ByRef Amount As Decimal, ByRef PrudentialAmount As Decimal, _
                                        ByRef HeldAmount As Decimal) As Boolean
        Dim IsContinue As Boolean

        'Allocation of Default Interest on MF and Vat on MF
        If Amount > 0 Then
            Dim listMFandVat = (From x In Me.ListWESMBillSummaries _
                                Where (x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV) _
                                       And x.IDNumber.IDNumber = participant.IDNumber And x.EndingBalance < 0 _
                                Select x).ToList()

            IsContinue = Me.AllocateCollection(participant, resultCollectionAllocations, _
                                               dicWESMBillSummary, listMFandVat, itemCollection, InterestRate, _
                                               AllocationCategory.DefaultInterestOnMFandMFV, Amount, PrudentialAmount, HeldAmount)
        End If

        'Allocation of Market Fees
        If Amount > 0 And IsContinue = True Then
            Dim listMF = (From x In Me.ListWESMBillSummaries _
                          Where (x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV) _
                                 And x.IDNumber.IDNumber = participant.IDNumber And x.EndingBalance < 0 _
                          Select x).ToList()

            IsContinue = Me.AllocateCollection(participant, resultCollectionAllocations, _
                                               dicWESMBillSummary, listMF, itemCollection, InterestRate, _
                                               AllocationCategory.MarketFees, Amount, PrudentialAmount, HeldAmount)
        End If

        'Allocation of Default Interest on Energy
        If (Amount > 0 And IsContinue = True) Or PrudentialAmount > 0 Then
            Dim listEnergy = (From x In Me.ListWESMBillSummaries _
                              Where (x.ChargeType = EnumChargeType.E _
                                    And x.IDNumber.IDNumber = participant.IDNumber And x.EndingBalance < 0) _
                                Or (x.ChargeType = EnumChargeType.E _
                                    And x.IDNumber.IDNumber = participant.IDNumber And x.EndingBalance = 0 And x.EnergyWithholdStatus = EnumEnergyWithholdStatus.UnpaidEWT) _
                              Select x).ToList()

            IsContinue = Me.AllocateCollection(participant, resultCollectionAllocations, _
                                               dicWESMBillSummary, listEnergy, itemCollection, InterestRate, _
                                               AllocationCategory.DefaultInterestOnEnergy, Amount, PrudentialAmount, HeldAmount)

            'Allocation of Energy
            'It must check first if the total collection and prudential can pay the outstanding balance
            If (Amount > 0 And IsContinue = True) Or PrudentialAmount > 0 Then

                IsContinue = Me.AllocateCollection(participant, resultCollectionAllocations, _
                                                   dicWESMBillSummary, listEnergy, itemCollection, InterestRate, _
                                                   AllocationCategory.Energy, Amount, PrudentialAmount, HeldAmount)
            End If
        End If

        
        'Allocation of Vat on Energy
        If Amount > 0 And IsContinue = True Then
            Dim listVatOnEnergy = (From x In Me.ListWESMBillSummaries _
                                   Where x.ChargeType = EnumChargeType.EV _
                                        And x.IDNumber.IDNumber = participant.IDNumber And x.EndingBalance < 0 _
                                   Select x Order By x.DueDate).ToList()

            IsContinue = Me.AllocateCollection(participant, resultCollectionAllocations, _
                                               dicWESMBillSummary, listVatOnEnergy, itemCollection, InterestRate, _
                                               AllocationCategory.VatOnEnergy, Amount, PrudentialAmount, HeldAmount)
        End If

    End Function

    Private Function AllocateCollection(ByVal participant As AMParticipants, _
                                        ByRef resultCollectionAllocations As List(Of CollectionAllocation), _
                                        ByRef dicWESMBillSummary As Dictionary(Of Long, WESMBillSummary), _
                                        ByVal listSummaries As List(Of WESMBillSummary), ByVal itemCollection As Collection, _
                                        ByVal InterestRate As Decimal, ByVal category As AllocationCategory, _
                                        ByRef Amount As Decimal, ByRef PrudentialAmount As Decimal, _
                                        ByRef HeldAmount As Decimal) As Boolean
        Select Case category

            Case AllocationCategory.DefaultInterestOnMFandMFV
                AllocateCollection = True

                'Get the distinct due date and billing period
                'Dim itemDistincts = From x In listSummaries _
                'Select x.DueDate, x.BillPeriod Distinct _
                'Order By DueDate, BillPeriod

                'Get the distinct due date and billing period ' Changed by Vloody 12/21/2014
                Dim itemDistincts = From x In listSummaries Group x By _
                     x.NewDueDate, x.BillPeriod _
                    Into _
                    Group Order By NewDueDate, BillPeriod Ascending _
                    Select New With {.DueDate = NewDueDate, .BillPeriod = BillPeriod}

                For Each itemDistinct In itemDistincts
                    Dim newDueDateValue = itemDistinct.DueDate
                    Dim billPeriodvalue = itemDistinct.BillPeriod

                    Dim items = From x In listSummaries _
                                Where x.DueDate = newDueDateValue And x.BillPeriod = billPeriodvalue _
                                Select x

                    'Check if the collection can pay the default interest with the same 
                    'Due Date and Billing period
                    Dim tempTotal As Decimal = 0
                    For Each item In items
                        Dim taxAmount As Decimal = 0

                        'Add the default interest of the base amount
                        tempTotal += Math.Round(BFactory.ComputeDefaultInterest(item.DueDate, item.NewDueDate, itemCollection.CollectionDate, _
                                                                     Math.Abs(item.EndingBalance), InterestRate), 2)

                        'Compute the Withholding Tax
                        If item.ChargeType = EnumChargeType.MF Then
                            taxAmount = Math.Round(item.EndingBalance * participant.MarketFeesWHTax, 2)
                        Else
                            taxAmount = Math.Round(item.EndingBalance * ((participant.MarketFeesWHVAT * 100) / (AMModule.VatValue * 100)), 2)
                        End If

                        'Deduct also the default interest in Witholding Tax
                        If taxAmount <> 0 Then
                            tempTotal += Math.Round(BFactory.ComputeDefaultInterest(item.DueDate, item.NewDueDate, itemCollection.CollectionDate, _
                                                                         taxAmount, InterestRate), 2)

                        End If

                        'Compare the cash if it can fully paid the outstanding balance and transfer into held collection if not.
                        If tempTotal > Amount Then
                            HeldAmount = Amount
                            Amount = 0
                            AllocateCollection = False
                            Exit Function
                        End If
                    Next

                    For Each item In items
                        Dim taxAmount As Decimal = 0
                        Dim defaultValue As Decimal = 0
                        Dim DIWTaxAmount As Decimal = 0

                        'Compute the default interest
                        defaultValue = Math.Round(BFactory.ComputeDefaultInterest(item.DueDate, item.NewDueDate, itemCollection.CollectionDate, _
                                                                       Math.Abs(item.EndingBalance), InterestRate), 2)
                        If defaultValue <> 0 Then
                            Amount = Amount - defaultValue

                            Dim itemAllocation As New CollectionAllocation
                            With itemAllocation
                                .WESMBillSummaryNo = item
                                .BillingPeriod = item.BillPeriod
                                .CollectionNumber = itemCollection.CollectionNumber
                                .Amount = defaultValue
                                .NewDueDate = itemCollection.CollectionDate
                                .DueDate = CType(BFactory.CloneObject(item.NewDueDate), Date)
                                .EndingBalance = Math.Abs(CType(BFactory.CloneObject(item.EndingBalance), Decimal))
                                .AllocationDate = Me.AllocationDate
                                .ReferenceNumber = CType(BFactory.CloneObject(item.INVDMCMNo), String)
                                .ReferenceType = CType(BFactory.CloneObject(item.SummaryType), EnumSummaryType)

                                Select Case item.ChargeType
                                    Case EnumChargeType.MF
                                        .CollectionType = EnumCollectionType.DefaultInterestOnMF
                                    Case EnumChargeType.MFV
                                        .CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF
                                End Select
                            End With

                            'Add in the Dictionary
                            If Not Me.DicWESMBillDueDate.ContainsKey(item.WESMBillSummaryNo) Then
                                Me.DicWESMBillDueDate.Add(item.WESMBillSummaryNo, CType(BFactory.CloneObject(item.NewDueDate), Date))
                            End If

                            If dicWESMBillSummary.ContainsKey(item.WESMBillSummaryNo) Then
                                dicWESMBillSummary(item.WESMBillSummaryNo) = item
                            Else
                                dicWESMBillSummary.Add(item.WESMBillSummaryNo, item)
                            End If
                            resultCollectionAllocations.Add(itemAllocation)

                            'Default Interest on Withholding Tax
                            If participant.MarketFeesWHVAT <> 0 Or participant.MarketFeesWHTax <> 0 Then
                                Dim endingBalance As Decimal = 0D
                                Dim DICollectionType As EnumCollectionType

                                If item.ChargeType = EnumChargeType.MF Then
                                    taxAmount = Math.Round(item.EndingBalance * participant.MarketFeesWHTax, 2)

                                    'For default Interest of WitholdingTaxOnMF
                                    DICollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest
                                    DIWTaxAmount = Math.Round(BFactory.ComputeDefaultInterest(item.DueDate, item.NewDueDate, itemCollection.CollectionDate, _
                                                                                   taxAmount, InterestRate), 2)
                                Else
                                    taxAmount = Math.Round(item.EndingBalance * ((participant.MarketFeesWHVAT * 100) / (AMModule.VatValue * 100)), 2)

                                    'For default Interest of WitholdingVatOnVatOnMF
                                    DICollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest
                                    DIWTaxAmount = Math.Round(BFactory.ComputeDefaultInterest(item.DueDate, item.NewDueDate, itemCollection.CollectionDate, _
                                                                                   taxAmount, InterestRate), 2)
                                End If

                                'For Default Interest of Withholding
                                If DIWTaxAmount <> 0 Then
                                    'Add the WTAX and Default Interest in WTAX in Base Amount
                                    Amount -= DIWTaxAmount

                                    Dim itemAllocDI As New CollectionAllocation

                                    With itemAllocDI
                                        .WESMBillSummaryNo = item
                                        .BillingPeriod = item.BillPeriod
                                        .CollectionNumber = itemCollection.CollectionNumber
                                        .Amount = DIWTaxAmount
                                        .NewDueDate = itemCollection.CollectionDate
                                        .DueDate = CType(BFactory.CloneObject(item.NewDueDate), Date)
                                        .EndingBalance = defaultValue
                                        .AllocationDate = Me.AllocationDate
                                        .CollectionType = DICollectionType
                                        .ReferenceNumber = CType(BFactory.CloneObject(item.INVDMCMNo), String)
                                        .ReferenceType = CType(BFactory.CloneObject(item.SummaryType), EnumSummaryType)
                                    End With
                                    resultCollectionAllocations.Add(itemAllocDI)
                                End If
                            End If

                            'Update the new duedate
                            If itemCollection.CollectionDate > item.NewDueDate Then
                                item.NewDueDate = itemCollection.CollectionDate
                            End If
                        End If
                    Next
                Next

            Case AllocationCategory.MarketFees, AllocationCategory.VatOnMarketFees
                AllocateCollection = True


                'Get the distinct InvoiceNo and DueDate
                'Deduct first the oldest due date and smaller amount
                Dim itemDistincts = From x In listSummaries Group x By _
                                    x.INVDMCMNo, x.SummaryType, x.DueDate, x.NewDueDate _
                                    Into _
                                    EndingBalance = Sum(x.EndingBalance) Order By EndingBalance Descending _
                                    Select New With {.INVDMCMNo = INVDMCMNo, .SummaryType = SummaryType, _
                                                     .DueDate = DueDate, .NewDueDate = NewDueDate, _
                                                     .EndingBalance = EndingBalance}

                Dim MFValue As Decimal = 0, MFVValue As Decimal = 0
                Dim WithholdTaxValue As Decimal = 0, WithholdVatValue As Decimal = 0
                Dim TotalValue As Decimal = 0, AmountToAllocate As Decimal = 0

                For Each itemDistinct In itemDistincts
                    Dim selectedItem = itemDistinct

                    Dim items = From x In listSummaries _
                                Where x.INVDMCMNo = selectedItem.INVDMCMNo And x.SummaryType = selectedItem.SummaryType _
                                Select x

                    Dim TotalOutstanding As Decimal = 0
                    Dim TotalTaxAmount As Decimal = 0D

                    'Compute the total recievable
                    For Each item In items
                        If item.ChargeType = EnumChargeType.MF Then
                            MFValue = Math.Abs(item.EndingBalance)

                            If participant.MarketFeesWHTax <> 0 Then
                                WithholdTaxValue = Math.Round(item.EndingBalance * participant.MarketFeesWHTax, 2)
                            End If
                        Else
                            MFVValue = Math.Abs(item.EndingBalance)

                            If participant.MarketFeesWHVAT <> 0 Then
                                WithholdVatValue = Math.Round(item.EndingBalance * (participant.MarketFeesWHVAT / AMModule.VatValue), 2)
                            End If
                        End If
                    Next

                    'Compute the total amount
                    TotalValue = MFValue + MFVValue + WithholdTaxValue + WithholdVatValue
                    AmountToAllocate = Amount

                    'Check if partial or fully paid
                    Dim IsPartial As Boolean = False
                    If TotalValue > Amount Then
                        IsPartial = True
                        Amount = 0
                    Else
                        Amount -= TotalValue
                    End If

                    'Allocate Partial
                    If IsPartial Then
                        For Each item In items
                            If item.ChargeType = EnumChargeType.MF Then
                                MFValue = Math.Round((Math.Abs(item.EndingBalance) / TotalValue) * AmountToAllocate, 2)

                                If participant.MarketFeesWHTax <> 0 Then
                                    WithholdTaxValue = Math.Round((item.EndingBalance * participant.MarketFeesWHTax) / _
                                                                    TotalValue * AmountToAllocate, 2)
                                End If
                            Else
                                MFVValue = Math.Round((Math.Abs(item.EndingBalance) / TotalValue) * AmountToAllocate, 2)

                                If participant.MarketFeesWHVAT <> 0 Then
                                    WithholdVatValue = Math.Round((item.EndingBalance * (participant.MarketFeesWHVAT / _
                                                                    AMModule.VatValue) / TotalValue) * AmountToAllocate, 2)
                                End If
                            End If
                        Next

                        'If there is .01 difference, adjust market fees
                        Dim allocatedAmount = MFValue + MFVValue + WithholdTaxValue + WithholdVatValue
                        If allocatedAmount <> AmountToAllocate Then
                            MFValue = MFValue - (allocatedAmount - AmountToAllocate)
                        End If
                    End If

                    For Each item In items
                        Dim DueDate As Date
                        If Me.DicWESMBillDueDate.ContainsKey(item.WESMBillSummaryNo) Then
                            DueDate = Me.DicWESMBillDueDate(item.WESMBillSummaryNo)
                        Else
                            DueDate = CType(BFactory.CloneObject(item.NewDueDate), Date)
                        End If

                        'For Withholding Tax
                        If WithholdTaxValue <> 0 And item.ChargeType = EnumChargeType.MF Then
                            Dim itemWithhold As New CollectionAllocation
                            With itemWithhold
                                .WESMBillSummaryNo = item
                                .BillingPeriod = item.BillPeriod
                                .CollectionNumber = itemCollection.CollectionNumber
                                .NewDueDate = itemCollection.CollectionDate
                                .DueDate = DueDate
                                .EndingBalance = Math.Abs(CType(BFactory.CloneObject(item.EndingBalance), Decimal))
                                .AllocationDate = Me.AllocationDate
                                .ReferenceNumber = CType(BFactory.CloneObject(item.INVDMCMNo), String)
                                .ReferenceType = CType(BFactory.CloneObject(item.SummaryType), EnumSummaryType)
                                .CollectionType = EnumCollectionType.WithholdingTaxOnMF
                                .Amount = WithholdTaxValue

                                resultCollectionAllocations.Add(itemWithhold)
                            End With
                        End If

                        'For Withholding VAT
                        If WithholdVatValue <> 0 And item.ChargeType = EnumChargeType.MFV Then
                            Dim itemWithhold As New CollectionAllocation
                            With itemWithhold
                                .WESMBillSummaryNo = item
                                .BillingPeriod = item.BillPeriod
                                .CollectionNumber = itemCollection.CollectionNumber
                                .NewDueDate = itemCollection.CollectionDate
                                .DueDate = DueDate
                                .EndingBalance = Math.Abs(CType(BFactory.CloneObject(item.EndingBalance), Decimal))
                                .AllocationDate = Me.AllocationDate
                                .ReferenceNumber = CType(BFactory.CloneObject(item.INVDMCMNo), String)
                                .ReferenceType = CType(BFactory.CloneObject(item.SummaryType), EnumSummaryType)
                                .CollectionType = EnumCollectionType.WithholdingVatonMF
                                .Amount = WithholdVatValue

                                resultCollectionAllocations.Add(itemWithhold)
                            End With
                        End If

                        'For Base Amount
                        Dim itemAllocation As New CollectionAllocation
                        With itemAllocation
                            .WESMBillSummaryNo = item
                            .BillingPeriod = item.BillPeriod
                            .CollectionNumber = itemCollection.CollectionNumber
                            .NewDueDate = itemCollection.CollectionDate
                            .DueDate = DueDate
                            .EndingBalance = Math.Abs(CType(BFactory.CloneObject(item.EndingBalance), Decimal))
                            .AllocationDate = Me.AllocationDate
                            .ReferenceNumber = CType(BFactory.CloneObject(item.INVDMCMNo), String)
                            .ReferenceType = CType(BFactory.CloneObject(item.SummaryType), EnumSummaryType)

                            Select Case item.ChargeType
                                Case EnumChargeType.MF
                                    .CollectionType = EnumCollectionType.MarketFees
                                    .Amount = MFValue

                                    'Update the ending balance
                                    item.EndingBalance = item.EndingBalance + MFValue
                                Case EnumChargeType.MFV
                                    .CollectionType = EnumCollectionType.VatOnMarketFees
                                    .Amount = MFVValue

                                    'Update the ending balance
                                    item.EndingBalance = item.EndingBalance + MFVValue
                            End Select
                        End With
                        resultCollectionAllocations.Add(itemAllocation)

                        If dicWESMBillSummary.ContainsKey(item.WESMBillSummaryNo) Then
                            dicWESMBillSummary(item.WESMBillSummaryNo) = item
                        Else
                            dicWESMBillSummary.Add(item.WESMBillSummaryNo, item)
                        End If
                    Next
                Next

            Case AllocationCategory.DefaultInterestOnEnergy
                AllocateCollection = True

                'Get the distinct due date and billing period
                'Dim itemDistincts = From x In listSummaries _
                '                    Select x.NewDueDate, x.BillPeriod Distinct _
                '                    Order By NewDueDate, BillPeriod

                'Get the distinct due date and billing period ' Changed by Vloody 12/21/2014
                Dim itemDistincts = From x In listSummaries Group x By _
                     x.NewDueDate, x.BillPeriod _
                    Into _
                    Group Order By NewDueDate, BillPeriod Ascending _
                    Select New With {.NewDueDate = NewDueDate, .BillPeriod = BillPeriod}

                For Each itemDistinct In itemDistincts
                    Dim newDueDateValue = itemDistinct.NewDueDate
                    Dim billPeriodvalue = itemDistinct.BillPeriod

                    Dim items = From x In listSummaries _
                                Where x.NewDueDate = newDueDateValue And x.BillPeriod = billPeriodvalue _
                                Select x

                    'Check if the collection can pay the default interest with the same 
                    'Due Date and Billing period
                    Dim tempTotal As Decimal = 0
                    For Each item In items
                        tempTotal += Math.Round(BFactory.ComputeDefaultInterest(item.DueDate, item.NewDueDate, itemCollection.CollectionDate, _
                                                                     Math.Abs(item.EndingBalance - item.EnergyWithhold), InterestRate), 2)
                        If tempTotal > Amount + PrudentialAmount Then
                            HeldAmount = Amount
                            Amount = 0
                            AllocateCollection = False
                            Exit Function
                        End If
                    Next

                    For Each item In items
                        Dim defaultValue As Decimal = Math.Round(BFactory.ComputeDefaultInterest(item.DueDate, item.NewDueDate, itemCollection.CollectionDate, _
                                                                                      Math.Abs(item.EndingBalance - item.EnergyWithhold), InterestRate), 2)
                        If defaultValue <> 0 Then
                            If Amount >= defaultValue Then
                                Amount = Amount - defaultValue

                                Dim itemAllocation As New CollectionAllocation
                                With itemAllocation
                                    .WESMBillSummaryNo = item
                                    .BillingPeriod = item.BillPeriod
                                    .CollectionNumber = itemCollection.CollectionNumber
                                    .Amount = defaultValue
                                    .NewDueDate = itemCollection.CollectionDate
                                    .DueDate = CType(BFactory.CloneObject(item.NewDueDate), Date)
                                    .EndingBalance = Math.Abs(CType(BFactory.CloneObject(item.EndingBalance), Decimal))
                                    .AllocationDate = Me.AllocationDate
                                    .CollectionType = EnumCollectionType.DefaultInterestOnEnergy
                                    .ReferenceNumber = CType(BFactory.CloneObject(item.INVDMCMNo), String)
                                    .ReferenceType = CType(BFactory.CloneObject(item.SummaryType), EnumSummaryType)
                                End With
                                resultCollectionAllocations.Add(itemAllocation)

                            Else
                                PrudentialAmount = PrudentialAmount + Amount + defaultValue

                                Dim itemAllocation As New CollectionAllocation
                                With itemAllocation
                                    .WESMBillSummaryNo = item
                                    .BillingPeriod = item.BillPeriod
                                    .CollectionNumber = 0
                                    .Amount = defaultValue + Amount
                                    .NewDueDate = itemCollection.CollectionDate
                                    .DueDate = CType(BFactory.CloneObject(item.NewDueDate), Date)
                                    .EndingBalance = CType(BFactory.CloneObject(item.EndingBalance), Decimal)
                                    .AllocationDate = Me.AllocationDate
                                    .CollectionType = EnumCollectionType.DefaultInterestOnEnergy
                                    .ReferenceNumber = CType(BFactory.CloneObject(item.INVDMCMNo), String)
                                    .ReferenceType = CType(BFactory.CloneObject(item.SummaryType), EnumSummaryType)
                                End With
                                resultCollectionAllocations.Add(itemAllocation)

                                If Amount > 0 Then
                                    itemAllocation = New CollectionAllocation
                                    With itemAllocation
                                        .WESMBillSummaryNo = item
                                        .BillingPeriod = item.BillPeriod
                                        .CollectionNumber = itemCollection.CollectionNumber
                                        .Amount = Amount * -1D
                                        .NewDueDate = itemCollection.CollectionDate
                                        .DueDate = CType(BFactory.CloneObject(item.NewDueDate), Date)
                                        .EndingBalance = CType(BFactory.CloneObject(item.EndingBalance), Decimal)
                                        .AllocationDate = Me.AllocationDate
                                        .CollectionType = EnumCollectionType.DefaultInterestOnEnergy
                                        .ReferenceNumber = CType(BFactory.CloneObject(item.INVDMCMNo), String)
                                        .ReferenceType = CType(BFactory.CloneObject(item.SummaryType), EnumSummaryType)
                                    End With

                                    resultCollectionAllocations.Add(itemAllocation)
                                End If
                                Amount = 0
                            End If

                            'Add in the Dictionary
                            If Not Me.DicWESMBillDueDate.ContainsKey(item.WESMBillSummaryNo) Then
                                Me.DicWESMBillDueDate.Add(item.WESMBillSummaryNo, CType(BFactory.CloneObject(item.NewDueDate), Date))
                            End If

                            'Update the new duedate
                            If itemCollection.CollectionDate > item.NewDueDate Then
                                item.NewDueDate = itemCollection.CollectionDate
                            End If

                            If dicWESMBillSummary.ContainsKey(item.WESMBillSummaryNo) Then
                                dicWESMBillSummary(item.WESMBillSummaryNo) = item
                            Else
                                dicWESMBillSummary.Add(item.WESMBillSummaryNo, item)
                            End If
                        End If
                    Next
                Next

            Case AllocationCategory.Energy
                AllocateCollection = True

                Dim items = From x In listSummaries _
                            Select x Order By x.NewDueDate Ascending, x.BillPeriod Ascending, x.EndingBalance Ascending

                For Each item In items                    
                    If Amount = 0 And PrudentialAmount = 0 Then
                        Exit Function
                    End If

                    Dim DueDate As Date
                    If Me.DicWESMBillDueDate.ContainsKey(item.WESMBillSummaryNo) Then
                        DueDate = Me.DicWESMBillDueDate(item.WESMBillSummaryNo)
                    Else
                        DueDate = CType(BFactory.CloneObject(item.NewDueDate), Date)
                    End If

                    If item.EnergyWithholdStatus = EnumEnergyWithholdStatus.UnpaidEWT Then
                        If Amount + PrudentialAmount >= Math.Abs(item.EnergyWithhold) Then
                            Dim itemAllocation As New CollectionAllocation
                            Dim EnergyWithholdAmount As Decimal = Math.Abs(item.EnergyWithhold)
                            With itemAllocation
                                .WESMBillSummaryNo = item
                                .BillingPeriod = item.BillPeriod
                                .CollectionNumber = itemCollection.CollectionNumber
                                .Amount = EnergyWithholdAmount
                                .EndingBalance = Math.Abs(CType(BFactory.CloneObject(item.EndingBalance), Decimal))
                                .AllocationDate = Me.AllocationDate
                                .CollectionType = EnumCollectionType.WithholdingTaxonEnergy
                                .NewDueDate = CType(BFactory.CloneObject(item.NewDueDate), Date)
                                .DueDate = DueDate
                                .ReferenceNumber = CType(BFactory.CloneObject(item.INVDMCMNo), String)
                                .ReferenceType = CType(BFactory.CloneObject(item.SummaryType), EnumSummaryType)
                            End With
                            resultCollectionAllocations.Add(itemAllocation)

                            Amount = Amount - EnergyWithholdAmount

                            'Update the ending balance
                            item.EnergyWithholdStatus = EnumEnergyWithholdStatus.PaidEWT
                        End If
                    End If

                    ' Save the ending balance in temporary variable because the ending balance
                    ' will not be deducted upon displaying in collection allocation
                    Dim tempEndingBalance = 0D
                    If item.EnergyWithholdStatus = EnumEnergyWithholdStatus.NotApplicable Then
                        tempEndingBalance = Math.Abs(CType(BFactory.CloneObject(item.EndingBalance), Decimal) - item.EnergyWithhold)
                    Else
                        tempEndingBalance = Math.Abs(CType(BFactory.CloneObject(item.EndingBalance), Decimal))
                    End If

                    If Amount + PrudentialAmount >= tempEndingBalance Then
                        If Amount >= tempEndingBalance Then
                            Dim itemAllocation As New CollectionAllocation
                            With itemAllocation
                                .WESMBillSummaryNo = item
                                .BillingPeriod = item.BillPeriod
                                .CollectionNumber = itemCollection.CollectionNumber
                                .Amount = tempEndingBalance
                                .EndingBalance = Math.Abs(CType(BFactory.CloneObject(item.EndingBalance), Decimal))
                                .AllocationDate = Me.AllocationDate
                                .CollectionType = EnumCollectionType.Energy
                                .NewDueDate = CType(BFactory.CloneObject(item.NewDueDate), Date)
                                .DueDate = DueDate
                                .ReferenceNumber = CType(BFactory.CloneObject(item.INVDMCMNo), String)
                                .ReferenceType = CType(BFactory.CloneObject(item.SummaryType), EnumSummaryType)
                            End With
                            resultCollectionAllocations.Add(itemAllocation)

                            Amount = Amount - tempEndingBalance

                            'Update the ending balance
                            item.EndingBalance = item.EnergyWithhold
                        Else
                            Dim itemAllocation As CollectionAllocation

                            If Amount > 0 Then
                                itemAllocation = New CollectionAllocation
                                With itemAllocation
                                    .WESMBillSummaryNo = item
                                    .BillingPeriod = item.BillPeriod
                                    .CollectionNumber = itemCollection.CollectionNumber
                                    .Amount = Amount
                                    .EndingBalance = tempEndingBalance
                                    .AllocationDate = Me.AllocationDate
                                    .CollectionType = EnumCollectionType.Energy
                                    .NewDueDate = CType(BFactory.CloneObject(item.NewDueDate), Date)
                                    .DueDate = DueDate
                                    .ReferenceNumber = CType(BFactory.CloneObject(item.INVDMCMNo), String)
                                    .ReferenceType = CType(BFactory.CloneObject(item.SummaryType), EnumSummaryType)
                                End With

                                item.EndingBalance = item.EndingBalance + Amount
                                resultCollectionAllocations.Add(itemAllocation)
                            End If

                            itemAllocation = New CollectionAllocation
                            With itemAllocation
                                .WESMBillSummaryNo = item
                                .BillingPeriod = item.BillPeriod
                                .CollectionNumber = 0
                                .Amount = item.EndingBalance
                                .EndingBalance = Math.Abs(CType(BFactory.CloneObject(item.EndingBalance), Decimal))
                                .AllocationDate = Me.AllocationDate
                                .CollectionType = EnumCollectionType.Energy
                                .NewDueDate = CType(BFactory.CloneObject(item.NewDueDate), Date)
                                .DueDate = DueDate
                                .ReferenceNumber = CType(BFactory.CloneObject(item.INVDMCMNo), String)
                                .ReferenceType = CType(BFactory.CloneObject(item.SummaryType), EnumSummaryType)
                            End With
                            resultCollectionAllocations.Add(itemAllocation)

                            PrudentialAmount = PrudentialAmount + item.EndingBalance
                            item.EndingBalance = 0
                            Amount = 0
                        End If
                    Else
                        Dim itemAllocation As CollectionAllocation

                        If Amount > 0 Then
                            itemAllocation = New CollectionAllocation
                            With itemAllocation
                                .WESMBillSummaryNo = item
                                .BillingPeriod = item.BillPeriod
                                .CollectionNumber = itemCollection.CollectionNumber
                                .Amount = Amount
                                .EndingBalance = Math.Abs(CType(BFactory.CloneObject(item.EndingBalance), Decimal))
                                .AllocationDate = Me.AllocationDate
                                .CollectionType = EnumCollectionType.Energy
                                .NewDueDate = CType(BFactory.CloneObject(item.NewDueDate), Date)
                                .DueDate = DueDate
                                .ReferenceNumber = CType(BFactory.CloneObject(item.INVDMCMNo), String)
                                .ReferenceType = CType(BFactory.CloneObject(item.SummaryType), EnumSummaryType)
                            End With

                            item.EndingBalance = item.EndingBalance + Amount
                            resultCollectionAllocations.Add(itemAllocation)
                        End If

                        If PrudentialAmount > 0 Then
                            itemAllocation = New CollectionAllocation
                            With itemAllocation
                                .WESMBillSummaryNo = item
                                .BillingPeriod = item.BillPeriod
                                .CollectionNumber = 0
                                .Amount = PrudentialAmount * -1D
                                .EndingBalance = tempEndingBalance
                                .AllocationDate = Me.AllocationDate
                                .CollectionType = EnumCollectionType.Energy
                                .NewDueDate = CType(BFactory.CloneObject(item.NewDueDate), Date)
                                .DueDate = DueDate
                                .ReferenceNumber = CType(BFactory.CloneObject(item.INVDMCMNo), String)
                                .ReferenceType = CType(BFactory.CloneObject(item.SummaryType), EnumSummaryType)
                            End With

                            item.EndingBalance = item.EndingBalance + PrudentialAmount
                            resultCollectionAllocations.Add(itemAllocation)
                        End If

                        Amount = 0
                        PrudentialAmount = 0
                        AllocateCollection = False
                    End If

                    If dicWESMBillSummary.ContainsKey(item.WESMBillSummaryNo) Then
                        dicWESMBillSummary(item.WESMBillSummaryNo) = item
                    Else
                        dicWESMBillSummary.Add(item.WESMBillSummaryNo, item)
                    End If
                Next

            Case AllocationCategory.VatOnEnergy
                AllocateCollection = True

                Dim items = From x In listSummaries _
                            Select x Order By x.NewDueDate Ascending, x.BillPeriod Ascending, x.EndingBalance Ascending

                For Each item In items

                    If Amount = 0 Then
                        Exit Function
                    End If

                    Dim endingBalance As Decimal = item.EnergyWithhold
                    Dim allocatedAmount As Decimal = 0D

                    If Amount > Math.Abs(item.EndingBalance - item.EnergyWithhold) Then
                        Amount = Amount - Math.Abs(item.EndingBalance - item.EnergyWithhold)
                        allocatedAmount = Math.Abs(item.EndingBalance - item.EnergyWithhold)
                    Else
                        allocatedAmount = Amount
                        endingBalance = item.EndingBalance - item.EnergyWithhold + Amount
                        Amount = 0
                        AllocateCollection = False
                    End If

                    Dim itemAllocation As New CollectionAllocation
                    With itemAllocation
                        .WESMBillSummaryNo = item
                        .BillingPeriod = item.BillPeriod
                        .CollectionNumber = itemCollection.CollectionNumber
                        .Amount = allocatedAmount
                        .NewDueDate = CType(BFactory.CloneObject(item.NewDueDate), Date)
                        .DueDate = CType(BFactory.CloneObject(item.NewDueDate), Date)
                        .EndingBalance = Math.Abs(CType(BFactory.CloneObject(item.EndingBalance), Decimal))
                        .AllocationDate = Me.AllocationDate
                        .CollectionType = EnumCollectionType.VatOnEnergy
                        .ReferenceNumber = CType(BFactory.CloneObject(item.INVDMCMNo), String)
                        .ReferenceType = CType(BFactory.CloneObject(item.SummaryType), EnumSummaryType)
                    End With

                    'Update the ending balance
                    item.EndingBalance = endingBalance

                    If dicWESMBillSummary.ContainsKey(item.WESMBillSummaryNo) Then
                        dicWESMBillSummary(item.WESMBillSummaryNo) = item
                    Else
                        dicWESMBillSummary.Add(item.WESMBillSummaryNo, item)
                    End If

                    resultCollectionAllocations.Add(itemAllocation)
                Next
        End Select

    End Function

#End Region

#Region "AllocateDrawdown"

    Private Function AllocateDrawdown(ByVal participant As AMParticipants, _
                                      ByRef resultCollectionAllocations As List(Of CollectionAllocation), _
                                      ByRef dicWESMBillSummary As Dictionary(Of Long, WESMBillSummary), _
                                      ByVal InterestRate As Decimal, ByRef PrudentialAmount As Decimal) As Boolean
        Dim IsContinue As Boolean

        'Allocation of Default Interest on Energy
        If PrudentialAmount > 0 Then
            Dim listEnergy = (From x In Me.ListWESMBillSummaries _
                              Where x.ChargeType = EnumChargeType.E _
                              And x.IDNumber.IDNumber = participant.IDNumber And x.EndingBalance < 0 _
                              Select x).ToList()

            IsContinue = Me.AllocateDrawdown(participant, resultCollectionAllocations, _
                                             dicWESMBillSummary, listEnergy, InterestRate, _
                                             AllocationCategory.DefaultInterestOnEnergy, PrudentialAmount)
        End If

        'Allocation of Energy
        If PrudentialAmount > 0 And IsContinue = True Then
            Dim listEnergy = (From x In Me.ListWESMBillSummaries _
                              Where x.ChargeType = EnumChargeType.E _
                              And x.IDNumber.IDNumber = participant.IDNumber And x.EndingBalance < 0 _
                              Select x Order By x.DueDate, x.EndingBalance, x.INVDMCMNo, x.IDType).ToList()

            IsContinue = Me.AllocateDrawdown(participant, resultCollectionAllocations, _
                                             dicWESMBillSummary, listEnergy, InterestRate, _
                                             AllocationCategory.Energy, PrudentialAmount)
        End If

    End Function

    Private Function AllocateDrawdown(ByVal participant As AMParticipants, _
                                      ByRef resultCollectionAllocations As List(Of CollectionAllocation), _
                                      ByRef dicWESMBillSummary As Dictionary(Of Long, WESMBillSummary), _
                                      ByVal listSummaries As List(Of WESMBillSummary), _
                                      ByVal InterestRate As Decimal, ByVal category As AllocationCategory, _
                                      ByRef PrudentialAmount As Decimal) As Boolean
        Select Case category
            Case AllocationCategory.DefaultInterestOnEnergy
                AllocateDrawdown = True

                'Get the distinct due date and billing period
                Dim itemDistincts = From x In listSummaries _
                                    Select x.NewDueDate, x.BillPeriod Distinct _
                                    Order By NewDueDate, BillPeriod

                For Each itemDistinct In itemDistincts
                    Dim newDueDateValue = itemDistinct.NewDueDate
                    Dim billPeriodvalue = itemDistinct.BillPeriod

                    Dim items = From x In listSummaries _
                                Where x.NewDueDate = newDueDateValue And x.BillPeriod = billPeriodvalue _
                                Select x

                    'Check if the collection can pay the default interest with the same 
                    'Due Date and Billing period
                    Dim tempTotal As Decimal = 0
                    For Each item In items
                        tempTotal += Math.Abs(BFactory.ComputeDefaultInterest(item.DueDate, item.NewDueDate, Me.DrawdownDate, _
                                                                              item.EndingBalance - item.EnergyWithhold, InterestRate))
                        If tempTotal > PrudentialAmount Then
                            AllocateDrawdown = False
                            Exit Function
                        End If
                    Next

                    For Each item In items
                        Dim defaultValue As Decimal = BFactory.ComputeDefaultInterest(item.DueDate, item.NewDueDate, Me.DrawdownDate, _
                                                                                      item.EndingBalance - item.EnergyWithhold, InterestRate)
                        If defaultValue <> 0 Then
                            PrudentialAmount = PrudentialAmount + defaultValue
                        Else
                            Continue For
                        End If

                        Dim itemAllocation As New CollectionAllocation
                        With itemAllocation
                            .WESMBillSummaryNo = item
                            .BillingPeriod = item.BillPeriod
                            .CollectionNumber = 0
                            .Amount = defaultValue
                            .DueDate = CType(BFactory.CloneObject(item.NewDueDate), Date)
                            .EndingBalance = CType(BFactory.CloneObject(item.EndingBalance), Decimal)
                            .NewDueDate = Me.DrawdownDate
                            .AllocationDate = Me.AllocationDate
                            .CollectionType = EnumCollectionType.DefaultInterestOnEnergy
                            .ReferenceNumber = CType(BFactory.CloneObject(item.INVDMCMNo), String)
                            .ReferenceType = CType(BFactory.CloneObject(item.SummaryType), EnumSummaryType)
                        End With

                        'Add in the Dictionary
                        If Not Me.DicWESMBillDueDate.ContainsKey(item.WESMBillSummaryNo) Then
                            Me.DicWESMBillDueDate.Add(item.WESMBillSummaryNo, CType(BFactory.CloneObject(item.NewDueDate), Date))
                        End If

                        'Update the new duedate
                        item.NewDueDate = AllocationDate

                        If dicWESMBillSummary.ContainsKey(item.WESMBillSummaryNo) Then
                            dicWESMBillSummary(item.WESMBillSummaryNo) = item
                        Else
                            dicWESMBillSummary.Add(item.WESMBillSummaryNo, item)
                        End If

                        resultCollectionAllocations.Add(itemAllocation)
                    Next
                Next

            Case AllocationCategory.Energy
                AllocateDrawdown = True

                For Each item In listSummaries
                    If PrudentialAmount = 0 Then
                        Exit Function
                    End If

                    Dim endingBalance As Decimal = 0D
                    Dim allocatedAmount As Decimal = 0D

                    If PrudentialAmount >= Math.Abs(item.EndingBalance) Then
                        PrudentialAmount = PrudentialAmount + item.EndingBalance
                        allocatedAmount = Math.Abs(item.EndingBalance)
                    Else
                        allocatedAmount = PrudentialAmount
                        endingBalance = item.EndingBalance + PrudentialAmount
                        PrudentialAmount = 0
                        AllocateDrawdown = False
                    End If

                    Dim DueDate As Date
                    If Me.DicWESMBillDueDate.ContainsKey(item.WESMBillSummaryNo) Then
                        DueDate = Me.DicWESMBillDueDate(item.WESMBillSummaryNo)
                    Else
                        DueDate = CType(BFactory.CloneObject(item.NewDueDate), Date)
                    End If

                    Dim itemAllocation As New CollectionAllocation
                    With itemAllocation
                        .WESMBillSummaryNo = item
                        .BillingPeriod = item.BillPeriod
                        .CollectionNumber = 0
                        .Amount = allocatedAmount * -1D
                        .EndingBalance = CType(BFactory.CloneObject(item.EndingBalance), Decimal)
                        .NewDueDate = CType(BFactory.CloneObject(item.NewDueDate), Date)
                        .DueDate = DueDate
                        .AllocationDate = Me.AllocationDate
                        .CollectionType = EnumCollectionType.Energy
                        .ReferenceNumber = CType(BFactory.CloneObject(item.INVDMCMNo), String)
                        .ReferenceType = CType(BFactory.CloneObject(item.SummaryType), EnumSummaryType)
                    End With

                    'Update the ending balance
                    item.EndingBalance = endingBalance

                    If dicWESMBillSummary.ContainsKey(item.WESMBillSummaryNo) Then
                        dicWESMBillSummary(item.WESMBillSummaryNo) = item
                    Else
                        dicWESMBillSummary.Add(item.WESMBillSummaryNo, item)
                    End If

                    resultCollectionAllocations.Add(itemAllocation)
                Next

        End Select
    End Function

#End Region


End Class
