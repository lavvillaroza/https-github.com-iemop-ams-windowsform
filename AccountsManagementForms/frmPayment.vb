'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmPaymentAllocationMgt
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     January 23, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI for viewing of Payment Allocation Details
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   January 4, 2011         Juan Carlo L. Panopio       Changed whole design based from meeting.

Option Explicit On
Option Strict On

Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

'Imports LDAPLib
'Imports LDAPLogin


Public Class frmPayment

    Dim WBillHelper As WESMBillHelper
    Dim BFactory As BusinessFactory
    Dim PaymentAllocation As New FuncAutoPaymentAllocationResult

    Dim BillPeriod As Integer
    Dim DueDate As Date
    Dim PaymentBatchCode As Long

    'Values per BP
    Private NSSComputation As Decimal = 0
    Private _State As String

    Private Property State() As String
        Get
            Return _State
        End Get
        Set(ByVal value As String)
            Me._State = value
        End Set
    End Property


    Private _isViewing As Boolean
    Public Property isViewing() As Boolean
        Get
            Return _isViewing
        End Get
        Set(ByVal value As Boolean)
            _isViewing = value
        End Set
    End Property

    Private Class BPDueDate

        Public Sub New()
            Me._BillingPeriod = 0
            Me._DueDate = Nothing
            Me._PayType = ""
            Me._IndexNo = 0
        End Sub

        Public Sub New(ByVal BillPeriod As Integer, ByVal DueDate As Date, ByVal PayType As String, ByVal IndexNo As Integer)
            Me._BillingPeriod = BillingPeriod
            Me._DueDate = DueDate
            Me._PayType = PayType
            Me._IndexNo = IndexNo
        End Sub


        Private _IndexNo As Integer
        Public Property IndexNo() As Integer
            Get
                Return _IndexNo
            End Get
            Set(ByVal value As Integer)
                _IndexNo = value
            End Set
        End Property

        Private _BillingPeriod As Integer
        Public Property BillingPeriod() As Integer
            Get
                Return _BillingPeriod
            End Get
            Set(ByVal value As Integer)
                _BillingPeriod = value
            End Set
        End Property


        Private _DueDate As Date
        Public Property DueDate() As Date
            Get
                Return _DueDate
            End Get
            Set(ByVal value As Date)
                _DueDate = value
            End Set
        End Property


        Private _PayType As String
        Public Property PayType() As String
            Get
                Return _PayType
            End Get
            Set(ByVal value As String)
                _PayType = value
            End Set
        End Property

    End Class

    Private Sub frmPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Try
            WBillHelper = WESMBillHelper.GetInstance()

            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName
            BFactory = BusinessFactory.GetInstance()
            Me.RefreshForm()

            Me.LoadComboItems(WBillHelper, Me.cbo_CollectionAllocDate, isViewing)
            _State = "NOTUPDATED"

            'Me.dgv_CollectionDetails.Columns(1).ValueType = GetType(Decimal)
            'Me.dgv_CollectionDetails.Columns(1).DefaultCellStyle.Format = "#,##0.00"

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Updated By Lance 08/19/2014
            If _isViewing = False Then
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_PaymentAllocationAllocateWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
            Else
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_PaymentAllocationViewWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
            End If
            Exit Sub
        End Try
    End Sub

    'Search and compute for the Allocation Per BP, Per Participant, and Participant Allocation details
    Private Sub cmd_Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Search.Click
        If cbo_CollectionAllocDate.SelectedIndex = -1 Then
            MsgBox("No Collections for Allocation was found.", MsgBoxStyle.Exclamation, "No Collections found")
            Exit Sub
        End If
        Dim timeStart As New DateTime
        timeStart = DateTime.Now
        Dim ProgressForm As New frmProgress
        'Try
        Dim PayAlloc As New FuncAutoPaymentAllocationv2
        Dim _colAllocation As New List(Of CollectionAllocation)
        Dim _GetPaymentAllocation = WBillHelper.GetPayment(CDate(cbo_CollectionAllocDate.SelectedItem.ToString))

        'If _GetPaymentAllocation.Count = 0 Then
        '    _isViewing = False
        'Else
        '    _isViewing = True
        'End If

        ProgressForm.Show()

        Dim _GetAllParticipants = WBillHelper.GetAMParticipants()
        If _isViewing = False Then
            With PayAlloc
                Dim _chkOR = WBillHelper.GetCollectionOR()

                If _chkOR = True Then
                    Dim msgans As New MsgBoxResult
                    msgans = MsgBox("There are Collection Transactions that are not yet allocated. Do you still want to proceed?", CType(MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, MsgBoxStyle), "Payment Allocation")
                    If msgans = MsgBoxResult.No Then
                        ProgressForm.Close()
                        Exit Sub
                    End If
                    Me.RefreshForm()
                End If

                Dim _GetWESMInvoice = WBillHelper.GetWESMInvoices()
                Dim _GetCurrentInterestRate = WBillHelper.GetDailyInterestRate()
                Dim _GetWESMBillSummary = WBillHelper.GetWESMBillSummary()

                Dim _GetAccountingCodes = WBillHelper.GetAccountingCodes()
                Dim _GetBillCaledar = WBillHelper.GetCalendarBP()
                Dim _GetCollections = WBillHelper.GetCollections(CDate(cbo_CollectionAllocDate.SelectedItem.ToString))
                Dim _GetCollectionMonitoring = WBillHelper.GetCollectionMonitoring(CDate(cbo_CollectionAllocDate.SelectedItem.ToString))

                'Check posted Collections
                Dim _chkPosted = (From x In _GetCollections _
                                  Where x.IsPosted = EnumIsPosted.NotPost _
                                  Select x).ToList

                If _chkPosted.Count > 0 Then
                    MsgBox("There are Manual collections that needs to be posted for the selected Allocation date. Saving of Payment Allocation is disabled", MsgBoxStyle.Exclamation, "Collections not Posted")
                    Me.cmd_CommitAllocation.Enabled = False
                    'Exit Sub
                Else
                    Me.cmd_CommitAllocation.Enabled = True
                End If

                For Each itmCollection In _GetCollections
                    _colAllocation.AddRange(itmCollection.ListOfCollectionAllocation)
                Next

                GC.Collect()
                Dim _GetForEFTDocuments = WBillHelper.GetAMPEMCPaymentForEFT(CDate(cbo_CollectionAllocDate.SelectedItem.ToString))
                .OrigWESMSummary = _GetWESMBillSummary
                .ColAllocationDate = DateAdd(DateInterval.Day, 0, CDate(cbo_CollectionAllocDate.SelectedItem.ToString))
                .lstAllParticipants = _GetAllParticipants
                .lstWESMSummary = WBillHelper.GetWESMBillSummary()
                .CollectionMonitoring = _GetCollectionMonitoring
                For Each itmAllocation In _colAllocation
                    Dim _itmAlloc = itmAllocation

                    Dim WESMSummary = (From x In .lstWESMSummary _
                                       Where x.WESMBillSummaryNo = _itmAlloc.WESMBillSummaryNo.WESMBillSummaryNo _
                                       Select x).FirstOrDefault

                    _itmAlloc.WESMBillSummaryNo = WESMSummary

                Next

                .lstCollectionAllocation = _colAllocation
                .lstCollectionAllocation = (From x In .lstCollectionAllocation _
                                            Where x.WESMBillSummaryNo IsNot Nothing _
                                            Select x).ToList
                .lstWESMInvoice = _GetWESMInvoice

                .lstAccountCodes = _GetAccountingCodes
                .lstExistPaymentAlloc = Me.WBillHelper.GetPayment()
                .lstCollection = _GetCollections
                .lstForEFt = _GetForEFTDocuments

                If Not _GetCurrentInterestRate.ContainsKey(CDate(cbo_CollectionAllocDate.SelectedItem.ToString)) Then
                    MsgBox("Cannot find Daily Interest rate for the allocation date " & FormatDateTime(CDate(cbo_CollectionAllocDate.SelectedItem.ToString), DateFormat.ShortDate) & ".", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, MsgBoxStyle), "No Interest Rate Found")
                    ProgressForm.Close()
                    Exit Sub
                End If
                .CurrInterestRate = _GetCurrentInterestRate(CDate(cbo_CollectionAllocDate.SelectedItem.ToString))
                PaymentAllocation = .GeneratePaymentAllocation()

                'Update OR Main Details
                'Get Distinct OR Numbers
                Dim dstORNumber = (From x In PaymentAllocation.lstOfficialReceipt _
                                   Select x.ORNo Distinct).ToList

                For Each itmORNumber In dstORNumber
                    'Get WESM Summaries with the selected OR Number
                    Dim _itmORNumber = itmORNumber
                    Dim ORToUpdate = (From x In PaymentAllocation.lstOfficialReceipt _
                                      Where x.ORNo = _itmORNumber _
                                      Select x).FirstOrDefault()
                    Dim lstPaymentAccount = (From x In PaymentAllocation.lstPerSummaryAllocation _
                                             Where x.ORNumber = _itmORNumber _
                                             Select x).ToList
                    Dim lstWESMSummary = (From x In lstPaymentAccount _
                                          Select x.WESMBillSummary).ToList


                    'Get invoice number list
                    Dim lstInvoiceNo = (From x In lstWESMSummary _
                                        Select x.INVDMCMNo Distinct).ToList

                    Dim SalesAndPurchased = WBillHelper.GetWESMInvoiceSalesAndPurchased(lstInvoiceNo)

                    ORToUpdate = BFactory.GeneratePaymentMainORDetails(SalesAndPurchased, _
                                                                       lstWESMSummary, _
                                                                        lstPaymentAccount, ORToUpdate)
                Next
            End With
        ElseIf _isViewing = True Then
            Me.cmd_CommitAllocation.Enabled = False
            PaymentAllocation = WBillHelper.GetPaymentAllocation(CDate(cbo_CollectionAllocDate.SelectedItem.ToString))
        End If

        Me.dgv_PaymentSummary.DataSource = Nothing
        Me.dgv_CollectionDetails.DataSource = Nothing

        ' Me.CMD_Details.Enabled = True
        Me.cmd_ViewPayment.Enabled = True
        Me.dgv_CollectionDetails.DataSource = Me.ConvertCollectionTable(Me.PaymentAllocation, WBillHelper)
        Me.dgv_PaymentSummary.DataSource = Me.CreatePaymentDataTable(Me.PaymentAllocation, _GetAllParticipants)

        'Fix table rows
        For x = 2 To Me.dgv_CollectionDetails.Columns.Count - 1
            Me.dgv_CollectionDetails.Columns(x).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Next

        For x = 2 To Me.dgv_PaymentSummary.Columns.Count - 1
            Me.dgv_PaymentSummary.Columns(x).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Next

        Me.dgv_PaymentSummary.Columns(1).Frozen = True
        Me.dgv_CollectionDetails.Columns(1).Frozen = True

        'If dgv_CollectionDetails.RowCount = 0 And dgv_PaymentSummary.RowCount = 0 Then
        '    'Exit Sub
        '    Dim MessageToText As String = ""
        '    MessageToText = "Transactions for this allocation date, "
        '    Dim _colAllocationDetails = WBillHelper.GetCollectionMonitoring(PaymentAllocation.AllocationDate)
        '    Dim _Excess As Decimal = 0
        '    _Excess = (From x In _colAllocationDetails _
        '               Where x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection _
        '               Select x.Amount).Sum
        '    If _Excess <> 0 Then
        '        MessageToText &= "Excess Collection, "
        '    End If


        '    Dim _MFSumFromCollection As Decimal = 0
        '    _MFSumFromCollection = (From x In PaymentAllocation.CollectionAllocationDetails _
        '                            Where x.CollectionType = EnumCollectionType.MarketFees _
        '                            Or x.CollectionType = EnumCollectionType.VatOnMarketFees _
        '                            Or x.CollectionType = EnumCollectionType.WithholdingTaxOnMF _
        '                            Or x.CollectionType = EnumCollectionType.WithholdingVatonMF _
        '                            Select x.Amount).Sum

        '    If _MFSumFromCollection <> 0 Then
        '        MessageToText &= "Market Fees, "
        '    End If

        '    Dim _PRReplenish As Decimal = 0


        '    _PRReplenish = (From x In _colAllocationDetails _
        '                    Where x.TransType = EnumCollectionMonitoringType.TransferToPRReplenishment _
        '                    Select x.Amount).Sum

        '    If _PRReplenish <> 0 Then
        '        MessageToText &= "Prudential Replenishment, "
        '    End If

        '    Dim _TransferPEMC As Decimal = 0
        '    _TransferPEMC = (From x In _colAllocationDetails _
        '                     Where x.TransType = EnumCollectionMonitoringType.TransferToPEMCAccount _
        '                     Select x.Amount).Sum
        '    If _TransferPEMC <> 0 Then
        '        MessageToText &= "Transfer to PEMC, "
        '    End If

        '    MessageToText &= "transactions from collection only."
        '    MsgBox(MessageToText, MsgBoxStyle.Information, "No Payment Allocations")
        'Else
        '    grpBox_Reports.Enabled = True
        'End If
        Me.State = "UPDATED"

        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        '    If _isViewing = False Then
        '        'LDAPModule.LDAP.InsertLogs(EnumAMSModules.AMS_PaymentAllocationAllocateWindow.ToString, eLogsStatus.Failed, "Error Encoutered: " & ex.Message)
        '    Else
        '        'LDAPModule.LDAP.InsertLogs(EnumAMSModules.AMS_PaymentAllocationViewWindow.ToString, eLogsStatus.Failed, "Error Encoutered: " & ex.Message)
        '    End If
        '    Exit Sub
        'Finally
        'MsgBox("Performance Testing - Payment Allocation" & vbCrLf & _
        '       "Time Start: " & timeStart & vbCrLf & _
        '       "Time End: " & DateTime.Now.ToString)

        ProgressForm.Close()
        'End Try

        'MsgBox(Now.ToString())
    End Sub

    'Show Payment Details
    Private Sub cmd_ViewPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ViewPayment.Click
        ' Try
        Dim FilterParticipantSummary As New List(Of PaymentAllocationAccount)
        Dim FilterParticipantAllocation As New List(Of PaymentAllocationParticipant)
        Dim FilterDMCMPerParticipant As New List(Of DebitCreditMemo)
        Dim FilterORPerParticipant As New List(Of OfficialReceiptMain)
        If Me.dgv_PaymentSummary.Rows.Count = 0 Then
            MsgBox("No Participants available for viweing of payment details.", MsgBoxStyle.Information, "No participants available")
            Exit Sub
        End If
        '            If Me.dgv_PaymentSummary.SelectedCells.Count = 1 Then
        Dim IDNumber = CStr(Me.dgv_PaymentSummary.CurrentRow.Cells("ID Number").Value)
        Dim _Participant = WBillHelper.GetAMParticipants(IDNumber).FirstOrDefault

        FilterParticipantSummary = (From x In PaymentAllocation.lstPerSummaryAllocation _
                                           Where x.WESMBillSummary.IDNumber.IDNumber = CStr(IDNumber) _
                                           Select x Order By x.WESMBillSummary.INVDMCMNo Ascending).ToList

        'check
        Dim _chk = (From x In PaymentAllocation.lstPerSummaryAllocation _
                    Where x.PaymentType = EnumPaymentType.DeferredAppliedEnergy _
                    Or x.PaymentType = EnumPaymentType.DeferredAppliedVAT _
                    Select x).ToList

        FilterDMCMPerParticipant = (From x In PaymentAllocation.lstDebitCreditMemo _
                                        Where x.IDNumber = IDNumber _
                                        Select x Distinct Order By x.UpdatedDate Ascending).ToList
        '                                And x.BillingPeriod = CInt(dgv_ParticipantAllocation.CurrentRow.Cells("hBillPeriod").Value) _
        'End If

        If isViewing = True Then
            'Get all dmcm no
            Dim _AllDMCM = (From x In FilterDMCMPerParticipant _
                            Select x.DMCMNumber Distinct).ToList
            If _AllDMCM.Count <> 0 Then
                Dim _DMCMDetails = WBillHelper.GetDebitCreditMemoDetails(_AllDMCM)

                For Each itmDMCM In _AllDMCM
                    Dim _itmDMCM = itmDMCM

                    'Get Main DMCM
                    Dim MainDMCM = (From x In FilterDMCMPerParticipant _
                                    Where x.DMCMNumber = _itmDMCM _
                                    Select x).FirstOrDefault

                    MainDMCM.DMCMDetails.AddRange((From x In _DMCMDetails _
                                                   Where x.DMCMNumber = _itmDMCM _
                                                   Select x).ToList)
                Next
            End If
        End If

        'Get official Receipts and filter
        Dim _GetOR = (From x In FilterParticipantSummary _
                      Join y In PaymentAllocation.lstOfficialReceipt _
                      On x.ORNumber Equals y.ORNo _
                      Select y Distinct).ToList

        'get Participant PR
        Dim _PRAmount = (From x In PaymentAllocation.ParticipantForEFT _
                         Where x.Participant.IDNumber = IDNumber _
                         Select x.TransferPrudential).Sum

        'get Participant Excess Collection
        Dim _AmountExcess = (From x In PaymentAllocation.ParticipantForEFT _
                             Where x.Participant.IDNumber = IDNumber _
                             Select x.ExcessCollection).Sum

        Dim _tmpForm As New frmPaymentAllocationViewDetails
        With _tmpForm
            .ShowPaymentDetails(FilterParticipantAllocation, FilterParticipantSummary, FilterDMCMPerParticipant, _
                                PaymentAllocation.lstAccountCodes, _GetOR, _Participant, PaymentAllocation.AllocationDate, PaymentAllocation.dicPaymentTransType, _
                                _PRAmount, _AmountExcess, _Participant.IDNumber, _Participant.ParticipantID, PaymentAllocation.PaymentbatchCode)
            .ShowDialog()
        End With

        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        '    'Updated By Lance 08/19/2014
        '    If _isViewing = False Then
        '        _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_PaymentAllocationAllocateWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
        '    Else
        '        _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_PaymentAllocationViewWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
        '    End If
        '    Exit Sub
        'End Try
    End Sub

    'Change form state when Index of ComboBox is changed
    Private Sub CBO_cBATCHCODE_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo_CollectionAllocDate.SelectedIndexChanged
        Me.State = "NOTUPDATED"
        Me.RefreshForm()
    End Sub

    'Refresh the Form and Combo Boxes in form
    Private Sub cmd_Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Refresh.Click
        Try
            Me.RefreshForm()

            'Me.cmd_ColPaymentSummary.Enabled = False
            'Me.cmd_CommitAllocation.Enabled = False
            'Me.cmd_DMCMSummary.Enabled = False
            'Me.cmd_ViewAllocation.Enabled = False
            'Me.cmd_ViewEFTCheck.Enabled = False
            'Me.cmd_ViewFTF.Enabled = False
            'Me.cmd_ViewJV.Enabled = False
            'Me.cmd_ViewOffsetSummary.Enabled = False
            'Me.cmd_ViewPayment.Enabled = False

            Me.grpBox_Reports.Enabled = False

            Me.txt_CollectionTotal.Text = "0.00"
            Me.txt_NSSApplied.Text = "0.00"
            Me.txt_tDefaultCol.Text = "0.00"
            Me.txt_tDeferredApplied.Text = "0.00"
            Me.txt_tEnergyCol.Text = "0.00"
            Me.txt_tVATCol.Text = "0.00"

            Me.LoadComboItems(WBillHelper, Me.cbo_CollectionAllocDate, isViewing)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Updated By Lance 08/19/2014
            If _isViewing = False Then
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_PaymentAllocationAllocateWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
            Else
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_PaymentAllocationViewWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
            End If
            Exit Sub
        End Try
    End Sub

    'Close the Form
    Private Sub cmd_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Close.Click
        Me.Close()
    End Sub

    'Save Payment Allocation batch
    Private Sub cmd_CommitAllocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_CommitAllocation.Click
        Dim ans As MsgBoxResult = MsgBoxResult.No

        If Me.State <> "UPDATED" Then
            MsgBox("Search filters were changed, please click the allocate button again.", MsgBoxStyle.Critical, "Payment Allocation")
            Exit Sub
        End If
        Try
            frmPaymentTransferToPR.MainPaymentForm = Me
            frmPaymentTransferToPR.LoadForTransfer(PaymentAllocation)
            frmPaymentTransferToPR.Show()

            ' Me.cmd_CommitAllocation.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Updated By Lance 08/19/2014
            If _isViewing = False Then
                '_Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_PaymentAllocationAllocateWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
            Else
                ' _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_PaymentAllocationViewWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
            End If
            Exit Sub
        End Try
    End Sub

#Region "Form Functions"
    'Load Collection Batch Codes which are not yet Allocated To payment.
    Public Sub LoadComboItems(ByVal WESMHelper As WESMBillHelper, ByVal cboAllocationDate As ComboBox, Optional ByVal isViewing As Boolean = False)
        Dim CollectionData As New List(Of Date)
        Dim PaymentData As New List(Of Date)
        cboAllocationDate.Items.Clear()
        'Get Allocation Date from Collection that are allocated
        CollectionData = WESMHelper.GetCollectionAllocationDate()

        'Get Payment Allocation date based from RFP Allocation dates
        PaymentData = WESMHelper.GetRFPAllocationDate()

        CollectionData = (From x In CollectionData _
                          Select x Order By x Descending).ToList

        'Where(x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy _
        'Or x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnEnergy _
        'Or x.CollectionType = EnumCollectionType.Energy _
        'Or x.CollectionType = EnumCollectionType.VatOnEnergy)

        If CollectionData.Count = 0 Then
            MsgBox("No Collection data was found.", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, MsgBoxStyle), "No Collection Data")
            Exit Sub
        End If



        If isViewing = False Then
            For Each colDate In CollectionData
                Dim _colDate = colDate
                Dim _chkDate = (From x In PaymentData _
                                Where x = _colDate _
                                Select x Order By x Descending).ToList

                If _chkDate.Count = 0 Then
                    Me.cbo_CollectionAllocDate.Items.Add(_colDate)
                Else
                End If
            Next
        Else
            PaymentData = (From x In PaymentData _
                           Select x Order By x Descending).ToList
            For Each payDate In PaymentData
                Me.cbo_CollectionAllocDate.Items.Add(payDate)
            Next
        End If

        If cboAllocationDate.Items.Count > 0 Then
            'Automatically Select 1st Item in Combo Box
            cboAllocationDate.SelectedIndex = 0

            Me.grpBox_Reports.Enabled = True
        Else
            'To Display if there's no Collection ready for Payment Allocation is found
            MsgBox("No Collection data was found.", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, MsgBoxStyle), "No Collection Data")
            Me.cmd_Search.Enabled = False
        End If

    End Sub

    'Clear all Lists - Reset the form
    Public Sub RefreshForm()
        For Each item In Me.Controls
            If item.GetType() Is GetType(TextBox) Then
                Dim ctrl = CType(item, TextBox)
                ctrl.Text = "0.00"
            End If
        Next

        Me.cmd_CommitAllocation.Enabled = False
        Me.dgv_CollectionDetails.DataSource = Nothing
        Me.dgv_PaymentSummary.DataSource = Nothing
    End Sub
#End Region

    Public Function ConvertCollectionTable(ByVal PayAllocation As FuncAutoPaymentAllocationResult, ByVal WBillHelp As WESMBillHelper) As DataTable
        Dim retDatatable As New DataTable
        Dim CollectionHeader As New List(Of Collection)
        Dim AllParticipants As New List(Of AMParticipants)
        Dim AllWESMSummary = WBillHelp.GetWESMBillSummary()

        Dim DistinctCollectionAllocDetails = (From x In PayAllocation.CollectionAllocationDetails _
                                                  Select x.CollectionNumber Distinct).ToList

        AllParticipants = WBillHelp.GetAMParticipants()

        For Each item In DistinctCollectionAllocDetails
            CollectionHeader.AddRange(WBillHelp.GetCollections(item))
        Next

        Dim _CollectionBillPeriod = (From x In AllWESMSummary _
                                     Select x.BillPeriod, x.DueDate Distinct _
                                     Order By BillPeriod Ascending, DueDate Ascending).ToList

        'Fill due Date in Colleciton Allocation Details
        For Each itmWESMsummary In PayAllocation.CollectionAllocationDetails
            Dim _itmWESMSummary = itmWESMsummary
            Dim DateDue = (From x In AllWESMSummary _
                                       Where x.WESMBillSummaryNo = _itmWESMSummary.WESMBillSummaryNo.WESMBillSummaryNo _
                                       Select x.DueDate).FirstOrDefault

            itmWESMsummary.WESMBillSummaryNo.DueDate = DateDue
        Next

        With retDatatable.Columns
            .Add("ID Number")
            .Add("Participant ID")
            For Each itmBillPeriod In _CollectionBillPeriod
                Dim _itmBillPeriod = itmBillPeriod

                Dim chkSumEnergy = (From x In PayAllocation.CollectionAllocationDetails _
                                    Where x.CollectionType = EnumCollectionType.Energy _
                                    And x.BillingPeriod = _itmBillPeriod.BillPeriod _
                                    And x.WESMBillSummaryNo.DueDate = _itmBillPeriod.DueDate _
                                    Select x.Amount).Sum

                If chkSumEnergy <> 0 Then
                    .Add(itmBillPeriod.BillPeriod.ToString & vbCrLf & FormatDateTime(itmBillPeriod.DueDate, DateFormat.ShortDate) & vbCrLf & EnumCollectionType.Energy.ToString)
                End If

                Dim chkSumDefault = (From x In PayAllocation.CollectionAllocationDetails _
                                    Where x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy _
                                    And x.WESMBillSummaryNo.DueDate = _itmBillPeriod.DueDate _
                                    And x.BillingPeriod = _itmBillPeriod.BillPeriod _
                                    Select x.Amount).Sum

                If chkSumDefault <> 0 Then
                    .Add(itmBillPeriod.BillPeriod.ToString & vbCrLf & FormatDateTime(itmBillPeriod.DueDate, DateFormat.ShortDate) & vbCrLf & EnumCollectionType.DefaultInterestOnEnergy.ToString)
                End If

                Dim chkSumVAT = (From x In PayAllocation.CollectionAllocationDetails _
                                    Where x.CollectionType = EnumCollectionType.VatOnEnergy _
                                    And x.WESMBillSummaryNo.DueDate = _itmBillPeriod.DueDate _
                                    And x.BillingPeriod = _itmBillPeriod.BillPeriod _
                                    Select x.Amount).Sum

                If chkSumVAT <> 0 Then
                    .Add(itmBillPeriod.BillPeriod.ToString & vbCrLf & FormatDateTime(itmBillPeriod.DueDate, DateFormat.ShortDate) & vbCrLf & EnumCollectionType.VatOnEnergy.ToString)
                End If
            Next
        End With

        'Fill Rows of table

        'Get Each Participant
        Dim _lstCollectionParticipant = (From x In CollectionHeader _
                               Select x.IDNumber Distinct).ToList

        Dim EnergyAMT As Decimal = 0
        Dim VATAmout As Decimal = 0
        Dim DefaultAmount As Decimal = 0

        Dim tEnergy As Decimal = 0
        Dim tDefault As Decimal = 0
        Dim tVAT As Decimal = 0

        For Each itmParticipant In _lstCollectionParticipant

            Dim _itmparticipant = itmParticipant

            'get Participant's Collection
            Dim _ParticipantCollection = (From x In CollectionHeader _
                                          Where x.IDNumber = _itmparticipant _
                                          Select x.CollectionNumber Distinct).ToList

            For Each itmCollection In _ParticipantCollection
                Dim dr As DataRow
                dr = retDatatable.NewRow
                dr("ID Number") = itmParticipant
                dr("Participant ID") = (From x In AllParticipants _
                                       Where x.IDNumber = _itmparticipant _
                                       Select x.ParticipantID).FirstOrDefault
                Dim _itmCollection = itmCollection

                'get Collection Details
                Dim _ParticipantCDetails = (From x In PayAllocation.CollectionAllocationDetails _
                                            Where x.CollectionNumber = _itmCollection _
                                            And (x.CollectionType = EnumCollectionType.Energy _
                                                 Or x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy _
                                                 Or x.CollectionType = EnumCollectionType.VatOnEnergy) _
                                           Select x).ToList

                Dim _chck = (From x In PayAllocation.CollectionAllocationDetails _
                             Where x.BillingPeriod = 80 _
                             And x.DueDate = New Date(2013, 11, 25) _
                             And x.CollectionType = EnumCollectionType.Energy _
                             Select x.Amount).Sum

                Dim _chck2 = (From x In PayAllocation.CollectionAllocationDetails _
                             Where x.BillingPeriod = 85 _
                             And x.DueDate = New Date(2013, 11, 25) _
                             And x.CollectionType = EnumCollectionType.Energy _
                             Select x.Amount).Sum

                Dim _chck3 = (From x In PayAllocation.CollectionAllocationDetails _
                             Where x.BillingPeriod = 88 _
                             And x.DueDate = New Date(2013, 11, 25) _
                             And x.CollectionType = EnumCollectionType.Energy _
                             Select x.Amount).Sum

                Dim _BillPeriodDueDate = (From x In _ParticipantCDetails _
                                          Select x.BillingPeriod, x.WESMBillSummaryNo.DueDate Distinct).ToList

                For Each itmBPDueDate In _BillPeriodDueDate
                    'Get Collection Details
                    Dim _itmBPDueDate = itmBPDueDate
                    Dim cDetails = (From x In _ParticipantCDetails _
                                    Where x.BillingPeriod = _itmBPDueDate.BillingPeriod _
                                    And x.WESMBillSummaryNo.DueDate = _itmBPDueDate.DueDate _
                                    Select x).ToList

                    EnergyAMT = 0
                    DefaultAmount = 0
                    VATAmout = 0

                    For Each itmCDetails In cDetails
                        Dim _itmCDetails = itmCDetails
                        Dim PosAmount As Decimal = CDec(FormatNumber(IIf(_itmCDetails.Amount < 0, _itmCDetails.Amount * -1D, _itmCDetails.Amount), 2, TriState.True, TriState.True))
                        Dim _DueDate = CDate((From x In AllWESMSummary _
                                        Where x.WESMBillSummaryNo = _itmCDetails.WESMBillSummaryNo.WESMBillSummaryNo _
                                        Select x.DueDate).FirstOrDefault.ToString)

                        If itmCDetails.CollectionType = EnumCollectionType.Energy Then
                            EnergyAMT += PosAmount
                            tEnergy += PosAmount
                        ElseIf itmCDetails.CollectionType = EnumCollectionType.DefaultInterestOnEnergy Then
                            DefaultAmount += PosAmount
                            tDefault += PosAmount
                        ElseIf itmCDetails.CollectionType = EnumCollectionType.VatOnEnergy Then
                            VATAmout += PosAmount
                            tVAT += PosAmount
                        End If
                    Next

                    If EnergyAMT <> 0 Then
                        dr(itmBPDueDate.BillingPeriod.ToString & vbCrLf & FormatDateTime(itmBPDueDate.DueDate, DateFormat.ShortDate) & vbCrLf & EnumCollectionType.Energy.ToString) = EnergyAMT.ToString("#,##0.00")
                    End If

                    If DefaultAmount <> 0 Then
                        dr(itmBPDueDate.BillingPeriod.ToString & vbCrLf & FormatDateTime(itmBPDueDate.DueDate, DateFormat.ShortDate) & vbCrLf & EnumCollectionType.DefaultInterestOnEnergy.ToString) = DefaultAmount.ToString("#,##0.00")
                    End If

                    If VATAmout <> 0 Then
                        dr(itmBPDueDate.BillingPeriod.ToString & vbCrLf & FormatDateTime(itmBPDueDate.DueDate, DateFormat.ShortDate) & vbCrLf & EnumCollectionType.VatOnEnergy.ToString) = VATAmout.ToString("#,##0.00")
                    End If
                Next

                If _ParticipantCDetails.Count <> 0 Then
                    retDatatable.Rows.Add(dr)
                    retDatatable.AcceptChanges()
                End If
            Next

        Next

        'Dim NSSBalanceSum = Me.PaymentAllocation.lstPerBillPeriodAllocation.Sum(Function(x As Payment) x.NSSBalance)
        Dim TotalNSS = Me.PaymentAllocation.lstPerBillPeriodAllocation.Sum(Function(x As Payment) x.Total_NSSRA)

        Dim NSSApplied = TotalNSS 'NSSBalanceSum +

        Me.txt_tDeferredApplied.Text = (From x In PayAllocation.lstPerSummaryAllocation _
                                        Where x.PaymentType = EnumPaymentType.DeferredAppliedEnergy _
                                        Or x.PaymentType = EnumPaymentType.DeferredAppliedVAT _
                                        Select x.PaymentAmount).Sum.ToString("#,##0.00")
        Me.txt_tDefaultCol.Text = tDefault.ToString("#,##0.00")
        Me.txt_tEnergyCol.Text = tEnergy.ToString("#,##0.00")
        Me.txt_tVATCol.Text = tVAT.ToString("#,##0.00")
        Me.txt_NSSApplied.Text = FormatNumber(NSSApplied, 2, TriState.True, TriState.True)
        If isViewing = False Then
            Me.txt_CollectionTotal.Text = FormatNumber(tEnergy + tDefault + tVAT + NSSApplied + CDec(txt_tDeferredApplied.Text), 2, TriState.True, TriState.True)
        Else
            Me.txt_CollectionTotal.Text = FormatNumber(tEnergy + tDefault + tVAT + NSSApplied + CDec(txt_tDeferredApplied.Text), 2, TriState.True, TriState.True)
        End If


        Return retDatatable
    End Function

    Private Function CreatePaymentDataTable(ByVal PayAllocation As FuncAutoPaymentAllocationResult, ByVal AllParticipants As List(Of AMParticipants)) As DataTable

        Dim dt As New DataTable
        Dim tStart As New DateTime
        tStart = Date.Now
        'Get List of Participant with Allocation
        Dim PerParticipant = (From x In PayAllocation.lstPerParticipantAllocationViewing _
                              Join y In AllParticipants _
                              On x.Participant.IDNumber Equals y.IDNumber _
                              Select y Distinct Order By y.ParticipantID).ToList

        Dim BPAndDueDate = (From x In PayAllocation.lstPerParticipantAllocationViewing _
                            Where x.AllocDetails.Energy <> 0 _
                                  Or x.AllocDetails.VAT <> 0 _
                                  Or x.AllocDetails.DefaultInterest <> 0 _
                            Select x.DueDate, x.BillPeriod Distinct _
                            Order By BillPeriod Ascending, DueDate Ascending).ToList

        BPAndDueDate.AddRange((From x In PayAllocation.lstAllocationSummary _
                               Where x.Amount <> 0 _
                              Select x.DueDate, x.BillPeriod Distinct _
                              Order By BillPeriod Ascending, DueDate Ascending).ToList)

        BPAndDueDate = (From x In BPAndDueDate _
                       Select x.DueDate, x.BillPeriod Distinct _
                        Order By BillPeriod Ascending, DueDate Ascending).ToList

        'Fill Columns of Datatable
        With dt.Columns
            .Add("ID Number")
            .Add("Participant ID")

            'Get Distinct Bill Period and Due Date
            For Each itmBPDue In BPAndDueDate
                Dim _itmBPDue = itmBPDue

                Dim _chkEnergy2 = (From x In PayAllocation.lstPerSummaryAllocation _
                                   Where x.WESMBillSummary.BillPeriod = _itmBPDue.BillPeriod _
                                   And x.WESMBillSummary.DueDate = _itmBPDue.DueDate _
                                   And (x.PaymentType = EnumPaymentType.PaymentEnergy _
                                        Or x.PaymentType = EnumPaymentType.OffsetAmountEnergy _
                                        Or x.PaymentType = EnumPaymentType.OffsetOfPreviousReceivableEnergy _
                                        Or x.PaymentType = EnumPaymentType.OffsetToCurrentReceivableEnergy) _
                                   Select x).ToList

                'Check Energy total
                'Dim _chkEnergy = (From x In PayAllocation.lstPerParticipantAllocationViewing _
                '                  Where x.BillPeriod = _itmBPDue.BillPeriod _
                '                  And x.DueDate = _itmBPDue.DueDate _
                '                  And x.AllocDetails.TotalEnergyPayment <> 0 _
                '                  Select x).ToList

                If _chkEnergy2.Count <> 0 Then
                    .Add(itmBPDue.BillPeriod & vbCrLf & FormatDateTime(itmBPDue.DueDate, DateFormat.ShortDate) & vbCrLf & EnumPaymentAllocationType.Energy.ToString)
                End If

                Dim _chkVAT2 = (From x In PayAllocation.lstPerSummaryAllocation _
                                Where x.WESMBillSummary.BillPeriod = _itmBPDue.BillPeriod _
                                And x.WESMBillSummary.DueDate = _itmBPDue.DueDate _
                                And (x.PaymentType = EnumPaymentType.PaymentEnergyVAT _
                                     Or x.PaymentType = EnumPaymentType.OffsetAmountVATEnergy _
                                     Or x.PaymentType = EnumPaymentType.OffsetOfPreviousReceivableVAT _
                                     Or x.PaymentType = EnumPaymentType.OffsetToCurrentReceivableVAT) _
                                Select x).ToList

                Dim _chk = (From x In PayAllocation.lstPerSummaryAllocation _
                                Where x.WESMBillSummary.BillPeriod = _itmBPDue.BillPeriod _
                                And x.WESMBillSummary.DueDate = _itmBPDue.DueDate _
                                And (x.PaymentType.ToString.Contains("VAT")) _
                                Select x).ToList

                'Check VAT on Energy total
                'Dim _chkVAT = (From x In PayAllocation.lstPerParticipantAllocationViewing _
                '                  Where x.BillPeriod = _itmBPDue.BillPeriod _
                '                  And x.DueDate = _itmBPDue.DueDate _
                '                  And x.AllocDetails.TotalVATPayment <> 0 _
                '                  Select x).ToList

                If _chkVAT2.Count <> 0 Then
                    .Add(itmBPDue.BillPeriod & vbCrLf & FormatDateTime(itmBPDue.DueDate, DateFormat.ShortDate) & vbCrLf & EnumPaymentAllocationType.VATonEnergy.ToString)
                End If

                Dim _chkDefault2 = (From x In PayAllocation.lstPerSummaryAllocation _
                                Where x.WESMBillSummary.BillPeriod = _itmBPDue.BillPeriod _
                                And x.WESMBillSummary.DueDate = _itmBPDue.DueDate _
                                And (x.PaymentType = EnumPaymentType.DefaultAllocation _
                                     Or x.PaymentType = EnumPaymentType.OffsetOfPreviousReceivableEnergyDefault _
                                     Or x.PaymentType = EnumPaymentType.OffsetToCurrentReceivableEnergyDefault) _
                                Select x).ToList

                'Check Default Interest total
                'Dim _chkDefault = (From x In PayAllocation.lstPerParticipantAllocationViewing _
                '                  Where x.BillPeriod = _itmBPDue.BillPeriod _
                '                  And x.DueDate = _itmBPDue.DueDate _
                '                  And x.AllocDetails.DefaultInterest <> 0 _
                '                  Select x).ToList

                If _chkDefault2.Count <> 0 Then
                    .Add(itmBPDue.BillPeriod & vbCrLf & FormatDateTime(itmBPDue.DueDate, DateFormat.ShortDate) & vbCrLf & EnumPaymentAllocationType.DefaultInterestOnEnergy.ToString)
                End If

                Dim chkOffsetting = (From x In PayAllocation.lstPerSummaryAllocation _
                                     Where x.WESMBillSummary.BillPeriod = _itmBPDue.BillPeriod _
                                     And x.WESMBillSummary.DueDate = _itmBPDue.DueDate _
                                     And x.PaymentType = EnumPaymentType.WESMBillOffsetting _
                                     Select x).ToList

                If chkOffsetting.Count > 0 Then
                    .Add(itmBPDue.BillPeriod & vbCrLf & FormatDateTime(itmBPDue.DueDate, DateFormat.ShortDate) & vbCrLf & EnumPaymentType.WESMBillOffsetting.ToString)
                End If
            Next

            Dim chkMarketFees = (From x In PayAllocation.lstPerSummaryAllocation _
                                     Where x.PaymentType = EnumPaymentType.UnpaidMF _
                                     Or x.PaymentType = EnumPaymentType.UnpaidMFV _
                                     Or x.PaymentType = EnumPaymentType.UnpaidMFDefault _
                                     Or x.PaymentType = EnumPaymentType.UnpaidMFVDefault _
                                     Select x.WESMBillSummary.BillPeriod, x.WESMBillSummary.DueDate Distinct _
                                     Order By BillPeriod Ascending, DueDate Ascending).ToList

            If chkMarketFees.Count > 0 Then
                For Each itmMF In chkMarketFees
                    .Add(itmMF.BillPeriod & vbCrLf & itmMF.DueDate & vbCrLf & EnumPaymentType.UnpaidMF.ToString)
                Next
            End If

            .Add("DeferredPayment" & vbCrLf & "Energy")
            .Add("DeferredPayment" & vbCrLf & "VATOnEnergy")
            .Add("Excess" & vbCrLf & "Collection")
        End With

        'Fill Rows of Datatable
        For Each itmParticipant In PerParticipant
            'Where x.IDNumber = _itmParticipant.Participant.IDNumber _
            Debug.Print(itmParticipant.ParticipantID & " " & itmParticipant.IDNumber)
            Dim _itmParticipant = itmParticipant
            Dim ParticipantDetails = (From x In AllParticipants _
                                      Where x.IDNumber = _itmParticipant.IDNumber _
                                      Select x).FirstOrDefault
            Dim dr As DataRow
            dr = dt.NewRow

            With ParticipantDetails
                dr("ID Number") = .IDNumber
                dr("Participant ID") = .ParticipantID

                Dim DeferredPaymentEnergy As Decimal = 0
                Dim DeferredPaymentVAT As Decimal = 0

                Dim tDeferredEnergy As Decimal = 0
                Dim tDeferredVAT As Decimal = 0

                For Each itmBPDue In BPAndDueDate
                    Debug.Print(itmBPDue.BillPeriod & " " & itmBPDue.DueDate)
                    Dim _itmBPDue = itmBPDue
                    DeferredPaymentEnergy = (From x In PayAllocation.lstPerParticipantAllocation _
                                             Where x.Participant.IDNumber = .IDNumber _
                                             And x.BillPeriod = _itmBPDue.BillPeriod _
                                             And x.DueDate = _itmBPDue.DueDate _
                                             Select x.AllocDetails.DeferredEnergy).Sum

                    DeferredPaymentVAT = (From x In PayAllocation.lstPerParticipantAllocation _
                                              Where x.Participant.IDNumber = .IDNumber _
                                              And x.BillPeriod = _itmBPDue.BillPeriod _
                                              And x.DueDate = _itmBPDue.DueDate _
                                              Select x.AllocDetails.DeferredVAT).Sum

                    tDeferredEnergy += DeferredPaymentEnergy
                    tDeferredVAT += DeferredPaymentVAT

                    Dim EnergyTotal = (From x In PayAllocation.lstPerSummaryAllocation _
                                       Where x.WESMBillSummary.IDNumber.IDNumber = .IDNumber _
                                       And x.WESMBillSummary.BillPeriod = _itmBPDue.BillPeriod _
                                       And x.WESMBillSummary.DueDate = _itmBPDue.DueDate _
                                       And (x.PaymentType = EnumPaymentType.PaymentEnergy _
                                            Or x.PaymentType = EnumPaymentType.OffsetOfPreviousReceivableEnergy _
                                            Or x.PaymentType = EnumPaymentType.OffsetToCurrentReceivableEnergy) _
                                            Select x.PaymentAmount).Sum

                    'Or x.PaymentType = EnumPaymentType.OffsetAmountEnergy _
                    'Or x.PaymentType = EnumPaymentType.OffsetAllocatedEnergy) _

                    If EnergyTotal <> 0 Then
                        dr(itmBPDue.BillPeriod & vbCrLf & FormatDateTime(itmBPDue.DueDate, DateFormat.ShortDate) & vbCrLf & EnumPaymentAllocationType.Energy.ToString) = FormatNumber(EnergyTotal, 2, TriState.True, TriState.True, TriState.True)
                    End If

                    Dim VATTotal = (From x In PayAllocation.lstPerSummaryAllocation _
                                    Where x.WESMBillSummary.IDNumber.IDNumber = .IDNumber _
                                       And x.WESMBillSummary.BillPeriod = _itmBPDue.BillPeriod _
                                       And x.WESMBillSummary.DueDate = _itmBPDue.DueDate _
                                       And (x.PaymentType = EnumPaymentType.PaymentEnergyVAT _
                                            Or x.PaymentType = EnumPaymentType.OffsetOfPreviousReceivableVAT _
                                            Or x.PaymentType = EnumPaymentType.OffsetToCurrentReceivableVAT) _
                                            Select x.PaymentAmount).Sum
                    'Or x.PaymentType = EnumPaymentType.OffsetAmountVATEnergy _
                    'Or x.PaymentType = EnumPaymentType.OffsetAllocatedVAT) _


                    If VATTotal <> 0 Then
                        dr(itmBPDue.BillPeriod & vbCrLf & FormatDateTime(itmBPDue.DueDate, DateFormat.ShortDate) & vbCrLf & EnumPaymentAllocationType.VATonEnergy.ToString) = FormatNumber(VATTotal, 2, TriState.True, TriState.True, TriState.True)
                    End If

                    Dim DefaultTotal = (From x In PayAllocation.lstPerSummaryAllocation _
                                    Where x.WESMBillSummary.IDNumber.IDNumber = .IDNumber _
                                    And x.WESMBillSummary.BillPeriod = _itmBPDue.BillPeriod _
                                    And x.WESMBillSummary.DueDate = _itmBPDue.DueDate _
                                    And (x.PaymentType = EnumPaymentType.DefaultAllocation _
                                         Or x.PaymentType = EnumPaymentType.OffsetOfPreviousReceivableEnergyDefault _
                                         Or x.PaymentType = EnumPaymentType.OffsetToCurrentReceivableEnergyDefault) _
                                    Select x.PaymentAmount).Sum

                    If DefaultTotal <> 0 Then
                        dr(itmBPDue.BillPeriod & vbCrLf & FormatDateTime(itmBPDue.DueDate, DateFormat.ShortDate) & vbCrLf & EnumPaymentAllocationType.DefaultInterestOnEnergy.ToString) = FormatNumber(DefaultTotal, 2, TriState.True, TriState.True, TriState.True)
                    End If



                Next

                'Generate Value for Market Fees
                Dim MFBPDueDate = (From x In PayAllocation.lstPerSummaryAllocation _
                                       Where (x.PaymentType = EnumPaymentType.UnpaidMF _
                                       Or x.PaymentType = EnumPaymentType.UnpaidMFV _
                                       Or x.PaymentType = EnumPaymentType.UnpaidMFDefault _
                                       Or x.PaymentType = EnumPaymentType.UnpaidMFVDefault) _
                                       And x.WESMBillSummary.IDNumber.IDNumber = .IDNumber _
                                       Select x.WESMBillSummary.BillPeriod, x.WESMBillSummary.DueDate Distinct _
                                       Order By BillPeriod Ascending, DueDate Ascending).ToList

                For Each itmMFBPDueDate In MFBPDueDate
                    Dim _itmMFBPDueDate = itmMFBPDueDate
                    'Dim MFTotal = (From x In PayAllocation.lstPerSummaryAllocation _
                    '                    Where (x.WESMBillSummary.ChargeType = EnumChargeType.MF Or _
                    '                           x.WESMBillSummary.ChargeType = EnumChargeType.MFV) _
                    '                    And x.WESMBillSummary.IDNumber.IDNumber = .IDNumber _
                    '                    And x.WESMBillSummary.BillPeriod = _itmMFBPDueDate.BillPeriod _
                    '                    And x.WESMBillSummary.DueDate = _itmMFBPDueDate.DueDate _
                    '                    And (x.PaymentType <> EnumPaymentType.UnpaidMFWHTax Or _
                    '                         x.PaymentType <> EnumPaymentType.UnpaidMFWHVAT Or _
                    '                         x.PaymentType <> EnumPaymentType.WHTaxDefault Or _
                    '                         x.PaymentType <> EnumPaymentType.WHVATDefault) _
                    'Select x.PaymentAmount).Sum
                    Dim MFTotal = (From x In PayAllocation.lstPerSummaryAllocation _
                                   Where (x.PaymentType = EnumPaymentType.UnpaidMF _
                                          Or x.PaymentType = EnumPaymentType.UnpaidMFDefault _
                                          Or x.PaymentType = EnumPaymentType.UnpaidMFV _
                                          Or x.PaymentType = EnumPaymentType.UnpaidMFVDefault _
                                          Or x.PaymentType = EnumPaymentType.UnpaidMFWHTax _
                                          Or x.PaymentType = EnumPaymentType.UnpaidMFWHVAT _
                                          Or x.PaymentType = EnumPaymentType.WHTaxDefault _
                                          Or x.PaymentType = EnumPaymentType.WHVATDefault) _
                                    And x.WESMBillSummary.IDNumber.IDNumber = .IDNumber _
                                    And x.WESMBillSummary.BillPeriod = _itmMFBPDueDate.BillPeriod _
                                    And x.WESMBillSummary.DueDate = _itmMFBPDueDate.DueDate _
                                    Select x.PaymentAmount).Sum


                    dr(itmMFBPDueDate.BillPeriod & vbCrLf & itmMFBPDueDate.DueDate & vbCrLf & _
                  EnumPaymentType.UnpaidMF.ToString) = FormatNumber(MFTotal, 2, TriState.True, TriState.True, TriState.True)
                Next

                'Generate Values for Deferred Payments
                If tDeferredEnergy <> 0 Then
                    dr("DeferredPayment" & vbCrLf & "Energy") = tDeferredEnergy.ToString("#,##0.00")
                End If

                If tDeferredVAT <> 0 Then
                    dr("DeferredPayment" & vbCrLf & "VATonEnergy") = tDeferredVAT.ToString("#,##0.00")
                End If

                'Check Excess Collection of Participant
                Dim tExcessCollection As Decimal = 0
                tExcessCollection = (From x In PaymentAllocation.ParticipantForEFT _
                                     Where x.ExcessCollection <> 0 _
                                     And x.Participant.IDNumber = .IDNumber _
                                     Select x.ExcessCollection).Sum
                If tExcessCollection <> 0 Then
                    dr("Excess" & vbCrLf & "Collection") = tExcessCollection.ToString("#,##0.00")
                End If

            End With
            dt.Rows.Add(dr)
            dt.AcceptChanges()
        Next

        'Get Participants without entries but with Excess Collection
        Dim _ParticipantExcess = (From x In PaymentAllocation.ParticipantForEFT _
                                  Where x.ExcessCollection <> 0 _
                                  Select x.Participant.IDNumber Distinct).ToList

        'Get List of Participants not in regular process
        For Each itmExcess In _ParticipantExcess
            Dim _itmExcess = itmExcess
            Dim chkParticipant = (From x In PerParticipant _
                                  Where x.IDNumber = _itmExcess _
                                  Select x).ToList
            If chkParticipant.Count = 0 Then
                Dim dr As DataRow
                dr = dt.NewRow

                Dim pDetails = (From x In PaymentAllocation.ParticipantForEFT _
                                Where x.Participant.IDNumber = _itmExcess _
                                Select x).FirstOrDefault

                Dim totalExcess = (From x In PaymentAllocation.ParticipantForEFT _
                                  Where x.Participant.IDNumber = _itmExcess _
                                  Select x.ExcessCollection).Sum

                dr("ID Number") = _itmExcess
                dr("Participant ID") = pDetails.Participant.ParticipantID
                dr("Excess" & vbCrLf & "Collection") = totalExcess.ToString("#,##0.00")

                dt.Rows.Add(dr)
                dt.AcceptChanges()
            End If
        Next
        'MsgBox("Data table start run: " & tStart.ToString() & vbCrLf & "Data table end run: " & Date.Now.ToString())
        Return dt
    End Function

    Private Sub dgv_PaymentSummary_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv_PaymentSummary.SelectionChanged
        Me.UpdateLabelText()
    End Sub

    Private Sub UpdateLabelText()
        Dim TotalSelected As Decimal = 0
        Dim EnergyTotal As Decimal = 0
        Dim VATTotal As Decimal = 0
        Dim DefaultTotal As Decimal = 0
        Dim UnpaidMFTotal As Decimal = 0
        Dim DeferredEnergy As Decimal = 0
        Dim DeferredVAT As Decimal = 0
        'Iterate through all Rows and Sum up Columns
        For ctr As Integer = 0 To dgv_PaymentSummary.SelectedCells.Count - 1
            If dgv_PaymentSummary.SelectedCells(ctr).FormattedValueType Is _
            Type.GetType("System.String") Then
                Dim value As String = Nothing
                If (dgv_PaymentSummary.IsCurrentCellDirty = True) Then
                    value = dgv_PaymentSummary.SelectedCells(ctr) _
                    .EditedFormattedValue.ToString()
                Else
                    value = dgv_PaymentSummary.SelectedCells(ctr) _
                    .FormattedValue.ToString()
                End If

                If value = "0.00" Then
                    Continue For
                End If

                If value IsNot Nothing Then
                    If Not dgv_PaymentSummary.SelectedCells(ctr).ColumnIndex = dgv_PaymentSummary.Columns("ID Number").Index And _
                       Not dgv_PaymentSummary.SelectedCells(ctr).ColumnIndex = dgv_PaymentSummary.Columns("Participant ID").Index And _
                       Not dgv_PaymentSummary.SelectedCells(ctr).ColumnIndex = dgv_PaymentSummary.Columns("Excess" & vbCrLf & "Collection").Index Then

                        If Not value.Length = 0 Then
                            If CDec(value) < 0 Then
                                value = Replace(value, "(", "")
                                value = Replace(value, ")", "")
                                value = CStr(CDec(value) * -1D)
                            End If

                            If dgv_PaymentSummary.Columns(dgv_PaymentSummary.SelectedCells(ctr).ColumnIndex).Name.Contains(EnumPaymentAllocationType.Energy.ToString) Then
                                If dgv_PaymentSummary.Columns(dgv_PaymentSummary.SelectedCells(ctr).ColumnIndex).Name.Contains("VAT") Then
                                    If dgv_PaymentSummary.Columns(dgv_PaymentSummary.SelectedCells(ctr).ColumnIndex).Name.Contains("DeferredPayment") Then
                                        'Add to Deferred VAT
                                        DeferredVAT += Decimal.Parse(value)
                                    Else
                                        'Add to VAT
                                        VATTotal += Decimal.Parse(value)
                                    End If
                                Else
                                    If dgv_PaymentSummary.Columns(dgv_PaymentSummary.SelectedCells(ctr).ColumnIndex).Name.Contains("DeferredPayment") Then
                                        DeferredEnergy += Decimal.Parse(value)
                                    Else
                                        If dgv_PaymentSummary.Columns(dgv_PaymentSummary.SelectedCells(ctr).ColumnIndex).Name.Contains(EnumPaymentAllocationType.DefaultInterestOnEnergy.ToString) Then
                                            'Add to Energy
                                            DefaultTotal += Decimal.Parse(value)
                                        Else
                                            'Add to Default Interest
                                            EnergyTotal += Decimal.Parse(value)
                                        End If

                                    End If
                                End If
                                'ElseIf dgv_PaymentSummary.Columns(dgv_PaymentSummary.SelectedCells(ctr).ColumnIndex).Name.Contains(EnumPaymentAllocationType.DefaultInterestOnEnergy.ToString) Then
                                'Add to Default Interest
                                '   DefaultTotal += Decimal.Parse(value)
                            ElseIf dgv_PaymentSummary.Columns(dgv_PaymentSummary.SelectedCells(ctr).ColumnIndex).Name.Contains("UnpaidMF") Then
                                UnpaidMFTotal += Decimal.Parse(value)
                            End If

                            'If Not dgv_PaymentSummary.Columns(dgv_PaymentSummary.SelectedCells(ctr).ColumnIndex).Name.Contains("DeferredPayment") Then
                            If Not dgv_PaymentSummary.Columns(dgv_PaymentSummary.SelectedCells(ctr).ColumnIndex).Name.Contains(EnumPaymentType.WESMBillOffsetting.ToString) Then
                                TotalSelected += Decimal.Parse(value)
                            End If
                            'End If
                        End If

                    End If
                End If
            End If
        Next

        tslbl_Energy.Text = "Energy: " & FormatNumber(EnergyTotal, 2, TriState.True, TriState.True, TriState.True)
        tslbl_DefaultInterest.Text = " DefaultInterest: " & FormatNumber(DefaultTotal, 2, TriState.True, TriState.True, TriState.True)
        tslbl_VAT.Text = " VAT: " & FormatNumber(VATTotal, 2, TriState.True, TriState.True, TriState.True)
        tslbl_UnpaidMF.Text = " MarketFees: " & FormatNumber(UnpaidMFTotal, 2, TriState.True, TriState.True, TriState.True)
        tslbl_DeferredEnergy.Text = " Deferred-Energy: " & FormatNumber(DeferredEnergy, 2, TriState.True, TriState.True, TriState.True)
        tslbl_DeferredVAT.Text = " Deferred-VAT: " & FormatNumber(DeferredVAT, 2, TriState.True, TriState.True, TriState.True)
        If isViewing = False Then
            tslbl_GrandTotal.Text = " GrandTotal: " & FormatNumber(TotalSelected, 2, TriState.True, TriState.True, TriState.True)
        Else
            tslbl_GrandTotal.Text = " GrandTotal: " & FormatNumber(TotalSelected, 2, TriState.True, TriState.True, TriState.True)
        End If

    End Sub

    Private Sub cmd_ViewAllocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ViewAllocation.Click
        Dim DetailsForm As New frmPaymentAllocationDetails
        If PaymentAllocation.lstAllocationSummary.Count = 0 Then
            MsgBox("No Records to generate for Allocation Summary", MsgBoxStyle.Exclamation, "No records")
            Exit Sub
        End If
        With DetailsForm
            .LoadData(PaymentAllocation.lstAllocationSummary)


            .ShowDialog()
        End With
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ViewEFTCheck.Click
        If Me.cbo_CollectionAllocDate.SelectedIndex = -1 Then
            Exit Sub
        End If

        If PaymentAllocation.ParticipantForEFT.Count = 0 Then
            MsgBox("No records found.", MsgBoxStyle.Exclamation, "No records found")
            Exit Sub
        End If

        Dim ViewEFT As New frmEFT

        Dim combineEFT = PaymentAllocation.ForEFTNew
        combineEFT.AddRange(PaymentAllocation.ForEFTUpdate)

        With ViewEFT
            .LoadList(PaymentAllocation.ParticipantForEFT, PaymentAllocation.AllocationDate)
            .Show()
        End With
    End Sub

    Private Sub cmd_DMCMSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_DMCMSummary.Click
        Dim dt = New DSReport.DMCMSummaryDataTable

        Dim lstDMCMSummaryNo = (From x In Me.PaymentAllocation.DMCMSummary _
                               Select x.DMCMNo Distinct _
                               Order By DMCMNo Ascending).ToList

        Dim AllParticipants = WBillHelper.GetAMParticipants()

        Dim Signatories = WBillHelper.GetSignatories("S_DMCM_CPSUMMARY").FirstOrDefault

        If lstDMCMSummaryNo.Count = 0 Then
            MsgBox("No Records to generate for Debit/Credit Memo Summary", MsgBoxStyle.Exclamation, "No records")
            Exit Sub
        End If

        For Each itmDMCMNo In lstDMCMSummaryNo
            Dim dr = dt.NewRow
            Dim _itmDMCMNo = itmDMCMNo
            Dim itmDMCMSummary = (From x In Me.PaymentAllocation.DMCMSummary _
                                  Where x.DMCMNo = _itmDMCMNo _
                                  Select x).FirstOrDefault

            With itmDMCMSummary
                dr("DMCM_DATE") = .DMCMDate
                dr("DMCM_NO") = Me.BFactory.GenerateBIRDocumentNumber(.DMCMDetails.DMCMNumber, BIRDocumentsType.DMCM)
                dr("ID_NUMBER") = .DMCMDetails.IDNumber
                dr("PARTICIPANT_ID") = (From x In AllParticipants _
                                        Where x.IDNumber = .DMCMDetails.IDNumber _
                                        Select x.ParticipantID).FirstOrDefault
                Select Case .DMCMDetails.TransType
                    Case EnumDMCMTransactionType.PaymentChildToParentOffsetting
                        dr("TRANSACTION_TYPE") = "Child to Parent Offsetting"
                    Case EnumDMCMTransactionType.PaymentDefaultInterestAllocation
                        dr("TRANSACTION_TYPE") = "Allocation of Default Interest from Collection"
                    Case EnumDMCMTransactionType.PaymentMFandMFVATDefaultInterest
                        dr("TRANSACTION_TYPE") = "Application for Default Interest in MF/VAT on MF"
                    Case EnumDMCMTransactionType.PaymentOffsettingOfReceivableEnergy
                        dr("TRANSACTION_TYPE") = "Application for Energy Receivable"
                    Case EnumDMCMTransactionType.PaymentOffsettingOfReceivableMF
                        dr("TRANSACTION_TYPE") = "Application for Market Fees Receivable"
                    Case EnumDMCMTransactionType.PaymentOffsettingOfReceivableMFV
                        dr("TRANSACTION_TYPE") = "Application for VAT on Market Fees Receivable"
                    Case EnumDMCMTransactionType.PaymentOffsettingOfReceivableVATOnEnergy
                        dr("TRANSACTION_TYPE") = "Application for VAT on Energy Receivable"
                    Case EnumDMCMTransactionType.PaymentSetupEnergyDefaultInterest
                        dr("TRANSACTION_TYPE") = "Setup for Default Interest"
                    Case EnumDMCMTransactionType.PaymentSetupWithholding
                        dr("TRANSACTION_TYPE") = "Setup for Withholding Tax/Withholding VAT for Market Fees"
                    Case EnumDMCMTransactionType.PaymentSetupOfDefaultInterestMFMFV
                        dr("TRANSACTION_TYPE") = "Setup for Default Interest in Market Fees/VAT for Market Fees"
                    Case EnumDMCMTransactionType.PaymentDefaultInterestAllocation2
                        dr("TRANSACTION_TYPE") = "Allocation of Default Interest from Offsetting"
                    Case EnumDMCMTransactionType.PaymentOffsettingOfReceivableEnergyDefault
                        dr("TRANSACTION_TYPE") = "Application of Default Interest"
                    Case EnumDMCMTransactionType.PaymentOffsettingOfReceivableEnergyClearing
                        dr("TRANSACTION_TYPE") = "Application to Clearing Account for Energy"
                    Case EnumDMCMTransactionType.PaymentOffsettingOfReceivableVATEnergyClearing
                        dr("TRANSACTION_TYPE") = "Application to Clearing Account for VAT On Energy"
                    Case EnumDMCMTransactionType.PaymentPrudentialReplenishment
                        dr("TRANSACTION_TYPE") = "Transfer of Payment to Participant to Prudential"
                    Case Else
                        dr("TRANSACTION_TYPE") = .DMCMDetails.TransType
                End Select
                dr("INVOICE_NUMBER") = .INVDMCMNo
                dr("AMOUNT") = .DMCMDetails.TotalAmountDue
                dr("PREPARED_BY") = AMModule.FullName
                dr("PREPARED_POSITION") = AMModule.Position
                dr("CHECKED_BY") = Signatories.Signatory_1
                dr("CHECKED_POSITION") = Signatories.Position_1
                dr("APPROVED_BY") = Signatories.Signatory_2
                dr("APPROVEd_POSITION") = Signatories.Position_2
            End With

            dt.Rows.Add(dr)
            dt.AcceptChanges()
        Next

        Dim RPTDMCMSummary As New frmReportViewer
        frmProgress.Show()
        With RPTDMCMSummary
            If _isViewing = True Then
                .LoadDMCMSummary(dt, PaymentAllocation.AllocationDate, PaymentAllocation.lstPerBillPeriodAllocation.FirstOrDefault.PaymentBatchCode)
            Else
                .LoadDMCMSummary(dt, PaymentAllocation.AllocationDate, "P-XXX")
            End If
            .ShowDialog()
        End With
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ViewOffsetSummary.Click
        Dim ProgressForm As New frmProgress
        Try
            ProgressForm.Show()
            Dim dt = New DSReport.C2PDMCMSummaryDataTable

            Dim DMCMSummaryList = (From x In Me.PaymentAllocation.DMCMSummary _
                                   Where x.DMCMSummaryType = EnumDMCMSummaryType.Offsetting _
                                   Select x).ToList

            If DMCMSummaryList.Count = 0 Then
                MsgBox("No Records to generate for Offsetting Summary", MsgBoxStyle.Exclamation, "No records")
                Exit Sub
            End If

            For Each itmDMCMSummary In DMCMSummaryList
                With itmDMCMSummary
                    For Each itmDMCMDetails In .DMCMDetails.DMCMDetails
                        Dim dr = dt.NewRow
                        dr("DMCM_DATE") = .DMCMDate
                        dr("DMCM_NO") = Me.BFactory.GenerateBIRDocumentNumber(.DMCMNo, BIRDocumentsType.DMCM)
                        dr("PARTICIPANT_ID") = .DMCMDetails.IDNumber
                        Select Case .DMCMDetails.TransType
                            Case EnumDMCMTransactionType.PaymentChildToParentOffsetting
                                dr("TRANSACTION_TYPE") = "Child to Parent Offsetting"
                            Case EnumDMCMTransactionType.PaymentDefaultInterestAllocation
                                dr("TRANSACTION_TYPE") = "Allocation of Default Interest"
                            Case EnumDMCMTransactionType.PaymentMFandMFVATDefaultInterest
                                dr("TRANSACTION_TYPE") = "Application for Default Interest in MF/VAT on MF"
                            Case EnumDMCMTransactionType.PaymentOffsettingOfReceivableEnergy
                                dr("TRANSACTION_TYPE") = "Application for Energy Receivable"
                            Case EnumDMCMTransactionType.PaymentOffsettingOfReceivableMF
                                dr("TRANSACTION_TYPE") = "Application for Market Fees Receivable"
                            Case EnumDMCMTransactionType.PaymentOffsettingOfReceivableMFV
                                dr("TRANSACTION_TYPE") = "Application for VAT on Market Fees Receivable"
                            Case EnumDMCMTransactionType.PaymentOffsettingOfReceivableVATOnEnergy
                                dr("TRANSACTION_TYPE") = "Application for VAT on Energy Receivable"
                            Case EnumDMCMTransactionType.PaymentSetupEnergyDefaultInterest
                                dr("TRANSACTION_TYPE") = "Setup for Default Interest"
                            Case EnumDMCMTransactionType.PaymentSetupWithholding
                                dr("TRANSACTION_TYPE") = "Setup for Withholding Tax/Withholding VAT for Market Fees"
                            Case EnumDMCMTransactionType.PaymentSetupOfDefaultInterestMFMFV
                                dr("TRANSACTION_TYPE") = "Setup for Default Interest in Market Fees/VAT for Market Fees"
                        End Select
                        Dim _mSummaryType = CStr(IIf(itmDMCMDetails.SummaryType = EnumSummaryType.DMCM, "DMCM-W", "FS-W"))
                        dr("INVOICE_NUMBER") = itmDMCMDetails.InvDMCMNo
                        If itmDMCMDetails.AccountCode = CreditCode Then
                            dr("CREDIT") = itmDMCMDetails.Credit
                            dr("DEBIT") = itmDMCMDetails.Debit
                        Else
                            dr("CREDIT") = itmDMCMDetails.Credit
                            dr("DEBIT") = itmDMCMDetails.Debit
                        End If
                        dt.Rows.Add(dr)
                        dt.AcceptChanges()
                    Next
                End With
            Next

            Dim RPTDMCMSummary As New frmReportViewer
            frmProgress.Show()
            With RPTDMCMSummary
                .LoadC2PDMCMSummary(dt, PaymentAllocation.AllocationDate, "Test Batch")
                .ShowDialog()
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error!")
            'Updated By Lance 08/19/2014
            If _isViewing = False Then
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_PaymentAllocationAllocateWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
            Else
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_PaymentAllocationViewWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
            End If
            Exit Sub
        Finally
            ProgressForm.Close()
        End Try
    End Sub

    Private Sub cmd_ViewJV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ViewJV.Click
        Dim dt As New DataTable
        Dim dtDetails As New DataTable

        With dt.Columns
            .Add("JVNumber")
            .Add("JVGPRef")
            .Add("JVDate")
            .Add("JVStatus")
            .Add("JVPrepared")
            .Add("JVApproved")
            .Add("JVUpdated")
        End With

        With dtDetails.Columns
            .Add("JVNumberDetails")
            .Add("JVDAccountCode")
            .Add("JVDDebit")
            .Add("JVDCredit")
            .Add("JVUpdated")
        End With

        For Each itmJVHeader In PaymentAllocation.lstJournalVoucher
            Dim dr As DataRow
            dr = dt.NewRow

            With itmJVHeader
                dr("JVNumber") = .JVNumber
                dr("JVGPRef") = .GPRefNo
                dr("JVDate") = .JVDate
                dr("JVStatus") = .Status
                dr("JVPrepared") = .PreparedBy
                dr("JVApproved") = .ApprovedBy
                dr("JVUpdated") = .UpdatedBy
            End With

            dt.Rows.Add(dr)
            dt.AcceptChanges()

            For Each itmJVDetails In itmJVHeader.JVDetails
                Dim drDetails As DataRow
                drDetails = dtDetails.NewRow

                With itmJVDetails
                    drDetails("JVNumberDetails") = .JVNumber
                    drDetails("JVDAccountCode") = .AccountCode
                    drDetails("JVDDebit") = .Debit
                    drDetails("JVDCredit") = .Credit
                    drDetails("JVUpdated") = .UpdatedBy
                End With

                dtDetails.Rows.Add(drDetails)
                dtDetails.AcceptChanges()
            Next
        Next

        Dim ds As New DataSet
        ds.Tables.Add(dt)
        ds.Tables.Add(dtDetails)

        Dim _tmpForm As New frmReportViewer
        frmProgress.Show()
        With _tmpForm
            .LoadJournalVoucher(ds)
            .Show()
        End With

    End Sub

    Private Sub cmd_ColPaymentSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ColPaymentSummary.Click
        Dim ProgressForm As New frmProgress

        Try
            ProgressForm.Show()
            If Me.cbo_CollectionAllocDate.SelectedIndex = -1 Then
                Exit Sub
            End If

            If Me.dgv_CollectionDetails.RowCount = 0 And Me.dgv_PaymentSummary.RowCount = 0 Then
                MsgBox("No records to generate for Collection and Payment Summary", MsgBoxStyle.Exclamation, "No Records")
                Exit Sub
            End If

            Dim frmview As New frmViewDetails
            With frmview
                .dgridView.DataSource = CreateSummaryOfColPay(Me.PaymentAllocation, CType(Me.dgv_CollectionDetails.DataSource, DataTable), WBillHelper)

                For ctrCol = 0 To .dgridView.Columns.Count - 1
                    .dgridView.Columns(ctrCol).SortMode = DataGridViewColumnSortMode.Programmatic
                    .dgridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .dgridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)
                    If ctrCol > 1 Then
                        .dgridView.Columns(ctrCol).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    End If
                Next
                If isViewing = False Then
                    .Text = "Summary of Collection and Payments (Initial) - Allocation Date: " & FormatDateTime(PaymentAllocation.AllocationDate, DateFormat.ShortDate)
                Else
                    .Text = "Summary of Collection and Payments (Final) - Allocation Date: " & FormatDateTime(PaymentAllocation.AllocationDate, DateFormat.ShortDate)
                End If
                .cmd_ExportToExcel.Visible = True
                .ViewColPaymentSummary(_isViewing)
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error!")
            'Updated By Lance 08/19/2014
            If _isViewing = False Then
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_PaymentAllocationAllocateWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
            Else
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_PaymentAllocationViewWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
            End If
            Exit Sub
        Finally
            ProgressForm.Close()
        End Try
    End Sub

    Public Function CreateSummaryOfColPay(ByVal PayAllocation As FuncAutoPaymentAllocationResult, ByVal CollectionTable As DataTable, _
                                          Optional ByVal WBillHelper As WESMBillHelper = Nothing) As DataTable
        Dim dt As New DataTable
        Dim colRowCount As Long = 0

        Dim dicCollectionTotal As New Dictionary(Of Integer, Decimal)
        Dim dicPaymentTotal As New Dictionary(Of Integer, Decimal)

        Dim _AllParticipants = WBillHelper.GetAMParticipantsAll()

        'Get Columns for Collection and Payment
        Dim _BPDueDate = (From x In PayAllocation.lstPerSummaryAllocation _
                          Select x.WESMBillSummary.BillPeriod, x.WESMBillSummary.DueDate Distinct).ToList

        Dim _ColMonitoring = WBillHelper.GetCollectionMonitoring(PayAllocation.AllocationDate)

        Dim _PaymentDataTable As DataTable = Me.CreatePaymentDataTable(PayAllocation, _AllParticipants)

        Dim MFDistinct = (From x In PayAllocation.CollectionAllocationDetails _
                          Where x.WESMBillSummaryNo.ChargeType = EnumChargeType.MF Or _
                          x.WESMBillSummaryNo.ChargeType = EnumChargeType.MFV _
                          And (x.BillingPeriod <> 0) _
                          Select x.WESMBillSummaryNo.DueDate, x.BillingPeriod Distinct).ToList

        Dim _CollectionDatatable As DataTable = CType(CollectionTable, DataTable)

        dt.Merge(_CollectionDatatable)
        dt.Rows.Clear()

        Dim ctrOrdinal As Long = 0
        Dim ctrMF As Long = 0

        For _PayColumn = 2 To _PaymentDataTable.Columns.Count - 1

            If dt.Columns.Contains(_PaymentDataTable.Columns(_PayColumn).ColumnName) = True Then
                Continue For
            Else
                dt.Columns.Add(_PaymentDataTable.Columns(_PayColumn).ColumnName)
            End If



            'If _PaymentDataTable.Columns(_tmpcol).ColumnName.Contains(EnumPaymentType.WESMBillOffsetting.ToString) = False Then
            '    dt.Columns.Add(_PaymentDataTable.Columns(_tmpcol).ColumnName)
            '    If _PaymentDataTable.Columns(_tmpcol).ColumnName.Contains(EnumPaymentType.UnpaidMF.ToString) Then
            '        If ctrMF = 0 Then
            '            ctrMF = _tmpcol
            '        End If

            '    End If
            'End If
        Next

        For Each itmBPDueDate In MFDistinct

            Dim ColName = itmBPDueDate.BillingPeriod & vbCrLf & itmBPDueDate.DueDate & vbCrLf & EnumPaymentType.UnpaidMF.ToString
            Dim isAdded As Boolean = False
            Dim x = _PaymentDataTable.Columns.Count
            For tmpCol = 2 To _PaymentDataTable.Columns.Count - 4
                Dim ColumnName = _PaymentDataTable.Columns(tmpCol).ColumnName
                Dim GetBillPeriod = CInt(Trim(Mid(ColumnName, 1, InStr(ColumnName, vbCrLf))))
                Dim GetDueDate As Date
                If GetBillPeriod = 0 Then
                    GetDueDate = CDate(Trim(Mid(ColumnName, Len(GetBillPeriod), (InStr(Trim(Mid(ColumnName, Len(GetBillPeriod) + 1)), vbCrLf)))))
                Else
                    GetDueDate = CDate(Trim(Mid(ColumnName, Len(GetBillPeriod) + 1, (InStr(Trim(Mid(ColumnName, Len(GetBillPeriod) + 1)), vbCrLf)))))
                End If

                Dim GetIfMF = Trim(Mid(ColumnName, Len(GetBillPeriod) + Len(GetDueDate) + 4))
                If itmBPDueDate.BillingPeriod = GetBillPeriod And itmBPDueDate.DueDate = GetDueDate Then
                    'MsgBox(GetIfMF.Trim.ToUpper & " " & EnumCollectionType.Energy.ToString.Trim.ToUpper)
                    If GetIfMF.Trim.ToUpper = EnumPaymentType.UnpaidMF.ToString.Trim.ToUpper _
                       Or GetIfMF.Trim.ToUpper = EnumCollectionType.Energy.ToString.Trim.ToUpper _
                       Or GetIfMF.Trim.ToUpper = EnumCollectionType.DefaultInterestOnEnergy.ToString.Trim.ToUpper _
                       Or GetIfMF.Trim.ToUpper = "VATONENERGY" _
                       Or GetIfMF.Trim.ToUpper = EnumPaymentType.WESMBillOffsetting.ToString.Trim.ToUpper Then

                        If ColName = ColumnName Then
                            isAdded = True
                            Exit For
                        End If
                    Else

                        dt.Columns.Add(ColName).SetOrdinal(CInt(tmpCol + ctrMF)) ' - 2
                        ctrOrdinal += 1
                        isAdded = True
                        Exit For
                    End If
                Else
                    Continue For
                End If
            Next

            If isAdded = False Then
                dt.Columns.Add(ColName).SetOrdinal(CInt(_PaymentDataTable.Columns.Count - 3 + ctrOrdinal))
            End If

        Next

        Dim _lstBPDueDate As New List(Of BPDueDate)
        Dim _lstMFBP As New List(Of BPDueDate)
        For Each itmColumn As DataColumn In dt.Columns
            If itmColumn.ColumnName.Contains("Excess") Then
                Continue For
            End If
            If itmColumn.ColumnName.Contains("ID Number") Or itmColumn.ColumnName.Contains("Deferred") Or _
            itmColumn.ColumnName.Contains("Participant") Or itmColumn.ColumnName.Contains("Unpaid") Then
                If itmColumn.ColumnName.Contains("Unpaid") Then
                    Dim itmBPDue As New BPDueDate
                    Dim GetBillPeriod As Integer = CInt(Trim(Mid(itmColumn.ColumnName, 1, InStr(itmColumn.ColumnName, vbCrLf))))
                    Dim GetDueDate = CDate(Trim(Mid(itmColumn.ColumnName, Len(GetBillPeriod.ToString()) + 1, (InStr(Trim(Mid(itmColumn.ColumnName, Len(GetBillPeriod) + 1)), vbCrLf)))))
                    Dim GetPayType = CStr(Trim(Mid(itmColumn.ColumnName, Len(GetBillPeriod) + Len(GetDueDate) + 4, Len(itmColumn.ColumnName))))

                    With itmBPDue
                        .BillingPeriod = GetBillPeriod
                        .DueDate = GetDueDate
                        .PayType = Replace(Trim(Mid(GetPayType, 2)), vbCrLf, "")
                    End With
                    _lstMFBP.Add(itmBPDue)

                End If
            Else
                Dim GetBillPeriod = CInt(Trim(Mid(itmColumn.ColumnName, 1, InStr(itmColumn.ColumnName, vbCrLf))))
                Dim strRemoveBillPeriod As String
                strRemoveBillPeriod = Trim(Mid(itmColumn.ColumnName, Len(GetBillPeriod), Len(itmColumn.ColumnName)))
                strRemoveBillPeriod = Trim(strRemoveBillPeriod)

                If Trim(Mid(strRemoveBillPeriod, 1, 1)) = Chr(13) Then
                    strRemoveBillPeriod = Trim(Mid(strRemoveBillPeriod, 3, Len(strRemoveBillPeriod)))
                End If

                Dim GetDueDate = CDate(Trim(Mid(Trim(strRemoveBillPeriod), 1, InStr(strRemoveBillPeriod, vbCrLf))))
                'Dim GetDueDate = CDate(Trim(Mid(itmColumn.ColumnName, Len(Trim(CStr(GetBillPeriod))) + 1, InStr(Trim(Mid(itmColumn.ColumnName, Len(Trim(CStr(GetBillPeriod))) + 1)), vbCrLf))))
                Dim GetPayType = CStr(Trim(Mid(itmColumn.ColumnName, Len(GetBillPeriod) + Len(GetDueDate) + 4, Len(itmColumn.ColumnName))))
                Dim itmBPDue As New BPDueDate
                With itmBPDue
                    .BillingPeriod = GetBillPeriod
                    .DueDate = GetDueDate
                    .PayType = Replace(Trim(Mid(GetPayType, 2)), vbCrLf, "")
                End With
                _lstBPDueDate.Add(itmBPDue)
            End If



        Next

        '_lstBPDueDate = (From x In _lstBPDueDate _
        '                 Select x Order By x.BillingPeriod Ascending, _
        '                 x.DueDate Ascending).ToList

        '_lstMFBP = (From x In _lstBPDueDate _
        '            Select x Order By x.BillingPeriod Ascending, _
        '            x.DueDate Ascending).ToList


        'For Each itmBPDueDate In _lstBPDueDate
        '    Dim x As Integer = 2
        '    With itmBPDueDate
        '        MsgBox(dt.Columns(.BillingPeriod.ToString & vbCrLf & .DueDate.ToString & vbCrLf & .PayType.ToString).ColumnName)
        '    End With
        '    x = x + 1
        'Next


        With dt.Columns
            .Add("Replenishment of Prudential Security")
            .Add("Transfer to PEMC BPI")
            .Add("Held Collection")
            '.Add("Excess Collection")
        End With

        Dim dr As DataRow

        'Get Surplus
        Dim _NSSForPayment = (From x In PayAllocation.lstPerBillPeriodAllocation _
                              Where x.Total_NSSRA <> 0 _
                              Select x).ToList
        If _NSSForPayment.Count > 0 Then
            dr = dt.NewRow

            dr("Participant ID") = "Surplus"

            For Each itmNSS In _NSSForPayment
                'If NSS is Positive
                'If itmNSS.Total_NSSRA - itmNSS.NSSBalance > 0 Then
                dr(itmNSS.BillingPeriod & vbCrLf & itmNSS.DueDate & vbCrLf & EnumCollectionType.Energy.ToString) = FormatNumber(itmNSS.Total_NSSRA + itmNSS.NSSBalance, 2, TriState.True, TriState.True)
                'End If
            Next

            dt.Rows.Add(dr)
            dt.AcceptChanges()
            dt.Rows.Add(dt.NewRow)
            dt.AcceptChanges()
            colRowCount += 2
        End If

        'Get Collections
        'Prudential Allocations
        Dim _GetCollections = WBillHelper.GetCollections(PayAllocation.AllocationDate)

        Dim _CurrentCollectionsDrawDown = (From x In _GetCollections _
                                           Where x.CollectionCategory = EnumCollectionCategory.Drawdown _
                                           Select x).ToList

        'Dim _allParticipants = WBillHelper.GetAMParticipantsAll()

        Dim _DDownCollections As New List(Of CollectionAllocation)

        'Add Collection Details

        'Collections - DRAWDOWN
        If _CurrentCollectionsDrawDown.Count <> 0 Then
            dr = dt.NewRow
            dr("Participant ID") = "Prudential - Drawdown"
            dt.Rows.Add(dr)
            dt.AcceptChanges()
            colRowCount += 1

            Dim _dParticipants = (From x In _CurrentCollectionsDrawDown _
                                  Join y In _AllParticipants _
                                  On x.IDNumber Equals y.IDNumber _
                                  Select y Distinct Order By y.ParticipantID Ascending).ToList

            'Get Distinct participants
            For Each itmParticipant In _dParticipants
                Dim _itmParticipant = itmParticipant
                dr = dt.NewRow
                dr("ID Number") = _itmParticipant.IDNumber
                dr("Participant ID") = _itmParticipant.ParticipantID

                'Get Participant's Collection Number
                Dim _pCollections = (From x In _CurrentCollectionsDrawDown _
                                     Where x.IDNumber = _itmParticipant.IDNumber _
                                     Select x.CollectionNumber Distinct).ToList


                Dim _CheckTransferToPEMC = (From x In _ColMonitoring _
                           Where x.TransType = EnumCollectionMonitoringType.TransferToPEMCAccount _
                           And x.IDNumber.IDNumber = _itmParticipant.IDNumber _
                           Select x).FirstOrDefault

                If _CheckTransferToPEMC IsNot Nothing Then
                    dr("Transfer to PEMC BPI") = FormatNumber(_CheckTransferToPEMC.Amount, 2, TriState.True, TriState.True)
                    _ColMonitoring.Remove(_CheckTransferToPEMC)
                End If


                'Get Collection details
                For Each itmColNumber In _pCollections
                    Dim _itmColNumber = itmColNumber

                    'Get Collections
                    Dim _curCollections = (From x In PayAllocation.CollectionAllocationDetails _
                                           Where x.CollectionNumber = _itmColNumber _
                                           Select x).ToList

                    Dim tEnergy As Decimal = 0
                    Dim tDefault As Decimal = 0

                    For Each itmColDetails In _curCollections
                        Dim _itmColDetails = itmColDetails
                        If _itmColDetails.CollectionType = EnumCollectionType.Energy Then

                            Dim oValue As Decimal = 0 '= CDec(IIf(dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumCollectionType.Energy.ToString).ToString = "", 0, dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumCollectionType.Energy.ToString)))

                            If dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumCollectionType.Energy.ToString).ToString = "" Then
                                oValue = 0
                            Else
                                oValue = CDec(dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumCollectionType.Energy.ToString).ToString)
                            End If

                            oValue += Math.Abs(_itmColDetails.Amount)
                            dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumCollectionType.Energy.ToString) = FormatNumber(Math.Abs(oValue), 2, TriState.True, TriState.True)
                        Else
                            Dim oValue As Decimal = 0 '= CDec(IIf(dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumCollectionType.DefaultInterestOnEnergy.ToString).ToString = "", 0, dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.DueDate & vbCrLf & EnumCollectionType.DefaultInterestOnEnergy.ToString)))

                            If dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumCollectionType.DefaultInterestOnEnergy.ToString).ToString = "" Then
                                oValue = 0
                            Else
                                oValue = CDec(dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumCollectionType.DefaultInterestOnEnergy.ToString).ToString)
                            End If

                            oValue += Math.Abs(_itmColDetails.Amount)
                            dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumCollectionType.DefaultInterestOnEnergy.ToString) = FormatNumber(Math.Abs(oValue), 2, TriState.True, TriState.True)
                        End If
                    Next
                Next

                dt.Rows.Add(dr)
                dt.AcceptChanges()
                colRowCount += 1
            Next

            dt.Rows.Add(dt.NewRow)
            dt.Rows.Add(dt.NewRow)
            colRowCount += 2
        End If

        'Collections - CASH
        Dim _CurrentCollectionCash = (From x In _GetCollections _
                                   Where x.CollectionCategory = EnumCollectionCategory.Cash _
                                   Select x).ToList

        Dim _CashCollections As New List(Of CollectionAllocation)

        If _CurrentCollectionCash.Count <> 0 Then
            dr = dt.NewRow
            dr("Participant ID") = "Collections"
            dt.Rows.Add(dr)
            dt.AcceptChanges()
            colRowCount += 1

            Dim _dParticipants = (From x In _CurrentCollectionCash _
                                  Join y In _AllParticipants _
                                  On x.IDNumber Equals y.IDNumber _
                                  Select y Distinct Order By y.ParticipantID Ascending).ToList

            'Get Distinct participants
            For Each itmParticipant In _dParticipants
                Dim _itmParticipant = itmParticipant
                dr = dt.NewRow
                dr("ID Number") = _itmParticipant.IDNumber
                dr("Participant ID") = _itmParticipant.ParticipantID


                Dim _CheckTransferAmount = (From x In _ColMonitoring _
                                            Where x.TransType = EnumCollectionMonitoringType.TransferToPEMCAccount _
                                            And x.IDNumber.IDNumber = _itmParticipant.IDNumber _
                                            Select x.Amount).Sum

                'Dim _CheckTransferToPEMC = (From x In _ColMonitoring _
                '                            Where x.TransType = EnumCollectionMonitoringType.TransferToPEMCAccount _
                '                            And x.IDNumber.IDNumber = _itmParticipant.IDNumber _
                '                            Select x).FirstOrDefault



                If _CheckTransferAmount <> 0 Then
                    dr("Transfer to PEMC BPI") = FormatNumber(_CheckTransferAmount, 2, TriState.True, TriState.True)
                    '_ColMonitoring.Remove(_CheckTransferToPEMC)
                End If

                Dim _CheckReplenishment = (From x In _ColMonitoring _
                                             Where x.TransType = EnumCollectionMonitoringType.TransferToPRReplenishment _
                                             And x.IDNumber.IDNumber = _itmParticipant.IDNumber _
                                             Select x.Amount).Sum

                'Dim _CheckPRReplenishment = (From x In _ColMonitoring _
                '                             Where x.TransType = EnumCollectionMonitoringType.TransferToPRReplenishment _
                '                             And x.IDNumber.IDNumber = _itmParticipant.IDNumber _
                '                             Select x).FirstOrDefault

                If _CheckReplenishment <> 0 Then
                    dr("Replenishment of Prudential Security") = FormatNumber(_CheckReplenishment, 2, TriState.True, TriState.True)
                End If

                Dim _CheckHeld = (From x In _ColMonitoring _
                                  Where x.TransType = EnumCollectionMonitoringType.TransferToHeldCollection _
                                  And x.IDNumber.IDNumber = _itmParticipant.IDNumber _
                                  Select x.Amount).Sum

                'Dim _CheckHeldCollection = (From x In _ColMonitoring _
                '                            Where x.TransType = EnumCollectionMonitoringType.TransferToHeldCollection _
                '                            And x.IDNumber.IDNumber = _itmParticipant.IDNumber _
                '                            Select x).FirstOrDefault

                If _CheckHeld <> 0 Then
                    dr("Held Collection") = FormatNumber(_CheckHeld, 2, TriState.True, TriState.True)
                End If

                Dim _CheckExcess = (From x In _ColMonitoring _
                                              Where x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection _
                                              And x.IDNumber.IDNumber = _itmParticipant.IDNumber _
                                              Select x.Amount).Sum

                'Dim _CheckExcessCollection = (From x In _ColMonitoring _
                '                              Where x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection _
                '                              And x.IDNumber.IDNumber = _itmParticipant.IDNumber _
                '                              Select x).FirstOrDefault

                If _CheckExcess <> 0 Then
                    dr("Excess" & vbCrLf & "Collection") = FormatNumber(_CheckExcess, 2, TriState.True, TriState.True)
                End If

                'Get Participant's Collection Number
                Dim _pCollections = (From x In _CurrentCollectionCash _
                                     Where x.IDNumber = _itmParticipant.IDNumber _
                                     Select x.CollectionNumber Distinct).ToList

                'Get Collection details
                For Each itmColNumber In _pCollections
                    Dim _itmColNumber = itmColNumber

                    'Get Collections
                    Dim _curCollections = (From x In PayAllocation.CollectionAllocationDetails _
                                           Where x.CollectionNumber = _itmColNumber _
                                           Select x).ToList

                    Dim tEnergy As Decimal = 0
                    Dim tDefault As Decimal = 0
                    Dim tVAT As Decimal = 0

                    For Each itmColDetails In _curCollections
                        Dim _itmColDetails = itmColDetails

                        Dim _itmRevAmount = _itmColDetails.Amount '* -1

                        If _itmColDetails.CollectionType = EnumCollectionType.Energy Then
                            'Get Row Details 
                            Dim rwAmount As Decimal

                            If dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumCollectionType.Energy.ToString).ToString = "" Then
                                rwAmount = 0
                            Else
                                rwAmount = CDec(dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumCollectionType.Energy.ToString).ToString)
                            End If

                            dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumCollectionType.Energy.ToString) = FormatNumber(_itmRevAmount + rwAmount, 2, TriState.True, TriState.True)

                        ElseIf _itmColDetails.CollectionType = EnumCollectionType.DefaultInterestOnEnergy Then
                            'Get Row Details 
                            Dim rwAmount As Decimal

                            If dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumCollectionType.DefaultInterestOnEnergy.ToString).ToString = "" Then
                                rwAmount = 0
                            Else
                                rwAmount = CDec(dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumCollectionType.DefaultInterestOnEnergy.ToString).ToString)
                            End If

                            dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumCollectionType.DefaultInterestOnEnergy.ToString) = FormatNumber(_itmRevAmount + rwAmount, 2, TriState.True, TriState.True)

                        ElseIf _itmColDetails.CollectionType = EnumCollectionType.VatOnEnergy Then
                            'Get Row Details 
                            Dim rwAmount As Decimal

                            If dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumCollectionType.VatOnEnergy.ToString).ToString = "" Then
                                rwAmount = 0
                            Else
                                rwAmount = CDec(dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumCollectionType.VatOnEnergy.ToString).ToString)
                            End If

                            dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumCollectionType.VatOnEnergy.ToString) = FormatNumber(_itmRevAmount + rwAmount, 2, TriState.True, TriState.True)

                        ElseIf _itmColDetails.CollectionType = EnumCollectionType.DefaultInterestOnMF Or _itmColDetails.CollectionType = EnumCollectionType.MarketFees _
                             Or _itmColDetails.CollectionType = EnumCollectionType.VatOnMarketFees Or _itmColDetails.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest _
                             Or _itmColDetails.CollectionType = EnumCollectionType.WithholdingTaxOnMF Or _itmColDetails.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest _
                             Or _itmColDetails.CollectionType = EnumCollectionType.WithholdingVatonMF Or _itmColDetails.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF Then

                            Dim rwAmount As Decimal

                            If dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumPaymentType.UnpaidMF.ToString).ToString = "" Then
                                rwAmount = 0
                            Else
                                rwAmount = CDec(dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumPaymentType.UnpaidMF.ToString).ToString)
                            End If

                            dr(_itmColDetails.BillingPeriod & vbCrLf & _itmColDetails.WESMBillSummaryNo.DueDate & vbCrLf & EnumPaymentType.UnpaidMF.ToString) = FormatNumber(_itmRevAmount + rwAmount, 2, TriState.True, TriState.True)

                        End If
                    Next
                Next

                dt.Rows.Add(dr)
                dt.AcceptChanges()
                colRowCount += 1
            Next
            dt.Rows.Add(dt.NewRow)
            colRowCount += 1
        End If

        'Get Total Per Column
        dr = dt.NewRow
        For tblCol = 1 To dt.Columns.Count - 1

            If tblCol = 1 Then
                dr(tblCol) = "Total"
            Else
                Dim TotalAmount As Decimal = 0
                For tblRow = 0 To dt.Rows.Count - 1
                    If dt.Rows(tblRow).Item(tblCol).ToString <> "" Then
                        If IsNumeric(dt.Rows(tblRow).Item(tblCol)) Then
                            TotalAmount += CDec(dt.Rows(tblRow).Item(tblCol).ToString)
                        End If
                    End If
                Next
                If TotalAmount <> 0 Then
                    dr(tblCol) = FormatNumber(TotalAmount, 2, TriState.True, TriState.True)
                End If
                dicCollectionTotal.Add(tblCol, TotalAmount)
            End If
        Next

        dt.Rows.Add(dr)
        dt.AcceptChanges()
        colRowCount += 1

        'Add Spacing
        dt.Rows.Add(dt.NewRow)
        dt.Rows.Add(dt.NewRow)
        colRowCount += 3

        dr = dt.NewRow
        dr("Participant ID") = "Payment Allocations"
        dt.Rows.Add(dr)
        dt.AcceptChanges()


        'Add Payment Details
        For rwCount = 0 To _PaymentDataTable.Rows.Count - 1
            dr = dt.NewRow

            For colCount = 0 To _PaymentDataTable.Columns.Count - 1
                dr(_PaymentDataTable.Columns(colCount).ColumnName) = _PaymentDataTable.Rows(rwCount).Item(colCount).ToString
            Next

            dt.Rows.Add(dr)
            dt.AcceptChanges()

        Next

        dt.Rows.Add(dt.NewRow)
        dt.AcceptChanges()

        'Add Grand Total
        dt.Columns.Add("Grand Total")
        For rwCount = 0 To dt.Rows.Count - 1
            Dim GrandTotal As Decimal = 0

            If rwCount >= colRowCount Then
                If dt.Rows(rwCount).Item("ID Number").ToString <> "" Then
                    Dim _IDNumber = CStr(dt.Rows(rwCount).Item("ID Number").ToString)
                    'Check in EFT
                    Dim ChkEFT = (From x In PayAllocation.ParticipantForEFT _
                                  Where x.Participant.IDNumber = _IDNumber _
                                  Select x).FirstOrDefault

                    If ChkEFT IsNot Nothing Then
                        dt.Rows(rwCount).Item("Replenishment of Prudential Security") = FormatNumber(ChkEFT.TransferPrudential * -1D, 2, TriState.True, TriState.True)
                    End If
                End If
            End If

            If dt.Rows(rwCount).Item("Participant ID").ToString = "" Or dt.Rows(rwCount).Item("Participant ID").ToString = "Collections" Or dt.Rows(rwCount).Item("Participant ID").ToString = "Prudential - Drawdown" Then
                Continue For
            End If

            For colCount = 2 To dt.Columns.Count - 1
                If dt.Rows(rwCount).Item("Participant ID").ToString <> "" Or dt.Rows(rwCount).Item("Participant ID").ToString <> "Collections" Or dt.Rows(rwCount).Item("Participant ID").ToString <> "Payment" Then
                    If dt.Rows(rwCount).Item(colCount).ToString <> "" Then
                        If dt.Columns(colCount).ColumnName.Contains("Offsetting") = False Then
                            GrandTotal += CDec(dt.Rows(rwCount).Item(colCount).ToString)
                        End If
                    End If
                    If dt.Columns(colCount).ColumnName.Contains("Grand Total") Then
                        If dt.Rows(rwCount).Item("ID Number").ToString <> "" Or dt.Rows(rwCount).Item("ID Number").ToString <> "Total" Then
                            dt.Rows(rwCount).Item(colCount) = FormatNumber(GrandTotal, 2, TriState.True, TriState.True)
                            dt.AcceptChanges()
                        End If
                    End If
                End If

            Next
        Next

        'Add Totals in Payment Allocation
        dr = dt.NewRow
        For tblCol = 1 To dt.Columns.Count - 1
            If tblCol = 1 Then
                dr(tblCol) = "Total"
            Else
                Dim TotalAmount As Decimal = 0
                For tblRow = colRowCount To dt.Rows.Count - 1
                    If dt.Rows(CInt(tblRow)).Item(tblCol).ToString <> "" Then
                        TotalAmount += CDec(dt.Rows(CInt(tblRow)).Item(tblCol).ToString)
                    End If
                Next
                dr(tblCol) = FormatNumber(TotalAmount, 2, TriState.True, TriState.True)
                dicPaymentTotal.Add(tblCol, TotalAmount)
            End If
        Next
        dt.Rows.Add(dr)
        dt.AcceptChanges()

        'Add Spacing and Totals for Payment Allocation
        dt.Rows.Add(dt.NewRow)
        dt.Rows.Add(dt.NewRow)

        'Add other Details - MF with VAT Application / PEMC BPI / STL Account / Surplus / Prudential Requirement
        For x = 1 To 5
            Dim dRow As DataRow
            dRow = dt.NewRow
            Dim GrandTotal As Decimal = 0
            For colCnt = 2 To dt.Columns.Count - 2
                Select Case x
                    Case 1 ' for Market Fees with VAT Application
                        If dt.Columns(colCnt).ColumnName.Contains(EnumPaymentType.UnpaidMF.ToString) Then
                            Dim MFSum As Decimal = 0
                            MFSum = dicCollectionTotal(colCnt) - dicPaymentTotal(colCnt)
                            GrandTotal += MFSum
                            dRow("Participant ID") = "Market Fees"
                            dRow(colCnt) = FormatNumber(MFSum, 2, TriState.True, TriState.True)
                        End If
                    Case 2
                        If dt.Columns(colCnt).ColumnName.Contains("PEMC BPI") = True Then
                            Dim BPITotal As Decimal = 0
                            BPITotal = dicCollectionTotal(colCnt) - dicPaymentTotal(colCnt)
                            GrandTotal += BPITotal
                            dRow("Participant ID") = "PEMC - BPI"
                            dRow(colCnt) = FormatNumber(BPITotal, 2, TriState.True, TriState.True)
                        End If
                    Case 3
                        If dt.Columns(colCnt).ColumnName.Contains("Deferred") = True Or _
                           dt.Columns(colCnt).ColumnName.Contains("Collection") = True Or _
                           dt.Columns(colCnt).ColumnName.Contains("Interest") = True And _
                           dt.Columns(colCnt).ColumnName.Contains("Default") = False Then

                            Dim STLSum As Decimal = 0
                            STLSum = dicCollectionTotal(colCnt) - dicPaymentTotal(colCnt)
                            GrandTotal += STLSum
                            dRow("Participant ID") = "STL Account"
                            dRow(colCnt) = FormatNumber(STLSum, 2, TriState.True, TriState.True)
                        End If
                    Case 4
                        If dt.Columns(colCnt).ColumnName.Contains(EnumCollectionType.Energy.ToString) = True Or _
                           dt.Columns(colCnt).ColumnName.Contains(EnumCollectionType.DefaultInterestOnEnergy.ToString) = True Or _
                           dt.Columns(colCnt).ColumnName.Contains(EnumCollectionType.VatOnEnergy.ToString) = True Then
                            If dt.Columns(colCnt).ColumnName.Contains("Deferred") = False Then
                                Dim NSSSum As Decimal = 0D
                                NSSSum = dicCollectionTotal(colCnt) - Math.Round(dicPaymentTotal(colCnt), 2)
                                GrandTotal += NSSSum
                                dRow("Participant ID") = "Surplus"
                                dRow(colCnt) = FormatNumber(NSSSum, 2, TriState.True, TriState.True)
                            End If
                        End If
                    Case 5
                        If dt.Columns(colCnt).ColumnName.Contains("Prudential Security") = True Then
                            Dim PRTotal As Decimal = 0D
                            PRTotal = dicCollectionTotal(colCnt) - dicPaymentTotal(colCnt)
                            GrandTotal += PRTotal
                            dRow("Participant ID") = "Prudential Requirement"
                            dRow(colCnt) = FormatNumber(PRTotal, 2, TriState.True, TriState.True)
                        End If
                End Select
            Next
            dRow("Grand Total") = FormatNumber(GrandTotal, 2, TriState.True, TriState.True)
            dt.Rows.Add(dRow)
            dt.AcceptChanges()
        Next

        Return dt
    End Function

    Private Sub cmd_PaymentSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_PaymentSummary.Click

        If Me.dgv_CollectionDetails.RowCount = 0 And Me.dgv_PaymentSummary.RowCount = 0 Then
            MsgBox("No Records found", MsgBoxStyle.Exclamation, "No records found")
            Exit Sub
        End If

        Dim rptView As New frmReportViewer
        Dim dtReport As New DSReport.CollectionSummaryDataTable

        Dim lstParticipant = (From x In PaymentAllocation.lstPerParticipantAllocationViewing _
                               Select x.Participant.IDNumber Distinct Order By IDNumber Ascending).ToList
        Dim _Signatories = WBillHelper.GetSignatories("PaymentAlloc").FirstOrDefault


        For Each itmParticipant In lstParticipant
            Dim itmIDNumber = itmParticipant
            Dim _itmParticipant = (From x In PaymentAllocation.lstPerParticipantAllocationViewing _
                                   Where x.Participant.IDNumber = itmIDNumber _
                                   Select x.Participant).FirstOrDefault

            'Get Participant Payment Allocation Details
            'Get Reference Numbers

            'Check Participant for Excess Collection
            Dim _lstReferenceNo = (From x In PaymentAllocation.lstPerParticipantAllocationViewing _
                                   Where x.Participant.IDNumber = _itmParticipant.IDNumber _
                                   Select x.PaymentGroupCode, x.PaymentBatchCode Distinct).ToList

            'Get Transactions to table
            For Each itmRefNo In _lstReferenceNo
                Dim _itmRefNo = itmRefNo



                'Change Generation of Report from Here
                Dim curTransaction = (From x In PaymentAllocation.lstPerSummaryAllocation _
                                      Where x.PaymentBatchCode = _itmRefNo.PaymentBatchCode _
                                      And x.PaymentGroupCode = _itmRefNo.PaymentGroupCode _
                                      Select x).ToList

                'Get WESM Invoice Numbers
                Dim InvoiceTransaction = (From x In curTransaction _
                                          Select x.WESMBillSummary.INVDMCMNo Distinct Order By INVDMCMNo Ascending).ToList

                For Each itmInvoice In InvoiceTransaction
                    Dim _itmInvoice = itmInvoice
                    Dim lstTransaction = (From x In curTransaction _
                                       Where x.WESMBillSummary.INVDMCMNo = _itmInvoice _
                                       Select x).ToList

                    Dim lstDMCMNo = (From x In lstTransaction _
                                     Where x.DMCMNo <> 0 _
                                     Select x.DMCMNo Distinct Order By DMCMNo Ascending).ToList

                    Dim lstPayments = (From x In lstTransaction _
                                       Where x.DMCMNo = 0 _
                                       Select x.WESMBillSummary.INVDMCMNo Distinct).ToList

                    'For Transactions without DMCM
                    For Each itmPayment In lstPayments
                        Dim dtRow = dtReport.NewRow
                        Dim _itmPayment = itmPayment
                        Dim TransactionType = (From x In lstTransaction _
                                               Where x.DMCMNo = 0 _
                                               And x.WESMBillSummary.INVDMCMNo = _itmPayment _
                                               Select x.PaymentType).FirstOrDefault

                        dtRow(dtReport.AllocationDateColumn) = FormatDateTime(PaymentAllocation.AllocationDate, DateFormat.ShortDate)

                        Dim PayAmount As Decimal = 0
                        PayAmount = (From x In lstTransaction _
                                     Where x.DMCMNo = 0 _
                                     Select x.PaymentAmount).Sum
                        dtRow(dtReport.AmountAppliedColumn) = PayAmount
                        dtRow(dtReport.AmountColumn) = PayAmount

                        Dim BaseAmount = (From x In lstTransaction _
                                          Where (x.WESMBillSummary.ChargeType = EnumChargeType.E _
                                          Or x.WESMBillSummary.ChargeType = EnumChargeType.MF) _
                                          And x.DMCMNo = 0 _
                                          Select x.PaymentAmount).Sum

                        Dim VATAmount = (From x In lstTransaction _
                                          Where (x.WESMBillSummary.ChargeType = EnumChargeType.EV _
                                          Or x.WESMBillSummary.ChargeType = EnumChargeType.MFV) _
                                          And x.DMCMNo = 0 _
                                          Select x.PaymentAmount).Sum

                        If TransactionType = EnumPaymentType.DeferredAppliedEnergy Or TransactionType = EnumPaymentType.DeferredAppliedVAT Then
                            BaseAmount = (From x In lstTransaction _
                                          Where x.PaymentType = EnumPaymentType.DeferredAppliedEnergy _
                                          Select x.PaymentAmount).Sum

                            VATAmount = (From x In lstTransaction _
                                         Where x.PaymentType = EnumPaymentType.DeferredAppliedVAT _
                                         Select x.PaymentAmount).Sum
                        End If


                        dtRow(dtReport.BASE_AMOUNTColumn) = BaseAmount
                        dtRow(dtReport.VAT_AMOUNTColumn) = VATAmount

                        dtRow(dtReport.BatchCodeColumn) = PaymentAllocation.PaymentbatchCode
                        dtRow(dtReport.CollectionDateColumn) = FormatDateTime(PaymentAllocation.AllocationDate, DateFormat.ShortDate)

                        dtRow(dtReport.TypeColumn) = PaymentAllocation.dicPaymentTransType(TransactionType)

                        dtRow(dtReport.Position1Column) = _Signatories.Position_1
                        dtRow(dtReport.Position2Column) = _Signatories.Position_2
                        dtRow(dtReport.Position3Column) = _Signatories.Position_3

                        dtRow(dtReport.Signatory1Column) = _Signatories.Signatory_1
                        dtRow(dtReport.Signatory2Column) = _Signatories.Signatory_2
                        dtRow(dtReport.Signatory3Column) = _Signatories.Signatory_3

                        dtRow(dtReport.DocDateColumn) = FormatDateTime(PaymentAllocation.AllocationDate, DateFormat.ShortDate)
                        dtRow(dtReport.IDNumberColumn) = _itmParticipant.IDNumber
                        dtRow(dtReport.ParticipantIDColumn) = _itmParticipant.ParticipantID

                        'Reference Document
                        If itmPayment = "" Then
                            dtRow(dtReport.DocumentNoColumn) = ""
                            dtRow(dtReport.DueDateColumn) = ""
                        Else
                            'get Summary Type
                            Dim Summary As New WESMBillSummary
                            Summary = (From x In curTransaction _
                                        Where x.WESMBillSummary.INVDMCMNo = _itmInvoice _
                                        Select x.WESMBillSummary).FirstOrDefault
                            If Summary.SummaryType = EnumSummaryType.INV Then
                                dtRow(dtReport.DocumentNoColumn) = Summary.INVDMCMNo
                            Else
                                dtRow(dtReport.DocumentNoColumn) = Me.BFactory.GenerateBIRDocumentNumber(CLng(Summary.INVDMCMNo), BIRDocumentsType.DMCM)
                            End If

                            dtRow(dtReport.DueDateColumn) = FormatDateTime(Summary.DueDate, DateFormat.ShortDate)
                        End If

                        dtReport.Rows.Add(dtRow)
                        dtReport.AcceptChanges()
                    Next

                    'For Transactions with DMCM
                    For Each itmDMCM In lstDMCMNo
                        Dim dtRow = dtReport.NewRow
                        Dim _itmDMCM = itmDMCM
                        Dim TransactionType = (From x In lstTransaction _
                                               Where x.DMCMNo = _itmDMCM _
                                               Select x.PaymentType).FirstOrDefault

                        dtRow(dtReport.AllocationDateColumn) = FormatDateTime(PaymentAllocation.AllocationDate, DateFormat.ShortDate)


                        Dim PayAmount As Decimal = 0
                        PayAmount = (From x In lstTransaction _
                                     Where x.DMCMNo = _itmDMCM _
                                     Select x.PaymentAmount).Sum
                        dtRow(dtReport.AmountAppliedColumn) = PayAmount
                        dtRow(dtReport.AmountColumn) = PayAmount

                        Dim BaseAmount = (From x In lstTransaction _
                                          Where (x.WESMBillSummary.ChargeType = EnumChargeType.E _
                                          Or x.WESMBillSummary.ChargeType = EnumChargeType.MF) _
                                           And x.DMCMNo = _itmDMCM _
                                          Select x.PaymentAmount).Sum

                        Dim VATAmount = (From x In lstTransaction _
                                          Where (x.WESMBillSummary.ChargeType = EnumChargeType.EV _
                                          Or x.WESMBillSummary.ChargeType = EnumChargeType.MFV) _
                                          And x.DMCMNo = _itmDMCM _
                                          Select x.PaymentAmount).Sum

                        dtRow(dtReport.BASE_AMOUNTColumn) = BaseAmount
                        dtRow(dtReport.VAT_AMOUNTColumn) = VATAmount

                        dtRow(dtReport.BatchCodeColumn) = PaymentAllocation.PaymentbatchCode
                        dtRow(dtReport.CollectionDateColumn) = FormatDateTime(PaymentAllocation.AllocationDate, DateFormat.ShortDate)


                        dtRow(dtReport.TypeColumn) = PaymentAllocation.dicPaymentTransType(TransactionType)

                        dtRow(dtReport.Position1Column) = _Signatories.Position_1
                        dtRow(dtReport.Position2Column) = _Signatories.Position_2
                        dtRow(dtReport.Position3Column) = _Signatories.Position_3

                        dtRow(dtReport.Signatory1Column) = _Signatories.Signatory_1
                        dtRow(dtReport.Signatory2Column) = _Signatories.Signatory_2
                        dtRow(dtReport.Signatory3Column) = _Signatories.Signatory_3

                        dtRow(dtReport.DocDateColumn) = FormatDateTime(PaymentAllocation.AllocationDate, DateFormat.ShortDate)
                        dtRow(dtReport.IDNumberColumn) = _itmParticipant.IDNumber
                        dtRow(dtReport.ParticipantIDColumn) = _itmParticipant.ParticipantID

                        dtRow(dtReport.CreatedDocumentNoColumn) = Me.BFactory.GenerateBIRDocumentNumber(_itmDMCM, BIRDocumentsType.DMCM)

                        'Get OR Number
                        Dim _ORNumber = (From x In curTransaction _
                                         Where x.DMCMNo = _itmDMCM _
                                         Select x.ORNumber).FirstOrDefault.ToString

                        If _ORNumber <> "0" Then
                            dtRow(dtReport.RefNoColumn) = Me.BFactory.GenerateBIRDocumentNumber(CLng(_ORNumber), BIRDocumentsType.OfficialReceipt)
                        End If

                        'Reference Document
                        If _itmDMCM = 0 Then
                            dtRow(dtReport.DocumentNoColumn) = ""
                            dtRow(dtReport.DueDateColumn) = ""
                        Else
                            'get Summary Type
                            Dim Summary As New WESMBillSummary
                            Summary = (From x In curTransaction _
                                        Where x.WESMBillSummary.INVDMCMNo = _itmInvoice _
                                        Select x.WESMBillSummary).FirstOrDefault
                            If Summary.SummaryType = EnumSummaryType.INV Then
                                dtRow(dtReport.DocumentNoColumn) = Summary.INVDMCMNo
                            Else
                                dtRow(dtReport.DocumentNoColumn) = Me.BFactory.GenerateBIRDocumentNumber(CLng(Summary.INVDMCMNo), BIRDocumentsType.DMCM)
                            End If

                            dtRow(dtReport.DueDateColumn) = FormatDateTime(Summary.DueDate, DateFormat.ShortDate)
                        End If

                        dtReport.Rows.Add(dtRow)
                        dtReport.AcceptChanges()
                    Next

                Next

                Dim _chkExcessCollection = (From x In PaymentAllocation.ParticipantForEFT _
                                        Where x.Participant.IDNumber = _itmParticipant.IDNumber _
                                        Select x.ExcessCollection).Sum

                If _chkExcessCollection <> 0 Then
                    Dim dtRow = dtReport.NewRow
                    dtRow(dtReport.AllocationDateColumn) = FormatDateTime(PaymentAllocation.AllocationDate, DateFormat.ShortDate)

                    dtRow(dtReport.AmountAppliedColumn) = _chkExcessCollection
                    dtRow(dtReport.AmountColumn) = _chkExcessCollection
                    dtRow(dtReport.BASE_AMOUNTColumn) = _chkExcessCollection
                    dtRow(dtReport.VAT_AMOUNTColumn) = 0

                    dtRow(dtReport.BatchCodeColumn) = PaymentAllocation.PaymentbatchCode
                    dtRow(dtReport.CollectionDateColumn) = FormatDateTime(PaymentAllocation.AllocationDate, DateFormat.ShortDate)

                    dtRow(dtReport.TypeColumn) = PaymentAllocation.dicPaymentTransType(EnumPaymentType.ReturnToParticipant)

                    dtRow(dtReport.Position1Column) = _Signatories.Position_1
                    dtRow(dtReport.Position2Column) = _Signatories.Position_2
                    dtRow(dtReport.Position3Column) = _Signatories.Position_3

                    dtRow(dtReport.Signatory1Column) = _Signatories.Signatory_1
                    dtRow(dtReport.Signatory2Column) = _Signatories.Signatory_2
                    dtRow(dtReport.Signatory3Column) = _Signatories.Signatory_3

                    dtRow(dtReport.DocDateColumn) = FormatDateTime(PaymentAllocation.AllocationDate, DateFormat.ShortDate)
                    dtRow(dtReport.IDNumberColumn) = _itmParticipant.IDNumber
                    dtRow(dtReport.ParticipantIDColumn) = _itmParticipant.ParticipantID
                    dtRow(dtReport.DocumentNoColumn) = ""
                    dtRow(dtReport.DueDateColumn) = ""

                    dtReport.Rows.Add(dtRow)
                    dtReport.AcceptChanges()
                End If

            Next

            'Check participant for Replenishment
            Dim _chkPRR = (From x In PaymentAllocation.ParticipantForEFT _
                           Where x.Participant.IDNumber = _itmParticipant.IDNumber _
                           And x.TransferPrudential <> 0 _
                           Select x).FirstOrDefault

            If _chkPRR IsNot Nothing Then
                Dim dtRow = dtReport.NewRow
                dtRow(dtReport.AllocationDateColumn) = FormatDateTime(PaymentAllocation.AllocationDate, DateFormat.ShortDate)
                dtRow(dtReport.AmountAppliedColumn) = _chkPRR.TransferPrudential * -1
                dtRow(dtReport.AmountColumn) = _chkPRR.TransferPrudential
                dtRow(dtReport.BatchCodeColumn) = PaymentAllocation.PaymentbatchCode
                dtRow(dtReport.CollectionDateColumn) = FormatDateTime(PaymentAllocation.AllocationDate, DateFormat.ShortDate)

                dtRow(dtReport.DocDateColumn) = FormatDateTime(PaymentAllocation.AllocationDate, DateFormat.ShortDate)
                dtRow(dtReport.IDNumberColumn) = _itmParticipant.IDNumber
                dtRow(dtReport.ParticipantIDColumn) = _itmParticipant.ParticipantID
                dtRow(dtReport.DocumentNoColumn) = "PR Replenishment"

                dtRow(dtReport.Position1Column) = _Signatories.Position_1
                dtRow(dtReport.Position2Column) = _Signatories.Position_2
                dtRow(dtReport.Position3Column) = _Signatories.Position_3

                dtRow(dtReport.Signatory1Column) = _Signatories.Signatory_1
                dtRow(dtReport.Signatory2Column) = _Signatories.Signatory_2
                dtRow(dtReport.Signatory3Column) = _Signatories.Signatory_3

                dtReport.Rows.Add(dtRow)
                dtReport.AcceptChanges()
            End If

        Next

        'Check for participant not in list
        Dim withExcess = (From x In PaymentAllocation.ParticipantForEFT _
                          Where x.ExcessCollection <> 0 _
                          Select x.Participant.IDNumber Distinct).ToList

        For Each itmExcess In withExcess
            Dim _itmExcess = itmExcess
            Dim _chkParticipant = (From x In lstParticipant _
                                   Where x = _itmExcess _
                                   Select x).ToList

            If _chkParticipant.Count = 0 Then
                Dim _chkExcessCollection = (From x In PaymentAllocation.ParticipantForEFT _
                                        Where x.Participant.IDNumber = _itmExcess _
                                        Select x.ExcessCollection).Sum

                Dim _itmParticipant = (From x In PaymentAllocation.ParticipantForEFT _
                                       Where x.Participant.IDNumber = _itmExcess _
                                       Select x.Participant).FirstOrDefault

                If _chkExcessCollection <> 0 Then
                    Dim dtRow = dtReport.NewRow
                    dtRow(dtReport.AllocationDateColumn) = FormatDateTime(PaymentAllocation.AllocationDate, DateFormat.ShortDate)

                    dtRow(dtReport.AmountAppliedColumn) = _chkExcessCollection
                    dtRow(dtReport.AmountColumn) = _chkExcessCollection
                    dtRow(dtReport.BASE_AMOUNTColumn) = _chkExcessCollection
                    dtRow(dtReport.VAT_AMOUNTColumn) = 0

                    dtRow(dtReport.BatchCodeColumn) = PaymentAllocation.PaymentbatchCode
                    dtRow(dtReport.CollectionDateColumn) = FormatDateTime(PaymentAllocation.AllocationDate, DateFormat.ShortDate)

                    dtRow(dtReport.TypeColumn) = PaymentAllocation.dicPaymentTransType(EnumPaymentType.ReturnToParticipant)

                    dtRow(dtReport.Position1Column) = _Signatories.Position_1
                    dtRow(dtReport.Position2Column) = _Signatories.Position_2
                    dtRow(dtReport.Position3Column) = _Signatories.Position_3

                    dtRow(dtReport.Signatory1Column) = _Signatories.Signatory_1
                    dtRow(dtReport.Signatory2Column) = _Signatories.Signatory_2
                    dtRow(dtReport.Signatory3Column) = _Signatories.Signatory_3

                    dtRow(dtReport.DocDateColumn) = FormatDateTime(PaymentAllocation.AllocationDate, DateFormat.ShortDate)
                    dtRow(dtReport.IDNumberColumn) = _itmParticipant.IDNumber
                    dtRow(dtReport.ParticipantIDColumn) = _itmParticipant.ParticipantID
                    dtRow(dtReport.DocumentNoColumn) = ""
                    dtRow(dtReport.DueDateColumn) = ""

                    dtReport.Rows.Add(dtRow)
                    dtReport.AcceptChanges()
                End If


            End If
        Next

        frmProgress.Show()
        With rptView
            .LoadPaymentSummary(dtReport)
            .Show()
        End With

    End Sub

    Private Sub cmd_ViewORSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ViewORSummary.Click
        Try
            Dim ViewReport As New frmReportViewer
            Dim dt As New DSReport.CollectionDataTable

            If (Me.dgv_CollectionDetails.RowCount = 0 And Me.dgv_PaymentSummary.RowCount = 0) Or PaymentAllocation.lstOfficialReceipt.Count = 0 Then
                MsgBox("No records found", MsgBoxStyle.Exclamation, "No Records")
                Exit Sub
            End If

            'Get OR List
            Dim _lstOR As New List(Of OfficialReceiptMain)
            _lstOR = PaymentAllocation.lstOfficialReceipt
            Dim _GetParticipants = WBillHelper.GetAMParticipants()

            For Each itmOR In _lstOR
                Dim dr As DataRow
                dr = dt.NewRow

                With itmOR
                    dr(dt.OR_NOColumn) = Me.BFactory.GenerateBIRDocumentNumber(.ORNo, BIRDocumentsType.OfficialReceipt)
                    dr(dt.ID_NUMBERColumn) = .IDNumber
                    dr(dt.PARTICIPANT_IDColumn) = (From x In _GetParticipants _
                                                   Where x.IDNumber = .IDNumber _
                                                   Select x.ParticipantID).FirstOrDefault
                    dr(dt.AMOUNTColumn) = Math.Abs(.Amount)
                    dr(dt.TYPEColumn) = .TransactionType.ToString
                    dr(dt.DATE_ALLOCATEDColumn) = FormatDateTime(PaymentAllocation.AllocationDate, DateFormat.ShortDate)
                End With

                dt.Rows.Add(dr)
                dt.AcceptChanges()
            Next

            With ViewReport
                .LoadPaymentORSummary(dt, Me.PaymentAllocation.AllocationDate, Me.PaymentAllocation.AllocationDate)
                .Show()
            End With

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Encountered")
            Exit Sub
        Finally
            frmProgress.Close()
        End Try
    End Sub

End Class


