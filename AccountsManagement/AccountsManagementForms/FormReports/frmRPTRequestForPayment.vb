Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmRPTRequestForPayment
    Dim WBillHelper As WESMBillHelper
    Private RequestForPayment As List(Of RequestForPayment)
    Private SelRequestForPayment As New RequestForPayment
    Dim lstParticipants As List(Of AMParticipants)

    Private Sub frmRPTRequestForPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.MdiParent = MainForm
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName

            ProgressThread.Show("Please wait while preparing RFP Summary Report.")
            'fill Combo box
            Me.GetRequestForPayment()
            If Me.cbo_AllocationDate.Items.Count = 0 Then
                ProgressThread.Close()
                MsgBox("No available Request for Payment report to generate.", MsgBoxStyle.Exclamation, "No records found")
                Me.Close()
            End If
            ProgressThread.Close()
        Catch ex As Exception
            ProgressThread.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub


    Private Sub cbo_AllocationDate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo_AllocationDate.SelectedIndexChanged
        If cbo_AllocationDate.Items.Count <> 0 Then
            Me.FillFormDetails()
        End If
    End Sub


    Private Sub GetRequestForPayment()
        RequestForPayment = Me.WBillHelper.GetRequestForPayment()

        'Fill Combo box
        If RequestForPayment.Count = 0 Then
            'Add Error Message
            Exit Sub
        End If

        Dim lstAllocationDate = (From z In RequestForPayment _
                                 Select z.AllocationDate Distinct _
                                 Order By AllocationDate Descending).ToList

        cbo_AllocationDate.DataSource = lstAllocationDate
    End Sub


    Private Sub FillFormDetails(Optional ByVal AllocDate As Date = Nothing, Optional ByVal lstRFP As RequestForPayment = Nothing)
        Try


            'get RFP For Selected Due Date
            Dim AllocationDate As Date
            If AllocDate = Nothing Then
                AllocationDate = CDate(cbo_AllocationDate.SelectedItem.ToString)
            Else
                AllocationDate = AllocDate
            End If

            lstParticipants = WBillHelper.GetAMParticipants()



            If RequestForPayment Is Nothing Then
                SelRequestForPayment = lstRFP
                If SelRequestForPayment Is Nothing Then
                    SelRequestForPayment = New RequestForPayment
                End If

            Else
                SelRequestForPayment = (From x In RequestForPayment _
                                        Where x.AllocationDate = AllocationDate _
                                        Select x).FirstOrDefault
            End If

            dgv_CollectionDetails.Rows.Clear()
            dgv_PaymentDetails.Rows.Clear()

            Dim TotalLBC As Decimal
            Dim TotalRTGS As Decimal
            Dim TotalDeferred As Decimal
            Dim TotalPayment As Decimal

            'Fill Text Boxes
            With SelRequestForPayment
                Me.txt_RefNo.Text = CStr(.ReferenceNo.ToString("000")) '.ToString("0000000")
                Me.txt_RFPPurpose.Text = .PurposeOfPayment
                Me.txt_RFPTo.Text = .toRFP
                Me.txt_RFPFrom.Text = .FromRFP
                Me.dtp_PaymentDate.Value = .AllocationDate
                Me.txt_NSSApplied.Text = FormatNumber(.NSSAmount, 2, TriState.True, TriState.True)
                Me.txt_PRReplenish.Text = FormatNumber(.PRReplenishment, 2, TriState.True, TriState.True)
                Me.txt_MFApplication.Text = FormatNumber(.MarketFees, 2, TriState.True, TriState.True)
                Me.txt_HeldCollection.Text = FormatNumber(.HeldCollection, 2, TriState.True, TriState.True)
                Me.txt_TransferPEMC.Text = FormatNumber(.TransferToPEMC, 2, TriState.True, TriState.True)

                'Get Payments
                Dim cRFPPayments = (From x In .RFPDetails _
                                    Where x.RFPDetailsType = EnumRFPDetailsType.Payment
                                    Select x).ToList

                For Each item In cRFPPayments
                    'get Participant
                    Dim _item = item                    
                    Dim itmParticipant = (From x In lstParticipants _
                                          Where x.IDNumber = CStr(_item.Participant) _
                                          Select x).FirstOrDefault
                    dgv_PaymentDetails.Rows.Add(itmParticipant.ParticipantID, itmParticipant.IDNumber, _
                                                itmParticipant.Bank & " " & itmParticipant.BankBranch, itmParticipant.BankAccountNo, _
                                                item.PaymentType, item.Amount, item.Particulars)
                    If item.Particulars.Contains("Deferred") Then
                        TotalDeferred += item.Amount
                    Else
                        If item.PaymentType = EnumParticipantPaymentType.Check Then
                            ' Compute LBC/Checks
                            TotalLBC += item.Amount
                        ElseIf item.PaymentType = EnumParticipantPaymentType.EFT Then
                            ' Compute RTGS / EFT
                            TotalRTGS += item.Amount
                        End If
                    End If
                    TotalPayment += item.Amount
                Next

                Me.txt_TotLBC.Text = TotalLBC.ToString("#,##0.00")
                Me.txt_TotRTGS.Text = TotalRTGS.ToString("#,##0.00")
                Me.txt_totDeferred.Text = TotalDeferred.ToString("#,##0.00")
                Me.txt_TotalPayment.Text = TotalPayment.ToString("#,##0.00")

                'Get Collections
                Dim cRFPCollection = (From x In .RFPDetails _
                                      Where x.RFPDetailsType = EnumRFPDetailsType.Collection
                                      Select x Order By x.Participant Descending).ToList
                Dim TotCollectionApplied As Decimal = 0
                For Each item In cRFPCollection
                    Dim _item = item

                    Dim itmParticipant = (From x In lstParticipants _
                                              Where x.IDNumber = CStr(_item.Participant) _
                                              Select x).FirstOrDefault

                    dgv_CollectionDetails.Rows.Add(itmParticipant.ParticipantID, itmParticipant.IDNumber, _
                                               FormatDateTime(CDate(item.DateOfDeposit), DateFormat.ShortDate), IIf(item.Amount < 0, item.Amount * -1D, item.Amount), item.Particulars)

                    TotCollectionApplied += Math.Abs(_item.Amount)
                Next
                Me.txt_Collection.Text = FormatNumber(TotCollectionApplied, 2, TriState.True, TriState.True)
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try


    End Sub

    Public Sub GenerateReport(ByVal AllocationDate As Date, ByVal lstRFP As RequestForPayment)

        Me.MdiParent = MainForm
        Dim ConnectionString = AMModule.ConnectionString '"Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm3;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName '"JCLP"

        If AllocationDate <> CDate(Me.cbo_AllocationDate.Items(0)) Then
            Me.cbo_AllocationDate.Items.Add(AllocationDate)
        End If

        Me.cbo_AllocationDate.SelectedIndex = 0
        Me.FillFormDetails(AllocationDate, lstRFP)
        Me.cmd_GenerateReport_Click(Nothing, Nothing)
    End Sub

    Private Sub cmd_GenerateReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_GenerateReport.Click
        Try
            Dim ds As New DataSet

            If CDate(FormatDateTime(Me.dtp_PaymentDate.Value, DateFormat.ShortDate)) < CDate(Me.cbo_AllocationDate.SelectedItem.ToString) Then
                MsgBox("Invalid Payment Date, Cannot be less than Allocation Date", MsgBoxStyle.Critical, "Error!")
                Me.dtp_PaymentDate.Value = CDate(Me.cbo_AllocationDate.SelectedItem.ToString)
                Exit Sub
            End If

            Dim tblRFPMain As New DSReport.RFPMainDataTable
            Dim nRow = tblRFPMain.NewRow
            nRow("REF_NO") = CStr(CInt(txt_RefNo.Text).ToString("000"))
            nRow("RFP_TO") = CStr(txt_RFPTo.Text)
            nRow("RFP_FROM") = CStr(txt_RFPFrom.Text)
            nRow("ALLOCATION_DATE") = CDate(FormatDateTime(CDate(dtp_PaymentDate.Text.ToString), DateFormat.ShortDate))
            nRow("PURPOSE") = CStr(txt_RFPPurpose.Text)
            nRow("NOTE") = CStr(dgv_CollectionDetails.Text)
            nRow("TOTAL_PAYMENT") = CDec(txt_TotalPayment.Text)
            nRow("DATE") = CDate(FormatDateTime(CDate(dtp_PaymentDate.Text.ToString), DateFormat.ShortDate))
            nRow("TOTAL_DEFERRED") = CDec(txt_totDeferred.Text)
            nRow("TOTAL_LBC") = CDec(txt_TotLBC.Text)
            nRow("TOTAL_RTGS") = CDec(txt_TotRTGS.Text)
            nRow("TOTAL_NSS") = FormatNumber(CDec((txt_NSSApplied.Text)), 2, TriState.True, TriState.True, TriState.True)
            nRow("TOTAL_PRUDENTIAL") = FormatNumber(CDec((txt_PRReplenish.Text)), 2, TriState.True, TriState.True, TriState.True)
            nRow("TOTAL_MF") = FormatNumber(CDec((txt_MFApplication.Text)), 2, TriState.True, TriState.True, TriState.True)
            nRow("HELD_COLLECTION") = FormatNumber(CDec((txt_HeldCollection.Text)), 2, TriState.True, TriState.True, TriState.True)
            nRow("TO_PEMC") = FormatNumber(CDec((txt_TransferPEMC.Text)), 2, TriState.True, TriState.True, TriState.True)
            nRow("PREPARED_BY") = SelRequestForPayment.PreparedBy
            nRow("PREPARED_BY_POS") = SelRequestForPayment.PreparedByPosition
            nRow("REVIEWED_BY") = SelRequestForPayment.ReviewedBy
            nRow("REVIEWED_BY_POS") = SelRequestForPayment.ReviewedByPosition
            nRow("APPROVED_BY") = SelRequestForPayment.ApprovedBy
            nRow("APPROVED_BY_POS") = SelRequestForPayment.ApprovedByPosition

            tblRFPMain.Rows.Add(nRow)
            tblRFPMain.AcceptChanges()

            Dim tblRFPDetailsPayment As New DSReport.RFPDetailsPaymentDataTable
            For x As Integer = 0 To dgv_PaymentDetails.Rows.Count - 1
                
                Dim nRowDetails As DataRow
                nRowDetails = tblRFPDetailsPayment.NewRow
                With dgv_PaymentDetails.Rows(x)
                    Dim getParticipantInfo = (From y In lstParticipants _
                                              Where y.IDNumber = CStr(.Cells("pIDNumber").Value) _
                                              Select y).FirstOrDefault

                    nRowDetails("PAYEE") = getParticipantInfo.FullName
                    nRowDetails("ID_NO") = .Cells("pIDNumber").Value
                    nRowDetails("BANKBRANCH") = .Cells("pBankBranch").Value
                    nRowDetails("ACCOUNT_NO") = .Cells("pAccountNo").Value
                    nRowDetails("PAYMENT_TYPE") = .Cells("pPaymentType").Value
                    nRowDetails("AMOUNT") = .Cells("pAmount").Value
                    nRowDetails("PARTICULARS") = .Cells("pParticulars").Value
                    If cbo_AllocationDate.SelectedIndex = -1 Then
                        nRowDetails("ALLOCATION_DATE") = CDate(FormatDateTime(DateTime.Now, DateFormat.ShortDate))
                    Else
                        nRowDetails("ALLOCATION_DATE") = CDate(cbo_AllocationDate.SelectedItem.ToString)
                    End If

                End With
                tblRFPDetailsPayment.Rows.Add(nRowDetails)
                tblRFPDetailsPayment.AcceptChanges()
            Next

            Dim tblRFPDetailsCollection As New DSReport.RFPDetailsCollectionDataTable
            For x As Integer = 0 To dgv_CollectionDetails.Rows.Count - 1
                Dim nRowDetailsCollection As DataRow
                nRowDetailsCollection = tblRFPDetailsCollection.NewRow
                With dgv_CollectionDetails.Rows(x)
                    Dim getParticipantInfo = (From y In lstParticipants _
                                              Where y.IDNumber = CStr(.Cells("cIDNumber").Value) _
                                              Select y).FirstOrDefault

                    nRowDetailsCollection("PAYOR") = getParticipantInfo.FullName
                    nRowDetailsCollection("ID_NO") = .Cells("cIDNumber").Value
                    nRowDetailsCollection("DATE_OF_DEPOSIT") = .Cells("cDateOfDeposit").Value
                    nRowDetailsCollection("AMOUNT") = .Cells("cAmount").Value
                    nRowDetailsCollection("PARTICULARS") = .Cells("cParticulars").Value
                    If cbo_AllocationDate.SelectedIndex = -1 Then
                        nRowDetailsCollection("ALLOCATION_DATE") = CDate(FormatDateTime(DateTime.Now, DateFormat.ShortDate))
                    Else
                        nRowDetailsCollection("ALLOCATION_DATE") = CDate(cbo_AllocationDate.SelectedItem.ToString)
                    End If
                End With
                tblRFPDetailsCollection.Rows.Add(nRowDetailsCollection)
                tblRFPDetailsCollection.AcceptChanges()
            Next

            With ds.Tables
                .Add(tblRFPMain)
                .Add(tblRFPDetailsPayment)
                .Add(tblRFPDetailsCollection)
            End With
            ds.AcceptChanges()
            Dim RPTViewer As New frmReportViewer
            With RPTViewer
                .LoadRFP(ds)
                .ShowDialog()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try        
    End Sub

    Private Sub cmd_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub cmd_editTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_editTo.Click
        Me.SaveEdit(cmd_editTo, txt_RFPTo)
    End Sub

    Private Sub cmd_editFrom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_editFrom.Click
        Me.SaveEdit(cmd_editFrom, txt_RFPFrom)
    End Sub

    Private Sub cmd_editPurpose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_editPurpose.Click
        Me.SaveEdit(cmd_editPurpose, txt_RFPPurpose)
    End Sub

    Private Sub cmd_EditNote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_EditNote.Click
        Me.SaveEdit(cmd_EditNote, txt_Remarks)
    End Sub

    Private Sub SaveEdit(ByVal cmdButton As Button, ByVal txtBox As TextBox)
        Dim DefaultColor As New System.Drawing.Color
        DefaultColor = System.Drawing.Color.FromArgb(255, 255, 192)
        If cmdButton.Text = "Edit" Then
            cmdButton.Text = "Save"
            txtBox.Enabled = True
            txtBox.BackColor = Color.White
            txtBox.Focus()
        Else
            cmdButton.Text = "Edit"
            txtBox.BackColor = DefaultColor
            txtBox.Enabled = False
        End If
    End Sub

    'Private Sub cmd_DisplayDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_DisplayDetails.Click
    '    Dim frmDetails As New frmRPTRequestForPaymentDetails
    '    With frmDetails
    '        Dim TotalPayment As Decimal = 0
    '        .txt_Collection.Text = Me.txt_Collection.Text

    '        .txt_HeldCollection.Text = Me.txt_HeldCollection.Text
    '        .txt_MFApplication.Text = Me.txt_MFApplication.Text
    '        .txt_NSSApplied.Text = Me.txt_NSSApplied.Text
    '        .txt_PRReplenish.Text = Me.txt_PRReplenish.Text
    '        .txt_totDeferred.Text = Me.txt_totDeferred.Text
    '        .txt_TotLBC.Text = Me.txt_TotLBC.Text
    '        .txt_TotRTGS.Text = Me.txt_TotRTGS.Text
    '        .txt_TransferPEMC.Text = Me.txt_TransferPEMC.Text

    '        TotalPayment = (CDec(Me.txt_HeldCollection.Text) + CDec(Me.txt_MFApplication.Text) + CDec(Me.txt_NSSApplied.Text))
    '        TotalPayment += (CDec(Me.txt_PRReplenish.Text) + CDec(Me.txt_totDeferred.Text) + CDec(Me.txt_TransferPEMC.Text))
    '        TotalPayment += (CDec(Me.txt_TotLBC.Text) + CDec(Me.txt_TotRTGS.Text))

    '        .txt_Payment.Text = FormatNumber(TotalPayment.ToString, 2, TriState.True, TriState.True)
    '        .Show()
    '    End With
    'End Sub

    Private Sub dtp_PaymentDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtp_PaymentDate.ValueChanged
        If CDate(FormatDateTime(Me.dtp_PaymentDate.Value, DateFormat.ShortDate)) < CDate(Me.cbo_AllocationDate.SelectedItem.ToString) Then
            MsgBox("Invalid Payment Date, Cannot be less than Allocation Date", MsgBoxStyle.Critical, "Error!")
            Me.dtp_PaymentDate.Value = CDate(Me.cbo_AllocationDate.SelectedItem.ToString)
            Exit Sub
        End If
    End Sub

End Class