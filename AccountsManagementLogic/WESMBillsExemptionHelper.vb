Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementDataAccess


Public Class WESMBillsExemptionHelper
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

    Private _WESMBillsExemptionList As List(Of WESMBillsExemption)
    Public Property WESMBillsExemptionList() As List(Of WESMBillsExemption)
        Get
            Return _WESMBillsExemptionList
        End Get
        Set(ByVal value As List(Of WESMBillsExemption))
            _WESMBillsExemptionList = value
        End Set
    End Property

    Private _WESMBillsExemptionListWithUpdate As List(Of WESMBillsExemption)
    Public Property WESMBillsExemptionListWithUpdate() As List(Of WESMBillsExemption)
        Get
            Return _WESMBillsExemptionListWithUpdate
        End Get
        Set(ByVal value As List(Of WESMBillsExemption))
            _WESMBillsExemptionListWithUpdate = value
        End Set
    End Property

    Public Sub New()
        'Get the current instance of the dal
        Me._DataAccess = DAL.GetInstance()
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._WESMBillsExemptionListWithUpdate = New List(Of WESMBillsExemption)
        Me._WESMBillsExemptionList = New List(Of WESMBillsExemption)
    End Sub

    Public Sub UpdateWESMBillExemptionForNoOffset(ByVal wesmbillSummaryNo As Long, ByVal noOffset As Boolean)
        Dim getWESMBillsExemptionItem As WESMBillsExemption = WESMBillsExemptionList.Where(Function(x) x.WESMBillSummaryNo = wesmbillSummaryNo).FirstOrDefault        
        If WESMBillsExemptionListWithUpdate.Count = 0 Then
            If Not getWESMBillsExemptionItem.NoOffsetOrig = noOffset Then
                With getWESMBillsExemptionItem
                    .NoOffset = noOffset
                    WESMBillsExemptionListWithUpdate.Add(New WESMBillsExemption(.WESMBillSummaryNo, .IDNumber, .ParticipantName,
                                                                                .WESMBillBatchNo, .BillPeriod, .ChargeType, .InvoiceNo,
                                                                                .OrigDueDate, .NewDueDate, .BeginningBalance, .EndingBalance,
                                                                                noOffset, .NoOffsetOrig, .NoSOA, .NoSOAOrig,
                                                                                .NoDefaultInterest, .NoDefaultInterestOrig))
                End With
            End If
        Else
            Dim getWEMSBillsExemptionItemWithUpdate As WESMBillsExemption = WESMBillsExemptionListWithUpdate.Where(Function(x) x.WESMBillSummaryNo = wesmbillSummaryNo).FirstOrDefault
            If getWEMSBillsExemptionItemWithUpdate Is Nothing Then
                With getWESMBillsExemptionItem
                    .NoOffset = noOffset
                    WESMBillsExemptionListWithUpdate.Add(New WESMBillsExemption(.WESMBillSummaryNo, .IDNumber, .ParticipantName,
                                                                                .WESMBillBatchNo, .BillPeriod, .ChargeType, .InvoiceNo,
                                                                                .OrigDueDate, .NewDueDate, .BeginningBalance, .EndingBalance,
                                                                                noOffset, .NoOffsetOrig, .NoSOA, .NoSOAOrig,
                                                                                .NoDefaultInterest, .NoDefaultInterestOrig))
                End With
            Else
                With getWEMSBillsExemptionItemWithUpdate
                    .NoOffset = noOffset
                    If .NoOffset = .NoOffsetOrig And .NoSOA = .NoSOAOrig And .NoDefaultInterest = .NoDefaultInterestOrig Then
                        Me.WESMBillsExemptionListWithUpdate.RemoveAll(Function(x) x.WESMBillSummaryNo = wesmbillSummaryNo)
                    End If
                End With
                With getWESMBillsExemptionItem
                    .NoOffset = noOffset
                End With
            End If            
        End If
    End Sub


    Public Sub UpdateWESMBillExemptionForNoSOA(ByVal wesmbillSummaryNo As Long, ByVal noSOA As Boolean)
        Dim getWESMBillsExemptionItem As WESMBillsExemption = WESMBillsExemptionList.Where(Function(x) x.WESMBillSummaryNo = wesmbillSummaryNo).FirstOrDefault        
        If WESMBillsExemptionListWithUpdate.Count = 0 Then
            If Not getWESMBillsExemptionItem.NoSOAOrig = noSOA Then
                With getWESMBillsExemptionItem
                    .NoSOA = noSOA
                    WESMBillsExemptionListWithUpdate.Add(New WESMBillsExemption(.WESMBillSummaryNo, .IDNumber, .ParticipantName,
                                                                                .WESMBillBatchNo, .BillPeriod, .ChargeType, .InvoiceNo,
                                                                                .OrigDueDate, .NewDueDate, .BeginningBalance, .EndingBalance,
                                                                                .NoOffset, .NoOffsetOrig, noSOA, .NoSOAOrig,
                                                                                .NoDefaultInterest, .NoDefaultInterestOrig))
                End With
            End If
        Else
            Dim getWEMSBillsExemptionItemWithUpdate As WESMBillsExemption = WESMBillsExemptionListWithUpdate.Where(Function(x) x.WESMBillSummaryNo = wesmbillSummaryNo).FirstOrDefault
            If getWEMSBillsExemptionItemWithUpdate Is Nothing Then
                With getWESMBillsExemptionItem
                    .NoSOA = noSOA
                    WESMBillsExemptionListWithUpdate.Add(New WESMBillsExemption(.WESMBillSummaryNo, .IDNumber, .ParticipantName,
                                                                                .WESMBillBatchNo, .BillPeriod, .ChargeType, .InvoiceNo,
                                                                                .OrigDueDate, .NewDueDate, .BeginningBalance, .EndingBalance,
                                                                                .NoOffset, .NoOffsetOrig, noSOA, .NoSOAOrig,
                                                                                .NoDefaultInterest, .NoDefaultInterestOrig))
                End With
            Else
                With getWEMSBillsExemptionItemWithUpdate
                    .NoSOA = noSOA
                    If .NoOffset = .NoOffsetOrig And .NoSOA = .NoSOAOrig And .NoDefaultInterest = .NoDefaultInterestOrig Then
                        Me.WESMBillsExemptionListWithUpdate.RemoveAll(Function(x) x.WESMBillSummaryNo = wesmbillSummaryNo)
                    End If
                End With
                With getWESMBillsExemptionItem
                    .NoSOA = noSOA
                End With
            End If            
        End If
    End Sub

    Public Sub UpdateWESMBillExemptionForNoDefInt(ByVal wesmbillSummaryNo As Long, ByVal noDefInt As Boolean)
        Dim getWESMBillsExemptionItem As WESMBillsExemption = WESMBillsExemptionList.Where(Function(x) x.WESMBillSummaryNo = wesmbillSummaryNo).FirstOrDefault        
        If WESMBillsExemptionListWithUpdate.Count = 0 Then
            If Not getWESMBillsExemptionItem.NoDefaultInterestOrig = noDefInt Then
                With getWESMBillsExemptionItem
                    .NoDefaultInterest = noDefInt
                    WESMBillsExemptionListWithUpdate.Add(New WESMBillsExemption(.WESMBillSummaryNo, .IDNumber, .ParticipantName,
                                                                                .WESMBillBatchNo, .BillPeriod, .ChargeType, .InvoiceNo,
                                                                                .OrigDueDate, .NewDueDate, .BeginningBalance, .EndingBalance,
                                                                                .NoOffset, .NoOffsetOrig, .NoSOA, .NoSOAOrig,
                                                                                noDefInt, .NoDefaultInterestOrig))
                End With
            End If
        Else
            Dim getWEMSBillsExemptionItemWithUpdate As WESMBillsExemption = WESMBillsExemptionListWithUpdate.Where(Function(x) x.WESMBillSummaryNo = wesmbillSummaryNo).FirstOrDefault

            If getWEMSBillsExemptionItemWithUpdate Is Nothing Then
                With getWESMBillsExemptionItem
                    .NoDefaultInterest = noDefInt
                    WESMBillsExemptionListWithUpdate.Add(New WESMBillsExemption(.WESMBillSummaryNo, .IDNumber, .ParticipantName,
                                                                                .WESMBillBatchNo, .BillPeriod, .ChargeType, .InvoiceNo,
                                                                                .OrigDueDate, .NewDueDate, .BeginningBalance, .EndingBalance,
                                                                                .NoOffset, .NoOffsetOrig, .NoSOA, .NoSOAOrig,
                                                                                noDefInt, .NoDefaultInterestOrig))
                End With
            Else
                With getWEMSBillsExemptionItemWithUpdate
                    .NoDefaultInterest = noDefInt
                    If .NoOffset = .NoOffsetOrig And .NoSOA = .NoSOAOrig And .NoDefaultInterest = .NoDefaultInterestOrig Then
                        Me.WESMBillsExemptionListWithUpdate.RemoveAll(Function(x) x.WESMBillSummaryNo = wesmbillSummaryNo)
                    End If
                End With
                With getWESMBillsExemptionItem
                    .NoDefaultInterest = noDefInt
                End With
            End If            
        End If
    End Sub

    Public Sub GetWESMBillsExemptionList()        
        Dim report As New DataReport

        Try

            Dim SQL As String = "SELECT AP.ID_NUMBER, AP.PARTICIPANT_ID, AWBS.WESMBILL_SUMMARY_NO, AWBS.WESMBILL_BATCH_NO, AWBS.BILLING_PERIOD, " _
                            & "AWBS.INV_DM_CM, AWBS.DUE_DATE, AWBS.NEW_DUEDATE, AWBS.BEGINNING_BALANCE, " _
                            & "AWBS.ENDING_BALANCE, AWBS.NO_SOA, AWBS.NO_OFFSET, AWBS.NO_DEFINT, AWBS.CHARGE_TYPE FROM AM_WESM_BILL_SUMMARY AWBS " _
                            & "INNER JOIN AM_PARTICIPANTS AP ON AP.ID_NUMBER = AWBS.ID_NUMBER " _
                            & "WHERE AWBS.BALANCE_TYPE = 'AR' AND AWBS.ENDING_BALANCE < 0"

            Me.DataAccess.ConnectionString = AMModule.ConnectionString
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            Me.CreateWESMBillsExemption(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub

    Private Sub CreateWESMBillsExemption(ByVal dr As IDataReader)
        Dim resultObj As New List(Of WESMBillsExemption)        
        Dim index As Integer = 0

        Try            
            While dr.Read()
                index += 1
                With dr
                    Dim item As New WESMBillsExemption
                    item.WESMBillSummaryNo = CLng(.Item("WESMBILL_SUMMARY_NO").ToString())
                    item.WESMBillBatchNo = CInt(.Item("WESMBILL_BATCH_NO").ToString())
                    item.IDNumber = CStr(.Item("ID_NUMBER").ToString())
                    item.ParticipantName = CStr(.Item("PARTICIPANT_ID").ToString())
                    item.BillPeriod = CInt(.Item("BILLING_PERIOD").ToString())
                    item.ChargeType = CType(System.Enum.Parse(GetType(EnumChargeType), CStr(.Item("CHARGE_TYPE").ToString())), EnumChargeType)
                    item.InvoiceNo = CStr(.Item("INV_DM_CM").ToString())
                    item.OrigDueDate = CDate(.Item("DUE_DATE").ToString())
                    item.NewDueDate = CDate(.Item("NEW_DUEDATE").ToString())
                    item.BeginningBalance = CDec(.Item("BEGINNING_BALANCE").ToString())
                    item.EndingBalance = CDec(.Item("ENDING_BALANCE").ToString())
                    item.NoOffset = CBool(IIf(CInt(.Item("NO_OFFSET")) = 1, True, False))
                    item.NoOffsetOrig = CBool(IIf(CInt(.Item("NO_OFFSET")) = 1, True, False))
                    item.NoSOA = CBool(IIf(CInt(.Item("NO_SOA")) = 1, True, False))
                    item.NoSOAOrig = CBool(IIf(CInt(.Item("NO_SOA")) = 1, True, False))
                    item.NoDefaultInterest = CBool(IIf(CInt(.Item("NO_DEFINT")) = 1, True, False))
                    item.NoDefaultInterestOrig = CBool(IIf(CInt(.Item("NO_DEFINT")) = 1, True, False))

                    resultObj.Add(item)
                End With
            End While
            Me._WESMBillsExemptionList = resultObj
        Catch ex As Exception
            Throw New ApplicationException("Error in row " & index & " --- " & ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try
    End Sub

    Public Sub SaveChanges()
        Dim report As New DataReport
        Dim ListofSQL As New List(Of String)
        Try
            ListofSQL = Me.CreateQueries
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

    Private Function CreateQueries() As List(Of String)
        Dim ret As List(Of String) = New List(Of String)
        Try
            For Each item In Me.WESMBillsExemptionListWithUpdate
                Dim noOffset As Integer = If(item.NoOffset = True, 1, 0)
                Dim noDefInt As Integer = If(item.NoDefaultInterest = True, 1, 0)
                Dim noSOA As Integer = If(item.NoSOA = True, 1, 0)


                Dim sql As String = "UPDATE AM_WESM_BILL_SUMMARY SET NO_OFFSET = " & noOffset _
                                & ", NO_SOA = " & noSOA & ", NO_DEFINT = " & noDefInt _
                                & " WHERE WESMBILL_SUMMARY_NO = " & item.WESMBillSummaryNo
                ret.Add(sql)
            Next
            ret.TrimExcess()
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

End Class
