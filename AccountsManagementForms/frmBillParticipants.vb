'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmBillParticipants
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     January 18, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for the maintenance of Bill Participants
'Arguments/Parameters:  
'Files/Database Tables:  BILL_PARTICIPANTS
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   January 18, 2012        Vladimir E. Espiritu                 GUI design and basic functionalities    
'   January 25, 2011        Vladimir E. Espiritu                 Added viewing OnDoubleClicked on grid
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports Microsoft.Office.Interop

Public Class frmBillParticipants

    Private WBillHelper As WESMBillHelper
    Private listOfParticipants As New List(Of AMParticipants)

    Private Sub frmBillParticipants_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.MdiParent = MainForm
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName

            listOfParticipants = WBillHelper.GetAMParticipantsAll()
            Me.DisplayOnGrid(listOfParticipants)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DGridView_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGridView.CellFormatting
        If Me.DGridView.Columns(e.ColumnIndex).Name = "colStatus" Then
            If e.Value.ToString() = EnumStatus.InActive.ToString() Then
                Me.DGridView.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Red
            Else
                Me.DGridView.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Black
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim participantid = Me.txtSearch.Text.Trim.ToUpper()

        If participantid.Length = 0 Then
            MsgBox("Specify first the participant id!", MsgBoxStyle.Critical, "Invalid")
            Me.txtSearch.Select()
            Exit Sub
        End If

        For index As Integer = 0 To Me.DGridView.RowCount - 1
            If CStr(Me.DGridView.Rows(index).Cells(1).Value).ToUpper() = participantid Then
                Me.DGridView.Rows(index).Selected = True
                Me.DGridView.CurrentCell = Me.DGridView.Rows(index).Cells(0)
                Me.txtSearch.Text = ""
                Exit Sub
            End If
        Next

        MsgBox("The participant does not exist!", MsgBoxStyle.Information, "No record found")
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim frm As New frmBillParticipantsMgt
        With frm
            .LoadType = frmBillParticipantsMgt.AMParticipantsLoadType.Add
            .ShowDialog()
        End With
        listOfParticipants = WBillHelper.GetAMParticipantsAll()
        Me.DisplayOnGrid(listOfParticipants)
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim item As New AMParticipants
        With Me.DGridView.CurrentRow
            item = (From x In listOfParticipants Where x.IDNumber = CStr(.Cells("colIDNo").Value) Select x).FirstOrDefault()
        End With

        Dim frm As New frmBillParticipantsMgt
        With frm
            .LoadType = frmBillParticipantsMgt.AMParticipantsLoadType.Edit
            .Participant = item
            .ShowDialog()
        End With
        listOfParticipants = WBillHelper.GetAMParticipantsAll()
        Me.DisplayOnGrid(listOfParticipants)
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        ListRefresh()
    End Sub

    Public Sub ListRefresh()
        Dim Participants = WBillHelper.GetAMParticipantsAll()
        Me.DisplayOnGrid(Participants)
        Me.txtSearch.Text = ""
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim participantid = Me.txtSearch.Text.Trim.ToUpper()

            If participantid.Length = 0 Then
                MsgBox("Specify first the participant id!", MsgBoxStyle.Critical, "Invalid")
                Me.txtSearch.Select()
                Exit Sub
            End If

            For index As Integer = 0 To Me.DGridView.RowCount - 1
                If CStr(Me.DGridView.Rows(index).Cells(1).Value).ToUpper() = participantid Then
                    Me.DGridView.Rows(index).Selected = True
                    Me.DGridView.CurrentCell = Me.DGridView.Rows(index).Cells(0)
                    Me.txtSearch.Text = ""
                    Exit Sub
                End If
            Next

            MsgBox("The participant does not exist!", MsgBoxStyle.Information, "No record found")
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub DGridView_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGridView.CellMouseDoubleClick

        If e.RowIndex = -1 Then
            Exit Sub
        End If

        Dim item As New AMParticipants
        With Me.DGridView.CurrentRow
            item = (From x In listOfParticipants Where x.IDNumber = CStr(.Cells("colIDNo").Value) Select x).FirstOrDefault()
        End With
        Dim frm As New frmBillParticipantsMgt
        With frm
            .LoadType = frmBillParticipantsMgt.AMParticipantsLoadType.View
            .Participant = item
            .ShowDialog()
        End With
    End Sub

#Region "Methods/Functions"
    Private Sub DisplayOnGrid(ByVal listParticipants As List(Of AMParticipants))
        Try
            Me.DGridView.Rows.Clear()
            For Each item In listParticipants
                With item
                    Me.DGridView.Rows.Add(.IDNumber, .ParticipantID, .FullName, .Status.ToString)
                End With
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

    Private Sub btn_Import_Click(sender As Object, e As EventArgs) Handles btn_Import.Click
        Dim frm As New frmBillParticiopantsImportFromCRSSDB
        With frm
            .ShowDialog()
        End With
        Me.ListRefresh()

    End Sub

    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        Dim sFolderDialog As New FolderBrowserDialog
        Dim TargetPath As String = ""
        Dim RowIndx As Integer = 0
        Dim misValue As Object = System.Reflection.Missing.Value
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlContent As Excel.Range

        With sFolderDialog
            .ShowDialog()
            If .SelectedPath.ToString.Trim.Length = 0 Then
                Exit Sub
            Else
                TargetPath = sFolderDialog.SelectedPath
            End If
        End With
        ProgressThread.Show("Please wait while exporting.")
        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add()
        xlWorkSheet = CType(xlWorkBook.Sheets(1), Excel.Worksheet)


        Try
            Dim _ParticipantsArrList As Object(,) = New Object(,) {}
            ReDim _ParticipantsArrList(listOfParticipants.Count + 1, 31)
            _ParticipantsArrList(0, 0) = "ID_NUMBER"
            _ParticipantsArrList(0, 1) = "PARTICIPANT_ID"
            _ParticipantsArrList(0, 2) = "FULL_NAME"
            _ParticipantsArrList(0, 3) = "BUSINESS_STYLE"
            _ParticipantsArrList(0, 4) = "ECOZONE_REG_CERT_NO"
            _ParticipantsArrList(0, 5) = "ECOZONE_EFFECTIVITY_DATE"
            _ParticipantsArrList(0, 6) = "GEN/LOAD"
            _ParticipantsArrList(0, 7) = "PARTICIPANT_ADDRESS"
            _ParticipantsArrList(0, 8) = "BILLING_ADDRESS"
            _ParticipantsArrList(0, 9) = "REGION"
            _ParticipantsArrList(0, 10) = "MFWHTAX"
            _ParticipantsArrList(0, 11) = "MFWHVAT"
            _ParticipantsArrList(0, 12) = "ENERGY_WHTAX"
            _ParticipantsArrList(0, 13) = "ENERGY_WHVAT"
            _ParticipantsArrList(0, 14) = "ZERO_RATED_ENERGY"
            _ParticipantsArrList(0, 15) = "ZERO_RATED_MF"
            _ParticipantsArrList(0, 16) = "VIRTUAL_ACCOUNT_NO"
            _ParticipantsArrList(0, 17) = "BANK_TRANS_CODE"
            _ParticipantsArrList(0, 18) = "BANK_ACCT_NO"
            _ParticipantsArrList(0, 19) = "BANK"
            _ParticipantsArrList(0, 20) = "BANK_BRANCH"
            _ParticipantsArrList(0, 21) = "CHECK_PAY"
            _ParticipantsArrList(0, 22) = "PAYMENT_TYPE"
            _ParticipantsArrList(0, 23) = "REP_TITLE"
            _ParticipantsArrList(0, 24) = "REP_FNAME"
            _ParticipantsArrList(0, 25) = "REP_MNAME"
            _ParticipantsArrList(0, 26) = "REP_LNAME"
            _ParticipantsArrList(0, 27) = "REP_POSITION"
            _ParticipantsArrList(0, 28) = "REP_CONTACT"
            _ParticipantsArrList(0, 29) = "REP_EMAIL"
            _ParticipantsArrList(0, 30) = "STATUS"
            _ParticipantsArrList(0, 31) = "MEMBERSHIP_TYPE"

            If listOfParticipants.Count() > 0 Then
                Dim i As Integer = 0
                For Each item In listOfParticipants
                    i += 1
                    _ParticipantsArrList(i, 0) = item.IDNumber
                    _ParticipantsArrList(i, 1) = item.ParticipantID
                    _ParticipantsArrList(i, 2) = item.FullName
                    _ParticipantsArrList(i, 3) = If(item.BusinessStyle Is Nothing, "", item.BusinessStyle)
                    _ParticipantsArrList(i, 4) = If(item.EcoZoneRegCertificateNo Is Nothing, "", item.EcoZoneRegCertificateNo)
                    If IsNothing(item.EcoZoneEffectiveDate) Then
                        _ParticipantsArrList(i, 5) = ""
                    Else
                        _ParticipantsArrList(i, 5) = item.EcoZoneEffectiveDate
                    End If
                    _ParticipantsArrList(i, 6) = item.GenLoad.ToString()
                    _ParticipantsArrList(i, 7) = item.ParticipantAddress
                    _ParticipantsArrList(i, 8) = item.BillingAddress
                    _ParticipantsArrList(i, 9) = item.Region
                    _ParticipantsArrList(i, 10) = item.MarketFeesWHTax
                    _ParticipantsArrList(i, 11) = item.MarketFeesWHVAT
                    _ParticipantsArrList(i, 12) = item.EnergyWHTax
                    _ParticipantsArrList(i, 13) = item.EnergyWHVAT
                    _ParticipantsArrList(i, 14) = item.ZeroRatedEnergy
                    _ParticipantsArrList(i, 15) = item.ZeroRatedMarketFees
                    _ParticipantsArrList(i, 16) = item.VirtualAccountNo
                    _ParticipantsArrList(i, 17) = item.BankTransactionCode
                    _ParticipantsArrList(i, 18) = item.BankAccountNo
                    _ParticipantsArrList(i, 19) = item.Bank
                    _ParticipantsArrList(i, 20) = item.BankBranch
                    _ParticipantsArrList(i, 21) = item.CheckPay.ToString()
                    _ParticipantsArrList(i, 22) = item.PaymentType.ToString()
                    _ParticipantsArrList(0, 23) = item.Representative.Title
                    _ParticipantsArrList(0, 24) = item.Representative.FName
                    _ParticipantsArrList(0, 25) = item.Representative.MName
                    _ParticipantsArrList(0, 26) = item.Representative.LName
                    _ParticipantsArrList(0, 27) = item.Representative.Position
                    _ParticipantsArrList(0, 28) = item.Representative.Contact
                    _ParticipantsArrList(0, 29) = item.Representative.EmailAddress
                    _ParticipantsArrList(i, 30) = item.Status.ToString()
                    _ParticipantsArrList(i, 31) = item.MembershipType.ToString()
                Next
                RowIndx += 1
                xlRowRange1 = DirectCast(xlWorkSheet.Cells(RowIndx, 1), Excel.Range)
                RowIndx += UBound(_ParticipantsArrList, 1)
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(RowIndx, UBound(_ParticipantsArrList, 2) + 1), Excel.Range)
                xlContent = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlContent.WrapText = False
                xlContent.ColumnWidth = 25
                xlContent.Value = _ParticipantsArrList
            End If
            Dim FileName As String = "AMS_MasterList_asof " & Date.Now().ToString("MMddyyyy")
            xlWorkBook.SaveAs(TargetPath & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                              Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
            ProgressThread.Close()
            MessageBox.Show("Exporting successfully!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            xlWorkBook.Close(False)
            xlApp.Quit()
            releaseObject(CObj(xlContent))
            releaseObject(CObj(xlRowRange2))
            releaseObject(CObj(xlRowRange1))
            releaseObject(CObj(xlWorkSheet))
            releaseObject(CObj(xlWorkBook))
            releaseObject(CObj(xlApp))
        End Try
    End Sub
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub
End Class