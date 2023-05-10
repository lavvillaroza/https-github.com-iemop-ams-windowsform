Imports System.Text
Imports AccountsManagementObjects
Imports AccountsManagementDataAccess

Public Class ImportParticipantsFromCRSSDBHelper

#Region "WESMBillHelper"
    Private _WBillHelper As WESMBillHelper
    Public ReadOnly Property WBillHelper() As WESMBillHelper
        Get
            Return _WBillHelper
        End Get
    End Property
#End Region

#Region "PosgresSQLDAL"
    Private _NpgDataAccess As NpgsqlDAL
    Public ReadOnly Property NpgDataAccess() As NpgsqlDAL
        Get
            Return Me._NpgDataAccess
        End Get
    End Property
#End Region

    Private _AMSParticipants As List(Of AMParticipants)
    Private _CRSSParticipants As List(Of CRSSParticipants)


    Private _NewCRSSParticipants As List(Of CRSSParticipants)
    Public Property NewCRSSParticipants() As List(Of CRSSParticipants)
        Get
            Return _NewCRSSParticipants
        End Get
        Set(ByVal value As List(Of CRSSParticipants))
            _NewCRSSParticipants = value
        End Set
    End Property

    Private _ExistCRSSParticipants As List(Of CRSSParticipants)
    Public Property ExistCRSSParticipants() As List(Of CRSSParticipants)
        Get
            Return _ExistCRSSParticipants
        End Get
        Set(ByVal value As List(Of CRSSParticipants))
            _ExistCRSSParticipants = value
        End Set
    End Property

    Public Sub New()
        Me._NpgDataAccess = NpgsqlDAL.GetInstance
        Me._NpgDataAccess.ConnectionString = AMModule.ConnectionStringCRSS
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._WBillHelper.ConnectionString = AMModule.ConnectionString
        Me.FillUpParticipantsProperties()
        Me.GetNewCRSSParticipants()
        Me.GetExistCRSSParticipants()
    End Sub

#Region "Get Participants in CRSS DB"
    Function GetAllParticipantsInCRSSDB() As List(Of CRSSParticipants)
        Dim result As New List(Of CRSSParticipants)

        Dim report As New DataReport
        Dim SQL As String

        Try
            If AMModule.RegionType = "LV" Then
                SQL = "SELECT * FROM settlement.cfg_billing_id WHERE region IN ('LUZON', 'VISAYAS')"
            ElseIf AMModule.RegionType = "M" Then
                SQL = "SELECT * FROM settlement.cfg_billing_id WHERE region = 'MINDANAO' ORDER BY billing_id"
            Else
                SQL = "SELECT * FROM settlement.cfg_billing_id WHERE region IN ('LUZON', 'VISAYAS', 'LUZON_VISAYAS','MINDANAO') ORDER BY billing_id"
            End If

            report = Me.NpgDataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.GetAllParticipantsInCRSSDB(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function

    Private Function GetAllParticipantsInCRSSDB(ByVal dr As IDataReader) As List(Of CRSSParticipants)
        Dim result As New List(Of CRSSParticipants)
        Try
            While dr.Read()
                With dr
                    Dim itemParticipant As New CRSSParticipants
                    itemParticipant.IDNumber = .Item("billing_id").ToString.ToUpper
                    itemParticipant.FullName = .Item("trading_participant").ToString.ToUpper
                    itemParticipant.BillingAddress = .Item("tp_address")
                    itemParticipant.Renewable = .Item("renewable")
                    itemParticipant.ZeroRated = .Item("zero_rated")
                    itemParticipant.IncomeTaxHoliday = "N"
                    itemParticipant.FacilityType = .Item("tp_facility_type")
                    itemParticipant.Region = .Item("region")
                    itemParticipant.MembershipType = .Item("tp_membership_type")
                    result.Add(itemParticipant)
                End With
            End While
            result.TrimExcess()
        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try
        Return result
    End Function
#End Region

#Region "Fillup Participants Properties"
    Sub FillUpParticipantsProperties()
        Me._AMSParticipants = WBillHelper.GetAMParticipantsAll()
        Me._CRSSParticipants = GetAllParticipantsInCRSSDB()
    End Sub
#End Region

    Private Sub GetNewCRSSParticipants()
        Dim getBillingIDInAMSParticipants As List(Of String) = (From x In _AMSParticipants Select x.IDNumber).ToList
        Dim getNewParticipants As List(Of CRSSParticipants) = (From x In _CRSSParticipants Where Not getBillingIDInAMSParticipants.Contains(x.IDNumber) Select x).ToList
        Me._NewCRSSParticipants = getNewParticipants
    End Sub

    Private Sub GetExistCRSSParticipants()
        Dim getBillingIDInAMSParticipants As List(Of String) = (From x In _AMSParticipants Select x.IDNumber.ToUpper).ToList
        Dim getExistParticipants As List(Of CRSSParticipants) = (From x In _CRSSParticipants Where getBillingIDInAMSParticipants.Contains(x.IDNumber.ToUpper) Select x).ToList
        Me._ExistCRSSParticipants = getExistParticipants
    End Sub



    Public Sub SaveSelectedNewParticipants(ByVal newPartcipantList As List(Of AMParticipants))
        WBillHelper.SaveAMParticipantList(newPartcipantList)
    End Sub
End Class
