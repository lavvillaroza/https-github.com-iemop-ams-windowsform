'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmReportCollectionNotice
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 29, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI for the Generation of Collection Notice Report
'Arguments/Parameters:  
'Files/Database Tables:  AM_COLLECTION_NOTICE, AM_COLLECTION_NOTICE_DETAILS, AM_WESM_BILL, 
'                        BILL_PARTICIPANTS
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description
'   October 03, 2011     Juan Carlo L. Panopio         Completed form GUI and Generation per participant
'	October 03, 2011     Juan Carlo L. Panopio         Completed Generation for all Participants
'	October 07, 2011     Juan Carlo L. Panopio         Adjusted Report generation to not to show previous transactions
'                                                      with Zero (0) Balance in AM_WESM_BILL
'	October 10, 2011     Juan Carlo L. Panopio         Added a method in order to generate report even there's no 
'                                                      Previous Balances of the participant.
'   November 24, 2011    Juan Carlo L. Panopio         Removed old code in populating Previous Collection Notice Details,
'                                                      Changed Previous Collection Notice Details from Per Invoice to Per Summary.
'                                                      Added Getting Participant Lineage in the chosen Billing Period.
'                                                      Added Code to exclude POSITIVE VALUES in Energy in the Collection Notice
'   November 30, 2011   Juan Carlo L. Panopio          Modified Collection Notice Generation according to New Parent-Child Mapping of Participants
'                                                      Participants Listed on Table/Dropdown will be all PARENT only and All Participants
'                                                      not yet assigned will not have any Collection Notice.
'   December 3, 2011    Juan Carlo L. Panopio          Finished Collection Notice Preparation - All Participants, Single Participant Generation
'   March 12, 2012      Juan Carlo L. Panopio          Changed frmCollectionNotice to frmcollectionNoticeMgt for Adding remarks and new form design
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
'Imports LDAPLib
'Imports LDAPLogin

'For Crystal Reports Export to PDF
Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports WESMLib.Auth.Lib


Public Class frmRPTStatementOfAccount
    Dim WBillHelper As WESMBillHelper

    Private AllParticipants As New List(Of AMParticipants)
    Private AllWESMBills As New List(Of WESMBillSummary)
    Private BFactory As New BusinessFactory
    Private lstSOAHeader As New List(Of CollectionNotice)
    Private lstSOADetails As New List(Of CollectionNoticeDetails)

    Private dicMPIDtoNumber As New Dictionary(Of String, String)
    Private dicNumberToMPID As New Dictionary(Of String, String)
    Private dicIDNumberToCtr As New Dictionary(Of String, Long)
    Private dicBillPeriodToCalendar As New Dictionary(Of Integer, String)
    Private dicInterestRate As New Dictionary(Of Date, Decimal)
    Private dicRemarks As New Dictionary(Of String, String)

    Private Sub frmRPTStatementOfAccount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString

        BFactory = BusinessFactory.GetInstance()

        AllParticipants = WBillHelper.GetAMParticipants()
        For Each itmParticipant In AllParticipants
            dicMPIDtoNumber.Add(itmParticipant.ParticipantID, itmParticipant.IDNumber)
            dicNumberToMPID.Add(itmParticipant.IDNumber, itmParticipant.ParticipantID)
        Next

        Dim BillingCalendar As New List(Of CalendarBillingPeriod)
        BillingCalendar = WBillHelper.GetCalendarBP()

        For Each itmBPCalendar In BillingCalendar
            With itmBPCalendar
                dicBillPeriodToCalendar.Add(.BillingPeriod, FormatDateTime(.StartDate, DateFormat.ShortDate) & "-" & FormatDateTime(.EndDate, DateFormat.ShortDate))
            End With
        Next

        'Populate Due Date CBO
        Me.cbo_dueDate.DataSource = Me.GenerateDueDate()
        If Me.cbo_dueDate.Items.Count = 0 Then
            'If no bills are present
            Exit Sub
        End If

    End Sub


#Region "Generate List/Combo boxes"
    'Based on the oustanding Bills 
    'Latest Due Date will be the current
    Function GenerateDueDate() As List(Of String)
        Dim retDueDates As New List(Of String)

        Dim lstWESMSummary As New List(Of WESMBillSummary)
        lstWESMSummary = Me.WBillHelper.GetWESMBillSummary()
        lstWESMSummary = (From x In lstWESMSummary _
                          Where x.EndingBalance < 0 _
                          Select x Order By x.DueDate Ascending).ToList

        'Get Distinct Due Dates
        Dim lstDueDate As New List(Of Date)
        lstDueDate = (From x In lstWESMSummary _
                      Select x.DueDate Distinct Order By DueDate Descending).ToList

        'Assign all AR Summary
        AllWESMBills = lstWESMSummary


        Dim _test = (From x In AllWESMBills _
                     Where x.IDNumber.IDNumber = "1361" _
                     Select x).ToList

        For Each itmDueDate In lstDueDate
            retDueDates.Add(FormatDateTime(itmDueDate, DateFormat.ShortDate))
        Next

        Return retDueDates
    End Function

    'Based on the Due Date
    Function GenerateParticipantList() As List(Of String)
        Dim retlstParticipant As New List(Of String)
        Dim DueDate As New Date
        DueDate = CDate(Me.cbo_dueDate.SelectedItem.ToString)

        lstSOAHeader.Clear()
        lstSOADetails.Clear()

        'Check for Existing records
        Dim _chkExisiting As New List(Of CollectionNotice)
        _chkExisiting = Me.WBillHelper.GetCollectionNotice(CDate(Me.cbo_dueDate.SelectedItem.ToString))
        If _chkExisiting.Count = 0 Then
            Dim fltrSummary As New List(Of WESMBillSummary)
            fltrSummary = (From x In AllWESMBills _
                           Where x.DueDate <= DueDate _
                           Select x Order By x.IDNumber.ParticipantID Ascending).ToList

            'get participant list
            Dim dstParticipant As New List(Of String)
            dstParticipant = (From x In fltrSummary _
                              Select x.IDNumber.IDNumber Distinct).ToList

            For Each itmParticipant In dstParticipant
                retlstParticipant.Add(dicNumberToMPID(itmParticipant))
            Next

            Me.cmd_preview.Enabled = True
            'Me.cmd_Remarks.Enabled = True
            'Me.cmd_remRemarks.Enabled = True
        Else
            For Each itmParticipant In _chkExisiting
                retlstParticipant.Add(dicNumberToMPID(itmParticipant.IDNumber))
            Next

            Dim _lstRefNo As New List(Of Long)
            _lstRefNo = (From x In _chkExisiting _
                         Select x.ReferenceNo).ToList

            lstSOAHeader = _chkExisiting
            lstSOADetails = Me.WBillHelper.GetCollectionNoticeDetails(_lstRefNo)
            dicIDNumberToCtr.Clear()
            For Each itmHeader In lstSOAHeader
                dicIDNumberToCtr.Add(itmHeader.IDNumber, itmHeader.ReferenceNo)
            Next

            Me.cmd_preview.Enabled = False
            'Me.cmd_Remarks.Enabled = False
            'Me.cmd_remRemarks.Enabled = False
        End If

        Return retlstParticipant
    End Function
#End Region

    Private Sub cbo_dueDate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo_dueDate.SelectedIndexChanged
        'Me.clb_participants.Items.Clear()
        'dicRemarks.Clear()
        'Me.txt_remarks.Text = ""
        ' Me.txt_SOANo.Text = "Type SOA No. here"
    End Sub

    Private Sub cmd_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub

    Private Sub cmd_GenerateReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_GenerateReport.Click
        Dim ans As New MsgBoxResult
        ans = MsgBox("Do you really want to generate the Statement Of Account for the due date " & Me.cbo_dueDate.SelectedItem.ToString & "?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "Generate Statement Of Account")

        If ans = MsgBoxResult.No Then
            MsgBox("Transaction Cancelled", MsgBoxStyle.Exclamation, "Generate Statement Of Account")
            Exit Sub
        End If

        Dim SOAMain As New List(Of StatementOfAccount)
        SOAMain = Me.WBillHelper.GetStatementOfAccount(CDate(Me.cbo_dueDate.SelectedItem.ToString))

        Dim Overwrite As New MsgBoxResult
        Dim isOverwrite As Boolean = False
        Dim isSOAPreviousPresent As Boolean = False
        
        If SOAMain.Count <> 0 Then
            Overwrite = MsgBox("Do you want to overwrite the previous Statement Of Account for the due date " & Me.cbo_dueDate.SelectedItem.ToString & "?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, MsgBoxStyle), "Overwrite Statement Of Account")
        End If

        SOAMain.Clear()

        If Overwrite = MsgBoxResult.No Then
            Exit Sub
        Else
            isOverwrite = True
        End If

        Dim SOACtr As Long
        SOACtr = 1

        Dim DateFrom As New Date
        Dim DateTo As New Date

        'Get Previous Statement of acocunt
        Dim PrevSOA As New List(Of StatementOfAccount)
        PrevSOA = Me.WBillHelper.GetStatementOfAccount()
        Dim itmPrevious As New StatementOfAccount
        itmPrevious = (From x In PrevSOA _
                       Where x._DueDate < Me.WBillHelper.GetSystemDate _
                       Select x Order By x._DueDate Descending).FirstOrDefault

        Dim _lstOutstandingBal As New List(Of WESMBillSummary)
        _lstOutstandingBal = Me.WBillHelper.GetWESMBillSummary()

        If itmPrevious IsNot Nothing Then
            DateFrom = itmPrevious._SOADate
            isSOAPreviousPresent = True
        Else           
            DateFrom = (From x In _lstOutstandingBal _
                        Where x.NewDueDate < Me.WBillHelper.GetSystemDate _
                        Select x.NewDueDate Order By NewDueDate Descending).FirstOrDefault
        End If

        DateTo = Me.WBillHelper.GetSystemDate

        _lstOutstandingBal = (From x In _lstOutstandingBal _
                      Where x.EndingBalance < 0 _
                      Select x).ToList
        'And x.EndingBalance <> x.EnergyWithhold _

        'Get Selected Participants from all outstanding balances
        Dim lstSelParticipant As New List(Of String)
        lstSelParticipant = (From x In _lstOutstandingBal _
                             Select x.IDNumber.IDNumber Distinct Order By IDNumber Ascending).ToList
        'And x.EndingBalance <> x.EnergyWithhold _

        'Generate if not yet present in the database
        'Get Current Invoices
        Dim _lstCurrentInvoice As New List(Of WESMBill)
        _lstCurrentInvoice = Me.WBillHelper.GetWESMBills(DateSerial(CDate(Me.cbo_dueDate.SelectedItem.ToString).Year, _
                                                                    CDate(Me.cbo_dueDate.SelectedItem.ToString).Month, _
                                                                    1), DateTo)

        'Get Collections
        Dim _lstCollections As New List(Of CollectionAllocation)
        _lstCollections = Me.WBillHelper.GetCollectionAllocation(DateFrom, DateTo, False)

        'Get Payment Offsetting
        Dim _lstPayment As New List(Of PaymentAllocationAccount)
        _lstPayment = Me.WBillHelper.GetPaymentAllocationAccount(DateFrom, DateTo)

        'As Of Date Generation

        For Each itmOutstanding In _lstOutstandingBal
            Dim _itmOutstanding = itmOutstanding
            Dim itmPayment = (From x In _lstPayment _
                              Where x.WESMBillSummary.WESMBillSummaryNo = _itmOutstanding.WESMBillSummaryNo _
                              Select x).FirstOrDefault
            If itmPayment Is Nothing Then
                Continue For
            End If

            itmPayment.WESMBillSummary = _itmOutstanding
        Next

        'WESM Bill History
        Dim _WESMTransactionHist As New List(Of WESMBillSummaryHistory)
        _WESMTransactionHist = Me.WBillHelper.GetWESMBillSummaryHistory(DateFrom, DateTo)

        'Compute for the Participant's Oustanding Balance
        For Each itmParticipant In lstSelParticipant
            Dim _SOAMain As New StatementOfAccount
            Dim _itmParticipant = itmParticipant

            'Participant's outstanding balance
            Dim EnergyOutBal As Decimal = 0
            Dim VAT As Decimal = 0
            Dim MF As Decimal = 0 'Inclusive of VAT
            Dim EnergyDefault As Decimal = 0
            Dim MFDefault As Decimal = 0

            'Participant's Collection and Payment
            Dim CollectionEnergy As Decimal = 0
            Dim CollectionVAT As Decimal = 0
            Dim CollectionMF As Decimal = 0
            Dim CollectionDefault As Decimal = 0

            Dim PaymentEnergy As Decimal = 0
            Dim PaymentVAT As Decimal = 0
            Dim PaymentMF As Decimal = 0
            Dim PaymentDefault As Decimal = 0

            Dim _lstParticipantBalance As New List(Of WESMBillSummary)
            _lstParticipantBalance = (From x In _lstOutstandingBal _
                                     Where x.IDNumber.IDNumber = _itmParticipant _
                                     And x.DueDate <> CDate(Me.cbo_dueDate.SelectedItem.ToString) _
                                     Select x).ToList

            Dim _lstTransactionHistory As New List(Of WESMBillSummaryHistory)
            Dim _lstWESMBillNo As New List(Of Long)
            _lstWESMBillNo = (From x In _lstParticipantBalance _
                              Select x.WESMBillSummaryNo).ToList

            _lstTransactionHistory = (From x In _WESMTransactionHist, _
                                      y In _lstWESMBillNo _
                                      Where x.WESMBillSummaryNo = y _
                                      Select x).ToList

            _SOAMain._SOANumber = SOACtr
            _SOAMain._IDNumber = _itmParticipant.ToString
            _SOAMain._DueDate = CDate(Me.cbo_dueDate.SelectedItem.ToString)
            _SOAMain._SOADate = CDate(FormatDateTime(Date.Now, DateFormat.ShortDate))

            CollectionEnergy = (From x In _lstTransactionHistory _
                                Where x.CollectionType = EnumCollectionType.Energy _
                                Select x.Amount).Sum
            Dim ColEnergy As New StatementOfAccountMainDetails
            With ColEnergy
                ._Amount = CollectionEnergy
                ._SOANumber = SOACtr
                ._SOAType = EnumSOAMainType.CollectionEnergy
            End With
            _SOAMain._lstDetails.Add(ColEnergy)


            CollectionVAT = (From x In _lstTransactionHistory _
                             Where x.CollectionType = EnumCollectionType.VatOnEnergy _
                             Select x.Amount).Sum

            Dim ColVAT As New StatementOfAccountMainDetails
            With ColVAT
                ._Amount = CollectionVAT
                ._SOANumber = SOACtr
                ._SOAType = EnumSOAMainType.CollectionVAT
            End With
            _SOAMain._lstDetails.Add(ColVAT)

            CollectionMF = (From x In _lstTransactionHistory _
                            Where x.CollectionType = EnumCollectionType.MarketFees _
                            Or x.CollectionType = EnumCollectionType.VatOnMarketFees _
                            Select x.Amount).Sum

            Dim ColMF As New StatementOfAccountMainDetails
            With ColMF
                ._Amount = CollectionMF
                ._SOANumber = SOACtr
                ._SOAType = EnumSOAMainType.CollectionMF
            End With
            _SOAMain._lstDetails.Add(ColMF)

            CollectionDefault = (From x In _lstTransactionHistory _
                                 Where x.CollectionType = EnumCollectionType.DefaultInterestOnMF _
                                 Or x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy _
                                 Or x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF _
                                 Select x.Amount).Sum

            Dim ColDefault As New StatementOfAccountMainDetails
            With ColDefault
                ._Amount = CollectionDefault
                ._SOANumber = SOACtr
                ._SOAType = EnumSOAMainType.CollectionDefault
            End With
            _SOAMain._lstDetails.Add(ColDefault)

            PaymentEnergy = (From x In _lstTransactionHistory _
                             Where x.PaymentType = EnumPaymentType.OffsetToCurrentReceivableEnergy _
                             Or x.PaymentType = EnumPaymentType.OffsetOfPreviousReceivableEnergy _
                             Select x.Amount).Sum

            Dim PayEnergy As New StatementOfAccountMainDetails
            With PayEnergy
                ._Amount = PaymentEnergy
                ._SOANumber = SOACtr
                ._SOAType = EnumSOAMainType.PaymentEnergy
            End With
            _SOAMain._lstDetails.Add(PayEnergy)

            PaymentVAT = (From x In _lstTransactionHistory _
                          Where x.PaymentType = EnumPaymentType.OffsetOfPreviousReceivableVAT _
                          Or x.PaymentType = EnumPaymentType.OffsetToCurrentReceivableVAT _
                          Select x.Amount).Sum

            Dim PayVAT As New StatementOfAccountMainDetails
            With PayVAT
                ._Amount = PaymentVAT
                ._SOANumber = SOACtr
                ._SOAType = EnumSOAMainType.PaymentVAT
            End With
            _SOAMain._lstDetails.Add(PayEnergy)

            PaymentMF = (From x In _lstTransactionHistory _
                         Where x.PaymentType = EnumPaymentType.UnpaidMF _
                         Or x.PaymentType = EnumPaymentType.UnpaidMFV _
                         Select x.Amount).Sum

            Dim PayMF As New StatementOfAccountMainDetails
            With PayMF
                ._Amount = PaymentMF
                ._SOANumber = SOACtr
                ._SOAType = EnumSOAMainType.PaymentVAT
            End With
            _SOAMain._lstDetails.Add(PayMF)

            PaymentDefault = (From x In _lstTransactionHistory _
                              Where x.PaymentType = EnumPaymentType.OffsetOfPreviousReceivableEnergyDefault _
                              Or x.PaymentType = EnumPaymentType.OffsetToCurrentReceivableEnergyDefault _
                              Or x.PaymentType = EnumPaymentType.UnpaidMFDefault _
                              Or x.PaymentType = EnumPaymentType.UnpaidMFVDefault _
                              Select x.Amount).Sum

            Dim PayDefault As New StatementOfAccountMainDetails
            With PayDefault
                ._Amount = PaymentMF
                ._SOANumber = SOACtr
                ._SOAType = EnumSOAMainType.PaymentDefault
            End With
            _SOAMain._lstDetails.Add(PayDefault)

            'Compute for Default Interest
            Dim DefaultDetails As New StatementOfAccountMainDetails
            With DefaultDetails
                ._Amount = PaymentDefault + CollectionDefault
                ._SOANumber = SOACtr
                ._SOAType = EnumSOAMainType.OutstandingDefault
            End With
            _SOAMain._lstDetails.Add(DefaultDetails)


            'Outstanding Balances

            EnergyOutBal = (From x In _lstParticipantBalance _
                            Where x.ChargeType = EnumChargeType.E _
                            Select x.EndingBalance).Sum
            Dim EnergyDetails As New StatementOfAccountMainDetails
            With EnergyDetails
                ._Amount = (Math.Abs(EnergyOutBal) + Math.Abs(CollectionEnergy) + Math.Abs(PaymentEnergy)) * -1D
                ._SOANumber = SOACtr
                ._SOAType = EnumSOAMainType.OutstandingE
            End With
            _SOAMain._lstDetails.Add(EnergyDetails)

            VAT = (From x In _lstParticipantBalance _
                            Where x.ChargeType = EnumChargeType.EV _
                            Select x.EndingBalance).Sum
            Dim VATDetails As New StatementOfAccountMainDetails
            With VATDetails
                ._Amount = (Math.Abs(VAT) + Math.Abs(CollectionVAT) + Math.Abs(PaymentVAT)) * -1D
                ._SOANumber = SOACtr
                ._SOAType = EnumSOAMainType.OutstandingVAT
            End With
            _SOAMain._lstDetails.Add(VATDetails)

            MF = (From x In _lstParticipantBalance _
                            Where (x.ChargeType = EnumChargeType.MF _
                            Or x.ChargeType = EnumChargeType.MFV) _
                            Select x.EndingBalance).Sum
            Dim MFDetails As New StatementOfAccountMainDetails
            With MFDetails
                ._Amount = (Math.Abs(MF) + Math.Abs(CollectionMF) + Math.Abs(PaymentMF)) * -1D
                ._SOANumber = SOACtr
                ._SOAType = EnumSOAMainType.OutstandingMF
            End With
            _SOAMain._lstDetails.Add(MFDetails)

            Dim chkTransaction = (From x In _SOAMain._lstDetails _
                                  Select x._Amount).Sum

            If chkTransaction = 0 Then
                'Continue For
            End If

            SOAMain.Add(_SOAMain)
            SOACtr += 1
        Next

        Me.WBillHelper.SaveStatementOfAccount(SOAMain, CDate(Me.cbo_dueDate.SelectedItem.ToString), isOverwrite)
        MsgBox("Successfully Saved!", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Information, MsgBoxStyle), "Success")
        'MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Encountered")
        ''Updated By Lance 08/17/2014
        '_Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_GenerateStatementOfAccountWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInExporting.ToString, AMModule.UserName)

        Exit Sub

    End Sub

#Region "Generate SOA functions"
    Private Function GenerateSOAHeader(ByVal lstParticipants As List(Of String)) As List(Of CollectionNotice)
        Dim retHeaderSOA As New List(Of CollectionNotice)
        Dim SOACounter As Long = 1

        dicIDNumberToCtr.Clear()

        For Each itmParticipant In lstParticipants
            Dim SOAHeader As New CollectionNotice

            With SOAHeader
                .IDNumber = dicMPIDtoNumber(itmParticipant)
                .CNDate = CDate(FormatDateTime(SystemDate, DateFormat.ShortDate))
                .CNNumber = SOACounter
                .DueDate = CDate(Me.cbo_dueDate.SelectedItem.ToString)
                .GenerateFlag = EnumCNFlag.BeforeCollection
                .ReferenceNo = .CNNumber
                .Status = EnumStatus.Active
                dicIDNumberToCtr.Add(.IDNumber, .CNNumber)
            End With

            retHeaderSOA.Add(SOAHeader)
            SOACounter += 1
        Next

        Return retHeaderSOA
    End Function

#End Region

    Private Sub chk_allParticipants_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If clb_participants.Items.Count > 0 Then
        '    If Me.chk_allParticipants.Checked Then
        '        'If checked, disable check list box
        '        clb_participants.Enabled = False
        '    Else
        '        'Enable remarks
        '        clb_participants.Enabled = True
        '        Me.clb_participants.SelectedIndex = 0
        '    End If

        '    For i As Integer = 0 To clb_participants.Items.Count - 1
        '        clb_participants.SetItemChecked(i, Me.chk_allParticipants.Checked)
        '    Next
        'End If
    End Sub


    'Private Sub cmd_Remarks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If Me.cmd_Remarks.Text = "Add Remarks" Then
    '        Dim _selIndex As Integer = 0
    '        _selIndex = Me.clb_participants.SelectedIndex

    '        'is Selected Checked?
    '        Dim _isChecked As Boolean = True
    '        If chk_allParticipants.Checked = True Then
    '            _isChecked = True
    '        Else
    '            If _selIndex = -1 Then
    '                MsgBox("No Participant/s selected.", MsgBoxStyle.Critical, "Add Remarks")
    '                Exit Sub
    '            End If

    '            _isChecked = CBool(Me.clb_participants.GetItemCheckState(_selIndex))
    '        End If

    '        If _isChecked = False Then
    '            MsgBox("Selected participant is not checked", MsgBoxStyle.Critical, "Error")
    '            Exit Sub
    '        Else
    '            Me.cmd_Remarks.Text = "Save"
    '            Me.clb_participants.Enabled = False
    '            Me.txt_remarks.ReadOnly = False
    '        End If
    '    Else
    '        If Me.txt_remarks.Text.Trim.Length = 0 Then
    '            MsgBox("Remarks cannot be left blank", MsgBoxStyle.Critical, "Error")
    '            Me.txt_remarks.Focus()
    '            Exit Sub
    '        Else

    '            If Me.chk_allParticipants.Checked Then
    '                For Each itmChecked In Me.clb_participants.CheckedItems
    '                    If dicRemarks.ContainsKey(itmChecked.ToString) Then
    '                        dicRemarks(itmChecked.ToString) = Me.txt_remarks.Text
    '                    Else
    '                        dicRemarks.Add(itmChecked.ToString, Me.txt_remarks.Text)
    '                    End If
    '                Next
    '            Else
    '                Dim _selParticipant As String = ""
    '                _selParticipant = Me.clb_participants.SelectedItem.ToString

    '                If dicRemarks.ContainsKey(_selParticipant) Then
    '                    dicRemarks(_selParticipant) = Me.txt_remarks.Text
    '                Else
    '                    dicRemarks.Add(_selParticipant, Me.txt_remarks.Text)
    '                End If
    '            End If
    '        End If

    '        MsgBox("Remarks Successfully Added", MsgBoxStyle.Information, "Success")
    '        Me.cmd_Remarks.Text = "Add Remarks"
    '        Me.clb_participants.Enabled = True
    '        Me.clb_participants.Focus()
    '        Me.txt_remarks.ReadOnly = True
    '    End If
    'End Sub

    Private Sub clb_participants_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs)
        'If Me.chk_allParticipants.Checked = False Then

        '    If CBool(Me.clb_participants.GetItemCheckState(Me.clb_participants.SelectedIndex)) Then
        '        If Me.dicRemarks.ContainsKey(Me.clb_participants.SelectedItem.ToString) Then
        '            Me.txt_remarks.Text = dicRemarks(Me.clb_participants.SelectedItem.ToString)
        '        Else
        '            Me.txt_remarks.Text = ""
        '        End If
        '    Else
        '        Me.txt_remarks.Text = ""
        '    End If

        'End If

    End Sub

    Private Sub clb_participants_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If Me.clb_participants.SelectedIndex = -1 Then
        '    Exit Sub
        'End If

        'If CBool(Me.clb_participants.GetItemCheckState(Me.clb_participants.SelectedIndex)) Then
        '    If Me.dicRemarks.ContainsKey(Me.clb_participants.SelectedItem.ToString) Then
        '        Me.txt_remarks.Text = dicRemarks(Me.clb_participants.SelectedItem.ToString)
        '    Else
        '        Me.txt_remarks.Text = ""
        '    End If
        'Else
        '    Me.txt_remarks.Text = ""
        'End If
    End Sub

    Private Sub cmd_remRemarks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'If Me.chk_allParticipants.Checked Then
        '    dicRemarks.Clear()
        '    MsgBox("Successfully removed remarks", MsgBoxStyle.Information, "Remove Remarks")
        '    Exit Sub
        'End If

        'If Me.clb_participants.SelectedIndex = -1 Then
        '    MsgBox("No Participant/s selected.", MsgBoxStyle.Critical, "Remove Remarks")
        '    Exit Sub
        'End If

        'If CBool(Me.clb_participants.GetItemCheckState(Me.clb_participants.SelectedIndex)) Then
        '    If Me.dicRemarks.ContainsKey(Me.clb_participants.SelectedItem.ToString) Then
        '        dicRemarks.Remove(Me.clb_participants.SelectedItem.ToString)
        '        MsgBox("Successfully removed remarks", MsgBoxStyle.Information, "Remove Remarks")
        '        Me.txt_remarks.Text = ""
        '        Exit Sub
        '    Else
        '        MsgBox("Nothing to remove", MsgBoxStyle.Exclamation, "Remove Remarks")
        '        Exit Sub
        '    End If
        'End If
    End Sub

    Private Sub cmd_preview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_preview.Click
        Dim SOAMain As New List(Of StatementOfAccount)
        SOAMain = Me.WBillHelper.GetStatementOfAccount(CDate(Me.cbo_dueDate.SelectedItem.ToString))

        If SOAMain.Count = 0 Then
            MsgBox("No Statement of account found for the Due Date " & Me.cbo_dueDate.SelectedItem.ToString & ".", CType(MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, MsgBoxStyle), "Statement Of Account")
            Exit Sub
        End If

        Dim ds As New DataSet
        Dim dtMain As New DSReport.StatementOfAccountDataTable
        Dim dtDetails As New DSReport.StatementOfAccountDetailsDataTable

        Dim AllParticipants As New List(Of AMParticipants)
        AllParticipants = Me.WBillHelper.GetAMParticipants()

        Dim CurrentInvoices As New List(Of WESMBill)
        CurrentInvoices = Me.WBillHelper.GetWESMBills()

        For Each itmMain In SOAMain
            Dim _itmMain = itmMain
            Dim selparticipant = (From x In AllParticipants _
                                  Where x.IDNumber = _itmMain._IDNumber _
                                  Select x).FirstOrDefault

            Dim drMain As DataRow
            drMain = dtMain.NewRow
            With itmMain
                drMain(dtMain.SOA_NUMBERColumn) = ._SOANumber
                drMain(dtMain.SOA_DATEColumn) = ._SOADate
                drMain(dtMain.DUE_DATEColumn) = ._DueDate
                drMain(dtMain.ID_NUMBERColumn) = ._IDNumber
                drMain(dtMain.PARTICIPANT_IDColumn) = selparticipant.ParticipantID
                drMain(dtMain.PARTICIPANT_ADDRESSColumn) = selparticipant.ParticipantAddress
            End With

            For Each itmDetails In itmMain._lstDetails
                With itmDetails
                    Select Case ._SOAType
                        Case EnumSOAMainType.OutstandingE
                            drMain(dtMain.ENERGY_OUTSTANDINGColumn) = ._Amount
                        Case EnumSOAMainType.OutstandingVAT
                            drMain(dtMain.VAT_OUTSTANDINGColumn) = ._Amount
                        Case EnumSOAMainType.OutstandingMF
                            drMain(dtMain.MF_OUTSTANDINGColumn) = ._Amount
                        Case EnumSOAMainType.OutstandingDefault
                            drMain(dtMain.DEFAULT_OUTSTANDINGColumn) = ._Amount
                        Case EnumSOAMainType.CollectionEnergy
                            drMain(dtMain.COL_ENERGYColumn) = ._Amount
                        Case EnumSOAMainType.CollectionVAT
                            drMain(dtMain.COL_VATColumn) = ._Amount
                        Case EnumSOAMainType.CollectionMF
                            drMain(dtMain.COL_MFColumn) = ._Amount
                        Case EnumSOAMainType.CollectionDefault
                            drMain(dtMain.COL_DEFAULTColumn) = ._Amount
                        Case EnumSOAMainType.PaymentEnergy
                            drMain(dtMain.PAY_ENERGYColumn) = ._Amount
                        Case EnumSOAMainType.PaymentVAT
                            drMain(dtMain.PAY_VATColumn) = ._Amount
                        Case EnumSOAMainType.PaymentMF
                            drMain(dtMain.PAY_MFColumn) = ._Amount
                        Case EnumSOAMainType.PaymentDefault
                            drMain(dtMain.PAY_DEFAULTColumn) = ._Amount
                    End Select
                End With
            Next
            dtMain.Rows.Add(drMain)
            dtMain.AcceptChanges()

            Dim ParticipantCurrentInvoice As New List(Of WESMBill)
            ParticipantCurrentInvoice = (From x In CurrentInvoices _
                                        Where x.IDNumber = _itmMain._IDNumber _
                                        And x.DueDate = CDate(Me.cbo_dueDate.SelectedItem.ToString) _
                                        Select x).ToList

            If ParticipantCurrentInvoice.Count = 0 Then
                Dim drDetails As DataRow
                drDetails = dtDetails.NewRow
                drDetails(dtDetails.SOA_NUMBERColumn) = _itmMain._SOANumber
                drDetails(dtDetails.DATE_COVEREDColumn) = "NoRecord"

                dtDetails.Rows.Add(drDetails)
                dtDetails.AcceptChanges()
            Else
                Dim DistinctInvoiceNo As New List(Of String)
                DistinctInvoiceNo = (From x In ParticipantCurrentInvoice _
                                     Select x.InvoiceNumber Distinct).ToList

                For Each itmInvoiceNo In DistinctInvoiceNo
                    Dim drDetails As DataRow
                    Dim _itmInvoiceNo = itmInvoiceNo
                    drDetails = dtDetails.NewRow

                    drDetails(dtDetails.SOA_NUMBERColumn) = _itmMain._SOANumber
                    drDetails(dtDetails.INVOICE_NOColumn) = itmInvoiceNo

                    Dim Invoice As New List(Of WESMBill)
                    Invoice = (From x In ParticipantCurrentInvoice _
                               Where x.InvoiceNumber = _itmInvoiceNo _
                               Select x).ToList

                    For Each itmForDetails In Invoice
                        With itmForDetails
                            Select Case .ChargeType
                                Case EnumChargeType.E
                                    drDetails(dtDetails.ENERGYColumn) = .Amount
                                Case EnumChargeType.EV
                                    drDetails(dtDetails.VATColumn) = .Amount
                                Case EnumChargeType.MF, EnumChargeType.MFV
                                    If IsDBNull(drDetails(dtDetails.MFColumn)) Then
                                        drDetails(dtDetails.MFColumn) = 0 + .Amount
                                    Else
                                        drDetails(dtDetails.MFColumn) = CDec(drDetails(dtDetails.MFColumn)) + .Amount
                                    End If
                            End Select

                            If itmForDetails.RegistrationID = itmForDetails.IDNumber Then
                                drDetails(dtDetails.PARENT_IDColumn) = (From x In AllParticipants _
                                                                        Where x.IDNumber = itmForDetails.IDNumber _
                                                                        Select x.ParticipantID).FirstOrDefault
                            Else
                                drDetails(dtDetails.CHILD_IDColumn) = (From x In AllParticipants _
                                                                       Where x.IDNumber = itmForDetails.RegistrationID _
                                                                       Select x.ParticipantID).FirstOrDefault
                            End If

                            drDetails(dtDetails.BILLING_PERIODColumn) = itmForDetails.BillingPeriod
                            'drDetails(dtDetails.DATE_COVEREDColumn = )

                        End With
                    Next

                    dtDetails.Rows.Add(drDetails)
                    dtDetails.AcceptChanges()
                Next

            End If


        Next
        ds.Tables.Add(dtMain)
        ds.Tables.Add(dtDetails)

        With frmReportViewer
            .LoadStatementOfAccount(ds)
            .Show()
        End With


    End Sub

    Private Sub cmd_PDFExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_PDFExport.Click

    End Sub

    Private Sub cmd_search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'If IsNumeric(Me.txt_SOANo.Text) = False Then
        '    MsgBox("Please enter valid characters only", MsgBoxStyle.Critical, "Error")
        '    Exit Sub
        'End If

        'Dim SOANo As Long = 0
        'SOANo = CLng(Me.txt_SOANo.Text)

        'lstSOAHeader = Me.WBillHelper.GetCollectionNotice(SOANo)
        'lstSOADetails = Me.WBillHelper.GetCollectionNoticeDetails(SOANo)

        'If lstSOAHeader.Count = 0 Then
        '    MsgBox("SOA number does not exist.", MsgBoxStyle.Critical, "Record not found")
        '    Me.txt_SOANo.Focus()
        '    Exit Sub
        'End If

        'Me.clb_participants.Items.Clear()
        'Me.dicIDNumberToCtr.Clear()
        'For Each itmSOAHeader In lstSOAHeader
        '    Me.clb_participants.Items.Add(dicNumberToMPID(itmSOAHeader.IDNumber))
        '    dicIDNumberToCtr.Add(itmSOAHeader.IDNumber, itmSOAHeader.ReferenceNo)
        'Next

        'Me.cmd_Remarks.Enabled = False
        'Me.cmd_remRemarks.Enabled = False
    End Sub

    Private Sub txt_SOANo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        ' Me.txt_SOANo.Text = ""
    End Sub

    Private Sub txt_SOANo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        'If Me.txt_SOANo.Text.Trim.Length = 0 Then
        '    Me.txt_SOANo.Text = "Type SOA No. here"
        'End If
    End Sub

    Private Sub cmd_browse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_browse.Click
        Dim fldrBrowse As New FolderBrowserDialog

        With fldrBrowse
            .ShowDialog()
            Me.txt_FilePath.Text = .SelectedPath
        End With



    End Sub


    Private Sub cmd_EmailToParticipants_Click(sender As Object, e As EventArgs) Handles cmd_EmailToParticipants.Click

    End Sub
End Class