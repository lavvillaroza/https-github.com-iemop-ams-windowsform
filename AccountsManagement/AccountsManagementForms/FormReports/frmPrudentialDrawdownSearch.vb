'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmPrudentialDrawdownSearch
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     February 08, 2013
'Development Group:      Software Development and Support Division
'Description:            Class for Drawdown Notice
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   February 08, 2013       Vladimir E. Espiritu            Class initialization
'

Public Class frmPrudentialDrawdownSearch

#Region "Properties"
    Private _ListOfTransDate As List(Of String)
    Public Property ListOfTransDate() As List(Of String)
        Get
            Return _ListOfTransDate
        End Get
        Set(ByVal value As List(Of String))
            _ListOfTransDate = value
        End Set
    End Property

#End Region

    Private Sub frmPrudentialDrawdownSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.ddlTransDate.DataSource = Me.ListOfTransDate
        Me.ddlTransDate.SelectedIndex = -1
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If Me.ddlTransDate.Items.Count = 0 Then
            Exit Sub
        End If

        If Me.ddlTransDate.SelectedIndex = -1 Then
            MsgBox("Please select the transaction date!", MsgBoxStyle.Critical, "Specify the inputs")
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
End Class