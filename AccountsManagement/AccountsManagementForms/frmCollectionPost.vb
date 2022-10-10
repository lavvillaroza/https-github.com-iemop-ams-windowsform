'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmCollectionPost
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     February 09, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for Posting Manual Collection and Daily Collection
'Arguments/Parameters:  
'Files/Database Tables:  frmCollectionPost
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   October 09, 2012        Vladimir E. Espiritu                 GUI design and basic functionalities                    
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

'Imports LDAPLib
'Imports LDAPLogin

Public Class frmCollectionPost
    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory

#Region "Properties"
    Private _CollectionPostType As EnumCollectionPostType
    Public Property CollectionPostType() As EnumCollectionPostType
        Get
            Return _CollectionPostType
        End Get
        Set(ByVal value As EnumCollectionPostType)
            _CollectionPostType = value
        End Set
    End Property

    Private _LabelTransactionType As String
    Public ReadOnly Property LabelTransactionType() As String
        Get
            Select Case Me.CollectionPostType

                Case EnumCollectionPostType.PostDaily
                    _LabelTransactionType = "Collection Date:"

                Case EnumCollectionPostType.PostManual
                    _LabelTransactionType = "Allocation Date:"

            End Select

            Return _LabelTransactionType
        End Get
    End Property

    Private _ListCollections As List(Of Collection)
    Public Property ListCollections() As List(Of Collection)
        Get
            Return _ListCollections
        End Get
        Set(ByVal value As List(Of Collection))
            _ListCollections = value
        End Set
    End Property

    Private _ListCollectionMonitoring As List(Of CollectionMonitoring)
    Public Property ListCollectionMonitoring() As List(Of CollectionMonitoring)
        Get
            Return _ListCollectionMonitoring
        End Get
        Set(ByVal value As List(Of CollectionMonitoring))
            _ListCollectionMonitoring = value
        End Set
    End Property

    Private _ListDMCMDrawdown As List(Of DebitCreditMemo)
    Public Property ListDMCMDrawdown() As List(Of DebitCreditMemo)
        Get
            Return _ListDMCMDrawdown
        End Get
        Set(ByVal value As List(Of DebitCreditMemo))
            _ListDMCMDrawdown = value
        End Set
    End Property

    Private _ListDMCMSetup As List(Of DebitCreditMemo)
    Public Property ListDMCMSetup() As List(Of DebitCreditMemo)
        Get
            Return _ListDMCMSetup
        End Get
        Set(ByVal value As List(Of DebitCreditMemo))
            _ListDMCMSetup = value
        End Set
    End Property

    Private _ListOR As List(Of OfficialReceiptMain)
    Public Property ListOR() As List(Of OfficialReceiptMain)
        Get
            Return _ListOR
        End Get
        Set(ByVal value As List(Of OfficialReceiptMain))
            _ListOR = value
        End Set
    End Property

    Private _ListEFT As List(Of EFT)
    Public Property listEFT() As List(Of EFT)
        Get
            Return _ListEFT
        End Get
        Set(ByVal value As List(Of EFT))
            _ListEFT = value
        End Set
    End Property

    Private _ListFTF As List(Of FundTransferFormMain)
    Public Property listFTF() As List(Of FundTransferFormMain)
        Get
            Return _ListFTF
        End Get
        Set(ByVal value As List(Of FundTransferFormMain))
            _ListFTF = value
        End Set
    End Property

    Private _ItemJournalVoucher As JournalVoucher
    Public Property ItemJournalVoucher() As JournalVoucher
        Get
            Return _ItemJournalVoucher
        End Get
        Set(ByVal value As JournalVoucher)
            _ItemJournalVoucher = value
        End Set
    End Property

    Private _ItemGPPosted As WESMBillGPPosted
    Public Property ItemGPPosted() As WESMBillGPPosted
        Get
            Return _ItemGPPosted
        End Get
        Set(ByVal value As WESMBillGPPosted)
            _ItemGPPosted = value
        End Set
    End Property

    Private _ListParticipants As List(Of AMParticipants)
    Public Property ListParticipants() As List(Of AMParticipants)
        Get
            Return _ListParticipants
        End Get
        Set(ByVal value As List(Of AMParticipants))
            _ListParticipants = value
        End Set
    End Property

    Private _ListAccountingCodes As List(Of AccountingCode)
    Public Property ListAccountingCodes() As List(Of AccountingCode)
        Get
            Return _ListAccountingCodes
        End Get
        Set(ByVal value As List(Of AccountingCode))
            _ListAccountingCodes = value
        End Set
    End Property

#End Region

    Private Sub frmCollectionPost_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Try
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName

            BFactory = BusinessFactory.GetInstance()
            Me.ListCollections = New List(Of Collection)

            'Get the participants
            Me.ListParticipants = WBillHelper.GetAMParticipantsAll()

            'Get the accounting code
            Me.ListAccountingCodes = WBillHelper.GetAccountingCodes()

            Me.lblTransactionDate.Text = Me.LabelTransactionType

            Select Case Me.CollectionPostType
                Case EnumCollectionPostType.PostDaily
                    Me.Text = "Post to GP - Daily Collections"

                Case EnumCollectionPostType.PostManual
                    Me.Text = "Post to GP - Manual Collection Allocation"

            End Select

            'Format Datagrid
            'Me.FormatDatagrid()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")           
        End Try

    End Sub

    Private Sub btnViewJV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewJV.Click
        Try
            If Me.DGridViewCollection.RowCount = 0 Then
                MsgBox("Nothing to view!", MsgBoxStyle.Exclamation, "No data")
                Exit Sub
            End If

            ProgressThread.Show("Please wait while processing Deferred Payment Report.")

            'Generate dataset for Journal voucher
            Dim ds = BFactory.GenerateJournalVoucherReport(Me.ListAccountingCodes, Me.ItemJournalVoucher, New DSReport.JournalVoucherDataTable, _
                                                           New DSReport.JournalVoucherDetailsDataTable, New DSReport.AccountingCodeDataTable)

            Dim frmViewer As New frmReportViewer()
            With frmViewer
                frmViewer.LoadJournalVoucherDraft(ds)
                ProgressThread.Close()
                .ShowDialog()
            End With
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try        
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        Try

            Select Case CollectionPostType
                Case EnumCollectionPostType.PostDaily
                    If Me.ListCollections.Count = 0 Then
                        MsgBox("No available collection/s to post in the specified collection date!", MsgBoxStyle.Exclamation, "No data")
                        Exit Sub
                    End If

                    Dim ans = MsgBox("Do you really want to post the daily collection/s?", _
                                MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")
                    If ans = MsgBoxResult.No Then
                        Exit Sub
                    End If

                    'Save/Update Journal Voucher and GP Posted
                    WBillHelper.PostDailyCollections(Me.ListCollections, Me.ItemJournalVoucher, Me.ItemGPPosted)

                    MsgBox("Successfully posted to great plains!", MsgBoxStyle.Information, "Posted")
                    'LDAPModule.LDAP.InsertLog(Me.Name, EnumAMSModules.AMS_PostDailyCollectionsWindow.ToString(), Me.dtTransactionDate.Value.ToString(), "", "", EnumColorCode.Blue.ToString(), EnumLogType.SuccessfullyPosted.ToString(), 'LDAPModule.LDAP.Username)

                Case EnumCollectionPostType.PostManual
                    If ListCollections.Count = 0 Then
                        MsgBox("No available applied collection/s to post in the specified allocation date!", MsgBoxStyle.Exclamation, "No data")
                        Exit Sub
                    End If

                    Dim ans = MsgBox("Do you really want to post the unposted collection in great plains?", _
                                 MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")
                    If ans = MsgBoxResult.No Then
                        Exit Sub
                    End If

                    'Selection of JV date
                    Dim frmJVDate As New frmCollectionSearch
                    With frmJVDate
                        Dim valSize As New System.Drawing.Size
                        valSize.Width = 343
                        valSize.Height = 130

                        .Size = valSize
                        .LoadType = frmCollectionSearch.EnumFunctionType.SelectJVDate

                        If .ShowDialog() <> Windows.Forms.DialogResult.OK Then
                            Exit Sub
                        End If
                    End With
                    Dim JVDate = CDate(FormatDateTime(frmJVDate.dtAllocationDate.Value, DateFormat.ShortDate))

                    'Save/Update Journal Voucher, GP Posted and list of collection monitoring
                    WBillHelper.PostManualCollectionAllocation(Me.ListCollections, Me.listFTF, Me.ItemJournalVoucher, _
                                                               Me.ItemGPPosted, Me.listEFT, JVDate)

                    MsgBox("Successfully posted to great plains!", MsgBoxStyle.Information, "Posted")
                    'LDAPModule.LDAP.InsertLog(Me.Name, EnumAMSModules.AMS_PostManualAllocatedCollectionWindow.ToString(), Me.dtTransactionDate.Value.ToString(), "", "", EnumColorCode.Blue.ToString(), EnumLogType.SuccessfullyPosted.ToString(), 'LDAPModule.LDAP.Username)
            End Select

            Me.DGridViewCollection.Rows.Clear()
            Me.ListCollections = New List(Of Collection)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")           
        End Try
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Try            
            Select Case Me.CollectionPostType
                Case EnumCollectionPostType.PostDaily

                    If CDate(Me.dtTransactionDate.Value.ToString("MM/dd/yyyy")) > CDate(SystemDate.ToString("MM/dd/yyyy")) Then
                        MsgBox("Specified date must not be later than date today!", MsgBoxStyle.Critical, "Invalid Date")
                        Exit Sub
                    End If

                    'Get the unpost daily collections
                    Me.ListCollections = WBillHelper.GetCollectionsDaily(CDate(FormatDateTime(Me.dtTransactionDate.Value, DateFormat.ShortDate)))

                    'Generate Journal Voucher
                    Me.ItemJournalVoucher = Me.BFactory.GenerateDailyCollectionJournalVoucher(Me.ListCollections, _
                                                                                              Me.WBillHelper.GetSignatories("JV").First(), _
                                                                                              CDate(Me.dtTransactionDate.Value.ToString("MM/dd/yyyy")))

                    'Generate the GP Posted
                    Me.ItemGPPosted = Me.BFactory.GenerateDailyCollectionGPPosted(Me.ItemJournalVoucher)

                    If ListCollections.Count = 0 Then
                        MsgBox("No unposted collections for the specified collection date!", MsgBoxStyle.Exclamation, "No data")
                        Exit Sub
                    End If

                Case EnumCollectionPostType.PostManual
                    'Get the unpost collections which are manual allocation
                    ListCollections = WBillHelper.GetCollections(CDate(FormatDateTime(Me.dtTransactionDate.Value, DateFormat.ShortDate)), _
                                                                 EnumAllocationType.Manual)

                    If ListCollections.Count = 0 Then
                        MsgBox("No unposted collections for the specified allocation date!", MsgBoxStyle.Exclamation, "No data")
                        Exit Sub
                    End If

                    

                    'Add temporary variable to hold the list of collections.
                    Dim tempListCollections As New List(Of Collection)                    
                    For Each item In ListCollections
                        tempListCollections.Add(CType(BFactory.CloneObject(item), Collection))                        
                    Next


                    'Get all unpost collection monitoring
                    Me.ListCollectionMonitoring = WBillHelper.GetCollectionMonitoringNotPosted()

                    'Join list of manual collections and all unpost collection monitoring
                    Dim listCollectionMonitoringFinal = (From x In ListCollections Join y In ListCollectionMonitoring _
                                                         On x.CollectionNumber Equals y.CollectionNo _
                                                         Select y).ToList()

                    'Create Debit/Credit for drawdown which will be use in journal voucher only
                    Me.ListDMCMDrawdown = Me.BFactory.GenerateDMCMDrawdown(CType(Me.BFactory.CloneObject(ListCollections), List(Of Collection)), _
                                                                            Me.WBillHelper.GetSignatories("DMCM").First, _
                                                                            Me.WBillHelper.GetDailyInterestRate())

                    'Generate DMCM Setup  which will be use in journal voucher only
                    Me.ListDMCMSetup = Me.BFactory.GenerateCollectionDMCM(CType(Me.BFactory.CloneObject(ListCollections), List(Of Collection)), _
                                                                          Me.WBillHelper.GetSignatories("DMCM").First, _
                                                                          Me.WBillHelper.GetDailyInterestRate())

                    'Generate Official Receipt  which will be use in journal voucher only
                    Me.ListOR = Me.WBillHelper.GetOfficialReceiptByAllocDate(CDate(Me.dtTransactionDate.Value.ToString("MM/dd/yyyy")))

                    'Generate the EFT
                    Me.listEFT = Me.BFactory.GenerateCollectionEFT(listCollectionMonitoringFinal)

                    'Generate the FTF
                    Me.listFTF = Me.BFactory.GenerateCollectionFTF(CDate(FormatDateTime(Me.dtTransactionDate.Value, DateFormat.ShortDate)), _
                                                                    listCollectionMonitoringFinal, ListCollections, Me.WBillHelper.GetSignatories("FTF").First)

                    'Generate Journal Voucher
                    Me.ItemJournalVoucher = Me.BFactory.GenerateCollectionJournalVoucher(ListDMCMSetup, ListDMCMDrawdown, ListOR, listFTF, _
                                                                                         Me.WBillHelper.GetSignatories("JV").First, _
                                                                                         CDate(Me.dtTransactionDate.Value.ToString("MM/dd/yyyy")))

                    'Generate the GP Posted
                    Me.ItemGPPosted = Me.BFactory.GenerateCollectionGPPosted(Me.ItemJournalVoucher)
            End Select

            'Load the collections on the grid
            Me.DisplayCollections()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")           
        End Try
    End Sub

    Private Sub btnCollectionSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCollectionSummary.Click
        If Me.DGridViewCollection.RowCount = 0 Then
            MsgBox("Nothing to view!", MsgBoxStyle.Exclamation, "No data")
            Exit Sub
        End If

        Try
            ProgressThread.Show("Please wait while processing.")
            Select Case Me.CollectionPostType
                Case EnumCollectionPostType.PostDaily
                    'Get the Signatories
                    Dim Signatories = WBillHelper.GetSignatories("DCS").First()

                    'Generate collection summary for manual
                    Dim dt = BFactory.GenerateDailyCollectionSummary(EnumPostedType.DC.ToString() & "-XXX", 0, Me.ListCollections, _
                                                                     Me.ListParticipants, Signatories, _
                                                                     New DSReport.DailyCollectionSummaryDataTable)
                    Dim frmViewer As New frmReportViewer()
                    With frmViewer
                        .LoadDailyCollectionSummary(dt)
                        ProgressThread.Close()
                        .ShowDialog()
                    End With


                Case EnumCollectionPostType.PostManual

                    Dim TotalCash As Decimal = 0, TotalDrawdown As Decimal = 0

                    'Get the Signatories
                    Dim Signatories = WBillHelper.GetSignatories("CS").First()

                    'Get the DMCM which are not yet posted
                    Dim listDMCM = WBillHelper.GetDebitCreditMemoMainFromJV(0)

                    'Generate collection summary for manual
                    Dim dt = BFactory.GenerateCollectionSummary(EnumPostedType.C.ToString() & "-XXX", 0, Me.ListCollections, _
                                                                 Me.ListCollectionMonitoring, Me.ListParticipants, _
                                                                 listDMCM, Signatories, TotalCash, TotalDrawdown, _
                                                                 New DSReport.CollectionSummaryDataTable, "")
                    Dim frmViewer As New frmReportViewer()
                    With frmViewer
                        .LoadCollectionSummaryPerJV(dt, TotalCash, TotalDrawdown)
                        ProgressThread.Close()
                        .ShowDialog()
                    End With
            End Select

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)           
        End Try

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

#Region "Methods/Functions"

    Private Sub DisplayCollections()
        Me.DGridViewCollection.Rows.Clear()

        Dim listItems = From x In Me.ListCollections Join y In Me.ListParticipants _
                        On x.IDNumber Equals y.IDNumber _
                        Select x, y.ParticipantID Order By x.ORNo


        For Each item In listItems
            With item.x
                Dim createdDocument As String = ""

                If .DMCMNumber <> 0 Then
                    createdDocument = BFactory.GenerateBIRDocumentNumber(.DMCMNumber, BIRDocumentsType.DMCM)
                End If

                If .ORNo <> 0 Then
                    createdDocument = BFactory.GenerateBIRDocumentNumber(.ORNo, BIRDocumentsType.OfficialReceipt)
                End If

                Me.DGridViewCollection.Rows.Add(.CollectionNumber, .ORNo, .DMCMNumber, createdDocument, _
                                                FormatDateTime(.CollectionDate, DateFormat.ShortDate), .IDNumber, _
                                                item.ParticipantID, .CollectedAmount + .CollectedHeld, .CollectionCategory, .AllocationType)

            End With
        Next

    End Sub

    Private Sub FormatDatagrid()
        Me.DGridViewCollection.Columns("colCollected").ValueType = GetType(Decimal)
        Me.DGridViewCollection.Columns("colCollected").DefaultCellStyle.Format = "#,###.##"
    End Sub

#End Region


End Class