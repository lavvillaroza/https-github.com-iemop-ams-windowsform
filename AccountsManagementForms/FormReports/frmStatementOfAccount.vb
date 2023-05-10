Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports System.Threading
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmStatementOfAccount
    Dim WBillHelper As WESMBillHelper
    Dim SOAHelper As New StatementOfAccountHelper
    Public isViewing As Boolean = False
    Private AppendOldSOA As Boolean = False

    Private Sub frmStatementOfAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        WBillHelper = WESMBillHelper.GetInstance
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName
        If isViewing = False Then
            SOAHelper.SOAForAdding()
            For Each item In SOAHelper.objDueDateList
                Me.DueDate_ComboBox.Items.Add(item)
            Next
            Save_Button.Enabled = False
        Else
            SOAHelper.SOAForViewing()
            For Each item In SOAHelper.objDueDateList
                Me.DueDate_ComboBox.Items.Add(item)
            Next
            Save_Button.Visible = False
            cmd_GenerateReport.Text = "View"
            Preview_Button.Text = "Export"
        End If
        
        Preview_Button.Enabled = False

    End Sub

    
    Private Sub cmd_GenerateReport_Click(sender As Object, e As EventArgs) Handles cmd_GenerateReport.Click        
        Try
            If isViewing = True Then
                If DueDate_ComboBox.SelectedIndex = -1 Then
                    Exit Sub
                End If
                ProgressThread.Show("Please wait while processing SOA Report.")
                SOAHelper.GenerateSOAViewing(DueDate_ComboBox.SelectedItem)
                dgv_SOA.DataSource = SOAHelper.GenerateSOADT()
                Me.dgv_Format()

                Preview_Button.Enabled = True
                Save_Button.Enabled = True

            Else
                SOAHelper.SelectedDueDate(DueDate_ComboBox.SelectedItem)
                If SOAHelper.VerifyIfSOACreated(DueDate_ComboBox.SelectedItem) = True Then
                    Dim OverwriteMsg As New MsgBoxResult
                    OverwriteMsg = MsgBox("Do you want to overwrite the previous Statement Of Account for the due date " & CDate(Me.DueDate_ComboBox.SelectedItem.ToString).ToShortDateString & "?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, MsgBoxStyle), "Overwrite Statement Of Account")
                    If OverwriteMsg = MsgBoxResult.No Then
                        Exit Sub
                    Else
                        AppendOldSOA = True
                        ProgressThread.Show("Please wait while processing SOA Report.")
                        SOAHelper.GenerateSOA(DueDate_ComboBox.SelectedItem)
                    End If
                Else
                    Dim ans As New MsgBoxResult
                    ans = MsgBox("Do you really want to generate the Statement Of Account for the due date " & CDate(Me.DueDate_ComboBox.SelectedItem.ToString).ToShortDateString & "?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "Generate Statement Of Account")
                    If ans = MsgBoxResult.No Then
                        MsgBox("Transaction Cancelled", MsgBoxStyle.Exclamation, "Generate Statement Of Account")
                        Exit Sub
                    End If
                    ProgressThread.Show("Please wait while processing SOA Report.")
                    SOAHelper.GenerateSOA(DueDate_ComboBox.SelectedItem)
                End If
                dgv_SOA.DataSource = SOAHelper.GenerateSOADT()
                Me.dgv_Format()

                Preview_Button.Enabled = True
                Save_Button.Enabled = True
            End If

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ProgressThread.Close()
        End Try       
    End Sub

    Private Sub DueDate_ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DueDate_ComboBox.SelectedIndexChanged
        Me.dgv_SOA.DataSource = Nothing
        Me.dgv_SOA.Rows.Clear()
    End Sub

    Private Sub dgv_Format()
        With dgv_SOA.Columns(0).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        With dgv_SOA.Columns(5).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        With dgv_SOA.Columns(6).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        With dgv_SOA.Columns(7).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        With dgv_SOA.Columns(8).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        With dgv_SOA.Columns(9).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        With dgv_SOA.Columns(10).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        With dgv_SOA.Columns(11).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        With dgv_SOA.Columns(12).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        With dgv_SOA.Columns(13).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        With dgv_SOA.Columns(14).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        With dgv_SOA.Columns(15).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With
    End Sub

    Private Sub Close_Button_Click(sender As Object, e As EventArgs) Handles Close_Button.Click
        Me.Close()
    End Sub

    Private Sub Preview_Button_Click(sender As Object, e As EventArgs) Handles Preview_Button.Click
        Try
            If isViewing = False Then
                Dim ds As New DataSet
                Dim dtMain As New DSReport.StatementOfAccountDataTable
                Dim dtDetails As New DSReport.StatementOfAccountDetailsDataTable

                Dim GetDS As DataSet = SOAHelper.GenerateSOAReport(dtMain, dtDetails)
                With frmReportViewer
                    .LoadStatementOfAccount(GetDS)
                    .Show()
                End With
            Else
                Dim filesDestination As String
                Dim fOpen As New FolderBrowserDialog
                fOpen.ShowDialog()

                If fOpen.SelectedPath = "" Then
                    MessageBox.Show("Please select file destination!", "Invalid Files Destination!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                Else
                    filesDestination = fOpen.SelectedPath
                End If

                ProgressThread.Show("Please wait while extracting to " & filesDestination)

                Dim ds As New DataSet
                Dim dtMain As New DSReport.StatementOfAccountDataTable
                Dim dtDetails As New DSReport.StatementOfAccountDetailsDataTable

                ds = SOAHelper.GenerateSOAReport(dtMain, dtDetails)

                Dim listSOA = From x In ds.Tables("StatementOfAccount").AsEnumerable() _
                              Select x("SOA_NUMBER") Distinct

                For Each item In listSOA
                    Dim seletectedSOA As Long = CLng(item.ToString())

                    Dim dtMainPerIDNumber = (From x In ds.Tables("StatementOfAccount").AsEnumerable() _
                                             Where x("SOA_NUMBER").ToString() = seletectedSOA.ToString _
                                             Select x).CopyToDataTable()
                    Dim participant As String = CStr(dtMainPerIDNumber.Rows(0)("ID_NUMBER"))

                    Dim dtDetailsPerIDNumber = (From x In ds.Tables("StatementOfAccountDetails").AsEnumerable() _
                                                Where x("SOA_NUMBER").ToString() = seletectedSOA.ToString _
                                                Select x).CopyToDataTable()

                    Dim dsReport = New DataSet()
                    dtMainPerIDNumber.TableName = "StatementOfAccount"
                    dtDetailsPerIDNumber.TableName = "StatementOfAccountDetails"
                    dsReport.Tables.Add(dtMainPerIDNumber)
                    dsReport.Tables.Add(dtDetailsPerIDNumber)

                    Dim selectedDate As Date = CDate(DueDate_ComboBox.SelectedItem)
                    Dim expReport = New RPTCollectionNotice
                    expReport.SetDataSource(dsReport)
                    expReport.SetParameterValue("paramCompanyBDOAcc", AMModule.CompanyBDOAccountName.ToString)
                    expReport.SetParameterValue("paramCompanyFullName", AMModule.CompanyFullName.ToString)
                    expReport.SetParameterValue("paramCompanyAccNum", AMModule.CompanyAccountNumber.ToString)                    
                    expReport.SetParameterValue("paramBIRDateIssued", AMModule.BIRDateIssued.ToString)
                    expReport.SetParameterValue("paramBIRValidUntil", AMModule.BIRValidUntil.ToString)
                    expReport.SetParameterValue("paramSeriesNo", AMModule.SOANumberPrefix.ToString)
                    expReport.SetParameterValue("paramBIRPermit", AMModule.BIRPermitNumber.ToString)
                    

                    expReport.ExportToDisk(ExportFormatType.PortableDocFormat, filesDestination & "\" & _
                                           participant & "_" & AMModule.SOANumberPrefix.ToString & seletectedSOA.ToString("D7") & "_" & selectedDate.ToString("MMMddyyyy") & ".pdf")
                    expReport.Close()
                    expReport.Dispose()
                Next
                ProgressThread.Close()
                MessageBox.Show("The SOA Reports exported successfully.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Save_Button_Click(sender As Object, e As EventArgs) Handles Save_Button.Click
        Try
            Dim msgAns As New MsgBoxResult
            msgAns = MsgBox("Do you really want to save the generated Statement of Account?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "System Message")

            If msgAns = MsgBoxResult.Yes Then
                ProgressThread.Show("Please wait while saving SOA Report.")
                SOAHelper.SaveSOA(AppendOldSOA)
                ProgressThread.Close()
                MessageBox.Show("The processed data have been successfully saved.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Save_Button.Visible = False
                Preview_Button.Enabled = False
            End If
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)                    
        End Try
    End Sub
End Class