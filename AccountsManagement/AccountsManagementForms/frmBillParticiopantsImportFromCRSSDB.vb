
Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmBillParticiopantsImportFromCRSSDB
    Private _ImportParticipantsFromCRSSDBHlpr As New ImportParticipantsFromCRSSDBHelper
    Private Sub frmBillParticiopantsImportFromCRSSDB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DisplayOnGrid()
    End Sub
    Private Sub DisplayOnGrid()
        Me.DGridViewNew.Rows.Clear()
        For Each item In _ImportParticipantsFromCRSSDBHlpr.NewCRSSParticipants
            With item
                Me.DGridViewNew.Rows.Add(False, .IDNumber, .FullName, .BillingAddress, .Renewable, .ZeroRated, .IncomeTaxHoliday, .FacilityType, .Region, .MembershipType)
            End With
        Next
    End Sub

    Private Sub DGridViewNew_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGridViewNew.CellContentClick
        If (e.ColumnIndex = 0 AndAlso e.RowIndex >= 0) Then
            Dim value = DirectCast(DGridViewNew(e.ColumnIndex, e.RowIndex).FormattedValue,  _
                                   Nullable(Of Boolean))
            If (value.HasValue AndAlso value = True) Then               
                DGridViewNew(e.ColumnIndex, e.RowIndex).Value = False
            Else
                DGridViewNew(e.ColumnIndex, e.RowIndex).Value = True
            End If
        End If
    End Sub

    Private Sub cb_SelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles cb_SelectAll.CheckedChanged
        If cb_SelectAll.Checked = True Then
            For index As Integer = 0 To Me.DGridViewNew.RowCount - 1
                With Me.DGridViewNew.Rows(index)
                    If CBool(.Cells("cbAdd").Value) = False Then
                        .Cells("cbAdd").Value = True
                    End If
                End With
            Next
        Else
            For index As Integer = 0 To Me.DGridViewNew.RowCount - 1
                With Me.DGridViewNew.Rows(index)
                    If CBool(.Cells("cbAdd").Value) = True Then
                        .Cells("cbAdd").Value = False
                    End If
                End With
            Next
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim ans As MsgBoxResult            
            Dim newParticpantList As New List(Of AMParticipants)

            ans = MsgBox("Do you really want to save this record?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")

            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            For index As Integer = 0 To Me.DGridViewNew.RowCount - 1
                With Me.DGridViewNew.Rows(index)
                    If CBool(.Cells("cbAdd").Value) = True Then
                        Dim newParticipant As New AMParticipants

                        newParticipant.IDNumber = CStr(.Cells("IDNumber").Value.ToString).ToUpper
                        newParticipant.ParticipantID = CStr(.Cells("IDNumber").Value.ToString).ToUpper
                        newParticipant.FullName = CStr(.Cells("FullName").Value.ToString).ToUpper
                        newParticipant.TIN = ""
                        newParticipant.EcoZoneRegCertificateNo = ""
                        newParticipant.EcoZoneEffectiveDate = Nothing

                        newParticipant.City = ""
                        newParticipant.Province = ""
                        newParticipant.ZipCode = ""
                        newParticipant.BillingAddress = CStr(.Cells("Billing_Address").Value.ToString).ToUpper
                        newParticipant.ParticipantAddress = ""
                        newParticipant.BusinessStyle = ""

                        newParticipant.Representative = New ParticipantRepresentative("", "", "", "", "", "", "")

                        newParticipant.ZeroRatedEnergy = CType(IIf(CStr(.Cells("ZeroRated").Value.ToString) = "N", False, True), Boolean)
                        newParticipant.ZeroRatedMarketFees = CType(IIf(CStr(.Cells("ZeroRated").Value.ToString) = "N", False, True), Boolean)
                        newParticipant.GenLoad = CType(IIf(CStr(.Cells("FacilityType").Value.ToString) = "GENERATOR", EnumGenLoad.G, EnumGenLoad.L), EnumGenLoad)
                        newParticipant.Region = CStr(.Cells("ColRegion").Value.ToString)
                        newParticipant.MembershipType = CStr(.Cells("MembershipType").Value.ToString)

                        newParticipant.Status = EnumStatus.Active
                        newParticipant.BankTransactionCode = ""
                        newParticipant.BankAccountNo = ""
                        newParticipant.Bank = ""
                        newParticipant.BankBranch = ""
                        newParticipant.CheckPay = ""
                        newParticipant.VirtualAccountNo = ""
                        newParticipant.PaymentType = EnumParticipantPaymentType.Check

                        newParticipant.MarketFeesWHTax = CDec("0.00")
                        newParticipant.MarketFeesWHVAT = CDec("0.00")
                        newParticipant.EnergyWHTax = CDec("0.00")
                        newParticipant.EnergyWHVAT = CDec("0.00")
                        newParticipant.BIRATCType = "WC158"
                        newParticpantList.Add(newParticipant)
                    End If
                End With
            Next
            If newParticpantList.Count > 0 Then
                _ImportParticipantsFromCRSSDBHlpr.SaveSelectedNewParticipants(newParticpantList)
                MsgBox("Successfully Saved!", MsgBoxStyle.Information, "Save")
                Me.Close()
            Else
                MsgBox("No selected New Participants", MsgBoxStyle.Information, "System")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

End Class