'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              FuncManualDMCM
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     January 21, 2013
'Development Group:      Software Development and Support Division
'Description:            GUI for the maintenance of Collection
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   January 21, 2013        Vladimir E. Espiritu                 GUI design and basic functionalities   
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class FuncManualDMCM

#Region "Initialization/Constructor"
    Public Sub New()
        Me.New(New DebitCreditMemo)
    End Sub

    Public Sub New(ByVal dmcm As DebitCreditMemo)
        Me._ItemDMCM = dmcm
        Me._HasReference = False
        Me._IsDebitAndCreditAreEqual = False
        Me._DocumentAmount = 0
        Me._IsAccountsPayable = False
        Me._IsAccountsRecievable = False
        Me._IsDuplicateAccountCode = False
    End Sub
#End Region


#Region "ItemDMCM"
    Private _ItemDMCM As DebitCreditMemo
    Public Property ItemDMCM() As DebitCreditMemo
        Get
            Return _ItemDMCM
        End Get
        Set(ByVal value As DebitCreditMemo)
            _ItemDMCM = value
        End Set
    End Property

#End Region

#Region "HasReference"
    Private _HasReference As Boolean
    Public ReadOnly Property HasReference() As Boolean
        Get
            For Each item In Me.ItemDMCM.DMCMDetails
                Select Case item.AccountCode
                    Case AMModule.CreditCode 'Accounts Receivable
                        If item.Credit <> 0 Then
                            _HasReference = True
                            Exit For
                        End If

                    Case AMModule.DebitCode  'Accounts Payable
                        If item.Debit <> 0 Then
                            _HasReference = True
                            Exit For
                        End If
                    Case Else

                End Select
            Next

            Return _HasReference
        End Get
    End Property

#End Region

#Region "IsDebitAndCreditAreEqual"
    Private _IsDebitAndCreditAreEqual As Boolean
    Public ReadOnly Property IsDebitAndCreditAreEqual() As Boolean
        Get
            Dim Credit As Decimal = 0, Debit As Decimal = 0
            For Each item In Me.ItemDMCM.DMCMDetails
                Debit += item.Debit
                Credit += item.Credit
            Next

            If Debit <> Credit Then
                _IsDebitAndCreditAreEqual = False
            Else
                _IsDebitAndCreditAreEqual = True
            End If

            Return _IsDebitAndCreditAreEqual
        End Get
    End Property

#End Region

#Region "DocumentAmount"
    Private _DocumentAmount As Decimal
    Public ReadOnly Property DocumentAmount() As Decimal
        Get
            _DocumentAmount = 0
            For Each item In Me.ItemDMCM.DMCMDetails
                _DocumentAmount += item.Debit
            Next

            Return _DocumentAmount
        End Get
    End Property


#End Region

#Region "IsAccountsPayable"
    Private _IsAccountsPayable As Boolean
    Public ReadOnly Property IsAccountsPayable() As Boolean
        Get
            For Each item In Me.ItemDMCM.DMCMDetails
                If item.AccountCode = AMModule.DebitCode Then
                    _IsAccountsPayable = True
                    Exit For
                End If
            Next

            Return _IsAccountsPayable
        End Get
    End Property
#End Region

#Region "IsAccountsRecievable"
    Private _IsAccountsRecievable As Boolean
    Public ReadOnly Property IsAccountsRecievable() As Boolean
        Get
            For Each item In Me.ItemDMCM.DMCMDetails
                If item.AccountCode = AMModule.CreditCode Then
                    _IsAccountsRecievable = True
                    Exit For
                End If
            Next

            Return _IsAccountsRecievable
        End Get
    End Property
#End Region

#Region "IsDuplicateAccountCode"
    Private _IsDuplicateAccountCode As Boolean
    Public ReadOnly Property IsDuplicateAccountCode() As Boolean
        Get
            For Each item In ItemDMCM.DMCMDetails
                Dim cnt As Integer = 0
                For Each item1 In ItemDMCM.DMCMDetails
                    If item.AccountCode = item1.AccountCode Then
                        cnt += 1
                    End If
                Next

                If cnt > 1 Then
                    Return True
                End If
            Next

            Return _IsDuplicateAccountCode
        End Get
    End Property

#End Region

End Class
