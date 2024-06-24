Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Imports Microsoft.Office.Interop
Imports System.Threading.Tasks
Imports System.Threading
Imports System.IO

Public Class SummaryofWTADetailsHelper
    Public includeReference As Boolean
    Public includeStltId As Boolean
    Public Sub New()
        'Get the current instance of the dal
        Me._DataAccess = DAL.GetInstance()
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._ListofYear = GetWTAListOfYears()
    End Sub

#Region "WESMBillHelper"
    Public _WBillHelper As WESMBillHelper
    Private ReadOnly Property WBillHelper() As WESMBillHelper
        Get
            Return _WBillHelper
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

#Region "List of Year"
    Private _ListofYear As List(Of Integer)
    Public Property ListofYear() As List(Of Integer)
        Get
            Return _ListofYear
        End Get
        Set(ByVal value As List(Of Integer))
            _ListofYear = value
        End Set
    End Property
#End Region

#Region "List of Buyer Transaction Number per MP"
    Private _ListofBuyerTransNoPerMP As Dictionary(Of String, List(Of String))
    Public Property ListofBuyerTransNoPerMP() As Dictionary(Of String, List(Of String))
        Get
            Return _ListofBuyerTransNoPerMP
        End Get
        Set(ByVal value As Dictionary(Of String, List(Of String)))
            _ListofBuyerTransNoPerMP = value
        End Set
    End Property
#End Region

#Region "List of Seller Transaction Number per MP"
    Private _ListofSellerTransNoPerMP As Dictionary(Of String, List(Of String))
    Public Property ListofSellerTransNoPerMP() As Dictionary(Of String, List(Of String))
        Get
            Return _ListofSellerTransNoPerMP
        End Get
        Set(ByVal value As Dictionary(Of String, List(Of String)))
            _ListofSellerTransNoPerMP = value
        End Set
    End Property
#End Region


#Region "Get WTA List of Years"
    Public Function GetWTAListOfYears() As List(Of Integer)
        Dim ret As New List(Of Integer)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT DISTINCT EXTRACT(YEAR FROM DUE_DATE) AS YEARS FROM AM_WESM_BILL WHERE INVOICE_NO LIKE 'TS-%'AND NOT UPPER(INVOICE_NO) LIKE '%ADJ%' AND CHARGE_TYPE = 'E'"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            ret = Me.GetWTAListOfYears(report.ReturnedIDatareader)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetWTAListOfYears(ByVal dr As IDataReader) As List(Of Integer)
        Dim ret As New List(Of Integer)
        Try
            While dr.Read()
                With dr
                    ret.Add(CInt(.Item("YEARS")))
                End With
            End While
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try
        Return ret
    End Function
#End Region

#Region "Get WTA List of MP by Year"
    Public Sub WTAListOfMPByYear(ByVal year As String)
        ListofBuyerTransNoPerMP = GetListofBuyerTransNoPerMPByYear(year)
        ListofSellerTransNoPerMP = GetListofSellerTransNoPerMPByYear(year)
    End Sub

    Private Function GetListofSellerTransNoPerMPByYear(ByVal year As String) As Dictionary(Of String, List(Of String))
        Dim ret As New Dictionary(Of String, List(Of String))
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT ID_NUMBER, INVOICE_NO FROM AM_WESM_BILL " _
                              & "WHERE INVOICE_NO LIKE 'TS-%'AND NOT UPPER(INVOICE_NO) LIKE '%ADJ%' AND CHARGE_TYPE = 'E' " _
                              & "AND AMOUNT > 0 AND EXTRACT(YEAR FROM DUE_DATE) = " & year & " " _
                              & "ORDER BY ID_NUMBER, INVOICE_NO"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetListofTransNoPerMPByYear(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetListofBuyerTransNoPerMPByYear(ByVal year As String) As Dictionary(Of String, List(Of String))
        Dim ret As New Dictionary(Of String, List(Of String))
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT ID_NUMBER, INVOICE_NO FROM AM_WESM_BILL " _
                              & "WHERE INVOICE_NO LIKE 'TS-%'AND NOT UPPER(INVOICE_NO) LIKE '%ADJ%' AND CHARGE_TYPE = 'E' " _
                              & "AND AMOUNT < 0 AND EXTRACT(YEAR FROM DUE_DATE) = " & year & " " _
                              & "ORDER BY ID_NUMBER, INVOICE_NO"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Me.GetListofTransNoPerMPByYear(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetListofTransNoPerMPByYear(ByVal dr As IDataReader) As Dictionary(Of String, List(Of String))
        Dim ret As New Dictionary(Of String, List(Of String))
        Try
            While dr.Read()
                Dim _WTATransNo As String, _MP As String
                With dr
                    _MP = CStr(.Item("ID_NUMBER")).Replace("_FIT", "")
                    _WTATransNo = CStr(.Item("INVOICE_NO"))
                    If Not ret.ContainsKey(_MP) Then
                        ret.Add(_MP, New List(Of String))
                        ret(_MP).Add(_WTATransNo)
                    Else
                        ret(_MP).Add(_WTATransNo)
                    End If
                End With
            End While
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try
        Return ret
    End Function
#End Region

#Region "Get WTA Details Summary Per WTA Trans Number"
    Private Async Function GetWTADSummaryAsSellerAsync(ByVal transno As String) As Task(Of List(Of WESMTransDetailsSummary))
        Dim ret As New List(Of WESMTransDetailsSummary)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT A.BUYER_BILLING_ID, A.BUYER_TRANS_NO, A.SELLER_BILLING_ID, A.SELLER_TRANS_NO, A.DUE_DATE, " _
                                    & "A.BB_AMOUNT_ENERGY, A.BB_AMOUNT_VAT, A.BB_AMOUNT_EWT, A.EB_AMOUNT_ENERGY, A.EB_AMOUNT_VAT, A.EB_AMOUNT_EWT, " _
                                    & "B.ID_NUMBER AS SELLER_STL_ID, C.ID_NUMBER AS BUYER_STL_ID FROM AM_WTA_DETAILS_SUMMARY_VEW2 A " _
                              & "LEFT JOIN (SELECT * FROM AM_WESM_BILL WHERE CHARGE_TYPE = 'E') B ON B.INVOICE_NO = A.SELLER_TRANS_NO " _
                              & "LEFT JOIN (SELECT * FROM AM_WESM_BILL WHERE CHARGE_TYPE = 'E') C ON C.INVOICE_NO = A.BUYER_TRANS_NO " _
                              & "WHERE A.SELLER_TRANS_NO = '" & transno & "'"

            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Await Me.GetWTADSummary(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Async Function GetWTADSummaryAsBuyerAsync(ByVal transno As String) As Task(Of List(Of WESMTransDetailsSummary))
        Dim ret As New List(Of WESMTransDetailsSummary)
        Dim report As New DataReport
        Try
            Dim SQL As String = "SELECT A.BUYER_BILLING_ID, A.BUYER_TRANS_NO, A.SELLER_BILLING_ID, A.SELLER_TRANS_NO, A.DUE_DATE, " _
                              & "A.BB_AMOUNT_ENERGY, A.BB_AMOUNT_VAT, A.BB_AMOUNT_EWT, A.EB_AMOUNT_ENERGY, A.EB_AMOUNT_VAT, A.EB_AMOUNT_EWT, " _
                              & "B.ID_NUMBER AS SELLER_STL_ID, C.ID_NUMBER AS BUYER_STL_ID FROM AM_WTA_DETAILS_SUMMARY_VEW2 A " _
                              & "LEFT JOIN (SELECT * FROM AM_WESM_BILL WHERE CHARGE_TYPE = 'E') B ON B.INVOICE_NO = A.SELLER_TRANS_NO " _
                              & "LEFT JOIN (SELECT * FROM AM_WESM_BILL WHERE CHARGE_TYPE = 'E') C ON C.INVOICE_NO = A.BUYER_TRANS_NO " _
                              & "WHERE BUYER_TRANS_NO = '" & transno & "'"

            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ret = Await Me.GetWTADSummary(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function GetWTADSummary(ByVal dr As IDataReader) As Task(Of List(Of WESMTransDetailsSummary))
        Dim result As New List(Of WESMTransDetailsSummary)
        Dim index As Integer = 0
        Try
            While dr.Read()
                index += 1
                With dr
                    Dim item As New WESMTransDetailsSummary
                    item.BuyerTransNo = CStr(.Item("BUYER_TRANS_NO"))
                    item.BuyerBillingID = CStr(.Item("BUYER_BILLING_ID"))
                    item.BuyerSTLID = CStr(.Item("BUYER_STL_ID"))
                    item.SellerTransNo = CStr(.Item("SELLER_TRANS_NO"))
                    item.SellerBillingID = CStr(.Item("SELLER_BILLING_ID"))
                    item.SellerSTLID = CStr(.Item("SELLER_STL_ID"))
                    item.DueDate = CDate(.Item("DUE_DATE"))
                    item.NewDueDate = CDate(.Item("DUE_DATE"))
                    item.OrigBalanceInEnergy = CDec(.Item("BB_AMOUNT_ENERGY"))
                    item.OrigBalanceInVAT = CDec(.Item("BB_AMOUNT_VAT"))
                    item.OrigBalanceInEWT = CDec(.Item("BB_AMOUNT_EWT"))
                    item.OutstandingBalanceInEnergy = If(IsDBNull(.Item("EB_AMOUNT_ENERGY")), 0D, CDec(.Item("EB_AMOUNT_ENERGY")))
                    item.OutstandingBalanceInVAT = If(IsDBNull(.Item("EB_AMOUNT_VAT")), 0D, CDec(.Item("EB_AMOUNT_VAT")))
                    item.OutstandingBalanceInEWT = If(IsDBNull(.Item("EB_AMOUNT_EWT")), 0D, CDec(.Item("EB_AMOUNT_EWT")))
                    item.Status = EnumWESMTransDetailsSummaryStatus.UPDATED.ToString
                    result.Add(item)
                End With
            End While
        Catch ex As Exception
            Throw New ApplicationException("Error in row " & index & " --- " & ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try
        Return Task.FromResult(result)
    End Function
#End Region

#Region "Method and Functions For Generation of WTA Details per MP on MS-Excel"
    Public Async Sub GenerateWTADetailsSummaryReport(ByVal targetPathFolder As String,
                                                     ByVal selectedIDNumber As String,
                                                     ByVal selectedYear As String,
                                                     ByVal progress As IProgress(Of ProgressClass), ByVal ct As CancellationToken)

        Dim listofBuyerTransNo As List(Of String) = New List(Of String)
        If ListofBuyerTransNoPerMP.ContainsKey(selectedIDNumber) Then
            listofBuyerTransNo = ListofBuyerTransNoPerMP(selectedIDNumber)
        End If

        Dim listofSellerTransNo As List(Of String) = New List(Of String)
        If ListofSellerTransNoPerMP.ContainsKey(selectedIDNumber) Then
            listofSellerTransNo = ListofSellerTransNoPerMP(selectedIDNumber)
        End If

        'Dim uniqueTransNoListDic As New Dictionary(Of String, String)
        Dim listOfWTADetailsSummaryAsBuyer As List(Of WESMTransDetailsSummary) = New List(Of WESMTransDetailsSummary)
        For Each item In listofBuyerTransNo
            Dim initListWTADetailsSummary As List(Of WESMTransDetailsSummary) = Await GetWTADSummaryAsBuyerAsync(item)
            listOfWTADetailsSummaryAsBuyer.AddRange(initListWTADetailsSummary)
        Next
        listOfWTADetailsSummaryAsBuyer.TrimExcess()

        Dim listOfWTADetailsSummaryAsSeller As List(Of WESMTransDetailsSummary) = New List(Of WESMTransDetailsSummary)
        For Each item In listofSellerTransNo
            Dim initListWTADetailsSummary As List(Of WESMTransDetailsSummary) = Await GetWTADSummaryAsSellerAsync(item)
            listOfWTADetailsSummaryAsSeller.AddRange(initListWTADetailsSummary)
        Next
        listOfWTADetailsSummaryAsSeller.TrimExcess()

        'uniqueTransNoListDic = Nothing

        Dim newProgress As ProgressClass = New ProgressClass
        newProgress.ProgressIndicator = 0
        newProgress.ProgressMsg = "Creating WTA Details Summary for " & selectedIDNumber
        progress.Report(newProgress)

        CreateWTADetailsSummaryInExcel(targetPathFolder, selectedIDNumber, selectedYear,
                                       listOfWTADetailsSummaryAsSeller.OrderBy(Function(x) x.DueDate).OrderBy(Function(y) y.BuyerTransNo).ToList(),
                                       listOfWTADetailsSummaryAsBuyer.OrderBy(Function(x) x.DueDate).OrderBy(Function(y) y.BuyerTransNo).ToList())

        If ct.IsCancellationRequested Then
            Throw New OperationCanceledException
        End If
    End Sub
    Public Sub CreateWTADetailsSummaryInExcel(ByVal targetPathFolder As String,
                                              ByVal selectedparticipant As String,
                                              ByVal selectedYear As String,
                                              ByVal listofWTADetailsSummaryAsSeller As List(Of WESMTransDetailsSummary),
                                              ByVal listofWTADetailsSummaryAsBuyer As List(Of WESMTransDetailsSummary))
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet1 As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlWTADetailsSummary As Excel.Range
        Dim xlWorkSheet2 As Excel.Worksheet

        Dim GetDateNow As Date = CDate(AMModule.SystemDate.ToShortDateString)
        Dim misValue As Object = System.Reflection.Missing.Value
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add()

        Dim WTADetailsSummaryHeaderSeller As Object(,) = New Object(,) {}
        ReDim WTADetailsSummaryHeaderSeller(1, 12)
        WTADetailsSummaryHeaderSeller(1, 0) = "Seller Transaction No."
        WTADetailsSummaryHeaderSeller(1, 1) = "Seller Billing Id"
        WTADetailsSummaryHeaderSeller(1, 2) = "Seller Stl Id"
        WTADetailsSummaryHeaderSeller(1, 3) = "Buyer Transaction No."
        WTADetailsSummaryHeaderSeller(1, 4) = "Buyer Billing Id"
        WTADetailsSummaryHeaderSeller(1, 5) = "Buyer Stl Id"
        WTADetailsSummaryHeaderSeller(1, 6) = "Due Date"
        WTADetailsSummaryHeaderSeller(1, 7) = "Billed Amount (Energy)"
        WTADetailsSummaryHeaderSeller(1, 8) = "Outstanding Balance (Energy)"
        WTADetailsSummaryHeaderSeller(1, 9) = "Billed Amount (Vat)"
        WTADetailsSummaryHeaderSeller(1, 10) = "Outstanding Balance(Vat)"
        WTADetailsSummaryHeaderSeller(1, 11) = "Billed Amount (WTax)"
        WTADetailsSummaryHeaderSeller(1, 12) = "Outstanding Balance (WTax)"

        'As Seller WorkSheet
        xlWorkSheet1 = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet1.Name = "as_Seller"
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(1, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(2, UBound(WTADetailsSummaryHeaderSeller, 2) + 1), Excel.Range)
        xlWTADetailsSummary = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlWTADetailsSummary.Value = WTADetailsSummaryHeaderSeller
        Dim lrow1 As Integer = xlWorkSheet1.Range("A" & xlWorkSheet1.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
        If listofWTADetailsSummaryAsSeller.Count <> 0 Then
            Dim WTADetailsSummaryArr As Object(,) = Me.GenerateWTADetailsSummaryArraySeller(listofWTADetailsSummaryAsSeller)
            xlRowRange1 = DirectCast(xlWorkSheet1.Cells(lrow1, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet1.Cells(lrow1 + UBound(WTADetailsSummaryArr, 1), UBound(WTADetailsSummaryHeaderSeller, 2) + 1), Excel.Range)
            xlWTADetailsSummary = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
            xlWTADetailsSummary.Value = WTADetailsSummaryArr
        End If

        If xlWorkBook.Worksheets.Count = 1 Then
            xlWorkSheet2 = xlWorkBook.Worksheets.Add(After:=xlWorkSheet1)
        Else
            xlWorkSheet2 = CType(xlWorkBook.Sheets(2), Excel.Worksheet)
        End If

        Dim WTADetailsSummaryHeaderBuyer As Object(,) = New Object(,) {}
        ReDim WTADetailsSummaryHeaderBuyer(1, 12)
        WTADetailsSummaryHeaderBuyer(1, 0) = "Buyer Transaction No."
        WTADetailsSummaryHeaderBuyer(1, 1) = "Buyer Billing Id"
        WTADetailsSummaryHeaderBuyer(1, 2) = "Buyer Stl Id"
        WTADetailsSummaryHeaderBuyer(1, 3) = "Seller Transaction No."
        WTADetailsSummaryHeaderBuyer(1, 4) = "Seller Billing Id"
        WTADetailsSummaryHeaderBuyer(1, 5) = "Seller Stl Id"
        WTADetailsSummaryHeaderBuyer(1, 6) = "Due Date"
        WTADetailsSummaryHeaderBuyer(1, 7) = "Billed Amount (Energy)"
        WTADetailsSummaryHeaderBuyer(1, 8) = "Outstanding Balance (Energy)"
        WTADetailsSummaryHeaderBuyer(1, 9) = "Billed Amount (Vat)"
        WTADetailsSummaryHeaderBuyer(1, 10) = "Outstanding Balance(Vat)"
        WTADetailsSummaryHeaderBuyer(1, 11) = "Billed Amount (WTax)"
        WTADetailsSummaryHeaderBuyer(1, 12) = "Outstanding Balance (WTax)"

        'As Buyer WorkSheet        
        xlWorkSheet2.Name = "as_Buyer"
        xlRowRange1 = DirectCast(xlWorkSheet2.Cells(1, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet2.Cells(2, UBound(WTADetailsSummaryHeaderBuyer, 2) + 1), Excel.Range)
        xlWTADetailsSummary = xlWorkSheet2.Range(xlRowRange1, xlRowRange2)
        xlWTADetailsSummary.Value = WTADetailsSummaryHeaderBuyer
        Dim lrow2 As Integer = xlWorkSheet2.Range("A" & xlWorkSheet2.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
        If listofWTADetailsSummaryAsBuyer.Count <> 0 Then
            Dim WTADetailsSummaryArr As Object(,) = Me.GenerateWTADetailsSummaryArrayBuyer(listofWTADetailsSummaryAsBuyer)
            xlRowRange1 = DirectCast(xlWorkSheet2.Cells(lrow1, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet2.Cells(lrow1 + UBound(WTADetailsSummaryArr, 1), UBound(WTADetailsSummaryHeaderBuyer, 2) + 1), Excel.Range)
            xlWTADetailsSummary = xlWorkSheet2.Range(xlRowRange1, xlRowRange2)
            xlWTADetailsSummary.Value = WTADetailsSummaryArr
        End If

        Dim FileName As String
        FileName = FileExistIncrementer(targetPathFolder & "\" & "WTA Details Summary_" & selectedparticipant & "_" & selectedYear & ".xlsx")

        xlWorkBook.SaveAs(FileName, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
        xlWorkBook.Close(False)
        xlApp.Quit()

        releaseObject(xlWTADetailsSummary)
        releaseObject(xlRowRange2)
        releaseObject(xlRowRange1)
        releaseObject(xlWorkSheet1)
        releaseObject(xlWorkBook)
        releaseObject(xlApp)
    End Sub

    Private Function GenerateWTADetailsSummaryArrayBuyer(ByVal wtaDetailsSummaryList As List(Of WESMTransDetailsSummary)) As Object(,)
        Dim ret As Object(,) = New Object(,) {}
        Dim ObjDicInvNo As New Dictionary(Of String, Integer)
        Dim ObjDicSeq As New Dictionary(Of Integer, Integer)
        Dim RowIndex As Integer = 0
        Dim RowCount As Integer = wtaDetailsSummaryList.Count()

        Dim WTADSArr As Object(,) = New Object(,) {}
        ReDim WTADSArr(RowCount, 12)

        For Each item In wtaDetailsSummaryList
            WTADSArr(RowIndex, 0) = item.BuyerTransNo
            WTADSArr(RowIndex, 1) = item.BuyerBillingID
            WTADSArr(RowIndex, 2) = If(includeStltId, item.BuyerSTLID, "")
            WTADSArr(RowIndex, 3) = If(includeReference, item.SellerTransNo, "")
            WTADSArr(RowIndex, 4) = item.SellerBillingID
            WTADSArr(RowIndex, 5) = If(includeStltId, item.SellerSTLID, "")
            WTADSArr(RowIndex, 6) = item.DueDate.ToShortDateString
            WTADSArr(RowIndex, 7) = item.OrigBalanceInEnergy
            WTADSArr(RowIndex, 8) = item.OutstandingBalanceInEnergy
            WTADSArr(RowIndex, 9) = item.OrigBalanceInVAT
            WTADSArr(RowIndex, 10) = item.OutstandingBalanceInVAT
            WTADSArr(RowIndex, 11) = Math.Abs(item.OrigBalanceInEWT)
            WTADSArr(RowIndex, 12) = Math.Abs(item.OutstandingBalanceInEWT)
            RowIndex += 1
        Next
        ret = WTADSArr

        Return ret
    End Function

    Private Function GenerateWTADetailsSummaryArraySeller(ByVal wtaDetailsSummaryList As List(Of WESMTransDetailsSummary)) As Object(,)
        Dim ret As Object(,) = New Object(,) {}
        Dim ObjDicInvNo As New Dictionary(Of String, Integer)
        Dim ObjDicSeq As New Dictionary(Of Integer, Integer)
        Dim RowIndex As Integer = 0
        Dim RowCount As Integer = wtaDetailsSummaryList.Count()

        Dim WTADSArr As Object(,) = New Object(,) {}
        ReDim WTADSArr(RowCount, 12)

        For Each item In wtaDetailsSummaryList
            WTADSArr(RowIndex, 0) = item.SellerTransNo
            WTADSArr(RowIndex, 1) = item.SellerBillingID
            WTADSArr(RowIndex, 2) = If(includeStltId, item.SellerSTLID, "")
            WTADSArr(RowIndex, 3) = If(includeReference, item.BuyerTransNo, "")
            WTADSArr(RowIndex, 4) = item.BuyerBillingID
            WTADSArr(RowIndex, 5) = If(includeStltId, item.BuyerSTLID, "")
            WTADSArr(RowIndex, 6) = item.DueDate.ToShortDateString
            WTADSArr(RowIndex, 7) = Math.Abs(item.OrigBalanceInEnergy)
            WTADSArr(RowIndex, 8) = Math.Abs(item.OutstandingBalanceInEnergy)
            WTADSArr(RowIndex, 9) = Math.Abs(item.OrigBalanceInVAT)
            WTADSArr(RowIndex, 10) = Math.Abs(item.OutstandingBalanceInVAT)
            WTADSArr(RowIndex, 11) = item.OrigBalanceInEWT
            WTADSArr(RowIndex, 12) = item.OutstandingBalanceInEWT
            RowIndex += 1
        Next
        ret = WTADSArr

        Return ret
    End Function
    Public Shared Function FileExistIncrementer(ByVal orginialFileName As String) As String
        Dim counter As Integer = 0
        Dim NewFileName As String = orginialFileName
        While File.Exists(NewFileName)
            counter = counter + 1
            NewFileName = String.Format("{0}\{1}{2}{3}", Path.GetDirectoryName(orginialFileName), Path.GetFileNameWithoutExtension(orginialFileName), " (" & counter.ToString() & ")", Path.GetExtension(orginialFileName))
        End While
        Return NewFileName
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
#End Region

End Class
