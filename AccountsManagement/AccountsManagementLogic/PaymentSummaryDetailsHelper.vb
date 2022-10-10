Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Imports Excel = Microsoft.Office.Interop.Excel
Public Class PaymentSummaryDetailsHelper
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
    Public PSDDate As Date
    Public PaymentRemarks As String = ""
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
            
            Dim Remittances As New List(Of STLNoticeNew)
            Dim OffettingOnFITParticipants As New List(Of STLNoticeNew)
            Dim EndingBalances As New List(Of STLNoticeNew)
            Dim TransferToPayment As New List(Of STLNoticeNew)

            Dim AmParticipantsInfo As AMParticipants = (From x In Me.WBillHelper.GetAMParticipants() Where x.ParticipantID = ParticipantID.ToString() Select x).FirstOrDefault()

            Remittances = WBillHelper.GetPaymentSummaryDetails(PSDDate, AmParticipantsInfo.IDNumber)
            TransferToPayment = WBillHelper.GetPaymentSummaryDetailsTransfer(PSDDate, AmParticipantsInfo.IDNumber)

            Dim RecordsCount As Integer = Remittances.Count + TransferToPayment.Count

            Me.ProcessRemarks = "Exporting Payment Summary Details of " & ParticipantID
            If RecordsCount > 0 Then
                FileCounter += RecordsCount
                Me.GenerateSTLNoticeWithTemplateInExcel(FilePath, Remittances, TransferToPayment, _
                                                      AmParticipantsInfo, PaymentRemarks)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Private Sub GenerateSTLNoticeWithTemplateInExcel(ByVal SavingPathName As String, ByVal Remittances As List(Of STLNoticeNew), ByVal TransferToPayment As List(Of STLNoticeNew), _
                                                    ByVal Participant As AMParticipants, Optional ByVal Remarks As String = "")
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlContentHeader As Excel.Range        
        Dim xlContentSettlement As Excel.Range
        Dim xlContentRemarks As Excel.Range

        Dim misValue As Object = System.Reflection.Missing.Value
        Dim TemplatePathFile = AppDomain.CurrentDomain.BaseDirectory & "Excel_Template\PaymentSummaryDetails.xltm"
        Dim rowIndex As Integer = 9

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add(TemplatePathFile)
        xlWorkSheet = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet.Name = PSDDate.ToString("MMMM-dd-yyyy")
        xlContentRemarks = Nothing

        Dim ContentHeader As Object(,) = New Object(,) {}        
        Dim SettlementMonthHeaderArr As Object(,) = New Object(,) {}
        Dim SettlementGrandTotalArr As Object(,) = New Object(,) {}        
        Dim RemarksArr As Object(,) = New Object(,) {}

        Try
            '********************************************* Supply Content Header
            ReDim ContentHeader(3, 0)
            ContentHeader(0, 0) = "MP Name: " & Participant.FullName.ToString()
            ContentHeader(1, 0) = "MP ID No.: " & Participant.IDNumber.ToString()
            ContentHeader(2, 0) = "'" & PSDDate.ToString("MMMM dd, yyyy")

            xlRowRange1 = DirectCast(xlWorkSheet.Cells(2, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet.Cells(4, 1), Excel.Range)
            xlContentHeader = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
            xlContentHeader.Value = ContentHeader

            '********************************************* Supply Settlement For the selected Month        
            If Remittances.Count > 0 Or TransferToPayment.Count > 0 Then
                'Settlement Month Header        
                ReDim SettlementMonthHeaderArr(0, 0)
                ReDim SettlementGrandTotalArr(0, 20)
                SettlementGrandTotalArr(0, 0) = "TOTAL REMITTANCES FOR " & MonthName(PSDDate.Month, False).ToUpper.ToString()                
                rowIndex += 0
                
                'Collection for the month
                Dim CollRemittPerDate As List(Of Date) = (From x In Remittances Select x.CollPayAllocDate Order By CollPayAllocDate).Union _
                                                         (From y In TransferToPayment Select y.CollPayAllocDate Order By CollPayAllocDate).Distinct.ToList()
                Dim RemittancesList As List(Of STLNoticeNew) = (From x In Remittances Where x.TransType = 2 Select x Order By x.BillingPeriod, x.WESMBillInv, x.IndexSorting, x.ParticularsChargeType).ToList
                Dim DeferredDate As New Date
                If CollRemittPerDate.Count > 0 Then
                    For Each ItemDate In CollRemittPerDate
                        'Remittances
                        Dim SettlementPerDayContentArr As Object(,) = New Object(,) {}
                        Dim GetItemPerRemittancePerDate As List(Of STLNoticeNew) = (From x In RemittancesList Where x.CollPayAllocDate = ItemDate Select x).ToList()
                        Dim GetItemTransferToPayment As List(Of STLNoticeNew) = (From x In TransferToPayment Where x.CollPayAllocDate = ItemDate Select x).ToList()
                        If GetItemPerRemittancePerDate.Count > 0 Then
                            Dim RemIndx As Integer = 0
                            Dim RemLastIndx As Integer = 0
                            Dim OverAllTotal As Decimal = 0
                            If GetItemTransferToPayment.Count = 0 Then
                                ReDim SettlementPerDayContentArr(GetItemPerRemittancePerDate.Count, 20)
                            Else
                                ReDim SettlementPerDayContentArr(GetItemPerRemittancePerDate.Count + GetItemTransferToPayment.Count, 20)
                            End If

                            RemLastIndx = UBound(SettlementPerDayContentArr, 1)

                            For Each Item In GetItemPerRemittancePerDate
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
                                    SettlementPerDayContentArr(RemIndx, 12) = Item.Energy
                                    TotalReceivable += Item.Energy
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
                                    If Item.ParticularsChargeType = "MF-Wtax" Then
                                        SettlementPerDayContentArr(RemIndx, 15) = Item.MFAndVAT
                                        TotalReceivable += Item.MFAndVAT
                                    Else
                                        SettlementPerDayContentArr(RemIndx, 8) = Item.MFAndVAT
                                        TotalPayable += Item.MFAndVAT
                                    End If

                                ElseIf Item.MFAndVAT > 0 Then
                                    If Item.ParticularsChargeType = "MF-Wtax" Then
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

                            If OverAllTotal >= 1000 Or (OverAllTotal = 0 And GetItemPerRemittancePerDate.Count > 0) Then
                                SettlementPerDayContentArr(RemLastIndx, 0) = "TOTAL REMITTANCE ON " & MonthName(ItemDate.Month).ToUpper.ToString() & " " & ItemDate.Day & ", " & ItemDate.Year
                            Else
                                SettlementPerDayContentArr(RemLastIndx, 0) = "TOTAL AMOUNT WITHHELD ON " & MonthName(ItemDate.Month).ToUpper.ToString() & " " & ItemDate.Day & ", " & ItemDate.Year
                            End If

                            rowIndex += 1
                            xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                            rowIndex += RemLastIndx
                            xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 21), Excel.Range)
                            xlContentSettlement = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                            xlContentSettlement.Value = SettlementPerDayContentArr
                        ElseIf GetItemPerRemittancePerDate.Count = 0 And GetItemTransferToPayment.Count > 0 Then
                            Dim RemIndx As Integer = 0
                            Dim RemLastIndx As Integer = 0
                            Dim OverAllTotal As Decimal = 0
                            If GetItemTransferToPayment.Count = 0 Then
                                ReDim SettlementPerDayContentArr(GetItemPerRemittancePerDate.Count, 20)
                            Else
                                ReDim SettlementPerDayContentArr(GetItemPerRemittancePerDate.Count + GetItemTransferToPayment.Count, 20)
                            End If

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
                                OverAllTotal += item.Others
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
                    Next

                    ReDim RemarksArr(0, 0)
                    RemarksArr(0, 0) = Remarks
                    rowIndex += 1
                    xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                    xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
                    xlContentRemarks = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
                    xlContentRemarks.Value = RemarksArr
                End If
            Else

                xlContentSettlement = Nothing
                xlContentRemarks = Nothing
            End If


            Dim FileName As String = Participant.ParticipantID.ToString() & " " & Participant.IDNumber.ToString & "_" & PSDDate.ToString("MMMddyyyy") & " Payment Details"
            xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbookMacroEnabled, misValue, misValue, misValue, misValue,
                        Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
            xlWorkBook.Close(False)
            xlApp.Quit()

            releaseObject(xlContentRemarks)
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
