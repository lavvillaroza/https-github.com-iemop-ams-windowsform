'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmSummaryOfDefaultInterest
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     January 23, 2013
'Development Group:      Software Development and Support Division
'Description:            GUI for Viewing and/or Printing Summary of Default interest per indicated Date Range
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description
'
'

Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementLogic
Imports WESMLib.Auth.Lib



Public Class frmSummaryOfDefaultInterest
    Private WbillHelper As WESMBillHelper
    Private _lstParticipants As New List(Of AMParticipants)
    Private _lstBillPeriod As New List(Of CalendarBillingPeriod)
    Private _lstInterestRates As Dictionary(Of Date, Decimal)

    Private Sub frmSummaryOfOutstandingBalances_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.MdiParent = MainForm
            WbillHelper = WESMBillHelper.GetInstance
            WbillHelper.UserName = AMModule.UserName

            _lstParticipants = WbillHelper.GetAMParticipantsAll()
            _lstBillPeriod = WbillHelper.GetCalendarBP()
            _lstInterestRates = WbillHelper.GetDailyInterestRate()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Close.Click
        Me.Close()
    End Sub

    Private Sub cmd_GenerateReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_GenerateReport.Click
        Try
            Dim _lstCollections = WbillHelper.GetCollectionDISummary(CDate(FormatDateTime(Me.dtp_From.Value, DateFormat.ShortDate)), CDate(FormatDateTime(Me.dtp_To.Value, DateFormat.ShortDate)))
            Dim _lstColDefaultInterest = (From x In _lstCollections _
                                       Where x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy _
                                       Or x.CollectionType = EnumCollectionType.DefaultInterestOnMF _
                                       Or x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF _
                                       Or x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnEnergy _
                                       Or x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest _
                                       Or x.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest _
                                       Select x).ToList

            Dim _CollectionHeaders = Me.WbillHelper.GetCollections(CDate(FormatDateTime(Me.dtp_From.Value, DateFormat.ShortDate)), CDate(FormatDateTime(Me.dtp_To.Value, DateFormat.ShortDate)), True)
            Dim _fltrCollection As New List(Of Collection)
            For Each itmCollection In _CollectionHeaders
                Dim chkCollection = (From x In itmCollection.ListOfCollectionAllocation _
                                          Where x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy _
                                          Or x.CollectionType = EnumCollectionType.DefaultInterestOnMF _
                                          Or x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF _
                                          Or x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnEnergy _
                                          Or x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest _
                                          Or x.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest _
                                          Select x).ToList

                If chkCollection.Count > 0 Then
                    _fltrCollection.Add(itmCollection)
                End If
            Next

            Dim _lstPaymentAlloc = WbillHelper.GetPaymentAllocationAccount() 'CDate(FormatDateTime(Me.dtp_From.Value, DateFormat.ShortDate)), CDate(FormatDateTime(Me.dtp_To.Value, DateFormat.ShortDate)))
            Dim _lstPayment = WbillHelper.GetPayment(CDate(FormatDateTime(Me.dtp_From.Value, DateFormat.ShortDate)), CDate(FormatDateTime(Me.dtp_To.Value, DateFormat.ShortDate)))
            Dim _lstPayDefaultInterest = (From x In _lstPaymentAlloc, z In _lstPayment _
                                          Where z.PaymentPerBPNo = x.PaymentBatchCode _
                                          And (x.PaymentType = EnumPaymentType.UnpaidMFDefault _
                                          Or x.PaymentType = EnumPaymentType.UnpaidMFVDefault _
                                          Or x.PaymentType = EnumPaymentType.WHTaxDefault _
                                          Or x.PaymentType = EnumPaymentType.WHVATDefault _
                                          Or x.PaymentType = EnumPaymentType.OffsetOfPreviousReceivableEnergyDefault _
                                          Or x.PaymentType = EnumPaymentType.OffsetToCurrentReceivableEnergyDefault _
                                          Or x.PaymentType = EnumPaymentType.DefaultAllocation) _
                                          Select x).ToList

            If _lstPayDefaultInterest.Count <> 0 Then
                'Get WESM Bill Summary Nos
                Dim _lstWESMBillNo = (From x In _lstPayDefaultInterest _
                                      Select x.WESMBillSummary.WESMBillSummaryNo Distinct).ToList

                Dim _lstWESMBills = WbillHelper.GetWESMBillSummary(_lstWESMBillNo, False)

                For Each itmPayment In _lstPayDefaultInterest
                    Dim _itmPayment = itmPayment
                    Dim _WESMBill = (From x In _lstWESMBills _
                                     Where x.WESMBillSummaryNo = _itmPayment.WESMBillSummary.WESMBillSummaryNo _
                                     Select x).FirstOrDefault

                    itmPayment.WESMBillSummary = _WESMBill
                Next
            End If

            If _fltrCollection.Count = 0 And _lstPayment.Count = 0 Then
                Throw New ApplicationException("No Records found.")
            End If

            Me.dgv_ViewRecords.DataSource = Nothing
            Me.dgv_ViewRecords.DataSource = Me.GenerateDT2(_lstColDefaultInterest, _lstPayDefaultInterest, _fltrCollection, _lstPayment)

            For x = 0 To dgv_ViewRecords.Columns.Count - 1
                dgv_ViewRecords.Columns(x).SortMode = DataGridViewColumnSortMode.NotSortable
            Next

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Updated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepSummaryofDefaultWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInGeneratingReport.ToString, AMModule.UserName)
            Exit Sub
        End Try
    End Sub

    Private Function GenerateDT2(ByVal lstCollectionDefault As List(Of CollectionAllocation), ByVal lstPaymentDefault As List(Of PaymentAllocationAccount), _
                                ByVal CollectionHeader As List(Of Collection), ByVal PaymentHeader As List(Of Payment)) As DataTable
        Dim dt As New DataTable
        If rb_MarketFees.Checked Then
            lstCollectionDefault = (From x In lstCollectionDefault _
                                    Where x.WESMBillSummaryNo.ChargeType = EnumChargeType.MF _
                                    Or x.WESMBillSummaryNo.ChargeType = EnumChargeType.MFV _
                                    Select x).ToList

            lstPaymentDefault = (From x In lstPaymentDefault _
                                 Where x.WESMBillSummary.ChargeType = EnumChargeType.MF _
                                 Or x.WESMBillSummary.ChargeType = EnumChargeType.MFV _
                                 Select x).ToList
        Else
            lstCollectionDefault = (From x In lstCollectionDefault _
                                    Where x.WESMBillSummaryNo.ChargeType <> EnumChargeType.MF _
                                    And x.WESMBillSummaryNo.ChargeType <> EnumChargeType.MFV _
                                    Select x).ToList

            lstPaymentDefault = (From x In lstPaymentDefault _
                                 Where x.WESMBillSummary.ChargeType <> EnumChargeType.MF _
                                 And x.WESMBillSummary.ChargeType <> EnumChargeType.MFV _
                                 Select x).ToList
        End If

        If lstPaymentDefault.Count = 0 And lstCollectionDefault.Count = 0 Then
            Throw New ApplicationException("No Records found for the said range.")
            Exit Function
        End If

        With dt.Columns
            .Add("ID Number")
            .Add("Participant ID")
            .Add("OR Number")
            .Add("OR Date")
            .Add("Billing Period")
            .Add("Default Period")
            .Add("Reference Document")
            .Add("Days Outstanding")
            .Add("Interest Rate")
            .Add("Amount Defaulted")
            If rb_MarketFees.Checked Then
                .Add("WHTax/WHVat Amount")
            End If
            .Add("Default Interest")
        End With

        'Add Header for Collections in Cash
        Dim TotalCollection As Decimal = 0
        Dim dr As DataRow
        dr = dt.NewRow

        dr("ID Number") = "Cash Collections"

        dt.Rows.Add(dr)
        dt.AcceptChanges()

        'Get Distinct Participants with OR
        Dim _lstColParticipant = (From x In CollectionHeader _
                                  Select x.IDNumber Distinct).ToList

        For Each itmParticipant In _lstColParticipant
            Dim _itmParticipant = itmParticipant

            'Get Collections of Participant
            Dim _lstParticipantCollection = (From x In CollectionHeader _
                                             Where x.IDNumber = _itmParticipant _
                                             Select x).ToList

            For Each itmCollection In _lstParticipantCollection
                Dim _itmCollection = itmCollection
                Dim _init As Boolean = True

                dr = dt.NewRow

                dr("ID Number") = _itmCollection.IDNumber
                dr("Participant ID") = (From x In _lstParticipants _
                                        Where x.IDNumber = _itmCollection.IDNumber _
                                        Select x.ParticipantID).FirstOrDefault

                dr("OR Number") = _itmCollection.ORNo
                dr("OR Date") = FormatDateTime(_itmCollection.CollectionDate, DateFormat.ShortDate)

                'Get collection details
                Dim _lstCollectionDetails = (From x In lstCollectionDefault _
                                             Where x.CollectionNumber = _itmCollection.CollectionNumber _
                                             Select x).ToList

                'Get Per WESMBill Summary No
                Dim _lstWESMSummaryNo = (From x In _lstCollectionDetails _
                                         Select x.WESMBillSummaryNo.WESMBillSummaryNo Distinct).ToList

                For Each itmDetails In _lstWESMSummaryNo
                    Dim _itmDetails = itmDetails

                    'get collection for WESM Bill
                    Dim _lstColDetails = (From x In _lstCollectionDetails _
                                                 Where x.WESMBillSummaryNo.WESMBillSummaryNo = _itmDetails _
                                                 Select x).ToList

                    For Each colDetails In _lstColDetails
                        Dim _colDetails = colDetails
                        If _init = False Then
                            dr = dt.NewRow
                        End If
                        dr("Billing Period") = colDetails.BillingPeriod

                        'Default Period
                        Dim DefaultPeriod As String = ""
                        DefaultPeriod = "From " & FormatDateTime(colDetails.DueDate, DateFormat.ShortDate) & " To " & FormatDateTime(_itmCollection.CollectionDate, DateFormat.ShortDate)
                        dr("Default Period") = DefaultPeriod

                        'Reference Document
                        dr("Reference Document") = colDetails.ReferenceType.ToString & colDetails.ReferenceNumber

                        'Days Default
                        Dim DefaultDays As Long = 0
                        If colDetails.DueDate = colDetails.WESMBillSummaryNo.DueDate Then
                            DefaultDays = Math.Abs(DateDiff(DateInterval.Day, _itmCollection.CollectionDate, colDetails.DueDate)) + 1
                        Else
                            DefaultDays = Math.Abs(DateDiff(DateInterval.Day, _itmCollection.CollectionDate, colDetails.DueDate))
                        End If
                        dr("Days Outstanding") = DefaultDays

                        'Interest Rate
                        dr("Interest Rate") = Me._lstInterestRates(colDetails.AllocationDate)

                        'Amount Defaulted
                        dr("Amount Defaulted") = FormatNumber(colDetails.EndingBalance, 2, TriState.True, TriState.True)

                        'WHTax/WHVAT Amount
                        If rb_MarketFees.Checked Then
                            If colDetails.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest Or _
                                colDetails.CollectionType = EnumCollectionType.WithholdingVatonDefaultInterest Then
                                Dim WtaxVATAmount As Decimal = (From x In _lstColDetails _
                                                            Where (x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest _
                                                            Or x.CollectionType = EnumCollectionType.WithholdingVatonDefaultInterest) _
                                                            And (x.ReferenceNumber = _colDetails.ReferenceNumber _
                                                            Or x.ReferenceType = _colDetails.ReferenceType) _
                                                            Select x.Amount).Sum

                                dr("WHTax/WHVat Amount") = FormatNumber(WtaxVATAmount, 2, TriState.True, TriState.True)

                            End If
                        End If

                        'Default Interest
                        dr("Default Interest") = FormatNumber(colDetails.Amount, 2, TriState.True, TriState.True)
                        TotalCollection += colDetails.Amount
                        dt.Rows.Add(dr)
                        dt.AcceptChanges()
                        _init = False

                    Next

                Next
            Next
        Next

        'Add Total
        dr = dt.NewRow

        dr("ID Number") = "Total Cash Collections"

        dr("Default Interest") = FormatNumber(TotalCollection, 2, TriState.True, TriState.True)
        dt.Rows.Add(dr)

        'Add Spacing 
        dt.Rows.Add(dt.NewRow)
        dt.AcceptChanges()


        dr = dt.NewRow
        dr("ID Number") = "Payment Offsetting"
        dt.Rows.Add(dr)
        dt.AcceptChanges()

        'Add Payment Offsetting
        Dim _lstPayOffsetting As List(Of PaymentAllocationAccount)
        Dim TotalPayment As Decimal = 0


        If rb_Energy.Checked Then
            _lstPayOffsetting = (From x In lstPaymentDefault _
                                     Where x.PaymentType = EnumPaymentType.OffsetOfPreviousReceivableEnergyDefault _
                                     Or x.PaymentType = EnumPaymentType.OffsetToCurrentReceivableEnergyDefault _
                                     Select x).ToList
        Else
            _lstPayOffsetting = (From x In lstPaymentDefault _
                         Where x.PaymentType = EnumPaymentType.UnpaidMFDefault _
                         Or x.PaymentType = EnumPaymentType.UnpaidMFVDefault _
                         Or x.PaymentType = EnumPaymentType.WHTaxDefault _
                         Or x.PaymentType = EnumPaymentType.WHVATDefault _
                         Select x).ToList
        End If


        For Each itmOffset In _lstPayOffsetting
            Dim _itmOffset = itmOffset
            dr = dt.NewRow

            'Get Current Participant
            Dim _ParticipantInfo = (From x In _lstParticipants _
                                 Where x.IDNumber = _itmOffset.WESMBillSummary.IDNumber.IDNumber _
                                 Select x).FirstOrDefault

            'Get Payment Header Details
            Dim _PaymentHeader = (From x In PaymentHeader _
                                  Where x.PaymentPerBPNo = _itmOffset.PaymentBatchCode _
                                  Select x).FirstOrDefault

            dr("ID Number") = _ParticipantInfo.IDNumber
            dr("Participant ID") = _ParticipantInfo.ParticipantID

            dr("OR Date") = FormatDateTime(_PaymentHeader.PaymentAllocationDate, DateFormat.ShortDate)
            dr("Billing Period") = _itmOffset.WESMBillSummary.BillPeriod

            Dim DefaultPeriod As String = ""
            DefaultPeriod = "From " & FormatDateTime(_itmOffset.WESMBillSummary.DueDate, DateFormat.ShortDate) & " To " & FormatDateTime(_PaymentHeader.PaymentAllocationDate, DateFormat.ShortDate) ' itmBills.NewDueDate, DateFormat.ShortDate)
            dr("Default Period") = DefaultPeriod

            dr("Reference Document") = _itmOffset.WESMBillSummary.SummaryType.ToString & _itmOffset.WESMBillSummary.INVDMCMNo

            Dim DefaultDays As Long = 0
            If _itmOffset.DueDate = _itmOffset.WESMBillSummary.DueDate Then
                DefaultDays = DateDiff(DateInterval.Day, _PaymentHeader.PaymentAllocationDate, _itmOffset.DueDate)
            Else
                DefaultDays = DateDiff(DateInterval.Day, _PaymentHeader.PaymentAllocationDate, _itmOffset.DueDate) + 1
            End If
            dr("Days Outstanding") = Math.Abs(DefaultDays)

            dr("Amount Defaulted") = FormatNumber(_itmOffset.BeginningBalance, 2, TriState.True, TriState.True)

            dr("Interest Rate") = _lstInterestRates(CDate(FormatDateTime(_PaymentHeader.PaymentAllocationDate, DateFormat.ShortDate)))

            dr("Default Interest") = FormatNumber(_itmOffset.PaymentAmount, 2, TriState.True, TriState.True)
            TotalPayment += _itmOffset.PaymentAmount
            dt.Rows.Add(dr)
            dt.AcceptChanges()
        Next

        dr = dt.NewRow
        dr("ID Number") = "Total Payment Offsetting"
        dr("Default Interest") = FormatNumber(TotalPayment, 2, TriState.True, TriState.True)
        dt.Rows.Add(dr)
        dt.AcceptChanges()

        dt.Rows.Add(dt.NewRow)
        dt.AcceptChanges()

        dr = dt.NewRow
        dr("ID Number") = "Default Interest Allocation"
        dt.Rows.Add(dr)
        dt.AcceptChanges()


        For Each itmAllocated In lstPaymentDefault.Where(Function(x) x.PaymentType = EnumPaymentType.DefaultAllocation).OrderBy(Function(x) x.WESMBillSummary.IDNumber.IDNumber)

            Dim _itmAllocation = itmAllocated
            Dim _ParticipantInfo = (From x In _lstParticipants _
                                    Where x.IDNumber = _itmAllocation.WESMBillSummary.IDNumber.IDNumber _
                                    Select x).FirstOrDefault

            Dim _PaymentHeader = (From x In PaymentHeader _
                                  Where x.PaymentPerBPNo = _itmAllocation.PaymentBatchCode _
                                  Select x).FirstOrDefault

            dr = dt.NewRow

            dr("ID Number") = _ParticipantInfo.IDNumber
            dr("Participant ID") = _ParticipantInfo.ParticipantID
            dr("OR Date") = FormatDateTime(_PaymentHeader.PaymentAllocationDate, DateFormat.ShortDate)
            dr("Billing Period") = _itmAllocation.WESMBillSummary.BillPeriod


            Dim DefaultPeriod As String = ""
            DefaultPeriod = "From " & FormatDateTime(_itmAllocation.WESMBillSummary.DueDate, DateFormat.ShortDate) & " To " & FormatDateTime(_PaymentHeader.PaymentAllocationDate, DateFormat.ShortDate) ' itmBills.NewDueDate, DateFormat.ShortDate)
            dr("Default Period") = DefaultPeriod

            dr("Reference Document") = EnumSummaryType.DMCM.ToString & _itmAllocation.DMCMNo
            dr("Default Interest") = FormatNumber(_itmAllocation.PaymentAmount, 2, TriState.True, TriState.True)

            dt.Rows.Add(dr)
            dt.AcceptChanges()
        Next

        dr = dt.NewRow
        dr("ID Number") = "Total Default Interest Allocation"
        Dim TotalSum = (From x In lstPaymentDefault _
                        Where x.PaymentType = EnumPaymentType.DefaultAllocation _
                        Select x.PaymentAmount).Sum

        dr("Default Interest") = FormatNumber(TotalSum, 2, TriState.True, TriState.True)

        dt.Rows.Add(dr)
        dt.AcceptChanges()

        Return dt
    End Function

    Private Sub cmd_ExportFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ExportFile.Click

        If Me.dgv_ViewRecords.RowCount = 0 Then
            MsgBox("No records to export", MsgBoxStyle.Exclamation, "Error Encountered")
            Exit Sub
        End If

        Dim fldrMNU As New FolderBrowserDialog
        With fldrMNU
            .ShowDialog()
            Dim fname As String = ""

            If .SelectedPath.Trim.Length = 0 Then
                MsgBox("Export to CSV is cancelled by the user.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            If rb_Energy.Checked Then
                fname = .SelectedPath & "\EnergyDefaultInterestSummary" & Replace(CDate(FormatDateTime(SystemDate, DateFormat.ShortDate)).ToString("MMddyyyy"), "/", "") & ".csv"
            Else
                fname = .SelectedPath & "\MarketFeesDefaultInterestSummary" & Replace(CDate(FormatDateTime(SystemDate, DateFormat.ShortDate)).ToString("MMddyyyy"), "/", "") & ".csv"
            End If


            Me.WbillHelper.DataTable2CSV(Me.WbillHelper.BFactory.RemoveCommaForCSVExport(CType(Me.dgv_ViewRecords.DataSource, DataTable)), fname)
            MsgBox("Successfully exported to " & fname, MsgBoxStyle.Information, "Successful Export")
        End With
    End Sub
End Class