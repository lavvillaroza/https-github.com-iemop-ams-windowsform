
Imports AccountsManagementObjects
Imports AccountsManagementDataAccess

Public Class SPAHelper
    Private dicListofSeq As New Dictionary(Of String, Long)
#Region "WESMBillHelper"
    Public _WBillHelper As WESMBillHelper
    Private ReadOnly Property WBillHelper() As WESMBillHelper
        Get
            Return _WBillHelper
        End Get
    End Property
#End Region

#Region "DAL"
    Private _DataAccess As DAL
    Public ReadOnly Property DataAccess() As DAL
        Get
            Return Me._DataAccess
        End Get
    End Property
#End Region

#Region "BFactory"
    Public _BFactory As New BusinessFactory
    Private ReadOnly Property BFactory() As BusinessFactory
        Get
            Return _BFactory
        End Get
    End Property
#End Region
    Public Sub New()
        Me.Reinitialize()
    End Sub

    Public Sub InitializeAdding()
        Me._DataAccess = DAL.GetInstance()
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._BFactory = BusinessFactory.GetInstance

        Me._ListOfWESMBillSummaryAR = New List(Of WESMBillSummary)
        Me._ListOfParticipantsInfo = Me.WBillHelper.GetAMParticipants()

        Me._ListOfParticipants = (From x In Me.ListOfParticipantsInfo _
                                  Select x.ParticipantID).ToList()

        Me._CalendarBP = Me.WBillHelper.GetCalendarBP()
        Me.JVSignatories = WBillHelper.GetSignatories("JV").First
        Me.DMCMSignatories = WBillHelper.GetSignatories("DMCM").First()
    End Sub

#Region "Method for Viewing SPA"
    Public Sub InitializeViewing(ByVal SPANumber As Long)
        Me._DataAccess = DAL.GetInstance()
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._BFactory = BusinessFactory.GetInstance
        Me._objSPAMain = WBillHelper.GetSPAMain(SPANumber)
        Me.GetObjectForViewing()
    End Sub

    Private Sub GetObjectForViewing()
        With Me.objSPAMain
            For Each item In .SPADetailsList
                If item.BalanceType = EnumBalanceType.AR Then
                    Me._ListOfWESMBillSummaryAR.Add(item.WESMBillSummaryInfo)
                Else
                    Me._ListOfWESMBillSummaryAP.Add(item.WESMBillSummaryInfo)
                End If
            Next

            Me._ListofSPABillDueDate = (From x In Me._ListOfWESMBillSummaryAR Select x.DueDate).Distinct.ToList()

            For Each item In .SPAMonitoringList
                If item.WESMBillSummaryInfo.BeginningBalance < 0 Then
                    item.WESMBillSummaryInfo.BalanceType = EnumBalanceType.AR
                    Me._SPAWESMBillSummaryListAR.Add(item.WESMBillSummaryInfo)
                Else
                    item.WESMBillSummaryInfo.BalanceType = EnumBalanceType.AP
                    Me._SPAWESMBillSummaryListAP.Add(item.WESMBillSummaryInfo)
                End If
            Next

            Dim GetDMCMListFromSPADetails As List(Of DebitCreditMemo) = Me.WBillHelper.GetDebitCreditMemoMainFromJV(.JVClosing.JVNumber)

            For Each dmcmItem In GetDMCMListFromSPADetails
                Me._DMCMListForSPA.Add(dmcmItem)
            Next

            Dim GetDMCMListFromSPAMon As List(Of DebitCreditMemo) = Me.WBillHelper.GetDebitCreditMemoMainFromJV(.JVClosing.JVNumber)
            For Each dmcmItem In GetDMCMListFromSPAMon
                Me._DMCMListForSPA.Add(dmcmItem)
            Next

            Dim oListofSPADetailsAR As List(Of SPADetails) = (From x In .SPADetailsList Where x.BalanceType = EnumBalanceType.AR Select x).ToList
            Dim oListofSPADetailsAP As List(Of SPADetails) = (From x In .SPADetailsList Where x.BalanceType = EnumBalanceType.AP Select x).ToList

            Me._AREWESMBillSummaryListDT = Me.CreateViewARDataTable(Me.ListOfWESMBillSummaryAR, oListofSPADetailsAR) 'create datatable for wesmbillsummary ar
            Me._APEWESMBillSummaryListDT = Me.CreateViewAPDataTable(Me.ListOfWESMBillSummaryAP, oListofSPADetailsAP) 'create datatable for wesmbillsummary ap

        End With
    End Sub
#End Region

    Public Sub Reinitialize()
        Me._objSPAMain = New SPAMain
        Me._ListOfWESMBillSummaryAP = New List(Of WESMBillSummary)
        Me._SPAWESMBillSummaryListAR = New List(Of WESMBillSummary)
        Me._SPAWESMBillSummaryListAP = New List(Of WESMBillSummary)
        Me._SPAWESMBillSummaryMonitoringList = New List(Of SPAMonitoring)
        Me._DMCMListForSPA = New List(Of DebitCreditMemo)
        Me._GPPosted = New List(Of WESMBillGPPosted)
        Me.InitialJVNo = 0
        Me.InitialDMCMNo = 0
        Me._WESMBillSummaryNo = 0
        Me._WESMBillSummarySPANo = 0
    End Sub

#Region "WESMBillSummary No"
    Private _WESMBillSummaryNo As Long
    Public ReadOnly Property WESMBillSummaryNo() As Long
        Get
            Return _WESMBillSummaryNo
        End Get
    End Property
#End Region

#Region "WESMBillSummary SPA No"
    Private _WESMBillSummarySPANo As Long
    Public ReadOnly Property WESMBillSummarySPANo() As Long
        Get
            Return _WESMBillSummarySPANo
        End Get
    End Property
#End Region

#Region "Initial DMCM No"
    Private InitialDMCMNo As Long = 0
#End Region

#Region "Initial JV No"
    Private InitialJVNo As Long = 0
#End Region

#Region "Initial SPA No"
    Private InitialSPANo As Long = 1
#End Region

#Region "Property of SPA"
    Private _objSPAMain As New SPAMain
    Public ReadOnly Property objSPAMain() As SPAMain
        Get
            Return _objSPAMain
        End Get
    End Property
#End Region

#Region "Property for SPA DueDate List"
    Private _ListofSPABillDueDate As List(Of Date)
    Public ReadOnly Property ListofSPABillDueDate() As List(Of Date)
        Get
            Return _ListofSPABillDueDate
        End Get
    End Property

#End Region

#Region "Property of AR WESMBillSummaryList DataTable"
    Private _AREWESMBillSummaryListDT As New DataTable
    Public ReadOnly Property AREWESMBillSummaryListDT() As DataTable
        Get
            Return _AREWESMBillSummaryListDT
        End Get
    End Property
#End Region

#Region "Property of AP WESMBillSummaryList DataTable"
    Private _APEWESMBillSummaryListDT As New DataTable
    Public ReadOnly Property APEWESMBillSummaryListDT() As DataTable
        Get
            Return _APEWESMBillSummaryListDT
        End Get
    End Property
#End Region

#Region "Property for List of ParticipantID"
    Private _ListOfParticipants As New List(Of String)
    Public ReadOnly Property ListOfParticipants() As List(Of String)
        Get
            Return _ListOfParticipants
        End Get
    End Property
#End Region

#Region "Property for List of ParticipantID Info"
    Private _ListOfParticipantsInfo As New List(Of AMParticipants)
    Public ReadOnly Property ListOfParticipantsInfo() As List(Of AMParticipants)
        Get
            Return _ListOfParticipantsInfo
        End Get
    End Property
#End Region

#Region "Property for List of WESMBillSummary AR"
    Private _ListOfWESMBillSummaryAR As New List(Of WESMBillSummary)
    Public ReadOnly Property ListOfWESMBillSummaryAR() As List(Of WESMBillSummary)
        Get
            Return _ListOfWESMBillSummaryAR
        End Get
    End Property
#End Region

#Region "Property for List of WESMBillSummary AP"
    Private _ListOfWESMBillSummaryAP As New List(Of WESMBillSummary)
    Public ReadOnly Property ListOfWESMBillSummaryAP() As List(Of WESMBillSummary)
        Get
            Return _ListOfWESMBillSummaryAP
        End Get
    End Property
#End Region

#Region "Property of Calendar Billing Period"
    Private _CalendarBP As New List(Of CalendarBillingPeriod)
    Public ReadOnly Property CalendarBP() As List(Of CalendarBillingPeriod)
        Get
            Return _CalendarBP
        End Get
    End Property
#End Region

#Region "Property for List of SPABillSummary on AR"
    Private _SPAWESMBillSummaryListAR As New List(Of WESMBillSummary)
    Public ReadOnly Property SPAWESMBillSummaryListAR() As List(Of WESMBillSummary)
        Get
            Return _SPAWESMBillSummaryListAR
        End Get
    End Property
#End Region

#Region "Property for List of SPABillSummary on AP"
    Private _SPAWESMBillSummaryListAP As New List(Of WESMBillSummary)
    Public ReadOnly Property SPAWESMBillSummaryListAP() As List(Of WESMBillSummary)
        Get
            Return _SPAWESMBillSummaryListAP
        End Get
    End Property
#End Region

#Region "Property of SPA WESMBillSummary Monitoring"
    Private _SPAWESMBillSummaryMonitoringList As New List(Of SPAMonitoring)
    Public ReadOnly Property SPAWESMBillSUmmaryMonitoringList() As List(Of SPAMonitoring)
        Get
            Return _SPAWESMBillSummaryMonitoringList
        End Get
    End Property
#End Region

#Region "GP Posted"
    Private _GPPosted As New List(Of WESMBillGPPosted)
    Public ReadOnly Property GPPosted() As List(Of WESMBillGPPosted)
        Get
            Return _GPPosted
        End Get
    End Property
#End Region

#Region "DMCM List Created"
    Private _DMCMListForSPA As New List(Of DebitCreditMemo)
    Public ReadOnly Property DMCMListForSPA() As List(Of DebitCreditMemo)
        Get
            Return _DMCMListForSPA
        End Get
    End Property
#End Region

#Region "DMCM Signatories"
    Private _DMCMSignatories As New DocSignatories
    Public Property DMCMSignatories() As DocSignatories
        Get
            Return _DMCMSignatories
        End Get
        Set(value As DocSignatories)
            _DMCMSignatories = value
        End Set
    End Property
#End Region

#Region "JV Signatories"
    Private _JVSignatories As New DocSignatories
    Public Property JVSignatories() As DocSignatories
        Get
            Return _JVSignatories
        End Get
        Set(value As DocSignatories)
            _JVSignatories = value
        End Set
    End Property
#End Region

#Region "Function for Getting Participant WESM Bill Summary"
    Public Function GetWESMBillSummaryByParticipantID(ByVal ParticipantID As String, ByVal oChargeType As EnumChargeType) As List(Of WESMBillSummary)
        Dim ret As New List(Of WESMBillSummary)
        Dim GetIDNumber As String = (From x In Me.ListOfParticipantsInfo Where x.ParticipantID = ParticipantID Select x.IDNumber).FirstOrDefault

        Me._ListOfWESMBillSummaryAR = New List(Of WESMBillSummary)
        If Not GetIDNumber Is Nothing Then
            ret = (From x In Me.WBillHelper.GetWESMBillSummaryWithAREndingBalance(GetIDNumber)
                   Where x.IDNumber.ParticipantID = ParticipantID And x.ChargeType = oChargeType
                   Select x Order By x.ChargeType, x.NewDueDate, x.BillPeriod, x.EndingBalance).ToList()
        End If
        Me._ListofSPABillDueDate = (From x In ret Select x.DueDate).Distinct.ToList()
        Me._ListOfWESMBillSummaryAR = ret
        Return ret
    End Function
#End Region

#Region "Function for Creating SPA Invoices"
    Public Sub CreateSPA(ByVal ParticipantID As String,
                         ByVal DateFrom As Date,
                         ByVal inMonths As Integer,
                         ByVal InterestRate As Decimal,
                         ByVal ListofWESMBillSummaryNo As List(Of WESMBillSummary),
                         ByVal eChargeType As EnumChargeType)

        Try
            Dim ListOfWESMBillSummarySelected As List(Of WESMBillSummary) = (From x In Me.ListOfWESMBillSummaryAR
                                                                        Join y In ListofWESMBillSummaryNo On x.WESMBillSummaryNo Equals y.WESMBillSummaryNo
                                                                        Select x Order By x.EndingBalance Descending).ToList

            Dim ListOfWESMBillSummaryAR As List(Of WESMBillSummary) = (From x In ListOfWESMBillSummarySelected Select x).ToList()

            Dim AMParticipantInfo As AMParticipants = (From x In Me.ListOfParticipantsInfo Where x.ParticipantID = ParticipantID Select x).FirstOrDefault

            Dim ListOfWESMBillSummaryAP As New List(Of WESMBillSummary)
            Dim TotalAmountofAllInvoices As Decimal = (From x In ListOfWESMBillSummarySelected Select x.EndingBalance).Sum
            Dim ListOfSPADetailsOnAR As New List(Of SPADetails)

            Using oSPAMain As New SPAMain
                oSPAMain.ParticipantInfo = AMParticipantInfo
                oSPAMain.FirstPaymentDate = DateFrom
                oSPAMain.InMonths = inMonths
                oSPAMain.InterestRate = InterestRate
                oSPAMain.TotalPrincipalAmount = Math.Round(TotalAmountofAllInvoices, 2)
                Me._objSPAMain = oSPAMain

                For Each item In ListOfWESMBillSummaryAR
                    Using SPAMonItem As New SPADetails
                        With SPAMonItem
                            .WESMBillSummaryInfo = item
                            .BalanceAmount = item.EndingBalance
                            .BalanceType = EnumBalanceType.AR
                            .ChargeType = item.ChargeType
                            ListOfSPADetailsOnAR.Add(SPAMonItem)
                        End With
                    End Using
                Next

                For Each item In ListOfSPADetailsOnAR
                    item.DMCMNumber = Me.CreateDMCMClosingAR(item)
                    oSPAMain.SPADetailsList.Add(item)
                Next

                Me._AREWESMBillSummaryListDT = Me.CreateARDataTable(ListOfWESMBillSummaryAR, ListOfSPADetailsOnAR) 'create Datatable for WESMBillSummary AR

                Dim ListOfSPAMonitoringOnAP As List(Of SPADetails) = Me.GetAccountsPayable(ListOfSPADetailsOnAR, ListOfWESMBillSummaryAR, InterestRate, eChargeType)

                For Each item In ListOfSPAMonitoringOnAP
                    oSPAMain.SPADetailsList.Add(item)
                Next

                Dim ListOfWESMBillSummaryOnAP As List(Of WESMBillSummary) = (From x In Me.ListOfWESMBillSummaryAP Where x.ChargeType = eChargeType Select x).ToList()

                Me._APEWESMBillSummaryListDT = Me.CreateAPDataTable(ListOfWESMBillSummaryOnAP, ListOfSPAMonitoringOnAP) 'create datatable for wesmbillsummary ap
                Dim GetTotalSPAAR As Decimal = (From x In oSPAMain.SPADetailsList Where x.BalanceType = EnumBalanceType.AR Select x.BalanceAmount).Sum()
                Dim GetTotalSPAAP As Decimal = (From x In oSPAMain.SPADetailsList Where x.BalanceType = EnumBalanceType.AP Select x.BalanceAmount).Sum()

                oSPAMain.JVClosing = Me.GenerateJVClosing(GetTotalSPAAR, GetTotalSPAAP)
                Me.CreateWESMBillSummary(oSPAMain, AMParticipantInfo, DateFrom, InterestRate, inMonths)

                Dim GetTotalSPABIllAR As Decimal = (From x In Me.SPAWESMBillSummaryListAR Where x.BalanceType = EnumBalanceType.AR Select x.BeginningBalance).Sum()
                Dim GetTotalSPABIllAP As Decimal = (From x In Me.SPAWESMBillSummaryListAP Where x.BalanceType = EnumBalanceType.AP Select x.BeginningBalance).Sum()
                oSPAMain.JVSetup = Me.GenerateJVSetupForSPA(GetTotalSPABIllAR, GetTotalSPABIllAP)
                Me._objSPAMain = oSPAMain
            End Using
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub

    Private Function GetAccountsPayable(ByVal SPAMonitoringList As List(Of SPADetails), _
                                        ByVal ListOfWESMBillSummary As List(Of WESMBillSummary), _
                                        ByVal SPARate As Decimal, _
                                        ByVal eChargeType As EnumChargeType) As List(Of SPADetails)

        Dim ret As New List(Of SPADetails)
        Dim GetWESMBillSummaryList = (From x In SPAMonitoringList
                                      Join y In ListOfWESMBillSummary On y.WESMBillSummaryNo Equals x.WESMBillSummaryInfo.WESMBillSummaryNo
                                      Select x, y.WESMBillBatchNo).ToList()

        Dim GetWESMBillSummaryGroup = (From x In GetWESMBillSummaryList Select x.WESMBillBatchNo Order By WESMBillBatchNo).Distinct.ToList()
        For Each WBBatchNo In GetWESMBillSummaryGroup
            Dim GetWESMBillSummaryListPerBP = (From x In GetWESMBillSummaryList
                                               Where x.WESMBillBatchNo = x.WESMBillBatchNo
                                               Select x).ToList
            Dim TotalSPAAmount As Decimal = Math.Abs((From x In GetWESMBillSummaryListPerBP Where x.WESMBillBatchNo = WBBatchNo Select x.x.BalanceAmount).Sum())
            Dim GetWESMBillSummaryListAP = (From x In WBillHelper.GetWESMBillSummaryWithAPEndingBalance(WBBatchNo, eChargeType)
                                            Where x.WESMBillBatchNo = WBBatchNo
                                            Select x).ToList()

            Dim TotalWESMBillSummaryListAP As Decimal = (From x In GetWESMBillSummaryListAP Select x.EndingBalance).Sum()

            Dim SPAMonitoringAPList As New List(Of SPADetails)
            For Each item In GetWESMBillSummaryListAP
                Me._ListOfWESMBillSummaryAP.Add(item)
                Dim ShareAmount As Decimal = Me.ComputeAllocation(item.EndingBalance, TotalWESMBillSummaryListAP, TotalSPAAmount)
                If ShareAmount <> 0 Then
                    Using SPAMon As New SPADetails
                        With SPAMon
                            .WESMBillSummaryInfo = item
                            .BalanceAmount = ShareAmount
                            .BalanceType = EnumBalanceType.AP
                            .ChargeType = item.ChargeType
                            SPAMonitoringAPList.Add(SPAMon)
                        End With
                    End Using
                End If
            Next

            Dim AdjustTotalSPAAmount As Decimal = (From x In SPAMonitoringAPList Select x.BalanceAmount).Sum()
            If Not AdjustTotalSPAAmount = TotalSPAAmount And Not TotalSPAAmount = 0 Then
                Dim AdjustSPAMonitoringItem As SPADetails = (From x In SPAMonitoringAPList Select x Order By x.BalanceAmount Descending).First
                Dim AdjustedSPAMonitoringAmount As Decimal = TotalSPAAmount - AdjustTotalSPAAmount
                AdjustSPAMonitoringItem.BalanceAmount += AdjustedSPAMonitoringAmount
            End If

            For Each item In SPAMonitoringAPList
                item.DMCMNumber = Me.CreateDMCMClosingAP(item)
                ret.Add(item)
            Next
        Next

        Return ret
    End Function

    Private Sub CreateWESMBillSummary(ByRef objSPAMain As SPAMain,
                                      ByVal ParticipantInfo As AMParticipants,
                                      ByVal FirstPaymentDate As Date,
                                      ByVal InterestRate As Decimal,
                                      ByVal inMonths As Integer)

        Dim ListOfSPADetailOnAR As List(Of SPADetails) = (From x In objSPAMain.SPADetailsList Where x.BalanceType = EnumBalanceType.AR Select x).ToList
        Dim ListOfSPADetailOnAP As List(Of SPADetails) = (From x In objSPAMain.SPADetailsList Where x.BalanceType = EnumBalanceType.AP Select x).ToList

        Dim getTotalAR = (From x In ListOfSPADetailOnAR Select x.BalanceAmount).Sum()
        Dim getTotalAP = (From x In ListOfSPADetailOnAP Select x.BalanceAmount).Sum()
        Dim GetChargeType As EnumChargeType = (From x In objSPAMain.SPADetailsList Select x.ChargeType).Distinct().First

        Dim DividedRate As Decimal = InterestRate / 12
        Dim ListOfFactor As New List(Of Decimal)
        Dim Factor As Decimal = 0
        Dim TotalFactor As Decimal = 0
        For i As Integer = 1 To inMonths
            Dim ComputedFactor As Decimal = 0
            If i = 1 Then
                ComputedFactor = CDec(1 / (1 + DividedRate))
                ListOfFactor.Add(ComputedFactor)
            Else
                ComputedFactor = CDec(Factor / (1 + DividedRate))
                ListOfFactor.Add(ComputedFactor)
            End If
            Factor = ComputedFactor
            TotalFactor += ComputedFactor
        Next
        Me.CreateSPAWESMBillSummaryAP(objSPAMain, ParticipantInfo, ListOfSPADetailOnAP, FirstPaymentDate, inMonths, TotalFactor, DividedRate, GetChargeType)
    End Sub

    Private Sub CreateSPAWESMBillSummaryAR(ByRef objSPAMain As SPAMain,
                                           ByVal ParticipantInfo As AMParticipants,
                                           ByVal ListSPAMonitoringAP As List(Of SPAMonitoring),
                                           ByVal ListOfSPABillAP As List(Of WESMBillSummary),
                                           ByVal oChargeType As EnumChargeType)

        Dim DistinctPaymentDate As List(Of Date) = (From x In ListOfSPABillAP Select x.DueDate Order By DueDate).Distinct().ToList()
        Dim ListOfSPABillFinal = (From x In ListOfSPABillAP _
                                  Join y In ListSPAMonitoringAP On y.WESMBillSummaryInfo.WESMBillSummaryNo Equals x.WESMBillSummaryNo _
                                  Select x, y.PrincipalAmount, y.InterestAmount, y.BalanceAmount).ToList()

        Dim SPAWESMBillSummaryARList As New List(Of WESMBillSummary)
        Dim SPAWESMBillSummaryMonitoringList As New List(Of SPAMonitoring)
        For Each iDate In DistinctPaymentDate
            Me._WESMBillSummarySPANo += 1
            Me._WESMBillSummaryNo += 1
            Dim SPAWESMBillSummaryitem As New WESMBillSummary
            Dim TotalMonthlyAmort As Decimal = (From x In ListOfSPABillFinal Where x.x.DueDate = iDate Select x.x.BeginningBalance).Sum() * -1
            Dim TotalPrincipalAmnt As Decimal = (From x In ListOfSPABillFinal Where x.x.DueDate = iDate Select x.PrincipalAmount).Sum() * -1
            Dim TotalInterestAmnt As Decimal = (From x In ListOfSPABillFinal Where x.x.DueDate = iDate Select x.InterestAmount).Sum() * -1
            Dim TotalBalanceAmnt As Decimal = (From x In ListOfSPABillFinal Where x.x.DueDate = iDate Select x.BalanceAmount).Sum() * -1

            With SPAWESMBillSummaryitem
                .BillPeriod = (From x In Me.CalendarBP _
                               Where x.StartDate.ToString("MM/yyyy") = iDate.AddMonths(-2).ToString("MM/yyyy") _
                               And x.EndDate.ToString("MM/yyyy") = iDate.AddMonths(-1).ToString("MM/yyyy") _
                               Select x.BillingPeriod).FirstOrDefault
                If .BillPeriod = 0 Then
                    Throw New Exception("Create Billing Period for " + iDate.AddMonths(-2).ToString("MM/yyyy") + " - " + iDate.AddMonths(-1).ToString("MM/yyyy") _
                                        + Environment.NewLine + "Please go to File Menu > Libraries > Calendar Billing Period.")
                End If

                .IDNumber = ParticipantInfo
                .ChargeType = oChargeType
                .DueDate = iDate
                .BeginningBalance = TotalMonthlyAmort
                .EndingBalance = TotalMonthlyAmort
                .IDType = EnumIDType.P.ToString
                .NewDueDate = iDate
                .SummaryType = EnumSummaryType.SPA
                .INVDMCMNo = EnumSummaryType.SPA.ToString & "-" & Me.WESMBillSummarySPANo.ToString("0000000")
                .WESMBillSummaryNo = Me.WESMBillSummaryNo
                .BalanceType = EnumBalanceType.AR
                .SPANo = InitialSPANo
                .EnergyWithhold = EnumEnergyWithholdStatus.NotApplicable
            End With
            SPAWESMBillSummaryARList.Add(SPAWESMBillSummaryitem)
            Me.SPAWESMBillSummaryListAR.Add(SPAWESMBillSummaryitem)

            Dim SPAWESMBillSummaryMonitoring As New SPAMonitoring
            With SPAWESMBillSummaryMonitoring
                .WESMBillSummaryInfo = SPAWESMBillSummaryitem
                .MonthlyDueDate = iDate
                .MonthlyAmortization = TotalMonthlyAmort
                .PrincipalAmount = TotalPrincipalAmnt
                .InterestAmount = TotalInterestAmnt
                .BalanceAmount = TotalBalanceAmnt
            End With
            SPAWESMBillSummaryMonitoringList.Add(SPAWESMBillSummaryMonitoring)
            objSPAMain.SPAMonitoringList.Add(SPAWESMBillSummaryMonitoring)
        Next

        objSPAMain.TotalInterestAmount = (From x In SPAWESMBillSummaryMonitoringList Select x.InterestAmount).Sum()

        Dim CreatedDMCM As Long = Me.CreateDMCMSetupAR(SPAWESMBillSummaryARList, SPAWESMBillSummaryMonitoringList)
        For Each oitem In SPAWESMBillSummaryMonitoringList
            Dim GetoSPAMain_oSPAMonItem = (From x In objSPAMain.SPAMonitoringList Where x.WESMBillSummaryInfo.WESMBillSummaryNo = oitem.WESMBillSummaryInfo.WESMBillSummaryNo Select x).FirstOrDefault
            GetoSPAMain_oSPAMonItem.DMCMNumber = CreatedDMCM
            oitem.DMCMNumber = CreatedDMCM
        Next

    End Sub

    Private Sub CreateSPAWESMBillSummaryAP(ByRef objSPAMain As SPAMain,
                                           ByVal ParticipantInfo As AMParticipants,
                                           ByVal ListOfSPADetail As List(Of SPADetails),
                                           ByVal FirstPaymentDate As Date,
                                           ByVal inMonths As Integer,
                                           ByVal TotalFactor As Decimal,
                                           ByVal DividedRate As Decimal,
                                           ByVal oChargeType As EnumChargeType)

        Dim ListOfParticipantPerSPADetails = (From x In ListOfSPADetail
                                             Join y In ListOfWESMBillSummaryAP On y.WESMBillSummaryNo Equals x.WESMBillSummaryInfo.WESMBillSummaryNo
                                             Group By y.IDNumber.IDNumber Into SumOfPrincipalAmount = Sum(x.BalanceAmount)
                                             Select IDNumber, SumOfPrincipalAmount Order By IDNumber).ToList()

        Dim DistinctIDNumber As List(Of String) = (From x In ListOfParticipantPerSPADetails Select x.IDNumber).Distinct.ToList()
        Dim SPAWESMBillSummaryAPList As New List(Of WESMBillSummary)
        Dim SPAWESMBillSummaryMonitoringList As New List(Of SPAMonitoring)

        For Each item In DistinctIDNumber
            Dim SPAWESMBillSummaryAPListPerItem As New List(Of WESMBillSummary)
            Dim SPAWESMBillSummaryMonitoringListPerItem As New List(Of SPAMonitoring)
            Dim AMParticipantInfo As AMParticipants = (From x In Me.ListOfParticipantsInfo Where x.IDNumber = item Select x).First
            Dim TotalAPBalanceAmount As Decimal = (From x In ListOfParticipantPerSPADetails Where x.IDNumber = item Select x.SumOfPrincipalAmount).Sum()
            Dim rMonthlyAmortizationAmount As Decimal = Math.Round((TotalAPBalanceAmount / TotalFactor), 2) 'rounded by 2  
            Dim nrMonthlyAmortizationAmount As Decimal = TotalAPBalanceAmount / TotalFactor 'not rounded by 2  
            If TotalAPBalanceAmount <> 0 Then
                Dim rListOfMonthlyAmort As New Dictionary(Of Long, Decimal)
                'Dim rListOfPrincipalAmount As New Dictionary(Of Long, Decimal) 'rounded Principal Amount
                Dim rListOfInterestAmount As New Dictionary(Of Long, Decimal)  'rounded Interest Amount
                Dim rListOfBalance As New Dictionary(Of Long, Decimal)  'rounded Interest Amount
                Dim nrListOfMonthlyAmort As New Dictionary(Of Long, Decimal)
                'Dim nrListOfPrincipalAmount As New Dictionary(Of Long, Decimal) 'not rounded Principal Amount
                Dim nrListOfInterestAmount As New Dictionary(Of Long, Decimal)  'not rounded Interest Amount                
                Dim nrListOfBalance As New Dictionary(Of Long, Decimal)  'not rounded Interest Amount                

                Dim nrTotalAPBalanceAmount As Decimal = TotalAPBalanceAmount
                Dim rTotalAPBalanceAmount As Decimal = TotalAPBalanceAmount
                For i As Integer = 1 To inMonths
                    Dim Month As Integer = i
                    Dim nrInterestAmnt As Decimal = 0
                    Dim nrPrincipalAmnt As Decimal = 0
                    Dim rInterestAmnt As Decimal = 0
                    Dim rPrincipalAmnt As Decimal = 0
                    If Month = 1 Then
                        nrInterestAmnt = nrTotalAPBalanceAmount * DividedRate
                        nrPrincipalAmnt = nrMonthlyAmortizationAmount - nrInterestAmnt
                        nrTotalAPBalanceAmount -= nrPrincipalAmnt
                        nrListOfInterestAmount.Add(i, nrInterestAmnt)
                        'nrListOfPrincipalAmount.Add(i, nrPrincipalAmnt)
                        nrListOfMonthlyAmort.Add(i, nrMonthlyAmortizationAmount)
                        nrListOfBalance.Add(i, nrTotalAPBalanceAmount)

                        rInterestAmnt = Math.Round((rTotalAPBalanceAmount * DividedRate), 2)
                        rPrincipalAmnt = Math.Round((rMonthlyAmortizationAmount - rInterestAmnt), 2)
                        rTotalAPBalanceAmount -= rPrincipalAmnt
                        rListOfInterestAmount.Add(i, rInterestAmnt)
                        'rListOfPrincipalAmount.Add(i, rPrincipalAmnt)
                        rListOfMonthlyAmort.Add(i, rMonthlyAmortizationAmount)
                        rListOfBalance.Add(i, rTotalAPBalanceAmount)
                    Else
                        nrInterestAmnt = nrTotalAPBalanceAmount * DividedRate
                        nrPrincipalAmnt = nrMonthlyAmortizationAmount - nrInterestAmnt
                        nrTotalAPBalanceAmount -= nrPrincipalAmnt
                        nrListOfInterestAmount.Add(i, nrInterestAmnt)
                        'nrListOfPrincipalAmount.Add(i, nrPrincipalAmnt)
                        nrListOfMonthlyAmort.Add(i, nrMonthlyAmortizationAmount)
                        nrListOfBalance.Add(i, nrTotalAPBalanceAmount)

                        rInterestAmnt = Math.Round(rTotalAPBalanceAmount * DividedRate, 2)
                        rPrincipalAmnt = Math.Round(rMonthlyAmortizationAmount - rInterestAmnt, 2)
                        rTotalAPBalanceAmount -= rPrincipalAmnt
                        rListOfInterestAmount.Add(i, rInterestAmnt)
                        'rListOfPrincipalAmount.Add(i, rPrincipalAmnt)
                        rListOfMonthlyAmort.Add(i, rMonthlyAmortizationAmount)
                        rListOfBalance.Add(i, rTotalAPBalanceAmount)
                    End If
                Next
                Dim nrTotalMonthlyAmort As Decimal = nrListOfMonthlyAmort.Values.Sum()
                Dim nrTotalInterestAmount As Decimal = nrListOfInterestAmount.Values.Sum()
                'Dim nrTotalPrincipalAmount As Decimal = nrListOfPrincipalAmount.Values.Sum()
                Dim rTotalMonthlyAmort As Decimal = Math.Round(rListOfMonthlyAmort.Values.Sum(), 2)
                Dim rTotalInterestAmount As Decimal = Math.Round(rListOfInterestAmount.Values.Sum(), 2)
                'Dim rTotalPrincipalAmount As Decimal = Math.Round(rListOfPrincipalAmount.Values.Sum(), 2)

                Dim AdjOnInterestAmount As Decimal = Math.Round(nrTotalInterestAmount - rTotalInterestAmount, 2)
                'Dim AdjOnPrincipalAmount As Decimal = Math.Round(nrTotalPrincipalAmount - rTotalPrincipalAmount, 2)
                Dim AdjOnMonthlyAmortizationAmount As Decimal = Math.Round(nrTotalMonthlyAmort - rTotalMonthlyAmort, 2)
                Dim dicSPANumber As New Dictionary(Of Integer, Long)
                Dim dicWBSNumber As New Dictionary(Of Integer, Long)

                For i As Integer = 1 To inMonths
                    Me._WESMBillSummarySPANo += 1
                    Me._WESMBillSummaryNo += 1
                    dicSPANumber.Add(i, Me.WESMBillSummarySPANo)
                    dicWBSNumber.Add(i, Me.WESMBillSummaryNo)
                Next

                Dim TotalAPBalanceFinal As Decimal = 0
                For i As Integer = inMonths To 1 Step -1
                    Dim Month As Integer = i
                    
                    Dim ExitForBool As Boolean = False
                    Dim SPAWESMBillSummaryitem As New WESMBillSummary
                    Dim PaymentDate As Date = FirstPaymentDate.AddMonths(CInt(i - 1))
                    Dim oPrincipalAmount As Decimal = 0
                    Dim oInterestAmount As Decimal = 0
                    Dim FinalMonthlyAmortizationAmount As Decimal = 0

                    oInterestAmount = rListOfInterestAmount.Item(i)

                    If (rMonthlyAmortizationAmount + AdjOnMonthlyAmortizationAmount) <= 0 And Not AdjOnMonthlyAmortizationAmount = 0 Then
                        AdjOnMonthlyAmortizationAmount += rMonthlyAmortizationAmount
                        FinalMonthlyAmortizationAmount = 0
                    Else
                        FinalMonthlyAmortizationAmount = rMonthlyAmortizationAmount + AdjOnMonthlyAmortizationAmount
                        AdjOnMonthlyAmortizationAmount -= AdjOnMonthlyAmortizationAmount
                    End If

                    If (oInterestAmount + AdjOnInterestAmount) <= 0 And Not AdjOnInterestAmount = 0 Then
                        AdjOnInterestAmount += oInterestAmount '-= (oInterestAmount + AdjOnInterestAmount)
                        oInterestAmount = 0
                    Else
                        If AdjOnInterestAmount > 0 And FinalMonthlyAmortizationAmount = 0 And AdjOnMonthlyAmortizationAmount < 0 Then
                            AdjOnMonthlyAmortizationAmount += AdjOnInterestAmount
                            AdjOnInterestAmount -= AdjOnInterestAmount
                            oInterestAmount = 0
                        ElseIf AdjOnInterestAmount > 0 And AdjOnInterestAmount > FinalMonthlyAmortizationAmount And FinalMonthlyAmortizationAmount > 0 Then
                            oInterestAmount = (AdjOnInterestAmount - FinalMonthlyAmortizationAmount)
                            AdjOnInterestAmount -= oInterestAmount
                        ElseIf AdjOnInterestAmount > 0 And AdjOnInterestAmount > FinalMonthlyAmortizationAmount And FinalMonthlyAmortizationAmount < 0 Then
                            oInterestAmount = (AdjOnInterestAmount - FinalMonthlyAmortizationAmount)
                            FinalMonthlyAmortizationAmount = oInterestAmount
                            AdjOnInterestAmount -= oInterestAmount
                        Else
                            oInterestAmount += AdjOnInterestAmount
                            AdjOnInterestAmount -= AdjOnInterestAmount
                        End If
                    End If

                    oPrincipalAmount = Math.Round(((FinalMonthlyAmortizationAmount) - oInterestAmount), 2) '+ Math.Abs(AdjOnInterestAmount)

                    If i = 1 Then
                        rListOfBalance(i) = Math.Round(CDec(TotalAPBalanceAmount - oPrincipalAmount), 2)
                    Else
                        If oPrincipalAmount = 0 And CDec(rListOfBalance(i)) < 0 Then
                            rListOfBalance(i) = 0
                        Else
                            If CDec(rListOfBalance(i - 1)) = 0 Then
                                oPrincipalAmount += CDec(rListOfBalance(i))
                                FinalMonthlyAmortizationAmount = oPrincipalAmount + oInterestAmount
                                rListOfBalance(i) = 0
                            Else
                                If rListOfBalance.Count = i And CDec(rListOfBalance(i - 1) + oPrincipalAmount) <> 0 Then
                                    rListOfBalance(i) = 0
                                ElseIf rListOfBalance.Count > i And CDec(rListOfBalance(i - 1) + oPrincipalAmount) = 0 Then
                                    rListOfBalance(i) = Math.Round(CDec(rListOfBalance(i - 1) + oPrincipalAmount), 2)
                                    oPrincipalAmount = 0
                                    FinalMonthlyAmortizationAmount = oPrincipalAmount + oInterestAmount
                                Else
                                    rListOfBalance(i) = Math.Round(CDec(rListOfBalance(i - 1) - oPrincipalAmount), 2)
                                End If
                            End If
                        End If
                    End If


                    TotalAPBalanceFinal = rListOfBalance(i)

                    With SPAWESMBillSummaryitem
                        .BillPeriod = (From x In Me.CalendarBP
                                       Where x.StartDate.ToString("MM/yyyy") = PaymentDate.AddMonths(-2).ToString("MM/yyyy") _
                                       And x.EndDate.ToString("MM/yyyy") = PaymentDate.AddMonths(-1).ToString("MM/yyyy")
                                       Select x.BillingPeriod).FirstOrDefault
                        If .BillPeriod = 0 Then
                            Throw New Exception("Create Billing Period for " + PaymentDate.AddMonths(-2).ToString("MM/yyyy") + " - " + PaymentDate.AddMonths(-1).ToString("MM/yyyy") _
                                                + Environment.NewLine + "Please go to File Menu > Libraries > Calendar Billing Period.")
                        End If
                        .IDNumber = AMParticipantInfo
                        .ChargeType = oChargeType
                        .DueDate = PaymentDate
                        .BeginningBalance = FinalMonthlyAmortizationAmount
                        .EndingBalance = FinalMonthlyAmortizationAmount
                        .IDType = EnumIDType.P.ToString
                        .NewDueDate = PaymentDate
                        .SummaryType = EnumSummaryType.SPA
                        .INVDMCMNo = EnumSummaryType.SPA.ToString & "-" & dicSPANumber.Item(i).ToString("0000000")
                        .WESMBillSummaryNo = dicWBSNumber.Item(i)
                        .BalanceType = EnumBalanceType.AP
                        .SPANo = InitialSPANo
                    End With
                    SPAWESMBillSummaryAPListPerItem.Add(SPAWESMBillSummaryitem)
                    SPAWESMBillSummaryAPList.Add(SPAWESMBillSummaryitem)
                    Me.SPAWESMBillSummaryListAP.Add(SPAWESMBillSummaryitem)

                    Dim SPAWESMBillSummaryMonitoring As New SPAMonitoring
                    With SPAWESMBillSummaryMonitoring
                        .WESMBillSummaryInfo = SPAWESMBillSummaryitem
                        .MonthlyDueDate = PaymentDate
                        .MonthlyAmortization = FinalMonthlyAmortizationAmount
                        .PrincipalAmount = oPrincipalAmount
                        .InterestAmount = oInterestAmount
                        .BalanceAmount = TotalAPBalanceFinal 'TotalAPBalance                        
                    End With
                    SPAWESMBillSummaryMonitoringListPerItem.Add(SPAWESMBillSummaryMonitoring)
                    SPAWESMBillSummaryMonitoringList.Add(SPAWESMBillSummaryMonitoring)
                    objSPAMain.SPAMonitoringList.Add(SPAWESMBillSummaryMonitoring)
                Next
                Dim CreatedDMCM As Long = Me.CreateDMCMSetupAP(SPAWESMBillSummaryAPListPerItem, SPAWESMBillSummaryMonitoringListPerItem)
                For Each oitem In SPAWESMBillSummaryMonitoringListPerItem
                    Dim GetoSPAMain_oSPAMonItem = (From x In objSPAMain.SPAMonitoringList Where x.WESMBillSummaryInfo.WESMBillSummaryNo = oitem.WESMBillSummaryInfo.WESMBillSummaryNo Select x).FirstOrDefault
                    GetoSPAMain_oSPAMonItem.DMCMNumber = CreatedDMCM
                    oitem.DMCMNumber = CreatedDMCM
                Next
            End If
        Next
        Me.CreateSPAWESMBillSummaryAR(objSPAMain, ParticipantInfo, SPAWESMBillSummaryMonitoringList, SPAWESMBillSummaryAPList, oChargeType)

    End Sub

#End Region

#Region "Editing SPA"
    Public Sub EditSPA(ByVal FirsPayment As Date)
        Me._objSPAMain.FirstPaymentDate = FirsPayment
    End Sub
#End Region

#Region "GET SPA ARSPABillSummary DataTable"
    Public Function CreateSPAARDataTable(ByVal SPADate As Date) As DataTable
        Dim GetSPABillAsPerDate = (From x In Me.SPAWESMBillSummaryListAR Where x.DueDate = SPADate And x.BalanceType = EnumBalanceType.AR _
                                   Join y In objSPAMain.SPAMonitoringList On x.WESMBillSummaryNo Equals y.WESMBillSummaryInfo.WESMBillSummaryNo
                                   Select x, y.InterestAmount, y.PrincipalAmount, y.BalanceAmount, y.DMCMNumber, y.MonthlyAmortization).ToList()

        Dim SPAARDT As New DataTable
        SPAARDT.TableName = "AccountReceivables"
        With SPAARDT.Columns
            .Add("BillingPeriod", GetType(Long))
            .Add("IDNumber", GetType(String))
            .Add("ParticipantID", GetType(String))
            .Add("InvoiceNumber", GetType(String))
            .Add("DueDate", GetType(Date))
            .Add("NewDueDate", GetType(Date))
            .Add("Principal", GetType(String))
            .Add("Interest", GetType(String))
            .Add("MonthlyPayment", GetType(String))
            .Add("DebitCreditMemo", GetType(String))
            .Add("DMCMNo", GetType(Long))
        End With

        For Each ARItem In GetSPABillAsPerDate
            Dim row As DataRow
            row = SPAARDT.NewRow()
            row("BillingPeriod") = ARItem.x.BillPeriod
            row("IDNumber") = ARItem.x.IDNumber.IDNumber
            row("ParticipantID") = ARItem.x.IDNumber.ParticipantID
            row("InvoiceNumber") = ARItem.x.INVDMCMNo
            row("DueDate") = ARItem.x.DueDate
            row("NewDueDate") = ARItem.x.NewDueDate
            row("Principal") = FormatNumber(ARItem.PrincipalAmount, UseParensForNegativeNumbers:=TriState.True)
            row("Interest") = FormatNumber(ARItem.InterestAmount, UseParensForNegativeNumbers:=TriState.True)
            row("MonthlyPayment") = FormatNumber(ARItem.MonthlyAmortization, UseParensForNegativeNumbers:=TriState.True)
            row("DebitCreditMemo") = DMCMNumberPrefix.ToString & ARItem.DMCMNumber.ToString("0000000")
            row("DMCMNo") = ARItem.DMCMNumber
            SPAARDT.Rows.Add(row)
        Next
        SPAARDT.AcceptChanges()
        Return SPAARDT
    End Function
#End Region

#Region "GET SPA APSPABillSummary DataTable"
    Public Function CreateSPAAPDataTable(ByVal SPADate As Date) As DataTable
        Dim GetSPABillAsPerDate = (From x In Me.SPAWESMBillSummaryListAP Where x.DueDate = SPADate And x.BalanceType = EnumBalanceType.AP _
                                   Join y In objSPAMain.SPAMonitoringList On x.WESMBillSummaryNo Equals y.WESMBillSummaryInfo.WESMBillSummaryNo
                                   Select x, y.InterestAmount, y.PrincipalAmount, y.BalanceAmount, y.DMCMNumber, y.MonthlyAmortization, y.WESMBillSummaryInfo.WESMBillSummaryNo).ToList()

        Dim SPAAPDT As New DataTable
        SPAAPDT.TableName = "AccountPayables"
        With SPAAPDT.Columns
            .Add("BillingPeriod", GetType(Long))
            .Add("IDNumber", GetType(String))
            .Add("ParticipantID", GetType(String))
            .Add("InvoiceNumber", GetType(String))
            .Add("DueDate", GetType(Date))
            .Add("NewDueDate", GetType(Date))
            .Add("Principal", GetType(String))
            .Add("Interest", GetType(String))
            .Add("MonthlyPayment", GetType(String))
            .Add("DebitCreditMemo", GetType(String))
            .Add("DMCMNo", GetType(Long))
        End With

        For Each APItem In GetSPABillAsPerDate
            Dim row As DataRow
            row = SPAAPDT.NewRow()
            row("BillingPeriod") = APItem.x.BillPeriod
            row("IDNumber") = APItem.x.IDNumber.IDNumber
            row("ParticipantID") = APItem.x.IDNumber.ParticipantID
            row("InvoiceNumber") = APItem.x.INVDMCMNo
            row("DueDate") = APItem.x.DueDate
            row("NewDueDate") = APItem.x.NewDueDate
            row("Principal") = FormatNumber(APItem.PrincipalAmount, UseParensForNegativeNumbers:=TriState.True)
            row("Interest") = FormatNumber(APItem.InterestAmount, UseParensForNegativeNumbers:=TriState.True)
            row("MonthlyPayment") = FormatNumber(APItem.MonthlyAmortization, UseParensForNegativeNumbers:=TriState.True)
            row("DebitCreditMemo") = DMCMNumberPrefix.ToString & APItem.DMCMNumber.ToString("0000000")
            row("DMCMNo") = APItem.DMCMNumber

            SPAAPDT.Rows.Add(row)
        Next
        SPAAPDT.AcceptChanges()
        Return SPAAPDT
    End Function
#End Region

#Region "Get ARWESMBillSummary DataTable"
    Public Function CreateARDataTable(ByVal ARWESMBillSummary As List(Of WESMBillSummary), SPADetails As List(Of SPADetails)) As DataTable
        Dim ARDT As New DataTable
        Dim ARBillList = (From x In ARWESMBillSummary
                          Join y In SPADetails On y.WESMBillSummaryInfo.WESMBillSummaryNo Equals x.WESMBillSummaryNo
                          Select x, y).ToList()

        ARDT.TableName = "AccountPayables"
        With ARDT.Columns
            .Add("BillingPeriod", GetType(Long))
            .Add("IDNumber", GetType(String))
            .Add("ParticipantID", GetType(String))
            .Add("InvoiceNumber", GetType(String))
            .Add("NewDueDate", GetType(Date))
            .Add("OutstandingBalance", GetType(String))
            .Add("SPAAmount", GetType(String))
            .Add("NewOutstandingBalance", GetType(String))
            .Add("DebitCreditMemo", GetType(String))
            .Add("DMCMNo", GetType(Long))
        End With

        For Each ARItem In ARBillList
            Dim row As DataRow
            row = ARDT.NewRow()
            row("BillingPeriod") = ARItem.x.BillPeriod
            row("IDNumber") = ARItem.x.IDNumber.IDNumber
            row("ParticipantID") = ARItem.y.WESMBillSummaryInfo.IDNumber.ParticipantID
            row("InvoiceNumber") = ARItem.x.INVDMCMNo
            row("NewDueDate") = ARItem.x.NewDueDate
            row("OutstandingBalance") = FormatNumber(ARItem.x.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
            row("SPAAmount") = FormatNumber(ARItem.y.BalanceAmount, UseParensForNegativeNumbers:=TriState.True)
            row("NewOutstandingBalance") = FormatNumber(ARItem.x.EndingBalance - ARItem.y.BalanceAmount, UseParensForNegativeNumbers:=TriState.True)
            row("DebitCreditMemo") = DMCMNumberPrefix.ToString & ARItem.y.DMCMNumber.ToString("0000000")
            row("DMCMNo") = ARItem.y.DMCMNumber
            ARDT.Rows.Add(row)
        Next
        ARDT.AcceptChanges()
        Return ARDT
    End Function

    Public Function CreateViewARDataTable(ByVal ARWESMBillSummary As List(Of WESMBillSummary), SPADetails As List(Of SPADetails)) As DataTable
        Dim ARDT As New DataTable
        Dim ARBillList = (From x In ARWESMBillSummary
                          Join y In SPADetails On y.WESMBillSummaryInfo.WESMBillSummaryNo Equals x.WESMBillSummaryNo
                          Select x, y).ToList()

        ARDT.TableName = "AccountPayables"
        With ARDT.Columns
            .Add("BillingPeriod", GetType(Long))
            .Add("IDNumber", GetType(String))
            .Add("ParticipantID", GetType(String))
            .Add("InvoiceNumber", GetType(String))
            .Add("NewDueDate", GetType(Date))
            .Add("OutstandingBalance", GetType(String))
            .Add("SPAAmount", GetType(String))
            .Add("NewOutstandingBalance", GetType(String))
            .Add("DebitCreditMemo", GetType(String))
            .Add("DMCMNo", GetType(Long))
        End With

        For Each ARItem In ARBillList
            Dim row As DataRow
            row = ARDT.NewRow()
            row("BillingPeriod") = ARItem.x.BillPeriod
            row("IDNumber") = ARItem.x.IDNumber.IDNumber
            row("ParticipantID") = ARItem.y.WESMBillSummaryInfo.IDNumber.ParticipantID
            row("InvoiceNumber") = ARItem.x.INVDMCMNo
            row("NewDueDate") = ARItem.x.NewDueDate
            row("OutstandingBalance") = FormatNumber(ARItem.y.BalanceAmount, UseParensForNegativeNumbers:=TriState.True)
            row("SPAAmount") = FormatNumber(ARItem.y.BalanceAmount, UseParensForNegativeNumbers:=TriState.True)
            row("NewOutstandingBalance") = FormatNumber(ARItem.x.EndingBalance - ARItem.y.BalanceAmount, UseParensForNegativeNumbers:=TriState.True)
            row("DebitCreditMemo") = DMCMNumberPrefix.ToString & ARItem.y.DMCMNumber.ToString("0000000")
            row("DMCMNo") = ARItem.y.DMCMNumber
            ARDT.Rows.Add(row)
        Next
        ARDT.AcceptChanges()
        Return ARDT
    End Function
#End Region

#Region "Get AP WESMBillSummary Selected DataTable"
    Public Function CreateAPDataTable(ByVal APWESMBillSummary As List(Of WESMBillSummary), ByVal SPADetails As List(Of SPADetails)) As DataTable
        Dim APDT As New DataTable
        Dim APBillList = (From x In APWESMBillSummary
                          Join y In SPADetails On y.WESMBillSummaryInfo.WESMBillSummaryNo Equals x.WESMBillSummaryNo
                          Select x, y).ToList()

        APDT.TableName = "AccountPayables"
        With APDT.Columns
            .Add("BillingPeriod", GetType(Long))
            .Add("IDNumber", GetType(String))
            .Add("ParticipantID", GetType(String))
            .Add("InvoiceNumber", GetType(String))
            '.Add("DueDate", GetType(Date))
            .Add("NewDueDate", GetType(Date))
            .Add("OutstandingBalance", GetType(String))
            .Add("SPAAmount", GetType(String))
            .Add("NewOutstandingBalance", GetType(String))
            .Add("DebitCreditMemo", GetType(String))
            .Add("DMCMNo", GetType(Long))
        End With
        For Each APitem In APBillList
            Dim row As DataRow
            row = APDT.NewRow()
            row("BillingPeriod") = APitem.x.BillPeriod
            row("IDNumber") = APitem.x.IDNumber.IDNumber
            row("ParticipantID") = APitem.y.WESMBillSummaryInfo.IDNumber.ParticipantID
            row("InvoiceNumber") = APitem.x.INVDMCMNo
            row("NewDueDate") = APitem.x.NewDueDate
            row("OutstandingBalance") = FormatNumber(APitem.x.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
            row("SPAAmount") = FormatNumber(APitem.y.BalanceAmount, UseParensForNegativeNumbers:=TriState.True)
            row("NewOutstandingBalance") = FormatNumber(APitem.x.EndingBalance - APitem.y.BalanceAmount, UseParensForNegativeNumbers:=TriState.True)
            row("DebitCreditMemo") = DMCMNumberPrefix.ToString & APitem.y.DMCMNumber.ToString("0000000")
            row("DMCMNo") = APitem.y.DMCMNumber
            APDT.Rows.Add(row)
        Next
        APDT.AcceptChanges()
        Return APDT
    End Function

    Public Function CreateViewAPDataTable(ByVal APWESMBillSummary As List(Of WESMBillSummary), ByVal SPADetails As List(Of SPADetails)) As DataTable
        Dim APDT As New DataTable
        Dim APBillList = (From x In APWESMBillSummary
                          Join y In SPADetails On y.WESMBillSummaryInfo.WESMBillSummaryNo Equals x.WESMBillSummaryNo
                          Select x, y).ToList()

        APDT.TableName = "AccountPayables"
        With APDT.Columns
            .Add("BillingPeriod", GetType(Long))
            .Add("IDNumber", GetType(String))
            .Add("ParticipantID", GetType(String))
            .Add("InvoiceNumber", GetType(String))
            '.Add("DueDate", GetType(Date))
            .Add("NewDueDate", GetType(Date))
            .Add("OutstandingBalance", GetType(String))
            .Add("SPAAmount", GetType(String))
            .Add("NewOutstandingBalance", GetType(String))
            .Add("DebitCreditMemo", GetType(String))
            .Add("DMCMNo", GetType(Long))
        End With
        For Each APitem In APBillList
            Dim row As DataRow
            row = APDT.NewRow()
            row("BillingPeriod") = APitem.x.BillPeriod
            row("IDNumber") = APitem.x.IDNumber.IDNumber
            row("ParticipantID") = APitem.y.WESMBillSummaryInfo.IDNumber.ParticipantID
            row("InvoiceNumber") = APitem.x.INVDMCMNo
            row("NewDueDate") = APitem.x.NewDueDate
            row("OutstandingBalance") = FormatNumber(APitem.y.BalanceAmount, UseParensForNegativeNumbers:=TriState.True)
            row("SPAAmount") = FormatNumber(APitem.y.BalanceAmount, UseParensForNegativeNumbers:=TriState.True)
            row("NewOutstandingBalance") = FormatNumber(APitem.x.EndingBalance - APitem.y.BalanceAmount, UseParensForNegativeNumbers:=TriState.True)
            row("DebitCreditMemo") = DMCMNumberPrefix.ToString & APitem.y.DMCMNumber.ToString("0000000")
            row("DMCMNo") = APitem.y.DMCMNumber
            APDT.Rows.Add(row)
        Next
        APDT.AcceptChanges()
        Return APDT
    End Function
#End Region

#Region "Create DMCM Closing AR WESMBillSummary Invoice"
    Private Function CreateDMCMClosingAR(ByVal objSPADetails As SPADetails) As Long
        Dim ret As New Long
        Dim oDMCM As New DebitCreditMemo
        Dim GetWESMBillItem As WESMBillSummary = (From x In ListOfWESMBillSummaryAR Where x.WESMBillSummaryNo = objSPADetails.WESMBillSummaryInfo.WESMBillSummaryNo Select x).FirstOrDefault
        Me.InitialDMCMNo += 1
        If Not GetWESMBillItem Is Nothing Then
            With oDMCM
                .BillingPeriod = GetWESMBillItem.BillPeriod
                .DueDate = GetWESMBillItem.DueDate
                .IDNumber = GetWESMBillItem.IDNumber.IDNumber
                .DMCMNumber = InitialDMCMNo
                .JVNumber = InitialJVNo
                .VATExempt = Math.Abs(objSPADetails.BalanceAmount)
                .TotalAmountDue = Math.Abs(objSPADetails.BalanceAmount)
                .Particulars = "To close AR invoice as entered into Special Payment Agreement by " & objSPAMain.ParticipantInfo.ParticipantID
                .ChargeType = objSPADetails.ChargeType
                .TransType = EnumDMCMTransactionType.SPAClosingAccountReceivable
                .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.ClearingAccountCode, Math.Abs(objSPADetails.BalanceAmount), 0, GetWESMBillItem.INVDMCMNo, EnumSummaryType.SPA, GetWESMBillItem.IDNumber, EnumDMCMComputed.Compute))
                .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.CreditCode, 0, Math.Abs(objSPADetails.BalanceAmount), GetWESMBillItem.INVDMCMNo, EnumSummaryType.SPA, GetWESMBillItem.IDNumber, EnumDMCMComputed.Compute))
                .UpdatedBy = AMModule.UserName
                .PreparedBy = AMModule.FullName
                .CheckedBy = DMCMSignatories.Signatory_1
                .ApprovedBy = DMCMSignatories.Signatory_2
            End With
            Me._DMCMListForSPA.Add(oDMCM)
        End If
        ret = oDMCM.DMCMNumber
        Return ret
    End Function
#End Region

#Region "Create DMCM Setup AR SPA Invoice"
    Private Function CreateDMCMSetupAR(ByVal objSPABill As List(Of WESMBillSummary), ByVal objSPAMon As List(Of SPAMonitoring)) As Long
        Dim ret As New Long
        Dim oDMCM As New DebitCreditMemo
        Dim GetSPABillList = (From x In objSPABill _
                              Join y In objSPAMon On y.WESMBillSummaryInfo.WESMBillSummaryNo Equals x.WESMBillSummaryNo _
                              Select x).ToList

        Dim SPABillInfo = (From x In GetSPABillList Select x).FirstOrDefault

        Dim GetTotalAmount As Decimal = (From x In objSPABill Select x.BeginningBalance).Sum()
        Me.InitialDMCMNo += 1
        If Not SPABillInfo Is Nothing Then
            With oDMCM
                .BillingPeriod = SPABillInfo.BillPeriod
                .DueDate = SPABillInfo.DueDate
                .IDNumber = SPABillInfo.IDNumber.IDNumber
                .DMCMNumber = InitialDMCMNo
                .JVNumber = InitialJVNo
                .VATExempt = Math.Abs(GetTotalAmount)
                .TotalAmountDue = Math.Abs(GetTotalAmount)
                .Particulars = "To setup AR invoice as entered into Special Payment Agreement by " & objSPAMain.ParticipantInfo.ParticipantID
                .ChargeType = GetSPABillList.First.ChargeType
                .TransType = EnumDMCMTransactionType.SPASetupAccountReceivable
                .UpdatedBy = AMModule.UserName
                .PreparedBy = AMModule.FullName
                .CheckedBy = DMCMSignatories.Signatory_1
                .ApprovedBy = DMCMSignatories.Signatory_2
                For Each item In GetSPABillList
                    .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.CreditCode, Math.Abs(item.BeginningBalance), 0, item.INVDMCMNo, EnumSummaryType.SPA, SPABillInfo.IDNumber, EnumDMCMComputed.Compute))
                Next
                .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.ClearingAccountCode, 0, Math.Abs(GetTotalAmount), "", EnumSummaryType.SPA, SPABillInfo.IDNumber, EnumDMCMComputed.Compute))
            End With
            Me._DMCMListForSPA.Add(oDMCM)
        End If
        ret = oDMCM.DMCMNumber
        Return ret
    End Function
#End Region

#Region "Create DMCM Closing AP WESMBillSummary Invoice"
    Private Function CreateDMCMClosingAP(ByVal objSPADetails As SPADetails) As Long
        Dim ret As New Long
        Dim oDMCM As New DebitCreditMemo
        Dim GetWESMBillItem As WESMBillSummary = (From x In Me.ListOfWESMBillSummaryAP Where x.WESMBillSummaryNo = objSPADetails.WESMBillSummaryInfo.WESMBillSummaryNo Select x).FirstOrDefault
        Me.InitialDMCMNo += 1
        If Not GetWESMBillItem Is Nothing Then
            With oDMCM
                .BillingPeriod = GetWESMBillItem.BillPeriod
                .DueDate = GetWESMBillItem.DueDate
                .IDNumber = GetWESMBillItem.IDNumber.IDNumber
                .DMCMNumber = InitialDMCMNo
                .JVNumber = InitialJVNo
                .VATExempt = Math.Abs(objSPADetails.BalanceAmount)
                .TotalAmountDue = Math.Abs(objSPADetails.BalanceAmount)
                .Particulars = "To close AP invoice as entered into Special Payment Agreement by " & objSPAMain.ParticipantInfo.ParticipantID
                .ChargeType = objSPADetails.ChargeType
                .TransType = EnumDMCMTransactionType.SPAClosingAccountPayable
                .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.DebitCode, Math.Abs(objSPADetails.BalanceAmount), 0, GetWESMBillItem.INVDMCMNo, EnumSummaryType.SPA, GetWESMBillItem.IDNumber, EnumDMCMComputed.Compute))
                .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.ClearingAccountCode, 0, Math.Abs(objSPADetails.BalanceAmount), GetWESMBillItem.INVDMCMNo, EnumSummaryType.SPA, GetWESMBillItem.IDNumber, EnumDMCMComputed.Compute))
                .UpdatedBy = AMModule.UserName
                .PreparedBy = AMModule.FullName
                .CheckedBy = DMCMSignatories.Signatory_1
                .ApprovedBy = DMCMSignatories.Signatory_2
            End With
            Me._DMCMListForSPA.Add(oDMCM)
        End If
        ret = oDMCM.DMCMNumber
        Return ret
    End Function
#End Region

#Region "Create DMCM Setup AP SPA Invoice"
    Private Function CreateDMCMSetupAP(ByVal objSPABill As List(Of WESMBillSummary), ByVal objSPAMon As List(Of SPAMonitoring)) As Long
        Dim ret As New Long
        Dim oDMCM As New DebitCreditMemo
        Dim GetSPABillList = (From x In objSPABill _
                              Join y In objSPAMon On y.WESMBillSummaryInfo.WESMBillSummaryNo Equals x.WESMBillSummaryNo _
                              Select x Order By x.WESMBillSummaryNo Descending).ToList

        Dim SPABillInfo = (From x In GetSPABillList Select x).FirstOrDefault

        Dim GetTotalAmount As Decimal = (From x In objSPABill Select x.BeginningBalance).Sum()
        Me.InitialDMCMNo += 1
        If Not SPABillInfo Is Nothing Then
            With oDMCM
                .BillingPeriod = SPABillInfo.BillPeriod
                .DueDate = SPABillInfo.DueDate
                .IDNumber = SPABillInfo.IDNumber.IDNumber
                .DMCMNumber = InitialDMCMNo
                .JVNumber = InitialJVNo
                .VATExempt = Math.Abs(GetTotalAmount)
                .TotalAmountDue = Math.Abs(GetTotalAmount)
                .Particulars = "To setup AP invoice as entered into Special Payment Agreement by " & objSPAMain.ParticipantInfo.ParticipantID
                .ChargeType = SPABillInfo.ChargeType
                .TransType = EnumDMCMTransactionType.SPASetupAccountPayable
                .UpdatedBy = AMModule.UserName
                .PreparedBy = AMModule.FullName
                .CheckedBy = DMCMSignatories.Signatory_1
                .ApprovedBy = DMCMSignatories.Signatory_2
                .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.ClearingAccountCode, Math.Abs(GetTotalAmount), 0, "", EnumSummaryType.SPA, SPABillInfo.IDNumber, EnumDMCMComputed.Compute))
                For Each item In GetSPABillList
                    .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.DebitCode, 0, Math.Abs(item.BeginningBalance), item.INVDMCMNo, EnumSummaryType.SPA, SPABillInfo.IDNumber, EnumDMCMComputed.Compute))
                Next
            End With
            Me._DMCMListForSPA.Add(oDMCM)
        End If
        ret = oDMCM.DMCMNumber
        Return ret
    End Function
#End Region

#Region "Create JV Closing for WESMBillSummary Invoices"
    Private Function GenerateJVClosing(ByVal TotalSPAAmountAR As Decimal, ByVal TotalSPAAmountAP As Decimal) As JournalVoucher
        Dim JVSPA As New JournalVoucher
        Me.InitialJVNo += 1
        With JVSPA
            .JVDate = SystemDate
            .JVNumber = Me.InitialJVNo
            .PostedType = EnumPostedType.SPAC.ToString
            .Status = EnumPostedTypeStatus.NotPosted
            .BatchCode = EnumPostedType.SPAC.ToString & "-" & Me.InitialJVNo.ToString()
            .Remarks = "Total Amount for closing WESMBillSummary Invoices."
            .JVDetails.Add(New JournalVoucherDetails(.JVNumber, ClearingAccountCode, Math.Abs(TotalSPAAmountAR), 0))
            .JVDetails.Add(New JournalVoucherDetails(.JVNumber, CreditCode, 0, Math.Abs(TotalSPAAmountAR)))
            .JVDetails.Add(New JournalVoucherDetails(.JVNumber, DebitCode, Math.Abs(TotalSPAAmountAP), 0))
            .JVDetails.Add(New JournalVoucherDetails(.JVNumber, ClearingAccountCode, 0, Math.Abs(TotalSPAAmountAP)))
            .UpdatedBy = AMModule.UserName
            .PreparedBy = AMModule.FullName
            .CheckedBy = JVSignatories.Signatory_1
            .ApprovedBy = JVSignatories.Signatory_2
        End With

        Dim WESMBillGPPostedItem As New WESMBillGPPosted

        With WESMBillGPPostedItem
            .Remarks = JVSPA.Remarks
            .Posted = 0
            .BatchCode = JVSPA.BatchCode
            .PostType = JVSPA.PostedType
            .DocumentAmount = Math.Round((From x In JVSPA.JVDetails Select x.Debit).Sum, 2)
            .JVNumber = JVSPA.JVNumber
        End With

        Me._GPPosted.Add(WESMBillGPPostedItem)
        Return JVSPA
    End Function
#End Region

#Region "Generate JV Setup for SPA invoices"
    Private Function GenerateJVSetupForSPA(ByVal TotalSPAAmountAR As Decimal, ByVal TotalSPAAmountAP As Decimal) As JournalVoucher
        Dim JVClosing As New JournalVoucher
        Me.InitialJVNo += 1
        With JVClosing
            .JVDate = SystemDate
            .JVNumber = Me.InitialJVNo
            .PostedType = EnumPostedType.SPASU.ToString
            .Status = EnumPostedTypeStatus.NotPosted
            .BatchCode = EnumPostedType.SPASU.ToString & "-" & Me.InitialJVNo.ToString()
            .Remarks = "Total Amount for Set-up Special Payment Agreement invoices."
            .JVDetails.Add(New JournalVoucherDetails(.JVNumber, CreditCode, Math.Abs(TotalSPAAmountAR), 0))
            .JVDetails.Add(New JournalVoucherDetails(.JVNumber, ClearingAccountCode, 0, Math.Abs(TotalSPAAmountAR)))
            .JVDetails.Add(New JournalVoucherDetails(.JVNumber, ClearingAccountCode, Math.Abs(TotalSPAAmountAP), 0))
            .JVDetails.Add(New JournalVoucherDetails(.JVNumber, DebitCode, 0, Math.Abs(TotalSPAAmountAP)))
            .UpdatedBy = AMModule.UserName
            .PreparedBy = AMModule.FullName
            .CheckedBy = JVSignatories.Signatory_1
            .ApprovedBy = JVSignatories.Signatory_2
        End With

        Dim WESMBillGPPostedItem As New WESMBillGPPosted

        With WESMBillGPPostedItem
            .Remarks = JVClosing.Remarks
            .Posted = 0
            .BatchCode = JVClosing.BatchCode
            .PostType = JVClosing.PostedType
            .DocumentAmount = Math.Round((From x In JVClosing.JVDetails Select x.Debit).Sum, 2)
            .JVNumber = JVClosing.JVNumber
        End With

        Me._GPPosted.Add(WESMBillGPPostedItem)
        Return JVClosing
    End Function
#End Region

#Region "Generate DMCM Report"
    Public Function GenerateDMCM(ByVal DT As DataTable, ByVal IDnumber As String, ByVal DMCMNumber As Long) As DataTable
        Dim ret As New DataTable
        Dim _DMCM As DebitCreditMemo = (From x In Me.DMCMListForSPA Where x.DMCMNumber = DMCMNumber Select x).FirstOrDefault
        Dim Signatory As DocSignatories = WBillHelper.GetSignatories("DMCM").First
        Dim ListofAccountingCodes As List(Of AccountingCode) = WBillHelper.GetAccountingCodes()
        Dim _ParticipantInfo As AMParticipants = Me.WBillHelper.GetAMParticipants(IDnumber).FirstOrDefault

        If Not _DMCM Is Nothing Then
            With _DMCM
                For Each Item In .DMCMDetails
                    Dim _Description As String = (From x In ListofAccountingCodes Where x.AccountCode = Item.AccountCode Select x.Description).FirstOrDefault
                    Dim row As DataRow = DT.NewRow()
                    row("ID_NUMBER") = _ParticipantInfo.IDNumber & " / " & _ParticipantInfo.ParticipantID
                    row("BUSINESS_STYLE") = _ParticipantInfo.BusinessStyle
                    row("ADDRESS") = _ParticipantInfo.ParticipantAddress
                    row("PARTICIPANT_TIN") = _ParticipantInfo.TIN
                    row("DMCM_NO") = BFactory.GenerateBIRDocumentNumber(.DMCMNumber, BIRDocumentsType.DMCM)
                    row("JV_NO") = BFactory.GenerateBIRDocumentNumber(.JVNumber, BIRDocumentsType.JournalVoucher)
                    row("PARTICULARS") = .Particulars
                    row("ACCOUNT_CODE") = Item.AccountCode
                    If Item.InvDMCMNo = "" Then
                        row("DESCRIPTION") = _Description
                    Else
                        row("DESCRIPTION") = _Description & "(" & Item.InvDMCMNo & ")"
                    End If

                    row("PREPARED_BY") = AMModule.FullName
                    row("CHECKED_BY") = Signatory.Signatory_1
                    row("APPROVED_BY") = Signatory.Signatory_2
                    row("POSITION1") = AMModule.Position
                    row("POSITION2") = Signatory.Position_1
                    row("POSITION3") = Signatory.Position_2
                    row("PARTICIPANT_NAME") = _ParticipantInfo.FullName
                    row("DR_AMOUNT") = Item.Debit
                    row("CR_AMOUNT") = Item.Credit
                    row("PARTICIPANT_ID") = _ParticipantInfo.ParticipantID
                    row("EWT") = .EWT
                    row("EWV") = .EWV
                    row("VATABLE") = .Vatable
                    row("VAT") = .VAT
                    row("VAT_EXEMPT_SALE") = .VATExempt
                    row("VAT_ZERO") = .VatZeroRated
                    row("OTHERS") = .Others
                    row("TOTAL_AMOUNT_DUE") = .TotalAmountDue
                    row("BIR_VALUE") = AMModule.BIRPermitNumber
                    DT.Rows.Add(row)
                Next
            End With
        End If
        ret = DT
        Return ret
    End Function
#End Region

#Region "Function For Generation of Journal Voucher From Collection"

    Public Function GenerateJVClosingReport(ByVal IntJV As Integer) As DataSet
        Dim ret As New DataSet
        Dim objJV As New JournalVoucher
        Dim Accountingcode = WBillHelper.GetAccountingCodes()
        Dim JV_Signatories = WBillHelper.GetSignatories("JV").First
        Dim AllGPPosted = WBillHelper.GetWESMBillGPPosted()
        Dim JournalVoucherTable As New DataTable

        If IntJV = 1 Then
            objJV = objSPAMain.JVClosing
        Else
            objJV = objSPAMain.JVSetup
        End If
        JournalVoucherTable.TableName = "JournalVoucher"
        With JournalVoucherTable.Columns
            .Add("JV_NO", GetType(String))
            .Add("JV_DATE", GetType(Date))
            .Add("BATCHCODE", GetType(String))
            .Add("OFFSETNUMBER", GetType(String))
            .Add("STATUS", GetType(Integer))
            .Add("PREPAREDBY", GetType(String))
            .Add("CHECKEDBY", GetType(String))
            .Add("APPROVEDBY", GetType(String))
            .Add("UPDATEDBY", GetType(String))
            .Add("UPDATEDDATE", GetType(Date))
            .Add("REMARKS", GetType(String))
            .Add("GPREF_NO", GetType(String))
            .Add("BIRPermit", GetType(String))
        End With

        Dim dr As DataRow
        dr = JournalVoucherTable.NewRow
        With objJV
            dr("JV_NO") = BFactory.GenerateBIRDocumentNumber(.JVNumber, BIRDocumentsType.JournalVoucher)
            dr("JV_DATE") = .JVDate
            dr("BATCHCODE") = .BatchCode.ToString
            'dr("OFFSETNUMBER") = .OffsetNumber.ToString
            dr("STATUS") = .Status
            dr("PREPAREDBY") = .PreparedBy
            dr("CHECKEDBY") = JV_Signatories.Signatory_1
            dr("APPROVEDBY") = JV_Signatories.Signatory_2
            dr("UPDATEDBY") = AMModule.UserName
            dr("UPDATEDDATE") = AMModule.SystemDate
            dr("GPREF_NO") = .GPRefNo
            dr("REMARKS") = .Remarks
        End With
        JournalVoucherTable.Rows.Add(dr)

        Dim JournalVoucherDetailsTable As New DataTable
        JournalVoucherDetailsTable.TableName = "JournalVoucherDetails"
        With JournalVoucherDetailsTable.Columns
            .Add("JV_NO", GetType(String))
            .Add("ACCOUNTCODE", GetType(String))
            .Add("CREDIT", GetType(Decimal))
            .Add("DEBIT", GetType(Decimal))
            .Add("UPDATEDBY", GetType(String))
            .Add("UPDATEDDATE", GetType(Date))
        End With

        For Each rowJVDetails In objJV.JVDetails
            Dim jvRow As DataRow
            jvRow = JournalVoucherDetailsTable.NewRow
            With rowJVDetails
                jvRow("JV_NO") = Me.BFactory.GenerateBIRDocumentNumber(.JVNumber, BIRDocumentsType.JournalVoucher)
                jvRow("ACCOUNTCODE") = .AccountCode
                jvRow("CREDIT") = .Credit
                jvRow("DEBIT") = .Debit
                jvRow("UPDATEDBY") = .UpdatedBy
                jvRow("UPDATEDDATE") = .UpdatedDate
            End With
            JournalVoucherDetailsTable.Rows.Add(jvRow)
        Next

        Dim AccountCodesTable As New DataTable
        AccountCodesTable.TableName = "AccountingCode"
        With AccountCodesTable.Columns
            .Add("ACCT_CODE", GetType(String))
            .Add("DESCRIPTION", GetType(String))
        End With

        For Each acctCodes In Accountingcode
            Dim acRow As DataRow
            acRow = AccountCodesTable.NewRow
            With acRow
                acRow("ACCT_CODE") = acctCodes.AccountCode
                acRow("DESCRIPTION") = acctCodes.Description
            End With
            AccountCodesTable.Rows.Add(acRow)
        Next

        ret.Tables.Add(JournalVoucherTable)
        ret.Tables.Add(JournalVoucherDetailsTable)
        ret.Tables.Add(AccountCodesTable)

        Return ret
    End Function
#End Region

#Region "Generate Payment Schedule Summary"
    Public Function GeneratePaymentScheduleSummary(ByVal SPAMainDT As DataTable, ByVal SPADetailsDT As DataTable) As DataSet
        Dim ret As New DataSet

        Dim GetListOfParticipantAR As List(Of AMParticipants) = (From x In Me.SPAWESMBillSummaryListAR Select x.IDNumber).Distinct.ToList()
        Dim GetListOfParticipantAP As List(Of AMParticipants) = (From x In Me.SPAWESMBillSummaryListAP Select x.IDNumber).Distinct.ToList()

        Dim GetDistinctListofParticipant As List(Of AMParticipants) = GetListOfParticipantAR.Union(GetListOfParticipantAP).Distinct(New MyComparer()).ToList

        For Each item In GetDistinctListofParticipant
            Dim UnionSPABillSummary As List(Of WESMBillSummary) = (From x In Me.SPAWESMBillSummaryListAR Select x).Union _
                                                                  (From x In Me.SPAWESMBillSummaryListAP Select x).ToList()

            Dim SPAWESMBillSummary = (From x In UnionSPABillSummary Where x.IDNumber.IDNumber = item.IDNumber
                                      Join y In Me.objSPAMain.SPAMonitoringList On y.WESMBillSummaryInfo.WESMBillSummaryNo Equals x.WESMBillSummaryNo
                                      Select x.IDNumber, x.DueDate, x.BeginningBalance, y.PrincipalAmount, y.InterestAmount, y.BalanceAmount Order By DueDate).ToList()

            Dim SumOfPrincipalAmount As Decimal = Math.Round((From x In SPAWESMBillSummary Select x.PrincipalAmount).Sum, 2)
            Dim SumOfInterestAmount As Decimal = Math.Round((From x In SPAWESMBillSummary Select x.InterestAmount).Sum, 2)
            Dim SumOfTotalAmount As Decimal = Math.Round((From x In SPAWESMBillSummary Select x.BeginningBalance).Sum, 2)

            Dim row As DataRow
            row = SPAMainDT.NewRow()
            row("ID_NUMBER") = item.IDNumber
            row("PARTICIPANT_ID") = item.ParticipantID
            row("OUTSTANDING_AMOUNT") = SumOfPrincipalAmount            
            row("ANNUAL_INTEREST_RATE") = objSPAMain.InterestRate
            row("INMONTHS") = objSPAMain.InMonths
            row("FIRST_PAYMENT_DATE") = objSPAMain.FirstPaymentDate
            row("MONTHLY_INTEREST_RATE") = Math.Round(objSPAMain.InterestRate / 12, 2)
            row("TOTAL_BALANCE") = SumOfPrincipalAmount
            SPAMainDT.Rows.Add(row)

            Dim MonthlyAmor As Decimal = 0
            For Each oitem In SPAWESMBillSummary
                Dim orow As DataRow
                MonthlyAmor += (oitem.PrincipalAmount + oitem.InterestAmount)
                orow = SPADetailsDT.NewRow()
                orow("ID_NUMBER") = oitem.IDNumber.IDNumber
                orow("DUE_DATE") = oitem.DueDate
                orow("MONTHLY_AMORTIZATION") = oitem.BeginningBalance
                orow("PRINCIPAL_AMOUNT") = oitem.PrincipalAmount
                orow("INTEREST_AMOUNT") = oitem.InterestAmount
                orow("BALANCE") = oitem.BalanceAmount
                SPADetailsDT.Rows.Add(orow)
            Next
            SPADetailsDT.AcceptChanges()
        Next
        SPAMainDT.AcceptChanges()
        ret.Tables.Add(SPAMainDT)
        ret.Tables.Add(SPADetailsDT)
        ret.AcceptChanges()

        Return ret
    End Function
#End Region

#Region "ComputeShareAmount"
    Private Function ComputeAllocation(ByVal OutstandingBalance As Decimal, ByVal TotalOutstandingBalance As Decimal, ByVal TotalAmount As Decimal) As Decimal
        Dim returnAmntAlloc As Decimal
        If OutstandingBalance <> 0 Then
            returnAmntAlloc = (OutstandingBalance / TotalOutstandingBalance) * TotalAmount
        Else
            returnAmntAlloc = 0
        End If
        returnAmntAlloc = Math.Round(returnAmntAlloc, 2)
        Return returnAmntAlloc
    End Function
#End Region

#Region "Saving"
    Public Sub SPASaveUpdateToDB()
        Try
            Dim report As New DataReport
            Dim ListOfSQL As List(Of String) = Me.SPACreateQuery()
            report = Me.DataAccess.ExecuteSaveQuery(ListOfSQL, New DataSet)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub

    Public Sub SPASaveUpdatedDueDateToDB(ByVal ListofSelectedDueDate As Dictionary(Of Date, List(Of Long)))
        Try
            Dim report As New DataReport
            Dim ListOfSQL As List(Of String) = Me.SPAUpdateDueDate(ListofSelectedDueDate)
            report = Me.DataAccess.ExecuteSaveQuery(ListOfSQL, New DataSet)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub

    Public Function SPAUpdateDueDate(ByVal DicSelectedDueDate As Dictionary(Of Date, List(Of Long))) As List(Of String)
        Try
            Dim ListOfSQL As New List(Of String)
            Dim SQL As String = ""
            Dim SysDate As Date = WBillHelper.GetSystemDate()
            Dim SysDateTime As Date = WBillHelper.GetSystemDateTime()
            Dim UpdatedBy As String = AMModule.UserName
            Dim GetFirstPaymentOnSPA = (From x In objSPAMain.SPAMonitoringList Select x.WESMBillSummaryInfo.DueDate Order By DueDate).FirstOrDefault

            If objSPAMain.FirstPaymentDate.Month = GetFirstPaymentOnSPA.Month And
                objSPAMain.FirstPaymentDate.Year = GetFirstPaymentOnSPA.Year Then
                SQL = "UPDATE AM_SPA_MAIN A SET A.FIRST_PAYMENT_DATE = TO_DATE('" & GetFirstPaymentOnSPA & "', 'mm/dd/yyyy'), UPDATED_DATE = TO_DATE('" & SysDateTime & "','MM/dd/yyyyy hh24:mi:ss') & " & _
                 "WHERE A.SPA_NO =" & objSPAMain.SPANo
                ListOfSQL.Add(SQL)
            End If

            For Each item In DicSelectedDueDate.Keys
                Dim GetListOfWESMBillSummaryNo As List(Of Long) = DicSelectedDueDate(item)
                Dim GetListofSPAMon As List(Of SPAMonitoring) = (From x In objSPAMain.SPAMonitoringList
                                                                    Where GetListOfWESMBillSummaryNo.Contains(x.WESMBillSummaryInfo.WESMBillSummaryNo)
                                                                    Select x).ToList()
                For Each oItem In GetListofSPAMon
                    'AM_WESM_BILL update query
                    SQL = "UPDATE AM_WESM_BILL A SET A.DUE_DATE = TO_DATE('" & oItem.WESMBillSummaryInfo.DueDate & "','mm/dd/yyyy'), " & _
                                                    "A.UPDATED_DATE = TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), A.UPDATED_BY = '" & UpdatedBy & "' " & _
                            "WHERE A.INVOICE_NO = '" & oItem.WESMBillSummaryInfo.INVDMCMNo & "' AND A.CHARGE_TYPE = '" & oItem.WESMBillSummaryInfo.ChargeType.ToString() & "'"
                    ListOfSQL.Add(SQL)

                    'AM_WESM_BILL_SUMMARY Update query
                    SQL = "UPDATE AM_WESM_BILL_SUMMARY A SET A.DUE_DATE = TO_DATE('" & oItem.WESMBillSummaryInfo.DueDate & "','mm/dd/yyyy'), " & _
                                                            "A.NEW_DUEDATE = TO_DATE('" & oItem.WESMBillSummaryInfo.NewDueDate & "','mm/dd/yyyy'), " & _
                                                            "A.UPDATED_DATE = TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), A.UPDATED_BY = '" & UpdatedBy & "' " & _
                         "WHERE A.WESMBILL_SUMMARY_NO = " & oItem.WESMBillSummaryInfo.WESMBillSummaryNo
                    ListOfSQL.Add(SQL)
                Next
            Next

            Return ListOfSQL
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Function

    Public Sub SPASaveAddToDB()
        Try
            Dim report As New DataReport
            Dim ListOfSQL As List(Of String) = Me.SPACreateQuery()
            report = Me.DataAccess.ExecuteSaveQuery(ListOfSQL, New DataSet)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            ListOfSQL = CreateSQLStatementNewForSeq(dicListofSeq)
            report = Me.DataAccess.ExecuteSaveQuery(ListOfSQL, New DataSet)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            ListOfSQL = Nothing
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub

    Public Function SPACreateQuery() As List(Of String)
        Try
            Dim ListOfSQL As New List(Of String)
            Dim SQL As String = ""
            Dim SysDate As Date = WBillHelper.GetSystemDate()
            Dim SysDateTime As Date = WBillHelper.GetSystemDateTime()
            Dim UpdatedBy As String = AMModule.UserName
            Dim SPANo As Long
            Dim JVNoClosing As Long
            Dim JVBatchNoClosing As Long
            Dim JVNoSetup As Long
            Dim JVBatchNoSetup As Long

            Dim dicJVNo As New Dictionary(Of String, Long)
            Dim dicJVBatchNo As New Dictionary(Of String, Long)
            Dim ListofSequenceUsed As New List(Of String)

            ListofSequenceUsed.Add("SEQ_AM_SPA_NO")
            ListofSequenceUsed.Add("SEQ_AM_JV_NO")
            ListofSequenceUsed.Add("SEQ_AM_BATCH_CODE")
            ListofSequenceUsed.Add("SEQ_AM_DMCM_NO")
            ListofSequenceUsed.Add("SEQ_AM_WESMBILL_BATCH_NO")
            ListofSequenceUsed.Add("SEQ_AM_WESMBILL_SUMMARY_NO") 'SEQ_AM_SPA_INV_NO
            ListofSequenceUsed.Add("SEQ_AM_SPA_INV_NO")

            dicListofSeq = New Dictionary(Of String, Long)
            dicListofSeq = WBillHelper.GetMaxSequenceID(ListofSequenceUsed)

            'Get Sequence
            SPANo = dicListofSeq("SEQ_AM_SPA_NO")
            dicListofSeq("SEQ_AM_SPA_NO") += 1

            JVNoClosing = dicListofSeq("SEQ_AM_JV_NO")
            dicListofSeq("SEQ_AM_JV_NO") += 1
            JVBatchNoClosing = dicListofSeq("SEQ_AM_BATCH_CODE")
            dicListofSeq("SEQ_AM_BATCH_CODE") += 1
            JVNoSetup = dicListofSeq("SEQ_AM_JV_NO")
            dicListofSeq("SEQ_AM_JV_NO") += 1
            JVBatchNoSetup = dicListofSeq("SEQ_AM_BATCH_CODE")
            dicListofSeq("SEQ_AM_BATCH_CODE") += 1

            With Me.objSPAMain
                'AM_SPA_MAIN Insert query
                SQL = "INSERT INTO AM_SPA_MAIN (SPA_NO, ID_NUMBER, FIRST_PAYMENT_DATE, TERMS_OF_LOAN_INMONTHS, INTEREST_RATE, TOTAL_PRINCIPAL_AMOUNT, " & _
                                               "TOTAL_INTEREST_AMOUNT, JV_NO_CLOSING, JV_NO_SETUP, UPDATED_BY, UPDATED_DATE, TRANSACTION_DATE) " & _
                      "VALUES  (" & SPANo & ", '" & .ParticipantInfo.IDNumber & "', TO_DATE('" & .FirstPaymentDate & "', 'MM/dd/yyyy'), " & .InMonths & ", " & _
                                .InterestRate * 100 & ", " & .TotalPrincipalAmount & ", " & .TotalInterestAmount & ", " & JVNoClosing & ", " & JVNoSetup & ", '" & AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "', 'MM/dd/yyyy hh24:mi:ss')" & ", TO_DATE('" & SysDate & "', 'MM/dd/yyyy')" & ")"
                ListOfSQL.Add(SQL)

                With .JVClosing
                    Dim ItemPostedType As EnumPostedType = CType([Enum].Parse(GetType(EnumPostedType), CStr(.PostedType)), EnumPostedType)

                    'AM_WESM_BILL_GP_POSTED Insert Query
                    SQL = "INSERT INTO AM_WESM_BILL_GP_POSTED (BILLING_PERIOD, STL_RUN, CHARGE_TYPE, DUE_DATE, REMARKS, POSTED, UPDATED_BY, BATCH_CODE, GP_REFNO, POSTED_TYPE, DOCUMENT_AMOUNT, AM_JV_NO) " & _
                                    "VALUES ('','','','','" & .Remarks & "', '0', '" & _
                                            .UpdatedBy & "', '" & _
                                            ItemPostedType.ToString & "-" & JVBatchNoClosing & "', '', '" & _
                                            ItemPostedType.ToString & "', '" & _
                                            Math.Round((From x In .JVDetails Select x.Debit).Sum, 2) & "', '" & _
                                            JVNoClosing & "')"
                    ListOfSQL.Add(SQL)

                    SQL = "INSERT INTO AM_JV(AM_JV_NO, AM_JV_DATE, BATCH_CODE, STATUS, PREPARED_BY, CHECKED_BY, APPROVED_BY, UPDATED_BY, UPDATED_DATE, POSTED_TYPE) " _
                                & "VALUES (" & JVNoClosing & ", TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & ItemPostedType.ToString & "-" & JVBatchNoClosing _
                                         & "', '" & .Status & "', '" & .PreparedBy & "', '" & .CheckedBy & "', '" & .ApprovedBy & "', '" & .UpdatedBy _
                                         & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & ItemPostedType.ToString() & "')"
                    ListOfSQL.Add(SQL)
                    'AM_JV_DETAILS Insert Query
                    For Each detail In .JVDetails
                        SQL = "INSERT INTO AM_JV_DETAILS (AM_JV_NO, ACCT_CODE, DEBIT, CREDIT, UPDATED_BY, UPDATED_DATE) " _
                            & "VALUES(" & JVNoClosing & ", '" & detail.AccountCode & "', " & detail.Debit & ", " & detail.Credit _
                                    & ", '" & .UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'))"
                        ListOfSQL.Add(SQL)
                    Next
                End With

                With .JVSetup
                    Dim ItemPostedType As EnumPostedType = CType([Enum].Parse(GetType(EnumPostedType), CStr(.PostedType)), EnumPostedType)

                    'AM_WESM_BILL_GP_POSTED Insert Query
                    SQL = "INSERT INTO AM_WESM_BILL_GP_POSTED (BILLING_PERIOD, STL_RUN, CHARGE_TYPE, DUE_DATE, REMARKS, POSTED, UPDATED_BY, BATCH_CODE, GP_REFNO, POSTED_TYPE, DOCUMENT_AMOUNT, AM_JV_NO) " & _
                                    "VALUES ('','','','','" & .Remarks & "', '0', '" & _
                                            .UpdatedBy & "', '" & _
                                            ItemPostedType.ToString & "-" & JVBatchNoSetup & "', '', '" & _
                                            ItemPostedType.ToString & "', '" & _
                                            Math.Round((From x In .JVDetails Select x.Debit).Sum, 2) & "', '" & _
                                            JVNoSetup & "')"
                    ListOfSQL.Add(SQL)

                    SQL = "INSERT INTO AM_JV(AM_JV_NO, AM_JV_DATE, BATCH_CODE, STATUS, PREPARED_BY, CHECKED_BY, APPROVED_BY, UPDATED_BY, UPDATED_DATE, POSTED_TYPE) " _
                                & "VALUES (" & JVNoSetup & ", TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & ItemPostedType.ToString & "-" & JVBatchNoSetup _
                                         & "', '" & .Status & "', '" & .PreparedBy & "', '" & .CheckedBy & "', '" & .ApprovedBy & "', '" & .UpdatedBy _
                                         & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & ItemPostedType.ToString() & "')"
                    ListOfSQL.Add(SQL)
                    'AM_JV_DETAILS Insert Query
                    For Each detail In .JVDetails
                        SQL = "INSERT INTO AM_JV_DETAILS (AM_JV_NO, ACCT_CODE, DEBIT, CREDIT, UPDATED_BY, UPDATED_DATE) " _
                            & "VALUES(" & JVNoSetup & ", '" & detail.AccountCode & "', " & detail.Debit & ", " & detail.Credit _
                                    & ", '" & .UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'))"
                        ListOfSQL.Add(SQL)
                    Next
                End With

                For Each item In .SPADetailsList
                    Dim DMCMNo As Long = dicListofSeq("SEQ_AM_DMCM_NO")
                    Dim GetDMCMItem As DebitCreditMemo = (From x In DMCMListForSPA Where x.DMCMNumber = item.DMCMNumber Select x).FirstOrDefault
                    With GetDMCMItem
                        'AM_DMCM Insert query
                        SQL = "INSERT INTO AM_DMCM (AM_DMCM_NO, AM_JV_NO, ID_NUMBER, PARTICULARS, CHARGE_TYPE, PREPARED_BY, CHECKED_BY, APPROVED_BY, " & _
                                                "UPDATED_BY, UPDATED_DATE, TRANS_TYPE, VATABLE, VAT, VAT_EXEMPT, VAT_ZERO_RATED, TOTAL_AMOUNT_DUE, EWT, EWV, STATUS, OTHERS, BILLING_PERIOD, DUE_DATE) " & _
                            "VALUES ('" & DMCMNo & "', '" & JVNoClosing & "', '" & .IDNumber & "', '" & .Particulars & "', '" & .ChargeType.ToString & "', '" & .PreparedBy & "', '" & .CheckedBy & "', '" & .ApprovedBy & "', " & _
                                    "'" & .UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & .TransType & "', '" & .Vatable & "', '" & .VAT & "', '" & .VATExempt & "', '" & .VatZeroRated & "', " & _
                                    "'" & .TotalAmountDue & "', '" & .EWT & "','" & .EWV & "', '1', '" & .Others & "', '" & .BillingPeriod & "', TO_DATE('" & .DueDate & "','MM/DD/YYYY'))"
                        ListOfSQL.Add(SQL)

                        'AM_DMCM_DETAILS Insert query
                        For Each itemDMCM In .DMCMDetails
                            SQL = "INSERT INTO AM_DMCM_DETAILS (AM_DMCM_NO, ACCT_CODE, DEBIT, CREDIT, UPDATED_BY, UPDATED_DATE, INV_DM_CM, SUMMARY_TYPE, ID_NUMBER, IS_COMPUTE) " & _
                                    "VALUES ('" & DMCMNo & "', '" & itemDMCM.AccountCode & "', '" & itemDMCM.Debit & "', '" & itemDMCM.Credit & "', '" & itemDMCM.UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), " & _
                                            "'" & itemDMCM.InvDMCMNo & "', '" & itemDMCM.SummaryType & "', '" & itemDMCM.IDNumber.IDNumber & "', '" & itemDMCM.IsComputed & "')"
                            ListOfSQL.Add(SQL)
                        Next
                    End With
                    'AM_WESM_BILL_SUMMARY Update query
                    SQL = "UPDATE AM_WESM_BILL_SUMMARY A SET A.ENDING_BALANCE = (A.ENDING_BALANCE - " & item.BalanceAmount & "), A.TRANSACTION_DATE = TO_DATE('" & SysDate & "','mm/dd/yyyy'), " & _
                                                            "A.NEW_DUEDATE = TO_DATE('" & SysDate & "','mm/dd/yyyy'), " & _
                                                            "A.UPDATED_DATE = TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), A.UPDATED_BY = '" & UpdatedBy & "' " & _
                         "WHERE A.WESMBILL_SUMMARY_NO = " & item.WESMBillSummaryInfo.WESMBillSummaryNo
                    ListOfSQL.Add(SQL)

                    SQL = "INSERT INTO AM_SPA_DETAILS (SPA_NO, WESMBILL_SUMMARY_NO, BALANCE_AMOUNT, BALANCE_TYPE, DMCM_NO) " & _
                          "VALUES (" & SPANo & ", " & item.WESMBillSummaryInfo.WESMBillSummaryNo & ", " & item.BalanceAmount & ", " & item.BalanceType & ", " & DMCMNo & ")"
                    ListOfSQL.Add(SQL)
                    dicListofSeq("SEQ_AM_DMCM_NO") += 1
                Next

                Dim DicDMCMDistinct As New Dictionary(Of Long, Long)
                Dim DicWESMBillBatchNo As New Dictionary(Of Date, Long)
                Dim WESMBillBatchNo As Long = 0
                Dim ListofMonthlyDate As List(Of Date) = New List(Of Date)

               
                Dim getSPAMonitoringList As List(Of SPAMonitoring) = .SPAMonitoringList.ToList
                Dim getMonthlyDueDate As List(Of Date) = getSPAMonitoringList.Select(Function(x) x.MonthlyDueDate).Distinct().OrderBy(Function(d) d).ToList

                For Each item In getMonthlyDueDate

                    Dim getSPAMonitoringListPerDueDate As List(Of SPAMonitoring) = (From x In getSPAMonitoringList Where x.MonthlyDueDate = item Select x).ToList()

                    For Each oitem As SPAMonitoring In getSPAMonitoringListPerDueDate
                        Dim SPAWESMBillSummaryNo As Long = dicListofSeq("SEQ_AM_WESMBILL_SUMMARY_NO")

                        'AM_WESMBILL_SUMMARY Insert Query
                        With oitem.WESMBillSummaryInfo
                            WESMBillBatchNo = dicListofSeq("SEQ_AM_WESMBILL_BATCH_NO")
                            Dim SPAINV As String = EnumSummaryType.SPA.ToString & "-" & dicListofSeq("SEQ_AM_SPA_INV_NO").ToString("D7")

                            SQL = "INSERT INTO AM_WESM_BILL (BATCH_CODE,AM_CODE,BILLING_PERIOD,STL_RUN,ID_NUMBER,REG_ID,FOR_ACCOUNT_OF,FULL_NAME, " _
                                & "INVOICE_NO,INVOICE_DATE,AMOUNT,CHARGE_TYPE,DUE_DATE,MARKET_FEES_RATE,REMARKS) VALUES( " _
                                & "'U-XX', 'U-CODE-XXX', " & .BillPeriod & ", 'FX', '" _
                                & .IDNumber.IDNumber & "', '" & .IDNumber.IDNumber & "', 'FOR THE ACCOUNT OF', '" & .IDNumber.FullName.Replace("'", "") & "', '" _
                                & SPAINV & "', TO_DATE('" & SysDate.ToString("MM/dd/yyyy") & "','mm/dd/yyyy'), " & .BeginningBalance & ", '" & .ChargeType.ToString & "', " _
                                & "TO_DATE('" & .DueDate & "','mm/dd/yyyy'), 0, 'SPA Invoices of " & Me.objSPAMain.ParticipantInfo.ParticipantID & "')"

                            ListOfSQL.Add(SQL)

                            SQL = "INSERT INTO AM_WESM_BILL_SUMMARY (BILLING_PERIOD, ID_NUMBER, CHARGE_TYPE, DUE_DATE, BEGINNING_BALANCE, UPDATED_DATE, UPDATED_BY, ENDING_BALANCE, GROUP_NO, " & _
                                                                "ID_TYPE, NEW_DUEDATE, IS_MFWTAX_DEDUCTED, INV_DM_CM, SUMMARY_TYPE, WESMBILL_SUMMARY_NO, ADJUSTMENT, TRANSACTION_DATE, ENERGY_WITHHOLD, SPA_NO, WESMBILL_BATCH_NO, ENERGY_WITHHOLD_STATUS, BALANCE_TYPE) " & _
                                "VALUES (" & .BillPeriod & ", '" & .IDNumber.IDNumber & "', '" & .ChargeType.ToString & "', TO_DATE('" & .DueDate.ToShortDateString & "', 'mm/dd/yyyy'), " & .BeginningBalance & ", " & _
                                        "TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & UpdatedBy & "', " & .EndingBalance & ", 0, '" & .IDType.ToString & "', " & _
                                        "TO_DATE('" & .NewDueDate.ToShortDateString & "', 'mm/dd/yyyy'), 0, '" & SPAINV & "', '" & .SummaryType.ToString & "', " & SPAWESMBillSummaryNo & ", 0, TO_DATE('" & SysDate.ToShortDateString & "', 'mm/dd/yyyy'), " & _
                                        "0, " & SPANo & "," & WESMBillBatchNo & ", " & .EnergyWithholdStatus & ", '" & If(.BeginningBalance >= 0, "AP", "AR") & "')"
                            ListOfSQL.Add(SQL)
                        End With
                        Dim DMCMNo As Long = 0

                        If Not DicDMCMDistinct.ContainsKey(oitem.DMCMNumber) Then
                            DMCMNo = dicListofSeq("SEQ_AM_DMCM_NO")
                            DicDMCMDistinct.Add(oitem.DMCMNumber, DMCMNo)
                            Dim GetDMCMItem As DebitCreditMemo = (From x In DMCMListForSPA Where x.DMCMNumber = oitem.DMCMNumber Select x).FirstOrDefault
                            With GetDMCMItem
                                'AM_DMCM Insert Query
                                SQL = "INSERT INTO AM_DMCM (AM_DMCM_NO, AM_JV_NO, ID_NUMBER, PARTICULARS, CHARGE_TYPE, PREPARED_BY, CHECKED_BY, APPROVED_BY, " & _
                                                        "UPDATED_BY, UPDATED_DATE, TRANS_TYPE, VATABLE, VAT, VAT_EXEMPT, VAT_ZERO_RATED, TOTAL_AMOUNT_DUE, EWT, EWV, STATUS, OTHERS, BILLING_PERIOD, DUE_DATE) " & _
                                    "VALUES ('" & DMCMNo & "', '" & JVNoSetup & "', '" & .IDNumber & "', '" & .Particulars & "', '" & .ChargeType.ToString & "', '" & .PreparedBy & "', '" & .CheckedBy & "', '" & .ApprovedBy & "', " & _
                                            "'" & .UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & .TransType & "', '" & .Vatable & "', '" & .VAT & "', '" & .VATExempt & "', '" & .VatZeroRated & "', " & _
                                            "'" & .TotalAmountDue & "', '" & .EWT & "','" & .EWV & "', '1', '" & .Others & "', '" & .BillingPeriod & "', TO_DATE('" & .DueDate & "','MM/DD/YYYY'))"
                                ListOfSQL.Add(SQL)

                                'Insert AM_DMCM_DETAILS query database
                                For Each itemDMCM In .DMCMDetails
                                    SQL = "INSERT INTO AM_DMCM_DETAILS (AM_DMCM_NO, ACCT_CODE, DEBIT, CREDIT, UPDATED_BY, UPDATED_DATE, INV_DM_CM, SUMMARY_TYPE, ID_NUMBER, IS_COMPUTE) " & _
                                            "VALUES ('" & DMCMNo & "', '" & itemDMCM.AccountCode & "', '" & itemDMCM.Debit & "', '" & itemDMCM.Credit & "', '" & itemDMCM.UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), " & _
                                                    "'" & itemDMCM.InvDMCMNo & "', '" & itemDMCM.SummaryType & "', '" & itemDMCM.IDNumber.IDNumber & "', '" & itemDMCM.IsComputed & "')"
                                    ListOfSQL.Add(SQL)
                                Next
                            End With
                            dicListofSeq("SEQ_AM_DMCM_NO") += 1
                        Else
                            DMCMNo = DicDMCMDistinct.Item(oitem.DMCMNumber)
                        End If
                        'AM_SPA_Monitoring Insert Query
                        SQL = "INSERT INTO AM_SPA_MONITORING (SPA_WESMBILL_SUMMARY_NO, MONTHLY_AMORTIZATION, MONTHLY_DUEDATE, PRINCIPAL_AMOUNT, INTEREST_AMOUNT, BALANCE_AMOUNT, DMCM_NO, SPA_NO) " & _
                                "VALUES (" & SPAWESMBillSummaryNo & ", " & oitem.MonthlyAmortization & ", TO_DATE('" & oitem.MonthlyDueDate.ToShortDateString & "', 'mm/dd/yyyy')" & ", " & oitem.PrincipalAmount & ", " & oitem.InterestAmount & ", " & oitem.BalanceAmount & ", " & DMCMNo & ", " & SPANo & ")"
                        ListOfSQL.Add(SQL)
                        dicListofSeq("SEQ_AM_WESMBILL_SUMMARY_NO") += 1
                        dicListofSeq("SEQ_AM_SPA_INV_NO") += 1
                    Next
                    dicListofSeq("SEQ_AM_WESMBILL_BATCH_NO") += 1
                Next

            End With
            Return ListOfSQL
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Function
    Private Function CreateSQLStatementNewForSeq(ByVal updatedDicSeqList As Dictionary(Of String, Long)) As List(Of String)
        Dim listofSequenceUsed As New List(Of String)
        Dim getDicListOfSeqIDs As New Dictionary(Of String, Long)

        Dim listSQL As New List(Of String)
        For Each kvp As KeyValuePair(Of String, Long) In updatedDicSeqList
            Dim newValSeq As Long = CLng(updatedDicSeqList.Item(kvp.Key))
            Dim sql As String = "ALTER SEQUENCE " & kvp.Key & " RESTART START WITH " & newValSeq

            listSQL.Add(sql)
        Next
        Return listSQL
    End Function
#End Region


End Class
