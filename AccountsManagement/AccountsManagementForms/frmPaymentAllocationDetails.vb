'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmPaymentAllocationDetails
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     July 18, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for viewing of Payment Allocation per Billing Period/Due Date/Payment Type
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description

Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmPaymentAllocationDetails
    Private WbillHelper As WESMBillHelper
    Private BFactory As BusinessFactory

    Private _lstAllocation As List(Of PaymentAllocation)
    Public Property lstAllocation() As List(Of PaymentAllocation)
        Get
            Return _lstAllocation
        End Get
        Set(ByVal value As List(Of PaymentAllocation))
            _lstAllocation = value
        End Set
    End Property

    Private FilterBillPeriod As Long
    Private FilterDueDate As Date
    Private FilterPaymentType As EnumPaymentAllocationType

    Public Sub LoadData(ByVal AllocationList As List(Of PaymentAllocation))
        Me._lstAllocation = AllocationList
        BFactory = BusinessFactory.GetInstance()
        Me.FilterBillPeriod = AllocationList.Max(Function(x As PaymentAllocation) x.BillPeriod)
        Me.FilterDueDate = AllocationList.Max(Function(x As PaymentAllocation) x.DueDate)
        Me.FilterPaymentType = AllocationList.Max(Function(x As PaymentAllocation) x.AllocationType)

        Me.LoadToDataGrid()
        Me.LoadComboFilters()
    End Sub

    Private Sub LoadToDataGrid()
        Dim FilterData = (From x In Me._lstAllocation _
                          Where x.BillPeriod = FilterBillPeriod _
                          And x.DueDate = FilterDueDate _
                          And x.AllocationType = FilterPaymentType)

        'Dim FilterData = (From x In Me._lstAllocation _
        '                  Where x.AllocationType = FilterPaymentType _
        '                  Select x).ToList

        'FilterData = (From x In Me._lstAllocation _
        '              Where x.AllocationType = EnumPaymentAllocationType.Energy _
        '              Or x.AllocationType = EnumPaymentAllocationType.DefaultInterestOnEnergy _
        '              Select x).ToList

        Dim dt As New DataTable
        With dt.Columns
            .Add("Billing Period")
            .Add("Due Date")
            .Add("Participant ID")
            .Add("Allocation Type")
            .Add("Amount")
            .Add("INV/DMCM No")
        End With

        Dim AllocationSummary = (From x In FilterData _
                                 Select x _
                                 Order By x.BillPeriod Ascending, x.DueDate Ascending, _
                                 x.AllocationType Ascending)

        For Each itmSummary In AllocationSummary
            Dim dr As DataRow
            dr = dt.NewRow



            With itmSummary
                dr("Billing Period") = .BillPeriod
                dr("Due Date") = FormatDateTime(.DueDate, DateFormat.ShortDate)
                dr("Participant ID") = .Participant.IDNumber
                dr("Allocation Type") = .AllocationType.ToString
                dr("Amount") = FormatNumber(.Amount, 2, TriState.True, TriState.True)

                Dim SumType As String = CStr(IIf(.WESMSummary.SummaryType = EnumSummaryType.DMCM, "DMCM-", "INV-"))
                If SumType = "INV-" Then
                    dr("INV/DMCM No") = .WESMSummary.INVDMCMNo
                Else
                    dr("INV/DMCM No") = Me.BFactory.GenerateBIRDocumentNumber(.WESMSummary.INVDMCMNo, BIRDocumentsType.DMCM)
                End If

            End With
            'If itmSummary.Amount < 0 Then
            dt.Rows.Add(dr)
            dt.AcceptChanges()
            'End If
        Next

        dgv_detailTable.DataSource = dt
        Me.dgv_detailTable.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    End Sub

    Private Sub LoadComboFilters()
        cbo_BillPeriod.DataSource = (From x In _lstAllocation _
                                     Select x.BillPeriod Distinct _
                                     Order By BillPeriod Descending).ToList

        Me.FilterBillPeriod = cbo_BillPeriod.SelectedItem

        cbo_DueDate.DataSource = (From x In _lstAllocation _
                                  Where x.BillPeriod = Me.FilterBillPeriod _
                                  Select x.DueDate Distinct _
                                  Order By DueDate Descending).ToList


        Me.FilterDueDate = cbo_DueDate.SelectedItem
        cbo_PaymentType.DataSource = (From x In _lstAllocation _
                                      Where x.BillPeriod = Me.FilterBillPeriod _
                                      And x.DueDate = Me.FilterDueDate _
                                      Select x.AllocationType Distinct _
                                      Order By AllocationType Descending).ToList

        Me.FilterPaymentType = cbo_PaymentType.SelectedItem

    End Sub

    Private Sub cmd_Filter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Filter.Click
        Me.FilterBillPeriod = cbo_BillPeriod.SelectedItem
        Me.FilterDueDate = cbo_DueDate.SelectedItem
        Me.FilterPaymentType = cbo_PaymentType.SelectedItem

        dgv_detailTable.DataSource = Nothing

        Me.LoadToDataGrid()
    End Sub

    Private Sub cbo_BillPeriod_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo_BillPeriod.SelectedValueChanged
        Me.FilterBillPeriod = cbo_BillPeriod.SelectedItem

        cbo_DueDate.DataSource = (From x In _lstAllocation _
                                  Where x.BillPeriod = Me.FilterBillPeriod _
                                  Select x.DueDate Distinct _
                                  Order By DueDate Descending).ToList

        Me.FilterDueDate = cbo_DueDate.SelectedItem

        cbo_PaymentType.DataSource = (From x In _lstAllocation _
                                      Where x.BillPeriod = Me.FilterBillPeriod _
                                      And x.DueDate = Me.FilterDueDate _
                                      Select x.AllocationType Distinct _
                                      Order By AllocationType Descending).ToList
    End Sub

    Private Sub cmd_GenerateReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_GenerateReport.Click

        WbillHelper = WESMBillHelper.GetInstance

        If Me.dgv_detailTable.RowCount = 0 Then
            MsgBox("No records to export", MsgBoxStyle.Exclamation, "Error Encountered")
            Exit Sub
        End If

        Dim fldrMNU As New FolderBrowserDialog
        With fldrMNU
            .ShowDialog()
            Dim fname As String = ""
            If .SelectedPath.Trim.Length = 0 Then
                MsgBox("Cancelled export to CSV.", MsgBoxStyle.Critical, "Action Cancelled")
                Exit Sub
            End If
            fname = .SelectedPath & "\AllocationDetailsFor" & Replace(FormatDateTime(Me._lstAllocation.FirstOrDefault.AllocationDate, DateFormat.ShortDate), "/", "") & "_" & Me.cbo_BillPeriod.SelectedItem.ToString & "_" & Me.cbo_PaymentType.SelectedItem.ToString & "_" & Replace(FormatDateTime(Me.cbo_DueDate.SelectedItem.ToString, DateFormat.ShortDate), "/", "") & ".csv"

            Me.WbillHelper.DataTable2CSV(Me.WbillHelper.BFactory.RemoveCommaForCSVExport(CType(Me.dgv_detailTable.DataSource, DataTable)), fname)
            MsgBox("Successfully exported to " & fname, MsgBoxStyle.Information, "Successful Export")
        End With
    End Sub

    Private Sub frmPaymentAllocationDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class

