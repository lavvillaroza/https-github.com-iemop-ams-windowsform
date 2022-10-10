Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Public Class WBSChangeParentIdHelper
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

    Sub New()
        Me._DataAccess = DAL.GetInstance()
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._objBillingPeriod = Me.WBillHelper.GetCalendarBP()
        Me._AMParticipantsList = (From x In Me.WBillHelper.GetAMParticipants() Select x Order By x.IDNumber).ToList()        
    End Sub

    Private _objBillingPeriod As New List(Of CalendarBillingPeriod)
    Public ReadOnly Property objBillingPeriod() As List(Of CalendarBillingPeriod)
        Get
            Return _objBillingPeriod
        End Get
    End Property

    Private _AMParticipantsList As New List(Of AMParticipants)
    Public ReadOnly Property AMParticipantsList() As List(Of AMParticipants)
        Get
            Return _AMParticipantsList
        End Get
    End Property

    Public Function GetobjWESMBillSummaryChangeParent(ByVal bpno As Integer)
        Return Me.WBillHelper.GetWESMBillSummaryChangeParentID(bpno)
    End Function

    Public Function CheckIfDuplicate(ByVal wbschangeparticipantiditem As WESMBillSummaryChangeParentId)
        Dim ret As Boolean
        Dim report As New DataReport
        Dim SQL As String = ""
        Try
            With wbschangeparticipantiditem
                SQL = "SELECT * FROM AM_WESM_BILL_SUMMARY_CHANGEID A " & vbNewLine _
                    & " WHERE A.BILLING_PERIOD = " & .BillingPeriod & vbNewLine _
                    & " AND A.PARENT_ID_NUMBER = '" & .ParentParticipants.IDNumber & "' AND A.CHILD_ID_NUMBER = '" & .ChildParticipants.IDNumber & "' " & vbNewLine _
                    & " AND A.NEW_PARENT_ID_NUMBER = '" & .NewParentParticipants.IDNumber & "'"
            End With

            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.getCheckIfDuplicate(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Public Function getCheckIfDuplicate(ByVal dr As IDataReader) As Boolean
        Dim result As Boolean
        Dim Counter As Integer
        Try
            While dr.Read()
                Counter += 1
            End While
            If Counter > 0 Then
                result = True
            End If
        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return result
    End Function

    Public Sub Save(ByVal wbschangeparticipantiditem As WESMBillSummaryChangeParentId)
        Try
            Dim report As New DataReport
            Dim ListofSQL As New List(Of String)
            With wbschangeparticipantiditem
                Dim SQL As String = "INSERT INTO AM_WESM_BILL_SUMMARY_CHANGEID(BILLING_PERIOD, PARENT_ID_NUMBER, CHILD_ID_NUMBER, NEW_PARENT_ID_NUMBER, STATUS, UPDATED_DATE, UPDATED_BY) " & vbNewLine _
                                & "VALUES (" & .BillingPeriod & ", '" & .ParentParticipants.IDNumber & "', '" & .ChildParticipants.IDNumber & "', '" & .NewParentParticipants.IDNumber & "', " & vbNewLine _
                                             & .Status & ", SYSDATE, '" & AMModule.UserName & "')"
                ListofSQL.Add(SQL)
            End With
            report = Me.DataAccess.ExecuteSaveQuery(ListofSQL, New DataSet)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub

    Public Sub Update(ByVal wbschangeparticipantiditem As WESMBillSummaryChangeParentId)
        Try
            Dim report As New DataReport
            Dim ListofSQL As New List(Of String)
            With wbschangeparticipantiditem
                Dim SQL As String = "UPDATE AM_WESM_BILL_SUMMARY_CHANGEID SET NEW_PARENT_ID_NUMBER = '" & .NewParentParticipants.IDNumber & "', STATUS = " & .Status & vbNewLine _
                                                                          & ", UPDATED_BY = '" & AMModule.UserName & "', " & "UPDATED_DATE = SYSDATE " & vbNewLine _
                                  & "WHERE BILLING_PERIOD = " & .BillingPeriod & " AND PARENT_ID_NUMBER = '" & .ParentParticipants.IDNumber & "' AND CHILD_ID_NUMBER = '" & .ChildParticipants.IDNumber & "'"

                ListofSQL.Add(SQL)
            End With
            report = Me.DataAccess.ExecuteSaveQuery(ListofSQL, New DataSet)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub

    Public Sub ImportSave(ByVal FromBillingPeriodNo As Integer, ByVal ToBillingPeriodNo As Integer)
        Try
            Dim report As New DataReport
            Dim ListofSQL As New List(Of String)

            Dim SQL As String = "INSERT INTO AM_WESM_BILL_SUMMARY_CHANGEID(BILLING_PERIOD, PARENT_ID_NUMBER, CHILD_ID_NUMBER, NEW_PARENT_ID_NUMBER, STATUS, UPDATED_BY, UPDATED_DATE) " & vbNewLine & " " _
                              & "SELECT " & ToBillingPeriodNo & ", A.PARENT_ID_NUMBER, A.CHILD_ID_NUMBER, A.NEW_PARENT_ID_NUMBER, A.STATUS, '" & AMModule.UserName & "', SYSDATE FROM AM_WESM_BILL_SUMMARY_CHANGEID A" & vbNewLine & " " _
                              & "WHERE A.BILLING_PERIOD = " & FromBillingPeriodNo & " AND NOT EXISTS (SELECT B.* FROM AM_WESM_BILL_SUMMARY_CHANGEID B WHERE B.PARENT_ID_NUMBER = A.PARENT_ID_NUMBER AND B.CHILD_ID_NUMBER = A.CHILD_ID_NUMBER AND B.BILLING_PERIOD = " & ToBillingPeriodNo & ")"
            ListofSQL.Add(SQL)

            report = Me.DataAccess.ExecuteSaveQuery(ListofSQL, New DataSet)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

    End Sub
End Class
