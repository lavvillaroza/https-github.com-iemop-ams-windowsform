' AMS
' Jonathan Saura
' 08/07/2015
'jsaura@hotmail.ph

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib
Public Class frmInterestRateEarned

    Dim WBillHelper As WESMBillHelper

    Private Sub frmInterestRateEarned_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub frmInterestRateEarned_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.MdiParent = MainForm
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        Me.GetRecords()
        Me.ResizeRedraw = True
        For Each iPost As EnumPostedTypeStatus In [Enum].GetValues(GetType(EnumPostedTypeStatus))
            cboStatus.Items.Add(iPost.ToString)
            cboSearch.Items.Add(iPost.ToString)
        Next


        Me.transactionDate.Enabled = False
        Me.txtInterestRate.Enabled = False
        Me.cboStatus.Enabled = False
        Me.rtbRemarks.Enabled = False

        Me.cmdNew.Enabled = True
        Me.cmdSave.Enabled = False
        Me.cmdCancel.Enabled = False
        Me.cmdDelete.Enabled = False
        Me.lblRecordCount.ForeColor = Color.Red

        Me.GetRecords()

    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        Me.cmdNew.Enabled = False
        Me.cmdSave.Enabled = True
        Me.cmdCancel.Enabled = True

        Me.transactionDate.Enabled = True
        Me.txtInterestRate.Enabled = True
        Me.cboStatus.Enabled = True
        Me.rtbRemarks.Enabled = True

        Me.transactionDate.Text = ""
        Me.txtInterestRate.Text = ""
        Me.cboStatus.Text = ""
        Me.rtbRemarks.Text = ""
        Me.txtid.Text = CStr(0)

        cboStatus.Items.Clear()

        For Each iPost As EnumPostedTypeStatus In [Enum].GetValues(GetType(EnumPostedTypeStatus))

            If iPost.ToString <> "Cancelled" Then
                cboStatus.Items.Add(iPost.ToString)
            End If

        Next
        Me.cboStatus.SelectedIndex = 0
        Me.cboStatus.Enabled = False
    End Sub


    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        Dim jv_no As Integer = 0

        Try

            If Me.txtInterestRate.Text.Length < 1 Then

                MsgBox("Please enter enterest earned amount")
                Me.txtInterestRate.Focus()
                Exit Sub

            ElseIf Not IsNumeric(Me.txtInterestRate.Text) Then

                MsgBox("Interest rate must be numeric value", MsgBoxStyle.Exclamation, "Warning")
                Me.txtInterestRate.Focus()
                Exit Sub

            ElseIf Me.cboStatus.SelectedText Is Nothing Then

                MsgBox("Please select status")
                Me.cboStatus.Focus()
                Exit Sub

            ElseIf Me.cboStatus.SelectedIndex < 0 Then

                MsgBox("Status is required")
                Me.cboStatus.Focus()
                Exit Sub

            End If

            If (Me.cboStatus.SelectedIndex) = 2 Then
                jv_no = CInt(WBillHelper.GetSequenceID("SEQ_AM_JV_NO"))
            End If


            Dim i As New List(Of InterestEarned)
            i = WBillHelper.GetInterestEarn(CDate(Me.transactionDate.Text))

            If CInt(Me.txtid.Text) > 0 Then

                'update data
                If i.Count <> 0 Then
                    MsgBox("Entry Type Invalid. " & vbCrLf & CDate(Me.transactionDate.Text) & " interest earned was already posted!", MsgBoxStyle.Critical, "Entry Type Invalid")
                    Exit Sub
                End If

                Me.WBillHelper.SaveInterestEarned(CDate(Me.transactionDate.Value.ToString("MM/dd/yyyy")), CDec(Me.txtInterestRate.Text), (Me.cboStatus.SelectedIndex), jv_no, CStr(Me.rtbRemarks.Text), CInt(Me.txtid.Text), 1)
                MsgBox("Interest earned successfully updated", MsgBoxStyle.Information, "Success")

            Else

                'insert new data


                If i.Count <> 0 Then

                    MsgBox("Entry Type Invalid." & vbCrLf & CDate(Me.transactionDate.Text) & " interest earned was already posted!", MsgBoxStyle.Critical, "Entry Type Invalid")
                    Exit Sub

                Else                    
                    Me.WBillHelper.SaveInterestEarned(CDate(Me.transactionDate.Text), CDec(Me.txtInterestRate.Text), (Me.cboStatus.SelectedIndex + 1), jv_no, CStr(Me.rtbRemarks.Text), 0, 0)
                    MsgBox("Interest earned successfully saved", MsgBoxStyle.Information, "Success")

                End If


            End If

            Me.transactionDate.Text = ""
            Me.txtInterestRate.Text = ""
            Me.cboStatus.Text = ""

            Me.transactionDate.Enabled = False
            Me.txtInterestRate.Enabled = False
            Me.cboStatus.Enabled = False
            Me.rtbRemarks.Enabled = False

            Me.cmdNew.Enabled = True
            Me.cmdSave.Enabled = False
            Me.cmdCancel.Enabled = False
            Me.cmdDelete.Enabled = False

            Me.GetRecords()


        Catch ex As Exception

            MsgBox(ex.Message.ToString(), MsgBoxStyle.Critical, "Error")

        End Try



    End Sub
    Public Sub GetRecords()

        Try
            Dim st As String
            Dim iEarned As New List(Of InterestEarned)
            iEarned = WBillHelper.GetInterestEarned()

            Me.lblRecordCount.Text = CStr(iEarned.Count & " records")

            If iEarned.Count <> 0 Then
                Me.dgvIntRateEarned.Rows.Clear()
                Me.dgvIntRateEarned.Rows.Add()


                For Each rec In iEarned
                    If rec.status = 2 Then

                        st = EnumPostedTypeStatus.Posted.ToString

                    ElseIf rec.status = 1 Then

                        st = EnumPostedTypeStatus.NotPosted.ToString

                    Else

                        st = EnumPostedTypeStatus.Cancelled.ToString

                    End If

                    Me.dgvIntRateEarned.Rows.Add(rec.rowId, FormatDateTime(rec.transDate, DateFormat.ShortDate), CDec(rec.intEarned), st, rec.JVNumber, rec.Remarks, FormatDateTime(rec.dtCreated, DateFormat.ShortDate), rec.updatedby)

                Next
                dgvIntRateEarned.Rows(0).Visible = False
            End If

        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")

        End Try
    End Sub

    Private Sub dgvIntRateEarned_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvIntRateEarned.CellClick
        If e.RowIndex > 0 Then
            Dim row As DataGridViewRow
            row = Me.dgvIntRateEarned.Rows(e.RowIndex)
            Me.cboStatus.Items.Clear()

            For Each iPost As EnumPostedTypeStatus In [Enum].GetValues(GetType(EnumPostedTypeStatus))
                cboStatus.Items.Add(iPost.ToString)
            Next

            Me.transactionDate.Value = CDate(row.Cells("trans_Date").Value)
            Me.txtInterestRate.Text = CDec(row.Cells("int_Earned").Value).ToString()
            Me.cboStatus.SelectedItem = CStr(row.Cells("int_Status").Value)
            Me.txtid.Text = CStr(CInt(row.Cells("ID").Value))
            Me.rtbRemarks.Text = CStr(row.Cells("int_remarks").Value)

            do_readOnly(CStr(row.Cells("int_Status").Value))

        End If
    End Sub

    Private Sub do_readOnly(status As String)

        If status <> "NotPosted" Then 'NotPosted
            Me.transactionDate.Enabled = False
            Me.txtInterestRate.Enabled = False
            Me.cboStatus.Enabled = False
            Me.cmdNew.Enabled = True
            Me.cmdSave.Enabled = False
            Me.cmdCancel.Enabled = False
            Me.rtbRemarks.Enabled = False
            Me.cmdDelete.Enabled = False

            cboStatus.Items.Clear()

            For Each iPost As EnumPostedTypeStatus In [Enum].GetValues(GetType(EnumPostedTypeStatus))
                cboStatus.Items.Add(iPost.ToString)
            Next


        Else

            Me.transactionDate.Enabled = False
            Me.txtInterestRate.Enabled = True
            Me.cboStatus.Enabled = True
            Me.rtbRemarks.Enabled = True
            Me.cmdNew.Enabled = True
            Me.cmdSave.Enabled = True
            Me.cmdCancel.Enabled = True
            Me.cmdDelete.Enabled = True

        End If

    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        'Me.Close()


        Me.transactionDate.Text = Nothing
        Me.txtInterestRate.Text = Nothing
        Me.cboStatus.Text = Nothing
        Me.rtbRemarks.Text = Nothing

        'Me.transactionDate.Enabled = False
        'Me.txtInterestRate.Enabled = False
        'Me.cboStatus.Enabled = False
        'Me.rtbRemarks.Enabled = False

        Me.cmdNew.Enabled = True
        Me.cmdDelete.Enabled = False
        Me.cmdSave.Enabled = False
        Me.cmdCancel.Enabled = False

        Me.GetRecords()

    End Sub

    Private Sub dgvIntRateEarned_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvIntRateEarned.CellEnter
        If e.RowIndex > 0 Then
            Dim row As DataGridViewRow
            row = Me.dgvIntRateEarned.Rows(e.RowIndex)

            Me.transactionDate.Value = CDate(row.Cells("trans_Date").Value)
            Me.txtInterestRate.Text = CDec(row.Cells("int_Earned").Value).ToString()
            Me.cboStatus.SelectedItem = CStr(row.Cells("int_Status").Value)
            Me.txtid.Text = CStr(CInt(row.Cells("ID").Value))
            Me.rtbRemarks.Text = CStr(row.Cells("int_remarks").Value)

            'do_readOnly(CStr(row.Cells("int_Status").Value))

        End If
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        If MsgBox("Do you really want to delete the selected item?", MsgBoxStyle.YesNo Or MsgBoxStyle.Information, "Confirm deletion") = MsgBoxResult.Yes Then

            Me.WBillHelper.DeleteInterestEarned(CInt(Me.txtid.Text))
            Me.GetRecords()
            Me.transactionDate.Text = Nothing
            Me.txtInterestRate.Text = Nothing
            Me.cboStatus.Text = Nothing
            Me.rtbRemarks.Text = Nothing

            Me.transactionDate.Enabled = False
            Me.txtInterestRate.Enabled = False
            Me.cboStatus.Enabled = False
            Me.rtbRemarks.Enabled = False

            Me.cmdNew.Enabled = True
            Me.cmdSave.Enabled = False
            Me.cmdCancel.Enabled = False

        End If

    End Sub

    Private Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        Dim sDate As Date
        Dim eDate As Date
        Dim status As Integer
        Dim st As String
        Dim iSearch As New List(Of InterestEarned)

        sDate = CDate(Me.dtSearchFrom.Text)
        eDate = CDate(Me.dtSearchTo.Text)
        status = CInt(cboSearch.SelectedIndex)

        Try
            iSearch = WBillHelper.SearchInterestEarned(sDate, eDate, status)


            If iSearch.Count <> 0 Then

                Me.dgvIntRateEarned.Rows.Clear()
                Me.dgvIntRateEarned.Rows.Add()


                For Each rec In iSearch
                    If rec.status = 2 Then

                        st = EnumPostedTypeStatus.Posted.ToString

                    ElseIf rec.status = 1 Then

                        st = EnumPostedTypeStatus.NotPosted.ToString

                    Else

                        st = EnumPostedTypeStatus.Cancelled.ToString

                    End If

                    Me.dgvIntRateEarned.Rows.Add(rec.rowId, FormatDateTime(rec.transDate, DateFormat.ShortDate), CDec(rec.intEarned), st, rec.JVNumber, rec.Remarks, FormatDateTime(rec.dtCreated, DateFormat.ShortDate), rec.updatedby)

                Next
                dgvIntRateEarned.Rows(0).Visible = False
                Me.lblRecordCount.Text = CStr(iSearch.Count & " records")
            Else
                Me.dgvIntRateEarned.Rows.Clear()
                'MsgBox(iSearch.Count & " record found!", MsgBoxStyle.Information)
                MsgBox("No record found!", MsgBoxStyle.Information)
                'GetRecords()
                Me.lblRecordCount.Text = CStr(iSearch.Count & " records")
            End If

        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")

        End Try


    End Sub


    Private Sub btnFilterClose_Click(sender As Object, e As EventArgs) Handles btnFilterClose.Click
        pnlFilter.Visible = False
    End Sub

    Private Sub btnFilterOnOff_Click(sender As Object, e As EventArgs) Handles btnFilterOnOff.Click
        pnlFilter.Visible = True
    End Sub

    Private Sub txtInterestRate_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Back Or e.KeyCode = Keys.Left Or e.KeyCode = Keys.Right Then
            e.SuppressKeyPress = False
        Else
           
        End If

    End Sub

End Class