Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmPaymentNewOffsetting

    Public _PaymentHelper As PaymentHelper
    Private ReadOnly Property PaymentHelper_() As PaymentHelper
        Get
            Return _PaymentHelper
        End Get
    End Property

    Public _WBillHelper As WESMBillHelper
    Private ReadOnly Property WBillHelper() As WESMBillHelper
        Get
            Return _WBillHelper
        End Get
    End Property

    Public _OffsetToInvoiceDT As New DataTable
    Private ReadOnly Property OffsetToInvoiceDT() As DataTable
        Get
            Return _OffsetToInvoiceDT
        End Get
    End Property

    Public _ARAPDT As New DataTable
    Private ReadOnly Property ARAPDT() As DataTable
        Get
            Return _ARAPDT
        End Get
    End Property

    Public HideORButton As Boolean = True
    Public isAP As Boolean = False
    Public isView As Boolean = False
    Public _MainLabel As String = ""
    Public _DetailsLabel As String = ""

    Private Sub frmPaymentNewOffsetting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.dgv_ShareOnInvoice.DataSource = OffsetToInvoiceDT
        Me.DataGridViewAPShare(Me.dgv_ShareOnInvoice)

        Me.dgv_ARAPDetails.DataSource = ARAPDT
        Me.DataGridViewARAP(Me.dgv_ARAPDetails)
        Me.GB_Main.Text = _MainLabel
        Me.GB_Details.Text = _DetailsLabel

        If HideORButton = True Then
            Me.Btn_GenerateOR.Enabled = False
        Else
            Me.Btn_GenerateOR.Enabled = True
        End If

        Me.ComputeGrandTotal()
    End Sub


    Private Sub ComputeGrandTotal()
        If isAP = True Then
            Try
                Dim TotalofAmountShare As Decimal = (From x In Me.ARAPDT.AsEnumerable() Select CDec(x("AllocationAmount"))).Sum
                Dim TotalAmountOffsetAlloc As Decimal = 0

                Dim OffsetToInvoiceList = (From x In Me.OffsetToInvoiceDT.AsEnumerable() Select CDec(x("AmountOffset"))).ToList
                If OffsetToInvoiceList.Count > 0 Then
                    TotalAmountOffsetAlloc = (From x In Me.OffsetToInvoiceDT.AsEnumerable() Select CDec(x("AmountOffset"))).Sum
                End If

                Dim TotalofAmountShareBalance As Decimal = TotalofAmountShare - TotalAmountOffsetAlloc


                Me.txtbox_TotalAmountShareARAP.Text = FormatNumber(TotalofAmountShare, UseParensForNegativeNumbers:=TriState.True)
                Me.txtbox_TotalAmountOffsetAPAR.Text = FormatNumber(TotalAmountOffsetAlloc, UseParensForNegativeNumbers:=TriState.True)
                Me.txtbox_TotalAmountShareARAPBalance.Text = FormatNumber(TotalofAmountShareBalance, UseParensForNegativeNumbers:=TriState.True)

                Me.txtbox_TotalAmountAllocARAP.Text = FormatNumber(TotalofAmountShare, UseParensForNegativeNumbers:=TriState.True)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            Try

                Dim TotalofAmountShare As Decimal = 0

                Dim ListofShareFromAP = (From x In Me.OffsetToInvoiceDT.AsEnumerable() _
                                        Group x By OffsetSeqNo = x("OffsetSeqNo"), _
                                        BillingPeriod = x("BillingPeriod"), _
                                        InvoiceNumber = x("InvoiceNumber"), _
                                        AmountShareTotal = x("AmountShare") _
                                        Into TotalAmountOffset = Sum(CDec(x("AmountOffset"))) _
                                        Select OffsetSeqNo, BillingPeriod, InvoiceNumber, AmountShareTotal, TotalAmountOffset).ToList()
                Dim _ListofShareFromAP = (From x In ListofShareFromAP Select CDec(x.AmountShareTotal)).ToList()

                Dim ListofShareFromAPOffsetToAR = (From x In Me.OffsetToInvoiceDT.AsEnumerable() Select CDec(x("AmountOffset"))).Sum
                If ListofShareFromAP.Count > 0 Then
                    TotalofAmountShare = _ListofShareFromAP.Sum
                End If

                Dim TotalofAmountShareBalance As Decimal = TotalofAmountShare - ListofShareFromAPOffsetToAR

                Me.txtbox_TotalAmountShareARAP.Text = FormatNumber(TotalofAmountShare, UseParensForNegativeNumbers:=TriState.True)
                Me.txtbox_TotalAmountOffsetAPAR.Text = FormatNumber(ListofShareFromAPOffsetToAR, UseParensForNegativeNumbers:=TriState.True)
                Me.txtbox_TotalAmountShareARAPBalance.Text = FormatNumber(TotalofAmountShareBalance, UseParensForNegativeNumbers:=TriState.True)

                Dim TotalAmountOffsetAlloc As Decimal = (From x In Me.ARAPDT.AsEnumerable() Select CDec(x("AllocationAmount"))).Sum
                Me.txtbox_TotalAmountAllocARAP.Text = FormatNumber(TotalAmountOffsetAlloc, UseParensForNegativeNumbers:=TriState.True)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End If
    End Sub

    Private Sub DataGridViewAPShare(ByVal dgv As DataGridView)
        With dgv.Columns(4).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        If dgv.Columns.Count > 5 Then
            With dgv.Columns(5).DefaultCellStyle
                .Format = "N2"
                .Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End If

        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub

    Private Sub DataGridViewARAP(ByVal dgv As DataGridView)
        With dgv.Columns(4).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(6).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(8).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(9).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        If dgv.Columns.Count > 10 Then
            With dgv.Columns(10).DefaultCellStyle
                .Format = "N2"
                .Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End If

        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub

    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Me.Close()
    End Sub

    Private Sub Btn_GenerateDMCM_Click(sender As Object, e As EventArgs) Handles Btn_GenerateDMCM.Click
        If isAP = True Then
            Dim ARAPList = (From x In Me.ARAPDT.AsEnumerable() Where Not IsDBNull(x("Remarks")) And Not x("Remarks") = "0" Select x("Remarks")).Distinct().ToList()
            Dim IDNumber = (From x In Me.ARAPDT.AsEnumerable() Select x("IDNumber")).FirstOrDefault

            'Get the Signatory                       
            Try
                If ARAPList.Count > 0 Then
                    Dim dt = PaymentHelper_.CreateDMCMList(New DSReport.DebitCreditMemoDataTable, ARAPList, IDNumber)

                    ProgressThread.Show("Please wait while generating DMCM Report.")
                    'Get the datasource for the report
                    Dim frmViewer As New frmReportViewer()
                    With frmViewer
                        .LoadDebitCreditMemo(dt)
                        ProgressThread.Close()
                        .ShowDialog()
                    End With
                Else
                    MessageBox.Show("No DMCM for Default Interest.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                ProgressThread.Close()
                MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'Updated By Lance 08/18/2014
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepDebitCreditMemoWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInGeneratingReport.ToString, AMModule.UserName)
            End Try
        Else
            Dim ARAPList = (From x In Me.ARAPDT.AsEnumerable() Where Not IsDBNull(x("Remarks")) Select x("Remarks")).Distinct().ToList()
            Dim IDNumber = (From x In Me.ARAPDT.AsEnumerable() Select x("IDNumber")).FirstOrDefault

            'Get the Signatory
            Dim Signatory = WBillHelper.GetSignatories("DMCM").First()
            Dim ListAccountCodes = WBillHelper.GetAccountingCodes()
            
            Try
                Dim dt = PaymentHelper_.CreateDMCMList(New DSReport.DebitCreditMemoDataTable, ARAPList, IDNumber)
                ProgressThread.Show("Please wait while generating DMCM Report.")

                'Get the datasource for the report
                Dim frmViewer As New frmReportViewer()
                With frmViewer
                    .LoadDebitCreditMemo(dt)
                    ProgressThread.Close()
                    .ShowDialog()
                End With

            Catch ex As Exception
                ProgressThread.Close()
                MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'Updated By Lance 08/18/2014
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepDebitCreditMemoWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInGeneratingReport.ToString, AMModule.UserName)            
            End Try
        End If
    End Sub

    Private Sub Btn_GenerateOR_Click(sender As Object, e As EventArgs) Handles Btn_GenerateOR.Click
        Try
            If isView Then

            Else
                Dim frmViewer As New frmReportViewer()
                Dim IDNumber = (From x In Me.ARAPDT.AsEnumerable() Select x("IDNumber")).FirstOrDefault

                ProgressThread.Show("Please while preparing OR.")
                Dim result = PaymentHelper_.GenerateORReport(New DSReport.OfficialReceiptMainNewDataTable, IDNumber)

                With frmViewer
                    .LoadOR(result)
                    ProgressThread.Close()
                    .ShowDialog()
                End With
            End If
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Updated By Lance 08/18/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepDebitCreditMemoWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInGeneratingReport.ToString, AMModule.UserName)
        End Try
    End Sub
End Class