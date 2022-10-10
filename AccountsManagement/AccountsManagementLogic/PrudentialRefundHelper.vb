Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementDataAccess


Public Class PrudentialRefundHelper
    Implements IDisposable

    Private dicListofSeq As New Dictionary(Of String, Long)

#Region "WESMBillHelper"
    Public _WBillHelper As WESMBillHelper
    Private ReadOnly Property WBillHelper() As WESMBillHelper
        Get
            Return _WBillHelper
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

#Region "DAL"
    Private _DataAccess As DAL
    Public ReadOnly Property DataAccess() As DAL
        Get
            Return Me._DataAccess
        End Get
    End Property
#End Region

#Region "Allocation Date"
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

#Region "List of Allocation Date"
    Private _TransDateList As List(Of Date)
    Public Property TransDateList() As List(Of Date)
        Get
            Return _TransDateList
        End Get
        Set(ByVal value As List(Of Date))
            _TransDateList = value
        End Set
    End Property
#End Region

#Region "Prudential Refund List"
    Private _PrudentialRefund As PrudentialRefund
    Public Property PrudentialRefund() As PrudentialRefund
        Get
            Return _PrudentialRefund
        End Get
        Set(ByVal value As PrudentialRefund)
            _PrudentialRefund = value
        End Set
    End Property
#End Region

#Region "Prudential Details List"
    Private _PrudentialDetailsList As List(Of PrudentialRefundDetails)
    Public Property PrudentialDetailsList() As List(Of PrudentialRefundDetails)
        Get
            Return _PrudentialDetailsList
        End Get
        Set(ByVal value As List(Of PrudentialRefundDetails))
            _PrudentialDetailsList = value
        End Set
    End Property
#End Region

#Region "JV List"
    Private _PRJVsList As List(Of JournalVoucher)
    Public Property PRJVsList() As List(Of JournalVoucher)
        Get
            Return _PRJVsList
        End Get
        Set(ByVal value As List(Of JournalVoucher))
            _PRJVsList = value
        End Set
    End Property
#End Region

#Region "FTF"
    Private _PRFTF As FundTransferFormMain
    Public Property PRFTF() As FundTransferFormMain
        Get
            Return _PRFTF
        End Get
        Set(ByVal value As FundTransferFormMain)
            _PRFTF = value
        End Set
    End Property
#End Region

#Region "EFT"
    Private _PRRefundEFT As List(Of EFT)
    Public Property PRRefundEFT() As List(Of EFT)
        Get
            Return _PRRefundEFT
        End Get
        Set(ByVal value As List(Of EFT))
            _PRRefundEFT = value
        End Set
    End Property

#End Region
    Private seqJVno As Integer
    Public Sub New()
        'Get the current instance of the dal
        Me._DataAccess = DAL.GetInstance()
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._BFactory = BusinessFactory.GetInstance
        Me._PrudentialRefund = New PrudentialRefund
        Me._PrudentialDetailsList = New List(Of PrudentialRefundDetails)
    End Sub

#Region "Initialize as Viewer"
    Public Sub initializeView()
        Me.GetTransDateList()
    End Sub
#End Region

#Region "GetSystemDate"
    Public Function GetSystemDate() As Date
        Dim ret As Date
        ret = Me.WBillHelper.GetSystemDate()
        Return ret
    End Function
#End Region

#Region "GetTransaDateList"
    Private Sub GetTransDateList()
        Dim report As New DataReport

        Try

            Dim SQL As String = "SELECT * FROM AM_PRUDENTIAL_REFUND_MAIN  ORDER BY TRANS_DATE DESC"

            Me.DataAccess.ConnectionString = AMModule.ConnectionString
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            Me.GetTransDateList(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub
    Private Sub GetTransDateList(ByVal dr As IDataReader)
        Dim resultObj As New List(Of Date)
        Dim index As Integer = 0

        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As New Date
                    item = CDate(.Item("TRANS_DATE").ToString())                    
                    resultObj.Add(item)
                End With
            End While
            Me._TransDateList = resultObj
        Catch ex As Exception
            Throw New ApplicationException("Error in row " & index & " --- " & ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try
    End Sub
#End Region

#Region "Get Refund Prudential"
    Public Sub GetPrudentialByTransDate(ByVal transadate As Date)
        Me._PrudentialRefund = New PrudentialRefund
        Me._PrudentialDetailsList = New List(Of PrudentialRefundDetails)
        Me._PRJVsList = New List(Of JournalVoucher)
        Me._PRFTF = New FundTransferFormMain

        Me.GetPRRefMain(transadate)
        Me.GetPRRefDetails()
        Me.GetPRRefJVList()
        Me.GetFTF()
    End Sub

    Private Sub GetPRRefMain(ByVal transdate As Date)
        Dim report As New DataReport

        Try

            Dim SQL As String = "SELECT * FROM AM_PRUDENTIAL_REFUND_MAIN WHERE TRANS_DATE = TO_DATE('" & transdate & "','MM/DD/YYYY')"

            Me.DataAccess.ConnectionString = AMModule.ConnectionString
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            Me.GetPRRefMain(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub
    Private Sub GetPRRefMain(ByVal dr As IDataReader)
        Dim resultObj As New PrudentialRefund
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As New PrudentialRefund
                    item.TransactionNo = CLng(.Item("TRANS_NO").ToString())
                    item.TransactionDate = CDate(.Item("TRANS_DATE").ToString())
                    resultObj = item
                End With
            End While
            Me._PrudentialRefund = resultObj
        Catch ex As Exception
            Throw New ApplicationException("Error in row " & index & " --- " & ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try
    End Sub

    Private Sub GetPRRefDetails()
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT A.*, B.PARTICIPANT_ID, B.FULL_NAME FROM AM_PRUDENTIAL_REFUND_DETAILS A " & _
                            "LEFT JOIN AM_PARTICIPANTS B ON B.ID_NUMBER = A.ID_NUMBER " & _
                            "WHERE A.TRANS_NO = " & Me.PrudentialRefund.TransactionNo

            Me.DataAccess.ConnectionString = AMModule.ConnectionString
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            Me.GetPRRefDetails(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub

    Private Sub GetPRRefDetails(ByVal dr As IDataReader)
        Dim resultObj As New List(Of PrudentialRefundDetails)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As New PrudentialRefundDetails
                    item.IDNumber = CStr(.Item("ID_NUMBER").ToString())
                    item.ParticipantID = CStr(.Item("PARTICIPANT_ID").ToString())
                    item.FullName = CStr(.Item("FULL_NAME").ToString())
                    item.PRAmount = CDec(.Item("PR_AMOUNT").ToString())
                    item.AmountRefund = CDec(.Item("AMOUNT_REFUND").ToString())
                    item.InterestAmount = CDec(.Item("INTEREST_AMOUNT").ToString())
                    resultObj.Add(item)
                End With
            End While
            Me._PrudentialRefund.PRRefundDetails = resultObj
        Catch ex As Exception
            Throw New ApplicationException("Error in row " & index & " --- " & ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try
    End Sub

    Private Sub GetPRRefJVList()
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT A.*, B.* FROM AM_PRUDENTIAL_REFUND_JV A " & _
                            "LEFT JOIN AM_JV B ON B.AM_JV_NO = A.JV_NO " & _
                            "WHERE A.TRANS_NO = " & Me.PrudentialRefund.TransactionNo

            Me.DataAccess.ConnectionString = AMModule.ConnectionString
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            Me.GetPRRefJVList(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub

    Private Sub GetPRRefJVList(ByVal dr As IDataReader)
        Dim resultObj As New List(Of JournalVoucher)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As New JournalVoucher
                    item.JVDate = CDate(.Item("AM_JV_DATE"))
                    item.JVNumber = CLng(.Item("AM_JV_NO"))
                    item.BatchCode = CStr(.Item("BATCH_CODE").ToString)
                    item.Status = CInt(.Item("STATUS"))
                    item.PreparedBy = CStr(.Item("PREPARED_BY"))
                    If Not IsDBNull(.Item("APPROVED_BY")) Then
                        item.ApprovedBy = CStr(.Item("APPROVED_BY")).Trim
                    Else
                        item.ApprovedBy = ""
                    End If
                    item.CheckedBy = CStr(.Item("CHECKED_BY"))
                    item.PostedType = CStr(.Item("POSTED_TYPE"))
                    item.UpdatedDate = CDate(.Item("UPDATED_DATE"))
                    item.UpdatedDate = CDate(FormatDateTime(item.UpdatedDate, DateFormat.ShortDate))
                    item.UpdatedBy = CStr(.Item("UPDATED_BY"))
                    item.JVDetails = GetPRRefJVDetailsList(item.JVNumber)
                    resultObj.Add(item)
                End With
            End While
            Me._PRJVsList = resultObj
        Catch ex As Exception
            Throw New ApplicationException("Error in row " & index & " --- " & ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try
    End Sub

    Private Function GetPRRefJVDetailsList(ByVal jvNO As Long) As List(Of JournalVoucherDetails)
        Dim result As New List(Of JournalVoucherDetails)
        Dim report As New DataReport

        Try

            Dim SQL As String = "SELECT * FROM AM_JV_DETAILS WHERE AM_JV_NO = " & jvNO

            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.GetPRRefJVDetailsList(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return result
    End Function
    
    Private Function GetPRRefJVDetailsList(ByVal dr As IDataReader) As List(Of JournalVoucherDetails)
        Dim result As New List(Of JournalVoucherDetails)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As New JournalVoucherDetails
                    item.JVNumber = CLng(.Item("AM_JV_NO"))
                    item.AccountCode = CStr(.Item("ACCT_CODE"))
                    item.Debit = CDec(.Item("DEBIT"))
                    item.Credit = CDec(.Item("CREDIT"))
                    item.UpdatedDate = CDate(.Item("UPDATED_DATE"))
                    item.UpdatedBy = CStr(.Item("UPDATED_BY").ToString)
                    result.Add(item)
                End With
            End While
        Catch ex As Exception
            Throw New ApplicationException("Error in row " & index & " --- " & ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return result
    End Function

    Private Sub GetFTF()
        Dim ret As New FundTransferFormMain
        Dim report As New DataReport
        Dim getJVBatchCode As String = (From x In Me.PRJVsList Where x.PostedType = EnumPostedType.PRREFFTF.ToString Select x.BatchCode).FirstOrDefault

        Try
            Dim SQL As String = "SELECT A.* FROM AM_FTF_MAIN A " _
                                & "WHERE BATCH_CODE = '" & getJVBatchCode & "'"

            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            Me._PRFTF = Me.GetFTF(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub
    Private Function GetFTF(ByVal dr As IDataReader) As FundTransferFormMain
        Dim ret As New FundTransferFormMain
        Try
            While dr.Read()
                With dr
                    Dim item As New FundTransferFormMain
                    item.AllocationDate = CDate(FormatDateTime(CDate(.Item("ALLOCATION_DATE")), DateFormat.ShortDate))
                    item.BatchCode = CStr(.Item("BATCH_CODE"))
                    item.RefNo = CInt(.Item("REF_NO"))
                    item.DRDate = CDate(FormatDateTime(CDate(.Item("DR_DATE")), DateFormat.ShortDate))
                    item.CRDate = CDate(FormatDateTime(CDate(.Item("CR_DATE")), DateFormat.ShortDate))
                    item.TotalAmount = CDec(.Item("TOTAL_AMOUNT"))
                    item.TransType = CType(.Item("TRANS_TYPE"), EnumFTFTransType)
                    item.Status = CType(.Item("STATUS"), EnumStatus)
                    item.IsPosted = CType(.Item("IS_POSTED"), EnumIsPosted)
                    item.RequestingApproval = CStr(.Item("REQUESTING_APPROVAL"))
                    item.PreparedBy = CStr(.Item("UPDATED_BY"))
                    item.ApprovedBy = CStr(.Item("APPROVED_BY"))
                    item.ListOfFTFParticipants = Me.GetFTFParticipant(item.RefNo)
                    item.ListOfFTFDetails = Me.GetFTFDetails(item.RefNo)
                    ret = item

                End With
            End While
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try
        Return ret
    End Function

    Public Function GetFTFParticipant(ByVal RefNumber As Long) As List(Of FundTransferFormParticipant)
        Dim ret As New List(Of FundTransferFormParticipant)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.*, B.PARTICIPANT_ID FROM AM_FTF_PARTICIPANT A " _
                                & "LEFT JOIN AM_PARTICIPANTS B ON A.ID_NUMBER = B.ID_NUMBER " _
                                & "WHERE A.REF_NO = " & RefNumber

            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            ret = Me.GetFTFParticipant(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return ret
    End Function

    Private Function GetFTFParticipant(ByVal dr As IDataReader) As List(Of FundTransferFormParticipant)
        Dim ret As New List(Of FundTransferFormParticipant)
        Try
            While dr.Read()
                With dr
                    Dim item As New FundTransferFormParticipant
                    item.RefNo = CLng(.Item("REF_NO"))
                    item.IDNumber = New AMParticipants(CStr(.Item("ID_NUMBER")), CStr(.Item("PARTICIPANT_ID")))
                    item.Amount = CDec(.Item("AMOUNT"))
                    ret.Add(item)
                    ret.TrimExcess()
                End With
            End While
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try
        Return ret
    End Function

    Public Function GetFTFDetails(ByVal RefNumber As Long) As List(Of FundTransferFormDetails)
        Dim result As New List(Of FundTransferFormDetails)
        Dim report As New DataReport

        Try
            Dim SQL As String = "SELECT A.* FROM AM_FTF_DETAILS A " _
                                & "WHERE REF_NO = " & RefNumber


            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.GetFTFDetails(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function

    Private Function GetFTFDetails(ByVal dr As IDataReader) As List(Of FundTransferFormDetails)
        Dim ret As New List(Of FundTransferFormDetails)
        Try
            While dr.Read()
                With dr
                    Dim item As New FundTransferFormDetails
                    item.RefNo = CLng(.Item("REF_NO"))
                    item.BankAccountNo = CStr(.Item("BANK_ACCNT_NO").ToString)
                    item.Debit = CDec(.Item("DEBIT"))
                    item.Credit = CDec(.Item("CREDIT"))
                    ret.Add(item)
                    ret.TrimExcess()
                End With
            End While
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return ret
    End Function
#End Region


#Region "Get Prudential List"
    Public Sub GetPrudentialList()
        Dim report As New DataReport

        Try

            Dim SQL As String = "SELECT A.ID_NUMBER, B.PARTICIPANT_ID, B.FULL_NAME, A.PRUDENTIAL_AMOUNT, A.INTEREST_AMOUNT FROM AM_PRUDENTIAL A " _
                                & "LEFT JOIN AM_PARTICIPANTS B ON B.ID_NUMBER = A.ID_NUMBER " _
                                & "WHERE A.PRUDENTIAL_AMOUNT <> 0 ORDER BY B.PARTICIPANT_ID"

            Me.DataAccess.ConnectionString = AMModule.ConnectionString
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            Me.GetPrudentialList(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub

    Private Sub GetPrudentialList(ByVal dr As IDataReader)
        Dim resultObj As New List(Of PrudentialRefundDetails)
        Dim index As Integer = 0

        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As New PrudentialRefundDetails
                    item.IDNumber = CStr(.Item("ID_NUMBER").ToString())
                    item.ParticipantID = CStr(.Item("PARTICIPANT_ID").ToString())
                    item.FullName = CStr(.Item("FULL_NAME").ToString())
                    item.PRAmount = CDec(.Item("PRUDENTIAL_AMOUNT").ToString())
                    item.InterestAmount = CDec(.Item("INTEREST_AMOUNT").ToString())
                    item.AmountRefund = 0
                    resultObj.Add(item)
                End With
            End While
            Me._PrudentialDetailsList = resultObj
        Catch ex As Exception
            Throw New ApplicationException("Error in row " & index & " --- " & ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try
    End Sub
#End Region

#Region "Check Transaction Date"
    Public Function GetMaxPaymentRemitDate() As AllocationDate
        Dim result As New AllocationDate
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT ALLOCATION_DATE, PAYMENT_NO, REMITTANCE_DATE FROM AM_PAYMENT_NEW WHERE REMITTANCE_DATE = (SELECT MAX(REMITTANCE_DATE) FROM AM_PAYMENT_NEW)"

            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.GetPayAllocDateList(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return result

    End Function

    Private Function GetPayAllocDateList(ByVal reader As IDataReader) As AllocationDate
        Dim result As New AllocationDate
        Dim cnt As Integer = 0
        Try
            While reader.Read()
                With reader
                    result = New AllocationDate(CDate(FormatDateTime(CDate(.Item("ALLOCATION_DATE")))), CLng(.Item("PAYMENT_NO")), CDate(FormatDateTime(CDate(.Item("REMITTANCE_DATE")))))
                End With
            End While
        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not reader.IsClosed Then
                reader.Close()
            End If
        End Try
        Return result
    End Function

#End Region

#Region "Create Transaction"
    Public Sub CreateTransaction(ByVal transDate As Date)
        Dim iniPRRefund As PrudentialRefund = New PrudentialRefund
        Me.GetTransDateList()

        Dim countTransDate As Integer = Me.TransDateList.Where(Function(x) x = transDate).Count()


        If countTransDate > 0 Then
            Throw New Exception("Selected date is already exist! Please choose another date.")
            Exit Sub
        End If

        Me.GetPrudentialList()
        Me._PrudentialRefund = New PrudentialRefund

        iniPRRefund.TransactionDate = transDate
        iniPRRefund.PRRefundDetails = New List(Of PrudentialRefundDetails)
        Me.PrudentialRefund = iniPRRefund
        Me._PRJVsList = New List(Of JournalVoucher)
        Me._PRFTF = New FundTransferFormMain
        Me._PRRefundEFT = New List(Of EFT)
    End Sub
#End Region

#Region "Update Transaction Details"
    Public Sub UpdateTransactionDetails(ByVal oPRDetails As PrudentialRefundDetails)        
        If Me.PrudentialRefund.PRRefundDetails.Count = 0 Then
            If oPRDetails.AmountRefund < 1000 Then
                Throw New Exception("Invalid amount! Refund should be greater or equal to 1000.")
            End If
            Me.PrudentialRefund.PRRefundDetails.Add(oPRDetails)
        Else
            Dim getPRDetails As PrudentialRefundDetails = (From x In Me.PrudentialRefund.PRRefundDetails Where x.IDNumber = oPRDetails.IDNumber Select x).FirstOrDefault
            If Not getPRDetails Is Nothing Then
                If oPRDetails.PRAmount = 0 Then
                    Me.PrudentialRefund.PRRefundDetails.Remove(Me.PrudentialRefund.PRRefundDetails.Single(Function(x) x.IDNumber = oPRDetails.IDNumber))
                ElseIf oPRDetails.AmountRefund < 1000 Then
                    Throw New Exception("Invalid amount! Refund should be greater or equal to 1000.")                
                Else
                    getPRDetails.PRAmount = oPRDetails.PRAmount
                End If
            Else
                If oPRDetails.AmountRefund < 1000 Then
                    Throw New Exception("Invalid amount! Refund should be greater or equal to 1000.")
                End If
                Me.PrudentialRefund.PRRefundDetails.Add(oPRDetails)
            End If
        End If

        Me.UpdateJVFTF()
        Me.UpdateJVClosing()
        Me.UpdateJVPREFT()
        Me.UpdateFTFPREFT()
    End Sub
#End Region

#Region "Update JV FTF Setup"
    Public Sub UpdateJVFTF()
        If Me.PRJVsList.Count > 0 Then
            If Me.PRJVsList.Where(Function(x) x.PostedType = EnumPostedType.PRREFFTF.ToString).Count > 0 Then
                Me.PRJVsList.Remove(Me.PRJVsList.Single(Function(x) x.PostedType = EnumPostedType.PRREFFTF.ToString))
            End If
        End If

        Dim initialJVFTF As JournalVoucher = New JournalVoucher
        Dim JVSign As DocSignatories = WBillHelper.GetSignatories("JV").First
        seqJVno += 1
        With initialJVFTF
            .JVNumber = seqJVno
            .JVDate = Me.PrudentialRefund.TransactionDate
            .BatchCode = EnumPostedType.PRREFFTF.ToString & "-" & seqJVno
            .PostedType = EnumPostedType.PRREFFTF.ToString
            .Status = EnumPostedTypeStatus.NotPosted
            .Remarks = "Refund of Prudential  - FTF Setup"
            Dim sumOfPRRef As Decimal = (From x In Me.PrudentialRefund.PRRefundDetails Select x.AmountRefund).Sum()
            .JVDetails.Add(New JournalVoucherDetails(seqJVno, AMModule.CashInbankSettlementcode, sumOfPRRef, 0))
            .JVDetails.Add(New JournalVoucherDetails(seqJVno, AMModule.CashinBankPrudentialCode, 0, sumOfPRRef))
            .UpdatedBy = AMModule.UserName
            .PreparedBy = AMModule.FullName
            .CheckedBy = JVSign.Signatory_1
            .ApprovedBy = JVSign.Signatory_2
        End With
        Me.PRJVsList.Add(initialJVFTF)
    End Sub
#End Region

#Region "Update JV Closing"
    Public Sub UpdateJVClosing()
        If Me.PRJVsList.Count > 0 Then
            If Me.PRJVsList.Where(Function(x) x.PostedType = EnumPostedType.PRREF.ToString).Count > 0 Then
                Me.PRJVsList.Remove(Me.PRJVsList.Single(Function(x) x.PostedType = EnumPostedType.PRREF.ToString))
            End If
        End If
        Dim initialJVPR As JournalVoucher = New JournalVoucher
        Dim JVSign As DocSignatories = WBillHelper.GetSignatories("JV").First
        seqJVno += 1
        With initialJVPR
            .JVNumber = seqJVno
            .JVDate = Me.PrudentialRefund.TransactionDate
            .BatchCode = EnumPostedType.PRREF.ToString & "-" & seqJVno
            .PostedType = EnumPostedType.PRREF.ToString
            .Status = EnumPostedTypeStatus.NotPosted
            .Remarks = "Refund of Prudential  - Closing"
            Dim sumOfPRRef As Decimal = (From x In Me.PrudentialRefund.PRRefundDetails Select x.AmountRefund).Sum()
            .JVDetails.Add(New JournalVoucherDetails(seqJVno, AMModule.PRWESMCode, sumOfPRRef, 0))
            .JVDetails.Add(New JournalVoucherDetails(seqJVno, AMModule.ClearingAccountCode, 0, sumOfPRRef))
            .UpdatedBy = AMModule.UserName
            .PreparedBy = AMModule.FullName
            .CheckedBy = JVSign.Signatory_1
            .ApprovedBy = JVSign.Signatory_2
        End With
        Me.PRJVsList.Add(initialJVPR)
    End Sub
#End Region

#Region "Update JV EFT"
    Public Sub UpdateJVPREFT()
        If Me.PRJVsList.Count > 0 Then
            If Me.PRJVsList.Where(Function(x) x.PostedType = EnumPostedType.PRREFEFT.ToString).Count > 0 Then
                Me.PRJVsList.Remove(Me.PRJVsList.Single(Function(x) x.PostedType = EnumPostedType.PRREFEFT.ToString))
            End If
        End If

        Dim initialJVPREFT As JournalVoucher = New JournalVoucher
        Dim JVSign As DocSignatories = WBillHelper.GetSignatories("JV").First
        seqJVno += 1
        With initialJVPREFT
            .JVNumber = seqJVno
            .JVDate = Me.PrudentialRefund.TransactionDate
            .BatchCode = EnumPostedType.PRREFEFT.ToString & "-" & seqJVno
            .PostedType = EnumPostedType.PRREFEFT.ToString
            .Status = EnumPostedTypeStatus.NotPosted
            .Remarks = "Refund of Prudential  - EFT"
            Dim sumOfPRRef As Decimal = (From x In Me.PrudentialRefund.PRRefundDetails Select x.AmountRefund).Sum()
            .JVDetails.Add(New JournalVoucherDetails(seqJVno, AMModule.ClearingAccountCode, sumOfPRRef, 0))
            .JVDetails.Add(New JournalVoucherDetails(seqJVno, AMModule.CashInbankSettlementcode, 0, sumOfPRRef))
            .UpdatedBy = AMModule.UserName
            .PreparedBy = AMModule.FullName
            .CheckedBy = JVSign.Signatory_1
            .ApprovedBy = JVSign.Signatory_2
        End With
        Me.PRJVsList.Add(initialJVPREFT)
    End Sub
#End Region

#Region "Update FTF Report"
    Public Sub UpdateFTFPREFT()
        Dim ret As New FundTransferFormMain

        If Me.PrudentialRefund.PRRefundDetails.Count = 0 Then
            Me.PRFTF = New FundTransferFormMain
            Exit Sub
        End If

        Dim Signatory = WBillHelper.GetSignatories("FTF").First()
        Using _FTFMain As New FundTransferFormMain
            With _FTFMain
                .DRDate = CDate(Me.PrudentialRefund.TransactionDate.ToShortDateString)
                .CRDate = CDate(Me.PrudentialRefund.TransactionDate.ToShortDateString)
                .RefNo = 1
                .TransType = EnumFTFTransType.PRRefund
                .AllocationDate = CDate(Me.PrudentialRefund.TransactionDate.ToShortDateString)
                Dim sumOfPRRef As Decimal = (From x In Me.PrudentialRefund.PRRefundDetails Select x.AmountRefund).Sum()
                Dim getParticipants As List(Of AMParticipants) = Me.WBillHelper.GetAMParticipants()

                For Each item In Me.PrudentialRefund.PRRefundDetails
                    Dim itemParticipant As AMParticipants = (From x In getParticipants Where x.IDNumber = item.IDNumber Select x).FirstOrDefault
                    If item.AmountRefund > 0 Then
                        .ListOfFTFParticipants.Add(New FundTransferFormParticipant(.RefNo, itemParticipant, Math.Abs(item.AmountRefund)))
                    End If
                Next

                .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, AMModule.CashInbankSettlementcode, AMModule.SettlementBankAccountNo, sumOfPRRef, 0))
                .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, AMModule.CashinBankPrudentialCode, AMModule.PrudentialBankAccountNo, 0, sumOfPRRef))
                .PreparedBy = AMModule.FullName
                .RequestingApproval = Signatory.Signatory_1
                .ApprovedBy = Signatory.Signatory_2
                .TotalAmount = sumOfPRRef
                .Status = EnumStatus.Active
            End With
            Me.PRFTF = _FTFMain
        End Using

    End Sub
#End Region

#Region "Function For Generation of Journal Voucher"

    Public Function GenerateJVReport(ByVal PostedType As EnumPostedType) As DataSet
        Dim ret As New DataSet
        Dim Accountingcode = WBillHelper.GetAccountingCodes()
        Dim JV_Signatories = WBillHelper.GetSignatories("JV").First
        Dim AllGPPosted = WBillHelper.GetWESMBillGPPosted()

        If Me.PRJVsList Is Nothing Then Throw New Exception("No JV available, please check the transaction.")

        Dim JVData = (From x In Me.PRJVsList Where x.PostedType = PostedType.ToString Select x).FirstOrDefault

        If JVData Is Nothing Then
            Return ret
        End If
        Dim JournalVoucherTable As New DataTable

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
        With JVData
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

        For Each rowJVDetails In JVData.JVDetails
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

#Region "Save Refund"
    Public Sub SaveRefund()
        Dim report As New DataReport
        Dim ListofSQL As New List(Of String)
        Try
            ListofSQL = Me.CreateSQLStatement
            report = Me.DataAccess.ExecuteSaveQuery(ListofSQL, New DataSet)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            ListofSQL = CreateSQLStatementNewForSeq(dicListofSeq)
            report = Me.DataAccess.ExecuteSaveQuery(ListofSQL, New DataSet)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
        Catch ex As Exception
            ListofSQL = Nothing
            Throw New ApplicationException(ex.Message)
        Finally
            ListofSQL = Nothing
        End Try
    End Sub
#End Region
#Region "Create SQL Queries"
    Private Function CreateSQLStatement() As List(Of String)
        Dim listSQL As New List(Of String)
        Dim SQL As String
        Dim SysDateTime As Date = WBillHelper.GetSystemDateTime()
        Dim UpdatedBy As String = AMModule.UserName
        Dim PRRefundNo As Long

        Dim dicJVNo As New Dictionary(Of String, Long)
        Dim dicJVBatchNo As New Dictionary(Of String, Long)
        Dim ListofSequenceUsed As New List(Of String)

        ListofSequenceUsed.Add("SEQ_AM_PR_REF_NO")
        ListofSequenceUsed.Add("SEQ_AM_BATCH_CODE")
        ListofSequenceUsed.Add("SEQ_AM_JV_NO")
        ListofSequenceUsed.Add("SEQ_AM_FTF_REF_NO")

        dicListofSeq = New Dictionary(Of String, Long)
        dicListofSeq = WBillHelper.GetMaxSequenceID(ListofSequenceUsed)

        PRRefundNo = dicListofSeq("SEQ_AM_PR_REF_NO")
        dicListofSeq("SEQ_AM_PR_REF_NO") += 1

        SysDateTime = WBillHelper.GetSystemDateTime()
        UpdatedBy = AMModule.UserName

        'Insert AM_JV
        Dim ListofJVNumber As New List(Of Long)
        For Each item In Me.PRJVsList            
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
                If ItemPostedType = EnumPostedType.PRREFEFT Then
                    'AM_CHECK
                    For Each Checkitem In Me.PRRefundEFT.Where(Function(x) x.PaymentType = EnumParticipantPaymentType.Check)
                        With Checkitem
                            SQL = "INSERT INTO AM_CHECKS (TRANSACTION_DATE, ID_NUMBER, AMOUNT, CHECK_NUMBER, CHECK_TYPE, CV_NUMBER, UPDATED_BY, STATUS, REMARKS, BATCH_CODE) " & _
                                  "SELECT TO_DATE('" & .AllocationDate & "','MM/DD/YYYY'),'" & _
                                            .Participant.IDNumber & "'," & .PrudentialRefund & ", '', '', '', '" & _
                                            AMModule.UserName & "'," & "1, '" & "Prudential Security Refund" & "', '" & _
                                            ItemPostedType.ToString & "-" & BatchCode & "' FROM DUAL"
                            listSQL.Add(SQL)
                        End With
                    Next
                End If

                'AM_PRUDENTIAL_REFUND_JV
                SQL = "INSERT INTO AM_PRUDENTIAL_REFUND_JV (TRANS_NO, JV_NO, POSTED_TYPE) " & _
                    "SELECT " & PRRefundNo & ", " & JVNumber & ", '" & item.PostedType.ToString() & "' FROM DUAL"
                listSQL.Add(SQL)

                'WESMBillGPPosted
                SQL = "INSERT INTO AM_WESM_BILL_GP_POSTED (BILLING_PERIOD, STL_RUN, CHARGE_TYPE, DUE_DATE, REMARKS, POSTED, UPDATED_BY, BATCH_CODE, GP_REFNO, POSTED_TYPE, DOCUMENT_AMOUNT, AM_JV_NO) " & _
                      "SELECT '','','','','" & .Remarks & "', '0', '" & _
                                    .UpdatedBy & "', '" & _
                                    ItemPostedType.ToString & "-" & BatchCode & "', '', '" & _
                                    ItemPostedType.ToString & "', '" & _
                                    Math.Round((From x In .JVDetails Select x.Debit).Sum, 2) & "', '" & _
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

        'AM_FTF_MAIN        
        Dim FTFReferenceNo As Long = 0
        With Me.PRFTF            
            Dim JVBatchCode As String = ""
            Dim ReferenceNo As Long = dicListofSeq.Item("SEQ_AM_FTF_REF_NO")
            .RefNo = ReferenceNo

            JVBatchCode = EnumPostedType.PRREFFTF.ToString & "-" & CStr(dicJVBatchNo(EnumPostedType.PRREFFTF.ToString))
            FTFReferenceNo = ReferenceNo

            SQL = "INSERT INTO AM_FTF_MAIN (ALLOCATION_DATE, BATCH_CODE, REF_NO, DR_DATE, CR_DATE, TOTAL_AMOUNT, " & _
                                                        "TRANS_TYPE, STATUS, IS_POSTED, REQUESTING_APPROVAL, APPROVED_BY, UPDATED_BY, UPDATED_DATE) " & _
                  "SELECT TO_DATE('" & .AllocationDate.ToShortDateString & "','MM/DD/YYYY'), '" & JVBatchCode & "', '" & ReferenceNo & "', " & _
                                "TO_DATE('" & .DRDate.ToString("MM/dd/yyyy") & "','MM/DD/YYYY'), TO_DATE('" & .CRDate.ToString("MM/dd/yyyy") & "','MM/DD/YYYY'), '" & .TotalAmount & "', " & _
                                "'" & .TransType & "', '" & .Status & "', '" & .IsPosted & "', '" & .RequestingApproval & "', '" & .ApprovedBy & "', " & _
                                "'" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') FROM DUAL"
            listSQL.Add(SQL)

            For Each ItemFTF In .ListOfFTFDetails
                SQL = "INSERT INTO AM_FTF_DETAILS (REF_NO, ACCT_CODE, BANK_ACCNT_NO, ISSUING_BANK, RECEIVING_BANK, DEBIT, CREDIT, PARTICULARS, UPDATED_BY, UPDATED_DATE) " & _
                      "SELECT '" & ReferenceNo & "', '" & ItemFTF.AccountCode & "', '" & ItemFTF.BankAccountNo & "', '" & ItemFTF.IssuingBank & "', '" & ItemFTF.ReceivingBank & "', " & _
                             "'" & ItemFTF.Debit & "', '" & ItemFTF.Credit & "', '" & ItemFTF.Particulars & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') FROM DUAL"
                listSQL.Add(SQL)
            Next

            For Each ItemParticipants In .ListOfFTFParticipants
                SQL = "INSERT INTO AM_FTF_PARTICIPANT (REF_NO, ID_NUMBER, AMOUNT, UPDATED_BY, UPDATED_DATE) " & _
                      "SELECT '" & ReferenceNo & "', '" & ItemParticipants.IDNumber.IDNumber & "', " & _
                              "'" & ItemParticipants.Amount & "', '" & UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') FROM DUAL"
                listSQL.Add(SQL)
            Next
            dicListofSeq("SEQ_AM_FTF_REF_NO") += 1
        End With

        'Insert AM_PRUDENTIAL_REFUND_MAIN
        With Me.PrudentialRefund
            SQL = "INSERT INTO AM_PRUDENTIAL_REFUND_MAIN (TRANS_NO, TRANS_DATE, UPDATED_BY, UPDATED_DATE) " & _
                "SELECT " & PRRefundNo & ", TO_DATE('" & .TransactionDate & "','MM/DD/YYYY'), '" & AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss')  FROM DUAL"
            listSQL.Add(SQL)

            For Each item In .PRRefundDetails

                'Insert AM_PRUDENTIAL_REFUND_DETAILS
                SQL = "INSERT INTO AM_PRUDENTIAL_REFUND_DETAILS (TRANS_NO, ID_NUMBER, PR_AMOUNT, INTEREST_AMOUNT, AMOUNT_REFUND) " & _
                "SELECT " & PRRefundNo & ", '" & item.IDNumber & "', " & item.PRAmount & ", " & item.InterestAmount & ", " & item.AmountRefund & " FROM DUAL"
                listSQL.Add(SQL)

                Dim JVBatchCode As String = EnumPostedType.PRREF.ToString & "-" & CStr(dicJVBatchNo(EnumPostedType.PRREF.ToString))

                'AM_PRUDENTIAL
                SQL = "UPDATE AM_PRUDENTIAL SET PRUDENTIAL_AMOUNT = PRUDENTIAL_AMOUNT - " & item.AmountRefund & _
                     ",UPDATED_BY = '" & AMModule.UserName & "',UPDATED_DATE = " & "TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') " & _
                     "WHERE ID_NUMBER = '" & item.IDNumber & "'"
                listSQL.Add(SQL)

                'AM_PRUDENTIAL_HISTORY
                SQL = "INSERT INTO AM_PRUDENTIAL_HISTORY (ID_NUMBER, AMOUNT, TRANS_TYPE, TRANS_DATE, UPDATED_BY, UPDATED_DATE, BATCH_CODE, FTF_REF_NO) " & _
                      "SELECT '" & item.IDNumber & "', " & item.AmountRefund & ", " & EnumPrudentialTransType.PRRefund & ", " & "TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss') " & _
                                ", '" & AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & JVBatchCode & _
                                "', " & FTFReferenceNo & " FROM DUAL"
                listSQL.Add(SQL)
            Next
        End With


        Return listSQL

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


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
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
