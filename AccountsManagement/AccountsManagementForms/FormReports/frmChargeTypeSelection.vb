'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmChargeTypeSelection
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     March 04, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for teh selection of Charge Type
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   March 04, 2012          Vladimir E. Espiritu            GUI initialization and viewing of existing records in grid.
'

Option Explicit On
Option Strict On

Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmChargeTypeSelection

#Region "InvoiceNumber"
    Private _InvoiceNumber As Long
    Public Property InvoiceNumber() As Long
        Get
            Return _InvoiceNumber
        End Get
        Set(ByVal value As Long)
            _InvoiceNumber = value
        End Set
    End Property

#End Region

#Region "ChargeType"
    Private _ChargeType As EnumChargeType
    Public Property ChargeType() As EnumChargeType
        Get
            Return _ChargeType
        End Get
        Set(ByVal value As EnumChargeType)
            _ChargeType = value
        End Set
    End Property

#End Region

#Region "ViewType"
    Private _ViewType As Integer
    Public Property ViewType() As Integer
        Get
            Return _ViewType
        End Get
        Set(ByVal value As Integer)
            _ViewType = value
        End Set
    End Property

#End Region


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Dim selectCharge As EnumChargeType

        Select Case Me.ViewType
            Case 1
                If Me.rbEnergyMF.Checked = False And Me.rbEnergyMF.Checked Then
                    MsgBox("Please select first the charge Type!", MsgBoxStyle.Critical, "Warning")
                    Exit Sub
                End If


                If Me.ChargeType = EnumChargeType.E Then
                    If Me.rbEnergyMF.Checked Then
                        selectCharge = EnumChargeType.E
                    Else
                        selectCharge = EnumChargeType.EV
                    End If
                Else
                    If Me.rbEnergyMF.Checked Then
                        selectCharge = EnumChargeType.MF
                    Else
                        selectCharge = EnumChargeType.MFV
                    End If
                End If

                Dim frm As New frmUploadedWESMBillTracking
                With frmUploadedWESMBillTracking
                    .LoadInputs()
                    .LoadWESMInvoice(Me.InvoiceNumber, selectCharge)
                    .ShowDialog()
                End With

            Case 2

                If Not Me.rbEnergyMF.Checked And Not Me.rbVAT.Checked And Not Me.rbMF.Checked And Not Me.rbVATMF.Checked Then
                    MsgBox("Please select first the charge Type!", MsgBoxStyle.Critical, "Warning")
                    Exit Sub
                End If

                If Me.rbEnergyMF.Checked Then
                    selectCharge = EnumChargeType.E
                ElseIf Me.rbVAT.Checked Then
                    selectCharge = EnumChargeType.EV
                ElseIf Me.rbMF.Checked Then
                    selectCharge = EnumChargeType.MF
                ElseIf Me.rbVATMF.Checked Then
                    selectCharge = EnumChargeType.MFV
                End If

                With frmUploadedWESMBillTracking
                    .LoadInputs()
                    .LoadWESMInvoice(Me.InvoiceNumber, selectCharge)
                    Me.Close()
                End With
        End Select

    End Sub

    Private Sub frmChargeTypeSelection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class