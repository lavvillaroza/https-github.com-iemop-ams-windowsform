'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmSummaryOfCollectionAndPayment
'Orginal Author:         Juan Carlo Panopio
'File Creation Date:     October 29, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for the Generation and Viewing of Summary of Collection and Payments
'Arguments/Parameters:  
'Files/Database Tables:  AM_DMCM and AM_DMCM_DETAILS
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************

Option Explicit On
Option Strict On

Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Public Class frmSummaryOfCollectionAndPayment
    Private WBillHelper As WESMBillHelper
    Private xlHandler As ExcelHandler
    Private Sub frmSummaryOfCollectionAndPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.MdiParent = MainForm

            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName
            xlHandler = ExcelHandler.GetInstance()

            frmPayment.LoadComboItems(WBillHelper, Me.cbo_CollectionAllocDate, True)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Search.Click
        Try
            If cbo_CollectionAllocDate.SelectedIndex = -1 Then
                MsgBox("No records found", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, MsgBoxStyle), "Error")
                Exit Sub
            End If

            Dim GetPayments = WBillHelper.GetPayment(CDate(Me.cbo_CollectionAllocDate.SelectedItem.ToString))

            If GetPayments.Count = 0 Then
                MsgBox("No records found.", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, MsgBoxStyle), Me.Text)
                Exit Sub
            End If

            Dim GetPaymentAllocation = WBillHelper.GetPaymentAllocation(CDate(FormatDateTime(CDate(cbo_CollectionAllocDate.SelectedItem.ToString), DateFormat.ShortDate)))

            Me.dgView.DataSource = frmPayment.CreateSummaryOfColPay(GetPaymentAllocation, frmPayment.ConvertCollectionTable(GetPaymentAllocation, WBillHelper), WBillHelper)

            For itmCol = 0 To dgView.ColumnCount - 1
                Me.dgView.Columns(itmCol).SortMode = DataGridViewColumnSortMode.NotSortable
                For ctrCol = 0 To Me.dgView.Columns.Count - 1
                    Me.dgView.Columns(ctrCol).SortMode = DataGridViewColumnSortMode.Programmatic
                    Me.dgView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    If ctrCol > 1 Then
                        Me.dgView.Columns(ctrCol).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    End If
                Next
            Next
            Me.dgView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)
            Me.dgView.Columns(1).Frozen = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ExportExcel.Click
        Try
            Dim ds As New DataSet
            Dim sFilePath As String = ""
            xlHandler = ExcelHandler.GetInstance()
            WBillHelper = WESMBillHelper.GetInstance()

            If Me.dgView.RowCount = 0 Then
                MsgBox("No available data for export.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            ds.Tables.Add(CType(Me.dgView.DataSource, DataTable))

            Dim sfDialog As New FolderBrowserDialog
            With sfDialog
                .ShowNewFolderButton = True
                .RootFolder = Environment.SpecialFolder.MyDocuments
                .ShowDialog()

                Dim Signatories = WBillHelper.GetSignatories("S_COLPAYMENT").FirstOrDefault
                'Add Signatories
                ds.Tables(0).Rows.Add(ds.Tables(0).NewRow)
                ds.Tables(0).Rows.Add(ds.Tables(0).NewRow)
                ds.Tables(0).AcceptChanges()

                Dim dr As DataRow
                dr = ds.Tables(0).NewRow

                dr(2) = "Prepared By:"
                dr(5) = "Checked By:"
                dr(8) = "Approved By:"

                ds.Tables(0).Rows.Add(dr)
                ds.Tables(0).AcceptChanges()

                dr = ds.Tables(0).NewRow

                dr(2) = AMModule.UserName
                dr(5) = Signatories.Signatory_1
                dr(8) = Signatories.Signatory_2

                ds.Tables(0).Rows.Add(dr)
                ds.Tables(0).AcceptChanges()

                dr = ds.Tables(0).NewRow

                dr(2) = AMModule.Position
                dr(5) = Signatories.Position_1
                dr(8) = Signatories.Position_2

                ds.Tables(0).Rows.Add(dr)
                ds.Tables(0).AcceptChanges()

                sFilePath = .SelectedPath & "\Summary of Collection and Payments(Final) " & Replace(FormatDateTime(Now, DateFormat.ShortDate), "/", "") & ".xls"
            End With

            If xlHandler.ExportToExcel(sFilePath, ds, "Summary of Collection and Payments", "", ds.Tables(0).Columns.Count) = True Then
                MsgBox("Successfully completed generation of Summary of Collection and Payments", MsgBoxStyle.Information, Me.Name)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub


End Class