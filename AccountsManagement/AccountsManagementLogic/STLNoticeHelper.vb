'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             STLNoticeHelper
'Orginal Author:         Lance Arjay Villaroza
'File Creation Date:     March 28, 2016
'Development Group:      Software Development and Support Division
'Description:            Class Settlement Notice Helper
'Arguments/Parameters:   -
'Files/Database Tables:  -
'Return Value:           -
'Error codes/Exceptions: -
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description


Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Imports Excel = Microsoft.Office.Interop.Excel

Public Class STLNoticeHelper
#Region "WESMBillHelper"
    Public _WBillHelper As WESMBillHelper
    Private ReadOnly Property WBillHelper() As WESMBillHelper
        Get
            Return _WBillHelper
        End Get
    End Property
#End Region

#Region "BFactory"
    Public _BFactory As New BusinessFactory
    Private ReadOnly Property BFactory() As BusinessFactory
        Get
            Return _BFactory
        End Get
    End Property
#End Region

#Region "DAL"
    Private _DataAccess As DAL
    Public ReadOnly Property DataAccess() As DAL
        Get
            Return Me._DataAccess
        End Get
    End Property
#End Region

    Private _AMParticipantsList As New List(Of AMParticipants)
    Public STLNoticeDate As Date
    Public STLRemarks As String = ""
    Public FileCounter As Integer = 0
    Public FilePath As String = ""
    Public ProcessRemarks As String = ""

    Public Sub New()
        'Get the current instance of the dal
        Me._DataAccess = DAL.GetInstance()
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._BFactory = BusinessFactory.GetInstance
        Me._AMParticipantsList = Me.WBillHelper.GetAMParticipants()
    End Sub

    Public Sub GenerateSTLNoticeReport(ByVal ParticipantID As String)
        Try
            Dim BeginningBalances As New List(Of STLNoticeNew)
            Dim CurrentBill As New List(Of STLNoticeNew)
            Dim SPABill As New List(Of STLNoticeNew)
            Dim FirstOffsetting As New List(Of STLNoticeNew)
            Dim CollectionsAndRemittances As New List(Of STLNoticeNew)
            Dim ClosingWESMBillsBySPA As New List(Of STLNoticeNew)
            Dim OffettingOnFITParticipants As New List(Of STLNoticeNew)
            Dim EndingBalances As New List(Of STLNoticeNew)            
            Dim TransferToPayment As New List(Of STLNoticeNew)
            Dim PrudentialRefund As New List(Of STLNoticeNew)
            Dim WHTaxCertifColPay As New List(Of STLNoticeNew)


            Dim AmParticipantsInfo As AMParticipants = (From x In Me.WBillHelper.GetAMParticipants() Where x.ParticipantID = ParticipantID.ToString() Select x).FirstOrDefault()

            BeginningBalances = WBillHelper.GetSTLNoticeBB(STLNoticeDate.AddMonths(-1), AmParticipantsInfo.IDNumber)
            If Not BeginningBalances.Count > 0 Then
                BeginningBalances = WBillHelper.GetSTLNoticeOrigBB(STLNoticeDate.AddMonths(-1), AmParticipantsInfo.IDNumber)
            Else
                BeginningBalances = (From x In BeginningBalances _
                                     Where x.Energy <> 0 _
                                     Or x.VAT <> 0 _
                                     Or x.MFAndVAT <> 0 _
                                     Or x.Others <> 0 _
                                     Select x).ToList()
            End If

            CurrentBill = WBillHelper.GetSTLNoticeCB(STLNoticeDate, AmParticipantsInfo.IDNumber)
            SPABill = WBillHelper.GetSTLNoticeSPABill(STLNoticeDate, AmParticipantsInfo.IDNumber)
            FirstOffsetting = WBillHelper.GetSTLNoticeP2PC2C(STLNoticeDate, AmParticipantsInfo.IDNumber)
            CollectionsAndRemittances = WBillHelper.GetSTLNoticeCollectionsRemittances(STLNoticeDate, AmParticipantsInfo.IDNumber)
            ClosingWESMBillsBySPA = WBillHelper.GetSTLNoticeClosingWESMbySPABill(STLNoticeDate, AmParticipantsInfo.IDNumber)
            TransferToPayment = WBillHelper.GetSTLNoticeTransfer(STLNoticeDate, AmParticipantsInfo.IDNumber)
            PrudentialRefund = WBillHelper.GetSTLNoticePRRefund(STLNoticeDate, AmParticipantsInfo.IDNumber)
            WHTaxCertifColPay = WBillHelper.GetSTLNoticeWHTaxCertif(STLNoticeDate, AmParticipantsInfo.IDNumber)

            EndingBalances = WBillHelper.GetSTLNoticePrevEB(STLNoticeDate, AmParticipantsInfo.IDNumber)
            If Not EndingBalances.Count > 0 Then                
                EndingBalances = WBillHelper.GetSTLNoticeEB(STLNoticeDate, AmParticipantsInfo.IDNumber)
            End If

            Dim RecordsCount As Integer = BeginningBalances.Count + CurrentBill.Count + FirstOffsetting.Count _
                                          + CollectionsAndRemittances.Count + TransferToPayment.Count + EndingBalances.Count

            Me.ProcessRemarks = "Exporting STL Notice of " & ParticipantID
            If RecordsCount > 0 Then
                FileCounter += RecordsCount
                Me.GenerateSTLNoticeWithTemplateInExcel(FilePath, BeginningBalances, CurrentBill, SPABill,
                                                     FirstOffsetting, CollectionsAndRemittances, TransferToPayment,
                                                     ClosingWESMBillsBySPA, PrudentialRefund, WHTaxCertifColPay,
                                                     EndingBalances, AmParticipantsInfo, STLRemarks)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Private Sub GenerateSTLNoticeWithTemplateInExcel(ByVal SavingPathName As String, ByVal BeginningBalance As List(Of STLNoticeNew),
                                                    ByVal CurrentInvoice As List(Of STLNoticeNew), ByVal SPAInvoice As List(Of STLNoticeNew), ByVal FirstOffsetting As List(Of STLNoticeNew),
                                                    ByVal CollectionsRemittances As List(Of STLNoticeNew), ByVal TransferToPayment As List(Of STLNoticeNew),
                                                    ByVal ClosingWESMBillsBySPA As List(Of STLNoticeNew), ByVal PRRefund As List(Of STLNoticeNew), ByVal WHTaxCertifColPay As List(Of STLNoticeNew),
                                                    ByVal EndingBalances As List(Of STLNoticeNew), ByVal Participant As AMParticipants, Optional ByVal Remarks As String = "")
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlContentHeader As Excel.Range
        Dim xlContentBeginningBalance As Excel.Range
        Dim xlContentCurrentBills As Excel.Range
        Dim xlContentSPABills As Excel.Range
        Dim xlContentFinalStatementOffsetting As Excel.Range
        Dim xlContentSettlement As Excel.Range
        Dim xlContentEndingBalance As Excel.Range
        Dim xlContentRemarks As Excel.Range

        Dim misValue As Object = System.Reflection.Missing.Value
        Dim TemplatePathFile = AppDomain.CurrentDomain.BaseDirectory & "Excel_Template\STLNoticeTemplateNew.xltm"
        Dim rowIndex As Integer = 9

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add(TemplatePathFile)
        xlWorkSheet = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet.Name = STLNoticeDate.ToString("MMMM-dd-yyyy")

        Dim ContentHeader As Object(,) = New Object(,) {}
        Dim BeginningBalanceArr As Object(,) = New Object(,) {}
        Dim CurrentBillsArr As Object(,) = New Object(,) {}
        Dim SPABillsArr As Object(,) = New Object(,) {}
        Dim FinalStatementOffsettingArr As Object(,) = New Object(,) {}
        Dim SettlementMonthHeaderArr As Object(,) = New Object(,) {}
        Dim SettlementGrandTotalArr As Object(,) = New Object(,) {}
        Dim EndingBalanceArr As Object(,) = New Object(,) {}
        Dim RemarksArr As Object(,) = New Object(,) {}

        Try
            '********************************************* Supply Content Header
            ReDim ContentHeader(3, 0)
            ContentHeader(0, 0) = "MP Name: " & Participant.FullName.ToString()
            ContentHeader(1, 0) = "MP ID No.: " & Participant.IDNumber.ToString()
            ContentHeader(2, 0) = "As of " & STLNoticeDate.ToString("MMMM dd, yyyy")

            xlRowRange1 = DirectCast(xlWorkSheet.Cells(2, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet.Cells(4, 1), Excel.Range)
            xlContentHeader = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
            xlContentHeader.Value = ContentHeader

            '********************************************* Supply Beginning Balances
            If BeginningBalance.Count > 0 Then
                Dim BBLastIndx As Integer = 0
                Dim BBIndx As Integer = 0
                Dim BBCount As Integer = If(BeginningBalance.Count = 0, 2, BeginningBalance.Count + 1)
                ReDim BeginningBalanceArr(BBCount, 20)
                BBLastIndx = UBound(BeginningBalanceArr, 1)
                BeginningBalanceArr(0, 0) = "BEGINNING BALANCES:"
                BeginningBalanceArr(BBLastIndx, 0) = "TOTAL BEGINNING BALANCES"
                For Each Item In BeginningBalance

                    BBIndx += 1
                    BeginningBalanceArr(BBIndx, 0) = Item.BillingPeriod
                    BeginningBalanceArr(BBIndx, 1) = Item.ParticularsChargeType
                    BeginningBalanceArr(BBIndx, 2) = Item.ParticularsBillType
                    BeginningBalanceArr(BBIndx, 3) = Item.OrigDueDate.ToShortDateString.ToString()
                    BeginningBalanceArr(BBIndx, 4) = Item.WESMBillInv

                    Dim TotalPayable As Decimal = 0
                    Dim TotalReceivable As Decimal = 0
                    If Item.Energy < 0 Then
                        If Item.WESMBillInv.IndexOf("DMCM-") >= 0 Then
                            BeginningBalanceArr(BBIndx, 12) = Item.Energy
                            TotalReceivable += Item.Energy
                        Else
                            BeginningBalanceArr(BBIndx, 5) = Item.Energy
                            TotalPayable += Item.Energy
                        End If
                    ElseIf Item.Energy > 0 Then
                        If Item.WESMBillInv.IndexOf("DMCM-") >= 0 Then
                            BeginningBalanceArr(BBIndx, 5) = Item.Energy
                            TotalPayable += Item.Energy
                        Else
                            BeginningBalanceArr(BBIndx, 12) = Item.Energy
                            TotalReceivable += Item.Energy
                        End If
                    End If

                    If Item.VAT < 0 Then
                        BeginningBalanceArr(BBIndx, 6) = Item.VAT
                        TotalPayable += Item.VAT
                    ElseIf Item.VAT > 0 Then
                        BeginningBalanceArr(BBIndx, 13) = Item.VAT
                        TotalReceivable += Item.VAT
                    End If

                    If Item.DefaultOnEnergy < 0 Then
                        BeginningBalanceArr(BBIndx, 7) = Item.DefaultOnEnergy
                        TotalPayable += Item.DefaultOnEnergy
                    ElseIf Item.DefaultOnEnergy > 0 Then
                        BeginningBalanceArr(BBIndx, 14) = Item.DefaultOnEnergy
                        TotalReceivable += Item.DefaultOnEnergy
                    End If

                    If Item.MFAndVAT < 0 Then
                        BeginningBalanceArr(BBIndx, 8) = Item.MFAndVAT
                        TotalPayable += Item.MFAndVAT
                    ElseIf Item.DefaultOnEnergy > 0 Then
                        BeginningBalanceArr(BBIndx, 15) = Item.MFAndVAT
                        TotalReceivable += Item.MFAndVAT
                    End If

                    If Item.DefaultOnMFwithVAT < 0 Then
                        BeginningBalanceArr(BBIndx, 9) = Item.DefaultOnMFwithVAT
                        TotalPayable += Item.DefaultOnMFwithVAT
                    ElseIf Item.DefaultOnEnergy > 0 Then
                        BeginningBalanceArr(BBIndx, 16) = Item.DefaultOnMFwithVAT
                        TotalReceivable += Item.DefaultOnMFwithVAT
                    End If

                    If Item.Others < 0 Then
                        BeginningBalanceArr(BBIndx, 10) = Item.Others
                        TotalPayable += Item.Others
                    ElseIf Item.DefaultOnEnergy > 0 Then
                        BeginningBalanceArr(BBIndx, 17) = Item.Others
                        TotalReceivable += Item.Others
                    End If

                    BeginningBalanceArr(BBIndx, 11) = If(TotalPayable = 0, Nothing, TotalPayable)
                    BeginningBalanceArr(BBIndx, 18) = If(TotalReceivable = 0, Nothing, TotalReceivable)
                    BeginningBalanceArr(BBIndx, 20) = CDec(BeginningBalanceArr(BBIndx, 11)) + CDec(BeginningBalanceArr(BBIndx, 18))
                    'SubTotal of BeginningBalance
                    BeginningBalanceArr(BBLastIndx, 5) = CDec(BeginningBalanceArr(BBLastIndx, 5)) + CDec(BeginningBalanceArr(BBIndx, 5))
                    BeginningBalanceArr(BBLastIndx, 6) = CDec(BeginningBalanceArr(BBLastIndx, 6)) + CDec(BeginningBalanceArr(BBIndx, 6))
                    BeginningBalanceArr(BBLastIndx, 7) = CDec(BeginningBalanceArr(BBLastIndx, 7)) + CDec(BeginningBalanceArr(BBIndx, 7))
                    BeginningBalanceArr(BBLastIndx, 8) = CDec(BeginningBalanceArr(BBLastIndx, 8)) + CDec(BeginningBalanceArr(BBIndx, 8))
                    BeginningBalanceArr(BBLastIndx, 9) = CDec(BeginningBalanceArr(BBLastIndx, 9)) + CDec(BeginningBalanceArr(BBIndx, 9))
                    BeginningBalanceArr(BBLastIndx, 10) = CDec(BeginningBalanceArr(BBLastIndx, 10)) + CDec(BeginningBalanceArr(BBIndx, 10))
                    BeginningBalanceArr(BBLastIndx, 11) = CDec(BeginningBalanceArr(BBLastIndx, 11)) + CDec(BeginningBalanceArr(BBIndx, 11))
                    BeginningBalanceArr(BBLastIndx, 12) = CDec(BeginningBalanceArr(BBLastIndx, 12)) + CDec(BeginningBalanceArr(BBIndx, 12))
                    BeginningBalanceArr(BBLastIndx, 13) = CDec(BeginningBalanceArr(BBLastIndx, 13)) + CDec(BeginningBalanceArr(BBIndx, 13))
                    BeginningBalanceArr(BBLastIndx, 14) = CDec(BeginningBalanceArr(BBLastIndx, 14)) + CDec(BeginningBalanceArr(BBIndx, 14))
                    BeginningBalanceArr(BBLastIndx, 15) = CDec(BeginningBalanceArr(BBLastIndx, 15)) + CDec(BeginningBalanceArr(BBIndx, 15))
                    BeginningBalanceArr(BBLastIndx, 16) = CDec(BeginningBalanceArr(BBLastIndx, 16)) + CDec(BeginningBalanceArr(BBIndx, 16))
                    BeginningBalanceArr(BBLastIndx, 17) = CDec(BeginningBalanceArr(BBLastIndx, 17)) + CDec(BeginningBalanceArr(BBIndx, 17))
                    BeginningBalanceArr(BBLastIndx, 18) = CDec(BeginningBalanceArr(BBLastIndx, 18)) + CDec(BeginningBalanceArr(BBIndx, 18))
                    BeginningBalanceArr(BBLastIndx, 20) = CDec(BeginningBalanceArr(BBLastIndx, 20)) + CDec(BeginningBalanceArr(BBIndx, 20))
                Next
                xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                rowIndex += BBLastIndx
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 21), Excel.Range)
                xlContentBeginningBalance = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlContentBeginningBalance.Value = BeginningBalanceArr
            Else
                xlContentBeginningBalance = Nothing
            End If

            '********************************************* Supply Current Invoices
            If CurrentInvoice.Count > 0 Then
                Dim CBIndx As Integer = 0
                Dim CBLastIndx As Integer = 0
                Dim CBCount As Integer = If(CurrentInvoice.Count = 0, 2, CurrentInvoice.Count + 1)
                ReDim CurrentBillsArr(CBCount, 20)
                CBLastIndx = UBound(CurrentBillsArr, 1)
                CurrentBillsArr(0, 0) = "CURRENT BILLS:"
                CurrentBillsArr(CBLastIndx, 0) = "TOTAL CURRENT BILLS"

                For Each Item In CurrentInvoice
                    CBIndx += 1
                    CurrentBillsArr(CBIndx, 0) = Item.BillingPeriod
                    CurrentBillsArr(CBIndx, 1) = Item.ParticularsChargeType
                    CurrentBillsArr(CBIndx, 2) = Item.ParticularsBillType
                    CurrentBillsArr(CBIndx, 3) = Item.OrigDueDate.ToShortDateString.ToString()
                    CurrentBillsArr(CBIndx, 4) = Item.WESMBillInv

                    Dim TotalPayable As Decimal = 0
                    Dim TotalReceivable As Decimal = 0
                    If Item.Energy < 0 Then
                        CurrentBillsArr(CBIndx, 5) = Item.Energy
                        TotalPayable += Item.Energy
                    ElseIf Item.Energy > 0 Then
                        CurrentBillsArr(CBIndx, 12) = Item.Energy
                        TotalReceivable += Item.Energy
                    End If

                    If Item.VAT < 0 Then
                        CurrentBillsArr(CBIndx, 6) = Item.VAT
                        TotalPayable += Item.VAT
                    ElseIf Item.VAT > 0 Then
                        CurrentBillsArr(CBIndx, 13) = Item.VAT
                        TotalReceivable += Item.VAT
                    End If

                    If Item.DefaultOnEnergy < 0 Then
                        CurrentBillsArr(CBIndx, 7) = Item.DefaultOnEnergy
                        TotalPayable += Item.DefaultOnEnergy
                    ElseIf Item.DefaultOnEnergy > 0 Then
                        CurrentBillsArr(CBIndx, 14) = Item.DefaultOnEnergy
                        TotalReceivable += Item.DefaultOnEnergy
                    End If

                    If Item.MFAndVAT < 0 Then
                        CurrentBillsArr(CBIndx, 8) = Item.MFAndVAT
                        TotalPayable += Item.MFAndVAT
                    ElseIf Item.MFAndVAT > 0 Then
                        CurrentBillsArr(CBIndx, 15) = Item.MFAndVAT
                        TotalReceivable += Item.MFAndVAT
                    End If

                    If Item.DefaultOnMFwithVAT < 0 Then
                        CurrentBillsArr(CBIndx, 9) = Item.DefaultOnMFwithVAT
                        TotalPayable += Item.DefaultOnMFwithVAT
                    ElseIf Item.DefaultOnMFwithVAT > 0 Then
                        CurrentBillsArr(CBIndx, 16) = Item.DefaultOnMFwithVAT
                        TotalReceivable += Item.DefaultOnMFwithVAT
                    End If

                    If Item.Others < 0 Then
                        CurrentBillsArr(CBIndx, 10) = Item.Others
                        TotalPayable += Item.Others
                    ElseIf Item.Others > 0 Then
                        CurrentBillsArr(CBIndx, 17) = Item.Others
                        TotalReceivable += Item.Others
                    End If

                    CurrentBillsArr(CBIndx, 11) = If(TotalPayable = 0, Nothing, TotalPayable)
                    CurrentBillsArr(CBIndx, 18) = If(TotalReceivable = 0, Nothing, TotalReceivable)
                    CurrentBillsArr(CBIndx, 20) = CDec(CurrentBillsArr(CBIndx, 11)) + CDec(CurrentBillsArr(CBIndx, 18))
                    'SubTotal of BeginningBalance
                    CurrentBillsArr(CBLastIndx, 5) = CDec(CurrentBillsArr(CBLastIndx, 5)) + CDec(CurrentBillsArr(CBIndx, 5))
                    CurrentBillsArr(CBLastIndx, 6) = CDec(CurrentBillsArr(CBLastIndx, 6)) + CDec(CurrentBillsArr(CBIndx, 6))
                    CurrentBillsArr(CBLastIndx, 7) = CDec(CurrentBillsArr(CBLastIndx, 7)) + CDec(CurrentBillsArr(CBIndx, 7))
                    CurrentBillsArr(CBLastIndx, 8) = CDec(CurrentBillsArr(CBLastIndx, 8)) + CDec(CurrentBillsArr(CBIndx, 8))
                    CurrentBillsArr(CBLastIndx, 9) = CDec(CurrentBillsArr(CBLastIndx, 9)) + CDec(CurrentBillsArr(CBIndx, 9))
                    CurrentBillsArr(CBLastIndx, 10) = CDec(CurrentBillsArr(CBLastIndx, 10)) + CDec(CurrentBillsArr(CBIndx, 10))
                    CurrentBillsArr(CBLastIndx, 11) = CDec(CurrentBillsArr(CBLastIndx, 11)) + CDec(CurrentBillsArr(CBIndx, 11))
                    CurrentBillsArr(CBLastIndx, 12) = CDec(CurrentBillsArr(CBLastIndx, 12)) + CDec(CurrentBillsArr(CBIndx, 12))
                    CurrentBillsArr(CBLastIndx, 13) = CDec(CurrentBillsArr(CBLastIndx, 13)) + CDec(CurrentBillsArr(CBIndx, 13))
                    CurrentBillsArr(CBLastIndx, 14) = CDec(CurrentBillsArr(CBLastIndx, 14)) + CDec(CurrentBillsArr(CBIndx, 14))
                    CurrentBillsArr(CBLastIndx, 15) = CDec(CurrentBillsArr(CBLastIndx, 15)) + CDec(CurrentBillsArr(CBIndx, 15))
                    CurrentBillsArr(CBLastIndx, 16) = CDec(CurrentBillsArr(CBLastIndx, 16)) + CDec(CurrentBillsArr(CBIndx, 16))
                    CurrentBillsArr(CBLastIndx, 17) = CDec(CurrentBillsArr(CBLastIndx, 17)) + CDec(CurrentBillsArr(CBIndx, 17))
                    CurrentBillsArr(CBLastIndx, 18) = CDec(CurrentBillsArr(CBLastIndx, 18)) + CDec(CurrentBillsArr(CBIndx, 18))
                    CurrentBillsArr(CBLastIndx, 20) = CDec(CurrentBillsArr(CBLastIndx, 20)) + CDec(CurrentBillsArr(CBIndx, 20))
                Next
                rowIndex += 2
                xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                rowIndex += CBLastIndx
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 21), Excel.Range)
                xlContentCurrentBills = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlContentCurrentBills.Value = CurrentBillsArr
            Else
                xlContentCurrentBills = Nothing
            End If

            '********************************************* Supply SPA Invoices
            If SPAInvoice.Count > 0 Then
                Dim SPABIndx As Integer = 0
                Dim SPABLastIndx As Integer = 0
                Dim SPABCount As Integer = If(SPAInvoice.Count = 0, 2, SPAInvoice.Count + 1)
                ReDim SPABillsArr(SPABCount, 20)
                SPABLastIndx = UBound(SPABillsArr, 1)
                SPABillsArr(0, 0) = "SPA BILLS:"
                SPABillsArr(SPABLastIndx, 0) = "TOTAL SPA BILLS"

                For Each Item In SPAInvoice
                    SPABIndx += 1
                    SPABillsArr(SPABIndx, 0) = Item.BillingPeriod
                    SPABillsArr(SPABIndx, 1) = Item.ParticularsChargeType
                    SPABillsArr(SPABIndx, 2) = Item.ParticularsBillType
                    SPABillsArr(SPABIndx, 3) = Item.OrigDueDate.ToShortDateString.ToString()
                    SPABillsArr(SPABIndx, 4) = Item.WESMBillInv

                    Dim TotalPayable As Decimal = 0
                    Dim TotalReceivable As Decimal = 0
                    If Item.Energy < 0 Then
                        SPABillsArr(SPABIndx, 5) = Item.Energy
                        TotalPayable += Item.Energy
                    ElseIf Item.Energy > 0 Then
                        SPABillsArr(SPABIndx, 12) = Item.Energy
                        TotalReceivable += Item.Energy
                    End If

                    If Item.VAT < 0 Then
                        SPABillsArr(SPABIndx, 6) = Item.VAT
                        TotalPayable += Item.VAT
                    ElseIf Item.VAT > 0 Then
                        SPABillsArr(SPABIndx, 13) = Item.VAT
                        TotalReceivable += Item.VAT
                    End If

                    If Item.DefaultOnEnergy < 0 Then
                        SPABillsArr(SPABIndx, 7) = Item.DefaultOnEnergy
                        TotalPayable += Item.DefaultOnEnergy
                    ElseIf Item.DefaultOnEnergy > 0 Then
                        SPABillsArr(SPABIndx, 14) = Item.DefaultOnEnergy
                        TotalReceivable += Item.DefaultOnEnergy
                    End If

                    If Item.MFAndVAT < 0 Then
                        SPABillsArr(SPABIndx, 8) = Item.MFAndVAT
                        TotalPayable += Item.MFAndVAT
                    ElseIf Item.MFAndVAT > 0 Then
                        SPABillsArr(SPABIndx, 15) = Item.MFAndVAT
                        TotalReceivable += Item.MFAndVAT
                    End If

                    If Item.DefaultOnMFwithVAT < 0 Then
                        SPABillsArr(SPABIndx, 9) = Item.DefaultOnMFwithVAT
                        TotalPayable += Item.DefaultOnMFwithVAT
                    ElseIf Item.DefaultOnMFwithVAT > 0 Then
                        SPABillsArr(SPABIndx, 16) = Item.DefaultOnMFwithVAT
                        TotalReceivable += Item.DefaultOnMFwithVAT
                    End If

                    If Item.Others < 0 Then
                        SPABillsArr(SPABIndx, 10) = Item.Others
                        TotalPayable += Item.Others
                    ElseIf Item.Others > 0 Then
                        SPABillsArr(SPABIndx, 17) = Item.Others
                        TotalReceivable += Item.Others
                    End If

                    SPABillsArr(SPABIndx, 11) = If(TotalPayable = 0, Nothing, TotalPayable)
                    SPABillsArr(SPABIndx, 18) = If(TotalReceivable = 0, Nothing, TotalReceivable)
                    SPABillsArr(SPABIndx, 20) = CDec(SPABillsArr(SPABIndx, 11)) + CDec(SPABillsArr(SPABIndx, 18))
                    'SubTotal of BeginningBalance
                    SPABillsArr(SPABLastIndx, 5) = CDec(SPABillsArr(SPABLastIndx, 5)) + CDec(SPABillsArr(SPABIndx, 5))
                    SPABillsArr(SPABLastIndx, 6) = CDec(SPABillsArr(SPABLastIndx, 6)) + CDec(SPABillsArr(SPABIndx, 6))
                    SPABillsArr(SPABLastIndx, 7) = CDec(SPABillsArr(SPABLastIndx, 7)) + CDec(SPABillsArr(SPABIndx, 7))
                    SPABillsArr(SPABLastIndx, 8) = CDec(SPABillsArr(SPABLastIndx, 8)) + CDec(SPABillsArr(SPABIndx, 8))
                    SPABillsArr(SPABLastIndx, 9) = CDec(SPABillsArr(SPABLastIndx, 9)) + CDec(SPABillsArr(SPABIndx, 9))
                    SPABillsArr(SPABLastIndx, 10) = CDec(SPABillsArr(SPABLastIndx, 10)) + CDec(SPABillsArr(SPABIndx, 10))
                    SPABillsArr(SPABLastIndx, 11) = CDec(SPABillsArr(SPABLastIndx, 11)) + CDec(SPABillsArr(SPABIndx, 11))
                    SPABillsArr(SPABLastIndx, 12) = CDec(SPABillsArr(SPABLastIndx, 12)) + CDec(SPABillsArr(SPABIndx, 12))
                    SPABillsArr(SPABLastIndx, 13) = CDec(SPABillsArr(SPABLastIndx, 13)) + CDec(SPABillsArr(SPABIndx, 13))
                    SPABillsArr(SPABLastIndx, 14) = CDec(SPABillsArr(SPABLastIndx, 14)) + CDec(SPABillsArr(SPABIndx, 14))
                    SPABillsArr(SPABLastIndx, 15) = CDec(SPABillsArr(SPABLastIndx, 15)) + CDec(SPABillsArr(SPABIndx, 15))
                    SPABillsArr(SPABLastIndx, 16) = CDec(SPABillsArr(SPABLastIndx, 16)) + CDec(SPABillsArr(SPABIndx, 16))
                    SPABillsArr(SPABLastIndx, 17) = CDec(SPABillsArr(SPABLastIndx, 17)) + CDec(SPABillsArr(SPABIndx, 17))
                    SPABillsArr(SPABLastIndx, 18) = CDec(SPABillsArr(SPABLastIndx, 18)) + CDec(SPABillsArr(SPABIndx, 18))
                    SPABillsArr(SPABLastIndx, 20) = CDec(SPABillsArr(SPABLastIndx, 20)) + CDec(SPABillsArr(SPABIndx, 20))
                Next
                rowIndex += 2
                xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                rowIndex += SPABLastIndx
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 21), Excel.Range)
                xlContentSPABills = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlContentSPABills.Value = SPABillsArr
            Else
                xlContentSPABills = Nothing
            End If

            '********************************************* Supply Final Statement Offsetting
            If FirstOffsetting.Count > 0 Then
                Dim FOIndx As Integer = 0
                Dim FOLastIndx As Integer = 0
                Dim FOCount As Integer = If(FirstOffsetting.Count = 0, 2, FirstOffsetting.Count + 1)
                ReDim FinalStatementOffsettingArr(FOCount, 20)
                FOLastIndx = UBound(FinalStatementOffsettingArr, 1)
                FinalStatementOffsettingArr(0, 0) = "FINAL STATEMENT OFFSETTING:"
                FinalStatementOffsettingArr(FOLastIndx, 0) = "TOTAL FINAL STATEMENT OFFSETTING"

                For Each Item In FirstOffsetting
                    FOIndx += 1
                    FinalStatementOffsettingArr(FOIndx, 0) = Item.BillingPeriod
                    FinalStatementOffsettingArr(FOIndx, 1) = Item.ParticularsChargeType
                    FinalStatementOffsettingArr(FOIndx, 2) = Item.ParticularsBillType
                    FinalStatementOffsettingArr(FOIndx, 3) = Item.OrigDueDate.ToShortDateString.ToString()
                    FinalStatementOffsettingArr(FOIndx, 4) = Item.WESMBillInv

                    Dim TotalPayable As Decimal = 0
                    Dim TotalReceivable As Decimal = 0
                    If Item.Energy < 0 Then
                        FinalStatementOffsettingArr(FOIndx, 5) = Item.Energy
                        TotalPayable += Item.Energy
                    ElseIf Item.Energy > 0 Then
                        FinalStatementOffsettingArr(FOIndx, 12) = Item.Energy
                        TotalReceivable += Item.Energy
                    End If

                    If Item.VAT < 0 Then
                        FinalStatementOffsettingArr(FOIndx, 6) = Item.VAT
                        TotalPayable += Item.VAT
                    ElseIf Item.VAT > 0 Then
                        FinalStatementOffsettingArr(FOIndx, 13) = Item.VAT
                        TotalReceivable += Item.VAT
                    End If

                    If Item.DefaultOnEnergy < 0 Then
                        FinalStatementOffsettingArr(FOIndx, 7) = Item.DefaultOnEnergy
                        TotalPayable += Item.DefaultOnEnergy
                    ElseIf Item.DefaultOnEnergy > 0 Then
                        FinalStatementOffsettingArr(FOIndx, 14) = Item.DefaultOnEnergy
                        TotalReceivable += Item.DefaultOnEnergy
                    End If

                    If Item.MFAndVAT < 0 Then
                        FinalStatementOffsettingArr(FOIndx, 8) = Item.MFAndVAT
                        TotalPayable += Item.MFAndVAT
                    ElseIf Item.MFAndVAT > 0 Then
                        FinalStatementOffsettingArr(FOIndx, 15) = Item.MFAndVAT
                        TotalReceivable += Item.MFAndVAT
                    End If

                    If Item.DefaultOnMFwithVAT < 0 Then
                        FinalStatementOffsettingArr(FOIndx, 9) = Item.DefaultOnMFwithVAT
                        TotalPayable += Item.DefaultOnMFwithVAT
                    ElseIf Item.DefaultOnMFwithVAT > 0 Then
                        FinalStatementOffsettingArr(FOIndx, 16) = Item.DefaultOnMFwithVAT
                        TotalReceivable += Item.DefaultOnMFwithVAT
                    End If

                    If Item.Others < 0 Then
                        FinalStatementOffsettingArr(FOIndx, 10) = Item.Others
                        TotalPayable += Item.Others
                    ElseIf Item.Others > 0 Then
                        FinalStatementOffsettingArr(FOIndx, 17) = Item.Others
                        TotalReceivable += Item.Others
                    End If

                    FinalStatementOffsettingArr(FOIndx, 11) = If(TotalPayable = 0, Nothing, TotalPayable)
                    FinalStatementOffsettingArr(FOIndx, 18) = If(TotalReceivable = 0, Nothing, TotalReceivable)
                    FinalStatementOffsettingArr(FOIndx, 20) = CDec(FinalStatementOffsettingArr(FOIndx, 11)) + CDec(FinalStatementOffsettingArr(FOIndx, 18))
                    'SubTotal of BeginningBalance
                    FinalStatementOffsettingArr(FOLastIndx, 5) = CDec(FinalStatementOffsettingArr(FOLastIndx, 5)) + CDec(FinalStatementOffsettingArr(FOIndx, 5))
                    FinalStatementOffsettingArr(FOLastIndx, 6) = CDec(FinalStatementOffsettingArr(FOLastIndx, 6)) + CDec(FinalStatementOffsettingArr(FOIndx, 6))
                    FinalStatementOffsettingArr(FOLastIndx, 7) = CDec(FinalStatementOffsettingArr(FOLastIndx, 7)) + CDec(FinalStatementOffsettingArr(FOIndx, 7))
                    FinalStatementOffsettingArr(FOLastIndx, 8) = CDec(FinalStatementOffsettingArr(FOLastIndx, 8)) + CDec(FinalStatementOffsettingArr(FOIndx, 8))
                    FinalStatementOffsettingArr(FOLastIndx, 9) = CDec(FinalStatementOffsettingArr(FOLastIndx, 9)) + CDec(FinalStatementOffsettingArr(FOIndx, 9))
                    FinalStatementOffsettingArr(FOLastIndx, 10) = CDec(FinalStatementOffsettingArr(FOLastIndx, 10)) + CDec(FinalStatementOffsettingArr(FOIndx, 10))
                    FinalStatementOffsettingArr(FOLastIndx, 11) = CDec(FinalStatementOffsettingArr(FOLastIndx, 11)) + CDec(FinalStatementOffsettingArr(FOIndx, 11))
                    FinalStatementOffsettingArr(FOLastIndx, 12) = CDec(FinalStatementOffsettingArr(FOLastIndx, 12)) + CDec(FinalStatementOffsettingArr(FOIndx, 12))
                    FinalStatementOffsettingArr(FOLastIndx, 13) = CDec(FinalStatementOffsettingArr(FOLastIndx, 13)) + CDec(FinalStatementOffsettingArr(FOIndx, 13))
                    FinalStatementOffsettingArr(FOLastIndx, 14) = CDec(FinalStatementOffsettingArr(FOLastIndx, 14)) + CDec(FinalStatementOffsettingArr(FOIndx, 14))
                    FinalStatementOffsettingArr(FOLastIndx, 15) = CDec(FinalStatementOffsettingArr(FOLastIndx, 15)) + CDec(FinalStatementOffsettingArr(FOIndx, 15))
                    FinalStatementOffsettingArr(FOLastIndx, 16) = CDec(FinalStatementOffsettingArr(FOLastIndx, 16)) + CDec(FinalStatementOffsettingArr(FOIndx, 16))
                    FinalStatementOffsettingArr(FOLastIndx, 17) = CDec(FinalStatementOffsettingArr(FOLastIndx, 17)) + CDec(FinalStatementOffsettingArr(FOIndx, 17))
                    FinalStatementOffsettingArr(FOLastIndx, 18) = CDec(FinalStatementOffsettingArr(FOLastIndx, 18)) + CDec(FinalStatementOffsettingArr(FOIndx, 18))
                    FinalStatementOffsettingArr(FOLastIndx, 20) = CDec(FinalStatementOffsettingArr(FOLastIndx, 20)) + CDec(FinalStatementOffsettingArr(FOIndx, 20))
                Next
                rowIndex += 2
                xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                rowIndex += FOLastIndx
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 21), Excel.Range)
                xlContentFinalStatementOffsetting = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlContentFinalStatementOffsetting.Value = FinalStatementOffsettingArr
            Else
                xlContentFinalStatementOffsetting = Nothing
            End If
            '********************************************* Supply Settlement For the selected Month        
            If CollectionsRemittances.Count > 0 Or TransferToPayment.Count > 0 Or PRRefund.Count > 0 Then
                'Settlement Month Header        
                ReDim SettlementMonthHeaderArr(0, 0)
                ReDim SettlementGrandTotalArr(0, 20)
                SettlementGrandTotalArr(0, 0) = "TOTAL COLLECTIONS/REMITTANCES FOR " & MonthName(STLNoticeDate.Month, False).ToUpper.ToString()
                SettlementMonthHeaderArr(0, 0) = "SETTLEMENT FOR THE MONTH OF " & MonthName(STLNoticeDate.Month, False).ToUpper.ToString() & " " & STLNoticeDate.Year.ToString()
                rowIndex += 2
                xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                xlContentSettlement = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlContentSettlement.Value = SettlementMonthHeaderArr

                'Collection for the month
                Dim CollRemittPerDate As List(Of Date) = (From x In CollectionsRemittances Select x.CollPayAllocDate Order By CollPayAllocDate).Union _
                                                         (From y In TransferToPayment Select y.CollPayAllocDate Order By CollPayAllocDate).Union _
                                                         (From z In ClosingWESMBillsBySPA Select z.CollPayAllocDate Order By CollPayAllocDate).Union _
                                                         (From z In PRRefund Select z.CollPayAllocDate Order By CollPayAllocDate).Union _
                                                         (From t In WHTaxCertifColPay Select t.CollPayAllocDate Order By CollPayAllocDate).Distinct.ToList()

                CollRemittPerDate = CollRemittPerDate.OrderBy(Function(x) x).ToList

                Dim CollectionsList As List(Of STLNoticeNew) = (From x In CollectionsRemittances Where x.TransType = 1 Select x Order By x.BillingPeriod, x.WESMBillInv, x.IndexSorting, x.ParticularsChargeType).ToList
                Dim DrawDownList As List(Of STLNoticeNew) = (From x In CollectionsRemittances Where x.TransType = 3 Select x Order By x.BillingPeriod, x.WESMBillInv, x.IndexSorting, x.ParticularsChargeType).ToList
                Dim RemittancesList As List(Of STLNoticeNew) = (From x In CollectionsRemittances Where x.TransType = 2 Select x Order By x.BillingPeriod, x.WESMBillInv, x.IndexSorting, x.ParticularsChargeType).ToList
                Dim WESMBillsClosingSPA As List(Of STLNoticeNew) = (From x In ClosingWESMBillsBySPA Where x.TransType = 4 Select x Order By x.BillingPeriod, x.WESMBillInv, x.IndexSorting, x.ParticularsChargeType).ToList

                Dim DeferredDate As New Date
                If CollRemittPerDate.Count > 0 Then
                    For Each ItemDate In CollRemittPerDate
                        'Collections
                        Dim SettlementPerDayContentArr As Object(,) = New Object(,) {}
                        Dim GetItemPerCollectionsPerDate As List(Of STLNoticeNew) = (From x In CollectionsList Where x.CollPayAllocDate = ItemDate Select x).ToList()
                        If GetItemPerCollectionsPerDate.Count > 0 Then
                            Dim ColIndx As Integer = 0
                            Dim ColLastIndx As Integer = 0
                            ReDim SettlementPerDayContentArr(GetItemPerCollectionsPerDate.Count, 20)
                            ColLastIndx = UBound(SettlementPerDayContentArr, 1)
                            SettlementPerDayContentArr(ColLastIndx, 0) = "TOTAL COLLECTION ON " & MonthName(ItemDate.Month).ToUpper.ToString() & " " & ItemDate.Day & ", " & ItemDate.Year
                            For Each Item In GetItemPerCollectionsPerDate
                                SettlementPerDayContentArr(ColIndx, 0) = Item.BillingPeriod
                                SettlementPerDayContentArr(ColIndx, 1) = Item.ParticularsChargeType
                                SettlementPerDayContentArr(ColIndx, 2) = Item.ParticularsBillType
                                SettlementPerDayContentArr(ColIndx, 3) = Item.CollPayAllocDate.ToShortDateString
                                SettlementPerDayContentArr(ColIndx, 4) = Item.WESMBillInv
                                Dim TotalPayable As Decimal = 0
                                Dim TotalReceivable As Decimal = 0
                                If Item.Energy <> 0 Then
                                    SettlementPerDayContentArr(ColIndx, 5) = Item.Energy
                                    TotalPayable += Item.Energy
                                End If

                                If Item.VAT <> 0 Then
                                    SettlementPerDayContentArr(ColIndx, 6) = Item.VAT
                                    TotalPayable += Item.VAT
                                End If

                                If Item.DefaultOnEnergy <> 0 Then
                                    SettlementPerDayContentArr(ColIndx, 7) = Item.DefaultOnEnergy
                                    TotalPayable += Item.DefaultOnEnergy
                                End If

                                If Item.MFAndVAT <> 0 Then
                                    SettlementPerDayContentArr(ColIndx, 8) = Item.MFAndVAT
                                    TotalPayable += Item.MFAndVAT
                                End If

                                If Item.DefaultOnMFwithVAT <> 0 Then
                                    SettlementPerDayContentArr(ColIndx, 9) = Item.DefaultOnMFwithVAT
                                    TotalPayable += Item.DefaultOnMFwithVAT
                                End If

                                If Item.Others <> 0 Then
                                    SettlementPerDayContentArr(ColIndx, 10) = Item.Others
                                    TotalPayable += Item.Others
                                End If

                                SettlementPerDayContentArr(ColIndx, 11) = If(TotalPayable = 0, Nothing, TotalPayable)
                                SettlementPerDayContentArr(ColIndx, 18) = If(TotalReceivable = 0, Nothing, TotalReceivable)
                                SettlementPerDayContentArr(ColIndx, 20) = CDec(SettlementPerDayContentArr(ColIndx, 11)) + CDec(SettlementPerDayContentArr(ColIndx, 18))
                                'SubTotal of Per Day
                                SettlementPerDayContentArr(ColLastIndx, 5) = CDec(SettlementPerDayContentArr(ColLastIndx, 5)) + CDec(SettlementPerDayContentArr(ColIndx, 5))
                                SettlementPerDayContentArr(ColLastIndx, 6) = CDec(SettlementPerDayContentArr(ColLastIndx, 6)) + CDec(SettlementPerDayContentArr(ColIndx, 6))
                                SettlementPerDayContentArr(ColLastIndx, 7) = CDec(SettlementPerDayContentArr(ColLastIndx, 7)) + CDec(SettlementPerDayContentArr(ColIndx, 7))
                                SettlementPerDayContentArr(ColLastIndx, 8) = CDec(SettlementPerDayContentArr(ColLastIndx, 8)) + CDec(SettlementPerDayContentArr(ColIndx, 8))
                                SettlementPerDayContentArr(ColLastIndx, 9) = CDec(SettlementPerDayContentArr(ColLastIndx, 9)) + CDec(SettlementPerDayContentArr(ColIndx, 9))
                                SettlementPerDayContentArr(ColLastIndx, 10) = CDec(SettlementPerDayContentArr(ColLastIndx, 10)) + CDec(SettlementPerDayContentArr(ColIndx, 10))
                                SettlementPerDayContentArr(ColLastIndx, 11) = CDec(SettlementPerDayContentArr(ColLastIndx, 11)) + CDec(SettlementPerDayContentArr(ColIndx, 11))
                                SettlementPerDayContentArr(ColLastIndx, 12) = CDec(SettlementPerDayContentArr(ColLastIndx, 12)) + CDec(SettlementPerDayContentArr(ColIndx, 12))
                                SettlementPerDayContentArr(ColLastIndx, 13) = CDec(SettlementPerDayContentArr(ColLastIndx, 13)) + CDec(SettlementPerDayContentArr(ColIndx, 13))
                                SettlementPerDayContentArr(ColLastIndx, 14) = CDec(SettlementPerDayContentArr(ColLastIndx, 14)) + CDec(SettlementPerDayContentArr(ColIndx, 14))
                                SettlementPerDayContentArr(ColLastIndx, 15) = CDec(SettlementPerDayContentArr(ColLastIndx, 15)) + CDec(SettlementPerDayContentArr(ColIndx, 15))
                                SettlementPerDayContentArr(ColLastIndx, 16) = CDec(SettlementPerDayContentArr(ColLastIndx, 16)) + CDec(SettlementPerDayContentArr(ColIndx, 16))
                                SettlementPerDayContentArr(ColLastIndx, 17) = CDec(SettlementPerDayContentArr(ColLastIndx, 17)) + CDec(SettlementPerDayContentArr(ColIndx, 17))
                                SettlementPerDayContentArr(ColLastIndx, 18) = CDec(SettlementPerDayContentArr(ColLastIndx, 18)) + CDec(SettlementPerDayContentArr(ColIndx, 18))
                                SettlementPerDayContentArr(ColLastIndx, 20) = CDec(SettlementPerDayContentArr(ColLastIndx, 20)) + CDec(SettlementPerDayContentArr(ColIndx, 20))

                                ColIndx += 1
                            Next

                            SettlementGrandTotalArr(0, 5) = CDec(SettlementGrandTotalArr(0, 5)) + CDec(SettlementPerDayContentArr(ColLastIndx, 5))
                            SettlementGrandTotalArr(0, 6) = CDec(SettlementGrandTotalArr(0, 6)) + CDec(SettlementPerDayContentArr(ColLastIndx, 6))
                            SettlementGrandTotalArr(0, 7) = CDec(SettlementGrandTotalArr(0, 7)) + CDec(SettlementPerDayContentArr(ColLastIndx, 7))
                            SettlementGrandTotalArr(0, 8) = CDec(SettlementGrandTotalArr(0, 8)) + CDec(SettlementPerDayContentArr(ColLastIndx, 8))
                            SettlementGrandTotalArr(0, 9) = CDec(SettlementGrandTotalArr(0, 9)) + CDec(SettlementPerDayContentArr(ColLastIndx, 9))
                            SettlementGrandTotalArr(0, 10) = CDec(SettlementGrandTotalArr(0, 10)) + CDec(SettlementPerDayContentArr(ColLastIndx, 10))
                            SettlementGrandTotalArr(0, 11) = CDec(SettlementGrandTotalArr(0, 11)) + CDec(SettlementPerDayContentArr(ColLastIndx, 11))
                            SettlementGrandTotalArr(0, 12) = CDec(SettlementGrandTotalArr(0, 12)) + CDec(SettlementPerDayContentArr(ColLastIndx, 12))
                            SettlementGrandTotalArr(0, 13) = CDec(SettlementGrandTotalArr(0, 13)) + CDec(SettlementPerDayContentArr(ColLastIndx, 13))
                            SettlementGrandTotalArr(0, 14) = CDec(SettlementGrandTotalArr(0, 14)) + CDec(SettlementPerDayContentArr(ColLastIndx, 14))
                            SettlementGrandTotalArr(0, 15) = CDec(SettlementGrandTotalArr(0, 15)) + CDec(SettlementPerDayContentArr(ColLastIndx, 15))
                            SettlementGrandTotalArr(0, 16) = CDec(SettlementGrandTotalArr(0, 16)) + CDec(SettlementPerDayContentArr(ColLastIndx, 16))
                            SettlementGrandTotalArr(0, 17) = CDec(SettlementGrandTotalArr(0, 17)) + CDec(SettlementPerDayContentArr(ColLastIndx, 17))
                            SettlementGrandTotalArr(0, 18) = CDec(SettlementGrandTotalArr(0, 18)) + CDec(SettlementPerDayContentArr(ColLastIndx, 18))
                            SettlementGrandTotalArr(0, 20) = CDec(SettlementGrandTotalArr(0, 20)) + CDec(SettlementPerDayContentArr(ColLastIndx, 20))

                            rowIndex += 1
                            xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                            rowIndex += ColLastIndx
                            xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 21), Excel.Range)
                            xlContentSettlement = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                            xlContentSettlement.Value = SettlementPerDayContentArr
                        End If

                        'DrawDown
                        SettlementPerDayContentArr = New Object(,) {}
                        Dim GetItemPerDrawDownPerDate As List(Of STLNoticeNew) = (From x In DrawDownList Where x.CollPayAllocDate = ItemDate Select x).ToList()
                        If GetItemPerDrawDownPerDate.Count > 0 Then
                            Dim DDIndx As Integer = 0
                            Dim DDLastIndx As Integer = 0
                            ReDim SettlementPerDayContentArr(GetItemPerDrawDownPerDate.Count, 20)
                            DDLastIndx = UBound(SettlementPerDayContentArr, 1)
                            SettlementPerDayContentArr(DDLastIndx, 0) = "TOTAL DRAWDOWN ON " & MonthName(ItemDate.Month).ToUpper.ToString() & " " & ItemDate.Day & ", " & ItemDate.Year
                            For Each Item In GetItemPerDrawDownPerDate
                                SettlementPerDayContentArr(DDIndx, 0) = Item.BillingPeriod
                                SettlementPerDayContentArr(DDIndx, 1) = Item.ParticularsChargeType
                                SettlementPerDayContentArr(DDIndx, 2) = Item.ParticularsBillType
                                SettlementPerDayContentArr(DDIndx, 3) = Item.CollPayAllocDate.ToShortDateString
                                SettlementPerDayContentArr(DDIndx, 4) = Item.WESMBillInv
                                Dim TotalPayable As Decimal = 0
                                Dim TotalReceivable As Decimal = 0
                                If Item.Energy <> 0 Then
                                    SettlementPerDayContentArr(DDIndx, 5) = Item.Energy
                                    TotalPayable += Item.Energy
                                End If

                                If Item.VAT <> 0 Then
                                    SettlementPerDayContentArr(DDIndx, 6) = Item.VAT
                                    TotalPayable += Item.VAT
                                End If

                                If Item.DefaultOnEnergy <> 0 Then
                                    SettlementPerDayContentArr(DDIndx, 7) = Item.DefaultOnEnergy
                                    TotalPayable += Item.DefaultOnEnergy
                                End If

                                If Item.MFAndVAT <> 0 Then
                                    SettlementPerDayContentArr(DDIndx, 8) = Item.MFAndVAT
                                    TotalPayable += Item.MFAndVAT
                                End If

                                If Item.DefaultOnMFwithVAT <> 0 Then
                                    SettlementPerDayContentArr(DDIndx, 9) = Item.DefaultOnMFwithVAT
                                    TotalPayable += Item.DefaultOnMFwithVAT
                                End If

                                If Item.Others <> 0 Then
                                    SettlementPerDayContentArr(DDIndx, 10) = Item.Others
                                    TotalPayable += Item.Others
                                End If

                                SettlementPerDayContentArr(DDIndx, 11) = If(TotalPayable = 0, Nothing, TotalPayable)
                                SettlementPerDayContentArr(DDIndx, 18) = If(TotalReceivable = 0, Nothing, TotalReceivable)
                                SettlementPerDayContentArr(DDIndx, 20) = CDec(SettlementPerDayContentArr(DDIndx, 11)) + CDec(SettlementPerDayContentArr(DDIndx, 18))
                                'SubTotal of Per Day
                                SettlementPerDayContentArr(DDLastIndx, 5) = CDec(SettlementPerDayContentArr(DDLastIndx, 5)) + CDec(SettlementPerDayContentArr(DDIndx, 5))
                                SettlementPerDayContentArr(DDLastIndx, 6) = CDec(SettlementPerDayContentArr(DDLastIndx, 6)) + CDec(SettlementPerDayContentArr(DDIndx, 6))
                                SettlementPerDayContentArr(DDLastIndx, 7) = CDec(SettlementPerDayContentArr(DDLastIndx, 7)) + CDec(SettlementPerDayContentArr(DDIndx, 7))
                                SettlementPerDayContentArr(DDLastIndx, 8) = CDec(SettlementPerDayContentArr(DDLastIndx, 8)) + CDec(SettlementPerDayContentArr(DDIndx, 8))
                                SettlementPerDayContentArr(DDLastIndx, 9) = CDec(SettlementPerDayContentArr(DDLastIndx, 9)) + CDec(SettlementPerDayContentArr(DDIndx, 9))
                                SettlementPerDayContentArr(DDLastIndx, 10) = CDec(SettlementPerDayContentArr(DDLastIndx, 10)) + CDec(SettlementPerDayContentArr(DDIndx, 10))
                                SettlementPerDayContentArr(DDLastIndx, 11) = CDec(SettlementPerDayContentArr(DDLastIndx, 11)) + CDec(SettlementPerDayContentArr(DDIndx, 11))
                                SettlementPerDayContentArr(DDLastIndx, 12) = CDec(SettlementPerDayContentArr(DDLastIndx, 12)) + CDec(SettlementPerDayContentArr(DDIndx, 12))
                                SettlementPerDayContentArr(DDLastIndx, 13) = CDec(SettlementPerDayContentArr(DDLastIndx, 13)) + CDec(SettlementPerDayContentArr(DDIndx, 13))
                                SettlementPerDayContentArr(DDLastIndx, 14) = CDec(SettlementPerDayContentArr(DDLastIndx, 14)) + CDec(SettlementPerDayContentArr(DDIndx, 14))
                                SettlementPerDayContentArr(DDLastIndx, 15) = CDec(SettlementPerDayContentArr(DDLastIndx, 15)) + CDec(SettlementPerDayContentArr(DDIndx, 15))
                                SettlementPerDayContentArr(DDLastIndx, 16) = CDec(SettlementPerDayContentArr(DDLastIndx, 16)) + CDec(SettlementPerDayContentArr(DDIndx, 16))
                                SettlementPerDayContentArr(DDLastIndx, 17) = CDec(SettlementPerDayContentArr(DDLastIndx, 17)) + CDec(SettlementPerDayContentArr(DDIndx, 17))
                                SettlementPerDayContentArr(DDLastIndx, 18) = CDec(SettlementPerDayContentArr(DDLastIndx, 18)) + CDec(SettlementPerDayContentArr(DDIndx, 18))
                                SettlementPerDayContentArr(DDLastIndx, 20) = CDec(SettlementPerDayContentArr(DDLastIndx, 20)) + CDec(SettlementPerDayContentArr(DDIndx, 20))

                                DDIndx += 1
                            Next

                            SettlementGrandTotalArr(0, 5) = CDec(SettlementGrandTotalArr(0, 5)) + CDec(SettlementPerDayContentArr(DDLastIndx, 5))
                            SettlementGrandTotalArr(0, 6) = CDec(SettlementGrandTotalArr(0, 6)) + CDec(SettlementPerDayContentArr(DDLastIndx, 6))
                            SettlementGrandTotalArr(0, 7) = CDec(SettlementGrandTotalArr(0, 7)) + CDec(SettlementPerDayContentArr(DDLastIndx, 7))
                            SettlementGrandTotalArr(0, 8) = CDec(SettlementGrandTotalArr(0, 8)) + CDec(SettlementPerDayContentArr(DDLastIndx, 8))
                            SettlementGrandTotalArr(0, 9) = CDec(SettlementGrandTotalArr(0, 9)) + CDec(SettlementPerDayContentArr(DDLastIndx, 9))
                            SettlementGrandTotalArr(0, 10) = CDec(SettlementGrandTotalArr(0, 10)) + CDec(SettlementPerDayContentArr(DDLastIndx, 10))
                            SettlementGrandTotalArr(0, 11) = CDec(SettlementGrandTotalArr(0, 11)) + CDec(SettlementPerDayContentArr(DDLastIndx, 11))
                            SettlementGrandTotalArr(0, 12) = CDec(SettlementGrandTotalArr(0, 12)) + CDec(SettlementPerDayContentArr(DDLastIndx, 12))
                            SettlementGrandTotalArr(0, 13) = CDec(SettlementGrandTotalArr(0, 13)) + CDec(SettlementPerDayContentArr(DDLastIndx, 13))
                            SettlementGrandTotalArr(0, 14) = CDec(SettlementGrandTotalArr(0, 14)) + CDec(SettlementPerDayContentArr(DDLastIndx, 14))
                            SettlementGrandTotalArr(0, 15) = CDec(SettlementGrandTotalArr(0, 15)) + CDec(SettlementPerDayContentArr(DDLastIndx, 15))
                            SettlementGrandTotalArr(0, 16) = CDec(SettlementGrandTotalArr(0, 16)) + CDec(SettlementPerDayContentArr(DDLastIndx, 16))
                            SettlementGrandTotalArr(0, 17) = CDec(SettlementGrandTotalArr(0, 17)) + CDec(SettlementPerDayContentArr(DDLastIndx, 17))
                            SettlementGrandTotalArr(0, 18) = CDec(SettlementGrandTotalArr(0, 18)) + CDec(SettlementPerDayContentArr(DDLastIndx, 18))
                            SettlementGrandTotalArr(0, 20) = CDec(SettlementGrandTotalArr(0, 20)) + CDec(SettlementPerDayContentArr(DDLastIndx, 20))

                            rowIndex += 1
                            xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                            rowIndex += DDLastIndx
                            xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 21), Excel.Range)
                            xlContentSettlement = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                            xlContentSettlement.Value = SettlementPerDayContentArr
                        End If

                        'Closing WESMBILLS By SPA
                        SettlementPerDayContentArr = New Object(,) {}
                        Dim GetItemPerClosingWBBySPAPerDate As List(Of STLNoticeNew) = (From x In WESMBillsClosingSPA Where x.CollPayAllocDate = ItemDate Select x).ToList()
                        If GetItemPerClosingWBBySPAPerDate.Count > 0 Then
                            Dim DDIndx As Integer = 0
                            Dim DDLastIndx As Integer = 0
                            ReDim SettlementPerDayContentArr(GetItemPerClosingWBBySPAPerDate.Count + 1, 20)
                            DDLastIndx = UBound(SettlementPerDayContentArr, 1)
                            SettlementPerDayContentArr(DDLastIndx, 0) = "TO CLOSE INVOICES SUBJECT TO SPA ON " & MonthName(ItemDate.Month).ToUpper.ToString() & " " & ItemDate.Day & ", " & ItemDate.Year
                            Dim getCountGetItemPerClosingWBBySPAPerDate As Long = GetItemPerClosingWBBySPAPerDate.Count
                            Dim TotalSPA As Decimal = 0
                            For Each Item In GetItemPerClosingWBBySPAPerDate
                                SettlementPerDayContentArr(DDIndx, 0) = Item.BillingPeriod
                                SettlementPerDayContentArr(DDIndx, 1) = Item.ParticularsChargeType
                                SettlementPerDayContentArr(DDIndx, 2) = Item.ParticularsBillType
                                SettlementPerDayContentArr(DDIndx, 3) = Item.CollPayAllocDate.ToShortDateString
                                SettlementPerDayContentArr(DDIndx, 4) = Item.WESMBillInv
                                Dim TotalPayable As Decimal = 0
                                Dim TotalReceivable As Decimal = 0

                                If Item.Energy < 0 Then
                                    SettlementPerDayContentArr(DDIndx, 5) = Item.Energy
                                    TotalPayable += Item.Energy
                                    TotalSPA += Item.Energy
                                ElseIf Item.Energy >= 0 Then
                                    SettlementPerDayContentArr(DDIndx, 12) = Item.Energy
                                    TotalReceivable += Item.Energy
                                    TotalSPA += Item.Energy
                                End If

                                If Item.VAT < 0 Then
                                    SettlementPerDayContentArr(DDIndx, 6) = Item.VAT
                                    TotalPayable += Item.VAT
                                    TotalSPA += Item.VAT
                                ElseIf Item.VAT >= 0 Then
                                    SettlementPerDayContentArr(DDIndx, 13) = Item.VAT
                                    TotalReceivable += Item.VAT
                                    TotalSPA += Item.VAT
                                End If

                                If Item.DefaultOnEnergy < 0 Then
                                    SettlementPerDayContentArr(DDIndx, 7) = Item.DefaultOnEnergy
                                    TotalPayable += Item.DefaultOnEnergy
                                    TotalSPA += Item.DefaultOnEnergy
                                ElseIf Item.DefaultOnEnergy >= 0 Then
                                    SettlementPerDayContentArr(DDIndx, 14) = Item.DefaultOnEnergy
                                    TotalReceivable += Item.DefaultOnEnergy
                                    TotalSPA += Item.DefaultOnEnergy
                                End If

                                If Item.MFAndVAT < 0 Then
                                    SettlementPerDayContentArr(DDIndx, 8) = Item.MFAndVAT
                                    TotalPayable += Item.MFAndVAT
                                    TotalSPA += Item.MFAndVAT
                                ElseIf Item.MFAndVAT >= 0 Then
                                    SettlementPerDayContentArr(DDIndx, 15) = Item.MFAndVAT
                                    TotalReceivable += Item.MFAndVAT
                                    TotalSPA += Item.MFAndVAT
                                End If

                                If Item.DefaultOnMFwithVAT < 0 Then
                                    SettlementPerDayContentArr(DDIndx, 9) = Item.DefaultOnMFwithVAT
                                    TotalPayable += Item.DefaultOnMFwithVAT
                                    TotalSPA += Item.DefaultOnMFwithVAT
                                ElseIf Item.DefaultOnMFwithVAT >= 0 Then
                                    SettlementPerDayContentArr(DDIndx, 16) = Item.DefaultOnMFwithVAT
                                    TotalReceivable += Item.DefaultOnMFwithVAT
                                    TotalSPA += Item.DefaultOnMFwithVAT
                                End If

                                If Item.Others < 0 Then
                                    SettlementPerDayContentArr(DDIndx, 10) = Item.Others
                                    TotalPayable += Item.Others
                                    TotalSPA += Item.Others
                                ElseIf Item.Others >= 0 Then
                                    SettlementPerDayContentArr(DDIndx, 17) = Item.Others
                                    TotalReceivable += Item.Others
                                    TotalSPA += Item.Others
                                End If

                                SettlementPerDayContentArr(DDIndx, 11) = If(TotalPayable = 0, Nothing, TotalPayable)
                                SettlementPerDayContentArr(DDIndx, 18) = If(TotalReceivable = 0, Nothing, TotalReceivable)
                                SettlementPerDayContentArr(DDIndx, 20) = CDec(SettlementPerDayContentArr(DDIndx, 11)) + CDec(SettlementPerDayContentArr(DDIndx, 18))
                                'SubTotal of Per Day
                                SettlementPerDayContentArr(DDLastIndx, 5) = CDec(SettlementPerDayContentArr(DDLastIndx, 5)) + CDec(SettlementPerDayContentArr(DDIndx, 5))
                                SettlementPerDayContentArr(DDLastIndx, 6) = CDec(SettlementPerDayContentArr(DDLastIndx, 6)) + CDec(SettlementPerDayContentArr(DDIndx, 6))
                                SettlementPerDayContentArr(DDLastIndx, 7) = CDec(SettlementPerDayContentArr(DDLastIndx, 7)) + CDec(SettlementPerDayContentArr(DDIndx, 7))
                                SettlementPerDayContentArr(DDLastIndx, 8) = CDec(SettlementPerDayContentArr(DDLastIndx, 8)) + CDec(SettlementPerDayContentArr(DDIndx, 8))
                                SettlementPerDayContentArr(DDLastIndx, 9) = CDec(SettlementPerDayContentArr(DDLastIndx, 9)) + CDec(SettlementPerDayContentArr(DDIndx, 9))
                                SettlementPerDayContentArr(DDLastIndx, 10) = CDec(SettlementPerDayContentArr(DDLastIndx, 10)) + CDec(SettlementPerDayContentArr(DDIndx, 10))
                                SettlementPerDayContentArr(DDLastIndx, 11) = CDec(SettlementPerDayContentArr(DDLastIndx, 11)) + CDec(SettlementPerDayContentArr(DDIndx, 11))
                                SettlementPerDayContentArr(DDLastIndx, 12) = CDec(SettlementPerDayContentArr(DDLastIndx, 12)) + CDec(SettlementPerDayContentArr(DDIndx, 12))
                                SettlementPerDayContentArr(DDLastIndx, 13) = CDec(SettlementPerDayContentArr(DDLastIndx, 13)) + CDec(SettlementPerDayContentArr(DDIndx, 13))
                                SettlementPerDayContentArr(DDLastIndx, 14) = CDec(SettlementPerDayContentArr(DDLastIndx, 14)) + CDec(SettlementPerDayContentArr(DDIndx, 14))
                                SettlementPerDayContentArr(DDLastIndx, 15) = CDec(SettlementPerDayContentArr(DDLastIndx, 15)) + CDec(SettlementPerDayContentArr(DDIndx, 15))
                                SettlementPerDayContentArr(DDLastIndx, 16) = CDec(SettlementPerDayContentArr(DDLastIndx, 16)) + CDec(SettlementPerDayContentArr(DDIndx, 16))
                                SettlementPerDayContentArr(DDLastIndx, 17) = CDec(SettlementPerDayContentArr(DDLastIndx, 17)) + CDec(SettlementPerDayContentArr(DDIndx, 17))
                                SettlementPerDayContentArr(DDLastIndx, 18) = CDec(SettlementPerDayContentArr(DDLastIndx, 18)) + CDec(SettlementPerDayContentArr(DDIndx, 18))
                                SettlementPerDayContentArr(DDLastIndx, 20) = CDec(SettlementPerDayContentArr(DDLastIndx, 20)) + CDec(SettlementPerDayContentArr(DDIndx, 20))

                                DDIndx += 1
                                If DDIndx = getCountGetItemPerClosingWBBySPAPerDate Then
                                    SettlementPerDayContentArr(DDIndx, 0) = "Invoices subject to SPA"
                                    SettlementPerDayContentArr(DDIndx, 3) = Item.CollPayAllocDate.ToShortDateString

                                    If TotalSPA < 0 Then
                                        SettlementPerDayContentArr(DDIndx, 10) = TotalSPA * -1
                                        SettlementPerDayContentArr(DDIndx, 11) = TotalSPA * -1
                                        SettlementPerDayContentArr(DDIndx, 18) = 0
                                    ElseIf TotalSPA >= 0 Then
                                        SettlementPerDayContentArr(DDIndx, 17) = TotalSPA * -1
                                        SettlementPerDayContentArr(DDIndx, 11) = 0
                                        SettlementPerDayContentArr(DDIndx, 18) = TotalSPA * -1
                                    End If

                                    SettlementPerDayContentArr(DDIndx, 20) = CDec(SettlementPerDayContentArr(DDIndx, 11)) + CDec(SettlementPerDayContentArr(DDIndx, 18))
                                    'SubTotal of Per Day
                                    SettlementPerDayContentArr(DDLastIndx, 5) = CDec(SettlementPerDayContentArr(DDLastIndx, 5)) + CDec(SettlementPerDayContentArr(DDIndx, 5))
                                    SettlementPerDayContentArr(DDLastIndx, 6) = CDec(SettlementPerDayContentArr(DDLastIndx, 6)) + CDec(SettlementPerDayContentArr(DDIndx, 6))
                                    SettlementPerDayContentArr(DDLastIndx, 7) = CDec(SettlementPerDayContentArr(DDLastIndx, 7)) + CDec(SettlementPerDayContentArr(DDIndx, 7))
                                    SettlementPerDayContentArr(DDLastIndx, 8) = CDec(SettlementPerDayContentArr(DDLastIndx, 8)) + CDec(SettlementPerDayContentArr(DDIndx, 8))
                                    SettlementPerDayContentArr(DDLastIndx, 9) = CDec(SettlementPerDayContentArr(DDLastIndx, 9)) + CDec(SettlementPerDayContentArr(DDIndx, 9))
                                    SettlementPerDayContentArr(DDLastIndx, 10) = CDec(SettlementPerDayContentArr(DDLastIndx, 10)) + CDec(SettlementPerDayContentArr(DDIndx, 10))
                                    SettlementPerDayContentArr(DDLastIndx, 11) = CDec(SettlementPerDayContentArr(DDLastIndx, 11)) + CDec(SettlementPerDayContentArr(DDIndx, 11))
                                    SettlementPerDayContentArr(DDLastIndx, 12) = CDec(SettlementPerDayContentArr(DDLastIndx, 12)) + CDec(SettlementPerDayContentArr(DDIndx, 12))
                                    SettlementPerDayContentArr(DDLastIndx, 13) = CDec(SettlementPerDayContentArr(DDLastIndx, 13)) + CDec(SettlementPerDayContentArr(DDIndx, 13))
                                    SettlementPerDayContentArr(DDLastIndx, 14) = CDec(SettlementPerDayContentArr(DDLastIndx, 14)) + CDec(SettlementPerDayContentArr(DDIndx, 14))
                                    SettlementPerDayContentArr(DDLastIndx, 15) = CDec(SettlementPerDayContentArr(DDLastIndx, 15)) + CDec(SettlementPerDayContentArr(DDIndx, 15))
                                    SettlementPerDayContentArr(DDLastIndx, 16) = CDec(SettlementPerDayContentArr(DDLastIndx, 16)) + CDec(SettlementPerDayContentArr(DDIndx, 16))
                                    SettlementPerDayContentArr(DDLastIndx, 17) = CDec(SettlementPerDayContentArr(DDLastIndx, 17)) + CDec(SettlementPerDayContentArr(DDIndx, 17))
                                    SettlementPerDayContentArr(DDLastIndx, 18) = CDec(SettlementPerDayContentArr(DDLastIndx, 18)) + CDec(SettlementPerDayContentArr(DDIndx, 18))
                                    SettlementPerDayContentArr(DDLastIndx, 20) = CDec(SettlementPerDayContentArr(DDLastIndx, 20)) + CDec(SettlementPerDayContentArr(DDIndx, 20))
                                End If
                            Next

                            SettlementGrandTotalArr(0, 5) = CDec(SettlementGrandTotalArr(0, 5)) + CDec(SettlementPerDayContentArr(DDLastIndx, 5))
                            SettlementGrandTotalArr(0, 6) = CDec(SettlementGrandTotalArr(0, 6)) + CDec(SettlementPerDayContentArr(DDLastIndx, 6))
                            SettlementGrandTotalArr(0, 7) = CDec(SettlementGrandTotalArr(0, 7)) + CDec(SettlementPerDayContentArr(DDLastIndx, 7))
                            SettlementGrandTotalArr(0, 8) = CDec(SettlementGrandTotalArr(0, 8)) + CDec(SettlementPerDayContentArr(DDLastIndx, 8))
                            SettlementGrandTotalArr(0, 9) = CDec(SettlementGrandTotalArr(0, 9)) + CDec(SettlementPerDayContentArr(DDLastIndx, 9))
                            SettlementGrandTotalArr(0, 10) = CDec(SettlementGrandTotalArr(0, 10)) + CDec(SettlementPerDayContentArr(DDLastIndx, 10))
                            SettlementGrandTotalArr(0, 11) = CDec(SettlementGrandTotalArr(0, 11)) + CDec(SettlementPerDayContentArr(DDLastIndx, 11))
                            SettlementGrandTotalArr(0, 12) = CDec(SettlementGrandTotalArr(0, 12)) + CDec(SettlementPerDayContentArr(DDLastIndx, 12))
                            SettlementGrandTotalArr(0, 13) = CDec(SettlementGrandTotalArr(0, 13)) + CDec(SettlementPerDayContentArr(DDLastIndx, 13))
                            SettlementGrandTotalArr(0, 14) = CDec(SettlementGrandTotalArr(0, 14)) + CDec(SettlementPerDayContentArr(DDLastIndx, 14))
                            SettlementGrandTotalArr(0, 15) = CDec(SettlementGrandTotalArr(0, 15)) + CDec(SettlementPerDayContentArr(DDLastIndx, 15))
                            SettlementGrandTotalArr(0, 16) = CDec(SettlementGrandTotalArr(0, 16)) + CDec(SettlementPerDayContentArr(DDLastIndx, 16))
                            SettlementGrandTotalArr(0, 17) = CDec(SettlementGrandTotalArr(0, 17)) + CDec(SettlementPerDayContentArr(DDLastIndx, 17))
                            SettlementGrandTotalArr(0, 18) = CDec(SettlementGrandTotalArr(0, 18)) + CDec(SettlementPerDayContentArr(DDLastIndx, 18))
                            SettlementGrandTotalArr(0, 20) = CDec(SettlementGrandTotalArr(0, 20)) + CDec(SettlementPerDayContentArr(DDLastIndx, 20))

                            rowIndex += 1
                            xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                            rowIndex += DDLastIndx
                            xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 21), Excel.Range)
                            xlContentSettlement = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                            xlContentSettlement.Value = SettlementPerDayContentArr
                        End If


                        'Remittances
                        SettlementPerDayContentArr = New Object(,) {}
                        Dim GetItemPerRemittancePerDate As List(Of STLNoticeNew) = (From x In RemittancesList Where x.CollPayAllocDate = ItemDate Select x).ToList()
                        Dim GetItemTransferToPayment As List(Of STLNoticeNew) = (From x In TransferToPayment Where x.CollPayAllocDate = ItemDate Select x).ToList()
                        Dim getItemPRRefundPerDate As List(Of STLNoticeNew) = (From x In PRRefund Where x.CollPayAllocDate = ItemDate Select x).ToList()

                        If GetItemPerRemittancePerDate.Count > 0 Then
                            Dim RemIndx As Integer = 0
                            Dim RemLastIndx As Integer = 0
                            Dim OverAllTotal As Decimal = 0

                            Dim GetItemPerRemittancePerDateGroupBy = (From x In GetItemPerRemittancePerDate
                                                                      Group x By keys = New With {Key x.BillingPeriod, Key x.ParticularsBillType, Key x.ParticularsChargeType,
                                                                      Key x.OrigDueDate, Key x.WESMBillInv, Key x.CollPayAllocDate} Into Group
                                                                      Select New With {.BillingPeriod = keys.BillingPeriod, .ParticularsChargeType = keys.ParticularsChargeType,
                                                                            .ParticularsBillType = keys.ParticularsBillType, .OrigDueDate = keys.OrigDueDate,
                                                                            .WESMBillInv = keys.WESMBillInv, .CollPayAllocDate = keys.CollPayAllocDate,
                                                                            .Energy = Group.Sum(Function(i) i.Energy), .VAT = Group.Sum(Function(i) i.VAT),
                                                                            .DefaultOnEnergy = Group.Sum(Function(i) i.DefaultOnEnergy), .MFAndVAT = Group.Sum(Function(i) i.MFAndVAT),
                                                                            .DefaultOnMFwithVAT = Group.Sum(Function(i) i.DefaultOnMFwithVAT), .Others = Group.Sum(Function(i) i.Others)}).ToList

                            If GetItemTransferToPayment.Count = 0 And getItemPRRefundPerDate.Count = 0 Then
                                ReDim SettlementPerDayContentArr(GetItemPerRemittancePerDateGroupBy.Count, 20)
                            ElseIf GetItemTransferToPayment.Count <> 0 And getItemPRRefundPerDate.Count = 0 Then
                                ReDim SettlementPerDayContentArr(GetItemPerRemittancePerDateGroupBy.Count + GetItemTransferToPayment.Count, 20)
                            ElseIf GetItemTransferToPayment.Count = 0 And getItemPRRefundPerDate.Count <> 0 Then
                                ReDim SettlementPerDayContentArr(GetItemPerRemittancePerDateGroupBy.Count + getItemPRRefundPerDate.Count, 20)
                            Else
                                ReDim SettlementPerDayContentArr(GetItemPerRemittancePerDateGroupBy.Count + GetItemTransferToPayment.Count + getItemPRRefundPerDate.Count, 20)
                            End If

                            RemLastIndx = UBound(SettlementPerDayContentArr, 1)

                            For Each Item In GetItemPerRemittancePerDateGroupBy 'GetItemPerRemittancePerDate
                                SettlementPerDayContentArr(RemIndx, 0) = Item.BillingPeriod
                                SettlementPerDayContentArr(RemIndx, 1) = Item.ParticularsChargeType
                                SettlementPerDayContentArr(RemIndx, 2) = Item.ParticularsBillType
                                SettlementPerDayContentArr(RemIndx, 3) = Item.CollPayAllocDate.ToShortDateString.ToString()
                                SettlementPerDayContentArr(RemIndx, 4) = Item.WESMBillInv
                                Dim TotalPayable As Decimal = 0
                                Dim TotalReceivable As Decimal = 0

                                If Item.Energy < 0 Then
                                    If Item.WESMBillInv.IndexOf("DMCM-") >= 0 Then
                                        SettlementPerDayContentArr(RemIndx, 12) = Item.Energy
                                        TotalReceivable += Item.Energy
                                    Else
                                        SettlementPerDayContentArr(RemIndx, 5) = Item.Energy
                                        TotalPayable += Item.Energy
                                    End If
                                ElseIf Item.Energy > 0 Then
                                    If Item.WESMBillInv.IndexOf("DMCM-") >= 0 Then
                                        SettlementPerDayContentArr(RemIndx, 5) = Item.Energy
                                        TotalPayable += Item.Energy
                                    Else
                                        SettlementPerDayContentArr(RemIndx, 12) = Item.Energy
                                        TotalReceivable += Item.Energy
                                    End If
                                End If

                                If Item.VAT < 0 Then
                                    SettlementPerDayContentArr(RemIndx, 6) = Item.VAT
                                    TotalPayable += Item.VAT
                                ElseIf Item.VAT > 0 Then
                                    SettlementPerDayContentArr(RemIndx, 13) = Item.VAT
                                    TotalReceivable += Item.VAT
                                End If

                                If Item.DefaultOnEnergy < 0 Then
                                    SettlementPerDayContentArr(RemIndx, 7) = Item.DefaultOnEnergy
                                    TotalPayable += Item.DefaultOnEnergy
                                ElseIf Item.DefaultOnEnergy > 0 Then
                                    SettlementPerDayContentArr(RemIndx, 14) = Item.DefaultOnEnergy
                                    TotalReceivable += Item.DefaultOnEnergy
                                End If

                                If Item.MFAndVAT < 0 Then
                                    If Item.ParticularsChargeType = "MF-Wtax" Or Item.ParticularsChargeType = "MF-Wvat" Then
                                        SettlementPerDayContentArr(RemIndx, 15) = CDec(SettlementPerDayContentArr(RemIndx, 15)) + Item.MFAndVAT
                                        TotalReceivable += Item.MFAndVAT
                                    ElseIf Item.WESMBillInv.IndexOf("DMCM-") >= 0 Then
                                        SettlementPerDayContentArr(RemIndx, 15) = Item.MFAndVAT
                                        TotalReceivable += Item.MFAndVAT
                                    Else
                                        SettlementPerDayContentArr(RemIndx, 8) = Item.MFAndVAT
                                        TotalPayable += Item.MFAndVAT
                                    End If

                                ElseIf Item.MFAndVAT > 0 Then
                                    If Item.ParticularsChargeType = "MF-Wtax" Or Item.ParticularsChargeType = "MF-Wvat" Then
                                        SettlementPerDayContentArr(RemIndx, 8) = CDec(SettlementPerDayContentArr(RemIndx, 8)) + Item.MFAndVAT
                                        TotalPayable += Item.MFAndVAT
                                    ElseIf Item.WESMBillInv.IndexOf("DMCM-") >= 0 Then
                                        SettlementPerDayContentArr(RemIndx, 8) = Item.MFAndVAT
                                        TotalPayable += Item.MFAndVAT
                                    Else
                                        SettlementPerDayContentArr(RemIndx, 15) = Item.MFAndVAT
                                        TotalReceivable += Item.MFAndVAT
                                    End If
                                End If

                                If Item.DefaultOnMFwithVAT < 0 Then
                                    SettlementPerDayContentArr(RemIndx, 9) = Item.DefaultOnMFwithVAT
                                    TotalPayable += Item.DefaultOnMFwithVAT
                                ElseIf Item.DefaultOnMFwithVAT > 0 Then
                                    SettlementPerDayContentArr(RemIndx, 16) = Item.DefaultOnMFwithVAT
                                    TotalReceivable += Item.DefaultOnMFwithVAT
                                End If

                                If Item.Others < 0 Then
                                    If Item.BillingPeriod = "Payment allocation - TransCo" Then
                                        SettlementPerDayContentArr(RemIndx, 17) = Item.Others
                                        TotalReceivable += Item.Others
                                    Else
                                        SettlementPerDayContentArr(RemIndx, 10) = Item.Others
                                        TotalPayable += Item.Others
                                    End If
                                ElseIf Item.Others > 0 Then
                                    SettlementPerDayContentArr(RemIndx, 17) = Item.Others
                                    TotalReceivable += Item.Others
                                End If

                                OverAllTotal += TotalPayable + TotalReceivable

                                SettlementPerDayContentArr(RemIndx, 11) = If(TotalPayable = 0, Nothing, TotalPayable)
                                SettlementPerDayContentArr(RemIndx, 18) = If(TotalReceivable = 0, Nothing, TotalReceivable)
                                SettlementPerDayContentArr(RemIndx, 20) = CDec(SettlementPerDayContentArr(RemIndx, 11)) + CDec(SettlementPerDayContentArr(RemIndx, 18))

                                'SubTotal of Per Day
                                SettlementPerDayContentArr(RemLastIndx, 5) = CDec(SettlementPerDayContentArr(RemLastIndx, 5)) + CDec(SettlementPerDayContentArr(RemIndx, 5))
                                SettlementPerDayContentArr(RemLastIndx, 6) = CDec(SettlementPerDayContentArr(RemLastIndx, 6)) + CDec(SettlementPerDayContentArr(RemIndx, 6))
                                SettlementPerDayContentArr(RemLastIndx, 7) = CDec(SettlementPerDayContentArr(RemLastIndx, 7)) + CDec(SettlementPerDayContentArr(RemIndx, 7))
                                SettlementPerDayContentArr(RemLastIndx, 8) = CDec(SettlementPerDayContentArr(RemLastIndx, 8)) + CDec(SettlementPerDayContentArr(RemIndx, 8))
                                SettlementPerDayContentArr(RemLastIndx, 9) = CDec(SettlementPerDayContentArr(RemLastIndx, 9)) + CDec(SettlementPerDayContentArr(RemIndx, 9))
                                SettlementPerDayContentArr(RemLastIndx, 10) = CDec(SettlementPerDayContentArr(RemLastIndx, 10)) + CDec(SettlementPerDayContentArr(RemIndx, 10))
                                SettlementPerDayContentArr(RemLastIndx, 11) = CDec(SettlementPerDayContentArr(RemLastIndx, 11)) + CDec(SettlementPerDayContentArr(RemIndx, 11))
                                SettlementPerDayContentArr(RemLastIndx, 12) = CDec(SettlementPerDayContentArr(RemLastIndx, 12)) + CDec(SettlementPerDayContentArr(RemIndx, 12))
                                SettlementPerDayContentArr(RemLastIndx, 13) = CDec(SettlementPerDayContentArr(RemLastIndx, 13)) + CDec(SettlementPerDayContentArr(RemIndx, 13))
                                SettlementPerDayContentArr(RemLastIndx, 14) = CDec(SettlementPerDayContentArr(RemLastIndx, 14)) + CDec(SettlementPerDayContentArr(RemIndx, 14))
                                SettlementPerDayContentArr(RemLastIndx, 15) = CDec(SettlementPerDayContentArr(RemLastIndx, 15)) + CDec(SettlementPerDayContentArr(RemIndx, 15))
                                SettlementPerDayContentArr(RemLastIndx, 16) = CDec(SettlementPerDayContentArr(RemLastIndx, 16)) + CDec(SettlementPerDayContentArr(RemIndx, 16))
                                SettlementPerDayContentArr(RemLastIndx, 17) = CDec(SettlementPerDayContentArr(RemLastIndx, 17)) + CDec(SettlementPerDayContentArr(RemIndx, 17))
                                SettlementPerDayContentArr(RemLastIndx, 18) = CDec(SettlementPerDayContentArr(RemLastIndx, 18)) + CDec(SettlementPerDayContentArr(RemIndx, 18))
                                SettlementPerDayContentArr(RemLastIndx, 20) = CDec(SettlementPerDayContentArr(RemLastIndx, 20)) + CDec(SettlementPerDayContentArr(RemIndx, 20))
                                RemIndx += 1
                            Next

                            For Each item In GetItemTransferToPayment
                                SettlementPerDayContentArr(RemIndx, 0) = item.BillingPeriod
                                SettlementPerDayContentArr(RemIndx, 1) = item.ParticularsBillType
                                SettlementPerDayContentArr(RemIndx, 2) = item.ParticularsChargeType
                                SettlementPerDayContentArr(RemIndx, 3) = item.CollPayAllocDate.ToShortDateString.ToString()
                                SettlementPerDayContentArr(RemIndx, 4) = item.WESMBillInv

                                If item.Others < 0 Then
                                    If item.BillingPeriod.Contains("Financial Penalty") Then
                                        SettlementPerDayContentArr(RemIndx, 17) = item.Others
                                        SettlementPerDayContentArr(RemIndx, 18) = item.Others
                                        SettlementPerDayContentArr(RemIndx, 20) = item.Others
                                    Else
                                        SettlementPerDayContentArr(RemIndx, 10) = item.Others
                                        SettlementPerDayContentArr(RemIndx, 11) = item.Others
                                        SettlementPerDayContentArr(RemIndx, 20) = item.Others
                                    End If
                                ElseIf item.Others > 0 Then
                                    SettlementPerDayContentArr(RemIndx, 17) = item.Others
                                    SettlementPerDayContentArr(RemIndx, 18) = item.Others
                                    SettlementPerDayContentArr(RemIndx, 20) = item.Others
                                End If
                                OverAllTotal += item.Others

                                'SubTotal of Per Day
                                SettlementPerDayContentArr(RemLastIndx, 5) = CDec(SettlementPerDayContentArr(RemLastIndx, 5)) + CDec(SettlementPerDayContentArr(RemIndx, 5))
                                SettlementPerDayContentArr(RemLastIndx, 6) = CDec(SettlementPerDayContentArr(RemLastIndx, 6)) + CDec(SettlementPerDayContentArr(RemIndx, 6))
                                SettlementPerDayContentArr(RemLastIndx, 7) = CDec(SettlementPerDayContentArr(RemLastIndx, 7)) + CDec(SettlementPerDayContentArr(RemIndx, 7))
                                SettlementPerDayContentArr(RemLastIndx, 8) = CDec(SettlementPerDayContentArr(RemLastIndx, 8)) + CDec(SettlementPerDayContentArr(RemIndx, 8))
                                SettlementPerDayContentArr(RemLastIndx, 9) = CDec(SettlementPerDayContentArr(RemLastIndx, 9)) + CDec(SettlementPerDayContentArr(RemIndx, 9))
                                SettlementPerDayContentArr(RemLastIndx, 10) = CDec(SettlementPerDayContentArr(RemLastIndx, 10)) + CDec(SettlementPerDayContentArr(RemIndx, 10))
                                SettlementPerDayContentArr(RemLastIndx, 11) = CDec(SettlementPerDayContentArr(RemLastIndx, 11)) + CDec(SettlementPerDayContentArr(RemIndx, 11))
                                SettlementPerDayContentArr(RemLastIndx, 12) = CDec(SettlementPerDayContentArr(RemLastIndx, 12)) + CDec(SettlementPerDayContentArr(RemIndx, 12))
                                SettlementPerDayContentArr(RemLastIndx, 13) = CDec(SettlementPerDayContentArr(RemLastIndx, 13)) + CDec(SettlementPerDayContentArr(RemIndx, 13))
                                SettlementPerDayContentArr(RemLastIndx, 14) = CDec(SettlementPerDayContentArr(RemLastIndx, 14)) + CDec(SettlementPerDayContentArr(RemIndx, 14))
                                SettlementPerDayContentArr(RemLastIndx, 15) = CDec(SettlementPerDayContentArr(RemLastIndx, 15)) + CDec(SettlementPerDayContentArr(RemIndx, 15))
                                SettlementPerDayContentArr(RemLastIndx, 16) = CDec(SettlementPerDayContentArr(RemLastIndx, 16)) + CDec(SettlementPerDayContentArr(RemIndx, 16))
                                SettlementPerDayContentArr(RemLastIndx, 17) = CDec(SettlementPerDayContentArr(RemLastIndx, 17)) + CDec(SettlementPerDayContentArr(RemIndx, 17))
                                SettlementPerDayContentArr(RemLastIndx, 18) = CDec(SettlementPerDayContentArr(RemLastIndx, 18)) + CDec(SettlementPerDayContentArr(RemIndx, 18))
                                SettlementPerDayContentArr(RemLastIndx, 20) = CDec(SettlementPerDayContentArr(RemLastIndx, 20)) + CDec(SettlementPerDayContentArr(RemIndx, 20))
                                RemIndx += 1
                            Next


                            For Each item In getItemPRRefundPerDate
                                SettlementPerDayContentArr(RemIndx, 0) = item.BillingPeriod
                                SettlementPerDayContentArr(RemIndx, 1) = item.ParticularsBillType
                                SettlementPerDayContentArr(RemIndx, 2) = item.ParticularsChargeType
                                SettlementPerDayContentArr(RemIndx, 3) = item.CollPayAllocDate.ToShortDateString.ToString()
                                SettlementPerDayContentArr(RemIndx, 4) = item.WESMBillInv

                                SettlementPerDayContentArr(RemIndx, 17) = item.Others
                                SettlementPerDayContentArr(RemIndx, 18) = item.Others
                                SettlementPerDayContentArr(RemIndx, 20) = item.Others

                                OverAllTotal += item.Others

                                'SubTotal of Per Day
                                SettlementPerDayContentArr(RemLastIndx, 5) = CDec(SettlementPerDayContentArr(RemLastIndx, 5)) + CDec(SettlementPerDayContentArr(RemIndx, 5))
                                SettlementPerDayContentArr(RemLastIndx, 6) = CDec(SettlementPerDayContentArr(RemLastIndx, 6)) + CDec(SettlementPerDayContentArr(RemIndx, 6))
                                SettlementPerDayContentArr(RemLastIndx, 7) = CDec(SettlementPerDayContentArr(RemLastIndx, 7)) + CDec(SettlementPerDayContentArr(RemIndx, 7))
                                SettlementPerDayContentArr(RemLastIndx, 8) = CDec(SettlementPerDayContentArr(RemLastIndx, 8)) + CDec(SettlementPerDayContentArr(RemIndx, 8))
                                SettlementPerDayContentArr(RemLastIndx, 9) = CDec(SettlementPerDayContentArr(RemLastIndx, 9)) + CDec(SettlementPerDayContentArr(RemIndx, 9))
                                SettlementPerDayContentArr(RemLastIndx, 10) = CDec(SettlementPerDayContentArr(RemLastIndx, 10)) + CDec(SettlementPerDayContentArr(RemIndx, 10))
                                SettlementPerDayContentArr(RemLastIndx, 11) = CDec(SettlementPerDayContentArr(RemLastIndx, 11)) + CDec(SettlementPerDayContentArr(RemIndx, 11))
                                SettlementPerDayContentArr(RemLastIndx, 12) = CDec(SettlementPerDayContentArr(RemLastIndx, 12)) + CDec(SettlementPerDayContentArr(RemIndx, 12))
                                SettlementPerDayContentArr(RemLastIndx, 13) = CDec(SettlementPerDayContentArr(RemLastIndx, 13)) + CDec(SettlementPerDayContentArr(RemIndx, 13))
                                SettlementPerDayContentArr(RemLastIndx, 14) = CDec(SettlementPerDayContentArr(RemLastIndx, 14)) + CDec(SettlementPerDayContentArr(RemIndx, 14))
                                SettlementPerDayContentArr(RemLastIndx, 15) = CDec(SettlementPerDayContentArr(RemLastIndx, 15)) + CDec(SettlementPerDayContentArr(RemIndx, 15))
                                SettlementPerDayContentArr(RemLastIndx, 16) = CDec(SettlementPerDayContentArr(RemLastIndx, 16)) + CDec(SettlementPerDayContentArr(RemIndx, 16))
                                SettlementPerDayContentArr(RemLastIndx, 17) = CDec(SettlementPerDayContentArr(RemLastIndx, 17)) + CDec(SettlementPerDayContentArr(RemIndx, 17))
                                SettlementPerDayContentArr(RemLastIndx, 18) = CDec(SettlementPerDayContentArr(RemLastIndx, 18)) + CDec(SettlementPerDayContentArr(RemIndx, 18))
                                SettlementPerDayContentArr(RemLastIndx, 20) = CDec(SettlementPerDayContentArr(RemLastIndx, 20)) + CDec(SettlementPerDayContentArr(RemIndx, 20))
                                RemIndx += 1
                            Next

                            If OverAllTotal >= 1000 Then
                                SettlementPerDayContentArr(RemLastIndx, 0) = "TOTAL REMITTANCE ON " & MonthName(ItemDate.Month).ToUpper.ToString() & " " & ItemDate.Day & ", " & ItemDate.Year
                            ElseIf OverAllTotal = 0 And GetItemPerRemittancePerDate.Count > 0 Then
                                SettlementPerDayContentArr(RemLastIndx, 0) = "TOTAL OFFSETTING ON " & MonthName(ItemDate.Month).ToUpper.ToString() & " " & ItemDate.Day & ", " & ItemDate.Year
                            Else
                                SettlementPerDayContentArr(RemLastIndx, 0) = "TOTAL AMOUNT WITHHELD ON " & MonthName(ItemDate.Month).ToUpper.ToString() & " " & ItemDate.Day & ", " & ItemDate.Year
                            End If

                            SettlementGrandTotalArr(0, 5) = CDec(SettlementGrandTotalArr(0, 5)) + CDec(SettlementPerDayContentArr(RemLastIndx, 5))
                            SettlementGrandTotalArr(0, 6) = CDec(SettlementGrandTotalArr(0, 6)) + CDec(SettlementPerDayContentArr(RemLastIndx, 6))
                            SettlementGrandTotalArr(0, 7) = CDec(SettlementGrandTotalArr(0, 7)) + CDec(SettlementPerDayContentArr(RemLastIndx, 7))
                            SettlementGrandTotalArr(0, 8) = CDec(SettlementGrandTotalArr(0, 8)) + CDec(SettlementPerDayContentArr(RemLastIndx, 8))
                            SettlementGrandTotalArr(0, 9) = CDec(SettlementGrandTotalArr(0, 9)) + CDec(SettlementPerDayContentArr(RemLastIndx, 9))
                            SettlementGrandTotalArr(0, 10) = CDec(SettlementGrandTotalArr(0, 10)) + CDec(SettlementPerDayContentArr(RemLastIndx, 10))
                            SettlementGrandTotalArr(0, 11) = CDec(SettlementGrandTotalArr(0, 11)) + CDec(SettlementPerDayContentArr(RemLastIndx, 11))
                            SettlementGrandTotalArr(0, 12) = CDec(SettlementGrandTotalArr(0, 12)) + CDec(SettlementPerDayContentArr(RemLastIndx, 12))
                            SettlementGrandTotalArr(0, 13) = CDec(SettlementGrandTotalArr(0, 13)) + CDec(SettlementPerDayContentArr(RemLastIndx, 13))
                            SettlementGrandTotalArr(0, 14) = CDec(SettlementGrandTotalArr(0, 14)) + CDec(SettlementPerDayContentArr(RemLastIndx, 14))
                            SettlementGrandTotalArr(0, 15) = CDec(SettlementGrandTotalArr(0, 15)) + CDec(SettlementPerDayContentArr(RemLastIndx, 15))
                            SettlementGrandTotalArr(0, 16) = CDec(SettlementGrandTotalArr(0, 16)) + CDec(SettlementPerDayContentArr(RemLastIndx, 16))
                            SettlementGrandTotalArr(0, 17) = CDec(SettlementGrandTotalArr(0, 17)) + CDec(SettlementPerDayContentArr(RemLastIndx, 17))
                            SettlementGrandTotalArr(0, 18) = CDec(SettlementGrandTotalArr(0, 18)) + CDec(SettlementPerDayContentArr(RemLastIndx, 18))
                            SettlementGrandTotalArr(0, 20) = CDec(SettlementGrandTotalArr(0, 20)) + CDec(SettlementPerDayContentArr(RemLastIndx, 20))

                            rowIndex += 1
                            xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                            rowIndex += RemLastIndx
                            xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 21), Excel.Range)
                            xlContentSettlement = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                            xlContentSettlement.Value = SettlementPerDayContentArr
                        ElseIf GetItemPerRemittancePerDate.Count = 0 And GetItemTransferToPayment.Count > 0 And getItemPRRefundPerDate.Count = 0 Then
                            Dim RemIndx As Integer = 0
                            Dim RemLastIndx As Integer = 0
                            Dim OverAllTotal As Decimal = 0

                            ReDim SettlementPerDayContentArr(GetItemTransferToPayment.Count, 20)

                            RemLastIndx = UBound(SettlementPerDayContentArr, 1)
                            For Each item In GetItemTransferToPayment
                                SettlementPerDayContentArr(RemIndx, 0) = item.BillingPeriod
                                SettlementPerDayContentArr(RemIndx, 1) = item.ParticularsBillType
                                SettlementPerDayContentArr(RemIndx, 2) = item.ParticularsChargeType
                                SettlementPerDayContentArr(RemIndx, 3) = item.CollPayAllocDate.ToShortDateString.ToString()
                                SettlementPerDayContentArr(RemIndx, 4) = item.WESMBillInv

                                If item.Others < 0 Then
                                    If item.BillingPeriod.Contains("Financial Penalty") Then
                                        SettlementPerDayContentArr(RemIndx, 17) = item.Others
                                        SettlementPerDayContentArr(RemIndx, 18) = item.Others
                                        SettlementPerDayContentArr(RemIndx, 20) = item.Others
                                    Else
                                        SettlementPerDayContentArr(RemIndx, 10) = item.Others
                                        SettlementPerDayContentArr(RemIndx, 11) = item.Others
                                        SettlementPerDayContentArr(RemIndx, 20) = item.Others
                                    End If
                                ElseIf item.Others > 0 Then
                                    SettlementPerDayContentArr(RemIndx, 17) = item.Others
                                    SettlementPerDayContentArr(RemIndx, 18) = item.Others
                                    SettlementPerDayContentArr(RemIndx, 20) = item.Others
                                End If
                                OverAllTotal += item.Others

                                'SubTotal of Per Day
                                SettlementPerDayContentArr(RemLastIndx, 5) = CDec(SettlementPerDayContentArr(RemLastIndx, 5)) + CDec(SettlementPerDayContentArr(RemIndx, 5))
                                SettlementPerDayContentArr(RemLastIndx, 6) = CDec(SettlementPerDayContentArr(RemLastIndx, 6)) + CDec(SettlementPerDayContentArr(RemIndx, 6))
                                SettlementPerDayContentArr(RemLastIndx, 7) = CDec(SettlementPerDayContentArr(RemLastIndx, 7)) + CDec(SettlementPerDayContentArr(RemIndx, 7))
                                SettlementPerDayContentArr(RemLastIndx, 8) = CDec(SettlementPerDayContentArr(RemLastIndx, 8)) + CDec(SettlementPerDayContentArr(RemIndx, 8))
                                SettlementPerDayContentArr(RemLastIndx, 9) = CDec(SettlementPerDayContentArr(RemLastIndx, 9)) + CDec(SettlementPerDayContentArr(RemIndx, 9))
                                SettlementPerDayContentArr(RemLastIndx, 10) = CDec(SettlementPerDayContentArr(RemLastIndx, 10)) + CDec(SettlementPerDayContentArr(RemIndx, 10))
                                SettlementPerDayContentArr(RemLastIndx, 11) = CDec(SettlementPerDayContentArr(RemLastIndx, 11)) + CDec(SettlementPerDayContentArr(RemIndx, 11))
                                SettlementPerDayContentArr(RemLastIndx, 12) = CDec(SettlementPerDayContentArr(RemLastIndx, 12)) + CDec(SettlementPerDayContentArr(RemIndx, 12))
                                SettlementPerDayContentArr(RemLastIndx, 13) = CDec(SettlementPerDayContentArr(RemLastIndx, 13)) + CDec(SettlementPerDayContentArr(RemIndx, 13))
                                SettlementPerDayContentArr(RemLastIndx, 14) = CDec(SettlementPerDayContentArr(RemLastIndx, 14)) + CDec(SettlementPerDayContentArr(RemIndx, 14))
                                SettlementPerDayContentArr(RemLastIndx, 15) = CDec(SettlementPerDayContentArr(RemLastIndx, 15)) + CDec(SettlementPerDayContentArr(RemIndx, 15))
                                SettlementPerDayContentArr(RemLastIndx, 16) = CDec(SettlementPerDayContentArr(RemLastIndx, 16)) + CDec(SettlementPerDayContentArr(RemIndx, 16))
                                SettlementPerDayContentArr(RemLastIndx, 17) = CDec(SettlementPerDayContentArr(RemLastIndx, 17)) + CDec(SettlementPerDayContentArr(RemIndx, 17))
                                SettlementPerDayContentArr(RemLastIndx, 18) = CDec(SettlementPerDayContentArr(RemLastIndx, 18)) + CDec(SettlementPerDayContentArr(RemIndx, 18))
                                SettlementPerDayContentArr(RemLastIndx, 20) = CDec(SettlementPerDayContentArr(RemLastIndx, 20)) + CDec(SettlementPerDayContentArr(RemIndx, 20))
                                RemIndx += 1

                            Next

                            If OverAllTotal >= 1000 Or (OverAllTotal = 0 And GetItemPerRemittancePerDate.Count > 0) Then
                                SettlementPerDayContentArr(RemLastIndx, 0) = "TOTAL REMITTANCE ON " & MonthName(ItemDate.Month).ToUpper.ToString() & " " & ItemDate.Day & ", " & ItemDate.Year
                            Else
                                SettlementPerDayContentArr(RemLastIndx, 0) = "TOTAL AMOUNT WITHHELD ON " & MonthName(ItemDate.Month).ToUpper.ToString() & " " & ItemDate.Day & ", " & ItemDate.Year
                            End If

                            SettlementGrandTotalArr(0, 5) = CDec(SettlementGrandTotalArr(0, 5)) + CDec(SettlementPerDayContentArr(RemLastIndx, 5))
                            SettlementGrandTotalArr(0, 6) = CDec(SettlementGrandTotalArr(0, 6)) + CDec(SettlementPerDayContentArr(RemLastIndx, 6))
                            SettlementGrandTotalArr(0, 7) = CDec(SettlementGrandTotalArr(0, 7)) + CDec(SettlementPerDayContentArr(RemLastIndx, 7))
                            SettlementGrandTotalArr(0, 8) = CDec(SettlementGrandTotalArr(0, 8)) + CDec(SettlementPerDayContentArr(RemLastIndx, 8))
                            SettlementGrandTotalArr(0, 9) = CDec(SettlementGrandTotalArr(0, 9)) + CDec(SettlementPerDayContentArr(RemLastIndx, 9))
                            SettlementGrandTotalArr(0, 10) = CDec(SettlementGrandTotalArr(0, 10)) + CDec(SettlementPerDayContentArr(RemLastIndx, 10))
                            SettlementGrandTotalArr(0, 11) = CDec(SettlementGrandTotalArr(0, 11)) + CDec(SettlementPerDayContentArr(RemLastIndx, 11))
                            SettlementGrandTotalArr(0, 12) = CDec(SettlementGrandTotalArr(0, 12)) + CDec(SettlementPerDayContentArr(RemLastIndx, 12))
                            SettlementGrandTotalArr(0, 13) = CDec(SettlementGrandTotalArr(0, 13)) + CDec(SettlementPerDayContentArr(RemLastIndx, 13))
                            SettlementGrandTotalArr(0, 14) = CDec(SettlementGrandTotalArr(0, 14)) + CDec(SettlementPerDayContentArr(RemLastIndx, 14))
                            SettlementGrandTotalArr(0, 15) = CDec(SettlementGrandTotalArr(0, 15)) + CDec(SettlementPerDayContentArr(RemLastIndx, 15))
                            SettlementGrandTotalArr(0, 16) = CDec(SettlementGrandTotalArr(0, 16)) + CDec(SettlementPerDayContentArr(RemLastIndx, 16))
                            SettlementGrandTotalArr(0, 17) = CDec(SettlementGrandTotalArr(0, 17)) + CDec(SettlementPerDayContentArr(RemLastIndx, 17))
                            SettlementGrandTotalArr(0, 18) = CDec(SettlementGrandTotalArr(0, 18)) + CDec(SettlementPerDayContentArr(RemLastIndx, 18))
                            SettlementGrandTotalArr(0, 20) = CDec(SettlementGrandTotalArr(0, 20)) + CDec(SettlementPerDayContentArr(RemLastIndx, 20))

                            rowIndex += 1
                            xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                            rowIndex += RemLastIndx
                            xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 21), Excel.Range)
                            xlContentSettlement = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                            xlContentSettlement.Value = SettlementPerDayContentArr

                        ElseIf GetItemPerRemittancePerDate.Count = 0 And GetItemTransferToPayment.Count > 0 And getItemPRRefundPerDate.Count > 0 Then
                            Dim RemIndx As Integer = 0
                            Dim RemLastIndx As Integer = 0
                            Dim OverAllTotal As Decimal = 0
                            ReDim SettlementPerDayContentArr(getItemPRRefundPerDate.Count + GetItemTransferToPayment.Count, 20)

                            RemLastIndx = UBound(SettlementPerDayContentArr, 1)
                            For Each item In GetItemTransferToPayment
                                SettlementPerDayContentArr(RemIndx, 0) = item.BillingPeriod
                                SettlementPerDayContentArr(RemIndx, 1) = item.ParticularsBillType
                                SettlementPerDayContentArr(RemIndx, 2) = item.ParticularsChargeType
                                SettlementPerDayContentArr(RemIndx, 3) = item.CollPayAllocDate.ToShortDateString.ToString()
                                SettlementPerDayContentArr(RemIndx, 4) = item.WESMBillInv

                                If item.Others < 0 Then
                                    If item.BillingPeriod.Contains("Financial Penalty") Then
                                        SettlementPerDayContentArr(RemIndx, 17) = item.Others
                                        SettlementPerDayContentArr(RemIndx, 18) = item.Others
                                        SettlementPerDayContentArr(RemIndx, 20) = item.Others
                                    Else
                                        SettlementPerDayContentArr(RemIndx, 10) = item.Others
                                        SettlementPerDayContentArr(RemIndx, 11) = item.Others
                                        SettlementPerDayContentArr(RemIndx, 20) = item.Others
                                    End If
                                ElseIf item.Others > 0 Then
                                    SettlementPerDayContentArr(RemIndx, 17) = item.Others
                                    SettlementPerDayContentArr(RemIndx, 18) = item.Others
                                    SettlementPerDayContentArr(RemIndx, 20) = item.Others
                                End If
                                OverAllTotal += item.Others

                                'SubTotal of Per Day
                                SettlementPerDayContentArr(RemLastIndx, 5) = CDec(SettlementPerDayContentArr(RemLastIndx, 5)) + CDec(SettlementPerDayContentArr(RemIndx, 5))
                                SettlementPerDayContentArr(RemLastIndx, 6) = CDec(SettlementPerDayContentArr(RemLastIndx, 6)) + CDec(SettlementPerDayContentArr(RemIndx, 6))
                                SettlementPerDayContentArr(RemLastIndx, 7) = CDec(SettlementPerDayContentArr(RemLastIndx, 7)) + CDec(SettlementPerDayContentArr(RemIndx, 7))
                                SettlementPerDayContentArr(RemLastIndx, 8) = CDec(SettlementPerDayContentArr(RemLastIndx, 8)) + CDec(SettlementPerDayContentArr(RemIndx, 8))
                                SettlementPerDayContentArr(RemLastIndx, 9) = CDec(SettlementPerDayContentArr(RemLastIndx, 9)) + CDec(SettlementPerDayContentArr(RemIndx, 9))
                                SettlementPerDayContentArr(RemLastIndx, 10) = CDec(SettlementPerDayContentArr(RemLastIndx, 10)) + CDec(SettlementPerDayContentArr(RemIndx, 10))
                                SettlementPerDayContentArr(RemLastIndx, 11) = CDec(SettlementPerDayContentArr(RemLastIndx, 11)) + CDec(SettlementPerDayContentArr(RemIndx, 11))
                                SettlementPerDayContentArr(RemLastIndx, 12) = CDec(SettlementPerDayContentArr(RemLastIndx, 12)) + CDec(SettlementPerDayContentArr(RemIndx, 12))
                                SettlementPerDayContentArr(RemLastIndx, 13) = CDec(SettlementPerDayContentArr(RemLastIndx, 13)) + CDec(SettlementPerDayContentArr(RemIndx, 13))
                                SettlementPerDayContentArr(RemLastIndx, 14) = CDec(SettlementPerDayContentArr(RemLastIndx, 14)) + CDec(SettlementPerDayContentArr(RemIndx, 14))
                                SettlementPerDayContentArr(RemLastIndx, 15) = CDec(SettlementPerDayContentArr(RemLastIndx, 15)) + CDec(SettlementPerDayContentArr(RemIndx, 15))
                                SettlementPerDayContentArr(RemLastIndx, 16) = CDec(SettlementPerDayContentArr(RemLastIndx, 16)) + CDec(SettlementPerDayContentArr(RemIndx, 16))
                                SettlementPerDayContentArr(RemLastIndx, 17) = CDec(SettlementPerDayContentArr(RemLastIndx, 17)) + CDec(SettlementPerDayContentArr(RemIndx, 17))
                                SettlementPerDayContentArr(RemLastIndx, 18) = CDec(SettlementPerDayContentArr(RemLastIndx, 18)) + CDec(SettlementPerDayContentArr(RemIndx, 18))
                                SettlementPerDayContentArr(RemLastIndx, 20) = CDec(SettlementPerDayContentArr(RemLastIndx, 20)) + CDec(SettlementPerDayContentArr(RemIndx, 20))
                                RemIndx += 1

                            Next

                            For Each item In getItemPRRefundPerDate
                                SettlementPerDayContentArr(RemIndx, 0) = item.BillingPeriod
                                SettlementPerDayContentArr(RemIndx, 1) = item.ParticularsBillType
                                SettlementPerDayContentArr(RemIndx, 2) = item.ParticularsChargeType
                                SettlementPerDayContentArr(RemIndx, 3) = item.CollPayAllocDate.ToShortDateString.ToString()
                                SettlementPerDayContentArr(RemIndx, 4) = item.WESMBillInv

                                SettlementPerDayContentArr(RemIndx, 17) = item.Others
                                SettlementPerDayContentArr(RemIndx, 18) = item.Others
                                SettlementPerDayContentArr(RemIndx, 20) = item.Others

                                OverAllTotal += item.Others

                                'SubTotal of Per Day
                                SettlementPerDayContentArr(RemLastIndx, 5) = CDec(SettlementPerDayContentArr(RemLastIndx, 5)) + CDec(SettlementPerDayContentArr(RemIndx, 5))
                                SettlementPerDayContentArr(RemLastIndx, 6) = CDec(SettlementPerDayContentArr(RemLastIndx, 6)) + CDec(SettlementPerDayContentArr(RemIndx, 6))
                                SettlementPerDayContentArr(RemLastIndx, 7) = CDec(SettlementPerDayContentArr(RemLastIndx, 7)) + CDec(SettlementPerDayContentArr(RemIndx, 7))
                                SettlementPerDayContentArr(RemLastIndx, 8) = CDec(SettlementPerDayContentArr(RemLastIndx, 8)) + CDec(SettlementPerDayContentArr(RemIndx, 8))
                                SettlementPerDayContentArr(RemLastIndx, 9) = CDec(SettlementPerDayContentArr(RemLastIndx, 9)) + CDec(SettlementPerDayContentArr(RemIndx, 9))
                                SettlementPerDayContentArr(RemLastIndx, 10) = CDec(SettlementPerDayContentArr(RemLastIndx, 10)) + CDec(SettlementPerDayContentArr(RemIndx, 10))
                                SettlementPerDayContentArr(RemLastIndx, 11) = CDec(SettlementPerDayContentArr(RemLastIndx, 11)) + CDec(SettlementPerDayContentArr(RemIndx, 11))
                                SettlementPerDayContentArr(RemLastIndx, 12) = CDec(SettlementPerDayContentArr(RemLastIndx, 12)) + CDec(SettlementPerDayContentArr(RemIndx, 12))
                                SettlementPerDayContentArr(RemLastIndx, 13) = CDec(SettlementPerDayContentArr(RemLastIndx, 13)) + CDec(SettlementPerDayContentArr(RemIndx, 13))
                                SettlementPerDayContentArr(RemLastIndx, 14) = CDec(SettlementPerDayContentArr(RemLastIndx, 14)) + CDec(SettlementPerDayContentArr(RemIndx, 14))
                                SettlementPerDayContentArr(RemLastIndx, 15) = CDec(SettlementPerDayContentArr(RemLastIndx, 15)) + CDec(SettlementPerDayContentArr(RemIndx, 15))
                                SettlementPerDayContentArr(RemLastIndx, 16) = CDec(SettlementPerDayContentArr(RemLastIndx, 16)) + CDec(SettlementPerDayContentArr(RemIndx, 16))
                                SettlementPerDayContentArr(RemLastIndx, 17) = CDec(SettlementPerDayContentArr(RemLastIndx, 17)) + CDec(SettlementPerDayContentArr(RemIndx, 17))
                                SettlementPerDayContentArr(RemLastIndx, 18) = CDec(SettlementPerDayContentArr(RemLastIndx, 18)) + CDec(SettlementPerDayContentArr(RemIndx, 18))
                                SettlementPerDayContentArr(RemLastIndx, 20) = CDec(SettlementPerDayContentArr(RemLastIndx, 20)) + CDec(SettlementPerDayContentArr(RemIndx, 20))
                                RemIndx += 1
                            Next

                            If OverAllTotal >= 1000 Or (OverAllTotal = 0 And GetItemPerRemittancePerDate.Count > 0) Then
                                SettlementPerDayContentArr(RemLastIndx, 0) = "TOTAL REMITTANCE ON " & MonthName(ItemDate.Month).ToUpper.ToString() & " " & ItemDate.Day & ", " & ItemDate.Year
                            Else
                                SettlementPerDayContentArr(RemLastIndx, 0) = "TOTAL AMOUNT WITHHELD ON " & MonthName(ItemDate.Month).ToUpper.ToString() & " " & ItemDate.Day & ", " & ItemDate.Year
                            End If

                            SettlementGrandTotalArr(0, 5) = CDec(SettlementGrandTotalArr(0, 5)) + CDec(SettlementPerDayContentArr(RemLastIndx, 5))
                            SettlementGrandTotalArr(0, 6) = CDec(SettlementGrandTotalArr(0, 6)) + CDec(SettlementPerDayContentArr(RemLastIndx, 6))
                            SettlementGrandTotalArr(0, 7) = CDec(SettlementGrandTotalArr(0, 7)) + CDec(SettlementPerDayContentArr(RemLastIndx, 7))
                            SettlementGrandTotalArr(0, 8) = CDec(SettlementGrandTotalArr(0, 8)) + CDec(SettlementPerDayContentArr(RemLastIndx, 8))
                            SettlementGrandTotalArr(0, 9) = CDec(SettlementGrandTotalArr(0, 9)) + CDec(SettlementPerDayContentArr(RemLastIndx, 9))
                            SettlementGrandTotalArr(0, 10) = CDec(SettlementGrandTotalArr(0, 10)) + CDec(SettlementPerDayContentArr(RemLastIndx, 10))
                            SettlementGrandTotalArr(0, 11) = CDec(SettlementGrandTotalArr(0, 11)) + CDec(SettlementPerDayContentArr(RemLastIndx, 11))
                            SettlementGrandTotalArr(0, 12) = CDec(SettlementGrandTotalArr(0, 12)) + CDec(SettlementPerDayContentArr(RemLastIndx, 12))
                            SettlementGrandTotalArr(0, 13) = CDec(SettlementGrandTotalArr(0, 13)) + CDec(SettlementPerDayContentArr(RemLastIndx, 13))
                            SettlementGrandTotalArr(0, 14) = CDec(SettlementGrandTotalArr(0, 14)) + CDec(SettlementPerDayContentArr(RemLastIndx, 14))
                            SettlementGrandTotalArr(0, 15) = CDec(SettlementGrandTotalArr(0, 15)) + CDec(SettlementPerDayContentArr(RemLastIndx, 15))
                            SettlementGrandTotalArr(0, 16) = CDec(SettlementGrandTotalArr(0, 16)) + CDec(SettlementPerDayContentArr(RemLastIndx, 16))
                            SettlementGrandTotalArr(0, 17) = CDec(SettlementGrandTotalArr(0, 17)) + CDec(SettlementPerDayContentArr(RemLastIndx, 17))
                            SettlementGrandTotalArr(0, 18) = CDec(SettlementGrandTotalArr(0, 18)) + CDec(SettlementPerDayContentArr(RemLastIndx, 18))
                            SettlementGrandTotalArr(0, 20) = CDec(SettlementGrandTotalArr(0, 20)) + CDec(SettlementPerDayContentArr(RemLastIndx, 20))

                            rowIndex += 1
                            xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                            rowIndex += RemLastIndx
                            xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 21), Excel.Range)
                            xlContentSettlement = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                            xlContentSettlement.Value = SettlementPerDayContentArr
                        ElseIf GetItemPerRemittancePerDate.Count = 0 And GetItemTransferToPayment.Count = 0 And getItemPRRefundPerDate.Count > 0 Then
                            Dim RemIndx As Integer = 0
                            Dim RemLastIndx As Integer = 0
                            Dim OverAllTotal As Decimal = 0

                            ReDim SettlementPerDayContentArr(getItemPRRefundPerDate.Count, 20)

                            RemLastIndx = UBound(SettlementPerDayContentArr, 1)
                            For Each item In getItemPRRefundPerDate
                                SettlementPerDayContentArr(RemIndx, 0) = item.BillingPeriod
                                SettlementPerDayContentArr(RemIndx, 1) = item.ParticularsBillType
                                SettlementPerDayContentArr(RemIndx, 2) = item.ParticularsChargeType
                                SettlementPerDayContentArr(RemIndx, 3) = item.CollPayAllocDate.ToShortDateString.ToString()
                                SettlementPerDayContentArr(RemIndx, 4) = item.WESMBillInv

                                SettlementPerDayContentArr(RemIndx, 17) = item.Others
                                SettlementPerDayContentArr(RemIndx, 18) = item.Others
                                SettlementPerDayContentArr(RemIndx, 20) = item.Others

                                OverAllTotal += item.Others

                                'SubTotal of Per Day
                                SettlementPerDayContentArr(RemLastIndx, 5) = CDec(SettlementPerDayContentArr(RemLastIndx, 5)) + CDec(SettlementPerDayContentArr(RemIndx, 5))
                                SettlementPerDayContentArr(RemLastIndx, 6) = CDec(SettlementPerDayContentArr(RemLastIndx, 6)) + CDec(SettlementPerDayContentArr(RemIndx, 6))
                                SettlementPerDayContentArr(RemLastIndx, 7) = CDec(SettlementPerDayContentArr(RemLastIndx, 7)) + CDec(SettlementPerDayContentArr(RemIndx, 7))
                                SettlementPerDayContentArr(RemLastIndx, 8) = CDec(SettlementPerDayContentArr(RemLastIndx, 8)) + CDec(SettlementPerDayContentArr(RemIndx, 8))
                                SettlementPerDayContentArr(RemLastIndx, 9) = CDec(SettlementPerDayContentArr(RemLastIndx, 9)) + CDec(SettlementPerDayContentArr(RemIndx, 9))
                                SettlementPerDayContentArr(RemLastIndx, 10) = CDec(SettlementPerDayContentArr(RemLastIndx, 10)) + CDec(SettlementPerDayContentArr(RemIndx, 10))
                                SettlementPerDayContentArr(RemLastIndx, 11) = CDec(SettlementPerDayContentArr(RemLastIndx, 11)) + CDec(SettlementPerDayContentArr(RemIndx, 11))
                                SettlementPerDayContentArr(RemLastIndx, 12) = CDec(SettlementPerDayContentArr(RemLastIndx, 12)) + CDec(SettlementPerDayContentArr(RemIndx, 12))
                                SettlementPerDayContentArr(RemLastIndx, 13) = CDec(SettlementPerDayContentArr(RemLastIndx, 13)) + CDec(SettlementPerDayContentArr(RemIndx, 13))
                                SettlementPerDayContentArr(RemLastIndx, 14) = CDec(SettlementPerDayContentArr(RemLastIndx, 14)) + CDec(SettlementPerDayContentArr(RemIndx, 14))
                                SettlementPerDayContentArr(RemLastIndx, 15) = CDec(SettlementPerDayContentArr(RemLastIndx, 15)) + CDec(SettlementPerDayContentArr(RemIndx, 15))
                                SettlementPerDayContentArr(RemLastIndx, 16) = CDec(SettlementPerDayContentArr(RemLastIndx, 16)) + CDec(SettlementPerDayContentArr(RemIndx, 16))
                                SettlementPerDayContentArr(RemLastIndx, 17) = CDec(SettlementPerDayContentArr(RemLastIndx, 17)) + CDec(SettlementPerDayContentArr(RemIndx, 17))
                                SettlementPerDayContentArr(RemLastIndx, 18) = CDec(SettlementPerDayContentArr(RemLastIndx, 18)) + CDec(SettlementPerDayContentArr(RemIndx, 18))
                                SettlementPerDayContentArr(RemLastIndx, 20) = CDec(SettlementPerDayContentArr(RemLastIndx, 20)) + CDec(SettlementPerDayContentArr(RemIndx, 20))
                                RemIndx += 1
                            Next

                            If OverAllTotal >= 1000 Or (OverAllTotal = 0 And GetItemPerRemittancePerDate.Count > 0) Then
                                SettlementPerDayContentArr(RemLastIndx, 0) = "TOTAL REMITTANCE ON " & MonthName(ItemDate.Month).ToUpper.ToString() & " " & ItemDate.Day & ", " & ItemDate.Year
                            Else
                                SettlementPerDayContentArr(RemLastIndx, 0) = "TOTAL AMOUNT WITHHELD ON " & MonthName(ItemDate.Month).ToUpper.ToString() & " " & ItemDate.Day & ", " & ItemDate.Year
                            End If

                            SettlementGrandTotalArr(0, 5) = CDec(SettlementGrandTotalArr(0, 5)) + CDec(SettlementPerDayContentArr(RemLastIndx, 5))
                            SettlementGrandTotalArr(0, 6) = CDec(SettlementGrandTotalArr(0, 6)) + CDec(SettlementPerDayContentArr(RemLastIndx, 6))
                            SettlementGrandTotalArr(0, 7) = CDec(SettlementGrandTotalArr(0, 7)) + CDec(SettlementPerDayContentArr(RemLastIndx, 7))
                            SettlementGrandTotalArr(0, 8) = CDec(SettlementGrandTotalArr(0, 8)) + CDec(SettlementPerDayContentArr(RemLastIndx, 8))
                            SettlementGrandTotalArr(0, 9) = CDec(SettlementGrandTotalArr(0, 9)) + CDec(SettlementPerDayContentArr(RemLastIndx, 9))
                            SettlementGrandTotalArr(0, 10) = CDec(SettlementGrandTotalArr(0, 10)) + CDec(SettlementPerDayContentArr(RemLastIndx, 10))
                            SettlementGrandTotalArr(0, 11) = CDec(SettlementGrandTotalArr(0, 11)) + CDec(SettlementPerDayContentArr(RemLastIndx, 11))
                            SettlementGrandTotalArr(0, 12) = CDec(SettlementGrandTotalArr(0, 12)) + CDec(SettlementPerDayContentArr(RemLastIndx, 12))
                            SettlementGrandTotalArr(0, 13) = CDec(SettlementGrandTotalArr(0, 13)) + CDec(SettlementPerDayContentArr(RemLastIndx, 13))
                            SettlementGrandTotalArr(0, 14) = CDec(SettlementGrandTotalArr(0, 14)) + CDec(SettlementPerDayContentArr(RemLastIndx, 14))
                            SettlementGrandTotalArr(0, 15) = CDec(SettlementGrandTotalArr(0, 15)) + CDec(SettlementPerDayContentArr(RemLastIndx, 15))
                            SettlementGrandTotalArr(0, 16) = CDec(SettlementGrandTotalArr(0, 16)) + CDec(SettlementPerDayContentArr(RemLastIndx, 16))
                            SettlementGrandTotalArr(0, 17) = CDec(SettlementGrandTotalArr(0, 17)) + CDec(SettlementPerDayContentArr(RemLastIndx, 17))
                            SettlementGrandTotalArr(0, 18) = CDec(SettlementGrandTotalArr(0, 18)) + CDec(SettlementPerDayContentArr(RemLastIndx, 18))
                            SettlementGrandTotalArr(0, 20) = CDec(SettlementGrandTotalArr(0, 20)) + CDec(SettlementPerDayContentArr(RemLastIndx, 20))

                            rowIndex += 1
                            xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                            rowIndex += RemLastIndx
                            xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 21), Excel.Range)
                            xlContentSettlement = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                            xlContentSettlement.Value = SettlementPerDayContentArr
                        End If

                        'For Collection and Payment of Withholding Tax Certificate
                        SettlementPerDayContentArr = New Object(,) {}
                        Dim getItemPerWHTaxCertificate As List(Of STLNoticeNew) = (From x In WHTaxCertifColPay Where x.CollPayAllocDate = ItemDate Select x).ToList()
                        If getItemPerWHTaxCertificate.Count > 0 Then
                            Dim RemIndx As Integer = 0
                            Dim RemLastIndx As Integer = 0
                            Dim OverAllTotal As Decimal = 0

                            ReDim SettlementPerDayContentArr(getItemPerWHTaxCertificate.Count, 20)

                            RemLastIndx = UBound(SettlementPerDayContentArr, 1)
                            For Each item In getItemPerWHTaxCertificate
                                SettlementPerDayContentArr(RemIndx, 0) = item.BillingPeriod
                                SettlementPerDayContentArr(RemIndx, 1) = item.ParticularsBillType
                                SettlementPerDayContentArr(RemIndx, 2) = item.ParticularsChargeType
                                SettlementPerDayContentArr(RemIndx, 3) = item.CollPayAllocDate.ToShortDateString.ToString()
                                SettlementPerDayContentArr(RemIndx, 4) = item.WESMBillInv

                                If item.Energy < 0 Then
                                    SettlementPerDayContentArr(RemIndx, 5) = item.Energy
                                    SettlementPerDayContentArr(RemIndx, 11) = item.Energy
                                    SettlementPerDayContentArr(RemIndx, 20) = item.Energy
                                ElseIf item.Energy > 0 Then
                                    SettlementPerDayContentArr(RemIndx, 12) = item.Energy
                                    SettlementPerDayContentArr(RemIndx, 18) = item.Energy
                                    SettlementPerDayContentArr(RemIndx, 20) = item.Energy
                                End If

                                OverAllTotal += item.Others

                                'SubTotal of Per Day
                                SettlementPerDayContentArr(RemLastIndx, 5) = CDec(SettlementPerDayContentArr(RemLastIndx, 5)) + CDec(SettlementPerDayContentArr(RemIndx, 5))
                                SettlementPerDayContentArr(RemLastIndx, 6) = CDec(SettlementPerDayContentArr(RemLastIndx, 6)) + CDec(SettlementPerDayContentArr(RemIndx, 6))
                                SettlementPerDayContentArr(RemLastIndx, 7) = CDec(SettlementPerDayContentArr(RemLastIndx, 7)) + CDec(SettlementPerDayContentArr(RemIndx, 7))
                                SettlementPerDayContentArr(RemLastIndx, 8) = CDec(SettlementPerDayContentArr(RemLastIndx, 8)) + CDec(SettlementPerDayContentArr(RemIndx, 8))
                                SettlementPerDayContentArr(RemLastIndx, 9) = CDec(SettlementPerDayContentArr(RemLastIndx, 9)) + CDec(SettlementPerDayContentArr(RemIndx, 9))
                                SettlementPerDayContentArr(RemLastIndx, 10) = CDec(SettlementPerDayContentArr(RemLastIndx, 10)) + CDec(SettlementPerDayContentArr(RemIndx, 10))
                                SettlementPerDayContentArr(RemLastIndx, 11) = CDec(SettlementPerDayContentArr(RemLastIndx, 11)) + CDec(SettlementPerDayContentArr(RemIndx, 11))
                                SettlementPerDayContentArr(RemLastIndx, 12) = CDec(SettlementPerDayContentArr(RemLastIndx, 12)) + CDec(SettlementPerDayContentArr(RemIndx, 12))
                                SettlementPerDayContentArr(RemLastIndx, 13) = CDec(SettlementPerDayContentArr(RemLastIndx, 13)) + CDec(SettlementPerDayContentArr(RemIndx, 13))
                                SettlementPerDayContentArr(RemLastIndx, 14) = CDec(SettlementPerDayContentArr(RemLastIndx, 14)) + CDec(SettlementPerDayContentArr(RemIndx, 14))
                                SettlementPerDayContentArr(RemLastIndx, 15) = CDec(SettlementPerDayContentArr(RemLastIndx, 15)) + CDec(SettlementPerDayContentArr(RemIndx, 15))
                                SettlementPerDayContentArr(RemLastIndx, 16) = CDec(SettlementPerDayContentArr(RemLastIndx, 16)) + CDec(SettlementPerDayContentArr(RemIndx, 16))
                                SettlementPerDayContentArr(RemLastIndx, 17) = CDec(SettlementPerDayContentArr(RemLastIndx, 17)) + CDec(SettlementPerDayContentArr(RemIndx, 17))
                                SettlementPerDayContentArr(RemLastIndx, 18) = CDec(SettlementPerDayContentArr(RemLastIndx, 18)) + CDec(SettlementPerDayContentArr(RemIndx, 18))
                                SettlementPerDayContentArr(RemLastIndx, 20) = CDec(SettlementPerDayContentArr(RemLastIndx, 20)) + CDec(SettlementPerDayContentArr(RemIndx, 20))
                                RemIndx += 1
                            Next

                            SettlementPerDayContentArr(RemLastIndx, 0) = "TOTAL AMOUNT OF EWT CERTIFICATE ON " & MonthName(ItemDate.Month).ToUpper.ToString() & " " & ItemDate.Day & ", " & ItemDate.Year

                            SettlementGrandTotalArr(0, 5) = CDec(SettlementGrandTotalArr(0, 5)) + CDec(SettlementPerDayContentArr(RemLastIndx, 5))
                            SettlementGrandTotalArr(0, 6) = CDec(SettlementGrandTotalArr(0, 6)) + CDec(SettlementPerDayContentArr(RemLastIndx, 6))
                            SettlementGrandTotalArr(0, 7) = CDec(SettlementGrandTotalArr(0, 7)) + CDec(SettlementPerDayContentArr(RemLastIndx, 7))
                            SettlementGrandTotalArr(0, 8) = CDec(SettlementGrandTotalArr(0, 8)) + CDec(SettlementPerDayContentArr(RemLastIndx, 8))
                            SettlementGrandTotalArr(0, 9) = CDec(SettlementGrandTotalArr(0, 9)) + CDec(SettlementPerDayContentArr(RemLastIndx, 9))
                            SettlementGrandTotalArr(0, 10) = CDec(SettlementGrandTotalArr(0, 10)) + CDec(SettlementPerDayContentArr(RemLastIndx, 10))
                            SettlementGrandTotalArr(0, 11) = CDec(SettlementGrandTotalArr(0, 11)) + CDec(SettlementPerDayContentArr(RemLastIndx, 11))
                            SettlementGrandTotalArr(0, 12) = CDec(SettlementGrandTotalArr(0, 12)) + CDec(SettlementPerDayContentArr(RemLastIndx, 12))
                            SettlementGrandTotalArr(0, 13) = CDec(SettlementGrandTotalArr(0, 13)) + CDec(SettlementPerDayContentArr(RemLastIndx, 13))
                            SettlementGrandTotalArr(0, 14) = CDec(SettlementGrandTotalArr(0, 14)) + CDec(SettlementPerDayContentArr(RemLastIndx, 14))
                            SettlementGrandTotalArr(0, 15) = CDec(SettlementGrandTotalArr(0, 15)) + CDec(SettlementPerDayContentArr(RemLastIndx, 15))
                            SettlementGrandTotalArr(0, 16) = CDec(SettlementGrandTotalArr(0, 16)) + CDec(SettlementPerDayContentArr(RemLastIndx, 16))
                            SettlementGrandTotalArr(0, 17) = CDec(SettlementGrandTotalArr(0, 17)) + CDec(SettlementPerDayContentArr(RemLastIndx, 17))
                            SettlementGrandTotalArr(0, 18) = CDec(SettlementGrandTotalArr(0, 18)) + CDec(SettlementPerDayContentArr(RemLastIndx, 18))
                            SettlementGrandTotalArr(0, 20) = CDec(SettlementGrandTotalArr(0, 20)) + CDec(SettlementPerDayContentArr(RemLastIndx, 20))

                            rowIndex += 1
                            xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                            rowIndex += RemLastIndx
                            xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 21), Excel.Range)
                            xlContentSettlement = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                            xlContentSettlement.Value = SettlementPerDayContentArr
                        End If
                    Next
                    rowIndex += 2
                    xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                    xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 21), Excel.Range)
                    xlContentSettlement = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                    xlContentSettlement.Value = SettlementGrandTotalArr
                End If

            Else
                'Settlement Month Header        
                ReDim SettlementMonthHeaderArr(0, 0)
                ReDim SettlementGrandTotalArr(0, 20)
                SettlementGrandTotalArr(0, 0) = "TOTAL COLLECTIONS/REMITTANCES FOR " & MonthName(STLNoticeDate.Month, False).ToUpper.ToString()
                SettlementMonthHeaderArr(0, 0) = "SETTLEMENT FOR THE MONTH OF " & MonthName(STLNoticeDate.Month, False).ToUpper.ToString() & " " & STLNoticeDate.Year.ToString()
                rowIndex += 2
                xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                xlContentSettlement = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlContentSettlement.Value = SettlementMonthHeaderArr

                rowIndex += 2
                xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 21), Excel.Range)
                xlContentSettlement = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlContentSettlement.Value = SettlementGrandTotalArr
            End If

            '********************************************* Supply Ending Balance
            If EndingBalances.Count > 0 Then
                Dim EBLastIndx As Integer = 0
                Dim EBIndx As Integer = 0
                Dim EBCount As Integer = If(EndingBalances.Count = 0, 2, EndingBalances.Count + 1)
                ReDim EndingBalanceArr(EBCount, 20)
                EBLastIndx = UBound(EndingBalanceArr, 1)
                EndingBalanceArr(0, 0) = "ENDING BALANCES:"
                EndingBalanceArr(EBLastIndx, 0) = "TOTAL ENDING BALANCES"
                For Each Item In EndingBalances
                    EBIndx += 1
                    EndingBalanceArr(EBIndx, 0) = Item.BillingPeriod
                    EndingBalanceArr(EBIndx, 1) = Item.ParticularsChargeType
                    EndingBalanceArr(EBIndx, 2) = Item.ParticularsBillType
                    EndingBalanceArr(EBIndx, 3) = Item.OrigDueDate.ToShortDateString.ToString()
                    EndingBalanceArr(EBIndx, 4) = Item.WESMBillInv

                    Dim TotalPayable As Decimal = 0
                    Dim TotalReceivable As Decimal = 0
                    If Item.Energy < 0 Then
                        If Item.WESMBillInv.IndexOf("DMCM-") >= 0 Then
                            EndingBalanceArr(EBIndx, 12) = Item.Energy
                            TotalReceivable += Item.Energy
                        Else
                            EndingBalanceArr(EBIndx, 5) = Item.Energy
                            TotalPayable += Item.Energy
                        End If

                    ElseIf Item.Energy > 0 Then
                        If Item.WESMBillInv.IndexOf("DMCM-") >= 0 Then
                            EndingBalanceArr(EBIndx, 5) = Item.Energy
                            TotalPayable += Item.Energy
                        Else
                            EndingBalanceArr(EBIndx, 12) = Item.Energy
                            TotalReceivable += Item.Energy
                        End If
                    End If

                    If Item.VAT < 0 Then
                        EndingBalanceArr(EBIndx, 6) = Item.VAT
                        TotalPayable += Item.VAT
                    ElseIf Item.VAT > 0 Then
                        EndingBalanceArr(EBIndx, 13) = Item.VAT
                        TotalReceivable += Item.VAT

                    End If

                    If Item.DefaultOnEnergy < 0 Then
                        EndingBalanceArr(EBIndx, 7) = Item.DefaultOnEnergy
                        TotalPayable += Item.DefaultOnEnergy
                    ElseIf Item.DefaultOnEnergy > 0 Then
                        EndingBalanceArr(EBIndx, 14) = Item.DefaultOnEnergy
                        TotalReceivable += Item.DefaultOnEnergy
                    End If

                    If Item.MFAndVAT < 0 Then
                        EndingBalanceArr(EBIndx, 8) = Item.MFAndVAT
                        TotalPayable += Item.MFAndVAT
                    ElseIf Item.MFAndVAT > 0 Then
                        EndingBalanceArr(EBIndx, 15) = Item.MFAndVAT
                        TotalReceivable += Item.MFAndVAT
                    End If

                    If Item.DefaultOnMFwithVAT < 0 Then
                        EndingBalanceArr(EBIndx, 9) = Item.DefaultOnMFwithVAT
                        TotalPayable += Item.DefaultOnMFwithVAT
                    ElseIf Item.DefaultOnMFwithVAT > 0 Then
                        EndingBalanceArr(EBIndx, 16) = Item.DefaultOnMFwithVAT
                        TotalReceivable += Item.DefaultOnMFwithVAT
                    End If

                    If Item.Others < 0 Then
                        EndingBalanceArr(EBIndx, 10) = Item.Others
                        TotalPayable += Item.Others
                    ElseIf Item.Others > 0 Then
                        EndingBalanceArr(EBIndx, 17) = Item.Others
                        TotalReceivable += Item.Others
                    End If

                    EndingBalanceArr(EBIndx, 11) = If(TotalPayable = 0, Nothing, TotalPayable)
                    EndingBalanceArr(EBIndx, 18) = If(TotalReceivable = 0, Nothing, TotalReceivable)
                    EndingBalanceArr(EBIndx, 20) = CDec(EndingBalanceArr(EBIndx, 11)) + CDec(EndingBalanceArr(EBIndx, 18))
                    'SubTotal of BeginningBalance
                    EndingBalanceArr(EBLastIndx, 5) = CDec(EndingBalanceArr(EBLastIndx, 5)) + CDec(EndingBalanceArr(EBIndx, 5))
                    EndingBalanceArr(EBLastIndx, 6) = CDec(EndingBalanceArr(EBLastIndx, 6)) + CDec(EndingBalanceArr(EBIndx, 6))
                    EndingBalanceArr(EBLastIndx, 7) = CDec(EndingBalanceArr(EBLastIndx, 7)) + CDec(EndingBalanceArr(EBIndx, 7))
                    EndingBalanceArr(EBLastIndx, 8) = CDec(EndingBalanceArr(EBLastIndx, 8)) + CDec(EndingBalanceArr(EBIndx, 8))
                    EndingBalanceArr(EBLastIndx, 9) = CDec(EndingBalanceArr(EBLastIndx, 9)) + CDec(EndingBalanceArr(EBIndx, 9))
                    EndingBalanceArr(EBLastIndx, 10) = CDec(EndingBalanceArr(EBLastIndx, 10)) + CDec(EndingBalanceArr(EBIndx, 10))
                    EndingBalanceArr(EBLastIndx, 11) = CDec(EndingBalanceArr(EBLastIndx, 11)) + CDec(EndingBalanceArr(EBIndx, 11))
                    EndingBalanceArr(EBLastIndx, 12) = CDec(EndingBalanceArr(EBLastIndx, 12)) + CDec(EndingBalanceArr(EBIndx, 12))
                    EndingBalanceArr(EBLastIndx, 13) = CDec(EndingBalanceArr(EBLastIndx, 13)) + CDec(EndingBalanceArr(EBIndx, 13))
                    EndingBalanceArr(EBLastIndx, 14) = CDec(EndingBalanceArr(EBLastIndx, 14)) + CDec(EndingBalanceArr(EBIndx, 14))
                    EndingBalanceArr(EBLastIndx, 15) = CDec(EndingBalanceArr(EBLastIndx, 15)) + CDec(EndingBalanceArr(EBIndx, 15))
                    EndingBalanceArr(EBLastIndx, 16) = CDec(EndingBalanceArr(EBLastIndx, 16)) + CDec(EndingBalanceArr(EBIndx, 16))
                    EndingBalanceArr(EBLastIndx, 17) = CDec(EndingBalanceArr(EBLastIndx, 17)) + CDec(EndingBalanceArr(EBIndx, 17))
                    EndingBalanceArr(EBLastIndx, 18) = CDec(EndingBalanceArr(EBLastIndx, 18)) + CDec(EndingBalanceArr(EBIndx, 18))
                    EndingBalanceArr(EBLastIndx, 20) = CDec(EndingBalanceArr(EBLastIndx, 20)) + CDec(EndingBalanceArr(EBIndx, 20))
                Next
                rowIndex += 2
                xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                rowIndex += EBLastIndx
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 21), Excel.Range)
                xlContentEndingBalance = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlContentEndingBalance.Value = EndingBalanceArr

                ReDim RemarksArr(0, 0)
                RemarksArr(0, 0) = Remarks
                rowIndex += 2
                xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                xlContentRemarks = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                xlContentRemarks.Value = RemarksArr
            Else
                xlContentEndingBalance = Nothing
                xlContentRemarks = Nothing
            End If


            Dim FileName As String = Participant.ParticipantID.ToString() & " " & Participant.IDNumber.ToString & "_" & STLNoticeDate.ToString("MMMyyyy")

            xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbookMacroEnabled, misValue, misValue, misValue, misValue,
                        Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
            xlWorkBook.Close(False)
            xlApp.Quit()

            releaseObject(xlContentRemarks)
            releaseObject(xlContentEndingBalance)
            releaseObject(xlContentSettlement)
            releaseObject(xlContentFinalStatementOffsetting)
            releaseObject(xlContentCurrentBills)
            releaseObject(xlContentBeginningBalance)
            releaseObject(xlContentHeader)
            releaseObject(xlRowRange2)
            releaseObject(xlRowRange1)
            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlApp)

        Catch ex As Exception
            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlApp)
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Function AdjustRemmitanceDay(ByVal idate As Date) As Date
        Dim Adate As New Date

        Select Case idate.ToString("dddd")
            Case "Saturday"
                Adate = idate.AddDays(2)
            Case "Sunday"
                Adate = idate.AddDays(1)
            Case Else                
                Adate = idate
        End Select
        Return Adate
    End Function

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
