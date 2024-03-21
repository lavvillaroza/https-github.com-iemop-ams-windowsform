'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmWESMInvoice
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     February 05, 2012
'Development Group:      Software Development and Support Division
'Description:            Viewing for WESM Invoice
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   February 05, 2012       Vladimir E. Espiritu            GUI initialization.
'   March 13, 2012          Vladimir E. Espiritu            Rename the filename of PDF upon downloading


Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementLogic
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports WESMLib.Auth.Lib

Public Class frmWESMInvoice
    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory

    Private _listCharges As List(Of ChargeId)
    Private _listCalendarBP As List(Of CalendarBillingPeriod)
    Private _listParticipants As List(Of AMParticipants)
    Private _listFilters As List(Of WESMInvoice)

    Private systemGeneratedMessage As String = ""

    Private Enum FilterType
        Energy
        MarketFees
        BillingPeriod
        SettlementRun
    End Enum

    Private Sub frmWESMInvoice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MdiParent = MainForm

        WBillHelper = WESMBillHelper.GetInstance()        
        BFactory = BusinessFactory.GetInstance()

        Me.ClearControls()
        Me.LoadInputs()

        AddHandler Me.rbEnergy.CheckedChanged, AddressOf Me.rbChargeTypeCheckedChange
        AddHandler Me.rbMarketFees.CheckedChanged, AddressOf Me.rbChargeTypeCheckedChange
        AddHandler ddlBillingPeriod.SelectedIndexChanged, AddressOf Me.ddlBillingPeriod_SelectedIndexChanged

        Me.LoadDefaultValue()
    End Sub

    Private Sub rbChargeTypeCheckedChange(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.rbEnergy.Checked Then
            Me.LoadFilters(FilterType.Energy)
        Else
            Me.LoadFilters(FilterType.MarketFees)
        End If
    End Sub

    Private Sub ddlBillingPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.LoadFilters(FilterType.BillingPeriod)
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.ClearControls()
        Me.LoadInputs()
        Me.LoadDefaultValue()
    End Sub

    Private Sub chckAll_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chckAll.CheckStateChanged
        If Me.chckAll.Checked Then
            For index As Integer = 0 To Me.chckList.Items.Count - 1
                Me.chckList.SetItemChecked(index, True)
            Next
        Else
            For index As Integer = 0 To Me.chckList.Items.Count - 1
                Me.chckList.SetItemChecked(index, False)
            Next
        End If
    End Sub

    Private Sub btnShowReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowReport.Click        
        Try
            If Not Me.Validation Then
                Exit Sub
            End If
            ProgressThread.Show("Please wait while printing the report.")
            Dim listWESMInvoice As New List(Of WESMInvoice)
            Dim listWESMSalesAndPurchased As New List(Of WESMBillSalesAndPurchased)
            Dim listSelectedParticipant As New List(Of String)
            Dim fileType As EnumFileType

            If Me.rbEnergy.Checked Then
                fileType = EnumFileType.Energy
            Else
                fileType = EnumFileType.MarketFees
            End If

            'Get the WESM Invoice
            If Me.rbEnergy.Checked Then
                listWESMInvoice = WBillHelper.GetWESMInvoices(CInt(Me.ddlBillingPeriod.Text), Me.ddlSTLRun.Text, _
                                                               EnumFileType.Energy)
            Else
                listWESMInvoice = WBillHelper.GetWESMInvoices(CInt(Me.ddlBillingPeriod.Text), Me.ddlSTLRun.Text, _
                                                              EnumFileType.MarketFees)
            End If

            'Get the selected participants
            For cnt As Integer = 0 To Me.chckList.CheckedItems.Count - 1
                listSelectedParticipant.Add(Me.chckList.CheckedItems(cnt).ToString())
            Next

            'Get the WESM Sales and Purchased if Energy
            If fileType = EnumFileType.Energy Then
                listWESMSalesAndPurchased = WBillHelper.GetWESMInvoiceSalesAndPurchased(CInt(Me.ddlBillingPeriod.Text), Me.ddlSTLRun.Text)
            End If

            'Get the signatories for WESM Invoice
            Dim Signatory = WBillHelper.GetSignatories("INV").First()

            Dim getMarketFeesRate As String = ""

            If fileType = EnumFileType.MarketFees Then
                getMarketFeesRate = (From x In listWESMInvoice Select x.MarketFeesRate).FirstOrDefault.ToString
            End If

            'Get the dataset for WESM Invoice
            Dim ds = BFactory.GenerateWESMInvoice(New DSReport.WESMInvoiceDataTable, New DSReport.WESMInvoiceDetailsDataTable, _
                                                  listWESMInvoice, listWESMSalesAndPurchased, Me._listParticipants, _
                                                  Me._listCharges, Me._listCalendarBP, listSelectedParticipant, Signatory, fileType)

            If ds.Tables("WESMInvoice").Rows.Count = 0 Then
                ProgressThread.Close()
                MsgBox("No WESM Invoice for the selected participant/s", MsgBoxStyle.Information, "No data")
                Exit Sub
            End If

            Dim chargeType As EnumChargeType
            If Me.rbEnergy.Checked = True Then
                chargeType = EnumChargeType.E
            Else
                chargeType = EnumChargeType.MF
            End If
            Dim frmViewer As New frmReportViewer()
            With frmViewer
                .LoadWESMInvoice(ds, chargeType, getMarketFeesRate, systemGeneratedMessage)
                ProgressThread.Close()
                .ShowDialog()
            End With
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)        
        End Try
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim frmProg As New frmProgress
        Try

            Dim fileName As String

            If Not Me.Validation Then
                Exit Sub
            End If

            If Me.txtDestination.Text.Length = 0 Then
                MsgBox("Please specify first the destination folder", MsgBoxStyle.Critical, "Specify the data")
                Exit Sub
            End If

            Dim ans As MsgBoxResult

            ans = MsgBox("Do you really want to download the WESM Invoice/s?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Download")

            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            ProgressThread.Show("Please wait while preparing the file.")

            Dim listWESMInvoice As New List(Of WESMInvoice)
            Dim listWESMSalesAndPurchased As New List(Of WESMBillSalesAndPurchased)
            Dim listSelectedParticipant As New List(Of String)
            Dim fileType As EnumFileType, fileTypeValue As String

            If Me.rbEnergy.Checked Then
                fileType = EnumFileType.Energy
                fileTypeValue = "INV"
            Else
                fileType = EnumFileType.MarketFees
                fileTypeValue = "MF"
            End If

            'Get the WESM Invoice
            If Me.rbEnergy.Checked Then
                listWESMInvoice = WBillHelper.GetWESMInvoices(CInt(Me.ddlBillingPeriod.Text), Me.ddlSTLRun.Text, _
                                                               EnumFileType.Energy)
            Else
                listWESMInvoice = WBillHelper.GetWESMInvoices(CInt(Me.ddlBillingPeriod.Text), Me.ddlSTLRun.Text, _
                                                              EnumFileType.MarketFees)
            End If

            'Get the selected participants
            For cnt As Integer = 0 To Me.chckList.CheckedItems.Count - 1
                listSelectedParticipant.Add(Me.chckList.CheckedItems(cnt).ToString())
            Next

            'Get the WESM Sales and Purchased if Energy
            If fileType = EnumFileType.Energy Then
                listWESMSalesAndPurchased = WBillHelper.GetWESMInvoiceSalesAndPurchased(CInt(Me.ddlBillingPeriod.Text), Me.ddlSTLRun.Text)
            End If

            'Get the signatories for WESM Invoice
            Dim Signatory = WBillHelper.GetSignatories("INV").First()

            Dim getMarketFeesRate As String = ""

            If fileType = EnumFileType.MarketFees Then
                getMarketFeesRate = (From x In listWESMInvoice Select x.MarketFeesRate).FirstOrDefault.ToString
            End If

            'Get the dataset for WESM Invoice
            Dim ds = BFactory.GenerateWESMInvoice(New DSReport.WESMInvoiceDataTable, New DSReport.WESMInvoiceDetailsDataTable, _
                                                  listWESMInvoice, listWESMSalesAndPurchased, Me._listParticipants, _
                                                  Me._listCharges, Me._listCalendarBP, listSelectedParticipant, Signatory, fileType)


            If ds.Tables("WESMInvoice").Rows.Count = 0 Then
                ProgressThread.Close()
                MsgBox("No WESM Invoice for the selected participant/s", MsgBoxStyle.Information, "No data")
                Exit Sub
            End If

            'Get the billing period
            Dim bp = (From x In Me._listCalendarBP _
                      Where x.BillingPeriod = CInt(Me.ddlBillingPeriod.Text) _
                      Select x).First()

            Dim bpValue As String = bp.EndDate.ToString("MMMyyyy").ToUpper
            fileName = bp.BillingPeriod & "" & Me.ddlSTLRun.Text.Replace("R", "F") & "-"

            For cnt As Integer = 0 To Me.chckList.CheckedItems.Count - 1
                Dim participant = Me.chckList.CheckedItems(cnt).ToString()

                Dim itemCount = (From x In ds.Tables("WESMInvoice").AsEnumerable() _
                                  Where x("PARTICIPANT_ID").ToString() = participant _
                                  Select x).Count()

                If itemCount = 0 Then
                    Continue For
                End If

                Dim listInvoice = From x In ds.Tables("WESMInvoice").AsEnumerable() _
                                   Where x("PARTICIPANT_ID").ToString() = participant _
                                   Select x("BILL_NUMBER") Distinct

                For Each item In listInvoice
                    Dim seletectedInvoice = item.ToString()

                    Dim dtMain = (From x In ds.Tables("WESMInvoice").AsEnumerable() _
                                  Where x("BILL_NUMBER").ToString() = seletectedInvoice _
                                  Select x).CopyToDataTable()

                    Dim BillNumber As String = CStr(dtMain.Rows(0)("BILL_NUMBER"))

                    Dim dtDetails = (From x In ds.Tables("WESMInvoiceDetails").AsEnumerable() _
                                     Where x("BILL_NUMBER").ToString() = seletectedInvoice _
                                     Select x).CopyToDataTable()

                    Dim dsReport = New DataSet()
                    dtMain.TableName = "WESMInvoice"
                    dtDetails.TableName = "WESMInvoiceDetails"
                    dsReport.Tables.Add(dtMain)
                    dsReport.Tables.Add(dtDetails)


                    Dim expReport = New RPTWESMInvoice
                    expReport.SetDataSource(dsReport)
                    expReport.SetParameterValue("paramBIRDateIssued", AMModule.BIRDateIssued)
                    expReport.SetParameterValue("paramBIRValidUntil", AMModule.BIRValidUntil)
                    expReport.SetParameterValue("paramSeriesNo", AMModule.FSNumberPrefix.ToString)
                    expReport.SetParameterValue("paramMarketFeeRate", getMarketFeesRate)
                    expReport.SetParameterValue("paramSystemGeneratedMsg", systemGeneratedMessage)

                    Dim stlRun As String = Me.ddlSTLRun.Text
                    If stlRun.StartsWith("R") Then
                        If fileType = EnumFileType.MarketFees Then
                            fileTypeValue = "RMF"
                        End If
                        If stlRun.Length <> 1 Then
                            expReport.ExportToDisk(ExportFormatType.PortableDocFormat, Me.txtDestination.Text & "\" &
                                               participant & "_TS-RAD-" & fileName & seletectedInvoice.Replace("FS-W", "") & "_" & fileTypeValue & ".pdf")
                        Else
                            expReport.ExportToDisk(ExportFormatType.PortableDocFormat, Me.txtDestination.Text & "\" &
                                               participant & "_TS-RF-" & fileName & seletectedInvoice.Replace("FS-W", "") & "_" & fileTypeValue & ".pdf")
                        End If
                    Else
                        If stlRun.Length <> 1 Then
                            expReport.ExportToDisk(ExportFormatType.PortableDocFormat, Me.txtDestination.Text & "\" &
                                               participant & "_TS-WAD-" & fileName & seletectedInvoice.Replace("FS-W", "") & "_" & fileTypeValue & ".pdf")
                        Else
                            expReport.ExportToDisk(ExportFormatType.PortableDocFormat, Me.txtDestination.Text & "\" &
                                               participant & "_TS-WF-" & fileName & seletectedInvoice.Replace("FS-W", "") & "_" & fileTypeValue & ".pdf")
                        End If
                    End If
                    expReport.Close()
                    expReport.Dispose()
                Next
            Next

            ProgressThread.Close()
            MsgBox("Successfully downloaded!", MsgBoxStyle.Information, "Downloaded")
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)        
        End Try
    End Sub

    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
        Dim fOpen As New FolderBrowserDialog
        fOpen.ShowDialog()
        Me.txtDestination.Text = fOpen.SelectedPath
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#Region "Methods/Functions"
    Private Sub LoadFilters(ByVal filterValue As FilterType)
        Select Case filterValue
            Case FilterType.Energy
                Dim BPs = (From x In Me._listFilters _
                           Where x.FileType = EnumFileType.Energy _
                           Select x.BillingPeriod Distinct Order By BillingPeriod Descending).ToList()

                With Me.ddlBillingPeriod
                    .DisplayMember = "BillingPeriod"
                    .ValueMember = "Rangedate"
                    .DataSource = BPs
                    .SelectedIndex = -1
                End With

                Me.ddlSTLRun.DataSource = Nothing

            Case FilterType.MarketFees
                Dim BPs = (From x In Me._listFilters _
                           Where x.FileType = EnumFileType.MarketFees _
                           Select x.BillingPeriod Distinct Order By BillingPeriod Descending).ToList()

                With Me.ddlBillingPeriod
                    .DisplayMember = "BillingPeriod"
                    .ValueMember = "Rangedate"
                    .DataSource = BPs
                    .SelectedIndex = -1
                End With

                Me.ddlSTLRun.DataSource = Nothing

            Case FilterType.BillingPeriod
                If Me.ddlBillingPeriod.Text.Trim.Length = 0 Then
                    Exit Sub
                End If

                Dim STLRuns As List(Of String)
                If Me.rbEnergy.Checked Then
                    STLRuns = (From x In Me._listFilters Where x.FileType = EnumFileType.Energy _
                               And x.BillingPeriod = CInt(Me.ddlBillingPeriod.Text) _
                               Select x.SettlementRun Distinct Order By SettlementRun).ToList()
                Else
                    STLRuns = (From x In Me._listFilters Where x.FileType = EnumFileType.MarketFees _
                               And x.BillingPeriod = CInt(Me.ddlBillingPeriod.Text) _
                               Select x.SettlementRun Distinct Order By SettlementRun).ToList()
                End If

                Me.ddlSTLRun.DataSource = STLRuns
                Me.ddlSTLRun.SelectedIndex = -1

        End Select
    End Sub

    Private Function Validation() As Boolean
        If Not Me.rbEnergy.Checked And Not Me.rbMarketFees.Checked Then
            MsgBox("Please select first the type of WESM Invoice to be shown!", MsgBoxStyle.Critical, "Specify the inputs")
            Exit Function
        ElseIf Me.ddlBillingPeriod.SelectedIndex = -1 Then
            MsgBox("Please select first the billing period!", MsgBoxStyle.Critical, "Specify the inputs")
            Exit Function
        ElseIf Me.ddlSTLRun.SelectedIndex = -1 Then
            MsgBox("Please select first the settlement run!", MsgBoxStyle.Critical, "Specify the inputs")
            Exit Function
        ElseIf Me.chckList.CheckedItems.Count = 0 Then
            MsgBox("Please select first the participant/s!", MsgBoxStyle.Critical, "Specify the inputs")
            Exit Function
        End If

        Return True
    End Function

    Private Sub LoadInputs()
        Me._listCharges = WBillHelper.GetChargeIDCodes()
        Me._listCalendarBP = WBillHelper.GetCalendarBP()
        Me._listParticipants = WBillHelper.GetAMParticipants()
        Me._listFilters = WBillHelper.GetFilterValuesForWESMInvoice()
    End Sub

    Private Sub ClearControls()
        Me.rbEnergy.Checked = False
        Me.rbMarketFees.Checked = False
        Me.ddlBillingPeriod.DataSource = Nothing
        Me.ddlBillingPeriod.SelectedIndex = -1
        Me.ddlSTLRun.DataSource = Nothing
        Me.ddlSTLRun.SelectedIndex = -1
        Me.chckAll.Checked = False
        Me.chckList.Items.Clear()
        Me.txtDestination.Text = ""
    End Sub

    Private Sub LoadDefaultValue()
        Me.rbEnergy.Checked = True
        Me.chckList.Items.Clear()

        For Each item In Me._listParticipants
            Me.chckList.Items.Add(item.ParticipantID)
        Next

    End Sub

    Private Function LoadWESMInvoice() As DataSet
        Dim fileType As EnumFileType
        Dim ds As New DataSet
        Dim billedTo As String, billNumber As String, billingDate As String
        Dim TotalTradingAmount As Decimal = 0D, Vatable As Decimal = 0D
        Dim VatOnEnergy As Decimal = 0D, VatZeroRated As Decimal = 0D
        Dim MarketFees As Decimal = 0D, VatOnMarketFees As Decimal = 0D
        Dim TotalAmountDue As Decimal = 0D, TotalQuantity As Decimal = 0D
        Dim SalesAmount As Decimal = 0D, VatOnSalesAmount As Decimal = 0D
        Dim PurchasedAmount As Decimal = 0D, VatOnPurchasedAmount As Decimal = 0D
        Dim GMRValue As Decimal = 0D
        Dim listWESMInvoice As New List(Of WESMInvoice)
        Dim listWESMSalesAndPurchased As New List(Of WESMBillSalesAndPurchased)

        If Me.rbEnergy.Checked Then
            fileType = EnumFileType.Energy
        Else
            fileType = EnumFileType.MarketFees
        End If

        Dim dtMain As New DSReport.WESMInvoiceDataTable
        Dim dtDetails As New DSReport.WESMInvoiceDetailsDataTable

        'Get the WESM Invoice
        If Me.rbEnergy.Checked Then
            listWESMInvoice = WBillHelper.GetWESMInvoices(CInt(Me.ddlBillingPeriod.Text), Me.ddlSTLRun.Text, _
                                                           EnumFileType.Energy)
        Else
            listWESMInvoice = WBillHelper.GetWESMInvoices(CInt(Me.ddlBillingPeriod.Text), Me.ddlSTLRun.Text, _
                                                          EnumFileType.MarketFees)
        End If

        'Get the WESM Sales and Purchased if Energy
        If fileType = EnumFileType.Energy Then
            listWESMSalesAndPurchased = WBillHelper.GetWESMInvoiceSalesAndPurchased(CInt(Me.ddlBillingPeriod.Text), Me.ddlSTLRun.Text)
        End If

        For cnt As Integer = 0 To Me.chckList.CheckedItems.Count - 1
            Dim participant = Me.chckList.CheckedItems(cnt).ToString()

            Dim listData = From w In listWESMInvoice Join x In Me._listParticipants On _
                           CStr(w.IDNumber) Equals x.IDNumber Join y In Me._listCharges On _
                           w.ChargeID Equals y.ChargeId Join z In Me._listCalendarBP On _
                           w.BillingPeriod Equals z.BillingPeriod _
                           Where x.ParticipantID = participant _
                           Select w.IDNumber, x.ParticipantID, x.ParticipantAddress, x.FullName, _
                           w.InvoiceNumber, w.InvoiceDate, z.StartDate, z.EndDate, w.DueDate, _
                           y.Description, y.cIDType, w.Quantity, w.Amount

            If listData.Count = 0 Then
                Continue For
            End If


            If fileType = EnumFileType.Energy Then
                TotalTradingAmount = (From x In listData _
                                      Where x.cIDType <> EnumChargeType.EV _
                                      Select x.Amount).Sum()

                VatOnEnergy = (From x In listData _
                               Where x.cIDType = EnumChargeType.EV _
                               Select x.Amount).Sum()

                Vatable = VatOnEnergy / AMModule.VatValue

                VatZeroRated = TotalTradingAmount - Vatable

                TotalAmountDue = (From x In listData _
                                  Select x.Amount).Sum()

                Dim listSales = (From x In listWESMSalesAndPurchased _
                                 Where x.InvoiceNumber = listData.First.InvoiceNumber _
                                 Select x).ToList()

                If listSales.Count() <> 0 Then
                    SalesAmount = listSales.First.VatableSales

                    VatOnSalesAmount = listSales.First.VATonSales

                    PurchasedAmount = listSales.First.VatablePurchases

                    VatOnPurchasedAmount = listSales.First.VATonPurchases

                    GMRValue = listSales.First.GMR
                End If
            Else
                MarketFees = (From x In listData _
                              Where x.cIDType = EnumChargeType.MF _
                              Select x.Amount).Sum()

                VatOnMarketFees = (From x In listData _
                                   Where x.cIDType = EnumChargeType.MFV _
                                   Select x.Amount).Sum()
            End If

            Dim invoice_number As String = "0000000" & listData.First.InvoiceNumber.ToString()
            billNumber = "INV" & Mid(invoice_number, invoice_number.Length - 6, 7)

            TotalQuantity = (From x In listData _
                             Select x.Quantity).Sum()


            Dim rowMain = dtMain.NewRow()
            With listData.First
                billedTo = .IDNumber & vbCrLf & .FullName & vbCrLf & .ParticipantAddress
                billingDate = .StartDate.ToString("MMM dd") & " - " & _
                              .EndDate.ToString("MMM dd, yyyy")

                rowMain("ID_NUMBER") = .IDNumber
                rowMain("PARTICIPANT_ID") = .ParticipantID
                rowMain("BILLED_TO") = billedTo
                rowMain("BILL_NUMBER") = billNumber
                rowMain("INVOICE_DATE") = .InvoiceDate
                rowMain("BILLING_PERIOD") = billingDate
                rowMain("DUE_DATE") = .DueDate
                rowMain("TOTAL_TRADING_AMOUNT") = TotalTradingAmount
                rowMain("VATABLE") = Vatable
                rowMain("VAT_ZERO_RATED") = VatZeroRated
                rowMain("VAT_ON_ENERGY") = VatOnEnergy
                rowMain("TOTAL_TRADING_AMOUNT_VAT") = TotalAmountDue
                rowMain("MARKET_FEES") = MarketFees
                rowMain("VAT_ON_MARKET_FEES") = VatOnMarketFees
                rowMain("TOTAL_AMOUNT_DUE") = TotalAmountDue
                rowMain("TOTAL_QUANTITY") = TotalQuantity
                rowMain("TOTAL_AMOUNT") = TotalAmountDue
                rowMain("SALES") = SalesAmount
                rowMain("PURCHASED") = VatOnSalesAmount
                rowMain("VAT_ON_SALES") = PurchasedAmount
                rowMain("VAT_ON_PURCHASED") = VatOnPurchasedAmount
                rowMain("GMR") = GMRValue
                dtMain.Rows.Add(rowMain)
                dtMain.AcceptChanges()
            End With

            For Each i In listData
                Dim rowDetails = dtDetails.NewRow()
                rowDetails("BILL_NUMBER") = billNumber
                rowDetails("DESCRIPTION") = i.Description
                rowDetails("QUANTITY") = i.Quantity
                rowDetails("AMOUNT") = i.Amount
                dtDetails.Rows.Add(rowDetails)
                dtDetails.AcceptChanges()
            Next
        Next

        ds.Tables.Add(dtMain)
        ds.Tables.Add(dtDetails)
        ds.AcceptChanges()

        Return ds
    End Function

#End Region


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            systemGeneratedMessage = "This is system generated invoice no signature required."
        Else
            systemGeneratedMessage = ""
        End If
    End Sub
End Class