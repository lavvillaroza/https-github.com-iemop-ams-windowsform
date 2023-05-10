
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib
Imports System.Text

Public Class frmImportCRSSMappingID
    Dim UtiImporter As ImporterUtility
    Dim WBillHelper As WESMBillHelper

    Private Sub frmImportCSVInsert_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Try
            UtiImporter = ImporterUtility.GetInstance()
            UtiImporter.ConnectionString = AMModule.ConnectionString

            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnOpenFile_Click(sender As Object, e As EventArgs) Handles btnOpenFile.Click
        Dim openFD As New OpenFileDialog

        With openFD
            .Filter = "CSV Files (*.csv)|*.csv"
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.txtDirectory.Text = .FileName
                Me.txtDirectory.SelectionStart = Me.txtDirectory.Text.Length + 1
            End If
        End With
    End Sub


    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim PathFolder As String = ""
        Dim FileName As String = ""
        Dim ListOfCRSSMappingID As New List(Of WESMInvoiceCRSSMappping)
        Dim ans As MsgBoxResult
        Try
            If Me.txtDirectory.Text.Trim.Length = 0 Then
                MsgBox("Please specify the file to be uploaded.!", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            ElseIf Not File.Exists(Me.txtDirectory.Text.Trim()) Then
                MsgBox("File does not exist!", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            PathFolder = Path.GetDirectoryName(Me.txtDirectory.Text.Trim())
            FileName = Path.GetFileName(Me.txtDirectory.Text.Trim())

            ans = MsgBox("Do you really want to save the records?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")
            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            ProgressThread.Show("Please wait while processing.")
            ListOfCRSSMappingID = UtiImporter.ImportCRSSMappingID(PathFolder, FileName)

            WBillHelper.ExecuteListOfSQL(CreateSQLScript(ListOfCRSSMappingID))

            ProgressThread.Close()
            MsgBox("Successfully uploaded to Database", MsgBoxStyle.Information, "Success!")
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function CreateSQLScript(ByVal listofCRSSMapping As List(Of WESMInvoiceCRSSMappping)) As List(Of String)
        Dim ret As New List(Of String)
        ret.Add("DELETE FROM AM_WESM_INVOICE_CRSS_MAPPING")
        For Each item In listofCRSSMapping
            ret.Add("INSERT INTO AM_WESM_INVOICE_CRSS_MAPPING(ID_NUMBER,REG_ID,NEW_REG_ID,REMARKS,UPDATED_BY) VALUES('" & item.IDNumber & "','" & item.RegIDNumber & "','" & item.NewRegIDNumber & "', '" & item.Remarks & "', '" & AMModule.UserName & "')")
        Next

        ret.TrimExcess()

        Return ret
    End Function

End Class