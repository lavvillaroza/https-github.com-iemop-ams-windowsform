Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.IO
Imports System.Windows.Forms

Public Class MAPReportHelper
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
    Public AMParticipantsList As New List(Of AMParticipants)
    Private WESMBillList As New List(Of WESMBill)
    Private SettlementRun As New List(Of String)
    Private DueDate As New List(Of Date)
    Private SelectionOfParticipants As New List(Of String)
    Private WBSalesAndPurchased As New List(Of WESMBillSalesAndPurchased)
    Private BIRATCList As New List(Of BIRAlphanumericTaxCode)
    Public TargetedPath As String
    Public SelectedDueDate As Date

    Public Sub New()
        Me._DataAccess = DAL.GetInstance()
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._BFactory = BusinessFactory.GetInstance
        Me.AMParticipantsList = Me.WBillHelper.GetAMParticipants()
        Me.DueDate = Me.WBillHelper.GetDueDateSalesAndPurchased()
        Me.BIRATCList = WBillHelper.GetBIRATC()
        'Me.WESMBillSaleAndPurchased = Me.WBillHelper.GetWESMBillSalesAndPurchasedCount(123)
    End Sub

    Public Function GetDueDate() As List(Of Date)
        Dim ret As New List(Of Date)
        ret = Me.DueDate
        Return ret
    End Function

    Public Function GetParticipantList(ByVal xDueDate As Date) As List(Of String)
        Dim Ret As New List(Of String)
        Me.SelectionOfParticipants = WBillHelper.GetParticipantsListBasedonWBSAP(CDate(xDueDate.ToShortDateString))
        Ret = Me.SelectionOfParticipants
        Return Ret
    End Function

    Public Sub GetWBSalesAndPurchased()
        Try
            Me.WBSalesAndPurchased = WBillHelper.GetWESMBillSalesAndPurchasedForEWT(SelectedDueDate)
            Me.WESMBillList = WBillHelper.GetWESMBills(SelectedDueDate)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub

    Public Sub GenerateMAPRawReport()
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim xlWorkSheet2 As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlHeader As Excel.Range
        Dim xlDetails As Excel.Range
        Dim xlControl As Excel.Range

        Try
            Dim misValue As Object = System.Reflection.Missing.Value

            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Add()
            If Not xlWorkBook.Worksheets.Count > 1 Then
                xlWorkBook.Worksheets.Add(Before:=xlWorkBook.Worksheets(1))
            End If

            xlWorkSheet = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
            xlWorkSheet2 = CType(xlWorkBook.Sheets(2), Excel.Worksheet)
            xlWorkSheet.Name = AMModule.CompanyTinNumber & AMModule.CompanyTinExt & SelectedDueDate.ToString("MMyy") & AMModule.FormTypeCodeBaseNumber & "temp"
            xlWorkSheet2.Name = AMModule.CompanyTinNumber & AMModule.CompanyTinExt & SelectedDueDate.ToString("MMyy") & AMModule.FormTypeCodeBaseNumber

            Dim ArrHeader As Object(,) = New Object(,) {}
            Dim ArrHeaderContent As Object(,) = New Object(,) {}
            Dim ArrDetails As Object(,) = New Object(,) {}
            Dim ArrDetailsContent As Object(,) = New Object(,) {}
            Dim ArrControl As Object(,) = New Object(,) {}
            Dim ArrControlContent As Object(,) = New Object(,) {}

            'Header
            ReDim ArrHeader(0, 6)
            ArrHeader(0, 0) = "Fixed"
            ArrHeader(0, 1) = "Fixed"
            ArrHeader(0, 2) = "PEMC TIN"
            ArrHeader(0, 3) = "PEMC TIN Extension"
            ArrHeader(0, 4) = "Template"
            ArrHeader(0, 5) = "Month/Year Filed"
            ArrHeader(0, 6) = "RDO No. (Fixed)"

            Dim rowIndex As Integer = 1
            xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 7), Excel.Range)
            xlHeader = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
            xlHeader.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
            xlHeader.Value = ArrHeader

            'Header Content
            ReDim ArrHeaderContent(0, 6)
            ArrHeaderContent(0, 0) = AMModule.AlphaTypeHeader
            ArrHeaderContent(0, 1) = AMModule.FormTypeCodeHeader
            ArrHeaderContent(0, 2) = AMModule.CompanyTinNumber
            ArrHeaderContent(0, 3) = AMModule.CompanyTinExt
            ArrHeaderContent(0, 4) = AMModule.CompanyFullName
            ArrHeaderContent(0, 5) = "'" & SelectedDueDate.ToString("MM/yyyy")
            ArrHeaderContent(0, 6) = AMModule.CompanyRDONumber

            'Worksheet1     
            rowIndex += 1
            xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 7), Excel.Range)
            xlDetails = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
            xlDetails.Value = ArrHeaderContent

            'Worksheet2              
            xlRowRange1 = DirectCast(xlWorkSheet2.Cells(rowIndex - 1, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet2.Cells(rowIndex - 1, 7), Excel.Range)
            xlDetails = xlWorkSheet2.Range(xlRowRange1, xlRowRange2)
            xlDetails.Value = ArrHeaderContent

            'Details
            ReDim ArrDetails(0, 14)
            ArrDetails(0, 0) = "Fixed"
            ArrDetails(0, 1) = "Fixed"
            ArrDetails(0, 2) = "Series/Number"
            ArrDetails(0, 3) = "Tin"
            ArrDetails(0, 4) = "Tin Extension"
            ArrDetails(0, 5) = "Company Name"
            ArrDetails(0, 6) = "If Individual (Surname)"
            ArrDetails(0, 7) = "First Name"
            ArrDetails(0, 8) = "MI"
            ArrDetails(0, 9) = "Month/Year  Filed"
            ArrDetails(0, 10) = "ATC"
            ArrDetails(0, 11) = "Description of ATC"
            ArrDetails(0, 12) = "Rate"
            ArrDetails(0, 13) = "Tax Base"
            ArrDetails(0, 14) = "Amount"

            rowIndex += 2
            xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 15), Excel.Range)
            xlDetails = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
            xlDetails.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
            xlDetails.Value = ArrDetails

            'Details Content            
            Dim SumOfSalesAndPurchased = (From x In Me.WBSalesAndPurchased _
                                          Group By x.IDNumber.IDNumber _
                                          Into VATBLSales = Sum(x.VatableSales), _
                                            ZeroRatedSales = Sum(x.ZeroRatedSales), _
                                            ZeroRatedEcozone = Sum(x.ZeroRatedEcozone), _
                                            WithholdingTax = Sum(x.WithholdingTAX) _
                                          Select IDNumber, VATBLSales, ZeroRatedSales, ZeroRatedEcozone, WithholdingTax).ToList()

            ReDim ArrDetailsContent(SumOfSalesAndPurchased.Count, 14)
            Dim SeriesNumber As Integer = 0
            Dim iRow As Integer = 0
            For Each item In SumOfSalesAndPurchased                
                If item.VATBLSales <> 0 Or item.ZeroRatedSales <> 0 Or item.ZeroRatedEcozone <> 0 Then
                    Dim GetAMParticipantInfo As AMParticipants = (From x In Me.AMParticipantsList Where x.IDNumber = item.IDNumber).FirstOrDefault
                    Dim GetBIRATCCode As BIRAlphanumericTaxCode = (From x In Me.BIRATCList Where x.ATCName = GetAMParticipantInfo.BIRATCType Select x).FirstOrDefault()
                    Dim Tin As String = ""
                    Dim TinExt As String = ""
                    Dim cnt As Integer = 0
                    For Each strchr In GetAMParticipantInfo.TIN.Split(CChar("-"))
                        cnt += 1
                        If cnt = 4 Then
                            TinExt = strchr
                        Else
                            Tin &= strchr
                        End If
                    Next
                    SeriesNumber += 1
                    ArrDetailsContent(iRow, 0) = AMModule.AlphaTypeDetails
                    ArrDetailsContent(iRow, 1) = AMModule.FormTypeCodeDetails
                    ArrDetailsContent(iRow, 2) = "'" & SeriesNumber.ToString("000000000")
                    ArrDetailsContent(iRow, 3) = "'" & Tin
                    ArrDetailsContent(iRow, 4) = "'" & TinExt
                    ArrDetailsContent(iRow, 5) = GetAMParticipantInfo.FullName
                    ArrDetailsContent(iRow, 6) = ""
                    ArrDetailsContent(iRow, 7) = ""
                    ArrDetailsContent(iRow, 8) = ""
                    ArrDetailsContent(iRow, 9) = "'" & SelectedDueDate.ToString("MM/yyyy")
                    ArrDetailsContent(iRow, 10) = GetBIRATCCode.ATCName
                    ArrDetailsContent(iRow, 11) = GetBIRATCCode.ATCDescription
                    ArrDetailsContent(iRow, 12) = "0.02" 'GetAMParticipantInfo.APEnergyWTax
                    ArrDetailsContent(iRow, 13) = item.VATBLSales + item.ZeroRatedSales + item.ZeroRatedEcozone
                    ArrDetailsContent(iRow, 14) = Math.Abs(item.WithholdingTax)
                    iRow += 1
                End If
            Next

            'Worksheet 1
            rowIndex += 1
            xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
            rowIndex += SumOfSalesAndPurchased.Count
            xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 15), Excel.Range)
            xlDetails = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
            xlDetails.Value = ArrDetailsContent

            'Worksheet 2            
            xlRowRange1 = DirectCast(xlWorkSheet2.Cells(rowIndex - (SumOfSalesAndPurchased.Count + 3), 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet2.Cells(rowIndex - 4, 15), Excel.Range)
            xlDetails = xlWorkSheet2.Range(xlRowRange1, xlRowRange2)
            xlDetails.Value = ArrDetailsContent


            'Control
            ReDim ArrControl(0, 6)
            ArrControl(0, 0) = "Fixed"
            ArrControl(0, 1) = "Fixed"
            ArrControl(0, 2) = "PEMC TIN"
            ArrControl(0, 3) = "PEMC TIN Extension"
            ArrControl(0, 4) = "Month/Year Filed"
            ArrControl(0, 5) = "Sum- Tax Base"
            ArrControl(0, 6) = "Sum - Withholding Tax"

            'Worksheet 1
            rowIndex += 1
            xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 7), Excel.Range)
            xlControl = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
            xlControl.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
            xlControl.Value = ArrControl

            'Control Content
            ReDim ArrControlContent(0, 6)
            ArrControlContent(0, 0) = AMModule.AlphaTypeControl
            ArrControlContent(0, 1) = AMModule.FormTypeCodeControl
            ArrControlContent(0, 2) = AMModule.CompanyTinNumber
            ArrControlContent(0, 3) = AMModule.CompanyTinExt
            ArrControlContent(0, 4) = "'" & SelectedDueDate.ToString("MM/yyyy")
            ArrControlContent(0, 5) = (From x In Me.WBSalesAndPurchased Select x.VatableSales).Sum()
            ArrControlContent(0, 6) = (From x In Me.WBSalesAndPurchased Select x.WithholdingTAX).Sum()

            'Worksheet1
            rowIndex += 1
            xlRowRange1 = DirectCast(xlWorkSheet.Cells(rowIndex, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet.Cells(rowIndex, 7), Excel.Range)
            xlControl = xlWorkSheet.Range(xlRowRange1, xlRowRange2)
            xlControl.Value = ArrControlContent

            'Worksheet2            
            xlRowRange1 = DirectCast(xlWorkSheet2.Cells(rowIndex - 5, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet2.Cells(rowIndex - 5, 7), Excel.Range)
            xlControl = xlWorkSheet2.Range(xlRowRange1, xlRowRange2)
            xlControl.Value = ArrControlContent

            TargetedPath &= "\EWT MAP Report - " & SelectedDueDate.ToShortDateString.ToString().Replace("/", "")



            xlWorkBook.SaveAs(TargetedPath, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                        Excel.XlSaveAsAccessMode.xlNoChange, misValue, misValue, misValue, misValue, misValue)
            xlWorkBook.Close(False)
            xlApp.Quit()

            releaseObject(CObj(xlControl))
            releaseObject(CObj(xlDetails))
            releaseObject(CObj(xlHeader))
            releaseObject(CObj(xlRowRange2))
            releaseObject(CObj(xlRowRange1))
            releaseObject(CObj(xlWorkSheet2))
            releaseObject(CObj(xlWorkSheet))
            releaseObject(CObj(xlWorkBook))
            releaseObject(CObj(xlApp))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub GenerateBIR2307(ByVal oParticipantID As String)
        Try
            Dim FilterParticipantInfo As AMParticipants = (From x In Me.AMParticipantsList Where x.ParticipantID = oParticipantID Select x).FirstOrDefault

            Dim GetIDNumberInWBSAP As List(Of String) = (From x In Me.WBSalesAndPurchased _
                                                         Where x.IDNumber.IDNumber = FilterParticipantInfo.IDNumber _
                                                         Select x.IDNumber.IDNumber Order By IDNumber).Distinct.ToList()

            Dim WESMBillList As List(Of WESMBill) = (From x In Me.WESMBillList _
                                                    Where x.DueDate.ToString("YYYY") = SelectedDueDate.ToString("YYYY") _
                                                    And x.IDNumber = FilterParticipantInfo.IDNumber _
                                                    Select x).ToList()

            Dim WBSAPList As List(Of WESMBillSalesAndPurchased) = (From x In Me.WBSalesAndPurchased
                                                                   Where x.IDNumber.IDNumber = FilterParticipantInfo.IDNumber
                                                                   Select x).ToList()

            Me.GenerateBIR2307Excel(WESMBillList, WBSAPList, TargetedPath, FilterParticipantInfo)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Public Sub GenerateBIR2307Excel(ByVal _WESMBill As List(Of WESMBill),
                                    ByVal WBSAPItem As List(Of WESMBillSalesAndPurchased),
                                    ByVal SavingPathFile As String,
                                    ByVal AmParticipantInfo As AMParticipants)

        Dim GetBIRATC As BIRAlphanumericTaxCode = (From x In BIRATCList Where x.ATCName = AmParticipantInfo.BIRATCType Select x).FirstOrDefault
        Dim ArrTin() As String = {}
        'If Not ArrTin.Length = 4 Then
        '    Throw New Exception(AmParticipantInfo.ParticipantID & " has invalid TIN format. Required format should be '###-###-###-###'.")
        '    Exit Sub
        'End If

        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim xlContent1 As Excel.Range
        Dim xlContent2 As Excel.Range
        Dim xlContent3 As Excel.Range
        Dim xlContent4 As Excel.Range

        Try

            Dim GetFromDate As Date = CDate((From x In _WESMBill Select x.DueDate).Min.ToShortDateString)
            Dim GetToDate As Date = CDate((From x In _WESMBill Select x.DueDate).Max.ToShortDateString)


            Dim misValue As Object = System.Reflection.Missing.Value
            Dim TemplatePathFile = AppDomain.CurrentDomain.BaseDirectory & "Excel_Template\BIR2307Temp.xltx"
            Dim rowIndex As Integer = 1
            Dim FromDate As Date = GetFromDate
            Dim ToDate As Date = GetToDate


            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Add(TemplatePathFile)
            xlWorkSheet = CType(xlWorkBook.Sheets(1), Excel.Worksheet)

            With xlWorkSheet.Shapes
                'From Date
                With .Item("FromMM")
                    .TextFrame.Characters.Text = FromDate.ToString("MM")
                End With
                With .Item("FromDD")
                    .TextFrame.Characters.Text = FromDate.ToString("dd")
                End With
                With .Item("FromYY")
                    .TextFrame.Characters.Text = FromDate.ToString("yy")
                End With

                'To Date
                With .Item("ToMM")
                    .TextFrame.Characters.Text = FromDate.ToString("MM")
                End With
                With .Item("ToDD")
                    .TextFrame.Characters.Text = FromDate.ToString("dd")
                End With
                With .Item("ToYY")
                    .TextFrame.Characters.Text = FromDate.ToString("yy")
                End With

                If Not AmParticipantInfo.TIN.ToString = "" Or Not AmParticipantInfo.TIN.ToString.Length = 0 Then
                    ArrTin = AmParticipantInfo.TIN.Split(CChar("-"))
                    'Tin Number
                    With .Item("Tin1stDiv")
                        .TextFrame.Characters.Text = ArrTin(0).ToString
                    End With
                    With .Item("Tin2ndDiv")
                        .TextFrame.Characters.Text = ArrTin(1).ToString
                    End With
                    With .Item("Tin3rdDiv")
                        .TextFrame.Characters.Text = ArrTin(2).ToString
                    End With
                    With .Item("Tin4thDiv")
                        .TextFrame.Characters.Text = ArrTin(3).ToString
                    End With
                End If

                'Payeesname
                With .Item("PayeesName")
                    .TextFrame.Characters.Text = AmParticipantInfo.FullName
                End With

                'Address
                With .Item("RegisteredAddress")
                    .TextFrame.Characters.Text = AmParticipantInfo.ParticipantAddress
                End With

                'ZipCode
                With .Item("4AZipCode")
                    .TextFrame.Characters.Text = AmParticipantInfo.ZipCode
                End With

                'Income Payments Subject to Expanded Withholding Tax
                xlContent1 = DirectCast(xlWorkSheet.Cells(33, 2), Excel.Range)
                xlContent1.Value = GetBIRATC.ATCDescription

                'ATC
                xlContent2 = DirectCast(xlWorkSheet.Cells(33, 14), Excel.Range)
                xlContent2.Value = GetBIRATC.ATCName

                '1st Month of the Quarter
                xlContent3 = DirectCast(xlWorkSheet.Cells(33, 18), Excel.Range)
                xlContent3.Value = FormatNumber((From x In WBSAPItem Select x.VatableSales + x.ZeroRatedEcozone + x.ZeroRatedSales).Sum().ToString(), , , TriState.True)

                'Tax Withheld For the Quarter
                xlContent4 = DirectCast(xlWorkSheet.Cells(33, 38), Excel.Range)
                xlContent4.Value = FormatNumber(Math.Abs((From x In WBSAPItem Select x.WithholdingTAX).Sum()).ToString(), , , TriState.True)

            End With

            Dim vRes As DialogResult = DialogResult.Yes

            SavingPathFile = SavingPathFile & "\" & AmParticipantInfo.ParticipantID.ToString() & "_" & GetFromDate.ToString("MMddyyyy") & "-" & GetToDate.ToString("MMddyyyy") & "_BIR2307"
            If File.Exists(SavingPathFile) Then
                vRes = MessageBox.Show(String.Format("A file named '{0}' already exists in this location. Do you want to replace it?", SavingPathFile), _
                           "Microsoft Excel", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
                xlApp.DisplayAlerts = False
            End If

            If vRes = DialogResult.Yes Then
                xlWorkBook.SaveAs(SavingPathFile, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                        Excel.XlSaveAsAccessMode.xlNoChange, misValue, misValue, misValue, misValue, misValue)
            End If
            
            xlWorkBook.Close(False)
            xlApp.Quit()

            releaseObject(CObj(xlContent4))
            releaseObject(CObj(xlContent3))
            releaseObject(CObj(xlContent2))
            releaseObject(CObj(xlContent1))
            releaseObject(CObj(xlWorkSheet))
            releaseObject(CObj(xlWorkBook))
            releaseObject(CObj(xlApp))

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub releaseObject(ByRef obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Sub
End Class
