Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmPaymentNewFTF
    Public _PymtHelper As New PaymentHelper
    Public ReadOnly Property PymtHelper() As PaymentHelper
        Get
            Return _PymtHelper
        End Get
    End Property

    Public _Signatory As New DocSignatories
    Public ReadOnly Property Signatory() As DocSignatories
        Get
            Return _Signatory
        End Get
    End Property

    Public _Signatory2 As New DocSignatories
    Public ReadOnly Property Signatory2() As DocSignatories
        Get
            Return _Signatory2
        End Get
    End Property

    Private Sub frmPaymentNewFTF_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.RefreshComboBox()
    End Sub

    Private Sub RefreshComboBox()
        Me.cmb_FTFTransType.Items.Clear()
        Dim items As Array
        items = System.Enum.GetNames(GetType(EnumFTFTransType))
        Dim item As String
        For Each item In items
            Me.cmb_FTFTransType.Items.Add(item.ToString())
        Next
    End Sub

    Private Sub btn_GenerateFTF_Click(sender As Object, e As EventArgs) Handles btn_GenerateFTF.Click
        Try
            Dim ds As New DataSet
            Dim dtMain As New DSReport.FTFMainDataTable
            Dim TransTypeStr = DirectCast([Enum].Parse(GetType(EnumFTFTransType), Me.cmb_FTFTransType.SelectedItem.ToString()), EnumFTFTransType)


            Dim GetFTF = (From x In PymtHelper.FundTransferform _
                          Where x.TransType = TransTypeStr _
                          Select x).FirstOrDefault

            'Dim GetFTFColl = (From x In PymtHelper.CollFundTransferform _
            '                  Where x.TransType = TransTypeStr _
            '                  Select x).ToList

            'Dim GetFTFAll = GetFTFColl.Union(GetFTF).ToList

            If Not GetFTF Is Nothing Then
                Dim DTSet As DataSet = Me.GenerateFTFDS(GetFTF)
                Dim frmViewer As New frmReportViewer()
                With frmViewer
                    .LoadFTF(DTSet)                    
                    .ShowDialog()
                End With
            Else
                MessageBox.Show("No available FTF report based on selected transaction.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Function GenerateFTFDS(ByVal FTF As FundTransferFormMain) As DataSet
        Dim DS As New DataSet
        Dim dtMain As New DSReport.FTFMainDataTable
        Dim dtParticipant As New DSReport.FTFParticipantDataTable
        Dim dtDetails As New DSReport.FTFDetailsDataTable
        With FTF
            Dim row = dtMain.NewRow()
            row("REF_NO") = .RefNo
            row("DR_DATE") = .DRDate.ToString("MMMM dd, yyyy")
            row("CR_DATE") = .CRDate.ToString("MMMM dd, yyyy")
            Select Case .TransType
                Case EnumFTFTransType.DrawDown
                    row("REMARKS") = "Drawdown of Prudential Security Deposit     P" & FormatNumber(.TotalAmount, 2)
                Case EnumFTFTransType.Replenishment
                    row("REMARKS") = "Replenishment of Prudential Security Deposit      P" & FormatNumber(.TotalAmount, 2)
                Case EnumFTFTransType.TransferPEMCAccount
                    row("REMARKS") = "Fund Transfer From Settlement to IEMOP Account    P" & FormatNumber(.TotalAmount, 2)
                Case EnumFTFTransType.TransferSTLToNSS
                    row("REMARKS") = "Fund Transfer From Settlement to NSS Account     P" & FormatNumber(.TotalAmount, 2)
                Case EnumFTFTransType.TransferNSSToSTL
                    row("REMARKS") = "Transfer of NSS to Settlement Account     P" & FormatNumber(.TotalAmount, 2)
                Case EnumFTFTransType.TransferMarketFeesToPEMC
                    row("REMARKS") = "Transfer of Market Fees to IEMOP Account     P" & FormatNumber(.TotalAmount, 2)
                Case EnumFTFTransType.TransferMarketFeesToSTL
                    row("REMARKS") = "Transfer of Market Fees to Settlement from IEMOP Account     P" & FormatNumber(.TotalAmount, 2)
            End Select

            row("TOTAL_AMOUNT") = .TotalAmount
            row("PREPARED_BY") = AMModule.FullName
            row("POSITION") = AMModule.Position

            If .TransType = EnumFTFTransType.TransferMarketFeesToPEMC Or .TransType = EnumFTFTransType.TransferPEMCAccount Then
                row("REQUESTING_APPROVAL") = Signatory2.Signatory_1
                row("POSITION1") = Signatory2.Position_1
                row("APPROVED_BY") = Signatory2.Signatory_2
                row("POSITION2") = Signatory2.Position_2
            Else
                row("REQUESTING_APPROVAL") = Signatory.Signatory_1
                row("POSITION1") = Signatory.Position_1
                row("APPROVED_BY") = Signatory.Signatory_2
                row("POSITION2") = Signatory.Position_2
            End If
            
            If .TransType = EnumFTFTransType.DrawDown Then
                row("NOTED_BY") = Signatory.Signatory_3
                row("POSITION3") = Signatory.Position_3
            End If
            dtMain.Rows.Add(row)
        End With
        dtMain.AcceptChanges()


        For Each item In FTF.ListOfFTFParticipants
            Dim row = dtParticipant.NewRow()

            With item
                row("REF_NO") = .RefNo
                row("ID_NUMBER") = .IDNumber.IDNumber
                row("PARTICPANT_ID") = .IDNumber.ParticipantID
                row("AMOUNT") = .Amount

                dtParticipant.Rows.Add(row)
            End With
        Next
        dtParticipant.AcceptChanges()


        For Each item In FTF.ListOfFTFDetails
            Dim row = dtDetails.NewRow()

            With item
                row("REF_NO") = .RefNo
                row("BANK_ACCT_NO") = .BankAccountNo
                If .BankAccountNo.Contains("Pru") Then
                    If .Debit <> 0 Then
                        row("RECEIVING_BANK") = "SCB Prudential"
                    ElseIf .Credit <> 0 Then
                        row("ISSUING_BANK") = "SCB Prudential"
                    End If
                ElseIf .BankAccountNo.Contains("PEM") Then
                    If .Debit <> 0 Then
                        row("RECEIVING_BANK") = "BPI"
                    ElseIf .Credit <> 0 Then
                        row("ISSUING_BANK") = "BPI"
                    End If
                ElseIf .BankAccountNo.Contains("Setl") Then
                    If .Debit <> 0 Then
                        row("RECEIVING_BANK") = "SCB Settlement"
                    ElseIf .Credit <> 0 Then
                        row("ISSUING_BANK") = "SCB Settlement"
                    End If
                End If

                'If FTF.TransType = EnumFTFTransType.TransferMarketFeesToPEMC Or FTF.TransType = EnumFTFTransType.TransferPEMCAccount Then
                'row("AMOUNT") = (.Debit - .Credit) * -1
                'Else
                row("AMOUNT") = .Debit - .Credit
                'End If
                dtDetails.Rows.Add(row)
            End With
        Next
        dtDetails.AcceptChanges()


        With DS.Tables
            .Add(dtMain)
            .Add(dtParticipant)
            .Add(dtDetails)
        End With
        DS.AcceptChanges()

        Return DS
    End Function

    Private Sub btn_Close_Click(sender As Object, e As EventArgs) 
        Me.Close()
    End Sub
End Class