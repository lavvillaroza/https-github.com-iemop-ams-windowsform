'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmRPTOutstandingBalances
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 07, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for viewing of existing Outstanding Balances per participant
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description

'

Option Explicit On
Option Strict On

Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports System.IO
Imports System.Windows.Forms

Public Class frmRPTOutstandingBalances
    Private WBillHelper As WESMBillHelper
    Private xlHandler As ExcelHandler
    Private WBillDefaulted As New List(Of WESMBillSummary)

    Private Sub frmRPTOutstandingBalances_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.MdiParent = MainForm
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            xlHandler = ExcelHandler.GetInstance()
        Catch ex As Exception
            MsgBox(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_GenerateReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_GenerateReport.Click
        Dim ds As New DataSet
        Dim GenSummary As New DataTable        
        Dim FileName As String = Me.txt_DestFile.Text & "\Summary of Outstanding Balances As Of " & Replace(FormatDateTime(Now, DateFormat.ShortDate), "/", "") & ".csv"
        Try

            If Trim(Me.txt_DestFile.Text).Length = 0 Then
                MsgBox("Please select a destination folder.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            If Me.dgv_OutstandingSummary.RowCount = 0 Then
                MsgBox("There are no records to export.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            Dim dt As New DataTable            
            dt = Me.WBillHelper.BFactory.RemoveCommaForCSVExport(CType(Me.dgv_OutstandingSummary.DataSource, DataTable))
            'ds.Tables.Add(dt)

            Dim _dt As New DataTable
            _dt = CType(Me.dgv_OutstandingSummary.DataSource, DataTable)

            Me.WBillHelper.DataTable2CSV(Me.WBillHelper.BFactory.RemoveCommaForCSVExport(_dt), FileName)
            'Change report to CSV File 021113
            'If xlHandler.ExportToExcel(Me.txt_DestFile.Text, ds, "Summary of Outstanding Balances", "Error Generating Summary of " & vbCrLf & "Outstanding Balances", ds.Tables(0).Columns.Count) = True Then
            MsgBox("Successfully completed generation of Summary of Outstanding Balances", MsgBoxStyle.Information, Me.Name)
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
            Exit Sub        
        End Try
    End Sub

    Private Sub cmd_browse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_browse.Click
        Dim sfDialog As New FolderBrowserDialog
        With sfDialog
            .ShowNewFolderButton = True
            .RootFolder = Environment.SpecialFolder.MyComputer
            .ShowDialog()
            Me.txt_DestFile.Text = .SelectedPath
        End With
    End Sub

    Private Sub cmd_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub

    Private Sub cmd_browseDNotice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim sfDialog As New FolderBrowserDialog
        With sfDialog
            .RootFolder = Environment.SpecialFolder.Desktop
            .ShowDialog()
        End With
    End Sub

    Private Function GenerateOutstandingSummary(ByVal Charge As String) As DataTable
        Dim retGenSummary As New DataTable

        Dim WBillSummary As New List(Of WESMBillSummary)
        Dim WBSummaryPrefix1 As String = ""        

        If Charge = "ENERGY" Then
            WBillSummary = WBillHelper.GetWESMBillSummary()
            WBillSummary = (From x In WBillSummary _
                            Where x.ChargeType <> EnumChargeType.MF _
                            And x.ChargeType <> EnumChargeType.MFV _
                            And x.ChargeType <> EnumChargeType.EV _
                            Select x).ToList
            WBSummaryPrefix1 = EnumChargeType.E.ToString            
        ElseIf Charge = "VATONENERGY" Then
            WBillSummary = WBillHelper.GetWESMBillSummary()
            WBillSummary = (From x In WBillSummary _
                            Where x.ChargeType <> EnumChargeType.MF _
                            And x.ChargeType <> EnumChargeType.MFV _
                            And x.ChargeType <> EnumChargeType.E _
                            Select x).ToList
            WBSummaryPrefix1 = EnumChargeType.EV.ToString                    
        End If

        Dim _AllParticipants = Me.WBillHelper.GetAMParticipants()

        'Get Distinct BP and DueDates
        Dim _fltrBPDueDate = (From x In WBillSummary _
                              Select x.BillPeriod, x.DueDate Distinct _
                              Order By BillPeriod Ascending, DueDate Ascending).ToList

        If WBillSummary.Count = 0 Then
            Throw New ApplicationException("No Outstanding Balances found!")
        End If

        retGenSummary.Columns.Add("Participant ID")
        retGenSummary.Columns.Add("Settlement ID")
        retGenSummary.Columns.Add("Region")

        For Each itmBPDueDate In _fltrBPDueDate
            With retGenSummary.Columns
                .Add(WBSummaryPrefix1 & " (" & itmBPDueDate.BillPeriod.ToString & ")" & vbCrLf & FormatDateTime(itmBPDueDate.DueDate, DateFormat.ShortDate))
            End With
        Next

        Dim chargeType1 As Integer = DirectCast(EnumChargeType.Parse(GetType(EnumChargeType), WBSummaryPrefix1), EnumChargeType) 'Get selected chargetype            

        Dim TotalARBills1 = (From x In WBillSummary _
                            Where x.EndingBalance < 0 _
                            And x.ChargeType = chargeType1 _
                            Select x.EndingBalance).Sum 'TotalARBills for Energy or VatOnEnergy

        'Total Receivables in Percentage
        Dim initRow As DataRow
        initRow = retGenSummary.NewRow
        For Each BPDueDate In _fltrBPDueDate
            Dim _BPDueDate = BPDueDate
            'Get Total Sum Per BP
            Dim TotalARPerBP1 = (From x In WBillSummary _
                                Where x.EndingBalance < 0 _
                                And x.BillPeriod = _BPDueDate.BillPeriod _
                                And x.DueDate = _BPDueDate.DueDate _
                                And x.ChargeType = chargeType1 _
                                Select x.EndingBalance).Sum

            If TotalARPerBP1 = 0 Then
                Exit For
            End If
            'Compute Percentage
            Dim Pct1 = TotalARPerBP1 / TotalARBills1 * 100            
            initRow(WBSummaryPrefix1 & " (" & _BPDueDate.BillPeriod & ")" & vbCrLf & FormatDateTime(_BPDueDate.DueDate, DateFormat.ShortDate)) = Pct1 & "%"
        Next

        retGenSummary.Columns.Add("Prudential Security")
        retGenSummary.Columns.Add("DifferenceOf OBalance-PR")
        retGenSummary.Columns.Add("Total")
        retGenSummary.Columns.Add("Remarks")

        retGenSummary.Rows.Add(initRow)
        retGenSummary.AcceptChanges()

        'Total Receivables per Billing Period
        'Get Total AR For All Bills
        Dim initRow2 As DataRow
        initRow2 = retGenSummary.NewRow
        For Each BPDueDate In _fltrBPDueDate
            Dim _BPDueDate = BPDueDate
            'Get Total Sum Per BP
            Dim TotalARPerBP1 = (From x In WBillSummary _
                                Where x.EndingBalance < 0 _
                                And x.BillPeriod = _BPDueDate.BillPeriod _
                                And x.DueDate = _BPDueDate.DueDate _
                                And x.ChargeType = chargeType1 _
                                Select x.EndingBalance).Sum

            If TotalARPerBP1 = 0 Then
                Exit For
            End If
            initRow2(WBSummaryPrefix1 & " (" & _BPDueDate.BillPeriod & ")" & vbCrLf & FormatDateTime(_BPDueDate.DueDate, DateFormat.ShortDate)) = FormatNumber(TotalARPerBP1, 2, TriState.True, TriState.True)            
        Next

        initRow2("Total") = FormatNumber(TotalARBills1, 2, TriState.True, TriState.True)
        retGenSummary.Rows.Add(initRow2)
        retGenSummary.AcceptChanges()

        'Get AR Outstanding Balances
        'Get Participant with AR
        Dim _fltrARParticipants = (From x In WBillSummary _
                                    Where x.EndingBalance < 0 _
                                    Select x.IDNumber.IDNumber Distinct Order By IDNumber Ascending).ToList


        For Each itmParticipant In _fltrARParticipants
            Dim _itmParticipant = itmParticipant

            'Get Participant's Bills
            Dim _pARBills = (From x In WBillSummary _
                                Where x.EndingBalance < 0 _
                                And x.IDNumber.IDNumber = _itmParticipant _
                                Select x).ToList
            Dim dr As DataRow
            dr = retGenSummary.NewRow
            'Filter per BP and DueDate
            For Each BPDueDate In _fltrBPDueDate
                Dim _BPDueDate = BPDueDate

                dr("Participant ID") = _pARBills.First.IDNumber.ParticipantID
                dr("Settlement ID") = _pARBills.First.IDNumber.IDNumber
                dr("Region") = (From x In _AllParticipants _
                                Where x.IDNumber = _pARBills.FirstOrDefault.IDNumber.IDNumber _
                                Select x.Region).FirstOrDefault '_pARBills.First.IDNumber.

                'Total AR of Bills for BP/DueDate
                Dim pTotalAR1 = (From x In _pARBills _
                                    Where x.BillPeriod = _BPDueDate.BillPeriod _
                                    And x.DueDate = _BPDueDate.DueDate _
                                    And x.ChargeType = chargeType1 _
                                    Select x.EndingBalance).Sum

                Dim pTotalARBills = (From x In _pARBills _
                                        Where x.BillPeriod = _BPDueDate.BillPeriod _
                                        And x.DueDate = _BPDueDate.DueDate _
                                        Select x).ToList

                dr(WBSummaryPrefix1 & " (" & _BPDueDate.BillPeriod.ToString & ")" & vbCrLf & FormatDateTime(_BPDueDate.DueDate, DateFormat.ShortDate)) = FormatNumber(pTotalAR1, 2, TriState.True, TriState.True)                

                'Loop Participant's AR Bills
                For Each itmARBill In pTotalARBills
                    'Check AR Bills If Defaulted
                    If itmARBill.NewDueDate < SystemDate Then
                        If itmARBill.ChargeType <> EnumChargeType.EV Then
                            WBillDefaulted.Add(itmARBill)
                        End If
                    End If
                Next

            Next

            Dim PrudentialAmount = WBillHelper.GetParticipantPrudential(_pARBills.FirstOrDefault.IDNumber.IDNumber).PrudentialAmount
            Dim TotalARperParticipant = _pARBills.Sum(Function(x As WESMBillSummary) x.EndingBalance)
            Dim DiffPROB = TotalARperParticipant + PrudentialAmount

            dr("Prudential Security") = FormatNumber(PrudentialAmount, 2, TriState.True, TriState.True)
            dr("DifferenceOf OBalance-PR") = FormatNumber(DiffPROB, 2, TriState.True, TriState.True)
            dr("Total") = FormatNumber(TotalARperParticipant, 2, TriState.True, TriState.True)

            retGenSummary.Rows.Add(dr)
            retGenSummary.AcceptChanges()

        Next

        retGenSummary.Rows.Add(retGenSummary.NewRow)
        retGenSummary.Rows.Add(retGenSummary.NewRow)

        'Get AP Outstanding Balances
        'Get AP Participants

        'Total Receivables in Percentage
        initRow = retGenSummary.NewRow
        initRow2 = retGenSummary.NewRow
        Dim TotalAPBills1 = (From x In WBillSummary _
                            Where x.EndingBalance > 0 _
                            And x.ChargeType = chargeType1 _
                            Select x.EndingBalance).Sum

        Dim TotalAPBills3 = (From x In WBillSummary _
                            Where x.EndingBalance > 0 _
                            Select x.EndingBalance).Sum

        For Each BPDueDate In _fltrBPDueDate
            Dim _BPDueDate = BPDueDate
            'Get Total Sum Per BP
            Dim TotalAPPerBP1 = (From x In WBillSummary _
                                Where x.EndingBalance > 0 _
                                And x.BillPeriod = _BPDueDate.BillPeriod _
                                And x.DueDate = _BPDueDate.DueDate _
                                And x.ChargeType = chargeType1 _
                                Select x.EndingBalance).Sum

            'Compute Percentage
            Dim Pct1 = TotalAPPerBP1 / TotalAPBills1 * 100

            initRow(WBSummaryPrefix1 & " (" & _BPDueDate.BillPeriod & ")" & vbCrLf & FormatDateTime(_BPDueDate.DueDate, DateFormat.ShortDate)) = Pct1 & "%"
            initRow2(WBSummaryPrefix1 & " (" & _BPDueDate.BillPeriod & ")" & vbCrLf & FormatDateTime(_BPDueDate.DueDate, DateFormat.ShortDate)) = FormatNumber(TotalAPPerBP1, 2, TriState.True, TriState.True)
        Next
        retGenSummary.Rows.Add(initRow)
        retGenSummary.AcceptChanges()

        initRow2("Total") = FormatNumber(TotalAPBills1, 2, TriState.True, TriState.True)
        retGenSummary.Rows.Add(initRow2)
        retGenSummary.AcceptChanges()

        Dim _fltrAPParticipants = (From x In WBillSummary _
                                Where x.EndingBalance > 0 _
                                Select x.IDNumber.IDNumber Distinct Order By IDNumber Ascending).ToList

        For Each itmParticipant In _fltrAPParticipants
            Dim _itmParticipant = itmParticipant
            Dim APBills = (From x In WBillSummary _
                            Where x.EndingBalance > 0 _
                            And x.IDNumber.IDNumber = _itmParticipant _
                            Select x).ToList

            Dim dr As DataRow
            dr = retGenSummary.NewRow

            For Each itmBPDueDate In _fltrBPDueDate
                Dim _itmBPDueDate = itmBPDueDate

                Dim TotAP As Decimal = 0
                'Get Participant's Bills
                Dim _pAPBills1 = (From x In APBills _
                                    Where x.BillPeriod = _itmBPDueDate.BillPeriod _
                                    And x.ChargeType = chargeType1 _
                                    And x.DueDate = _itmBPDueDate.DueDate _
                                    Select x).ToList

                Dim _pAPTotal1 = (From x In _pAPBills1 _
                                    Select x.EndingBalance).Sum

                'If _pAPBills1.Count = 0 Then
                '    Continue For
                'End If
                If _pAPBills1.Count > 0 Then
                    dr("Participant ID") = _pAPBills1.First.IDNumber.ParticipantID
                    dr("Settlement ID") = _pAPBills1.First.IDNumber.IDNumber
                    dr("Region") = (From x In _AllParticipants _
                                    Where x.IDNumber = _pAPBills1.FirstOrDefault.IDNumber.IDNumber _
                                    Select x.Region).FirstOrDefault  '_pARBills.First.IDNumber.                    
                End If
                dr(WBSummaryPrefix1 & " (" & _itmBPDueDate.BillPeriod & ")" & vbCrLf & FormatDateTime(_itmBPDueDate.DueDate, DateFormat.ShortDate)) = FormatNumber(_pAPTotal1, 2, TriState.True, TriState.True)
            Next

            'Loop Participant's AP Bills
            dr("Total") = FormatNumber(APBills.Sum(Function(x As WESMBillSummary) x.EndingBalance), 2, TriState.True, TriState.True)

            retGenSummary.Rows.Add(dr)
            retGenSummary.AcceptChanges()
        Next        

        Return retGenSummary
    End Function

    Private Function GenerateOutstandingSummaryMF(ByVal Charge As String) As DataTable
        Dim retGenSummary As New DataTable

        Dim WBillSummary As New List(Of WESMBillSummary)
        Dim WBSummaryPrefix1 As String = ""
        Dim WBSummaryPrefix2 As String = ""

        WBillSummary = WBillHelper.GetWESMBillSummary()
        WBillSummary = (From x In WBillSummary _
                        Where x.ChargeType <> EnumChargeType.E _
                        And x.ChargeType <> EnumChargeType.EV _
                        Select x).ToList
        WBSummaryPrefix1 = EnumChargeType.MF.ToString
        WBSummaryPrefix2 = EnumChargeType.MFV.ToString

        Dim _AllParticipants = Me.WBillHelper.GetAMParticipants()

        'Get Distinct BP and DueDates
        Dim _fltrBPDueDate = (From x In WBillSummary _
                              Select x.BillPeriod, x.DueDate Distinct _
                              Order By BillPeriod Ascending, DueDate Ascending).ToList

        If WBillSummary.Count = 0 Then
            Throw New ApplicationException("No Outstanding Balances found!")
        End If

        retGenSummary.Columns.Add("Participant ID")
        retGenSummary.Columns.Add("Settlement ID")
        retGenSummary.Columns.Add("Region")

        For Each itmBPDueDate In _fltrBPDueDate
            With retGenSummary.Columns
                .Add(WBSummaryPrefix1 & " (" & itmBPDueDate.BillPeriod.ToString & ")" & vbCrLf & FormatDateTime(itmBPDueDate.DueDate, DateFormat.ShortDate))
                .Add(WBSummaryPrefix2 & " (" & itmBPDueDate.BillPeriod.ToString & ")" & vbCrLf & FormatDateTime(itmBPDueDate.DueDate, DateFormat.ShortDate))                
            End With
        Next

            Dim chargeType1 As Integer = DirectCast(EnumChargeType.Parse(GetType(EnumChargeType), WBSummaryPrefix1), EnumChargeType) 'Get selected chargetype
            Dim chargeType2 As Integer = DirectCast(EnumChargeType.Parse(GetType(EnumChargeType), WBSummaryPrefix2), EnumChargeType) 'Get selected chargetype

            Dim TotalARBills1 = (From x In WBillSummary _
                                Where x.EndingBalance < 0 _
                                And x.ChargeType = chargeType1 _
                                Select x.EndingBalance).Sum 'TotalARBills for Energy or MF

            Dim TotalARBills2 = (From x In WBillSummary _
                                Where x.EndingBalance < 0 _
                                And x.ChargeType = chargeType2 _
                                Select x.EndingBalance).Sum 'TotalARBills for EnergyVAT or MFVAT


            'Total Receivables in Percentage
            Dim initRow As DataRow
            initRow = retGenSummary.NewRow
            For Each BPDueDate In _fltrBPDueDate
                Dim _BPDueDate = BPDueDate
                'Get Total Sum Per BP
                Dim TotalARPerBP1 = (From x In WBillSummary _
                                    Where x.EndingBalance < 0 _
                                    And x.BillPeriod = _BPDueDate.BillPeriod _
                                    And x.DueDate = _BPDueDate.DueDate _
                                    And x.ChargeType = chargeType1 _
                                    Select x.EndingBalance).Sum

                Dim TotalARPerBP2 = (From x In WBillSummary _
                                    Where x.EndingBalance < 0 _
                                    And x.BillPeriod = _BPDueDate.BillPeriod _
                                    And x.DueDate = _BPDueDate.DueDate _
                                    And x.ChargeType = chargeType2 _
                                    Select x.EndingBalance).Sum

                If TotalARPerBP1 = 0 And TotalARBills2 = 0 Then
                    Exit For
                End If
                'Compute Percentage
                Dim Pct1 = TotalARPerBP1 / TotalARBills1 * 100
                Dim Pct2 = TotalARPerBP2 / TotalARBills2 * 100

            initRow(WBSummaryPrefix1 & " (" & _BPDueDate.BillPeriod & ")" & vbCrLf & FormatDateTime(_BPDueDate.DueDate, DateFormat.ShortDate)) = Pct1 & "%"
            initRow(WBSummaryPrefix2 & " (" & _BPDueDate.BillPeriod & ")" & vbCrLf & FormatDateTime(_BPDueDate.DueDate, DateFormat.ShortDate)) = Pct2 & "%"
            Next

            retGenSummary.Columns.Add("Prudential Security")
            retGenSummary.Columns.Add("DifferenceOf OBalance-PR")
            retGenSummary.Columns.Add("Total")
            retGenSummary.Columns.Add("Remarks")

            retGenSummary.Rows.Add(initRow)
            retGenSummary.AcceptChanges()

            'Total Receivables per Billing Period
            'Get Total AR For All Bills
            Dim initRow2 As DataRow
            initRow2 = retGenSummary.NewRow
            For Each BPDueDate In _fltrBPDueDate
                Dim _BPDueDate = BPDueDate
                'Get Total Sum Per BP
                Dim TotalARPerBP1 = (From x In WBillSummary _
                                   Where x.EndingBalance < 0 _
                                   And x.BillPeriod = _BPDueDate.BillPeriod _
                                   And x.DueDate = _BPDueDate.DueDate _
                                   And x.ChargeType = chargeType1 _
                                   Select x.EndingBalance).Sum

                Dim TotalARPerBP2 = (From x In WBillSummary _
                                    Where x.EndingBalance < 0 _
                                    And x.BillPeriod = _BPDueDate.BillPeriod _
                                    And x.DueDate = _BPDueDate.DueDate _
                                    And x.ChargeType = chargeType2 _
                                    Select x.EndingBalance).Sum


                If TotalARPerBP1 = 0 And TotalARBills2 = 0 Then
                    Exit For
                End If
                initRow2(WBSummaryPrefix1 & " (" & _BPDueDate.BillPeriod & ")" & vbCrLf & FormatDateTime(_BPDueDate.DueDate, DateFormat.ShortDate)) = FormatNumber(TotalARPerBP1, 2, TriState.True, TriState.True)
                initRow2(WBSummaryPrefix2 & " (" & _BPDueDate.BillPeriod & ")" & vbCrLf & FormatDateTime(_BPDueDate.DueDate, DateFormat.ShortDate)) = FormatNumber(TotalARPerBP2, 2, TriState.True, TriState.True)
            Next

            initRow2("Total") = FormatNumber(TotalARBills1 + TotalARBills2, 2, TriState.True, TriState.True)
            retGenSummary.Rows.Add(initRow2)
            retGenSummary.AcceptChanges()


            'Get AR Outstanding Balances
            'Get Participant with AR
            Dim _fltrARParticipants = (From x In WBillSummary _
                                       Where x.EndingBalance < 0 _
                                       Select x.IDNumber.IDNumber Distinct Order By IDNumber Ascending).ToList


            For Each itmParticipant In _fltrARParticipants
                Dim _itmParticipant = itmParticipant

                'Get Participant's Bills
                Dim _pARBills = (From x In WBillSummary _
                                 Where x.EndingBalance < 0 _
                                 And x.IDNumber.IDNumber = _itmParticipant _
                                 Select x).ToList
                Dim dr As DataRow
                dr = retGenSummary.NewRow
                'Filter per BP and DueDate
                For Each BPDueDate In _fltrBPDueDate
                    Dim _BPDueDate = BPDueDate

                    dr("Participant ID") = _pARBills.First.IDNumber.ParticipantID
                    dr("Settlement ID") = _pARBills.First.IDNumber.IDNumber
                    dr("Region") = (From x In _AllParticipants _
                                    Where x.IDNumber = _pARBills.FirstOrDefault.IDNumber.IDNumber _
                                    Select x.Region).FirstOrDefault '_pARBills.First.IDNumber.

                    'Total AR of Bills for BP/DueDate
                    Dim pTotalAR1 = (From x In _pARBills _
                                        Where x.BillPeriod = _BPDueDate.BillPeriod _
                                        And x.DueDate = _BPDueDate.DueDate _
                                        And x.ChargeType = chargeType1 _
                                        Select x.EndingBalance).Sum

                    Dim pTotalAR2 = (From x In _pARBills _
                                        Where x.BillPeriod = _BPDueDate.BillPeriod _
                                        And x.DueDate = _BPDueDate.DueDate _
                                        And x.ChargeType = chargeType2 _
                                        Select x.EndingBalance).Sum

                    Dim pTotalARBills = (From x In _pARBills _
                                         Where x.BillPeriod = _BPDueDate.BillPeriod _
                                         And x.DueDate = _BPDueDate.DueDate _
                                         Select x).ToList

                    dr(WBSummaryPrefix1 & " (" & _BPDueDate.BillPeriod.ToString & ")" & vbCrLf & FormatDateTime(_BPDueDate.DueDate, DateFormat.ShortDate)) = FormatNumber(pTotalAR1, 2, TriState.True, TriState.True)
                    dr(WBSummaryPrefix2 & " (" & _BPDueDate.BillPeriod.ToString & ")" & vbCrLf & FormatDateTime(_BPDueDate.DueDate, DateFormat.ShortDate)) = FormatNumber(pTotalAR2, 2, TriState.True, TriState.True)

                    'Loop Participant's AR Bills
                    For Each itmARBill In pTotalARBills
                        'Check AR Bills If Defaulted
                        If itmARBill.NewDueDate < SystemDate Then
                            If itmARBill.ChargeType <> EnumChargeType.EV Then
                                WBillDefaulted.Add(itmARBill)
                            End If
                        End If
                    Next

                Next


                Dim PrudentialAmount = WBillHelper.GetParticipantPrudential(_pARBills.FirstOrDefault.IDNumber.IDNumber).PrudentialAmount
                Dim TotalARperParticipant = _pARBills.Sum(Function(x As WESMBillSummary) x.EndingBalance)
                Dim DiffPROB = TotalARperParticipant + PrudentialAmount

                dr("Prudential Security") = FormatNumber(PrudentialAmount, 2, TriState.True, TriState.True)
                dr("DifferenceOf OBalance-PR") = FormatNumber(DiffPROB, 2, TriState.True, TriState.True)
                dr("Total") = FormatNumber(TotalARperParticipant, 2, TriState.True, TriState.True)

                retGenSummary.Rows.Add(dr)
                retGenSummary.AcceptChanges()

            Next

        Return retGenSummary

    End Function

    Private Sub cmd_search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_search.Click
        Dim ChargeSelected As String = ""

        Try
            
            If rb_Energy.Checked = True Then
                ChargeSelected = "ENERGY"
                Me.dgv_OutstandingSummary.DataSource = Me.GenerateOutstandingSummary(ChargeSelected)            
            ElseIf rb_MF.Checked = True Then
                ChargeSelected = "MF"
                Me.dgv_OutstandingSummary.DataSource = Me.GenerateOutstandingSummaryMF(ChargeSelected)
            ElseIf rb_VATonEnergy.Checked = True Then
                ChargeSelected = "VATONENERGY"
                Me.dgv_OutstandingSummary.DataSource = Me.GenerateOutstandingSummary(ChargeSelected)
            End If

            'Remove column sorting
            For cctr = 0 To dgv_OutstandingSummary.ColumnCount - 1
                dgv_OutstandingSummary.Columns(cctr).SortMode = DataGridViewColumnSortMode.Programmatic
            Next

            'Alignment of Gridview Column
            'For cCtr = 2 To dgv_OutstandingSummary.ColumnCount - 1
            '    dgv_OutstandingSummary.Columns(cCtr).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'Next
        Catch ex As Exception
            MessageBox.Show("No Outstanding Balance Found", "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        
    End Sub

    Private Sub cmd_GenerateReportToExcel_Click(sender As Object, e As EventArgs) Handles cmd_GenerateReportToExcel.Click
        Dim ds As New DataSet
        Dim GenSummary As New DataTable        
        Dim ChargeSelected As String = ""

        If rb_Energy.Checked = True Then
            ChargeSelected = "Energy"
        End If

        If rb_MF.Checked = True Then
            ChargeSelected = "MF"
        End If

        If rb_VATonEnergy.Checked = True Then
            ChargeSelected = "VATOnEnergy"
        End If


        Dim FileName As String = Me.txt_DestFile.Text & "\Summary of Outstanding Balances for " & ChargeSelected & " As Of " & Replace(FormatDateTime(Now, DateFormat.ShortDate), "/", "") & ".xls"
        Try

            If Trim(Me.txt_DestFile.Text).Length = 0 Then
                MsgBox("Please select a destination folder.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            If Me.dgv_OutstandingSummary.RowCount = 0 Then
                MsgBox("There are no records to export.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If
            ProgressThread.Show("Please wait while generating Excel File.")
            Dim _dt As New DataTable
            _dt = CType(Me.dgv_OutstandingSummary.DataSource, DataTable)

            Me.WBillHelper.DataTable2Excel(_dt, FileName)
            ProgressThread.Close()
            MsgBox("Successfully completed generation of Summary of Outstanding Balances", MsgBoxStyle.Information, Me.Name)

        Catch ex As Exception
            ProgressThread.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
            Exit Sub        
        End Try
    End Sub

End Class