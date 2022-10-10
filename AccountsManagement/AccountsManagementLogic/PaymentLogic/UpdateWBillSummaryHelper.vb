Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Public Class UpdateWBillSummaryHelper
    Public isCurDueDateChange As Boolean = False
    Public isEWTBalanceChange As Boolean = False
    Public isParticipantIDChange As Boolean = False

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

#Region "BFactory"
    Public _BFactory As New BusinessFactory
    Private ReadOnly Property BFactory() As BusinessFactory
        Get
            Return _BFactory
        End Get
    End Property
#End Region
    Public Sub New()
        Me._ListOfWESMBillSummary = New List(Of WESMBillSummary)
        Me._ListOfAMParticipants = New List(Of AMParticipants)
        InitializeObject()
    End Sub

    Public Sub InitializeObject()
        Me._DataAccess = DAL.GetInstance()
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._BFactory = BusinessFactory.GetInstance

        Me._ListOfAMParticipants = Me.WBillHelper.GetAMParticipants()
        'Me._ListOfWESMBillSummary = Me.WBillHelper.GetWESMBillSummaryWithEndingBalance()
    End Sub

#Region "Property of AMParticipants"
    Private _ListOfAMParticipants As New List(Of AMParticipants)
    Public ReadOnly Property ListOfAMParticipants() As List(Of AMParticipants)
        Get
            Return _ListOfAMParticipants
        End Get
    End Property
#End Region

#Region "WESM Bill Summary with Outstanding Balance"
    Private _ListOfWESMBillSummary As New List(Of WESMBillSummary)
    Public ReadOnly Property ListOfWESMBillSummary() As List(Of WESMBillSummary)
        Get
            Return _ListOfWESMBillSummary
        End Get
    End Property
#End Region

#Region "WESM Bill Summary Data Table"
    Public Function GetWESMBillSummaryDT(ByVal ParticipantID As String, ByVal ChargeType As EnumChargeType) As DataTable
        Dim ret As New DataTable
        Try
            Me._ListOfWESMBillSummary = WBillHelper.GetWESMBillSummaryWithBalancePerParticipant(ParticipantID, ChargeType)
            Dim ListofWESMBillSummaryPerID = (From x In Me.ListOfWESMBillSummary _
                                              Where x.IDNumber.ParticipantID = ParticipantID _
                                              And x.ChargeType = ChargeType _
                                              Select x).ToList()

            ret.TableName = "WESMBillSummaryList"
            With ret.Columns
                .Add("WESMBillSummaryNo", GetType(Long))
                .Add("BillingPeriod", GetType(Long))
                .Add("IDNumber", GetType(String))
                .Add("ParticipantID", GetType(String))
                .Add("InvoiceNumber", GetType(String))
                .Add("OrigDueDate", GetType(Date))
                .Add("NewDueDate", GetType(Date))
                .Add("ChargeType", GetType(String))
                .Add("CurrentEndingBalance", GetType(String))
                .Add("EnergyWithholdingTax", GetType(String))
            End With

            For Each Item In ListofWESMBillSummaryPerID
                Dim row As DataRow
                row = ret.NewRow()
                row("WESMBillSummaryNo") = Item.WESMBillSummaryNo
                row("BillingPeriod") = Item.BillPeriod
                row("IDNumber") = Item.IDNumber.IDNumber
                row("ParticipantID") = Item.IDNumber.ParticipantID
                row("InvoiceNumber") = Item.INVDMCMNo
                row("OrigDueDate") = Item.DueDate
                row("NewDueDate") = Item.NewDueDate
                row("ChargeType") = IIf(Item.ChargeType = EnumChargeType.E, "Energy", "VAT")
                row("CurrentEndingBalance") = FormatNumber(Item.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                row("EnergyWithholdingTax") = FormatNumber(Item.EnergyWithhold, UseParensForNegativeNumbers:=TriState.True)
                ret.Rows.Add(row)
            Next
            ret.AcceptChanges()
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try        
        Return ret
    End Function
#End Region

#Region "Get AM_WESMBILL_SUMMARY_CLOGS"
    Public Function GetAMWESMBillSUmmaryClogs(ByVal _WESMBillSummaryNo As Long) As DataTable
        Dim ret As New DataTable
        Try
            Dim FetchWBSChangeLogs As List(Of WESMBillSummaryChangeLogs) = (From x In WBillHelper.GetWESMBillSummaryChangeLogs(_WESMBillSummaryNo) Select x Order By x.UpdatedDate).ToList

            ret.TableName = "WESMBillSummaryChangeLogs"
            With ret.Columns
                .Add("WESMBillSummaryNo", GetType(Long))
                .Add("ChangeInfoType", GetType(String))
                .Add("OldValue", GetType(String))
                .Add("NewValue", GetType(String))
                .Add("UpdatedDate", GetType(String))
                .Add("UpdatedBy", GetType(String))
            End With

            For Each Item In FetchWBSChangeLogs
                Dim row As DataRow
                row = ret.NewRow()
                row("WESMBillSummaryNo") = Item.WESMBillSummaryNo
                row("ChangeInfoType") = Item.ChangeInfoType
                row("OldValue") = Item.OldValue
                row("NewValue") = Item.NewValue
                row("UpdatedDate") = Item.UpdatedDate
                row("UpdatedBy") = Item.UpdatedBy
                ret.Rows.Add(row)
            Next
            ret.AcceptChanges()
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return ret
    End Function

#End Region

#Region "List of ParticipantID"
    Public Function GetListofParticipantID() As List(Of String)
        Dim ret As New List(Of String)
        For Each item In Me.ListOfAMParticipants
            ret.Add(item.ParticipantID)
        Next
        Return ret
    End Function
#End Region

#Region "Save Changes in WESMBillSummaryItem"
    Public Sub SaveChanges(ByVal WBSItem As WESMBillSummary, ByVal NewParticipantId As String,
                           ByVal NewCurDueDate As Date, ByVal NewEWTBalance As Decimal)
        Dim ListOfSQL As New List(Of String)
        Dim sSQL As String = "UPDATE AM_WESM_BILL_SUMMARY SET "
        Dim sSQL2 As String = ""
        Dim SysDateTime = WBillHelper.GetSystemDateTime()
        If NewParticipantId <> "" And Me.isParticipantIDChange = True Then
            Dim GetNewParticipantID As AMParticipants = (From x In Me.ListOfAMParticipants Where x.ParticipantID = NewParticipantId Select x).FirstOrDefault
            sSQL &= "ID_Number ='" & GetNewParticipantID.IDNumber & "', "
            sSQL2 = "INSERT INTO AM_WESM_BILL_SUMMARY_CLOGS(WESMBILL_SUMMARY_NO, CHANGE_INFO_TYPE, OLD_VALUE, NEW_VALUE, UPDATED_DATE, UPDATED_BY) " _
                  & "VALUES(" & WBSItem.WESMBillSummaryNo & ", 'ID_NUMBER', '" & WBSItem.IDNumber.IDNumber & "', '" & GetNewParticipantID.IDNumber & "', " _
                          & "TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "', 'MM/DD/YYYY HH24:MI:SS'), '" & AMModule.UserName & "')"

            ListOfSQL.Add(sSQL2)

            Dim getDMCMForPCOffsetting As List(Of DebitCreditMemo) = WBillHelper.GetDMCMMainInvoiceP2COffset(WBSItem.INVDMCMNo)
            For Each item In getDMCMForPCOffsetting
                With item
                    sSQL2 = "UPDATE AM_DMCM SET ID_NUMBER = '" & GetNewParticipantID.IDNumber & "' WHERE AM_DMCM_NO = " & item.DMCMNumber
                    ListOfSQL.Add(sSQL2)

                    sSQL2 = "UPDATE AM_DMCM_DETAILS SET ID_NUMBER = '" & GetNewParticipantID.IDNumber & "' WHERE AM_DMCM_NO = " & item.DMCMNumber
                    ListOfSQL.Add(sSQL2)

                End With
            Next

        End If

        If Not NewCurDueDate = Date.MinValue And Me.isCurDueDateChange = True Then            
            sSQL &= "NEW_DUEDATE = TO_DATE('" & NewCurDueDate.ToShortDateString & "', 'mm/dd/yyyy'), "
            sSQL2 = "INSERT INTO AM_WESM_BILL_SUMMARY_CLOGS(WESMBILL_SUMMARY_NO, CHANGE_INFO_TYPE, OLD_VALUE, NEW_VALUE, UPDATED_DATE, UPDATED_BY) " _
                & "VALUES(" & WBSItem.WESMBillSummaryNo & ", 'NEW_DUEDATE', '" & WBSItem.OrigNewDueDate.ToShortDateString & "', '" & NewCurDueDate.ToShortDateString & "', " _
                        & "TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "', 'MM/DD/YYYY HH24:MI:SS'), '" & AMModule.UserName & "')"

            ListOfSQL.Add(sSQL2)
        End If

        If Me.isEWTBalanceChange = True Then
            sSQL &= "ENERGY_WITHHOLD = " & NewEWTBalance & " "
            sSQL2 = "INSERT INTO AM_WESM_BILL_SUMMARY_CLOGS(WESMBILL_SUMMARY_NO, CHANGE_INFO_TYPE, OLD_VALUE, NEW_VALUE, UPDATED_DATE, UPDATED_BY) " _
                  & "VALUES(" & WBSItem.WESMBillSummaryNo & ", 'ENERGY_WITHHOLD', '" & FormatNumber(WBSItem.EnergyWithhold, UseParensForNegativeNumbers:=TriState.True).ToString & "', '" _
                          & FormatNumber(NewEWTBalance, UseParensForNegativeNumbers:=TriState.True).ToString & "', " _
                          & "TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "', 'MM/DD/YYYY HH24:MI:SS'), '" & AMModule.UserName & "')"

            ListOfSQL.Add(sSQL2)
        End If

        sSQL = CStr(IIf(Right(sSQL, 2) = ", ", Left(sSQL, Len(sSQL) - 2), sSQL))
        sSQL &= "WHERE WESMBILL_SUMMARY_NO = " & WBSItem.WESMBillSummaryNo

        Try
            Dim report As New DataReport
            ListOfSQL.Add(sSQL)
            report = Me.DataAccess.ExecuteSaveQuery(ListOfSQL, New DataSet)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

    End Sub

    
#End Region
End Class
