'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             MainForm
'Orginal Author:         Ramilo P. Mendoza
'File Creation Date:     August 22, 2011
'Development Group:      Software Development and Support Division
'Description:            Main form for Accounts Management
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   August 22, 2011         Ramilo P. Mendoza            GUI initialization
'   

Imports Library.Core.LDAP
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Imports WESMLib.Auth
Imports WESMLib.Auth.BO
Imports WESMLib.Auth.Lib
Imports System.Configuration
Imports System.String
Imports System.Threading
Imports System.Threading.Tasks

Public Class MainForm
    Private _DAL As DAL
    Private _BFactory As BusinessFactory
    Private _WBillHelper As WESMBillHelper    
    Private _userId As String = ""
    Private _userIdSet As String = ""

    'Private Sub New()

    '    Dim t As Thread = New Thread(New ThreadStart(AddressOf StartFlashScreen))
    '    t.Start()
    '    Thread.Sleep(5000)

    '    ' This call is required by the designer.
    '    InitializeComponent()
    '    t.Abort()
    '    ' Add any initialization after the InitializeComponent() call.

    'End Sub

    'Public Sub StartFlashScreen()
    '    Application.Run(New frmFlashScreen)
    'End Sub

    Private m_ChildFormNumber As Integer
    Private Sub ChargeIDToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChargeIDToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.LibChargeIDWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmChargeId.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AccountingCodeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AccountingCodeToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.LibAccountCodeWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            frmAccountingCode.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WESMBills_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WESMBills_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepWESMBillsWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmUploadedWESMBill.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WESMBillsSummaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WESMBillsSummary_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepWESMBillsSummary.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmWESMSummary.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub StatementAccount_TSMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatementAccount_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepSOAWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            With frmStatementOfAccount
                .isViewing = True
                .Show()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub JournalVoucherToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JournalVoucher_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepJVWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmViewJournalVoucher.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SignatoriesMaintenanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SignatoriesMaintenanceToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.LibSignatoriesMainteWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmSignatoriesMaintenance.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function SystemAuthentication() As Boolean
        _Login.HeaderTitle = "Login Form"
        _Login.ApplicationName = "Accounts Management System"
        _Login.AccessWindow = "AMS_AccessWindow"
        If _Login.ShowDialog(Me) = DialogResult.OK Then
            _userId = _Login.UserLogged
            Return True
        End If
        Return False
    End Function

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            If Not SystemAuthentication() Then
                Me.Close()
                Exit Sub
            End If

            ProgressThread.Show("Please wait while preparing the main window.")
            Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            Dim settings As KeyValueConfigurationCollection = config.AppSettings.Settings

            'Others            
            AMModule.ConnectionString = _Login.AMSConnectionString
            AMModule.ConnectionStringCRSS = _Login.CRSSConnectionString
            AMModule.UserName = _Login.UserLogged  '"Vloody"
            AMModule.FullName = _LDAPUserInfo.Name  '"Vladimir E. Espiritu" 
            AMModule.Position = _LDAPUserInfo.Position  '"Sr. Software Engineer" 

            User_TSStatusLabel.Text = "User: " & AMModule.UserName 'Strings.Left(AMModule.UserName, InStr(AMModule.UserName, "@") - 1).ToUpper

            _DAL = DAL.GetInstance()
            _BFactory = BusinessFactory.GetInstance()
            _WBillHelper = WESMBillHelper.GetInstance()
            _WBillHelper.ConnectionString = _Login.AMSConnectionString
            _WBillHelper.UserName = AMModule.UserName

            SystemDate = _WBillHelper.GetSystemDate()

            Dim dicSettings = _WBillHelper.GetDicAdminSettings()
            _BFactory.LoadSettingsValues(dicSettings)

            If Not _Login.HasAccess(EnumAMSModulesFinal.CollEditCollectionDefaultInterest.ToString) Then
                AMModule.IsAllowedToEditDefaultInterest = True
            Else
                AMModule.IsAllowedToEditDefaultInterest = False
            End If
            ProgressThread.Close()
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub



    Private Sub DailyInterestRateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DailyInterestRateToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.LibDailyInterestRateWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmInterestRate.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BillParticipantsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BillParticipantsToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.LibParticipantInfoWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmBillParticipants.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub GreatPlainsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GreatPlainsToolStripMenuItem.Click
    '    Try
    '        If Not _Login.HasAccess(EnumAMSModulesFinal.AMS_GreatPlainsWindow.ToString) Then
    '            MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.AMS_GreatPlainsWindow.ToString, "Accessing Great Plains WIndow", "", "", EnumColorCode.Red, EnumLogType.NoAccess, AMModule.UserName)
    '            Exit Sub
    '        Else
    '            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.AMS_GreatPlainsWindow.ToString, "Accessing Great Plains WIndow", "", "", EnumColorCode.Green, EnumLogType.SuccessfullyAccessed, AMModule.UserName)
    '        End If
    '        frmGPInterface.Show()
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub ManualDMCMToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManualDMCMToolStripMenuItem1.Click
    '    Try
    '        If Not _Login.HasAccess(EnumAMSModulesFinal.AMS_ManualDMCMWindow.ToString) Then
    '            MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.AMS_ManualDMCMWindow.ToString, "Accessing Manual DMCM Window", "", "", EnumColorCode.Red, EnumLogType.NoAccess, AMModule.UserName)
    '            Exit Sub
    '        Else
    '            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.AMS_ManualDMCMWindow.ToString, "Accessing Manual DMCM Window", "", "", EnumColorCode.Green, EnumLogType.SuccessfullyAccessed, AMModule.UserName)
    '        End If
    '        frmManualDMCM.Show()
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub CollectionSummary_TSMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CollectionSummary_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepCollectionSummaryWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Dim frm As New frmCollectionSummary
            frm.flag = 1
            frm.Show()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DebitCreditMemo_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepDebitCreditMemoWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmDebitCreditMemo.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FundTransferFormToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FundTransferForm_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepFundTransferFormWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmFTF.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub EFTToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EFT_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepEFTWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmEFT.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MarketFeesSummaryToolStrip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MarketFeesSummary_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepMFSummaryWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmMarketFeesSummary.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RequestForPaymentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RequestForPayment_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepRequestForPaymentWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmRPTRequestForPayment.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SummaryOfOutstandingBalancesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SummaryOfOutstandingBalances_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepSummaryofOBWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmOutstandingBalancesNew.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DefaultNoticeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DefaultNotice_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepDefaultNoticeWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmDefaultNotice.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TransferOfInterestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InterestToolStripMenuItem1.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.MonPrudentialIntWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmPrudentialInterest.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrudentialToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Prudential_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepPrudentialWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmPrudential.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SettlementNoticeToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SettlementNotice_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepSettleNoticeWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmSTLNoticeNew.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ReplenishmentToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReplenishmentToolStripMenuItem1.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.MonPrudentialRepWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmPrudentialReplenishment.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TransferOfInterestToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TransferOfInterestToolStripMenuItem1.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.MonPrudentialTransIntWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmPrudentialTransferInterest.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DailyCollectionSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DailyCollectionSummary_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepDCashCollSummaryWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Dim frm As New frmCollectionSummary
            frm.flag = 3
            frm.Show()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AgingReport_TSMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AgingReport_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepAgingReportWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmAgingReport.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CalendarBillingPeriodToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CalendarBillingPeriodToolStripMenuItem1.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.LibCalendarBillingWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmBillingCalendar.Show()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SummaryOfDefaultInterestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SummaryOfDefaultInterest_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepSummaryofDefaultWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmSummaryOfDefaultInterest.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SummaryForAccountingBooksToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SummaryForAccountingBooks_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepSummaryForABWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmSummary.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CashSummaryReport_TSMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CashSummaryReport_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepCashSummaryReportWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmCashSummaryReport.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub OfficialReceiptToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OfficialReceipt_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepOfficialReceiptWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmOfficialReceipt.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WESMBillToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WESMBillToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.SAPUploadWESMBillWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmImportWESMBillFlatfile.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WESMSalesAndPurchasedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WESMSalesAndPurchasedToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.SAPUploadWESMBillSAPWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmImportWESMBillSalesAndPurchased.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WESMBillOffsettingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WESMBillOffsettingToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.SAPWESMBillOffsetingWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmOffSetWESMBill.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub StatementOfAccountToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatementOfAccountToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.SAPSOAWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmStatementOfAccount.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ChecksToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Checks_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepChecksWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmChecks.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub CollectionEntryAllocationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CollectionEntryAllocationToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.CollEntryAllocationWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If _Login.HasAccess(EnumAMSModulesFinal.CollEditCollectionDefaultInterest.ToString()) Then
                AMModule.IsAllowedToEditDefaultInterest = True
            Else
                AMModule.IsAllowedToEditDefaultInterest = False
            End If
            frmCollection.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PostDailyCollectionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostDailyCollectionToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.CollPostDailyCollectionsWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim frm As New frmCollectionPost
            frm.CollectionPostType = EnumCollectionPostType.PostDaily
            frm.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PostManualCollectionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostManualCollectionsToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.CollPostManualCollectionsWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim frm As New frmCollectionPost
            frm.CollectionPostType = EnumCollectionPostType.PostManual
            frm.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub TestToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    frmUploadedWESMBillTracking.Show()
    'End Sub

    'Private Sub GenerateSettlementEndingBalanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GenerateSettlementEndingBalanceToolStripMenuItem.Click
    '    If Not _Login.HasAccess(EnumAMSModulesFinal.AMS_GenerateSettlementEndingBalanceWindow.ToString) Then
    '        MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.AMS_GenerateSettlementEndingBalanceWindow.ToString(), "Accessing Generate Settlement Ending Balance Window", "", "", EnumColorCode.Red, EnumLogType.NoAccess, AMModule.UserName)
    '        Exit Sub
    '    Else
    '        _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.AMS_GenerateSettlementEndingBalanceWindow.ToString, "Accessing Generate Settlement Ending Balance Window", "", "", EnumColorCode.Green, EnumLogType.SuccessfullyAccessed, AMModule.UserName)
    '    End If

    '    frmSettlementEndingBalance.Show()
    'End Sub

    Private Sub LogsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Logs_TSMenuItem.Click
        If Not _Login.HasAccess(EnumAMSModulesFinal.Logs.ToString) Then
            MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        frmLogs.Show()
    End Sub

    Private Sub DeferredPaymentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeferredPayment_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepDeferredPaymentWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmDeferredMonitoring.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ToolStripEarnedInterest_Click(sender As Object, e As EventArgs) Handles ToolStripEarnedInterest.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.MonEarnedInterestWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmInterestRateEarned.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SPAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SPAToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.MonSPAWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmSPA.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AccountsReceivableToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AccountsReceivable_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepSubsidiaryLedgerWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            With frmSLAccountsReceivable
                .Show()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AccountsPayableToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AccountsPayable_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepSubsidiaryLedgerWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            With frmSLAccountsPayable
                .Show()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrudentialPerParticipantToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrudentialPerParticipant_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepSubsidiaryLedgerWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            With frmSLPrudentialPerParticipant
                .Show()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CashInBankPrudentialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CashInBankPrudential_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepGeneralLedgerWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmGLCashInBankPrudential.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CashInBankSettlementToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CashInBankSettlement_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepGeneralLedgerWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmGLCashInBankSettlement.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub InterestPayableSettlementToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InterestPayableSettlement_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepGeneralLedgerWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmGLInterestPayableSettlement.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub InterestPayablePrudentialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InterestPayablePrudential_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepGeneralLedgerWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmGLInterestPayablePrudential.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub timerActiveWindowCheck_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerActiveWindowCheck.Tick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Status_TSStatusLabel.Text = "ActiveForm: " & activeChild.Text
        Else
            Status_TSStatusLabel.Text = "Ready"
        End If

        Date_TSStatusLabel.Text = System.DateTime.Today.ToLongDateString
        Time_TSStatusLabel.Text = System.DateTime.Now.ToLongTimeString
    End Sub

    Private Sub WithholdingTAXonEnergy_Click(sender As Object, e As EventArgs) Handles MAPBIR_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepMAPEWTCertifWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmMAPReport.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BIRAlphanumericTaxCode_TSMenuItem_Click(sender As Object, e As EventArgs) Handles BIRAlphanumericTaxCode_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.LibBATCodeWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmBIRATC.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BIRAccessToRecordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BIRAccessToRecordToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepBIRAccessToRecordWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmBIRAccessToRecord.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PaymentDetails_TSMenuItem_Click(sender As Object, e As EventArgs) Handles PaymentDetails_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepPaymentDetailsWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmPaymentSummaryDetails.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ParticipantMaintenanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ParticipantMaintenanceToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.LibParticipantPACEOWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmParticipantPCMapping.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WESMBillSummaryChangeParentIDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WESMBillSummaryChangeParentIDToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.LibWESMBillChangeParentIDWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmWBSParentIdChange.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PerParticipantToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PerParticipantToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.MonWhTaxAdjPerInvoiceWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmUpdateWESMBillSummary.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PerInvoiceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PerInvoiceToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.MonWhTaxWhtaxAdjustmentWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmWBSAdjustmentPerBatchMain.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WESMBillExtractionFromCRSSDBToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WESMBillExtractionFromCRSSDBToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.SAPUploadWESMBillFetchFromCRSSDBWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmImportWESMBillFromCRSS.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AllocateCollectionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllocateCollectionsToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.PayAllocateCollectionsWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmPaymentNew.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AllocateCollectionsCollectionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewAllocatedCollectionsToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.PayViewAllocatedCollectionsWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmPaymentNewView.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        Dim msgAns As New MsgBoxResult
        msgAns = MsgBox("Do you really want to logout?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "System Message")
        If msgAns = MsgBoxResult.Yes Then
            MessageBox.Show(Me, "You have successfully logout.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
            Process.Start(Application.ExecutablePath)
        End If
    End Sub

    Private Sub PostingWESMBillToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PostingWESMBillToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.SAPWESMBillJVPostingWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmGPInterface.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.MonRefundPRWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmPrudentialRefund.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ViewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.MonRefundPRWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmPrudentialRefundView.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SystemParametersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SystemParametersToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.SystemParamWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmSettings.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ExemptionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExemptionToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.MonWESMBillsExemptionWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmWBSExemption.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrintingOfWESMInvoiceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintingOfWESMInvoiceToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepWESMInvWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmWESMInvoice.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub ChangeBPinWBToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeBPinWBToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.ChangeWESMBillBP.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmWESMBillBillingPeriodMgt.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UploadCSVToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UploadCSVToolStripMenuItem.Click
        Try
            frmImportCSVInsert.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WESMBillTransactionSummaryToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.SAPUploadWESMBillWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmImportWESMTransAllocSummary.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrintingOfWESMTransactionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintingOfWESMTransactionToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepWESMInvWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmWESMBillTransactionSummaryPrint.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub WESMBillTransactionSummaryFetchFromCRSSDBToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WESMBillTransactionSummaryFetchFromCRSSDBToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.SAPUploadWESMBillFetchFromCRSSDBWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmImportWESMTransAllocSummaryFlatFile.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PaymentGenXTaggingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PaymentGenXTaggingToolStripMenuItem.Click
        Try
            frmPaymentTaggingMgt.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WithholdingTaxCertificateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WithholdingTaxCertificateToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.CollEntryAllocationWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmWHTaxCertificateSTLMgt.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BIRRulingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BIRRulingToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepCollectionSummaryWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmBRCollectionReportMgt.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WESMTransAllocationSummary_TSMenuItem_Click(sender As Object, e As EventArgs) Handles WESMTransAllocationSummary_TSMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.InqRepWESMBillsSummary.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmExportWTASummary.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WESMBillsMappingCRSSChangeInvoiceIDForAggregationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WESMBillsMappingCRSSChangeInvoiceIDForAggregationToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.SAPWESMBillAggregateMapping.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmImportCRSSMappingID.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub EWT_TagAlloc_Report_ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EWT_TagAlloc_Report_ToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.CollEntryAllocationWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmEWTTaggedAllocReport.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WESMBillsAggregateInvoicesMappingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WESMBillsAggregateInvoicesMappingToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.SAPWESMBillAggregateMapping.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmImportCRSSMappingID.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WESMTransactionSummaryFromCRSSDBToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WESMTransactionSummaryFromCRSSDBToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.SAPUploadWESMBillFetchFromCRSSDBWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmImportWTAFromCRSS.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WithholdingVATCertificateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WithholdingVATCertificateToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.CollEntryAllocationWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmWHVATCertificateSTLMgt.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WVAT_TagAlloc_ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WVAT_TagAlloc_ToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.CollEntryAllocationWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmWVATTaggedAllocReport.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SummaryofWTADetails_Click(sender As Object, e As EventArgs) Handles SummaryofWTADetails.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.CollEntryAllocationWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmSummaryOfWTADetails.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ReserveTransactionAllocationSummaryFromFlatFilePOrFToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReserveTransactionAllocationSummaryFromFlatFilePOrFToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.SAPUploadWESMBillWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmImportReserveTransAllocFlatFile.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ReserveTransactionSummaryFromCRSSDBToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReserveTransactionSummaryFromCRSSDBToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.SAPUploadWESMBillWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmImportReservedFromCRSS.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ReseveBillFromFlatFileMarketFeesPrelimToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReseveBillFromFlatFileMarketFeesPrelimToolStripMenuItem.Click
        Try
            If Not _Login.HasAccess(EnumAMSModulesFinal.SAPUploadWESMBillWindow.ToString) Then
                MessageBox.Show("You're not authorized to access this window! Please contact the administrator.", "System Message!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            frmImportReserveMFFlatFile.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
