Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Imports System.Text
Imports System.IO
Imports System.Windows.Forms
Imports System.Threading

Public Class PaymentSaveHelper : Implements IDisposable
    Public _objDAL As DAL
    Private ReadOnly Property objDAL() As DAL
        Get
            Return _objDAL
        End Get
    End Property

    Public _objWBillHelper As WESMBillHelper
    Private ReadOnly Property objWBillHelper() As WESMBillHelper
        Get
            Return _objWBillHelper
        End Get
    End Property

    Public _AllocationDate As New AllocationDate
    Private ReadOnly Property AllocationDate() As AllocationDate
        Get
            Return _AllocationDate
        End Get
    End Property

    Public _AMCollectionAR As New List(Of ARCollection)
    Private ReadOnly Property AMCollectionAR() As List(Of ARCollection)
        Get
            Return _AMCollectionAR
        End Get
    End Property


    Public _AMPaymentNewAP As New List(Of APAllocation)
    Private ReadOnly Property AMPaymentNewAP() As List(Of APAllocation)
        Get
            Return _AMPaymentNewAP
        End Get
    End Property

    Public _AMPaymentNewOffsettingAP As New List(Of APAllocation)
    Private ReadOnly Property AMPaymentNewOffsettingAP() As List(Of APAllocation)
        Get
            Return _AMPaymentNewOffsettingAP
        End Get
    End Property

    Public _AMPaymentNewOffsettingAR As New List(Of ARCollection)
    Private ReadOnly Property AMPaymentNewOffsettingAR() As List(Of ARCollection)
        Get
            Return _AMPaymentNewOffsettingAR
        End Get
    End Property

    Public _PaymentDMCMList As New List(Of DebitCreditMemo)
    Private ReadOnly Property PaymentDMCMList() As List(Of DebitCreditMemo)
        Get
            Return _PaymentDMCMList
        End Get
    End Property

    Public _AMPaymentTransferToPR As New List(Of PaymentTransferToPR)
    Private ReadOnly Property AMPaymentTransferToPR() As List(Of PaymentTransferToPR)
        Get
            Return _AMPaymentTransferToPR
        End Get
    End Property

    Public _UpdatedWESMBillSummaryList As New List(Of WESMBillSummary)
    Private ReadOnly Property UpdatedWESMBillSummaryList() As List(Of WESMBillSummary)
        Get
            Return _UpdatedWESMBillSummaryList
        End Get
    End Property

    Public _UpdatedWESMBillSummaryWHTAXAdjList As New List(Of WESMBillSummary)
    Private ReadOnly Property UpdatedWESMBillSummaryWHTAXAdjList() As List(Of WESMBillSummary)
        Get
            Return _UpdatedWESMBillSummaryWHTAXAdjList
        End Get
    End Property


    Public _PaymentFTF As New List(Of FundTransferFormMain)
    Private ReadOnly Property PaymentFTF() As List(Of FundTransferFormMain)
        Get
            Return _PaymentFTF
        End Get
    End Property

    Public _CollectionFTF As New List(Of FundTransferFormMain)
    Private ReadOnly Property CollectionFTF() As List(Of FundTransferFormMain)
        Get
            Return _CollectionFTF
        End Get
    End Property


    Public _NewPaymentDeferred As New List(Of DeferredMain)
    Private ReadOnly Property NewPaymentDeferred() As List(Of DeferredMain)
        Get
            Return _NewPaymentDeferred
        End Get
    End Property

    Public _PaymentEFT As New List(Of EFT)
    Private ReadOnly Property PaymentEFT() As List(Of EFT)
        Get
            Return _PaymentEFT
        End Get
    End Property

    Public _AMPaymentShare As New List(Of PaymentShare)
    Private ReadOnly Property AMPaymentShare() As List(Of PaymentShare)
        Get
            Return _AMPaymentShare
        End Get
    End Property

    Public _JournalVoucherList As New List(Of JournalVoucher)
    Private ReadOnly Property JournalVoucherList() As List(Of JournalVoucher)
        Get
            Return _JournalVoucherList
        End Get
    End Property

    Public _WESMBillGPPostedList As New List(Of WESMBillGPPosted)
    Private ReadOnly Property WESMBillGPPostedList() As List(Of WESMBillGPPosted)
        Get
            Return _WESMBillGPPostedList
        End Get
    End Property

    Public _RFPList As New RequestForPayment
    Private ReadOnly Property RFPList() As RequestForPayment
        Get
            Return _RFPList
        End Get
    End Property

    Public _WESMBillSummaryBalance As New List(Of PaymentWBSHistoryBalance)
    Private ReadOnly Property WESMBillSummaryBalance() As List(Of PaymentWBSHistoryBalance)
        Get
            Return _WESMBillSummaryBalance
        End Get
    End Property

    Public _PrudentialHistoryList As New List(Of PrudentialHistory)
    Private ReadOnly Property PrudentialHistoryList() As List(Of PrudentialHistory)
        Get
            Return _PrudentialHistoryList
        End Get
    End Property

    Public _ORList As New List(Of OfficialReceiptMain)
    Private ReadOnly Property ORList() As List(Of OfficialReceiptMain)
        Get
            Return _ORList
        End Get
    End Property

    Public _WESMBillSummaryNoTransAR As New List(Of ARCollection)
    Private ReadOnly Property WESMBillSummaryNoTransAR() As List(Of ARCollection)
        Get
            Return _WESMBillSummaryNoTransAR
        End Get
    End Property

    Public _WESMBillSummaryNoTransAP As New List(Of APAllocation)
    Private ReadOnly Property WESMBillSummaryNoTransAP() As List(Of APAllocation)
        Get
            Return _WESMBillSummaryNoTransAP
        End Get
    End Property

#Region "Property of WESM Transaction Details Summary"
    Public _WESMTransDetailsSummaryList As New List(Of WESMTransDetailsSummary)
    Private ReadOnly Property WESMTransDetailsSummaryList() As List(Of WESMTransDetailsSummary)
        Get
            Return _WESMTransDetailsSummaryList
        End Get

    End Property
#End Region

#Region "Property of WESM Transaction Details Summary"
    Public _WESMTransDetailsSummaryHistoryList As New List(Of WESMTransDetailsSummaryHistory)
    Private ReadOnly Property WESMTransDetailsSummaryHistoryList() As List(Of WESMTransDetailsSummaryHistory)
        Get
            Return _WESMTransDetailsSummaryHistoryList
        End Get

    End Property
#End Region

    Public Sub ClearObjects()
        _WESMBillSummaryNoTransAP = Nothing
        _WESMBillSummaryNoTransAR = Nothing
        _ORList = Nothing
        _PrudentialHistoryList = Nothing
        _RFPList = Nothing
        _WESMBillGPPostedList = Nothing
        _JournalVoucherList = Nothing
        _AMPaymentShare = Nothing
        _PaymentEFT = Nothing
        _NewPaymentDeferred = Nothing
        _CollectionFTF = Nothing
        _PaymentFTF = Nothing
        _UpdatedWESMBillSummaryWHTAXAdjList = Nothing
        _UpdatedWESMBillSummaryList = Nothing
        _AMPaymentTransferToPR = Nothing
        _PaymentDMCMList = Nothing
        _AMPaymentNewOffsettingAR = Nothing
        _AMPaymentNewOffsettingAP = Nothing
        _AMPaymentNewAP = Nothing
        _AMCollectionAR = Nothing
        _WESMTransDetailsSummaryList = Nothing
        _WESMTransDetailsSummaryHistoryList = Nothing
        _AllocationDate = Nothing
        _objWBillHelper = Nothing
        _objDAL = Nothing
    End Sub

    Private PaymentNumber As Long
    Private dicListofSeq As New Dictionary(Of String, Long)

    'This method is for backup procedure only if directly saving cause of an error encountered.
    Public Sub SaveToTxtFile()
        Dim ListofSQLData As New List(Of String)
        Dim ListofSQLSeq As New List(Of String)
        Try

            Dim ans As New MsgBoxResult
            Dim _fldrSelect As New FolderBrowserDialog
            Dim fPathName As String = ""

            ans = MsgBox("Do you really want to export the records as CSV Format?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Export records")
            If ans <> MsgBoxResult.Yes Then
                Exit Sub
            End If

            With _fldrSelect
                .ShowDialog()
                If .SelectedPath = "" Then
                    Exit Sub
                End If
                fPathName = .SelectedPath
            End With

            ListofSQLData = Me.CreateSQLStatement
            Dim counter As Integer = 0, fileCount As Integer = 0
            Dim getListOfSQLData As List(Of String) = New List(Of String)
            For Each item In ListofSQLData
                counter += 1
                getListOfSQLData.Add(item)
                If (counter Mod 50000) = 0 Then
                    fileCount += 1
                    CreateCSV(getListOfSQLData, fPathName & "\PaymentSaveDataSQLs Part " & fileCount)
                End If
            Next
        Catch ex As Exception
            ListofSQLData = Nothing
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub

    Private Sub CreateCSV(listOfSQL As List(Of String), filename As String)
        Dim strBldr As New StringBuilder()
        'Data       
        For Each item In listOfSQL
            strBldr.AppendLine(item & ";")
            strBldr.AppendLine()
        Next
        Using fs As FileStream = File.Create(filename)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(strBldr.ToString)
            fs.Write(info, 0, info.Length)
        End Using
    End Sub

    Public Sub SaveToDB(ByVal progress As IProgress(Of ProgressClass), ByVal ct As CancellationToken)
        Dim report As New DataReport
        Dim ListofSQL As New List(Of String)
        Try
            ListofSQL = Me.CreateOptimizeSQLStatement
            report = Me.objDAL.ExecuteSaveQuery2(ListofSQL, progress, ct)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            'ListofSQL = CreateSQLStatementNewForSeq(dicListofSeq)
            'report = Me.objDAL.ExecuteSaveQuery2(ListofSQL, progress, ct)

            'If report.ErrorMessage.Length <> 0 Then
            '    Throw New ApplicationException(report.ErrorMessage)
            'End If
        Catch ex As Exception
            ListofSQL = Nothing
            Throw New ApplicationException(ex.Message)
        Finally
            ListofSQL = Nothing
        End Try
    End Sub

    Private Function CreateSQLStatementNewForSeq(ByVal updatedDicSeqList As Dictionary(Of String, Long)) As List(Of String)
        Dim listSQL As New List(Of String)
        For Each kvp As KeyValuePair(Of String, Long) In updatedDicSeqList
            Dim newValSeq As Long = CLng(updatedDicSeqList.Item(kvp.Key))
            Dim sql As String = "ALTER SEQUENCE " & kvp.Key & " RESTART START WITH " & newValSeq

            listSQL.Add(sql)
        Next
        Return listSQL
    End Function

    Private Function CreateOptimizeSQLStatement() As List(Of String)
        Dim listSQL As New List(Of String)
        Dim SQL As String
        Dim SysDateTime As Date = objWBillHelper.GetSystemDateTime()
        Dim UpdatedBy As String = AMModule.UserName
        Dim PaymentNo As Long
        Dim PaymentShareNo As Long

        Dim dicDMCM As New Dictionary(Of Long, Long)
        Dim dicJVNo As New Dictionary(Of String, Long)
        Dim dicJVBatchNo As New Dictionary(Of String, Long)

        PaymentNo = objWBillHelper.GetSequenceID("SEQ_AM_PAY_NO")
        Me.PaymentNumber = PaymentNo

        'Updating AM_COLLECTION
        SQL = "UPDATE AM_COLLECTION SET IS_ALLOCATED = " & EnumIsAllocated.Allocated _
            & " WHERE ALLOCATION_DATE = TO_DATE('" & Me.AllocationDate.CollAllocationDate.ToShortDateString & "', 'MM/dd/yyyy')"
        listSQL.Add(SQL)

        'Insert AM_JV, AM_JV_DETAILS, AM_CHECKS, AM_WESM_BILL_GP_POSTED        
        Dim ListofJVNumber As New List(Of Long)
        For Each item In Me.JournalVoucherList

            Dim BatchCode As Long = objWBillHelper.GetSequenceID("SEQ_AM_BATCH_CODE")
            Dim JVNumber As Long = objWBillHelper.GetSequenceID("SEQ_AM_JV_NO")
            Dim ItemPostedType As EnumPostedType = CType([Enum].Parse(GetType(EnumPostedType), CStr(item.PostedType)), EnumPostedType)

            If Not dicJVNo.ContainsKey(CStr(item.PostedType)) Then
                dicJVNo.Add(CStr(item.PostedType), JVNumber)
            End If
            If Not dicJVBatchNo.ContainsKey(CStr(item.PostedType)) Then
                dicJVBatchNo.Add(CStr(item.PostedType), BatchCode)
            End If

            ListofJVNumber.Add(JVNumber)
            With item
                If ItemPostedType = EnumPostedType.PEFT Then
                    For Each Checkitem In RFPList.RFPDetails.Where(Function(x) x.PaymentType = EnumParticipantPaymentType.Check And x.RFPDetailsType = EnumRFPDetailsType.Payment)
                        With Checkitem
                            SQL = "INSERT INTO AM_CHECKS (TRANSACTION_DATE, ID_NUMBER, AMOUNT, CHECK_NUMBER, CHECK_TYPE, CV_NUMBER, UPDATED_BY, STATUS, REMARKS, BATCH_CODE) " &
                                  "SELECT TO_DATE('" & .AllocationDate & "','MM/DD/YYYY'),'" &
                                            .Participant & "'," & .Amount & ", '', '', '', '" &
                                            AMModule.UserName & "'," & "1, '" & .Particulars & "', '" &
                                            ItemPostedType.ToString & "-" & BatchCode & "' FROM DUAL"
                            listSQL.Add(SQL)
                        End With
                    Next
                End If

                'WESMBillGPPosted
                SQL = "INSERT INTO AM_WESM_BILL_GP_POSTED (BILLING_PERIOD, STL_RUN, CHARGE_TYPE, DUE_DATE, REMARKS, POSTED, UPDATED_BY, BATCH_CODE, GP_REFNO, POSTED_TYPE, DOCUMENT_AMOUNT, AM_JV_NO) " &
                      "SELECT '','','','','" & .Remarks & "', '0', '" &
                                    .UpdatedBy & "', '" &
                                    ItemPostedType.ToString & "-" & BatchCode & "', '', '" &
                                    ItemPostedType.ToString & "', '" &
                                    Math.Round((From x In .JVDetails Select x.Debit).Sum, 2) & "', '" &
                                    JVNumber & "' FROM DUAL"
                listSQL.Add(SQL)

                SQL = "INSERT INTO AM_JV(AM_JV_NO, AM_JV_DATE, BATCH_CODE, STATUS, PREPARED_BY, CHECKED_BY, APPROVED_BY, UPDATED_BY, UPDATED_DATE, POSTED_TYPE) " _
                    & "SELECT " & JVNumber & ", TO_DATE('" & .JVDate.ToShortDateString & "','mm/dd/yyyy'), '" & ItemPostedType.ToString & "-" & BatchCode _
                                     & "', '" & .Status & "', '" & .PreparedBy & "', '" & .CheckedBy & "', '" & .ApprovedBy & "', '" & .UpdatedBy _
                                     & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & ItemPostedType.ToString() & "' FROM DUAL"
                listSQL.Add(SQL)
                'Insert AM_JV_DETAILS
                For Each detail In .JVDetails
                    SQL = "INSERT INTO AM_JV_DETAILS (AM_JV_NO, ACCT_CODE, DEBIT, CREDIT, UPDATED_BY, UPDATED_DATE) " _
                        & "SELECT " & JVNumber & ", '" & detail.AccountCode & "', " & detail.Debit & ", " & detail.Credit _
                                & ", '" & item.UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') FROM DUAL"
                    listSQL.Add(SQL)
                Next
            End With
        Next

        'Insert AM_DMCM query database        
        For Each item In PaymentDMCMList
            'Generate DMCM NO            
            Dim DMCMNo As Long = objWBillHelper.GetSequenceID("SEQ_AM_DMCM_NO")
            Dim JournalVoucherNo As Long = dicJVNo(EnumPostedType.PA.ToString())

            If Not dicDMCM.ContainsKey(item.DMCMNumber) Then
                dicDMCM.Add(item.DMCMNumber, DMCMNo)
            Else
                DMCMNo = dicDMCM(item.DMCMNumber)
            End If

            SQL = "INSERT INTO AM_DMCM (AM_DMCM_NO, AM_JV_NO, ID_NUMBER, PARTICULARS, CHARGE_TYPE, PREPARED_BY, CHECKED_BY, APPROVED_BY, " &
                    "UPDATED_BY, UPDATED_DATE, TRANS_TYPE, VATABLE, VAT, VAT_EXEMPT, VAT_ZERO_RATED, TOTAL_AMOUNT_DUE, EWT, EWV, STATUS, OTHERS, BILLING_PERIOD, DUE_DATE) " &
                    "SELECT '" & DMCMNo & "', '" & JournalVoucherNo & "', '" & item.IDNumber & "', '" & item.Particulars & "', '" & item.ChargeType.ToString & "', '" & item.PreparedBy & "', '" & item.CheckedBy & "', '" & item.ApprovedBy & "', " &
                    "'" & item.UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & item.TransType & "', '" & item.Vatable & "', '" & item.VAT & "', '" & item.VATExempt & "', '" & item.VatZeroRated & "', " &
                    "'" & item.TotalAmountDue & "', '" & item.EWT & "','" & item.EWV & "', '1', '" & item.Others & "', '" & item.BillingPeriod & "', TO_DATE('" & item.DueDate & "','MM/DD/YYYY') FROM DUAL"
            listSQL.Add(SQL)

            'Insert AM_DMCM_DETAILS query database
            For Each itemDMCM In item.DMCMDetails
                SQL = "INSERT INTO AM_DMCM_DETAILS (AM_DMCM_NO, ACCT_CODE, DEBIT, CREDIT, UPDATED_BY, UPDATED_DATE, INV_DM_CM, SUMMARY_TYPE, ID_NUMBER, IS_COMPUTE) " &
                        "SELECT '" & DMCMNo & "', '" & itemDMCM.AccountCode & "', '" & itemDMCM.Debit & "', '" & itemDMCM.Credit & "', '" & itemDMCM.UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), " &
                        "'" & itemDMCM.InvDMCMNo & "', '" & itemDMCM.SummaryType & "', '" & itemDMCM.IDNumber.IDNumber & "', '" & itemDMCM.IsComputed & "' FROM DUAL"
                listSQL.Add(SQL)
            Next
        Next

        'Insert into AM_PAYMENT_NEW query database        
        SQL = "INSERT INTO AM_PAYMENT_NEW (PAYMENT_NO,ALLOCATION_DATE,UPDATED_BY,UPDATED_DATE,IS_POSTED,REMARKS,REMITTANCE_DATE) " &
            "SELECT '" & PaymentNo & "', TO_DATE('" & Me.AllocationDate.CollAllocationDate.ToShortDateString & "','MM/DD/YYYY'),'" & UpdatedBy & "', " &
            "TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '0', NULL, TO_DATE('" & Me.AllocationDate.RemittanceDate.ToShortDateString & "','MM/DD/YYYY') FROM DUAL"
        listSQL.Add(SQL)
        'Insert into AM_PAYMENT_NEW_DETAILS query database
        For Each item In ListofJVNumber
            SQL = "INSERT INTO AM_PAYMENT_NEW_DETAILS (PAYMENT_NO, AM_JV_NO) " &
                 "SELECT " & PaymentNo & ", " & item & " FROM DUAL"
            listSQL.Add(SQL)
        Next

        'Insert into AM_PAYMENT_NEW_AP query database
        For Each item In Me._AMPaymentNewAP
            If Not item.GeneratedDMCM = 0 Then
                Dim _DMCMNumber As Long = dicDMCM(item.GeneratedDMCM)
                'add ENERGY_WITHHOLD and ID_NUMBER for CAP SUMMARY HIstorical Purposes added by lance as of 06/05/2020
                SQL = "INSERT INTO AM_PAYMENT_NEW_AP (PAYMENT_NO, BILLING_PERIOD, ENDING_BALANCE, NEW_ENDING_BALANCE, DUE_DATE, NEW_DUEDATE, " &
                   "ALLOCATION_AMOUNT, PAYMENT_TYPE, PAYMENT_CATEGORY, ALLOCATION_DATE, WESMBILL_SUMMARY_NO, UPDATED_BY, UPDATED_DATE, AM_DMCM_NO, ENERGY_WITHHOLD, ID_NUMBER) " &
                   "SELECT '" & PaymentNo & "', '" & item.BillingPeriod & "', '" & item.EndingBalance & "', '" & item.NewEndingBalance & "', TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), " &
                   "TO_DATE('" & item.NewDueDate & "','MM/DD/YYYY'), '" & item.AllocationAmount & "', '" & item.PaymentType & "', " & item.PaymentCategory & ", TO_DATE('" & item.AllocationDate & "','MM/DD/YYYY'), " &
                   "'" & item.WESMBillSummaryNo & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss')," & _DMCMNumber & ", " & item.EnergyWithHold & ", '" & item.IDNumber & "' FROM DUAL"
                listSQL.Add(SQL)
            Else
                'add ENERGY_WITHHOLD and ID_NUMBER for CAP SUMMARY HIstorical Purposes added by lance as of 06/05/2020
                SQL = "INSERT INTO AM_PAYMENT_NEW_AP (PAYMENT_NO, BILLING_PERIOD, ENDING_BALANCE, NEW_ENDING_BALANCE, DUE_DATE, NEW_DUEDATE, " &
                   "ALLOCATION_AMOUNT, PAYMENT_TYPE, PAYMENT_CATEGORY, ALLOCATION_DATE, WESMBILL_SUMMARY_NO, UPDATED_BY, UPDATED_DATE, AM_DMCM_NO, ENERGY_WITHHOLD, ID_NUMBER) " &
                   "SELECT '" & PaymentNo & "', '" & item.BillingPeriod & "', '" & item.EndingBalance & "', '" & item.NewEndingBalance & "', TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), " &
                   "TO_DATE('" & item.NewDueDate & "','MM/DD/YYYY'), '" & item.AllocationAmount & "', '" & item.PaymentType & "', " & item.PaymentCategory & ", TO_DATE('" & item.AllocationDate & "','MM/DD/YYYY'), " &
                   "'" & item.WESMBillSummaryNo & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss')," & 0 & ", " & item.EnergyWithHold & ", '" & item.IDNumber & "' FROM DUAL"
                listSQL.Add(SQL)
            End If
        Next

        'Insert into AM_PAYMENT_NEW_OFFSETTING_AP query database
        For Each item In Me._AMPaymentNewOffsettingAP
            If Not item.GeneratedDMCM = 0 Then
                Dim _DMCMNumber As Long = dicDMCM(item.GeneratedDMCM)
                'add ENERGY_WITHHOLD and ID_NUMBER for CAP SUMMARY HIstorical Purposes added by lance as of 06/05/2020
                SQL = "INSERT INTO AM_PAYMENT_NEW_OFFSETTING_AP (PAYMENT_NO, BILLING_PERIOD, ENDING_BALANCE, NEW_ENDING_BALANCE, DUE_DATE, NEW_DUEDATE, ALLOCATION_AMOUNT, PAYMENT_TYPE, PAYMENT_CATEGORY, " &
                        "ALLOCATION_DATE, WESMBILL_SUMMARY_NO, UPDATED_BY, UPDATED_DATE, AM_DMCM_NO, OFFSET_SEQ, ENERGY_WITHHOLD, ID_NUMBER) " &
                        "SELECT '" & PaymentNo & "', '" & item.BillingPeriod & "', '" & item.EndingBalance & "', '" & item.NewEndingBalance & "', " &
                        "TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), TO_DATE('" & item.NewDueDate & "','MM/DD/YYYY'), '" & item.AllocationAmount & "', '" & item.PaymentType & "', " & item.PaymentCategory & ", " &
                        "TO_DATE('" & item.AllocationDate & "','MM/DD/YYYY'), '" & item.WESMBillSummaryNo & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), " & _DMCMNumber & ", " & item.OffsettingSequence & ", " & item.EnergyWithHold & ", '" & item.IDNumber & "' FROM DUAL"
                listSQL.Add(SQL)
            Else
                'add ENERGY_WITHHOLD and ID_NUMBER for CAP SUMMARY HIstorical Purposes added by lance as of 06/05/2020
                SQL = "INSERT INTO AM_PAYMENT_NEW_OFFSETTING_AP (PAYMENT_NO, BILLING_PERIOD, ENDING_BALANCE, NEW_ENDING_BALANCE, DUE_DATE, NEW_DUEDATE, ALLOCATION_AMOUNT, PAYMENT_TYPE, PAYMENT_CATEGORY," &
                        "ALLOCATION_DATE, WESMBILL_SUMMARY_NO, UPDATED_BY, UPDATED_DATE, AM_DMCM_NO, OFFSET_SEQ, ENERGY_WITHHOLD, ID_NUMBER) " &
                        "SELECT '" & PaymentNo & "', '" & item.BillingPeriod & "', '" & item.EndingBalance & "', '" & item.NewEndingBalance & "', " &
                        "TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), TO_DATE('" & item.NewDueDate & "','MM/DD/YYYY'), '" & item.AllocationAmount & "', '" & item.PaymentType & "', " & item.PaymentCategory & ", " &
                        "TO_DATE('" & item.AllocationDate & "','MM/DD/YYYY'), '" & item.WESMBillSummaryNo & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), " & 0 & ", " & item.OffsettingSequence & ", " & item.EnergyWithHold & ", '" & item.IDNumber & "' FROM DUAL"
                listSQL.Add(SQL)
            End If
        Next

        'Insert into AM_PAYMENT_NEW_OFFSETTING_AR query database
        For Each item In Me._AMPaymentNewOffsettingAR
            If Not item.GeneratedDMCM = 0 Then
                Dim _DMCMNumber As Long = dicDMCM(item.GeneratedDMCM)
                'add ENERGY_WITHHOLD and ID_NUMBER for CAP SUMMARY HIstorical Purposes added by lance as of 06/05/2020
                SQL = "INSERT INTO AM_PAYMENT_NEW_OFFSETTING_AR (PAYMENT_NO, BILLING_PERIOD, ENDING_BALANCE, NEW_ENDING_BALANCE, DUE_DATE, NEW_DUEDATE, ALLOCATION_AMOUNT, COLLECTION_TYPE, COLLECTION_CATEGORY, " &
                        "ALLOCATION_DATE, WESMBILL_SUMMARY_NO, UPDATED_BY, UPDATED_DATE, AM_DMCM_NO, OFFSET_SEQ, ENERGY_WITHHOLD, ID_NUMBER) " &
                      "SELECT '" & PaymentNo & "', '" & item.BillingPeriod & "', '" & item.EndingBalance & "', '" & item.NewEndingBalance & "', " &
                        "TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), TO_DATE('" & item.NewDueDate & "','MM/DD/YYYY'), '" & item.AllocationAmount & "', '" & item.CollectionType & "', '" & item.CollectionCategory & "', " &
                        "TO_DATE('" & item.AllocationDate & "','MM/DD/YYYY'), '" & item.WESMBillSummaryNo & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), " & _DMCMNumber & ", " & item.OffsettingSequence & ", " & item.EnergyWithHold & ", '" & item.IDNumber & "' FROM DUAL"
                listSQL.Add(SQL)

            Else
                'add ENERGY_WITHHOLD and ID_NUMBER for CAP SUMMARY HIstorical Purposes added by lance as of 06/05/2020
                SQL = "INSERT INTO AM_PAYMENT_NEW_OFFSETTING_AR (PAYMENT_NO, BILLING_PERIOD, ENDING_BALANCE, NEW_ENDING_BALANCE, DUE_DATE, NEW_DUEDATE, ALLOCATION_AMOUNT, COLLECTION_TYPE, COLLECTION_CATEGORY, " &
                        "ALLOCATION_DATE, WESMBILL_SUMMARY_NO, UPDATED_BY, UPDATED_DATE, AM_DMCM_NO, OFFSET_SEQ, ENERGY_WITHHOLD, ID_NUMBER) " &
                      "SELECT '" & PaymentNo & "', '" & item.BillingPeriod & "', '" & item.EndingBalance & "', '" & item.NewEndingBalance & "', " &
                        "TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), TO_DATE('" & item.NewDueDate & "','MM/DD/YYYY'), '" & item.AllocationAmount & "', '" & item.CollectionType & "', '" & item.CollectionCategory & "', " &
                        "TO_DATE('" & item.AllocationDate & "','MM/DD/YYYY'), '" & item.WESMBillSummaryNo & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), " & 0 & ", " & item.OffsettingSequence & ", " & item.EnergyWithHold & ", '" & item.IDNumber & "' FROM DUAL"
                listSQL.Add(SQL)
            End If
        Next

        'WESMBillSummary        
        For Each item In Me.UpdatedWESMBillSummaryList
            SQL = "UPDATE AM_WESM_BILL_SUMMARY SET ENDING_BALANCE = '" & item.EndingBalance & "', NEW_DUEDATE = TO_DATE('" & item.NewDueDate & "','MM/DD/YYYY'), " &
                                                  "UPDATED_DATE = TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), UPDATED_BY = '" & UpdatedBy & "', " &
                                                  "TRANSACTION_DATE = TO_DATE('" & Me.AllocationDate.CollAllocationDate.ToShortDateString & "','MM/DD/YYYY'), " &
                                                  "ENERGY_WITHHOLD_STATUS = " & item.EnergyWithholdStatus & ",  " &
                                                  "ENERGY_WITHHOLD = " & item.EnergyWithhold & " " &
                                                  "WHERE WESMBILL_SUMMARY_NO = '" & item.WESMBillSummaryNo & "'"
            listSQL.Add(SQL)
        Next


        'WESMBillSummaryHistory            
        Dim ForWESMBillSummaryHistoryAP = (From x In Me.AMPaymentNewAP Select x).Union _
                                          (From x In Me.AMPaymentNewOffsettingAP Select x).ToList()

        Dim SumOfForWESMBillSummaryHistoryAP = (From x In ForWESMBillSummaryHistoryAP
                                                Group By WESMBillSummaryNo = x.WESMBillSummaryNo, DueDate = x.NewDueDate,
                                                PaymentType = x.PaymentType Into AllocationAmount = Sum(x.AllocationAmount)).ToList()

        Dim SumOfForWESMBillSummaryHistoryAR = (From x In Me.AMPaymentNewOffsettingAR
                                                Group By WESMBillSummaryNo = x.WESMBillSummaryNo, DueDate = x.NewDueDate,
                                                CollectionType = x.CollectionType Into AllocationAmount = Sum(x.AllocationAmount)).ToList()

        For Each item In SumOfForWESMBillSummaryHistoryAP
            SQL = "INSERT INTO AM_WESM_BILL_SUMMARY_HISTORY (WESMBILL_SUMMARY_NO, DUE_DATE, AMOUNT, UPDATED_BY, UPDATED_DATE, PAYMENT_TYPE, PAYMENT_NO) " &
                    "SELECT '" & item.WESMBillSummaryNo & "', TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), '" & item.AllocationAmount & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & item.PaymentType & "', '" & PaymentNo & "' FROM DUAL"
            listSQL.Add(SQL)
        Next

        For Each item In SumOfForWESMBillSummaryHistoryAR
            SQL = "INSERT INTO AM_WESM_BILL_SUMMARY_HISTORY (WESMBILL_SUMMARY_NO, DUE_DATE, AMOUNT, UPDATED_BY, UPDATED_DATE,COLLECTION_TYPE, PAYMENT_NO) " &
                  "SELECT '" & item.WESMBillSummaryNo & "', TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), '" & item.AllocationAmount & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & item.CollectionType & "', '" & PaymentNo & "' FROM DUAL"
            listSQL.Add(SQL)
        Next

        Dim GetDMCMForWESMBillSummaryWHTaxAdj = (From x In Me.PaymentDMCMList
                                                 Where x.TransType = EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAROnEnergyAddBack _
                                                 Or x.TransType = EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAPOnEnergyAddBack Select x).ToList()

        For Each item In UpdatedWESMBillSummaryWHTAXAdjList
            Dim getDMCM = (From x In GetDMCMForWESMBillSummaryWHTaxAdj Where x.Particulars.Contains(item.INVDMCMNo) Select x).FirstOrDefault
            If Not getDMCM Is Nothing Then
                If item.BalanceType = EnumBalanceType.AR Then
                    SQL = "INSERT INTO AM_WESM_BILL_SUMMARY_HISTORY (WESMBILL_SUMMARY_NO, DUE_DATE, AMOUNT, UPDATED_BY, UPDATED_DATE,COLLECTION_TYPE, PAYMENT_NO) " &
                        "VALUES ('" & item.WESMBillSummaryNo & "', TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), '" & getDMCM.TotalAmountDue & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & EnumCollectionType.EnergyAREndingBalanceAdjustment & "', '" & PaymentNo & "')"
                    listSQL.Add(SQL)
                Else
                    SQL = "INSERT INTO AM_WESM_BILL_SUMMARY_HISTORY (WESMBILL_SUMMARY_NO, DUE_DATE, AMOUNT, UPDATED_BY, UPDATED_DATE,PAYMENT_TYPE, PAYMENT_NO) " &
                        "VALUES ('" & item.WESMBillSummaryNo & "', TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), '" & getDMCM.TotalAmountDue * -1 & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & EnumPaymentNewType.EnergyAPEndingBalanceAdjustment & "', '" & PaymentNo & "')"
                    listSQL.Add(SQL)
                End If
            End If
        Next

        'AM_FTF_MAIN        
        Dim ReplenishmentRefNo As Long = 0
        For Each item In Me.PaymentFTF

            Dim JVBatchCode As String = ""
            Dim ReferenceNo As Long = objWBillHelper.GetSequenceID("SEQ_AM_FTF_REF_NO")

            item.RefNo = ReferenceNo
            JVBatchCode = EnumPostedType.PEFT.ToString & "-" & CStr(dicJVBatchNo(EnumPostedType.PEFT.ToString))
            ReplenishmentRefNo = ReferenceNo

            SQL = "INSERT INTO AM_FTF_MAIN (ALLOCATION_DATE, BATCH_CODE, REF_NO, DR_DATE, CR_DATE, TOTAL_AMOUNT, " &
                                                        "TRANS_TYPE, STATUS, IS_POSTED, REQUESTING_APPROVAL, APPROVED_BY, UPDATED_BY, UPDATED_DATE) " &
                  "SELECT TO_DATE('" & item.AllocationDate.ToShortDateString & "','MM/DD/YYYY'), '" & JVBatchCode & "', '" & ReferenceNo & "', " &
                                "TO_DATE('" & item.DRDate.ToString("MM/dd/yyyy") & "','MM/DD/YYYY'), TO_DATE('" & item.CRDate.ToString("MM/dd/yyyy") & "','MM/DD/YYYY'), '" & item.TotalAmount & "', " &
                                "'" & item.TransType & "', '" & item.Status & "', '" & item.IsPosted & "', '" & item.RequestingApproval & "', '" & item.ApprovedBy & "', " &
                                "'" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') FROM DUAL"
            listSQL.Add(SQL)

            For Each ItemFTF In item.ListOfFTFDetails
                SQL = "INSERT INTO AM_FTF_DETAILS (REF_NO, ACCT_CODE, BANK_ACCNT_NO, ISSUING_BANK, RECEIVING_BANK, DEBIT, CREDIT, PARTICULARS, UPDATED_BY, UPDATED_DATE) " &
                      "SELECT '" & ReferenceNo & "', '" & ItemFTF.AccountCode & "', '" & ItemFTF.BankAccountNo & "', '" & ItemFTF.IssuingBank & "', '" & ItemFTF.ReceivingBank & "', " &
                             "'" & ItemFTF.Debit & "', '" & ItemFTF.Credit & "', '" & ItemFTF.Particulars & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') FROM DUAL"
                listSQL.Add(SQL)
            Next

            For Each ItemParticipants In item.ListOfFTFParticipants
                SQL = "INSERT INTO AM_FTF_PARTICIPANT (REF_NO, ID_NUMBER, AMOUNT, UPDATED_BY, UPDATED_DATE) " &
                      "SELECT '" & ReferenceNo & "', '" & ItemParticipants.IDNumber.IDNumber & "', " &
                              "'" & ItemParticipants.Amount & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') FROM DUAL"
                listSQL.Add(SQL)
            Next
        Next

        'AM_FTF_MAIN COLLECTION
        For Each item In Me.CollectionFTF
            If item.TransType = EnumFTFTransType.DrawDown Or item.TransType = EnumFTFTransType.Replenishment Or
                item.TransType = EnumFTFTransType.TransferMarketFeesToPEMC Then
                SQL = "UPDATE AM_FTF_MAIN SET DR_DATE = TO_DATE('" & Me.AllocationDate.RemittanceDate.ToShortDateString & "','MM/DD/YYYY'), CR_DATE = TO_DATE('" & Me.AllocationDate.RemittanceDate.ToShortDateString & "','MM/DD/YYYY')  WHERE REF_NO = " & item.RefNo
                listSQL.Add(SQL)
            End If
        Next

        'AM_DEFERRED_PAYMENT_MAIN
        For Each item In Me.NewPaymentDeferred
            Dim DeferredPaymentNo As Long = objWBillHelper.GetSequenceID("SEQ_AM_DEF_PYMNT_NO")
            If Not item.DMCMNumber = 0 Then
                Dim DmcmNoX As Long = dicDMCM(item.DMCMNumber)
                SQL = "INSERT INTO AM_DEFERRED_PAYMENT_MAIN (ALLOCATION_DATE, DEFERRED_PAYMENT_NO, ID_NUMBER, OUTSTANDING_DEFERRED_PAYMENT, " &
                                                                            "PAYMENT_NO, DEFERRED_AMOUNT, DEFERRED_TYPE, CHARGE_TYPE, AM_DMCM_NO, REMARKS, ORIG_DATE) " &
                      "SELECT TO_DATE('" & item.AllocationDate & "','MM/DD/YYYY'), '" & DeferredPaymentNo & "', '" & item.IDNumber & "', " &
                             "'" & item.OutstandingBalanceDeferredPayment & "', '" & PaymentNo & "', '" & item.DeferredAmount & "', '" & item.DeferredType & "', " &
                             "'" & item.ChargeType.ToString & "', '" & DmcmNoX & "', '" & item.Remarks & "',TO_DATE('" & item.OriginalDate.ToShortDateString & "','mm/dd/yyyy') FROM DUAL"
                listSQL.Add(SQL)
            Else
                SQL = "INSERT INTO AM_DEFERRED_PAYMENT_MAIN (ALLOCATION_DATE, DEFERRED_PAYMENT_NO, ID_NUMBER, OUTSTANDING_DEFERRED_PAYMENT, " &
                                                                          "PAYMENT_NO, DEFERRED_AMOUNT, DEFERRED_TYPE, CHARGE_TYPE, AM_DMCM_NO, REMARKS, ORIG_DATE) " &
                      "SELECT TO_DATE('" & item.AllocationDate & "','MM/DD/YYYY'), '" & DeferredPaymentNo & "', '" & item.IDNumber & "', " &
                      "'" & item.OutstandingBalanceDeferredPayment & "', '" & PaymentNo & "', '" & item.DeferredAmount & "', '" & item.DeferredType & "', " &
                      "'" & item.ChargeType.ToString & "', '" & 0 & "', '" & item.Remarks & "',TO_DATE('" & item.OriginalDate.ToShortDateString & "','mm/dd/yyyy') FROM DUAL"
                listSQL.Add(SQL)
            End If
        Next

        'EFT
        For Each item In PaymentEFT
            SQL = "INSERT INTO AM_PAYMENT_NEW_EFT (PAYMENT_NO, ALLOCATION_DATE, ID_NUMBER, PAYMENT_TYPE, CHECK_NO, EXCESS_COLLECTION, DEFERRED_ENERGY, " &
                                                                "DEFERRED_VAT, ENERGY, VAT, MF, RETURN_AMOUNT, TRANSFER_TO_PRUDENTIAL, INTEREST_NSS, INTEREST_STL, UPDATED_BY, UPDATED_DATE, TRANSFER_TO_FINPEN, OFFSET_DEFERRED_ENERGY, OFFSET_DEFERRED_VAT) " &
                  "SELECT " & PaymentNo & " ,TO_DATE('" & item.AllocationDate & "','MM/DD/YYYY'), '" & item.Participant.IDNumber & "', '" & item.PaymentType & "', " &
                        "'" & item.CheckNumber & "', '" & item.ExcessCollection & "', '" & item.DeferredEnergy & "', '" & item.DeferredVAT & "', '" & item.Energy & "', '" & item.VAT & "', " &
                        "'" & item.MarketFees & "', '" & item.ReturnAmount & "', '" & item.TransferPrudential & "', '" & item.NSSInterest & "', '" & item.STLInterest & "', " &
                        "'" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), " & item.TransferFinPen.ToString & ", " & item.OffsetOnDeferredEnergy.ToString & ", " & item.OffsetOnDeferredVAT.ToString & " FROM DUAL"
            listSQL.Add(SQL)
        Next

        'AM_PAYMENT_SHARE
        For Each item In AMPaymentShare
            PaymentShareNo = objWBillHelper.GetSequenceID("SEQ_AM_PAY_SHARE_NO")
            SQL = "INSERT INTO AM_PAYMENT_NEW_SHARE (PAYMENT_SHARE_NO, WESMBILLSUMMARY_NO, BILLING_PERIOD, ID_NUMBER, INV_NUMBER, AMOUNT_SHARE, AMOUNT_OFFSET, " &
                                                                  "AMOUNT_BALANCE, CHARGE_TYPE, OFFSETTING_SEQ_NO, PAYMENT_NO, PAYMENT_TYPE) " &
                  "SELECT '" & PaymentShareNo & "','" & item.WESMBillSummaryNo & "', '" & item.BillingPeriod & "', '" & item.IDNumber & "', '" & item.InvoiceNumber & "', " &
                  "'" & item.AmountShare & "', '" & 0 & "', '" & item.AmountBalance & "', '" & item.ChargeType.ToString & "', '" & item.OffsettingSequence & "', '" & PaymentNo & "', '" & item.PaymentType & "' FROM DUAL"
            listSQL.Add(SQL)
            For Each itemPaymentShare In item.AmountOffset
                SQL = "INSERT INTO AM_PAYMENT_NEW_SHARE_DETAILS (PAYMENT_SHARE_NO, WESMBILL_SUMMARY_NO, BILLING_PERIOD, ID_NUMBER, INV_NUMBER, AMOUNT_OFFSET, CHARGE_TYPE, OFFSETTING_SEQ_NO) " &
                      "SELECT '" & PaymentShareNo & "', '" & itemPaymentShare.ForWESMBillSummaryNo & "', '" & itemPaymentShare.BillingPeriod & "', '" & itemPaymentShare.IDNumber & "', '" & itemPaymentShare.InvoiceNumber & "', " &
                             "'" & itemPaymentShare.OffsetAmount & "', '" & itemPaymentShare.ChargeType.ToString & "', '" & itemPaymentShare.BatchGroupSequence & "' FROM DUAL"
                listSQL.Add(SQL)
            Next
        Next

        'AM_RFP
        With RFPList
            Dim RFPRefNo As Long = objWBillHelper.GetSequenceID("SEQ_AM_RFP_NO")
            SQL = "INSERT INTO AM_RFP (ALLOCATION_DATE, REFERENCE_NO, PAYMENT_DATE, RFP_TO, RFP_FROM, RFP_PURPOSE, PREPARED_BY, REVIEWED_BY, APPROVED_BY, " _
                                    & "UPDATED_DATE, UPDATED_BY, NSS_APPLIED, PR_REPLENISHMENT, MF_APPLIED, TRANSFER_PEMC, HELD_COLLECTION, PAYMENT_NO) " _
                & "SELECT TO_DATE('" & .AllocationDate & "','MM/DD/YYYY'), " & RFPRefNo & ", TO_DATE('" & .PaymentDate & "','MM/DD/YYYY'), '" _
                        & .toRFP & "', '" & .FromRFP & "', '" & .PurposeOfPayment & "', '" & .PreparedBy & "', '" & .ReviewedBy & "', '" & .ApprovedBy & "', " _
                        & "TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & .UpdatedBy & "', " & .NSSAmount & ", " _
                        & .PRReplenishment & ", " & .MarketFees & ", " & .TransferToPEMC & ", " & .HeldCollection & ", " & PaymentNo & " FROM DUAL"

            listSQL.Add(SQL)

            For Each item In RFPList.RFPDetails
                SQL = "INSERT INTO AM_RFP_DETAILS (REFERENCE_NO, ID_NUMBER, BANK_BRANCH, ACCOUNT_NO, RFP_PAYMENT_TYPE, AMOUNT, DATE_OF_DEPOSIT, PARTICULARS, ALLOCATION_DATE, RFP_DETAILS_TYPE) " _
                    & "SELECT " & RFPRefNo & ", '" & item.Participant & "', '" & item.BankBranch & "', '" & item.AccountNo & "', '" & item.PaymentType & "', '" & item.Amount & "', " _
                                & "TO_DATE('" & item.DateOfDeposit & "', 'MM/DD/YYYY'), '" & item.Particulars & "', TO_DATE('" & item.AllocationDate & "', 'MM/DD/YYYY'), " & item.RFPDetailsType & " FROM DUAL"
                listSQL.Add(SQL)
            Next
        End With

        'AM_PAYMENT_NEW_WBSHISTORY_BAL
        For Each item In Me.WESMBillSummaryBalance
            SQL = "INSERT INTO AM_PAYMENT_NEW_WBS_BALANCE (PAYMENT_NO, BILLING_PERIOD, ORIG_DUE_DATE, BILLING_PERIOD_REMARKS, TOTAL_BILL, ID_NUMBER, AMOUNT_BALANCE, CHARGE_TYPE, BALANCE_TYPE, WESMBILL_BATCH_NO) " _
                & "SELECT " & PaymentNo & ", " & item.BillingPeriod & ", TO_DATE('" & item.OriginalDueDate & "', 'MM/DD/YYYY'), '" & item.BillingPeriodRemarks & "', '" & item.TotalBillAmount & "', '" & item.IDNumber.IDNumber _
                            & "', " & item.AmountBalance & ", '" & item.ChargeType.ToString & "', '" & item.BalanceType.ToString & "', " & item.WESMBillBatchNo & " FROM DUAL"
            listSQL.Add(SQL)
        Next

        'AM_PRUDENTIAL_HISTORY
        For Each item In Me.PrudentialHistoryList
            Dim JVBatchCode As String = EnumPostedType.PEFT.ToString & "-" & CStr(dicJVBatchNo(EnumPostedType.PEFT.ToString))
            SQL = "UPDATE AM_PRUDENTIAL SET PRUDENTIAL_AMOUNT = PRUDENTIAL_AMOUNT + " & item.Amount &
                 ",UPDATED_BY = '" & AMModule.UserName & "',UPDATED_DATE = " & "TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') " &
                 "WHERE ID_NUMBER = '" & item.IDNumber.IDNumber & "'"
            listSQL.Add(SQL)

            SQL = "INSERT INTO AM_PRUDENTIAL_HISTORY (ID_NUMBER, AMOUNT, TRANS_TYPE, TRANS_DATE, UPDATED_BY, UPDATED_DATE, BATCH_CODE, FTF_REF_NO) " &
                  "SELECT '" & item.IDNumber.IDNumber & "', " & item.Amount & ", " & item.TransType & ", " & "TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') " &
                            ", '" & AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & JVBatchCode &
                            "', " & ReplenishmentRefNo & " FROM DUAL"
            listSQL.Add(SQL)
        Next

        'Declared dictionary variables to get the OR FINPEN
        Dim dicORFinPen As New Dictionary(Of String, Long)

        'AM_OFFICIAL_RECEIPT_MAIN
        For Each item In Me.ORList

            Dim ORNo As Long = objWBillHelper.GetSequenceID("SEQ_AM_OR_NO")
            Dim BatchCode As String = EnumPostedType.PA.ToString & "-" & dicJVBatchNo(EnumPostedType.PA.ToString).ToString

            If item.TransactionType = EnumORTransactionType.FinPenAmount Then
                If Not dicORFinPen.ContainsKey(item.IDNumber) Then
                    dicORFinPen.Add(item.IDNumber, ORNo)
                End If
            End If
            SQL = "INSERT INTO AM_OFFICIAL_RECEIPT_MAIN (OR_NO,OR_DATE,ID_NUMBER,OR_AMOUNT,STATUS,UPDATED_BY,UPDATED_DATE, BATCH_CODE,REMARKS,TRANS_TYPE, " &
                                                        "VAT_EXEMPT,VATABLE,VAT,VAT_ZERO_RATED,OTHERS,WITHHOLDING_TAX,WITHHOLDING_VAT) " &
                  "SELECT " & ORNo & ", TO_DATE('" & item.ORDate & "', 'MM/DD/YYYY'), '" & item.IDNumber & "', " & item.Amount & ", " & item.Status & ", '" &
                            AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & BatchCode &
                            "', '" & item.Remarks & "', " & item.TransactionType & ", " & item.VATExempt & ", " & item.Vatable & ", " & item.VAT & ", " & item.VATZeroRated &
                            ", " & item.Others & ", " & item.WithholdingTax & ", " & item.WithholdingVAT & " FROM DUAL"

            listSQL.Add(SQL)

            For Each detail In item.ListORDetails
                SQL = "INSERT INTO AM_OFFICIAL_RECEIPT_DETAILS (OR_NO, ACCT_CODE, DESCRIPTION, DEBIT, CREDIT, UPDATED_BY, UPDATED_DATE) " &
                      "SELECT " & ORNo & ", '" & detail.AccountCode & "', '" & detail.Description & "', " & detail.Debit & ", " & detail.Credit &
                               ", '" & AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') FROM DUAL"
                listSQL.Add(SQL)
            Next

            For Each summary In item.ListORSummary
                SQL = "INSERT INTO AM_OFFICIAL_RECEIPT_SUMMARY (OR_NO, WESMBILL_SUMMARY_NO, DUE_DATE, AMOUNT, COLLECTION_TYPE, UPDATED_BY, UPDATED_DATE) " &
                      "SELECT " & ORNo & ", " & summary.WESMBillSummaryNo & ", TO_DATE('" & summary.DueDate & "', 'MM/DD/YYYY'), " & summary.Amount & ", " &
                                summary.CollectionType & ", '" & AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') FROM DUAL"
                listSQL.Add(SQL)
            Next
        Next

        'AM_PAYMENT_NEW_TRANSFER_TO_PR
        For Each item In Me._AMPaymentTransferToPR
            Dim getFinPenOrPerMP As Long = (From x In Me.ORList Where x.TransactionType = EnumORTransactionType.FinPenAmount And x.IDNumber = item.IDNumber Select x.ORNo).FirstOrDefault
            If dicORFinPen.ContainsKey(item.IDNumber) Then
                SQL = "INSERT INTO AM_PAYMENT_NEW_TRANSFER_TO_PR (PAYMENT_NO, ID_NUMBER, PAYMENT_EXCESS_COLLECTION, PAYMENT_DEFERRED_ENERGY, PAYMENT_DEFERRED_VATONENERGY, PAYMENT_MF, PAYMENT_ENERGY, " &
                                                             "PAYMENT_VATONENERGY, PAYMENT_TOTAL, FULLY_TRANSFER_TO_PR, TRANSFER_TO_PR, TOTAL_AMOUNT_FOR_REMITTANCE, FULLY_TRANSFER_TO_FINPEN, TRANSFER_TO_FINPEN, FINPEN_OR_NO, OFFSET_DEFERRED_ENERGY, OFFSET_DEFERRED_VAT) " &
                    "SELECT '" & PaymentNo & "','" & item.IDNumber & "','" & item.PaymentOnExcessCollection & "', '" & item.PaymentOnDeferredonEnergy & "', '" & item.PaymentOnDeferredonVATonEnergy & "', '" & item.PaymentOnMFWithVAT &
                             "','" & item.PaymentOnEnergy & "', '" & item.PaymentOnVATonEnergy & "','" & item.TotalPaymentAllocated & "','" & IIf(item.FullyTransferToPR, 1, 0) & "','" & item.TransferToPrudential & "','" & item.TotalAmountForRemittance &
                             "','" & IIf(item.FullyTransferToFinPen, 1, 0) & "'," & item.TransferToFinPen.ToString & ", " & dicORFinPen.Item(item.IDNumber).ToString & ", " & item.OffsetOnOffsetDeferredonEnergy & ", " & item.OffsetOnOffsetDeferredonVATonEnergy & " FROM DUAL"
            Else
                SQL = "INSERT INTO AM_PAYMENT_NEW_TRANSFER_TO_PR (PAYMENT_NO, ID_NUMBER, PAYMENT_EXCESS_COLLECTION, PAYMENT_DEFERRED_ENERGY, PAYMENT_DEFERRED_VATONENERGY, PAYMENT_MF, PAYMENT_ENERGY, " &
                                                             "PAYMENT_VATONENERGY, PAYMENT_TOTAL, FULLY_TRANSFER_TO_PR, TRANSFER_TO_PR, TOTAL_AMOUNT_FOR_REMITTANCE, FULLY_TRANSFER_TO_FINPEN, TRANSFER_TO_FINPEN, OFFSET_DEFERRED_ENERGY, OFFSET_DEFERRED_VAT) " &
                    "SELECT '" & PaymentNo & "','" & item.IDNumber & "','" & item.PaymentOnExcessCollection & "', '" & item.PaymentOnDeferredonEnergy & "', '" & item.PaymentOnDeferredonVATonEnergy & "', '" & item.PaymentOnMFWithVAT &
                             "','" & item.PaymentOnEnergy & "', '" & item.PaymentOnVATonEnergy & "','" & item.TotalPaymentAllocated & "','" & IIf(item.FullyTransferToPR, 1, 0) & "','" & item.TransferToPrudential & "','" & item.TotalAmountForRemittance &
                             "','" & IIf(item.FullyTransferToFinPen, 1, 0) & "'," & item.TransferToFinPen.ToString & ", " & item.OffsetOnOffsetDeferredonEnergy & ", " & item.OffsetOnOffsetDeferredonVATonEnergy & " FROM DUAL"
            End If
            listSQL.Add(SQL)
        Next

        For Each item In Me.WESMBillSummaryNoTransAR
            'add ENERGY_WITHHOLD and ID_NUMBER for CAP SUMMARY HIstorical Purposes added by lance as of 06/05/2020
            SQL = "INSERT INTO AM_PAYMENT_NEW_WBS_NOTRANS ( PAYMENT_NO, BILLING_PERIOD, ENDING_BALANCE, NEW_ENDING_BALANCE, ALLOCATION_AMOUNT, DUE_DATE, NEW_DUEDATE, WESMBILL_SUMMARY_NO, ENERGY_WITHHOLD, ID_NUMBER) " _
                & "SELECT " & Me.PaymentNumber & ", " & item.BillingPeriod & ", '" & item.EndingBalance & "', '" & item.NewEndingBalance & "', '0', TO_DATE('" & item.DueDate.ToShortDateString & "','mm/dd/yyyy'), " _
                         & "TO_DATE('" & item.NewDueDate.ToShortDateString & "','mm/dd/yyyy'), " & item.WESMBillSummaryNo & ", " & item.EnergyWithHold & ", '" & item.IDNumber & "' FROM DUAL"
            listSQL.Add(SQL)
        Next

        For Each item In Me.WESMBillSummaryNoTransAP
            'add ENERGY_WITHHOLD and ID_NUMBER for CAP SUMMARY HIstorical Purposes added by lance as of 06/05/2020
            SQL = "INSERT INTO AM_PAYMENT_NEW_WBS_NOTRANS ( PAYMENT_NO, BILLING_PERIOD, ENDING_BALANCE, NEW_ENDING_BALANCE, ALLOCATION_AMOUNT, DUE_DATE, NEW_DUEDATE, WESMBILL_SUMMARY_NO, ENERGY_WITHHOLD, ID_NUMBER) " _
                & "SELECT " & Me.PaymentNumber & ", " & item.BillingPeriod & ", '" & item.EndingBalance & "', '" & item.NewEndingBalance & "', '0', TO_DATE('" & item.DueDate.ToShortDateString & "','mm/dd/yyyy'), " _
                         & "TO_DATE('" & item.NewDueDate.ToShortDateString & "','mm/dd/yyyy'), " & item.WESMBillSummaryNo & ", " & item.EnergyWithHold & ", '" & item.IDNumber & "' FROM DUAL"
            listSQL.Add(SQL)
        Next

        For Each item In Me.WESMTransDetailsSummaryList
            If item.Status.Equals(EnumWESMTransDetailsSummaryStatus.ADDED.ToString) Then
                SQL = "INSERT INTO AM_WESM_TRANS_DETAILS_SUMMARY(BUYER_TRANS_NO,BUYER_BILLING_ID,SELLER_TRANS_NO,SELLER_BILLING_ID,DUE_DATE,NEW_DUE_DATE,ORIG_AMOUNT_ENERGY,OBIN_ENERGY," & vbNewLine _
                                            & "ORIG_AMOUNT_VAT,OBIN_VAT,ORIG_AMOUNT_EWT,OBIN_EWT,UPDATED_DATE,UPDATED_BY)" & vbNewLine _
                      & "SELECT '" & item.BuyerTransNo & "', '" & item.BuyerBillingID & "', '" & item.SellerTransNo & "', '" & item.SellerBillingID & "', TO_DATE('" & item.DueDate.ToShortDateString & "','MM/DD/yyyy')," & vbNewLine _
                      & "TO_DATE('" & item.NewDueDate.ToShortDateString & "','MM/DD/yyyy')," & item.OrigBalanceInEnergy & "," & item.OutstandingBalanceInEnergy & "," & item.OrigBalanceInVAT & vbNewLine _
                      & "," & item.OutstandingBalanceInVAT & "," & item.OrigBalanceInEWT & "," & item.OutstandingBalanceInEWT & ",SYSDATE,'" & AMModule.UserName & "' FROM DUAL"
                listSQL.Add(SQL)
            ElseIf item.Status.Equals(EnumWESMTransDetailsSummaryStatus.UPDATED.ToString) Then
                SQL = "UPDATE AM_WESM_TRANS_DETAILS_SUMMARY SET OBIN_ENERGY = " & item.OutstandingBalanceInEnergy & ",OBIN_VAT = " & item.OutstandingBalanceInVAT & vbNewLine _
                    & " WHERE BUYER_TRANS_NO = '" & item.BuyerTransNo & "' AND SELLER_TRANS_NO = '" & item.SellerTransNo & "'"
                listSQL.Add(SQL)
            End If
        Next

        For Each item In Me.WESMTransDetailsSummaryHistoryList
            SQL = "INSERT INTO AM_WESM_TRANS_DETAILS_SUMMARY_HISTORY(BUYER_TRANS_NO,BUYER_BILLING_ID,SELLER_TRANS_NO,SELLER_BILLING_ID,DUE_DATE,ALLOCATION_DATE,REMITTANCE_DATE,ALLOCATED_IN_ENERGY,ALLOCATED_IN_DEFINT,ALLOCATED_IN_VAT," & vbNewLine _
                                            & "PROCESSED_DATE,PROCESED_BY)" & vbNewLine _
                      & "SELECT '" & item.BuyerTransNo & "', '" & item.BuyerBillingID & "', '" & item.SellerTransNo & "', '" & item.SellerBillingID & "', TO_DATE('" & item.DueDate.ToShortDateString & "','MM/DD/yyyy')" & vbNewLine _
                      & ",TO_DATE('" & Me.AllocationDate.CollAllocationDate.ToShortDateString & "','MM/DD/yyyy')" & ",TO_DATE('" & Me.AllocationDate.RemittanceDate.ToShortDateString & "','MM/DD/yyyy')," & vbNewLine _
                      & item.AllocatedInEnergy & "," & item.AllocatedInDefInt & "," & item.AllocatedInVAT & vbNewLine _
                      & ",SYSDATE,'" & AMModule.UserName & "' FROM DUAL"
            listSQL.Add(SQL)
        Next

        If Me.AllocationDate.CollAllocationDate.Month = Me.AllocationDate.RemittanceDate.Month Then
            'Delete AM_STL_NOTICE_NEW
            SQL = "DELETE FROM AM_STL_NOTICE_NEW WHERE STL_NOTICE_DATE = LAST_DAY(TO_DATE('" & FormatDateTime(Me.AllocationDate.CollAllocationDate, DateFormat.ShortDate) & "','mm/dd/yyyy'))"
            listSQL.Add(SQL)

            'Insert into AM_STL_NOTICE_NEW
            SQL = "INSERT INTO AM_STL_NOTICE_NEW(STL_NOTICE_DATE, ENDING_BALANCE, WESMBILL_SUMMARY_NO) " &
                  "(SELECT LAST_DAY(TO_DATE('" & FormatDateTime(Me.AllocationDate.CollAllocationDate, DateFormat.ShortDate) & "','mm/dd/yyyy')) AS TRANSACTIONDATE, ENDING_BALANCE, WESMBILL_SUMMARY_NO " &
                  " FROM AM_WESM_BILL_SUMMARY WHERE ENDING_BALANCE <> 0 OR TO_CHAR(NEW_DUEDATE,'MM/YYYY') = TO_CHAR(TO_DATE('" & FormatDateTime(Me.AllocationDate.CollAllocationDate, DateFormat.ShortDate) & "','MM/DD/YYYY'),'MM/YYYY'))"

            listSQL.Add(SQL)
        Else
            Dim distinctWESMBillSummaryNo As List(Of Long) = (From x In Me.AMCollectionAR Select x.WESMBillSummaryNo).Distinct.ToList()
            For Each item In distinctWESMBillSummaryNo
                'UPDATE
                SQL = "UPDATE AM_STL_NOTICE_NEW A " &
                      "SET A.STL_NOTICE_DATE = LAST_DAY(TO_DATE('" & FormatDateTime(Me.AllocationDate.CollAllocationDate, DateFormat.ShortDate) & "','mm/dd/yyyy')), " &
                          "A.ENDING_BALANCE = (SELECT B.ENDING_BALANCE FROM AM_WESM_BILL_SUMMARY B WHERE B.WESMBILL_SUMMARY_NO = A.WESMBILL_SUMMARY_NO) " &
                      "WHERE A.WESMBILL_SUMMARY_NO = " & item & " AND A.STL_NOTICE_DATE = LAST_DAY(TO_DATE('" & FormatDateTime(Me.AllocationDate.CollAllocationDate, DateFormat.ShortDate) & "','mm/dd/yyyy'))"
                listSQL.Add(SQL)
            Next

            'Insert into AM_STL_NOTICE_NEW
            SQL = "INSERT INTO AM_STL_NOTICE_NEW(STL_NOTICE_DATE, ENDING_BALANCE, WESMBILL_SUMMARY_NO) " &
                  "(SELECT LAST_DAY(TO_DATE('" & FormatDateTime(Me.AllocationDate.RemittanceDate, DateFormat.ShortDate) & "','mm/dd/yyyy')) AS TRANSACTIONDATE, ENDING_BALANCE, WESMBILL_SUMMARY_NO " &
                  " FROM AM_WESM_BILL_SUMMARY WHERE ENDING_BALANCE <> 0 OR TO_CHAR(NEW_DUEDATE,'MM/YYYY') = TO_CHAR(TO_DATE('" & FormatDateTime(Me.AllocationDate.CollAllocationDate, DateFormat.ShortDate) & "','MM/DD/YYYY'),'MM/YYYY'))"

            listSQL.Add(SQL)

        End If

        dicDMCM = Nothing
        dicJVNo = Nothing
        dicJVBatchNo = Nothing

        Return listSQL

    End Function

    Private Function CreateSQLStatement() As List(Of String)
        Dim listSQL As New List(Of String)
        Dim SQL As String
        Dim SysDateTime As Date = objWBillHelper.GetSystemDateTime()
        Dim UpdatedBy As String = AMModule.UserName
        Dim PaymentNo As Long
        Dim PaymentShareNo As Long

        Dim dicDMCM As New Dictionary(Of Long, Long)
        Dim dicJVNo As New Dictionary(Of String, Long)
        Dim dicJVBatchNo As New Dictionary(Of String, Long)
        Dim ListofSequenceUsed As New List(Of String)

        ListofSequenceUsed.Add("SEQ_AM_PAY_NO")
        ListofSequenceUsed.Add("SEQ_AM_BATCH_CODE")
        ListofSequenceUsed.Add("SEQ_AM_JV_NO")
        ListofSequenceUsed.Add("SEQ_AM_DMCM_NO")
        ListofSequenceUsed.Add("SEQ_AM_FTF_REF_NO")
        ListofSequenceUsed.Add("SEQ_AM_DEF_PYMNT_NO")
        ListofSequenceUsed.Add("SEQ_AM_PAY_SHARE_NO")
        ListofSequenceUsed.Add("SEQ_AM_RFP_NO")
        ListofSequenceUsed.Add("SEQ_AM_OR_NO")

        dicListofSeq = New Dictionary(Of String, Long)
        dicListofSeq = objWBillHelper.GetMaxSequenceID(ListofSequenceUsed)
        PaymentNo = dicListofSeq("SEQ_AM_PAY_NO")
        dicListofSeq("SEQ_AM_PAY_NO") += 1
        Me.PaymentNumber = PaymentNo

        'Updating AM_COLLECTION
        SQL = "UPDATE AM_COLLECTION SET IS_ALLOCATED = " & EnumIsAllocated.Allocated _
            & " WHERE ALLOCATION_DATE = TO_DATE('" & Me.AllocationDate.CollAllocationDate.ToShortDateString & "', 'MM/dd/yyyy')"
        listSQL.Add(SQL)

        'Insert AM_JV, AM_JV_DETAILS, AM_CHECKS, AM_WESM_BILL_GP_POSTED        
        Dim ListofJVNumber As New List(Of Long)
        For Each item In Me.JournalVoucherList

            Dim BatchCode As Long = dicListofSeq.Item("SEQ_AM_BATCH_CODE")
            Dim JVNumber As Long = dicListofSeq.Item("SEQ_AM_JV_NO")
            Dim ItemPostedType As EnumPostedType = CType([Enum].Parse(GetType(EnumPostedType), CStr(item.PostedType)), EnumPostedType)

            If Not dicJVNo.ContainsKey(CStr(item.PostedType)) Then
                dicJVNo.Add(CStr(item.PostedType), JVNumber)
            End If
            If Not dicJVBatchNo.ContainsKey(CStr(item.PostedType)) Then
                dicJVBatchNo.Add(CStr(item.PostedType), BatchCode)
            End If

            ListofJVNumber.Add(JVNumber)
            With item
                If ItemPostedType = EnumPostedType.PEFT Then
                    For Each Checkitem In RFPList.RFPDetails.Where(Function(x) x.PaymentType = EnumParticipantPaymentType.Check And x.RFPDetailsType = EnumRFPDetailsType.Payment)
                        With Checkitem
                            SQL = "INSERT INTO AM_CHECKS (TRANSACTION_DATE, ID_NUMBER, AMOUNT, CHECK_NUMBER, CHECK_TYPE, CV_NUMBER, UPDATED_BY, STATUS, REMARKS, BATCH_CODE) " &
                                  "SELECT TO_DATE('" & .AllocationDate & "','MM/DD/YYYY'),'" &
                                            .Participant & "'," & .Amount & ", '', '', '', '" &
                                            AMModule.UserName & "'," & "1, '" & .Particulars & "', '" &
                                            ItemPostedType.ToString & "-" & BatchCode & "' FROM DUAL"
                            listSQL.Add(SQL)
                        End With
                    Next
                End If

                'WESMBillGPPosted
                SQL = "INSERT INTO AM_WESM_BILL_GP_POSTED (BILLING_PERIOD, STL_RUN, CHARGE_TYPE, DUE_DATE, REMARKS, POSTED, UPDATED_BY, BATCH_CODE, GP_REFNO, POSTED_TYPE, DOCUMENT_AMOUNT, AM_JV_NO) " &
                      "SELECT '','','','','" & .Remarks & "', '0', '" &
                                    .UpdatedBy & "', '" &
                                    ItemPostedType.ToString & "-" & BatchCode & "', '', '" &
                                    ItemPostedType.ToString & "', '" &
                                    Math.Round((From x In .JVDetails Select x.Debit).Sum, 2) & "', '" &
                                    JVNumber & "' FROM DUAL"
                listSQL.Add(SQL)

                SQL = "INSERT INTO AM_JV(AM_JV_NO, AM_JV_DATE, BATCH_CODE, STATUS, PREPARED_BY, CHECKED_BY, APPROVED_BY, UPDATED_BY, UPDATED_DATE, POSTED_TYPE) " _
                    & "SELECT " & JVNumber & ", TO_DATE('" & .JVDate.ToShortDateString & "','mm/dd/yyyy'), '" & ItemPostedType.ToString & "-" & BatchCode _
                                     & "', '" & .Status & "', '" & .PreparedBy & "', '" & .CheckedBy & "', '" & .ApprovedBy & "', '" & .UpdatedBy _
                                     & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & ItemPostedType.ToString() & "' FROM DUAL"
                listSQL.Add(SQL)
                'Insert AM_JV_DETAILS
                For Each detail In .JVDetails
                    SQL = "INSERT INTO AM_JV_DETAILS (AM_JV_NO, ACCT_CODE, DEBIT, CREDIT, UPDATED_BY, UPDATED_DATE) " _
                        & "SELECT " & JVNumber & ", '" & detail.AccountCode & "', " & detail.Debit & ", " & detail.Credit _
                                & ", '" & item.UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') FROM DUAL"
                    listSQL.Add(SQL)
                Next
                dicListofSeq("SEQ_AM_BATCH_CODE") += 1
                dicListofSeq("SEQ_AM_JV_NO") += 1
            End With
        Next

        'Insert AM_DMCM query database        
        For Each item In PaymentDMCMList
            'Generate DMCM NO            
            Dim DMCMNo As Long = dicListofSeq.Item("SEQ_AM_DMCM_NO")
            Dim JournalVoucherNo As Long = dicJVNo(EnumPostedType.PA.ToString())

            If Not dicDMCM.ContainsKey(item.DMCMNumber) Then
                dicDMCM.Add(item.DMCMNumber, DMCMNo)
            Else
                DMCMNo = dicDMCM(item.DMCMNumber)
            End If

            SQL = "INSERT INTO AM_DMCM (AM_DMCM_NO, AM_JV_NO, ID_NUMBER, PARTICULARS, CHARGE_TYPE, PREPARED_BY, CHECKED_BY, APPROVED_BY, " &
                    "UPDATED_BY, UPDATED_DATE, TRANS_TYPE, VATABLE, VAT, VAT_EXEMPT, VAT_ZERO_RATED, TOTAL_AMOUNT_DUE, EWT, EWV, STATUS, OTHERS, BILLING_PERIOD, DUE_DATE) " &
                    "SELECT '" & DMCMNo & "', '" & JournalVoucherNo & "', '" & item.IDNumber & "', '" & item.Particulars & "', '" & item.ChargeType.ToString & "', '" & item.PreparedBy & "', '" & item.CheckedBy & "', '" & item.ApprovedBy & "', " &
                    "'" & item.UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & item.TransType & "', '" & item.Vatable & "', '" & item.VAT & "', '" & item.VATExempt & "', '" & item.VatZeroRated & "', " &
                    "'" & item.TotalAmountDue & "', '" & item.EWT & "','" & item.EWV & "', '1', '" & item.Others & "', '" & item.BillingPeriod & "', TO_DATE('" & item.DueDate & "','MM/DD/YYYY') FROM DUAL"
            listSQL.Add(SQL)

            'Insert AM_DMCM_DETAILS query database
            For Each itemDMCM In item.DMCMDetails
                SQL = "INSERT INTO AM_DMCM_DETAILS (AM_DMCM_NO, ACCT_CODE, DEBIT, CREDIT, UPDATED_BY, UPDATED_DATE, INV_DM_CM, SUMMARY_TYPE, ID_NUMBER, IS_COMPUTE) " &
                        "SELECT '" & DMCMNo & "', '" & itemDMCM.AccountCode & "', '" & itemDMCM.Debit & "', '" & itemDMCM.Credit & "', '" & itemDMCM.UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), " &
                        "'" & itemDMCM.InvDMCMNo & "', '" & itemDMCM.SummaryType & "', '" & itemDMCM.IDNumber.IDNumber & "', '" & itemDMCM.IsComputed & "' FROM DUAL"
                listSQL.Add(SQL)
            Next
            dicListofSeq("SEQ_AM_DMCM_NO") += 1
        Next

        'Insert into AM_PAYMENT_NEW query database        
        SQL = "INSERT INTO AM_PAYMENT_NEW (PAYMENT_NO,ALLOCATION_DATE,UPDATED_BY,UPDATED_DATE,IS_POSTED,REMARKS,REMITTANCE_DATE) " &
            "SELECT '" & PaymentNo & "', TO_DATE('" & Me.AllocationDate.CollAllocationDate.ToShortDateString & "','MM/DD/YYYY'),'" & UpdatedBy & "', " &
            "TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '0', NULL, TO_DATE('" & Me.AllocationDate.RemittanceDate.ToShortDateString & "','MM/DD/YYYY') FROM DUAL"
        listSQL.Add(SQL)
        'Insert into AM_PAYMENT_NEW_DETAILS query database
        For Each item In ListofJVNumber
            SQL = "INSERT INTO AM_PAYMENT_NEW_DETAILS (PAYMENT_NO, AM_JV_NO) " &
                 "SELECT " & PaymentNo & ", " & item & " FROM DUAL"
            listSQL.Add(SQL)
        Next

        'Insert into AM_PAYMENT_NEW_AP query database
        For Each item In Me._AMPaymentNewAP
            If Not item.GeneratedDMCM = 0 Then
                Dim _DMCMNumber As Long = dicDMCM(item.GeneratedDMCM)
                'add ENERGY_WITHHOLD and ID_NUMBER for CAP SUMMARY HIstorical Purposes added by lance as of 06/05/2020
                SQL = "INSERT INTO AM_PAYMENT_NEW_AP (PAYMENT_NO, BILLING_PERIOD, ENDING_BALANCE, NEW_ENDING_BALANCE, DUE_DATE, NEW_DUEDATE, " &
                   "ALLOCATION_AMOUNT, PAYMENT_TYPE, PAYMENT_CATEGORY, ALLOCATION_DATE, WESMBILL_SUMMARY_NO, UPDATED_BY, UPDATED_DATE, AM_DMCM_NO, ENERGY_WITHHOLD, ID_NUMBER) " &
                   "SELECT '" & PaymentNo & "', '" & item.BillingPeriod & "', '" & item.EndingBalance & "', '" & item.NewEndingBalance & "', TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), " &
                   "TO_DATE('" & item.NewDueDate & "','MM/DD/YYYY'), '" & item.AllocationAmount & "', '" & item.PaymentType & "', " & item.PaymentCategory & ", TO_DATE('" & item.AllocationDate & "','MM/DD/YYYY'), " &
                   "'" & item.WESMBillSummaryNo & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss')," & _DMCMNumber & ", " & item.EnergyWithHold & ", '" & item.IDNumber & "' FROM DUAL"
                listSQL.Add(SQL)
            Else
                'add ENERGY_WITHHOLD and ID_NUMBER for CAP SUMMARY HIstorical Purposes added by lance as of 06/05/2020
                SQL = "INSERT INTO AM_PAYMENT_NEW_AP (PAYMENT_NO, BILLING_PERIOD, ENDING_BALANCE, NEW_ENDING_BALANCE, DUE_DATE, NEW_DUEDATE, " &
                   "ALLOCATION_AMOUNT, PAYMENT_TYPE, PAYMENT_CATEGORY, ALLOCATION_DATE, WESMBILL_SUMMARY_NO, UPDATED_BY, UPDATED_DATE, AM_DMCM_NO, ENERGY_WITHHOLD, ID_NUMBER) " &
                   "SELECT '" & PaymentNo & "', '" & item.BillingPeriod & "', '" & item.EndingBalance & "', '" & item.NewEndingBalance & "', TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), " &
                   "TO_DATE('" & item.NewDueDate & "','MM/DD/YYYY'), '" & item.AllocationAmount & "', '" & item.PaymentType & "', " & item.PaymentCategory & ", TO_DATE('" & item.AllocationDate & "','MM/DD/YYYY'), " &
                   "'" & item.WESMBillSummaryNo & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss')," & 0 & ", " & item.EnergyWithHold & ", '" & item.IDNumber & "' FROM DUAL"
                listSQL.Add(SQL)
            End If
        Next

        'Insert into AM_PAYMENT_NEW_OFFSETTING_AP query database
        For Each item In Me._AMPaymentNewOffsettingAP
            If Not item.GeneratedDMCM = 0 Then
                Dim _DMCMNumber As Long = dicDMCM(item.GeneratedDMCM)
                'add ENERGY_WITHHOLD and ID_NUMBER for CAP SUMMARY HIstorical Purposes added by lance as of 06/05/2020
                SQL = "INSERT INTO AM_PAYMENT_NEW_OFFSETTING_AP (PAYMENT_NO, BILLING_PERIOD, ENDING_BALANCE, NEW_ENDING_BALANCE, DUE_DATE, NEW_DUEDATE, ALLOCATION_AMOUNT, PAYMENT_TYPE, PAYMENT_CATEGORY, " &
                        "ALLOCATION_DATE, WESMBILL_SUMMARY_NO, UPDATED_BY, UPDATED_DATE, AM_DMCM_NO, OFFSET_SEQ, ENERGY_WITHHOLD, ID_NUMBER) " &
                        "SELECT '" & PaymentNo & "', '" & item.BillingPeriod & "', '" & item.EndingBalance & "', '" & item.NewEndingBalance & "', " &
                        "TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), TO_DATE('" & item.NewDueDate & "','MM/DD/YYYY'), '" & item.AllocationAmount & "', '" & item.PaymentType & "', " & item.PaymentCategory & ", " &
                        "TO_DATE('" & item.AllocationDate & "','MM/DD/YYYY'), '" & item.WESMBillSummaryNo & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), " & _DMCMNumber & ", " & item.OffsettingSequence & ", " & item.EnergyWithHold & ", '" & item.IDNumber & "' FROM DUAL"
                listSQL.Add(SQL)
            Else
                'add ENERGY_WITHHOLD and ID_NUMBER for CAP SUMMARY HIstorical Purposes added by lance as of 06/05/2020
                SQL = "INSERT INTO AM_PAYMENT_NEW_OFFSETTING_AP (PAYMENT_NO, BILLING_PERIOD, ENDING_BALANCE, NEW_ENDING_BALANCE, DUE_DATE, NEW_DUEDATE, ALLOCATION_AMOUNT, PAYMENT_TYPE, PAYMENT_CATEGORY," &
                        "ALLOCATION_DATE, WESMBILL_SUMMARY_NO, UPDATED_BY, UPDATED_DATE, AM_DMCM_NO, OFFSET_SEQ, ENERGY_WITHHOLD, ID_NUMBER) " &
                        "SELECT '" & PaymentNo & "', '" & item.BillingPeriod & "', '" & item.EndingBalance & "', '" & item.NewEndingBalance & "', " &
                        "TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), TO_DATE('" & item.NewDueDate & "','MM/DD/YYYY'), '" & item.AllocationAmount & "', '" & item.PaymentType & "', " & item.PaymentCategory & ", " &
                        "TO_DATE('" & item.AllocationDate & "','MM/DD/YYYY'), '" & item.WESMBillSummaryNo & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), " & 0 & ", " & item.OffsettingSequence & ", " & item.EnergyWithHold & ", '" & item.IDNumber & "' FROM DUAL"
                listSQL.Add(SQL)
            End If
        Next

        'Insert into AM_PAYMENT_NEW_OFFSETTING_AR query database
        For Each item In Me._AMPaymentNewOffsettingAR
            If Not item.GeneratedDMCM = 0 Then
                Dim _DMCMNumber As Long = dicDMCM(item.GeneratedDMCM)
                'add ENERGY_WITHHOLD and ID_NUMBER for CAP SUMMARY HIstorical Purposes added by lance as of 06/05/2020
                SQL = "INSERT INTO AM_PAYMENT_NEW_OFFSETTING_AR (PAYMENT_NO, BILLING_PERIOD, ENDING_BALANCE, NEW_ENDING_BALANCE, DUE_DATE, NEW_DUEDATE, ALLOCATION_AMOUNT, COLLECTION_TYPE, COLLECTION_CATEGORY, " &
                        "ALLOCATION_DATE, WESMBILL_SUMMARY_NO, UPDATED_BY, UPDATED_DATE, AM_DMCM_NO, OFFSET_SEQ, ENERGY_WITHHOLD, ID_NUMBER) " &
                      "SELECT '" & PaymentNo & "', '" & item.BillingPeriod & "', '" & item.EndingBalance & "', '" & item.NewEndingBalance & "', " &
                        "TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), TO_DATE('" & item.NewDueDate & "','MM/DD/YYYY'), '" & item.AllocationAmount & "', '" & item.CollectionType & "', '" & item.CollectionCategory & "', " &
                        "TO_DATE('" & item.AllocationDate & "','MM/DD/YYYY'), '" & item.WESMBillSummaryNo & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), " & _DMCMNumber & ", " & item.OffsettingSequence & ", " & item.EnergyWithHold & ", '" & item.IDNumber & "' FROM DUAL"
                listSQL.Add(SQL)

            Else
                'add ENERGY_WITHHOLD and ID_NUMBER for CAP SUMMARY HIstorical Purposes added by lance as of 06/05/2020
                SQL = "INSERT INTO AM_PAYMENT_NEW_OFFSETTING_AR (PAYMENT_NO, BILLING_PERIOD, ENDING_BALANCE, NEW_ENDING_BALANCE, DUE_DATE, NEW_DUEDATE, ALLOCATION_AMOUNT, COLLECTION_TYPE, COLLECTION_CATEGORY, " &
                        "ALLOCATION_DATE, WESMBILL_SUMMARY_NO, UPDATED_BY, UPDATED_DATE, AM_DMCM_NO, OFFSET_SEQ, ENERGY_WITHHOLD, ID_NUMBER) " &
                      "SELECT '" & PaymentNo & "', '" & item.BillingPeriod & "', '" & item.EndingBalance & "', '" & item.NewEndingBalance & "', " &
                        "TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), TO_DATE('" & item.NewDueDate & "','MM/DD/YYYY'), '" & item.AllocationAmount & "', '" & item.CollectionType & "', '" & item.CollectionCategory & "', " &
                        "TO_DATE('" & item.AllocationDate & "','MM/DD/YYYY'), '" & item.WESMBillSummaryNo & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), " & 0 & ", " & item.OffsettingSequence & ", " & item.EnergyWithHold & ", '" & item.IDNumber & "' FROM DUAL"
                listSQL.Add(SQL)
            End If
        Next

        'WESMBillSummary        
        For Each item In Me.UpdatedWESMBillSummaryList
            SQL = "UPDATE AM_WESM_BILL_SUMMARY SET ENDING_BALANCE = '" & item.EndingBalance & "', NEW_DUEDATE = TO_DATE('" & item.NewDueDate & "','MM/DD/YYYY'), " &
                                                  "UPDATED_DATE = TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), UPDATED_BY = '" & UpdatedBy & "', " &
                                                  "TRANSACTION_DATE = TO_DATE('" & Me.AllocationDate.CollAllocationDate.ToShortDateString & "','MM/DD/YYYY'), " &
                                                  "ENERGY_WITHHOLD_STATUS = " & item.EnergyWithholdStatus & ",  " &
                                                  "ENERGY_WITHHOLD = " & item.EnergyWithhold & " " &
                                                  "WHERE WESMBILL_SUMMARY_NO = '" & item.WESMBillSummaryNo & "'"
            listSQL.Add(SQL)
        Next


        'WESMBillSummaryHistory            
        Dim ForWESMBillSummaryHistoryAP = (From x In Me.AMPaymentNewAP Select x).Union _
                                          (From x In Me.AMPaymentNewOffsettingAP Select x).ToList()

        Dim SumOfForWESMBillSummaryHistoryAP = (From x In ForWESMBillSummaryHistoryAP
                                                Group By WESMBillSummaryNo = x.WESMBillSummaryNo, DueDate = x.NewDueDate,
                                                PaymentType = x.PaymentType Into AllocationAmount = Sum(x.AllocationAmount)).ToList()

        Dim SumOfForWESMBillSummaryHistoryAR = (From x In Me.AMPaymentNewOffsettingAR
                                                Group By WESMBillSummaryNo = x.WESMBillSummaryNo, DueDate = x.NewDueDate,
                                                CollectionType = x.CollectionType Into AllocationAmount = Sum(x.AllocationAmount)).ToList()

        For Each item In SumOfForWESMBillSummaryHistoryAP
            SQL = "INSERT INTO AM_WESM_BILL_SUMMARY_HISTORY (WESMBILL_SUMMARY_NO, DUE_DATE, AMOUNT, UPDATED_BY, UPDATED_DATE, PAYMENT_TYPE, PAYMENT_NO) " &
                    "SELECT '" & item.WESMBillSummaryNo & "', TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), '" & item.AllocationAmount & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & item.PaymentType & "', '" & PaymentNo & "' FROM DUAL"
            listSQL.Add(SQL)
        Next

        For Each item In SumOfForWESMBillSummaryHistoryAR
            SQL = "INSERT INTO AM_WESM_BILL_SUMMARY_HISTORY (WESMBILL_SUMMARY_NO, DUE_DATE, AMOUNT, UPDATED_BY, UPDATED_DATE,COLLECTION_TYPE, PAYMENT_NO) " &
                  "SELECT '" & item.WESMBillSummaryNo & "', TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), '" & item.AllocationAmount & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & item.CollectionType & "', '" & PaymentNo & "' FROM DUAL"
            listSQL.Add(SQL)
        Next

        Dim GetDMCMForWESMBillSummaryWHTaxAdj = (From x In Me.PaymentDMCMList
                                                 Where x.TransType = EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAROnEnergyAddBack _
                                                 Or x.TransType = EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAPOnEnergyAddBack Select x).ToList()

        For Each item In UpdatedWESMBillSummaryWHTAXAdjList
            Dim getDMCM = (From x In GetDMCMForWESMBillSummaryWHTaxAdj Where x.Particulars.Contains(item.INVDMCMNo) Select x).FirstOrDefault
            If Not getDMCM Is Nothing Then
                If item.BalanceType = EnumBalanceType.AR Then
                    SQL = "INSERT INTO AM_WESM_BILL_SUMMARY_HISTORY (WESMBILL_SUMMARY_NO, DUE_DATE, AMOUNT, UPDATED_BY, UPDATED_DATE,COLLECTION_TYPE, PAYMENT_NO) " & _
                        "VALUES ('" & item.WESMBillSummaryNo & "', TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), '" & getDMCM.TotalAmountDue & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & EnumCollectionType.EnergyAREndingBalanceAdjustment & "', '" & PaymentNo & "')"
                    listSQL.Add(SQL)
                Else
                    SQL = "INSERT INTO AM_WESM_BILL_SUMMARY_HISTORY (WESMBILL_SUMMARY_NO, DUE_DATE, AMOUNT, UPDATED_BY, UPDATED_DATE,PAYMENT_TYPE, PAYMENT_NO) " & _
                        "VALUES ('" & item.WESMBillSummaryNo & "', TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), '" & getDMCM.TotalAmountDue * -1 & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & EnumPaymentNewType.EnergyAPEndingBalanceAdjustment & "', '" & PaymentNo & "')"
                    listSQL.Add(SQL)
                End If
            End If
        Next

        'AM_FTF_MAIN        
        Dim ReplenishmentRefNo As Long = 0
        For Each item In Me.PaymentFTF

            Dim JVBatchCode As String = ""
            Dim ReferenceNo As Long = dicListofSeq.Item("SEQ_AM_FTF_REF_NO")

            item.RefNo = ReferenceNo
            JVBatchCode = EnumPostedType.PEFT.ToString & "-" & CStr(dicJVBatchNo(EnumPostedType.PEFT.ToString))
            ReplenishmentRefNo = ReferenceNo

            SQL = "INSERT INTO AM_FTF_MAIN (ALLOCATION_DATE, BATCH_CODE, REF_NO, DR_DATE, CR_DATE, TOTAL_AMOUNT, " & _
                                                        "TRANS_TYPE, STATUS, IS_POSTED, REQUESTING_APPROVAL, APPROVED_BY, UPDATED_BY, UPDATED_DATE) " & _
                  "SELECT TO_DATE('" & item.AllocationDate.ToShortDateString & "','MM/DD/YYYY'), '" & JVBatchCode & "', '" & ReferenceNo & "', " & _
                                "TO_DATE('" & item.DRDate.ToString("MM/dd/yyyy") & "','MM/DD/YYYY'), TO_DATE('" & item.CRDate.ToString("MM/dd/yyyy") & "','MM/DD/YYYY'), '" & item.TotalAmount & "', " & _
                                "'" & item.TransType & "', '" & item.Status & "', '" & item.IsPosted & "', '" & item.RequestingApproval & "', '" & item.ApprovedBy & "', " & _
                                "'" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') FROM DUAL"
            listSQL.Add(SQL)

            For Each ItemFTF In item.ListOfFTFDetails
                SQL = "INSERT INTO AM_FTF_DETAILS (REF_NO, ACCT_CODE, BANK_ACCNT_NO, ISSUING_BANK, RECEIVING_BANK, DEBIT, CREDIT, PARTICULARS, UPDATED_BY, UPDATED_DATE) " & _
                      "SELECT '" & ReferenceNo & "', '" & ItemFTF.AccountCode & "', '" & ItemFTF.BankAccountNo & "', '" & ItemFTF.IssuingBank & "', '" & ItemFTF.ReceivingBank & "', " & _
                             "'" & ItemFTF.Debit & "', '" & ItemFTF.Credit & "', '" & ItemFTF.Particulars & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') FROM DUAL"
                listSQL.Add(SQL)
            Next

            For Each ItemParticipants In item.ListOfFTFParticipants
                SQL = "INSERT INTO AM_FTF_PARTICIPANT (REF_NO, ID_NUMBER, AMOUNT, UPDATED_BY, UPDATED_DATE) " & _
                      "SELECT '" & ReferenceNo & "', '" & ItemParticipants.IDNumber.IDNumber & "', " & _
                              "'" & ItemParticipants.Amount & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') FROM DUAL"
                listSQL.Add(SQL)
            Next
            dicListofSeq("SEQ_AM_FTF_REF_NO") += 1
        Next

        'AM_FTF_MAIN COLLECTION
        For Each item In Me.CollectionFTF
            If item.TransType = EnumFTFTransType.DrawDown Or item.TransType = EnumFTFTransType.Replenishment Or
                item.TransType = EnumFTFTransType.TransferMarketFeesToPEMC Then
                SQL = "UPDATE AM_FTF_MAIN SET DR_DATE = TO_DATE('" & Me.AllocationDate.RemittanceDate.ToShortDateString & "','MM/DD/YYYY'), CR_DATE = TO_DATE('" & Me.AllocationDate.RemittanceDate.ToShortDateString & "','MM/DD/YYYY')  WHERE REF_NO = " & item.RefNo
                listSQL.Add(SQL)
            End If
        Next

        'AM_DEFERRED_PAYMENT_MAIN
        For Each item In Me.NewPaymentDeferred
            Dim DeferredPaymentNo As Long = dicListofSeq.Item("SEQ_AM_DEF_PYMNT_NO")
            If Not item.DMCMNumber = 0 Then
                Dim DmcmNoX As Long = dicDMCM(item.DMCMNumber)
                SQL = "INSERT INTO AM_DEFERRED_PAYMENT_MAIN (ALLOCATION_DATE, DEFERRED_PAYMENT_NO, ID_NUMBER, OUTSTANDING_DEFERRED_PAYMENT, " & _
                                                                            "PAYMENT_NO, DEFERRED_AMOUNT, DEFERRED_TYPE, CHARGE_TYPE, AM_DMCM_NO, REMARKS, ORIG_DATE) " & _
                      "SELECT TO_DATE('" & item.AllocationDate & "','MM/DD/YYYY'), '" & DeferredPaymentNo & "', '" & item.IDNumber & "', " & _
                             "'" & item.OutstandingBalanceDeferredPayment & "', '" & PaymentNo & "', '" & item.DeferredAmount & "', '" & item.DeferredType & "', " & _
                             "'" & item.ChargeType.ToString & "', '" & DmcmNoX & "', '" & item.Remarks & "',TO_DATE('" & item.OriginalDate.ToShortDateString & "','mm/dd/yyyy') FROM DUAL"
                listSQL.Add(SQL)
            Else
                SQL = "INSERT INTO AM_DEFERRED_PAYMENT_MAIN (ALLOCATION_DATE, DEFERRED_PAYMENT_NO, ID_NUMBER, OUTSTANDING_DEFERRED_PAYMENT, " & _
                                                                          "PAYMENT_NO, DEFERRED_AMOUNT, DEFERRED_TYPE, CHARGE_TYPE, AM_DMCM_NO, REMARKS, ORIG_DATE) " & _
                      "SELECT TO_DATE('" & item.AllocationDate & "','MM/DD/YYYY'), '" & DeferredPaymentNo & "', '" & item.IDNumber & "', " & _
                      "'" & item.OutstandingBalanceDeferredPayment & "', '" & PaymentNo & "', '" & item.DeferredAmount & "', '" & item.DeferredType & "', " & _
                      "'" & item.ChargeType.ToString & "', '" & 0 & "', '" & item.Remarks & "',TO_DATE('" & item.OriginalDate.ToShortDateString & "','mm/dd/yyyy') FROM DUAL"
                listSQL.Add(SQL)
            End If
            dicListofSeq("SEQ_AM_DEF_PYMNT_NO") += 1
        Next

        'EFT
        For Each item In PaymentEFT
            SQL = "INSERT INTO AM_PAYMENT_NEW_EFT (PAYMENT_NO, ALLOCATION_DATE, ID_NUMBER, PAYMENT_TYPE, CHECK_NO, EXCESS_COLLECTION, DEFERRED_ENERGY, " & _
                                                                "DEFERRED_VAT, ENERGY, VAT, MF, RETURN_AMOUNT, TRANSFER_TO_PRUDENTIAL, INTEREST_NSS, INTEREST_STL, UPDATED_BY, UPDATED_DATE, TRANSFER_TO_FINPEN, OFFSET_DEFERRED_ENERGY, OFFSET_DEFERRED_VAT) " & _
                  "SELECT " & PaymentNo & " ,TO_DATE('" & item.AllocationDate & "','MM/DD/YYYY'), '" & item.Participant.IDNumber & "', '" & item.PaymentType & "', " & _
                        "'" & item.CheckNumber & "', '" & item.ExcessCollection & "', '" & item.DeferredEnergy & "', '" & item.DeferredVAT & "', '" & item.Energy & "', '" & item.VAT & "', " & _
                        "'" & item.MarketFees & "', '" & item.ReturnAmount & "', '" & item.TransferPrudential & "', '" & item.NSSInterest & "', '" & item.STLInterest & "', " & _
                        "'" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), " & item.TransferFinPen.ToString & ", " & item.OffsetOnDeferredEnergy.ToString & ", " & item.OffsetOnDeferredVAT.ToString & " FROM DUAL"
            listSQL.Add(SQL)
        Next

        'AM_PAYMENT_SHARE
        For Each item In AMPaymentShare
            PaymentShareNo = dicListofSeq.Item("SEQ_AM_PAY_SHARE_NO")
            SQL = "INSERT INTO AM_PAYMENT_NEW_SHARE (PAYMENT_SHARE_NO, WESMBILLSUMMARY_NO, BILLING_PERIOD, ID_NUMBER, INV_NUMBER, AMOUNT_SHARE, AMOUNT_OFFSET, " & _
                                                                  "AMOUNT_BALANCE, CHARGE_TYPE, OFFSETTING_SEQ_NO, PAYMENT_NO, PAYMENT_TYPE) " & _
                  "SELECT '" & PaymentShareNo & "','" & item.WESMBillSummaryNo & "', '" & item.BillingPeriod & "', '" & item.IDNumber & "', '" & item.InvoiceNumber & "', " & _
                  "'" & item.AmountShare & "', '" & 0 & "', '" & item.AmountBalance & "', '" & item.ChargeType.ToString & "', '" & item.OffsettingSequence & "', '" & PaymentNo & "', '" & item.PaymentType & "' FROM DUAL"
            listSQL.Add(SQL)
            For Each itemPaymentShare In item.AmountOffset
                SQL = "INSERT INTO AM_PAYMENT_NEW_SHARE_DETAILS (PAYMENT_SHARE_NO, WESMBILL_SUMMARY_NO, BILLING_PERIOD, ID_NUMBER, INV_NUMBER, AMOUNT_OFFSET, CHARGE_TYPE, OFFSETTING_SEQ_NO) " & _
                      "SELECT '" & PaymentShareNo & "', '" & itemPaymentShare.ForWESMBillSummaryNo & "', '" & itemPaymentShare.BillingPeriod & "', '" & itemPaymentShare.IDNumber & "', '" & itemPaymentShare.InvoiceNumber & "', " & _
                             "'" & itemPaymentShare.OffsetAmount & "', '" & itemPaymentShare.ChargeType.ToString & "', '" & itemPaymentShare.BatchGroupSequence & "' FROM DUAL"
                listSQL.Add(SQL)
            Next
            dicListofSeq("SEQ_AM_PAY_SHARE_NO") += 1
        Next

        'AM_RFP
        With RFPList
            Dim RFPRefNo As Long = dicListofSeq.Item("SEQ_AM_RFP_NO")
            SQL = "INSERT INTO AM_RFP (ALLOCATION_DATE, REFERENCE_NO, PAYMENT_DATE, RFP_TO, RFP_FROM, RFP_PURPOSE, PREPARED_BY, REVIEWED_BY, APPROVED_BY, " _
                                    & "UPDATED_DATE, UPDATED_BY, NSS_APPLIED, PR_REPLENISHMENT, MF_APPLIED, TRANSFER_PEMC, HELD_COLLECTION, PAYMENT_NO) " _
                & "SELECT TO_DATE('" & .AllocationDate & "','MM/DD/YYYY'), " & RFPRefNo & ", TO_DATE('" & .PaymentDate & "','MM/DD/YYYY'), '" _
                        & .toRFP & "', '" & .FromRFP & "', '" & .PurposeOfPayment & "', '" & .PreparedBy & "', '" & .ReviewedBy & "', '" & .ApprovedBy & "', " _
                        & "TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & .UpdatedBy & "', " & .NSSAmount & ", " _
                        & .PRReplenishment & ", " & .MarketFees & ", " & .TransferToPEMC & ", " & .HeldCollection & ", " & PaymentNo & " FROM DUAL"

            listSQL.Add(SQL)

            For Each item In RFPList.RFPDetails
                SQL = "INSERT INTO AM_RFP_DETAILS (REFERENCE_NO, ID_NUMBER, BANK_BRANCH, ACCOUNT_NO, RFP_PAYMENT_TYPE, AMOUNT, DATE_OF_DEPOSIT, PARTICULARS, ALLOCATION_DATE, RFP_DETAILS_TYPE) " _
                    & "SELECT " & RFPRefNo & ", '" & item.Participant & "', '" & item.BankBranch & "', '" & item.AccountNo & "', '" & item.PaymentType & "', '" & item.Amount & "', " _
                                & "TO_DATE('" & item.DateOfDeposit & "', 'MM/DD/YYYY'), '" & item.Particulars & "', TO_DATE('" & item.AllocationDate & "', 'MM/DD/YYYY'), " & item.RFPDetailsType & " FROM DUAL"
                listSQL.Add(SQL)
            Next
            dicListofSeq("SEQ_AM_RFP_NO") += 1
        End With

        'AM_PAYMENT_NEW_WBSHISTORY_BAL
        For Each item In Me.WESMBillSummaryBalance
            SQL = "INSERT INTO AM_PAYMENT_NEW_WBS_BALANCE (PAYMENT_NO, BILLING_PERIOD, ORIG_DUE_DATE, BILLING_PERIOD_REMARKS, TOTAL_BILL, ID_NUMBER, AMOUNT_BALANCE, CHARGE_TYPE, BALANCE_TYPE, WESMBILL_BATCH_NO) " _
                & "SELECT " & PaymentNo & ", " & item.BillingPeriod & ", TO_DATE('" & item.OriginalDueDate & "', 'MM/DD/YYYY'), '" & item.BillingPeriodRemarks & "', '" & item.TotalBillAmount & "', '" & item.IDNumber.IDNumber _
                            & "', " & item.AmountBalance & ", '" & item.ChargeType.ToString & "', '" & item.BalanceType.ToString & "', " & item.WESMBillBatchNo & " FROM DUAL"
            listSQL.Add(SQL)
        Next

        'AM_PRUDENTIAL_HISTORY
        For Each item In Me.PrudentialHistoryList
            Dim JVBatchCode As String = EnumPostedType.PEFT.ToString & "-" & CStr(dicJVBatchNo(EnumPostedType.PEFT.ToString))
            SQL = "UPDATE AM_PRUDENTIAL SET PRUDENTIAL_AMOUNT = PRUDENTIAL_AMOUNT + " & item.Amount & _
                 ",UPDATED_BY = '" & AMModule.UserName & "',UPDATED_DATE = " & "TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') " & _
                 "WHERE ID_NUMBER = '" & item.IDNumber.IDNumber & "'"
            listSQL.Add(SQL)

            SQL = "INSERT INTO AM_PRUDENTIAL_HISTORY (ID_NUMBER, AMOUNT, TRANS_TYPE, TRANS_DATE, UPDATED_BY, UPDATED_DATE, BATCH_CODE, FTF_REF_NO) " & _
                  "SELECT '" & item.IDNumber.IDNumber & "', " & item.Amount & ", " & item.TransType & ", " & "TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') " & _
                            ", '" & AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & JVBatchCode & _
                            "', " & ReplenishmentRefNo & " FROM DUAL"
            listSQL.Add(SQL)
        Next

        'Declared dictionary variables to get the OR FINPEN
        Dim dicORFinPen As New Dictionary(Of String, Long)

        'AM_OFFICIAL_RECEIPT_MAIN
        For Each item In Me.ORList

            Dim ORNo As Long = dicListofSeq.Item("SEQ_AM_OR_NO")
            Dim BatchCode As String = EnumPostedType.PA.ToString & "-" & dicJVBatchNo(EnumPostedType.PA.ToString).ToString

            If item.TransactionType = EnumORTransactionType.FinPenAmount Then
                If Not dicORFinPen.ContainsKey(item.IDNumber) Then
                    dicORFinPen.Add(item.IDNumber, ORNo)
                End If
            End If
            SQL = "INSERT INTO AM_OFFICIAL_RECEIPT_MAIN (OR_NO,OR_DATE,ID_NUMBER,OR_AMOUNT,STATUS,UPDATED_BY,UPDATED_DATE, BATCH_CODE,REMARKS,TRANS_TYPE, " & _
                                                        "VAT_EXEMPT,VATABLE,VAT,VAT_ZERO_RATED,OTHERS,WITHHOLDING_TAX,WITHHOLDING_VAT) " & _
                  "SELECT " & ORNo & ", TO_DATE('" & item.ORDate & "', 'MM/DD/YYYY'), '" & item.IDNumber & "', " & item.Amount & ", " & item.Status & ", '" & _
                            AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & BatchCode & _
                            "', '" & item.Remarks & "', " & item.TransactionType & ", " & item.VATExempt & ", " & item.Vatable & ", " & item.VAT & ", " & item.VATZeroRated & _
                            ", " & item.Others & ", " & item.WithholdingTax & ", " & item.WithholdingVAT & " FROM DUAL"

            listSQL.Add(SQL)

            For Each detail In item.ListORDetails
                SQL = "INSERT INTO AM_OFFICIAL_RECEIPT_DETAILS (OR_NO, ACCT_CODE, DESCRIPTION, DEBIT, CREDIT, UPDATED_BY, UPDATED_DATE) " & _
                      "SELECT " & ORNo & ", '" & detail.AccountCode & "', '" & detail.Description & "', " & detail.Debit & ", " & detail.Credit & _
                               ", '" & AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') FROM DUAL"
                listSQL.Add(SQL)
            Next

            For Each summary In item.ListORSummary
                SQL = "INSERT INTO AM_OFFICIAL_RECEIPT_SUMMARY (OR_NO, WESMBILL_SUMMARY_NO, DUE_DATE, AMOUNT, COLLECTION_TYPE, UPDATED_BY, UPDATED_DATE) " & _
                      "SELECT " & ORNo & ", " & summary.WESMBillSummaryNo & ", TO_DATE('" & summary.DueDate & "', 'MM/DD/YYYY'), " & summary.Amount & ", " & _
                                summary.CollectionType & ", '" & AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') FROM DUAL"
                listSQL.Add(SQL)
            Next
            dicListofSeq("SEQ_AM_OR_NO") += 1
        Next

        'AM_PAYMENT_NEW_TRANSFER_TO_PR
        For Each item In Me._AMPaymentTransferToPR
            Dim getFinPenOrPerMP As Long = (From x In Me.ORList Where x.TransactionType = EnumORTransactionType.FinPenAmount And x.IDNumber = item.IDNumber Select x.ORNo).FirstOrDefault
            If dicORFinPen.ContainsKey(item.IDNumber) Then
                SQL = "INSERT INTO AM_PAYMENT_NEW_TRANSFER_TO_PR (PAYMENT_NO, ID_NUMBER, PAYMENT_EXCESS_COLLECTION, PAYMENT_DEFERRED_ENERGY, PAYMENT_DEFERRED_VATONENERGY, PAYMENT_MF, PAYMENT_ENERGY, " & _
                                                             "PAYMENT_VATONENERGY, PAYMENT_TOTAL, FULLY_TRANSFER_TO_PR, TRANSFER_TO_PR, TOTAL_AMOUNT_FOR_REMITTANCE, FULLY_TRANSFER_TO_FINPEN, TRANSFER_TO_FINPEN, FINPEN_OR_NO, OFFSET_DEFERRED_ENERGY, OFFSET_DEFERRED_VAT) " & _
                    "SELECT '" & PaymentNo & "','" & item.IDNumber & "','" & item.PaymentOnExcessCollection & "', '" & item.PaymentOnDeferredonEnergy & "', '" & item.PaymentOnDeferredonVATonEnergy & "', '" & item.PaymentOnMFWithVAT & _
                             "','" & item.PaymentOnEnergy & "', '" & item.PaymentOnVATonEnergy & "','" & item.TotalPaymentAllocated & "','" & IIf(item.FullyTransferToPR, 1, 0) & "','" & item.TransferToPrudential & "','" & item.TotalAmountForRemittance & _
                             "','" & IIf(item.FullyTransferToFinPen, 1, 0) & "'," & item.TransferToFinPen.ToString & ", " & dicORFinPen.Item(item.IDNumber).ToString & ", " & item.OffsetOnOffsetDeferredonEnergy & ", " & item.OffsetOnOffsetDeferredonVATonEnergy & " FROM DUAL"
            Else
                SQL = "INSERT INTO AM_PAYMENT_NEW_TRANSFER_TO_PR (PAYMENT_NO, ID_NUMBER, PAYMENT_EXCESS_COLLECTION, PAYMENT_DEFERRED_ENERGY, PAYMENT_DEFERRED_VATONENERGY, PAYMENT_MF, PAYMENT_ENERGY, " & _
                                                             "PAYMENT_VATONENERGY, PAYMENT_TOTAL, FULLY_TRANSFER_TO_PR, TRANSFER_TO_PR, TOTAL_AMOUNT_FOR_REMITTANCE, FULLY_TRANSFER_TO_FINPEN, TRANSFER_TO_FINPEN, OFFSET_DEFERRED_ENERGY, OFFSET_DEFERRED_VAT) " & _
                    "SELECT '" & PaymentNo & "','" & item.IDNumber & "','" & item.PaymentOnExcessCollection & "', '" & item.PaymentOnDeferredonEnergy & "', '" & item.PaymentOnDeferredonVATonEnergy & "', '" & item.PaymentOnMFWithVAT & _
                             "','" & item.PaymentOnEnergy & "', '" & item.PaymentOnVATonEnergy & "','" & item.TotalPaymentAllocated & "','" & IIf(item.FullyTransferToPR, 1, 0) & "','" & item.TransferToPrudential & "','" & item.TotalAmountForRemittance & _
                             "','" & IIf(item.FullyTransferToFinPen, 1, 0) & "'," & item.TransferToFinPen.ToString & ", " & item.OffsetOnOffsetDeferredonEnergy & ", " & item.OffsetOnOffsetDeferredonVATonEnergy & " FROM DUAL"
            End If
            listSQL.Add(SQL)
        Next

        For Each item In Me.WESMBillSummaryNoTransAR
            'add ENERGY_WITHHOLD and ID_NUMBER for CAP SUMMARY HIstorical Purposes added by lance as of 06/05/2020
            SQL = "INSERT INTO AM_PAYMENT_NEW_WBS_NOTRANS ( PAYMENT_NO, BILLING_PERIOD, ENDING_BALANCE, NEW_ENDING_BALANCE, ALLOCATION_AMOUNT, DUE_DATE, NEW_DUEDATE, WESMBILL_SUMMARY_NO, ENERGY_WITHHOLD, ID_NUMBER) " _
                & "SELECT " & Me.PaymentNumber & ", " & item.BillingPeriod & ", '" & item.EndingBalance & "', '" & item.NewEndingBalance & "', '0', TO_DATE('" & item.DueDate.ToShortDateString & "','mm/dd/yyyy'), " _
                         & "TO_DATE('" & item.NewDueDate.ToShortDateString & "','mm/dd/yyyy'), " & item.WESMBillSummaryNo & ", " & item.EnergyWithHold & ", '" & item.IDNumber & "' FROM DUAL"
            listSQL.Add(SQL)
        Next

        For Each item In Me.WESMBillSummaryNoTransAP
            'add ENERGY_WITHHOLD and ID_NUMBER for CAP SUMMARY HIstorical Purposes added by lance as of 06/05/2020
            SQL = "INSERT INTO AM_PAYMENT_NEW_WBS_NOTRANS ( PAYMENT_NO, BILLING_PERIOD, ENDING_BALANCE, NEW_ENDING_BALANCE, ALLOCATION_AMOUNT, DUE_DATE, NEW_DUEDATE, WESMBILL_SUMMARY_NO, ENERGY_WITHHOLD, ID_NUMBER) " _
                & "SELECT " & Me.PaymentNumber & ", " & item.BillingPeriod & ", '" & item.EndingBalance & "', '" & item.NewEndingBalance & "', '0', TO_DATE('" & item.DueDate.ToShortDateString & "','mm/dd/yyyy'), " _
                         & "TO_DATE('" & item.NewDueDate.ToShortDateString & "','mm/dd/yyyy'), " & item.WESMBillSummaryNo & ", " & item.EnergyWithHold & ", '" & item.IDNumber & "' FROM DUAL"
            listSQL.Add(SQL)
        Next

        For Each item In Me.WESMTransDetailsSummaryList
            If item.Status.Equals(EnumWESMTransDetailsSummaryStatus.ADDED.ToString) Then
                SQL = "INSERT INTO AM_WESM_TRANS_DETAILS_SUMMARY(BUYER_TRANS_NO,BUYER_BILLING_ID,SELLER_TRANS_NO,SELLER_BILLING_ID,DUE_DATE,NEW_DUE_DATE,ORIG_AMOUNT_ENERGY,OBIN_ENERGY," & vbNewLine _
                                            & "ORIG_AMOUNT_VAT,OBIN_VAT,ORIG_AMOUNT_EWT,OBIN_EWT,UPDATED_DATE,UPDATED_BY)" & vbNewLine _
                      & "SELECT '" & item.BuyerTransNo & "', '" & item.BuyerBillingID & "', '" & item.SellerTransNo & "', '" & item.SellerBillingID & "', TO_DATE('" & item.DueDate.ToShortDateString & "','MM/DD/yyyy')," & vbNewLine _
                      & "TO_DATE('" & item.NewDueDate.ToShortDateString & "','MM/DD/yyyy')," & item.OrigBalanceInEnergy & "," & item.OutstandingBalanceInEnergy & "," & item.OrigBalanceInVAT & vbNewLine _
                      & "," & item.OutstandingBalanceInVAT & "," & item.OrigBalanceInEWT & "," & item.OutstandingBalanceInEWT & ",SYSDATE,'" & AMModule.UserName & "' FROM DUAL"
                listSQL.Add(SQL)
            ElseIf item.Status.Equals(EnumWESMTransDetailsSummaryStatus.UPDATED.ToString) Then
                SQL = "UPDATE AM_WESM_TRANS_DETAILS_SUMMARY SET OBIN_ENERGY = " & item.OutstandingBalanceInEnergy & ",OBIN_VAT = " & item.OutstandingBalanceInVAT & vbNewLine _
                    & " WHERE BUYER_TRANS_NO = '" & item.BuyerTransNo & "' AND SELLER_TRANS_NO = '" & item.SellerTransNo & "'"
                listSQL.Add(SQL)
            End If
        Next

        For Each item In Me.WESMTransDetailsSummaryHistoryList
            SQL = "INSERT INTO AM_WESM_TRANS_DETAILS_SUMMARY_HISTORY(BUYER_TRANS_NO,BUYER_BILLING_ID,SELLER_TRANS_NO,SELLER_BILLING_ID,DUE_DATE,ALLOCATION_DATE,REMITTANCE_DATE,ALLOCATED_IN_ENERGY,ALLOCATED_IN_DEFINT,ALLOCATED_IN_VAT," & vbNewLine _
                                            & "PROCESSED_DATE,PROCESED_BY)" & vbNewLine _
                      & "SELECT '" & item.BuyerTransNo & "', '" & item.BuyerBillingID & "', '" & item.SellerTransNo & "', '" & item.SellerBillingID & "', TO_DATE('" & item.DueDate.ToShortDateString & "','MM/DD/yyyy')" & vbNewLine _
                      & ",TO_DATE('" & Me.AllocationDate.CollAllocationDate.ToShortDateString & "','MM/DD/yyyy')" & ",TO_DATE('" & Me.AllocationDate.RemittanceDate.ToShortDateString & "','MM/DD/yyyy')," & vbNewLine _
                      & item.AllocatedInEnergy & "," & item.AllocatedInDefInt & "," & item.AllocatedInVAT & vbNewLine _
                      & ",SYSDATE,'" & AMModule.UserName & "' FROM DUAL"
            listSQL.Add(SQL)
        Next

        If Me.AllocationDate.CollAllocationDate.Month = Me.AllocationDate.RemittanceDate.Month Then
            'Delete AM_STL_NOTICE_NEW
            SQL = "DELETE FROM AM_STL_NOTICE_NEW WHERE STL_NOTICE_DATE = LAST_DAY(TO_DATE('" & FormatDateTime(Me.AllocationDate.CollAllocationDate, DateFormat.ShortDate) & "','mm/dd/yyyy'))"
            listSQL.Add(SQL)

            'Insert into AM_STL_NOTICE_NEW
            SQL = "INSERT INTO AM_STL_NOTICE_NEW(STL_NOTICE_DATE, ENDING_BALANCE, WESMBILL_SUMMARY_NO) " & _
                  "(SELECT LAST_DAY(TO_DATE('" & FormatDateTime(Me.AllocationDate.CollAllocationDate, DateFormat.ShortDate) & "','mm/dd/yyyy')) AS TRANSACTIONDATE, ENDING_BALANCE, WESMBILL_SUMMARY_NO " & _
                  " FROM AM_WESM_BILL_SUMMARY WHERE ENDING_BALANCE <> 0 OR TO_CHAR(NEW_DUEDATE,'MM/YYYY') = TO_CHAR(TO_DATE('" & FormatDateTime(Me.AllocationDate.CollAllocationDate, DateFormat.ShortDate) & "','MM/DD/YYYY'),'MM/YYYY'))"

            listSQL.Add(SQL)
        Else
            Dim distinctWESMBillSummaryNo As List(Of Long) = (From x In Me.AMCollectionAR Select x.WESMBillSummaryNo).Distinct.ToList()
            For Each item In distinctWESMBillSummaryNo
                'UPDATE
                SQL = "UPDATE AM_STL_NOTICE_NEW A " & _
                      "SET A.STL_NOTICE_DATE = LAST_DAY(TO_DATE('" & FormatDateTime(Me.AllocationDate.CollAllocationDate, DateFormat.ShortDate) & "','mm/dd/yyyy')), " & _
                          "A.ENDING_BALANCE = (SELECT B.ENDING_BALANCE FROM AM_WESM_BILL_SUMMARY B WHERE B.WESMBILL_SUMMARY_NO = A.WESMBILL_SUMMARY_NO) " & _
                      "WHERE A.WESMBILL_SUMMARY_NO = " & item & " AND A.STL_NOTICE_DATE = LAST_DAY(TO_DATE('" & FormatDateTime(Me.AllocationDate.CollAllocationDate, DateFormat.ShortDate) & "','mm/dd/yyyy'))"
                listSQL.Add(SQL)
            Next

            'Insert into AM_STL_NOTICE_NEW
            SQL = "INSERT INTO AM_STL_NOTICE_NEW(STL_NOTICE_DATE, ENDING_BALANCE, WESMBILL_SUMMARY_NO) " & _
                  "(SELECT LAST_DAY(TO_DATE('" & FormatDateTime(Me.AllocationDate.RemittanceDate, DateFormat.ShortDate) & "','mm/dd/yyyy')) AS TRANSACTIONDATE, ENDING_BALANCE, WESMBILL_SUMMARY_NO " & _
                  " FROM AM_WESM_BILL_SUMMARY WHERE ENDING_BALANCE <> 0 OR TO_CHAR(NEW_DUEDATE,'MM/YYYY') = TO_CHAR(TO_DATE('" & FormatDateTime(Me.AllocationDate.CollAllocationDate, DateFormat.ShortDate) & "','MM/DD/YYYY'),'MM/YYYY'))"

            listSQL.Add(SQL)

        End If

        Return listSQL

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                Me.ClearObjects()
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
