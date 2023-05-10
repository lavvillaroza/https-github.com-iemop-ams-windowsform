Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Public Class ParentChildExempHelper
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

    Public Sub New()        
        Me._WBillHelper = Nothing
        Me._DataAccess = Nothing
        Me.Initialize()
    End Sub

    Public Sub Initialize()
        Me._DataAccess = DAL.GetInstance()        
        Me._WBillHelper = WESMBillHelper.GetInstance        
        Me._ListOfParticipantsInfo = Me.WBillHelper.GetAMParticipants()
        Me._ParentChildExempList = Me.WBillHelper.GetParentChildOffsettingExemp()        
    End Sub

#Region "Property of ParentChildExemp List"
    Private _ParentChildExempList As New List(Of ParentChildExemption)
    Public ReadOnly Property ParentChildExempList() As List(Of ParentChildExemption)
        Get
            Return _ParentChildExempList
        End Get
    End Property
#End Region

#Region "Property of ParentChildExemp View"
    Private _ParentChildExempView As New ParentChildExemption
    Public ReadOnly Property ParentChildExempView() As ParentChildExemption
        Get
            Return _ParentChildExempView
        End Get
    End Property
#End Region

#Region "Property for List of ParticipantID Info"
    Private _ListOfParticipantsInfo As New List(Of AMParticipants)
    Public ReadOnly Property ListOfParticipantsInfo() As List(Of AMParticipants)
        Get
            Return _ListOfParticipantsInfo
        End Get
    End Property
#End Region

#Region "Get Data for Viewing"
    Public Sub GetSelectedExemption(ByVal parentid As String, ByVal childid As String, ByVal chargetype As EnumChargeType, ByVal status As EnumStatus, ByVal updatedby As String, ByVal updateddate As Date)
        Dim getParentId As AMParticipants = (From x In Me.ListOfParticipantsInfo Where x.IDNumber = parentid Select x).FirstOrDefault
        Dim getChildId As AMParticipants = (From x In Me.ListOfParticipantsInfo Where x.IDNumber = childid Select x).FirstOrDefault
        Me._ParentChildExempView = New ParentChildExemption(getParentId, getChildId, chargetype, status, updatedby, updateddate)
    End Sub
#End Region

#Region "Save New Exemption"
    Public Sub SaveNewExemption(ByVal parentID As String, ByVal childID As String, ByVal chargeType As EnumChargeType, ByVal stat As EnumStatus)
        Try
            Dim report As New DataReport

            If WBillHelper.checkItemIfExistInPCOffsettingExemption(parentID, childID, chargeType) = True Then
                Throw New Exception("Parent and Child ID are already exist in the Table.")
            Else
                Dim ListOfSQL As List(Of String) = Me.CreateInsertQuery(parentID, childID, chargeType, stat)
                report = Me.DataAccess.ExecuteSaveQuery(ListOfSQL, New DataSet)

                If report.ErrorMessage.Length <> 0 Then
                    Throw New ApplicationException(report.ErrorMessage)
                End If
            End If

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

    End Sub

    Public Function CreateInsertQuery(ByVal ParentID As String, ByVal ChildID As String, ByVal oChargeType As EnumChargeType, ByVal oStat As EnumStatus) As List(Of String)
        Try
            Dim ListOfSQL As New List(Of String)
            Dim SQL As String = ""
            Dim SysDate As Date = WBillHelper.GetSystemDate()
            Dim SysDateTime As Date = WBillHelper.GetSystemDateTime()
            Dim UpdatedBy As String = AMModule.UserName

            SQL = "INSERT INTO AM_P2C_1STOFFSET_EXEMPTION(ID_NUMBER_PARENT, ID_NUMBER_CHILD, CHARGE_TYPE, STATUS, UPDATED_BY, UPDATED_DATE) " & vbNewLine & _
                  "VALUES('" & ParentID & "', '" & ChildID & "', '" & oChargeType.ToString & "'," & oStat & ", '" & UpdatedBy & "', " & vbNewLine & _
                         "TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'))"
            ListOfSQL.Add(SQL)

            Return ListOfSQL
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Function
#End Region

#Region "Update Exemption"
    Public Sub UpdateExemption(ByVal parentID As String, ByVal childID As String, ByVal chargeType As EnumChargeType, ByVal stat As EnumStatus)
        Try
            Dim report As New DataReport
            Dim ListOfSQL As List(Of String) = Me.CreateUpdateQuery(parentID, childID, chargeType, stat)
            report = Me.DataAccess.ExecuteSaveQuery(ListOfSQL, New DataSet)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub

    Public Function CreateUpdateQuery(ByVal ParentID As String, ByVal ChildID As String, ByVal oChargeType As EnumChargeType, ByVal oStat As EnumStatus) As List(Of String)
        Try
            Dim ListOfSQL As New List(Of String)
            Dim SQL As String = ""
            Dim SysDate As Date = WBillHelper.GetSystemDate()
            Dim SysDateTime As Date = WBillHelper.GetSystemDateTime()
            Dim UpdatedBy As String = AMModule.UserName

            SQL = "UPDATE AM_P2C_1STOFFSET_EXEMPTION SET STATUS = " & oStat & vbNewLine & ", UPDATED_DATE = SYSDATE, UPDATED_BY = '" & AMModule.UserName & "' " & _
                  "WHERE ID_NUMBER_PARENT = '" & ParentID & "' AND ID_NUMBER_CHILD = '" & ChildID & "' AND CHARGE_TYPE = '" & oChargeType.ToString & "'"
            ListOfSQL.Add(SQL)
            Return ListOfSQL
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Function
#End Region

End Class
